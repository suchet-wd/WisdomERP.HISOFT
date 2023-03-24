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

Public Class wImportOrderDemandPull

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
        Dim _bRetProgrammeVendor As Boolean = False

        Try
            If Me.FNHSysVenderPramId.Text.Trim() <> "" Then
                _bRetProgrammeVendor = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
                Me.FNHSysVenderPramId.Focus()
            End If
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            End If
        End Try

        Return _bRetProgrammeVendor

    End Function

    Private Function W_PRCbValidateConfirmGenerateFactoryOrder() As Boolean
        Dim _bRet As Boolean = False
        Try
            If Me.FNHSysCustId.Text.Trim() <> "" Then
                If Me.FNHSysVenderPramId.Text.Trim() <> "" Then
                    If Me.FNHSysCmpRunId.Text.Trim() <> "" Then
                        If Me.FNHSysBuyId.Text.Trim() <> "" Then
                            If W_PRCbAddNewStyleImport() = True Then
                                _bRet = True
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
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
                    Me.FNHSysVenderPramId.Focus()
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

    Private Function W_PRCbRemoveGridViewColumn(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraGrid.Views.Grid.GridView
        Try
            With pGridView
                For nLoopColGridView As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns.Item(nLoopColGridView).Name.ToString.ToUpper
                        Case "FTMatColorName".ToString.ToUpper, "FTMatColorCode".ToString.ToUpper
                            '...do nothing
                        Case "FNSeq".ToString.ToUpper, "FTSizeSpecDesc".ToString.ToUpper
                            '...do nothing
                            'Case "oColFNRowImport".ToString.ToUpper, "oColFTPONo".ToString.ToUpper, "oColFTPOTrading".ToString.ToUpper, "oColFTPOItem".ToUpper.ToUpper, "oColFNHSysMerTeamId".ToUpper.ToUpper & _
                            '     "oColFTMerTeamCode".ToString.ToUpper, "oColFTMerTeamDesc".ToUpper.ToUpper, "oColFTPOCreateDate".ToString.ToUpper, "oColFTOrderDate".ToString.ToUpper, "oColFNHSysPlantId".ToString.ToUpper & _
                            '     "oColFTPlantDesc".ToString.ToUpper, "oColFNHSysBuyGrpId".ToString.ToUpper, " oColFTBuyGrpCode".ToString.ToUpper, "oColFTBuyGrpDesc".ToString.ToUpper, "oColFTStyle".ToString.ToUpper & _
                            '     "oColFTStyleDesc".ToString.ToUpper, "oColFNHSysMainCategoryId".ToString.ToUpper, "oColFTMainCategoryCode".ToString.ToUpper, "oColFTMainCategoryDesc".ToString.ToUpper, "oColFTProdTypeDesc".ToString.ToUpper & _
                            '     "oColFTMaterial".ToString.ToUpper, "oColFTMaterialDesc".ToString.ToUpper, "oColFTPlanningSeason".ToString.ToUpper, "oColFTYear".ToString.ToUpper, " oColFNHSysBuyerId".ToString.ToUpper & _
                            '     "oColFTBuyerCode".ToString.ToUpper, "oColFTBuyerDesc".ToString.ToUpper, "oColFNHSysCountryId".ToString.ToUpper, "oColFNHSysCountryCode".ToString.ToUpper, "oColFTCountryDesc".ToString.ToUpper & _
                            '     "oColFNHSysGenderId".ToString.ToUpper, "oColFTGenderCode".ToString.ToUpper, "oColFTGenderDesc".ToString.ToUpper, "oColFDShipmentDate".ToString.ToUpper, "oColFDShipmentDateOriginal".ToString.ToUpper & _
                            '     "oColFNHSysMatColorId".ToString.ToUpper, "oColFTColorwayCode".ToString.ToUpper, "oColFTColorwayDesc".ToString.ToUpper, "oColFNHSysShipModeId".ToString.ToUpper, " oColFTShipModeDesc".ToString.ToUpper & _
                            '     "oColFNHSysUnitId".ToString.ToUpper, "oColFTUnitDesc".ToString.ToUpper
                            '    '...do nothing
                        Case "oColFNRowImport".ToString.ToUpper, "oColFTPONo".ToString.ToUpper, "oColFTPOTrading".ToString.ToUpper, "oColFTPOItem".ToString.ToUpper
                            '...do nothing
                        Case "oColFNHSysMerTeamId".ToString.ToUpper, "ColFTMerTeamCode".ToString.ToUpper, "oColFTMerTeamDesc".ToString.ToUpper, "oColFTPOCreateDate".ToString.ToUpper
                            '...do nothing
                        Case "oColFTOrderDate".ToString.ToUpper, "oColFNHSysPlantId".ToString.ToUpper, "oColFTPlantDesc".ToString.ToUpper, "oColFNHSysBuyGrpId".ToString.ToUpper
                            '...do nothing
                        Case "oColFTBuyGrpCode".ToString.ToUpper, "oColFTBuyGrpDesc".ToString.ToUpper, "oColFTStyle".ToString.ToUpper, "oColFTStyleDesc".ToString.ToUpper, "oColFNHSysMainCategoryId".ToString.ToUpper
                            '...do nothing
                        Case "oColFTMainCategoryCode".ToString.ToUpper, "oColFTMainCategoryDesc".ToString.ToUpper, "oColFTProdTypeDesc".ToString.ToUpper, "oColFTMaterial".ToString.ToUpper
                            '...do nothing
                        Case "oColFTMaterialDesc".ToString.ToUpper, "oColFTPlanningSeason".ToString.ToUpper, "oColFTYear".ToString.ToUpper, "oColFNHSysBuyerId".ToString.ToUpper
                            '...do nothing
                        Case "oColFTBuyerCode".ToString.ToUpper, "oColFTBuyerDesc".ToString.ToUpper, "oColFNHSysCountryId".ToString.ToUpper, "oColFNHSysCountryCode".ToString.ToUpper
                            '...do nothing
                        Case "oColFTCountryDesc".ToString.ToUpper, "oColFNHSysGenderId".ToString.ToUpper, "oColFTGenderCode".ToString.ToUpper, "oColFTGenderDesc".ToString.ToUpper
                            '...do nothing
                        Case "oColFDShipmentDate".ToString.ToUpper, "oColFDShipmentDateOriginal".ToString.ToUpper, "oColFNHSysMatColorId".ToString.ToUpper, "oColFTColorwayCode".ToString.ToUpper
                            '...do nothing
                        Case "oColFTColorwayDesc".ToUpper.ToUpper, "oColFNHSysShipModeId".ToString.ToUpper, "oColFTShipModeDesc".ToString.ToUpper, "oColFNHSysUnitId".ToString.ToUpper, "oColFTUnitDesc".ToString.ToUpper
                            '...do noghing
                            'MsgBox("Grid View Name : " & pGridView.Name & Environment.NewLine & "Column Name : " & CStr(pGridView.Columns.Item(nLoopColGridView).Name.ToString.ToUpper), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        Case Else
                            .Columns.Remove(.Columns.Item(nLoopColGridView))
                    End Select

                Next

            End With

        Catch ex As Exception
        End Try

        Return pGridView

    End Function

    Private Sub W_PRCxInitialGridBandView()
        Dim bRet As Boolean = _bInitialGridBandView

        'If (_bInitialGridBandView = True) Then
        '    Return
        'End If

        Dim oDBdtMatSize As System.Data.DataTable

        tSql = ""
        tSql = ";WITH cteImportSize(FTMatSize)"
        tSql &= Environment.NewLine & "AS (SELECT DISTINCT L1.FTSizeBreakdownCode"
        tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS L1"
        tSql &= Environment.NewLine & "    WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        tSql &= Environment.NewLine & "    )"
        tSql &= Environment.NewLine & "--SELECT B.FNHSysMatSizeId, A.FTMatSize, B.FTMatSizeNameEN, B.FTMatSizeNameEN"
        tSql &= Environment.NewLine & "SELECT B.FTMatSizeCode"
        tSql &= Environment.NewLine & "FROM cteImportSize AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS B WITH(NOLOCK) ON A.FTMatSize = B.FTMatSizeCode"
        tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC;"

        oDBdtMatSize = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

        Try
            Dim oGridBandedView As DevExpress.XtraGrid.Views.Grid.GridView = Me.ogdConfirmImport.Views(0)
            REM 2014/06/24
            'oGridBandedView.ClearGrouping()
            oGridBandedView.ClearDocument()

            'If Not (_bInitialGridBandView) Then
            '    oGridBandedView.ClearGrouping()
            '    oGridBandedView.ClearDocument()
            'End If

            REM oGridBandedView.Columns("FTMerTeamCode").SortIndex = 0
            REM oGridBandedView.Columns("FTMerTeamCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            REM oGridBandedView.Columns("FTPONo").SortIndex = 1
            REM oGridBandedView.Columns("FTPONo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            ' Make the group footers always visible.
            oGridBandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

            oGridBandedView.Columns("FTMerTeamCode").Group()
            oGridBandedView.Columns("FTMerTeamCode").SortIndex = 0
            oGridBandedView.Columns("FTMerTeamCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            REM oGridBandedView.Columns("FTMerTeamCode").Caption = "Merchandiser Team"
            oGridBandedView.Columns("FTPONo").Group()
            oGridBandedView.Columns("FTPONo").SortIndex = 1
            oGridBandedView.Columns("FTPONo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            REM oGridBandedView.Columns("FTPONo").Caption = "PO No."

            If Not oDBdtMatSize Is Nothing AndAlso oDBdtMatSize.Rows.Count > 0 Then
                Dim nLoopMatSize As Integer

                For nLoopMatSize = 0 To oDBdtMatSize.Rows.Count - 1
                    Dim oItemSummary As GridGroupSummaryItem = New GridGroupSummaryItem
                    oItemSummary.FieldName = "FNQuantity" & oDBdtMatSize.Rows(nLoopMatSize).Item("FTMatSizeCode").ToString()
                    oItemSummary.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    oItemSummary.DisplayFormat = "{0:n0}"
                    oItemSummary.ShowInGroupColumnFooter = oGridBandedView.Columns("FNQuantity" & oDBdtMatSize.Rows(nLoopMatSize).Item("FTMatSizeCode").ToString())
                    oGridBandedView.GroupSummary.Add(oItemSummary)

                Next nLoopMatSize

            End If

            oGridBandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
            oGridBandedView.ExpandAllGroups()
            oGridBandedView.OptionsView.ColumnAutoWidth = False
            oGridBandedView.RefreshData()

            _bInitialGridBandView = True

        Catch ex As Exception
            _bInitialGridBandView = False
        End Try

    End Sub

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
            Me.ogdImportOrder.DataSource = Nothing
            Me.ogdImportOrder.Refresh()
            Call W_PRCbRemoveGridViewColumn(Me.ogvImportOrder)

            'II
            'oGridViewConfirmImport = Me.ogdConfirmImport.Views(0)
            'Me.ogdConfirmImport.DataSource = Nothing
            'Call W_PRCbRemoveGridViewColumn(Me.ogvConfirmImport)

            'II
            Me.ogdConfirmImport.DataSource = Nothing
            Me.ogdConfirmImport.Refresh()
            Call W_PRCbRemoveGridViewColumn(Me.ogvConfirmImport)

            'Call W_PRCxInitialGridBandView()

            _bCatchSrcFile = False

            HI.TL.HandlerControl.ClearControl(Me)

            Me.FNHSysCustId.Focus()
            Me.FNHSysCustId.SelectionStart = 0
            Me.FNHSysCustId.SelectionLength = Len(Me.FNHSysCustId.Text)
            FNHSysUnitId.Text = "PCS"
        Catch ex As Exception
        End Try

        Return True

    End Function

    Private Shared Function W_PRCbValidateIsFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function

    ' This function checks to see if a file is open or not. If the file is
    ' already open, it returns True. If the file is not open, it returns
    ' False. Otherwise, a run-time error occurs because there is
    ' some other problem accessing the file.

    'Function IsFileOpen(filename As String)
    '    Dim filenum As Integer, errnum As Integer

    '    On Error Resume Next   ' Turn error checking off.
    '    filenum = FreeFile()   ' Get a free file number.
    '    ' Attempt to open the file and lock it.
    '    Open filename For Input Lock Read As #filenum
    '    Close #filenum          ' Close the file.
    '    errnum = CInt(Err.ToString)           ' Save the error number that occurred.
    '    On Error GoTo 0        ' Turn error checking back on.
    '    ' Check to see which error occurred.
    '    Select Case errnum
    '        ' No error occurred.
    '        ' File is NOT already open by another user.
    '        Case 0
    '            IsFileOpen = False
    '            ' Error number for "Permission Denied."
    '            ' File is already opened by another user.
    '        Case 70
    '            IsFileOpen = True
    '            ' Another error occurred.
    '        Case Else
    '            Error errnum
    '    End Select

    'End Function

    Private Function W_PRCbAddNewStyleImport() As Boolean
        Dim _bRet As Boolean = False
        Try
            _bRet = True
            If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                Dim nFNHSysStyleId As Integer
                Dim nFNHSysCustId As Integer
                Dim tStyleImport As String
                Dim tStyleImportDesc As String

                nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag)

                If nFNHSysCustId = 0 And Me.FNHSysCustId.Text.Trim() <> "" Then

                    tSql = "SELECT TOP 1 A.FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS A WITH(NOLOCK) WHERE A.FTCustCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysCustId.Text.Trim()) & "';"
                    nFNHSysCustId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

                End If

                Dim oStrBuilder As New System.Text.StringBuilder()

                Dim nLoopStyle As Integer

                For nLoopStyle = 0 To oDBdtStyle.Rows.Count - 1
                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    tStyleImport = ""
                    tStyleImport = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleCode")

                    tStyleImportDesc = ""
                    tStyleImportDesc = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleName")

                    tSql = "SELECT TOP 1 A.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A WITH(NOLOCK) WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'"

                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                        nFNHSysStyleId = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]([FTInsUser]")
                        oStrBuilder.AppendLine("											  ,[FDInsDate]")
                        oStrBuilder.AppendLine("											  ,[FTInsTime]")
                        oStrBuilder.AppendLine("											  ,[FTUpdUser]")
                        oStrBuilder.AppendLine("											  ,[FDUpdDate]")
                        oStrBuilder.AppendLine("											  ,[FTUpdTime]")
                        oStrBuilder.AppendLine("											  ,[FNHSysStyleId]")
                        oStrBuilder.AppendLine("											  ,[FTStyleCode]")
                        oStrBuilder.AppendLine("											  ,[FTStyleNameTH]")
                        oStrBuilder.AppendLine("											  ,[FTStyleNameEN]")
                        oStrBuilder.AppendLine("											  ,[FTRemark]")
                        oStrBuilder.AppendLine("											  ,[FTStateActive],[FNCMDisPer]")
                        oStrBuilder.AppendLine("											  ,[FNHSysCustId])")
                        oStrBuilder.AppendLine("VALUES(NULL")
                        oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(10),GETDATE(),111)")
                        oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(8),GETDATE(),114)")
                        oStrBuilder.AppendLine("      ,NULL")
                        oStrBuilder.AppendLine("      ,NULL")
                        oStrBuilder.AppendLine("      ,NULL")
                        oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysStyleId))
                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'")
                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImportDesc) & "'")
                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImportDesc) & "'")
                        oStrBuilder.AppendLine("      ,''")
                        oStrBuilder.AppendLine("      ,'1',5.0")
                        REM 2014/05/24 oStrBuilder.AppendLine("      ,COALESCE((SELECT TOP 1 A.FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS A WITH(NOLOCK) WHERE A.FTCustCode = N'" & HI.UL.ULF.rpQuoted(tCustCodeStyle) & "'), NULL))")
                        oStrBuilder.AppendLine(String.Format("       ,{0})", nFNHSysCustId))

                        tSql = ""
                        tSql = oStrBuilder.ToString()

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                            'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                    End If

                Next nLoopStyle

            Else
                '...do nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                Throw New Exception(ex.Message().ToString() & ex.StackTrace.ToString())
            End If
            _bRet = False
        End Try

        Return _bRet

    End Function

    Private Function W_PRCbValidateMatchPlant(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Clone()

                Dim nLoop As Integer
                Dim nStartRow As Integer = _nRowHeader

                Try
                    'For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                    '    'Application.DoEvents()
                    '    oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    'Next nLoop
                    For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                        'Application.DoEvents()
                        oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    Next nLoop

                    oDBdtSrc.AcceptChanges()

                Catch ex As Exception
                    'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End Try

                Dim tColPlant As String = "F31"

                Dim tPlant As List(Of String) = oDBdtSrc.AsEnumerable() _
                                     .Select(Function(r) r.Field(Of String)(tColPlant)) _
                                     .Distinct() _
                                     .ToList()

                If Not tPlant Is Nothing And tPlant.Count > 0 Then
                    Dim oDBdtPlant As System.Data.DataTable

                    tSql = ""
                    tSql = "SELECT A.FTPlantCode"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPlant AS A WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "ORDER BY A.FNHSysPlantId ASC;"

                    oDBdtPlant = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                    If Not oDBdtPlant Is Nothing And oDBdtPlant.Rows.Count > 0 Then
                        Dim bFoundPlant As Boolean = True

                        For Each s As String In tPlant
                            If Not s Is Nothing Then
                                If s.Trim() <> "" Then
                                    Dim oDataRow As DataRow()
                                    Try
                                        oDataRow = oDBdtPlant.Select("FTPlantCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                        If oDataRow.Length > 0 Then
                                            '...valid team buy group
                                            If oDataRow(0)("FTPlantCode") <> "" Then
                                                'bFoundBuyGrp = True
                                            Else
                                                '...do nothing
                                            End If
                                        Else

                                            Select Case HI.ST.Lang.Language

                                                Case HI.ST.Lang.eLang.TH
                                                    MsgBox("ไม่พบรายการข้อมูลโรงงานลูกค้าในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสโรงงานลูกค้า : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                                Case Else
                                                    MsgBox("Plant code not exists in system master plant " & Environment.NewLine & "Plant code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            End Select

                                            bFoundPlant = False

                                            Exit For

                                        End If

                                    Catch ex As Exception
                                    End Try

                                Else
                                    MsgBox("Source file row item not yet specify plant code (plant code is ) :" & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    bFoundPlant = False
                                    Exit For
                                End If

                            Else

                                Select Case HI.ST.Lang.Language

                                    Case HI.ST.Lang.eLang.TH
                                        MsgBox("ไม่พบรายการข้อมูลโรงงานลูกค้า !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                    Case Else
                                        MsgBox("Invalid data plant !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End Select

                                bFoundPlant = False

                                Exit For

                            End If

                        Next

                        If bFoundPlant = True Then
                            bRet = True
                        End If

                    End If

                End If

            Else
                '...do nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateMatchCurrency(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = True

        If _tCurtImport <= 0 Then
            Return True
        End If

        Dim _Qry As String = ""
        Dim _CheckCur As String = ""
        Dim _dtCur As New DataTable
        _dtCur.Columns.Add("FTCurrency", GetType(String))
        Try
            For Each R As DataRow In oDBdt.Select("F" & _tCurtImport.ToString & "<>''")

                If _dtCur.Select("FTCurrency='" & HI.UL.ULF.rpQuoted(R.Item("F" & _tCurtImport.ToString).ToString) & "'").Length <= 0 Then
                    _dtCur.BeginInit()
                    _dtCur.Rows.Add(R.Item("F" & _tCurtImport.ToString).ToString)
                    _dtCur.EndInit()
                End If
            Next

            If _dtCur.Rows.Count > 0 Then
                For Each R As DataRow In _dtCur.Rows
                    _Qry = "SELECT TOP 1  FTCurCode"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(R!FTCurrency.ToString) & "'"

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                        If _CheckCur = "" Then
                            _CheckCur = R!FTCurrency.ToString
                        Else
                            _CheckCur = _CheckCur & "," & R!FTCurrency.ToString
                        End If
                    End If

                Next
            End If

            If _CheckCur <> "" Then
                HI.MG.ShowMsg.mInfo("ข้อมูลสกุลเงิน ไม่ถูกต้อง กรุณาทำการตรวจสอบ !!!", 1504020085, Me.Text, _CheckCur, MessageBoxIcon.Warning)
                bRet = False
            End If
        Catch ex As Exception

        End Try
        Return bRet

    End Function

    Private Function W_PRCbValidateMatchBuyGroup(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim oDBdtSrc As New System.Data.DataTable

                oDBdtSrc = oDBdt.Clone
                Dim nLoop As Integer
                Dim nStartRow As Integer = _nRowHeader

                Try
                    REM 2014/06/30
                    'For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                    '    Application.DoEvents()
                    '    oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    'Next nLoop

                    For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                        Application.DoEvents()
                        oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    Next nLoop

                    oDBdtSrc.AcceptChanges()

                Catch ex As Exception
                    'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End Try


                Dim tColBuyGrp As String = "F43"

                Dim tTexValidateBuyGrp As String


                tTexValidateBuyGrp = ""

                For Each oDataRowColBuyGrp As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColBuyGrp As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColBuyGrp.ColumnName.ToString.ToUpper
                            Case tColBuyGrp.ToUpper
                                tTexValidateBuyGrp = tTexValidateBuyGrp & oDataRowColBuyGrp.Item(oColBuyGrp.ColumnName.ToString)
                            Case Else
                                '...Nothing 
                        End Select
                    Next

                Next

                If tTexValidateBuyGrp <> "" Then
                    Dim tBuyGrp As List(Of String) = oDBdtSrc.AsEnumerable() _
                                     .Select(Function(r) r.Field(Of String)(tColBuyGrp)) _
                                     .Distinct() _
                                     .ToList()

                    If Not tBuyGrp Is Nothing And tBuyGrp.Count > 0 Then
                        Dim oDBdtBuyGrp As System.Data.DataTable

                        tSql = ""
                        tSql = "SELECT A.FTBuyGrpCode"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS A WITH(NOLOCK)"
                        tSql &= Environment.NewLine & "ORDER BY A.FNHSysBuyGrpId ASC;"

                        oDBdtBuyGrp = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                        If Not oDBdtBuyGrp Is Nothing And oDBdtBuyGrp.Rows.Count > 0 Then
                            Dim bFoundBuyGrp As Boolean = True

                            For Each s As String In tBuyGrp

                                If Not s Is Nothing Then

                                    If s.Trim() <> "" Then

                                        Dim oDataRow As DataRow()
                                        Try
                                            oDataRow = oDBdtBuyGrp.Select("FTBuyGrpCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                            If oDataRow.Length > 0 Then
                                                '...valid team buy group
                                                If oDataRow(0)("FTBuyGrpCode") <> "" Then
                                                    'bFoundBuyGrp = True
                                                Else
                                                    '...do nothing
                                                End If
                                            Else

                                                Select Case HI.ST.Lang.Language

                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบรายการข้อมูลกลุ่มการซื้อในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสกลุ่มการซื้อ : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Buy group code not exists in system master buy group " & Environment.NewLine & "Buy group code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                End Select

                                                bFoundBuyGrp = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else
                                        MsgBox("Source file row item not yet specify buy group code (buy group code is ) :" & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        bFoundBuyGrp = False
                                        Exit For
                                    End If

                                Else

                                    Select Case HI.ST.Lang.Language

                                        Case HI.ST.Lang.eLang.TH
                                            MsgBox("ไม่พบรายการข้อมูลกลุ่มการซื้อ !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)


                                        Case Else
                                            MsgBox("Invalid data buy group !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End Select

                                    bFoundBuyGrp = False

                                    Exit For

                                End If

                            Next

                            If bFoundBuyGrp = True Then
                                bRet = True
                            End If

                        End If

                    Else
                        MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูล Buy Group" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูล Buy Group !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    End If

                Else
                    MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูล Buy Group" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูลรายการ Buy Group !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            Else
                '...do nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateMatchMerchanTeam(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then

                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Clone()

                Dim nLoop As Integer
                Dim nStartRow As Integer = _nRowHeader

                Try
                    REM 2014/06/30
                    'For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                    '    'Application.DoEvents()
                    '    oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    'Next nLoop

                    For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                        'Application.DoEvents()
                        oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    Next nLoop

                    oDBdtSrc.AcceptChanges()

                Catch ex As Exception
                    'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End Try

                Dim tColMerchanTeam As String = "F14"

                'Dim tmpDTColMerchanTeam As New System.Data.DataTable

                'Dim oColMerChanTeamValidate As System.Data.DataColumn
                'oColMerChanTeamValidate = New System.Data.DataColumn(tColMerchanTeam.ToString, System.Type.GetType("System.String"))
                'oColMerChanTeamValidate.Caption = tColMerchanTeam.ToString

                'tmpDTColMerchanTeam.Columns.Add(oColMerChanTeamValidate.ColumnName, oColMerChanTeamValidate.DataType)

                Dim tTexValidateMerchandiserTeam As String


                tTexValidateMerchandiserTeam = ""

                For Each oDataRowColMerchanTeam As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColMerchanTeamXX As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColMerchanTeamXX.ColumnName.ToString.ToUpper
                            Case tColMerchanTeam.ToUpper
                                'Dim oNewRowMerchanTeam As System.Data.DataRow

                                'oNewRowMerchanTeam = tmpDTColMerchanTeam.NewRow()

                                'oNewRowMerchanTeam.Item(oColMerChanTeamValidate.ColumnName.ToString) = oDataRowColMerchanTeam!tColMerchanTeam.ToString

                                'tmpDTColMerchanTeam.Rows.Add(oNewRowMerchanTeam)

                                'tTexValidateMerchandiserTeam = tTexValidateMerchandiserTeam & oDataRowColMerchanTeam!oColMerchanTeamXX.ColumnName.ToString

                                tTexValidateMerchandiserTeam = tTexValidateMerchandiserTeam & oDataRowColMerchanTeam.Item(oColMerchanTeamXX.ColumnName.ToString)

                            Case Else
                                '...Nothing 
                        End Select
                    Next

                Next

                '...validate column merchandiser team
                If tTexValidateMerchandiserTeam <> "" Then
                    Dim tMerChanTeam As List(Of String) = oDBdtSrc.AsEnumerable() _
                                     .Select(Function(r) r.Field(Of String)(tColMerchanTeam)) _
                                     .Distinct() _
                                     .ToList()

                    If Not tMerChanTeam Is Nothing And tMerChanTeam.Count > 0 Then
                        '...ถ้ามีบางรายการ ของแถวข้อมูล สำหรับ คอลัมน์ Merchandiser Team เป็นค่าว่าง หรือ blank
                        Dim oDBdtMerchanTeam As System.Data.DataTable

                        tSql = ""
                        tSql = "SELECT A.FTMerTeamCode"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK)"
                        tSql &= Environment.NewLine & "ORDER BY A.FNHSysMerTeamId ASC;"

                        oDBdtMerchanTeam = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                        If Not oDBdtMerchanTeam Is Nothing And oDBdtMerchanTeam.Rows.Count > 0 Then
                            Dim bFoundMerTeam As Boolean = True

                            For Each s As String In tMerChanTeam

                                If Not s Is Nothing Then

                                    If s.Trim() <> "" Then

                                        Dim oDataRow As DataRow()

                                        Try
                                            oDataRow = oDBdtMerchanTeam.Select("FTMerTeamCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                            If oDataRow.Length > 0 Then
                                                '...valid team merchandiser
                                                If oDataRow(0)("FTMerTeamCode") <> "" Then
                                                    'MsgBox("Merchan Team : " & s.ToString())
                                                    'bFoundMerTeam = True
                                                Else
                                                    '...do nothing
                                                End If

                                            Else
                                                REM MsgBox("Merchandiser Team code not exists in system master merchandiser team" & Environment.NewLine & "Merchandiser Team code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                                'Select Case HI.ST.Lang.Language

                                                '    Case HI.ST.Lang.eLang.TH
                                                '        MsgBox("ไม่พบข้อมูลทีมเมอร์แชนไดเซอร์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสทีม : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                '    Case Else
                                                '        MsgBox("Merchandiser Team code not exists in system master merchandiser team" & Environment.NewLine & "Merchandiser Team code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                'End Select

                                                Select Case HI.ST.Lang.Language
                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบข้อมูลทีมเมอร์แชนไดเซอร์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสทีม : " & s.ToString() & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                        'MsgBox("การกำหนดข้อมูลทีมเมอร์แชนไดเซอร์บางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!" & Environment.NewLine & "Merchandiser Team : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                End Select

                                                bFoundMerTeam = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else
                                        REM MsgBox("Source file row item not yet specify merchandiser team code (merchandiser team code is ) :" & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                        Select Case HI.ST.Lang.Language

                                            Case HI.ST.Lang.eLang.TH
                                                MsgBox("ไฟล์นำเข้ารายการใบสั่งผลิตอัตโนมัติยังไม่มีการกำหนดรายการข้อมูลทีมเมอร์แชนไดเซอร์ หรือ กำหนดข้อมูลไม่ครบถ้วนกรุณาตรวจสอบอีกครั้ง !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            Case Else
                                                MsgBox("Source file row item not complete or not yet specify merchandiser team code !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End Select

                                        bFoundMerTeam = False

                                        Exit For

                                    End If

                                Else
                                    REM MsgBox("Invalid data merchandiser team code !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                    Select Case HI.ST.Lang.Language

                                        Case HI.ST.Lang.eLang.TH
                                            MsgBox("ไม่พบรายการข้อมูลทีมเมอร์แชนไดเซอร์ !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        Case Else
                                            MsgBox("Invalid data merchandiser team code !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End Select

                                    bFoundMerTeam = False

                                    Exit For

                                End If

                            Next

                            If bFoundMerTeam = True Then
                                bRet = True
                            End If

                        End If

                    Else
                        MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลทีมเมอร์แชนไดเซอร์" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูลรายการทีมเมอร์แชนไดเซอร์ !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    End If

                Else
                    MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลทีมเมอร์แชนไดเซอร์" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูลรายการทีมเมอร์แชนไดเซอร์ !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            Else
                '...do nothing

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRet

    End Function

    Private Sub W_PRCxValidateMaster(ByVal oDBdt As System.Data.DataTable)
        Call W_PRCbValidateExistsMasterSeason(oDBdt)
        Call W_PRCbValidateExistsMasterStyle(oDBdt)
        Call W_PRCbValidateExistsMatchColor(oDBdt)
        Call W_PRCbValidateExistsMasterGender(oDBdt)
        Call W_PRCbValidateExistsMasterBuyer(oDBdt)
        Call W_PRCbValidateExistsMasterShipMode(oDBdt)
        Call W_PRCbValidateExistsMasterMerchanUnit(oDBdt)
        Call W_PRCbValidateExistsMasterCountry(oDBdt)   '...validate country/continent

        '========================================= Validate Map Size ============================================
        Dim tmpDTNotMapSize As New System.Data.DataTable

        Dim oColFTSizeCodeNotExists As System.Data.DataColumn
        oColFTSizeCodeNotExists = New System.Data.DataColumn("FTSizeCodeNotExists", System.Type.GetType("System.String"))
        oColFTSizeCodeNotExists.Caption = "FTSizeCodeNotExists"

        tmpDTNotMapSize.Columns.Add(oColFTSizeCodeNotExists.ColumnName, oColFTSizeCodeNotExists.DataType)

        Dim oColMapSizeExtend As System.Data.DataColumn
        oColMapSizeExtend = New System.Data.DataColumn("FTMapSizeExtend", System.Type.GetType("System.String"))
        oColMapSizeExtend.Caption = "FTMapSizeExtend"

        tmpDTNotMapSize.Columns.Add(oColMapSizeExtend.ColumnName, oColMapSizeExtend.DataType)

        Call W_PRCbValidateExistsMatchSize(oDBdt, tmpDTNotMapSize)

        If Not tmpDTNotMapSize Is Nothing AndAlso tmpDTNotMapSize.Rows.Count > 0 Then
            _oSplash.TopMost = False
            _oSplash.Refresh()

            _wMapSizeImportOrder = New wMapSizeImportOrder(tmpDTNotMapSize)

            HI.TL.HandlerControl.AddHandlerObj(_wMapSizeImportOrder)

            Dim oSysLang As New HI.ST.SysLanguage

            Try
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wMapSizeImportOrder.Name.ToString.Trim, _wMapSizeImportOrder)

                Call HI.ST.Lang.SP_SETxLanguage(_wMapSizeImportOrder)

                _wMapSizeImportOrder.TopMost = True
                _wMapSizeImportOrder.Refresh()

                _oDBdtUserImportMapSizeManual = Nothing

                With _wMapSizeImportOrder
                    .DTUserImportMapSize = Nothing
                    If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        _oDBdtUserImportMapSizeManual = .DTUserImportMapSize.Copy()
                    Else

                        If Not DBNull.Value.Equals(tmpDTNotMapSize) And tmpDTNotMapSize.Rows.Count > 0 Then
                            For Each oDataRowNotMapSize As System.Data.DataRow In tmpDTNotMapSize.Rows
                                '...add new FNHSysMatSizeId:FTMatSizeCode in System TMERMMatSize [HITECH_MASTER]
                                Dim nFNHSysMatSizeId As Integer

                                nFNHSysMatSizeId = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())

                                tSql = ""
                                tSql = "DECLARE @FNMatSizeSeqMax AS INT;"
                                tSql &= Environment.NewLine & "SELECT @FNMatSizeSeqMax = MAX(A.FNMatSizeSeq)"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK)"
                                tSql &= Environment.NewLine & "GROUP BY A.FNHSysMatSizeId;"
                                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] ([FTInsUser],[FDInsDate],[FTInsTime]"
                                tSql &= Environment.NewLine & "                                                                                                    ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                tSql &= Environment.NewLine & "                                                                                                    ,[FNHSysMatSizeId],[FTMatSizeCode],[FNMatSizeSeq]"
                                tSql &= Environment.NewLine & "							                                                                           ,[FTMatSizeNameTH],[FTMatSizeNameEN],[FTRemark],[FTStateActive])"
                                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                tSql &= ",NULL, NULL, NULL"
                                tSql &= ", " & nFNHSysMatSizeId & ", N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', (ISNULL(@FNMatSizeSeqMax, 0) + 1)"
                                tSql &= ", N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', '', '1');"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    If System.Diagnostics.Debugger.IsAttached = True Then
                                        System.Diagnostics.Debug.Write("Append new size code to system data base TMERMMatSize [HITECH_MASTER] : " & oDataRowNotMapSize!FTSizeCodeNotExists.ToString & " " & Environment.NewLine)
                                    End If

                                End If

                            Next

                        End If

                    End If

                End With

                _wMapSizeImportOrder.TopMost = False
                _wMapSizeImportOrder.Refresh()

            Catch ex As Exception
                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                End If
            End Try

            _oSplash.TopMost = True
            _oSplash.Refresh()

        End If

        If Not tmpDTNotMapSize Is Nothing Then tmpDTNotMapSize.Dispose()
        '========================================================================================================

        Call W_PRCbValidateExistsMasterProductType(oDBdt)

    End Sub

    Private Function W_PRCbValidateExistsMasterProductType(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColCategoryCode As String = "F10"
                Dim tColCategoryExtend As String = "F11"
                Dim tColSubCategoryExtend As String = "F12"

                Dim oStrBuilder As New System.Text.StringBuilder()

                Dim nLoopCategory As Integer

                For nLoopCategory = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลประเภทผลิตภัณฑ์...")

                    Dim tCategoryCode As String = ""
                    Dim tCategoryExtend As String = ""
                    Dim tSubCategoryExtend As String = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopCategory).Item(tColCategoryCode)) Then
                        tCategoryCode = oDBdt.Rows(nLoopCategory).Item(tColCategoryCode)

                        If tCategoryCode <> "" AndAlso Not DBNull.Value.Equals(oDBdt.Rows(nLoopCategory).Item(tColCategoryExtend)) Then
                            tCategoryExtend = oDBdt.Rows(nLoopCategory).Item(tColCategoryExtend)

                            If tCategoryExtend <> "" AndAlso Not DBNull.Value.Equals(oDBdt.Rows(nLoopCategory).Item(tColSubCategoryExtend)) Then
                                tSubCategoryExtend = oDBdt.Rows(nLoopCategory).Item(tColSubCategoryExtend)

                                If tSubCategoryExtend <> "" Then
                                    '...validate product type exists in system merchan (Category)

                                    Dim tProductTypeCode As String
                                    tProductTypeCode = tCategoryCode.Trim() & Mid$(tCategoryExtend.Trim(), 1, 1) & Mid$(tSubCategoryExtend.Trim(), 1, 1)

                                    Dim tProductTypeDesc As String
                                    tProductTypeDesc = tCategoryExtend.Trim()

                                    Dim tProductTypeRemark As String
                                    tProductTypeRemark = tCategoryExtend.Trim() & "-" & tSubCategoryExtend.Trim()

                                    Dim nFNHSysProdTypeId As Integer
                                    nFNHSysProdTypeId = Val(HI.TL.RunID.GetRunNoID("TMERMProductType", "FNHSysProdTypeId", Conn.DB.DataBaseName.DB_MASTER).ToString())
                                    '======================================================================================================================================
                                    'BB + BASKETBALL                + SPORT                       + 01        ==> (BB + B) + S + 01 ==> BBBS01:BASKETBALL-SPORT
                                    'BB + BASKETBALL                + SPORT TEAM                  + 02        ==> (BB + B) + S + 02 ==> BBBS01:BASKETBALL-SPORT TEAM
                                    'BB + BASKETBALL                + PERFORMANCE                 + 03        ==> (BB + B) + P + 01 ==> BBBS01:BASKETBALL-PERFORMANCE
                                    '======================================================================================================================================
                                    'BB + TRAINING                  + FOUNDATIONAL TRAINING       + 01        ==> BB + T + S + 01 ==> BBTS01:TRAINING-FOUNDATIONAL TRAINING
                                    'BB + JORDAN PERF BBALL         + JORDAN CORE                 + 01        ==> BB + J + J + 01 ==> BBJJ01:JORDAN PERF BBALL-JORDAN CORE
                                    'FB + FOOTBALL/SOCCER TEAMSPORT + TEAMSPORT                   + 01        ==> FB + F + T + 01 ==> FOOTBALL/SOCCER TEAMSPORT-TEAMSPORT

                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    oStrBuilder.AppendLine("SELECT TOP 1 A.FTProdTypeCode")
                                    oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMProductType AS A WITH(NOLOCK)")
                                    oStrBuilder.AppendLine("WHERE (A.FTProdTypeNameTH = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "' OR A.FTProdTypeNameEN = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "');")

                                    If oStrBuilder.Length > 0 Then
                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                            oStrBuilder.Remove(0, oStrBuilder.Length)

                                            oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType]")
                                            oStrBuilder.AppendLine("           ([FTInsUser]")
                                            oStrBuilder.AppendLine("           ,[FDInsDate]")
                                            oStrBuilder.AppendLine("           ,[FTInsTime]")
                                            oStrBuilder.AppendLine("           ,[FTUpdUser]")
                                            oStrBuilder.AppendLine("           ,[FDUpdDate]")
                                            oStrBuilder.AppendLine("           ,[FTUpdTime]")
                                            oStrBuilder.AppendLine("           ,[FNHSysProdTypeId]")
                                            oStrBuilder.AppendLine("           ,[FTProdTypeCode]")
                                            oStrBuilder.AppendLine("           ,[FTProdTypeNameTH]")
                                            oStrBuilder.AppendLine("           ,[FTProdTypeNameEN]")
                                            oStrBuilder.AppendLine("           ,[FTRemark]")
                                            oStrBuilder.AppendLine("           ,[FTStateActive])")
                                            oStrBuilder.AppendLine(" VALUES")
                                            oStrBuilder.AppendLine(String.Format("       ({0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", nFNHSysProdTypeId))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeCode) & "'"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "'"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "'"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeRemark) & "'"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0})", "N'1'"))

                                            If oStrBuilder.Length > 0 Then
                                                tSql = ""
                                                tSql = oStrBuilder.ToString()

                                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                                    'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                Else
                                                    'MsgBox("Execute data not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                End If

                                            End If

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopCategory

                bRet = True

            End If

        Catch ex As Exception
            ' If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_GETtMerTeamCode() As String
        Dim tMerTeamCode As String = ""
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTMerTeamCodeMax AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FTMerTeamCodeRet AS NVARCHAR(30);")
            oStrBuilder.AppendLine("SELECT @FTMerTeamCodeMax = MAX(A.FTMerTeamCode)")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK)")
            oStrBuilder.AppendLine("WHERE SUBSTRING(A.FTMerTeamCode, 1, 1) = N'M'")
            oStrBuilder.AppendLine("      AND ISNUMERIC(SUBSTRING(A.FTMerTeamCode, 2, LEN(A.FTMerTeamCode)-1)) = 1")
            oStrBuilder.AppendLine("GROUP BY A.FTMerTeamCode;")
            oStrBuilder.AppendLine("IF (@@ROWCOUNT > 0)")
            oStrBuilder.AppendLine("	BEGIN")
            oStrBuilder.AppendLine("		PRINT 'FTMerTeamCode Max is : ' + @FTMerTeamCodeMax;")
            oStrBuilder.AppendLine("		DECLARE @nCntMerTeam AS INT;")
            oStrBuilder.AppendLine("		SELECT @nCntMerTeam = COUNT(L1.FTMerTeamCode)")
            oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK)")
            oStrBuilder.AppendLine("		WHERE SUBSTRING(L1.FTMerTeamCode, 1, 1) = N'M'")
            oStrBuilder.AppendLine("		      AND ISNUMERIC(SUBSTRING(L1.FTMerTeamCode, 2, LEN(L1.FTMerTeamCode)-1)) = 1")
            oStrBuilder.AppendLine("		IF (@@ROWCOUNT > 0)")
            oStrBuilder.AppendLine("			BEGIN")
            oStrBuilder.AppendLine("			  PRINT '@nCntMerTeam : ' + CONVERT(VARCHAR(10), (@nCntMerTeam + 1));")
            oStrBuilder.AppendLine("			  PRINT 'FTMerTeamCodeRet : ' + N'M' + CONVERT(VARCHAR(10), (@nCntMerTeam + 1));")
            oStrBuilder.AppendLine("			  SET @FTMerTeamCodeRet =  N'M' + CONVERT(NVARCHAR(29), (@nCntMerTeam + 1))")
            oStrBuilder.AppendLine("			END")
            oStrBuilder.AppendLine("		ELSE")
            oStrBuilder.AppendLine("		    BEGIN")
            oStrBuilder.AppendLine("			  PRINT 'FTMerTeamCodeRet : ' + N'M1';")
            oStrBuilder.AppendLine("			  SET @FTMerTeamCodeRet = N'M1'")
            oStrBuilder.AppendLine("		    END")
            oStrBuilder.AppendLine("	END")
            oStrBuilder.AppendLine("ELSE")
            oStrBuilder.AppendLine("	BEGIN")
            oStrBuilder.AppendLine("		SET @FTMerTeamCodeRet = N'M1'")
            oStrBuilder.AppendLine("		PRINT 'FTMerTeamCodeRet : ' + @FTMerTeamCodeRet")
            oStrBuilder.AppendLine("	END;")
            oStrBuilder.AppendLine("SELECT @FTMerTeamCodeRet AS FTMerTeamCode;	")

            tSql = ""
            tSql = oStrBuilder.ToString()

            'tMerTeamCode = HI.Conn.SQLConn.GetField(tSql, "").ToString()

            Dim oDBdt As System.Data.DataTable

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then
                If Not DBNull.Value.Equals(oDBdt.Rows(0).Item(0)) Then
                    If oDBdt.Rows(0).Item(0).ToString() <> "" Then
                        tMerTeamCode = oDBdt.Rows(0).Item(0).ToString()
                    End If

                End If

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return tMerTeamCode

    End Function

    Private Function W_PRCbLoadMapSize() As System.Data.DataTable
        Dim tmpDTMapSizeMaster As System.Data.DataTable
        Try
            tSql = ""
            tSql = "SELECT A.FTMapSize, A.FTMapSizeExtension"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERMMapSize] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "ORDER BY A.FTMapSize ASC;"

            tmpDTMapSizeMaster = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            tmpDTMapSizeMaster = Nothing
        End Try

        oDBdtMapSize = tmpDTMapSizeMaster.Copy()

        tmpDTMapSizeMaster.Dispose()

        Return oDBdtMapSize

    End Function

    Private Function W_PRCbValidateExistsMasterSeason(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColPlanningSeason As String = "F25"
                Dim tColYear As String = "F26"
                Dim nLoopSeason As Integer
                'For nLoopSeason = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopSeason = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopSeason).Item(tColPlanningSeason)) Then
                        If oDBdt.Rows(nLoopSeason).Item(tColPlanningSeason).ToString() <> "" Then
                            Dim tSeason As String, tYear As String
                            tSeason = ""
                            tYear = ""

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopSeason).Item(tColYear)) Then
                                If oDBdt.Rows(nLoopSeason).Item(tColYear).ToString() <> "" Then
                                    tYear = Microsoft.VisualBasic.Mid$(oDBdt.Rows(nLoopSeason).Item(tColYear).ToString(), 3, 4)
                                End If
                            End If

                            tSeason = oDBdt.Rows(nLoopSeason).Item(tColPlanningSeason).ToString() & tYear

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTSeasonCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS A WITH(NOLOCK) WHERE A.FTSeasonCode = N'" & HI.UL.ULF.rpQuoted(tSeason) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysSeasonId As Integer

                                nFNHSysSeasonId = Val(HI.TL.RunID.GetRunNoID("TMERMSeason", "FNHSysSeasonId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())

                                Dim oStrBuilder As New System.Text.StringBuilder()

                                oStrBuilder.Remove(0, oStrBuilder.Length)

                                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason]([FTInsUser]")
                                oStrBuilder.AppendLine("											   ,[FDInsDate]")
                                oStrBuilder.AppendLine("											   ,[FTInsTime]")
                                oStrBuilder.AppendLine("											   ,[FTUpdUser]")
                                oStrBuilder.AppendLine("											   ,[FDUpdDate]")
                                oStrBuilder.AppendLine("											   ,[FTUpdTime]")
                                oStrBuilder.AppendLine("											   ,[FNHSysSeasonId]")
                                oStrBuilder.AppendLine("											   ,[FTSeasonCode]")
                                oStrBuilder.AppendLine("											   ,[FTSeasonNameTH]")
                                oStrBuilder.AppendLine("											   ,[FTSeasonNameEN]")
                                oStrBuilder.AppendLine("											   ,[FTRemark]")
                                oStrBuilder.AppendLine("											   ,[FTStateActive])")
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
                                    'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopSeason

                bRet = True
            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterBuyGroup(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColBuyGrp As String = "F43"
                Dim tColBuyGrpDesc As String = "F44"

                Dim nLoopBuyGrp As Integer

                For nLoopBuyGrp = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrp)) Then
                        If oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrp).ToString() <> "" Then
                            Dim tBuyGrpCode As String
                            tBuyGrpCode = ""

                            tBuyGrpCode = oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrp).ToString()

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc)) Then
                                If oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc).ToString() <> "" Then
                                    Dim tBuyGrpDesc As String
                                    tBuyGrpDesc = ""

                                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc)) Then
                                        tBuyGrpDesc = oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc).ToString().Trim()
                                    End If

                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS A WITH(NOLOCK) WHERE A.FTBuyGrpCode = N'" & HI.UL.ULF.rpQuoted(tBuyGrpCode) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                        Dim nFNHSysBuyGrpId As Integer

                                        nFNHSysBuyGrpId = Val(HI.TL.RunID.GetRunNoID("TMERMBuyGrp", "FNHSysBuyGrpId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                        Dim oStrBuilder As New System.Text.StringBuilder()

                                        oStrBuilder.Remove(0, oStrBuilder.Length)

                                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp]([FTInsUser]")
                                        oStrBuilder.AppendLine("											   ,[FDInsDate]")
                                        oStrBuilder.AppendLine("											   ,[FTInsTime]")
                                        oStrBuilder.AppendLine("											   ,[FTUpdUser]")
                                        oStrBuilder.AppendLine("											   ,[FDUpdDate]")
                                        oStrBuilder.AppendLine("											   ,[FTUpdTime]")
                                        oStrBuilder.AppendLine("											   ,[FNHSysBuyGrpId]")
                                        oStrBuilder.AppendLine("											   ,[FTBuyGrpCode]")
                                        oStrBuilder.AppendLine("											   ,[FTBuyGrpNameTH]")
                                        oStrBuilder.AppendLine("											   ,[FTBuyGrpNameEN]")
                                        oStrBuilder.AppendLine("											   ,[FNQtySpecialType]")
                                        oStrBuilder.AppendLine("											   ,[FNQtySpecial]")
                                        oStrBuilder.AppendLine("											   ,[FTRemark]")
                                        oStrBuilder.AppendLine("											   ,[FTStateActive])")
                                        oStrBuilder.AppendLine("VALUES")
                                        oStrBuilder.AppendLine("   (NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", nFNHSysBuyGrpId))
                                        oStrBuilder.AppendLine("   ,N'" & HI.UL.ULF.rpQuoted(tBuyGrpCode) & "'")
                                        oStrBuilder.AppendLine("   ,N'" & HI.UL.ULF.rpQuoted(tBuyGrpDesc) & "'")
                                        oStrBuilder.AppendLine("   ,N'" & HI.UL.ULF.rpQuoted(tBuyGrpDesc) & "'")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,''")
                                        oStrBuilder.AppendLine("   ,'1')")
                                        oStrBuilder.AppendLine("")

                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                                            'MsgBox("Execute data complelte...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopBuyGrp

                bRet = True
            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterPlant(ByVal oDBdt As System.Data.DataTable) As Boolean
        '...default FNQtySpecialType : 2 {%}
        '...default FNQtySpecial : 1
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColPlant As String = "F31"
                Dim tColPlantDesc As String = "F32"

                Dim nLoopPlant As Integer

                For nLoopPlant = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopPlant).Item(tColPlant)) Then
                        If oDBdt.Rows(nLoopPlant).Item(tColPlant).ToString() <> "" Then
                            Dim tPlantCode As String = ""

                            tPlantCode = oDBdt.Rows(nLoopPlant).Item(tColPlant).ToString()

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopPlant).Item(tColPlantDesc)) Then
                                If oDBdt.Rows(nLoopPlant).Item(tColPlantDesc).ToString() <> "" Then
                                    Dim tPlantDesc As String = ""

                                    tPlantDesc = oDBdt.Rows(nLoopPlant).Item(tColPlantDesc).ToString()

                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTPlantCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(tPlantCode) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                        Dim nFNHSysPlantId As Integer

                                        nFNHSysPlantId = Val(HI.TL.RunID.GetRunNoID("TMERMPlant", "FNHSysPlantId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                        Dim oStrBuilder As New System.Text.StringBuilder()

                                        oStrBuilder.Remove(0, oStrBuilder.Length)

                                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant]([FTInsUser]")
                                        oStrBuilder.AppendLine("											  ,[FDInsDate]")
                                        oStrBuilder.AppendLine("											  ,[FTInsTime]")
                                        oStrBuilder.AppendLine("											  ,[FTUpdUser]")
                                        oStrBuilder.AppendLine("											  ,[FDUpdDate]")
                                        oStrBuilder.AppendLine("											  ,[FTUpdTime]")
                                        oStrBuilder.AppendLine("											  ,[FNHSysPlantId]")
                                        oStrBuilder.AppendLine("											  ,[FTPlantCode]")
                                        oStrBuilder.AppendLine("											  ,[FTPlantNameTH]")
                                        oStrBuilder.AppendLine("											  ,[FTPlantNameEN]")
                                        oStrBuilder.AppendLine("											  ,[FNQtySpecialType]")
                                        oStrBuilder.AppendLine("                                              ,[FNQtySpecial]")
                                        oStrBuilder.AppendLine("											  ,[FTRemark]")
                                        oStrBuilder.AppendLine("											  ,[FTStateActive])")
                                        oStrBuilder.AppendLine("VALUES(NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysPlantId))
                                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tPlantCode) & "'")
                                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tPlantDesc) & "'")
                                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tPlantDesc) & "'")
                                        oStrBuilder.AppendLine("      ,2")
                                        oStrBuilder.AppendLine("      ,1")
                                        oStrBuilder.AppendLine("      ,''")
                                        oStrBuilder.AppendLine("      ,'1')")

                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                            'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopPlant

                bRet = True
            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterCountry(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColCountry As String = "F39"

                Dim nLoopCountry As Integer

                For nLoopCountry = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลประเทศ...")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopCountry).Item(tColCountry)) Then
                        If oDBdt.Rows(nLoopCountry).Item(tColCountry).ToString() <> "" Then
                            Dim tCountry As String
                            tCountry = ""

                            tCountry = oDBdt.Rows(nLoopCountry).Item(tColCountry).ToString()
                            tCountry = tCountry.Trim()

                            tSql = ""
                            'tSql = "SELECT TOP 1 A.FTCountryNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A WITH(NOLOCK) WHERE A.FTCountryNameEN = N'" & HI.UL.ULF.rpQuoted(tCountry) & "';"
                            tSql = "SELECT TOP 1 A.FTCountryNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A WITH(NOLOCK) WHERE (A.FTCountryNameEN LIKE '%" & HI.UL.ULF.rpQuoted(tCountry) & "%' OR A.FTCountryCode LIKE '%" & HI.UL.ULF.rpQuoted(tCountry) & "');"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysCountryId As Integer

                                nFNHSysCountryId = Val(HI.TL.RunID.GetRunNoID("TCNMCountry", "FNHSysCountryId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                Dim oStrBuilder As New System.Text.StringBuilder()

                                oStrBuilder.Remove(0, oStrBuilder.Length)

                                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry]([FTInsUser]")
                                oStrBuilder.AppendLine("                                               ,[FDInsDate]")
                                oStrBuilder.AppendLine("                                               ,[FTInsTime]")
                                oStrBuilder.AppendLine("                                               ,[FTUpdUser]")
                                oStrBuilder.AppendLine("                                               ,[FDUpdDate]")
                                oStrBuilder.AppendLine("                                               ,[FTUpdTime]")
                                oStrBuilder.AppendLine("											   ,[FNHSysCountryId]")
                                oStrBuilder.AppendLine("											   ,[FNHSysContinentId]")
                                oStrBuilder.AppendLine("											   ,[FTCountryCode]")
                                oStrBuilder.AppendLine("											   ,[FTCountryNameTH]")
                                oStrBuilder.AppendLine("											   ,[FTCountryNameEN]")
                                oStrBuilder.AppendLine("											   ,[FTRemark]")
                                oStrBuilder.AppendLine("											   ,[FTStateActive])")
                                oStrBuilder.AppendLine("VALUES(NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysCountryId))
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tCountry) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tCountry) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tCountry) & "'")
                                oStrBuilder.AppendLine("      ,''")
                                oStrBuilder.AppendLine("      ,'1');")

                                tSql = ""
                                tSql = oStrBuilder.ToString()

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                                    '...validate new country not map continent
                                    Dim nFNHSysContinentId As Integer

                                    nFNHSysContinentId = Val(HI.TL.RunID.GetRunNoID("TCNMContinent", "FNHSysContinentId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    oStrBuilder.AppendLine("DECLARE @FNHSysContinentId AS INT;")
                                    oStrBuilder.AppendLine("DECLARE @FNHSysCountryId AS INT;")
                                    oStrBuilder.AppendLine(String.Format("SET @FNHSysContinentId = {0};", nFNHSysContinentId))
                                    oStrBuilder.AppendLine(String.Format("SET @FNHSysCountryId = {0};", nFNHSysCountryId))
                                    oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMContinent] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]")
                                    oStrBuilder.AppendLine("											 ,[FNHSysContinentId]")
                                    oStrBuilder.AppendLine("										     ,[FTContinentCode]")
                                    oStrBuilder.AppendLine("										     ,[FTContinentNameTH]")
                                    oStrBuilder.AppendLine("										     ,[FTContinentNameEN]")
                                    oStrBuilder.AppendLine("											 ,[FTRemark]")
                                    oStrBuilder.AppendLine("										     ,[FTStateActive])")
                                    oStrBuilder.AppendLine("SELECT NULL, NULL, NULL, NULL, NULL, NULL, @FNHSysContinentId, A.FTCountryCode, A.FTCountryCode, A.FTCountryCode,'', N'1'")
                                    oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A WITH(NOLOCK)")
                                    oStrBuilder.AppendLine("WHERE A.FNHSysCountryId = @FNHSysCountryId;")

                                    If oStrBuilder.Length > 0 Then
                                        tSql = ""
                                        tSql = oStrBuilder.ToString()
                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                            '...map country zone continent
                                            tSql = ""
                                            tSql = "UPDATE A"
                                            tSql &= Environment.NewLine & "SET  A.FNHSysContinentId = " & nFNHSysContinentId
                                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A"
                                            tSql &= Environment.NewLine & "WHERE A.FNHSysCountryId = " & nFNHSysCountryId & ";"

                                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopCountry

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterMerchanUnit(ByVal oDBDt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = True
        'Try
        '    If Not oDBDt Is Nothing And oDBDt.Rows.Count > 0 And oDBDt.Rows.Count > _nRowHeader Then
        '        Dim tColUom As String = "F47"

        '        Dim nLoopUOM As Integer

        '        For nLoopUOM = _nStartRowImportExcel To oDBDt.Rows.Count - 1

        '            If Not DBNull.Value.Equals(oDBDt.Rows(nLoopUOM).Item(tColUom)) Then
        '                If oDBDt.Rows(nLoopUOM).Item(tColUom).ToString() <> "" Then
        '                    Dim tUOM As String
        '                    tUOM = ""

        '                    tUOM = oDBDt.Rows(nLoopUOM).Item(tColUom).ToString()

        '                    If UCase(tUOM) <> _tUnitImport Then
        '                        tSql = ""
        '                        tSql = "SELECT TOP 1 A.FTUnitCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS A WITH(NOLOCK) WHERE A.FTUnitCode = N'" & HI.UL.ULF.rpQuoted(tUOM) & "' AND A.FTStateUnitSale = '1';"

        '                        If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
        '                            Dim nFNHSysUnitId As Integer

        '                            nFNHSysUnitId = Val(HI.TL.RunID.GetRunNoID("TCNMUnit", "FNHSysUnitId", Conn.DB.DataBaseName.DB_MASTER).ToString())

        '                            Dim oStrBuilder As New System.Text.StringBuilder()

        '                            oStrBuilder.Remove(0, oStrBuilder.Length)

        '                            oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit]([FTInsUser]")
        '                            oStrBuilder.AppendLine("									   ,[FDInsDate]")
        '                            oStrBuilder.AppendLine("									   ,[FTInsTime]")
        '                            oStrBuilder.AppendLine("									   ,[FTUpdUser]")
        '                            oStrBuilder.AppendLine("									   ,[FDUpdDate]")
        '                            oStrBuilder.AppendLine("									   ,[FTUpdTime]")
        '                            oStrBuilder.AppendLine("									   ,[FNHSysUnitId]")
        '                            oStrBuilder.AppendLine("									   ,[FTUnitCode]")
        '                            oStrBuilder.AppendLine("									   ,[FTUnitNameTH]")
        '                            oStrBuilder.AppendLine("									   ,[FTUnitNameEN]")
        '                            oStrBuilder.AppendLine("									   ,[FTRemark]")
        '                            oStrBuilder.AppendLine("                                       ,[FTStateUnitSale]")
        '                            oStrBuilder.AppendLine("									   ,[FTStateUnitPurchase]")
        '                            oStrBuilder.AppendLine("									   ,[FTStateUnitStock]")
        '                            oStrBuilder.AppendLine("									   ,[FTStateActive])")
        '                            oStrBuilder.AppendLine("VALUES(NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysUnitId))
        '                            oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tUOM) & "'")
        '                            oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tUOM) & "'")
        '                            oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tUOM) & "'")
        '                            oStrBuilder.AppendLine("      ,''")
        '                            oStrBuilder.AppendLine("      ,'1'")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,'1')")

        '                            tSql = ""
        '                            tSql = oStrBuilder.ToString()

        '                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
        '                                'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '                            End If

        '                        End If

        '                    End If

        '                End If

        '            End If

        '            'Application.DoEvents()

        '        Next nLoopUOM

        '        bRet = True

        '    End If

        'Catch ex As Exception
        '    'If System.Diagnostics.Debugger.IsAttached = True Then
        '    MsgBox(ex.Message().ToString() & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '    ' End If
        'End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterShipMode(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColShipMode As String = "F46"

                Dim nLoopShipMode As Integer

                For nLoopShipMode = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลประเภทการจัดส่งสินค้า..")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopShipMode).Item(tColShipMode)) Then
                        If oDBdt.Rows(nLoopShipMode).Item(tColShipMode).ToString() <> "" Then
                            Dim tShipMode As String
                            tShipMode = ""

                            tShipMode = oDBdt.Rows(nLoopShipMode).Item(tColShipMode).ToString()
                            tShipMode = tShipMode.Trim()

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTShipModeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS A WITH(NOLOCK) WHERE A.FTShipModeCode = N'" & HI.UL.ULF.rpQuoted(tShipMode) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysShipModeId As Integer

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

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopShipMode

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterBuyer(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColBuyer As String = "F36"
                Dim tColBuyerDesc As String = "F37"

                Dim nLoopBuyer As Integer
                'For nLoopBuyer = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopBuyer = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลผู้ซื้อ...")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyer).Item(tColBuyer)) Then
                        Dim tBuyerCode As String = ""

                        tBuyerCode = oDBdt.Rows(nLoopBuyer).Item(tColBuyer).ToString()
                        tBuyerCode = tBuyerCode.Trim()

                        If tBuyerCode <> "" And UCase(tBuyerCode) <> UCase(_tNotAssigned) And UCase(tBuyerCode) <> UCase(_tNA) Then

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyer).Item(tColBuyerDesc)) Then
                                Dim tBuyerDesc As String = ""

                                tBuyerDesc = oDBdt.Rows(nLoopBuyer).Item(tColBuyerDesc).ToString()
                                tBuyerDesc = tBuyerDesc.Trim()

                                If tBuyerDesc <> "" And UCase(tBuyerDesc) <> UCase(_tNotAssigned) And UCase(tBuyerDesc) <> UCase(_tNA) Then

                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTBuyerCode  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS A WITH(NOLOCK) WHERE A.FTBuyerCode = N'" & HI.UL.ULF.rpQuoted(tBuyerCode) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                        Dim nFNHSysBuyerId As Integer

                                        nFNHSysBuyerId = Val(HI.TL.RunID.GetRunNoID("TMERMBuyer", "FNHSysBuyerId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                        Dim oStrBuilder As New System.Text.StringBuilder()

                                        oStrBuilder.Remove(0, oStrBuilder.Length)

                                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer]")
                                        oStrBuilder.AppendLine("           ([FTInsUser]")
                                        oStrBuilder.AppendLine("           ,[FDInsDate]")
                                        oStrBuilder.AppendLine("           ,[FTInsTime]")
                                        oStrBuilder.AppendLine("           ,[FTUpdUser]")
                                        oStrBuilder.AppendLine("           ,[FDUpdDate]")
                                        oStrBuilder.AppendLine("           ,[FTUpdTime]")
                                        oStrBuilder.AppendLine("           ,[FNHSysBuyerId]")
                                        oStrBuilder.AppendLine("           ,[FTBuyerCode]")
                                        oStrBuilder.AppendLine("           ,[FTBuyerNameTH]")
                                        oStrBuilder.AppendLine("           ,[FTBuyerNameEN]")
                                        oStrBuilder.AppendLine("           ,[FTRemark]")
                                        oStrBuilder.AppendLine("           ,[FTStateActive])")
                                        oStrBuilder.AppendLine("VALUES")
                                        oStrBuilder.AppendLine(String.Format("   ({0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", nFNHSysBuyerId))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "N'" & HI.UL.ULF.rpQuoted(tBuyerCode) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "N'" & HI.UL.ULF.rpQuoted(tBuyerDesc) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "N'" & HI.UL.ULF.rpQuoted(tBuyerDesc) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "''"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0})", "N'1'"))

                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                            'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        Else
                                            MsgBox("Execute data not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopBuyer

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterMerchanTeam(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColMerTeam As String = "F14"

                Dim nLoopMasterMerchanTeam As Integer

                For nLoopMasterMerchanTeam = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                    Dim tMerchanTeam As String
                    tMerchanTeam = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopMasterMerchanTeam).Item(tColMerTeam)) Then
                        If oDBdt.Rows(nLoopMasterMerchanTeam).Item(tColMerTeam).ToString() <> "" Then
                            tMerchanTeam = oDBdt.Rows(nLoopMasterMerchanTeam).Item(tColMerTeam).ToString()
                            tMerchanTeam = tMerchanTeam.Trim()

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTMerTeamNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK) WHERE A.FTMerTeamNameEN = N'" & HI.UL.ULF.rpQuoted(tMerchanTeam) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysMerTeamId As Integer

                                nFNHSysMerTeamId = Val(HI.TL.RunID.GetRunNoID("TMERMMerTeam", "FNHSysMerTeamId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                Dim tMerchanTeamCode As String

                                tMerchanTeamCode = W_GETtMerTeamCode()

                                If tMerchanTeamCode <> "" Then
                                    tSql = ""
                                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam]([FTInsUser],[FDInsDate],[FTInsTime]"
                                    tSql &= Environment.NewLine & "                         ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                    tSql &= Environment.NewLine & "                         ,[FNHSysMerTeamId],[FTMerTeamCode],[FTMerTeamNameTH]"
                                    tSql &= Environment.NewLine & "                         ,[FTMerTeamNameEN],[FTRemark],[FTStateActive])"
                                    tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                    tSql &= "                            ,NULL, NULL, NULL"
                                    tSql &= "," & nFNHSysMerTeamId & ", N'" & HI.UL.ULF.rpQuoted(tMerchanTeamCode) & "', N'" & HI.UL.ULF.rpQuoted(tMerchanTeam) & "'"
                                    tSql &= ",N'" & HI.UL.ULF.rpQuoted(tMerchanTeam) & "', '', '1')"

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                                        'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopMasterMerchanTeam

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterGender(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColGender As String = "F18"

                Dim nLoopMasterGender As Integer
                'For nLoopMasterGender = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopMasterGender = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเพศผลิตภัณฑ์...")

                    Dim tMatchGender As String
                    tMatchGender = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopMasterGender).Item(tColGender)) Then
                        If oDBdt.Rows(nLoopMasterGender).Item(tColGender).ToString() <> "" Then
                            tMatchGender = oDBdt.Rows(nLoopMasterGender).Item(tColGender).ToString()
                            tMatchGender = tMatchGender.Trim()

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTGenderNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS A  WITH(NOLOCK) WHERE A.FTGenderNameEN = N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysGenderId As Integer

                                nFNHSysGenderId = Val(HI.TL.RunID.GetRunNoID("TMERMGender", "FNHSysGenderId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                tSql = ""
                                tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender]([FTInsUser],[FDInsDate],[FTInsTime]"
                                tSql &= Environment.NewLine & "                        ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                tSql &= Environment.NewLine & "                        ,[FNHSysGenderId],[FTGenderCode],[FTGenderNameTH]"
                                tSql &= Environment.NewLine & "                        ,[FTGenderNameEN],[FTRemark],[FTStateActive])"
                                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                tSql &= "                            ,NULL, NULL , NULL"
                                tSql &= "                            ," & nFNHSysGenderId & ", N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "'"
                                tSql &= "                            ,N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', '', '1')"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopMasterGender

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchColor(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim oDBdtMatColorTemp As System.Data.DataTable

                oDBdtMatColorTemp = New System.Data.DataTable

                Dim oColFTMatColorCode As DataColumn
                oColFTMatColorCode = New DataColumn("FTMatColorCode", System.Type.GetType("System.String"))
                oColFTMatColorCode.Caption = "FTMatColorCode"

                oDBdtMatColorTemp.Columns.Add(oColFTMatColorCode.ColumnName, oColFTMatColorCode.DataType)

                Dim oColFTMatColorDesc As DataColumn
                oColFTMatColorDesc = New DataColumn("FTMatColorDesc", System.Type.GetType("System.String"))
                oColFTMatColorDesc.Caption = "FTMatColorDesc"

                oDBdtMatColorTemp.Columns.Add(oColFTMatColorDesc.ColumnName, oColFTMatColorDesc.DataType)

                Dim tColMatColor As String = "F16"
                Dim tColMatColorDesc As String = "F23"

                Dim nLoopRowMatchColor As Integer
                'For nLoopRowMatchColor = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopRowMatchColor = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลสีผลิตภัณฑ์...")

                    Dim tMatchColor As String = ""
                    tMatchColor = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColor)) Then
                        If oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColor).ToString() <> "" Then
                            tMatchColor = oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColor)

                            Dim nPosHyphen As Integer

                            nPosHyphen = InStr(tMatchColor, _tHyphenSign)

                            If nPosHyphen > 0 Then
                                tMatchColor = Microsoft.VisualBasic.Mid$(tMatchColor, nPosHyphen + 1, Len(tMatchColor))

                                If Not oDBdtMatColorTemp Is Nothing And oDBdtMatColorTemp.Rows.Count > 0 Then
                                    Dim oDataRowValidate As DataRow()

                                    oDataRowValidate = oDBdtMatColorTemp.Select("FTMatColorCode = '" & HI.UL.ULF.rpQuoted(tMatchColor) & "'")

                                    If Not oDataRowValidate Is Nothing And oDataRowValidate.Length <= 0 Then
                                        tSql = ""
                                        tSql = "SELECT TOP 1 A.FTMatColorCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A WITH(NOLOCK) WHERE A.FTMatColorCode  = N'" & HI.UL.ULF.rpQuoted(tMatchColor) & "';"

                                        If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then

                                            Dim tMatchColorDesc As String = ""
                                            tMatchColorDesc = oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColorDesc).ToString().Trim()

                                            Dim oRowItemMatColor As DataRow
                                            '...กรณีเป็นรายการ รหัส สีที่ยังไม่มีอยู่ในระบบ
                                            oRowItemMatColor = oDBdtMatColorTemp.NewRow()
                                            oRowItemMatColor.Item("FTMatColorCode") = tMatchColor
                                            oRowItemMatColor.Item("FTMatColorDesc") = tMatchColorDesc
                                            oDBdtMatColorTemp.Rows.Add(oRowItemMatColor)

                                        End If

                                    End If

                                ElseIf Not oDBdtMatColorTemp Is Nothing And oDBdtMatColorTemp.Rows.Count = 0 Then
                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTMatColorCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A WITH(NOLOCK) WHERE A.FTMatColorCode  = N'" & HI.UL.ULF.rpQuoted(tMatchColor) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then

                                        Dim tMatchColorDesc As String = ""
                                        tMatchColorDesc = oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColorDesc).ToString().Trim()

                                        Dim oRowItemMatColor As DataRow
                                        '...กรณีเป็นรายการ รหัส สีที่ยังไม่มีอยู่ในระบบ
                                        oRowItemMatColor = oDBdtMatColorTemp.NewRow()
                                        oRowItemMatColor.Item("FTMatColorCode") = tMatchColor
                                        oRowItemMatColor.Item("FTMatColorDesc") = tMatchColorDesc
                                        oDBdtMatColorTemp.Rows.Add(oRowItemMatColor)

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopRowMatchColor

                If Not oDBdtMatColorTemp Is Nothing And oDBdtMatColorTemp.Rows.Count > 0 Then
                    oDBdtMatColorTemp.AcceptChanges()

                    Dim oDBdvNotExistsMatColor As System.Data.DataView = oDBdtMatColorTemp.DefaultView
                    oDBdvNotExistsMatColor.Sort = "FTMatColorCode"
                    Dim oDBdtNotExistsMatColor As System.Data.DataTable = oDBdvNotExistsMatColor.ToTable()
                    If Not oDBdtNotExistsMatColor Is Nothing And oDBdtNotExistsMatColor.Rows.Count > 0 Then
                        Dim nLoopNotExitsMatColor As Integer
                        Dim tMatColorCodeNotExists As String, tMatColorDescNotExists As String

                        For nLoopNotExitsMatColor = 0 To oDBdtNotExistsMatColor.Rows.Count - 1
                            tMatColorCodeNotExists = oDBdtNotExistsMatColor.Rows(nLoopNotExitsMatColor).Item("FTMatColorCode")
                            tMatColorDescNotExists = oDBdtNotExistsMatColor.Rows(nLoopNotExitsMatColor).Item("FTMatColorDesc")

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTMatColorCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A WITH(NOLOCK) WHERE A.FTMatColorCode  = N'" & HI.UL.ULF.rpQuoted(tMatColorCodeNotExists) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysMatColorId As Integer

                                nFNHSysMatColorId = Val(HI.TL.RunID.GetRunNoID("TMERMMatColor", "FNHSysMatColorId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                tSql = ""
                                tSql = "DECLARE @nFNMatColorId AS INT;"
                                tSql &= Environment.NewLine & "DECLARE @tFTMatColorCode AS NVARCHAR(30);"
                                tSql &= Environment.NewLine & "DECLARE @tFTMatColorDesc AS NVARCHAR(100);"
                                tSql &= Environment.NewLine & "SET @nFNMatColorId = " & nFNHSysMatColorId & ";"
                                tSql &= Environment.NewLine & "SET @tFTMatColorCode = N'" & HI.UL.ULF.rpQuoted(tMatColorCodeNotExists) & "';"
                                tSql &= Environment.NewLine & "SET @tFTMatColorDesc = N'" & HI.UL.ULF.rpQuoted(tMatColorDescNotExists) & "';"
                                tSql &= Environment.NewLine & "DECLARE @FNMatColorSeq AS INT;"
                                tSql &= Environment.NewLine & "SELECT @FNMatColorSeq = ISNULL(MAX(A.FNMatColorSeq),0)"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A"
                                tSql &= Environment.NewLine & "GROUP BY A.FNMatColorSeq;"
                                tSql &= Environment.NewLine & "--PRINT 'FNMatColorSeq Current : ' + CONVERT(VARCHAR(10), @FNMatColorSeq);"
                                tSql &= Environment.NewLine & "--PRINT 'FNMatColorSeq Next : ' + CONVERT(VARCHAR(10), (@FNMatColorSeq + 1));"
                                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor]([FTInsUser],[FDInsDate],[FTInsTime]"
                                tSql &= Environment.NewLine & "                                                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                tSql &= Environment.NewLine & "                                                 ,[FNHSysMatColorId],[FTMatColorCode],[FNMatColorSeq]"
                                tSql &= Environment.NewLine & "                                                 ,[FTMatColorNameTH],[FTMatColorNameEN],[FTRemark],[FTStateActive])"
                                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                tSql &= Environment.NewLine & ",NULL, NULL, NULL"
                                tSql &= Environment.NewLine & ",@nFNMatColorId, @tFTMatColorCode, (ISNULL(@FNMatColorSeq, 0) + 1)"
                                tSql &= Environment.NewLine & ",@tFTMatColorDesc, @tFTMatColorDesc, '', '1');"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        Next nLoopNotExitsMatColor

                    End If

                Else
                    '...do nothing
                End If

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchSize(ByVal oDBdt As System.Data.DataTable, ByRef paramDTNotMapSize As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader And oDBdt.Columns.Count > _nStartColImportSizeValExcel Then
                Dim nCntColMatchSize As Integer
                Dim nLoopColMatchSize As Integer
                Dim tMatchSize As String

                nCntColMatchSize = oDBdt.Columns.Count
                nLoopColMatchSize = _nStartColImportSizeValExcel

                tMatchSize = oDBdt.Rows(_nRowHeader - 2).Item(_nStartColImportSizeValExcel).ToString()

                If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                    Dim tMapSizeExtension As String = ""
                    Dim oDataRow As DataRow()
                    Try
                        oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                        If oDataRow.Length > 0 Then '...XXL ==> 2XL
                            If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                            End If

                        End If

                    Catch ex As Exception
                    End Try

                End If

                Do While tMatchSize <> "" And UCase(tMatchSize) <> UCase(_tTextMatchSizeTerminate) And nLoopColMatchSize < nCntColMatchSize

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลขนาดผลิตภัณฑ์...")

                    tSql = ""
                    tSql = "SELECT TOP 1 A.FTMatSizeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK) WHERE A.FTMatSizeCode = N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "';"
                    '...Mat Size Code not exists in system
                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                        '...not match size code in system database TMERMMatSize [MASTER]

                        REM 2014/11/27
                        '====================================================================================================================================================================================================
                        'Dim nFNHSysMatSizeId As Integer

                        'nFNHSysMatSizeId = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                        'tSql = ""
                        'tSql = "DECLARE @FNMatSizeSeqMax AS INT;"
                        'tSql &= Environment.NewLine & "SELECT @FNMatSizeSeqMax = MAX(A.FNMatSizeSeq)"
                        'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK)"
                        'tSql &= Environment.NewLine & "GROUP BY A.FNHSysMatSizeId;"
                        'tSql &= Environment.NewLine & "--PRINT 'FNMatSizeSeqMax Current : ' + CONVERT(VARCHAR(10),ISNULL(@FNMatSizeSeqMax, 1));"
                        'tSql &= Environment.NewLine & "--PRINT 'FNMatSizeSeq Max Next : ' + CONVERT(VARCHAR(10),(ISNULL(@FNMatSizeSeqMax, 1) + 1));"
                        'tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] ([FTInsUser],[FDInsDate],[FTInsTime]"
                        'tSql &= Environment.NewLine & "                                                                                                    ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                        'tSql &= Environment.NewLine & "                                                                                                    ,[FNHSysMatSizeId],[FTMatSizeCode],[FNMatSizeSeq]"
                        'tSql &= Environment.NewLine & "							                                                                           ,[FTMatSizeNameTH],[FTMatSizeNameEN],[FTRemark],[FTStateActive])"
                        'tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                        'tSql &= ",NULL, NULL, NULL"
                        'tSql &= ", " & nFNHSysMatSizeId & ", N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "', (ISNULL(@FNMatSizeSeqMax, 0) + 1)"
                        'tSql &= ", N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "', N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "', '', '1');"

                        'If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                        '    'MsgBox("Execute Data Complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        'End If
                        '====================================================================================================================================================================================================

                        If paramDTNotMapSize.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'").Length <= 0 Then
                            Dim oDataRow As System.Data.DataRow

                            oDataRow = paramDTNotMapSize.NewRow()

                            oDataRow.Item("FTSizeCodeNotExists") = tMatchSize
                            oDataRow.Item("FTMapSizeExtend") = ""

                            paramDTNotMapSize.Rows.Add(oDataRow)
                        End If

                    End If

                    nLoopColMatchSize = nLoopColMatchSize + 5

                    tMatchSize = oDBdt.Rows(_nRowHeader - 2).Item(nLoopColMatchSize).ToString()

                    If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                        Dim tMapSizeExtension As String = ""
                        Dim oDataRow As DataRow()
                        Try
                            oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")

                            If oDataRow.Length > 0 Then
                                If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                    tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                                End If

                            Else
                                '...mat size id not exists in system database ???
                            End If

                        Catch ex As Exception
                        End Try

                    End If

                    'Application.DoEvents()

                Loop

                If Not paramDTNotMapSize Is Nothing AndAlso paramDTNotMapSize.Rows.Count > 0 Then paramDTNotMapSize.AcceptChanges()

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterStyle(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then

                '...represent style master when import new style
                '-------------------------------------------------------------------------------
                oDBdtStyle = Nothing
                oDBdtStyle = New System.Data.DataTable

                Dim oColFTStyleCode As DataColumn
                oColFTStyleCode = New DataColumn("FTStyleCode", System.Type.GetType("System.String"))

                oDBdtStyle.Columns.Add(oColFTStyleCode.ColumnName, oColFTStyleCode.DataType)

                Dim oColFTStyleName As DataColumn
                oColFTStyleName = New DataColumn("FTStyleName", System.Type.GetType("System.String"))

                oDBdtStyle.Columns.Add(oColFTStyleName.ColumnName, oColFTStyleName.DataType)
                '-------------------------------------------------------------------------------

                Dim tColStyle As String = "F15"
                Dim tColMaterialStyle As String = "F17"
                Dim tColPlanSeasonStyle As String = "F25"
                Dim tColYearStyle As String = "F26"
                Dim tColCustStyle As String = "F36"

                Dim nFNHSysCustId As Integer
                nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag)

                Dim nLoopStyle As Integer
                'For nLoopStyle = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopStyle = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลแบบผลิตภัณฑ์...")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColStyle)) Then
                        If oDBdt.Rows(nLoopStyle).Item(tColStyle).ToString() <> "" Then
                            Dim tStyleCode As String
                            tStyleCode = ""

                            tStyleCode = oDBdt.Rows(nLoopStyle).Item(tColStyle).ToString()
                            tStyleCode = tStyleCode.Trim()

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColPlanSeasonStyle)) Then
                                If oDBdt.Rows(nLoopStyle).Item(tColPlanSeasonStyle).ToString() <> "" Then
                                    Dim tPlanningSeasonStyle As String
                                    tPlanningSeasonStyle = ""

                                    tPlanningSeasonStyle = oDBdt.Rows(nLoopStyle).Item(tColPlanSeasonStyle).ToString()
                                    tPlanningSeasonStyle.Trim()

                                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColYearStyle)) Then
                                        If oDBdt.Rows(nLoopStyle).Item(tColYearStyle).ToString() <> "" Then
                                            Dim tYearStyle As String
                                            tYearStyle = ""

                                            tYearStyle = oDBdt.Rows(nLoopStyle).Item(tColYearStyle).ToString()
                                            tYearStyle = tYearStyle.Trim()

                                            If Len(tYearStyle) > 2 Then
                                                tYearStyle = Microsoft.VisualBasic.Right$(tYearStyle, 2)
                                            End If

                                            Dim tStyleImport As String = ""


                                            Select Case FNHSysVenderPramId.Text.ToUpper
                                                Case "HSC"
                                                    'tStyleImport = tStyleCode & tPlanningSeasonStyle & tYearStyle & "HSC"
                                                    tStyleImport = tStyleCode & "HSC"
                                                Case "HIP/DP"
                                                    tStyleImport = tStyleCode & "/HIP"
                                                Case "HIP/MS"
                                                    'tStyleImport = tStyleCode & tPlanningSeasonStyle & tYearStyle & "MS"
                                                    tStyleImport = tStyleCode & "MS"
                                                Case Else
                                                    'tStyleImport = tStyleCode & tPlanningSeasonStyle & tYearStyle
                                                    tStyleImport = tStyleCode
                                            End Select

                                            tSql = ""
                                            tSql = "SELECT TOP 1 A.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A  WITH(NOLOCK) WHERE A.FTStyleCode = '" & HI.UL.ULF.rpQuoted(tStyleImport) & "';"

                                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                                '...validate new style import order
                                                '-------------------------------------------------------------------------------------------------------------------------------------
                                                If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColMaterialStyle)) Then
                                                    If oDBdt.Rows(nLoopStyle).Item(tColMaterialStyle).ToString() <> "" Then
                                                        Dim tMaterialStyle As String = ""
                                                        tMaterialStyle = oDBdt.Rows(nLoopStyle).Item(tColMaterialStyle).ToString()
                                                        tMaterialStyle = tMaterialStyle.Trim()

                                                        Dim tStyleImportDesc As String = ""
                                                        'tStyleImportDesc = tMaterialStyle & "-" & tPlanningSeasonStyle & "-" & tYearStyle
                                                        tStyleImportDesc = tMaterialStyle

                                                        Dim tCustCodeStyle As String = ""
                                                        tCustCodeStyle = oDBdt.Rows(nLoopStyle).Item(tColCustStyle).ToString()
                                                        tCustCodeStyle = tCustCodeStyle.Trim()

                                                        Dim oRow As DataRow
                                                        If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                                                            Dim oDataRowExists As DataRow()
                                                            oDataRowExists = oDBdtStyle.Select("FTStyleCode = '" & HI.UL.ULF.rpQuoted(tStyleImport) & "'")
                                                            If oDataRowExists.Length > 0 Then
                                                                '...already exists
                                                            Else
                                                                oRow = oDBdtStyle.NewRow()
                                                                oRow.Item("FTStyleCode") = tStyleImport
                                                                oRow.Item("FTStyleName") = tStyleImportDesc
                                                                oDBdtStyle.Rows.Add(oRow)
                                                            End If

                                                        Else
                                                            oRow = oDBdtStyle.NewRow()
                                                            oRow.Item("FTStyleCode") = tStyleImport
                                                            oRow.Item("FTStyleName") = tStyleImportDesc
                                                            oDBdtStyle.Rows.Add(oRow)
                                                        End If

                                                    End If
                                                    '-------------------------------------------------------------------------------------------------------------------------------------

                                                End If

                                            End If

                                            tSql = ""
                                            tSql &= "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle"
                                            tSql &= " (FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive) "
                                            tSql &= Environment.NewLine & "SELECT TOP 1  FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTStyleCode"
                                            tSql &= Environment.NewLine & "                                  , FTStyleNameTH, FTStyleNameEN, FNHSysCustId"
                                            tSql &= Environment.NewLine & "                                  , ISNULL((SELECT TOP 1 FNHSysSeasonId "
                                            tSql &= Environment.NewLine & "                                            FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS A  WITH(NOLOCK)"
                                            tSql &= Environment.NewLine & "                                            WHERE A.FTSeasonCode = N'" & HI.UL.ULF.rpQuoted(tPlanningSeasonStyle & tYearStyle) & "'"
                                            tSql &= Environment.NewLine & "                                            ), 0) AS  FNHSysSeasonId"
                                            tSql &= Environment.NewLine & "                                  , FTStateActive"
                                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A  WITH(NOLOCK)"
                                            tSql &= Environment.NewLine & "WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'"
                                            tSql &= Environment.NewLine & "      AND NOT (A.FTStyleCode IN (SELECT FTStyleCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle) )"

                                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopStyle

                If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                    oDBdtStyle.AcceptChanges()
                End If

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Sub CheckCurrencyImport(ByVal oDBdtSrc As System.Data.DataTable)
        _tCurtImport = 0

        If Not oDBdtSrc Is Nothing And oDBdtSrc.Rows.Count > 0 And oDBdtSrc.Rows.Count > _nRowHeader Then
            Dim _TCol As Integer = 0
            Dim FoundColumn As Boolean = False
            For Each Col As DataColumn In oDBdtSrc.Columns
                _TCol = _TCol + 1

                If oDBdtSrc.Rows(0).Item(Col).ToString.Trim.ToLower = "Overall Result".ToLower() And oDBdtSrc.Rows(2).Item(Col).ToString.Trim.ToLower = "Trading Co Gross Unit Price".ToLower() Then
                    FoundColumn = True
                    Exit For
                End If
            Next

            If FoundColumn = True Then
                If oDBdtSrc.Columns.Count > _TCol Then
                    _tCurtImport = _TCol + 1
                End If

            End If
        End If
    End Sub

    Private Function W_PRCbSaveListOrderToTemp(ByVal oDBdtSrc As System.Data.DataTable) As Boolean
        '...catch raw data before confirm import factory order no.
        Dim bAppendSrcHeader As Boolean = True
        Dim bAppendSrcDetial As Boolean = True
        Dim bRet As Boolean = False
        Dim _FTCurrency As String = ""
        Dim _FTSubPgm As String = ""

        Dim _FTSubPgmSeq As String = ""

        _tSubPgmImport = _tCurtImport + 1

        Try
            If Not oDBdtSrc Is Nothing And oDBdtSrc.Rows.Count > 0 And oDBdtSrc.Rows.Count > _nRowHeader Then

                '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการเตรียมข้อมูลเพื่อนำเข้ารายการใบสั่งผลิต...")

                '...clear soure before import order temp
                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                    'MsgBox("Delete data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                    'MsgBox("Delete data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                Dim nLoopOrder As Integer
                Dim oStrBuilder As New System.Text.StringBuilder()

                If bAppendSrcHeader = True Then

                    For nLoopOrder = _nStartRowImportExcel To oDBdtSrc.Rows.Count - 1
                        Application.DoEvents()

                        oStrBuilder.Remove(0, oStrBuilder.Length)

                        Dim tStyle As String
                        Dim tMaterialStyle As String
                        Dim tPlanSeasonStyle As String
                        Dim tYearStyle As String
                        Dim tFTCountry As String
                        Dim tFTBuyer As String

                        tFTCountry = ""
                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F39")) Then
                        '    If oDBdtSrc.Rows(nLoopOrder).Item("F39").ToString() <> "" Then
                        '        tFTCountry = oDBdtSrc.Rows(nLoopOrder).Item("F39").ToString().Trim()
                        '    End If
                        'End If

                        If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F36")) Then
                            If oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString() <> "" Then
                                tFTCountry = oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString().Trim()
                            End If
                        End If

                        tFTBuyer = ""
                        If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F36")) Then
                            If oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString() <> "" Then
                                tFTBuyer = oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString().Trim()
                            End If
                        End If

                        tStyle = oDBdtSrc.Rows(nLoopOrder).Item("F15")
                        tMaterialStyle = oDBdtSrc.Rows(nLoopOrder).Item("F17")
                        tPlanSeasonStyle = oDBdtSrc.Rows(nLoopOrder).Item("F25")

                        If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F26")) Then
                            If Len(oDBdtSrc.Rows(nLoopOrder).Item("F26")) = 4 Then
                                tYearStyle = Microsoft.VisualBasic.Right(oDBdtSrc.Rows(nLoopOrder).Item("F26"), 2)
                            ElseIf Len(oDBdtSrc.Rows(nLoopOrder).Item("F26")) = 2 Then
                                tYearStyle = oDBdtSrc.Rows(nLoopOrder).Item("F26")
                            End If
                        Else
                            tYearStyle = CStr(Now.Year)
                        End If

                        tStyle = tStyle.Trim() : tMaterialStyle = tMaterialStyle.Trim() : tPlanSeasonStyle = tPlanSeasonStyle.Trim() : tYearStyle = tYearStyle.Trim()

                        Dim tStyleCode As String = tStyle

                        'Select Case FNHSysVenderPramId.Text.ToUpper
                        '    Case "HSC"
                        '        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "HSC"
                        '        tStyleCode = tStyle & "HSC"
                        '    Case "HIP/DP"
                        '        tStyleCode = tStyle & "/HIP"
                        '    Case "HIP/MS"
                        '        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "MS"
                        '        tStyleCode = tStyle & "MS"
                        '    Case Else
                        '        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle
                        '        tStyleCode = tStyle
                        'End Select

                        Dim tStyleDesc As String
                        tStyleDesc = tMaterialStyle & "-" & tPlanSeasonStyle & "-" & tYearStyle

                        Dim objCulture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US", True)

                        objCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                        objCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

                        System.Threading.Thread.CurrentThread.CurrentCulture = objCulture

                        'Dim oDateF8 As DateTime
                        Dim tDateF8 As String

                        tDateF8 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F8"))

                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F8")) And CStr(oDBdtSrc.Rows(nLoopOrder).Item("F8")) <> "" Then
                        '    oDateF8 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F8") & " 00:00:00 AM", CultureInfo.InvariantCulture)
                        '    tDateF8 = Format$(oDateF8, "dd/MM/yyyy")
                        '    'oDateF8 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F8") & " 00:00:00 AM", System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'oDateF8 = DateTime.ParseExact(oDBdtSrc.Rows(nLoopOrder).Item("F8"), objCulture.DateTimeFormat.ShortDatePattern, System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'tDateF8 = HI.UL.ULDate.CheckDate(oDateF8)
                        'Else
                        '    tDateF8 = ""
                        'End If

                        'Dim oDateF9 As DateTime
                        Dim tDateF9 As String

                        tDateF9 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F9"))

                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F9")) And CStr(oDBdtSrc.Rows(nLoopOrder).Item("F9")) <> "" Then
                        '    oDateF9 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F9") & " 00:00:00 AM", CultureInfo.InvariantCulture)
                        '    tDateF9 = Format$(oDateF9, "dd/MM/yyyy")
                        '    'oDateF9 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F9") & " 00:00:00 AM", System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'oDateF9 = DateTime.ParseExact(oDBdtSrc.Rows(nLoopOrder).Item("F9"), objCulture.DateTimeFormat.ShortDatePattern, System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'tDateF9 = HI.UL.ULDate.CheckDate(oDateF9)
                        'Else
                        '    tDateF9 = ""
                        'End If

                        'Dim oDateF41 As DateTime
                        Dim tDateF41 As String

                        tDateF41 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F41"))

                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F41")) And CStr(oDBdtSrc.Rows(nLoopOrder).Item("F41")) <> "" Then
                        '    oDateF41 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F41") & " 00:00:00 AM", CultureInfo.InvariantCulture)
                        '    tDateF41 = Format$(oDateF41, "dd/MM/yyyy")
                        '    'oDateF41 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F41") & " 00:00:00 AM", System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'oDateF41 = DateTime.ParseExact(oDBdtSrc.Rows(nLoopOrder).Item("F41"), objCulture.DateTimeFormat.ShortDatePattern, System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'tDateF41 = HI.UL.ULDate.CheckDate(oDateF41)
                        'Else
                        '    tDateF41 = ""
                        'End If

                        'Dim oDateF42 As DateTime
                        Dim tDateF42 As String

                        tDateF42 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F42"))

                        If _tCurtImport > 0 Then
                            Try
                                _FTCurrency = oDBdtSrc.Rows(nLoopOrder).Item("F" & _tCurtImport.ToString & "").ToString.Trim()
                            Catch ex As Exception
                                _FTCurrency = ""
                            End Try

                        Else
                            _FTCurrency = ""
                        End If


                        _FTSubPgmSeq = "999999"

                        If _tSubPgmImport > 1 Then
                            Try
                                _FTSubPgm = oDBdtSrc.Rows(nLoopOrder).Item("F" & _tSubPgmImport.ToString & "").ToString.Trim()
                            Catch ex As Exception
                                _FTSubPgm = ""
                            End Try

                            Try
                                Try
                                    _FTSubPgmSeq = _FTSubPgmSeq & oDBdtSrc.Rows(nLoopOrder).Item("F" & (_tSubPgmImport + 1).ToString & "").ToString.Trim()
                                Catch ex As Exception
                                    _FTSubPgmSeq = "999999"
                                End Try


                                _FTSubPgmSeq = Microsoft.VisualBasic.Right(_FTSubPgmSeq, 6)

                            Catch ex As Exception

                            End Try
                        Else
                            _FTSubPgm = ""
                        End If


                        _FTSubPgm = Microsoft.VisualBasic.Left(_FTSubPgm, 200)
                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F42")) And CStr(oDBdtSrc.Rows(nLoopOrder).Item("F42")) <> "" Then
                        '    oDateF42 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F42") & " 00:00:00 AM", CultureInfo.InvariantCulture)
                        '    tDateF42 = Format$(oDateF42, "dd/MM/yyyy")
                        '    'oDateF42 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F42") & " 00:00:00 AM", System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'oDateF42 = DateTime.ParseExact(oDBdtSrc.Rows(nLoopOrder).Item("F42"), objCulture.DateTimeFormat.ShortDatePattern, System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    ' tDateF42 = HI.UL.ULDate.CheckDate(oDateF42)
                        'Else
                        '    tDateF42 = ""
                        'End If

                        '...record import order to temp
                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] ([FTUserLogin]")
                        oStrBuilder.AppendLine("	                                                     ,[FNRowImport]")
                        oStrBuilder.AppendLine("                                                         ,[FTPONo]")
                        oStrBuilder.AppendLine("						                                 ,[FTPOTrading]")
                        oStrBuilder.AppendLine("			                                             ,[FTPOItem]")
                        oStrBuilder.AppendLine("							                             ,[FTPOCreateDate]")
                        oStrBuilder.AppendLine("							                             ,[FTOrderDate]")
                        oStrBuilder.AppendLine("							                             ,[FTStyle]")
                        oStrBuilder.AppendLine("						                                 ,[FTStyleDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysMainCategoryId]")
                        oStrBuilder.AppendLine("							                             ,[FTMainCategoryDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysPlantId]")
                        oStrBuilder.AppendLine("							                             ,[FTPlantDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysBuyGrpId]")
                        oStrBuilder.AppendLine("							                             ,[FTBuyGrpNameDesc]")
                        oStrBuilder.AppendLine("							                             ,[FTMaterial]")
                        oStrBuilder.AppendLine("							                             ,[FTMaterialDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysGenderId]")
                        oStrBuilder.AppendLine("						                                 ,[FTGenderNameDesc]")
                        oStrBuilder.AppendLine("			                                             ,[FTPlanningSeason]")
                        oStrBuilder.AppendLine("							                             ,[FTYear]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysBuyerId]")
                        oStrBuilder.AppendLine("			                                             ,[FTCustCode]")
                        oStrBuilder.AppendLine("							                             ,[FTCustDesc]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysCountryId]")
                        oStrBuilder.AppendLine("				                                         ,[FTCountry]")
                        oStrBuilder.AppendLine("						                                 ,[FDShipDateOrginal]")
                        oStrBuilder.AppendLine("							                             ,[FDShipDate]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysMerTeamId]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysProdTypeId],[FTCurrency],[FNHSysProvinceId],[FTSubPgm],[FTSubPgmSeq])")
                        oStrBuilder.AppendLine(String.Format("VALUES({0}", "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", nLoopOrder))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F5")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F6")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F7")) & "'"))
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULDate.ConvertEnDB(oDBdtSrc.Rows(nLoopOrder).Item("F8"))) & "'")
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULDate.ConvertEnDB(oDBdtSrc.Rows(nLoopOrder).Item("F9"))) & "'")
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & oDBdtSrc.Rows(nLoopOrder).Item("F8")) & "'")
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & oDBdtSrc.Rows(nLoopOrder).Item("F9")) & "'")

                        If tDateF8 <> "" Then
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF8)))
                        Else
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", ""))
                        End If

                        If tDateF9 <> "" Then
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF9)))
                        Else
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", ""))
                        End If

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(tStyleDesc) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 L1.FNHSysMainCategoryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WITH(NOLOCK) WHERE L1.FTMainCategoryCode = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F10")), "", oDBdtSrc.Rows(nLoopOrder).Item("F10"))) & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F10")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 A.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F31")) & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F32")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 A.FNHSysBuyGrpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS A WITH(NOLOCK) WHERE A.FTBuyGrpCode = N'" & oDBdtSrc.Rows(nLoopOrder).Item("F43") & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F44")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F16")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F17")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 FNHSysGenderId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS A  WITH(NOLOCK) WHERE A.FTGenderCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "' OR A.FTGenderNameEN = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "' OR A.FTGenderNameTH = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F25")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F26")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT A.FNHSysBuyerId " &
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS A WITH(NOLOCK) " &
                                                                                           "WHERE A.FTBuyerCode = N'" & HI.UL.ULF.rpQuoted(tFTBuyer) & "'), ISNULL((SELECT L1.FNHSysBuyerId " &
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) " &
                                                                                           "WHERE L1.FTBuyerCode = N'-'), NULL)))"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F36")), "", oDBdtSrc.Rows(nLoopOrder).Item("F36"))) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F37")), "", oDBdtSrc.Rows(nLoopOrder).Item("F37"))) & "'"))

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "CASE WHEN N'" & HI.UL.ULF.rpQuoted(tFTCountry.Trim()) & "'  ='#' THEN (SELECT TOP 1 A.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F31")) & "')  ELSE (SELECT ISNULL((SELECT TOP 1 L1.FNHSysCountryId " &
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCMMProvince] AS L1 WITH(NOLOCK) " &
                                                                                           "WHERE  L1.FTProvinceCode =  N'" & HI.UL.ULF.rpQuoted(tFTCountry) & "'),NULL)) END"))

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F39")), "", oDBdtSrc.Rows(nLoopOrder).Item("F39"))) & "'"))
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULDate.ConvertEnDB(oDBdtSrc.Rows(nLoopOrder).Item("F41"))) & "'")
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULDate.ConvertEnDB(oDBdtSrc.Rows(nLoopOrder).Item("F42"))) & "'")
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & oDBdtSrc.Rows(nLoopOrder).Item("F41")) & "'")
                        'oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & oDBdtSrc.Rows(nLoopOrder).Item("F42")) & "'")


                        If tDateF41 <> "" Then
                            oStrBuilder.AppendLine(String.Format("     ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF41)))
                        Else
                            oStrBuilder.AppendLine(String.Format("     ,'{0}'", ""))
                        End If

                        If tDateF42 <> "" Then
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF42)))
                        Else
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", ""))
                        End If

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT TOP 1 A.FNHSysMerTeamId " &
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK)" &
                                                                                           "WHERE A.FTMerTeamCode = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F14")), "", oDBdtSrc.Rows(nLoopOrder).Item("F14"))) & "'),NULL))"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT TOP 1 A.FNHSysProdTypeId " &
                                                                            "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS A WITH(NOLOCK)" &
                                                                            "WHERE (A.FTProdTypeNameEN = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F11")), "", oDBdtSrc.Rows(nLoopOrder).Item("F11"))) & "' OR A.FTProdTypeNameTH = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F11")), "", oDBdtSrc.Rows(nLoopOrder).Item("F11"))) & "')),NULL))" & ""))
                        oStrBuilder.AppendLine(String.Format("      ,'{0}'", _FTCurrency) & "")
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "CASE WHEN N'" & HI.UL.ULF.rpQuoted(tFTCountry.Trim()) & "'  ='#' THEN (SELECT TOP 1 A.FNHSysProvinceId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F31")) & "')  ELSE (SELECT ISNULL((SELECT TOP 1 L1.FNHSysProvinceId " &
                                                                                         "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCMMProvince] AS L1 WITH(NOLOCK) " &
                                                                                         "WHERE  L1.FTProvinceCode =  N'" & HI.UL.ULF.rpQuoted(tFTCountry) & "'),NULL)) END "))

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(_FTSubPgm) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(_FTSubPgmSeq) & "')"))

                        tSql = ""
                        tSql = oStrBuilder.ToString()

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        Else
                            'MsgBox("Execute batch file not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                    Next nLoopOrder

                End If

                '...represent record colorway size breakdown
                If bAppendSrcDetial = True Then
                    '...add row by colorway / size breakdown
                    Dim tColPONo As String = "F5"
                    Dim tColPOTrading As String = "F6"
                    Dim tColPOItem As String = "F7"

                    Dim tColMatColor As String = "F16"
                    Dim tColMatColorDesc As String = "F23"
                    Dim tColShipMode As String = "F46"
                    Dim tColUom As String = "F47"

                    Dim nLoopSizeBreakdown As Integer

                    For nLoopSizeBreakdown = _nStartRowImportExcel To oDBdtSrc.Rows.Count - 1
                        'Application.DoEvents()

                        '...Column Extension Match Size Breakdown
                        '...col 1 : Total Item Qty
                        '...col 2 : NET Unit Price
                        '...col 3 : Trading Co Net Unit Price
                        '...col 4 : Gross Unit Price
                        '...col 5 : Tranding Co Gross Unit Price

                        Dim tDateF42 As String
                        tDateF42 = CStr(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F42"))

                        Dim tMatColor As String
                        tMatColor = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColMatColor)

                        Dim nPosHyphen As Integer
                        nPosHyphen = InStr(tMatColor, _tHyphenSign)

                        If nPosHyphen > 0 Then
                            tMatColor = Microsoft.VisualBasic.Mid$(tMatColor, nPosHyphen + 1, Len(tMatColor))

                            Dim nCntColMatchSize As Integer
                            Dim nLoopColMatchSize As Integer
                            Dim tMatchSize As String

                            nCntColMatchSize = oDBdtSrc.Columns.Count
                            nLoopColMatchSize = _nStartColImportSizeValExcel

                            tMatchSize = oDBdtSrc.Rows(_nRowHeader - 2).Item(_nStartColImportSizeValExcel).ToString()
                            tMatchSize = tMatchSize.Trim()

                            '...validate map size ex. XXL ==> 2XL, XXXL ==> 3XL, XXXXL ==> 4XL
                            If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                                Dim tMapSizeExtension As String = ""
                                Dim oDataRow As DataRow()
                                Try
                                    oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                    If oDataRow.Length > 0 Then

                                        If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                            tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                                        End If

                                    Else
                                        '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                        If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                            Dim oDataRowMapSizeManual As System.Data.DataRow()
                                            Try
                                                oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                                If oDataRowMapSizeManual.Length > 0 Then

                                                    If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                        tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                    End If

                                                End If

                                            Catch ex As Exception
                                            End Try

                                        End If

                                    End If

                                Catch ex As Exception
                                End Try

                            Else
                                '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                    Dim oDataRowMapSizeManual As System.Data.DataRow()

                                    Try
                                        oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                        If oDataRowMapSizeManual.Length > 0 Then

                                            If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                            End If

                                        End If

                                    Catch ex As Exception
                                    End Try

                                End If

                            End If

                            Do While tMatchSize <> "" And UCase(tMatchSize) <> UCase(_tTextMatchSizeTerminate) And (nLoopColMatchSize < nCntColMatchSize)
                                Dim tStyle As String
                                Dim tMaterialStyle As String
                                Dim tPlanSeasonStyle As String
                                Dim tYearStyle As String

                                tStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F15")
                                tMaterialStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F17")
                                tPlanSeasonStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F25")

                                tYearStyle = Microsoft.VisualBasic.Right$(CStr(Now.Year), 4)
                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")) Then
                                    If Len(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")) = 4 Then
                                        tYearStyle = Microsoft.VisualBasic.Right(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26"), 2)
                                    ElseIf Len(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")) = 2 Then
                                        tYearStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")
                                    End If
                                Else
                                    tYearStyle = Microsoft.VisualBasic.Right$(CStr(Now.Year), 2)
                                End If

                                Dim tStyleCode As String
                                ' tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle

                                Select Case FNHSysVenderPramId.Text.ToUpper
                                    Case "HSC"
                                        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "HSC"
                                        tStyleCode = tStyle & "HSC"
                                    Case "HIP/DP"
                                        tStyleCode = tStyle & "/HIP"
                                    Case "HIP/MS"
                                        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "MS"
                                        tStyleCode = tStyle & "MS"
                                    Case Else
                                        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle
                                        tStyleCode = tStyle
                                End Select

                                Dim nFNQuantity As Double                  '...Col ที่ 1 : ของไซส์
                                Dim tFTNetUnitPrice As String               '...Col ที่ 2 : ของไซส์
                                Dim tFTTradingCoNetUnitPrice As String      '...Col ที่ 3 : ของไซส์
                                Dim tFTGrossUnitPrice As String             '...Col ที่ 4 : ของไซส์
                                Dim tFTTradingCoGrossUnitPrice As String    '...Col ที่ 5 : ของไซส์

                                Dim nColIncMatSize As Integer
                                nColIncMatSize = nLoopColMatchSize

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize)) Then

                                    If CStr(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize)).Trim() <> "" Then
                                        nFNQuantity = Double.Parse(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize), NumberStyles.Currency)
                                    Else
                                        nFNQuantity = 0
                                    End If
                                Else
                                    nFNQuantity = 0
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 1)) Then
                                    tFTNetUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 1)

                                    Dim _tmp As String = tFTNetUnitPrice
                                    tFTNetUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTNetUnitPrice = tFTNetUnitPrice & C
                                        End If
                                    Next

                                    tFTNetUnitPrice = tFTNetUnitPrice.Trim()

                                    If IsNumeric(tFTNetUnitPrice) = False Then
                                        tFTNetUnitPrice = ""
                                    End If

                                Else
                                    tFTNetUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 2)) Then
                                    tFTTradingCoNetUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 2)

                                    Dim _tmp As String = tFTTradingCoNetUnitPrice
                                    tFTTradingCoNetUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTTradingCoNetUnitPrice = tFTTradingCoNetUnitPrice & C
                                        End If
                                    Next

                                    tFTTradingCoNetUnitPrice = tFTTradingCoNetUnitPrice.Trim()

                                    If IsNumeric(tFTTradingCoNetUnitPrice) = False Then
                                        tFTTradingCoNetUnitPrice = ""
                                    End If

                                Else
                                    tFTTradingCoNetUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 3)) Then
                                    tFTGrossUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 3)

                                    Dim _tmp As String = tFTGrossUnitPrice
                                    tFTGrossUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTGrossUnitPrice = tFTGrossUnitPrice & C
                                        End If
                                    Next

                                    tFTGrossUnitPrice = tFTGrossUnitPrice.Trim()

                                    If IsNumeric(tFTGrossUnitPrice) = False Then
                                        tFTGrossUnitPrice = ""
                                    End If

                                Else
                                    tFTGrossUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 4)) Then
                                    tFTTradingCoGrossUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 4)

                                    Dim _tmp As String = tFTTradingCoGrossUnitPrice
                                    tFTTradingCoGrossUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTTradingCoGrossUnitPrice = tFTTradingCoGrossUnitPrice & C
                                        End If
                                    Next

                                    tFTTradingCoGrossUnitPrice = tFTTradingCoGrossUnitPrice.Trim()

                                    If IsNumeric(tFTTradingCoGrossUnitPrice) = False Then
                                        tFTTradingCoGrossUnitPrice = ""
                                    End If

                                Else
                                    tFTTradingCoGrossUnitPrice = ""
                                End If

                                Application.DoEvents()

                                tSql = ""
                                tSql = "SELECT TOP 1 A.FTSizeBreakdownCode"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A WITH(NOLOCK)"
                                tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                tSql &= Environment.NewLine & "      AND A.FNRowImport = " & nLoopSizeBreakdown
                                tSql &= Environment.NewLine & "      AND A.FTPONo = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPONo)) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTPOTrading = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOTrading)) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTPOItem = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOItem)) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTStyle = N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTColorwayCode = N'" & HI.UL.ULF.rpQuoted(tMatColor) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTShipDate = N'" & HI.UL.ULDate.ConvertEnDB(tDateF42) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTSizeBreakdownCode = N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "';"
                                If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                                    '...exists in system import order temp and quantity greater than zero / update record
                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    If nFNQuantity > 0 Then

                                        oStrBuilder.AppendLine("UPDATE A")
                                        oStrBuilder.AppendLine(String.Format("SET A.[FNQuantity] = {0}", nFNQuantity))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTNetUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTNetUnitPrice) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTTradingCoNetUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTTradingCoNetUnitPrice) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTGrossUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTGrossUnitPrice) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTTradingCoGrossUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTTradingCoGrossUnitPrice) & "'"))
                                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A")
                                        oStrBuilder.AppendLine(String.Format("WHERE A.[FTUserLogin] = {0}", "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FNRowImport] = {0}", nLoopSizeBreakdown))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTPONo] = {0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPONo)) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTPOTrading] = {0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOTrading)) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTPOItem] = {0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOItem)) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTStyle] = {0}", "N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTColorwayCode] = {0}", "N'" & HI.UL.ULF.rpQuoted(tMatColor) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTShipDate] = {0}", "N'" & HI.UL.ULDate.ConvertEnDB(tDateF42) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTSizeBreakdownCode] = {0}", "N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "'"))

                                    End If

                                Else
                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp]")
                                    oStrBuilder.AppendLine("           ([FTUserLogin]")
                                    oStrBuilder.AppendLine("           ,[FNRowImport]")
                                    oStrBuilder.AppendLine("           ,[FTPONo]")
                                    oStrBuilder.AppendLine("           ,[FTPOTrading]")
                                    oStrBuilder.AppendLine("           ,[FTPOItem]")
                                    oStrBuilder.AppendLine("           ,[FTStyle]")
                                    oStrBuilder.AppendLine("           ,[FTColorwayCode]")
                                    oStrBuilder.AppendLine("           ,[FTColorwayDesc]")
                                    oStrBuilder.AppendLine("           ,[FTSizeBreakdownCode]")
                                    oStrBuilder.AppendLine("           ,[FTSizeBreakdownDesc]")
                                    oStrBuilder.AppendLine("           ,[FTMode]")
                                    oStrBuilder.AppendLine("           ,[FTUom]")
                                    oStrBuilder.AppendLine("           ,[FNQuantity]")
                                    oStrBuilder.AppendLine("           ,[FTNetUnitPrice]")
                                    oStrBuilder.AppendLine("           ,[FTTradingCoNetUnitPrice]")
                                    oStrBuilder.AppendLine("           ,[FTGrossUnitPrice]")
                                    oStrBuilder.AppendLine("           ,[FTTradingCoGrossUnitPrice],[FTShipDate])")
                                    oStrBuilder.AppendLine("     VALUES(")
                                    oStrBuilder.AppendLine(String.Format("            {0}", "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", nLoopSizeBreakdown))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPONo)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOTrading)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOItem)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & tMatColor & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColMatColorDesc)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColShipMode)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(FNHSysUnitId.Text) & "'"))
                                    'oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColUom)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", nFNQuantity))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTNetUnitPrice), "", tFTNetUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTTradingCoNetUnitPrice), "", tFTTradingCoNetUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTGrossUnitPrice), "", tFTGrossUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTTradingCoGrossUnitPrice), "", tFTTradingCoGrossUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0})", "N'" & HI.UL.ULDate.ConvertEnDB(tDateF42) & "'"))

                                End If

                                If oStrBuilder.Length > 0 Then
                                    tSql = ""
                                    tSql = oStrBuilder.ToString()

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    Else
                                        'MsgBox("Execute batch file not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End If

                                Else
                                    'MsgBox("Do nothing zzz !!!")
                                End If

                                '...increment col match size
                                nLoopColMatchSize = nLoopColMatchSize + 5

                                tMatchSize = oDBdtSrc.Rows(_nRowHeader - 2).Item(nLoopColMatchSize).ToString()

                                If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                                    Dim tMapSizeExtension As String = ""
                                    Dim oDataRow As DataRow()
                                    Try
                                        oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                        If oDataRow.Length > 0 Then
                                            If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                                tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                                            End If

                                        Else
                                            '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                            If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                                Dim oDataRowMapSizeManual As System.Data.DataRow()

                                                Try
                                                    oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                                    If oDataRowMapSizeManual.Length > 0 Then

                                                        If System.Diagnostics.Debugger.IsAttached = True Then
                                                            System.Diagnostics.Debug.Write("Map Size Code : " & tMatchSize & " To ==> " & "^_^" & " ")

                                                            If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                                tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                            End If

                                                            System.Diagnostics.Debug.Write(" " & tMatchSize & " " & Environment.NewLine)
                                                        Else
                                                            If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                                tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                            End If

                                                        End If

                                                    End If

                                                Catch ex As Exception
                                                End Try

                                            End If

                                        End If

                                    Catch ex As Exception
                                    End Try

                                Else
                                    '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                    If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                        Dim oDataRowMapSizeManual As System.Data.DataRow()

                                        Try
                                            oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                            If oDataRowMapSizeManual.Length > 0 Then

                                                If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                    tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                End If

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    End If

                                End If

                                'Application.DoEvents()

                            Loop

                        End If

                    Next nLoopSizeBreakdown

                End If

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
            REM MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        End Try

        Return bRet

    End Function

    Private Function W_PRCbShowBrowseDataImportFactoryOrder() As Boolean
        Dim bRet As Boolean = False

        'Me.ogdConfirmImport.DataSource = Nothing
        'Me.ogdConfirmImport.Refresh()
        'Call W_PRCbRemoveGridViewColumn(Me.ogvConfirmImport)
        'ogvConfirmImport.OptionsView.ColumnAutoWidth = False

        Try
            Dim oDBdtStatic As System.Data.DataTable
            Dim oDBdtDynamic As System.Data.DataTable
            Dim oDBdtBrowse As System.Data.DataTable
            Dim oDBdtMatSize As System.Data.DataTable

            Dim _dt As DataTable
            Dim _Qry As String = ""



            tSql = ""
            tSql = ";WITH cteImportSize(FTMatSize)"
            tSql &= Environment.NewLine & "AS (SELECT DISTINCT L1.FTSizeBreakdownCode"
            tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS L1"
            tSql &= Environment.NewLine & "    WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "    )"
            tSql &= Environment.NewLine & "--SELECT B.FNHSysMatSizeId, A.FTMatSize, B.FTMatSizeNameEN, B.FTMatSizeNameEN"
            tSql &= Environment.NewLine & "SELECT B.FTMatSizeCode"
            tSql &= Environment.NewLine & "FROM cteImportSize AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS B WITH(NOLOCK) ON A.FTMatSize = B.FTMatSizeCode"
            tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC;"

            oDBdtMatSize = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

            tSql = ""
            tSql = "SELECT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "      , A.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTPOCreateDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTPOCreateDate AS DATE), 103) ELSE '' END AS FTPOCreateDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTOrderDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTOrderDate AS DATE), 103) ELSE '' END AS FTOrderDate"
            tSql &= Environment.NewLine & "      , A.FNHSysPlantId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTPlantNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FNHSysPlantId = A.FNHSysPlantId) AS FTPlantDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysBuyGrpId  AS FNHSysBuyGrpId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId)  AS FTBuyGrpCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId) AS FTBuyGrpDesc"
            tSql &= Environment.NewLine & "      , A.FTStyle, A.FTStyleDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysMainCategoryId  AS FNHSysMainCategoryId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryDescEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTProdTypeNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysProdTypeId = A.FNHSysProdTypeId) AS FTProdTypeDesc"
            tSql &= Environment.NewLine & "      , A.FTMaterial, A.FTMaterial, A.FTPlanningSeason, A.FTYear"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysBuyerId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode)  AS FNHSysBuyerId"
            tSql &= Environment.NewLine & "      , A.FTCustCode AS FTBuyerCode"
            tSql &= Environment.NewLine & "      , ISNULL((SELECT TOP 1 L1.FTBuyerNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode), '') AS FTBuyerDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysCountryId AS FNHSysCountryId"
            tSql &= Environment.NewLine & "      , D.FTCountryCode AS FNHSysCountryCode"
            tSql &= Environment.NewLine & "      , D.FTCountryNameEN AS FTCountryDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysGenderId   AS FNHSysGenderId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderCode  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDate) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDate AS DATE), 103) ELSE '' END AS FDShipmentDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDateOrginal) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDateOrginal AS DATE), 103) ELSE '' END AS FDShipmentDateOriginal"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysMatColorId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS L1 WITH(NOLOCK) WHERE L1.FTMatColorCode = B.FTColorwayCode) AS FNHSysMatColorId"
            tSql &= Environment.NewLine & "      , B.FTColorwayCode"
            tSql &= Environment.NewLine & "      , B.FTColorwayDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysShipModeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS L1 WITH(NOLOCK) WHERE L1.FTShipModeCode = B.FTMode) AS FNHSysShipModeId"
            tSql &= Environment.NewLine & "      , B.FTMode AS FTShipModeDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysUnitId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L1 WITH(NOLOCK) WHERE L1.FTUnitCode = B.FTUom) AS FNHSysUnitId"
            tSql &= Environment.NewLine & "      , B.FTUom AS FTUnitDesc"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN (SELECT DISTINCT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "                                                                             , A.FTStyle, A.FTColorwayCode, A.FTColorwayDesc, A.FTMode, A.FTUom"
            tSql &= Environment.NewLine & "                                                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS B WITH(NOLOCK) ON A.FTColorwayCode = B.FTMatColorCode"
            tSql &= Environment.NewLine & "                                                               WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "                                                              ) AS B ON A.FNRowImport = B.FNRowImport"
            tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            tSql &= Environment.NewLine & "      AND A.FTPOTrading = B.FTPOTrading"
            tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C WITH(NOLOCK) ON B.FTColorwayCode = C.FTMatColorCode"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS D WITH(NOLOCK) ON A.FNHSysCountryId = D.FNHSysCountryId"
            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "ORDER BY A.FNRowImport ASC, A.FTPONo ASC, A.FTPOItem ASC, C.FNMatColorSeq ASC;"

            oDBdtStatic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtMatSize Is Nothing And oDBdtMatSize.Rows.Count > 0 Then

                'oGridViewConfirmImport = Me.ogdConfirmImport.Views(0)
                'Call W_PRCbRemoveGridViewColumn(oGridViewConfirmImport)

                tSql = ""
                tSql = "SELECT  A.FNRowImport, A.FTPONo, FTPOTrading, A.FTPOItem"
                tSql &= Environment.NewLine & "        , A.FTStyle, A.FTColorwayCode, A.FTSizeBreakdownCode AS FTMatSizeCode, A.FNQuantity"
                tSql &= Environment.NewLine & "        , A.FTNetUnitPrice, A.FTTradingCoNetUnitPrice"
                tSql &= Environment.NewLine & "        , A.FTGrossUnitPrice, A.FTTradingCoGrossUnitPrice"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS B ON A.FTSizeBreakdownCode = B.FTMatSizeCode"
                tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C ON A.FTColorwayCode = C.FTMatColorCode"
                tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "ORDER BY A.FNRowImport ASC, A.FTPONo, A.FTPOItem, C.FNMatColorSeq ASC, B.FNMatSizeSeq ASC"

                oDBdtDynamic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                'If Not oDBdtStatic Is Nothing And oDBdtStatic.Rows.Count > 0 Then
                '    Me.ogdConfirmImport.DataSource = Nothing
                '    oDBdtBrowse = New DataTable()
                '    oDBdtBrowse = oDBdtStatic.Copy()
                'End If

                For Each oRow As DataRow In oDBdtMatSize.Rows

                    For nLoopCol As Integer = 1 To 5
                        Select Case nLoopCol
                            Case 1
                                '...FNQuantity : Total Item Qty
                                Dim oColAppendSizeQty As DataColumn = New DataColumn("FNQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Int32"))
                                oColAppendSizeQty.Caption = "Total Item Qty : " & oRow.Item("FTMatSizeCode").ToString()

                                Me.ogvConfirmImport.Columns.AddField(oColAppendSizeQty.ColumnName)
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).FieldName = oColAppendSizeQty.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).Name = oColAppendSizeQty.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).Caption = oColAppendSizeQty.Caption
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).Visible = True
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).OptionsColumn.AllowEdit = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).OptionsColumn.AllowMove = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).OptionsColumn.AllowSort = False

                                oDBdtStatic.Columns.Add(oColAppendSizeQty.ColumnName, oColAppendSizeQty.DataType)

                            Case 2
                                '...FTNetUnitPrice : Net Unit Price
                                Dim oColAppendSizeNetUnitPrice As DataColumn = New DataColumn("FTNetUnitPrice" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                                oColAppendSizeNetUnitPrice.Caption = "Net Unit Price : " & oRow.Item("FTMatSizeCode").ToString()

                                Me.ogvConfirmImport.Columns.AddField(oColAppendSizeNetUnitPrice.ColumnName)
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).FieldName = oColAppendSizeNetUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).Name = oColAppendSizeNetUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).Caption = oColAppendSizeNetUnitPrice.Caption
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).Visible = True
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).OptionsColumn.AllowEdit = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).OptionsColumn.AllowMove = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeNetUnitPrice.ColumnName).OptionsColumn.AllowSort = False

                                oDBdtStatic.Columns.Add(oColAppendSizeNetUnitPrice.ColumnName, oColAppendSizeNetUnitPrice.DataType)

                            Case 3
                                '...FTTradingCoNetUnitPrice : Trading Co Net Unit Price
                                Dim oColAppendSizeCoNetUnitPrice As DataColumn = New DataColumn("FTTradingCoNetUnitPrice" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                                oColAppendSizeCoNetUnitPrice.Caption = "Trading Co Net Unit Price : " & oRow.Item("FTMatSizeCode").ToString()

                                Me.ogvConfirmImport.Columns.AddField(oColAppendSizeCoNetUnitPrice.ColumnName)
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).FieldName = oColAppendSizeCoNetUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).Name = oColAppendSizeCoNetUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).Caption = oColAppendSizeCoNetUnitPrice.Caption
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).Visible = True
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).OptionsColumn.AllowEdit = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).OptionsColumn.AllowMove = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeCoNetUnitPrice.ColumnName).OptionsColumn.AllowSort = False

                                oDBdtStatic.Columns.Add(oColAppendSizeCoNetUnitPrice.ColumnName, oColAppendSizeCoNetUnitPrice.DataType)

                            Case 4
                                '...FTGrossUnitPrice : Gross Unit Price
                                Dim oColAppendSizeGrossUnitPrice As DataColumn = New DataColumn("FTGrossUnitPrice" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                                oColAppendSizeGrossUnitPrice.Caption = "Gross Unit Price : " & oRow.Item("FTMatSizeCode").ToString()

                                Me.ogvConfirmImport.Columns.AddField(oColAppendSizeGrossUnitPrice.ColumnName)
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).FieldName = oColAppendSizeGrossUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).Name = oColAppendSizeGrossUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).Caption = oColAppendSizeGrossUnitPrice.Caption
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).Visible = True
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).OptionsColumn.AllowEdit = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).OptionsColumn.AllowMove = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeGrossUnitPrice.ColumnName).OptionsColumn.AllowSort = False

                                oDBdtStatic.Columns.Add(oColAppendSizeGrossUnitPrice.ColumnName, oColAppendSizeGrossUnitPrice.DataType)

                            Case 5
                                '...FTTradingCoGrossUnitPrice : Trading Co Gross Unit Price
                                Dim oColAppendSizeTradingCoGrossUnitPrice As DataColumn = New DataColumn("FTTradingCoGrossUnitPrice" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                                oColAppendSizeTradingCoGrossUnitPrice.Caption = "Trading Co Gross Unit Price : " & oRow.Item("FTMatSizeCode").ToString()

                                Me.ogvConfirmImport.Columns.AddField(oColAppendSizeTradingCoGrossUnitPrice.ColumnName)
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).FieldName = oColAppendSizeTradingCoGrossUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).Name = oColAppendSizeTradingCoGrossUnitPrice.ColumnName
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).Caption = oColAppendSizeTradingCoGrossUnitPrice.Caption
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).Visible = True
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).OptionsColumn.AllowEdit = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).OptionsColumn.AllowMove = False
                                Me.ogvConfirmImport.Columns(oColAppendSizeTradingCoGrossUnitPrice.ColumnName).OptionsColumn.AllowSort = DefaultBoolean.False

                                oDBdtStatic.Columns.Add(oColAppendSizeTradingCoGrossUnitPrice.ColumnName, oColAppendSizeTradingCoGrossUnitPrice.DataType)

                            Case Else
                                '...do nothing
                        End Select

                        'Application.DoEvents()

                    Next nLoopCol

                    oDBdtStatic.AcceptChanges()

                Next

                If Not oDBdtDynamic Is Nothing And oDBdtDynamic.Rows.Count > 0 Then
                    '...Append Value Column Size Breakdown (by size sequence master file)
                    Dim nLoopRowImport As Integer

                    For nLoopRowImport = 0 To oDBdtStatic.Rows.Count - 1

                        Try
                            Dim oDataRow() As DataRow

                            Dim nFNRowImport As Integer
                            Dim tFTPOTrading As String
                            Dim tFTPOItem As String
                            Dim tFTStyle As String
                            Dim tFTColorwayCode As String

                            nFNRowImport = oDBdtStatic.Rows(nLoopRowImport).Item("FNRowImport")

                            If Not DBNull.Value.Equals(oDBdtStatic.Rows(nLoopRowImport).Item("FTPOTrading")) Then
                                tFTPOTrading = oDBdtStatic.Rows(nLoopRowImport).Item("FTPOTrading")
                            End If

                            If Not DBNull.Value.Equals(oDBdtStatic.Rows(nLoopRowImport).Item("FTPOItem")) Then
                                tFTPOItem = oDBdtStatic.Rows(nLoopRowImport).Item("FTPOItem")
                            End If

                            If Not DBNull.Value.Equals(oDBdtStatic.Rows(nLoopRowImport).Item("FTStyle")) Then
                                tFTStyle = oDBdtStatic.Rows(nLoopRowImport).Item("FTStyle")
                            End If

                            If Not DBNull.Value.Equals(oDBdtStatic.Rows(nLoopRowImport).Item("FTColorwayCode")) Then
                                tFTColorwayCode = oDBdtStatic.Rows(nLoopRowImport).Item("FTColorwayCode")
                            End If

                            oDataRow = oDBdtDynamic.Select("FNRowImport = " & oDBdtStatic.Rows(nLoopRowImport).Item("FNRowImport") &
                                                           "AND FTPOTrading = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTPOTrading")) & "'" &
                                                           "AND FTPOItem = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTPOItem")) & "'" &
                                                           "AND FTStyle = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTStyle")) & "'" &
                                                           "AND FTColorwayCode = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTColorwayCode")) & "'")

                            If Not oDataRow Is Nothing AndAlso oDataRow.Count > 0 Then

                                For nLoopDataRow As Integer = 0 To oDataRow.Count - 1

                                    If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FTMatSizeCode")) Then
                                        Dim tFTMatSizeCode As String = ""
                                        tFTMatSizeCode = oDataRow(nLoopDataRow).Item("FTMatSizeCode").ToString()

                                        If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FNQuantity")) Then
                                            oDBdtStatic.Rows(nLoopRowImport).Item("FNQuantity" & tFTMatSizeCode) = oDataRow(nLoopDataRow).Item("FNQuantity")
                                        End If

                                        If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FTNetUnitPrice")) Then
                                            oDBdtStatic.Rows(nLoopRowImport).Item("FTNetUnitPrice" & tFTMatSizeCode) = oDataRow(nLoopDataRow).Item("FTNetUnitPrice")
                                        End If

                                        If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FTTradingCoNetUnitPrice")) Then
                                            oDBdtStatic.Rows(nLoopRowImport).Item("FTTradingCoNetUnitPrice" & tFTMatSizeCode) = oDataRow(nLoopDataRow).Item("FTTradingCoNetUnitPrice")
                                        End If

                                        If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FTGrossUnitPrice")) Then
                                            oDBdtStatic.Rows(nLoopRowImport).Item("FTGrossUnitPrice" & tFTMatSizeCode) = oDataRow(nLoopDataRow).Item("FTGrossUnitPrice")
                                        End If

                                        If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FTTradingCoGrossUnitPrice")) Then
                                            oDBdtStatic.Rows(nLoopRowImport).Item("FTTradingCoGrossUnitPrice" & tFTMatSizeCode) = oDataRow(nLoopDataRow).Item("FTTradingCoGrossUnitPrice")
                                        End If

                                    End If

                                    'Application.DoEvents()

                                Next nLoopDataRow

                                oDBdtStatic.AcceptChanges()

                            Else
                                '...do nothing
                            End If

                        Catch ex As Exception
                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End Try

                    Next nLoopRowImport

                End If

            End If

            Me.ogdConfirmImport.DataSource = oDBdtStatic
            Me.ogdConfirmImport.Refresh()
            Me.ogvConfirmImport.RefreshData()
            Me.ogvConfirmImport.BestFitColumns()

            'If Not _bInitialGridBandView Then
            '    Call W_PRCxInitialGridBandView()
            'End If

            Call W_PRCxInitialGridBandView()

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
            'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        End Try

        Return bRet

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

    Private Sub SaveQuantityByStyle(FTPONo As String, StyleCode As String, SeasonCode As String, StyleId As Integer, SeasonId As Integer, _FNImportOrderType As Integer, _FixCmpByProgram As Integer, nFNHSysVenderPramId As Integer, nFNHSysCustId As Integer, nFNHSysCmpRunId As Integer, nFNHSysBuyId As Integer, DataShipDate As String, DataPlant As Integer)
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
            sql &= vbCrLf & "         And A.FDShipDateOrginal ='" & HI.UL.ULF.rpQuoted(DataShipDate) & "' "
            sql &= vbCrLf & "         And ISNULL(A.FNHSysPlantId,0) =" & Val(DataPlant) & " "
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

                        For Each Rx As DataRow In _dtdata.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorwayCode.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakdownCode.ToString) & "' AND FNQuantity-FNCreateData >0 AND FNQuantity>0", "FTCreateDate,FTOrderNo")
                            MainJobRef = Rx!FTOrderNo.ToString
                            Exit For
                        Next
                    End If


                    If MainJobRef <> "" Then

                        BalQty = (Val(R!FNQuantity.ToString) - Val(R!FNCreateData.ToString))

                        '   Do While StateCheck And BalQty > 0

                        For Each Rx As DataRow In _dtdata.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorwayCode.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakdownCode.ToString) & "' AND FNQuantity-FNCreateData >0 AND FNQuantity>0 AND FTOrderNo='" & HI.UL.ULF.rpQuoted(MainJobRef) & "'", "FTCreateDate,FTOrderNo")
                            MainBalQty = (Val(Rx!FNQuantity.ToString) - Val(Rx!FNCreateData.ToString))

                            If MainBalQty > BalQty Then

                                Rx!FNGenOrderData = BalQty
                                Rx!FNCreateData = Val(Rx!FNCreateData.ToString) + BalQty
                                R!FNCreateData = Val(R!FNCreateData.ToString) + BalQty
                                R!FNGenOrderData = BalQty

                                BalQty = 0


                            Else

                                Rx!FNGenOrderData = MainBalQty
                                Rx!FNCreateData = Val(Rx!FNCreateData.ToString) + MainBalQty

                                R!FNCreateData = Val(R!FNCreateData.ToString) + MainBalQty
                                R!FNGenOrderData = MainBalQty

                                BalQty = BalQty - MainBalQty
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

                        For Each Rx As DataRow In _dtdata.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorwayCode.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakdownCode.ToString) & "' ", "FTCreateDate,FTOrderNo")
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

        _DmP = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN), "TMERTOrder", "0", False, "" & Me.FNHSysCmpRunId.Text.Trim().ToString()).ToString()
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
            _Qry &= vbCrLf & "   FNHSysPlantId," & BuyGrpId & " AS FNHSysBuyGrpId," & MainCategoryId & " AS  FNHSysMainCategoryId," & nFNHSysVenderPramId & " AS  FNHSysVenderPramId"
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
            _Qry &= vbCrLf & ", FTOther3Note1, FTRemark, FNHSysShipPortId, FDShipDateOrginal, FTCustRef,FNHSysPlantId,FNHSysBuyGrpId"
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
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & " AS FNHSysUnitId"
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
            _Qry &= vbCrLf & "," & BuyGrpId & " AS FNHSysBuyGrpId"
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
                _Qry &= vbCrLf & "  FNExtraQty, FNQuantityExtra, FNGarmentQtyTest,FTNikePOLineItem,FNPrice,FNPriceOrg)"
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
                _Qry &= vbCrLf & "," & Val(Rx!FNPrice.ToString) & " "
                _Qry &= vbCrLf & "," & Val(Rx!FNPrice.ToString) & " "



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
            '_Qry &= vbCrLf & " FNPrice=BB.FNPrice"
            '_Qry &= vbCrLf & " ,FNPriceOrg=BB.FNPriceOrg"
            _Qry &= vbCrLf & "  FNCMDisPer=BB.FNCMDisPer"
            _Qry &= vbCrLf & " ,FNCMDisAmt= Convert(numeric(18,4),((M.FNPrice * BB.FNCMDisPer) /100.00)) "
            _Qry &= vbCrLf & " ,FNNetPrice = BB.FNCMDisPer - Convert(numeric(18,4),((M.FNPrice * BB.FNCMDisPer) /100.00)) "
            _Qry &= vbCrLf & "  ,FNAmt=Convert(numeric(18,2),(M.FNQuantity * M.FNPrice))"
            _Qry &= vbCrLf & ", FNAmntExtra=0"
            _Qry &= vbCrLf & ", FNGrandAmnt=Convert(numeric(18,2),(M.FNQuantity * M.FNPrice))"
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

            Dim Cmdstring As String = ""

            Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(_DmP) & "'"
            HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

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

            tSql = ""
            tSql = "  Select TOP 1  FTCfgData"
            tSql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig With(NOLOCK) "
            tSql &= vbCrLf & " WHERE  (FTCfgName = N'" & HI.UL.ULF.rpQuoted(FNHSysVenderPramId.Text.Trim()) & "')"
            _FixCmpByProgram = Val(HI.Conn.SQLConn.GetField(tSql, Conn.DB.DataBaseName.DB_SECURITY, "0"))

            Dim _Prefix As String = HI.TL.CboList.GetListRefer("FNOrderType", _FNImportOrderType)

            nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag)
            nFNHSysCmpRunId = Val(Me.FNHSysCmpRunId.Properties.Tag)
            nFNHSysBuyId = Val(Me.FNHSysBuyId.Properties.Tag)

            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysVenderPramId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMVenderPram] AS A WITH(NOLOCK) WHERE A.FNHSysVenderPramId = " & Val(Me.FNHSysVenderPramId.Properties.Tag)

            nFNHSysVenderPramId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

            If nFNHSysVenderPramId <= 0 Then Return False

            Dim oDBdtImport As System.Data.DataTable


            tSql = ""
            tSql &= Environment.NewLine & "     Select  A.FTPONo,A.FTOrderDate,A.FTStyle,A.FTSeason,ST.FNHSysStyleId ,SS.FNHSysSeasonId ,A.FDShipDateOrginal,A.FNHSysPlantId "
            tSql &= Environment.NewLine & "    FROM(SELECT  FTPONo, "
            tSql &= Environment.NewLine & "     FTOrderDate "
            tSql &= Environment.NewLine & "   , FTStyle "
            tSql &= Environment.NewLine & "   , FTPlanningSeason+ RIGHT(FTYear,2) As FTSeason,FDShipDateOrginal,FNHSysPlantId "
            tSql &= Environment.NewLine & "    From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp As A "
            tSql &= Environment.NewLine & "    Where (FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
            tSql &= Environment.NewLine & "    Group By FTPONo, "
            tSql &= Environment.NewLine & "    FTOrderDate "
            tSql &= Environment.NewLine & "   	, FTStyle,FDShipDateOrginal,FNHSysPlantId "
            tSql &= Environment.NewLine & "   , FTPlanningSeason+ RIGHT(FTYear,2) ) As A "
            tSql &= Environment.NewLine & "   INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle As ST With(NOLOCK) On  A.FTStyle = ST.FTStyleCode "
            tSql &= Environment.NewLine & "   	INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMSeason As SS With(NOLOCK) On  A.FTSeason = SS.FTSeasonCode  "


            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then

                For Each R As DataRow In oDBdtImport.Rows

                    Call SaveQuantityByStyle(R!FTPONo.ToString, R!FTStyle.ToString, R!FTSeason.ToString, Val(R!FNHSysStyleId.ToString), Val(R!FNHSysSeasonId.ToString), _FNImportOrderType, _FixCmpByProgram, nFNHSysVenderPramId, nFNHSysCustId, nFNHSysCmpRunId, nFNHSysBuyId, R!FDShipDateOrginal.ToString, Val(R!FNHSysPlantId.ToString()))

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
        Me.FNHSysCustId.Focus()
    End Sub

    Private Sub wImportOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True

        If System.Diagnostics.Debugger.IsAttached = True Then
            Me.otcImportOrderNo.TabPages.Item(1).PageVisible = True
        Else
            Me.otcImportOrderNo.TabPages.Item(1).PageVisible = False
        End If

        oGridViewConfirmImport = New DevExpress.XtraGrid.Views.Grid.GridView()
        oGridViewConfirmImport = ogdConfirmImport.Views(0)

        ogdConfirmImport.ViewCollection.Add(oGridViewConfirmImport)
        ogdConfirmImport.MainView = oGridViewConfirmImport

        oGridViewConfirmImport.GridControl = ogdConfirmImport

        Me.ogdConfirmImport.DataSource = Nothing
        Me.ogdConfirmImport.Refresh()

        oGridViewConfirmImport.OptionsView.ShowAutoFilterRow = True
        oGridViewConfirmImport.OptionsBehavior.AllowAddRows = DefaultBoolean.False
        oGridViewConfirmImport.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False
        oGridViewConfirmImport.OptionsBehavior.Editable = False

        Me.oGridViewConfirmImport.RefreshData()
        Me.oGridViewConfirmImport.BestFitColumns()

        Me.oGridViewConfirmImport.OptionsView.ColumnAutoWidth = False
        Me.oGridViewConfirmImport.RefreshData()

        FNHSysUnitId.Text = "PCS"
    End Sub

    Private Sub ocmReadExcel_Click_REM_20140605_PM1745(sender As Object, e As EventArgs) Handles ocmTestFile.Click
        Exit Sub
        Try
            Dim oDBdtExcel As System.Data.DataTable

            If Me.FTFilePath.Text.Trim() <> "" Then

                Dim objCulture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US", True)

                objCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                objCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
                System.Threading.Thread.CurrentThread.CurrentCulture = objCulture

                If IO.File.Exists(Me.FTFilePath.Text.Trim().ToString()) Then

                    _bCatchSrcFile = False

                    REM Dim _Spls As HI.TL.SplashScreen
                    'Dim tSplashText As String

                    Select Case HI.ST.Lang.Language
                        Case HI.ST.Lang.eLang.TH
                            _tSplashText = "กรุณารอสักครู่.....กำลังดำเนินการอ่านข้อมูล"
                        Case Else
                            _tSplashText = "Read data.....please wait"
                    End Select

                    _oSplash = New HI.TL.SplashScreen(_tSplashText)

                    'Dim nLoop As Integer
                    'For nLoop = 1 To 9999999
                    '    _oSplash.UpdateInformation(_tSplashText & " " & nLoop)
                    'Next

                    REM _Spls = New HI.TL.SplashScreen(tSplashText & " " & String.Format(" {0}", Me.Span_lbl.Text))

                    Call W_PRCbLoadMapSize()

                    Dim _FilePath As String
                    _FilePath = Me.FTFilePath.Text.Trim()

                    REM 2014/06/05 Dim oDBdtExcel As System.Data.DataTable

                    Dim tFileExtension As String

                    tFileExtension = IO.Path.GetExtension(_FilePath)

                    REM 2014/06/05
                    '------------------------------------------------------------------------------
                    'Select Case tFileExtension.ToLower()
                    '    Case ".xls", ".xlsx"
                    '        oDBdtExcel = W_GETdtReadExcelData(_FilePath, tFileExtension)
                    '        oDBdtExcel = W_PRCdtReadExcelIntoDataTable(_FilePath, "Sheet1")
                    '        oDBdtExcel = W_GETdtReadExcelData(_FilePath, tFileExtension).Copy()
                    '    Case ".csv"
                    '        oDBdtExcel = W_GETdtCSVToDataTable(_FilePath, False).Copy()
                    'End Select
                    '------------------------------------------------------------------------------

                    Application.DoEvents()

                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From Excel File.......")

                    oDBdtExcel = HI.UL.ReadExcel.Read(_FilePath, "Sheet1", -1)

                    Me.ogdImportOrder.DataSource = oDBdtExcel
                    Me.ogdImportOrder.Refresh()
                    Me.ogvImportOrder.RefreshData()
                    Me.ogvImportOrder.OptionsView.ColumnAutoWidth = False
                    Me.ogvImportOrder.OptionsBehavior.AllowAddRows = DefaultBoolean.False

                    If Not Me.ogdImportOrder.DataSource Is Nothing Then

                        For nLoopColGridView As Integer = 0 To ogvImportOrder.Columns.Count - 1
                            Application.DoEvents()

                            With Me.ogvImportOrder
                                .Columns(nLoopColGridView).OptionsColumn.AllowMove = False
                                .Columns(nLoopColGridView).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .Columns(nLoopColGridView).SortOrder = DevExpress.Data.ColumnSortOrder.None
                                .Columns(nLoopColGridView).OptionsColumn.AllowEdit = False
                            End With

                        Next nLoopColGridView

                        'F8 : PO Create Date, F9 : Document Date, F41 : OGAC Date, F42 : GAC Date
                        Dim nLoopValidateDate As Integer
                        Dim tDateValImport As String = ""
                        For nLoopValidateDate = _nStartRowImportExcel To oDBdtExcel.Rows.Count - 1
                            Application.DoEvents()
                            _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเกี่ยวกับวันที่...")

                            '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเกี่ยวกับวันที่...")

                            Dim nLoopCol As Integer
                            For nLoopCol = 0 To oDBdtExcel.Columns.Count - 1

                                Select Case oDBdtExcel.Columns(nLoopCol).ColumnName
                                    Case "F8", "F9", "F41", "F42"
                                        '...MM/dd/yyyy
                                        Try
                                            REM tDateValImport = HI.UL.ULDate.ConvertEN(oDBdtExcel.Rows(nLoopValidateDate).Item(nLoopCol)) '(CDate(oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString())), DateFormat.ShortDate)
                                            tDateValImport = FormatDateTime(CDate(oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString())), DateFormat.ShortDate)
                                        Catch ex As Exception
                                            'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            tDateValImport = oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()).ToString()
                                            REM oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()) = Microsoft.VisualBasic.Mid$(tDateValImport, 4, 3) & Microsoft.VisualBasic.Mid$(tDateValImport, 1, 3) & Microsoft.VisualBasic.Mid$(tDateValImport, 7, 4)

                                        End Try

                                        oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()) = Microsoft.VisualBasic.Mid$(tDateValImport, 4, 3) & Microsoft.VisualBasic.Mid$(tDateValImport, 1, 3) & Microsoft.VisualBasic.Mid$(tDateValImport, 7, 4)

                                        REM 2014/06/04
                                        'If nLoopValidateDate <= 5 Then
                                        '    If Len(tDateValImport) < 10 Then
                                        '        MsgBox("tDateValImport : " & tDateValImport, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        '    End If
                                        '    MsgBox("Lenght Date Val is :" & Len(tDateValImport) & Environment.NewLine & "Date Before Split : " & tDateValImport & Environment.NewLine & "Date After Split : " & Microsoft.VisualBasic.Mid$(tDateValImport, 4, 3) & Microsoft.VisualBasic.Mid$(tDateValImport, 1, 3) & Microsoft.VisualBasic.Mid$(tDateValImport, 7, 4), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        'End If


                                        REM 2014/06/04
                                        'If HI.UL.ULDate.CheckDate(oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString())) = "" Then
                                        '    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
                                        '    Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"

                                        '    REM 2014/06/03 oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName)) = HI.UL.ULDate.ConvertEN(oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName))

                                        '    Try
                                        '        Dim str As String = oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName)
                                        '        Dim dDate As DateTime = DateTime.Parse(str, CultureInfo.InvariantCulture)
                                        '        Dim strDayFirst As String = Format(dDate, "dd/MM/yyyy")
                                        '        oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()) = strDayFirst
                                        '    Catch ex As Exception
                                        '        oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()) = HI.UL.ULDate.ConvertEN(oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()))
                                        '    End Try

                                        'Else
                                        '    Try
                                        '        Dim str As String = oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName)
                                        '        Dim dDate As DateTime = DateTime.Parse(str, CultureInfo.InvariantCulture)
                                        '        Dim strDayFirst As String = Format(dDate, "dd/MM/yyyy")
                                        '        oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()) = strDayFirst
                                        '    Catch ex As Exception
                                        '        oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()) = HI.UL.ULDate.ConvertEN(oDBdtExcel.Rows(nLoopValidateDate).Item(oDBdtExcel.Columns(nLoopCol).ColumnName.ToString()))
                                        '    End Try

                                        'End If

                                    Case Else
                                        'do nothing
                                End Select

                            Next nLoopCol

                        Next nLoopValidateDate

                        oDBdtExcel.AcceptChanges()

                        If Not oDBdtExcel Is Nothing And oDBdtExcel.Rows.Count > 0 Then

                            Application.DoEvents()
                            _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเกี่ยวกับ Mer, Buy Group And Plant ")

                            If Not W_PRCbValidateMatchMerchanTeam(oDBdtExcel) Then

                                Select Case HI.ST.Lang.Language

                                    Case HI.ST.Lang.eLang.TH
                                        MsgBox("การกำหนดข้อมูลทีมเมอร์แชนไดเซอร์บางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)

                                    Case Else
                                        MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                End Select

                            ElseIf Not W_PRCbValidateMatchBuyGroup(oDBdtExcel) Then

                                Select Case HI.ST.Lang.Language

                                    Case HI.ST.Lang.eLang.TH
                                        MsgBox("การกำหนดข้อมูลกลุ่มการซื้อบางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการกลุ่มการซื้อ !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)

                                    Case Else
                                        MsgBox("Invalid Buy Group " & Environment.NewLine & "Or Not provide for buy group master file !!!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                End Select

                            ElseIf Not W_PRCbValidateMatchPlant(oDBdtExcel) Then
                                '...รายการแฟ้มข้อมูลโรงงานลูกค้า
                                Select Case HI.ST.Lang.Language
                                    Case HI.ST.Lang.eLang.TH
                                        MsgBox("การกำหนดข้อมูลหลักรายการโรงงานลูกค้าของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการโรงงานลูกค้า !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                    Case Else
                                        MsgBox("Invalid Plant " & Environment.NewLine & "Or Not provide for plant master file !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                End Select

                            Else

                                _bCatchSrcFile = True

                                Application.DoEvents()

                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูล Master ")

                                Call W_PRCxValidateMaster(oDBdtExcel)

                                If _oSplash.TopMost = False Then
                                    _oSplash.TopMost = True
                                    _oSplash.Refresh()
                                End If

                                '_oSplash.Close()
                                'Exit Sub

                                Application.DoEvents()

                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Load Data To Temp ")

                                If W_PRCbSaveListOrderToTemp(oDBdtExcel) = True Then
                                    '...update FNPrice ==> FTOrderNo, FTSubOrderNo, Colorway/Size Breakdown HITECH_MERCHAN..TMERTImportOrderSizeBreakdownTemp
                                    tSql = ""
                                    tSql = "DECLARE @FTDollarSign NVARCHAR(1);"
                                    tSql &= Environment.NewLine & "SET @FTDollarSign = N'$';"
                                    tSql &= Environment.NewLine & "UPDATE A"
                                    tSql &= Environment.NewLine & "SET A.FNPrice =CASE WHEN ISNUMERIC((REPLACE(A.FTTradingCoNetUnitPrice,@FTDollarSign,'')))  = 1 THEN CONVERT(NUMERIC(18, 4), (REPLACE(A.FTTradingCoNetUnitPrice,@FTDollarSign,''))) ELSE 0 END "
                                    'tSql &= Environment.NewLine & "SET A.FNPrice = (CASE WHEN SUBSTRING(A.FTGrossUnitPrice, 1, 1) = @FTDollarSign THEN CONVERT(NUMERIC(15, 4), SUBSTRING(A.FTGrossUnitPrice, 2, LEN(A.FTGrossUnitPrice)))"
                                    'tSql &= Environment.NewLine & " 												                              ELSE CASE WHEN SUBSTRING(A.FTTradingCoGrossUnitPrice, 1, 1) = @FTDollarSign THEN  CONVERT(NUMERIC(15, 4), SUBSTRING(A.FTTradingCoGrossUnitPrice, 2, LEN(A.FTTradingCoGrossUnitPrice)))"
                                    'tSql &= Environment.NewLine & "																															                                  ELSE CASE WHEN SUBSTRING(A.FTNetUnitPrice, 1, 1) = @FTDollarSign THEN CONVERT(NUMERIC(15,4), SUBSTRING(A.FTNetUnitPrice, 2, LEN(A.FTNetUnitPrice)))"
                                    'tSql &= Environment.NewLine & "																																																                               ELSE CASE WHEN SUBSTRING(A.FTTradingCoNetUnitPrice, 1, 1) = @FTDollarSign THEN CONVERT(NUMERIC(15, 4), SUBSTRING(A.FTTradingCoNetUnitPrice, 2, LEN(A.FTTradingCoNetUnitPrice)))"
                                    'tSql &= Environment.NewLine & " 																																																                                                                                                     ELSE 0 END END END END)"
                                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                                    tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FNQuantity > 0;"

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        'MsgBox("EXECUTE DATA COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    Else
                                        'MsgBox("EXECUTE DATA NOT COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End If

                                    Call W_PRCbShowBrowseDataImportFactoryOrder()

                                End If

                            End If

                        Else
                            Select Case HI.ST.Lang.Language

                                Case HI.ST.Lang.eLang.TH
                                    MsgBox("ไม่พบข้อมูล...worksheet 1 !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                Case Else
                                    MsgBox("Data not found...worksheet 1 !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                            End Select
                        End If

                    Else
                        Me.ogdImportOrder.DataSource = Nothing
                        Me.ogdImportOrder.Refresh()
                        Me.ogvImportOrder.RefreshData()
                    End If

                    _oSplash.Close()

                Else
                    MsgBox("Unable to locate source file : " & Me.FTFilePath.Text.ToString(), vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                    Me.ogdImportOrder.DataSource = Nothing
                    Me.ogvImportOrder.RefreshData()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTFilePath_lbl.Text)
                Me.ogdImportOrder.DataSource = Nothing
                Me.ogvImportOrder.RefreshData()
                Me.FTFilePath.Focus()
            End If

        Catch ex As Exception

            _oSplash.Close()

            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
        End Try

    End Sub

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try

            If Me.FNHSysUnitId.Text <= "" Or Me.FNHSysUnitId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysUnitId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysVenderPramId.Text <= "" Or Me.FNHSysVenderPramId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysVenderPramId_lbl.Text)
                Exit Sub
            End If

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

                    oSpreadSheetCtrl.LoadDocument(_FilePath)

                    If Not oSpreadSheetCtrl Is Nothing Then

                        oWorkSheetItem = oSpreadSheetCtrl.Document.Worksheets

                        Dim bValidateSheetName As Boolean = False

                        For Each oListWorkSheet As DevExpress.Spreadsheet.Worksheet In oWorkSheetItem
                            '...default sheet1 for automatic import factory order no.
                            If oListWorkSheet.Name.ToString.ToUpper = "Sheet1".ToUpper Then
                                bValidateSheetName = True
                                Exit For
                            Else
                                'If System.Diagnostics.Debugger.IsAttached = True Then
                                '    MsgBox("Sheet Name : " + oListWorkSheet.Name.ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                'End If

                            End If

                        Next

                        If Not bValidateSheetName Then
                            HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            'If System.Diagnostics.Debugger.IsAttached = True Then
                            '    MsgBox("Work Sheet Name for automatic import factory order is : {Sheet1}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                            'End If

                        End If

                    Else
                        HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    '=====================================================================================================================================================================================================================

                    Select Case HI.ST.Lang.Language

                        Case HI.ST.Lang.eLang.TH
                            _tSplashText = "กรุณารอสักครู่.....กำลังดำเนินการอ่านข้อมูล"
                        Case Else
                            _tSplashText = "Read data.....please wait"
                    End Select

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        'Me.ogbmainprocbutton.Enabled = False
                        Me.Cursor = Cursors.WaitCursor
                        Me.ogbHeader.Enabled = False
                        Me.ogbBrowseFile.Enabled = False
                        Me.ogbViewDetail.Enabled = False
                    End If

                    _oSplash = New HI.TL.SplashScreen(_tSplashText, "", True)

                    Application.DoEvents()

                    Dim _FoundPrm As Boolean = False '...กำหนดโครงการ Program HIT/HIG/HIP...

                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From Excel File.......")

                    With ogvConfirmImport
                        For I As Integer = .Columns.Count - 1 To 0 Step -1
                            Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                                Case "oCol".ToUpper
                                Case Else
                                    ' MsgBox("Column Field Name : " + .Columns(I).FieldName & "Column Name : " & .Columns(I).Name, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                                    .Columns.Remove(.Columns(I))
                            End Select

                        Next

                    End With

                    Call W_PRCbLoadMapSize()

                    oDBdtExcel = HI.UL.ReadExcel.Read(_FilePath, "Sheet1", -1)

                    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    oDBdtExcel.BeginInit()
                    For Each Rx As DataRow In oDBdtExcel.Select("F1 = 'Overall Result' ")
                        Rx.Delete()
                    Next
                    oDBdtExcel.EndInit()

                    'Overall Result'
                    If oDBdtExcel.Select("F1='" & HI.UL.ULF.rpQuoted(Me.FNHSysVenderPramId.Text) & "'").Length > 0 Then
                        _FoundPrm = True

                        If oDBdtExcel.Rows.Count > 3 Then
                            oDBdtExcel.Rows(0)!F1 = Me.FNHSysVenderPramId.Text
                            oDBdtExcel.Rows(1)!F1 = Me.FNHSysVenderPramId.Text
                            oDBdtExcel.Rows(2)!F1 = Me.FNHSysVenderPramId.Text
                        End If

                        oDBdtExcel.BeginInit()
                        For Each Rx As DataRow In oDBdtExcel.Select("F1<>'" & HI.UL.ULF.rpQuoted(Me.FNHSysVenderPramId.Text) & "'")
                            Rx.Delete()
                        Next
                        oDBdtExcel.EndInit()

                        Dim tRowDelimeterOverAllResult As String = ""

                        If Not DBNull.Value.Equals(oDBdtExcel.Rows(oDBdtExcel.Rows.Count - 1).Item(0)) Then
                            tRowDelimeterOverAllResult = oDBdtExcel.Rows(oDBdtExcel.Rows.Count - 1).Item(0).ToString.Trim()
                        End If

                        If tRowDelimeterOverAllResult Like _tTextRowDelimeter.Split("|")(0) Or tRowDelimeterOverAllResult Like _tTextRowDelimeter.Split("|")(1) Then
                            oDBdtExcel.Rows.RemoveAt(oDBdtExcel.Rows.Count - 1)
                        End If

                        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        Me.ogdImportOrder.DataSource = oDBdtExcel
                        Me.ogdImportOrder.Refresh()
                        Me.ogvImportOrder.RefreshData()
                        Me.ogvImportOrder.OptionsView.ColumnAutoWidth = False
                        Me.ogvImportOrder.OptionsBehavior.AllowAddRows = DefaultBoolean.False

                        If Not Me.ogdImportOrder.DataSource Is Nothing Then

                            For nLoopColGridView As Integer = 0 To ogvImportOrder.Columns.Count - 1

                                Application.DoEvents()

                                With Me.ogvImportOrder
                                    .Columns(nLoopColGridView).OptionsColumn.AllowMove = False
                                    .Columns(nLoopColGridView).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .Columns(nLoopColGridView).SortOrder = DevExpress.Data.ColumnSortOrder.None
                                    .Columns(nLoopColGridView).OptionsColumn.AllowEdit = False
                                End With

                            Next nLoopColGridView

                            If Not oDBdtExcel Is Nothing And oDBdtExcel.Rows.Count > 0 Then

                                Application.DoEvents()

                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเกี่ยวกับ Merchandiser Team, Buy Group And Plant ")
                                Call CheckCurrencyImport(oDBdtExcel)
                                If Not W_PRCbValidateMatchMerchanTeam(oDBdtExcel) Then

                                    'Select Case HI.ST.Lang.Language

                                    '    Case HI.ST.Lang.eLang.TH
                                    '        MsgBox("การกำหนดข้อมูลทีมเมอร์แชนไดเซอร์บางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                    '    Case Else
                                    '        MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                    'End Select

                                ElseIf Not W_PRCbValidateMatchBuyGroup(oDBdtExcel) Then

                                    'Select Case HI.ST.Lang.Language

                                    '    Case HI.ST.Lang.eLang.TH
                                    '        MsgBox("การกำหนดข้อมูลกลุ่มการซื้อบางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการกลุ่มการซื้อ !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                    '    Case Else
                                    '        MsgBox("Invalid Buy Group " & Environment.NewLine & "Or Not provide for buy group master file !!!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                    'End Select

                                ElseIf Not W_PRCbValidateMatchPlant(oDBdtExcel) Then

                                    '...รายการแฟ้มข้อมูลโรงงานลูกค้า
                                    Select Case HI.ST.Lang.Language

                                        Case HI.ST.Lang.eLang.TH
                                            MsgBox("การกำหนดข้อมูลหลักรายการโรงงานลูกค้าของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการโรงงานลูกค้า !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                        Case Else
                                            MsgBox("Invalid Plant " & Environment.NewLine & "Or Not provide for plant master file !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                    End Select
                                ElseIf Not W_PRCbValidateMatchCurrency(oDBdtExcel) Then
                                Else

                                    _bCatchSrcFile = True

                                    Application.DoEvents()

                                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูล Master กรุณารอสักครู่...")

                                    Call W_PRCxValidateMaster(oDBdtExcel)

                                    Application.DoEvents()

                                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Load Data To Temp ")

                                    Application.DoEvents()

                                    If W_PRCbSaveListOrderToTemp(oDBdtExcel) = True Then
                                        '...update FNPrice ==> FTOrderNo, FTSubOrderNo, Colorway/Size Breakdown HITECH_MERCHAN..TMERTImportOrderSizeBreakdownTemp
                                        'tSql = ""
                                        'tSql = "DECLARE @FTDollarSign NVARCHAR(1);"
                                        'tSql &= Environment.NewLine & "SET @FTDollarSign = N'$';"
                                        'tSql &= Environment.NewLine & "UPDATE A"
                                        'tSql &= Environment.NewLine & "SET A.FNPrice = (CASE WHEN SUBSTRING(A.FTGrossUnitPrice, 1, 1) = @FTDollarSign THEN CONVERT(NUMERIC(15, 4), SUBSTRING(A.FTGrossUnitPrice, 2, LEN(A.FTGrossUnitPrice)))"
                                        'tSql &= Environment.NewLine & " 												                              ELSE CASE WHEN SUBSTRING(A.FTTradingCoGrossUnitPrice, 1, 1) = @FTDollarSign THEN  CONVERT(NUMERIC(15, 4), SUBSTRING(A.FTTradingCoGrossUnitPrice, 2, LEN(A.FTTradingCoGrossUnitPrice)))"
                                        'tSql &= Environment.NewLine & "																															                                  ELSE CASE WHEN SUBSTRING(A.FTNetUnitPrice, 1, 1) = @FTDollarSign THEN CONVERT(NUMERIC(15,4), SUBSTRING(A.FTNetUnitPrice, 2, LEN(A.FTNetUnitPrice)))"
                                        'tSql &= Environment.NewLine & "																																																                               ELSE CASE WHEN SUBSTRING(A.FTTradingCoNetUnitPrice, 1, 1) = @FTDollarSign THEN CONVERT(NUMERIC(15, 4), SUBSTRING(A.FTTradingCoNetUnitPrice, 2, LEN(A.FTTradingCoNetUnitPrice)))"
                                        'tSql &= Environment.NewLine & " 																																																                                                                                                     ELSE 0 END END END END)"
                                        'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                                        'tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        'tSql &= Environment.NewLine & "      AND A.FNQuantity > 0;"

                                        'If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        '    'MsgBox("EXECUTE DATA COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        'Else
                                        '    'MsgBox("EXECUTE DATA NOT COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        'End If

                                        '...update FNPrice ==> FTOrderNo, FTSubOrderNo, Colorway/Size Breakdown HITECH_MERCHAN..TMERTImportOrderSizeBreakdownTemp

                                        REM 2015/03/31
                                        '================================================================================================================================================================================================================================
                                        'tSql = ""
                                        'tSql = "DECLARE @FTDollarSign NVARCHAR(1);"
                                        'tSql &= Environment.NewLine & "SET @FTDollarSign = N'$';"
                                        'tSql &= Environment.NewLine & "UPDATE A"
                                        'tSql &= Environment.NewLine & "SET A.FNPrice = CASE WHEN ISNUMERIC((REPLACE(A.FTTradingCoNetUnitPrice,@FTDollarSign,'')))  = 1 THEN CONVERT(NUMERIC(18, 4), (REPLACE(A.FTTradingCoNetUnitPrice, @FTDollarSign,''))) ELSE 0 END "

                                        'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                                        'tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        'tSql &= Environment.NewLine & "      AND A.FNQuantity > 0;"
                                        '=================================================================================================================================================================================================================================

                                        '...modify by Tid 2015/03/31
                                        Dim oStrBuilder As New System.Text.StringBuilder()

                                        'oStrBuilder.Remove(0, oStrBuilder.Length)

                                        'oStrBuilder.AppendLine("DECLARE @FTDollarSign NVARCHAR(1);")
                                        'oStrBuilder.AppendLine("SET @FTDollarSign = N'$';")
                                        'oStrBuilder.AppendLine("UPDATE A")
                                        'oStrBuilder.AppendLine("SET A.FNPrice = ISNULL((CASE WHEN B.FTGrossUnitPrice <> '0'")
                                        'oStrBuilder.AppendLine("	                              AND SUBSTRING(B.FTGrossUnitPrice, 1, 1) = @FTDollarSign")
                                        'oStrBuilder.AppendLine("				                  AND ISNUMERIC((REPLACE(B.FTGrossUnitPrice, @FTDollarSign,''))) = 1")
                                        'oStrBuilder.AppendLine("				                  AND CONVERT(NUMERIC(18, 4), (REPLACE(B.FTGrossUnitPrice, @FTDollarSign,''))) > 0")
                                        'oStrBuilder.AppendLine("		                     THEN CONVERT(NUMERIC(18, 4), (REPLACE(B.FTGrossUnitPrice, @FTDollarSign,'')))")
                                        'oStrBuilder.AppendLine("	                         ELSE CASE WHEN B.FTTradingCoGrossUnitPrice <> '0'")
                                        'oStrBuilder.AppendLine("				                            AND SUBSTRING(B.FTTradingCoGrossUnitPrice, 1, 1) = @FTDollarSign")
                                        'oStrBuilder.AppendLine("						                    AND ISNUMERIC((REPLACE(B.FTTradingCoGrossUnitPrice, @FTDollarSign,''))) = 1")
                                        'oStrBuilder.AppendLine("						                    AND CONVERT(NUMERIC(18, 4), (REPLACE(B.FTTradingCoGrossUnitPrice, @FTDollarSign,''))) > 0")
                                        'oStrBuilder.AppendLine("					                   THEN CONVERT(NUMERIC(18, 4), (REPLACE(B.FTTradingCoGrossUnitPrice, @FTDollarSign,'')))")
                                        'oStrBuilder.AppendLine("			                           ELSE CASE WHEN B.FTNetUnitPrice <> '0'")
                                        'oStrBuilder.AppendLine("					                                  AND SUBSTRING(B.FTNetUnitPrice, 1, 1) = @FTDollarSign")
                                        'oStrBuilder.AppendLine("									                  AND ISNUMERIC((REPLACE(B.FTNetUnitPrice, @FTDollarSign,''))) = 1")
                                        'oStrBuilder.AppendLine("									                  AND CONVERT(NUMERIC(18, 4), (REPLACE(B.FTNetUnitPrice, @FTDollarSign,''))) > 0")
                                        'oStrBuilder.AppendLine("							                     THEN CONVERT(NUMERIC(18, 4), (REPLACE(B.FTNetUnitPrice, @FTDollarSign,'')))")
                                        'oStrBuilder.AppendLine("								                 ELSE CASE WHEN B.FTTradingCoNetUnitPrice <> '0'")
                                        'oStrBuilder.AppendLine("								                                AND SUBSTRING(B.FTTradingCoNetUnitPrice, 1, 1) = @FTDollarSign")
                                        'oStrBuilder.AppendLine("											                    AND ISNUMERIC((REPLACE(B.FTTradingCoNetUnitPrice, @FTDollarSign, ''))) = 1")
                                        'oStrBuilder.AppendLine("											                    AND CONVERT(NUMERIC(18, 4), (REPLACE(B.FTTradingCoNetUnitPrice, @FTDollarSign,''))) > 0")
                                        'oStrBuilder.AppendLine("										                   THEN CONVERT(NUMERIC(18, 4), (REPLACE(B.FTTradingCoNetUnitPrice, @FTDollarSign,'')))")
                                        'oStrBuilder.AppendLine("										                   ELSE 0 END END END END), 0)")
                                        'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp AS A (NOLOCK) LEFT JOIN (SELECT A.FTUserLogin, A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem, A.FTStyle, A.FTColorwayCode, A.FTSizeBreakdownCode,")
                                        'oStrBuilder.AppendLine("                                                                                       CASE WHEN A.FTGrossUnitPrice IS NOT NULL AND SUBSTRING(A.FTGrossUnitPrice, 1, 1) = @FTDollarSign THEN A.FTGrossUnitPrice ELSE '0' END AS FTGrossUnitPrice,")
                                        'oStrBuilder.AppendLine("																					   CASE WHEN A.FTTradingCoGrossUnitPrice IS NOT NULL AND SUBSTRING(A.FTTradingCoGrossUnitPrice, 1, 1) = @FTDollarSign THEN A.FTTradingCoGrossUnitPrice ELSE '0' END AS FTTradingCoGrossUnitPrice,")
                                        'oStrBuilder.AppendLine("																					   CASE WHEN A.FTNetUnitPrice IS NOT NULL AND SUBSTRING(A.FTNetUnitPrice, 1, 1) = @FTDollarSign THEN A.FTNetUnitPrice ELSE '0' END AS FTNetUnitPrice,")
                                        'oStrBuilder.AppendLine("																					   CASE WHEN A.FTTradingCoNetUnitPrice IS NOT NULL AND SUBSTRING(A.FTTradingCoNetUnitPrice, 1, 1) = @FTDollarSign THEN A.FTTradingCoNetUnitPrice ELSE '0' END AS FTTradingCoNetUnitPrice")
                                        'oStrBuilder.AppendLine("																				  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp AS A (NOLOCK)")
                                        'oStrBuilder.AppendLine("																				  WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
                                        'oStrBuilder.AppendLine("                                                                                  ) AS B ON A.FTUserLogin = B.FTUserLogin")
                                        'oStrBuilder.AppendLine("																			             AND A.FNRowImport = B.FNRowImport")
                                        'oStrBuilder.AppendLine("																						 AND A.FTPONo = B.FTPONo")
                                        'oStrBuilder.AppendLine("																						 AND A.FTPOItem = B.FTPOItem")
                                        'oStrBuilder.AppendLine("																						 AND A.FTStyle = B.FTStyle")
                                        'oStrBuilder.AppendLine("																						 AND A.FTColorwayCode = B.FTColorwayCode")
                                        'oStrBuilder.AppendLine("																						 AND A.FTSizeBreakdownCode = B.FTSizeBreakdownCode")
                                        'oStrBuilder.AppendLine("WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
                                        'oStrBuilder.AppendLine("      AND A.FNQuantity > 0;")

                                        'If oStrBuilder.Length > 0 Then
                                        '    tSql = ""
                                        '    tSql = oStrBuilder.ToString()
                                        'End If

                                        tSql = " UPDATE XA SET FNGrpSeq = XX.FNGrpSeq"
                                        tSql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp AS XA INNER JOIN"
                                        tSql &= vbCrLf & " 	(SELECT A.FTPONo"
                                        tSql &= vbCrLf & "  , A.FTPOItem"
                                        tSql &= vbCrLf & "   , A.FDShipDate"
                                        tSql &= vbCrLf & "   ,A.FTStyle "
                                        tSql &= vbCrLf & "   , B.FTColorwayCode"
                                        tSql &= vbCrLf & "   ,ROW_NUMBER() Over (PARTITION BY A.FTPONo,A.FTStyle ,A.FDShipDate,A.FNHSysBuyGrpId,A.FNHSysPlantId,B.FTColorwayCode ORDER BY A.FTPOItem) AS FNGrpSeq"
                                        tSql &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp AS A WITH (NOLOCK) INNER JOIN"
                                        tSql &= vbCrLf & " 	(SELECT  FTPONo,FTStyle ,FTShipDate,FTColorwayCode,FTPOItem "
                                        tSql &= vbCrLf & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp WITH (NOLOCK) "
                                        tSql &= vbCrLf & " WHERE FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        tSql &= vbCrLf & " GROUP BY FTPONo,FTStyle ,FTShipDate,FTColorwayCode,FTPOItem "
                                        tSql &= vbCrLf & " 	)AS B ON  A.FTPONo = B.FTPONo AND A.FTPOItem = B.FTPOItem AND "
                                        tSql &= vbCrLf & "     A.FDShipDate = B.FTShipDate And A.FTStyle = B.FTStyle"
                                        tSql &= vbCrLf & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        tSql &= vbCrLf & "  ) AS XX ON XA.FTPONo = XX.FTPONo  "
                                        tSql &= vbCrLf & " AND  XA.FTPOItem = XX.FTPOItem "
                                        tSql &= vbCrLf & " AND XA.FDShipDate = XX.FDShipDate "
                                        tSql &= vbCrLf & "  AND XA.FTStyle = XX.FTStyle"
                                        tSql &= vbCrLf & " WHERE XA.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                                        tSql = "UPDATE A SET FNPrice = CASE WHEN ISNUMERIC(FTTradingCoGrossUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTTradingCoGrossUnitPrice)  "
                                        tSql &= vbCrLf & "                  WHEN ISNUMERIC(FTGrossUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTGrossUnitPrice)  "
                                        tSql &= vbCrLf & "                  WHEN ISNUMERIC(FTTradingCoNetUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTTradingCoNetUnitPrice)  "
                                        tSql &= vbCrLf & "                  WHEN ISNUMERIC(FTNetUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTNetUnitPrice)  ELSE 0 END "
                                        tSql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp AS A "
                                        tSql &= vbCrLf & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        tSql &= vbCrLf & "       AND A.FNQuantity > 0 "

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                            'If System.Diagnostics.Debugger.IsAttached = True Then
                                            '    MsgBox("EXECUTE DATA COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            'End If
                                        Else
                                            'If System.Diagnostics.Debugger.IsAttached = True Then
                                            '    MsgBox("EXECUTE DATA NOT COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            'End If
                                        End If

                                        Application.DoEvents()

                                        If CheckOrderQtyZero() = False Then
                                            Call W_PRCbShowBrowseDataImportFactoryOrder()
                                        Else
                                            '...clear source before import order temp
                                            tSql = ""
                                            tSql = "DELETE A"
                                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                            End If

                                            tSql = ""
                                            tSql = "DELETE A"
                                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                            End If

                                            _oSplash.Close()

                                            If System.Diagnostics.Debugger.IsAttached = True Then
                                                'Me.ogbmainprocbutton.Enabled = True
                                                Me.Cursor = Cursors.Default
                                                Me.ogbHeader.Enabled = True
                                                Me.ogbBrowseFile.Enabled = True
                                                Me.ogbViewDetail.Enabled = True
                                            End If

                                            HI.MG.ShowMsg.mInfo("พบข้อมูลจำนวน Order เป็น 0 ไม่สามารถทำการ โหลด File ได้ กรูณาทำการตรวจสอบยอด Order !!!", 1407010001, Me.Text, FNHSysVenderPramId_lbl.Text & "  " & Me.FNHSysVenderPramId.Text, MessageBoxIcon.Warning)

                                            Exit Sub

                                        End If

                                    Else
                                        MsgBox("พบข้อผิดพลาดในการตรวจสอบข้อมูลก่อนนำเข้่ารายการใบสั่งผลิต !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, My.Application.Info.Title)
                                    End If

                                End If

                            Else
                                Select Case HI.ST.Lang.Language

                                    Case HI.ST.Lang.eLang.TH
                                        MsgBox("ไม่พบข้อมูลสำหรับการนำเข้ารายการใบสั่งผลิตอัตโนมัติ...worksheet 1 !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    Case Else
                                        MsgBox("Data not found, for import factory order...worksheet 1 !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End Select
                            End If

                        Else
                            Me.ogdImportOrder.DataSource = Nothing
                            Me.ogdImportOrder.Refresh()
                            Me.ogvImportOrder.RefreshData()
                        End If

                    End If

                    _oSplash.Close()

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        'Me.ogbmainprocbutton.Enabled = True
                        Me.Cursor = Cursors.Default
                        Me.ogbHeader.Enabled = True
                        Me.ogbBrowseFile.Enabled = True
                        Me.ogbViewDetail.Enabled = True
                    End If

                    If Not (_FoundPrm) Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล File Program ตามที่ระบุ กรุณาทำการตรวจสอบ File !!!", 140630001, Me.Text, FNHSysVenderPramId_lbl.Text & "  " & Me.FNHSysVenderPramId.Text, MessageBoxIcon.Warning)
                        FNHSysVenderPramId.Focus()
                    End If

                Else
                    MsgBox("Unable to locate source file : " & Me.FTFilePath.Text.ToString(), vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                    Me.ogdImportOrder.DataSource = Nothing
                    Me.ogvImportOrder.RefreshData()
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
                Me.ogbHeader.Enabled = True
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

                    If HI.MG.ShowMsg.mConfirmProcess("", 1404240002, "Confirm") = True Then

                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                        Dim _Spls As New HI.TL.SplashScreen("Generate Factory Order No .....Please Wait")

                        If W_PRCbImportFactoryOrder(_Spls) = True Then
                            '...clear temp after process import order complete
                            '---------------------------------------------------------------------------------------------------------------------------------------------------------
                            '  If Not System.Diagnostics.Debugger.IsAttached = True Then
                            Application.DoEvents()

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
                                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS B ON A.FTOrderNo = B.FTGenerateOrderNo"
                                tSql &= Environment.NewLine & "                                        LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS C ON A.FNHSysStyleId = C.FNHSysStyleId"
                                tSql &= Environment.NewLine & " WHERE B.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                                Else

                                End If

                            Catch ex As Exception
                            End Try

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                            tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                            tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
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

                            Call W_PRCbShowBrowseDataImportFactoryOrder()

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
                            tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                            tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                            tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                            tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

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

    Private Sub FNHSysBuyId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FNHSysBuyId.KeyPress
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

    Private Sub FNHSysVenderPramId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysVenderPramId.EditValueChanged
        FTFilePath.Text = ""
        ogdConfirmImport.DataSource = Nothing
    End Sub


    Private Function CheckOrderQtyZero() As Boolean
        Dim _Qry As String = ""

        _Qry = " SELECT  FTPONo FROM (SELECT  FTPONo, SUM(ISNULL(FNQuantity,0)) AS FNQuantity"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp WITH (NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  (FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
        _Qry &= vbCrLf & "   GROUP BY  FTPONo ) AS A WHERE FNQuantity <= 0 "

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "")

    End Function

End Class
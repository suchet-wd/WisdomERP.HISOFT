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
Imports System.Diagnostics
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

Public Class wImportSalesman

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
    Private Const _nTerminateColValidSrc As Integer = 55
    Private Const _tRowBlankHeader As String = "="
    Private Const _tPrefixCustPONumber As String = "N00"

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
    Private Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal lpFileName As String, _
                                                                            ByVal dwDesiredAccess As Long, _
                                                                            ByVal dwShareMode As Long, _
                                                                            ByVal lpSecurityAttributes As Long, _
                                                                            ByVal dwCreationDisposition As Long, _
                                                                            ByVal dwFlagsAndAttributes As Long, _
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

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function CreateFile(lpFileName As String, dwDesiredAccess As FileSystemRights, dwShareMode As FileShare, securityAttrs As IntPtr, dwCreationDisposition As FileMode, dwFlagsAndAttributes As FileOptions, _
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
        Dim bRetProgrammeVendor As Boolean = False

        Try
            'If Me.FNHSysVenderPramId.Text.Trim() <> "" Then
            '    _bRetProgrammeVendor = True
            'Else
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
            '    Me.FNHSysVenderPramId.Focus()
            'End If

            If Not bRetProgrammeVendor AndAlso Me.FNHSysVenderPramId.Text.Trim() <> "" Then
                'bRetProgrammeVendor = True

                If Not bRetProgrammeVendor AndAlso Me.FNHSysSeasonId.Text.Trim() <> "" Then
                    'bRetProgrammeVendor = True

                    If Not bRetProgrammeVendor AndAlso Me.FNHSysUnitId.Text.Trim <> "" Then
                        'bRetProgrammeVendor = True

                        If Not bRetProgrammeVendor AndAlso Me.FNHSysCurId.Text.Trim <> "" Then
                            'bRetProgrammeVendor = True

                            If Not bRetProgrammeVendor AndAlso Me.FDOrderDate.Text.Trim() <> "" Then
                                bRetProgrammeVendor = True
                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDOrderDate_lbl.Text)
                                Me.FDOrderDate.Focus()
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCurId_lbl.Text)
                            Me.FNHSysCurId.Focus()
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                        Me.FNHSysUnitId.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysSeasonId_lbl.Text)
                    Me.FNHSysSeasonId.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
                Me.FNHSysVenderPramId.Focus()
            End If

        Catch ex As Exception
           
        End Try

        Return bRetProgrammeVendor

    End Function

    Private Function W_PRCbValidateConfirmGenerateFactoryOrder() As Boolean
        Dim _bRet As Boolean = False
        Try
            If Me.FNHSysCustId.Text.Trim() <> "" Then
                If Me.FNHSysVenderPramId.Text.Trim() <> "" Then
                    If Me.FNHSysCmpRunId.Text.Trim() <> "" Then
                        If Me.FNHSysBuyId.Text.Trim() <> "" Then
                            If Me.FNHSysCurId.Text.Trim <> "" Then

                                If Me.FDOrderDate.Text <> "" Then
                                    If W_PRCbAddNewStyleImport() = True Then
                                        _bRet = True
                                    End If
                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDOrderDate_lbl.Text)
                                    Me.FDOrderDate.Focus()
                                End If

                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCurId_lbl.Text)
                                Me.FNHSysCurId.Focus()
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

        Dim oDBdtMatSize As System.Data.DataTable

        tSql = ""
        tSql = ";WITH cteImportSize(FTMatSize)"
        tSql &= Environment.NewLine & "AS (SELECT DISTINCT L1.FTSizeBreakdownCode"
        tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanSizeBreakdownTemp] AS L1"
        tSql &= Environment.NewLine & "    WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        tSql &= Environment.NewLine & "    )"
        tSql &= Environment.NewLine & "--SELECT B.FNHSysMatSizeId, A.FTMatSize, B.FTMatSizeNameEN, B.FTMatSizeNameEN"
        tSql &= Environment.NewLine & "SELECT B.FTMatSizeCode"
        tSql &= Environment.NewLine & "FROM cteImportSize AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS B WITH(NOLOCK) ON A.FTMatSize = B.FTMatSizeCode"
        tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC;"

        oDBdtMatSize = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

        Try
            Dim oGridBandedView As DevExpress.XtraGrid.Views.Grid.GridView = Me.ogdConfirmImport.Views(0)

            oGridBandedView.ClearDocument()

            ' Make the group footers always visible.
            oGridBandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

            oGridBandedView.Columns("FTMerTeamCode").Group()
            oGridBandedView.Columns("FTMerTeamCode").SortIndex = 0
            oGridBandedView.Columns("FTMerTeamCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            oGridBandedView.Columns("FTPONo").Group()
            oGridBandedView.Columns("FTPONo").SortIndex = 1
            oGridBandedView.Columns("FTPONo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

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

            Me.FNHSysUnitId.Text = "PCS"
            Me.FNHSysCurId.Text = "US"

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
                    tStyleImport = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleCode").ToString

                    tStyleImportDesc = ""
                    tStyleImportDesc = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleName").ToString

                    tSql = "SELECT TOP 1 A.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A WITH(NOLOCK) WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "';"

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
                        oStrBuilder.AppendLine("      ,'1',2.0")
                        REM 2014/05/24 oStrBuilder.AppendLine("      ,COALESCE((SELECT TOP 1 A.FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS A WITH(NOLOCK) WHERE A.FTCustCode = N'" & HI.UL.ULF.rpQuoted(tCustCodeStyle) & "'), NULL))")
                        oStrBuilder.AppendLine(String.Format("       ,{0})", nFNHSysCustId))

                        tSql = ""
                        tSql = oStrBuilder.ToString()

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then

                        End If

                    End If

                Next nLoopStyle

            Else
                '...do nothing
            End If

        Catch ex As Exception
            

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
          
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateMatchMerMainCategoryType(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Copy()

                Dim tColMerMainCategoryType As String = "F38"

                Dim tTexValidateMainCategoryType As String

                tTexValidateMainCategoryType = ""

                For Each oDataRowColMainCategoryType As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColMainCategoryXX As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColMainCategoryXX.ColumnName.ToString.ToUpper
                            Case tColMerMainCategoryType.ToUpper
                                tTexValidateMainCategoryType = tTexValidateMainCategoryType & oDataRowColMainCategoryType.Item(oColMainCategoryXX.ColumnName.ToString)
                            Case Else
                                '...Nothing 
                        End Select

                    Next

                Next

                '...validate column merchandiser team
                If tTexValidateMainCategoryType <> "" Then

                    Dim tMerChanMainCategoryType As List(Of String) = oDBdtSrc.AsEnumerable() _
                                                                  .Select(Function(r) r.Field(Of String)(tColMerMainCategoryType)) _
                                                                  .Distinct() _
                                                                  .ToList()

                    If Not tMerChanMainCategoryType Is Nothing And tMerChanMainCategoryType.Count > 0 Then
                        '...ถ้ามีบางรายการ ของแถวข้อมูล สำหรับ คอลัมน์ Main Category Type เป็นค่าว่าง หรือ blank
                        Dim oDBdtMerMainCategoryType As System.Data.DataTable

                        tSql = ""
                        tSql = "SELECT A.FTMainCategoryCode"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainCategoryType AS A (NOLOCK)"
                        tSql &= Environment.NewLine & "ORDER BY A.FTMainCategoryCode ASC;"

                        oDBdtMerMainCategoryType = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                        If Not oDBdtMerMainCategoryType Is Nothing And oDBdtMerMainCategoryType.Rows.Count > 0 Then

                            Dim bFoundMerMainCategoryType As Boolean = True

                            For Each s As String In tMerChanMainCategoryType

                                If Not s Is Nothing Then

                                    If s.Trim() <> "" Then

                                        Dim oDataRow As DataRow()

                                        Try
                                            oDataRow = oDBdtMerMainCategoryType.Select("FTMainCategoryCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                            If oDataRow.Length > 0 Then
                                                '...valid team merchandiser
                                                If oDataRow(0)("FTMainCategoryCode") <> "" Then
                                                    'MsgBox("Merchan Team : " & s.ToString())
                                                    'bFoundMerMainCategoryType = True
                                                Else
                                                    '...do nothing
                                                End If

                                            Else

                                                Select Case HI.ST.Lang.Language
                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบข้อมูลประเภทผลิตภัณฑ์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการประเภทผลิตภัณฑ์ : " & s.ToString() & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการประเภทผลิตภัณฑ์ !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Invalid Main Category Type " & Environment.NewLine & "Or Not provide for main category type master file !!!" & Environment.NewLine & "Main Category Type : " & s.ToString() & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                End Select

                                                bFoundMerMainCategoryType = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else

                                        Select Case HI.ST.Lang.Language
                                            Case HI.ST.Lang.eLang.TH
                                                MsgBox("ไฟล์นำเข้ารายการใบสั่งผลิตอัตโนมัติยังไม่มีการกำหนดรายการข้อมูลประเภทผลิตภัณฑ์ หรือ กำหนดข้อมูลไม่ครบถ้วนกรุณาตรวจสอบอีกครั้ง !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            Case Else
                                                MsgBox("Source file row item not complete or not yet specify main category type code" & Environment.NewLine & "{FB:Football, BB:Basketball} !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End Select

                                        bFoundMerMainCategoryType = False

                                        Exit For

                                    End If

                                Else

                                    Select Case HI.ST.Lang.Language
                                        Case HI.ST.Lang.eLang.TH
                                            MsgBox("ไม่พบรายการข้อมูลประเภทผลิตภัณฑ์ !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        Case Else
                                            MsgBox("Invalid data main category type code !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End Select

                                    bFoundMerMainCategoryType = False

                                    Exit For

                                End If

                            Next

                            If bFoundMerMainCategoryType = True Then
                                bRet = True
                            End If

                        End If

                        If Not oDBdtMerMainCategoryType Is Nothing Then oDBdtMerMainCategoryType.Dispose()

                    Else
                        MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลประเภทผลิตภัณฑ์ เช่น ฟุตบอล/บาสเก็ตบอล" & Environment.NewLine & "ตัวอย่าง เช่น FB:Football, BB:Basketball !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    End If

                Else
                    MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลประเภทผลิตภัณฑ์ เช่น ฟุตบอล/บาสเก็ตบอล" & Environment.NewLine & "ตัวอย่าง เช่น FB:Football, BB:Basketball !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            Else
                '...do nothing
            End If

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateMatchMerchanTeam(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Copy()

                Dim tColMerchanTeam As String = "F5"

                Dim tTexValidateMerchandiserTeam As String

                tTexValidateMerchandiserTeam = ""

                For Each oDataRowColMerchanTeam As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColMerchanTeamXX As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColMerchanTeamXX.ColumnName.ToString.ToUpper
                            Case tColMerchanTeam.ToUpper
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

                                                Select Case HI.ST.Lang.Language
                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบข้อมูลทีมเมอร์แชนไดเซอร์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสทีม : " & s.ToString() & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!" & Environment.NewLine & "Merchandiser Team : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                End Select

                                                bFoundMerTeam = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else

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

                        If Not oDBdtMerchanTeam Is Nothing Then oDBdtMerchanTeam.Dispose()

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
           
        End Try

        Return bRet

    End Function

    Private Sub W_PRCxValidateMaster(ByVal oDBdt As System.Data.DataTable)

        Call W_PRCbValidateExistsMasterStyle(oDBdt)     '...validate style
        Call W_PRCbValidateExistsMatchColor(oDBdt)      '...validate colorway
        Call W_PRCbValidateExistsMasterGender(oDBdt)    '...validate gender
        Call W_PRCbValidateExistsMasterShipMode(oDBdt)  '...validate ship mode
        Call W_PRCbValidateExistsMasterContinent(oDBdt) '...validate continent
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
                                    

                                End If

                            Next

                        End If

                    End If

                End With

                _wMapSizeImportOrder.TopMost = False
                _wMapSizeImportOrder.Refresh()

            Catch ex As Exception
               
            End Try

            _oSplash.TopMost = True
            _oSplash.Refresh()

        End If

        If Not tmpDTNotMapSize Is Nothing Then tmpDTNotMapSize.Dispose()
        '========================================================================================================

        Call W_PRCbValidateExistsMasterProductType(oDBdt) '******************************************************

    End Sub

    Private Function W_PRCbValidateExistsMasterProductType(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                Dim tColCategoryCode As String = "F38" '...AK : FB:Footbal, BB:Basketball
                Dim tColCategoryExtend As String = "F37" '...AJ : CMO_Sponsor_Bus_Org
                Dim tColSubCategoryExtend As String = "F39" '...AL : CMO_Sponsor_Consumer_Purpose

                '---------------------------------------------------------------------------------------------------------------------------------
                Dim DTMatProductTypeTemp As System.Data.DataTable

                DTMatProductTypeTemp = New System.Data.DataTable

                Dim oColFTProdTypeCode As System.Data.DataColumn
                oColFTProdTypeCode = New System.Data.DataColumn("FTProdTypeCode", System.Type.GetType("System.String"))
                oColFTProdTypeCode.Caption = "FTProdTypeCode"
                DTMatProductTypeTemp.Columns.Add(oColFTProdTypeCode.ColumnName, oColFTProdTypeCode.DataType)

                Dim oColFTProdTypeDesc As System.Data.DataColumn
                oColFTProdTypeDesc = New System.Data.DataColumn("FTProdTypeDesc", System.Type.GetType("System.String"))
                oColFTProdTypeDesc.Caption = "FTProdTypeDesc"

                DTMatProductTypeTemp.Columns.Add(oColFTProdTypeDesc.ColumnName, oColFTProdTypeDesc.DataType)

                Dim oColFTProdTypeRemark As System.Data.DataColumn
                oColFTProdTypeRemark = New System.Data.DataColumn("FTProdTypeRemark", System.Type.GetType("System.String"))
                oColFTProdTypeRemark.Caption = "FTProdTypeRemark"

                DTMatProductTypeTemp.Columns.Add(oColFTProdTypeRemark.ColumnName, oColFTProdTypeRemark.DataType)

                '---------------------------------------------------------------------------------------------------------------------------------

                Dim tTextCategoryCode As String
                Dim tTextCategoryExtend As String
                Dim tTextSubCategoryExtend As String

                For Each oRowProdType As System.Data.DataRow In oDBdt.Rows

                    tTextCategoryCode = "" : tTextCategoryExtend = "" : tTextSubCategoryExtend = ""

                    If Not DBNull.Value.Equals(oRowProdType.Item(tColCategoryCode)) Then
                        tTextCategoryCode = oRowProdType.Item(tColCategoryCode).ToString.Trim

                        If Not DBNull.Value.Equals(oRowProdType.Item(tColCategoryExtend)) Then
                            tTextCategoryExtend = oRowProdType.Item(tColCategoryExtend).ToString.Trim
                        End If

                        If Not DBNull.Value.Equals(oRowProdType.Item(tColSubCategoryExtend)) Then
                            tTextSubCategoryExtend = oRowProdType.Item(tColSubCategoryExtend).ToString.Trim
                        End If

                    End If

                    If tTextCategoryCode <> "" And tTextCategoryExtend <> "" And tTextSubCategoryExtend <> "" Then

                        Dim oRowAppendProdType As System.Data.DataRow

                        If Not DTMatProductTypeTemp Is Nothing And DTMatProductTypeTemp.Rows.Count > 0 Then
                            Dim oRowProdTypeNotExists As System.Data.DataRow()

                            oRowProdTypeNotExists = DTMatProductTypeTemp.Select("FTProdTypeCode = '" & HI.UL.ULF.rpQuoted(tTextCategoryCode) & "' AND FTProdTypeDesc = '" & HI.UL.ULF.rpQuoted(tTextCategoryExtend) & "' AND FTProdTypeRemark = '" & HI.UL.ULF.rpQuoted(tTextSubCategoryExtend) & "'")

                            If oRowProdTypeNotExists.Length > 0 Then
                                '...already exists record
                            Else
                                oRowAppendProdType = DTMatProductTypeTemp.NewRow()
                                oRowAppendProdType.Item("FTProdTypeCode") = tTextCategoryCode
                                oRowAppendProdType.Item("FTProdTypeDesc") = tTextCategoryExtend
                                oRowAppendProdType.Item("FTProdTypeRemark") = tTextSubCategoryExtend
                                DTMatProductTypeTemp.Rows.Add(oRowAppendProdType)
                            End If
                        Else
                            oRowAppendProdType = DTMatProductTypeTemp.NewRow()
                            oRowAppendProdType.Item("FTProdTypeCode") = tTextCategoryCode
                            oRowAppendProdType.Item("FTProdTypeDesc") = tTextCategoryExtend
                            oRowAppendProdType.Item("FTProdTypeRemark") = tTextSubCategoryExtend
                            DTMatProductTypeTemp.Rows.Add(oRowAppendProdType)
                        End If

                    End If

                Next

                If Not DTMatProductTypeTemp Is Nothing Then DTMatProductTypeTemp.AcceptChanges()

                If Not DTMatProductTypeTemp Is Nothing AndAlso DTMatProductTypeTemp.Rows.Count > 0 Then

                    Dim tCategoryCode As String = ""
                    Dim tCategoryExtend As String = ""
                    Dim tSubCategoryExtend As String = ""

                    Dim oStrBuilder As New System.Text.StringBuilder()

                    For Each oRowMatProdType As System.Data.DataRow In DTMatProductTypeTemp.Rows

                        tCategoryCode = "" : tCategoryExtend = "" : tSubCategoryExtend = ""

                        If Not DBNull.Value.Equals(oRowMatProdType.Item("FTProdTypeCode")) Then
                            tCategoryCode = oRowMatProdType.Item("FTProdTypeCode").ToString.Trim

                            If tCategoryCode <> "" AndAlso Not DBNull.Value.Equals(oRowMatProdType.Item("FTProdTypeDesc")) Then
                                tCategoryExtend = oRowMatProdType.Item("FTProdTypeDesc").ToString.Trim

                                If tCategoryExtend <> "" AndAlso Not DBNull.Value.Equals(oRowMatProdType.Item("FTProdTypeRemark")) Then
                                    tSubCategoryExtend = oRowMatProdType.Item("FTProdTypeRemark").ToString.Trim

                                    If tSubCategoryExtend <> "" Then

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
                                        'oStrBuilder.AppendLine("WHERE (A.FTProdTypeNameTH = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "' OR A.FTProdTypeNameEN = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "');")
                                        oStrBuilder.AppendLine("WHERE A.FTProdTypeCode = N'" & HI.UL.ULF.rpQuoted(tProductTypeCode) & "';")

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
                                                oStrBuilder.AppendLine(String.Format("       ({0}", "'Admin'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "'2015/03/27'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "'00:13:00'"))
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

                    Next

                End If

                If Not DTMatProductTypeTemp Is Nothing Then DTMatProductTypeTemp.Dispose()

                bRet = True

            End If

        Catch ex As Exception
           
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

            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

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

        If Not tmpDTMapSizeMaster Is Nothing Then tmpDTMapSizeMaster.Dispose()

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

            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

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

            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

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

                Next nLoopPlant

                bRet = True
            End If

        Catch ex As Exception

            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterContinent(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing AndAlso oDBdt.Rows.Count > 0 Then

                Dim tColFTContinent As String = "F17" '...AF REQ# : ทวีป : Continent

                '--------------------------------------------------------------------------------------------------------------
                Dim DTMatContinent As System.Data.DataTable

                DTMatContinent = New System.Data.DataTable

                Dim oColFTContinentCode As System.Data.DataColumn
                oColFTContinentCode = New System.Data.DataColumn("FTContinentCode", System.Type.GetType("System.String"))
                oColFTContinentCode.Caption = "FTContinentCode"
                DTMatContinent.Columns.Add(oColFTContinentCode.ColumnName, oColFTContinentCode.DataType)

                Dim oColFTContinentDesc As System.Data.DataColumn
                oColFTContinentDesc = New System.Data.DataColumn("FTContinentDesc", System.Type.GetType("System.String"))
                oColFTContinentDesc.Caption = "FTContinentDesc"
                DTMatContinent.Columns.Add(oColFTContinentDesc.ColumnName, oColFTContinentDesc.DataType)
                '--------------------------------------------------------------------------------------------------------------

                Dim tTextContinentCode As String
                Dim tTextContinentDesc As String

                For Each oRowContinent As System.Data.DataRow In oDBdt.Rows
                    tTextContinentCode = "" : tTextContinentDesc = ""

                    If Not DBNull.Value.Equals(oRowContinent.Item(tColFTContinent)) Then
                        tTextContinentCode = oRowContinent.Item(tColFTContinent).ToString.Trim
                        tTextContinentDesc = tTextContinentCode
                    End If

                    If tTextContinentCode <> "" Then
                        Dim oRowAppend As System.Data.DataRow

                        If Not DTMatContinent Is Nothing AndAlso DTMatContinent.Rows.Count > 0 Then
                            Dim oRowContinentNotExists As System.Data.DataRow()

                            oRowContinentNotExists = DTMatContinent.Select("FTContinentCode = '" & HI.UL.ULF.rpQuoted(tTextContinentCode) & "'")

                            If oRowContinentNotExists.Length > 0 Then
                                '...already exists record
                            Else
                                oRowAppend = DTMatContinent.NewRow()
                                oRowAppend.Item("FTContinentCode") = tTextContinentCode
                                oRowAppend.Item("FTContinentDesc") = tTextContinentDesc
                                DTMatContinent.Rows.Add(oRowAppend)
                            End If

                        Else
                            oRowAppend = DTMatContinent.NewRow()
                            oRowAppend.Item("FTContinentCode") = tTextContinentCode
                            oRowAppend.Item("FTContinentDesc") = tTextContinentDesc
                            DTMatContinent.Rows.Add(oRowAppend)
                        End If

                    End If

                Next

                If Not DTMatContinent Is Nothing Then DTMatContinent.AcceptChanges()

                If Not DTMatContinent Is Nothing AndAlso DTMatContinent.Rows.Count > 0 Then

                    Dim tContinentCode As String
                    Dim tContinentDesc As String

                    For Each oRowValidContinent As System.Data.DataRow In DTMatContinent.Rows

                        tContinentCode = "" : tContinentDesc = ""

                        If Not DBNull.Value.Equals(oRowValidContinent.Item("FTContinentCode")) Then
                            tContinentCode = oRowValidContinent.Item("FTContinentCode").ToString.Trim

                            If tContinentCode <> "" Then
                                tContinentDesc = oRowValidContinent.Item("FTContinentDesc").ToString.Trim

                                tSql = ""
                                tSql = "SELECT TOP 1 A.FTContinentNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS A (NOLOCK) WHERE A.FTContinentNameEN = N'" & HI.UL.ULF.rpQuoted(tContinentCode) & "' OR A.FTContinentCode = N'" & HI.UL.ULF.rpQuoted(tContinentCode) & "';"

                                If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then

                                    Dim nFNHSysContinentId As Integer

                                    nFNHSysContinentId = Val(HI.TL.RunID.GetRunNoID("TCNMContinent", "FNHSysContinentId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                    Dim oStrBuilder As New System.Text.StringBuilder()

                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TCNMContinent]")
                                    oStrBuilder.AppendLine("           ([FTInsUser]")
                                    oStrBuilder.AppendLine("           ,[FDInsDate]")
                                    oStrBuilder.AppendLine("           ,[FTInsTime]")
                                    oStrBuilder.AppendLine("           ,[FTUpdUser]")
                                    oStrBuilder.AppendLine("           ,[FDUpdDate]")
                                    oStrBuilder.AppendLine("           ,[FTUpdTime]")
                                    oStrBuilder.AppendLine("           ,[FNHSysContinentId]")
                                    oStrBuilder.AppendLine("           ,[FTContinentCode]")
                                    oStrBuilder.AppendLine("           ,[FTContinentNameTH]")
                                    oStrBuilder.AppendLine("           ,[FTContinentNameEN]")
                                    oStrBuilder.AppendLine("           ,[FTRemark]")
                                    oStrBuilder.AppendLine("           ,[FTStateActive])")
                                    oStrBuilder.AppendLine(String.Format("SELECT  '{0}',", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                                    oStrBuilder.AppendLine(String.Format("        '{0}',", "2015/03/27"))
                                    oStrBuilder.AppendLine(String.Format("        '{0}',", "16:52:00"))
                                    oStrBuilder.AppendLine("        NULL,")
                                    oStrBuilder.AppendLine("        NULL,")
                                    oStrBuilder.AppendLine("        NULL,")
                                    oStrBuilder.AppendLine(String.Format("        '{0}',", nFNHSysContinentId))
                                    oStrBuilder.AppendLine(String.Format("        '{0}',", HI.UL.ULF.rpQuoted(tContinentCode)))
                                    oStrBuilder.AppendLine(String.Format("        '{0}',", HI.UL.ULF.rpQuoted(tContinentDesc)))
                                    oStrBuilder.AppendLine(String.Format("        '{0}',", HI.UL.ULF.rpQuoted(tContinentDesc)))
                                    oStrBuilder.AppendLine("        '',")
                                    oStrBuilder.AppendLine("        '1';")
                                    If oStrBuilder.Length > 0 Then
                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then

                                            MsgBox("Execute data complete...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)


                                        End If

                                    End If

                                End If

                            End If

                        End If

                    Next

                End If

                If Not DTMatContinent Is Nothing Then DTMatContinent.Dispose()

                bRet = True

            End If

        Catch ex As Exception

            MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)

        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterCountry(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        'F4  :  CUST XREF : ประเทศ : Country
        Try

            If Not oDBdt Is Nothing AndAlso oDBdt.Rows.Count > 0 Then

                Dim tColFTCountry As String = "F4"
                Dim tColFTCountry2 As String = "F19"

                Dim DTMatCountry As System.Data.DataTable

                DTMatCountry = New System.Data.DataTable

                Dim oColFTMatCountryCode As System.Data.DataColumn
                oColFTMatCountryCode = New System.Data.DataColumn("FTCountryCode", System.Type.GetType("System.String"))
                oColFTMatCountryCode.Caption = "FTCountryCode"

                DTMatCountry.Columns.Add(oColFTMatCountryCode.ColumnName, oColFTMatCountryCode.DataType)

                Dim oColFTMatCountryDesc As System.Data.DataColumn
                oColFTMatCountryDesc = New System.Data.DataColumn("FTCountryDesc", System.Type.GetType("System.String"))
                oColFTMatCountryDesc.Caption = "FTCountryDesc"

                DTMatCountry.Columns.Add(oColFTMatCountryDesc.ColumnName, oColFTMatCountryDesc.DataType)

                Dim tTextCountryCode As String
                Dim tTextCountryDesc As String

                For Each oRowCountry As System.Data.DataRow In oDBdt.Rows
                    tTextCountryCode = "" : tTextCountryDesc = ""

                    If Not DBNull.Value.Equals(oRowCountry.Item(tColFTCountry)) Then

                        If Not DBNull.Value.Equals(oRowCountry.Item(tColFTCountry2)) Then
                            tTextCountryCode = oRowCountry.Item(tColFTCountry).ToString.Trim & oRowCountry.Item(tColFTCountry2).ToString.Trim
                        Else
                            tTextCountryCode = oRowCountry.Item(tColFTCountry).ToString.Trim
                        End If

                        tTextCountryDesc = tTextCountryCode
                    End If

                    If tTextCountryCode <> "" Then
                        Dim oRowAppendCountry As System.Data.DataRow

                        If Not DTMatCountry Is Nothing And DTMatCountry.Rows.Count > 0 Then

                            Dim oRowCountryNotExists As System.Data.DataRow()

                            oRowCountryNotExists = DTMatCountry.Select("FTCountryCode = '" & HI.UL.ULF.rpQuoted(tTextCountryCode) & "'")

                            If oRowCountryNotExists.Length > 0 Then
                                '...already exists record
                            Else
                                oRowAppendCountry = DTMatCountry.NewRow()
                                oRowAppendCountry.Item("FTCountryCode") = tTextCountryCode
                                oRowAppendCountry.Item("FTCountryDesc") = tTextCountryDesc
                                DTMatCountry.Rows.Add(oRowAppendCountry)
                            End If

                        Else
                            oRowAppendCountry = DTMatCountry.NewRow()
                            oRowAppendCountry.Item("FTCountryCode") = tTextCountryCode
                            oRowAppendCountry.Item("FTCountryDesc") = tTextCountryDesc
                            DTMatCountry.Rows.Add(oRowAppendCountry)
                        End If

                    End If

                Next

                If Not DTMatCountry Is Nothing Then DTMatCountry.AcceptChanges()

                If Not DTMatCountry Is Nothing AndAlso DTMatCountry.Rows.Count > 0 Then

                    Dim tAppendCountryCode As String
                    Dim tAppendCountryDesc As String

                    For Each oRowAppendNewCountry As System.Data.DataRow In DTMatCountry.Rows
                        tAppendCountryCode = "" : tAppendCountryDesc = ""

                        tAppendCountryCode = oRowAppendNewCountry.Item("FTCountryCode").ToString.Trim

                        If tAppendCountryCode <> "" Then

                            tAppendCountryDesc = oRowAppendNewCountry.Item("FTCountryDesc").ToString.Trim

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTCountryNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A WITH(NOLOCK) WHERE (A.FTCountryNameEN LIKE '%" & HI.UL.ULF.rpQuoted(tAppendCountryCode) & "%' OR A.FTCountryCode LIKE '%" & HI.UL.ULF.rpQuoted(tAppendCountryCode) & "');"

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
                                oStrBuilder.AppendLine("VALUES(N'Admin'")
                                oStrBuilder.AppendLine("      ,N'2015/03/28'")
                                oStrBuilder.AppendLine("      ,N'23:49:00'")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysCountryId))
                                oStrBuilder.AppendLine("      ,-1")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tAppendCountryCode) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tAppendCountryDesc) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tAppendCountryDesc) & "'")
                                oStrBuilder.AppendLine("      ,''")
                                oStrBuilder.AppendLine("      ,'1');")

                                tSql = ""
                                tSql = oStrBuilder.ToString()

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    

                                End If

                            End If

                        End If

                    Next

                End If

                If Not DTMatCountry Is Nothing Then DTMatCountry.Dispose()

                bRet = True

            End If

        Catch ex As Exception
           
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
      
        'End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterShipMode(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                '------------------------------------------------------------------------------------------------------------------
                Dim DTShipModeTemp As System.Data.DataTable

                DTShipModeTemp = New System.Data.DataTable

                Dim oColFTMDTR As System.Data.DataColumn
                oColFTMDTR = New System.Data.DataColumn("FTShipModeCode", System.Type.GetType("System.String"))
                oColFTMDTR.Caption = "FTShipModeCode"

                DTShipModeTemp.Columns.Add(oColFTMDTR.ColumnName, oColFTMDTR.DataType)

                Dim oColFTMDTRDesc As System.Data.DataColumn
                oColFTMDTRDesc = New System.Data.DataColumn("FTShipModeDesc", System.Type.GetType("System.String"))
                oColFTMDTRDesc.Caption = "FTShipModeDesc"

                DTShipModeTemp.Columns.Add(oColFTMDTRDesc.ColumnName, oColFTMDTRDesc.DataType)
                '------------------------------------------------------------------------------------------------------------------

                Dim tColMDTR As String = "F16"

                Dim tTextShipMode As String, tTextShipModeDesc As String

                For Each oRowShipMode As System.Data.DataRow In oDBdt.Rows
                    tTextShipMode = "" : tTextShipModeDesc = ""

                    If Not DBNull.Value.Equals(oRowShipMode.Item(tColMDTR)) Then
                        tTextShipMode = oRowShipMode.Item(tColMDTR).ToString.Trim()
                        tTextShipModeDesc = tTextShipMode
                    End If

                    If tTextShipMode <> "" Then

                        Dim oRowAppendShipMode As System.Data.DataRow

                        If Not DTShipModeTemp Is Nothing AndAlso DTShipModeTemp.Rows.Count > 0 Then
                            Dim oDataRowShipModeExists As System.Data.DataRow()
                            oDataRowShipModeExists = DTShipModeTemp.Select("FTShipModeCode = '" & HI.UL.ULF.rpQuoted(tTextShipMode) & "'")

                            If oDataRowShipModeExists.Length > 0 Then
                                '...already exists
                            Else
                                oRowAppendShipMode = DTShipModeTemp.NewRow()
                                oRowAppendShipMode.Item("FTShipModeCode") = tTextShipMode
                                oRowAppendShipMode.Item("FTShipModeDesc") = tTextShipModeDesc
                                DTShipModeTemp.Rows.Add(oRowAppendShipMode)
                            End If

                        Else
                            oRowAppendShipMode = DTShipModeTemp.NewRow()
                            oRowAppendShipMode.Item("FTShipModeCode") = tTextShipMode
                            oRowAppendShipMode.Item("FTShipModeDesc") = tTextShipModeDesc
                            DTShipModeTemp.Rows.Add(oRowAppendShipMode)
                        End If

                    End If

                Next

                If Not DTShipModeTemp Is Nothing Then DTShipModeTemp.AcceptChanges()

                If Not DTShipModeTemp Is Nothing AndAlso DTShipModeTemp.Rows.Count > 0 Then

                    Dim oDBdvNotExistsShipMode As System.Data.DataView = DTShipModeTemp.DefaultView
                    oDBdvNotExistsShipMode.Sort = "FTShipModeCode"

                    Dim oDBdtNotExistsMatShipMode As System.Data.DataTable = oDBdvNotExistsShipMode.ToTable()

                    If Not oDBdtNotExistsMatShipMode Is Nothing AndAlso oDBdtNotExistsMatShipMode.Rows.Count > 0 Then

                        Dim nLoopNotExitsShipMode As Integer

                        Dim tTextShipModeCodeNotExists As String, tTextShipModeDescNotExists As String

                        For nLoopNotExitsShipMode = 0 To oDBdtNotExistsMatShipMode.Rows.Count - 1
                            tTextShipModeCodeNotExists = "" : tTextShipModeDescNotExists = ""

                            If Not DBNull.Value.Equals(oDBdtNotExistsMatShipMode.Rows(nLoopNotExitsShipMode)("FTShipModeCode")) Then
                                tTextShipModeCodeNotExists = oDBdtNotExistsMatShipMode.Rows(nLoopNotExitsShipMode)("FTShipModeCode").ToString.Trim
                            End If

                            If Not DBNull.Value.Equals(oDBdtNotExistsMatShipMode.Rows(nLoopNotExitsShipMode)("FTShipModeDesc")) Then
                                tTextShipModeDescNotExists = oDBdtNotExistsMatShipMode.Rows(nLoopNotExitsShipMode)("FTShipModeDesc").ToString
                            End If

                            If tTextShipModeCodeNotExists <> "" Then
                                tSql = ""
                                tSql = "SELECT TOP 1 A.FTShipModeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS A  WITH(NOLOCK) WHERE A.FTShipModeCode = N'" & HI.UL.ULF.rpQuoted(tTextShipModeCodeNotExists) & "';"

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
                                    oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tTextShipModeCodeNotExists) & "'")
                                    oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tTextShipModeDescNotExists) & "'")
                                    oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tTextShipModeCodeNotExists) & "'")
                                    oStrBuilder.AppendLine("       ,''")
                                    oStrBuilder.AppendLine("       ,'1')")

                                    tSql = ""
                                    tSql = oStrBuilder.ToString()

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                        'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End If

                                End If

                            End If

                        Next nLoopNotExitsShipMode

                    End If

                End If

                If Not DTShipModeTemp Is Nothing Then DTShipModeTemp.Dispose()

                bRet = True

            End If

        Catch ex As Exception
           
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
           
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterMerchanTeam(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then
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

                Next nLoopMasterMerchanTeam

                bRet = True

            End If

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterGender(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then
                '------------------------------------------------------------------------------------------------

                Dim DTMerMatGenderTemp As System.Data.DataTable

                DTMerMatGenderTemp = New System.Data.DataTable

                Dim oColFTGenderCode As System.Data.DataColumn
                oColFTGenderCode = New System.Data.DataColumn("FTGenderCode", System.Type.GetType("System.String"))
                oColFTGenderCode.Caption = "FTGenderCode"

                DTMerMatGenderTemp.Columns.Add(oColFTGenderCode.ColumnName, oColFTGenderCode.DataType)

                Dim oColFTGenderName As System.Data.DataColumn
                oColFTGenderName = New System.Data.DataColumn("FTGenderName", System.Type.GetType("System.String"))
                oColFTGenderName.Caption = "FTGenderName"

                DTMerMatGenderTemp.Columns.Add(oColFTGenderName.ColumnName, oColFTGenderName.DataType)

                '------------------------------------------------------------------------------------------------

                Dim tColConsumerGrp As String = "F40"

                For Each oDataRowGender As System.Data.DataRow In oDBdt.Rows
                    Dim tTextGenderCode As String
                    Dim tTextGenderName As String

                    tTextGenderCode = "" : tTextGenderName = ""

                    If Not DBNull.Value.Equals(oDataRowGender.Item(tColConsumerGrp)) Then
                        tTextGenderCode = oDataRowGender.Item(tColConsumerGrp).ToString
                        tTextGenderName = tTextGenderCode
                    End If

                    If tTextGenderCode <> "" Then

                        Dim oRowAppendGender As System.Data.DataRow

                        If Not DTMerMatGenderTemp Is Nothing And DTMerMatGenderTemp.Rows.Count > 0 Then

                            Dim oRowGenderExists As System.Data.DataRow()

                            oRowGenderExists = DTMerMatGenderTemp.Select("FTGenderCode = '" & HI.UL.ULF.rpQuoted(tTextGenderCode) & "'")

                            If oRowGenderExists.Length > 0 Then
                                '...already exists
                            Else
                                oRowAppendGender = DTMerMatGenderTemp.NewRow()
                                oRowAppendGender.Item("FTGenderCode") = tTextGenderCode
                                oRowAppendGender.Item("FTGenderName") = tTextGenderName
                                DTMerMatGenderTemp.Rows.Add(oRowAppendGender)
                            End If

                        Else
                            oRowAppendGender = DTMerMatGenderTemp.NewRow()
                            oRowAppendGender.Item("FTGenderCode") = tTextGenderCode
                            oRowAppendGender.Item("FTGenderName") = tTextGenderName
                            DTMerMatGenderTemp.Rows.Add(oRowAppendGender)
                        End If

                    End If

                Next

                If Not DTMerMatGenderTemp Is Nothing Then DTMerMatGenderTemp.AcceptChanges()

                If Not DTMerMatGenderTemp Is Nothing AndAlso DTMerMatGenderTemp.Rows.Count > 0 Then

                    Dim oDBdvNotExistsMatGender As System.Data.DataView = DTMerMatGenderTemp.DefaultView
                    oDBdvNotExistsMatGender.Sort = "FTGenderCode"

                    Dim oDBdtNotExistsMatGender As System.Data.DataTable = oDBdvNotExistsMatGender.ToTable()

                    If Not oDBdtNotExistsMatGender Is Nothing AndAlso oDBdtNotExistsMatGender.Rows.Count > 0 Then

                        Dim nLoopNotExitsMatGender As Integer

                        Dim tMatGenderCodeNotExists As String, tMatGenderNameNotExists As String

                        For nLoopNotExitsMatGender = 0 To oDBdtNotExistsMatGender.Rows.Count - 1
                            tMatGenderCodeNotExists = "" : tMatGenderNameNotExists = ""

                            If Not DBNull.Value.Equals(oDBdtNotExistsMatGender.Rows(nLoopNotExitsMatGender)("FTGenderCode")) Then
                                tMatGenderCodeNotExists = oDBdtNotExistsMatGender.Rows(nLoopNotExitsMatGender)("FTGenderCode").ToString
                            End If

                            If Not DBNull.Value.Equals(oDBdtNotExistsMatGender.Rows(nLoopNotExitsMatGender)("FTGenderName")) Then
                                tMatGenderNameNotExists = oDBdtNotExistsMatGender.Rows(nLoopNotExitsMatGender)("FTGenderName").ToString
                            End If

                            If tMatGenderCodeNotExists <> "" Then
                                tSql = ""
                                tSql = "SELECT TOP 1 A.FTGenderCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS A  WITH(NOLOCK) WHERE A.FTGenderCode = N'" & HI.UL.ULF.rpQuoted(tMatGenderCodeNotExists) & "';"

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
                                    tSql &= "                            ," & nFNHSysGenderId & ", N'" & HI.UL.ULF.rpQuoted(tMatGenderCodeNotExists) & "', N'" & HI.UL.ULF.rpQuoted(tMatGenderNameNotExists) & "'"
                                    tSql &= "                            ,N'" & HI.UL.ULF.rpQuoted(tMatGenderCodeNotExists) & "', '', '1')"

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                        'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End If

                                End If

                            End If

                        Next nLoopNotExitsMatGender

                    End If

                End If

                If Not DTMerMatGenderTemp Is Nothing Then DTMerMatGenderTemp.Dispose()

                bRet = True

            End If

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchColor(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                '------------------------------------------------------------------------------------------------
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
                '------------------------------------------------------------------------------------------------

                Dim tColCLRCOD As String = "F7"
                Dim tColCLRDESC As String = "F7"

                For Each oRowCLRCOD As System.Data.DataRow In oDBdt.Rows

                    Dim tTextCLRCODE As String
                    Dim tTextCLRDESC As String

                    tTextCLRCODE = "" : tTextCLRDESC = ""

                    If Not DBNull.Value.Equals(oRowCLRCOD.Item(tColCLRCOD)) Then
                        tTextCLRCODE = oRowCLRCOD.Item(tColCLRCOD).ToString
                        tTextCLRDESC = tTextCLRCODE
                    End If

                    If tTextCLRCODE <> "" Then
                        Dim oRowMatColorAppend As System.Data.DataRow

                        If Not oDBdtMatColorTemp Is Nothing AndAlso oDBdtMatColorTemp.Rows.Count > 0 Then
                            Dim oDataRowExists As DataRow()
                            oDataRowExists = oDBdtMatColorTemp.Select("FTMatColorCode = '" & HI.UL.ULF.rpQuoted(tTextCLRCODE) & "'")

                            If oDataRowExists.Length > 0 Then
                                '...already exists
                            Else
                                oRowMatColorAppend = oDBdtMatColorTemp.NewRow()
                                oRowMatColorAppend.Item("FTMatColorCode") = tTextCLRCODE
                                oRowMatColorAppend.Item("FTMatColorDesc") = tTextCLRDESC
                                oDBdtMatColorTemp.Rows.Add(oRowMatColorAppend)
                            End If

                        Else
                            oRowMatColorAppend = oDBdtMatColorTemp.NewRow()
                            oRowMatColorAppend.Item("FTMatColorCode") = tTextCLRCODE
                            oRowMatColorAppend.Item("FTMatColorDesc") = tTextCLRDESC
                            oDBdtMatColorTemp.Rows.Add(oRowMatColorAppend)
                        End If

                    End If

                Next

                If Not oDBdtMatColorTemp Is Nothing Then oDBdtMatColorTemp.AcceptChanges()

                If Not oDBdtMatColorTemp Is Nothing And oDBdtMatColorTemp.Rows.Count > 0 Then

                    'oDBdtMatColorTemp.AcceptChanges()

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
                                 
                                End If

                            End If

                        Next nLoopNotExitsMatColor

                    End If

                Else
                    '...do nothing
                End If

                If Not oDBdtMatColorTemp Is Nothing Then oDBdtMatColorTemp.Dispose()

                bRet = True

            End If

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchSize(ByVal oDBdt As System.Data.DataTable, ByRef paramDTNotMapSize As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try

            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                '...modify Match Size Temp 2015/03/26
                '------------------------------------------------------------------------------------------------------------------------
                Dim DTMapSizeTemp As System.Data.DataTable

                DTMapSizeTemp = New System.Data.DataTable

                Dim oColFTMapSizeCode As System.Data.DataColumn
                oColFTMapSizeCode = New System.Data.DataColumn("FTMapSizeCode", System.Type.GetType("System.String"))
                oColFTMapSizeCode.Caption = "FTMapSizeCode"

                DTMapSizeTemp.Columns.Add(oColFTMapSizeCode.ColumnName, oColFTMapSizeCode.DataType)

                Dim tColFTSize As String = "F21"
                Dim tTextFTSize As String

                For Each oRowMapSize As System.Data.DataRow In oDBdt.Rows

                    tTextFTSize = ""

                    If Not DBNull.Value.Equals(oRowMapSize.Item(tColFTSize)) Then
                        tTextFTSize = oRowMapSize.Item(tColFTSize).ToString.Trim
                    End If
                    If tTextFTSize.ToLower = "CUST1".ToLower Then
                        Beep()
                    End If
                    If tTextFTSize <> "" Then

                        Dim oRowAppendMapSize As System.Data.DataRow

                        If Not DTMapSizeTemp Is Nothing AndAlso DTMapSizeTemp.Rows.Count > 0 Then
                            Dim oRowMapSizeNotExists As System.Data.DataRow()
                            oRowMapSizeNotExists = DTMapSizeTemp.Select("FTMapSizeCode = '" & HI.UL.ULF.rpQuoted(tTextFTSize) & "'")

                            If oRowMapSizeNotExists.Length > 0 Then
                                '...already exists
                            Else
                                oRowAppendMapSize = DTMapSizeTemp.NewRow()
                                oRowAppendMapSize.Item("FTMapSizeCode") = tTextFTSize
                                DTMapSizeTemp.Rows.Add(oRowAppendMapSize)
                            End If

                        Else
                            oRowAppendMapSize = DTMapSizeTemp.NewRow()
                            oRowAppendMapSize.Item("FTMapSizeCode") = tTextFTSize
                            DTMapSizeTemp.Rows.Add(oRowAppendMapSize)
                        End If

                    End If

                Next

                If Not DTMapSizeTemp Is Nothing Then DTMapSizeTemp.AcceptChanges()

                If Not DTMapSizeTemp Is Nothing AndAlso DTMapSizeTemp.Rows.Count > 0 Then

                    Dim tMatchSize As String

                    For Each oRowValidateMapSize As System.Data.DataRow In DTMapSizeTemp.Rows

                        tMatchSize = ""

                        If Not DBNull.Value.Equals(oRowValidateMapSize.Item("FTMapSizeCode")) Then
                            tMatchSize = oRowValidateMapSize.Item("FTMapSizeCode").ToString.Trim
                        End If

                        If tMatchSize <> "" Then

                            '...Lookup map size
                            '------------------------------------------------------------------------------------------------
                            If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
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
                            '------------------------------------------------------------------------------------------------

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTMatSizeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK) WHERE A.FTMatSizeCode = N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "';"

                            '...Mat Size Code not exists in system
                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                '...not match size code in system database TMERMMatSize [MASTER]

                                If paramDTNotMapSize.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'").Length <= 0 Then
                                    Dim oDataRow As System.Data.DataRow

                                    oDataRow = paramDTNotMapSize.NewRow()

                                    oDataRow.Item("FTSizeCodeNotExists") = tMatchSize
                                    oDataRow.Item("FTMapSizeExtend") = ""

                                    paramDTNotMapSize.Rows.Add(oDataRow)

                                End If

                            End If

                        End If

                    Next

                End If


                '------------------------------------------------------------------------------------------------------------------------

                If Not paramDTNotMapSize Is Nothing AndAlso paramDTNotMapSize.Rows.Count > 0 Then paramDTNotMapSize.AcceptChanges()

                bRet = True

            End If

        Catch ex As Exception
            
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterStyle(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

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

                Dim tColStyleNumber As String = "F6"
                Dim tColProdStyleName As String = "F36"

                Dim nLoopStyle As Integer

                For nLoopStyle = 0 To oDBdt.Rows.Count - 1

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColStyleNumber)) Then

                        If oDBdt.Rows(nLoopStyle).Item(tColStyleNumber).ToString() <> "" Then

                            Dim tStyleCode As String, tStyleName As String

                            tStyleCode = "" : tStyleName = ""

                            tStyleCode = oDBdt.Rows(nLoopStyle).Item(tColStyleNumber).ToString()
                            tStyleCode = tStyleCode.Trim()

                            tStyleName = oDBdt.Rows(nLoopStyle).Item(tColProdStyleName).ToString()
                            tStyleName = tStyleName.Trim()

                            Dim oRow As DataRow
                            If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                                Dim oDataRowExists As DataRow()
                                oDataRowExists = oDBdtStyle.Select("FTStyleCode = '" & HI.UL.ULF.rpQuoted(tStyleCode) & "'")
                                If oDataRowExists.Length > 0 Then
                                    '...already exists
                                Else
                                    oRow = oDBdtStyle.NewRow()
                                    oRow.Item("FTStyleCode") = tStyleCode
                                    oRow.Item("FTStyleName") = tStyleName
                                    oDBdtStyle.Rows.Add(oRow)
                                End If

                            Else
                                oRow = oDBdtStyle.NewRow()
                                oRow.Item("FTStyleCode") = tStyleCode
                                oRow.Item("FTStyleName") = tStyleName
                                oDBdtStyle.Rows.Add(oRow)
                            End If

                            'tSql = ""
                            'tSql &= "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle "
                            'tSql &= "(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive) "
                            'tSql &= Environment.NewLine & "SELECT  TOP 1  FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTStyleCode"
                            'tSql &= Environment.NewLine & ", FTStyleNameTH, FTStyleNameEN, FNHSysCustId"
                            'tSql &= Environment.NewLine & ", ISNULL((SELECT TOP 1 FNHSysSeasonId"
                            'tSql &= Environment.NewLine & "          FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS A  WITH(NOLOCK)"
                            'tSql &= Environment.NewLine & "          WHERE A.FTSeasonCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonId.Text.Trim) & "'"
                            'tSql &= Environment.NewLine & "         ), 0) AS  FNHSysSeasonId"
                            'tSql &= Environment.NewLine & ", FTStateActive"
                            'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A  WITH(NOLOCK)"
                            'tSql &= Environment.NewLine & "WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"
                            'tSql &= Environment.NewLine & "      AND  NOT (A.FTStyleCode IN (SELECT FTStyleCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle))"

                            'If HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            

                            'End If

                        End If

                    End If

                Next nLoopStyle

                If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                    oDBdtStyle.AcceptChanges()
                End If

                bRet = True

            End If

        Catch ex As Exception
            
        End Try

        Return bRet

    End Function

    Private Function W_PRCbSaveListOrderSalesManToTemp(ByVal oDBdtSrc As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Dim bFlagSaveImportHDSalesman As Boolean = False
        Dim bFlagSaveImportDTSalesman As Boolean = False
        Try
            If Not oDBdtSrc Is Nothing AndAlso oDBdtSrc.Rows.Count > 0 Then

                tSql = ""
                tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                tSql &= Environment.NewLine & "DELETE B FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS B WHERE B.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                End If

                '-------------------------------------------------------------------------------------------------------------
                Dim DTImporSalesmanHDTemp As System.Data.DataTable

                DTImporSalesmanHDTemp = New System.Data.DataTable

                Dim oColFTPONo As System.Data.DataColumn
                oColFTPONo = New System.Data.DataColumn("FTPONo", System.Type.GetType("System.String"))
                oColFTPONo.Caption = "FTPONo"
                DTImporSalesmanHDTemp.Columns.Add(oColFTPONo.ColumnName, oColFTPONo.DataType)

                Dim oColFTStyleNo As System.Data.DataColumn
                oColFTStyleNo = New System.Data.DataColumn("FTStyleNo", System.Type.GetType("System.String"))
                oColFTStyleNo.Caption = "FTStyleNo"
                DTImporSalesmanHDTemp.Columns.Add(oColFTStyleNo.ColumnName, oColFTStyleNo.DataType)

                Dim oColFTProdStyleName As System.Data.DataColumn
                oColFTProdStyleName = New System.Data.DataColumn("FTProdStyleName", System.Type.GetType("System.String"))
                oColFTProdStyleName.Caption = "FTProdStyleName"
                DTImporSalesmanHDTemp.Columns.Add(oColFTProdStyleName.ColumnName, oColFTProdStyleName.DataType)

                Dim oColFDOrderDate As System.Data.DataColumn
                oColFDOrderDate = New System.Data.DataColumn("FDOrderDate", System.Type.GetType("System.String"))
                oColFDOrderDate.Caption = "FDOrderDate"
                DTImporSalesmanHDTemp.Columns.Add(oColFDOrderDate.ColumnName, oColFDOrderDate.DataType)

                Dim oColFTMainCategoryCode As System.Data.DataColumn
                oColFTMainCategoryCode = New System.Data.DataColumn("FTMainCategoryCode", System.Type.GetType("System.String"))
                oColFTMainCategoryCode.Caption = "FTMainCategoryCode"
                DTImporSalesmanHDTemp.Columns.Add(oColFTMainCategoryCode.ColumnName, oColFTMainCategoryCode.DataType)

                Dim oColFTBuyGrpCode As System.Data.DataColumn
                oColFTBuyGrpCode = New System.Data.DataColumn("FTBuyGrpCode", System.Type.GetType("System.String"))
                oColFTBuyGrpCode.Caption = "FTBuyGrpCode"
                DTImporSalesmanHDTemp.Columns.Add(oColFTBuyGrpCode.ColumnName, oColFTBuyGrpCode.DataType)

                Dim oColFTProdTypeCode As System.Data.DataColumn
                oColFTProdTypeCode = New System.Data.DataColumn("FTProdTypeCode", System.Type.GetType("System.String"))
                oColFTProdTypeCode.Caption = "FTProdTypeCode"
                DTImporSalesmanHDTemp.Columns.Add(oColFTProdTypeCode.ColumnName, oColFTProdTypeCode.DataType)

                Dim oColFTSeaSonCode As System.Data.DataColumn
                oColFTSeaSonCode = New System.Data.DataColumn("FTSeasonCode", System.Type.GetType("System.String"))
                oColFTSeaSonCode.Caption = "FTSeasonCode"
                DTImporSalesmanHDTemp.Columns.Add(oColFTSeaSonCode.ColumnName, oColFTSeaSonCode.DataType)

                Dim oColFTMerTeamCode As System.Data.DataColumn
                oColFTMerTeamCode = New System.Data.DataColumn("FTMerTeamCode", System.Type.GetType("System.String"))
                oColFTMerTeamCode.Caption = "FTMerTeamCode"
                DTImporSalesmanHDTemp.Columns.Add(oColFTMerTeamCode.ColumnName, oColFTMerTeamCode.DataType)

                Dim oColSubPG As System.Data.DataColumn
                oColSubPG = New System.Data.DataColumn("FTSubPGM", System.Type.GetType("System.String"))
                oColSubPG.Caption = "FTSubPGM"
                DTImporSalesmanHDTemp.Columns.Add(oColSubPG.ColumnName, oColSubPG.DataType)


                Dim oColCmpCode As System.Data.DataColumn
                oColCmpCode = New System.Data.DataColumn("FTCmp", System.Type.GetType("System.String"))
                oColCmpCode.Caption = "FTCmp"
                DTImporSalesmanHDTemp.Columns.Add(oColCmpCode.ColumnName, oColCmpCode.DataType)
                '-------------------------------------------------------------------------------------------------------------

                '============================================ represent to temp TMERTImportSalesmanTemp =======================================================
                Dim tTextFTPONo As String '...F1
                Dim tTextFDOrderDate As String
                Dim tTextFTStyleNo As String '...F6
                Dim tTextFTProdStyleName As String '...F35
                Dim tTextFTMainCategoryCode As String '...F37
                Dim tTextFTBuyGrpCode As String
                Dim tTextFTMerTeamCode As String  '...F5
                Dim tTextFTProdTypeCode As String '...F37 + F36 {1} + F38 {1} ==> AK : FB:Footbal, BB:Basketball || AJ : CMO_Sponsor_Bus_Org || AL : CMO_Sponsor_Consumer_Purpose
                Dim tTextFTSeaSonCode As String

                Dim tTextFTSubPGM As String  '...F54
                Dim tTextFTCmpCode As String '...F55

                Dim oRowAppendImportHD As System.Data.DataRow

                For Each oRowImportSalesman As System.Data.DataRow In oDBdtSrc.Rows

                    If Not DBNull.Value.Equals(oRowImportSalesman.Item("F1")) AndAlso Not DBNull.Value.Equals(oRowImportSalesman.Item("F6")) Then

                        tTextFTPONo = oRowImportSalesman.Item("F1").ToString.Trim
                        tTextFTStyleNo = oRowImportSalesman.Item("F6").ToString.Trim

                        If tTextFTPONo <> "" AndAlso tTextFTStyleNo <> "" Then

                            tTextFDOrderDate = Me.FDOrderDate.Text
                            tTextFTProdStyleName = oRowImportSalesman.Item("F36").ToString.Trim
                            tTextFTMainCategoryCode = oRowImportSalesman.Item("F38").ToString.Trim
                            tTextFTBuyGrpCode = Me.FNHSysBuyGrpId.Text.Trim
                            tTextFTMerTeamCode = oRowImportSalesman.Item("F5").ToString.Trim
                            tTextFTSeaSonCode = Me.FNHSysSeasonId.Text.Trim

                            Dim tTextCategoryCode As String, tTextCategoryExtend As String, tTextSubCategoryExtend As String

                            tTextCategoryCode = "" : tTextCategoryExtend = "" : tTextSubCategoryExtend = ""

                            tTextCategoryCode = oRowImportSalesman.Item("F38").ToString.Trim
                            tTextCategoryExtend = oRowImportSalesman.Item("F37").ToString.Trim
                            tTextSubCategoryExtend = oRowImportSalesman.Item("F39").ToString.Trim

                            tTextFTSubPGM = oRowImportSalesman.Item("F54").ToString.Trim
                            tTextFTCmpCode = oRowImportSalesman.Item("F55").ToString.Trim

                            tTextFTProdTypeCode = tTextCategoryCode & Microsoft.VisualBasic.Mid$(tTextCategoryExtend, 1, 1) & Microsoft.VisualBasic.Mid$(tTextSubCategoryExtend, 1, 1)

                            If Not DTImporSalesmanHDTemp Is Nothing AndAlso DTImporSalesmanHDTemp.Rows.Count > 0 Then
                                Dim oRowSalesmanNotExists As System.Data.DataRow()

                                oRowSalesmanNotExists = DTImporSalesmanHDTemp.Select("FTPONo = '" & HI.UL.ULF.rpQuoted(tTextFTPONo) & "' AND FTStyleNo = '" & HI.UL.ULF.rpQuoted(tTextFTStyleNo) & "'")

                                If oRowSalesmanNotExists.Length > 0 Then
                                    '...already exists record
                                Else
                                    oRowAppendImportHD = DTImporSalesmanHDTemp.NewRow()

                                    With oRowAppendImportHD
                                        .Item("FTPONo") = tTextFTPONo
                                        .Item("FTStyleNo") = tTextFTStyleNo
                                        .Item("FTProdStyleName") = tTextFTProdStyleName
                                        .Item("FDOrderDate") = tTextFDOrderDate
                                        .Item("FTMainCategoryCode") = tTextFTMainCategoryCode
                                        .Item("FTBuyGrpCode") = tTextFTBuyGrpCode
                                        .Item("FTProdTypeCode") = tTextFTProdTypeCode
                                        .Item("FTSeasonCode") = tTextFTSeaSonCode
                                        .Item("FTMerTeamCode") = tTextFTMerTeamCode
                                        .Item("FTSubPGM") = tTextFTSubPGM
                                        .Item("FTCmp") = tTextFTCmpCode
                                    End With

                                    DTImporSalesmanHDTemp.Rows.Add(oRowAppendImportHD)

                                End If

                            Else
                                oRowAppendImportHD = DTImporSalesmanHDTemp.NewRow()

                                With oRowAppendImportHD
                                    .Item("FTPONo") = tTextFTPONo
                                    .Item("FTStyleNo") = tTextFTStyleNo
                                    .Item("FTProdStyleName") = tTextFTProdStyleName
                                    .Item("FDOrderDate") = tTextFDOrderDate
                                    .Item("FTMainCategoryCode") = tTextFTMainCategoryCode
                                    .Item("FTBuyGrpCode") = tTextFTBuyGrpCode
                                    .Item("FTProdTypeCode") = tTextFTProdTypeCode
                                    .Item("FTSeasonCode") = tTextFTSeaSonCode
                                    .Item("FTMerTeamCode") = tTextFTMerTeamCode
                                    .Item("FTSubPGM") = tTextFTSubPGM
                                    .Item("FTCmp") = tTextFTCmpCode
                                End With

                                DTImporSalesmanHDTemp.Rows.Add(oRowAppendImportHD)

                            End If

                        End If

                    End If

                Next

                If Not DTImporSalesmanHDTemp Is Nothing Then DTImporSalesmanHDTemp.AcceptChanges()

                Dim oStrBuilder As New System.Text.StringBuilder()

                If Not DTImporSalesmanHDTemp Is Nothing AndAlso DTImporSalesmanHDTemp.Rows.Count > 0 Then
                    '...iterate loop save list to temp : TMERTImportSalesmanTemp

                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    For Each oRowAddToTempSalesman As System.Data.DataRow In DTImporSalesmanHDTemp.Rows

                        tTextFTPONo = "" : tTextFTStyleNo = "" : tTextFTProdStyleName = ""
                        tTextFDOrderDate = "" : tTextFTMainCategoryCode = "" : tTextFTBuyGrpCode = ""
                        tTextFTProdTypeCode = "" : tTextFTSeaSonCode = "" : tTextFTMerTeamCode = ""
                        tTextFTCmpCode = ""
                        tTextFTCmpCode = ""

                        With oRowAddToTempSalesman

                            tTextFTPONo = .Item("FTPONo").ToString
                            tTextFTStyleNo = .Item("FTStyleNo").ToString
                            tTextFTProdStyleName = .Item("FTProdStyleName").ToString
                            tTextFDOrderDate = .Item("FDOrderDate").ToString
                            tTextFTMainCategoryCode = .Item("FTMainCategoryCode").ToString
                            tTextFTBuyGrpCode = .Item("FTBuyGrpCode").ToString
                            tTextFTProdTypeCode = .Item("FTProdTypeCode").ToString
                            tTextFTSeaSonCode = .Item("FTSeasonCode").ToString
                            tTextFTMerTeamCode = .Item("FTMerTeamCode").ToString
                            tTextFTBuyGrpCode = .Item("FTBuyGrpCode").ToString
                            tTextFTProdTypeCode = .Item("FTProdTypeCode").ToString
                            tTextFTSeaSonCode = .Item("FTSeasonCode").ToString

                            tTextFTSubPGM = .Item("FTSubPGM")
                            tTextFTCmpCode = .Item("FTCmp")

                        End With

                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportSalesmanTemp]([FTUserLogIn],[FTPONo],[FDOrderDate],[FTStyleNo],[FTProdStyleName]")
                        oStrBuilder.AppendLine("										,[FNHSysMainCategoryId],[FTMainCategoryDesc],[FNHSysBuyGrpId],[FTBuyGrpNameDesc]")
                        oStrBuilder.AppendLine("										,[FNHSysMerTeamId],[FNHSysProdTypeId],[FNHSysSeasonId],[FTGenerateOrderNo],FTSubPGM,FTCmp)")
                        oStrBuilder.AppendLine("SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', '" & HI.UL.ULF.rpQuoted(tTextFTPONo) & "' FTPONo, N'" & HI.UL.ULDate.ConvertEnDB(tTextFDOrderDate) & "' AS FDOrderDate, N'" & HI.UL.ULF.rpQuoted(tTextFTStyleNo) & "' FTStyleNo, N'" & HI.UL.ULF.rpQuoted(tTextFTProdStyleName) & "' AS FTProdStyleName, ")
                        oStrBuilder.AppendLine("      (SELECT TOP 1 L1.FNHSysMainCategoryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainCategoryType AS L1 (NOLOCK) WHERE L1.FTMainCategoryCode = N'" & HI.UL.ULF.rpQuoted(tTextFTMainCategoryCode) & "')  AS FNHSysMainCategoryId,")
                        oStrBuilder.AppendLine("      (SELECT TOP 1 L2.FTMainCategoryDescEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainCategoryType AS L2 (NOLOCK) WHERE L2.FTMainCategoryCode = N'" & HI.UL.ULF.rpQuoted(tTextFTMainCategoryCode) & "') AS FTMainCategoryDesc,")
                        oStrBuilder.AppendLine("      (SELECT TOP 1 L3.FNHSysBuyGrpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS L3 (NOLOCK) WHERE L3.FTBuyGrpCode = N'" & HI.UL.ULF.rpQuoted(tTextFTBuyGrpCode) & "') AS NHSysBuyGrpId,")
                        oStrBuilder.AppendLine("      (SELECT TOP 1 L4.FTBuyGrpNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS L4 (NOLOCK) WHERE L4.FTBuyGrpCode = N'" & HI.UL.ULF.rpQuoted(tTextFTBuyGrpCode) & "') AS FTBuyGrpNameDesc,")
                        oStrBuilder.AppendLine("      (SELECT TOP 1 L5.FNHSysMerTeamId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMerTeam AS L5 (NOLOCK) WHERE L5.FTMerTeamCode = N'" & HI.UL.ULF.rpQuoted(tTextFTMerTeamCode) & "') AS FNHSysMerTeamId,")
                        oStrBuilder.AppendLine("      (SELECT TOP 1 L6.FNHSysProdTypeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMProductType AS L6 (NOLOCK) WHERE L6.FTProdTypeCode = N'" & HI.UL.ULF.rpQuoted(tTextFTProdTypeCode) & "') AS FNHSysProdTypeId,")
                        oStrBuilder.AppendLine("      (SELECT TOP 1 L7.FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMSeason AS L7 (NOLOCK) WHERE L7.FTSeasonCode = N'" & HI.UL.ULF.rpQuoted(tTextFTSeaSonCode) & "') AS FNHSysSeasonId,")
                        oStrBuilder.AppendLine("      NULL AS FTGenerateOrderNo,'" & HI.UL.ULF.rpQuoted(tTextFTSubPGM) & "','" & HI.UL.ULF.rpQuoted(tTextFTCmpCode) & "';")

                    Next

                    If oStrBuilder.Length > 0 Then
                        tSql = ""
                        tSql = oStrBuilder.ToString()

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                            bFlagSaveImportHDSalesman = True


                        End If

                    End If

                End If

                If Not DTImporSalesmanHDTemp Is Nothing Then DTImporSalesmanHDTemp.Dispose()

                '=================================================== represent to temp TMERTImportSalesmanSizeBreakdownTemp ============================================
                If bFlagSaveImportHDSalesman = True Then
                    '...represent by & order by PO. Number, Style Number, CUST_XREF, AF_REQ, CU_CD, CLR_COD, SIZE

                    'SELECT [FTPONo] : F1 , [FTStyleNo] : F6, [FTCUST_XREF] : F4
                    '      ,[FTAF_REQ] : F16, [FTCU_CD] : F18, [FTCURRNT_ETS] : 12
                    '      ,[FTORIGNL_ETS] : F9, [FTCMO_Consumer_Group] : F39, [FTColorwayCode] : F7
                    '      ,[FTSizeBreakdownCode] : F20, [FTMD_TR] : F15, [FNQuantity] : F8
                    'FROM [HITECH_MERCHAN].[dbo].[TMERTImportSalesmanSizeBreakdownTemp]

                    '...FTUserLogIn, FTPONo, FTStyleNo, FTCUST_XREF, FTCU_CD
                    '...FTCURRNT_ETS, FTORIGNL_ETS, FTCMO_Consumer_Group, 
                    '...FTColorwayCode, FTSizeBreakdownCode, FTMD_TR, FNQuantity

                    Dim DVImportSalesmanDTTemp As System.Data.DataView = oDBdtSrc.DefaultView

                    DVImportSalesmanDTTemp.Sort = "F1 ASC, F6 ASC, F16 ASC, F18, F4 ASC, F7 ASC, F20 ASC"

                    Dim DTImportSalesmanDTTemp As System.Data.DataTable = DVImportSalesmanDTTemp.ToTable()

                    If Not DTImportSalesmanDTTemp Is Nothing AndAlso DTImportSalesmanDTTemp.Rows.Count > 0 Then

                        Dim tColFTPONo As String = "F1"
                        Dim tColFTStyleNo As String = "F6"
                        Dim tColFTCUST_XREF As String = "F4"
                        Dim tColFTAF_REQ As String = "F17"
                        Dim tColFTCU_CD As String = "F19"
                        Dim tColFTCURRNT_ETS As String = "F13", tColFTORIGNL_ETS As String = "F10"
                        Dim tColFTCMO_Consumer_Group As String = "F40"
                        Dim tColFTColorwayCode As String = "F7"
                        Dim tColFTSizeBreakdownCode As String = "F21"
                        Dim tColFTMD_TR As String = "F16"
                        Dim tColFNQuantity As String = "F8"
                        Dim tColFNExternalQuantity As String = "F9"

                        oStrBuilder.Remove(0, oStrBuilder.Length)

                        Dim tTmpFTPONo As String
                        Dim tTmpFTStyleNo As String
                        Dim tTmpFTCUST_XREF As String
                        Dim tTmpFTAF_REQ As String
                        Dim tTmpFTCU_CD As String
                        Dim tTmpFTCURRNT_ETS As String
                        Dim tTmpFTORIGNL_ETS As String
                        Dim tTmpFTCMO_Consumer_Group As String
                        Dim tTmpFTColorwayCode As String
                        Dim tTmpFTSizeBreakdownCode As String
                        Dim tTmpFTMD_TR As String
                        Dim nTmpFNQuantity As Integer
                        Dim nTmpFNExternalQuantity As Integer

                        oStrBuilder.AppendLine("DECLARE @FTYearYY_12 AS VARCHAR(2);")
                        oStrBuilder.AppendLine("SELECT @FTYearYY_12 = LEFT(YEAR(GETDATE()), 2);")
                        oStrBuilder.AppendLine("PRINT '@FTYearYY_12 : ' + @FTYearYY_12;")

                        For Each oRowImportSalesmanTmp As System.Data.DataRow In DTImportSalesmanDTTemp.Rows

                            tTmpFTPONo = "" : tTmpFTStyleNo = ""
                            tTmpFTCUST_XREF = "" : tTmpFTAF_REQ = "" : tTmpFTCU_CD = ""
                            tTmpFTCURRNT_ETS = "" : tTmpFTORIGNL_ETS = ""
                            tTmpFTCMO_Consumer_Group = "" : tTmpFTColorwayCode = "" : tTmpFTSizeBreakdownCode = ""
                            tTmpFTMD_TR = "" : nTmpFNQuantity = 0
                            nTmpFNExternalQuantity = 0

                            With oRowImportSalesmanTmp

                                tTmpFTPONo = .Item(tColFTPONo).ToString.Trim
                                tTmpFTStyleNo = .Item(tColFTStyleNo).ToString.Trim
                                tTmpFTCUST_XREF = .Item(tColFTCUST_XREF).ToString.Trim
                                tTmpFTAF_REQ = .Item(tColFTAF_REQ).ToString.Trim
                                tTmpFTCU_CD = .Item(tColFTCU_CD).ToString.Trim
                                tTmpFTCURRNT_ETS = .Item(tColFTCURRNT_ETS).ToString.Trim
                                tTmpFTORIGNL_ETS = .Item(tColFTORIGNL_ETS).ToString.Trim
                                tTmpFTCMO_Consumer_Group = .Item(tColFTCMO_Consumer_Group).ToString.Trim
                                tTmpFTColorwayCode = .Item(tColFTColorwayCode).ToString.Trim
                                tTmpFTSizeBreakdownCode = .Item(tColFTSizeBreakdownCode).ToString.Trim

                                '...validate map size ex. XXL ==> 2XL, XXXL ==> 3XL, XXXXL ==> 4XL
                                If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                                    Dim tMapSizeExtension As String = ""
                                    Dim oDataRow As DataRow()
                                    Try
                                        oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tTmpFTSizeBreakdownCode) & "'")
                                        If oDataRow.Length > 0 Then

                                            If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                                tTmpFTSizeBreakdownCode = oDataRow(0)("FTMapSizeExtension").ToString()
                                            End If

                                        Else
                                            '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                            If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                                Dim oDataRowMapSizeManual As System.Data.DataRow()
                                                Try
                                                    oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tTmpFTSizeBreakdownCode) & "'")
                                                    If oDataRowMapSizeManual.Length > 0 Then

                                                        If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                            tTmpFTSizeBreakdownCode = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
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
                                            oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tTmpFTSizeBreakdownCode) & "'")
                                            If oDataRowMapSizeManual.Length > 0 Then

                                                If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                    tTmpFTSizeBreakdownCode = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                End If

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    End If

                                End If

                                tTmpFTMD_TR = .Item(tColFTMD_TR).ToString.Trim

                                If Not DBNull.Value.Equals(.Item(tColFNQuantity)) Then
                                    nTmpFNQuantity = Val(.Item(tColFNQuantity))
                                End If


                                If Not DBNull.Value.Equals(.Item(tColFNExternalQuantity)) Then
                                    nTmpFNExternalQuantity = Val(.Item(tColFNExternalQuantity))
                                End If


                            End With

                            If tTmpFTPONo <> "" AndAlso tTmpFTStyleNo <> "" AndAlso tTmpFTCUST_XREF <> "" AndAlso tTmpFTAF_REQ <> "" AndAlso tTmpFTColorwayCode <> "" AndAlso tTmpFTSizeBreakdownCode <> "" Then

                                ' oStrBuilder.AppendLine("IF NOT EXISTS (SELECT TOP 1 A.FTPONo")
                                oStrBuilder.AppendLine(" UPDATE A SET FNQuantity=FNQuantity+" & nTmpFNQuantity & ", FNExternalQuantity=FNExternalQuantity+" & nTmpFNExternalQuantity & "")
                                oStrBuilder.AppendLine("               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A ")
                                oStrBuilder.AppendLine("			   WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
                                oStrBuilder.AppendLine("			         AND A.FTPONo = N'" & HI.UL.ULF.rpQuoted(tTmpFTPONo) & "'")
                                oStrBuilder.AppendLine("					 AND A.FTStyleNo = N'" & HI.UL.ULF.rpQuoted(tTmpFTStyleNo) & "'")
                                oStrBuilder.AppendLine("					 AND A.FTCUST_XREF = N'" & HI.UL.ULF.rpQuoted(tTmpFTCUST_XREF) & "'")
                                oStrBuilder.AppendLine("                     AND A.FTAF_REQ = N'" & HI.UL.ULF.rpQuoted(tTmpFTAF_REQ) & "'")
                                oStrBuilder.AppendLine("					 AND A.FTCU_CD = N'" & HI.UL.ULF.rpQuoted(tTmpFTCU_CD) & "'")
                                oStrBuilder.AppendLine("					 AND A.FTColorwayCode = N'" & HI.UL.ULF.rpQuoted(tTmpFTColorwayCode) & "'")
                                oStrBuilder.AppendLine("					 AND A.FTSizeBreakdownCode = N'" & HI.UL.ULF.rpQuoted(tTmpFTSizeBreakdownCode) & "'")
                                'oStrBuilder.AppendLine("					 AND A.FTCURRNT_ETS = N'" & HI.UL.ULF.rpQuoted(tTmpFTCURRNT_ETS) & "'")
                                oStrBuilder.AppendLine("					 AND A.FTCURRNT_ETS=" & String.Format(" (SELECT CONVERT(VARCHAR(10), CAST((@FTYearYY_12 + '{0}') AS DATE), 111))", tTmpFTORIGNL_ETS) & "")
                                ' oStrBuilder.AppendLine(")")
                                oStrBuilder.AppendLine(" IF @@RowCount <=0 ")
                                oStrBuilder.AppendLine("BEGIN")
                                oStrBuilder.AppendLine("  --PRINT 'PROMP APPEND NEW RECORD Import Salesman Colorway Size Breakdown...';")
                                oStrBuilder.AppendLine("  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportSalesmanSizeBreakdownTemp] ([FTUserLogIn],[FTPONo],[FTStyleNo],[FTCUST_XREF],[FTAF_REQ],[FTCU_CD],[FTCURRNT_ETS],[FTORIGNL_ETS],[FTCMO_Consumer_Group],[FTColorwayCode],[FTSizeBreakdownCode],[FTMD_TR],[FNQuantity],[FNExternalQuantity])")
                                oStrBuilder.AppendLine(String.Format("  SELECT N'{0}' AS [FTUserLogIn], N'{1}' AS [FTPONo], N'{2}' AS [FTStyleNo], N'{3}' AS [FTCUST_XREF], N'{4}' AS [FTAF_REQ], N'{5}' AS [FTCU_CD]", {HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName), HI.UL.ULF.rpQuoted(tTmpFTPONo), HI.UL.ULF.rpQuoted(tTmpFTStyleNo), HI.UL.ULF.rpQuoted(tTmpFTCUST_XREF), HI.UL.ULF.rpQuoted(tTmpFTAF_REQ), HI.UL.ULF.rpQuoted(tTmpFTCU_CD)}))
                                oStrBuilder.AppendLine(String.Format("		,(SELECT CONVERT(VARCHAR(10), CAST((@FTYearYY_12 + '{0}') AS DATE), 111)) AS [FTCURRNT_ETS]", tTmpFTCURRNT_ETS))
                                oStrBuilder.AppendLine(String.Format("		,(SELECT CONVERT(VARCHAR(10), CAST((@FTYearYY_12 + '{0}') AS DATE), 111)) AS [FTORIGNL_ETS]", tTmpFTORIGNL_ETS))
                                oStrBuilder.AppendLine(String.Format("		,N'{0}' AS [FTCMO_Consumer_Group], N'{1}' AS [FTColorwayCode], N'{2}' AS [FTSizeBreakdownCode], N'{3}' AS [FTMD_TR], {4}  AS [FNQuantity], {5}  AS [FNExternalQuantity]", {HI.UL.ULF.rpQuoted(tTmpFTCMO_Consumer_Group), HI.UL.ULF.rpQuoted(tTmpFTColorwayCode), HI.UL.ULF.rpQuoted(tTmpFTSizeBreakdownCode), HI.UL.ULF.rpQuoted(tTmpFTMD_TR), nTmpFNQuantity, nTmpFNExternalQuantity}))

                                oStrBuilder.AppendLine("END;")

                            End If

                        Next

                    End If

                    If Not DTImportSalesmanDTTemp Is Nothing Then DTImportSalesmanDTTemp.Dispose()

                    If oStrBuilder.Length > 0 Then

                        tSql = ""
                        tSql = oStrBuilder.ToString()

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                            bFlagSaveImportDTSalesman = True

                        End If

                    End If
                End If

                If bFlagSaveImportDTSalesman = True Then bRet = True

            End If

        Catch ex As Exception
        End Try

        Return bRet

    End Function

    Private Function W_PRCbSaveListOrderToTemp(ByVal oDBdtSrc As System.Data.DataTable) As Boolean
        '...catch raw data before confirm import factory order no.

        Dim bAppendSrcHeader As Boolean = True
        Dim bAppendSrcDetial As Boolean = True
        Dim bRet As Boolean = False

        Try
            If Not oDBdtSrc Is Nothing And oDBdtSrc.Rows.Count > 0 Then

                '...clear temp import salesman
                tSql = ""
                tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                  
                End If

                tSql = ""
                tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                  
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
                        If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F39")) Then
                            If oDBdtSrc.Rows(nLoopOrder).Item("F39").ToString() <> "" Then
                                tFTCountry = oDBdtSrc.Rows(nLoopOrder).Item("F39").ToString().Trim()
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

                        Dim tStyleCode As String

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
                                tStyleCode = tStyle '& tPlanSeasonStyle & tYearStyle
                        End Select


                        Dim tStyleDesc As String
                        tStyleDesc = tMaterialStyle & "-" & tPlanSeasonStyle & "-" & tYearStyle

                        Dim objCulture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US", True)

                        objCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                        objCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

                        System.Threading.Thread.CurrentThread.CurrentCulture = objCulture

                        Dim tDateF8 As String

                        tDateF8 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F8"))

                        Dim tDateF9 As String

                        tDateF9 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F9"))

                        Dim tDateF41 As String

                        tDateF41 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F41"))

                        Dim tDateF42 As String

                        tDateF42 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F42"))

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
                        oStrBuilder.AppendLine("                                                         ,[FNHSysProdTypeId])")
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
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT A.FNHSysBuyerId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS A WITH(NOLOCK) " & _
                                                                                           "WHERE A.FTBuyerCode = N'" & HI.UL.ULF.rpQuoted(tFTBuyer) & "'), ISNULL((SELECT L1.FNHSysBuyerId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) " & _
                                                                                           "WHERE L1.FTBuyerCode = N'-'), NULL)))"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F36")), "", oDBdtSrc.Rows(nLoopOrder).Item("F36"))) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F37")), "", oDBdtSrc.Rows(nLoopOrder).Item("F37"))) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT L1.FNHSysCountryId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) " & _
                                                                                           "WHERE  L1.FTCountryCode =  N'" & HI.UL.ULF.rpQuoted(tFTCountry) & "' " & _
                                                                                           "OR (L1.FTCountryNameEN = N'" & HI.UL.ULF.rpQuoted(tFTCountry) & "' OR L1.FTCountryNameTH = N'" & HI.UL.ULF.rpQuoted(tFTCountry) & "')), ISNULL((SELECT L2.FNHSysCountryId " & _
                                                                                                                                                                              "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L2 WITH(NOLOCK) " & _
                                                                                                                                                                              "WHERE L2.FTCountryCode = N'-'), NULL)))"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F39")), "", oDBdtSrc.Rows(nLoopOrder).Item("F39"))) & "'"))

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

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT A.FNHSysMerTeamId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK)" & _
                                                                                           "WHERE A.FTMerTeamCode = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F14")), "", oDBdtSrc.Rows(nLoopOrder).Item("F14"))) & "'),NULL))"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT TOP 1 A.FNHSysProdTypeId " & _
                                                                            "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS A WITH(NOLOCK)" & _
                                                                            "WHERE (A.FTProdTypeNameEN = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F11")), "", oDBdtSrc.Rows(nLoopOrder).Item("F11"))) & "' OR A.FTProdTypeNameTH = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F11")), "", oDBdtSrc.Rows(nLoopOrder).Item("F11"))) & "')),NULL))" & ")"))

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
                                        tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "HSC"
                                    Case "HIP/DP"
                                        tStyleCode = tStyle & "/HIP"
                                    Case "HIP/MS"
                                        tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "MS"
                                    Case Else
                                        tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle
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
                                Else
                                    tFTNetUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 2)) Then
                                    tFTTradingCoNetUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 2)
                                Else
                                    tFTTradingCoNetUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 3)) Then
                                    tFTGrossUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 3)
                                Else
                                    tFTGrossUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 4)) Then
                                    tFTTradingCoGrossUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 4)
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

                                'Application.DoEvents()

                            Loop

                        End If

                    Next nLoopSizeBreakdown

                End If

                bRet = True

            End If

        Catch ex As Exception

        End Try

        Return bRet

    End Function

    'Private Function W_PRCbShowBrowseDataImportFactoryOrder() As Boolean
    '    Dim bRet As Boolean = False

    '    Try
    '        Dim oDBdtStatic As System.Data.DataTable
    '        Dim oDBdtDynamic As System.Data.DataTable
    '        Dim oDBdtMatSize As System.Data.DataTable

    '        tSql = ""
    '        tSql = ";WITH cteImportSize(FTMatSize)"
    '        tSql &= Environment.NewLine & "AS (SELECT DISTINCT L1.FTSizeBreakdownCode  AS FTSizeBreakdownCode"
    '        tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanSizeBreakdownTemp] AS L1"
    '        tSql &= Environment.NewLine & "    WHERE L1.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        tSql &= Environment.NewLine & "    )"
    '        tSql &= Environment.NewLine & "--SELECT B.FNHSysMatSizeId, A.FTMatSize, B.FTMatSizeNameEN, B.FTMatSizeNameEN"
    '        tSql &= Environment.NewLine & "SELECT B.FTMatSizeCode"
    '        tSql &= Environment.NewLine & "FROM cteImportSize AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS B WITH(NOLOCK) ON A.FTMatSize = B.FTMatSizeCode"
    '        tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC;"

    '        oDBdtMatSize = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

    '        Dim oStrBuilder As New System.Text.StringBuilder()

    '        oStrBuilder.AppendLine("SELECT DISTINCT A.FTPONo, B.FTMerTeamCode, B.FTMerTeamNameEN AS FTMerTeamDesc, A.FTStyleNo AS FTStyle, A.FTProdStyleName AS FTStyleDesc,")
    '        oStrBuilder.AppendLine("       CONVERT(VARCHAR(10), CAST(A.FDOrderDate AS DATE), 103) AS FTOrderDate, C.FTBuyGrpCode AS FTBuyGrpCode, C.FTBuyGrpNameEN AS FTBuyGrpDesc, D.FTMainCategoryCode AS FTMainCategoryCode, D.FTMainCategoryDescEN AS FTMainCategoryDesc,")
    '        oStrBuilder.AppendLine("	   E.FTProdTypeCode + ' : ' + E.FTProdTypeNameEN AS FTProdTypeDesc, F.FTSeasonCode + ' : ' + F.FTSeasonNameEN AS FTPlanningSeason,")
    '        oStrBuilder.AppendLine("	   G.FTAF_REQ AS FTAF_REQ, G.FTCU_CD AS FTCU_CD, G.FTCUST_XREF AS FTCUST_XREF,")
    '        oStrBuilder.AppendLine("	   G.FDShipmentDate, G.FDShipmentDateOriginal, G.FTShipModeDesc, G.FTGenderCode,")
    '        oStrBuilder.AppendLine("	   G.FTUnitDesc, G.FTColorwayCode")
    '        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMerTeam AS B (NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId")
    '        oStrBuilder.AppendLine("                                                             LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS C (NOLOCK) ON A.FNHSysBuyGrpId = C.FNHSysBuyGrpId")
    '        oStrBuilder.AppendLine("														     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainCategoryType AS D (NOLOCK) ON A.FNHSysMainCategoryId = D.FNHSysMainCategoryId")
    '        oStrBuilder.AppendLine("														     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMProductType AS E (NOLOCK) ON A.FNHSysProdTypeId = E.FNHSysProdTypeId")
    '        oStrBuilder.AppendLine("														     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMSeason AS F (NOLOCK) ON A.FNHSysSeasonId = F.FNHSysSeasonId")
    '        oStrBuilder.AppendLine("														     LEFT JOIN (SELECT A.FTUserLogIn, A.FTPONo, A.FTStyleNo, A.FTAF_REQ AS FTAF_REQ, A.FTCU_CD AS FTCU_CD, A.FTCUST_XREF AS FTCUST_XREF,")
    '        oStrBuilder.AppendLine("                                                                             CASE WHEN ISDATE(A.FTCURRNT_ETS) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTCURRNT_ETS AS DATE), 103) ELSE '' END AS FDShipmentDate,")
    '        oStrBuilder.AppendLine("	                                                                         CASE WHEN ISDATE(A.FTORIGNL_ETS) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTORIGNL_ETS AS DATE), 103) ELSE '' END AS FDShipmentDateOriginal,")
    '        oStrBuilder.AppendLine("                                                                             A.FTMD_TR AS FTShipModeDesc, A.FTCMO_Consumer_Group AS FTGenderCode, A.FTColorwayCode AS FTColorwayCode, A.FTSizeBreakdownCode, A.FNQuantity AS FNQuantity, N'PCS' AS FTUnitDesc")
    '        oStrBuilder.AppendLine("                                                                        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A (NOLOCK)")
    '        oStrBuilder.AppendLine("																		WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
    '        oStrBuilder.AppendLine("																	  ) AS G ON  A.FTPONo = G.FTPONo AND A.FTStyleNo = G.FTStyleNo AND A.FTUserLogIn = G.FTUserLogIn")
    '        oStrBuilder.AppendLine("WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
    '        oStrBuilder.AppendLine("ORDER BY A.FTPONo ASC, A.FTStyleNo ASC, G.FTAF_REQ ASC, G.FTCU_CD ASC, G.FTCUST_XREF ASC, G.FTColorwayCode ASC")

    '        If oStrBuilder.Length > 0 Then
    '            tSql = ""
    '            tSql = oStrBuilder.ToString()
    '        End If

    '        oDBdtStatic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

    '        If Not oDBdtMatSize Is Nothing And oDBdtMatSize.Rows.Count > 0 Then

    '            tSql = ""
    '            tSql = "SELECT A.FTPONo, A.FTStyleNo AS FTStyle, A.FTAF_REQ, A.FTCU_CD, A.FTCU_CD, A.FTCUST_XREF, A.FTColorwayCode, A.FTSizeBreakdownCode AS FTMatSizeCode, A.FNQuantity"
    '            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A (NOLOCK)"
    '            tSql &= Environment.NewLine & "WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            tSql &= Environment.NewLine & "ORDER  BY A.FTPONo ASC, A.FTStyleNo ASC, A.FTAF_REQ ASC, A.FTCU_CD ASC, A.FTCUST_XREF ASC, A.FTColorwayCode ASC, A.FTSizeBreakdownCode ASC;"

    '            oDBdtDynamic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

    '            For Each oRow As DataRow In oDBdtMatSize.Rows
    '                '...FNQuantity : Total Item Qty
    '                Dim oColAppendSizeQty As DataColumn = New DataColumn("FNQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Int32"))
    '                oColAppendSizeQty.Caption = "Total Item Qty : " & oRow.Item("FTMatSizeCode").ToString()

    '                Me.ogvConfirmImport.Columns.AddField(oColAppendSizeQty.ColumnName)
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).FieldName = oColAppendSizeQty.ColumnName
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).Name = oColAppendSizeQty.ColumnName
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).Caption = oColAppendSizeQty.Caption
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).Visible = True
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).OptionsColumn.AllowEdit = False
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).OptionsColumn.AllowMove = False
    '                Me.ogvConfirmImport.Columns(oColAppendSizeQty.ColumnName).OptionsColumn.AllowSort = False

    '                oDBdtStatic.Columns.Add(oColAppendSizeQty.ColumnName, oColAppendSizeQty.DataType)

    '            Next

    '            If Not oDBdtStatic Is Nothing AndAlso oDBdtStatic.Rows.Count > 0 Then oDBdtStatic.AcceptChanges()

    '            'If  (1 > 2) AndAlso oDBdtDynamic.Rows.Count > 0 Then
    '            If Not oDBdtDynamic Is Nothing AndAlso oDBdtDynamic.Rows.Count > 0 Then

    '                Dim nLoopRowImport As Integer

    '                For nLoopRowImport = 0 To oDBdtStatic.Rows.Count - 1

    '                    Try
    '                        Dim oDataRow() As DataRow

    '                        Dim tTextFTPONo As String
    '                        Dim tTextFTStyleNo As String
    '                        Dim tTextFTAF_REQ As String
    '                        Dim tTextFTCU_CD As String
    '                        Dim tTextFTCUST_XREF As String
    '                        Dim tTextFTColorwayCode As String

    '                        tTextFTPONo = "" : tTextFTStyleNo = "" : tTextFTAF_REQ = "" : tTextFTCU_CD = "" : tTextFTCUST_XREF = "" : tTextFTColorwayCode = ""

    '                        If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTPONo")) Then
    '                            tTextFTPONo = oDBdtDynamic.Rows(nLoopRowImport).Item("FTPONo").ToString.Trim
    '                        End If

    '                        If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTStyle")) Then
    '                            tTextFTStyleNo = oDBdtDynamic.Rows(nLoopRowImport).Item("FTStyle").ToString.Trim
    '                        End If

    '                        If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTAF_REQ")) Then
    '                            tTextFTAF_REQ = oDBdtDynamic.Rows(nLoopRowImport).Item("FTAF_REQ").ToString.Trim
    '                        End If

    '                        If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTCU_CD")) Then
    '                            tTextFTCU_CD = oDBdtDynamic.Rows(nLoopRowImport).Item("FTCU_CD").ToString
    '                        End If

    '                        If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTCUST_XREF")) Then
    '                            tTextFTCUST_XREF = oDBdtDynamic.Rows(nLoopRowImport).Item("FTCUST_XREF").ToString
    '                        End If

    '                        If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTColorwayCode")) Then
    '                            tTextFTColorwayCode = oDBdtDynamic.Rows(nLoopRowImport).Item("FTColorwayCode").ToString
    '                        End If

    '                        oDataRow = oDBdtDynamic.Select("FTPONo = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTPONo").ToString) & _
    '                                                        "' AND FTStyle = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTStyle").ToString) & _
    '                                                        "' AND FTAF_REQ = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTAF_REQ").ToString) & _
    '                                                        "' AND FTCU_CD = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTCU_CD").ToString) & _
    '                                                        "' AND FTCUST_XREF = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTCUST_XREF").ToString) & _
    '                                                        "' AND FTColorwayCode = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTColorwayCode").ToString) & "'")

    '                        If Not oDataRow Is Nothing AndAlso oDataRow.Count > 0 Then

    '                            For nLoopDataRow As Integer = 0 To oDataRow.Count - 1

    '                                If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FTMatSizeCode")) Then
    '                                    Dim tFTMatSizeCode As String = ""
    '                                    tFTMatSizeCode = oDataRow(nLoopDataRow).Item("FTMatSizeCode").ToString()

    '                                    If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FNQuantity")) Then
    '                                        oDBdtStatic.Rows(nLoopRowImport).Item("FNQuantity" & tFTMatSizeCode) = oDataRow(nLoopDataRow).Item("FNQuantity")
    '                                    End If

    '                                End If

    '                            Next nLoopDataRow

    '                            oDBdtStatic.AcceptChanges()

    '                        Else
    '                            '...do nothing
    '                        End If

    '                    Catch ex As Exception
    '                        MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
    '                    End Try

    '                Next nLoopRowImport

    '            End If

    '        End If

    '        Me.ogdConfirmImport.DataSource = oDBdtStatic
    '        Me.ogdConfirmImport.Refresh()
    '        Me.ogvConfirmImport.RefreshData()
    '        Me.ogvConfirmImport.BestFitColumns()

    '        Call W_PRCxInitialGridBandView()

    '    Catch ex As Exception
    '        MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
    '    End Try

    '    Return bRet

    'End Function

  
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

    Private Function W_PRCbImportFactoryOrder(_Spls) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub

        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""

        Try
            Dim nFNHSysCustId As Integer
            Dim nFNHSysCmpRunId As Integer
            Dim nFNHSysBuyId As Integer
            Dim nFNHSysVenderPramId As Integer
            Dim FNQtySpecialType As Integer = 0
            Dim FNQtySpecialTypeQty As Double = 0
            Dim _dtper As DataTable

            tSql = "SELECT TOP 1 FNQtySpecialType, FNQtySpecial "
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS M WITH(NOLOCK) "
            tSql &= vbCrLf & " WHERE FNHSysBuyGrpId=" & Integer.Parse(Val(FNHSysBuyGrpId.Properties.Tag.ToString)) & ""
            _dtper = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In _dtper.Rows

                FNQtySpecialType = Integer.Parse(Val(R!FNQtySpecialType.ToString()))
                FNQtySpecialTypeQty = Double.Parse(Val(R!FNQtySpecial.ToString()))

                Exit For
            Next
            Dim OrderTypeValue As Integer = 13
            Dim OrderType As Integer = HI.TL.CboList.GetIndexByValue("FNOrderType", OrderTypeValue)



            nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag) : nFNHSysCmpRunId = Val(Me.FNHSysCmpRunId.Properties.Tag) : nFNHSysBuyId = Val(Me.FNHSysBuyId.Properties.Tag)

            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysVenderPramId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMVenderPram] AS A WITH(NOLOCK) WHERE A.FNHSysVenderPramId = " & Val(Me.FNHSysVenderPramId.Properties.Tag)

            nFNHSysVenderPramId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

            If nFNHSysVenderPramId <= 0 Then Return False

            Dim oDBdtImport As System.Data.DataTable


            tSql = "  SELECT  A.FNHSysMerTeamId, A.FTPONo, '' AS FTPOTrading, '' AS FTPOCreateDate, A.FDOrderDate AS FTOrderDate"
            tSql &= Environment.NewLine & "         , " & nFNHSysBuyId & " AS FNHSysBuyId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId, " & nFNHSysCustId & " AS FNHSysCustId"
            tSql &= Environment.NewLine & "    ,N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AS FTOrderBy, 0 AS FNOrderType, 0 AS FNJobState"
            tSql &= Environment.NewLine & "    ,A.FTStyleNo AS FTStyle, '' AS FDShipDate, '' AS FDShipDateOrginal, A.FNHSysBuyGrpId, 0 AS FNHSysGenderId, A.FNHSysProdTypeId"
            tSql &= Environment.NewLine & "    ,-1 FNHSysPlantId,'' AS FTYear, A.FNHSysMainCategoryId,A.FNHSysSeasonId ASFTPlanningSeason,0 AS FNHSysCountryId, 0 AS FNHSysBuyerId"
            tSql &= Environment.NewLine & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A INNER JOIN [HITECH_MASTER]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "       WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "    AND A.FTPONo IN (SELECT MAX(L1.FTPONo)"
            tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS L1 WITH(NOLOCK)"
            tSql &= Environment.NewLine & "     WHERE L1.FTUserLogin = A.FTUserLogin"
            tSql &= Environment.NewLine & "        AND L1.FTPONo = A.FTPONO"
            tSql &= Environment.NewLine & "    GROUP BY L1.FTPONo)"
            tSql &= Environment.NewLine & "   ORDER BY B.FNHSysMerTeamId ASC, A.FTStyleNo ASC;"

            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then
                '...generate facotry order no.
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                tSql = ""
                tSql = "DECLARE @DBName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @TblName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @DocType NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @GetFotmat NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @AddPrefix NVARCHAR(30);"
                tSql &= Environment.NewLine & "SET @DBName = N'" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "';"
                tSql &= Environment.NewLine & "SET @TblName = N'TMERTOrder';"
                tSql &= Environment.NewLine & "SET @DocType = '" & OrderType.ToString & "';"
                tSql &= Environment.NewLine & "SET @GetFotmat = '';"
                tSql &= Environment.NewLine & "SET @AddPrefix = N'" & HI.TL.CboList.GetListRefer("FNOrderType", OrderType) & HI.UL.ULF.rpQuoted(Me.FNHSysCmpRunId.Text.Trim()) & "';"
                tSql &= Environment.NewLine & "DECLARE @tblSrcConfigDoc AS TABLE(FTRunNo NVARCHAR(30), FTRunStr NVARCHAR(30), FNRunning INT, FNRunningNoMax INT);"
                tSql &= Environment.NewLine & "INSERT INTO @tblSrcConfigDoc(FTRunNo, FTRunStr, FNRunning, FNRunningNoMax)"
                tSql &= Environment.NewLine & "EXEC SP_GET_FACTORY_ORDERNO_MAX @DBName, @TblName, @DocType, @GetFotmat,@AddPrefix;"
                tSql &= Environment.NewLine & "DECLARE @FTRunNo        AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FTRunStr       AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FNRunning      AS INT;"
                tSql &= Environment.NewLine & "DECLARE @FNRunningNoMax AS INT;"
                tSql &= Environment.NewLine & "SELECT @FTRunNo = A.FTRunNo, @FTRunStr = A.FTRunStr, @FNRunning = A.FNRunning, @FNRunningNoMax = A.FNRunningNoMax"
                tSql &= Environment.NewLine & "FROM @tblSrcConfigDoc AS A;"
                tSql &= Environment.NewLine & "--SET @FNRunningNoMax = @FNRunningNoMax + 1;"
                tSql &= Environment.NewLine & " create table #Tab("
                tSql &= Environment.NewLine & " [FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & " [FDOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNOrderType] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpRunId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysStyleId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTPORef] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNHSysCustId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysAgencyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysProdTypeId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyerId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTMainMaterial] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTCombination] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & " [FTStateOrderApp] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNJobState] [int] NULL,"
                tSql &= Environment.NewLine & " [FTStateBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDStateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTStateTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTImage1] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage2] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage3] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage4] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FNHSysBrandId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppRemark] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTPOTradingCo] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOItem] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOCreateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FNHSysMerTeamId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysPlantId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyGrpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysMainCategoryId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysVenderPramId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTOrderCreateStatus] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTImportUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDImportDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTImportTime] [nvarchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNRowImport] [Int],"
                tSql &= Environment.NewLine & " [FTStyle] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FNHSysSeasonId] [Int] NULL,"
                tSql &= Environment.NewLine & " [FTStateSet] [varchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTSubPGM] [nvarchar](200) NULL,"
                tSql &= Environment.NewLine & " [FTCmpID]  [Int] NULL"
                tSql &= Environment.NewLine & "  )"
                tSql &= Environment.NewLine & " INSERT INTO  #Tab"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNRowImport],[FTStyle],[FNHSysSeasonId],[FTStateSet],FTSubPGM,FTCmpID)"
                tSql &= Environment.NewLine & " SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , @FTRunNo + (RIGHT(@FTRunStr, @FNRunning - LEN(CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY B.FTMerTeamCode ASC,A.FTStyleNo ASC,A.FTPONO ASC) + @FNRunningNoMax)))) + CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY B.FTMerTeamCode ASC,A.FTStyleNo ASC,A.FTPONO ASC) + @FNRunningNoMax))) AS FTOrderNo"
                tSql &= Environment.NewLine & "     ,  A.FDOrderDate AS FTOrderDate, NULL AS FTOrderBy, " & OrderTypeValue.ToString & " AS FNOrderType"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysCmpId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE L1.FTStyleCode = A.FTStyleNo), NULL) AS FNHSysStyleId"
                tSql &= Environment.NewLine & "     , A.FTPONo, " & nFNHSysCustId & " AS FNHSysCustId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysAgencyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS L1 WITH(NOLOCK) WHERE L1.FTAgencyCode = N'-'),NULL) AS FNHSysAgencyId"
                tSql &= Environment.NewLine & "     , A.FNHSysProdTypeId, -1 AS FNHSysBuyerId, NULL AS FTMainMaterial, NULL AS FTCombination, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , NULL AS FTStateOrderApp, NULL AS FTAppBy, NULL AS FDAppDate, NULL AS FTAppTime"
                tSql &= Environment.NewLine & "     , 0 AS FNJobState, NULL AS FTStateBy, NULL AS FDStateDate, NULL AS FTStateTime"
                tSql &= Environment.NewLine & "     , NULL AS FTImage1, NULL AS FTImage2, NULL AS FTImage3, NULL AS FTImage4"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysBrandId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS L1 WITH(NOLOCK) WHERE L1.FTBrandCode = N'-'),NULL) AS FNHSysBrandId"
                tSql &= Environment.NewLine & "     , " & nFNHSysBuyId & " AS FNHSysBuyId"
                tSql &= Environment.NewLine & "     , NULL AS FTCancelAppBy, NULL AS FDCancelAppDate, NULL AS FDCancelAppTime, NULL AS FTCancelAppRemark"
                tSql &= Environment.NewLine & "     , '' AS FTPOTrading, NULL AS FTPOItem, '' AS FTPOCreateDate"
                tSql &= Environment.NewLine & "     , A.FNHSysMerTeamId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                'tSql &= Environment.NewLine & "   , -1 AS FNHSysPlantId "
                tSql &= Environment.NewLine & "    , A.FNHSysBuyGrpId "
                tSql &= Environment.NewLine & "    , A.FNHSysMainCategoryId, " & nFNHSysVenderPramId & " AS FNHSysVenderPramId"
                tSql &= Environment.NewLine & "    , N'Y' AS FTOrderCreateStatus, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTImportUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDImportDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTImportTime,0 AS FNRowImport,A.FTStyleNo," & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & " "
                tSql &= Environment.NewLine & "    , ISNULL((SELECT  TOP 1 L1.FTStateStyleSet FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE L1.FTStyleCode = A.FTStyleNo), '0') AS FTStateSet"
                tSql &= Environment.NewLine & "    , A.FTSubPGM "
                tSql &= Environment.NewLine & "    , ISNULL((SELECT  TOP 1 L1.FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] AS L1 WITH(NOLOCK) WHERE L1.FTCmpCode = A.FTCmp), 0) AS FNCmpId"
                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "      AND A.FTPONO IN (SELECT MAX(L1.FTPONO)"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE L1.FTUserLogin = A.FTUserLogin"
                tSql &= Environment.NewLine & "                                 AND L1.FTPONo = A.FTPONO"
                tSql &= Environment.NewLine & "                                 AND L1.FTStyleNo = A.FTStyleNo"
                tSql &= Environment.NewLine & "                           GROUP BY L1.FTPONo)"
                tSql &= Environment.NewLine & " ORDER BY B.FTMerTeamCode ASC,A.FTStyleNo ASC,A.FTPONO ASC;"
                tSql &= Environment.NewLine & " UPDATE A"
                tSql &= Environment.NewLine & " SET FTGenerateOrderNo = ISNULL((SELECT TOP 1 ISNULL(FTOrderNo,'')"
                tSql &= Environment.NewLine & "                                FROM #Tab"
                tSql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyleNo), '')"
                tSql &= Environment.NewLine & ",FTStateSet = ISNULL((SELECT TOP 1 ISNULL(FTStateSet,'')"
                tSql &= Environment.NewLine & "                                FROM #Tab"
                tSql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyleNo), '')"
                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A INNER JOIN [HITECH_MASTER]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "      AND A.FTPONo IN ( "
                tSql &= Environment.NewLine & "                              SELECT MAX(L1.FTPONo) "
                tSql &= Environment.NewLine & "                              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS L1 WITH(NOLOCK) "
                tSql &= Environment.NewLine & "                              WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "                              GROUP BY L1.FTPONo,L1.FTStyleNo "
                tSql &= Environment.NewLine & " 	                       )"
                tSql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],FTSubPgm)"
                tSql &= Environment.NewLine & " SELECT [FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "       ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "       ,[FTOrderNo],[FDOrderDate]"

                tSql &= Environment.NewLine & "       ,ISNULL(("
                tSql &= Environment.NewLine & "   SELECT TOP 1 FTUserName"
                tSql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMMerTeam AS AX WITH(NOLOCK)"
                tSql &= Environment.NewLine & "   WHERE   (AX.FNHSysMerTeamId = M.FNHSysMerTeamId)"
                tSql &= Environment.NewLine & "        ),'')  AS FTOrderBy"
                tSql &= Environment.NewLine & ",[FNOrderType]"

                tSql &= Environment.NewLine & "       ,FTCmpID,[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "       ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "       ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "       ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "       ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "       ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "       ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "       ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "       ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "       ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],FTSubPGM"
                tSql &= Environment.NewLine & " FROM #Tab AS M"
                tSql &= Environment.NewLine & " DROP TABLE #Tab;"

                '...Merge Transaction TMERTOrderSub And TMERTOrderSub_BreakDown
                '========================================================================================================================================================================


                tSql &= Environment.NewLine & " create table #Tabsuborder("
                tSql &= Environment.NewLine & "  [FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FDSubOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDProDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FDShipDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "	[FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysContinentId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCountryId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysProvinceId] [int] NULL,"
                tSql &= Environment.NewLine & "	[FNHSysShipModeId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCurId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysGenderId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysUnitId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNSubOrderState] [int] NULL,"
                tSql &= Environment.NewLine & "[FTStateEmb] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStatePrint] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateHeat] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateLaser] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateWindows] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther1] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther1Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther2] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther2Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther3] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther3Note1] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & "[FNHSysShipPortId] [int] NULL,"
                tSql &= Environment.NewLine & "[FDShipDateOrginal] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTCustRef] [nvarchar](200) NULL,"
                tSql &= Environment.NewLine & "[FNHSysPlantId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysBuyGrpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTStateSet]  [int] NULL"
                tSql &= Environment.NewLine & "  )"

                'tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]"
                'tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                'tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                'tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                'tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                'tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                'tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                'tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                'tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                'tSql &= Environment.NewLine & "         ,[FTRemark]"
                'tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                'tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],[FTStateSet])"

                tSql &= Environment.NewLine & "INSERT INTO  #Tabsuborder"
                tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "         ,[FTRemark]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],[FTStateSet])"


                tSql &= Environment.NewLine & " SELECT DISTINCT  NULL AS FTInsUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser , NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , A.FTOrderNo"
                'tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' + CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,B.FTCountryCode))) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)


                '  tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' + CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,B.FTCountryCode))) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)

                tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' +   CASE WHEN ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,B.FTCountryCode) <=26 THEN  CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,B.FTCountryCode)) ELSE 'A' + CHAR(64 + ((ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,B.FTCountryCode)) - 26))   END  ) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)

                tSql &= Environment.NewLine & "     , A.FDOrderDate AS FDSubOrderDate"
                tSql &= Environment.NewLine & "     , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[FN_GET_PRODDATE_IMPORT] (B.FTShipDate, " & nFNHSysCustId & ", NULL)), NULL) AS FDProdDate"
                tSql &= Environment.NewLine & "     , B.FTShipDate AS FDShipDate"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysBuyId"
                'tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysContinentId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCountryId = A.FNHSysCountryId),-1) AS FNHSysContinentId"
                'tSql &= Environment.NewLine & "     , A.FNHSysCountryId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysContinentId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCountryId = "
                tSql &= Environment.NewLine & " ISNULL((SELECT TOP 1  L1.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FTCountryCode = B.FTCountryCode),-1)"
                tSql &= Environment.NewLine & " ),-1) AS FNHSysContinentId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1  L1.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FTCountryCode = B.FTCountryCode),-1) AS FNHSysCountryId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysProvinceId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCMMProvince] AS L1 WITH(NOLOCK) WHERE L1.FTProvinceCode = N'-'), -1) AS FNHSysProvinceId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipModeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS L1 WITH(NOLOCK) WHERE L1.FTShipModeCode = B.FTMode), NULL) AS FNHSysShipModeId"
                tSql &= Environment.NewLine & "     , A.FNHSysCurId AS FNHSysCurId"
                ' tSql &= Environment.NewLine & "     , A.FNHSysGenderId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1  L1.FNHSysGenderId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FTGenderCode = B.FTGender),NULL) AS FNHSysGenderId"
                tSql &= Environment.NewLine & "     ," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString())) & "  AS FNHSysUnitId"
                tSql &= Environment.NewLine & "     , NULL AS FNSubOrderState"
                tSql &= Environment.NewLine & "     , C.FTStateEmb AS FTStateEmb"
                tSql &= Environment.NewLine & "     , C.FTStatePrint AS FTStatePrint"
                tSql &= Environment.NewLine & "     , C.FTStateHeat AS FTStateHeat"
                tSql &= Environment.NewLine & "     , C.FTStateLaser AS FTStateLaser"
                tSql &= Environment.NewLine & "     , C.FTStateWindows AS FTStateWindows"
                tSql &= Environment.NewLine & "     , NULL AS FTOther1Note, NULL AS FTOther1Note, NULL AS FTStateOther2, NULL AS FTOther2Note, NULL AS FTStateOther3, NULL AS FTOther3Note1, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipPortId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS L1 WITH(NOLOCK) WHERE L1.FTShipPortCode = N'-'),NULL) AS FNHSysShipPortId"
                tSql &= Environment.NewLine & "     , B.FDShipDateOrginal AS FDShipDateOrginal"
                tSql &= Environment.NewLine & "     , dbo.FN_GetCustomer_Refer(A.FNHSysPlantId, A.FNHSysBuyerId) AS FTCustRef,A.FNHSysPlantId," & Val(FNHSysBuyGrpId.Properties.Tag.ToString()) & " AS FNHSysBuyGrpId,A.FTStateSet"
                tSql &= Environment.NewLine & "FROM( "
                '-----Order Normal Data  


                tSql &= Environment.NewLine & "  Select   CASE WHEN ISNULL(A.FTStateSet,'0') ='1' THEN 1 ELSE 0 END AS  FTStateSet,A.FTPONo, C.FDOrderDate, '' AS FTPOTrading, '' AS FTPOItem, 0 AS FNRowImport, A.FTStyleNo AS FTStyle, C.FTOrderNo"
                tSql &= Environment.NewLine & "     ,  ISNULL((SELECT TOP 1  FTCURRNT_ETS "
                tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A "
                tSql &= Environment.NewLine & "      WHERE FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "     AND FTPONo = A.FTPONo "
                tSql &= Environment.NewLine & "    AND FTStyleNo=A.FTStyleNo),'') AS FDShipDate"
                tSql &= Environment.NewLine & "      ,ISNULL((SELECT TOP 1  FTORIGNL_ETS "
                tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A "
                tSql &= Environment.NewLine & "     WHERE FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "     AND FTPONo = A.FTPONo "
                tSql &= Environment.NewLine & "    AND FTStyleNo=A.FTStyleNo),'') AS FDShipDateOrginal"
                'tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1  L1.FNHSysGenderId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FTGenderCode = B.FTGender),NULL) AS FNHSysGenderId"
                ' tSql &= Environment.NewLine & "     , -1 AS FNHSysCountryId"
                'tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1  L1.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FTCountryCode = B.FTCountryCode),-1) AS FNHSysCountryId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                ' tSql &= Environment.NewLine & ",-1 AS FNHSysBuyerId "
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysBuyerId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = N'-'),NULL) AS FNHSysBuyerId"
                tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                tSql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                tSql &= Environment.NewLine & "           AND A.FTPONo IN (SELECT MAX(L1.FTPONo)"
                tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FTStyleNo) "



                '-----Order Normal Data  
                '-----Order Normal Data Set 
                tSql &= Environment.NewLine & " UNION  "
                tSql &= Environment.NewLine & "  Select   2 AS FTStateSet,A.FTPONo, C.FDOrderDate, '' AS FTPOTrading, '' AS FTPOItem, 0 AS FNRowImport, A.FTStyleNo AS FTStyle, C.FTOrderNo"
                tSql &= Environment.NewLine & "     ,  ISNULL((SELECT TOP 1  FTCURRNT_ETS "
                tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A "
                tSql &= Environment.NewLine & "      WHERE FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "     AND FTPONo = A.FTPONo "
                tSql &= Environment.NewLine & "    AND FTStyleNo=A.FTStyleNo),'') AS FDShipDate"
                tSql &= Environment.NewLine & "      ,ISNULL((SELECT TOP 1  FTORIGNL_ETS "
                tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A "
                tSql &= Environment.NewLine & "     WHERE FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "     AND FTPONo = A.FTPONo "
                tSql &= Environment.NewLine & "    AND FTStyleNo=A.FTStyleNo),'') AS FDShipDateOrginal"
                'tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1  L1.FNHSysGenderId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FTGenderCode = B.FTGender),NULL) AS FNHSysGenderId"
                ' tSql &= Environment.NewLine & "     , -1 AS FNHSysCountryId"
                'tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1  L1.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FTCountryCode = B.FTCountryCode),-1) AS FNHSysCountryId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                ' tSql &= Environment.NewLine & ",-1 AS FNHSysBuyerId "
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysBuyerId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = N'-'),NULL) AS FNHSysBuyerId"
                tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                tSql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                tSql &= Environment.NewLine & "           AND A.FTPONo IN (SELECT MAX(L1.FTPONo)"
                tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FTStyleNo) "
                tSql &= Environment.NewLine & "           AND ISNULL(A.FTStateSet,'0') ='1'"
                '-----Order Normal Data Set 


                tSql &= Environment.NewLine & " ) AS A INNER JOIN "
                tSql &= Environment.NewLine & "     (SELECT  0 AS FNRowImport, FTPONo, '' AS FTPOTrading, FTStyleNo AS FTStyle,FTCURRNT_ETS AS  FTShipDate,Max(FTMD_TR)  As  FTMode, '' FTUom,MAX(FTCMO_Consumer_Group) AS FTGender"
                tSql &= Environment.NewLine & "   , (FTCUST_XREF + FTCU_CD) AS FTCountryCode,MAX(FTORIGNL_ETS) AS FDShipDateOrginal "
                tSql &= Environment.NewLine & "      FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp WITH(NOLOCK) "
                tSql &= Environment.NewLine & "     WHERE FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "     GROUP BY FTPONo,  FTStyleNo,(FTCUST_XREF + FTCU_CD) ,FTCURRNT_ETS "
                tSql &= Environment.NewLine & "     ) AS B ON A.FTPONo = B.FTPONo"
                tSql &= Environment.NewLine & "               AND A.FTPOTrading = B.FTPOTrading"
                tSql &= Environment.NewLine & "               AND A.FTStyle = B.FTStyle"
                tSql &= Environment.NewLine & "             "
                tSql &= Environment.NewLine & "       LEFT JOIN (SELECT L4.FNHSysStyleId, L4.FTStyleCode, ISNULL(MAX(L3.FTStateEmb),0) AS FTStateEmb, ISNULL(MAX(L3.FTStatePrint),0) AS FTStatePrint, ISNULL(MAX(L3.FTStateHeat),0) AS FTStateHeat, ISNULL(MAX(L3.FTStateLaser),0) AS FTStateLaser, ISNULL(MAX(L3.FTStateWindows),0) AS FTStateWindows"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTStyle_Part] AS L3 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L4 WITH(NOLOCK) ON L3.FNHSysStyleId = L4.FNHSysStyleId"
                tSql &= Environment.NewLine & "                  GROUP BY L4.FNHSysStyleId, L4.FTStyleCode ) AS C ON A.FTStyle = C.FTStyleCode"
                tSql &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC;"




                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]"
                tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "         ,[FTRemark]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],[FNOrderSetType])"
                tSql &= Environment.NewLine & "   SELECT      [FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "         ,[FTRemark]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],[FTStateSet]"
                tSql &= Environment.NewLine & "  FROM #Tabsuborder "

                tSql &= Environment.NewLine & " UPDATE A"

                tSql &= Environment.NewLine & " SET FTGenerateOrderSubNo = ISNULL((SELECT TOP 1 ISNULL(FTSubOrderNo,'')"
                tSql &= Environment.NewLine & "                                FROM #Tabsuborder"
                tSql &= Environment.NewLine & "                                WHERE FTOrderNo = A.FTGenerateOrderNo AND FDShipDate = A.FDShipDate AND FNGrpSeq=A.FNGrpSeq AND FNHSysBuyGrpId=A.FNHSysBuyGrpId  AND FTStateSet<>2 ), '')"
                tSql &= Environment.NewLine & " ,FTGenerateOrderSubNo2 = ISNULL((SELECT TOP 1 ISNULL(FTSubOrderNo,'')"
                tSql &= Environment.NewLine & "                                FROM #Tabsuborder"
                tSql &= Environment.NewLine & "                                WHERE FTOrderNo = A.FTGenerateOrderNo AND FDShipDate = A.FDShipDate AND FNGrpSeq=A.FNGrpSeq AND FNHSysBuyGrpId=A.FNHSysBuyGrpId AND FTStateSet=2), '')"



                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                tSql &= Environment.NewLine & "  DROP TABLE #Tabsuborder "
                tSql &= Environment.NewLine & "DECLARE @TMERTOrderSub_BreakDown_Import AS TABLE ([FTInsUser] [nvarchar](50), [FDInsDate] [varchar](10) NULL, [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTUpdUser] [nvarchar](50) NULL, [FDUpdDate] [varchar](10) NULL, [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTOrderNo] [nvarchar](30) NOT NULL, [FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FTColorway] [nvarchar](30) NOT NULL, [FTSizeBreakDown] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FNPrice] [numeric](18, 5) NULL, [FNQuantity] [int] NULL, [FNAmt] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNHSysMatColorId] [int] NULL, [FNHSysMatSizeId] [int] NULL,"
                tSql &= Environment.NewLine & "                              [FNExtraQty] [numeric](18, 5) NULL, [FNQuantityExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandQuantity] [numeric](18, 5) NULL, [FNAmntExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandAmnt] [numeric](18, 5) NULL, [FNGarmentQtyTest] [int] NULL,[FNAmntQtyTest] [numeric](18, 5) NULL,[FTNikePOLineItem] [nvarchar](30) NOT NULL,[FNOperateFee]  [numeric](18, 5) NULL, [FNExternalQtyTest] [int] NULL,[FNExternalAmntQtyTest] [numeric](18, 5) NULL)"
                tSql &= Environment.NewLine & "INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                tSql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                tSql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                tSql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                tSql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                tSql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt],[FTNikePOLineItem],[FNOperateFee],[FNExternalQtyTest],[FNExternalAmntQtyTest])"
                tSql &= Environment.NewLine & "SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                tSql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                tSql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                tSql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"

                ' tSql &= Environment.NewLine & "       0 AS FNExtraQty,0 AS FNQuantityExtra"

                Select Case FNQtySpecialType
                    Case 1

                        tSql &= Environment.NewLine & "            0 AS FNExtraQty"
                        tSql &= Environment.NewLine & "           ,  " & Integer.Parse(Val(FNQtySpecialTypeQty)) & " AS FNQuantityExtra"

                    Case 2

                        tSql &= Environment.NewLine & "            " & FNQtySpecialTypeQty & " AS FNExtraQty"
                        tSql &= Environment.NewLine & "           , CEILING((ordImport.FNQuantity * " & FNQtySpecialTypeQty & ")/100.00) AS FNQuantityExtra"

                    Case Else

                        tSql &= Environment.NewLine & "       0 AS FNExtraQty,0 AS FNQuantityExtra"

                End Select

                tSql &= Environment.NewLine & " , ordImport.FNGrandQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt"
                tSql &= Environment.NewLine & "  , CONVERT(NVARCHAR(30),((ROW_NUMBER() OVER (PARTITION BY  ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway ORDER BY ordImport.FTColorway  )) * 10)) AS FTNikePOLineItem "
                tSql &= Environment.NewLine & "           ,FNOperateFee,FNExternalQuantity,0"
                tSql &= Environment.NewLine & "FROM ( "

                '-----Order Normal Data 
                tSql &= Environment.NewLine & " SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                tSql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                tSql &= Environment.NewLine & "           , A.FTColorwayCode AS FTColorway"
                tSql &= Environment.NewLine & "           , A.FTSizeBreakdownCode AS FTSizeBreakDown"
                tSql &= Environment.NewLine & "           , ISNULL(MAX(0),0) AS FNPrice"
                tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(0)), 0) AS FNAmt"
                tSql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                tSql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                tSql &= Environment.NewLine & "           , 0 AS FNExtraQty"
                tSql &= Environment.NewLine & "           , 0 AS FNQuantityExtra"
                tSql &= Environment.NewLine & "           , ISNULL(SUM(0), 0) AS FNGrandQuantity"
                tSql &= Environment.NewLine & "           , 0 AS FNAmntExtra"
                tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(0)), NULL) AS FNGrandAmnt,MAX(ISNULL(Cus.FNOperateFee,0)) AS FNOperateFee"
                tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNExternalQuantity), 0) AS FNExternalQuantity"
                tSql &= Environment.NewLine & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS AA WITH(NOLOCK)"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS MM3 WITH(NOLOCK) ON AA.FTGenerateOrderNo = MM3.FTOrderNo "
                'tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS MM3 WITH(NOLOCK) ON AA.FTGenerateOrderNo = MM3.FTOrderNo AND AA.FTGenerateOrderSubNo=MM3.FTSubOrderNo"
                tSql &= Environment.NewLine & "           INNER JOIN (SELECT A.*"
                tSql &= Environment.NewLine & "      ,    ISNULL((SELECT TOP 1  L1.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FTCountryCode = (A.FTCUST_XREF + A.FTCU_CD)),-1) AS FNHSysCountryId"
                tSql &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanSizeBreakdownTemp] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "           WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "    ) AS A  ON  AA.FTUserLogin = A.FTUserLogin AND AA.FTPONo = A.FTPONo  AND AA.FTStyleNo = A.FTStyleNo AND MM3.FNHSysCountryId = A.FNHSysCountryId  AND MM3.FDShipDate =A.FTCURRNT_ETS"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS B WITH(NOLOCK) ON AA.FTGenerateOrderNo = B.FTOrderNo"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C (NOLOCK) ON A.FTColorwayCode = C.FTMatColorCode"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS D (NOLOCK) ON A.FTSizeBreakdownCode = D.FTMatSizeCode"
                tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
                tSql &= Environment.NewLine & "    WHERE AA.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AND A.FNQuantity > 0 "
                tSql &= Environment.NewLine & "    GROUP BY AA.FTGenerateOrderNo, A.FTPONo,  A.FTStyleNo, MM3.FTSubOrderNo, A.FTColorwayCode, A.FTSizeBreakdownCode, C.FNHSysMatColorId, D.FNHSysMatSizeId"

                '-----Order Normal Data 

                '-----Order Set Data 
                'tSql &= Environment.NewLine & " UNION "
                'tSql &= Environment.NewLine & " SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                'tSql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                'tSql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                'tSql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                'tSql &= Environment.NewLine & "           , A.FTColorwayCode AS FTColorway"
                'tSql &= Environment.NewLine & "           , A.FTSizeBreakdownCode AS FTSizeBreakDown"
                'tSql &= Environment.NewLine & "           , ISNULL(MAX(0),0) AS FNPrice"
                'tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                'tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(0)), 0) AS FNAmt"
                'tSql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                'tSql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                'tSql &= Environment.NewLine & "           , 0 AS FNExtraQty"
                'tSql &= Environment.NewLine & "           , 0 AS FNQuantityExtra"
                'tSql &= Environment.NewLine & "           , ISNULL(SUM(0), 0) AS FNGrandQuantity"
                'tSql &= Environment.NewLine & "           , 0 AS FNAmntExtra"
                'tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(0)), NULL) AS FNGrandAmnt,MAX(ISNULL(Cus.FNOperateFee,0)) AS FNOperateFee"
                'tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS AA WITH(NOLOCK)"
                'tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS MM3 WITH(NOLOCK) ON AA.FTGenerateOrderNo = MM3.FTOrderNo AND AA.FTGenerateOrderSubNo2=MM3.FTSubOrderNo"
                'tSql &= Environment.NewLine & "           INNER JOIN (SELECT A.*"
                'tSql &= Environment.NewLine & "      ,    ISNULL((SELECT TOP 1  L1.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FTCountryCode = (A.FTCUST_XREF + A.FTCU_CD)),-1) AS FNHSysCountryId"
                'tSql &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanSizeBreakdownTemp] AS A WITH(NOLOCK)"
                'tSql &= Environment.NewLine & "           WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                'tSql &= Environment.NewLine & "    ) AS A  ON  AA.FTUserLogin = A.FTUserLogin AND AA.FTPONo = A.FTPONo  AND AA.FTStyleNo = A.FTStyleNo AND MM3.FNHSysCountryId = A.FNHSysCountryId  AND MM3.FDShipDate =A.FTCURRNT_ETS"
                'tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS B WITH(NOLOCK) ON AA.FTGenerateOrderNo = B.FTOrderNo"
                'tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C (NOLOCK) ON A.FTColorwayCode = C.FTMatColorCode"
                'tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS D (NOLOCK) ON A.FTSizeBreakdownCode = D.FTMatSizeCode"
                'tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
                'tSql &= Environment.NewLine & "      WHERE AA.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AND A.FNQuantity > 0  AND ISNULL(AA.FTGenerateOrderSubNo2,'') <> '' "
                'tSql &= Environment.NewLine & "      GROUP BY AA.FTGenerateOrderNo, A.FTPONo,  A.FTStyleNo, MM3.FTSubOrderNo, A.FTColorwayCode, A.FTSizeBreakdownCode, C.FNHSysMatColorId, D.FNHSysMatSizeId"
                '-----Order Set Data 



                tSql &= Environment.NewLine & "      ) AS ordImport"

                tSql &= Environment.NewLine & " UPDATE A SET FTNikePOLineItem=  CONVERT(NVARCHAR(30),(ISNULL(("

                tSql &= Environment.NewLine & "SELECT TOP 1 FNSeq FROM (SELECT FTOrderNo,FTSubOrderNo,FTColorway ,(ROW_NUMBER() OVER (PARTITION BY  FTOrderNo,FTSubOrderNo ORDER BY FTColorway  )) AS FNSeq  FROM  ( "
                tSql &= Environment.NewLine & " SELECT FTOrderNo,FTSubOrderNo,FTColorway "
                tSql &= Environment.NewLine & " FROM    @TMERTOrderSub_BreakDown_Import "
                tSql &= Environment.NewLine & " GROUP BY FTOrderNo,FTSubOrderNo,FTColorway "
                tSql &= Environment.NewLine & " ) As X) AS X WHERE X.FTOrderNo=A.FTOrderNo AND X.FTSubOrderNo=A.FTSubOrderNo AND X.FTColorway=A.FTColorway"
                tSql &= Environment.NewLine & "  ),1) * 10))  "
                tSql &= Environment.NewLine & " FROM  @TMERTOrderSub_BreakDown_Import As A "
                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                 ,[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "                 ,[FTColorway],[FTSizeBreakDown]"
                tSql &= Environment.NewLine & "                 ,[FNPrice],[FNQuantity],[FNAmt]"
                tSql &= Environment.NewLine & "                 ,[FNHSysMatColorId],[FNHSysMatSizeId]"
                tSql &= Environment.NewLine & "                 ,[FNExtraQty],[FNQuantityExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandQuantity]"
                tSql &= Environment.NewLine & "                 ,[FNAmntExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandAmnt]"
                tSql &= Environment.NewLine & "                 ,[FNGarmentQtyTest]"
                tSql &= Environment.NewLine & "                 ,[FNAmntQtyTest],[FNPriceOrg],[FTNikePOLineItem],[FNOperateFee],[FNOperateFeeAmt],[FNNetFOB],[FNExternalQtyTest],[FNExternalAmntQtyTest])"



                tSql &= Environment.NewLine & "Select aa.[FTInsUser], aa.[FDInsDate], aa.[FTInsTime],"
                tSql &= Environment.NewLine & "       aa.[FTUpdUser], aa.[FDUpdDate], aa.[FTUpdTime],"
                tSql &= Environment.NewLine & "       aa.[FTOrderNo], aa.[FTSubOrderNo], aa.[FTColorway],"
                tSql &= Environment.NewLine & "       aa.[FTSizeBreakDown], aa.[FNPrice],"
                tSql &= Environment.NewLine & "       aa.[FNQuantity], aa.[FNAmt], aa.[FNHSysMatColorId], aa.[FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "       aa.[FNExtraQty], aa.[FNQuantityExtra]"
                tSql &= Environment.NewLine & "      ,(aa.[FNQuantity] + aa.[FNQuantityExtra] +aa.[FNExternalQtyTest] ) As  [FNGrandQuantity]"
                'tSql &= Environment.NewLine & "      , aa.[FNGrandQuantity]"
                tSql &= Environment.NewLine & "      , aa.[FNAmntExtra], aa.[FNGrandAmnt],0 As FNGarmentQtyTest ,0 As FNAmntQtyTest, aa.[FNPrice],aa.[FTNikePOLineItem]"
                tSql &= Environment.NewLine & "     ,aa.FNOperateFee,Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) AS FNOperateFeeAmt,(aa.[FNPrice] - Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) ) AS FNNetFOB,aa.[FNExternalQtyTest],aa.[FNExternalAmntQtyTest] "
                tSql &= Environment.NewLine & "FROM @TMERTOrderSub_BreakDown_Import As aa"
                tSql &= Environment.NewLine & "WHERE Not EXISTS (Select 'T'"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS bb (NOLOCK)"
                tSql &= Environment.NewLine & "                  WHERE bb.FTOrderNo = aa.FTOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTSubOrderNo = aa.FTSubOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTColorway = aa.FTColorway"
                tSql &= Environment.NewLine & "                        AND bb.FTSizeBreakDown = aa.FTSizeBreakDown"
                tSql &= Environment.NewLine & "                  );"
                '========================================================================================================================================================================

                If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    'MsgBox("Step Append New Order No !!!")
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    _bImportComplete = False

                Else

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Dim Cmdstring As String = ""
                    Dim dtstss As New DataTable

                    Cmdstring = "  SELECT  DISTINCT C.FNHSysStyleId,C.FNHSysSeasonId   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A "
                    Cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                    Cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                    dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each R As DataRow In dtstss.Rows
                        csOrder.ConfirmSizeSpecToOrder(Val(R!FNHSysStyleId.ToString), Val(R!FNHSysSeasonId.ToString))
                    Next

                    _bImportComplete = True

                End If

            Else
                'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If

            'If _bImportComplete = True Then
            '    Application.DoEvents()

            '    _Spls.UpdateInformation("Generate Factory Sub Order Extra And Test Quantity .....Please Wait")

            '    '...get DataTable ON BEGIN TRANS ZZZ {DEBUG...}
            '    Dim oDBdtUpdExtraGarmentTest As System.Data.DataTable

            '    tSql = ""
            '    tSql &= Environment.NewLine & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTUserLogin,"
            '    tSql &= Environment.NewLine & "       M2.FTOrderNo, M2.FTPORef, M3.FTSubOrderNo, "
            '    tSql &= Environment.NewLine & "       M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
            '    tSql &= Environment.NewLine & "       M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
            '    tSql &= Environment.NewLine & "       M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest"
            '    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
            '    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M2.FTOrderNo = M3.FTOrderNo"
            '    tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
            '    tSql &= Environment.NewLine & "                AND M3.FTSubOrderNo = M4.FTSubOrderNo "
            '    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
            '    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
            '    tSql &= Environment.NewLine & "WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    tSql &= Environment.NewLine & "      AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
            '    tSql &= Environment.NewLine & "                             FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
            '    tSql &= Environment.NewLine & "                             WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    tSql &= Environment.NewLine & "                                   AND L1.FTPONo = M1.FTPONo"
            '    tSql &= Environment.NewLine & "                                   AND L1.FTStyle = M1.FTStyle"
            '    tSql &= Environment.NewLine & "                             GROUP BY L1.FTPONo,L1.FTStyle)"
            '    tSql &= Environment.NewLine & "      AND M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
            '    tSql &= Environment.NewLine & "ORDER BY M2.FTOrderNo ASC, M3.FTSubOrderNo ASC, M5.FNMatColorSeq ASC, M6.FNMatSizeSeq ASC;"

            '    oDBdtUpdExtraGarmentTest = HI.Conn.SQLConn.GetDataTableOnbeginTrans(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '    '...represent record sub order no / size breakdown use for compute extra quantity garment by vendor programme/main catetory BB:Basketball, FB:Football/plant/buy group
            '    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] ([FTUserLogin]"
            '    tSql &= Environment.NewLine & "             ,[FTOrderNo]"
            '    tSql &= Environment.NewLine & "             ,[FTPORef]"
            '    tSql &= Environment.NewLine & "             ,[FTSubOrderNo]"
            '    tSql &= Environment.NewLine & "             ,[FNHSysMatColorId]"
            '    tSql &= Environment.NewLine & "             ,[FNHSysMatSizeId]"
            '    tSql &= Environment.NewLine & "             ,[FNQuantity]"
            '    tSql &= Environment.NewLine & "             ,[FNPrice]"
            '    tSql &= Environment.NewLine & "             ,[FNAmt]"
            '    tSql &= Environment.NewLine & "             ,[FNExtraQty]"
            '    tSql &= Environment.NewLine & "             ,[FNQuantityExtra]"
            '    tSql &= Environment.NewLine & "             ,[FNGrandQuantity]"
            '    tSql &= Environment.NewLine & "             ,[FNAmntExtra]"
            '    tSql &= Environment.NewLine & "             ,[FNGrandAmnt]"
            '    tSql &= Environment.NewLine & "             ,[FNGarmentQtyTest])"
            '    tSql &= Environment.NewLine & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTUserLogin,"
            '    tSql &= Environment.NewLine & "       M2.FTOrderNo, M2.FTPORef, M3.FTSubOrderNo, "
            '    tSql &= Environment.NewLine & "       M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
            '    tSql &= Environment.NewLine & "       M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
            '    tSql &= Environment.NewLine & "       M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest"
            '    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
            '    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M2.FTOrderNo = M3.FTOrderNo"
            '    tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
            '    tSql &= Environment.NewLine & "     AND M3.FTSubOrderNo = M4.FTSubOrderNo"
            '    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
            '    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
            '    tSql &= Environment.NewLine & "WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    tSql &= Environment.NewLine & "      AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
            '    tSql &= Environment.NewLine & "                             FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
            '    tSql &= Environment.NewLine & "                             WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    tSql &= Environment.NewLine & "                                   AND L1.FTPONo = M1.FTPONo"
            '    tSql &= Environment.NewLine & "                                   AND L1.FTStyle = M1.FTStyle"
            '    tSql &= Environment.NewLine & "                             GROUP BY L1.FTPONo,L1.FTStyle)"
            '    tSql &= Environment.NewLine & "       AND M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
            '    tSql &= Environment.NewLine & "ORDER BY M2.FTOrderNo ASC, M3.FTSubOrderNo ASC, M5.FNMatColorSeq ASC, M6.FNMatSizeSeq ASC;"

            '    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '        'MsgBox("Step Append New ImportOrderUpdExtraQtyTemp !!!")
            '        HI.Conn.SQLConn.Tran.Rollback()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        _bImportComplete = False
            '    Else
            '        HI.Conn.SQLConn.Tran.Commit()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        _bImportComplete = True
            '    End If

            'End If

            'If _bImportComplete = True Then

            '    If _bImportComplete = True Then

            '        '==============================================================================================================================================================================
            '        '...when execute import order complete next process update test garment and update (%) extra qty by vendor programme/main category type basketball, football/plant/buy group
            '        '...called sproc execute for update extra garment qty/ test qty garment
            '        tSql = ""
            '        tSql = "EXEC SP_UPDATE_IMPORT_ORDER_QTY_EXTRA_GARMENT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            '        'If HI.Conn.SQLConn.ExecuteOnly(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

            '        '    tSql = ""
            '        '    tSql = "UPDATE X"
            '        '    tSql &= Environment.NewLine & "SET X.FNGrandQuantity = ISNULL(X.FNQuantity,0) + ISNULL(Y.FNQuantityExtra, 0)+ ISNULL(X.FNGarmentQtyTest, 0),"
            '        '    tSql &= Environment.NewLine & "    X.FNAmntExtra = ISNULL(Y.FNQuantityExtra,0) * X.FNPrice,"
            '        '    tSql &= Environment.NewLine & "    X.FNGrandAmnt = ISNULL(X.FNAmt, 0) + (ISNULL(Y.FNQuantityExtra, 0) * X.FNPrice),"
            '        '    tSql &= Environment.NewLine & "    X.FNAmntQtyTest = ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice"
            '        '    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS X LEFT JOIN (SELECT M2.FTOrderNo, M3.FTSubOrderNo,"
            '        '    tSql &= Environment.NewLine & "                                                                        M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
            '        '    tSql &= Environment.NewLine & "                                                                        M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
            '        '    tSql &= Environment.NewLine & "                                                                        M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest"
            '        '    tSql &= Environment.NewLine & "                                                                 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
            '        '    tSql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M2.FTOrderNo = M3.FTOrderNo"
            '        '    tSql &= Environment.NewLine & "  																     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
            '        '    tSql &= Environment.NewLine & " 																               AND M3.FTSubOrderNo = M4.FTSubOrderNo"
            '        '    tSql &= Environment.NewLine & "   																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
            '        '    tSql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
            '        '    tSql &= Environment.NewLine & "                                                                 WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '        '    tSql &= Environment.NewLine & "																          AND M1.FNRowImport in (SELECT MAX(L1.FNRowImport)"
            '        '    tSql &= Environment.NewLine & " 																						    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
            '        '    tSql &= Environment.NewLine & "                                                                                             WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '        '    tSql &= Environment.NewLine & " 																							      AND L1.FTPONo = M1.FTPONo"
            '        '    tSql &= Environment.NewLine & " 																						    GROUP BY L1.FTPONo,L1.FDShipDate,L1.FTStyle)"                   
            '        '    tSql &= Environment.NewLine & "                                                                       AND  M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
            '        '    tSql &= Environment.NewLine & " 															    ) AS Y ON X.FTOrderNo = Y.FTOrderNo"
            '        '    tSql &= Environment.NewLine & " WHERE  X.FTSubOrderNo = Y.FTSubOrderNo"
            '        '    tSql &= Environment.NewLine & "        AND X.FNHSysMatColorId = Y.FNHSysMatColorId"
            '        '    tSql &= Environment.NewLine & "        AND X.FNHSysMatSizeId = Y.FNHSysMatSizeId"

            '        '    If HI.Conn.SQLConn.ExecuteOnly(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
            '        '        _bImportComplete = True

            '        '    Else
            '        '        _bImportComplete = False
            '        '    End If

            '        'End If
            '        '==============================================================================================================================================================================

            '    Else

            '    End If

            'End If

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
                      
                    End Try

                Case Else
                    '...do nothing
            End Select

            Call Me.ocmReadExcel.PerformClick()

        End If

    End Sub

    Private Sub wImportSalesman_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.FNHSysCustId.Focus()
    End Sub

    Private Sub wImportSalesman_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True

        Me.otcImportOrderNo.TabPages.Item(1).PageVisible = False

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

        Me.FNHSysUnitId.Text = "PCS"
        Me.FNHSysCurId.Text = "US"

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

    Private Function W_PRCbShowBrowseDataImportFactoryOrder() As Boolean
        Dim bRet As Boolean = False

        Try
            Dim oDBdtStatic As System.Data.DataTable
            Dim oDBdtDynamic As System.Data.DataTable
            Dim oDBdtMatSize As System.Data.DataTable

            tSql = ""
            tSql = ";WITH cteImportSize(FTMatSize)"
            tSql &= Environment.NewLine & "AS (SELECT DISTINCT L1.FTSizeBreakdownCode  AS FTSizeBreakdownCode"
            tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanSizeBreakdownTemp] AS L1"
            tSql &= Environment.NewLine & "    WHERE L1.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "    )"
            tSql &= Environment.NewLine & "--SELECT B.FNHSysMatSizeId, A.FTMatSize, B.FTMatSizeNameEN, B.FTMatSizeNameEN"
            tSql &= Environment.NewLine & "SELECT B.FTMatSizeCode"
            tSql &= Environment.NewLine & "FROM cteImportSize AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS B WITH(NOLOCK) ON A.FTMatSize = B.FTMatSizeCode"
            tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC;"

            oDBdtMatSize = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.AppendLine("SELECT DISTINCT A.FTPONo, B.FTMerTeamCode, B.FTMerTeamNameEN AS FTMerTeamDesc, A.FTStyleNo AS FTStyle, A.FTProdStyleName AS FTStyleDesc,")
            oStrBuilder.AppendLine("       CONVERT(VARCHAR(10), CAST(A.FDOrderDate AS DATE), 103) AS FTOrderDate, C.FTBuyGrpCode AS FTBuyGrpCode, C.FTBuyGrpNameEN AS FTBuyGrpDesc, D.FTMainCategoryCode AS FTMainCategoryCode, D.FTMainCategoryDescEN AS FTMainCategoryDesc,")
            oStrBuilder.AppendLine("	   E.FTProdTypeCode + ' : ' + E.FTProdTypeNameEN AS FTProdTypeDesc, F.FTSeasonCode + ' : ' + F.FTSeasonNameEN AS FTPlanningSeason,")
            oStrBuilder.AppendLine("	   G.FTAF_REQ AS FTAF_REQ, G.FTCU_CD AS FTCU_CD, G.FTCUST_XREF AS FTCUST_XREF,")
            oStrBuilder.AppendLine("	   G.FDShipmentDate, G.FDShipmentDateOriginal, G.FTShipModeDesc, G.FTGenderCode,")
            oStrBuilder.AppendLine("	   G.FTUnitDesc, G.FTColorwayCode,A.FTSubPGM,A.FTCmp")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMerTeam AS B (NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId")
            oStrBuilder.AppendLine("                                                             LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS C (NOLOCK) ON A.FNHSysBuyGrpId = C.FNHSysBuyGrpId")
            oStrBuilder.AppendLine("														     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainCategoryType AS D (NOLOCK) ON A.FNHSysMainCategoryId = D.FNHSysMainCategoryId")
            oStrBuilder.AppendLine("														     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMProductType AS E (NOLOCK) ON A.FNHSysProdTypeId = E.FNHSysProdTypeId")
            oStrBuilder.AppendLine("														     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMSeason AS F (NOLOCK) ON A.FNHSysSeasonId = F.FNHSysSeasonId")
            oStrBuilder.AppendLine("														     LEFT JOIN (SELECT A.FTUserLogIn, A.FTPONo, A.FTStyleNo, A.FTAF_REQ AS FTAF_REQ, A.FTCU_CD AS FTCU_CD, A.FTCUST_XREF AS FTCUST_XREF,")
            oStrBuilder.AppendLine("                                                                             CASE WHEN ISDATE(A.FTCURRNT_ETS) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTCURRNT_ETS AS DATE), 103) ELSE '' END AS FDShipmentDate,")
            oStrBuilder.AppendLine("	                                                                         CASE WHEN ISDATE(A.FTORIGNL_ETS) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTORIGNL_ETS AS DATE), 103) ELSE '' END AS FDShipmentDateOriginal,")
            oStrBuilder.AppendLine("                                                                             A.FTMD_TR AS FTShipModeDesc, A.FTCMO_Consumer_Group AS FTGenderCode, A.FTColorwayCode AS FTColorwayCode, A.FTSizeBreakdownCode, A.FNQuantity AS FNQuantity, N'PCS' AS FTUnitDesc")
            oStrBuilder.AppendLine("                                                                        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A (NOLOCK)")
            oStrBuilder.AppendLine("																		WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
            oStrBuilder.AppendLine("																	  ) AS G ON  A.FTPONo = G.FTPONo AND A.FTStyleNo = G.FTStyleNo AND A.FTUserLogIn = G.FTUserLogIn")
            oStrBuilder.AppendLine("WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
            oStrBuilder.AppendLine("ORDER BY A.FTPONo ASC, A.FTStyleNo ASC, G.FTAF_REQ ASC, G.FTCU_CD ASC, G.FTCUST_XREF ASC, G.FTColorwayCode ASC")

            If oStrBuilder.Length > 0 Then
                tSql = ""
                tSql = oStrBuilder.ToString()
            End If

            oDBdtStatic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtMatSize Is Nothing And oDBdtMatSize.Rows.Count > 0 Then

                tSql = ""
                tSql = "SELECT A.FTPONo, A.FTStyleNo AS FTStyle, A.FTAF_REQ, A.FTCU_CD, A.FTCU_CD, A.FTCUST_XREF, A.FTColorwayCode, A.FTSizeBreakdownCode AS FTMatSizeCode, A.FNQuantity,A.FTCURRNT_ETS"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A (NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "ORDER  BY A.FTPONo ASC, A.FTStyleNo ASC, A.FTAF_REQ ASC, A.FTCU_CD ASC, A.FTCUST_XREF ASC, A.FTColorwayCode ASC, A.FTSizeBreakdownCode ASC;"

                oDBdtDynamic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                For Each oRow As DataRow In oDBdtMatSize.Rows

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

                Next

                If Not oDBdtStatic Is Nothing AndAlso oDBdtStatic.Rows.Count > 0 Then oDBdtStatic.AcceptChanges()

                'If  (1 > 2) AndAlso oDBdtDynamic.Rows.Count > 0 Then
                If Not oDBdtDynamic Is Nothing AndAlso oDBdtDynamic.Rows.Count > 0 Then

                    Dim nLoopRowImport As Integer

                    For nLoopRowImport = 0 To oDBdtStatic.Rows.Count - 1

                        Try
                            Dim oDataRow() As DataRow

                            Dim tTextFTPONo As String
                            Dim tTextFTStyleNo As String
                            Dim tTextFTAF_REQ As String
                            Dim tTextFTCU_CD As String
                            Dim tTextFTCUST_XREF As String
                            Dim tTextFTColorwayCode As String

                            tTextFTPONo = "" : tTextFTStyleNo = "" : tTextFTAF_REQ = "" : tTextFTCU_CD = "" : tTextFTCUST_XREF = "" : tTextFTColorwayCode = ""

                            If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTPONo")) Then
                                tTextFTPONo = oDBdtDynamic.Rows(nLoopRowImport).Item("FTPONo").ToString.Trim
                            End If

                            If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTStyle")) Then
                                tTextFTStyleNo = oDBdtDynamic.Rows(nLoopRowImport).Item("FTStyle").ToString.Trim
                            End If

                            If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTAF_REQ")) Then
                                tTextFTAF_REQ = oDBdtDynamic.Rows(nLoopRowImport).Item("FTAF_REQ").ToString.Trim
                            End If

                            If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTCU_CD")) Then
                                tTextFTCU_CD = oDBdtDynamic.Rows(nLoopRowImport).Item("FTCU_CD").ToString
                            End If

                            If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTCUST_XREF")) Then
                                tTextFTCUST_XREF = oDBdtDynamic.Rows(nLoopRowImport).Item("FTCUST_XREF").ToString
                            End If

                            If Not DBNull.Value.Equals(oDBdtDynamic.Rows(nLoopRowImport).Item("FTColorwayCode")) Then
                                tTextFTColorwayCode = oDBdtDynamic.Rows(nLoopRowImport).Item("FTColorwayCode").ToString
                            End If

                            oDataRow = oDBdtDynamic.Select("FTPONo = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTPONo").ToString) &
                                                            "' AND FTStyle = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTStyle").ToString) &
                                                            "' AND FTAF_REQ = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTAF_REQ").ToString) &
                                                            "' AND FTCU_CD = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTCU_CD").ToString) &
                                                            "' AND FTCUST_XREF = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTCUST_XREF").ToString) &
                                                            "' AND FTCURRNT_ETS = '" & HI.UL.ULDate.ConvertEnDB(oDBdtStatic.Rows(nLoopRowImport).Item("FDShipmentDate").ToString) &
                                                            "' AND FTColorwayCode = '" & HI.UL.ULF.rpQuoted(oDBdtStatic.Rows(nLoopRowImport).Item("FTColorwayCode").ToString) & "'")

                            If Not oDataRow Is Nothing AndAlso oDataRow.Count > 0 Then

                                For nLoopDataRow As Integer = 0 To oDataRow.Count - 1

                                    If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FTMatSizeCode")) Then

                                        Dim tFTMatSizeCode As String = ""
                                        tFTMatSizeCode = oDataRow(nLoopDataRow).Item("FTMatSizeCode").ToString()

                                        If Not DBNull.Value.Equals(oDataRow(nLoopDataRow).Item("FNQuantity")) Then

                                            oDBdtStatic.Rows(nLoopRowImport).Item("FNQuantity" & tFTMatSizeCode) = oDataRow(nLoopDataRow).Item("FNQuantity")

                                        End If

                                    End If

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

            Call W_PRCxInitialGridBandView()

        Catch ex As Exception
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        End Try

        Return bRet

    End Function

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try
            If Me.FNHSysVenderPramId.Text <= "" Or Me.FNHSysVenderPramId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysVenderPramId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysSeasonId.Text <= "" Or Me.FNHSysSeasonId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysSeasonId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysUnitId.Text <= "" Or Me.FNHSysUnitId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysCurId.Text <= "" Or Me.FNHSysCurId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysCurId_lbl.Text)
                Exit Sub
            End If

            If Me.FDOrderDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDOrderDate_lbl.Text)
                Exit Sub
            End If

            Dim _CheckOrderQtyZero As Boolean = False

            Dim oDBdtExcel As System.Data.DataTable

            oDBdtExcel = New System.Data.DataTable

            If Me.FTFilePath.Text.Trim() <> "" Then

                If IO.File.Exists(Me.FTFilePath.Text.Trim().ToString()) Then

                    _bCatchSrcFile = False

                    Dim _FilePath As String
                    _FilePath = Me.FTFilePath.Text.Trim()

                    Dim tFileExtension As String

                    tFileExtension = IO.Path.GetExtension(_FilePath)

                    '=====================================================================================================================================================================================================================
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
                            End If

                        Next

                        If Not bValidateSheetName Then
                            HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                            Exit Sub
                        Else
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

                    _oSplash = New HI.TL.SplashScreen(_tSplashText, "", True)

                    REM
                    Application.DoEvents()

                    Dim _FoundPrm As Boolean = False '...กำหนดโครงการ Program HIT/HIG/HIP...

                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From Excel File.......")

                    'Dim _FoundPrm As Boolean = False

                    With ogvConfirmImport
                        For nLoopConfirmImport As Integer = .Columns.Count - 1 To 0 Step -1
                            Select Case Microsoft.VisualBasic.Left(.Columns(nLoopConfirmImport).Name.ToString, 4).ToUpper
                                Case "oCol".ToUpper
                                    '...none dynamic column is not remov (static columns)
                                Case Else
                                    '...remove dynamic column : quantity order by size breakdown
                                    .Columns.Remove(.Columns(nLoopConfirmImport))
                            End Select

                        Next

                    End With

                    Call W_PRCbLoadMapSize()

                    oDBdtExcel = HI.UL.ReadExcel.Read(_FilePath, "Sheet1", -1)

                    If Not oDBdtExcel Is Nothing AndAlso oDBdtExcel.Columns.Count > _nTerminateColValidSrc Then

                        oDBdtExcel.BeginInit()

                        For nLoopColSrcValid As Integer = (oDBdtExcel.Columns.Count - 1) To (_nTerminateColValidSrc) Step -1
                            oDBdtExcel.Columns.RemoveAt(nLoopColSrcValid)
                        Next nLoopColSrcValid

                        Dim bFlagValidRowBlankHeader As Boolean = False
                        Dim tTempValidateRowBlankHeader As String

                        If Not bFlagValidRowBlankHeader AndAlso Not DBNull.Value.Equals(oDBdtExcel.Rows(_nStartRowImportExcel)("F1")) Then
                            tTempValidateRowBlankHeader = ""

                            tTempValidateRowBlankHeader = oDBdtExcel.Rows(_nStartRowImportExcel)("F1").ToString '...F1 : PO NUMBER

                            If tTempValidateRowBlankHeader <> "" AndAlso tTempValidateRowBlankHeader Like _tRowBlankHeader Then
                                oDBdtExcel.Rows.RemoveAt(_nStartRowImportExcel)
                                bFlagValidRowBlankHeader = True
                            End If

                        End If

                        If Not bFlagValidRowBlankHeader AndAlso Not DBNull.Value.Equals(oDBdtExcel.Rows(_nStartRowImportExcel)("F2")) Then
                            tTempValidateRowBlankHeader = ""

                            tTempValidateRowBlankHeader = oDBdtExcel.Rows(_nStartRowImportExcel)("F2").ToString '...F2 : P.I

                            If tTempValidateRowBlankHeader <> "" AndAlso tTempValidateRowBlankHeader Like _tRowBlankHeader Then
                                oDBdtExcel.Rows.RemoveAt(_nStartRowImportExcel)
                                bFlagValidRowBlankHeader = True
                            End If

                        End If

                        If Not bFlagValidRowBlankHeader AndAlso Not DBNull.Value.Equals(oDBdtExcel.Rows(_nStartRowImportExcel)("F3")) Then

                            tTempValidateRowBlankHeader = ""

                            tTempValidateRowBlankHeader = oDBdtExcel.Rows(_nStartRowImportExcel)("F3").ToString '...PO I.D.

                            If tTempValidateRowBlankHeader <> "" AndAlso tTempValidateRowBlankHeader Like _tRowBlankHeader Then
                                oDBdtExcel.Rows.RemoveAt(_nStartRowImportExcel)
                                bFlagValidRowBlankHeader = True
                            End If

                        End If

                        oDBdtExcel.EndInit()

                        oDBdtExcel.AcceptChanges()

                    End If

                    Dim DTSrcExcel As System.Data.DataTable

                    DTSrcExcel = oDBdtExcel.Copy()

                    Me.ogdImportOrder.DataSource = DTSrcExcel
                    Me.ogdImportOrder.Refresh()
                    Me.ogvImportOrder.RefreshData()
                    Me.ogvImportOrder.OptionsView.ColumnAutoWidth = False
                    Me.ogvImportOrder.OptionsBehavior.AllowAddRows = DefaultBoolean.False

                    If Not Me.ogdImportOrder.DataSource Is Nothing AndAlso CType(Me.ogdImportOrder.DataSource, System.Data.DataTable).Rows.Count > 0 Then

                        For nLoopColGridViewSrc As Integer = 0 To ogvImportOrder.Columns.Count - 1

                            Application.DoEvents()

                            With Me.ogvImportOrder
                                .Columns(nLoopColGridViewSrc).OptionsColumn.AllowMove = False
                                .Columns(nLoopColGridViewSrc).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .Columns(nLoopColGridViewSrc).SortOrder = DevExpress.Data.ColumnSortOrder.None
                                .Columns(nLoopColGridViewSrc).OptionsColumn.AllowEdit = False
                            End With

                        Next nLoopColGridViewSrc

                    End If

                    If oDBdtExcel.Select("F3 Like '%" & HI.UL.ULF.rpQuoted(Me.FNHSysVenderPramId.Text.Trim) & "%'").Length > 0 Then

                        _FoundPrm = True

                        oDBdtExcel.BeginInit()
                        For Each oDataRowNotLikeVenderPram As System.Data.DataRow In oDBdtExcel.Select("F3 Not Like '%" & HI.UL.ULF.rpQuoted(Me.FNHSysVenderPramId.Text.Trim) & "%'")
                            oDataRowNotLikeVenderPram.Delete()
                        Next
                        oDBdtExcel.EndInit()

                        oDBdtExcel.AcceptChanges()

                        If Not oDBdtExcel Is Nothing And oDBdtExcel.Rows.Count > 0 Then

                            Dim tColPONumber As String = "F1"
                            Dim tColStyleNo As String = "F6"
                            Dim tColColorCode As String = "F7"

                            '...update PO.Number,Style Number, Color Code
                            '...F1 : PO. Number, F6 : Style Number, F7 : CLR CODE
                            oDBdtExcel.BeginInit()
                            For Each oRowUpdateCustPONoStyleNoColor As System.Data.DataRow In oDBdtExcel.Rows
                                oRowUpdateCustPONoStyleNoColor.Item(tColPONumber) = _tPrefixCustPONumber & Microsoft.VisualBasic.Right(oRowUpdateCustPONoStyleNoColor.Item(tColStyleNo).ToString, 3)
                                oRowUpdateCustPONoStyleNoColor.Item(tColStyleNo) = oRowUpdateCustPONoStyleNoColor.Item(tColStyleNo).ToString '& Me.FNHSysSeasonId.Text.Trim

                                If oRowUpdateCustPONoStyleNoColor.Item(tColColorCode).ToString <> "" AndAlso Len(Microsoft.VisualBasic.Trim(oRowUpdateCustPONoStyleNoColor.Item(tColColorCode).ToString)) < 3 Then
                                    oRowUpdateCustPONoStyleNoColor.Item(tColColorCode) = Microsoft.VisualBasic.Right$("00" & oRowUpdateCustPONoStyleNoColor.Item(tColColorCode).ToString.Trim, 3)
                                End If

                            Next
                            oDBdtExcel.EndInit()

                            oDBdtExcel.AcceptChanges()

                            Application.DoEvents()

                            _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเกี่ยวกับ Merchandiser Team, Main Category Product Type Code")

                            If Not W_PRCbValidateMatchMerchanTeam(oDBdtExcel) Then
                                '...Nothing
                            ElseIf Not W_PRCbValidateMatchMerMainCategoryType(oDBdtExcel) Then
                                '...Nothing
                            Else

                                _bCatchSrcFile = True

                                Application.DoEvents()

                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลหลัก {Master file} กรุณารอสักครู่...")

                                Call W_PRCxValidateMaster(oDBdtExcel)

                                Application.DoEvents()

                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Load data to temp for import factory orders salesman...")

                                Application.DoEvents()

                                If W_PRCbSaveListOrderSalesManToTemp(oDBdtExcel) = True Then

                                    Application.DoEvents()

                                    If CheckOrderQtyZero() = False Then
                                        Call W_PRCbShowBrowseDataImportFactoryOrder()
                                    Else
                                        '...clear temp import salesman orders

                                        tSql = ""
                                        tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                                        tSql &= Environment.NewLine & "DELETE B FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS B WHERE B.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                        HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                                        _oSplash.Close()


                                        HI.MG.ShowMsg.mInfo("พบข้อมูลจำนวน Order เป็น 0 ไม่สามารถทำการ โหลด File ได้ กรูณาทำการตรวจสอบยอด Order !!!", 1407010001, Me.Text, FNHSysVenderPramId_lbl.Text & "  " & Me.FNHSysVenderPramId.Text, MessageBoxIcon.Warning)

                                    End If

                                Else
                                    _oSplash.Close()
                                    HI.MG.ShowMsg.mInfo("พบข้อผิดพลาดในการตรวจสอบข้อมูลก่อนนำเข้่ารายการใบสั่งผลิต !!!", 1502300002, Me.Text, , MessageBoxIcon.Warning)

                                    Exit Sub
                                End If

                            End If

                        Else
                            _oSplash.Close()
                            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลสำหรับการนำเข้ารายการใบสั่งผลิตอัตโนมัติ...worksheet 1 !!!", 1502300001, Me.Text, , MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                    End If

                    _oSplash.Close()

                  

                    If Not (_FoundPrm) Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล File Program ตามที่ระบุ กรุณาทำการตรวจสอบ File !!!", 140630001, Me.Text, FNHSysVenderPramId_lbl.Text & "  " & Me.FNHSysVenderPramId.Text, MessageBoxIcon.Warning)
                        FNHSysVenderPramId.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInfo("Unable to locate source file", 1502300003, Me.Text, , MessageBoxIcon.Warning)


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
                            MsgBox("การกำหนดข้อมูลทีมเมอร์แชนไดเซอร์บางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "การกำหนดข้อมูลประเภทผลิตภัณฑ์บางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & "หรือยังไม่มีการกำหนดข้อมูลหลักประเภทผลิตภัณฑ์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        Case Else
                            MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!" & Environment.NewLine & "Invalid Main Category Type " & Environment.NewLine & "Or Not provide for main category type master file !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                    End Select

                    Exit Sub

                End If

                tSql = ""
                tSql = "SELECT TOP 1 A.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData AS A (NOLOCK) WHERE A.FTListName = N'FNOrderType' AND A.FNListIndex = 13;"

                If Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_SYSTEM, "-1")) <> 13 Then

                    Select Case HI.ST.Lang.Language
                        Case HI.ST.Lang.eLang.TH
                            MsgBox("ไม่พบการกำหนดค่าประเภทรายการใบสั่งผลิต เซลส์แมน {13}..." & Environment.NewLine & "กรุณาติดต่อ ผู้ดูแลระบบ !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        Case Else
                            MsgBox("System cannot found data order type salesman {13}..." & Environment.NewLine & "please contract administrator !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    End Select

                    Exit Sub

                Else
                   
                End If

                If W_PRCbValidateConfirmGenerateFactoryOrder() = True Then

                    If HI.MG.ShowMsg.mConfirmProcess("-----", 1404240002, "Confirm") = True Then

                        Dim _Spls As New HI.TL.SplashScreen("Generate Factory Orders Salesman .....Please Wait")

                        If W_PRCbImportFactoryOrder(_Spls) = True Then

                            '...clear temp after process import orders salesman  complete
                            '---------------------------------------------------------------------------------------------------------------------------------------------------------
                            Application.DoEvents()

                            _Spls.UpdateInformation("Finishing Generate Orders Salesman .....Please Wait")

                            Try
                                tSql = ""
                                tSql = "UPDATE A"
                                tSql &= Environment.NewLine & "SET A.FPOrderImage1 = C.FPStyleImage1,"
                                tSql &= Environment.NewLine & "    A.FPOrderImage2 = C.FPStyleImage2,"
                                tSql &= Environment.NewLine & "    A.FPOrderImage3 = C.FPStyleImage3,"
                                tSql &= Environment.NewLine & "    A.FPOrderImage4 = C.FPStyleImage4"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS B ON A.FTOrderNo = B.FTGenerateOrderNo"
                                tSql &= Environment.NewLine & "                          LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS C ON A.FNHSysStyleId = C.FNHSysStyleId"
                                tSql &= Environment.NewLine & "WHERE B.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                                Else

                                End If

                            Catch ex As Exception
                            End Try

                            Dim Cmdstring As String = ""
                            Dim dtstss As DataTable

                            Cmdstring = "  SELECT  DISTINCT C.FTOrderNo  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanTemp] AS A "
                            Cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                            Cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                            dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                            For Each R As DataRow In dtstss.Rows

                                Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                                HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                            Next

                            tSql = ""
                            tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                            tSql &= Environment.NewLine & "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            '---------------------------------------------------------------------------------------------------------------------------------------------------------

                            _bCatchSrcFile = False

                            Me.ogdImportOrder.DataSource = Nothing
                            Me.ogdImportOrder.Refresh()

                            Call W_PRCbShowBrowseDataImportFactoryOrder()

                            _Spls.Close()

                            Select Case HI.ST.Lang.Language
                                Case HI.ST.Lang.eLang.TH
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "นำเข้าข้อมูลรายการใบสั่งผลิต {Salesman} เรียบร้อยแล้ว...")
                                Case Else
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Import Factory Orders {Salesman}. Complete...")
                            End Select

                        Else
                            tSql = ""
                            tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                            tSql &= Environment.NewLine & "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            _Spls.Close()

                            MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิตอัตโนมัติ {Salesman} !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)

                        End If

                    Else
                        '...do nothing
                    End If

                Else
                    '...do nothing
                End If

            Else
                MsgBox("Cannot Import to Factory Orders Salesman. , Please validate source file for import data !!!", vbOKOnly + MsgBoxStyle.Information, "Information")
            End If

        Catch ex As Exception

            tSql = ""
            tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanSizeBreakdownTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
            tSql &= Environment.NewLine & " DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanTemp AS A WHERE A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
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

        _Qry = "SELECT  FTPONo"
        _Qry &= vbCrLf & "FROM (SELECT  L1.FTPONo, SUM(ISNULL(L1.FNQuantity,0)) AS FNQuantity"
        _Qry &= vbCrLf & "      FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportSalesmanSizeBreakdownTemp AS L1 WITH(NOLOCK)"
        _Qry &= vbCrLf & "      WHERE  (FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
        _Qry &= vbCrLf & "      GROUP BY  L1.FTPONo) AS A"
        _Qry &= vbCrLf & "WHERE A.FNQuantity <= 0 "

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "")

    End Function

    Private Sub FNHSysCustId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCustId.EditValueChanged
        Try
            If Me.FNHSysCustId.Text.Trim <> "" AndAlso Val(Me.FNHSysCustId.Properties.Tag) > 0 Then
                Dim sSql As String
                sSql = ""
                sSql = "SELECT TOP 1 A.FTCurCode"
                sSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS A (NOLOCK)"
                sSql &= Environment.NewLine & "WHERE A.FNHSysCurId = (SELECT TOP 1 L1.FNHSysCurId"
                sSql &= Environment.NewLine & "                       FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS L1 (NOLOCK)"
                sSql &= Environment.NewLine & "                       WHERE L1.FNHSysCustId  = " & Val(Me.FNHSysCustId.Properties.Tag) & ")"
                'If HI.Conn.SQLConn.GetField(sSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") <> "" Then
                '    Me.FNHSysCustId.Text = ""
                'Else
                '    Me.FNHSysCustId.Text = ""
                'End If
                Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField(sSql, HI.Conn.DB.DataBaseName.DB_MASTER, "")

            Else
                Me.FNHSysCurId.Text = ""
            End If

        Catch ex As Exception
            
        End Try
    End Sub

End Class
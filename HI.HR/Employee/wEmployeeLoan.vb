

Imports System.IO
Imports DevExpress.CodeParser
Imports System.Data.SqlClient
Imports System.Text
Imports System
Imports DevExpress.PivotGrid.QueryMode
Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports System.Globalization
Imports DevExpress.XtraSpreadsheet.Export

Public Class wEmployeeLoan
    Private _wEmployeeLoan_Popup As wEmployeeLoan_Popup
    Private _DefailtPath As String
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitGrid()
        InitGrid_Export()
        ' Add any initialization after the InitializeComponent() call.
        _wEmployeeLoan_Popup = New wEmployeeLoan_Popup
        HI.TL.HandlerControl.AddHandlerObj(_wEmployeeLoan_Popup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wEmployeeLoan_Popup.Name.ToString.Trim, _wEmployeeLoan_Popup)
        Catch ex As Exception
        Finally
        End Try

    End Sub

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Procedure"
    Private Sub LoadDataInfo()

        '' Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")


        'Dim _Qry As String = ""
        '_Qry = " SELECT A.* "
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' เดือน ' + convert(nvarchar(30), A.FNMonthWorkAgeDay) +' วัน' AS FTWorkAge "
        '    _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' เดือน' AS FTEmpAge "
        'Else
        '    _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' Month ' + convert(nvarchar(30) , A.FNMonthWorkAgeDay) +' Day'  AS FTWorkAge "
        '    _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' Month' AS FTEmpAge "
        'End If


        'With Me.ogc
        '    .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        '    ogv.ExpandAllGroups()
        '    ogv.RefreshData()
        'End With

    End Sub
#End Region

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        With ogv

            Dim sFieldCount As String = ""
            Dim sFieldSum As String = "FCReqFinAmt112|FCReqFinAmt113|FCReqTotalAmt|FCFinAmt112|FCFinAmt113|FCTotalAmt"
            Dim sFieldGrpCount As String = ""
            Dim sFieldGrpSum As String = "FCReqFinAmt112|FCReqFinAmt113|FCReqTotalAmt|FCFinAmt112|FCFinAmt113|FCTotalAmt"

            '.Columns("FTEmpTypeCode").Group()
            '.Columns("FTSectCode").Group()
            '.Columns("FTEmpStatusName").Group()

            '.Columns("FTEmpCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode")
            '.Columns("FTEmpCode").SummaryItem.DisplayFormat = "{0:n0}"
            '.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", Nothing, "(Count by " & .Columns.ColumnByFieldName("FTEmpCode").Caption & "={0:n0})")
            '.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            '.OptionsView.ShowFooter = True
            '.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            '.OptionsView.ShowGroupPanel = True
            '.ExpandAllGroups()

            '.RefreshData()

            .ClearGrouping()
            .ClearDocument()
            '.Columns.ColumnByFieldName("FDScanDateGrp").Group()
            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            .Columns("FTEmpCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode")
            .Columns("FTEmpCode").SummaryItem.DisplayFormat = "{0:n0}"
            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", Nothing, "(Count by " & .Columns.ColumnByFieldName("FTEmpCode").Caption & "={0:n0})")

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

        End With
        '------End Add Summary Grid-------------
    End Sub

    Private Sub InitGrid_Export()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FCReqFinAmt112|FCReqFinAmt113|FCReqTotalAmt|FCFinAmt112|FCFinAmt113|FCTotalAmt"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FCReqFinAmt112|FCReqFinAmt113|FCReqTotalAmt|FCFinAmt112|FCFinAmt113|FCTotalAmt"

        With ogv_export
            .ClearGrouping()
            .ClearDocument()
            '.Columns.ColumnByFieldName("FDScanDateGrp").Group()
            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            .Columns("FTEmpCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode")
            .Columns("FTEmpCode").SummaryItem.DisplayFormat = "{0:n0}"
            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", Nothing, "(Count by " & .Columns.ColumnByFieldName("FTEmpCode").Caption & "={0:n0})")

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "General"
    Private Sub wEmployeeLoan_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        'Call InitGrid()

        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
        Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")


        'Try
        '    FNEmpStatusReport.SelectedIndex = 1
        'Catch ex As Exception
        'End Try

        'Call Me.LoadDataInfo()

        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception

        End Try


        Dim _FileName As String = System.Windows.Forms.Application.StartupPath & "\ExportData\"


        _FileName = _FileName & "TemplateStudentLoan.Xlsx"
        Try

            Dim proc = Process.GetProcessesByName("excel")

            For i As Integer = 0 To proc.Count - 1
                proc(i).Kill()
            Next i

        Catch ex As Exception
        End Try

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


        XtraTabPage3.PageVisible = False

    End Sub

    Public Shared Function ReadRegistry() As String
        Dim regKey As Microsoft.Win32.RegistryKey
        Dim valreturn As String = ""

        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\HI SOFT", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExportPOItem", "")
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

        regKey.SetValue("PathExportPOItem", value.ToString)
        regKey.Close()

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    'Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
    '    With ogv
    '        If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
    '            e.Appearance.ForeColor = System.Drawing.Color.Red
    '        End If
    '    End With
    'End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmcheck_Click(sender As Object, e As EventArgs) Handles ocmcheck.Click
        Try
            If FTPayYear.Text <> "" Then


                Dim _Cmd As String = ""
                Dim _FileName As String = ""
                Dim folderDlg As New OpenFileDialog
                With folderDlg
                    '.Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                    .Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx|Excel Worksheets(97-2003)|*.xls"
                    .FilterIndex = 1
                    .RestoreDirectory = False
                    .Multiselect = True
                    Dim dr As DialogResult = .ShowDialog()
                    If (dr = System.Windows.Forms.DialogResult.OK) Then
                        Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Import Data From File ")
                        For Each file In .FileNames
                            ' _FileName = .FileName
                            ''  MsgBox("a")
                            Call ReadXlsfile_Multiple(file, _Spls)
                            ''  MsgBox("z")
                        Next
                        _Spls.Close()
                    End If
                End With
            Else
                Dim _strAlert As String
                _strAlert = "please "
                HI.MG.ShowMsg.mProcessError(202006010001, _strAlert, Me.Text, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ReadXlsfile_Multiple(_fileName As String, ByVal _Spls As HI.TL.SplashScreen)
        Try
            ''Me.oTabPlanGen.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.oTabmaster})
            Dim _strAlert As String = ""
            Dim _Qry As String = ""
            Dim xlsFilename As String = Path.GetFileName(_fileName)
            Dim _dt As System.Data.DataTable
            _dt = HI.UL.ReadExcel.Read(_fileName, "Sheet1", 0)



            If (_dt.Rows.Count > 1) Then
                For Each R As DataRow In _dt.Rows

                    If (Val(R!F1.ToString) = False) Then
                        R.Delete()

                        Exit For
                    End If


                Next

                _Qry = " DELETE FROM dbo.TmpStudentLoan WHERE FTUser='" & HI.ST.UserInfo.UserName & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                End If

                _dt.Columns.Add("FTUser", GetType(String))
                For Each R As DataRow In _dt.Rows
                    R!FTUser = HI.ST.UserInfo.UserName
                Next

                Using con As New SqlConnection(HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR))
                    Using sqlBulkCopy As New SqlBulkCopy(con)
                        'Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.TmpStudentLoan "

                        '[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("F1", "F1")
                        sqlBulkCopy.ColumnMappings.Add("F2", "F2")
                        sqlBulkCopy.ColumnMappings.Add("F3", "F3")
                        sqlBulkCopy.ColumnMappings.Add("F4", "F4")
                        sqlBulkCopy.ColumnMappings.Add("F5", "F5")
                        sqlBulkCopy.ColumnMappings.Add("F6", "F6")
                        sqlBulkCopy.ColumnMappings.Add("F7", "F7")
                        sqlBulkCopy.ColumnMappings.Add("F8", "F8")
                        sqlBulkCopy.ColumnMappings.Add("F9", "F9")
                        sqlBulkCopy.ColumnMappings.Add("F10", "F10")
                        sqlBulkCopy.ColumnMappings.Add("F11", "F11")
                        sqlBulkCopy.ColumnMappings.Add("F12", "F12")
                        sqlBulkCopy.ColumnMappings.Add("F13", "F13")
                        sqlBulkCopy.ColumnMappings.Add("FTUser", "FTUser")
                        con.Open()
                        sqlBulkCopy.WriteToServer(_dt)
                        con.Close()
                    End Using
                End Using



                Call VerifyImportData_More()



            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        End Try

    End Sub
    Private Sub VerifyImportData_More()

        Dim _Qry As String = ""
        Dim _strAlert As String = ""

        ''verify 
        Dim _dt_more As System.Data.DataTable



        Dim _strCmpID As String
        'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
        '    _strCmpID = " M.FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
        'Else
        '    _strCmpID = " M.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
        'End If

        _strCmpID = " M.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

        _Qry = " SELECT PVT.FNHSysEmpID, PVT.FTEmpIdNo ,PVT.FNHSysEmpTypeId ,ISNULL([112],0) as 'FCFinAmt112',ISNULL([113],0) AS 'FCFinAmt113'"
        _Qry &= vbCrLf & " , FTEmpCode, FTEmpNameTH + ' ' +  FTEmpSurnameTH AS 'FTEmpName', '0' AS 'FCReqFinAmt112', '0' AS 'FCReqFinAmt113' "
        _Qry &= vbCrLf & "  , 'M' AS Condition_type, 'กยศ. ไม่ได้แจ้งหัก' AS 'typeDesc' "
        _Qry &= vbCrLf & " , ETG.FTNameEN AS 'FNEmptypeGroupName', FTEmpTypeCode ,FNCalType "
        _Qry &= vbCrLf & " , CASE WHEN ISDATE(FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,FDDateEnd),103) ELSE '' END AS FDDateEnd "
        _Qry &= vbCrLf & "  FROM "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " SELECT EF.[FNHSysEmpID], M.FTEmpIdNo, M.FNHSysEmpTypeId  , FNCalType, FNHSysCmpId "
        _Qry &= vbCrLf & " ,[FTFinCode],[FTFinAmt], FTEmpCode, FTEmpNameTH, FTEmpSurnameTH  , FTEmpTypeCode, FDDateEnd, FNEmpTypeGroup "
        _Qry &= vbCrLf & " FROM [HITECH_HR].[dbo].[THRMEmployeeFin] EF "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [HITECH_HR].[dbo].THRMEmployee As M WHERE M.FNHSysEmpID = EF.FNHSysEmpID ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC) AS M "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 FNCalType, FTEmpTypeCode, FNEmpTypeGroup FROM HITECH_MASTER.dbo.THRMEmpType WHERE M.FNHSysEmpTypeId=FNHSysEmpTypeId AND " & _strCmpID & ") AS ET "
        _Qry &= vbCrLf & " WHERE FTFinCode IN('112','113') AND FTFinAmt>0 "
        _Qry &= vbCrLf & " AND " & _strCmpID
        _Qry &= vbCrLf & " ) A "
        _Qry &= vbCrLf & " PIVOT "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " MAX(FTFinAmt)"
        _Qry &= vbCrLf & " FOR FTFinCode  IN ([112],[113]) "
        _Qry &= vbCrLf & "  )PVT "
        _Qry &= vbCrLf & " LEFT  JOIN  [HITECH_HR].[dbo].TmpStudentLoan Tmp ON Tmp.F2 = PVT.FTEmpIdNo AND Tmp.FTUser = '" & HI.ST.UserInfo.UserName & "'"
        _Qry &= vbCrLf & "  OUTER APPLY (  SELECT TOP 1 * FROM HITECH_SYSTEM.dbo.HSysListData WHERE FTListName ='FNEmptypeGroup' AND FNEmpTypeGroup=FNListIndex  ) ETG"

        _Qry &= vbCrLf & " WHERE  Tmp.F2 IS NULL "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " UNION ALL "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & "  SELECT "
        _Qry &= vbCrLf & " M.FNHSysEmpID, M.FTEmpIdNo "
        _Qry &= vbCrLf & " ,M.FNHSysEmpTypeId "
        _Qry &= vbCrLf & " ,ISNULL([F112],0) as 'FCFinAmt112',ISNULL([F113],0) AS 'FCFinAmt113' "
        _Qry &= vbCrLf & "  ,M.FTEmpCode , M.FTEmpNameTH + ' ' +  M.FTEmpSurnameTH AS 'FTEmpName', F4 AS 'FCReqFinAmt112', F5 AS 'FCReqFinAmt113'"
        _Qry &= vbCrLf & " ,'L' AS Condition_type , '' AS 'typeDesc' "
        _Qry &= vbCrLf & " , ETG.FTNameEN AS 'FNEmptypeGroupName', ET.FTEmpTypeCode, ET.FNCalType "
        _Qry &= vbCrLf & " , CASE WHEN ISDATE( M.FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime, M.FDDateEnd),103) ELSE '' END AS FDDateEnd "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].TmpStudentLoan Tmp "
        _Qry &= vbCrLf & "  LEFT JOIN "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " SELECT PVT.FNHSysEmpID, PVT.FTEmpIdNo ,PVT.FNHSysEmpTypeId ,ISNULL([112],0) as 'F112',ISNULL([113],0) AS 'F113',  FNCalType, FNHSysCmpId "
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " SELECT EF.[FNHSysEmpID], M.FTEmpIdNo, M.FNHSysEmpTypeId  , FNCalType, M.FNHSysCmpId "
        _Qry &= vbCrLf & "  ,[FTFinCode] "
        _Qry &= vbCrLf & " , CASE WHEN [FNCalType] = 0 THEN FTFinAmt * 2 ELSE FTFinAmt END AS FTFinAmt "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployeeFin] EF "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRMEmployee As M WHERE M.FNHSysEmpID = EF.FNHSysEmpID AND " & _strCmpID & " ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC ) AS M "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 FNCalType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE M.FNHSysEmpTypeId=FNHSysEmpTypeId AND M.FNHSysCmpId=FNHSysCmpId) AS ET "
        _Qry &= vbCrLf & "  WHERE FTFinCode IN('112','113') "
        _Qry &= vbCrLf & "  AND  " & _strCmpID
        _Qry &= vbCrLf & "  ) A "
        _Qry &= vbCrLf & " PIVOT "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " MAX(FTFinAmt) "
        _Qry &= vbCrLf & " FOR FTFinCode  IN ([112],[113])  "
        _Qry &= vbCrLf & " )PVT "
        _Qry &= vbCrLf & "  ) BB  "
        _Qry &= vbCrLf & " ON BB.FTEmpIdNo = Tmp.f2 "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRMEmployee As M WHERE M.FTEmpIdNo = Tmp.f2 AND " & _strCmpID & "  ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC ) AS M  "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [HITECH_MASTER].dbo.THRMEmpType WHERE M.FNHSysEmpTypeId  = FNHSysEmpTypeId ) ET "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM HITECH_SYSTEM.dbo.HSysListData WHERE FTListName ='FNEmptypeGroup' AND ET.FNEmptypeGroup=FNListIndex  ) ETG"

        _Qry &= vbCrLf & " WHERE Tmp.FTUser = '" & HI.ST.UserInfo.UserName & "' AND ((F4<>F112) OR (F5<>F113)) "

        _dt_more = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Dim _n_data_More As Integer = 0

        _n_data_More = 0

        For Each dr_m As DataRow In _dt_more.Select("Condition_type='M'")
            _n_data_More += 1
        Next




        If (_dt_more.Rows.Count > 0) Then
            ''import
            Dim msgConfirm As String
            msgConfirm = "คุณต้องการน้ำเข้าข้อมูล การนำส่งเงินกู้ยืมเพื่อการศึกษาประจำเดือน" & FNMonth.SelectedItem.ToString() & " ใช่หรือไม่ ?"


            If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, "พบจำนวนเงินไม่เท่ากัน  ต้องการตรวจสอบหรือไม่", Me.Text) = True Then
                With _wEmployeeLoan_Popup
                    .DT = _dt_more
                    .Qry_more = _Qry
                    .ShowDialog()
                    Call VerifyImportData_More()
                End With

            Else
                If _n_data_More = 0 Then

                    If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, msgConfirm, Me.Text) = True Then
                        ImportData()
                    Else
                        Exit Sub
                    End If
                Else

                End If


            End If

        Else

            Dim msgConfirm As String
            msgConfirm = "คุณต้องการน้ำเข้าข้อมูล การนำส่งเงินกู้ยืมเพื่อการศึกษาประจำเดือน" & FNMonth.SelectedItem.ToString() & " ใช่หรือไม่ ?"
            If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, msgConfirm, Me.Text) = True Then
                ImportData()
            Else
                Exit Sub
            End If

        End If
    End Sub

    Private Sub VerifyImportData_Less()

        Dim _Qry As String = ""
        Dim _strAlert As String = ""
        Dim _dt_less As System.Data.DataTable
        Dim _strCmpID As String

        'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
        '    _strCmpID = " FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
        'Else
        '    _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
        'End If

        _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

        _Qry = "  SELECT "
        _Qry &= vbCrLf & "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
        _Qry &= vbCrLf & " ,NULL,NULL,NULL "
        _Qry &= vbCrLf & " ," & FTPayYear.Text & "," & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & "," & "F1"
        _Qry &= vbCrLf & " ,M.FNHSysEmpID,F2,F3 "
        _Qry &= vbCrLf & " ,F4,F5,F6 "
        _Qry &= vbCrLf & " ,M.FNHSysEmpTypeId,M.FNHSysCmpId , F112, F113, F112+F113 AS [FCTotalAmt]"
        _Qry &= vbCrLf & " ,F7, F8,F9,F10,F11,F12,F13 "
        _Qry &= vbCrLf & "  ,M.FTEmpCode , M.FTEmpNameTH + ' ' +  M.FTEmpSurnameTH AS 'FTEmpName', F4 AS 'FCReqFinAmt112', F5 AS 'FCReqFinAmt113'"
        _Qry &= vbCrLf & ", ISNULL([F112],0) as 'FCFinAmt112',ISNULL([F113],0) AS 'FCFinAmt113' "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].TmpStudentLoan Tmp "
        _Qry &= vbCrLf & "  LEFT JOIN "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " SELECT PVT.FNHSysEmpID, PVT.FTEmpIdNo ,PVT.FNHSysEmpTypeId ,ISNULL([112],0) as 'F112',ISNULL([113],0) AS 'F113',  FNCalType, FNHSysCmpId "
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " SELECT EF.[FNHSysEmpID], M.FTEmpIdNo, M.FNHSysEmpTypeId  , FNCalType, M.FNHSysCmpId "
        _Qry &= vbCrLf & "  ,[FTFinCode] "
        _Qry &= vbCrLf & " , CASE WHEN [FNCalType] = 0 THEN FTFinAmt * 2 ELSE FTFinAmt END AS FTFinAmt "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployeeFin] EF "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRMEmployee As M WHERE M.FNHSysEmpID = EF.FNHSysEmpID AND " & _strCmpID & "  ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC  ) AS M "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT FNCalType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE M.FNHSysEmpTypeId=FNHSysEmpTypeId AND M.FNHSysCmpId=FNHSysCmpId) AS ET "
        _Qry &= vbCrLf & "  WHERE FTFinCode IN('112','113') "
        _Qry &= vbCrLf & "  AND  M." & _strCmpID
        _Qry &= vbCrLf & "  ) A "
        _Qry &= vbCrLf & " PIVOT "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " MAX(FTFinAmt) "
        _Qry &= vbCrLf & " FOR FTFinCode  IN ([112],[113])  "
        _Qry &= vbCrLf & " )PVT "
        _Qry &= vbCrLf & "  ) BB  "
        _Qry &= vbCrLf & " ON BB.FTEmpIdNo = Tmp.f2 "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRMEmployee As M WHERE M.FTEmpIdNo = Tmp.f2 AND " & _strCmpID & "   ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC ) AS M  "
        _Qry &= vbCrLf & " WHERE Tmp.FTUser = '" & HI.ST.UserInfo.UserName & "'  AND BB.FNHSysEmpID is null "
        _Qry &= vbCrLf & "   ORDER BY F1 "

        _dt_less = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        '' MsgBox("i")
        If (_dt_less.Rows.Count = 0) Then
            Call VerifyImportData_Amt()
        Else

            With _wEmployeeLoan_Popup
                .DT = _dt_less
                .ShowDialog()
                Call VerifyImportData_Less()
            End With

        End If
    End Sub

    Private Sub VerifyImportData_Amt()

        Dim _Qry As String = ""
        Dim _strAlert As String = ""
        Dim _dt_Amt As System.Data.DataTable
        Dim _strCmpID As String

        'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
        '    _strCmpID = " FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
        'Else
        '    _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
        'End If

        _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

        _Qry = "  SELECT "
        _Qry &= vbCrLf & " M.FNHSysEmpID, M.FTEmpIdNo "
        _Qry &= vbCrLf & " ,M.FNHSysEmpTypeId "
        _Qry &= vbCrLf & " ,ISNULL([F112],0) as 'FCFinAmt112',ISNULL([F113],0) AS 'FCFinAmt113' "
        _Qry &= vbCrLf & "  ,M.FTEmpCode , M.FTEmpNameTH + ' ' +  M.FTEmpSurnameTH AS 'FTEmpName', F4 AS 'FCReqFinAmt112', F5 AS 'FCReqFinAmt113'"
        _Qry &= vbCrLf & " ,'L' AS type "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].TmpStudentLoan Tmp "
        _Qry &= vbCrLf & "  LEFT JOIN "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " SELECT PVT.FNHSysEmpID, PVT.FTEmpIdNo ,PVT.FNHSysEmpTypeId ,ISNULL([112],0) as 'F112',ISNULL([113],0) AS 'F113',  FNCalType, FNHSysCmpId "
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " SELECT EF.[FNHSysEmpID], M.FTEmpIdNo, M.FNHSysEmpTypeId  , FNCalType, M.FNHSysCmpId "
        _Qry &= vbCrLf & "  ,[FTFinCode] "
        _Qry &= vbCrLf & " , CASE WHEN [FNCalType] = 0 THEN FTFinAmt * 2 ELSE FTFinAmt END AS FTFinAmt "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployeeFin] EF "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRMEmployee As M WHERE M.FNHSysEmpID = EF.FNHSysEmpID AND " & _strCmpID & "  ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC) AS M "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT FNCalType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE M.FNHSysEmpTypeId=FNHSysEmpTypeId AND M.FNHSysCmpId=FNHSysCmpId) AS ET "
        _Qry &= vbCrLf & "  WHERE FTFinCode IN('112','113') "
        _Qry &= vbCrLf & "  AND  M." & _strCmpID
        _Qry &= vbCrLf & "  ) A "
        _Qry &= vbCrLf & " PIVOT "
        _Qry &= vbCrLf & " ( "
        _Qry &= vbCrLf & " MAX(FTFinAmt) "
        _Qry &= vbCrLf & " FOR FTFinCode  IN ([112],[113])  "
        _Qry &= vbCrLf & " )PVT "
        _Qry &= vbCrLf & "  ) BB  "
        _Qry &= vbCrLf & " ON BB.FTEmpIdNo = Tmp.f2 "
        _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRMEmployee As M WHERE M.FTEmpIdNo = Tmp.f2 AND " & _strCmpID & "  ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC ) AS M  "
        _Qry &= vbCrLf & " WHERE Tmp.FTUser = '" & HI.ST.UserInfo.UserName & "' AND (F4+F5)<>(F112+F113) "
        _Qry &= vbCrLf & "   ORDER BY F1 "

        _dt_Amt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        '' MsgBox("i")
        If (_dt_Amt.Rows.Count > 0) Then
            Dim msgConfirm As String
            msgConfirm = "คุณต้องการน้ำเข้าข้อมูล การนำส่งเงินกู้ยืมเพื่อการศึกษาประจำเดือน" & FNMonth.SelectedItem.ToString() & " ใช่หรือไม่ ?"


            If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, "พบจำนวนเงินไม่เท่ากัน  ต้องการตรวจสอบหรือไม่", Me.Text) = True Then
                With _wEmployeeLoan_Popup
                    .DT = _dt_Amt
                    .ShowDialog()
                    Call VerifyImportData_Amt()
                End With

            Else
                If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, msgConfirm, Me.Text) = True Then
                    ImportData()
                Else
                    Exit Sub
                End If
            End If

        Else
            Dim msgConfirm As String
            msgConfirm = "คุณต้องการน้ำเข้าข้อมูล การนำส่งเงินกู้ยืมเพื่อการศึกษาประจำเดือน" & FNMonth.SelectedItem.ToString() & " ใช่หรือไม่ ?"

            If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, msgConfirm, Me.Text) = True Then
                ImportData()
            Else
                Exit Sub
            End If
        End If
    End Sub

    Private Sub ImportData()
        Try
            Dim _strCmpID As String
            'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
            '    _strCmpID = " FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
            'Else
            '    _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
            'End If

            _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

            Dim _Qry As String
            _Qry = " DELETE  [HITECH_HR].[dbo].THRTPayRollStudentLoan  WHERE  " & _strCmpID & "AND FTPayYear='" & FTPayYear.Text & "' AND FNMonth='" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & "' "

            _Qry &= vbCrLf & " INSERT INTO  [HITECH_HR].[dbo].THRTPayRollStudentLoan  "
            _Qry &= vbCrLf & " (FTInsUser, FTInsDate, FTInsTime, FTUpdUser, FTUpdDate, FTUpdTime"
            _Qry &= vbCrLf & " ,FTPayYear, FNMonth, FNSeq "
            _Qry &= vbCrLf & " , FNHSysEmpID, FTEmpIdNo, FTEmpNameSurname "
            _Qry &= vbCrLf & " , FCReqFinAmt112, FCReqFinAmt113, FCReqTotalAmt "
            _Qry &= vbCrLf & " , FNHSysEmpTypeId, FNHSysCmpId, FCFinAmt112, FCFinAmt113, FCTotalAmt "
            _Qry &= vbCrLf & " , FTCmpTaxNo, FTCmpCodeNo, FNMonthCal, FTYearCal, FDDateCal, FCCheckNotPay, FNReason) "
            _Qry &= vbCrLf & "  SELECT "
            _Qry &= vbCrLf & "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & " ,NULL,NULL,NULL "
            _Qry &= vbCrLf & " ," & FTPayYear.Text & "," & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & "," & "F1"
            _Qry &= vbCrLf & " ,FNHSysEmpID,F2,F3 "
            _Qry &= vbCrLf & " ,F4,F5,F6 "
            _Qry &= vbCrLf & " ,FNHSysEmpTypeId,FNHSysCmpId , F112, F113, F112+F113 AS [FCTotalAmt]"
            _Qry &= vbCrLf & " ,F7, F8,F9,F10,F11,F12,F13 "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].TmpStudentLoan Tmp "
            _Qry &= vbCrLf & "  LEFT JOIN "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT PVT.FNHSysEmpID, PVT.FTEmpIdNo ,PVT.FNHSysEmpTypeId ,ISNULL([112],0) as 'F112',ISNULL([113],0) AS 'F113',  FNCalType, FNHSysCmpId "
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT EF.[FNHSysEmpID], M.FTEmpIdNo, M.FNHSysEmpTypeId  , FNCalType, M.FNHSysCmpId "
            _Qry &= vbCrLf & "  ,[FTFinCode] "
            _Qry &= vbCrLf & " , CASE WHEN [FNCalType] = 0 THEN FTFinAmt * 2 ELSE FTFinAmt END AS FTFinAmt  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployeeFin] EF "
            _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRMEmployee As M WHERE M.FNHSysEmpID = EF.FNHSysEmpID ORDER BY M.FDDateStart DESC, M.FNEmpStatus ASC ) AS M "
            _Qry &= vbCrLf & " OUTER APPLY ( SELECT FNCalType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE M.FNHSysEmpTypeId=FNHSysEmpTypeId AND M.FNHSysCmpId=FNHSysCmpId) AS ET "
            _Qry &= vbCrLf & "  WHERE FTFinCode IN('112','113') "
            _Qry &= vbCrLf & "  AND  M." & _strCmpID
            _Qry &= vbCrLf & "  ) A "
            _Qry &= vbCrLf & " PIVOT "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " MAX(FTFinAmt) "
            _Qry &= vbCrLf & " FOR FTFinCode  IN ([112],[113])  "
            _Qry &= vbCrLf & " )PVT "
            _Qry &= vbCrLf & "  ) BB  "
            _Qry &= vbCrLf & " ON BB.FTEmpIdNo = Tmp.f2 "
            _Qry &= vbCrLf & " WHERE Tmp.FTUser = '" & HI.ST.UserInfo.UserName & "' AND FNHSysEmpID is not null"
            ' _Qry &= vbCrLf & " WHERE FNHSysEmpID is not null "

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            BindData()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindData()
        Try

            Dim _strCmpID As String
            Dim _Qry As String

            'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
            '    _strCmpID = " L.FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
            'Else
            '    _strCmpID = " L.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
            'End If

            _strCmpID = " L.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

            _Qry = " SELECT  FTPayYear, FNMonth, L.FNSeq "
            _Qry &= vbCrLf & "  , L.FNHSysEmpID, L.FTEmpIdNo, FTEmpNameSurname "
            _Qry &= vbCrLf & "  , FCReqFinAmt112, FCReqFinAmt113, FCReqTotalAmt "
            _Qry &= vbCrLf & "  , L.FNHSysEmpTypeId, L.FNHSysCmpId, FCFinAmt112, FCFinAmt113, FCTotalAmt "
            _Qry &= vbCrLf & "  , FTCmpTaxNo, FTCmpCodeNo, FNMonthCal, FTYearCal, FDDateCal, FCCheckNotPay, FNReason "
            _Qry &= vbCrLf & "  , M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName "
            _Qry &= vbCrLf & "  , ES.FTNameTH  AS FTEmpStatusName  , M.FNEmpStatus "
            _Qry &= vbCrLf & "  , ET.FTEmpTypeNameTH  AS FTEmpTypeName  "
            _Qry &= vbCrLf & "  , Dept.FTDeptDescTH  AS FTDeptName  "
            _Qry &= vbCrLf & "  , DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  , ST.FTSectNameTH  AS FTSectName "

            _Qry &= vbCrLf & " , US.FTUnitSectNameTH AS FTUnitSectName "
            _Qry &= vbCrLf & " , OrgPosit.FTPositNameTH AS  FTPositName "
            _Qry &= vbCrLf & " , M.FTEmpCode, P.FTPreNameNameTH AS FTPreNameName  "


            _Qry &= vbCrLf & "  , ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode  "
            _Qry &= vbCrLf & "  , ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode  "
            _Qry &= vbCrLf & "  , ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode, OrgPosit.FTPositCode "

            _Qry &= vbCrLf & " , ETG.FTNameEN AS 'FNEmptypeGroupName'"
            _Qry &= vbCrLf & " , CASE WHEN ISDATE(M.FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateEnd),103) ELSE '' END AS FDDateEnd "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRollStudentLoan L  "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE FNHSysEmpID=L.FNHSysEmpID) AS M "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename WHERE  M.FNHSysPreNameId = FNHSysPreNameId ) P  "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE  M.FNHSysEmpTypeId = FNHSysEmpTypeId ) ET "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment WHERE  M.FNHSysDeptId = FNHSysDeptId ) Dept "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision WHERE M.FNHSysDivisonId = FNHSysDivisonId ) DI "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect WHERE M.FNHSysSectId = FNHSysSectId ) ST  "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect WHERE  M.FNHSysUnitSectId = FNHSysUnitSectId ) US "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition WHERE  M.FNHSysPositId = FNHSysPositId ) OrgPosit "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPositionGrp WHERE  OrgPosit.FNHSysPositGrpId = FNHSysPositGrpId ) G "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus WHERE  M.FNEmpStatus = FNListIndex ) ES "
            _Qry &= vbCrLf & "  OUTER APPLY ( SELECT TOP 1 * FROM HITECH_SYSTEM.dbo.HSysListData WHERE FTListName ='FNEmptypeGroup' AND ET.FNEmptypeGroup=FNListIndex  ) ETG "
            _Qry &= vbCrLf & "  WHERE FTPayYear='" & FTPayYear.Text & "'AND FNMonth = '" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & "' "
            _Qry &= vbCrLf & "  AND  " & _strCmpID
            _Qry &= vbCrLf & " ORDER BY L.FNSeq "

            'Dim _Dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            'Me.ogc.DataSource = _Dt
            With Me.ogc
                .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                ' ogv.ExpandAllGroups()
                ogv.RefreshData()
            End With
        Catch ex As Exception

        End Try
    End Sub



    'Private Sub NewExcel(ByVal _oDt As System.Data.DataTable, _filename As String, _Spls As HI.TL.SplashScreen)
    '    Try
    '        'Dim TmpFile As String = _Path & "\Reports\TmpPayRollToBank.xlsx"
    '        'Dim BakFile As String = _Path & "\Reports\Blank.xlsx"





    '        Dim xlBookTmp As Workbook
    '        '' Dim xlBookBak As Workbook
    '        Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection


    '        xlBookTmp = oExcel.Workbooks.Open(_filename)

    '        'xlBookTmp.Worksheets(1).copy(After:=xlBookTmp.Worksheets(1))

    '        'Dim i As Integer = 13
    '        'With xlBookTmp.Worksheets(1)
    '        '    'If _oDt.Rows.Count > 0 Then
    '        '    '    .Cells(3, 1) = "Đợt  " & Me.FTPayTerm.Text & "/tháng  " & _oDt.Rows(0)!FTMonth.ToString & "  /năm " & Me.FTPayYear.Text
    '        '    '    .Cells(4, 1) = "Term  " & Me.FTPayTerm.Text & "/month  " & _oDt.Rows(0)!FTMonth.ToString & "  /year " & Me.FTPayYear.Text
    '        '    'End If

    '        '    'oExcel.Application.DisplayAlerts = False
    '        '    'oExcel.Selection.Merge()

    '        '    'oExcel.Application.DisplayAlerts = True
    '        '    'oExcel.Application.DisplayAlerts = True,
    '        '    Try

    '        '        '.Cells(i + 1, 1).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
    '        '        '.Cells(i + 1, 2).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
    '        '        '.Cells(i + 1, 3).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
    '        '        '.Cells(i + 1, 4).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
    '        '        '.Cells(i + 1, 5).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
    '        '    Catch ex As Exception
    '        '    End Try


    '        'End With

    '        ''  MsgBox("ex")
    '        Dim i As Integer = 1
    '        Dim x As Integer = 500
    '        Dim oSheet As Object
    '        oSheet = xlBookTmp.Worksheets(1)
    '        Dim lastRow As Integer = oSheet.UsedRange.Rows.Count

    '        Dim _IdNo As String = ""
    '        Dim FDDateEnd As String = ""
    '        Dim FNEmpStatus As String = ""
    '        Dim _flag_cal As String = ""
    '        ''  MsgBox(lastRow)
    '        With xlBookTmp.Worksheets(1)

    '            For z As Integer = i To lastRow


    '                ''MsgBox("ex" & z)

    '                _IdNo = .Cells(z, 2).Value
    '                'FDDateEnd = .Cells(z, 14).Value
    '                'FNEmpStatus = .Cells(z, 14).Value
    '                For Each R As DataRow In _oDt.Select("FTEmpIdNo='" & _IdNo & "'")
    '                    _flag_cal = "Y"
    '                    If (R!FDDateEnd <> "" And R!FNEmpStatus = "2") Then
    '                        '.Cells(i, 4).Font.Color = 0
    '                        .Cells(z, 4) = "'0"
    '                        '.Cells(i, 5).Font.Color = 0
    '                        .Cells(z, 5) = "'0"
    '                        .Cells(z, 12) = "'Y"
    '                        .Cells(z, 13) = "'1"
    '                    ElseIf (R!FTEmpTypeCode = "M" Or R!FTEmpTypeCode = "O" Or R!FTEmpTypeCode = "N" Or R!FTEmpTypeCode = "M1" Or R!FTEmpTypeCode = "O1" Or R!FTEmpTypeCode = "N1") Then
    '                        .Cells(z, 14) = "'M"
    '                    Else
    '                        '.Cells(i, 4).Font.Color = 0
    '                        .Cells(z, 4) = "'" & R!F112.ToString

    '                        '.Cells(i, 5).Font.Color = 0
    '                        .Cells(z, 5) = "'" & R!F113.ToString

    '                    End If

    '                Next

    '                If (_flag_cal = "") Then
    '                    .Cells(z, 14) = "'Not Calculate"
    '                End If


    '                _flag_cal = ""


    '                '.Cells(i, 1).Font.Color = 0
    '                '.Cells(i, 1) = "'" & R!FNSeq.ToString
    '                '    .Cells(i, 2).Font.Color = 0
    '                '    .Cells(i, 2) = "'" & R!Name.ToString
    '                '    .Cells(i, 3).Font.Color = 0
    '                '    .Cells(i, 3) = "'" & R!FTAccNo.ToString
    '                '    .Cells(i, 4).Font.Color = 0
    '                '    .Cells(i, 4) = "'" & R!FTEmpIdNo.ToString
    '                '    .Cells(i, 5).Font.Color = 0
    '                '    .Cells(i, 5) = R!FNNetpay.ToString
    '                '    .Cells(i, 5).NumberFormat = "#,###,###"


    '                'NumberFormat = "#,###,###"
    '            Next

    '        End With
    '        '' MsgBox("end ex")
    '        'Next


    '        Try
    '            If oExcel.Application.Sheets.Count > 4 Then
    '                For xi As Integer = oExcel.Application.Sheets.Count To 2 Step -1
    '                    Try
    '                        If Microsoft.VisualBasic.Right(CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Name.ToString(), 1) = ")" Then
    '                            CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Delete()
    '                            oExcel.Application.DisplayAlerts = False
    '                        End If
    '                    Catch ex As Exception
    '                    End Try
    '                    Try
    '                        If Microsoft.VisualBasic.Right(oExcel.Sheets(xi).Name.ToString(), 1) = ")" Then
    '                            oExcel.Sheets(xi).delete()
    '                            oExcel.Application.DisplayAlerts = True
    '                        End If
    '                    Catch ex As Exception
    '                    End Try
    '                Next
    '            End If
    '        Catch ex As Exception
    '        End Try

    '        Try
    '            CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
    '        Catch ex As Exception
    '        End Try

    '        oExcel.DisplayAlerts = False
    '        '' _filename = "C:\Users\jakkaphan.l\Desktop\TestFile.xls"

    '        xlBookTmp.SaveAs(_filename)
    '        ''xlBookTmp.Save()
    '        xlBookTmp.Close()
    '        _Spls.Close()

    '        Process.Start(_filename)



    '    Catch ex As Exception
    '        MsgBox(ex.Message.ToString)
    '        _Spls.Close()
    '        HI.MG.ShowMsg.mProcessError(1505029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)

    '    End Try
    'End Sub

    Private Sub ocmLoad_Import_Click(sender As Object, e As EventArgs) Handles ocmLoad_Import.Click
        Try
            If FTPayYear.Text <> "" Then
                BindData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmLoadDataExport_Click(sender As Object, e As EventArgs) Handles ocmLoadDataExport.Click
        Try
            If FTPayYear.Text <> "" Then
                BindDataExport()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub BindDataExport()
        Try

            Dim _strCmpID As String
            Dim _Qry As String
            Dim _FTPayYear As String, _FNMonth As String

            _FTPayYear = FTPayYear.Text
            _FNMonth = HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)

            'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
            '    _strCmpID = " L.FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
            'Else
            '    _strCmpID = " L.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
            'End If

            _strCmpID = " L.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

            _Qry = " SELECT  L.FTPayYear, L.FNMonth, L.FNSeq "
            _Qry &= vbCrLf & "  , L.FNHSysEmpID, L.FTEmpIdNo, FTEmpNameSurname "
            _Qry &= vbCrLf & "  , FCReqFinAmt112, FCReqFinAmt113, FCReqTotalAmt "
            _Qry &= vbCrLf & "  , L.FNHSysEmpTypeId, L.FNHSysCmpId "
            _Qry &= vbCrLf & " , ISNULL(F112,0) AS 'FCFinAmt112' , ISNULL(F113,0) AS 'FCFinAmt113' , ISNULL(F112,0) + ISNULL(F113,0) AS 'FCTotalAmt'"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "  , FTCmpTaxNo, FTCmpCodeNo, FNMonthCal, FTYearCal, FDDateCal, ISNULL(FCCheckNotPay,'') AS FCCheckNotPay "
            _Qry &= vbCrLf & " ,ISNULL(RS.FTNameTH,'') AS FNReason"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "  , M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName "
            _Qry &= vbCrLf & "  , ES.FTNameTH  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  , ET.FTEmpTypeNameTH  AS FTEmpTypeName  "
            _Qry &= vbCrLf & "  , Dept.FTDeptDescTH  AS FTDeptName  "
            _Qry &= vbCrLf & "  , DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  , ST.FTSectNameTH  AS FTSectName "

            _Qry &= vbCrLf & " , US.FTUnitSectNameTH AS FTUnitSectName "
            _Qry &= vbCrLf & " , OrgPosit.FTPositNameTH AS  FTPositName "
            _Qry &= vbCrLf & " , M.FTEmpCode, P.FTPreNameNameTH AS FTPreNameName  "


            _Qry &= vbCrLf & "  , ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode  "
            _Qry &= vbCrLf & "  , ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode  "
            _Qry &= vbCrLf & "  , ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode, OrgPosit.FTPositCode "

            _Qry &= vbCrLf & " , ETG.FTNameEN AS 'FNEmptypeGroupName'"
            _Qry &= vbCrLf & " , CASE WHEN ISDATE(M.FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateEnd),103) ELSE '' END AS FDDateEnd "

            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRollStudentLoan L  "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE FNHSysEmpID=L.FNHSysEmpID) AS M "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename WHERE  M.FNHSysPreNameId = FNHSysPreNameId ) P  "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE  M.FNHSysEmpTypeId = FNHSysEmpTypeId ) ET "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment WHERE  M.FNHSysDeptId = FNHSysDeptId ) Dept "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision WHERE M.FNHSysDivisonId = FNHSysDivisonId ) DI "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect WHERE M.FNHSysSectId = FNHSysSectId ) ST  "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect WHERE  M.FNHSysUnitSectId = FNHSysUnitSectId ) US "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition WHERE  M.FNHSysPositId = FNHSysPositId ) OrgPosit "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPositionGrp WHERE  OrgPosit.FNHSysPositGrpId = FNHSysPositGrpId ) G "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus WHERE  M.FNEmpStatus = FNListIndex ) ES "
            _Qry &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 * FROM HITECH_SYSTEM.dbo.HSysListData WHERE FTListName ='FNEmptypeGroup' AND ET.FNEmptypeGroup=FNListIndex  ) ETG"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " LEFT JOIN ( "
            _Qry &= vbCrLf & " SELECT piv.FTPayYear , piv.FNMonth ,piv.FNHSysEmpID ,SUM(ISNULL([112],0)) as 'F112',SUM(ISNULL([113],0)) AS 'F113' , FTEmpIdNo, FTEmpCode, FTEmpNameTH, FTEmpSurnameTH,  FNHSysCmpId "
            _Qry &= vbCrLf & "  FROM (  "
            _Qry &= vbCrLf & "    SELECT  P.FTPayYear, P.FTPayTerm,P.FNHSysEmpID,FTFinCode,  FCTotalFinAmt, D.FNMonth "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin  P "
            _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE P.FNHSysEmpID= FNHSysEmpID ORDER BY FDDateStart DESC, FNEmpStatus ) E  "
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT  AS D ON P.FTPayTerm = D.FTPayTerm AND P.FTPayYear = D.FTPayYear AND E.FNHSysEmpTypeId = D.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "  WHERE P.FTPayYear ='" & _FTPayYear & "'  AND  D.FNMonth = '" & _FNMonth & "'"
            _Qry &= vbCrLf & "   AND FTFinCode IN ('112','113') "
            _Qry &= vbCrLf & "  ) D  "
            _Qry &= vbCrLf & "  PIVOT  "
            _Qry &= vbCrLf & "  ( MAX(FCTotalFinAmt)  "
            _Qry &= vbCrLf & "  For FTFinCode   in ([112],[113])   "
            _Qry &= vbCrLf & "  ) piv "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE piv.FNHSysEmpID= FNHSysEmpID ORDER BY FDDateStart DESC, FNEmpStatus ) E "
            _Qry &= vbCrLf & " WHERE piv.FTPayYear = '" & _FTPayYear & "' AND  piv.FNMonth = '" & _FNMonth & "'"
            _Qry &= vbCrLf & " GROUP BY piv.FTPayYear, piv.FNHSysEmpID, piv.FNMonth, FTEmpIdNo, FTEmpCode, FTEmpNameTH, FTEmpSurnameTH, FNHSysCmpId  ) AS PF ON L.FNHSysEmpID=PF.FNHSysEmpID AND L.FTPayYear=PF.FTPayYear AND  L.FNMonth=PF.FNMonth "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ReasonStudentLoan WHERE  L.FNReason = FNListIndex ) RS"
            _Qry &= vbCrLf & "  WHERE L.FTPayYear='" & FTPayYear.Text & "' AND L.FNMonth = '" & _FNMonth & "' "
            _Qry &= vbCrLf & "  AND  " & _strCmpID
            _Qry &= vbCrLf & " ORDER BY L.FNSeq "

            With Me.ogc_export
                .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                ' ogv.ExpandAllGroups()
                ogv_export.RefreshData()
            End With
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ocmProcess_Click(sender As Object, e As EventArgs) Handles ocmProcess.Click
        Try

            Dim _Spls As New HI.TL.SplashScreen("Calculating...   Please Wait   ")
            Dim _strCmpID As String

            'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
            '    _strCmpID = " FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
            'Else
            '    _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
            'End If

            _strCmpID = " FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

            Dim _Qry As String

            ''  checkปิดงวด
            'Dim _Qry As String = ""
            '_Qry = "SELECT TOP 1 P.FNHSysEmpID"
            '_Qry &= vbCrLf & " FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTPayRoll AS P WITH (NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMCfgPayHD AS PD WITH (NOLOCK) "
            '_Qry &= vbCrLf & "ON P.FNHSysEmpTypeId = PD.FNHSysEmpTypeId "
            '_Qry &= vbCrLf & " 	AND P.FTPayYear  = PD.FTPayYear"
            '_Qry &= vbCrLf & " 	AND P.FTPayTerm  = PD.FTPayTerm"
            '_Qry &= vbCrLf & " WHERE P.FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & ""

            'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
            '    HI.MG.ShowMsg.mInfo("พนักงานมีการคำนวณสิ้นงวดในงวดปัจจุบันแล้ว หลังการบันทึกกรุณาแจ้งฝ่ายเงินเดือน !!!", 1300001111, Me.Text, FNHSysEmpID.Text, MessageBoxIcon.Warning)
            'End If



            '' insert into payroll payrolfin   รายเดือน

            Dim _dtEFin As System.Data.DataTable

            _Qry = " SELECT  piv.FNHSysEmpID ,ISNULL([112],0) as 'F112',ISNULL([113],0) AS 'F113' , FTEmpIdNo, FTEmpCode, FTEmpNameTH, FTEmpSurnameTH,  E.FNHSysCmpId, E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "  , FNHSysDeptId, FNHSysDivisonId, FNHSysSectId "
            _Qry &= vbCrLf & " , FNHSysUnitSectId, FNHSysPositId "

            _Qry &= vbCrLf & " FROM ( "
            _Qry &= vbCrLf & " SELECT  FNHSysEmpID,FTFinCode,  FTFinAmt  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeFin "
            _Qry &= vbCrLf & "  WHERE  FTFinCode IN ('112','113') AND FTFinAmt >0 "
            _Qry &= vbCrLf & " ) D "
            _Qry &= vbCrLf & "  PIVOT "
            _Qry &= vbCrLf & " ( MAX(FTFinAmt)  "
            _Qry &= vbCrLf & "  For FTFinCode   in ([112],[113])   "
            _Qry &= vbCrLf & " ) piv "
            _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE piv.FNHSysEmpID= FNHSysEmpID ORDER BY FDDateStart DESC, FNEmpStatus ) E  "
            _Qry &= vbCrLf & " OUTER APPLY (SELECT FNCalType FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE E.FNHSysEmpTypeId=FNHSysEmpTypeId AND E.FNHSysCmpId=FNHSysCmpId) AS ET "
            _Qry &= vbCrLf & " WHERE ET.FNCalType = 2 AND E." & _strCmpID
            _Qry &= vbCrLf & " "

            _dtEFin = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)



            Dim _FNHSysEmpID As String, _F112 As String, _F113 As String, _FTEmpIdNo As String
            Dim _FNHSysCmpId As String, _FNHSysEmpTypeId As String
            Dim _FTPayYear As String, _FTPayTerm As String, _FNMonth As String
            Dim _FNHSysDeptId As String, _FNHSysDivisonId As String, _FNHSysSectId As String
            Dim _FNHSysUnitSectId As String, _FNHSysPositId As String

            _FTPayYear = FTPayYear.Text
            _FNMonth = HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)
            _FTPayTerm = HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)
            _FTPayTerm = Format(Integer.Parse(_FTPayTerm), "00")

            Dim _Rec As Integer = 0
            Dim _TotalRec As Integer = 0

            _TotalRec = _dtEFin.Rows.Count

            For Each R As DataRow In _dtEFin.Rows
                _Rec += 1
                _Spls.UpdateInformation("Calculating... Employee Code " & R!FTEmpCode.ToString & "    " & R!FTEmpNameTH.ToString & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")

                _FNHSysEmpID = R!FNHSysEmpID.ToString
                _F112 = R!F112.ToString
                _F113 = R!F113.ToString
                _FTEmpIdNo = R!FTEmpIdNo.ToString
                _FNHSysCmpId = R!FNHSysCmpId.ToString
                _FNHSysEmpTypeId = R!FNHSysEmpTypeId.ToString

                _FNHSysDeptId = R!FNHSysDeptId.ToString
                _FNHSysDivisonId = R!FNHSysDivisonId.ToString
                _FNHSysSectId = R!FNHSysSectId.ToString
                _FNHSysUnitSectId = R!FNHSysUnitSectId.ToString
                _FNHSysPositId = R!FNHSysPositId.ToString


                _Qry = " DELETE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll WHERE  FTPayYear='" & _FTPayYear & "' AND FTPayTerm='" & _FTPayTerm & "' AND FNHSysEmpID=" & Val(_FNHSysEmpID)
                _Qry &= vbCrLf & "  "
                _Qry &= vbCrLf & " DELETE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin WHERE  FTPayYear='" & _FTPayYear & "' AND FTPayTerm='" & _FTPayTerm & "' AND FNHSysEmpID=" & Val(_FNHSysEmpID) & " AND FTFinCode IN ('112','113') "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll (FTInsUser, FTInsDate, FTInsTime , FTPayDate "
                _Qry &= vbCrLf & " , FTPayYear, FTPayTerm, FNHSysEmpID "
                _Qry &= vbCrLf & " , FTEmpIdNo, FNHSysEmpTypeId "
                _Qry &= vbCrLf & " , FNHSysDeptId, FNHSysDivisonId, FNHSysSectId "
                _Qry &= vbCrLf & " , FNHSysUnitSectId, FNHSysPositId) "
                _Qry &= vbCrLf & " 	SELECT '" & HI.ST.UserInfo.UserName & "', CONVERT(varchar(10),GetDate(),111) ,CONVERT(varchar(8),GetDate(),114) , CONVERT(varchar(10),GetDate(),111)"
                _Qry &= vbCrLf & " , '" & _FTPayYear & "','" & _FTPayTerm & "'," & Val(_FNHSysEmpID)
                _Qry &= vbCrLf & " , " & _FTEmpIdNo & "," & _FNHSysEmpTypeId
                _Qry &= vbCrLf & " , " & Val(_FNHSysDeptId) & "," & Val(_FNHSysDivisonId) & "," & Val(_FNHSysSectId)
                _Qry &= vbCrLf & " , " & Val(_FNHSysUnitSectId) & "," & Val(_FNHSysPositId)
                _Qry &= vbCrLf & " "

                If Val(_F112) > 0 Then
                    _Qry &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTInsUser, FTInsDate, FTInsTime  "
                    _Qry &= vbCrLf & " , FTPayYear, FTPayTerm, FNHSysEmpID"
                    _Qry &= vbCrLf & " , FTFinCode , FCFinAmt, FCTotalFinAmt) "
                    _Qry &= vbCrLf & " 	SELECT '" & HI.ST.UserInfo.UserName & "',CONVERT(varchar(10),GetDate(),111),CONVERT(varchar(8),GetDate(),114)"
                    _Qry &= vbCrLf & " , '" & _FTPayYear & "','" & _FTPayTerm & "'," & Val(_FNHSysEmpID)
                    _Qry &= vbCrLf & " ,'112'," & _F112 & "," & _F112
                    _Qry &= vbCrLf & " "
                End If

                If Val(_F113) > 0 Then
                    _Qry &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin (FTInsUser, FTInsDate, FTInsTime  "
                    _Qry &= vbCrLf & " , FTPayYear, FTPayTerm, FNHSysEmpID"
                    _Qry &= vbCrLf & " , FTFinCode , FCFinAmt, FCTotalFinAmt) "
                    _Qry &= vbCrLf & " 	SELECT '" & HI.ST.UserInfo.UserName & "',CONVERT(varchar(10),GetDate(),111),CONVERT(varchar(8),GetDate(),114)"
                    _Qry &= vbCrLf & " , '" & _FTPayYear & "','" & _FTPayTerm & "'," & Val(_FNHSysEmpID)
                    _Qry &= vbCrLf & " ,'113'," & _F113 & "," & _F113
                    _Qry &= vbCrLf & " "
                End If

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            Next

            '-update วันที่จ่ายเงิน 

            Dim _date As Integer = 0
            Dim _Str_date As String = ""
            Dim _Str__FTPayTerm As String = ""
            Dim _Month As String = ""
            _date = System.DateTime.DaysInMonth(_FTPayYear, _FTPayTerm)
            _Month = Format(Integer.Parse(_FTPayTerm), "00")
            _Str_date = Format(Integer.Parse(_date), "00")


            Dim _YearSend As String = Format(IIf(Integer.Parse(_FTPayYear) < 2500, Integer.Parse(_FTPayYear) + 543, Integer.Parse(_FTPayYear)), "0000")
            Dim DateBf As String = _Str_date & "/" & _Month & "/" & _YearSend


            _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRollStudentLoan SET FDDateCal = '" & DateBf & "'"
            _Qry &= vbCrLf & "  WHERE FTPayYear='" & FTPayYear.Text & "' AND FNMonth = '" & _FNMonth & "' "
            _Qry &= vbCrLf & "  AND  " & _strCmpID
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
            '' bind data export 
            BindDataExport()

            _Spls.Close()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmExportCSV_Click(sender As Object, e As EventArgs) Handles ocmExportCSV.Click
        Try

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

                StateExpoort = exportCSV(FileNameRef)

                _Spls.Close()

                If StateExpoort Then
                    HI.MG.ShowMsg.mInfo("Export Data Complete !!!", 1614413254, Me.Text, , MessageBoxIcon.Warning)
                End If

                'Dim newFilePath As String

                'newFilePath = FileNameRef.Replace(".xlsx", ".csv")

                'System.IO.File.Move(FileNameRef, newFilePath)

                'Dim sr As New StreamReader(newFilePath)
                'Dim sw As New StreamWriter(newFilePath.Replace(".csv", "11.csv"), False, Encoding.UTF8)

                'sw.WriteLine(sr.ReadToEnd)

                'sw.Close()
                'sr.Close()
                'Process.Start(newFilePath.Replace(".csv", "11.csv"))

                Process.Start(FileNameRef)

            End If



        Catch ex As Exception

        End Try
    End Sub
    Private Function exportCSV(ByRef FileNameRef As String) As Boolean
        Dim _CSV As String
        Dim _strCmpID As String
        Dim _Qry As String
        Dim _strAlert As String = ""

        'If (Val(HI.ST.SysInfo.CmpID) = "1306010001") Then
        '    _strCmpID = " L.FNHSysCmpId IN ('1306010001','1309150001','1309280001','1408290001') "
        'Else
        '    _strCmpID = " L.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
        'End If

        _strCmpID = " L.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)

        Dim _FTPayYear As String, _FTPayTerm As String
        _FTPayYear = FTPayYear.Text
        _FTPayTerm = HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)




        _Qry = " SELECT   L.FNSeq "
        _Qry &= vbCrLf & "  , CAST(L.FTEmpIdNo AS varchar)  AS FTEmpIdNo, FTEmpNameSurname "
        _Qry &= vbCrLf & " , ISNULL(F112,0) AS 'FCFinAmt112' , ISNULL(F113,0) AS 'FCFinAmt113' , ISNULL(F112,0) + ISNULL(F113,0) AS 'FCTotalAmt'"
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & "  , FTCmpTaxNo, FTCmpCodeNo, FNMonthCal, FTYearCal, FDDateCal "
        _Qry &= vbCrLf & " , FCCheckNotPay "
        _Qry &= vbCrLf & " ,  CASE WHEN ISNULL(FNReason,0) = '0' THEN CAST('' as nvarchar(2)) ELSE  CAST(FNReason as nvarchar(2)) END AS  FNReason "


        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRollStudentLoan L  "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE FNHSysEmpID=L.FNHSysEmpID) AS M "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename WHERE  M.FNHSysPreNameId = FNHSysPreNameId ) P  "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE  M.FNHSysEmpTypeId = FNHSysEmpTypeId ) ET "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment WHERE  M.FNHSysDeptId = FNHSysDeptId ) Dept "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision WHERE M.FNHSysDivisonId = FNHSysDivisonId ) DI "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect WHERE M.FNHSysSectId = FNHSysSectId ) ST  "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect WHERE  M.FNHSysUnitSectId = FNHSysUnitSectId ) US "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition WHERE  M.FNHSysPositId = FNHSysPositId ) OrgPosit "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPositionGrp WHERE  OrgPosit.FNHSysPositGrpId = FNHSysPositGrpId ) G "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus WHERE  M.FNEmpStatus = FNListIndex ) ES "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " LEFT JOIN ( "
        _Qry &= vbCrLf & " SELECT piv.FTPayYear , piv.FNMonth ,piv.FNHSysEmpID ,SUM(ISNULL([112],0)) as 'F112',SUM(ISNULL([113],0)) AS 'F113' , FTEmpIdNo, FTEmpCode, FTEmpNameTH, FTEmpSurnameTH,  FNHSysCmpId "
        _Qry &= vbCrLf & "  FROM (  "
        _Qry &= vbCrLf & "  SELECT  P.FTPayYear, P.FTPayTerm,P.FNHSysEmpID,FTFinCode,  FCTotalFinAmt ,D.FNMonth "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin P "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE P.FNHSysEmpID= FNHSysEmpID ORDER BY FDDateStart DESC, FNEmpStatus ) E "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT  AS D ON P.FTPayTerm = D.FTPayTerm AND P.FTPayYear = D.FTPayYear AND E.FNHSysEmpTypeId = D.FNHSysEmpTypeId"
        _Qry &= vbCrLf & "  WHERE P.FTPayYear ='" & _FTPayYear & "' AND  D.FNMonth = '" & _FTPayTerm & "' "
        _Qry &= vbCrLf & "   AND FTFinCode IN ('112','113') "
        _Qry &= vbCrLf & "  ) D  "
        _Qry &= vbCrLf & "  PIVOT  "
        _Qry &= vbCrLf & "  ( MAX(FCTotalFinAmt)  "
        _Qry &= vbCrLf & "  For FTFinCode   in ([112],[113])   "
        _Qry &= vbCrLf & "  ) piv "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE piv.FNHSysEmpID= FNHSysEmpID ORDER BY FDDateStart DESC, FNEmpStatus ) E "
        _Qry &= vbCrLf & " WHERE piv.FTPayYear = '" & _FTPayYear & "'  AND  piv.FNMonth = '" & _FTPayTerm & "' "
        _Qry &= vbCrLf & " GROUP BY piv.FTPayYear, piv.FNHSysEmpID, piv.FNMonth, FTEmpIdNo, FTEmpCode, FTEmpNameTH, FTEmpSurnameTH, FNHSysCmpId  ) AS PF ON L.FNHSysEmpID=PF.FNHSysEmpID AND L.FTPayYear=PF.FTPayYear AND  L.FNMonth=PF.FNMonth "
        _Qry &= vbCrLf & " "

        _Qry &= vbCrLf & "  WHERE L.FTPayYear='" & FTPayYear.Text & "'AND L.FNMonth = '" & _FTPayTerm & "' "
        _Qry &= vbCrLf & "  AND  " & _strCmpID
        _Qry &= vbCrLf & " ORDER BY L.FNSeq "



        Dim _dtZCSV As System.Data.DataTable
        _dtZCSV = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


        For Each dr As DataRow In _dtZCSV.Select("FCCheckNotPay='Y' AND FNReason = 0")
            _strAlert &= vbCrLf & "NO:" & dr!FNSeq & " Employee ID : " & dr!FTEmpIdNo & " Name :" & dr!FTEmpNameSurname
        Next


        Dim StartRow As Integer = 1
        Dim RowIndx As Integer = 0
        RowIndx = 0
        If _dtZCSV.Rows.Count > 0 Then


            With opshet.ActiveWorksheet


                For Each r As DataRow In _dtZCSV.Rows
                    .Rows(StartRow + RowIndx).Insert()
                    .Rows(StartRow + RowIndx).CopyFrom(.Rows(StartRow + RowIndx + 1), DevExpress.Spreadsheet.PasteSpecial.All)

                    .Rows(StartRow + RowIndx).Item(0).Value = r!FNSeq.ToString
                    .Rows(StartRow + RowIndx).Item(1).Value = r!FTEmpIdNo.ToString
                    .Rows(StartRow + RowIndx).Item(2).Value = r!FTEmpNameSurname.ToString
                    .Rows(StartRow + RowIndx).Item(3).Value = r!FCFinAmt112.ToString
                    .Rows(StartRow + RowIndx).Item(4).Value = r!FCFinAmt113.ToString
                    .Rows(StartRow + RowIndx).Item(5).Value = r!FCTotalAmt.ToString

                    .Rows(StartRow + RowIndx).Item(6).Value = r!FTCmpTaxNo.ToString
                    .Rows(StartRow + RowIndx).Item(7).Value = r!FTCmpCodeNo.ToString
                    .Rows(StartRow + RowIndx).Item(8).Value = r!FNMonthCal.ToString
                    .Rows(StartRow + RowIndx).Item(9).Value = r!FTYearCal.ToString

                    .Rows(StartRow + RowIndx).Item(10).Value = r!FDDateCal.ToString
                    .Rows(StartRow + RowIndx).Item(11).Value = r!FCCheckNotPay.ToString
                    .Rows(StartRow + RowIndx).Item(12).Value = r!FNReason.ToString
                    RowIndx = RowIndx + 1

                Next



            End With

            StartRow = StartRow + RowIndx
        End If


        Dim strDate = ""
        strDate = Format(Now, "yyyy")
        Dim _Month As String = ""
        _Month = Format(Integer.Parse(_FTPayTerm), "00")


        'Dim Op As New System.Windows.Forms.SaveFileDialog
        'Op.Filter = "*.CSV|*.csv"
        'Op.FileName = "RD_DEBT_" & _Month & strDate & "_Excel.csv"

        Dim FileName As String = _DefailtPath & "\" & "RD_DEBT_" & _Month & strDate & "_Excel.csv"

        Dim FileNameExcel As String = _DefailtPath & "\" & "RD_DEBT_" & _Month & strDate & "_Excel.xlsx"



        FileNameRef = FileNameExcel
        ' opshet.SaveDocumentAs(FileName)
        'opshet.ActiveWorksheet.Options.Export.Csv.ValueSeparator = ","
        opshet.Options.Export.Csv.Encoding = Encoding.UTF8
        opshet.Options.Export.Csv.UseCellNumberFormat = True
        '  opshet.Options.Export.Csv = CsvDocumentExporterOptions.
        'CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString()
        opshet.SaveDocument(FileName)

        opshet.SaveDocument(FileNameRef)


        'Dim workbook As New Workbook() 'read from FileData-Property  
        'Dim worksheet As Worksheet = opshet.ActiveWorkshee

        'workbook.Options.Export.Csv.Culture = New Globalization.CultureInfo(name:="en-US")
        'workbook.Options.Export.Csv.ValueSeparator = ","
        'Dim csvStream As IO.Stream = New IO.MemoryStream
        'workbook.SaveDocument(csvStream, DocumentFormat.Csv)

        ' Process.Start(FileName)



    End Function

    Private Sub ReposFCCheckNotPay_CheckedChanged(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFCCheckNotPay.EditValueChanging
        Try
            'If Val(e.NewValue.ToString) < 0 Then
            '    e.Cancel = True
            'Else
            '    e.Cancel = False

            Dim _Qry As String
            Dim _NewValue As String

            _NewValue = e.NewValue.ToString()
            With Me.ogv_export

                Dim _FNHSysEmpID As Integer = Val("" & .GetFocusedRowCellValue("FNHSysEmpID").ToString)
                Dim _FTPayYear As String = "" & .GetFocusedRowCellValue("FTPayYear").ToString
                Dim _FNMonth As String = "" & .GetFocusedRowCellValue("FNMonth").ToString
                Dim _FCCheckNotPay As String = "" & .GetFocusedRowCellValue("FCCheckNotPay").ToString


                'Dim ReposFCCheckNotPay As String = ""
                'If .GetFocusedRowCellValue("ReposFCCheckNotPay").ToString <> "" Then
                '    ReposFCCheckNotPay = "Y"
                'End If

                Dim _Qry_And As String = ""

                If _NewValue <> "Y" Then
                    .SetFocusedRowCellValue("FNReason_ex", "-1")
                    _Qry_And = ", FNReason='' "
                End If


                _Qry = " UPDATE HITECH_HR.dbo.THRTPayRollStudentLoan SET FCCheckNotPay='" & _NewValue & "' " & _Qry_And
                _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(_FNHSysEmpID) & " AND  FTPayYear='" & _FTPayYear & "' AND  FNMonth='" & _FNMonth & "' "
                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = " INSERT INTO HITECH_HR.dbo.THRTPayRollStudentLoan  (FNHSysEmpID , FTPayYear , FNMonth, FCCheckNotPay) "
                    _Qry &= vbCrLf & "SELECT  " & Val(_FNHSysEmpID) & ",'" & _FTPayYear & "','" & _FNMonth & "',  " & _NewValue & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                End If


            End With

            'End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub RepoFNReason_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepoFNReason.EditValueChanging
        Try


            Dim _Qry As String
            Dim _NewValue As String
            Dim _FNReason As Integer
            _NewValue = e.NewValue.ToString()

            _FNReason = HI.TL.CboList.GetIndexByText("FNReasonStudentLoan", _NewValue)
            With Me.ogv_export

                Dim _FNHSysEmpID As Integer = Val("" & .GetFocusedRowCellValue("FNHSysEmpID").ToString)

                Dim _FTPayYear As String = "" & .GetFocusedRowCellValue("FTPayYear").ToString
                Dim _FNMonth As String = "" & .GetFocusedRowCellValue("FNMonth").ToString
                Dim _FCCheckNotPay_ex As String = "" & .GetFocusedRowCellValue("FCCheckNotPay").ToString


                Dim _Qry_And As String = ""
                If Val(_FNReason) > 0 Then
                    .SetFocusedRowCellValue("FCCheckNotPay", "Y")
                    _Qry_And = ", FCCheckNotPay = 'Y' "
                Else
                    .SetFocusedRowCellValue("FCCheckNotPay", "")
                    _Qry_And = ", FCCheckNotPay = '' "
                End If

                _Qry = " UPDATE HITECH_HR.dbo.THRTPayRollStudentLoan SET FNReason='" & _FNReason & "' " & _Qry_And
                _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(_FNHSysEmpID) & " AND  FTPayYear='" & _FTPayYear & "' AND  FNMonth='" & _FNMonth & "' "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = " INSERT INTO HITECH_HR.dbo.THRTPayRollStudentLoan  (FNHSysEmpID , FTPayYear , FNMonth, FNReason) "
                    _Qry &= vbCrLf & "SELECT  " & Val(_FNHSysEmpID) & ",'" & _FTPayYear & "','" & _FNMonth & "',  " & _FNReason & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                End If

            End With

        Catch ex As Exception

        End Try


    End Sub


    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle

        With ogv


            Try
                If Val(.GetRowCellValue(e.RowHandle, "FCReqFinAmt112")) <> Val(.GetRowCellValue(e.RowHandle, "FCFinAmt112")) Or Val(.GetRowCellValue(e.RowHandle, "FCReqFinAmt112")) <> Val(.GetRowCellValue(e.RowHandle, "FCFinAmt112")) Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                End If
            Catch ex As Exception
            End Try

        End With

    End Sub


    Private Sub ogv_export_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv_export.RowCellStyle

        With ogv_export


            Try
                If Val(.GetRowCellValue(e.RowHandle, "FCReqFinAmt112")) <> Val(.GetRowCellValue(e.RowHandle, "FCFinAmt112")) Or Val(.GetRowCellValue(e.RowHandle, "FCReqFinAmt113")) <> Val(.GetRowCellValue(e.RowHandle, "FCFinAmt113")) Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                End If
            Catch ex As Exception
            End Try

        End With

    End Sub

End Class
Imports System.Windows.Forms
Imports System
Imports System.Data
Imports CrystalDecisions.Data.AdoDotNetInterop
Imports CrystalDecisions.Data
Imports CrystalDecisions
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports DevExpress.XtraEditors
Imports System.Net.Mail
Imports Microsoft
Imports CrystalDecisions.CrystalReports.Engine.ReportDocument

Public Class Report

    Public Enum ExFile As Integer
        None = 0
        PDF = 1
        Excel = 2

    End Enum

    Sub New()
        Me.PathReport = Application.StartupPath & "\Reports\"
        Me.CreateTableParameter()
    End Sub

    Private _ExportFile As ExFile = ExFile.None
    Public Property ExportFile() As ExFile
        Get
            Return _ExportFile
        End Get
        Set(value As ExFile)
            _ExportFile = value
        End Set
    End Property

    Private _ExportName As String = ""
    Public Property ExportName() As String
        Get
            Return _ExportName
        End Get
        Set(value As String)
            _ExportName = value
        End Set
    End Property

    Private _PathExport As String = ""
    Public Property PathExport() As String
        Get
            Return _PathExport
        End Get
        Set(value As String)
            _PathExport = value
        End Set
    End Property


    Private _FormTitle As String = ""
    Public Property FormTitle() As String
        Get
            Return _FormTitle
        End Get
        Set(value As String)
            _FormTitle = value
        End Set
    End Property

    Private _ReportTitle As String = ""
    Public Property ReportTitle() As String
        Get
            Return _ReportTitle
        End Get
        Set(value As String)
            _ReportTitle = value
        End Set
    End Property

    Private _PathReportTitle As String = Application.StartupPath & "\Reports\"
    Public Property PathReport() As String
        Get
            Return _PathReportTitle
        End Get
        Set(value As String)
            _PathReportTitle = value
        End Set
    End Property

    Private _ReportName As String = ""
    Public Property ReportName() As String
        Get
            Return _ReportName
        End Get
        Set(value As String)
            _ReportName = value
        End Set
    End Property

    Private _ReportFolderName As String = ""
    Public Property ReportFolderName() As String
        Get
            Return _ReportFolderName
        End Get
        Set(value As String)
            _ReportFolderName = value
        End Set
    End Property

    Private _Formular As String = ""
    Public Property Formular() As String
        Get
            Return _Formular
        End Get
        Set(value As String)
            _Formular = value
        End Set
    End Property

    Private _ShowPrint As Boolean = True
    Public Property ShowPrint() As Boolean
        Get
            Return _ShowPrint
        End Get
        Set(value As Boolean)
            _ShowPrint = value
        End Set
    End Property

    Private _ShowExport As Boolean = True
    Public Property ShowExport() As Boolean
        Get
            Return _ShowExport
        End Get
        Set(value As Boolean)
            _ShowExport = value
        End Set
    End Property

    Private _ExportFileSuccessName As String = ""
    Public Property ExportFileSuccessName() As String
        Get
            Return _ExportFileSuccessName
        End Get
        Set(value As String)
            _ExportFileSuccessName = value
        End Set
    End Property

    Private _Print As Boolean = False
    Public Property Print() As Boolean
        Get
            Return _Print
        End Get
        Set(value As Boolean)
            _Print = value
        End Set
    End Property

    Private _DTParameter As New DataTable
    Private Sub CreateTableParameter()
        _DTParameter = New DataTable
        _DTParameter.Columns.Add("ParameterName", GetType(String))
        _DTParameter.Columns.Add("ParameterValues", GetType(String))
    End Sub

    Public Sub AddParameter(ParameterName As String, ValuesName As String)
        If _DTParameter Is Nothing Then
            Call CreateTableParameter()
        End If
        With _DTParameter
            If .Select("ParameterName='" & HI.UL.ULF.rpQuoted(ParameterName) & "'").Length <= 0 Then
                .Rows.Add(ParameterName)
            End If

            For Each Row In .Select("ParameterName='" & HI.UL.ULF.rpQuoted(ParameterName) & "'")
                Row!ParameterValues = ValuesName
            Next
        End With
    End Sub

    Public Sub Preview()
        Try
            If IO.File.Exists(Me.PathReport & Me.ReportFolderName & Me.ReportName) = False Then

                If HI.ST.SysInfo.Admin Then
                    HI.MG.ShowMsg.mInfo("Can't Find Report", 1403160001, Me.FormTitle, Me.PathReport & Me.ReportFolderName & Me.ReportName)
                End If

                Exit Sub
            End If
        Catch ex As Exception
        End Try


        Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")

        Try

            If HI.Conn.DB.APIReportServiceLink <> "" And ExportFile = ExFile.None Then
                Dim ERPAPI As New HI.Conn.API()
                Dim rptreturn As String() = Nothing
                Dim rptbyte As Byte() = Nothing

                rptreturn = ERPAPI.GetReportPDF(HI.ST.SysInfo.CmpID.ToString, HI.ST.UserInfo.UserName, Me.ReportFolderName.Replace("\", "").Replace("/", ""), Me.ReportName, Me.Formular, HI.ST.Lang.Language)

                If rptreturn.Length = 2 Then
                    If rptreturn(0) = "" Then

                        Try
                            rptbyte = Convert.FromBase64String(rptreturn(1))

                            If rptbyte IsNot Nothing Then

                                With New wReport

                                    .PDFShowViewer.LoadDocument(New System.IO.MemoryStream(rptbyte))
                                    .WindowState = FormWindowState.Maximized

                                    If (HI.ST.SysInfo.Admin) Then
                                        .Text = Me.FormTitle & " ( " & Me.ReportName & " ) "
                                    Else
                                        .Text = Me.FormTitle
                                    End If

                                    _spls.Close()

                                    .Show()

                                End With

                            Else
                                _spls.Close()
                                HI.MG.ShowMsg.mInfo("Can't Preview Report", 1403168881, Me.FormTitle)
                            End If

                        Catch ex As Exception
                            _spls.Close()
                            HI.MG.ShowMsg.mInfo("Can't Preview Report", 1403168881, Me.FormTitle)
                        End Try


                    Else
                        _spls.Close()
                        HI.MG.ShowMsg.mInfo(rptreturn(0), 1403169881, Me.FormTitle)
                    End If
                Else
                    _spls.Close()
                    HI.MG.ShowMsg.mInfo("Can't Preview Report", 1403168881, Me.FormTitle)
                End If



            Else
                Dim report As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                Dim i As Integer
                Dim Database As String
                Dim _ServerName, _UID, _PWS As String

                If IO.File.Exists(Me.PathReport & Me.ReportFolderName & Me.ReportName) Then
                    With report
                        .Load(Me.PathReport & Me.ReportFolderName & Me.ReportName)

                        Dim myTables As Tables = .Database.Tables
                        For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                            Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                            Database = myTableLogonInfo.ConnectionInfo.DatabaseName
                            If (HI.Conn.DB.UsedDB(myTableLogonInfo.ConnectionInfo.DatabaseName)) Then
                                _ServerName = HI.Conn.DB.SerVerName
                                _UID = HI.Conn.DB.UIDName
                                _PWS = HI.Conn.DB.PWDName
                            Else
                                _ServerName = ""
                                _UID = ""
                                _PWS = ""
                            End If

                            Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
                            myConnectionInfo.DatabaseName = Database
                            myConnectionInfo.UserID = _UID
                            myConnectionInfo.Password = _PWS
                            myConnectionInfo.ServerName = _ServerName

                            myTableLogonInfo.ConnectionInfo = myConnectionInfo
                            myTable.ApplyLogOnInfo(myTableLogonInfo)

                        Next
                        Dim _Str As String = ""
                        Dim _Default As String = ""
                        Dim _FTParameterName As String = ""
                        _Str = "SELECT TOP 1  FTParameterName "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysReportLanguage WITH(NOLOCK)"
                        _Str &= vbCrLf & " WHERE FTReportName='" & HI.UL.ULF.rpQuoted(Me.ReportName) & "'  "

                        If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_LANG, "") = "" Then
                            If .ParameterFields.Count > 0 Then
                                For i = 0 To .ParameterFields.Count - 1
                                    If Not (.ParameterFields(i).Name.ToString.ToUpper Like "*PM-*") Then

                                        _FTParameterName = report.ParameterFields(i).Name.ToString
                                        Dim _PMValue As New CrystalDecisions.Shared.ParameterDiscreteValue
                                        _Default = ""
                                        Try
                                            _PMValue = report.ParameterFields(i).DefaultValues(0)

                                            _Default = _PMValue.Value.ToString()
                                        Catch ex As Exception
                                            _Default = ""
                                        End Try

                                        If _Default <> "" Then
                                            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysReportLanguage"
                                            _Str &= vbCrLf & " (FTReportName, FTParameterName, FTLangEN, FTLangTH, FTLangVT, FTLangKM) "
                                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(Me.ReportName) & "'"
                                            _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTParameterName) & "'"
                                            _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Default) & "'"
                                            _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Default) & "'"
                                            _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Default) & "'"
                                            _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Default) & "'"

                                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_LANG)

                                        End If
                                    End If
                                Next
                            End If
                        End If

                        If _DTParameter Is Nothing Then
                            Me.CreateTableParameter()
                        End If


                        Dim _DtParm As DataTable

                        _Str = "SELECT  FTParameterName , "

                        Select Case HI.ST.Lang.Language
                            Case ST.Lang.eLang.TH
                                _Str &= vbCrLf & " FTLangTH AS FTLang "
                            Case ST.Lang.eLang.EN
                                _Str &= vbCrLf & " FTLangEN AS FTLang "
                            Case ST.Lang.eLang.KM
                                _Str &= vbCrLf & " FTLangKM AS FTLang "
                            Case ST.Lang.eLang.VT
                                _Str &= vbCrLf & " FTLangVT AS FTLang "
                            Case ST.Lang.eLang.BM
                                _Str &= vbCrLf & " FTLangBM AS FTLang "
                            Case ST.Lang.eLang.LAO
                                _Str &= vbCrLf & " FTLangLAO AS FTLang "
                            Case ST.Lang.eLang.CH
                                _Str &= vbCrLf & " FTLangCH AS FTLang "
                            Case Else
                                _Str &= vbCrLf & " FTLangEN AS FTLang "
                        End Select

                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysReportLanguage WITH(NOLOCK)"
                        _Str &= vbCrLf & " WHERE FTReportName='" & HI.UL.ULF.rpQuoted(Me.ReportName) & "'  "

                        _DtParm = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

                        For Each R As DataRow In _DtParm.Rows
                            Me.AddParameter(R!FTParameterName.ToString, R!FTLang.ToString)
                        Next

                        _DtParm.Dispose()

                        Me.AddParameter("FNLang", HI.ST.Lang.Language)
                        Me.AddParameter("FNHSysCmpID", HI.ST.SysInfo.CmpID)
                        Me.AddParameter("FTUserLogIn", HI.ST.UserInfo.UserName)
                        Me.AddParameter("FTPreviewDate", HI.UL.ULDate.ConvertEnDB(HI.ST.UserInfo.LogINDate))

                        If .ParameterFields.Count > 0 Then
                            For i = 0 To .ParameterFields.Count - 1

                                If _DTParameter.Select("ParameterName='" & HI.UL.ULF.rpQuoted(.ParameterFields(i).Name.ToString) & "'").Length > 0 Then

                                    For Each Row In _DTParameter.Select("ParameterName='" & HI.UL.ULF.rpQuoted(.ParameterFields(i).Name.ToString) & "'")
                                        .SetParameterValue(.ParameterFields(i).Name.ToString, Row!ParameterValues.ToString)
                                    Next

                                Else

                                    If Not (.ParameterFields(i).Name.ToString.ToUpper Like "*PM-*") Then
                                        .SetParameterValue(.ParameterFields(i).Name.ToString, "")
                                    End If

                                End If
                            Next
                        End If

                        Try
                            .SummaryInfo.ReportTitle = Me.ReportTitle
                        Catch ex As Exception
                        End Try

                    End With

                    Select Case ExportFile
                        Case ExFile.None

                            Dim CRV As New CrystalDecisions.Windows.Forms.CrystalReportViewer

                            With CRV
                                .ShowRefreshButton = False
                                .ReportSource = report

                                If Me.Formular <> "" Then

                                    If report.RecordSelectionFormula <> "" Then
                                        .SelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                    Else
                                        .SelectionFormula = Me.Formular
                                    End If

                                End If

                                .ShowPrintButton = Me.ShowPrint
                                .ShowExportButton = Me.ShowExport

                                .Dock = DockStyle.Fill

                                For Each ts As ToolStrip In .Controls.OfType(Of ToolStrip)()
                                    For Each tsb As ToolStripButton In ts.Items.OfType(Of ToolStripButton)()
                                        'hacky but should work. you can probably figure out a better method

                                        Select Case True
                                            Case tsb.ToolTipText.ToLower().Contains("print")
                                                'Adding a handler for our propose
                                                AddHandler tsb.Click, AddressOf printButton_Click
                                            Case tsb.ToolTipText.ToLower().Contains("export")
                                                'Adding a handler for our propose
                                                AddHandler tsb.Click, AddressOf exportButton_Click
                                        End Select
                                    Next
                                Next

                            End With

                            If Me.Print Then
                                CRV.PrintReport()
                            Else

                                Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
                                Dim CrDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions()
                                CrExportOptions = report.ExportOptions
                                CrDiskFileDestinationOptions.DiskFileName = Me.ReportName

                                With CrExportOptions
                                    .ExportDestinationType = ExportDestinationType.DiskFile
                                    .DestinationOptions = CrDiskFileDestinationOptions
                                End With

                                Dim RPT As New DevExpress.XtraEditors.XtraForm
                                With RPT
                                    .Controls.Add(CRV)
                                    .WindowState = FormWindowState.Maximized

                                    If (HI.ST.SysInfo.Admin) Then
                                        .Text = Me.FormTitle & " ( " & Me.ReportName & " ) "
                                    Else
                                        .Text = Me.FormTitle
                                    End If

                                    _spls.Close()

                                    .Show()
                                End With
                            End If
                        Case ExFile.PDF

                            If (Me.PathExport) <> "" Then
                                Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
                                Dim CrDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions()
                                Dim CrFormatTypeOptions As New CrystalDecisions.Shared.PdfRtfWordFormatOptions()

                                Dim Str_Doc_Name As String = Me.PathExport & "\" & Me.ExportName & ".pdf"
                                If Dir(Str_Doc_Name) <> "" Then Kill(Str_Doc_Name)

                                CrDiskFileDestinationOptions.DiskFileName = Str_Doc_Name
                                CrExportOptions = report.ExportOptions

                                With CrExportOptions
                                    .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                                    .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                                    .DestinationOptions = CrDiskFileDestinationOptions
                                    .FormatOptions = CrFormatTypeOptions
                                End With

                                If Me.Formular <> "" Then
                                    If report.RecordSelectionFormula <> "" Then
                                        report.RecordSelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                    Else
                                        report.RecordSelectionFormula = Me.Formular
                                    End If
                                End If

                                report.Export()

                                _spls.Close()

                                Me.ExportFileSuccessName = Me.ExportName & ".pdf"
                            Else

                                _spls.Close()

                            End If

                        Case ExFile.Excel

                            If (Me.PathExport) <> "" Then
                                Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
                                Dim CrDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions()
                                Dim CrFormatTypeOptions As New CrystalDecisions.Shared.ExcelFormatOptions

                                Dim Str_Doc_Name As String = Me.PathExport & "\" & Me.ExportName & ".xls"
                                If Dir(Str_Doc_Name) <> "" Then Kill(Str_Doc_Name)

                                CrDiskFileDestinationOptions.DiskFileName = Str_Doc_Name
                                CrExportOptions = report.ExportOptions

                                With CrExportOptions
                                    .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                                    .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                                    .DestinationOptions = CrDiskFileDestinationOptions
                                    .FormatOptions = CrFormatTypeOptions
                                End With

                                If Me.Formular <> "" Then
                                    If report.RecordSelectionFormula <> "" Then
                                        report.RecordSelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                    Else
                                        report.RecordSelectionFormula = Me.Formular
                                    End If
                                End If

                                report.Export()

                                _spls.Close()

                                Me.ExportFileSuccessName = Me.ExportName & ".xls"
                            Else

                                _spls.Close()

                            End If

                        Case Else

                            Dim CRV As New CrystalDecisions.Windows.Forms.CrystalReportViewer

                            With CRV
                                .ShowRefreshButton = False
                                .ReportSource = report

                                If Me.Formular <> "" Then

                                    If report.RecordSelectionFormula <> "" Then
                                        .SelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                    Else
                                        .SelectionFormula = Me.Formular
                                    End If
                                End If

                                .ShowPrintButton = Me.ShowPrint
                                .ShowExportButton = Me.ShowExport

                                .Dock = DockStyle.Fill

                                For Each ts As ToolStrip In .Controls.OfType(Of ToolStrip)()
                                    For Each tsb As ToolStripButton In ts.Items.OfType(Of ToolStripButton)()
                                        'hacky but should work. you can probably figure out a better method

                                        Select Case True
                                            Case tsb.ToolTipText.ToLower().Contains("print")
                                                'Adding a handler for our propose
                                                AddHandler tsb.Click, AddressOf printButton_Click
                                            Case tsb.ToolTipText.ToLower().Contains("export")
                                                'Adding a handler for our propose
                                                AddHandler tsb.Click, AddressOf exportButton_Click
                                        End Select
                                    Next
                                Next

                            End With

                            If Me.Print Then

                                _spls.Close()

                                CRV.PrintReport()
                            Else
                                Dim RPT As New DevExpress.XtraEditors.XtraForm
                                With RPT
                                    .Controls.Add(CRV)
                                    .WindowState = FormWindowState.Maximized


                                    If (HI.ST.SysInfo.Admin) Then
                                        .Text = Me.FormTitle & " ( " & Me.ReportName & " ) "
                                    Else
                                        .Text = Me.FormTitle
                                    End If

                                    _spls.Close()

                                    .Show()
                                End With
                            End If

                    End Select
                Else

                    _spls.Close()
                    If HI.ST.SysInfo.Admin Then
                        HI.MG.ShowMsg.mInfo("Can't Find Report", 1403160001, Me.FormTitle, (Me.PathReport & Me.ReportFolderName & Me.ReportName), MessageBoxIcon.Warning)
                    Else
                        HI.MG.ShowMsg.mInfo("Can't Find Report", 1403160001, Me.FormTitle)
                    End If

                End If
            End If


        Catch ex As Exception

            _spls.Close()

            'MsgBox(ex.Message & vbCrLf & (Me.PathReport & Me.ReportFolderName & Me.ReportName))
        End Try

    End Sub

    Public Sub PrevieNoSplash(Optional LangState As Integer = -1)

        Try
            Dim report As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            Dim i As Integer
            Dim Database As String
            Dim _ServerName, _UID, _PWS As String

            If IO.File.Exists(Me.PathReport & Me.ReportFolderName & Me.ReportName) Then
                With report
                    .Load(Me.PathReport & Me.ReportFolderName & Me.ReportName)

                    Dim myTables As Tables = .Database.Tables
                    For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                        Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                        Database = myTableLogonInfo.ConnectionInfo.DatabaseName
                        If (HI.Conn.DB.UsedDB(myTableLogonInfo.ConnectionInfo.DatabaseName)) Then
                            _ServerName = HI.Conn.DB.SerVerName
                            _UID = HI.Conn.DB.UIDName
                            _PWS = HI.Conn.DB.PWDName
                        Else
                            _ServerName = ""
                            _UID = ""
                            _PWS = ""
                        End If

                        Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
                        myConnectionInfo.DatabaseName = Database
                        myConnectionInfo.UserID = _UID
                        myConnectionInfo.Password = _PWS
                        myConnectionInfo.ServerName = _ServerName

                        myTableLogonInfo.ConnectionInfo = myConnectionInfo
                        myTable.ApplyLogOnInfo(myTableLogonInfo)

                    Next


                    If _DTParameter Is Nothing Then
                        Me.CreateTableParameter()
                    End If

                    Dim _Str As String = ""
                    Dim _DtParm As DataTable

                    _Str = "SELECT  FTParameterName , "

                    If LangState = -1 Then
                        Select Case HI.ST.Lang.Language
                            Case ST.Lang.eLang.TH
                                _Str &= vbCrLf & " FTLangTH AS FTLang "
                            Case ST.Lang.eLang.EN
                                _Str &= vbCrLf & " FTLangEN AS FTLang "
                            Case ST.Lang.eLang.KM
                                _Str &= vbCrLf & " FTLangKM AS FTLang "
                            Case ST.Lang.eLang.VT
                                _Str &= vbCrLf & " FTLangVT AS FTLang "
                            Case ST.Lang.eLang.BM
                                _Str &= vbCrLf & " FTLangBM AS FTLang "
                            Case ST.Lang.eLang.LAO
                                _Str &= vbCrLf & " FTLangLAO AS FTLang "
                            Case ST.Lang.eLang.CH
                                _Str &= vbCrLf & " FTLangCH AS FTLang "
                            Case Else
                                _Str &= vbCrLf & " FTLangEN AS FTLang "
                        End Select
                    Else
                        Select Case LangState
                            Case 0
                                _Str &= vbCrLf & " FTLangTH AS FTLang "
                            Case Else
                                _Str &= vbCrLf & " FTLangEN AS FTLang "
                        End Select
                    End If


                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysReportLanguage WITH(NOLOCK)"
                    _Str &= vbCrLf & " WHERE FTReportName='" & HI.UL.ULF.rpQuoted(Me.ReportName) & "'  "

                    _DtParm = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

                    For Each R As DataRow In _DtParm.Rows
                        Me.AddParameter(R!FTParameterName.ToString, R!FTLang.ToString)
                    Next

                    _DtParm.Dispose()

                    If LangState = -1 Then
                        Me.AddParameter("FNLang", HI.ST.Lang.Language)
                    Else
                        Select Case LangState
                            Case 0
                                Me.AddParameter("FNLang", ST.Lang.eLang.TH)
                            Case Else
                                Me.AddParameter("FNLang", ST.Lang.eLang.EN)
                        End Select
                    End If

                    Me.AddParameter("FNHSysCmpID", HI.ST.SysInfo.CmpID)
                    Me.AddParameter("FTUserLogIn", HI.ST.UserInfo.UserName)
                    Me.AddParameter("FTPreviewDate", HI.UL.ULDate.ConvertEnDB(HI.ST.UserInfo.LogINDate))

                    If .ParameterFields.Count > 0 Then
                        For i = 0 To .ParameterFields.Count - 1
                            If _DTParameter.Select("ParameterName='" & HI.UL.ULF.rpQuoted(.ParameterFields(i).Name.ToString) & "'").Length > 0 Then

                                For Each Row In _DTParameter.Select("ParameterName='" & HI.UL.ULF.rpQuoted(.ParameterFields(i).Name.ToString) & "'")
                                    .SetParameterValue(.ParameterFields(i).Name.ToString, Row!ParameterValues.ToString)
                                Next

                            Else

                                If Not (.ParameterFields(i).Name.ToString.ToUpper Like "*PM-*") Then
                                    .SetParameterValue(.ParameterFields(i).Name.ToString, "")
                                End If

                            End If
                        Next
                    End If

                    Try
                        .SummaryInfo.ReportTitle = Me.ReportTitle
                    Catch ex As Exception
                    End Try

                End With


                'Dim templatefont As System.Drawing.Font

                ''scan all report objects in the crystal report _reportDoc
                'For Each x As ReportObject In report.ReportDefinition.ReportObjects
                '    'just change the font family. Keep styling (e.g. bold) same
                '    If x.GetType.Name.Equals("TextObject") Then
                '        templatefont = DirectCast(x, TextObject).Font
                '        DirectCast(x, TextObject).ApplyFont(New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, templatefont.Size, templatefont.Style, templatefont.Unit))
                '    End If

                '    If x.GetType.Name.Equals("FieldObject") Then
                '        templatefont = DirectCast(x, FieldObject).Font
                '        DirectCast(x, FieldObject).ApplyFont(New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, templatefont.Size, templatefont.Style, templatefont.Unit))
                '    End If
                'Next


                Select Case ExportFile
                    Case ExFile.None

                        Dim CRV As New CrystalDecisions.Windows.Forms.CrystalReportViewer

                        With CRV
                            .ShowRefreshButton = False
                            .ReportSource = report

                            If Me.Formular <> "" Then

                                If report.RecordSelectionFormula <> "" Then
                                    .SelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                Else
                                    .SelectionFormula = Me.Formular
                                End If

                            End If

                            .ShowPrintButton = Me.ShowPrint
                            .ShowExportButton = Me.ShowExport

                            .Dock = DockStyle.Fill

                            For Each ts As ToolStrip In .Controls.OfType(Of ToolStrip)()
                                For Each tsb As ToolStripButton In ts.Items.OfType(Of ToolStripButton)()
                                    'hacky but should work. you can probably figure out a better method

                                    Select Case True
                                        Case tsb.ToolTipText.ToLower().Contains("print")
                                            'Adding a handler for our propose
                                            AddHandler tsb.Click, AddressOf printButton_Click
                                        Case tsb.ToolTipText.ToLower().Contains("export")
                                            'Adding a handler for our propose
                                            AddHandler tsb.Click, AddressOf exportButton_Click
                                    End Select
                                Next
                            Next

                        End With

                        If Me.Print Then
                            CRV.PrintReport()
                        Else

                            Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
                            Dim CrDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions()
                            CrExportOptions = report.ExportOptions
                            CrDiskFileDestinationOptions.DiskFileName = Me.ReportName

                            With CrExportOptions
                                .ExportDestinationType = ExportDestinationType.DiskFile
                                .DestinationOptions = CrDiskFileDestinationOptions
                            End With

                            Dim RPT As New DevExpress.XtraEditors.XtraForm
                            With RPT
                                .Controls.Add(CRV)
                                .WindowState = FormWindowState.Maximized

                                If (HI.ST.SysInfo.Admin) Then
                                    .Text = Me.FormTitle & " ( " & Me.ReportName & " ) "
                                Else
                                    .Text = Me.FormTitle
                                End If


                                .Show()
                            End With
                        End If
                    Case ExFile.PDF

                        If (Me.PathExport) <> "" Then
                            Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
                            Dim CrDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions()
                            Dim CrFormatTypeOptions As New CrystalDecisions.Shared.PdfRtfWordFormatOptions()

                            Dim Str_Doc_Name As String = Me.PathExport & "\" & Me.ExportName & ".pdf"
                            If Dir(Str_Doc_Name) <> "" Then Kill(Str_Doc_Name)

                            CrDiskFileDestinationOptions.DiskFileName = Str_Doc_Name
                            CrExportOptions = report.ExportOptions

                            With CrExportOptions
                                .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                                .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                                .DestinationOptions = CrDiskFileDestinationOptions
                                .FormatOptions = CrFormatTypeOptions
                            End With

                            If Me.Formular <> "" Then
                                If report.RecordSelectionFormula <> "" Then
                                    report.RecordSelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                Else
                                    report.RecordSelectionFormula = Me.Formular
                                End If
                            End If

                            report.Export()

                            Me.ExportFileSuccessName = Me.ExportName & ".pdf"
                        Else

                        End If

                    Case ExFile.Excel

                        If (Me.PathExport) <> "" Then
                            Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
                            Dim CrDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions()
                            Dim CrFormatTypeOptions As New CrystalDecisions.Shared.ExcelFormatOptions

                            Dim Str_Doc_Name As String = Me.PathExport & "\" & Me.ExportName & ".xls"
                            If Dir(Str_Doc_Name) <> "" Then Kill(Str_Doc_Name)

                            CrDiskFileDestinationOptions.DiskFileName = Str_Doc_Name
                            CrExportOptions = report.ExportOptions

                            With CrExportOptions
                                .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                                .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                                .DestinationOptions = CrDiskFileDestinationOptions
                                .FormatOptions = CrFormatTypeOptions
                            End With

                            If Me.Formular <> "" Then
                                If report.RecordSelectionFormula <> "" Then
                                    report.RecordSelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                Else
                                    report.RecordSelectionFormula = Me.Formular
                                End If
                            End If

                            report.Export()


                            Me.ExportFileSuccessName = Me.ExportName & ".xls"
                        Else

                        End If

                    Case Else

                        Dim CRV As New CrystalDecisions.Windows.Forms.CrystalReportViewer

                        With CRV
                            .ShowRefreshButton = False
                            .ReportSource = report

                            If Me.Formular <> "" Then

                                If report.RecordSelectionFormula <> "" Then
                                    .SelectionFormula = "(" & report.RecordSelectionFormula & " )  AND  " & Me.Formular
                                Else
                                    .SelectionFormula = Me.Formular
                                End If
                            End If

                            .ShowPrintButton = Me.ShowPrint
                            .ShowExportButton = Me.ShowExport

                            .Dock = DockStyle.Fill

                            For Each ts As ToolStrip In .Controls.OfType(Of ToolStrip)()
                                For Each tsb As ToolStripButton In ts.Items.OfType(Of ToolStripButton)()
                                    'hacky but should work. you can probably figure out a better method

                                    Select Case True
                                        Case tsb.ToolTipText.ToLower().Contains("print")
                                            'Adding a handler for our propose
                                            AddHandler tsb.Click, AddressOf printButton_Click
                                        Case tsb.ToolTipText.ToLower().Contains("export")
                                            'Adding a handler for our propose
                                            AddHandler tsb.Click, AddressOf exportButton_Click
                                    End Select

                                Next
                            Next

                        End With

                        If Me.Print Then

                            report.DataDefinition.RecordSelectionFormula = CRV.SelectionFormula
                            report.PrintToPrinter(1, True, 0, 0)

                        Else
                            Dim RPT As New DevExpress.XtraEditors.XtraForm
                            With RPT
                                .Controls.Add(CRV)
                                .WindowState = FormWindowState.Maximized


                                If (HI.ST.SysInfo.Admin) Then
                                    .Text = Me.FormTitle & " ( " & Me.ReportName & " ) "
                                Else
                                    .Text = Me.FormTitle
                                End If

                                .Show()
                            End With
                        End If

                End Select
            Else

                HI.MG.ShowMsg.mInfo("Can't Find Report", 1403160001, Me.FormTitle)
            End If

        Catch ex As Exception

            ' MsgBox(ex.Message & vbCrLf & (Me.PathReport & Me.ReportFolderName & Me.ReportName))
        End Try

    End Sub

    Private Sub printButton_Click(sender As Object, e As EventArgs)
        ' MessageBox.Show("Printed")
    End Sub

    Private Sub exportButton_Click(sender As Object, e As EventArgs)
        '  MessageBox.Show("Export")
    End Sub

End Class

Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Pdf
Imports DevExpress.XtraReports.UI
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine


Public Class wUseDoc
    Private _ProcLoad As Boolean = False
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wUseDoc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            XtraTabPage2.PageVisible = False
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.FNHSysCmpId.Properties.Tag = HI.ST.SysInfo.CmpID
        Catch ex As Exception
        End Try
    End Sub

    Private ReportName As String = "RptDocument.rpt"
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
    Private _PathReportTitle As String = Application.StartupPath & "\Reports\"

    Private _Formular As String = ""
    Public Property Formular() As String
        Get
            Return _Formular
        End Get
        Set(value As String)
            _Formular = value
        End Set
    End Property


    Private Sub LoadReport(_FNHSysDocNameId As Integer)
        Try


            Dim _Fm As String = ""

            '  _Fm = "  {V_ReportExportInv.FTInvoiceNo} = '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "



            Dim report As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            Dim i As Integer
            Dim Database As String
            Dim _ServerName, _UID, _PWS As String


            With report

                .Load(_PathReportTitle & "Document\RptDocument.rpt")
                _Formular = "{TDCDocument.FNHSysDocNameId} =" & _FNHSysDocNameId
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

                'If _DTParameter Is Nothing Then
                '    Me.CreateTableParameter()
                'End If

                Try
                    Me.CreateTableParameter()
                Catch ex As Exception

                End Try


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

                'Try
                '    .SummaryInfo.ReportTitle = Me.ReportTitle
                'Catch ex As Exception
                'End Try

            End With



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

                .ShowPrintButton = True
                .ShowExportButton = True
                .ShowGotoPageButton = False

                .Dock = DockStyle.Fill
            End With


            Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
            Dim CrDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions()
            CrExportOptions = report.ExportOptions
            CrDiskFileDestinationOptions.DiskFileName = Me.ReportName

            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .DestinationOptions = CrDiskFileDestinationOptions
            End With

            ' Dim RPT As New DevExpress.XtraEditors.XtraForm
            With ogrpcover
                .Controls.Clear()
                .Controls.Add(CRV)
                ' .WindowState = FormWindowState.Maximized

                'If (HI.ST.SysInfo.Admin) Then
                '    .Text = Me.FormTitle & " ( " & Me.ReportName & " ) "
                'Else
                '    .Text = Me.FormTitle
                'End If

                '_spls.Close()

                .Show()
            End With







        Catch ex As Exception

        End Try
    End Sub

    Private _Pdfdata As Byte()
    Private Sub LoadDetail()
        Dim  _Spls As New HI.TL.SplashScreen("Loading Document... Please Wait... ")
        Try
            Dim _Cmd As String = "" : Dim _FileType As Integer = 0
            _Cmd = "Select Top 1  FNFileType "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle WITH(NOLOCK)"
            _Cmd &= vbCrLf & " WHERE FNHSysDocNameId=" & Integer.Parse(Me.FNHSysDocNameId.Properties.Tag)
            _Cmd &= vbCrLf & " ORDER BY FDDocumentDate DESC "
            _FileType = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_DOC, 0)

            _Cmd = "Select FBDocument From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FNHSysDocNameId=" & Integer.Parse(Me.FNHSysDocNameId.Properties.Tag)
            _Cmd &= vbCrLf & " And Isnull(FTStateManagerApp,'') = '1'"
            If Not (HI.ST.SysInfo.Admin) Then
                _Cmd &= vbCrLf & " And FTDocumentNo in  (Select R.FTDocumentNo"
                _Cmd &= vbCrLf & "  FRom    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK)  "
                _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH(NOLOCK) ON U.FNHSysEmpID = E.FNHSysEmpID"
                _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCPermisstionRead AS R WITH(NOLOCK) ON E.FNHSysUnitSectId = R.FNHSysUnitSectId "
                _Cmd &= vbCrLf & " Where U.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  )"
            End If


            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)
            For Each R As DataRow In _oDt.Rows
                Select Case _FileType
                    Case 2
                        Me.ogrpdetail.Controls.Clear()
                        Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                        With _XlsV
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .Unit = DevExpress.Office.DocumentUnit.Inch
                            .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)
                        End With
                        Me.ogrpdetail.Controls.Add(_XlsV)
                    Case 3
                        Me.ogrpdetail.Controls.Clear()
                        Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                        With _RTX
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DocumentFormat.Doc)
                        End With
                        Me.ogrpdetail.Controls.Add(_RTX)
                    Case 1
                        Me.ogrpdetail.Controls.Clear()
                        Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                        With _RTX
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DocumentFormat.PlainText)
                        End With
                        Me.ogrpdetail.Controls.Add(_RTX)
                    Case 0
                        Try
                            Me.ogrpdetail.Controls.Clear()
                            Dim _Pdfv As New PdfViewer
                            With _Pdfv
                                .Dock = DockStyle.Fill
                                _Pdfdata = CType(R!FBDocument, Byte())
                                .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())))
                            End With
                            Me.ogrpdetail.Controls.Add(_Pdfv)
                        Catch ex As Exception
                        End Try
                End Select

                XtraTabPage2.PageVisible = True
                LoadReport(Integer.Parse(Me.FNHSysDocNameId.Properties.Tag.ToString))
            Next
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FNHSysDocNameId.Text <> "" Then
            XtraTabPage2.PageVisible = False
            Call LoadDetail()

        End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmdownloadfile_Click(sender As Object, e As EventArgs) Handles ocmdownloadfile.Click
        Try
            For Each obj As Object In Me.ogrpdetail.Controls
                'Dim _Filter As String = "Excel Workbook(*.xlsx)|*.xlsx|Excel 97-2003 Workbook(*.xls)|*.xls|PDF(*.pdf)|*.pdf|Word Documents(97-2003)|*.doc|Word Documents(2010-2013)|*.docx"
                Select Case obj.GetType.Name.ToString
                    Case "RichEditControl"
                        Dim dialog As New SaveFileDialog()
                        dialog.Filter = "Word Documents(97-2003)|*.doc|Word Documents(2010-2013)|*.docx"
                        Dim result As DialogResult = dialog.ShowDialog()
                        Dim fileName As String = dialog.FileName
                        If result = DialogResult.OK Then
                            With CType(obj, DevExpress.XtraRichEdit.RichEditControl)
                                .SaveDocument(fileName, DocumentFormat.Doc)
                            End With
                        End If
                        Process.Start(fileName)
                    Case "PdfViewer"
                        Dim dialog As New SaveFileDialog()
                        dialog.Filter = "PDF files |*.pdf"
                        Dim result As DialogResult = dialog.ShowDialog()
                        Dim fileName As String = dialog.FileName
                        If result = DialogResult.OK Then
                            With CType(obj, DevExpress.XtraPdfViewer.PdfViewer)
                                My.Computer.FileSystem.WriteAllBytes(fileName, _Pdfdata, True)
                            End With
                            Process.Start(fileName)
                        End If
                    Case "SpreadsheetControl"
                        Dim dialog As New SaveFileDialog()
                        dialog.Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                        Dim result As DialogResult = dialog.ShowDialog()
                        Dim fileName As String = dialog.FileName
                        If result = DialogResult.OK Then
                            With CType(obj, DevExpress.XtraSpreadsheet.SpreadsheetControl)
                                .SaveDocument(fileName)
                            End With
                        End If
                        Process.Start(fileName)
                End Select
            Next

        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True
        Dim _Dt As System.Data.DataTable
        Dim _Cmd As String = "  "

        _Dt = LoadData(Key)
        Me.FTReviseNo.Text = "0"
        If _Dt.Rows.Count > 0 Then
            Me.FTReviseNo.Text = _Dt.Rows(0).Item("FNReviseNo").ToString
        End If



        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd = "Select  [dbo].[FN_GET_YMD](FDManagerAppDate,convert(varchar(10),getdate(),111),'TH') AS FDDocAge "
        Else
            _Cmd = "Select  [dbo].[FN_GET_YMD](FDManagerAppDate,convert(varchar(10),getdate(),111),'EN') AS FDDocAge "
        End If
        _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle  WITH(NOLOCK) "
        _Cmd &= vbCrLf & "WHERE FNHSysDocNameId=" & Integer.Parse("0" & Me.FNHSysDocNameId.Properties.Tag)
        _Dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)




        Dim _FieldName As String = ""
        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                .Text = R.Item(Col).ToString

                            End With

                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            Obj.Text = R.Item(Col).ToString
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            Try

                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next
            Exit For
        Next
        
        _ProcLoad = False
    End Sub

    Private Sub FNHSysDocNameId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysDocNameId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysDocNameId_EditValueChanged), New Object() {sender, e})
            Else
                Me.FDDocAge.Text = ""
                If FNHSysDocNameId.Text <> "" Then
                    Dim _Qry As String = "SELECT TOP 1 FNHSysDocNameId  FROM  TDOCMDocumentTitle WITH(NOLOCK) WHERE FTDocNameCode ='" & HI.UL.ULF.rpQuoted(FNHSysDocNameId.Text) & "' "
                    FNHSysDocNameId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                    Call LoadDataInfo(Me.FNHSysDocNameId.Text)
                Else
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function LoadData(Key As Object) As System.Data.DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT D.FTDocumentNo,   max( isnull(D.FNReviseNo,0)) as FNReviseNo  "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & " , max( T.FTDocNameTH) as FTDocName  "
            Else
                _Cmd &= vbCrLf & " , max( T.FTDocNameEN) as FTDocName "
            End If
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument AS D WITH(NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument_File AS F WITH(NOLOCK) ON D.FTDocumentNo = F.FTDocumentNo "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle AS T WITH(NOLOCK) ON D.FNHSysDocNameId = T.FNHSysDocNameId "
            _Cmd &= vbCrLf & " Where  FNHSysCmpId=" & Integer.Parse(Me.FNHSysCmpId.Properties.Tag.ToString)
            _Cmd &= vbCrLf & " and  D.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & " Group by D.FTDocumentNo   "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)

            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



End Class
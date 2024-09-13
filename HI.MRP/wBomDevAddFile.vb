Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic

Public Class wBomDevAddFile

#Region "Variable Declaration"
    Private Const _nTotalFactorySubOrderNo As Integer = 26
    Private _tSysDBName As String
    Private _tSysTableName As String
    Private __SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
    Private _tFTOrderNo As String       '...เลขที่ใบสั่งผลิต
    Private _nFNHSysCmpId As Integer
    Private _nFNHSysCmpRunId As Integer
    Private _nFNHSysStyleId As Integer
    Private _tFNHSysCmpId As String     '...รหัสโรงงาน/บริษัท สาขา : Code
    Private _tFNHSysCmpRunId As String  '...รหัสเลข run เอกสาร : Code
    Private _tFNHSysStyleId As String   '...รหัสสไตล์ : Code
    Private tSql As String

    'Private _wListCompleteCopyOrder As wListCompleteCopyOrder

    Private _oImage1 As System.Drawing.Image
    Private _oImage2 As System.Drawing.Image
    Private _oImage3 As System.Drawing.Image
    Private _oImage4 As System.Drawing.Image

    Private _FTImage1 As String
    Private _FTImage2 As String
    Private _FTImage3 As String
    Private _FTImage4 As String

#End Region

#Region "Property"
    Private Shared _DTImageRefOrderNo As System.Data.DataTable = Nothing
    Private Shared ReadOnly Property _LoadImageRefOrderNo(ByVal paramOrderNo As String) As System.Data.DataTable
        Get
            If _DTImageRefOrderNo Is Nothing Then
                Dim sSQL As String
                sSQL = ""
                sSQL = "SELECT A.FTOrderNo, A.FTImage1, A.FTImage2, A.FTImage3, A.FTImage4"
                sSQL &= Environment.NewLine & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS A (NOLOCK)"
                sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramOrderNo) & "';"

                _DTImageRefOrderNo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            End If

            Return _DTImageRefOrderNo

        End Get

    End Property

    Private _ProcState As Boolean = False
    Public Property AddFileState As Boolean
        Get
            Return _ProcState
        End Get
        Set(value As Boolean)
            _ProcState = value
        End Set
    End Property

    Private _DataFilePath As String = ""
    Public Property DataFilePath As String
        Get
            Return _DataFilePath
        End Get
        Set(value As String)
            _DataFilePath = value
        End Set
    End Property



    Enum FileExtention
        Text = 0
        PDF = 1
        DOC = 2
        DOCX = 3
        XLS = 4
        XLSX = 5
    End Enum

    Private _FileExt As FileExtention = FileExtention.Text
    Public Property FileExt As FileExtention
        Get
            Return _FileExt
        End Get
        Set(value As FileExtention)
            _FileExt = value
        End Set
    End Property

    Public Sub ShowFile(AddFileType As Integer, AddFileName As String, dttabyte As Byte(), FileExt As String)

        FTFileName.Text = AddFileName

        'Select Case AddFileType
        '    Case 0
        '        '"PDF files |*.pdf"

        '        Dim _Pdfv As New DevExpress.XtraPdfViewer.PdfViewer


        '        _Pdfv.LoadDocument(New System.IO.MemoryStream(dttabyte))
        '        _Pdfv.ReadOnly = True
        '        Me.oGrpdetail.Controls.Add(_Pdfv)
        '        _Pdfv.Dock = DockStyle.Fill
        '    Case 1, 3
        '        ' "Text files |*.txt"  ' "Word Documents(97-2003)|*.doc|Word Documents(2010-2013)|*.docx"
        '        Dim _Txt As New DevExpress.XtraRichEdit.RichEditControl

        '        If FileExt = "DOC" Or FileExt = "DOCX" Then
        '            _Txt.LoadDocument(New System.IO.MemoryStream(dttabyte), DevExpress.XtraRichEdit.DocumentFormat.Doc)
        '        Else
        '            _Txt.LoadDocument(New System.IO.MemoryStream(dttabyte), DevExpress.XtraRichEdit.DocumentFormat.OpenXml)
        '        End If
        '        _Txt.ReadOnly = True

        '        Me.oGrpdetail.Controls.Add(_Txt)
        '        _Txt.Dock = DockStyle.Fill
        '    Case 2
        '        '"Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"

        '        Dim _Excel As New DevExpress.XtraSpreadsheet.SpreadsheetControl


        '        If FileExt = "XLSX" Then
        '            _Excel.LoadDocument(New System.IO.MemoryStream(dttabyte), DevExpress.Spreadsheet.DocumentFormat.Xlsx)
        '        Else
        '            _Excel.LoadDocument(New System.IO.MemoryStream(dttabyte), DevExpress.Spreadsheet.DocumentFormat.Xls)
        '        End If
        '        _Excel.ReadOnly = True

        '        Me.oGrpdetail.Controls.Add(_Excel)
        '        _Excel.Dock = DockStyle.Fill
        'End Select


        Select Case AddFileType
            Case 2
                Me.oGrpdetail.Controls.Clear()
                Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                With _XlsV
                    .ReadOnly = True
                    .Dock = DockStyle.Fill
                    .Unit = DevExpress.Office.DocumentUnit.Inch
                    .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document

                    If FileExt = "XLSX" Then
                        .LoadDocument(New MemoryStream(CType(dttabyte, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xlsx)
                    Else
                        .LoadDocument(New MemoryStream(CType(dttabyte, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)
                    End If


                End With
                Me.oGrpdetail.Controls.Add(_XlsV)
            Case 3
                Me.oGrpdetail.Controls.Clear()
                Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                With _RTX
                    .ReadOnly = True
                    .Dock = DockStyle.Fill
                    .LoadDocument(New MemoryStream(CType(dttabyte, Byte())), DevExpress.XtraRichEdit.DocumentFormat.Doc)
                End With
                Me.oGrpdetail.Controls.Add(_RTX)
            Case 1
                Me.oGrpdetail.Controls.Clear()
                Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                With _RTX
                    .ReadOnly = True
                    .Dock = DockStyle.Fill
                    .LoadDocument(New MemoryStream(CType(dttabyte, Byte())), DevExpress.XtraRichEdit.DocumentFormat.PlainText)

                End With
                Me.oGrpdetail.Controls.Add(_RTX)
            Case 0
                Try
                    Me.oGrpdetail.Controls.Clear()
                    Dim _Pdfv As New DevExpress.XtraPdfViewer.PdfViewer
                    With _Pdfv
                        .Dock = DockStyle.Fill

                        .LoadDocument(New MemoryStream(CType(dttabyte, Byte())))
                    End With
                    Me.oGrpdetail.Controls.Add(_Pdfv)
                Catch ex As Exception
                End Try
        End Select


        '_Cmd = "Select Top 1  FTDocumentTitle, FBDocument, FNFileType "
        '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument WITH(NOLOCK)"
        '_Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
        'Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)
        'For Each R As DataRow In _oDt.Rows
        '    Select Case CInt("0" & R!FNFileType.ToString)
        '        Case 2
        '            Me.oGrpdetail.Controls.Clear()
        '            Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
        '            With _XlsV
        '                .ReadOnly = True
        '                .Dock = DockStyle.Fill
        '                .Unit = DevExpress.Office.DocumentUnit.Inch
        '                .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
        '                .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)

        '            End With
        '            Me.oGrpdetail.Controls.Add(_XlsV)
        '        Case 3
        '            Me.oGrpdetail.Controls.Clear()
        '            Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
        '            With _RTX
        '                .ReadOnly = True
        '                .Dock = DockStyle.Fill
        '                .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DocumentFormat.Doc)
        '            End With
        '            Me.oGrpdetail.Controls.Add(_RTX)
        '        Case 1
        '            Me.oGrpdetail.Controls.Clear()
        '            Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
        '            With _RTX
        '                .ReadOnly = True
        '                .Dock = DockStyle.Fill
        '                '.Options.Import.PlainText.AutoDetectEncoding = False
        '                '.Options.Import.PlainText.Encoding = Encoding.UTF8
        '                .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DocumentFormat.PlainText)

        '            End With
        '            Me.oGrpdetail.Controls.Add(_RTX)
        '        Case 0
        '            Try
        '                Me.oGrpdetail.Controls.Clear()
        '                Dim _Pdfv As New PdfViewer
        '                With _Pdfv
        '                    .Dock = DockStyle.Fill
        '                    _Pdfdata = CType(R!FBDocument, Byte())
        '                    .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())))
        '                End With
        '                Me.oGrpdetail.Controls.Add(_Pdfv)
        '            Catch ex As Exception
        '            End Try
        '    End Select
        'Next


    End Sub
#End Region

#Region "Procedure And Function"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub





#End Region

#Region "Event Handle"

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        AddFileState = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click

        If FTFileName.Text <> "" Then
            If Me.oGrpdetail.Controls.Count > 0 Then
                AddFileState = True
                Me.Close()
            End If
        End If


    End Sub


    Private Sub ocmReadDocumentfile_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
        Try
            Dim _FileName As String = ""
            Dim _FileType As String = ""
            Dim _FilePath As String = ""

            Dim folderDlg As New OpenFileDialog
            With folderDlg

                Select Case Me.FNFileType.SelectedIndex
                    Case 0
                        .Filter = "PDF files |*.pdf"
                    Case 1
                        .Filter = "Text files |*.txt"
                    Case 2
                        .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                    Case 3
                        .Filter = "Word Documents(97-2003)|*.doc|Word Documents(2010-2013)|*.docx"
                End Select

                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False

                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    _FilePath = .FileName


                    DataFilePath = _FilePath

                    _FileType = System.IO.Path.GetExtension(_FilePath)
                    _FileName = System.IO.Path.GetFileName(_FilePath)
                    Select Case _FileType.ToUpper
                        Case ".XLSX".ToUpper, ".XLS".ToUpper

                            If _FileType.ToUpper = ".XLSX" Then
                                FileExt = FileExtention.XLSX
                            Else
                                FileExt = FileExtention.XLS
                            End If

                            Call _ExcelViewer(_FilePath)

                        Case ".TXT".ToUpper, ".DOC".ToUpper, ".DOCX".ToUpper

                            If _FileType.ToUpper = ".TXT" Then
                                FileExt = FileExtention.Text
                            ElseIf _FileType.ToUpper = ".DOC" Then
                                FileExt = FileExtention.DOC
                            Else
                                FileExt = FileExtention.DOCX
                            End If

                            Call _TextViewer(_FilePath)

                        Case ".PDF".ToUpper

                            FileExt = FileExtention.PDF

                            Call _PDFViewer(_FilePath)
                        Case Else
                            HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1010301710, Me.Text)
                            Exit Sub
                    End Select

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _PDFViewer(_FileName As String)
        Try
            Me.oGrpdetail.Controls.Clear()
            Dim _Pdfv As New DevExpress.XtraPdfViewer.PdfViewer
            _Pdfv.Dock = DockStyle.Fill
            _Pdfv.LoadDocument(_FileName)
            Me.oGrpdetail.Controls.Add(_Pdfv)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _TextViewer(_FileName As String)
        Try
            Me.oGrpdetail.Controls.Clear()
            Dim _Txt As New DevExpress.XtraRichEdit.RichEditControl
            _Txt.ReadOnly = True
            _Txt.Dock = DockStyle.Fill

            ' _Txt.Options.Import.PlainText.Encoding = Encoding.UTF8
            _Txt.LoadDocument(_FileName)
            Me.oGrpdetail.Controls.Add(_Txt)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _ExcelViewer(_FileName As String)
        Try
            Me.oGrpdetail.Controls.Clear()
            Dim _Excel As New DevExpress.XtraSpreadsheet.SpreadsheetControl
            _Excel.Dock = DockStyle.Fill
            _Excel.LoadDocument(_FileName)
            _Excel.ReadOnly = True
            Me.oGrpdetail.Controls.Add(_Excel)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNFileType.SelectedIndexChanged
        Try
            Me.oGrpdetail.Controls.Clear()
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class
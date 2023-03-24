Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Pdf


Public Class wReadDocFile

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.Width = Screen.PrimaryScreen.Bounds.Width
        Me.Height = (Screen.PrimaryScreen.Bounds.Height / 100) * 85
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _Key As String = ""
    Public Property Key As String
        Get
            Return _Key
        End Get
        Set(value As String)
            _Key = value
        End Set
    End Property

    Private _DocName As String = ""
    Public Property DocName As String
        Get
            Return _DocName
        End Get
        Set(value As String)
            _DocName = value
        End Set
    End Property

    Private Sub wReadDocFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.Text = DocName
            GenerateTab()
        Catch ex As Exception
        End Try
    End Sub

    Private Function LoadData(_Key As String) As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select  FTDocumentNo , FNSeq , FTDocumentTitle , FBDocument  "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument_File  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & " where  FTDocumentNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)
            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub GenerateTab()
        Try
            Dim _Cmd As String = "" : Dim _FileType As Integer = 0
            Dim _oDt As DataTable = LoadData(Key)
            Dim _Grp As DevExpress.XtraEditors.GroupControl
            Dim _Tab As DevExpress.XtraTab.XtraTabPage
            Me.XtraTabControl1.TabPages.Clear()

            _Cmd = "Select Top 1  FNFileType "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle WITH(NOLOCK)"
            _Cmd &= vbCrLf & " where  FTDocumentNo='" & HI.UL.ULF.rpQuoted(Key) & "'"

            _FileType = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_DOC, 0)

            For Each R As DataRow In _oDt.Rows
                _Grp = New DevExpress.XtraEditors.GroupControl
                With _Grp
                    .Name = "Grp" & R!FNSeq.ToString
                    .Text = R!FNSeq.ToString
                    .ShowCaption = True
                    .Dock = DockStyle.Fill
                End With

                Select Case _FileType
                    Case 2
                        _Grp.Controls.Clear()
                        Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                        With _XlsV
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .Unit = DevExpress.Office.DocumentUnit.Inch
                            .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)
                        End With
                        _Grp.Controls.Add(_XlsV)
                    Case 3
                        _Grp.Controls.Clear()
                        Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                        With _RTX
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.XtraRichEdit.DocumentFormat.Doc)
                        End With
                        _Grp.Controls.Add(_RTX)
                    Case 1
                        _Grp.Controls.Clear()
                        Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                        With _RTX
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.XtraRichEdit.DocumentFormat.PlainText)
                        End With
                        _Grp.Controls.Add(_RTX)
                    Case 0
                        Try
                            _Grp.Controls.Clear()
                            Dim _Pdfv As New PdfViewer
                            With _Pdfv
                                .Dock = DockStyle.Fill

                                .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())))
                            End With
                            _Grp.Controls.Add(_Pdfv)
                        Catch ex As Exception
                        End Try
                End Select

                _Tab = New DevExpress.XtraTab.XtraTabPage
                With _Tab
                    .Name = "Tab" & R!FNSeq.ToString
                    .Controls.Add(_Grp)
                    .Text = R!FNSeq.ToString & " " & R!FTDocumentTitle.ToString
                End With

                Me.XtraTabControl1.TabPages.Add(_Tab)
            Next

            Me.XtraTabPage1.Controls.Add(Me.GroupControl1)
            Me.XtraTabPage1.Name = "XtraTabPage1"
            Me.XtraTabPage1.Size = New System.Drawing.Size(1196, 778)
            Me.XtraTabPage1.Text = "XtraTabPage1"




        Catch ex As Exception

        End Try
    End Sub

End Class
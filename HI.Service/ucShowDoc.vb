Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Pdf
Public Class ucShowDoc

    Sub New(data As Byte(), filetype As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call LoadTxt(data, filetype)
    End Sub

    Private Sub LoadTxt(data As Byte(), Filetype As Integer)
        Try

            Select Case Filetype
                Case 2
                    Me.ogrpdetail.Controls.Clear()
                    Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                    With _XlsV
                        .ReadOnly = True
                        .Dock = DockStyle.Fill
                        .Unit = DevExpress.Office.DocumentUnit.Inch
                        .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                        .LoadDocument(New MemoryStream(CType(data, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)
                    End With
                    Me.ogrpdetail.Controls.Add(_XlsV)
                Case 3
                    Me.ogrpdetail.Controls.Clear()
                    Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                    With _RTX
                        .ReadOnly = True
                        .Dock = DockStyle.Fill
                        .LoadDocument(New MemoryStream(CType(data, Byte())), DocumentFormat.Doc)
                    End With
                    Me.ogrpdetail.Controls.Add(_RTX)
                Case 1
                    Me.ogrpdetail.Controls.Clear()
                    Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                    With _RTX
                        .ReadOnly = True
                        .Dock = DockStyle.Fill
                        .LoadDocument(New MemoryStream(CType(data, Byte())), DocumentFormat.PlainText)
                    End With
                    Me.ogrpdetail.Controls.Add(_RTX)
                Case 0
                    Try
                        Me.ogrpdetail.Controls.Clear()
                        Dim _Pdfv As New PdfViewer
                        With _Pdfv
                            .Dock = DockStyle.Fill
                            .LoadDocument(New MemoryStream(CType(data, Byte())))
                        End With
                        Me.ogrpdetail.Controls.Add(_Pdfv)
                    Catch ex As Exception
                    End Try
            End Select

        Catch ex As Exception

        End Try
    End Sub

  
End Class

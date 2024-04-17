

Imports DevExpress.Pdf
Imports DevExpress.Drawing
Imports System.Drawing
Imports DevExpress.Text.Fonts
Imports System.IO
Imports DevExpress.XtraEditors.Controls
Imports System.Data.SqlClient

Public Class wPDFViewer

    Public grp As List(Of String)
    Public SuplierCode As String
    Public PINO As String
    Private fields As List(Of PdfAcroFormField) = New List(Of PdfAcroFormField)()
    Public pfilByte As Byte()
    Public pfilByteNew As Byte()
    Private createFieldsModeEnabled As Boolean = False
    Public AddComplete As Boolean = False

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub


#Region "Function"


#End Region

    Private Sub wAddRequest_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Using processor = New PdfDocumentProcessor()
        '    processor.CreateEmptyDocument()

        '    Using graphics As PdfGraphics = processor.CreateGraphics()
        '        ' Obtain the first document page
        '        Dim page As PdfPage = processor.AddNewPage(PdfPaperSize.A4)
        '        Dim pageSize As PdfRectangle = page.CropBox

        '        ' Specify text to draw
        '        Dim text As String = "PDF Document API"
        '        Using textBrush As New SolidBrush(Color.FromArgb(255, Color.DarkOrange))
        '            Using font As New DXFont("Segoe UI", 20, DXFontStyle.Regular)
        '                ' Calculate text size
        '                Dim textSize As SizeF = graphics.MeasureString(text, font, New PdfStringFormat(), 72, 72)

        '                ' Calculate a point where to draw text
        '                Dim textPoint As New PointF(CSng((pageSize.Width - textSize.Width) \ 2), CSng((pageSize.Height - textSize.Height) \ 2))

        '                ' Draw text at the calculated point
        '                graphics.DrawString(text, font, textBrush, textPoint)

        '                ' Add graphics content to the page foreground
        '                graphics.AddToPageForeground(page, 72, 72)
        '            End Using
        '        End Using
        '    End Using
        '    processor.SaveDocument("result.pdf")
        'End Using
    End Sub

    Private Sub FilePdf_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles FilePdf.MouseUp
        Try
            If Not createFieldsModeEnabled Then Return
            Dim PDFCursorLocation As PdfDocumentPosition = FilePdf.GetDocumentPosition(e.Location)
            If PDFCursorLocation Is Nothing Then Return

            Dim cmdstring As String = "SELECT TOP 1 FTUserDescriptionEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin With(NOLOCK) WHERE FTUserName ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            Dim myFieldName As String = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")


            If myFieldName <> "" Then

                Dim myPage As Integer = PDFCursorLocation.PageNumber
                Dim startPoint As PdfPoint = PDFCursorLocation.Point
                Dim endPoint As PdfPoint = ExpandToRectangle(startPoint)
                Dim pdfRectangle As PdfRectangle = New PdfRectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Max(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y))

                Dim textBox As PdfAcroFormTextBoxField = New PdfAcroFormTextBoxField(myFieldName, myPage, pdfRectangle)
                textBox.Text = myFieldName
                textBox.Appearance.ForeColor = New PdfRGBColor(0.1, 0, 0.8)
                'textBox.Appearance.BackgroundColor = New PdfRGBColor(0.8, 0.5, 0.3)
                textBox.Appearance.FontSize = 12

                fields.Add(textBox)
                UpdateDocument()
                ocmconfirm.Enabled = True
            End If
        Catch ex As Exception

        End Try


    End Sub

    'Private Shared Sub Draw(ByVal page As PdfPage)
    '    Using graphics As PdfGraphics = New DevExpress.Pdf.PdfGraphics()

    '        Using image As Bitmap = New Bitmap("..\..\Northwind.png")
    '            Dim width As Single = image.Width
    '            Dim height As Single = image.Height
    '            graphics.DrawImage(image, New RectangleF(100, 100, width / 2, height / 2))
    '        End Using

    '        graphics.AddToPageForeground(page)
    '    End Using
    'End Sub

    Private Sub UpdateDocument()
        Dim scrollPosition As Single = FilePdf.VerticalScrollPosition
        Dim filePath As String = FilePdf.DocumentFilePath
        Dim tempFilePath As String = System.IO.Path.GetTempFileName()
        FilePdf.CloseDocument()

        Try

            Using processor As PdfDocumentProcessor = New PdfDocumentProcessor()
                processor.LoadDocument(New MemoryStream(pfilByte))
                processor.AddFormFields(fields.ToArray())
                processor.SaveDocument(tempFilePath)

            End Using

            pfilByteNew = System.IO.File.ReadAllBytes(tempFilePath)
        Finally
            fields.Clear()
            FilePdf.LoadDocument(New MemoryStream(pfilByteNew))
            FilePdf.VerticalScrollPosition = scrollPosition
        End Try

    End Sub

    Private Function ExpandToRectangle(ByVal point As PdfPoint) As PdfPoint
        Return New PdfPoint(point.X + 100, point.Y - 50)
    End Function

    Private Sub CheckEdit1_CheckedChanged(sender As Object, e As EventArgs) Handles chkaddlicense.CheckedChanged
        ocmconfirm.Enabled = False
        createFieldsModeEnabled = chkaddlicense.Checked

        ocmconfirm.Visible = createFieldsModeEnabled

        If createFieldsModeEnabled = False Then
            FilePdf.LoadDocument(New MemoryStream(pfilByte))

        End If
    End Sub

    Private Sub ocmconfirm_Click(sender As Object, e As EventArgs) Handles ocmconfirm.Click
        If createFieldsModeEnabled Then
            Try

                Dim pSaveFileNameNew As String = System.IO.Path.GetTempFileName()

                Dim _CheckPath As String = "C:\WISDOMPOPDF"

                Try
                    If Directory.Exists(_CheckPath) = False Then
                        Directory.CreateDirectory(_CheckPath)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Exit Sub
                End Try


                Dim pPathPDF As String = ""
                Dim cmdstring As String = ""

                cmdstring = "select top 1 FTCfgData   "
                cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig With(NOLOCK) "
                cmdstring &= vbCrLf & "  Where (FTCfgName = N'POPDF')"

                pPathPDF = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

                Dim pdfDocumentProcessor As New DevExpress.Pdf.PdfDocumentProcessor()
                pdfDocumentProcessor.CreateEmptyDocument()

                Dim datapi As Byte() = pfilByteNew

                Dim myByteArray1 As Byte() = datapi
                Dim Stream1 As New MemoryStream()
                Stream1.Write(myByteArray1, 0, myByteArray1.Length)
                pdfDocumentProcessor.AppendDocument(Stream1)

                Dim RIndx As Integer = 0
                cmdstring = ""
                Dim dtpdf As New DataTable
                For Each pPoNo As String In grp

                    cmdstring = " select top 1 FPFile from [WSM-HT-HQ].[HITECH_PURCHASE].[dbo].[TPURTPurchase_PDF]  WITH(NOLOCK)   WHERE FTPurchaseNo='" + HI.UL.ULF.rpQuoted(pPoNo) + "' AND FTStateFIle='1' "

                    dtpdf = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                    If dtpdf.Rows.Count > 0 Then
                        For Each R As DataRow In dtpdf.Rows

                            Dim myByteArray As Byte() = CType(R("FPFile"), Byte())
                            Dim Stream As New MemoryStream()
                            Stream.Write(myByteArray, 0, myByteArray.Length)
                            pdfDocumentProcessor.AppendDocument(Stream)

                        Next
                    Else

                        cmdstring = "   Select TOP 1 FTPurchaseBy, FNPoState from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase Where (FTPurchaseNo ='" + HI.UL.ULF.rpQuoted(pPoNo) + "') "
                        dtpdf = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                        For Each R As DataRow In dtpdf.Rows

                            Dim PODocName As String = Me.CratePOPDF(_CheckPath, pPathPDF, R!FTPurchaseBy.ToString, pPoNo, Val(R!FNPoState.ToString))

                            If PODocName <> "" Then

                                Dim myByteArray As Byte() = File.ReadAllBytes(PODocName)
                                Dim Stream As New MemoryStream()
                                Stream.Write(myByteArray, 0, myByteArray.Length)
                                pdfDocumentProcessor.AppendDocument(Stream)

                            End If
                        Next

                    End If

                Next

                pdfDocumentProcessor.SaveDocument(pSaveFileNameNew, True)

                Dim Rex As Integer = 0

                cmdstring = "Declare @FileId int =0 "
                cmdstring &= vbCrLf & " Select TOP 1  @FileId = ISNULL(1,0)   "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PIAccepetedPDF As A With(NOLOCK)"
                cmdstring &= vbCrLf & "  WHERE FTSuplierCode='" & HI.UL.ULF.rpQuoted(SuplierCode) & "' AND FTPINo='" & HI.UL.ULF.rpQuoted(PINO) & "'"
                cmdstring &= vbCrLf & "  IF @FileId  <=  0 "
                cmdstring &= vbCrLf & "          BEGIN "
                cmdstring &= vbCrLf & "                INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PIAccepetedPDF(FTInsUser, FDInsDate, FTInsTime,FTSuplierCode,FTPINo)"
                cmdstring &= vbCrLf & "                Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & "                       ," & HI.UL.ULDate.FormatDateDB
                cmdstring &= vbCrLf & "                       ," & HI.UL.ULDate.FormatTimeDB
                cmdstring &= vbCrLf & "                       ,'" & HI.UL.ULF.rpQuoted(SuplierCode) & "' "
                cmdstring &= vbCrLf & "                       ,'" & HI.UL.ULF.rpQuoted(PINO) & "' "
                cmdstring &= vbCrLf & "            		SET @FileId = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "          END "
                cmdstring &= vbCrLf & "   SELECT @FileId "

                Rex = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))

                If Rex > 0 Then

                    Dim data As Byte() = System.IO.File.ReadAllBytes(pSaveFileNameNew)

                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
                    HI.Conn.SQLConn.SqlConnectionOpen()

                    cmdstring = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PIAccepetedPDF "
                    cmdstring &= " Set  FTStateFIle='1',FDPDFDate=Convert(varchar(10),Getdate(),111),FTPDFTime=Convert(varchar(8),Getdate(),114), FPFile=@FPFile, FPPIFile=@FPPIFile"
                    cmdstring &= "  Where FTSuplierCode=@SuplierCode"
                    cmdstring &= "  AND FTPINo=@PINo"

                    Dim scmd As New SqlCommand(cmdstring, HI.Conn.SQLConn.Cnn)
                    Dim p6 As New SqlParameter("@FPFile", SqlDbType.VarBinary)
                    p6.Value = data

                    Dim p7 As New SqlParameter("@FPPIFile", SqlDbType.VarBinary)
                    p7.Value = datapi

                    Dim p8 As New SqlParameter("@SuplierCode", SqlDbType.NVarChar)
                    p8.Value = SuplierCode

                    Dim p9 As New SqlParameter("@PINo", SqlDbType.NVarChar)
                    p9.Value = PINO

                    scmd.Parameters.Add(p6)
                    scmd.Parameters.Add(p7)
                    scmd.Parameters.Add(p8)
                    scmd.Parameters.Add(p9)
                    scmd.ExecuteNonQuery()

                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                    AddComplete = True
                    Me.Close()

                End If


            Catch ex As Exception
            End Try

        End If
    End Sub

    Private Sub chkaddlicense_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles chkaddlicense.EditValueChanging
        Dim _CheckPath As String = "C:\WISDOMPOPDF"
        If grp.Count > 0 Then
            Try
                If Directory.Exists(_CheckPath) = False Then
                    Directory.CreateDirectory(_CheckPath)
                End If
                e.Cancel = False
            Catch ex As Exception
                MsgBox(ex.Message)

                e.Cancel = True
                Exit Sub
            End Try
        Else
            e.Cancel = True
        End If



    End Sub
    '...

    Private Function CratePOPDF(_CheckPath As String, pPathPDF As String, pPOBy As String, pPONO As String, PoState As Integer) As String
        Try

            Dim _Sql As String = ""
            Dim Str_Doc_Name As String = ""
            Dim StateFoundPDF As Boolean = False

            Dim StatePDF As Boolean = False

            _Sql = "Select TOP 1 '1' AS FTStatePDF "
            _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase With(NOLOCK) "
            _Sql &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pPONO) & "' AND FTStateManagerApp='1' AND FTStatePDF='1'"
            StatePDF = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_PUR, "") = "1"

            If pPathPDF <> "" And StatePDF Then

                Str_Doc_Name = pPathPDF & "\" & pPOBy & "\" & pPONO & ".pdf"

                If File.Exists(Str_Doc_Name) = True Then
                    StateFoundPDF = True
                Else
                    Str_Doc_Name = ""
                End If

            End If

            If StateFoundPDF = False Then

                With New HI.RP.Report
                    .FormTitle = "Convert To " & pPONO & ".pdf"
                    .ReportFolderName = "PurchaseOrder\"  '"Purchase Report\" '
                    .ReportName = "PurchaseOrder.rpt"
                    .AddParameter("Draft", "")
                    .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(pPONO) & "'"
                    ' ตรวจสอบ โฟร์เดอร์ก่อน

                    .PathExport = _CheckPath & ""
                    '.PathExport = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy & "\"
                    .ExportName = pPONO
                    .ExportFile = HI.RP.Report.ExFile.PDF

                    ' กรณีหาไฟล์ไม่เจอ  ????
                    .PrevieNoSplash(PoState)

                    Dim _FIleExportPDFName As String = .ExportFileSuccessName

                    Str_Doc_Name = _CheckPath & "\" & pPONO & ".pdf"

                End With

            End If

            If File.Exists(Str_Doc_Name) = True Then
                Return Str_Doc_Name
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub ocmexporttopdf_Click(sender As Object, e As EventArgs) Handles ocmexporttopdf.Click
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "PDF Files(*.pdf)|*.pdf"
            Op.ShowDialog()
            Try

                If Op.FileName <> "" Then

                    FilePdf.Export(Op.FileName, PdfFormDataFormat.Fdf)


                    Dim pdfDocumentProcessor As New DevExpress.Pdf.PdfDocumentProcessor()
                    pdfDocumentProcessor.CreateEmptyDocument()

                    Dim datapi As Byte() = pfilByte

                    Dim myByteArray1 As Byte() = datapi
                    Dim Stream1 As New MemoryStream()
                    Stream1.Write(myByteArray1, 0, myByteArray1.Length)
                    pdfDocumentProcessor.AppendDocument(Stream1)


                    pdfDocumentProcessor.SaveDocument(Op.FileName, True)


                    Try
                        Process.Start(Op.FileName)
                    Catch ex As Exception
                    End Try

                End If

            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try

    End Sub
End Class
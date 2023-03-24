
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Imports DevExpress.XtraRichEdit.API.Native

Imports System.Data.SqlClient
Imports DevExpress.XtraRichEdit
Imports System.Windows.Forms
Imports System.Reflection
Imports DevExpress.Office.Utils
Imports DevExpress.XtraRichEdit.Export
Imports System.Text

Imports System.Net

Imports HI.SE.RunID

Imports DevExpress.XtraRichEdit.Model
Imports DevExpress.XtraEditors.Controls.Rtf
Imports DevExpress.XtraEditors

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns


Public Class wMailNew2

    Private sFilePath As String = String.Empty
    Private sFileName As String = String.Empty
    Private sPathFileName As String = String.Empty
    Private sPathServer As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\Mail\Doc\"


    ' Private oDBdtTMAILFileAttach As DataTable
    Private _DTAttach As DataTable


    Private Shared _sFileName As String = ""
    Public Shared Property Data_sFileName As String
        Get
            Return _sFileName
        End Get
        Set(value As String)
            _sFileName = value
        End Set
    End Property

    Private Shared _sPathFileName As String = ""
    Public Shared Property Data_sPathFileName As String
        Get
            Return _sPathFileName
        End Get
        Set(value As String)
            _sPathFileName = value
        End Set
    End Property

    Private Shared _FTMailText As Byte()
    Public Shared Property Data_FTMailText As Byte()
        Get
            Return _FTMailText
        End Get
        Set(value As Byte())
            _FTMailText = value
        End Set
    End Property

    Private Shared _FTMailId_Old As Integer
    Public Shared Property Data_FTMailId_Old As Integer
        Get
            Return _FTMailId_Old
        End Get
        Set(value As Integer)
            _FTMailId_Old = value
        End Set
    End Property


    Private Sub BtnSend_Click(sender As Object, e As EventArgs)

        'If (FTMailTo.Text.Trim = String.Empty) Then
        '    MessageBox.Show("กรุณาระบุ ชื่อผู้รับเมล", "Message 60", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    FTMailTo.Focus() : Exit Sub
        'End If





        'If InsertTMAILMessages() = True Then
        '    Me.Close()
        'End If


    End Sub

    Private Function InsertTMAILMessages() As Boolean

        Dim _Str As String
        Dim data As Byte()

        Dim _FTMailId As Integer

        Dim _TempData As String

        '  Call WriteToStream2()


        '  Dim _Path As String = "E:\HI SOFT SOURCE CODE_PEE\HI SOFT\HI\bin\Debug\Images\Mail\Doc\"

        _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MAIL)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim nMailStateAttach As Integer = 0
            If ogvTMAILFileAttach.RowCount > 0 Then
                nMailStateAttach = 1
            Else
                nMailStateAttach = 0
            End If


            'If FNMailFileAttach.Text.Trim = String.Empty Then
            '    nMailStateAttach = 0
            'Else
            '    nMailStateAttach = 1
            'End If

            'Dim sTempDate As String
            'sTempDate = Now.Date.ToString("yyyy/MM/dd") & " " & Format(Date.Now, "HH:mm:ss")

            _Str = ""
            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus])"
            _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & Me.FTMailTo.Text & "'"
            _Str &= ",'" & Me.FTMailSubject.Text & "',0,1,"
            _Str &= nMailStateAttach & "," & FNMailStatePriority.SelectedIndex & ",0)"


            ' _Str &= ControlChars.CrLf & ",[FTMailOpenTime],[FNMailStateAttach],[FNMailStatePriority],[FNMailStateSend]"  
            ' _Str &= ",@FTMailText "
            'Now.Date.ToString("yyyy/MM/dd HH:mm:ss")
            ' Now.Date.ToString("yyyy/MM/dd")
            ''" & Now.Date.ToString("yyyy/MM/dd HH:mm:ss") & "

            ' HI.UL.ULDate.FormatDateDB & " " & HI.UL.ULDate.FormatTimeDB

            '_Str &= ",'" & _NameRTF & "'"
            ' _Str &= ",@FTMailText "
            ' _Str &= ",'0','" & Now.Date.ToString("yyyy/MM/dd") & "',1)"



            ' FTMailText.SaveDocument(_Path & "SavedDocument.rtf", DocumentFormat.Rtf)
            ' Dim fi As New System.IO.FileInfo("SavedDocument.rtf")
            ' Dim msg As String = String.Format("The size of the file is {0:#,#} bytes.", fi.Length.ToString("#,#"))
            ' MessageBox.Show(msg)


            'data = HI.UL.ULImage.ConvertImageToByteArray(Me.FTMailText.Text, UL.ULImage.PicType.Nothing)
            'Dim p As New SqlParameter("@FTMailText", WriteToStream(FTMailText.Text))
            'p.Value = data
            'HI.Conn.SQLConn.Cmd.Parameters.Add(p)


            ' _Str &= "@FTMailText "

            'HI.Conn.SQLConn.Cmd.Parameters.AddWithValue("@FTMailText", WriteToStream(FTMailText.Document.Text))

            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            ' ทำการ Upload File

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '  FTMailText.SaveDocument(_Path & _NameRTF, DocumentFormat.Rtf)

            Try
                If sFileName.Trim <> String.Empty Then

                    Dim sSource = sPathServer & sFileName
                    File.Copy(sPathFileName, sSource, True)

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'varbinary(MAX)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Private Sub FTMailText_BeforeExport(sender As Object, e As BeforeExportEventArgs)

    '    Dim checkDocument As DocumentExportCapabilities = FTMailText.Document.RequiredExportCapabilities
    '    If (e.DocumentFormat = DocumentFormat.Rtf) AndAlso checkDocument.InlinePictures Then
    '        ' Save เก็บรูปไว้ด้วย
    '        Dim options As RtfDocumentExporterOptions = TryCast(e.Options, RtfDocumentExporterOptions)
    '        If options IsNot Nothing Then
    '            options.Compatibility.DuplicateObjectAsMetafile = True
    '        End If
    '    End If

    'End Sub


    Private Sub CreateTabel_Attach()
        _DTAttach = New DataTable
        _DTAttach.Columns.Add("FTMailId", GetType(Object))
        _DTAttach.Columns.Add("FNMailFileAttach", GetType(Object))
    End Sub



    Private Sub NewRowFileAttach()
        Try
           
            Dim oDataRow As DataRow

            oDataRow = _DTAttach.NewRow()

            oDataRow.Item("FTMailId") = ""
            oDataRow.Item("FNMailFileAttach") = sFileName

            _DTAttach.Rows.Add(oDataRow)

            ogcTMAILFileAttach.DataSource = _DTAttach
            ogcTMAILFileAttach.Refresh()
            ogvTMAILFileAttach.RefreshData()

        Catch ex As Exception
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

    End Sub





#Region "Function"
    Public Function WriteToStream(ByVal strText As String) As MemoryStream

        Dim ms As New MemoryStream
        Try
            ' Dim strText As String = "This is some sample text sentence that i am adding in this variable. I can also " & _
            '                       "get this variable from some other source like a file or a network socket or whatever"

            Dim enc As New UTF8Encoding
            Dim arrBytData() As Byte = enc.GetBytes(strText)
            ms.Write(arrBytData, 0, arrBytData.Length)

            'The stream contains the data in mamory, in binary form.
            'You can utilize the stream for further operations here
            Return ms

        Catch ex As Exception
            Return Nothing
        Finally
            ms.Close()
            ms = Nothing

        End Try

    End Function

    Public Sub WriteToStream2()
        Dim ms As New MemoryStream

        Dim strText As String = "This is some sample text sentence that i am adding in this variable." & _
                                " I can also get this variable from some other source like a file or a network socket or whatever"

        Dim enc As New UTF8Encoding
        Dim arrBytData() As Byte = enc.GetBytes(strText)
        ms.Write(arrBytData, 0, arrBytData.Length)

        'The stream contains the data in mamory, in binary form.
        'You can utilize the stream for further operations here
        Return
        ms.Close()
        ms = Nothing

    End Sub


    Public Function ReadStream(ByVal memStream As MemoryStream) As String
        ' Reset the stream otherwise you will just get an empty string.
        ' Remember the position so we can restore it later.
        Dim pos = memStream.Position
        memStream.Position = 0

        Dim reader As New StreamReader(memStream)
        Dim str = reader.ReadToEnd()

        ' Reset the position so that subsequent writes are correct.
        memStream.Position = pos

        Return str
    End Function



    Private Sub SetDataGrid()

        ogvTMAILFileAttach.Columns.ColumnByName("FNMailFileAttach").Width = 60


    End Sub

    'Public Sub convert()
    '    Dim range As DocumentRange
    '    Using model = New DocumentModel()
    '        Dim rtfConverter = New StringEditValueToDocumentModelConverter(DocumentFormat.PlainText, Encoding.[Default])
    '        Dim stringConverter = New DocumentModelToStringConverter(DocumentFormat.Rtf, Encoding.[Default])
    '        rtfConverter.ConvertToDocumentModel(model, "Some text")

    '        'model.DefaultCharacterProperties.FontSize = 14;
    '        'model.DefaultCharacterProperties.FontBold = true;
    '        'model.DefaultCharacterProperties.FontName = "Arial";

    '        Dim rtfSomeText As String = TryCast(stringConverter.ConvertToEditValue(model), String)
    '        range = FTMailText.Document.InsertRtfText(FTMailText.Document.Range.[End], rtfSomeText)
    '    End Using
    '    Dim props As DevExpress.XtraRichEdit.API.Native.CharacterProperties = FTMailText.Document.BeginUpdateCharacters(range)
    '    props.Bold = True
    '    props.FontSize = 14
    '    props.FontName = "Arial"
    '    FTMailText.Document.EndUpdateCharacters(props)
    'End Sub

    Public Function ConvertImage(ByVal myImage As Image) As Byte()

        Dim mstream As New MemoryStream
        myImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

        Dim myBytes(mstream.Length - 1) As Byte
        mstream.Position = 0

        mstream.Read(myBytes, 0, mstream.Length)

        Return myBytes

    End Function


    'Private Sub Show_FNMailStatePriority()


    '    Dim Str As String = " SELECT FOODID, FOODTYPEID, TYPENAME, FOODSTATUS, FOODREMARK, LUPDATE FROM FOODTYPE where FOODSTATUS = 1  order by  FOODTYPEID"

    '    Dim dtShopType As DataTable = ClsShopType.Select_Data(Str)

    '    If (dtShopType Is Nothing) Then

    '        FNMailStatePriority.DataSource = Nothing : Cbo_ShopType.Items.Clear()
    '        Exit Sub

    '    End If

    '    FNMailStatePriority.DataSource = Nothing
    '    FNMailStatePriority.DataSource = dtShopType

    '    FNMailStatePriority.DisplayMember = "TYPENAME"
    '    FNMailStatePriority.ValueMember = "FOODTYPEID"


    'End Sub

    Private Sub Open_AttachFile()

        Dim sSlash As Integer

        With OpenFileDialog
            .Title = "Attach File"
            ' .InitialDirectory = "D:\"
            .FileName = ""
            .Multiselect = False
            .FilterIndex = 0
            .Filter = "All File|*.*"   '"Word File|*.doc|Text File|*.txt|All File|*.*"
        End With

        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            sSlash = InStrRev(OpenFileDialog.FileName, "\")
            sFilePath = Mid(OpenFileDialog.FileName, 1, sSlash)
            sFileName = Mid(OpenFileDialog.FileName, sSlash + 1, Len(OpenFileDialog.FileName))

            sPathFileName = OpenFileDialog.FileName

            wMailNew2.Data_sPathFileName = sPathFileName
            wMailNew2.Data_sFileName = sFileName
        Else

            wMailNew2.Data_sPathFileName = String.Empty
            wMailNew2.Data_sFileName = String.Empty
        End If

    End Sub





    Private Function GetFileSize(ByVal Myfilepath As String) As Long  '  หาขนาดfile bytes
        Dim MyFile As New FileInfo(Myfilepath)
        Dim FileSize As Long = Math.Ceiling((MyFile.Length / 1024 * 1024))
        Return FileSize
    End Function


#End Region


    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim StrFileOutput As String = String.Empty

        Call Open_AttachFile()


        ' If (sFileName = String.Empty) Then
        ' MessageBox.Show("กรุณาเลือก file Excle", "Message 369", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ' txtpathfileExcel.Text = String.Empty
        '  Exit Sub
        '  End If
        'Dim HyperLink As HyperLinkEdit

        'If sFileName <> String.Empty Then
        '    HyperLink = New HyperLinkEdit
        '    HyperLink.Name = sFileName
        '    '   FNMailFileAttach.add(HyperLink.Name)
        'End If

        ' ogvTMAILFileAttach.AddNewRow.item("")

        If sFileName <> String.Empty Then
            Call NewRowFileAttach()

        End If




        ' FNMailFileAttach.Text = sFileName

    End Sub

   

    Private Sub wMailNew2_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim L As Long
        'L = GetFileSize("E:\Project_Hi_Tuk\Work_tuk\RobinsonFinger\Finger_FTP\Finger_FTP\bin\Debug\CONFIG.xml")


        'Dim fi As New System.IO.FileInfo("E:\Project_Hi_Tuk\Work_tuk\RobinsonFinger\Finger_FTP\Finger_FTP\bin\Debug\CONFIG.xml")
        'Dim msg As String = String.Format("The size of the file is {0:#,#} bytes.", fi.Length.ToString("#,#"))
        'MessageBox.Show(L & "               " & msg)


        With ogvTMAILFileAttach
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        Call CreateTabel_Attach()


        Select Case wMailMain.Data_FormState
            Case 2   ' Load
                FTMailTo.Text = String.Empty
                FTMailSubject.Text = String.Empty
                FNMailStatePriority.SelectedIndex = 0
                FNMailStatePriority.Enabled = True

            Case 3  ' Reply

                If wMailMain.Data_FTMailFrom = String.Empty Then
                    Exit Sub
                End If

                FTMailTo.Text = wMailMain.Data_FTMailTo

                FTMailText2.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ


                FTMailSubject.Text = "RE : " & wMailMain.Data_FTMailSubject
                FNMailStatePriority.SelectedIndex = wMailMain.Data_FNMailStatePriority
                FNMailStatePriority.Enabled = False
                FNMailStatePriority.BackColor = Color.White

                _FTMailId_Old = wMailMain.Data_FTMailId

                Call LoadogcTMAILFileAttach(wMailMain.Data_FTMailId)



                Dim pos As DocumentPosition = FTMailText2.Document.CaretPosition
                FTMailText2.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ
                FTMailText2.Text = vbCrLf & vbCrLf & _
                                   "____________________________________________________" & vbCrLf & _
                                  "From :   " & wMailMain.Data_FTMailFrom & vbCrLf & _
                                  "Sent :   " & wMailMain.Data_FDInsDate & " " & wMailMain.Data_FTInsTime & vbCrLf & _
                                  "To :   " & wMailMain.Data_FTMailTo & vbCrLf & _
                                  "Subject :   " & wMailMain.Data_FTMailSubject & vbCrLf & _
                                  vbCrLf & wMailMain.Data_FTMailText2 & vbCrLf





            Case 5  ' Forword

                If wMailMain.Data_FTMailFrom = String.Empty Then
                    Exit Sub
                End If

                ' FTMailTo.Text = String.Empty

                FTMailText2.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ

                FTMailTo.Text = String.Empty
                FTMailSubject.Text = wMailMain.Data_FTMailSubject
                FNMailStatePriority.SelectedIndex = wMailMain.Data_FNMailStatePriority
                FNMailStatePriority.Enabled = False
                FNMailStatePriority.BackColor = Color.White


                _FTMailId_Old = wMailMain.Data_FTMailId
                Call LoadogcTMAILFileAttach(wMailMain.Data_FTMailId)



                Dim pos As DocumentPosition = FTMailText2.Document.CaretPosition
                FTMailText2.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ
                FTMailText2.Text = vbCrLf & vbCrLf & _
                                   "____________________________________________________" & vbCrLf & _
                                  "From :   " & wMailMain.Data_FTMailFrom & vbCrLf & _
                                  "Sent :   " & wMailMain.Data_FDInsDate & " " & wMailMain.Data_FTInsTime & vbCrLf & _
                                  "To :   " & wMailMain.Data_FTMailTo & vbCrLf & _
                                  "Subject :   " & wMailMain.Data_FTMailSubject & vbCrLf & _
                                  vbCrLf & wMailMain.Data_FTMailText2 & vbCrLf

                FTMailText2.Focus()



        End Select







    End Sub


    Private Sub LoadogcTMAILFileAttach(ByVal TempData As String)
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = " SELECT FTMailId,"
            _str &= "isnull(FNMailFileAttach,'') as FNMailFileAttach "
            _str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILFileAttach AS A WITH(NOLOCK)"
            _str &= " Where FTMailId = " & TempData

            _dt = HI.Conn.SQLConn.GetDataTable(_str, Conn.DB.DataBaseName.DB_MAIL)

            Me.ogcTMAILFileAttach.DataSource = _dt

            If _dt.Rows.Count > 0 Then
                Dim view As GridView
                view = ogcTMAILFileAttach.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                view.BestFitColumns()

                Me.ogcTMAILFileAttach = view.GridControl
                Me.ogcTMAILFileAttach.Refresh()

                ' Call SetDataGrid2()

                _dt.Dispose()
            Else
                Me.ogcTMAILFileAttach.DataSource = Nothing
                Dim view As GridView
                view = ogcTMAILFileAttach.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                view.BestFitColumns()

                Me.ogcTMAILFileAttach = view.GridControl
                Me.ogcTMAILFileAttach.Refresh()

                ' Call SetDataGrid2()

                _dt.Dispose()


            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub HyperLinkEdit1_OpenLink(sender As Object, e As DevExpress.XtraEditors.Controls.OpenLinkEventArgs)

        Try

            Dim _path As String = "E:\Project_Hi_Tuk\TMAILMessages.xlsx"


            'Shell(sPathServer & sFileName, AppWinStyle.NormalFocus)


            ' Shell("E:\Project_Hi_Tuk\VBNet\VBNet\bin\Debug\vbnet.exe", AppWinStyle.NormalFocus)

            Select Case Path.GetExtension(_path)   'ตรวจสอบนามสกุล เอารูปมาใส่ที่หลัง
                Case ".txt"
                    MessageBox.Show("1111")
                Case ".bmp"
                    MessageBox.Show("2222")
                Case ".xlsx"
                    MessageBox.Show("3333")

            End Select



            Process.Start(_path)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub



    ' My.Computer.FileSystem.ReadAllBytes("C:/Documents and Settings/selfportrait.jpg")

    ' Usage
    '  Dim value As Byte() = My.Computer.FileSystem.ReadAllBytes(File)



   

    Private Sub ogcTMAILFileAttach_Click(sender As Object, e As EventArgs) Handles ogcTMAILFileAttach.Click

    End Sub

   
    Private Sub ogvTMAILFileAttach_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvTMAILFileAttach.KeyDown
        With Me.ogvTMAILFileAttach
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Select Case e.KeyCode

                Case Windows.Forms.Keys.Delete
                    With ogvTMAILFileAttach
                        If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                    End With

                    Call ogvTMAILFileAttach.DeleteRow(ogvTMAILFileAttach.FocusedRowHandle)
                    CType(Me.ogcTMAILFileAttach.DataSource, DataTable).AcceptChanges()

                Case Windows.Forms.Keys.Enter

                    Dim PathAttach As String = String.Empty
                    PathAttach = sPathServer & .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                    If File.Exists(PathAttach) Then   ' ถ้ามีอยู่แล้วว ลบ File .dat ทิ้งก่อน แล้วค่อยเพิ่ม
                        'File.Delete(PathAttach)
                        Process.Start(PathAttach)

                    End If
            End Select

        End With
    End Sub

    Private Sub FTMailTo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FTMailTo.KeyPress
        If (e.KeyChar = "'") Or (e.KeyChar = "\") Then
            e.Handled = True
        End If
    End Sub

    Private Sub FTMailSubject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FTMailSubject.KeyPress
        If (e.KeyChar = "'") Or (e.KeyChar = "\") Then
            e.Handled = True
        End If
    End Sub


    Private Sub FTMailText2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FTMailText2.KeyPress
        If (e.KeyChar = "'") Or (e.KeyChar = "\") Then
            e.Handled = True
        End If
    End Sub
End Class
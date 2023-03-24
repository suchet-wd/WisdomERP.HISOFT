Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Pdf
Imports DevExpress.XtraEditors
Imports System.Drawing

Public Class wUseDocTracking
    Private _ProcLoad As Boolean = False
    Private _ReadDocFile As wReadDocFile
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub wUseDoc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.FNHSysCmpId.Properties.Tag = HI.ST.SysInfo.CmpID
        Catch ex As Exception
        End Try
    End Sub

    Private _Pdfdata As Byte()
    Private Sub LoadDetail()
        Dim _Spls As New HI.TL.SplashScreen("Loading Document... Please Wait... ")
        Try
            Dim _Cmd As String = "" : Dim _FileType As Integer = 0
            _Cmd = "Select Top 1  FNFileType "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle WITH(NOLOCK)"
            '_Cmd &= vbCrLf & " WHERE FNHSysDocNameId=" & Integer.Parse(Me.FNHSysDocNameId.Properties.Tag)
            _Cmd &= vbCrLf & " ORDER BY FDDocumentDate DESC "
            _FileType = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_DOC, 0)

            _Cmd = "Select FBDocument From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument WITH(NOLOCK) "
            '_Cmd &= vbCrLf & " WHERE FNHSysDocNameId=" & Integer.Parse(Me.FNHSysDocNameId.Properties.Tag)
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
            Next
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        'If Me.FNHSysDocNameId.Text <> "" Then
        '  Call LoadDetail()
        Call CreateLayout()
        'End If
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
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd = "Select  [dbo].[FN_GET_YMD](FDManagerAppDate,convert(varchar(10),getdate(),111),'TH') AS FDDocAge "
        Else
            _Cmd = "Select  [dbo].[FN_GET_YMD](FDManagerAppDate,convert(varchar(10),getdate(),111),'EN') AS FDDocAge "
        End If
        _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle  WITH(NOLOCK) "
        '_Cmd &= vbCrLf & "WHERE FNHSysDocNameId=" & Integer.Parse("0" & Me.FNHSysDocNameId.Properties.Tag)
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

    Private Sub FNHSysDocNameId_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysDocNameId_EditValueChanged), New Object() {sender, e})
            Else
                'Me.FDDocAge.Text = ""
                'If FNHSysDocNameId.Text <> "" Then
                '    Dim _Qry As String = "SELECT TOP 1 FNHSysDocNameId  FROM  TDOCMDocumentTitle WITH(NOLOCK) WHERE FTDocNameCode ='" & HI.UL.ULF.rpQuoted(FNHSysDocNameId.Text) & "' "
                '    FNHSysDocNameId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                '    Call LoadDataInfo(Me.FNHSysDocNameId.Text)
                'Else
                'End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#Region "Title Group"
    Private Function LoadData() As System.Data.DataTable
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
            _Cmd &= vbCrLf & " Group by D.FTDocumentNo   "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)

            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub CreateLayout()
        Try
            Dim _oDt As DataTable = LoadData()

            Dim _TileGroup As New DevExpress.XtraEditors.TileGroup
            _TileGroup.Text = "222222"

            TileControl.Groups.Add(_TileGroup)
            Dim _seq As Integer = 0

            For Each item As DataRow In _oDt.Rows
                _seq += +1
                Dim _i As New DevExpress.XtraEditors.TileItem
                _i.AllowAnimation = True
                _i.BackgroundImageScaleMode = TileItemImageScaleMode.ZoomInside
                _i.AppearanceItem.Normal.BackColor = Color.DodgerBlue
                _i.AppearanceItem.Normal.BackColor2 = Color.LightBlue
                _i.AppearanceItem.Normal.BorderColor = Color.White
                _i.AppearanceItem.Normal.ForeColor = Color.Black
                _i.AppearanceItem.Normal.Font = New Font("Tahoma", 8, FontStyle.Bold)
                _i.ItemSize = TileItemSize.Large
                _i.ContentAnimation = TileItemContentAnimationType.ScrollLeft

                _i.Name = item.Item("FTDocumentNo").ToString
                _i.Text = item.Item("FTDocName").ToString
                _i.Id = _seq ' item.Item("FTDocumentNo").ToString

                Dim Elmt As DevExpress.XtraEditors.TileItemElement
                Elmt = New DevExpress.XtraEditors.TileItemElement
                '   Elmt.Text = R!FTQADetailCode.ToString
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    Elmt.Text = "ครั้งที่แก้ไข " & item.Item("FNReviseNo").ToString
                Else
                    Elmt.Text = "Revise " & item.Item("FNReviseNo").ToString
                End If

                Elmt.TextAlignment = TileItemContentAlignment.BottomLeft

                _i.Elements.Add(Elmt)

                _TileGroup.Items.Add(_i)
            Next

            AddHandler TileControl.ItemDoubleClick, AddressOf TileItem1_ItemClick
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TileItem1_ItemClick(sender As Object, e As TileItemEventArgs)
        'If Verify() Then
        '    If Me.FTPointName.Text <> "" Then
        '        subCreateLayout(e.Item.Id, e.Item.Text)
        '    Else

        _ReadDocFile = New wReadDocFile
        With _ReadDocFile
            .Key = e.Item.Name.ToString
            .DocName = e.Item.Text.ToString
            .ShowDialog()
        End With

        '    End If

        'End If

    End Sub

    Public Class SampleDataCommon
        Private subtitleCore, imagePathCore, descriptionCore, titleCore As String
        Public ReadOnly Property Title() As String
            Get
                Return titleCore
            End Get
        End Property
        Public ReadOnly Property Subtitle() As String
            Get
                Return subtitleCore
            End Get
        End Property
        Public ReadOnly Property ImagePath() As String
            Get
                Return imagePathCore
            End Get
        End Property
        Public ReadOnly Property Description() As String
            Get
                Return descriptionCore
            End Get
        End Property
        Public Sub New(ByVal title As String, ByVal subtitle As String, ByVal imagePath As String, ByVal description As String)
            titleCore = title
            subtitleCore = subtitle
            imagePathCore = imagePath
            descriptionCore = description
        End Sub
        Public Sub New()
        End Sub
    End Class

    Public Class SampleDataItem
        Inherits SampleDataCommon
        Private contentCore, groupNameCore As String
        Private _id As Integer
        Public Sub New(ByVal title As String, ByVal subtitle As String, ByVal imagePath As String, ByVal description As String, ByVal content As String, ByVal groupName As String, ByVal _codeId As Integer)
            MyBase.New(title, subtitle, imagePath, description)
            contentCore = content
            groupNameCore = groupName
            _id = _codeId
        End Sub
        Public ReadOnly Property Id() As Integer
            Get
                Return _id
            End Get
        End Property
        Public ReadOnly Property Content() As String
            Get
                Return contentCore
            End Get
        End Property
        Public ReadOnly Property GroupName() As String
            Get
                Return groupNameCore
            End Get
        End Property
    End Class

#End Region
End Class
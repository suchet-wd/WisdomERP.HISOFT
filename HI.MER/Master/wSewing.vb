Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Pdf

Public Class wSewing

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As System.Data.DataTable
    Private _ProcLoad As Boolean = False
    ' Private _AddItemPopup As wMailPopup
    Private _FilePath As String
    Private _CopySewing As wCopySewing

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        '_AddItemPopup = New wMailPopup
        'HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        'Dim oSysLang As New ST.SysLanguage
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        'Catch ex As Exception
        'Finally
        'End Try
        _CopySewing = New wCopySewing
        HI.TL.HandlerControl.AddHandlerObj(_CopySewing)
        Dim oSysLang As New HI.ST.SysLanguage
        'Call HI.ST.Lang.InsertLanguage(_CopyStyle)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopySewing.Name.ToString.Trim, _CopySewing)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_CopySewing)

        'Call InitFormControl()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Property"

    Private _WHID As Integer
    Public Property WH As Integer
        Get
            Return _WHID
        End Get
        Set(value As Integer)
            _WHID = value
        End Set
    End Property

    Private _WHIDTo As Integer
    Public Property WHTo As Integer
        Get
            Return _WHIDTo
        End Get
        Set(value As Integer)
            _WHIDTo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _OrderNoTo As String = ""
    Public Property OrderNoTo As String
        Get
            Return _OrderNoTo
        End Get
        Set(value As String)
            _OrderNoTo = value
        End Set
    End Property

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _DocRefNo As String = ""
    Public Property DocRefNo As String
        Get
            Return _DocRefNo
        End Get
        Set(value As String)
            _DocRefNo = value
        End Set
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(value As String)
            _MainKeyID = value
        End Set
    End Property

    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
    End Property

    Private _RequireField As String = ""
    Public Property RequireField As String
        Get
            Return _RequireField
        End Get
        Set(value As String)
            _RequireField = value
        End Set
    End Property

    Public ReadOnly Property Query As String
        Get
            Return _FormHeader(0).Query
        End Get
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region

#Region "Procedure"


    Private Function SaveData1() As Boolean

        Dim dt As DataTable
        Dim _StateNew As Boolean = False
        Dim _dataBinary As Byte()

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
       



            _Str = "SELECT TOP 1 FNHSysStyleId "
            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing AS A WITH(NOLOCK)"
            _Str &= vbCrLf & " WHERE FNHSysStyleId='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "'"
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "") = "")

            Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _season As String = HI.Conn.SQLConn.GetField("SELECT FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WHERE FTSeasonCode='" & Me.FNHSysSeasonId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            Dim _File As String = ""
            _File = System.IO.Path.GetExtension(_FilePath)

            ' For Each R As DataRow In dt.Select("FNMonthSeq>0 ", "FNMonthSeq")
            If (_StateNew) Then
                If _File = "" Then
                    _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing "
                    _Str &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    '_Str &= vbCrLf & ",FTFileExtention='" & _File & "'"
                    _Str &= vbCrLf & ", FNHSysSeasonId='" & _season & "'"
                    _Str &= vbCrLf & "WHERE FNHSysStyleId= '" & _style & "'"
                Else
                    _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing "
                    _Str &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Str &= vbCrLf & ",FTFileExtention='" & _File & "'"
                    _Str &= vbCrLf & ", FNHSysSeasonId='" & _season & "'"
                    _Str &= vbCrLf & "WHERE FNHSysStyleId= '" & _style & "'"

                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing  "
                    _Str &= "( FTInsUser, FDInsDate, FTInsTime,FNHSysStyleId,FTFileExtention,FNHSysSeasonId)"
                    _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Str &= vbCrLf & ",'" & _style & "'"
                    _Str &= vbCrLf & ",'" & _File & "'"
                    _Str &= vbCrLf & ",'" & _season & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If
                If _File <> "" Then
                    Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                    _dataBinary = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
                    If Not (_dataBinary Is Nothing) Then
                        Dim _cmd As String = ""
                        _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing"
                        _cmd &= " Set  FBDocument=@FBDocument"
                        _cmd &= " WHERE FNHSysStyleId=@FNHSysStyleId"
                        Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                        cmd.Parameters.AddWithValue("@FNHSysStyleId", _style)
                        Dim _p As New SqlParameter("@FBDocument", SqlDbType.VarBinary)
                        _p.Value = _dataBinary
                        cmd.Parameters.Add(_p)
                        cmd.ExecuteNonQuery()

                    End If
                End If


            End If

            '  Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function



    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _Str As String
            Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing WHERE FNHSysStyleId='" & HI.UL.ULF.rpQuoted(_style) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing WHERE FNHSysStyleId='" & HI.UL.ULF.rpQuoted(_style) & "'")

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function


    Private Sub FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)
        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
    End Sub
#End Region
    Private Sub wImportDoc_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Try
        '    'Me.FNHSysStyleId.Text = HI.ST.SysInfo.CmpCode
        '    'Me.FNHSysStyleId.Properties.Tag = HI.ST.SysInfo.CmpID

        '    'Call TabChanged()
        'Catch ex As Exception
        'End Try
        Call LoadDetail()
    End Sub

    Private Sub Readfile()
        Try
            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)
                Select Case _FileType.ToUpper
                    Case ".XLSX".ToUpper, ".XLS".ToUpper
                        Call _ExcelViewer(_FilePath)
                    Case ".PDF".ToUpper
                        Call _PDFViewer(_FilePath)
                    Case Else
                        HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
                        Exit Sub
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _PDFViewer(_FileName As String)
        Try
            Me.oGrpdetail.Controls.Clear()
            Dim _Pdfv As New PdfViewer
            _Pdfv.Dock = DockStyle.Fill
            _Pdfv.LoadDocument(_FileName)
            Me.oGrpdetail.Controls.Add(_Pdfv)
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

    Private _Pdfdata As Byte()
    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) = True Then
                If DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
                    Me.FNHSysStyleId_None.Text = ""
                    Me.FNHSysSeasonId_None.Text = ""
                    Me.FNHSysSeasonId.Text = ""
                    Me.oGrpdetail.Controls.Clear()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            Me.FNHSysStyleId_None.Text = ""
            Me.FNHSysSeasonId_None.Text = ""
            Me.FNHSysSeasonId.Text = ""
            Me.oGrpdetail.Controls.Clear()
            Call LoadDetail()
        Catch ex As Exception
        End Try

    End Sub


    Private Sub ocmsavedocument_Click(sender As Object, e As EventArgs) Handles ocmsavedocument.Click
        If VerifyData() Then


            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Saving data...   Please Wait  ")
                If Me.SaveData1() Then

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If


            End If

        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

    End Sub
    Private Function VerifyData() As Boolean
        If Me.FNHSysStyleId.Text <> "" Then
            Return True
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุแบบผลิตภัณฑ์ !!!", 1512130570, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function


    Private Sub ocmReadDocumentfile_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg

                '         .Filter = "PDF files |*.pdf"

                '    .Filter = "Text files |*.txt"

                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx|PDF files |*.pdf"

                ' .Filter = "Word Documents(97-2003)|*.doc|Word Documents(2010-2013)|*.docx"


                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    _FilePath = .FileName
                    Call Readfile()
                Else
                    _FilePath = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        'Call LoadDetail()
        'Me.FNHSysSeasonId = Me.FNHSysSeasonId
    End Sub

    Private Sub LoadDetail()
        Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        Dim _FE As String = HI.Conn.SQLConn.GetField("SELECT FTFileExtention FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing WHERE FNHSysStyleId='" & _style & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        Dim _season As String = HI.Conn.SQLConn.GetField("SELECT FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WHERE FTSeasonCode='" & Me.FNHSysSeasonId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

        Dim _Cmd As String = ""
        _Cmd = "Select  S.FTStyleCode as FNHSysStyleId ,SS.FBDocument,Se.FTSeasonCode as FNHSysSeasonId "
        _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing as SS INNER JOIN"
        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SS.FNHSysStyleId=S.FNHSysStyleId INNER JOIN"
        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS Se ON SS.FNHSysSeasonId=Se.FNHSysSeasonId"
        _Cmd &= vbCrLf & " Where SS.FNHSysStyleId='" & _style & "'"
        If Me.FNHSysSeasonId.Text <> "" Then
            _Cmd &= vbCrLf & " AND SS.FNHSysSeasonId ='" & _season & "' "
        End If
        Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)
       

        For Each R As DataRow In _oDt.Rows
        
            If _FE = ".xls" Then
                Me.oGrpdetail.Controls.Clear()
                Dim _ss As String = ""
                Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                With _XlsV
                    .ReadOnly = True
                    .Dock = DockStyle.Fill
                    .Unit = DevExpress.Office.DocumentUnit.Inch
                    .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                    .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)
                End With
                Me.oGrpdetail.Controls.Add(_XlsV)
                '_ss = R!FNHSysSeasonId.ToString
                'Me.FNHSysSeasonId.Text = _ss
            Else
                Try
                    Me.oGrpdetail.Controls.Clear()
                    Dim _Pdfv As New PdfViewer
                    Dim _ss As String = ""
                    With _Pdfv
                        .Dock = DockStyle.Fill
                        _Pdfdata = CType(R!FBDocument, Byte())
                        .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())))
                    End With
                    Me.oGrpdetail.Controls.Add(_Pdfv)
                    '_ss = R!FNHSysSeasonId.ToString
                    'Me.FNHSysSeasonId.Text = _ss
                Catch ex As Exception
                End Try
            End If
        Next
        
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click

        If Me.FNHSysStyleId.Text <> "" Then
            If "" & Me.FNHSysStyleId.Properties.Tag.ToString <> "" Then
                If FNHSysSeasonId.Text <> "" Then

                    If FNHSysSeasonId.Properties.Tag.ToString <> "" Then
                        Call HI.ST.Lang.SP_SETxLanguage(_CopySewing)
                        Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        Dim _SSS As String = HI.Conn.SQLConn.GetField("SELECT FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FNHSysStyleId='" & _style & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        With _CopySewing
                            .FNHSysStyleId.Text = _SSS
                            .FNHSysSeasonIdS.Text = FNHSysSeasonId.Text.Trim
                            .FNHSysSeasonIdS_None.Text = FNHSysSeasonId_None.Text
                            .FNHSysSeasonIdS.Properties.Tag = FNHSysSeasonId.Properties.Tag.ToString
                            .FNHSysStyleId2.Text = ""
                            .FNHSysSeasonId.Text = ""
                            .ProcComplete = False
                            .ShowDialog()

                            If (.ProcComplete) Then
                                Call SaveData()

                            End If

                        End With

                    End If

                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
                FNHSysStyleId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
        End If
    End Sub

    Private Function SaveData() As Boolean

        Dim _dataBinary As Byte()

       
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String

            'Dim _File As String = ""
            '_File = System.IO.Path.GetExtension(_FilePath)

            Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _FE As String = HI.Conn.SQLConn.GetField("SELECT FTFileExtention FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing WHERE FNHSysStyleId='" & _style & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _FB As String = HI.Conn.SQLConn.GetField("SELECT FBDocument FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing WHERE FNHSysStyleId='" & _style & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")



            Dim _Cm As String = ""
            _Cm = "Select  S.FTStyleCode,SS.FBDocument,Se.FTSeasonCode as FNHSysSeasonId "
            _Cm &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing as SS INNER JOIN"
            _Cm &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SS.FNHSysStyleId=S.FNHSysStyleId INNER JOIN"
            _Cm &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS Se ON SS.FNHSysSeasonId=Se.FNHSysSeasonId"
            _Cm &= vbCrLf & " Where SS.FNHSysStyleId='" & _style & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cm, Conn.DB.DataBaseName.DB_MERCHAN)
            For Each R As DataRow In _oDt.Rows
                _dataBinary = R!FBDocument
            Next


            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing  "
            _Str &= "( FTInsUser, FDInsDate, FTInsTime,FNHSysStyleId,FTFileExtention,FNHSysSeasonId)"
            _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Str &= vbCrLf & ",'" & _CopySewing.FNHSysStyleId2.Properties.Tag.ToString & "'"
            _Str &= vbCrLf & ",'" & _FE & "'"
            _Str &= vbCrLf & ",'" & _CopySewing.FNHSysSeasonId.Properties.Tag.ToString & "'"
            '_Str &= vbCrLf & ",'" & _FB & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            'If _File <> "" Then
            'Dim br As New BinaryReader(New FileStream(_FB, FileMode.Open, FileAccess.Read))
            '_dataBinary = br.ReadBytes(CInt(New FileInfo(_FB).Length))
            If Not (_dataBinary Is Nothing) Then
                Dim _cmd As String = ""
                _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing"
                _cmd &= " Set  FBDocument=@FBDocument"
                _cmd &= " WHERE FNHSysStyleId=@FNHSysStyleId"
                Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                cmd.Parameters.AddWithValue("@FNHSysStyleId", _CopySewing.FNHSysStyleId2.Properties.Tag.ToString)
                Dim _p As New SqlParameter("@FBDocument", SqlDbType.VarBinary)
                _p.Value = _dataBinary
                cmd.Parameters.Add(_p)
                cmd.ExecuteNonQuery()

            End If
            'End If


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '  HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub FNHSysSeasonId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSeasonId.EditValueChanged

        Dim _season As String = HI.Conn.SQLConn.GetField("SELECT FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WHERE FTSeasonCode='" & Me.FNHSysSeasonId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        Dim _FB As String = HI.Conn.SQLConn.GetField("SELECT FBDocument FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing WHERE FNHSysStyleId='" & _style & "' and FNHSysSeasonId ='" & _season & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

        'Me.FNHSysSeasonId.Text = Me.FNHSysSeasonId.Text

        Call LoadDetail()
  
      
        If _FB = "" Then
            Me.oGrpdetail.Controls.Clear()
        End If


    End Sub


End Class
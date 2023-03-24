Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Collections
Imports DevExpress
Imports DevExpress.XtraEditors
Imports System.Drawing



Public Class wPacking

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    ' Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _oDtPacking As DataTable
    Private _FilePath As String
    Private sSQL As String
    Private _CopyPacking As wCopyPacking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _CopyPacking = New wCopyPacking
        HI.TL.HandlerControl.AddHandlerObj(_CopyPacking)
        Dim oSysLang As New HI.ST.SysLanguage
        'Call HI.ST.Lang.InsertLanguage(_CopyStyle)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyPacking.Name.ToString.Trim, _CopyPacking)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_CopyPacking)
  
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

    Private Sub LoadDetail()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable
            Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _season As String = HI.Conn.SQLConn.GetField("SELECT FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WHERE FTSeasonCode='" & Me.FNHSysSeasonId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            _Qry = "SELECT   S.FTStyleCode as FNHSysStyleId ,SS.FNPackSeq,SS.FTPackDescription,SS.FTPackNote,SS.FBImage,Se.FTSeasonCode as FNHSysSeasonId"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing  as SS "
            _Qry &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SS.FNHSysStyleId=S.FNHSysStyleId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS Se ON SS.FNHSysSeasonId=Se.FNHSysSeasonId"
            _Qry &= vbCrLf & " WHERE S.FNHSysStyleId ='" & _style & "' "
            If Me.FNHSysSeasonId.Text <> "" Then
                _Qry &= vbCrLf & " AND SS.FNHSysSeasonId ='" & _season & "' "
            End If

                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


                If _oDt.Rows.Count = 0 Then
                    With _oDt
                        .Rows.Add()
                        .AcceptChanges()
                        For Each R As DataRow In .Rows
                            Dim _ss As String = ""
                            R!FNPackSeq = 1
                        '_ss = R!FNHSysSeasonId.ToString
                        'Me.FNHSysSeasonId.Text = _ss
                        Next
                    End With

                End If
                Me.ogcdetail.DataSource = _oDt

        Catch ex As Exception
        End Try
    End Sub

    Private Sub wPacking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadDetail()
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Down
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        If .FocusedColumn.FieldName.ToString <> "FBImage" Then
                            Exit Sub
                        End If
                        Dim x As Integer = 0
                        If .GetRowCellValue(.FocusedRowHandle, "FTPackDescription").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FTPackNote").ToString <> "" Or _
                             .GetRowCellValue(.FocusedRowHandle, "FBImage").ToString <> "" Then
                            With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                                .AcceptChanges()
                                If .Select("FTPackDescription='' or FTPackDescription Is null").Length <= 0 Then
                                    x = .Rows.Count + 1
                                    .Rows.Add(x)
                                    .Rows(x - 1).Item("FNPackSeq") = x
                                End If
                                .AcceptChanges()
                            End With
                            .FocusedRowHandle = x
                            .FocusedColumn = .Columns.ColumnByFieldName("FTPackDescription")
                        End If
                    End With
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        With CType(ogcdetail.DataSource, DataTable)
                            .AcceptChanges()
                            Dim x As Integer = 0
                            For Each r As DataRow In .Select("FNPackSeq<>0", "FNPackSeq")
                                x += +1
                                r!FNPackSeq = x
                               
                            Next
                            .AcceptChanges()
                        End With
                    End With

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
       
        If VerifyData() Then

            Call DeleteData()
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) = True Then

                If Me.SaveData1() Then

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Else
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
    Private Function SaveData1() As Boolean

        Dim dt As DataTable
        Dim _StateNew As Boolean = False
        Dim _dataBinary As Byte()
        Dim _DataPic As Byte()
        Dim path As String

        ' Dim _Spls As New HI.TL.SplashScreen("Saving data...   Please Wait  ")

        With CType(ogcdetail.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



            Dim _Str As String
            Dim _Seq As Integer = 0
            Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _pack As String = HI.Conn.SQLConn.GetField("SELECT  top 1 FNPackSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing WHERE FNHSysStyleId='" & _style & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _season As String = HI.Conn.SQLConn.GetField("SELECT FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WHERE FTSeasonCode='" & Me.FNHSysSeasonId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            _Str = "SELECT TOP 1 FNHSysStyleId "
            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Sewing AS A WITH(NOLOCK)"
            _Str &= vbCrLf & " WHERE FNHSysStyleId='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "'"
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "") = "")

            For Each R As DataRow In dt.Select("FTPackDescription <> ''  ", "FNPackSeq")


                _Seq += +1
                ' If _pack <> "" Then
                _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing "
                _Str &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",FTPackDescription='" & HI.UL.ULF.rpQuoted(R!FTPackDescription.ToString) & "'"
                _Str &= vbCrLf & ",FTPackNote='" & HI.UL.ULF.rpQuoted(R!FTPackNote.ToString) & "'"
                _Str &= vbCrLf & ", FNHSysSeasonId='" & _season & "'"
                _Str &= vbCrLf & "WHERE FNHSysStyleId= '" & _style & "'"
                _Str &= vbCrLf & "AND FNPackSeq=" & _Seq

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing  "
                    _Str &= "( FTInsUser, FDInsDate, FTInsTime,FNHSysStyleId,FNPackSeq,FTPackDescription,FTPackNote,FNHSysSeasonId)"
                    _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Str &= vbCrLf & ",'" & _style & "'"
                    _Str &= vbCrLf & "," & _Seq
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackDescription.ToString) & "'"
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNote.ToString) & "'"
                    _Str &= vbCrLf & ",'" & _season & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If R!FBImage.ToString <> "" Then
                        _dataBinary = R!FBImage
                        If Not (_dataBinary Is Nothing) Then
                            Dim _cmd As String = ""
                            _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing"
                            _cmd &= " Set  FBImage=@FBImage"
                            _cmd &= " WHERE FNHSysStyleId=@FNHSysStyleId"
                            _cmd &= "  AND FNPackSeq=@FNPackSeq"
                            Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                            cmd.Parameters.AddWithValue("@FNHSysStyleId", _style)
                            cmd.Parameters.AddWithValue("@FNPackSeq", _Seq)

                            Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)

                            For Each Obj As Object In Me.Controls.Find("FBImage", True)
                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.PictureEdit
                                        _dataBinary = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                                End Select
                            Next


                            Dim _p As New SqlParameter("@FBImage", SqlDbType.Image)
                            _p.Value = _dataBinary
                            cmd.Parameters.Add(_p)
                            cmd.ExecuteNonQuery()
                        End If
                    End If

                End If


                'For Each E As DataRow In dt.Select("FTPackDescription <> '' and FTPackNote <> '' ", "FNPackSeq")
                '    If E!FBImage.ToString <> "" And E!FNPackSeq.ToString <> "" Then
                '        _dataBinary = E!FBImage
                '        If Not (_dataBinary Is Nothing) Then
                '            Dim _cmd As String = ""
                '            _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing"
                '            _cmd &= " Set  FBImage=@FBImage"
                '            _cmd &= ",FNPackSeq=@FNPackSeq"
                '            _cmd &= " WHERE FNHSysStyleId=@FNHSysStyleId"
                '            _cmd &= "  AND FNPackSeq=@FNPackSeq"
                '            Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                '            cmd.Parameters.AddWithValue("@FNHSysStyleId", _style)
                '            cmd.Parameters.AddWithValue("@FNPackSeq", _Seq)
                '            Dim _p As New SqlParameter("@FBImage", SqlDbType.VarBinary)
                '            _p.Value = _dataBinary
                '            cmd.Parameters.Add(_p)
                '            cmd.ExecuteNonQuery()
                '        End If
                '    End If


                'Next



            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False

        End Try

    End Function

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            Me.ogcdetail.DataSource = Nothing
            Me.FNHSysStyleId_None.Text = ""
            Me.FNHSysSeasonId_None.Text = ""
            Me.FNHSysSeasonId.Text = ""
            Call LoadDetail()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) = True Then
                If DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
                    Me.FNHSysStyleId_None.Text = ""
                    Me.FNHSysSeasonId_None.Text = ""
                    Me.FNHSysSeasonId.Text = ""
                    Me.ogcdetail.Controls.Clear()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _Str As String
            Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _season As String = HI.Conn.SQLConn.GetField("SELECT FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WHERE FTSeasonCode='" & Me.FNHSysSeasonId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing WHERE FNHSysStyleId='" & HI.UL.ULF.rpQuoted(_style) & "' and FNHSysSeasonId ='" & HI.UL.ULF.rpQuoted(_season) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing WHERE FNHSysStyleId='" & HI.UL.ULF.rpQuoted(_style) & "' and  FNHSysSeasonId ='" & HI.UL.ULF.rpQuoted(_season) & "'")

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
    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        'Call LoadDetail()
        'Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        'Dim _SS As String = HI.Conn.SQLConn.GetField("SELECT FTSeasonCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  as S INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing  AS SP ON S.FNHSysSeasonId=SP.FNHSysSeasonId  WHERE SP.FNHSysStyleId='" & _style & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        'Me.FNHSysSeasonId.Text = _SS
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click

        If Me.FNHSysStyleId.Text <> "" Then
            If "" & Me.FNHSysStyleId.Properties.Tag.ToString <> "" Then
                If FNHSysSeasonId.Text <> "" Then

                    If FNHSysSeasonId.Properties.Tag.ToString <> "" Then
                        Call HI.ST.Lang.SP_SETxLanguage(_CopyPacking)
                        Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        Dim _SSS As String = HI.Conn.SQLConn.GetField("SELECT FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FNHSysStyleId='" & _style & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        With _CopyPacking
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

            Dim _Cm As String = ""
            _Cm = "Select  S.FTStyleCode,SS.FBImage,Se.FTSeasonCode as FNHSysSeasonId,SS.FNPackSeq ,SS.FTPackDescription,SS.FTPackNote"
            _Cm &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing as SS INNER JOIN"
            _Cm &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SS.FNHSysStyleId=S.FNHSysStyleId INNER JOIN"
            _Cm &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS Se ON SS.FNHSysSeasonId=Se.FNHSysSeasonId"
            _Cm &= vbCrLf & " Where SS.FNHSysStyleId='" & _style & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cm, Conn.DB.DataBaseName.DB_MERCHAN)
            For Each R As DataRow In _oDt.Rows
                _dataBinary = R!FBImage
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing  "
                _Str &= "(FTInsUser, FDInsDate, FTInsTime,FNHSysStyleId,FNPackSeq,FTPackDescription,FTPackNote,FNHSysSeasonId)"
                _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",'" & _CopyPacking.FNHSysStyleId2.Properties.Tag.ToString & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNPackSeq.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackDescription.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNote.ToString) & "'"
                _Str &= vbCrLf & ",'" & _CopyPacking.FNHSysSeasonId.Properties.Tag.ToString & "'"
                '_Str &= vbCrLf & ",'" & R!FBImage.ToString & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
                If R!FBImage.ToString <> "" Then
                    _dataBinary = R!FBImage
                    If Not (_dataBinary Is Nothing) Then
                        Dim _cmd As String = ""
                        _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing"
                        _cmd &= " Set  FBImage=@FBImage"
                        _cmd &= " WHERE FNHSysStyleId=@FNHSysStyleId"
                        _cmd &= "  AND FNPackSeq=@FNPackSeq"
                        Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                        cmd.Parameters.AddWithValue("@FNHSysStyleId", _CopyPacking.FNHSysStyleId2.Properties.Tag.ToString)
                        cmd.Parameters.AddWithValue("@FNPackSeq", R!FNPackSeq.ToString)
                        Dim _p As New SqlParameter("@FBImage", SqlDbType.VarBinary)
                        _p.Value = _dataBinary
                        cmd.Parameters.Add(_p)
                        cmd.ExecuteNonQuery()
                    End If
                End If

            Next

            'If _File <> "" Then
            '    Dim br As New BinaryReader(New FileStream(_FB, FileMode.Open, FileAccess.Read))
            '    _dataBinary = br.ReadBytes(CInt(New FileInfo(_FB).Length))

            'For Each E As DataRow In _oDt.Rows
            '    If E!FBImage.ToString <> "" Then
            '        _dataBinary = E!FBImage
            '        If Not (_dataBinary Is Nothing) Then
            '            Dim _cmd As String = ""
            '            _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing"
            '            _cmd &= " Set  FBImage=@FBImage"
            '            _cmd &= " WHERE FNHSysStyleId=@FNHSysStyleId"
            '            _cmd &= "  AND FNPackSeq=@FNPackSeq"
            '            Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            '            cmd.Parameters.AddWithValue("@FNHSysStyleId", _style)
            '            cmd.Parameters.AddWithValue("@FNPackSeq", E!FNPackSeq.ToString)
            '            Dim _p As New SqlParameter("@FBImage", SqlDbType.VarBinary)
            '            _p.Value = _dataBinary
            '            cmd.Parameters.Add(_p)
            '            cmd.ExecuteNonQuery()
            '        End If
            '    End If


            'Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub FNHSysSeasonId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSeasonId.EditValueChanged

        'Dim _season As String = HI.Conn.SQLConn.GetField("SELECT FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WHERE FTSeasonCode='" & Me.FNHSysSeasonId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        'Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        'Dim _SS As String = HI.Conn.SQLConn.GetField("SELECT FTSeasonCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  as S INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing  AS SP ON S.FNHSysSeasonId=SP.FNHSysSeasonId  WHERE SP.FNHSysStyleId='" & _style & "'  and S.FNHSysSeasonId='" & _season & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

        'Me.FNHSysSeasonId.Text = Me.FNHSysSeasonId.Text

        Call LoadDetail()
    End Sub


End Class
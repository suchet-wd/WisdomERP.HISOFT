Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports HI.TL

Public Class wAddEditPIDes
#Region "Vaiable"

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Public _KeyFiled As New List(Of HI.TL.PKFiled)()
    Public _LockFiled As New List(Of HI.TL.LockEditField)()
    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    Private _BaseFiled As New List(Of HI.TL.DataBaseFiled)()
    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()
    Private _DefaultsData As New List(Of HI.TL.DefaultsData)()
    Private _DataInfo As DataTable
    Private _SysPathImge As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _StageSave As Boolean = False
    Private _ProdAdd As Boolean = False
#End Region

    Sub New(ByVal FormName As String, ByVal Title As String, ByVal ObjId As Integer, ByVal AssemblyPath As String, ByVal tImage As String, ByVal tParentForm As Object)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Name = FormName & "AddEditPopup"
        Me.FormName = FormName
        Me.AssPath = AssemblyPath
        Me.Text = Title
        Me.Parent_Form = tParentForm

        _KeyFiled.Clear()
        _CheckFiled.Clear()
        _CheckDuplFiled.Clear()
        _BaseFiled.Clear()
        _CheckDelFiled.Clear()

        Me.FormObjID = ObjId


        If HI.ST.SysInfo.DevelopMode Then
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        End If

        If tImage <> "" Then
            Dim tPathImgDis As String = _SysPathImge & "\Menu\" & tImage
            If IO.File.Exists(tPathImgDis) Then

                Me.Icon = Icon.FromHandle(DirectCast(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))), Bitmap).GetHicon())
            End If
        End If

    End Sub

#Region "Property"

    Private _ActiveLang As HI.ST.Lang.eLang = -1
    Public Property ActiveLang As HI.ST.Lang.eLang
        Get
            Return _ActiveLang
        End Get
        Set(ByVal value As HI.ST.Lang.eLang)
            _ActiveLang = value
        End Set
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(ByVal value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(ByVal value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(ByVal value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(ByVal value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(ByVal value As String)
            _MainKeyID = value
        End Set
    End Property

    Private _MainKey As String = ""
    Public Property MainKey As String
        Get
            Return _MainKey
        End Get
        Set(ByVal value As String)
            _MainKey = value
        End Set
    End Property

    Private _RequireField As String = ""
    Public Property RequireField As String
        Get
            Return _RequireField
        End Get
        Set(ByVal value As String)
            _RequireField = value
        End Set
    End Property

    Private _Query As String = ""
    Public Property Query As String
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
        End Set
    End Property

    Private _QryTabPart As String
    Public Property QryTabPart As String
        Get
            Return _QryTabPart
        End Get
        Set(value As String)
            _QryTabPart = value
        End Set
    End Property


    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(ByVal value As Object)
            _Parent_Form = value
        End Set
    End Property
    Private _CheckTab As Boolean
    Public Property CheckTab As Boolean
        Get
            Return _CheckTab
        End Get
        Set(value As Boolean)
            _CheckTab = value
        End Set
    End Property

#End Region

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click

        Dim Qry As String = ""
        Dim _State As String = ""
        Dim _State1 As String = ""
        Dim _DataPic As Byte()

        If Me.FTStateActive.Checked Then
            _State = "1"
        Else
            _State = "2"
        End If
        'If Me.FTStateCritical.Checked Then
        '    _State1 = "1"
        'Else
        '    _State1 = "2"
        'End If


        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            'If Me.oTab.SelectedTabPage.Name = PI.Name Then
            If VerifyData() Then
                Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_PIDescription "
                Qry &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                Qry &= vbCrLf & ",FNHSysMainMatId='" & Me.FNHSysMainMatId.Text & "'"
                Qry &= vbCrLf & ",FTMainMatNameTH='" & Me.FTMainMatNameTH.Text & "'"
                Qry &= vbCrLf & ",FTMainMatNameEN='" & Me.FTMainMatNameEN.Text & "'"
                Qry &= vbCrLf & ",FTRemark='" & Me.FTRemark.Text & "'"
                Qry &= vbCrLf & " ,FTStateActive='" & _State & "'"
                Qry &= vbCrLf & "WHERE FNHSysDescRawMatId=" & Me.MainKeyID & ""
                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End If

                'Dim _cmd As String = ""
                '_cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart"
                '_cmd &= " Set  FPImage=@FPImage"
                '_cmd &= " WHERE FNHSysAssetPartId=@FNHSysAssetPartId"
                'Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                'cmd.Parameters.AddWithValue("@FNHSysAssetPartId", MainKeyID)
                'Dim _pdf As New SqlParameter("@FPImage", SqlDbType.VarBinary)
                'If Me.opnDialog.Text <> "" Then
                '    Dim br As New BinaryReader(New FileStream(Me.opnDialog.Text, FileMode.Open, FileAccess.Read))
                '    _pdf.Value = br.ReadBytes(CInt(New FileInfo(Me.opnDialog.Text).Length))
                '    cmd.Parameters.Add(_pdf)
                '    cmd.ExecuteNonQuery()
                'Else
                '    _pdf.Value = Me.Parent_Form._UPdatedataByte
                '    cmd.Parameters.Add(_pdf)
                '    cmd.ExecuteNonQuery()
                'End If
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Me.ProcComplete = True
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.Parent_Form.Preform()
                Me.Close()
            End If
            'Else
            '    If VerifyDataSpare() Then
            '        Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartSparePart"
            '        Qry &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "',FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
            '        Qry &= vbCrLf & ",FNQuantity=" & Me.FNQuantity.Value & ""
            '        Qry &= vbCrLf & "WHERE FNHSysAssetPartId=" & Me.FNHSysAssetPartId.Properties.Tag & " AND FNSeq=" & Me.FNSeq.Value & " AND FNHSysAssetSparePartId=" & Me.FNHSysAssetSparePartId.Properties.Tag & ""
            '        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        End If
            '        HI.Conn.SQLConn.Tran.Commit()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        Me.ProcComplete = True
            '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            '        Me.Close()
            '    End If
            'End If
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.ProcComplete = False
        End Try
    End Sub

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        Dim _SystemKey As String = ""
        Dim Qry As String = ""
        Dim _State As String = ""
        Dim _FilePath As String = ""
        Dim QryAfterAdd As String = ""
        Dim _MaxFNSeq As String = ""
        Dim _dataBinary As Byte()
        Dim dt As DataTable
        Dim _State1 As String = ""


        If Me.FTStateActive.Checked Then
            _State = "1"
        Else
            _State = "0"
        End If
        'If Me.FTStateCritical.Checked Then
        '    _State1 = "1"
        'Else
        '    _State1 = "0"
        'End If


        Try

            'If Me.oTab.SelectedTabPage.Name = PI.Name Then
            If VerifyData() Then
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                _SystemKey = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TINVENMMaterial_PIDescription", "FNHSysDescRawMatId", Conn.DB.DataBaseName.DB_MASTER)

                'Dim _BrandID As String = HI.Conn.SQLConn.GetField("select FTAssetBrandCode as FNHSysAssetBrandId  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand as B where B.FNHSysAssetBrandId ='" & Me.FNHSysAssetBrandId.Properties.Tag.ToString & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                '   Dim _TypeID As String = HI.Conn.SQLConn.GetField("select FTAssetPartTypeCode as FNHSysAssetPartTyped  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartType as T where T.FNHSysAssetPartTyped ='" & Me.FNHSysAssetPartTyped.Properties.Tag.ToString & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_PIDescription"
                Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FNHSysDescRawMatId,FNHSysMainMatId,FTMainMatNameTH,FTMainMatNameEN,FTRemark,FTStateActive)"
                Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                Qry &= vbCrLf & "," & Val(_SystemKey) & "," & Me.FNHSysMainMatId.Properties.Tag.ToString & ",'" & Me.FTMainMatNameTH.Text & "','" & Me.FTMainMatNameEN.Text & "'"
                Qry &= vbCrLf & ",'" & Me.FTRemark.Text & "'," & _State & "  "
                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.ProcComplete = False
                End If

                _StageSave = True

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Parent_Form.Preform()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

            'Else

            '    If VerifyDataSpare() Then

            '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
            '        HI.Conn.SQLConn.SqlConnectionOpen()
            '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            '        Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartSparePart"
            '        Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FNHSysAssetPartId,FNHSysAssetSparePartId,FNSeq,FNQuantity)"
            '        Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
            '        Qry &= vbCrLf & "," & Me.FNHSysAssetPartId.Properties.Tag & "," & Me.FNHSysAssetSparePartId.Properties.Tag & "," & Me.FNSeq.Value & "," & Me.FNQuantity.Value & ""
            '        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
            '            HI.Conn.SQLConn.Tran.Commit()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            '            Try
            '                QryAfterAdd = "SELECT MAX(FNSeq) AS FNSeq  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartSparePart AS MO WITH(NOLOCK) WHERE FNHSysAssetPartId=" & Me.FNHSysAssetPartId.Properties.Tag & ""
            '                _MaxFNSeq = HI.Conn.SQLConn.GetField(QryAfterAdd, Conn.DB.DataBaseName.DB_MASTER, "0")
            '                Me.FNSeq.Value = Integer.Parse(Val(_MaxFNSeq.ToString) + 1)
            '                Call LodaGridPartDetail()
            '            Catch ex As Exception

            '            End Try


            '            FNHSysAssetSparePartId.Text = ""
            '            FNQuantity.Value = 0
            '            Me.ProcComplete = True
            '        Else
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            '            Me.ProcComplete = False
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.ProcComplete = False
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        'If oTab.SelectedTabPage.Name = PI.Name Then
        Me.FNHSysMainMatId.Text = "" : Me.FTMainMatNameTH.Text = "" : Me.FTMainMatNameEN.Text = "" : Me.FTRemark.Text = "" : Me.FTStateActive.Checked = False
        'Me.FTStateCritical.Checked = False : Me.FNPrice.Value = 0 : Me.FNMinimumStock.Value = 0 : Me.FNMaximumStock.Value = 0 : Me.FTProductCode.Text = "" : Me.FNHSysAssetPartGrpId.Text = "" : Me.FNHSysAssetPartTyped.Text = "" : Me.FNHSysAssetBrandId.Text = "" : Me.FNHSysSuplId.Text = ""
        'Me.gbFPImage.Controls.Clear()
        'Else
        '    'Me.FNSeq.Value = 0
        '    Me.FNHSysAssetSparePartId.Text = "" : Me.FNHSysAssetSparePartId_None.Text = "" : Me.FNQuantity.Value = 0
        '    Me.ocmAddEdit.Enabled = True
        '    Me.ocmedit.Enabled = False
        '    Me.ocmdelete.Enabled = False
        'End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Not (ocmdelete.Enabled) Then Exit Sub
        Dim _Str As String = ""

        'If Me.oTab.SelectedTabPage.Name = PI.Name Then
        If Me.CheckNotUsed(Me.MainKeyID) = False Then Exit Sub
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, Me.FNHSysMainMatId.Text, Me.Text) = True Then

            _Str = " Delete From " & Me.TableName & " " & "  WHERE  FNHSysMainMatId ='" & Me.FNHSysMainMatId.Text & "' "
            _Str &= vbCrLf & "Delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_PIDescription WHERE FNHSysDescRawMatId=" & Me.MainKeyID & ""
            If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Str, _DBEnum)) Then
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.Close()
                Me.Parent_Form.Preform()
            End If
        End If

        'Else
        '    'If VerifyDataSpare() Then
        '    'For Each R As DataRow In dt.Rows
        '    If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, Me.FTAssetPartCode.Text, Me.Text) = True Then

        '        _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartSparePart "
        '        _Str &= vbCrLf & "WHERE FNHSysAssetPartId=" & Me.FNHSysAssetPartId.Properties.Tag & " AND FNSeq=" & FNSeq.Value & " AND FNHSysAssetSparePartId=" & FNHSysAssetSparePartId.Properties.Tag & ""
        '        If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Str, _DBEnum)) Then
        '            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        '        Else
        '            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        '            Me.FNHSysAssetSparePartId.Text = ""
        '            Me.FNQuantity.Value = 0
        '            Me.ocmedit.Enabled = False
        '            Me.ocmAddEdit.Enabled = True
        '            Me.ocmdelete.Enabled = False
        '            _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartSparePart "
        '            _Str &= vbCrLf & "SET FNSeq=FNSeq-1 WHERE FNHSysAssetPartId=" & Me.FNHSysAssetPartId.Properties.Tag & "AND FNSeq>" & Me.FNSeq.Value & ""
        '            HI.Conn.SQLConn.ExecuteNonQuery(_Str, _DBEnum)
        '            LodaGridPartDetail()
        '        End If
        '    End If
        '    'Next
        '    'End If
        'End If
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysMainMatId.Text.Trim <> "" Then
            If Me.FTMainMatNameTH.Text.Trim <> "" Then
                If Me.FTMainMatNameEN.Text.Trim() <> "" Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTMainMatNameEN_lbl.Text)
                    Me.FTMainMatNameEN.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTMainMatNameTH_lbl.Text)
                Me.FTMainMatNameTH.Focus()
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysMainMatId_lbl.Text)
            Me.FNHSysMainMatId.Focus()
        End If
        Return _Pass
    End Function
    'Private Function VerifyDataSpare() As Boolean
    '    Dim _Pass As Boolean = False
    '    If Me.FNHSysAssetSparePartId.Text <> "" Then
    '        If Me.FNQuantity.Value > 0 Then
    '            _Pass = True
    '        Else
    '            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNQuantity_lbl.Text)
    '            Me.FNQuantity.Focus()
    '        End If
    '    Else
    '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysAssetSparePartId_lbl.Text)
    '        Me.FNHSysAssetSparePartId.Focus()
    '    End If
    '    Return _Pass
    'End Function



    Private Function CheckNotUsed(ByVal Key As String) As Boolean
        Dim _Str As String = ""

        For I As Integer = 0 To _CheckDelFiled.ToArray.Count - 1
            _Str = _CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                Return False
            End If
        Next

        Return True
    End Function
    'Private Sub ButtonEdit1_Properties_Click(sender As Object, e As EventArgs)
    '    Dim opn As New OpenFileDialog

    '    With opn
    '        .Filter = "PDF files |*.pdf"
    '        .Multiselect = False
    '        .RestoreDirectory = True
    '        Dim result As DialogResult = .ShowDialog()
    '        Try
    '            If result = DialogResult.OK Then
    '                Me.opnDialog.Text = ""
    '                Me.opnDialog.Text = .FileName
    '                Call _PDFViewer()
    '            End If
    '        Catch ex As Exception

    '        End Try
    '    End With
    'End Sub
    'Private Sub _PDFViewer()
    '    Dim _Pdf As New DevExpress.XtraPdfViewer.PdfViewer

    '    Try
    '        Me.gbFPImage.Controls.Clear()
    '        _Pdf.Dock = DockStyle.Fill
    '        _Pdf.LoadDocument(Me.opnDialog.Text)
    '        Me.gbFPImage.Controls.Add(_Pdf)
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub LodaGridPartDetail()
    '    Dim Qry As String = ""
    '    Dim dt As DataTable

    '    Qry = "SELECT  MP.FNSeq,MO.FTAssetPartCode,MO.FTAssetPartNameTH,MO.FTAssetPartNameEN,P.FTAssetSparePartCode,P.FTAssetSparePartNameTH,P.FTAssetSparePartNameEN,MP.FNQuantity"
    '    Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo. TASMAssetPartSparePart AS MP INNER JOIN"
    '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetSparePart AS P WITH(NOLOCK) ON MP.FNHSysAssetSparePartId =P.FNHSysAssetSparePartId  LEFT OUTER JOIN"
    '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS MO WITH(NOLOCK) ON MP.FNHSysAssetPartId=MO.FNHSysAssetPartId "
    '    Qry &= vbCrLf & " WHERE MP.FNHSysAssetPartId=" & Me.FNHSysAssetPartId.Properties.Tag & " order by FNSeq asc"
    '    dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)

    '    Me.ogcSpare.DataSource = dt

    '    dt.Dispose()

    'End Sub

    'Private Sub ocmAddEdit_Click(sender As Object, e As EventArgs)
    '    Dim Qry As String = ""
    '    Dim _MaxFNSeq As String = ""
    '    Try
    '        Qry = "SELECt MAX(FNSeq) AS FNSeq  FroM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartSparePart  WITH(NOLOCK) WHERE FNHSysAssetPartId=" & Me.FNHSysAssetPartId.Properties.Tag & ""
    '        _MaxFNSeq = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "0")
    '        Me.FNSeq.Value = Integer.Parse(Val(_MaxFNSeq.ToString) + 1)

    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartSparePart"
    '        Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FNHSysAssetPartId,FNHSysAssetSparePartId,FNSeq,FNQuantity)"
    '        Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
    '        Qry &= vbCrLf & "," & Me.FNHSysAssetPartId.Properties.Tag & "," & Me.FNHSysAssetSparePartId.Properties.Tag & "," & Me.FNSeq.Value & "," & Me.FNQuantity.Value & ""
    '        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
    '            HI.Conn.SQLConn.Tran.Commit()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '            Me.FNHSysAssetSparePartId.Text = ""
    '            Me.FNQuantity.Value = 0
    '            Call LodaGridPartDetail()
    '        Else
    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '            Me.Close()
    '        End If


    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '        Me.Close()
    '    End Try

    'End Sub
    'Private Sub oTab_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs)
    '    Try
    '        If Me.CheckTab Then
    '            If oTab.SelectedTabPage.Name = PI.Name Then
    '                oTab.SelectedTabPage.TabControl.TabPages(1).PageEnabled = False
    '                Me.ocmAddEdit.Enabled = False
    '            Else
    '                Me.ocmAddEdit.Enabled = False
    '            End If
    '        Else
    '            If oTab.SelectedTabPage.Name = SparePart.Name And Me.FNHSysAssetSparePartId.Text = "" Then
    '                Me.ocmedit.Enabled = False
    '                Me.ocmAddEdit.Enabled = True
    '                Me.ocmdelete.Enabled = True
    '            Else
    '                Me.ocmedit.Enabled = True
    '                Me.ocmdelete.Enabled = True
    '            End If
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub ogvSpare_DoubleClick(sender As Object, e As EventArgs)
    '    If ocmaddnew.Visible = False Then
    '        Try
    '            With ogvSpare
    '                If Not (CType(ogcSpare.DataSource, DataTable) Is Nothing) Then
    '                    For Each RR As DataRow In CType(ogcSpare.DataSource, DataTable).Select("FNSeq=" & .GetRowCellValue(.FocusedRowHandle, "FNSeq" & ""))
    '                        Me.FNSeq.Value = Val(RR!FNSeq.ToString)
    '                        Me.FNHSysAssetPartId.Text = RR!FTAssetPartCode.ToString
    '                        Me.FNHSysAssetSparePartId.Text = RR!FTAssetSparePartCode.ToString
    '                        Me.FNQuantity.Value = RR!FNQuantity
    '                    Next
    '                    Me.ocmedit.Enabled = True
    '                    Me.ocmAddEdit.Enabled = False
    '                    Me.ocmdelete.Enabled = True
    '                End If
    '            End With
    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub


    Private Sub wAddEditPIDes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
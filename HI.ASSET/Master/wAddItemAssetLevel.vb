Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports HI.TL
Public Class wAddItemAssetLevel
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Public _KeyFiled As New List(Of HI.TL.PKFiled)()
    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    Private _BaseFiled As New List(Of HI.TL.DataBaseFiled)()
    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()
    Private _DefaultsData As New List(Of HI.TL.DefaultsData)()
    Private _DataInfo As DataTable
    Private _SysPathImge As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _ModelID As String = ""
    Private _ProdAdd As Boolean = False
    Sub New(ByVal FormName As String, ByVal Title As String, ByVal ObjId As Integer, ByVal AssemblyPath As String, ByVal tImage As String, ByVal tParentForm As Object)
        ' This call is required by the designer.

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
        'Me.PrepareForm()

        'Me.ocmsavelayuot.Visible = HI.ST.SysInfo.DevelopMode
        'Me.sbCustomization.Visible = HI.ST.SysInfo.DevelopMode
        'Me.ocmdeletelayout.Visible = HI.ST.SysInfo.DevelopMode
        'Me.olymain.AllowCustomizationMenu = HI.ST.SysInfo.DevelopMode

        If HI.ST.SysInfo.DevelopMode Then
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
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

#Region "Property"
    Private _AddComplete As Boolean = False
    Public Property AddComplete As Boolean
        Get
            Return _AddComplete
        End Get
        Set(value As Boolean)
            _AddComplete = value
        End Set
    End Property
    Private _Add As Boolean = False

    Public Property Add As Boolean
        Get
            Return _Add
        End Get
        Set(value As Boolean)
            _Add = value
        End Set
    End Property
    Private _PONO As String = ""
    Public Property PONO As String
        Get
            Return _PONO
        End Get
        Set(value As String)
            _PONO = value
        End Set
    End Property
#End Region
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        FTUserName.Text = ""
        FNStartQty.Text = "0"
        FNEndQty.Text = "0"
        FNHSysCmpId.Text = ""
    End Sub



    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()


    End Sub




    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Not (ocmdelete.Enabled) Then Exit Sub
        Dim Qry As String = ""
        If Me.CheckNotUsed(Me.MainKeyID) = False Then Exit Sub
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, Me.FNFixedAssetType.Text, Me.Text) = True Then

            Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel WHERE FNHSysConfigLevelId = " & Me._MainKeyID & ""
            If Not (HI.Conn.SQLConn.ExecuteNonQuery(Qry, _DBEnum)) Then
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

            Else
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.Close()
                Me.Parent_Form.Preform()
            End If
        End If


    End Sub
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

    Private Function VerityData() As Boolean


        If FNFixedAssetType.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNFixedAssetType_lbl.Text)
            FNFixedAssetType.Focus()
            Return False
        End If
        Return True

    End Function

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click

        Dim Qry As String = ""
        Dim _State As String = ""
        Dim _StateFa As String = ""
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            If Me.FTStateDirector.Checked Then
                _State = "1"
            Else
                _State = "0"
            End If
            If Me.FTStateFactory.Checked Then
                _StateFa = "1"
            Else
                _StateFa = "0"
            End If
            If VerityData() Then
                Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel "
                Qry &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                Qry &= vbCrLf & ",FNFixedAssetType='" & Me.FNFixedAssetType.SelectedIndex & "'"
                Qry &= vbCrLf & ",FTUserName='" & Me.FTUserName.Text & "'"
                Qry &= vbCrLf & ",FNEndQty=" & Me.FNEndQty.Value & ""
                Qry &= vbCrLf & ",FNStartQty=" & Me.FNStartQty.Value & ""
                Qry &= vbCrLf & ",FNHSysCmpId='" & Me.FNHSysCmpId.Properties.Tag & "'"
                Qry &= vbCrLf & " ,FTStateDirector='" & _State & "'"
                Qry &= vbCrLf & " ,FTStateFactory='" & _StateFa & "'"
                Qry &= vbCrLf & "WHERE FNHSysConfigLevelId='" & Me.MainKeyID & "'"
                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End If
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Me.ProcComplete = True
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.Close()
            End If
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
        Dim _StateFa As String = ""
        Try
            If VerityData() Then

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                If Me.FTStateDirector.Checked Then
                    _State = "1"
                Else
                    _State = "0"
                End If
                If Me.FTStateFactory.Checked Then
                    _StateFa = "1"
                Else
                    _StateFa = "0"
                End If
                _SystemKey = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TASMAssetConfigLevel", "FNHSysConfigLevelId", Conn.DB.DataBaseName.DB_MASTER)

                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel"
                Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTinsTime,FNHSysConfigLevelId,FNFixedAssetType,FTUserName,FNEndQty,FNStartQty,FNHSysCmpId,FTStateDirector,FTStateFactory)"
                Qry &= vbCrLf & "Select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                Qry &= vbCrLf & "," & Val(_SystemKey) & "," & FNFixedAssetType.SelectedIndex & ",'" & FTUserName.Text & "'," & FNEndQty.Value & "," & FNStartQty.Value & "," & Me.FNHSysCmpId.Properties.Tag & "," & _State & "," & _StateFa & ""
                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.ProcComplete = True
                End If
            End If
            Close()
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        End Try
    End Sub

    Private Sub wAddItemAssetLevel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Public Class wQAPreFinalNikeAudit

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False

#Region "Property"

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

#Region "Command"
    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTQANikeAuditBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub
        If VerrifyData() Then

            If SaveOtherDocument() Then
                Exit Sub
            End If

            If SaveData() Then
                Dim _Qry As String

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit "
                _Qry &= vbCrLf & "  SET FTStateSendApp='0' "
                _Qry &= vbCrLf & " , FTSendAppBy=''"
                _Qry &= vbCrLf & "  ,FTStateApp='0' "
                _Qry &= vbCrLf & "  ,FTAppName='' "
                _Qry &= vbCrLf & "  ,FTStateReject='0' "
                _Qry &= vbCrLf & "  ,FTRejectName='' "
                _Qry &= vbCrLf & "  ,FTStateLineLeaderApp='0' "
                _Qry &= vbCrLf & "  ,FTLineLeaderAppName='' "
                _Qry &= vbCrLf & "  ,FTStateLineLeaderReject='0' "
                _Qry &= vbCrLf & "  ,FTLineLeaderRejectName='' "
                _Qry &= vbCrLf & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditNo.Text) & "' AND FTStateApp <>'0'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                Me.FTStateApp.Checked = False
                FTStateApp.Checked = False
                FTStateReject.Checked = False
                FTStateLineLeaderApp.Checked = False
                FTStateLineLeaderReject.Checked = False

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

        If CheckOwner() = False Then Exit Sub
        If Me.FTQANikeAuditNo.Text <> "" Then
            If DeleteData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub
#End Region


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

    End Sub


    Private Sub InitFormControl()

        Dim _Str As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim _StrQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _StartX As Double = 0
        Dim _StartY As Double = 0
        Dim _CtrLv As Double = -1
        Dim _CtrHeight As Double = 0
        Dim _dtgrpobj As New DataTable


        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            _SortField = _dt.Rows(0)!FTSortField.ToString

            _Str = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
            _Str &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)


            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then

                For Each Row As DataRow In _dtgrpobj.Rows

                    Select Case Row!FNGenFormObj.ToString
                        Case "H"
                            Dim _DMF As New HI.TL.DynamicForm(_objId, Val(Row!FNFormObjID.ToString), _dt, Me)
                            _DMF.SysObjID = Val(Row!FNFormObjID.ToString)
                            _DMF.SysTableName = Row!FTTableName.ToString
                            _DMF.SysDBName = Row!FTBaseName.ToString
                            _FormHeader.Add(_DMF)

                    End Select

                Next

            End If

        End If

        _dt.Dispose()
        _dtgrpobj.Dispose()

    End Sub


    Public Sub DefaultsData()
        Dim _FieldName As String
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).DefaultsData.ToArray.Count - 1
                _FieldName = _FormHeader(cind).DefaultsData(I).FiledName.ToString

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString

                                HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)

                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)

                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                .SelectedIndex = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Checked = (_FormHeader(cind).DefaultsData(I).DataDefaults.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)

                                Try
                                    .DateTime = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                                Catch ex As Exception
                                    .Text = ""
                                End Try

                            End With

                        Case ENM.Control.ControlType.MemoEdit
                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case ENM.Control.ControlType.TextEdit
                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case Else
                    End Select
                Next
            Next
        Next

    End Sub

    Private Function SaveOtherDocument() As Boolean
        Dim _Qry As String = ""
        Dim _FTQANikeAuditNo As String = ""

        _Qry = "SELECT TOP 1 FTQANikeAuditNo "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTQANikeAuditNo<>'" & HI.UL.ULF.rpQuoted(FTQANikeAuditNo.Text) & "' "
        _Qry &= vbCrLf & " AND FDQADate='" & HI.UL.ULDate.ConvertEnDB(FDQADate.Text) & "' "
        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
        _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' "
        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse(Val(FNHSysUnitSectId.Properties.Tag.ToString)) & " "

        _FTQANikeAuditNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

        If _FTQANikeAuditNo <> "" Then

            HI.MG.ShowMsg.mInfo("ข้อมูลชุดนี้ถูกบันทึกด้วยหมายเลขเอกสารอื่นแล้ว !!!", 1505090879, Me.Text, _FTQANikeAuditNo, MessageBoxIcon.Warning)
            Return True
        Else
            Return False
        End If

    End Function

    Private Function SaveData() As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False
        Dim _CmpH As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else


                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else

                                    _Key = .Text

                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString

        End If

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean
            For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
                For I As Integer = 0 To _FormHeader(cind).BaseFiled.ToArray.Count - 1
                    _FieldName = _FormHeader(cind).BaseFiled(I).FiledName.ToString
                    _FoundControl = False
                    If (_StateNew) Then

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else
                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            If _Values <> "" Then _Values &= ","
                            If _Fields <> "" Then _Fields &= ","

                            _Fields &= _FieldName

                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate")
                                    _Values &= HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
                                    _Values &= "''"
                                Case UCase("FTInsTime")
                                    _Values &= HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FTInsUser")
                                    _Values &= "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= "'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If


                    Else

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else

                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case Else
                                    If _Values <> "" Then _Values &= ","
                            End Select

                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FTUpdTime")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case UCase("FTUpdUser")
                                    _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If




                    End If

                Next
                If (_StateNew) Then
                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
                Else
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next



            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function


    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

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
                                If Obj.Name.ToString = "FDSampleAppDate" Then
                                    Beep()
                                End If
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

        Dim _Qry As String = ""
        Dim _dtd As DataTable

        _Qry = "SELECT TOP 1"
        _Qry &= vbCrLf & "   FTStateSendApp "
        _Qry &= vbCrLf & "  ,FTStateApp "
        _Qry &= vbCrLf & "  ,FTStateReject "
        _Qry &= vbCrLf & "  ,FTStateLineLeaderApp "
        _Qry &= vbCrLf & "  ,FTStateLineLeaderReject "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditNo.Text) & "' "

        _dtd = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        FTStateSendApp.Checked = False
        FTStateApp.Checked = False
        FTStateReject.Checked = False
        FTStateLineLeaderApp.Checked = False
        FTStateLineLeaderReject.Checked = False

        For Each R As DataRow In _dtd.Rows
            FTStateSendApp.Checked = (R!FTStateSendApp.ToString = "1")
            FTStateApp.Checked = (R!FTStateApp.ToString = "1")
            FTStateReject.Checked = (R!FTStateReject.ToString = "1")
            FTStateLineLeaderApp.Checked = (R!FTStateLineLeaderApp.ToString = "1")
            FTStateLineLeaderReject.Checked = (R!FTStateLineLeaderReject.ToString = "1")
            Exit For
        Next

        Call LoadQAFinalDetail()

        Me.FDQADate.Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")

        Me.FNHSysUnitSectId.Properties.ReadOnly = (Me.FTQANikeAuditNo.Properties.Tag.ToString <> "")
        Me.FNHSysUnitSectId.Properties.Buttons(0).Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")

        Me.FTOrderNo.Properties.ReadOnly = (Me.FTQANikeAuditNo.Properties.Tag.ToString <> "")
        Me.FTOrderNo.Properties.Buttons(0).Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")

        Me.FTSubOrderNo.Properties.ReadOnly = (Me.FTQANikeAuditNo.Properties.Tag.ToString <> "")
        Me.FTSubOrderNo.Properties.Buttons(0).Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")

        _ProcLoad = False
    End Sub

    Private Function VerrifyData() As Boolean
        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
                _Caption = ""

                For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)
                    If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
                        _Caption = ObjCaption.Text
                        Exit For
                    End If
                Next

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Properties.Buttons.Count <= 1 Then
                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                        Pass = False
                                    End If
                                End If
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                If Val(.Value.ToString) <= 0 Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If .SelectedIndex < 0 Then Pass = False
                            End With
                        Case ENM.Control.ControlType.CheckEdit

                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                If .Image Is Nothing Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            If Obj.Text = "" Then
                                Pass = False
                            End If
                        Case Else
                            Pass = False
                    End Select

                    If Pass = False Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
                        Obj.Focus()
                        Return False
                    End If
                Next
            Next
        Next


        '---------- Validate Document ---------------------
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else

                                Dim _CmpH As String = ""
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next

        Return True
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub LoadQAFinalDetail()

        Call InitGridAudit(Nothing, Nothing)
        Call InitGridDetail(Nothing, Nothing)
        ogcQAFinalCheckPoint.DataSource = Nothing

        If Me.FDQADate.Text = "" Or Me.FNHSysUnitSectId.Text = "" Or FTOrderNo.Text = "" Or FTSubOrderNo.Text = "" Then
            Exit Sub
        End If

        Dim _Spls As New HI.TL.SplashScreen("Loading... Please wait..")
        Try

            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _dtdetail As New DataTable
            Dim _dtdetailColor As New DataTable
            Dim _dtdetailtmpAudit As New DataTable
            Dim _dtdetailAudit As New DataTable

            With _dtdetailAudit
                .Columns.Add("FTTitle", GetType(String))
                .Columns.Add("FNTotal", GetType(Integer))
            End With

            _dtdetailAudit.Rows.Add("QUANTITY TO BE SHIPPED", 0)
            _dtdetailAudit.Rows.Add("QUANTITY AUDITED", 0)
            _dtdetailAudit.Rows.Add("TOTAL NO CONFORMING", 0)

            With _dtdetailColor
                .Columns.Add("FTColorway", GetType(String))
                .Columns.Add("FNHSysMatColorId", GetType(Integer))
            End With

            With _dtdetail
                .Columns.Add("FTQATypeCode", GetType(String))
                .Columns.Add("FNTotal", GetType(Integer))
            End With

            _Qry = " SELECT  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FTBarcodeCartonNo, FNQAInQty, FNQAAqlQty, FNQAActualQty, "
            _Qry &= vbCrLf & " FNMajorQty, FNMinorQty, FNAndon, FTStateReject,FTColorway,FTSubOrderNo,Isnull(FTBarcodeRef,'') AS FTBarcodeRef"
            _Qry &= vbCrLf & "    , ISNULL ((SELECT TOP 1 FNHSysMatColorId"
            _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS X WITH (NOLOCK)"
            _Qry &= vbCrLf & "       WHERE   (FTMatColorCode = A.FTColorway)), 0) AS FNHSysMatColorId"
            _Qry &= vbCrLf & " FROM ( SELECT  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FTBarcodeCartonNo, FNQAInQty, FNQAAqlQty, FNQAActualQty, "
            _Qry &= vbCrLf & " FNMajorQty, FNMinorQty, FNAndon, FTStateReject , FTBarcodeRef"
            _Qry &= vbCrLf & "        , ISNULL ((SELECT TOP 1 FTSubOrderNo"
            _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS X WITH (NOLOCK)"
            _Qry &= vbCrLf & "       WHERE   (FTBarcodeNo = A.FTBarcodeCartonNo)), '') AS FTSubOrderNo, ISNULL"
            _Qry &= vbCrLf & "     ((SELECT TOP 1 FTColorway"
            _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS X WITH (NOLOCK)"
            _Qry &= vbCrLf & "    WHERE   (FTBarcodeNo = A.FTBarcodeCartonNo)), '') AS FTColorway"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
            _Qry &= vbCrLf & " AND A.FDQADate='" & HI.UL.ULDate.ConvertEnDB(FDQADate.Text) & "' "
            _Qry &= vbCrLf & " AND A.FNHSysUnitSectId=" & Integer.Parse(Val(FNHSysUnitSectId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & " ) AS A"
            _Qry &= vbCrLf & " WHERE A.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' "
            _Qry &= vbCrLf & "Order By FTBarcodeRef ASC"
            _dtdetailtmpAudit = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Dim _FTBarcodeRef As String = ""

            For Each R As DataRow In _dtdetailtmpAudit.Rows
                If _dtdetailAudit.Columns.IndexOf("C" & R!FNHSysMatColorId.ToString) < 0 Then
                    _dtdetailAudit.BeginInit()
                    _dtdetailAudit.Columns.Add("C" & R!FNHSysMatColorId.ToString, GetType(String))
                    _dtdetailAudit.Rows(0).Item("C" & R!FNHSysMatColorId.ToString) = 0
                    _dtdetailAudit.Rows(1).Item("C" & R!FNHSysMatColorId.ToString) = 0
                    _dtdetailAudit.Rows(2).Item("C" & R!FNHSysMatColorId.ToString) = 0
                    _dtdetailAudit.EndInit()
                End If
                If _FTBarcodeRef = "" Or _FTBarcodeRef <> R!FTBarcodeRef.ToString Then
                    _dtdetailAudit.Rows(0).Item("C" & R!FNHSysMatColorId.ToString) = Integer.Parse(Val(_dtdetailAudit.Rows(0).Item("C" & R!FNHSysMatColorId.ToString))) + Val(R!FNQAInQty.ToString)
                    _dtdetailAudit.Rows(1).Item("C" & R!FNHSysMatColorId.ToString) = Integer.Parse(Val(_dtdetailAudit.Rows(1).Item("C" & R!FNHSysMatColorId.ToString))) + Val(R!FNQAActualQty.ToString)
                    _dtdetailAudit.Rows(2).Item("C" & R!FNHSysMatColorId.ToString) = Integer.Parse(Val(_dtdetailAudit.Rows(2).Item("C" & R!FNHSysMatColorId.ToString))) + (Val(R!FNMajorQty.ToString) + Val(R!FNMinorQty.ToString))

                    _dtdetailAudit.Rows(0).Item("FNTotal") = Integer.Parse(Val(_dtdetailAudit.Rows(0).Item("FNTotal"))) + Val(R!FNQAInQty.ToString)
                    _dtdetailAudit.Rows(1).Item("FNTotal") = Integer.Parse(Val(_dtdetailAudit.Rows(1).Item("FNTotal"))) + Val(R!FNQAActualQty.ToString)
                    _dtdetailAudit.Rows(2).Item("FNTotal") = Integer.Parse(Val(_dtdetailAudit.Rows(2).Item("FNTotal"))) + (Val(R!FNMajorQty.ToString) + Val(R!FNMinorQty.ToString))

                End If
                _FTBarcodeRef = R!FTBarcodeRef.ToString
            Next

            _Qry = "  SELECT FTQATypeCode,0 AS  FNTotal  "
            _Qry &= vbCrLf & "   FROM        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQAType AS DT WITH (NOLOCK)"
            _Qry &= vbCrLf & " ORDER BY FTQATypeCode"
            _dtdetail = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            _Qry = "  SELECT FNHSysStyleId,FNHSysUnitSectId,FTOrderNo,FTSubOrderNo,FDQADate,FTBarcodeCartonNo,FNHSysQATypeId,FTColorway"
            _Qry &= vbCrLf & " , FTQADetailCode, FTQADetailNameTH, FTQADetailNameEN, FTQATypeNameTH, FTQATypeNameEN, FTQATypeCode"
            _Qry &= vbCrLf & "    , ISNULL ((SELECT TOP 1 FNHSysMatColorId"
            _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS X WITH (NOLOCK)"
            _Qry &= vbCrLf & "       WHERE   (FTMatColorCode = A.FTColorway)), 0) AS FNHSysMatColorId"
            _Qry &= vbCrLf & " FROM (   SELECT AA.FNHSysStyleId, AA.FNHSysUnitSectId, AA.FTOrderNo, AA.FDQADate, AA.FTBarcodeCartonNo, DT.FNHSysQATypeId, ISNULL"
            _Qry &= vbCrLf & "        ((SELECT TOP 1 FTSubOrderNo"
            _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS X WITH (NOLOCK)"
            _Qry &= vbCrLf & "       WHERE   (FTBarcodeNo = AA.FTBarcodeCartonNo)), '') AS FTSubOrderNo, ISNULL"
            _Qry &= vbCrLf & "     ((SELECT TOP 1 FTColorway"
            _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS X WITH (NOLOCK)"
            _Qry &= vbCrLf & "    WHERE   (FTBarcodeNo = AA.FTBarcodeCartonNo)), '') AS FTColorway"
            _Qry &= vbCrLf & "  , ISNULL(D.FTQADetailCode,'') AS FTQADetailCode"
            _Qry &= vbCrLf & ", ISNULL(D.FTQADetailNameTH,'') AS FTQADetailNameTH"
            _Qry &= vbCrLf & " , ISNULL(D.FTQADetailNameEN,'') AS FTQADetailNameEN"
            _Qry &= vbCrLf & " , ISNULL(DT.FTQATypeNameTH,'') AS FTQATypeNameTH"
            _Qry &= vbCrLf & " , ISNULL(DT.FTQATypeNameEN,'') AS FTQATypeNameEN"
            _Qry &= vbCrLf & " , ISNULL(DT.FTQATypeCode,'') AS FTQATypeCode"
            _Qry &= vbCrLf & "      FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal AS AA WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A WITH (NOLOCK) ON AA.FNHSysStyleId=A.FNHSysStyleId"
            _Qry &= vbCrLf & "   AND AA.FNHSysUnitSectId=A.FNHSysUnitSectId"
            _Qry &= vbCrLf & "   AND AA.FTOrderNo=A.FTOrderNo"
            _Qry &= vbCrLf & "   AND AA.FDQADate=A.FDQADate"
            _Qry &= vbCrLf & "   AND AA.FNHourNo=A.FNHourNo"
            _Qry &= vbCrLf & "   AND AA.FTBarcodeCartonNo=A.FTBarcodeCartonNo"
            _Qry &= vbCrLf & "     LEFT OUTER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS D WITH (NOLOCK) ON A.FNHSysQADetailId = D.FNHSysQADetailId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQAType AS DT WITH (NOLOCK) ON D.FNHSysQATypeId = DT.FNHSysQATypeId"
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & " WHERE AA.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
            _Qry &= vbCrLf & " AND AA.FDQADate='" & HI.UL.ULDate.ConvertEnDB(FDQADate.Text) & "' "
            _Qry &= vbCrLf & " AND AA.FNHSysUnitSectId=" & Integer.Parse(Val(FNHSysUnitSectId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & " ) AS A"
            _Qry &= vbCrLf & " WHERE A.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' "
            _Qry &= vbCrLf & " ORDER BY FTQATypeCode,FTQADetailCode "
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Dim _StrFilter As String = ""

            For Each R As DataRow In _dt.Rows
                _StrFilter = "FTQATypeCode='" & HI.UL.ULF.rpQuoted(R!FTQATypeCode.ToString) & "' "

                If _dtdetail.Select(_StrFilter).Length <= 0 Then
                    _dtdetail.Rows.Add(R!FTQATypeCode.ToString, 1)
                End If

                If _dtdetail.Columns.IndexOf("C" & R!FNHSysMatColorId.ToString) < 0 Then
                    _dtdetail.Columns.Add("C" & R!FNHSysMatColorId.ToString, GetType(String))
                End If

                If R!FTQADetailCode.ToString <> "" Then
                    Dim _Found As Boolean = False
                    For Each Rx As DataRow In _dtdetail.Select(_StrFilter)
                        Rx!FNTotal = Val(Rx!FNTotal) + 1
                        Dim _tmpstr As String = Rx.Item("C" & R!FNHSysMatColorId.ToString).ToString
                        Dim _tmp As String = ""
                        If R!FTQADetailCode.ToString <> "" Then
                            If _tmpstr <> "" Then

                                For Each t As String In _tmpstr.Split(",")
                                    If _Found = False Then
                                        If Microsoft.VisualBasic.Left(t, R!FTQADetailCode.ToString.Length) = R!FTQADetailCode.ToString Then
                                            _Found = True

                                            If _tmp = "" Then
                                                _tmp = (t.Split("-")(0)) & "-" & (Integer.Parse(Val(t.Split("-")(1)) + 1)).ToString
                                            Else
                                                _tmp = _tmp & "," & (t.Split("-")(0)) & "-" & (Integer.Parse(Val(t.Split("-")(1)) + 1)).ToString
                                            End If

                                        Else
                                            If _tmp = "" Then
                                                _tmp = t
                                            Else
                                                _tmp = _tmp & "," & t
                                            End If
                                        End If
                                    Else
                                        If _tmp = "" Then
                                            _tmp = t
                                        Else
                                            _tmp = _tmp & "," & t
                                        End If
                                    End If

                                Next

                                If _Found = False Then
                                    If _tmp = "" Then
                                        _tmp = R!FTQADetailCode.ToString & "-1"
                                    Else
                                        _tmp = _tmp & "," & R!FTQADetailCode.ToString & "-1"
                                    End If
                                End If

                                Rx.Item("C" & R!FNHSysMatColorId.ToString) = _tmp

                            Else
                                Rx.Item("C" & R!FNHSysMatColorId.ToString) = R!FTQADetailCode.ToString & "-1"
                            End If
                        End If

                    Next
                End If


                If _dtdetailColor.Select("FNHSysMatColorId=" & Val(R!FNHSysMatColorId.ToString) & "").Length <= 0 Then
                    _dtdetailColor.Rows.Add(R!FTColorway.ToString, Val(R!FNHSysMatColorId.ToString))
                End If
            Next

            Call InitGridAudit(_dtdetailAudit, _dtdetailColor)
            Call InitGridDetail(_dtdetail, _dtdetailColor)
            Call LoadQAPointDetail()
        Catch ex As Exception

        End Try
        _Spls.Close()
    End Sub

    Private Sub InitGridAudit(Optional _dt As DataTable = Nothing, Optional _dtcolor As DataTable = Nothing)

        Dim _colcount As Integer = 0
        With Me.ogvaudit

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTTitle".ToUpper, "FNTotal".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next


            If Not (_dtcolor Is Nothing) Then

                For Each R As DataRow In _dtcolor.Select("FNHSysMatColorId>=0", "FTColorway")

                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                    With ColG
                        .Visible = True
                        .FieldName = "C" & R!FNHSysMatColorId.ToString
                        .Name = "C" & R!FNHSysMatColorId.ToString
                        .Caption = R!FTColorway.ToString
                    End With

                    .Columns.Add(ColG)

                    With .Columns("C" & R!FNHSysMatColorId.ToString)

                        .OptionsFilter.AllowAutoFilter = False
                        .OptionsFilter.AllowFilter = False
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                        With .OptionsColumn
                            .AllowMove = False
                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .AllowEdit = False
                            .AllowMove = False
                        End With

                    End With
                Next

            End If

        End With

        Me.ogcaudit.DataSource = _dt

    End Sub

    Private Sub InitGridDetail(Optional _dt As DataTable = Nothing, Optional _dtcolor As DataTable = Nothing)

        Dim _colcount As Integer = 0
        With Me.ogvDetail

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTQATypeCode".ToUpper, "FNTotal".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next


            If Not (_dtcolor Is Nothing) Then

                For Each R As DataRow In _dtcolor.Select("FNHSysMatColorId>=0", "FTColorway")

                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                    With ColG
                        .Visible = True
                        .FieldName = "C" & R!FNHSysMatColorId.ToString
                        .Name = "C" & R!FNHSysMatColorId.ToString
                        .Caption = R!FTColorway.ToString
                    End With

                    .Columns.Add(ColG)

                    With .Columns("C" & R!FNHSysMatColorId.ToString)

                        .OptionsFilter.AllowAutoFilter = False
                        .OptionsFilter.AllowFilter = False
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                        With .OptionsColumn
                            .AllowMove = False
                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .AllowEdit = False
                            .AllowMove = False
                        End With

                    End With

                Next

            End If

        End With

        Me.ogcDetail.DataSource = _dt

    End Sub

    Private Sub LoadQAPointDetail()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT Row_number() Over (Order By D.FTQAFinalCheckPointCode) AS FNSeq"
        _Qry &= vbCrLf & ", A.FNHSysQAFinalCheckPointId, A.FTHour01, A.FTHour02, A.FTHour03, A.FTHour04, A.FTHour05"
        _Qry &= vbCrLf & ", A.FTHour06, A.FTHour07, A.FTHour08, A.FTHour09, A.FTHour10, A.FTHour11, D.FTQAFinalCheckPointCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  ,   D.FTQAFinalCheckPointNameTH AS FTQAFinalCheckPointName"
        Else
            _Qry &= vbCrLf & "   ,  D.FTQAFinalCheckPointNameEN AS FTQAFinalCheckPointName "
        End If

        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_QA_Final_ChaeckPoint_ByHour AS A INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQAFinalCheckPoint AS D WITH(NOLOCK) ON A.FNHSysQAFinalCheckPointId = D.FNHSysQAFinalCheckPointId"
        _Qry &= vbCrLf & "   WHERE  (A.FNHSysUnitSectId = " & Integer.Parse(Val(FNHSysUnitSectId.Properties.Tag.ToString)) & ")"
        _Qry &= vbCrLf & "    AND (A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' ) "
        _Qry &= vbCrLf & "  AND (A.FDQADate = '" & HI.UL.ULDate.ConvertEnDB(FDQADate.Text) & "') "
        _Qry &= vbCrLf & "  AND (A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' )"
        _Qry &= vbCrLf & " ORDER BY   D.FTQAFinalCheckPointCode"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcQAFinalCheckPoint.DataSource = _dt.Copy
        _dt.Dispose()
    End Sub

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

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
        _ProcLoad = True
        HI.TL.HandlerControl.ClearControl(Me)
        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
        Me.FDQADate.Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")
        Me.FNHSysUnitSectId.Properties.ReadOnly = (Me.FTQANikeAuditNo.Properties.Tag.ToString <> "")
        Me.FNHSysUnitSectId.Properties.Buttons(0).Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")
        Me.FTOrderNo.Properties.ReadOnly = (Me.FTQANikeAuditNo.Properties.Tag.ToString <> "")
        Me.FTOrderNo.Properties.Buttons(0).Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")
        Me.FTSubOrderNo.Properties.ReadOnly = (Me.FTQANikeAuditNo.Properties.Tag.ToString <> "")
        Me.FTSubOrderNo.Properties.Buttons(0).Enabled = (Me.FTQANikeAuditNo.Properties.Tag.ToString = "")

        _ProcLoad = False
        Call LoadQAFinalDetail()
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTQANikeAuditNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "QAPreFinalNikeAuditSlip.rpt"
                .Formular = "{TPROTQAPreFinalNikeAudit.FTQANikeAuditNo}='" & HI.UL.ULF.rpQuoted(FTQANikeAuditNo.Text) & "' "
                .Preview()
            End With
        End If
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTQANikeAuditNo.Text <> "" And Me.FTQANikeAuditNo.Properties.Tag.ToString <> "" Then
            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateSendApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditNo.Text) & "'  "
            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "1" Then
                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,FTStateApp='0' "
                _Qry &= vbCrLf & "  ,FTAppName='' "
                _Qry &= vbCrLf & "  ,FTStateReject='0' "
                _Qry &= vbCrLf & "  ,FTRejectName='' "
                _Qry &= vbCrLf & "  ,FTStateLineLeaderApp='0' "
                _Qry &= vbCrLf & "  ,FTLineLeaderAppName='' "
                _Qry &= vbCrLf & "  ,FTStateLineLeaderReject='0' "
                _Qry &= vbCrLf & "  ,FTLineLeaderRejectName='' "
                _Qry &= vbCrLf & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)
                Call SendMailApp()
            End If
            FTStateSendApp.Checked = True
            FTStateApp.Checked = False
            FTStateReject.Checked = False
            FTStateLineLeaderApp.Checked = False
            FTStateLineLeaderReject.Checked = False
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTQANikeAuditNo_lbl.Text)
        End If
    End Sub

    Private Sub SendMailApp()
        Dim _Qry As String = ""
        Dim _UserMailTo As String = ""

        _Qry = " SELECT TOP 1 Tm.FTUserName"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS Tm WITH(NOLOCK)  ON U.FNHSysTeamGrpId = Tm.FNHSysTeamGrpId"
        _Qry &= vbCrLf & " WHERE  (U.FTUserName = N'" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditBy.Text) & "')"
        _UserMailTo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

        If _UserMailTo <> "" Then

            Dim tmpsubject As String = ""
            Dim tmpmessage As String = ""
            tmpsubject = "Send Approve QA. Pre-Final Nike Audit No " & Me.FTQANikeAuditNo.Text
            tmpmessage = "Send Approve QA. Pre-Final Nike Audit No " & Me.FTQANikeAuditNo.Text
            tmpmessage &= vbCrLf & "Date :" & Me.FDQANikeAuditqDate.Text
            tmpmessage &= vbCrLf & "By :" & Me.FTQANikeAuditBy.Text
            tmpmessage &= vbCrLf & "QA Pre-Fianl Date :" & Me.FDQADate.Text
            tmpmessage &= vbCrLf & "QA Pre-Fianl Line :" & Me.FNHSysUnitSectId.Text
            tmpmessage &= vbCrLf & "QA Pre-Fianl FO. No. :" & Me.FTOrderNo.Text
            tmpmessage &= vbCrLf & "QA Pre-Fianl Sub FO. No :" & Me.FTSubOrderNo.Text
            tmpmessage &= vbCrLf & "Note :" & Me.FTRemark.Text
            If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 7, Me.FTQANikeAuditNo.Text) Then
            End If
        End If
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        If Me.FTQANikeAuditNo.Text <> "" And Me.FTQANikeAuditNo.Properties.Tag.ToString <> "" Then

            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditNo.Text) & "'  "
            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") <> "1" Then
                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROTQAPreFinalNikeAudit "
                _Qry &= vbCrLf & "  SET FTStateApp='1' "
                _Qry &= vbCrLf & ", FTAppName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & ", FTAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(Me.FTQANikeAuditNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)
            End If
            FTStateApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTQANikeAuditNo_lbl.Text)
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo(FTQANikeAuditNo.Properties.Tag.ToString)
    End Sub

    Private Sub FTSubOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSubOrderNo.EditValueChanged, FTOrderNo.EditValueChanged
        If _ProcLoad = False Then
            Call LoadQAFinalDetail()
        End If
    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectId.EditValueChanged
        FTOrderNo.Text = ""
        FTOrderNo.Text = ""
    End Sub

    Private Sub FDQADate_EditValueChanged(sender As Object, e As EventArgs) Handles FDQADate.EditValueChanged
        FNHSysUnitSectId.Text = ""
    End Sub

    Private Sub wQAPreFinalNikeAudit_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ogvaudit.OptionsView.ShowAutoFilterRow = False
        Me.ogvDetail.OptionsView.ShowAutoFilterRow = False
        Me.ogvQAFinalCheckPoint.OptionsView.ShowAutoFilterRow = False
    End Sub

End Class
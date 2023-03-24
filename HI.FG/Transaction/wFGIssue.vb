Imports System.Windows.Forms

Public Class wFGIssue
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_FG
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitFormControl()
    End Sub

    Private Sub wFGAdjust_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FTBarcodeCartonNo.EnterMoveNextControl = False
        Catch ex As Exception
        End Try
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

        Call LoadDetail(Me.FTIssueFGNo.Text)
        Call CheckIssNo()
        _ProcLoad = False
    End Sub

    Private Sub CheckIssNo()
        Try
            Dim _Cmd As String = ""

            _Cmd = "Select Top 1 FTDocumentRefNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG_Detail where FTDocumentRefNo = '" & HI.UL.ULF.rpQuoted(Me.FTIssueFGNo.Text) & "'"
            _Cmd &= vbCrLf & "UNION ALL "
            _Cmd &= vbCrLf & "Select Top 1 FTDocumentRefNo  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail Where FTDocumentRefNo = '" & HI.UL.ULF.rpQuoted(Me.FTIssueFGNo.Text) & "'"
            If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG).Rows.Count > 0 Then
                Me.RepositoryFNQuantity.ReadOnly = True
                Me.cFNQuantity.OptionsColumn.AllowEdit = False
                'Me.FNHSysStyleId.Properties.ReadOnly = True
                'Me.FNHSysStyleId.Properties.Buttons(0).Enabled = False
            Else
                Me.RepositoryFNQuantity.ReadOnly = False
                Me.cFNQuantity.OptionsColumn.AllowEdit = True
                'Me.FNHSysStyleId.Properties.ReadOnly = False
                'Me.FNHSysStyleId.Properties.Buttons(0).Enabled = True
            End If
        Catch ex As Exception

        End Try
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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Function CheckNotUsed(Key As String) As Boolean
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckDelFiled.ToArray.Count - 1
                _Str = _FormHeader(cind).CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                    HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                    Return False
                End If

            Next
        Next
        Return True
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

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
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

            If Not (SaveDetail(_Key)) Then
                'HI.Conn.SQLConn.Tran.Rollback()
                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                'Return False
            End If

            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next



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

    Private Function SaveDetail(key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
          
            Dim _Seq As Integer = 0
            For Each R As DataRow In _oDt.Rows
                _Seq += +1
                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail "
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNQuantity=" & Integer.Parse("0" & R!FNQuantity.ToString)
                _Cmd &= vbCrLf & ", FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & ", FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & ", FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & ", FNHSysStyleId=" & Integer.Parse(R!FNHSysStyleId.ToString)
                _Cmd &= vbCrLf & ",FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                _Cmd &= vbCrLf & "WHERE FTIssueFGNo='" & HI.UL.ULF.rpQuoted(key) & "'"
                _Cmd &= vbCrLf & "And FNSeq=" & Integer.Parse(_Seq)
                '_Cmd &= vbCrLf & "And FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                '_Cmd &= vbCrLf & "And FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Cmd &= vbCrLf & "And FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                '_Cmd &= vbCrLf & "And FNHSysStyleId=" & Integer.Parse(R!FNHSysStyleId.ToString)

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail (FTInsUser, FDInsDate, FTInsTime,  FTIssueFGNo, FTColorway,FTSizeBreakDown, FNQuantity,FTOrderNo,FNHSysStyleId,FNSeq,FTBarCodeCarton)"
                    _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(key) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNQuantity.ToString)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysStyleId.ToString)
                    _Cmd &= vbCrLf & "," & Integer.Parse(_Seq)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            Next

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail Where FTIssueFGNo='" & HI.UL.ULF.rpQuoted(key) & "'"
            _Cmd &= vbCrLf & "And FNSeq >" & Integer.Parse(_Seq)
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG WHERE FTIssueFGNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueFGNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail WHERE FTIssueFGNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueFGNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail WHERE FTIssueFGNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueFGNo.Text) & "'")

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub LoadData(HSysId As String)
        Dim _Str As String = Me.Query & "  WHERE " & Me.MainKey & "='" & HSysId & "' "
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                .Text = R.Item(Col).ToString
                                Call HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)
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
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                Try
                                    .DateTime = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Text = ""
                                End Try
                            End With

                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next

    End Sub

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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

#End Region

#Region "Command"
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            Call LoadDetail(Me.FTIssueFGNo.Text)
            Call VisibleWH()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTIssueFGNo.Text, Me.Text) = False Then
                Exit Sub
            End If
            If Not (CheckUse(Me.FTIssueFGNo.Text)) Then
                HI.MG.ShowMsg.mInfo("Can not Delete Data are used !!", 1510071020, Me.Text)
                Exit Sub
            End If
            If Me.DeleteData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
                Me.DefaultsData()
                Call LoadDetail(Me.FTIssueFGNo.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Me.VerrifyData Then
                If Me.SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region
    Private Function CheckUse(_Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FTDocumentRefNo From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows.Count > 0 Then
                Return False
            End If
            _Cmd = "Select Top 1 FTDocumentRefNo From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows.Count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadDetail(key As String)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT        MO.FTPORef , MO.FNHSysStyleId, ST.FTStyleCode, MO.FTOrderNo "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & " ,  PT.FTProdTypeNameTH AS FTProdTypeName   "
            Else
                _Cmd &= vbCrLf & " ,  PT.FTProdTypeNameEN AS FTProdTypeName   "
            End If
            _Cmd &= vbCrLf & ", AJ.FTColorway, AJ.FTSizeBreakDown, VA.FTCustBarcodeNo, AJ.FNQuantity , AJ.FTBarCodeCarton "
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail AS AJ WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON  AJ.FTOrderNo = VA.FTOrderNo And AJ.FNHSysStyleId = VA.FNHSysStyleId"
            _Cmd &= vbCrLf & " And AJ.FTColorway = VA.FTColorway and AJ.FTSizeBreakDown = VA.FTSizeBreakDown  LEFT OUTER JOIN  "
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS MO WITH (NOLOCK) ON AJ.FTOrderNo = MO.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS PT WITH (NOLOCK) ON MO.FNHSysProdTypeId = PT.FNHSysProdTypeId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK) ON MO.FNHSysStyleId = ST.FNHSysStyleId"

            _Cmd &= vbCrLf & " WHERE AJ.FTIssueFGNo='" & HI.UL.ULF.rpQuoted(key) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
            Me.ogcdetail.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysStyleId_KeyDown(sender As Object, e As KeyEventArgs) Handles FNHSysStyleId.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.FNHSysStyleId.Text <> "" And Me.FNHSysWHFGId.Text <> "" Then
                    '  Dim _WHFGId As Integer = HI.Conn.SQLConn.GetField("SELECT TOP (1) FNHSysWHFGId  FROM  TCNMWarehouseFG WITH(NOLOCK) WHERE Isnull(FTStateSale,'') = '1' and FNHSysWHFGId=" & Integer.Parse(Me.FNHSysWHFGId.Properties.Tag.ToString), Conn.DB.DataBaseName.DB_MASTER, "0")

                    Dim _WHFGId As Integer = HI.Conn.SQLConn.GetField("SELECT TOP (1) FNHSysWHFGId  FROM  TCNMWarehouseFG WITH(NOLOCK) WHERE Isnull(FTStateSale,'') = '1' and FNHSysWHFGId=" & Integer.Parse(Me.FNHSysWHFGId.Properties.Tag.ToString), Conn.DB.DataBaseName.DB_MASTER, "0")
                    If _WHFGId = 0 Then
                        _WHFGId = Integer.Parse("0" & Me.FNHSysWHFGId.Properties.Tag.ToString)
                    End If

                    Dim _dt As DataTable = ChekOnhand(_WHFGId, Me.FNHSysStyleId.Text)
                    Dim _oDt As DataTable = CType(ogcdetail.DataSource, DataTable)
                    If IsNothing(_oDt) Then
                        _oDt = New DataTable
                        With _oDt
                            .Columns.Add("FTPORef", GetType(String))
                            .Columns.Add("FNHSysStyleId", GetType(Integer))
                            .Columns.Add("FTStyleCode", GetType(String))
                            .Columns.Add("FTOrderNo", GetType(String))
                            .Columns.Add("FTProdTypeName", GetType(String))
                            .Columns.Add("FTColorway", GetType(String))
                            .Columns.Add("FTSizeBreakDown", GetType(String))
                            .Columns.Add("FTCustBarcodeNo", GetType(String))
                            .Columns.Add("FNQuantity", GetType(Integer))
                            .Columns.Add("FTBarCodeCarton", GetType(String))

                            '.Columns.Add("FNQuantityBal", GetType(Integer))
                        End With
                    End If
                    With _oDt
                        If .Rows.Count > 0 Then
                            .AcceptChanges()
                            If .Select("FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'").Length <= 0 Then
                                For Each R As DataRow In _dt.Rows
                                    .Rows.Add(R!FTPORef.ToString, R!FNHSysStyleId.ToString, HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text), R!FTOrderNo.ToString, R!FTProdTypeName.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString,
                                                                             R!FTCustBarcodeNo.ToString, R!FNQuantityBal.ToString, R!FTBarCodeCarton.ToString)
                                Next
                            Else
                                For Each R As DataRow In .Select("FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'")
                                    Dim Filter As String = ""
                                    Filter = "FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "' and FTOrderNo='" & R!FTOrderNo.ToString & "'"
                                    Filter &= " and FTColorway='" & R!FTColorway.ToString & "' and FTSizeBreakDown='" & R!FTSizeBreakDown.ToString & "'"
                                    For Each x As DataRow In _dt.Select(Filter)
                                        R!FNQuantity = x!FNQuantityBal.ToString
                                    Next
                                Next
                            End If
                            .AcceptChanges()
                        Else

                            For Each R As DataRow In _dt.Rows
                                .Rows.Add(R!FTPORef.ToString, R!FNHSysStyleId.ToString, HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text), R!FTOrderNo.ToString, R!FTProdTypeName.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString,
                                                                           R!FTCustBarcodeNo.ToString, R!FNQuantityBal.ToString, R!FTBarCodeCarton.ToString)
                            Next
                        End If
                    End With
                    Me.ogcdetail.DataSource = _oDt
                    Me.ogcdetail.RefreshDataSource()


                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub FTBarcodeCust_KeyDown(sender As Object, e As KeyEventArgs)
    '    Try
    '        If e.KeyCode = Keys.Enter Then
    '            If Me.FTOrderNo.Text = "" Then
    '                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNo_lbl.Text)
    '                Me.FTOrderNo.Focus()
    '                Exit Sub
    '            End If

    '            Dim _WHFGId As Integer = HI.Conn.SQLConn.GetField("SELECT TOP (1) FNHSysWHFGId  FROM  TCNMWarehouseFG WITH(NOLOCK) WHERE Isnull(FTStateSale,'') = '1'", Conn.DB.DataBaseName.DB_MASTER, "0")

    '            Dim _Cmd As String = ""
    '            Dim _dt As DataTable = ChekOnhand(_WHFGId, Me.FTOrderNo.Text, Me.FTStyleCode.Text)
    '            If _dt.Rows.Count <= 0 Then
    '                HI.MG.ShowMsg.mInfo("ไม่มีจำนวนของในคลัง...กรุณตรวจสอบ!!!", 150908002, Me.Text)
    '                Exit Sub
    '            End If

    '            Dim _oDt As DataTable = CType(ogcdetail.DataSource, DataTable)
    '            If IsNothing(_oDt) Then
    '                _oDt = New DataTable
    '                With _oDt
    '                    .Columns.Add("FNHSysStyleId", GetType(Integer))
    '                    .Columns.Add("FTStyleCode", GetType(String))
    '                    .Columns.Add("FTOrderNo", GetType(String))
    '                    .Columns.Add("FTProdTypeName", GetType(String))
    '                    .Columns.Add("FTColorway", GetType(String))
    '                    .Columns.Add("FTSizeBreakDown", GetType(String))
    '                    .Columns.Add("FTCustBarcodeNo", GetType(String))
    '                    .Columns.Add("FNQuantity", GetType(Integer))
    '                    .Columns.Add("FNQuantityBal", GetType(Integer))
    '                End With
    '            End If
    '            With _oDt
    '                If .Rows.Count > 0 Then
    '                    .AcceptChanges()
    '                    If .Select("FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text) & "'").Length <= 0 Then
    '                        For Each R As DataRow In _dt.Rows
    '                            If Me.FNQuantity.Value <= Integer.Parse("0" & R!FNQuantityBal.ToString) Then
    '                                .Rows.Add(R!FNHSysStyleId.ToString, R!FTStyleCode.ToString, R!FTOrderNo.ToString, R!FTProdTypeName.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString, _
    '                                                                         Me.FTBarcodeCust.Text, Me.FNQuantity.Value, R!FNQuantityBal.ToString)
    '                            Else
    '                                HI.MG.ShowMsg.mInfo("ยอดไม่พอจ่าย...กรุณาตรวจสอบ", 1509100933, Me.Text)
    '                                Exit For
    '                            End If
    '                        Next
    '                    Else
    '                        For Each R As DataRow In .Select("FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text) & "'")
    '                            If (R!FNQuantity + Me.FNQuantity.Value) <= Integer.Parse("0" & _dt.Rows(0)!FNQuantityBal.ToString) Then
    '                                R!FNQuantity += +Me.FNQuantity.Value
    '                            Else
    '                                HI.MG.ShowMsg.mInfo("ยอดไม่พอจ่าย...กรุณาตรวจสอบ", 1509100933, Me.Text)
    '                                Exit For
    '                            End If
    '                        Next
    '                    End If
    '                    .AcceptChanges()
    '                Else
    '                    For Each R As DataRow In _dt.Rows
    '                        If Me.FNQuantity.Value <= Integer.Parse("0" & R!FNQuantityBal.ToString) Then
    '                            .Rows.Add(R!FNHSysStyleId.ToString, R!FTStyleCode.ToString, R!FTOrderNo.ToString, R!FTProdTypeName.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString, _
    '                                                                 Me.FTBarcodeCust.Text, Me.FNQuantity.Value, R!FNQuantityBal.ToString)
    '                        Else
    '                            HI.MG.ShowMsg.mInfo("ยอดไม่พอจ่าย...กรุณาตรวจสอบ", 1509100933, Me.Text)
    '                            Exit For
    '                        End If
    '                    Next
    '                End If

    '            End With
    '            Me.ogcdetail.DataSource = _oDt
    '            Me.ogcdetail.RefreshDataSource()
    '            Me.FNQuantity.Value = 1
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Function ChekOnhand(WHFGId As Integer, StyleNo As String) As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT   '' as FTBarCodeCarton ,  OD.FTPORef,  TT.FNHSysWHFGId, TT.FTColorWay, TT.FTSizeBreakDown, TT.FTOrderNo,   WF.FTWHFGCode,  "
            _Cmd &= vbCrLf & "  PT.FTProdTypeCode , sum(TT.FNQuantity) AS FNQuantityBal , BC.FTCustBarcodeNo , SY.FNHSysStyleId , SY.FTStyleCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & " , PT.FTProdTypeNameTH as FTProdTypeName ,WF.FTWHFGNameTH AS FTWHFGName "
            Else
                _Cmd &= vbCrLf & " , PT.FTProdTypeNameEN as FTProdTypeName ,WF.FTWHFGNameEN AS FTWHFGName "
            End If
            _Cmd &= vbCrLf & " FROM   ("
            _Cmd &= vbCrLf & " SELECT        F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, SUM(Isnull(F.FNQuantity,0)) AS FNQuantity  "
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH (NOLOCK)   "
            _Cmd &= vbCrLf & "  GROUP BY F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo"
            _Cmd &= vbCrLf & " union all"
            _Cmd &= vbCrLf & " SELECT    T.FNHSysWHIdFGTo,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo,   SUM(D.FNQuantity) AS FNQuantity "
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton"
            _Cmd &= vbCrLf & " GROUP BY  T.FNHSysWHIdFGTo,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo "
            _Cmd &= vbCrLf & "    UNION ALL"
            _Cmd &= vbCrLf & " SELECT      HJ.FNHSysWHFGId, VA.FTColorway,VA.FTSizeBreakDown, AJ.FTOrderNo,  sum(AJ.FNQuantity) AS FNQuantity "
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG AS HJ WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTAdjustFGNo = AJ.FTAdjustFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FTCustBarcodeNo = VA.FTCustBarcodeNo and AJ.FTOrderNo = VA.FTOrderNo"
            _Cmd &= vbCrLf & " GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, VA.FTSizeBreakDown"
            _Cmd &= vbCrLf & "  UNION ALL"
            _Cmd &= vbCrLf & " SELECT      HJ.FNHSysWHFGId, VA.FTColorway,VA.FTSizeBreakDown, AJ.FTOrderNo,  sum(AJ.FNQuantity) AS FNQuantity "
            _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG AS HJ WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTReturnFGNo = AJ.FTReturnFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FNHSysStyleId = VA.FNHSysStyleId and AJ.FTOrderNo = VA.FTOrderNo"
            _Cmd &= vbCrLf & " and AJ.FTColorway = VA.FTColorway and AJ.FTSizeBreakDown = VA.FTSizeBreakDown"
            _Cmd &= vbCrLf & " GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, VA.FTSizeBreakDown"
            _Cmd &= vbCrLf & "  UNION All"
            _Cmd &= vbCrLf & " SELECT     T.FNHSysWHIdFG,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo,   - SUM(D.FNQuantity) AS FNQuantity "
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton"
            _Cmd &= vbCrLf & " GROUP BY  T.FNHSysWHIdFG,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo"
            _Cmd &= vbCrLf & "   UNION ALL"
            _Cmd &= vbCrLf & " SELECT      HJ.FNHSysWHFGId, VA.FTColorway,VA.FTSizeBreakDown, AJ.FTOrderNo, - sum(AJ.FNQuantity) AS FNQuantity "
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG AS HJ WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTIssueFGNo = AJ.FTIssueFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON   AJ.FTOrderNo = VA.FTOrderNo and AJ.FNHSysStyleId = VA.FNHSysStyleId"
            _Cmd &= vbCrLf & " and AJ.FTColorway = VA.FTColorway and AJ.FTSizeBreakDown = VA.FTSizeBreakDown"
            _Cmd &= vbCrLf & "where HJ.FTIssueFGNo <>'" & HI.UL.ULF.rpQuoted(Me.FTIssueFGNo.Text) & "'"
            _Cmd &= vbCrLf & " GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, VA.FTSizeBreakDown"
            _Cmd &= vbCrLf & " UNION All"
            _Cmd &= vbCrLf & " SELECT       FNHSysWHFGId,  FTColorway,FTSizeBreakDown,   FTOrderNo, - sum(FNQuantity) AS  FNQuantity"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS S WITH (NOLOCK)"
            _Cmd &= vbCrLf & "Where FTInvoiceNo like '%INVI%' "
            _Cmd &= vbCrLf & " Group by  FNHSysWHFGId,  FTColorway,FTSizeBreakDown,   FTOrderNo"
            _Cmd &= vbCrLf & " ) AS TT LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG AS WF WITH (NOLOCK) ON TT.FNHSysWHFGId = WF.FNHSysWHFGId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD WITH (NOLOCK) ON TT.FTOrderNo = OD.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS PT WITH (NOLOCK) ON OD.FNHSysProdTypeId = PT.FNHSysProdTypeId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS BC WITH(NOLOCK) ON TT.FTOrderNo = BC.FTOrderNo "
            _Cmd &= vbCrLf & " and TT.FTColorWay = BC.FTColorway and TT.FTSizeBreakDown = BC.FTSizeBreakDown "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS SY WITH(NOLOCK) ON OD.FNHSysStyleId = SY.FNHSysStyleId"
            _Cmd &= vbCrLf & "WHERE TT.FNHSysWHFGId=" & Integer.Parse("0" & WHFGId)
            _Cmd &= vbCrLf & "and SY.FTStyleCode ='" & HI.UL.ULF.rpQuoted(StyleNo) & "'"
            _Cmd &= vbCrLf & " group by OD.FTPORef,  TT.FNHSysWHFGId, TT.FTColorWay, TT.FTSizeBreakDown, TT.FTOrderNo,    WF.FTWHFGCode,  "
            _Cmd &= vbCrLf & " PT.FTProdTypeCode, BC.FTCustBarcodeNo, SY.FNHSysStyleId, SY.FTStyleCode"
            _Cmd &= vbCrLf & " , PT.FTProdTypeNameTH  ,WF.FTWHFGNameTH , PT.FTProdTypeNameEN ,WF.FTWHFGNameEN "
            _Cmd &= vbCrLf & "having sum(TT.FNQuantity) > 0"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)

            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function GetDetailBarcodeCust(BarCode As String, OrderNo As String) As DataTable
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT   Top 1     FTOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo, FTProdTypeCode, FNHSysStyleId, FTStyleCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTProdTypeNameTH as FTProdTypeName"
            Else
                _Cmd &= vbCrLf & ", FTProdTypeNameEN as FTProdTypeName"
            End If
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "]..V_GetDetailAdj"
            _Cmd &= vbCrLf & " WHERE   FTCustBarcodeNo ='" & HI.UL.ULF.rpQuoted(BarCode) & "'"
            _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

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
                                Dim _CmpH As String = "0"
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

    'Private Sub RepositoryFNQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNQuantity.EditValueChanging
    '    Try
    '        If e.NewValue > e.OldValue Then
    '            e.Cancel = True
    '        Else
    '            e.Cancel = False
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged
        Call VisibleWH()
    End Sub
    Private Sub VisibleWH()
        Try
            With Me.ogvdetail
                If .RowCount > 0 Then
                    Me.FNHSysWHFGId.Properties.Buttons(0).Enabled = False
                    Me.FNHSysWHFGId.Properties.ReadOnly = True
                    'Me.FNHSysStyleId.Properties.Buttons(0).Enabled = False
                    'Me.FNHSysStyleId.Properties.ReadOnly = True
                Else
                    Me.FNHSysWHFGId.Properties.Buttons(0).Enabled = True
                    Me.FNHSysWHFGId.Properties.ReadOnly = False
                    'Me.FNHSysStyleId.Properties.Buttons(0).Enabled = True
                    'Me.FNHSysStyleId.Properties.ReadOnly = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            Dim _Str As String = ""
            If e.KeyCode = Keys.Delete Then
                With ogvdetail
                    If .RowCount < 0 And .FocusedRowHandle <= -1 Then Exit Sub
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With
                End With
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTBarcodeCartonNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeCartonNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.FTBarcodeCartonNo.Text <> "" And Me.FNHSysWHFGId.Text <> "" Then
                    Dim _WHFGId As Integer = HI.Conn.SQLConn.GetField("SELECT TOP (1) FNHSysWHFGId  FROM  TCNMWarehouseFG WITH(NOLOCK) WHERE Isnull(FTStateSale,'') = '1' and FNHSysWHFGId=" & Integer.Parse(Me.FNHSysWHFGId.Properties.Tag.ToString), Conn.DB.DataBaseName.DB_MASTER, "0")

                    Dim _dt As DataTable = CheckOnhand(Me.FTBarcodeCartonNo.Text)
                    Dim _oDt As DataTable = CType(ogcdetail.DataSource, DataTable)
                    If IsNothing(_oDt) Then
                        _oDt = New DataTable
                        With _oDt
                            .Columns.Add("FTPORef", GetType(String))
                            .Columns.Add("FNHSysStyleId", GetType(Integer))
                            .Columns.Add("FTStyleCode", GetType(String))
                            .Columns.Add("FTOrderNo", GetType(String))
                            .Columns.Add("FTProdTypeName", GetType(String))
                            .Columns.Add("FTColorway", GetType(String))
                            .Columns.Add("FTSizeBreakDown", GetType(String))
                            .Columns.Add("FTCustBarcodeNo", GetType(String))
                            .Columns.Add("FNQuantity", GetType(Integer))
                            .Columns.Add("FTBarCodeCarton", GetType(String))

                        End With
                    End If
                    With _oDt
                        If .Rows.Count > 0 Then
                            .AcceptChanges()
                            If .Select("FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeCartonNo.Text) & "'").Length <= 0 Then
                                For Each R As DataRow In _dt.Rows
                                    .Rows.Add(R!FTPORef.ToString, R!FNHSysStyleId.ToString, R!FTStyleCode.ToString, R!FTOrderNo.ToString, R!FTProdTypeName.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString,
                                                                            R!FTCustBarcodeNo.ToString, R!FNQuantityBal.ToString, Me.FTBarcodeCartonNo.Text)
                                Next
                            Else
                                For Each R As DataRow In .Select("FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeCartonNo.Text) & "'")
                                    Dim Filter As String = ""
                                    Filter = "FTStyleCode='" & R!FTStyleCode.ToString & "' and FTOrderNo='" & R!FTOrderNo.ToString & "'"
                                    Filter &= " and FTColorway='" & R!FTColorway.ToString & "' and FTSizeBreakDown='" & R!FTSizeBreakDown.ToString & "'"
                                    For Each x As DataRow In _dt.Select(Filter)
                                        R!FNQuantity = x!FNQuantityBal.ToString
                                    Next
                                Next
                            End If
                            .AcceptChanges()
                        Else

                            For Each R As DataRow In _dt.Rows
                                .Rows.Add(R!FTPORef.ToString, R!FNHSysStyleId.ToString, R!FTStyleCode.ToString, R!FTOrderNo.ToString, R!FTProdTypeName.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString,
                                                                           R!FTCustBarcodeNo.ToString, R!FNQuantityBal.ToString, Me.FTBarcodeCartonNo.Text)
                            Next
                        End If
                    End With
                    Me.ogcdetail.DataSource = _oDt
                    Me.ogcdetail.RefreshDataSource()


                End If
                Me.FTBarcodeCartonNo.Focus()
                Me.FTBarcodeCartonNo.SelectAll()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Overloads Function CheckOnhand(ByVal _BarcodeCarton As String) As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT '0' AS FTSelect , TT.FNHSysWHFGId , '' as FTCustBarcodeNo  , St.FNHSysStyleId, TT.FTOrderNo,OSC.FNHSysContinentId,OSC.FNHSysCountryId,OSC.FNHSysProvinceId,OSC.FNHSysShipModeId, TT.FNQuantity,  TT.FNQuantityOut, WF.FTWHFGCode, ST.FTStyleCode , SFG.FTBarCodeCarton,SFG.FTPackNo"
            _Cmd &= vbCrLf & ",ISNULL (( SELECT TOP 1 STUFF "
            _Cmd &= vbCrLf & "((SELECT  ', ' + t2.FTColorway "
            _Cmd &= vbCrLf & "FROM      (SELECT        c.FTBarCodeCarton, d.FTColorway"
            _Cmd &= vbCrLf & "FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS c INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS d ON c.FTPackNo = d.FTPackNo AND c.FNCartonNo = d.FNCartonNo"
            _Cmd &= vbCrLf & "GROUP BY c.FTBarCodeCarton, d.FTColorway) t2"
            _Cmd &= vbCrLf & "WHERE   t2.FTBarCodeCarton =  SFG.FTBarCodeCarton  FOR XML PATH('')), 1, 2, '')  )"
            _Cmd &= vbCrLf & ",'') AS FTColorway "

            _Cmd &= vbCrLf & ",ISNULL (( SELECT TOP 1 STUFF"
            _Cmd &= vbCrLf & "((SELECT  ', ' + t2.FTSizeBreakDown "
            _Cmd &= vbCrLf & "FROM      (SELECT        c.FTBarCodeCarton, d.FTSizeBreakDown"
            _Cmd &= vbCrLf & "FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS c INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS d ON c.FTPackNo = d.FTPackNo AND c.FNCartonNo = d.FNCartonNo"
            _Cmd &= vbCrLf & "GROUP BY c.FTBarCodeCarton, d.FTSizeBreakDown) t2"
            _Cmd &= vbCrLf & "WHERE   t2.FTBarCodeCarton =  SFG.FTBarCodeCarton  FOR XML PATH('')), 1, 2, '')  )"
            _Cmd &= vbCrLf & ",'') AS FTSizeBreakDown "

            _Cmd &= vbCrLf & " , PT.FTProdTypeCode , (TT.FNQuantity - TT.FNQuantityOut) AS FNQuantityBal   ,isnull(sum (PPC.FNQuantity1),0) as FNQuantityBundle" '-TT.TransFNQtyBundle

            _Cmd &= vbCrLf & " , OD.FTPORef ,count(SFG.TotalCarton) as FNCartonNo ,isnull(SFG.FNCarton,0) AS FNCarton,isnull(PPC.PP,0) as FNPackPerCarton ,PPC.FTCartonCode as FNHSysCartonId ,(Convert(varchar(50),PPC.FNWidth) + ' X ' +  Convert(varchar(50),PPC.FNLength) + ' X ' +  Convert(varchar(50),PPC.FNHeight) + '  ' + Convert(varchar(50),PPC.FTUnitCode)) AS FTDimension"
            _Cmd &= vbCrLf & " ,ODT.FTNikePOLineItem as FTNikePOLineItem,isnull(ODT.QrderQuantity,0) as FNQuantity,isnull(ODT.FNQuantityExtra,0)as FNQuantityExtra ,isnull(ODT.FNGarmentQtyTest,0) as FNGarmentQtyTest ,isnull(ODT.FNGrandQuantity,0) as FNGrandQuantity ,isnull(PC.FNQuantityBundle,0) as ToTalBundle,(PPC.WLH * count(SFG.TotalCarton)) as CBM"
            _Cmd &= vbCrLf & ",ISNULL((SELECT  convert(varchar(10),convert(date,min(SS.FDShipDate)),103) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=TT.FTOrderNo),null) AS FDShipDate "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & " , PT.FTProdTypeNameTH as FTProdTypeName ,WF.FTWHFGNameTH AS FTWHFGName "
            Else
                _Cmd &= vbCrLf & " , PT.FTProdTypeNameEN as FTProdTypeName ,WF.FTWHFGNameEN AS FTWHFGName "
            End If

            _Cmd &= vbCrLf & "FROM           (SELECT FG.FNHSysWHFGId, FG.FTColorWay,  FG.FTOrderNo,( FG.FNQuantity)As FNQuantity,   ISNULL(T.FNQuantity, 0) AS FNQuantityOut,ISNULL(T.FNQuantityBundle, 0) AS TransFNQtyBundle,FG.FTBarCodeCarton,FG.FNCartonNo,T.FNCartonNo AS FNCarton"
            _Cmd &= vbCrLf & " FROM            (SELECT        FF.FNHSysWHFGId, FF.FTColorWay,  FF.FTOrderNo,  sum(Isnull(FF.FNQuantity,0))  AS FNQuantity   ,(FF.FNCartonNo) AS  FNCartonNo,FF.FTBarCodeCarton"
            _Cmd &= vbCrLf & "FROM( SELECT        F.FNHSysWHFGId, F.FTColorWay,  F.FTOrderNo, sum(Isnull(F.FNQuantity,0)) AS FNQuantity ,(f.FNCartonNo)as FNCartonNo,f.FTBarCodeCarton"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH (NOLOCK)  "
            _Cmd &= vbCrLf & " GROUP BY F.FNHSysWHFGId, F.FTColorWay, F.FTOrderNo,f.FTBarCodeCarton,f.FNCartonNo"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT       HJ.FNHSysWHFGId, VA.FTColorway, AJ.FTOrderNo,  sum(AJ.FNQuantity) AS FNQuantity  ,f.FNCartonNo,f.FTBarCodeCarton"
            _Cmd &= vbCrLf & "FROM        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG AS HJ WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTAdjustFGNo = AJ.FTAdjustFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FTCustBarcodeNo = VA.FTCustBarcodeNo and AJ.FTOrderNo = VA.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F ON AJ.FTOrderNo=F.FTOrderNo  "
            _Cmd &= vbCrLf & "  GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, f.FNCartonNo,f.FTBarCodeCarton"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT         HJ.FNHSysWHFGId, VA.FTColorway, AJ.FTOrderNo, sum(AJ.FNQuantity) AS FNQuantity ,f.FNCartonNo,f.FTBarCodeCarton"
            _Cmd &= vbCrLf & "FROM        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG AS HJ WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTReturnFGNo = AJ.FTReturnFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FNHSysStyleId = VA.FNHSysStyleId and AJ.FTOrderNo = VA.FTOrderNo and AJ.FTColorway = VA.FTColorway   LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F ON AJ.FTOrderNo=F.FTOrderNo "
            _Cmd &= vbCrLf & "   GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway,f.FNCartonNo,f.FTBarCodeCarton"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT    T.FNHSysWHIdFGTo,  FG.FTColorWay,   FG.FTOrderNo,   (D.FNQuantity) AS FNQuantity ,D.FNCartonNo,fg.FTBarCodeCarton"
            _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo. TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton"
            '_Cmd &= vbCrLf & "where Isnull(T.FTStateApprove,'0') = '1' "
            _Cmd &= vbCrLf & "GROUP BY  T.FNHSysWHIdFGTo,  FG.FTColorWay,   FG.FTOrderNo,D.FNCartonNo ,fg.FTBarCodeCarton,D.FNQuantity) AS FF"
            _Cmd &= vbCrLf & "GROUP BY FF.FNHSysWHFGId, FF.FTColorWay,  FF.FTOrderNo ,FF.FNCartonNo ,ff.FTBarCodeCarton,FF.FNQuantity ) AS FG"

            _Cmd &= vbCrLf & "LEFT OUTER JOIN  "
            _Cmd &= vbCrLf & " (SELECT     XX.FNHSysWHFGId,  XX.FTColorway,   XX.FTOrderNo, (XX.FNQuantity) AS  FNQuantity , XX.FTBarCodeCarton ,(xx.FNCartonNo)  as FNCartonNo,(xx.FNQuantityBundle) AS FNQuantityBundle"
            _Cmd &= vbCrLf & " FROM("
            _Cmd &= vbCrLf & "SELECT       S.FNHSysWHFGId,  S.FTColorway,   S.FTOrderNo, sum(S.FNQuantity) AS  FNQuantity,fg.FTBarCodeCarton,fg.FNCartonNo,A.FNQuantity as FNQuantityBundle"
            _Cmd &= vbCrLf & "FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS S WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON S.FTOrderNo=FG.FTOrderNo INNER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON FG.FTOrderNo=A.FTOrderNo and FG.FNCartonNo=A.FNCartonNo and FG.FTPackNo=A.FTPackNo " 'and FG.FTSizeBreakDown=A.FTSizeBreakDown "
            _Cmd &= vbCrLf & " WHERE S.FTInvoiceNo Like '%INVI%'"
            _Cmd &= vbCrLf & "Group by  S.FNHSysWHFGId,  S.FTColorway,   S.FTOrderNo,fg.FTBarCodeCarton,fg.FNCartonNo,A.FNQuantity"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT      HJ.FNHSysWHFGId, VA.FTColorway, AJ.FTOrderNo, sum(AJ.FNQuantity) AS FNQuantity ,fg.FTBarCodeCarton ,fg.FNCartonNo,A.FNQuantity as FNQuantityBundle"
            _Cmd &= vbCrLf & "FROM        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG AS HJ WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTIssueFGNo = AJ.FTIssueFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FNHSysStyleId = VA.FNHSysStyleId and AJ.FTOrderNo = VA.FTOrderNo and AJ.FTColorway = VA.FTColorway   LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON AJ.FTOrderNo=FG.FTOrderNo INNER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON FG.FTOrderNo=A.FTOrderNo and FG.FNCartonNo=A.FNCartonNo  and FG.FTPackNo=A.FTPackNo " 'and FG.FTSizeBreakDown=A.FTSizeBreakDown  "
            _Cmd &= vbCrLf & " GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, fg.FTBarCodeCarton,fg.FNCartonNo,A.FNQuantity"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT    FG.FNHSysWHFGId,  FG.FTColorWay,   FG.FTOrderNo,  (FG.FNQuantity) AS FNQuantity ,fg.FTBarCodeCarton ,FG.FNCartonNo,(A.FNQuantity) as FNQuantityBundle"
            _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton and D.FNCartonNo=FG.FNCartonNo  INNER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON FG.FTOrderNo=A.FTOrderNo and FG.FNCartonNo=A.FNCartonNo and FG.FTPackNo=A.FTPackNo  and FG.FNCartonNo=A.FNCartonNo " 'and FG.FTSizeBreakDown=A.FTSizeBreakDown
            '_Cmd &= vbCrLf & "where Isnull(T.FTStateApprove,'0') = '1' "
            _Cmd &= vbCrLf & "GROUP BY  FG.FNHSysWHFGId,  FG.FTColorWay,   FG.FTOrderNo,fg.FTBarCodeCarton ,FG.FNCartonNo,FG.FNQuantity ,A.FNQuantity)as XX "
            _Cmd &= vbCrLf & "group by   XX.FNHSysWHFGId,  XX.FTColorway,   XX.FTOrderNo,XX.FTBarCodeCarton,XX.FNQuantity,xx.FNCartonNo,xx.FNQuantityBundle"
            _Cmd &= vbCrLf & ")  AS T ON FG.FTOrderNo = T.FTOrderNo AND FG.FTColorWay = T.FTColorway   and FG.FNQuantity=T.FNQuantity and FG.FNHSysWHFGId = T.FNHSysWHFGId and FG.FTBarCodeCarton=T.FTBarCodeCarton  and FG.FNCartonNo=T.FNCartonNo" 'AND FG.FTSizeBreakDown = T.FTSizeBreakDown 
            _Cmd &= vbCrLf & " GROUP BY FG.FNHSysWHFGId, FG.FTColorWay,  FG.FTOrderNo,FG.FNQuantity ,T.FNQuantity, T.FNQuantityBundle,FG.FTBarCodeCarton ,FG.FNCartonNo,T.FNCartonNo"
            _Cmd &= vbCrLf & ") AS TT "

            _Cmd &= vbCrLf & "LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "(SELECT  SFG.FTOrderNo ,TFC.FTTransferFGNo,SFG.FNHSysWHFGId ,SFG.FTColorway,sum(SFG.FNQuantity) AS FNQuantity,SFG.FTBarCodeCarton,SFG.FTPackNo , (SFG.FNCartonNo)as FNCarton ,count(TFC.FNCartonNo) AS TranferCartonNo ,count(SFG.FTBarCodeCarton)as TotalCarton"
            _Cmd &= vbCrLf & " FROM"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG WITH(NOLOCK) "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS M WITH(NOLOCK) ON SFG.FTOrderNo=M.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG AS WF WITH (NOLOCK) ON SFG.FNHSysWHFGId = WF.FNHSysWHFGId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN ("
            _Cmd &= vbCrLf & "select T.FNHSysWHIdFGTo,T.FTTransferFGNo,T.FTStateApprove,F.FNCartonNo,F.FNQuantity,SFG.FTBarCodeCarton,F.FTPackNo"
            _Cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T inner join"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail as F  on  T.FTTransferFGNo=F.FTTransferFGNo inner join"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG WITH(NOLOCK) on  F.FTBarCodeCarton=SFG.FTBarCodeCarton and F.FNCartonNo=SFG.FNCartonNo "
            ' _Cmd &= vbCrLf & "  WHERE Isnull(T.FTStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & " )AS TFC ON SFG.FTBarCodeCarton = TFC.FTBarCodeCarton AND SFG.FNCartonNo=TFC.FNCartonNo"
            _Cmd &= vbCrLf & "  GROUP BY SFG.FTOrderNo,TFC.FTTransferFGNo,SFG.FTColorway,SFG.FNHSysWHFGId ,SFG.FTBarCodeCarton,SFG.FTPackNo,SFG.FNCartonNo"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & " SELECT TFC.FTOrderNo,TFC.FTTransferFGNo,TFC.FNHSysWHIdFGTO ,TFC.FTColorway ,sum(TFC.FNQuantity) AS FNQuantity,TFC.FTBarCodeCarton,TFC.FTPackNo,( TFC.FNCartonNo)AS FNCartonNo ,(TFC.KKK) AS TranferCartonNo1 ,count(TFC.FNCartonNo)-(TFC.KKK) AS TotalCarton1 "
            _Cmd &= vbCrLf & "FROM ("
            _Cmd &= vbCrLf & " SELECT SFG.FTOrderNo,T.FTTransferFGNo,T.FNHSysWHIdFGTo,SFG.FTColorWay,sum(SFG.FNQuantity) as FNQuantity,SFG.FTBarCodeCarton,SFG.FTPackNo,(F.FNCartonNo)AS FFNCartonNo,sfg.FNCartonNo,sfg.FNCartonNo -f.FNCartonNo as KKK"
            _Cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T inner join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail as F  on  T.FTTransferFGNo=F.FTTransferFGNo inner join"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG WITH(NOLOCK) on   F.FTBarCodeCarton=SFG.FTBarCodeCarton and F.FNCartonNo=SFG.FNCartonNo "
            '_Cmd &= vbCrLf & "  WHERE Isnull(T.FTStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "  GROUP BY SFG.FTOrderNo,T.FTTransferFGNo,T.FNHSysWHIdFGTo,SFG.FTColorWay,SFG.FTBarCodeCarton,SFG.FTPackNo,F.FNCartonNo,SFG.FNCartonNo"
            _Cmd &= vbCrLf & ")AS TFC"
            _Cmd &= vbCrLf & " GROUP BY TFC.FTOrderNo,TFC.FTTransferFGNo,TFC.FNHSysWHIdFGTO ,TFC.FTColorway,TFC.FTBarCodeCarton,TFC.FTPackNo,TFC.KKK,TFC.FNCartonNo"
            _Cmd &= vbCrLf & ")AS SFG  ON TT.FTOrderNo =SFG.FTOrderNo    and TT.FTColorWay =SFG.FTColorWay and TT.FNQuantity =SFG.FNQuantity and TT.FNHSysWHFGId=SFG.FNHSysWHFGId And TT.FTBarCodeCarton= SFG.FTBarCodeCarton and TT.FNCartonNo=SFG.FNCarton"

            _Cmd &= vbCrLf & "LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG AS WF WITH (NOLOCK) ON TT.FNHSysWHFGId = WF.FNHSysWHFGId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD WITH (NOLOCK) ON TT.FTOrderNo = OD.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS PT WITH (NOLOCK) ON OD.FNHSysProdTypeId = PT.FNHSysProdTypeId LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS TD WITH (NOLOCK) ON SFG.FTBarCodeCarton =TD.FTBarCodeCarton and SFG.FNCarton  =TD.FNCartonNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) ON TD.FTTransferFGNo=T.FTTransferFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK) ON OD.FNHSysStyleId = ST.FNHSysStyleId"
            '------------------------------------------
            _Cmd &= vbCrLf & "LEFT OUTER JOIN  ("
            _Cmd &= vbCrLf & "SELECT SFG.FNHSysWHFGId,SFG.FTOrderNo  ,A.FTColorway ,sum(SFG.FNQuantity)as FNQuantity,SUM(AA.FNScanQuantity) as FNQuantity1 ,C.FTCartonCode ,A.FNCartonNo ,A.FTPackNo,A.FNPackPerCarton as PP,sfg.FTBarCodeCarton,A.FTSubOrderNo"
            _Cmd &= vbCrLf & ",(Convert(numeric(18,2),C.FNWidth)) as FNWidth ,(Convert(numeric(18,2),C.FNLength))as FNLength ,(Convert(numeric(18,2),C.FNHeight)) as FNHeight,U.FTUnitCode"
            _Cmd &= vbCrLf & ",((Convert(numeric(18,2),C.FNWidth*C.FNLength*C.FNHeight/1000000000))) AS WLH"
            _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS AA on A.FTOrderNo=AA.FTOrderNo and A.FTPackNo=AA.FTPackNo and A.FNCartonNo=AA.FNCartonNo and A.FTColorway=AA.FTColorway and A.FTSizeBreakDown=AA.FTSizeBreakDown and A.FTSubOrderNo=AA.FTSubOrderNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG ON A.FTOrderNo=SFG.FTOrderNo and A.FTPackNo=SFG.FTPackNo and A.FNCartonNo=SFG.FNCartonNo and A.FTSizeBreakDown=SFG.FTSizeBreakDown "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "] .dbo.TCNMCarton AS C ON A.FNHSysCartonId=C.FNHSysCartonId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "] .dbo.TCNMUnit as U ON C.FNHSysUnitId=U.FNHSysUnitId"
            _Cmd &= vbCrLf & "  GROUP BY SFG.FNHSysWHFGId,SFG.FTOrderNo ,A.FTColorway ,A.FNCartonNo ,C.FTCartonCode ,A.FTPackNo,A.FNPackPerCarton,sfg.FTBarCodeCarton,A.FTSubOrderNo,C.FNWidth,C.FNLength,C.FNHeight,U.FTUnitCode"
            _Cmd &= vbCrLf & ")AS PPC ON TT.FTOrderNo = PPC.FTOrderNo and SFG.FTPackNo=PPC.FTPackNo and SFG.FTBarCodeCarton=PPC.FTBarCodeCarton and SFG.FTColorWay=PPC.FTColorway " 'and TT.FNHSysWHFGId=PPC.FNHSysWHFGId"
            '------------------------------------------------
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  ("
            _Cmd &= vbCrLf & "  SELECT ODT.FNHSysWHFGId,ODT.FTNikePOLineItem,ODT.FTOrderNo,ODT.FTColorway ,SUM(ODT.QrderQuantity)-sum(isnull(OT.QrderQuantity,'0')) as QrderQuantity, SUM(ODT.FNQuantityExtra)-sum(isnull(OT.FNQuantityExtra,'0')) AS FNQuantityExtra,isnull( SUM(ODT.FNGarmentQtyTest),0)-sum(isnull(OT.FNGarmentQtyTest,'0'))  AS FNGarmentQtyTest, SUM(ODT.FNGrandQuantity)-sum(isnull(OT.FNGrandQuantity,'0')) AS FNGrandQuantity,ODT.FNCartonNo,ODT.FTBarCodeCarton,ODT.FTPackNo"
            _Cmd &= vbCrLf & "     FROM ("
            _Cmd &= vbCrLf & "  SELECT  sfg.FNHSysWHFGId, isnull(SBD.FTNikePOLineItem,'') AS FTNikePOLineItem,SBD.FTOrderNo,SBD.FTColorway, SUM(SBD.FNQuantity) AS QrderQuantity, SUM(SBD.FNQuantityExtra) AS FNQuantityExtra,isnull( SUM(SBD.FNGarmentQtyTest),0) AS FNGarmentQtyTest, SUM(SBD.FNGrandQuantity) AS FNGrandQuantity,A.FNCartonNo,SFG.FTBarCodeCarton,SFG.FTPackNo"
            _Cmd &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG ON SBD.FTOrderNo=SFG.FTOrderNo and SBD.FTSizeBreakDown=SFG.FTSizeBreakDown and SBD.FTColorway=SFG.FTColorway LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON SBD.FTOrderNo=A.FTOrderNo and SFG.FTColorway=A.FTColorway and SFG.FNCartonNo=A.FNCartonNo and SBD.FTSubOrderNo=A.FTSubOrderNo and SFG.FTPackNo= A.FTPackNo and SFG.FTSizeBreakDown=A.FTSizeBreakDown"
            _Cmd &= vbCrLf & "  	GROUP BY   sfg.FNHSysWHFGId,SBD.FTOrderNo,SBD.FTColorway,SBD.FTNikePOLineItem,SFG.FTBarCodeCarton,SFG.FTPackNo,A.FNCartonNo"
            _Cmd &= vbCrLf & " union all  ( "
            _Cmd &= vbCrLf & "  SELECT   T.FNHSysWHIdFGTo,isnull(SBD.FTNikePOLineItem,'') AS FTNikePOLineItem,SBD.FTOrderNo,SBD.FTColorway, SUM(SBD.FNQuantity) AS QrderQuantity, SUM(SBD.FNQuantityExtra) AS FNQuantityExtra,isnull( SUM(SBD.FNGarmentQtyTest),0) AS FNGarmentQtyTest, SUM(SBD.FNGrandQuantity) AS FNGrandQuantity,SFG.FNCartonNo,SFG.FTBarCodeCarton,SFG.FTPackNo"
            _Cmd &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG ON SBD.FTOrderNo=SFG.FTOrderNo and SBD.FTSizeBreakDown=SFG.FTSizeBreakDown and SBD.FTColorway=SFG.FTColorway LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo. TFGTransferFG_Detail AS D WITH (NOLOCK) ON SFG.FTPackNo=D.FTPackNo and SFG.FTBarCodeCarton=D.FTBarCodeCarton and SFG.FNCartonNo=D.FNCartonNo  LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) on D.FTTransferFGNo = T.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON SFG.FTOrderNo=A.FTOrderNo and SFG.FNCartonNo=A.FNCartonNo and SFG.FTPackNo=A.FTPackNo  and SFG.FNCartonNo=A.FNCartonNo and SBD.FTSubOrderNo=A.FTSubOrderNo and SFG.FTSizeBreakDown=A.FTSizeBreakDown"
            ' _Cmd &= vbCrLf & "  WHERE Isnull(T.FTStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "  	GROUP BY T.FNHSysWHIdFGTo, SBD.FTOrderNo,SBD.FTColorway,SBD.FTNikePOLineItem,SFG.FNCartonNo,SFG.FTBarCodeCarton,SFG.FTPackNo))AS ODT"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  ("
            _Cmd &= vbCrLf & "  SELECT   T.FNHSysWHIdFG,SBD.FTOrderNo,SBD.FTColorway, (SBD.FNQuantity) AS QrderQuantity, SUM(SBD.FNQuantityExtra) AS FNQuantityExtra,isnull( SUM(SBD.FNGarmentQtyTest),0) AS FNGarmentQtyTest, SUM(SBD.FNGrandQuantity) AS FNGrandQuantity,SFG.FNCartonNo,SFG.FTPackNo"
            _Cmd &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG ON SBD.FTOrderNo=SFG.FTOrderNo and SBD.FTSizeBreakDown=SFG.FTSizeBreakDown and SBD.FTColorway=SFG.FTColorway LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo. TFGTransferFG_Detail AS D WITH (NOLOCK) ON SFG.FTPackNo=D.FTPackNo and SFG.FTBarCodeCarton=D.FTBarCodeCarton and SFG.FNCartonNo=D.FNCartonNo  LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) on D.FTTransferFGNo = T.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON SFG.FTOrderNo=A.FTOrderNo and SFG.FNCartonNo=A.FNCartonNo and SFG.FTPackNo=A.FTPackNo  and SFG.FNCartonNo=A.FNCartonNo and SBD.FTSubOrderNo=A.FTSubOrderNo"
            '  _Cmd &= vbCrLf & "  WHERE Isnull(T.FTStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "  	GROUP BY T.FNHSysWHIdFG, SBD.FTOrderNo,SBD.FTColorway,SFG.FNCartonNo,SFG.FTPackNo,SBD.FNQuantity"
            _Cmd &= vbCrLf & ")AS OT ON  ODT.FTOrderNo = OT.FTOrderNo  and ODT.FTColorWay=OT.FTColorway  and ODT.FTPackNo=OT.FTPackNo and ODT.FNHSysWHFGId=OT .FNHSysWHIdFG and ODT.FNCartonNo=OT.FNCartonNo"
            _Cmd &= vbCrLf & "  group by ODT.FNHSysWHFGId,ODT.FTNikePOLineItem,ODT.FTOrderNo,ODT.FTColorway,ODT.FNCartonNo,ODT.FTBarCodeCarton,ODT.FTPackNo"
            _Cmd &= vbCrLf & ")AS ODT ON  TT.FTOrderNo = ODT.FTOrderNo  and TT.FTColorWay=ODT.FTColorway   and SFG.FNCarton=ODT.FNCartonNo and SFG.FTPackNo=ODT.FTPackNo and sfg.FTBarCodeCarton=odt.FTBarCodeCarton and SFG.FNHSysWHFGId=odt.FNHSysWHFGId"
            '------------------------------------------------------------------
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  ("
            _Cmd &= vbCrLf & "  SELECT FG.FNHSysWHFGId,A.FTOrderNo ,SUM(A.FNScanQuantity) AS FNQuantityBundle,A.FTColorway,A.FTSubOrderNo,A.FTPackNo,FG.FTBarCodeCarton,FG.FNCartonNo"
            _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A inner join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON A.FTOrderNo=FG.FTOrderNo and A.FTColorway=FG.FTColorWay and A.FTSizeBreakDown=FG.FTSizeBreakDown   "
            _Cmd &= vbCrLf & "  	GROUP BY FG.FNHSysWHFGId,A.FTOrderNo,A.FTColorway,A.FTSubOrderNo,A.FTPackNo,FG.FTBarCodeCarton,FG.FNCartonNo" ') AS PC ON TT.FTOrderNo = PC.FTOrderNo  and TT.FTColorWay=PC.FTColorway  and ODT.FTSubOrderNo=PC.FTSubOrderNo and SFG.FTPackNo=PC.FTPackNo  and PPC.FTSizeBreakDown = PC.FTSizeBreakDown "
            _Cmd &= vbCrLf & ") AS PC ON TT.FTOrderNo = PC.FTOrderNo  and TT.FTColorWay=PC.FTColorway   and PPC.FTPackNo=PC.FTPackNo    and PPC.FTBarCodeCarton=PC.FTBarCodeCarton and PPC.FNCartonNo=PC.FNCartonNo and PPC.FTSubOrderNo=PC.FTSubOrderNo" 'and SFG.FNHSysWHFGId=PC.FNHSysWHFGId
            '------------------------------------------------------------------
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  ("
            _Cmd &= vbCrLf & " select OS.FTOrderNo,OS.FTSubOrderNo,C.FTContinentCode as FNHSysContinentId,CT.FTCountryCode as FNHSysCountryId,P.FTProvinceCode AS FNHSysProvinceId,S.FTShipModeCode as FNHSysShipModeId"
            _Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub as OS INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "] .dbo.TCNMContinent as C ON OS.FNHSysContinentId=C.FNHSysContinentId INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "] .dbo.TCNMCountry AS CT ON OS.FNHSysCountryId=CT.FNHSysCountryId and C.FNHSysContinentId=CT.FNHSysContinentId INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "] .dbo.TCNMShipMode AS S ON OS.FNHSysShipModeId=S.FNHSysShipModeId INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "] .dbo.TCMMProvince AS P ON OS.FNHSysProvinceId=P.FNHSysProvinceId"
            _Cmd &= vbCrLf & "  )AS OSC ON TT.FTOrderNo=OSC.FTOrderNo and PPC.FTSubOrderNo=OSC.FTSubOrderNo"
            _Cmd &= vbCrLf & " WHERE SFG.FTBarCodeCarton ='" & _BarcodeCarton & "'"


            _Cmd &= vbCrLf & " and ODT.QrderQuantity >0"
            _Cmd &= vbCrLf & "GROUP BY TT.FNHSysWHFGId, TT.FTOrderNo , St.FNHSysStyleId, TT.FNQuantity, TT.FNQuantityOut, WF.FTWHFGCode, ST.FTStyleCode , SFG.FTBarCodeCarton ,SFG.FTPackNo  ,SFG.FNCarton,TT.FNCarton,PPC.FNWidth,PPC.FNLength,PPC.FNHeight,PPC.FTUnitCode" ', TT.FTColorWay, TT.FTSizeBreakDown
            _Cmd &= vbCrLf & ",PT.FTProdTypeCode, OD.FTPORef  ,PPC.PP,ODT.FTNikePOLineItem,ODT.QrderQuantity   ,PPC.FTCartonCode ,ODT.FNQuantityExtra ,ODT.FNGarmentQtyTest ,ODT.FNGrandQuantity,PC.FNQuantityBundle,OSC.FNHSysContinentId,OSC.FNHSysCountryId,OSC.FNHSysProvinceId,OSC.FNHSysShipModeId,PPC.WLH"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",PT.FTProdTypeNameTH,WF.FTWHFGNameTH"
            Else
                _Cmd &= vbCrLf & ",PT.FTProdTypeNameEN,WF.FTWHFGNameEN"
            End If

            _Cmd &= vbCrLf & "ORDER BY  FTSizeBreakDown,FTColorway desc,ODT.FTNikePOLineItem desc "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
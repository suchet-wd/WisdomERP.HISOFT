Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Public Class wMEDRecieve
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MEDC
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wRecievePopup

    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _ReceiveUnitDrug As wReceiveUnitDrug


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Call PrepareForm()
        ' Add any initialization after the InitializeComponent() call.

        _AddItemPopup = New wRecievePopup
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        End Try



        _ReceiveUnitDrug = New wReceiveUnitDrug
        HI.TL.HandlerControl.AddHandlerObj(_ReceiveUnitDrug)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ReceiveUnitDrug.Name.ToString.Trim, _ReceiveUnitDrug)
        Catch ex As Exception
        End Try

        'Me.FNHSysCmpId.Properties.Tag = HI.ST.SysInfo.CmpID
        'Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode



    End Sub

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


#Region "Procedure"

    Private Property ogcpricehistory As Object

    Private Sub PrepareForm()

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
        _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
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
        'MsgBox("1 " & _Str.ToString)
        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        'MsgBox("2 " & _Dt.Rows.Count.ToString)

        Dim _FieldName As String = ""
        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns
                _FieldName = Col.ColumnName.ToString
                'MsgBox("3 " & _FieldName.ToString)
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

        Me.FNHSysCmpId.Text = ""


        Me.LoadDataDetail()
        _ProcLoad = False
    End Sub



    Public Sub DefaultsData()
        _FormLoad = True
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
        Me.LoadDataDetail()
        _FormLoad = False
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

    Private Function VerrifyData() As Boolean
        Try
            Dim _Cmd As String = ""
            If Me.FTMEDRcvNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTMEDRcvNo_lbl.Text)
                Me.FTMEDRcvNo.Focus()
                Return False
            End If

 

            Return True
        Catch ex As Exception
            Return False
        End Try
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

                                If CType(Obj, DevExpress.XtraEditors.ButtonEdit).Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else

                                    _Key = .Text

                                    '_Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    'Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    'If _dt.Rows.Count <= 0 Then
                                    '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                    '    Obj.Focus()
                                    '    Return False
                                    'End If
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
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser"), UCase("FNHSysCmpId")
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
                                Case UCase("FNHSysCmpId")
                                    _Values &= "'" & HI.ST.SysInfo.CmpID.ToString & "'"
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
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser"), UCase("FNHSysCmpId")
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
                                Case UCase("FNHSysCmpId")
                                    _Values &= _FieldName & "='" & HI.ST.SysInfo.CmpID.ToString & "'"
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

   

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MEDC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  TMECTRecieve  WHERE FTMEDRcvNo='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  TMECTRecieve_Detail WHERE FTMEDRcvNo='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  HITECH_MEDICAL.dbo.TMECTRecieve  WHERE FTMEDRcvNo ='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'")
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
        Me.LoadDataDetail()
    End Sub

    Private Sub FormRefresh()
        _FormLoad = True
        HI.TL.HandlerControl.ClearControl(Me)

        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
        _FormLoad = False
    End Sub

#End Region

    
    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerrifyData() Then
                If SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.LoadDataDetail()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmAdd_Click(sender As Object, e As EventArgs) Handles ocmAddDT.Click
        Try
            If ChkIss() Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถรับยาเพิ่มได้ เนื่องจากเอกสารใบนี้ได้มีการจ่ายไปแล้ว...", 1505110002, Me.Text, "", MessageBoxIcon.Stop)
                Exit Sub
            End If

            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _oDt2 As DataTable
            If Not (VerrifyData()) Then Exit Sub
            If Not (SaveData()) Then Exit Sub
            If Me.FTMECPurchaseNo.Text <> "" Then


                _Cmd = "  SELECT      P.FTMECPurchaseNo, P.FNHSysDrugId, P.FNHSysDrugUnitId, D.FTDrugCode,   U.FTDrugUnitCode"
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", D.FTDrugNameTH AS FTDrugName "
                Else
                    _Cmd &= vbCrLf & ", D.FTDrugNameEN AS FTDrugName "
                End If
                _Cmd &= vbCrLf & ", U.FTDrugUnitCode AS FNHSysDrugUnitIdTo , P.FNHSysDrugUnitId as FNHSysDrugUnitIdTo_Hide"
                _Cmd &= vbCrLf & "FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTPurchase_Detail AS P WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrug AS D WITH (NOLOCK) ON P.FNHSysDrugId = D.FNHSysDrugId LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnit AS U ON P.FNHSysDrugUnitId = U.FNHSysDrugUnitId"
                _Cmd &= vbCrLf & "WHERE P.FNHSysDrugId in ("
                _Cmd &= vbCrLf & "Select FNHSysDrugId"
                _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTPurchase_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "Where FTMECPurchaseNo = '" & HI.UL.ULF.rpQuoted(Me.FTMECPurchaseNo.Text) & "'"
                _Cmd &= vbCrLf & "and FNHSysDrugId not in ( "
                _Cmd &= vbCrLf & "Select FNHSysDrugId"
                _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail WITH (NOLOCK) ) )"
                _oDt2 = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                If _oDt2.Rows.Count > 0 Then
                    HI.MG.ShowMsg.mInfo("พบว่ามีการรับยาครั้งแรก กรุณาระบุหน่วยการรับ !!!", 1505120012, Me.Text, , MessageBoxIcon.Warning)
                    HI.TL.HandlerControl.ClearControl(_ReceiveUnitDrug)

                    With _ReceiveUnitDrug
                        Call HI.ST.Lang.SP_SETxLanguage(_ReceiveUnitDrug)

                        .ocmreceive.Enabled = True
                        .ocmcancel.Enabled = True

                        .ogcrcv.DataSource = _oDt2
                        .ProcessProc = False
                        .ShowDialog()

                        If .ProcessProc = False Then
                            Exit Sub
                        End If

                    End With
                End If



                _Cmd = "SELECT  '1' AS FTSelect ,    D.FTMECPurchaseNo, D.FNHSysDrugId,E.FNHSysDrugUnitId_Rcv as FNHSysDrugUnitId ,  E.FTDrugCode,   U.FTDrugUnitCode  "

                _Cmd &= vbCrLf & ",CASE WHEN D.FNHSysDrugUnitId <> E.FNHSysDrugUnitId_Rcv Then "
                _Cmd &= vbCrLf & "D.FNQuantity * ISNULL((Select Top 1 FNRateTo From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnitConvert WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE FNHSysDrugUnitId = D.FNHSysDrugUnitId and FNHSysDrugUnitIdTo = E.FNHSysDrugUnitId_Rcv  ),0)"
                _Cmd &= vbCrLf & "  Else"
                _Cmd &= vbCrLf & "  D.FNQuantity "
                _Cmd &= vbCrLf & "   End"
                _Cmd &= vbCrLf & " as FNQuantity"

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", E.FTDrugNameTH AS FTDrugName , U.FTDrugUnitNameTH AS FTDrugUnitName "
                Else
                    _Cmd &= vbCrLf & ", E.FTDrugNameEN AS FTDrugName , U.FTDrugUnitNameEN AS FTDrugUnitName   "
                End If

                _Cmd &= vbCrLf & "        , Isnull((SELECT   SUM(FNQuantity) AS FNQuantity"
                _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "Where   FTMEDRcvNo='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
                _Cmd &= vbCrLf & " and FNHSysDrugId=D.FNHSysDrugId),0) AS FNRcvQty"


                _Cmd &= vbCrLf & ",ISNULL((SELECT   sum(DC.FNQuantity) "
                _Cmd &= vbCrLf & "FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve AS HC WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail AS DC WITH (NOLOCK) ON HC.FTMEDRcvNo = DC.FTMEDRcvNo"
                _Cmd &= vbCrLf & "            Where HC.FTMECPurchaseNo = D.FTMECPurchaseNo"
                _Cmd &= vbCrLf & "and DC.FNHSysDrugId = D.FNHSysDrugId"
                _Cmd &= vbCrLf & "and DC.FNHSysDrugUnitId = ISNULL(E.FNHSysDrugUnitId_Rcv, D.FNHSysDrugUnitId)),0 ) AS FNRcvHistory"

                _Cmd &= vbCrLf & ",(( CASE WHEN D.FNHSysDrugUnitId <> E.FNHSysDrugUnitId_Rcv Then "
                _Cmd &= vbCrLf & "D.FNQuantity * ISNULL((Select Top 1 FNRateTo From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnitConvert WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE FNHSysDrugUnitId = D.FNHSysDrugUnitId and FNHSysDrugUnitIdTo = E.FNHSysDrugUnitId_Rcv  ),0)"
                _Cmd &= vbCrLf & "  Else"
                _Cmd &= vbCrLf & "  D.FNQuantity "
                _Cmd &= vbCrLf & "   End"
                _Cmd &= vbCrLf & ") - ISNULL((SELECT   sum(DC.FNQuantity) "
                _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve AS HC WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail AS DC WITH (NOLOCK) ON HC.FTMEDRcvNo = DC.FTMEDRcvNo"
                _Cmd &= vbCrLf & "           Where HC.FTMECPurchaseNo = D.FTMECPurchaseNo"
                _Cmd &= vbCrLf & "and DC.FNHSysDrugId = D.FNHSysDrugId"
                _Cmd &= vbCrLf & "and DC.FNHSysDrugUnitId = ISNULL(E.FNHSysDrugUnitId_Rcv, D.FNHSysDrugUnitId)),0 )) AS  FNRcvBal"
                _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTPurchase_Detail AS D WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrug AS E WITH (NOLOCK) ON D.FNHSysDrugId = E.FNHSysDrugId LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnit AS U WITH (NOLOCK) ON E.FNHSysDrugUnitId_Rcv = U.FNHSysDrugUnitId"

                _Cmd &= vbCrLf & "WHERE D.FTMECPurchaseNo ='" & HI.UL.ULF.rpQuoted(Me.FTMECPurchaseNo.Text) & "'"

                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

                HI.TL.HandlerControl.ClearControl(_AddItemPopup)
                With _AddItemPopup
                    .ogcDetail.DataSource = _oDt
                    .ShowDialog()

                    With .ogcDetail
                        If Not (.DataSource Is Nothing) And ogvDetail.RowCount > 0 Then
                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                        _oDt = CType(.DataSource, DataTable)
                    End With

                    If (._Proc) Then
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        For Each R As DataRow In _oDt.Select("FTSelect='1'")
                            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail"
                            _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Cmd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",FNHSysDrugUnitId=" & Integer.Parse(R!FNHSysDrugUnitId.ToString)
                            _Cmd &= vbCrLf & ",FNQuantity=" & CDbl("0" & R!FNRcvQty.ToString)
                            _Cmd &= vbCrLf & "WHERE FTMEDRcvNo='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
                            _Cmd &= vbCrLf & "AND FNHSysDrugId=" & Integer.Parse(R!FNHSysDrugId.ToString)
                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                _Cmd &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail"
                                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTMEDRcvNo, FNHSysDrugId, FNHSysDrugUnitId, FNQuantity)"
                                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
                                _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysDrugId.ToString)
                                _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysDrugUnitId.ToString)
                                _Cmd &= vbCrLf & "," & CDbl("0" & R!FNRcvQty.ToString)

                                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Exit Sub
                                End If
                            End If
                        Next
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End If


                End With
            Else
                'Case Is Not PO
                _Cmd = "SELECT     '0' AS FTSelect, '' AS FTMECPurchaseNo, E.FNHSysDrugId, U.FNHSysDrugUnitId ,0 as FNQuantity, E.FTDrugCode, U.FTDrugUnitCode "
                _Cmd &= vbCrLf & "    , 0 AS FNRcvQty,  0 AS FNRcvHistory, 0 AS FNRcvBal"

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", E.FTDrugNameTH AS FTDrugName, U.FTDrugUnitNameTH AS FTDrugUnitName "
                Else
                    _Cmd &= vbCrLf & ", E.FTDrugNameEN AS FTDrugName, U.FTDrugUnitNameEN AS FTDrugUnitName "
                End If
                _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrug AS E WITH (NOLOCK)   LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnit AS U WITH (NOLOCK) ON E.FNHSysDrugUnitId_Rcv = U.FNHSysDrugUnitId"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                HI.TL.HandlerControl.ClearControl(_AddItemPopup)
                With _AddItemPopup
                    .ogcDetail.DataSource = _oDt
                    .ShowDialog()

                    With .ogcDetail
                        If Not (.DataSource Is Nothing) And ogvDetail.RowCount > 0 Then
                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                        _oDt = CType(.DataSource, DataTable)
                    End With

                    If (._Proc) Then

                        'GetId
                        Dim tText As String = ""
                        Dim _Where As String = ""
                        For Each oRow As DataRow In _oDt.Select("FTSelect='1'")
                            tText &= oRow("FNHSysDrugId") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                            _Where &= "  '" & tText.Replace("|", "','") & "'  "
                        End If
                        'GetIdDrug

                        _Cmd = "  SELECT     D.FNHSysDrugId, U.FNHSysDrugUnitId, D.FTDrugCode,   U.FTDrugUnitCode"
                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            _Cmd &= vbCrLf & ", D.FTDrugNameTH AS FTDrugName "
                        Else
                            _Cmd &= vbCrLf & ", D.FTDrugNameEN AS FTDrugName "
                        End If
                        _Cmd &= vbCrLf & ", U.FTDrugUnitCode AS FNHSysDrugUnitIdTo , U.FNHSysDrugUnitId as FNHSysDrugUnitIdTo_Hide"
                        _Cmd &= vbCrLf & "FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrug AS D WITH (NOLOCK)   LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnit AS U ON D.FNHSysDrugUnitId = U.FNHSysDrugUnitId"
                        _Cmd &= vbCrLf & "WHERE   D.FNHSysDrugId not in  ("
                        _Cmd &= vbCrLf & "Select FNHSysDrugId"
                        _Cmd &= vbCrLf & "FROM   [HITECH_MEDICAL]..TMECTRecieve_Detail WITH (NOLOCK)  "
                        _Cmd &= vbCrLf & "Where   FNHSysDrugId   in (" & _Where & "))"
                        _Cmd &= vbCrLf & "And  D.FNHSysDrugId in(" & _Where & ") "

                        _oDt2 = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                        If _oDt2.Rows.Count > 0 Then
                            HI.MG.ShowMsg.mInfo("พบว่ามีการรับยาครั้งแรก กรุณาระบุหน่วยการรับ !!!", 1505120012, Me.Text, , MessageBoxIcon.Warning)
                            HI.TL.HandlerControl.ClearControl(_ReceiveUnitDrug)

                            With _ReceiveUnitDrug
                                Call HI.ST.Lang.SP_SETxLanguage(_ReceiveUnitDrug)

                                .ocmreceive.Enabled = True
                                .ocmcancel.Enabled = True

                                .ogcrcv.DataSource = _oDt2
                                .ProcessProc = False
                                .ShowDialog()

                                If .ProcessProc = False Then
                                    Exit Sub
                                End If

                            End With
                        End If

                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        Dim _DrugUnitIdRcv As String = ""
                        For Each R As DataRow In _oDt.Select("FTSelect='1'")
                            _DrugUnitIdRcv = "Select Top 1 FNHSysDrugUnitId_Rcv From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrug WHERE FNHSysDrugId=" & Integer.Parse(R!FNHSysDrugId.ToString)
                            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail"
                            _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Cmd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",FNHSysDrugUnitId=" & Integer.Parse(HI.Conn.SQLConn.GetFieldOnBeginTrans(_DrugUnitIdRcv, Conn.DB.DataBaseName.DB_SYSTEM, "0"))
                            _Cmd &= vbCrLf & ",FNQuantity=" & CDbl("0" & R!FNRcvQty.ToString)
                            _Cmd &= vbCrLf & "WHERE FTMEDRcvNo='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
                            _Cmd &= vbCrLf & "AND FNHSysDrugId=" & Integer.Parse(R!FNHSysDrugId.ToString)
                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                _Cmd &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail"
                                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTMEDRcvNo, FNHSysDrugId, FNHSysDrugUnitId, FNQuantity)"
                                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
                                _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysDrugId.ToString)
                                _Cmd &= vbCrLf & "," & Integer.Parse(HI.Conn.SQLConn.GetFieldOnBeginTrans(_DrugUnitIdRcv, Conn.DB.DataBaseName.DB_SYSTEM, "0"))
                                _Cmd &= vbCrLf & "," & CDbl("0" & R!FNRcvQty.ToString)

                                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Exit Sub
                                End If
                            End If
                        Next
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End If
                End With

            End If
            Me.LoadDataDetail()
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataDetail()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "Select       D.FTMEDRcvNo, D.FNHSysDrugId, D.FNHSysDrugUnitId,  D.FNQuantity, U.FTDrugUnitCode , G.FTDrugCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", U.FTDrugUnitNameTH AS FTDrugUnitName , G.FTDrugNameTH AS FTDrugName"
            Else
                _Cmd &= vbCrLf & ", U.FTDrugUnitNameEN AS FTDrugUnitName , G.FTDrugNameEN AS FTDrugName"
            End If
            _Cmd &= vbCrLf & ",Isnull(P.FNPrice,0) AS FNPrice , Isnull(P.FNDisPer,0) AS FNDisPer , Isnull(P.FNDisAmt,0) AS FNDisAmt  "   ', Isnull(P.FNNetAmt,0) AS FNNetAmt
            _Cmd &= vbCrLf & ",(D.FNQuantity /Isnull(DC.FNRateTo,1)) * (Isnull(P.FNPrice,0) -  Isnull(P.FNDisAmt,0)) as FNNetAmt "
            _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail AS D WITH (NOLOCK)  LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnit AS U WITH (NOLOCK) ON D.FNHSysDrugUnitId = U.FNHSysDrugUnitId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrug AS G WITH(NOLOCK) ON D.FNHSysDrugId = G.FNHSysDrugId  "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve AS H WITH (NOLOCK) ON D.FTMEDRcvNo = H.FTMEDRcvNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTPurchase_Detail AS P WITH (NOLOCK) ON H.FTMECPurchaseNo = P.FTMECPurchaseNo"
            _Cmd &= vbCrLf & "And D.FNHSysDrugId = P.FNHSysDrugId "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMDrugUnitConvert AS DC ON d.FNHSysDrugUnitId = Dc.FNHSysDrugUnitIdTo  and p.FNHSysDrugUnitId = Dc.FNHSysDrugUnitId"
            _Cmd &= vbCrLf & " WHERE    D.FTMEDRcvNo='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            Me.ogcDetail.DataSource = _oDt

            Me.FTMECPurchaseNo.Properties.ReadOnly = _oDt.Rows.Count > 0
            Me.FTMECPurchaseNo.Properties.Buttons.Item(0).Enabled = IIf(_oDt.Rows.Count > 0, False, True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvDetail.KeyDown
        Try
            If ChkIss() Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถลบข้อมูลได้เนื่องจากมีการจ่ายแล้ว", 1505110001, Me.Text, "", MessageBoxIcon.Stop)
                Exit Sub
            End If
            Dim _Cmd As String = ""
            If e.KeyCode = Keys.Delete Then
                With Me.ogvDetail
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                    Dim _DrugId As Integer = 0

                    For Each i As Integer In .GetSelectedRows()
                        _DrugId = Integer.Parse("0" & .GetRowCellValue(i, "FNHSysDrugId").ToString)

                        _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTRecieve_Detail  "
                        _Cmd &= vbCrLf & "WHERE FTMEDRcvNo='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
                        _Cmd &= vbCrLf & "And FNHSysDrugId=" & Integer.Parse(_DrugId)
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                    Next

                End With
            End If

            Me.LoadDataDetail()
        Catch ex As Exception

        End Try
    End Sub

    Private Function ChkIss() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _RcvQty As Double = 0.0

            _Cmd = "Select Top 1 Isnull(FNQuantity ,0) AS FNQuantity"
            _Cmd &= vbCrLf & "From TMECTDrugPay WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTDocumentRefNo = '" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
            _RcvQty = CDbl(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MEDC, "0"))
            Return _RcvQty > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.FTMEDRcvNo.Text <> "" Then
                If ChkIss() Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถลบข้อมูลได้เนื่องจากมีการจ่ายแล้ว", 1505110001, Me.Text, "", MessageBoxIcon.Stop)
                    Exit Sub
                End If
                If Me.DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
                    Me.LoadDataDetail()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Try
            Call FormRefresh()
        Catch ex As Exception
        End Try
    End Sub

    

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            If Me.FTMEDRcvNo.Text <> "" And Me.FTMEDRcvNo.Properties.Tag.ToString <> "" Then
                With New HI.RP.Report

                    Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language


                    .FormTitle = Me.Text
                    .ReportFolderName = "MEDICAL\"
                    .ReportName = "RecieveDrug.rpt"

                    .Formular = "{TMECTRecieve.FTMEDRcvNo}='" & HI.UL.ULF.rpQuoted(Me.FTMEDRcvNo.Text) & "'"
                    .Preview()

                    HI.ST.Lang.Language = _tmplang
                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTMEDRcvNo_lbl.Text)
                FTMECPurchaseNo.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
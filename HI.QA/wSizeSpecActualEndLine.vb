Imports DevExpress.Spreadsheet
'Imports Microsoft.Office.Interop.Excel

Public Class wSizeSpecActualEndLine
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As System.Data.DataTable
    Private _ProcLoad As Boolean = False

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call InitFormControl()
    End Sub

    '  Private FNQCType As String = ""
    'Public Property FNQCType As String
    '    Get
    '        Return _TypeDoc
    '    End Get
    '    Set(value As String)
    '        _TypeDoc = value
    '    End Set
    'End Property

    Private Sub wSizeSpecAdj_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ' FNQCType = "1"
            _SysDocType = Me.FNQCType.Text
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

    Public Function GetDocType(type As String) As String
        Try
            Return type
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Sub InitFormControl()

        Dim _Str As String = ""
        Dim _objId As Integer
        Dim _dt As System.Data.DataTable
        Dim _StrQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _StartX As Double = 0
        Dim _StartY As Double = 0
        Dim _CtrLv As Double = -1
        Dim _CtrHeight As Double = 0
        Dim _dtgrpobj As New System.Data.DataTable


        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='wSizeSpecActual' "
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


        Dim _Dt As System.Data.DataTable
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

        Call LoadData()
        _ProcLoad = False
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
                                Dim _CmpH
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                If .Text = "" Then
                                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                                Else
                                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                                End If
                                            End With
                                            Exit For
                                    End Select

                                Next

                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "1", True, _CmpH).ToString() Then
                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                    Dim _dt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

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
                                                If .Text = "" Then
                                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                                Else
                                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                                End If
                                            End With
                                            Exit For
                                    End Select
                                Next
                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "1", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else
                                    _Key = .Text
                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

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
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "1", False, _CmpH).ToString
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
                                            If .Name.ToString = "FTColorway" Then
                                                _Val = "" & .Text
                                            End If
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
                                            If .Name.ToString = "FTColorway" Then
                                                _Val = "" & .Text
                                            End If
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
            If SaveData_Detail(_Key) Then
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

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas WHERE FTQCMeasNo='" & HI.UL.ULF.rpQuoted(Me.FTQCMeasNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas_Detail WHERE FTQCMeasNo='" & HI.UL.ULF.rpQuoted(Me.FTQCMeasNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas WHERE FTQCMeasNo='" & HI.UL.ULF.rpQuoted(Me.FTQCMeasNo.Text) & "'")
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
        Dim _dt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
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
    End Sub
#End Region


    Private Sub LoadData()
        '  Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            Dim _colcount As Integer = 0

            _Cmd = "   SELECT     LEFT(E.FTStyleCode,6) AS FTStyleCode , N.FTSeasonCode, Isnull(M.FTMeasCode,'') AS FTMeasCode , S.FTTolerant as FTTOLPlus ,   S.FTSizeSpecExtension  , T.FTMatSizeCode as FTSizeCode, T.FNMatSizeSeq"
            _Cmd &= vbCrLf & " , 0 AS FNQCSeq,'' AS FTNoteDetail  , S.FNSeq AS FNRowSeq "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", isnull(M.FTMeasDescTH ,S.FTSizeSpecDesc) as FTDesc  "
            Else
                _Cmd &= vbCrLf & ", isnull( M.FTMeasDescEN ,S.FTSizeSpecDesc) as FTDesc "
            End If
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS T WITH (NOLOCK) ON S.FNHSysMatSizeId = T.FNHSysMatSizeId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMeasurements AS M WITH (NOLOCK) ON S.FNHSysMeasId = M.FNHSysMeasId INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON S.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS E WITH (NOLOCK) ON O.FNHSysStyleId = E.FNHSysStyleId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS N WITH (NOLOCK) ON O.FNHSysSeasonId = N.FNHSysSeasonId  "

            _Cmd &= vbCrLf & " WHERE E.FNHSysStyleId =" & Integer.Parse("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & " and N.FNHSysSeasonId=" & Integer.Parse("0" & Me.FNHSysSeasonId.Properties.Tag)
            _Cmd &= vbCrLf & " and O.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"

            _Cmd &= vbCrLf & "UNION ALL "
            _Cmd &= vbCrLf & " Select distinct LEFT(ST.FTStyleCode,6) AS  FTStyleCode ,S.FTSeasonCode   ,Co.FTMeasCode , X.FTTolerant  "
            _Cmd &= vbCrLf & "  , Co.FTActualQtySize , Co.FTSizeBreakDown , Z.FNMatSizeSeq "
            _Cmd &= vbCrLf & " ,Co.FNQCSeq ,Co.FTNoteDetail , co.FNRowSeq "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", Isnull(M.FTMeasDescTH , X.FTSizeSpecDesc) as FTDesc  "
            Else
                _Cmd &= vbCrLf & ", Isnull(M.FTMeasDescEN , X.FTSizeSpecDesc) as FTDesc "
            End If
            _Cmd &= vbCrLf & " From ( Select H.FTQCMeasNo , H.FNHSysStyleId , H.FTOrderNo , H.FTColorway , H.FNHSysUnitSectId , D.FTMeasCode , D.FTSizeBreakDown , D.FNQCSeq , D.FTActualQtySize  "
            _Cmd &= vbCrLf & " , H.FTNote  , D.FTNote AS FTNoteDetail , D.FNRowSeq  "
            _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas AS H WITH(NOLOCK)  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas_Detail AS D WITH(NOLOCK) ON  H.FTQCMeasNo = D.FTQCMeasNo   ) AS Co "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON Co.FNHSysStyleId = ST.FNHSysStyleId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON Co.FTOrderNo = O.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK) ON O.FNHSysSeasonId = S.FNHSysSeasonId"
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec AS H WITH(NOLOCK) ON LEFT(ST.FTStyleCode,6) = H.FTStyleCode"
            '_Cmd &= vbCrLf & " and S.FTSeasonCode = H.FTSeasonCode "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMeasurements AS M WITH(NOLOCK) ON Co.FTMeasCode = M.FTMeasCode "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS Z WITH(NOLOCK) ON Co.FTSizeBreakDown = Z.FTMatSizeCode"
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec AS X WITH(NOLOCK) ON Co.FTOrderNo = X.FTOrderNo and Z.FNHSysMatSizeId = X.FNHSysMatSizeId"
            _Cmd &= vbCrLf & " and co.FTOrderNo = x.FTOrderNo and co.FNRowSeq = X.FNSeq"
            _Cmd &= vbCrLf & "Where Co.FNHSysStyleId=" & Integer.Parse("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & "and Co.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "and Co.FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & "and Co.FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "'"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

            If _oDt.Rows.Count <= 0 Then
                '    _Spls.Close()
                Exit Sub
            End If

            Dim _dt As New System.Data.DataTable
            Call CreateDatatable(_oDt, ogvdetail, _dt)
            Call GenerateGridBand(_oDt, ogvdetail, ogcdetail, _dt)
            '    _Spls.Close()
        Catch ex As Exception
            '  _Spls.Close()
        End Try
    End Sub

    Private Sub CreateDatatable(dt As System.Data.DataTable, ByVal ogv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByRef oDtRef As System.Data.DataTable)
        Dim _dt As New System.Data.DataTable

        With _dt

            _dt.Columns.Add("FTMeasCode", GetType(String))
            _dt.Columns.Add("FTDesc", GetType(String))
            _dt.Columns.Add("FTTOLPlus", GetType(String))
            _dt.Columns.Add("FTNoteDetail", GetType(String))

        End With

        Dim _StrFilter As String = ""

        If Not (dt Is Nothing) Then
            Dim _StateActual As Boolean = dt.Select("FNQCSeq <> 0").Count > 0
            Dim _QCSeqMax As Integer = 0
            For Each z As DataRow In dt.Select("FNQCSeq <> 0", "FNQCSeq Desc ")
                _QCSeqMax = z!FNQCSeq.ToString
                Exit For
            Next
            For Each R As DataRow In dt.Select("FTDesc<>''", "FTMeasCode ASC ,FNRowSeq ASC ,  FTNoteDetail desc ")
                _StrFilter = "FTMeasCode='" & R!FTMeasCode.ToString & "' "
                _StrFilter &= "  AND FTDesc='" & R!FTDesc.ToString & "' "
                Try
                    If _dt.Select(_StrFilter).Length <= 0 Then
                        _dt.Rows.Add(R!FTMeasCode.ToString, R!FTDesc.ToString, R!FTTOLPlus.ToString, R!FTNoteDetail.ToString)
                    End If
                Catch ex As Exception
                End Try

                If Not (_StateActual) Then
                    If _dt.Columns.IndexOf("C" & R!FTSizeCode.ToString) < 0 Then
                        _dt.Columns.Add("C" & R!FTSizeCode.ToString, GetType(String))
                        _dt.Columns.Add("C" & R!FTSizeCode.ToString & "1", GetType(String))
                    End If
                Else
                    If R!FNQCSeq.ToString = "0" Then
                        If _dt.Columns.IndexOf("C" & R!FTSizeCode.ToString) < 0 Then
                            _dt.Columns.Add("C" & R!FTSizeCode.ToString, GetType(String))
                        End If
                    Else
                        If _dt.Columns.IndexOf("C" & R!FTSizeCode.ToString & R!FNQCSeq.ToString) < 0 Then
                            _dt.Columns.Add("C" & R!FTSizeCode.ToString & R!FNQCSeq.ToString, GetType(String))
                        End If
                    End If
                    For i As Integer = 1 To _QCSeqMax
                        If _dt.Columns.IndexOf("C" & R!FTSizeCode.ToString & i) < 0 Then
                            _dt.Columns.Add("C" & R!FTSizeCode.ToString & i, GetType(String))
                        End If
                    Next
                End If


                For Each Rx As DataRow In _dt.Select(_StrFilter)
                    If Not (_StateActual) Then
                        Rx.Item("C" & R!FTSizeCode.ToString) = R!FTSizeSpecExtension.ToString
                    Else
                        If R!FNQCSeq.ToString = "0" Then
                            Rx.Item("C" & R!FTSizeCode.ToString) = R!FTSizeSpecExtension.ToString
                        Else
                            Rx.Item("C" & R!FTSizeCode.ToString & R!FNQCSeq.ToString) = R!FTSizeSpecExtension.ToString
                        End If
                    End If
                Next

            Next
        End If

        With ogv
            .BeginInit()
            .Columns.Clear()
            .Bands.Clear()

            For Each Col As DataColumn In _dt.Columns

                Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                With _BanCol
                    .Caption = Col.ColumnName.ToString
                    .FieldName = Col.ColumnName.ToString
                    .Name = Col.ColumnName.ToString
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                    .Visible = True

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTDate".ToUpper, "FTEXP".ToUpper, "FTMeasCode".ToUpper,
                              "FTGarmentSpec".ToUpper, "FTPomDesc".ToUpper, "FTMedPattern".ToUpper, "FTTOLPlus".ToUpper, "FTGrandRule1".ToUpper, "FTGrandRule2".ToUpper
                            .Width = 50
                            .OptionsColumn.AllowEdit = False
                            .OptionsColumn.ReadOnly = True
                        Case "FTDesc".ToUpper
                            .Width = 250
                            .OptionsColumn.AllowEdit = False
                            .OptionsColumn.ReadOnly = True
                        Case "FTNoteDetail".ToUpper
                            .Width = 200
                            .OptionsColumn.AllowEdit = True
                            .OptionsColumn.ReadOnly = False
                        Case Else
                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "C" Then
                                .Width = 50
                                If IsNumeric(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 1)) Then
                                    .OptionsColumn.AllowEdit = True
                                    .OptionsColumn.ReadOnly = False
                                    'Dim _Rep As New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
                                    '_Rep.Precision = 2
                                    '_Rep.Buttons.Item(0).Visible = False
                                    '.ColumnEdit = _Rep
                                Else
                                    .OptionsColumn.AllowEdit = False
                                    .OptionsColumn.ReadOnly = True

                                End If
                            Else
                                If Col.ColumnName.ToString = "FTSizeCode" Then
                                    .Width = 50
                                Else
                                    .Width = 50
                                End If
                            End If

                    End Select
                End With
                .Columns.Add(_BanCol)
            Next
            .EndInit()
        End With

        oDtRef = _dt
    End Sub



    Private Sub GenerateGridBand(ByVal EmpData As System.Data.DataTable, ByVal ogv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByVal ogc As DevExpress.XtraGrid.GridControl, ByVal oDt As System.Data.DataTable)
        Try
            Try
                With ogvdetail
                    .Bands.Clear()
                End With
            Catch ex As Exception
            End Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0
            Dim _Str As String = ""
            '_Str = "FTStyleCode|FTSeasonCode|FTDate|FTEXP|FTMeasCode|FTGarmentSpec|FTPomDesc|FTMedPattern|FTTOLPlus|FTGrandRule1|FTGrandRule2"
            _Str = "FTMeasCode|FTDesc|FTTOLPlus"
            Dim sFieldSum As String = ""
            With ogv
                .BeginInit()
                For Each Str As String In _Str.Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        '.Caption = Str
                        .Caption = ogvv.Columns.ColumnByFieldName(Str).Caption.ToString
                        .Columns.Add(ogv.Columns.ColumnByFieldName(Str))
                        .Name = ogv.Name.ToString & "gb" & Str
                        .RowCount = 2
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                    End With
                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next
                If Not (EmpData Is Nothing) Then
                    Dim _StateActual As Boolean = EmpData.Select("FNQCSeq <> 0").Count > 0
                    Dim _MainMaxSeq As Integer = 0 : Dim _ChildMaxSeq As Integer = 0
                    For Each z As DataRow In EmpData.Select("FNQCSeq <> 0", "FNQCSeq Desc")
                        _MainMaxSeq = z!FNQCSeq.ToString
                        Exit For
                    Next
                    Dim grp As List(Of String) = (EmpData.Select("FTSizeCode <> ''", "FNMatSizeSeq , FTSizeCode").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTSizeCode")) _
                                                      .Distinct() _
                                                      .ToList()
                    Dim _StateCreateBand As Boolean = False

                    Dim _Code As String = ""
                    For Each Ind As String In grp
                        _StateCreateBand = False
                        Dim _GrbandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                        For Each R As DataRow In EmpData.Select("FTSizeCode='" & Ind & "'", "FNMatSizeSeq , FTSizeCode")
                            If _Code <> R!FTSizeCode.ToString Then
                                If _StateCreateBand = False Then
                                    With _GrbandType
                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = R!FTSizeCode.ToString
                                        .Name = ogv.Name.ToString & "gbt" & R!FTSizeCode.ToString
                                        .RowCount = 1
                                    End With
                                    .Bands.Add(_GrbandType)
                                    _StateCreateBand = True
                                End If
                                Dim _GrbandCol1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                Dim _GrbandCol2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                                If Not (_StateActual) Then
                                    For i As Integer = 1 To 2
                                        _GrbandCol1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                        With _GrbandCol1
                                            .AppearanceHeader.Options.UseTextOptions = True
                                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                            If i = 1 Then
                                                ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString).AppearanceCell.BackColor = System.Drawing.Color.LightSalmon
                                                .Columns.Add(ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString))
                                                Appearance.BackColor = System.Drawing.Color.LightSalmon
                                            Else
                                                ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString & "1").AppearanceCell.BackColor = System.Drawing.Color.LightCyan()
                                                .Columns.Add(ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString & "1"))
                                            End If
                                            .Caption = IIf(i = 1, "O", (i - 1))
                                            .Name = ogv.Name.ToString & "gbcol1" & R!FTSizeCode.ToString & "_" & (i - 1).ToString
                                            .RowCount = 1
                                            .Width = 50
                                        End With
                                        _GrbandType.Children.Add(_GrbandCol1)
                                    Next
                                Else
                                    For i As Integer = 0 To _MainMaxSeq
                                        Try
                                            Dim _nCol As String = ogv.Name.ToString & "gbcol1" & R!FTSizeCode.ToString & "_" & (i).ToString
                                            _GrbandCol1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                            With _GrbandCol1
                                                .AppearanceHeader.Options.UseTextOptions = True
                                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                                If i = 0 Then
                                                    ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString).AppearanceCell.BackColor = System.Drawing.Color.LightSalmon
                                                    .Columns.Add(ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString))
                                                Else
                                                    ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString & i.ToString).AppearanceCell.BackColor = System.Drawing.Color.LightCyan()
                                                    .Columns.Add(ogv.Columns.ColumnByFieldName("C" & R!FTSizeCode.ToString & i.ToString))
                                                End If
                                                .Caption = IIf(i = "0", "O", i.ToString)
                                                .Name = _nCol
                                                .RowCount = 1
                                                .Width = 50
                                            End With
                                            _GrbandType.Children.Add(_GrbandCol1)
                                        Catch ex As Exception
                                        End Try
                                    Next

                                End If
                                _GrbandType.Width = _GrbandType.Children.Count * 50
                            End If

                            _Code = R!FTSizeCode.ToString
                        Next
                    Next
                End If

                _Str = "FTNoteDetail"
                For Each Str As String In _Str.Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        '.Caption = Str
                        .Caption = ogvdetail.Columns.ColumnByFieldName(Str).Caption.ToString
                        .Columns.Add(ogv.Columns.ColumnByFieldName(Str))
                        .Name = ogv.Name.ToString & "gb" & Str
                        .RowCount = 2
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
                    End With
                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next

                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
                .EndInit()
            End With
            ogc.DataSource = oDt
            Me.ogcv.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            Call LoadData()
        Catch ex As Exception
        End Try
    End Sub

    Private Function CheckApproveSizeSpec() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 Isnull(FTStateApprovedSizeSpec,'0') AS FTStateApprovedSizeSpec From TMERTOrderSub_ApprovedInfo WITH(NOLOCK) Where FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' and isnull(FTStateApprovedSizeSpec,'0') ='1'"
            Return IIf(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "0") = "1", True, False)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Not VerrifyData() Then Exit Sub
            If Not (CheckApproveSizeSpec()) Then
                HI.MG.ShowMsg.mInfo("Size Spec By Order No yet Approved...", 1512171343, Me.Text)
                Exit Sub
            End If
            If SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData_Detail(_Key As String) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _SizeCode As String = "" : Dim _QCSeq As Integer = 0 : Dim _QCSeqMax As Integer = 0
            Dim _oDt As System.Data.DataTable : Dim _Row As Integer = 0
            With CType(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            For Each R As DataRow In _oDt.Rows
                _Row += +1
                For Each Col As DataColumn In _oDt.Columns
                    If IsNumeric(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 1)) Then
                        _SizeCode = Replace(Col.ColumnName.ToString, "C", "")
                        If IsNumeric(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 2)) Then
                            _SizeCode = Replace(_SizeCode, Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 2), "")
                            _QCSeq = CInt(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 2))
                        ElseIf IsNumeric(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 1)) Then
                            _SizeCode = Replace(_SizeCode, Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 1), "")
                            _QCSeq = CInt(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 1))
                        End If
                        If _QCSeq > _QCSeqMax Then
                            _QCSeqMax = _QCSeq
                        End If

                        If R.Item(Col.ColumnName.ToString).ToString <> "" Then


                            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas_Detail"
                            _Cmd &= vbCrLf & " Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",FTActualQtySize='" & HI.UL.ULF.rpQuoted(R.Item(Col.ColumnName.ToString)) & "'"
                            _Cmd &= vbCrLf & ",FTNote='" & HI.UL.ULF.rpQuoted(R!FTNoteDetail.ToString) & "'"
                            _Cmd &= vbCrLf & ", FTMeasCode='" & HI.UL.ULF.rpQuoted(R!FTMeasCode.ToString) & "'"
                            _Cmd &= vbCrLf & "Where FTQCMeasNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Cmd &= vbCrLf & "And FTSizeBreakDown='" & _SizeCode & "'"
                            _Cmd &= vbCrLf & "And FNQCSeq=" & Integer.Parse(_QCSeq)
                            _Cmd &= vbCrLf & "And FNRowSeq=" & Integer.Parse(_Row)
                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                _Cmd = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas_Detail"
                                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTQCMeasNo, FTMeasCode, FTSizeBreakDown, FTActualQtySize, FNQCSeq,FTNote,FNRowSeq)"
                                _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMeasCode.ToString) & "'"
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SizeCode) & "'"
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R.Item(Col.ColumnName.ToString)) & "'"
                                _Cmd &= vbCrLf & "," & Integer.Parse(_QCSeq)
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNoteDetail.ToString) & "'"
                                _Cmd &= vbCrLf & "," & Integer.Parse(_Row)
                                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                            End If
                        Else

                            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas_Detail"
                            _Cmd &= vbCrLf & "Where FTQCMeasNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Cmd &= vbCrLf & "And FTSizeBreakDown='" & _SizeCode & "'"
                            _Cmd &= vbCrLf & "And FNQCSeq=" & Integer.Parse(_QCSeq)
                            _Cmd &= vbCrLf & "And FNRowSeq=" & Integer.Parse(_Row)
                            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        End If
                    End If
                Next
            Next

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas_Detail"
            _Cmd &= vbCrLf & "Where FTQCMeasNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            _Cmd &= vbCrLf & "And FNQCSeq > " & Integer.Parse(_QCSeqMax)
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function


    Private Sub LoadDtInfo(_Dt As System.Data.DataTable)
        Try
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
            Call VisibleObject()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            Dim _oDt As System.Data.DataTable
            Dim _Code As String = "" : Dim _FiledName As String
            With CType(ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            For Each oGridCol As DevExpress.XtraGrid.Views.BandedGrid.GridBand In ogvdetail.Bands
                Select Case Microsoft.VisualBasic.Left(oGridCol.Name, ogvdetail.Name.Length + 3)
                    Case ogvdetail.Name & "gbt"
                        _FiledName = "C" & Replace(oGridCol.ToString, "gbt", "").ToString & oGridCol.Children.Count.ToString
                        With _oDt
                            .BeginInit()
                            .Columns.Add(_FiledName, GetType(String))
                            .EndInit()
                        End With

                        With ogvdetail
                            .BeginInit()
                            Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                            With _BanCol
                                .Caption = _FiledName.ToString
                                .FieldName = _FiledName.ToString
                                .Name = _FiledName.ToString
                                .OptionsColumn.AllowEdit = True
                                .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                                .OptionsColumn.ReadOnly = False
                                .Visible = True
                                .Width = 50
                                'Dim _Rep As New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
                                '_Rep.Precision = 2
                                '_Rep.Buttons.Item(0).Visible = False
                                ''AddHandler _Rep.EditValueChanging, AddressOf ReposPrice_EditValueChanging
                                ''AddHandler _Rep.KeyDown, AddressOf Rep_KeyDown
                                '.ColumnEdit = _Rep

                            End With
                            .Columns.Add(_BanCol)
                            .EndInit()
                        End With

                        Dim _GrbandCol1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        Dim _GrbandCol2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        _GrbandCol1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        With _GrbandCol1
                            .AppearanceHeader.Options.UseTextOptions = True
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .Columns.Add(ogvdetail.Columns.ColumnByFieldName(_FiledName.ToString))
                            .Caption = oGridCol.Children.Count.ToString
                            .Name = _FiledName.ToString
                            .RowCount = 1
                            .Width = 50
                        End With
                        oGridCol.Children.Add(_GrbandCol1)
                        oGridCol.Width = oGridCol.Children.Count * 50
                End Select
            Next
            ogcdetail.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged, FTQCMeasNo.EditValueChanged
        Try
            If Me.FTOrderNo.Text <> "" Then
                Dim _Cmd As String = ""
                _Cmd = "SELECT  Top 1  O.FTPORef, T.FTStyleCode, S.FTSeasonCode"
                _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH (NOLOCK) ON O.FNHSysSeasonId = S.FNHSysSeasonId"
                _Cmd &= vbCrLf & "WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN).Rows
                    Me.FTPORef.Text = R!FTPORef.ToString
                    Me.FNHSysSeasonId.Text = R!FTSeasonCode.ToString
                    Me.FNHSysStyleId.Text = R!FTStyleCode.ToString
                Next

                Me.FNCntApprovedSizeSpec.Text = ""
                Me.FDDateApprovedSizeSpec.Text = ""
                Me.FTTimeApprovedSizeSpec.Text = ""
                Me.FTStateApprovedSizeSpec.Checked = False
                Me.FTUserApprovedSizeSpec.Text = ""

                _Cmd = "Select max(FNCntApprovedSizeSpec) AS FNCntApprovedSizeSpec , Case When isdate(FDDateApprovedSizeSpec) = 1 Then convert(varchar(10),convert(datetime,FDDateApprovedSizeSpec),103) Else '' End AS FDDateApprovedSizeSpec"
                _Cmd &= vbCrLf & ",FTTimeApprovedSizeSpec , Isnull(FTStateApprovedSizeSpec,'0') AS FTStateApprovedSizeSpec ,FTUserApprovedSizeSpec"
                _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_ApprovedInfo WITH(NOLOCK) "
                _Cmd &= vbCrLf & " Where FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & " group by   FTStateApprovedSizeSpec,   FDDateApprovedSizeSpec, FTTimeApprovedSizeSpec, FTUserApprovedSizeSpec "
                _Cmd &= vbCrLf & " Order by FDDateApprovedSizeSpec DESC , FTTimeApprovedSizeSpec DESC"
                For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN).Rows
                    Me.FNCntApprovedSizeSpec.Text = "R : " & IIf(CInt("0" & R!FNCntApprovedSizeSpec.ToString) <= 1, 0, CInt("0" & R!FNCntApprovedSizeSpec.ToString) - 1)
                    Me.FDDateApprovedSizeSpec.Text = R!FDDateApprovedSizeSpec.ToString
                    Me.FTTimeApprovedSizeSpec.Text = R!FTTimeApprovedSizeSpec.ToString
                    Me.FTStateApprovedSizeSpec.Checked = IIf(R!FTStateApprovedSizeSpec.ToString = "1", True, False)
                    Me.FTUserApprovedSizeSpec.Text = R!FTUserApprovedSizeSpec.ToString
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectId.EditValueChanged
        Try
            Call Call_LoadData()
            Call VisibleObject()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTColorway_EditValueChanged(sender As Object, e As EventArgs) Handles FTColorway.EditValueChanged
        Try
            Call Call_LoadData()
            Call VisibleObject()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub VisibleObject()
        Try

            Me.FTOrderNo.Properties.Buttons.Item(0).Visible = Not (Me.FNHSysUnitSectId.Text <> "" And Me.FTColorway.Text <> "")
            Me.FTOrderNo.Properties.ReadOnly = (Me.FNHSysUnitSectId.Text <> "" And Me.FTColorway.Text <> "")
            Me.FNHSysStyleId.Properties.Buttons.Item(0).Visible = Not (Me.FNHSysUnitSectId.Text <> "" And Me.FTColorway.Text <> "")
            Me.FNHSysStyleId.Properties.ReadOnly = (Me.FNHSysUnitSectId.Text <> "" And Me.FTColorway.Text <> "")
            Me.FNHSysSeasonId.Properties.ReadOnly = (Me.FNHSysUnitSectId.Text <> "" And Me.FTColorway.Text <> "")
            Me.FNHSysSeasonId.Properties.Buttons.Item(0).Visible = Not (Me.FNHSysUnitSectId.Text <> "" And Me.FTColorway.Text <> "")

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Call_LoadData()
        Try
            If Me.FTOrderNo.Text = "" Then
                Exit Sub
            End If
            If Me.FTPORef.Text = "" Then
                Exit Sub
            End If
            If Me.FNHSysStyleId.Text = "" Then
                Exit Sub
            End If
            If Me.FNHSysSeasonId.Text = "" Then
                Exit Sub
            End If
            If Me.FNHSysUnitSectId.Text = "" Then
                Exit Sub
            End If
            If Me.FTColorway.Text = "" Then
                Exit Sub
            End If
            Call LoadData()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Me.FTQCMeasNo.Text <> "" Then
            If Me.DeleteData Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try
            Dim _oDt As System.Data.DataTable : Dim _FiledName As String = ""
            With CType(ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            For Each oGridCol As DevExpress.XtraGrid.Views.BandedGrid.GridBand In ogvdetail.Bands
                Select Case Microsoft.VisualBasic.Left(oGridCol.Name, ogvdetail.Name.Length + 3)
                    Case ogvdetail.Name & "gbt"
                        _FiledName = "C" & Replace(oGridCol.ToString, "gbt", "").ToString & (oGridCol.Children.Count - 1).ToString
                        With _oDt
                            .BeginInit()
                            .Columns.Remove(_FiledName)
                            .EndInit()
                        End With

                        oGridCol.Children.RemoveAt(oGridCol.Children.Count - 1)
                        oGridCol.Width = oGridCol.Children.Count * 50
                End Select
            Next
            ogcdetail.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            If Me.FTQCMeasNo.Text <> "" Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Production\"
                    .ReportName = "Report_QCMeas.rpt"
                    .Formular = "{TPRODTQC_Meas.FTQCMeasNo} ='" & HI.UL.ULF.rpQuoted(FTQCMeasNo.Text) & "' "
                    .Preview()
                End With
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class
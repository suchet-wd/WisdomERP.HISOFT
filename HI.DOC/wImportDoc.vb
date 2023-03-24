Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Xpf.Printing
Imports DevExpress.Pdf
Imports System.Drawing
Imports System.Text

Public Class wImportDoc

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As System.Data.DataTable
    Private _ProcLoad As Boolean = False
    Private _AddItemPopup As wMailPopup
    Private _FilePath As String

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        _AddItemPopup = New wMailPopup
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try


        Call InitFormControl()
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
        Call GetStateApprove(Key)
        Call LoadDetail()
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

        Me.oGrpdetail.Controls.Clear()
        Call GetStateApprove("")
        Call DefaultUnitsectId()
    End Sub

    Private Sub DefaultUnitsectId()
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT Top 1   isnull(S.FTUnitSectCode,'') as FTUnitSectCode "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS L WITH (NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON L.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S WITH (NOLOCK) ON E.FNHSysUnitSectId = S.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " where  L.FTUserName ='" & HI.ST.UserInfo.UserName & "'"
            Try
                Me.FNHSysUnitSectId.Text = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "")
            Catch ex As Exception
                Me.FNHSysUnitSectId.Text = ""
            End Try

        Catch ex As Exception

        End Try
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

                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
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
        If Me.FNOperActivity.SelectedIndex = 3 Or Me.FNOperActivity.SelectedIndex = 2 Then
            If Me.FTOperActivityName.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                FTOperActivityName.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function _SaveData(FTDocumentTitle As String, data() As Byte, FTFileType As String) As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String : Dim _Cmd As String = ""
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
                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
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
        Dim _Date As String = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB & " AS FTDate ", Conn.DB.DataBaseName.DB_DOC, "")
        Dim _Time As String = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatTimeDB & " AS FTTime  ", Conn.DB.DataBaseName.DB_DOC, "")
        If (_StateNew) Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.FNDocType.SelectedIndex, False, _CmpH).ToString
            Try
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                If data Is Nothing Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                    _Cmd &= " (FTInsUser, FDInsDate, FTInsTime, FTDocumentNo, FDDocumentDate, FTDocumentBy, FNHSysCmpId, FNDocType, FTNote ,FTBenefit)"
                    _Cmd &= " VALUES(@FTInsUser, @FDInsDate, @FTInsTime, @FTDocumentNo, @FDDocumentDate, @FTDocumentBy, @FNHSysCmpId, @FNDocType,  @FTNote , @FTBenefit)"
                    Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
                    cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    Dim p As New SqlParameter("@FDInsDate", SqlDbType.NVarChar)
                    p.Value = _Date
                    Dim p2 As New SqlParameter("@FTInsTime", SqlDbType.NVarChar)
                    p2.Value = _Time
                    Dim p3 As New SqlParameter("@FTDocumentNo", SqlDbType.NVarChar)
                    p3.Value = _Key
                    Dim p4 As New SqlParameter("@FDDocumentDate", SqlDbType.NVarChar)
                    p4.Value = HI.UL.ULDate.ConvertEnDB(Me.FDDocumentDate.Text)
                    Dim p11 As New SqlParameter("@FTDocumentBy", SqlDbType.NVarChar)
                    p11.Value = HI.UL.ULF.rpQuoted(Me.FTDocumentBy.Text)
                    Dim p5 As New SqlParameter("@FNHSysCmpId", SqlDbType.Int)
                    p5.Value = Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
                    Dim p6 As New SqlParameter("@FNDocType", SqlDbType.Int)
                    p6.Value = Me.FNDocType.SelectedIndex
                    Dim p10 As New SqlParameter("@FTNote", SqlDbType.NVarChar)
                    p10.Value = HI.UL.ULF.rpQuoted(Me.FTNote.Text)
                    Dim p12 As New SqlParameter("@FTBenefit", SqlDbType.NVarChar)
                    p12.Value = HI.UL.ULF.rpQuoted(Me.FTBenefit.Text)

                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(p2)
                    cmd.Parameters.Add(p3)
                    cmd.Parameters.Add(p4)
                    cmd.Parameters.Add(p11)
                    cmd.Parameters.Add(p5)
                    cmd.Parameters.Add(p6)
                    cmd.Parameters.Add(p10)
                    cmd.Parameters.Add(p12)
                    cmd.ExecuteNonQuery()
                Else
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                    _Cmd &= " (FTInsUser, FDInsDate, FTInsTime, FTDocumentNo, FDDocumentDate, FTDocumentBy, FNHSysCmpId, FNDocType, FTDocumentTitle, FBDocument, FNFileType, FTNote,FTBenefit)"
                    _Cmd &= " VALUES(@FTInsUser, @FDInsDate, @FTInsTime, @FTDocumentNo, @FDDocumentDate, @FTDocumentBy, @FNHSysCmpId, @FNDocType, @FTDocumentTitle,@FBDocument, @FNFileType, @FTNote,@FTBenefit)"
                    Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
                    cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))

                    Dim p As New SqlParameter("@FDInsDate", SqlDbType.NVarChar)
                    p.Value = _Date

                    Dim p2 As New SqlParameter("@FTInsTime", SqlDbType.NVarChar)
                    p2.Value = _Time

                    Dim p3 As New SqlParameter("@FTDocumentNo", SqlDbType.NVarChar)
                    p3.Value = _Key

                    Dim p4 As New SqlParameter("@FDDocumentDate", SqlDbType.NVarChar)
                    p4.Value = HI.UL.ULDate.ConvertEnDB(Me.FDDocumentDate.Text)

                    Dim p11 As New SqlParameter("@FTDocumentBy", SqlDbType.NVarChar)
                    p11.Value = HI.UL.ULF.rpQuoted(Me.FTDocumentBy.Text)

                    Dim p5 As New SqlParameter("@FNHSysCmpId", SqlDbType.Int)
                    p5.Value = Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)

                    Dim p6 As New SqlParameter("@FNDocType", SqlDbType.Int)
                    p6.Value = Me.FNDocType.SelectedIndex

                    Dim p7 As New SqlParameter("@FTDocumentTitle", SqlDbType.NVarChar)
                    p7.Value = FTDocumentTitle

                    Dim p8 As New SqlParameter("@FBDocument", SqlDbType.VarBinary)
                    p8.Value = data

                    Dim p9 As New SqlParameter("@FNFileType", SqlDbType.Int)
                    p9.Value = Me.FNFileType.SelectedIndex

                    Dim p10 As New SqlParameter("@FTNote", SqlDbType.NVarChar)
                    p10.Value = HI.UL.ULF.rpQuoted(Me.FTNote.Text)

                    Dim p12 As New SqlParameter("@FTBenefit", SqlDbType.NVarChar)
                    p12.Value = HI.UL.ULF.rpQuoted(Me.FTBenefit.Text)

                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(p2)
                    cmd.Parameters.Add(p3)
                    cmd.Parameters.Add(p4)
                    cmd.Parameters.Add(p11)
                    cmd.Parameters.Add(p5)
                    cmd.Parameters.Add(p6)
                    cmd.Parameters.Add(p7)
                    cmd.Parameters.Add(p8)
                    cmd.Parameters.Add(p9)
                    cmd.Parameters.Add(p10)
                    cmd.Parameters.Add(p12)
                    cmd.ExecuteNonQuery()
                End If
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
            Catch ex As Exception
            End Try

        Else
            Try
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_DOC)
                HI.Conn.SQLConn.SqlConnectionOpen()
                If data Is Nothing Then
                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                    _Cmd &= " Set FTUpdUser=@FTUpdUser"
                    _Cmd &= " ,FDUpdDate=@FDUpdDate"
                    _Cmd &= " ,FTUpdTime=@FTUpdTime"
                    _Cmd &= " ,FNDocType=@FNDocType"
                    _Cmd &= " ,FTNote=@FTNote"
                    _Cmd &= " ,FTBenefit=@FTBenefit"
                    _Cmd &= "  Where FTDocumentNo=@FTDocumentNo"
                    Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
                    cmd.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    Dim p As New SqlParameter("@FDUpdDate", SqlDbType.NVarChar)
                    p.Value = _Date
                    Dim p2 As New SqlParameter("@FTUpdTime", SqlDbType.NVarChar)
                    p2.Value = _Time
                    Dim p3 As New SqlParameter("@FNDocType", SqlDbType.Int)
                    p3.Value = Me.FNDocType.SelectedIndex
                    Dim p4 As New SqlParameter("@FTNote", SqlDbType.NVarChar)
                    p4.Value = HI.UL.ULF.rpQuoted(Me.FTNote.Text)
                    Dim p8 As New SqlParameter("@FTDocumentNo", SqlDbType.NVarChar)
                    p8.Value = _Key
                    Dim p12 As New SqlParameter("@FTBenefit", SqlDbType.NVarChar)
                    p12.Value = HI.UL.ULF.rpQuoted(Me.FTBenefit.Text)

                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(p2)
                    cmd.Parameters.Add(p3)
                    cmd.Parameters.Add(p4)
                    cmd.Parameters.Add(p8)
                    cmd.Parameters.Add(p12)
                    cmd.ExecuteNonQuery()
                Else
                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                    _Cmd &= " Set FTUpdUser=@FTUpdUser"
                    _Cmd &= " ,FDUpdDate=@FDUpdDate"
                    _Cmd &= " ,FTUpdTime=@FTUpdTime"
                    _Cmd &= " ,FNDocType=@FNDocType"
                    _Cmd &= " ,FTNote=@FTNote"
                    _Cmd &= " ,FTDocumentTitle=@FTDocumentTitle"
                    _Cmd &= " ,FBDocument=@FBDocument"
                    _Cmd &= "  ,FNFileType=@FNFileType"
                    _Cmd &= " ,FTBenefit=@FTBenefit"
                    _Cmd &= "  Where FTDocumentNo=@FTDocumentNo"
                    Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
                    cmd.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    'cmd.Parameters.Add("@FTUpdUser", SqlDbType.NVarChar).Value = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
                    Dim p As New SqlParameter("@FDUpdDate", SqlDbType.NVarChar)
                    p.Value = _Date
                    Dim p2 As New SqlParameter("@FTUpdTime", SqlDbType.NVarChar)
                    p2.Value = _Time
                    Dim p3 As New SqlParameter("@FNDocType", SqlDbType.Int)
                    p3.Value = Me.FNDocType.SelectedIndex
                    Dim p4 As New SqlParameter("@FTNote", SqlDbType.NVarChar)
                    p4.Value = HI.UL.ULF.rpQuoted(Me.FTNote.Text)
                    Dim p5 As New SqlParameter("@FTDocumentTitle", SqlDbType.NVarChar)
                    p5.Value = FTDocumentTitle
                    Dim p6 As New SqlParameter("@FBDocument", SqlDbType.VarBinary)
                    p6.Value = data
                    Dim p7 As New SqlParameter("@FNFileType", SqlDbType.Int)
                    p7.Value = Me.FNFileType.SelectedIndex
                    Dim p8 As New SqlParameter("@FTDocumentNo", SqlDbType.NVarChar)
                    p8.Value = _Key
                    Dim p12 As New SqlParameter("@FTBenefit", SqlDbType.NVarChar)
                    p12.Value = HI.UL.ULF.rpQuoted(Me.FTBenefit.Text)
                    cmd.Parameters.Add(p)
                    cmd.Parameters.Add(p2)
                    cmd.Parameters.Add(p3)
                    cmd.Parameters.Add(p4)
                    cmd.Parameters.Add(p5)
                    cmd.Parameters.Add(p6)
                    cmd.Parameters.Add(p7)
                    cmd.Parameters.Add(p8)
                    cmd.Parameters.Add(p12)
                    cmd.ExecuteNonQuery()
                End If
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
            Catch ex As Exception
            End Try
        End If
        Try


            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function SaveData(FTDocumentTitle As String, data() As Byte, FTFileType As String) As Boolean

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
                    If _FieldName = "FTStateMNGDepApp" Then Continue For
                    If _FieldName = "FTStateManagerApp" Then Continue For
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
            Dim _Cmd As String = ""
            Dim _RvNo As Integer = 0
            If Not (data Is Nothing) Then
                _Cmd = "Select Top 1  Isnull(FNReviseNo,0) AS FNReviseNo  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument  "
                _Cmd &= "  Where FTDocumentNo='" & _Key & "'"
                _RvNo = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_DOC, "0")
                If Me.FNOperActivity.SelectedIndex = 0 Then
                Else
                    _RvNo += +1
                End If


                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                _Cmd &= " Set  FTDocumentTitle=@FTDocumentTitle"
                _Cmd &= " ,FBDocument=@FBDocument"
                _Cmd &= " ,FNFileType=@FNFileType"
                _Cmd &= " ,FNReviseNo=@FNReviseNo"
                _Cmd &= "  Where FTDocumentNo=@FTDocumentNo"
                Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                cmd.Parameters.AddWithValue("@FTDocumentTitle", FTDocumentTitle)
                Dim p6 As New SqlParameter("@FBDocument", SqlDbType.VarBinary)
                p6.Value = data
                Dim p7 As New SqlParameter("@FNFileType", SqlDbType.Int)
                p7.Value = Me.FNFileType.SelectedIndex
                Dim p8 As New SqlParameter("@FTDocumentNo", SqlDbType.NVarChar)
                p8.Value = _Key
                Dim p9 As New SqlParameter("@FNReviseNo", SqlDbType.Int)
                p9.Value = _RvNo

                cmd.Parameters.Add(p6)
                cmd.Parameters.Add(p7)
                cmd.Parameters.Add(p8)
                cmd.Parameters.Add(p9)
                cmd.ExecuteNonQuery()



                _Cmd = "Delete   From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument_File"
                _Cmd &= vbCrLf & " where  FTDocumentNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Cmd &= vbCrLf & " and FNSeq =" & _RvNo
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument_File "
                _Cmd &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTDocumentNo, FNSeq, FTDocumentTitle)"
                _Cmd &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Cmd &= vbCrLf & "," & _RvNo
                _Cmd &= vbCrLf & ",'" & FTDocumentTitle & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If


                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument_File"
                _Cmd &= " Set  FBDocument=@FBDocument"
                _Cmd &= "  Where FTDocumentNo=@FTDocumentNo"
                _Cmd &= " and FNSeq=@FNSeq"

                cmd = New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim p10 As New SqlParameter("@FBDocument", SqlDbType.VarBinary)
                p10.Value = data
                cmd.Parameters.AddWithValue("@FTDocumentNo", _Key)
                cmd.Parameters.AddWithValue("@FNSeq", _RvNo)
                cmd.Parameters.Add(p10)
                cmd.ExecuteNonQuery()

            End If

            _Cmd = "Delete   From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCPermisstionRead"
            _Cmd &= vbCrLf & " where  FTDocumentNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            With DirectCast(Me.ogcapprove.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTState = '1'")
                    _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCPermisstionRead "
                    _Cmd &= vbCrLf & "   (FTInsUser, FDInsDate,FTInsTime ,FTDocumentNo , FNHSysUnitSectId ,FTStateActive )"
                    _Cmd &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNHSysUnitSectId.ToString)
                    _Cmd &= vbCrLf & ",'1'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                Next
            End With


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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'")

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
    Private Sub wImportDoc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.FNHSysCmpId.Properties.Tag = HI.ST.SysInfo.CmpID
        Catch ex As Exception
        End Try
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
                    Case ".TXT".ToUpper, ".DOC".ToUpper, ".DOCX".ToUpper
                        Call _TextViewer(_FilePath)
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

    Private Sub _TextViewer(_FileName As String)
        Try
            Me.oGrpdetail.Controls.Clear()
            Dim _Txt As New DevExpress.XtraRichEdit.RichEditControl
            _Txt.ReadOnly = True
            _Txt.Dock = DockStyle.Fill

            ' _Txt.Options.Import.PlainText.Encoding = Encoding.UTF8
            _Txt.LoadDocument(_FileName)
            Me.oGrpdetail.Controls.Add(_Txt)
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

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTDocumentBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT Top 1   isnull(S.FTUnitSectCode,'') as FTUnitSectCode "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS L WITH (NOLOCK) LEFT OUTER JOIN "
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON L.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S WITH (NOLOCK) ON E.FNHSysUnitSectId = S.FNHSysUnitSectId"
            _Qry &= vbCrLf & " where  L.FTUserName ='" & HI.ST.UserInfo.UserName & "'"

            If Me.FNHSysUnitSectId.Text.ToUpper = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") Then
                Return True
            Else
                HI.MG.ShowMsg.mProcessError(1809291544, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            End If

            '_Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            '_Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTDocumentBy.Text) & "' "
            '_FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            '_Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            '_Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            '_Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            '_FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            'If _FNHSysTeamGrpId > 0 Then

            '    If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
            '        Return True
            '    Else
            '        HI.MG.ShowMsg.mProcessError(1809291544, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            '        Return False
            '    End If

            'Else
            '    HI.MG.ShowMsg.mProcessError(1809291544, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            '    Return False

            'End If


        End If

    End Function

    Private Sub ocmimportexcel_Click(sender As Object, e As EventArgs) Handles ocmsavedocument.Click
        Try
            If Me.FTDocumentNo.Text <> "" Then
                If CheckOwner() = False Then Exit Sub

                If Me.FTStateManagerApp.Checked And Me.FNOperActivity.SelectedIndex = 0 Then
                    HI.MG.ShowMsg.mInfo("Can't Update Data.. ", 1511101450, Me.Text)
                    Exit Sub
                End If
                If Me.FNOperActivity.SelectedIndex = 1 Then
                    If FTOperActivityName.Text = "" Then
                        HI.MG.ShowMsg.mInfo("กรุณาใส่เหตุผลในการแก้ไขเอกสาร !!!!!!", 1809291516, Me.FTOperActivityName_lbl.Text)
                        Me.FTOperActivityName.Focus()
                        Exit Sub
                    End If
                End If

                If Me.FNOperActivity.SelectedIndex <> 0 And Me.FTSandApprove.Checked And Me.FTStateMNGDepApp.Checked And Me.FTStateManagerApp.Checked Then

                    Dim _Cmd As String = ""
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                    _Cmd &= vbCrLf & " Set FTStateMNGDepApp='0'"
                    _Cmd &= vbCrLf & ", FTMNGDepAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDMNGDepAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTMNGDepAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " , FTStateManagerApp='0'"
                    _Cmd &= vbCrLf & ", FTManagerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " , FTSandApprove='0'"
                    _Cmd &= vbCrLf & ", FTSandApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDSandApproveDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_DOC)

                    Me.FTSandApprove.Checked = False
                    Me.FTStateMNGDepApp.Checked = False
                    Me.FTStateManagerApp.Checked = False

                End If

                If Me.oGrpdetail.Controls.Count <= 0 Then

                    HI.MG.ShowMsg.mInfo("กรุณาเพิ่มเอกสารไฟล์แนบ !!! ", 1806120920, Me.Text)
                    Exit Sub

                End If

                If _FilePath <> "" Then

                    Dim _FileType As String = "" : Dim _FileName As String : Dim data As Byte()
                    _FileType = System.IO.Path.GetExtension(_FilePath)
                    _FileName = System.IO.Path.GetFileName(_FilePath)

                    Select Case _FileType.ToUpper
                        Case ".XLSX".ToUpper, ".XLS".ToUpper

                            Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                            data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))

                            If SaveData(_FileName, data, _FileType) Then
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            Else
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            End If

                        Case ".TXT".ToUpper, ".DOC".ToUpper, ".DOCX".ToUpper

                            data = System.IO.File.ReadAllBytes(_FilePath)

                            If SaveData(_FileName, data, _FileType) Then
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            Else
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            End If

                        Case ".PDF".ToUpper

                            Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))

                            'Dim newFile As String = "C:\Users\WSMDEV\Downloads\dt.rpt"

                            'Dim fs As New FileStream(newFile, FileMode.Create, FileAccess.Write)
                            'Dim writer As PdfWriter = PdfWriter.GetInstance(, fs)
                            ' Dim cb As PdfContentByte = writer.DirectContent

                            data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))

                            If SaveData(_FileName, data, _FileType) Then
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            Else
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            End If

                    End Select

                Else

                    If (SaveData("", Nothing, "")) Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If

                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTDocumentNo_lbl.Text)
            End If
        Catch ex As Exception
            HI.MG.ShowMsg.mInfo(ex.ToString, 1808171709, Me.Text)
        End Try
    End Sub

    Private _Pdfdata As Byte()
    Private Sub LoadDetail()
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT Top 1   isnull(S.FTUnitSectCode,'') as FTUnitSectCode "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS L WITH (NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON L.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S WITH (NOLOCK) ON E.FNHSysUnitSectId = S.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " where  L.FTUserName ='" & HI.ST.UserInfo.UserName & "'"
            Try
                Me.FNHSysUnitSectId.Text = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "")
            Catch ex As Exception
                Me.FNHSysUnitSectId.Text = ""
            End Try



            _Cmd = "Select Top 1  FTDocumentTitle, FBDocument, FNFileType "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument WITH(NOLOCK)"
            _Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)
            For Each R As DataRow In _oDt.Rows
                Select Case CInt("0" & R!FNFileType.ToString)
                    Case 2
                        Me.oGrpdetail.Controls.Clear()
                        Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                        With _XlsV
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .Unit = DevExpress.Office.DocumentUnit.Inch
                            .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)

                        End With
                        Me.oGrpdetail.Controls.Add(_XlsV)
                    Case 3
                        Me.oGrpdetail.Controls.Clear()
                        Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                        With _RTX
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DocumentFormat.Doc)
                        End With
                        Me.oGrpdetail.Controls.Add(_RTX)
                    Case 1
                        Me.oGrpdetail.Controls.Clear()
                        Dim _RTX As New DevExpress.XtraRichEdit.RichEditControl
                        With _RTX
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            '.Options.Import.PlainText.AutoDetectEncoding = False
                            '.Options.Import.PlainText.Encoding = Encoding.UTF8
                            .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DocumentFormat.PlainText)

                        End With
                        Me.oGrpdetail.Controls.Add(_RTX)
                    Case 0
                        Try
                            Me.oGrpdetail.Controls.Clear()
                            Dim _Pdfv As New PdfViewer
                            With _Pdfv
                                .Dock = DockStyle.Fill
                                _Pdfdata = CType(R!FBDocument, Byte())
                                .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())))
                            End With
                            Me.oGrpdetail.Controls.Add(_Pdfv)
                        Catch ex As Exception
                        End Try
                End Select
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdownloadfile_Click(sender As Object, e As EventArgs) Handles ocmdownloadfile.Click
        Try
            For Each obj As Object In Me.oGrpdetail.Controls
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

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.FTDocumentNo.Text <> "" Then
                If Me.FTStateManagerApp.Checked Then
                    HI.MG.ShowMsg.mInfo("Can't Update Data.. ", 1511101450, Me.Text)
                    Exit Sub
                End If
                If DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
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
            Me.oGrpdetail.Controls.Clear()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpostdocument_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
        Try
            If Me.FTDocumentNo.Text <> "" Then
                If Me.FTStateManagerApp.Checked Then
                    HI.MG.ShowMsg.mInfo("Can't Update Data.. ", 1511101450, Me.Text)
                    Exit Sub
                End If
                If SendApprove() Then
                    HI.MG.ShowMsg.mInfo("Send Approve  Successful...", 1511061347, Me.Text, "", MessageBoxIcon.Information)
                    Call LoadDataInfo(Me.FTDocumentNo.Text)
                Else
                    HI.MG.ShowMsg.mInfo("Send Approve  Unsuccessful!!!", 1511061348, Me.Text, "", MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SendApprove() As Boolean
        Try

            Dim _Cmd As String

            'If Me.FNDocType.SelectedIndex = 1 Then
            '    If Not (FncsendMail()) Then
            '        Return False
            '    End If
            'End If

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "UPDATE   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument "
            _Cmd &= vbCrLf & "Set FTSandApprove='1'"
            _Cmd &= vbCrLf & ",FTSandApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDSandApproveDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTOwnerStateApprove ='1'"
            _Cmd &= vbCrLf & ",FTOwnerApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDOwnerApproveDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTOwnerApproveTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & " " & GetStrownerCmp.ToString
            'If Me.FNDocType.SelectedIndex = 5 Then
            '    _Cmd &= vbCrLf & ",FTStateMNGDepApp ='1'"
            '    _Cmd &= vbCrLf & ",FTMNGDepAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Cmd &= vbCrLf & ",FDMNGDepAppDate=" & HI.UL.ULDate.FormatDateDB
            '    _Cmd &= vbCrLf & ",FTMNGDepAppTime=" & HI.UL.ULDate.FormatTimeDB
            'End If

            _Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            If Me.FNOperActivity.SelectedIndex = 2 Then
                _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle"
                _Cmd &= vbCrLf & "Set FTNote='" & HI.UL.ULF.rpQuoted(Me.FTOperActivityName.Text) & "'"
                _Cmd &= vbCrLf & "Where FNHSysDocNameId=" & Integer.Parse(Me.FNHSysDocNameId.Properties.Tag)
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
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

    Private Function FncsendMail()
        Try
            Dim _State As Boolean = False
            Dim tmpsubject As String = ""
            Dim tmpmessage As String = ""
            Dim _Qry As String = ""
            Dim _oDt As DataTable


            _Qry = "Select FTDCUserName"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) "
            _Qry &= vbCrLf & "WHERE FTCmpCode in ('HT91' , 'HT70','HTSP','HTC1','HTC2','HTC3','HTSR','HTVN','HTCD','HTFG') and Isnull(FTStateActive,'0') = '1'"
            _Qry &= vbCrLf & "and FNHSysCmpId <> " & Integer.Parse(HI.ST.SysInfo.CmpID.ToString)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)
            If _oDt.Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลกลุ่ม User Accont กรุณาทำการติดต่อผู้ดูแลระบบ !!!", 1504290017, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            End If
            HI.TL.HandlerControl.ClearControl(_AddItemPopup)
            With _AddItemPopup
                .ShowDialog()
                If (.State) Then
                    tmpsubject = HI.UL.ULF.rpQuoted(.FTSubJect.Text)
                    tmpmessage = HI.UL.ULF.rpQuoted(.FTMessange.Text)
                Else
                    Return False
                End If
            End With
            For Each R As DataRow In _oDt.Rows
                If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, HI.UL.ULF.rpQuoted(R!FTDCUserName.ToString), tmpsubject, tmpmessage, 8, Me.FTDocumentNo.Text) Then
                    _State = True
                End If
            Next
            Return _State
        Catch ex As Exception
        End Try
    End Function

    Private Function GetStrownerCmp() As String
        Try
            Dim _Str As String = ""
            Select Case Me.FNHSysCmpId.Text.ToUpper
                Case "HT91".ToUpper
                    _Str = ", FT91StateApprove='1'  , FT91ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FD91ApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FT91ApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HT70".ToUpper
                    _Str = ", FT70StateApprove='1'  , FT70ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FD70ApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FT70ApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTSP".ToUpper
                    _Str = ", FTSPStateApprove='1'  , FTSPApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDSPApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTSPApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTC1".ToUpper
                    _Str = ", FTC1StateApprove='1'  , FTC1ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDC1ApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTC1ApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTC2".ToUpper
                    _Str = ", FTC2StateApprove='1'  , FTC2ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDC2ApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTC2ApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTC3".ToUpper
                    _Str = ", FTC3StateApprove='1'  , FTC3ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDC3ApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTC3ApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTSR".ToUpper
                    _Str = ", FTSRStateApprove='1'  , FTSRApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDSRApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTSRApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTVN".ToUpper
                    _Str = ", FTVNStateApprove='1'  , FTVNApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDVNApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTVNApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTCD".ToUpper
                    _Str = ", FTCDStateApprove='1'  , FTCDApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDCDApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTCDApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                Case "HTFG".ToUpper
                    _Str = ", FTFGStateApprove='1'  , FTFGApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FDFGApproveDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= " , FTFGApproveTime=" & HI.UL.ULDate.FormatTimeDB & "  "
            End Select
            Return _Str
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg

                Select Case Me.FNFileType.SelectedIndex
                    Case 0
                        .Filter = "PDF files |*.pdf"
                    Case 1
                        .Filter = "Text files |*.txt"
                    Case 2
                        .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                    Case 3
                        .Filter = "Word Documents(97-2003)|*.doc|Word Documents(2010-2013)|*.docx"
                End Select

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

    Private Sub FNDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNFileType.SelectedIndexChanged
        Try
            Me.oGrpdetail.Controls.Clear()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetStateApprove(Key As String)
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT     FT91StateApprove AS FTState, 'HT91' AS FTCmpName"
            _Cmd &= vbCrLf & " FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FT91StateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FT70StateApprove , 'HT70'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FT70StateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTC1StateApprove , 'HTC1'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTC1StateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTC2StateApprove, 'HTC2'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTC2StateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTC3StateApprove , 'HTC3'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTC3StateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTSRStateApprove , 'HTSR'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTSRStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTSPStateApprove, 'HTSP'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTSPStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTCDStateApprove, 'HTCD'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTCDStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTVNStateApprove, 'HTVN'"
            _Cmd &= vbCrLf & "FROM TDCDocument  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTVNStateApprove,'0') = '1'"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "Select FTFGStateApprove , 'HTFG'"
            _Cmd &= vbCrLf & "FROM TDCDocument WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Cmd &= vbCrLf & "and isnull(FTFGStateApprove,'0') = '1'"



            _Cmd = "SELECT case when  S.FNHSysUnitSectId = P.FNHSysUnitSectId then '1' else '0' end  AS  FTState ,  S.FNHSysUnitSectId   "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & "  ,FTUnitSectNameTH as  FTUnitSectName "
            Else
                _Cmd &= vbCrLf & "  ,FTUnitSectNameEN as  FTUnitSectName "
            End If


            _Cmd &= vbCrLf & "FRom [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S WITH (NOLOCK) "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  (Select *  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCPermisstionRead WITH(NOLOCK)  where FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Key) & "' ) AS P ON S.FNHSysUnitSectId = P.FNHSysUnitSectId "
            _Cmd &= vbCrLf & "where  S.FTStateActive = '1' "
            Me.ogcapprove.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNOperActivity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOperActivity.SelectedIndexChanged
        Try
            If Me.FNOperActivity.SelectedIndex <> 0 Then
                '  Me.FTOperActivityName.Enabled = True
                ' Else
                '  Me.FTOperActivityName.Enabled = True
                If Me.FTSandApprove.Checked And Me.FTStateMNGDepApp.Checked And Me.FTStateManagerApp.Checked Then
                    Me.oGrpdetail.Controls.Clear()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadEmpInfomation()
        Try
            Dim _dt As DataTable

            Dim _Qry As String = ""
            _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
            _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
            _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode,M.FNSalary,ET.FNCalType,ET.FNSalaryDivide "
            _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysUnitSectId.Properties.Tag) & " "
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _dt.Rows
                'FNHSysDeptId.Text = R!FTDeptCode.ToString
                'FNHSysPositId.Text = R!FTPositCode.ToString
            Next
            If _dt.Rows.Count <= 0 Then
                'FNHSysDeptId.Text = ""
                'FNHSysPositId.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysEmpID_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpID_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysUnitSectId.Text <> "" Then

                    Dim _Qry As String = "SELECT TOP 1 FNHSysUnitSectId  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect  WITH(NOLOCK) WHERE FTUnitSectCode ='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "' "
                    FNHSysUnitSectId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                    ' Call LoadEmpInfomation()
                Else

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class
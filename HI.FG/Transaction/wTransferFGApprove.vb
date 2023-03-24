Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Drawing
Imports System.Windows.Forms

Public Class wTransferFGApprove
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _ProcLoad As Boolean = False
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitFormControl()
        'HI.TL.HandlerControl.AddHandlerObj(Me)
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
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted("wTransferFG") & "' "
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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
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

        Call LoadWH(Key.ToString)
        Call LoadGridDetail()



        _ProcLoad = False
    End Sub

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

    Private Sub LoadGridDetail()
        Dim _Qry As String = ""
        Dim _dt As DataTable


        '_Qry = "select SFG.FTPackNo,SFG.FTOrderNo,S.FTStyleCode,M.FTPORef,f.FTBarCodeCarton,SFG.FTColorway,SFG.FTSizeBreakDown,SFG.FNQuantity,SD.FTNikePOLineItem,SFG.FNCartonNo  ,  case when isnull(T.FTStateApprove,'0')='1' then  F.FNQuantityRcv  else    F.FNQuantity end   as FNQuantityRcv "
        '_Qry &= vbCrLf & " FROM "
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG AS SFG WITH(NOLOCK) "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail as F WITH(NOLOCK) ON SFG.FTBarCodeCarton=F.FTBarCodeCarton"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG as T WITH(NOLOCK) ON F.FTTransferFGNo = T.FTTransferFGNo"

        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS M WITH(NOLOCK) ON SFG.FTOrderNo=M.FTOrderNo"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON M.FNHSysStyleId=S.FNHSysStyleId"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN ("
        '_Qry &= vbCrLf & "  SELECT  isnull(SBD.FTNikePOLineItem,'') AS FTNikePOLineItem,SBD.FTOrderNo,SBD.FTColorway,A.FNCartonNo,SFG.FTBarCodeCarton,SFG.FTPackNo"
        '_Qry &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK) LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG ON SBD.FTOrderNo=SFG.FTOrderNo and SBD.FTSizeBreakDown=SFG.FTSizeBreakDown and SBD.FTColorway=SFG.FTColorway LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON SBD.FTOrderNo=A.FTOrderNo and SFG.FTColorway=A.FTColorway and SFG.FNCartonNo=A.FNCartonNo and SBD.FTSubOrderNo=A.FTSubOrderNo and SFG.FTPackNo= A.FTPackNo and SFG.FTSizeBreakDown=A.FTSizeBreakDown"
        '_Qry &= vbCrLf & "  	GROUP BY SBD.FTOrderNo,SBD.FTColorway,SBD.FTNikePOLineItem,SFG.FTBarCodeCarton,SFG.FTPackNo,A.FNCartonNo"
        '_Qry &= vbCrLf & ") AS SD ON SFG.FTOrderNo=SD.FTOrderNo and SFG.FTColorWay=SD.FTColorway and SFG.FTPackNo=SD.FTPackNo and SFG.FNCartonNo=SD.FNCartonNo and SFG.FTBarCodeCarton=SD.FTBarCodeCarton"
        '_Qry &= vbCrLf & "WHERE F.FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "' AND SFG.FTBarCodeCarton=F.FTBarCodeCarton AND SFG.FTOrderNo=M.FTOrderNo AND M.FNHSysStyleId=S.FNHSysStyleId"
        '_Qry &= vbCrLf & "group by SFG.FTPackNo,SFG.FTOrderNo,S.FTStyleCode,M.FTPORef,f.FTBarCodeCarton,SFG.FTColorway,SFG.FTSizeBreakDown,SFG.FNQuantity,SD.FTNikePOLineItem,SFG.FNCartonNo ,isnull(T.FTStateApprove,'0') , F.FNQuantityRcv , F.FNQuantity      "

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.SP_GET_TRANSFERDATA '" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)

        Dim _Column As String = "FTPackNo|FTOrderNo|FTStyleCode|FTPORef|FTBarCodeCarton|FTNikePOLineItem|FNCartonNo|FNQuantityRcv|FTColorway|FNTotal"

        With Me.ogvDetail
            .BeginInit()



            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString

                    Case "FTPackNo", "FTOrderNo", "FTStyleCode", "FTPORef", "FTBarCodeCarton", "FTNikePOLineItem", "FNCartonNo", "FTColorway", "FNTotal"
                        .Columns(I).AppearanceCell.BackColor = Color.White
                        .Columns(I).AppearanceCell.ForeColor = Color.Black
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case "FTSelect"
                        .Columns(I).AppearanceCell.BackColor = Color.White
                        .Columns(I).AppearanceCell.ForeColor = Color.Black
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next
            Dim _colcount As Integer = 0
            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString
                        Case "FTPackNo", "FTOrderNo", "FTStyleCode", "FTPORef", "FTBarCodeCarton", "FTNikePOLineItem", "FNCartonNo", "FTColorway", "FNTotal", "FTSelect"
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                            End With
                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With
                            End With
                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                    End Select
                Next

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                Next
            End If
            .EndInit()
        End With
        ogcDetail.DataSource = _dt
    End Sub

    Private Sub LoadWH(key As String)
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT W.FTWHFGCode FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG as F WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouseFG AS W WITH(NOLOCK) ON F.FNHSysWHIdFG=W.FNHSysWHFGId"
        _Qry &= vbCrLf & "WHERE F.FTTransferFGNo='" & key.ToString & "'"
        _Qry &= vbCrLf & "AND F.FNHSysWHIdFG=W.FNHSysWHFGId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)

        For Each R As DataRow In _dt.Rows
            FNHSysWHIdFG.Text = R!FTWHFGCode.ToString
        Next

        _Qry = "SELECT W.FTWHFGCode FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG as F WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouseFG AS W WITH(NOLOCK) ON F.FNHSysWHIdFGTo=W.FNHSysWHFGId"
        _Qry &= vbCrLf & "WHERE F.FTTransferFGNo='" & key.ToString & "'"
        _Qry &= vbCrLf & "AND F.FNHSysWHIdFGTo=W.FNHSysWHFGId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)

        For Each R As DataRow In _dt.Rows
            FNHSysWHIdFGTo.Text = R!FTWHFGCode.ToString
        Next

    End Sub

#End Region

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click

        Dim _Qry As String = ""
        Dim _oDt As DataTable

        If Me.FTTransferFGNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTTransferWHNo_lbl.Text)
            Me.FTTransferFGNo.Focus()
            Exit Sub
        End If

        With DirectCast(Me.ogcDetail.DataSource, DataTable)
            .AcceptChanges()
            _oDt = .Copy
        End With
        Try
            For Each R As DataRow In _oDt.Rows
                _Qry = " Select Top 1  FTBarCodeCarton   From    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGIssueFG_Detail with(nolock)   "
                _Qry &= vbCrLf & " where   FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FG, "") = "" Then
                    _Qry = " Update   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail  "
                    _Qry &= vbCrLf & "  set  FNQuantityRcv =" & IIf(R!FTSelect.ToString = "1", Double.Parse("0" & R!FNTotal.ToString), 0)
                    _Qry &= vbCrLf & " where   FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "' "
                    _Qry &= vbCrLf & " and FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    _Qry &= vbCrLf & "  and  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                    _Qry &= vbCrLf & "  and  FNCartonNo=" & Integer.Parse("0" & R!FNCartonNo.ToString)
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_FG)
                End If

            Next
        Catch ex As Exception
            HI.MG.ShowMsg.mInfo("approved Receive Transfer Finish Goods unsuccessful !!!!!", 1802031447, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            If Me.FTTransferFGNo.Text <> "" Then
                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG"
                _Qry &= vbCrLf & "SET FTStateApprove='1'"
                _Qry &= vbCrLf & ",FTApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDApproveDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTApproveTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "WHERE FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "' AND ISNULL(FTStateApprove,'')<>'1'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_FG)
                Me.LoadDataInfo(Me.FTTransferFGNo.Text)

                HI.MG.ShowMsg.mInfo("approved Receive Transfer Finish Goods successfully !!!!", 1802031446, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Information)

            Else
                HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, Me.FTTransferWHNo_lbl.Text)
            End If

        Catch ex As Exception
            HI.MG.ShowMsg.mInfo("approved Receive Transfer Finish Goods unsuccessful !!!!!", 1802031447, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End Try


    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Dim _Qry As String = ""

        If Me.FTTransferFGNo.Text <> "" Then
            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG"
            _Qry &= vbCrLf & "SET FTStateApprove='0',FTApproveBy='',FDApproveDate='',FTApproveTime=''"
            _Qry &= vbCrLf & ",FTCancelBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ",FDCancelDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & ",FTCancelTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & "WHERE FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "' AND ISNULL(FTStateApprove,'')='1'"

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_FG)
            Me.LoadDataInfo(Me.FTTransferFGNo.Text)
        Else
            HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, Me.FTTransferWHNo_lbl.Text)
        End If

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

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Call FormRefresh()
        ogcDetail.DataSource = Nothing

    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.oSelectAll.Checked Then
                _State = "1"
            End If
            With ogcDetail
                If Not (.DataSource Is Nothing) And ogvDetail.RowCount > 0 Then
                    With ogvDetail
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTProductBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTProductBarcodeNo.KeyDown
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _FTTranFerNo As String = ""
        Select Case e.KeyCode
            Case Keys.Enter
                If Me.FTTransferFGNo.Text <> "" Then
                    _Qry = " Select Top 1  FTTransferFGNo   From    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail with(nolock)   "
                    _Qry &= vbCrLf & " where   FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "'"
                    _FTTranFerNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FG, "")
                    If _FTTranFerNo <> "" Then
                        'HI.MG.ShowMsg.mInfo("ไม่สามารถเพิ่มข้อมูลได้ เนื่องจากปลายทางได้ทำการอนุมัติแล้ว !!!!", 1802121603, Me.Text)
                        'Exit Sub
                        If Me.FTTransferFGNo.Text <> _FTTranFerNo Then
                            Me.FTTransferFGNo.Text = _FTTranFerNo
                        End If

                        With DirectCast(Me.ogcDetail.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Select("FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "'")
                                R!FTSelect = "1"
                            Next
                            .AcceptChanges()
                        End With
                    End If

                    'Else
                    '    HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, FDTransferWHDate_lbl.Text, Me.Text)
                    '    Me.FTTransferFGNo.Focus()
                    '    Exit Sub
                End If
        End Select
    End Sub


    'Private Sub ogvDetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvDetail.CellMerge
    '    With ogvDetail
    '        Select Case e.Column.FieldName
    '            Case "FTColorway"
    '                If "" & .GetRowCellValue(e.RowHandle1, "FTTransferFGNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTTransferFGNo").ToString _
    '                    And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
    '                    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
    '                    e.Handled = True
    '                    e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '                Else
    '                    e.Merge = False
    '                    e.Handled = True
    '                End If
    '            Case "FTSizeBreakDown"

    '                If ("" & .GetRowCellValue(e.RowHandle1, "FTTransferFGNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTTransferFGNo").ToString) _
    '                    And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
    '                    And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

    '                    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
    '                    e.Handled = True
    '                    e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '                Else
    '                    e.Merge = False
    '                    e.Handled = True
    '                End If


    '        End Select
    '    End With

    'End Sub

End Class
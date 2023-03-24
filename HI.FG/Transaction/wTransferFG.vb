Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing


Public Class wTransferFG
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private StateCal As Boolean = False
    Private _ProcLoad As Boolean = False
    Private _ListCarton As wListCarton
    Private _TransferList As wTransferList

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitFormControl()

        _ListCarton = New wListCarton
        HI.TL.HandlerControl.AddHandlerObj(_ListCarton)

        _TransferList = New wTransferList
        HI.TL.HandlerControl.AddHandlerObj(_TransferList)


        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _ListCarton.Name.ToString.Trim, _ListCarton)
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _TransferList.Name.ToString.Trim, _TransferList)
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

    'Private Sub LoadDataBarCodeIngrid(Key As String)
    '    Dim _Qry As String = ""
    '    Dim _dt As DataTable

    '    _Qry = "select f.FTTransferFGNo,f.FTBarCodeCarton,f.FNQuantity "
    '    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail as F"
    '    _Qry &= vbCrLf & "WHERE f.FTTransferFGNo='" & Key.ToString & "'"
    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
    '    ogcDetail.DataSource = _dt

    'End Sub

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG WHERE FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail WHERE FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG WHERE FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "'")

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
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


#End Region

    Private Function VerrifyDataDetail() As Boolean
        Dim _Qry As String = ""
        Dim _barcode As String

        _Qry = "SELECT F.FTBarCodeCarton"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail AS F"
        _Qry &= vbCrLf & "where f.FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "' AND f.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "'"

        _barcode = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FG)

        If _barcode.ToString = "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1508241005, "มีบาร์โค๊ดอยู่แล้ว", Me.FTProductBarcodeNo.Text)
            Return False
        End If

        Me.FTProductBarcodeNo.Focus()
    End Function

    Private Sub wTransferFG_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FTProductBarcodeNo.EnterMoveNextControl = False

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveDetail() As Boolean
        Dim _Qry As String = ""
        Dim _Qrys As String = ""
        Dim _dt As DataTable
        Try
            Dim _Cmd As String = "" : Dim _BarcodeCarton As String = "" : Dim _BarcodeBundle As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT Top 1 B.FTBarCodeCarton"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS B WITH (NOLOCK)"
            _Cmd &= vbCrLf & "where B.FTBarCodeCarton = '" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            If _oDt.Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลบาร์โค๊ด กรุณาตรวจสอบ..!!!!!", 1508071614, Me.Text)
                Return False
            End If
            For Each R As DataRow In _oDt.Rows
                _BarcodeCarton = R!FTBarCodeCarton.ToString
            Next
            Dim _oodt As DataTable : Dim _FTTransferFGNo As String = ""
            _Cmd = "SELECT TFG.FTTransferFGNo"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG AS TFG"
            _Cmd &= vbCrLf & "WHERE TFG.FTTransferFGNo='" & Me.FTTransferFGNo.Text & "'"
            _oodt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)

            For Each R As DataRow In _oodt.Rows
                _FTTransferFGNo = R!FTTransferFGNo.ToString
            Next

            _Cmd = "SELECT '0' AS FTState , A.FTBarCodeCarton,sum(FNQuantity) AS FNQuantity , FTPackNo , FNCartonNo"
            _Cmd &= vbCrLf & "FROM"
            _Cmd &= vbCrLf & "(SELECT  F.FTBarCodeCarton,F.FTColorWay,F.FTSizeBreakDown,F.FNQuantity  , F.FTPackNo , F.FNCartonNo"
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG AS W WITH (NOLOCK) ON F.FNHSysWHFGId = W.FNHSysWHFGId"
            _Cmd &= vbCrLf & "WHERE  (F.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_BarcodeCarton) & "'"
            _Cmd &= vbCrLf & " OR  F.FTBarCodeCarton = '" & HI.UL.ULF.rpQuoted(_BarcodeBundle) & "')"
            _Cmd &= vbCrLf & "and F.FTBarCodeCarton +'|'+F.FTPackNo+'|'+convert(varchar(18),F.FNCartonNo) not in ( "
            _Cmd &= vbCrLf & " Select  FTBarCodeCarton +'|'+ FTPackNo+'|'+convert(varchar(18), FNCartonNo) From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail WITH(NOLOCK)  Where FTBarCodeCarton ='" & HI.UL.ULF.rpQuoted(_BarcodeCarton) & "') "
            _Cmd &= vbCrLf & "group by F.FTBarCodeCarton,F.FTColorWay,F.FTSizeBreakDown,F.FNQuantity , F.FTPackNo , F.FNCartonNo ) AS A"
            _Cmd &= vbCrLf & "group by a.FTBarCodeCarton , FTPackNo , FNCartonNo "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)

            If _dt.Rows.Count > 1 Then
                With _ListCarton
                    .ogcdetail.DataSource = _dt
                    .ShowDialog()
                    If (.Pass) Then
                        With CType(.ogcdetail.DataSource, DataTable)
                            .AcceptChanges()
                            _dt = .Copy
                        End With
                        _dt = _dt.Select("FTState='1'").CopyToDataTable
                    Else
                        Return False
                    End If
                End With
            End If

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FG)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            If _dt.Rows.Count > 0 Then
                For Each R As DataRow In _dt.Rows
                    _Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail"
                    _Qry &= vbCrLf & "(  FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ",FTTransferFGNo,FTBarCodeCarton,FNQuantity, FTPackNo , FNCartonNo)"
                    _Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTTransferFGNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'," & Val(R!FNQuantity.ToString) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'," & Integer.Parse("0" & R!FNCartonNo.ToString)
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                Next
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Me.FTProductBarcodeNo.Focus()
                Return True
            Else
                HI.MG.ShowMsg.mProcessNotComplete(HI.MG.ShowMsg.ProcessType.mSave, Me.Text)
                Return False
            End If
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Me.FTProductBarcodeNo.Focus()
            Return False
        End Try
    End Function

    Private Function DeleteBarcode() As Boolean
        Dim _Qry As String = ""

        If CheckStageApprove() = True Then
            With ogvDetail
                If .RowCount <= 0 Then Return False
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Return False

                For Each i As Integer In .GetSelectedRows()
                    Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarCodeCarton").ToString
                    Dim _FGNo As String = "" & .GetRowCellValue(i, "FTTransferFGNo").ToString
                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                        _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail WHERE FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_Barcode.ToString) & "' AND FTTransferFGNo='" & HI.UL.ULF.rpQuoted(_FGNo.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        Else
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return True
                        End If
                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End Try
                Next
            End With
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถลบได้ เนื่องจากปลายทางได้ทำการอนุมัติแล้ว !!!!", 1509040926, Me.Text)
        End If
    End Function

    Private Sub LoadBarcodeOnScanbarcode()
        Dim _Qry As String = ""
        Dim LastBarcode As String
        _Qry = "select top 1 F.FTBarCodeCarton from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail AS F "
        _Qry &= vbCrLf & "ORDER BY F.FTBarCodeCarton desc"
        LastBarcode = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FG)
        FTProductBarcodeNo.Text = LastBarcode.ToString
    End Sub

    Private Sub LoadGridDetail()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "select f.FTTransferFGNo,SFG.FTOrderNo,S.FTStyleCode,M.FTPORef,f.FTBarCodeCarton,SFG.FTColorway,SFG.FTSizeBreakDown,SFG.FNQuantity AS FNQuantity , F.FTPackNo , F.FNCartonNo ,SD.FTNikePOLineItem"
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG AS SFG WITH(NOLOCK) "
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail as F WITH(NOLOCK) ON SFG.FTBarCodeCarton=F.FTBarCodeCarton and SFG.FTPackNo = F.FTPackNo and SFG.FNCartonNo = F.FNCartonNo"
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS M WITH(NOLOCK) ON SFG.FTOrderNo=M.FTOrderNo"
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON M.FNHSysStyleId=S.FNHSysStyleId"
        _Qry &= vbCrLf & "LEFT OUTER JOIN ("
        _Qry &= vbCrLf & "  SELECT  isnull(SBD.FTNikePOLineItem,'') AS FTNikePOLineItem,SBD.FTOrderNo,SBD.FTColorway,A.FNCartonNo,SFG.FTBarCodeCarton,SFG.FTPackNo"
        _Qry &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS SFG ON SBD.FTOrderNo=SFG.FTOrderNo and SBD.FTSizeBreakDown=SFG.FTSizeBreakDown and SBD.FTColorway=SFG.FTColorway LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A ON SBD.FTOrderNo=A.FTOrderNo and SFG.FTColorway=A.FTColorway and SFG.FNCartonNo=A.FNCartonNo and SBD.FTSubOrderNo=A.FTSubOrderNo and SFG.FTPackNo= A.FTPackNo and SFG.FTSizeBreakDown=A.FTSizeBreakDown"
        _Qry &= vbCrLf & "  	GROUP BY SBD.FTOrderNo,SBD.FTColorway,SBD.FTNikePOLineItem,SFG.FTBarCodeCarton,SFG.FTPackNo,A.FNCartonNo"
        _Qry &= vbCrLf & ") AS SD ON SFG.FTOrderNo=SD.FTOrderNo and SFG.FTColorWay=SD.FTColorway and SFG.FTPackNo=SD.FTPackNo and SFG.FNCartonNo=SD.FNCartonNo and SFG.FTBarCodeCarton=SD.FTBarCodeCarton"
        _Qry &= vbCrLf & "WHERE F.FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "' AND SFG.FTBarCodeCarton=F.FTBarCodeCarton AND SFG.FTOrderNo=M.FTOrderNo AND M.FNHSysStyleId=S.FNHSysStyleId"
        _Qry &= vbCrLf & "group by f.FTTransferFGNo,SFG.FTOrderNo,S.FTStyleCode,M.FTPORef,f.FTBarCodeCarton,SFG.FTColorway,SFG.FTSizeBreakDown , F.FTPackNo , F.FNCartonNo ,SFG.FNQuantity,SD.FTNikePOLineItem"
        _Qry &= vbCrLf & " ORDER BY  F.FNCartonNo ASC "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
        ogcDetail.DataSource = _dt
    End Sub

    Private Sub FTProductBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTProductBarcodeNo.KeyDown

        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Select Case e.KeyCode
            Case Keys.Enter
                If Me.FDDateTransferFG.Text <> "" Then
                    If CheckStageApprove() = False Then
                        HI.MG.ShowMsg.mInfo("ไม่สามารถเพิ่มข้อมูลได้ เนื่องจากปลายทางได้ทำการอนุมัติแล้ว !!!!", 1509040946, Me.Text)
                        Exit Sub
                    End If

                    If Me.FTTransferFGNo.Text <> "" Then

                        If Me.FNHSysWHIdFG.Text <> "" Then

                            If Me.FNHSysWHIdFGTo.Text <> "" Then
                                _Qry = "SELECT TOP 1 FTTransferFGNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG"
                                _Qry &= vbCrLf & "WHERE FTTransferFGNo='" & HI.UL.ULF.rpQuoted(FTTransferFGNo.Text) & "'"
                                Dim chk As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FG)

                                If chk = "" Then
                                    If Me.SaveData() Then
                                        ' If VerrifyDataDetail() Then
                                        If SaveDetail() Then
                                            Me.FTProductBarcodeNo.Focus()
                                            Me.FTProductBarcodeNo.SelectAll()
                                            Call LoadGridDetail()
                                        Else
                                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                            Exit Sub
                                        End If
                                        'Else
                                        '    Exit Sub
                                        'End If
                                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                    Else
                                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                    End If
                                Else
                                    ' If VerrifyDataDetail() Then
                                    If SaveDetail() Then
                                        Me.FTProductBarcodeNo.Focus()
                                        Me.FTProductBarcodeNo.SelectAll()
                                        Call LoadGridDetail()
                                    Else
                                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                        Exit Sub
                                    End If
                                End If

                            Else
                                HI.MG.ShowMsg.mInvalidData("กรุณาเลือกคลังปลายทาง!!!!", 1508201634, Me.Text)
                                FNHSysWHIdFGTo.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData("กรุณาเลือกคลังต้นทางก่อน !!!!", 1508201626, Me.Text)
                            FNHSysWHIdFG.Focus()
                            Exit Sub
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, FTTransferWHNo_lbl.Text, Me.Text)
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, FDTransferWHDate_lbl.Text, Me.Text)
                    Me.FTTransferFGNo.Focus()
                    Exit Sub
                End If
        End Select
    End Sub


    Private Sub ocmSave_Click(sender As Object, e As EventArgs) Handles ocmSave.Click

        If Me.FTTransferFGNo.Text <> "" Then

            If Me.FNHSysWHIdFG.Text <> "" Then

                If Me.FNHSysWHIdFGTo.Text <> "" Then
                    If Me.SaveData() Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                    Exit Sub
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาเลือกคลังปลายทาง!!!!", 1508201634, Me.Text)
                    FNHSysWHIdFGTo.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาเลือกคลังต้นทางก่อน !!!!", 1508201626, Me.Text)
                FNHSysWHIdFG.Focus()
                Exit Sub
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, FTTransferWHNo_lbl.Text, Me.Text)
        End If


    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click

        Call LoadDataInfo(Me.FTTransferFGNo.Text)
        Call LoadGridDetail()

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Call FormRefresh()
        Me.ogcDetail.DataSource = Nothing
        Me.FNHSysWHIdFG.Properties.Buttons.Item(0).Enabled = True
        Me.FNHSysWHIdFG.Properties.ReadOnly = False
        Me.FNHSysWHIdFGTo.Properties.Buttons.Item(0).Enabled = True
        Me.FNHSysWHIdFGTo.Properties.ReadOnly = False
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

        If CheckStageApprove() = True Then
            If CheckTransactionMI() Then
                HI.MG.ShowMsg.mInfo("เลขที่ใบโอนนี้มีการทำรายการไปแล้ว กรุณาตรวจสอบ..", 1602041449, Me.Text, "", MessageBoxIcon.Stop)
                Exit Sub
            End If
            If Me.FTTransferFGNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, Me.FTTransferWHNo_lbl.Text)
                Exit Sub
            Else
                If HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการลบข้อมูลทรายเฟอร์หรือไม่", 1508251135, Me.Text) = True Then
                    If DeleteData() Then
                        Call FormRefresh()
                        LoadGridDetail()
                    End If
                    Exit Sub
                End If
            End If
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถลบได้ เนื่องจากปลายทางได้ทำการอนุมัติแล้ว !!!!", 1509040926, Me.Text)
        End If
    End Sub

    Private Function CheckStageApprove() As Boolean
        Dim _Qry As String = ""

        _Qry = "SELECT TOP 1 ISNULL(F.FTStateApprove,'') FROM[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG AS F"
        _Qry &= vbCrLf & "WHERE F.FTTransferFGNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "'"

        Dim StateTure As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FG)

        If StateTure = "0" Or StateTure = "" Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ocmdeletebarcode_Click(sender As Object, e As EventArgs) Handles ocmdeletebarcode.Click
        If CheckTransactionMI() Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถลบบาร์โค้ดได้ เนื่องจากมีการทำรายการจากบาโค้ดด้วยนี้แล้ว...", 1602041510, Me.Text, "", MessageBoxIcon.Stop)
            Exit Sub
        End If
        Call DeleteBarcode()
        Me.FTProductBarcodeNo.Focus()
        Me.FTProductBarcodeNo.SelectAll()
        Call LoadGridDetail()
    End Sub


    Private Sub ogvDetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvDetail.CellMerge
        With ogvDetail
            Select Case e.Column.FieldName
                Case "FTColorway"
                    If "" & .GetRowCellValue(e.RowHandle1, "FTTransferFGNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTTransferFGNo").ToString _
                        And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                        e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
                Case "FTSizeBreakDown"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTTransferFGNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTTransferFGNo").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                        And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                        e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
            End Select
        End With

    End Sub

    Private Sub ogvDetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvDetail.RowCountChanged
        Try
            With ogvDetail
                If .RowCount > 0 Then
                    Me.FNHSysWHIdFG.Properties.Buttons.Item(0).Enabled = False
                    Me.FNHSysWHIdFGTo.Properties.Buttons.Item(0).Enabled = False
                    Me.FNHSysWHIdFG.Properties.ReadOnly = True
                    Me.FNHSysWHIdFGTo.Properties.ReadOnly = True
                Else
                    Me.FNHSysWHIdFG.Properties.Buttons.Item(0).Enabled = True
                    Me.FNHSysWHIdFGTo.Properties.Buttons.Item(0).Enabled = True
                    Me.FNHSysWHIdFGTo.Properties.ReadOnly = False
                    Me.FNHSysWHIdFG.Properties.ReadOnly = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Function CheckTransactionMI() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FTCustomerPO  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_DocRef WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> ""
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            If Not (VerrifyData()) Then Exit Sub
            If Not (SaveData()) Then Exit Sub
            Dim _Qry As String = ""
            Dim _oDt As DataTable : Dim _dt As DataTable
            With _TransferList
                .WHCode = Me.FNHSysWHIdFG.Text
                .TransferFGNo = Me.FTTransferFGNo.Text
                .ShowDialog()
                If (.Pass) Then
                    With DirectCast(.ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        With DirectCast(Me.ogcDetail.DataSource, DataTable)
                            .AcceptChanges()
                            _oDt = .Copy
                        End With
                        '_oDt.BeginInit()
                        _dt = .Select("FTSelect = '1'").CopyToDataTable
                        'For Each R As DataRow In _dt.Rows
                        '    _oDt.Rows.Add(R!FTTransferFGNo.ToString, R!FTOrderNo.ToString, R!FTStyleCode.ToString, R!FTPORef.ToString, R!FTBarCodeCarton.ToString, R!FTColorway.ToString, _
                        '                  R!FTSizeBreakDown.ToString, Integer.Parse("0" & R!FNQuantity), R!FTPackNo.ToString, Integer.Parse("0" & R!FNCartonNo.ToString))
                        'Next
                        '_oDt.EndInit()
                    End With
                    'Me.ogcDetail.DataSource = _oDt

                    Try

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FG)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                        If _dt.Rows.Count > 0 Then
                            For Each R As DataRow In _dt.Rows
                                _Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail"
                                _Qry &= vbCrLf & "(  FTInsUser, FDInsDate, FTInsTime"
                                _Qry &= vbCrLf & ",FTTransferFGNo,FTBarCodeCarton,FNQuantity, FTPackNo , FNCartonNo)"
                                _Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTTransferFGNo.Text) & "','" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'," & Val(R!FNQuantity.ToString) & ""
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'," & Integer.Parse("0" & R!FNCartonNo.ToString)
                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Exit Sub
                                End If
                            Next
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Me.FTProductBarcodeNo.Focus()
                            Call LoadGridDetail()
                        Else
                            HI.MG.ShowMsg.mProcessNotComplete(HI.MG.ShowMsg.ProcessType.mSave, Me.Text)
                            Exit Sub
                        End If
                    Catch ex As Exception

                    End Try
                End If

            End With
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

    'Private Sub ogvDetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvDetail.RowStyle
    '    Try
    '        Dim View As GridView = sender
    '        If (e.RowHandle >= 0) Then
    '            Dim _CartonNo_Hold As String = "" : Dim _State As Boolean = True
    '            Dim _CartonNo As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNCartonNo"))
    '            If _CartonNo_Hold = "" Then
    '                _CartonNo_Hold = _CartonNo
    '                _State = True
    '            End If
    '            If _CartonNo_Hold <> _CartonNo Then
    '                _State = Not (_State)
    '            End If

    '            If (_State) Then
    '                e.Appearance.BackColor = Color.Salmon
    '                e.Appearance.BackColor2 = Color.SeaShell
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
End Class
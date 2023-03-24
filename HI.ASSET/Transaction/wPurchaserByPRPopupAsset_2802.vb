Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls

Public Class wPurchaserByPRPopupAsset
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_FIXED
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    'Private _AddItemPopup As wAddItemPOAsset
    'Private _RevisedPopup As wPurchaseReviseRemark
    Private _ListPop As wListAutoPurchaseAssetNo
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True

    Sub New()
        _FormLoad = True

        ' This call is required by the designer.
        InitializeComponent()
        '   Call PrepareForm()
        _ListPop = New wListAutoPurchaseAssetNo
        'TL.HandlerControl.AddHandlerObj(_ListPop)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListPop.Name.ToString.Trim, _ListPop)
        Catch ex As Exception
        Finally
        End Try

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Property"
    Private _PRState As Integer = 0
    Public Property PRState As Integer
        Get
            Return _PRState
        End Get
        Set(value As Integer)
            _PRState = value
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

    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Private _oDtRef As DataTable
    Public Property oDtRef As DataTable
        Get
            Return _oDtRef
        End Get
        Set(value As DataTable)
            _oDtRef = value
        End Set
    End Property
#End Region

#Region "Proceducre"
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
        If _PRState <> 1 Then
            _Str &= vbCrLf & " WHERE FTDynamicFormName='wPurchaserAsset'"
        Else
            _Str &= vbCrLf & " WHERE FTDynamicFormName='wPurchaserServiceAsset'"
        End If
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

    Private Function VerrifyData() As Boolean
        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
                _Caption = ""

                Select Case _FieldName.ToString
                    Case "FTPurchaseNo", "FDPurchaseDate", "FTPurchaseBy"
                    Case Else
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
                End Select
            Next
        Next

        If FNExchangeRate.Value <= 0 Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
            FNExchangeRate.Focus()
            Return False
        End If

        Return True
    End Function

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

    Private Function SaveData(ByRef _dt As DataTable) As Boolean
        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = True
        Dim _CmpH As String = ""
        'Dim _StateUpdate As Boolean = False

        For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

            Select Case HI.ENM.Control.GeTypeControl(ctrl)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                    End With

                    Exit For
                Case ENM.Control.ControlType.TextEdit
                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK) WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                    End With

                    Exit For
            End Select

        Next

        If (_StateNew) Then
            Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
            Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
            If _PRState <> 1 Then
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & FNHSysCmpRunId.Text & _Year & "ASSET" & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
            Else
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
            End If
            '  FTPurchaseState.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & " MANUAL " & HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)) & " " & Format(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM), "HH:mm:ss")
            FDPurchaseDate.Text = HI.Conn.SQLConn.GetField("Select Convert(varchar(10),Getdate(),103)", Conn.DB.DataBaseName.DB_FIXED, "")
                FTPurchaseBy.Text = HI.ST.UserInfo.UserName
            End If



        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
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
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
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
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
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
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
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
                                Case UCase("FDUpdDate"), UCase("FTUpdDate")
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
                    ' _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "

                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)

                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select

            Next
            If SaveDetail(_Key) Then
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _dt.Rows.Add("0", FNHSysSuplId.Text, FNHSysSuplId_None.Text, _Key, "")
                Return True
            Else
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function SaveDetail(Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _Price As Double = 0 : Dim _DisPer As Double = 0 : Dim _NetAmt As Double = 0
            Dim _FNDisAmt As Double = 0
            Dim _FNHSysUnitId As Integer = 0
            Dim _FTDescription As String = ""
            Dim _FNSeq As Integer = 0
            With CType(Me.ogcsum.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            For Each R As DataRow In _oDtRef.Rows

                For Each x As DataRow In _oDt.Select("FTAssetCode='" & (R!FTAssetCode.ToString) & "'")
                    _Price = Double.Parse(x!FNPrice.ToString)
                    _DisPer = Double.Parse(x!FNDisPer.ToString)
                    _FNHSysUnitId = Val(x!FNHSysUnitId.ToString)
                    _FNDisAmt = Double.Parse(x!FNDisAmt.ToString)
                    _NetAmt = Double.Parse(R!FNQuantity.ToString) * _Price
                    _FTDescription = x!FTDescription.ToString
                    _FNSeq += 1
                    If _PRState <> 1 Then
                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail "
                        _Cmd &= vbCrLf & "SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Cmd &= vbCrLf & ",FNHSysUnitId=" & _FNHSysUnitId & ""
                        _Cmd &= vbCrLf & ",FNPrice=" & _Price & ""
                        _Cmd &= vbCrLf & ",FNDisPer=" & _DisPer & ""
                        _Cmd &= vbCrLf & ",FNDisAmt=" & (_NetAmt * _DisPer) / 100
                        _Cmd &= vbCrLf & ",FNQuantity=" & Double.Parse(R!FNQuantity.ToString) & ""
                        _Cmd &= vbCrLf & ",FNNetAmt=" & _NetAmt - ((_NetAmt * _FNDisAmt) / 100) & ""
                        _Cmd &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
                        _Cmd &= vbCrLf & "AND FNHSysFixedAssetId='" & Val(R!FNHSysFixedAssetId.ToString) & "' "
                        _Cmd &= vbCrLf & "AND FNSeq=" & x!FNSeq & ""
                    Else
                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail "
                        _Cmd &= vbCrLf & "SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Cmd &= vbCrLf & ",FNHSysUnitId=" & _FNHSysUnitId & ""
                        _Cmd &= vbCrLf & ",FNPrice=" & _Price & ""
                        _Cmd &= vbCrLf & ",FNDisPer=" & _DisPer & ""
                        _Cmd &= vbCrLf & ",FNDisAmt=" & (_NetAmt * _DisPer) / 100
                        _Cmd &= vbCrLf & ",FNQuantity=" & Double.Parse(R!FNQuantity.ToString) & ""
                        _Cmd &= vbCrLf & ",FNAmount=" & _NetAmt - ((_NetAmt * _FNDisAmt) / 100) & ""
                        _Cmd &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
                        _Cmd &= vbCrLf & "AND FNHSysFixedAssetId='" & Val(R!FNHSysFixedAssetId.ToString) & "' "
                        _Cmd &= vbCrLf & "AND FNSeq=" & x!FNSeq & ""
                    End If


                    '_Cmd &= vbCrLf & " ,FNSurchangeAmt=" & Double.Parse(R!FNQuantity.ToString) * _SurCharUnit & ""
                    '_Cmd &= vbCrLf & " ,FNGrandNetAmt=" & (_NetAmt - ((_NetAmt * Double.Parse(R!FNDisAmt.ToString)) / 100)) + (Double.Parse(R!FNQuantity.ToString) * _SurCharUnit) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        '_Cmd = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail(FTInsUser, FDInsDate, FTInsTime"
                        If _PRState <> 1 Then
                            _Cmd = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail(FTInsUser, FDInsDate, FTInsTime"
                            _Cmd &= vbCrLf & " , FTPurchaseNo,FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, "
                            _Cmd &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark)"
                            _Cmd &= vbCrLf & "  Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Key) & "' "
                            _Cmd &= vbCrLf & "," & _FNSeq & ""
                            _Cmd &= vbCrLf & "," & Val(x!FNHSysFixedAssetId.ToString) & " "
                            _Cmd &= vbCrLf & "," & _FNHSysUnitId & " "
                            _Cmd &= vbCrLf & "," & _Price & " "
                            _Cmd &= vbCrLf & "," & _DisPer & " "
                            _Cmd &= vbCrLf & "," & (_NetAmt * _DisPer) / 100 & " "
                            _Cmd &= vbCrLf & "," & Double.Parse(R!FNQuantity.ToString) & " "
                            _Cmd &= vbCrLf & "," & _NetAmt - ((_NetAmt * Double.Parse(x!FNDisAmt.ToString)) / 100) & " "
                            _Cmd &= vbCrLf & ",'" & x!FTRemark.ToString & "'"

                        Else
                            _Cmd = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail(FTInsUser, FDInsDate, FTInsTime"
                            _Cmd &= vbCrLf & " , FTPurchaseNo,FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, "
                            _Cmd &= vbCrLf & "    FNDisAmt, FNQuantity, FNAmount, FTRemark,FTDescription)"
                            _Cmd &= vbCrLf & "  Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Key) & "' "
                            _Cmd &= vbCrLf & "," & _FNSeq & ""
                            _Cmd &= vbCrLf & "," & Val(x!FNHSysFixedAssetId.ToString) & " "
                            _Cmd &= vbCrLf & "," & _FNHSysUnitId & " "
                            _Cmd &= vbCrLf & "," & _Price & " "
                            _Cmd &= vbCrLf & "," & _DisPer & " "
                            _Cmd &= vbCrLf & "," & (_NetAmt * _DisPer) / 100 & " "
                            _Cmd &= vbCrLf & "," & Double.Parse(R!FNQuantity.ToString) & " "
                            _Cmd &= vbCrLf & "," & _NetAmt - ((_NetAmt * Double.Parse(x!FNDisAmt.ToString)) / 100) & " "
                            _Cmd &= vbCrLf & ",'" & x!FTRemark.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & _FTDescription & "'"
                        End If

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail "
                    _Cmd &= vbCrLf & " Set FTPurchaseRefNo='" & HI.UL.ULF.rpQuoted(Key) & "'"
                    _Cmd &= vbCrLf & " Where FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "'"
                    _Cmd &= vbCrLf & " And FNHSysFixedAssetId=" & Val(x!FNHSysFixedAssetId.ToString) & " "
                    _Cmd &= vbCrLf & " And FNSeq=" & x!FNSeq & ""
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                Next
            Next

            'For Each R As DataRow In _oDtRef.Rows
            '    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TFIXEDTPurchase_Request_Detail "
            '    _Cmd &= vbCrLf & " Set FTPurchaseRefNo='" & HI.UL.ULF.rpQuoted(Key) & "'"
            '    _Cmd &= vbCrLf & " Where FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "'"
            '    _Cmd &= vbCrLf & " And FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & " "
            '    _Cmd &= vbCrLf & " And FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
            '    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '        HI.Conn.SQLConn.Tran.Rollback()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        Return False
            '    End If
            'Next
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs)
        Me.FormRefresh()
    End Sub


    Private Sub FNHSysCmpId_EditValueChanged(sender As Object, e As EventArgs)
        Dim joke As String = Me.FNHSysCmpId.Text
    End Sub

    Private Sub FNHSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCurId.EditValueChanged
        If _FormLoad Then Exit Sub
        If FNHSysCurId.Text = "" Then
            FNExchangeRate.Value = 0
            Exit Sub
        End If
        If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

            FNExchangeRate.Properties.ReadOnly = True

            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 1
            End If

        Else
            FNExchangeRate.Properties.ReadOnly = True
            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 0
                Dim _Qry As String = ""

                _Qry = " SELECT TOP 1 FNBuyingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & UL.ULDate.FormatDateDB & "')"
                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "'"
                _Qry &= vbCrLf & "  ))"

                FNExchangeRate.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

                If FNExchangeRate.Value <= 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
                End If

            End If

        End If
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                  FNPoAmt.EditValueChanged,
                                                                                  FNDisCountAmt.EditValueChanged,
                                                                                  FNVatPer.EditValueChanged,
                                                                                  FNVatAmt.EditValueChanged,
                                                                                  FNSurcharge.EditValueChanged,
                                                                                  FNTaxPer.EditValueChanged,
                                                                                  FNAmt.EditValueChanged

        Static _Proc As Boolean

        If Not (_Proc) And Not (_ProcLoad) Then
            _Proc = True
            Dim _POAmt As Double = FNPoAmt.Value

            If _POAmt = 0 Then
                FNDisCountAmt.Value = 0
                FNVatAmt.Value = 0
            End If

            Dim _DisPer As Double = FNDisCountPer.Value
            Dim _DisAmt As Double = FNDisCountAmt.Value
            Dim _VatPer As Double = FNVatPer.Value
            Dim _VatAmt As Double = FNVatAmt.Value
            Dim _SurAmt As Double = FNSurcharge.Value
            Dim _TaxAmt As Double = FNVatAmt.Value
            Dim _TaxPer As Double = FNVatAmt.Value
            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper
                    _DisPer = FNDisCountPer.Value
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt
                Case "FNDisCountAmt".ToUpper
                    _DisAmt = FNDisCountAmt.Value

                    If _POAmt > 0 Then
                        _DisPer = Format((_DisAmt * 100) / _POAmt, HI.ST.Config.PercentFormat)
                    Else
                        _DisPer = 0
                    End If
                    FNDisCountPer.Value = _DisPer
                Case "FNVatPer".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
                Case "FNVatAmt".ToUpper
                    _VatAmt = FNVatAmt.Value

                    If (_POAmt - _DisAmt) > 0 Then
                        _VatPer = Format((_VatAmt * 100) / (_POAmt - _DisAmt), HI.ST.Config.PercentFormat)
                    Else
                        _VatPer = 0
                    End If
                    FNVatPer.Value = _VatPer
                Case Else
                    '_DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    'FNDisCountAmt.Value = _DisAmt

                    '_VatPer = FNVatPer.Value
                    '_VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    'FNVatAmt.Value = _VatAmt

                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt
                    _TaxPer = FNTaxPer.Value
                    _TaxAmt = Format(((_POAmt - _DisAmt) * _TaxPer) / 100, HI.ST.Config.AmtFormat)
                    FNAmt.Value = (_POAmt - _DisAmt)
                    FNTaxAmt.Value = _TaxAmt
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format((((_POAmt - _DisAmt) - _TaxAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select
            Me.FNPONetAmt.Value = (_POAmt - _DisAmt) - _TaxAmt

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)
            Me.FTPOGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNPOGrandAmt.Value)
            Me.FTPOGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNPOGrandAmt.Value)
            _Proc = False
        End If
    End Sub

    Private Sub wPurchaserByPRPopupAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _FormLoad = False
            Call PrepareForm()
            Call Calculate(FNPrice, New System.EventArgs)
            Me.FDPurchaseDate.Text = HI.ST.UserInfo.LogINDate
            Me.FTPurchaseBy.Text = HI.ST.UserInfo.UserName
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
            Call SumPO()

        Catch ex As Exception
        End Try
    End Sub

    Private Function CheckReceive(POKey As String) As Boolean
        Dim _Pass As Boolean = False
        Dim _Str As String = ""

        _Str = "Select TOP 1 FTPurchaseNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive As R WITH(NOLOCK) WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "
        If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "") <> "" Then
            MG.ShowMsg.mProcessError(201610261712, "มีการรับสินทรัพย์แล้ว", Me.Text, MessageBoxIcon.Information)
            _Pass = True
        End If
        Return _Pass
    End Function

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With ogvdetail
                If .GetRowCellValue(e.RowHandle, "FTStateRcv") = "1" Then
                    e.Appearance.ForeColor = System.Drawing.Color.Green
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Dim _TmpPO As New DataTable : Dim Qry As String = ""
        Try
            If VerrifyData() Then
                '_TmpPO = HI.Conn.SQLConn.GetDataTable("SELECT '' AS FTSelect, '' AS FTSupplier,'' AS FTSupplierName ,'' AS FTPurchaseNo,Convert(nvarchar(500),'') AS FTItemRef", Conn.DB.DataBaseName.DB_FIXED)
                _TmpPO.Columns.Add("FTSelect")
                _TmpPO.Columns.Add("FTSupplier")
                _TmpPO.Columns.Add("FTSupplierName")
                _TmpPO.Columns.Add("FTPurchaseNo")
                _TmpPO.Columns.Add("FTItemRef")

                Me.ocmok.Enabled = False
                Dim _Spls As New HI.TL.SplashScreen("Generating PO...Data., Please Wait......")
                Try
                    If SaveData(_TmpPO) Then
                        ogcsum.DataSource = Nothing
                        _oDtRef = Nothing
                        Me.ocmok.Enabled = True
                        _Spls.Close()

                        If _TmpPO.Rows.Count > 0 Then
                            _TmpPO.BeginInit()
                            For Each R As DataRow In _TmpPO.Rows
                                Qry = "select [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.fn_POHeader_ItemInfo('" & R!FTPurchaseNo.ToString & "')"
                                R!FTItemRef = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "")
                            Next
                            _TmpPO.EndInit()
                        End If

                        With _ListPop
                            .ogclist.DataSource = _TmpPO
                            .ShowDialog()
                        End With
                        Me._State = True
                        Me.Close()
                    Else
                        Me.ocmok.Enabled = True
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                Catch ex As Exception
                    Me.ocmok.Enabled = True
                    _Spls.Close()
                End Try

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub


    Private Sub RepFNPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFNPrice.EditValueChanging
        Try
            With Me.ogvsum
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _NetAmt As Double = 0
                Dim _DisPer As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNDisPer"))
                Dim _DisAmt As Double = 0

                If _DisPer > 0 Then
                    .SetRowCellValue(.FocusedRowHandle, "FNDisAmt", CDbl(Format((e.NewValue * _DisPer) / 100, HI.ST.Config.PriceFormat)))
                Else
                    .SetRowCellValue(.FocusedRowHandle, "FNDisAmt", 0.0)
                End If

                _NetAmt = Format((Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) * (Double.Parse(e.NewValue) - Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNDisAmt")))), HI.ST.Config.AmtFormat)
                .SetRowCellValue(.FocusedRowHandle, "FNNetAmt", _NetAmt)

                'Dim _GndAmt As Double = 0
                '_GndAmt = Format(_NetAmt + Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNSurchangeAmt")), HI.ST.Config.AmtFormat)
                .SetRowCellValue(.FocusedRowHandle, "FNGrandNetAmt", _NetAmt)

                Call SumPO()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFNDisPer_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFNDisPer.EditValueChanging
        Try
            With Me.ogvsum
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _NetAmt As Double = 0 : Dim _DisAmt As Double = 0

                Dim _DisPer As Double = e.NewValue
                Dim _Price As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNPrice"))

                If _DisPer > 0 Then
                    _DisAmt = (((.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) * _Price) * _DisPer) / 100
                    .SetRowCellValue(.FocusedRowHandle, "FNDisAmt", _DisAmt)
                Else
                    .SetRowCellValue(.FocusedRowHandle, "FNDisAmt", 0.0)
                End If

                _NetAmt = Format(Double.Parse(((.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) * _Price)) - _DisAmt, ST.Config.AmtFormat)

                .SetRowCellValue(.FocusedRowHandle, "FNNetAmt", _NetAmt)

                'Dim _GndAmt As Double = 0
                '_GndAmt = Format(_NetAmt + Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNSurchangeAmt")), HI.ST.Config.AmtFormat)
                .SetRowCellValue(.FocusedRowHandle, "FNGrandNetAmt", _NetAmt)

                Call SumPO()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFNDisAmt_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNDisAmt.EditValueChanging
        Try
            With Me.ogvsum
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _NetAmt As Double = 0 : Dim _DisPer As Double = 0
                _NetAmt = Format((Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) * Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNPrice"))) - Double.Parse(e.NewValue), HI.ST.Config.AmtFormat)
                .SetRowCellValue(.FocusedRowHandle, "FNNetAmt", _NetAmt)
                _DisPer = Format((Double.Parse(e.NewValue) * 100) / (Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) * Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNPrice"))), HI.ST.Config.PercentFormat)
                .SetRowCellValue(.FocusedRowHandle, "FNDisPer", _DisPer)

                'Dim _GndAmt As Double = 0
                '_GndAmt = Format(_NetAmt + Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNSurchangeAmt")), HI.ST.Config.AmtFormat)
                .SetRowCellValue(.FocusedRowHandle, "FNGrandNetAmt", _NetAmt)

                Call SumPO()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SumPO()
        Try
            Static _Proc As Boolean

            If Not (_Proc) Then
                _Proc = True
                Dim _oDt As DataTable
                With CType(Me.ogcsum.DataSource, DataTable)
                    .AcceptChanges()
                    _oDt = .Copy
                End With
                Dim _NetAmt As Double = 0 : Dim _DisAmt As Double = 0
                For Each R As DataRow In _oDt.Rows
                    _NetAmt += +Double.Parse(R!FNGrandNetAmt.ToString)
                    '_DisAmt += +Double.Parse(R!FNDisAmt.ToString)
                Next
                Me.FNPoAmt.Value = _NetAmt
                'Me.FNDisCountAmt.Value = _DisAmt
                _Proc = False
            End If


        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged
        If _PRState <> 1 Then
            FNTaxPer.Value = 0
        Else
            FNTaxPer.Value = 3
        End If
        Call Calculate(FNPrice, New System.EventArgs)
    End Sub



End Class
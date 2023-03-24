Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Public Class wPurchaserByPRPopupServiceAsset
    '    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    '    Private _Bindgrid As Boolean = False
    '    Private _RowDcng As Boolean = False
    '    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    '    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    '    Private _AddItemPopup As wAddItemPOServiceAsset
    '    'Private _RevisePopup As wPurchaserAssetRevise

    '    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    '    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    '    Private _ProcLoad As Boolean = False
    '    Private _FormLoad As Boolean = True
    '    Sub New()

    '        ' This call is required by the designer.
    '        InitializeComponent()
    '        ' Add any initialization after the InitializeComponent() call.
    '        _FormLoad = True
    '        _AddItemPopup = New wAddItemPOServiceAsset
    '        TL.HandlerControl.AddHandlerObj(_AddItemPopup)
    '        Dim oSysLang As New ST.SysLanguage
    '        Try
    '            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
    '        Catch ex As Exception
    '        Finally
    '        End Try
    '        PrepareForm()

    '    End Sub

    '#Region "Property"
    '    Private _AssPath As String = ""
    '    Public Property AssPath As String
    '        Get
    '            Return _AssPath
    '        End Get
    '        Set(value As String)
    '            _AssPath = value
    '        End Set
    '    End Property

    '    Private _FormName As String = ""
    '    Public Property FormName As String
    '        Get
    '            Return _FormName
    '        End Get
    '        Set(value As String)
    '            _FormName = value
    '        End Set
    '    End Property

    '    Private _FormObjID As Integer = 0
    '    Public Property FormObjID As Integer
    '        Get
    '            Return _FormObjID
    '        End Get
    '        Set(value As Integer)
    '            _FormObjID = value
    '        End Set
    '    End Property

    '    Private _FormPopup As String = ""
    '    Public Property FormPopup As String
    '        Get
    '            Return _FormPopup
    '        End Get
    '        Set(value As String)
    '            _FormPopup = value
    '        End Set
    '    End Property

    '    Private _ObjectFocus As Object = Nothing
    '    Public Property ObjectFocus As Object
    '        Get
    '            Return _ObjectFocus
    '        End Get
    '        Set(value As Object)
    '            _ObjectFocus = value
    '        End Set
    '    End Property

    '    Private _SysDBName As String = ""
    '    Public Property SysDBName As String
    '        Get
    '            Return _SysDBName
    '        End Get
    '        Set(value As String)
    '            _SysDBName = value
    '        End Set
    '    End Property

    '    Private _SysTableName As String = ""
    '    Public Property SysTableName As String
    '        Get
    '            Return _SysTableName
    '        End Get
    '        Set(value As String)
    '            _SysTableName = value
    '        End Set
    '    End Property

    '    Private _SysDocType As String = ""
    '    Public Property SysDocType As String
    '        Get
    '            Return _SysDocType
    '        End Get
    '        Set(value As String)
    '            _SysDocType = value
    '        End Set
    '    End Property

    '    Private _TableName As String = ""
    '    Public Property TableName As String
    '        Get
    '            Return _TableName
    '        End Get
    '        Set(value As String)
    '            _TableName = value
    '        End Set
    '    End Property

    '    Private _MainKeyID As String = ""
    '    Public Property MainKeyID As String
    '        Get
    '            Return _MainKeyID
    '        End Get
    '        Set(value As String)
    '            _MainKeyID = value
    '        End Set
    '    End Property

    '    Public ReadOnly Property MainKey As String
    '        Get
    '            Return _FormHeader(0).MainKey
    '        End Get
    '    End Property

    '    Private _RequireField As String = ""
    '    Public Property RequireField As String
    '        Get
    '            Return _RequireField
    '        End Get
    '        Set(value As String)
    '            _RequireField = value
    '        End Set
    '    End Property

    '    Public ReadOnly Property Query As String
    '        Get
    '            Return _FormHeader(0).Query
    '        End Get
    '    End Property

    '    Private _ProcComplete As Boolean = False
    '    Public Property ProcComplete As Boolean
    '        Get
    '            Return _ProcComplete
    '        End Get
    '        Set(value As Boolean)
    '            _ProcComplete = value
    '        End Set
    '    End Property

    '    Private _Parent_Form As Object
    '    Public Property Parent_Form As Object
    '        Get
    '            Return _Parent_Form
    '        End Get
    '        Set(value As Object)
    '            _Parent_Form = value
    '        End Set
    '    End Property
    '    Private _PRState As Integer = 0
    '    Public Property PRState As Integer
    '        Get
    '            Return _PRState
    '        End Get
    '        Set(value As Integer)
    '            _PRState = value
    '        End Set
    '    End Property

    '    Private _State As Boolean = False
    '    Public Property State As Boolean
    '        Get
    '            Return _State
    '        End Get
    '        Set(value As Boolean)
    '            _State = value
    '        End Set
    '    End Property
    '    Private _oDtRef As DataTable
    '    Public Property oDtRef As DataTable
    '        Get
    '            Return _oDtRef
    '        End Get
    '        Set(value As DataTable)
    '            _oDtRef = value
    '        End Set
    '    End Property
    '#End Region

    '#Region "Proceducre"
    '    Private Sub PrepareForm()

    '        Dim _Str As String = ""
    '        Dim _objId As Integer
    '        Dim _dt As DataTable
    '        Dim _StrQuery As String = ""
    '        Dim _SortField As String = ""
    '        Dim _ColCount As Integer = 0
    '        Dim _StartX As Double = 0
    '        Dim _StartY As Double = 0
    '        Dim _CtrLv As Double = -1
    '        Dim _CtrHeight As Double = 0
    '        Dim _dtgrpobj As New DataTable


    '        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
    '        _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
    '        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
    '        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

    '        If _dt.Rows.Count > 0 Then

    '            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
    '            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
    '            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
    '            Me.TableName = _dt.Rows(0)!FTTableName.ToString

    '            _SortField = _dt.Rows(0)!FTSortField.ToString

    '            _Str = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
    '            _Str &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
    '            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm  WITH(NOLOCK)  "
    '            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
    '            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
    '            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)


    '            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
    '            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

    '            If _dt.Rows.Count > 0 Then

    '                For Each Row As DataRow In _dtgrpobj.Rows
    '                    Select Case Row!FNGenFormObj.ToString
    '                        Case "H"
    '                            Dim _DMF As New HI.TL.DynamicForm(_objId, Val(Row!FNFormObjID.ToString), _dt, Me)
    '                            _DMF.SysObjID = Val(Row!FNFormObjID.ToString)
    '                            _DMF.SysTableName = Row!FTTableName.ToString
    '                            _DMF.SysDBName = Row!FTBaseName.ToString
    '                            _FormHeader.Add(_DMF)

    '                    End Select
    '                Next

    '            End If

    '        End If

    '        _dt.Dispose()
    '        _dtgrpobj.Dispose()

    '    End Sub

    '    Public Sub LoadDataInfo(Key As Object)
    '        _ProcLoad = True


    '        Dim _Dt As DataTable
    '        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

    '        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

    '        Dim _FieldName As String = ""
    '        For Each R As DataRow In _Dt.Rows
    '            For Each Col As DataColumn In _Dt.Columns
    '                _FieldName = Col.ColumnName.ToString

    '                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
    '                    Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                        Case ENM.Control.ControlType.ButtonEdit
    '                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                                .Text = R.Item(Col).ToString
    '                            End With

    '                        Case ENM.Control.ControlType.CalcEdit
    '                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
    '                                .Value = Val(R.Item(Col).ToString)
    '                            End With
    '                        Case ENM.Control.ControlType.ComboBoxEdit
    '                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
    '                                Try
    '                                    .SelectedIndex = Val(R.Item(Col).ToString)
    '                                Catch ex As Exception
    '                                    .SelectedIndex = -1
    '                                End Try
    '                            End With
    '                        Case ENM.Control.ControlType.CheckEdit
    '                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
    '                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
    '                            End With
    '                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
    '                            Obj.Text = R.Item(Col).ToString
    '                        Case ENM.Control.ControlType.PictureEdit
    '                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
    '                                Try
    '                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
    '                                Catch ex As Exception
    '                                    .Image = Nothing
    '                                End Try
    '                            End With
    '                        Case ENM.Control.ControlType.DateEdit
    '                            Try

    '                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
    '                                    .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
    '                                End With
    '                            Catch ex As Exception
    '                            End Try
    '                        Case Else
    '                            Obj.Text = R.Item(Col).ToString
    '                    End Select
    '                Next
    '            Next

    '            Exit For
    '        Next

    '        Call LoadPoDetail(Key.ToString)
    '        'Call LoadDataTop10Price()
    '        Me.oxtb.SelectedTabPageIndex = 0

    '        _Dt.Dispose()
    '        _ProcLoad = False
    '    End Sub

    '    Private Sub LoadPoDetail(PoKey As String)
    '        Dim Qry As String = ""
    '        Qry = "select A.FNSeq,A.FTDescription,A.FNQuantity,A.FNPrice,A.FNAmount,A.FTNote,A.FNNetAmt,B.FTAssetCode,B.FTAssetNameTH AS FTAssetName,B.FTProductCode,M.FTAssetModelCode AS FTAssetModelName,BR.FTAssetBrandCode AS FTAssetBrandName,U.FTUnitAssetCode as FTUnitCode,U.FNHSysUnitAssetId AS FNHSysUnitId,A.FNHSysFixedAssetId"
    '        Qry &= vbCrLf & "from [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService AS S WITH(NOLOCK)  LEFT OUTER JOIN"
    '        Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "]..TFIXEDTPurchaseService_Detail AS A WItH(NOLOCK)  ON S.FTPurchaseNo=A.FTPurchaseNo LEFT OUTER JOIN"
    '        Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.v_TASMAsset AS B WITH(NOLOCK) ON A.FNHSysFixedAssetId = B.FNHSysFixedAssetId  AND S.FNFixedAssetType=B.FNFixedAssetType LEFT OUTER JOIN"
    '        Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH(NOLOCK) ON B.FNHSysAssetModelId = M.FNHSysAssetModelId LEFT OUTER JOIN"
    '        Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS BR WITH(NOLOCK) ON B.FNHSysAssetBrandId = BR.FNHSysAssetBrandId LEFT OUTER JOIN "
    '        Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON A.FNHSysUnitId = U.FNHSysUnitAssetId  "
    '        Qry &= vbCrLf & "where A.FTPurchaseNo='" & PoKey & "'"

    '        Me.ogcdetail.DataSource = Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)

    '    End Sub



    '    Public Sub DefaultsData()
    '        _FormLoad = True
    '        Dim _FieldName As String
    '        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
    '            For I As Integer = 0 To _FormHeader(cind).DefaultsData.ToArray.Count - 1
    '                _FieldName = _FormHeader(cind).DefaultsData(I).FiledName.ToString

    '                Dim Pass As Boolean = True

    '                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
    '                    Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                        Case ENM.Control.ControlType.ButtonEdit
    '                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString

    '                                HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)

    '                            End With
    '                        Case ENM.Control.ControlType.CalcEdit
    '                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
    '                                .Value = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)

    '                            End With
    '                        Case ENM.Control.ControlType.ComboBoxEdit
    '                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
    '                                .SelectedIndex = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)
    '                            End With
    '                        Case ENM.Control.ControlType.CheckEdit
    '                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
    '                                .Checked = (_FormHeader(cind).DefaultsData(I).DataDefaults.ToString = "1")
    '                            End With
    '                        Case ENM.Control.ControlType.DateEdit
    '                            With CType(Obj, DevExpress.XtraEditors.DateEdit)

    '                                Try
    '                                    .DateTime = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
    '                                Catch ex As Exception
    '                                    .Text = ""
    '                                End Try

    '                            End With

    '                        Case ENM.Control.ControlType.MemoEdit
    '                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
    '                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
    '                            End With
    '                        Case ENM.Control.ControlType.TextEdit
    '                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
    '                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
    '                            End With
    '                        Case Else
    '                    End Select
    '                Next
    '            Next
    '        Next
    '        _FormLoad = False
    '    End Sub

    '    Private Function CheckNotUsed(Key As String) As Boolean
    '        Dim _Str As String = ""
    '        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
    '            For I As Integer = 0 To _FormHeader(cind).CheckDelFiled.ToArray.Count - 1
    '                _Str = _FormHeader(cind).CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

    '                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
    '                    HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
    '                    Return False
    '                End If

    '            Next
    '        Next
    '        Return True
    '    End Function

    '    Private Function VerrifyData() As Boolean
    '        Dim _FieldName As String
    '        Dim _Val As String = ""
    '        Dim _Caption As String = ""
    '        Dim _Str As String = ""
    '        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
    '            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
    '                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
    '                _Caption = ""

    '                For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)
    '                    If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
    '                        _Caption = ObjCaption.Text
    '                        Exit For
    '                    End If
    '                Next

    '                Dim Pass As Boolean = True

    '                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
    '                    Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                        Case ENM.Control.ControlType.ButtonEdit
    '                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                                If .Properties.Buttons.Count <= 1 Then
    '                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
    '                                        Pass = False
    '                                    End If
    '                                End If
    '                            End With
    '                        Case ENM.Control.ControlType.CalcEdit
    '                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
    '                                If Val(.Value.ToString) <= 0 Then
    '                                    Pass = False
    '                                End If
    '                            End With
    '                        Case ENM.Control.ControlType.ComboBoxEdit
    '                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
    '                                If .SelectedIndex < 0 Then Pass = False
    '                            End With
    '                        Case ENM.Control.ControlType.CheckEdit

    '                        Case ENM.Control.ControlType.DateEdit
    '                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
    '                                If HI.UL.ULDate.CheckDate(.Text) = "" Then
    '                                    Pass = False
    '                                End If
    '                            End With
    '                        Case ENM.Control.ControlType.PictureEdit
    '                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
    '                                If .Image Is Nothing Then
    '                                    Pass = False
    '                                End If
    '                            End With
    '                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
    '                            If Obj.Text = "" Then
    '                                Pass = False
    '                            End If
    '                        Case Else
    '                            Pass = False
    '                    End Select

    '                    If Pass = False Then
    '                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
    '                        Obj.Focus()
    '                        Return False
    '                    End If
    '                Next
    '            Next
    '        Next


    '        '---------- Validate Document ---------------------
    '        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
    '            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
    '                Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                    Case ENM.Control.ControlType.ButtonEdit
    '                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                            If .Text.Trim() = "" Then
    '                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
    '                                Obj.Focus()
    '                                Return False
    '                            Else

    '                                Dim _CmpH As String = ""
    '                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

    '                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
    '                                        Case ENM.Control.ControlType.ButtonEdit
    '                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
    '                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                                            End With

    '                                            Exit For
    '                                        Case ENM.Control.ControlType.TextEdit
    '                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
    '                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                                            End With

    '                                            Exit For
    '                                    End Select

    '                                Next

    '                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
    '                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
    '                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

    '                                    If _dt.Rows.Count <= 0 Then
    '                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
    '                                        Obj.Focus()
    '                                        Return False
    '                                    End If
    '                                End If
    '                            End If
    '                        End With

    '                End Select
    '            Next
    '        Next

    '        If FNExchangeRate.Value <= 0 Then
    '            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
    '            FNExchangeRate.Focus()
    '            Return False
    '        End If

    '        Return True
    '    End Function

    '    Private Sub FormRefresh()
    '        _FormLoad = True
    '        HI.TL.HandlerControl.ClearControl(Me)

    '        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
    '            Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                Case ENM.Control.ControlType.ButtonEdit
    '                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                        .Focus()
    '                    End With
    '            End Select
    '        Next
    '        _FormLoad = False
    '    End Sub

    '    Private Function CheckOwner() As Boolean
    '        If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
    '            Return True
    '        Else


    '            Dim _Qry As String = ""
    '            Dim _Qry2 As String = ""
    '            Dim _FNHSysTeamGrpId As Integer = 0
    '            Dim _FNHSysTeamGrpIdTo As Integer = 0

    '            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
    '            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
    '            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTPurchaseBy.Text) & "' "
    '            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

    '            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
    '            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
    '            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
    '            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

    '            If _FNHSysTeamGrpId > 0 Then

    '                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
    '                    Return True
    '                Else
    '                    HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
    '                    Return False
    '                End If

    '            Else

    '                HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
    '                Return False

    '            End If


    '        End If

    '    End Function

    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <returns></returns>
    '    Private Function SaveData() As Boolean
    '        Dim _FieldName As String
    '        Dim _Fields As String = ""
    '        Dim _Values As String = ""
    '        Dim _Str As String
    '        Dim _Key As String = ""
    '        Dim _Val As String = ""
    '        Dim _StateNew As Boolean = False
    '        Dim _CmpH As String = ""
    '        'Dim _StateUpdate As Boolean = False
    '        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString

    '        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
    '            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
    '                Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                    Case ENM.Control.ControlType.ButtonEdit

    '                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                            If .Text.Trim() = "" Then
    '                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
    '                                Obj.Focus()
    '                                Return False
    '                            Else

    '                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

    '                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
    '                                        Case ENM.Control.ControlType.ButtonEdit
    '                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
    '                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                                            End With

    '                                            Exit For
    '                                        Case ENM.Control.ControlType.TextEdit
    '                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
    '                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK) WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                                            End With

    '                                            Exit For
    '                                    End Select

    '                                Next

    '                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
    '                                    _StateNew = True
    '                                Else

    '                                    _Key = .Text

    '                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
    '                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

    '                                    If _dt.Rows.Count <= 0 Then
    '                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
    '                                        Obj.Focus()
    '                                        Return False
    '                                    End If

    '                                End If

    '                            End If
    '                        End With

    '                End Select
    '            Next
    '        Next
    '        If (_StateNew) Then
    '            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
    '        End If
    '        Try

    '            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
    '            HI.Conn.SQLConn.SqlConnectionOpen()
    '            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '            Dim _FoundControl As Boolean
    '            For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
    '                For I As Integer = 0 To _FormHeader(cind).BaseFiled.ToArray.Count - 1
    '                    _FieldName = _FormHeader(cind).BaseFiled(I).FiledName.ToString
    '                    _FoundControl = False
    '                    If (_StateNew) Then

    '                        '------Update -------------
    '                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
    '                            _FoundControl = True
    '                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
    '                                _Val = _Key
    '                            Else
    '                                Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                                    Case ENM.Control.ControlType.ButtonEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                                            _Val = "" & .Properties.Tag.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.CalcEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
    '                                            _Val = .Value.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.ComboBoxEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
    '                                            _Val = .SelectedIndex.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.CheckEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
    '                                            _Val = .EditValue.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.PictureEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
    '                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
    '                                        End With
    '                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
    '                                        _Val = Obj.Text
    '                                    Case Else
    '                                        _Val = Obj.Text
    '                                End Select
    '                            End If
    '                        Next

    '                        If Not (_FoundControl) Then
    '                            Select Case UCase(_FieldName)
    '                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
    '                                    _FoundControl = True
    '                            End Select
    '                        End If

    '                        If _FoundControl Then
    '                            If _Values <> "" Then _Values &= ","
    '                            If _Fields <> "" Then _Fields &= ","

    '                            _Fields &= _FieldName

    '                            Select Case UCase(_FieldName)
    '                                Case UCase("FDInsDate"), UCase("FTInsDate")
    '                                    _Values &= HI.UL.ULDate.FormatDateDB & ""
    '                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
    '                                    _Values &= "''"
    '                                Case UCase("FTInsTime")
    '                                    _Values &= HI.UL.ULDate.FormatTimeDB & ""
    '                                Case UCase("FTInsUser")
    '                                    _Values &= "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                                Case Else
    '                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
    '                                        Case "FC", "FN"
    '                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
    '                                        Case "FD"
    '                                            _Values &= "'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
    '                                        Case Else
    '                                            _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"
    '                                    End Select
    '                            End Select
    '                        End If

    '                    Else

    '                        '------Update -------------
    '                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
    '                            _FoundControl = True
    '                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
    '                                _Val = _Key
    '                            Else

    '                                Select Case HI.ENM.Control.GeTypeControl(Obj)
    '                                    Case ENM.Control.ControlType.ButtonEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                                            _Val = "" & .Properties.Tag.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.CalcEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
    '                                            _Val = .Value.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.ComboBoxEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
    '                                            _Val = .SelectedIndex.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.CheckEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
    '                                            _Val = .EditValue.ToString
    '                                        End With
    '                                    Case ENM.Control.ControlType.PictureEdit
    '                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
    '                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
    '                                        End With
    '                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
    '                                        _Val = Obj.Text
    '                                    Case Else
    '                                        _Val = Obj.Text
    '                                End Select

    '                            End If

    '                        Next

    '                        If Not (_FoundControl) Then
    '                            Select Case UCase(_FieldName)
    '                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
    '                                    _FoundControl = True
    '                            End Select
    '                        End If

    '                        If _FoundControl Then

    '                            Select Case UCase(_FieldName)
    '                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
    '                                Case Else
    '                                    If _Values <> "" Then _Values &= ","
    '                            End Select

    '                            Select Case UCase(_FieldName)
    '                                Case UCase("FDUpdDate"), UCase("FTUpdDate")
    '                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatDateDB & ""
    '                                Case UCase("FTUpdTime")
    '                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatTimeDB & ""
    '                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
    '                                Case UCase("FTUpdUser")
    '                                    _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                                Case Else
    '                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
    '                                        Case "FC", "FN"
    '                                            _Values &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
    '                                        Case "FD"
    '                                            _Values &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
    '                                        Case Else
    '                                            _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
    '                                    End Select
    '                            End Select

    '                        End If
    '                    End If
    '                Next

    '                If (_StateNew) Then
    '                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
    '                Else
    '                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
    '                    '_StateUpdate = True
    '                End If

    '                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '            Next

    '            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
    '                Select Case Obj.GetType.FullName.ToString.ToUpper
    '                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
    '                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
    '                            .Properties.Tag = _Key
    '                            .Text = _Key
    '                        End With
    '                End Select
    '            Next

    '            HI.Conn.SQLConn.Tran.Commit()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '            Return True
    '        Catch ex As Exception
    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False
    '        End Try

    '    End Function
    '#End Region


    '    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
    '        Me.Close()
    '    End Sub

    '    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
    '        Me.FormRefresh()
    '    End Sub

    '    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
    '        If CheckOwner() = False Then Exit Sub

    '        Dim _CmpH As String = ""
    '        Dim _ReviseState As Boolean = False
    '        For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

    '            Select Case HI.ENM.Control.GeTypeControl(ctrl)
    '                Case ENM.Control.ControlType.ButtonEdit
    '                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
    '                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                    End With

    '                    Exit For
    '                Case ENM.Control.ControlType.TextEdit
    '                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
    '                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                    End With

    '                    Exit For
    '            End Select
    '        Next
    '        If FTPurchaseNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then
    '            If Me.VerrifyData() Then
    '                If Me.SaveData() Then
    '                Else
    '                    Exit Sub
    '                End If
    '            Else
    '                Exit Sub
    '            End If
    '        Else
    '            If Me.FTPurchaseNo.Text = "" Then Exit Sub
    '        End If

    '        With _AddItemPopup
    '            .AddComplete = False
    '            HI.TL.HandlerControl.ClearControl(_AddItemPopup)
    '            .ShowDialog()

    '            If (.AddComplete) Then
    '                Dim Qry As String = ""
    '                Dim _FNSeq As String = (HI.Conn.SQLConn.GetField("select max(FNSeq) AS FNSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail WITH(NOLOCK) WHERE FTPurchaseNo='" & UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'", Conn.DB.DataBaseName.DB_FIXED, "0") + 1)

    '                Try
    '                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
    '                    HI.Conn.SQLConn.SqlConnectionOpen()
    '                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
    '                    If .FTAssetCode.Text = "" Then
    '                        Qry = "insert into [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail "
    '                        Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FNSeq, FTDescription, FNQuantity, FNPrice, FNAmount, FTNote)"
    '                        Qry &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
    '                        Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'," & Val(_FNSeq) & ",'" & .FTDescription.Text & "'"
    '                        Qry &= vbCrLf & "," & .FNQuantity.Value & "," & .FNPrice.Value & "," & .FNAmount.Value & ",'" & .FTNote.Text & "'"
    '                    Else
    '                        Qry = "insert into [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail "
    '                        Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FNSeq, FTDescription, FNQuantity, FNPrice, FNAmount, FTNote,FNHSysFixedAssetId,FNHSysUnitId)"
    '                        Qry &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
    '                        Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'," & Val(_FNSeq) & ",'" & .FTDescription.Text & "'"
    '                        Qry &= vbCrLf & "," & .FNQuantity.Value & "," & .FNPrice.Value & "," & .FNAmount.Value & ",'" & .FTNote.Text & "'," & .FTAssetCode.Properties.Tag & "," & .FNHSysUnitAssetId.Properties.Tag & ""
    '                    End If

    '                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                        HI.Conn.SQLConn.Tran.Rollback()
    '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                        MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '                        Exit Sub
    '                    End If
    '                    HI.Conn.SQLConn.Tran.Commit()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    FTStateSendApp.Checked = False
    '                    'FTStateSuperVisorApp.Checked = False
    '                    FTStateManagerApp.Checked = False
    '                    Qry = "select isnull(sum(convert(numeric(18,2),PD.FNQuantity * PD.FNPrice)),0) AS FNNetAmt"
    '                    Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail AS PD WiTH(NOLOCK) where PD.FTPurchaseNo='" & FTPurchaseNo.Text & "'"
    '                    Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "0"))
    '                    Me.SaveData()
    '                    Me.LoadPoDetail(Me.FTPurchaseNo.Text)
    '                Catch ex As Exception
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                End Try
    '            End If
    '        End With
    '    End Sub

    '    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
    '        Dim _Qry As String = ""
    '        If CheckOwner() = False Then Exit Sub
    '        If Me.FTPurchaseNo.Text <> "" Then

    '            If VerrifyData() Then
    '                If Me.SaveData() Then
    '                    Me.LoadDataInfo(Me.FTPurchaseNo.Text)
    '                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '                Else
    '                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '                End If
    '            End If
    '        End If

    '    End Sub

    '    Private Sub FNHSysCmpId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpId.EditValueChanged
    '        Dim joke As String = Me.FNHSysCmpId.Text
    '    End Sub

    '    Private Sub FNHSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCurId.EditValueChanged
    '        If _FormLoad Then Exit Sub
    '        If FNHSysCurId.Text = "" Then
    '            FNExchangeRate.Value = 0
    '            Exit Sub
    '        End If
    '        If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

    '            FNExchangeRate.Properties.ReadOnly = True

    '            If Not (_ProcLoad) Then
    '                FNExchangeRate.Value = 1
    '            End If

    '        Else
    '            FNExchangeRate.Properties.ReadOnly = True
    '            If Not (_ProcLoad) Then
    '                FNExchangeRate.Value = 0
    '                Dim _Qry As String = ""

    '                _Qry = " SELECT TOP 1 FNBuyingRate"
    '                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
    '                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDPurchaseDate.Text) & "')"
    '                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
    '                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
    '                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
    '                _Qry &= vbCrLf & "  WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "'"
    '                _Qry &= vbCrLf & "  ))"

    '                FNExchangeRate.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

    '                If FNExchangeRate.Value <= 0 Then
    '                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
    '                End If

    '            End If

    '        End If
    '    End Sub

    '    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
    '                                                                                  FNPoAmt.EditValueChanged,
    '                                                                                  FNDisCountAmt.EditValueChanged,
    '                                                                                  FNVatPer.EditValueChanged,
    '                                                                                  FNVatAmt.EditValueChanged,
    '                                                                                  FNSurcharge.EditValueChanged,
    '                                                                                  FNTaxPer.EditValueChanged,
    '                                                                                  FNAmt.EditValueChanged

    '        Static _Proc As Boolean

    '        If Not (_Proc) And Not (_ProcLoad) Then
    '            _Proc = True
    '            Dim _POAmt As Double = FNPoAmt.Value

    '            If _POAmt = 0 Then
    '                FNDisCountAmt.Value = 0
    '                FNVatAmt.Value = 0
    '            End If

    '            Dim _DisPer As Double = FNDisCountPer.Value
    '            Dim _DisAmt As Double = FNDisCountAmt.Value
    '            Dim _VatPer As Double = FNVatPer.Value
    '            Dim _VatAmt As Double = FNVatAmt.Value
    '            Dim _SurAmt As Double = FNSurcharge.Value
    '            Dim _TaxAmt As Double = FNVatAmt.Value
    '            Dim _TaxPer As Double = FNVatAmt.Value
    '            Select Case sender.Name.ToString.ToUpper
    '                Case "FNDisCountPer".ToUpper
    '                    _DisPer = FNDisCountPer.Value
    '                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
    '                    FNDisCountAmt.Value = _DisAmt
    '                Case "FNDisCountAmt".ToUpper
    '                    _DisAmt = FNDisCountAmt.Value

    '                    If _POAmt > 0 Then
    '                        _DisPer = Format((_DisAmt * 100) / _POAmt, HI.ST.Config.PercentFormat)
    '                    Else
    '                        _DisPer = 0
    '                    End If
    '                    FNDisCountPer.Value = _DisPer
    '                Case "FNVatPer".ToUpper
    '                    _VatPer = FNVatPer.Value
    '                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
    '                    FNVatAmt.Value = _VatAmt
    '                Case "FNVatAmt".ToUpper
    '                    _VatAmt = FNVatAmt.Value

    '                    If (_POAmt - _DisAmt) > 0 Then
    '                        _VatPer = Format((_VatAmt * 100) / (_POAmt - _DisAmt), HI.ST.Config.PercentFormat)
    '                    Else
    '                        _VatPer = 0
    '                    End If
    '                    FNVatPer.Value = _VatPer
    '                Case Else
    '                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
    '                    FNDisCountAmt.Value = _DisAmt
    '                    _TaxPer = FNTaxPer.Value
    '                    _TaxAmt = Format(((_POAmt - _DisAmt) * _TaxPer) / 100, HI.ST.Config.AmtFormat)
    '                    FNAmt.Value = (_POAmt - _DisAmt)
    '                    FNTaxAmt.Value = _TaxAmt
    '                    _VatPer = FNVatPer.Value
    '                    _VatAmt = Format((((_POAmt - _DisAmt) - _TaxAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
    '                    FNVatAmt.Value = _VatAmt
    '            End Select

    '            Me.FNPONetAmt.Value = (_POAmt - _DisAmt) - _TaxAmt

    '            Select Case sender.Name.ToString.ToUpper
    '                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
    '                    _VatPer = FNVatPer.Value
    '                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
    '                    FNVatAmt.Value = _VatAmt
    '            End Select

    '            FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)
    '            Me.FTPOGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNPOGrandAmt.Value)
    '            Me.FTPOGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNPOGrandAmt.Value)

    '            _Proc = False
    '        End If
    '    End Sub

    '    Private Sub wPurchaserAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
    '        Try
    '            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    '            _FormLoad = False
    '        Catch ex As Exception
    '        End Try
    '    End Sub

    '    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
    '        If FTPurchaseNo.Text = "" Then Exit Sub
    '        If CheckOwner() = False Then Exit Sub
    '        'If CheckReceive(Me.FTPurchaseNo.Text) Then
    '        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTPurchaseNo.Text, Me.Text) = True Then
    '            If Me.DeleteData() Then
    '                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
    '                HI.TL.HandlerControl.ClearControl(Me)
    '                Me.DefaultsData()
    '                Me.FTPurchaseNo.Focus()
    '            Else
    '                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
    '            End If
    '        End If
    '        'End If

    '    End Sub

    '    Private Function DeleteData() As Boolean
    '        Try
    '            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
    '            HI.Conn.SQLConn.SqlConnectionOpen()
    '            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '            Dim _Str As String
    '            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
    '            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If

    '            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
    '            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

    '            HI.Conn.SQLConn.Tran.Commit()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail "
    '            _Str &= vbCrLf & "  SET FTPurchaseRefNo='' WHERE FTPurchaseRefNo = '" & FTPurchaseNo.Text & "' "
    '            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_FIXED)

    '            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'")
    '            Return True

    '        Catch ex As Exception
    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False
    '        End Try

    '    End Function

    '    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
    '        If ogvdetail.RowCount = 0 Or ogvdetail.FocusedRowHandle <= -1 Then Exit Sub

    '        Dim _FNSeq As Integer = 0
    '        Dim Qry As String = ""
    '        Dim _id As Integer = 0
    '        With _AddItemPopup
    '            .AddComplete = False
    '            TL.HandlerControl.ClearControl(_AddItemPopup)
    '            Try
    '                _FNSeq = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNSeq").ToString)
    '                .FNPrice.Value = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNPrice").ToString)
    '                .FTDescription.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTDescription").ToString
    '                .FTNote.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTNote").ToString
    '                .FNQuantity.Value = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNQuantity").ToString)
    '                .FNAmount.Value = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNAmount").ToString)
    '                .FTAssetCode.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetCode").ToString
    '                .FTAssetCode.Properties.Tag = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysFixedAssetId"))
    '                .FNHSysFixedAssetId_None.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetName").ToString
    '                .FNHSysUnitAssetId.Properties.Tag = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysUnitId"))
    '                .FNHSysUnitAssetId.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTUnitCode").ToString

    '            Catch ex As Exception

    '            End Try
    '            .ShowDialog()
    '            If .AddComplete Then
    '                Try

    '                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
    '                    HI.Conn.SQLConn.SqlConnectionOpen()
    '                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '                    Qry = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail "
    '                    Qry &= vbCrLf & "set FTUpdUser='" & ST.UserInfo.UserName & "', FDUpdDate=" & UL.ULDate.FormatDateDB & ", FTUpdTime=" & UL.ULDate.FormatTimeDB & ""
    '                    Qry &= vbCrLf & ",FNPrice=" & .FNPrice.Value & ",FTDescription='" & .FTDescription.Text & "',FTNote='" & .FTNote.Text & "',FNQuantity=" & .FNQuantity.Value & " ,FNAmount=" & .FNAmount.Value & ""
    '                    Qry &= vbCrLf & ",FNHSysFixedAssetId=" & .FTAssetCode.Properties.Tag & ",FNHSysUnitId=" & .FNHSysUnitAssetId.Properties.Tag & ""
    '                    Qry &= vbCrLf & "where FTPurchaseNo='" & FTPurchaseNo.Text & "' and FNSeq=" & _FNSeq & ""

    '                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                        HI.Conn.SQLConn.Tran.Rollback()
    '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    End If
    '                    HI.Conn.SQLConn.Tran.Commit()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '                    Qry = "select isnull(sum(convert(numeric(18,2),PD.FNQuantity * PD.FNPrice)),0) AS FNNetAmt"
    '                    Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail AS PD WiTH(NOLOCK) where PD.FTPurchaseNo='" & FTPurchaseNo.Text & "'"
    '                    Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "0"))
    '                    Me.SaveData()
    '                    Me.LoadPoDetail(Me.FTPurchaseNo.Text)

    '                Catch ex As Exception
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                End Try
    '            End If

    '        End With

    '    End Sub

    '    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
    '        If CheckOwner() = False Then Exit Sub
    '        Dim Qry As String = ""
    '        Dim _FNSeq As Integer = 0
    '        Dim _IDAsset As Integer = 0
    '        Try
    '            With ogvdetail
    '                _FNSeq = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString)
    '            End With

    '            Qry = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail where FTPurchaseNo='" & FTPurchaseNo.Text & "' and FNSeq=" & _FNSeq & ""
    '            HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_FIXED)


    '            If HI.Conn.SQLConn.ExecuteOnly(Qry, _DBEnum) = True Then

    '                Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, Qry)

    '                Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
    '                Qry &= vbCrLf & "  SET FTStateSendApp='0' "
    '                Qry &= vbCrLf & "  ,FTSendAppDate='' "
    '                Qry &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
    '                Qry &= vbCrLf & "  ,FTSuperVisorName='' "
    '                Qry &= vbCrLf & "  ,FTStateManagerApp='0' "
    '                Qry &= vbCrLf & "  ,FTSuperManagerName='' "
    '                Qry &= vbCrLf & "  ,FTStatePDF='0' "
    '                Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
    '                Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_FIXED)

    '                FTStateSendApp.Checked = False
    '                'FTStateSuperVisorApp.Checked = False
    '                FTStateManagerApp.Checked = False

    '                Qry = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice ))) AS NETAMT"
    '                Qry &= vbCrLf & "    FROM"
    '                Qry &= vbCrLf & " ("
    '                Qry &= vbCrLf & " SELECT   FNPrice,  FNQuantity"
    '                Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TFIXEDTPurchaseService_Detail AS A  WITH(NOLOCK)"
    '                Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
    '                Qry &= vbCrLf & " ) AS A"

    '                Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(Qry, _DBEnum, "0"))

    '                Me.SaveData()
    '                Me.LoadPoDetail(Me.FTPurchaseNo.Text)
    '            End If
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
    '        If CheckOwner() = False Then Exit Sub
    '        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then
    '            Dim _Qry As String = ""
    '            _Qry = "Select  TOP  1  FTStateSendApp  "
    '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService AS A WITH(NOLOCK)"
    '            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTStateSuperVisorApp<>'2' AND FTStateManagerApp<>'2' "

    '            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FIXED, "") <> "1" Then

    '                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
    '                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
    '                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
    '                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
    '                _Qry &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
    '                _Qry &= vbCrLf & "  ,FTSuperVisorName='' "
    '                _Qry &= vbCrLf & "  ,FTStateManagerApp='0' "
    '                _Qry &= vbCrLf & "  ,FTStatePDF='0' "
    '                _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

    '                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_FIXED)

    '            End If
    '            FTStateSendApp.Checked = True
    '        Else
    '            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
    '            FTPurchaseNo.Focus()
    '        End If
    '    End Sub

    '    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
    '        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then
    '            With New HI.RP.Report

    '                'Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language


    '                .FormTitle = Me.Text
    '                .ReportFolderName = "PurchaseAsset\"
    '                .ReportName = "PurchaseServiceAsset.rpt"

    '                .Formular = "{TFIXEDTPurchaseService.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
    '                .Preview()

    '                'HI.ST.Lang.Language = _tmplang
    '            End With
    '        Else
    '            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
    '            FTPurchaseNo.Focus()
    '        End If
    '    End Sub

    '    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
    '        Call LoadDataInfo(Me.FTPurchaseNo.Text)
    '    End Sub

    '    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged
    '        If FNHSysSuplId.Text <> "" Then
    '            FNTaxPer.Value = 3
    '        End If

    '    End Sub

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

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub LoadPopup()
        Call PrepareForm()
        _ListPop = New wListAutoPurchaseAssetNo
        'TL.HandlerControl.AddHandlerObj(_ListPop)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListPop.Name.ToString.Trim, _ListPop)
        Catch ex As Exception
        Finally
        End Try
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
        If _PRState = 0 Then
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
            ' Dim _type As String = HI.Conn.SQLConn.GetField("Select L.FTReferCode from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH = '" & Me.FNFixedAssetType.Text & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
            If _PRState <> 0 Then
                '_Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & FNHSysCmpRunId.Text & _Year & "ASSET" & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
               
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString()
            End If

            FTPurchaseState.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & " AUTO " & HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)) & " " & Format(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM), "HH:mm:ss")
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
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
                    '_StateUpdate = True
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
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            Dim _Str As String = ""
            Dim _Type As String = ""


            For Each R As DataRow In _oDtRef.Rows


                For Each x As DataRow In _oDt.Select("FTAssetCode='" & (R!FTAssetCode.ToString) & "'")

                    If ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH ='" & x!FNFixedAssetType.ToString & "'" 'Edit by joker 2017/06/30 from select top 1 To select max
                    Else
                        _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameEN ='" & x!FNFixedAssetType.ToString & "'"
                    End If
                    _Type = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")


                    _Price = Double.Parse(x!FNPrice.ToString)
                    _DisPer = Double.Parse(x!FNDisPer.ToString)
                    _FNHSysUnitId = Val(x!FNHSysUnitId.ToString)
                    _FNDisAmt = Double.Parse(x!FNDisAmt.ToString)
                    _NetAmt = Double.Parse(R!FNQuantity.ToString) * _Price
                    _FTDescription = R!FTDescription.ToString
                    _FNSeq += 1
                    If _PRState = 0 Then
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
                        _Cmd &= vbCrLf & ",FNFixedAssetType=" & _Type & ""
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
                        If _PRState = 0 Then
                            _Cmd = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail(FTInsUser, FDInsDate, FTInsTime"
                            _Cmd &= vbCrLf & " , FTPurchaseNo,FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, "
                            _Cmd &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark,FNFixedAssetType)"
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
                            _Cmd &= vbCrLf & ",'" & _Type & "'"

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

                    If _PRState <> 0 Then
                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Cmd &= vbCrLf & " Set FNFixedAssetType='" & _Type & "'"
                        _Cmd &= vbCrLf & " Where FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

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
        ' If _FormLoad Then Exit Sub
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

    'Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
    '                                                                              FNPoAmt.EditValueChanged,
    '                                                                              FNDisCountAmt.EditValueChanged,
    '                                                                              FNVatPer.EditValueChanged,
    '                                                                              FNVatAmt.EditValueChanged,
    '                                                                              FNSurcharge.EditValueChanged

    '    Static _Proc As Boolean

    '    If Not (_Proc) And Not (_ProcLoad) Then
    '        _Proc = True
    '        Dim _POAmt As Double = FNPoAmt.Value

    '        If _POAmt = 0 Then
    '            FNDisCountAmt.Value = 0
    '            FNVatAmt.Value = 0
    '        End If

    '        Dim _DisPer As Double = FNDisCountPer.Value
    '        Dim _DisAmt As Double = FNDisCountAmt.Value
    '        Dim _VatPer As Double = FNVatPer.Value
    '        Dim _VatAmt As Double = FNVatAmt.Value
    '        Dim _SurAmt As Double = FNSurcharge.Value

    '        Select Case sender.Name.ToString.ToUpper
    '            Case "FNDisCountPer".ToUpper
    '                _DisPer = FNDisCountPer.Value
    '                _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
    '                FNDisCountAmt.Value = _DisAmt
    '            Case "FNDisCountAmt".ToUpper
    '                _DisAmt = FNDisCountAmt.Value

    '                If _POAmt > 0 Then
    '                    _DisPer = Format((_DisAmt * 100) / _POAmt, HI.ST.Config.PercentFormat)
    '                Else
    '                    _DisPer = 0
    '                End If
    '                FNDisCountPer.Value = _DisPer
    '            Case "FNVatPer".ToUpper
    '                _VatPer = FNVatPer.Value
    '                _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
    '                FNVatAmt.Value = _VatAmt
    '            Case "FNVatAmt".ToUpper
    '                _VatAmt = FNVatAmt.Value

    '                If (_POAmt - _DisAmt) > 0 Then
    '                    _VatPer = Format((_VatAmt * 100) / (_POAmt - _DisAmt), HI.ST.Config.PercentFormat)
    '                Else
    '                    _VatPer = 0
    '                End If
    '                FNVatPer.Value = _VatPer
    '            Case Else
    '                _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
    '                FNDisCountAmt.Value = _DisAmt

    '                _VatPer = FNVatPer.Value
    '                _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
    '                FNVatAmt.Value = _VatAmt
    '        End Select

    '        Me.FNPONetAmt.Value = (_POAmt - _DisAmt)

    '        Select Case sender.Name.ToString.ToUpper
    '            Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
    '                _VatPer = FNVatPer.Value
    '                _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
    '                FNVatAmt.Value = _VatAmt
    '        End Select

    '        FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)

    '        _Proc = False
    '    End If
    'End Sub

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
            Dim _FNAmt As Double = FNAmt.Value

            If _FNAmt = 0 Then
                FNTaxAmt.Value = 0
                FNVatAmt.Value = 0
            End If

            Dim _DisPer As Double = FNDisCountPer.Value
            Dim _DisAmt As Double = FNDisCountAmt.Value
            Dim _TaxPer As Double = FNTaxPer.Value
            Dim _TaxAmt As Double = FNTaxAmt.Value
            Dim _VatPer As Double = FNVatPer.Value
            Dim _VatAmt As Double = FNVatAmt.Value
            Dim _SurAmt As Double = FNSurcharge.Value

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
                Case "FNTaxPer".ToUpper
                    _TaxPer = FNTaxPer.Value
                    _TaxAmt = Format((_FNAmt * _TaxPer) / 100, HI.ST.Config.AmtFormat)
                    FNTaxAmt.Value = _TaxAmt
                Case "FNTaxAmt".ToUpper
                    _TaxAmt = FNTaxAmt.Value

                    If _POAmt > 0 Then
                        _TaxPer = Format((_TaxAmt * 100) / _FNAmt, HI.ST.Config.PercentFormat)
                    Else
                        _TaxPer = 0
                    End If
                    FNTaxPer.Value = _TaxPer
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
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt

                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select
            Me.FNAmt.Value = (_POAmt - _DisAmt)
            'Me.FNPONetAmt.Value = (_FNAmt - _TaxAmt)

            If Me.FNDisCountPer.Text = "0.00000" Then
                Me.FNPONetAmt.Value = (_POAmt - _TaxAmt)
            Else
                Me.FNPONetAmt.Value = (_FNAmt - _TaxAmt)
            End If


            FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)
            Me.FTPOGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNPOGrandAmt.Value)
            Me.FTPOGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNPOGrandAmt.Value)
            _Proc = False
        End If
    End Sub
    Private Sub wPurchaserByPRPopupAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            '_FormLoad = False
            'Me.FDPurchaseDate.Text = HI.ST.UserInfo.LogINDate
            'Me.FTPurchaseBy.Text = HI.ST.UserInfo.UserName
            'Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
            _FormLoad = False
            Call LoadPopup()
            Call Calculate(FNPrice, New System.EventArgs)
            Me.FDPurchaseDate.Text = HI.ST.UserInfo.LogINDate
            Me.FTPurchaseBy.Text = HI.ST.UserInfo.UserName
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
            Call SumPO()

            If Me.FNHSysCurId.Enabled Then
                FNHSysCurId_EditValueChanged(Me.FNHSysCurId, New System.EventArgs)
            End If
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
                        ogcdetail.DataSource = Nothing
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


    'Private Sub RepFNPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNPrice.EditValueChanging
    '    Try
    '        With Me.ogvdetail
    '            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
    '            Dim _NetAmt As Double = 0
    '            Dim _DisPer As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNDisPer"))
    '            Dim _DisAmt As Double = 0

    '            If _DisPer > 0 Then
    '                .SetRowCellValue(.FocusedRowHandle, "FNDisAmt", CDbl(Format((e.NewValue * _DisPer) / 100, HI.ST.Config.PriceFormat)))
    '            Else
    '                .SetRowCellValue(.FocusedRowHandle, "FNDisAmt", 0.0)
    '            End If

    '            _NetAmt = Format((Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) * (Double.Parse(e.NewValue) - Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNDisAmt")))), HI.ST.Config.AmtFormat)
    '            .SetRowCellValue(.FocusedRowHandle, "FNNetAmt", _NetAmt)

    '            'Dim _GndAmt As Double = 0
    '            '_GndAmt = Format(_NetAmt + Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNSurchangeAmt")), HI.ST.Config.AmtFormat)
    '            .SetRowCellValue(.FocusedRowHandle, "FNGrandNetAmt", _NetAmt)

    '            Call SumPO()
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub





    Private Sub SumPO()
        Try
            Static _Proc As Boolean

            If Not (_Proc) Then
                _Proc = True
                Dim _oDt As DataTable
                With CType(Me.ogcdetail.DataSource, DataTable)
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

            If _PRState = 0 Then
                FNTaxPer.Value = 0
            Else
                FNTaxPer.Value = 3
            End If

            Dim _Str As String = ""
            _Str = " SELECT TOP 1 s.FNTax"
            _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier as s WITH(NOLOCK)  "
            _Str &= vbCrLf & "   WHERE  s.FTSuplCode = '" & Me.FNHSysSuplId.Text & "'"
            FNVatPer.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "0")))

        Catch ex As Exception
        End Try
    End Sub
    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged
        If _PRState = 0 Then
            FNTaxPer.Value = 0
        Else
            FNTaxPer.Value = 3
        End If
        Call Calculate(FNPrice, New System.EventArgs)
    End Sub
End Class
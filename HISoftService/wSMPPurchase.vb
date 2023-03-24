Imports System.Windows.Forms
Imports HI
Public Class wSMPPurchase

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_SAMPLE
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItem As wSMPPurchaseAddItem

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        Call InitGridControl()


        _AddItem = New wSMPPurchaseAddItem
        Dim _SysLang As New HI.ST.SysLanguage

        HI.TL.HandlerControl.AddHandlerObj(_AddItem)

        Try
            Call _SysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItem.Name.ToString.Trim, _AddItem)
        Catch ex As Exception
        Finally
        End Try


    End Sub

#Region "Grid"
    Private Sub InitGridControl()

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n4}"
            .Columns("FNAmount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount")
            .Columns("FNAmount").SummaryItem.DisplayFormat = "{0:n2}"
            .OptionsView.ShowFooter = True
        End With

        With ogvsum
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n4}"
            .Columns("FNAmount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount")
            .Columns("FNAmount").SummaryItem.DisplayFormat = "{0:n2}"
            .OptionsView.ShowFooter = True
        End With

    End Sub
#End Region
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
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            _SortField = _dt.Rows(0)!FTSortField.ToString

            _Str = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
            _Str &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SYSTEM)


            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SYSTEM)
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

        _FormLoad = True
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

        'Me.FNHSysCurId.Text = ""
        'Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Me.FNExchangeRate.Value = 1
        oxtb.SelectedTabPageIndex = 0
        Call LoadDetail(Key.ToString)

        _ProcLoad = False
        _FormLoad = False
    End Sub

    'Private Function CheckOwner() As Boolean
    '    If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
    '        Return True
    '    Else
    '        HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
    '        Return False
    '    End If
    'End Function

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTPurchaseBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If


        End If

    End Function

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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString


        'Me.FNHSysCurId.Text = ""
        'Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Me.FNExchangeRate.Value = 1

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

        If FNExchangeRate.Value <= 0 Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
            Return False
        End If

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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


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
        oxtb.SelectedTabPageIndex = 0
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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString

        oxtb.SelectedTabPageIndex = 0

        _FormLoad = False

    End Sub

#End Region

#Region "MAIN PROC"

    Private Function CheckReceive(POKey As String) As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""


        _Str = "Select TOP 1 FTReceiveNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceive As R WITH(NOLOCK) WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "

        If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SAMPLE, "") <> "" Then

            HI.MG.ShowMsg.mProcessError(1303150331, "พบข้อมูลการรับแล้ว ไม่สามารถทำการลบหรือแก้ไขรายการได้ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)


            _Pass = False
        End If



        Return _Pass
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        ' If CheckOwner() = False Then Exit Sub

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If

        If Me.VerrifyData Then
            If Me.SaveData() Then




                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTPurchaseNo.Text, Me.Text) = False Then
            Exit Sub
        End If

        If Me.DeleteData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.DefaultsData()
            Me.FormRefresh()
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTPurchaseNo.Text <> "" Then

            'With New HI.RP.Report
            '    .FormTitle = Me.Text
            '    .ReportFolderName = "PurchaseOrder\"
            '    .ReportName = "PurchaseService.rpt"
            '    .Formular = "{TSMPPurchase.FTPurchaseNo} ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
            '    .Preview()
            'End With

            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseSample.rpt"
                .Formular = "{TSMPPurchase.FTPurchaseNo} ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

#End Region

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString

        _FormLoad = False

    End Sub

    Private Sub LoadDetail(ByVal _DocRefNo As String)
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable

            _Qry = " SELECT  P.FNSeq, P.FTSMPOrderNo, P.FTMatCode,P.FTMatColorCode,P.FTMatSizeCode,P.FTDescription, P.FNQuantity, P.FNPrice, P.FNAmount,P.FTNote,ISNULL(U.FTUnitCode,'') AS FTUnitCode"
            _Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail AS P WITH(NOLOCK) "
            _Qry &= vbCrLf & "	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON P.FNHSysUnitId=U.FNHSysUnitId "
            _Qry &= vbCrLf & "	 WHERE P.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'"
            _Qry &= vbCrLf & "	ORDER BY P.FNSeq "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcdetail.DataSource = _oDt.Copy


            _Qry = "SELECT ROW_NUMBER() Over (Order By A.FNSeq) AS FNSeq , FTMatCode,FTMatColorCode,FTMatSizeCode,FTDescription,FNQuantity,FNPrice,Convert(numeric(18,2),FNQuantity * FNPrice ) AS FNAmount"
            _Qry &= vbCrLf & " FROM ( SELECT  MIN(P.FNSeq) AS FNSeq,  P.FTMatCode,P.FTMatColorCode,P.FTMatSizeCode,MAX(P.FTDescription) AS FTDescription, SUM(P.FNQuantity) AS FNQuantity, MAX(P.FNPrice) AS FNPrice ,MAX(P.FTNote) AS FTNote,MAX(ISNULL(U.FTUnitCode,'')) AS FTUnitCode"
            _Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail AS P WITH(NOLOCK) "
            _Qry &= vbCrLf & "	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON P.FNHSysUnitId=U.FNHSysUnitId "
            _Qry &= vbCrLf & "	 WHERE P.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'"
            _Qry &= vbCrLf & "	GROUP BY  P.FTMatCode,P.FTMatColorCode,P.FTMatSizeCode  )AS A "
            _Qry &= vbCrLf & "	ORDER BY ROW_NUMBER() Over (Order By A.FNSeq)  "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcsum.DataSource = _oDt.Copy


            _oDt.Dispose()

            oxtb.SelectedTabPageIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                    FNDisCountAmt.EditValueChanged,
                                                                                    FNVatPer.EditValueChanged,
                                                                                    FNVatAmt.EditValueChanged,
                                                                                    FNSurcharge.EditValueChanged, FNPoAmt.EditValueChanged

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
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt

                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            Me.FNPONetAmt.Value = (_POAmt - _DisAmt)

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)

            _Proc = False
        End If
    End Sub

    Private Sub FNInvGrandAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNPOGrandAmt.EditValueChanged
        Try
            If Not (_ProcLoad) Then
                Me.FTPOGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNPOGrandAmt.Value)
                Me.FTPOGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNPOGrandAmt.Value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged

        'Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Me.FNExchangeRate.Value = 1

    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As Object, e As EventArgs) Handles FNExchangeRate.EditValueChanged
        'Try
        '    If FNExchangeRate.Value <> 1 Then
        '        FNExchangeRate.Value = 1
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmAddDT.Click
        If CheckOwner() = False Then Exit Sub

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If

        If CheckOwner() = False Then Exit Sub

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If
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


        If FTPurchaseNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTPurchaseNo.Text = "" Then Exit Sub

            If Me.SaveData() Then
            Else
                Exit Sub
            End If

        End If

        With _AddItem
            .AddMat = False
            .PONO = FTPurchaseNo.Text
            .FNSeq = 0
            Call HI.ST.Lang.SP_SETxLanguage(_AddItem)
            HI.TL.HandlerControl.ClearControl(_AddItem)

            .FTSMPOrderNo.Properties.ReadOnly = False
            .FTSMPOrderNo.Properties.Buttons(0).Enabled = True
            .ShowDialog()

            If (.AddMat) Then
                Dim _Str As String = ""
                Dim _FNSeq As Integer = 0

                _Str = "SELECT TOP 1 FNSeq +1  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail WITH(NOLOCK) "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                _Str &= vbCrLf & " ORDER BY FNSeq DESC  "

                _FNSeq = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SAMPLE, "1")))

                '_Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail(FTInsUser, FDInsDate, FTInsTime"
                '_Str &= vbCrLf & " , FTPurchaseNo, FNSeq, FTSMPOrderNo,FTMatCode,FTMatColorCode,FTMatSizeCode, FTDescription,FNHSysUnitId, FNQuantity, FNPrice, FNAmount, FTNote)"
                '_Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                '_Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                '_Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                '_Str &= vbCrLf & "," & _FNSeq & " "
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPOrderNo.Text) & "' "
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatCode.Text.Trim) & "' "
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatColorCode.Text.Trim) & "' "
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatSizeCode.Text.Trim) & "' "
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatDescription.Text.Trim) & "' "
                '_Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                '_Str &= vbCrLf & "," & .FNQuantity.Value & " "
                '_Str &= vbCrLf & "," & .FNPrice.Value & " "
                '_Str &= vbCrLf & "," & .FNAmount.Value & " "
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTNote.Text.Trim) & "' "

                Try

                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ,FNPrice=" & .FNPrice.Value & ""
                    _Str &= vbCrLf & " ,FNQuantity=" & .FNQuantity.Value & ""
                    _Str &= vbCrLf & " ,FNAmount=" & .FNAmount.Value & ""
                    _Str &= vbCrLf & " ,FTNote=N'" & HI.UL.ULF.rpQuoted(.FTNote.Text.Trim) & "' "
                    _Str &= vbCrLf & " ,FTDescription=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatDescription.Text.Trim) & "' "
                    _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & "  AND FTSMPOrderNo=N'" & HI.UL.ULF.rpQuoted(.FTSMPOrderNo.Text.Trim) & "' "
                    _Str &= vbCrLf & "  AND FTMatCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatCode.Text.Trim) & "' "
                    _Str &= vbCrLf & "  AND FTMatColorCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatColorCode.Text.Trim) & "' "
                    _Str &= vbCrLf & "  AND FTMatSizeCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatSizeCode.Text.Trim) & "' "


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail(FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & " , FTPurchaseNo, FNSeq, FTSMPOrderNo,FTMatCode,FTMatColorCode,FTMatSizeCode, FTDescription,FNHSysUnitId, FNQuantity, FNPrice, FNAmount, FTNote)"
                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & "," & _FNSeq & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPOrderNo.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatCode.Text.Trim) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatColorCode.Text.Trim) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatSizeCode.Text.Trim) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatDescription.Text.Trim) & "' "
                        _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & "," & .FNQuantity.Value & " "
                        _Str &= vbCrLf & "," & .FNPrice.Value & " "
                        _Str &= vbCrLf & "," & .FNAmount.Value & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTNote.Text.Trim) & "' "


                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Exit Sub
                        End If
                    End If



                    _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail SET  "
                    _Str &= vbCrLf & " FNPrice=" & .FNPrice.Value & ""
                    _Str &= vbCrLf & " ,FNAmount= Convert(numeric(18,2),FNQuantity * " & .FNPrice.Value & ")"
                    _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & "  AND FTSMPOrderNo <> N'" & HI.UL.ULF.rpQuoted(.FTSMPOrderNo.Text.Trim) & "' "
                    _Str &= vbCrLf & "  AND FTMatCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatCode.Text.Trim) & "' "
                    _Str &= vbCrLf & "  AND FTMatColorCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatColorCode.Text.Trim) & "' "
                    _Str &= vbCrLf & "  AND FTMatSizeCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatSizeCode.Text.Trim) & "' "
                    _Str &= vbCrLf & " AND FNPrice<>" & .FNPrice.Value & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If

                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase "
                    _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                    _Str &= vbCrLf & "  ,FTSendAppDate='' "
                    _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                    _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                    _Str &= vbCrLf & "  ,FTStatePDF='0' "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    FTStateSendApp.Checked = False
                    FTStateSuperVisorApp.Checked = False
                    FTStateManagerApp.Checked = False

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice ))) AS NETAMT"
                    _Str &= vbCrLf & "    FROM"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " SELECT       FTMatCode,FTMatColorCode,FTMatSizeCode,  FNPrice, SUM(FNQuantity) AS  FNQuantity"
                    _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail AS A  WITH(NOLOCK)"
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " GROUP BY  FTMatCode,FTMatColorCode,FTMatSizeCode,  FNPrice "
                    _Str &= vbCrLf & " ) AS A"

                    Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                    Me.SaveData()
                    Call LoadDetail(Me.FTPurchaseNo.Text)
                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                End Try
            End If
        End With

        oxtb.SelectedTabPageIndex = 0
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmRemoveDT.Click
        If CheckOwner() = False Then Exit Sub

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If
        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _FNSeq As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString


            Dim _Str As String = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FNSeq=" & Val(_FNSeq) & " "

            If HI.Conn.SQLConn.ExecuteOnly(_Str, _DBEnum) = True Then


                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase "
                _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                _Str &= vbCrLf & "  ,FTSendAppDate='' "
                _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                _Str &= vbCrLf & "  ,FTStatePDF='0' "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)

                FTStateSendApp.Checked = False
                FTStateSuperVisorApp.Checked = False
                FTStateManagerApp.Checked = False


                '_Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice ))) AS NETAMT"
                '_Str &= vbCrLf & "    FROM"
                '_Str &= vbCrLf & " ("
                '_Str &= vbCrLf & " SELECT        FTPurchaseNo,  FNPrice,  FNQuantity"
                '_Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail AS A  WITH(NOLOCK)"
                '_Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                '_Str &= vbCrLf & " ) AS A "

                'Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice ))) AS NETAMT"
                _Str &= vbCrLf & "    FROM"
                _Str &= vbCrLf & " ("
                _Str &= vbCrLf & " SELECT       FTMatCode,FTMatColorCode,FTMatSizeCode,  FNPrice, SUM(FNQuantity) AS  FNQuantity"
                _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail AS A  WITH(NOLOCK)"
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                _Str &= vbCrLf & " GROUP BY  FTMatCode,FTMatColorCode,FTMatSizeCode,  FNPrice "
                _Str &= vbCrLf & " ) AS A"


                Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                Me.SaveData()
                Me.LoadDetail(Me.FTPurchaseNo.Text)

            End If

        End With

    End Sub


    Private Sub FNHSysCurId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCurId.EditValueChanged

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
                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDPurchaseDate.Text) & "')"
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

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then

            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateSendApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTStateSuperVisorApp<>'2' AND FTStateManagerApp<>'2' "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SAMPLE, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                _Qry &= vbCrLf & "  ,FTSuperVisorName='' "
                _Qry &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Qry &= vbCrLf & "  ,FTSuperManagerName='' "
                _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

            End If
            FTStateSendApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub oxtb_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles oxtb.SelectedPageChanging
        ocmAddDT.Visible = (e.Page.Name = otpdetailitem.Name)
        ocmRemoveDT.Visible = (e.Page.Name = otpdetailitem.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        'Try
        '    With ogvdetail
        '        Dim _RevisedRemark As String = ""
        '        Dim _CheckRcv As Boolean = False
        '        If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

        '        Dim _FNSeq As Integer = Val("" & ogvdetail.GetFocusedRowCellValue("FNSeq").ToString)

        '        If CheckOwner() = False Then Exit Sub

        '        If FTPurchaseNo.Text <> "" Then
        '            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        '        End If

        '        Dim _Qry As String = ""

        '        With _AddItem
        '            .AddMat = False
        '            .PONO = FTPurchaseNo.Text
        '            .FNSeq = _FNSeq
        '            HI.TL.HandlerControl.ClearControl(_AddItem)
        '            .FTSMPOrderNo.Text = "" & ogvdetail.GetFocusedRowCellValue("FTSMPOrderNo").ToString
        '            .FTSMPMatCode.Text = "" & ogvdetail.GetFocusedRowCellValue("FTMatCode").ToString
        '            .FTSMPMatColorCode.Text = "" & ogvdetail.GetFocusedRowCellValue("FTMatColorCode").ToString
        '            .FTSMPMatSizeCode.Text = "" & ogvdetail.GetFocusedRowCellValue("FTMatSizeCode").ToString
        '            .FTSMPMatDescription.Text = "" & ogvdetail.GetFocusedRowCellValue("FTDescription").ToString
        '            .FNHSysUnitIdPO.Text = "" & ogvdetail.GetFocusedRowCellValue("FTUnitCode").ToString
        '            .FNPrice.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNPrice").ToString)
        '            .FNQuantity.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNQuantity").ToString)
        '            .FTNote.Text = "" & ogvdetail.GetFocusedRowCellValue("FTNote").ToString
        '            .ShowDialog()

        '            If (.AddMat) Then
        '                Dim _Str As String = ""

        '                _Str = "SELECT TOP 1 FNSeq +1  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail WITH(NOLOCK) "
        '                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        '                _Str &= vbCrLf & " ORDER BY FNSeq DESC  "

        '                _FNSeq = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SAMPLE, "1")))


        '                Try
        '                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
        '                    HI.Conn.SQLConn.SqlConnectionOpen()
        '                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        '                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


        '                    _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        '                    _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
        '                    _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
        '                    _Str &= vbCrLf & " ,FNPrice=" & .FNPrice.Value & ""
        '                    _Str &= vbCrLf & " ,FNQuantity=" & .FNQuantity.Value & ""
        '                    _Str &= vbCrLf & " ,FNAmount=" & .FNAmount.Value & ""
        '                    _Str &= vbCrLf & " ,FTNote=N'" & HI.UL.ULF.rpQuoted(.FTNote.Text.Trim) & "' "
        '                    _Str &= vbCrLf & " ,FTDescription=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatDescription.Text.Trim) & "' "
        '                    _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
        '                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        '                    _Str &= vbCrLf & "  AND FTSMPOrderNo=N'" & HI.UL.ULF.rpQuoted(.FTSMPOrderNo.Text.Trim) & "' "
        '                    _Str &= vbCrLf & "  AND FTMatCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatCode.Text.Trim) & "' "
        '                    _Str &= vbCrLf & "  AND FTMatColorCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatColorCode.Text.Trim) & "' "
        '                    _Str &= vbCrLf & "  AND FTMatSizeCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatSizeCode.Text.Trim) & "' "


        '                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

        '                        _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail(FTInsUser, FDInsDate, FTInsTime"
        '                        _Str &= vbCrLf & " , FTPurchaseNo, FNSeq, FTSMPOrderNo,FTMatCode,FTMatColorCode,FTMatSizeCode, FTDescription,FNHSysUnitId, FNQuantity, FNPrice, FNAmount, FTNote)"
        '                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        '                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
        '                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
        '                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        '                        _Str &= vbCrLf & "," & _FNSeq & " "
        '                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPOrderNo.Text) & "' "
        '                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatCode.Text.Trim) & "' "
        '                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatColorCode.Text.Trim) & "' "
        '                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatSizeCode.Text.Trim) & "' "
        '                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSMPMatDescription.Text.Trim) & "' "
        '                        _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
        '                        _Str &= vbCrLf & "," & .FNQuantity.Value & " "
        '                        _Str &= vbCrLf & "," & .FNPrice.Value & " "
        '                        _Str &= vbCrLf & "," & .FNAmount.Value & " "
        '                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTNote.Text.Trim) & "' "


        '                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                            HI.Conn.SQLConn.Tran.Rollback()
        '                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                            Exit Sub
        '                        End If
        '                    End If



        '                    _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail SET  "
        '                    _Str &= vbCrLf & " FNPrice=" & .FNPrice.Value & ""
        '                    _Str &= vbCrLf & " ,FNAmount= Convert(numeric(18,2),FNQuantity * " & .FNPrice.Value & ")"
        '                    _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
        '                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        '                    _Str &= vbCrLf & "  AND FTSMPOrderNo <> N'" & HI.UL.ULF.rpQuoted(.FTSMPOrderNo.Text.Trim) & "' "
        '                    _Str &= vbCrLf & "  AND FTMatCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatCode.Text.Trim) & "' "
        '                    _Str &= vbCrLf & "  AND FTMatColorCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatColorCode.Text.Trim) & "' "
        '                    _Str &= vbCrLf & "  AND FTMatSizeCode=N'" & HI.UL.ULF.rpQuoted(.FTSMPMatSizeCode.Text.Trim) & "' "
        '                    _Str &= vbCrLf & " AND FNPrice<>" & .FNPrice.Value & ""

        '                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                    End If

        '                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase "
        '                    _Str &= vbCrLf & "  SET FTStateSendApp='0' "
        '                    _Str &= vbCrLf & "  ,FTSendAppDate='' "
        '                    _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
        '                    _Str &= vbCrLf & "  ,FTSuperVisorName='' "
        '                    _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
        '                    _Str &= vbCrLf & "  ,FTSuperManagerName='' "
        '                    _Str &= vbCrLf & "  ,FTStatePDF='0' "
        '                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
        '                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

        '                    FTStateSendApp.Checked = False
        '                    FTStateSuperVisorApp.Checked = False
        '                    FTStateManagerApp.Checked = False

        '                    HI.Conn.SQLConn.Tran.Commit()
        '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        '                    _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice ))) AS NETAMT"
        '                    _Str &= vbCrLf & "    FROM"
        '                    _Str &= vbCrLf & " ("
        '                    _Str &= vbCrLf & " SELECT       FTMatCode,FTMatColorCode,FTMatSizeCode,  FNPrice, SUM(FNQuantity) AS  FNQuantity"
        '                    _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase_Detail AS A  WITH(NOLOCK)"
        '                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        '                    _Str &= vbCrLf & " GROUP BY  FTMatCode,FTMatColorCode,FTMatSizeCode,  FNPrice "
        '                    _Str &= vbCrLf & " ) AS A"

        '                    Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

        '                    Me.SaveData()

        '                    Me.LoadDetail(Me.FTPurchaseNo.Text)

        '                Catch ex As Exception

        '                    HI.Conn.SQLConn.Tran.Rollback()
        '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        '                End Try
        '            End If
        '        End With
        '    End With
        'Catch ex As Exception

        'End Try
    End Sub
End Class
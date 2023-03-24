Imports System.Windows.Forms

Public Class wPurchaseSendSupl_CVN

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

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

    End Sub

#Region "Grid"
    Private Sub InitGridControl()

        With ogvsenddetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n0}"
            .Columns("FNAmount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount")
            .Columns("FNAmount").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"

            .Columns("FNAmtDeductSupl").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmtDeductSupl")
            .Columns("FNAmtDeductSupl").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"

            .Columns("FNPaySupl").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNPaySupl")
            .Columns("FNPaySupl").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"

            .Columns("FNQtyDeduct").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQtyDeduct")
            .Columns("FNQtyDeduct").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"

            .OptionsView.ShowFooter = True
        End With

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
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
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='wPurchaseSendSupl'"
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

        Me.FNHSysCurId.Text = ""
        Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        Me.FNExchangeRate.Value = 1

        Call LoadDocRef(Key)
        Call LoadDocIssueRef(Key)
        'Call LoadDetail("")
        ' Call LoadDetail(Me.FTSendSuplNo.Text)
        otbm.SelectedTabPageIndex = 0
        otbdocref.SelectedTabPageIndex = 0

        _ProcLoad = False
        _FormLoad = False
    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
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
        Me.ogcdetail.DataSource = Nothing

        otbdocref.SelectedTabPageIndex = 0
        otbm.SelectedTabPageIndex = 0

        Me.FNHSysCurId.Text = ""
        Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        Me.FNExchangeRate.Value = 1

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

            SaveDetail(_Key)

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

    Private Function SaveDetail(ByVal _Key As String) As Boolean

        Try
            Dim _Qry As String = ""
            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocSendRef"
            _Qry &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If Not (ogcdocref.DataSource Is Nothing) Then
                Dim dtDocRef As DataTable
                With CType(ogcdocref.DataSource, DataTable)
                    .AcceptChanges()
                    dtDocRef = .Copy
                End With

                For Each R As DataRow In dtDocRef.Rows
                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocSendRef"
                    _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Qry &= vbCrLf & "AND FTSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocSendRef"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTPurchaseNo, FTSendSuplNo)"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Next
            End If

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocIssueRef"
            _Qry &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If Not (ogcdocissref.DataSource Is Nothing) Then
                Dim dtDocIssRef As DataTable
                With CType(ogcdocissref.DataSource, DataTable)
                    .AcceptChanges()
                    dtDocIssRef = .Copy
                End With

                For Each R As DataRow In dtDocIssRef.Rows
                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocIssueRef"
                    _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Qry &= vbCrLf & "AND FTIssueNo='" & HI.UL.ULF.rpQuoted(R!FTIssueNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocIssueRef"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTPurchaseNo, FTIssueNo)"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTIssueNo.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Next
            End If

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_Detail"
            _Qry &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If Not (ogcsenddetail.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogcsenddetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Rows

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_Detail"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNQuantity=" & CDbl("0" & R!FNQuantity.ToString)
                        ' _Qry &= vbCrLf & ",FNPrice=" & CDbl("0" & R!FNPrice.ToString)
                        _Qry &= vbCrLf & ",FNAmount=" & CDbl("0" & R!FNAmount.ToString)
                        _Qry &= vbCrLf & ",FTNote='" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                        _Qry &= vbCrLf & ",FNQuantityRcv=" & Double.Parse("0" & R!FNSendSupQty.ToString)
                        _Qry &= vbCrLf & ",FNQuantityDefect=" & Double.Parse("0" & R!FNDefectQty.ToString)
                        _Qry &= vbCrLf & ",FNQuantityDefectDeduct=" & Double.Parse("0" & R!FNQtyDeduct.ToString)
                        _Qry &= vbCrLf & ",FNAmtDefectDeduct=" & Double.Parse("0" & R!FNAmtDeductSupl.ToString)

                        _Qry &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & "AND FNHSysPartId=" & CInt("0" & R!FNHSysPartId.ToString)
                        _Qry &= vbCrLf & "AND FNSendSuplType=" & CDbl("0" & R!FNSendSuplType.ToString)
                        _Qry &= vbCrLf & "AND FTNote='" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                        _Qry &= vbCrLf & "AND FNPrice=" & CDbl("0" & R!FNPrice.ToString)

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_Detail"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTPurchaseNo, FNHSysPartId, FNSendSuplType, FNQuantity, FNPrice, FNAmount,FTNote ,FNQuantityRcv,FNQuantityDefect,FNQuantityDefectDeduct,FNAmtDefectDeduct)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysPartId.ToString)
                            _Qry &= vbCrLf & "," & CDbl("0" & R!FNSendSuplType.ToString)
                            _Qry &= vbCrLf & "," & CDbl("0" & R!FNQuantity.ToString)
                            _Qry &= vbCrLf & "," & CDbl("0" & R!FNPrice.ToString)
                            _Qry &= vbCrLf & "," & CDbl("0" & R!FNAmount.ToString)
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                            _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNSendSupQty.ToString)
                            _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNDefectQty.ToString)
                            _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNQtyDeduct.ToString)
                            _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNAmtDeductSupl.ToString)


                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            End If

        Catch ex As Exception
        End Try

    End Function

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_Detail WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocIssueRef WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocSendRef WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

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
        _FormLoad = False

    End Sub

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub
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

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "PurchaseSendSupl.rpt"
                .Formular = "{TPRODTPurchaseSendSupl.FTPurchaseNo} ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                .Preview()
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

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged

        Try
            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FNHSysSuplId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysSuplId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            dt.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.ogvdocref.OptionsView.ShowAutoFilterRow = False
        Me.ogvdocissref.OptionsView.ShowAutoFilterRow = False
        _FormLoad = False

    End Sub


    Private Sub CalculateAmt(Dt As DataTable)

        Dim _Amt As Double = 0
        Dim _tmpamt As Double = 0

        For Each R As DataRow In Dt.Rows
            _tmpamt = Val(R!FNAmount)
            _Amt = _Amt + _tmpamt
        Next

        Me.FNPoAmt.Value = _Amt

    End Sub

    Private Sub LoadDocRef(ByVal _DocRefNo As String)
        Dim _dt As DataTable : Dim _oDt As New DataTable
        Dim _Qry As String = ""
        _Qry = "  Select FTSendSuplNo"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocSendRef AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "' "
        _Qry &= vbCrLf & " ORDER BY FTSendSuplNo "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With _oDt
            .Columns.Add("FTSendSuplNo", GetType(String))
            .Columns.Add("FTInvoiceRef", GetType(String))
        End With
        With _dt
            .BeginInit()
            If .Rows.Count > 0 Then
                .Columns.Add("FTInvoiceRef", GetType(String))
            End If
            For Each r As DataRow In .Rows
                _oDt.Rows.Add(r!FTSendSuplNo.ToString, GetInvoice(r!FTSendSuplNo.ToString))
            Next
            .EndInit()
        End With

        Me.ogcdocref.DataSource = _oDt

    End Sub


    Private Sub LoadDocIssueRef(ByVal _DocRefNo As String)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = "  Select FTIssueNo"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocIssueRef AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "' "
        _Qry &= vbCrLf & " ORDER BY FTIssueNo "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogcdocissref.DataSource = _dt

    End Sub

    Private Sub LoadDetail(ByVal _DocRefNo As String)
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable
            Dim _dtgrp As DataTable

            If Not (Me.ogcdocref.DataSource Is Nothing) Then
                With CType(Me.ogcdocref.DataSource, DataTable)
                    .AcceptChanges()
                    _dtdoc = .Copy
                End With

                For Each R As DataRow In _dtdoc.Rows
                    If R!FTSendSuplNo.ToString <> "" Then
                        If _DocNo = "" Then
                            _DocNo = R!FTSendSuplNo.ToString
                        Else
                            _DocNo = _DocNo & "','" & R!FTSendSuplNo.ToString
                        End If
                    End If
                Next
            End If

            _dtdoc.Dispose()

            _Qry = " SELECT  A.FNHSysPartId,A.FNSendSuplType, A.FTNote "

            _Qry &= vbCrLf & " , A.FNSendSupQty - (A.FNDefectQtyFac + ISNULL ((SELECT        SUM(FNFacDefectQty) AS FNDefectQty"
            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPROSendSuplDefect AS XQA WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTDocumentRef = A.FTSendSuplNo) AND (ISNULL(FTStateFromSupl, '0') = '1')), 0) ) AS FNQuantity"


            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",PM.FTPartNameTH AS FTPartName"
                _Qry &= vbCrLf & ",C.FNSendSuplTypeNameTH AS FNSendSuplTypeName"
            Else

                _Qry &= vbCrLf & ",PM.FTPartNameEN  AS FTPartName  "
                _Qry &= vbCrLf & ",C.FNSendSuplTypeNameEN AS FNSendSuplTypeName "
            End If

            _Qry &= vbCrLf & "	,ISNULL(D.FNPrice,0.0000) AS FNPrice"
            _Qry &= vbCrLf & "	,Convert(numeric(18,2),A.FNQuantity * ISNULL(D.FNPrice,0.0000))  AS FNAmount "

            _Qry &= vbCrLf & ", Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) as   FNDefectAcceptPer"
            _Qry &= vbCrLf & ",A.FNFarbicAmt"
            _Qry &= vbCrLf & ",A.FNDefectQty "
            _Qry &= vbCrLf & ",A.FNSendSupQty"
            ' _Qry &= vbCrLf & ",A.FNDefectQtySupl "

            _Qry &= vbCrLf & " , ISNULL ((SELECT        SUM(FNFacDefectQty) AS FNDefectQty"
            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPROSendSuplDefect AS XQA WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTDocumentRef = A.FTSendSuplNo) AND (ISNULL(FTStateFromSupl, '0') = '1')), 0) AS FNDefectQtySupl"


            _Qry &= vbCrLf & ",A.FNDefectQtyFac "

            '_Qry &= vbCrLf & ",(CASE WHEN A.FNDefectQty > Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) THEN Convert(numeric(18,2),"
            '_Qry &= "(((A.FNDefectQty -Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100))) * A.FNFarbicAmt) + ((A.FNDefectQty - Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) ) * A.FNCutBaht))) Else 0 END  "
            '_Qry &= vbCrLf & ") AS FNAmtDeductSupl"

            _Qry &= vbCrLf & " ,(CASE WHEN (ISNULL ((SELECT        SUM(FNFacDefectQty) AS FNDefectQty"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPROSendSuplDefect AS XQA WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE        (FTDocumentRef = A.FTSendSuplNo) AND (ISNULL(FTStateFromSupl, '0') = '1')), 0) + A.FNDefectQtyFac ) > Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) "
            _Qry &= vbCrLf & "THEN Convert(numeric(18,2),((((ISNULL ((SELECT        SUM(FNFacDefectQty) AS FNDefectQty"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPROSendSuplDefect AS XQA WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE        (FTDocumentRef = A.FTSendSuplNo) AND (ISNULL(FTStateFromSupl, '0') = '1')), 0) + A.FNDefectQtyFac ) -Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100))) * A.FNFarbicAmt) +"
            _Qry &= vbCrLf & " (((ISNULL ((SELECT        SUM(FNFacDefectQty) AS FNDefectQty"
            _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPROSendSuplDefect AS XQA WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTDocumentRef = A.FTSendSuplNo) AND (ISNULL(FTStateFromSupl, '0') = '1')), 0) + A.FNDefectQtyFac ) - Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) ) * A.FNCutBaht)))"
            _Qry &= vbCrLf & "Else 0 END  "
            _Qry &= vbCrLf & ") AS FNAmtDeductSupl"



            _Qry &= vbCrLf & ",(CASE WHEN A.FNDefectQty > Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) THEN Convert(numeric(18,2),A.FNQuantity * ISNULL(D.FNPrice,0.0000) - "
            _Qry &= "(((A.FNDefectQty -Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100))) * A.FNFarbicAmt) + ((A.FNDefectQty - Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) ) * A.FNCutBaht))) Else Convert(numeric(18,2),A.FNQuantity * ISNULL(D.FNPrice,0.0000)) END  "
            _Qry &= vbCrLf & ") AS FNPaySupl"

            _Qry &= vbCrLf & ",CASE WHEN (ISNULL ((SELECT        SUM(FNFacDefectQty) AS FNDefectQty"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPROSendSuplDefect AS XQA WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE        (FTDocumentRef = A.FTSendSuplNo) AND (ISNULL(FTStateFromSupl, '0') = '1')), 0) + A.FNDefectQtyFac ) > Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)) THEN (((ISNULL ((SELECT        SUM(FNFacDefectQty) AS FNDefectQty"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPROSendSuplDefect AS XQA WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE        (FTDocumentRef = A.FTSendSuplNo) AND (ISNULL(FTStateFromSupl, '0') = '1')), 0) + A.FNDefectQtyFac ) -Convert(int, ((A.FNSendSupQty * A.FNDefectAcceptPer) /100)))) ELSE 0 END AS FNQtyDeduct "


            _Qry &= vbCrLf & "	    FROM "
            _Qry &= vbCrLf & "	 ("

            _Qry &= vbCrLf & "	 SELECT  FTSendSuplNo,  M.FNHSysPartId, M.FNSendSuplType"
            _Qry &= vbCrLf & " , SUM(M.FNQuantity-ISNULL(M.FNDefectQty,0)) AS FNQuantity,FTNote AS FTNote"
            _Qry &= vbCrLf & " ,sum(ISNULL(M.FNDefectQty,0)) AS FNDefectQty , max(M.FNFarbicAmt) AS FNFarbicAmt"
            _Qry &= vbCrLf & " ,(SELECT        TOP (1)  FTCfgData"
            _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSESystemConfig WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE        (FTCfgName = N'CfDefectAcceptPer')) AS FNDefectAcceptPer"
            _Qry &= vbCrLf & " ,(SELECT        TOP (1)  FTCfgData"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSESystemConfig WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE        (FTCfgName = N'CfCutBaht')) AS FNCutBaht"
            _Qry &= vbCrLf & " ,SUM(M.FNQuantity) AS FNSendSupQty"
            _Qry &= vbCrLf & " ,sum(ISNULL(M.FNDefectQtyFac,0)) as FNDefectQtyFac,sum(ISNULL(M.FNDefectQtySupl,0))  as FNDefectQtySupl"


            _Qry &= vbCrLf & "	FROM ("
            _Qry &= vbCrLf & "	 SELECT  PS.FTSendSuplNo,  BSS.FNHSysPartId, BSS.FNSendSuplType,O.FTOrderNo"
            _Qry &= vbCrLf & " , BD.FNQuantity"

            _Qry &= vbCrLf & " , isnull(( Select Isnull(FNConSmp,0) * Isnull(FNPrice,0) AS CPC"
            _Qry &= vbCrLf & " From(Select TOP 1 M.FTOrderNo , M.FNHSysStyleId ,M.FNHSysMerMatId , M.FNConSmp , A.FTMainMatCode , B.FNHSysRawMatId , C.FNPrice "
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle_Mat AS M WITH(NOLOCK) LEFT OUTER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainMat AS A WITH(NOLOCK) ON M.FNHSysMerMatId = A.FNHSysMainMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMaterial AS B WITH(NOLOCK) ON A.FTMainMatCode = B.FTRawMatCode"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode AS C WITH(NOLOCK) ON B.FNHSysRawMatId = C.FNHSysRawMatId"
            _Qry &= vbCrLf & "where M.FTStateMainMaterial = '1'"
            _Qry &= vbCrLf & "and M.FNHSysStyleId = O.FNHSysStyleId --and M.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & "order by isnull(C.FNPrice ,0) desc) AS T"
            _Qry &= vbCrLf & " ),0) AS FNFarbicAmt"


            _Qry &= vbCrLf & " ,ISNULL(("
            _Qry &= vbCrLf & " SELECT TOP 1 FTNote"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   WHERE X.FTSendSuplRef = BSS.FTSendSuplRef "
            _Qry &= vbCrLf & " ),'') AS FTNote"

            _Qry &= vbCrLf & " ,ISNULL(("
            _Qry &= vbCrLf & " SELECT SUM(FNDefectQty) as  FNDefectQty"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS XQA WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE XQA.FTBarcodeSendSuplNo = PSB.FTBarcodeSendSuplNo "
            _Qry &= vbCrLf & " ),0) AS FNDefectQty "

            _Qry &= vbCrLf & " ,ISNULL(("
            _Qry &= vbCrLf & " SELECT SUM(FNDefectQty) as  FNDefectQty"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS XQA WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE XQA.FTBarcodeSendSuplNo = PSB.FTBarcodeSendSuplNo and Isnull(XQA.FTStateFromSupl,'0') = '1'  "
            _Qry &= vbCrLf & " ),0) AS FNDefectQtySupl "

            _Qry &= vbCrLf & " ,Convert(int, ISNULL(("
            _Qry &= vbCrLf & " SELECT SUM(FNDefectQty) as  FNDefectQty"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS XQA WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE XQA.FTBarcodeSendSuplNo = PSB.FTBarcodeSendSuplNo and Isnull(XQA.FTStateFromSupl,'0') = '0'  "
            _Qry &= vbCrLf & " ),0)) AS FNDefectQtyFac "


            _Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS PS WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "	          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS PSB WITH(NOLOCK)   ON PS.FTSendSuplNo = PSB.FTSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "	          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BSS WITH(NOLOCK)   ON PSB.FTBarcodeSendSuplNo = BSS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "	          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BD WITH(NOLOCK)   ON BSS.FTBarcodeBundleNo = BD.FTBarcodeBundleNo"
            _Qry &= vbCrLf & "	   INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH(NOLOCK)   ON BD.FTOrderProdNo = ODP.FTOrderProdNo"
            _Qry &= vbCrLf & "	   INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)   ON ODP.FTOrderNo = O.FTOrderNo"

            _Qry &= vbCrLf & "	  WHERE PS.FNHSysSuplId =" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
            _Qry &= vbCrLf & "	 AND PS.FTSendSuplNo in ('" & _DocNo & "')"
            _Qry &= vbCrLf & "	) AS M "
            _Qry &= vbCrLf & "	 GROUP BY M.FNHSysPartId, M.FNSendSuplType,M.FTNote , FTSendSuplNo "

            _Qry &= vbCrLf & "	 ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS PM WITH(NOLOCK)"
            _Qry &= vbCrLf & "	 ON A.FNHSysPartId = PM.FNHSysPartId LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	 SELECT FNListIndex, FTNameTH AS FNSendSuplTypeNameTH, FTNameEN AS FNSendSuplTypeNameEN"
            _Qry &= vbCrLf & "	 	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Qry &= vbCrLf & "	 	WHERE  (FTListName = N'FNSendSuplType')"
            _Qry &= vbCrLf & "	 ) AS C ON A.FNSendSuplType = C.FNListIndex LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	 ("
            _Qry &= vbCrLf & "	 SELECT FTPurchaseNo, FNHSysPartId, FNSendSuplType, FNQuantity, FNPrice, FNAmount,FTNote "
            _Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_Detail  WITH(NOLOCK)"
            _Qry &= vbCrLf & "	 WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'"
            _Qry &= vbCrLf & "	 ) AS D ON A.FNHSysPartId = D.FNHSysPartId AND A.FNSendSuplType=D.FNSendSuplType AND A.FTNote = D.FTNote "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcsenddetail.DataSource = _oDt.Copy

            Call CalculateAmt(_oDt)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDetailIssue(ByVal _DocRefNo As String)
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            If Not (Me.ogcdocissref.DataSource Is Nothing) Then
                With CType(Me.ogcdocissref.DataSource, DataTable)
                    .AcceptChanges()
                    _dtdoc = .Copy
                End With

                For Each R As DataRow In _dtdoc.Rows
                    If R!FTIssueNo.ToString <> "" Then
                        If _DocNo = "" Then
                            _DocNo = R!FTIssueNo.ToString
                        Else
                            _DocNo = _DocNo & "','" & R!FTIssueNo.ToString
                        End If
                    End If
                Next
            End If

            _dtdoc.Dispose()

            _Qry = " Select A.FNHSysRawMatId"
            _Qry &= vbCrLf & "  ,IM.FTRawMatCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  ,IM.FTRawMatNameTH AS FTRawMatName "
            Else
                _Qry &= vbCrLf & "  ,IM.FTRawMatNameEN AS FTRawMatName "
            End If

            _Qry &= vbCrLf & "  ,IMC.FTRawMatColorCode "
            _Qry &= vbCrLf & " ,IMS.FTRawMatSizeCode "
            _Qry &= vbCrLf & " ,A.FNQuantity"
            _Qry &= vbCrLf & "  ,U.FTUnitCode "
            _Qry &= vbCrLf & " FROM (SELECT B.FNHSysRawMatId, SUM(BO.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)   ON H.FNHSysWHId = BO.FNHSysWHId INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)  ON BO.FTBarcodeNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & " WHERE  (H.FTIssueNo in ('" & _DocNo & "'))"
            _Qry &= vbCrLf & " GROUP BY B.FNHSysRawMatId) AS A INNER JOIN "
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON A.FNHSysRawMatId  = IM.FNHSysRawMatId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK) ON IM.FNHSysRawMatSizeId  = IMS.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit  AS U WITH(NOLOCK) ON IM.FNHSysUnitId   = U.FNHSysUnitId"
            _Qry &= vbCrLf & " ORDER BY IM.FTRawMatCode ,IMC.FTRawMatColorCode  ,IMS.FNRawMatSizeSeq "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcdetail.DataSource = _oDt.Copy

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

    Private Sub ogvdocref_DoubleClick(sender As Object, e As EventArgs) Handles ogvdocref.DoubleClick
        Try
            With ogvdocref
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)

                With CType(Me.ogcdocref.DataSource, DataTable)
                    .AcceptChanges()

                End With

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdocref_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdocref.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Try
                        With ogvdocref
                            If .FocusedRowHandle < 0 Then Exit Sub
                            .DeleteRow(.FocusedRowHandle)

                            With CType(Me.ogcdocref.DataSource, DataTable)
                                .AcceptChanges()

                            End With

                        End With
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdocref_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdocref.RowCountChanged

        Try
            Call LoadDetail("")
        Catch ex As Exception
        End Try

    End Sub

    Private Sub FTDocRefNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSendSuplNo.EditValueChanged
    End Sub

    Private Sub FTDocRefNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTSendSuplNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter

                    If FTSendSuplNo.Text = "" Then Exit Sub
                    If FTSendSuplNo.Properties.Tag.ToString = "" Then Exit Sub

                    Dim _Cmd As String = ""

                    Dim _dtdoc As DataTable
                    If Me.ogcdocref.DataSource Is Nothing Then
                        Dim dt As New DataTable
                        dt.Columns.Add("FTSendSuplNo", GetType(String))
                        dt.Columns.Add("FTInvoiceRef", GetType(String))
                        Me.ogcdocref.DataSource = dt
                    End If

                    With CType(Me.ogcdocref.DataSource, DataTable)
                        .AcceptChanges()
                        _dtdoc = .Copy
                    End With

                    If _dtdoc.Select("FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'").Length <= 0 Then
                        _dtdoc.Rows.Add(FTSendSuplNo.Text, GetInvoice(Me.FTSendSuplNo.Text))
                    End If

                    Me.ogcdocref.DataSource = _dtdoc
                    Me.ogcdocref.Refresh()

                    FTSendSuplNo.Text = ""
                    FTSendSuplNo.Focus()

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetInvoice(Key As String) As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT       Top 1   (SELECT        TOP 1 STUFF"
            _Cmd &= vbCrLf & "           ((SELECT        ', ' + t2.FTInvoiceNo"
            _Cmd &= vbCrLf & "              FROM            (SELECT        S.FTSendSuplNo,M.FTInvoiceNo"
            _Cmd &= vbCrLf & "                          FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "							[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS R WITH (NOLOCK) ON S.FTBarcodeSendSuplNo = R.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "							[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS M WITH (NOLOCK) ON R.FTRcvSuplNo = M.FTRcvSuplNo"
            _Cmd &= vbCrLf & "							 group by  S.FTSendSuplNo,M.FTInvoiceNo"
            _Cmd &= vbCrLf & "							) t2"
            _Cmd &= vbCrLf & "                       WHERE        t2.FTSendSuplNo = S.FTSendSuplNo FOR XML PATH('')), 1, 2, '') AS FTOrderNo) AS FTInvoiceNo"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS R WITH (NOLOCK) ON S.FTBarcodeSendSuplNo = R.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS M WITH (NOLOCK) ON R.FTRcvSuplNo = M.FTRcvSuplNo"
            _Cmd &= vbCrLf & "where S.FTSendSuplNo = '" & HI.UL.ULF.rpQuoted(Key) & "' and M.FTInvoiceNo <> ''"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged

        Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        Me.FNExchangeRate.Value = 1

    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As Object, e As EventArgs) Handles FNExchangeRate.EditValueChanged
        Try
            If FNExchangeRate.Value <> 1 Then
                FNExchangeRate.Value = 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogcdocref_Click(sender As Object, e As EventArgs) Handles ogcdocref.Click

    End Sub

    Private Sub ogvdocissref_DoubleClick(sender As Object, e As EventArgs) Handles ogvdocissref.DoubleClick
        Try
            With ogvdocissref
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)

                With CType(Me.ogcdocissref.DataSource, DataTable)
                    .AcceptChanges()

                End With

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdocissref_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdocissref.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Try
                        With ogvdocissref
                            If .FocusedRowHandle < 0 Then Exit Sub
                            .DeleteRow(.FocusedRowHandle)

                            With CType(Me.ogcdocissref.DataSource, DataTable)
                                .AcceptChanges()

                            End With

                        End With
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdocissref_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdocissref.RowCountChanged
        Try
            Call LoadDetailIssue("")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTDocIssRefNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTIssueNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter

                    If FTIssueNo.Text = "" Then Exit Sub
                    If FTIssueNo.Properties.Tag.ToString = "" Then Exit Sub

                    Dim _dtdoc As DataTable
                    If Me.ogcdocissref.DataSource Is Nothing Then
                        Dim dt As New DataTable
                        dt.Columns.Add("FTIssueNo", GetType(String))
                        Me.ogcdocissref.DataSource = dt
                    End If

                    With CType(Me.ogcdocissref.DataSource, DataTable)
                        .AcceptChanges()
                        _dtdoc = .Copy
                    End With

                    If _dtdoc.Select("FTIssueNo='" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & "'").Length <= 0 Then
                        _dtdoc.Rows.Add(FTIssueNo.Text)
                    End If

                    Me.ogcdocissref.DataSource = _dtdoc
                    Me.ogcdocissref.Refresh()

                    FTIssueNo.Text = ""
                    FTIssueNo.Focus()

            End Select

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepGFNPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepGFNPrice.EditValueChanging

        Try

            Dim _TotalAmt As Double = 0
            Dim _TotalQty As Double = 0
            Dim _TotalDQty As Double = 0

            With Me.ogvsenddetail

                .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)
                _TotalQty = Double.Parse(Val(.GetFocusedRowCellValue("FNQuantity")))
                _TotalAmt = Format(_TotalQty * e.NewValue, HI.ST.Config.AmtFormat)
                _TotalDQty = Double.Parse(Val(.GetFocusedRowCellValue("FNAmtDeductSupl")))

                .SetFocusedRowCellValue("FNAmount", _TotalAmt)

                .SetFocusedRowCellValue("FNPaySupl", _TotalAmt - _TotalDQty)



            End With

            Dim _dtsource As DataTable
            With CType(ogcsenddetail.DataSource, DataTable)
                .AcceptChanges()
                _dtsource = .Copy
            End With

            Dim _FNPoAmt As Double = 0

            For Each R As DataRow In _dtsource.Rows
                _FNPoAmt = _FNPoAmt + Val(R!FNPaySupl.ToString)
            Next

            FNPoAmt.Value = _FNPoAmt

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPurchaseNo.EditValueChanged

    End Sub
End Class
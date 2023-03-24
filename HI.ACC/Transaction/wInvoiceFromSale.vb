Imports System.Windows.Forms

Public Class wInvoiceFromSale

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
    Private _oDtPacking As DataTable
    Private RetoyCal As New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call PrepareForm()

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("FNAmount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount")
            .Columns("FNAmount").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"

            .OptionsView.ShowFooter = True
        End With

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

        Call LoadDocumentDetail(Key.ToString)

        'If Me.FTInvoiceNo.Properties.Tag <> "" Then
        '    If ogvdetail.RowCount > 0 Then
        '        Me.FTDocRefNo.Enabled = False
        '    End If
        'Else
        '    Me.FTDocRefNo.Enabled = True
        'End If

        Call LoadDocRef(Key)
        Call LoadDetail(Me.FTDocRefNo.Text)

        _ProcLoad = False
        _FormLoad = False
    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTInvoiceBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub LoadDocumentDetail(PoKey As String)

        Dim _Str As String = ""

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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.ogcdetail.DataSource = Nothing
        'If Me.FTInvoiceNo.Properties.Tag <> "" Then
        '    If ogvdetail.RowCount > 0 Then
        '        Me.FTDocRefNo.Enabled = False
        '    End If
        'Else
        '    Me.FTDocRefNo.Enabled = True
        '    Me.FTDocRefNo.Properties.Buttons.Item(0).Enabled = True
        'End If
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
                                    Try
                                        If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                            Pass = False
                                        End If
                                    Catch ex As Exception
                                    End Try

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

                                If SysDocType <> "0" Then
                                    If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, SysDocType, True, _CmpH).ToString() Then
                                        _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                        If _dt.Rows.Count <= 0 Then
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                            Obj.Focus()
                                            Return False
                                        End If
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next

        If Me.FNInvoiceState.SelectedIndex <> 0 Then
            If Me.FNHSysCmpIdTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpIdTo_lbl.Text)
                Me.FNHSysCmpIdTo.Focus()
                Return False
            End If
        End If


        Return True
    End Function

    Private Overloads Function SaveData(Optional ByVal State As Boolean = True) As Boolean

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
                                'If SysDocType <> "0" Then
                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, SysDocType, True, _CmpH).ToString() Then
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
                                    'Else

                                    '    _Key = .Text

                                    '    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    '    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
                                    '    If _dt.Rows.Count <= 0 Then
                                    '        _StateNew = True
                                    '    Else
                                    '        _StateNew = False
                                    '    End If

                                    'End If

                                End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then
            'If SysDocType <> "0" Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, FNInvoiceState.SelectedIndex.ToString(), False, _CmpH).ToString
            'End If


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

            If (State) Then
                SaveDetail(_Key)
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
            If (State) Then
                Call GetGrid()
                Call UpdateCTNs()
            End If
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

   
    Private Sub UpdateCTNs()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT     FTInvoiceNo, FNHSysRawMatId,  SUM(FNQtyCTN) AS FNQtyCTN ,  SUM(FNTotalNetWeight) AS  FNTotalNetWeight, sum(FNTotalGrossWeight) AS FNTotalGrossWeight"
            _Cmd &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Packing WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _Cmd &= vbCrLf & "group by  FTInvoiceNo, FNHSysRawMatId"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            For Each R As DataRow In _oDt.Rows
                _Cmd = "Update   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail "
                _Cmd &= vbCrLf & "Set FNCTN =" & Integer.Parse(R!FNQtyCTN.ToString)
                _Cmd &= vbCrLf & ",FNNW =" & Double.Parse(R!FNTotalNetWeight.ToString)
                _Cmd &= vbCrLf & ",FNGW =" & Double.Parse(R!FNTotalGrossWeight.ToString)
                _Cmd &= vbCrLf & "WHERE FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                _Cmd &= vbCrLf & "and FNHSysRawMatId =" & Integer.Parse(R!FNHSysRawMatId.ToString)
                HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Function SaveDetail(ByVal _Key As String) As Boolean
        If Not (Me.ogcpackinglist.DataSource Is Nothing) Then
            Dim dt23 As DataTable
            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                dt23 = .Copy
            End With
            Dim _ogvpackinglist As DataTable
            With CType(Me.ogcpackinglist.DataSource, DataTable)
                .AcceptChanges()
                _ogvpackinglist = .Copy
            End With
            Dim _FNGrpSeq As Integer = 0
            For Each R As DataRow In _ogvpackinglist.Rows
                _FNGrpSeq = _FNGrpSeq + 1
                For Each Rx As DataRow In dt23.Select("FNHSysMatTypeId=" & Val(R!FNHSysMatTypeId) & " AND FTRawMatNameEN='" & HI.UL.ULF.rpQuoted(R!FTRawMatName.ToString) & "'")

                    Rx!FNCTN = R!FNCTN
                    Rx!FNNW = R!FNNW
                    Rx!FNGW = R!FNGW
                    Rx!FNQBM = R!FNQBM
                    Rx!FNGrpSeq = _FNGrpSeq
                    Rx!FNChargeServicePer = R!FNChargeServicePer.ToString
                    Rx!FNChargeClearPer = R!FNChargeClearPer.ToString
                    Rx!FNChargeService = R!FNChargeService.ToString
                    Rx!FNChargeClear = R!FNChargeClear.ToString
                    Rx!FNNetPrice = R!FNNetPrice.ToString

                Next
            Next

            Me.ogcdetail.DataSource = dt23

        End If

        Try
            Dim _Qry As String = ""
            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo. TACCTSaleInvoice_DocRef"
            _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If Not (ogcdocref.DataSource Is Nothing) Then
                Dim dtDocRef As DataTable
                With CType(ogcdocref.DataSource, DataTable)
                    .AcceptChanges()
                    dtDocRef = .Copy
                End With

                For Each R As DataRow In dtDocRef.Rows
                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef"
                    _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Qry &= vbCrLf & "AND FTDocRefNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTInvoiceNo, FTDocRefNo)"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Next
            End If

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail"
            _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If Not (ogcdetail.DataSource Is Nothing) Then

                Dim dt As DataTable

                With CType(ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With


                For Each R As DataRow In dt.Rows

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail"
                    _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",FNQuantity=FNQuantity+" & CDbl("0" & R!FNQuantity.ToString)
                    _Qry &= vbCrLf & ",FNPriceSale=" & CDbl("0" & R!FNPriceSale.ToString)
                    _Qry &= vbCrLf & ",FNChargeService=" & CDbl("0" & R!FNChargeService.ToString)
                    _Qry &= vbCrLf & ",FNChargeClear=" & CDbl("0" & R!FNChargeClear.ToString)
                    _Qry &= vbCrLf & ",FNNetPrice=" & CDbl("0" & R!FNNetPrice.ToString)
                    _Qry &= vbCrLf & ",FNHSysUnitId=" & CDbl("0" & R!FNHSysUnitId_Hide.ToString)
                    _Qry &= vbCrLf & ",FNCTN=" & CDbl("0" & R!FNCTN.ToString)
                    _Qry &= vbCrLf & ",FNNW=" & CDbl("0" & R!FNNW.ToString)
                    _Qry &= vbCrLf & ",FNGW=" & CDbl("0" & R!FNGW.ToString)
                    _Qry &= vbCrLf & ",FNQBM=" & CDbl("0" & R!FNQBM.ToString)
                    _Qry &= vbCrLf & ",FNGrpSeq=" & CDbl("0" & R!FNGrpSeq.ToString)
                    _Qry &= vbCrLf & ",FNChargeServicePer=" & Double.Parse(R!FNChargeServicePer.ToString)
                    _Qry &= vbCrLf & ",FNChargeClearPer=" & Double.Parse(R!FNChargeClearPer.ToString)
                    _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Qry &= vbCrLf & "AND FNHSysRawMatId=" & CInt("0" & R!FNHSysRawMatId.ToString)
                    _Qry &= vbCrLf & "AND FNPrice=" & CDbl("0" & R!FNPrice.ToString)

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTInvoiceNo, FNHSysRawMatId, FNPrice, FNQuantity"
                        _Qry &= vbCrLf & ",FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice,FNHSysUnitId,FNCTN, FNNW, FNGW, FNQBM,FNGrpSeq,FNChargeServicePer,FNChargeClearPer)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysRawMatId.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNPrice.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNQuantity.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNPriceSale.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNChargeService.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNChargeClear.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNNetPrice.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNHSysUnitId_Hide.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNCTN.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNNW.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNGW.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNQBM.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNGrpSeq.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse(R!FNChargeServicePer.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse(R!FNChargeClearPer.ToString)

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Next

                _Qry = " update O set O.FNPriceTrans =  D.FNPriceSale "
                _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS R WITH (NOLOCK) ON I.FTInvoiceNo = R.FTInvoiceNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENBarcode_IN AS O WITH (NOLOCK) ON R.FTDocRefNo = O.FTDocumentNo"
                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeNo "
                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS D WITH(NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo and B.FNHSysRawMatId = D.FNHSysRawMatId"
                _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(_Key) & "')"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Qry = " update O set O.FNPriceTrans =  D.FNPriceSale    "
                    _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS R WITH (NOLOCK) ON I.FTInvoiceNo = R.FTInvoiceNo LEFT OUTER JOIN"
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENBarcode_OUT AS O WITH (NOLOCK) ON R.FTDocRefNo = O.FTDocumentNo"
                    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeNo "
                    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS D WITH(NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo and B.FNHSysRawMatId = D.FNHSysRawMatId"
                    _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(_Key) & "')"
                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If

                _Qry = " update B set    B.FNPriceTrans =  D.FNPriceSale     "
                _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS R WITH (NOLOCK) ON I.FTInvoiceNo = R.FTInvoiceNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENBarcode_IN AS O WITH (NOLOCK) ON R.FTDocRefNo = O.FTDocumentNo"
                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeNo "
                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS D WITH(NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo and B.FNHSysRawMatId = D.FNHSysRawMatId"
                _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(_Key) & "')"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Qry = " update B set  B.FNPriceTrans =  D.FNPriceSale  "
                    _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS R WITH (NOLOCK) ON I.FTInvoiceNo = R.FTInvoiceNo LEFT OUTER JOIN"
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENBarcode_OUT AS O WITH (NOLOCK) ON R.FTDocRefNo = O.FTDocumentNo"
                    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeNo "
                    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS D WITH(NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo and B.FNHSysRawMatId = D.FNHSysRawMatId"
                    _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(_Key) & "')"
                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If
            End If
        Catch ex As Exception
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Packing WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
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

        If Me.FNHSysCurId.Properties.Tag Is Nothing And Me.FNHSysCurId.Text <> "" Then
            Me.FNHSysCurId.Properties.Tag = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysCurId From TFINMCurrency WITH(NOLOCK) WHERE FTCurCode ='" & Me.FNHSysCurId.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
        End If

        If CheckDocumentAuto() Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                'FTDocRefNo.Properties.ReadOnly = CType(ogcdetail.DataSource, DataTable).Rows.Count > 0 '(ogvdetail.Rows.Count > 0)
                'FTDocRefNo.Properties.Buttons.Item(0).Enabled = Not (CType(ogcdetail.DataSource, DataTable).Rows.Count > 0)
                Call LoadDocRef(Me.FTInvoiceNo.Text)
                Call LoadDetail("")
                Call SaveData(False)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub
    Private Function CheckDocumentAuto() As Boolean
        Dim cmdstring As String = ""
        cmdstring = "select top 1 FTDocAutoRefNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS X WITH(NOLOCK) where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "'"

        If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> "" Then
            HI.MG.ShowMsg.mInfo("เอกสารมาจาการ Auto ไม่สารมารถทำการลบหรือแก้ไขได้ !!!", 1902109457, Me.Text,, MessageBoxIcon.Warning)
            Return True
        Else
            Return False
        End If


    End Function
    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub
        If CheckDocumentAuto() Then Exit Sub
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTInvoiceNo.Text, Me.Text) = False Then
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
        Me.FNHSysCurId.Text = ""
        Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        Me.FNExchangeRate.Value = 1
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTInvoiceNo.Text <> "" Then

            'With New HI.RP.Report
            '    .FormTitle = Me.Text
            '    .ReportFolderName = "Inventrory\"
            '    Select Case Me.FNInvoiceState.SelectedIndex
            '        Case 0, 2, 3
            '            '.ReportName = "ReportSaleInvoice.rpt"
            '        Case 1
            '            .ReportName = "ReportSaleInvoiceImport.rpt"
            '    End Select
            '    .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
            '    .Preview()
            'End With

            If Me.FNInvoiceState.SelectedIndex = 1 Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Inventrory\"
                    .ReportName = "ReportSaleInvoiceImport.rpt"
                    .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                    .Preview()
                End With

                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Inventrory\"
                    .ReportName = "ReportSaleInvoicePacking_Fabric.rpt"
                    .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                    .Preview()
                End With
            Else
                For Each R As String In Split("" & Me.FTReportInvoice.Text & "|" & Me.FTReportInvoiceCopy.Text & "|" & Me.FTReportInvoiceCopy2.Text & "", "|")
                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .AddParameter("ReportName", R.ToString)
                        .ReportFolderName = "Inventrory\"
                        .ReportName = "ReportSaleInvoiceDomestic.rpt"
                        .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                        .Preview()
                    End With
                Next

                For Each R As String In Split("" & Me.FTReportBill.Text & "|" & Me.FTReportBillCopy.Text & "", "|")
                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .AddParameter("ReportName", R.ToString)
                        .ReportFolderName = "Inventrory\"
                        .ReportName = "ReportSaleInvoiceBill.rpt"
                        .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                        .Preview()
                    End With
                Next

            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTIssueNo_lbl.Text)
            FTInvoiceNo.Focus()
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

            'FTDocRefNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            'FTDocRefNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            '  FNExchangeRate.Properties.ReadOnly = (dt.Rows.Count > 0)

            FNHSysCmpIdTo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysCmpIdTo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNInvoiceState.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNInvoiceState.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            dt.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.ogvdocref.OptionsView.ShowAutoFilterRow = False
        AddHandler RepositoryFNHSysUnitId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        RemoveHandler FTDocRefNo.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
        RemoveHandler FTInvoiceNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler FTInvoiceNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        _FormLoad = False
    End Sub

    'Private Sub FTDocRefNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTDocRefNo.EditValueChanged
    '    Try

    '        If Me.FTDocRefNo.Text <> "" Then
    '            Me.LoadDetail(Me.FTDocRefNo.Text)
    '        Else
    '            Me.ogcdetail.DataSource = Nothing
    '        End If
    '        Call FNInvoiceState_SelectedIndexChanged(FNInvoiceState, New System.EventArgs)
    '        'Me.FNHSysCurId.Properties.Tag = 1310200002
    '        'Me.FNHSysCurId.Text = "THB"
    '        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '        '    Me.FNHSysCurId_None.Text = "บาท"
    '        'Else
    '        '    Me.FNHSysCurId_None.Text = "THB"
    '        'End If

    '    Catch ex As Exception
    '    End Try

    'End Sub

    Private Sub CalculateAmt(Dt As DataTable, Optional _StateEdit As String = "0")
        Dim _dtamt As New DataTable

        If Me.FNInvoiceState.SelectedIndex <> 1 Then
            _dtamt.Columns.Add("FNHSysRawMatId", GetType(Integer))
            _dtamt.Columns.Add("FNNetPrice", GetType(Double))
            _dtamt.Columns.Add("FNQuantity", GetType(Double))
        Else
            _dtamt.Columns.Add("FNHSysMatTypeId", GetType(Integer))
            _dtamt.Columns.Add("FTRawMatNameEN", GetType(String))
            _dtamt.Columns.Add("FNNetPrice", GetType(Double))
            _dtamt.Columns.Add("FNQuantity", GetType(Double))
        End If

        Dim _Amt As Double = 0
        Dim _NetPrice As Double = 0

        With Dt
            .BeginInit()

            For Each R As DataRow In .Rows
                'If Me.FNExchangeRate.Value > 0 Then
                '    R!FNPriceSale = CDbl(Format((Val(R!FNPrice.ToString)) / Val(Me.FNExchangeRate.Value), HI.ST.Config.PriceFormat))
                'End If

                'If Me.FNChargeService.Value > 0 Then
                '    R!FNChargeService = CDbl(Format(((Val(R!FNPriceSale.ToString)) * (Me.FNChargeService.Value)) / 100, HI.ST.Config.PriceFormat))
                'Else
                '    R!FNChargeService = 0
                'End If

                'If Me.FNChargeClear.Value > 0 Then
                '    R!FNChargeClear = CDbl(Format(((Val(R!FNPriceSale.ToString)) * (Me.FNChargeClear.Value)) / 100, HI.ST.Config.PriceFormat))
                'Else
                '    R!FNChargeClear = 0
                'End If

                If Me.FNExchangeRate.Value > 0 Then
                    R!FNPriceSale = CDbl(Format((Val(R!FNPrice.ToString)) / Val(Me.FNExchangeRate.Value), "0.0000"))
                End If

                If _StateEdit = "1" Or Double.Parse(R!FNChargeServiceState.ToString) <= -1 Then
                    If Me.FNChargeService.Value > 0 Then
                        R!FNChargeService = CDbl(Format(((Val(R!FNPriceSale.ToString)) * (Me.FNChargeService.Value)) / 100, "0.0000"))
                        ' R!FNChargeService = CDbl(Format(((Val(R!FNPriceSale.ToString)) * (R!FNChargeServicePer.ToString)) / 100, "0.0000"))
                        R!FNChargeServicePer = Me.FNChargeService.Value
                    Else
                        R!FNChargeService = 0
                        R!FNChargeServicePer = 0
                    End If
                Else
                    R!FNChargeService = Double.Parse(R!FNChargeServiceInv.ToString)
                    R!FNChargeServicePer = R!FNChargeServicePer
                End If

                If _StateEdit = "1" Or Double.Parse(R!FNChargeClear.ToString) <= -1 Then
                    If Me.FNChargeClear.Value > 0 Then
                        R!FNChargeClear = CDbl(Format(((Val(R!FNPriceSale.ToString)) * (Me.FNChargeClear.Value)) / 100, "0.0000"))
                        R!FNChargeClearPer = Me.FNChargeClear.Value
                    Else
                        R!FNChargeClear = 0
                        R!FNChargeClearPer = 0
                    End If
                Else
                    R!FNChargeClear = Double.Parse(R!FNChargeClearInv.ToString)
                End If


                R!FNNetPrice = Val(R!FNPriceSale.ToString) + Val(R!FNChargeService.ToString) + Val(R!FNChargeClear.ToString)
                R!FNAmount = CDbl(Format(Val(R!FNQuantity) * Val(R!FNNetPrice), HI.ST.Config.AmtFormat))

            Next

            .EndInit()
            .AcceptChanges()
            For Each R As DataRow In .Rows
                If Me.FNInvoiceState.SelectedIndex <> 1 Then
                    If _dtamt.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId) & " AND FNNetPrice=" & Val(R!FNNetPrice) & "").Length > 0 Then
                        For Each Rx As DataRow In _dtamt.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId) & " AND FNNetPrice=" & Val(R!FNNetPrice) & "")
                            Rx!FNQuantity = Val(Rx!FNQuantity) + Val(R!FNQuantity)
                        Next
                    Else
                        _dtamt.Rows.Add(Val(R!FNHSysRawMatId), Val(R!FNNetPrice), Val(R!FNQuantity))
                    End If
                Else
                    If _dtamt.Select("FNHSysMatTypeId=" & Val(R!FNHSysMatTypeId) & " AND FTRawMatNameEN='" & HI.UL.ULF.rpQuoted(R!FTRawMatNameEN.ToString) & "' AND FNNetPrice=" & Val(R!FNNetPrice) & "").Length > 0 Then
                        For Each Rx As DataRow In _dtamt.Select("FNHSysMatTypeId=" & Val(R!FNHSysMatTypeId) & " AND FNNetPrice=" & Val(R!FNNetPrice) & "")
                            Rx!FNQuantity = Val(Rx!FNQuantity) + Val(R!FNQuantity)
                        Next
                    Else
                        _dtamt.Rows.Add(Val(R!FNHSysMatTypeId), R!FTRawMatNameEN.ToString, Val(R!FNNetPrice), Val(R!FNQuantity))
                    End If
                End If
            Next
        End With
        _Amt = 0
        If Not (Me.ogcpackinglist.DataSource Is Nothing) Then
            Dim _ogvpackinglist As DataTable
            With CType(Me.ogcpackinglist.DataSource, DataTable)
                .AcceptChanges()
                _ogvpackinglist = .Copy
            End With
            '_ogvpackinglist.Columns.Add("FNChargeServicePer", GetType(String))
            For Each R As DataRow In _ogvpackinglist.Rows
                For Each Rx As DataRow In Dt.Select("FNHSysMatTypeId=" & Val(R!FNHSysMatTypeId) & " AND FTRawMatNameEN='" & HI.UL.ULF.rpQuoted(R!FTRawMatName.ToString) & "'")

                    R!FNPriceSale = Rx!FNPriceSale
                    R!FNChargeService = Rx!FNChargeService
                    R!FNChargeClear = Rx!FNChargeClear
                    R!FNNetPrice = Val(Rx!FNPriceSale.ToString) + Val(Rx!FNChargeService.ToString) + Val(Rx!FNChargeClear.ToString)
                    R!FNChargeServicePer = Rx!FNChargeServicePer
                    R!FNChargeClearPer = Rx!FNChargeClearPer
                    Exit For
                Next
            Next

            For Each R As DataRow In _ogvpackinglist.Rows
                For Each Rx As DataRow In Dt.Select("FNHSysMatTypeId=" & Val(R!FNHSysMatTypeId) & " AND FTRawMatNameEN='" & HI.UL.ULF.rpQuoted(R!FTRawMatName.ToString) & "'")
                    R!FNQuantity = Val(R!FNQuantity) + Val(Rx!FNQuantity)
                Next
            Next

            For Each R As DataRow In _ogvpackinglist.Rows
                R!FNAmount = CDbl(Format(Val(R!FNQuantity) * Val(R!FNNetPrice), HI.ST.Config.AmtFormat))
            Next

            Me.ogcpackinglist.DataSource = _ogvpackinglist.Copy

        End If

        'For Each R As DataRow In Dt.Rows
        '    _Amt = _Amt + CDbl(Val(R!FNAmount.ToString))
        'Next

        For Each R As DataRow In _dtamt.Rows
            Dim _tmpamt As Double
            _tmpamt = CDbl(Format(Val(R!FNQuantity) * Val(R!FNNetPrice), HI.ST.Config.AmtFormat))
            _Amt = _Amt + _tmpamt
        Next

        Me.FNInvAmt.Value = _Amt
        Call Calculate(FNInvAmt, Nothing)
        Me.ogcdetail.DataSource = Dt

    End Sub

    Private Sub LoadDocRef(ByVal _DocRefNo As String)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = "  Select FTDocRefNo AS FTDocumentNo"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "' "
        _Qry &= vbCrLf & " ORDER BY FTDocRefNo "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        Me.ogcdocref.DataSource = _dt

    End Sub

    Private Sub LoadPODocRef(ByVal _DocRefNo As String)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = "  Select FTDocRefNo AS FTDocumentNo"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "' "
        _Qry &= vbCrLf & " ORDER BY FTDocRefNo "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        Me.ogcdocref.DataSource = _dt

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
                    If R!FTDocumentNo.ToString <> "" Then
                        If _DocNo = "" Then
                            _DocNo = R!FTDocumentNo.ToString
                        Else
                            _DocNo = _DocNo & "','" & R!FTDocumentNo.ToString
                        End If
                    End If
                Next
            End If

            _dtdoc.Dispose()
            '20160107 Group Rawmat not validate OrderNO, PurchaseNo
            _Qry = "SELECT     TT.FTBarcodeNo, TT.FTOrderNo,SUM(Isnull(B_1.FNQuantity ,TT.FNQuantity )) as FNQuantity , TT.FNHSysCmpId, TT.FNHSysWHId, TT.FTDocumentNo, max(TT.FTFabricFrontSize) AS FTFabricFrontSize, TT.FTPurchaseNo,SUM(Isnull(B_1.FNQuantity ,TT.FNQuantity )) * TT.FNPrice AS FNAmount, TT.FNHSysRawMatId, "
            _Qry &= vbCrLf & "  TT.FTRawMatCode, TT.FTRawMatNameTH, TT.FTRawMatNameEN, TT.FTRawMatColorCode, TT.FTRawMatSizeCode, TT.FTRawMatSizeNameTH, TT.FTRawMatSizeNameEN, TT.FNHSysUnitId, "
            _Qry &= vbCrLf & "   TT.FTUnitNameTH, TT.FTUnitNameEN, TT.FNPrice, TT.FTRawMatName, max(TT.FNPriceSale) AS FNPriceSale , TT.FNChargeService, TT.FNChargeClear, max(TT.FNNetPrice) AS FNNetPrice, TT.FNChargeServicePer, TT.FNChargeClearPer, "
            _Qry &= vbCrLf & "  TT.FNMerMatType, TT.FNHSysUnitId_Hide, max(TT.FNPriceOrg) AS FNPriceOrg , TT.FNHSysUnitIdSale, TT.FNConvRatio, TT.FNHSysMatTypeId, TT.FTRawMatColorNameEN, TT.FNCTN, TT.FNNW, TT.FNGW, TT.FNQBM, "
            _Qry &= vbCrLf & "    TT.FNGrpSeq, TT.FNChargeServiceState, TT.FNChargeClearState, TT.FNChargeServiceInv, TT.FNChargeClearInv"
            _Qry &= vbCrLf & " From ( SELECT     FTBarcodeNo, '' as FTOrderNo, SUM(FNQuantity) AS FNQuantity, FNHSysCmpId, FNHSysWHId, FTDocumentNo,max(FTFabricFrontSize) AS  FTFabricFrontSize, MAX(FTPurchaseNo) AS FTPurchaseNo, SUM(FNAmount) "
            _Qry &= vbCrLf & " AS FNAmount, FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FTRawMatColorCode, FTRawMatSizeCode, FTRawMatSizeNameTH, FTRawMatSizeNameEN, "
            _Qry &= vbCrLf & "FNHSysUnitId, FTUnitNameTH, FTUnitNameEN, FNPrice, FTRawMatName,max(FNPriceSale) AS  FNPriceSale, FNChargeService, FNChargeClear,max(FNNetPrice) AS  FNNetPrice, FNChargeServicePer, FNChargeClearPer, FNMerMatType, "
            _Qry &= vbCrLf & "FNHSysUnitId_Hide,max(FNPriceOrg) AS  FNPriceOrg, FNHSysUnitIdSale, FNConvRatio, FNHSysMatTypeId, FTRawMatColorNameEN, FNCTN, FNNW, FNGW, FNQBM, FNGrpSeq, FNChargeServiceState, "
            _Qry &= vbCrLf & " FNChargeClearState, FNChargeServiceInv, FNChargeClearInv"
            _Qry &= vbCrLf & " From ( SELECT      '' as FTBarcodeNo, BO.FTOrderNo,  BO.FNQuantity  as FNQuantity, BO.FNHSysCmpId, BO.FNHSysWHId,'' as  FTDocumentNo, BR.FTFabricFrontSize, BR.FTPurchaseNo, CONVERT(numeric(18, 4), "
            _Qry &= vbCrLf & "	          Isnull(B.FNQuantity, BO.FNQuantity) * BR.FNPrice) AS FNAmount, BR.FNHSysRawMatId, MM.FTRawMatCode, MM.FTRawMatNameTH,MM.FTRawMatNameEN , MC.FTRawMatColorCode, "
            _Qry &= vbCrLf & "	    MS.FTRawMatSizeCode, MS.FTRawMatSizeNameTH, MS.FTRawMatSizeNameEN, MU.FTUnitCode as FNHSysUnitId, MU.FTUnitNameTH, MU.FTUnitNameEN"

            Select Case Me.FNInvoiceState.SelectedIndex
                Case 2, 3
                    _Qry &= vbCrLf & ", Isnull(B.FNPriceSale , Isnull(MMP.FNPrice ,0))  as FNPrice"
                Case 0
                    _Qry &= vbCrLf & ", ISNULL(PO2.FNPrice,Isnull(B.FNPriceSale , Isnull(MMP.FNPrice ,0)))  as FNPrice"
                Case Else
                    _Qry &= vbCrLf & ", Isnull(B.FNPriceSale ,  BR.FNPrice)  as FNPrice"
            End Select

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", Isnull(MD.FTMainMatNameTH, MM.FTRawMatNameTH) as FTRawMatName"
            Else
                _Qry &= vbCrLf & ", Isnull(MD.FTMainMatNameEN, MM.FTRawMatNameEN) as FTRawMatName"
            End If

            _Qry &= vbCrLf & ", 0.0000  AS FNPriceSale,Isnull(B.FNChargeService,0) AS  FNChargeService,Isnull(B.FNChargeClear,0) AS  FNChargeClear, BR.FNPrice  AS  FNNetPrice ,Isnull(B.FNChargeServicePer, 0) AS FNChargeServicePer ,Isnull(B.FNChargeClearPer,0) AS FNChargeClearPer "
            _Qry &= vbCrLf & ",ISNULL(MMM.FNMerMatType,0) AS FNMerMatType "
            _Qry &= vbCrLf & ",BR.FNHSysUnitId as FNHSysUnitId_Hide"
            _Qry &= vbCrLf & ",BR.FNPrice AS FNPriceOrg"
            _Qry &= vbCrLf & ",CASE WHEN MMM.FNMerMatType = 0 THEN  ISNULL((SELECT TOP 1 FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) WHERE FTUnitCode in ('YDS')),0)  ELSE BR.FNHSysUnitId END AS FNHSysUnitIdSale"
            _Qry &= vbCrLf & ", 1.00000 AS FNConvRatio"
            _Qry &= vbCrLf & ",ISNULL(MMM.FNHSysMatTypeId,0) AS FNHSysMatTypeId "
            _Qry &= vbCrLf & ",  MC.FTRawMatColorNameEN as FTRawMatColorNameEN"
            _Qry &= vbCrLf & ",Isnull(B.FNCTN,0) AS FNCTN"
            _Qry &= vbCrLf & ", Isnull(B.FNNW,0) AS FNNW"
            _Qry &= vbCrLf & ",Isnull(B.FNGW,0) AS FNGW"
            _Qry &= vbCrLf & ",0.0000 AS FNQBM,0 AS FNGrpSeq   ,Isnull(B.FNChargeService,-1) AS FNChargeServiceState, Isnull(B.FNChargeClear,-1) AS FNChargeClearState  "
            _Qry &= vbCrLf & " , Isnull(B.FNChargeService,0) AS FNChargeServiceInv  ,Isnull( B.FNChargeClear,0) AS FNChargeClearInv "

            _Qry &= vbCrLf & "	FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BR WITH (NOLOCK) ON BO.FTBarcodeNo = BR.FTBarcodeNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS MM WITH (NOLOCK) ON BR.FNHSysRawMatId = MM.FNHSysRawMatId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON MM.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS WITH (NOLOCK) ON MM.FNHSysRawMatSizeId = MS.FNHSysRawMatSizeId  "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MMM WITH(NOLOCK) ON MM.FTRawMatCode = MMM.FTMainMatCode "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_Description AS MD WITH(NOLOCK) ON MMM.FNHSysMainMatId = MD.FNHSysMainMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN (Select * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPriceMatType  WITH(NOLOCK) WHERE FNInvoiceState='" & Me.FNInvoiceState.SelectedIndex & "' AND FTStateActive = '1' ) AS MMP  ON MMM.FNHSysMainMatId = MMP.FNHSysMainMatId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN "
            _Qry &= vbCrLf & " ( SELECT FNGrpSeq ,FNHSysRawMatId, FNPrice, FNQuantity, FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice, FNHSysUnitId,  FNCTN, FNNW, FNGW, FNQBM , Isnull(FNChargeServicePer,0) AS FNChargeServicePer , Isnull(FNChargeClearPer,0) AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
            _Qry &= vbCrLf & " ) AS B ON BR.FNHSysRawMatId = B.FNHSysRawMatId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS MU WITH (NOLOCK) ON Isnull(B.FNHSysUnitId,BR.FNHSysUnitId) = MU.FNHSysUnitId"


            _Qry &= vbCrLf & "   OUTER APPLY ( SELECT TOP 1  PF.FNPrice  "
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TWH WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS PF WITH(NOLOCK) ON TWH.FTFacPurchaseNo = PF.FTFacPurchaseNo"
            _Qry &= vbCrLf & "  WHERE  (PF.FTOrderNo =BO.FTOrderNo) "
            _Qry &= vbCrLf & "         And (PF.FNHSysRawMatId =  BR.FNHSysRawMatId) "
            _Qry &= vbCrLf & "         And (TWH.FTTransferWHNo  in ('" & _DocNo & "')) "
            _Qry &= vbCrLf & "  ) AS PO2  "

            _Qry &= vbCrLf & "WHERE BO.FTDocumentNo in ('" & _DocNo & "') ) AS T "
            _Qry &= vbCrLf & "GROUP BY FTBarcodeNo,   FNHSysCmpId, FNHSysWHId, FTDocumentNo,  FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FTRawMatColorCode, " 'FTFabricFrontSize,
            _Qry &= vbCrLf & " FTRawMatSizeCode, FTRawMatSizeNameTH, FTRawMatSizeNameEN, FNHSysUnitId, FTUnitNameTH, FTUnitNameEN, FNPrice, FTRawMatName,  FNChargeService, FNChargeClear, "
            _Qry &= vbCrLf & "  FNChargeServicePer, FNChargeClearPer, FNMerMatType, FNHSysUnitId_Hide,  FNHSysUnitIdSale, FNConvRatio, FNHSysMatTypeId, FTRawMatColorNameEN, FNCTN, "
            _Qry &= vbCrLf & " FNNW, FNGW, FNQBM, FNGrpSeq, FNChargeServiceState, FNChargeClearState, FNChargeServiceInv, FNChargeClearInv ) AS TT "
            _Qry &= vbCrLf & " LEFT OUTER JOIN "
            _Qry &= vbCrLf & "   (SELECT     FNGrpSeq, FNHSysRawMatId, FNPrice, FNQuantity, FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice, FNHSysUnitId, FNCTN, FNNW, FNGW, FNQBM, "
            _Qry &= vbCrLf & "  ISNULL(FNChargeServicePer, 0) AS FNChargeServicePer, ISNULL(FNChargeClearPer, 0) AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTSaleInvoice_Detail AS A WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE      (FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "')) AS B_1 ON TT.FNHSysRawMatId = B_1.FNHSysRawMatId"
            '20160107 Group Rawmat not validate OrderNO, PurchaseNo
            _Qry &= vbCrLf & "Group by      TT.FTBarcodeNo, TT.FTOrderNo,  TT.FNHSysCmpId, TT.FNHSysWHId, TT.FTDocumentNo, TT.FTPurchaseNo,   TT.FNHSysRawMatId, " ', TT.FTFabricFrontSize

            _Qry &= vbCrLf & "TT.FTRawMatCode, TT.FTRawMatNameTH, TT.FTRawMatNameEN, TT.FTRawMatColorCode, TT.FTRawMatSizeCode, TT.FTRawMatSizeNameTH, TT.FTRawMatSizeNameEN, TT.FNHSysUnitId,"
            _Qry &= vbCrLf & " TT.FTUnitNameTH, TT.FTUnitNameEN, TT.FNPrice, TT.FTRawMatName,  TT.FNChargeService, TT.FNChargeClear,  TT.FNChargeServicePer, TT.FNChargeClearPer,"
            _Qry &= vbCrLf & " TT.FNMerMatType, TT.FNHSysUnitId_Hide,  TT.FNHSysUnitIdSale, TT.FNConvRatio, TT.FNHSysMatTypeId, TT.FTRawMatColorNameEN, TT.FNCTN, TT.FNNW, TT.FNGW, TT.FNQBM,"
            _Qry &= vbCrLf & " TT.FNGrpSeq, TT.FNChargeServiceState, TT.FNChargeClearState, TT.FNChargeServiceInv, TT.FNChargeClearInv"
            'TT.FNPriceSale,TT.FNPriceOrg,TT.FNNetPrice,

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            If FNInvoiceState.SelectedIndex = 1 Then

                Dim _FNConvRatio As Double = 1
                Dim _SysUnitStock As Integer = 0
                Dim _SysUnitStockSale As Integer = 0

                If _oDt.Select("FNHSysUnitIdSale<=0").Length > 0 Then
                    _oDt.Rows.Clear()
                End If

                For Each R As DataRow In _oDt.Select("FNMerMatType=0")

                    _SysUnitStock = Integer.Parse(Val(R!FNHSysUnitId_Hide.ToString))
                    _SysUnitStockSale = Integer.Parse(Val(R!FNHSysUnitIdSale.ToString))

                    If Integer.Parse(_SysUnitStock) <> Integer.Parse(_SysUnitStockSale) Then

                        _Qry = " SELECT      TOP 1   Convert(numeric(18,5),FNRateFrom * FNRateTo)  As  FNConvRatio "
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                        _Qry &= vbCrLf & "  WHERE FNHSysUnitId =" & Integer.Parse(_SysUnitStock) & " "
                        _Qry &= vbCrLf & "  AND FNHSysUnitIdTo =" & Integer.Parse(_SysUnitStockSale) & " "

                        _FNConvRatio = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "1"))

                        R!FNHSysUnitId_Hide = _SysUnitStockSale
                        R!FNHSysUnitId = "YDS"
                        R!FNQuantity = CDbl(Format((Val(R!FNQuantity) * _FNConvRatio), HI.ST.Config.QtyFormat))
                        'R!FNPrice = CDbl(Format((Val(R!FNPrice.ToString)) / Val(_FNConvRatio), HI.ST.Config.PriceFormat))

                        'R!FNNetPrice = CDbl(Format((Val(R!FNPrice.ToString)) / Val(_FNConvRatio), HI.ST.Config.PriceFormat))
                        'R!FNPriceOrg = CDbl(Format((Val(R!FNPrice.ToString)) / Val(_FNConvRatio), HI.ST.Config.PriceFormat))

                        R!FNPrice = CDbl(Format((Val(R!FNPrice.ToString)) / Val(_FNConvRatio), "0.0000"))

                        R!FNNetPrice = CDbl(Format((Val(R!FNPrice.ToString)) / Val(_FNConvRatio), "0.0000"))
                        R!FNPriceOrg = CDbl(Format((Val(R!FNPrice.ToString)) / Val(_FNConvRatio), "0.0000"))

                    End If
                Next

            End If

            _Qry = " SELECT A.FTRawMatNameEN AS FTRawMatNameEN,max(A.FTRawMatName) AS FTRawMatName,A.FNMerMatType,A.FNHSysMatTypeId  , A.FNPriceSale   ,A.FNChargeClear,A.FNChargeService,A.FNNetPrice,0.00 AS FNAmount "
            _Qry &= vbCrLf & ",Max(U.FTUnitCode) AS FTUnitCode"
            _Qry &= vbCrLf & ",0.00  AS FNQuantity"
            _Qry &= vbCrLf & ",ISNULL(B.FNCTN,0) AS FNCTN"
            _Qry &= vbCrLf & ",ISNULL(B.FNNW,0) AS FNNW"
            _Qry &= vbCrLf & ",ISNULL(B.FNGW,0) AS FNGW"
            _Qry &= vbCrLf & ",ISNULL(B.FNQBM,0) AS FNQBM"
            _Qry &= vbCrLf & ",Isnull(B.FNChargeServicePer,0) AS FNChargeServicePer  , Isnull(B.FNChargeClearPer,0) AS FNChargeClearPer , A.FTRawMatCode , max(A.FNHSysRawMatId) AS FNHSysRawMatId"
            _Qry &= vbCrLf & "FROM 	 ( SELECT     BO.FTBarcodeNo, BO.FTOrderNo, BO.FNQuantity, BO.FNHSysCmpId, BO.FNHSysWHId, BO.FTDocumentNo, BR.FTFabricFrontSize, BR.FTPurchaseNo,   BR.FNPrice, CONVERT(numeric(18, 4), "
            _Qry &= vbCrLf & "	            BO.FNQuantity * BR.FNPrice) AS FNAmount, BR.FNHSysRawMatId, MM.FTRawMatCode, MM.FTRawMatNameTH, Isnull(MD.FTMainMatNameEN ,MM.FTRawMatNameEN) AS FTRawMatNameEN ,MM.FTRawMatNameEN AS FTRawMatName , MC.FTRawMatColorCode, "
            _Qry &= vbCrLf & "	    MS.FTRawMatSizeCode, MS.FTRawMatSizeNameTH, MS.FTRawMatSizeNameEN, MU.FTUnitCode, MU.FTUnitNameTH, MU.FTUnitNameEN"
            _Qry &= vbCrLf & ", 0.0000  AS FNPriceSale,0.00 AS  FNChargeService,0.00 FNChargeClear,BR.FNPrice AS  FNNetPrice,0.00 AS  FNChargeServicePer"
            _Qry &= vbCrLf & ",ISNULL(MMM.FNMerMatType,0) AS FNMerMatType "
            _Qry &= vbCrLf & ",BR.FNHSysUnitId"
            _Qry &= vbCrLf & ",BR.FNPrice AS FNPriceOrg"
            _Qry &= vbCrLf & ",CASE WHEN MMM.FNMerMatType = 0 THEN  ISNULL((SELECT TOP 1 FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) WHERE FTUnitCode in ('YDS')),0)  ELSE BR.FNHSysUnitId END AS FNHSysUnitIdSale"
            _Qry &= vbCrLf & ", 1.00000 AS FNConvRatio"
            _Qry &= vbCrLf & ",ISNULL(MMM.FNHSysMatTypeId,0) AS FNHSysMatTypeId "
            _Qry &= vbCrLf & ",  MC.FTRawMatColorNameEN as FTRawMatColorNameEN"
            _Qry &= vbCrLf & "	FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BR WITH (NOLOCK) ON BO.FTBarcodeNo = BR.FTBarcodeNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS MM WITH (NOLOCK) ON BR.FNHSysRawMatId = MM.FNHSysRawMatId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON MM.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS WITH (NOLOCK) ON MM.FNHSysRawMatSizeId = MS.FNHSysRawMatSizeId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS MU WITH (NOLOCK) ON BR.FNHSysUnitId = MU.FNHSysUnitId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MMM WITH(NOLOCK) ON MM.FTRawMatCode = MMM.FTMainMatCode "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_Description AS MD WITH(NOLOCK) ON MMM.FNHSysMainMatId = MD.FNHSysMainMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPriceMatType AS MMP WITH(NOLOCK) ON MMM.FNHSysMainMatId = MMP.FNHSysMainMatId "

            _Qry &= vbCrLf & "WHERE BO.FTDocumentNo in ('" & _DocNo & "')"
            _Qry &= vbCrLf & " ) AS A  LEFT OUTER JOIN"
            _Qry &= vbCrLf & " ( SELECT FNGrpSeq ,FNHSysRawMatId, FNPrice, FNQuantity, FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice, FNHSysUnitId,  FNCTN, FNNW, FNGW, FNQBM , Isnull(FNChargeServicePer,0) AS FNChargeServicePer , Isnull(FNChargeClearPer,0) AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
            _Qry &= vbCrLf & " ) AS B ON A.FNHSysRawMatId = B.FNHSysRawMatId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitIdSale =U.FNHSysUnitId  "
            _Qry &= vbCrLf & " GROUP BY B.FNGrpSeq,  A.FTRawMatNameEN,A.FNMerMatType,A.FNHSysMatTypeId,A.FNPriceSale,A.FNChargeClear,A.FNChargeService,A.FNNetPrice,A.FNHSysUnitIdSale"
            _Qry &= vbCrLf & ",ISNULL(B.FNCTN,0)"
            _Qry &= vbCrLf & ",ISNULL(B.FNNW,0)"
            _Qry &= vbCrLf & ",ISNULL(B.FNGW,0)"
            _Qry &= vbCrLf & ",ISNULL(B.FNQBM,0) ,B.FNChargeServicePer , B.FNChargeClearPer, A.FTRawMatCode  "
            _Qry &= vbCrLf & "ORDER BY  A.FNMerMatType , B.FNGrpSeq "

            _dtgrp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            CreateTabFabric(_dtgrp)
            _oDtPacking = _dtgrp.Copy
            Me.ogcpackinglist.DataSource = _dtgrp.Copy
            Call CalculateAmt(_oDt)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateTabFabric(_oDt As DataTable)
        Try
            Me.oCTabPacking.TabPages.Clear()
            Me.oCTabPackingAcc.TabPages.Clear()
          
            For Each R As DataRow In _oDt.Select("FNMerMatType=0")
                'Me.oCTabPacking.TabPages.Add(Microsoft.VisualBasic.Left(R!FTRawMatNameEN.ToString, 20))
                Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
                Dim _Grid As New DevExpress.XtraGrid.GridControl
                With _TabPage
                    .Name = "otb" & R!FTRawMatCode.ToString
                    .Text = Microsoft.VisualBasic.Left(R!FTRawMatNameEN.ToString, 20)
                    .Tag = "2|"
                End With
                With _Grid
                    .Name = R!FNHSysRawMatId.ToString

                End With

                Dim _GridV As New DevExpress.XtraGrid.Views.Grid.GridView
                _GridV.Name = "ogv" & R!FTRawMatCode.ToString
             
                Call SetGrid(_GridV, R!FNMerMatType.ToString)

                _Grid.BeginInit()
                _Grid.MainView = _GridV
                _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
                AddHandler _GridV.KeyDown, AddressOf ogvTmp_KeyDown
                _Grid.EndInit()

                _TabPage.Controls.Add(_Grid)
                _Grid.Dock = DockStyle.Fill
                _Grid.DataSource = GetDataPacking(Me.FTInvoiceNo.Text, R!FNHSysRawMatId.ToString)
                Me.oCTabPacking.TabPages.Add(_TabPage)
            Next

            For Each R As DataRow In _oDt.Select("FNMerMatType=1")
                'Me.oCTabPacking.TabPages.Add(Microsoft.VisualBasic.Left(R!FTRawMatNameEN.ToString, 20))
                Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
                Dim _Grid As New DevExpress.XtraGrid.GridControl
                With _TabPage
                    .Name = "otb" & R!FTRawMatCode.ToString
                    .Text = Microsoft.VisualBasic.Left(R!FTRawMatNameEN.ToString, 20)
                    .Tag = "2|"
                End With
                With _Grid
                    .Name = R!FNHSysRawMatId.ToString

                End With

                Dim _GridV As New DevExpress.XtraGrid.Views.Grid.GridView
                _GridV.Name = "ogv" & R!FTRawMatCode.ToString
                _GridV.Tag = "2|"

                Call SetGrid(_GridV, R!FNMerMatType.ToString)

                _Grid.BeginInit()
                _Grid.MainView = _GridV
                _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
                AddHandler _GridV.KeyDown, AddressOf ogvTmp_KeyDown

                _Grid.EndInit()

                _TabPage.Controls.Add(_Grid)
                _Grid.Dock = DockStyle.Fill
                _Grid.DataSource = GetDataPacking(Me.FTInvoiceNo.Text, R!FNHSysRawMatId.ToString)
                Me.oCTabPackingAcc.TabPages.Add(_TabPage)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Function GetDataPacking(_InvoiceNo As String, _RawMatId As Integer) As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT     FTInvoiceNo, FNHSysRawMatId, FNRollNo, FNRollToNo, FNQtyPerRoll, FNQtyCTN, FNTotalQty, FNNetWeightPerRoll, FNGrossWeightPerRoll, FNTotalNetWeight, FNTotalGrossWeight"
            _Cmd &= vbCrLf & "FROM         TACCTSaleInvoice_Packing WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "'"
            _Cmd &= vbCrLf & "AND FNHSysRawMatId=" & Integer.Parse(_RawMatId)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            _oDt.Rows.Add()
            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub SetGrid(ByVal _orgGrid As DevExpress.XtraGrid.Views.Grid.GridView, _Type As String)
        Try
            _orgGrid.BeginInit()
            Dim sFieldSum As String = "FNQtyCTN|FNTotalQty|FNTotalNetWeight|FNTotalGrossWeight"
            With _orgGrid
                .OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True
                .OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True
                .OptionsView.ColumnAutoWidth = False
            End With

            For Each Col As DevExpress.XtraGrid.Columns.GridColumn In IIf(_Type = 0, Me.ogvTmp.Columns, Me.ogvAccTmp.Columns)

                Dim _nCol As New DevExpress.XtraGrid.Columns.GridColumn

                With _nCol
                    .Name = _orgGrid.Name & Col.Name.ToString
                    .FieldName = Col.FieldName.ToString
                    .Caption = Col.Caption.ToString
                    .Tag = Col.Tag.ToString
                    .OptionsFilter.AllowAutoFilter = False
                    .OptionsFilter.AllowFilter = False
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = Col.DisplayFormat.FormatString

                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    .ColumnEdit = CreateRetoyCalEdit(_orgGrid.Name.ToString & "" & Col.FieldName.ToString, _orgGrid)
                    With .OptionsColumn
                        .AllowMove = False
                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .AllowEdit = True
                        .ReadOnly = False

                    End With
                    .Visible = True
                    .Width = Col.Width
                End With


                _orgGrid.Columns.Add(_nCol)
            Next

            With _orgGrid
                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                    End If
                Next
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            End With
            _orgGrid.EndInit()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                    FNDisCountAmt.EditValueChanged,
                                                                                    FNVatPer.EditValueChanged,
                                                                                    FNVatAmt.EditValueChanged,
                                                                                    FNSurcharge.EditValueChanged

        Static _Proc As Boolean

        If Not (_Proc) And Not (_ProcLoad) Then
            _Proc = True
            Dim _POAmt As Double = FNInvAmt.Value

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

            Me.FNInvNetAmt.Value = (_POAmt - _DisAmt)

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            FNInvGrandAmt.Value = Format(Me.FNInvNetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)

            _Proc = False
        End If
    End Sub

    Private Sub FNInvGrandAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNInvGrandAmt.EditValueChanged
        Try
            If Not (_ProcLoad) Then
                Me.FTInvGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNInvGrandAmt.Value)
                Me.FTInvGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNInvGrandAmt.Value)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub FNInvoiceState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNInvoiceState.SelectedIndexChanged
        Try
            _SysDocType = Me.FNInvoiceState.SelectedIndex
            Select Case Me.FNInvoiceState.SelectedIndex
                Case 0
                    'ในประเทศ
                    'Me.FNHSysCurId.Properties.Tag = 1310200002
                    Me.FNHSysCurId.Text = ""
                    Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
                    Me.FNExchangeRate.Value = 1
                    Me.FNHSysCurId.Enabled = False
                    Me.otpdomestic.PageVisible = True
                    Me.otpexport.PageVisible = False
                    Me.FDInvoiceDate.Properties.ReadOnly = False
                    Me.otbm.SelectedTabPageIndex = 1
                    Me.otabPackingAcc.PageVisible = False
                    Me.otabPackingFab.PageVisible = False
                Case 1
                    'ต่างประเทศ
                    Me.FNHSysCurId.Text = ""
                    Me.FNHSysCurId.Enabled = True

                    Me.otpdomestic.PageVisible = False
                    Me.otpexport.PageVisible = True
                    Me.FDInvoiceDate.Properties.ReadOnly = True
                    Me.otbm.SelectedTabPageIndex = 0
                    Me.otabPackingAcc.PageVisible = True
                    Me.otabPackingFab.PageVisible = True
                Case 2
                    'สาขา
                    Me.FNHSysCurId.Text = ""
                    Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
                    Me.FNExchangeRate.Value = 1
                    Me.FNHSysCurId.Enabled = False
                    Me.otpdomestic.PageVisible = True
                    Me.otpexport.PageVisible = False
                    Me.FDInvoiceDate.Properties.ReadOnly = True
                    Me.otbm.SelectedTabPageIndex = 1
                    Me.otabPackingAcc.PageVisible = False
                    Me.otabPackingFab.PageVisible = False
                Case 3
                    'ส่วนกลาง
                    Me.FNHSysCurId.Text = ""
                    Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
                    Me.FNExchangeRate.Value = 1
                    Me.FNHSysCurId.Enabled = False
                    Me.otpdomestic.PageVisible = True
                    Me.otpexport.PageVisible = False
                    Me.FDInvoiceDate.Properties.ReadOnly = True
                    Me.otbm.SelectedTabPageIndex = 1
                    Me.otabPackingAcc.PageVisible = False
                    Me.otabPackingFab.PageVisible = False
            End Select


            Call LoadDetail(FTDocRefNo.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNHSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCurId.EditValueChanged
        If _FormLoad Then Exit Sub
        If Me.FNHSysCurId.Properties.Tag Is Nothing Then
            Me.FNHSysCurId.Properties.Tag = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysCurId From TFINMCurrency WITH(NOLOCK) WHERE FTCurCode ='" & Me.FNHSysCurId.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
        End If

        If FNHSysCurId.Text = "" Then
            FNExchangeRate.Value = 1
            Exit Sub
        End If
        If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

            FNExchangeRate.Properties.ReadOnly = True

            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 1
            End If

        Else


            If Not (_ProcLoad) Then
                FNExchangeRate.Properties.ReadOnly = False
                FNExchangeRate.Value = 0
                Dim _Qry As String = ""

                _Qry = " SELECT TOP 1 FNSellingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDInvoiceDate.Text) & "')"
                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "'"
                _Qry &= vbCrLf & "  ))"

                FNExchangeRate.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

                If FNExchangeRate.Value <= 0 Then
                    FNExchangeRate.Value = 1
                End If

            End If

        End If
    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As Object, e As EventArgs) Handles FNExchangeRate.EditValueChanged, FNChargeService.EditValueChanged, FNChargeClear.EditValueChanged
        Try
            If Not (Me.ogcdetail.DataSource Is Nothing) Then
                Dim dt As DataTable
                With CType(Me.ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                Call CalculateAmt(dt, "1")
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

    Private Sub FTDocRefNo_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTDocRefNo.ButtonClick
        Try
            If Me.FTInvoiceNo.Text = "" Then
                Me.FTInvoiceNo.Focus()
                Exit Sub
            End If

            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _Where As String = "  FTDocumentNo not in(SELECT     FTDocRefNo  FROM  [HITECH_ACCOUNT]..TACCTSaleInvoice_DocRef WITH(NOLOCK) WHERE     (FTInvoiceNo <> N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'))"
            _Where &= vbCrLf & "  And FNHSysCmpIdTo=" & Integer.Parse("0" & Me.FNHSysCmpIdTo.Properties.Tag)


            With New wDynamicBrowseSelectInfo(e.Button.Tag.ToString, _Where)
                .Proc = False

                .ShowDialog()

                If Not (.Proc) Then Exit Sub
                'If .ogcbrowse.DataSource Is Nothing Then
                With CType(.ogcbrowse.DataSource, DataTable)
                    .AcceptChanges()
                    _oDt = .Copy
                End With

                Dim _dtdoc As DataTable

                If Me.ogcdocref.DataSource Is Nothing Then
                    Dim dt As New DataTable
                    dt.Columns.Add("FTDocumentNo", GetType(String))
                    Me.ogcdocref.DataSource = dt
                End If
                With CType(Me.ogcdocref.DataSource, DataTable)
                    .AcceptChanges()
                    _dtdoc = .Copy
                End With



                For Each R As DataRow In _oDt.Select("FTSelect = '1'")
                    _dtdoc.Rows.Add(R!FTDocumentNo.ToString)
                Next
                Me.ogcdocref.DataSource = _dtdoc
                Me.ogcdocref.Refresh()
                'End If



            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTDocRefNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTDocRefNo.EditValueChanged
    End Sub

    Private Sub FTDocRefNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTDocRefNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter

                    If FTDocRefNo.Text = "" Then Exit Sub
                    If FTDocRefNo.Properties.Tag.ToString = "" Then Exit Sub

                    Dim _dtdoc As DataTable
                    If Me.ogcdocref.DataSource Is Nothing Then
                        Dim dt As New DataTable
                        dt.Columns.Add("FTDocumentNo", GetType(String))
                        Me.ogcdocref.DataSource = dt
                    End If

                    With CType(Me.ogcdocref.DataSource, DataTable)
                        .AcceptChanges()
                        _dtdoc = .Copy
                    End With

                    If _dtdoc.Select("FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTDocRefNo.Text) & "'").Length <= 0 Then
                        _dtdoc.Rows.Add(FTDocRefNo.Text)
                    End If

                    Me.ogcdocref.DataSource = _dtdoc
                    Me.ogcdocref.Refresh()

                    FTDocRefNo.Text = ""
                    FTDocRefNo.Focus()
            End Select

        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryFNChargeServicePer_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNChargeServicePer.EditValueChanging
        Try
            Dim _ChargeService As Double = 0
            Dim _Clear, _Net, _Amt As Double
            With Me.ogvpackinglist
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                _ChargeService = CDbl(Format(((Val(.GetRowCellValue(.FocusedRowHandle, "FNPriceSale"))) * (e.NewValue)) / 100, "0.0000"))
                .SetRowCellValue(.FocusedRowHandle, "FNChargeService", _ChargeService)
                _Net = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNChargeClear").ToString) + Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNPriceSale").ToString) + _ChargeService
                .SetRowCellValue(.FocusedRowHandle, "FNNetPrice", _Net)
                _Amt = _Net * Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _Amt)

            End With

        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryFNChargeClearPer_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNChargeClearPer.EditValueChanging
        Try
            Dim _ChargeService As Double = 0
            Dim _Clear, _Net, _Amt As Double
            With Me.ogvpackinglist
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                _ChargeService = CDbl(Format(((Val(.GetRowCellValue(.FocusedRowHandle, "FNPriceSale"))) * (e.NewValue)) / 100, "0.0000"))
                .SetRowCellValue(.FocusedRowHandle, "FNChargeClear", _ChargeService)
                _Net = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNChargeService").ToString) + Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNPriceSale").ToString) + _ChargeService
                .SetRowCellValue(.FocusedRowHandle, "FNNetPrice", _Net)
                _Amt = _Net * Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _Amt)

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryPriceSale_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryPriceSale.EditValueChanging
        Try
            Dim _Ser, _Clear, _Net, _Amt As Double
            With Me.ogvpackinglist
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                _Ser = CDbl(Format(((Val(.GetRowCellValue(.FocusedRowHandle, "FNChargeServicePer"))) * (e.NewValue)) / 100, "0.0000"))
                .SetRowCellValue(.FocusedRowHandle, "FNChargeService", _Ser)
                _Clear = CDbl(Format(((Val(.GetRowCellValue(.FocusedRowHandle, "FNChargeClearPer"))) * (e.NewValue)) / 100, "0.0000"))
                .SetRowCellValue(.FocusedRowHandle, "FNChargeClear", _Clear)
                _Net = _Ser + _Clear + e.NewValue
                .SetRowCellValue(.FocusedRowHandle, "FNNetPrice", _Net)
                _Amt = CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString) * _Net
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _Amt)

            End With


            _Amt = 0
            With DirectCast(Me.ogcpackinglist.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    Dim _tmpamt As Double
                    _tmpamt = CDbl(Format(Val(R!FNQuantity) * Val(R!FNNetPrice), HI.ST.Config.AmtFormat))
                    _Amt = _Amt + _tmpamt
                Next

                Me.FNInvAmt.Value = _Amt

            End With



        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryQuantity.EditValueChanging
        Try
            Dim _Amt As Double
            With Me.ogvpackinglist
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                _Amt = CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNNetPrice").ToString) * e.NewValue
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _Amt)

            End With


            _Amt = 0
            With DirectCast(Me.ogcpackinglist.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    Dim _tmpamt As Double
                    _tmpamt = CDbl(Format(Val(R!FNQuantity) * Val(R!FNNetPrice), HI.ST.Config.AmtFormat))
                    _Amt = _Amt + _tmpamt
                Next

                Me.FNInvAmt.Value = _Amt

            End With





        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTInvoiceNo.EditValueChanged
        Try
            Call LoadDataInfo(Me.FTInvoiceNo.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvTmp_HiddenEditor(sender As Object, e As EventArgs)
        Try
            With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                .AcceptChanges()
                Dim _oDt As DataTable = .Copy
                .Rows.Add()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvTmp_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Dim _RowSeq As Integer = 0
            Dim _Focus As Integer = 0
            Select Case e.KeyCode
                Case Keys.Down
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        _Focus = .FocusedRowHandle
                    End With
                    With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                        .AcceptChanges()
                        If _Focus = .Rows.Count - 1 Then
                            .Rows.Add()
                        End If
                        .AcceptChanges()
                    End With
                Case Keys.Enter
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        If .FocusedColumn.FieldName.ToString = "FNTotalGrossWeight" Then
                            _Focus = .FocusedRowHandle

                            If (.GetRowCellValue(.FocusedRowHandle, "FNRollToNo").ToString <> "" And .GetRowCellValue(.FocusedRowHandle, "FNRollNo").ToString <> "" _
                              And .GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString <> "" _
                              And .GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString <> "" _
                              And .GetRowCellValue(.FocusedRowHandle, "FNTotalQty").ToString <> "" _
                              And .GetRowCellValue(.FocusedRowHandle, "FNNetWeightPerRoll").ToString <> "" _
                              And .GetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll").ToString <> "" _
                              And .GetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight").ToString <> "" _
                              And .GetRowCellValue(.FocusedRowHandle, "FNTotalGrossWeight").ToString <> "") Then


                                With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                                    .AcceptChanges()
                                    If _Focus = .Rows.Count - 1 Then
                                        .Rows.Add()
                                    End If
                                    .AcceptChanges()
                                    Dim _oDt As DataTable = .Copy
                                    '    Call SaveData(_oDt, (CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).Name.ToString)
                                    _RowSeq = .Rows.Count
                                End With

                                .FocusedRowHandle = .FocusedRowHandle + 1
                                .FocusedColumn = .Columns.ColumnByFieldName("FNRollNo")
                            Else
                                Select Case True
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNRollNo").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNRollNo")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNRollToNo").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNRollToNo")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNQtyPerRoll")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNQtyCTN")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNTotalQty").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNTotalQty")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNNetWeightPerRoll").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNNetWeightPerRoll")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNGrossWeightPerRoll")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNTotalNetWeight")
                                        Exit Sub
                                    Case .GetRowCellValue(.FocusedRowHandle, "FNTotalGrossWeight").ToString = ""
                                        .FocusedRowHandle = .FocusedRowHandle
                                        .FocusedColumn = .Columns.ColumnByFieldName("FNTotalGrossWeight")
                                        Exit Sub
                                End Select
                            End If

                        End If
                    End With
                    'Case Keys.Tab
                    '    With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                    '        .AcceptChanges()
                    '    End With
                    '    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                    '        Select Case True
                    '            Case .FocusedColumn.FieldName.ToString = "FNQtyCTN"
                    '                Dim _Value As Double
                    '                _Value = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString) * Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString)
                    '                .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", _Value)

                    '            Case .FocusedColumn.FieldName.ToString = "FNQtyPerRoll"
                    '                Dim _Value As Double
                    '                _Value = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString)
                    '                .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", _Value)
                    '        End Select
                    '    End With

                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Overloads Function SaveData(_oDt As DataTable, RawMatId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _Seq As Integer = 0
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _oDt.Rows
                If IsNumeric(R!FNRollNo.ToString) And IsNumeric(R!FNRollToNo.ToString) And IsNumeric(R!FNNetWeightPerRoll.ToString) And IsNumeric(R!FNGrossWeightPerRoll.ToString) And IsNumeric(R!FNGrossWeightPerRoll.ToString) And IsNumeric(R!FNTotalNetWeight.ToString) And IsNumeric(R!FNTotalGrossWeight.ToString) Then
                    _Seq += +1
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Packing"
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUpdTime =" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FNQtyPerRoll=" & Double.Parse(R!FNQtyPerRoll.ToString)
                    _Cmd &= vbCrLf & ",FNQtyCTN=" & Double.Parse(R!FNQtyCTN.ToString)
                    _Cmd &= vbCrLf & ",FNTotalQty=" & Double.Parse(R!FNTotalQty.ToString)
                    _Cmd &= vbCrLf & ",FNNetWeightPerRoll=" & Double.Parse(R!FNNetWeightPerRoll.ToString)
                    _Cmd &= vbCrLf & ",FNGrossWeightPerRoll=" & Double.Parse(R!FNGrossWeightPerRoll.ToString)
                    _Cmd &= vbCrLf & ",FNTotalNetWeight=" & Double.Parse(R!FNTotalNetWeight.ToString)
                    _Cmd &= vbCrLf & ",FNTotalGrossWeight=" & Double.Parse(R!FNTotalGrossWeight.ToString)
                    _Cmd &= vbCrLf & ",FNRollNo=" & Integer.Parse(R!FNRollNo.ToString)
                    _Cmd &= vbCrLf & ",FNRollToNo=" & Integer.Parse(R!FNRollToNo.ToString)
                    _Cmd &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                    _Cmd &= vbCrLf & "AND FNHSysRawMatId=" & Integer.Parse(RawMatId)
                 
                    _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(_Seq)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Packing "
                        _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTInvoiceNo, FNHSysRawMatId, FNRollNo, FNRollToNo, FNQtyPerRoll, FNQtyCTN"
                        _Cmd &= ", FNTotalQty, FNNetWeightPerRoll, FNGrossWeightPerRoll, FNTotalNetWeight, FNTotalGrossWeight,FNSeq)"
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        _Cmd &= vbCrLf & "," & Integer.Parse(RawMatId)
                        _Cmd &= vbCrLf & "," & Integer.Parse(R!FNRollNo.ToString)
                        _Cmd &= vbCrLf & "," & Integer.Parse(R!FNRollToNo.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse(R!FNQtyPerRoll.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse(R!FNQtyCTN.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse(R!FNTotalQty.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse(R!FNNetWeightPerRoll.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse(R!FNGrossWeightPerRoll.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse(R!FNTotalNetWeight.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse(R!FNTotalGrossWeight.ToString)
                        _Cmd &= vbCrLf & "," & Integer.Parse(_Seq)

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If
                
                End If
            Next

            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Packing"
            _Cmd &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNHSysRawMatId=" & Integer.Parse(RawMatId)
            _Cmd &= vbCrLf & "AND FNSeq >" & Integer.Parse(_Seq)
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

    Private Sub GetGrid()
        Try
            Dim _name As String = ""
            Dim _FieldName As String = ""
            For Each oCol As DataRow In _oDtPacking.Rows
                _FieldName = oCol!FNHSysRawMatId.ToString
                For Each Obj As Object In oCTabPacking.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.GridControl
                            With CType(Obj, DevExpress.XtraGrid.GridControl)
                                CType(.DataSource, DataTable).AcceptChanges()
                                Call SaveData(CType(.DataSource, DataTable), .Name)
                            End With
                    End Select
                Next
                For Each Obj As Object In oCTabPackingAcc.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.GridControl
                            With CType(Obj, DevExpress.XtraGrid.GridControl)
                                CType(.DataSource, DataTable).AcceptChanges()
                                Call SaveData(CType(.DataSource, DataTable), .Name)
                            End With
                    End Select
                Next
            Next
        Catch ex As Exception
        End Try
    End Sub


    Private Function CreateRetoyCalEdit(_name As String, _ogv As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
        Try
            With RetoyCal
                .Name = "RepositoryItemCal" & _name
                .Precision = 2
                .Buttons.Item(0).Visible = False
                AddHandler .EditValueChanging, AddressOf RepositoryCal_EditValueChanging
            End With
            Return RetoyCal
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub RepositoryCal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            Dim _ChargeService As Double = 0

            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Select Case .FocusedColumn.FieldName.ToString
                    Case "FNQtyCTN"
                        .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString) * Double.Parse(e.NewValue))
                        .SetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNNetWeightPerRoll").ToString) * Double.Parse(e.NewValue))
                        .SetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll").ToString) * Double.Parse(e.NewValue))
                    Case "FNQtyPerRoll"
                        .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Double.Parse(e.NewValue))
                    Case "FNNetWeightPerRoll"
                        .SetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Double.Parse(e.NewValue))
                    Case "FNGrossWeightPerRoll"
                        .SetRowCellValue(.FocusedRowHandle, "FNTotalGrossWeight", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Double.Parse(e.NewValue))
                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryFNPriceSaleDetail_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNPriceSaleDetail.EditValueChanging
        Try
            Dim _FNChargeService, _FNChargeClear, _NetPrice As Double
            With Me.ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _FNChargeService = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNChargeService").ToString)
                _FNChargeClear = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNChargeClear").ToString)
                _NetPrice = Double.Parse(e.NewValue) + _FNChargeService + _FNChargeClear
                .SetRowCellValue(.FocusedRowHandle, "FNNetPrice", _NetPrice)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _NetPrice * Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString))
            End With


            Dim _Amt As Double = 0
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    Dim _tmpamt As Double
                    _tmpamt = CDbl(Format(Val(R!FNQuantity) * Val(R!FNNetPrice), HI.ST.Config.AmtFormat))
                    _Amt = _Amt + _tmpamt
                Next

                Me.FNInvAmt.Value = _Amt

            End With


        Catch ex As Exception
        End Try
    End Sub

 
    Private Sub RepositoryFNQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNQuantity.EditValueChanging
        Try
            Dim _NetPrice As Double
            With Me.ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _NetPrice = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNNetPrice").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _NetPrice * Double.Parse(e.NewValue))
            End With

            Dim _Amt As Double = 0
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    Dim _tmpamt As Double
                    _tmpamt = CDbl(Format(Val(R!FNQuantity) * Val(R!FNNetPrice), HI.ST.Config.AmtFormat))
                    _Amt = _Amt + _tmpamt
                Next

                Me.FNInvAmt.Value = _Amt

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogcdocref_Click(sender As Object, e As EventArgs) Handles ogcdocref.Click

    End Sub

    Private Sub FNHSysCmpIdTo_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpIdTo.EditValueChanged

    End Sub

    Private Sub ocmpreviewinv_Click(sender As Object, e As EventArgs) Handles ocmpreviewinv.Click
        If Me.FTInvoiceNo.Text <> "" Then


            For I As Integer = 1 To 5
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Inventrory\"
                    .ReportName = "ReportSaleInvoiceBill" & I.ToString & ".rpt"
                    .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                    .Preview()
                End With
            Next


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTIssueNo_lbl.Text)
            FTInvoiceNo.Focus()
        End If
    End Sub
End Class
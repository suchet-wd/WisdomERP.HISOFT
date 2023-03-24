Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls

Public Class wInvoice

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
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

            _objId = Integer.Parse(_dt.Rows(0) !FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0) !FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0) !FHSysTableName.ToString
            Me.TableName = _dt.Rows(0) !FTTableName.ToString

            _SortField = _dt.Rows(0) !FTSortField.ToString

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

        Call LoadDetail()

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
        Call LoadDetail()
        Me.FNHSysCurId.Text = ""
        Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        Me.FNExchangeRate.Value = 1
        _FormLoad = False
        Me.FNVatPer.Value = 7
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

        For Each R As DataRow In CType(Me.ogcdetail.DataSource, DataTable).Select("FNGrpSeq>0", "FNGrpSeq")
            If R!FTDescription.ToString <> "" Or R!FNHSysUnitId.ToString <> "" Then
                If R!FTDescription.ToString = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDescription.GetTextCaption.ToString)
                    Me.ogvdetail.FocusedColumn = Me.ogvdetail.Columns("FTDescription")
                    Return False
                End If
                If R!FNHSysUnitId.ToString = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysUnitId.GetTextCaption.ToString)
                    Me.ogvdetail.FocusedColumn = Me.ogvdetail.Columns("FNHSysUnitId")
                    Return False
                End If
                'If Double.Parse(R!FNQuantity.ToString) <= 0 Then
                '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNQuantity.GetTextCaption.ToString)
                '    Me.ogvdetail.FocusedColumn = Me.ogvdetail.Columns("FNQuantity")
                '    Return False
                'End If
            End If
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

                                If SysDocType <> "0" Then
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
                                Else
                                    _Key = .Text

                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
                                    If _dt.Rows.Count <= 0 Then
                                        _StateNew = True
                                    Else
                                        _StateNew = False
                                    End If

                                End If

                            End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then
            If SysDocType <> "0" Then
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, FNInvoiceState.SelectedIndex.ToString(), False, _CmpH).ToString
            End If
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
            Dim _Seq As Integer = 0
            If Not (ogcdetail.DataSource Is Nothing) Then
                Dim dt As DataTable
                With CType(ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                For Each R As DataRow In dt.Select("FTDescription <> '' and FNQuantity <> 0 and  FNHSysUnitId <> ''", "FNGrpSeq")
                    _Seq += +1
                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail"
                    _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",FNQuantity=" & Double.Parse("0" & R!FNQuantity.ToString)
                    _Qry &= vbCrLf & ",FTDescription='" & HI.UL.ULF.rpQuoted(R!FTDescription.ToString) & "'"
                    _Qry &= vbCrLf & ",FNPriceSale=" & Double.Parse("0" & R!FNPriceSale.ToString)
                    _Qry &= vbCrLf & ",FNChargeService=" & Double.Parse("0" & R!FNChargeService.ToString)
                    _Qry &= vbCrLf & ",FNChargeClear=" & Double.Parse("0" & R!FNChargeClear.ToString)
                    _Qry &= vbCrLf & ",FNNetPrice=" & Double.Parse("0" & R!FNNetPrice.ToString)
                    _Qry &= vbCrLf & ",FNHSysUnitId=" & Double.Parse(R!FNHSysUnitId_Hide.ToString)
                    _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Qry &= vbCrLf & "AND FNGrpSeq=" & _Seq
                    ' _Qry &= vbCrLf & "AND FNPrice=" & Double.Parse(R!FNNetPrice.ToString)
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTInvoiceNo,   FNGrpSeq,FNPrice, FNQuantity"
                        _Qry &= vbCrLf & ",FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice,FNHSysUnitId,FTDescription)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & "," & _Seq
                        _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNNetPrice.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNQuantity.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNPriceSale.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNChargeService.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNChargeClear.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse("0" & R!FNNetPrice.ToString)
                        _Qry &= vbCrLf & "," & Double.Parse(R!FNHSysUnitId_Hide.ToString)
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTDescription.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If
                Next
                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail"
                _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Qry &= vbCrLf & "AND FNGrpSeq >" & _Seq
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
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

                'FTDocRefNo.Properties.ReadOnly = CType(ogcdetail.DataSource, DataTable).Rows.Count > 0 '(ogvdetail.Rows.Count > 0)
                'FTDocRefNo.Properties.Buttons.Item(0).Enabled = Not (CType(ogcdetail.DataSource, DataTable).Rows.Count > 0)

                Call SaveData(False)
                Call LoadDetail()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub

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

            For Each R As String In Split("" & Me.FTReportInvoice.Text & "|" & Me.FTReportInvoiceCopy.Text & "|" & Me.FTReportInvoiceCopy2.Text & "", "|")
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .AddParameter("ReportName", R.ToString)
                    .AddParameter("FTInvoiceType", Me.FTInvoiceType.SelectedIndex)
                    .ReportFolderName = "Inventrory\"
                    .ReportName = "ReportInvoiceDomestic.rpt"
                    .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                    .Preview()
                End With
            Next
            For Each R As String In Split("" & Me.FTReportBill.Text & "|" & Me.FTReportBillCopy.Text & "", "|")
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .AddParameter("ReportName", R.ToString)
                    .AddParameter("FTInvoiceType", Me.FTInvoiceType.SelectedIndex)
                    .ReportFolderName = "Inventrory\"
                    .ReportName = "ReportInvoiceBill.rpt"
                    .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                    .Preview()
                End With
            Next

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

    Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Down
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        If .FocusedColumn.FieldName.ToString <> "FNQuantity" Then
                            Exit Sub
                        End If
                        Dim x As Integer = 0
                        If .GetRowCellValue(.FocusedRowHandle, "FTDescription").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitId").ToString <> "" Or
                            .GetRowCellValue(.FocusedRowHandle, "FNPriceSale").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString <> "" Or
                            .GetRowCellValue(.FocusedRowHandle, "FNChargeService").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FNChargeClear").ToString <> "" Then
                            With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                                .AcceptChanges()
                                If .Select("FTDescription='' or FTDescription Is null").Length <= 0 Then
                                    x = .Rows.Count + 1
                                    .Rows.Add(x)
                                    .Rows(x - 1).Item("FNGrpSeq") = x
                                End If
                                .AcceptChanges()
                            End With
                            .FocusedRowHandle = x
                            .FocusedColumn = .Columns.ColumnByFieldName("FTDescription")
                        End If
                    End With
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        With CType(ogcdetail.DataSource, DataTable)
                            .AcceptChanges()
                            Dim x As Integer = 0
                            For Each r As DataRow In .Select("FNGrpSeq<>0", "FNGrpSeq")
                                x += +1
                                r!FNGrpSeq = x
                            Next
                            .AcceptChanges()
                        End With
                    End With
                    SumAmt()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged

        Try
            'Dim dt As New DataTable

            'Try
            '    dt = CType(ogcdetail.DataSource, DataTable).Copy
            'Catch ex As Exception
            'End Try

            ''FNHSysCmpIdTo.Properties.ReadOnly = (dt.Rows.Count > 0)
            ''FNHSysCmpIdTo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            ''FNInvoiceState.Properties.ReadOnly = (dt.Rows.Count > 0)
            ''FNInvoiceState.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            'dt.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.ogvdetail.OptionsView.ShowAutoFilterRow = False
        AddHandler RepositoryFNHSysUnitId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        RemoveHandler FTInvoiceNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler FTInvoiceNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        _FormLoad = False
    End Sub




    Private Sub LoadDetail()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable


            _Qry = "SELECT     FTInvoiceNo  , FNPrice, FNGrpSeq, FNQuantity, FNPriceSale, FNChargeService "
            _Qry &= vbCrLf & " , FNChargeClear , FNNetPrice, U.FTUnitCode AS FNHSysUnitId , D.FNHSysUnitId AS FNHSysUnitId_Hide,  "
            _Qry &= vbCrLf & "    FTDescription  ,FNNetPrice *  FNQuantity AS FNAmount "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail AS D WITH (NOLOCK)"
            _Qry &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            If _oDt.Rows.Count = 0 Then
                With _oDt
                    .Rows.Add()
                    .AcceptChanges()
                    For Each R As DataRow In .Rows
                        R!FNGrpSeq = 1
                    Next
                End With
            End If
            Me.ogcdetail.DataSource = _oDt
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


    'Private Sub FNInvoiceState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNInvoiceState.SelectedIndexChanged
    '    Try
    '        _SysDocType = Me.FNInvoiceState.SelectedIndex
    '        Select Case Me.FNInvoiceState.SelectedIndex
    '            Case 0
    '                'ในประเทศ
    '                'Me.FNHSysCurId.Properties.Tag = 1310200002
    '                Me.FNHSysCurId.Text = ""
    '                Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
    '                Me.FNExchangeRate.Value = 1
    '                Me.FNHSysCurId.Enabled = False
    '                Me.otpdomestic.PageVisible = True
    '                Me.otpexport.PageVisible = False
    '                Me.FDInvoiceDate.Properties.ReadOnly = False
    '                Me.otbm.SelectedTabPageIndex = 1
    '                Me.otabPackingAcc.PageVisible = False
    '                Me.otabPackingFab.PageVisible = False
    '            Case 1
    '                'ต่างประเทศ
    '                Me.FNHSysCurId.Text = ""
    '                Me.FNHSysCurId.Enabled = True

    '                Me.otpdomestic.PageVisible = False
    '                Me.otpexport.PageVisible = True
    '                Me.FDInvoiceDate.Properties.ReadOnly = True
    '                Me.otbm.SelectedTabPageIndex = 0
    '                Me.otabPackingAcc.PageVisible = True
    '                Me.otabPackingFab.PageVisible = True
    '            Case 2
    '                'สาขา
    '                Me.FNHSysCurId.Text = ""
    '                Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
    '                Me.FNExchangeRate.Value = 1
    '                Me.FNHSysCurId.Enabled = False
    '                Me.otpdomestic.PageVisible = True
    '                Me.otpexport.PageVisible = False
    '                Me.FDInvoiceDate.Properties.ReadOnly = True
    '                Me.otbm.SelectedTabPageIndex = 1
    '                Me.otabPackingAcc.PageVisible = False
    '                Me.otabPackingFab.PageVisible = False
    '            Case 3
    '                'ส่วนกลาง
    '                Me.FNHSysCurId.Text = ""
    '                Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
    '                Me.FNExchangeRate.Value = 1
    '                Me.FNHSysCurId.Enabled = False
    '                Me.otpdomestic.PageVisible = True
    '                Me.otpexport.PageVisible = False
    '                Me.FDInvoiceDate.Properties.ReadOnly = True
    '                Me.otbm.SelectedTabPageIndex = 1
    '                Me.otabPackingAcc.PageVisible = False
    '                Me.otabPackingFab.PageVisible = False
    '        End Select


    '        Call LoadDetail(FTDocRefNo.Text)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub FNHSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCurId.EditValueChanged
        If _FormLoad Then Exit Sub
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

    Private Sub FNExchangeRate_EditValueChanged(sender As Object, e As EventArgs) Handles FNExchangeRate.EditValueChanged

        Try
            If Not (Me.ogcdetail.DataSource Is Nothing) Then
                Dim dt As DataTable
                With CType(Me.ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With


            End If
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
                _FNChargeService = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNChargeService").ToString)
                _FNChargeClear = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNChargeClear").ToString)
                _NetPrice = Double.Parse(e.NewValue) + _FNChargeService + _FNChargeClear
                .SetRowCellValue(.FocusedRowHandle, "FNNetPrice", _NetPrice)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _NetPrice * Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString))
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryFNQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNQuantity.EditValueChanging
        Try
            Dim _NetPrice As Double
            With Me.ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _NetPrice = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNNetPrice").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _NetPrice * Double.Parse(e.NewValue))
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryFNChargeClear_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNChargeClear.EditValueChanging
        Try
            Dim _FNChargeService, _FNChargeClear, _NetPrice As Double
            With Me.ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _FNChargeService = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNChargeService").ToString)
                _FNChargeClear = Double.Parse("0" & e.NewValue)
                _NetPrice = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNPriceSale").ToString) + _FNChargeService + _FNChargeClear
                .SetRowCellValue(.FocusedRowHandle, "FNNetPrice", _NetPrice)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _NetPrice * Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString))
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryFNChargeService_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNChargeService.EditValueChanging
        Try
            Dim _FNChargeService, _FNChargeClear, _NetPrice As Double
            With Me.ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _FNChargeService = Double.Parse("0" & e.NewValue)
                _FNChargeClear = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNChargeClear").ToString)
                _NetPrice = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNPriceSale").ToString) + _FNChargeService + _FNChargeClear
                .SetRowCellValue(.FocusedRowHandle, "FNNetPrice", _NetPrice)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _NetPrice * Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString))
            End With
            Call SumAmt()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub SumAmt()
        Try
            With Me.ogcdetail
                Dim _Amt As Double
                For Each R As DataRow In CType(.DataSource, DataTable).Rows
                    _Amt += +R!FNAmount.ToString
                Next
                Me.FNInvAmt.Value = _Amt
                Call Calculate(FNInvAmt, Nothing)
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNInvoiceState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNInvoiceState.SelectedIndexChanged
        _SysDocType = Me.FNInvoiceState.SelectedIndex
    End Sub

    Private Sub FTInvoiceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FTInvoiceType.SelectedIndexChanged
        Try
            Select Case Me.FTInvoiceType.SelectedIndex
                Case 0
                    Me.FTDeductedWaste_lbl.Visible = False
                    Me.FNDisCountPer_lbl.Visible = True
                Case 1
                    Me.FTDeductedWaste_lbl.Visible = True
                    Me.FNDisCountPer_lbl.Visible = False
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryFNHSysUnitId_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFNHSysUnitId.EditValueChanging
        Try

            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FNHSysUnitId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) where FTUnitCode='" & e.NewValue.ToString & "'"
            Dim UnitId As Integer = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0")
            With Me.ogvdetail
                .SetRowCellValue(.FocusedRowHandle, "FNHSysUnitId_Hide", UnitId)
            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreviewinv_Click(sender As Object, e As EventArgs) Handles ocmpreviewinv.Click
        If Me.FTInvoiceNo.Text <> "" Then


            For I As Integer = 1 To 5
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Inventrory\"
                    .ReportName = "ReportInvoiceBill" & I.ToString & ".rpt"
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
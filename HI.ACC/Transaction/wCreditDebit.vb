Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Public Class wCreditDebit

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
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"
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

        '  _FormLoad = True
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "





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
        Call LoadFTInvoiceNo()
        Call LoadDetail()
        If Me.FNDebitCreditState.Text <> "ลูกค้า" Then
            Call LoadPurchase()
            ' Call GetDataBySupl(Integer.Parse("0" & Me.FNHSysSuplId.Properties.Tag), Key)
        End If

        _ProcLoad = False
        ' _FormLoad = False
    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTDebitCreditBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub LoadDocumentDetail(PoKey As String)

        Dim _Str As String = ""

    End Sub
    Private Sub LoadFTInvoiceNo()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select isnull(FTInvoiceNo,'') as FTInvoiceNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice"
            _Cmd &= vbCrLf & "Where FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
            Me.ogcinvoice.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadPurchase()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select isnull(FTPurchaseNo,'') as FTPurchaseNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase"
            _Cmd &= vbCrLf & "Where FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
            Me.ogcpurchase.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            'Call Allocate()
        Catch ex As Exception
        End Try
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
        '   Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
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
            If R!FTDescription.ToString <> "" Or R!FNHSysUnitId.ToString <> "" Or Double.Parse("0" & R!FNQuantity.ToString) > 0 Then
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
                If Double.Parse("0" & R!FNQuantity.ToString) <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNQuantity.GetTextCaption.ToString)
                    Me.ogvdetail.FocusedColumn = Me.ogvdetail.Columns("FNQuantity")
                    Return False
                End If
            End If
        Next

        If Me.FNDebitCreditState.SelectedIndex <> 0 Then
            If Me.FNHSysCmpIdTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpIdTo_lbl.Text)
                Me.FNHSysCmpIdTo.Focus()
                Return False
            End If
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

                            End If

                            _Key = .Text
                        End With

                End Select

            Next

        Next

        _Str = "SELECT TOP 1 FTDebitCreditNo "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit AS A WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(FTDebitCreditNo.Text) & "'"

        _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "")

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean
            ' If Me.FNDebitCreditState.Text = "ลูกค้า" Then
            If (_StateNew) Then
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit  "
                _Str &= "( FTInsUser, FDInsDate, FTInsTime,FTDebitCreditNo, FDDebitCreditDate, FTDebitCreditBy,FNDocDebitCreditState,FNDebitCreditState,FNHSysCmpIdTo,FNHSysSuplId,FNHSysCurId,FNExchangeRate,FNDebitCreditAmt,FNInvoiceAmt,FNDiffAmt,FNVatPer,FNVatAmt,FNDebitCreditGrandAmt,FTDebitCreditGrandAmtTH,FTDebitCreditGrandAmtEN,FTRemark,FNHSysCmpId)"
                _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTDebitCreditNo.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDDebitCreditDate.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditBy.Text) & "'"
                _Str &= vbCrLf & "," & Me.FNDocDebitCreditState.SelectedIndex
                _Str &= vbCrLf & "," & Me.FNDebitCreditState.SelectedIndex
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysCmpIdTo.Properties.Tag)
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysSuplId.Properties.Tag)
                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                _Str &= vbCrLf & "," & Integer.Parse(Me.FNHSysCurId.Properties.Tag)
                _Str &= vbCrLf & "," & Me.FNExchangeRate.Value
                _Str &= vbCrLf & "," & Me.FNDebitCreditAmt.Value
                _Str &= vbCrLf & "," & Me.FNInvoiceAmt.Value
                _Str &= vbCrLf & "," & Me.FNDiffAmt.Value
                _Str &= vbCrLf & "," & Me.FNVatPer.Value
                _Str &= vbCrLf & "," & Me.FNVatAmt.Value
                _Str &= vbCrLf & "," & Me.FNDebitCreditGrandAmt.Value
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditGrandAmtTH.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditGrandAmtEN.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Str &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "


                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If
            ' If (_StateNew) Then
            _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit "
            _Str &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Str &= vbCrLf & ",FNDocDebitCreditState=" & Me.FNDocDebitCreditState.SelectedIndex
            _Str &= vbCrLf & ",FNDebitCreditState=" & Me.FNDebitCreditState.SelectedIndex
            _Str &= vbCrLf & ",FNHSysCmpIdTo=" & Integer.Parse("0" & Me.FNHSysCmpIdTo.Properties.Tag)
            _Str &= vbCrLf & ",FNHSysSuplId=" & Integer.Parse("0" & Me.FNHSysSuplId.Properties.Tag)
            '_Str &= vbCrLf & ",FTInvoiceNo= '" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "'"
            ' _Str &= vbCrLf & ",FTInvoiceNo= '" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "'"
            _Str &= vbCrLf & ",FNHSysCurId=" & Integer.Parse(Me.FNHSysCurId.Properties.Tag)
            _Str &= vbCrLf & ",FNDebitCreditAmt=" & Me.FNDebitCreditAmt.Value
            _Str &= vbCrLf & ",FNInvoiceAmt=" & Me.FNInvoiceAmt.Value
            _Str &= vbCrLf & ",FNDiffAmt=" & Me.FNDiffAmt.Value
            _Str &= vbCrLf & ",FNVatPer=" & Me.FNVatPer.Value
            _Str &= vbCrLf & ",FNVatAmt=" & Me.FNVatAmt.Value
            _Str &= vbCrLf & ",FNDebitCreditGrandAmt=" & Me.FNDebitCreditGrandAmt.Value
            _Str &= vbCrLf & ",FTDebitCreditGrandAmtTH= '" & HI.UL.ULF.rpQuoted(FTDebitCreditGrandAmtTH.Text) & "'"
            _Str &= vbCrLf & ",FTDebitCreditGrandAmtEN= '" & HI.UL.ULF.rpQuoted(FTDebitCreditGrandAmtEN.Text) & "'"
            _Str &= vbCrLf & ",FTRemark= '" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"

            _Str &= vbCrLf & "WHERE FTDebitCreditNo= '" & HI.UL.ULF.rpQuoted(FTDebitCreditNo.Text) & "'"


            'If (_StateNew) Then
            '    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
            'Else
            '    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
            'End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            'Else

            Call SaveInvoice()
            Call SavePurchaseRef()
            ' End If

            Call SaveDetail(_Key)


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
            Dim Str As String = ""
            Dim INV As String = ""
            If Not (ogcdetail.DataSource Is Nothing) Then
                Dim dt As DataTable
                With CType(ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                For Each R As DataRow In dt.Select("FTDescription <> '' and FNQuantity >= '0' and  FNHSysUnitId <> ''", "FNSeq")



                    Dim _FNHSysUnitId As String = HI.Conn.SQLConn.GetField("SELECT FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) WHERE FTUnitCode='" & R!FNHSysUnitId.ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                    _Seq += +1
                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Detail"
                    _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",FNQuantity=" & CDbl("0" & R!FNQuantity.ToString)
                    _Qry &= vbCrLf & ",FTDescription='" & HI.UL.ULF.rpQuoted(R!FTDescription.ToString) & "'"
                    _Qry &= vbCrLf & ",FNPrice=" & CDbl("0" & R!FNPrice.ToString)
                    _Qry &= vbCrLf & ",FNHSysUnitId=" & _FNHSysUnitId
                    _Qry &= vbCrLf & ",FNAmount=" & CDbl("0" & R!FNAmount.ToString)
                    _Qry &= vbCrLf & ",FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                    _Qry &= vbCrLf & "WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Qry &= vbCrLf & "AND FNSeq=" & _Seq

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Detail"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTDebitCreditNo,   FNSeq,FNPrice, FNQuantity,FTInvoiceNo"
                        _Qry &= vbCrLf & ",FNHSysUnitId,FTDescription,FNAmount)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & "," & _Seq
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNPrice.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNQuantity.ToString)
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                        _Qry &= vbCrLf & "," & _FNHSysUnitId
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTDescription.ToString) & "'"
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNAmount.ToString)

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                Next

                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Detail"
                _Qry &= vbCrLf & "WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Qry &= vbCrLf & "AND FNSeq >" & _Seq
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function
    Private Function SaveInvoice() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _Str As String = ""
            Dim _PO As String = ""
            With CType(ogcinvoice.DataSource, DataTable)
                .AcceptChanges()
                For Each E As DataRow In .Rows

                    _Cmd = E!FTInvoiceNo.ToString
                    'Next

                   
                      
                    'For Each R As DataRow In _oDt.Rows
                            _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice "
                            _Str &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",FTInvoiceNo= '" & HI.UL.ULF.rpQuoted(_Cmd) & "'"
                            _Str &= vbCrLf & "WHERE FTDebitCreditNo= '" & HI.UL.ULF.rpQuoted(FTDebitCreditNo.Text) & "'"
                            _Str &= vbCrLf & "and FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Cmd) & "'"
                            _Str &= vbCrLf & "and FNDocDebitCreditState=" & Me.FNDocDebitCreditState.SelectedIndex
                            _Str &= vbCrLf & "and FNDebitCreditState=" & Me.FNDebitCreditState.SelectedIndex

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice  "
                                _Str &= "( FTInsUser, FDInsDate, FTInsTime,FTDebitCreditNo,FNDocDebitCreditState,FNDebitCreditState,FTInvoiceNo)"
                        _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTDebitCreditNo.Text) & "'"
                        _Str &= vbCrLf & "," & Me.FNDocDebitCreditState.SelectedIndex
                        _Str &= vbCrLf & "," & Me.FNDebitCreditState.SelectedIndex
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Cmd) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If
                        Next
                    End With
                     

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SavePurchaseRef() As Boolean
        Try
            Dim _Cmd As String = ""
            With DirectCast(Me.ogcpurchase.DataSource, DataTable)
                .AcceptChanges()


                For Each R As DataRow In .Rows
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase "
                    _Cmd &= vbCrLf & " Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"
                    _Cmd &= vbCrLf & "where  FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
                    _Cmd &= vbCrLf & "and FNDocDebitCreditState=" & Me.FNDocDebitCreditState.SelectedIndex
                    _Cmd &= vbCrLf & "and FNDebitCreditState=" & Me.FNDebitCreditState.SelectedIndex
                    _Cmd &= vbCrLf & "and  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase "
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTDebitCreditNo, FNDocDebitCreditState,FNDebitCreditState, FTPurchaseNo)"
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
                        _Cmd &= vbCrLf & "," & Me.FNDocDebitCreditState.SelectedIndex
                        _Cmd &= vbCrLf & "," & Me.FNDebitCreditState.SelectedIndex
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If
                Next
            End With

            Return True
        Catch ex As Exception
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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Detail WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "'"
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


        '  If Me.VerrifyData Then
        If Me.SaveData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            'FTDocRefNo.Properties.ReadOnly = CType(ogcdetail.DataSource, DataTable).Rows.Count > 0 '(ogvdetail.Rows.Count > 0)
            'FTDocRefNo.Properties.Buttons.Item(0).Enabled = Not (CType(ogcdetail.DataSource, DataTable).Rows.Count > 0)

            Call SaveData()
            Call LoadDetail()
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
        'End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTDebitCreditNo.Text, Me.Text) = False Then
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
        Me.FNDebitCreditAmt.Text = ""
        Me.FNDiffAmt.Text = ""
        Me.FNVatPer.Text = ""
        Me.FNDebitCreditGrandAmt.Text = ""
        Me.FNHSysCurId.Text = ""
        Me.FNHSysCmpIdTo.Text = ""
        Me.ogcinvoice.DataSource = Nothing
        'Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        Me.FNExchangeRate.Value = 1
        Me.FNVatPer.Value = 7
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        Dim _Str As String = ""
        With DirectCast(Me.ogcdetail.DataSource, DataTable)
            .AcceptChanges()
            For Each R As DataRow In .Rows
                ' If key <> "" Then key &= ","
                If _Str = "" Then
                    _Str = R!FTInvoiceNo.ToString & ""
                Else
                    _Str &= " ," & R!FTInvoiceNo.ToString & ""
                End If

            Next
        End With


        If Me.FTDebitCreditNo.Text <> "" Then

            'For Each R As String In Split("" & Me.FTReport.Text & "", "|")
            '    With New HI.RP.Report
            '        .FormTitle = Me.Text
            '        .AddParameter("ReportName", R.ToString)
            '        .AddParameter("FNDocDebitCreditState", Me.FNDocDebitCreditState.SelectedIndex)
            '        .ReportFolderName = "Account\"
            '        .ReportName = "RpCredit.rpt"
            '        .Formular = "{V_Rpt_CreditDabit.FTDebitCreditNo} ='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "' "
            '        .Preview()
            '    End With
            'Next
            With New HI.RP.Report
                .FormTitle = Me.Text
                .AddParameter("FTInvoiceNo", _Str)
                .ReportFolderName = "Account\"
                .ReportName = "RpCredit.rpt"
                .Formular = "{V_Rpt_CreditDabit.FTDebitCreditNo}  ='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "' "
                .Preview()
            End With


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTDebitCreditNo_lbl.Text)
            FTDebitCreditNo.Focus()
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
                        If .GetRowCellValue(.FocusedRowHandle, "FTDescription").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitId").ToString <> "" Or _
                            .GetRowCellValue(.FocusedRowHandle, "FNPrice").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FNQ").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FNQ").ToString <> "" Then
                            With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                                .AcceptChanges()
                                If .Select("FTDescription='' or FTDescription Is null").Length <= 0 Then
                                    x = .Rows.Count + 1
                                    .Rows.Add(x)
                                    .Rows(x - 1).Item("FNSeq") = x
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
                            For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                                x += +1
                                r!FNSeq = x
                            Next
                            .AcceptChanges()
                        End With
                    End With
                    SumAmt()
            End Select
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
                Me.FNDiffAmt.Value = _Amt
                Call Calculate(FNDiffAmt, Nothing)
            End With
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs)

    '    Try
    '        'Dim dt As New DataTable

    '        'Try
    '        '    dt = CType(ogcdetail.DataSource, DataTable).Copy
    '        'Catch ex As Exception
    '        'End Try

    '        ''FNHSysCmpIdTo.Properties.ReadOnly = (dt.Rows.Count > 0)
    '        ''FNHSysCmpIdTo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

    '        ''FNInvoiceState.Properties.ReadOnly = (dt.Rows.Count > 0)
    '        ''FNInvoiceState.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

    '        'dt.Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.ogvdetail.OptionsView.ShowAutoFilterRow = False
        AddHandler RepositoryFNHSysUnitId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        RemoveHandler FTDebitCreditNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler FTDebitCreditNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler Me.FTInvoiceNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler Me.FTInvoiceNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        _FormLoad = False
    End Sub




    Private Sub LoadDetail()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable


            _Qry = "SELECT    FTDebitCreditNo  , FNPrice, FNSeq, FNQuantity, FNPrice,  U.FTUnitCode AS FNHSysUnitId , D.FNHSysUnitId AS FNHSysUnitId_Hide,  "
            _Qry &= vbCrLf & "     FTDescription  ,FNPrice *  FNQuantity AS FNAmount,D.FTInvoiceNo "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Detail AS D WITH (NOLOCK)"
            _Qry &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId "
            _Qry &= vbCrLf & " WHERE FTDebitCreditNo='" & HI.UL.ULF.rpQuoted(Me.FTDebitCreditNo.Text) & "' "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            If _oDt.Rows.Count = 0 Then
                With _oDt
                    .Rows.Add()
                    .AcceptChanges()
                    For Each R As DataRow In .Rows
                        R!FNSeq = 1
                    Next
                End With
            End If
            Me.ogcdetail.DataSource = _oDt

        Catch ex As Exception
        End Try
    End Sub



    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNVatPer.EditValueChanged, FNVatAmt.EditValueChanged

        'FNDisCountAmt.EditValueChanged,

        'FNSurcharge.EditValueChanged+

        Static _Proc As Boolean

        If Not (_Proc) And Not (_ProcLoad) Then
            _Proc = True
            Dim _POAmt As Double = FNDiffAmt.Value
            Dim _Amt As Double = FNInvoiceAmt.Value

            If _POAmt = 0 Then
                'FNDisCountAmt.Value = 0
                FNVatAmt.Value = 0
            End If

            'Dim _DisPer As Double = FNDisCountPer.Value
            'Dim _DisAmt As Double = FNDisCountAmt.Value
            Dim _VatPer As Double = FNVatPer.Value
            Dim _VatAmt As Double = FNVatAmt.Value
            'Dim _SurAmt As Double = FNSurcharge.Value

            Select Case sender.Name.ToString.ToUpper
                Case "FNVatPer".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format((_POAmt * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
                Case "FNVatAmt".ToUpper
                    _VatAmt = FNVatAmt.Value

                    If (_POAmt) > 0 Then
                        _VatPer = Format((_VatAmt * 100) / (_POAmt), HI.ST.Config.PercentFormat)
                    Else
                        _VatPer = 0
                    End If
                    FNVatPer.Value = _VatPer
                Case Else
                    '_DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    'FNDisCountAmt.Value = _DisAmt

                    _VatPer = FNVatPer.Value
                    _VatAmt = Format((_POAmt * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select
            If FNDocDebitCreditState.Text = "เพิ่มหนี้" Then

                Me.FNDebitCreditAmt.Value = (_Amt + _POAmt)
            Else
                Me.FNDebitCreditAmt.Value = (_Amt - _POAmt)
            End If
            FNDebitCreditGrandAmt.Value = Format(Me.FNDiffAmt.Value + FNVatAmt.Value, HI.ST.Config.AmtFormat)
            _Proc = False
        End If
    End Sub

    Private Sub FNDebitCreditGrandAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNDebitCreditGrandAmt.EditValueChanged
        Try
            If Not (_ProcLoad) Then
                Me.FTDebitCreditGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNDebitCreditGrandAmt.Value)
                Me.FTDebitCreditGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNDebitCreditGrandAmt.Value)
            End If
        Catch ex As Exception
        End Try
    End Sub



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
                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDDebitCreditDate.Text) & "')"
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

    Private Sub FTDebitCreditNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTDebitCreditNo.EditValueChanged
        Try
            Me.ogcinvoice.DataSource = Nothing
            Call LoadDataInfo(Me.FTDebitCreditNo.Text)
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub ogvTmp_HiddenEditor(sender As Object, e As EventArgs)
    '    Try
    '        With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
    '            .AcceptChanges()
    '            Dim _oDt As DataTable = .Copy
    '            .Rows.Add()
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub ogvTmp_KeyDown(sender As Object, e As KeyEventArgs)
    '    Try
    '        Dim _RowSeq As Integer = 0
    '        Dim _Focus As Integer = 0
    '        Select Case e.KeyCode
    '            Case Keys.Down
    '                With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
    '                    _Focus = .FocusedRowHandle
    '                End With
    '                With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
    '                    .AcceptChanges()
    '                    If _Focus = .Rows.Count - 1 Then
    '                        .Rows.Add()
    '                    End If
    '                    .AcceptChanges()
    '                End With
    '            Case Keys.Enter
    '                With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
    '                    If .FocusedColumn.FieldName.ToString = "FNTotalGrossWeight" Then
    '                        _Focus = .FocusedRowHandle

    '                        If (.GetRowCellValue(.FocusedRowHandle, "FNRollToNo").ToString <> "" And .GetRowCellValue(.FocusedRowHandle, "FNRollNo").ToString <> "" _
    '                          And .GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString <> "" _
    '                          And .GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString <> "" _
    '                          And .GetRowCellValue(.FocusedRowHandle, "FNTotalQty").ToString <> "" _
    '                          And .GetRowCellValue(.FocusedRowHandle, "FNNetWeightPerRoll").ToString <> "" _
    '                          And .GetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll").ToString <> "" _
    '                          And .GetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight").ToString <> "" _
    '                          And .GetRowCellValue(.FocusedRowHandle, "FNTotalGrossWeight").ToString <> "") Then


    '                            With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
    '                                .AcceptChanges()
    '                                If _Focus = .Rows.Count - 1 Then
    '                                    .Rows.Add()
    '                                End If
    '                                .AcceptChanges()
    '                                Dim _oDt As DataTable = .Copy
    '                                '    Call SaveData(_oDt, (CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).Name.ToString)
    '                                _RowSeq = .Rows.Count
    '                            End With

    '                            .FocusedRowHandle = .FocusedRowHandle + 1
    '                            .FocusedColumn = .Columns.ColumnByFieldName("FNRollNo")
    '                        Else
    '                            Select Case True
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNRollNo").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNRollNo")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNRollToNo").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNRollToNo")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNQtyPerRoll")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNQtyCTN")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNTotalQty").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNTotalQty")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNNetWeightPerRoll").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNNetWeightPerRoll")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNGrossWeightPerRoll")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNTotalNetWeight")
    '                                    Exit Sub
    '                                Case .GetRowCellValue(.FocusedRowHandle, "FNTotalGrossWeight").ToString = ""
    '                                    .FocusedRowHandle = .FocusedRowHandle
    '                                    .FocusedColumn = .Columns.ColumnByFieldName("FNTotalGrossWeight")
    '                                    Exit Sub
    '                            End Select
    '                        End If

    '                    End If
    '                End With
    '                'Case Keys.Tab
    '                '    With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
    '                '        .AcceptChanges()
    '                '    End With
    '                '    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
    '                '        Select Case True
    '                '            Case .FocusedColumn.FieldName.ToString = "FNQtyCTN"
    '                '                Dim _Value As Double
    '                '                _Value = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString) * Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString)
    '                '                .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", _Value)

    '                '            Case .FocusedColumn.FieldName.ToString = "FNQtyPerRoll"
    '                '                Dim _Value As Double
    '                '                _Value = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString)
    '                '                .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", _Value)
    '                '        End Select
    '                '    End With

    '            Case Keys.Delete
    '                With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
    '                    .DeleteRow(.FocusedRowHandle)
    '                End With
    '        End Select

    '    Catch ex As Exception

    '    End Try
    'End Sub



    'Private Function CreateRetoyCalEdit(_name As String, _ogv As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    '    Try
    '        With RetoyCal
    '            .Name = "RepositoryItemCal" & _name
    '            .Precision = 2
    '            .Buttons.Item(0).Visible = False
    '            AddHandler .EditValueChanging, AddressOf RepositoryCal_EditValueChanging
    '        End With
    '        Return RetoyCal
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function


    'Private Sub RepositoryCal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
    '    Try
    '        Dim _ChargeService As Double = 0

    '        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
    '            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
    '            Select Case .FocusedColumn.FieldName.ToString
    '                Case "FNQtyCTN"
    '                    .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyPerRoll").ToString) * Double.Parse(e.NewValue))
    '                    .SetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNNetWeightPerRoll").ToString) * Double.Parse(e.NewValue))
    '                    .SetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNGrossWeightPerRoll").ToString) * Double.Parse(e.NewValue))
    '                Case "FNQtyPerRoll"
    '                    .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Double.Parse(e.NewValue))
    '                Case "FNNetWeightPerRoll"
    '                    .SetRowCellValue(.FocusedRowHandle, "FNTotalNetWeight", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Double.Parse(e.NewValue))
    '                Case "FNGrossWeightPerRoll"
    '                    .SetRowCellValue(.FocusedRowHandle, "FNTotalGrossWeight", Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQtyCTN").ToString) * Double.Parse(e.NewValue))
    '            End Select
    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub RepositoryFNPriceDetail_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNPriceDetail.EditValueChanging
        Try
            Dim _Price As Double
            With Me.ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _Price * Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString))
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

   
    Private Sub RepositoryFNQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNQuantity.EditValueChanging
        Try
            Dim _Price As Double
            With Me.ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Price = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNPrice").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _Price * Double.Parse(e.NewValue))
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    
    Private Sub FTBuyerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNDebitCreditState.SelectedIndexChanged
        Try
            Select Case Me.FNDebitCreditState.SelectedIndex
                Case 0
                    Me.FNHSysCmpIdTo_lbl.Visible = True
                    Me.FNHSysCmpIdTo.Visible = True
                    Me.FNHSysCmpIdTo_None.Visible = True
                    Me.FNHSysSuplId_lbl.Visible = False
                    Me.FNHSysSuplId.Visible = False
                    Me.FNHSysSuplId_None.Visible = False
                    Me.FTInvoiceNo1.Visible = False
                    'Me.ogcinvoice.Visible = False
                    Me.FTPurchaseNo_lbl.Visible = False
                    Me.FTPurchaseNo.Visible = False
                    Me.ogcpurchase.Visible = False
                    Me.FTInvoiceNo_lbl1.Visible = True
                    Me.FTInvoiceNo_lbl.Visible = False
                Case 1
                    Me.FNHSysSuplId_lbl.Visible = True
                    Me.FNHSysSuplId.Visible = True
                    Me.FNHSysSuplId_None.Visible = True
                    Me.FNHSysCmpIdTo_lbl.Visible = False
                    Me.FNHSysCmpIdTo.Visible = False
                    Me.FNHSysCmpIdTo_None.Visible = False
                    Me.FTInvoiceNo1.Visible = True
                    'Me.ogcinvoice.Visible = True
                    Me.FTPurchaseNo_lbl.Visible = True
                    Me.FTPurchaseNo.Visible = True
                    Me.ogcpurchase.Visible = True
                    Me.FTInvoiceNo_lbl1.Visible = False
                    Me.FTInvoiceNo_lbl.Visible = True
            End Select
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadQTY(Optional ByVal _State As Boolean = True)
        Dim _Qry As String = ""
        Dim Str As String = ""
        Dim _PO As String = ""
        '  If (_State) Then
        With CType(ogcinvoice.DataSource, DataTable)
            .AcceptChanges()
            For Each R As DataRow In .Rows
                If Str <> "" Then Str &= ","
                Str &= "'" & R!FTInvoiceNo.ToString & "'"
            Next
        End With
        ' End If

        If Me.FNDebitCreditState.Text = "ลูกค้า" Then
            _Qry = " SELECT sum(FNInvNetAmt) as FNInvNetAmt"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice  WITH(NOLOCK)  "
            _Qry &= vbCrLf & "   WHERE  FTInvoiceNo in (" & Str & ")  "
        Else


            With CType(ogcpurchase.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    If _PO <> "" Then _PO &= ","
                    _PO &= "'" & R!FTPurchaseNo.ToString & "'"
                Next
            End With
            _Qry = " SELECT  SUM(RD.FNNetAmt) as FNInvNetAmt" 'sum(Convert(numeric(18, 2),  RD.FNNetAmt+(RD.FNNetAmt*R.FNVatPer)/100)) as FNInvNetAmt"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R INNER JOIN  "
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail as RD ON R.FTReceiveNo=RD.FTReceiveNo"
            _Qry &= vbCrLf & "   WHERE  R.FTInvoiceNo in (" & Str & ") and R.FTPurchaseNo in (" & _PO & ") "
        End If
        FNInvoiceAmt.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

    End Sub
    Private Sub LoadExchangeRate()
        Dim _Qry As String = ""
        Dim Str As String = ""
        With CType(ogcinvoice.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Rows
                If Str <> "" Then Str &= ","
                Str &= "'" & R!FTInvoiceNo.ToString & "'"
            Next
        End With

        _Qry = " SELECT FNExchangeRate"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "   WHERE  FTInvoiceNo in (" & Str & ")"

        FNExchangeRate.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")

    End Sub
    Private Sub LoadCurrency()
        Dim _Qry As String = ""
        Dim Str As String = ""
        With CType(ogcinvoice.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Rows
                If Str <> "" Then Str &= ","
                Str &= "'" & R!FTInvoiceNo.ToString & "'"
            Next
        End With


        _Qry = " SELECT C.FTCurCode"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice as I WITH(NOLOCK)  "
        _Qry &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH ( NOLOCK )  ON I.FNHSysCurId=C.FNHSysCurId"
        _Qry &= vbCrLf & "   WHERE  FTInvoiceNo in (" & Str & ")"

        FNHSysCurId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

    End Sub

    Private Sub LoadCmp()
        Dim _Qry As String = ""
        Dim Str As String = ""
        With CType(ogcinvoice.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Rows
                If Str <> "" Then Str &= ","
                Str &= "'" & R!FTInvoiceNo.ToString & "'"
            Next
        End With

        _Qry = " SELECT C.FTCmpCode as FNHSysCmpIdTo"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice as I  WITH(NOLOCK) INNER JOIN "
        _Qry &= vbCrLf & "   (   SELECT  FTCmpCode,  FTDescription,FNHSysCmpId "
        _Qry &= vbCrLf & "FROM (SELECT   FTCmpCode,FTCmpNameEN AS FTDescription,FNHSysCmpId  "
        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH ( NOLOCK ) "
        _Qry &= vbCrLf & "WHERE  FTStateActive ='1' "
        _Qry &= vbCrLf & "UNION "
        _Qry &= vbCrLf & "SELECT    FTCustCode,FTCustNameEN,   FNHSysCustId"
        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer WITH ( NOLOCK )"
        _Qry &= vbCrLf & " where FTStateSaleInvoice = '1' OR FTStateGarment = '1'  ) AS T"
        _Qry &= vbCrLf & "   )as C ON I.FNHSysCmpIdTo=C.FNHSysCmpId"
        _Qry &= vbCrLf & "   WHERE  FTInvoiceNo in (" & Str & ")"

        FNHSysCmpIdTo.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

    End Sub
    Private Sub LoadCmpTo()
        Dim _Qry As String = ""

        Dim Str As String = ""
        With CType(ogcinvoice.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Rows
                If Str <> "" Then Str &= ","
                Str &= "'" & R!FTInvoiceNo.ToString & "'"
            Next
        End With



        _Qry = " SELECT C.FTDescription as FNHSysCmpIdTo_None"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice as I  WITH(NOLOCK) INNER JOIN "
        _Qry &= vbCrLf & "   (   SELECT  FTCmpCode,  FTDescription,FNHSysCmpId "
        _Qry &= vbCrLf & "FROM (SELECT   FTCmpCode,FTCmpNameTH AS FTDescription,FNHSysCmpId  "
        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH ( NOLOCK ) "
        _Qry &= vbCrLf & "WHERE  FTStateActive ='1' "
        _Qry &= vbCrLf & "UNION "
        _Qry &= vbCrLf & "SELECT    FTCustCode,FTCustNameTH AS FTDescription,   FNHSysCustId"
        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer WITH ( NOLOCK )"
        _Qry &= vbCrLf & " where FTStateSaleInvoice = '1' OR FTStateGarment = '1'  ) AS T"
        _Qry &= vbCrLf & "   )as C ON I.FNHSysCmpIdTo=C.FNHSysCmpId"
        _Qry &= vbCrLf & "   WHERE  FTInvoiceNo in (" & Str & ")"

        FNHSysCmpIdTo_None.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

    End Sub

    Private Sub FTInvoiceNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTInvoiceNo.KeyDown

        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    If Me.FTDebitCreditNo.Text = "" Then
                        Me.FTInvoiceNo.Text = ""
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTInvoiceNo_lbl.Text)
                        Me.FTDebitCreditNo.Focus()
                        Exit Sub
                    End If
                    ' If Me.FTStateSendApp.Checked = True Then Exit Sub
                    If FTInvoiceNo.Text = "" Then Exit Sub

                    ' If FTPurchaseNo.Properties.Tag.ToString = "" Then Exit Sub
                    Dim _dtdoc As DataTable
                    If Me.ogcinvoice.DataSource Is Nothing Then
                        Dim dt As New DataTable
                        dt.Columns.Add("FTInvoiceNo", GetType(String))
                        Me.ogcinvoice.DataSource = dt
                    End If
                    With CType(Me.ogcinvoice.DataSource, DataTable)
                        .AcceptChanges()
                        _dtdoc = .Copy
                    End With
                    If _dtdoc.Select("FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "'").Length <= 0 Then
                        _dtdoc.Rows.Add(FTInvoiceNo.Text)
                    End If



        
        Me.ogcinvoice.DataSource = _dtdoc
        Me.ogcinvoice.Refresh()
        FTInvoiceNo.Text = ""
                    FTInvoiceNo.Focus()

                    Call LoadQTY()
                    Call LoadCmp()
                    Call LoadDetail()
                    Call LoadCurrency()

                    Call LoadDesc()
                    Call LoadCmpTo()
                    Call LoadExchangeRate()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDesc()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable
            Dim Str As String = ""
            With CType(ogcinvoice.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    If Str <> "" Then Str &= ","
                    Str &= "'" & R!FTInvoiceNo.ToString & "'"
                Next
            End With
            Dim _INV As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTInvoiceNoFROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail   WHERE  FTInvoiceNo in (" & Str & ")", Conn.DB.DataBaseName.DB_MASTER, "")


            If _INV = "" Then
                _Qry = "    SELECT FNGrpSeq as FNSeq,ID.FTDescription,isnull( U.FTUnitCode,'-') AS FNHSysUnitId , ID.FNPrice,('0.00')as FNQuantity,ID.FNPrice*0 AS FNAmount,ID.FTInvoiceNo"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail  as ID  "
                _Qry &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON ID.FNHSysUnitId = U.FNHSysUnitId "
                _Qry &= vbCrLf & "   WHERE  ID.FTInvoiceNo in (" & Str & ")"
            Else
                _Qry = "    SELECT FNGrpSeq as FNSeq,ID.FTDescription,isnull( U.FTUnitCode,'-') AS FNHSysUnitId , ID.FNPrice,isnuLL(D.FNQuantity,'0.00')as FNQuantity,ID.FNPrice*0 AS FNAmount"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit AS C"
                _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Detail AS D ON C.FTDebitCreditNo=D.FTDebitCreditNo"
                _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS DN ON C.FTDebitCreditNo=DN.FTDebitCreditNo"
                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Detail  as ID WITH(NOLOCK) ON ID.FTInvoiceNo=DN.FTInvoiceNo  "
                _Qry &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON ID.FNHSysUnitId = U.FNHSysUnitId "
                _Qry &= vbCrLf & "   WHERE  ID.FTInvoiceNo in (" & Str & ")"
            End If



            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Me.ogcdetail.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTInvoiceNo.EditValueChanged
    '    Dim _IN As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 C.FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice as I  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH ( NOLOCK )  ON I.FNHSysCurId=C.FNHSysCurId  WHERE  I.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "'", Conn.DB.DataBaseName.DB_MASTER, "")
    '    Call LoadQTY()
    '    Call LoadCmp()
    '    Call LoadDetail()
    '    Call LoadDesc()
    '    If _IN <> "" Then
    '        Call LoadCurrency()
    '        Call LoadExchangeRate()
    '    End If
    '    Call LoadCmpTo()
    'End Sub


    Private Sub FNInvoiceAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNInvoiceAmt.EditValueChanged
        Call SumAmt()
    End Sub
    Private Sub FTInvoiceNo1_KeyDown(sender As Object, e As KeyEventArgs) Handles FTInvoiceNo1.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    If Me.FTDebitCreditNo.Text = "" Then
                        Me.FTInvoiceNo1.Text = ""
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTInvoiceNo_lbl.Text)
                        Me.FTDebitCreditNo.Focus()
                        Exit Sub
                    End If
                    ' If Me.FTStateSendApp.Checked = True Then Exit Sub
                    If FTInvoiceNo1.Text = "" Then Exit Sub

                    ' If FTPurchaseNo.Properties.Tag.ToString = "" Then Exit Sub
                    Dim _dtdoc As DataTable
                    If Me.ogcinvoice.DataSource Is Nothing Then
                        Dim dt As New DataTable
                        dt.Columns.Add("FTInvoiceNo", GetType(String))
                        Me.ogcinvoice.DataSource = dt
                    End If
                    With CType(Me.ogcinvoice.DataSource, DataTable)
                        .AcceptChanges()
                        _dtdoc = .Copy
                    End With
                    If _dtdoc.Select("FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo1.Text) & "'").Length <= 0 Then
                        _dtdoc.Rows.Add(FTInvoiceNo1.Text)
                    End If
                    Me.ogcinvoice.DataSource = _dtdoc
                    Me.ogcinvoice.Refresh()
                    FTInvoiceNo1.Text = ""
                    FTInvoiceNo1.Focus()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvInvoice_RowCountChanged(sender As Object, e As EventArgs) Handles ogvInvoice.RowCountChanged
        Try
            Dim key As String = ""
            Dim _OD As String = ""
            With DirectCast(Me.ogcinvoice.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    If key <> "" Then key &= ","
                    key &= "'" & R!FTInvoiceNo.ToString & "'"
                Next
            End With
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    ' If key <> "" Then key &= ","
                    _OD &= "" & R!FTDebitCreditNo.ToString & ""
                Next
            End With


            Call LoadDesc()
            If _OD = "" Then
                If key <> "" Then
                    'If Me.FNDebitCreditState.Text = "ผู้ขาย" Then
                    GetDataBySupl(Integer.Parse("0" & Me.FNHSysSuplId.Properties.Tag), key)
                    'End If
                End If
            End If

            Call LoadQTY()
            Call LoadCmp()
            Call LoadDetail()
            Call LoadCurrency()


            Call LoadCmpTo()
            Call LoadExchangeRate()

        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetDataBySupl(_SuplId As Integer, Str As String, Optional ByVal _State As Boolean = False)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _PO As String = ""
            _State = True
            If (_State) Then
                With CType(ogcinvoice.DataSource, DataTable)
                    .AcceptChanges()
                    Str = ""
                    For Each R As DataRow In .Rows
                        If Str <> "" Then Str &= ","
                        Str &= "'" & R!FTInvoiceNo.ToString & "'"
                    Next
                End With

                With CType(ogcpurchase.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows
                        If _PO <> "" Then _PO &= ","
                        _PO &= "'" & R!FTPurchaseNo.ToString & "'"
                    Next
                End With

                _Cmd = "select  row_number() OVER (ORDER BY FTRawMatNameTH) AS 'FNSeq',M.FTRawMatNameTH as FTDescription,isnull( U.FTUnitCode,'-') AS FNHSysUnitId , RD.FNNetPrice AS FNPrice,0.00 as FNQuantity,RD.FNNetPrice*0 AS FNAmount"
                _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS R LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK) ON  R.FTReceiveNo=RD.FTReceiveNo  "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON RD.FNHSysUnitId = U.FNHSysUnitId "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH(NOLOCK) ON RD.FNHSysRawMatId = M.FNHSysRawMatId"
                _Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS C ON R.FTInvoiceNo=C.FTInvoiceNo"
                '_Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Detail AS D ON C.FTDebitCreditNo=D.FTDebitCreditNo"
                _Cmd &= vbCrLf & "Where R.FTInvoiceNo in (" & Str & ") and R.FTPurchaseNo in (" & _PO & ")"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                Me.ogcdetail.DataSource = _oDt
                Me.ogvdetail.ExpandAllGroups()

            End If

        Catch ex As Exception
        End Try

    End Sub
    Private Sub ogvInvoice_DoubleClick(sender As Object, e As EventArgs) Handles ogvInvoice.DoubleClick
        Try
            '  If Not (CheckSendApp()) Then Exit Sub
            With ogvInvoice
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                With CType(Me.ogcinvoice.DataSource, DataTable)
                    .AcceptChanges()
                End With
            End With
            Me.FNHSysCurId.Text = ""
            Me.FNHSysCmpIdTo.Text = ""
            Call LoadDetail()
        Catch ex As Exception
        End Try


    End Sub

    Private Sub FTPurchaseNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTPurchaseNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    If Me.FTDebitCreditNo.Text = "" Then
                        Me.FTPurchaseNo.Text = ""
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTInvoiceNo_lbl.Text)
                        Me.FTDebitCreditNo.Focus()
                        Exit Sub
                    End If

                    If FTPurchaseNo.Text = "" Then Exit Sub

                    ' If FTPurchaseNo.Properties.Tag.ToString = "" Then Exit Sub
                    Dim _dtdoc As DataTable
                    If Me.ogcpurchase.DataSource Is Nothing Then
                        Dim dt As New DataTable
                        dt.Columns.Add("FTPurchaseNo", GetType(String))
                        Me.ogcpurchase.DataSource = dt
                    End If
                    With CType(Me.ogcpurchase.DataSource, DataTable)
                        .AcceptChanges()
                        _dtdoc = .Copy
                    End With
                    If _dtdoc.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'").Length <= 0 Then
                        _dtdoc.Rows.Add(FTPurchaseNo.Text)
                    End If
                    Me.ogcpurchase.DataSource = _dtdoc
                    Me.ogcpurchase.Refresh()
                    FTPurchaseNo.Text = ""
                    FTPurchaseNo.Focus()
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ogvpurchase_DoubleClick(sender As Object, e As EventArgs) Handles ogvpurchase.DoubleClick
        Try
            '  If Not (CheckSendApp()) Then Exit Sub
            With ogvpurchase
                If .FocusedRowHandle < 0 Then Exit Sub

                .DeleteRow(.FocusedRowHandle)
                With CType(Me.ogcpurchase.DataSource, DataTable)
                    .AcceptChanges()
                End With
            End With
            Call LoadDetail()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvpurchase_RowCountChanged(sender As Object, e As EventArgs) Handles ogvpurchase.RowCountChanged
        Try
            Dim key As String = ""
            Dim _OD As String = ""
            With DirectCast(Me.ogcpurchase.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    If key <> "" Then key &= ","
                    key &= "'" & R!FTPurchaseNo.ToString & "'"
                Next
            End With
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    ' If key <> "" Then key &= ","
                    _OD &= "" & R!FTDebitCreditNo.ToString & ""
                Next
            End With

            If _OD = "" Then
                If key <> "" Then
                    If Me.FNDebitCreditState.Text = "ผู้ขาย" Then
                        GetDataBySupl(Integer.Parse("0" & Me.FNHSysSuplId.Properties.Tag), key)
                    End If

                End If
            End If

            Call LoadQTY()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogcinvoice_Click(sender As Object, e As EventArgs) Handles ogcinvoice.Click

    End Sub
End Class
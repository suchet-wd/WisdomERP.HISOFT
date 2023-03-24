Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Office.Interop
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Public Class wAcceptPckPlandTrack

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As System.Data.DataTable
    Private tW_SysPath As String = System.Windows.Forms.Application.StartupPath & IIf(Microsoft.VisualBasic.Right(System.Windows.Forms.Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = System.Windows.Forms.Application.StartupPath & IIf(Microsoft.VisualBasic.Right(System.Windows.Forms.Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _oDtPacking As System.Data.DataTable
    Private RetoyCal As New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Private _wInvoiceSelectPoup As wInvoiceSelectPoup
    Private _dtcuspobreakdown As System.Data.DataTable
    Private _dtcuspobreakdownspare As System.Data.DataTable
    Private _ReportPopUp As wReportExportPopup
    Private _LstReport As HI.RP.ListReport
    Private _wCreateInvForBooking As wCreateInvForBooking
    Private _wPckPlandEditByColorWay As wPckPlandEditByColorWay

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _wInvoiceSelectPoup = New wInvoiceSelectPoup
        HI.TL.HandlerControl.AddHandlerObj(_wInvoiceSelectPoup)
        _ReportPopUp = New wReportExportPopup
        HI.TL.HandlerControl.AddHandlerObj(_ReportPopUp)
        Dim oSysLang As New ST.SysLanguage
        _wCreateInvForBooking = New wCreateInvForBooking
        HI.TL.HandlerControl.AddHandlerObj(_wCreateInvForBooking)
        _wPckPlandEditByColorWay = New wPckPlandEditByColorWay
        HI.TL.HandlerControl.AddHandlerObj(_wPckPlandEditByColorWay)


        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wInvoiceSelectPoup.Name.ToString.Trim, _wInvoiceSelectPoup)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ReportPopUp.Name.ToString.Trim, _ReportPopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wCreateInvForBooking.Name.ToString.Trim, _wCreateInvForBooking)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wPckPlandEditByColorWay.Name.ToString.Trim, _wPckPlandEditByColorWay)
        Catch ex As Exception
        Finally
        End Try
        Call PrepareForm()
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


    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region





#Region "Procedure"

    Private Property ogvdocref As Object

    Private Sub PrepareForm()

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

        _FormLoad = True
        _ProcLoad = True
        HI.TL.HandlerControl.ClearControl(Me)

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

        Call LoadDocumentDetail(Key.ToString)

        'If Me.FTInvoiceNo.Properties.Tag <> "" Then
        '    If ogvdetail.RowCount > 0 Then
        '        Me.FTDocRefNo.Enabled = False
        '    End If
        'Else
        '    Me.FTDocRefNo.Enabled = True
        'End If

        '  Call LoadDocRef(Key)
        ' Call LoadDetail(Me.FTDocRefNo.Text)


        _ProcLoad = False
        _FormLoad = False
    End Sub

    Private Function CheckOwner() As Boolean
        'If (HI.ST.UserInfo.UserName.ToUpper = FTExportInvoiceUser.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
        '    Return True
        'Else
        '    HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        '    Return False
        'End If
    End Function

    Private Sub LoadDocumentDetail(Key As String)
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT  FTInvoiceNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice_Detail WITH(NOLOCK) Where FTExportInvoiceNo='" & Key & "'"
            ' Me.ogcRef.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

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
        '   Me.ogcdetail.DataSource = Nothing
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



        'For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
        '    For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
        '        _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
        '        _Caption = ""

        '        For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)

        '            If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
        '                _Caption = ObjCaption.Text
        '                Exit For
        '            End If
        '        Next
        '        Dim Pass As Boolean = True
        '        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
        '            Select Case HI.ENM.Control.GeTypeControl(Obj)
        '                Case ENM.Control.ControlType.ButtonEdit
        '                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
        '                        If .Properties.Buttons.Count <= 1 Then
        '                            Try
        '                                If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
        '                                    Pass = False
        '                                End If
        '                            Catch ex As Exception
        '                            End Try

        '                        End If
        '                    End With
        '                Case ENM.Control.ControlType.CalcEdit
        '                    With CType(Obj, DevExpress.XtraEditors.CalcEdit)
        '                        If Val(.Value.ToString) <= 0 Then
        '                            Pass = False
        '                        End If
        '                    End With
        '                Case ENM.Control.ControlType.ComboBoxEdit
        '                    With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
        '                        If .SelectedIndex < 0 Then Pass = False
        '                    End With
        '                Case ENM.Control.ControlType.CheckEdit

        '                Case ENM.Control.ControlType.DateEdit
        '                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
        '                        If HI.UL.ULDate.CheckDate(.Text) = "" Then
        '                            Pass = False
        '                        End If
        '                    End With
        '                Case ENM.Control.ControlType.PictureEdit
        '                    With CType(Obj, DevExpress.XtraEditors.PictureEdit)
        '                        If .Image Is Nothing Then
        '                            Pass = False
        '                        End If
        '                    End With
        '                Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
        '                    If Obj.Text = "" Then
        '                        Pass = False
        '                    End If
        '                Case Else
        '                    Pass = False
        '            End Select

        '            If Pass = False Then
        '                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
        '                Obj.Focus()
        '                Return False
        '            End If
        '        Next
        '    Next
        'Next

        '---------- Validate Document ---------------------
        'For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
        '    For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
        '        Select Case HI.ENM.Control.GeTypeControl(Obj)
        '            Case ENM.Control.ControlType.ButtonEdit
        '                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
        '                    If .Text.Trim() = "" Then
        '                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
        '                        Obj.Focus()
        '                        Return False
        '                    Else
        '                        Dim _CmpH As String = ""
        '                        For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

        '                            Select Case HI.ENM.Control.GeTypeControl(ctrl)
        '                                Case ENM.Control.ControlType.ButtonEdit
        '                                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
        '                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        '                                    End With
        '                                    Exit For
        '                                Case ENM.Control.ControlType.TextEdit
        '                                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
        '                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        '                                    End With
        '                                    Exit For
        '                            End Select
        '                        Next
        '                        If SysDocType <> "0" Then
        '                            If .Text = "" Then
        '                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
        '                                Obj.Focus()
        '                                Return False
        '                            End If

        '                            'If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, SysDocType, True, _CmpH).ToString() Then
        '                            '    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
        '                            '    Dim _dt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        '                            '    If _dt.Rows.Count <= 0 Then
        '                            '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
        '                            '        Obj.Focus()
        '                            '        Return False
        '                            '    End If
        '                            'End If
        '                        End If
        '                    End If
        '                End With
        '        End Select
        '    Next
        'Next
        Return True
    End Function





    Private Function _SizeIndex(sizecode As String) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1   FNMatSizeSeq From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize WITH(NOLOCK) Where FTMatSizeCode='" & sizecode & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "999")
        Catch ex As Exception
            Return 9999
        End Try
    End Function

    Private Function DeleteData(_oDt As System.Data.DataTable) As Boolean
        Try
            Dim _Str As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _oDt.Select("FTSelect = '1'")
                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice_D WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Next

            'Dim _Str As String
            '_Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice WHERE FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
            'If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '    HI.Conn.SQLConn.Tran.Rollback()
            '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '    Return False
            'End If

            '_Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice_Detail WHERE FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
            'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            '_Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice_Price WHERE FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
            'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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
        '  FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        _FormLoad = False
    End Sub
#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs)
        'If CheckOwner() = False Then Exit Sub



        Dim state As Boolean = False
        'oTabmaster 'oTabmasterInden
        Dim _odt As System.Data.DataTable : Dim _Cmd As String = "" : Dim _FtPONo As String = "" : Dim _dtd As New System.Data.DataTable
        Dim _FtPONoRef As String = ""
        Dim _ShipDate As String = ""
        With DirectCast(Me.ogcPlandM.DataSource, System.Data.DataTable)
            .AcceptChanges()
            _odt = .Copy
            .AcceptChanges()
        End With
        If _odt.Select("FTSelect = '1'  ").Length <= 0 Then
            HI.MG.ShowMsg.mInfo("Pls check data select !!!!", 1803151437, Me.Text, "", MessageBoxIcon.Stop)
            Exit Sub
        End If
        'OR FTStateShipDate ='1'
        'can't check shiptment date

        If _odt.Select("(FTShipModeState = '1'  OR FTStateMainMat='1' OR FTStateForwarder='1' OR  FTProvinceState='1') and FTSelect ='1' ").Length > 0 Then
            HI.MG.ShowMsg.mInfo("ข้อมูล Packing กับ ระบบสั่งผลิต ไม่ตรงกัน กรุณาตรวจสอบข้อมูล !!!!", 1907091049, Me.Text, "", MessageBoxIcon.Stop)
            Exit Sub
        End If



        If _odt.Select("FTSelect = '1'  ").Length > 0 Then
            For Each R As DataRow In _odt.Select("FTSelect = '1'")
                _Cmd = "Select Top 1 FTInvoiceNo "
                _Cmd &= vbCrLf & " From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice_D WITH(NOLOCK)"
                _Cmd &= vbCrLf & " where  FTPORef='" & R!FTPORef.ToString & "' "
                _Cmd &= vbCrLf & " and FTPORefNo='" & R!FTPORefNo.ToString & "' "
                '_Cmd &= vbCrLf & " and convert(varchar(5), convert(int ,FTNikePOLineItem)) in (" & R!FTNikePOLineItem.ToString & "  )"
                Dim _InvoiceNo As String = ""
                _InvoiceNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")
                If _InvoiceNo <> "" Then
                    _Cmd = " Select  Top 1  A.FTStateApprove"
                    _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_Ref As R With(nolock)"
                    _Cmd &= vbCrLf & "   inner join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice As A With(NOLOCK) On R.FTInvoiceNo = A.FTInvoiceNo"
                    _Cmd &= vbCrLf & " where R.FTInvoiceRefNo='" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "'"
                    If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "2" Then
                    Else
                        HI.MG.ShowMsg.mInfo("ไม่สามารถบันทึกเอกสารเนื่องจากมี รายการ บันทึกเอกสารไปแล้ว กรุณาตรวจสอบ !!!!", 1803241701, Me.Text, "", MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                End If
            Next
        End If


        If _odt.Select("FTSelect = '1' and FTInvoiceNo <> ''").Length >= 1 Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถบันทึกเอกสารเนื่องจากมี รายการ บันทึกเอกสารไปแล้ว กรุณาตรวจสอบ !!!!", 1803241701, Me.Text, "", MessageBoxIcon.Stop)
            Exit Sub
        End If

        If _odt.Select("FTSelect ='1'").Length > 0 Then

            For Each R As DataRow In _odt.Select("FTSelect = '1'")
                If R!FTPlantCode.ToString = "" And R!FTSuplCode.ToString = "" Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถบันทึกเอกสารเนื่องจากข้อมูลไม่สมบูรณ์  กรุณาตรวจสอบ ข้อมูลสั่งโรงงานผลิต !!!!", 1810221544, Me.Text, "", MessageBoxIcon.Stop)
                    Exit Sub
                End If
            Next


        End If


        _FtPONo = ""
        _FtPONoRef = ""
        For Each R As DataRow In _odt.Select("FTSelect = '1'")
            If _FtPONo <> "" Then _FtPONo &= ","
            If _FtPONoRef <> "" Then _FtPONoRef &= ","
            _FtPONo &= "'" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
            _FtPONoRef &= "'" & HI.UL.ULF.rpQuoted(R!FTPORefNo.ToString) & "'"
            If HI.UL.ULDate.ConvertEN(_ShipDate) > R!FDShipDate.ToString Or _ShipDate = "" Then _ShipDate = R!FDShipDate.ToString
            If R!FDCfmShipDate.ToString <> "" Then
                _ShipDate = R!FDCfmShipDate.ToString

            End If
        Next

        _Cmd = " SELECT '1' AS FTSelect ,   FTPckPlanNo, FTPORef, FTRangeNo, FNFrom, FNTo, FTSerialFrom, FTSerialTo, FTPackInstructionCode, FTLineNo, FTStyleCode, FTSKU, FTPONo, "
        _Cmd &= vbCrLf & "  FTPOLineNo, FTColorWay, FTSizeBreakDown, FTShortDescription, FTShipmentMethod,   FNItemQty, FNQtyPerPack, FNInnerPackCount, FNPackCount,  "
        _Cmd &= vbCrLf & "      FTR, FTPackCode, FNNetWeight, FNTotalNetWeight, FNGrossNetWeight, FTUnitCode,  FNL, FNW, FNH, FTItemUnitCode, FTScanID ,FTPORefNo"
        _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D WITH(NOLOCK)  "
        _Cmd &= vbCrLf & " where  FTPORef in (" & _FtPONo & ")"
        _Cmd &= vbCrLf & " and  FTPORefNo in (" & _FtPONoRef & ")"
        _Cmd &= vbCrLf & " and   FTPORef+'|'+ FTPOLineNo+'|'+FTColorway+'|'+FTSizeBreakDown  not in (  "
        _Cmd &= vbCrLf & " SELECT FTPORef+'|'+ FTNikePOLineItem+'|'+FTColorway+'|'+FTSizeBreakDown   "
        _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice_D WITH(NOLOCK) "
        _Cmd &= vbCrLf & " where  FTPORef in (" & _FtPONo & ")    "
        _Cmd &= vbCrLf & " and  FTPORefNo in (" & _FtPONoRef & "))"
        _Cmd &= vbCrLf & " ORDER BY  FTPOLineNo  ASC , FTPORef ASC ,FTRangeNo ASC , FTLineNo  ASC "

        _dtd = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)



        '_odt.Columns.Add("FTInvoiceNo", GetType(String))
        '_odt.AcceptChanges()
        HI.TL.HandlerControl.ClearControl(_wCreateInvForBooking)
        With _wCreateInvForBooking
            .FTGenInvType.SelectedIndex = 0
            .FDShipDate.Text = _ShipDate
            .ogcPlandM.DataSource = _odt.Select("FTSelect = '1'").CopyToDataTable
            .ogcPlandD.DataSource = _dtd.Copy

            .ShowDialog()
            If Not (.ProcComplete) Then
                For Each R As DataRow In _odt.Select("FTSelect = '1'")
                    R!FTSelect = "0"
                Next
                Me.ogcPlandM.DataSource = _odt.Copy
                Exit Sub
            Else
                Dim _odtnew As System.Data.DataTable
                With DirectCast(.ogcPlandM.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    _odtnew = .Copy
                End With

                For Each Rx As DataRow In _odtnew.Rows
                    For Each R As DataRow In _odt.Select("FTSelect = '1' and FTPckPlanNo='" & Rx!FTPckPlanNo.ToString & "' and FTPORef='" & Rx!FTPORef.ToString & "' and FTPORefNo='" & Rx!FTPORefNo.ToString & "'")
                        R!FTSelect = "0"
                        R!FTInvoiceNo = Rx!FTInvoiceNo.ToString
                        If .FTGenInvType.SelectedIndex = 0 Then
                            R!FTInvoiceNoPre = Rx!FTInvoiceNo.ToString
                        End If
                    Next
                Next


                Me.ogcPlandM.DataSource = _odt.Copy


                ' ocmload_Click(sender, e)
            End If
        End With

        If state Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Dim view As ColumnView = Me.ogvPlandM
            view.ActiveFilter.Clear()
        End If


    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs)
        '  If CheckOwner() = False Then Exit Sub
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, "", Me.Text) = False Then
            Exit Sub
        End If
        Dim _odt As System.Data.DataTable : Dim _Cmd As String = "" : Dim _FtPONo As String = "" : Dim _dtd As New System.Data.DataTable
        Dim _ShipDate As String = ""
        With DirectCast(Me.ogcPlandM.DataSource, System.Data.DataTable)
            .AcceptChanges()
            _odt = .Copy
            .AcceptChanges()
        End With

        If _odt.Select("FTSelect = '1'  ").Length <= 0 Then
            HI.MG.ShowMsg.mInfo("Pls check data select !!!!", 1807191617, Me.Text, "", MessageBoxIcon.Stop)
            Exit Sub
        End If

        If _odt.Select("FTSelect = '1' and FTInvoiceNoPre <> ''").Length > 1 Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถลบเอกสารเนื่องจากมี รายการ บันทึกเอกสารใบกำกับภาษีส่งออก ไปแล้ว กรุณาตรวจสอบ !!!!", 1807191616, Me.Text, "", MessageBoxIcon.Stop)
            Exit Sub
        End If


        If Me.DeleteData(_odt) Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            For Each R As DataRow In _odt.Select("FTSelect = '1' ")
                R!FTSelect = "0"
                R!FTInvoiceNo = ""

            Next
            Me.ogcPlandM.DataSource = _odt
            '  ocmload_Click(sender, e)
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Try

            ' HI.TL.HandlerControl.ClearControl(Me)
            Dim ind As Integer = 0
            For Each xtab As DevExpress.XtraTab.XtraTabPage In Me.oTabPlanGen.TabPages
                ind = xtab.TabIndex

            Next


            For i As Integer = ind To 1 Step -1
                Me.oTabPlanGen.TabPages.RemoveAt(i)
            Next
            Me.oTabPlanGen.TabPages.Item(0).Visible = True

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs)
        Try
            Dim _rSelectIndex As Integer = 0

            HI.TL.HandlerControl.ClearControl(_ReportPopUp)
            With _ReportPopUp
                .ShowDialog()
                If (.Proc) Then
                    _rSelectIndex = .FNReportname.SelectedIndex
                    _LstReport = ._LstReport
                Else
                    Exit Sub
                End If
            End With
            Dim _AllReportName As String = _LstReport.GetValue(_rSelectIndex)
            'If _AllReportName <> "" Then

            '    For Each _ReportName As String In _AllReportName.Split(",")
            '        With New HI.RP.Report

            '            .FormTitle = Me.Text
            '            .ReportFolderName = _LstReport.GetFolderReportValue(_rSelectIndex)
            '            .Formular = "{V_ExportInvoice.FTExportInvoiceNo} = '" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
            '            .ReportName = _ReportName
            '            .Preview()
            '        End With
            '    Next
            'Else
            '    HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            'End If
        Catch ex As Exception
        End Try
        'With New HI.RP.Report
        '    .FormTitle = Me.Text
        '    .ReportFolderName = "Account\"

        '    .ReportName = "RptExportInvoice.rpt"

        '    .Formular = "{V_ExportInvoice.FTExportInvoiceNo}  ='" & HI.UL.ULF.rpQuoted(FTExportInvoiceNo.Text) & "' "
        '    .Preview()
        'End With
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

#End Region

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        '   Me.ogvRef.OptionsView.ShowAutoFilterRow = False
        ' Me.ogvposum.OptionsView.ShowAutoFilterRow = False
        ' AddHandler RepositoryFNHSysUnitId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        'RemoveHandler FTExportInvoiceNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        'RemoveHandler FTExportInvoiceNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        ' ocmload_Click(sender, e)
        _FormLoad = False
    End Sub

    Private Function GetDataPacking(_InvoiceNo As String, _RawMatId As Integer) As System.Data.DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
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

    Private _pload As Boolean = True
    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs)
        Try
            If _pload Then
                '  _pload = False
                'If FTExportInvoiceNo.Text <> "" Then
                '    Call LoadDataInfo(Me.FTExportInvoiceNo.Text)
                '    '    Call LoadDataPlandPack()
                'End If
                '  - _pload = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            Dim _Cmd As String = ""
            _Cmd = "Exec  dbo.SP_GET_ExprtTrackForPland '" & Me.FNHSysVenderPramId.Text & "', '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipment.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndShipment.Text) & "'"
            Dim _odt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
            Me.ogcPlandM.DataSource = _odt




        Catch ex As Exception

        End Try
    End Sub
End Class




Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports Microsoft.Office.Interop.Excel

Public Class wExportInvPkg

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As System.Data.DataTable
    Private _SystemFilePath As String = System.Windows.Forms.Application.StartupPath & IIf(Microsoft.VisualBasic.Right(System.Windows.Forms.Application.StartupPath, 1) = "\", "", "\") & "Images"
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

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _wInvoiceSelectPoup = New wInvoiceSelectPoup
        HI.TL.HandlerControl.AddHandlerObj(_wInvoiceSelectPoup)
        _ReportPopUp = New wReportExportPopup
        HI.TL.HandlerControl.AddHandlerObj(_ReportPopUp)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wInvoiceSelectPoup.Name.ToString.Trim, _wInvoiceSelectPoup)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ReportPopUp.Name.ToString.Trim, _ReportPopUp)

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
        If (HI.ST.UserInfo.UserName.ToUpper = FTExportInvoiceUser.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub LoadDocumentDetail(Key As String)
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT  FTInvoiceNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice_Detail WITH(NOLOCK) Where FTExportInvoiceNo='" & Key & "'"
            Me.ogcRef.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

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
                                    If .Text = "" Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If

                                    'If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, SysDocType, True, _CmpH).ToString() Then
                                    '    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                    '    Dim _dt AsSystem.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    '    If _dt.Rows.Count <= 0 Then
                                    '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                    '        Obj.Focus()
                                    '        Return False
                                    '    End If
                                    'End If
                                End If
                            End If
                        End With
                End Select
            Next
        Next
        Return True
    End Function

    Private Function _SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _SysInvoiceKey As String = ""
            If FTExportInvoiceNo.Text = HI.TL.Document.GetDocumentNo(SysDBName, SysTableName, "", True, "").ToString() Then
                _SysInvoiceKey = HI.TL.Document.GetDocumentNo(SysDBName, SysTableName, "", False, "").ToString()
            Else
                _SysInvoiceKey = FTExportInvoiceNo.Text
            End If

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice"
            _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTShipper='" & HI.UL.ULF.rpQuoted(Me.FTShipper.Text) & "'"
            _Cmd &= vbCrLf & ",FTShippedOnDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTShippedOnDate.Text) & "'"
            _Cmd &= vbCrLf & ",FTETA='" & HI.UL.ULF.rpQuoted(Me.FTETA.Text) & "'"
            _Cmd &= vbCrLf & ",FTETD='" & HI.UL.ULF.rpQuoted(Me.FTETD.Text) & "'"
            _Cmd &= vbCrLf & ",FNHSysTermOfPMId=" & Integer.Parse("0" & Me.FNHSysTermOfPMId.Properties.Tag.ToString)
            _Cmd &= vbCrLf & ",FTIssBankName='" & HI.UL.ULF.rpQuoted(Me.FTIssBankName.Text) & "'"
            _Cmd &= vbCrLf & ",FTIssBankAccNo='" & HI.UL.ULF.rpQuoted(Me.FTIssBankAccNo.Text) & "'"
            _Cmd &= vbCrLf & ",FTIssBankAddr='" & HI.UL.ULF.rpQuoted(Me.FTIssBankAddr.Text) & "'"
            _Cmd &= vbCrLf & ",FTIssBankTel='" & HI.UL.ULF.rpQuoted(Me.FTIssBankTel.Text) & "'"
            _Cmd &= vbCrLf & ",FTIssBankSwiftCode='" & HI.UL.ULF.rpQuoted(Me.FTIssBankSwiftCode.Text) & "'"
            _Cmd &= vbCrLf & ",FNExchangeRate=" & Me.FNExchangeRate.Value
            _Cmd &= vbCrLf & ",FNHSysCurId=" & Val("0" & Me.FNHSysCurId.Properties.Tag)
            _Cmd &= vbCrLf & ",FTBookingNo='" & HI.UL.ULF.rpQuoted(Me.FTBookingNo.Text) & "'"
            '_Cmd &= vbCrLf & ",FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            _Cmd &= vbCrLf & "where FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(_SysInvoiceKey) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice"
                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTExportInvoiceNo, FDExportInvoiceDate, FTExportInvoiceUser, FNHSysCmpId, FTShipper, FTShippedOnDate, FTETA, FTETD, FNHSysTermOfPMId"
                _Cmd &= vbCrLf & ",FTIssBankName ,FTIssBankAccNo,FTIssBankAddr,FTIssBankTel,FTIssBankSwiftCode,FNExchangeRate,FNHSysCurId,FTBookingNo)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SysInvoiceKey) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDExportInvoiceDate.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceUser.Text) & "'"
                _Cmd &= vbCrLf & "," & HI.ST.SysInfo.CmpID
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTShipper.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTShippedOnDate.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTETA.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTETD.Text) & "'"
                _Cmd &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysTermOfPMId.Properties.Tag.ToString)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTIssBankName.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTIssBankAccNo.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTIssBankAddr.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTIssBankTel.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTIssBankSwiftCode.Text) & "'"
                _Cmd &= vbCrLf & "," & Me.FNExchangeRate.Value
                _Cmd &= vbCrLf & "," & Val("0" & Me.FNHSysCurId.Properties.Tag)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBookingNo.Text) & "'"
                '_Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If
            If Not (_SaveDetail(_SysInvoiceKey)) Then
                Return False
            End If

            If Not (_SaveDetailPrice(_SysInvoiceKey)) Then
                Return False
            End If



            If Not (_SaveDetailWeight(_SysInvoiceKey)) Then
                Return False
            End If


            Me.FTExportInvoiceNo.Text = _SysInvoiceKey
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            SaveDataDetail()
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function _SaveDetailPrice(key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            With CType(ogcbreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            Dim _i As Integer = 0
            For Each R As DataRow In _oDt.Rows
                For Each x As DataColumn In _oDt.Columns
                    If Microsoft.VisualBasic.Left(x.ColumnName.ToString, 1) = "c" And R.Item(x.ColumnName).ToString <> "" Then
                        _i += 1
                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_price"
                        _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FNPrice=" & Double.Parse(R.Item(x.ColumnName).ToString)
                        _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
                        _Cmd &= vbCrLf & "and FTColorway='" & R!FTColorway.ToString & "'"
                        _Cmd &= vbCrLf & "and FTSizeBreakDown='" & Replace(x.ColumnName.ToString, "c", "") & "'"
                        _Cmd &= vbCrLf & "and FTCustomerPO='" & R!FTCustomerPO.ToString & "'"
                        _Cmd &= vbCrLf & "and FTInvoiceNo='" & R!FTInvoiceNo.ToString & "'"
                        _Cmd &= vbCrLf & "and FNSeq=" & _i

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_price"
                            _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTExportInvoiceNo,FTCustomerPO, FTColorway, FTSizeBreakDown, FNPrice,FTInvoiceNo , FNSeq)"
                            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & key & "'"
                            _Cmd &= vbCrLf & ",'" & R!FTCustomerPO.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & Replace(x.ColumnName.ToString, "c", "") & "'"
                            _Cmd &= vbCrLf & "," & Double.Parse(R.Item(x.ColumnName).ToString)
                            _Cmd &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "'"
                            _Cmd &= vbCrLf & "," & _i

                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If
                        End If
                    End If
                Next
            Next

            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_price"
            _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
            _Cmd &= vbCrLf & "and FNSeq > " & _i
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function _SaveDetailWeight(key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            With CType(ogcbreakdownweight.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            For Each R As DataRow In _oDt.Rows
                For Each x As DataColumn In _oDt.Columns
                    If Microsoft.VisualBasic.Left(x.ColumnName.ToString, 1) = "c" And R.Item(x.ColumnName).ToString <> "" Then
                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Weight"
                        _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FNWeight=" & Double.Parse(R.Item(x.ColumnName).ToString)
                        _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
                        _Cmd &= vbCrLf & "and FTColorway='" & R!FTColorway.ToString & "'"
                        _Cmd &= vbCrLf & "and FTSizeBreakDown='" & Replace(x.ColumnName.ToString, "c", "") & "'"
                        _Cmd &= vbCrLf & "and FTCustomerPO='" & R!FTCustomerPO.ToString & "'"
                        _Cmd &= vbCrLf & "and FTInvoiceNo='" & R!FTInvoiceNo.ToString & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Weight"
                            _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTExportInvoiceNo,FTCustomerPO, FTColorway, FTSizeBreakDown, FNWeight,FTInvoiceNo)"
                            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & key & "'"
                            _Cmd &= vbCrLf & ",'" & R!FTCustomerPO.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & Replace(x.ColumnName.ToString, "c", "") & "'"
                            _Cmd &= vbCrLf & "," & Double.Parse(R.Item(x.ColumnName).ToString)
                            _Cmd &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "'"

                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If
                        End If
                    End If
                Next
            Next
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function _SaveDetail(key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            With CType(ogcRef.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            Dim _i As Integer = 0
            For Each R As DataRow In _oDt.Rows
                _i += +1
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Detail"
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
                _Cmd &= vbCrLf & "and FTInvoiceNo='" & R!FTInvoiceNo.ToString & "'"
                _Cmd &= vbCrLf & "and FNSeq=" & _i

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Detail"
                    _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTExportInvoiceNo,FTInvoiceNo,FNSeq)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & key & "'"
                    _Cmd &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "'"
                    _Cmd &= vbCrLf & "," & _i

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            Next
            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Detail"
            _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
            _Cmd &= vbCrLf & "and FNSeq > " & _i
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub LoadOrderBreakDown(Key As Object)
        Dim _dt As New System.Data.DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _Filter As String = ""
        With _dt
            .Columns.Add("FTInvoiceNo", GetType(String))
            .Columns.Add("FTCustomerPO", GetType(String))
            .Columns.Add("FTColorway", GetType(String))
        End With

        _Qry = "SELECT DISTINCT I.FTCustomerPO, I.FTInvoiceNo, I.FNHsysCmpID,  D.FTColorway, D.FTSizeBreakDown ,isnull(C.FNPrice , 0 ) AS FNPrice"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS D WITH (NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS P WITH (NOLOCK) ON O.FNHSysPlantId = P.FNHSysPlantId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Price AS C WITH(NOLOCK) ON I.FTInvoiceNo = C.FTInvoiceNo"
        _Qry &= vbCrLf & " and D.FTColorway= C.FTColorway and D.FTSizeBreakDown = C.FTSizeBreakDown and I.FTCustomerPO = C.FTCustomerPO"
        _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo IN ('" & Key & "'))"
        _Qry &= vbCrLf & "and I.FTStateWHApp='1'  and I.FTInvoiceNo <> '' "
        If Me.FTExportInvoiceNo.Text <> "" And IsDate(Microsoft.VisualBasic.Right(Me.FTExportInvoiceNo.Text, 4)) Then
            _Qry &= vbCrLf & " and C.FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
        End If
        _Qry &= vbCrLf & "Order BY I.FTCustomerPO ASC ,D.FTColorway ASC , D.FTSizeBreakDown ASC"
        Dim _oDt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each R As DataRow In _oDt.Rows
            _Filter = "FTInvoiceNo='" & R!FTInvoiceNo.ToString & "' and FTCustomerPO='" & R!FTCustomerPO.ToString & "' and FTColorway='" & R!FTColorway.ToString & "'"

            If _dt.Select(_Filter).Length <= 0 Then
                _dt.Rows.Add(R!FTInvoiceNo.ToString, R!FTCustomerPO.ToString, R!FTColorway.ToString)
            End If
            If _dt.Columns.IndexOf("c" & R!FTSizeBreakDown.ToString) < 0 Then
                _dt.Columns.Add("c" & R!FTSizeBreakDown.ToString, GetType(Double))
            End If
            For Each x As DataRow In _dt.Select(_Filter)
                x.Item("c" & R!FTSizeBreakDown.ToString) = R!FNPrice.ToString
            Next
        Next

        ' _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.ogvbreakdown
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
                        Case Else
                            _colcount = _colcount + 1

                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = Col.ColumnName.ToString
                                .Caption = Replace(Col.ColumnName.ToString, "c", "")
                                .ColumnEdit = ReposPrice
                                .VisibleIndex = _SizeIndex(Replace(Col.ColumnName.ToString, "c", ""))
                            End With

                            .Columns.Add(ColG)
                            With .Columns(Col.ColumnName.ToString)
                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n2}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            End With
                            '.Columns(Col.ColumnName.ToString).Width = 70
                            '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select
                Next
            End If
        End With

        Me.ogcbreakdown.DataSource = _dt.Copy ' _dt.Copy
        Me.ogcbreakdown.Refresh()

    End Sub

    Private Sub LoadOrderBreakDown_Weight(Key As Object)
        Dim _dt As New System.Data.DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _Filter As String = ""
        With _dt
            .Columns.Add("FTInvoiceNo", GetType(String))
            .Columns.Add("FTCustomerPO", GetType(String))
            .Columns.Add("FTColorway", GetType(String))
        End With

        _Qry = "SELECT DISTINCT I.FTCustomerPO, I.FTInvoiceNo, I.FNHsysCmpID,  D.FTColorway, D.FTSizeBreakDown ,isnull(C.FNWeight , 0 ) AS FNWeight"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS D WITH (NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS P WITH (NOLOCK) ON O.FNHSysPlantId = P.FNHSysPlantId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Weight AS C WITH(NOLOCK) ON I.FTInvoiceNo = C.FTInvoiceNo"
        _Qry &= vbCrLf & " and D.FTColorway= C.FTColorway and D.FTSizeBreakDown = C.FTSizeBreakDown and I.FTCustomerPO = C.FTCustomerPO"
        _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo IN ('" & Key & "'))"
        _Qry &= vbCrLf & "and I.FTStateWHApp='1'  and I.FTInvoiceNo <> '' "
        If Me.FTExportInvoiceNo.Text <> "" And IsDate(Microsoft.VisualBasic.Right(Me.FTExportInvoiceNo.Text, 4)) Then
            _Qry &= vbCrLf & " and C.FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
        End If
        _Qry &= vbCrLf & "Order BY I.FTCustomerPO ASC ,D.FTColorway ASC , D.FTSizeBreakDown ASC"
        Dim _oDt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each R As DataRow In _oDt.Rows
            _Filter = "FTInvoiceNo='" & R!FTInvoiceNo.ToString & "' and FTCustomerPO='" & R!FTCustomerPO.ToString & "' and FTColorway='" & R!FTColorway.ToString & "'"

            If _dt.Select(_Filter).Length <= 0 Then
                _dt.Rows.Add(R!FTInvoiceNo.ToString, R!FTCustomerPO.ToString, R!FTColorway.ToString)
            End If
            If _dt.Columns.IndexOf("c" & R!FTSizeBreakDown.ToString) < 0 Then
                _dt.Columns.Add("c" & R!FTSizeBreakDown.ToString, GetType(Double))
            End If
            For Each x As DataRow In _dt.Select(_Filter)
                x.Item("c" & R!FTSizeBreakDown.ToString) = R!FNWeight.ToString
            Next
        Next

        ' _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.ogvbreakdownweight
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
                        Case Else
                            _colcount = _colcount + 1

                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = Col.ColumnName.ToString
                                .Caption = Replace(Col.ColumnName.ToString, "c", "")
                                .ColumnEdit = ReposPrice
                                .VisibleIndex = _SizeIndex(Replace(Col.ColumnName.ToString, "c", ""))
                            End With

                            .Columns.Add(ColG)
                            With .Columns(Col.ColumnName.ToString)
                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n2}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            End With

                            '.Columns(Col.ColumnName.ToString).Width = 70
                            '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next
            End If
        End With

        Me.ogcbreakdownweight.DataSource = _dt.Copy ' _dt.Copy
        ogcbreakdownweight.Refresh()

    End Sub

    Private Function _SizeIndex(sizecode As String) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1   FNMatSizeSeq From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize WITH(NOLOCK) Where FTMatSizeCode='" & sizecode & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "999")
        Catch ex As Exception
            Return 9999
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice WHERE FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice_Detail WHERE FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice_Price WHERE FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
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

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me._SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Call LoadDatadetail()
                Call LoadDataPlandPack()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTExportInvoiceNo.Text, Me.Text) = False Then
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
        Me.FTExportInvoiceNo.Text = ""

    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
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
            If _AllReportName <> "" Then

                For Each _ReportName As String In _AllReportName.Split(",")
                    With New HI.RP.Report

                        .FormTitle = Me.Text
                        .ReportFolderName = _LstReport.GetFolderReportValue(_rSelectIndex)
                        .Formular = "{V_ExportInvoice.FTExportInvoiceNo} = '" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
                        .ReportName = _ReportName
                        .Preview()
                    End With
                Next
            Else
                HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            End If
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
        Me.ogvRef.OptionsView.ShowAutoFilterRow = False
        Me.ogvposum.OptionsView.ShowAutoFilterRow = False
        ' AddHandler RepositoryFNHSysUnitId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        RemoveHandler FTExportInvoiceNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler FTExportInvoiceNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly

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
    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTExportInvoiceNo.EditValueChanged
        Try
            If _pload Then
                '  _pload = False
                If FTExportInvoiceNo.Text <> "" Then
                    Call LoadDataInfo(Me.FTExportInvoiceNo.Text)
                    Call LoadDataPlandPack()
                End If
                '  - _pload = True
            End If


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
                    With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, System.Data.DataTable)
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

                                With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, System.Data.DataTable)
                                    .AcceptChanges()
                                    If _Focus = .Rows.Count - 1 Then
                                        .Rows.Add()
                                    End If
                                    .AcceptChanges()
                                    Dim _oDt As System.Data.DataTable = .Copy
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
                '    With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource,System.Data.DataTable)
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

    Private Overloads Function SaveData(_oDt As System.Data.DataTable, RawMatId As Integer) As Boolean
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
                    _Cmd &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
                    _Cmd &= vbCrLf & "AND FNHSysRawMatId=" & Integer.Parse(RawMatId)

                    _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(_Seq)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Packing "
                        _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTInvoiceNo, FNHSysRawMatId, FNRollNo, FNRollToNo, FNQtyPerRoll, FNQtyCTN"
                        _Cmd &= ", FNTotalQty, FNNetWeightPerRoll, FNGrossWeightPerRoll, FNTotalNetWeight, FNTotalGrossWeight,FNSeq)"
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
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
            _Cmd &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
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

    Private Sub FTAddInvoice_Click(sender As Object, e As EventArgs) Handles FTAddInvoice.Click
        Try
            If Me.FTExportInvoiceNo.Text = "" Then
                HI.MG.ShowMsg.mInfo("Pls Input Invoice No. !!!", 1601041038, Me.Text)
                Me.FTExportInvoiceNo.Focus()
                Exit Sub
            End If
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable : Dim _oDtRef As System.Data.DataTable

            HI.TL.HandlerControl.ClearControl(_wInvoiceSelectPoup)
            With _wInvoiceSelectPoup
                _oDt = .GetInvoice(True, Me.FTExportInvoiceNo.Text)
                With CType(ogcRef.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    _oDtRef = .Copy
                End With
                If _oDtRef Is Nothing Or _oDtRef.Rows.Count <= 0 Then
                    .ogcdetail.DataSource = _oDt
                Else
                    Dim _InvoiceNo As String = CType(Me.ogcRef.DataSource, System.Data.DataTable).Rows(0).Item("FTInvoiceNo").ToString
                    Dim _Filter As String = ""
                    For Each R As DataRow In _oDt.Select("FTInvoiceNo='" & _InvoiceNo & "'")
                        _Filter = "FNHSysContinentId=" & CInt("0" & R!FNHSysContinentId.ToString) & " and FNHSysCountryId=" & CInt("0" & R!FNHSysCountryId.ToString)
                        _Filter &= " and FNHSysShipModeId=" & CInt("0" & R!FNHSysShipModeId.ToString) & " and FNHSysProvinceId=" & CInt("0" & R!FNHSysProvinceId.ToString)
                    Next
                    _oDt.BeginInit()
                    For Each R As DataRow In CType(Me.ogcRef.DataSource, System.Data.DataTable).Rows
                        For Each x As DataRow In _oDt.Select("FTInvoiceNo='" & R!FTInvoiceNo.ToString & "' and " & _Filter)
                            x!FTSelect = "1"
                        Next
                    Next
                    _oDt.EndInit()
                    If .ogcdetail.DataSource Is Nothing Then
                        .ogcdetail.DataSource = _oDt
                    End If
                    .ogcdetail.DataSource = _oDt.Select(_Filter).CopyToDataTable
                End If
                .ShowDialog()
                If (.Proc) Then
                    _oDt = CType(.ogcdetail.DataSource, System.Data.DataTable).Copy
                    Me.ogcRef.DataSource = _oDt.Select("FTSelect='1'").CopyToDataTable
                Else
                    Exit Sub
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvRef_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvRef.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                With ogvRef
                    If .RowCount < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                    Dim _invoiceNo As String = .GetRowCellValue(.FocusedRowHandle, "FTInvoiceNo").ToString
                    .DeleteRow(.FocusedRowHandle)
                End With
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDatadetail()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As System.Data.DataTable

            If Not (Me.ogcRef.DataSource Is Nothing) Then
                With CType(Me.ogcRef.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    _dtdoc = .Copy
                End With

                For Each R As DataRow In _dtdoc.Rows
                    If R!FTInvoiceNo.ToString <> "" Then
                        If _DocNo = "" Then
                            _DocNo = R!FTInvoiceNo.ToString
                        Else
                            _DocNo = _DocNo & "','" & R!FTInvoiceNo.ToString
                        End If
                    End If
                Next
            End If
            _Cmd = "SELECT     distinct  M.FTCustCode ,  I.FTCustomerPO, I.FTInvoiceNo, I.FDInvoiceDate, I.FNHsysCmpID, I.FTStateWHApp, I.FTInvoiceExportNo, I.FDInvoiceExportDate, I.FTInvoiceExportNote, I.FNHSysContinentId, I.FNHSysCountryId, "
            _Cmd &= vbCrLf & "    I.FNHSysProvinceId, I.FNHSysShipModeId, I.FNHSysShipPortId, I.FNTotalCarton, I.FTStateHanger, D.FTColorway, D.FTSizeBreakDown, D.FNQuantity, D.FTPOLineItem, I.FNHSysCurId"
            _Cmd &= vbCrLf & ",P.FTPlantCode as FTMarkNo ,Isnull(C.FNPrice , 0) AS FNUintPrice , (Isnull(C.FNPrice , 0) * D.FNQuantity ) AS FNAmount , '' AS FTDescOfGoods "
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTFactoryCMInvoice_D AS D WITH (NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON I.FTCustomerPO = O.FTPORef"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPlant AS P WITH(NOLOCK) ON O.FNHSysPlantId = P.FNHSysPlantId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Price AS C WITH(NOLOCK) ON I.FTInvoiceNo = C.FTInvoiceNo"
            _Cmd &= vbCrLf & " and D.FTColorway= C.FTColorway and D.FTSizeBreakDown = C.FTSizeBreakDown and I.FTCustomerPO = C.FTCustomerPO"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS M WITH(NOLOCK) ON O.FNHSysCustId = M.FNHSysCustId "
            _Cmd &= vbCrLf & " Where I.FTInvoiceNo in('" & _DocNo & "') and I.FTInvoiceNo <> '' "
            If Me.FTExportInvoiceNo.Text <> "" And IsDate(Microsoft.VisualBasic.Right(Me.FTExportInvoiceNo.Text, 4)) Then
                _Cmd &= vbCrLf & " and C.FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
            End If
            _Cmd &= vbCrLf & "Order by P.FTPlantCode ASC"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.FNHSysCustId.Text = _oDt.Rows(0).Item("FTCustCode").ToString
            Me.ogcdetail.DataSource = _oDt
            Call LoadOrderBreakDown(_DocNo)
            Call LoadOrderBreakDown_Weight(_DocNo)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvRef_RowCountChanged(sender As Object, e As EventArgs) Handles ogvRef.RowCountChanged
        Try
            Call LoadDatadetail()
            Me.FNCartonQty.Value = GetCartonQty()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetCartonQty() As Integer
        Try
            Dim _Cmd As String = "" : Dim _Filter As String = ""
            With DirectCast(Me.ogcRef.DataSource, System.Data.DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    If _Filter <> "" Then _Filter &= ","
                    _Filter &= "'" & R!FTInvoiceNo.ToString & "'"
                Next
            End With
            If _Filter = "" Then Return 0
            _Cmd = "select sum(isnull(FNTotalCarton,0)) AS FNTotalCarton  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTFactoryCMInvoice WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTInvoiceNo in (" & _Filter & ")"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, 0)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub RepositoryFNUintPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNUintPrice.EditValueChanging
        Try
            With ogvdetail
                If .RowCount < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                Dim _Qty As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                Dim _Amt As Double = _Qty * e.NewValue
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", _Amt)
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReposPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposPrice.EditValueChanging
        Try
            If e.OldValue Is Nothing Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposPrice_KeyDown(sender As Object, e As KeyEventArgs) Handles ReposPrice.KeyDown
        Try
            With DirectCast(DirectCast(DirectCast(sender, DevExpress.XtraEditors.CalcEdit).Parent, DevExpress.XtraGrid.GridControl).DefaultView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F9
                        Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                        For I As Integer = 0 To .RowCount - 1
                            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                Select Case GridCol.FieldName.ToString.ToUpper
                                    Case "FTCustomerPO".ToUpper, "FTColorway".ToUpper, "FTInvoiceNo".ToUpper
                                    Case Else
                                        If Not (.GetRowCellValue(I, GridCol.FieldName.ToString).ToString = "") Then
                                            .SetRowCellValue(I, GridCol.FieldName.ToString, _Value)
                                        End If
                                End Select
                            Next
                        Next
                        CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
                        ogcbreakdown.RefreshDataSource()
                    Case Keys.F10
                        Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            Select Case GridCol.FieldName.ToString.ToUpper
                                Case "FTCustomerPO".ToUpper, "FTColorway".ToUpper, "FTInvoiceNo".ToUpper
                                Case Else
                                    If Not (.GetFocusedRowCellValue(GridCol.FieldName.ToString).ToString = "") Then
                                        .SetFocusedRowCellValue(GridCol.FieldName.ToString, _Value)
                                    End If
                            End Select
                        Next
                        CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
                        ogcbreakdown.RefreshDataSource()
                    Case Keys.F11
                        Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value

                        For I As Integer = 0 To .RowCount - 1
                            If Not (.GetRowCellValue(I, .FocusedColumn.FieldName.ToString).ToString = "") Then
                                .SetRowCellValue(I, .FocusedColumn.FieldName.ToString, _Value)
                            End If
                        Next
                        CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
                        ogcbreakdown.RefreshDataSource()
                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCurId.EditValueChanged
        Try
            If _FormLoad Then Exit Sub
            If FNHSysCurId.Text = "" Then
                FNExchangeRate.Value = 0
                Exit Sub
            End If
            If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then
                'FNExchangeRate.Properties.ReadOnly = True
                If Not (_ProcLoad) Then
                    FNExchangeRate.Value = 1
                End If
            Else

                '  FNExchangeRate.Properties.ReadOnly = True
                If Not (_ProcLoad) Then
                    FNExchangeRate.Value = 0
                    Dim _Qry As String = ""

                    _Qry = " SELECT TOP 1 FNSellingRate"
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                    _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDExportInvoiceDate.Text) & "')"
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
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try
            Dim _Cmd As String = ""


            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = ""
                .Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx|Excel Worksheets(97-2003)|*.xls"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    _FileName = .FileName
                    ' Me.FTFileName.Text = _FileName.ToString
                    Call ReadXlsfile(_FileName)
                    ' Dim stream As New IO.FileStream("Documents\Document.xlsx", FileMode.Open)
                    '   Me.ogrsheetOriginal.LoadDocument(_FileName)
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub otab_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otab.SelectedPageChanged
        Try
            'If e.Page.Name = "otpweight" Then
            '    ocmReadExcel.Visible = True
            'Else
            '    ocmReadExcel.Visible = False
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReadXlsfile(_fileName As String)
        Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Import Data From File ")
        Try
            Dim _oDt As New System.Data.DataTable
            Dim _Qry As String = ""
            Dim _RowDes As Integer = 0
            Dim xlsFilename As String = Path.GetFileName(_fileName)
            _oDt = HI.UL.ReadExcel.Read(_fileName, "Detail", -1)

            If Not (_oDt Is Nothing) Then
                Dim _dt As New System.Data.DataTable
                With _dt
                    .Columns.Add("FTPORef", GetType(String))
                    .Columns.Add("FTPONo", GetType(String))
                    .Columns.Add("FNQuantity", GetType(Integer))
                    .Columns.Add("FNPackcount", GetType(Integer))
                    .Columns.Add("FNNet", GetType(Double))
                    .Columns.Add("FNTotalNet", GetType(Double))
                    .Columns.Add("FNGrossWeight", GetType(Double))
                    .Columns.Add("FNHSysUnitId", GetType(Integer))
                    .Columns.Add("FTUnitCode", GetType(String))
                    .Columns.Add("FNVol", GetType(Double))
                    .Columns.Add("FTVolUnit", GetType(String))
                End With

                Dim _RecodeCheck As Integer = 0
                If _oDt.Rows(5).Item(0).ToString = "PO SUMMARY" Then
                    _RecodeCheck = 0
                ElseIf _oDt.Rows(5).Item(0).ToString = "" And _oDt.Rows(5).Item(1).ToString = "PO SUMMARY" Then
                    _RecodeCheck = 1
                End If


                For Each R As DataRow In _oDt.Rows
                    If _oDt.Rows(5).Item(0 + _RecodeCheck).ToString = "PO SUMMARY" And (IsNumeric(R.Item(1 + _RecodeCheck).ToString) Or R.Item(1 + _RecodeCheck).ToString = "Totals") And IsNumeric(R.Item(3 + _RecodeCheck).ToString) Then

                        Dim _dr As DataRow = _dt.NewRow
                        _dr("FTPORef") = _oDt.Rows(2).Item(9).ToString
                        _dr("FTPONo") = R.Item(1 + _RecodeCheck).ToString
                        _dr("FNQuantity") = R.Item(3 + _RecodeCheck).ToString
                        _dr("FNPackcount") = R.Item(4 + _RecodeCheck).ToString
                        _dr("FNNet") = R.Item(5 + _RecodeCheck).ToString
                        _dr("FNTotalNet") = R.Item(7 + _RecodeCheck).ToString
                        _dr("FNGrossWeight") = R.Item(9 + _RecodeCheck).ToString
                        _dr("FNHSysUnitId") = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysUnitId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) Where FTUnitCode='" & R.Item(11).ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
                        _dr("FTUnitCode") = R.Item(11 + _RecodeCheck).ToString
                        _dr("FNVol") = R.Item(12 + _RecodeCheck).ToString
                        _dr("FTVolUnit") = R.Item(13 + _RecodeCheck).ToString
                        _dt.Rows.Add(_dr)

                    End If

                Next
                If _dt IsNot Nothing Then
                    Me.ogcposum.DataSource = _dt
                End If

                Dim _dtd As New System.Data.DataTable
                With _dtd
                    .Columns.Add("FTPORef", GetType(String))
                    .Columns.Add("FTRange", GetType(String))
                    .Columns.Add("FNFrom", GetType(Integer))
                    .Columns.Add("FNTo", GetType(Integer))
                    .Columns.Add("FTSerialFrom", GetType(String))
                    .Columns.Add("FTSerialTo", GetType(String))
                    .Columns.Add("FTPackInstructionCode", GetType(String))
                    .Columns.Add("FTLineNo", GetType(String))
                    .Columns.Add("FTStyleCode", GetType(String))
                    .Columns.Add("FTSKU", GetType(String))
                    .Columns.Add("FTPONo", GetType(String))
                    .Columns.Add("FTPOLineNo", GetType(String))
                    .Columns.Add("FTColorWay", GetType(String))
                    .Columns.Add("FTSizeBreakDown", GetType(String))
                    .Columns.Add("FTShortDescription", GetType(String))
                    .Columns.Add("FTShipmentMethod", GetType(String))
                    .Columns.Add("FNItemQty", GetType(Integer))
                    .Columns.Add("FNQtyPerPack", GetType(Integer))
                    .Columns.Add("FNInnerPackCount", GetType(Integer))
                    .Columns.Add("FNPackCount", GetType(Integer))
                    .Columns.Add("FTR", GetType(String))
                    .Columns.Add("FTPackCode", GetType(String))
                    .Columns.Add("FNNetWeight", GetType(Double))
                    .Columns.Add("FNTotalNetWeight", GetType(Double))
                    .Columns.Add("FNGrossNetWeight", GetType(Double))
                    .Columns.Add("FTUnitCode", GetType(String))
                    .Columns.Add("FNL", GetType(Double))
                    .Columns.Add("FNW", GetType(Double))
                    .Columns.Add("FNH", GetType(Double))
                    .Columns.Add("FTItemUnitCode", GetType(String))
                    .Columns.Add("FTScanID", GetType(String))
                End With


                For Each R As DataRow In _oDt.Rows

                    If _oDt.Rows(12).Item(0 + _RecodeCheck).ToString = "PACKAGE DETAIL" And IsNumeric(R.Item(3 + _RecodeCheck).ToString) And IsNumeric(R.Item(4 + _RecodeCheck).ToString) And IsNumeric(R.Item(5 + _RecodeCheck).ToString) And R.Item(1 + _RecodeCheck).ToString = "" Then

                        Dim _dr As DataRow = _dtd.NewRow
                        _dr("FTPORef") = _oDt.Rows(2).Item(9).ToString
                        _dr("FTRange") = R.Item(3 + _RecodeCheck).ToString
                        _dr("FNFrom") = R.Item(4 + _RecodeCheck).ToString
                        _dr("FNTo") = R.Item(5 + _RecodeCheck).ToString
                        _dr("FTSerialFrom") = R.Item(7 + _RecodeCheck).ToString
                        _dr("FTSerialTo") = R.Item(9 + _RecodeCheck).ToString
                        _dr("FTPackInstructionCode") = R.Item(11 + _RecodeCheck).ToString
                        _dr("FTLineNo") = R.Item(12 + _RecodeCheck).ToString
                        _dr("FTStyleCode") = R.Item(13 + _RecodeCheck).ToString
                        _dr("FTSKU") = R.Item(14 + _RecodeCheck).ToString
                        _dr("FTPONo") = R.Item(16 + _RecodeCheck).ToString
                        _dr("FTPOLineNo") = R.Item(17 + _RecodeCheck).ToString
                        _dr("FTColorWay") = R.Item(18 + _RecodeCheck).ToString
                        _dr("FTSizeBreakDown") = R.Item(20 + _RecodeCheck).ToString
                        _dr("FTShortDescription") = R.Item(21 + _RecodeCheck).ToString
                        _dr("FTShipmentMethod") = R.Item(22 + _RecodeCheck).ToString
                        _dr("FNItemQty") = R.Item(23 + _RecodeCheck).ToString
                        _dr("FNQtyPerPack") = R.Item(24 + _RecodeCheck).ToString
                        _dr("FNInnerPackCount") = Integer.Parse("0" & R.Item(25 + _RecodeCheck).ToString)
                        _dr("FNPackCount") = R.Item(26 + _RecodeCheck).ToString
                        _dr("FTR") = R.Item(27 + _RecodeCheck).ToString
                        _dr("FTPackCode") = R.Item(28 + _RecodeCheck).ToString
                        _dr("FNNetWeight") = R.Item(29 + _RecodeCheck).ToString
                        _dr("FNTotalNetWeight") = R.Item(30 + _RecodeCheck).ToString
                        _dr("FNGrossNetWeight") = R.Item(31 + _RecodeCheck).ToString
                        _dr("FTUnitCode") = R.Item(33 + _RecodeCheck).ToString
                        _dr("FNL") = R.Item(35 + _RecodeCheck).ToString
                        _dr("FNW") = R.Item(36 + _RecodeCheck).ToString
                        _dr("FNH") = R.Item(38 + _RecodeCheck).ToString
                        _dr("FTItemUnitCode") = R.Item(39 + _RecodeCheck).ToString
                        _dr("FTScanID") = R.Item(40 + _RecodeCheck).ToString

                        _dtd.Rows.Add(_dr)

                    End If

                Next
                If _dt IsNot Nothing Then
                    Me.ogcpackdetail.DataSource = _dtd
                End If
                _RowDes = _oDt.Rows.Count



                Dim _Cmd As String = ""
                Dim _xDt As System.Data.DataTable
                _Cmd = "      Select   distinct  G.FTGenderNameEN +' '+ P.FTProdTypeNameEN +' '+T.FTStyleNameEN as Descrition  , V.FTProvinceCode , V.FTProvinceNameEN + ',' + C.FTCountryNameEN AS Destination "
                _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (Nolock) LEFT OUTER Join"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH (NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
                _Cmd &= vbCrLf & "	Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown As D With(NOLOCK) On O.FTOrderNo = D.FTOrderNo And S.FTSubOrderNo = D.FTSubOrderNo"
                _Cmd &= vbCrLf & "	Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender as G WITH(NOLOCK) ON S.FNHSysGenderId = G.FNHSysGenderId "
                _Cmd &= vbCrLf & "  Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType As P With(NOLOCK) On O.FNHSysProdTypeId = P.FNHSysProdTypeId"
                _Cmd &= vbCrLf & "  Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH(NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId"
                _Cmd &= vbCrLf & "  Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince As V With(NOLOCK) On S.FNHSysProvinceId = V.FNHSysProvinceId"
                _Cmd &= vbCrLf & "  Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS C WITH(NOLOCK) ON S.FNHSysCountryId = C.FNHSysCountryId"
                _Cmd &= vbCrLf & "where  D.FTNikePOLineItem In ( '10' ) "
                _Cmd &= vbCrLf & "And   case when  ISNULL( S.FTPORef , '') <> ''  then  ISNULL( S.FTPORef , '') else ISNULL(O.FTPORef , '') end = '4504644214'"
                _xDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                'Dim excel As New Microsoft.Office.Interop.Excel.Application
                'Dim workbook As Workbook
                'Dim sheet As Worksheet
                ' Dim rx As Range
                '  Dim array(,) As Object

                'workbook = excel.Workbooks.Open(_fileName)
                'workbook = excel.ActiveWorkbook
                'sheet = workbook.Worksheets(2)

                'rx = sheet.UsedRange

                '' Load all cells into 2d array.
                'array = rx.Value(XlRangeValueDataType.xlRangeValueDefault)

                '' Get bounds of the array.
                'Dim bound0 As Integer = Array.GetUpperBound(0) 'last row number
                'Dim bound1 As Integer = Array.GetUpperBound(1) 'last column number

                ''get total number of rows
                'Dim totalrows As Integer = bound0 - 1 'since 1st row is header



                'With workbook.Worksheets(2)
                '    '.Cells(6, 42).Formula = " = (E37 * 100) / (F6 + F" & (_oDtS.Rows.Count + 5).ToString & "/2)"
                '    '.Cells(6, 42).NumberFormat = "0.00"
                '    .Range(.Cells(6, 23), .Cells(6, 25)).Merge
                '    .Range(.Cells(7, 23), .Cells(7, 25)).Merge
                '    .Cells(6, 23) = "'" & _xDt.Rows(0).Item("FTProvinceCode").ToString
                '    .Cells(7, 23) = "'" & _xDt.Rows(0).Item("Destination").ToString
                '    .Cells(6, 23).Style.VerticalAlignment = 1
                '    .Cells(7, 23).Style.VerticalAlignment = 1

                '    Dim _row As Integer = 0
                '    For Each x As DataRow In _xDt.Rows
                '        .Cells(_RowDes + 2 + _row, 1) = "DESCIPTION:"
                '        .Cells(_RowDes + 2 + _row, 3) = " " & x!Descrition.ToString
                '        _row += +1
                '    Next

                'End With



                ''  sheet.Cells(1, 21) = "DESCIPTION:"
                'workbook.Save()
                'workbook.Close()
                'excel.Quit()

                Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
                Dim TmpFile As String = _Path & "\ExportPland\"

                Dim DestPath As String = TmpFile
                If Not Directory.Exists(DestPath) Then
                    Directory.CreateDirectory(DestPath)
                End If
                Dim file = New FileInfo(_fileName)
                file.CopyTo(Path.Combine(DestPath, file.Name), True)
                Try
                    My.Computer.FileSystem.DeleteFile(DestPath & Me.FTExportInvoiceNo.Text.ToString & ".Xlsx")
                Catch ex As Exception

                End Try


                My.Computer.FileSystem.RenameFile(DestPath & file.Name, Me.FTExportInvoiceNo.Text.ToString & ".Xlsx")

                _Spls.Close()
                Me.SpreadsheetControl1.LoadDocument(_fileName)
                'Process.Start(_fileName)



            Else
                HI.MG.ShowMsg.mInfo("Invalid Sheet Name In Excel File..", 1509281139, Me.Text, "Detail")
                Exit Sub
            End If
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Function SaveDataDetail() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable()

            With DirectCast(Me.ogcposum.DataSource, System.Data.DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    If IsNumeric(R!FTPONo.ToString) Then

                        _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Pland "
                        _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ", FNQuantity=" & R!FNQuantity.ToString
                        _Cmd &= vbCrLf & ", FNPackcount=" & R!FNPackcount.ToString
                        _Cmd &= vbCrLf & ", FNNet=" & R!FNNet.ToString
                        _Cmd &= vbCrLf & ", FNTotalNet=" & R!FNTotalNet.ToString
                        _Cmd &= vbCrLf & ", FNGrossWeight=" & R!FNGrossWeight.ToString
                        _Cmd &= vbCrLf & ", FNHSysUnitId=" & Integer.Parse(Val(R!FNHSysUnitId.ToString))
                        _Cmd &= vbCrLf & ", FTUnitCode='" & R!FTUnitCode.ToString & "'"
                        _Cmd &= vbCrLf & ", FNVol=" & R!FNVol.ToString
                        _Cmd &= vbCrLf & ", FTVolUnit='" & R!FTVolUnit.ToString & "'"
                        _Cmd &= vbCrLf & " where  FTPORef='" & R!FTPORef.ToString & "'"
                        _Cmd &= vbCrLf & " and   FTPONo='" & R!FTPONo.ToString & "'"
                        _Cmd &= vbCrLf & "and FTExportInvoiceNo='" & Me.FTExportInvoiceNo.Text & "'"
                        If Not HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT) Then
                            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Pland  "
                            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,   FTPORef, FTPONo, FNQuantity, FNPackcount, FNNet, FNTotalNet, FNGrossWeight, FNHSysUnitId, FTUnitCode, FNVol, FTVolUnit ,FTExportInvoiceNo)"
                            _Cmd &= vbCrLf & "Select  '" & HI.ST.UserInfo.UserName & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & R!FTPORef.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & R!FTPONo.ToString & "'"
                            _Cmd &= vbCrLf & "," & R!FNQuantity.ToString
                            _Cmd &= vbCrLf & "," & R!FNPackcount.ToString
                            _Cmd &= vbCrLf & "," & R!FNNet.ToString
                            _Cmd &= vbCrLf & "," & R!FNTotalNet.ToString
                            _Cmd &= vbCrLf & "," & R!FNGrossWeight.ToString
                            _Cmd &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysUnitId.ToString))
                            _Cmd &= vbCrLf & ",'" & R!FTUnitCode.ToString & "'"
                            _Cmd &= vbCrLf & "," & R!FNVol.ToString
                            _Cmd &= vbCrLf & ",'" & R!FTVolUnit.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & Me.FTExportInvoiceNo.Text & "'"
                            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

                        End If

                    End If


                Next
            End With

            With DirectCast(Me.ogcpackdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()

                'Select    FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTPORef, FTRange, FNFrom, FNTo, FTSerialFrom, FTSerialTo, FTPackInstructionCode, FTLineNo, FTStyleCode, FTSKU, FTPONo, 
                '  FTPOLineNo, FTColorWay, FTSizeBreakDown, FTShortDescription, FTShipmentMethod, FNItemQty, FNQtyPerPack, FNInnerPackCount, FNPackcount, FTR, FTPackCode, FNNetWeight, FNTotalNetWeight,
                '  FNGrossNetWeight, FTUnitCode, FNL, FNW, FNH, FTItemUnitCode, FTScanID
                'From TACCTInvoice_Pland_Detail

                For Each R As DataRow In .Rows
                    _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Pland_Detail "
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ", FNFrom=" & R!FNFrom.ToString
                    _Cmd &= vbCrLf & ", FNTo=" & R!FNTo.ToString
                    _Cmd &= vbCrLf & ", FTSerialFrom='" & R!FTSerialFrom.ToString & "'"
                    _Cmd &= vbCrLf & ", FTSerialTo='" & R!FTSerialTo.ToString & "'"
                    _Cmd &= vbCrLf & ", FTPackInstructionCode='" & R!FTPackInstructionCode.ToString & "'"
                    _Cmd &= vbCrLf & ", FTLineNo='" & R!FTLineNo.ToString & "'"
                    _Cmd &= vbCrLf & ", FTStyleCode='" & R!FTStyleCode.ToString & "'"
                    _Cmd &= vbCrLf & ", FTSKU='" & R!FTSKU.ToString & "'"
                    _Cmd &= vbCrLf & ", FTPONo='" & R!FTPONo.ToString & "'"
                    _Cmd &= vbCrLf & ", FTPOLineNo='" & R!FTPOLineNo.ToString & "'"
                    _Cmd &= vbCrLf & ", FTColorWay='" & R!FTColorWay.ToString & "'"
                    _Cmd &= vbCrLf & ", FTSizeBreakDown='" & R!FTSizeBreakDown.ToString & "'"
                    _Cmd &= vbCrLf & ", FTShortDescription='" & R!FTShortDescription.ToString & "'"
                    _Cmd &= vbCrLf & ", FTShipmentMethod='" & R!FTShipmentMethod.ToString & "'"
                    _Cmd &= vbCrLf & ", FNItemQty=" & R!FNItemQty.ToString
                    _Cmd &= vbCrLf & ", FNQtyPerPack=" & R!FNQtyPerPack.ToString
                    _Cmd &= vbCrLf & ", FNInnerPackCount=" & R!FNInnerPackCount.ToString
                    _Cmd &= vbCrLf & ", FNPackcount=" & R!FNPackcount.ToString
                    _Cmd &= vbCrLf & ", FTR='" & R!FTR.ToString & "'"
                    _Cmd &= vbCrLf & ", FTPackCode='" & R!FTPackCode.ToString & "'"
                    _Cmd &= vbCrLf & ", FNNetWeight=" & R!FNNetWeight.ToString
                    _Cmd &= vbCrLf & ", FNTotalNetWeight=" & R!FNTotalNetWeight.ToString
                    _Cmd &= vbCrLf & ", FNGrossNetWeight=" & R!FNGrossNetWeight.ToString
                    _Cmd &= vbCrLf & ", FTUnitCode='" & R!FTUnitCode.ToString & "'"
                    _Cmd &= vbCrLf & ", FNL=" & R!FNL.ToString
                    _Cmd &= vbCrLf & ", FNW=" & R!FNW.ToString
                    _Cmd &= vbCrLf & ", FNH=" & R!FNH.ToString
                    _Cmd &= vbCrLf & ", FTItemUnitCode='" & R!FTItemUnitCode.ToString & "'"
                    _Cmd &= vbCrLf & ", FTScanID='" & R!FTScanID.ToString & "'"

                    _Cmd &= vbCrLf & " Where  FTPORef = '" & R!FTPORef.ToString & "'"
                    _Cmd &= vbCrLf & "and  FTRange='" & R!FTRange.ToString & "'"
                    _Cmd &= vbCrLf & "and FTExportInvoiceNo='" & Me.FTExportInvoiceNo.Text & "'"

                    If Not HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT) Then

                        _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoice_Pland_Detail "
                        _Cmd &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FTPORef, FTRange, FNFrom, FNTo, FTSerialFrom, FTSerialTo, FTPackInstructionCode, FTLineNo, FTStyleCode, FTSKU, FTPONo, "
                        _Cmd &= vbCrLf & " FTPOLineNo, FTColorWay, FTSizeBreakDown, FTShortDescription, FTShipmentMethod, FNItemQty, FNQtyPerPack, FNInnerPackCount, FNPackcount, FTR, FTPackCode, FNNetWeight, FNTotalNetWeight, "
                        _Cmd &= vbCrLf & " FNGrossNetWeight, FTUnitCode, FNL, FNW, FNH, FTItemUnitCode, FTScanID ,FTExportInvoiceNo) "
                        _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        _Cmd &= vbCrLf & ",'" & R!FTPORef.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTRange.ToString & "'"

                        _Cmd &= vbCrLf & "," & R!FNFrom.ToString
                        _Cmd &= vbCrLf & "," & R!FNTo.ToString
                        _Cmd &= vbCrLf & ",'" & R!FTSerialFrom.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTSerialTo.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTPackInstructionCode.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTLineNo.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTStyleCode.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTSKU.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTPONo.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTPOLineNo.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTColorWay.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTSizeBreakDown.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTShortDescription.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTShipmentMethod.ToString & "'"
                        _Cmd &= vbCrLf & "," & R!FNItemQty.ToString
                        _Cmd &= vbCrLf & "," & R!FNQtyPerPack.ToString
                        _Cmd &= vbCrLf & "," & R!FNInnerPackCount.ToString
                        _Cmd &= vbCrLf & "," & R!FNPackcount.ToString
                        _Cmd &= vbCrLf & ",'" & R!FTR.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTPackCode.ToString & "'"
                        _Cmd &= vbCrLf & "," & R!FNNetWeight.ToString
                        _Cmd &= vbCrLf & "," & R!FNTotalNetWeight.ToString
                        _Cmd &= vbCrLf & "," & R!FNGrossNetWeight.ToString
                        _Cmd &= vbCrLf & ",'" & R!FTUnitCode.ToString & "'"
                        _Cmd &= vbCrLf & "," & R!FNL.ToString
                        _Cmd &= vbCrLf & "," & R!FNW.ToString
                        _Cmd &= vbCrLf & "," & R!FNH.ToString
                        _Cmd &= vbCrLf & ",'" & R!FTItemUnitCode.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTScanID.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & Me.FTExportInvoiceNo.Text & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

                    End If

                Next


            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Private Sub LoadDataPlandPack()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            _Cmd = "Select  *  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo. TACCTInvoice_Pland with(nolock) "
            _Cmd &= vbCrLf & "where  FTExportInvoiceNo='" & Me.FTExportInvoiceNo.Text & "'"
            Me.ogcposum.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "Select  *  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo. TACCTInvoice_Pland_Detail with(nolock) "
            _Cmd &= vbCrLf & "where  FTExportInvoiceNo='" & Me.FTExportInvoiceNo.Text & "'"
            Me.ogcpackdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
            Dim TmpFile As String = _Path & "\ExportPland\"


            Me.SpreadsheetControl1.LoadDocument(TmpFile & Me.FTExportInvoiceNo.Text & ".Xlsx")


        Catch ex As Exception

        End Try
    End Sub
End Class
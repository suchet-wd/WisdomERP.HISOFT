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

Public Class wBookingTracking

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

    Private _dtcuspobreakdown As System.Data.DataTable
    Private _dtcuspobreakdownspare As System.Data.DataTable
    Private _LstReport As HI.RP.ListReport


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()


        Try
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


    Private Function _GetCmd() As String
        Try


        Catch ex As Exception

        End Try
    End Function

    Private Function _SaveData(_Tab As DevExpress.XtraTab.XtraTabPage) As Boolean
        Try

            Dim _Cmd As String = "" : Dim _CmdIns As String = "" : Dim _CmdUpd As String = ""
            Dim _Where As String = ""
            Dim _FieldName As String = ""
            Dim _PKey As String = "FTPckPlanNo" : Dim _FKey As String = "FTPORef"
            Dim _Value As String = "" : Dim _ValueUpd As String = ""
            Dim _Collaction As String = ""
            Dim _StrFileH As String = "FTPckPlanNo|FTPORef|FNQuantity|FNPackcount|FNNet|FNTotalNet|FNGrossWeight|FNHSysUnitId|FNVol|FTVolUnit|FDShipDate|FNHSysProvinceId|FNHSysMainMatSpecId"

            _CmdIns = "Insert into   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan  "
            _CmdIns &= "( FTInsUser, FDInsDate, FTInsTime,FTPckPlanNo, FTPORef, FNQuantity, FNPackcount, FNNet, FNTotalNet, FNGrossWeight, FNHSysUnitId, FNVol, FTVolUnit)"
            _CmdIns &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ","

            _CmdUpd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan "
            _CmdUpd &= vbCrLf & " set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _CmdUpd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _CmdUpd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

            '***********************************************
            Dim _DCmdIns As String = "" : Dim _DCmdUpd As String = ""
            Dim _DStrFileD As String = "FTPckPlanNo|FTPORef|FTRangeNo|FNFrom|FNTo|FTSerialFrom|FTSerialTo|FTPackInstructionCode|FTLineNo|FTStyleCode|FTSKU|"
            _DStrFileD &= "FTPOLineNo|FTColorWay|FTSizeBreakDown|FTShortDescription|FTShipmentMethod|FNItemQty|FNQtyPerPack|FNInnerPackCount|FNPackCount|FTR|FTPackCode|FNNetWeight|FNTotalNetWeight|FNGrossNetWeight|FTUnitCode|"
            _DStrFileD &= "FNL|FNW|FNH|FTItemUnitCode|FTScanID"
            Dim _DPKey As String = "FTPckPlanNo" : Dim _DFKey As String = "FTPORef" : Dim _DFKey2 As String = "FTRangeNo"
            _DCmdIns = "Insert into   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan_D  "
            _DCmdIns &= "(FTInsUser, FDInsDate, FTInsTime,   FTPckPlanNo, FTPORef, FTRangeNo, FNFrom, FNTo, FTSerialFrom, FTSerialTo, FTPackInstructionCode, FTLineNo, FTStyleCode, FTSKU, 
                  FTPOLineNo, FTColorWay, FTSizeBreakDown, FTShortDescription, FTShipmentMethod, FNItemQty, FNQtyPerPack, FNInnerPackCount, FNPackCount, FTR, FTPackCode, FNNetWeight, FNTotalNetWeight, FNGrossNetWeight, FTUnitCode, 
                  FNL, FNW, FNH, FTItemUnitCode, FTScanID)"
            _DCmdIns &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ","


            _DCmdUpd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan_D "
            _DCmdUpd &= vbCrLf & " set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _DCmdUpd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _DCmdUpd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB



            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _oDt As System.Data.DataTable

            Try

                With DirectCast(DirectCast((_Tab.Controls.Find("ogcPlandM" & _Tab.Text, True)(0)), DevExpress.XtraGrid.GridControl).DataSource, System.Data.DataTable)
                    _oDt = .Copy
                    For Each R As DataRow In _oDt.Rows
                        _Value = ""
                        For Each _Str As String In _StrFileH.Split("|")
                            If _Value <> "" Then _Value &= ","
                            If Microsoft.VisualBasic.Left(_Str, 2).ToString = "FT" Then
                                _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item(_Str.ToString)) & "'"

                            ElseIf Microsoft.VisualBasic.Left(_Str, 2).ToString = "FD" Then
                                _Value &= "'" & HI.UL.ULDate.ConvertEnDB(R.Item(_Str.ToString)) & "'"
                            Else

                                If _Str.ToString = "FNHSysProvinceId" Then

                                    _Value &= R.Item(_Str.ToString & "_Hide")
                                Else
                                    _Value &= R.Item(_Str.ToString)
                                End If

                                _Value &= R.Item(_Str.ToString)
                            End If


                            If _PKey = _Str Then
                                _Where = "  WHERE " & _PKey & " = '" & R.Item(_Str.ToString) & "'"
                                If R.Item(_Str.ToString) = "Totals" Then
                                    GoTo 1
                                End If
                            ElseIf _FKey = _Str Then
                                _Where &= vbCrLf & "  AND " & _FKey & " = '" & R.Item(_Str.ToString) & "'"
                                If DirectCast((_Tab.Controls.Find("FTApproveState" & R.Item(_Str.ToString), True)(0)), DevExpress.XtraEditors.CheckEdit).Checked Then

                                    HI.MG.ShowMsg.mInfo("Customer PONo. :" & _FKey & " Saving Fail", 1802021537, R.Item(_Str.ToString), "", MessageBoxIcon.Hand)

                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False

                                End If
                            Else
                                If _ValueUpd <> "" Then _ValueUpd &= ","

                                If _Str.ToString = "FNHSysProvinceId" Then
                                    _ValueUpd &= _Str & " ='" & R.Item(_Str.ToString & "_Hide") & "'"
                                ElseIf Microsoft.VisualBasic.Left(_Str, 2).ToString = "FD" Then
                                    _ValueUpd &= _Str & " ='" & HI.UL.ULDate.ConvertEnDB(R.Item(_Str.ToString)) & "'"
                                Else
                                    _ValueUpd &= _Str & " ='" & R.Item(_Str.ToString) & "'"
                                End If
                            End If
                        Next
                        _Cmd = _CmdUpd & " , " & _ValueUpd & " " & _Where
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            _Cmd = _CmdIns & " " & _Value
                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If
                        End If

                    Next

                End With
1:
                With DirectCast(DirectCast((_Tab.Controls.Find("ogcPlandD" & _Tab.Text, True)(0)), DevExpress.XtraGrid.GridControl).DataSource, System.Data.DataTable)
                    _oDt = .Copy
                    For Each R As DataRow In _oDt.Rows
                        _Value = ""
                        _ValueUpd = ""
                        For Each _Str As String In _DStrFileD.Split("|")
                            If _Value <> "" Then _Value &= ","
                            If Microsoft.VisualBasic.Left(_Str, 2).ToString = "FT" Then
                                _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item(_Str.ToString)) & "'"
                            Else
                                _Value &= R.Item(_Str.ToString)
                            End If


                            If _DPKey = _Str Then
                                _Where = "  WHERE " & _DPKey & " = '" & R.Item(_Str.ToString) & "'"
                            ElseIf _DFKey = _Str Then
                                _Where &= vbCrLf & "  AND " & _DFKey & " = '" & R.Item(_Str.ToString) & "'"

                            ElseIf _DFKey2 = _Str Then
                                _Where &= vbCrLf & "  AND " & _DFKey2 & " = '" & R.Item(_Str.ToString) & "'"
                            Else
                                If _ValueUpd <> "" Then _ValueUpd &= ","
                                _ValueUpd &= _Str & " ='" & R.Item(_Str.ToString) & "'"
                            End If

                        Next
                        _Cmd = _DCmdUpd & " , " & _ValueUpd & " " & _Where
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            _Cmd = _DCmdIns & " " & _Value
                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If
                        End If

                    Next


                End With

            Catch ex As Exception

            End Try

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '    SaveDataDetail()
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function _SaveDetailPrice(key As String) As Boolean
        'Try
        '    Dim _Cmd As String = ""
        '    Dim _oDt As System.Data.DataTable
        '    With CType(ogcbreakdown.DataSource, System.Data.DataTable)
        '        .AcceptChanges()
        '        _oDt = .Copy
        '    End With
        '    Dim _i As Integer = 0
        '    For Each R As DataRow In _oDt.Rows
        '        For Each x As DataColumn In _oDt.Columns
        '            If Microsoft.VisualBasic.Left(x.ColumnName.ToString, 1) = "c" And R.Item(x.ColumnName).ToString <> "" Then
        '                _i += 1
        '                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_price"
        '                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
        '                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
        '                _Cmd &= vbCrLf & ",FNPrice=" & Double.Parse(R.Item(x.ColumnName).ToString)
        '                _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
        '                _Cmd &= vbCrLf & "and FTColorway='" & R!FTColorway.ToString & "'"
        '                _Cmd &= vbCrLf & "and FTSizeBreakDown='" & Replace(x.ColumnName.ToString, "c", "") & "'"
        '                _Cmd &= vbCrLf & "and FTCustomerPO='" & R!FTCustomerPO.ToString & "'"
        '                _Cmd &= vbCrLf & "and FTInvoiceNo='" & R!FTInvoiceNo.ToString & "'"
        '                _Cmd &= vbCrLf & "and FNSeq=" & _i

        '                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_price"
        '                    _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTExportInvoiceNo,FTCustomerPO, FTColorway, FTSizeBreakDown, FNPrice,FTInvoiceNo , FNSeq)"
        '                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
        '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
        '                    _Cmd &= vbCrLf & ",'" & key & "'"
        '                    _Cmd &= vbCrLf & ",'" & R!FTCustomerPO.ToString & "'"
        '                    _Cmd &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
        '                    _Cmd &= vbCrLf & ",'" & Replace(x.ColumnName.ToString, "c", "") & "'"
        '                    _Cmd &= vbCrLf & "," & Double.Parse(R.Item(x.ColumnName).ToString)
        '                    _Cmd &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "'"
        '                    _Cmd &= vbCrLf & "," & _i

        '                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                        HI.Conn.SQLConn.Tran.Rollback()
        '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                        Return False
        '                    End If
        '                End If
        '            End If
        '        Next
        '    Next

        '    _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_price"
        '    _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
        '    _Cmd &= vbCrLf & "and FNSeq > " & _i
        '    HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

        '    Return True
        'Catch ex As Exception
        '    HI.Conn.SQLConn.Tran.Rollback()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '    Return False
        'End Try
    End Function

    Private Function _SaveDetailWeight(key As String) As Boolean
        'Try
        '    Dim _Cmd As String = ""
        '    Dim _oDt As System.Data.DataTable
        '    With CType(ogcbreakdownweight.DataSource, System.Data.DataTable)
        '        .AcceptChanges()
        '        _oDt = .Copy
        '    End With
        '    For Each R As DataRow In _oDt.Rows
        '        For Each x As DataColumn In _oDt.Columns
        '            If Microsoft.VisualBasic.Left(x.ColumnName.ToString, 1) = "c" And R.Item(x.ColumnName).ToString <> "" Then
        '                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Weight"
        '                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
        '                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
        '                _Cmd &= vbCrLf & ",FNWeight=" & Double.Parse(R.Item(x.ColumnName).ToString)
        '                _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
        '                _Cmd &= vbCrLf & "and FTColorway='" & R!FTColorway.ToString & "'"
        '                _Cmd &= vbCrLf & "and FTSizeBreakDown='" & Replace(x.ColumnName.ToString, "c", "") & "'"
        '                _Cmd &= vbCrLf & "and FTCustomerPO='" & R!FTCustomerPO.ToString & "'"
        '                _Cmd &= vbCrLf & "and FTInvoiceNo='" & R!FTInvoiceNo.ToString & "'"

        '                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Weight"
        '                    _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTExportInvoiceNo,FTCustomerPO, FTColorway, FTSizeBreakDown, FNWeight,FTInvoiceNo)"
        '                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
        '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
        '                    _Cmd &= vbCrLf & ",'" & key & "'"
        '                    _Cmd &= vbCrLf & ",'" & R!FTCustomerPO.ToString & "'"
        '                    _Cmd &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
        '                    _Cmd &= vbCrLf & ",'" & Replace(x.ColumnName.ToString, "c", "") & "'"
        '                    _Cmd &= vbCrLf & "," & Double.Parse(R.Item(x.ColumnName).ToString)
        '                    _Cmd &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "'"

        '                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                        HI.Conn.SQLConn.Tran.Rollback()
        '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                        Return False
        '                    End If
        '                End If
        '            End If
        '        Next
        '    Next
        '    Return True
        'Catch ex As Exception
        '    HI.Conn.SQLConn.Tran.Rollback()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '    Return False
        'End Try
    End Function

    Private Function _SaveDetail(key As String) As Boolean
        Try
            'Dim _Cmd As String = ""
            'Dim _oDt As System.Data.DataTable
            'With CType(ogcRef.DataSource, System.Data.DataTable)
            '    .AcceptChanges()
            '    _oDt = .Copy
            'End With
            'Dim _i As Integer = 0
            'For Each R As DataRow In _oDt.Rows
            '    _i += +1
            '    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Detail"
            '    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            '    _Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
            '    _Cmd &= vbCrLf & "and FTInvoiceNo='" & R!FTInvoiceNo.ToString & "'"
            '    _Cmd &= vbCrLf & "and FNSeq=" & _i

            '    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '        _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Detail"
            '        _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,  FTExportInvoiceNo,FTInvoiceNo,FNSeq)"
            '        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '        _Cmd &= vbCrLf & ",'" & key & "'"
            '        _Cmd &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "'"
            '        _Cmd &= vbCrLf & "," & _i

            '        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            Return False
            '        End If
            '    End If
            'Next
            '_Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Detail"
            '_Cmd &= vbCrLf & "Where FTExportInvoiceNo='" & key & "'"
            '_Cmd &= vbCrLf & "and FNSeq > " & _i
            'HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub LoadOrderBreakDown(Key As Object)
        'Dim _dt As New System.Data.DataTable
        'Dim _Qry As String = ""
        'Dim _colcount As Integer = 0
        'Dim _Filter As String = ""
        'With _dt
        '    .Columns.Add("FTInvoiceNo", GetType(String))
        '    .Columns.Add("FTCustomerPO", GetType(String))
        '    .Columns.Add("FTColorway", GetType(String))
        'End With

        '_Qry = "SELECT DISTINCT I.FTCustomerPO, I.FTInvoiceNo, I.FNHsysCmpID,  D.FTColorway, D.FTSizeBreakDown ,isnull(C.FNPrice , 0 ) AS FNPrice"
        '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS D WITH (NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS P WITH (NOLOCK) ON O.FNHSysPlantId = P.FNHSysPlantId"
        '_Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Price AS C WITH(NOLOCK) ON I.FTInvoiceNo = C.FTInvoiceNo"
        '_Qry &= vbCrLf & " and D.FTColorway= C.FTColorway and D.FTSizeBreakDown = C.FTSizeBreakDown and I.FTCustomerPO = C.FTCustomerPO"
        '_Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo IN ('" & Key & "'))"
        '_Qry &= vbCrLf & "and I.FTStateWHApp='1'  and I.FTInvoiceNo <> '' "
        'If Me.FTExportInvoiceNo.Text <> "" And IsDate(Microsoft.VisualBasic.Right(Me.FTExportInvoiceNo.Text, 4)) Then
        '    _Qry &= vbCrLf & " and C.FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
        'End If
        '_Qry &= vbCrLf & "Order BY I.FTCustomerPO ASC ,D.FTColorway ASC , D.FTSizeBreakDown ASC"
        'Dim _oDt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        'For Each R As DataRow In _oDt.Rows
        '    _Filter = "FTInvoiceNo='" & R!FTInvoiceNo.ToString & "' and FTCustomerPO='" & R!FTCustomerPO.ToString & "' and FTColorway='" & R!FTColorway.ToString & "'"

        '    If _dt.Select(_Filter).Length <= 0 Then
        '        _dt.Rows.Add(R!FTInvoiceNo.ToString, R!FTCustomerPO.ToString, R!FTColorway.ToString)
        '    End If
        '    If _dt.Columns.IndexOf("c" & R!FTSizeBreakDown.ToString) < 0 Then
        '        _dt.Columns.Add("c" & R!FTSizeBreakDown.ToString, GetType(Double))
        '    End If
        '    For Each x As DataRow In _dt.Select(_Filter)
        '        x.Item("c" & R!FTSizeBreakDown.ToString) = R!FNPrice.ToString
        '    Next
        'Next

        '' _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'With Me.ogvbreakdown
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next

        '    If Not (_dt Is Nothing) Then
        '        For Each Col As DataColumn In _dt.Columns
        '            Select Case Col.ColumnName.ToString.ToUpper
        '                Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
        '                Case Else
        '                    _colcount = _colcount + 1

        '                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
        '                    With ColG
        '                        .Visible = True
        '                        .FieldName = Col.ColumnName.ToString
        '                        .Name = Col.ColumnName.ToString
        '                        .Caption = Replace(Col.ColumnName.ToString, "c", "")
        '                        .ColumnEdit = ReposPrice
        '                        .VisibleIndex = _SizeIndex(Replace(Col.ColumnName.ToString, "c", ""))
        '                    End With

        '                    .Columns.Add(ColG)
        '                    With .Columns(Col.ColumnName.ToString)
        '                        .OptionsFilter.AllowAutoFilter = False
        '                        .OptionsFilter.AllowFilter = False
        '                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '                        .DisplayFormat.FormatString = "{0:n2}"
        '                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '                    End With
        '                    '.Columns(Col.ColumnName.ToString).Width = 70
        '                    '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
        '                    '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

        '            End Select
        '        Next
        '    End If
        'End With

        'Me.ogcbreakdown.DataSource = _dt.Copy ' _dt.Copy
        'Me.ogcbreakdown.Refresh()

    End Sub

    Private Sub LoadOrderBreakDown_Weight(Key As Object)
        'Dim _dt As New System.Data.DataTable
        'Dim _Qry As String = ""
        'Dim _colcount As Integer = 0
        'Dim _Filter As String = ""
        'With _dt
        '    .Columns.Add("FTInvoiceNo", GetType(String))
        '    .Columns.Add("FTCustomerPO", GetType(String))
        '    .Columns.Add("FTColorway", GetType(String))
        'End With

        '_Qry = "SELECT DISTINCT I.FTCustomerPO, I.FTInvoiceNo, I.FNHsysCmpID,  D.FTColorway, D.FTSizeBreakDown ,isnull(C.FNWeight , 0 ) AS FNWeight"
        '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS D WITH (NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS P WITH (NOLOCK) ON O.FNHSysPlantId = P.FNHSysPlantId"
        '_Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Weight AS C WITH(NOLOCK) ON I.FTInvoiceNo = C.FTInvoiceNo"
        '_Qry &= vbCrLf & " and D.FTColorway= C.FTColorway and D.FTSizeBreakDown = C.FTSizeBreakDown and I.FTCustomerPO = C.FTCustomerPO"
        '_Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo IN ('" & Key & "'))"
        '_Qry &= vbCrLf & "and I.FTStateWHApp='1'  and I.FTInvoiceNo <> '' "
        'If Me.FTExportInvoiceNo.Text <> "" And IsDate(Microsoft.VisualBasic.Right(Me.FTExportInvoiceNo.Text, 4)) Then
        '    _Qry &= vbCrLf & " and C.FTExportInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTExportInvoiceNo.Text) & "'"
        'End If
        '_Qry &= vbCrLf & "Order BY I.FTCustomerPO ASC ,D.FTColorway ASC , D.FTSizeBreakDown ASC"
        'Dim _oDt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        'For Each R As DataRow In _oDt.Rows
        '    _Filter = "FTInvoiceNo='" & R!FTInvoiceNo.ToString & "' and FTCustomerPO='" & R!FTCustomerPO.ToString & "' and FTColorway='" & R!FTColorway.ToString & "'"

        '    If _dt.Select(_Filter).Length <= 0 Then
        '        _dt.Rows.Add(R!FTInvoiceNo.ToString, R!FTCustomerPO.ToString, R!FTColorway.ToString)
        '    End If
        '    If _dt.Columns.IndexOf("c" & R!FTSizeBreakDown.ToString) < 0 Then
        '        _dt.Columns.Add("c" & R!FTSizeBreakDown.ToString, GetType(Double))
        '    End If
        '    For Each x As DataRow In _dt.Select(_Filter)
        '        x.Item("c" & R!FTSizeBreakDown.ToString) = R!FNWeight.ToString
        '    Next
        'Next

        '' _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'With Me.ogvbreakdownweight
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next

        '    If Not (_dt Is Nothing) Then
        '        For Each Col As DataColumn In _dt.Columns
        '            Select Case Col.ColumnName.ToString.ToUpper
        '                Case "FTInvoiceNo".ToUpper, "FTCustomerPO".ToUpper, "FTColorway".ToUpper
        '                Case Else
        '                    _colcount = _colcount + 1

        '                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
        '                    With ColG
        '                        .Visible = True
        '                        .FieldName = Col.ColumnName.ToString
        '                        .Name = Col.ColumnName.ToString
        '                        .Caption = Replace(Col.ColumnName.ToString, "c", "")
        '                        .ColumnEdit = ReposPrice
        '                        .VisibleIndex = _SizeIndex(Replace(Col.ColumnName.ToString, "c", ""))
        '                    End With

        '                    .Columns.Add(ColG)
        '                    With .Columns(Col.ColumnName.ToString)
        '                        .OptionsFilter.AllowAutoFilter = False
        '                        .OptionsFilter.AllowFilter = False
        '                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '                        .DisplayFormat.FormatString = "{0:n2}"
        '                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '                    End With

        '                    '.Columns(Col.ColumnName.ToString).Width = 70
        '                    '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
        '                    '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

        '            End Select

        '        Next
        '    End If
        'End With

        'Me.ogcbreakdownweight.DataSource = _dt.Copy ' _dt.Copy
        'ogcbreakdownweight.Refresh()

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


    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try
            Dim _Cmd As String = ""


            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = True
                Dim dr As DialogResult = .ShowDialog()
                If (dr = System.Windows.Forms.DialogResult.OK) Then
                    Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Import Data From File ")
                    For Each file In .FileNames
                        ' _FileName = .FileName

                        Call ReadXlsfile_Multiple(file, _Spls)

                    Next
                    _Spls.Close()
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub



    Private Function GetDescription(_PORef As String) As System.Data.DataTable
        Try
            Dim _Cmd As String = ""
            _Cmd = "  Select distinct Isnull(S.FTPORef , O.FTPORef)  as FTPORef ,  FDShipDate , S.FNHSysProvinceId ,  O.FNHSysStyleId , O.FNHSysSeasonId  ,isnull(P.FTMainMatSpecEN , '') as FTMainMatSpec , isnull(  P.FNHSysMainMatSpecId ,0 )  as FNHSysMainMatSpecId  "
            _Cmd &= vbCrLf & " ,  V.FTProvinceCode  "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK)  ON O.FTOrderNo = S.FTOrderNo "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMatSpec AS P WITH(NOLOCK) ON O.FNHSysStyleId  = P.FNHSysStyleId and  O.FNHSysSeasonId = P.FNHSysSeasonId  "
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS V WITH(NOLOCK)  ON  S.FNHSysProvinceId  = V.FNHSysProvinceId"
            _Cmd &= vbCrLf & "  where  Isnull(S.FTPORef , O.FTPORef) = '" & HI.UL.ULF.rpQuoted(_PORef) & "'   "
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub ReadXlsfile_Multiple(_fileName As String, ByVal _Spls As HI.TL.SplashScreen)
        Try
            Me.oTabPlanGen.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.oTabmaster})

            Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
            Dim _TabSub As New DevExpress.XtraTab.XtraTabControl
            Dim _TabPageSubHead As New DevExpress.XtraTab.XtraTabPage
            Dim _TabPageSubDetail As New DevExpress.XtraTab.XtraTabPage
            Dim _GridDM As New DevExpress.XtraGrid.GridControl
            Dim _GridVDM As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GridDD As New DevExpress.XtraGrid.GridControl
            Dim _GridVDD As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GrpSum As New DevExpress.XtraEditors.GroupControl
            Dim _GrpDetail As New DevExpress.XtraEditors.GroupControl
            Dim _GvCol As DevExpress.XtraGrid.Columns.GridColumn()
            Dim _GrpInfo As New DevExpress.XtraEditors.GroupControl


            Dim _oDt As New System.Data.DataTable
            Dim _oDtIn As New System.Data.DataTable

            Dim _Qry As String = ""
            Dim _RowDes As Integer = 0
            Dim xlsFilename As String = Path.GetFileName(_fileName)
            _oDt = HI.UL.ReadExcel.Read(_fileName, "Detail", -1)
            _oDtIn = HI.UL.ReadExcel.Read(_fileName, "Identification", -1)

            Dim _POrefNo As String = _oDt.Rows(7).Item(2).ToString

            'Tabmain
            With _TabPage
                .Name = "otb" & _POrefNo
                .Text = _POrefNo
                .Tag = "2|"
            End With
            'Tabmain

            With _TabPageSubHead
                '.Controls.Add(_GridDM)

                .Name = "otbSubHead" & _POrefNo
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    .Text = "PO SUMMARY"
                Else
                    .Text = "PO SUMMARY"
                End If
            End With

            'TabSub  

            Dim _oGrpDetail As New DevExpress.XtraEditors.GroupControl

            'Dim _OUFromEditImport1 As New oUFromEditImport
            'With _OUFromEditImport1
            '    .Dock = System.Windows.Forms.DockStyle.Fill
            '    .Location = New System.Drawing.Point(0, 0)
            '    .Name = "OUFromEditImport1" & _POrefNo
            '    .TabIndex = 0
            'End With
            'With _TabPageSubDetail
            '    .Controls.Add(_OUFromEditImport1)
            '    .Name = "otbSubDetail" & _POrefNo
            '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '        .Text = "IDENTIFICATION"
            '    Else
            '        .Text = "IDENTIFICATION"
            '    End If
            'End With


            'If Not (_oDtIn Is Nothing) Then

            '    Try
            '        With _OUFromEditImport1
            '            .FTPlanRefNo.Text = _oDtIn.Rows(5).Item(11).ToString
            '            .FDPlanDate.Text = _oDtIn.Rows(5).Item(27).ToString
            '        End With




            '    Catch ex As Exception

            '    End Try
            'End If


            'TabSub
            With _TabSub
                .Name = "otbSub" & _POrefNo
                .Dock = System.Windows.Forms.DockStyle.Fill
                .TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {_TabPageSubHead})
            End With

            With _GridVDM

                '   .GridControl = _GridDM
                .Name = "ogvPlandM" & _POrefNo
                .OptionsView.ColumnAutoWidth = False
                '.OptionsView.ShowFooter = True
                .OptionsView.ShowGroupPanel = False

            End With

            With _GridVDM
                .BeginInit()
                For Each oCol As DevExpress.XtraGrid.Columns.GridColumn In ogvPlandM.Columns
                    '  oCol.Name = oCol.Name & _POrefNo
                    _GridVDM.Columns.Add(oCol)
                    Dim _GvCoxl As New DevExpress.XtraGrid.Columns.GridColumn()
                    _GvCoxl.AppearanceHeader.Options.UseTextOptions = True
                    _GvCoxl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    _GvCoxl.Caption = oCol.Caption
                    _GvCoxl.Name = oCol.Name & _POrefNo
                    _GvCoxl.FieldName = oCol.FieldName
                    _GvCoxl.DisplayFormat.FormatType = oCol.DisplayFormat.FormatType
                    _GvCoxl.DisplayFormat.FormatString = oCol.DisplayFormat.GetFormatString
                    .Columns.Add(_GvCoxl)
                Next

                .EndInit()
            End With

            With _GridDM
                .Dock = System.Windows.Forms.DockStyle.Fill
                .Location = New System.Drawing.Point(2, 25)
                .MainView = _GridVDM
                .Name = "ogcPlandM" & _POrefNo
                .Size = New System.Drawing.Size(1231, 143)
                .TabIndex = 0
                ' .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvPlandM})

            End With

            ' _GridVDD = ogvPlandD
            Dim _inx As Integer = 0

            With _GridVDD
                .BeginInit()
                .GridControl = _GridDD
                .Name = "ogvPlandD" & _POrefNo
                .OptionsView.ColumnAutoWidth = False
                .OptionsView.ShowGroupPanel = False
                .OptionsMenu.ShowGroupSummaryEditorItem = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowGroupedColumns = True
                .Tag = "2|"
                For Each oColx As DevExpress.XtraGrid.Columns.GridColumn In ogvPlandD.Columns
                    '   oCol.Name = oCol.Name & _POrefNo
                    ' .Columns.Add(oColx)
                    Dim _GvCoxlx As New DevExpress.XtraGrid.Columns.GridColumn()
                    '_GvCoxlx = oColx
                    .Columns.Add(_GvCoxlx)

                    Select Case oColx.FieldName.ToString.ToUpper
                        Case "FTPackCode".ToUpper, "FNTotalNetWeight".ToUpper, "FNGrossNetWeight".ToUpper

                            _GvCoxlx.Caption = oColx.Caption
                            _GvCoxlx.Name = oColx.FieldName & _POrefNo
                            _GvCoxlx.FieldName = oColx.FieldName
                            _GvCoxlx.DisplayFormat.FormatString = "{0:n4}"
                            _GvCoxlx.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            _GvCoxlx.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, oColx.FieldName.ToString, "{0:n4}")})
                            _GvCoxlx.Visible = True
                            _GvCoxlx.VisibleIndex = _inx
                            _GvCoxlx.Width = 85

                        Case "FNItemQty".ToUpper
                            _GvCoxlx.Caption = "FNItemQty"
                            _GvCoxlx.DisplayFormat.FormatString = "{0:n0}"
                            _GvCoxlx.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            _GvCoxlx.FieldName = "FNItemQty"
                            _GvCoxlx.Name = "GridColumn35"
                            _GvCoxlx.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNItemQty", "{0:n0}")})
                            _GvCoxlx.Visible = True
                            _GvCoxlx.VisibleIndex = _inx
                            _GvCoxlx.Width = oColx.Width
                        Case Else
                            _GvCoxlx.AppearanceHeader.Options.UseTextOptions = True
                            _GvCoxlx.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            _GvCoxlx.Caption = oColx.Caption
                            _GvCoxlx.Name = oColx.FieldName & _POrefNo
                            _GvCoxlx.FieldName = oColx.FieldName
                            _GvCoxlx.DisplayFormat.FormatType = oColx.DisplayFormat.FormatType
                            _GvCoxlx.DisplayFormat.FormatString = oColx.DisplayFormat.GetFormatString
                            _GvCoxlx.Visible = True
                            _GvCoxlx.VisibleIndex = _inx
                            _GvCoxlx.Width = 85

                    End Select
                    _inx += +1

                Next
                .EndInit()
            End With



            'With _GridVDD
            '    .BeginInit()
            '    For Each oColx As DevExpress.XtraGrid.Columns.GridColumn In ogvPlandD.Columns
            '        '   oCol.Name = oCol.Name & _POrefNo
            '        .Columns.Add(oColx)
            '        Dim _GvCoxlx As New DevExpress.XtraGrid.Columns.GridColumn()
            '        '_GvCoxlx = oColx
            '        _GvCoxlx.AppearanceHeader.Options.UseTextOptions = True
            '        _GvCoxlx.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '        _GvCoxlx.Caption = oColx.Caption
            '        _GvCoxlx.Name = oColx.FieldName & _POrefNo
            '        _GvCoxlx.FieldName = oColx.FieldName
            '        _GvCoxlx.DisplayFormat.FormatType = oColx.DisplayFormat.FormatType
            '        _GvCoxlx.DisplayFormat.FormatString = oColx.DisplayFormat.GetFormatString

            '        .Columns.Add(_GvCoxlx)
            '    Next

            '    .EndInit()
            'End With


            'Dim _ColOld As String = ""

            'With _GridVDD
            '    .BeginInit()
            '    For Each oColx As DevExpress.XtraGrid.Columns.GridColumn In .Columns
            '        '   oCol.Name = oCol.Name & _POrefNo


            '        If Microsoft.VisualBasic.Left(oColx.FieldName, 2) = "FN" Then
            '            oColx.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '            oColx.DisplayFormat.FormatString = "{0:n4}"
            '        End If

            '        If _ColOld.ToString = oColx.FieldName.ToString Then
            '            '  .Columns.Remove(oColx)
            '            oColx.FieldName = ""
            '        Else
            '            _ColOld = oColx.FieldName.ToString
            '        End If

            '        '.Columns.Add(oColx)
            '        'Dim _GvCoxlx As New DevExpress.XtraGrid.Columns.GridColumn()
            '        ''_GvCoxlx = oColx
            '        '_GvCoxlx.AppearanceHeader.Options.UseTextOptions = True
            '        '_GvCoxlx.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '        '_GvCoxlx.Caption = oColx.Caption
            '        '_GvCoxlx.Name = oColx.FieldName & _POrefNo
            '        '_GvCoxlx.FieldName = oColx.FieldName
            '        '_GvCoxlx.DisplayFormat.FormatType = oColx.DisplayFormat.FormatType
            '        '_GvCoxlx.DisplayFormat.FormatString = oColx.DisplayFormat.GetFormatString

            '        '.Columns.Add(_GvCoxlx)
            '    Next

            '    .EndInit()
            'End With



            With _GridDD
                .Dock = System.Windows.Forms.DockStyle.Fill
                ' .Location = New System.Drawing.Point(2, 25)
                .MainView = _GridVDD
                .Name = "ogcPlandD" & _POrefNo
                ' .Size = New System.Drawing.Size(1231, 349)
                .TabIndex = 0
                '  .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridVDD})
            End With

            With _GrpDetail
                .Controls.Add(_GridDD)
                .Dock = System.Windows.Forms.DockStyle.Fill
                .Location = New System.Drawing.Point(0, 170)
                .Name = "grpControl" & _POrefNo
                ' .Size = New System.Drawing.Size(1235, 376)
                .TabIndex = 2
                .Text = "PACKAGE DETAIL"
            End With

            With _GrpSum
                .Controls.Add(_GridDM)
                .Dock = System.Windows.Forms.DockStyle.Top
                '  .Location = New System.Drawing.Point(0, 0)
                .Name = "grpControl" & _POrefNo
                .Size = New System.Drawing.Size(1235, 170)
                .TabIndex = 1
                .Text = "PO SUMMARY"
            End With

            Dim _appstate As New DevExpress.XtraEditors.CheckEdit
            Dim _lbl3 As New DevExpress.XtraEditors.LabelControl
            Dim _appdate As New DevExpress.XtraEditors.DateEdit
            Dim _lbl1 As New DevExpress.XtraEditors.LabelControl
            Dim _appby As New DevExpress.XtraEditors.TextEdit


            With _appstate
                .Location = New System.Drawing.Point(25, 36)
                .Name = "FTApproveState" & _POrefNo
                .Properties.AutoHeight = False
                .Properties.Caption = Me.FTApproveState.Properties.Caption
                .Properties.ReadOnly = True
                .Properties.ValueChecked = "1"
                .Properties.ValueUnchecked = "0"
                .Size = New System.Drawing.Size(246, 21)
                .TabIndex = 6
            End With

            With _lbl3
                .Appearance.Options.UseTextOptions = True
                .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                .AutoEllipsis = True
                .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                .Location = New System.Drawing.Point(629, 37)
                .Name = "LabelControl3" & _POrefNo
                .Size = New System.Drawing.Size(199, 16)
                .TabIndex = 4
                .Text = Me.LabelControl3.Text
            End With

            With _appdate
                .EditValue = Nothing
                .Location = New System.Drawing.Point(836, 35)
                .Name = "FDApproveDate" & _POrefNo
                .Properties.Buttons(0).Visible = False
                '.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
                '.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
                .Properties.DisplayFormat.FormatString = ""
                .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Properties.EditFormat.FormatString = ""
                .Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Properties.Mask.EditMask = ""
                .Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
                .Properties.ReadOnly = True
                .Size = New System.Drawing.Size(149, 22)
                .TabIndex = 5
            End With

            With _appby
                .Location = New System.Drawing.Point(443, 35)
                .Name = "FTApproveBy" & _POrefNo
                .Properties.ReadOnly = True
                .Size = New System.Drawing.Size(172, 22)
                .TabIndex = 3
            End With

            With _lbl1
                .Appearance.Options.UseTextOptions = True
                .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                .AutoEllipsis = True
                .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                .Location = New System.Drawing.Point(246, 37)
                .Name = "LabelControl1" & _POrefNo
                .Size = New System.Drawing.Size(191, 16)
                .TabIndex = 2
                .Text = Me.LabelControl1.Text

            End With

            With _GrpInfo
                .Dock = System.Windows.Forms.DockStyle.Top
                .Name = "grpInfo" & _POrefNo
                .Size = New System.Drawing.Size(1239, 76)
                .TabIndex = 0
                .Location = New System.Drawing.Point(0, 0)
                .Text = "Document Info"


                .Controls.Add(_appstate)
                .Controls.Add(_lbl3)
                .Controls.Add(_appdate)
                .Controls.Add(_appby)
                .Controls.Add(_lbl1)

            End With




            With _TabPageSubHead
                .Controls.Add(_GrpDetail)
                .Controls.Add(_GrpSum)
                .Controls.Add(_GrpInfo)
            End With
            HI.TL.HandlerControl.AddHandlerGridColumnEdit(_GridDM.Views(0))



            If Not (_oDt Is Nothing) Then
                Dim _dt As New System.Data.DataTable
                With _dt
                    .Columns.Add("FTPckPlanNo", GetType(String))
                    .Columns.Add("FTPORef", GetType(String))
                    .Columns.Add("FNQuantity", GetType(Integer))
                    .Columns.Add("FNPackcount", GetType(Integer))
                    .Columns.Add("FNNet", GetType(Double))
                    .Columns.Add("FNTotalNet", GetType(Double))
                    .Columns.Add("FNGrossWeight", GetType(Double))
                    .Columns.Add("FNHSysUnitId", GetType(Integer))
                    .Columns.Add("FTUnitCode", GetType(String))
                    .Columns.Add("FNVol", GetType(Double))
                    .Columns.Add("FTVolUnit", GetType(String))
                    .Columns.Add("FDShipDate", GetType(String))
                    .Columns.Add("FNHSysProvinceId", GetType(String))
                    .Columns.Add("FNHSysDescRawMatId", GetType(Integer))
                    .Columns.Add("FTMainMatSpec", GetType(String))
                    .Columns.Add("FNHSysProvinceId_Hide", GetType(Integer))

                End With

                Dim _RecodeCheck As Integer = 0
                If _oDt.Rows(5).Item(0).ToString = "PO SUMMARY" Then
                    _RecodeCheck = 0
                ElseIf _oDt.Rows(5).Item(0).ToString = "" And _oDt.Rows(5).Item(1).ToString = "PO SUMMARY" Then
                    _RecodeCheck = 1
                End If


                For Each R As DataRow In _oDt.Rows
                    If _oDt.Rows(5).Item(0 + _RecodeCheck).ToString = "PO SUMMARY" And (IsNumeric(R.Item(1 + _RecodeCheck).ToString) Or R.Item(1 + _RecodeCheck).ToString = "Totals") And IsNumeric(R.Item(3 + _RecodeCheck).ToString) Then
                        Dim _dtt As System.Data.DataTable = GetDescription(_oDt.Rows(2).Item(9).ToString)
                        Dim _dr As DataRow = _dt.NewRow
                        _dr("FTPckPlanNo") = R.Item(1 + _RecodeCheck).ToString
                        _dr("FTPORef") = _oDt.Rows(2).Item(9).ToString
                        _dr("FNQuantity") = R.Item(3 + _RecodeCheck).ToString
                        _dr("FNPackcount") = R.Item(4 + _RecodeCheck).ToString
                        _dr("FNNet") = R.Item(5 + _RecodeCheck).ToString
                        _dr("FNTotalNet") = R.Item(7 + _RecodeCheck).ToString
                        _dr("FNGrossWeight") = R.Item(9 + _RecodeCheck).ToString
                        _dr("FNHSysUnitId") = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysUnitId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) Where FTUnitCode='" & R.Item(11).ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
                        _dr("FTUnitCode") = R.Item(11 + _RecodeCheck).ToString
                        _dr("FNVol") = R.Item(12 + _RecodeCheck).ToString
                        _dr("FTVolUnit") = R.Item(13 + _RecodeCheck).ToString
                        _dr("FDShipDate") = _dtt.Rows(0).Item("FDShipDate").ToString
                        _dr("FNHSysProvinceId") = _dtt.Rows(0).Item("FTProvinceCode").ToString
                        _dr("FNHSysDescRawMatId") = Integer.Parse("0" & _dtt.Rows(0).Item("FNHSysDescRawMatId").ToString)
                        _dr("FTMainMatSpec") = _dtt.Rows(0).Item("FTMainMatSpec").ToString
                        _dr("FNHSysProvinceId_Hide") = Integer.Parse("0" & _dtt.Rows(0).Item("FNHSysProvinceId").ToString)

                        _dt.Rows.Add(_dr)

                    End If

                Next
                If _dt IsNot Nothing Then
                    _GridDM.DataSource = _dt
                End If

                Dim _dtd As New System.Data.DataTable
                With _dtd
                    .Columns.Add("FTPckPlanNo", GetType(String))
                    .Columns.Add("FTRangeNo", GetType(String))
                    .Columns.Add("FNFrom", GetType(Integer))
                    .Columns.Add("FNTo", GetType(Integer))
                    .Columns.Add("FTSerialFrom", GetType(String))
                    .Columns.Add("FTSerialTo", GetType(String))
                    .Columns.Add("FTPackInstructionCode", GetType(String))
                    .Columns.Add("FTLineNo", GetType(String))
                    .Columns.Add("FTStyleCode", GetType(String))
                    .Columns.Add("FTSKU", GetType(String))
                    .Columns.Add("FTPORef", GetType(String))
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
                        _dr("FTPckPlanNo") = R.Item(16 + _RecodeCheck).ToString
                        _dr("FTRangeNo") = R.Item(3 + _RecodeCheck).ToString
                        _dr("FNFrom") = R.Item(4 + _RecodeCheck).ToString
                        _dr("FNTo") = R.Item(5 + _RecodeCheck).ToString
                        _dr("FTSerialFrom") = R.Item(7 + _RecodeCheck).ToString
                        _dr("FTSerialTo") = R.Item(9 + _RecodeCheck).ToString
                        _dr("FTPackInstructionCode") = R.Item(11 + _RecodeCheck).ToString
                        _dr("FTLineNo") = R.Item(12 + _RecodeCheck).ToString
                        _dr("FTStyleCode") = R.Item(13 + _RecodeCheck).ToString
                        _dr("FTSKU") = R.Item(14 + _RecodeCheck).ToString
                        _dr("FTPORef") = _oDt.Rows(2).Item(9).ToString
                        _dr("FTPOLineNo") = R.Item(17 + _RecodeCheck).ToString
                        _dr("FTColorWay") = R.Item(18 + _RecodeCheck).ToString
                        _dr("FTSizeBreakDown") = R.Item(20 + _RecodeCheck).ToString
                        _dr("FTShortDescription") = R.Item(21 + _RecodeCheck).ToString
                        _dr("FTShipmentMethod") = R.Item(22 + _RecodeCheck).ToString
                        _dr("FNItemQty") = Microsoft.VisualBasic.Replace(R.Item(23 + _RecodeCheck).ToString, ",", "")
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
                    _GridDD.DataSource = _dtd
                End If
                _RowDes = _oDt.Rows.Count


                _TabPage.Controls.Add(_TabSub)
                HI.TL.HandlerControl.AddHandlerObj(_TabPage)
                Me.oTabPlanGen.TabPages.Add(_TabPage)

                Me.oTabmaster.Visible = False
                Me.oTabmasterInden.Visible = False
                Me.oTabmasterInden.PageVisible = False
                Me.oTabmaster.PageVisible = False

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


                'Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
                'Dim TmpFile As String = _Path & "\ExportPland\"

                'Dim DestPath As String = TmpFile
                'If Not Directory.Exists(DestPath) Then
                '    Directory.CreateDirectory(DestPath)
                'End If
                'Dim file = New FileInfo(_fileName)
                'file.CopyTo(Path.Combine(DestPath, file.Name), True)
                'Try
                '    My.Computer.FileSystem.DeleteFile(DestPath & Me.FTExportInvoiceNo.Text.ToString & ".Xlsx")
                'Catch ex As Exception

                'End Try


                'My.Computer.FileSystem.RenameFile(DestPath & file.Name, Me.FTExportInvoiceNo.Text.ToString & ".Xlsx")

                '_Spls.Close()
                'Me.SpreadsheetControl1.LoadDocument(_fileName)
                ''Process.Start(_fileName)

            Else
                HI.MG.ShowMsg.mInfo("Invalid Sheet Name In Excel File..", 1509281139, Me.Text, "Detail")
                Exit Sub
            End If


            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub




    Private Sub ocmapprove_Click(sender As Object, e As EventArgs)
        Try
            If Me.VerrifyData Then
                Dim state As Boolean = False
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, "ต้องการอนุมัติทั้งหมด หรือ เฉพาะแทป กรุณากดปุ่ม  Yes ทั้งหมด  , No เฉพาะแทป ") = True Then
                    For Each xtab As DevExpress.XtraTab.XtraTabPage In Me.oTabPlanGen.TabPages
                        If xtab.Name <> "oTabmasterInden" And xtab.Name <> "oTabmaster" Then
                            If Me.ApproveData(xtab) Then
                                ' HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                state = True
                            Else
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                Exit Sub
                            End If
                        End If

                    Next
                Else

                    If Me.ApproveData(Me.oTabPlanGen.SelectedTabPage) Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Exit Sub
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Exit Sub
                    End If
                End If
                'oTabmaster 'oTabmasterInden

                If state Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Function ApproveData(_Tab As DevExpress.XtraTab.XtraTabPage, Optional ByVal _state As Boolean = True) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _PORef As String = ""
            Dim _statex As String = IIf(_state, "1", "0")
            _PORef = Microsoft.VisualBasic.Replace(_Tab.Name.ToString, "otb", "")

            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan "
            _Cmd &= vbCrLf & " set  FTApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Cmd &= vbCrLf & " , FDApproveDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & " , FTApproveTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTApproveState ='" & _statex & "'"
            _Cmd &= vbCrLf & " Where  FTPckPlanNo='" & _PORef & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


            _Cmd = "select top 1   FTApproveBy "
            _Cmd &= vbCrLf & " , FDApproveDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",isnull( FTApproveState ,'0')  as FTApproveState "
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan "
            _Cmd &= vbCrLf & " Where  FTPckPlanNo='" & _PORef & "'"
            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows
                DirectCast((_Tab.Controls.Find("FTApproveBy" & _PORef, True)(0)), DevExpress.XtraEditors.TextEdit).Text = R!FTApproveBy.ToString
                DirectCast((_Tab.Controls.Find("FDApproveDate" & _PORef, True)(0)), DevExpress.XtraEditors.DateEdit).Text = HI.UL.ULDate.ConvertEN(R!FDApproveDate.ToString).ToString
                DirectCast((_Tab.Controls.Find("FTApproveState" & _PORef, True)(0)), DevExpress.XtraEditors.CheckEdit).EditValue = (R!FTApproveState.ToString)
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmreject_Click(sender As Object, e As EventArgs)
        Try
            If Me.VerrifyData Then
                Dim state As Boolean = False
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, "ต้องการยกอนุมัติทั้งหมด หรือ เฉพาะแทป กรุณากดปุ่ม  Yes ทั้งหมด  , No เฉพาะแทป ") = True Then
                    For Each xtab As DevExpress.XtraTab.XtraTabPage In Me.oTabPlanGen.TabPages
                        If xtab.Name <> "oTabmasterInden" And xtab.Name <> "oTabmaster" Then
                            If Me.ApproveData(xtab, False) Then
                                ' HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                state = True
                            Else
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                Exit Sub
                            End If
                        End If

                    Next
                Else

                    If Me.ApproveData(Me.oTabPlanGen.SelectedTabPage, False) Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Exit Sub
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Exit Sub
                    End If
                End If
                'oTabmaster 'oTabmasterInden

                If state Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function verifydata() As Boolean
        Try
            Dim _State As Boolean = False
            If Me.FTStartShipment.Text <> "" And Me.FTEndShipment.Text <> "" Then
                _State = True
            End If

            If FTCustomerPO.Text <> "" Then
                _State = True
            End If
            If Not (_State) Then
                HI.MG.ShowMsg.mInfo("กรุณาเลือก วันที่ส่งมอบสินค้า หรือ เลขที่ใบสั่งซื้อลูกค้า !!!!", 1907100944, Me.Text)
            End If
            Return _State
        Catch ex As Exception

        End Try
    End Function

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If verifydata() = False Then
                Exit Sub
            End If

            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Load Data  ")


            _Cmd = "Exec  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GET_BOOKING_TRACKING '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipment.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndShipment.Text) & "'"
            _Cmd &= " ,'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            Dim _dt As New System.Data.DataTable
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogcPlandM.DataSource = _dt

            _Spls.Close()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Load_Multiple(_FtPONo As String, ByVal _Spls As HI.TL.SplashScreen)
        Try
            Me.oTabPlanGen.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.oTabmaster})

            Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
            Dim _TabSub As New DevExpress.XtraTab.XtraTabControl
            Dim _TabPageSubHead As New DevExpress.XtraTab.XtraTabPage
            Dim _TabPageSubDetail As New DevExpress.XtraTab.XtraTabPage
            Dim _GridDM As New DevExpress.XtraGrid.GridControl
            Dim _GridVDM As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GridDD As New DevExpress.XtraGrid.GridControl
            Dim _GridVDD As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GrpSum As New DevExpress.XtraEditors.GroupControl
            Dim _GrpDetail As New DevExpress.XtraEditors.GroupControl
            Dim _GvCol As DevExpress.XtraGrid.Columns.GridColumn()
            Dim _GrpInfo As New DevExpress.XtraEditors.GroupControl


            Dim _oDt As New System.Data.DataTable
            Dim _oDtIn As New System.Data.DataTable

            Dim _Qry As String = ""
            Dim _RowDes As Integer = 0

            Dim _POrefNo As String = _FtPONo

            'Tabmain
            With _TabPage
                .Name = "otb" & _POrefNo
                .Text = _POrefNo
                .Tag = "2|"
            End With
            'Tabmain

            With _TabPageSubHead
                '.Controls.Add(_GridDM)

                .Name = "otbSubHead" & _POrefNo
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    .Text = "PO SUMMARY"
                Else
                    .Text = "PO SUMMARY"
                End If
            End With

            'TabSub  

            Dim _oGrpDetail As New DevExpress.XtraEditors.GroupControl
            'TabSub
            With _TabSub
                .Name = "otbSub" & _POrefNo
                .Dock = System.Windows.Forms.DockStyle.Fill
                .TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {_TabPageSubHead})
            End With
            _GridVDM = ogvPlandM
            With _GridVDM

                '   .GridControl = _GridDM
                .Name = "ogvPlandM" & _POrefNo
                .OptionsView.ColumnAutoWidth = False
                '.OptionsView.ShowFooter = True
                .OptionsView.ShowGroupPanel = False

            End With



            With _GridDM
                .Dock = System.Windows.Forms.DockStyle.Fill
                .Location = New System.Drawing.Point(2, 25)
                .MainView = _GridVDM
                .Name = "ogcPlandM" & _POrefNo
                .Size = New System.Drawing.Size(1231, 143)
                .TabIndex = 0
                ' .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvPlandM})

            End With

            With _GridVDD
                '.GridControl = _GridDD
                .Name = "ogvPlandD" & _POrefNo
                .OptionsView.ColumnAutoWidth = False
                .OptionsView.ShowGroupPanel = False


            End With

            With _GridDD
                .Dock = System.Windows.Forms.DockStyle.Fill
                ' .Location = New System.Drawing.Point(2, 25)
                .MainView = ogvPlandD
                .Name = "ogcPlandD" & _POrefNo
                ' .Size = New System.Drawing.Size(1231, 349)
                .TabIndex = 0
                '  .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridVDD})
            End With

            With _GrpDetail
                .Controls.Add(_GridDD)
                .Dock = System.Windows.Forms.DockStyle.Fill
                .Location = New System.Drawing.Point(0, 170)
                .Name = "grpControl" & _POrefNo
                ' .Size = New System.Drawing.Size(1235, 376)
                .TabIndex = 2
                .Text = "PACKAGE DETAIL"
            End With

            With _GrpSum
                .Controls.Add(_GridDM)
                .Dock = System.Windows.Forms.DockStyle.Top
                '  .Location = New System.Drawing.Point(0, 0)
                .Name = "grpControl" & _POrefNo
                .Size = New System.Drawing.Size(1235, 170)
                .TabIndex = 1
                .Text = "PO SUMMARY"
            End With

            Dim _appstate As New DevExpress.XtraEditors.CheckEdit
            Dim _lbl3 As New DevExpress.XtraEditors.LabelControl
            Dim _appdate As New DevExpress.XtraEditors.DateEdit
            Dim _lbl1 As New DevExpress.XtraEditors.LabelControl
            Dim _appby As New DevExpress.XtraEditors.TextEdit


            With _appstate
                .Location = New System.Drawing.Point(25, 36)
                .Name = "FTApproveState" & _POrefNo
                .Properties.AutoHeight = False
                .Properties.Caption = Me.FTApproveState.Properties.Caption
                .Properties.ReadOnly = True
                .Properties.ValueChecked = "1"
                .Properties.ValueUnchecked = "0"
                .Size = New System.Drawing.Size(246, 21)
                .TabIndex = 6
            End With

            With _lbl3
                .Appearance.Options.UseTextOptions = True
                .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                .AutoEllipsis = True
                .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                .Location = New System.Drawing.Point(629, 37)
                .Name = "LabelControl3" & _POrefNo
                .Size = New System.Drawing.Size(199, 16)
                .TabIndex = 4
                .Text = Me.LabelControl3.Text
            End With

            With _appdate
                .EditValue = Nothing
                .Location = New System.Drawing.Point(836, 35)
                .Name = "FDApproveDate" & _POrefNo
                .Properties.Buttons(0).Visible = False
                '.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
                '.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
                .Properties.DisplayFormat.FormatString = ""
                .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Properties.EditFormat.FormatString = ""
                .Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .Properties.Mask.EditMask = ""
                .Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
                .Properties.ReadOnly = True
                .Size = New System.Drawing.Size(149, 22)
                .TabIndex = 5
            End With

            With _appby
                .Location = New System.Drawing.Point(443, 35)
                .Name = "FTApproveBy" & _POrefNo
                .Properties.ReadOnly = True
                .Size = New System.Drawing.Size(172, 22)
                .TabIndex = 3
            End With

            With _lbl1
                .Appearance.Options.UseTextOptions = True
                .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                .AutoEllipsis = True
                .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                .Location = New System.Drawing.Point(246, 37)
                .Name = "LabelControl1" & _POrefNo
                .Size = New System.Drawing.Size(191, 16)
                .TabIndex = 2
                .Text = Me.LabelControl1.Text

            End With

            With _GrpInfo
                .Dock = System.Windows.Forms.DockStyle.Top
                .Name = "grpInfo" & _POrefNo
                .Size = New System.Drawing.Size(1239, 76)
                .TabIndex = 0
                .Location = New System.Drawing.Point(0, 0)
                .Text = "Document Info"


                .Controls.Add(_appstate)
                .Controls.Add(_lbl3)
                .Controls.Add(_appdate)
                .Controls.Add(_appby)
                .Controls.Add(_lbl1)

            End With


            With _TabPageSubHead
                .Controls.Add(_GrpDetail)
                .Controls.Add(_GrpSum)
                .Controls.Add(_GrpInfo)
            End With
            HI.TL.HandlerControl.AddHandlerGridColumnEdit(_GridDM.Views(0))



            If _FtPONo <> "" Then

                Dim _Cmd As String = ""
                _Cmd = " SELECT   FTPckPlanNo, FTPORef, FNQuantity, FNPackcount, FNNet, FNTotalNet, FNGrossWeight, FNHSysUnitId, FNVol, FTVolUnit, isnull( FTApproveState ,'0') as FTApproveState , FTApproveBy,  FDApproveDate "
                _Cmd &= vbCrLf & " , FDShipDate ,  isnull(FNHSysMainMatSpecId,0) as  FNHSysMainMatSpecId  , FNHSysProvinceId as  FNHSysProvinceId_Hide  "
                _Cmd &= vbCrLf & " ,  (Select Top 1 isnull(FTMainMatSpecEN,'') as FTMainMatSpecEN  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMatSpec  WITH (NOLOCK)  Where FNHSysMainMatSpecId = P.FNHSysMainMatSpecId )  as FTMainMatSpec  "
                _Cmd &= vbCrLf & " ,  (Select Top 1 isnull(FTProvinceCode,'') as FTProvinceCode  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince  WITH (NOLOCK)  Where FNHSysProvinceId = P.FNHSysProvinceId )  as FNHSysProvinceId  "
                _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan AS P WITH(NOLOCK)  "
                _Cmd &= vbCrLf & " where  FTPORef='" & HI.UL.ULF.rpQuoted(_FtPONo) & "'"


                Dim _dt As New System.Data.DataTable
                _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

                If _dt IsNot Nothing Then
                    _GridDM.DataSource = _dt
                End If


                _Cmd = " SELECT   FTPckPlanNo, FTPORef, FTRangeNo, FNFrom, FNTo, FTSerialFrom, FTSerialTo, FTPackInstructionCode, FTLineNo, FTStyleCode, FTSKU, FTPONo, "
                _Cmd &= vbCrLf & "  FTPOLineNo, FTColorWay, FTSizeBreakDown, FTShortDescription, FTShipmentMethod,   FNItemQty, FNQtyPerPack, FNInnerPackCount, FNPackCount,  "
                _Cmd &= vbCrLf & "      FTR, FTPackCode, FNNetWeight, FNTotalNetWeight, FNGrossNetWeight, FTUnitCode,  FNL, FNW, FNH, FTItemUnitCode, FTScanID"
                _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D WITH(NOLOCK)  "
                _Cmd &= vbCrLf & " where  FTPORef='" & HI.UL.ULF.rpQuoted(_FtPONo) & "'"

                Dim _dtd As New System.Data.DataTable
                _dtd = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)




                _GridDD.DataSource = _dtd


                _RowDes = _oDt.Rows.Count


                _TabPage.Controls.Add(_TabSub)
                HI.TL.HandlerControl.AddHandlerObj(_TabPage)
                Me.oTabPlanGen.TabPages.Add(_TabPage)

                Me.oTabmaster.Visible = False
                Me.oTabmasterInden.Visible = False
                Me.oTabmasterInden.PageVisible = False
                Me.oTabmaster.PageVisible = False
                For Each R As DataRow In _dt.Rows
                    DirectCast((_TabPage.Controls.Find("FTApproveBy" & _FtPONo, True)(0)), DevExpress.XtraEditors.TextEdit).Text = R!FTApproveBy.ToString
                    DirectCast((_TabPage.Controls.Find("FDApproveDate" & _FtPONo, True)(0)), DevExpress.XtraEditors.DateEdit).Text = HI.UL.ULDate.ConvertEN(R!FDApproveDate.ToString).ToString
                    DirectCast((_TabPage.Controls.Find("FTApproveState" & _FtPONo, True)(0)), DevExpress.XtraEditors.CheckEdit).EditValue = (R!FTApproveState.ToString)
                Next


            Else
                HI.MG.ShowMsg.mInfo("Invalid Sheet Name In Excel File..", 1509281139, Me.Text, "Detail")
                Exit Sub
            End If


            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub


    Dim _pack As Double = 0
    Dim _netweigth As Double = 0
    Dim _grossweight As Double = 0
    Private Sub ogvPlandD_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles ogvPlandD.CustomSummaryCalculate
        Try
            Dim view As New GridView()
            view = DirectCast(sender, GridView)


            Dim _PckCount As Integer = Integer.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNPackCount"))
            Dim _L As Double = Double.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNL"))
            Dim _W As Double = Double.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNW"))
            Dim _H As Double = Double.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNH"))
            Dim _Net As Double = Double.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNTotalNetWeight"))
            Dim _Gos As Double = Double.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNGrossNetWeight"))

            Select Case e.SummaryProcess
                Case CustomSummaryProcess.Start
                    _pack = 0 : _netweigth = 0 : _grossweight = 0

                Case CustomSummaryProcess.Calculate
                    Select Case DirectCast(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString.ToUpper
                        Case "FTPackCode".ToUpper
                            _pack = _PckCount * (_L * _W * _H)
                        Case "FNTotalNetWeight".ToUpper
                            _netweigth = _PckCount * _Net
                        Case "FNGrossNetWeight".ToUpper
                            _grossweight = _PckCount * _Gos
                    End Select
                Case CustomSummaryProcess.Finalize
                    Select Case DirectCast(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString.ToUpper
                        Case "FTPackCode".ToUpper
                            e.TotalValue = _pack
                        Case "FNTotalNetWeight".ToUpper
                            e.TotalValue = _netweigth
                        Case "FNGrossNetWeight".ToUpper
                            e.TotalValue = _grossweight
                    End Select
            End Select


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FuncExcel_Click(sender As Object, e As EventArgs) Handles FuncExcelDemco.Click
        Try
            Dim _oDT As System.Data.DataTable
            With DirectCast(Me.ogcPlandM.DataSource, System.Data.DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูลเพื่อสร้าง Templet !!!", 1803210850, Me.Text, "", MessageBoxIcon.Information)
                    Exit Sub
                End If
                _oDT = .Select("FTSelect = '1'").CopyToDataTable
                ExportDataToTemp(_oDT)

            End With


        Catch ex As Exception

        End Try
    End Sub


    Private Function ExportDataToTemp(oDt As System.Data.DataTable) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("กำลังบันทึก Template กรุณารอซักครู่")
        Try
            Dim _Cmd As String = ""
            Dim _Row As Integer = 3
            Dim _Col As Integer = 3
            Dim _rPO As Integer = 0
            Dim _PoRef As String = ""


            Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()

            If xlApp Is Nothing Then
                HI.MG.ShowMsg.mInfo("Excel is not properly installed!!", 1803210834, Me.Text, "", MessageBoxIcon.Hand)
            End If


            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            Dim _Path As String = "C:\ExportDataForm_WISDOM\"


            If Not (Directory.Exists(_Path)) Then
                My.Computer.FileSystem.CreateDirectory(_Path)
            End If

            _Path = _Path & "template_booking_" & HI.Conn.SQLConn.GetField("Select REPLACE( convert(nvarchar(30) , GETDATE() ,111) , '/','') + REPLACE( Convert(varchar(8),Getdate(),114),':','')", Conn.DB.DataBaseName.DB_SYSTEM) & ".xls"
            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("sheet1")
            'xlWorkSheet.Cells(1, 1) = "Sheet 1"

            _PoRef = ""
            For Each R As DataRow In oDt.Rows

                If _PoRef <> R!FTPORef.ToString Then
                    _rPO = _Row + 1
                End If


                xlWorkSheet.Cells(_Row, _Col) = R!FTCatData.ToString & "  H.S. CODE  " & HI.UL.ULF.rpQuoted(R!FTHTSData.ToString)
                xlWorkSheet.Cells(_Row + 1, _Col) = " " & HI.UL.ULF.rpQuoted(R!FTCustExpProductDesc.ToString)
                xlWorkSheet.Cells(_Row + 2, _Col) = " LINE :" & HI.UL.ULF.rpQuoted(R!FTMainMatSpecCode.ToString)
                xlWorkSheet.Cells(_Row + 3, _Col) = " " & HI.UL.ULF.rpQuoted(R!FTMainMatSpec.ToString)
                xlWorkSheet.Cells(_Row + 4, _Col) = " PO. No.:" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString)
                xlWorkSheet.Cells(_Row + 5, _Col) = " Material Code:" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString)
                xlWorkSheet.Cells(_Row + 6, _Col) = " PO. Line Item: " & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString)
                xlWorkSheet.Cells(_Row + 7, _Col) = " Q'TY:" & R!FNQuantity.ToString
                If HI.UL.ULF.rpQuoted(R!FNHSysProvinceId.ToString).Length = 4 Then
                    xlWorkSheet.Cells(_Row + 8, _Col) = " PLANT :" & HI.UL.ULF.rpQuoted(R!FNHSysProvinceId.ToString)
                Else
                    xlWorkSheet.Cells(_Row + 8, _Col) = " SHIP TO :" & HI.UL.ULF.rpQuoted(R!FNHSysProvinceId.ToString)
                End If

                xlWorkSheet.Cells(_Row + 9, _Col) = " Invoice No.: " & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString)

                Dim _xxDt As System.Data.DataTable = GetMeas(HI.UL.ULF.rpQuoted(R!FTPORef.ToString))

                xlWorkSheet.Cells(_Row, _Col + 8) = "NO CAB "
                xlWorkSheet.Cells(_Row + 1, _Col + 8) = "IN DIAMOND"
                xlWorkSheet.Cells(_Row + 2, _Col + 8) = "" & HI.UL.ULF.rpQuoted(R!FTProvinceName.ToString) & ","
                xlWorkSheet.Cells(_Row + 3, _Col + 8) = " " & HI.UL.ULF.rpQuoted(R!FTCountryName.ToString)
                xlWorkSheet.Cells(_Row + 4, _Col + 8) = "COUNTRY OF"
                xlWorkSheet.Cells(_Row + 5, _Col + 8) = "ORIGIN :" & HI.UL.ULF.rpQuoted(R!FTCountry.ToString)
                Dim x As Integer = 0
                For Each Rx As DataRow In _xxDt.Rows
                    xlWorkSheet.Cells(_Row + 6 + x, _Col + 8) = "  " & HI.UL.ULF.rpQuoted(Rx!FNCNT.ToString) & " CMS / CTNS"
                    x += +1
                Next


                xlWorkSheet.Cells(_Row, _Col + 14) = "Remark "
                _Cmd = "SELECT FTPckPlanNo, FTPORef,   FTRangeNo ,count(FTSizeBreakDown) AS FTSizeBreakDown"
                _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D with(nolock)  "
                _Cmd &= vbCrLf & "where  FTPckPlanNo='" & HI.UL.ULF.rpQuoted(R!FTPckPlanNo.ToString) & "'"
                _Cmd &= vbCrLf & "and   FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Cmd &= vbCrLf & "Group by  FTPckPlanNo ,FTPORef, FTRangeNo "
                Dim _xDt As New System.Data.DataTable
                _xDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                If _xDt.Select("FTSizeBreakDown > 1 ", "FTSizeBreakDown desc").Length > 1 Then
                    xlWorkSheet.Cells(_Row + 1, _Col + 14) = " MSR "
                End If


                _PoRef = R!FTPORef.ToString

                _Row += +(15 + x)
            Next


            xlWorkBook.SaveAs(_Path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            xlWorkBook.Close(True, misValue, misValue)
            xlApp.Quit()

            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
            _Spls.Close()

            Try
                Process.Start(_Path)
            Catch ex As Exception
            End Try



        Catch ex As Exception
            _Spls.Close()
        End Try
    End Function



    Private Function ExportPackingPlan(oDt As System.Data.DataTable, _Path As String) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("กำลังบันทึก Packing Plan กรุณารอซักครู่")
        Try
            Dim _Cmd As String = ""
            Dim _Row As Integer = 1
            Dim _Col As Integer = 1
            Dim _rPO As Integer = 0
            Dim _PoRef As String = ""




            'Dim _Path As String = "C:\ExportDataForm_WISDOM\"


            If Not (Directory.Exists(_Path)) Then
                My.Computer.FileSystem.CreateDirectory(_Path)
            End If


            'xlWorkSheet.Cells(1, 1) = "Sheet 1"

            _PoRef = ""

            Dim _oDM As System.Data.DataTable
            Dim _oDT As System.Data.DataTable
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim _SavePath As String = _Path


            For Each R As DataRow In oDt.Rows
                Dim misValue As Object = System.Reflection.Missing.Value
                Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()

                If xlApp Is Nothing Then
                    HI.MG.ShowMsg.mInfo("Excel is not properly installed!!", 1803210834, Me.Text, "", MessageBoxIcon.Hand)
                End If


                xlWorkBook = xlApp.Workbooks.Add(misValue)
                xlWorkSheet = xlWorkBook.Sheets("sheet1")

                Dim _FileName As String = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(R!FTPORefNo.ToString), "\", "")
                _FileName = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(_FileName), "/", "")
                _FileName = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(_FileName), "?", "")
                _FileName = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(_FileName), ":", "")
                _FileName = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(_FileName), "*", "")
                _FileName = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(_FileName), ">", "")
                _FileName = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(_FileName), "<", "")
                _FileName = Microsoft.VisualBasic.Replace(HI.UL.ULF.rpQuoted(_FileName), " ", "")
                _SavePath = _Path & "\" & "placking_Plan_" & _FileName & ".xls"



                _Cmd = " SELECT  distinct  FTPckPlanNo, P.FTPORef, FNQuantity, FNPackcount, FNNet, FNTotalNet, FNGrossWeight,P.FNHSysUnitId, FNVol, FTVolUnit, isnull( FTApproveState ,'0') as FTApproveState , FTApproveBy,  FDApproveDate "
                _Cmd &= vbCrLf & "    ,  isnull(FNHSysMainMatSpecId,0) as  FNHSysMainMatSpecId   , P.FNHSysProvinceId as  FNHSysProvinceId_Hide  "
                '_Cmd &= vbCrLf & " ,  (Select Top 1 isnull(FTMainMatSpecEN,'') as FTMainMatSpecEN  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMatSpec  WITH (NOLOCK)  Where FNHSysMainMatSpecId = P.FNHSysMainMatSpecId )  as FTMainMatSpec  "
                '_Cmd &= vbCrLf & " ,  (Select Top 1 isnull(FTProvinceCode,'') as FTProvinceCode  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince  WITH (NOLOCK)  Where FNHSysProvinceId = P.FNHSysProvinceId )  as FNHSysProvinceId  "
                '_Cmd &= vbCrLf & " ,  V.FTPlantCode  , P.FTDiamondMarkCode ,  V.FTProvinceNameEN  as FTProvinceName ,  V.FTCountryNameEN as FTCountryName "
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatSpecCode.ToString) & "' AS  FTMainMatSpecCode "
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatSpec.ToString) & "' AS  FTMainMatSpec "
                _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan AS P WITH(NOLOCK)  "
                '_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. V_ExportInvoice_H AS V ON P.FNHSysProvinceId = V.FNHSysProvinceId AND P.FDShipDate = V.FDShipDate AND P.FTPORef = V.FTPORef  "
                _Cmd &= vbCrLf & " where   P.FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Cmd &= vbCrLf & " and    P.FTPORefNo='" & HI.UL.ULF.rpQuoted(R!FTPORefNo.ToString) & "'"
                _oDM = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)



                _Cmd = " SELECT   FTPckPlanNo, FTPORef, FTRangeNo, FNFrom, FNTo, FTSerialFrom, FTSerialTo, FTPackInstructionCode, FTLineNo, FTStyleCode, FTSKU, FTPONo, "
                _Cmd &= vbCrLf & "  FTPOLineNo, FTColorWay, FTSizeBreakDown, FTShortDescription, FTShipmentMethod,   FNItemQty, FNQtyPerPack, FNInnerPackCount, FNPackCount,  "
                _Cmd &= vbCrLf & "      FTR, FTPackCode, FNNetWeight, FNTotalNetWeight, FNGrossNetWeight, FTUnitCode,  FNL, FNW, FNH, FTItemUnitCode, FTScanID , FTPackCode as FTCartonCode "
                _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D AS D  WITH(NOLOCK)  "
                '_Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK)  ON     D.FNHSysCartonId  = C.FNHSysCartonId   "
                _Cmd &= vbCrLf & " where  FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Cmd &= vbCrLf & " and    FTPORefNo='" & HI.UL.ULF.rpQuoted(R!FTPORefNo.ToString) & "'"
                _Cmd &= vbCrLf & " Order by  FTRangeNo  asc "
                _oDT = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                For Each H As DataRow In _oDM.Rows

                    'Header 
                    With xlWorkSheet
                        _Row = 1
                        '.ShowGridlines = False
                        ' .Worksheets(0).ActiveView.ShowGridlines = False
                        '.Sheets("sheet1").ShowGridlines = False


                        .Range("A1:A1").ColumnWidth = 0
                        .Range("B1:B1").ColumnWidth = 0
                        .Range("C1:C1").ColumnWidth = 6
                        .Range("D1:D1").ColumnWidth = 3.2
                        .Range("E1:E1").ColumnWidth = 3.2
                        .Range("F1:F1").ColumnWidth = 4.22
                        .Range("G1:G1").ColumnWidth = 0
                        .Range("H1:H1").ColumnWidth = 5
                        .Range("I1:I1").ColumnWidth = 0
                        .Range("J1:J1").ColumnWidth = 5
                        .Range("K1:K1").ColumnWidth = 0
                        .Range("L1:L1").ColumnWidth = 3.56
                        .Range("M1:M1").ColumnWidth = 4.22
                        .Range("N1:N1").ColumnWidth = 4.22
                        .Range("O1:O1").ColumnWidth = 4.89
                        .Range("P1:P1").ColumnWidth = 4.78
                        .Range("Q1:Q1").ColumnWidth = 0
                        .Range("R1:R1").ColumnWidth = 2.7
                        .Range("S1:S1").ColumnWidth = 3.5
                        .Range("T1:W1").ColumnWidth = 4.22
                        .Range("X1:X1").ColumnWidth = 1.5
                        .Range("Y1:Y1").ColumnWidth = 3
                        .Range("Z1:Z1").ColumnWidth = 4.22
                        .Range("AA1:AA1").ColumnWidth = 4.22
                        .Range("AB1:AB1").ColumnWidth = 4.11
                        .Range("AC1:AC1").ColumnWidth = 0
                        .Range("AD1:AD1").ColumnWidth = 3.11
                        .Range("AE1:AE1").ColumnWidth = 0
                        .Range("AF1:AF1").ColumnWidth = 4.22
                        .Range("AG1:AG1").ColumnWidth = 3.22
                        .Range("AH1:AH1").ColumnWidth = 0
                        .Range("AH1:AH1").ColumnWidth = 0
                        .Range("AI1:AI1").ColumnWidth = 4.22
                        .Range("AJ1:AJ1").ColumnWidth = 2.89




                        .Range(.Cells(_Row, _Col), .Cells(_Row, _Col + 7)).Merge()
                        .Rows(1).Font.Name = "Arial"
                        .Rows(1).Font.size = 14
                        .Rows(1).Font.Bold = True
                        .Cells(_Row, _Col).Value = "Packing Plan"
                        .Range(.Cells(_Row + 2, _Col + 2), .Cells(_Row + 2, _Col + 7)).Merge()
                        .Rows(3).Font.Name = "Arial"
                        .Rows(3).Font.size = 9
                        .Cells(_Row + 2, _Col + 2).Value = "Plan Ref Number"
                        .Range(.Cells(_Row + 2, _Col + 8), .Cells(_Row + 2, _Col + 13)).Merge()
                        ' .Range(.Cells(_Row + 2, _Col + 8), .Cells(_Row + 2, _Col + 13)).HorizontalAlignment = HorizontalAlignment.Left
                        .Cells(_Row + 2, _Col + 8).Value = "'" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) ' PORef

                        '.Cells(_Row + 2, _Col + 8).VerticalAlignment = Left
                        .Cells(_Row + 2, _Col + 8).Font.Bold = True
                        .Range(.Cells(_Row + 2, _Col + 14), .Cells(_Row + 2, _Col + 19)).Merge()
                        If R!FTCustPORef.ToString <> "" Then
                            .Cells(_Row + 2, _Col + 14).Value = "CUST PO. " & R!FTCustPORef.ToString
                        End If

                        .Range(.Cells(_Row + 2, _Col + 22), .Cells(_Row + 2, _Col + 31)).Merge()
                        .Cells(_Row + 2, _Col + 22).Font.Bold = True


                        If R!FDCfmShipDate.ToString <> "" Then
                            .Cells(_Row + 2, _Col + 22).Value = "SHIPMENT  " & HI.UL.ULDate.ConvertEN(R!FDCfmShipDate.ToString) & "   " & R!FTVenderPramCode.ToString & "   (" & R!FTCmpCode.ToString & ")"
                        Else
                            .Cells(_Row + 2, _Col + 22).Value = "SHIPMENT  " & HI.UL.ULDate.ConvertEN(R!FDShipDate.ToString) & "   " & R!FTVenderPramCode.ToString & "   (" & R!FTCmpCode.ToString & ")"
                        End If

                        '.Cells(_Row + 2, _Col + 22)
                        .Range(.Cells(_Row + 3, _Col), .Cells(_Row + 3, _Col + 32)).Borders(9).Weight = 2
                        .Range(.Cells(_Row + 5, _Col), .Cells(_Row + 5, _Col + 14)).Merge()
                        .Rows(6).Font.Name = "Arial"
                        .Rows(6).Font.size = 11
                        .Rows(6).Font.Bold = True
                        .Cells(_Row + 5, _Col).Value = "PO SUMMARY"
                        ' table summary
                        .Rows(_Row + 6).Font.Name = "Arial"
                        .Rows(_Row + 6).Font.size = 7
                        .Range(.Cells(_Row + 6, _Col + 5), .Cells(_Row + 6, _Col + 6)).Merge() : .Range(.Cells(_Row + 6, _Col + 7), .Cells(_Row + 6, _Col + 8)).Merge() : .Range(.Cells(_Row + 6, _Col + 9), .Cells(_Row + 6, _Col + 10)).Merge()
                        .Cells(_Row + 6, _Col + 2).Value = "PO Number" : .Cells(_Row + 6, _Col + 3).Value = "Item Qty" : .Cells(_Row + 6, _Col + 4).Value = "Pkg Count" : .Cells(_Row + 6, _Col + 5).Value = "Net Net" : .Cells(_Row + 6, _Col + 7).Value = "Net"
                        .Cells(_Row + 6, _Col + 9).Value = "Gross" : .Cells(_Row + 6, _Col + 11).Value = "Unit" : .Cells(_Row + 6, _Col + 12).Value = "Vol" : .Cells(_Row + 6, _Col + 13).Value = "Unit"


                        .Range(.Cells(_Row + 6, _Col + 23), .Cells(_Row + 6, _Col + 27)).Merge()
                        .Cells(_Row + 6, _Col + 23).Value = R!FTDiamondMarkCode.ToString
                        .Cells(_Row + 6, _Col + 23).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                        .Range(.Cells(_Row + 7, _Col + 20), .Cells(_Row + 7, _Col + 33)).Merge()
                        .Cells(_Row + 7, _Col + 20).Value = R!FTProvinceName.ToString
                        .Cells(_Row + 7, _Col + 20).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

                        .Cells(_Row + 6, _Col + 23).Font.Name = "Arial"
                        .Cells(_Row + 6, _Col + 23).Font.size = 12
                        .Cells(_Row + 6, _Col + 23).Font.Bold = True

                        .Cells(_Row + 7, _Col + 20).Font.Name = "Arial"
                        .Cells(_Row + 7, _Col + 20).Font.size = 12
                        .Cells(_Row + 7, _Col + 20).Font.Bold = True
                        .Rows(16).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter

                        _Row = _Row + 7

                        'values
                        .Rows(_Row).Font.Name = "Arial"
                        .Rows(_Row).Font.size = 7
                        .Range(.Cells(_Row, _Col + 5), .Cells(_Row, _Col + 6)).Merge() : .Range(.Cells(_Row, _Col + 7), .Cells(_Row, _Col + 8)).Merge() : .Range(.Cells(_Row, _Col + 9), .Cells(_Row, _Col + 10)).Merge()
                        .Cells(_Row, _Col + 2).Value = HI.UL.ULF.rpQuoted(R!FTPckPlanNo.ToString) ' packing No
                        .Cells(_Row, _Col + 3).Value = H!FNQuantity.ToString 'Item Qty
                        .Cells(_Row, _Col + 4).Value = H!FNPackcount.ToString
                        .Cells(_Row, _Col + 5).Value = H!FNNet.ToString
                        .Cells(_Row, _Col + 7).Value = H!FNTotalNet.ToString
                        .Cells(_Row, _Col + 9).Value = H!FNGrossWeight.ToString
                        .Cells(_Row, _Col + 11).Value = "KG"
                        .Cells(_Row, _Col + 12).Value = H!FNVol.ToString
                        .Cells(_Row, _Col + 13).Value = H!FTVolUnit.ToString
                        _Row = _Row + 1
                        .Rows(_Row).Font.Name = "Arial"
                        .Rows(_Row).Font.size = 7
                        .Cells(_Row, _Col + 2).Value = "'Totals"
                        .Cells(_Row, _Col + 3).Value = H!FNQuantity.ToString 'Item Qty
                        .Cells(_Row, _Col + 4).Value = H!FNPackcount.ToString
                        .Cells(_Row, _Col + 5).Value = H!FNNet.ToString
                        .Cells(_Row, _Col + 5).NumberFormat = "#,##0.00"
                        .Cells(_Row, _Col + 7).Value = H!FNTotalNet.ToString
                        .Cells(_Row, _Col + 7).NumberFormat = "#,##0.00"
                        .Cells(_Row, _Col + 9).Value = H!FNGrossWeight.ToString
                        .Cells(_Row, _Col + 9).NumberFormat = "#,##0.00"

                        .Cells(_Row, _Col + 11).Value = "KG"
                        .Cells(_Row, _Col + 12).Value = H!FNVol.ToString
                        .Cells(_Row, _Col + 12).NumberFormat = "#,##0.000"
                        .Cells(_Row, _Col + 13).Value = H!FTVolUnit.ToString

                        With .Range("C7:N9")
                            .Borders.Weight = 2
                            '.Borders(8).Weight = 2
                            '.Borders(9).Weight = 2
                            '.Borders(10).Weight = 2
                        End With
                        With .Range("X7:AB7")
                            .Borders(7).Weight = 2
                            .Borders(8).Weight = 2
                            .Borders(9).Weight = 2
                            .Borders(10).Weight = 2
                        End With
                        With .Range("U8:AG8")
                            .Borders(7).Weight = 2
                            .Borders(8).Weight = 2
                            .Borders(9).Weight = 2
                            .Borders(10).Weight = 2
                        End With


                        'End table summary

                        _Row = _Row - 1
                        _Row = _Row + 4

                        .Range(.Cells(11, _Col), .Cells(11, _Col + 32)).Borders(9).Weight = 2
                        _Row = _Row + 2 ' 13
                        .Range(.Cells(_Row, _Col), .Cells(_Row, _Col + 14)).Merge()
                        .Cells(_Row, _Col).Value = "PACKAGE DETAIL"
                        .Rows(_Row).Font.Name = "Arial"
                        .Rows(_Row).Font.size = 11
                        .Rows(_Row).Font.Bold = True
                        'table Detail
                        _Row = _Row + 2 '= 15

                        .Rows(_Row).Font.Name = "Arial"
                        .Rows(_Row).Font.size = 7

                        .Cells(_Row, _Col + 3).Value = "Range"
                        .Cells(_Row, _Col + 4).Value = "From"
                        .Cells(_Row, _Col + 5).Value = "To"
                        .Range(.Cells(_Row, _Col + 7), .Cells(_Row, _Col + 8)).Merge()
                        .Cells(_Row, _Col + 7).Value = "Serial From"
                        .Range(.Cells(_Row, _Col + 9), .Cells(_Row, _Col + 10)).Merge()
                        .Cells(_Row, _Col + 9).Value = "Serial To"
                        .Cells(_Row, _Col + 11).Value = "Pack Instruction Code"
                        .Cells(_Row, _Col + 12).Value = "Line #"
                        .Cells(_Row, _Col + 13).Value = "Buyer Item #"
                        .Cells(_Row, _Col + 14).Value = "MAIN LINE #"
                        .Range(.Cells(_Row, _Col + 15), .Cells(_Row, _Col + 16)).Merge()
                        .Cells(_Row, _Col + 15).Value = "Color Name"
                        .Cells(_Row, _Col + 17).Value = "Size"
                        .Cells(_Row, _Col + 18).Value = "Short Description"
                        .Cells(_Row, _Col + 19).Value = "Shipment Method"
                        .Cells(_Row, _Col + 20).Value = "Item Qty"
                        .Cells(_Row, _Col + 21).Value = "Qty per Pkg/Inner Pack"
                        .Cells(_Row, _Col + 22).Value = "Pkg Count"
                        .Cells(_Row, _Col + 23).Value = "R"
                        .Cells(_Row, _Col + 24).Value = "Pkg Code"
                        .Cells(_Row, _Col + 25).Value = "Net Net"
                        .Cells(_Row, _Col + 26).Value = "Net"
                        .Range(.Cells(_Row, _Col + 27), .Cells(_Row, _Col + 28)).Merge()
                        .Cells(_Row, _Col + 27).Value = "Gross"
                        .Range(.Cells(_Row, _Col + 29), .Cells(_Row, _Col + 30)).Merge()
                        .Cells(_Row, _Col + 29).Value = "Unit"
                        .Cells(_Row, _Col + 31).Value = "L"
                        .Range(.Cells(_Row, _Col + 32), .Cells(_Row, _Col + 33)).Merge()
                        .Cells(_Row, _Col + 32).Value = "W"
                        .Cells(_Row, _Col + 34).Value = "H"
                        .Cells(_Row, _Col + 35).Value = "Unit"

                        For i As Integer = 1 To 14
                            .Cells(7, i).WrapText = True

                        Next


                        For i As Integer = 1 To 35
                            .Cells(_Row, i).WrapText = True
                            '.Range(.Cells(_Row, i)).HorizontalAlignment = HorizontalAlignment.Center
                        Next


                        'value detail
                        _Row = _Row + 1
                        For i As Integer = 1 To _Row
                            .Rows(i).RowHeight = 23
                        Next
                        .Rows(16).RowHeight = 35
                        .Rows(2).RowHeight = 0
                        .Rows(4).RowHeight = 5
                        .Rows(5).RowHeight = 0
                        .Rows(10).RowHeight = 0
                        .Rows(12).RowHeight = 0.2
                        .Rows(13).RowHeight = 0
                        .Rows(17).RowHeight = 0
                        Dim zRow As Integer = _Row
                        Dim _RangeNoOld As String = ""
                        For Each Z As DataRow In _oDT.Rows
                            zRow += +1


                            .Rows(zRow).Font.Name = "Arial"
                            .Rows(zRow).Font.size = 7
                            .Rows(zRow).RowHeight = 23
                            If _RangeNoOld <> Z!FTRangeNo.ToString Then
                                .Cells(zRow, _Col + 3).Value = "'" & Z!FTRangeNo.ToString
                                .Cells(zRow, _Col + 4).Value = "" & Z!FNFrom.ToString
                                .Cells(zRow, _Col + 5).Value = "" & Z!FNTo.ToString
                                .Cells(zRow, _Col + 7).Value = "" & Z!FTSerialFrom.ToString
                                .Cells(zRow, _Col + 9).Value = "" & Z!FTSerialTo.ToString




                                .Cells(zRow, _Col + 22).Value = "" & Z!FNPackCount.ToString
                                .Cells(zRow, _Col + 23).Value = "" & Z!FTR.ToString
                                .Cells(zRow, _Col + 24).Value = "" & Z!FTCartonCode.ToString
                                .Cells(zRow, _Col + 25).Value = "" & Z!FNNetWeight.ToString
                                .Cells(zRow, _Col + 25).NumberFormat = "#,##0.000"
                                .Cells(zRow, _Col + 26).Value = "" & Z!FNTotalNetWeight.ToString
                                .Cells(zRow, _Col + 26).NumberFormat = "#,##0.000"
                                .Cells(zRow, _Col + 27).Value = "" & Z!FNGrossNetWeight.ToString
                                .Cells(zRow, _Col + 27).NumberFormat = "#,##0.000"
                                .Cells(zRow, _Col + 29).Value = "KG" ' & Z!FTUnitCode.ToString
                                .Cells(zRow, _Col + 31).Value = "" & Z!FNL.ToString
                                .Cells(zRow, _Col + 31).NumberFormat = "#,##0.000"
                                .Cells(zRow, _Col + 32).Value = "" & Z!FNW.ToString
                                .Cells(zRow, _Col + 32).NumberFormat = "#,##0.000"
                                .Cells(zRow, _Col + 34).Value = "" & Z!FNH.ToString
                                .Cells(zRow, _Col + 34).NumberFormat = "#,##0.000"
                                .Cells(zRow, _Col + 35).Value = "MR" '& Z!FTUnitCode.ToString


                            Else

                            End If


                            .Cells(zRow, _Col + 11).Value = "" & Z!FTPackInstructionCode.ToString
                            .Cells(zRow, _Col + 12).Value = "'" & Z!FTLineNo.ToString
                            .Cells(zRow, _Col + 13).Value = "" & Z!FTStyleCode.ToString
                            .Cells(zRow, _Col + 14).Value = "'" & Z!FTPOLineNo.ToString
                            .Cells(zRow, _Col + 15).Value = "" & Z!FTColorWay.ToString
                            .Cells(zRow, _Col + 17).Value = "" & Z!FTSizeBreakDown.ToString
                            .Cells(zRow, _Col + 18).Value = "" & Z!FTShortDescription.ToString
                            .Cells(zRow, _Col + 19).Value = "" & Z!FTShipmentMethod.ToString
                            .Cells(zRow, _Col + 20).Value = "" & Z!FNItemQty.ToString
                            .Cells(zRow, _Col + 21).Value = "" & Z!FNQtyPerPack.ToString




                            .Cells(zRow, _Col + 3).WrapText = True
                            .Cells(zRow, _Col + 4).WrapText = True
                            .Cells(zRow, _Col + 5).WrapText = True
                            .Cells(zRow, _Col + 7).WrapText = True
                            .Cells(zRow, _Col + 9).WrapText = True
                            .Cells(zRow, _Col + 11).WrapText = True
                            .Cells(zRow, _Col + 12).WrapText = True
                            .Cells(zRow, _Col + 13).WrapText = True
                            .Cells(zRow, _Col + 14).WrapText = True
                            .Cells(zRow, _Col + 15).WrapText = True
                            .Cells(zRow, _Col + 17).WrapText = True
                            .Cells(zRow, _Col + 18).WrapText = True
                            .Cells(zRow, _Col + 19).WrapText = True
                            .Cells(zRow, _Col + 20).WrapText = True
                            .Cells(zRow, _Col + 21).WrapText = True
                            .Cells(zRow, _Col + 22).WrapText = True
                            .Cells(zRow, _Col + 23).WrapText = True
                            .Cells(zRow, _Col + 24).WrapText = True
                            .Cells(zRow, _Col + 25).WrapText = True
                            .Cells(zRow, _Col + 26).WrapText = True
                            .Cells(zRow, _Col + 27).WrapText = True

                            _RangeNoOld = Z!FTRangeNo.ToString

                        Next

                        With .Range("D16:AJ" & zRow)
                            .Borders.Weight = 2
                            '.Borders(8).Weight = 2
                            '.Borders(9).Weight = 2
                            '.Borders(10).Weight = 2
                        End With


                        'End table Detail
                        zRow = zRow + 3
                        _Row = zRow
                        .Range(.Cells(zRow, 3), .Cells(zRow, 6)).Merge()
                        .Cells(zRow, 3).Value = "DESCRIPTION :"
                        .Cells(zRow, 7).Value = "" & H!FTMainMatSpecCode.ToString
                        .Range(.Cells(zRow, 7), .Cells(zRow, 29)).Merge()

                        zRow = zRow + 1
                        .Range(.Cells(zRow, 3), .Cells(zRow, 6)).Merge()
                        .Cells(zRow, 3).Value = "SHELL :"
                        .Cells(zRow, 7).Value = "" & H!FTMainMatSpec.ToString
                        '  .Range("B:B", misValue).EntireColumn.Hidden = True
                        .Range(.Cells(zRow, 7), .Cells(zRow, 29)).Merge()


                    End With


                    'Header


                    xlWorkBook.SaveAs(_SavePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                    xlWorkBook.Close(True, misValue, misValue)
                    xlApp.Quit()

                    releaseObject(xlWorkSheet)
                    releaseObject(xlWorkBook)
                    releaseObject(xlApp)

                    xlWorkSheet = Nothing
                    xlWorkSheet = Nothing
                    misValue = Nothing
                    Try
                        Process.Start(_SavePath)
                    Catch ex As Exception
                    End Try



                Next

            Next



            _Spls.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            _Spls.Close()
            Return False

        End Try
    End Function





    Private Function GetMeas(_PoRef As String) As System.Data.DataTable
        Try
            Dim _Cmd As String = ""

            _Cmd = " SELECT Distinct  D.FTPORef  , D.FNHSysCartonId  , case when isnull(CC.FNWidth,0)  = 0 then '0' else   "
            _Cmd &= vbCrLf & " convert(nvarchar(30),  convert(numeric(18,2),(isnull(CC.FNWidth,0) /10))) + ' x ' +  convert(nvarchar(30), convert(numeric(18,2) , ( CC.FNLength/10)) ) +' x '+  convert(nvarchar(30),  convert(numeric(18,2),(CC.FNWeight * 10))) end AS FNCNT"
            _Cmd &= vbCrLf & "    FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) LEFT OUTER  JOIN  "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice_D AS D WITH (NOLOCK) ON H.FTInvoiceNo = D.FTInvoiceNo  "
            _Cmd &= vbCrLf & "    LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..V_TCNMCarton AS CC ON D.FNHSysCartonId = CC.FNHSysCartonId "
            _Cmd &= vbCrLf & " where  D.FTPORef ='" & _PoRef & "'"

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception

        End Try
    End Function

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Shared Sub ExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    '  DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)

                        'Dim _XlsxExportOption As New DevExpress.XtraPrinting.XlsxExportOptions()
                        '_XlsxExportOption.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text

                        '.ExportToXlsx(Op.FileName, _XlsxExportOption)

                        .ExportToXlsx(Op.FileName)

                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try

                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsavepreinv_Click(sender As Object, e As EventArgs) Handles ocmsavepreinv.Click
        Dim _Spls As New HI.TL.SplashScreen("Generate  an Pre Invoice, Please wait.")
        Try

            Dim _PreInvoice As String = "" : Dim _PreInvoiceGrp As String = ""
            Dim _Pass As Boolean = False
            Dim _Cmd As String = "" : Dim _DocInvoce As String = ""
            Dim _StateFormatDoc As Boolean = True
            Dim _DocRefDocGrpNo As String = "" : Dim _FTStateDocRefSort As Boolean = False
            Dim _ProcFormat As Boolean = False

            With DirectCast(Me.ogcPlandM.DataSource, System.Data.DataTable)
                .AcceptChanges()

                If .Select("FTSelect ='1' and FTInvoiceNo <> ''  ").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("Pls check data select !!!!", 1803151437, Me.Text, "", MessageBoxIcon.Stop)
                    _Spls.Close()
                    Exit Sub
                End If

                For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNoPre <> ''", "FTInvoiceNo  ASC ")
                    HI.MG.ShowMsg.mInfo("Pls check data select !!!!", 1803151437, Me.Text, "", MessageBoxIcon.Stop)
                    _Spls.Close()
                    Exit Sub
                Next

                For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                    If Not (_DocRefDocGrpNo = Microsoft.VisualBasic.Left(R!FTInvoiceNo.ToString, Microsoft.VisualBasic.Len(R!FTInvoiceNo.ToString) - 1)) And Not (_DocRefDocGrpNo = "") Then
                        _StateFormatDoc = False
                    Else
                        If Not (_DocRefDocGrpNo = "") Then
                            _FTStateDocRefSort = True
                        End If
                    End If
                    _Cmd = "SELECT Top 1  FTInvoiceRefNo  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) where FTInvoiceNo = '" & R!FTInvoiceNo.ToString & "'"
                    _DocRefDocGrpNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")


                Next

                If _StateFormatDoc Then
                    If .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ").Length = 1 Then
                        For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                            '  _PreInvoice &= Microsoft.VisualBasic.Right(R!FTInvoiceNo.ToString, 1)
                            If Not _Pass Then
                                _Cmd = "SELECT Top 1  FTInvoiceRefNo  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) where FTInvoiceNo = '" & R!FTInvoiceNo.ToString & "'"
                                _DocInvoce = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")
                                _Pass = True
                            End If
                        Next
                    Else
                        For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                            _PreInvoice &= Microsoft.VisualBasic.Right(R!FTInvoiceNo.ToString, 1)
                            If Not _Pass Then
                                _Cmd = "SELECT Top 1  FTInvoiceRefNo  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) where FTInvoiceNo = '" & R!FTInvoiceNo.ToString & "'"
                                _DocInvoce = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")
                                _Pass = True
                            End If
                        Next
                    End If

                    _PreInvoiceGrp = _DocInvoce & _PreInvoice
                Else
                    _DocRefDocGrpNo = ""
                    _PreInvoice = ""
                    Dim _RCount As Integer = 1 : _DocInvoce = ""
                    For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                        _Cmd = "SELECT Top 1  FTInvoiceRefNo  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) where FTInvoiceNo = '" & R!FTInvoiceNo.ToString & "'"
                        _DocInvoce = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")
                        If (_FTStateDocRefSort) And (_DocRefDocGrpNo = "" Or _DocRefDocGrpNo = _DocInvoce) Then
                            _PreInvoice &= Microsoft.VisualBasic.Right(R!FTInvoiceNo.ToString, 1)
                            _ProcFormat = True
                        Else
                            _ProcFormat = False
                        End If

                        If _ProcFormat = False Then
                            If _RCount <= 2 Then
                                _PreInvoice = ""
                                _DocInvoce = R!FTInvoiceNo.ToString
                            End If
                            If _DocRefDocGrpNo <> "" Then
                                _DocInvoce = _DocRefDocGrpNo
                            End If
                            Exit For
                        End If

                        _DocRefDocGrpNo = _DocInvoce
                        _RCount += +1
                    Next

                    _PreInvoiceGrp = _DocInvoce & _PreInvoice

                End If





                Try

                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                    For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                        _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_Ref "
                        _Cmd &= vbCrLf & "Set FTUpdUser = '" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & "where  FTInvoiceNo ='" & _PreInvoiceGrp & "'"
                        _Cmd &= vbCrLf & " and  FTInvoiceRefNo='" & R!FTInvoiceNo.ToString & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_Ref (FTInsUser, FDInsDate, FTInsTime,  FTInvoiceNo, FTInvoiceRefNo) "
                            _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & _PreInvoiceGrp & "'"
                            _Cmd &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "'"
                            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        End If
                    Next



                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try
                _Cmd = " Exec   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..[SP_EXPORT_PREINVOICE_GRP] '" & _PreInvoiceGrp & "','" & HI.ST.UserInfo.UserName & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                Dim _oDt As System.Data.DataTable
                Dim _Amt As Double = 0
                Dim FTTotalAmountENB As String = ""
                Dim FTTotalAmountTHB As String = ""
                Dim _VatAmt As Double = 0
                Dim _GrandAmt As Double = 0

                _Cmd = "   Select D.FTInvoiceNo, D.FTPORef, D.FNHSysStyleId, D.FNCTNS, D.FNTNW, D.FNTGW, D.FNQuantity, D.FNUnitPrice, D.FNTotalAmount   "
                _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D As D  "
                _Cmd &= vbCrLf & " where  D.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_PreInvoiceGrp) & "'"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)



                Try
                    _Amt = Double.Parse(_oDt.Compute("sum(FNTotalAmount)", "FTPORef <> ''"))
                    _VatAmt = Format((_Amt * 0) / 100, HI.ST.Config.AmtFormat)
                    _GrandAmt = _Amt + _VatAmt
                Catch ex As Exception
                    _Amt = 0
                    _VatAmt = 0
                    _GrandAmt = 0
                End Try

                FTTotalAmountENB = HI.UL.ULF.Convert_Bath_EN(_GrandAmt)
                FTTotalAmountTHB = HI.UL.ULF.Convert_Bath_TH(_GrandAmt)



                _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice "
                _Cmd &= vbCrLf & " set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Cmd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & " , FNAmt=" & _Amt
                _Cmd &= vbCrLf & " , FNDisCountPer=0"
                _Cmd &= vbCrLf & " , FNDisCountAmt=0"
                _Cmd &= vbCrLf & " , FNNetAmt=" & _Amt
                _Cmd &= vbCrLf & " , FNVatPer=0"
                _Cmd &= vbCrLf & " , FNVatAmt=" & _VatAmt
                _Cmd &= vbCrLf & " , FNSurcharge=0"
                _Cmd &= vbCrLf & " , FNTotalAmount=" & _GrandAmt
                _Cmd &= vbCrLf & " ,   FTTotalAmountTHB='" & HI.UL.ULF.rpQuoted(FTTotalAmountTHB) & "' "
                _Cmd &= vbCrLf & " ,   FTTotalAmountENB='" & HI.UL.ULF.rpQuoted(FTTotalAmountENB) & "' "
                _Cmd &= vbCrLf & " where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_PreInvoiceGrp) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                    R!FTInvoiceNoPre = _PreInvoiceGrp
                    R!FTSelect = "0"
                Next


            End With
            _Spls.Close()
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            '  ocmload_Click(sender, e)

            Dim view As ColumnView = Me.ogvPlandM
            view.ActiveFilter.Clear()

        Catch ex As Exception
            _Spls.Close()
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End Try
    End Sub
    Private _StateSetSelectAll As Boolean = True


    'Private Sub RepositoryItemFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemFTSelect.EditValueChanging
    '    Try

    '        With ogvPlandM
    '            Dim _FTPlantCode As String = .GetRowCellValue(.FocusedRowHandle, "FTPlantCode")
    '            Dim x As Integer = 0
    '            For i As Integer = 0 To .RowCount Step 1
    '                If .FocusedRowHandle <> i And _FTPlantCode = .GetRowCellValue(i, "FTPlantCode") Then
    '                    x += +2

    '                End If

    '            Next


    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub filterinfo(pland As String)
        Try
            Dim view As ColumnView = Me.ogvPlandM
            Dim viewFilterInfo As New ViewColumnFilterInfo(view.Columns("FTPlantCode"),
              New ColumnFilterInfo("[FTPlantCode] = '" & pland & "'", ""))
            view.ActiveFilter.Add(viewFilterInfo)
            cFDShipDate2.SortOrder = DevExpress.Data.ColumnSortOrder.Descending
            cFDShipDate2.SortIndex = 1
            cFTInvoiceNo.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            cFTInvoiceNo.SortIndex = 2
            GridColumn51.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            GridColumn51.SortIndex = 3
            ' view.ActiveFilter.Clear()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub RepositoryItemFTSelect_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemFTSelect.EditValueChanged
        Try
            Dim view As ColumnView = Me.ogvPlandM
            If view.ActiveFilter.Count >= 1 Then Exit Sub

            With Me.ogvPlandM
                If .RowCount <= 0 Or .FocusedRowHandle < -1 Then Exit Sub
                filterinfo(.GetRowCellValue(.FocusedRowHandle, "FTPlantCode").ToString)
                '   filterinfo(.GetRowCellValue(.FocusedRowHandle, "FTPlantCode").ToString, .GetRowCellValue(.FocusedRowHandle, "FNHSysProvinceId").ToString, .GetRowCellValue(.FocusedRowHandle, "FTShipModeCode").ToString)
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvPlandM_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvPlandM.RowStyle
        Try
            'FTRowType
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim _FTRowType As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTRowType"))
                If _FTRowType = "1" Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                    e.HighPriority = True
                End If
                Dim _ProvinceId As String = ""
                Try
                    _ProvinceId = View.GetRowCellValue(e.RowHandle, View.Columns("FNHSysProvinceId")).ToString
                Catch ex As Exception
                    _ProvinceId = ""
                End Try

                Dim FTPlantCode As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTPlantCode"))

                Dim _ShipModestate As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTShipModeState"))
                Dim _Provincestate As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTProvinceState"))
                Dim _FTSuplCode As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTStateForwarder"))
                Dim _FTMainMatSpec As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTStateMainMat"))
                Dim _FTStateShipDate As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTStateShipDate"))


                If (FTPlantCode = "") Or (_ShipModestate = "1") Or (_Provincestate = "1") Or (_FTSuplCode = "1") Or (_FTMainMatSpec = "1") Or (_FTStateShipDate = "1") Then
                    e.Appearance.BackColor = Color.LightBlue
                    e.Appearance.ForeColor = Color.Black
                    e.HighPriority = True
                End If



                'If (_ProvinceId = "" And FTPlantCode = "") Or _FTSuplCode = "" Or _FTMainMatSpec = "" Then
                '    e.Appearance.BackColor = Color.Red
                '    e.Appearance.BackColor2 = Color.SeaShell
                '    e.HighPriority = True
                'End If

                'If _ShipModestate = "1" Then
                '    e.Appearance.BackColor = Color.Blue
                '    e.Appearance.BackColor2 = Color.SeaShell
                '    e.HighPriority = True

                'End If

                'If _Provincestate = "1" Then
                '    e.Appearance.BackColor = Color.Yellow
                '    e.Appearance.BackColor2 = Color.SeaShell
                '    e.HighPriority = True
                'End If
                'If _Provincestate = "1" Then
                '    e.Appearance.BackColor = Color.LightGreen
                '    e.Appearance.BackColor2 = Color.SeaShell
                '    e.HighPriority = True
                'End If

                'If _Provincestate = "1" And _ShipModestate = "1" And _Provincestate = "1" Then
                '    e.Appearance.BackColor = Color.Gray
                '    e.Appearance.BackColor2 = Color.SeaShell
                '    e.HighPriority = True
                'End If




            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try
            Dim _oDT As System.Data.DataTable : Dim _Path As String = ""
            With DirectCast(Me.ogcPlandM.DataSource, System.Data.DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูลเพื่อสร้าง PackingPlan !!!", 1803210850, Me.Text, "", MessageBoxIcon.Information)
                    Exit Sub
                End If
                _oDT = .Select("FTSelect = '1'").CopyToDataTable

                Dim saveFileDialog1 As New FolderBrowserDialog()
                If saveFileDialog1.ShowDialog() = DialogResult.OK Then

                    _Path = saveFileDialog1.SelectedPath.ToString
                    ExportPackingPlan(_oDT, _Path)
                End If


            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            Dim _oDT As System.Data.DataTable : Dim _Path As String = ""
            Dim _CustCode As String = "" : Dim _SuplCode As String = ""
            With DirectCast(Me.ogcPlandM.DataSource, System.Data.DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูล !!!", 1903091328, Me.Text, "", MessageBoxIcon.Information)
                    Exit Sub
                End If
                _oDT = .Select("FTSelect = '1'").CopyToDataTable

                Dim _Cmd As String = ""
                _Cmd = " Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.Tmp_ReportGrp"
                _Cmd &= vbCrLf & " where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                _Cmd &= vbCrLf & "Delete  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TmpReportExportInv_GrpPrice "
                '_Cmd &= vbCrLf & " where Expr1='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
                _Cmd &= vbCrLf & " where  FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                _Cmd &= vbCrLf & "Delete  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.Tmp_V_ReportExportInv "
                _Cmd &= vbCrLf & " where FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                _Cmd = ""

                Dim _Row As Integer = 0
                For Each R As DataRow In _oDT.Select("FTSelect = '1'", "FTInvoiceNoPre asc ")
                    _CustCode = R!FTCustCode.ToString : _SuplCode = R!FTSuplCode.ToString : _Row += +1
                    _Cmd &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.Tmp_ReportGrp"
                    _Cmd &= vbCrLf & "(FTUserLogIn,FTInvoiceNo)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTInvoiceNoPre.ToString) & "'"
                    'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                    _Cmd &= vbCrLf & "                      SELECT V_ReportExportInv.FTNikePOLineItem, V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS, CONVERT(varchar(20), CONVERT(numeric(18, 2), "
                    _Cmd &= vbCrLf & "                                         V_ReportExportInv.FNUnitPrice)) AS FNUnitPrice, V_ReportExportInv.FTPORef, V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo,   "
                    _Cmd &= vbCrLf & "                                       V_ReportExportInv.FTRangeNo, V_ReportExportInv_Grp.FTInvoiceGrpNo AS Expr1 "
                    _Cmd &= vbCrLf & "    INTO #Tmp" & HI.UL.ULF.rpQuoted(_Row) & ""
                    _Cmd &= vbCrLf & "                                FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..V_ReportExportInv_Assort AS V_ReportExportInv INNER  JOIN "
                    _Cmd &= vbCrLf & "                                       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. V_ReportExportInv_Grp AS V_ReportExportInv_Grp ON V_ReportExportInv.FTInvoiceGrpNo = V_ReportExportInv_Grp.FTInvoiceBookNo "
                    '_Cmd &= vbCrLf & "                                       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..V_TMERMMainMatSpec AS V_TMERMMainMatSpec ON V_ReportExportInv.FNHSysStyleId = V_TMERMMainMatSpec.FNHSysStyleId   "
                    '_Cmd &= vbCrLf & "                                                               AND   V_ReportExportInv.FNHSysSeasonId = V_TMERMMainMatSpec.FNHSysSeasonId"
                    _Cmd &= vbCrLf & " where V_ReportExportInv_Grp.FTInvoiceGrpNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNoPre.ToString) & "' "
                    _Cmd &= vbCrLf & "                 GROUP BY V_ReportExportInv.FTNikePOLineItem, V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS, V_ReportExportInv.FNUnitPrice, V_ReportExportInv.FTPORef, "
                    _Cmd &= vbCrLf & "                                    V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo,   V_ReportExportInv.FTRangeNo, V_ReportExportInv_Grp.FTInvoiceGrpNo "
                    _Cmd &= vbCrLf & "    "

                    _Cmd &= vbCrLf & " insert into     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TmpReportExportInv_GrpPrice "
                    _Cmd &= vbCrLf & " (FTUserLogin, FTNikePOLineItem, FNTNW, FNTGW, FNCTNS, FNUnitPrice, FTPORef, FTInvoiceGrpNo, FTInvoiceBookNo, FTMainMatSpecCode, FTRangeNo, Expr1, FNHSysStyleId, FNUnitPriceMuti)"
                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', V_ReportExportInv.FTNikePOLineItem,  V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS,  isnull(V_ReportExportInv.FNUnitPrice,0) as FNUnitPrice,   "
                    _Cmd &= vbCrLf & "       V_ReportExportInv.FTPORef, V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo, V_TMERMMainMatSpec.FTMainMatSpecCode, V_ReportExportInv.FTRangeNo, V_ReportExportInv_Grp.FTInvoiceGrpNo AS Expr1, V_ReportExportInv.FNHSysStyleId,"
                    _Cmd &= vbCrLf & "    (SELECT TOP 1 STUFF "
                    _Cmd &= vbCrLf & "     ((SELECT  distinct ' / ' + t2.FNUnitPrice   "
                    _Cmd &= vbCrLf & "               FROM       #Tmp" & HI.UL.ULF.rpQuoted(_Row) & "  as  t2 "
                    _Cmd &= vbCrLf & "     "
                    _Cmd &= vbCrLf & "     WHERE   t2.FTPORef = V_ReportExportInv.FTPORef AND t2.FTInvoiceGrpNo = V_ReportExportInv.FTInvoiceGrpNo AND t2.FTRangeNo = V_ReportExportInv.FTRangeNo  " 'AND   t2.FTNikePOLineItem = V_ReportExportInv.FTNikePOLineItem 
                    _Cmd &= vbCrLf & "                     FOR XML PATH('')), 1, 2, '') AS FTSubOrderNo) AS FNUnitPriceMuti "
                    _Cmd &= vbCrLf & "    FROM     V_ReportExportInv_Assort AS V_ReportExportInv INNER JOIN "
                    _Cmd &= vbCrLf & "     V_ReportExportInv_Grp AS V_ReportExportInv_Grp ON V_ReportExportInv.FTInvoiceGrpNo = V_ReportExportInv_Grp.FTInvoiceBookNo INNER JOIN "
                    _Cmd &= vbCrLf & "       V_TMERMMainMatSpec AS V_TMERMMainMatSpec ON V_ReportExportInv.FNHSysStyleId = V_TMERMMainMatSpec.FNHSysStyleId  "
                    _Cmd &= vbCrLf & " where V_ReportExportInv_Grp.FTInvoiceGrpNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNoPre.ToString) & "' "
                    _Cmd &= vbCrLf & " GROUP BY V_ReportExportInv.FTNikePOLineItem, V_ReportExportInv_Grp.FTInvoiceBookNo, V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS, V_ReportExportInv.FNUnitPrice, V_ReportExportInv.FTPORef,  "
                    _Cmd &= vbCrLf & "   V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo, V_TMERMMainMatSpec.FTMainMatSpecCode, V_ReportExportInv.FTRangeNo, V_ReportExportInv.FNHSysStyleId, V_ReportExportInv_Grp.FTInvoiceGrpNo"
                    'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                    _Cmd &= vbCrLf & " insert into     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.Tmp_V_ReportExportInv "
                    _Cmd &= vbCrLf & " ( FTUserLogIn  ,  FTCmpCode, FTCmpNameTH, FTCmpNameEN, FTAddressInvoiceTH, FTAddressInvoiceEN, FTExpSoldAddrEN, FTRemark, FTInvoiceNo, FTInvoiceBy, FDInvoiceDate, FTShipModeCode, FTShipModenNameTH,  "
                    _Cmd &= vbCrLf & " FTShipModeNameEN, FTShipPortCode, FTShipPortNameTH, Expr1, FTProvinceCode, FTProvinceNameTH, FTProvinceNameEN, FTPORef, FTStyleCode, FNQuantity, FNUnitPrice, FNTotalAmount, FTTotalAmountTHB, FTTotalAmountENB, "
                    _Cmd &= vbCrLf & " FTNikePOLineItem, FTColorway, FTSizeBreakDown, FTVesserName, FNHSysTermOfPMId, FNHSysCrTermId, FNExchangeRate, FNCreditDay, FNCTNS, FNTNW, FNTGW, FNHSysCartonId, FNHSysSeasonId, FNHSysCmpId, "
                    _Cmd &= vbCrLf & " FTStateSendApp, FTSandApproveBy, FDSendAapproveDate, FTSendApproveTime, FTStateApprove, FTApproveBy, FDApproveDate, FTApproveTime, FTCustNameEN, FTCustNameTH, FTCustCode, FTExpShipNameEN, FTExpShipCode,  "
                    _Cmd &= vbCrLf & "  FDESTTimeDept, FDESTTimeArrl, FTInvoiceBookNo, FDShipDate, FNHSysCurId, FTCurDescEN, FNHSysStyleId, FTRangeNo, FTLineNo, FTDiamondMarkCode, FTInvoiceGrp, FTPORefNo) "
                    _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  , FTCmpCode, FTCmpNameTH, FTCmpNameEN, FTAddressInvoiceTH, FTAddressInvoiceEN, FTExpSoldAddrEN, FTRemark, FTInvoiceNo, FTInvoiceBy, FDInvoiceDate, FTShipModeCode, FTShipModenNameTH,   "
                    _Cmd &= vbCrLf & "FTShipModeNameEN, FTShipPortCode, FTShipPortNameTH, Expr1, FTProvinceCode, FTProvinceNameTH, FTProvinceNameEN, FTPORef, FTStyleCode, FNQuantity, FNUnitPrice, FNTotalAmount, FTTotalAmountTHB, FTTotalAmountENB,  "
                    _Cmd &= vbCrLf & " FTNikePOLineItem, FTColorway, FTSizeBreakDown, FTVesserName, FNHSysTermOfPMId, FNHSysCrTermId, FNExchangeRate, FNCreditDay, FNCTNS, FNTNW, FNTGW, FNHSysCartonId, FNHSysSeasonId, FNHSysCmpId, "
                    _Cmd &= vbCrLf & " FTStateSendApp, FTSandApproveBy, FDSendAapproveDate, FTSendApproveTime, FTStateApprove, FTApproveBy, FDApproveDate, FTApproveTime, FTCustNameEN, FTCustNameTH, FTCustCode, FTExpShipNameEN, FTExpShipCode, "
                    _Cmd &= vbCrLf & " FDESTTimeDept, FDESTTimeArrl, FTInvoiceBookNo, FDShipDate, FNHSysCurId, FTCurDescEN, FNHSysStyleId, FTRangeNo, FTLineNo, FTDiamondMarkCode, FTInvoiceGrp, FTPORefNo"
                    _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_ReportExportInv "
                    _Cmd &= vbCrLf & " WHERE  (FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(R!FTInvoiceNoPre.ToString) & "')"




                Next

                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)



            End With

            Dim _Fm As String = ""

            _Fm = "  {Tmp_ReportGrp.FTUserLogIn} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Account\"
                Select Case _CustCode
                    Case "NI"
                        If _SuplCode.Trim.ToString = "DAMCO" Then
                            .ReportName = "RptExportInvoice_Nike_Cover.rpt"
                        Else
                            .ReportName = "RptExportInvoice_Nike_APL_Cover.rpt"
                        End If
                    Case "AE"
                        .ReportName = "RptExportInvoice_AEON.rpt"
                    Case Else
                        .ReportName = "RptExportInvoice_Nike.rpt"
                End Select
                .Formular = _Fm
                .Preview()
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class




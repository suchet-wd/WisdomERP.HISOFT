﻿Imports System.Windows.Forms

Public Class wTransferOrder
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AutoPo As wReserveAutoPO
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _AutoTransferToWH As wTransferOrderAutoTransferToWH
    Private _AddItemPopup As wTransferOrderAddBarcode

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _FormLoad = True
        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()
        Dim oSysLang As New ST.SysLanguage

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With

        _AddItemPopup = New wTransferOrderAddBarcode

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        _AutoPo = New wReserveAutoPO
        HI.TL.HandlerControl.AddHandlerObj(_AutoPo)

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoPo.Name.ToString.Trim, _AutoPo)
        Catch ex As Exception
        Finally
        End Try


        _AutoTransferToWH = New wTransferOrderAutoTransferToWH
        HI.TL.HandlerControl.AddHandlerObj(_AutoTransferToWH)

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoTransferToWH.Name.ToString.Trim, _AutoTransferToWH)
        Catch ex As Exception
        Finally
        End Try

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

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _mPriceClosed1 As Double = -1
    Public Property PriceClosed1 As Double
        Get
            Return _mPriceClosed1
        End Get
        Set(value As Double)
            _mPriceClosed1 = value
        End Set
    End Property


    Private _mPriceClosed2 As Double = -1
    Public Property PriceClosed2 As Double
        Get
            Return _mPriceClosed2
        End Get
        Set(value As Double)
            _mPriceClosed2 = value
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
        _ProcLoad = True
        'HI.TL.HandlerControl.ClearControl(ogbh)
        'HI.TL.HandlerControl.ClearControl(ogbpayment)
        'HI.TL.HandlerControl.ClearControl(ogbsuplcfm)
        'HI.TL.HandlerControl.ClearControl(ogbpoamt)
        'HI.TL.HandlerControl.ClearControl(ogbnote)
        'HI.TL.HandlerControl.ClearControl(ogbdocdetail)

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

        Call LoadDucumentDetail(Key.ToString)

        Dim cmdstring As String = ""
        cmdstring = "select top 1 FTFacPurchaseNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase AS X WITH(NOLOCK) where FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
        FTFacPurchaseNo.Text = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")

        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail(Key As String)

        ogcdetail.DataSource = HI.INVEN.Barcode.LoadDocumentBarcode(Key, Barcode.DocType.TransferOrder)

    End Sub

    Public Sub DefaultsData()
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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder WHERE FTTransferOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferOrderNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder WHERE FTTransferOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferOrderNo.Text) & "'")
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
    End Sub

#End Region

#Region "MAIN PROC"


    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTTransferOrderBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTTransferOrderBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If
        End If
    End Function

    Private Function ChkCloseStock() As Boolean
        Dim _Qry As String = ""
        Dim _Pass As Boolean = False
        Dim _dt As DataTable
        _Qry = "  SELECT A.FTTransferOrderNo, B.FNHSysWHId"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK) ON A.FTTransferOrderNo = B.FTDocumentNo"
        _Qry &= vbCrLf & " WHERE A.FTTransferOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferOrderNo.Text) & "' "
        _Qry &= vbCrLf & "  GROUP BY A.FTTransferOrderNo, B.FNHSysWHId"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        For Each R As DataRow In _dt.Rows
            If StockValidate.CheckCloseStock(Integer.Parse(Val(R!FNHSysWHId.ToString)), Me.FDTransferOrderDate.Text) = True Then
                Return True
            End If
        Next

        Return _Pass
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub
        If ChkCloseStock() = True Then
            Exit Sub
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
        If ChkCloseStock() = True Then
            Exit Sub
        End If
        If Barcode.CheckDocumentRefOut(Me.FTTransferOrderNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสาร มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1312220001, Me.Text)
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTTransferOrderNo.Text, Me.Text) = False Then
            Exit Sub
        End If

        If Me.DeleteData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.DefaultsData()

        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTTransferOrderNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "TransferOrderSlip.rpt"
                .Formular = "{TINVENTransferOrder.FTTransferOrderNo}='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "TransferOrderSlip_Barcode.rpt"
                .Formular = "{TINVENTransferOrder.FTTransferOrderNo}='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTTransferOrderNo_lbl.Text)
            FTTransferOrderNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Function SaveBarcode(Optional BarOdeData As String = "") As Boolean
        Dim _Str As String
        Dim _BarCode As String = ""

        If BarOdeData = "" Then
            _BarCode = FTBarcodeNo.Text
        Else
            _BarCode = BarOdeData
        End If

        Dim _StateNew As Boolean
        Try

            _Str = " SELECT TOP 1 FTBarcodeNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK) WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "' "
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StateNew Then
                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FNPriceTrans)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' "
                _Str &= vbCrLf & "," & Val("" & Me.WH) & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
                _Str &= vbCrLf & "," & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'," & Val(HI.ST.SysInfo.CmpID) & "  "
                _Str &= vbCrLf & ",CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "
            Else
                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & Me.WH) & " "
                _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
                _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",FTStateReserve='' "
                _Str &= vbCrLf & ",FNPriceTrans=CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "
                _Str &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' "
                _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & "  AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "' "
            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FNPriceTrans)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' "
                _Str &= vbCrLf & "," & Val("" & Me.WH) & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' "
                _Str &= vbCrLf & "," & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'," & Val(HI.ST.SysInfo.CmpID) & "  "
                _Str &= vbCrLf & ",CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "
            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & Me.WH) & " "
                _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' "
                _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",FTStateReserve='' "
                _Str &= vbCrLf & ",FNPriceTrans=CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "
                _Str &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' "
                _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & "  AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "' "

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

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

    Private Function DeleteBarcode(BarcodeKey As String) As Boolean
        Dim _Str As String
        Dim _BarCode As String = BarcodeKey

        Try


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' ")

            Return True
        Catch ex As Exception


            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

#End Region

#Region " Variable "

#End Region

    Private Sub FNIssueBarType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNIssueBarType.SelectedIndexChanged
        FNQuantityBal.Properties.ReadOnly = (FNIssueBarType.SelectedIndex <> 1)

        If FTTransferOrderNo.Text <> "" Then
            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()
        End If
    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        If ocmsave.Enabled = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    If FTTransferOrderNo.Properties.Tag.ToString = "" Then
                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If Me.FTTransferOrderNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTTransferOrderNo.Text)
                    End If

                    Call AddBarCode(FTBarcodeNo.Text, (FNQuantityBal.Properties.ReadOnly))

                End If

        End Select
    End Sub


    Private Sub AddBarCode(BarcodeNo As String, StateAdd As Boolean, Optional Qty As Double = 0, Optional MsgShow As Boolean = True)

        If CheckOwner() = False Then Exit Sub

        FNQuantityBal.Value = 0

        If Barcode.CheckTransactionOUT(BarcodeNo, FTTransferOrderNo.Text, 0, "") Then

            If MsgShow Then
                HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1311240006, Me.Text)
            End If
            Exit Sub
        End If

        Dim _RawmatId As Integer = 0
        _RawmatId = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A WITH(NOLOCK) WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeNo) & "'", Conn.DB.DataBaseName.DB_INVEN, "0")))
        If HI.INVEN.StockValidate.OrderUsedRawmat(Me.FTOrderNoTo.Text, _RawmatId) = False Then
            Exit Sub
        End If

        Dim _Dt As DataTable = Barcode.BarCodeBalance(BarcodeNo, 0.ToString, "", Me.FTTransferOrderNo.Text, True)
        If _Dt.Rows.Count > 0 Then
            If _Dt.Select("FNQuantityBal >=" & Qty & "").Length > 0 Then

                If _Dt.Select("FNQuantityBal >=" & Qty & " AND FNQuantityBal >0 AND ( FNHSysCmpId=" & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " OR FNHSysCmpManageId=" & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " )").Length <= 0 Then

                    If MsgShow Then
                        HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ Company นี้  !!!", 1312788101, Me.Text)
                    End If

                    Exit Sub
                End If

                If _Dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNQuantityBal >=" & Qty & " AND FNQuantityBal >0  AND ( FNHSysCmpId=" & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " OR FNHSysCmpManageId=" & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " )").Length > 0 Then

                    Me.WH = 0
                    Me.OrderNo = ""
                    Me.WHTo = 0
                    Me.OrderNoTo = ""
                    Me.DocRefNo = ""

                    For Each R As DataRow In _Dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNQuantityBal >=" & Qty & " AND FNQuantityBal >0 AND  ( FNHSysCmpId=" & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " OR FNHSysCmpManageId=" & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " )")

                        If Qty <> 0 Then
                            FNQuantityBal.Value = Qty
                        Else
                            FNQuantityBal.Value = R!FNQuantityBal
                        End If

                        Me.FNPriceTrans = Val(R!FNPriceTrans)
                        Me.WH = Val(R!FNHSysWHId.ToString)
                        Me.OrderNo = R!FTOrderNo.ToString
                        Me.DocRefNo = R!FTDocumentNo.ToString

                        Exit For

                    Next

                    If (StateAdd) And Me.WH <> 0 And Me.OrderNo <> "" Then

                        If StockValidate.CheckCloseStock(Integer.Parse(Val(Me.WH)), Me.FDTransferOrderDate.Text) = True Then
                            Exit Sub
                        End If

                        If SaveBarcode(BarcodeNo) Then
                            FTBarcodeNo.Focus()
                            FTBarcodeNo.SelectAll()
                            FNQuantityBal.Value = 0

                            If MsgShow Then

                                LoadDucumentDetail(Me.FTTransferOrderNo.Text)

                            End If

                        End If

                    Else

                        If FNQuantityBal.Properties.ReadOnly = False Then

                            FNQuantityBal.Focus()
                            FNQuantityBal.SelectAll()

                        End If

                    End If

                    Me.WH = 0
                    Me.OrderNo = ""
                    Me.WHTo = 0
                    Me.OrderNoTo = ""
                    Me.DocRefNo = ""
                Else
                    HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ Order นี้  !!!", 1311240009, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("จำนวน Balance ไม่พอ !!!", 1311240010, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData("ไม่พบข้อมูลหมายเลข Barcode !!!", 1311240007, Me.Text)
        End If
        _Dt.Dispose()
    End Sub

    Private Sub FNQuantityBal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNQuantityBal.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    If FTTransferOrderNo.Properties.Tag.ToString = "" Then
                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If Me.FTTransferOrderNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTTransferOrderNo.Text)
                    End If

                    Call AddBarCode(FTBarcodeNo.Text, True, FNQuantityBal.Value)


                Else
                    FTBarcodeNo.Focus()
                    FTBarcodeNo.SelectAll()
                End If

        End Select
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown
        If ocmdeletebarcode.Enabled = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Delete
                'With ogvdetail
                '    If .RowCount <= 0 Then Exit Sub
                '    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                '    Dim _Barcode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo").ToString

                '    If Barcode.CheckTransactionOUT(_Barcode, FTTransferOrderNo.Text, 0, "") Then
                '        HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1311240006, Me.Text)
                '        Exit Sub
                '    End If

                '    If _Barcode <> "" Then
                '        If DeleteBarcode(_Barcode) Then
                '            FTBarcodeNo.Focus()
                '            FTBarcodeNo.SelectAll()
                '            FNQuantityBal.Value = 0

                '            LoadDucumentDetail(Me.FTTransferOrderNo.Text)
                '        End If
                '    End If

                'End With
                Call DeleteBarcode()
        End Select
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged
 

        Try
            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FTOrderNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTOrderNo.Properties.Buttons.Item(0).Enabled = Not ((dt.Rows.Count > 0))

            FTOrderNoTo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTOrderNoTo.Properties.Buttons.Item(0).Enabled = Not ((dt.Rows.Count > 0))

            dt.Dispose()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcode.Click
        Call DeleteBarcode()
    End Sub

    Private Sub DeleteBarcode()
        If CheckOwner() = False Then Exit Sub
        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString

                If Barcode.CheckTransactionOUT(_Barcode, FTTransferOrderNo.Text, 0, "") Then
                    HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1311240006, Me.Text, _Barcode)
                Else
                    If _Barcode <> "" Then

                        If StockValidate.CheckCloseStock(Integer.Parse(Val("" & .GetRowCellValue(i, "FNHSysWHId").ToString)), Me.FDTransferOrderDate.Text) = True Then
                        Else
                            If DeleteBarcode(_Barcode) Then
                                _StateDelete = True

                            End If
                        End If

                       
                    End If

                End If


            Next

            If (_StateDelete) Then

                FTBarcodeNo.Focus()
                FTBarcodeNo.SelectAll()
                FNQuantityBal.Value = 0

                LoadDucumentDetail(Me.FTTransferOrderNo.Text)

            End If

        End With
    End Sub

    Private Sub FNQuantityBal_EditValueChanged(sender As Object, e As EventArgs) Handles FNQuantityBal.EditValueChanged

    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub wTransferOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString

        _FormLoad = False


        Dim Indx As Integer = 0
        Try
            Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDoc" & Me.Name))
        Catch ex As Exception
        End Try


        FNListDocumentData.SelectedIndex = Indx
    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub ocmautotrwWH_Click(sender As Object, e As EventArgs) Handles ocmautotrwWH.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTTransferOrderNo.Text <> "" Then

            Dim dtauto As DataTable
            Dim _Qry As String

            '_Qry = " Select '1' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
            '_Qry &= vbCrLf & " ,M.FTBarcodeNo "
            '_Qry &= vbCrLf & " ,WS.FTWHCode"
            '_Qry &= vbCrLf & ",M.FTOrderNo"
            '_Qry &= vbCrLf & " ,M.FNQuantity"

            '_Qry &= vbCrLf & "  , IM.FTRawMatCode"

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
            'Else
            '    _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
            'End If

            '_Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
            '_Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
            '_Qry &= vbCrLf & ",M.FTFabricFrontSize"
            '_Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade,M.FNPriceTrans"
            '_Qry &= vbCrLf & "  FROM"
            '_Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
            '_Qry &= vbCrLf & "   ,FNHSysWHId"
            '_Qry &= vbCrLf & "   ,FTOrderNo"
            '_Qry &= vbCrLf & "  ,FNQuantity"
            '_Qry &= vbCrLf & "  ,ISNULL(("
            '_Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            '_Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
            '_Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo"
            '_Qry &= vbCrLf & "  AND FTDocumentRefNo = A.FTDocumentNo"
            '_Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"
            '_Qry &= vbCrLf & "   ,FTPurchaseNo"
            '_Qry &= vbCrLf & "   ,FTDocumentNo"
            '_Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade,FNPriceTrans"
            '_Qry &= vbCrLf & "   FROM"
            '_Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,BI.FNHSysWHId,BI.FTOrderNo,SUM(BI.FNQuantity) AS FNQuantity"
            '_Qry &= vbCrLf & "  ,B.FTPurchaseNo,BI.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,ISNULL(BI.FNPriceTrans,-1) AS FNPriceTrans "
            '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
            '_Qry &= vbCrLf & "   ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH(NOLOCK)"
            '_Qry &= vbCrLf & "   WHERE B.FTBarcodeNo = BI.FTBarcodeNo"
            '_Qry &= vbCrLf & "  AND BI.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferOrderNo.Text) & "'"
            '_Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,BI.FNHSysWHId,BI.FTOrderNo,B.FTPurchaseNo,BI.FTDocumentNo "
            '_Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,ISNULL(BI.FNPriceTrans,-1) "
            '_Qry &= vbCrLf & "  ) AS A ) AS M"
            '_Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            '_Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            '_Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            '_Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            '_Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"
            '_Qry &= vbCrLf & " ORDER BY  WS.FTWHCode,M.FTBarcodeNo"
            'dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            dtauto = HI.INVEN.Barcode.GetListBarcodeInForCreateTransaction(Me.FTTransferOrderNo.Text)

            If dtauto.Rows.Count > 0 Then
                HI.TL.HandlerControl.ClearControl(_AutoTransferToWH)
                With _AutoTransferToWH

                    Call HI.ST.Lang.SP_SETxLanguage(_AutoTransferToWH)

                    .ReceiveNo = Me.FTTransferOrderNo.Text
                    .WHID = 0
                    .ogcbarcode.DataSource = dtauto.Copy
                    .ShowDialog()

                End With
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ โอนได้ กรุณาทำการตรวจสอบ !!!", 1410020009, Me.Text, , MessageBoxIcon.Warning)
            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTTransferOrderNo_lbl.Text)
            FTTransferOrderNo.Focus()
        End If
    End Sub

    Private Sub ocmautoissue_Click(sender As Object, e As EventArgs) Handles ocmautoissue.Click

    End Sub

    Private Sub ocmaddbarcod_Click(sender As Object, e As EventArgs) Handles ocmaddbarcod.Click
        If CheckOwner() = False Then Exit Sub

        If HI.ST.ValidateData.CloseJob(Me.FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If


        If HI.ST.ValidateData.CloseJob(Me.FTOrderNoTo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If

        If FTTransferOrderNo.Properties.Tag.ToString = "" Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTTransferOrderNo.Text = "" Then Exit Sub
            LoadDataInfo(Me.FTTransferOrderNo.Text)
        End If

        Call AddBarCodeSelect()
    End Sub

    Private Sub AddBarCodeSelect()

        Dim _Str As String = ""
        Dim BarcodeNo As String = ""
        Dim _dtBar As New DataTable
        Dim _Dt As New DataTable
        Dim Qty As Double = 0

        Dim _FNOrderType As Integer = 0

        _Str = "SELECT TOP 1 FNOrderType "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' "

        _FNOrderType = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

        _Str = " SELECT     '0' AS FTSelect,M.FTRawMatCode, MC.FTRawMatColorCode, MZ.FTRawMatSizeCode, B_1.FTBarcodeNo, B_1.FTOrderNo, B_1.FNQuantityBal,0.0000 AS FNReserveQty,B_1.FTDocumentNo ,B_1.FTPurchaseNo "
        _Str &= vbCrLf & ", PXTD.FTRawMatColorNameEN AS FTRawMatColorName,WHC.FTWHCode"
        _Str &= vbCrLf & " FROM          "

        If _FNOrderType = 0 Or _FNOrderType = 13 Then

            _Str &= vbCrLf & "   ( SELECT FTOrderNo, FNHSysRawMatId "
            _Str &= vbCrLf & " FROM"
            _Str &= vbCrLf & " (SELECT        FTOrderNo, FNHSysRawMatId"
            _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS RS WITH (NOLOCK)"
            _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' "
            _Str &= vbCrLf & "  GROUP BY FTOrderNo, FNHSysRawMatId"
            _Str &= vbCrLf & "  UNION "
            _Str &= vbCrLf & " SELECT O.FTOrderNo,B.FNHSysRawMatId  "
            _Str &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) "
            _Str &= vbCrLf & "   CROSS Join "
            _Str &= vbCrLf & "   (SELECT RM.FNHSysRawMatId"
            _Str &= vbCrLf & "    FROM "
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH (NOLOCK)  INNER JOIN"
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH (NOLOCK) ON  RM.FTRawMatCode = MM.FTMainMatCode"
            _Str &= vbCrLf & "  WHERE MM.FTStateNotCheckResuorce ='1') AS B "
            _Str &= vbCrLf & "  WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            _Str &= vbCrLf & " ) AS MM GROUP BY FTOrderNo, FNHSysRawMatId ) AS A INNER JOIN"

        Else

            _Str &= vbCrLf & "   ( SELECT FTOrderNo, FNHSysRawMatId "
            _Str &= vbCrLf & " FROM"
            _Str &= vbCrLf & " ("
            _Str &= vbCrLf & " SELECT O.FTOrderNo,B.FNHSysRawMatId  "
            _Str &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) "
            _Str &= vbCrLf & "   CROSS Join "
            _Str &= vbCrLf & "   (SELECT RM.FNHSysRawMatId"
            _Str &= vbCrLf & "    FROM "
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH (NOLOCK)  INNER JOIN"
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH (NOLOCK) ON  RM.FTRawMatCode = MM.FTMainMatCode"
            _Str &= vbCrLf & "  ) AS B "
            _Str &= vbCrLf & "  WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            _Str &= vbCrLf & " ) AS MM GROUP BY FTOrderNo, FNHSysRawMatId ) AS A INNER JOIN "

        End If

        _Str &= vbCrLf & "   (SELECT        FTBarcodeNo, FNHSysWHId, FTOrderNo, FNQuantity, FNQuantity - ISNULL"
        _Str &= vbCrLf & "   ((SELECT        SUM(FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH (NOLOCK)"
        _Str &= vbCrLf & "   WHERE        (FTBarcodeNo = A_1.FTBarcodeNo) AND (FNHSysWHId = A_1.FNHSysWHId) AND (FTOrderNo = A_1.FTOrderNo) AND "
        _Str &= vbCrLf & "    (FTDocumentRefNo = A_1.FTDocumentNo) AND (FTDocumentNo <> '')), 0)  "


        _Str &= vbCrLf & " - ISNULL"
        _Str &= vbCrLf & "   ((SELECT        SUM(FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode WITH (NOLOCK)"
        _Str &= vbCrLf & "   WHERE        (FTBarcodeNo = A_1.FTBarcodeNo) And (FNHSysWHId = A_1.FNHSysWHId) And (FTOrderNo = A_1.FTOrderNo) And "
        _Str &= vbCrLf & "    (FTDocumentRefNo = A_1.FTDocumentNo) And (FTDocumentNo <> '' ) AND ISNULL(FTIssueReferNo,'') ='' ), 0) AS FNQuantityBal "


        _Str &= vbCrLf & " ,FTPurchaseNo, FTDocumentNo, FNHSysRawMatId"
        _Str &= vbCrLf & " FROM            (SELECT        BI.FTBarcodeNo, BI.FNHSysWHId, BI.FTOrderNo, SUM(BI.FNQuantity) AS FNQuantity, B.FTPurchaseNo, BI.FTDocumentNo, "
        _Str &= vbCrLf & " BI.FNHSysCmpId, B.FNHSysRawMatId"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON BI.FTBarcodeNo = B.FTBarcodeNo "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK) ON BI.FNHSysWHId = W.FNHSysWHId "
        _Str &= vbCrLf & "  WHERE BI.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
        _Str &= vbCrLf & "  AND (BI.FTOrderNo <>'" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "')  AND W.FNHSysCmpId=" & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
        _Str &= vbCrLf & "  GROUP BY BI.FTBarcodeNo, BI.FNHSysWHId, BI.FTOrderNo, B.FTPurchaseNo, BI.FTDocumentNo, BI.FNHSysCmpId, B.FNHSysRawMatId) AS A_1) "
        _Str &= vbCrLf & "  AS B_1 "
        _Str &= vbCrLf & "     ON A.FNHSysRawMatId = B_1.FNHSysRawMatId  "
        _Str &= vbCrLf & "  INNER JOIN "
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON B_1.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON M.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MZ WITH (NOLOCK) ON M.FNHSysRawMatSizeId = MZ.FNHSysRawMatSizeId"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BTX WITH(NOLOCK) ON B_1.FTBarcodeNo=BTX.FTBarcodeNo "
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PXTD ON BTX.FNHSysRawMatId = PXTD.FNHSysRawMatId AND BTX.FTOrderNo = PXTD.FTOrderNo AND BTX.FTPurchaseNo = PXTD.FTPurchaseNo"

        _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WHC WITH (NOLOCK) ON B_1.FNHSysWHId = WHC.FNHSysWHId"
        _Str &= vbCrLf & " WHERE B_1.FNQuantityBal > 0 "

        _dtBar = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        With _AddItemPopup
            Call HI.ST.Lang.SP_SETxLanguage(_AddItemPopup)
            .ogcbarcode.DataSource = _dtBar
            .ogvbarcode.ActiveFilter.Clear()
            .ShowDialog()
            If .ProcessProc Then

                Dim _DocumentRefNo As String = ""
                If _dtBar.Select(" FTSelect='1' AND FNReserveQty > 0 ").Length > 0 Then

                    For Each R2 As DataRow In _dtBar.Select(" FTSelect='1' AND FNReserveQty > 0 ")

                        BarcodeNo = R2!FTBarcodeNo.ToString
                        Qty = Val(R2!FNReserveQty.ToString)

                        Call AddBarCode(BarcodeNo, True, Qty, False)

                    Next

                    LoadDucumentDetail(Me.FTTransferOrderNo.Text)

                End If

            End If

        End With

        _Dt.Dispose()
        _dtBar.Dispose()

    End Sub

    Private Sub ocmgenpo_Click(sender As Object, e As EventArgs) Handles ocmgenpo.Click
        'Try

        '    If Not (Me.ogcdetail.DataSource Is Nothing) Then
        '        With CType(Me.ogcdetail.DataSource, DataTable)
        '            .AcceptChanges()

        '            If .Rows.Count <= 0 Then
        '                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการที่ต้องการเปิด PO ซื้อของระหว่างโรงงาน กรุณาทำการตรวจสอบ !!!", 1806080457, Me.Text,, MessageBoxIcon.Warning)
        '                Exit Sub
        '            End If

        '        End With

        '        Dim cmd As String = ""
        '        Dim SysCmpId As Integer = 0
        '        Dim SysWHCmpId As Integer = 0

        '        cmd = "select top 1 C.FNHSysCmpIdTo  "
        '        cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X with(nolock) "
        '        cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON X.FNHSysCmpId=C.FNHSysCmpId  "

        '        cmd &= vbCrLf & " where X.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
        '        SysWHCmpId = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MERCHAN, "0"))

        '        cmd = "select top 1 C.FNHSysCmpIdTo  "
        '        cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X with(nolock) "
        '        cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON X.FNHSysCmpId=C.FNHSysCmpId  "

        '        cmd &= vbCrLf & " where X.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'"
        '        SysCmpId = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

        '        If SysCmpId > 0 And SysWHCmpId > 0 Then
        '            If SysCmpId = SysWHCmpId Then
        '                HI.MG.ShowMsg.mInfo("ใบสั่งผลิตนี้ ผลิตที่ สาขาเจ้าของคลังนี้อยู่แล้ว ไม่สามารถทำการเปิด PO ได้ !!!", 1806080458, Me.Text,, MessageBoxIcon.Warning)
        '                Exit Sub
        '            Else


        '                cmd = " SELECT TOP 1 FTFacPurchaseNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "'"
        '                Dim pokey As String = ""
        '                pokey = HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PUR, "")

        '                If pokey <> "" Then
        '                    HI.MG.ShowMsg.mInfo("พบข้อมูลการเปิด PO แล้ว !!!  ( " & pokey & " ) ", 1806089458, Me.Text,, MessageBoxIcon.Warning)
        '                    Exit Sub
        '                End If

        '                Dim _CmpRunText As String = "" ' Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 13), 2)
        '                Dim _POGrp As String = "" 'Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 9), 2)
        '                ' Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 7), 1
        '                Dim PurNo As String = ""
        '                Dim _CmpRunID As Integer = 0
        '                Dim _POGrpID As Integer = 0


        '                With _AutoPo
        '                    .StateProc = False
        '                    .FNHSysCmpRunId.Text = ""
        '                    .FNHSysPurGrpId.Text = ""
        '                    .ShowDialog()

        '                    If .StateProc Then

        '                        _CmpRunText = .FNHSysCmpRunId.Text
        '                        _POGrp = .FNHSysPurGrpId.Text
        '                        _CmpRunID = Val(.FNHSysCmpRunId.Properties.Tag.ToString)
        '                        _POGrpID = Val(.FNHSysPurGrpId.Properties.Tag.ToString)

        '                        PurNo = AutoPurchase(SysCmpId, SysWHCmpId, _CmpRunText, _POGrp, _CmpRunID, _POGrpID)

        '                    End If

        '                End With

        '                If PurNo <> "" Then
        '                    FTFacPurchaseNo.Text = PurNo
        '                    HI.MG.ShowMsg.mInfo("Generate PO Complete !!!  ", 1806449458, Me.Text, PurNo, MessageBoxIcon.Warning)
        '                End If

        '            End If

        '        End If


        '    End If


        'Catch ex As Exception

        'End Try

    End Sub

    Private Function AutoPurchase(SysCmpId As Integer, SysWHCmpId As Integer, _CmpRunText As String, _POGrp As String, _CmpRunid As Integer, _POGrpId As Integer) As String

        Dim cmdstring As String = ""
        Dim pofacno As String = ""

        Try
            Dim vatper As Double = 0

            Dim poamt As Double = 0
            Dim podisamt As Double = 0
            Dim ponetamt As Double = 0
            Dim povatamt As Double = 0
            Dim pograndamt As Double = 0
            Dim poamtth As String
            Dim poamten As String
            Dim Surcharge As Double = 0
            Dim FNHSysDeliveryId As Integer = 0
            Dim SysCurId As Integer = 1310200002
            Dim ExcRate As Double = 1
            Dim POFacExcRate As Double = 1
            Dim _POType As String = ""
            Dim FNPoState As Integer = 0


            If ((SysCmpId = 1311090006 Or SysCmpId = 1311090005 Or SysCmpId = 1410220001 Or SysCmpId = 1501190001) And (SysWHCmpId <> 1311090006 And SysWHCmpId <> 1311090005 And SysWHCmpId <> 1410220001 And SysWHCmpId <> 1501190001)) Or
                ((SysWHCmpId = 1311090006 Or SysWHCmpId = 1311090005 Or SysWHCmpId = 1410220001 Or SysWHCmpId = 1501190001) And (SysCmpId <> 1311090006 And SysCmpId <> 1311090005 And SysCmpId <> 1410220001 And SysCmpId <> 1501190001)) Then

                vatper = 0

                SysCurId = 1310190001

                Dim _Qry As String = ""

                _Qry = " SELECT TOP 1 FNBuyingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "   WHERE  (FDDate ='" & HI.UL.ULDate.ConvertEnDB(FDTransferOrderDate.Text) & "')"
                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTCurCode='USD'"
                _Qry &= vbCrLf & "  ))"

                ExcRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "1")))

                _POType = "I"
                FNPoState = 1

            Else
                vatper = 7
                SysCurId = 1310200002

                ExcRate = 1
                _POType = "D"

                FNPoState = 0

            End If

            Dim pokey As String = ""

            cmdstring = "select  TOP 1 FTPurchaseNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS X WITH(NOLOCK)  "
            pokey = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")

            If pokey = "" Then
                Return ""
            End If

            Dim DeliveryId As Integer = 0
            cmdstring = "select  TOP 1 FNHSysDeliveryId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery AS X WITH(NOLOCK)  WHERE  FNHSysCmpId =" & Val(SysCmpId) & " AND ISNULL(FNHSysCmpIdTo,0) = 0"
            DeliveryId = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "0"))


            Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
            Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
            Dim _CmpHCreate As String = HI.Conn.SQLConn.GetField("Select TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp With(NOLOCK) WHERE FNHSysCmpId=" & Val(SysCmpId) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            pofacno = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTFacPurchase", "", False, _CmpHCreate & "F" & _CmpRunText & _Year & _POGrp & _POType & _Month).ToString

            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
            cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
            cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
            cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
            cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
            cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
            cmdstring &= vbCrLf & ")"
            cmdstring &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' FTInsUser"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " AS FDInsDate "
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime "
            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo "
            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " AS  FDPurchaseDate"
            cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTPurchaseBy"
            cmdstring &= vbCrLf & " ,'AUTO FROM TRANSFER NO ' +  '" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' AS  FTPurchaseState,'' FTRefer," & FNPoState & " AS  FNPoState"
            cmdstring &= vbCrLf & "," & _POGrpId & " AS  FNHSysPurGrpId"
            cmdstring &= vbCrLf & " ," & _CmpRunid & " AS  FNHSysCmpRunId," & SysWHCmpId & " AS  FNHSysCmpId," & HI.UL.ULDate.FormatDateDB & " AS  FDDeliveryDate"
            cmdstring &= vbCrLf & " , FNHSysCrTermId"
            cmdstring &= vbCrLf & "  , FNCreditDay"
            cmdstring &= vbCrLf & " , FNHSysTermOfPMId," & SysCurId & " AS FNHSysCurId," & ExcRate & " AS  FNExchangeRate, CASE WHEN " & DeliveryId & " >0 THEN " & DeliveryId & " ELSE  FNHSysDeliveryId END "
            cmdstring &= vbCrLf & " ,'' AS  FTContactPerson"
            cmdstring &= vbCrLf & " ,'' AS  FDSampleAppDate,'' AS FDSignDate "
            cmdstring &= vbCrLf & " ,'' AS FDBLDate ,'' AS FDSuplCfmDliDate,'' AS FDCfmDate,'' AS FTRemark"
            cmdstring &= vbCrLf & ",0 As FNPoAmt"
            cmdstring &= vbCrLf & ",0 AS FNDisCountPer"
            cmdstring &= vbCrLf & ",0 AS FNDisCountAmt"
            cmdstring &= vbCrLf & ",0 AS FNPONetAmt," & vatper & " FNVatPer"
            cmdstring &= vbCrLf & ",0 AS FNVatAmt"
            cmdstring &= vbCrLf & ",0 AS FNSurcharge"
            cmdstring &= vbCrLf & ",0 AS  FNPOGrandAmt"
            cmdstring &= vbCrLf & ",'' AS FTPOGrandAmtTH"
            cmdstring &= vbCrLf & ",'' AS  FTPOGrandAmtEN"
            cmdstring &= vbCrLf & ",'0' AS   FTStateSendApp,'' AS FTSendAppBy,'' AS FTSendAppDate,'' AS FTSendAppTime,  "
            cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "' AS FTPurchaseNoRef," & SysCmpId & " AS  FNHSysCmpIdCreate"
            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
            cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
            cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo ("
            cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo,  FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRemark, "
            cmdstring &= vbCrLf & "    FTFabricFrontSize, FNReservePOQuantity, FTRawMatColorNameTH, FTRawMatColorNameEN, FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate,FTPurchaseNo"
            cmdstring &= vbCrLf & ")"
            cmdstring &= vbCrLf & "  Select   "
            cmdstring &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & "  , " & HI.UL.ULDate.FormatDateDB & " "
            cmdstring &= vbCrLf & "  , " & HI.UL.ULDate.FormatTimeDB & " "
            cmdstring &= vbCrLf & "  ,   '" & HI.UL.ULF.rpQuoted(pofacno) & "' "

            cmdstring &= vbCrLf & "  ,  FTOrderNo "
            cmdstring &= vbCrLf & "  ,FNHSysRawMatId "
            cmdstring &= vbCrLf & "  ,FNHSysUnitId"
            cmdstring &= vbCrLf & " ,FNPrice,0 AS FNDisPer,0 AS FNDisAmt"
            cmdstring &= vbCrLf & " ,FNQuantity"
            cmdstring &= vbCrLf & "  ,CONVERT(numeric(18,2),FNPrice * FNQuantity) AS FNNetAmt,'' AS FTRemark"
            cmdstring &= vbCrLf & " ,FTFabricFrontSize,0 AS FNReservePOQuantity,'' AS  FTRawMatColorNameTH,'' AS  FTRawMatColorNameEN,0 AS FNSurchangeAmt,0 AS  FNSurchangePerUnit,CONVERT(numeric(18,2),FNPrice * FNQuantity)  AS FNGrandNetAmt,'' AS FTOGacDat"
            cmdstring &= vbCrLf & " ,FTPurchaseNo"
            cmdstring &= vbCrLf & " FROM(Select B.FTOrderNo"
            cmdstring &= vbCrLf & "  , BB.FNHSysRawMatId"
            cmdstring &= vbCrLf & "  , MAX(BB.FNHSysUnitId) As FNHSysUnitId"
            cmdstring &= vbCrLf & "  , MAX(CASE WHEN ISNULL(BB.FNPriceTrans,0) <=0 THEN BB.FNPrice ELSE ISNULL(BB.FNPriceTrans,0) END) AS FNPrice		"
            cmdstring &= vbCrLf & " , SUM(B.FNQuantity) As FNQuantity"
            cmdstring &= vbCrLf & "  , MAX(BB.FTFabricFrontSize) AS FTFabricFrontSize"
            cmdstring &= vbCrLf & "  , MAX(BB.FTPurchaseNo) As FTPurchaseNo"
            cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As BB WITH(NOLOCK) INNER Join"
            cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As B WITH(NOLOCK)  On BB.FTBarcodeNo = B.FTBarcodeNo INNER Join"
            cmdstring &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder As A WITH(NOLOCK)  On B.FTDocumentNo = A.FTTransferOrderNo"
            cmdstring &= vbCrLf & " Where (A.FTTransferOrderNo = N'" & HI.UL.ULF.rpQuoted(FTTransferOrderNo.Text) & "')"
            cmdstring &= vbCrLf & " Group By B.FTOrderNo, BB.FNHSysRawMatId"
            cmdstring &= vbCrLf & " ) As R"

            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)


            If ExcRate > 0 Then
                cmdstring = " UPDATE B SET "
                cmdstring &= vbCrLf & "B.FNPrice = Convert(numeric(18,4),(B.FNPrice )/" & ExcRate & " )"
                cmdstring &= vbCrLf & ", B.FNDisAmt = Convert(numeric(18,4),(B.FNDisAmt)/" & ExcRate & " )"
                cmdstring &= vbCrLf & " , B.FNNetAmt = Convert(numeric(18,4),(B.FNNetAmt )/" & ExcRate & " )"
                cmdstring &= vbCrLf & ", B.FNSurchangeAmt = Convert(numeric(18,4),(B.FNSurchangeAmt )/" & ExcRate & " )"
                cmdstring &= vbCrLf & ", B.FNSurchangePerUnit = Convert(numeric(18,4),(B.FNSurchangePerUnit )/" & ExcRate & " )"
                cmdstring &= vbCrLf & ", B.FNGrandNetAmt = Convert(numeric(18,2),(B.FNGrandNetAmt )/" & ExcRate & " )"
                cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A INNER Join"
                cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As B On A.FTFacPurchaseNo = B.FTFacPurchaseNo "
                cmdstring &= vbCrLf & " WHERE A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)
            End If


            podisamt = CDbl(Format(podisamt / ExcRate, "0.00")) ' Val(R!FNDisCountAmt.ToString)
            Surcharge = CDbl(Format(Surcharge / ExcRate, "0.00"))  'Val(R!FNSurcharge.ToString)

            cmdstring = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
            cmdstring &= vbCrLf & "    FROM"
            cmdstring &= vbCrLf & " ("
            cmdstring &= vbCrLf & " SELECT        FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
            cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  WITH(NOLOCK)"
            cmdstring &= vbCrLf & " WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "' "
            cmdstring &= vbCrLf & " GROUP BY FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

            poamt = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
            ponetamt = poamt - podisamt
            povatamt = CDbl(Format((ponetamt * vatper) / 100.0, "0.00"))
            pograndamt = ponetamt + povatamt + Surcharge

            poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
            poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

            cmdstring = "UPDATE A Set "
            cmdstring &= vbCrLf & "  FNPoAmt=" & poamt & ""
            cmdstring &= vbCrLf & ", FNPONetAmt=" & ponetamt & ""
            cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
            cmdstring &= vbCrLf & ", FNPOGrandAmt=" & pograndamt & ""
            cmdstring &= vbCrLf & ", FNSurcharge=" & Surcharge & ""
            cmdstring &= vbCrLf & ", FNDisCountAmt=" & podisamt & ""
            cmdstring &= vbCrLf & ", FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
            cmdstring &= vbCrLf & ", FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"
            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
            cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        Catch ex As Exception
            Return ""
        End Try


        Return pofacno

    End Function

    Private Sub FNListDocumentData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentData.SelectedIndex.ToString)
        End If
    End Sub
End Class
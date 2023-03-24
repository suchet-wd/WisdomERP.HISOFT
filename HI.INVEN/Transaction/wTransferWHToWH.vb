Imports System.Windows.Forms

Public Class wTransferWHToWH
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _SelectBarcodeFromRcv As wTransferWHToWHAddBarRcv
    Private _FormLoad As Boolean = True
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _FormLoad = True
        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        _SelectBarcodeFromRcv = New wTransferWHToWHAddBarRcv
        HI.TL.HandlerControl.AddHandlerObj(_SelectBarcodeFromRcv)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _SelectBarcodeFromRcv.Name.ToString.Trim, _SelectBarcodeFromRcv)
        Catch ex As Exception
        Finally
        End Try

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
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


        FTDocumentState.Checked = False
        FTInvoiceNo.Text = ""

        Dim dt As DataTable

        _Str = "select top 1 FTDocumentState,FTInvoiceNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS X WITH(NOLOCK) WHERE FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "'"
        dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        For Each Rx As DataRow In dt.Rows
            FTDocumentState.Checked = (Rx!FTDocumentState.ToString = "1")
            FTInvoiceNo.Text = Rx!FTInvoiceNo.ToString()
            Exit For
        Next

        Call LoadDucumentDetail(Key.ToString)

        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail(Key As String)

        ogcdetail.DataSource = HI.INVEN.Barcode.LoadDocumentBarcode(Key, Barcode.DocType.TransferWH)

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

        'Dim _FTStateCheckMatch As Integer = 0
        'Dim cmdstring As String = ""

        'cmdstring = "SELECT CASE WHEN ISNULL((SELECT TOP 1 FNHSysCmpId "
        'cmdstring &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X WITH(NOLOCK) "
        'cmdstring &= vbCrLf & "  WHERE(FNHSysWHId = " & Val(FNHSysWHId.Properties.Tag.ToString) & ") "
        'cmdstring &= vbCrLf & " ),0) = "
        'cmdstring &= vbCrLf & " ISNULL((SELECT TOP 1 FNHSysCmpId "
        'cmdstring &= vbCrLf & "        From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X WITH(NOLOCK) "
        'cmdstring &= vbCrLf & " WHERE  (FNHSysWHId = " & Val(FNHSysWHIdTo.Properties.Tag.ToString) & ") "
        'cmdstring &= vbCrLf & " ),0) THEN 1 ELSE 0 END AS FTStateCheckMatch"

        '_FTStateCheckMatch = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        'If _FTStateCheckMatch <> 1 Then
        '    If FTFacPurchaseNo.Text.Trim = "" Then

        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTFacPurchaseNo_lbl.Text)
        '        FTFacPurchaseNo.Focus()

        '        Return False

        '    End If
        'End If

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
        Dim FTDocType As String = ""

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

            If Me.FTFacPurchaseNo.Text <> "" Then
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "1", False, _CmpH).ToString()
                FTDocType = "1"
            Else
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString()
                FTDocType = ""
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

                If (_StateNew) Then

                    _Str = "UPDATE   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].[TINVENTransferWH] SET FTDocType='" & HI.UL.ULF.rpQuoted(FTDocType) & "'"
                    _Str &= vbCrLf & " WHERE FTTransferWHNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "

                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

            _Str = "UPDATE A Set FTDocumentState='0'"
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A "
            _Str &= vbCrLf & "WHERE  A.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' AND FTDocumentState='1'"
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

    Private Function DeleteData(InvoiceRemarkCancel As String) As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH WHERE FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If FTFacPurchaseNo.Text <> "" Then

                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTFacPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTFacPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            End If

            If FTInvoiceNo.Text <> "" Then

                _Str = "Update A SET FTStateCancel='1',FTCancelNote='" & HI.UL.ULF.rpQuoted(InvoiceRemarkCancel) & "'  "
                _Str &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS A "
                _Str &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"

                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                '_Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"

                'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                '_Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"

                'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH WHERE FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "'")


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
        If (HI.ST.UserInfo.UserName.ToUpper = FTTransferWHBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTTransferWHBy.Text) & "' "
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

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDTransferWHDate.Text) = True Then
            Exit Sub
        End If

        If FTInvoiceNo.Text = "" Then
            If HI.INVEN.StockValidate.CheckDocCreateInvoiceSale(Me.FTTransferWHNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการบันทึก ออก Invoice แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1011220002, Me.Text)
                Exit Sub
            End If
        End If


        Dim _Strsql As String = ""
        _Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
        _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH "
        _Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

        If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") = "1" Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
            Exit Sub
        End If

        If Me.VerrifyData Then

            Me.FTStateApprove.Checked = False
            Me.FTDocumentState.Checked = False
            If Me.SaveData() Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            Else

                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            End If

        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDTransferWHDate.Text) = True Then
            Exit Sub
        End If

        If FTInvoiceNo.Text = "" Then
            If HI.INVEN.StockValidate.CheckDocCreateInvoiceSale(Me.FTTransferWHNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการบันทึก ออก Invoice แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1011220002, Me.Text)
                Exit Sub
            End If
        End If


        Dim _Strsql As String = ""
        _Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
        _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH "
        _Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

        If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") = "1" Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTTransferWHNo.Text, Me.Text) = False Then
            Exit Sub
        End If

        Dim StateDelete As Boolean = False
        Dim InvoiceRemarkCancel As String = ""
        If FTInvoiceNo.Text <> "" Then

            With New wTransferWHToWHInvoiceRemark
                .ShowDialog()

                If .StateProc Then
                    InvoiceRemarkCancel = .FTRemark.Text.Trim()
                    StateDelete = True
                Else
                    StateDelete = False
                End If

            End With
        Else

            StateDelete = True

        End If

        If StateDelete Then

            If Me.DeleteData(InvoiceRemarkCancel) Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
                Me.DefaultsData()

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If

    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTTransferWHNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "TransferToWarehouseSlip.rpt"
                .Formular = "{TINVENTransferWH.FTTransferWHNo}='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "TransferToWarehouseSlip_Barcode.rpt"
                .Formular = "{TINVENTransferWH.FTTransferWHNo}='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTTransferWHNo_lbl.Text)
            FTTransferWHNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Function SaveBarcode(DataBarCodeNo As String, Optional BarcodePL As String = "") As Boolean
        Dim _Str As String
        Dim _BarCode As String = DataBarCodeNo
        Dim _StateNew As Boolean
        Try

            _Str = " SELECT TOP 1 FTBarcodeNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK) "
            _Str &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
            _Str &= vbCrLf & " And FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            _Str &= vbCrLf & " And FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'  "
            _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "

            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FNPriceTrans,FNPriceClose1,FNPriceClose2)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
                _Str &= vbCrLf & "," & Val("" & Me.WH) & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "
                _Str &= vbCrLf & "," & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
                _Str &= vbCrLf & ",CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "

                _Str &= vbCrLf & ",CASE WHEN " & Me.PriceClosed1 & "<0 THEN NULL ELSE " & Me.PriceClosed1 & "  END "
                _Str &= vbCrLf & ",CASE WHEN " & Me.PriceClosed2 & "<0 THEN NULL ELSE " & Me.PriceClosed2 & "  END "


            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & Me.WH) & " "
                _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "



                If BarcodePL <> "" Then
                    _Str &= vbCrLf & ",FNQuantity = FNQuantity +" & FNQuantityBal.Value & " "
                Else
                    _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
                End If

                _Str &= vbCrLf & ",FTStateReserve='' "
                _Str &= vbCrLf & ",FNPriceTrans=CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "
                _Str &= vbCrLf & ",FNPriceClose1=CASE WHEN " & Me.PriceClosed1 & "<0 THEN NULL ELSE " & Me.PriceClosed1 & "  END "
                _Str &= vbCrLf & ",FNPriceClose2=CASE WHEN " & Me.PriceClosed2 & "<0 THEN NULL ELSE " & Me.PriceClosed2 & "  END "

                _Str &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
                _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & "  AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'  "
                _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End If

            'If _StateNew Then

            '    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve)  "
            '    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            '    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            '    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            '    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            '    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
            '    _Str &= vbCrLf & "," & Val("" & Me.WHTo) & " "
            '    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "
            '    _Str &= vbCrLf & "," & FNQuantityBal.Value & " "
            '    _Str &= vbCrLf & ",'' "

            'Else

            '    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN "
            '    _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            '    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
            '    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
            '    _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & Me.WHTo) & " "
            '    _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "
            '    _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
            '    _Str &= vbCrLf & ",FTStateReserve='' "
            '    _Str &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
            '    _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            'End If

            'If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '    HI.Conn.SQLConn.Tran.Rollback()
            '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '    Return False
            'End If


            If BarcodePL <> "" Then
                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM "
                _Str &= vbCrLf & " SET FTIssUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDIssDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTIssTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",FTStateIssue='1'"
                _Str &= vbCrLf & " WHERE  FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & " AND FTPLBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodePL) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return False
                End If

            End If



            _Str = "UPDATE A Set FTDocumentState='0'"
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A "
            _Str &= vbCrLf & "WHERE  A.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' AND FTDocumentState='1'"
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

#End Region
    Private Function DeleteBarcode(BarcodeKey As String, FTOrderNoKey As String) As Boolean
        Dim _Str As String
        Dim _BarCode As String = BarcodeKey
        Dim _FTOrderNo As String = FTOrderNoKey

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If


            _Str = "UPDATE A Set FTDocumentState='0'"
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A "
            _Str &= vbCrLf & "WHERE  A.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' AND FTDocumentState='1'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' ")
            Return True
        Catch ex As Exception


            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
#Region " Variable "

#End Region

    Private Sub FNIssueBarType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNIssueBarType.SelectedIndexChanged
        FNQuantityBal.Properties.ReadOnly = (FNIssueBarType.SelectedIndex <> 1)
    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        If ocmsave.Enabled = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    Dim _Strsql As String = ""
                    _Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
                    _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH "
                    _Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

                    If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") = "1" Then
                        HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
                        Exit Sub
                    End If


                        Me.FTStateApprove.Checked = False

                        If FTTransferWHNo.Properties.Tag.ToString = "" Then
                            If Me.VerrifyData() Then
                                If Me.SaveData Then
                                Else
                                    Exit Sub
                                End If
                            Else
                                Exit Sub
                            End If
                        Else
                            If Me.FTTransferWHNo.Text = "" Then Exit Sub
                            LoadDataInfo(Me.FTTransferWHNo.Text)
                        End If

                        Call AddBarCode(FTBarcodeNo.Text, (FNQuantityBal.Properties.ReadOnly))

                    End If

        End Select
    End Sub


    Private Sub AddBarCode(BarcodeNo As String, StateAdd As Boolean, Optional Qty As Double = 0)
        If CheckOwner() = False Then Exit Sub
        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDTransferWHDate.Text) = True Then
            Exit Sub
        End If

        If FTInvoiceNo.Text = "" Then
            If HI.INVEN.StockValidate.CheckDocCreateInvoiceSale(Me.FTTransferWHNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการบันทึก ออก Invoice แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1011220002, Me.Text)
                Exit Sub
            End If
        End If


        Dim _Strsql As String = ""
        '_Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
        '_Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH WITH(NOLOCK) "
        '_Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

        'If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") = "1" Then
        '    HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
        '    Exit Sub
        'End If

        Dim _DtOrder As DataTable
        Dim _DtWarehouse As DataTable
        Dim _FNOrderType As Integer = 0
        Dim _FNHSysCmpIdTo As Integer = 0
        Dim _FNHSysCmpIdToOrder As Integer = 0
        Dim _FTStateFreeZone As Boolean = False
        Dim _Qry As String = ""

        _Qry = "SELECT TOP 1 FNHSysCmpId,FTStateFreeZone "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTWHCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdTo.Text) & "'"
        _DtWarehouse = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In _DtWarehouse.Rows
            _FTStateFreeZone = (R!FTStateFreeZone.ToString = "1")
            _FNHSysCmpIdTo = Integer.Parse(Val(R!FNHSysCmpId.ToString))
            Exit For
        Next



        Select Case True
            Case (Microsoft.VisualBasic.Left(FTBarcodeNo.Text, HI.INVEN.Barcode.BarGrpRun.Length) = HI.INVEN.Barcode.BarGrpRun And HI.INVEN.Barcode.BarGrpRun <> "")
                ProcessBarcodeGrp(FTBarcodeNo.Text, _FNHSysCmpIdTo, _FTStateFreeZone)
            Case (Microsoft.VisualBasic.Left(FTBarcodeNo.Text, 2).ToLower = "pl".ToLower)
                ProcessBarcodePL(FTBarcodeNo.Text, _FNHSysCmpIdTo, _FTStateFreeZone)
            Case Else
                FNQuantityBal.Value = 0
                Dim _Dt As DataTable = Barcode.BarCodeBalance(FTBarcodeNo.Text, 0.ToString, "", Me.FTTransferWHNo.Text, True)

                If _Dt.Rows.Count > 0 Then
                    If _Dt.Select("FNQuantityBal >=" & Qty & "").Length > 0 Then

                        If _Dt.Select("FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >=" & Qty & " AND FNQuantityBal >0").Length > 0 Then
                            Me.OrderNo = ""
                            If Me.FTOrderNo.Text = "" Then
                                For Each R As DataRow In _Dt.Select("FNQuantityBal >=" & Qty & " AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 ")
                                    Me.FTOrderNo.Text = R!FTOrderNo.ToString
                                    Exit For
                                Next
                            End If

                            If Me.FTOrderNo.Text = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTOrderNo_lbl.Text)
                                FTOrderNo.Focus()
                                Exit Sub
                            End If

                            _Qry = "SELECT TOP 1 FNHSysCmpId,FNOrderType "
                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS W WITH(NOLOCK)"
                            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"

                            _DtOrder = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _FNHSysCmpIdToOrder = 0
                            _FNOrderType = 0

                            For Each Rox As DataRow In _DtOrder.Rows
                                _FNOrderType = Integer.Parse(Val(Rox!FNOrderType.ToString))
                                _FNHSysCmpIdToOrder = Integer.Parse(Val(Rox!FNHSysCmpId.ToString))
                                Exit For
                            Next

                            If (_FNHSysCmpIdTo <> _FNHSysCmpIdToOrder Or _FNHSysCmpIdToOrder = 0 Or _FNHSysCmpIdTo = 0) And (_FNOrderType = 0 Or _FNOrderType = 2 Or _FNOrderType = 3 Or _FNOrderType = 13 Or _FNOrderType = 22) And _FTStateFreeZone = False Then
                                HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ โอนได้กรุณาทำการตรวจสอบ !!!", 1498928411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            Me.WH = Val(FNHSysWHId.Properties.Tag.ToString)
                            Me.OrderNo = Me.FTOrderNo.Text
                            Me.WHTo = Val(FNHSysWHIdTo.Properties.Tag.ToString)
                            Me.OrderNoTo = Me.FTOrderNo.Text
                            Me.DocRefNo = ""

                            If _Dt.Select("FNQuantityBal >=" & Qty & " AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' ").Length > 0 Then

                                For Each R As DataRow In _Dt.Select("FNQuantityBal >=" & Qty & " AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' ")
                                    If Qty <> 0 Then
                                        FNQuantityBal.Value = Qty
                                    Else
                                        FNQuantityBal.Value = R!FNQuantityBal
                                    End If

                                    Me.FNPriceTrans = Val(R!FNPriceTrans)
                                    Me.DocRefNo = R!FTDocumentNo.ToString

                                    Me.PriceClosed1 = Val(R!FNPriceClose1)
                                    Me.PriceClosed2 = Val(R!FNPriceClose2)

                                    Exit For
                                Next

                                If (StateAdd) Then
                                    If SaveBarcode(FTBarcodeNo.Text) Then
                                        FTBarcodeNo.Focus()
                                        FTBarcodeNo.SelectAll()
                                        FNQuantityBal.Value = 0

                                        LoadDucumentDetail(Me.FTTransferWHNo.Text)
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
                            HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ คลังนี้  !!!", 1311240008, Me.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("จำนวน Balance ไม่พอ !!!", 1311240010, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("ไม่พบข้อมูลหมายเลข Barcode !!!", 1311240007, Me.Text)
                End If
                _Dt.Dispose()
        End Select


    End Sub


    Private Sub ProcessBarcodePL(BarcodeGrpNo As String, _FNHSysCmpIdTo As Integer, _FTStateFreeZone As Boolean)
        FNQuantityBal.Value = 0
        Dim _Dt As DataTable = Barcode.BarCodePLBalance(FTBarcodeNo.Text, 0.ToString, "", Me.FTTransferWHNo.Text, True)
        If _Dt.Rows.Count > 0 Then

            Dim _RawmatId As Integer = 0

            If _Dt.Select(" FNQuantityBal >0 ").Length > 0 Then
                If _Dt.Select("FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & "  AND FNQuantityBal >0 ").Length > 0 Then
                    If _Dt.Select(" FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0   ").Length > 0 Then

                        Dim BarcodeNo As String = ""
                        Dim StateAdd As Boolean = False
                        Dim DataOrderNo As String = ""
                        Dim _Qry As String = ""
                        Dim _DtOrder As DataTable
                        Dim _FNHSysCmpIdToOrder As Integer
                        Dim _FNOrderType As Integer


                        For Each R As DataRow In _Dt.Select("   FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 ")
                            BarcodeNo = R!FTBarcodeNo.ToString
                            _RawmatId = Val(R!FNHSysRawMatId.ToString())
                            DataOrderNo = R!FTOrderNo.ToString


                            _Qry = "SELECT TOP 1 FNHSysCmpId,FNOrderType "
                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS W WITH(NOLOCK)"
                            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(DataOrderNo) & "'"

                            _DtOrder = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _FNHSysCmpIdToOrder = 0
                            _FNOrderType = 0
                            For Each Rox As DataRow In _DtOrder.Rows
                                _FNOrderType = Integer.Parse(Val(Rox!FNOrderType.ToString))
                                _FNHSysCmpIdToOrder = Integer.Parse(Val(Rox!FNHSysCmpId.ToString))
                                Exit For
                            Next

                            _DtOrder.Dispose()
                            If (_FNHSysCmpIdTo <> _FNHSysCmpIdToOrder Or _FNHSysCmpIdToOrder = 0 Or _FNHSysCmpIdTo = 0) And (_FNOrderType = 0 Or _FNOrderType = 2 Or _FNOrderType = 3 Or _FNOrderType = 13 Or _FNOrderType = 22) And _FTStateFreeZone = False Then
                            Else

                                Me.WH = Val(FNHSysWHId.Properties.Tag.ToString)
                                Me.OrderNo = DataOrderNo
                                Me.WHTo = Val(FNHSysWHIdTo.Properties.Tag.ToString)
                                Me.OrderNoTo = DataOrderNo
                                Me.DocRefNo = ""
                                Me.FNPriceTrans = -1
                                Me.DocRefNo = R!FTDocumentNo.ToString

                                FNQuantityBal.Value = Val(R!FNQuantityBal.ToString)

                                If HI.INVEN.StockValidate.OrderUsedRawmat(DataOrderNo, _RawmatId, False) = False Then
                                    Exit Sub
                                End If

                                If SaveBarcode(BarcodeNo, BarcodeGrpNo) Then

                                    StateAdd = True

                                End If

                            End If

                            Me.DocRefNo = ""

                        Next

                        If (StateAdd) Then

                            LoadDucumentDetail(Me.FTTransferWHNo.Text)
                            FTBarcodeNo.Focus()
                            FTBarcodeNo.SelectAll()
                            FNQuantityBal.Value = 0

                        Else

                            If FNQuantityBal.Properties.ReadOnly = False Then

                                FNQuantityBal.Focus()
                                FNQuantityBal.SelectAll()

                            End If

                        End If
                        Me.DocRefNo = ""
                    Else
                        HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ Order นี้  !!!", 1311240009, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ คลังนี้  !!!", 1311240008, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("จำนวน Balance ไม่พอ !!!", 1311240010, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData("ไม่พบข้อมูลหมายเลข Barcode !!!", 1311240007, Me.Text)
        End If
        _Dt.Dispose()
    End Sub

    Private Sub ProcessBarcodeGrp(BarcodeGrpNo As String, _FNHSysCmpIdTo As Integer, _FTStateFreeZone As Boolean)
        FNQuantityBal.Value = 0
        Dim _Dt As DataTable = Barcode.BarCodeGrpBalance(FTBarcodeNo.Text, 0.ToString, "", Me.FTTransferWHNo.Text, True)
        If _Dt.Rows.Count > 0 Then

            Dim _RawmatId As Integer = 0
            '_RawmatId = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A WITH(NOLOCK) WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'", Conn.DB.DataBaseName.DB_INVEN, "0")))
            'If HI.INVEN.StockValidate.OrderUsedRawmat(Me.FTOrderNo.Text, _RawmatId) = False Then
            '    Exit Sub
            'End If

            If _Dt.Select(" FNQuantityBal >0 ").Length > 0 Then
                If _Dt.Select("FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & "  AND FNQuantityBal >0 ").Length > 0 Then


                    Dim BarcodeNo As String = ""
                    Dim StateAdd As Boolean = False
                        Dim DataOrderNo As String = ""
                        Dim _Qry As String = ""
                        Dim _DtOrder As DataTable
                        Dim _FNHSysCmpIdToOrder As Integer
                        Dim _FNOrderType As Integer

                    For Each R As DataRow In _Dt.Select("  FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 ")
                        BarcodeNo = R!FTBarcodeNo.ToString
                        _RawmatId = Val(R!FNHSysRawMatId.ToString())
                        DataOrderNo = R!FTOrderNo.ToString


                        _Qry = "SELECT TOP 1 FNHSysCmpId,FNOrderType "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS W WITH(NOLOCK)"
                        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(DataOrderNo) & "'"

                        _DtOrder = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                        _FNHSysCmpIdToOrder = 0
                        _FNOrderType = 0
                        For Each Rox As DataRow In _DtOrder.Rows
                            _FNOrderType = Integer.Parse(Val(Rox!FNOrderType.ToString))
                            _FNHSysCmpIdToOrder = Integer.Parse(Val(Rox!FNHSysCmpId.ToString))
                            Exit For
                        Next

                        _DtOrder.Dispose()
                        If (_FNHSysCmpIdTo <> _FNHSysCmpIdToOrder Or _FNHSysCmpIdToOrder = 0 Or _FNHSysCmpIdTo = 0) And (_FNOrderType = 0 Or _FNOrderType = 2 Or _FNOrderType = 3 Or _FNOrderType = 13 Or _FNOrderType = 22) And _FTStateFreeZone = False Then
                        Else

                            Me.WH = Val(FNHSysWHId.Properties.Tag.ToString)
                            Me.OrderNo = DataOrderNo
                            Me.WHTo = Val(FNHSysWHIdTo.Properties.Tag.ToString)
                            Me.OrderNoTo = DataOrderNo
                            Me.DocRefNo = ""
                            Me.FNPriceTrans = Val(R!FNPriceTrans)
                            Me.DocRefNo = R!FTDocumentNo.ToString

                            FNQuantityBal.Value = Val(R!FNQuantityBal.ToString)

                            If HI.INVEN.StockValidate.OrderUsedRawmat(DataOrderNo, _RawmatId, False) = False Then
                                Exit Sub
                            End If

                            If SaveBarcode(BarcodeNo) Then

                                StateAdd = True

                            End If

                        End If

                    Next

                    If (StateAdd) Then

                        LoadDucumentDetail(Me.FTTransferWHNo.Text)
                        FTBarcodeNo.Focus()
                        FTBarcodeNo.SelectAll()
                        FNQuantityBal.Value = 0

                    Else

                        If FNQuantityBal.Properties.ReadOnly = False Then

                            FNQuantityBal.Focus()
                            FNQuantityBal.SelectAll()

                        End If

                    End If
                    Me.DocRefNo = ""

                Else
                    HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ คลังนี้  !!!", 1311240008, Me.Text)
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

                    Dim _Strsql As String = ""
                    _Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
                    _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH "
                    _Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

                    If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") = "1" Then
                        HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
                        Exit Sub
                    End If


                    Me.FTStateApprove.Checked = False

                    If FTTransferWHNo.Properties.Tag.ToString = "" Then
                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If Me.FTTransferWHNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTTransferWHNo.Text)
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

                Call DeleteBarcode()

                'With ogvdetail
                '    If .RowCount <= 0 Then Exit Sub
                '    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                '    Dim _Strsql As String = ""
                '    _Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
                '    _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH "
                '    _Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

                '    If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") <> "0" Then
                '        HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
                '        Exit Sub
                '    End If

                '    Dim _Barcode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo").ToString

                '    If _Barcode <> "" Then
                '        If DeleteBarcode(_Barcode) Then
                '            FTBarcodeNo.Focus()
                '            FTBarcodeNo.SelectAll()
                '            FNQuantityBal.Value = 0

                '            LoadDucumentDetail(Me.FTTransferWHNo.Text)
                '        End If
                '    End If

                'End With
        End Select
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged


        Try
            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FNHSysWHId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysWHId.Properties.Buttons.Item(0).Enabled = Not ((dt.Rows.Count > 0))


            Dim _StateLock As Boolean = False
            Dim _Qry As String = ""

            _Qry = " SELECT TOP 1  ISNULL(H.FTStateApprove,'') AS FTStateApprove "
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS H WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE H.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "'"
            _StateLock = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "1")

            FNHSysWHIdTo.Properties.ReadOnly = _StateLock
            FNHSysWHIdTo.Properties.Buttons.Item(0).Enabled = Not (_StateLock)

            dt.Dispose()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcode.Click

        Call DeleteBarcode()
    End Sub

    Private Sub DeleteBarcode()
        If CheckOwner() = False Then Exit Sub
        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDTransferWHDate.Text) = True Then
            Exit Sub
        End If

        If FTInvoiceNo.Text = "" Then
            If HI.INVEN.StockValidate.CheckDocCreateInvoiceSale(Me.FTTransferWHNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการบันทึก ออก Invoice แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1011220002, Me.Text)
                Exit Sub
            End If
        End If

        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub


            Dim _Strsql As String = ""
            _Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
            _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH "
            _Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

            If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") = "1" Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
                Exit Sub
            End If

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()

                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString
                Dim _FTOrderNo As String = "" & .GetRowCellValue(i, "FTOrderNo").ToString

                If _Barcode <> "" And _FTOrderNo <> "" Then
                    If DeleteBarcode(_Barcode, _FTOrderNo) Then

                        _StateDelete = True

                    End If
                End If

            Next

            If (_StateDelete) Then
                FTBarcodeNo.Focus()
                FTBarcodeNo.SelectAll()
                FNQuantityBal.Value = 0

                LoadDucumentDetail(Me.FTTransferWHNo.Text)
            End If

        End With
    End Sub

    Private Sub wTransferWHToWH_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        _FormLoad = False

        Dim Indx As Integer = 0
        Try
            Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDoc" & Me.Name))
        Catch ex As Exception
        End Try


        FNListDocumentData.SelectedIndex = Indx

    End Sub

    Private Sub ocmaddbarrcv_Click(sender As Object, e As EventArgs) Handles ocmaddbarrcv.Click
        If CheckOwner() = False Then Exit Sub
        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDTransferWHDate.Text) = True Then
            Exit Sub
        End If

        If FTInvoiceNo.Text = "" Then
            If HI.INVEN.StockValidate.CheckDocCreateInvoiceSale(Me.FTTransferWHNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการบันทึก ออก Invoice แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1011220002, Me.Text)
                Exit Sub
            End If
        End If

        If FTTransferWHNo.Properties.Tag.ToString = "" Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTTransferWHNo.Text = "" Then Exit Sub
            LoadDataInfo(Me.FTTransferWHNo.Text)
        End If

        HI.TL.HandlerControl.ClearControl(_SelectBarcodeFromRcv)
        HI.ST.Lang.SP_SETxLanguage(_SelectBarcodeFromRcv)

        With _SelectBarcodeFromRcv
            .StateProc = False
            .TransferNo = Me.FTTransferWHNo.Text
            .WHID = Val(Me.FNHSysWHId.Properties.Tag.ToString)
            .FNHSysWHId.Text = Me.FNHSysWHId.Text
            .WHTo = Me.FNHSysWHIdTo.Text
            .MainObject = Me
            .ShowDialog()

            'If (.StateProc) Then
            '    Call LoadDucumentDetail(Me.FTTransferWHNo.Text)
            'End If

        End With

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged

    End Sub

    Private Sub FTTransferWHNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTTransferWHNo.EditValueChanged

    End Sub

    Private Sub ApproveDocument(DocumentNokey As String)
        Try

            If Not (Me.ogcdetail.DataSource Is Nothing) Then
                With CType(Me.ogcdetail.DataSource, DataTable)
                    .AcceptChanges()

                    If .Rows.Count <= 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการ กรุณาทำการตรวจสอบ !!!", 1806480457, Me.Text,, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End With

                Dim cmd As String = ""
                Dim SysCmpId As Integer = 0
                Dim SysCmpPOBF As Integer = 0
                Dim SysWHCmpId As Integer = 0
                Dim StateProcess As Boolean = False
                Dim StateCancelInvoiceBF As Boolean = False
                Dim StateCancelInvoiceRemark As String = ""

                If FTFacPurchaseNo.Text <> "" Then

                    cmd = " SELECT TOP 1 FNHSysCmpIdCreate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(FTFacPurchaseNo.Text) & "'"
                    SysCmpPOBF = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PUR))

                End If

                cmd = "select top 1 C.FNHSysCmpIdTo "
                cmd &= vbCrLf & "  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X with(nolock) "
                cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON X.FNHSysCmpId=C.FNHSysCmpId  "
                cmd &= vbCrLf & "  where X.FTWHCode='" & HI.UL.ULF.rpQuoted(FNHSysWHIdTo.Text) & "'"
                SysCmpId = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MERCHAN, "0"))

                cmd = "select top 1 C.FNHSysCmpIdTo "
                cmd &= vbCrLf & "  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X with(nolock) "
                cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON X.FNHSysCmpId=C.FNHSysCmpId  "
                cmd &= vbCrLf & "  where X.FTWHCode='" & HI.UL.ULF.rpQuoted(FNHSysWHId.Text) & "'"
                SysWHCmpId = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

                StateProcess = False
                If SysCmpPOBF > 0 Then
                    If SysCmpPOBF <> SysCmpId Then

                        With New wTransferWHToWHInvoiceRemark
                            .ShowDialog()

                            If .StateProc Then
                                StateCancelInvoiceRemark = .FTRemark.Text.Trim()
                                StateProcess = True
                                StateCancelInvoiceBF = True
                            Else
                                StateProcess = False
                            End If

                        End With
                    Else
                        If SysCmpPOBF = SysCmpId And SysCmpId = SysWHCmpId Then
                            If FTInvoiceNo.Text <> "" Then

                                With New wTransferWHToWHInvoiceRemark
                                    .ShowDialog()

                                    If .StateProc Then
                                        StateCancelInvoiceRemark = .FTRemark.Text.Trim()
                                        StateProcess = True
                                        StateCancelInvoiceBF = True
                                    Else
                                        StateProcess = False
                                    End If

                                End With
                            Else
                                StateProcess = True
                            End If
                        Else
                            StateProcess = True
                        End If

                    End If
                Else
                    StateProcess = True
                End If


                If StateProcess = False Then
                    Exit Sub
                End If

                If SysCmpId > 0 And SysWHCmpId > 0 Then
                    If SysCmpId = SysWHCmpId Then

                        cmd = "UPDATE A Set "
                        cmd &= vbCrLf & "  FTFacPurchaseNo=''"
                        cmd &= vbCrLf & ", FTDocumentState='1'"
                        cmd &= vbCrLf & " ,FTInvoiceNo=''"
                        cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A "
                        cmd &= vbCrLf & "WHERE  A.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "'"

                        If StateCancelInvoiceBF Then
                            If FTFacPurchaseNo.Text <> "" Then

                                cmd &= vbCrLf & " Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTFacPurchaseNo.Text) & "'"
                                cmd &= vbCrLf & " Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTFacPurchaseNo.Text) & "'"


                            End If

                            If FTInvoiceNo.Text <> "" Then

                                cmd &= vbCrLf & "  Update A Set FTStateCancel='1',FTCancelNote='" & HI.UL.ULF.rpQuoted(StateCancelInvoiceRemark) & "'  "
                                cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS A "
                                cmd &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"

                            End If

                        End If

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN) = True Then
                            FTDocumentState.Checked = True
                            LoadDataInfo(FTTransferWHNo.Text)
                            HI.MG.ShowMsg.mInfo("Confirm DucumentComplete !!!  ", 1156449458, Me.Text, , MessageBoxIcon.Warning)
                        End If

                        Exit Sub
                    Else

                        cmd = "     Select  MAX(BB.FTPurchaseNo) As FTPurchaseNo "
                        cmd &= vbCrLf & "   ,MIN(ISNULL(RCD.FDReceiveDate,'')) AS FDReceiveDate "
                        cmd &= vbCrLf & "      From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As BB WITH(NOLOCK) INNER Join"
                        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As B With(NOLOCK)  On BB.FTBarcodeNo = B.FTBarcodeNo INNER Join"
                        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A WITH(NOLOCK)  On B.FTDocumentNo = A.FTTransferWHNo"
                        cmd &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As XXX WITH(NOLOCK)  On B.FTDocumentRefNo = XXX.FTReceiveNo "
                        cmd &= vbCrLf & "    OUTER APPLY(Select TOP 1 FDReceiveDate FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As X With(NOLOCK) WHERE X.FTPurchaseNo =XXX.FTPurchaseNo ) As RCD"
                        cmd &= vbCrLf & "    Where(A.FTTransferWHNo ='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "') "

                        Dim dtpo As DataTable
                        dtpo = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_INVEN)

                        If dtpo.Rows.Count > 0 Then

                            Dim PurNo As String = ""
                            For Each Rp As DataRow In dtpo.Rows
                                PurNo = AutoPurchase(SysCmpId, SysWHCmpId, Rp!FDReceiveDate.ToString, Rp!FTPurchaseNo.ToString, StateCancelInvoiceBF, StateCancelInvoiceRemark, FTFacPurchaseNo.Text)
                                Exit For
                            Next

                            If PurNo <> "" Then
                                FTFacPurchaseNo.Text = PurNo
                                FTDocumentState.Checked = True
                                HI.MG.ShowMsg.mInfo("Generate PO Complete !!!  ", 1806449458, Me.Text, PurNo, MessageBoxIcon.Warning)
                            Else
                                Dim dt As DataTable

                                cmd = "select top 1 FTDocumentState,FTInvoiceNo,FTFacPurchaseNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS X WITH(NOLOCK) WHERE FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "'"
                                dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_INVEN)

                                For Each Rx As DataRow In dt.Rows
                                    FTDocumentState.Checked = (Rx!FTDocumentState.ToString = "1")
                                    FTInvoiceNo.Text = Rx!FTInvoiceNo.ToString()
                                    FTFacPurchaseNo.Text = Rx!FTFacPurchaseNo.ToString
                                    Exit For
                                Next

                                dt.Dispose()

                            End If

                        End If

                    End If

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function AutoPurchase(SysCmpId As Integer, SysWHCmpId As Integer, ReceiveDate As String, PORef As String, StateCancelInvoice As Boolean, StateCancelInvoiceRemark As String, Optional pofaccreate As String = "") As String

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
            Dim PODocRef As String = ""
            Dim WHStateDead As Boolean = False

            cmdstring = "Select TOP 1 FTStateDead  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse With(NOLOCK) WHERE FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString()) & " "
            WHStateDead = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "0") = "1")

            If ((SysCmpId = 1311090006 Or SysCmpId = 1311090005 Or SysCmpId = 1410220001 Or SysCmpId = 1501190001) And (SysWHCmpId <> 1311090006 And SysWHCmpId <> 1311090005 And SysWHCmpId <> 1410220001 And SysWHCmpId <> 1501190001)) Or
                ((SysWHCmpId = 1311090006 Or SysWHCmpId = 1311090005 Or SysWHCmpId = 1410220001 Or SysWHCmpId = 1501190001) And (SysCmpId <> 1311090006 And SysCmpId <> 1311090005 And SysCmpId <> 1410220001 And SysCmpId <> 1501190001)) Then

                vatper = 0

                SysCurId = 1310190001

                Dim _Qry As String = ""

                _Qry = " Select TOP 1 FNBuyingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  With(NOLOCK)  "

                If ReceiveDate <> "" Then
                    _Qry &= vbCrLf & "   WHERE  (FDDate ='" & ReceiveDate & "')"
                Else
                    _Qry &= vbCrLf & "   WHERE  (FDDate ='" & HI.UL.ULDate.ConvertEnDB(FDTransferWHDate.Text) & "')"
                End If

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
            pokey = PORef

            Dim DeliveryId As Integer = 0
            cmdstring = "select  TOP 1 FNHSysDeliveryId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery AS X WITH(NOLOCK)  WHERE  FNHSysCmpId =" & Val(SysCmpId) & " AND ISNULL(FNHSysCmpIdTo,0) = 0"
            DeliveryId = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "0"))

            Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
            Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
            Dim _CmpHCreate As String = ""
            Dim _CmpRunText As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 13), 2)
            Dim _POGrp As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 9), 2)
            Dim UserDoucument As String = ""
            Dim dtuser As DataTable
            cmdstring = "Select TOP 1 FTDocRun,FTUserNameAutoPOFac FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp With(NOLOCK) WHERE FNHSysCmpId=" & Val(SysCmpId) & " "

            dtuser = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

            For Each Rxc As DataRow In dtuser.Rows

                _CmpHCreate = Rxc!FTDocRun.ToString()
                UserDoucument = Rxc!FTUserNameAutoPOFac.ToString()

                Exit For
            Next
            dtuser.Dispose()

            If UserDoucument = "" Then
                UserDoucument = HI.ST.UserInfo.UserName

            End If

            If StateCancelInvoice Then
                pofaccreate = ""
            End If


            If pofaccreate = "" Then
                pofacno = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTFacPurchase", "", False, _CmpHCreate & "F" & _CmpRunText & _Year & _POGrp & _POType & _Month).ToString
            End If

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                If StateCancelInvoice Then
                    If FTFacPurchaseNo.Text <> "" Then

                        cmdstring = " Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTFacPurchaseNo.Text) & "'"
                        cmdstring &= vbCrLf & "  Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTFacPurchaseNo.Text) & "'"

                        HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If

                    If FTInvoiceNo.Text <> "" Then

                        cmdstring = "  Update A Set FTStateCancel='1',FTCancelNote='" & HI.UL.ULF.rpQuoted(StateCancelInvoiceRemark) & "'  "
                        cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS A "
                        cmdstring &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If

                    Me.FTInvoiceNo.Text = ""
                    Me.FTFacPurchaseNo.Text = ""

                End If

                If pofaccreate = "" Then


                    cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
                    cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
                    cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & " Select TOP 1  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' FTInsUser"
                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " AS FDInsDate "
                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " AS  FDPurchaseDate"
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(UserDoucument) & "' AS FTPurchaseBy"
                    cmdstring &= vbCrLf & " ,'AUTO FROM TRANSFER WH NO ' +  '" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' AS  FTPurchaseState,'' FTRefer," & FNPoState & " AS  FNPoState"
                    cmdstring &= vbCrLf & ",FNHSysPurGrpId"
                    cmdstring &= vbCrLf & " ,FNHSysCmpRunId," & SysWHCmpId & " AS  FNHSysCmpId," & HI.UL.ULDate.FormatDateDB & " AS  FDDeliveryDate"
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
                    cmdstring &= vbCrLf & ",'1' AS   FTStateSendApp,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSendAppBy," & HI.UL.ULDate.FormatDateDB & "  AS FTSendAppDate," & HI.UL.ULDate.FormatTimeDB & " AS FTSendAppTime,  "
                    cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' AS FTPurchaseNoRef," & SysCmpId & " AS  FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo IN ( "
                    cmdstring &= vbCrLf & " SELECT TOP 1 FTPurchaseNo  FROM ( "
                    cmdstring &= vbCrLf & " SELECT TOP 1 FTPurchaseNo ,0 AS FNSeq "
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"
                    cmdstring &= vbCrLf & " UNION "
                    cmdstring &= vbCrLf & " SELECT TOP 1 FTPurchaseNo ,1 AS FNSeq "
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    cmdstring &= vbCrLf & " ) AS XXX ORDER BY XXX.FNSeq "
                    cmdstring &= vbCrLf & " ) "

                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return ""
                    End If


                Else
                    pofacno = pofaccreate
                End If


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
                cmdstring &= vbCrLf & " ,(FNPrice + Convert(numeric(18,4),((FNPrice * ISNULL(FNChargePer,0))/100.00))) AS FNPrice,0 AS FNDisPer,0 AS FNDisAmt"
                cmdstring &= vbCrLf & " ,FNQuantity"
                cmdstring &= vbCrLf & "  ,CONVERT(numeric(18,2),((FNPrice + Convert(numeric(18,4),((FNPrice * ISNULL(FNChargePer,0))/100.00))) ) * FNQuantity) AS FNNetAmt,'' AS FTRemark"
                cmdstring &= vbCrLf & " ,FTFabricFrontSize,0 AS FNReservePOQuantity"
                cmdstring &= vbCrLf & "  ,'' AS  FTRawMatColorNameTH,'' AS  FTRawMatColorNameEN,0 AS FNSurchangeAmt,0 AS  FNSurchangePerUnit,CONVERT(numeric(18,2),FNPrice * FNQuantity)  AS FNGrandNetAmt,'' AS FTOGacDat"
                cmdstring &= vbCrLf & " ,FTPurchaseNo"
                cmdstring &= vbCrLf & " FROM(Select B.FTOrderNo"
                cmdstring &= vbCrLf & "  , BB.FNHSysRawMatId"
                cmdstring &= vbCrLf & "  , MAX(BB.FNHSysUnitId) As FNHSysUnitId"

                If WHStateDead Then
                    cmdstring &= vbCrLf & "  , MAX(CASE WHEN ISNULL(MDP.FNPrice,0) <=0 THEN (CASE WHEN ISNULL(BI.FNPriceTrans,0) <=0 THEN BB.FNPrice ELSE ISNULL(BI.FNPriceTrans,0) END) ELSE ISNULL(MDP.FNPrice,0) END) AS FNPrice		"

                Else
                    cmdstring &= vbCrLf & "  , MAX(CASE WHEN ISNULL(BI.FNPriceTrans,0) <=0 THEN BB.FNPrice ELSE ISNULL(BI.FNPriceTrans,0) END) AS FNPrice		"
                End If

                cmdstring &= vbCrLf & " , SUM(B.FNQuantity) As FNQuantity"
                cmdstring &= vbCrLf & "  , MAX(BB.FTFabricFrontSize) AS FTFabricFrontSize"
                cmdstring &= vbCrLf & "  , MAX(BB.FTPurchaseNo) As FTPurchaseNo"
                cmdstring &= vbCrLf & "  , MAX(ISNULL(PCG.FNChargePer,0)) As FNChargePer"

                cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As BB WITH(NOLOCK) INNER Join"
                cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As B WITH(NOLOCK)  On BB.FTBarcodeNo = B.FTBarcodeNo INNER Join"
                cmdstring &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A WITH(NOLOCK)  On B.FTDocumentNo = A.FTTransferWHNo"
                cmdstring &= vbCrLf & " OUTER APPLY (SELECT TOP 1 FNPriceTrans FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As BIX WITH(NOLOCK) WHERE BIX.FTBarcodeNo=B.FTBarcodeNo AND BIX.FTOrderNo= B.FTOrderNo AND BIX.FTDocumentNo =B.FTDocumentRefNo ) AS BI "


                cmdstring &= vbCrLf & " OUTER APPLY ( "
                cmdstring &= vbCrLf & "  SELECT TOP 1  ISNULL(CH.FNChargePer, 0) AS FNChargePer "

                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS AX "
                cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS AXH  ON AX.FTReceiveNo=AXH.FTReceiveNo"
                cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  ON AXH.FTPurchaseNo=H.FTPurchaseNo"
                cmdstring &= vbCrLf & "  INNER Join "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On AX.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
                cmdstring &= vbCrLf & " (SELECT  *  "
                cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
                cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & SysCmpId & ""
                cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType AND H.FNPoState=CH.FNPoType"
                cmdstring &= vbCrLf & " WHERE AX.FTReceiveNo=B.FTDocumentRefNo AND AX.FNHSysRawMatId=BB.FNHSysRawMatId"

                cmdstring &= vbCrLf & "   ) As  PCG "

                cmdstring &= vbCrLf & " OUTER APPLY ( "
                cmdstring &= vbCrLf & "  Select TOP 1  AX.FNPrice  "

                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPriceMatType As AX  WITH(NOLOCK)	 "
                cmdstring &= vbCrLf & "     INNER Join     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM WITH(NOLOCK)	  On AX.FNHSysMainMatId = MM.FNHSysMainMatId  "
                cmdstring &= vbCrLf & "     INNER Join     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IMX WITH(NOLOCK)	  On MM.FTMainMatCode = IMX.FTRawMatCode   "

                cmdstring &= vbCrLf & " WHERE AX.FNInvoiceState='2' AND AX.FTStateActive = '1' And IMX.FNHSysRawMatId=BB.FNHSysRawMatId"
                cmdstring &= vbCrLf & "   ) As  MDP "

                cmdstring &= vbCrLf & " Where (A.FTTransferWHNo = N'" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "')"
                cmdstring &= vbCrLf & " Group By B.FTOrderNo, BB.FNHSysRawMatId"
                cmdstring &= vbCrLf & " ) As R"


                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return ""
                End If


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

                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return ""
                    End If

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

                poamt = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
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
                cmdstring &= vbCrLf & ",FTStateSendApp='1'"
                cmdstring &= vbCrLf & ",FTSendAppBy='" & HI.UL.ULF.rpQuoted(UserDoucument) & "'"
                cmdstring &= vbCrLf & ",FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
                cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return ""
                End If

            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return ""
            End Try




            If AutoSaleInvoice(pofacno, SysCurId, ExcRate, vatper, SysCmpId, SysWHCmpId) = False Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return ""

            Else
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return pofacno
            End If

        Catch ex As Exception
            Return ""
        End Try


        Return ""

    End Function

    Private Function AutoSaleInvoice(ByVal pofacno As String, SysCurId As Integer, Rateexchange As Decimal, Vat As Decimal, CmpIdTo As Integer, SysWHCmpId As Integer) As Boolean


        Try
            Dim _Qry As String = ""
            Dim Invoice As String = ""
            Dim _CmpH As String = ""

            Dim UserDoucument As String = ""
            Dim dtuser As DataTable
            _Qry = "Select TOP 1 FTDocRun,FTUserNameAutoInvoice  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp With(NOLOCK) WHERE FNHSysCmpId=" & SysWHCmpId & " "

            dtuser = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            For Each Rxc As DataRow In dtuser.Rows

                _CmpH = Rxc!FTDocRun.ToString()
                UserDoucument = Rxc!FTUserNameAutoInvoice.ToString()

                Exit For
            Next
            dtuser.Dispose()

            If UserDoucument = "" Then
                UserDoucument = HI.ST.UserInfo.UserName
            End If

            If FTInvoiceNo.Text = "" Then
                Invoice = HI.TL.Document.GetDocumentNoOnBeginTrans("HITECH_ACCOUNT", "TACCTSaleInvoice", "2", False, _CmpH).ToString

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTInvoiceNo, FDInvoiceDate, FTInvoiceBy, FTDocRefNo, FNInvoiceState"
                _Qry &= vbCrLf & ", FNHSysCmpIdTo, FTRemark, FNInvAmt, FNDisCountPer, FNDisCountAmt, "
                _Qry &= vbCrLf & "  FNInvNetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNInvGrandAmt"
                _Qry &= vbCrLf & " , FTInvGrandAmtTH, FTInvGrandAmtEN, FNHSysCmpId, FNHSysShipModeId, FTPaymentTerms, FTReferenceNo"
                _Qry &= vbCrLf & " , FTECNo, FNHSysCurId, FNExchangeRate,  "
                _Qry &= vbCrLf & "   FNChargeService, FNChargeClear, FNHSysTermOfPMId, FTStateAuto,FTDocAutoRefNo,FDDateSailing "
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & "  Select   "
                _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & "  , " & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  , " & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,   '" & HI.UL.ULF.rpQuoted(Invoice) & "' "
                _Qry &= vbCrLf & "  ,   '" & HI.UL.ULDate.ConvertEnDB(FDTransferWHDate.Text) & "' "
                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(UserDoucument) & "' "
                _Qry &= vbCrLf & " ,'' As  FTDocRefNo,2 AS  FNInvoiceState"
                _Qry &= vbCrLf & "," & CmpIdTo & " AS  FNHSysCmpIdTo,'' AS  FTRemark, 0 AS  FNInvAmt, 0 AS FNDisCountPer,0 AS FNDisCountAmt, "
                _Qry &= vbCrLf & "  0 AS FNInvNetAmt," & Vat & " FNVatPer, 0 AS FNVatAmt,0 AS FNSurcharge,0 AS FNInvGrandAmt"
                _Qry &= vbCrLf & " , '' AS FTInvGrandAmtTH,'' FTInvGrandAmtEN," & SysWHCmpId & " AS  FNHSysCmpId"
                _Qry &= vbCrLf & " ,1406250001 AS  FNHSysShipModeId,'' FTPaymentTerms,'' FTReferenceNo"
                _Qry &= vbCrLf & " ,'' FTECNo," & SysCurId & " AS  FNHSysCurId," & Rateexchange & " AS   FNExchangeRate,  "
                _Qry &= vbCrLf & "  0 AS  FNChargeService,0 AS FNChargeClear,1405060001 AS FNHSysTermOfPMId,'1' AS  FTStateAuto "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' "
                _Qry &= vbCrLf & "  ,   '" & HI.UL.ULDate.ConvertEnDB(FDTransferWHDate.Text) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Return False
                End If

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTInvoiceNo, FTDocRefNo)"
                _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Invoice) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Return False
                End If

            Else
                Invoice = FTInvoiceNo.Text

            End If

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail"
            _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail"
            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTInvoiceNo, FNHSysRawMatId, FNPrice, FNQuantity"
            _Qry &= vbCrLf & ",FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice,FNHSysUnitId,FNCTN, FNNW, FNGW, FNQBM,FNGrpSeq,FNChargeServicePer,FNChargeClearPer)"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Invoice) & "'"
            _Qry &= vbCrLf & ", FNHSysRawMatId"
            _Qry &= vbCrLf & ",FNPrice"
            _Qry &= vbCrLf & ",FNQuantity"
            _Qry &= vbCrLf & ",FNPriceSale"
            _Qry &= vbCrLf & ",0 AS FNChargeService"
            _Qry &= vbCrLf & ",0 AS FNChargeService"
            _Qry &= vbCrLf & ",FNNetPrice"
            _Qry &= vbCrLf & ",FNHSysUnitId_Hide"
            _Qry &= vbCrLf & ",0 AS FNCTN"
            _Qry &= vbCrLf & ",0 AS FNNW"
            _Qry &= vbCrLf & ",0 AS FNGW "
            _Qry &= vbCrLf & ",0 AS FNQBM "
            _Qry &= vbCrLf & ",FNGrpSeq"
            _Qry &= vbCrLf & ",0 AS FNChargeServicePer"
            _Qry &= vbCrLf & ",0 AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM ( Select     TT.FTBarcodeNo, TT.FTOrderNo,SUM(Isnull(B_1.FNQuantity ,TT.FNQuantity )) As FNQuantity , TT.FNHSysCmpId, TT.FNHSysWHId, TT.FTDocumentNo, max(TT.FTFabricFrontSize) As FTFabricFrontSize, TT.FTPurchaseNo,SUM(Isnull(B_1.FNQuantity ,TT.FNQuantity )) * TT.FNPrice As FNAmount, TT.FNHSysRawMatId, "
            _Qry &= vbCrLf & "  TT.FTRawMatCode, TT.FTRawMatNameTH, TT.FTRawMatNameEN, TT.FTRawMatColorCode, TT.FTRawMatSizeCode, TT.FTRawMatSizeNameTH, TT.FTRawMatSizeNameEN, TT.FNHSysUnitId, "
            _Qry &= vbCrLf & "   TT.FTUnitNameTH, TT.FTUnitNameEN, TT.FNPrice, TT.FTRawMatName, max(TT.FNPriceSale) As FNPriceSale , TT.FNChargeService, TT.FNChargeClear, max(TT.FNNetPrice) As FNNetPrice, TT.FNChargeServicePer, TT.FNChargeClearPer, "
            _Qry &= vbCrLf & "  TT.FNMerMatType, TT.FNHSysUnitId_Hide, max(TT.FNPriceOrg) As FNPriceOrg , TT.FNHSysUnitIdSale, TT.FNConvRatio, TT.FNHSysMatTypeId, TT.FTRawMatColorNameEN, TT.FNCTN, TT.FNNW, TT.FNGW, TT.FNQBM, "
            _Qry &= vbCrLf & "    TT.FNGrpSeq, TT.FNChargeServiceState, TT.FNChargeClearState, TT.FNChargeServiceInv, TT.FNChargeClearInv"
            _Qry &= vbCrLf & " From ( Select     FTBarcodeNo, '' as FTOrderNo, SUM(FNQuantity) AS FNQuantity, FNHSysCmpId, FNHSysWHId, FTDocumentNo,max(FTFabricFrontSize) AS  FTFabricFrontSize, MAX(FTPurchaseNo) AS FTPurchaseNo, SUM(FNAmount) "
            _Qry &= vbCrLf & " AS FNAmount, FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FTRawMatColorCode, FTRawMatSizeCode, FTRawMatSizeNameTH, FTRawMatSizeNameEN, "
            _Qry &= vbCrLf & "FNHSysUnitId, FTUnitNameTH, FTUnitNameEN, FNPrice, FTRawMatName,max(FNPriceSale) AS  FNPriceSale, FNChargeService, FNChargeClear,max(FNNetPrice) AS  FNNetPrice, FNChargeServicePer, FNChargeClearPer, FNMerMatType, "
            _Qry &= vbCrLf & "FNHSysUnitId_Hide,max(FNPriceOrg) AS  FNPriceOrg, FNHSysUnitIdSale, FNConvRatio, FNHSysMatTypeId, FTRawMatColorNameEN, FNCTN, FNNW, FNGW, FNQBM, FNGrpSeq, FNChargeServiceState, "
            _Qry &= vbCrLf & " FNChargeClearState, FNChargeServiceInv, FNChargeClearInv"
            _Qry &= vbCrLf & " From ( SELECT      '' as FTBarcodeNo, BO.FTOrderNo,  BO.FNQuantity  as FNQuantity, BO.FNHSysCmpId, BO.FNHSysWHId,'' as  FTDocumentNo, BR.FTFabricFrontSize, BR.FTPurchaseNo, CONVERT(numeric(18, 4), "
            _Qry &= vbCrLf & "	          Isnull(B.FNQuantity, BO.FNQuantity) * BR.FNPrice) AS FNAmount, BR.FNHSysRawMatId, MM.FTRawMatCode, MM.FTRawMatNameTH,MM.FTRawMatNameEN , MC.FTRawMatColorCode, "
            _Qry &= vbCrLf & "	    MS.FTRawMatSizeCode, MS.FTRawMatSizeNameTH, MS.FTRawMatSizeNameEN, MU.FTUnitCode as FNHSysUnitId, MU.FTUnitNameTH, MU.FTUnitNameEN"

            _Qry &= vbCrLf & ", Isnull(B.FNPriceSale , Isnull(PO2.FNPrice ,0))  as FNPrice"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", Isnull(MD.FTMainMatNameTH, MM.FTRawMatNameTH) as FTRawMatName"
            Else
                _Qry &= vbCrLf & ", Isnull(MD.FTMainMatNameEN, MM.FTRawMatNameEN) as FTRawMatName"
            End If

            _Qry &= vbCrLf & ", ISNULL(PO2.FNPrice,0)  AS FNPriceSale,Isnull(B.FNChargeService,0) AS  FNChargeService,Isnull(B.FNChargeClear,0) AS  FNChargeClear, PO2.FNPrice  AS  FNNetPrice ,Isnull(B.FNChargeServicePer, 0) AS FNChargeServicePer ,Isnull(B.FNChargeClearPer,0) AS FNChargeClearPer "
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
            _Qry &= vbCrLf & " , Isnull(B.FNChargeService,0) AS FNChargeServiceInv  ,Isnull( B.FNChargeClear,0) AS FNChargeClearInv  ,PO2.FNPrice AS FNPOFacPrice"

            _Qry &= vbCrLf & "	FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BR WITH (NOLOCK) ON BO.FTBarcodeNo = BR.FTBarcodeNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS MM WITH (NOLOCK) ON BR.FNHSysRawMatId = MM.FNHSysRawMatId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON MM.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS WITH (NOLOCK) ON MM.FNHSysRawMatSizeId = MS.FNHSysRawMatSizeId  "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MMM WITH(NOLOCK) ON MM.FTRawMatCode = MMM.FTMainMatCode "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_Description AS MD WITH(NOLOCK) ON MMM.FNHSysMainMatId = MD.FNHSysMainMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN (Select * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPriceMatType  WITH(NOLOCK) WHERE FNInvoiceState='2' AND FTStateActive = '1' ) AS MMP  ON MMM.FNHSysMainMatId = MMP.FNHSysMainMatId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN "
            _Qry &= vbCrLf & " ( SELECT FNGrpSeq ,FNHSysRawMatId, FNPrice, FNQuantity, FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice, FNHSysUnitId,  FNCTN, FNNW, FNGW, FNQBM , Isnull(FNChargeServicePer,0) AS FNChargeServicePer , Isnull(FNChargeClearPer,0) AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
            _Qry &= vbCrLf & " ) AS B ON BR.FNHSysRawMatId = B.FNHSysRawMatId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS MU WITH (NOLOCK) ON Isnull(B.FNHSysUnitId,BR.FNHSysUnitId) = MU.FNHSysUnitId"


            _Qry &= vbCrLf & "   OUTER APPLY ( SELECT TOP 1  PF.FNPrice  "
            _Qry &= vbCrLf & "   FROM   "
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS PF WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE  (PF.FTOrderNo =BO.FTOrderNo) "
            _Qry &= vbCrLf & "         And (PF.FNHSysRawMatId =  BR.FNHSysRawMatId) "
            _Qry &= vbCrLf & "         And (PF.FTFacPurchaseNo  ='" & HI.UL.ULF.rpQuoted(pofacno) & "') "
            _Qry &= vbCrLf & "  ) AS PO2  "

            _Qry &= vbCrLf & "WHERE BO.FTDocumentNo ='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "' ) AS T "
            _Qry &= vbCrLf & "GROUP BY FTBarcodeNo,   FNHSysCmpId, FNHSysWHId, FTDocumentNo,  FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FTRawMatColorCode, " 'FTFabricFrontSize,
            _Qry &= vbCrLf & " FTRawMatSizeCode, FTRawMatSizeNameTH, FTRawMatSizeNameEN, FNHSysUnitId, FTUnitNameTH, FTUnitNameEN, FNPrice, FTRawMatName,  FNChargeService, FNChargeClear, "
            _Qry &= vbCrLf & "  FNChargeServicePer, FNChargeClearPer, FNMerMatType, FNHSysUnitId_Hide,  FNHSysUnitIdSale, FNConvRatio, FNHSysMatTypeId, FTRawMatColorNameEN, FNCTN, "
            _Qry &= vbCrLf & " FNNW, FNGW, FNQBM, FNGrpSeq, FNChargeServiceState, FNChargeClearState, FNChargeServiceInv, FNChargeClearInv ) AS TT "
            _Qry &= vbCrLf & " LEFT OUTER JOIN "
            _Qry &= vbCrLf & "   (SELECT     FNGrpSeq, FNHSysRawMatId, FNPrice, FNQuantity, FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice, FNHSysUnitId, FNCTN, FNNW, FNGW, FNQBM, "
            _Qry &= vbCrLf & "  ISNULL(FNChargeServicePer, 0) AS FNChargeServicePer, ISNULL(FNChargeClearPer, 0) AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTSaleInvoice_Detail AS A WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE      (FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Invoice) & "')) AS B_1 ON TT.FNHSysRawMatId = B_1.FNHSysRawMatId"
            '20160107 Group Rawmat not validate OrderNO, PurchaseNo
            _Qry &= vbCrLf & "Group by      TT.FTBarcodeNo, TT.FTOrderNo,  TT.FNHSysCmpId, TT.FNHSysWHId, TT.FTDocumentNo, TT.FTPurchaseNo,   TT.FNHSysRawMatId, " ', TT.FTFabricFrontSize

            _Qry &= vbCrLf & "TT.FTRawMatCode, TT.FTRawMatNameTH, TT.FTRawMatNameEN, TT.FTRawMatColorCode, TT.FTRawMatSizeCode, TT.FTRawMatSizeNameTH, TT.FTRawMatSizeNameEN, TT.FNHSysUnitId,"
            _Qry &= vbCrLf & " TT.FTUnitNameTH, TT.FTUnitNameEN, TT.FNPrice, TT.FTRawMatName,  TT.FNChargeService, TT.FNChargeClear,  TT.FNChargeServicePer, TT.FNChargeClearPer,"
            _Qry &= vbCrLf & " TT.FNMerMatType, TT.FNHSysUnitId_Hide,  TT.FNHSysUnitIdSale, TT.FNConvRatio, TT.FNHSysMatTypeId, TT.FTRawMatColorNameEN, TT.FNCTN, TT.FNNW, TT.FNGW, TT.FNQBM,"
            _Qry &= vbCrLf & " TT.FNGrpSeq, TT.FNChargeServiceState, TT.FNChargeClearState, TT.FNChargeServiceInv, TT.FNChargeClearInv ) AS A "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return False
            End If

            _Qry = " update O set O.FNPriceTrans =  D.FNPriceSale    "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS R WITH (NOLOCK) ON I.FTInvoiceNo = R.FTInvoiceNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENBarcode_OUT AS O WITH (NOLOCK) ON R.FTDocRefNo = O.FTDocumentNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS D WITH(NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo and B.FNHSysRawMatId = D.FNHSysRawMatId"
            _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Invoice) & "')  AND O.FNPriceTrans <>  D.FNPriceSale "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                'Return False
            End If


            Dim cmdstring As String = ""
            Dim poamten As String = ""
            Dim poamtth As String = ""
            Dim poamt As Double = 0
            Dim ponetamt As Double = 0
            Dim podisamt As Double = 0
            Dim povatamt As Double = 0
            Dim Surcharge As Double = 0
            Dim pograndamt As Double = 0

            cmdstring = "   SELECT        SUM(Convert(numeric(18,2),FNQuantity * FNNetPrice)) AS FNAmount"
            cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS A  WITH(NOLOCK)"
            cmdstring &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "' "

            poamt = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT, "0"))
            ponetamt = poamt - podisamt
            povatamt = CDbl(Format((ponetamt * Vat) / 100.0, "0.00"))
            pograndamt = ponetamt + povatamt + Surcharge

            poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
            poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

            cmdstring = "UPDATE A Set "
            cmdstring &= vbCrLf & "  FNInvAmt=" & poamt & ""
            cmdstring &= vbCrLf & ", FNInvNetAmt=" & ponetamt & ""
            cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
            cmdstring &= vbCrLf & ", FNInvGrandAmt=" & pograndamt & ""
            cmdstring &= vbCrLf & ", FTInvGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
            cmdstring &= vbCrLf & ", FTInvGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"
            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice As A "
            cmdstring &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "' "

            If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return False
            End If

            cmdstring = "UPDATE A Set "
            cmdstring &= vbCrLf & "  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
            cmdstring &= vbCrLf & ", FTDocumentState='1'"
            cmdstring &= vbCrLf & " ,FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "'"
            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A "
            cmdstring &= vbCrLf & "WHERE  A.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(FTTransferWHNo.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return False
            End If

            FTInvoiceNo.Text = Invoice

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click

        If CheckOwner() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDTransferWHDate.Text) = True Then
            Exit Sub
        End If

        If FTInvoiceNo.Text = "" Then
            If HI.INVEN.StockValidate.CheckDocCreateInvoiceSale(Me.FTTransferWHNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการบันทึก ออก Invoice แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1011220002, Me.Text)
                Exit Sub
            End If
        End If

        If FTDocumentState.Checked = True Then Exit Sub

        Dim _Strsql As String = ""
        _Strsql = "SELECT TOP 1  ISNULL(FTStateApprove,'') AS  FTStateApprove "
        _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH "
        _Strsql &= vbCrLf & " WHERE    FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTTransferWHNo.Text) & "' "

        If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_INVEN, "") = "1" Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
            Exit Sub
        End If

        If Me.VerrifyData Then

            If Me.SaveData() Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการอนุมัติเอกสารใบโอนสินค้าระหว่างคลังใช่หรื่อไม่ ?", 1901245874) = True Then
                    Call ApproveDocument(FTTransferWHNo.Text)
                End If
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            End If

        End If
    End Sub

    Private Sub FNListDocumentData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentData.SelectedIndex.ToString)
        End If
    End Sub
End Class
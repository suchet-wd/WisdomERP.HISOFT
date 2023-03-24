Imports System.Windows.Forms

Public Class wIssue

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
        _FormLoad = True
        Call InitFormControl()

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

        '_Str = "Select Top 1 FTStateSendApp FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS X WITH(NOLOCK) WHERE FTIssueNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
        'FTStateSendApp.Checked = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "1")

        Call LoadIssueDetail(Key.ToString)


        SetGridView(FTStateSendApp.Checked)

        _ProcLoad = False

    End Sub

    Private Sub LoadIssueDetail(PoKey As String)

 
        ogcdetail.DataSource = HI.INVEN.Barcode.LoadDocumentBarcode(PoKey, Barcode.DocType.Issue)

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
                            'Case ENM.Control.ControlType.PictureEdit
                            '    With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            '        If .Image Is Nothing Then
                            '            Pass = False
                            '        End If
                            '    End With
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


        SetGridView(False)
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

        '------------- Modify 1 by Num 20160429 Change Check Type Production Only to All Request By P'Vet
        '------------- Modify 2 by Num 20160506 Change Check Type Production All to Config
        Dim _Qry As String = ""
        _Qry = "SELECT TOP  1 FTOrderNo "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "'  AND ISNULL(FNHSysCmpId,0) NOT IN (SELECT FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XH WITH(NOLOCK) WHERE XH.FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString()) & "  )   " '" & Val(HI.ST.SysInfo.CmpID) & ""
        '_Qry &= vbCrLf & " AND FNOrderType=0
        _Qry &= vbCrLf & " AND FNOrderType IN ("
        _Qry &= vbCrLf & " Select FNListIndex"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNOrderType')"
        _Qry &= vbCrLf & "  AND (FTStateOrderLockIssueOwnCmp = N'1')"
        _Qry &= vbCrLf & " ) "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
            HI.MG.ShowMsg.mInfo("FO. No นี้ไม่ได้มีการสั่งผลิตที่สาขา นี้ กรุณาทำการตรวจสอบ", 15102105784, Me.Text, Me.FTOrderNo.Text.Trim(), MessageBoxIcon.Warning)
            FTOrderNo.Focus()
            Return False
        End If
        '------------- Modify 1 by Num 20160429 Change Check Type Production Only to All Request By P'Vet

        Return True
    End Function

    Private Function SaveData() As Boolean
        Dim StateSendApp As String = "0"

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

        Dim WHCmpID As Integer = 0

        _Str = "Select TOp 1 FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As WH With(NOLOCK)  WHERE FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " "
        WHCmpID = Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "0"))

        If WHCmpID <> HI.ST.SysInfo.CmpID Then
            StateSendApp = "1"
        End If

        FTStateSendApp.Checked = (StateSendApp = "1")
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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue WHERE FTIssueNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue WHERE FTIssueNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueNo.Text) & "'")

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
        If (HI.ST.UserInfo.UserName.ToUpper = FTIssueBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTIssueBy.Text) & "' "
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

    Private Function CheckCreatePurchaseSendSuplier() As Boolean

        Dim _Qry As String

        _Qry = "SELECT TOP 1  FTIssueNo "
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocIssueRef AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTIssueNo='" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & ""

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1487789811, "พบการเปิด PO ส่ง Supplier แล้ว ไม่สามารถทำการลบหรือแก้ไขได้ !!! ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If

    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If Barcode.CheckDocumentRefIn(FTIssueNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220003, Me.Text)
            Exit Sub
        End If
        If CheckOwner() = False Then Exit Sub
        If CheckCreatePurchaseSendSuplier() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDIssueDate.Text) = True Then
            Exit Sub
        End If
        If HI.ST.ValidateData.CloseJob(FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
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

        If Barcode.CheckDocumentRefIn(FTIssueNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220003, Me.Text)
            Exit Sub
        End If

        If Barcode.CheckDocumentRefRTSAfterIssue(FTIssueNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("มีการเดิน คืน Supplier แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312229193, Me.Text)
            Exit Sub
        End If

        If CheckOwner() = False Then Exit Sub
        If CheckCreatePurchaseSendSuplier() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDIssueDate.Text) = True Then
            Exit Sub
        End If
        If HI.ST.ValidateData.CloseJob(FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTIssueNo.Text, Me.Text) = False Then
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
        If Me.FTIssueNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "IssueSlip.rpt"
                .Formular = "{TINVENIssue.FTIssueNo}='" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "IssueSlip_Barcode.rpt"
                .Formular = "{TINVENIssue.FTIssueNo}='" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTIssueNo_lbl.Text)
            FTIssueNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Function SaveBarcode(DataBarCode As String, Optional BarcodePL As String = "") As Boolean
        Dim _Str As String
        Dim _BarCode As String = DataBarCode
        Dim _StateNew As Boolean
        Try

            _Str = " SELECT TOP 1 FTBarcodeNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK) WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "' "
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
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & "' "
                _Str &= vbCrLf & "," & Val("" & FNHSysWHId.Properties.Tag.ToString) & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
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
                _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & FNHSysWHId.Properties.Tag.ToString) & " "
                _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "


                If BarcodePL <> "" Then
                    _Str &= vbCrLf & ",FNQuantity = FNQuantity +" & FNQuantityBal.Value & " "
                Else
                    _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
                End If


                _Str &= vbCrLf & ",FTStateReserve='' "
                _Str &= vbCrLf & ",FNPriceTrans=CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "

                _Str &= vbCrLf & ",FNPriceClose1=CASE WHEN " & Me.PriceClosed1 & "<0 THEN NULL ELSE " & Me.PriceClosed1 & "  END "
                _Str &= vbCrLf & ",FNPriceClose2=CASE WHEN " & Me.PriceClosed2 & "<0 THEN NULL ELSE " & Me.PriceClosed2 & "  END "


                _Str &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & "' "
                _Str &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & " AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'"

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

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

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTIssueNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Str)

            Return True
        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

#End Region

    Private Sub FNIssueBarType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNIssueBarType.SelectedIndexChanged
        FNQuantityBal.Properties.ReadOnly = (FNIssueBarType.SelectedIndex <> 1)

        If FTIssueNo.Text <> "" Then
            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()
        End If

    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        If ocmsave.Enabled = False Then Exit Sub
        If CheckOwner() = False Then Exit Sub

        If HI.ST.ValidateData.CloseJob(FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    If FTIssueNo.Properties.Tag.ToString = "" Then
                        Call SetLoadDefaultByBarCode(FTBarcodeNo.Text)
                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If Me.FTIssueNo.Text = "" Then Exit Sub

                        If FTOrderNo.Properties.ReadOnly = False Then
                            LoadDataInfo(Me.FTIssueNo.Text)
                        End If

                    End If

                        Call AddBarCode(FTBarcodeNo.Text, (FNQuantityBal.Properties.ReadOnly))

                End If
        End Select
    End Sub

    Private Function CheckRawMatRequest(BarcodeKey As String, Optional ShoeMsg As Boolean = True) As Boolean
        If Me.FTIssueReqNo.Text.Trim = "" Then
            Return True
        Else

            Dim _Qry As String = ""

            _Qry = "  Select TOP 1  BC.FTBarcodeNo"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail AS B WITH(NOLOCK)  ON A.FTIssueReqNo = B.FTIssueReqNo INNER JOIN"
            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BC WITH(NOLOCK)   ON B.FNHSysRawMatId = BC.FNHSysRawMatId"
            _Qry &= vbCrLf & "  WHERE  (A.FTIssueReqNo =N'" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "')"
            _Qry &= vbCrLf & "          AND (BC.FTBarcodeNo =N'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "')"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                Return True
            Else

                If ShoeMsg Then
                    HI.MG.ShowMsg.mInvalidData("ไม่พบรายการวัตถุดิบนี้ ในใบขอเบิก กรุณาทำการตรวจสอบ !!!", 1412208127, Me.Text)
                End If

                Return False
            End If

        End If

    End Function

    Private Sub AddBarCode(BarcodeNo As String, StateAdd As Boolean, Optional Qty As Double = 0)

        If CheckOwner() = False Then Exit Sub
        If CheckCreatePurchaseSendSuplier() = False Then Exit Sub

        If Barcode.CheckTransactionIN(BarcodeNo, FTIssueNo.Text, FNHSysWHId.Properties.Tag.ToString, Me.FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312150001, Me.Text)
            Exit Sub
        End If

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDIssueDate.Text) = True Then
            Exit Sub
        End If

        Select Case True
            Case (Microsoft.VisualBasic.Left(FTBarcodeNo.Text, HI.INVEN.Barcode.BarGrpRun.Length) = HI.INVEN.Barcode.BarGrpRun And HI.INVEN.Barcode.BarGrpRun <> "")
                ProcessBarcodeGrp(FTBarcodeNo.Text)
            Case (Microsoft.VisualBasic.Left(FTBarcodeNo.Text, 2).ToLower = "pl".ToLower)
                ProcessBarcodePL(FTBarcodeNo.Text)
            Case Else
                FNQuantityBal.Value = 0
                Dim _Dt As DataTable = Barcode.BarCodeBalance(FTBarcodeNo.Text, 0.ToString, "", Me.FTIssueNo.Text, True)
                If _Dt.Rows.Count > 0 Then

                    If CheckRawMatRequest(FTBarcodeNo.Text) = False Then
                        Exit Sub
                    End If

                    Dim _RawmatId As Integer = 0
                    _RawmatId = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A WITH(NOLOCK) WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'", Conn.DB.DataBaseName.DB_INVEN, "0")))
                    If HI.INVEN.StockValidate.OrderUsedRawmat(Me.FTOrderNo.Text, _RawmatId) = False Then
                        Exit Sub
                    End If

                    If _Dt.Select("FNQuantityBal >=" & Qty & " AND FNQuantityBal >0 ").Length > 0 Then
                        If _Dt.Select("FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >=" & Qty & " AND FNQuantityBal >0 ").Length > 0 Then
                            If _Dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >=" & Qty & "  ").Length > 0 Then

                                If Barcode.CheckTransactionIN(BarcodeNo, FTIssueNo.Text, FNHSysWHId.Properties.Tag.ToString, _OrderNo) Then
                                    HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312150001, Me.Text)
                                    Exit Sub
                                End If

                                Me.DocRefNo = ""
                                For Each R As DataRow In _Dt.Select("FNQuantityBal >=" & Qty & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 ")

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

                                        LoadIssueDetail(Me.FTIssueNo.Text)
                                        FTBarcodeNo.Focus()
                                        FTBarcodeNo.SelectAll()
                                        FNQuantityBal.Value = 0

                                    End If

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
        End Select


    End Sub

    Private Sub ProcessBarcodeGrp(BarcodeGrpNo As String)
        FNQuantityBal.Value = 0
        Dim _Dt As DataTable = Barcode.BarCodeGrpBalance(FTBarcodeNo.Text, 0.ToString, "", Me.FTIssueNo.Text, True)
        If _Dt.Rows.Count > 0 Then

            Dim _RawmatId As Integer = 0

            If _Dt.Select(" FNQuantityBal >0 ").Length > 0 Then
                If _Dt.Select("FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & "  AND FNQuantityBal >0 ").Length > 0 Then
                    If _Dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0   ").Length > 0 Then

                        Dim BarcodeNo As String = ""
                        Dim StateAdd As Boolean = False

                        For Each R As DataRow In _Dt.Select(" FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 ")
                            BarcodeNo = R!FTBarcodeNo.ToString
                            _RawmatId = Val(R!FNHSysRawMatId.ToString())

                            Me.DocRefNo = ""

                            If CheckRawMatRequest(BarcodeNo, False) Then

                                If HI.INVEN.StockValidate.OrderUsedRawmat(Me.FTOrderNo.Text, _RawmatId, False) Then
                                    If Barcode.CheckTransactionIN(BarcodeNo, FTIssueNo.Text, FNHSysWHId.Properties.Tag.ToString, _OrderNo) = False Then

                                        Me.FNPriceTrans = Val(R!FNPriceTrans)
                                        Me.DocRefNo = R!FTDocumentNo.ToString

                                        FNQuantityBal.Value = Val(R!FNQuantityBal.ToString)
                                        If SaveBarcode(BarcodeNo) Then

                                            StateAdd = True

                                        End If

                                    End If
                                End If



                            End If

                        Next

                        If (StateAdd) Then

                            LoadIssueDetail(Me.FTIssueNo.Text)
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


    Private Sub ProcessBarcodePL(BarcodeGrpNo As String)
        FNQuantityBal.Value = 0
        Dim _Dt As DataTable = Barcode.BarCodePLBalance(FTBarcodeNo.Text, 0.ToString, "", Me.FTIssueNo.Text, True)
        If _Dt.Rows.Count > 0 Then

            Dim _RawmatId As Integer = 0

            If _Dt.Select(" FNQuantityBal >0 ").Length > 0 Then
                If _Dt.Select("FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & "  AND FNQuantityBal >0 ").Length > 0 Then
                    If _Dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0   ").Length > 0 Then

                        Dim BarcodeNo As String = ""
                        Dim StateAdd As Boolean = False

                        For Each R As DataRow In _Dt.Select(" FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 ")
                            BarcodeNo = R!FTBarcodeNo.ToString
                            _RawmatId = Val(R!FNHSysRawMatId.ToString())

                            Me.DocRefNo = ""

                            If CheckRawMatRequest(BarcodeNo, False) Then

                                If HI.INVEN.StockValidate.OrderUsedRawmat(Me.FTOrderNo.Text, _RawmatId, False) Then
                                    If Barcode.CheckTransactionIN(BarcodeNo, FTIssueNo.Text, FNHSysWHId.Properties.Tag.ToString, _OrderNo) = False Then

                                        Me.FNPriceTrans = -1 'Val(R!FNPriceTrans)
                                        Me.DocRefNo = R!FTDocumentNo.ToString

                                        FNQuantityBal.Value = Val(R!FNQuantityBal.ToString)
                                        If SaveBarcode(BarcodeNo, BarcodeGrpNo) Then

                                            StateAdd = True

                                        End If

                                    End If
                                End If



                            End If

                        Next

                        If (StateAdd) Then

                            LoadIssueDetail(Me.FTIssueNo.Text)
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
    Private Sub SetLoadDefaultByBarCode(_Barcode As String)
        If Me.FNHSysWHId.Text <> "" And FTOrderNo.Text <> "" Then
            Exit Sub
        End If

        Dim _Dt As DataTable = Barcode.BarCodeBalance(_Barcode, 0.ToString, "", Me.FTIssueNo.Text, True)
        If _Dt.Rows.Count > 0 Then
            If _Dt.Select(" FNQuantityBal >0 ").Length > 0 Then

                For Each R As DataRow In _Dt.Select(" FNQuantityBal >0 ")
                    If Me.FNHSysWHId.Text = "" Then
                        HI.TL.HandlerControl.DynamicButtoneditSysKey_Leave(Me.FNHSysWHId, R!FNHSysWHId.ToString)
                    End If
                    If FTOrderNo.Text = "" Then
                        FTOrderNo.Text = R!FTOrderNo
                    End If

                    Exit For
                Next



            End If

        End If
        _Dt.Dispose()

    End Sub
    Private Sub FNQuantityBal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNQuantityBal.KeyDown
        If CheckOwner() = False Then Exit Sub
        If HI.ST.ValidateData.CloseJob(FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    If FTIssueNo.Properties.Tag.ToString = "" Then

                        Call SetLoadDefaultByBarCode(FTBarcodeNo.Text)

                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If

                    Else
                        If Me.FTIssueNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTIssueNo.Text)
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
                If CheckOwner() = False Then Exit Sub


                If HI.ST.ValidateData.CloseJob(FTOrderNo.Text) Then
                    HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If CheckCreatePurchaseSendSuplier() = False Then Exit Sub

                If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDIssueDate.Text) = True Then
                    Exit Sub
                End If

                With ogvdetail
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    Dim _Barcode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo").ToString

                    If Barcode.CheckTransactionIN(_Barcode, FTIssueNo.Text, FNHSysWHId.Properties.Tag.ToString, Me.FTOrderNo.Text) Then
                        HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312150001, Me.Text)
                        Exit Sub
                    End If

                    If _Barcode <> "" Then
                        If DeleteBarcode(_Barcode) Then
                            FTBarcodeNo.Focus()
                            FTBarcodeNo.SelectAll()
                            FNQuantityBal.Value = 0

                            LoadIssueDetail(Me.FTIssueNo.Text)
                        End If
                    End If

                End With
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
            FTOrderNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNHSysWHId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysWHId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNHSysIssueSectId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysIssueSectId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FTIssueReqNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTIssueReqNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcode.Click
        If HI.ST.ValidateData.CloseJob(FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If

        Call DeleteBarcode()
    End Sub

    Private Sub DeleteBarcode()

        If CheckOwner() = False Then Exit Sub
        If CheckCreatePurchaseSendSuplier() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDIssueDate.Text) = True Then
            Exit Sub
        End If

        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString

                If Barcode.CheckTransactionIN(_Barcode, FTIssueNo.Text, 0, Me.FTOrderNo.Text) Then

                    HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312150001, Me.Text, _Barcode)

                Else

                    If Barcode.CheckTransactionRTSAfterIssue(_Barcode, FTIssueNo.Text, Me.FNHSysWHId.Properties.Tag.ToString, Me.FTOrderNo.Text) Then

                        HI.MG.ShowMsg.mInvalidData("Barcode มีการ คืน Supplier แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312159901, Me.Text, _Barcode)

                    Else

                        If _Barcode <> "" Then

                            If DeleteBarcode(_Barcode) Then

                                _StateDelete = True

                            End If

                        End If

                    End If

                End If
            Next

            If _StateDelete Then
                FTBarcodeNo.Focus()
                FTBarcodeNo.SelectAll()
                FNQuantityBal.Value = 0

                LoadIssueDetail(Me.FTIssueNo.Text)
            End If

        End With
    End Sub

    Private Sub wIssue_Load(sender As Object, e As EventArgs) Handles Me.Load

        _FormLoad = False
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString


        Dim Indx As Integer = 0
        Try
            Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDoc" & Me.Name))
        Catch ex As Exception
        End Try


        FNListDocumentData.SelectedIndex = Indx

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub FNQuantityBal_EditValueChanged(sender As Object, e As EventArgs) Handles FNQuantityBal.EditValueChanged

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged

    End Sub

    Private Sub FTIssueNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTIssueNo.EditValueChanged

    End Sub

    Private Sub FTStateSendApp_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateSendApp.CheckedChanged
        'With ogvdetail
        '    .Columns.ColumnByFieldName("FTSateApp").Visible = (FTStateSendApp.Checked)
        '    .Columns.ColumnByFieldName("FTSateAppBy").Visible = (FTStateSendApp.Checked)
        '    .Columns.ColumnByFieldName("FTSateAppDate").Visible = (FTStateSendApp.Checked)
        '    .Columns.ColumnByFieldName("FTSateAppTime").Visible = (FTStateSendApp.Checked)

        'End With

        SetGridView(FTStateSendApp.Checked)
    End Sub

    Private Sub SetGridView(state As Boolean)

        Try

            With ogvdetail
                .Columns.ColumnByFieldName("FTSateApp").Visible = state
                .Columns.ColumnByFieldName("FTSateAppBy").Visible = state
                .Columns.ColumnByFieldName("FTSateAppDate").Visible = state
                .Columns.ColumnByFieldName("FTSateAppTime").Visible = state

            End With

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FTBarcodeNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FTBarcodeNo.KeyPress

    End Sub

    Private Sub FNHSysWHId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysWHId.EditValueChanged

    End Sub

    Private Sub FNListDocumentData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentData.SelectedIndex.ToString)
        End If
    End Sub
End Class
Imports System.Windows.Forms

Public Class wIssMiniFG
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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



        Call loaddetail(Key.ToString)

        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail(Key As String)

        ' ogcdetail.DataSource = HI.INVEN.Barcode.LoadDocumentBarcode(Key, Barcode.DocType.TransferWH)

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
        Call loaddetail(Me.FTIssMiniFgNo.Text)
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
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString()


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

            If Not SaveDataDetail(_Key) Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
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

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKIssMiniFG WHERE FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKIssMiniFG_Detail WHERE FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            'HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH WHERE FTTransferWHNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "'")


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
        If (HI.ST.UserInfo.UserName.ToUpper = FTIssMiniFgBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTIssMiniFgBy.Text) & "' "
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

        Dim _Strsql As String = ""
        _Strsql = "SELECT TOP 1  ISNULL(FTStateApp,'') AS  FTStateApp "
        _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKIssMiniFG "
        _Strsql &= vbCrLf & " WHERE    FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "' "

        If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_PROD, "") = "1" Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
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

        Dim _Strsql As String = ""
        _Strsql = "SELECT TOP 1  ISNULL(FTStateApp,'') AS  FTStateApp "
        _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKIssMiniFG "
        _Strsql &= vbCrLf & " WHERE    FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "' "

        If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_PROD, "") = "1" Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTIssMiniFgNo.Text, Me.Text) = False Then
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
        Dim _Fm As String = ""
        _Fm = "{TPACKIssMiniFG.FTIssMiniFgNo}='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "' "
        If Me.FTIssMiniFgNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "MiniFGReserve.rpt"
                .Formular = _Fm
                .Preview()
            End With

            'With New HI.RP.Report
            '    .FormTitle = Me.Text
            '    .ReportFolderName = "Inventrory\"
            '    .ReportName = "TransferToWarehouseSlip_Barcode.rpt"
            '    .Formular = "{TINVENTransferWH.FTTransferWHNo}='" & HI.UL.ULF.rpQuoted(FTIssMiniFgNo.Text) & "' "
            '    .Preview()
            'End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTTransferWHNo_lbl.Text)
            FTIssMiniFgNo.Focus()
        End If

    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

#End Region

#Region " Variable "

#End Region


    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown

        Select Case e.KeyCode
            Case Keys.Delete

            Case Keys.Down
                addrow()


        End Select
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged


        Try
            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FNHSysWHFGId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysWHFGId.Properties.Buttons.Item(0).Enabled = Not ((dt.Rows.Count > 0))



            dt.Dispose()

        Catch ex As Exception
        End Try

    End Sub



    Private Sub wTransferWHToWH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogvdetail)
    End Sub


    Private Sub ocmapprove_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        Try

            Dim _Strsql As String = ""
            _Strsql = "SELECT TOP 1  ISNULL(FTStateApp,'') AS  FTStateApp "
            _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKIssMiniFG "
            _Strsql &= vbCrLf & " WHERE    FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "' "

            If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_PROD, "") = "1" Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
                Exit Sub
            End If

            addrow()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmRemoveDT.Click
        Try
            Dim _Strsql As String = ""
            _Strsql = "SELECT TOP 1  ISNULL(FTStateApp,'') AS  FTStateApp "
            _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKIssMiniFG "
            _Strsql &= vbCrLf & " WHERE    FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "' "

            If HI.Conn.SQLConn.GetField(_Strsql, Conn.DB.DataBaseName.DB_PROD, "") = "1" Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการ อนุมัติ รับเข้า แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312220002, Me.Text)
                Exit Sub
            End If


            remrow()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub addrow()
        Try
            If Me.ogcdetail.DataSource Is Nothing Then
                loaddetail(Me.FTIssMiniFgNo.Text)
            End If
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                If .Select("FNCartonNo = 0").Length <= 0 Then
                    .Rows.Add(.Rows.Count + 1, 0, "", "", "", 0, 0, "", "", 0, "", "", "", "", "", "", "")
                End If
                .AcceptChanges()
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub remrow()
        Try


            'With Me.ogvdetail
            '    If .RowCount < 0 And .FocusedRowHandle < -1 Then Exit Sub
            '    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, "") Then
            '        .DeleteRow(.FocusedRowHandle)
            '    End If

            'End With

            'If Not SaveDataDetail(Me.FTIssMiniFgNo.Text) Then

            '    HI.Conn.SQLConn.Tran.Rollback()
            '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            'End If

            Call DeleteDataRow()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DeleteDataRow()
        If CheckOwner() = False Then Exit Sub
        'If CheckCreatePurchaseSendSuplier() = False Then Exit Sub

        'If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDIssueDate.Text) = True Then
        '    Exit Sub
        'End If

        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _Seq As String = "" & .GetRowCellValue(i, "FNSeq").ToString

                If _Seq <> "" Then

                    If DeleteDataRow(_Seq) Then

                        _StateDelete = True

                    End If

                End If

            Next

            If _StateDelete Then
                loaddetail(Me.FTIssMiniFgNo.Text)
            End If

        End With
    End Sub

    Private Function DeleteDataRow(seq As String) As Boolean
        Dim _Str As String
        Dim _Seq As String = seq

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKIssMiniFG_Detail WHERE FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "' AND FNSeq='" & HI.UL.ULF.rpQuoted(_Seq) & "' "

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

    Private Sub loaddetail(key As String)
        Try
            Dim _Cmd As String = ""
            '_Cmd = "SELECT   FNSeq, FNCartonNo, FTOrderNo, FTSubOrderNo, FTPORefMain FTPORef, D.FNHSysStyleId as FNHSysStyleId_Hide, D.FNHSysSeaSonId as FNHSysSeaSonId_Hide,  FTColorWay, FTSizeBreakDown, FNQuantity   "
            '_Cmd &= vbCrLf & " , S.FTStyleCode as FNHSysStyleId  , e.FTSeasonCode as FNHSysSeaSonIdMain  , FTOrderNo as FTOrderNo_Hide , FTSubOrderNo as FTSubOrderNo_Hide , FTPORefMain as FTPORef_Hide  ,FTColorWay as FTColorWay_Hide, FTSizeBreakDown  as FTSizeBreakDown_Hide"
            '_Cmd &= vbCrLf & " , FTPORefNew , FTPOLineNew  , se.FTSeasonCode as  FNHSysSeaSonId  , D.FTGradeCode , D.FTRcvMiniFgNo , D.FTContinentCode, D.FTCountryCode, D.FTProvinceType"
            '_Cmd &= vbCrLf & " from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".. TPACKIssMiniFG_Detail D with(nolock) "
            '_Cmd &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " .dbo.TMERMStyle S on D.FNHSysStyleId = S.FNHSysStyleId "
            '_Cmd &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " .dbo.TMERMSeason e on d.FNHSysSeaSonIdMain = e.FNHSysSeasonId"
            '_Cmd &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " .dbo.TMERMSeason se on d.FNHSysSeaSonId = se.FNHSysSeasonId"
            '_Cmd &= vbCrLf & " where  D.FTIssMiniFgNo ='" & HI.UL.ULF.rpQuoted(key) & "'"

            _Cmd = " Select  D.FNSeq, FNCartonNo, D.FTOrderNo, D.FTSubOrderNo, FTPORefMain FTPORef, D.FNHSysStyleId As FNHSysStyleId_Hide, D.FNHSysSeaSonId As FNHSysSeaSonId_Hide,  FTColorWay, FTSizeBreakDown, FNQuantity    "
            _Cmd &= vbCrLf & " , S.FTStyleCode as FNHSysStyleId  , e.FTSeasonCode as FNHSysSeaSonIdMain  , D.FTOrderNo as FTOrderNo_Hide , D.FTSubOrderNo as FTSubOrderNo_Hide , FTPORefMain as FTPORef_Hide  ,FTColorWay as FTColorWay_Hide, FTSizeBreakDown  as FTSizeBreakDown_Hide"
            _Cmd &= vbCrLf & " , FTPORefNew , FTPOLineNew  , se.FTSeasonCode as  FNHSysSeaSonId  , D.FTGradeCode , D.FTRcvMiniFgNo , C.FTContinentCode, N.FTCountryCode "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", P.FTProvinceNameTH as FTProvinceType  "
            Else
                _Cmd &= vbCrLf & ",P.FTProvinceNameEN as FTProvinceType "
            End If
            _Cmd &= vbCrLf & " From HITECH_PRODUCTION.. TPACKIssMiniFG_Detail D with(nolock) LEFT Join"
            _Cmd &= vbCrLf & " HITECH_MERCHAN.dbo.TMERTOrderSub AS B WITH (NOLOCK) ON D.FTOrderNo = B.FTOrderNo  And D.FTSubOrderNo = B.FTSubOrderNo INNER Join"
            _Cmd &= vbCrLf & " HITECH_MASTER.dbo.TCNMCountry AS N ON B.FNHSysCountryId = N.FNHSysCountryId INNER Join"
            _Cmd &= vbCrLf & " HITECH_MASTER.dbo.TCNMContinent AS C ON B.FNHSysContinentId = C.FNHSysContinentId INNER Join"
            _Cmd &= vbCrLf & " HITECH_MASTER.dbo.TCMMProvince AS P ON B.FNHSysProvinceId = P.FNHSysProvinceId"
            _Cmd &= vbCrLf & " Left Join HITECH_MASTER .dbo.TMERMStyle S on D.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & " Left Join HITECH_MASTER .dbo.TMERMSeason e on d.FNHSysSeaSonIdMain = e.FNHSysSeasonId"
            _Cmd &= vbCrLf & " Left Join HITECH_MASTER .dbo.TMERMSeason se on d.FNHSysSeaSonId = se.FNHSysSeasonId"
            _Cmd &= vbCrLf & " where  D.FTIssMiniFgNo ='" & HI.UL.ULF.rpQuoted(key) & "'"

            Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveDataDetail(key As String) As Boolean
        Try
            Dim _TableName As String = "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKIssMiniFG"
            Dim _Cmd As String = "" : Dim _oDt As System.Data.DataTable
            Dim _CmdAll As String = ""
            Dim _StrFileH As String = "FTIssMiniFgNo|FNSeq|FNCartonNo|FTOrderNo|FTSubOrderNo|FTPORefMain|FNHSysStyleId|FNHSysSeaSonIdMain|FTColorWay|FTSizeBreakDown|FNQuantity|FTPOLine|FTPORefNew|FTPOLineNew|FNHSysSeasonId"
            Dim _CmdIns As String = "" : Dim _CmdUpd As String = "" : Dim _Value As String = "" : Dim _Where As String = "" : Dim _ValueUpd As String = ""
            Dim _PKey As String = "FTIssMiniFgNo"
            Dim _FKey As String = "FNSeq"
            Dim _FKey2 As String = "FNCartonNo"
            Dim _FKey3 As String = ""
            Dim _FKey4 As String = ""
            Dim _FKey5 As String = ""
            Dim _FKey6 As String = ""
            Dim _Count As Integer = 0

            _Cmd = " Delete From    " & _TableName & " "
            _Cmd &= vbCrLf & "where " & _PKey & "= '" & HI.UL.ULF.rpQuoted(key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _CmdIns = " INSERT INTO   " & _TableName & " "
            _CmdIns &= vbCrLf & "  (FTInsUser, FDInsDate, FTInsTime,  FTIssMiniFgNo, FNSeq, FNCartonNo, FTOrderNo, FTSubOrderNo, FTPORefMain, FNHSysStyleId, FNHSysSeaSonIdMain, FTColorWay, FTSizeBreakDown, 
                  FNQuantity, FTPOLine, FTPORefNew, FTPOLineNew, FNHSysSeasonId)"
            _CmdIns &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ","

            _CmdUpd = " UPDATE  " & _TableName & " "
            _CmdUpd &= vbCrLf & " Set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _CmdUpd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _CmdUpd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy

            End With

            For Each R As DataRow In _oDt.Rows
                _Value = ""
                _ValueUpd = "" : _Where = ""
                For Each _Str As String In _StrFileH.Split("|")
                    If _Value <> "" Then _Value &= ","
                    If Microsoft.VisualBasic.Left(_Str, 2).ToString = "FT" Then
                        If _Str = "FTRcvMiniFgNo" Then
                            _Value &= "'" & HI.UL.ULF.rpQuoted(key) & "'"
                        ElseIf _Str = "FNHSysStyleId" Then

                            Try
                                _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item("FNHSysStyleId_Hide")) & "'"
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try
                        ElseIf _Str = "FNHSysSeaSonId" Then

                            Try
                                _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item("FNHSysSeaSonId_Hide")) & "'"
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try



                        Else
                            _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item(_Str.ToString)) & "'"
                        End If

                    Else
                        If _Str = "FNHSysStyleId" Then
                            Try
                                _Value &= "" & HI.UL.ULF.rpQuoted(R.Item("FNHSysStyleId_Hide")) & ""
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try
                        ElseIf _Str = "FNHSysSeaSonId" Then

                            Try
                                _Value &= "" & HI.UL.ULF.rpQuoted(R.Item("FNHSysSeaSonId_Hide")) & ""
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try



                        Else
                            _Value &= R.Item(_Str.ToString)
                        End If


                    End If

                    If _PKey = _Str Then
                        _Where = "  WHERE " & _PKey & " = '" & key & "'"
                    ElseIf _FKey = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey2 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey2 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey3 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey3 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey4 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey4 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey5 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey5 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey6 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey6 & " = '" & R.Item(_Str.ToString) & "'"
                    Else
                        If _ValueUpd <> "" Then _ValueUpd &= ","
                        If _Str = "FTStateAppCarton" Then
                            _ValueUpd &= _Str & " ='" & R.Item("FTSelect") & "'"
                        Else
                            _ValueUpd &= _Str & " ='" & R.Item(_Str.ToString) & "'"
                        End If

                    End If

                Next
                If _ValueUpd = "" Then
                    _Cmd = _CmdUpd & "  " & _ValueUpd & " " & _Where
                Else

                    _Cmd = _CmdUpd & " , " & _ValueUpd & " " & _Where
                End If

                _Count += +1

                _CmdAll &= vbCrLf
                _CmdAll &= _CmdIns & " " & _Value
                If _Count = 1000 Then
                    If HI.Conn.SQLConn.Execute_Tran(_CmdAll, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                    _Count = 0
                    _CmdAll = ""
                End If



            Next



            If _CmdAll <> "" Then
                If HI.Conn.SQLConn.Execute_Tran(_CmdAll, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If



            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmapprove_Click_1(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            If Me.FTIssMiniFgNo.Text <> "" Then

                If Me.FTStateApp.Checked = False Then

                    Dim _Cmd As String = ""
                    _Cmd = "Update  a"
                    _Cmd &= vbCrLf & " set a.FTStateApp='1'"
                    _Cmd &= vbCrLf & " , a.FTInsUserApp= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ,  a.FDDateApp=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & " ,  a.FTTimeApp=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKIssMiniFG a "
                    _Cmd &= vbCrLf & " where  FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    Me.FTStateApp.Checked = True
                    HI.MG.ShowMsg.mInfo("อนุมัติเอกสารเรียบร้อยแล้ว...", 2210051531, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Try
            If Me.FTIssMiniFgNo.Text <> "" Then

                If Me.FTStateApp.Checked Then

                    Dim _Cmd As String = ""
                    _Cmd = "Update  a"
                    _Cmd &= vbCrLf & " set a.FTStateApp='0'"
                    _Cmd &= vbCrLf & " , a.FTInsUserApp= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ,  a.FDDateApp=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & " ,  a.FTTimeApp=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKIssMiniFG a "
                    _Cmd &= vbCrLf & " where  FTIssMiniFgNo='" & HI.UL.ULF.rpQuoted(Me.FTIssMiniFgNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    Me.FTStateApp.Checked = False
                    HI.MG.ShowMsg.mInfo("ยกเลิกอนุมัติเอกสารเรียบร้อยแล้ว...", 2210051532, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
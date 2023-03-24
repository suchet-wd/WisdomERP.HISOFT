Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports outlook = Microsoft.Office.Interop.Outlook

Public Class wPurchaseOrder

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wAddItemPO
    Private _RevisedPopup As wPurchaseReviseRemark
    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call PrepareForm()

        _AddItemPopup = New wAddItemPO
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try

        _RevisedPopup = New wPurchaseReviseRemark
        HI.TL.HandlerControl.AddHandlerObj(_RevisedPopup)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _RevisedPopup.Name.ToString.Trim, _RevisedPopup)
        Catch ex As Exception
        Finally
        End Try

    End Sub

#Region "Property"

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

        '------ Get Form Object ID-------------------
        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM  HSysTableObjForm WITH(NOLOCK) "
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
            _Str &= vbCrLf & "  FROM HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

            '------ Get Form Object Gen Grid-------------------
            _Str = " EXEC SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

            If _dt.Rows.Count > 0 Then

                For Each Row As DataRow In _dtgrpobj.Rows
                    Select Case Row!FNGenFormObj.ToString
                        Case "H"
                            Dim _ch As New HI.TL.DynamicForm(_objId, Val(Row!FNFormObjID.ToString), _dt, Me)
                            _ch.SysObjID = Val(Row!FNFormObjID.ToString)
                            _ch.SysTableName = Row!FTTableName.ToString
                            _ch.SysDBName = Row!FTBaseName.ToString
                            _FormHeader.Add(_ch)
                        Case "G"
                            'Dim _gd As New HI.TL.DynamicGrid(_objId, Val(Row!FNFormObjID.ToString), _dt, ogvdetail)
                            '_gd.SysObjID = Val(Row!FNFormObjID.ToString)
                            '_gd.SysTableName = Row!FTTableName.ToString
                            '_gd.SysDBName = Row!FTBaseName.ToString
                            '_FormGridDetail.Add(_gd)
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
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = R.Item(Col).ToString
                            End With

                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                            Obj.Text = R.Item(Col).ToString
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString) ' hImage ' ' Image.FromFile("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            Try
                                If Obj.Name.ToString = "FDSampleAppDate" Then
                                    Beep()
                                End If
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

        Call LoadPoDetail(Key.ToString)

        Me.oxtb.SelectedTabPageIndex = 0
        _ProcLoad = False
    End Sub

    Private Sub LoadPoDetail(PoKey As String)
        Dim _Str As String = ""
        _Str = " SELECT        D.FNHSysRawMatId, M.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTRawMatNameTH AS FTMatDesc"
        Else
            _Str &= vbCrLf & " , M.FTRawMatNameEN AS FTMatDesc"
        End If

        _Str &= vbCrLf & "  , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt, D.FTOrderNo, D.FNQuantity, D.FNNetAmt, D.FTRemark"
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS D WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId"
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & " WHERE        (D.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FTRawMatSizeCode, D.FTOrderNo "

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        _Str = " SELECT        D.FNHSysRawMatId, M.FTRawMatCode"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , MAX(M.FTRawMatNameTH) AS FTMatDesc"
        Else
            _Str &= vbCrLf & " , MAX(M.FTRawMatNameEN) AS FTMatDesc"
        End If

        _Str &= vbCrLf & " , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt, SUM(D.FNQuantity) AS  FNQuantity, SUM(D.FNNetAmt) AS FNNetAmt, MAX(D.FTRemark) AS FTRemark"
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS D WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId"
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & " WHERE        (D.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        _Str &= vbCrLf & " GROUP BY     D.FNHSysRawMatId, M.FTRawMatCode,C.FTRawMatColorCode, S.FTRawMatSizeCode,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt"
        _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FTRawMatSizeCode "

        Me.ogcsum.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

    End Sub

    Public Sub DefaultsData()
        Dim _FieldName As String
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).DefaultsData.ToArray.Count - 1
                _FieldName = _FormHeader(cind).DefaultsData(I).FiledName.ToString

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString

                                HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)

                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)

                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                .SelectedIndex = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)
                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Checked = (_FormHeader(cind).DefaultsData(I).DataDefaults.ToString = "1")
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)

                                Try
                                    .DateTime = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                                Catch ex As Exception
                                    .Text = ""
                                End Try

                            End With
                            'Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            '    With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            '        If .Image Is Nothing Then
                            '            Pass = False
                            '        End If
                            '    End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case "DevExpress.XtraEditors.TextEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case Else
                    End Select
                Next
            Next
        Next

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
                    If ObjCaption.GetType.FullName.ToString.ToUpper = "DevExpress.XtraEditors.LabelControl".ToUpper Then
                        _Caption = ObjCaption.Text
                        Exit For
                    End If
                Next

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Properties.Buttons.Count <= 1 Then
                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                        Pass = False
                                    End If
                                End If
                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                If Val(.Value.ToString) <= 0 Then
                                    Pass = False
                                End If
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If .SelectedIndex < 0 Then Pass = False
                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            'With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            '    If Val(.Value.ToString) <= 0 Then
                            '        Pass = False
                            '    End If
                            'End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                    Pass = False
                                End If
                            End With
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                If .Image Is Nothing Then
                                    Pass = False
                                End If
                            End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
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
                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else

                                Dim _CmpH As String = ""
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case ctrl.GetType.FullName.ToString.ToUpper
                                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case "DevExpress.XtraEditors.TextEdit".ToUpper
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

    Private Sub ClearSourcing(Optional OrderNo As String = "", Optional RawmatID As Integer = 0)
        Dim _Qry As String = ""

        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
        _Qry &= vbCrLf & " SET FTPurchaseNo='' "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "

        If OrderNo <> "" Then
            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
            _Qry &= vbCrLf & " AND FNHSysRawMatId=" & RawmatID & " "
        End If

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

    End Sub

    Private Function SaveData(Optional RevisedRemark As String = "") As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False

        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else


                                Dim _CmpH As String = ""
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case ctrl.GetType.FullName.ToString.ToUpper
                                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case "DevExpress.XtraEditors.TextEdit".ToUpper
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
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, FNHSysCmpRunId.Text & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex)).ToString
            Me.FTPurchaseState.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & " MANUAL " & HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)) & " " & Format(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM), "HH:mm:ss")
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
                                Select Case Obj.GetType.FullName.ToString.ToUpper
                                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper, "DevExpress.XtraEditors.DateEdit".ToUpper
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
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
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
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

                                Select Case Obj.GetType.FullName.ToString.ToUpper
                                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper, "DevExpress.XtraEditors.DateEdit".ToUpper
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
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
                                Case UCase("FDUpdDate"), UCase("FTUpdDate")
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
                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next

            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
            _Str &= vbCrLf & "  SET FTStateSendApp='0' "
            _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
            _Str &= vbCrLf & "  ,FTSuperVisorName='' "
            _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
            _Str &= vbCrLf & "  ,FTSuperManagerName='' "
            _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            If RevisedRemark <> "" Then


                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised ( "
                _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                _Str &= vbCrLf & " , FTPurchaseNo, FNRevisedSeq, FTPurchaseRevisedBy"
                _Str &= vbCrLf & ", FTRevisedDate, FTRevisedTime, FTNote"
                _Str &= vbCrLf & ")"
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                _Str &= vbCrLf & ", ISNULL(("
                _Str &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
                _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                _Str &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                _Str &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
                _Str &= vbCrLf & "),0) +1 "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(RevisedRemark) & "'"

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

            'Try
            '    Dim _Qry As String = ""
            '    _Qry = "Select  TOP 1 CASE WHEN ISNULL(FTStateSuperVisorApp,'0') ='1'  AND ISNULL(FTStateManagerApp,'0') ='1' THEN '1' Else '' END AS  FTStateApp  "
            '    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            '    _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            '    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1" Then
            '        _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
            '        _Qry &= vbCrLf & "  SET FTStateSendApp='0' "
            '        _Qry &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
            '        _Qry &= vbCrLf & "  ,FTStateManagerApp='0' "
            '        _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)
            '    End If
            'Catch ex As Exception
            'End Try

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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

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
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                '.Properties.Tag = R.Item(Col).ToString
                                .Text = R.Item(Col).ToString
                                Call HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)
                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                            Obj.Text = R.Item(Col).ToString
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString) ' hImage ' ' Image.FromFile("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
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
            Select Case Obj.GetType.FullName.ToString.ToUpper
                Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
    End Sub

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If

        If CheckOwner() = False Then Exit Sub
        If Me.VerrifyData Then
            Dim _Qry As String = ""
            Dim _RevisedRemark As String = ""
            _Qry = "SELECT TOP 1  FTStateSuperVisorApp "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1" Then

                With _RevisedPopup
                    .FTRemark.Text = ""
                    .StateProc = False
                    .ocmok.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()

                    If .StateProc = False Then
                        Exit Sub
                    Else

                        _RevisedRemark = .FTRemark.Text.Trim()

                    End If
                End With
            End If

            If Me.SaveData(_RevisedRemark) Then
                FTStateSendApp.Checked = False
                FTStateSuperVisorApp.Checked = False
                FTStateManagerApp.Checked = False
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If
        If CheckOwner() = False Then Exit Sub
        If Me.DeleteData() Then
            Call ClearSourcing()
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.DefaultsData()
            Me.FTPurchaseNo.Focus()
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then
            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If Me.FNPoState.SelectedIndex = 0 Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseOrder.rpt"
                .AddParameter("Draft", "DRAFT")
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub ocmprint_Click(sender As Object, e As EventArgs) Handles ocmprint.Click
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then
            With New HI.RP.Report


                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If Me.FNPoState.SelectedIndex = 0 Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseOrder.rpt"
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang

                Dim Qry As String
                Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET FTStatePrint='1'"
                Qry &= vbCrLf & ",FTPrintBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                Qry &= vbCrLf & ",FTPrintDate=" & HI.UL.ULDate.FormatDateDB & " "
                Qry &= vbCrLf & ",FTPrintTime=" & HI.UL.ULDate.FormatTimeDB & " "

                HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_PUR)
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "


    Private Sub LoadPriceHistory()
        Dim _Qry As String = ""
        Dim dt As DataTable
        _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_Purchase_History_ListPrice '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'," & HI.ST.Lang.Language & " "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        Me.ogcpricehistory.DataSource = dt.Copy
        dt.Dispose()

        Call InitialGridMergCell()

    End Sub

    Private Sub LoadRevisedHistory()
        Dim _Qry As String = ""
        Dim dt As DataTable

        _Qry = "SELECT FNRevisedSeq, FTPurchaseRevisedBy"
        _Qry &= vbCrLf & " ,CASE WHEN ISDATE(FTRevisedDate) = 1 THEN  Convert(nvarchar(10), Convert(datetime,FTRevisedDate) ,103)  ELSE '' END AS FTRevisedDate "
        _Qry &= vbCrLf & " , FTRevisedTime, FTNote "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq ASC "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        Me.ogcrevised.DataSource = dt.Copy
        dt.Dispose()

    End Sub

    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvpricehistory.Columns

            Select Case c.FieldName.ToString
                Case "FTPurchaseNo", "FTRawmatCode", "FTRawMatName", "FTRawMatColorCode", "FTRawMatSizeCode", "FTUnitCode", "FNNetPrice", "FTCurCode"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Sub ogvmrp_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvpricehistory.CellMerge
        Try
            With Me.ogvpricehistory
                If .GetRowCellValue(e.RowHandle1, "FTRawMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatCode").ToString Then

                    If e.Column.FieldName = "FNNetPrice" Then
                        If .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId2").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Else
                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                    End If

                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Variable "

#End Region

    Private Sub Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click
        If CheckOwner() = False Then Exit Sub
        Dim _CmpH As String = ""
        For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

            Select Case ctrl.GetType.FullName.ToString.ToUpper
                Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                    End With

                    Exit For
                Case "DevExpress.XtraEditors.TextEdit".ToUpper
                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                    End With

                    Exit For
            End Select
        Next

        If FTPurchaseNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTPurchaseNo.Text = "" Then Exit Sub
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If

        With _AddItemPopup
            .AddMat = False
            .PONO = FTPurchaseNo.Text
            Call HI.ST.Lang.SP_SETxLanguage(_AddItemPopup)
            HI.TL.HandlerControl.ClearControl(_AddItemPopup)
            .FTOrderNo.Properties.ReadOnly = False
            .FTOrderNo.Properties.Buttons(0).Enabled = True
            .ShowDialog()

            If (.AddMat) Then
                Dim _Str As String = ""

                _Str = "SELECT TOP 1 FTPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK) "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                If HI.Conn.SQLConn.GetField(_Str, _DBEnum, "") = "" Then

                    _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                    _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize)"
                    _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & "," & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                    _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                    _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                    _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                    _Str &= vbCrLf & "," & .FNNetAmt.Value & " "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "

                Else

                    _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                    _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                    _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                    _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                    _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                    _Str &= vbCrLf & " ,FNNetAmt=" & .FNNetAmt.Value & ""
                    _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                    _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                End If

                Try
                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Exit Sub
                    End If

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                    _Str &= vbCrLf & " SET FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                    _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                    _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                    _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                    _Str &= vbCrLf & " ,FNNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * " & (.FNPOPrice.Value - .FNDisAmt.Value) & " )"
                    _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                    _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                    _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " AND ISNULL(FTPurchaseNo,'') =''  "
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                    _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                    _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice - FNDisAmt))) AS NETAMT"
                    _Str &= vbCrLf & "    FROM"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity"
                    _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt) AS A"

                    Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))



                    Me.SaveData()

                    Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                End Try
            End If
        End With
    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNExchangeRate.EditValueChanged
        If FNExchangeRate.Value <= 0 Then FNExchangeRate.Value = 1
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                  FNPoAmt.EditValueChanged,
                                                                                  FNDisCountAmt.EditValueChanged,
                                                                                  FNVatPer.EditValueChanged,
                                                                                  FNVatAmt.EditValueChanged,
                                                                                  FNSurcharge.EditValueChanged

        Static _Proc As Boolean

        If Not (_Proc) And Not (_ProcLoad) Then
            _Proc = True
            Dim _POAmt As Double = FNPoAmt.Value

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

            Me.FNPONetAmt.Value = (_POAmt - _DisAmt)

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)

            _Proc = False
        End If
    End Sub

    Private Sub ocmremove_Click(sender As System.Object, e As System.EventArgs) Handles ocmremove.Click
        If CheckOwner() = False Then Exit Sub
        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
            Dim _JobNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString
            Dim _Amount As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNNetAmt").ToString

            If (CheckReceive(Me.FTPurchaseNo.Text, _MatID) = False) Then Exit Sub

            Dim _Str As String = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_JobNo) & "' AND FNHSysRawMatId=" & Val(_MatID) & " "

            If HI.Conn.SQLConn.ExecuteOnly(_Str, _DBEnum) = True Then

                Call ClearSourcing(_JobNo, Integer.Parse(Val(_MatID)))

                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Str, _DBEnum)

                _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice - FNDisAmt))) AS NETAMT"
                _Str &= vbCrLf & "    FROM"
                _Str &= vbCrLf & " ("
                _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt) AS A"

                Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                Me.SaveData()
                Me.LoadPoDetail(Me.FTPurchaseNo.Text)

            End If
        End With

    End Sub

    Private Sub FNDocNetAmt_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNPOGrandAmt.EditValueChanged

        If Not (_ProcLoad) Then
            Me.FTPOGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNPOGrandAmt.Value)
            Me.FTPOGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNPOGrandAmt.Value)
        End If

    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Function CheckReceive(POKey As String, Optional SysMatId As Integer = 0, Optional ShowMsg As Boolean = True) As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""

        If SysMatId = 0 Then
            _Str = "Select TOP 1 FTPurchaseNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As R WITH(NOLOCK) WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                If ShowMsg Then
                    HI.MG.ShowMsg.mProcessError(1303150001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                End If

                _Pass = False
            End If

        Else
            _Str = "Select TOP 1 H.FTPurchaseNo "
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As H WITH(NOLOCK), [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS D "
            _Str &= vbCrLf & " WHERE H.FTReceiveNo= D.FTReceiveNo AND H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "
            _Str &= vbCrLf & " AND FNHSysRawMatId=" & SysMatId & ""
            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                If ShowMsg Then
                    HI.MG.ShowMsg.mProcessError(1401260001, "พบการรับ Item นี้แล้ว ", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                End If

                _Pass = False
            End If
        End If

        Return _Pass
    End Function

    Private Sub FNHSysCurId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCurId.EditValueChanged
        If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then
            FNExchangeRate.Properties.ReadOnly = True

            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 1
            End If

        Else
            FNExchangeRate.Properties.ReadOnly = False
            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 1
            End If
        End If
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As System.EventArgs) Handles ogvdetail.DoubleClick
        With ogvdetail

            Dim _CheckRcv As Boolean = False
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim SysMatId As Integer = Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString)
            If (CheckReceive(Me.FTPurchaseNo.Text, SysMatId) = False) Then Exit Sub
            If (CheckReceive(Me.FTPurchaseNo.Text, , False) = False) Then
                _CheckRcv = True
            End If

            If CheckOwner() = False Then Exit Sub
            With _AddItemPopup
                .AddMat = False
                .PONO = FTPurchaseNo.Text

                HI.TL.HandlerControl.ClearControl(_AddItemPopup)

                .FTOrderNo.Text = "" & ogvdetail.GetFocusedRowCellValue("FTOrderNo").ToString

                Dim ItemCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatCode").ToString
                Dim ColorCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatColorCode").ToString
                Dim SizeCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatSizeCode").ToString

                .FTRawMatColorCode.Text = ColorCode
                .FTRawMatSizeCode.Text = SizeCode
                .FNHSysRawMatId.Text = ItemCode

                ' HI.TL.HandlerControl.DynamicButtonediHSysKey_Leave(.FNHSysRawMatId, Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                ' HI.TL.HandlerControl.DynamicButtonediHSysKey_Leave(.FNHSysUnitIdPO, Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysUnitId").ToString))
                .FNHSysUnitIdPO.Text = "" & ogvdetail.GetFocusedRowCellValue("FTUnitCode").ToString
                .FNPOPrice.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNPrice").ToString)
                .FNDisPer.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNDisPer").ToString)

                .FNPOQuantity.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNQuantity").ToString)
                .FTRemark.Text = "" & ogvdetail.GetFocusedRowCellValue("FTRemark").ToString
                .FTFabricFrontSize.Text = "" & ogvdetail.GetFocusedRowCellValue("FTFabricFrontSize").ToString
                .FTOrderNo.Properties.ReadOnly = _CheckRcv
                .FTOrderNo.Properties.Buttons(0).Enabled = Not (_CheckRcv)
                .ShowDialog()

                If (.AddMat) Then
                    Dim _Str As String = ""

                    _Str = "SELECT TOP 1 FTPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK) "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                    If HI.Conn.SQLConn.GetField(_Str, _DBEnum, "") = "" Then

                        _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                        _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize)"
                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & "," & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                        _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                        _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                        _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                        _Str &= vbCrLf & "," & .FNNetAmt.Value & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "

                    Else

                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt=" & .FNNetAmt.Value & ""
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                    End If

                    Try
                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Exit Sub
                        End If

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                        _Str &= vbCrLf & " SET FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * " & (.FNPOPrice.Value - .FNDisAmt.Value) & " )"
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                        _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " AND ISNULL(FTPurchaseNo,'') =''  "
                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                        _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                        _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                        _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                        _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                        _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * (FNPrice - FNDisAmt))) AS NETAMT"
                        _Str &= vbCrLf & "    FROM"
                        _Str &= vbCrLf & " ("
                        _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity"
                        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt) AS A"

                        Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                        Me.SaveData()

                        Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                    Catch ex As Exception

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    End Try
                End If
            End With
        End With
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then

            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateSendApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

            End If
            FTStateSendApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub oxtb_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles oxtb.SelectedPageChanging
        Select Case e.Page.Name
            Case otppurchasepricehistory.Name
                Call LoadPriceHistory()
            Case otprevisedhistory.Name
                Call LoadRevisedHistory()
            Case Else

        End Select
    End Sub

    Private Sub ogvpricehistory_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvpricehistory.RowCellStyle
        Try
            With Me.ogvpricehistory

                Try
                    If "" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString <> "" Then
                        If IsNumeric("" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString) Then
                            If .GetRowCellValue(e.RowHandle, "FNHSysRawMatId2").ToString = .GetRowCellValue(e.RowHandle, "FNHSysRawMatId").ToString Then
                                If Not (e.Appearance.ForeColor = System.Drawing.Color.Red) Then
                                    If CDbl("" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString) < CDbl("" & .GetRowCellValue(e.RowHandle, "FNNetPrice").ToString) Then
                                        e.Appearance.ForeColor = System.Drawing.Color.Red
                                    Else
                                        e.Appearance.ForeColor = System.Drawing.Color.Green
                                    End If
                                End If
                            End If

                        End If
                    End If
                Catch ex As Exception

                End Try
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPurchaseNo.EditValueChanged

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo(FTPurchaseNo.Properties.Tag.ToString)
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged

    End Sub

    Private Sub ogbmainprocbutton_Paint(sender As Object, e As PaintEventArgs) Handles ogbmainprocbutton.Paint

    End Sub

    Private Sub ocmmail_Click(sender As Object, e As EventArgs) Handles ocmmail.Click

        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then

            Dim _FTMail As String = ""
            Dim _Sql As String = ""

            _Sql = "SELECT TOP 1 FTMail "
            _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK) "
            _Sql &= vbCrLf & " WHERE FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
            _FTMail = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_MASTER, "")

            Dim _Spls As New HI.TL.SplashScreen("Creating....Mail Please Wait.")
            Try


                Dim OutlookMessage As outlook.MailItem
                Dim AppOutlook As New outlook.Application
                Dim objNS As outlook._NameSpace = AppOutlook.Session
                Dim objFolder As outlook.MAPIFolder
                objFolder = objNS.GetDefaultFolder(outlook.OlDefaultFolders.olFolderDrafts)

                Try
                    OutlookMessage = AppOutlook.CreateItem(outlook.OlItemType.olMailItem)

                    With OutlookMessage
                        .To = _FTMail
                        .CC = ""
                        .Subject = FTPurchaseNo.Text
                        .Body = Me.FTRemark.Text

                        Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                        Try
                            With New HI.RP.Report

                                If Me.FNPoState.SelectedIndex = 0 Then
                                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                                Else
                                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                                End If

                                .FormTitle = Me.Text
                                .ExportFile = RP.Report.ExFile.PDF
                                .ExportName = FTPurchaseNo.Text


                                Try
                                    If Directory.Exists("C:\HISOFTPDF") = False Then
                                        Directory.CreateDirectory("C:\HISOFTPDF")
                                    End If
                                Catch ex As Exception

                                End Try
                                .PathExport = "C:\HISOFTPDF"
                                .ReportFolderName = "PurchaseOrder\"
                                .ReportName = "PurchaseOrder.rpt"
                                .AddParameter("Draft", "DRAFT")
                                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                                .PrevieNoSplash()

                            End With
                        Catch ex As Exception

                        End Try
                        HI.ST.Lang.Language = _tmplang

                        Try
                            .Attachments.Add("C:\HISOFTPDF\" & FTPurchaseNo.Text & ".pdf")
                        Catch ex As Exception
                        End Try
                        _Spls.Close()
                        .Display(True)
                    End With

                    Try
                        If File.Exists("C:\HISOFTPDF\" & FTPurchaseNo.Text & ".pdf") = True Then
                            File.Delete("C:\HISOFTPDF\" & FTPurchaseNo.Text & ".pdf")
                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("เนื่องจากพบข้อผิดพลาดบางประการ ระบบจึงไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280001, Me.Text, , MessageBoxIcon.Warning)
                Finally
                    OutlookMessage = Nothing
                    AppOutlook = Nothing
                End Try



            Catch ex As Exception
                _Spls.Close()
                HI.MG.ShowMsg.mInfo("ไม่พบ Microsoft Outlook ไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280002, Me.Text, , MessageBoxIcon.Warning)
            End Try


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

End Class
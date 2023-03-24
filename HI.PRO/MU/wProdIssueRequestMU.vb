Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns

Public Class wProdIssueRequestMU

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _AddProdRequestItem As wProdIssueRequestAddItemMU
    Private _ProcLoad As Boolean = False
    Private cmpnotchk As Long = 0
    Private _AllDocNo As String = ""
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        'With ogvdetail
        '    .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
        '    .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
        '    .OptionsView.ShowFooter = True
        'End With

        _AddProdRequestItem = New wProdIssueRequestAddItemMU
        HI.TL.HandlerControl.AddHandlerObj(_AddProdRequestItem)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddProdRequestItem.Name.ToString.Trim, _AddProdRequestItem)
        Catch ex As Exception
        Finally
        End Try


        Dim cmdstring As String = "select top 1 FTCfgData from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X With(Nolock) where FTCfgName ='cfgconcmpautoissnotcheck'"

        cmpnotchk = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, ""))

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

    Private _FormName As String = "wProdIssueRequest"
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
        _Str &= vbCrLf & " WHERE FTDynamicFormName='wProdIssueRequest'" ' & HI.UL.ULF.rpQuoted(Me.Name) & "' "
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

            '
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
                If _FieldName = "FTOrderNo" Then
                    _FieldName = "FTGrpOrderNo"

                End If


                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                '.Properties.Tag = R.Item(Col).ToString


                                .Text = R.Item(Col).ToString

                                ' Call HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)
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

        Call LoadIssueDetail(Key.ToString)

        ' Call LoadPOInfo(FTPurchaseNo.Text, True)

        _ProcLoad = False
    End Sub

    Private Sub LoadIssueDetail(PoKey As String)

        Dim _Qry As String = ""

        _Qry = "  SELECT A.FTIssueReqNo, A.FTOrderProdNo, A.FNHSysMarkId, A.FNTableNo, A.FNHSysRawMatId, A.FNReqQuantity, IM.FTRawMatCode, ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode, "
        _Qry &= vbCrLf & " ISNULL(IMS.FTRawMatSizeCode,'') AS FTRawMatSizeCode, IMU.FTUnitCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",M.FTMarkCode + '::'+ M.FTMarkNameTH  AS FTMarkName"
            _Qry &= vbCrLf & ",IM.FTRawMatNameTH AS FTRawMatName"
        Else
            _Qry &= vbCrLf & ",M.FTMarkCode + '::'+ M.FTMarkNameEN  AS FTMarkName"
            _Qry &= vbCrLf & ",IM.FTRawMatNameEN AS FTRawMatName"
        End If

        _Qry &= vbCrLf & ",ISNULL(XXA.FTRawMatColorNameEN ,'') AS FTRawMatColorName"

        _Qry &= vbCrLf & ", [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.fn_IssueNo_OfRequestIss(A.FTIssueReqNo,A.FNHSysRawMatId) AS FTIssueNo"
        _Qry &= vbCrLf & "   "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq AS HHA WITH(NOLOCK) INNER JOIN"

        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail AS A WITH(NOLOCK) ON HHA.FTIssueReqNo=A.FTIssueReqNo INNER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)  ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M WITH(NOLOCK)  ON A.FNHSysMarkId = M.FNHSysMarkId INNER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS IMU  WITH(NOLOCK) ON IM.FNHSysUnitId = IMU.FNHSysUnitId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK)  ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK)  ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"

        _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
        _Qry &= vbCrLf & " 	SELECT XA.FTOrderNo, XB.FTMainMatCode, XA.FNHSysRawMatColorId, XA.FTRawMatColorNameTH, XA.FTRawMatColorNameEN"
        _Qry &= vbCrLf & " 	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS XA WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS XB WITH(NOLOCK) ON XA.FNHSysMainMatId = XB.FNHSysMainMatId"

        _Qry &= vbCrLf & " ) AS XXA ON HHA.FTOrderNo = XXA.FTOrderNo AND IM.FTRawMatCode = XXA.FTMainMatCode AND IM.FNHSysRawMatColorId = XXA.FNHSysRawMatColorId "



        _Qry &= vbCrLf & "   WHERE A.FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "' "
        _Qry &= vbCrLf & "  ORDER BY IM.FTRawMatCode, ISNULL(IMC.FTRawMatColorCode,''),ISNULL(IMS.FTRawMatSizeCode,''),A.FTOrderProdNo,M.FTMarkCode,A.FNTableNo"

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    End Sub

    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvdetail.Columns

            Select Case c.FieldName.ToString
                Case "FTIssueNo"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

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
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, FNMatType.SelectedIndex.ToString, False, _CmpH).ToString
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
                    If _FieldName = "FTOrderNo" Then
                        _FieldName = "FTGrpOrderNo"

                    End If
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

                            If _FieldName = "FTGrpOrderNo" Then
                                _Fields &= "FTOrderNo"
                            Else
                                _Fields &= _FieldName
                            End If


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
                                            If _FieldName = "FTGrpOrderNo" Then
                                                _Values &= " FTOrderNo='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                            Else
                                                _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                            End If


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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail_Barcode WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "'")

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

    Private Function AutoIssueAccessory() As Boolean

        Dim _Qry As String = ""

        Dim _dt As DataTable
        Dim _dtItem As DataTable
        Dim _dtwh As DataTable
        Dim _StateIssue As Boolean = False
        Dim _FNHSysWHId As Integer = 0
        Dim _FNHSysRawMatId As Integer = 0
        Dim _IssueNo As String = ""
        Dim _FNReqQuantity As Double = 0
        Dim _FNIssQuantity As Double = 0
        Dim _CmpH As String = ""
        Dim _BarCode As String = ""

        Try

            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            _Qry = " SELECT FNHSysRawMatId, SUM(FNReqQuantity) AS FNReqQuantity"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  (FTIssueReqNo = N'" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "')"
            _Qry &= vbCrLf & " GROUP BY FNHSysRawMatId"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            Dim OrcerCmpId As Integer = 0

            _Qry = "SELECT TOP 1 FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X WITH(NOLOCK) WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(FTGrpOrderNo.Text) & "' "
            OrcerCmpId = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0"))

            '_Qry = "  SELECT A.FNHSysWHId "
            '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS A WITH(NOLOCK)"
            '_Qry &= vbCrLf & "  INNER JOIN (SELECT TOP 1 FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X WITH(NOLOCK) WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') AS X ON A.FNHSysCmpId = X.FNHSysCmpId "
            '_Qry &= vbCrLf & " WHERE ISNULL(A.FTStateActive,'')='1' "
            '_Qry &= vbCrLf & " ORDER BY ISNULL(A.FTStateCenter,'')"


            _Qry = "   Select  FTWHCode,FNHSysWHId "
            _Qry &= vbCrLf & "   FROM( "
            _Qry &= vbCrLf & "  SELECT FTWHCode, FNHSysWHId,FTStateCenter "
            _Qry &= vbCrLf & "  FROM "
            _Qry &= vbCrLf & " (  SELECT   FTWHCode,  FNHSysWHId, FNHSysCmpId, FTStateActive, ISNULL(FTStateCenter,'') AS FTStateCenter FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse With ( NOLOCK )    WHERE FNHSysCmpId = " & OrcerCmpId & " AND ISNULL(FTStateActive,'')='1'   AND ISNULL(FNHSysCmpManageId,0)=0 "
            _Qry &= vbCrLf & "    UNION "
            _Qry &= vbCrLf & "    Select  FTWHCode,FNHSysWHId,FNHSysCmpManageId As FNHSysCmpId,FTStateActive, ISNULL(FTStateCenter,'') AS FTStateCenter  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse  With ( NOLOCK ) WHERE ISNULL(FNHSysCmpManageId,0)= " & OrcerCmpId & "  AND ISNULL(FTStateActive,'')='1'  AND ISNULL(FNHSysCmpManageId,0) >0 "
            _Qry &= vbCrLf & "  ) AS X "
            _Qry &= vbCrLf & " GROUP BY FTWHCode, FNHSysWHId,FTStateCenter "
            _Qry &= vbCrLf & "   ) AS H  "
            _Qry &= vbCrLf & " ORDER BY ISNULL(FTStateCenter,'') "

            _dtwh = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            If _dtwh.Rows.Count > 0 Then

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    For Each R As DataRow In _dtwh.Rows
                        _FNHSysWHId = Integer.Parse(Val(R!FNHSysWHId.ToString))
                        _IssueNo = ""

                        _dt.BeginInit()

                        For Each Rx As DataRow In _dt.Select("FNReqQuantity>0")

                            _FNHSysRawMatId = Integer.Parse(Val(Rx!FNHSysRawMatId.ToString))
                            _FNReqQuantity = Double.Parse(Val(Rx!FNReqQuantity.ToString))

                            _Qry = "SELECT FTBarcodeNo"
                            _Qry &= vbCrLf & "   ,FNHSysWHId"
                            _Qry &= vbCrLf & "    ,FTOrderNo"
                            _Qry &= vbCrLf & "   ,FNQuantity"
                            _Qry &= vbCrLf & "   ,FNQuantityBal"
                            _Qry &= vbCrLf & "   ,FTPurchaseNo"
                            _Qry &= vbCrLf & "   ,FTDocumentNo"
                            _Qry &= vbCrLf & "    FROM"
                            _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
                            _Qry &= vbCrLf & "   ,FNHSysWHId"
                            _Qry &= vbCrLf & "   ,FTOrderNo"
                            _Qry &= vbCrLf & "    ,FNQuantity"
                            _Qry &= vbCrLf & "   ,FNQuantity -  "
                            _Qry &= vbCrLf & " 	ISNULL(("
                            _Qry &= vbCrLf & " 	SELECT SUM(FNQuantity) AS FNQuantity"
                            _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
                            _Qry &= vbCrLf & "   WHERE FTBarcodeNo = A.FTBarcodeNo"
                            _Qry &= vbCrLf & " 	AND FNHSysWHId = A.FNHSysWHId"
                            _Qry &= vbCrLf & " 	AND FTOrderNo =  A.FTOrderNo"
                            _Qry &= vbCrLf & " 	AND FTDocumentRefNo =  A.FTDocumentNo"
                            _Qry &= vbCrLf & " 	),0)"
                            _Qry &= vbCrLf & "     AS  FNQuantityBal"
                            _Qry &= vbCrLf & "    ,FTPurchaseNo"
                            _Qry &= vbCrLf & "    ,FTDocumentNo"
                            _Qry &= vbCrLf & "      FROM"
                            _Qry &= vbCrLf & " 	(   SELECT BI.FTBarcodeNo,BI.FNHSysWHId,BI.FTOrderNo,SUM(BI.FNQuantity) AS FNQuantity,B.FTPurchaseNo,BI.FTDocumentNo ,BI.FNHSysCmpId"
                            _Qry &= vbCrLf & " 	FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH(NOLOCK)"
                            _Qry &= vbCrLf & " 	 , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
                            _Qry &= vbCrLf & " WHERE BI.FTBarcodeNo = B.FTBarcodeNo"
                            _Qry &= vbCrLf & "    AND BI.FNHSysWHId =" & _FNHSysWHId & ""
                            _Qry &= vbCrLf & "    AND BI.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTGrpOrderNo.Text) & "'"
                            _Qry &= vbCrLf & "    AND B.FNHSysRawMatId =" & _FNHSysRawMatId & ""
                            _Qry &= vbCrLf & "    GROUP BY BI.FTBarcodeNo,BI.FNHSysWHId,BI.FTOrderNo,B.FTPurchaseNo,BI.FTDocumentNo ,BI.FNHSysCmpId"
                            _Qry &= vbCrLf & " 	) AS A ) AS M"
                            _Qry &= vbCrLf & "    WHERE FNQuantityBal > 0"

                            _dtItem = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                            For Each RxI As DataRow In _dtItem.Rows

                                _FNIssQuantity = Double.Parse(Val(RxI!FNQuantityBal.ToString))
                                _BarCode = RxI!FTBarcodeNo.ToString
                                Me.DocRefNo = RxI!FTDocumentNo.ToString

                                If _IssueNo = "" Then

                                    _IssueNo = HI.TL.Document.GetDocumentNo("HITECH_INVENTORY", "TINVENIssue", "", False, _CmpH & "A").ToString()

                                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue"
                                    _Qry &= vbCrLf & " ("
                                    _Qry &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTIssueNo, FDIssueDate, FTIssueBy"
                                    _Qry &= vbCrLf & " , FNHSysIssueSectId, FNHSysWHId, FTOrderNo, FTRemark, FNHSysCmpId, FTIssueReqNo"
                                    _Qry &= vbCrLf & " )"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_IssueNo) & "'"
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & " ,1408060001"
                                    _Qry &= vbCrLf & " ," & _FNHSysWHId & ""
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTGrpOrderNo.Text) & "'"
                                    _Qry &= vbCrLf & " ,'Auto Issue From Production'"
                                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "'"


                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Return False
                                    End If

                                End If

                                If _FNIssQuantity >= _FNReqQuantity Then
                                    _FNIssQuantity = _FNReqQuantity
                                End If

                                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId)  "
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_IssueNo) & "' "
                                _Qry &= vbCrLf & "," & _FNHSysWHId & " "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTGrpOrderNo.Text) & "' "
                                _Qry &= vbCrLf & "," & _FNIssQuantity & " "
                                _Qry &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "


                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If

                                _FNReqQuantity = _FNReqQuantity - _FNIssQuantity

                                If _FNReqQuantity <= 0 Then
                                    Exit For
                                End If
                            Next

                            Rx!FNReqQuantity = _FNReqQuantity
                        Next
                        _dt.EndInit()

                        If _IssueNo <> "" Then

                            If _AllDocNo = "" Then

                                _AllDocNo = _IssueNo

                            Else

                                _AllDocNo = _AllDocNo & "," & _IssueNo

                            End If

                        End If

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

            Else

                Return False

            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

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

    Private Function CheckIssue() As Boolean
        Dim _Qry As String = ""

        _Qry = " Select Top 1 FTIssueReqNo "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "' AND ISNULL(FTIssueReqNo,'') <> ''  "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1412201911, "เอกสารที่การนำไปเบิกจ่ายแล้วไม่สามารถ ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Function CheckDataDetail() As Boolean
        Dim _Qry As String = ""

        _Qry = " Select Top 1 FTIssueReqNo "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "' AND ISNULL(FTIssueReqNo,'') <> ''  "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1442201911, "ไม่พบรายการของเอกสารนี้ ไม่สามารถทำรายการต่อได้ กรุณาทำการตรวจสอบ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTIssueReqBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If CheckIssue() = False Then Exit Sub
        If CheckOwner() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If CheckIssue() = False Then Exit Sub


        If CheckOwner() = False Then Exit Sub


        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTIssueReqNo.Text, Me.Text) = False Then
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
        If Me.FTIssueReqNo.Text <> "" Then


            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "IssueRequestSlip.rpt"
                .Formular = "{TPRODTIssueReq.FTIssueReqNo}='" & HI.UL.ULF.rpQuoted(FTIssueReqNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTIssueReqNo_lbl.Text)
            FTIssueReqNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "


#End Region

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs)

        Try

            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FTGrpOrderNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTGrpOrderNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNHSysUnitSectId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysUnitSectId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNMatType.Properties.ReadOnly = (dt.Rows.Count > 0)

            dt.Dispose()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub wIssue_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Sub ocmadddetail_Click(sender As Object, e As EventArgs) Handles ocmadd.Click

        If CheckIssue() = False Then Exit Sub
        If CheckOwner() = False Then Exit Sub

        If FTIssueReqNo.Properties.Tag.ToString = "" Then

            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTIssueReqNo.Text = "" Then Exit Sub
            LoadDataInfo(Me.FTIssueReqNo.Text)
        End If

        With _AddProdRequestItem
            .DocumentNo = Me.FTIssueReqNo.Text
            .Mattype = Me.FNMatType.SelectedIndex
            .OrderNo = Me.FTGrpOrderNo.Text
            .Proc = False
            .ShowDialog()

            If .Proc Then
                LoadIssueDetail(Me.FTIssueReqNo.Text)
            End If

        End With

    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Call RemoveDetail()
    End Sub

    Private Sub RemoveDetail()
        If CheckIssue() = False Then Exit Sub
        If CheckOwner() = False Then Exit Sub
        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            Dim _Qry As String = ""

            Try

                For Each i As Integer In .GetSelectedRows()
                    _StateDelete = True

                    Dim _FTOrderProdNo As String = "" & .GetRowCellValue(i, "FTOrderProdNo").ToString
                    Dim _FNHSysMarkId As Integer = Integer.Parse(Val("" & .GetRowCellValue(i, "FNHSysMarkId").ToString))
                    Dim _FNTableNo As Integer = Integer.Parse(Val("" & .GetRowCellValue(i, "FNTableNo").ToString))
                    Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetRowCellValue(i, "FNHSysRawMatId").ToString))

                    _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail"
                    _Qry &= vbCrLf & " WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "'"
                    _Qry &= vbCrLf & " AND FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "'"
                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & _FNHSysMarkId & ""
                    _Qry &= vbCrLf & " AND FNTableNo=" & _FNTableNo & ""
                    _Qry &= vbCrLf & " AND FNHSysRawMatId=" & _FNHSysRawMatId & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail_Barcode"
                    _Qry &= vbCrLf & " WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.FTIssueReqNo.Text) & "'"
                    _Qry &= vbCrLf & " AND FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "'"
                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & _FNHSysMarkId & ""
                    _Qry &= vbCrLf & " AND FNTableNo=" & _FNTableNo & ""
                    _Qry &= vbCrLf & " AND FNHSysRawMatId=" & _FNHSysRawMatId & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                Next

            Catch ex As Exception
            End Try

            If _StateDelete Then
                LoadIssueDetail(Me.FTIssueReqNo.Text)
            End If

        End With
    End Sub

    Private Sub ogvdetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvdetail.CellMerge
        Try
            With Me.ogvdetail
                Select Case e.Column.FieldName
                    Case "FTIssueNo"
                        If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                Call RemoveDetail()
        End Select
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With Me.ogvdetail
                If "" & .GetRowCellValue(e.RowHandle, "FTIssueNo") <> "" Then
                    e.Appearance.ForeColor = Drawing.Color.Green
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_RowCountChanged1(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged
        Try

            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FTGrpOrderNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTGrpOrderNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNMatType.Properties.ReadOnly = (dt.Rows.Count > 0)

            FNHSysUnitSectId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysUnitSectId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            dt.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmautoissue_Click(sender As Object, e As EventArgs) Handles ocmautoissue.Click

        If CheckIssue() = False Then Exit Sub

        If cmpnotchk <= 0 Then
            If CheckOwner() = False Then Exit Sub
        Else
            If cmpnotchk <> HI.ST.SysInfo.CmpID Then

                If CheckOwner() = False Then Exit Sub

            End If
        End If


        If Me.FTIssueReqNo.Text <> "" And FTIssueReqNo.Properties.Tag.ToString <> "" Then

            If Me.FNMatType.SelectedIndex = 1 Then

                If Me.CheckDataDetail() Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Auto Issue Accessory ใช่หรือไม่ ? ", 1541140078) = True Then

                        Dim _Spls As New HI.TL.SplashScreen("Issuing.... Please wait.")
                        _AllDocNo = ""
                        If Me.AutoIssueAccessory Then

                            Me.LoadIssueDetail(Me.FTIssueReqNo.Text)



                            _Spls.Close()



                            HI.MG.ShowMsg.mInfo("ระบบได้ทำการ Auto Issue เรียบร้อยแล้ว...", 1501130189, Me.Text, _AllDocNo, MessageBoxIcon.Information)

                        Else
                            _Spls.Close()
                            HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Auto Issue ได้ เนื่องจากพบข้อผิดพลาดบางประการ กรุณาทำการติดต่อ Admin เพื่อทำการตรวจสอบ !!!", 1541141078, Me.Text, , MessageBoxIcon.Warning)
                        End If
                    End If
                End If

            Else
                HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Auto Issue ให้ได้เนื่องจากไม่ใช่ Accessory !!!", 1501132178, Me.Text, , MessageBoxIcon.Warning)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTIssueReqNo_lbl.Text)
            FTIssueReqNo.Focus()
        End If

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub
End Class

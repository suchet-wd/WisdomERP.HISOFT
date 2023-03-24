Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Public Class wPurchaseRequest

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_PUR
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _AddPopup As wAddRequest
    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _FTPurchaseRefNo As String = ""
    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call PrepareForm()

        _AddPopup = New wAddRequest
        HI.TL.HandlerControl.AddHandlerObj(_AddPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddPopup.Name.ToString.Trim, _AddPopup)
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


        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
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

        _Dt.Dispose()

        Call LoadGridDetail()


        Me.FTStateSendApp.Checked = False
        Me.FTStateApp.Checked = False

        Dim _dtcheck As DataTable
        _Str = "SELECT TOP 1 FTStateSendApp,FTStateApp "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request AS X WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
        _dtcheck = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_PUR)


        For Each Rx As DataRow In _dtcheck.Rows

            Me.FTStateSendApp.Checked = (Rx!FTStateSendApp.ToString = "1")
            Me.FTStateApp.Checked = (Rx!FTStateApp.ToString = "1")

            Exit For
        Next

        _dtcheck.Dispose()

        'Me.oxtb.SelectedTabPageIndex = 0
        _ProcLoad = False
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



        Return True
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
        _FormLoad = False
    End Sub


    Private Function CheckBuyed() As Boolean
        Dim _Qry As String = ""
        Dim _Dt As DataTable

        _Qry = "SELECT PR.FTPurchaseRefNo"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_Request_OrderNo as PR"
        _Qry &= vbCrLf & "WHERE PR.FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
        _FTPurchaseRefNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR)

        If _FTPurchaseRefNo = "" Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region

#Region "Procedure Grid Detail"

    Private Sub LoadGridDetail()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT  PR.FTOrderNo, PR.FNQuantity, PR.FTRemark,PR.FNHSysRawMatId,PR.FNHSysUnitId"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , M.FTRawMatNameTH AS FTMatDesc"
        Else
            _Qry &= vbCrLf & " , M.FTRawMatNameEN AS FTMatDesc"
        End If
        _Qry &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Qry &= vbCrLf & ", ISNULL(PR.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Qry &= vbCrLf & ", ISNULL(U.FTUnitCode,'') AS FTUnitCode, M.FTRawMatCode"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(P.FDPRRequestDate)=1 THEN convert(nvarchar(10), convert(datetime,P.FDPRRequestDate),103) ELSE'' END AS FDPRRequestDate"
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS PR LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request AS P ON PR.FTPRPurchaseNo=P.FTPRPurchaseNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M ON PR.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U ON PR.FNHSysUnitId = U.FNHSysUnitId"
        _Qry &= vbCrLf & "WHERE PR.FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        Me.ogcdetail.DataSource = _dt

    End Sub



#End Region

#Region " function "

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTPRPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTPRPurchaseBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1510311002, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PR นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1510311002, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PR นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If


        End If

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
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK) WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
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
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
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
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
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

    Private Sub Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub


    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If CheckOwner() = False Then Exit Sub
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
        If CheckBuyed() Then
            If FTPRPurchaseNo.Text <> "" Then
                If HI.UL.ULDate.CheckDate(FDPRRequestDate.Text) <> "" Then
                    If FTPRPurchaseNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then

                        If Me.VerrifyData() Then
                            If Me.SaveData Then

                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FDPRRequestDate_lbl.Text)
                    FDPRRequestDate.Focus()
                    Exit Sub
                End If

            Else
                Exit Sub
            End If
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อวัตถุดิบได้ เนื่องจากได้ถูกนำไปสั่งซื้อวัตถุดิบแล้ว !!!!", 1511261436, Me.Text)
            Exit Sub
        End If

        With _AddPopup
            .PRNO = FTPRPurchaseNo.Text
            .AddMat = False

            Call HI.ST.Lang.SP_SETxLanguage(_AddPopup)
            HI.TL.HandlerControl.ClearControl(_AddPopup)

            .FTOrderNo.Properties.ReadOnly = False
            .FTOrderNo.Properties.Buttons(0).Enabled = True
            .ShowDialog()

            If (.AddMat) Then
                Dim _Str As String = ""

                Try
                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    _Str = "SELECT TOP 1 FTPRPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo WITH(NOLOCK) "
                    _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                    If HI.Conn.SQLConn.GetField(_Str, _DBEnum, "") = "" Then

                        _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & " , FTPRPurchaseNo,FTOrderNo, FNHSysRawMatId"
                        _Str &= vbCrLf & " , FNHSysUnitId,FNQuantity, FTRemark ,FTFabricFrontSize)"
                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & "," & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"


                    Else

                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & ",FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "



                    End If
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Else
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


                      
                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request "
                        _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                        _Str &= vbCrLf & "  ,FTSendAppBy='' "
                        _Str &= vbCrLf & "  ,FTStateApp='0' "
                        _Str &= vbCrLf & "  ,FTAppName='' "
                        _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND ISNULL(FTStateSendApp,'') ='1'"
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                        FTStateSendApp.Checked = False
                        FTStateApp.Checked = False

                    End If

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try


            End If

        End With
        Call LoadGridDetail()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If FTPRPurchaseNo.Text = "" Then Exit Sub
        If CheckOwner() = False Then Exit Sub

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        If CheckBuyed() Then
            If HI.UL.ULDate.CheckDate(FDPRRequestDate.Text) <> "" Then
                If Me.VerrifyData Then
                    If Me.SaveData Then

                        Dim _Str As String = ""

                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request "
                        _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                        _Str &= vbCrLf & "  ,FTSendAppBy='' "
                        _Str &= vbCrLf & "  ,FTStateApp='0' "
                        _Str &= vbCrLf & "  ,FTAppName='' "
                        _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND ISNULL(FTStateSendApp,'') ='1'"
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                        FTStateSendApp.Checked = False
                        FTStateApp.Checked = False

                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FDPRRequestDate_lbl.Text)
                FDPRRequestDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อวัตถุดิบได้ เนื่องจากได้ถูกนำไปสั่งซื้อวัตถุดิบแล้ว !!!!", 1511261436, Me.Text)
        End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Call FormRefresh()
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If CheckBuyed() Then
            If FTPRPurchaseNo.Text <> "" Then
                If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTPRPurchaseNo.Text, Me.Text) = True Then
                    If DeleteData() Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        'Call FormRefresh()
                        HI.TL.HandlerControl.ClearControl(Me)
                        Me.DefaultsData()
                        Me.FTPRPurchaseNo.Focus()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPRPurchaseNo_lbl.Text)
                Me.FTPRPurchaseNo.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อวัตถุดิบได้ เนื่องจากได้ถูกนำไปสั่งซื้อวัตถุดิบแล้ว !!!!", 1511261436, Me.Text)
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo(FTPRPurchaseNo.Properties.Tag.ToString)
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        If CheckOwner() = False Then Exit Sub
        If CheckBuyed() Then


            With ogvdetail
                If .RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                'Dim _FTPRPurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPRPurchaseNo").ToString
                Dim _FTOrderNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString
                Dim _FNHSysRawMatId As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString

                Dim _Qry As String = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo  WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' AND FNHSysRawMatId=" & Val(_FNHSysRawMatId) & " "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, _DBEnum)

                Dim _Str As String = ""

                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request "
                _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                _Str &= vbCrLf & "  ,FTSendAppBy='' "
                _Str &= vbCrLf & "  ,FTStateApp='0' "
                _Str &= vbCrLf & "  ,FTAppName='' "
                _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND ISNULL(FTStateSendApp,'') ='1'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                FTStateSendApp.Checked = False
                FTStateApp.Checked = False

                Call LoadGridDetail()

            End With
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อวัตถุดิบได้ เนื่องจากได้ถูกนำไปสั่งซื้อวัตถุดิบแล้ว !!!!", 1511261436, Me.Text)
        End If
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTPRPurchaseNo.Text <> "" And Me.FTPRPurchaseNo.Properties.Tag.ToString <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseRequest.rpt"
                '.AddParameter("Draft", "DRAFT")
                .Formular = "{TPURTPurchase_Request.FTPRPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPRPurchaseNo.Text)
            FTPRPurchaseNo.Focus()
        End If
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        Dim _Str As String = ""
        If CheckBuyed() Then
            With ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                'Dim MatId As Integer = Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString)
                With _AddPopup
                    .AddMat = False
                    .PRNO = FTPRPurchaseNo.Text

                    HI.TL.HandlerControl.ClearControl(_AddPopup)

                    .FTOrderNo.Text = "" & ogvdetail.GetFocusedRowCellValue("FTOrderNo").ToString
                    Dim ItemCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatCode").ToString

                    .FNHSysRawMatId.Text = ItemCode
                    .FTFabricFrontSize.Text = "" & ogvdetail.GetFocusedRowCellValue("FTFabricFrontSize").ToString
                    .FNHSysUnitIdPO.Text = "" & ogvdetail.GetFocusedRowCellValue("FTUnitCode").ToString
                    .FNHSysUnitIdPO.Properties.Tag = "" & ogvdetail.GetFocusedRowCellValue("FNHSysUnitId").ToString
                    .FNPOQuantity.Text = "" & ogvdetail.GetFocusedRowCellValue("FNQuantity").ToString
                    .FTRemark.Text = "" & ogvdetail.GetFocusedRowCellValue("FTRemark").ToString

                    .ShowDialog()

                    If (.AddMat) Then

                        Try

                            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                            HI.Conn.SQLConn.SqlConnectionOpen()
                            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                            _Str = "SELECT TOP 1 FTPRPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo WITH(NOLOCK) "
                            _Str &= vbCrLf & "WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
                            _Str &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "'"
                            _Str &= vbCrLf & "AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & ""

                            If HI.Conn.SQLConn.GetField(_Str, _DBEnum, "") = "" Then

                                _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                                _Str &= vbCrLf & " , FTPRPurchaseNo,FTOrderNo, FNHSysRawMatId"
                                _Str &= vbCrLf & " , FNHSysUnitId,FNQuantity, FTRemark ,FTFabricFrontSize)"
                                _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                                _Str &= vbCrLf & "," & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                                _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                                _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"
                            Else
                                _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                                _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                                _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                                _Str &= vbCrLf & ",FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                                _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                                _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                            End If
                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Else
                                HI.Conn.SQLConn.Tran.Commit()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            End If
                        Catch ex As Exception
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End Try
                    End If

                End With
            End With

            Call LoadGridDetail()
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อวัตถุดิบได้ เนื่องจากได้ถูกนำไปสั่งซื้อวัตถุดิบแล้ว !!!!", 1511261436, Me.Text)
        End If

    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTPRPurchaseNo.Text <> "" And Me.FTPRPurchaseNo.Properties.Tag.ToString <> "" Then

            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateSendApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FTStateApp<>'2'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,FTStateApp='0' "
                _Qry &= vbCrLf & "  ,FTAppName='' "
                _Qry &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

            End If
            FTStateSendApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPRPurchaseNo_lbl.Text)
            FTPRPurchaseNo.Focus()
        End If
    End Sub

    Private Sub FTPRPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPRPurchaseNo.EditValueChanged

    End Sub
End Class
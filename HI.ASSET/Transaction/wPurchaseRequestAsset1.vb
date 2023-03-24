Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.Pdf
Imports DevExpress.XtraPdfViewer


Public Class wPurchaseRequestAsset1

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_FIXED
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    'Private _AddPopup As wRequestItemPopup
    Private _AddPopup As wAddRequestAsset
    Private _AddPopup1 As wAddItemPRServiceAsset
    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _Attachbyte As Byte()
    Private _PathAttach As String = ""
    Private _FilePath As String
    'Private _StateLoadAttach As Boolean = False

    'Private _FTPurchaseRefNo As String = ""
    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call PrepareForm()

        '_AddPopup = New wRequestItemPopup
        'HI.TL.HandlerControl.AddHandlerObj(_AddPopup)
        _AddPopup = New wAddRequestAsset
        HI.TL.HandlerControl.AddHandlerObj(_AddPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddPopup.Name.ToString.Trim, _AddPopup)
        Catch ex As Exception
        Finally
        End Try

        _AddPopup1 = New wAddItemPRServiceAsset
        HI.TL.HandlerControl.AddHandlerObj(_AddPopup1)

        Dim oSysLang1 As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddPopup.Name.ToString.Trim, _AddPopup1)
        Catch ex As Exception
        Finally
        End Try
        Call InitGrid()

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
#Region "Initial Grid"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldSumAmt As String = "FNNetAmt|FNQuantity"
        ' Dim sFieldGrpSumAmt As String = "FNNetAmt"


        With ogvdetail
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FNNetAmt").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            'For Each Str As String In sFieldGrpSumAmt.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
            '    End If
            'Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .ExpandAllGroups()
            .RefreshData()

        End With

    End Sub
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
        'If _FieldName = "FTFileRef" And R.Item(Col).ToString <> "" Then
        '    Me.TabAttach.PageVisible = True
        '    Call _PDFViewer(CType(R.Item(Col), Byte()))
        'Else
        '    Obj.Text = R.Item(Col).ToString
        'End If
        If Me.FDPRPurchaseDate.Text <> "" And Me.FTPRPurchaseBy.Text <> "" Then
            'Dim dt As DataTable
            '_Str = "select top 1 FTFileRef FROM[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request with(nolock) where FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'"
            'dt = Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED, "datatable1")
            Dim _FTFileRef As String = HI.Conn.SQLConn.GetField("select top 1 FTFileRef FROM[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request with(nolock) where FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")


            ' If dt.Rows(0).Item("FTFileRef").ToString <> "" Then
            If _FTFileRef <> "" Then
                Try
                    Me.TabAttach.PageVisible = True
                    Call _PDFViewer(_FilePath)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If

        _Dt.Dispose()

        Call LoadGridDetail()
        'Call LoadFile()


        'Me.FTStateSendApp.Checked = False
        'Me.FTStateSendApp.Checked = False

        Dim _dtcheck As DataTable


        _Str = "SELECT TOP 1 FTStateSendApp,FTStateApp "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS X WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
        _dtcheck = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)


        For Each Rx As DataRow In _dtcheck.Rows

            Me.FTStateSendApp.Checked = (Rx!FTStateSendApp.ToString = "1")
            Me.FTStateApp.Checked = (Rx!FTStateApp.ToString = "1")

            Exit For
        Next
        Dim _RE As String = HI.Conn.SQLConn.GetField("SELECT FTStateRe FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request  WITH(NOLOCK) WHERE FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
        If _RE = "1" Then
            _Str = "SELECT TOP 1 FTAppName + '  REJECT' "
            _Str &= " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request WITH(NOLOCK)"
            _Str &= " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FTStateRe ='1'"
        Else
            _Str = "SELECT TOP 1 'REVISED' "
            _Str &= " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request WITH(NOLOCK)"
            _Str &= " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FTStateRe ='2'"
        End If


        Me.olbtrans.Text = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "")
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
        Dim _FTPurchaseRefNo As String = ""

        _Qry = "SELECT PR.FTPurchaseRefNo"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "]..TFIXEDTPurchase_Request_Detail as PR"
        _Qry &= vbCrLf & "WHERE PR.FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
        _FTPurchaseRefNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FIXED)

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
        If Me.FNFixedAssetType.Text = "อะไหล่จักร" Then
            _Qry = "select PRD.FNSeq,P.FTAssetPartCode AS FTAssetCode,PRD.FNHSysFixedAssetId,P.FTProductCode AS FTProductCode,PRD.FTRemark,isnull(PRD.FTDescription,'-')as FTDescription "
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",P.FTAssetPartNameTH  as FTAssetName,isnull(B.FTAssetBrandNameTH,'-') as FTAssetBrandName,'-' as FTAssetModelName"
            Else
                _Qry &= vbCrLf & ",P.FTAssetPartNameEN  as FTAssetName,B.FTAssetBrandNameEN as FTAssetBrandName,'-' as FTAssetModelName"
            End If
            _Qry &= vbCrLf & ",U.FTUnitAssetCode AS FTUnitCode,PRD.FNQuantity,ISNULL(Convert(numeric(18,2),PRD.FNPrice),0) as FNPrice,FNDisPer,ISNULL(Convert(numeric(18,2),FNDisAmt),0) as FNDisAmt,ISNULL(Convert(numeric(18,2),PRD.FNNetAmt),0) as FNNetAmt  from"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail AS PRD WItH(NOLOCK) LeFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS PR WItH(NOLOCK) ON PRD.FTPRPurchaseNo=PR.FTPRPurchaseNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WItH(NOLOCK) ON  PRD.FNHSysFixedAssetId=P.FNHSysAssetPartId  LEFT OUTER jOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WITH(NOLOCK) ON P.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON PRD.FNHSysUnitId=U.FNHSysUnitAssetId"
            _Qry &= vbCrLf & "where PRD.FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "' "
            _Qry &= vbCrLf & "order by P.FTAssetPartCode"
        Else
            _Qry = "select PRD.FNSeq,A.FTAssetCode AS FTAssetCode, PRD.FNHSysFixedAssetId,A.FTProductCode AS FTProductCode,PRD.FTRemark,isnull(PRD.FTDescription,'-')as FTDescription,PR.FTFileRef "
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",A.FTAssetNameTH as FTAssetName,isnull(B.FTAssetBrandNameTH,'-') as FTAssetBrandName,isnull(M.FTAssetModelNameTH,'-') as FTAssetModelName"
            Else
                _Qry &= vbCrLf & ",A.FTAssetNameEN  as FTAssetName,B.FTAssetBrandNameEN as FTAssetBrandName,M.FTAssetModelNameEN as FTAssetModelName"
            End If
            _Qry &= vbCrLf & ",U.FTUnitAssetCode AS FTUnitCode,PRD.FNQuantity,ISNULL(Convert(numeric(18,2),PRD.FNPrice),0) as FNPrice,FNDisPer,ISNULL(Convert(numeric(18,2),FNDisAmt),0) as FNDisAmt,ISNULL(Convert(numeric(18,2),PRD.FNNetAmt),0) as FNNetAmt from"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail AS PRD WItH(NOLOCK) LeFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS PR WItH(NOLOCK) ON PRD.FTPRPurchaseNo=PR.FTPRPurchaseNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) ON PRD.FNHSysFixedAssetId=A.FNHSysFixedAssetId LEFT OUTER jOIN"
            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WItH(NOLOCK) ON  PRD.FNHSysFixedAssetId=P.FNHSysAssetPartId  LEFT OUTER jOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WITH(NOLOCK) ON A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH(NOLOCK) ON A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON PRD.FNHSysUnitId=U.FNHSysUnitAssetId"
            _Qry &= vbCrLf & "where PRD.FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "' "
            _Qry &= vbCrLf & "order by PRD.FNSeq"
        End If

        
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FIXED)
        Me.ogcdetail.DataSource = _dt

        '    Dim _FE As String = HI.Conn.SQLConn.GetField("SELECT FTFileRef FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request WITH(NOLOCK) WHERE FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        '    For Each R As DataRow In _dt.Rows

        '        If _FE = ".xls" Then
        '            Me.opnl.Controls.Clear()
        '            Dim _ss As String = ""
        '            Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
        '            With _XlsV
        '                .ReadOnly = True
        '                .Dock = DockStyle.Fill
        '                .Unit = DevExpress.Office.DocumentUnit.Inch
        '                .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
        '                .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)
        '            End With
        '            Me.opnl.Controls.Add(_XlsV)
        '            '_ss = R!FNHSysSeasonId.ToString
        '            'Me.FNHSysSeasonId.Text = _ss
        '        Else
        '            Try
        '                Me.opnl.Controls.Clear()
        '                Dim _Pdfv As New PdfViewer
        '                Dim _ss As String = ""
        '                With _Pdfv
        '                    .Dock = DockStyle.Fill
        '                    _Pdfdata = CType(R!FTFileRef, Byte())
        '                    .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
        '                End With
        '                Me.opnl.Controls.Add(_Pdfv)
        '                '_ss = R!FNHSysSeasonId.ToString
        '                'Me.FNHSysSeasonId.Text = _ss
        '            Catch ex As Exception
        '            End Try
        '        End If
        '    Next
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
                    HI.MG.ShowMsg.mProcessError(1510311002, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PR นี้ ", Me.Text, Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1510311002, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PR นี้ ", Me.Text, Windows.Forms.MessageBoxIcon.Warning)
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
            '  _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
            Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
            Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
            Dim _type As String = HI.Conn.SQLConn.GetField("Select L.FTReferCode from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH = '" & Me.FNFixedAssetType.Text & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _Day As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 10), 2)
            Dim _STA As String = HI.Conn.SQLConn.GetField("Select L.FTReferCode from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH = '" & Me.FNPRState.Text & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & "PR" & _type & _STA & "-" & _Year & _Month & _Day).ToString

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
            'joker modify
            If _PathAttach <> "" Then
                Dim _cmd As String = ""
                Dim _AttachByte As Byte() = File.ReadAllBytes(_PathAttach)
                _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request"
                _cmd &= " Set  FTFileRef=@FTFileRef"
                _cmd &= " WHERE FTPRPurchaseNo=@FTPRPurchaseNo"
                Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                cmd.Parameters.AddWithValue("@FTPRPurchaseNo", Me.FTPRPurchaseNo.Text)
                Dim _pdf As New SqlParameter("@FTFileRef", SqlDbType.VarBinary)
                _pdf.Value = _AttachByte
                cmd.Parameters.Add(_pdf)
                cmd.ExecuteNonQuery()
            End If
            'end modify

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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
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
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อได้ เนื่องจากได้ถูกนำไปสั่งซื้อแล้ว !!!!", 206611525, Me.Text)
            Exit Sub
        End If
        Dim _Add As String = HI.Conn.SQLConn.GetField("SELECT FNPRState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request  WITH(NOLOCK) WHERE FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")


        ' If _Add = "0" Then
        If Me.FNPRState.Text = "สั่งซื้อ" Or Me.FNPRState.Text = "Purchase " Then
            'Dim _Qty As String = ""
            'Dim _dtBar As New DataTable
            '_Qty = " SELECT DISTINCT '0' AS FTSelect,A.FTProductCode AS FTAssetCode,A.FTAssetNameTH AS FTAssetName,0.00 AS FNQuantity,isnull(A.FNPrice,0.00) AS FNPrice,0.00 AS FNDisPer,0.00 AS FNDisAmt"
            '_Qty &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH (NOLOCK) "
            '_Qty &= vbCrLf & "LEFT OUTER JOIN (SELECT L.FTNameTH,L.FNListIndex"
            '_Qty &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
            '_Qty &= vbCrLf & "WHERE L.FTListName='FNFixedAssetType' ) AS L ON A.FNFixedAssetType=L.FNListIndex"
            '_Qty &= vbCrLf & "WHERE L.FTNameTH= '" & Me.FNFixedAssetType.Text & "'"
            '_dtBar = HI.Conn.SQLConn.GetDataTable(_Qty, Conn.DB.DataBaseName.DB_INVEN)

            'With _AddPopup
            '    '.PRNO = FTPRPurchaseNo.Text
            '    '.AddMat = False
            '    '.ProcLoad = True

            '    ''.FTAssetCode.Properties.Buttons(0).Enabled = True
            '    Call HI.ST.Lang.SP_SETxLanguage(_AddPopup)
            '    HI.TL.HandlerControl.ClearControl(_AddPopup)
            '    .ogcbarcode.DataSource = _dtBar
            '    .ogvbarcode.ActiveFilter.Clear()
            '    .ShowDialog()

            '    If (.AddMat) Then
            '        Dim _Str As String = ""
            '        Dim _FNSeq As String = ""


            '        Try
            '            _Str = "select max(FNSeq) AS FNSeq  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail where FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'" 'Edit by joker 2017/06/30 from select top 1 To select max
            '            _FNSeq = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0") + 1

            '            'Dim _Asset As String = HI.Conn.SQLConn.GetField("SELECT A.FNHSysFixedAssetId FROM( SELECT FNHSysFixedAssetId ,FTProductCode,FNPrice FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset  WITH(NOLOCK) UNION ALL SELECT P.FNHSysAssetPartId AS FNHSysFixedAssetId,P.FTProductCode,P.FNPrice FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) )AS A  WHERE FTProductCode='" & .FTAssetCode.Properties.Tag & "' AND FNPrice='" & .FNPrice.Value & "'", Conn.DB.DataBaseName.DB_MASTER, "")


            '            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
            '            HI.Conn.SQLConn.SqlConnectionOpen()
            '            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            '            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            '            '_Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail (FTInsUser, FDInsDate, FTInsTime"
            '            '_Str &= vbCrLf & ",FTPRPurchaseNo, FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt,FTRemark)"
            '            '_Str &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
            '            '_Str &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'," & Val(_FNSeq) & ",'" & _Asset & "','" & .FNHSysUnitAssetId.Properties.Tag & "'"
            '            '_Str &= vbCrLf & "," & .FNPrice.Value & "," & .FNDisPer.Value & "," & .FNDisAmt.Value & "," & .FNQuantity.Value & "," & .FNNetAmt.Value & ",'" & .FTRemark.Text & "'"

            '            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '                HI.Conn.SQLConn.Tran.Rollback()
            '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            Else
            '                HI.Conn.SQLConn.Tran.Commit()
            '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
            '                _Str &= vbCrLf & "  SET FTStateSendApp='0' "
            '                _Str &= vbCrLf & "  ,FTSendAppBy='' "
            '                _Str &= vbCrLf & "  ,FTStateApp='0' "
            '                _Str &= vbCrLf & "  ,FTAppName='' "
            '                _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND ISNULL(FTStateSendApp,'') ='1'"
            '                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_FIXED)

            '                FTStateSendApp.Checked = False
            '                FTStateSendApp.Checked = False

            '            End If

            '        Catch ex As Exception
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        End Try


            '    End If

            'End With
            With _AddPopup
                .PRNO = FTPRPurchaseNo.Text
                .AddMat = False
                .ProcLoad = True
                .FTAssetCode.Properties.Buttons(0).Enabled = True
                .FNFixedAssetType.Text = Me.FNFixedAssetType.Text
                Call HI.ST.Lang.SP_SETxLanguage(_AddPopup)
                HI.TL.HandlerControl.ClearControl(_AddPopup)
                .ShowDialog()

                If (.AddMat) Then

                    Dim _Str As String = ""
                    Dim _FNSeq As String = ""
                    Try
                        _Str = "select max(FNSeq) AS FNSeq  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail where FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'" 'Edit by joker 2017/06/30 from select top 1 To select max
                        _FNSeq = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0") + 1

                        '   Dim _Asset As String = HI.Conn.SQLConn.GetField("SELECT A.FNHSysFixedAssetId FROM( SELECT FNHSysFixedAssetId ,FTProductCode,FNPrice FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset  WITH(NOLOCK) UNION ALL SELECT P.FNHSysAssetPartId AS FNHSysFixedAssetId,P.FTProductCode,P.FNPrice FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) )AS A  WHERE FTProductCode='" & .FTAssetCode.Properties.Tag & "' AND FNPrice='" & .FNPrice.Value & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        '_Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail (FTInsUser, FDInsDate, FTInsTime"
                        '_Str &= vbCrLf & ",FTPRPurchaseNo, FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt,FTRemark)"
                        '_Str &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        '_Str &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'," & Val(_FNSeq) & ",'" & _Asset & "','" & .FNHSysUnitAssetId.Properties.Tag & "'"
                        '_Str &= vbCrLf & "," & .FNPrice.Value & "," & .FNDisPer.Value & "," & .FNDisAmt.Value & "," & .FNQuantity.Value & "," & .FNNetAmt.Value & ",'" & .FTRemark.Text & "'"

                        _Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail (FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & ",FTPRPurchaseNo, FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt,FTRemark)"
                        _Str &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'," & Val(_FNSeq) & ",'" & .FTAssetCode.Properties.Tag & "','" & .FNHSysUnitAssetId.Properties.Tag & "'"
                        _Str &= vbCrLf & "," & .FNPrice.Value & "," & .FNDisPer.Value & "," & .FNDisAmt.Value & "," & .FNQuantity.Value & "," & .FNNetAmt.Value & ",'" & .FTRemark.Text & "'"


                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TFIXEDTPurchase_Request "
                            _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                            _Str &= vbCrLf & "  ,FTSendAppBy='' "
                            _Str &= vbCrLf & "  ,FTStateApp='0' "
                            _Str &= vbCrLf & "  ,FTAppName='' "
                            _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND ISNULL(FTStateSendApp,'') ='1'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                            FTStateSendApp.Checked = False
                            FTStateSendApp.Checked = False

                        End If

                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End Try


                End If

            End With
        Else
            'Dim _Add As String = HI.Conn.SQLConn.GetField("SELECT FNPRState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request  WITH(NOLOCK) WHERE FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
            With _AddPopup1
                .PONO1 = FTPRPurchaseNo.Text
                .AddMat = False
                .ProcLoad = True
                '.FNPrice.Value =
                .FTAssetCode.Properties.Buttons(0).Enabled = True
                Call HI.ST.Lang.SP_SETxLanguage(_AddPopup1)
                HI.TL.HandlerControl.ClearControl(_AddPopup1)
                .ShowDialog()

                If (.AddMat) Then

                    Dim _Str As String = ""
                    Dim _FNSeq As String = ""
                    Try
                        _Str = "select max(FNSeq) AS FNSeq  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail where FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'" 'Edit by joker 2017/06/30 from select top 1 To select max
                        _FNSeq = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0") + 1

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        _Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail (FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & ",FTPRPurchaseNo, FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt,FTRemark,FTDescription)"
                        _Str &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'," & Val(_FNSeq) & ",'" & .FTAssetCode.Properties.Tag & "','" & .FNHSysUnitAssetId.Properties.Tag & "'"
                        _Str &= vbCrLf & "," & .FNPrice.Value & "," & .FNDisPer.Value & "," & .FNDisAmt.Value & "," & .FNQuantity.Value & "," & .FNAmount.Value & ",'" & .FTNote.Text & "','" & .FTDescription.Text & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                            _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                            _Str &= vbCrLf & "  ,FTSendAppBy='' "
                            _Str &= vbCrLf & "  ,FTStateApp='0' "
                            _Str &= vbCrLf & "  ,FTAppName='' "
                            _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND ISNULL(FTStateSendApp,'') ='1'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_FIXED)

                            FTStateSendApp.Checked = False
                            FTStateSendApp.Checked = False

                        End If

                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End Try


                End If

            End With





        End If






        Call LoadGridDetail()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If FTPRPurchaseNo.Text = "" Then Exit Sub
        If CheckOwner() = False Then Exit Sub
        Dim _File As String = ""
        Dim _dataBinary As Byte()
        _File = System.IO.Path.GetExtension(_FilePath)
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        If CheckBuyed() Then
            If HI.UL.ULDate.CheckDate(FDPRRequestDate.Text) <> "" Then
                If Me.VerrifyData Then
                    If Me.SaveData Then
                        Dim _Str As String = ""
                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                        _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                        _Str &= vbCrLf & "  ,FTSendAppBy='' "
                        _Str &= vbCrLf & "  ,FTStateApp='0' "
                        _Str &= vbCrLf & "  ,FTAppName='NULL' "
                        _Str &= vbCrLf & "  ,FTStateApp='0' "
                        _Str &= vbCrLf & "  ,FTAppName='NULL' "
                        _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                        _Str &= vbCrLf & "  ,FTManagerName='NULL' "
                        _Str &= vbCrLf & " ,FNPurchaseType = '" & HI.UL.ULF.rpQuoted(Me.FNPurchaseType.SelectedIndex) & "'"
                        _Str &= vbCrLf & " ,FNPRState = '" & HI.UL.ULF.rpQuoted(Me.FNPRState.SelectedIndex) & "'"
                        _Str &= vbCrLf & " ,FNLifetimeType = '" & HI.UL.ULF.rpQuoted(Me.FNLifetimeType.SelectedIndex) & "'"
                        _Str &= vbCrLf & " ,FNLifetime = '" & HI.UL.ULF.rpQuoted(Me.FNLifetime.Text) & "'"
                        _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & " AND ISNULL(FTStateSendApp,'') ='1'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_FIXED)

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
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อได้ เนื่องจากได้ถูกนำไปสั่งซื้อแล้ว !!!!", 206611525, Me.Text)
        End If
        Dim _RE As String = HI.Conn.SQLConn.GetField("SELECT FTStateRe FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request  WITH(NOLOCK) WHERE FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
        Dim _SE As String = HI.Conn.SQLConn.GetField("SELECT FTStateApp FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request  WITH(NOLOCK) WHERE FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
        Dim _St As String = ""
        If _RE = "1" Or _SE = "1" Then
            _St = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
            _St &= vbCrLf & "  SET FTStateRe='2' "
            _St &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_St, Conn.DB.DataBaseName.DB_FIXED)
            _St = "SELECT TOP 1 'REVISED' "
            _St &= " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request WITH(NOLOCK)"
            _St &= " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FTStateRe ='2'"
            Me.olbtrans.Text = HI.Conn.SQLConn.GetField(_St, Conn.DB.DataBaseName.DB_FIXED, "")
        End If
        If _RE = "1" Or _SE = "1" Or _RE = "2" Then
            Dim _Qry As String = ""
            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Revised ( "
            _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
            _Qry &= vbCrLf & " , FTPRPurchaseNo, FNRevisedSeq, FTPRPurchaseRevisedBy"
            _Qry &= vbCrLf & ", FTRevisedDate, FTRevisedTime"
            _Qry &= vbCrLf & ")"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
            _Qry &= vbCrLf & ", ISNULL(("
            _Qry &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Revised "
            _Qry &= vbCrLf & "  WHERE FTPRPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
            _Qry &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
            _Qry &= vbCrLf & "),0) +1 "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '_Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(RevisedRemark) & "'"
            ' If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_FIXED)
            '    HI.Conn.SQLConn.Tran.Rollback()
            '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            'End If


        End If
        If _File <> "" Then
            Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
            _dataBinary = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
            If Not (_dataBinary Is Nothing) Then
                Dim _cmd As String = ""
                _cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request"
                _cmd &= " Set  FTFileRef=@FTFileRef"
                _cmd &= " WHERE FTPRPurchaseNo=@FTPRPurchaseNo"
                Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                cmd.Parameters.AddWithValue("@FTPRPurchaseNo", Me.FTPRPurchaseNo.Text)
                Dim _p As New SqlParameter("@FTFileRef", SqlDbType.VarBinary)
                _p.Value = _dataBinary
                cmd.Parameters.Add(_p)
                cmd.ExecuteNonQuery()

            End If
        End If

    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Call FormRefresh()
        '_StateLoadAttach = False
        Me.olbtrans.Text = ""

        Me.TabAttach.PageVisible = False
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If CheckBuyed() Then
            'If Me.FTStateSendApp.Text = "0" Then

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

        'Else
        'HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อได้ เนื่องจากได้ถูกนำไปสั่งซื้อแล้ว !!!!", 206611525, Me.Text)
        'End If
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

                Dim _FNSeq As String = .GetRowCellValue(.FocusedRowHandle, "FNSeq")
                Dim _IDAsset As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId")


                Dim _Qry As String = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail  WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FNSeq=" & Val(_FNSeq) & " AND FNHSysFixedAssetId=" & Val(_IDAsset) & " "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, _DBEnum)

                Dim _Str As String = ""

                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                _Str &= vbCrLf & "  ,FTSendAppBy='' "
                _Str &= vbCrLf & "  ,FTStateApp='0' "
                _Str &= vbCrLf & "  ,FTAppName='' "
                _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND ISNULL(FTStateSendApp,'') ='1'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                FTStateSendApp.Checked = False
                FTStateSendApp.Checked = False

                Call LoadGridDetail()

                Dim _Seq As Integer = 0
                For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Rows
                    _Seq += 1
                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail "
                    _Str &= vbCrLf & "  SET FNSeq= " & _Seq & " "
                    _Str &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FNHSysFixedAssetId = " & R!FNHSysFixedAssetId & ""
                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_FIXED)
                Next
                Call LoadGridDetail()
            End With
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขใบขอซื้อสินทรัพย์ได้ เนื่องจากได้ถูกนำไปสั่งซื้อแล้ว !!!!", 1611171134, Me.Text)
        End If
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTPRPurchaseNo.Text <> "" And Me.FTPRPurchaseNo.Properties.Tag.ToString <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseAsset\"
                .ReportName = "PurchaseRequestAsset.rpt"
                .AddParameter("FNPR", "0")
                .Formular = "{TFIXEDTPurchase_Request.FTPRPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPRPurchaseNo.Text)
            FTPRPurchaseNo.Focus()
        End If
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        Dim _Str As String = ""
        Dim _FNSeq As String = ""

        If CheckBuyed() Then
            With ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                'Dim MatId As Integer = Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString)
                Dim _Add As String = HI.Conn.SQLConn.GetField("SELECT FNPRState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request  WITH(NOLOCK) WHERE FTPRPurchaseNo='" & Me.FTPRPurchaseNo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")

                If _Add = "0" Then
                    With _AddPopup
                        .AddMat = False
                        .PRNO = FTPRPurchaseNo.Text
                        .ProcLoad = True
                        HI.TL.HandlerControl.ClearControl(_AddPopup)
                        _FNSeq = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNSeq")
                        .FTAssetCode.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetCode")
                        .FNPrice.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNPrice")
                        .FNDisPer.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisPer")
                        .FNDisAmt.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisAmt")
                        .FNQuantity.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNQuantity")
                        .FNNetAmt.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNNetAmt")
                        .FNFixedAssetType.Text = Me.FNFixedAssetType.Text
                        .FNHSysUnitAssetId.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTUnitCode")
                        .FTRemark.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRemark")
                        .FNHSysFixedAssetId_None.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetName")
                        .FTAssetCode.Properties.Buttons(0).Enabled = False
                        .FTAssetCode.Properties.Tag = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysFixedAssetId")
                        .ShowDialog()

                        If (.AddMat) Then

                            Try

                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                                HI.Conn.SQLConn.SqlConnectionOpen()
                                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                                _Str = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail"
                                _Str &= vbCrLf & "SET FTUpdUser='" & ST.UserInfo.UserName & "', FDUpdDate=" & UL.ULDate.FormatDateDB & ", FTUpdTime=" & UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & ",FNHSysUnitId=" & .FNHSysUnitAssetId.Properties.Tag & ", FNPrice=" & .FNPrice.Value & ", FNDisPer=" & .FNDisPer.Value & ", FNDisAmt=" & .FNDisAmt.Value & ""
                                _Str &= vbCrLf & ", FNQuantity=" & .FNQuantity.Value & ", FNNetAmt=" & .FNNetAmt.Value & ",FTRemark='" & .FTRemark.Text & "'"
                                _Str &= vbCrLf & "where FTPRPurchaseNo='" & UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' and FNSeq=" & Val(_FNSeq) & " and FNHSysFixedAssetId=" & .FTAssetCode.Properties.Tag & ""

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
                Else
                    With _AddPopup1
                        .AddMat = False
                        .PONO1 = FTPRPurchaseNo.Text
                        .ProcLoad = True
                        HI.TL.HandlerControl.ClearControl(_AddPopup1)
                        _FNSeq = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNSeq")
                        '.FTAssetCode.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetCode")
                        '.FNPrice.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNPrice")
                        '.FNDisPer.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisPer")
                        ''.FNDisAmt.Value = FNDisAmt.ToString
                        '.FNDisAmt.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisAmt")
                        '.FNQuantity.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNQuantity")
                        '.FNAmount.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNNetAmt")
                        '.FNHSysUnitAssetId.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTUnitCode")
                        '.FTNote.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRemark")
                        '.FNHSysFixedAssetId_None.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetName")
                        '.FTDescription.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTDescription")

                        '.FTAssetCode.Properties.Buttons(0).Enabled = False


                        .FTAssetCode.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetCode")
                        .FNPrice.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNPrice")
                        .FNDisPer.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisPer")
                        .FNDisAmt.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisAmt")
                        .FNQuantity.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNQuantity")
                        .FNAmount.Value = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNNetAmt")
                        .FNHSysUnitAssetId.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTUnitCode")
                        .FTDescription.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTDescription")
                        .FNHSysFixedAssetId_None.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetName")
                        .FTAssetCode.Properties.Buttons(0).Enabled = False
                        .FTAssetCode.Properties.Tag = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysFixedAssetId")



                        .ShowDialog()

                        If (.AddMat) Then

                            Try

                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                                HI.Conn.SQLConn.SqlConnectionOpen()
                                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                                _Str = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail"
                                _Str &= vbCrLf & "SET FTUpdUser='" & ST.UserInfo.UserName & "', FDUpdDate=" & UL.ULDate.FormatDateDB & ", FTUpdTime=" & UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & ",FNHSysUnitId=" & .FNHSysUnitAssetId.Properties.Tag & ", FNPrice=" & .FNPrice.Value & ", FNDisPer=" & .FNDisPer.Value & ", FNDisAmt=" & .FNDisAmt.Value & ""
                                _Str &= vbCrLf & ", FNQuantity=" & .FNQuantity.Value & ", FNNetAmt=" & .FNAmount.Value & ",FTRemark='" & .FTNote.Text & "',FTDescription='" & .FTDescription.Text & "'"
                                _Str &= vbCrLf & "where FTPRPurchaseNo='" & UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' and FNSeq=" & Val(_FNSeq) & "" ' and FNHSysFixedAssetId=" & .FTAssetCode.Properties.Tag & ""

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
                End If



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
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' AND FTStateApp<>'2'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FIXED, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,FTStateApp='0' "
                _Qry &= vbCrLf & "  ,FTAppName='' "
                _Qry &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Qry &= vbCrLf & "  ,FTManagerName='' "
                _Qry &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_FIXED)

            End If
            FTStateSendApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPRPurchaseNo_lbl.Text)
            FTPRPurchaseNo.Focus()
        End If
    End Sub

    Private Sub ocmattach_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
        'Dim _OpnDial As New OpenFileDialog
        'Dim _extension As String = ""

        'With _OpnDial
        '    '.Filter = "PDF files|*.pdf"
        '    .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx|PDF files |*.pdf"
        '    '.RestoreDirectory = True
        '    .RestoreDirectory = False
        '    .FilterIndex = 1
        '    .Multiselect = False
        '    If .ShowDialog = Windows.Forms.DialogResult.OK Then
        '        _extension = Path.GetExtension(.FileName)
        '        If _extension = ".pdf" Then
        '            _PathAttach = .FileName
        '            Call _PDFViewer(.FileName)
        '        ElseIf _extension = ".xlsx" Then
        '            '    Call _ExcelViewer(.FileName)
        '            Call _ExcelViewer(.FileName)
        '            'Me.TabAttach.PageVisible = True

        '            'ElseIf _extension = ".docx" Then
        '            '    Call _WordViewer(.FileName)
        '        End If
        '        '_StateLoadAttach = True
        '        Me.TabAttach.PageVisible = True
        '    End If

        'End With
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "PDF files |*.pdf|Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"


                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    _FilePath = .FileName
                    Call Readfile()
                Else
                    _FilePath = ""
                End If
            End With
        Catch ex As Exception

        End Try



    End Sub
    Private Sub Readfile()
        Try
            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)
                Select Case _FileType.ToUpper
                    Case ".XLSX".ToUpper, ".XLS".ToUpper
                        Call _ExcelViewer(_FilePath)
                    Case ".PDF".ToUpper
                        Call _PDFViewer(_FilePath)
                    Case Else
                        HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
                        Exit Sub
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub _PDFViewer(ByVal _Filename As String)
        Dim FTFileRef As New DevExpress.XtraPdfViewer.PdfViewer
        Try
            FTFileRef.LoadDocument(_Filename)
        Catch ex As Exception
            MsgBox(ex)
        End Try
        'Try
        '    '  Me.oGrpdetail.Controls.Clear()
        '    Dim FTFileRef As New PdfViewer
        '    FTFileRef.Dock = DockStyle.Fill
        '    '_Pdfv.LoadDocument(_Filename)
        '    FTFileRef.LoadDocument(_Filename)
        '    Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
        '    _Tabpage.Controls.Add(FTFileRef)
        '    _Tabpage.Name = "XtraTabPage" & _Filename.ToString
        '    _Tabpage.Text = "" & _Filename.ToString
        '    _Tabpage.Tag = "" & _Filename

        '    Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)
        '    '   Me.oGrpdetail.Controls.Add(_Pdfv)
        'Catch ex As Exception
        'End Try
    End Sub

    'Private Sub _PDFViewer(ByVal _Byte As Byte())
    '    'Dim FTFileRef As New DevExpress.XtraPdfViewer.PdfViewer
    '    Try
    '        Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)
    '        Me.opnl.Controls.Add(oTabcAttachedDetail)
    '    Catch ex As Exception
    '        MsgBox(ex)
    '    End Try
    'End Sub
    Private Sub LoadFile()
        Try
            Dim _Cmd As String = "" : Dim _oDT As DataTable
            _Cmd = "Select   FTPRPurchaseNo,FNPurchaseType, FTFileRef  "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request WITH(NOLOCK)"
            _Cmd &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "'"
            '_Cmd &= vbCrLf & " Order by FNSeq asc"
            _oDT = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)
            Me.oTabcAttachedDetail.TabPages.Clear()
            For Each R As DataRow In _oDT.Rows
                Select Case CInt("0" & R!FTFileRef.ToString)
                    Case 0 ' pdf
                        Try

                            Dim _Pdfv As New PdfViewer
                            With _Pdfv
                                .Dock = DockStyle.Fill
                                .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
                            End With
                            Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
                            '_Tabpage.Name = "XtraTabPage" & R!FTFileName.ToString
                            '_Tabpage.Text = "" & R!FTFileName.ToString
                            _Tabpage.Tag = "" & _FilePath
                            _Tabpage.Controls.Add(_Pdfv)
                            Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)


                        Catch ex As Exception
                        End Try
                    Case 1 'excel
                        Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                        With _XlsV
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .Unit = DevExpress.Office.DocumentUnit.Inch
                            .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                            .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)

                        End With
                        Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
                        '_Tabpage.Name = "XtraTabPage" & R!FTFileName.ToString
                        '_Tabpage.Text = "" & R!FTFileName.ToString
                        _Tabpage.Tag = "" & _FilePath
                        _Tabpage.Controls.Add(_XlsV)
                        Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)

                End Select
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private _Pdfdata As Byte()
    Private Sub _ExcelViewer(ByVal _Filename As String)
        'Dim _Excel As New DevExpress.XtraSpreadsheet.SpreadsheetControl
        'Try
        '    With _Excel
        '        .Dock = DockStyle.Fill
        '        .ReadOnly = True
        '        .LoadDocument(_Filename)
        '        Me.opnl.Controls.Add(_Excel)
        '    End With
        'Catch ex As Exception
        '    MsgBox(ex)
        'End Try
        Try
            '   Me.oGrpdetail.Controls.Clear()
            _Filename = System.IO.Path.GetFileName(_FilePath)
            Dim _Excel As New DevExpress.XtraSpreadsheet.SpreadsheetControl
            _Excel.Dock = DockStyle.Fill
            _Excel.LoadDocument(_FilePath)
            _Excel.ReadOnly = True
            Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
            _Tabpage.Name = "TabAttach" & _Filename.ToString
            _Tabpage.Text = "" & _Filename.ToString
            _Tabpage.Tag = "" & _FilePath
            _Tabpage.Controls.Add(_Excel)
            Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)
            '    Me.oGrpdetail.Controls.Add(_Excel)
        Catch ex As Exception
            HI.MG.ShowMsg.mInfo(ex.ToString, 1902141317, Me.Text, "")
        End Try
    End Sub

    'Private Sub _WordViewer(ByVal _Filename As String)
    '    Dim _Word As New DevExpress.XtraRichEdit.RichEditControl
    '    Try
    '        With _Word
    '            .Dock = DockStyle.Fill
    '            .ReadOnly = True
    '            .LoadDocument(_Filename)
    '            Me.opnl.Controls.Add(_Word)
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex)
    '    End Try
    'End Sub


    Private Sub FTPRPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPRPurchaseNo.EditValueChanged
        Try
            Me.ogcdetail.DataSource = Nothing
            Call LoadDataInfo(Me.FTPRPurchaseNo.Text)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Olbtrans_Click(sender As Object, e As EventArgs) Handles olbtrans.Click

    End Sub


    'Private Sub RepositoryItemCalcEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcEdit1.EditValueChanging
    '    Try
    '        Dim _oDt As DataTable
    '        Me.ogvdetail.SetFocusedRowCellValue("FNAmount", Double.Parse("0" & e.NewValue))
    '        With DirectCast(Me.ogcdetail.DataSource, DataTable)
    '            .AcceptChanges()
    '            _oDt = .Copy
    '        End With
    '        FNNetAmt.Value = Double.Parse("0" & _oDt.Compute("SUM(FNNetAmt)", "FNNetAmt > 0"))
    '    Catch ex As Exception
    '    End Try
    'End Sub
End Class
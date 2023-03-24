Imports System.Windows.Forms

Public Class wSMPReceiveFromIssue

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_SAMPLE
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wSMPReceiveFromIssueItem
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _ProcLoad As Boolean = False

    Private _FormLoad As Boolean = True

    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        _AddItemPopup = New wSMPReceiveFromIssueItem
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try


        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSumAmt As String = "FNAmount"
        Dim sFieldSumQty As String = "FNQuantity"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""


        With ogvdetail
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()



            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit.ToString & "}"
                End If
            Next

            For Each Str As String In sFieldSumQty.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()


        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region
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

        Call LoadRcvDetail(Key.ToString)

        Me.oxtb.SelectedTabPageIndex = 0

        FDReceiveFromIssueDate.Properties.Buttons(0).Visible = False
        FDReceiveFromIssueDate.Properties.ReadOnly = True

        _ProcLoad = False
    End Sub

    Private Sub LoadRcvDetail(PoKey As String)

        Dim _Str As String = ""

        _Str = " SELECT  D.* , U.FTUnitCode"
        _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue_Detail AS D WITH (NOLOCK)  "
        _Str &= vbCrLf & "   Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON D.FNHSysUnitId=U.FNHSysUnitId "
        _Str &= vbCrLf & " WHERE        (D.FTReceiveFromIssueNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        '_Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FTRawMatSizeCode "
        '_Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FNRawMatColorSeq, S.FNRawMatSizeSeq "
        _Str &= vbCrLf & " ORDER BY D.FNSeq "

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)



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
                            'With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            '    If Val(.Value.ToString) <= 0 Then
                            '        Pass = False
                            '    End If
                            'End With
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
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
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

            If Me.FDReceiveFromIssueDate.Properties.ReadOnly = False Then
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH, HI.UL.ULDate.ConvertEnDB(Me.FDReceiveFromIssueDate.Text)).ToString
            Else
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue WHERE FTReceiveFromIssueNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If



            _Str = " UPDATE A  SET FTStateSampleRoomApp='0'"
            _Str &= vbCrLf & " ,FTSampleRoomAppBy= '' "
            _Str &= vbCrLf & ",FDSampleRoomAppDate='' "
            _Str &= vbCrLf & ",FTSampleRoomAppTime='' "
            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As A  "
            _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B On A.FTBarcodeNo=B.FTBarcodeNo  "

            _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TSMPReceiveIssue_Detail As XS On A.FTDocumentNo=XS.FTIssueRefNo  "
            _Str &= vbCrLf & " AND A.FTOrderNo=XS.FTSMPOrderNo"
            _Str &= vbCrLf & " AND B.FNHSysRawMatId=XS.FNHSysRawMatIdRef"
            _Str &= vbCrLf & " AND B.FTPurchaseNo=XS.FTPurchaseNoRef"

            _Str &= vbCrLf & " WHERE A.FTReceiveFromIssueNo='" & HI.UL.ULF.rpQuoted(FTReceiveFromIssueNo.Text) & "'"


            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue_Detail WHERE FTReceiveFromIssueNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue WHERE FTReceiveFromIssueNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "'")
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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        FDReceiveFromIssueDate.Properties.Buttons(0).Visible = False
        FDReceiveFromIssueDate.Properties.ReadOnly = True
        _FormLoad = False
    End Sub

#End Region

#Region "MAIN PROC"

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTReceiveFromIssueBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else

            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTReceiveFromIssueBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If


        End If
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub

        If Me.VerrifyData Then
            If Me.SaveData() Then

                FDReceiveFromIssueDate.Properties.Buttons(0).Visible = False
                FDReceiveFromIssueDate.Properties.ReadOnly = True

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If

    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub


        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTReceiveFromIssueNo.Text, Me.Text) = False Then
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
        If Me.FTReceiveFromIssueNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "SMPReceiveIssueSlip.rpt"
                .Formular = "{TSMPReceiveIssueIssue.FTReceiveFromIssueNo}='" & HI.UL.ULF.rpQuoted(FTReceiveFromIssueNo.Text) & "' "
                .Preview()
            End With


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReceiveFromIssueNo_lbl.Text)
            FTReceiveFromIssueNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Variable "

#End Region

    Private Sub Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
            _FormLoad = False
        Catch ex As Exception
        End Try
    End Sub



    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With Me.ogvdetail
                'FTStateSendApp
                Select Case True
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTStateSendApp").ToString = "1")
                        e.Appearance.BackColor = Drawing.Color.LightCyan
                        e.Appearance.ForeColor = Drawing.Color.Blue
                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As System.Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged


        Try
            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            Me.FNHSysWHId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysWHId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)




            dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub


    Private Sub FTReceiveFromIssueNo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTReceiveFromIssueNo.EditValueChanged
    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As System.Object, e As System.EventArgs)
        ' If FNExchangeRate.Value <= 0 Then FNExchangeRate.Value = 1
    End Sub

    Private Function CheckDocRef() As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""


        _Str = "Select TOP 1 FTDocReferNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPIssue_Detail As R WITH(NOLOCK) WHERE FTDocReferNo='" & HI.UL.ULF.rpQuoted(FTReceiveFromIssueNo.Text) & "'  "

        If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SAMPLE, "") <> "" Then

            HI.MG.ShowMsg.mProcessError(1303157881, "พบข้อมูลการจ่าย แล้ว ไม่สามารถทำการลบหรือแก้ไขรายการได้ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)


            _Pass = False
        End If



        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click
        If CheckOwner() = False Then Exit Sub
        If (CheckDocRef() = False) Then Exit Sub
        If FTReceiveFromIssueNo.Properties.Tag.ToString = "" Then

            If Me.VerrifyData() Then

                If Me.SaveData Then
                Else
                    Exit Sub
                End If

            Else
                Exit Sub
            End If

        Else

            If Me.FTReceiveFromIssueNo.Text = "" Or Me.FTReceiveFromIssueNo.Properties.Tag.ToString = "" Then
                Exit Sub
            Else

                If FNHSysWHId.Properties.ReadOnly = False Then

                    If Me.SaveData Then
                    Else
                        Exit Sub
                    End If

                End If

            End If

        End If

        Dim _dt As DataTable
        Dim _Qry As String = ""

        _Qry = "   Select  '0' as FTSelect "
        _Qry &= vbCrLf & "  ,A.FTIssueNo"
        _Qry &= vbCrLf & "  , A.FDIssueDate"
        _Qry &= vbCrLf & "  , A.FTIssueBy"
        _Qry &= vbCrLf & "  , A.FTOrderNo,A.FTOrderNo AS FTSMPOrderNo "
        _Qry &= vbCrLf & "  , A.FNHSysCmpId"
        _Qry &= vbCrLf & "  , A.FNHSysWHCmpId"
        _Qry &= vbCrLf & "  , A.FNHSysRawMatId"
        _Qry &= vbCrLf & "  ,IM.FTRawMatCode "
        _Qry &= vbCrLf & "  ,IM.FTRawMatNameEN "

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & "  ,IM.FTRawMatNameTH AS FTRawMatName "
        'Else
        '    _Qry &= vbCrLf & "  ,IM.FTRawMatNameEN AS FTRawMatName"
        'End If

        _Qry &= vbCrLf & "  ,IMC.FTRawMatColorCode  "
        _Qry &= vbCrLf & "  ,IMS.FTRawMatSizeCode "
        _Qry &= vbCrLf & "  ,IMU.FTUnitCode ,IM.FNHSysUnitId "
        _Qry &= vbCrLf & "  , A.FNQuantity "
        _Qry &= vbCrLf & "    ,A.FTPurchaseNo "
        _Qry &= vbCrLf & "  ,A.FNPrice "
        _Qry &= vbCrLf & "  FROM( Select H.FTIssueNo "
        _Qry &= vbCrLf & "  , H.FDIssueDate "
        _Qry &= vbCrLf & "  , H.FTIssueBy "
        _Qry &= vbCrLf & "  , H.FTOrderNo "
        _Qry &= vbCrLf & "  , H.FNHSysCmpId "
        _Qry &= vbCrLf & "  , WH.FNHSysCmpId As FNHSysWHCmpId "
        _Qry &= vbCrLf & "  , BB.FNHSysRawMatId "
        _Qry &= vbCrLf & "  , SUM(B.FNQuantity) As FNQuantity "
        _Qry &= vbCrLf & "    ,BB.FTPurchaseNo "
        _Qry &= vbCrLf & "  ,BB.FNPrice "
        _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As BB With(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As B With(NOLOCK) On BB.FTBarcodeNo = B.FTBarcodeNo INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue As H With(NOLOCK) On B.FTDocumentNo = H.FTIssueNo INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As WH With(NOLOCK) On H.FNHSysWHId = WH.FNHSysWHId"

        _Qry &= vbCrLf & "  INNER Join   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMIssueSect As IST With(NOLOCK) On H.FNHSysIssueSectId  = IST.FNHSysIssueSectId"
        _Qry &= vbCrLf & "  INNER Join   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As SMP With(NOLOCK) On H.FTOrderNo  = SMP.FTSMPOrderNo"

        _Qry &= vbCrLf & " WHERE ISNULL(IST.FTStateSampleRoom,'')='1' "
        _Qry &= vbCrLf & "  And ISNULL(B.FTStateSampleRoomApp ,'0') <>'1'  "

        _Qry &= vbCrLf & "  Group By H.FTIssueNo "
        _Qry &= vbCrLf & "  , H.FDIssueDate "
        _Qry &= vbCrLf & "  , H.FTIssueBy "
        _Qry &= vbCrLf & "  , H.FTOrderNo "
        _Qry &= vbCrLf & "  , H.FNHSysCmpId "
        _Qry &= vbCrLf & "  , WH.FNHSysCmpId "
        _Qry &= vbCrLf & "    ,BB.FTPurchaseNo "
        _Qry &= vbCrLf & "  ,BB.FNPrice "
        _Qry &= vbCrLf & "  , BB.FNHSysRawMatId ) As A INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With(NOLOCK) On A.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "  Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As IMC With(NOLOCK) On IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
        _Qry &= vbCrLf & "  Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As IMS With(NOLOCK) On IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId "
        _Qry &= vbCrLf & "  Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As IMU With(NOLOCK) On IM.FNHSysUnitId = IMU.FNHSysUnitId "
        _Qry &= vbCrLf & "  ORDER BY A.FTIssueNo		"
        _Qry &= vbCrLf & "  ,IM.FTRawMatCode "
        _Qry &= vbCrLf & " ,IMC.FTRawMatColorCode "
        _Qry &= vbCrLf & "   ,IMS.FTRawMatSizeCode "

        ' _Qry = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_GET_RECEIVE_FROM_ISSUE  "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        HI.TL.HandlerControl.ClearControl(_AddItemPopup)

        With _AddItemPopup
            Call HI.ST.Lang.SP_SETxLanguage(_AddItemPopup)

            .ogvrcv.ClearColumnsFilter()
            .ogvrcv.ActiveFilter.Clear()
            .ogcrcv.DataSource = _dt
            .ShowDialog()

            If .ProcessProc Then

                CType(.ogcrcv.DataSource, DataTable).AcceptChanges()

                _dt = CType(.ogcrcv.DataSource, DataTable)

                If _dt.Select(" FTSelect ='1' ").Length > 0 Then

                    If Me.VerrifyData() Then
                        If Me.SaveData Then
                        Else
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                    Dim FNDataSeq As Integer = 0

                    _Qry = "SELECT MAX(FNSeq) AS  FNSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue_Detail AS X WITH(nolock) "
                    _Qry &= vbCrLf & " WHERE   FTReceiveFromIssueNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "' "
                    FNDataSeq = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

                    Dim _Spls As New HI.TL.SplashScreen("Saving Issue Detail...   Please Wait   ")

                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                        '_Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue_Detail "
                        '_Str &= vbCrLf & " WHERE   FTReceiveFromIssueNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "' "

                        'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



                        For Each R As DataRow In _dt.Select(" FTSelect ='1'")

                            _Spls.UpdateInformation("Issue Detail....   Please Wait    ")

                            FNDataSeq = FNDataSeq + 1

                            _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue_Detail ( FTInsUser, FDInsDate, FTInsTime, FTReceiveFromIssueNo, FNSeq, FTMatCode,FTMatColorCode,FTMatSizeCode, FTDescription, FNQuantity, FNPrice, FNAmount, FTPurchaseNoRef, FTIssueRefNo,FNHSysUnitId,FNHSysRawMatIdRef,FTSMPOrderNo) "
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "' "
                            _Qry &= vbCrLf & "," & FNDataSeq & " "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatColorCode.ToString) & "' "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatSizeCode.ToString) & "' "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatNameEN.ToString) & "' "
                            _Qry &= vbCrLf & "," & Val(R!FNQuantity.ToString) & " "
                            _Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                            _Qry &= vbCrLf & "," & CDbl(Format(Val(R!FNQuantity.ToString) * Val(R!FNPrice.ToString), HI.ST.Config.AmtFormat)) & " "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTIssueNo.ToString) & "' "
                            _Qry &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                            _Qry &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' "


                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                _Spls.Close()
                                Exit Sub
                            End If

                            _Qry = " UPDATE A  SET FTStateSampleRoomApp='1'"
                            _Qry &= vbCrLf & " ,FTSampleRoomAppBy= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & ",FDSampleRoomAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & ",FTSampleRoomAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As A  "
                            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B On A.FTBarcodeNo=B.FTBarcodeNo  "

                            _Qry &= vbCrLf & " WHERE A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTIssueNo.ToString) & "'"
                            _Qry &= vbCrLf & " AND A.FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "'"
                            _Qry &= vbCrLf & " AND B.FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & " "
                            _Qry &= vbCrLf & " AND B.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' "

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                _Spls.Close()
                                Exit Sub
                            End If


                        Next

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        _Spls.Close()

                        ' Exit Sub

                    Catch ex As Exception
                        _Spls.Close()

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        ' Exit Sub
                    End Try

                    Me.LoadRcvDetail(Me.FTReceiveFromIssueNo.Text)

                End If

            End If

        End With

        _dt.Dispose()

    End Sub


    Private Sub ocmremove_Click(sender As System.Object, e As System.EventArgs) Handles ocmremove.Click
        With ogvdetail
            If CheckOwner() = False Then Exit Sub


            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            '  If (CheckReceive(Me.FTPoNo.Text) = False) Then Exit Sub

            Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString
            Dim IssueNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTIssueRefNo").ToString
            Dim OrderNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString
            Dim PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNoRef").ToString
            Dim SysRawMatId As Integer = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatIdRef").ToString

            Dim _Str As String = ""

            Try
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue_Detail   WHERE FTReceiveFromIssueNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "' AND  FNSeq=" & Val(_MatID) & " "
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub
                End If


                _Str = " UPDATE A  SET FTStateSampleRoomApp='0'"
                _Str &= vbCrLf & " ,FTSampleRoomAppBy= '' "
                _Str &= vbCrLf & ",FDSampleRoomAppDate='' "
                _Str &= vbCrLf & ",FTSampleRoomAppTime='' "
                _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As A  "
                _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B On A.FTBarcodeNo=B.FTBarcodeNo  "

                _Str &= vbCrLf & " WHERE A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(IssueNo) & "'"
                _Str &= vbCrLf & " AND A.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                _Str &= vbCrLf & " AND B.FNHSysRawMatId=" & SysRawMatId & " "
                _Str &= vbCrLf & " AND B.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PurchaseNo) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Exit Sub
                End If


                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceiveIssue_Detail   WHERE FTReceiveFromIssueNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveFromIssueNo.Text) & "' AND  FNSeq=" & Val(_MatID) & " ")

                ' Exit Sub
            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Exit Sub
            End Try

            Me.SaveData()
            Me.LoadRcvDetail(Me.FTReceiveFromIssueNo.Text)

        End With
    End Sub



    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadDataInfo(Me.FTReceiveFromIssueNo.Text)
    End Sub

    Private Sub ogcbarcode_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub FDReceiveDate_EditValueChanged(sender As Object, e As EventArgs) Handles FDReceiveFromIssueDate.EditValueChanged

    End Sub

    Private Sub FDReceiveDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FDReceiveFromIssueDate.EditValueChanging
        Try
            If FDReceiveFromIssueDate.Properties.ReadOnly = False Then
                If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) > HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)) Then
                    e.Cancel = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class
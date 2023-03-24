Imports System.Windows.Forms

Public Class wInvoiceCharge
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True

    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldSumAmt As String = "FNAmount"
        Dim sFieldGrpSumAmt As String = "FNAmount"

        With ogv1
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .Columns.ColumnByFieldName("FNSeq").Width = 60
            .Columns.ColumnByFieldName("FNAmount").Width = 100
            .Columns.ColumnByFieldName("FNSeq").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FNAmount").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogv2
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .Columns.ColumnByFieldName("FNSeq").Width = 60
            .Columns.ColumnByFieldName("FNAmount").Width = 100
            .Columns.ColumnByFieldName("FNSeq").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FNAmount").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogv3
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .Columns.ColumnByFieldName("FNSeq").Width = 60
            .Columns.ColumnByFieldName("FNAmount").Width = 100
            .Columns.ColumnByFieldName("FNSeq").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FNAmount").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogv4
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .Columns.ColumnByFieldName("FNSeq").Width = 60
            .Columns.ColumnByFieldName("FNAmount").Width = 100
            .Columns.ColumnByFieldName("FNSeq").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FNAmount").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogv5
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .Columns.ColumnByFieldName("FNSeq").Width = 60
            .Columns.ColumnByFieldName("FNAmount").Width = 100
            .Columns.ColumnByFieldName("FNSeq").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FNAmount").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogv6
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .Columns.ColumnByFieldName("FNSeq").Width = 60
            .Columns.ColumnByFieldName("FNAmount").Width = 100
            .Columns.ColumnByFieldName("FNSeq").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FNAmount").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .ExpandAllGroups()
            .RefreshData()

        End With

        ''------End Add Summary Grid-------------

        With ogv7
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .Columns.ColumnByFieldName("FNSeq").Width = 60
            .Columns.ColumnByFieldName("FNAmount").Width = 100
            .Columns.ColumnByFieldName("FNSeq").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FNAmount").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .ExpandAllGroups()
            .RefreshData()

        End With

        ''------End Add Summary Grid-------------
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

    Public Sub LoadDataInfo(Key As Object, SuplKey As Object)
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' AND  FNHSysSuplId=" & Integer.Parse(Val(SuplKey)) & ""

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        HI.TL.HandlerControl.ClearControl(Me.ogbinvoicechargedetail)
        HI.TL.HandlerControl.ClearControl(Me.ogbnote)

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

        If _Dt.Rows.Count <= 0 Then
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
            Call DefaultsData()
        End If

        Call LoadDataDetail(Key, SuplKey)
         Call GetDataBySupl()
        Call GetDataByInv()
        Call GetDataByType()
        If Allocate() Then
            GetDataByInv()
        End If

        _ProcLoad = False
    End Sub

    Private Sub LoadDataDetail(Key As Object, SuplKey As Object)
        Dim _Qry As String = ""
        Dim dt As DataTable


        _Qry = "    Select Max(FTCurCode) AS FTCurCode,SUM(FNNetAmt) AS FNNetAmtCur ,SUM(Convert(numeric(18, 2), FNExchangeRate * FNNetAmt)) AS FNNetAmt"
        _Qry &= vbCrLf & " FROM (SELECT A.FTReceiveNo, A.FNExchangeRate, SUM(B.FNNetAmt) AS FNNetAmt,MAX(ISNULL(CU.FTCurCode,'')) AS FTCurCode"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS B WITH(NOLOCK) ON A.FTReceiveNo = B.FTReceiveNo INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK) ON A.FTPurchaseNo = P.FTPurchaseNo"
        _Qry &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CU WITH(NOLOCK) ON P.FNHSysCurId = CU.FNHSysCurId"
        _Qry &= vbCrLf & " WHERE  (A.FTInvoiceNo ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "') "
        _Qry &= vbCrLf & " AND (P.FNHSysSuplId =" & Integer.Parse(Val(SuplKey)) & ")"
        _Qry &= vbCrLf & " GROUP BY A.FTReceiveNo, A.FNExchangeRate) AS A"
        ' FNNetAmount.Value = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "0"))
        FNNetAmount.Value = 0
        FNNetRcvAmount.Value = 0
        FTCurCode.Text = ""
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each Rx As DataRow In dt.Rows
            Try
                FNNetAmount.Value = Val(Rx!FNNetAmt.ToString())
                FNNetRcvAmount.Value = Val(Rx!FNNetAmtCur.ToString())
                FTCurCode.Text = Rx!FTCurCode.ToString
            Catch ex As Exception

            End Try
            Exit For
        Next

        For I As Integer = 0 To 6

            _Qry = " SELECT FNSeq,FNAmount "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(SuplKey)) & ""
            _Qry &= vbCrLf & "  AND FNInvoiceType=" & I & ""
            _Qry &= vbCrLf & " ORDER BY FNSeq"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            dt.Rows.Add(dt.Rows.Count + 1, 0.0)

            Select Case I
                Case 0
                    Me.ogd1.DataSource = dt.Copy
                Case 1
                    Me.ogd2.DataSource = dt.Copy
                Case 2
                    Me.ogd3.DataSource = dt.Copy
                Case 3
                    Me.ogd4.DataSource = dt.Copy
                Case 4
                    Me.ogd5.DataSource = dt.Copy
                Case 5
                    Me.ogd6.DataSource = dt.Copy
                Case 6
                    Me.ogd7.DataSource = dt.Copy
            End Select
        Next

        Call CalculateAmt()
        ' _Qry = "  AND FNHSysSuplId='" && "'
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
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
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

            _FieldName = _FormHeader(cind).MainKey
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

                            End If

                            _Key = .Text
                        End With

                End Select

            Next

        Next

        _Str = "SELECT TOP 1 FTInvoiceNo "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge AS A WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "'"
        _Str &= vbCrLf & " AND FNHSysSuplId=" & Integer.Parse(Val((FNHSysSuplId.Properties.Tag.ToString))) & ""

        _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "")

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
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            Call SaveDetail(_Key)

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

    Private Function SaveDetail(ByVal _Key As String) As Boolean

        Try
            Dim _Qry As String = ""
            Dim _RowInd As Integer = 0
            If Not (ogd1.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd1.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    _RowInd = 0

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _RowInd = _RowInd + 1

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNAmount=" & Double.Parse(Val(R!FNAmount.ToString))
                        _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                        _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & "  AND FNInvoiceType=0 "
                        _Qry &= vbCrLf & "  AND FNSeq=" & _RowInd & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo, FNHSysSuplId, FNInvoiceType, FNSeq,FNAmount)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",0"
                            _Qry &= vbCrLf & "," & _RowInd
                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNAmount.ToString))

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            Else
                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " AND FNInvoiceType=0 "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            If Not (ogd2.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd2.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    _RowInd = 0

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _RowInd = _RowInd + 1

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNAmount=" & Double.Parse(Val(R!FNAmount.ToString))
                        _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                        _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & "  AND FNInvoiceType=1 "
                        _Qry &= vbCrLf & "  AND FNSeq=" & _RowInd & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo, FNHSysSuplId, FNInvoiceType, FNSeq,FNAmount)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",1"
                            _Qry &= vbCrLf & "," & _RowInd
                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNAmount.ToString))

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            Else
                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " AND FNInvoiceType=1 "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If


            If Not (ogd3.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd3.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    _RowInd = 0

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _RowInd = _RowInd + 1

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNAmount=" & Double.Parse(Val(R!FNAmount.ToString))
                        _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                        _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & "  AND FNInvoiceType=2 "
                        _Qry &= vbCrLf & "  AND FNSeq=" & _RowInd & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo, FNHSysSuplId, FNInvoiceType, FNSeq,FNAmount)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",2"
                            _Qry &= vbCrLf & "," & _RowInd
                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNAmount.ToString))

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            Else
                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " AND FNInvoiceType=2 "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            If Not (ogd4.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd4.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    _RowInd = 0

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _RowInd = _RowInd + 1

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNAmount=" & Double.Parse(Val(R!FNAmount.ToString))
                        _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                        _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & "  AND FNInvoiceType=3 "
                        _Qry &= vbCrLf & "  AND FNSeq=" & _RowInd & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo, FNHSysSuplId, FNInvoiceType, FNSeq,FNAmount)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",3"
                            _Qry &= vbCrLf & "," & _RowInd
                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNAmount.ToString))

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            Else
                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " AND FNInvoiceType=3 "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If


            If Not (ogd5.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd5.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    _RowInd = 0

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _RowInd = _RowInd + 1

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNAmount=" & Double.Parse(Val(R!FNAmount.ToString))
                        _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                        _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & "  AND FNInvoiceType=4 "
                        _Qry &= vbCrLf & "  AND FNSeq=" & _RowInd & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo, FNHSysSuplId, FNInvoiceType, FNSeq,FNAmount)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",4"
                            _Qry &= vbCrLf & "," & _RowInd
                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNAmount.ToString))

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            Else
                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " AND FNInvoiceType=4 "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            If Not (ogd6.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd6.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    _RowInd = 0

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _RowInd = _RowInd + 1

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNAmount=" & Double.Parse(Val(R!FNAmount.ToString))
                        _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                        _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & "  AND FNInvoiceType=5 "
                        _Qry &= vbCrLf & "  AND FNSeq=" & _RowInd & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo, FNHSysSuplId, FNInvoiceType, FNSeq,FNAmount)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",5"
                            _Qry &= vbCrLf & "," & _RowInd
                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNAmount.ToString))

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            Else
                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " AND FNInvoiceType=5 "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            If Not (ogd7.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd7.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    _RowInd = 0

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _RowInd = _RowInd + 1

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",FNAmount=" & Double.Parse(Val(R!FNAmount.ToString))
                        _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                        _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & "  AND FNInvoiceType=6 "
                        _Qry &= vbCrLf & "  AND FNSeq=" & _RowInd & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo, FNHSysSuplId, FNInvoiceType, FNSeq,FNAmount)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",6"
                            _Qry &= vbCrLf & "," & _RowInd
                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNAmount.ToString))

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If

                    Next

                End With
            Else
                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D"
                _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " AND FNInvoiceType=6 "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge"
            _Qry &= vbCrLf & " SET  FTStateFinish='" & FTStateFinish.EditValue.ToString & "'"
            _Qry &= vbCrLf & " ,FTExceedRemark='" & HI.UL.ULF.rpQuoted(FTExceedRemark.Text) & "'"
            _Qry &= vbCrLf & " ,FTPFIRemark='" & HI.UL.ULF.rpQuoted(FTPFIRemark.Text) & "'"
            _Qry &= vbCrLf & " ,FTDHLRemark='" & HI.UL.ULF.rpQuoted(FTDHLRemark.Text) & "'"
            _Qry &= vbCrLf & " ,FTUPSRemark='" & HI.UL.ULF.rpQuoted(FTUPSRemark.Text) & "'"
            _Qry &= vbCrLf & " ,FTFreightRemark='" & HI.UL.ULF.rpQuoted(FTFreightRemark.Text) & "'"
            _Qry &= vbCrLf & " ,FTOther1Remark='" & HI.UL.ULF.rpQuoted(FTOther1Remark.Text) & "'"
            _Qry &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"
            _Qry &= vbCrLf & " ,FNHSysCmpId=" & Integer.Parse(Val(FNHSysCmpId.Text)) & ""

            _Qry &= vbCrLf & " ,FNExceedCost = ISNULL((SELECT SUM(FNAmount) "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & "  AND FNInvoiceType=0 "
            _Qry &= vbCrLf & "),0)"

            _Qry &= vbCrLf & " ,FNPFICost = ISNULL((SELECT SUM(FNAmount) "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & "  AND FNInvoiceType=1 "
            _Qry &= vbCrLf & "),0)"

            _Qry &= vbCrLf & " ,FNDHLCost = ISNULL((SELECT SUM(FNAmount) "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & "  AND FNInvoiceType=2 "
            _Qry &= vbCrLf & "),0)"

            _Qry &= vbCrLf & " ,FNUPSCost = ISNULL((SELECT SUM(FNAmount) "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & "  AND FNInvoiceType=3 "
            _Qry &= vbCrLf & "),0)"

            _Qry &= vbCrLf & " ,FNFreightCost = ISNULL((SELECT SUM(FNAmount) "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & "  AND FNInvoiceType=4 "
            _Qry &= vbCrLf & "),0)"

            _Qry &= vbCrLf & " ,FNOther1Cost = ISNULL((SELECT SUM(FNAmount) "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & "  AND FNInvoiceType=5 "
            _Qry &= vbCrLf & "),0)"

            _Qry &= vbCrLf & " ,FNDutyCost = ISNULL((SELECT SUM(FNAmount) "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & "  AND FNInvoiceType=6 "
            _Qry &= vbCrLf & "),0)"

            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge "
            _Str &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _Str &= vbCrLf & " AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D "
            _Str &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _Str &= vbCrLf & " AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
               
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
        HI.TL.HandlerControl.ClearControl(ogbinvoicechargedetail)

        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
        _FormLoad = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTPrepareBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1411070101, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข รายการ นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If CheckOwner() = False Then Exit Sub
        If Me.VerrifyData Then

            If Me.SaveData() Then
                Call Allocate()

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If CheckOwner() = False Then Exit Sub

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTInvoiceNo.Text, Me.Text) = True Then
            If Me.DeleteData() Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
                Me.DefaultsData()
                Me.FTInvoiceNo.Focus()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If

    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

  
    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub wInvoiceCharge_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Call InitGrid()
    End Sub

    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTInvoiceNo.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FTInvoiceNo_EditValueChanged), New Object() {sender, e})
            Else
                Call LoadDataInfo(FTInvoiceNo.Text, Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogv_KeyDown(sender As Object, e As KeyEventArgs) Handles ogv1.KeyDown, ogv2.KeyDown, ogv3.KeyDown, ogv4.KeyDown, ogv5.KeyDown, ogv6.KeyDown, ogv7.KeyDown
        Select Case e.KeyCode
            Case Keys.Down
                Try

                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .FocusedColumn = .Columns.ColumnByFieldName("FNSeq")
                    End With
                    Dim _RowSeq As Integer = 0
                    With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                        .AcceptChanges()
                        Dim dt As DataTable = .Copy
                        _RowSeq = dt.Rows.Count + 1

                        If .Select("FNAmount<=0").Length <= 0 Then
                            .Rows.Add(_RowSeq, 0)
                        End If
                        .AcceptChanges()
                    End With

                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .FocusedRowHandle = _RowSeq
                        .FocusedColumn = .Columns.ColumnByFieldName("FNAmount")
                    End With


                Catch ex As Exception
                End Try
            Case Keys.Delete
                Try

                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                        .AcceptChanges()

                        Dim dt As DataTable = .Copy

                        If .Select("FNAmount<=0").Length <= 0 Then
                            .Rows.Add(dt.Rows.Count + 1, 0)
                        End If

                        Dim _RowSeq As Integer = 1
                        For Each R As DataRow In .Select("FNSeq>-1", "FNSeq")
                            R!FNSeq = _RowSeq

                            _RowSeq = _RowSeq + 1
                        Next
                        .AcceptChanges()
                    End With
                Catch ex As Exception
                End Try
        End Select
    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysSuplId_EditValueChanged), New Object() {sender, e})
            Else
                Dim _Qry As String = ""

                _Qry = "SELECT TOP 1 FNHSysSuplId "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTSuplCode='" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "'"
                FNHSysSuplId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                Call LoadDataInfo(FTInvoiceNo.Text, Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CalculateAmt()
        Try

            Dim _NetAmount As Double = 0.0
            If Not (ogd1.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd1.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _NetAmount = _NetAmount + Double.Parse(Val(R!FNAmount.ToString))
                    Next

                End With
            Else

            End If

            If Not (ogd2.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd2.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")            
                        _NetAmount = _NetAmount + Double.Parse(Val(R!FNAmount.ToString))
                    Next

                End With
            Else
            End If


            If Not (ogd3.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd3.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _NetAmount = _NetAmount + Double.Parse(Val(R!FNAmount.ToString))
                    Next

                End With
            Else

            End If

            If Not (ogd4.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd4.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _NetAmount = _NetAmount + Double.Parse(Val(R!FNAmount.ToString))
                    Next

                End With
            Else
            End If


            If Not (ogd5.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd5.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _NetAmount = _NetAmount + Double.Parse(Val(R!FNAmount.ToString))
                    Next

                End With
            Else
            End If

            If Not (ogd6.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd6.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")
                        _NetAmount = _NetAmount + Double.Parse(Val(R!FNAmount.ToString))
                    Next

                End With
            Else
               
            End If

            If Not (ogd7.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogd7.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                    For Each R As DataRow In dt.Select("FNAmount>0", "FNSeq")          
                        _NetAmount = _NetAmount + Double.Parse(Val(R!FNAmount.ToString))
                    Next

                End With
            Else           
            End If

            '  FNNetAmount.Value = _NetAmount
            FNChargeNetAmount.Value = _NetAmount
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Reposc1FNAmount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles Reposc1FNAmount.EditValueChanging, Reposc2FNAmount.EditValueChanging _
                                                                                          , Reposc3FNAmount.EditValueChanging, Reposc4FNAmount.EditValueChanging _
                                                                                          , Reposc5FNAmount.EditValueChanging, Reposc6FNAmount.EditValueChanging, Reposc7FNAmount.EditValueChanging

        Try

            Select Case sender.parent.Name.ToString
                Case ogd1.Name.ToString
                    With Me.ogv1
                        .SetFocusedRowCellValue("FNAmount", e.NewValue)
                    End With
                Case ogd2.Name.ToString
                    With Me.ogv2
                        .SetFocusedRowCellValue("FNAmount", e.NewValue)
                    End With
                Case ogd3.Name.ToString
                    With Me.ogv3
                        .SetFocusedRowCellValue("FNAmount", e.NewValue)
                    End With
                Case ogd4.Name.ToString
                    With Me.ogv4
                        .SetFocusedRowCellValue("FNAmount", e.NewValue)
                    End With
                Case ogd5.Name.ToString
                    With Me.ogv5
                        .SetFocusedRowCellValue("FNAmount", e.NewValue)
                    End With
                Case ogd6.Name.ToString
                    With Me.ogv6
                        .SetFocusedRowCellValue("FNAmount", e.NewValue)
                    End With
                Case ogd7.Name.ToString
                    With Me.ogv7
                        .SetFocusedRowCellValue("FNAmount", e.NewValue)
                    End With

            End Select
        Catch ex As Exception

        End Try

        Call CalculateAmt()

    End Sub

    Private Sub FNChargeNetAmount_EditValueChanged(sender As Object, e As EventArgs) Handles FNChargeNetAmount.EditValueChanged, FNNetAmount.EditValueChanged

        FNGAmount.Value = FNChargeNetAmount.Value + FNNetAmount.Value

    End Sub

    'Private Function Allocate() As Boolean
    '    Try
    '        Dim _Cmd As String = ""
    '        Dim _oDt As DataTable
    '        Dim _Dt As DataTable

    '        Dim _Amt As Double = 0
    '        Dim _RO As String = ""
    '        Dim _R As String = ""
    '        Dim _Rec As String = ""
    '        Dim _O As String = ""
    '        Dim _Qunt As Integer = 0
    '        Dim _NetAmt As Integer = 0
    '        Dim _Sup As String = ""
    '        ' Dim _QunAmount As Integer = 0
    '        'Dim _AA As String = ""
    '        With DirectCast(Me.ogcallocate.DataSource, DataTable)
    '            .AcceptChanges()
    '            _oDt = .Copy
    '            '_Qunt = I!FNQuantity.ToString
    '        End With

    '        With DirectCast(Me.ogcAllo.DataSource, DataTable)
    '            .AcceptChanges()
    '            _Dt = .Copy

    '        End With

    '        For Each R As DataRow In _oDt.Rows
    '            _Rec = R!FTReceiveNo.ToString
    '            _RO = R!FNRceceiveType.ToString
    '            _Qunt = R!FNQuantity.ToString
    '            '_QunAmount = HI.Conn.SQLConn.GetField("select sum(O.FNQuantity)as FNQuantity from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo     WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '            '_AA = HI.Conn.SQLConn.GetField("select L.FTNameTH from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join (select L.FTNameTH,L.FNListIndex from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L where L.FTListName='FNRceceiveType')AS L ON R.FNRceceiveType=L.FNListIndex    WHERE R.FTReceiveNo='" & _Rec & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")



    '        Next



    '        For Each I As DataRow In _Dt.Rows
    '            _Amt += +Double.Parse("0" & I!FNNetAmt.ToString)
    '            _R = I!FTReceiveNo.ToString
    '            '_O = I!FTOrderNo.ToString
    '            _NetAmt = I!FNNetAmt.ToString
    '            _Sup = I!FNHSysSuplId.ToString
    '            'Dim _Order As String = HI.Conn.SQLConn.GetField("select O.FTOrderNo from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS O     WHERE O.FTReceiveNo='" & _R & "' and O.FTOrderNo='" & _O & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '        Next

    '        ' Dim _NetAmount = Me.FNNetRcvAmount.Text
    '        Dim _Charge As String = HI.Conn.SQLConn.GetField("select CD.FNAmount from (select R.FTInvoiceNo,RO.FTOrderNo,ro.FNPrice,sum(ro.FNQuantity) as FNQuantity,ro.FNPrice*sum(ro.FNQuantity) as FNAmount from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D AS CD ON R.FTInvoiceNo=CD.FTInvoiceNo group by R.FTInvoiceNo,RO.FTOrderNo,ro.FNPrice)as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "] .dbo.TACCTInvoiceCharge_D AS CD ON R.FTInvoiceNo=CD.FTInvoiceNo    WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")

    '        Dim _NetAmount As String = HI.Conn.SQLConn.GetField("select sum(R.FNAmount) as TOTAL from (select R.FTInvoiceNo,RO.FTOrderNo,ro.FNPrice,sum(ro.FNQuantity) as FNQuantity,ro.FNPrice*sum(ro.FNQuantity) as FNAmount from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D AS CD ON R.FTInvoiceNo=CD.FTInvoiceNo group by R.FTInvoiceNo,RO.FTOrderNo,ro.FNPrice)as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "] .dbo.TACCTInvoiceCharge_D AS CD ON R.FTInvoiceNo=CD.FTInvoiceNo    WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '        'Dim _QunAmount As String = HI.Conn.SQLConn.GetField("select sum(O.FNQuantity)as FNQuantity from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo     WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '        'Dim _AA As String = HI.Conn.SQLConn.GetField("select L.FTNameTH from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join (select L.FTNameTH,L.FNListIndex from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L where L.FTListName='FNRceceiveType')AS L ON R.FNRceceiveType=L.FNListIndex    WHERE R.FTReceiveNo='" & _Rec & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")

    '        'Dim _Order As String = HI.Conn.SQLConn.GetField("select O.FTOrderNo from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS O     WHERE O.FTReceiveNo='" & _R & "' and O.FTOrderNo='" & _O & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")

    '        '_AmtGA = Me.FNChargeNetAmount.Text
    '        '_AmtGA1 = Me.FNNetRcvAmount.Text
    '        ' _AmtGA2 = _Amtch / '_AmtGA




    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
    '        'If _Rec <>

    '        'For Each I As DataRow In _oDt.Rows
    '        ' _Rec = R!FTReceiveNo.ToString
    '        '_RO = R!FNRceceiveType.ToString
    '        Dim _QunAmount As String = HI.Conn.SQLConn.GetField("select sum(RO.FNQuantity)as FNQuantity from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo     WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '        Dim _AA As String = HI.Conn.SQLConn.GetField("select L.FTNameTH from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join (select L.FTNameTH,L.FNListIndex from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L where L.FTListName='FNRceceiveType')AS L ON R.FNRceceiveType=L.FNListIndex    WHERE R.FTReceiveNo='" & _R & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '        ' Dim _Order As String = HI.Conn.SQLConn.GetField("select O.FTOrderNo from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS O     WHERE O.FTReceiveNo='" & _R & "' and O.FTOrderNo='" & _O & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '        If _AA = "รับวัตถุดิบ" Then
    '            For Each I As DataRow In _Dt.Rows
    '                ' _R = I!FTReceiveNo.ToString
    '                _O = I!FTOrderNo.ToString
    '                Dim _Order As String = HI.Conn.SQLConn.GetField("select O.FTOrderNo from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS O     WHERE O.FTReceiveNo='" & _R & "' and O.FTOrderNo='" & _O & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
    '                _Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '                _Cmd &= vbCrLf & ",FNAmount=" & (_Charge * Double.Parse("0" & _NetAmt)) / _NetAmount
    '                _Cmd &= vbCrLf & "Where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
    '                _Cmd &= vbCrLf & "and FNHSysSuplId='" & HI.UL.ULF.rpQuoted(_Sup) & "'"
    '                _Cmd &= vbCrLf & "and FTOrderNo = '" & _Order & "'"
    '                ' _Cmd &= vbCrLf & "and FTReceiveNo = '" & _R & "'"

    '                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
    '                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo,FNHSysSuplId, FTOrderNo, FNAmount)"
    '                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
    '                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Sup) & "'"
    '                    _Cmd &= vbCrLf & ",'" & _Order & "'"
    '                    _Cmd &= vbCrLf & "," & (_Charge * Double.Parse("0" & _NetAmt)) / _NetAmount
    '                    ' _Cmd &= vbCrLf & ",'" & _R & "'"

    '                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                        HI.Conn.SQLConn.Tran.Rollback()
    '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                        Return False
    '                    End If
    '                End If
    '            Next
    '        Else
    '            For Each I As DataRow In _Dt.Rows
    '                Dim _Order As String = HI.Conn.SQLConn.GetField("select O.FTOrderNo from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS O     WHERE O.FTReceiveNo='" & _R & "' and O.FTOrderNo='" & _O & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
    '                '_R = I!FTReceiveNo.ToString
    '                _O = I!FTOrderNo.ToString
    '                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
    '                _Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '                _Cmd &= vbCrLf & ",FNAmount=" & (_Charge * Double.Parse("0" & _Qunt)) / _QunAmount
    '                _Cmd &= vbCrLf & "Where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
    '                _Cmd &= vbCrLf & "and FNHSysSuplId=" & Integer.Parse("0" & _Sup)
    '                _Cmd &= vbCrLf & "and FTOrderNo = '" & _Order & "'"
    '                '   _Cmd &= vbCrLf & "and FTReceiveNo = '" & _R & "'"


    '                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
    '                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo,FNHSysSuplId, FTOrderNo, FNAmount)"
    '                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
    '                    _Cmd &= vbCrLf & "," & Integer.Parse("0" & _Sup)
    '                    _Cmd &= vbCrLf & ",'" & _Order & "'"
    '                    _Cmd &= vbCrLf & "," & (_Charge * Double.Parse("0" & _Qunt)) / _QunAmount
    '                    '  _Cmd &= vbCrLf & ",'" & _R & "'"

    '                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                        HI.Conn.SQLConn.Tran.Rollback()
    '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                        Return False
    '                    End If
    '                End If
    '            Next
    '        End If
    '        'Next



    '        '_Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTProdCost "
    '        '_Cmd &= vbCrLf & "SET FTStateSendApp='1'"
    '        '_Cmd &= vbCrLf & " WHERE FTInvoiceBankNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "'"
    '        '_Cmd &= vbCrLf & " AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""

    '        'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) Then
    '        '    ' Me.FTStateSendApp.Checked = True
    '        '    ' HI.MG.ShowMsg.mInfo("Send Approve Success..", 1609231623, Me.Text)
    '        'End If

    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return True

    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return False
    '    End Try
    'End Function
    Private Function Allocate() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _oD As DataTable
            Dim _Dt As DataTable
            Dim _A1 As Double = 0
            Dim _A3 As Double = 0
            Dim _A2 As Double = 0
            Dim _AA As Double = 0
            Dim _Amt As Double = 0
            Dim _Sup As String = ""
            Dim _PO As String = ""
            Dim _Order As String = ""
            Dim _Order1 As String = ""
            Dim _Rec As String = ""
            Dim _T As String = ""
            Dim _D As String = ""
            With DirectCast(Me.ogcallocate.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy

            End With
    
            With DirectCast(Me.ogcAllo.DataSource, DataTable)
                .AcceptChanges()
                _Dt = .Copy
            End With
            For Each I As DataRow In _Dt.Rows
                _Amt = I!FNNetAmt.ToString
                _Sup = I!FNHSysSuplId.ToString

            Next
            With DirectCast(Me.ogctype.DataSource, DataTable)
                .AcceptChanges()
                _oD = .Copy

            End With
            'For Each D As DataRow In _oD.Rows
            '    _T = D!FTReceiveNo.ToString
            '    _D = D!FTReceiveNo.ToString
            'Next

            For Each R As DataRow In _oDt.Rows
                _Rec = R!FTReceiveNo.ToString
             

            Next
            'Dim _Charge As String = HI.Conn.SQLConn.GetField("select CD.FNAmount from (select R.FTInvoiceNo,RO.FTOrderNo,ro.FNPrice,sum(ro.FNQuantity) as FNQuantity,ro.FNPrice*sum(ro.FNQuantity) as FNAmount from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D AS CD ON R.FTInvoiceNo=CD.FTInvoiceNo group by R.FTInvoiceNo,RO.FTOrderNo,ro.FNPrice)as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "] .dbo.TACCTInvoiceCharge_D AS CD ON R.FTInvoiceNo=CD.FTInvoiceNo    WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")

            Dim _NetAmount As String = HI.Conn.SQLConn.GetField("select sum(FNAmount) as FNAmount from (select ro.FNPrice*sum(ro.FNQuantity) as FNAmount, RO.FTOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo     WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "' group by RO.FTOrderNo,ro.FNPrice) AS A", Conn.DB.DataBaseName.DB_ACCOUNT, "")
            'Dim _QunAmount As String = HI.Conn.SQLConn.GetField("select sum(RO.FNQuantity)as FNQuantity from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo     WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "' and R.FNRceceiveType='2' and R.FTPurchaseNo='" & _PO & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
            Dim _Charge As String = HI.Conn.SQLConn.GetField("select sum(CD.FNAmount )as FNAmount from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_D AS CD  WHERE CD.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each D As DataRow In _oD.Rows
                Dim _QunAmount As String = HI.Conn.SQLConn.GetField("select sum(RO.FNQuantity)as FNQuantity from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo     WHERE R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "' and R.FNRceceiveType='" & D!FNRceceiveType.ToString & "' and RO.FTOrderNo='" & D!FTOrderNo.ToString & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
                'Dim _ReceiveNo As String = HI.Conn.SQLConn.GetField("SELECT R.FTReceiveNo  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo   WHERE RO.FTOrderNo='" & I!FTOrderNo.ToString & "' and R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "' ", Conn.DB.DataBaseName.DB_ACCOUNT, "")
                ' Dim _Type As String = HI.Conn.SQLConn.GetField("select R.FNRceceiveType from(select R.FNRceceiveType,R.FTReceiveNo,sum(RO.FNQuantity)as FNQuantity,RO.FTOrderNo,R.FTInvoiceNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON R.FTReceiveNo=RO.FTReceiveNo group by  R.FNRceceiveType,RO.FTOrderNo,R.FTInvoiceNo,R.FTReceiveNo)AS R  WHERE R.FTOrderNo='" & I!FTOrderNo.ToString & "'  and R.FTReceiveNo='" & _T & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
                Select Case D!FNRceceiveType.ToString
                    Case 0
                        _A1 = (_Charge * D!FNNetAmt.ToString) / _NetAmount
                        _Order = D!FTOrderNo.ToString
                        _AA = _A1
                    Case 1
                        _A3 = (_Charge * D!FNNetAmt.ToString) / _NetAmount
                        _Order = D!FTOrderNo.ToString
                        _AA = _A3
                    Case 2

                        _A2 = (_Charge * D!FNQuantity.ToString) / _QunAmount
                        _Order1 = D!FTOrderNo.ToString

                        If _Order = _Order1 Then
                            _AA = _A1 + _A2 + _A3
                        Else
                            _AA = _A2
                        End If


                End Select

              
                '   



                '        _Order = HI.Conn.SQLConn.GetField("select O.FTOrderNo from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS O     WHERE O.FTReceiveNo='" & _Rec & "' and O.FTOrderNo='" & I!FTOrderNo.ToString & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
                _Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNAmount='" & _AA & "'"
                _Cmd &= vbCrLf & "Where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                _Cmd &= vbCrLf & "and FNHSysSuplId='" & HI.UL.ULF.rpQuoted(_Sup) & "'"
                _Cmd &= vbCrLf & "and FTOrderNo = '" & D!FTOrderNo.ToString & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo,FNHSysSuplId, FTOrderNo, FNAmount)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Sup) & "'"
                    _Cmd &= vbCrLf & ",'" & D!FTOrderNo.ToString & "'"
                    _Cmd &= vbCrLf & ",'" & _AA & "'"



                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
                '    Case 2
                '        _Order = HI.Conn.SQLConn.GetField("select O.FTOrderNo from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS O     WHERE O.FTReceiveNo='" & _Rec & "' and O.FTOrderNo='" & I!FTOrderNo.ToString & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
                '        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
                '        _Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '        _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                '        _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                '        _Cmd &= vbCrLf & ",FNAmount=" & (_Charge * I!FNQuantity.ToString) / _QunAmount
                '        _Cmd &= vbCrLf & "Where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                '        _Cmd &= vbCrLf & "and FNHSysSuplId='" & HI.UL.ULF.rpQuoted(_Sup) & "'"
                '        _Cmd &= vbCrLf & "and FTOrderNo = '" & I!FTOrderNo.ToString & "'"
                '        _Cmd &= vbCrLf & "and FNRceceiveType = '" & I!FNRceceiveType.ToString & "'"

                '        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order"
                '            _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo,FNHSysSuplId, FTOrderNo, FNAmount,FNRceceiveType)"
                '            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                '            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                '            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                '            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Sup) & "'"
                '            _Cmd &= vbCrLf & ",'" & I!FTOrderNo.ToString & "'"
                '            _Cmd &= vbCrLf & "," & (_Charge * I!FNQuantity.ToString) / _QunAmount
                '            _Cmd &= vbCrLf & ",'" & I!FNRceceiveType.ToString & "'"


                '            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '                HI.Conn.SQLConn.Tran.Rollback()
                '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '                Return False
                '            End If
                '        End If
                'End Select


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
    Private Sub GetDataBySupl()
        Try
            Dim _Cmd As String = ""
            Dim _PO As String = ""

            'End If


            _Cmd = " select R.FTReceiveNo,R.FTPurchaseNo,case when isdate(R.FDReceiveDate) = 1 then CONVERT(varchar(10),convert(datetime,R.FDReceiveDate),103) else '' end as FDReceiveDate,R.FTReceiveBy,sum(ro.FNQuantity) as FNQuantity,ro.FNPrice*sum(ro.FNQuantity) as FNAmount "
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",L.FTNameTH AS FNRceceiveType"
            Else
                _Cmd &= vbCrLf & ",L.FTNameEN AS FNRceceiveType"
            End If

            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO  WITH(NOLOCK) ON R.FTReceiveNo=RO.FTReceiveNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge AS CD WITH(NOLOCK) ON R.FTInvoiceNo=CD.FTInvoiceNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(select L.FTNameTH,L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData AS L WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where L.FTListName='FNRceceiveType' "
            _Cmd &= vbCrLf & ")  AS L ON R.FNRceceiveType=L.FNListIndex"
            _Cmd &= vbCrLf & "  where R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'"
            _Cmd &= vbCrLf & " GROUP BY ro.FNPrice,L.FTNameTH,R.FDReceiveDate,R.FTReceiveBy,R.FTReceiveNo,R.FTPurchaseNo"


            Me.ogcallocate.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogvallocate.ExpandAllGroups()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetDataByInv()
        Try
            Dim _Cmd As String = ""
            Dim _PO As String = ""

            'End If


            _Cmd = "select R.FTInvoiceNo,RO.FTOrderNo,sum(ro.FNQuantity) as FNQuantity,sum(ro.FNPrice*ro.FNQuantity) as FNNetAmt ,CO.FNAmount,P.FNHSysSuplId"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO  WITH(NOLOCK) ON R.FTReceiveNo=RO.FTReceiveNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge AS CD WITH(NOLOCK) ON R.FTInvoiceNo=CD.FTInvoiceNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) ON R.FTPurchaseNo=P.FTPurchaseNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order AS CO WITH(NOLOCK) ON R.FTInvoiceNo=CO.FTInvoiceNo  and RO.FTOrderNo=CO.FTOrderNo and P.FNHSysSuplId=CO.FNHSysSuplId "

            _Cmd &= vbCrLf & "  where R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'"
            _Cmd &= vbCrLf & " GROUP BY R.FTInvoiceNo,RO.FTOrderNo,CO.FNAmount,P.FNHSysSuplId"

            Me.ogcAllo.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogvAllo.ExpandAllGroups()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetDataByType()
        Try
            Dim _Cmd As String = ""
            Dim _PO As String = ""

            'End If


            _Cmd = "select R.FTInvoiceNo,RO.FTOrderNo,sum(ro.FNQuantity) as FNQuantity,sum(ro.FNPrice*ro.FNQuantity) as FNNetAmt ,CO.FNAmount,P.FNHSysSuplId,R.FNRceceiveType"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive as R WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO  WITH(NOLOCK) ON R.FTReceiveNo=RO.FTReceiveNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge AS CD WITH(NOLOCK) ON R.FTInvoiceNo=CD.FTInvoiceNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) ON R.FTPurchaseNo=P.FTPurchaseNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge_Order AS CO WITH(NOLOCK) ON R.FTInvoiceNo=CO.FTInvoiceNo  and RO.FTOrderNo=CO.FTOrderNo and P.FNHSysSuplId=CO.FNHSysSuplId "

            _Cmd &= vbCrLf & "  where R.FTInvoiceNo='" & Me.FTInvoiceNo.Text & "'"
            _Cmd &= vbCrLf & " GROUP BY R.FTInvoiceNo,RO.FTOrderNo,CO.FNAmount,P.FNHSysSuplId,R.FNRceceiveType"
            _Cmd &= vbCrLf & "order by RO.FTOrderNo,R.FNRceceiveType  asc"

            Me.ogctype.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogvtype.ExpandAllGroups()
        Catch ex As Exception
        End Try
    End Sub

End Class
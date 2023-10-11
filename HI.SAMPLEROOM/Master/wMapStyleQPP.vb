Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wMapStyleQPP
    Private _Popup As wMapStyleQPPAdd
    Private ds As DataSet
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogv)

        _Popup = New wMapStyleQPPAdd
        HI.TL.HandlerControl.AddHandlerObj(_Popup)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Popup.Name.ToString.Trim, _Popup)
        Catch ex As Exception
        End Try
        '  Call InitGrid()
    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()

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

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            '.OptionsSelection.MultiSelect = True
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .ExpandAllGroups()
            .RefreshData()


        End With




    End Sub
#End Region
    Private _DocNo As String = ""
    Public Property DocNo As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
        End Set
    End Property

    Private _WhId As Integer = 0
    Public Property WhId As Integer
        Get
            Return _WhId
        End Get
        Set(value As Integer)
            _WhId = value
        End Set
    End Property

    Public _CmpId As Integer = 0
    Public Property CmpId As Integer
        Get
            Return _CmpId
        End Get
        Set(value As Integer)
            _CmpId = value
        End Set
    End Property

    Private _StateSetSelectAll As Boolean = True
    Private _oDtselect As DataTable = Nothing
    Public Property oDtselect As DataTable
        Get
            Return _oDtselect
        End Get
        Set(value As DataTable)
            _oDtselect = value
        End Set
    End Property
    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property



    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmLoad_Click(sender As Object, e As EventArgs) Handles ocmLoad.Click
        LoadData()
    End Sub

    Private Sub wCountryForwarderMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        'RemoveHandler RepositoryItemButtonFNHSysSuplId.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
        LoadData()
        ' Me.ogvchild.ViewCaption = "Style"
    End Sub

    Private Sub ocmAdd_Click(sender As Object, e As EventArgs) Handles ocmAddDT.Click
        Try
            HI.TL.HandlerControl.ClearControl(_Popup)
            With _Popup
                .DocNo = ""
                .ShowDialog()
                If (.State) Then
                    LoadData()
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.sp_get_MapStylelist "
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)



            Me.ogc.DataSource = _oDt.Copy
            Me.ogv.BestFitColumns()

            ds = New DataSet

            _Cmd = "Exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.sp_get_MapStylelist_Child "
            ds.Tables.Add(HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE))


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv_DoubleClick(sender As Object, e As EventArgs) Handles ogv.DoubleClick
        Try
            With ogv
                If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub
                Dim _MapCodeRef As String = "" : Dim _SeaSonId As String = ""
                _MapCodeRef = .GetRowCellValue(.FocusedRowHandle, "FTMapStyleCode")
                _SeaSonId = .GetRowCellValue(.FocusedRowHandle, "FNHSysSeasonId")
                HI.TL.HandlerControl.ClearControl(_Popup)

                With _Popup
                    .DocNo = _MapCodeRef
                    .FNHSysSeasonId.Text = _SeaSonId

                    .ShowDialog()

                    If (.State) Then
                        LoadData()
                    End If
                End With
            End With

        Catch ex As Exception

        End Try
    End Sub



    Private Sub ogv_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles ogv.MasterRowGetRelationName
        e.RelationName = "Style"
    End Sub

    Private Sub ogv_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles ogv.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

    Private Sub ogv_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles ogv.MasterRowGetChildList
        Try
            Try
                Dim view As GridView = DirectCast(sender, GridView)
                e.ChildList = GetDetail(ogv.GetRowCellValue(ogv.FocusedRowHandle, "FTMapStyleCode").ToString)

            Catch ex As Exception
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetDetail(Key As String) As DataView
        Try
            Dim dt As DataTable = ds.Tables(0)
            Dim dv As DataView = New DataView(dt)

            dv.RowFilter = "FTMapStyleCode='" & Key & "'"
            dv.AllowEdit = False
            dv.AllowNew = False
            dv.AllowDelete = False


            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Private Sub ogv_MasterRowEmpty(sender As Object, e As MasterRowEmptyEventArgs) Handles ogv.MasterRowEmpty
        e.IsEmpty = False
    End Sub

    Private Sub ogv_MasterRowExpanded(sender As Object, e As CustomMasterRowEventArgs) Handles ogv.MasterRowExpanded
        ogvchild.BestFitColumns()
    End Sub
End Class
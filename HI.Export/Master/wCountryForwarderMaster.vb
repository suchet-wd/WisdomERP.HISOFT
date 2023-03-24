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

Public Class wCountryForwarderMaster
    Private _Popup As wCountryForwarderMasterAdd
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogv)

        _Popup = New wCountryForwarderMasterAdd
        HI.TL.HandlerControl.AddHandlerObj(_Popup)


        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Popup.Name.ToString.Trim, _Popup)
        Catch ex As Exception

        End Try





        Call InitGrid()
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
        RemoveHandler RepositoryItemButtonFNHSysSuplId.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
        LoadData()
    End Sub

    Private Sub ocmAdd_Click(sender As Object, e As EventArgs) Handles ocmAdd.Click
        Try
            HI.TL.HandlerControl.ClearControl(_Popup)
            With _Popup
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
            _Cmd = "Exec  get_load_TCMMCountryForwarder"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)



            With Me.ogv
                .OptionsView.ShowAutoFilterRow = False
                .OptionsView.ColumnAutoWidth = False
                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FNHSysCountryId".ToUpper, "FTRemark".ToUpper, "FTCountryCode".ToUpper, "FTCountryName".ToUpper

                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False


                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next
                Dim _colcount As Integer = 0
                If Not (_oDt Is Nothing) Then
                    For Each Col As DataColumn In _oDt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FNHSysCountryId".ToUpper, "FTRemark".ToUpper, "FTCountryCode".ToUpper, "FTCountryName".ToUpper


                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .OptionsColumn.AllowEdit = False
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = "c" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString
                                    .VisibleIndex = _colcount

                                End With

                                .Columns.Add(ColG)

                                'Dim Ctrl As New Object
                                'With .Columns(Col.ColumnName.ToString)

                                '    .OptionsFilter.AllowAutoFilter = False
                                '    .OptionsFilter.AllowFilter = False

                                '    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                '    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far



                                '    '.ColumnEdit = RepositoryItemButtonFNHSysSuplId
                                'End With

                                .Columns(Col.ColumnName.ToString).Width = 100

                        End Select

                    Next

                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        With GridCol
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                    Next

                End If

            End With
            Me.ogc.DataSource = _oDt.Copy


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv_DoubleClick(sender As Object, e As EventArgs) Handles ogv.DoubleClick
        Try
            With ogv
                If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub
                Dim _CountryCode As String = ""
                _CountryCode = .GetRowCellValue(.FocusedRowHandle, "FTCountryCode")
                Dim _Countryint As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysCountryId"))
                HI.TL.HandlerControl.ClearControl(_Popup)

                With _Popup
                    .FNHSysCountryId.Properties.Tag = _Countryint
                    .FNHSysCountryId.Text = _CountryCode


                    .ShowDialog()

                    If (.State) Then
                        LoadData()
                    End If
                End With
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class
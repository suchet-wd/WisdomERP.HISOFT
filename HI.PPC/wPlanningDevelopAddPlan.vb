Imports System.Drawing

Public Class wPlanningDevelopAddPlan


    Private _ListDataSubOrderOrg As New List(Of DataTable)
    Public _ListDataSubOrder As New List(Of DataTable)
    Public _ListDataSubOrderProd As New List(Of DataTable)
    Private _ListDataSubOrderBal As New List(Of DataTable)
    Private _ListDataSubOrderBlank As New List(Of DataTable)
    Private _StateSumGrid As Boolean = False
    Private _StateGridSelect As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        AddHandler RepSubOrderCal.Spin, AddressOf HI.TL.HandlerControl.Caledit_Spin

    End Sub

#Region "Property"

    Private _Process As Boolean = False
    Property Process As Boolean
        Get
            Return _Process
        End Get
        Set(value As Boolean)
            _Process = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Private Sub InitGrid()
        Try
            With Me.ogvsub
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        Try
            With Me.ogvsuborderorg
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        Try
            With Me.ogvsuborder
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        Try
            With Me.ogvsubprod
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        With ogvsub
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
        End With

        With ogvsuborderorg
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

        With ogvsuborder
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

        With ogvsubprod
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

    End Sub

    Private Sub InitData(Key As String)
        ogcsub.DataSource = Nothing
        ogcsuborderorg.DataSource = Nothing
        ogcsuborder.DataSource = Nothing
        ogcsubprod.DataSource = Nothing

        _ListDataSubOrderOrg.Clear()
        _ListDataSubOrder.Clear()
        _ListDataSubOrderProd.Clear()
        _ListDataSubOrderBal.Clear()
        _ListDataSubOrderBlank.Clear()

        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _dtSub As DataTable
        Dim _dtProd As DataTable
        Dim _Total As Integer = 0

        _Qry = " SELECT     '0'  As FTStateSelect"
        _Qry &= vbCrLf & "	,M.FTSubOrderNo"
        _Qry &= vbCrLf & "	, CASE WHEN ISDATE(M.FDSubOrderDate) =1 THEN Convert(varchar(10),Convert(datetime,M.FDSubOrderDate),103) Else '' END AS FDSubOrderDate"
        _Qry &= vbCrLf & "	, C.FTCountryCode"
        _Qry &= vbCrLf & "	, SCT.FTContinentCode"
        _Qry &= vbCrLf & "	, SHM.FTShipModeCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "	, C.FTCountryNameTH AS FTCountryName "
        Else
            _Qry &= vbCrLf & "	, C.FTCountryNameEN AS FTCountryName"
        End If

        _Qry &= vbCrLf & "	, CASE WHEN ISDATE(M.FDShipDate) =1 THEN Convert(varchar(10),Convert(datetime,M.FDShipDate),103) Else '' END AS FDShipDate"
        _Qry &= vbCrLf & "	, Convert(numeric(18,0),ISNULL(M.FNSubOrderQty,0) + ISNULL(M.FNExtraQty,0) + ISNULL(M.FNGarmentQtyTest,0)) AS FNTotalQty"
        _Qry &= vbCrLf & " ,ISNULL(SMOD.FNSam,ISNULL(SM.FNSam,0)) AS FNSam"
        _Qry &= vbCrLf & "	 FROM        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_TMERTOrderSub AS M WITH(NOLOCK) ON A.FTOrderNo=M.FTOrderNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS C WITH(NOLOCK) ON M.FNHSysCountryId = C.FNHSysCountryId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle AS SM WITH(NOLOCK) ON A.FNHSysStyleId = SM.FNHSysStyleId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS SMOD WITH(NOLOCK) ON A.FNHSysStyleId = SMOD.FNHSysStyleId AND A.FTOrderNo = SMOD.FTOrderNo AND M.FTSubOrderNo=SMOD.FTSubOrderNo"
        _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS SCT WITH(NOLOCK) ON M.FNHSysContinentId = SCT.FNHSysContinentId"
        _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS SHM WITH(NOLOCK) ON M.FNHSysShipModeId = SHM.FNHSysShipModeId"
        _Qry &= vbCrLf & "	 WHERE  A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "'"
        _Qry &= vbCrLf & "	 ORDER BY M.FTSubOrderNo"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dt.Rows

            _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.SP_Get_SubOrderBreakDown_Planning '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
            _dtSub = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PLANNING)

            _Qry = "SELECT  TOP 0  FTOrderProdNo, FTColorway, FTSizeBreakDown, FNQuantity"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail  AS T WITH(NOLOCK) "
            _Qry &= vbCrLf & "	 WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "'"
            _Qry &= vbCrLf & "	 AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
            _dtProd = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Dim M As New DataTable
            M = _dtSub.Copy
            _ListDataSubOrderOrg.Add(M)

            Dim M1 As New DataTable
            M1 = _dtSub.Copy

            For Each Rx As DataRow In _dtProd.Select("FTOrderProdNo<>'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "'")

                For Each Rx2 As DataRow In M1.Select("FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "' ")

                    If M1.Columns.IndexOf(Rx!FTSizeBreakDown.ToString) >= 0 Then
                        Rx2.Item(Rx!FTSizeBreakDown.ToString) = Rx2.Item(Rx!FTSizeBreakDown.ToString) - Val(Rx!FNQuantity)
                    End If

                    Exit For

                Next

            Next

            For Each Rx As DataRow In M1.Rows
                _Total = 0
                For Each Col As DataColumn In M1.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                        Case Else
                            _Total = _Total + Val(Rx.Item(Col.ColumnName.ToString))
                    End Select
                Next
                Rx.Item("Total") = _Total
            Next

            _ListDataSubOrderBal.Add(M1)

            Dim M2 As New DataTable
            M2 = _dtSub.Copy

            For Each Rx As DataRow In _dtProd.Rows
                For Each Rx2 As DataRow In M2.Select("FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "' ")

                    If M1.Columns.IndexOf(Rx!FTSizeBreakDown.ToString) >= 0 Then
                        Rx2.Item(Rx!FTSizeBreakDown.ToString) = Rx2.Item(Rx!FTSizeBreakDown.ToString) - Val(Rx!FNQuantity)
                    End If

                    Exit For
                Next
            Next

            For Each Rx As DataRow In M2.Rows
                _Total = 0
                For Each Col As DataColumn In M2.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                        Case Else
                            _Total = _Total + Val(Rx.Item(Col.ColumnName.ToString))
                    End Select
                Next
                Rx.Item("Total") = _Total
            Next

            _ListDataSubOrderProd.Add(M2)

            Dim M3 As New DataTable
            Dim M4 As New DataTable
            M3 = _dtSub.Copy
            For Each Rx As DataRow In M3.Rows
                _Total = 0
                For Each Col As DataColumn In M3.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                        Case Else
                            Rx.Item(Col.ColumnName.ToString) = 0
                            '  _Total = _Total + Val(Rx.Item(Col.ColumnName.ToString))
                    End Select
                Next
                Rx.Item("Total") = 0
            Next
            M4 = M3.Copy

            For Each Rx As DataRow In _dtProd.Select("FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "'")
                For Each Rx2 As DataRow In M3.Select("FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "' ")

                    If M1.Columns.IndexOf(Rx!FTSizeBreakDown.ToString) >= 0 Then
                        Rx2.Item(Rx!FTSizeBreakDown.ToString) = Rx2.Item(Rx!FTSizeBreakDown.ToString) + Val(Rx!FNQuantity)
                    End If

                    Exit For
                Next
            Next

            _ListDataSubOrder.Add(M3)

            _ListDataSubOrderBlank.Add(M4)
        Next

        Me.ogcsub.DataSource = _dt
        Me.ogvsub.BestFitColumns()
    End Sub

    Private Sub InitGridSubOrderOrg(Optional _dt As DataTable = Nothing)
        Dim _colcount As Integer = 0
        With Me.ogvsuborderorg

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        Case Else
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                _colcount = _colcount + 1

                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                                .Visible = True

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                            .Columns(Col.ColumnName.ToString).Width = 50
                    End Select

                Next

            End If

            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                With GridCol
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                    If (.OptionsColumn.AllowEdit) Then
                        .AppearanceCell.BackColor = Color.LightCyan
                    End If
                End With
            Next
            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcsuborderorg.DataSource = _dt


    End Sub

    Private Sub InitGridSubOrder(Optional _dt As DataTable = Nothing)

        Dim _colcount As Integer = 0
        With Me.ogvsuborder

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next


            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                                If Not (Col.ColumnName.ToString = "Total") Then
                                    .ColumnEdit = RepSubOrderCal
                                End If

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                If Not (Col.ColumnName.ToString = "Total") Then
                                    .AppearanceCell.BackColor = Drawing.Color.LightCyan
                                    .AppearanceCell.ForeColor = Color.Blue
                                End If

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = Not (Col.ColumnName.ToString = "Total")

                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                            .Columns(Col.ColumnName.ToString).Width = 50
                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcsuborder.DataSource = _dt

    End Sub

    Private Sub InitGridSubOrderProd(Optional _dt As DataTable = Nothing)
        Dim _colcount As Integer = 0
        With Me.ogvsubprod

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        Case Else
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                _colcount = _colcount + 1

                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                                .Visible = True

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                            .Columns(Col.ColumnName.ToString).Width = 50
                    End Select

                Next

            End If

            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                With GridCol
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                    If (.OptionsColumn.AllowEdit) Then
                        .AppearanceCell.BackColor = Color.LightCyan
                    End If
                End With
            Next
            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcsubprod.DataSource = _dt


    End Sub

    Private Sub SumGrid()
        _StateSumGrid = True
        Try
            Dim _Total As Integer = 0
            With Me.ogvsuborder
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                    Select Case GridCol.FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                        Case Else
                            If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                _Total = _Total + CDbl(.GetFocusedRowCellValue(GridCol))
                            Else
                                _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
                            End If
                            '  _Total = _Total + CDbl(.GetFocusedRowCellValue(GridCol))
                    End Select

                Next

                .SetFocusedRowCellValue("Total", _Total)
            End With
            _Total = 0
            With Me.ogvsubprod
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                    Select Case GridCol.FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                        Case Else
                            If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                _Total = _Total + CDbl(.GetFocusedRowCellValue(GridCol))
                            Else
                                _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
                            End If
                            '  _Total = _Total + CDbl(.GetFocusedRowCellValue(GridCol))
                    End Select

                Next

                .SetFocusedRowCellValue("Total", _Total)
            End With

            CType(ogcsuborder.DataSource, DataTable).AcceptChanges()
            CType(ogcsubprod.DataSource, DataTable).AcceptChanges()
            _Total = 0

            For Each Row As DataRow In CType(ogcsuborder.DataSource, DataTable).Rows

                _Total = _Total + CDbl(Row!Total)

            Next

            If _Total > 0 Then
                ogvsub.SetFocusedRowCellValue("FTStateSelect", "1")
            Else
                ogvsub.SetFocusedRowCellValue("FTStateSelect", "0")
            End If

            CType(Me.ogcsub.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception
        End Try
        _StateSumGrid = False
    End Sub


#End Region

#Region "Function"

#End Region

    Private Sub wGenerateJobProd_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        Me.FNGraphImageIndex.SelectedIndex = 0
        If FNCreateGraphProdType.Properties.Items.Count > 0 Then
            FNCreateGraphProdType.SelectedIndex = 0
        End If
        'Call InitGrid()
        'Call InitData()
        ' _Spls.Close()
    End Sub

    Private Sub ogvsub_BeforeLeaveRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles ogvsub.BeforeLeaveRow
        If Not (ogcsuborder.DataSource Is Nothing) Then
            CType(ogcsuborder.DataSource, DataTable).AcceptChanges()
        End If
    End Sub

    Private Sub ogvsub_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvsub.FocusedRowChanged

        With ogvsub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            ' Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
            Call InitGridSubOrderOrg(_ListDataSubOrderOrg(.FocusedRowHandle))
            Call InitGridSubOrder(_ListDataSubOrder(.FocusedRowHandle))
            Call InitGridSubOrderProd(_ListDataSubOrderProd(.FocusedRowHandle))
            '_Spls.Close()
        End With

    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Me.Process = False
        Me.Close()
    End Sub

#Region "Set Selecttion "

    Private Sub ogvsuborder_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvsuborder.FocusedColumnChanged

        If Not (_StateGridSelect) Then
            _StateGridSelect = True

            With ogvsuborder
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    _StateGridSelect = False
                    Exit Sub
                End If
            End With

            With ogvsubprod
                .FocusedRowHandle = ogvsuborder.FocusedRowHandle
                .FocusedColumn = ogvsuborder.FocusedColumn
            End With

            With ogvsuborderorg
                .FocusedRowHandle = ogvsuborder.FocusedRowHandle
                .FocusedColumn = ogvsuborder.FocusedColumn
            End With

            _StateGridSelect = False
        End If

    End Sub

    Private Sub ogvsuborder_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvsuborder.FocusedRowChanged

        If Not (_StateGridSelect) Then
            _StateGridSelect = True

            With ogvsuborder
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    _StateGridSelect = False
                    Exit Sub
                End If
            End With

            With ogvsubprod
                .FocusedRowHandle = ogvsuborder.FocusedRowHandle
                .FocusedColumn = ogvsuborder.FocusedColumn
            End With

            With ogvsuborderorg
                .FocusedRowHandle = ogvsuborder.FocusedRowHandle
                .FocusedColumn = ogvsuborder.FocusedColumn
            End With

            _StateGridSelect = False
        End If

    End Sub

    Private Sub ogvsuborderorg_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvsuborderorg.FocusedColumnChanged
        If Not (_StateGridSelect) Then
            _StateGridSelect = True

            With ogvsuborderorg
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    _StateGridSelect = False
                    Exit Sub
                End If
            End With

            With ogvsuborder
                .FocusedRowHandle = ogvsuborderorg.FocusedRowHandle
                .FocusedColumn = ogvsuborderorg.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            With ogvsubprod
                .FocusedRowHandle = ogvsuborderorg.FocusedRowHandle
                .FocusedColumn = ogvsuborderorg.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            _StateGridSelect = False
        End If
    End Sub

    Private Sub ogvsuborderorg_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvsuborderorg.FocusedRowChanged
        If Not (_StateGridSelect) Then
            _StateGridSelect = True

            With ogvsuborderorg
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    _StateGridSelect = False
                    Exit Sub
                End If
            End With

            With ogvsuborder
                .FocusedRowHandle = ogvsuborderorg.FocusedRowHandle
                .FocusedColumn = ogvsuborderorg.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            With ogvsubprod
                .FocusedRowHandle = ogvsuborderorg.FocusedRowHandle
                .FocusedColumn = ogvsuborderorg.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            _StateGridSelect = False
        End If
    End Sub

    Private Sub ogvsubprod_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvsubprod.FocusedColumnChanged
        If Not (_StateGridSelect) Then
            _StateGridSelect = True

            With ogvsubprod
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    _StateGridSelect = False
                    Exit Sub
                End If
            End With

            With ogvsuborder
                .FocusedRowHandle = ogvsubprod.FocusedRowHandle
                .FocusedColumn = ogvsubprod.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            With ogvsuborderorg
                .FocusedRowHandle = ogvsubprod.FocusedRowHandle
                .FocusedColumn = ogvsubprod.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            _StateGridSelect = False
        End If
    End Sub

    Private Sub ogvsubprod_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvsubprod.FocusedRowChanged
        If Not (_StateGridSelect) Then
            _StateGridSelect = True

            With ogvsubprod
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    _StateGridSelect = False
                    Exit Sub
                End If
            End With

            With ogvsuborder
                .FocusedRowHandle = ogvsubprod.FocusedRowHandle
                .FocusedColumn = ogvsubprod.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            With ogvsuborderorg
                .FocusedRowHandle = ogvsubprod.FocusedRowHandle
                .FocusedColumn = ogvsubprod.FocusedColumn
                .SelectCell(.FocusedRowHandle, .FocusedColumn)
            End With

            _StateGridSelect = False
        End If
    End Sub

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)

        Dim _Qry As String = ""
        Dim _dt As DataTable
        _Qry = "  SELECT TOP 1 B.FNGraphImageIndex,A.FPOrderImage1,ISNULL(SM.FNSam,0) AS FNSam,ISNULL(SS.FTSeasonCode,'') AS FTSeasonCode"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTCmpRunImage AS B WITH(NOLOCK)  ON A.FNHSysCmpRunId = B.FNHSysCmpRunId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle AS SM WITH(NOLOCK) ON A.FNHSysStyleId = SM.FNHSysStyleId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON A.FNHSysSeasonId = SS.FNHSysSeasonId"
        _Qry &= vbCrLf & "  WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.FNGraphImageIndex.SelectedIndex = 0
        Me.FPImage.Image = Nothing
        Me.FNSam.Value = 0
        Me.FNHSysSeasonId.Text = ""

        For Each Rx As DataRow In _dt.Rows
            Try
                Me.FNGraphImageIndex.SelectedIndex = Integer.Parse(Val(Rx!FNGraphImageIndex.ToString))
            Catch ex As Exception
                Me.FNGraphImageIndex.SelectedIndex = 0
            End Try

            Try
                Me.FPImage.Image = HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPOrderImage1)
            Catch ex As Exception
            End Try

            Me.FNSam.Value = Val(Rx!FNSam.ToString())
            Me.FNHSysSeasonId.Text = Rx!FTSeasonCode.ToString

            Exit For
        Next
       
    End Sub

#End Region

    Private Sub RepSubOrderCal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepSubOrderCal.EditValueChanging
        Try
            Dim _NewValue As Integer = e.NewValue
            Dim _OrgValue As Integer = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""

            If e.NewValue < 0 Then
                e.Cancel = True
            Else

                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")

                    For Each R As DataRow In _ListDataSubOrderBal(Me.ogvsub.FocusedRowHandle).Select("FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' ")
                        Try
                            _OrgValue = Val(R.Item(_Size))
                        Catch ex As Exception
                        End Try
                        Exit For
                    Next

                    If _OrgValue < _NewValue Then
                        e.Cancel = True
                    Else

                        If Not (_StateSumGrid) Then

                            ogvsubprod.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_OrgValue - _NewValue))
                            ogvsuborder.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))
                            CType(ogcsuborder.DataSource, DataTable).AcceptChanges()
                            CType(ogcsubprod.DataSource, DataTable).AcceptChanges()

                            If Not (ogcsuborder.DataSource Is Nothing) Then
                                Call SumGrid()
                            End If

                        End If

                    End If
                End With
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try

    End Sub

    Private Sub RepFTStateSelect_CheckedChanged(sender As Object, e As EventArgs) Handles RepFTStateSelect.CheckedChanged

        CType(Me.ogcsub.DataSource, DataTable).AcceptChanges()

        If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
            _ListDataSubOrderProd(ogvsub.FocusedRowHandle) = _ListDataSubOrderBlank(ogvsub.FocusedRowHandle).Copy
            _ListDataSubOrder(ogvsub.FocusedRowHandle) = _ListDataSubOrderBal(ogvsub.FocusedRowHandle).Copy
        Else
            _ListDataSubOrderProd(ogvsub.FocusedRowHandle) = _ListDataSubOrderBal(ogvsub.FocusedRowHandle).Copy
            _ListDataSubOrder(ogvsub.FocusedRowHandle) = _ListDataSubOrderBlank(ogvsub.FocusedRowHandle).Copy
        End If

        Call InitGridSubOrder(_ListDataSubOrder(ogvsub.FocusedRowHandle))
        Call InitGridSubOrderProd(_ListDataSubOrderProd(ogvsub.FocusedRowHandle))

    End Sub

    Private Sub ocmcreate_Click(sender As Object, e As EventArgs) Handles ocmcreate.Click

        CType(Me.ogcsub.DataSource, DataTable).AcceptChanges()
        CType(Me.ogcsuborder.DataSource, DataTable).AcceptChanges()

        If CType(Me.ogcsub.DataSource, DataTable).Select("FTStateSelect='1' ").Length > 0 Then

            If CType(Me.ogcsub.DataSource, DataTable).Select("FTStateSelect='1' AND FNSam<=0").Length > 0 Then

                HI.MG.ShowMsg.mInfo("ข้อมูล Sam ไม่ถูกต้อง กรุณาทำการตรวจสอบ !!!", 15111205487, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub

            End If

            If Me.FNHSysUnitSectId.Text <> "" Then
                Me.Process = True
                Me.Close()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitSectId_lbl.Text)
                FNHSysUnitSectId.Focus()
            End If

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูล", 1484108801, Me.Text)
        End If

    End Sub


    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else

            If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysCmpId=" & Val(FNHSysCmpId.Properties.Tag.ToString) & " AND  A.FTStateOrderApp='1'  AND  (A.FNOrderType IN(0,13,17,22))  AND A.FTOrderNoRef=''", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then

                Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
                Me.FNGraphImageIndex.SelectedIndex = 0

                Call LoadOrderProdDataInfo(FTOrderNo.Text)

                If FNCreateGraphProdType.Properties.Items.Count > 0 Then
                    FNCreateGraphProdType.SelectedIndex = 0
                End If

                Call InitGrid()
                Call InitData(FTOrderNo.Text.Trim)

                _Spls.Close()

            Else

                Me.ogcsub.DataSource = Nothing
                Me.ogcsuborder.DataSource = Nothing
                Me.ogcsuborderorg.DataSource = Nothing
                Me.ogcsubprod.DataSource = Nothing

            End If

        End If

    End Sub


End Class
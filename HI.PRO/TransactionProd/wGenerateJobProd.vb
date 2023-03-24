Imports System.Drawing

Public Class wGenerateJobProd

    Private _ListDataSubOrderOrg As New List(Of DataTable)
    Private _ListDataSubOrder As New List(Of DataTable)
    Private _ListDataSubOrderProd As New List(Of DataTable)
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

    Private _JobProdNo As String = ""
    Property JobProdNo As String
        Get
            Return _JobProdNo
        End Get
        Set(value As String)
            _JobProdNo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

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

    Private Sub InitData()
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

        _Qry = " SELECT     Case WHEN ISNULL(Prod.FTSubOrderNo,'') <>'' THEN '1' ELSE '0' END As FTStateSelect"
        _Qry &= vbCrLf & "	,M.FTSubOrderNo"
        _Qry &= vbCrLf & "	, CASE WHEN ISDATE(M.FDSubOrderDate) =1 THEN Convert(varchar(10),Convert(datetime,M.FDSubOrderDate),103) Else '' END AS FDSubOrderDate"
        _Qry &= vbCrLf & "	, C.FTCountryCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "	, C.FTCountryNameTH AS FTCountryName "
        Else
            _Qry &= vbCrLf & "	, C.FTCountryNameEN AS FTCountryName"
        End If

        _Qry &= vbCrLf & "	, CASE WHEN ISDATE(M.FDShipDate) =1 THEN Convert(varchar(10),Convert(datetime,M.FDShipDate),103) Else '' END AS FDShipDate"
        _Qry &= vbCrLf & "	, Convert(numeric(18,0),ISNULL(M.FNSubOrderQty,0) + ISNULL(M.FNExtraQty,0) + ISNULL(M.FNGarmentQtyTest,0)) AS FNTotalQty"
        _Qry &= vbCrLf & "	 FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_TMERTOrderSub AS M WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS C WITH(NOLOCK) ON M.FNHSysCountryId = C.FNHSysCountryId"
        _Qry &= vbCrLf & "		LEFT OUTER JOIN (SELECT DISTINCT  FTSubOrderNo"
        _Qry &= vbCrLf & "	  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail As C WITH(NOLOCK)"
        _Qry &= vbCrLf & "	  WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
        _Qry &= vbCrLf & "	  AND  FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.JobProdNo) & "' "
        _Qry &= vbCrLf & "	 ) AS Prod ON M.FTSubOrderNo = Prod.FTSubOrderNo"
        _Qry &= vbCrLf & "	 WHERE  M.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
        _Qry &= vbCrLf & "	 ORDER BY M.FTSubOrderNo"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dt.Rows

            _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_SubOrderBreakDown '" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
            _dtSub = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


            _Qry = "SELECT   FTOrderProdNo, FTColorway, FTSizeBreakDown, FNQuantity"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail  AS T WITH(NOLOCK) "
            _Qry &= vbCrLf & "	 WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
            _Qry &= vbCrLf & "	 AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
            _dtProd = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Dim M As New DataTable
            M = _dtSub.Copy
            _ListDataSubOrderOrg.Add(M)

            Dim M1 As New DataTable
            M1 = _dtSub.Copy

            For Each Rx As DataRow In _dtProd.Select("FTOrderProdNo<>'" & HI.UL.ULF.rpQuoted(Me.JobProdNo) & "'")

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

            For Each Rx As DataRow In _dtProd.Select("FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.JobProdNo) & "'")
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

    Private Function CreateNewJobProducttion() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Generating Job Production...Please Wait")
        Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _dtjobprod As DataTable
        Dim _tmpOrderProd As String = ""

        Dim I As Integer = 0
        Try

            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In CType(ogcsub.DataSource, DataTable).Rows

                If R!FTStateSelect.ToString = "1" Then
                    For Each Rx As DataRow In _ListDataSubOrder(I).Rows

                        _OrderNo = Rx!FTOrderNo.ToString
                        _SubOrderNo = Rx!FTSubOrderNo.ToString
                        _ColorWay = Rx!FTColorway.ToString

                        For Each Col As DataColumn In _ListDataSubOrder(I).Columns
                            Select Case Col.ColumnName.ToString.ToUpper
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                                Case Else

                                    If (Integer.Parse(Rx.Item(Col.ColumnName.ToString))) > 0 Then
                                        _Qry = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd "
                                        _Qry &= vbCrLf & " ( FTUserLogIn, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity)"
                                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & " "

                                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
                                            _Spls.Close()
                                            Return False

                                        End If

                                    End If

                            End Select
                        Next
                    Next

                End If

                I = I + 1
            Next

            Select Case Me.FNCreateJobProdType.SelectedIndex
                Case 0
                    _Qry = " SELECT        ISNULL(S.FNHSysCountryId,0) AS FNHSysCountryId"
                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
                    _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "  GROUP BY ISNULL(S.FNHSysCountryId,0)"

                    _dtjobprod = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry)

                    For Each R As DataRow In _dtjobprod.Rows
                        _tmpOrderProd = ""

                        _Qry = "   SELECT  TOP 1 FTOrderProdNo"
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd"
                        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'  AND LEN(FTOrderProdNo) = LEN('" & HI.UL.ULF.rpQuoted(Me.OrderNo & "-P001") & "') "
                        _Qry &= vbCrLf & "  ORDER BY FTOrderProdNo DESC "
                        _tmpOrderProd = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

                        If _tmpOrderProd = "" Then
                            _tmpOrderProd = Me.OrderNo & "-P001"
                        Else
                            _tmpOrderProd = Me.OrderNo & "-P" & Microsoft.VisualBasic.Right("0000" & Format(Val(Microsoft.VisualBasic.Right(_tmpOrderProd, 3)) + 1, "0"), 3)
                        End If

                        _Qry &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd "
                        _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo,FNHSysCmpId)"
                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                        _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail "
                        _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTOrderProdNo, FTSubOrderNo,FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCmpId)"
                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
                        _Qry &= vbCrLf & ",T.FTSubOrderNo"
                        _Qry &= vbCrLf & ",T.FTColorway"
                        _Qry &= vbCrLf & ",T.FTSizeBreakDown"
                        _Qry &= vbCrLf & ",T.FNQuantity"
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
                        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
                        _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " AND ISNULL(S.FNHSysCountryId,0)=" & Val(R!FNHSysCountryId) & ""

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If


                    Next

                Case 1

                    _Qry = " SELECT        ISNULL(S.FNHSysCountryId,0) AS FNHSysCountryId,T.FTColorway"
                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
                    _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
                    _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "  GROUP BY ISNULL(S.FNHSysCountryId,0),T.FTColorway"

                    _dtjobprod = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry)

                    For Each R As DataRow In _dtjobprod.Rows
                        _tmpOrderProd = ""

                        _Qry = "   SELECT  TOP 1 FTOrderProdNo"
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd"
                        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'  AND LEN(FTOrderProdNo) = LEN('" & HI.UL.ULF.rpQuoted(Me.OrderNo & "-P001") & "') "
                        _Qry &= vbCrLf & "  ORDER BY FTOrderProdNo DESC "
                        _tmpOrderProd = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

                        If _tmpOrderProd = "" Then
                            _tmpOrderProd = Me.OrderNo & "-P001"
                        Else
                            _tmpOrderProd = Me.OrderNo & "-P" & Microsoft.VisualBasic.Right("0000" & Format(Val(Microsoft.VisualBasic.Right(_tmpOrderProd, 3)) + 1, "0"), 3)
                        End If

                        _Qry &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd "
                        _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo,FNHSysCmpId)"
                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                        _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail "
                        _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTOrderProdNo, FTSubOrderNo,FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCmpId)"
                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
                        _Qry &= vbCrLf & ",T.FTSubOrderNo"
                        _Qry &= vbCrLf & ",T.FTColorway"
                        _Qry &= vbCrLf & ",T.FTSizeBreakDown"
                        _Qry &= vbCrLf & ",T.FNQuantity"
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
                        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
                        _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " AND ISNULL(S.FNHSysCountryId,0)=" & Val(R!FNHSysCountryId) & ""
                        _Qry &= vbCrLf & " AND T.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If


                    Next

                Case 2

                    _Qry = " SELECT      T.FTColorway"
                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
                    _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
                    _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "  GROUP BY T.FTColorway"

                    _dtjobprod = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry)

                    For Each R As DataRow In _dtjobprod.Rows
                        _tmpOrderProd = ""

                        _Qry = "   SELECT  TOP 1 FTOrderProdNo"
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd"
                        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' AND LEN(FTOrderProdNo) = LEN('" & HI.UL.ULF.rpQuoted(Me.OrderNo & "-P001") & "') "
                        _Qry &= vbCrLf & "  ORDER BY FTOrderProdNo DESC "
                        _tmpOrderProd = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

                        If _tmpOrderProd = "" Then
                            _tmpOrderProd = Me.OrderNo & "-P001"
                        Else
                            _tmpOrderProd = Me.OrderNo & "-P" & Microsoft.VisualBasic.Right("0000" & Format(Val(Microsoft.VisualBasic.Right(_tmpOrderProd, 3)) + 1, "0"), 3)
                        End If

                        _Qry &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd "
                        _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo,FNHSysCmpId)"
                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                        _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail "
                        _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTOrderProdNo, FTSubOrderNo,FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCmpId)"
                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
                        _Qry &= vbCrLf & ",T.FTSubOrderNo"
                        _Qry &= vbCrLf & ",T.FTColorway"
                        _Qry &= vbCrLf & ",T.FTSizeBreakDown"
                        _Qry &= vbCrLf & ",T.FNQuantity"
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
                        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
                        _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " AND T.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If


                    Next

            End Select


            Try

                _Qry = "   UPDATE A SET FTStateCut ='1'	 "
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                _Qry &= vbCrLf & "      INNER Join"
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & "   SELECT FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail"
                _Qry &= vbCrLf & "   WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')"
                _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "     WHERE A.FTStateCut <>'1'"
                _Qry &= vbCrLf & "   IF @@ROWCOUNT <=0"
                _Qry &= vbCrLf & "    BEGIN"
                _Qry &= vbCrLf & "      INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus("
                _Qry &= vbCrLf & "      FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "      	,FTStateCut, FTStateSew, FTStatePack)"
                _Qry &= vbCrLf & "      SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "      ," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "      ," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "      ,B.FTOrderNo"
                _Qry &= vbCrLf & "      ,B.FTSubOrderNo "
                _Qry &= vbCrLf & "      ,'1'"
                _Qry &= vbCrLf & "      ,'0'"
                _Qry &= vbCrLf & "      ,'0'"
                _Qry &= vbCrLf & "       FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                _Qry &= vbCrLf & "       RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "       ("
                _Qry &= vbCrLf & "       SELECT FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "        FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail"
                _Qry &= vbCrLf & "       WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')"
                _Qry &= vbCrLf & "       GROUP BY FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "       ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "        AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "        WHERE A.FTOrderNo Is NULL"
                _Qry &= vbCrLf & "    END "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Catch ex As Exception
            End Try

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
            _Spls.Close()
            Return False
        End Try
        HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
        _Spls.Close()
        Return True
    End Function

    Private Function UpdatejobProducttion() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Updating Job Production...Please Wait")
        Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _tmpOrderProd As String = ""

        Dim I As Integer = 0
        Try

            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



            For Each R As DataRow In CType(ogcsub.DataSource, DataTable).Rows

                If R!FTStateSelect.ToString = "1" Then
                    For Each Rx As DataRow In _ListDataSubOrder(I).Rows

                        _OrderNo = Rx!FTOrderNo.ToString
                        _SubOrderNo = Rx!FTSubOrderNo.ToString
                        _ColorWay = Rx!FTColorway.ToString

                        For Each Col As DataColumn In _ListDataSubOrder(I).Columns
                            Select Case Col.ColumnName.ToString.ToUpper
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                                Case Else

                                    If (Integer.Parse(Rx.Item(Col.ColumnName.ToString))) > 0 Then
                                        _Qry = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd "
                                        _Qry &= vbCrLf & " ( FTUserLogIn, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity)"
                                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & " "

                                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
                                            _Spls.Close()
                                            Return False

                                        End If

                                    End If

                            End Select
                        Next
                    Next

                End If

                I = I + 1
            Next

            _tmpOrderProd = Me.JobProdNo

            _Qry = "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail "
            _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTOrderProdNo, FTSubOrderNo,FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCmpId)"
            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
            _Qry &= vbCrLf & ",T.FTSubOrderNo"
            _Qry &= vbCrLf & ",T.FTColorway"
            _Qry &= vbCrLf & ",T.FTSizeBreakDown"
            _Qry &= vbCrLf & ",T.FNQuantity"
            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T "
            _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _Spls.Close()
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
            _Spls.Close()
            Return False
        End Try
        HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
        _Spls.Close()
        Return True
    End Function

#End Region

    Private Sub wGenerateJobProd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        If FNCreateJobProdType.Properties.Items.Count > 0 Then
            FNCreateJobProdType.SelectedIndex = 0
        End If
        Call InitGrid()
        Call InitData()
        _Spls.Close()
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
#End Region

    'Private Sub RepSubOrderCal_EditValueChanged(sender As Object, e As EventArgs) Handles RepSubOrderCal.EditValueChanged
    '    If Not (_StateSumGrid) Then
    '        CType(ogcsubprod.DataSource, DataTable).AcceptChanges()
    '        CType(ogcsuborder.DataSource, DataTable).AcceptChanges()

    '        If Not (ogcsuborder.DataSource Is Nothing) Then
    '            CType(ogcsuborder.DataSource, DataTable).AcceptChanges()
    '            Call SumGrid()
    '        End If

    '    End If
    'End Sub

    Private Sub RepSubOrderCal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepSubOrderCal.EditValueChanging
        Try
            Dim _NewValue As Integer = e.NewValue
            Dim _OrgValue As Integer = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""
            Dim _StateEditData As Boolean = False
            Dim _TableCutQty As Integer = 0
            Dim _Qry As String = ""

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    If Me.JobProdNo <> "" Then
                        _StateEditData = True
                    End If

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")

                    If _StateEditData Then

                        _Qry = "SELECt MAX(FNQuantity) AS FNQuantity"
                        _Qry &= vbCrLf & " FROM (  SELECT FNHSysMarkId, FTColorway, FTSizeBreakDown, SUM(FNQuantity) AS FNQuantity"
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS M WITH(NOLOCK)"
                        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.JobProdNo) & "'"
                        _Qry &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'"
                        _Qry &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_Size) & "'"
                        _Qry &= vbCrLf & "  GROUP BY FNHSysMarkId, FTColorway, FTSizeBreakDown"
                        _Qry &= vbCrLf & " ) X"

                        _TableCutQty = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")))

                    End If

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

                            Dim _StatePass As Boolean = False

                            If _StateEditData Then
                                If _TableCutQty > _NewValue Then
                                    e.Cancel = True
                                Else
                                    _StatePass = True
                                End If
                            Else
                                _StatePass = True
                            End If

                            If _StatePass Then

                                ogvsubprod.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_OrgValue - _NewValue))
                                ogvsuborder.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))
                                CType(ogcsuborder.DataSource, DataTable).AcceptChanges()
                                CType(ogcsubprod.DataSource, DataTable).AcceptChanges()

                                If Not (ogcsuborder.DataSource Is Nothing) Then
                                    Call SumGrid()
                                End If

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
        Try
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

        Catch ex As Exception

        End Try
       
    End Sub

    Private Sub ocmcreate_Click(sender As Object, e As EventArgs) Handles ocmcreate.Click

        CType(Me.ogcsub.DataSource, DataTable).AcceptChanges()
        CType(Me.ogcsuborder.DataSource, DataTable).AcceptChanges()

        If CType(Me.ogcsub.DataSource, DataTable).Select("FTStateSelect='1'").Length > 0 Then

            If Me.JobProdNo = "" Then

                If Me.CreateNewJobProducttion Then
                    HI.MG.ShowMsg.mInfo("Generate Job Producttion Complete !!!", 1404110001, Me.Text)
                    Me.Process = True
                    Me.Close()
                Else
                    HI.MG.ShowMsg.mInfo("Can not Generate Job Producttion, Fail !!!", 1404110002, Me.Text)
                End If

            Else

                If Me.UpdatejobProducttion Then

                    HI.MG.ShowMsg.mInfo("Update Job Producttion Complete !!!", 1404110006, Me.Text)
                    Me.Process = True
                    Me.Close()

                Else
                    HI.MG.ShowMsg.mInfo("Can not Update Job Producttion, Fail !!!", 1404110007, Me.Text)
                End If

            End If

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูล", 1404100001, Me.Text)
        End If

    End Sub

    Private Sub RepFTStateSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTStateSelect.EditValueChanging
        Try

            If Me.JobProdNo <> "" Then

                If e.OldValue.ToString = "1" Then
                    Dim _Qry As String = ""

                    _Qry = "SELECT TOP 1 FTOrderProdNo "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS X WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.JobProdNo) & "'"

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                        e.Cancel = True
                        HI.MG.ShowMsg.mInfo("ข้อมูลเอกสารได้ถูกนำไปสร้าง โต๊ะตัดแล้ว ไม่สามารถทำการยกเลิกได้ !!!", 1511300549, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Else
                        e.Cancel = False
                    End If

                Else
                    e.Cancel = False
                End If

            Else
                e.Cancel = False
            End If

        Catch ex As Exception
        End Try
    End Sub
End Class
Imports System.Drawing

Public Class wCMOrderBreakDown

    Private _ListDataSubOrderOrg As New List(Of DataTable)
    Private _ListDataSubOrder As New List(Of DataTable)
    Private _ListDataSubOrderProd As New List(Of DataTable)
    Private _ListDataSubOrderBal As New List(Of DataTable)
    Private _ListDataSubOrderBlank As New List(Of DataTable)
    Private _StateSumGrid As Boolean = False
    Private _StateGridSelect As Boolean = False
    'Private _CombinePopub As wPopupCreateOrderCombine

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        AddHandler RepSubOrderCal.Spin, AddressOf HI.TL.HandlerControl.Caledit_Spin


        '_CombinePopub = New wPopupCreateOrderCombine
        'HI.TL.HandlerControl.AddHandlerObj(_CombinePopub)
        'Dim oSysLang As New HI.ST.SysLanguage
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CombinePopub.Name.ToString.Trim, _CombinePopub)
        'Catch ex As Exception
        'End Try

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

    Private _SubOrderNo As String = ""
    Property SubOrderNo As String
        Get
            Return _SubOrderNo
        End Get
        Set(value As String)
            _SubOrderNo = value
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

    Public Sub InitGrid()

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

        With ogvsubordersum
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

    End Sub

    Public Function InitData() As Boolean
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

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "	, C.FTCountryNameTH AS FTCountryName "
        Else
            _Qry &= vbCrLf & "	, C.FTCountryNameEN AS FTCountryName"
        End If

        _Qry &= vbCrLf & "	, CASE WHEN ISDATE(M.FDShipDate) =1 THEN Convert(varchar(10),Convert(datetime,M.FDShipDate),103) Else '' END AS FDShipDate"
        _Qry &= vbCrLf & "	, Convert(numeric(18,0),ISNULL(M.FNSubOrderQty,0) + ISNULL(M.FNExtraQty,0) + ISNULL(M.FNGarmentQtyTest,0)) AS FNTotalQty"
        _Qry &= vbCrLf & "	 FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_TMERTOrderSub AS M WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS C WITH(NOLOCK) ON M.FNHSysCountryId = C.FNHSysCountryId"
        _Qry &= vbCrLf & "	LEFT OUTER JOIN (SELECT DISTINCT  FTOrderNo, FTSubOrderNo "
        _Qry &= vbCrLf & "	 FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS A (NOLOCK) "
        _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
        _Qry &= vbCrLf & "	) AS Prod ON M.FTOrderNo = Prod.FTOrderNo AND M.FTSubOrderNo =Prod.FTSubOrderNo "
        _Qry &= vbCrLf & " LEFT OUTER JOIN  ("
        _Qry &= vbCrLf & " SELECT DISTINCT  D.FTOrderNo, D.FTSubOrderNo"
        _Qry &= vbCrLf & " FROM  (SELECT   A.FTOrderNo, B.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS A (NOLOCK) INNER JOIN "
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
        _Qry &= vbCrLf & "  AND A.FTOrderNo = B.FTOrderNo"
        _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
        _Qry &= vbCrLf & "  GROUP BY A.FTOrderNo, B.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
        _Qry &= vbCrLf & "  ) AS X"
        _Qry &= vbCrLf & " RIGHT JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS D WITH(NOLOCK) "
        _Qry &= vbCrLf & " ON X.FTOrderNo = D.FTOrderNo and X.FTSubOrderNo = D.FTSubOrderNo and X.FTColorway = D.FTColorway and X.FTSizeBreakDown = D.FTSizeBreakDown"
        _Qry &= vbCrLf & "  WHERE D.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' AND  D.FNQuantity - isnull(X.FNQuantity, 0) > 0"
        _Qry &= vbCrLf & "	         )  AS T  ON M.FTOrderNo = T.FTOrderNo and M.FTSubOrderNo = T.FTSubOrderNo"
        _Qry &= vbCrLf & "	 WHERE  M.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
        '_Qry &= vbCrLf & "	And M.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.SubOrderNo) & "'"
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

        Me.ogcsub.DataSource = _dt.Copy
        Me.ogvsub.BestFitColumns()

        _dt.Dispose()

        If _dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

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
        Call InitGirdSubOrderSum()

    End Sub

    Private Sub InitGirdSubOrderSum()
        Dim _oDt As New DataTable
        With _oDt.Columns
            .Add("FTOrderNo", GetType(String))
            .Add("FTSubOrderNo", GetType(String))
            .Add("FTColorway", GetType(String))
            .Add("FTPOLine", GetType(String))
        End With
        Try

            For i As Integer = 0 To (_ListDataSubOrder.ToList.Count - 1)
                For Each R As DataRow In _ListDataSubOrder(i).Rows
                    If _oDt.Select("FTOrderNo='" & R!FTOrderNo.ToString & "' and FTColorway='" & R!FTColorway.ToString & "'").Length <= 0 Then
                        _oDt.Rows.Add(R!FTOrderNo.ToString, R!FTSubOrderNo.ToString, R!FTColorway.ToString, "")
                    End If
                    For Each col As DataColumn In _ListDataSubOrder(i).Columns
                        Select Case col.ColumnName.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                            Case Else
                                If _oDt.Columns.IndexOf(col.ColumnName.ToString) < 0 Then
                                    _oDt.Columns.Add(col.ColumnName.ToString, GetType(Integer))
                                End If
                                For Each rx As DataRow In _oDt.Select("FTOrderNo='" & R!FTOrderNo.ToString & "'  and FTColorway='" & R!FTColorway.ToString & "'")
                                    rx.Item(col.ColumnName.ToString) = Val("0" & rx.Item(col.ColumnName.ToString)) + Val(R.Item(col.ColumnName.ToString))
                                Next
                        End Select
                    Next

                Next
            Next


            Dim _colcount As Integer = 0
            With Me.ogvsubordersum

                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next


                If Not (_oDt Is Nothing) Then
                    For Each Col As DataColumn In _oDt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
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
                                        .AllowEdit = False

                                    End With

                                End With

                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                                .Columns(Col.ColumnName.ToString).Width = 50
                        End Select

                    Next

                End If


            End With

            Me.ogcsubordersum.DataSource = _oDt


        Catch ex As Exception
        End Try
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

        Call InitGirdSubOrderSum()
        _StateSumGrid = False

    End Sub


#End Region

#Region "Function"

    Private Function CreateNewOrderNo() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Combine Order Breakdown...Please Wait")
        '  Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _POLine As String = ""
        Dim _dtjobprod As DataTable
        Dim _tmpOrderProd As String = ""
        Dim _oDtSub As DataTable
        Dim _oDtMain As DataTable

        Dim I As Integer = 0
        Try

            Dim _KeyNew As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            Dim _OrderNew As String = ""

            _Qry = "Select  FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FTPORef"
            _Qry &= vbCrLf & ",(SELECT Top 1   FTStyleCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH(NOLOCK) WHERE T.FNHSysStyleId = O.FNHSysStyleId) AS FNHSysStyleId "
            _Qry &= vbCrLf & ",(SELECT Top 1  FTCmpCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) Where C.FNHSysCmpId = O.FNHSysCmpId ) AS FNHSysCmpId "
            _Qry &= vbCrLf & ",(SELECT Top 1  FTSeasonCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH (NOLOCK) Where S.FNHSysSeasonId = O.FNHSysSeasonId) AS FNHSysSeasonId "

            _Qry &= vbCrLf & ",(Select Top 1 FTCmpRunCode  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun AS M   WITH(NOLOCK)Where M.FNHSysCmpRunId = O.FNHSysCmpRunId  ) AS  FNHSysCmpRunId "
            _Qry &= vbCrLf & ",FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTStateOrderApp, FTAppBy, FDAppDate, FTAppTime, FNJobState, FTStateBy, FDStateDate, "
            _Qry &= vbCrLf & " FTStateTime, FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTCancelAppBy, FDCancelAppDate, FDCancelAppTime, FTCancelAppRemark, FTPOTradingCo, FTPOItem, "
            _Qry &= vbCrLf & " FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, FDImportDate, FTImportTime, FPOrderImage1, "
            _Qry &= vbCrLf & "  FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, FTUserChangeOrderImage1, FDDateChangeOrderImage2, "
            _Qry &= vbCrLf & "  FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4, "
            _Qry &= vbCrLf & " FTUserChangeOrderImage4, FTOrderNoRef, FTStateSendDirectorApp, FTStateSendDirectorBy, FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, "
            _Qry &= vbCrLf & " FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, "
            _Qry &= vbCrLf & "  FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, FDStateFactoryRejectDate, FTStateFactoryRejectTime, FTChangeCmpBy, FDChangeCmpDate, FTChangeCmpTime,"
            _Qry &= vbCrLf & "FNHSysStyleIdPull "
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & OrderNo.ToString & "' "
            _oDtMain = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _OrderNew = HI.TL.Document.GetDocumentNo("HITECH_MERCHAN", "TMERTOrder", _oDtMain.Rows(0).Item("FNOrderType").ToString, False, HI.TL.CboList.GetListRefer("FNOrderType", _oDtMain.Rows(0).Item("FNOrderType").ToString) & _oDtMain.Rows(0).Item("FNHSysCmpRunId").ToString()).ToString
            _oDtMain.BeginInit()
            For Each R As DataRow In _oDtMain.Rows
                R!FTOrderNo = _OrderNew
            Next
            _oDtMain.EndInit()

            _KeyNew = HI.Conn.SQLConn.GetField("EXEC  SP_GEN_CHARACTER_SubOrderNo '" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "")

            _Qry = "Select  FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate "
            _Qry &= vbCrLf & " , (SELECT  Top 1  FTBuyCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B WITH(NOLOCK) WHERE B.FNHSysBuyId = O.FNHSysBuyId ) as FNHSysBuyId "
            _Qry &= vbCrLf & " ,(Select Top 1  FTContinentCode From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS C WITH(NOLOCK) WHERE C.FNHSysContinentId = O.FNHSysContinentId ) AS FNHSysContinentId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTCountryCode  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS C WITH (NOLOCK) WHERE C.FNHSysCountryId = O.FNHSysCountryId) AS FNHSysCountryId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTProvinceCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS P WITH(NOLOCK) WHERE P.FNHSysProvinceId = O.FNHSysProvinceId) AS FNHSysProvinceId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTShipModeCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS S WITH(NOLOCK) Where S.FNHSysShipModeId = O.FNHSysShipModeId ) AS FNHSysShipModeId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTCurCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH (NOLOCK) Where C.FNHSysCurId = O.FNHSysCurId ) AS FNHSysCurId "
            _Qry &= vbCrLf & " ,(SELECT TOP (1)  FTGenderCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender AS G WITH(NOLOCK) WHERE G.FNHSysGenderId = O.FNHSysGenderId) AS FNHSysGenderId "
            _Qry &= vbCrLf & " ,(SELECT Top 1   FTUnitCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) Where U.FNHSysUnitId = O.FNHSysUnitId ) AS FNHSysUnitId "
            _Qry &= vbCrLf & ",  (Select Top 1 FTShipPortCode  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipPort AS S WITH(NOLOCK) WHERE S.FNHSysShipPortId = O.FNHSysShipPortId ) AS FNHSysShipPortId "
            _Qry &= vbCrLf & " , FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, "
            _Qry &= vbCrLf & " FTOther3Note1, FTRemark,   FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton, FTSubOrderNoDivertRef,   FTStateSendDirectorApp, FTStateSendDirectorBy, "
            _Qry &= vbCrLf & " FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, "
            _Qry &= vbCrLf & " FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, "
            _Qry &= vbCrLf & " FDStateFactoryRejectDate, FTStateFactoryRejectTime --, FNImportGrpSeq"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS O WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTSubOrderNo='" & OrderNo.ToString & "-A' "
            _oDtSub = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            _oDtSub.BeginInit()
            For Each R As DataRow In _oDtSub.Rows
                R!FTSubOrderNo = _KeyNew
            Next
            _oDtSub.EndInit()

            Dim AllNewJob As String = ""

            'New SubOrderNO
            'With _CombinePopub
            '    If Me.FNCombineType.SelectedIndex = 1 Then
            '        .OrderNo = _oDtMain
            '        .ogrpOrderFactory.Visible = True
            '    Else
            '        .ogrpOrderFactory.Visible = False
            '        .OrderNo = Nothing
            '    End If
            '    .SubOrderNo = _oDtSub
            '    .FTRemarkSubOrderNo.Text = ""
            '    _Spls.Close()
            '    .ShowDialog()
            '    _Spls = New HI.TL.SplashScreen("Save Data...Please Wait")
            '    If (.State) Then
            '        If Me.FNCombineType.SelectedIndex = 1 Then

            '            _OrderNew = HI.TL.Document.GetDocumentNo("HITECH_MERCHAN", "TMERTOrder", _oDtMain.Rows(0).Item("FNOrderType").ToString, False, HI.TL.CboList.GetListRefer("FNOrderType", _oDtMain.Rows(0).Item("FNOrderType").ToString) & _oDtMain.Rows(0).Item("FNHSysCmpRunId").ToString()).ToString

            '            If AllNewJob = "" Then
            '                AllNewJob = _OrderNew
            '            Else
            '                AllNewJob = AllNewJob & "|" & _OrderNew
            '            End If

            '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder "
            '            _Qry &= "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, "
            '            _Qry &= "FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark,   "
            '            _Qry &= "  FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTPOTradingCo, FTPOItem, "
            '            _Qry &= "FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, FDImportDate, FTImportTime, FPOrderImage1, "
            '            _Qry &= "FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, FTUserChangeOrderImage1, FDDateChangeOrderImage2, "
            '            _Qry &= " FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4, "
            '            _Qry &= "FTUserChangeOrderImage4, FTOrderNoRef,  FNHSysStyleIdPull"
            '            _Qry &= " , FTStateCombine ,FTOrderNoBombineRef)"

            '            _Qry &= vbCrLf & " Select  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNew) & "'"
            '            _Qry &= vbCrLf & ", FDOrderDate "
            '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '            _Qry &= vbCrLf & "  , FNOrderType"
            '            _Qry &= vbCrLf & " , " & Integer.Parse("0" & .FNHSysCmpId.Properties.Tag)
            '            _Qry &= vbCrLf & " ," & Integer.Parse("0" & .FNHSysCmpRunId.Properties.Tag)
            '            _Qry &= vbCrLf & " , FNHSysStyleId,'" & HI.UL.ULF.rpQuoted(.FTPORef.Text.Trim()) & "', "
            '            _Qry &= vbCrLf & "FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark,  "
            '            _Qry &= vbCrLf & "    FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTPOTradingCo, FTPOItem,"
            '            _Qry &= vbCrLf & "FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, FDImportDate, FTImportTime, FPOrderImage1,"
            '            _Qry &= vbCrLf & "FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, FTUserChangeOrderImage1, FDDateChangeOrderImage2,"
            '            _Qry &= vbCrLf & " FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4,"
            '            _Qry &= vbCrLf & " FTUserChangeOrderImage4, FTOrderNoRef, FNHSysStyleIdPull , '1' ,FTOrderNo"
            '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder"
            '            _Qry &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"

            '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '                HI.Conn.SQLConn.Tran.Rollback()
            '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '                _Spls.Close()
            '                Return False
            '            End If

            '        End If

            '        _KeyNew = HI.Conn.SQLConn.GetField("EXEC  SP_GEN_CHARACTER_SubOrderNo '" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "")

            '        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub "
            '        _Qry &= "( [FTInsUser],[FDInsDate],[FTInsTime] ,[FTOrderNo],[FTSubOrderNo],[FDSubOrderDate],[FTSubOrderBy] ,[FDProDate],[FDShipDate],[FNHSysContinentId] ,[FNHSysCountryId],[FNHSysProvinceId],[FNHSysShipModeId],[FNHSysCurId]"
            '        _Qry &= ",[FNHSysGenderId],[FNHSysUnitId],[FTStateEmb] ,[FTStatePrint],[FTStateHeat],[FTStateLaser],[FTStateWindows] ,[FTRemark],[FNHSysShipPortId]"
            '        _Qry &= " ,[FTCustRef],[FTPORef], FTStateCombine ,FTOrderNoBombineRef,FNHSysPlantId,FNHSysBuyGrpId,FNOrderSetType)"
            '        _Qry &= vbCrLf & " Select  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            '        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '        _Qry &= vbCrLf & ",'" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'"
            '        _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            '        _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(.FDSubOrderDate.Text) & "'"
            '        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '        _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(.FDProDate.Text) & "'"
            '        _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(.FDShipDate.Text) & "'"
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysContinentId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysCountryId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysProvinceId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysShipModeId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysCurId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysGenderId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysUnitId.Properties.Tag)
            '        _Qry &= vbCrLf & ",'0' , '0','0','0','0'"
            '        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemarkSubOrderNo.Text) & "'"
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysShipPortId.Properties.Tag)
            '        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTCustRef.Text) & "'"
            '        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSubPORef.Text.Trim()) & "'"
            '        _Qry &= vbCrLf & ",'1','" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysPlantId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & Integer.Parse(.FNHSysBuyGrpId.Properties.Tag)
            '        _Qry &= vbCrLf & "," & FNCombineType.SelectedIndex


            '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            _Spls.Close()
            '            Return False
            '        End If

            '    Else
            '        HI.Conn.SQLConn.Tran.Rollback()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        _Spls.Close()
            '        Return False
            '    End If
            'End With



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
                                        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown "
                                        _Qry &= vbCrLf & " Set  FNQuantity = (FNQuantity - " & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & " )"
                                        _Qry &= vbCrLf & ", FNAmt = (FNAmt - ( FNPrice * " & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & "))"
                                        _Qry &= vbCrLf & ", FNGrandQuantity = (FNGrandQuantity -" & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & ") "
                                        _Qry &= vbCrLf & ", FNGrandAmnt = (FNGrandAmnt - ( FNPrice * " & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & "))"
                                        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown"
                                        _Qry &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                        _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                        _Qry &= vbCrLf & " And FTColorway='" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                        _Qry &= vbCrLf & " And FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "

                                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
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

            Dim _oDtSum As DataTable = CType(Me.ogcsubordersum.DataSource, DataTable)
            For Each R As DataRow In _oDtSum.Rows
                _OrderNo = R!FTOrderNo.ToString
                _SubOrderNo = R!FTSubOrderNo.ToString
                _ColorWay = R!FTColorway.ToString
                _POLine = R!FTPOLine.ToString

                For Each Col As DataColumn In _oDtSum.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                        Case Else

                            If Integer.Parse(R.Item(Col.ColumnName.ToString)) > 0 Then
                                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown"
                                _Qry &= "(FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNHSysMatColorId, "
                                _Qry &= "  FNHSysMatSizeId, FNExtraQty, FNQuantityExtra, FNGrandQuantity, FNAmntExtra, FNGrandAmnt, FNGarmentQtyTest, FNAmntQtyTest, FNPriceOrg,  FNCMDisPer, FNCMDisAmt ,FTNikePOLineItem,FNOperateFee,FNOperateFeeAmt,FNNetFOB)"
                                _Qry &= vbCrLf & "Select TOP 1 N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                _Qry &= vbCrLf & ",'" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'"
                                _Qry &= vbCrLf & ",'" & _KeyNew & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                _Qry &= vbCrLf & ",FNPrice"
                                _Qry &= vbCrLf & "," & Integer.Parse(R.Item(Col.ColumnName.ToString)) & " "
                                _Qry &= vbCrLf & ",( FNPrice * " & Integer.Parse(R.Item(Col.ColumnName.ToString)) & ")"
                                _Qry &= vbCrLf & ",FNHSysMatColorId,FNHSysMatSizeId,0,0"
                                _Qry &= vbCrLf & "," & Integer.Parse(R.Item(Col.ColumnName.ToString)) & " "
                                _Qry &= vbCrLf & " ,0"
                                _Qry &= vbCrLf & ",( FNPrice * " & Integer.Parse(R.Item(Col.ColumnName.ToString)) & ")"
                                _Qry &= vbCrLf & ",0,0"
                                _Qry &= vbCrLf & ", FNPriceOrg,  FNCMDisPer, FNCMDisAmt"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "'"
                                _Qry &= vbCrLf & ",FNOperateFee,FNOperateFeeAmt,FNNetFOB"
                                _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown"
                                _Qry &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                ' _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                _Qry &= vbCrLf & " And FTColorway='" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                _Qry &= vbCrLf & " And FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    _Spls.Close()
                                    Return False
                                End If
                            End If

                    End Select
                Next
            Next


            For Each R As DataRow In CType(ogcsub.DataSource, DataTable).Rows
                If R!FTStateSelect.ToString = "1" Then
                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component"
                    _Qry &= "(FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq)"
                    _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'"
                    _Qry &= vbCrLf & ",'" & _KeyNew & "'"
                    _Qry &= vbCrLf & ", FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq"
                    _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew"
                    _Qry &= "(FTInsUser, FDInsDate, FTInsTime , FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage)"
                    _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'"
                    _Qry &= vbCrLf & ",'" & _KeyNew & "'"
                    _Qry &= vbCrLf & ",FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                    _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack"
                    _Qry &= "( FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage)"
                    _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'"
                    _Qry &= vbCrLf & ",'" & _KeyNew & "'"
                    _Qry &= vbCrLf & ", FNPackSeq, FTPackDescription, FTPackNote, FTImage"
                    _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec"
                    _Qry &= "( FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId)"
                    _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & IIf(Me.FNCombineType.SelectedIndex = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'"
                    _Qry &= vbCrLf & ",'" & _KeyNew & "'"
                    _Qry &= vbCrLf & ",  FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId"
                    _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                End If

                Exit For
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Dim cmdstring As String

            cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each Job As String In AllNewJob.Split("|")

                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(Job) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            Next

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
            _Spls.Close()
            Return False
        End Try
        ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
        _Spls.Close()
        Return True
    End Function

#End Region

    Private Sub wGenerateJobProd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        If FNCombineType.Properties.Items.Count > 0 Then
            FNCombineType.SelectedIndex = 0
        End If
        'Call InitGrid()
        'Call InitData()
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

    Private Sub ogvsubprod_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvsubprod.FocusedColumnChanged, ogvsubordersum.FocusedColumnChanged
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

    Private Sub ogvsubprod_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvsubprod.FocusedRowChanged, ogvsubordersum.FocusedRowChanged
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
        Try
            Dim _Total As Integer = 0
            With CType(Me.ogcsubordersum.DataSource, DataTable)
                .AcceptChanges()
                For Each r As DataRow In .Rows
                    _Total += +Integer.Parse("0" & r!Total.ToString)
                Next
            End With
            If _Total > 0 Then
                Call CreateNewOrderNo()
                Process = True
                Me.Close()
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูลที่ต้องการทำการ Combine !!!", 1511260754, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
        End Try
    End Sub


   
End Class
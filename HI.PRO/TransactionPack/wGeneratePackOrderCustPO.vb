Imports System.Drawing

Public Class wGeneratePackOrderCustPO

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

    Private _FNHSysStyleId As Integer = 0
    Property FNHSysStyleId As Integer
        Get
            Return _FNHSysStyleId
        End Get
        Set(value As Integer)
            _FNHSysStyleId = value
        End Set
    End Property

    Private _PackOrderNo As String = ""
    Property PackOrderNo As String
        Get
            Return _PackOrderNo
        End Get
        Set(value As String)
            _PackOrderNo = value
        End Set
    End Property

    Private _CustPONo As String = ""
    Property CustPONo As String
        Get
            Return _CustPONo
        End Get
        Set(value As String)
            _CustPONo = value
        End Set
    End Property

    Private _FNPackSetValue As Integer = 0
    Property FNPackSetValue As Integer
        Get
            Return _FNPackSetValue
        End Get
        Set(value As Integer)
            _FNPackSetValue = value
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
        _Qry &= vbCrLf & "	,M.FTOrderNo"
        _Qry &= vbCrLf & ", M.FTPOref AS FTCustomerPO  "
        _Qry &= vbCrLf & "	,M.FTSubOrderNo"

        _Qry &= vbCrLf & "	, C.FTCountryCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "	, C.FTCountryNameTH AS FTCountryName "
        Else
            _Qry &= vbCrLf & "	, C.FTCountryNameEN AS FTCountryName"
        End If

        _Qry &= vbCrLf & "	, CASE WHEN ISDATE(M.FDShipDate) =1 THEN Convert(datetime,M.FDShipDate) Else NULL END AS FDShipDate"
        _Qry &= vbCrLf & "	, Convert(numeric(18,0),ISNULL(M.FNTotalQty,0)) AS FNTotalQty"

        _Qry &= vbCrLf & "	,ISNULL(("
        _Qry &= vbCrLf & " SELECT TOP 1 '1' AS FTStatePack "
        _Qry &= vbCrLf & "	 FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail  AS PA WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE PA.FTPackNo<>'" & HI.UL.ULF.rpQuoted(Me.PackOrderNo) & "'"
        _Qry &= vbCrLf & " AND PA.FTOrderNo= M.FTOrderNo"
        _Qry &= vbCrLf & " AND PA.FTSubOrderNo= M.FTSubOrderNo"
        _Qry &= vbCrLf & "),'0') AS FTStatePack"
        _Qry &= vbCrLf & "	 FROM   "
        _Qry &= vbCrLf & "  ( "
        _Qry &= vbCrLf & " SELECT t.FTOrderNo "
        _Qry &= vbCrLf & "	 , t.FTSubOrderNo "
        _Qry &= vbCrLf & "	 , t.FTPOref "
        _Qry &= vbCrLf & " , t.FNHSysCountryId "
        _Qry &= vbCrLf & " , t.FDShipDate "
        _Qry &= vbCrLf & "	, SUM(t.FNGrandQuantity) As FNTotalQty "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination t  "
        _Qry &= vbCrLf & " inner join  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "..TMERTOrder o  on t.FTOrderNo = o.FTOrderNo"
        _Qry &= vbCrLf & " WHERE   (t.FNHSysStyleId =" & FNHSysStyleId & ") AND (t.FTPOref = N'" & HI.UL.ULF.rpQuoted(CustPONo) & "')  and o.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
        _Qry &= vbCrLf & " GROUP BY t.FTOrderNo "
        _Qry &= vbCrLf & " 	 , t.FTSubOrderNo"
        _Qry &= vbCrLf & " 	 , t.FTPOref"
        _Qry &= vbCrLf & " 	 , t.FNHSysCountryId"
        _Qry &= vbCrLf & " 	 , t.FDShipDate "
        _Qry &= vbCrLf & "   ) As M  INNER JOIN"
        _Qry &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry As C With(NOLOCK) On M.FNHSysCountryId = C.FNHSysCountryId"
        _Qry &= vbCrLf & "		LEFT OUTER JOIN (Select DISTINCT  FTSubOrderNo"
        _Qry &= vbCrLf & "	  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail As C With(NOLOCK)"
        _Qry &= vbCrLf & "	  WHERE   FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.PackOrderNo) & "' "
        _Qry &= vbCrLf & "	 ) AS Prod ON M.FTSubOrderNo = Prod.FTSubOrderNo"

        _Qry &= vbCrLf & "	 ORDER BY M.FTOrderNo,M.FTSubOrderNo"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dt.Rows

            _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_SubOrderBreakDown_Pack_ByCustPO_Partial '" & HI.UL.ULF.rpQuoted(CustPONo) & "','" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
            _dtSub = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = "SELECT   H.FTPackNo AS FTOrderProdNo, T.FTColorway, T.FTSizeBreakDown, T.FNQuantity,T.FTPOLine"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail  AS T WITH(NOLOCK) "
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS H WITH(NOLOCK) ON T.FTPackNo = H.FTPackNo "
            _Qry &= vbCrLf & "	 WHERE  T.FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
            _Qry &= vbCrLf & "	 AND  T.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
            _Qry &= vbCrLf & "	 AND (ISNULL( H.FTCustomerPO,'')='' OR  H.FTCustomerPO='" & HI.UL.ULF.rpQuoted(CustPONo) & "' ) "
            _dtProd = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Dim M As New DataTable
            M = _dtSub.Copy
            _ListDataSubOrderOrg.Add(M)

            Dim M1 As New DataTable
            M1 = _dtSub.Copy

            For Each Rx As DataRow In _dtProd.Select("FTOrderProdNo<>'" & HI.UL.ULF.rpQuoted(Me.PackOrderNo) & "'")

                For Each Rx2 As DataRow In M1.Select("FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "' and FTPOLine ='" & HI.UL.ULF.rpQuoted(Rx!FTPOLine.ToString) & "'")

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
                        Case "FTCustomerPO".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
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
                For Each Rx2 As DataRow In M2.Select("FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "'    and FTPOLine ='" & HI.UL.ULF.rpQuoted(Rx!FTPOLine.ToString) & "'")

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
                        Case "FTCustomerPO".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
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
                        Case "FTCustomerPO".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            Rx.Item(Col.ColumnName.ToString) = 0
                            '  _Total = _Total + Val(Rx.Item(Col.ColumnName.ToString))
                    End Select
                Next
                Rx.Item("Total") = 0
            Next
            M4 = M3.Copy

            For Each Rx As DataRow In _dtProd.Select("FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.PackOrderNo) & "'")
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

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
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

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

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

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
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
        Dim _Total As Integer = 0
        With Me.ogvsuborder
            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                Select Case GridCol.FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "Total".ToUpper
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
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "Total".ToUpper
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

        _StateSumGrid = False
    End Sub


#End Region

#Region "Function"

    Private Function CreateNewOrderPack() As Boolean
        CType(ogcsub.DataSource, DataTable).AcceptChanges()
        Dim _Spls As New HI.TL.SplashScreen("Generating Order Pack...Please Wait")
        Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.PackOrderNo) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _dtjobprod As DataTable
        Dim _tmpOrderProd As String = ""
        Dim _POLine As String = ""

        Dim Qty As Integer = 0

        Dim I As Integer = 0
        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            HI.Conn.SQLConn.Execute_Tran(_QryDelete, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In CType(ogcsub.DataSource, DataTable).Rows

                If R!FTStateSelect.ToString = "1" Then
                    For Each Rx As DataRow In _ListDataSubOrder(I).Rows

                        _OrderNo = Rx!FTOrderNo.ToString
                        _SubOrderNo = Rx!FTSubOrderNo.ToString
                        _ColorWay = Rx!FTColorway.ToString
                        _POLine = Rx!FTPOLine.ToString

                        For Each Col As DataColumn In _ListDataSubOrder(I).Columns
                            Select Case Col.ColumnName.ToString.Trim().ToUpper
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                Case Else

                                    Try
                                        Qty = Integer.Parse(Val(Rx.Item(Col.ColumnName.ToString)))
                                    Catch ex As Exception
                                        Qty = 0
                                    End Try

                                    If Qty > 0 Then

                                        _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail "
                                        _Qry &= vbCrLf & "  (FTInsUser, FDInsDate, FTInsTime, FTPackNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity,FTPOLine)"
                                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.PackOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & "," & Qty & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "

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

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return False
        End Try

        _Spls.Close()
        Return True
    End Function

#End Region

    Private Sub wGenerateJobProd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

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

    Private Sub RepSubOrderCal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepSubOrderCal.EditValueChanging
        Try
            Dim _NewValue As Integer = e.NewValue
            Dim _OrgValue As Integer = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""
            Dim _PORef As String = ""
            Dim _OrderNo As String = ""
            Dim _SubOrderNo As String = ""
            Dim _cmd As String = ""

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")
                    _OrderNo = .GetFocusedRowCellValue("FTOrderNo")
                    _SubOrderNo = .GetFocusedRowCellValue("FTSubOrderNo")

                    Dim _dttable As DataTable = DirectCast(_ListDataSubOrderBal(Me.ogvsub.FocusedRowHandle), DataTable)
                    _cmd = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_SubOrderBreakDown_Pack_ByCustPO_Partial '" & HI.UL.ULF.rpQuoted(CustPONo) & "','" & HI.UL.ULF.rpQuoted(_OrderNo) & "','" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "','1' "

                    _dttable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_MERCHAN)
                    Try
                        _OrgValue = _dttable.Rows(.FocusedRowHandle).Item(_Size)
                    Catch ex As Exception
                    End Try




                    'For Each R As DataRow In _ListDataSubOrderBal(Me.ogvsub.FocusedRowHandle).Select("FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' ")
                    '    Try
                    '        _OrgValue = Val(R.Item(_Size))
                    '    Catch ex As Exception
                    '    End Try
                    '    Exit For
                    'Next

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

        If CType(Me.ogcsub.DataSource, DataTable).Select("FTStateSelect='1'").Length > 0 Then

            If Me.CreateNewOrderPack Then

                HI.MG.ShowMsg.mInfo("Generate Order Pack Complete !!!", 1410140001, Me.Text)
                Me.Process = True
                Me.Close()

            Else
                HI.MG.ShowMsg.mInfo("Can not Order Pack Producttion, Fail !!!", 1410140002, Me.Text)
            End If

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูล", 1410140003, Me.Text)
        End If

    End Sub

End Class
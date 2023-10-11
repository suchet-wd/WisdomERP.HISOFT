Imports System.Drawing

Public Class wSMPChangeSuplProduction


    Private _ListDataMarkBreakDown As List(Of DataTable)
    Private _ListPurchaseOrder As New List(Of DataTable)


    Private _StateSubNew As Boolean = False

    Sub New()
        _StateSubNew = True
        ' This call is required by the designer.
        InitializeComponent()

        Dim oSysLang As New ST.SysLanguage
        Call TabChenge()
        _StateSubNew = False
    End Sub


#Region "Property"

#End Region

#Region "Procedure"

    Private Property ogcjobprod As Object

    Private Property ogcmainmark As Object

    Private Sub InitDataPurchaseOrder(OrderNoKey As String)
        _ListPurchaseOrder.Clear()

        Dim dt As DataTable
        Dim _Qry As String = ""

        _Qry = " SELECT  FTPurchaseNo  FROM "
        _Qry &= vbCrLf & "  ( "
        _Qry &= vbCrLf & "   Select A.FTPurchaseNo"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS C WITH(NOLOCK) ON B.FTRawMatCode = C.FTMainMatCode"
        _Qry &= vbCrLf & "   WHERE      (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey.ToString) & "')  "
        _Qry &= vbCrLf & "   AND     (C.FNMerMatType = 0) "
        _Qry &= vbCrLf & "   GROUP BY A.FTPurchaseNo "
        _Qry &= vbCrLf & "   UNION "
        _Qry &= vbCrLf & "   Select C.FTPurchaseNo"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS A WITH(NOLOCK) INNER JOIN "
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FNHSysWHId = B.FNHSysWHId INNER JOIN "
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo  INNER JOIN "
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        _Qry &= vbCrLf & "   WHERE      (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey.ToString) & "')  "
        _Qry &= vbCrLf & "      AND     (MM.FNMerMatType = 0) "
        _Qry &= vbCrLf & "   GROUP BY C.FTPurchaseNo "
        _Qry &= vbCrLf & "  ) AS A Order By FTPurchaseNo "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        If dt.Rows.Count <= 0 Then
            dt.Rows.Add("")
        End If

        _ListPurchaseOrder.Add(dt.Copy)
        dt.Dispose()
    End Sub

    Private Sub TabChenge()

        'ocmgeneratejobprod.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        'ocmdeletejobprod.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        'ocmaddsuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        'ocmdeletesuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        'ocmsavemainmark.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        'ocmsavesubmark.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        'ocmdeleteallsuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)

        'ocmaddtable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmdeletetable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmsavetable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmdeletealltable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmmappomatcolor.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)

        'ocmpreviewbytable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmpreviewalltable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmpreviewallprod.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmcopy.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    'Private Sub InitGridBreakdown()

    '    With ogvjobprod
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsMenu.EnableColumnMenu = False
    '        .OptionsMenu.ShowAutoFilterRowItem = False
    '        .OptionsFilter.AllowFilterEditor = False
    '        .OptionsFilter.AllowColumnMRUFilterList = False
    '        .OptionsFilter.AllowMRUFilterList = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
    '    End With

    '    With ogvsub
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsMenu.EnableColumnMenu = False
    '        .OptionsMenu.ShowAutoFilterRowItem = False
    '        .OptionsFilter.AllowFilterEditor = False
    '        .OptionsFilter.AllowColumnMRUFilterList = False
    '        .OptionsFilter.AllowMRUFilterList = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
    '    End With

    'End Sub

    Public Sub SetInfo(ByVal Key As Object)
        '...call by another form name zzz...
        FTSMPOrderNo.Text = Key.ToString
    End Sub

    'Public Sub LoadOrderProdDataInfo(ByVal Key As Object)
    '    If (_StateSubNew) Then Exit Sub
    '    Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
    '    Call ClearGrid()
    '    otbjobprod.TabPages.Clear()
    '    Dim _Qry As String = ""
    '    Dim _dtprod As DataTable

    '    _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTOrderProdNo  "
    '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P With(Nolock)"
    '    _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
    '    _Qry &= vbCrLf & "  Order By FTOrderProdNo  "

    '    _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    For Each R As DataRow In _dtprod.Rows

    '        Dim Otp As New DevExpress.XtraTab.XtraTabPage()
    '        With Otp
    '            .Name = R!FTOrderProdNo.ToString
    '            .Text = R!FTOrderProdNo.ToString
    '        End With

    '        otbjobprod.TabPages.Add(Otp)

    '    Next

    '    If _dtprod.Rows.Count > 0 Then
    '        otbjobprod.SelectedTabPageIndex = 0
    '    End If

    '    _dtprod.Dispose()

    '    Call InitDataPurchaseOrder(Key)

    '    _Spls.Close()
    'End Sub

    'Private Sub LoadOrderProdBreakDown(Key As Object)
    '    Dim _dt As DataTable
    '    Dim _Qry As String = ""
    '    Dim _colcount As Integer = 0

    '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    With Me.ogvjobprod

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next

    '        If Not (_dt Is Nothing) Then
    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1
    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = False
    '                                .ReadOnly = True
    '                            End With

    '                        End With

    '                        .Columns(Col.ColumnName.ToString).Width = 45
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select

    '            Next

    '        End If

    '        'If _colcount > 4 Then
    '        '    .BestFitColumns()
    '        'End If

    '    End With

    '    Me.ogcjobprod.DataSource = _dt.Copy
    '    'If _colcount > 4 Then
    '    '    ogvjobprod.BestFitColumns()
    '    'End If

    '    _colcount = 0
    '    With Me.ogvjobprodbal

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next

    '        If Not (_dt Is Nothing) Then
    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1
    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = False
    '                                .ReadOnly = True
    '                            End With

    '                        End With

    '                        .Columns(Col.ColumnName.ToString).Width = 45
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select

    '            Next

    '        End If

    '        'If _colcount > 4 Then
    '        '    .BestFitColumns()
    '        'End If

    '    End With

    '    Me.ogcjobprodbal.DataSource = _dt.Copy

    '    'If _colcount > 4 Then
    '    '    ogvjobprodbal.BestFitColumns()
    '    'End If

    'End Sub

    'Private Sub LoadOrderProdSubBreakDown(OrderProdNo As Object, SubOrderNo As Object)
    '    Dim _dt As DataTable
    '    Dim _Qry As String = ""
    '    Dim _colcount As Integer = 0

    '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdSubBreakDown '" & HI.UL.ULF.rpQuoted(OrderProdNo.ToString) & "','" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "' "
    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    With Me.ogvsub

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next

    '        If Not (_dt Is Nothing) Then
    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1
    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = False
    '                                .ReadOnly = True
    '                            End With

    '                        End With

    '                        .Columns(Col.ColumnName.ToString).Width = 45
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select

    '            Next

    '        End If

    '        'If _colcount > 4 Then
    '        '    .BestFitColumns()
    '        'End If

    '    End With

    '    Me.ogcsub.DataSource = _dt
    'End Sub

    'Private Sub LoadOrderProdDetail(Key As Object)
    '    Dim _Qry As String = ""
    '    Dim _dtprod As DataTable
    '    otbsuborder.TabPages.Clear()

    '    _Qry = "SELECT  FTSubOrderNo   "
    '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS P With(Nolock)"
    '    _Qry &= vbCrLf & "  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
    '    _Qry &= vbCrLf & "  Group By  FTSubOrderNo  "
    '    _Qry &= vbCrLf & "  Order By  FTSubOrderNo  "

    '    _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    For Each R As DataRow In _dtprod.Rows

    '        Dim Otp As New DevExpress.XtraTab.XtraTabPage()
    '        With Otp
    '            .Name = R!FTSubOrderNo.ToString
    '            .Text = R!FTSubOrderNo.ToString
    '        End With

    '        otbsuborder.TabPages.Add(Otp)

    '    Next

    '    If _dtprod.Rows.Count > 0 Then
    '        otbsuborder.SelectedTabPageIndex = 0
    '    End If

    '    _dtprod.Dispose()
    'End Sub

    'Private Sub InitGrid()

    '    With ogvjobprod
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    '    End With

    '    With ogvsub
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    '    End With

    '    With ogvmainmark
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    '    End With

    '    With ogvsubmark
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    '    End With

    '    With ogvmark
    '        .OptionsView.ShowAutoFilterRow = False
    '        '.OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelect = True
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect 'DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    '    End With

    '    With ogvjobprodbal
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    '    End With

    '    With ogvbalance
    '        .OptionsView.ShowAutoFilterRow = False
    '        .OptionsSelection.MultiSelect = False
    '        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    '    End With

    'End Sub

    'Private Sub ClearGrid(Optional Prod As Boolean = False)

    '    With Me.ogvjobprod
    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next
    '    End With

    '    With Me.ogvsub
    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next
    '    End With

    '    With Me.ogvjobprodbal
    '        For I As Integer = .Columns.Count - 1 To 0 Step -1

    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select

    '        Next
    '    End With

    '    ogcmainmark.DataSource = Nothing
    '    ogcsubmark.DataSource = Nothing
    '    ogcjobprod.DataSource = Nothing
    '    ogcsub.DataSource = Nothing

    '    If Not (Prod) Then
    '        Me.otbjobprod.TabPages.Clear()
    '    End If

    '    Me.otbsuborder.TabPages.Clear()
    '    Me.otbtable.TabPages.Clear()

    'End Sub

#End Region

#Region "Order Prod Mark"

    Private Sub LoadOrderProdMainMark(OrderProdKey As Object)
        Me.ogcmainmark.DataSource = Nothing
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = " SELECT        P.FTOrderProdNo, P.FNHSysMarkId AS FNHSysMarkId_Hide, M.FTMarkCode AS FNHSysMarkId,P.FTNote"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , M.FTMarkNameTH AS FNHSysMarkId_None"
        Else
            _Qry &= vbCrLf & " ,  M.FTMarkNameEN AS FNHSysMarkId_None "
        End If

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS P With(Nolock) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M   With(Nolock) ON P.FNHSysMarkId = M.FNHSysMarkId"
        _Qry &= vbCrLf & " WHERE        (P.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "')"
        _Qry &= vbCrLf & " Order By M.FTMarkCode ASC "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _dt.Rows.Add()

        Me.ogcmainmark.DataSource = _dt

    End Sub

    'Private Sub LoadOrderProdSubMark(OrderProdKey As Object, MainMarkKey As Object)
    '    Me.ogcsubmark.DataSource = Nothing
    '    Dim _Qry As String = ""
    '    Dim _dt As DataTable

    '    _Qry = " SELECT        P.FTOrderProdNo, P.FNHSysSubMarkId AS FNHSysMarkId_Hide, M.FTMarkCode AS FNHSysMarkId"
    '    _Qry &= vbCrLf & " ,P.FNHSysMarkId AS FNHSysMainMarkId,P.FTNote"

    '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '        _Qry &= vbCrLf & " , M.FTMarkNameTH AS FNHSysMarkId_None"
    '    Else
    '        _Qry &= vbCrLf & " ,  M.FTMarkNameEN AS FNHSysMarkId_None "
    '    End If

    '    _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub AS P With(Nolock) INNER JOIN"
    '    _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M   With(Nolock) ON P.FNHSysSubMarkId = M.FNHSysMarkId"
    '    _Qry &= vbCrLf & " WHERE        (P.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "')"
    '    _Qry &= vbCrLf & " AND        (P.FNHSysMarkId = " & Val(MainMarkKey.ToString) & ")"
    '    _Qry &= vbCrLf & " Order By M.FTMarkCode ASC "

    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '    _dt.Rows.Add()

    '    Me.ogcsubmark.DataSource = _dt

    'End Sub

    'Private Function SaveMainMark() As Boolean

    '    Dim _Qry As String = ""
    '    With CType(Me.ogcmainmark.DataSource, DataTable)
    '        .AcceptChanges()

    '        _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain"
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
    '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '        For Each R As DataRow In .Rows
    '            _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain"
    '            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
    '            _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(R!FNHSysMarkId_Hide.ToString) & "  "

    '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)


    '            _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain"
    '            _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FTNote)"
    '            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '            _Qry &= vbCrLf & "," & Val(R!FNHSysMarkId_Hide.ToString) & " "
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "' "

    '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '        Next

    '    End With

    '    Return True

    'End Function

    'Private Function SavSubMark() As Boolean

    '    Dim _Qry As String = ""
    '    With CType(Me.ogcsubmark.DataSource, DataTable)
    '        .AcceptChanges()

    '        _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub"
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
    '        _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.ogvmainmark.GetFocusedRowCellValue("FNHSysMarkId_Hide").ToString) & "  "

    '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '        For Each R As DataRow In .Rows
    '            _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub"
    '            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
    '            _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(R!FNHSysMainMarkId.ToString) & "  "
    '            _Qry &= vbCrLf & " AND FNHSysSubMarkId=" & Val(R!FNHSysMarkId_Hide.ToString) & "  "

    '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '            _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub"
    '            _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FNHSysSubMarkId,FTNote)"
    '            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '            _Qry &= vbCrLf & "," & Val(R!FNHSysMainMarkId.ToString) & " "
    '            _Qry &= vbCrLf & "," & Val(R!FNHSysMarkId_Hide.ToString) & " "
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "' "

    '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '        Next

    '    End With

    '    Call LoadOrderProdMainMark(Me.otbjobprod.SelectedTabPage.Name.ToString)

    '    Return True

    'End Function

    'Private Sub LoadOrderProdMarkBreakDown(Key As Object)
    '    Dim _dt As DataTable
    '    Dim _Qry As String = ""
    '    Dim _colcount As Integer = 0
    '    Me.ogcbalance.DataSource = Nothing
    '    Me.ogcjobprodbal.DataSource = Nothing

    '    _ListDataMarkBreakDown = New List(Of DataTable)
    '    _ListDataMarkBreakDown.Clear()

    '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '    _ListDataMarkBreakDown.Add(_dt.Copy)

    '    With Me.ogvjobprodbal

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next

    '        If Not (_dt Is Nothing) Then
    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1
    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = False
    '                                .ReadOnly = True
    '                            End With

    '                        End With
    '                        .Columns(Col.ColumnName.ToString).Width = 45
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select
    '            Next
    '        End If

    '        'If _colcount > 4 Then
    '        '    .BestFitColumns()
    '        'End If

    '    End With

    '    Me.ogcjobprodbal.DataSource = _dt.Copy

    '    With Me.ogvbalance

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next

    '        If Not (_dt Is Nothing) Then
    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1
    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = False
    '                                .ReadOnly = True
    '                            End With

    '                        End With
    '                        .Columns(Col.ColumnName.ToString).Width = 45
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select

    '            Next

    '        End If

    '        'If _colcount > 4 Then
    '        '    .BestFitColumns()
    '        'End If

    '    End With

    '    Me.ogcbalance.DataSource = _dt.Copy


    '    Dim M3 As New DataTable

    '    M3 = _dt.Copy
    '    Dim Ridx As Integer = 0

    '    M3.Columns.Remove("Total")
    '    M3.Columns.Add("FNAssort", GetType(String))
    '    Dim R As DataRow = M3.NewRow
    '    R!FNAssort = "Layer\Assort"
    '    M3.Rows.InsertAt(R, 0)
    '    Ridx = 0
    '    For Each Rx As DataRow In M3.Rows

    '        If Ridx > 0 Then
    '            For Each Col As DataColumn In M3.Columns
    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNAssort".ToUpper
    '                    Case Else
    '                        Rx.Item(Col.ColumnName.ToString) = 0
    '                End Select
    '            Next
    '        End If

    '        Ridx = Ridx + 1
    '    Next

    '    _ListDataMarkBreakDown.Add(M3.Copy)
    '    M3.Dispose()
    '    _dt.Dispose()

    'End Sub

#End Region

#Region "Order Prod Mark Cutting"

    'Private Sub LoadTableCuttingBreakdown(TableKey As Object)
    '    Dim _Qry As String = ""

    '    Me.ogcmark.DataSource = Nothing
    '    Me.ogcbalance.DataSource = Nothing
    '    Me.ogcjobprodbal.DataSource = Nothing

    '    Dim _dt As DataTable
    '    Dim _dtctd As DataTable
    '    Dim _dttmp As DataTable

    '    Try
    '        _dt = _ListDataMarkBreakDown(1).Copy
    '    Catch ex As Exception
    '        _dt = Nothing
    '    End Try

    '    Me.ogcbalance.DataSource = _ListDataMarkBreakDown(0).Copy
    '    Me.ogcjobprodbal.DataSource = _ListDataMarkBreakDown(0).Copy
    '    FNFabricFrontSize.Value = 0
    '    FNMarkYRD.Value = 0
    '    FNMarkINC.Value = 0
    '    FNMarkSpare.Value = 2
    '    FNMarkTotal.Value = 0
    '    FTRef.Text = ""

    '    _Qry = " Select TOp 1 A.FTNote, A.FNHSysUnitSectId,ISNULL(B.FTUnitSectCode,'') AS FTUnitSectCode,ISNULL(A.FTStateRepair,'') AS FTStateRepair "
    '    _Qry &= vbCrLf & "  , A.FNFabricFrontSize, A.FNMarkYRD,A.FNMarkINC, A.FNMarkSpare, A.FNMarkTotal,A.FTRef"
    '    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS A WITH(NOLOCK)  "
    '    _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH(NOLOCK) "
    '    _Qry &= vbCrLf & " ON A.FNHSysUnitSectId=B.FNHSysUnitSectId "
    '    _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '    _Qry &= vbCrLf & " AND  A.FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '    _Qry &= vbCrLf & " AND A.FNTableNo=" & Integer.Parse(Val(TableKey.ToString)) & " "

    '    _dttmp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '    FTRemark.Text = ""
    '    FNHSysUnitSectId.Text = ""
    '    FTStateRepair.Checked = False

    '    For Each R As DataRow In _dttmp.Rows

    '        FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
    '        FTRemark.Text = R!FTNote.ToString
    '        FTStateRepair.Checked = (R!FTStateRepair.ToString = "1")
    '        FNFabricFrontSize.Value = Val(R!FNFabricFrontSize.ToString)
    '        FNMarkYRD.Value = Val(R!FNMarkYRD.ToString)
    '        FNMarkINC.Value = Val(R!FNMarkINC.ToString)
    '        FNMarkSpare.Value = 2
    '        FNMarkTotal.Value = Val(R!FNMarkTotal.ToString)
    '        FTRef.Text = R!FTRef.ToString

    '        Exit For
    '    Next

    '    _Qry = "SELECT       FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity"
    '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS D WITH(Nolock)"

    '    If Not (Me.otbjobprod.SelectedTabPage Is Nothing) And Not (Me.otbmarkcutting.SelectedTabPage Is Nothing) Then
    '        _Qry &= vbCrLf & "  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '        _Qry &= vbCrLf & "  AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '    Else
    '        _Qry &= vbCrLf & "  WHERE FTOrderProdNo='' "
    '        _Qry &= vbCrLf & "  AND FNHSysMarkId=0"
    '    End If

    '    _dtctd = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    '------Start Set ยอด ตั้งต้น
    '    With CType(Me.ogcjobprodbal.DataSource, DataTable)
    '        .AcceptChanges()

    '        For Each R As DataRow In _dtctd.Select("FNTableNo<" & Val(TableKey) & "")
    '            For Each Rx As DataRow In .Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

    '                Try
    '                    Rx.Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val(Rx.Item(R!FTSizeBreakDown.ToString))) - Integer.Parse(Val(R!FNQuantity.ToString))
    '                Catch ex As Exception
    '                End Try
    '            Next
    '        Next

    '        .AcceptChanges()
    '    End With
    '    '------ฎืก Set ยอด ตั้งต้น

    '    '------Start Set ยอด Balance
    '    With CType(Me.ogcbalance.DataSource, DataTable)
    '        .AcceptChanges()

    '        For Each R As DataRow In _dtctd.Select("FNTableNo<=" & Val(TableKey) & "")
    '            For Each Rx As DataRow In .Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

    '                Try
    '                    Rx.Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val(Rx.Item(R!FTSizeBreakDown.ToString))) - Integer.Parse(Val(R!FNQuantity.ToString))
    '                Catch ex As Exception
    '                End Try
    '            Next
    '        Next

    '        .AcceptChanges()
    '    End With
    '    '------End Set ยอด Balance

    '    '------Start Set ยอด Table Cutting
    '    For Each R As DataRow In _dtctd.Select("FNTableNo=" & Val(TableKey) & "")
    '        For Each Rx As DataRow In _dt.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

    '            Try
    '                _dt.Rows(0).Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val(R!FNAssort.ToString))
    '                Rx.Item("FNAssort") = Integer.Parse(Val((R!FNLayer.ToString)))
    '                Rx.Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val((R!FNQuantity.ToString)))
    '            Catch ex As Exception
    '            End Try
    '        Next
    '    Next
    '    '------End Set ยอด Table Cutting

    '    _dtctd.Dispose()
    '    Dim _colcount As Integer = 0

    '    With Me.ogvmark

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next

    '        If Not (_dt Is Nothing) Then
    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1
    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString


    '                            If Not (Col.ColumnName.ToString = "Total") Then
    '                                .ColumnEdit = ReposFNAssort
    '                            End If

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = True
    '                                .ReadOnly = False
    '                            End With

    '                        End With
    '                        .Columns(Col.ColumnName.ToString).Width = 45
    '                        '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select

    '            Next

    '        End If

    '        'If _colcount > 4 Then
    '        '    .BestFitColumns()
    '        'End If

    '    End With

    '    Me.ogcmark.DataSource = _dt.Copy
    '    _dt.Dispose()
    'End Sub

    'Private Sub LoadOrderProdMarkCutting(OrderProdKey As Object)
    '    ' Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
    '    otbmarkcutting.TabPages.Clear()
    '    Dim _Qry As String = ""
    '    Dim _dtprod As DataTable

    '    _Qry = " SELECT A.FNHSysMarkId,B.FTMarkCode"

    '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '        _Qry &= vbCrLf & " ,B.FTMarkNameTH AS FTMarkName"
    '    Else
    '        _Qry &= vbCrLf & " ,B.FTMarkNameEN AS FTMarkName"
    '    End If

    '    _Qry &= vbCrLf & "    FROM"
    '    _Qry &= vbCrLf & "  (SELECT     1 AS FNSeq, FTOrderProdNo, FNHSysMarkId"
    '    _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain  WITH(NOLOCK)  "
    '    _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
    '    _Qry &= vbCrLf & "    UNION"
    '    _Qry &= vbCrLf & "  SELECT     2 AS FNSeq, FTOrderProdNo, FNHSysSubMarkId AS FNHSysMarkId"
    '    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub  WITH(NOLOCK)   "
    '    _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
    '    _Qry &= vbCrLf & " ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS B ON A.FNHSysMarkId = B.FNHSysMarkId"
    '    _Qry &= vbCrLf & "  Order BY A.FNSeq,B.FTMarkCode"

    '    _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    For Each R As DataRow In _dtprod.Rows

    '        Dim Otp As New DevExpress.XtraTab.XtraTabPage()
    '        With Otp
    '            .Name = R!FNHSysMarkId.ToString
    '            .Text = R!FTMarkCode.ToString & " ( " & R!FTMarkName.ToString & " ) "
    '        End With

    '        otbmarkcutting.TabPages.Add(Otp)

    '    Next

    '    If _dtprod.Rows.Count > 0 Then
    '        otbmarkcutting.SelectedTabPageIndex = 0
    '    End If

    '    _dtprod.Dispose()
    '    ' _Spls.Close()

    'End Sub

#End Region

#Region "Table Cutting"

    Private Sub LoadOrderProdMarkTableCutting(OrderProdKey As Object, MarkID As Object)
        'Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        'otbtable.TabPages.Clear()
        'Dim _Qry As String = ""
        'Dim _dtprod As DataTable

        '_Qry = " SELECT FNTableNo "
        '_Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTOrderProd_TableCut  WITH(NOLOCK)  "
        '_Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        '_Qry &= vbCrLf & "  AND FNHSysMarkId =" & Val(MarkID.ToString) & " "
        '_Qry &= vbCrLf & "  Order BY FNTableNo"

        '_dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'For Each R As DataRow In _dtprod.Rows

        '    Dim Otp As New DevExpress.XtraTab.XtraTabPage()
        '    With Otp
        '        .Name = R!FNTableNo.ToString
        '        .Text = R!FNTableNo.ToString
        '    End With

        '    otbtable.TabPages.Add(Otp)

        'Next

        'If _dtprod.Rows.Count > 0 Then
        '    otbtable.SelectedTabPageIndex = 0
        'End If

        '_dtprod.Dispose()
        '_Spls.Close()

    End Sub
#End Region

#Region "Function"
    Private Function DeleteOrderProd(OrderProdKey As Object) As Boolean


        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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

    'Private Function DeleteTableCutting(Optional StateAllTable As Boolean = False) As Boolean

    '    CType(ogcmark.DataSource, DataTable).AcceptChanges()

    '    Dim _Qry As String = ""
    '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '    Try

    '        _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '        _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

    '        If Not (StateAllTable) Then
    '            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
    '        End If

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False

    '        End If

    '        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail "
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '        _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

    '        If Not (StateAllTable) Then
    '            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
    '        End If

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '        End If

    '        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat "
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '        _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

    '        If Not (StateAllTable) Then
    '            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
    '        End If

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '        End If

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

    'Private Function SaveTableCutting() As Boolean

    '    CType(ogcmark.DataSource, DataTable).AcceptChanges()

    '    Dim _Qry As String = ""
    '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '    Try

    '        _Qry = " Select TOp 1 FTOrderProdNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '        _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '        _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

    '        If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

    '            _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
    '            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId"
    '            _Qry &= vbCrLf & ", FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef) "
    '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
    '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
    '            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '            _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
    '            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
    '            _Qry &= vbCrLf & " ," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & " "
    '            _Qry &= vbCrLf & " ,'" & FTStateRepair.EditValue.ToString & "' "
    '            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
    '            _Qry &= vbCrLf & "," & FNFabricFrontSize.Value & " "
    '            _Qry &= vbCrLf & "," & FNMarkYRD.Value & ""
    '            _Qry &= vbCrLf & "," & FNMarkINC.Value & ""
    '            _Qry &= vbCrLf & "," & FNMarkSpare.Value & ""
    '            _Qry &= vbCrLf & "," & FNMarkTotal.Value & ""
    '            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRef.Text) & "' "
    '        Else

    '            _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
    '            _Qry &= vbCrLf & " SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
    '            _Qry &= vbCrLf & " ,FTNote='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
    '            _Qry &= vbCrLf & " ,FNHSysUnitSectId=" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & " "
    '            _Qry &= vbCrLf & " ,FTStateRepair='" & FTStateRepair.EditValue.ToString & "'"
    '            _Qry &= vbCrLf & ",FNFabricFrontSize=" & FNFabricFrontSize.Value & " "
    '            _Qry &= vbCrLf & ",FNMarkYRD=" & FNMarkYRD.Value & ""
    '            _Qry &= vbCrLf & ",FNMarkINC=" & FNMarkINC.Value & ""
    '            _Qry &= vbCrLf & ",FNMarkSpare=" & FNMarkSpare.Value & ""
    '            _Qry &= vbCrLf & ",FNMarkTotal=" & FNMarkTotal.Value & ""
    '            _Qry &= vbCrLf & " ,FTRef='" & HI.UL.ULF.rpQuoted(FTRef.Text) & "' "
    '            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

    '        End If

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False

    '        End If

    '        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail "
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '        _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '        _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '        End If

    '        With CType(ogcmark.DataSource, DataTable)
    '            For Each R As DataRow In .Rows

    '                For Each Col As DataColumn In .Columns
    '                    Select Case Col.ColumnName.ToString.ToUpper
    '                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper
    '                        Case Else
    '                            If IsNumeric(R.Item(Col.ColumnName.ToString)) Then

    '                                If Integer.Parse(R.Item(Col.ColumnName.ToString)) > 0 AndAlso R!FTColorway.ToString <> "" Then

    '                                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTOrderProd_TableCut_Detail "
    '                                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity) "
    '                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
    '                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
    '                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                                    _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '                                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
    '                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' "
    '                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
    '                                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNAssort.ToString)) & " "
    '                                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(.Rows(0).Item(Col.ColumnName.ToString))) & " "
    '                                    _Qry &= vbCrLf & " ," & Integer.Parse(R.Item(Col.ColumnName.ToString)) & " "

    '                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                                        HI.Conn.SQLConn.Tran.Rollback()
    '                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                                        Return False
    '                                    End If

    '                                End If

    '                            End If
    '                    End Select

    '                Next

    '            Next
    '        End With

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

    'Private Function ValidateTableCutting(Optional StateSave As Boolean = True) As Boolean
    '    If Not (Me.otbtable.SelectedTabPage Is Nothing) Then

    '        If (StateSave) Then
    '            If Me.FNHSysUnitSectId.Text <> "" And Me.FNHSysUnitSectId.Properties.Tag.ToString <> "" Then
    '                Return True
    '            Else
    '                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitSectId_lbl.Text)
    '                FNHSysUnitSectId.Focus()
    '                FNHSysUnitSectId.SelectAll()
    '                Return False
    '            End If
    '        Else
    '            Return True
    '        End If
    '    Else
    '        HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Table Cutting", 1404210001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
    '        Return False
    '    End If
    'End Function

    'Private Function ValidateCreateTableCut() As Boolean
    '    Dim _Qry As String
    '    _Qry = "Select Top 1 FTOrderProdNo "
    '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut WITH (NOLOCK) "
    '    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "

    '    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    'Private Function DeleteSubOrder(Optional StateAll As Boolean = False) As Boolean

    '    ' CType(ogcmark.DataSource, DataTable).AcceptChanges()

    '    Dim _Qry As String = ""
    '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '    Try

    '        _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail "
    '        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "

    '        If Not (StateAll) Then
    '            _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.otbsuborder.SelectedTabPage.Name.ToString) & "' "
    '        End If

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False

    '        End If

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

    'Private Function CheckTableCreateLayCut(Optional StateAll As Boolean = False) As Boolean
    '    Dim _Qry As String = ""

    '    If (Me.otbtable.SelectedTabPage Is Nothing) Then
    '        Return False
    '    End If

    '    _Qry = " Select TOP 1  FTLayCutNo"
    '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " WHERE (FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "')"
    '    _Qry &= vbCrLf & " AND (FNHSysMarkId =" & Integer.Parse(Val(otbmarkcutting.SelectedTabPage.Name.ToString)) & ")"

    '    If Not (StateAll) Then
    '        _Qry &= vbCrLf & "  AND (FNTableNo =" & Integer.Parse(Val(otbtable.SelectedTabPage.Name.ToString)) & ")"
    '    End If

    '    Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")
    'End Function
#End Region

    Private Sub wCreateJobProduction_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try
            _ListDataMarkBreakDown.Clear()
            _ListDataMarkBreakDown = Nothing
            _ListPurchaseOrder.Clear()
            _ListPurchaseOrder = Nothing

        Catch ex As Exception
        End Try


    End Sub

    Private Sub wCreateJobProduction_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        AddHandler RepositoryFTSuplCode.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick

        'Call InitGrid()

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


#Region "Init New Row Data"

#End Region


    Private Sub FNMarkINC_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            If e.NewValue >= 36 Then
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadData(ByVal _OrderNo As String)
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable

            _Qry = "SELECT  '0' AS FTSelect ,     BS.FTBarcodeSendSuplNo, BS.FNHSysPartId, BS.FNSendSuplType, BS.FNHSysSuplId, BS.FTBarcodeBundleNo, BS.FTOrderProdNo, BS.FTSendSuplRef, BS.FNHSysCmpId, MS.FTSuplCode,OP.FTSMPOrderNo as FTOrderNo "


            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , MS.FTSuplNameTH AS FTSuplName   "
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName"
                _Qry &= vbCrLf & " 	,'' AS FTOperationName"
            Else
                _Qry &= vbCrLf & " , MS.FTSuplNameEN AS FTSuplName  "
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,'' AS FTOperationName"
            End If

            _Qry &= vbCrLf & " 	, BD.FTColorway"
            _Qry &= vbCrLf & " 	, BD.FTSizeBreakDown , BD.FNQuantity "

            '_Qry &= vbCrLf & " ,ISNULL(("

            '_Qry &= vbCrLf & "   SELECT        TOP 1  "


            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            'Else
            '    _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            'End If
            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            '_Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BS.FTBarcodeBundleNo)"
            '_Qry &= vbCrLf & " ),'') AS FTMarkName"

            '_Qry &= vbCrLf & " ,ISNULL(("
            '_Qry &= vbCrLf & "   SELECT        TOP 1  B.FNHSysMarkId "
            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo  "
            '_Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BS.FTBarcodeBundleNo)"
            '_Qry &= vbCrLf & " ),0) AS FNHSysMarkId"



            _Qry &= vbCrLf & "   ,   '' AS FTMarkName  ,0  AS FNHSysMarkId "


            _Qry &= vbCrLf & ",BD.FNBunbleSeq"

            '_Qry &= vbCrLf & " 	,MPP.FNHSysOperationId"
            '_Qry &= vbCrLf & " ,OPP.FNSeq "
            '_Qry &= vbCrLf & " ,OPP.FNHSysOperationIdTo "

            _Qry &= vbCrLf & " , 0 as FNHSysOperationId "
            _Qry &= vbCrLf & " , 0 as FNSeq "
            _Qry &= vbCrLf & " , 0 as FNHSysOperationIdTo "


            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS BS WITH(NOLOCK)  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS MS  WITH(NOLOCK)  ON BS.FNHSysSuplId = MS.FNHSysSuplId"
            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS OP WITH(NOLOCK)  ON BS.FTOrderProdNo = OP.FTSMPOrderNo "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl_Barcode AS RS WITH (NOLOCK) ON BS.FTBarcodeSendSuplNo = RS.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS BD ON BS.FTBarcodeBundleNo = BD.FTBarcodeBundleNo "
            _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON BS.FNHSysPartId = MP.FNHSysPartId"
            ''_Qry &= vbCrLf & " 	INNER JOIN         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON BS.FTSendSuplRef = SD.FTSendSuplRef AND OP.FTOrderProdNo = SD.FTOrderProdNo "

            '' _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON OP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "

            ''_Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON   OPP.FNHSysOperationId  = MPP.FNHSysOperationId"


            _Qry &= vbCrLf & " WHERE     (BS.FTBarcodeSendSuplNo NOT IN"
            _Qry &= vbCrLf & "          (SELECT     FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & "              FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl_Barcode WITH(NOLOCK) ))"

            _Qry &= vbCrLf & " AND OP.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & " Order by  BS.FTBarcodeSendSuplNo ASC "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
            Me.ogcNotSendSupl.DataSource = _oDt



            'Send Supl
            _Qry = "SELECT   '0' AS FTSelect ,   BS.FTBarcodeSendSuplNo, BS.FNHSysPartId, BS.FNSendSuplType, BS.FNHSysSuplId, BS.FTBarcodeBundleNo, BS.FTOrderProdNo, BS.FTSendSuplRef, BS.FNHSysCmpId, MS.FTSuplCode, "
            _Qry &= vbCrLf & "   OP.FTSMPOrderNo as FTOrderNo, SS.FTSendSuplNo,   RS.FTRcvSuplNo , SS.FTSendSuplNo"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , MS.FTSuplNameTH AS FTSuplName   "
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName"
                ''_Qry &= vbCrLf & " 	,MPP.FTOperationNameTH AS FTOperationName"
                _Qry &= vbCrLf & " 	,'' AS FTOperationName"
            Else
                _Qry &= vbCrLf & " , MS.FTSuplNameEN AS FTSuplName  "
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                '' _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
                _Qry &= vbCrLf & " 	, '' AS FTOperationName"
            End If
            _Qry &= vbCrLf & " 	, BD.FTColorway"
            _Qry &= vbCrLf & " 	, BD.FTSizeBreakDown , BD.FNQuantity "

            '_Qry &= vbCrLf & " ,ISNULL(("

            '_Qry &= vbCrLf & "   SELECT        TOP 1  "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            'Else
            '    _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            'End If

            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            '_Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BS.FTBarcodeBundleNo)"

            '_Qry &= vbCrLf & " ),'') AS FTMarkName"

            '_Qry &= vbCrLf & " ,ISNULL(("

            '_Qry &= vbCrLf & "   SELECT        TOP 1  B.FNHSysMarkId "
            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo  "
            '_Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BS.FTBarcodeBundleNo)"
            '_Qry &= vbCrLf & " ),0) AS FNHSysMarkId"


            _Qry &= vbCrLf & "   ,   '' AS FTMarkName  ,0  AS FNHSysMarkId "

            _Qry &= vbCrLf & ",BD.FNBunbleSeq"
            '_Qry &= vbCrLf & " 	,MPP.FNHSysOperationId"
            '_Qry &= vbCrLf & " ,OPP.FNSeq "
            '_Qry &= vbCrLf & " ,OPP.FNHSysOperationIdTo "

            _Qry &= vbCrLf & " 	, 0 as FNHSysOperationId"
            _Qry &= vbCrLf & " , 0 as FNSeq "
            _Qry &= vbCrLf & " ,0 as FNHSysOperationIdTo "


            _Qry &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS BS WITH (NOLOCK) "
            _Qry &= vbCrLf & "    LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS MS WITH (NOLOCK) ON BS.FNHSysSuplId = MS.FNHSysSuplId "
            _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS OP WITH (NOLOCK) ON BS.FTOrderProdNo = OP.FTSMPOrderNo "

            _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl_Barcode AS SS WITH (NOLOCK) ON BS.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl_Barcode AS RS WITH (NOLOCK) ON BS.FTBarcodeSendSuplNo = RS.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS BD ON BS.FTBarcodeBundleNo = BD.FTBarcodeBundleNo "
            _Qry &= vbCrLf & " 		   "
            _Qry &= vbCrLf & " 	    LEFT OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON BS.FNHSysPartId = MP.FNHSysPartId"
            '' _Qry &= vbCrLf & " 	INNER JOIN         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON BS.FTSendSuplRef = SD.FTSendSuplRef AND OP.FTOrderProdNo = SD.FTOrderProdNo "

            ''_Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON OP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "
            ''_Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON   OPP.FNHSysOperationId  = MPP.FNHSysOperationId"


            _Qry &= vbCrLf & " WHERE OP.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & " AND isnull(RS.FTRcvSuplNo,'') = '' "
            _Qry &= vbCrLf & "Order by BS.FTBarcodeSendSuplNo ASC "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
            Me.ogcSendSupl.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FTSMPOrderNo.Text <> "" Then
                Call LoadData(HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text))
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
                Me.FTSMPOrderNo.Focus()
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmChangeSupl_Click(sender As Object, e As EventArgs) Handles ocmChangeSupl.Click
        Try
            If (Me.VerrifyData) Then
                Dim _Spls As New HI.TL.SplashScreen("Prepre Data Process Change Supplier... Please Wait ")
                If ChangeSupl(Me.FTSMPOrderNo.Text, Me.FNHSysSuplId.Properties.Tag, _Spls) Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.LoadData(Me.FTSMPOrderNo.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try


            Dim _oDt As DataTable = CType(Me.ogcNotSendSupl.DataSource, DataTable).Copy
            Dim _oDt2 As DataTable = CType(Me.ogcSendSupl.DataSource, DataTable).Copy
            If otbdetail.SelectedTabPage Is otpNotSendSupl Then
                If _oDt.Select("FTSelect = '1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, otpNotSendSupl.Text)
                    Return False
                End If
            End If
            If otbdetail.SelectedTabPage Is otpSendSupl Then
                If _oDt2.Select("FTSelect= '1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, otpSendSupl.Text)
                    Return False
                End If
            End If

            If Me.FNHSysSuplId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysSuplId_lbl.Text)
                Me.FNHSysSuplId.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function ChangeSupl(ByVal _OrderNo As String, ByVal _FNHSysSuplId As Integer, _Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _Cmd As String = ""

            CType(Me.ogcNotSendSupl.DataSource, DataTable).AcceptChanges()
            CType(Me.ogcSendSupl.DataSource, DataTable).AcceptChanges()

            Dim _oDt As DataTable = CType(Me.ogcNotSendSupl.DataSource, DataTable).Copy
            Dim _oDt2 As DataTable = CType(Me.ogcSendSupl.DataSource, DataTable).Copy
            Dim _oDtChk As DataTable
            '    'HI.MG.ShowMsg.mInfo("Plase Select Barcode. !!!", 14121800009, Me.Text)
            Dim i As Integer = 0
            Dim _State As Boolean = True
            Dim _Branch As Boolean = HI.Conn.SQLConn.GetField("Select Top 1 Isnull(FNHSysCmpId,0) AS FNHSysCmpId From TCNMSupplier WITH(NOLOCK) WHERE FNHSysSuplId=" & Integer.Parse(0 & _FNHSysSuplId), Conn.DB.DataBaseName.DB_MASTER, 0) <> 0
            Dim _ToBranch As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("Select Top 1 Isnull(FNHSysCmpId,0) AS FNHSysCmpId From TCNMSupplier WITH(NOLOCK) WHERE FNHSysSuplId=" & Integer.Parse(0 & _FNHSysSuplId), Conn.DB.DataBaseName.DB_MASTER, "0")))

            If otbdetail.SelectedTabPage Is otpNotSendSupl Then
                'If _oDt.Select("FTSelect = '1'").Length <= 0 Then
                '    Return False
                'End If
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                For Each R As DataRow In _oDt.Select("FTSelect = '1'")
                    i += +1
                    _Spls.UpdateProgress(i)

                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl"
                    _Cmd &= vbCrLf & "SET FNHSysSuplId=" & CInt(_FNHSysSuplId)

                    _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS BS WITH(NOLOCK)  LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS MS  WITH(NOLOCK)  ON BS.FNHSysSuplId = MS.FNHSysSuplId"
                    _Cmd &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS OP WITH(NOLOCK)  ON BS.FTOrderProdNo = OP.FTSMPOrderNo "
                    _Cmd &= vbCrLf & " WHERE     (BS.FTBarcodeSendSuplNo NOT IN"
                    _Cmd &= vbCrLf & "          (SELECT     FTBarcodeSendSuplNo"
                    _Cmd &= vbCrLf & "              FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl_Barcode WITH(NOLOCK) ))"
                    _Cmd &= vbCrLf & " AND OP.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"

                    _Cmd &= vbCrLf & " AND BS.FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                    _Cmd &= vbCrLf & " AND BS.FNHSysPartId =" & CInt("0" & R!FNHSysPartId.ToString)
                    _Cmd &= vbCrLf & " AND BS.FNSendSuplType =" & CInt("0" & R!FNSendSuplType.ToString)
                    _Cmd &= vbCrLf & " AND BS.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                    _Cmd &= vbCrLf & " AND BS.FTOrderProdNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If

                Next

            End If

            If otbdetail.SelectedTabPage Is otpSendSupl Then
                'If _oDt2.Select("FTSelect = '1'").Length <= 0 Then
                '    Return False
                'End If
                Dim _Key As String = GenDocument.ToString

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                _oDt2.Columns.Add("FTStateUpdate", GetType(String))
                _State = False
                For Each R As DataRow In _oDt2.Select("FTSelect = '1'")
                    R!FTStateUpdate = "0"
                    _Cmd = "Select Top 1 *"
                    _Cmd &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS BS WITH (NOLOCK) "
                    _Cmd &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS MS WITH (NOLOCK) ON BS.FNHSysSuplId = MS.FNHSysSuplId"

                    _Cmd &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS OP WITH (NOLOCK) ON BS.FTOrderProdNo = OP.FTSMPOrderNo "

                    _Cmd &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl_Barcode AS SS WITH (NOLOCK) ON BS.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo "
                    _Cmd &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl_Barcode AS RS WITH (NOLOCK) ON BS.FTBarcodeSendSuplNo = RS.FTBarcodeSendSuplNo"
                    _Cmd &= vbCrLf & " WHERE OP.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                    _Cmd &= vbCrLf & " AND BS.FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                    _Cmd &= vbCrLf & " AND BS.FNHSysPartId =" & CInt("0" & R!FNHSysPartId.ToString)
                    _Cmd &= vbCrLf & " AND BS.FNSendSuplType =" & CInt("0" & R!FNSendSuplType.ToString)
                    _Cmd &= vbCrLf & " AND BS.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                    _Cmd &= vbCrLf & " AND BS.FTOrderProdNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                    _Cmd &= vbCrLf & " AND isnull(RS.FTRcvSuplNo,'') = '' "

                    If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows.Count > 0 Then
                        R!FTStateUpdate = "1"
                        _State = True
                    End If

                Next

                If (_State) Then
                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl"
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTSendSuplNo, FDSendSuplDate, FTSendSuplBy, FNHSysSuplId, FNSendSuplState,  FNHSysCmpId)"
                    _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & _Key.ToString & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & "," & _FNHSysSuplId
                    _Cmd &= vbCrLf & "," & CInt("0" & Me.FNSendSuplState.SelectedIndex)
                    _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysCmpId.Properties.Tag)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If

                    If (_Branch) Then
                        _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplToBranch"
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTSendSuplNo, FDSendSuplDate, FTSendSuplBy, FNHSysSuplId, FNSendSuplState,  FNHSysCmpId,FNHSysCmpIdTo)"
                        _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & _Key.ToString & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & "," & _FNHSysSuplId
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNSendSuplState.SelectedIndex)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysCmpId.Properties.Tag)
                        _Cmd &= vbCrLf & "," & _ToBranch

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If
                    End If


                Else
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If

                For Each R As DataRow In _oDt2.Select("FTSelect = '1' and FTStateUpdate = '1'")
                    i += +1
                    _Spls.UpdateProgress(i)

                    _Cmd = "Update BS "
                    _Cmd &= vbCrLf & "SET BS.FNHSysSuplId=" & CInt(_FNHSysSuplId)

                    _Cmd &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS BS WITH (NOLOCK) "
                    _Cmd &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS MS WITH (NOLOCK) ON BS.FNHSysSuplId = MS.FNHSysSuplId "
                    _Cmd &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS OP WITH (NOLOCK) ON BS.FTOrderProdNo = OP.FTSMPOrderNo"
                    _Cmd &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl_Barcode AS SS WITH (NOLOCK) ON BS.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo "
                    _Cmd &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl_Barcode AS RS WITH (NOLOCK) ON BS.FTBarcodeSendSuplNo = RS.FTBarcodeSendSuplNo"
                    _Cmd &= vbCrLf & " WHERE OP.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                    _Cmd &= vbCrLf & " AND BS.FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                    '_Cmd &= vbCrLf & " AND BS.FNHSysSuplId =" & Integer.Parse(Val(R!FNHSysSuplId.ToString))
                    _Cmd &= vbCrLf & " AND BS.FNHSysPartId =" & CInt("0" & R!FNHSysPartId.ToString)
                    _Cmd &= vbCrLf & " AND BS.FNSendSuplType =" & CInt("0" & R!FNSendSuplType.ToString)
                    _Cmd &= vbCrLf & " AND BS.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                    _Cmd &= vbCrLf & " AND BS.FTOrderProdNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                    _Cmd &= vbCrLf & " AND isnull(RS.FTRcvSuplNo,'') = '' "

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If

                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl_Barcode"
                    _Cmd &= vbCrLf & "Set FTSendSuplNo ='" & _Key.ToString & "'"
                    _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If

                    If (_Branch) Then
                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplToBranch_Barcode"
                        _Cmd &= vbCrLf & "Set FTSendSuplNo ='" & _Key.ToString & "'"
                        _Cmd &= vbCrLf & ",FNHSysSuplId=" & _FNHSysSuplId
                        _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplToBranch_Barcode"
                            _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTSendSuplNo,FTBarcodeSendSuplNo, FNBunbleSeq, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo, "
                            _Cmd &= "  FTOrderNo, FTOrderProdNo, FTSendSuplRef, FNHSysCmpId, FTColorway, FTSizeBreakDown, FNQuantity, FNHSysOperationId, FNSeq, FNHSysOperationIdTo, FNHSysMarkId)"
                            _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & _Key.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNBunbleSeq.ToString) '****
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNHSysPartId.ToString)
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNSendSuplType.ToString)
                            _Cmd &= vbCrLf & "," & _FNHSysSuplId
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSendSuplRef.ToString) & "'"
                            _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysCmpId.Properties.Tag)
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNQuantity.ToString)
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNHSysOperationId.ToString) '****
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString) '****
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNHSysOperationIdTo.ToString) '****
                            _Cmd &= vbCrLf & "," & CInt("0" & R!FNHSysMarkId.ToString) '****

                            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                _Spls.Close()
                                Return False
                            End If
                        End If
                    End If



                    'FTSendSuplNo
                    _Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl_Barcode WITH(NOLOCK)"
                    _Cmd &= vbCrLf & "WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"
                    _oDtChk = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd)
                    If _oDtChk.Rows.Count <= 0 Then
                        _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSupl "
                        _Cmd &= vbCrLf & "WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                        'If (_Branch) Then
                        _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplToBranch "
                        _Cmd &= vbCrLf & "WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        End If
                        _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplToBranch_Barcode "
                        _Cmd &= vbCrLf & "WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        End If
                        'End If

                    End If

                Next
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False

        End Try
    End Function

    Private Function GenDocument() As String
        Try
            Dim _Key As String = ""
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

            _Key = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTSendSupl", "", False, _CmpH).ToString

            Return _Key
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub ochkselectall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If
            With ogcNotSendSupl
                If Not (.DataSource Is Nothing) And ogvNotSendSupl.RowCount > 0 Then

                    With ogvNotSendSupl
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ochkselectall2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall2.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ochkselectall2.Checked Then
                _State = "1"
            End If
            With ogcSendSupl
                If Not (.DataSource Is Nothing) And ogvSendSupl.RowCount > 0 Then

                    With ogvSendSupl
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTSMPOrderNo.Focus()
    End Sub

    Private Sub FTSMPOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged
        Try
            ''  FTSMPOrderNo.Properties.Tag = FTSMPOrderNo.Text

            Call LoadInfo(FTSMPOrderNo.Text)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadInfo(_FTSMPOrderNo As String)
        Try
            Dim _dt As DataTable
            Dim _Dt2 As DataTable

            Dim _Qry As String = ""
            _Qry = " SELECT    SN.FTSeasonCode ,   M.FTSMPOrderNo     "
            _Qry &= vbCrLf & " ,Case WHEN  ISDate(M.FDSMPOrderDate) = 1 Then  Convert(nvarchar(10),Convert(datetime,M.FDSMPOrderDate),103) Else '' ENd As FDOrderDate   "
            _Qry &= vbCrLf & " ,Case WHEN  ISDate(B.FTGACDate) = 1 Then  Convert(nvarchar(10),Convert(datetime,B.FTGACDate),103) Else '' ENd As FDShipDate  "
            _Qry &= vbCrLf & "  ,B_SUM.FNGrandQty, S.FTStyleCode,  S.FTStyleNameEN AS FTStyleName,C.FTCustCode,C.FTCustNameEN As FTCustName    "
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS M WITH(NOLOCK) "
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON M.FNHSysStyleId = S.FNHSysStyleId   "
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK) ON M.FNHSysCustId = C.FNHSysCustId  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SN WITH(NOLOCK) ON M.FNHSysSeasonId = SN.FNHSysSeasonId     "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 FTGACDate FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown WHERE M.FTSMPOrderNo = FTSMPOrderNo ORDER BY FTGACDate ASC) B "
            _Qry &= vbCrLf & "  OUTER APPLY (SELECT SUM(ISNULL(FNQuantity,0)) as FNGrandQty FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown WHERE M.FTSMPOrderNo = FTSMPOrderNo ) B_SUM "
            _Qry &= vbCrLf & "  WHERE  M.FTSMPOrderNo  ='" & _FTSMPOrderNo & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


            For Each R As DataRow In _dt.Rows

                FTSMPOrderNo.Text = R!FTSMPOrderNo.ToString
                FDOrderDate.Text = R!FDOrderDate.ToString
                FNGrandQty.Text = R!FNGrandQty.ToString
                FNHSysStyleId.Text = R!FTStyleCode.ToString
                '' FNHSysStyleId_None.Text =""
                FNHSysCustId.Text = R!FTCustCode.ToString
                ''FNHSysCustId_None.Text = ""
                FDShipDate.Text = R!FDShipDate.ToString
                ''FNHSysSeasonId.Text = R!FTSeasonCode.ToString
            Next





        Catch ex As Exception

        End Try


    End Sub
End Class
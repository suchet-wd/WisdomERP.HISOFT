Imports System.Drawing

Public Class wCreateJobProduction

    Private _GenJobProd As wGenerateJobProd
    Private _ListDataMarkBreakDown As List(Of DataTable)
    Private _ListPurchaseOrder As New List(Of DataTable)
    Private _MapPurchaseOrderRawmat As wColorWayListPORawMat
    Private _CopyTable As wCopyTableCut
    Private _FindTable As wFindTableCut
    Private _StateSubNew As Boolean = False
    Private _TFNMarkSpare As Double = 2.0

    Sub New()
        _StateSubNew = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _GenJobProd = New wGenerateJobProd
        HI.TL.HandlerControl.AddHandlerObj(_GenJobProd)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenJobProd.Name.ToString.Trim, _GenJobProd)
        Catch ex As Exception
        Finally
        End Try

        _MapPurchaseOrderRawmat = New wColorWayListPORawMat
        HI.TL.HandlerControl.AddHandlerObj(_MapPurchaseOrderRawmat)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _MapPurchaseOrderRawmat.Name.ToString.Trim, _MapPurchaseOrderRawmat)
        Catch ex As Exception
        Finally
        End Try

        _CopyTable = New wCopyTableCut
        HI.TL.HandlerControl.AddHandlerObj(_CopyTable)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _CopyTable.Name.ToString.Trim, _CopyTable)
        Catch ex As Exception
        Finally
        End Try

        _FindTable = New wFindTableCut
        HI.TL.HandlerControl.AddHandlerObj(_FindTable)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _FindTable.Name.ToString.Trim, _FindTable)
        Catch ex As Exception
        Finally
        End Try

        With ReposFNAssort
            AddHandler .Spin, AddressOf HI.TL.HandlerControl.Caledit_Spin
        End With

        Dim _Qry As String = ""
        _Qry = "SELECT TOP 1 FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='TableCutMarkSpare'"

        _TFNMarkSpare = CDbl(Format(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "2.00")), "0.00"))

        Call TabChenge()
        _StateSubNew = False
    End Sub


#Region "Property"

#End Region

#Region "Procedure"

    Private Sub InitDataPurchaseOrder(OrderNoKey As String)
        '_ListPurchaseOrder.Clear()

        ' Dim dt As DataTable
        'Dim _Qry As String = ""

        '_Qry = " SELECT  FTPurchaseNo  FROM "
        '_Qry &= vbCrLf & "  ( "
        '_Qry &= vbCrLf & "   Select A.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
        '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS C WITH(NOLOCK) ON B.FTRawMatCode = C.FTMainMatCode"
        '_Qry &= vbCrLf & "   WHERE      (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey.ToString) & "')  "
        '_Qry &= vbCrLf & "   AND     (C.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "   GROUP BY A.FTPurchaseNo "
        '_Qry &= vbCrLf & "   UNION "
        '_Qry &= vbCrLf & "   Select C.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS A WITH(NOLOCK) INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FTReserveNo = B.FTDocumentNo INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo  INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        '_Qry &= vbCrLf & "   WHERE      (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey.ToString) & "')  "
        '_Qry &= vbCrLf & "      AND     (MM.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "   GROUP BY C.FTPurchaseNo "
        '_Qry &= vbCrLf & "   UNION "
        '_Qry &= vbCrLf & "   Select C.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock AS A WITH(NOLOCK) INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FTAdjustStockNo = B.FTDocumentNo INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo  INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        '_Qry &= vbCrLf & "   WHERE      (B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey.ToString) & "')  "
        '_Qry &= vbCrLf & "      AND     (MM.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "   GROUP BY C.FTPurchaseNo "
        '_Qry &= vbCrLf & "   UNION "
        '_Qry &= vbCrLf & "   Select C.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FTTransferOrderNo = B.FTDocumentNo INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo  INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        '_Qry &= vbCrLf & "   WHERE      (B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey.ToString) & "')  "
        '_Qry &= vbCrLf & "      AND     (MM.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "   GROUP BY C.FTPurchaseNo "
        '_Qry &= vbCrLf & "  ) AS A Order By FTPurchaseNo "

        'dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        'If dt.Rows.Count <= 0 Then
        '    dt.Rows.Add("")
        'End If

        '_ListPurchaseOrder.Add(dt.Copy)
        'dt.Dispose()
    End Sub

    Private Sub TabChenge()

        ocmgeneratejobprod.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmdeletejobprod.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmaddsuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmdeletesuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmsavemainmark.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmsavesubmark.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmdeleteallsuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)

        ocmaddtable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmdeletetable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmsavetable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmdeletealltable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmmappomatcolor.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)

        ocmpreviewbytable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmpreviewalltable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmpreviewallprod.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmcopy.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmfindtable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub InitGridBreakdown()

        With ogvjobprod
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

        With ogvsub
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

    Public Sub SetInfo(ByVal Key As Object)
        '...call by another form name zzz...
        FTOrderNo.Text = Key.ToString
    End Sub

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)
        If (_StateSubNew) Then Exit Sub
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        Call ClearGrid()
        otbjobprod.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTOrderProdNo  "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        _Qry &= vbCrLf & "  Order By LEN(FTOrderProdNo),FTOrderProdNo  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FTOrderProdNo.ToString
                .Text = R!FTOrderProdNo.ToString
            End With

            otbjobprod.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbjobprod.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()

        Call InitDataPurchaseOrder(Key)

        _Spls.Close()
    End Sub

    Private Sub LoadOrderProdBreakDown(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.ogvjobprod

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

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcjobprod.DataSource = _dt.Copy
        'If _colcount > 4 Then
        '    ogvjobprod.BestFitColumns()
        'End If

        _colcount = 0
        With Me.ogvjobprodbal

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

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcjobprodbal.DataSource = _dt.Copy

        'If _colcount > 4 Then
        '    ogvjobprodbal.BestFitColumns()
        'End If

    End Sub
    Private Function GetFTNikePOLineItem(_SubOrderNo As String, _Colorway As String) As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 isnull(FTNikePOLineItem,'') AS FTNikePOLineItem  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub LoadOrderProdSubBreakDown(OrderProdNo As Object, SubOrderNo As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdSubBreakDown '" & HI.UL.ULF.rpQuoted(OrderProdNo.ToString) & "','" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        With _dt
            .Columns.Add("FTNikePOLineItem", GetType(String))
        End With
        _dt.BeginInit()
        For Each R As DataRow In _dt.Rows
            R!FTNikePOLineItem = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)
        Next
        _dt.EndInit()
        With Me.ogvsub

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcsub.DataSource = _dt
    End Sub

    Private Sub LoadOrderProdDetail(Key As Object)
        Dim _Qry As String = ""
        Dim _dtprod As DataTable
        otbsuborder.TabPages.Clear()

        _Qry = "SELECT  FTSubOrderNo   "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        _Qry &= vbCrLf & "  Group By  FTSubOrderNo  "
        _Qry &= vbCrLf & "  Order By  FTSubOrderNo  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FTSubOrderNo.ToString
                .Text = R!FTSubOrderNo.ToString
            End With

            otbsuborder.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbsuborder.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()
    End Sub

    Private Sub InitGrid()

        With ogvjobprod
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvsub
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvmainmark
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvsubmark
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvmark
            .OptionsView.ShowAutoFilterRow = False
            '.OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect 'DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvjobprodbal
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvbalance
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

    End Sub

    Private Sub ClearGrid(Optional Prod As Boolean = False)

        With Me.ogvjobprod
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With

        With Me.ogvsub
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With

        With Me.ogvjobprodbal
            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next
        End With

        ogcmainmark.DataSource = Nothing
        ogcsubmark.DataSource = Nothing
        ogcjobprod.DataSource = Nothing
        ogcsub.DataSource = Nothing

        If Not (Prod) Then
            Me.otbjobprod.TabPages.Clear()
        End If

        Me.otbsuborder.TabPages.Clear()
        Me.otbtable.TabPages.Clear()

    End Sub

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

    Private Sub LoadOrderProdSubMark(OrderProdKey As Object, MainMarkKey As Object)
        Me.ogcsubmark.DataSource = Nothing
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = " SELECT        P.FTOrderProdNo, P.FNHSysSubMarkId AS FNHSysMarkId_Hide, M.FTMarkCode AS FNHSysMarkId"
        _Qry &= vbCrLf & " ,P.FNHSysMarkId AS FNHSysMainMarkId,P.FTNote"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , M.FTMarkNameTH AS FNHSysMarkId_None"
        Else
            _Qry &= vbCrLf & " ,  M.FTMarkNameEN AS FNHSysMarkId_None "
        End If

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub AS P With(Nolock) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M   With(Nolock) ON P.FNHSysSubMarkId = M.FNHSysMarkId"
        _Qry &= vbCrLf & " WHERE        (P.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "')"
        _Qry &= vbCrLf & " AND        (P.FNHSysMarkId = " & Val(MainMarkKey.ToString) & ")"
        _Qry &= vbCrLf & " Order By M.FTMarkCode ASC "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _dt.Rows.Add()

        Me.ogcsubmark.DataSource = _dt

    End Sub

    Private Function SaveMainMark() As Boolean

        Dim _Qry As String = ""
        With CType(Me.ogcmainmark.DataSource, DataTable)
            .AcceptChanges()

            _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In .Rows
                If Val((R!FNHSysMarkId_Hide.ToString)) > 0 Then
                    _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(R!FNHSysMarkId_Hide.ToString) & "  "

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain"
                    _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FTNote,FNHSysCmpId)"
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & "," & Val(R!FNHSysMarkId_Hide.ToString) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "' "
                    _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                End If

            Next

        End With

        Return True

    End Function

    Private Function SavSubMark() As Boolean

        Dim _Qry As String = ""
        With CType(Me.ogcsubmark.DataSource, DataTable)
            .AcceptChanges()

            _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
            _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.ogvmainmark.GetFocusedRowCellValue("FNHSysMarkId_Hide").ToString) & "  "

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In .Rows

                If Val(R!FNHSysMarkId_Hide.ToString) > 0 Then
                    _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(R!FNHSysMainMarkId.ToString) & "  "
                    _Qry &= vbCrLf & " AND FNHSysSubMarkId=" & Val(R!FNHSysMarkId_Hide.ToString) & "  "

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub"
                    _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FNHSysSubMarkId,FTNote,FNHSysCmpId)"
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & "," & Val(R!FNHSysMainMarkId.ToString) & " "
                    _Qry &= vbCrLf & "," & Val(R!FNHSysMarkId_Hide.ToString) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "' "
                    _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                End If

            Next

        End With

        Call LoadOrderProdMainMark(Me.otbjobprod.SelectedTabPage.Name.ToString)

        Return True

    End Function

    Private Sub LoadOrderProdMarkBreakDown(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Me.ogcbalance.DataSource = Nothing
        Me.ogcjobprodbal.DataSource = Nothing

        _ListDataMarkBreakDown = New List(Of DataTable)
        _ListDataMarkBreakDown.Clear()

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _ListDataMarkBreakDown.Add(_dt.Copy)

        With Me.ogvjobprodbal

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

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With
                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select
                Next
            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcjobprodbal.DataSource = _dt.Copy

        With Me.ogvbalance

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

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With
                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcbalance.DataSource = _dt.Copy

        Dim M3 As New DataTable

        M3 = _dt.Copy
        Dim Ridx As Integer = 0

        M3.Columns.Remove("Total")
        M3.Columns.Add("FNAssort", GetType(String))
        M3.Columns.Add("FNTableNoRef", GetType(Integer))
        Dim R As DataRow = M3.NewRow
        R!FNAssort = "Layer\Assort"
        M3.Rows.InsertAt(R, 0)
        Ridx = 0
        For Each Rx As DataRow In M3.Rows

            If Ridx > 0 Then
                For Each Col As DataColumn In M3.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNAssort".ToUpper, "FNTableNoRef".ToUpper
                        Case Else
                            Rx.Item(Col.ColumnName.ToString) = 0
                    End Select
                Next
            End If

            Ridx = Ridx + 1
        Next

        _ListDataMarkBreakDown.Add(M3.Copy)
        M3.Dispose()
        _dt.Dispose()

    End Sub

#End Region

#Region "Order Prod Mark Cutting"

    Private Sub LoadTableCuttingBreakdown(TableKey As Object)
        Dim _Qry As String = ""

        Me.ogcmark.DataSource = Nothing
        Me.ogcbalance.DataSource = Nothing
        Me.ogcjobprodbal.DataSource = Nothing

        Dim _dt As DataTable
        Dim _dtctd As DataTable
        Dim _dttmp As DataTable

        Try
            _dt = _ListDataMarkBreakDown(1).Copy
        Catch ex As Exception
            _dt = Nothing
        End Try

        Me.ogcbalance.DataSource = _ListDataMarkBreakDown(0).Copy
        Me.ogcjobprodbal.DataSource = _ListDataMarkBreakDown(0).Copy
        FNFabricFrontSize.Value = 0
        FNMarkYRD.Value = 0
        FNMarkINC.Value = 0
        FNMarkSpare.Value = _TFNMarkSpare
        FNMarkTotal.Value = 0
        FTRef.Text = ""

        _Qry = " Select TOp 1 A.FTNote, A.FNHSysUnitSectId,ISNULL(B.FTUnitSectCode,'') AS FTUnitSectCode,ISNULL(A.FTStateRepair,'') AS FTStateRepair "
        _Qry &= vbCrLf & "  , A.FNFabricFrontSize, A.FNMarkYRD,A.FNMarkINC, A.FNMarkSpare, A.FNMarkTotal,A.FTRef,A.FTStateBundle,A.FTStateCutchange,A.FTStatTableScrap"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS A WITH(NOLOCK)  "
        _Qry &= vbCrLf & "        LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH(NOLOCK) "
        _Qry &= vbCrLf & " ON A.FNHSysUnitSectId=B.FNHSysUnitSectId "
        _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
        _Qry &= vbCrLf & " AND  A.FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
        _Qry &= vbCrLf & " AND A.FNTableNo=" & Integer.Parse(Val(TableKey.ToString)) & " "

        _dttmp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        FTRemark.Text = ""
        FNHSysUnitSectId.Text = ""
        FTStateRepair.Checked = False
        FTStateBundle.Checked = False
        FTStateCutchange.Checked = False
        FTStatTableScrap.Checked = False

        For Each R As DataRow In _dttmp.Rows

            FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
            FTRemark.Text = R!FTNote.ToString
            FTStateRepair.Checked = (R!FTStateRepair.ToString = "1")
            FNFabricFrontSize.Value = Val(R!FNFabricFrontSize.ToString)
            FNMarkYRD.Value = Val(R!FNMarkYRD.ToString)
            FNMarkINC.Value = Val(R!FNMarkINC.ToString)
            FNMarkSpare.Value = Val(R!FNMarkSpare.ToString) '_TFNMarkSpare
            FNMarkTotal.Value = Val(R!FNMarkTotal.ToString)
            FTRef.Text = R!FTRef.ToString
            FTStateBundle.Checked = (R!FTStateBundle.ToString = "1")
            FTStateCutchange.Checked = (R!FTStateCutchange.ToString = "1")
            FTStatTableScrap.Checked = (R!FTStatTableScrap.ToString = "1")
            Exit For
        Next

        _Qry = "SELECT       FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity , isnull(FNTableNoRef, 0) as  FNTableNoRef "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS D WITH(Nolock)"

        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) And Not (Me.otbmarkcutting.SelectedTabPage Is Nothing) Then
            _Qry &= vbCrLf & "  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & "  AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
        Else
            _Qry &= vbCrLf & "  WHERE FTOrderProdNo='' "
            _Qry &= vbCrLf & "  AND FNHSysMarkId=0"
        End If

        _dtctd = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        '------Start Set ยอด ตั้งต้น
        With CType(Me.ogcjobprodbal.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In _dtctd.Select("FNTableNo<" & Val(TableKey) & "")
                For Each Rx As DataRow In .Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

                    Try
                        Rx.Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val(Rx.Item(R!FTSizeBreakDown.ToString))) - Integer.Parse(Val(R!FNQuantity.ToString))
                    Catch ex As Exception
                    End Try
                Next
            Next

            For Each Rx As DataRow In .Rows

                Dim _RowTotal As Integer = 0
                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNAssort".ToUpper, "FNTableNoRef".ToUpper
                        Case Else
                            _RowTotal = _RowTotal + Rx.Item(Col.ColumnName.ToString)
                    End Select
                Next

                Rx!Total = _RowTotal
            Next

            .AcceptChanges()
        End With
        '------ฎืก Set ยอด ตั้งต้น

        '------Start Set ยอด Balance
        With CType(Me.ogcbalance.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In _dtctd.Select("FNTableNo<=" & Val(TableKey) & "")
                For Each Rx As DataRow In .Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

                    Try
                        Rx.Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val(Rx.Item(R!FTSizeBreakDown.ToString))) - Integer.Parse(Val(R!FNQuantity.ToString))
                    Catch ex As Exception
                    End Try
                Next
            Next

            For Each Rx As DataRow In .Rows

                Dim _RowTotal As Integer = 0
                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNAssort".ToUpper, "FNTableNoRef".ToUpper
                        Case Else
                            _RowTotal = _RowTotal + Rx.Item(Col.ColumnName.ToString)
                    End Select
                Next

                Rx!Total = _RowTotal
            Next

            .AcceptChanges()
        End With
        '------End Set ยอด Balance

        '------Start Set ยอด Table Cutting
        For Each R As DataRow In _dtctd.Select("FNTableNo=" & Val(TableKey) & "")
            For Each Rx As DataRow In _dt.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

                Try
                    _dt.Rows(0).Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val(R!FNAssort.ToString))
                    Rx.Item("FNAssort") = Integer.Parse(Val((R!FNLayer.ToString)))
                    Rx.Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val((R!FNQuantity.ToString)))
                    Rx.Item("FNTableNoRef") = Integer.Parse(Val((R!FNTableNoRef.ToString)))
                Catch ex As Exception
                End Try
            Next
        Next
        '------End Set ยอด Table Cutting

        _dtctd.Dispose()
        Dim _colcount As Integer = 0

        With Me.ogvmark

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper, "FNTableNoRef".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper, "FNTableNoRef".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString


                                If Not (Col.ColumnName.ToString = "Total") Then
                                    .ColumnEdit = ReposFNAssort
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

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = True
                                    .ReadOnly = False
                                End With

                            End With
                            .Columns(Col.ColumnName.ToString).Width = 45
                            '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcmark.DataSource = _dt.Copy
        _dt.Dispose()
    End Sub

    Private Sub LoadOrderProdMarkCutting(OrderProdKey As Object)
        ' Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        otbmarkcutting.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = " SELECT A.FNHSysMarkId,B.FTMarkCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,B.FTMarkNameTH AS FTMarkName"
        Else
            _Qry &= vbCrLf & " ,B.FTMarkNameEN AS FTMarkName"
        End If

        _Qry &= vbCrLf & "  ,ISNULL(("

        _Qry &= vbCrLf & "  SELECT Count(XXA.FNTableNo) AS FNTotalTable"
        _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS XXA WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE  (XXA.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' ) "
        _Qry &= vbCrLf & " AND (XXA.FNHSysMarkId = A.FNHSysMarkId)"

        _Qry &= vbCrLf & "  ),0) AS FNTableCount"

        _Qry &= vbCrLf & "    FROM"
        _Qry &= vbCrLf & "  (SELECT     1 AS FNSeq, FTOrderProdNo, FNHSysMarkId"
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT     2 AS FNSeq, FTOrderProdNo, FNHSysSubMarkId AS FNHSysMarkId"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub  WITH(NOLOCK)   "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & " ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS B ON A.FNHSysMarkId = B.FNHSysMarkId"
        _Qry &= vbCrLf & "  Order BY A.FNSeq,B.FTMarkCode"

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FNHSysMarkId.ToString
                .Text = R!FTMarkCode.ToString & " ( " & R!FTMarkName.ToString & " )-Cut " & R!FNTableCount.ToString & "  Table"
            End With

            otbmarkcutting.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbmarkcutting.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()
        ' _Spls.Close()

    End Sub

#End Region

#Region "Table Cutting"

    Private Sub LoadOrderProdMarkTableCutting(OrderProdKey As Object, MarkID As Object)
        'Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        otbtable.TabPages.Clear()
        Dim _TotalTable As Integer = 0

        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = " SELECT FNTableNo "
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTOrderProd_TableCut  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & "  AND FNHSysMarkId =" & Val(MarkID.ToString) & " "
        _Qry &= vbCrLf & "  Order BY FNTableNo"

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows
            _TotalTable = _TotalTable + 1

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FNTableNo.ToString
                .Text = R!FNTableNo.ToString
            End With

            otbtable.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbtable.SelectedTabPageIndex = 0
        End If

        Try
            Dim _Text As String = Me.otbmarkcutting.SelectedTabPage.Text

            Me.otbmarkcutting.SelectedTabPage.Text = _Text.Split(")-Cut")(0) & ")-Cut " & _TotalTable.ToString & "  Table"
           
        Catch ex As Exception

        End Try
        _dtprod.Dispose()
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

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

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

            Try

                _Qry = " DELETE FROM A	 "
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                _Qry &= vbCrLf & "     LEFT OUTER JOIN "
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & "   SELECT FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail"
                _Qry &= vbCrLf & "   WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "')"
                _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "    WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' AND B.FTOrderNo Is NULL"

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Catch ex As Exception
            End Try

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' ")

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function DeleteTableCutting(Optional StateAllTable As Boolean = False) As Boolean

        CType(ogcmark.DataSource, DataTable).AcceptChanges()

        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

            If Not (StateAllTable) Then
                _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

            If Not (StateAllTable) Then
                _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

            If Not (StateAllTable) Then
                _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

            If Not (StateAllTable) Then
                _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            End If


            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function SaveTableCutting() As Boolean

        CType(ogcmark.DataSource, DataTable).AcceptChanges()

        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " Select TOp 1 FTOrderProdNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId"
                _Qry &= vbCrLf & ", FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef,FTStateBundle,FTStateCutchange,FTStatTableScrap) "
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                _Qry &= vbCrLf & " ," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " ,'" & FTStateRepair.EditValue.ToString & "' "
                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                _Qry &= vbCrLf & "," & FNFabricFrontSize.Value & " "
                _Qry &= vbCrLf & "," & FNMarkYRD.Value & ""
                _Qry &= vbCrLf & "," & FNMarkINC.Value & ""
                _Qry &= vbCrLf & "," & FNMarkSpare.Value & ""
                _Qry &= vbCrLf & "," & FNMarkTotal.Value & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRef.Text) & "' "
                _Qry &= vbCrLf & " ,'" & FTStateBundle.EditValue.ToString & "' "
                _Qry &= vbCrLf & " ,'" & FTStateCutchange.EditValue.ToString & "' "
                _Qry &= vbCrLf & " ,'" & FTStatTableScrap.EditValue.ToString & "' "
            Else

                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
                _Qry &= vbCrLf & " SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,FTNote='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                _Qry &= vbCrLf & " ,FNHSysUnitSectId=" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " ,FTStateRepair='" & FTStateRepair.EditValue.ToString & "'"
                _Qry &= vbCrLf & ",FNFabricFrontSize=" & FNFabricFrontSize.Value & " "
                _Qry &= vbCrLf & ",FNMarkYRD=" & FNMarkYRD.Value & ""
                _Qry &= vbCrLf & ",FNMarkINC=" & FNMarkINC.Value & ""
                _Qry &= vbCrLf & ",FNMarkSpare=" & FNMarkSpare.Value & ""
                _Qry &= vbCrLf & ",FNMarkTotal=" & FNMarkTotal.Value & ""
                _Qry &= vbCrLf & " ,FTRef='" & HI.UL.ULF.rpQuoted(FTRef.Text) & "' "
                _Qry &= vbCrLf & " ,FTStateBundle='" & FTStateBundle.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,FTStateCutchange='" & FTStateCutchange.EditValue.ToString & "' "
                _Qry &= vbCrLf & " ,FTStatTableScrap='" & FTStatTableScrap.EditValue.ToString & "' "
                _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            With CType(ogcmark.DataSource, DataTable)
                For Each R As DataRow In .Rows

                    For Each Col As DataColumn In .Columns
                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper, "FNTableNoRef".ToUpper
                            Case Else
                                If IsNumeric(R.Item(Col.ColumnName.ToString)) Then

                                    If Integer.Parse(R.Item(Col.ColumnName.ToString)) > 0 AndAlso R!FTColorway.ToString <> "" Then

                                        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTOrderProd_TableCut_Detail "
                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity,FNHSysCmpId ,FNTableNoRef) "
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                                        _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' "
                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNAssort.ToString)) & " "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(.Rows(0).Item(Col.ColumnName.ToString))) & " "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(R.Item(Col.ColumnName.ToString)) & " "
                                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNTableNoRef.ToString)) & " "

                                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                            Return False
                                        End If

                                    End If

                                End If
                        End Select

                    Next

                Next
            End With

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

    Private Function ValidateTableCutting(Optional StateSave As Boolean = True) As Boolean
        If Not (Me.otbtable.SelectedTabPage Is Nothing) Then

            If (StateSave) Then
                If Me.FNHSysUnitSectId.Text <> "" And Me.FNHSysUnitSectId.Properties.Tag.ToString <> "" Then
                    Return True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitSectId_lbl.Text)
                    FNHSysUnitSectId.Focus()
                    FNHSysUnitSectId.SelectAll()
                    Return False
                End If
            Else
                Return True
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Table Cutting", 1404210001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Function ValidateCreateTableCut() As Boolean
        Dim _Qry As String
        _Qry = "Select Top 1 FTOrderProdNo "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function DeleteSubOrder(Optional StateAll As Boolean = False) As Boolean

        ' CType(ogcmark.DataSource, DataTable).AcceptChanges()

        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "

            If Not (StateAll) Then
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.otbsuborder.SelectedTabPage.Name.ToString) & "' "
            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            Try

                _Qry = " DELETE FROM A	 "
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                _Qry &= vbCrLf & "     LEFT OUTER JOIN "
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & "   SELECT FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail"
                _Qry &= vbCrLf & "   WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "')"
                _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "    WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' AND B.FTOrderNo Is NULL"

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Catch ex As Exception
            End Try

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function CheckTableCreateLayCut(Optional StateAll As Boolean = False) As Boolean
        Dim _Qry As String = ""

        If (Me.otbtable.SelectedTabPage Is Nothing) Then
            Return False
        End If

        _Qry = " Select TOP 1  FTLayCutNo"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE (FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "')"
        _Qry &= vbCrLf & " AND (FNHSysMarkId =" & Integer.Parse(Val(otbmarkcutting.SelectedTabPage.Name.ToString)) & ")"

        If Not (StateAll) Then
            _Qry &= vbCrLf & "  AND (FNTableNo =" & Integer.Parse(Val(otbtable.SelectedTabPage.Name.ToString)) & ")"
        End If

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")
    End Function
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



        Call InitGrid()
        HI.TL.HandlerControl.SetStateProcClear = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        ' HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysCmpId, New EventArgs)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Call ClearGrid()
        FTOrderNo.Focus()
        FTOrderNo.SelectAll()
        otbdetail.SelectedTabPageIndex = 0
        FNFabricFrontSize.Value = 0
        FNMarkYRD.Value = 0
        FNMarkINC.Value = 0
        FNMarkSpare.Value = _TFNMarkSpare
        FNMarkTotal.Value = 0
    End Sub

    Private Sub ocmgeneratejobprod_Click(sender As Object, e As EventArgs) Handles ocmgeneratejobprod.Click
        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then

            With _GenJobProd
                .OrderNo = Me.FTOrderNo.Text
                .JobProdNo = ""
                Call HI.ST.Lang.SP_SETxLanguage(_GenJobProd)
                .FNCreateJobProdType.Visible = True
                .ShowDialog()

                If (.Process) Then
                    Call LoadOrderProdDataInfo(Me.FTOrderNo.Text)
                    Me.otbdetail.SelectedTabPageIndex = 0

                    Try
                        otbjobprod.SelectedTabPageIndex = (otbjobprod.TabPages.Count - 1)
                    Catch ex As Exception

                    End Try
                End If

            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
            FTOrderNo.SelectAll()
        End If
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then

            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})

        Else

            Call LoadOrderProdDataInfo(FTOrderNo.Text)
            Me.otbdetail.SelectedTabPageIndex = 0

        End If

    End Sub

    Private Sub otbjobprod_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbjobprod.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbjobprod_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbjobprod.SelectedTabPage Is Nothing) Then

                    Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdBreakDown(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdMainMark(otbjobprod.SelectedTabPage.Name.ToString)

                    Call LoadOrderProdMarkCutting(otbjobprod.SelectedTabPage.Name.ToString)

                Else
                    Call ClearGrid(True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbsuborder_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbsuborder.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbsuborder_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbsuborder.SelectedTabPage Is Nothing) Then
                    Call LoadOrderProdSubBreakDown(otbjobprod.SelectedTabPage.Name.ToString, otbsuborder.SelectedTabPage.Name.ToString)
                Else
                    Me.ogcsub.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddsuborder_Click(sender As Object, e As EventArgs) Handles ocmaddsuborder.Click

        'If ValidateCreateTableCut() Then
        '    HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        Call SavSubMark()

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            If Not (otbsuborder.SelectedTabPage Is Nothing) Then
                With _GenJobProd
                    .OrderNo = Me.FTOrderNo.Text
                    .JobProdNo = otbjobprod.SelectedTabPage.Name.ToString
                    .FNCreateJobProdType.Visible = False
                    Call HI.ST.Lang.SP_SETxLanguage(_GenJobProd)
                    .ShowDialog()

                    If (.Process) Then

                        Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Name.ToString)
                        Call LoadOrderProdBreakDown(otbjobprod.SelectedTabPage.Name.ToString)

                    End If

                End With
            Else
                HI.MG.ShowMsg.mInfo("", 1405110004, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
            FTOrderNo.SelectAll()
        End If
    End Sub

    Private Sub ogvmainmark_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvmainmark.FocusedRowChanged
        With ogvmainmark
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Call LoadOrderProdSubMark(otbjobprod.SelectedTabPage.Name.ToString, .GetFocusedRowCellValue("FNHSysMarkId_Hide").ToString)
        End With
    End Sub

#Region "Init New Row Data"

#End Region

    Private Sub InitMainMarkNewRow()
        With CType(Me.ogcmainmark.DataSource, DataTable)
            .Rows.Add()
            .Rows(.Rows.Count - 1)!FNHSysMarkId_None = ""
            .Rows(.Rows.Count - 1)!FNHSysMarkId = ""
            .Rows(.Rows.Count - 1)!FNHSysMarkId_Hide = 0
            .Rows(.Rows.Count - 1)!FTOrderProdNo = Me.otbjobprod.SelectedTabPage.Name.ToString
            .AcceptChanges()
        End With
    End Sub

    Private Sub InitSubMarkNewRow()
        With CType(Me.ogcsubmark.DataSource, DataTable)
            .Rows.Add()
            .Rows(.Rows.Count - 1)!FNHSysMarkId_None = ""
            .Rows(.Rows.Count - 1)!FNHSysMarkId = ""
            .Rows(.Rows.Count - 1)!FNHSysMarkId_Hide = 0
            .Rows(.Rows.Count - 1)!FNHSysMainMarkId = Me.ogvmainmark.GetFocusedRowCellValue("FNHSysMarkId_Hide")
            .Rows(.Rows.Count - 1)!FTOrderProdNo = Me.otbjobprod.SelectedTabPage.Name.ToString
            .AcceptChanges()
        End With
    End Sub

    Private Sub ogvmainmark_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvmainmark.KeyDown
        Try
            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.Delete
                    If ValidateCreateTableCut() Then
                        HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If
            End Select

            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Down
                    With CType(Me.ogcmainmark.DataSource, DataTable)

                        .AcceptChanges()

                        If .Select("FNHSysMarkId='' ").Length <= 0 Then

                            Call InitMainMarkNewRow()

                            .AcceptChanges()

                            Me.ogvmainmark.ClearSelection()
                            Me.ogvmainmark.SelectRow(.Rows.Count - 1)
                            Me.ogvmainmark.FocusedRowHandle = .Rows.Count - 1
                            Me.ogvmainmark.FocusedColumn = Me.ogvmainmark.Columns.ColumnByFieldName("FNHSysMarkId")

                        End If

                    End With
                Case System.Windows.Forms.Keys.Delete
                    Me.ogvmainmark.DeleteRow(Me.ogvmainmark.FocusedRowHandle)
                    With CType(Me.ogcmainmark.DataSource, DataTable)
                        .AcceptChanges()
                    End With
                Case System.Windows.Forms.Keys.Enter

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvsubmark_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvsubmark.CellValueChanged
        Static Proc As Boolean
        Try

            If Not (Proc) Then
                Proc = True

                With ogvsubmark
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    If Val("" & .GetFocusedRowCellValue("FNHSysMarkId_Hide").ToString) <> 0 Then
                        .SetFocusedRowCellValue("FNHSysMainMarkId", Me.ogvmainmark.GetFocusedRowCellValue("FNHSysMarkId_Hide"))
                    End If

                End With

                Proc = False

            End If

        Catch ex As Exception
            Proc = False
        End Try
    End Sub

    Private Sub ogvsubmark_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvsubmark.KeyDown

        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.Delete
                If ValidateCreateTableCut() Then
                    HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If

        End Select

        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                If Me.ogvmainmark.Columns.ColumnByFieldName("FNHSysMarkId").ToString = "" Then
                    Exit Sub
                End If
                With CType(Me.ogcsubmark.DataSource, DataTable)

                    .AcceptChanges()

                    If .Select("FNHSysMarkId='' ").Length <= 0 Then

                        Call InitSubMarkNewRow()

                        .AcceptChanges()

                        Me.ogvsubmark.ClearSelection()
                        Me.ogvsubmark.SelectRow(.Rows.Count - 1)
                        Me.ogvsubmark.FocusedRowHandle = .Rows.Count - 1
                        Me.ogvsubmark.FocusedColumn = Me.ogvsubmark.Columns.ColumnByFieldName("FNHSysMarkId")

                    End If

                End With
            Case System.Windows.Forms.Keys.Delete
                Me.ogvsubmark.DeleteRow(Me.ogvmainmark.FocusedRowHandle)
                With CType(Me.ogcsubmark.DataSource, DataTable)
                    .AcceptChanges()
                End With
            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub

    Private Sub ReposSubFNHSysMarkId_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposSubFNHSysMarkId.EditValueChanging
        Try
            If (Me.ogvmainmark.Columns.ColumnByFieldName("FNHSysMarkId").ToString = "") Or (ocmsavesubmark.Enabled = False) Then
                e.Cancel = True
            Else
                If CType(Me.ogcsubmark.DataSource, DataTable).Select("FNHSysMarkId='" & HI.UL.ULF.rpQuoted(e.NewValue.ToString()) & "' ").Length > 0 Then
                    e.Cancel = True
                    'Me.ogvsubmark.SetFocusedRowCellValue("FNHSysMarkId", "")
                    'Me.ogvsubmark.SetFocusedRowCellValue("FNHSysMarkId_None", "")
                    'Me.ogvsubmark.SetFocusedRowCellValue("FNHSysMarkId_Hide", "0")
                Else
                    If ValidateCreateTableCut() Then
                        e.Cancel = True
                        HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

                        'With ogvmainmark
                        '    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                        '    Call LoadOrderProdSubMark(otbjobprod.SelectedTabPage.Name.ToString, .GetFocusedRowCellValue("FNHSysMarkId_Hide").ToString)
                        'End With

                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReposFNHSysMarkId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ReposFNHSysMarkId.ButtonClick

    End Sub

    Private Sub ReposFNHSysMarkId_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNHSysMarkId.EditValueChanging
        Try
            If (CType(Me.ogcmainmark.DataSource, DataTable).Select("FNHSysMarkId='" & HI.UL.ULF.rpQuoted(e.NewValue.ToString()) & "' ").Length > 0) Or (ocmsavemainmark.Enabled = False) Then
                e.Cancel = True
            Else
                If ValidateCreateTableCut() Then
                    e.Cancel = True
                    HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    'Call LoadOrderProdMainMark(otbjobprod.SelectedTabPage.Name.ToString)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged

        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbdetail_SelectedPageChanged), New Object() {sender, e})
            Else
                Call TabChenge()

                Select Case otbdetail.SelectedTabPage.Name.ToString
                    Case otporderprodcuttingdetail.Name.ToString

                        If Me.ocmsavemainmark.Enabled Then
                            If ValidateCreateTableCut() Then
                            Else
                                Call SaveMainMark()
                            End If

                        End If

                        If Me.ocmsavesubmark.Enabled Then
                            If ValidateCreateTableCut() Then
                            Else
                                Call SavSubMark()
                            End If
                        End If

                        Call LoadOrderProdMarkCutting(otbjobprod.SelectedTabPage.Name.ToString)

                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbmarkcutting_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmarkcutting.SelectedPageChanged

        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbmarkcutting_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbmarkcutting.SelectedTabPage Is Nothing) Then
                    Call LoadOrderProdMarkBreakDown(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdMarkTableCutting(otbjobprod.SelectedTabPage.Name.ToString, otbmarkcutting.SelectedTabPage.Name.ToString)
                Else
                    Me.otbtable.TabPages.Clear()
                    Call LoadOrderProdMarkBreakDown("")
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvmark_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvmark.KeyDown
        Try
            With ogvmark
                If .RowCount < 1 Then Exit Sub
                If .FocusedRowHandle <= 0 Then Exit Sub
                If Me.otbtable.SelectedTabPage Is Nothing Then Exit Sub

                Select Case e.KeyCode
                    Case System.Windows.Forms.Keys.F6
                        Call ocmmappomatcolor_Click(ocmmappomatcolor, New System.EventArgs)

                End Select
            End With
        Catch ex As Exception

        End Try


    End Sub


    Private Sub ogvmark_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvmark.RowCellStyle
        With ogvmark
            If .RowCount < 0 Then Exit Sub

            Select Case True
                Case (e.RowHandle = 0 And e.Column.FieldName = "FNAssort")
                    'e.Column.OptionsColumn.AllowEdit = False
                    'e.Column.OptionsColumn.ReadOnly = True
                    e.Appearance.BackColor = Color.LemonChiffon
                    e.Appearance.ForeColor = Color.Black
                Case (e.RowHandle = 0 And e.Column.FieldName = "FTColorway")
                    'e.Column.OptionsColumn.AllowEdit = False
                    'e.Column.OptionsColumn.ReadOnly = True
                    e.Appearance.BackColor = Color.LemonChiffon
                    e.Appearance.ForeColor = Color.Black
                Case (e.RowHandle = 0 And e.Column.FieldName <> "FTColorway" And e.Column.FieldName <> "FNAssort")
                    'e.Column.OptionsColumn.AllowEdit = True
                    'e.Column.OptionsColumn.ReadOnly = False
                    e.Appearance.BackColor = Color.LightCyan
                    e.Appearance.ForeColor = Color.Blue
                Case (e.RowHandle > 0 And e.Column.FieldName <> "FTColorway" And e.Column.FieldName <> "FNAssort")
                    'e.Column.OptionsColumn.AllowEdit = False
                    'e.Column.OptionsColumn.ReadOnly = True
                    e.Appearance.BackColor = Color.LemonChiffon
                    e.Appearance.ForeColor = Color.Black
                Case (e.RowHandle > 0 And e.Column.FieldName = "FTColorway")
                    'e.Column.OptionsColumn.AllowEdit = False
                    'e.Column.OptionsColumn.ReadOnly = True
                    e.Appearance.BackColor = Color.LemonChiffon
                    e.Appearance.ForeColor = Color.Black
                Case (e.RowHandle > 0 And e.Column.FieldName = "FNAssort")
                    'e.Column.OptionsColumn.AllowEdit = True
                    'e.Column.OptionsColumn.ReadOnly = False
                    e.Appearance.BackColor = Color.LightCyan
                    e.Appearance.ForeColor = Color.Blue

                Case Else
            End Select

        End With
    End Sub

    Private Sub otbtable_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbtable.SelectedPageChanged

        Try
            If (Me.InvokeRequired) Then

                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbtable_SelectedPageChanged), New Object() {sender, e})

            Else

                If Not (otbtable.SelectedTabPage Is Nothing) Then
                    Call LoadTableCuttingBreakdown(otbtable.SelectedTabPage.Name.ToString)
                Else
                    Call LoadTableCuttingBreakdown("0")
                End If

            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvmark_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvmark.ShowingEditor
        With ogvmark
            If .RowCount < 0 Then Exit Sub

            Select Case True
                Case (.FocusedRowHandle = 0 And .FocusedColumn.FieldName = "FNAssort")
                    e.Cancel = True
                Case (.FocusedRowHandle = 0 And .FocusedColumn.FieldName = "FTColorway")
                    e.Cancel = True
                Case (.FocusedRowHandle = 0 And .FocusedColumn.FieldName <> "FTColorway" And .FocusedColumn.FieldName <> "FNAssort")
                    e.Cancel = False
                Case (.FocusedRowHandle > 0 And .FocusedColumn.FieldName <> "FTColorway" And .FocusedColumn.FieldName <> "FNAssort")
                    e.Cancel = True
                Case (.FocusedRowHandle > 0 And .FocusedColumn.FieldName = "FTColorway")
                    e.Cancel = True
                Case (.FocusedRowHandle > 0 And .FocusedColumn.FieldName = "FNAssort")
                    e.Cancel = False
                Case Else
            End Select

        End With
    End Sub

    Private Sub ReposFNAssort_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNAssort.EditValueChanging
        Static Proc As Boolean

        If Not (Proc) Then
            Proc = True

            Try
                Dim _RowIdx As Integer = 0
                Dim _ColIdx As String = ""
                Dim Qty As Integer
                With Me.ogvmark

                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                        Proc = False
                        Exit Sub
                    End If

                    If Me.CheckTableCreateLayCut() Then
                        ' HI.MG.ShowMsg.mInfo("พบข้อมูลการทำ Laycut แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1505220001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        e.Cancel = True
                        Proc = False
                        Exit Sub
                    End If

                    Select Case True
                        Case .FocusedRowHandle = 0 And .FocusedColumn.FieldName.ToString <> "FTColorway" And .FocusedColumn.FieldName.ToString <> "FNAssort"
                            _ColIdx = .FocusedColumn.FieldName.ToString

                            _RowIdx = 0
                            With CType(ogcmark.DataSource, DataTable)
                                .AcceptChanges()

                                For Each Row As DataRow In .Rows

                                    If _RowIdx > 0 Then
                                        If IsNumeric(Row.Item("FNAssort")) Then
                                            Qty = Integer.Parse(Row.Item("FNAssort"))
                                        Else
                                            Qty = 0
                                        End If

                                        Row.Item(_ColIdx) = Integer.Parse(Qty * Val(e.NewValue))

                                        With (CType(ogcbalance.DataSource, DataTable))
                                            .AcceptChanges()
                                            .Rows(_RowIdx - 1).Item(_ColIdx) = Integer.Parse((CType(ogcjobprodbal.DataSource, DataTable)).Rows(_RowIdx - 1).Item(_ColIdx)) - Integer.Parse(Row.Item(_ColIdx))
                                            .Rows(_RowIdx - 1).Item("Total") = 0

                                            For Each Col As DataColumn In .Columns
                                                Select Case Col.ColumnName.ToString.ToUpper
                                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                                                    Case Else
                                                        .Rows(_RowIdx - 1).Item("Total") = Integer.Parse(.Rows(_RowIdx - 1).Item("Total")) + Integer.Parse(.Rows(_RowIdx - 1).Item(Col.ColumnName.ToString))
                                                End Select
                                            Next

                                            .AcceptChanges()
                                        End With

                                    End If

                                    _RowIdx = _RowIdx + 1
                                Next

                                .AcceptChanges()
                            End With

                        Case .FocusedRowHandle > 0 And .FocusedColumn.FieldName.ToString = "FNAssort"
                            _RowIdx = .FocusedRowHandle

                            With CType(ogcmark.DataSource, DataTable)
                                .AcceptChanges()

                                For Each Col As DataColumn In .Columns
                                    Select Case Col.ColumnName.ToString.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper, "FNTableNoRef".ToUpper
                                        Case Else

                                            If IsNumeric(.Rows(0).Item(Col.ColumnName.ToString)) Then
                                                Qty = Integer.Parse(.Rows(0).Item(Col.ColumnName.ToString))
                                            Else
                                                Qty = 0
                                            End If

                                            .Rows(_RowIdx).Item(Col.ColumnName.ToString) = Integer.Parse(Qty * Val(e.NewValue))

                                            With (CType(ogcbalance.DataSource, DataTable))
                                                .AcceptChanges()
                                                .Rows(_RowIdx - 1).Item(Col.ColumnName.ToString) = Integer.Parse((CType(ogcjobprodbal.DataSource, DataTable)).Rows(_RowIdx - 1).Item(Col.ColumnName.ToString)) - Integer.Parse(Qty * Val(e.NewValue))

                                                .AcceptChanges()
                                            End With

                                    End Select
                                Next

                                With (CType(ogcbalance.DataSource, DataTable))
                                    .AcceptChanges()
                                    .Rows(_RowIdx - 1).Item("Total") = 0

                                    For Each Col As DataColumn In .Columns
                                        Select Case Col.ColumnName.ToString.ToUpper
                                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                                            Case Else
                                                .Rows(_RowIdx - 1).Item("Total") = Integer.Parse(.Rows(_RowIdx - 1).Item("Total")) + Integer.Parse(.Rows(_RowIdx - 1).Item(Col.ColumnName.ToString))
                                        End Select
                                    Next

                                    .AcceptChanges()
                                End With


                                .AcceptChanges()
                            End With
                    End Select
                End With

            Catch ex As Exception
                e.Cancel = True
            End Try

            Proc = False
        End If
    End Sub

    Private Sub ocmdeletejobprod_Click(sender As Object, e As EventArgs) Handles ocmdeletejobprod.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then

            If ValidateCreateTableCut() = False Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบ Order Producttion ใช่หรือไม่ ?", 1404180003, Me.otbjobprod.SelectedTabPage.Name.ToString) = True Then

                    If Me.DeleteOrderProd(Me.otbjobprod.SelectedTabPage.Name.ToString) Then
                        Call LoadOrderProdDataInfo(Me.FTOrderNo.Text)
                    End If

                End If
            Else
                HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180002, Me.Text)
            End If

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Order Producttion ที่ต้องการลบ ", 1404180001, Me.Text)
        End If

    End Sub

    Private Sub ocmsavetable_Click(sender As Object, e As EventArgs) Handles ocmsavetable.Click
        If ValidateTableCutting() Then

            If Me.CheckTableCreateLayCut() Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการทำ Laycut แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1505220001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            If SaveTableCutting() Then
                HI.MG.ShowMsg.mInfo("Save Data Complete !!!", 1404210006, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
            Else
                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการบันทึกข้อมูลโต๊ะตัดได้ !!!", 1404210005, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    Private Sub ocmaddtable_Click(sender As Object, e As EventArgs) Handles ocmaddtable.Click

        If Not (Me.otbmarkcutting.SelectedTabPage Is Nothing) Then
            If Not (Me.otbtable.SelectedTabPage Is Nothing) And Me.ocmsavetable.Enabled Then
                If Me.ValidateTableCutting Then
                    If Me.CheckTableCreateLayCut() = False Then
                        If Not SaveTableCutting() Then
                            'HI.MG.ShowMsg.mInfo("ไม่สามารถทำการบันทึกข้อมูลโต๊ะตัดได้ !!!", 1404210007, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                            'Exit Sub
                        End If
                    End If
                Else
                    Exit Sub
                End If

            End If

            Dim _Qry As String
            Dim _TableNo As Integer

            _Qry = " Select TOp 1 FNTableNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            _Qry &= vbCrLf & "  Order BY FNTableNo Desc  "

            _TableNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))) + 1

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = _TableNo.ToString
                .Text = _TableNo.ToString
            End With

            otbtable.TabPages.Add(Otp)
            otbtable.SelectedTabPage = otbtable.TabPages(otbtable.TabPages.Count - 1)


            Dim _TotalTable As Integer = otbtable.TabPages.Count

            Try
                Dim _Text As String = Me.otbmarkcutting.SelectedTabPage.Text

                Me.otbmarkcutting.SelectedTabPage.Text = _Text.Split(")-Cut")(0) & ")-Cut " & _TotalTable.ToString & "  Table"

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub ocmdeletetable_Click(sender As Object, e As EventArgs) Handles ocmdeletetable.Click
        If Me.ValidateTableCutting(False) Then

            If Me.CheckTableCreateLayCut() Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการทำ Laycut แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1505220001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูลโต๊ะตัดใช่หรือไม่ ?", 1404210009, Me.otbtable.SelectedTabPage.Name.ToString) = True Then
                If Me.DeleteTableCutting(False) Then
                    Call LoadOrderProdMarkTableCutting(Me.otbjobprod.SelectedTabPage.Name.ToString, Me.otbmarkcutting.SelectedTabPage.Name.ToString)
                End If
            End If
        End If
    End Sub

    Private Sub ocmdeletealltable_Click(sender As Object, e As EventArgs) Handles ocmdeletealltable.Click
        If Not (Me.otbmarkcutting.SelectedTabPage Is Nothing) Then
            If Me.ValidateTableCutting(False) Then

                If Me.CheckTableCreateLayCut(True) Then
                    HI.MG.ShowMsg.mInfo("พบข้อมูลการทำ Laycut แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1505220001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูลโต๊ะตัดทั้งหมดใช่หรือไม่ ?", 1404210009) = True Then
                    If Me.DeleteTableCutting(True) Then
                        Call LoadOrderProdMarkTableCutting(Me.otbjobprod.SelectedTabPage.Name.ToString, Me.otbmarkcutting.SelectedTabPage.Name.ToString)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ocmdeletesuborder_Click(sender As Object, e As EventArgs) Handles ocmdeletesuborder.Click
        If Not (Me.otbsuborder.SelectedTabPage Is Nothing) Then

            If ValidateCreateTableCut() = False Then

                If Me.DeleteSubOrder(False) Then

                    Call LoadOrderProdDataInfo(FTOrderNo.Text)
                    Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdBreakDown(otbjobprod.SelectedTabPage.Name.ToString)

                Else
                    HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180078, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            Else
                HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    Private Sub ocmdeleteallsuborder_Click(sender As Object, e As EventArgs) Handles ocmdeleteallsuborder.Click
        If Not (Me.otbsuborder.SelectedTabPage Is Nothing) Then

            If ValidateCreateTableCut() = False Then

                If Me.DeleteSubOrder(True) Then
                    Call LoadOrderProdDataInfo(FTOrderNo.Text)
                    Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdBreakDown(otbjobprod.SelectedTabPage.Name.ToString)
                Else
                    HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180078, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            Else
                HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    Private Sub ocmsavemainmark_Click(sender As Object, e As EventArgs) Handles ocmsavemainmark.Click

        If ValidateCreateTableCut() Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        Call SaveMainMark()

    End Sub

    Private Sub ocmsavesubmark_Click(sender As Object, e As EventArgs) Handles ocmsavesubmark.Click
        If ValidateCreateTableCut() Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        Call SavSubMark()
    End Sub


    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderProdDataInfo(FTOrderNo.Text)
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub

    Private Sub ocmmappomatcolor_Click(sender As Object, e As EventArgs) Handles ocmmappomatcolor.Click
        Try
            With ogvmark
                If .RowCount < 1 Then Exit Sub
                If .FocusedRowHandle <= 0 Then Exit Sub
                If Me.otbtable.SelectedTabPage Is Nothing Then Exit Sub

                Dim ColorWayCode As String = "" & .GetFocusedRowCellValue("FTColorway").ToString

                With _MapPurchaseOrderRawmat
                    HI.ST.Lang.SP_SETxLanguage(_MapPurchaseOrderRawmat)

                    .FTOrderProdNo.Text = Me.otbjobprod.SelectedTabPage.Text
                    .FNHSysMarkId.Text = Me.otbmarkcutting.SelectedTabPage.Text
                    .FNTableNo.Text = Me.otbtable.SelectedTabPage.Text
                    .FTColorway.Text = ColorWayCode
                    .OrderNo = Me.FTOrderNo.Text
                    .OrderProdNo = Me.otbjobprod.SelectedTabPage.Name.ToString
                    .MarkID = Me.otbmarkcutting.SelectedTabPage.Name.ToString
                    .TableNo = Me.otbtable.SelectedTabPage.Name.ToString
                    .ColorWay = ColorWayCode
                    ' .DataPurchase = _ListPurchaseOrder(0)

                    'With .FTPurchaseNo
                    '    '.Properties.ValueMember = "FTPurchaseNo"
                    '    '.Properties.DisplayMember = "FTPurchaseNo"
                    '    .Properties.DataSource = _ListPurchaseOrder(0)
                    '    '.Properties.PopulateColumns()
                    '    Try
                    '        .Text = ""
                    '    Catch ex As Exception
                    '    End Try

                    '    .EditValue = ""
                    'End With

                    .FTColorCode.Text = ""
                    .FTColorName.Text = ""
                    .FTRawMatSizeCode.Text = ""
                    .ShowDialog()

                End With

            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmpreviewbytable_Click(sender As Object, e As EventArgs) Handles ocmpreviewbytable.Click
        If Not (Me.otbtable.SelectedTabPage Is Nothing) Then
            Call PreviewReport(0)
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Table Cutting", 1404210001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub PreviewReport(opt As Integer)

        Dim _FM As String = " {TPRODTOrderProd_TableCut.FTOrderProdNo}<>'' "

        Select Case opt
            Case 0
                _FM &= "  AND  {TPRODTOrderProd_TableCut.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                _FM &= "  AND  {TPRODTOrderProd_TableCut.FNHSysMarkId}=" & Integer.Parse(Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString)) & ""
                _FM &= "  AND  {TPRODTOrderProd_TableCut.FNTableNo}=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Text)) & ""
            Case 1
                _FM &= "  AND  {TPRODTOrderProd_TableCut.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
            Case 2
                _FM &= "  AND  {TPRODTOrderProd.FTOrderNo}='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
        End Select

        With New HI.RP.Report
            .FormTitle = Me.Text
            .ReportFolderName = "Production\"
            .ReportName = "OrderCutting.rpt"
            .Formular = _FM
            .Preview()
        End With

    End Sub

    Private Sub ocmpreviewalltable_Click(sender As Object, e As EventArgs) Handles ocmpreviewalltable.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then

            Call PreviewReport(1)

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Order Producttion ที่ต้องการลบ ", 1404180001, Me.Text)
        End If
    End Sub

    Private Sub ocmpreviewallprod_Click(sender As Object, e As EventArgs) Handles ocmpreviewallprod.Click
        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            Call PreviewReport(2)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
            FTOrderNo.SelectAll()
        End If
    End Sub

    Private Sub otbdetail_Click(sender As Object, e As EventArgs) Handles otbdetail.Click

    End Sub

    Private Sub otbsuborder_Click(sender As Object, e As EventArgs) Handles otbsuborder.Click

    End Sub

    Private Sub otbtable_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otbtable.SelectedPageChanging
        If Not (Me.otbtable.SelectedTabPage Is Nothing) And Me.ocmsavetable.Enabled Then
            If Me.ValidateTableCutting Then
                If Me.CheckTableCreateLayCut() = False Then
                    If Not SaveTableCutting() Then
                        e.Cancel = True
                    End If
                End If
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        If Me.ValidateTableCutting(False) Then
            With _CopyTable
                HI.ST.Lang.SP_SETxLanguage(_CopyTable)

                .FTOrderProdNo.Text = Me.otbjobprod.SelectedTabPage.Text
                .FNHSysMarkId.Text = Me.otbmarkcutting.SelectedTabPage.Text
                .FNTableNo.Text = Me.otbtable.SelectedTabPage.Text
                .FNTotal.Value = 1
                .ShowDialog()

                If .ProcessSave Then
                    Dim _Qry As String = ""
                    Dim _Fromable As Integer = Integer.Parse(Val(Me.otbtable.SelectedTabPage.Text))
                    Dim _MaxTable As Integer = 0

                    _Qry = "SELECT MAX(FNTableNo) AS FNTableNo "
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut WITH(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

                    _MaxTable = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")))

                    For I As Integer = 1 To .FNTotal.Value
                        _MaxTable = _MaxTable + 1

                        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal, FTRef,FTStateBundle,FTStateCutchange) "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                        _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                        _Qry &= vbCrLf & " ," & Integer.Parse(_MaxTable) & " "
                        _Qry &= vbCrLf & " , FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal, FTRef,FTStateBundle,FTStateCutchange "
                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut  WITH(NOLOCK) "
                        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                        _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                        _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(_Fromable) & " "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail "
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity) "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                        _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                        _Qry &= vbCrLf & " ," & Integer.Parse(_MaxTable) & " "
                        _Qry &= vbCrLf & ", FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail  WITH(NOLOCK)"
                        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                        _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                        _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(_Fromable) & " "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)


                        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTPurchaseNo, FNHSysRawMatId,FTShades)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                        _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                        _Qry &= vbCrLf & " ," & Integer.Parse(_MaxTable) & " "
                        _Qry &= vbCrLf & ",FTColorway, FTPurchaseNo, FNHSysRawMatId,FTShades "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat  WITH(NOLOCK)"
                        _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                        _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                        _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(_Fromable) & " "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    Next


                    Call LoadOrderProdMarkBreakDown(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdMarkTableCutting(otbjobprod.SelectedTabPage.Name.ToString, otbmarkcutting.SelectedTabPage.Name.ToString)

                    Try
                        otbtable.SelectedTabPageIndex = (otbtable.TabPages.Count - 1)
                    Catch ex As Exception
                    End Try

                End If
            End With
        End If
    End Sub


    Private Sub Cal_EditValueChanged(sender As Object, e As EventArgs) Handles FNMarkYRD.EditValueChanged, FNMarkINC.EditValueChanged
        Static _Proc As Boolean
        If Not _Proc Then
            _Proc = True

            FNMarkTotal.Value = FNMarkYRD.Value + CDbl(Format(((FNMarkINC.Value + FNMarkSpare.Value) / 36), "0.00"))

            _Proc = False
        End If
    End Sub


    Private Sub FNMarkINC_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNMarkINC.EditValueChanging
        Try
            If e.NewValue >= 36 Then
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ogvmainmark_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvmainmark.ShowingEditor
        Try
            If ValidateCreateTableCut() Then
                e.Cancel = True
                HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvsubmark_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvsubmark.ShowingEditor
        Try
            If ValidateCreateTableCut() Then
                e.Cancel = True
                HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbjobprod_Click(sender As Object, e As EventArgs) Handles otbjobprod.Click

    End Sub

    Public Sub SearchDataTableCut(TableCutNo As Integer)
        Try
            For Each Tp As DevExpress.XtraTab.XtraTabPage In otbtable.TabPages
                If Tp.Text = TableCutNo.ToString Then
                    otbtable.SelectedTabPage = Tp
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmfindtable_Click(sender As Object, e As EventArgs) Handles ocmfindtable.Click
        If Not (Me.otbtable.SelectedTabPage Is Nothing) Then
            HI.ST.Lang.SP_SETxLanguage(_FindTable)
            With _FindTable

                Dim _MarkID As Integer = Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString)
                .FTOrderProdNo.Text = Me.otbjobprod.SelectedTabPage.Text
                .MainParentForm = Me
                .ocmfind.Enabled = True
                .ocmcancel.Enabled = True

                Dim _dt As DataTable
                Dim _Qry As String = ""
                Dim _MinTable As Integer = 0
                Dim _MaxTable As Integer = 0

                _Qry = "   SELECT Min(FNTableNo) AS FNTableNoMin,Max(FNTableNo) AS FNTableNoMax"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS X WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"
                _Qry &= vbCrLf & "  AND (FNHSysMarkId = " & _MarkID & ")"

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                For Each R As DataRow In _dt.Rows
                    _MinTable = Val(R!FNTableNoMin.ToString)
                    _MaxTable = Val(R!FNTableNoMax.ToString)

                    Exit For
                Next

                .FNSTableCutNo.Value = _MinTable
                .FNETableCutNo.Value = _MaxTable
                .FNTableCutNo.Value = 1
                .ShowDialog()

                Call LoadOrderProdMarkBreakDown(otbjobprod.SelectedTabPage.Name.ToString)
                Call LoadOrderProdMarkTableCutting(otbjobprod.SelectedTabPage.Name.ToString, otbmarkcutting.SelectedTabPage.Name.ToString)

                Try
                    otbtable.SelectedTabPageIndex = (otbtable.TabPages.Count - 1)
                Catch ex As Exception
                End Try

            End With

        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Table Cutting", 1404218711, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

        End If
    End Sub

    Private Sub otbjobprod_ForeColorChanged(sender As Object, e As EventArgs) Handles otbjobprod.ForeColorChanged

    End Sub
End Class
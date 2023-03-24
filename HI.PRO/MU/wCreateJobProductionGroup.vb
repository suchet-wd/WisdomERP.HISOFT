Imports System.Drawing

Public Class wCreateJobProductionGroup

    Private _GenJobProd As wGenerateJobProd
    Private _ListDataMarkBreakDown As List(Of DataTable)
    Private _ListPurchaseOrder As New List(Of DataTable)
    Private _ListDataSubOrder As New List(Of DataTable)
    Private _MapPurchaseOrderRawmat As wColorWayListPORawMat
    Private _CopyTable As wCopyTableCut
    Private _FindTable As wFindTableCut
    Private _StateSubNew As Boolean = False
    Private _TFNMarkSpare As Double = 2.0
    Private _adjunitsect As wAdjUnitSectToTable

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


        _adjunitsect = New wAdjUnitSectToTable
        HI.TL.HandlerControl.AddHandlerObj(_adjunitsect)


        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _adjunitsect.Name.ToString.Trim, _adjunitsect)
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
        'ocmaddsuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ' ocmdeletesuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmsavemainmark.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ocmsavesubmark.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)
        ' ocmdeleteallsuborder.Visible = (otbdetail.SelectedTabPage.Name = otporderproddetail.Name)

        ocmaddtable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmdeletetable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmsavetable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmdeletealltable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmmappomatcolor.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)

        ocmpreviewbytable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmpreviewalltable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmpreviewallprod.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        'ocmcopy.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)
        ocmfindtable.Visible = (otbdetail.SelectedTabPage.Name = otporderprodcuttingdetail.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub InitGridBreakdown()

        'With AdvBandedGridView1
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsMenu.EnableColumnMenu = False
        '    .OptionsMenu.ShowAutoFilterRowItem = False
        '    .OptionsFilter.AllowFilterEditor = False
        '    .OptionsFilter.AllowColumnMRUFilterList = False
        '    .OptionsFilter.AllowMRUFilterList = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        'End With

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
        Me.FTDocumentNo.Text = Key.ToString
    End Sub

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)
        If (_StateSubNew) Then Exit Sub
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        Call ClearGrid()
        otbjobprod.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = "Select     c.FTCustCode , sum(a.FNQuantity) as FNQuantity   "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODMUGroupPlan a with(nolock)"
        _Qry &= vbCrLf & " left join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder o with(nolock) on a.FTOrderNo = o.FTOrderNo"
        _Qry &= vbCrLf & " left join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer c with(nolock) on o.FNHSysCustId = c.FNHSysCustId "
        _Qry &= vbCrLf & "  where FTDocumentNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
        _Qry &= vbCrLf & "   group by c.FTCustCode"
        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        If _dtprod.Rows.Count <= 0 Then

            Me.FNHSysCustId.Text = ""
            Me.FNGrandQty.Value = 0

            _Spls.Close()
            Exit Sub
        End If

        Me.FNHSysCustId.Text = _dtprod.Rows(0).Item("FTCustCode").ToString
        Me.FNGrandQty.Value = _dtprod.Rows(0).Item("FNQuantity")



        _Qry = "SELECT distinct    FTOrderProdNo ,LEN(FTOrderProdNo)  "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd AS P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
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
        Else
            'If CreateNewJobProducttion() Then
            'LoadOrderProdDataInfo(Me.FTDocumentNo.Text)
            'End If
            '_Qry = "Select top 1     FTDocumentNo    from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODMUGroupPlan with(nolock)"
            '_Qry &= vbCrLf & " where FTDocumentNo='" & Me.FTDocumentNo.Text & "'"
            'If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0 Then

            'End If


        End If

        _dtprod.Dispose()

        Call InitDataPurchaseOrder(Key)

        _Spls.Close()
    End Sub

    'Private Sub InitGridRatio(_dt As DataTable, _dt2 As DataTable, ogv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView, ogc As DevExpress.XtraGrid.GridControl)
    '    Try

    '        Dim _Qry As String = ""
    '        Dim _colcount As Integer = 0

    '        With ogv

    '            For I As Integer = .Columns.Count - 1 To 0 Step -1

    '                Select Case .Columns(I).FieldName.ToString.ToUpper

    '                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
    '                     "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "FNYard".ToUpper, "FNInc".ToUpper, "FNQuantityUse".ToUpper, "FNActuallong".ToUpper


    '                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                    Case Else
    '                        .Columns.Remove(.Columns(I))
    '                End Select

    '            Next


    '            'Try
    '            '    For I As Integer = .Bands.Count - 1 To 0 Step -1

    '            '        Select Case .Bands(I).Name.ToUpper

    '            '            Case "gBMark".ToUpper, "gBTotal".ToUpper, "gbyard".ToUpper
    '            '            Case Else
    '            '                .Bands.Remove(.Bands(I))



    '            '        End Select

    '            '    Next
    '            'Catch ex As Exception

    '            'End Try


    '            If Not (_dt Is Nothing) Then
    '                Dim _StyleCodeOld As String = ""
    '                Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
    '                Dim colwith As Integer = 0
    '                For Each Col As DataColumn In _dt.Columns

    '                    Select Case Col.ColumnName.ToString.ToUpper

    '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
    '                                    "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
    '                                 "FNHSysStyleId_Hide".ToUpper, "FNYard".ToUpper, "FNInc".ToUpper, "FNQuantityUse".ToUpper, "FNActuallong".ToUpper


    '                        Case Else
    '                            Dim ColG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

    '                            Dim _StyleCode As String = ""
    '                            Dim _SizeBreakDown As String = ""
    '                            _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
    '                            _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))

    '                            If Not (_StyleCodeOld = _StyleCode) Then
    '                                colwith = 0
    '                                ColBand = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
    '                                With ColBand
    '                                    .Visible = True

    '                                    .AppearanceHeader.Options.UseTextOptions = True
    '                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                                    .Caption = _StyleCode

    '                                    .RowCount = 1

    '                                    .Name = "GridBand1" + _StyleCode
    '                                    .VisibleIndex = 1
    '                                    .Width = _dt2.Select("FTStyleCode='" & _StyleCode & "'").Length * 45

    '                                End With

    '                                .Bands.Add(ColBand)
    '                            End If


    '                            _colcount = _colcount + 1
    '                            colwith += +1
    '                            ColG = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    '                            With ColG
    '                                .Visible = True
    '                                .FieldName = Col.ColumnName.ToString
    '                                .Name = Col.ColumnName.ToString
    '                                .Caption = _SizeBreakDown
    '                                .Width = 45
    '                            End With
    '                            'ColBand.Columns.Add(ColG)
    '                            .Columns.Add(ColG)

    '                            With .Columns(Col.ColumnName.ToString)

    '                                .OptionsFilter.AllowAutoFilter = False
    '                                .OptionsFilter.AllowFilter = False
    '                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                                .DisplayFormat.FormatString = "{0:n0}"
    '                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far


    '                                With .OptionsColumn
    '                                    .AllowMove = False
    '                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                    .AllowEdit = False
    '                                    .ReadOnly = True
    '                                End With

    '                            End With


    '                            _StyleCodeOld = _StyleCode
    '                    End Select
    '                Next
    '                ' Next
    '            End If
    '            Try
    '                For I As Integer = .Bands.Count - 1 To 0 Step -1

    '                    Select Case .Bands(I).Name.ToUpper

    '                        Case "gBMark".ToUpper, "gBTotal".ToUpper
    '                        Case Else


    '                            For Each Col As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In .Columns
    '                                Dim _name As String = Replace(.Bands(I).Name, "GridBand1", "")

    '                                If Microsoft.VisualBasic.Left(Col.Name.ToString, Len(_name)) = _name Then
    '                                    Select Case Col.FieldName.ToUpper

    '                                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
    '                                        "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
    '                                     "FNHSysStyleId_Hide".ToUpper, "FNYard".ToUpper, "FNInc".ToUpper, "FNQuantityUse".ToUpper, "FNActuallong".ToUpper
    '                                        Case Else

    '                                            .Bands(I).Columns.Add(Col)

    '                                    End Select
    '                                End If
    '                            Next
    '                    End Select
    '                Next
    '            Catch ex As Exception
    '            End Try

    '        End With


    '        AddHandler ogv.RowCellStyle, AddressOf AdvBandedGridView2_RowCellStyle
    '        ogc.DataSource = _dt.Copy


    '    Catch ex As Exception
    '        MsgBox("N" & ex.ToString)
    '    End Try
    'End Sub

    Private Sub LoadOrderProdBreakDown(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _ds As DataSet
        Dim _dt2 As DataTable
        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown_MU '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _ds = New DataSet


        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)
        _dt = _ds.Tables(0)
        _dt2 = _ds.Tables(1)



        With Me.AdvBandedGridView1

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next




            Try
                For I As Integer = .Bands.Count - 1 To 0 Step -1

                    Select Case .Bands(I).Name.ToUpper

                        Case "gBInfo".ToUpper, "gbTotal".ToUpper
                        Case Else
                            .Bands.Remove(.Bands(I))



                    End Select

                Next
            Catch ex As Exception

            End Try




            If Not (_dt Is Nothing) Then
                Dim _StyleCodeOld As String = ""
                Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                Dim colwith As Integer = 0
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper

                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper

                        Case Else
                            Dim ColG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

                            Dim _StyleCode As String = ""
                            Dim _SizeBreakDown As String = ""
                            Try
                                _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
                                _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))
                            Catch ex As Exception

                            End Try


                            If Not (_StyleCodeOld = _StyleCode) Then
                                colwith = 0
                                ColBand = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                With ColBand
                                    .Visible = True

                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = _StyleCode

                                    .RowCount = 1

                                    .Name = "GridBand1" + _StyleCode
                                    .VisibleIndex = 1
                                    .Width = _dt2.Select("FTStyleCode='" & _StyleCode & "'").Length * 20

                                End With

                                .Bands.Add(ColBand)
                            End If


                            _colcount = _colcount + 1
                            colwith += +1
                            ColG = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                            Try
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString
                                    .Caption = _SizeBreakDown
                                    .Width = 20

                                End With
                                'ColBand.Columns.Add(ColG)
                                ColBand.Columns.Add(ColG)

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
                            Catch ex As Exception

                            End Try



                            _StyleCodeOld = _StyleCode
                    End Select
                Next
                ' Next
            End If






            'If Not (_dt Is Nothing) Then
            '    For Each Col As DataColumn In _dt.Columns

            '        Select Case Col.ColumnName.ToString.ToUpper
            '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
            '            Case Else
            '                _colcount = _colcount + 1
            '                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
            '                With ColG
            '                    .Visible = True
            '                    .FieldName = Col.ColumnName.ToString
            '                    .Name = "FTSubOrderNo" & Col.ColumnName.ToString
            '                    .Caption = Col.ColumnName.ToString

            '                End With

            '                .Columns.Add(ColG)

            '                With .Columns(Col.ColumnName.ToString)

            '                    .OptionsFilter.AllowAutoFilter = False
            '                    .OptionsFilter.AllowFilter = False
            '                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '                    .DisplayFormat.FormatString = "{0:n0}"
            '                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

            '                    With .OptionsColumn
            '                        .AllowMove = False
            '                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
            '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
            '                        .AllowEdit = False
            '                        .ReadOnly = True
            '                    End With

            '                End With

            '                .Columns(Col.ColumnName.ToString).Width = 45
            '                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
            '                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

            '        End Select

            '    Next

            'End If

            'If _colcount > 4 Then

            'End If

        End With


        Try

            Me.ogcjobprod.DataSource = _dt.Copy
            Me.AdvBandedGridView1.BestFitColumns()

        Catch ex As Exception

        End Try

        '_colcount = 0
        'With Me.ogvjobprodbal

        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next

        '    If Not (_dt Is Nothing) Then
        '        For Each Col As DataColumn In _dt.Columns

        '            Select Case Col.ColumnName.ToString.ToUpper
        '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                Case Else
        '                    _colcount = _colcount + 1
        '                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
        '                    With ColG
        '                        .Visible = True
        '                        .FieldName = Col.ColumnName.ToString
        '                        .Name = "FTSubOrderNo" & Col.ColumnName.ToString
        '                        .Caption = Col.ColumnName.ToString

        '                    End With

        '                    .Columns.Add(ColG)

        '                    With .Columns(Col.ColumnName.ToString)

        '                        .OptionsFilter.AllowAutoFilter = False
        '                        .OptionsFilter.AllowFilter = False
        '                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '                        .DisplayFormat.FormatString = "{0:n0}"
        '                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        '                        With .OptionsColumn
        '                            .AllowMove = False
        '                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
        '                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
        '                            .AllowEdit = False
        '                            .ReadOnly = True
        '                        End With

        '                    End With

        '                    .Columns(Col.ColumnName.ToString).Width = 45
        '                    .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
        '                    .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

        '            End Select

        '        Next

        '    End If

        '    'If _colcount > 4 Then
        '    '    .BestFitColumns()
        '    'End If

        'End With

        'Me.ogcjobprodbal.DataSource = _dt.Copy

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

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdSubBreakDown_MU '" & HI.UL.ULF.rpQuoted(OrderProdNo.ToString) & "','" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "' "
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
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail AS P With(Nolock)"
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

        'With AdvBandedGridView1
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

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

        With Advmark
            .OptionsView.ShowAutoFilterRow = False
            '.OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect 'DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        'With ogvjobprodbal
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        'With ogvbalance
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

    End Sub

    Private Sub ClearGrid(Optional Prod As Boolean = False)

        With Me.AdvBandedGridView1
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
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

        With Me.AdvBandedGridView2
            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "Total".ToUpper ', "FTColorway".ToUpper
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

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain AS P With(Nolock) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M   With(Nolock) ON P.FNHSysMarkId = M.FNHSysMarkId"
        _Qry &= vbCrLf & " WHERE        (P.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "')"
        _Qry &= vbCrLf & " Order By M.FTMarkCode ASC "






        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        If _dt.Rows.Count <= 0 Then
            _Qry = " SELECT      '" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' FTOrderProdNo,  M.FNHSysMarkId AS FNHSysMarkId_Hide, M.FTMarkCode AS FNHSysMarkId, p.FTRemark FTNote"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , M.FTMarkNameTH AS FNHSysMarkId_None"
            Else
                _Qry &= vbCrLf & " ,  M.FTMarkNameEN AS FNHSysMarkId_None "
            End If

            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMURatio AS P With(Nolock) "
            _Qry &= vbCrLf & "      left join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M   With(Nolock) ON P.FTPartCode = M.FTMarkCode"
            _Qry &= vbCrLf & " WHERE        (P.FTDocumentNo = N'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text().ToString) & "')"
            _Qry &= vbCrLf & " Order By M.FTMarkCode ASC "
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        End If

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

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub AS P With(Nolock) INNER JOIN"
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

            _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In .Rows
                If Val((R!FNHSysMarkId_Hide.ToString)) > 0 Then
                    _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(R!FNHSysMarkId_Hide.ToString) & "  "

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain"
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

            _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
            _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.ogvmainmark.GetFocusedRowCellValue("FNHSysMarkId_Hide").ToString) & "  "

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In .Rows

                If Val(R!FNHSysMarkId_Hide.ToString) > 0 Then
                    _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'  "
                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(R!FNHSysMainMarkId.ToString) & "  "
                    _Qry &= vbCrLf & " AND FNHSysSubMarkId=" & Val(R!FNHSysMarkId_Hide.ToString) & "  "

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub"
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
        Dim _dt2 As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Me.ogcbalance.DataSource = Nothing
        Me.ogcjobprodbal.DataSource = Nothing

        Dim _ds As DataSet
        _ListDataMarkBreakDown = New List(Of DataTable)
        _ListDataMarkBreakDown.Clear()

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown_MU_ProdBD '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _ds = New DataSet


        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)
        _dt = _ds.Tables(0)
        _dt2 = _ds.Tables(1)



        _ListDataMarkBreakDown.Add(_dt.Copy)

        With Me.AdvBandedGridView2

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "Total".ToUpper ', "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next



            Try
                For I As Integer = .Bands.Count - 1 To 0 Step -1

                    Select Case .Bands(I).Name.ToUpper

                        Case "gb1Total".ToUpper
                        Case Else
                            .Bands.Remove(.Bands(I))



                    End Select

                Next
            Catch ex As Exception

            End Try



            If Not (_dt Is Nothing) Then
                Dim _StyleCodeOld As String = ""
                Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                Dim colwith As Integer = 0
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper

                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper

                        Case Else
                            Dim ColG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

                            Dim _StyleCode As String = ""
                            Dim _SizeBreakDown As String = ""
                            Try
                                _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
                                _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))
                            Catch ex As Exception

                            End Try


                            If Not (_StyleCodeOld = _StyleCode) Then
                                colwith = 0
                                ColBand = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                With ColBand
                                    .Visible = True

                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = _StyleCode

                                    .RowCount = 1

                                    .Name = "GridBand2" + _StyleCode
                                    .VisibleIndex = 1
                                    .Width = _dt2.Select("FTStyleCode='" & _StyleCode & "'").Length * 20

                                End With

                                .Bands.Add(ColBand)
                            End If


                            _colcount = _colcount + 1
                            colwith += +1
                            ColG = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                            Try
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString
                                    .Caption = _SizeBreakDown
                                    .Width = 20

                                End With
                                'ColBand.Columns.Add(ColG)
                                ColBand.Columns.Add(ColG)

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
                            Catch ex As Exception

                            End Try



                            _StyleCodeOld = _StyleCode
                    End Select
                Next
                ' Next
            End If


            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcjobprodbal.DataSource = _dt.Copy

        With Me.AdvBandedGridView3

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "Total".ToUpper ', "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next



            Try
                For I As Integer = .Bands.Count - 1 To 0 Step -1

                    Select Case .Bands(I).Name.ToUpper

                        Case "gb2Total".ToUpper
                        Case Else
                            .Bands.Remove(.Bands(I))

                    End Select

                Next
            Catch ex As Exception

            End Try


            If Not (_dt Is Nothing) Then
                Dim _StyleCodeOld As String = ""
                Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                Dim colwith As Integer = 0
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper

                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper

                        Case Else
                            Dim ColG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

                            Dim _StyleCode As String = ""
                            Dim _SizeBreakDown As String = ""
                            Try
                                _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
                                _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))
                            Catch ex As Exception

                            End Try


                            If Not (_StyleCodeOld = _StyleCode) Then
                                colwith = 0
                                ColBand = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                With ColBand
                                    .Visible = True

                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = _StyleCode

                                    .RowCount = 1

                                    .Name = "GridBand3" + _StyleCode
                                    .VisibleIndex = 1
                                    .Width = _dt2.Select("FTStyleCode='" & _StyleCode & "'").Length * 20

                                End With

                                .Bands.Add(ColBand)
                            End If


                            _colcount = _colcount + 1
                            colwith += +1
                            ColG = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                            Try
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString
                                    .Caption = _SizeBreakDown
                                    .Width = 20

                                End With
                                'ColBand.Columns.Add(ColG)
                                ColBand.Columns.Add(ColG)

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
                            Catch ex As Exception

                            End Try



                            _StyleCodeOld = _StyleCode
                    End Select
                Next
                ' Next
            End If



            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcbalance.DataSource = _dt.Copy
        Me.AdvBandedGridView2.BestFitColumns()
        Me.AdvBandedGridView3.BestFitColumns()
        Dim M3 As New DataTable

        M3 = _dt.Copy
        Dim Ridx As Integer = 0

        M3.Columns.Remove("Total")
        M3.Columns.Add("FNAssort", GetType(String))
        Dim R As DataRow = M3.NewRow
        R!FNAssort = "Layer\Assort"
        M3.Rows.InsertAt(R, 0)
        Ridx = 0
        For Each Rx As DataRow In M3.Rows

            If Ridx > 0 Then
                For Each Col As DataColumn In M3.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNAssort".ToUpper
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
        FNInterstitial.Value = 0

        _Qry = " Select TOp 1 A.FTNote, A.FNHSysUnitSectId,ISNULL(B.FTUnitSectCode,'') AS FTUnitSectCode,ISNULL(A.FTStateRepair,'') AS FTStateRepair "
        _Qry &= vbCrLf & "  , A.FNFabricFrontSize, A.FNMarkYRD,A.FNMarkINC, A.FNMarkSpare, A.FNMarkTotal,A.FTRef,A.FTStateBundle,A.FTStateCutchange,A.FTStatTableScrap , isnull( a.FNInterstitial,0) as FNInterstitial"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut AS A WITH(NOLOCK)  "
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
            FNInterstitial.Value = Val(R!FNInterstitial.ToString)

            FTRef.Text = R!FTRef.ToString
            FTStateBundle.Checked = (R!FTStateBundle.ToString = "1")
            FTStateCutchange.Checked = (R!FTStateCutchange.ToString = "1")
            FTStatTableScrap.Checked = (R!FTStatTableScrap.ToString = "1")
            Exit For
        Next

        _Qry = "SELECT       FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut_Detail AS D WITH(Nolock)"

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
                For Each Rx As DataRow In .Rows  ' .Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNAssort".ToUpper
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
                For Each Rx As DataRow In .Rows  ' .Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNAssort".ToUpper
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
            For Each Rx As DataRow In _dt.Rows ' .Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'") Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")
                Try

                    _dt.Rows(0).Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val(R!FNAssort.ToString))
                    _dt.Rows(1).Item("FNAssort") = Integer.Parse(Val((R!FNLayer.ToString)))
                    Rx.Item(R!FTSizeBreakDown.ToString) = Integer.Parse(Val((R!FNQuantity.ToString)))



                Catch ex As Exception
                End Try
            Next
        Next
        '------End Set ยอด Table Cutting

        _dtctd.Dispose()
        Dim _colcount As Integer = 0

        With Me.Advmark

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FNAssort".ToUpper  ', "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            'If Not (_dt Is Nothing) Then
            '    For Each Col As DataColumn In _dt.Columns



            '        Select Case Col.ColumnName.ToString.ToUpper
            '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper
            '            Case Else
            '                _colcount = _colcount + 1
            '                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
            '                With ColG
            '                    .Visible = True
            '                    .FieldName = Col.ColumnName.ToString
            '                    .Name = "FTSubOrderNo" & Col.ColumnName.ToString
            '                    .Caption = Col.ColumnName.ToString


            '                    If Not (Col.ColumnName.ToString = "Total") Then
            '                        .ColumnEdit = ReposFNAssort
            '                    End If

            '                End With

            '                .Columns.Add(ColG)

            '                With .Columns(Col.ColumnName.ToString)

            '                    .OptionsFilter.AllowAutoFilter = False
            '                    .OptionsFilter.AllowFilter = False
            '                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '                    .DisplayFormat.FormatString = "{0:n0}"
            '                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

            '                    With .OptionsColumn
            '                        .AllowMove = False
            '                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
            '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
            '                        .AllowEdit = True
            '                        .ReadOnly = False
            '                    End With

            '                End With
            '                .Columns(Col.ColumnName.ToString).Width = 45
            '                '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
            '                '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

            '        End Select




            '    Next





            'End If





            Try
                For I As Integer = .Bands.Count - 1 To 0 Step -1

                    Select Case .Bands(I).Name.ToUpper

                        Case "gB4Info".ToUpper, "gb4Total".ToUpper
                        Case Else
                            .Bands.Remove(.Bands(I))



                    End Select

                Next
            Catch ex As Exception

            End Try




            If Not (_dt Is Nothing) Then
                Dim _StyleCodeOld As String = ""
                Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                Dim colwith As Integer = 0
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper

                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper

                        Case Else
                            Dim ColG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

                            Dim _StyleCode As String = ""
                            Dim _SizeBreakDown As String = ""
                            Try
                                _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
                                _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))
                            Catch ex As Exception

                            End Try


                            If Not (_StyleCodeOld = _StyleCode) Then
                                colwith = 0
                                ColBand = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                With ColBand
                                    .Visible = True

                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = _StyleCode

                                    .RowCount = 1

                                    .Name = "GridBand4" + _StyleCode
                                    .VisibleIndex = 1
                                    .Width = 100

                                End With

                                .Bands.Add(ColBand)
                            End If


                            _colcount = _colcount + 1
                            colwith += +1
                            ColG = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                            Try
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString
                                    .Caption = _SizeBreakDown
                                    .Width = 20
                                    .ColumnEdit = ReposFNAssort

                                End With
                                'ColBand.Columns.Add(ColG)
                                ColBand.Columns.Add(ColG)

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
                            Catch ex As Exception

                            End Try



                            _StyleCodeOld = _StyleCode
                    End Select
                Next
                ' Next
            End If



            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcmark.DataSource = _dt.Copy
        Me.Advmark.BestFitColumns()
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
        _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut AS XXA WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE  (XXA.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' ) "
        _Qry &= vbCrLf & " AND (XXA.FNHSysMarkId = A.FNHSysMarkId)"

        _Qry &= vbCrLf & "  ),0) AS FNTableCount"

        _Qry &= vbCrLf & "    FROM"
        _Qry &= vbCrLf & "  (SELECT     1 AS FNSeq, FTOrderProdNo, FNHSysMarkId"
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT     2 AS FNSeq, FTOrderProdNo, FNHSysSubMarkId AS FNHSysMarkId"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub  WITH(NOLOCK)   "
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
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut  WITH(NOLOCK)  "
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

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            Try

                _Qry = " DELETE FROM A	 "
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                _Qry &= vbCrLf & "     LEFT OUTER JOIN "
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & "   SELECT FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail"
                _Qry &= vbCrLf & "   WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "')"
                _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "    WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' AND B.FTOrderNo Is NULL"

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



                _Qry = " Delete d FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail d "
                _Qry &= vbCrLf & "   LEFT JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd p On d.FTOrderProdNo = p.FTOrderProdNo   "
                _Qry &= vbCrLf & " WHERE p.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
                _Qry &= vbCrLf & " and  p.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _Qry = " Delete D FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain d "
                _Qry &= vbCrLf & "  LEFT JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd p On d.FTOrderProdNo = p.FTOrderProdNo   "
                _Qry &= vbCrLf & " WHERE p.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
                _Qry &= vbCrLf & " and  p.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "


                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _Qry = " Delete d  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub d "
                _Qry &= vbCrLf & "  LEFT JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd p On d.FTOrderProdNo = p.FTOrderProdNo   "
                _Qry &= vbCrLf & " WHERE p.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
                _Qry &= vbCrLf & " and  p.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "



                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If






                _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd    "
                _Qry &= vbCrLf & " where  FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then



                End If


            Catch ex As Exception
            End Try

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd  WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' ")

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

            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut "
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

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

            If Not (StateAllTable) Then
                _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If


            _Qry = " DELETE Z  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd p  "
            _Qry &= vbCrLf & "  Left Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd mp with(nolock) ON p.FTDocumentNo = mp.FTOrderNo"
            _Qry &= vbCrLf & "  Left Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_TableCut_Detail x on p.FTOrderProdNo = x.FTOrderProdNo    "
            _Qry &= vbCrLf & " Left Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_TableCut z on x.FNTableNo = z.FNTableNo And x.FTOrderProdNo  = z.FTOrderProdNo "
            _Qry &= vbCrLf & "  And x.FNHSysMarkId = z.FNHSysMarkId  "
            _Qry &= vbCrLf & " WHERE mp.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  x.FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            If Not (StateAllTable) Then
                _Qry &= vbCrLf & "   And x.FNTableNoRef =" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            End If
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If


            _Qry = " DELETE X  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd p  "
            _Qry &= vbCrLf & "  Left Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd mp with(nolock) ON p.FTDocumentNo = mp.FTOrderNo"
            _Qry &= vbCrLf & "  Left Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_TableCut_Detail x on p.FTOrderProdNo = x.FTOrderProdNo    "
            _Qry &= vbCrLf & " Left Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_TableCut z on x.FNTableNo = z.FNTableNo And x.FTOrderProdNo  = z.FTOrderProdNo "
            _Qry &= vbCrLf & "  And x.FNHSysMarkId = z.FNHSysMarkId  "
            _Qry &= vbCrLf & " WHERE mp.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  x.FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            If Not (StateAllTable) Then
                _Qry &= vbCrLf & "   And x.FNTableNoRef =" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            End If
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If




            '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut_PO_Rawmat "
            '_Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            '_Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

            'If Not (StateAllTable) Then
            '    _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
            'End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut "
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

            _Qry = " Select TOp 1 FTOrderProdNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut "
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId"
                _Qry &= vbCrLf & ", FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef,FTStateBundle,FTStateCutchange,FTStatTableScrap,FNInterstitial) "
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
                _Qry &= vbCrLf & "," & FNInterstitial.Value & ""
            Else

                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut "
                _Qry &= vbCrLf & " SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,FTNote='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                _Qry &= vbCrLf & " ,FNHSysUnitSectId=" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " ,FTStateRepair='" & FTStateRepair.EditValue.ToString & "'"
                _Qry &= vbCrLf & ",FNFabricFrontSize=" & FNFabricFrontSize.Value & " "
                _Qry &= vbCrLf & ",FNMarkYRD=" & FNMarkYRD.Value & ""
                _Qry &= vbCrLf & ",FNMarkINC=" & FNMarkINC.Value & ""
                _Qry &= vbCrLf & ",FNMarkSpare=" & FNMarkSpare.Value & ""
                _Qry &= vbCrLf & ",FNMarkTotal=" & FNMarkTotal.Value & ""
                _Qry &= vbCrLf & ", FNInterstitial=" & FNInterstitial.Value & ""
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
            Dim _FNTableNoRef As Integer = 0


            _Qry = " select Top 1   FNTableNoRef  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "


            _FNTableNoRef = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND  FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
            _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            With CType(ogcmark.DataSource, DataTable)
                For Each R As DataRow In .Rows

                    For Each Col As DataColumn In .Columns
                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper
                            Case Else
                                If IsNumeric(R.Item(Col.ColumnName.ToString)) Then

                                    If Integer.Parse(R.Item(Col.ColumnName.ToString)) > 0 AndAlso Integer.Parse(Val(R!FNAssort.ToString)) > 0 Then 'AndAlso R!FTColorway.ToString <> ""

                                        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODMUTOrderProd_TableCut_Detail "
                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway"
                                        _Qry &= vbCrLf & ", FTSizeBreakDown, FNLayer, FNAssort, FNQuantity,FNHSysCmpId   ) "
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                                        _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
                                        _Qry &= vbCrLf & " ,'' "
                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNAssort.ToString)) & " "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(.Rows(0).Item(Col.ColumnName.ToString))) & " "
                                        _Qry &= vbCrLf & " ," & Integer.Parse(R.Item(Col.ColumnName.ToString)) & " "
                                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "


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
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut WITH (NOLOCK) "
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

            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail "
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
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderStatus AS A"
                _Qry &= vbCrLf & "     LEFT OUTER JOIN "
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & "   SELECT FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail"
                _Qry &= vbCrLf & "   WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "')"
                _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "    WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' AND B.FTOrderNo Is NULL"

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

        _Qry = " Select TOP 1  A.FTLayCutNo"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " left join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd as P with(nolock) on A.FTOrderProdNo = P.FTOrderProdNo"
        _Qry &= vbCrLf & " left join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd as PM with(nolock) on P.FTDocumentNo = PM.FTDocumentNo"


        _Qry &= vbCrLf & " WHERE (PM.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "')"
        '_Qry &= vbCrLf & " AND (FNHSysMarkId =" & Integer.Parse(Val(otbmarkcutting.SelectedTabPage.Name.ToString)) & ")"

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
        Me.AdvBandedGridView1.OptionsView.ShowAutoFilterRow = False
        Me.AdvBandedGridView2.OptionsView.ShowAutoFilterRow = False
        Me.AdvBandedGridView3.OptionsView.ShowAutoFilterRow = False
        ' HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysCmpId, New EventArgs)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Call ClearGrid()
        FTDocumentNo.Focus()
        FTDocumentNo.SelectAll()
        otbdetail.SelectedTabPageIndex = 0
        FNFabricFrontSize.Value = 0
        FNMarkYRD.Value = 0
        FNMarkINC.Value = 0
        FNMarkSpare.Value = _TFNMarkSpare
        FNMarkTotal.Value = 0
    End Sub

    Private Sub ocmgeneratejobprod_Click(sender As Object, e As EventArgs) Handles ocmgeneratejobprod.Click
        If Me.FTDocumentNo.Text <> "" And FTDocumentNo.Properties.Tag.ToString <> "" Then

            If CreateNewJobProducttion() Then
                LoadOrderProdDataInfo(Me.FTDocumentNo.Text)
            End If
            Me.otbdetail.SelectedTabPageIndex = 0

            Try
                otbjobprod.SelectedTabPageIndex = (otbjobprod.TabPages.Count - 1)
            Catch ex As Exception

            End Try
            'With _GenJobProd
            '    .OrderNo = Me.FTDocumentNo.Text
            '    .JobProdNo = ""
            '    Call HI.ST.Lang.SP_SETxLanguage(_GenJobProd)
            '    .FNCreateJobProdType.Visible = True
            '    .ShowDialog()

            '    If (.Process) Then
            '        Call LoadOrderProdDataInfo(Me.FTDocumentNo.Text)
            '        Me.otbdetail.SelectedTabPageIndex = 0

            '        Try
            '            otbjobprod.SelectedTabPageIndex = (otbjobprod.TabPages.Count - 1)
            '        Catch ex As Exception

            '        End Try
            '    End If

            'End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTDocumentNo_lbl.Text)
            FTDocumentNo.Focus()
            FTDocumentNo.SelectAll()
        End If
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTDocumentNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            If FTDocumentNo.Text = "" Then
                Exit Sub
            End If
            Call LoadOrderProdDataInfo(FTDocumentNo.Text)
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

    Private Sub ocmaddsuborder_Click(sender As Object, e As EventArgs)

        'If ValidateCreateTableCut() Then
        '    HI.MG.ShowMsg.mInfo("พบข้อมูลการออกใบสั่งปูตัดแล้วไม่สามารถทำการลบได้ !!!", 1404180077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    Exit Sub
        'End If
        Call SavSubMark()

        If Me.FTDocumentNo.Text <> "" And FTDocumentNo.Properties.Tag.ToString <> "" Then
            If Not (otbsuborder.SelectedTabPage Is Nothing) Then
                With _GenJobProd
                    .OrderNo = Me.FTDocumentNo.Text
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
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTDocumentNo_lbl.Text)
            FTDocumentNo.Focus()
            FTDocumentNo.SelectAll()
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


#Region "Function"



    Private Function CreateNewJobProducttion() As Boolean



        'Dim _Spls As New HI.TL.SplashScreen("Generating Job Production...Please Wait")
        Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _dtjobprod As DataTable
        Dim _tmpOrderProd As String = ""
        Dim _dt As DataTable
        Dim _groupNo As String = ""
        _Qry = "Select top 1  FTGroupNo  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan with(nolock)  "
        _Qry &= vbCrLf & " where  FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
        _groupNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_MU_OrderBreakDown  @FTGroupNo ='" & HI.UL.ULF.rpQuoted(_groupNo) & "' "
        _Qry &= vbCrLf & " , @FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)




        Dim I As Integer = 0
        Try

            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _dt.Rows


                _OrderNo = R!FTOrderNo.ToString
                _SubOrderNo = R!FTSubOrderNo.ToString
                _ColorWay = R!FTColorWay.ToString

                For Each Col As DataColumn In _dt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNHSysStyleId".ToUpper, "FTCustomerPO".ToUpper, "FTPOLine".ToUpper,
                               "FNHSysStyleId_Hide".ToUpper
                        Case Else

                            If (Double.Parse("0" & R.Item(Col.ColumnName.ToString))) > 0 Then
                                _Qry = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd "
                                _Qry &= vbCrLf & " ( FTUserLogIn, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                _Qry &= vbCrLf & "," & Double.Parse(R.Item(Col.ColumnName.ToString)) & " "

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                    HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
                                    '_Spls.Close()
                                    Return False

                                End If

                            End If

                    End Select
                Next


                I = I + 1
            Next





            _tmpOrderProd = ""

            _Qry = "   SELECT  TOP 1 FTOrderProdNo"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd"
            _Qry &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' AND LEN(FTOrderProdNo) = LEN('" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text & "-P001") & "') "
            _Qry &= vbCrLf & "  ORDER BY FTOrderProdNo DESC "
            _tmpOrderProd = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

            If _tmpOrderProd = "" Then
                _tmpOrderProd = HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "-P001"
            Else
                _tmpOrderProd = HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "-P" & Microsoft.VisualBasic.Right("0000" & Format(Val(Microsoft.VisualBasic.Right(_tmpOrderProd, 3)) + 1, "0"), 3)
            End If



            'For Each M As DataRow In _dtmain.Select("FTColorWay='" & R!FTColorway.ToString & "'")
            _Qry = ""
            _Qry &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd "
            _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo,FNHSysCmpId,FTDocumentNo)"
            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "


            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail "
            _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTOrderProdNo, FTSubOrderNo,FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCmpId,FTDocumentNo)"
            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & ",T.FTOrderNo " '& HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "' "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
            _Qry &= vbCrLf & ",T.FTSubOrderNo"
            _Qry &= vbCrLf & ",T.FTColorway"
            _Qry &= vbCrLf & ",T.FTSizeBreakDown"
            _Qry &= vbCrLf & ",T.FNQuantity"
            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "

            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
            _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Qry &= vbCrLf & " AND T.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
            '_Qry &= vbCrLf & " AND T.FTOrderNo='" & HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            'Next


            'Next






            Try

                _Qry = "Select  distinct  FTOrderNo from TPRODMUGroupPlan  "
                _Qry &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(_groupNo) & "' "
                _Qry &= vbCrLf & " and  FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "

                For Each M As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows

                    _Qry = "   UPDATE A SET FTStateCut ='1'	 "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                    _Qry &= vbCrLf & "      INNER Join"
                    _Qry &= vbCrLf & "  ("
                    _Qry &= vbCrLf & "   SELECT FTOrderNo, FTSubOrderNo"
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail"
                    _Qry &= vbCrLf & "   WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "')"
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
                    _Qry &= vbCrLf & "        FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail"
                    _Qry &= vbCrLf & "       WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "')"

                    _Qry &= vbCrLf & "       GROUP BY FTOrderNo, FTSubOrderNo"
                    _Qry &= vbCrLf & "       ) AS B ON A.FTOrderNo = B.FTOrderNo"
                    _Qry &= vbCrLf & "        AND A.FTSubOrderNo = B.FTSubOrderNo"
                    _Qry &= vbCrLf & "        WHERE A.FTOrderNo Is NULL"
                    _Qry &= vbCrLf & "    END "

                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                Next


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
            '_Spls.Close()
            Return False
        End Try
        HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
        '_Spls.Close()
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

            _tmpOrderProd = "" 'Me.JobProdNo

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

    Private Sub ogvmark_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Advmark.KeyDown
        Try
            With Advmark
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


    Private Sub ogvmark_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles Advmark.RowCellStyle
        With Advmark
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

    Private Sub ogvmark_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles Advmark.ShowingEditor
        With Advmark
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
                With Me.Advmark

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
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNAssort".ToUpper
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
                        Call LoadOrderProdDataInfo(Me.FTDocumentNo.Text)
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

            _Qry = " Select TOp 1 FNTableNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut "
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

    Private Sub ocmdeletesuborder_Click(sender As Object, e As EventArgs)
        If Not (Me.otbsuborder.SelectedTabPage Is Nothing) Then

            If ValidateCreateTableCut() = False Then

                If Me.DeleteSubOrder(False) Then

                    Call LoadOrderProdDataInfo(FTDocumentNo.Text)
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

    Private Sub ocmdeleteallsuborder_Click(sender As Object, e As EventArgs)
        If Not (Me.otbsuborder.SelectedTabPage Is Nothing) Then

            If ValidateCreateTableCut() = False Then

                If Me.DeleteSubOrder(True) Then
                    Call LoadOrderProdDataInfo(FTDocumentNo.Text)
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
        Call LoadOrderProdDataInfo(FTDocumentNo.Text)
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub

    Private Sub ocmmappomatcolor_Click(sender As Object, e As EventArgs) Handles ocmmappomatcolor.Click
        Try
            With Advmark
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
                    .OrderNo = Me.FTDocumentNo.Text
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
                _FM &= "  AND  {TPRODTOrderProd.FTOrderNo}='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
        End Select

        With New HI.RP.Report
            .FormTitle = Me.Text
            .ReportFolderName = "Production\"
            .ReportName = "OrderCuttingGrp.rpt"
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
        If Me.FTDocumentNo.Text <> "" And FTDocumentNo.Properties.Tag.ToString <> "" Then
            Call PreviewReport(2)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTDocumentNo_lbl.Text)
            FTDocumentNo.Focus()
            FTDocumentNo.SelectAll()
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

    'Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
    '    If Me.ValidateTableCutting(False) Then
    '        With _CopyTable
    '            HI.ST.Lang.SP_SETxLanguage(_CopyTable)

    '            .FTOrderProdNo.Text = Me.otbjobprod.SelectedTabPage.Text
    '            .FNHSysMarkId.Text = Me.otbmarkcutting.SelectedTabPage.Text
    '            .FNTableNo.Text = Me.otbtable.SelectedTabPage.Text
    '            .FNTotal.Value = 1
    '            .ShowDialog()

    '            If .ProcessSave Then
    '                Dim _Qry As String = ""
    '                Dim _Fromable As Integer = Integer.Parse(Val(Me.otbtable.SelectedTabPage.Text))
    '                Dim _MaxTable As Integer = 0

    '                _Qry = "SELECT MAX(FNTableNo) AS FNTableNo "
    '                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut WITH(NOLOCK) "
    '                _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "

    '                _MaxTable = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")))

    '                For I As Integer = 1 To .FNTotal.Value
    '                    _MaxTable = _MaxTable + 1

    '                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
    '                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal, FTRef,FTStateBundle,FTStateCutchange) "
    '                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
    '                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
    '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                    _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '                    _Qry &= vbCrLf & " ," & Integer.Parse(_MaxTable) & " "
    '                    _Qry &= vbCrLf & " , FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal, FTRef,FTStateBundle,FTStateCutchange "
    '                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut  WITH(NOLOCK) "
    '                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '                    _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(_Fromable) & " "

    '                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail "
    '                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity) "
    '                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
    '                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
    '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                    _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '                    _Qry &= vbCrLf & " ," & Integer.Parse(_MaxTable) & " "
    '                    _Qry &= vbCrLf & ", FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity "
    '                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail  WITH(NOLOCK)"
    '                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '                    _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(_Fromable) & " "

    '                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)


    '                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat"
    '                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTPurchaseNo, FNHSysRawMatId,FTShades)"
    '                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
    '                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
    '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                    _Qry &= vbCrLf & " ," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '                    _Qry &= vbCrLf & " ," & Integer.Parse(_MaxTable) & " "
    '                    _Qry &= vbCrLf & ",FTColorway, FTPurchaseNo, FNHSysRawMatId,FTShades "
    '                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat  WITH(NOLOCK)"
    '                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '                    _Qry &= vbCrLf & " AND FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
    '                    _Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(_Fromable) & " "

    '                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '                Next
    '                Call LoadOrderProdMarkBreakDown(otbjobprod.SelectedTabPage.Name.ToString)
    '                Call LoadOrderProdMarkTableCutting(otbjobprod.SelectedTabPage.Name.ToString, otbmarkcutting.SelectedTabPage.Name.ToString)
    '                Try
    '                    otbtable.SelectedTabPageIndex = (otbtable.TabPages.Count - 1)
    '                Catch ex As Exception
    '                End Try

    '            End If
    '        End With
    '    End If
    'End Sub


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

    Private Sub ocmadjunitsect_Click(sender As Object, e As EventArgs) Handles ocmadjunitsect.Click
        Try
            Dim MarkID As String = ""
            Dim OrderProdKey As String = ""
            MarkID = otbmarkcutting.SelectedTabPage.Name.ToString
            OrderProdKey = otbjobprod.SelectedTabPage.Name.ToString
            Dim _TotalTable As Integer = 0

            Dim _Qry As String = ""
            Dim _dtprod As DataTable

            _Qry = " SELECT '0' FTSelect , FNTableNo , t. FNHSysUnitSectId ,m.FTUnitSectCode"
            _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut t WITH(NOLOCK)  "
            _Qry &= vbCrLf & "   left join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect m   on t.FNHSysUnitSectId = m.FNHSysUnitSectId "
            _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
            _Qry &= vbCrLf & "  AND FNHSysMarkId =" & Val(MarkID.ToString) & " "
            _Qry &= vbCrLf & "  Order BY FNTableNo"

            _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Try
                Dim _Text As String = Me.otbmarkcutting.SelectedTabPage.Text


                HI.TL.HandlerControl.ClearControl(_adjunitsect)
                With _adjunitsect
                    .ogcpart.DataSource = _dtprod
                    .ogvpart.OptionsSelection.MultiSelect = True

                    .Text = _Text.Split(")-Cut")(0) & ")-Cut " & _TotalTable.ToString & "  Table"
                    .ShowDialog()
                    If .UpdateState = True Then


                        With DirectCast(.ogcpart.DataSource, DataTable)
                            .AcceptChanges()

                            For Each R As DataRow In .Select("FTSelect = '1' and FNHSysUnitSectId <>  0 ")

                                _Qry = " update  T"
                                _Qry &= vbCrLf & " Set T.FNHSysUnitSectId  = " & Val(R!FNHSysUnitSectId.ToString)
                                _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut  T  "
                                _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
                                _Qry &= vbCrLf & "  AND FNHSysMarkId =" & Val(MarkID.ToString) & " "
                                _Qry &= vbCrLf & " and  FNTableNo=" & Val(R!FNTableNo.ToString)

                                _Qry &= vbCrLf & " update  z"
                                _Qry &= vbCrLf & " Set z.FNHSysUnitSectId  = " & Val(R!FNHSysUnitSectId.ToString)
                                _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut  T  "
                                _Qry &= vbCrLf & " left join TPRODTOrderProd_TableCut_Detail x on t.FNTableNo = x.FNTableNoRef"
                                _Qry &= vbCrLf & " left join TPRODTOrderProd_TableCut z on x.FNTableNo = z.FNTableNo and x.FTOrderProdNo  = z.FTOrderProdNo "
                                _Qry &= vbCrLf & " and x.FNHSysMarkId = z.FNHSysMarkId left join TPRODTOrderProd p on z.FTOrderProdNo = p.FTOrderProdNo  and t.FTOrderProdNo = p.FTDocumentNo"


                                _Qry &= vbCrLf & "  WHERE t. FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
                                _Qry &= vbCrLf & "  AND t.FNHSysMarkId =" & Val(MarkID.ToString) & " "
                                _Qry &= vbCrLf & " and t. FNTableNo=" & Val(R!FNTableNo.ToString)



                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                            Next

                        End With


                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                        If Not (otbtable.SelectedTabPage Is Nothing) Then
                            Call LoadTableCuttingBreakdown(otbtable.SelectedTabPage.Name.ToString)
                        Else
                            Call LoadTableCuttingBreakdown("0")
                        End If

                    End If


                End With

            Catch ex As Exception

            End Try




        Catch ex As Exception

        End Try
    End Sub


#Region "Proc gen OrderProd"

    '    --================================================
    '-- Template generated from Template Explorer using:
    '-- Create Procedure (New Menu).SQL
    '--
    '-- Use the Specify Values for Template Parameters 
    '-- command (Ctrl-Shift-M) to fill in the parameter 
    '-- values below.
    '--
    '-- This block of comments will Not be included in
    '-- the definition of the procedure.
    '--================================================
    'Set ANSI_NULLS On
    'GO
    'Set QUOTED_IDENTIFIER On
    'GO
    '--=============================================
    '-- Author:		<Author,, Name>
    '-- Create Date <Create Date,,>
    '-- Description:	<Description,,>
    '--=============================================
    'CREATE PROCEDURE dbo.SP_GET_MUGroupProdToOrderProd
    '	-- Add the parameters for the stored procedure here
    '	 @User varchar(100) = 'admin'
    '	,@DocumentNo varchar(100)  = ''
    'AS
    'BEGIN




    '-- declare @User varchar(100)
    '--declare @DocumentNo varchar(100)
    '--set @User = 'Admin'
    '--set @DocumentNo = '91GP-22030100001' 





    ' Declare @TPRODMUTOrderProd_TableCut_Detail as Table (

    '	[FTOrderProdNo] [nvarchar](30) Not NULL,
    '	[FNHSysMarkId] [int] Not NULL,
    '	[FNTableNo] [int] Not NULL,
    '	[FTColorway] [nvarchar](30) Not NULL,
    '	[FTSizeBreakDown] [nvarchar](30) Not NULL,
    '	[FNLayer] [int] NULL,
    '	[FNAssort] [int] NULL,
    '	[FNQuantity] [int] NULL,
    '	[FNQuantityUse] [int] NULL

    '	)


    ' insert into @TPRODMUTOrderProd_TableCut_Detail
    'Select Case FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity, 0
    'FROM     HITECH_PRODUCTION.dbo.TPRODMUTOrderProd_TableCut_Detail
    ' where     FTOrderProdNo = @DocumentNo +'-P001'






    ' -- Declare Variable
    '	Declare @sCmpId int
    '	Declare @sOrderNo VARCHAR(50)
    '    Declare @sSubOrderNo varchar(50)
    '    Declare @sColorWay varchar(50)
    '    Declare @sStyleCode varchar(50)

    '	-- Declare cursor from select table 'CUSTOEMR'
    '	Declare cursor_grup CURSOR FOR ()

    '    Select Case distinct a.FNHSysCmpId, a.FTOrderNo  , a.FTSubOrderNo , a.FTColorWay , a.FTStyleCode

    '	from [HITECH_PRODUCTION].dbo. TPRODMUGroupPlan a With(nolock) 
    '	inner join [HITECH_PRODUCTION].dbo.TPRODMUTOrderProd_Detail P With(nolock) On a.FTOrderNo = p.FTOrderNo And a.FTSubOrderNo = p.FTSubOrderNo 
    '	And a.FTDocumentNo = p.FTDocumentNo And a.FTColorWay = p.FTColorway And a.FTSizeBreakDown = p.FTSizeBreakDown
    '	left join HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination v On a.FTCustomerPO = v.FTPOref And a.FTPOLine = v.FTNikePOLineItem
    '	And a.FTOrderNo = v.FTOrderNo And a.FTSubOrderNo = v.FTSubOrderNo And a.FTColorWay = v.FTColorway And a.FTSizeBreakDown = v.FTSizeBreakDown
    '	 where  a.FTDocumentNo=@DocumentNo
    '	  -- And a.FTSubOrderNo = 'NI2124701-A'
    '	 order by  a.FTOrderNo  , a.FTSubOrderNo , a.FTColorWay


    '	-- Open Cursor
    '	OPEN cursor_grup
    '	FETCH NEXT FROM cursor_grup 
    '	INTO @sCmpId, @sOrderNo,@sSubOrderNo , @sColorWay , @sStyleCode

    '	-- Loop From Cursor
    '	While (@@FETCH_STATUS = 0) 
    '	BEGIN 

    '		-- Display

    '		Declare @OrderProdNo varchar(30)
    '    Set @OrderProdNo = ''
    '		 Select Case TOP 1 @OrderProdNo  = FTOrderProdNo
    '		 from  HITECH_PRODUCTION.dbo.TPRODTOrderProd
    '		 WHERE FTOrderNo = @sOrderNo  And LEN(FTOrderProdNo) = LEN(@sOrderNo + '-P001')
    '		 order by  FTOrderProdNo desc 
    '		 If @OrderProdNo = '' 
    '			begin 
    '					Set @OrderProdNo = @OrderProdNo + '-P001'

    '			End
    '    Else
    '			begin 
    '					Set @OrderProdNo =  left( @OrderProdNo , len(@OrderProdNo) - 5)  +   '-P'+ RIGHT('000'+  convert(varchar(10) ,  convert(int ,  RIGHT(@OrderProdNo,3) )  + 1 ) ,3)

    '			End 

    '			 INSERT INTO  HITECH_PRODUCTION.dbo.TPRODTOrderProd 
    '			 (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo,FNHSysCmpId,FTDocumentNo)
    '			 Select Case@User
    '			 , Convert(varchar(10),Getdate(),111)
    '			 , Convert(varchar(8),Getdate(),114)
    '			 , @sOrderNo
    '			 , @OrderProdNo
    '			 ,@sCmpId
    '			 ,@DocumentNo


    '			 INSERT INTO HITECH_PRODUCTION.dbo.TPRODTOrderProd_Detail 
    '			  (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTOrderProdNo, FTSubOrderNo,FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCmpId)
    '			   Select Case distinct @User
    '			 , Convert(varchar(10),Getdate(),111)
    '			 , Convert(varchar(8),Getdate(),114)
    '			 , @sOrderNo
    '			 , @OrderProdNo
    '			 , a.FTSubOrderNo 
    '			 ,a.FTColorWay
    '			 , a.FTSizeBreakDown
    '			 , p.FNQuantity  
    '			 , @sCmpId
    '			from [HITECH_PRODUCTION].dbo. TPRODMUGroupPlan a With(nolock) 
    '			inner join [HITECH_PRODUCTION].dbo.TPRODMUTOrderProd_Detail P With(nolock) On a.FTOrderNo = p.FTOrderNo And a.FTSubOrderNo = p.FTSubOrderNo 
    '				And a.FTDocumentNo = p.FTDocumentNo And a.FTColorWay = p.FTColorway And a.FTSizeBreakDown = p.FTSizeBreakDown
    '			left join HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination v On a.FTCustomerPO = v.FTPOref And a.FTPOLine = v.FTNikePOLineItem
    '			And a.FTOrderNo = v.FTOrderNo And a.FTSubOrderNo = v.FTSubOrderNo And a.FTColorWay = v.FTColorway And a.FTSizeBreakDown = v.FTSizeBreakDown
    '			 where  p.FTDocumentNo=@DocumentNo
    '			 And a.FTSubOrderNo = @sSubOrderNo
    '			 And a.FTOrderNo = @sOrderNo
    '			 And a.FTColorWay = @sColorWay



    '			    UPDATE A Set FTStateCut ='1'	 
    '			   FROM  [HITECH_PRODUCTION].dbo.TPRODTOrderStatus As A
    '				  INNER Join
    '			  (
    '			   Select Case FTOrderNo, FTSubOrderNo
    '			   FROM  [HITECH_PRODUCTION].dbo.TPRODTOrderProd_Detail
    '			   WHERE (FTOrderNo =@sOrderNo)
    '			   GROUP BY FTOrderNo, FTSubOrderNo
    '			   ) AS B ON A.FTOrderNo = B.FTOrderNo
    '				And A.FTSubOrderNo = B.FTSubOrderNo
    '				 WHERE A.FTStateCut <>'1'
    '			   If @@ROWCOUNT <=0
    '				BEGIN
    '				  INSERT INTO [HITECH_PRODUCTION].dbo.TPRODTOrderStatus(
    '				  FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo
    '      				,FTStateCut, FTStateSew, FTStatePack)
    '				  Select Case'admin'
    '				  ,Convert(varchar(10),Getdate(),111)
    '				  ,Convert(varchar(8),Getdate(),114)
    '				  ,B.FTOrderNo
    '				  ,B.FTSubOrderNo 
    '				  ,'1'
    '				  ,'0'
    '				  ,'0'
    '				   FROM  [HITECH_PRODUCTION].dbo.TPRODTOrderStatus As A
    '				   RIGHT OUTER JOIN
    '				   (
    '				   Select Case FTOrderNo, FTSubOrderNo
    '					FROM   [HITECH_PRODUCTION].dbo.TPRODTOrderProd_Detail
    '				   WHERE (FTOrderNo = @sOrderNo)
    '				   GROUP BY FTOrderNo, FTSubOrderNo
    '				   ) AS B ON A.FTOrderNo = B.FTOrderNo
    '					And A.FTSubOrderNo = B.FTSubOrderNo
    '					WHERE A.FTOrderNo Is NULL
    '				End 




    '			insert into  [HITECH_PRODUCTION].dbo.TPRODTOrderProd_MarkMain
    '			(FTInsUser, FDInsDate, FTInsTime,  FTOrderProdNo, FNHSysMarkId, FTNote, FNHSysCmpId)
    '			Select Case@User
    '									 ,Convert(varchar(10),Getdate(),111) 
    '									 ,Convert(varchar(8),Getdate(),114) 
    '									 ,  @OrderProdNo, FNHSysMarkId, FTNote, FNHSysCmpId
    '			FROM     HITECH_PRODUCTION.dbo.TPRODMUTOrderProd_MarkMain t
    '			where left(FTOrderProdNo, Len(FTOrderProdNo) - 5) = @DocumentNo
    '			 And Not exists (
    '			Select Case*
    '            From HITECH_PRODUCTION.dbo.TPRODTOrderProd_MarkMain x 
    '			Where t.FNHSysMarkId = x.FNHSysMarkId
    '			And x.FTOrderProdNo = @OrderProdNo
    '			) 


    '			--- Add Table MarkMain
    '				 Declare @mMarkId int 
    '				 Declare cursor_mark CURSOR FOR ()
    '    Select Case FNHSysMarkId 
    '				from [HITECH_PRODUCTION].dbo.TPRODTOrderProd_MarkMain
    '				where FTOrderProdNo =  @OrderProdNo


    '				-- Open Cursor
    '				OPEN cursor_mark
    '				FETCH NEXT FROM cursor_mark 
    '				INTO @mMarkId


    '				-- Loop From Cursor
    '				While (@@FETCH_STATUS = 0) 
    '				BEGIN 






    '				    ---loop table cust main  for split to suborder
    '							Declare @tFNTableNo int  , @tFTSizeBreakDown varchar(30) , @tFNLayer int , @tFNAssort int  , @tFNQuantity int

    '							 Declare cursor_tablemain CURSOR FOR ()
    '    Select Case distinct   a.FNTableNo --, a.FTSizeBreakDown , a.FNLayer , a.FNAssort , a.FNQuantity - isnull( b.FNQuantity,0) As FNQuantity
    '							 from @TPRODMUTOrderProd_TableCut_Detail a 
    '							 outer apply(Select sum(b.FNQuantity) FNQuantity From  HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail b 
    '							 left join HITECH_PRODUCTION.dbo.TPRODTOrderProd p On b.FTOrderProdNo = p.FTOrderProdNo
    '							  outer apply(
    '									select top 1  s.FTStyleCode from HITECH_MERCHAN.dbo.TMERTOrder o with(nolock) 
    '									left join HITECH_MASTER.dbo.TMERMStyle s With(nolock) On  o.FNHSysStyleId = s.FNHSysStyleId
    '									where o.FTOrderNo = p.FTOrderNo
    '							  ) o  
    '							 where  p.FTDocumentNo = @DocumentNo And   a.FNTableNo  = b.FNTableNoRef   
    '							 And FTStyleCode+'-'+ b.FTSizeBreakDown = a.FTSizeBreakDown
    '							 ) b

    '							 where  a.FNHSysMarkId = @mMarkId							
    '							 And left(a.FTSizeBreakDown,len(@sStyleCode)) = @sStyleCode
    '							  And a.FNQuantity - isnull( b.FNQuantity,0) > 0 

    '							  -- Open Cursor
    '							OPEN cursor_tablemain
    '							FETCH NEXT FROM cursor_tablemain 
    '							INTO @tFNTableNo --, @tFTSizeBreakDown  , @tFNLayer   , @tFNAssort    , @tFNQuantity			

    '							-- Loop From Cursor
    '							While (@@FETCH_STATUS = 0) 
    '							BEGIN 



    '									  Declare @TableNos int 
    '									 Set @TableNos = 0 
    '				    					Select Case TOp 1 @TableNos = FNTableNo  FROM [HITECH_PRODUCTION].dbo.TPRODTOrderProd_TableCut 
    '										 WHERE FTOrderProdNo = @OrderProdNo
    '										 And  FNHSysMarkId=@mMarkId 
    '									 Order BY FNTableNo Desc  

    '									 Set @TableNos  = @TableNos+1

    '									 insert into  [HITECH_PRODUCTION].dbo.TPRODTOrderProd_TableCut_Detail
    '									 ( FTInsUser, FDInsDate, FTInsTime,  FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway
    '									 , FTSizeBreakDown, FNLayer, FNAssort, FNQuantity, FNHSysCmpId, FNTableNoRef)

    '									 Select Case@User
    '									 ,Convert(varchar(10),Getdate(),111) 
    '									 ,Convert(varchar(8),Getdate(),114) 
    '									 ,@OrderProdNo
    '									 ,@mMarkId 
    '									 ,@TableNos 
    '									 , @sColorWay
    '									 , a.FTSizeBreakDown

    '									 ,  case when ( p.FNQuantity - isnull(ba.FNQuantity,0) ) >=  tc.FNQuantity 
    '									         then 
    '													tc.FNLayer 
    '											 Else
    '												 convert(numeric(18,0) , 	( p.FNQuantity - isnull(ba.FNQuantity,0) ) / tc.FNAssort) 
    '											 End



    '									 , tc.FNAssort
    '									 ,  case when ( p.FNQuantity - isnull(ba.FNQuantity,0) ) >=  tc.FNQuantity 
    '									         then 
    '													tc.FNQuantity 
    '											 Else
    '													( p.FNQuantity - isnull(ba.FNQuantity,0) )
    '											 End
    '									 , @sCmpId
    '									 , @tFNTableNo 


    '									from [HITECH_PRODUCTION].dbo. TPRODMUGroupPlan a With(nolock) 
    '										inner join [HITECH_PRODUCTION].dbo.TPRODMUTOrderProd_Detail P With(nolock) On a.FTOrderNo = p.FTOrderNo And a.FTSubOrderNo = p.FTSubOrderNo 
    '										And a.FTDocumentNo = p.FTDocumentNo And a.FTColorWay = p.FTColorway And a.FTSizeBreakDown = p.FTSizeBreakDown
    '									left join HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination v On a.FTCustomerPO = v.FTPOref And a.FTPOLine = v.FTNikePOLineItem
    '									And a.FTOrderNo = v.FTOrderNo And a.FTSubOrderNo = v.FTSubOrderNo And a.FTColorWay = v.FTColorway And a.FTSizeBreakDown = v.FTSizeBreakDown
    '									left join HITECH_MASTER.dbo.TPRODMMark m On a.FTPartCode = m.FTMarkCode
    '									 outer apply(Select sum(b.FNQuantity) FNQuantity
    '									 From  HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail b 

    '										 where  b.FTOrderProdNo = @OrderProdNo
    '										 And FTColorway = @sColorWay
    '										 And FTSizeBreakDown = a.FTSizeBreakDown
    '										  And FNHSysMarkId = @mMarkId
    '										  --And FNTableNoRef = @tFNTableNo
    '										 ) ba

    '									cross apply(

    '                                     Select Case ta.FNTableNo  , ta.FTSizeBreakDown , ta.FNLayer , ta.FNAssort ,   ta.FNQuantity - isnull( b.FNQuantity,0) As FNQuantity
    '																 from @TPRODMUTOrderProd_TableCut_Detail ta  
    '																 outer apply(

    '                                                                 Select Case sum(b.FNQuantity) FNQuantity 
    '																 From  HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail b 
    '																 left join HITECH_PRODUCTION.dbo.TPRODTOrderProd p On b.FTOrderProdNo = p.FTOrderProdNo
    '																   outer apply(
    '																		select top 1  s.FTStyleCode from HITECH_MERCHAN.dbo.TMERTOrder o with(nolock) 
    '																		left join HITECH_MASTER.dbo.TMERMStyle s With(nolock) On  o.FNHSysStyleId = s.FNHSysStyleId
    '																		where o.FTOrderNo = p.FTOrderNo
    '																  ) o  
    '																 where  p.FTDocumentNo = @DocumentNo  And  b.FNTableNoRef    = ta.FNTableNo 
    '																  And FNHSysMarkId = @mMarkId
    '																  And b.FTSizeBreakDown = a.FTSizeBreakDown  
    '																  And FTStyleCode+'-'+ b.FTSizeBreakDown= ta.FTSizeBreakDown

    '																 ) b



    '																 where  ta.FNHSysMarkId = @mMarkId 
    '																 And  ta.FTSizeBreakDown  =  a.FTStyleCode +'-'+ a.FTSizeBreakDown 
    '																 And ta.FNQuantity - isnull( b.FNQuantity,0) > 0 
    '																 And ta.FNTableNo = @tFNTableNo


    '									) tc


    '									 where a.FTDocumentNo=@DocumentNo 
    '									 And a.FTSubOrderNo = @sSubOrderNo
    '									 And FNHSysMarkId = @mMarkId
    '									 And p.FNQuantity - isnull(ba.FNQuantity,0) >  0 
    '									 ---







    '									  INSERT INTO  [HITECH_PRODUCTION].dbo.TPRODTOrderProd_TableCut 
    '									 (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId
    '									, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef,FTStateBundle,FTStateCutchange,FTStatTableScrap) 
    '									 Select Case@User
    '									 ,Convert(varchar(10),Getdate(),111) 
    '									 ,Convert(varchar(8),Getdate(),114) 
    '									 ,@OrderProdNo
    '									 ,@mMarkId 
    '									 ,@TableNos 
    '									 , FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId
    '									, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef,FTStateBundle,FTStateCutchange,FTStatTableScrap

    '									 from [HITECH_PRODUCTION].dbo.TPRODMUTOrderProd_TableCut 
    '									 where FTOrderProdNo = @DocumentNo + '-P001'
    '									 And FNTableNo = @tFNTableNo
    '									 And FNHSysMarkId  = @mMarkId
    '									  And   exists (
    '										Select Case*
    '                                        From HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail x 
    '										Where x.FTOrderProdNo = @OrderProdNo
    '										And x.FNTableNo = @TableNos
    '										And x.FNHSysMarkId  = @mMarkId
    '										) 








    '							 FETCH NEXT FROM cursor_tablemain -- Fetch next cursor
    '								INTO @tFNTableNo --, @tFTSizeBreakDown  , @tFNLayer   , @tFNAssort    , @tFNQuantity	  -- Next into variable
    '							End

    '							-- Close cursor
    '							CLOSE cursor_tablemain; 
    '							DEALLOCATE cursor_tablemain;




    '					---loop table cust main  for split to suborder End








    '					FETCH NEXT FROM cursor_mark -- Fetch next cursor
    '					INTO @mMarkId  -- Next into variable
    '				End

    '				-- Close cursor
    '				CLOSE cursor_mark; 
    '				DEALLOCATE cursor_mark;






    '			 --- End Ad Table MarkMain



    '			 insert into  [HITECH_PRODUCTION].dbo.TPRODTOrderProd_MarkSub
    '			 (FTInsUser, FDInsDate, FTInsTime,   FTOrderProdNo, FNHSysMarkId, FNHSysSubMarkId , FTNote, FNHSysCmpId)
    '			Select Case@User
    '									 ,Convert(varchar(10),Getdate(),111) 
    '									 ,Convert(varchar(8),Getdate(),114) 
    '									 ,   @OrderProdNo, FNHSysMarkId,FNHSysSubMarkId , FTNote, FNHSysCmpId
    '			FROM     HITECH_PRODUCTION.dbo.TPRODMUTOrderProd_MarkSub t
    '			where left(FTOrderProdNo, Len(FTOrderProdNo) - 5) = @DocumentNo
    '			And Not exists (
    '			Select Case*
    '            From HITECH_PRODUCTION.dbo.TPRODMUTOrderProd_MarkSub x 
    '			Where t.FNHSysMarkId = x.FNHSysMarkId And t.FNHSysSubMarkId = x.FNHSysSubMarkId
    '			And x.FTOrderProdNo = @OrderProdNo
    '			) 


    '			--- Add Table MarkSub



    '			 Declare @mMarkIdsub int 
    '				 Declare cursor_mark CURSOR FOR ()
    '    Select Case FNHSysSubMarkId 
    '				from [HITECH_PRODUCTION].dbo.TPRODTOrderProd_MarkSub
    '				where FTOrderProdNo =  @OrderProdNo


    '				-- Open Cursor
    '				OPEN cursor_mark
    '				FETCH NEXT FROM cursor_mark 
    '				INTO @mMarkIdsub


    '				-- Loop From Cursor
    '				While (@@FETCH_STATUS = 0) 
    '				BEGIN 


    '					print ('@@mMarkIdsub')
    '				print @mMarkIdsub
    '				    ---loop table cust main  for split to suborder



    '							Declare @tFNTableNosub int  , @tFTSizeBreakDownSub varchar(30) , @tFNLayerSub int , @tFNAssortSub int  , @tFNQuantitySub int

    '							 Declare cursor_tablemain CURSOR FOR ()
    '    Select Case distinct   a.FNTableNo --, a.FTSizeBreakDown , a.FNLayer , a.FNAssort , a.FNQuantity - isnull( b.FNQuantity,0) As FNQuantity
    '							 from @TPRODMUTOrderProd_TableCut_Detail a 
    '							 outer apply(Select sum(b.FNQuantity) FNQuantity From  HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail b 
    '							 left join HITECH_PRODUCTION.dbo.TPRODTOrderProd p On b.FTOrderProdNo = p.FTOrderProdNo
    '							   outer apply(
    '									select top 1  s.FTStyleCode from HITECH_MERCHAN.dbo.TMERTOrder o with(nolock) 
    '									left join HITECH_MASTER.dbo.TMERMStyle s With(nolock) On  o.FNHSysStyleId = s.FNHSysStyleId
    '									where o.FTOrderNo = p.FTOrderNo
    '							  ) o  
    '							 where  p.FTDocumentNo = @DocumentNo And   a.FNTableNo  = b.FNTableNoRef   
    '							   And o. FTStyleCode+'-'+ b.FTSizeBreakDown= a.FTSizeBreakDown
    '							 ) b

    '							 where  a.FNHSysMarkId = @mMarkIdsub							
    '							 And left(a.FTSizeBreakDown,len(@sStyleCode)) = @sStyleCode
    '							  And a.FNQuantity - isnull( b.FNQuantity,0) > 0 

    '							  -- Open Cursor
    '							OPEN cursor_tablemain
    '							FETCH NEXT FROM cursor_tablemain 
    '							INTO @tFNTableNosub --, @tFTSizeBreakDown  , @tFNLayer   , @tFNAssort    , @tFNQuantity			

    '							-- Loop From Cursor
    '							While (@@FETCH_STATUS = 0) 
    '							BEGIN 

    '									 Declare @TableNossub int 
    '									 Set @TableNossub = 0 
    '				    					Select Case TOp 1 @TableNossub = FNTableNo  FROM [HITECH_PRODUCTION].dbo.TPRODTOrderProd_TableCut 
    '										 WHERE FTOrderProdNo = @OrderProdNo
    '										 And  FNHSysMarkId=@mMarkIdsub 
    '									 Order BY FNTableNo Desc  

    '									 Set @TableNossub  = @TableNossub+1

    '									 print @TableNossub

    '									 insert into  [HITECH_PRODUCTION].dbo.TPRODTOrderProd_TableCut_Detail
    '									 ( FTInsUser, FDInsDate, FTInsTime,  FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway
    '									 , FTSizeBreakDown, FNLayer, FNAssort, FNQuantity, FNHSysCmpId, FNTableNoRef)

    '									 Select Case@User
    '									 ,Convert(varchar(10),Getdate(),111) 
    '									 ,Convert(varchar(8),Getdate(),114) 
    '									 ,@OrderProdNo
    '									 ,@mMarkIdsub 
    '									 ,@TableNossub 
    '									 , @sColorWay
    '									 , a.FTSizeBreakDown
    '									 ,  case when ( p.FNQuantity - isnull(ba.FNQuantity,0) ) >=  tc.FNQuantity 
    '									         then 
    '													tc.FNLayer 
    '											 Else
    '												 convert(numeric(18,0) , 	( p.FNQuantity - isnull(ba.FNQuantity,0) ) / tc.FNAssort) 
    '											 End
    '									 , tc.FNAssort
    '									  ,  case when ( p.FNQuantity - isnull(ba.FNQuantity,0) ) >=  tc.FNQuantity 
    '									         then 
    '													tc.FNQuantity 
    '											 Else
    '													( p.FNQuantity - isnull(ba.FNQuantity,0) )
    '											 End
    '									 , @sCmpId
    '									 , @tFNTableNosub 


    '									from [HITECH_PRODUCTION].dbo. TPRODMUGroupPlan a With(nolock) 
    '										inner join [HITECH_PRODUCTION].dbo.TPRODMUTOrderProd_Detail P With(nolock) On a.FTOrderNo = p.FTOrderNo And a.FTSubOrderNo = p.FTSubOrderNo 
    '										And a.FTDocumentNo = p.FTDocumentNo And a.FTColorWay = p.FTColorway And a.FTSizeBreakDown = p.FTSizeBreakDown
    '									left join HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination v On a.FTCustomerPO = v.FTPOref And a.FTPOLine = v.FTNikePOLineItem
    '									And a.FTOrderNo = v.FTOrderNo And a.FTSubOrderNo = v.FTSubOrderNo And a.FTColorWay = v.FTColorway And a.FTSizeBreakDown = v.FTSizeBreakDown
    '									left join HITECH_MASTER.dbo.TPRODMMark m On a.FTPartCode = m.FTMarkCode
    '									 outer apply(Select sum(b.FNQuantity) FNQuantity From  HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail b 

    '										 where  b.FTOrderProdNo = @OrderProdNo
    '										 And FTColorway = @sColorWay
    '										 And FTSizeBreakDown = a.FTSizeBreakDown
    '										 And FNHSysMarkId = @mMarkIdsub
    '										 And FNTableNoRef = @tFNTableNo
    '										 ) ba

    '									cross apply(

    '                                     Select Case ta.FNTableNo  , ta.FTSizeBreakDown , ta.FNLayer , ta.FNAssort ,   ta.FNQuantity - isnull( b.FNQuantity,0) As FNQuantity
    '																 from @TPRODMUTOrderProd_TableCut_Detail ta  
    '																 outer apply(Select sum(b.FNQuantity) FNQuantity 
    '																 From  HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail b 
    '																 left join HITECH_PRODUCTION.dbo.TPRODTOrderProd p On b.FTOrderProdNo = p.FTOrderProdNo
    '																 outer apply(
    '																		select top 1  s.FTStyleCode from HITECH_MERCHAN.dbo.TMERTOrder o with(nolock) 
    '																		left join HITECH_MASTER.dbo.TMERMStyle s With(nolock) On  o.FNHSysStyleId = s.FNHSysStyleId
    '																		where o.FTOrderNo = p.FTOrderNo
    '																  ) o 
    '																 where  p.FTDocumentNo = @DocumentNo  And   ta.FNTableNo  = b.FNTableNoRef   
    '																 And FNHSysMarkId = @mMarkIdsub
    '																 And b.FTSizeBreakDown = a.FTSizeBreakDown 
    '																 And o. FTStyleCode+'-'+ b.FTSizeBreakDown = ta.FTSizeBreakDown
    '																 ) b
    '																 where  ta.FNHSysMarkId = @mMarkIdsub 
    '																 And  ta.FTSizeBreakDown  =  a.FTStyleCode +'-'+ a.FTSizeBreakDown 
    '																 And ta.FNQuantity - isnull( b.FNQuantity,0) > 0 
    '																 And ta.FNTableNo = @tFNTableNosub


    '									) tc






    '									 where a.FTDocumentNo=@DocumentNo 
    '									 And a.FTSubOrderNo = @sSubOrderNo
    '									 And FNHSysMarkId = @mMarkIdsub
    '									 And p.FNQuantity - isnull(ba.FNQuantity,0) >  0 
    '									 ---




    '									  INSERT INTO  [HITECH_PRODUCTION].dbo.TPRODTOrderProd_TableCut 
    '									 (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId
    '									, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef,FTStateBundle,FTStateCutchange,FTStatTableScrap) 
    '									 Select Case@User
    '									 ,Convert(varchar(10),Getdate(),111) 
    '									 ,Convert(varchar(8),Getdate(),114) 
    '									 ,@OrderProdNo
    '									 ,@mMarkIdsub 
    '									 ,@TableNossub 
    '									 , FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId
    '									, FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef,FTStateBundle,FTStateCutchange,FTStatTableScrap

    '									 from [HITECH_PRODUCTION].dbo.TPRODMUTOrderProd_TableCut 
    '									 where FTOrderProdNo = @DocumentNo + '-P001'
    '									 And FNTableNo = @tFNTableNo
    '									  And FNHSysMarkId  = @mMarkIdsub
    '									  And   exists (
    '										Select Case*
    '                                        From HITECH_PRODUCTION.dbo.TPRODTOrderProd_TableCut_Detail x 
    '										Where x.FTOrderProdNo = @OrderProdNo
    '										And x.FNTableNo = @TableNossub
    '										And x.FNHSysMarkId  = @mMarkIdsub
    '										) 










    '							 FETCH NEXT FROM cursor_tablemain -- Fetch next cursor
    '								INTO @tFNTableNo --, @tFTSizeBreakDown  , @tFNLayer   , @tFNAssort    , @tFNQuantity	  -- Next into variable
    '							End

    '							-- Close cursor
    '							CLOSE cursor_tablemain; 
    '							DEALLOCATE cursor_tablemain;





    '					FETCH NEXT FROM cursor_mark -- Fetch next cursor
    '					INTO @mMarkId  -- Next into variable
    '				End

    '				-- Close cursor
    '				CLOSE cursor_mark; 
    '				DEALLOCATE cursor_mark;



    '			--- End Add Table MarkSub





    '	FETCH NEXT FROM cursor_grup -- Fetch next cursor
    '	INTO @sCmpId, @sOrderNo,@sSubOrderNo , @sColorWay ,@sStyleCode  -- Next into variable
    'End

    '-- Close cursor
    'CLOSE cursor_grup; 
    'DEALLOCATE cursor_grup;






    '    End
    'GO

#End Region
End Class
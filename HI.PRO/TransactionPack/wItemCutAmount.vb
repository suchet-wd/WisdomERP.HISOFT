Imports System.Drawing
Imports System.Windows.Forms

Public Class wItemCutAmount
    Private _StateSubNew As Boolean = False
    Private _FTSubOrderNo As String
    Private _FTColorway As String

    Private _AddItem As wAddCutItem

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

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
        _Qry &= vbCrLf & "  Order By FTOrderProdNo  "

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

        'Call InitDataPurchaseOrder(Key)

        _Spls.Close()
    End Sub

    Private Sub LoadOrderProdBreakDown(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _Col As String
        Dim _dtprod As DataTable

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each row As DataRow In _dt.Rows
            _FTColorway = row!FTColorway.ToString
            _FTSubOrderNo = row!FTSubOrderNo.ToString
        Next
        'With Me.ogvjobprod

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

        'Me.ogcjobprod.DataSource = _dt.Copy
        'If _colcount > 4 Then
        '    ogvjobprod.BestFitColumns()
        'End Iadmin

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

        '_Qry = " SELECT OP.FTOrderNo,A.FNHSysMarkId,B.FTMarkCode"

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & " ,B.FTMarkNameTH AS FTMarkName"
        'Else
        '    _Qry &= vbCrLf & " ,B.FTMarkNameEN AS FTMarkName"
        'End If

        '_Qry &= vbCrLf & " ,ISNULL((Select FTPOref FROM HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination AS V WHERE OP.FTOrderNo = V.FTOrderNo group by FTPOref),0) AS FTPOref "
        '_Qry &= vbCrLf & " ,ISNULL((Select FTNikePOLineItem FROM HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination AS V LEFT OUTER JOIN HITECH_PRODUCTION.dbo.TPRODTOrderProd_Detail AS OD ON V.FTSubOrderNo = OD.FTSubOrderNo AND V.FTColorway = OD.FTColorway WHERE OD.FTOrderProdNo = OP.FTOrderProdNo group by FTNikePOLineItem),0) AS FTNikePOLineItem "

        '_Qry &= vbCrLf & "    FROM"
        '_Qry &= vbCrLf & "  (SELECT     1 AS FNSeq, FTOrderProdNo, FNHSysMarkId"
        '_Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain  WITH(NOLOCK)  "
        '_Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        '_Qry &= vbCrLf & "    UNION"
        '_Qry &= vbCrLf & "  SELECT     2 AS FNSeq, FTOrderProdNo, FNHSysSubMarkId AS FNHSysMarkId"
        '_Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub  WITH(NOLOCK)   "
        '_Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        '_Qry &= vbCrLf & " ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS B ON A.FNHSysMarkId = B.FNHSysMarkId"
        '_Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS OP ON A.FTOrderProdNo = OP.FTOrderProdNo"
        '_Qry &= vbCrLf & "  Order BY A.FNSeq,B.FTMarkCode"

        _Qry = " SELECT OP.FTOrderNo,A.FNHSysPartId,B.FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,B.FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & " ,B.FTPartNameEN AS FTPartName"
        End If

        _Qry &= vbCrLf & " ,ISNULL((Select FTPOref FROM HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination AS V WHERE OP.FTOrderNo = V.FTOrderNo group by FTPOref),0) AS FTPOref "
        _Qry &= vbCrLf & " ,ISNULL((Select FTNikePOLineItem FROM HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination AS V LEFT OUTER JOIN HITECH_PRODUCTION.dbo.TPRODTOrderProd_Detail AS OD ON V.FTSubOrderNo = OD.FTSubOrderNo AND V.FTColorway = OD.FTColorway WHERE OD.FTOrderNo = OP.FTOrderNo and OD.FTColorway = '" & _FTColorway & "' group by FTNikePOLineItem),0) AS FTNikePOLineItem "

        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part AS A WITH(NOLOCK)  "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS B ON A.FNHSysPartId = B.FNHSysPartId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OP ON A.FNHSysStyleId = OP.FNHSysStyleId AND A.FNHSysSeasonId = OP.FNHSysSeasonId"
        _Qry &= vbCrLf & "  WHERE OP.FTOrderNo = '" & FTOrderNo.Text & "'"

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.ogvbalance

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                                _Col = Col.ColumnName.ToString
                            End With
                            _dtprod.Columns.Add(_Col)
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

        'Me.ogcbalance.DataSource = _dtprod.Copy

        'Call Calculate_balance()
    End Sub

    Private Sub LoadOrderProdCutItem(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _Col As String
        Dim _dtprod As DataTable
        Dim _dtAmount As DataTable
        Dim _dtbal As DataTable



        _Qry = " SELECT  convert(varchar(10) , convert( date ,FDSaveDate),103) as FDSaveDate  ,  C.FTOrderNo, CD.FTOrderProdNo, FTColorway"
        _Qry &= vbCrLf & " , CD.FNHSysPartId, P.FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,max(P.FTPartNameTH) AS FTPartName"
        Else
            _Qry &= vbCrLf & " ,max(P.FTPartNameEN) AS FTPartName"
        End If
        _Qry &= vbCrLf & ", US.FTUnitSectCode, CD.FNHSysUnitSectId"
        '_Qry &= vbCrLf & " , FTSizeBreakDown, FNQuantity"
        _Qry &= vbCrLf & " ,ISNULL((Select Top 1 FTPOref FROM HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination AS V WHERE C.FTOrderNo = V.FTOrderNo  ),0) AS FTPOref "
        _Qry &= vbCrLf & " ,ISNULL((Select Top 1 FTNikePOLineItem FROM HITECH_MERCHAN.dbo.TMERTOrderSub_BreakDown AS V WHERE V.FTOrderNo = C.FTOrderNo and V.FTColorway = CD.FTColorway  ),0) AS FTNikePOLineItem "
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail AS CD WITH(NOLOCK)"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK) ON CD.FNHSysPartId = P.FNHSysPartId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord AS C WITH(NOLOCK) ON CD.FTOrderProdNo = C.FTOrderProdNo"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON CD.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & "   WHERE CD.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'"
        'If (FDFromDate.Text <> "") Then
        '    _Qry &= vbCrLf & "       AND FDSaveDate >= N'" & HI.UL.ULDate.ConvertEnDB(Me.FDFromDate.Text) & "'"
        'End If
        'If (FDToDate.Text <> "") Then
        '    _Qry &= vbCrLf & "       AND FDSaveDate <= N'" & HI.UL.ULDate.ConvertEnDB(Me.FDToDate.Text) & "'"
        'End If
        _Qry &= vbCrLf & "    Group by FDSaveDate , C.FTOrderNo, CD.FTOrderProdNo, FTColorway, CD.FNHSysPartId, P.FTPartCode , US.FTUnitSectCode, CD.FNHSysUnitSectId"



        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _Qry = " SELECT  convert(varchar(10) , convert( date ,FDSaveDate),103) as FDSaveDate  ,  C.FTOrderNo, CD.FTOrderProdNo, FTColorway"
        _Qry &= vbCrLf & " , CD.FNHSysPartId, P.FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,max(P.FTPartNameTH) AS FTPartName"
        Else
            _Qry &= vbCrLf & " ,max(P.FTPartNameEN) AS FTPartName"
        End If
        '_Qry &= vbCrLf & ", US.FTUnitSectCode, CD.FNHSysUnitSectId"
        '_Qry &= vbCrLf & " , FTSizeBreakDown, FNQuantity"
        _Qry &= vbCrLf & " ,ISNULL((Select Top 1 FTPOref FROM HITECH_MERCHAN.dbo.V_OrderSub_BreakDown_ShipDestination AS V WHERE C.FTOrderNo = V.FTOrderNo  ),0) AS FTPOref "
        _Qry &= vbCrLf & " ,ISNULL((Select Top 1 FTNikePOLineItem FROM HITECH_MERCHAN.dbo.TMERTOrderSub_BreakDown AS V WHERE V.FTOrderNo = C.FTOrderNo and V.FTColorway = CD.FTColorway  ),0) AS FTNikePOLineItem "
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail AS CD WITH(NOLOCK)"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK) ON CD.FNHSysPartId = P.FNHSysPartId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord AS C WITH(NOLOCK) ON CD.FTOrderProdNo = C.FTOrderProdNo"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON CD.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & "   WHERE CD.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'"
        'If (FDFromDate.Text <> "") Then
        '    _Qry &= vbCrLf & "       AND FDSaveDate >= N'" & HI.UL.ULDate.ConvertEnDB(Me.FDFromDate.Text) & "'"
        'End If
        'If (FDToDate.Text <> "") Then
        '    _Qry &= vbCrLf & "       AND FDSaveDate <= N'" & HI.UL.ULDate.ConvertEnDB(Me.FDToDate.Text) & "'"
        'End If
        _Qry &= vbCrLf & "    Group by FDSaveDate , C.FTOrderNo, CD.FTOrderProdNo, FTColorway, CD.FNHSysPartId, P.FTPartCode  "



        _dtbal = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)



        '__________________________________________________________________________________________
        _Qry = " SELECT convert(varchar(10) , convert(date, FDSaveDate),103) as FDSaveDate  ,  CD.FTOrderProdNo, FTColorway"
        _Qry &= vbCrLf & " , CD.FNHSysPartId, P.FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,P.FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & " ,P.FTPartNameEN AS FTPartName"
        End If


        _Qry &= vbCrLf & " , FTSizeBreakDown, FNQuantity  , FNHSysUnitSectId"
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail AS CD WITH(NOLOCK)"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK) ON CD.FNHSysPartId = P.FNHSysPartId"
        '_Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord AS C WITH(NOLOCK) ON CD.FTOrderProdNo = C.FTOrderProdNo"
        _Qry &= vbCrLf & "   WHERE CD.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'"
        'If (FDFromDate.Text <> "") Then
        '    _Qry &= vbCrLf & "       AND FDSaveDate >= N'" & HI.UL.ULDate.ConvertEnDB(Me.FDFromDate.Text) & "'"
        'End If



        _dtAmount = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _colcount = 0
        With Me.ogvmark

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FDSaveDate".ToUpper, "FTUnitSectCode".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FDSaveDate".ToUpper, "FTUnitSectCode".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                                _Col = Col.ColumnName.ToString
                                .DisplayFormat.FormatString = "{0:n0}"
                                If Not (Col.ColumnName.ToString = "Total") Then

                                    .ColumnEdit = ReposCaleditWeight
                                Else

                                End If

                            End With

                            _dtprod.Columns.Add(_Col)
                            _dtbal.Columns.Add(_Col)
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
                                    '.AllowEdit = True
                                    '.ReadOnly = False

                                    If Not (Col.ColumnName.ToString = "Total") Then
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    Else
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End If
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 45
                            '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                        If (.OptionsColumn.AllowEdit) Then
                            .AppearanceCell.BackColor = Color.LightCyan
                        End If
                    End With
                Next

            End If


        End With

        Me.ogcbalance.DataSource = _dtbal.Copy

        For Each _R As DataRow In _dtprod.Rows
            For Each Col As DataColumn In _dtprod.Columns
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FDSaveDate".ToUpper, "FTUnitSectCode".ToUpper
                    Case Else
                        If (_dtAmount Is Nothing) Then
                            _R(Col.ColumnName.ToString) = 0
                        Else
                            For Each _mR As DataRow In _dtAmount.Select("FNHSysPartId='" & _R!FNHSysPartId.ToString & "' AND FTSizeBreakDown ='" & Col.ColumnName.ToString & "' AND FNHSysUnitSectId =" & _R!FNHSysUnitSectId.ToString & "")
                                If (IsDBNull(_R.Item(Col.ColumnName.ToString))) Then
                                    _R.Item(Col.ColumnName.ToString) = _mR!FNQuantity
                                Else
                                    _R.Item(Col.ColumnName.ToString) = CInt(_R.Item(Col.ColumnName.ToString)) + CInt(_mR!FNQuantity)
                                End If
                            Next
                        End If

                End Select
            Next
        Next





        Me.ogcmark.DataSource = _dtprod



        Call Calculate_balance()
        Call SumGrid()
    End Sub

    'Private Sub LoadOrderCutItemUnitSect()
    '    Dim _dt As DataTable
    '    Dim _Qry As String = ""
    '    Dim _colcount As Integer = 0
    '    Dim _dtprod As DataTable

    '    _Qry = " SELECT FTOrderNo, FTSubOrderNo, C.FTOrderProdNo, FTColorway, C.FNHSysUnitSectId"
    '    _Qry &= vbCrLf & " , FNHSysPartId, FTSizeBreakDown, FNQuantity"

    '    _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord AS C WITH(NOLOCK)  "
    '    _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail AS CD WITH(NOLOCK) ON C.FTOrderProdNo = CD.FTOrderProdNo AND C.FNHSysUnitSectId = CD.FNHSysUnitSectId"
    '    _Qry &= vbCrLf & "  WHERE C.FTOrderProdNo = '" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' AND C.FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
    '    '_Qry &= vbCrLf & "  Order BY A.FNSeq,B.FTMarkCode"

    '    _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    CType(ogcmark.DataSource, DataTable).AcceptChanges()
    '    Try
    '        Dim _Total As Double = 0
    '        Dim _balDt As DataTable
    '        Dim _markDt As DataTable
    '        Dim _jobbalDt As DataTable
    '        _Total = 0

    '        With CType(ogcbalance.DataSource, DataTable)
    '            .AcceptChanges()
    '            _balDt = .Copy
    '        End With

    '        _markDt = ogcmark.DataSource
    '        _jobbalDt = ogcjobprodbal.DataSource

    '        With CType(Me.ogcmark.DataSource, DataTable)
    '            .AcceptChanges()
    '            For Each _Rbal As DataRow In .Rows
    '                For Each _Col As DataColumn In .Columns
    '                    Select Case _Col.ColumnName.ToString.ToUpper
    '                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper
    '                        Case Else
    '                            For Each _mR As DataRow In _dtprod.Select("FNHSysPartId='" & _Rbal!FNHSysPartId.ToString & "' AND FTSizeBreakDown ='" & _Col.ColumnName.ToString & "'")

    '                                _Rbal.Item(_Col.ColumnName.ToString) = _mR!FNQuantity
    '                            Next
    '                    End Select
    '                Next
    '            Next

    '        End With

    '    Catch ex As Exception
    '    End Try

    '    CType(ogcmark.DataSource, DataTable).AcceptChanges()

    'End Sub

    'Private Function PROC_SAVECutOrder() As Boolean
    '    Dim _Qry As String = ""
    '    Dim _QryCheckSeq As String = ""
    '    Dim bRet As Boolean = False
    '    Dim Maxleng As String = ""

    '    Try
    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
    '        _Qry &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

    '        _Qry &= vbCrLf & " WHERE  FTOrderNo='" & FTOrderNo.Text & "' AND FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '        _Qry &= vbCrLf & "AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
    '            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FTOrderProdNo, FTColorWay, FNHSysUnitSectId, FNHSysCmpId) "
    '            _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & ",'" & FTOrderNo.Text & "'"
    '            _Qry &= vbCrLf & ",'" & _FTSubOrderNo & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
    '            _Qry &= vbCrLf & ",'" & _FTColorway & "'"
    '            _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
    '            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID)

    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If
    '        End If

    '        Call SaveDetail(FTOrderNo.Text)

    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return True

    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Private Function SaveDetail(ByVal _Key As String) As Boolean 'save detail in gridview to DB
        'Try
        '    Dim _Qry As String = ""
        '    Dim _Seq As Integer = 0


        '    If Not (ogcmark.DataSource Is Nothing) Then
        '        _Seq = 0
        '        Dim dt As DataTable
        '        With CType(ogcmark.DataSource, DataTable)
        '            .AcceptChanges()
        '            dt = .Copy
        '        End With

        '        For Each R As DataRow In dt.Rows
        '            _Seq += +1

        '            For Each Col As DataColumn In dt.Columns
        '                Select Case Col.ColumnName.ToString.ToUpper
        '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper
        '                    Case Else
        '                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
        '                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
        '                        _Qry &= vbCrLf & ",FNQuantity=" & CInt("0" & R(Col.ColumnName.ToString).ToString)

        '                        _Qry &= vbCrLf & "WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' AND FNHSysPartId = " & R!FNHSysPartId & ""
        '                        _Qry &= vbCrLf & " AND FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)

        '                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysUnitSectId, FNHSysPartId, FTSizeBreakDown, FNQuantity"
        '                            _Qry &= vbCrLf & ")"
        '                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
        '                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
        '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'"
        '                            _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
        '                            _Qry &= vbCrLf & "," & R!FNHSysPartId
        '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
        '                            _Qry &= vbCrLf & "," & CInt("0" & R(Col.ColumnName.ToString).ToString)
        '                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                                HI.Conn.SQLConn.Tran.Rollback()
        '                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                                Return False
        '                            End If
        '                        End If
        '                End Select
        '            Next
        '        Next
        '        '_Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '        '_Qry &= vbCrLf & "WHERE FTOrderProdNo='" & _Key & "' "
        '        'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
        '    End If
        'Catch ex As Exception
        'End Try
        'Return True
    End Function

    Private Function DeleteData() As Boolean 'delete data from DB
        'Try
        '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
        '    HI.Conn.SQLConn.SqlConnectionOpen()
        '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        '    Dim _Str As String
        '    _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
        '    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '        HI.Conn.SQLConn.Tran.Rollback()
        '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '        Return False
        '    End If

        '    _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
        '    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

        '    HI.Conn.SQLConn.Tran.Commit()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '    Return True

        'Catch ex As Exception
        '    HI.Conn.SQLConn.Tran.Rollback()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '    Return False
        'End Try
    End Function

    Private _StateSumGrid As Boolean
    Private Sub SumGrid()
        _StateSumGrid = True
        CType(ogcmark.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            _Total = 0
            With Me.ogvmark

                For I As Integer = 0 To .RowCount - 1
                    _Total = 0
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FDSaveDate".ToUpper, "FTUnitSectCode".ToUpper
                            Case Else
                                If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                    _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
                                Else
                                    _Total = _Total + 0
                                End If
                        End Select

                    Next

                    .SetRowCellValue(I, "Total", _Total)
                Next

            End With

        Catch ex As Exception
        End Try

        CType(ogcmark.DataSource, DataTable).AcceptChanges()

        _StateSumGrid = False
    End Sub

    Private Sub Calculate_balance()
        _StateSumGrid = True
        CType(ogcmark.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            Dim _balDt As DataTable
            Dim _markDt As DataTable
            Dim _jobbalDt As DataTable
            _Total = 0

            With CType(ogcbalance.DataSource, DataTable)
                .AcceptChanges()
                _balDt = .Copy
            End With

            _markDt = ogcmark.DataSource
            _jobbalDt = ogcjobprodbal.DataSource

            With CType(Me.ogcbalance.DataSource, DataTable)
                .AcceptChanges()
                For Each _Row As DataRow In _jobbalDt.Rows
                    For Each _Rbal As DataRow In .Rows
                        For Each _mR As DataRow In _markDt.Select("FNHSysPartId='" & _Rbal!FNHSysPartId.ToString & "'")
                            For Each _Col As DataColumn In .Columns
                                Select Case _Col.ColumnName.ToString.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FNHSysUnitSectId".ToUpper, "FTOrderProdNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FDSaveDate".ToUpper, "FTUnitSectCode".ToUpper
                                    Case Else
                                        _Rbal.Item(_Col.ColumnName.ToString) = _Row.Item(_Col.ColumnName.ToString)

                                End Select
                            Next
                        Next
                    Next
                Next

            End With



            With CType(Me.ogcbalance.DataSource, DataTable)
                .AcceptChanges()
                For Each _Row As DataRow In _jobbalDt.Rows
                    For Each _Rbal As DataRow In .Rows
                        For Each _mR As DataRow In _markDt.Select("FNHSysPartId='" & _Rbal!FNHSysPartId.ToString & "'")
                            For Each _Col As DataColumn In .Columns
                                Select Case _Col.ColumnName.ToString.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FNHSysUnitSectId".ToUpper, "FTOrderProdNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FDSaveDate".ToUpper, "FTUnitSectCode".ToUpper
                                    Case Else
                                        _Rbal.Item(_Col.ColumnName.ToString) = _Rbal.Item(_Col.ColumnName.ToString) - _mR.Item(_Col.ColumnName.ToString)

                                End Select
                            Next
                        Next
                    Next
                Next

            End With

        Catch ex As Exception
        End Try

        CType(ogcmark.DataSource, DataTable).AcceptChanges()

        Call SumGridbal()
        _StateSumGrid = False
    End Sub

    Private Sub SumGridbal()
        _StateSumGrid = True
        CType(ogcbalance.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            _Total = 0
            With Me.ogvbalance

                For I As Integer = 0 To .RowCount - 1
                    _Total = 0
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper
                            Case Else
                                If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                    _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
                                Else
                                    _Total = _Total + 0
                                End If
                        End Select

                    Next

                    .SetRowCellValue(I, "Total", _Total)
                Next

            End With

        Catch ex As Exception
        End Try

        CType(ogcbalance.DataSource, DataTable).AcceptChanges()

        _StateSumGrid = False
    End Sub

    Private Sub ClearGrid(Optional Prod As Boolean = False)

        ogcmark.DataSource = Nothing
        'With Me.ogvjobprod
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next
        'End With

        'With Me.ogvsub
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next
        'End With

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

        With Me.ogvbalance
            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next
        End With

        With Me.ogvmark
            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FDSaveDate".ToUpper, "FTUnitSectCode".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next
        End With

        'ogcmainmark.DataSource = Nothing
        'ogcsubmark.DataSource = Nothing
        'ogcjobprod.DataSource = Nothing
        'ogcsub.DataSource = Nothing

        If Not (Prod) Then
            Me.otbjobprod.TabPages.Clear()
        End If

        'Me.otbsuborder.TabPages.Clear()
        'Me.otbtable.TabPages.Clear()

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderProdDataInfo(FTOrderNo.Text)

            If Not (otbjobprod.SelectedTabPage Is Nothing) Then
                Call LoadOrderProdCutItem(otbjobprod.SelectedTabPage.Name.ToString)
            End If

            'Me.otbdetail.SelectedTabPageIndex = 0
        End If
    End Sub

    Private Sub otbjobprod_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbjobprod.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbjobprod_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbjobprod.SelectedTabPage Is Nothing) Then

                    'Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdBreakDown(otbjobprod.SelectedTabPage.Name.ToString)
                    'Call LoadOrderProdMainMark(otbjobprod.SelectedTabPage.Name.ToString)
                    Call LoadOrderProdCutItem(otbjobprod.SelectedTabPage.Name.ToString)

                    'Call LoadOrderProdMarkCutting(otbjobprod.SelectedTabPage.Name.ToString)

                Else
                    Call ClearGrid(True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wItemCutAmount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Private Sub ReposCaleditWeight_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposCaleditWeight.EditValueChanging
        Try
            Dim _NewValue As Double = e.NewValue
            Dim _OrgValue As Double = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""
            Dim _Dt As DataTable = ogcmark.DataSource
            Dim _PartId As Integer = 0

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")
                    _PartId = .GetFocusedRowCellValue("FNHSysPartId")

                    If Not (_StateSumGrid) Then
                        Dim _ColName As String = .FocusedColumn.FieldName.ToString
                        With CType(ogcmark.DataSource, DataTable)
                            .AcceptChanges()

                            For Each R As DataRow In .Rows
                                If (R!FNHSysPartId = _PartId) Then
                                    R.Item(_ColName) = _NewValue
                                    Exit For
                                End If
                            Next

                        End With

                        ' ogvpackdetailWeight.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))

                        If Not (ogcmark.DataSource Is Nothing) Then
                            Call SumGrid()
                            Call Calculate_balance()
                        End If

                    End If

                End With
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Call ClearGrid()
        FTOrderNo.Focus()
        FTOrderNo.SelectAll()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        'PROC_SAVECutOrder()
        Dim tmpMsgSpecial As String = ""
        'If (FNHSysUnitSectId.Text <> "") Then

        '    If PROC_SAVECutOrder() Then
        '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

        '    Else
        '        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '    End If
        'Else
        '    HI.MG.ShowMsg.mProcessError(1711211727, "กรุณาใส่สังกัด ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        '    Me.FNHSysStyleId.Focus()
        'End If

    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs)
        'Call LoadOrderCutItemUnitSect()
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTOrderNo.Text, Me.Text) = False Then
            Exit Sub
        End If

        'If (FNHSysUnitSectId.Text <> "") Then
        '    Call DeleteData()
        '    Call ocmclear_Click(Nothing, Nothing)
        'Else
        '    HI.MG.ShowMsg.mProcessError(1000000006, "ไม่สามารถแก้ไขเอกสารนี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        'End If
    End Sub

    Private Sub ocmAddDT_Click(sender As Object, e As EventArgs) Handles ocmAddDT.Click
        If Me.FTOrderNo.Properties.Tag.ToString().Trim() <> "" Then

            _AddItem = New wAddCutItem(Me.FTOrderNo.Properties.Tag.ToString().Trim(), Me.otbjobprod.SelectedTabPage.Name.ToString)

            HI.TL.HandlerControl.AddHandlerObj(_AddItem)

            Dim oSysLang As New HI.ST.SysLanguage

            Try
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _AddItem.Name.ToString.Trim, _AddItem)
            Catch ex As Exception
            End Try

            Call HI.ST.Lang.SP_SETxLanguage(_AddItem)

            _AddItem.ShowDialog()
            Call LoadOrderProdCutItem(otbjobprod.SelectedTabPage.Name.ToString)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNo_lbl.Text)
            Me.FTOrderNo.Focus()
        End If
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If (FTOrderNo.Text <> "") Then
                'If (FDFromDate.Text <> "") Then
                Call LoadOrderProdCutItem(otbjobprod.SelectedTabPage.Name.ToString)
                'Else
                '    HI.MG.ShowMsg.mProcessError(1587889900, "กรุณาใส่วันที่ต้องการหาข้อมูล", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                '    Me.FDFromDate.Focus()
                'End If
            Else
                HI.MG.ShowMsg.mProcessError(1587889899, "กรุณาใส่เลขที่ใบสั่งผลิต", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Me.FTOrderNo.Focus()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvmark_DoubleClick(sender As Object, e As EventArgs) Handles ogvmark.DoubleClick
        Try
            With ogvmark
                If .FocusedRowHandle < -1 Or .RowCount <= 0 Then Exit Sub

                If Me.FTOrderNo.Properties.Tag.ToString().Trim() <> "" Then

                    _AddItem = New wAddCutItem(Me.FTOrderNo.Properties.Tag.ToString().Trim(), Me.otbjobprod.SelectedTabPage.Name.ToString)

                    HI.TL.HandlerControl.AddHandlerObj(_AddItem)

                    Dim oSysLang As New HI.ST.SysLanguage

                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _AddItem.Name.ToString.Trim, _AddItem)
                    Catch ex As Exception
                    End Try

                    Call HI.ST.Lang.SP_SETxLanguage(_AddItem)

                    Dim _UnitSectCode As String = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTUnitSectCode").ToString)
                    Dim _Part As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString)
                    Dim _partCode As String = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTPartName").ToString)
                    Dim _Colorway As String = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString)
                    Dim _UnitSectId As Integer = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysUnitSectId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect with(nolock) where FTUnitSectCode='" & _UnitSectCode & "'", Conn.DB.DataBaseName.DB_MASTER, 0)

                    With _AddItem
                        .UpdateState = True
                        .PartId = _Part
                        .FNHSysUnitSectId.Properties.Tag = _UnitSectId
                        .FNHSysUnitSectId.Text = _UnitSectCode
                        '.FTOrderNo_EditValueChanged(Nothing, Nothing)
                        '.LoadOrderCutItemUnitSect()
                    End With

                    _AddItem.ShowDialog()
                    Call LoadOrderProdCutItem(otbjobprod.SelectedTabPage.Name.ToString)
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNo_lbl.Text)
                    Me.FTOrderNo.Focus()
                End If


            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
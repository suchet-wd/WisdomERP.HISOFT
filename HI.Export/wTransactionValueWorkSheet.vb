Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports System.Net
Imports DevExpress.XtraGrid.Views.Grid

Public Class wTransactionValueWorkSheet

    Private _StateClear As Boolean = False
    Private StateFormLoad As Boolean = False

    Sub New()
        StateFormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitGrid()
    End Sub

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNPriceNew"
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNPriceNew"
        Dim sFieldGrpSumAmt As String = ""

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvdetail

            .ClearGrouping()
            .ClearDocument()

            .Columns.ColumnByFieldName("FTMattypeName").Group()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n5}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n5})")
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
            .OptionsView.ShowAutoFilterRow = False
            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogvactual

            .ClearGrouping()
            .ClearDocument()

            .Columns.ColumnByFieldName("FTMattypeName").Group()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n5}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n5})")
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
            .OptionsView.ShowAutoFilterRow = False
            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogvbreakdown
            .OptionsView.ShowAutoFilterRow = False
        End With

    End Sub

    Private Sub ClearGrid(Optional Prod As Boolean = False)

        With Me.ogvbreakdownnet

            .OptionsView.ShowAutoFilterRow = False

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper,
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

        End With

        With Me.ogvbreakdown

            .OptionsView.ShowAutoFilterRow = False

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper,
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

        End With

        With Me.ogvbreakdownsurcharge

            .OptionsView.ShowAutoFilterRow = False

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper,
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

        End With

    End Sub


    Private Sub LoadOrderBreakDown(Key As Object)
        If _StateClear Then Exit Sub

        Dim _dt As System.Data.DataTable
        Dim _dtsurcharge As System.Data.DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _FNHSysContinentId As Integer = 0
        Dim _FNHSysCountryId As Integer = 0
        Dim _FNHSysProvinceId As Integer = 0
        Dim _FNHSysShipModeId As Integer = 0
        Dim _FNHSysShipPortId As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_BreakDown_TVW '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.ogvbreakdown

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dt Is Nothing) Then

                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                        Case Else
                            _colcount = _colcount + 1

                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn

                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                                If Not (Col.ColumnName.ToString = "Total") Then
                                    .ColumnEdit = ReposPrice
                                End If

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n4}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 70
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n4}"

                    End Select

                Next
            End If
        End With

        Me.ogcbreakdown.DataSource = _dt.Copy
        ogcbreakdown.Refresh()



        '_Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_BreakDown_TVW_Surcharge '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        '_dtsurcharge = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtsurcharge = _dt.Copy
        With Me.ogvbreakdownsurcharge

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dtsurcharge Is Nothing) Then

                For Each Col As DataColumn In _dtsurcharge.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                        Case Else
                            _colcount = _colcount + 1

                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn

                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                                If Not (Col.ColumnName.ToString = "Total") Then
                                    .ColumnEdit = ReposPrice
                                End If

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n4}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far


                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 70
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n4}"

                    End Select

                Next
            End If
        End With


        For Each R As DataRow In _dtsurcharge.Rows
            For Each Col As DataColumn In _dtsurcharge.Columns
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                    Case Else
                        R.Item(Col.ColumnName.ToString) = 0

                End Select
            Next

        Next

        Me.ogcbreakdownsurcharge.DataSource = _dtsurcharge.Copy
        ogcbreakdownsurcharge.Refresh()


        'Dim _Filter As String

        '_dt.BeginInit()
        'For Each R As DataRow In _dt.Rows
        '    For Each Col As DataColumn In _dt.Columns
        '        Select Case Col.ColumnName.ToString.ToUpper
        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
        '            Case Else
        '                _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'  AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"

        '                If _dtsurcharge.Select(_Filter).Length > 0 Then

        '                    For Each Rx As DataRow In _dtsurcharge.Select(_Filter)

        '                        R.Item(Col.ColumnName.ToString) = Val(R.Item(Col.ColumnName.ToString)) + Val(Rx.Item(Col.ColumnName.ToString))

        '                        Exit For
        '                    Next

        '                End If

        '        End Select
        '    Next

        'Next
        '_dt.EndInit()


        With Me.ogvbreakdownnet

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dt Is Nothing) Then

                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                        Case Else
                            _colcount = _colcount + 1

                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn

                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                                If Not (Col.ColumnName.ToString = "Total") Then
                                    .ColumnEdit = ReposPrice
                                End If

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n4}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far


                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 70
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n4}"

                    End Select

                Next

            End If

        End With

        Me.ogcbreakdownnet.DataSource = _dt.Copy
        ogcbreakdownnet.Refresh()
        _dt.Dispose()
        _dtsurcharge.Dispose()
    End Sub


    Public Sub LoadOrderDataInfo(ByVal Key As Object)

        Dim _Qry As String = ""
        Dim _Lang As String = ""
        Dim _ChragePer As Double = 0

        _ChragePer = FNGenderChargePer.Value

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Lang = "TH"
        Else
            _Lang = "EN"
        End If

        Dim _dt As New System.Data.DataTable
        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_GET_TRANSACTION_VALUE_WORKSHEET_HANGER '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "','" & _Lang & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogcdetail.DataSource = _dt.Copy

        ogvdetail.ExpandAllGroups()
        ogvdetail.RefreshData()

        _Qry = " SELECT TOP 1 SS.FTStyleCode "
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS SS WITH(NOLOCK) ON O.FNHSysStyleId = SS.FNHSysStyleId "
        _Qry &= vbCrLf & "  WHERE O.FTOrderNo IN ("
        _Qry &= vbCrLf & " SELECT DISTINCT O.FTOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        _Qry &= vbCrLf & " )"

        FNHSysStyleId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

        Dim _FNFabricAsset As Double = 0.0
        Dim _FNAccessoryAsset As Double = 0.0
        Dim _FNHanger As Double = 0.0
        Dim _FNCMP As Double = 0.0
        Dim _FNCostTransport As Double = 0.0
        Dim _dtstyle As System.Data.DataTable
        Dim _FNFabricImport As Double = 0.0
        Dim _FNAccessoryImport As Double = 0.0
        Dim _dtcostimport As System.Data.DataTable
        Dim _FNExchangeRate As Double = 1


        For Each R As DataRow In _dt.Select("FNExchangeRate>0")
            _FNExchangeRate = Val(R!FNExchangeRate)
            Exit For
        Next

        For Each R As DataRow In _dt.Select("FTStateHanger='1'")
            _FNHanger = _FNHanger + CDbl(Format(Val(R!FNPriceNew), "0.00"))
        Next

        For Each R As DataRow In _dt.Select("FNMerMatType=0 AND FTStateHanger<>'1'")
            _FNFabricAsset = _FNFabricAsset + CDbl(Format(Val(R!FNPriceNew), "0.00"))
        Next

        For Each R As DataRow In _dt.Select("FNMerMatType<>0 AND FTStateHanger<>'1'")
            _FNAccessoryAsset = _FNAccessoryAsset + CDbl(Format(Val(R!FNPriceNew), "0.00"))
        Next


        _Qry = "  SELECT  TOP 1  A.FTPORef, ST.FNCM, ST.FNCMDisPer, ST.FNCMDisAmt, ST.FNNetCM, ST.FNCostTransport"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_CMPPrice_By_Style_And_Season AS ST  ON A.FNHSysStyleId = ST.FNHSysStyleId AND A.FNHSysSeasonId=ST.FNHSysSeasonId"
        ' _Qry &= vbCrLf & " WHERE  (A.FTPORef = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "')"
        _Qry &= vbCrLf & " WHERE A.FTOrderNo IN ("
        _Qry &= vbCrLf & " SELECT O.FTOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        _Qry &= vbCrLf & " GROUP BY O.FTOrderNo"
        _Qry &= vbCrLf & " )"
        _dtstyle = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dtstyle.Rows
            _FNCMP = CDbl(Format(Val(R!FNNetCM.ToString), "0.00"))

            '_Qry = "SELECT TOP 1 FNNetCM "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTCMPrice AS ZX WITH(NOLOCK)   "
            '_Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'   "
            '_Qry &= vbCrLf & " AND  FNHSysStyleId IN ("
            '_Qry &= vbCrLf & " SELECT O.FNHSysStyleId"
            '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
            '_Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
            '_Qry &= vbCrLf & " GROUP BY O.FNHSysStyleId"
            '_Qry &= vbCrLf & " )"

            '_FNCMP = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, _FNCMP.ToString))

            _FNCostTransport = CDbl(Format(Val(R!FNCostTransport.ToString), "0.00"))
        Next

        _Qry = " SELECT TOP 1  "
        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
        _Qry &= vbCrLf & ",FTCustomerPO,FNFabricAsset, FNAccessoryAsset, FNCMP, FNCostTransport"
        _Qry &= vbCrLf & ", FNFirstSale, FNHanger, FNNetFirstSale, "
        _Qry &= vbCrLf & " FNFabricAssetAct, FNAccessoryAssetAct, FNCMPAct, FNCostTransportAct, FNFirstSaleAct, FNHangerAct, FNNetFirstSaleAct,ISNULL(FNNetFirstSaleCostSheet,0) AS FNNetFirstSaleCostSheet"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (A. FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "')"

        _dtstyle = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim _FNNetFirstSale As Double = 0.0
        Dim _FNNetFirstSaleAct As Double = 0.0
        Dim _FNNetFirstSaleCSCurrent As Double = 0.0

        Me.FTStateAccept.Checked = False
        FTStateAcceptBy.Text = ""
        FTStateAcceptByDateTime.Text = ""

        For Each R As DataRow In _dtstyle.Rows

            _FNCostTransport = CDbl(Format(Val(R!FNCostTransport.ToString), "0.00"))
            _FNNetFirstSale = CDbl(Format(Val(R!FNNetFirstSale.ToString), "0.00"))
            _FNNetFirstSaleAct = CDbl(Format(Val(R!FNNetFirstSaleAct.ToString), "0.00"))
            _FNNetFirstSaleCSCurrent = CDbl(Format(Val(R!FNNetFirstSaleCostSheet.ToString), "0.00"))

            If R!FTUpdUser.ToString = "" Then

                FTStateAcceptBy.Text = R!FTInsUser.ToString
                FTStateAcceptByDateTime.Text = HI.UL.ULDate.ConvertEN(R!FDInsDate.ToString) & "  " & R!FTInsTime.ToString

            Else

                FTStateAcceptBy.Text = R!FTUpdUser.ToString
                FTStateAcceptByDateTime.Text = HI.UL.ULDate.ConvertEN(R!FDUpdDate.ToString) & "  " & R!FTUpdTime.ToString

            End If

            Me.FTStateAccept.Checked = True
        Next

        If _ChragePer <> 0 Then
            _FNHanger = _FNHanger + CDbl(Format(((_FNHanger * _ChragePer) / 100.0), "0.00"))
            _FNFabricAsset = _FNFabricAsset + CDbl(Format(((_FNFabricAsset * _ChragePer) / 100.0), "0.00"))
            _FNAccessoryAsset = _FNAccessoryAsset + CDbl(Format(((_FNAccessoryAsset * _ChragePer) / 100.0), "0.00"))
            _FNCostTransport = _FNCostTransport + CDbl(Format(((_FNCostTransport * _ChragePer) / 100.0), "0.00"))
            _FNCMP = _FNCMP + CDbl(Format(((_FNCMP * _ChragePer) / 100.0), "0.00"))
        End If


        Me.FNFabricAsset.Value = _FNFabricAsset
        Me.FNAccessoryAsset.Value = _FNAccessoryAsset
        Me.FNHanger.Value = _FNHanger
        Me.FNCMP.Value = _FNCMP
        Me.FNCostTransport.Value = _FNCostTransport

        FNNetFirstSaleCurrent.Value = _FNNetFirstSale
        FNNetFirstSaleActCurrent.Value = _FNNetFirstSaleAct
        FNNetFirstSaleCSCurrent.Value = _FNNetFirstSaleCSCurrent

        _Qry = "  SELECT A.*"
        _Qry &= vbCrLf & "  ,B.FNNetCost"
        _Qry &= vbCrLf & " FROM("
        _Qry &= vbCrLf & "   SELECT P.FNHSysSuplId"
        _Qry &= vbCrLf & " , A.FTInvoiceNo"
        _Qry &= vbCrLf & " , MAX(MM.FNMerMatType) AS FNMerMatType"
        _Qry &= vbCrLf & " , SUM(Convert(numeric(18,2),B.FNNetAmt* A.FNExchangeRate)) AS FNCostRcvAmt"

        _Qry &= vbCrLf & " , ISNULL((SELECT SUM(FNCostRcvAmt) AS FNCostRcvAmt FROM( SELECT XP.FNHSysSuplId, XA.FTInvoiceNo, MAX(XMM.FNMerMatType) AS FNMerMatType,SUM(Convert(numeric(18,2),XB.FNNetAmt* XA.FNExchangeRate)) AS FNCostRcvAmt"
        _Qry &= vbCrLf & "   FROM			[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS XA  WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS XB  WITH(NOLOCK) ON XA.FTReceiveNo = XB.FTReceiveNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS XIM  WITH(NOLOCK)  ON XB.FNHSysRawMatId = XIM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS XMM  WITH(NOLOCK)  ON XIM.FTRawMatCode = XMM.FTMainMatCode INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS XP  WITH(NOLOCK)  ON XA.FTPurchaseNo = XP.FTPurchaseNo"
        _Qry &= vbCrLf & " WHERE XP.FNHSysSuplId=P.FNHSysSuplId"
        _Qry &= vbCrLf & "      AND XA.FTInvoiceNo = A.FTInvoiceNo"
        _Qry &= vbCrLf & "  GROUP BY  XP.FNHSysSuplId,XA.FTInvoiceNo"
        _Qry &= vbCrLf & "   ) AS XX "
        '   _Qry &= vbCrLf & "  --WHERE   XX.FNMerMatType = MAX(MM.FNMerMatType)"
        _Qry &= vbCrLf & " ),0) AS FNTotalCostRcvAmt"

        _Qry &= vbCrLf & "   FROM			[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A  WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B  WITH(NOLOCK) ON A.FTReceiveNo = B.FTReceiveNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM  WITH(NOLOCK)  ON B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM  WITH(NOLOCK)  ON IM.FTRawMatCode = MM.FTMainMatCode INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK)  ON A.FTPurchaseNo = P.FTPurchaseNo"

        _Qry &= vbCrLf & " WHERE B.FTOrderNo IN ("
        _Qry &= vbCrLf & " SELECT O.FTOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        _Qry &= vbCrLf & " GROUP BY O.FTOrderNo"
        _Qry &= vbCrLf & " )"

        _Qry &= vbCrLf & " GROUP BY P.FNHSysSuplId, A.FTInvoiceNo) AS A INNER JOIN (SELECT FTInvoiceNo"
        _Qry &= vbCrLf & "		       , (ISNULL(FNExceedCost,0) + ISNULL(FNPFICost,0) + ISNULL(FNDHLCost,0) + ISNULL(FNUPSCost,0) + ISNULL(FNFreightCost,0) + ISNULL(FNOther1Cost,0) + ISNULL(FNOther2Cost,0) + ISNULL(FNDutyCost,0)) AS FNNetCost, FNHSysSuplId"
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " ) AS B ON A.FTInvoiceNo = B.FTInvoiceNo AND A.FNHSysSuplId = B.FNHSysSuplId "
        _dtcostimport = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each R As DataRow In _dtcostimport.Rows
            Select Case Val(R!FNMerMatType.ToString)
                Case 0
                    If Val(R!FNTotalCostRcvAmt) = Val(R!FNCostRcvAmt) Then
                        _FNFabricImport = _FNFabricImport + CDbl(Format(Val(R!FNNetCost.ToString), "0.00"))
                    Else
                        _FNFabricImport = _FNFabricImport + Double.Parse(Format(((Val(R!FNNetCost.ToString) * Val(R!FNCostRcvAmt)) / Val(R!FNTotalCostRcvAmt)), "0.00"))
                    End If

                Case Else
                    If Val(R!FNTotalCostRcvAmt) = Val(R!FNCostRcvAmt) Then
                        _FNAccessoryImport = _FNAccessoryImport + CDbl(Format(Val(R!FNNetCost.ToString), "0.00"))

                    Else
                        _FNAccessoryImport = _FNAccessoryImport + Double.Parse(Format(((Val(R!FNNetCost.ToString) * Val(R!FNCostRcvAmt)) / Val(R!FNTotalCostRcvAmt)), "0.00"))
                    End If
            End Select
        Next

        Dim _GOderQty As Double = 0

        _Qry = " SELECT SUM(S.FNGrandQuantity)"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE O.FTOrderNo IN ("
        _Qry &= vbCrLf & " SELECT DISTINCT O.FTOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        _Qry &= vbCrLf & " )"
        _GOderQty = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

        If _GOderQty > 0 Then
            _FNFabricImport = Double.Parse(Format((_FNFabricImport / _GOderQty) / _FNExchangeRate, "0.00"))
            _FNAccessoryImport = Double.Parse(Format((_FNAccessoryImport / _GOderQty) / _FNExchangeRate, "0.00"))
        Else
            _FNFabricImport = 0
            _FNAccessoryImport = 0
        End If

        If _ChragePer <> 0 Then
            _FNFabricImport = _FNFabricImport + CDbl(Format(((_FNFabricImport * _ChragePer) / 100.0), "0.00"))
            _FNAccessoryImport = _FNAccessoryImport + CDbl(Format(((_FNAccessoryImport * _ChragePer) / 100.0), "0.00"))

        End If

        Me.FNFabImport.Value = _FNFabricImport
        Me.FNAccImport.Value = _FNAccessoryImport

        Call Calculate()
        ' Call LoadOrderDataActualInfo(Key)
        Call LoadOrderDataCostSheet(Key)

        _Spls.Close()
    End Sub

    Public Sub LoadHistoryFirstPriceXMLInfo(ByVal Key As Object)
        Dim _Qry As String = ""
        Dim _dt As System.Data.DataTable

        _Qry = "  Select A.FTCustomerPO"
        _Qry &= vbCrLf & "   , A.FTInvoiceNo"
        _Qry &= vbCrLf & " , CASE WHEN ISDATE(A.FDInvoiceDate) = 1 THEN Convert(Datetime,A.FDInvoiceDate) ELSE NULL END AS FDInvoiceDate"
        _Qry &= vbCrLf & " , C.FTInvoiceExportNo"
        _Qry &= vbCrLf & " , CASE WHEN ISDATE(C.FDInvoiceExportDate) = 1 THEN Convert(Datetime,C.FDInvoiceExportDate) ELSE NULL END AS  FDInvoiceExportDate"
        _Qry &= vbCrLf & " , SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " , B.FNFirstPrice"
        _Qry &= vbCrLf & " , SUM(Convert(numeric(18,2),B.FNQuantity * B.FNFirstPrice)) AS FNNetAmt,ISNULL(A.FTStateHanger,'0') AS FTStateHanger"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS B WITH(NOLOCK) ON A.FTCustomerPO = B.FTCustomerPO AND A.FTInvoiceNo = B.FTInvoiceNo INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS C WITH(NOLOCK)  ON A.FTCustomerPO = C.FTCustomerPO AND A.FTInvoiceNo = C.FTInvoiceNo"
        _Qry &= vbCrLf & "  WHERE  A.FTCustomerPO='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim()) & "'"
        _Qry &= vbCrLf & " GROUP BY A.FTCustomerPO"
        _Qry &= vbCrLf & " , A.FTInvoiceNo"
        _Qry &= vbCrLf & " , A.FDInvoiceDate"
        _Qry &= vbCrLf & " , C.FTInvoiceExportNo"
        _Qry &= vbCrLf & " , C.FDInvoiceExportDate"
        _Qry &= vbCrLf & " , B.FNFirstPrice,ISNULL(A.FTStateHanger,'0')"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)


        Me.ogchistory.DataSource = _dt.Copy
        Me.ogchistory.Refresh()
    End Sub

    Private Sub LoadListOrderInfo(ByVal _FTPORef As String)
        Dim _Str As String = ""

        _Str = "SELECT '0' AS FNSelect"
        _Str &= vbCrLf & "  ,A.FTOrderNo"
        _Str &= vbCrLf & "  ,SEAS.FTSeasonCode,ISNULL(PMC.FTVenderPramCode,'') AS FTPGMCode  "
        _Str &= vbCrLf & "  ,CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) "
        _Str &= vbCrLf & "         ELSE '' END AS FDOrderDate, ISNULL"
        _Str &= vbCrLf & "                  ((SELECT     CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate"
        _Str &= vbCrLf & "                      FROM         (SELECT     X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate"
        _Str &= vbCrLf & "                                             FROM          HITECH_MERCHAN.dbo.TMERTOrder AS X INNER JOIN"
        _Str &= vbCrLf & "                                                                    HITECH_MERCHAN.dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo"
        _Str &= vbCrLf & "                                             GROUP BY X.FTOrderNo) AS L1"
        _Str &= vbCrLf & "                      WHERE     (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, A.FNHSysCustId, C.FTCustCode, C.FTCustNameEN AS FTCustName"
        _Str &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId"
        _Str &= vbCrLf & "       INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId  "
        _Str &= vbCrLf & "  	 LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON "
        _Str &= vbCrLf & "  	 A.FNHSysSeasonId = SEAS.FNHSysSeasonId"
        _Str &= vbCrLf & "  	 LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PMC WITH(NOLOCK)  ON  A.FNHSysVenderPramId=PMC.FNHSysVenderPramId"
        _Str &= vbCrLf & " WHERE A.FTOrderNo IN ("
        _Str &= vbCrLf & " SELECT O.FTOrderNo"
        _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Str &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        _Str &= vbCrLf & " GROUP BY O.FTOrderNo"
        _Str &= vbCrLf & " ) AND A.FNJobState=1 "
        _Str &= vbCrLf & "   ORDER BY  A.FTOrderNo"

        Dim dt As System.Data.DataTable
        dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
        ogvOrderList.OptionsView.ShowAutoFilterRow = False

        Me.GridOrderList.DataSource = dt.Copy

        Me.GridOrderList.Refresh()

        Try

            _Str = "   Select Max(ISNULL(B.FNChargePer,0)) AS FNChargePer"
            _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS A WITH(NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender AS B WITH(NOLOCK) ON A.FNHSysGenderId = B.FNHSysGenderId"
            _Str &= vbCrLf & " WHERE A.FTOrderNo IN ("
            _Str &= vbCrLf & " SELECT O.FTOrderNo"
            _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
            _Str &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
            _Str &= vbCrLf & " GROUP BY O.FTOrderNo"
            _Str &= vbCrLf & " ) "

            FNGenderChargePer.Value = Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "0"))

        Catch ex As Exception
        End Try

        Call ClearGrid()
        Call LoadOrderBreakDown(_FTPORef)
    End Sub

    Public Sub CheckImportCost(ByVal Key As Object)
        'Dim _Qry As String = ""
        'Dim _Dt As System.Data.DataTable

        'If Me.FTCustomerPO.Text <> "" Then

        '    _Qry = " SELECT SUM(1) AS FNTotal"
        '    _Qry &= vbCrLf & "  ,SUM( CASE WHEN ISNULL(C.FNExceedCost,0)"
        '    _Qry &= vbCrLf & "  + ISNULL(C.FNPFICost,0)"
        '    _Qry &= vbCrLf & "   + ISNULL(C.FNDHLCost,0)"
        '    _Qry &= vbCrLf & "   +ISNULL( C.FNUPSCost,0)"
        '    _Qry &= vbCrLf & "   + ISNULL(C.FNFreightCost,0)"
        '    _Qry &= vbCrLf & "   + ISNULL(C.FNOther1Cost,0)"
        '    _Qry &= vbCrLf & "   + ISNULL(C.FNOther2Cost,0)"
        '    _Qry &= vbCrLf & "   + ISNULL(C.FNDutyCost,0) > 0 THEN 1 ELSE 0 END) AS FNCostIMport"
        '    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK) INNER JOIN"
        '    _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK) ON A.FTPurchaseNo = R.FTPurchaseNo LEFT OUTER JOIN"
        '    _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceCharge AS C WITH(NOLOCK) ON R.FTInvoiceNo = C.FTInvoiceNo AND A.FNHSysSuplId = C.FNHSysSuplId"
        '    _Qry &= vbCrLf & "  WHERE (A.FNPoState = 1) "
        '    _Qry &= vbCrLf & "  AND A.FTPurchaseNo IN ("
        '    _Qry &= vbCrLf & "SELECT DISTINCT  X.FTPurchaseNo"
        '    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS  X WITH(NOLOCK) "
        '    _Qry &= vbCrLf & "  WHERE  X.FTOrderNo IN ("
        '    _Qry &= vbCrLf & "  SELECT DISTINCT FTOrderNo"
        '    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination"
        '    _Qry &= vbCrLf & " WHERE FTPOref  ='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
        '    _Qry &= vbCrLf & "  ) "
        '    _Qry &= vbCrLf & "  )"

        '    _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)


        '    If _Dt.Rows.Count > 0 Then
        '        If Val(_Dt.Rows(0)!FNCostIMport.ToString) = Val(_Dt.Rows(0)!FNTotal.ToString) Then
        '            FNFabImport.ForeColor = Color.Blue
        '            FNAccImport.ForeColor = Color.Blue
        '        Else
        '            FNFabImport.ForeColor = Color.Red
        '            FNAccImport.ForeColor = Color.Red
        '        End If
        '    Else
        '        FNFabImport.ForeColor = Color.Blue
        '        FNAccImport.ForeColor = Color.Blue
        '    End If

        'Else
        '    FNFabImport.ForeColor = Color.Blue
        '    FNAccImport.ForeColor = Color.Blue
        'End If

    End Sub

    Public Sub LoadOrderDataActualInfo(ByVal Key As Object)
        Dim _Qry As String = ""
        Dim _Lang As String = ""
        Dim _ChragePer As Double = 0
        _ChragePer = FNGenderChargePer.Value

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Lang = "TH"
        Else
            _Lang = "EN"
        End If

        Dim _dt As New System.Data.DataTable
        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_GET_TRANSACTION_VALUE_WORKSHEET_ACTUAL '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "','" & _Lang & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogcactual.DataSource = _dt.Copy

        ogvactual.ExpandAllGroups()
        ogvactual.RefreshData()

        Dim _FNFabricAssetAct As Double = 0.0
        Dim _FNAccessoryAssetAct As Double = 0.0
        Dim _FNHangerAct As Double = 0.0
        Dim _FNCMPAct As Double = 0.0
        Dim _FNCostTransportAct As Double = 0.0
        Dim _dtstyle As System.Data.DataTable

        For Each R As DataRow In _dt.Select("FTStateHanger='1'")
            _FNHangerAct = _FNHangerAct + CDbl(Format(Val(R!FNPriceNew), "0.00"))
        Next

        For Each R As DataRow In _dt.Select("FNMerMatType=0 AND FTStateHanger<>'1'")
            _FNFabricAssetAct = _FNFabricAssetAct + CDbl(Format(Val(R!FNPriceNew), "0.00"))
        Next

        For Each R As DataRow In _dt.Select("FNMerMatType<>0 AND FTStateHanger<>'1'")
            _FNAccessoryAssetAct = _FNAccessoryAssetAct + CDbl(Format(Val(R!FNPriceNew), "0.00"))
        Next

        If _ChragePer <> 0 Then
            _FNHangerAct = _FNHangerAct + CDbl(Format(((_FNHangerAct * _ChragePer) / 100.0), "0.00"))
            _FNFabricAssetAct = _FNFabricAssetAct + CDbl(Format(((_FNFabricAssetAct * _ChragePer) / 100.0), "0.00"))
            _FNAccessoryAssetAct = _FNAccessoryAssetAct + CDbl(Format(((_FNAccessoryAssetAct * _ChragePer) / 100.0), "0.00"))
        End If

        Me.FNFabricAssetAct.Value = _FNFabricAssetAct
        Me.FNAccessoryAssetAct.Value = _FNAccessoryAssetAct
        Me.FNHangerAct.Value = _FNHangerAct

        Call CalculateAct()

    End Sub

    Public Sub LoadOrderDataCostSheet(ByVal Key As Object)

        Dim _Qry As String = ""
        Dim _FTSeasonCode As String = ""
        Dim _dt As System.Data.DataTable
        Dim _ChragePer As Double = 0
        _ChragePer = FNGenderChargePer.Value

        _Qry = " SELECT TOP 1 SS.FTSeasonCode "
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON O.FNHSysSeasonId = SS.FNHSysSeasonId "
        _Qry &= vbCrLf & "  WHERE O.FTOrderNo IN ("
        _Qry &= vbCrLf & " SELECT DISTINCT O.FTOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        _Qry &= vbCrLf & " )"
        _FTSeasonCode = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

        FNFabricAssetCS.Value = 0
        FNAccessoryAssetCS.Value = 0
        FNFirstSaleCS.Value = 0
        FNHangerCS.Value = 0
        FNFabImportCS.Value = 0
        FNAccImportCS.Value = 0
        FNNetFirstSaleCS.Value = 0

        If _FTSeasonCode <> "" Then


            _Qry = "  SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
            _Qry &= vbCrLf & " , FTStyleCode, FTSeason, FNFabricAmt, FNAccessoryAmt, FNImportFabricAmt, FNImportAccessoryAmt"
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text.Trim) & "'"
            _Qry &= vbCrLf & "  AND FTSeason='" & HI.UL.ULF.rpQuoted(_FTSeasonCode) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            If _dt.Rows.Count > 0 Then
                For Each Rxd As DataRow In _dt.Rows

                    FNFabricAssetCS.Value = CDbl(Format(Val(Rxd!FNFabricAmt.ToString), "0.00"))
                    FNAccessoryAssetCS.Value = CDbl(Format(Val(Rxd!FNAccessoryAmt.ToString), "0.00"))
                    FNFirstSaleCS.Value = CDbl(Format(Val(Rxd!FNAccessoryAmt.ToString), "0.00"))
                    FNHangerCS.Value = FNHanger.Value
                    FNFabImportCS.Value = CDbl(Format(Val(Rxd!FNImportFabricAmt.ToString), "0.00"))
                    FNAccImportCS.Value = CDbl(Format(Val(Rxd!FNImportAccessoryAmt.ToString), "0.00"))

                    Exit For
                Next

            Else

                _Qry = "  SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                _Qry &= vbCrLf & " , FTStyleCode, FTSeason, FNFabricAmt, FNAccessoryAmt, FNImportFabricAmt, FNImportAccessoryAmt"
                _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale AS X WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTStyleCode IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1  B.FTStyleCode"
                _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B  WITH(NOLOCK)  ON A.FNHSysStyleIdTo = B.FNHSysStyleId"
                _Qry &= vbCrLf & " WHERE A.FTStyleCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text.Trim) & "')"
                _Qry &= vbCrLf & "  AND FTSeason='" & HI.UL.ULF.rpQuoted(_FTSeasonCode) & "'"

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                For Each Rxd As DataRow In _dt.Rows

                    FNFabricAssetCS.Value = CDbl(Format(Val(Rxd!FNFabricAmt.ToString), "0.00"))
                    FNAccessoryAssetCS.Value = CDbl(Format(Val(Rxd!FNAccessoryAmt.ToString), "0.00"))
                    FNFirstSaleCS.Value = CDbl(Format(Val(Rxd!FNAccessoryAmt.ToString), "0.00"))
                    FNHangerCS.Value = FNHanger.Value
                    FNFabImportCS.Value = CDbl(Format(Val(Rxd!FNImportFabricAmt.ToString), "0.00"))
                    FNAccImportCS.Value = CDbl(Format(Val(Rxd!FNImportAccessoryAmt.ToString), "0.00"))

                    Exit For

                Next

            End If

        End If

        Dim _Surchage As Double = 0

        _Qry = " SELECT   O.FTOrderNo ,SUM(1) AS FNTotal,SUM(FNPrice - ISNULL(FNPriceOrg, FNPrice) ) AS FNSurcharge"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "' AND O.FNJobState=1 "
        _Qry &= vbCrLf & " GROUP BY O.FTOrderNo"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each Rxd As DataRow In _dt.Rows

            _Surchage = CDbl(Format(Val(Rxd!FNSurcharge.ToString) / Val(Rxd!FNTotal.ToString), "0.00"))

            Exit For
        Next

        If _ChragePer <> 0 Then
            _Surchage = _Surchage + CDbl(Format(((_Surchage * _ChragePer) / 100.0), "0.00"))
            FNFabricAssetCS.Value = FNFabricAssetCS.Value + CDbl(Format(((FNFabricAssetCS.Value * _ChragePer) / 100.0), "0.00"))
            FNAccessoryAssetCS.Value = FNAccessoryAssetCS.Value + CDbl(Format(((FNAccessoryAssetCS.Value * _ChragePer) / 100.0), "0.00"))
            FNFabImportCS.Value = FNFabImportCS.Value + CDbl(Format(((FNFabImportCS.Value * _ChragePer) / 100.0), "0.00"))
            FNAccImportCS.Value = FNAccImportCS.Value + CDbl(Format(((FNAccImportCS.Value * _ChragePer) / 100.0), "0.00"))
        End If

        FNSurchargeCS.Value = _Surchage


        Call CalculateCS()

    End Sub

    Private Sub Calculate()
        Me.FNFirstSale.Value = (Me.FNFabricAsset.Value + Me.FNAccessoryAsset.Value + Me.FNCMP.Value + Me.FNCostTransport.Value)
        Me.FNNetFirstSale.Value = (Me.FNFirstSale.Value + Me.FNHanger.Value + Me.FNFabImport.Value + Me.FNAccImport.Value)

    End Sub

    Private Sub CalculateAct()
        Me.FNFirstSaleAct.Value = (Me.FNFabricAssetAct.Value + Me.FNAccessoryAssetAct.Value + Me.FNCMP.Value + Me.FNCostTransport.Value)
        Me.FNNetFirstSaleAct.Value = (Me.FNFirstSaleAct.Value + Me.FNHangerAct.Value + Me.FNFabImport.Value + Me.FNAccImport.Value)

    End Sub

    Private Sub CalculateCS()

        Me.FNFirstSaleCS.Value = (Me.FNFabricAssetCS.Value + Me.FNAccessoryAssetCS.Value + Me.FNCMP.Value + Me.FNCostTransport.Value)
        'Me.FNNetFirstSaleCS.Value = (Me.FNFirstSaleCS.Value + Me.FNHangerCS.Value + Me.FNFabImportCS.Value + Me.FNAccImportCS.Value + FNSurchargeCS.Value)
        Me.FNNetFirstSaleCS.Value = (Me.FNFirstSaleCS.Value + Me.FNHangerCS.Value + Me.FNFabImportCS.Value + Me.FNAccImportCS.Value)

    End Sub

    Private Function DeleteData() As Boolean
        Dim _Qry As String
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet_Breakdown"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

    Private Sub wXMLCreateInvoiceData_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.ocmedit.Enabled = False
        FNFabricAssetCS.Properties.ReadOnly = (ocmedit.Enabled = False)
        FNAccessoryAssetCS.Properties.ReadOnly = (ocmedit.Enabled = False)
        FNCMP.Properties.ReadOnly = (ocmedit.Enabled = False)
        FNCostTransport.Properties.ReadOnly = (ocmedit.Enabled = False)
        FNHangerCS.Properties.ReadOnly = (ocmedit.Enabled = False)
        FNFabImportCS.Properties.ReadOnly = (ocmedit.Enabled = False)
        FNAccImportCS.Properties.ReadOnly = (ocmedit.Enabled = False)
        FNSurchargeCS.Properties.ReadOnly = (ocmedit.Enabled = False)

        ogvbreakdown.OptionsView.ShowAutoFilterRow = False
        ogvbreakdownsurcharge.OptionsView.ShowAutoFilterRow = False
        ogvbreakdownnet.OptionsView.ShowAutoFilterRow = False

        StateFormLoad = False
    End Sub

    Private Sub FTCustomerPO_EditValueChanged(sender As Object, e As EventArgs) Handles FTCustomerPO.EditValueChanged

        If (StateFormLoad) Then Exit Sub

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTCustomerPO_EditValueChanged), New Object() {sender, e})
        Else
            otbbreakdown.SelectedTabPageIndex = 0

            Dim _Qry As String

            _Qry = " SELECt TOP 1 FTPOref "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination"
            _Qry &= vbCrLf & " WHERE FTPOref  ='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then

                Me.FNHSysStyleId.Text = ""
                Me.FNHSysStyleId_None.Text = ""

                Call LoadListOrderInfo(FTCustomerPO.Text)

                Call LoadOrderDataInfo(FTCustomerPO.Text)
                Call LoadHistoryFirstPriceXMLInfo(FTCustomerPO.Text)
                Call CheckImportCost(FTCustomerPO.Text.Trim)
                Me.otbmain.SelectedTabPageIndex = 0

            Else

                FNFabImport.ForeColor = Color.Blue
                FNAccImport.ForeColor = Color.Blue

            End If

        End If


    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function SaveData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String

            _Str = "UPDATE A SET "
            _Str &= vbCrLf & "   FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Str &= vbCrLf & " , FNFabricAsset=" & FNFabricAsset.Value & ""
            _Str &= vbCrLf & " , FNAccessoryAsset=" & FNAccessoryAsset.Value & ""
            _Str &= vbCrLf & " , FNCMP=" & FNCMP.Value & ""
            _Str &= vbCrLf & " , FNCostTransport=" & FNCostTransport.Value & ""
            _Str &= vbCrLf & " , FNFirstSale=" & FNFirstSale.Value & ""
            _Str &= vbCrLf & " , FNHanger=" & FNHanger.Value & ""
            _Str &= vbCrLf & " , FNNetFirstSale=" & FNNetFirstSale.Value & ""
            _Str &= vbCrLf & " , FNFabricAssetAct=" & FNFabricAssetAct.Value & ""
            _Str &= vbCrLf & " , FNAccessoryAssetAct=" & FNAccessoryAssetAct.Value & ""
            _Str &= vbCrLf & " , FNCMPAct=" & FNCMP.Value & ""
            _Str &= vbCrLf & " , FNCostTransportAct=" & FNCostTransport.Value & ""
            _Str &= vbCrLf & " , FNFirstSaleAct=" & FNFirstSaleAct.Value & ""
            _Str &= vbCrLf & " , FNHangerAct=" & FNHangerAct.Value & ""
            _Str &= vbCrLf & " , FNNetFirstSaleAct=" & FNNetFirstSaleAct.Value & ""
            _Str &= vbCrLf & " , FNFabImport=" & FNFabImport.Value & ""
            _Str &= vbCrLf & " , FNAccImport=" & FNAccImport.Value & ""
            _Str &= vbCrLf & " , FNFabricAssetCostSheet=" & FNFabricAssetCS.Value & ""
            _Str &= vbCrLf & " , FNAccessoryAssetCostSheet=" & FNAccessoryAssetCS.Value & ""
            _Str &= vbCrLf & " , FNCMPCostSheet=" & FNCMP.Value & ""
            _Str &= vbCrLf & " , FNCostTransportCostSheet=" & FNCostTransport.Value & ""
            _Str &= vbCrLf & " , FNFirstSaleCostSheet=" & FNFirstSaleCS.Value & ""
            _Str &= vbCrLf & " , FNHangerCostSheet=" & FNHangerCS.Value & ""
            _Str &= vbCrLf & " , FNNetFirstSaleCostSheet=" & FNNetFirstSaleCS.Value & ""
            _Str &= vbCrLf & " , FNFabImportCS=" & FNFabImportCS.Value & ""
            _Str &= vbCrLf & " , FNAccImportCS=" & FNAccImportCS.Value & ""
            _Str &= vbCrLf & " , FNSurchargeCS=" & FNSurchargeCS.Value & ""
            _Str &= vbCrLf & " , FNGenderChargePer=" & FNGenderChargePer.Value & ""
            _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet AS A "
            _Str &= vbCrLf & "  WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet  ( "
                _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FTCustomerPO, FNFabricAsset, FNAccessoryAsset, FNCMP, FNCostTransport, FNFirstSale, FNHanger, FNNetFirstSale, FNFabricAssetAct, FNAccessoryAssetAct, FNCMPAct, FNCostTransportAct, FNFirstSaleAct, FNHangerAct, FNNetFirstSaleAct,FNFabImport,FNAccImport"
                _Str &= vbCrLf & " ,FNFabricAssetCostSheet, FNAccessoryAssetCostSheet, FNCMPCostSheet, FNCostTransportCostSheet, FNFirstSaleCostSheet, FNHangerCostSheet, FNNetFirstSaleCostSheet, FNFabImportCS, FNAccImportCS,FNSurchargeCS,FNGenderChargePer"
                _Str &= vbCrLf & "  )"
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
                _Str &= vbCrLf & " ," & FNFabricAsset.Value & ""
                _Str &= vbCrLf & " ," & FNAccessoryAsset.Value & ""
                _Str &= vbCrLf & " ," & FNCMP.Value & ""
                _Str &= vbCrLf & " ," & FNCostTransport.Value & ""
                _Str &= vbCrLf & " ," & FNFirstSale.Value & ""
                _Str &= vbCrLf & " ," & FNHanger.Value & ""
                _Str &= vbCrLf & " ," & FNNetFirstSale.Value & ""
                _Str &= vbCrLf & " ," & FNFabricAssetAct.Value & ""
                _Str &= vbCrLf & " ," & FNAccessoryAssetAct.Value & ""
                _Str &= vbCrLf & " ," & FNCMP.Value & ""
                _Str &= vbCrLf & " ," & FNCostTransport.Value & ""
                _Str &= vbCrLf & " ," & FNFirstSaleAct.Value & ""
                _Str &= vbCrLf & " ," & FNHangerAct.Value & ""
                _Str &= vbCrLf & " ," & FNNetFirstSaleAct.Value & ""
                _Str &= vbCrLf & " ," & FNFabImport.Value & ""
                _Str &= vbCrLf & " ," & FNAccImport.Value & ""
                _Str &= vbCrLf & " , " & FNFabricAssetCS.Value & ""
                _Str &= vbCrLf & " ," & FNAccessoryAssetCS.Value & ""
                _Str &= vbCrLf & " , " & FNCMP.Value & ""
                _Str &= vbCrLf & " , " & FNCostTransport.Value & ""
                _Str &= vbCrLf & " , " & FNFirstSaleCS.Value & ""
                _Str &= vbCrLf & " , " & FNHangerCS.Value & ""
                _Str &= vbCrLf & " , " & FNNetFirstSaleCS.Value & ""
                _Str &= vbCrLf & " , " & FNFabImportCS.Value & ""
                _Str &= vbCrLf & " , " & FNAccImportCS.Value & ""
                _Str &= vbCrLf & " , " & FNSurchargeCS.Value & ""
                _Str &= vbCrLf & " , " & FNGenderChargePer.Value & ""

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            _Str = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet_Breakdown"
            _Str &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            With ogvbreakdown

                Dim Colorway As String = ""
                Dim SizeBreakdown As String = ""
                Dim POLineItem As String = ""
                Dim Qty As Double = 0

                For I As Integer = 0 To .RowCount - 1

                    Colorway = .GetRowCellValue(I, "FTColorway").ToString()
                    POLineItem = .GetRowCellValue(I, "FTNikePOLineItem").ToString()


                    Dim StateFound As Boolean = False

                    For C As Integer = 0 To .Columns.Count - 1
                        Select Case .Columns(C).FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper

                            Case Else
                                SizeBreakdown = .Columns(C).Caption.Trim()
                                Qty = Val("" & .GetRowCellValue(I, .Columns(C).FieldName.ToString).ToString())

                                If Qty > 0 Then
                                    StateFound = True
                                End If

                        End Select
                    Next

                    For C As Integer = 0 To .Columns.Count - 1
                        Select Case .Columns(C).FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
                            Case Else

                                SizeBreakdown = .Columns(C).Caption.Trim()
                                Qty = Val("" & .GetRowCellValue(I, .Columns(C).FieldName.ToString).ToString())

                                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet_Breakdown"
                                _Str &= vbCrLf & "  ("
                                _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCustomerPO, FTColorway, FTSizeBreakDown, FTPOLineItem, FNNetFOB "
                                _Str &= vbCrLf & "  ) "
                                _Str &= vbCrLf & " SELECT "
                                _Str &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Str &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
                                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Colorway) & "'"
                                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(SizeBreakdown) & "'"
                                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(POLineItem) & "'"
                                _Str &= vbCrLf & " ,'" & Qty & "'"

                                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False

                                End If

                        End Select
                    Next

                Next

            End With

            With ogvbreakdownsurcharge

                Dim Colorway As String = ""
                Dim SizeBreakdown As String = ""
                Dim POLineItem As String = ""
                Dim Qty As Double = 0

                For I As Integer = 0 To .RowCount - 1

                    Colorway = .GetRowCellValue(I, "FTColorway").ToString()
                    POLineItem = .GetRowCellValue(I, "FTNikePOLineItem").ToString()


                    Dim StateFound As Boolean = False

                    For C As Integer = 0 To .Columns.Count - 1
                        Select Case .Columns(C).FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper

                            Case Else
                                SizeBreakdown = .Columns(C).Caption.Trim()
                                Qty = Val("" & .GetRowCellValue(I, .Columns(C).FieldName.ToString).ToString())

                                If Qty > 0 Then
                                    StateFound = True
                                End If

                        End Select
                    Next

                    For C As Integer = 0 To .Columns.Count - 1
                        Select Case .Columns(C).FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
                            Case Else

                                SizeBreakdown = .Columns(C).Caption.Trim()
                                Qty = Val("" & .GetRowCellValue(I, .Columns(C).FieldName.ToString).ToString())

                                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet_Breakdown"
                                _Str &= vbCrLf & "  SET FNSurcharge =" & Qty & " "
                                _Str &= vbCrLf & " ,FNNetPrice=FNNetFOB + " & Qty & ""
                                _Str &= vbCrLf & "  WHERE FTCustomerPO ='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
                                _Str &= vbCrLf & "        AND FTColorway='" & HI.UL.ULF.rpQuoted(Colorway) & "'"
                                _Str &= vbCrLf & "        AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(SizeBreakdown) & "'"
                                _Str &= vbCrLf & "        AND FTPOLineItem ='" & HI.UL.ULF.rpQuoted(POLineItem) & "'"

                                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False

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

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.FTCustomerPO.Text <> "" Then

            If SaveData() = True Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                ' Call LoadOrderDataInfo(FTCustomerPO.Text)
                Call LoadDataSave()

            Else

                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If
    End Sub

    Private Sub LoadDataSave()
        Dim _dtstyle As System.Data.DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT TOP 1  "
        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
        _Qry &= vbCrLf & ",FTCustomerPO,FNFabricAsset, FNAccessoryAsset, FNCMP, FNCostTransport"
        _Qry &= vbCrLf & ", FNFirstSale, FNHanger, FNNetFirstSale, "
        _Qry &= vbCrLf & " FNFabricAssetAct, FNAccessoryAssetAct, FNCMPAct, FNCostTransportAct, FNFirstSaleAct, FNHangerAct, FNNetFirstSaleAct,ISNULL(FNNetFirstSaleCostSheet,0) AS FNNetFirstSaleCostSheet"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (A. FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "')"

        _dtstyle = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim _FNNetFirstSale As Double = 0.0
        Dim _FNNetFirstSaleAct As Double = 0.0
        Dim _FNNetFirstSaleCSCurrent As Double = 0.0

        Me.FTStateAccept.Checked = False
        FTStateAcceptBy.Text = ""
        FTStateAcceptByDateTime.Text = ""

        For Each R As DataRow In _dtstyle.Rows

            If R!FTUpdUser.ToString = "" Then

                FTStateAcceptBy.Text = R!FTInsUser.ToString
                FTStateAcceptByDateTime.Text = HI.UL.ULDate.ConvertEN(R!FDInsDate.ToString) & "  " & R!FTInsTime.ToString

            Else

                FTStateAcceptBy.Text = R!FTUpdUser.ToString
                FTStateAcceptByDateTime.Text = HI.UL.ULDate.ConvertEN(R!FDUpdDate.ToString) & "  " & R!FTUpdTime.ToString

            End If

            Me.FTStateAccept.Checked = True
        Next

        _dtstyle.Dispose()

    End Sub
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        _StateClear = True
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTStateAccept.Checked = False
        FTStateAcceptBy.Text = ""
        FTStateAcceptByDateTime.Text = ""
        Me.FNHSysStyleId.Text = ""
        Me.FNHSysStyleId_None.Text = ""
        otbbreakdown.SelectedTabPageIndex = 0

        _StateClear = False
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Me.FTCustomerPO.Text <> "" Then

            If Me.DeleteData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Call LoadOrderDataInfo(FTCustomerPO.Text)
                Me.FTStateAccept.Checked = False
                FTStateAcceptBy.Text = ""
                FTStateAcceptByDateTime.Text = ""
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            If FTOrderNo.Text <> "" Then

                If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then

                    Dim _Qry As String = ""
                    Dim _dt As System.Data.DataTable
                    _Qry = "   Select A.FTOrderNo"
                    _Qry &= vbCrLf & "    ,A.FTStyleCode"
                    _Qry &= vbCrLf & "    ,A.FTCustCode"
                    _Qry &= vbCrLf & " 	 ,A.FTPORef"
                    _Qry &= vbCrLf & " 	 ,CASE WHEN ISDATE(FDShipDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDShipDate) ,103)  Else '' END AS FDShipDate"
                    _Qry &= vbCrLf & " 	 ,CASE WHEN ISDATE(FDOrderDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDOrderDate) ,103)  Else '' END AS FDOrderDate"
                    _Qry &= vbCrLf & " 	 ,FNGrandQuantity"
                    _Qry &= vbCrLf & " 	 ,FTCmpName"
                    _Qry &= vbCrLf & "  FROM (SELECT O.FTOrderNo"
                    _Qry &= vbCrLf & "    , ST.FTStyleCode"
                    _Qry &= vbCrLf & "    , CT.FTCustCode"
                    _Qry &= vbCrLf & "   , O.FTPORef"
                    _Qry &= vbCrLf & "   , O.FDOrderDate"
                    _Qry &= vbCrLf & "    ,ISNULL(("
                    _Qry &= vbCrLf & " 	SELECT TOP 1 FDShipDate"
                    _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK)"
                    _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
                    _Qry &= vbCrLf & " 	ORDER BY    FDShipDate ASC"
                    _Qry &= vbCrLf & " 	),'') AS FDShipDate"
                    _Qry &= vbCrLf & " ,ISNULL(("
                    _Qry &= vbCrLf & " 	SELECT SUM(FNQuantity) AS FNGrandQuantity"
                    _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS Sub WITH(NOLOCK)"
                    _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
                    _Qry &= vbCrLf & " 	),0) AS FNGrandQuantity"

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & "   ,Cmp.FTCmpCode + ' : ' + Cmp.FTCmpNameTH AS FTCmpName"
                    Else
                        _Qry &= vbCrLf & "   ,Cmp.FTCmpCode + ' : ' +  Cmp.FTCmpNameEN AS FTCmpName"
                    End If

                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)  INNER JOIN"
                    _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN"
                    _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS CT WITH(NOLOCK)  ON O.FNHSysCustId = CT.FNHSysCustId"
                    _Qry &= vbCrLf & "       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK)  ON O.FNHSysCmpId = Cmp.FNHSysCmpId"
                    _Qry &= vbCrLf & "   WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                    _Qry &= vbCrLf & " 	 ) AS A"
                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                    Me.FDShipDate.Text = ""
                    Me.FDOrderDate.Text = ""
                    FNOrderQuantity.Value = 0

                    For Each R As DataRow In _dt.Rows

                        Me.FDShipDate.Text = R!FDShipDate.ToString
                        Me.FDOrderDate.Text = R!FDOrderDate.ToString
                        Me.FNOrderQuantity.Value = Val(R!FNGrandQuantity.ToString)

                        Exit For
                    Next
                Else
                    Me.FDShipDate.Text = ""
                    Me.FDOrderDate.Text = ""
                    Me.FNOrderQuantity.Value = 0
                End If
            Else
                Me.FDShipDate.Text = ""
                Me.FDOrderDate.Text = ""
                Me.FNOrderQuantity.Value = 0
            End If

        End If
    End Sub

    Private Sub FNCostTransport_EditValueChanged(sender As Object, e As EventArgs) Handles FNCostTransport.EditValueChanged
        Call Calculate()
        Call CalculateAct()
        Call CalculateCS()
    End Sub


    Private Sub FNSurchargeCS_EditValueChanged(sender As Object, e As EventArgs) Handles FNSurchargeCS.EditValueChanged

    End Sub

    Private Sub FNFabricAssetCS_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNFabricAssetCS.EditValueChanging, FNAccessoryAssetCS.EditValueChanging,
                                                                            FNCMP.EditValueChanging, FNCostTransport.EditValueChanging, FNHangerCS.EditValueChanging,
                                                                            FNFabImportCS.EditValueChanging, FNAccImportCS.EditValueChanging, FNSurchargeCS.EditValueChanging
        If Me.ocmedit.Enabled Then


            Dim _FNFabricAssetCS As Double = FNFabricAssetCS.Value
            Dim _FNAccessoryAssetCS As Double = FNAccessoryAssetCS.Value
            Dim _FNCMP As Double = FNCMP.Value
            Dim _FNCostTransport As Double = FNCostTransport.Value
            Dim _FNHangerCS As Double = FNHangerCS.Value
            Dim _FNFabImportCS As Double = FNFabImportCS.Value
            Dim _FNAccImportCS As Double = FNAccImportCS.Value
            Dim _FNSurchargeCS As Double = FNSurchargeCS.Value

            Select Case sender.name.ToString
                Case "FNFabricAssetCS"
                    _FNFabricAssetCS = Val(e.NewValue.ToString)
                Case "FNAccessoryAssetCS"
                    _FNAccessoryAssetCS = Val(e.NewValue.ToString)
                Case "FNCMP"
                    _FNCMP = Val(e.NewValue.ToString)
                Case "FNCostTransport"
                    _FNCostTransport = Val(e.NewValue.ToString)
                Case "FNHangerCS"
                    _FNHangerCS = Val(e.NewValue.ToString)
                Case "FNFabImportCS"
                    _FNFabImportCS = Val(e.NewValue.ToString)
                Case "FNAccImportCS"
                    _FNAccImportCS = Val(e.NewValue.ToString)
                Case "FNSurchargeCS"
                    _FNSurchargeCS = Val(e.NewValue.ToString)
            End Select

            FNFirstSaleCS.Value = _FNFabricAssetCS + _FNAccessoryAssetCS + _FNCMP
            FNNetFirstSaleCS.Value = FNFirstSaleCS.Value + (_FNHangerCS + _FNFabImportCS + _FNAccImportCS + _FNSurchargeCS)

        End If

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

    End Sub

    Private Sub ogvbreakdown_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvbreakdown.RowCellStyle, ogvbreakdownnet.RowCellStyle, ogvbreakdownsurcharge.RowCellStyle

        Try

            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                Select Case e.Column.FieldName.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
                    Case Else

                        If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString) > 0 Then
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        End If

                End Select

            End With

        Catch ex As Exception
        End Try
    End Sub
End Class
Imports System.Drawing

Public Class wDemandPullQuantiry

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Procedure"

    Private Sub InitGrid()

        Try
            With Me.ogvsource
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception

        End Try

        Try
            With Me.ogvdest
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception

        End Try

        Try
            With Me.ogvbal
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        With ogvsource
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

        With ogvdest
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

        With ogvbal
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

    Private Sub InitGridSource(Optional _dt As DataTable = Nothing)
        Dim _colcount As Integer = 0
        With Me.ogvsource

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dt Is Nothing) Then

                For Each Col As DataColumn In _dt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
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

        Me.ogcsource.DataSource = _dt


    End Sub

    Private Sub InitGridDest(Optional _dt As DataTable = Nothing)

        Dim _colcount As Integer = 0
        With Me.ogvdest

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next


            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
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

        End With

        Me.ogcdest.DataSource = _dt

    End Sub

    Private Sub InitGridBalance(Optional _dt As DataTable = Nothing)
        Dim _colcount As Integer = 0
        With Me.ogvbal

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
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

        Me.ogcbal.DataSource = _dt

    End Sub

    Private Sub LoadDataStyleInfo()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT TOP 1 CRN.FTCmpRunCode, OS.FDSubOrderDate, OS.FDProDate"
        _Qry &= vbCrLf & " , OS.FDShipDate, CTN.FTContinentCode, CUN.FTCountryCode"
        _Qry &= vbCrLf & " , P.FTProvinceCode, SM.FTShipModeCode, SP.FTShipPortCode, CUR.FTCurCode"
        _Qry &= vbCrLf & " , GD.FTGenderCode, U.FTUnitCode, OS.FTStateEmb, OS.FTStatePrint, OS.FTStateHeat"
        _Qry &= vbCrLf & " , OS.FTStateLaser, OS.FTStateWindows"
        _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS  WITH (NOLOCK)  ON O.FTOrderNo = OS.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST  WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U  WITH (NOLOCK) ON OS.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender AS GD  WITH (NOLOCK) ON OS.FNHSysGenderId = GD.FNHSysGenderId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CUR  WITH (NOLOCK) ON OS.FNHSysCurId = CUR.FNHSysCurId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS SP  WITH (NOLOCK) ON OS.FNHSysShipPortId = SP.FNHSysShipPortId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS SM WITH (NOLOCK)  ON OS.FNHSysShipModeId = SM.FNHSysShipModeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS P  WITH (NOLOCK) ON OS.FNHSysCountryId = P.FNHSysCountryId AND OS.FNHSysProvinceId = P.FNHSysProvinceId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS CUN  WITH (NOLOCK)  ON OS.FNHSysCountryId = CUN.FNHSysCountryId AND OS.FNHSysContinentId = CUN.FNHSysContinentId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS CTN  WITH (NOLOCK)  ON OS.FNHSysContinentId = CTN.FNHSysContinentId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun AS CRN   WITH (NOLOCK) ON O.FNHSysCmpRunId = CRN.FNHSysCmpRunId"
        _Qry &= vbCrLf & "  WHERE   (ST.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text.Trim()) & "')"
        _Qry &= vbCrLf & "          AND   (O.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNoRef.Text.Trim()) & "')"
        _Qry &= vbCrLf & "          AND (O.FNOrderType = 22) "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dt.Rows

            FNHSysCmpRunId.Text = R!FTCmpRunCode.ToString

            Try
                FDSubOrderDate.DateTime = R!FDSubOrderDate.ToString
            Catch ex As Exception
                FDSubOrderDate.Text = ""
            End Try

            Try
                FDProDate.DateTime = R!FDProDate.ToString
            Catch ex As Exception
                FDProDate.Text = ""
            End Try

            Try
                FDShipDate.DateTime = R!FDShipDate.ToString
            Catch ex As Exception
                FDShipDate.Text = ""
            End Try

            FNHSysContinentId.Text = R!FTContinentCode.ToString
            FNHSysCountryId.Text = R!FTCountryCode.ToString
            FNHSysProvinceId.Text = R!FTProvinceCode.ToString
            FNHSysShipModeId.Text = R!FTShipModeCode.ToString
            FNHSysShipPortId.Text = R!FTShipPortCode.ToString
            FNHSysCurId.Text = R!FTCurCode.ToString
            FNHSysGenderId.Text = R!FTGenderCode.ToString
            FNHSysUnitId.Text = R!FTUnitCode.ToString

            FTStateEmb.Checked = (R!FTStateEmb.ToString = "1")
            FTStatePrint.Checked = (R!FTStatePrint.ToString = "1")
            FTStateHeat.Checked = (R!FTStateHeat.ToString = "1")
            FTStateLaser.Checked = (R!FTStateLaser.ToString = "1")
            FTStateWindows.Checked = (R!FTStateWindows.ToString = "1")

            Exit For
        Next

    End Sub

    Private Sub InitData()

        Dim _Qry As String = ""
        Dim _dtSub As DataTable

        Dim _Total As Integer = 0

        _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[SP_Get_Style_BreakDown_DemandPull_ByJob] " & (Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString()))).ToString & ",'" & HI.UL.ULF.rpQuoted(FTOrderNoRef.Text.Trim()) & "' "
        _dtSub = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim _dtPrice As DataTable

        _Qry = "  SELECT B.FTColorway, B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "    FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK)   ON A.FTOrderNo = B.FTOrderNo"
        _Qry &= vbCrLf & "    WHERE  (A.FNOrderType = 0) AND (A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString())) & ") AND (A.FTOrderNoRef='" & HI.UL.ULF.rpQuoted(FTOrderNoRef.Text.Trim()) & "')"
        _Qry &= vbCrLf & "    GROUP BY B.FTColorway, B.FTSizeBreakDown"

        _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        If _dtPrice.Rows.Count > 0 Then

            Dim _Filter As String

            For Each R As DataRow In _dtSub.Rows
                For Each Col As DataColumn In _dtSub.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                        Case Else
                            '_Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                            _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "

                            If _dtPrice.Select(_Filter).Length > 0 Then

                                For Each Rx As DataRow In _dtPrice.Select(_Filter)

                                    If Val(R.Item(Col.ColumnName.ToString)) > Val(Rx!FNQuantity.ToString) Then
                                        R.Item(Col.ColumnName.ToString) = Val(R.Item(Col.ColumnName.ToString)) - Val(Rx!FNQuantity.ToString)
                                    Else
                                        R.Item(Col.ColumnName.ToString) = 0
                                    End If

                                    Exit For

                                Next

                            End If

                    End Select
                Next

            Next
        End If

        Dim M1 As New DataTable
        M1 = _dtSub.Copy

        'For Each Rx As DataRow In _dtProd.Select("FTOrderProdNo<>'" & HI.UL.ULF.rpQuoted(Me.JobProdNo) & "'")
        '    For Each Rx2 As DataRow In M1.Select("FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "' ")
        '        If M1.Columns.IndexOf(Rx!FTSizeBreakDown.ToString) >= 0 Then
        '            Rx2.Item(Rx!FTSizeBreakDown.ToString) = Rx2.Item(Rx!FTSizeBreakDown.ToString) - Val(Rx!FNQuantity)
        '        End If
        '        Exit For
        '    Next
        'Next

        For Each Rx As DataRow In M1.Rows
            _Total = 0
            For Each Col As DataColumn In M1.Columns
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                    Case Else
                        _Total = _Total + Val(Rx.Item(Col.ColumnName.ToString))
                End Select
            Next
            Rx.Item("Total") = _Total
        Next

        Dim M2 As New DataTable
        M2 = _dtSub.Copy

        M2.BeginInit()
        M2.Columns.Add("FTNikePOLineItem", GetType(String))
        M2.EndInit()

        For Each Rx As DataRow In M2.Rows

            _Total = 0

            For Each Col As DataColumn In M2.Columns

                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                    Case Else
                        Rx.Item(Col.ColumnName.ToString) = 0
                End Select

            Next

            Rx.Item("Total") = 0

        Next

        Dim M3 As New DataTable
        M3 = M1.Copy

        Call InitGridSource(M1.Copy)
        Call InitGridDest(M2.Copy)
        Call InitGridBalance(M3.Copy)

    End Sub

    Private _StateSumGrid As Boolean = False
    Private Sub SumGrid()
        _StateSumGrid = True
        Try
            Dim _Total As Integer = 0
            With Me.ogvdest
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                    Select Case GridCol.FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                        Case Else
                            If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                _Total = _Total + CDbl(.GetFocusedRowCellValue(GridCol))
                            Else
                                _Total = _Total + 0 '
                            End If
                    End Select

                Next

                .SetFocusedRowCellValue("Total", _Total)
            End With
            _Total = 0
            With Me.ogvbal
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                    Select Case GridCol.FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                        Case Else
                            If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                _Total = _Total + CDbl(.GetFocusedRowCellValue(GridCol))
                            Else
                                _Total = _Total + 0
                            End If
                    End Select

                Next

                .SetFocusedRowCellValue("Total", _Total)
            End With

            CType(ogcdest.DataSource, DataTable).AcceptChanges()
            CType(ogcbal.DataSource, DataTable).AcceptChanges()

            _Total = 0

        Catch ex As Exception
        End Try
        _StateSumGrid = False
    End Sub

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

                    For Each R As DataRow In CType(Me.ogcsource.DataSource, DataTable).Select("FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' ")
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

                            ogvbal.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_OrgValue - _NewValue))
                            ogvdest.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))
                            CType(ogcbal.DataSource, DataTable).AcceptChanges()
                            CType(ogcdest.DataSource, DataTable).AcceptChanges()

                            If Not (ogcbal.DataSource Is Nothing) Then
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

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        Try
            If Me.FNHSysStyleId.Text <> "" Then
                If FTOrderNoRef.Text.Trim = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTOrderNoRef_lbl.Text)
                    FTOrderNoRef.Focus()
                    Return False
                End If

                If Me.FDSubOrderDate.Text <> "" Then
                        If Me.FDProDate.Text <> "" Then
                            If Me.FDShipDate.Text <> "" Then
                                If Me.FNHSysContinentId.Text <> "" Then
                                If Me.FNHSysCountryId.Text <> "" Then
                                    If Me.FNHSysShipModeId.Text <> "" Then
                                        If Me.FNHSysShipPortId.Text <> "" Then
                                            If Me.FNHSysCurId.Text <> "" Then
                                                If Me.FNHSysGenderId.Text <> "" Then
                                                    If Me.FNHSysUnitId.Text <> "" Then
                                                        If Me.FTPORef.Text.Trim() <> "" Then
                                                            _Pass = True
                                                        Else
                                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTPORef_lbl.Text)
                                                            Me.FTPORef.Focus()
                                                        End If
                                                    Else
                                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                                                        Me.FNHSysUnitId.Focus()
                                                    End If
                                                Else
                                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysGenderId_lbl.Text)
                                                    Me.FNHSysGenderId.Focus()
                                                End If
                                            Else
                                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCurId_lbl.Text)
                                                Me.FNHSysCurId.Focus()
                                            End If
                                        Else
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysShipPortId_lbl.Text)
                                            Me.FNHSysShipPortId.Focus()
                                        End If
                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysShipModeId_lbl.Text)
                                        Me.FNHSysShipModeId.Focus()
                                    End If

                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCountryId_lbl.Text)
                                        Me.FNHSysCountryId.Focus()
                                    End If
                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysContinentId_lbl.Text)
                                    Me.FNHSysContinentId.Focus()
                                End If

                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDShipDate_lbl.Text)
                                Me.FDShipDate.Focus()
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDProDate_lbl.Text)
                            Me.FDProDate.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDSubOrderDate_lbl.Text)
                        Me.FDSubOrderDate.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysStyleId_lbl.Text)
                Me.FNHSysStyleId.Focus()
            End If

            Return _Pass

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                Throw New Exception(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace().ToString())
            End If

            Return False

        End Try

    End Function

    Private Function SaveDemandPull() As String
        Dim _DmP As String = ""
        Dim _DmPSub As String = ""
        Dim _Key As String = ""
        Dim _Qry As String = ""
        Dim _FTOrderNo As String = ""
        Dim _FTOrderNoMain As String = ""
        Dim _FTSubOrderNo As String = ""
        Dim _Dt As DataTable

        With CType(ogcdest.DataSource, DataTable)
            .AcceptChanges()
            _Dt = .Copy
        End With

        '_Qry = " SELECT  TOP 1 FTOrderNo"
        '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK)"
        '_Qry &= vbCrLf & " WHERE FNOrderType =0 "
        '_Qry &= vbCrLf & " AND FTPORef='" & HI.UL.ULF.rpQuoted(FTPORef.Text.Trim) & "' "
        '_Qry &= vbCrLf & " AND FTOrderNoRef ='" & HI.UL.ULF.rpQuoted(FTOrderNoRef.Text.Trim) & "' "
        '_Qry &= vbCrLf & " AND FNHSysStyleIdPull in ( "
        '_Qry &= vbCrLf & " SELECT TOP 1 FNHSysStyleId  "
        '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS A WITH(NOLOCK) "
        '_Qry &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
        '_Qry &= vbCrLf & "')"
        '_FTOrderNoMain = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

        '_Qry = " SELECT  TOP 1 FTOrderNo"
        '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK)"
        '_Qry &= vbCrLf & " WHERE FNOrderType =22 AND  FNHSysStyleId in ( "
        '_Qry &= vbCrLf & " SELECT TOP 1 FNHSysStyleId  "
        '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS A WITH(NOLOCK) "
        '_Qry &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "')"
        '_FTOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

        _FTOrderNo = FTOrderNoRef.Text.Trim
        _FTOrderNoMain = ""

        _Qry = " SELECT  TOP 1 FTSubOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS X"
        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
        _FTSubOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

        If _FTOrderNoMain <> "" Then
            _DmP = _FTOrderNoMain

            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_GEN_CHARACTER_SubOrderNo '" & HI.UL.ULF.rpQuoted(_FTOrderNoMain) & "'"
            _DmPSub = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_MERCHAN, "")

        Else

            _DmP = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN), "TMERTOrder", "0", False, "" & Me.FNHSysCmpRunId.Text.Trim().ToString()).ToString()
            _DmPSub = _DmP & "-A"

        End If
      
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            If _FTOrderNoMain = "" Then

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder("
                _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId"
                _Qry &= vbCrLf & " , FNHSysCmpRunId, FNHSysStyleId, FTPORef, FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, "
                _Qry &= vbCrLf & "       FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark "
                _Qry &= vbCrLf & "  ,FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FNHSysMerTeamId, "
                _Qry &= vbCrLf & "    FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus"
                _Qry &= vbCrLf & " , FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FTOrderNoRef, FNJobState,FNHSysStyleIdPull,FNHSysCmpIdCreate"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & "   SELECT Top 1  "
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmP) & "' AS  FTOrderNo"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FDSubOrderDate.Text) & "' AS  FDOrderDate"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS  FTOrderBy"
                _Qry &= vbCrLf & ",0 AS  FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPORef.Text.Trim) & "' AS FTPORef"
                _Qry &= vbCrLf & " , FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, "
                _Qry &= vbCrLf & "   FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTImage1, FTImage2"
                _Qry &= vbCrLf & " , FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FNHSysMerTeamId,"
                _Qry &= vbCrLf & "   FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId"
                _Qry &= vbCrLf & " ,'' AS  FTOrderCreateStatus, FPOrderImage1"
                _Qry &= vbCrLf & " , FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId"
                _Qry &= vbCrLf & ",FTOrderNo AS  FTOrderNoRef,"
                _Qry &= vbCrLf & " 0 AS  FNJobState"
                _Qry &= vbCrLf & " ,FNHSysStyleId," & Val(HI.ST.SysInfo.CmpID) & ""
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return ""
                End If

            End If

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub("
            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy"
            _Qry &= vbCrLf & ", FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId,"
            _Qry &= vbCrLf & "FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId"
            _Qry &= vbCrLf & ", FNHSysGenderId, FNHSysUnitId, FTStateEmb, FTStatePrint"
            _Qry &= vbCrLf & ", FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1,"
            _Qry &= vbCrLf & "  FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3"
            _Qry &= vbCrLf & ", FTOther3Note1, FTRemark, FNHSysShipPortId, FDShipDateOrginal, FTCustRef,FNHSysPlantId"
            _Qry &= vbCrLf & " )"
            _Qry &= vbCrLf & "   SELECT Top 1  "
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmP) & "' AS  FTOrderNo"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmPSub) & "' AS FTSubOrderNo"
            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FDSubOrderDate.Text) & "' AS FDSubOrderDate"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FDProDate.Text) & "' AS FDProDate"
            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FDShipDate.Text) & "' AS FDShipDate"
            _Qry &= vbCrLf & ", FNHSysBuyId"
            _Qry &= vbCrLf & " ," & Integer.Parse(Val(FNHSysContinentId.Properties.Tag.ToString)) & " AS  FNHSysContinentId"
            _Qry &= vbCrLf & ", " & Integer.Parse(Val(FNHSysCountryId.Properties.Tag.ToString)) & " AS FNHSysCountryId"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysProvinceId.Properties.Tag.ToString)) & " AS FNHSysProvinceId"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysShipModeId.Properties.Tag.ToString)) & " AS FNHSysShipModeId"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCurId.Properties.Tag.ToString)) & " AS  FNHSysCurId"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysGenderId.Properties.Tag.ToString)) & " AS FNHSysGenderId"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & " AS FNHSysUnitId"
            _Qry &= vbCrLf & ",'" & FTStateEmb.EditValue.ToString & "' AS  FTStateEmb"
            _Qry &= vbCrLf & ",'" & FTStatePrint.EditValue.ToString & "' AS  FTStatePrint"
            _Qry &= vbCrLf & ",'" & FTStateHeat.EditValue.ToString & "' AS  FTStateHeat"
            _Qry &= vbCrLf & ",'" & FTStateLaser.EditValue.ToString & "' AS  FTStateLaser"
            _Qry &= vbCrLf & ",'" & FTStateWindows.EditValue.ToString & "' AS  FTStateWindows"
            _Qry &= vbCrLf & ", FTStateOther1"
            _Qry &= vbCrLf & ",FTOther1Note"
            _Qry &= vbCrLf & ", FTStateOther2"
            _Qry &= vbCrLf & ", FTOther2Note"
            _Qry &= vbCrLf & ", FTStateOther3"
            _Qry &= vbCrLf & ", FTOther3Note1"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTRemarkSubOrderNo.Text.Trim) & "' AS  FTRemark"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysShipPortId.Properties.Tag.ToString)) & " AS FNHSysShipPortId"
            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FDShipDate.Text) & "' AS  FDShipDateOrginal"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTCustRef.Text.Trim) & "' AS  FTCustRef,FNHSysPlantId"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS X"
            _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
            _Qry &= vbCrLf & "  AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return ""
            End If

            Dim _ColorWay, _FTSizeBreakDown, _FTNikePOLineItem As String
            For Each Rx As DataRow In _Dt.Rows

                _ColorWay = Rx!FTColorway.ToString
                _FTNikePOLineItem = Rx!FTNikePOLineItem.ToString

                For Each Col As DataColumn In _Dt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
                        Case Else
                            If IsNumeric(Rx.Item(Col.ColumnName.ToString)) Then
                                If (Integer.Parse(Rx.Item(Col.ColumnName.ToString))) > 0 Then
                                    _FTSizeBreakDown = Col.ColumnName.ToString

                                    _Qry = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown "
                                    _Qry &= vbCrLf & " (  FTInsUser, FDInsDate, FTInsTime"
                                    _Qry &= vbCrLf & ",  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown"
                                    _Qry &= vbCrLf & ", FNQuantity,FNGrandQuantity, FNHSysMatColorId, FNHSysMatSizeId, "
                                    _Qry &= vbCrLf & "  FNExtraQty, FNQuantityExtra, FNGarmentQtyTest,FTNikePOLineItem)"
                                    _Qry &= vbCrLf & "   SELECT Top 1  "
                                    _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmP) & "' AS  FTOrderNo"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmPSub) & "' AS FTSubOrderNo"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "' "
                                    _Qry &= vbCrLf & "," & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & " "
                                    _Qry &= vbCrLf & "," & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & " "
                                    _Qry &= vbCrLf & ",ISNULL((SELECT TOP 1 FNHSysMatColorId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor WITH(NOLOCK) WHERE FTMatColorCode='" & HI.UL.ULF.rpQuoted(_ColorWay) & "' ),0)"
                                    _Qry &= vbCrLf & ",ISNULL((SELECT TOP 1 FNHSysMatSizeId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize WITH(NOLOCK) WHERE FTMatSizeCode='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "' ),0)"
                                    _Qry &= vbCrLf & ",0"
                                    _Qry &= vbCrLf & ",0"
                                    _Qry &= vbCrLf & ",0"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTNikePOLineItem) & "' "

                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                        Return ""

                                    End If
                                End If
                            End If
                    End Select
                Next
            Next

            _Qry = " UPDATE M SET"
            _Qry &= vbCrLf & " FNPrice=BB.FNPrice"
            _Qry &= vbCrLf & " ,FNPriceOrg=BB.FNPriceOrg"
            _Qry &= vbCrLf & " ,FNCMDisPer=BB.FNCMDisPer"
            _Qry &= vbCrLf & " ,FNCMDisAmt=BB.FNCMDisAmt"
            _Qry &= vbCrLf & " ,FNNetPrice=BB.FNNetPrice"
            _Qry &= vbCrLf & "  ,FNAmt=Convert(numeric(18,2),(M.FNQuantity * BB.FNPrice))"
            _Qry &= vbCrLf & ", FNAmntExtra=0"
            _Qry &= vbCrLf & ", FNGrandAmnt=Convert(numeric(18,2),(M.FNQuantity * BB.FNPrice))"
            _Qry &= vbCrLf & ", FNAmntQtyTest=0"
            '_Qry &= vbCrLf & ", FTNikePOLineItem = BB.FTNikePOLineItem"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown  AS M INNER JOIN "
            _Qry &= vbCrLf & " (  SELECT B.FTColorway, B.FTSizeBreakDown"
            _Qry &= vbCrLf & ", MAX(B.FNPrice) AS FNPrice, MAX(B.FNPriceOrg) AS FNPriceOrg, MAX(B.FNCMDisPer) AS FNCMDisPer"
            _Qry &= vbCrLf & ", MAX(B.FNCMDisAmt) AS FNCMDisAmt, MAX(B.FNNetPrice)    AS FNNetPrice,MAX(B.FTNikePOLineItem) AS FTNikePOLineItem"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo"
            _Qry &= vbCrLf & " WHERE  (A.FNOrderType = 22) "
            _Qry &= vbCrLf & " AND  A.FNHSysStyleId in ( "
            _Qry &= vbCrLf & " SELECT TOP 1 FNHSysStyleId  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "')"
            _Qry &= vbCrLf & " GROUP BY B.FTColorway, B.FTSizeBreakDown"
            _Qry &= vbCrLf & " ) AS BB ON M.FTColorway = BB.FTColorway AND M.FTSizeBreakDown=BB.FTSizeBreakDown"
            _Qry &= vbCrLf & "  WHERE M.FTOrderNo='" & HI.UL.ULF.rpQuoted(_DmP) & "'"
            _Qry &= vbCrLf & "  AND M.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_DmPSub) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return "FO. No. " & _DmP & "  SUB FO. No. " & _DmPSub

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return ""

        End Try

        Return "FO. No. " & _DmP & "  SUB FO. No. " & _DmPSub
    End Function

#End Region

    Private Sub wDemandPullQuantiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call InitGrid()
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ClearDataNewStyle(Optional StateOrder As Boolean = False)
        If StateOrder = False Then
            FTOrderNoRef.Text = ""
        End If

        FDSubOrderDate.Text = ""
        FDProDate.Text = ""
        FDShipDate.Text = ""
        FTCustRef.Text = ""
        FTRemarkSubOrderNo.Text = ""
        FNHSysContinentId.Text = ""
        FNHSysCountryId.Text = ""
        FNHSysProvinceId.Text = ""
        FNHSysShipModeId.Text = ""
        FNHSysShipPortId.Text = ""
        FNHSysCurId.Text = ""
        FNHSysGenderId.Text = ""
        FNHSysUnitId.Text = ""
        Me.FTPORef.Text = ""
        Me.FNHSysCmpRunId.Text = ""

        Call InitGridSource(Nothing)
        Call InitGridDest(Nothing)
        Call InitGridBalance(Nothing)
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysStyleId.Text <> "" Then

                If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS A WITH(NOLOCK) WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then

                    Call ClearDataNewStyle()
                    'Call LoadDataStyleInfo()
                    'Call InitData()
                Else
                    Call ClearDataNewStyle()

                End If
            Else
                Call ClearDataNewStyle()

            End If

        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            Me.FNHSysStyleId.Text = ""
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.VerifyData Then
            If Not (Me.ogcdest.DataSource Is Nothing) Then
                With CType(ogcdest.DataSource, DataTable)
                    .AcceptChanges()
                    If .Select("Total>0").Length <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ จำนวน ข้อมูล Breakdown !!!", 1509070485, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End With

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Pull Quantity จาก Style นี้ ใช่หรือไม่ ? ", 1589072485, Me.FNHSysStyleId.Text) = False Then
                    Exit Sub
                End If

                Dim _FTOrderNo As String = ""
                Dim _Spl As New HI.TL.SplashScreen("Saving...data Please wait.")
                _FTOrderNo = SaveDemandPull()
                _Spl.Close()

                If _FTOrderNo <> "" Then

                    Call ClearDataNewStyle()
                    Call LoadDataStyleInfo()
                    Call InitData()

                    HI.MG.ShowMsg.mInfo("Save Data Complete ...", 1509078467, Me.Text, _FTOrderNo, System.Windows.Forms.MessageBoxIcon.Information)

                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If

            End If
        End If
    End Sub

    Private Sub FTOrderNoRef_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNoRef.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNoRef_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysStyleId.Text <> "" Then

                If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNoRef.Text) & "' AND FNOrderType=22", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then

                    Call ClearDataNewStyle(True)
                    Call LoadDataStyleInfo()
                    Call InitData()

                Else
                    Call ClearDataNewStyle(True)
                End If

            Else
                Call ClearDataNewStyle(True)

            End If

        End If
    End Sub
End Class
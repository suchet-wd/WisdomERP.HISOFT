Public Class wJobCostTransactionDemandPullByParent

    Private _StateLoad As Boolean = False
    Private _FormLoad As Boolean = False
    Private _StateSaveJobType As Integer = 0 '0=Normal ,1 =Booking

    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ReposFTInvoiceDate

            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            ' AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave

        End With

        Call InitGrid()
        Call InitGridInvcharge()
        Call InitGridRawMatDetail()

    End Sub

#Region "Procedure"

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNExportQuantity|FNStockQuantity"
        Dim sFieldSumAmt As String = "FNExportAmt|FNExportAmtTHB"


        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNExportQuantity|FNStockQuantity"
        Dim sFieldGrpSumAmt As String = "FNExportAmt|FNExportAmtTHB"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv

            .ClearGrouping()
            .ClearDocument()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
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


        '------End Add Summary Grid-------------
    End Sub

    Private Sub InitGridInvcharge()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = "FNWageCost|FNProdCost|FNExportCost|FNTransportCost|FNTransportAirCost"


        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""
        Dim sFieldGrpSumAmt As String = "FNWageCost|FNProdCost|FNExportCost|FNTransportCost|FNTransportAirCost"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvinvcharge
            .ClearGrouping()
            .ClearDocument()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
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

        '------End Add Summary Grid-------------
    End Sub

    Private Sub InitGridRawMatDetail()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = "FNAmount|FNINVChargeAmt|FNNetAmount"


        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""
        Dim sFieldGrpSumAmt As String = "FNAmount|FNINVChargeAmt|FNNetAmount"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvdetail
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTMerMatTypeName").Group()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
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

            .ExpandAllGroups()
            .RefreshData()

        End With


        '------End Add Summary Grid-------------
    End Sub

#End Region

    Private Shared Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String

                Try

                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)

                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                    End If

                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate

                Try
                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                Catch ex As Exception
                End Try

                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub InitInvoice()
        Dim dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy

            With dt
                .BeginInit()

                For Each R As DataRow In .Select("FTInvoiceNo='' OR FDInvoiceDate=''")
                    R.Delete()
                Next

                .Rows.Add()
                .Rows(.Rows.Count - 1)!FTOrderNoRef = ""
                .Rows(.Rows.Count - 1)!FDShipDate = ""
                .Rows(.Rows.Count - 1)!FTCustomerPO = ""
                .Rows(.Rows.Count - 1)!FTInvoiceNo = ""
                .Rows(.Rows.Count - 1)!FDInvoiceDate = ""
                .Rows(.Rows.Count - 1)!FNExportQuantity = 0
                .Rows(.Rows.Count - 1)!FNStockQuantity = 0
                .Rows(.Rows.Count - 1)!FNPrice = 0
                .Rows(.Rows.Count - 1)!FNExchangeRate = 0
                .Rows(.Rows.Count - 1)!FNExportAmt = 0
                .Rows(.Rows.Count - 1)!FNExportAmtTHB = 0

                .EndInit()
            End With

            Me.ogc.DataSource = dt

        End With
    End Sub

    Private Sub CalculateCostData(OrderKey As String)
        Dim _Str As String
        Try
            Dim _dt As DataTable
            Dim Rind As Integer = 0
            Dim FNMonthSeq As Integer = 0
            Dim FNLastMonthSeq As Integer = 0
            Dim FTInvoiceMonth As String = ""

            _Str = vbCrLf & " Select  LEFT(FDInvoiceDate,7) AS FTInvoiceMonth ,0 AS FNMonthSeq"
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice"
            _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
            If Me.FTStateFinish.Checked Then
                _Str &= vbCrLf & " and  "
            End If
            _Str &= vbCrLf & "  GROUP BY  LEFT(FDInvoiceDate,7) "
            _Str &= vbCrLf & "  ORDER BY   LEFT(FDInvoiceDate,7) ASC  "

            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

            For Each R As DataRow In _dt.Rows
                Rind = Rind + 1


                R!FNMonthSeq = Rind
            Next

            FNLastMonthSeq = Rind


            For Each R As DataRow In _dt.Select("FNMonthSeq<" & Rind & "")
                FTInvoiceMonth = R!FTInvoiceMonth.ToString


                _Str = "SELECT TOP 1 FTInvoiceMonth  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly"
                _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                _Str &= vbCrLf & " AND  FTInvoiceMonth ='" & HI.UL.ULF.rpQuoted(FTInvoiceMonth) & "'"

                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "" Then
                    _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly ("
                    _Str &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTInvoiceMonth, FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, FNServiceCost, FNExportNetAmt,"
                    _Str &= vbCrLf & " FNEmbSubCost, FNPrintSubCost, FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost,"
                    _Str &= vbCrLf & "  FNTransportAirCost, FNOtherCost, FNNetCost, FNNetProfit, FNProfitPerPiece, FTRemark, FNExportQty, FNConductedCostPer, FNCommissionCostPer, FNAccFabStockOtherCost, FNFabricRcvCost,"
                    _Str &= vbCrLf & "   FNAccessroryRcvCost, FNFabricDummyCost, FNAccessroryDummyCost, FNFabricBalCost, FNAccessroryBalCost, FNAccFabStockRcvCost, FNAccFabStockOtherRcvCost, FNAccFabStockBalCost,"
                    _Str &= vbCrLf & "   FNAccFabStockOtherBalCost, FNExportNetCostAmt, FNNetProfitRcv, FNProfitPerPieceRcv, FNWageCutSew, FNWageCutTrim"

                    _Str &= vbCrLf & " )"

                    _Str &= vbCrLf & " SELECT TOP 1 "
                    _Str &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ", FTOrderNo"
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTInvoiceMonth) & "' AS  FTInvoiceMonth, FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, FNServiceCost, FNExportNetAmt,"
                    _Str &= vbCrLf & " FNEmbSubCost, FNPrintSubCost, FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost,"
                    _Str &= vbCrLf & "  FNTransportAirCost, FNOtherCost, FNNetCost, FNNetProfit, FNProfitPerPiece, FTRemark, FNExportQty, FNConductedCostPer, FNCommissionCostPer, FNAccFabStockOtherCost, FNFabricRcvCost,"
                    _Str &= vbCrLf & "   FNAccessroryRcvCost, FNFabricDummyCost, FNAccessroryDummyCost, FNFabricBalCost, FNAccessroryBalCost, FNAccFabStockRcvCost, FNAccFabStockOtherRcvCost, FNAccFabStockBalCost,"
                    _Str &= vbCrLf & "   FNAccFabStockOtherBalCost, FNExportNetCostAmt, FNNetProfitRcv, FNProfitPerPieceRcv, FNWageCutSew, FNWageCutTrim"

                    _Str &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost"
                    _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)
                End If

            Next
            FTInvoiceMonth = ""
            For Each R As DataRow In _dt.Select("FNMonthSeq=" & Rind & "")
                FTInvoiceMonth = R!FTInvoiceMonth.ToString

                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly"
                _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                _Str &= vbCrLf & " AND  FTInvoiceMonth ='" & HI.UL.ULF.rpQuoted(FTInvoiceMonth) & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

                _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly ("
                _Str &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTInvoiceMonth, FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, FNServiceCost, FNExportNetAmt,"
                _Str &= vbCrLf & " FNEmbSubCost, FNPrintSubCost, FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost,"
                _Str &= vbCrLf & "  FNTransportAirCost, FNOtherCost, FNNetCost, FNNetProfit, FNProfitPerPiece, FTRemark, FNExportQty, FNConductedCostPer, FNCommissionCostPer, FNAccFabStockOtherCost, FNFabricRcvCost,"
                _Str &= vbCrLf & "   FNAccessroryRcvCost, FNFabricDummyCost, FNAccessroryDummyCost, FNFabricBalCost, FNAccessroryBalCost, FNAccFabStockRcvCost, FNAccFabStockOtherRcvCost, FNAccFabStockBalCost,"
                _Str &= vbCrLf & "   FNAccFabStockOtherBalCost, FNExportNetCostAmt, FNNetProfitRcv, FNProfitPerPieceRcv, FNWageCutSew, FNWageCutTrim,FNWagePull"

                If Rind = 1 And Me.FTStateFinish.Checked Then

                    _Str &= vbCrLf & "  , FNCalExportAmt, FNCalFabricCost, FNCalAccessroryCost, FNCalAccFabStockCost, "
                    _Str &= vbCrLf & "    FNCalServiceCost, FNCalExportNetAmt, FNCalEmbSubCost, FNCalPrintSubCost, FNCalEmbBranchCost"
                    _Str &= vbCrLf & ", FNCalPrintBranchCost, FNCalEmbFacCost, FNCalWageCost, FNCalProdCost, FNCalConductedCost,"
                    _Str &= vbCrLf & "  FNCalCommissionCost, FNCalImportCost, FNCalExportCost, FNCalTransportCost"
                    _Str &= vbCrLf & ", FNCalTransportAirCost, FNCalOtherCost, FNCalNetCost, FNCalNetProfit, FNCalProfitPerPiece, FNCalExportQty,"
                    _Str &= vbCrLf & "  FNCalAccFabStockOtherCost, FNCalFabricRcvCost, FNCalAccessroryRcvCost, FNCalFabricDummyCost"
                    _Str &= vbCrLf & ", FNCalAccessroryDummyCost, FNCalFabricBalCost, FNCalAccessroryBalCost, FNCalAccFabStockRcvCost,"
                    _Str &= vbCrLf & "  FNCalAccFabStockOtherRcvCost, FNCalAccFabStockBalCost, FNCalAccFabStockOtherBalCost"
                    _Str &= vbCrLf & " , FNCalExportNetCostAmt, FNCalNetProfitRcv, FNCalProfitPerPieceRcv, FNCalWageCutSew, FNCalWageCutTrim,FNWagePullAct"

                End If

                _Str &= vbCrLf & " )"

                _Str &= vbCrLf & " SELECT TOP 1 "
                _Str &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & ", FTOrderNo"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTInvoiceMonth) & "' AS  FTInvoiceMonth, FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, FNServiceCost, FNExportNetAmt,"
                _Str &= vbCrLf & " FNEmbSubCost, FNPrintSubCost, FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost,"
                _Str &= vbCrLf & "  FNTransportAirCost, FNOtherCost, FNNetCost, FNNetProfit, FNProfitPerPiece, FTRemark, FNExportQty, FNConductedCostPer, FNCommissionCostPer, FNAccFabStockOtherCost, FNFabricRcvCost,"
                _Str &= vbCrLf & "   FNAccessroryRcvCost, FNFabricDummyCost, FNAccessroryDummyCost, FNFabricBalCost, FNAccessroryBalCost, FNAccFabStockRcvCost, FNAccFabStockOtherRcvCost, FNAccFabStockBalCost,"
                _Str &= vbCrLf & "   FNAccFabStockOtherBalCost, FNExportNetCostAmt, FNNetProfitRcv, FNProfitPerPieceRcv, FNWageCutSew, FNWageCutTrim,FNWagePull"

                If Rind = 1 And Me.FTStateFinish.Checked Then
                    _Str &= vbCrLf & "  , FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, "
                    _Str &= vbCrLf & "    FNServiceCost, FNExportNetAmt, FNEmbSubCost, FNPrintSubCost, FNEmbBranchCost"
                    _Str &= vbCrLf & ", FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost,"
                    _Str &= vbCrLf & "  FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost"
                    _Str &= vbCrLf & ", FNTransportAirCost, FNOtherCost, FNNetCost, FNNetProfit, FNProfitPerPiece, FNExportQty,"
                    _Str &= vbCrLf & "  FNAccFabStockOtherCost, FNFabricRcvCost, FNAccessroryRcvCost, FNFabricDummyCost"
                    _Str &= vbCrLf & ", FNAccessroryDummyCost, FNFabricBalCost, FNAccessroryBalCost, FNAccFabStockRcvCost,"
                    _Str &= vbCrLf & "  FNAccFabStockOtherRcvCost, FNAccFabStockBalCost, FNAccFabStockOtherBalCost"
                    _Str &= vbCrLf & " , FNExportNetCostAmt, FNNetProfitRcv, FNProfitPerPieceRcv, FNWageCutSew, FNWageCutTrim,FNWagePull"
                End If

                _Str &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost"
                _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

            Next

            If FTInvoiceMonth <> "" Then
                If Rind = 1 And Me.FTStateFinish.Checked Then
                Else

                    Dim dtallcal As DataTable
                    Dim dtall As DataTable

                    Dim _FNExportAmt, _FNFabricCost, _FNAccessroryCost, _FNAccFabStockCost, _FNServiceCost, _FNExportNetAmt, _FNEmbSubCost As Double
                    Dim _FNPrintSubCost, _FNEmbBranchCost, _FNPrintBranchCost, _FNEmbFacCost, _FNWageCost, _FNProdCost, _FNConductedCost As Double
                    Dim _FNCommissionCost, _FNImportCost, _FNExportCost, _FNTransportCost, _FNTransportAirCost As Double
                    Dim _FNOtherCost, _FNNetCost, _FNNetProfit, _FNProfitPerPiece, _FNExportQty, _FNConductedCostPer As Double
                    Dim _FNCommissionCostPer, _FNAccFabStockOtherCost, _FNFabricRcvCost, _FNAccessroryRcvCost As Double
                    Dim _FNFabricDummyCost, _FNAccessroryDummyCost, _FNFabricBalCost, _FNAccessroryBalCost, _FNAccFabStockRcvCost As Double
                    Dim _FNAccFabStockOtherRcvCost, _FNAccFabStockBalCost, _FNAccFabStockOtherBalCost As Double
                    Dim _FNExportNetCostAmt, _FNNetProfitRcv, _FNProfitPerPieceRcv, _FNWageCutSew, _FNWageCutTrim As Double
                    Dim _FNOrderQty As Integer
                    Dim FNTotalExportBFQty As Integer = 0
                    Dim FNTotalExportQty As Integer = 0
                    Dim FNTotalExportAmt As Double = 0
                    Dim TotalFNFabricCost As Double = 0
                    Dim TotalFNAccessroryCost As Double = 0
                    Dim TotalFNAccFabStockCost As Double = 0
                    Dim TotalFNServiceCost As Double = 0
                    Dim TotalFNEmbSubCost As Double = 0
                    Dim TotalFNPrintSubCost As Double = 0
                    Dim TotalFNEmbBranchCost As Double = 0
                    Dim TotalFNPrintBranchCost As Double = 0
                    Dim TotalFNEmbFacCost As Double = 0
                    Dim TotalFNConductedCost As Double = 0
                    Dim TotalFNCommissionCost As Double = 0
                    Dim TotalFNImportCost As Double = 0
                    Dim TotalFNOtherCost As Double = 0
                    Dim TotalFNConductedCostPer As Double = 0
                    Dim TotalFNCommissionCostPer As Double = 0
                    Dim TotalFNAccFabStockOtherCost As Double = 0
                    Dim TotalFNFabricRcvCost As Double = 0
                    Dim TotalFNAccessroryRcvCost As Double = 0
                    Dim TotalFNFabricDummyCost As Double = 0
                    Dim TotalFNAccessroryDummyCost As Double = 0
                    Dim TotalFNAccFabStockRcvCost As Double = 0
                    Dim TotalFNAccFabStockOtherRcvCost As Double = 0
                    Dim TotalFNWageCutSew As Double = 0
                    Dim TotalFNWageCutTrim As Double = 0
                    Dim FNMonthlyExportQty As Integer = 0
                    Dim FNMonthlyExportAmt As Double = 0
                    Dim _FNWagePull As Double = 0
                    Dim TotalFNWagePull As Double = 0

                    _FNOrderQty = FNOrderQuantity.Value
                    FNTotalExportQty = FNExportQty.Value
                    FNTotalExportAmt = FNExportAmt.Value

                    _Str = "SELECT TOP 0 "
                    _Str &= vbCrLf & "   FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, FNServiceCost, FNExportNetAmt, FNEmbSubCost,"
                    _Str &= vbCrLf & "FNPrintSubCost, FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost, FNTransportAirCost,"
                    _Str &= vbCrLf & "    FNOtherCost, FNNetCost, FNNetProfit, FNProfitPerPiece,FNExportQty, FNConductedCostPer, FNCommissionCostPer, FNAccFabStockOtherCost, FNFabricRcvCost, FNAccessroryRcvCost,"
                    _Str &= vbCrLf & "    FNFabricDummyCost, FNAccessroryDummyCost, FNFabricBalCost, FNAccessroryBalCost, FNAccFabStockRcvCost, FNAccFabStockOtherRcvCost, FNAccFabStockBalCost, FNAccFabStockOtherBalCost,"
                    _Str &= vbCrLf & "   FNExportNetCostAmt, FNNetProfitRcv, FNProfitPerPieceRcv, FNWageCutSew, FNWageCutTrim,FNExportQty AS FNTotalExportQty,FNExportQty AS FNOrderQty,0 AS FNMonthSeq,0 AS FNTotalExportBFQty,FNWagePull"
                    _Str &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost WITH(NOLOCK)"
                    _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                    dtallcal = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

                    _Str = "  SELECT A.FTOrderNo, A.FTInvoiceMonth, A.FNExportAmt, A.FNFabricCost, A.FNAccessroryCost, A.FNAccFabStockCost, A.FNServiceCost, A.FNExportNetAmt, "
                    _Str &= vbCrLf & "      A.FNEmbSubCost, A.FNPrintSubCost, A.FNEmbBranchCost, A.FNPrintBranchCost, A.FNEmbFacCost, A.FNConductedCost, A.FNCommissionCost, A.FNImportCost, "
                    _Str &= vbCrLf & "     A.FNOtherCost, A.FNNetCost, A.FNNetProfit, A.FNProfitPerPiece, A.FTRemark, A.FNExportQty, A.FNConductedCostPer, A.FNCommissionCostPer, A.FNAccFabStockOtherCost, A.FNFabricRcvCost, "
                    _Str &= vbCrLf & "     A.FNAccessroryRcvCost, A.FNFabricDummyCost, A.FNAccessroryDummyCost, A.FNFabricBalCost, A.FNAccessroryBalCost, A.FNAccFabStockRcvCost, A.FNAccFabStockOtherRcvCost, A.FNAccFabStockBalCost, "
                    _Str &= vbCrLf & "     A.FNAccFabStockOtherBalCost, A.FNExportNetCostAmt, A.FNNetProfitRcv, A.FNProfitPerPieceRcv, A.FNWageCutSew, A.FNWageCutTrim, A.FNCalExportAmt, A.FNCalFabricCost, A.FNCalAccessroryCost, A.FNCalAccFabStockCost, "
                    _Str &= vbCrLf & "     A.FNCalServiceCost, A.FNCalExportNetAmt, A.FNCalEmbSubCost, A.FNCalPrintSubCost, A.FNCalEmbBranchCost, A.FNCalPrintBranchCost, A.FNCalEmbFacCost, A.FNCalWageCost, A.FNCalProdCost, A.FNCalConductedCost, "
                    _Str &= vbCrLf & "     A.FNCalCommissionCost, A.FNCalImportCost, A.FNCalExportCost, A.FNCalTransportCost, A.FNCalTransportAirCost, A.FNCalOtherCost, A.FNCalNetCost, A.FNCalNetProfit, A.FNCalProfitPerPiece, A.FNCalExportQty, "
                    _Str &= vbCrLf & "     A.FNCalAccFabStockOtherCost, A.FNCalFabricRcvCost, A.FNCalAccessroryRcvCost, A.FNCalFabricDummyCost, A.FNCalAccessroryDummyCost, A.FNCalFabricBalCost, A.FNCalAccessroryBalCost, A.FNCalAccFabStockRcvCost, "
                    _Str &= vbCrLf & "     A.FNCalAccFabStockOtherRcvCost, A.FNCalAccFabStockBalCost, A.FNCalAccFabStockOtherBalCost, A.FNCalExportNetCostAmt, A.FNCalNetProfitRcv, A.FNCalProfitPerPieceRcv, A.FNCalWageCutSew, A.FNCalWageCutTrim,A.FNWagePull,A.FNWagePullAct"

                    _Str &= vbCrLf & " ,ISNULL(B.FNWageCost,0) AS FNWageCost"
                    _Str &= vbCrLf & ",ISNULL(B.FNProdCost,0) AS  FNProdCost"
                    _Str &= vbCrLf & " ,ISNULL(B.FNExportCost,0) AS  FNExportCost"
                    _Str &= vbCrLf & ",ISNULL(B.FNTransportCost,0) AS  FNTransportCost"
                    _Str &= vbCrLf & ",ISNULL(B.FNTransportAirCost,0) AS  FNTransportAirCost"
                    _Str &= vbCrLf & ",ISNULL(C.FNExportQuantity,0) AS  FNMonthExportQuantity"
                    _Str &= vbCrLf & ",ISNULL(C.FNExportAmtTHB,0) AS  FNMonthExportAmt,ISNULL(XBZ.FNMonthSeq,0) AS FNMonthSeq"
                    _Str &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly AS A WITH(NOLOCK) "
                    _Str &= vbCrLf & " LEFT OUTER JOIN ( "
                    _Str &= vbCrLf & "   SELECT AX.FTOrderNo, B.FTInvoiceMonth, SUM(AX.FNWageCost) AS FNWageCost, SUM(AX.FNProdCost) AS FNProdCost, SUM(AX.FNExportCost) AS FNExportCost, SUM(AX.FNTransportCost) AS FNTransportCost, "
                    _Str &= vbCrLf & "  SUM(AX.FNTransportAirCost) AS FNTransportAirCost"
                    _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice_Cost AS AX WITH(NOLOCK) INNER JOIN"
                    _Str &= vbCrLf & "   (SELECT FTOrderNo, FTInvoiceNo, LEFT(FDInvoiceDate, 7) AS FTInvoiceMonth"
                    _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS AM WITH(NOLOCK) "
                    _Str &= vbCrLf & "  WHERE AM.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                    _Str &= vbCrLf & "    GROUP BY FTOrderNo, FTInvoiceNo, LEFT(FDInvoiceDate, 7)) AS B ON AX.FTOrderNo = B.FTOrderNo AND AX.FTInvoiceNo = B.FTInvoiceNo"
                    _Str &= vbCrLf & "  WHERE AX.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                    _Str &= vbCrLf & " GROUP BY AX.FTOrderNo, B.FTInvoiceMonth"
                    _Str &= vbCrLf & " ) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTInvoiceMonth = B.FTInvoiceMonth"
                    _Str &= vbCrLf & " LEFT OUTER JOIN ( "

                    _Str &= vbCrLf & " SELECT FTOrderNo, LEFT(FDInvoiceDate, 7) AS FTInvoiceMonth"
                    _Str &= vbCrLf & " , SUM(FNExportQuantity) AS FNExportQuantity, SUM(FNStockQuantity) AS FNStockQuantity"
                    _Str &= vbCrLf & " , SUM(FNExportAmt) AS FNExportAmt, SUM(FNExportAmtTHB) "
                    _Str &= vbCrLf & "  AS FNExportAmtTHB"
                    _Str &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS AZ WITH(NOLOCK)"
                    _Str &= vbCrLf & "  WHERE AZ.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                    _Str &= vbCrLf & " GROUP BY FTOrderNo, LEFT(FDInvoiceDate, 7)"

                    _Str &= vbCrLf & " ) AS C ON A.FTOrderNo = C.FTOrderNo AND A.FTInvoiceMonth = C.FTInvoiceMonth"


                    _Str &= vbCrLf & " LEFT OUTER JOIN ( Select XX.FTOrderNo, LEFT(XX.FDInvoiceDate,7) AS FTInvoiceMonth ,ROW_NUMBER() OVER (ORDER BY LEFT(XX.FDInvoiceDate,7) ) AS FNMonthSeq"
                    _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS XX WITH(NOLOCK)"
                    _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                    _Str &= vbCrLf & "  GROUP BY  XX.FTOrderNo,LEFT(FDInvoiceDate,7) "
                    _Str &= vbCrLf & "  ) AS XBZ ON A.FTOrderNo = XBZ.FTOrderNo AND A.FTInvoiceMonth = XBZ.FTInvoiceMonth  "

                    _Str &= vbCrLf & "  WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
                    dtall = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

                    FNMonthSeq = 0
                    For Each R As DataRow In dtall.Select("FNMonthSeq>0", "FNMonthSeq")

                        FNMonthSeq = Val(R!FNMonthSeq)
                        _FNExportQty = Val(R!FNMonthExportQuantity.ToString)
                        _FNExportAmt = Val(R!FNMonthExportAmt.ToString)
                        FTInvoiceMonth = R!FTInvoiceMonth.ToString
                        _FNExportNetAmt = Val(R!FNExportNetAmt.ToString)

                        _FNNetCost = Val(R!FNNetCost.ToString)
                        _FNNetProfit = Val(R!FNNetProfit.ToString)
                        _FNProfitPerPiece = Val(R!FNProfitPerPiece.ToString)
                        _FNExportNetCostAmt = Val(R!FNExportNetCostAmt.ToString)
                        _FNNetProfitRcv = Val(R!FNNetProfitRcv.ToString)
                        _FNProfitPerPieceRcv = Val(R!FNProfitPerPieceRcv.ToString)
                        _FNAccFabStockBalCost = Val(R!FNAccFabStockBalCost.ToString)
                        _FNAccFabStockOtherBalCost = Val(R!FNAccFabStockOtherBalCost.ToString)
                        _FNFabricBalCost = Val(R!FNFabricBalCost.ToString)
                        _FNAccessroryBalCost = Val(R!FNAccessroryBalCost.ToString)

                        _FNWageCost = Val(R!FNWageCost.ToString)
                        _FNProdCost = Val(R!FNProdCost.ToString)
                        _FNExportCost = Val(R!FNExportCost.ToString)
                        _FNTransportCost = Val(R!FNTransportCost.ToString)
                        _FNTransportAirCost = Val(R!FNTransportAirCost.ToString)
                        _FNWagePull = Val(R!FNWagePull.ToString)

                        If FNMonthSeq = 1 Then

                            TotalFNFabricCost = Val(R!FNFabricCost.ToString)
                            TotalFNAccessroryCost = Val(R!FNAccessroryCost.ToString)
                            TotalFNAccFabStockCost = Val(R!FNAccFabStockCost.ToString)
                            TotalFNServiceCost = Val(R!FNServiceCost.ToString)
                            TotalFNEmbSubCost = Val(R!FNEmbSubCost.ToString)
                            TotalFNPrintSubCost = Val(R!FNPrintSubCost.ToString)
                            TotalFNEmbBranchCost = Val(R!FNEmbBranchCost.ToString)
                            TotalFNPrintBranchCost = Val(R!FNPrintBranchCost.ToString)
                            TotalFNEmbFacCost = Val(R!FNEmbFacCost.ToString)
                            TotalFNConductedCost = Val(R!FNConductedCost.ToString)
                            TotalFNCommissionCost = Val(R!FNCommissionCost.ToString)
                            TotalFNImportCost = Val(R!FNImportCost.ToString)
                            TotalFNOtherCost = Val(R!FNOtherCost.ToString)
                            TotalFNConductedCostPer = Val(R!FNConductedCostPer.ToString)
                            TotalFNCommissionCostPer = Val(R!FNCommissionCostPer.ToString)
                            TotalFNAccFabStockOtherCost = Val(R!FNAccFabStockOtherCost.ToString)
                            TotalFNFabricRcvCost = Val(R!FNFabricRcvCost.ToString)
                            TotalFNAccessroryRcvCost = Val(R!FNAccessroryRcvCost.ToString)
                            TotalFNFabricDummyCost = Val(R!FNFabricDummyCost.ToString)
                            TotalFNAccessroryDummyCost = Val(R!FNAccessroryDummyCost.ToString)
                            TotalFNAccFabStockRcvCost = Val(R!FNAccFabStockRcvCost.ToString)
                            TotalFNAccFabStockOtherRcvCost = Val(R!FNAccFabStockOtherRcvCost.ToString)
                            TotalFNWageCutSew = Val(R!FNWageCutSew.ToString)
                            TotalFNWageCutTrim = Val(R!FNWageCutTrim.ToString)
                            TotalFNWagePull = Val(R!FNWagePull.ToString)

                            _FNFabricCost = Val(R!FNFabricCost.ToString)
                            _FNAccessroryCost = Val(R!FNAccessroryCost.ToString)
                            _FNAccFabStockCost = Val(R!FNAccFabStockCost.ToString)
                            _FNServiceCost = Val(R!FNServiceCost.ToString)
                            _FNEmbSubCost = Val(R!FNEmbSubCost.ToString)
                            _FNPrintSubCost = Val(R!FNPrintSubCost.ToString)
                            _FNEmbBranchCost = Val(R!FNEmbBranchCost.ToString)
                            _FNPrintBranchCost = Val(R!FNPrintBranchCost.ToString)
                            _FNEmbFacCost = Val(R!FNEmbFacCost.ToString)
                            _FNConductedCost = Val(R!FNConductedCost.ToString)
                            _FNCommissionCost = Val(R!FNCommissionCost.ToString)
                            _FNImportCost = Val(R!FNImportCost.ToString)
                            _FNOtherCost = Val(R!FNOtherCost.ToString)
                            _FNConductedCostPer = Val(R!FNConductedCostPer.ToString)
                            _FNCommissionCostPer = Val(R!FNCommissionCostPer.ToString)
                            _FNAccFabStockOtherCost = Val(R!FNAccFabStockOtherCost.ToString)
                            _FNFabricRcvCost = Val(R!FNFabricRcvCost.ToString)
                            _FNAccessroryRcvCost = Val(R!FNAccessroryRcvCost.ToString)
                            _FNFabricDummyCost = Val(R!FNFabricDummyCost.ToString)
                            _FNAccessroryDummyCost = Val(R!FNAccessroryDummyCost.ToString)
                            _FNAccFabStockRcvCost = Val(R!FNAccFabStockRcvCost.ToString)
                            _FNAccFabStockOtherRcvCost = Val(R!FNAccFabStockOtherRcvCost.ToString)
                            _FNWageCutSew = Val(R!FNWageCutSew.ToString)
                            _FNWageCutTrim = Val(R!FNWageCutTrim.ToString)

                            dtallcal.Rows.Add(_FNExportAmt, _FNFabricCost, _FNAccessroryCost, _FNAccFabStockCost, _FNServiceCost, _FNExportNetAmt, _FNEmbSubCost,
                                                                                               _FNPrintSubCost, _FNEmbBranchCost, _FNPrintBranchCost, _FNEmbFacCost, _FNWageCost, _FNProdCost, _FNConductedCost,
                                                                                               _FNCommissionCost, _FNImportCost, _FNExportCost, _FNTransportCost, _FNTransportAirCost,
                                                                                               _FNOtherCost, _FNNetCost, _FNNetProfit, _FNProfitPerPiece, _FNExportQty, _FNConductedCostPer,
                                                                                               _FNCommissionCostPer, _FNAccFabStockOtherCost, _FNFabricRcvCost, _FNAccessroryRcvCost,
                                                                                               _FNFabricDummyCost, _FNAccessroryDummyCost, _FNFabricBalCost, _FNAccessroryBalCost, _FNAccFabStockRcvCost,
                                                                                               _FNAccFabStockOtherRcvCost, _FNAccFabStockBalCost, _FNAccFabStockOtherBalCost,
                                                                                               _FNExportNetCostAmt, _FNNetProfitRcv, _FNProfitPerPieceRcv, _FNWageCutSew, _FNWageCutTrim, FNTotalExportQty, _FNOrderQty, FNMonthSeq, FNTotalExportBFQty, _FNWagePull)


                        Else

                            If TotalFNWagePull < Val(R!FNWagePull.ToString) Then
                                _FNWagePull = Val(R!FNWagePull.ToString) - TotalFNWagePull
                            Else
                                _FNWagePull = 0
                            End If

                            If TotalFNFabricCost < Val(R!FNFabricCost.ToString) Then
                                _FNFabricCost = Val(R!FNFabricCost.ToString) - TotalFNFabricCost
                            Else
                                _FNFabricCost = 0
                            End If

                            If TotalFNAccessroryCost < Val(R!FNAccessroryCost.ToString) Then
                                _FNAccessroryCost = Val(R!FNAccessroryCost.ToString) - TotalFNAccessroryCost
                            Else
                                _FNAccessroryCost = 0
                            End If

                            If TotalFNAccFabStockCost < Val(R!FNAccFabStockCost.ToString) Then
                                _FNAccFabStockCost = Val(R!FNAccFabStockCost.ToString) - TotalFNAccFabStockCost
                            Else
                                _FNAccFabStockCost = 0
                            End If

                            If TotalFNServiceCost < Val(R!FNServiceCost.ToString) Then
                                _FNServiceCost = Val(R!FNServiceCost.ToString) - TotalFNServiceCost
                            Else
                                _FNServiceCost = 0
                            End If

                            If TotalFNEmbSubCost < Val(R!FNEmbSubCost.ToString) Then
                                _FNEmbSubCost = Val(R!FNEmbSubCost.ToString) - TotalFNEmbSubCost
                            Else
                                _FNEmbSubCost = 0
                            End If

                            If TotalFNPrintSubCost < Val(R!FNPrintSubCost.ToString) Then
                                _FNPrintSubCost = Val(R!FNPrintSubCost.ToString) - TotalFNPrintSubCost
                            Else
                                _FNPrintSubCost = 0
                            End If

                            If TotalFNEmbBranchCost < Val(R!FNEmbBranchCost.ToString) Then
                                _FNEmbBranchCost = Val(R!FNEmbBranchCost.ToString) - TotalFNEmbBranchCost
                            Else
                                _FNEmbBranchCost = 0
                            End If

                            If TotalFNPrintBranchCost < Val(R!FNPrintBranchCost.ToString) Then
                                _FNPrintBranchCost = Val(R!FNPrintBranchCost.ToString) - TotalFNPrintBranchCost
                            Else
                                _FNPrintBranchCost = 0
                            End If

                            If TotalFNEmbFacCost < Val(R!FNEmbFacCost.ToString) Then
                                _FNEmbFacCost = Val(R!FNEmbFacCost.ToString) - TotalFNEmbFacCost
                            Else
                                _FNEmbFacCost = 0
                            End If

                            If TotalFNConductedCost < Val(R!FNConductedCost.ToString) Then
                                _FNConductedCost = Val(R!FNConductedCost.ToString) - TotalFNConductedCost
                            Else
                                _FNConductedCost = 0
                            End If

                            If TotalFNCommissionCost < Val(R!FNCommissionCost.ToString) Then
                                _FNCommissionCost = Val(R!FNCommissionCost.ToString) - TotalFNCommissionCost
                            Else
                                _FNCommissionCost = 0
                            End If

                            If TotalFNImportCost < Val(R!FNImportCost.ToString) Then
                                _FNImportCost = Val(R!FNImportCost.ToString) - TotalFNImportCost
                            Else
                                _FNImportCost = 0
                            End If

                            If TotalFNOtherCost < Val(R!FNOtherCost.ToString) Then
                                _FNOtherCost = Val(R!FNOtherCost.ToString) - TotalFNOtherCost
                            Else
                                _FNOtherCost = 0
                            End If

                            If TotalFNConductedCostPer < Val(R!FNConductedCostPer.ToString) Then
                                _FNConductedCostPer = Val(R!FNConductedCostPer.ToString) - TotalFNConductedCostPer
                            Else
                                _FNConductedCostPer = 0
                            End If

                            If TotalFNCommissionCostPer < Val(R!FNCommissionCostPer.ToString) Then
                                _FNCommissionCostPer = Val(R!FNCommissionCostPer.ToString) - TotalFNCommissionCostPer
                            Else
                                _FNCommissionCostPer = 0
                            End If

                            If TotalFNAccFabStockOtherCost < Val(R!FNAccFabStockOtherCost.ToString) Then
                                _FNAccFabStockOtherCost = Val(R!FNAccFabStockOtherCost.ToString) - TotalFNAccFabStockOtherCost
                            Else
                                _FNAccFabStockOtherCost = 0
                            End If

                            If TotalFNFabricRcvCost < Val(R!FNFabricRcvCost.ToString) Then
                                _FNFabricRcvCost = Val(R!FNFabricRcvCost.ToString) - TotalFNFabricRcvCost
                            Else
                                _FNFabricRcvCost = 0
                            End If

                            If TotalFNAccessroryRcvCost < Val(R!FNAccessroryRcvCost.ToString) Then
                                _FNAccessroryRcvCost = Val(R!FNAccessroryRcvCost.ToString) - TotalFNAccessroryRcvCost
                            Else
                                _FNAccessroryRcvCost = 0
                            End If

                            If TotalFNFabricDummyCost < Val(R!FNFabricDummyCost.ToString) Then
                                _FNFabricDummyCost = Val(R!FNFabricDummyCost.ToString) - TotalFNFabricDummyCost
                            Else
                                _FNFabricDummyCost = 0
                            End If

                            If TotalFNAccessroryDummyCost < Val(R!FNAccessroryDummyCost.ToString) Then
                                _FNAccessroryDummyCost = Val(R!FNAccessroryDummyCost.ToString) - TotalFNAccessroryDummyCost
                            Else
                                _FNAccessroryDummyCost = 0
                            End If

                            If TotalFNAccFabStockRcvCost < Val(R!FNAccFabStockRcvCost.ToString) Then
                                _FNAccFabStockRcvCost = Val(R!FNAccFabStockRcvCost.ToString) - TotalFNAccFabStockRcvCost
                            Else
                                _FNAccFabStockRcvCost = 0
                            End If

                            If TotalFNAccFabStockOtherRcvCost < Val(R!FNAccFabStockOtherRcvCost.ToString) Then
                                _FNAccFabStockOtherRcvCost = Val(R!FNAccFabStockOtherRcvCost.ToString) - TotalFNAccFabStockOtherRcvCost
                            Else
                                _FNAccFabStockOtherRcvCost = 0
                            End If

                            If TotalFNWageCutSew < Val(R!FNWageCutSew.ToString) Then
                                _FNWageCutSew = Val(R!FNWageCutSew.ToString) - TotalFNWageCutSew
                            Else
                                _FNWageCutSew = 0
                            End If

                            If TotalFNWageCutTrim < Val(R!FNWageCutTrim.ToString) Then
                                _FNWageCutTrim = Val(R!FNWageCutTrim.ToString) - TotalFNWageCutTrim
                            Else
                                _FNWageCutTrim = 0
                            End If

                            TotalFNFabricCost = Val(R!FNFabricCost.ToString)
                            TotalFNFabricCost = Val(R!FNFabricCost.ToString)
                            TotalFNAccessroryCost = Val(R!FNAccessroryCost.ToString)
                            TotalFNAccFabStockCost = Val(R!FNAccFabStockCost.ToString)
                            TotalFNServiceCost = Val(R!FNServiceCost.ToString)
                            TotalFNEmbSubCost = Val(R!FNEmbSubCost.ToString)
                            TotalFNPrintSubCost = Val(R!FNPrintSubCost.ToString)
                            TotalFNEmbBranchCost = Val(R!FNEmbBranchCost.ToString)
                            TotalFNPrintBranchCost = Val(R!FNPrintBranchCost.ToString)
                            TotalFNEmbFacCost = Val(R!FNEmbFacCost.ToString)
                            TotalFNConductedCost = Val(R!FNConductedCost.ToString)
                            TotalFNCommissionCost = Val(R!FNCommissionCost.ToString)
                            TotalFNImportCost = Val(R!FNImportCost.ToString)
                            TotalFNOtherCost = Val(R!FNOtherCost.ToString)
                            TotalFNConductedCostPer = Val(R!FNConductedCostPer.ToString)
                            TotalFNCommissionCostPer = Val(R!FNCommissionCostPer.ToString)
                            TotalFNAccFabStockOtherCost = Val(R!FNAccFabStockOtherCost.ToString)
                            TotalFNFabricRcvCost = Val(R!FNFabricRcvCost.ToString)
                            TotalFNAccessroryRcvCost = Val(R!FNAccessroryRcvCost.ToString)
                            TotalFNFabricDummyCost = Val(R!FNFabricDummyCost.ToString)
                            TotalFNAccessroryDummyCost = Val(R!FNAccessroryDummyCost.ToString)
                            TotalFNAccFabStockRcvCost = Val(R!FNAccFabStockRcvCost.ToString)
                            TotalFNAccFabStockOtherRcvCost = Val(R!FNAccFabStockOtherRcvCost.ToString)
                            TotalFNWageCutSew = Val(R!FNWageCutSew.ToString)
                            TotalFNWageCutTrim = Val(R!FNWageCutTrim.ToString)

                            dtallcal.Rows.Add(_FNExportAmt, _FNFabricCost, _FNAccessroryCost, _FNAccFabStockCost, _FNServiceCost, _FNExportNetAmt, _FNEmbSubCost,
                                             _FNPrintSubCost, _FNEmbBranchCost, _FNPrintBranchCost, _FNEmbFacCost, _FNWageCost, _FNProdCost, _FNConductedCost,
                                             _FNCommissionCost, _FNImportCost, _FNExportCost, _FNTransportCost, _FNTransportAirCost,
                                             _FNOtherCost, _FNNetCost, _FNNetProfit, _FNProfitPerPiece, _FNExportQty, _FNConductedCostPer,
                                             _FNCommissionCostPer, _FNAccFabStockOtherCost, _FNFabricRcvCost, _FNAccessroryRcvCost,
                                             _FNFabricDummyCost, _FNAccessroryDummyCost, _FNFabricBalCost, _FNAccessroryBalCost, _FNAccFabStockRcvCost,
                                             _FNAccFabStockOtherRcvCost, _FNAccFabStockBalCost, _FNAccFabStockOtherBalCost,
                                             _FNExportNetCostAmt, _FNNetProfitRcv, _FNProfitPerPieceRcv, _FNWageCutSew, _FNWageCutTrim, FNTotalExportQty, _FNOrderQty, FNMonthSeq, FNTotalExportBFQty, _FNWagePull)


                        End If

                        FNTotalExportBFQty = FNTotalExportBFQty + Val(R!FNMonthExportQuantity)

                    Next

                    FNTotalExportQty = FNOrderQuantity.Value  ' FNExportQty.Value
                    FNTotalExportAmt = FNExportGAmt.Value


                    Dim _GFNFabricCost As Double = 0
                    Dim _GFNAccessroryCost As Double = 0
                    Dim _GFNAccFabStockCost As Double = 0
                    Dim _GFNServiceCost As Double = 0
                    Dim _GFNEmbSubCost As Double = 0
                    Dim _GFNPrintSubCost As Double = 0
                    Dim _GFNEmbBranchCost As Double = 0
                    Dim _GFNPrintBranchCost As Double = 0
                    Dim _GFNEmbFacCost As Double = 0
                    Dim _GFNConductedCost As Double = 0
                    Dim _GFNCommissionCost As Double = 0
                    Dim _GFNImportCost As Double = 0
                    Dim _GFNOtherCost As Double = 0
                    Dim _GFNAccFabStockOtherCost As Double = 0
                    Dim _GFNFabricRcvCost As Double = 0
                    Dim _GFNAccessroryRcvCost As Double = 0
                    Dim _GFNFabricDummyCost As Double = 0
                    Dim _GFNAccessroryDummyCost As Double = 0
                    Dim _GFNAccFabStockRcvCost As Double = 0
                    Dim _GFNAccFabStockOtherRcvCost As Double = 0
                    Dim _GFNWageCutSew As Double = 0
                    Dim _GFNWageCutTrim As Double = 0
                    Dim _GFNWagePull As Double = 0

                    For Each R As DataRow In dtallcal.Select("FNMonthSeq>0", "FNMonthSeq")

                        _GFNWagePull = _GFNWagePull + Val(R!FNWagePull.ToString)
                        _GFNFabricCost = _GFNFabricCost + Val(R!FNFabricCost.ToString)
                        _GFNAccessroryCost = _GFNAccessroryCost + Val(R!FNAccessroryCost.ToString)
                        _GFNAccFabStockCost = _GFNAccFabStockCost + Val(R!FNAccFabStockCost.ToString)
                        _GFNServiceCost = _GFNServiceCost + Val(R!FNServiceCost.ToString)
                        _GFNEmbSubCost = _GFNEmbSubCost + Val(R!FNEmbSubCost.ToString)
                        _GFNPrintSubCost = _GFNPrintSubCost + Val(R!FNPrintSubCost.ToString)
                        _GFNEmbBranchCost = _GFNEmbBranchCost + Val(R!FNEmbBranchCost.ToString)
                        _GFNPrintBranchCost = _GFNPrintBranchCost + Val(R!FNPrintBranchCost.ToString)
                        _GFNEmbFacCost = _GFNEmbFacCost + Val(R!FNEmbFacCost.ToString)
                        _GFNConductedCost = _GFNConductedCost + Val(R!FNConductedCost.ToString)
                        _GFNCommissionCost = _GFNCommissionCost + Val(R!FNCommissionCost.ToString)
                        _GFNImportCost = _GFNImportCost + Val(R!FNImportCost.ToString)
                        _GFNOtherCost = _GFNOtherCost + Val(R!FNOtherCost.ToString)
                        _GFNAccFabStockOtherCost = _GFNAccFabStockOtherCost + Val(R!FNAccFabStockOtherCost.ToString)
                        _GFNFabricRcvCost = _GFNFabricRcvCost + Val(R!FNFabricRcvCost.ToString)
                        _GFNAccessroryRcvCost = _GFNAccessroryRcvCost + Val(R!FNAccessroryRcvCost.ToString)
                        _GFNFabricDummyCost = _GFNFabricDummyCost + Val(R!FNFabricDummyCost.ToString)
                        _GFNAccessroryDummyCost = _GFNAccessroryDummyCost + Val(R!FNAccessroryDummyCost.ToString)
                        _GFNAccFabStockRcvCost = _GFNAccFabStockRcvCost + Val(R!FNAccFabStockRcvCost.ToString)
                        _GFNAccFabStockOtherRcvCost = _GFNAccFabStockOtherRcvCost + Val(R!FNAccFabStockOtherRcvCost.ToString)
                        _GFNWageCutSew = _GFNWageCutSew + Val(R!FNWageCutSew.ToString)
                        _GFNWageCutTrim = _GFNWageCutTrim + Val(R!FNWageCutTrim.ToString)
                    Next


                    For Each Rm As DataRow In dtall.Select("FNMonthSeq>0", "FNMonthSeq")
                        FNMonthSeq = Val(Rm!FNMonthSeq)

                        '_FNExportQty = Val(Rm!FNMonthExportQuantity.ToString)
                        '_FNExportAmt = Val(Rm!FNMonthExportAmt.ToString)

                        FNMonthlyExportQty = Val(Rm!FNMonthExportQuantity.ToString)
                        FNMonthlyExportAmt = Val(Rm!FNMonthExportAmt.ToString)

                        FTInvoiceMonth = Rm!FTInvoiceMonth.ToString

                        _FNExportNetAmt = Val(Rm!FNExportNetAmt.ToString)

                        _FNWageCost = Val(Rm!FNWageCost.ToString)
                        _FNProdCost = Val(Rm!FNProdCost.ToString)
                        _FNExportCost = Val(Rm!FNExportCost.ToString)
                        _FNTransportCost = Val(Rm!FNTransportCost.ToString)
                        _FNTransportAirCost = Val(Rm!FNTransportAirCost.ToString)

                        TotalFNFabricCost = 0
                        TotalFNAccessroryCost = 0
                        TotalFNAccFabStockCost = 0
                        TotalFNServiceCost = 0
                        TotalFNEmbSubCost = 0
                        TotalFNPrintSubCost = 0
                        TotalFNEmbBranchCost = 0
                        TotalFNPrintBranchCost = 0
                        TotalFNEmbFacCost = 0
                        TotalFNConductedCost = 0
                        TotalFNCommissionCost = 0
                        TotalFNImportCost = 0
                        TotalFNOtherCost = 0
                        TotalFNConductedCostPer = 0
                        TotalFNCommissionCostPer = 0
                        TotalFNAccFabStockOtherCost = 0
                        TotalFNFabricRcvCost = 0
                        TotalFNAccessroryRcvCost = 0
                        TotalFNFabricDummyCost = 0
                        TotalFNAccessroryDummyCost = 0
                        TotalFNAccFabStockRcvCost = 0
                        TotalFNAccFabStockOtherRcvCost = 0
                        TotalFNWageCutSew = 0
                        TotalFNWageCutTrim = 0
                        TotalFNWagePull = 0

                        For Each R As DataRow In dtallcal.Select("FNMonthSeq<=" & FNMonthSeq & "", "FNMonthSeq")

                            _FNExportQty = FNMonthlyExportQty 'Val(R!FNExportQty)

                            FNTotalExportBFQty = Val(R!FNTotalExportBFQty.ToString)

                            _FNWagePull = Val(R!FNWagePull.ToString)
                            _FNFabricCost = Val(R!FNFabricCost.ToString)
                            _FNAccessroryCost = Val(R!FNAccessroryCost.ToString)
                            _FNAccFabStockCost = Val(R!FNAccFabStockCost.ToString)
                            _FNServiceCost = Val(R!FNServiceCost.ToString)
                            _FNEmbSubCost = Val(R!FNEmbSubCost.ToString)
                            _FNPrintSubCost = Val(R!FNPrintSubCost.ToString)
                            _FNEmbBranchCost = Val(R!FNEmbBranchCost.ToString)
                            _FNPrintBranchCost = Val(R!FNPrintBranchCost.ToString)
                            _FNEmbFacCost = Val(R!FNEmbFacCost.ToString)
                            _FNConductedCost = Val(R!FNConductedCost.ToString)
                            _FNCommissionCost = Val(R!FNCommissionCost.ToString)
                            _FNImportCost = Val(R!FNImportCost.ToString)
                            _FNOtherCost = Val(R!FNOtherCost.ToString)
                            _FNAccFabStockOtherCost = Val(R!FNAccFabStockOtherCost.ToString)
                            _FNFabricRcvCost = Val(R!FNFabricRcvCost.ToString)
                            _FNAccessroryRcvCost = Val(R!FNAccessroryRcvCost.ToString)
                            _FNFabricDummyCost = Val(R!FNFabricDummyCost.ToString)
                            _FNAccessroryDummyCost = Val(R!FNAccessroryDummyCost.ToString)
                            _FNAccFabStockRcvCost = Val(R!FNAccFabStockRcvCost.ToString)
                            _FNAccFabStockOtherRcvCost = Val(R!FNAccFabStockOtherRcvCost.ToString)
                            _FNWageCutSew = Val(R!FNWageCutSew.ToString)
                            _FNWageCutTrim = Val(R!FNWageCutTrim.ToString)

                            If _FNFabricCost > 0 And (FNMonthSeq <> FNLastMonthSeq Or FTStateFinish.Checked = False) Then
                                _FNFabricCost = Double.Parse(Format(((_FNFabricCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNWagePull > 0 And (FNMonthSeq <> FNLastMonthSeq Or FTStateFinish.Checked = False) Then
                                'Modift by Num 20161004 Request By Pung Pool / Wasted Fix By Month
                                _FNWagePull = Double.Parse(Format(_FNWagePull, "0.00"))
                                ' _FNWagePull = Double.Parse(Format(((_FNWagePull * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNAccessroryCost > 0 Then
                                _FNAccessroryCost = Double.Parse(Format(((_FNAccessroryCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNAccFabStockCost > 0 Then
                                _FNAccFabStockCost = Double.Parse(Format(((_FNAccFabStockCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNServiceCost > 0 Then
                                _FNServiceCost = Double.Parse(Format(((_FNServiceCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))

                            End If

                            If _FNEmbSubCost > 0 Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNEmbSubCost = Double.Parse(Format(((_FNEmbSubCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                ' _FNEmbSubCost = Double.Parse(Format(((_FNEmbSubCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNEmbSubCost = Double.Parse(Format(_FNEmbSubCost, "0.00"))
                            End If

                            If _FNPrintSubCost > 0 Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNPrintSubCost = Double.Parse(Format(((_FNPrintSubCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNPrintSubCost = Double.Parse(Format(((_FNPrintSubCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                ' _FNPrintSubCost = Double.Parse(Format(_FNPrintSubCost, "0.00"))
                            End If

                            If _FNEmbBranchCost > 0 Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNEmbBranchCost = Double.Parse(Format(((_FNEmbBranchCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNEmbBranchCost = Double.Parse(Format(((_FNEmbBranchCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNEmbBranchCost = Double.Parse(Format(_FNEmbBranchCost, "0.00"))
                            End If

                            If _FNPrintBranchCost Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNPrintBranchCost = Double.Parse(Format(((_FNPrintBranchCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNPrintBranchCost = Double.Parse(Format(((_FNPrintBranchCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNPrintBranchCost = Double.Parse(Format(_FNPrintBranchCost, "0.00"))
                            End If

                            If _FNEmbFacCost > 0 Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNEmbFacCost = Double.Parse(Format(((_FNEmbFacCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNEmbFacCost = Double.Parse(Format(((_FNEmbFacCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                ' _FNEmbFacCost = Double.Parse(Format(_FNEmbFacCost, "0.00"))
                            End If

                            If _FNConductedCost > 0 Then
                                '_FNConductedCost = Double.Parse(Format(((_FNConductedCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                _FNConductedCost = Double.Parse(Format(_FNConductedCost, "0.00"))
                            End If

                            If _FNCommissionCost > 0 Then
                                '_FNCommissionCost = Double.Parse(Format(((_FNCommissionCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                _FNCommissionCost = Double.Parse(Format(_FNCommissionCost, "0.00"))
                            End If

                            If _FNImportCost > 0 Then
                                _FNImportCost = Double.Parse(Format(((_FNImportCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNOtherCost > 0 Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNOtherCost = Double.Parse(Format(((_FNOtherCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNOtherCost = Double.Parse(Format(((_FNOtherCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                ' _FNOtherCost = Double.Parse(Format(_FNOtherCost, "0.00"))
                            End If

                            If _FNAccFabStockOtherCost > 0 Then
                                _FNAccFabStockOtherCost = Double.Parse(Format(((_FNAccFabStockOtherCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNFabricRcvCost > 0 Then
                                _FNFabricRcvCost = Double.Parse(Format(((_FNFabricRcvCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNAccessroryRcvCost > 0 Then
                                _FNAccessroryRcvCost = Double.Parse(Format(((_FNAccessroryRcvCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNFabricDummyCost > 0 Then
                                _FNFabricDummyCost = Double.Parse(Format(((_FNFabricDummyCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNAccessroryDummyCost > 0 Then
                                _FNAccessroryDummyCost = Double.Parse(Format(((_FNAccessroryDummyCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNAccFabStockRcvCost > 0 Then
                                _FNAccFabStockRcvCost = Double.Parse(Format(((_FNAccFabStockRcvCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNAccFabStockOtherRcvCost > 0 Then
                                _FNAccFabStockOtherRcvCost = Double.Parse(Format(((_FNAccFabStockOtherRcvCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNWageCutSew > 0 Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNWageCutSew = Double.Parse(Format(((_FNWageCutSew * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNWageCutSew = Double.Parse(Format(((_FNWageCutSew * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                ' _FNWageCutSew = Double.Parse(Format(_FNWageCutSew, "0.00"))
                            End If

                            If _FNWageCutTrim > 0 Then
                                'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                                _FNWageCutTrim = Double.Parse(Format(((_FNWageCutTrim * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNWageCutTrim = Double.Parse(Format(((_FNWageCutTrim * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                                '_FNWageCutTrim = Double.Parse(Format(_FNWageCutTrim, "0.00"))
                            End If

                            'Modift by Num 20161004 Request By Pung Change to Fix by Month
                            TotalFNWagePull = TotalFNWagePull + _FNWagePull
                            'TotalFNWagePull = _FNWagePull
                            'Modift by Num 20161004 Request By Pung Change  to Fix by Month

                            TotalFNFabricCost = TotalFNFabricCost + _FNFabricCost
                            TotalFNAccessroryCost = TotalFNAccessroryCost + _FNAccessroryCost
                            TotalFNAccFabStockCost = TotalFNAccFabStockCost + _FNAccFabStockCost
                            TotalFNServiceCost = TotalFNServiceCost + _FNServiceCost

                            'Modift by Num 20161004 Request By Pung Change to Calculate By Export

                            '   TotalFNOtherCost = TotalFNOtherCost + _FNOtherCost
                            TotalFNEmbSubCost = TotalFNEmbSubCost + _FNEmbSubCost
                            TotalFNPrintSubCost = TotalFNPrintSubCost + _FNPrintSubCost
                            TotalFNEmbBranchCost = TotalFNEmbBranchCost + _FNEmbBranchCost
                            TotalFNPrintBranchCost = TotalFNPrintBranchCost + _FNPrintBranchCost
                            TotalFNEmbFacCost = TotalFNEmbFacCost + _FNEmbFacCost

                            'TotalFNEmbSubCost = TotalFNEmbSubCost + _FNEmbSubCost
                            'TotalFNPrintSubCost = TotalFNPrintSubCost + _FNPrintSubCost
                            'TotalFNEmbBranchCost = TotalFNEmbBranchCost + _FNEmbBranchCost
                            'TotalFNPrintBranchCost = TotalFNPrintBranchCost + _FNPrintBranchCost
                            'TotalFNEmbFacCost = TotalFNEmbFacCost + _FNEmbFacCost
                            TotalFNConductedCost = TotalFNConductedCost + _FNConductedCost
                            TotalFNCommissionCost = TotalFNCommissionCost + _FNCommissionCost
                            'TotalFNOtherCost = TotalFNOtherCost + _FNOtherCost

                            TotalFNOtherCost = _FNOtherCost
                            'TotalFNEmbSubCost = _FNEmbSubCost
                            'TotalFNPrintSubCost = _FNPrintSubCost
                            'TotalFNEmbBranchCost = _FNEmbBranchCost
                            'TotalFNPrintBranchCost = _FNPrintBranchCost
                            'TotalFNEmbFacCost = _FNEmbFacCost
                            'TotalFNConductedCost = _FNConductedCost
                            'TotalFNCommissionCost = _FNCommissionCost



                            'Modift by Num 20161004 Request By Pung Change to Calculate By Export


                            TotalFNImportCost = TotalFNImportCost + _FNImportCost

                            TotalFNAccFabStockOtherCost = TotalFNAccFabStockOtherCost + _FNAccFabStockOtherCost
                            TotalFNFabricRcvCost = TotalFNFabricRcvCost + _FNFabricRcvCost
                            TotalFNAccessroryRcvCost = TotalFNAccessroryRcvCost + _FNAccessroryRcvCost
                            TotalFNFabricDummyCost = TotalFNFabricDummyCost + _FNFabricDummyCost
                            TotalFNAccessroryDummyCost = TotalFNAccessroryDummyCost + _FNAccessroryDummyCost
                            TotalFNAccFabStockRcvCost = TotalFNAccFabStockRcvCost + _FNAccFabStockRcvCost
                            TotalFNAccFabStockOtherRcvCost = TotalFNAccFabStockOtherRcvCost + _FNAccFabStockOtherRcvCost

                            'Modift by Num 20161004 Request By Pung Change to Calculate By Export
                            TotalFNWageCutSew = TotalFNWageCutSew + _FNWageCutSew
                            TotalFNWageCutTrim = TotalFNWageCutTrim + _FNWageCutTrim

                            'TotalFNWageCutSew = TotalFNWageCutSew + _FNWageCutSew
                            'TotalFNWageCutTrim = TotalFNWageCutTrim + _FNWageCutTrim

                            'TotalFNWageCutSew = _FNWageCutSew
                            'TotalFNWageCutTrim = _FNWageCutTrim
                            'Modift by Num 20161004 Request By Pung Change to Calculate By Export

                        Next

                        FNMonthlyExportQty = Val(Rm!FNMonthExportQuantity.ToString)
                        FNMonthlyExportAmt = Val(Rm!FNMonthExportAmt.ToString)
                        _FNWageCost = Val(Rm!FNWageCost.ToString)
                        _FNProdCost = Val(Rm!FNProdCost.ToString)
                        _FNExportCost = Val(Rm!FNExportCost.ToString)
                        _FNTransportCost = Val(Rm!FNTransportCost.ToString)
                        _FNTransportAirCost = Val(Rm!FNTransportAirCost.ToString)

                        If TotalFNWagePull > _GFNWagePull Then
                            TotalFNWagePull = _GFNWagePull
                        End If

                        If TotalFNFabricCost > _GFNFabricCost Then
                            TotalFNFabricCost = _GFNFabricCost
                        End If

                        If TotalFNAccessroryCost > _GFNAccessroryCost Then
                            TotalFNAccessroryCost = _GFNAccessroryCost
                        End If

                        If TotalFNAccFabStockCost > _GFNAccFabStockCost Then
                            TotalFNAccFabStockCost = _GFNAccFabStockCost
                        End If

                        If TotalFNServiceCost > _GFNServiceCost Then
                            TotalFNServiceCost = _GFNServiceCost
                        End If

                        If TotalFNEmbSubCost > _GFNEmbSubCost Then
                            TotalFNEmbSubCost = _GFNEmbSubCost
                        End If

                        If TotalFNPrintSubCost > _GFNPrintSubCost Then
                            TotalFNPrintSubCost = _GFNPrintSubCost
                        End If

                        If TotalFNEmbBranchCost > _GFNEmbBranchCost Then
                            TotalFNEmbBranchCost = _GFNEmbBranchCost
                        End If

                        If TotalFNPrintBranchCost > _GFNPrintBranchCost Then
                            TotalFNPrintBranchCost = _GFNPrintBranchCost
                        End If

                        If TotalFNEmbFacCost > _GFNEmbFacCost Then
                            TotalFNEmbFacCost = _GFNEmbFacCost
                        End If

                        If TotalFNConductedCost > _GFNConductedCost Then
                            TotalFNConductedCost = _GFNConductedCost
                        End If

                        If TotalFNCommissionCost > _GFNCommissionCost Then
                            TotalFNCommissionCost = _GFNCommissionCost
                        End If

                        If TotalFNImportCost > _GFNImportCost Then
                            TotalFNImportCost = _GFNImportCost
                        End If

                        If TotalFNOtherCost > _GFNOtherCost Then
                            TotalFNOtherCost = _GFNOtherCost
                        End If

                        If TotalFNAccFabStockOtherCost > _GFNAccFabStockOtherCost Then
                            TotalFNAccFabStockOtherCost = _GFNAccFabStockOtherCost
                        End If

                        If TotalFNFabricRcvCost > _GFNFabricRcvCost Then
                            TotalFNFabricRcvCost = _GFNFabricRcvCost
                        End If

                        If TotalFNAccessroryRcvCost > _GFNAccessroryRcvCost Then
                            TotalFNAccessroryRcvCost = _GFNAccessroryRcvCost
                        End If

                        If TotalFNFabricDummyCost > _GFNFabricDummyCost Then
                            TotalFNFabricDummyCost = _GFNFabricDummyCost
                        End If

                        If TotalFNAccessroryDummyCost > _GFNAccessroryDummyCost Then
                            TotalFNAccessroryDummyCost = _GFNAccessroryDummyCost
                        End If

                        If TotalFNAccFabStockRcvCost > _GFNAccFabStockRcvCost Then
                            TotalFNAccFabStockRcvCost = _GFNAccFabStockRcvCost
                        End If

                        If TotalFNAccFabStockOtherRcvCost > _GFNAccFabStockOtherRcvCost Then
                            TotalFNAccFabStockOtherRcvCost = _GFNAccFabStockOtherRcvCost
                        End If

                        If TotalFNWageCutSew > _GFNWageCutSew Then
                            TotalFNWageCutSew = _GFNWageCutSew
                        End If

                        If TotalFNWageCutTrim > _GFNWageCutTrim Then
                            TotalFNWageCutTrim = _GFNWageCutTrim
                        End If

                        _GFNWagePull = _GFNWagePull - TotalFNWagePull
                        _GFNFabricCost = _GFNFabricCost - TotalFNFabricCost
                        _GFNAccessroryCost = _GFNAccessroryCost - TotalFNAccessroryCost
                        _GFNAccFabStockCost = _GFNAccFabStockCost - TotalFNAccFabStockCost
                        _GFNServiceCost = _GFNServiceCost - TotalFNServiceCost
                        _GFNEmbSubCost = _GFNEmbSubCost - TotalFNEmbSubCost
                        _GFNPrintSubCost = _GFNPrintSubCost - TotalFNPrintSubCost
                        _GFNEmbBranchCost = _GFNEmbBranchCost - TotalFNEmbBranchCost
                        _GFNPrintBranchCost = _GFNPrintBranchCost - TotalFNPrintBranchCost
                        _GFNEmbFacCost = _GFNEmbFacCost - TotalFNEmbFacCost
                        _GFNConductedCost = _GFNConductedCost - TotalFNConductedCost
                        _GFNCommissionCost = _GFNCommissionCost - TotalFNCommissionCost
                        _GFNImportCost = _GFNImportCost - TotalFNImportCost
                        _GFNAccFabStockOtherCost = _GFNAccFabStockOtherCost - TotalFNAccFabStockOtherCost
                        _GFNFabricRcvCost = _GFNFabricRcvCost - TotalFNFabricRcvCost
                        _GFNAccessroryRcvCost = _GFNAccessroryRcvCost - TotalFNAccessroryRcvCost
                        _GFNFabricDummyCost = _GFNFabricDummyCost - TotalFNFabricDummyCost
                        _GFNAccessroryDummyCost = _GFNAccessroryDummyCost - TotalFNAccessroryDummyCost
                        _GFNAccFabStockRcvCost = _GFNAccFabStockRcvCost - TotalFNAccFabStockRcvCost
                        _GFNAccFabStockOtherRcvCost = _GFNAccFabStockOtherRcvCost - TotalFNAccFabStockOtherRcvCost
                        _GFNWageCutSew = _GFNWageCutSew - TotalFNWageCutSew
                        _GFNWageCutTrim = _GFNWageCutTrim - TotalFNWageCutTrim


                        If FNMonthSeq = FNLastMonthSeq And FTStateFinish.Checked = True Then

                            TotalFNWagePull = TotalFNWagePull + _GFNWagePull
                            TotalFNFabricCost = TotalFNFabricCost + _GFNFabricCost
                            TotalFNAccessroryCost = TotalFNAccessroryCost + _GFNAccessroryCost
                            TotalFNAccFabStockCost = TotalFNAccFabStockCost + _GFNAccFabStockCost
                            TotalFNServiceCost = TotalFNServiceCost + _GFNServiceCost
                            TotalFNEmbSubCost = TotalFNEmbSubCost + _GFNEmbSubCost
                            TotalFNPrintSubCost = TotalFNPrintSubCost + _GFNPrintSubCost
                            TotalFNEmbBranchCost = TotalFNEmbBranchCost + _GFNEmbBranchCost
                            TotalFNPrintBranchCost = TotalFNPrintBranchCost + _GFNPrintBranchCost
                            TotalFNEmbFacCost = TotalFNEmbFacCost + _GFNEmbFacCost
                            TotalFNConductedCost = TotalFNConductedCost + _GFNConductedCost
                            TotalFNCommissionCost = TotalFNCommissionCost + _GFNCommissionCost
                            TotalFNImportCost = TotalFNImportCost + _GFNImportCost
                            'TotalFNOtherCost = TotalFNOtherCost + _GFNOtherCost
                            TotalFNAccFabStockOtherCost = TotalFNAccFabStockOtherCost + _GFNAccFabStockOtherCost
                            TotalFNFabricRcvCost = TotalFNFabricRcvCost + _GFNFabricRcvCost
                            TotalFNAccessroryRcvCost = TotalFNAccessroryRcvCost + _GFNAccessroryRcvCost
                            TotalFNFabricDummyCost = TotalFNFabricDummyCost + _GFNFabricDummyCost
                            TotalFNAccessroryDummyCost = TotalFNAccessroryDummyCost + _GFNAccessroryDummyCost
                            TotalFNAccFabStockRcvCost = TotalFNAccFabStockRcvCost + _GFNAccFabStockRcvCost
                            TotalFNAccFabStockOtherRcvCost = TotalFNAccFabStockOtherRcvCost + _GFNAccFabStockOtherRcvCost
                            TotalFNWageCutSew = TotalFNWageCutSew + _GFNWageCutSew
                            TotalFNWageCutTrim = TotalFNWageCutTrim + _GFNWageCutTrim

                            'TotalFNWagePull = _GFNWagePull
                            'TotalFNFabricCost = _GFNFabricCost
                            'TotalFNAccessroryCost = _GFNAccessroryCost
                            'TotalFNAccFabStockCost = _GFNAccFabStockCost
                            'TotalFNServiceCost = _GFNServiceCost
                            'TotalFNEmbSubCost = _GFNEmbSubCost
                            'TotalFNPrintSubCost = _GFNPrintSubCost
                            'TotalFNEmbBranchCost = _GFNEmbBranchCost
                            'TotalFNPrintBranchCost = _GFNPrintBranchCost
                            'TotalFNEmbFacCost = _GFNEmbFacCost
                            'TotalFNConductedCost = _GFNConductedCost
                            'TotalFNCommissionCost = _GFNCommissionCost
                            'TotalFNImportCost = _GFNImportCost
                            TotalFNOtherCost = _GFNOtherCost
                            'TotalFNAccFabStockOtherCost = _GFNAccFabStockOtherCost
                            'TotalFNFabricRcvCost = _GFNFabricRcvCost
                            'TotalFNAccessroryRcvCost = _GFNAccessroryRcvCost
                            'TotalFNFabricDummyCost = _GFNFabricDummyCost
                            'TotalFNAccessroryDummyCost = _GFNAccessroryDummyCost
                            'TotalFNAccFabStockRcvCost = _GFNAccFabStockRcvCost
                            'TotalFNAccFabStockOtherRcvCost = _GFNAccFabStockOtherRcvCost
                            'TotalFNWageCutSew = _GFNWageCutSew
                            'TotalFNWageCutTrim = _GFNWageCutTrim

                        End If


                        Dim _FNCalNetCost As Double = 0
                        Dim _FNCalExportNetAmt As Double = 0
                        Dim _FNCalNetProfit As Double = 0
                        Dim _FNCalProfitPerPiece As Double = 0
                        Dim _FNCalExportNetCostAmt As Double = 0
                        Dim _FNCalNetProfitRcv As Double = 0
                        Dim _FNCalProfitPerPieceRcv As Double = 0

                        _FNCalNetCost = (TotalFNEmbSubCost + TotalFNPrintSubCost + TotalFNEmbBranchCost + TotalFNPrintBranchCost + TotalFNEmbFacCost + _FNWageCost + _FNProdCost + TotalFNConductedCost + TotalFNCommissionCost + TotalFNImportCost + _FNExportCost + _FNTransportCost + _FNTransportAirCost + TotalFNOtherCost + TotalFNWageCutSew + TotalFNWageCutTrim)
                        _FNCalExportNetAmt = FNMonthlyExportAmt - (TotalFNFabricCost + TotalFNAccessroryCost + TotalFNAccFabStockCost + TotalFNServiceCost + TotalFNAccFabStockOtherCost + TotalFNWagePull)
                        _FNCalExportNetCostAmt = FNMonthlyExportAmt - ((TotalFNFabricRcvCost + TotalFNFabricDummyCost) + (TotalFNAccessroryRcvCost + TotalFNAccessroryDummyCost) + TotalFNAccFabStockRcvCost + TotalFNServiceCost + TotalFNAccFabStockOtherRcvCost + TotalFNWagePull)

                        _FNCalNetProfit = _FNCalExportNetAmt - _FNCalNetCost
                        _FNCalNetProfitRcv = _FNCalExportNetCostAmt - _FNCalNetCost

                        _FNCalProfitPerPiece = (_FNCalNetProfit / FNMonthlyExportQty)
                        _FNCalProfitPerPieceRcv = (_FNCalNetProfitRcv / FNMonthlyExportQty)

                        _Str = "UPDATE A SET "
                        _Str &= vbCrLf & " FNCalExportAmt=" & FNMonthlyExportAmt & ""
                        _Str &= vbCrLf & ", FNCalFabricCost=" & TotalFNFabricCost & ""
                        _Str &= vbCrLf & ", FNCalAccessroryCost=" & TotalFNAccessroryCost & ""
                        _Str &= vbCrLf & ", FNCalAccFabStockCost=" & TotalFNAccFabStockCost & ""
                        _Str &= vbCrLf & ", FNCalServiceCost=" & TotalFNServiceCost & ""
                        _Str &= vbCrLf & ", FNCalExportNetAmt=" & _FNCalExportNetAmt & ""
                        _Str &= vbCrLf & ", FNCalEmbSubCost=" & TotalFNEmbSubCost & ""
                        _Str &= vbCrLf & ", FNCalPrintSubCost=" & TotalFNPrintSubCost & ""
                        _Str &= vbCrLf & ", FNCalEmbBranchCost=" & TotalFNEmbBranchCost & ""
                        _Str &= vbCrLf & ", FNCalPrintBranchCost=" & TotalFNPrintBranchCost & ""
                        _Str &= vbCrLf & ", FNCalEmbFacCost=" & TotalFNEmbFacCost & ""
                        _Str &= vbCrLf & ", FNCalWageCost=" & _FNWageCost & ""
                        _Str &= vbCrLf & ", FNCalProdCost=" & _FNProdCost & ""
                        _Str &= vbCrLf & ", FNCalConductedCost=" & TotalFNConductedCost & ""
                        _Str &= vbCrLf & ", FNCalCommissionCost=" & TotalFNCommissionCost & ""
                        _Str &= vbCrLf & ", FNCalImportCost=" & TotalFNImportCost & ""
                        _Str &= vbCrLf & ", FNCalExportCost=" & _FNExportCost & ""
                        _Str &= vbCrLf & ", FNCalTransportCost=" & _FNTransportCost & ""
                        _Str &= vbCrLf & ", FNCalTransportAirCost=" & _FNTransportAirCost & ""
                        _Str &= vbCrLf & ", FNCalOtherCost=" & TotalFNOtherCost & ""
                        _Str &= vbCrLf & ", FNCalNetCost=" & _FNCalNetCost & ""
                        _Str &= vbCrLf & ", FNCalNetProfit=" & _FNCalNetProfit & ""
                        _Str &= vbCrLf & ", FNCalProfitPerPiece=" & _FNCalProfitPerPiece & ""
                        _Str &= vbCrLf & ", FNCalExportQty=" & FNMonthlyExportQty & ""
                        _Str &= vbCrLf & ", FNCalAccFabStockOtherCost=" & TotalFNAccFabStockOtherCost & ""
                        _Str &= vbCrLf & ", FNCalFabricRcvCost=" & TotalFNFabricRcvCost & ""
                        _Str &= vbCrLf & ", FNCalAccessroryRcvCost=" & TotalFNAccessroryRcvCost & ""
                        _Str &= vbCrLf & ", FNCalFabricDummyCost=" & TotalFNFabricDummyCost & ""
                        _Str &= vbCrLf & ", FNCalAccessroryDummyCost=" & TotalFNAccessroryDummyCost & ""
                        _Str &= vbCrLf & ", FNCalFabricBalCost=" & ((TotalFNFabricRcvCost + TotalFNFabricDummyCost) - (TotalFNFabricCost)) & ""
                        _Str &= vbCrLf & ", FNCalAccessroryBalCost=" & ((TotalFNAccessroryRcvCost + TotalFNAccessroryDummyCost) - (TotalFNAccessroryCost)) & ""
                        _Str &= vbCrLf & ", FNCalAccFabStockRcvCost=" & TotalFNAccFabStockRcvCost & ""
                        _Str &= vbCrLf & ", FNCalAccFabStockOtherRcvCost=" & TotalFNAccFabStockOtherRcvCost & ""
                        _Str &= vbCrLf & ", FNCalAccFabStockBalCost=" & (TotalFNAccFabStockRcvCost - TotalFNAccFabStockCost) & ""
                        _Str &= vbCrLf & ", FNCalAccFabStockOtherBalCost=" & (TotalFNAccFabStockOtherRcvCost - TotalFNAccFabStockOtherCost) & ""
                        _Str &= vbCrLf & ", FNCalExportNetCostAmt=" & _FNCalExportNetCostAmt & ""
                        _Str &= vbCrLf & ", FNCalNetProfitRcv=" & _FNCalNetProfitRcv & ""
                        _Str &= vbCrLf & ", FNCalProfitPerPieceRcv=" & _FNCalProfitPerPieceRcv & ""
                        _Str &= vbCrLf & ", FNCalWageCutSew=" & TotalFNWageCutSew & ""
                        _Str &= vbCrLf & ", FNCalWageCutTrim=" & TotalFNWageCutTrim & ""
                        _Str &= vbCrLf & ", FNWagePullAct=" & TotalFNWagePull & ""

                        If FNMonthSeq = FNLastMonthSeq And FTStateFinish.Checked = True Then

                            _Str &= vbCrLf & ", FNDebitAmt=" & FNDebitAmt.Value & ""

                        Else

                            _Str &= vbCrLf & ", FNDebitAmt=0"

                        End If

                        _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly AS A"
                        _Str &= vbCrLf & " WHERE  (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderKey) & "')"
                        _Str &= vbCrLf & "        AND (FTInvoiceMonth = '" & HI.UL.ULF.rpQuoted(FTInvoiceMonth) & "')"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

                    Next

                End If
            End If

            _Str = "Delete FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly"
            _Str &= vbCrLf & "  WHERE FTOrderNoMain='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

            If _StateSaveJobType = 1 Then
                Dim _FTOrderNoRef As DataTable

                _Str = "   SELECT  FTOrderNoRef"
                _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS X WITH(NOLOCK)"
                _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "' "
                _Str &= vbCrLf & "   AND ISNULL(FTOrderNoRef,'') <>''"
                _Str &= vbCrLf & "  GROUP BY FTOrderNoRef ORDER BY FTOrderNoRef"

                _FTOrderNoRef = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

                For Each Rx As DataRow In _FTOrderNoRef.Rows
                    Call CalculateCostDataOderRef(OrderKey, Rx!FTOrderNoRef)
                Next

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub CalculateCostDataOderRef(MainOrderKey As String, OrderKey As String)
        Dim _Str As String
        Try

            Dim _dt As DataTable
            Dim Rind As Integer = 0
            Dim FNMonthSeq As Integer = 0
            Dim FNLastMonthSeq As Integer = 0
            Dim FTInvoiceMonth As String = ""


            _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly ("
            _Str &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime"
            _Str &= vbCrLf & ", FTOrderNo, FTInvoiceMonth, FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, FNServiceCost, FNExportNetAmt, FNEmbSubCost, FNPrintSubCost, "
            _Str &= vbCrLf & " FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost, FNTransportAirCost, FNOtherCost,"
            _Str &= vbCrLf & " FNNetCost, FNNetProfit, FNProfitPerPiece, FTRemark, FNExportQty, FNConductedCostPer, FNCommissionCostPer, FNAccFabStockOtherCost, FNFabricRcvCost, FNAccessroryRcvCost, FNFabricDummyCost,"
            _Str &= vbCrLf & "  FNAccessroryDummyCost, FNFabricBalCost, FNAccessroryBalCost, FNAccFabStockRcvCost, FNAccFabStockOtherRcvCost, FNAccFabStockBalCost, FNAccFabStockOtherBalCost, FNExportNetCostAmt,"
            _Str &= vbCrLf & "   FNNetProfitRcv, FNProfitPerPieceRcv, FNWageCutSew, FNWageCutTrim, FNCalExportAmt, FNCalFabricCost, FNCalAccessroryCost, FNCalAccFabStockCost, FNCalServiceCost, FNCalExportNetAmt,"
            _Str &= vbCrLf & "   FNCalEmbSubCost, FNCalPrintSubCost, FNCalEmbBranchCost, FNCalPrintBranchCost, FNCalEmbFacCost, FNCalWageCost, FNCalProdCost, FNCalConductedCost, FNCalCommissionCost, FNCalImportCost,"
            _Str &= vbCrLf & "  FNCalExportCost, FNCalTransportCost, FNCalTransportAirCost, FNCalOtherCost, FNCalNetCost, FNCalNetProfit, FNCalProfitPerPiece, FNCalExportQty, FNCalAccFabStockOtherCost, FNCalFabricRcvCost,"
            _Str &= vbCrLf & "   FNCalAccessroryRcvCost, FNCalFabricDummyCost, FNCalAccessroryDummyCost, FNCalFabricBalCost, FNCalAccessroryBalCost, FNCalAccFabStockRcvCost, FNCalAccFabStockOtherRcvCost,"
            _Str &= vbCrLf & "  FNCalAccFabStockBalCost, FNCalAccFabStockOtherBalCost, FNCalExportNetCostAmt, FNCalNetProfitRcv, FNCalProfitPerPieceRcv, FNCalWageCutSew, FNCalWageCutTrim, FNDebitAmt, FTOrderNoMain"


            _Str &= vbCrLf & " )"

            _Str &= vbCrLf & " SELECT  "
            _Str &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
            _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            _Str &= vbCrLf & ", FTOrderNo, FTInvoiceMonth, FNCalExportAmt AS FNExportAmt"
            _Str &= vbCrLf & ", FNCalFabricCost AS FNFabricCost"
            _Str &= vbCrLf & ", FNCalAccessroryCost AS FNAccessroryCost"
            _Str &= vbCrLf & ", FNCalAccFabStockCost AS FNAccFabStockCost"
            _Str &= vbCrLf & ", FNCalServiceCost AS FNServiceCost"
            _Str &= vbCrLf & ", FNCalExportNetAmt As FNExportNetAmt"
            _Str &= vbCrLf & ", FNCalEmbSubCost AS FNEmbSubCost"
            _Str &= vbCrLf & ", FNCalPrintSubCost AS FNPrintSubCost"
            _Str &= vbCrLf & ", FNCalEmbBranchCost AS FNEmbBranchCost"
            _Str &= vbCrLf & ", FNCalPrintBranchCost AS FNPrintBranchCost"
            _Str &= vbCrLf & ", FNCalEmbFacCost AS FNEmbFacCost"
            _Str &= vbCrLf & ", FNCalWageCost AS FNWageCost"
            _Str &= vbCrLf & ", FNCalProdCost AS FNProdCost"
            _Str &= vbCrLf & ", FNCalConductedCost AS FNConductedCost"
            _Str &= vbCrLf & ", FNCalCommissionCost AS FNCommissionCost"
            _Str &= vbCrLf & ", FNCalImportCost AS FNImportCost"
            _Str &= vbCrLf & ", FNCalExportCost AS FNExportCost"
            _Str &= vbCrLf & ", FNCalTransportCost AS FNTransportCost"
            _Str &= vbCrLf & ", FNCalTransportAirCost As FNTransportAirCost"
            _Str &= vbCrLf & ", FNCalOtherCost AS FNOtherCost"
            _Str &= vbCrLf & ", FNCalNetCost AS FNNetCost"
            _Str &= vbCrLf & ", FNCalNetProfit AS FNNetProfit"
            _Str &= vbCrLf & ", FNCalProfitPerPiece AS FNProfitPerPiece"
            _Str &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTRemark.Text.Trim()) & "' AS FTRemark"
            _Str &= vbCrLf & ", FNCalExportQty AS FNExportQty"
            _Str &= vbCrLf & ", " & FNConductedCostPer.Value & " AS FNConductedCostPer"
            _Str &= vbCrLf & ", " & FNCommissionCostPer.Value & " AS FNCommissionCostPer"
            _Str &= vbCrLf & ", FNCalAccFabStockOtherCost AS FNAccFabStockOtherCost"
            _Str &= vbCrLf & ", FNCalFabricRcvCost AS FNFabricRcvCost"
            _Str &= vbCrLf & ", FNCalAccessroryRcvCost AS FNAccessroryRcvCost"
            _Str &= vbCrLf & ", FNCalFabricDummyCost AS FNFabricDummyCost"
            _Str &= vbCrLf & ", FNCalAccessroryDummyCost AS FNAccessroryDummyCost"
            _Str &= vbCrLf & ", FNCalFabricBalCost AS FNFabricBalCost"
            _Str &= vbCrLf & ", FNCalAccessroryBalCost AS FNAccessroryBalCost"
            _Str &= vbCrLf & ", FNCalAccFabStockRcvCost AS FNAccFabStockRcvCost"
            _Str &= vbCrLf & ", FNCalAccFabStockOtherRcvCost AS FNAccFabStockOtherRcvCost"
            _Str &= vbCrLf & ", FNCalAccFabStockBalCost AS FNAccFabStockBalCost"
            _Str &= vbCrLf & ", FNCalAccFabStockOtherBalCost AS FNAccFabStockOtherBalCost"
            _Str &= vbCrLf & ", FNCalExportNetCostAmt AS FNExportNetCostAmt"
            _Str &= vbCrLf & ", FNCalNetProfitRcv AS FNNetProfitRcv"
            _Str &= vbCrLf & ", FNCalProfitPerPieceRcv AS FNProfitPerPieceRcv"
            _Str &= vbCrLf & ", FNCalWageCutSew AS FNWageCutSew"
            _Str &= vbCrLf & ", FNCalWageCutTrim AS FNWageCutTrim"

            _Str &= vbCrLf & ", FNCalExportAmt"
            _Str &= vbCrLf & ", FNCalFabricCost, FNCalAccessroryCost, FNCalAccFabStockCost"
            _Str &= vbCrLf & ", FNCalServiceCost, FNCalExportNetAmt, FNCalEmbSubCost"
            _Str &= vbCrLf & ", FNCalPrintSubCost, FNCalEmbBranchCost, FNCalPrintBranchCost"
            _Str &= vbCrLf & ", FNCalEmbFacCost, FNCalWageCost, FNCalProdCost, FNCalConductedCost"
            _Str &= vbCrLf & ", FNCalCommissionCost, FNCalImportCost, FNCalExportCost"
            _Str &= vbCrLf & ", FNCalTransportCost, FNCalTransportAirCost, FNCalOtherCost"
            _Str &= vbCrLf & ", FNCalNetCost, FNCalNetProfit, FNCalProfitPerPiece, FNCalExportQty"
            _Str &= vbCrLf & ", FNCalAccFabStockOtherCost, FNCalFabricRcvCost, FNCalAccessroryRcvCost"
            _Str &= vbCrLf & ", FNCalFabricDummyCost, FNCalAccessroryDummyCost, FNCalFabricBalCost"
            _Str &= vbCrLf & ", FNCalAccessroryBalCost, FNCalAccFabStockRcvCost, FNCalAccFabStockOtherRcvCost"
            _Str &= vbCrLf & ",FNCalAccFabStockBalCost, FNCalAccFabStockOtherBalCost, FNCalExportNetCostAmt"
            _Str &= vbCrLf & ", FNCalNetProfitRcv, FNCalProfitPerPieceRcv, FNCalWageCutSew, FNCalWageCutTrim, FNDebitAmt"
            _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(MainOrderKey) & "'"

            _Str &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_JobCostOrderBooking_Ref"
            _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"

            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)


        Catch ex As Exception
        End Try

    End Sub

    Private Function SaveData() As Boolean

        Dim dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Dim dtinv As DataTable
        With CType(Me.ogcinvoicecharge.DataSource, DataTable)
            .AcceptChanges()
            dtinv = .Copy
        End With

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
            _Str &= vbCrLf & " , FNExportGAmt=" & FNExportGAmt.Value & ""
            _Str &= vbCrLf & " , FNDebitAmt=" & FNDebitAmt.Value & ""
            _Str &= vbCrLf & " , FNExportAmt=" & FNExportAmt.Value & ""
            _Str &= vbCrLf & " , FNFabricCost=" & FNFabricCost.Value & ""
            _Str &= vbCrLf & " , FNAccessroryCost=" & FNAccessroryCost.Value & ""
            _Str &= vbCrLf & " , FNAccFabStockCost=" & FNAccFabStockCost.Value & ""
            _Str &= vbCrLf & " , FNExportNetAmt=" & FNExportNetAmt.Value & ""
            _Str &= vbCrLf & " , FNEmbSubCost=" & FNEmbSubCost.Value & ""
            _Str &= vbCrLf & " , FNPrintSubCost=" & FNPrintSubCost.Value & ""
            _Str &= vbCrLf & " , FNEmbBranchCost=" & FNEmbBranchCost.Value & ""
            _Str &= vbCrLf & " , FNPrintBranchCost=" & FNPrintBranchCost.Value & ""
            _Str &= vbCrLf & " , FNEmbFacCost=" & FNEmbFacCost.Value & ""
            _Str &= vbCrLf & " , FNWageCost=" & FNWageCost.Value & ""
            _Str &= vbCrLf & " , FNProdCost=" & FNProdCost.Value & ""
            _Str &= vbCrLf & " , FNConductedCost=" & FNConductedCost.Value & ""
            _Str &= vbCrLf & " , FNCommissionCost=" & FNCommissionCost.Value & ""
            _Str &= vbCrLf & " , FNImportCost=" & FNImportCost.Value & ""
            _Str &= vbCrLf & " , FNExportCost=" & FNExportCost.Value & ""
            _Str &= vbCrLf & " , FNTransportCost=" & FNTransportCost.Value & ""
            _Str &= vbCrLf & " , FNTransportAirCost=" & FNTransportAirCost.Value & ""
            _Str &= vbCrLf & " , FNOtherCost=" & FNOtherCost.Value & ""
            _Str &= vbCrLf & " , FNNetCost=" & FNNetCost.Value & ""
            _Str &= vbCrLf & " , FNNetProfit=" & FNNetProfit.Value & ""
            _Str &= vbCrLf & " , FNProfitPerPiece=" & FNProfitPerPiece.Value & ""
            _Str &= vbCrLf & " , FTRemark='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"
            _Str &= vbCrLf & " , FNExportQty=" & FNExportQty.Value & ""
            _Str &= vbCrLf & " , FNServiceCost=" & FNServiceCost.Value & ""
            _Str &= vbCrLf & " , FNConductedCostPer=" & FNConductedCostPer.Value & ""
            _Str &= vbCrLf & " , FNCommissionCostPer=" & FNCommissionCostPer.Value & ""
            _Str &= vbCrLf & " , FNAccFabStockOtherCost=" & FNAccFabStockOtherCost.Value & ""
            _Str &= vbCrLf & " , FNFabricRcvCost=" & FNFabricRcvCost.Value & ""
            _Str &= vbCrLf & " , FNAccessroryRcvCost=" & FNAccessroryRcvCost.Value & ""
            _Str &= vbCrLf & " , FNFabricDummyCost=" & FNFabricDummyCost.Value & ""
            _Str &= vbCrLf & " , FNAccessroryDummyCost=" & FNAccessroryDummyCost.Value & ""
            _Str &= vbCrLf & " , FNFabricBalCost=" & FNFabricBalCost.Value & ""
            _Str &= vbCrLf & " , FNAccessroryBalCost=" & FNAccessroryBalCost.Value & ""
            _Str &= vbCrLf & " , FNAccFabStockRcvCost=" & FNAccFabStockRcvCost.Value & ""
            _Str &= vbCrLf & " , FNAccFabStockOtherRcvCost=" & FNAccFabStockOtherRcvCost.Value & ""
            _Str &= vbCrLf & " , FNAccFabStockBalCost=" & FNAccFabStockBalCost.Value & ""
            _Str &= vbCrLf & " , FNAccFabStockOtherBalCost=" & FNAccFabStockOtherBalCost.Value & ""
            _Str &= vbCrLf & " , FNExportNetCostAmt=" & FNExportNetCostAmt.Value & ""
            _Str &= vbCrLf & " , FNNetProfitRcv=" & FNNetProfitRcv.Value & ""
            _Str &= vbCrLf & " , FNProfitPerPieceRcv=" & FNExportNetCostAmt.Value & ""
            _Str &= vbCrLf & " , FNWageCutSew=" & FNWageCutSew.Value & ""
            _Str &= vbCrLf & " , FNWageCutTrim=" & FNWageCutTrim.Value & ""
            _Str &= vbCrLf & " , FNWagePull=" & FNWagePull.Value & ""

            If Me.FTStateFinish.Checked Then
                _Str &= vbCrLf & " , FTStateFinish='1'"
                _Str &= vbCrLf & " , FTStateFinishBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDStateFinishDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTStateFinishTime=" & HI.UL.ULDate.FormatTimeDB & ""
            Else
                _Str &= vbCrLf & " , FTStateFinish='0'"
            End If

            _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost AS A "
            _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost  ( "
                _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FTOrderNo,FNExportGAmt,FNDebitAmt, FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost, FNExportNetAmt, FNEmbSubCost, FNPrintSubCost, "
                _Str &= vbCrLf & "  FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost, FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost, FNTransportAirCost, FNOtherCost,"
                _Str &= vbCrLf & "   FNNetCost, FNNetProfit, FNProfitPerPiece, FTRemark,FNExportQty,FNServiceCost,FNConductedCostPer,FNCommissionCostPer"
                _Str &= vbCrLf & " , FNAccFabStockOtherCost"
                _Str &= vbCrLf & " , FNFabricRcvCost"
                _Str &= vbCrLf & " , FNAccessroryRcvCost"
                _Str &= vbCrLf & " , FNFabricDummyCost"
                _Str &= vbCrLf & " , FNAccessroryDummyCost"
                _Str &= vbCrLf & " , FNFabricBalCost"
                _Str &= vbCrLf & " , FNAccessroryBalCost"
                _Str &= vbCrLf & " , FNAccFabStockRcvCost"
                _Str &= vbCrLf & " , FNAccFabStockOtherRcvCost"
                _Str &= vbCrLf & " , FNAccFabStockBalCost"
                _Str &= vbCrLf & " , FNAccFabStockOtherBalCost,FNExportNetCostAmt,FNNetProfitRcv,FNProfitPerPieceRcv,FNWageCutSew,FNWageCutTrim"

                If Me.FTStateFinish.Checked Then
                    _Str &= vbCrLf & " , FTStateFinish"
                    _Str &= vbCrLf & " , FTStateFinishBy"
                    _Str &= vbCrLf & " , FDStateFinishDate"
                    _Str &= vbCrLf & " , FTStateFinishTime"
                Else
                    _Str &= vbCrLf & " , FTStateFinish"
                End If
                _Str &= vbCrLf & " , FNWagePull"
                _Str &= vbCrLf & "  )"
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                _Str &= vbCrLf & " ," & FNExportGAmt.Value & ""
                _Str &= vbCrLf & " ," & FNDebitAmt.Value & ""
                _Str &= vbCrLf & " ," & FNExportAmt.Value & ""
                _Str &= vbCrLf & " ," & FNFabricCost.Value & ""
                _Str &= vbCrLf & " ," & FNAccessroryCost.Value & ""
                _Str &= vbCrLf & " ," & FNAccFabStockCost.Value & ""
                _Str &= vbCrLf & " ," & FNExportNetAmt.Value & ""
                _Str &= vbCrLf & " ," & FNEmbSubCost.Value & ""
                _Str &= vbCrLf & " ," & FNPrintSubCost.Value & ""
                _Str &= vbCrLf & " ," & FNEmbBranchCost.Value & ""
                _Str &= vbCrLf & " ," & FNPrintBranchCost.Value & ""
                _Str &= vbCrLf & " ," & FNEmbFacCost.Value & ""
                _Str &= vbCrLf & " ," & FNWageCost.Value & ""
                _Str &= vbCrLf & " ," & FNProdCost.Value & ""
                _Str &= vbCrLf & " ," & FNConductedCost.Value & ""
                _Str &= vbCrLf & " ," & FNCommissionCost.Value & ""
                _Str &= vbCrLf & " ," & FNImportCost.Value & ""
                _Str &= vbCrLf & " ," & FNExportCost.Value & ""
                _Str &= vbCrLf & " ," & FNTransportCost.Value & ""
                _Str &= vbCrLf & " ," & FNTransportAirCost.Value & ""
                _Str &= vbCrLf & " ," & FNOtherCost.Value & ""
                _Str &= vbCrLf & " ," & FNNetCost.Value & ""
                _Str &= vbCrLf & " ," & FNNetProfit.Value & ""
                _Str &= vbCrLf & " ," & FNProfitPerPiece.Value & ""
                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"
                _Str &= vbCrLf & " ," & FNExportQty.Value & ""
                _Str &= vbCrLf & " ," & FNServiceCost.Value & ""
                _Str &= vbCrLf & " ," & FNConductedCostPer.Value & ""
                _Str &= vbCrLf & " ," & FNCommissionCostPer.Value & ""
                _Str &= vbCrLf & " , " & FNAccFabStockOtherCost.Value & ""
                _Str &= vbCrLf & " , " & FNFabricRcvCost.Value & ""
                _Str &= vbCrLf & " ," & FNAccessroryRcvCost.Value & ""
                _Str &= vbCrLf & " , " & FNFabricDummyCost.Value & ""
                _Str &= vbCrLf & " , " & FNAccessroryDummyCost.Value & ""
                _Str &= vbCrLf & " ," & FNFabricBalCost.Value & ""
                _Str &= vbCrLf & " , " & FNAccessroryBalCost.Value & ""
                _Str &= vbCrLf & " ," & FNAccFabStockRcvCost.Value & ""
                _Str &= vbCrLf & " , " & FNAccFabStockOtherRcvCost.Value & ""
                _Str &= vbCrLf & " ," & FNAccFabStockBalCost.Value & ""
                _Str &= vbCrLf & " , " & FNAccFabStockOtherBalCost.Value & ""
                _Str &= vbCrLf & " , " & FNExportNetCostAmt.Value & ""
                _Str &= vbCrLf & " , " & FNNetProfitRcv.Value & ""
                _Str &= vbCrLf & " , " & FNProfitPerPieceRcv.Value & ""
                _Str &= vbCrLf & " , " & FNWageCutSew.Value & ""
                _Str &= vbCrLf & " , " & FNWageCutTrim.Value & ""

                If Me.FTStateFinish.Checked Then
                    _Str &= vbCrLf & " ,'1'"
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                Else
                    _Str &= vbCrLf & " ,'0'"
                End If
                _Str &= vbCrLf & " , " & FNWagePull.Value & ""
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In dt.Select("FTInvoiceNo<>'' AND FDInvoiceDate<>'' ")

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FDInvoiceDate='" & HI.UL.ULDate.ConvertEnDB(R!FDInvoiceDate.ToString) & "'"
                _Str &= vbCrLf & " , FNExportQuantity=" & Val(R!FNExportQuantity.ToString) & ""
                _Str &= vbCrLf & " , FNStockQuantity=" & Val(R!FNStockQuantity.ToString) & ""
                _Str &= vbCrLf & " , FNPrice=" & Val(R!FNPrice.ToString) & ""
                _Str &= vbCrLf & " , FNExchangeRate=" & Val(R!FNExchangeRate.ToString) & ""
                _Str &= vbCrLf & " , FNExportAmt=" & Val(R!FNExportAmt.ToString) & ""
                _Str &= vbCrLf & " , FNExportAmtTHB=" & Val(R!FNExportAmtTHB.ToString) & ""
                _Str &= vbCrLf & " , FTOrderNoRef='" & HI.UL.ULF.rpQuoted(R!FTOrderNoRef.ToString) & "'"
                _Str &= vbCrLf & " , FTStateCalCost='" & R!FTStateCalCost.ToString & "'"
                _Str &= vbCrLf & "   WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                _Str &= vbCrLf & "       AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                _Str &= vbCrLf & "       AND FNPrice=" & Val(R!FNPrice.ToString) & ""

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTOrderNo"
                    _Str &= vbCrLf & ", FTInvoiceNo, FDInvoiceDate, FNExportQuantity"
                    _Str &= vbCrLf & ", FNStockQuantity, FNPrice, FNExchangeRate, FNExportAmt, FNExportAmtTHB,FTOrderNoRef , FTStateCalCost"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                    _Str &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FDInvoiceDate.ToString) & "'"
                    _Str &= vbCrLf & " ," & Val(R!FNExportQuantity.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNStockQuantity.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNPrice.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNExchangeRate.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNExportAmt.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNExportAmtTHB.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTOrderNoRef.ToString) & "'"
                    _Str &= vbCrLf & " , '" & R!FTStateCalCost.ToString & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice_Cost WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In dtinv.Select("FTInvoiceNo<>'' ")

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice_Cost"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FNWageCost=" & Val(R!FNWageCost.ToString) & ""
                _Str &= vbCrLf & " , FNProdCost=" & Val(R!FNProdCost.ToString) & ""
                _Str &= vbCrLf & " , FNExportCost=" & Val(R!FNExportCost.ToString) & ""
                _Str &= vbCrLf & " , FNTransportCost=" & Val(R!FNTransportCost.ToString) & ""
                _Str &= vbCrLf & " , FNTransportAirCost=" & Val(R!FNTransportAirCost.ToString) & ""
                _Str &= vbCrLf & "   WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                _Str &= vbCrLf & "       AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice_Cost"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTOrderNo"
                    _Str &= vbCrLf & ", FTInvoiceNo,FNWageCost, FNProdCost, FNExportCost, FNTransportCost, FNTransportAirCost"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"

                    _Str &= vbCrLf & " ," & Val(R!FNWageCost.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNProdCost.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNExportCost.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNTransportCost.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNTransportAirCost.ToString) & ""


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly"
            _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Str &= vbCrLf & " AND  FTInvoiceMonth NOT IN ("
            _Str &= vbCrLf & " Select DISTINCT LEFT(FDInvoiceDate,7) AS FTInvoiceMonth"
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice"
            _Str &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Str &= vbCrLf & "  )"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


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

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Monthly WHERE FTOrderNoMain='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'")

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Public Sub LoadOrderCostDataInfo(ByVal Key As Object)
        _StateLoad = True
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        FNExportGAmt.Value = 0
        FNExportAmt.Value = 0
        FNFabricCost.Value = 0
        FNAccessroryCost.Value = 0
        FNAccFabStockCost.Value = 0
        FNExportNetAmt.Value = 0
        FNEmbSubCost.Value = 0
        FNPrintSubCost.Value = 0
        FNEmbBranchCost.Value = 0
        FNPrintBranchCost.Value = 0
        FNEmbFacCost.Value = 0
        FNWageCost.Value = 0
        FNProdCost.Value = 0

        FNWageCutSew.Value = 0
        FNWageCutTrim.Value = 0

        FNConductedCostPer.Value = 0
        FNConductedCost.Value = 0

        FNConductedCostPer.Value = 0
        FNCommissionCostPer.Value = 0

        FNImportCost.Value = 0
        FNExportCost.Value = 0
        FNTransportCost.Value = 0
        FNTransportAirCost.Value = 0
        FNOtherCost.Value = 0
        FNServiceCost.Value = 0
        FNNetCost.Value = 0
        FNNetProfit.Value = 0
        FNProfitPerPiece.Value = 0
        FNNetProfitRcv.Value = 0
        FNProfitPerPieceRcv.Value = 0
        FTRemark.Text = ""
        FNExportQty.Value = 0
        FNExportNetCostAmt.Value = 0
        FNAccFabStockOtherCost.Value = 0
        FNFabricRcvCost.Value = 0
        FNAccessroryRcvCost.Value = 0
        FNFabricDummyCost.Value = 0
        FNAccessroryDummyCost.Value = 0
        FNFabricBalCost.Value = 0
        FNAccessroryBalCost.Value = 0
        FNAccFabStockRcvCost.Value = 0
        FNAccFabStockOtherRcvCost.Value = 0
        FNAccFabStockBalCost.Value = 0
        FNAccFabStockOtherBalCost.Value = 0
        FTCustomerPO.Text = ""
        FNHSysStyleId.Text = ""
        FNHSysCustId.Text = ""
        FDShipDate.Text = ""
        FNOrderQuantity.Value = 0
        FTCmpName.Text = ""

        FNWagePull.Value = 0

        Me.FTStateFinish.Checked = False
        Try
            Dim _Qry As String = ""
            Dim _dt As DataTable

            _Qry = "   Select A.FTOrderNo"
            _Qry &= vbCrLf & "    ,A.FTStyleCode"
            _Qry &= vbCrLf & "    ,A.FTCustCode"
            _Qry &= vbCrLf & " 	 ,A.FTPORef"
            _Qry &= vbCrLf & " 	 ,CASE WHEN ISDATE(FDShipDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDShipDate) ,103)  Else ''END AS FDShipDate"
            _Qry &= vbCrLf & " 	 ,FNGrandQuantity"
            _Qry &= vbCrLf & " 	 ,FTCmpName,FNOrderType"
            _Qry &= vbCrLf & "  FROM (SELECT O.FTOrderNo"
            _Qry &= vbCrLf & "    , ST.FTStyleCode"
            _Qry &= vbCrLf & "    , CT.FTCustCode"
            _Qry &= vbCrLf & "   , O.FTPORef"
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
            _Qry &= vbCrLf & " ,O.FNOrderType"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN"
            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS CT WITH(NOLOCK)  ON O.FNHSysCustId = CT.FNHSysCustId"
            _Qry &= vbCrLf & "       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK)  ON O.FNHSysCmpId = Cmp.FNHSysCmpId"
            _Qry &= vbCrLf & "   WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(Key) & "'"
            _Qry &= vbCrLf & " 	 ) AS A"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            'Me.ogv.Columns.ColumnByFieldName("FTOrderNoRef").Visible = False
            'Me.ogv.Columns.ColumnByFieldName("FDShipDate").Visible = False
            'Me.ogv.Columns.ColumnByFieldName("FTCustomerPO").Visible = False

            For Each R As DataRow In _dt.Rows
                FTCustomerPO.Text = R!FTPORef.ToString
                FNHSysStyleId.Text = R!FTStyleCode.ToString
                FNHSysCustId.Text = R!FTCustCode.ToString
                FDShipDate.Text = R!FDShipDate.ToString
                FNOrderQuantity.Value = Val(R!FNGrandQuantity.ToString())
                FTCmpName.Text = R!FTCmpName.ToString


                'If (Integer.Parse(Val(R!FNOrderType.ToString)) = 17) Then

                '    Me.ogv.Columns.ColumnByFieldName("FTCustomerPO").Visible = True
                '    Me.ogv.Columns.ColumnByFieldName("FTCustomerPO").VisibleIndex = 0
                '    Me.ogv.Columns.ColumnByFieldName("FDShipDate").Visible = True
                '    Me.ogv.Columns.ColumnByFieldName("FDShipDate").VisibleIndex = 0
                '    Me.ogv.Columns.ColumnByFieldName("FTOrderNoRef").Visible = True
                '    Me.ogv.Columns.ColumnByFieldName("FTOrderNoRef").VisibleIndex = 0

                'End If

                Exit For
            Next

            _Qry = "SELECT TOP 1  FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
            _Qry &= vbCrLf & " , FTOrderNo, FNExportAmt, FNFabricCost, FNAccessroryCost, FNAccFabStockCost"
            _Qry &= vbCrLf & " , FNExportNetAmt, FNEmbSubCost, FNPrintSubCost"
            _Qry &= vbCrLf & " , FNEmbBranchCost, FNPrintBranchCost, FNEmbFacCost, FNWageCost, FNProdCost"
            _Qry &= vbCrLf & " , FNConductedCost, FNCommissionCost, FNImportCost, FNExportCost, FNTransportCost"
            _Qry &= vbCrLf & " , FNTransportAirCost, FNOtherCost,FNNetCost, FNNetProfit, FNProfitPerPiece, FTRemark,FNExportQty"
            _Qry &= vbCrLf & " , FNServiceCost,FNConductedCostPer,FNCommissionCostPer "
            _Qry &= vbCrLf & "  , FNAccFabStockOtherCost, FNFabricRcvCost, FNAccessroryRcvCost"
            _Qry &= vbCrLf & " , FNFabricDummyCost, FNAccessroryDummyCost, FNFabricBalCost"
            _Qry &= vbCrLf & " , FNAccessroryBalCost, FNAccFabStockRcvCost, FNAccFabStockOtherRcvCost"
            _Qry &= vbCrLf & " , FNAccFabStockBalCost, FNAccFabStockOtherBalCost,FNExportNetCostAmt,FNNetProfitRcv,FNProfitPerPieceRcv,FNWagePull"
            _Qry &= vbCrLf & " ,FNWageCutSew,FNWageCutTrim,FTStateFinish,ISNULL(FNExportGAmt,FNExportAmt) AS FNExportGAmt,ISNULL(FNDebitAmt,0) AS FNDebitAmt "
            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key) & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            For Each R As DataRow In _dt.Rows

                FNExportQty.Value = Val(R!FNExportQty.ToString)
                FNExportGAmt.Value = Val(R!FNExportGAmt.ToString())
                FNDebitAmt.Value = Val(R!FNDebitAmt.ToString())
                FNExportAmt.Value = Val(R!FNExportAmt.ToString())
                FNFabricCost.Value = Val(R!FNFabricCost.ToString())
                FNAccessroryCost.Value = Val(R!FNAccessroryCost.ToString())
                FNAccFabStockCost.Value = Val(R!FNAccFabStockCost.ToString())
                FNServiceCost.Value = Val(R!FNServiceCost.ToString())
                FNExportNetAmt.Value = Val(R!FNExportNetAmt.ToString())
                FNEmbSubCost.Value = Val(R!FNEmbSubCost.ToString())
                FNPrintSubCost.Value = Val(R!FNPrintSubCost.ToString())
                FNEmbBranchCost.Value = Val(R!FNEmbBranchCost.ToString())
                FNPrintBranchCost.Value = Val(R!FNPrintBranchCost.ToString())
                FNEmbFacCost.Value = Val(R!FNEmbFacCost.ToString())
                FNWageCost.Value = Val(R!FNWageCost.ToString())
                FNProdCost.Value = Val(R!FNProdCost.ToString())
                FNConductedCostPer.Value = Val(R!FNConductedCostPer.ToString())
                FNConductedCost.Value = Val(R!FNConductedCost.ToString())
                FNCommissionCostPer.Value = Val(R!FNCommissionCostPer.ToString())
                FNCommissionCost.Value = Val(R!FNCommissionCost.ToString())
                FNImportCost.Value = Val(R!FNImportCost.ToString())
                FNExportCost.Value = Val(R!FNExportCost.ToString())
                FNTransportCost.Value = Val(R!FNTransportCost.ToString())
                FNTransportAirCost.Value = Val(R!FNTransportAirCost.ToString())
                FNOtherCost.Value = Val(R!FNOtherCost.ToString())
                FNNetCost.Value = Val(R!FNNetCost.ToString())
                FNNetProfit.Value = Val(R!FNNetProfit.ToString())
                FNProfitPerPiece.Value = Val(R!FNProfitPerPiece.ToString())
                FTRemark.Text = R!FTRemark.ToString

                FNAccFabStockOtherCost.Value = Val(R!FNAccFabStockOtherCost.ToString())
                FNFabricRcvCost.Value = Val(R!FNFabricRcvCost.ToString())
                FNAccessroryRcvCost.Value = Val(R!FNAccessroryRcvCost.ToString())
                FNFabricDummyCost.Value = Val(R!FNFabricDummyCost.ToString())
                FNAccessroryDummyCost.Value = Val(R!FNAccessroryDummyCost.ToString())
                FNFabricBalCost.Value = Val(R!FNFabricBalCost.ToString())
                FNAccessroryBalCost.Value = Val(R!FNAccessroryBalCost.ToString())
                FNAccFabStockRcvCost.Value = Val(R!FNAccFabStockRcvCost.ToString())
                FNAccFabStockOtherRcvCost.Value = Val(R!FNAccFabStockOtherRcvCost.ToString())
                FNAccFabStockBalCost.Value = Val(R!FNAccFabStockBalCost.ToString())
                FNAccFabStockOtherBalCost.Value = Val(R!FNAccFabStockOtherBalCost.ToString())
                FNExportNetCostAmt.Value = Val(R!FNExportNetCostAmt.ToString())

                FNNetProfitRcv.Value = Val(R!FNNetProfitRcv.ToString())
                FNProfitPerPieceRcv.Value = Val(R!FNProfitPerPieceRcv.ToString())

                FNWageCutSew.Value = Val(R!FNWageCutSew.ToString)
                FNWageCutTrim.Value = Val(R!FNWageCutTrim.ToString)
                FTStateFinish.Checked = (R!FTStateFinish.ToString = "1")
                FNWagePull.Value = Val(R!FNWagePull.ToString)

                Exit For

            Next

            Call LoadOrderNoRef(Key)
            Call LoadDetailOrderInvoicecharge(Key)
            Call LoadDetailOrderCost(Key)
            Call LoadDetailOrderCostRawmat(Key)
            Call LoadDetailOrderCostRawmatDetail(Key)
            Call LoadWagePull(Key)
            Call LoadImport(Key)
            Call CalculatePer()

        Catch ex As Exception
        End Try

        Me.otbmain.SelectedTabPageIndex = 0
        _StateLoad = False
        _Spls.Close()
    End Sub

    Private Sub LoadWagePull(Key As String)

        Dim _Cmd As String = ""

        _Cmd = "  SELECT Sum(FNAmount) AS FNAmount"
        _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder_Detail AS X WITH(NOLOCK) "
        _Cmd &= vbCrLf & " WHERE  (FTOrderNoTo = N'" & HI.UL.ULF.rpQuoted(Key) & "')"

        FNWagePull.Value = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0"))

    End Sub

    Private Sub LoadImport(Key As String)
        Try
            Dim _Cmd As String = ""
            _Cmd = "  SELECT Sum(FNAmount) AS FNAmount"
            _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Order AS X WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE  (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Key) & "')"
            If Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0")) > 0 Then
                ' FNImportCost.Value = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0"))
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub LoadOrderNoRef(ByVal Key As Object)
        Dim _Qry As String
        Dim _dt As DataTable

        _Qry = "SELECT FTOrderNoRef,FTCustomerPO "
        _Qry &= vbCrLf & " 	,CASE WHEN ISDATE(FDShipDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDShipDate) ,103)  Else ''END AS FDShipDate"
        _Qry &= vbCrLf & "  FROM ( SELECT  FTOrderNo AS FTOrderNoRef "
        _Qry &= vbCrLf & "  ,FTPORef AS FTCustomerPO"
        _Qry &= vbCrLf & "  ,ISNULL(("
        _Qry &= vbCrLf & " 	SELECT TOP 1 FDShipDate"
        _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK)"
        _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = A.FTOrderNo "
        _Qry &= vbCrLf & " 	ORDER BY    FDShipDate ASC"
        _Qry &= vbCrLf & " 	),'') AS FDShipDate"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE  (A.FTOrderNoRef = N'" & HI.UL.ULF.rpQuoted(Key) & "')"
        _Qry &= vbCrLf & ") AS A"
        _Qry &= vbCrLf & " ORDER BY FTOrderNoRef  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        RepFTOrderNoRef.DataSource = _dt.Copy

    End Sub

    Private Sub LoadDetailOrderInvoicecharge(Key As String)
        Dim _Qry As String
        Dim _dt As DataTable

        _Qry = "SELECT A.FTInvoiceNo"
        _Qry &= vbCrLf & "  , A.FNWageCost, A.FNProdCost"
        _Qry &= vbCrLf & " , A.FNExportCost, A.FNTransportCost, A.FNTransportAirCost"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice_Cost AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "   WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Key) & "')"
        _Qry &= vbCrLf & "   ORDER BY A.FTInvoiceNo"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        Me.ogcinvoicecharge.DataSource = _dt.Copy

    End Sub

    Private Sub LoadDetailOrderCost(Key As String)
        Dim _Qry As String
        Dim _dt As DataTable

        _Qry = "SELECT ISNULL(A.FTOrderNoRef,'') AS FTOrderNoRef "
        _Qry &= vbCrLf & " ,ISNULL(B.FDShipDate,'') AS FDShipDate"
        _Qry &= vbCrLf & ",ISNULL(B.FTCustomerPO,'') AS FTCustomerPO "
        _Qry &= vbCrLf & "  ,A.FTInvoiceNo"
        _Qry &= vbCrLf & "  , Convert(nvarchar(10),Convert(datetime,A.FDInvoiceDate),103) AS FDInvoiceDate"
        _Qry &= vbCrLf & "   , A.FNExportQuantity, A.FNStockQuantity, A.FNPrice"
        _Qry &= vbCrLf & "  , A.FNExchangeRate, A.FNExportAmt, A.FNExportAmtTHB  , isnull(A.FTStateCalCost,'0') AS  FTStateCalCost "
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS A WITH(NOLOCK)"

        _Qry &= vbCrLf & "   LEFT OUTER JOIN ("

        _Qry &= vbCrLf & "SELECT FTOrderNoRef,FTCustomerPO "
        _Qry &= vbCrLf & " 	,CASE WHEN ISDATE(FDShipDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDShipDate) ,103)  Else ''END AS FDShipDate"
        _Qry &= vbCrLf & "  FROM ( SELECT  FTOrderNo AS FTOrderNoRef "
        _Qry &= vbCrLf & "  ,FTPORef AS FTCustomerPO"
        _Qry &= vbCrLf & "  ,ISNULL(("
        _Qry &= vbCrLf & " 	SELECT TOP 1 FDShipDate"
        _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK)"
        _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = A.FTOrderNo "
        _Qry &= vbCrLf & " 	ORDER BY    FDShipDate ASC"
        _Qry &= vbCrLf & " 	),'') AS FDShipDate"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE  (A.FTOrderNoRef = N'" & HI.UL.ULF.rpQuoted(Key) & "')"
        _Qry &= vbCrLf & ") AS A"
        _Qry &= vbCrLf & " "

        _Qry &= vbCrLf & "  ) AS B ON A.FTOrderNoRef = B.FTOrderNoRef"

        _Qry &= vbCrLf & "   WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Key) & "')"
        _Qry &= vbCrLf & "   ORDER BY A.FDInvoiceDate"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        _dt.Rows.Add("", "", "", "", "", 0, 0, 0, 0, 0, 0)

        Me.ogc.DataSource = _dt.Copy

    End Sub

    Private Sub LoadDetailOrderCostRawmat(Key As String)
        Dim _Qry As String
        Dim _dt As DataTable

        Dim _FNHSysCmpID As Integer = 0

        _Qry = "SELECt TOP 1 FNHSysCmpId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
        _FNHSysCmpID = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))


        _Qry = "  SELECT ISNULL(MM.FNMerMatType,1) AS  FNMerMatType"
        _Qry &= vbCrLf & "  ,SUM(A.FNAmount + FNINVChargeAmt ) AS FNAmount"
        _Qry &= vbCrLf & "   ,A.FTStateRsv,FTStateWH"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & " ("
        _Qry &= vbCrLf & "    SELECT FNHSysRawMatId,FNPrice,FNQuantity,FNQuantityRet,FNQuantityRts,FNAmount"
        _Qry &= vbCrLf & "	,FTStateRsv"
        _Qry &= vbCrLf & "   ,FTInvoiceNo"
        _Qry &= vbCrLf & "  ,FTReceiveNo,FTStateWH"
        _Qry &= vbCrLf & "  ,CASE WHEN FTInvoiceNo ='' THEN 0.00 ELSE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.FN_GET_Invoice_Charge_Amt(FTInvoiceNo,FTReceiveNo,FNHSysRawMatId,FNAmount) END AS FNINVChargeAmt"
        _Qry &= vbCrLf & " FROM (SELECT FNHSysRawMatId  "
        _Qry &= vbCrLf & " ,FNPrice"
        _Qry &= vbCrLf & "  ,Sum(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " ,Sum(FNQuantityRet) AS FNQuantityRet"
        _Qry &= vbCrLf & "  ,Sum(FNQuantityRts) AS FNQuantityRts"
        _Qry &= vbCrLf & "   ,Sum(FNAmount) AS FNAmount"
        _Qry &= vbCrLf & "   ,FTStateRsv"
        _Qry &= vbCrLf & "   ,FTInvoiceNo"
        _Qry &= vbCrLf & "   ,FTReceiveNo,FTStateWH"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & " (SELECT  FTIssueNo"
        _Qry &= vbCrLf & " ,FNHSysRawMatId  "
        _Qry &= vbCrLf & "  ,FNPrice"
        _Qry &= vbCrLf & "  ,Sum(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  ,Sum(FNQuantityRet) AS FNQuantityRet"
        _Qry &= vbCrLf & "   ,Sum(FNQuantityRts) AS FNQuantityRts"
        _Qry &= vbCrLf & "    ,Convert(numeric(18,2),Sum(FNQuantity - (FNQuantityRet+FNQuantityRts) ) * FNPrice)  AS FNAmount"
        _Qry &= vbCrLf & "  ,FTStateRsv"
        _Qry &= vbCrLf & "   ,FTInvoiceNo"
        _Qry &= vbCrLf & "   ,FTReceiveNo,FTStateWH"
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " (SELECT	 H.FTIssueNo,BC.FNHSysRawMatId, H.FTOrderNo, B.FTBarcodeNo,BC.FNPrice "
        _Qry &= vbCrLf & " ,B.FTDocumentNo, B.FNHSysWHId, B.FNQuantity"
        _Qry &= vbCrLf & " ,ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN  AS BI WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE BI.FTDocumentRefNo = B.FTDocumentNo"
        _Qry &= vbCrLf & "   AND BI.FTBarcodeNo = B.FTBarcodeNo "
        _Qry &= vbCrLf & " ),0) AS FNQuantityRet"
        _Qry &= vbCrLf & " ,ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue  AS BI WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE BI.FTDocumentRefNo = B.FTDocumentNo"
        _Qry &= vbCrLf & "  AND BI.FTBarcodeNo = B.FTBarcodeNo "
        _Qry &= vbCrLf & " ),0) AS FNQuantityRts"
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(RSV.FTReserveNo ,'')='' THEN (CASE WHEN ISNULL(("
        _Qry &= vbCrLf & "  SELECT TOP 1  XRSV.FTReserveNo "
        _Qry &= vbCrLf & "    FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS XB WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "              [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS XRSV WITH(NOLOCK) ON XB.FTDocumentRefNo = XRSV.FTReserveNo "
        _Qry &= vbCrLf & "     WHERE   XB.FTBarcodeNo=B.FTBarcodeNo "
        _Qry &= vbCrLf & "     AND    XB.FTDocumentNo=B.FTDocumentRefNo AND ISNULL(XRSV.FTStateAutoFromCenter,'') <>'1' "
        _Qry &= vbCrLf & "            ),'')='' THEN '0' ELSE  (CASE WHEN  ISNULL(RSV.FTStateAutoFromCenter,'') <>'1' THEN '1' ELSE '0' END) END ) ELSE  (CASE WHEN  ISNULL(RSV.FTStateAutoFromCenter,'') <>'1' THEN '1' ELSE '0' END)  END AS FTStateRsv"
        _Qry &= vbCrLf & " ,ISNULL(R.FTInvoiceNo,'') AS FTInvoiceNo"
        _Qry &= vbCrLf & " ,ISNULL(R.FTReceiveNo,'') AS FTReceiveNo"
        _Qry &= vbCrLf & " ,CASE WHEN WH.FNHSysCmpId=" & _FNHSysCmpID & " THEN  "
        _Qry &= vbCrLf & " ( CASE WHEN "
        _Qry &= vbCrLf & "  ISNULL(("
        _Qry &= vbCrLf & "  SELECT TOP 1  CASE WHEN  XWH.FNHSysCmpId=" & _FNHSysCmpID & " THEN '' ELSE  '1' END  "
        ' _Qry &= vbCrLf & "    FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS XB WITH(NOLOCK) INNER JOIN"


        _Qry &= vbCrLf & " FROM ("
        _Qry &= vbCrLf & "  SELECT B.*,"
        _Qry &= vbCrLf & "  ISNULL((SELECT TOP 1 XBZ.FTReserveNo "
        _Qry &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS XX WITH(NOLOCK) INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS XBZ WITH(NOLOCK) ON XX.FTDocumentNo = XBZ.FTReserveNo "
        _Qry &= vbCrLf & "WHERE XX.FTBarcodeNo = B.FTBarcodeNo"
        _Qry &= vbCrLf & "	   AND XX.FTOrderNo =B.FTOrderNo "
        _Qry &= vbCrLf & "   AND XX.FTDocumentRefNo = B.FTDocumentNo "
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "	),"

        _Qry &= vbCrLf & " ISNULL((SELECT TOP 1 XBZ.FTReserveNo "
        _Qry &= vbCrLf & " FROM  [HITECH_INVENTORY].dbo.TINVENBarcode_OUT AS XX WITH(NOLOCK) INNER JOIN   [HITECH_INVENTORY].dbo.TINVENReserve AS XBZ WITH(NOLOCK) ON XX.FTDocumentRefNo = XBZ.FTReserveNo "
        _Qry &= vbCrLf & "  WHERE XX.FTBarcodeNo = B.FTBarcodeNo"
        _Qry &= vbCrLf & "  AND XX.FTOrderNo =B.FTOrderNo "
        _Qry &= vbCrLf & " AND XX.FTDocumentNo = B.FTDocumentRefNo "

        _Qry &= vbCrLf & "),'')"

        _Qry &= vbCrLf & ") AS FTReserveNo"
        _Qry &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK)"
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "   ) AS XB "

        _Qry &= vbCrLf & "         INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS XRSV WITH(NOLOCK) ON XB.FTReserveNo = XRSV.FTReserveNo "
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XWH WITH(NOLOCK) ON XRSV.FNHSysWHId = XWH.FNHSysWHId "
        _Qry &= vbCrLf & "     WHERE   XB.FTBarcodeNo=B.FTBarcodeNo "
        _Qry &= vbCrLf & "     AND    XB.FTDocumentNo=B.FTDocumentNo "
        _Qry &= vbCrLf & "            ),'')='' THEN '0' ELSE '1' END) "
        _Qry &= vbCrLf & " ELSE '1' END AS FTStateWH"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK) INNER JOIN"
        ' _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK) ON H.FTIssueNo = B.FTDocumentNo LEFT OUTER JOIN"

        _Qry &= vbCrLf & "  ("
        _Qry &= vbCrLf & "  SELECT B.*,"
        _Qry &= vbCrLf & "  ISNULL((SELECT TOP 1 XBZ.FTReserveNo "
        _Qry &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS XX WITH(NOLOCK) INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS XBZ WITH(NOLOCK) ON XX.FTDocumentNo = XBZ.FTReserveNo "
        _Qry &= vbCrLf & "WHERE XX.FTBarcodeNo = B.FTBarcodeNo"
        _Qry &= vbCrLf & "	   AND XX.FTOrderNo =B.FTOrderNo "
        _Qry &= vbCrLf & " AND XX.FTDocumentNo = B.FTDocumentRefNo  "
        _Qry &= vbCrLf & "	),'') AS FTReserveNo"
        _Qry &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK)"
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "   ) AS B ON H.FTIssueNo = B.FTDocumentNo LEFT OUTER JOIN"

        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS RSV WITH(NOLOCK) ON B.FTReserveNo = RSV.FTReserveNo "
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BC WITH(NOLOCK) ON B.FTBarcodeNo = BC.FTBarcodeNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK) ON B.FTDocumentNo = R.FTReceiveNo "
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WH WITH(NOLOCK) ON H.FNHSysWHId = WH.FNHSysWHId "
        _Qry &= vbCrLf & " WHERE  (H.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Key) & "')"
        _Qry &= vbCrLf & " ) AS A"
        _Qry &= vbCrLf & " GROUP BY FTIssueNo"
        _Qry &= vbCrLf & "	,FNHSysRawMatId  "
        _Qry &= vbCrLf & "  ,FNPrice"
        _Qry &= vbCrLf & "  ,FTStateRsv"
        _Qry &= vbCrLf & " ,FTInvoiceNo"
        _Qry &= vbCrLf & "  ,FTReceiveNo,FTStateWH) AS A2"
        _Qry &= vbCrLf & " GROUP BY "
        _Qry &= vbCrLf & "  FNHSysRawMatId  "
        _Qry &= vbCrLf & ",FNPrice	 "
        _Qry &= vbCrLf & " ,FTStateRsv"
        _Qry &= vbCrLf & "  ,FTInvoiceNo"
        _Qry &= vbCrLf & " ,FTReceiveNo,FTStateWH) AS A3"
        _Qry &= vbCrLf & " ) AS A "
        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        _Qry &= vbCrLf & " GROUP BY ISNULL(MM.FNMerMatType,1) ,FTStateRsv,FTStateWH"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Dim _FNFabricCost As Double = 0
        Dim _FNAccessroryCost As Double = 0
        Dim _FNAccFabStockCost As Double = 0
        Dim _FNAccFabStockCostOther As Double = 0

        For Each R As DataRow In _dt.Rows
            If R!FTStateRsv.ToString = "1" Then
                If R!FTStateWH.ToString = "1" Then
                    _FNAccFabStockCostOther = _FNAccFabStockCostOther + CDbl(Val(R!FNAmount.ToString))
                Else
                    _FNAccFabStockCost = _FNAccFabStockCost + CDbl(Val(R!FNAmount.ToString))
                End If

            Else
                If Val(R!FNMerMatType.ToString) = 0 Then
                    _FNFabricCost = _FNFabricCost + CDbl(Val(R!FNAmount.ToString))
                Else
                    _FNAccessroryCost = _FNAccessroryCost + CDbl(Val(R!FNAmount.ToString))
                End If
            End If
        Next

        _Qry = "  Select SUM(FNAmount)"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService_Detail AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTOrderNo =  N'" & HI.UL.ULF.rpQuoted(Key) & "')"

        FNServiceCost.Value = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0"))

        '_Qry = "  SELECT FNSendSuplType,FNSubContactType,SUM(Convert(numeric(18,2), FNQuantity * FNPrice)) AS FNAmount"
        '_Qry &= vbCrLf & "    FROM "
        '_Qry &= vbCrLf & " (SELECT A.FTPurchaseNo,  P.FTOrderNo, SUM(BD.FNQuantity) AS FNQuantity, D.FNPrice,D.FNSendSuplType ,S.FNSubContactType"
        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocSendRef AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B WITH(NOLOCK)  ON A.FTSendSuplNo = B.FTSendSuplNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS C WITH(NOLOCK) ON B.FTBarcodeSendSuplNo = C.FTBarcodeSendSuplNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON C.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BD WITH(NOLOCK) ON C.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_Detail AS D WITH(NOLOCK) ON A.FTPurchaseNo = D.FTPurchaseNo AND C.FNHSysPartId = D.FNHSysPartId AND C.FNSendSuplType = D.FNSendSuplType"
        '_Qry &= vbCrLf & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl AS H WITH( NOLOCK) ON D.FTPurchaseNo = H.FTPurchaseNo "
        '_Qry &= vbCrLf & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK ) ON H.FNHSysSuplId = S.FNHSysSuplId "
        '_Qry &= vbCrLf & "  WHERE P.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "' "
        '_Qry &= vbCrLf & " GROUP BY A.FTPurchaseNo,  P.FTOrderNo,  D.FNPrice,D.FNSendSuplType,S.FNSubContactType) AS A"
        '_Qry &= vbCrLf & " GROUP BY FNSendSuplType,FNSubContactType "

        'Dim _FNEmbSubCost As Double = 0
        'Dim _FNPrintSubCost As Double = 0
        'Dim _FNEmbBranchCost As Double = 0
        'Dim _FNPrintBranchCost As Double = 0

        'Dim _dtempprint As DataTable
        '_dtempprint = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'For Each R As DataRow In _dtempprint.Rows
        '    Select Case Integer.Parse(Val(R!FNSendSuplType.ToString))
        '        Case 0
        '            Select Case Integer.Parse(Val(R!FNSubContactType.ToString))
        '                Case 0
        '                    _FNEmbBranchCost = _FNEmbBranchCost + Val(R!FNAmount.ToString)
        '                Case 1
        '                    _FNEmbSubCost = _FNEmbSubCost + Val(R!FNAmount.ToString)
        '            End Select
        '        Case 1
        '            Select Case Integer.Parse(Val(R!FNSubContactType.ToString))
        '                Case 0
        '                    _FNPrintSubCost = _FNPrintSubCost + Val(R!FNAmount.ToString)
        '                Case 1
        '                    _FNEmbBranchCost = _FNEmbBranchCost + Val(R!FNAmount.ToString)
        '            End Select
        '    End Select
        'Next

        FNFabricCost.Value = _FNFabricCost
        FNAccessroryCost.Value = _FNAccessroryCost
        FNAccFabStockCost.Value = _FNAccFabStockCost
        FNAccFabStockOtherCost.Value = _FNAccFabStockCostOther
        'FNEmbSubCost.Value = _FNEmbSubCost
        'FNPrintSubCost.Value = _FNPrintSubCost
        'FNEmbBranchCost.Value = _FNEmbBranchCost
        'FNPrintBranchCost.Value = _FNPrintBranchCost

        Call LoadDetailOrderCostRawmatDataRcv(Key)

    End Sub

    Private Sub LoadDetailOrderCostRawmatDataRcv(Key As String)
        Dim _Qry As String
        Dim _dt As DataTable

        Dim _FNHSysCmpID As Integer = 0

        _Qry = "SELECt TOP 1 FNHSysCmpId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
        _FNHSysCmpID = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))


        _Qry = "    Select A.FTOrderNo"
        _Qry &= vbCrLf & "  	, A.FNMerMatType "
        _Qry &= vbCrLf & "    ,((ISNULL(B.FNStockRcvAmt,0)+ISNULL(TRI.TRIAmt,0) ) - (ISNULL(TRO.TROAmt,0)+ISNULL(RTS.FNStockRtsAmt,0) +ISNULL(RTS2.FNStockRtsAmt,0))) AS FNStockRcvAmt"
        _Qry &= vbCrLf & "    ,ISNULL(C.FNTrwAmt,0) AS FNTrwAmt"
        _Qry &= vbCrLf & "   FROM  ( SELECT A.FTOrderNo ,0 AS FNMerMatType"
        _Qry &= vbCrLf & "  	FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  	WHERE  A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "'"
        _Qry &= vbCrLf & "     UNION"
        _Qry &= vbCrLf & "  	SELECT A.FTOrderNo ,1 AS FNMerMatType"
        _Qry &= vbCrLf & "  	FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  	WHERE  A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "'"
        _Qry &= vbCrLf & "  	 ) AS A"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   (SELECT X.FTOrderNo,X.FNMerMatType ,SUM(X.FNStockRcvAmt) AS FNStockRcvAmt"
        _Qry &= vbCrLf & " FROM (SELECT A.FTOrderNo"
        _Qry &= vbCrLf & "  	,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType"
        _Qry &= vbCrLf & "  	, SUM(B.FNNetStockAmt) AS FNStockRcvAmt"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS C WITH(NOLOCK)  ON B.FTReceiveNo = C.FTReceiveNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON C.FNHSysWHId = W.FNHSysWHId AND A.FNHSysCmpId = W.FNHSysCmpId"
        _Qry &= vbCrLf & "  	   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "'"
        _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo"
        _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END"
        _Qry &= vbCrLf & " UNION ALL"

        _Qry &= vbCrLf & "SELECT A.FTOrderNo"
        _Qry &= vbCrLf & "  	,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType"
        _Qry &= vbCrLf & "  	, SUM(Convert(numeric(18,2),B.FNQuantity * B.FNPrice)) AS FNStockRcvAmt"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail_Finish AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion AS C WITH(NOLOCK)  ON B.FTConversionNo = C.FTConversionNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON C.FNHSysWHId = W.FNHSysWHId AND A.FNHSysCmpId = W.FNHSysCmpId"
        _Qry &= vbCrLf & "  	   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "'"
        _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo"
        _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END ) AS X"
        _Qry &= vbCrLf & "   GROUP BY X.FTOrderNo,X.FNMerMatType"
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FNMerMatType = B.FNMerMatType"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
        _Qry &= vbCrLf & "   SELECT M.FTOrderNo,FNMerMatType,SUM(FNTrwAmt-(FNTrwRetAmt+FNTrwRetAfIssAmt + FNTrwRetAfIssRtsAmt)) AS FNTrwAmt"
        _Qry &= vbCrLf & "   FROM ("
        _Qry &= vbCrLf & "   SELECT A.FTOrderNo,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType, (Convert(numeric(18,2),B.FNQuantity * BC.FNPrice)) AS FNTrwAmt"
        _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
        _Qry &= vbCrLf & "  	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS X  WITH(NOLOCK)"
        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS XA WITH(NOLOCK) ON  XA.FTTransferWHNo = X.FTDocumentNo"
        _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON XA.FNHSysWHIdTo = XW.FNHSysWHId "
        _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
        ' _Qry &= vbCrLf & "      LEFT OUTER  JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS RSXA WITH(NOLOCK) ON  X.FTDocumentRefNo = RSXA.FTReserveNo"
        _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
        _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
        _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
        _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
        ' _Qry &= vbCrLf & "  	  AND  RSXA.FTReserveNo IS NULL"
        _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAmt"

        _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
        _Qry &= vbCrLf & "  	FROM "
        _Qry &= vbCrLf & " ("
        _Qry &= vbCrLf & ""

        _Qry &= vbCrLf & "  SELECT TRWHD.FTBarcodeNo, TRWHD.FTDocumentNo, TRWHD.FNHSysWHId, TRWHD.FTOrderNo"
        _Qry &= vbCrLf & ", TRWHD.FNQuantity, TH.FTTransferWHNo AS FTDocumentRefNo, TRWHD.FNHSysCmpId, TRWHD.FNPriceTrans, TRWHD.FTStateReserve"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TRWH WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS TRWHD  WITH(NOLOCK)  ON TRWH.FTTransferWHNo = TRWHD.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A WITH(NOLOCK)   INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK)   ON A.FTDocumentNo = H.FTIssueNo INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TH WITH(NOLOCK)   ON A.FTDocumentRefNo = TH.FTTransferWHNo INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS RETD  WITH(NOLOCK)  ON A.FTBarcodeNo = RETD.FTBarcodeNo AND A.FTOrderNo = RETD.FTOrderNo AND A.FTDocumentNo = RETD.FTDocumentRefNo INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToStock AS PH WITH(NOLOCK)   ON RETD.FTDocumentNo = PH.FTReturnStockNo ON TRWHD.FTBarcodeNo = RETD.FTBarcodeNo AND TRWHD.FTOrderNo = RETD.FTOrderNo AND "
        _Qry &= vbCrLf & " TRWHD.FTDocumentRefNo = RETD.FTDocumentNo And TRWHD.FNHSysWHId = RETD.FNHSysWHId"

        ' [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS X  WITH(NOLOCK)"
        _Qry &= vbCrLf & " ) AS X"
        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS XA WITH(NOLOCK) ON  XA.FTTransferWHNo = X.FTDocumentNo"
        _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON XA.FNHSysWHIdTo = XW.FNHSysWHId "
        _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
        _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
        _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
        _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
        _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
        _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAfIssAmt"


        _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
        _Qry &= vbCrLf & "  	FROM "
        _Qry &= vbCrLf & " ("
        _Qry &= vbCrLf & ""

        _Qry &= vbCrLf & "  SELECT TH.FTTransferWHNo, RETD.FTBarcodeNo, RETD.FTDocumentNo, RETD.FNHSysWHId"
        _Qry &= vbCrLf & "   , RETD.FTOrderNo, RETD.FNQuantity, RETD.FTStateReserve, TH.FTTransferWHNo AS FTDocumentRefNo, RETD.FNHSysCmpId,  RETD.FNPriceTrans"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A  WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H  WITH(NOLOCK)  ON A.FTDocumentNo = H.FTIssueNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS RETD  WITH(NOLOCK)  ON A.FTBarcodeNo = RETD.FTBarcodeNo AND A.FTOrderNo = RETD.FTOrderNo AND A.FTDocumentNo = RETD.FTDocumentRefNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS PH  WITH(NOLOCK)  ON RETD.FTDocumentNo = PH.FTReturnSuplNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TH WITH(NOLOCK)  ON A.FTDocumentRefNo = TH.FTTransferWHNo"

        _Qry &= vbCrLf & " ) AS X"
        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS XA WITH(NOLOCK) ON  XA.FTReturnSuplNo = X.FTDocumentNo"
        _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON X.FNHSysWHId = XW.FNHSysWHId "
        _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
        _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
        _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
        _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
        _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
        _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAfIssRtsAmt"

        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON A.FNHSysCmpId = W.FNHSysCmpId INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS C WITH(NOLOCK)  ON W.FNHSysWHId = C.FNHSysWHIdTo INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo AND C.FTTransferWHNo = B.FTDocumentNo INNER JOIN"

        '_Qry &= vbCrLf & "  ("
        '_Qry &= vbCrLf & "     SELECT X.FTInsUser, X.FDInsDate, X.FTInsTime, X.FTUpdUser, X.FDUpdDate, X.FTUpdTime, X.FTBarcodeNo, X.FTDocumentNo, X.FNHSysWHId, X.FTOrderNo, X.FNQuantity, X.FTStateReserve, X.FNHSysCmpId, X.FNPriceTrans, "
        '_Qry &= vbCrLf & "       dbo.FN_TRANSFERWH_REF_RCVNO(X.FTDocumentNo, X.FTBarcodeNo, X.FTOrderNo) AS FTDocumentRefNo"
        '_Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS X WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS B WITH(NOLOCK)  ON X.FTDocumentNo = B.FTTransferWHNo"
        '_Qry &= vbCrLf & "     WHERE X.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "'"
        '_Qry &= vbCrLf & "   ) AS   B ON A.FTOrderNo = B.FTOrderNo AND C.FTTransferWHNo = B.FTDocumentNo INNER JOIN"

        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BC WITH(NOLOCK)  ON B.FTBarcodeNo = BC.FTBarcodeNo  INNER JOIN"
        _Qry &= vbCrLf & "  ( SELECT  FTReceiveNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R  WITH(NOLOCK)"
        _Qry &= vbCrLf & " UNION "
        _Qry &= vbCrLf & "  SELECT  FTConversionNo AS FTReceiveNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion AS R  WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ) AS R ON BC.FTDocumentNo = R.FTReceiveNo"

        '_Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R  WITH(NOLOCK) ON B.FTDocumentRefNo = R.FTReceiveNo"
        _Qry &= vbCrLf & "     INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON BC.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "

        _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W2 WITH(NOLOCK)  ON C.FNHSysWHId = W2.FNHSysWHId "

        '-----2016/05/31
        _Qry &= vbCrLf & "      LEFT OUTER  JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS RSXA WITH(NOLOCK) ON  B.FTDocumentRefNo = RSXA.FTReserveNo"
        '-----2016/05/31

        _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "' "
        _Qry &= vbCrLf & "   AND (BC.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "' OR BC.FTOrderNo IN (SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder WITH(NOLOCK) WHERE FTOrderNoTo=N'" & HI.UL.ULF.rpQuoted(Key) & "') )"
        _Qry &= vbCrLf & "   AND  (C.FTStateApprove = '1') AND W2.FNHSysCmpId<>" & _FNHSysCmpID & " "

        '-----2016/05/31
        _Qry &= vbCrLf & "  AND RSXA.FTReserveNo IS NULL "
        '-----2016/05/31

        _Qry &= vbCrLf & "   "
        _Qry &= vbCrLf & "   ) AS M"
        _Qry &= vbCrLf & "    GROUP BY M.FTOrderNo,FNMerMatType"
        _Qry &= vbCrLf & "   ) AS C ON A.FTOrderNo = C.FTOrderNo  AND A.FNMerMatType = C.FNMerMatType"

        _Qry &= vbCrLf & "   LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   (SELECT A.FTOrderNo"
        _Qry &= vbCrLf & "  	,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType"
        _Qry &= vbCrLf & "  	, SUM(Convert(numeric(18,2),BBO.FNQuantity * BB.FNPrice)) AS FNStockRtsAmt"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS C WITH(NOLOCK)  ON B.FTReceiveNo = C.FTReceiveNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON C.FNHSysWHId = W.FNHSysWHId AND A.FNHSysCmpId = W.FNHSysCmpId"
        _Qry &= vbCrLf & "  	   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "

        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BB WITH(NOLOCK)  ON B.FTOrderNo = BB.FTOrderNo AND B.FNHSysRawMatId = BB.FNHSysRawMatId AND B.FTReceiveNo=BB.FTDocumentNo"
        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BBO WITH(NOLOCK)  ON BB.FTBarcodeNo = BBO.FTBarcodeNo "
        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS RTSS WITH(NOLOCK)  ON BBO.FTDocumentNo = RTSS.FTReturnSuplNo "
        _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "'"
        _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo"
        _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END"
        _Qry &= vbCrLf & "   ) AS RTS ON A.FTOrderNo = RTS.FTOrderNo AND A.FNMerMatType = RTS.FNMerMatType"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   (SELECT A.FTOrderNo"
        _Qry &= vbCrLf & "  	,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType"
        _Qry &= vbCrLf & "  	, SUM(Convert(numeric(18,2),BBO.FNQuantity * BB.FNPrice)) AS FNStockRtsAmt"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS C WITH(NOLOCK)  ON B.FTReceiveNo = C.FTReceiveNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON C.FNHSysWHId = W.FNHSysWHId AND A.FNHSysCmpId = W.FNHSysCmpId"
        _Qry &= vbCrLf & "  	   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "

        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BB WITH(NOLOCK)  ON B.FTOrderNo = BB.FTOrderNo AND B.FNHSysRawMatId = BB.FNHSysRawMatId AND B.FTReceiveNo=BB.FTDocumentNo"
        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS BBO WITH(NOLOCK)  ON BB.FTBarcodeNo = BBO.FTBarcodeNo "
        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS RTSS WITH(NOLOCK)  ON BBO.FTDocumentNo = RTSS.FTReturnSuplNo "
        _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "'"
        _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo"
        _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END"
        _Qry &= vbCrLf & "   ) AS RTS2 ON A.FTOrderNo = RTS2.FTOrderNo AND A.FNMerMatType = RTS2.FNMerMatType"

        _Qry &= vbCrLf & "   LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   (SELECT A.FTOrderNoTo AS FTOrderNo"
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END AS  FNMerMatType"
        _Qry &= vbCrLf & " , SUM(Convert(numeric(18,2),B.FNQuantity*C.FNPrice)) AS TRIAmt "
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B  WITH(NOLOCK)   ON A.FTTransferOrderNo  = B.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C  WITH(NOLOCK)   ON B.FTBarcodeNo = C.FTBarcodeNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM  WITH(NOLOCK)   ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM   WITH(NOLOCK)  ON IM.FTRawMatCode = MM.FTMainMatCode"
        _Qry &= vbCrLf & "       INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON B.FNHSysWHId = W.FNHSysWHId "
        _Qry &= vbCrLf & "   WHERE A.FTOrderNoTo=N'" & HI.UL.ULF.rpQuoted(Key) & "' AND W.FNHSysCmpId=" & _FNHSysCmpID & ""
        _Qry &= vbCrLf & "  GROUP BY A.FTOrderNoTo,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END"
        _Qry &= vbCrLf & "   ) AS TRI ON A.FTOrderNo = TRI.FTOrderNo AND A.FNMerMatType = TRI.FNMerMatType"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   (SELECT A.FTOrderNo AS FTOrderNo"
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END AS  FNMerMatType"
        _Qry &= vbCrLf & " , SUM(Convert(numeric(18,2),B.FNQuantity*C.FNPrice)) AS TROAmt "
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B  WITH(NOLOCK)   ON A.FTTransferOrderNo  = B.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C  WITH(NOLOCK)   ON B.FTBarcodeNo = C.FTBarcodeNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM  WITH(NOLOCK)   ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM   WITH(NOLOCK)  ON IM.FTRawMatCode = MM.FTMainMatCode"
        _Qry &= vbCrLf & "       INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON B.FNHSysWHId = W.FNHSysWHId "
        _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "' AND W.FNHSysCmpId=" & _FNHSysCmpID & " "
        _Qry &= vbCrLf & "  GROUP BY A.FTOrderNo,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END"
        _Qry &= vbCrLf & "   ) AS TRO ON A.FTOrderNo = TRO.FTOrderNo AND A.FNMerMatType = TRO.FNMerMatType"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Dim _FNFabricRcvCost As Double = 0
        Dim _FNAccessroryRcvCost As Double = 0
        Dim _FNFabricDummyCost As Double = 0
        Dim _FNAccessroryDummyCost As Double = 0
        Dim _FNFabricBalCost As Double = 0
        Dim _FNAccessroryBalCost As Double = 0

        Dim _FNAccFabStockRcvCost, _FNAccFabStockOtherRcvCost, _FNAccFabStockBalCost, _FNAccFabStockOtherBalCost As Double

        _FNAccFabStockRcvCost = 0
        _FNAccFabStockOtherRcvCost = 0
        _FNAccFabStockBalCost = 0
        _FNAccFabStockOtherBalCost = 0

        For Each R As DataRow In _dt.Rows

            Select Case Integer.Parse(Val(R!FNMerMatType.ToString))
                Case 0
                    _FNFabricRcvCost = _FNFabricRcvCost + Val(R!FNStockRcvAmt) + Val(R!FNTrwAmt)
                Case Else
                    _FNAccessroryRcvCost = _FNAccessroryRcvCost + Val(R!FNStockRcvAmt) + Val(R!FNTrwAmt)
            End Select

        Next

        Dim _dtdummy As DataTable

        _Qry = "   SELECT M.FTOrderNo ,M.FTReceiveNo ,M.FTOrderMain "
        _Qry &= vbCrLf & "   FROM (SELECT X.*"
        _Qry &= vbCrLf & "   ,ISNULL(("
        _Qry &= vbCrLf & "   SELECT TOP 1 A.FTOrderNo"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
        _Qry &= vbCrLf & "   WHERE  (O.FNOrderType <> 4) AND A.FTReceiveNo = X.FTReceiveNo "
        _Qry &= vbCrLf & "  	),'') AS FTOrderMain "
        _Qry &= vbCrLf & "    FROM"
        _Qry &= vbCrLf & "   (SELECT A.FTOrderNo,A.FTReceiveNo "
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
        _Qry &= vbCrLf & "          INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BBB WITH(NOLOCK)"
        _Qry &= vbCrLf & "          ON A.FTReceiveNo=BBB.FTDocumentNo  AND  A.FTOrderNo = BBB.FTOrderNo AND A.FNHSysRawMatId = BBB.FNHSysRawMatId "
        _Qry &= vbCrLf & "    WHERE (O.FNOrderType = 4) AND  ISNULL(BBB.FTOrderNoRef,'') ='' "
        _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo,A.FTReceiveNo "
        _Qry &= vbCrLf & "   ) As X) AS M"
        _Qry &= vbCrLf & "  WHERE FTOrderMain=N'" & HI.UL.ULF.rpQuoted(Key) & "'"

        _dtdummy = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Dim _DummyNo As String = ""
        Dim _RcvNoNo As String = ""
        For Each Rx As DataRow In _dtdummy.Rows

            _DummyNo = Rx!FTOrderNo.ToString
            _RcvNoNo = Rx!FTReceiveNo.ToString

            _Qry = "    Select A.FTOrderNo"
            _Qry &= vbCrLf & "  	, A.FNMerMatType "
            _Qry &= vbCrLf & "    ,ISNULL(B.FNStockRcvAmt,0) AS FNStockRcvAmt"
            _Qry &= vbCrLf & "    ,ISNULL(C.FNTrwAmt,0) AS FNTrwAmt"
            _Qry &= vbCrLf & "   FROM  ( SELECT A.FTOrderNo ,0 AS FNMerMatType"
            _Qry &= vbCrLf & "  	FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  	WHERE  A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_DummyNo) & "'"
            _Qry &= vbCrLf & "     UNION"
            _Qry &= vbCrLf & "  	SELECT A.FTOrderNo ,1 AS FNMerMatType"
            _Qry &= vbCrLf & "  	FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  	WHERE  A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_DummyNo) & "'"
            _Qry &= vbCrLf & "  	 ) AS A"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   (SELECT A.FTOrderNo"
            _Qry &= vbCrLf & "  	,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType"
            _Qry &= vbCrLf & "  	, SUM(B.FNNetStockAmt) AS FNStockRcvAmt"
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS C WITH(NOLOCK)  ON B.FTReceiveNo = C.FTReceiveNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON C.FNHSysWHId = W.FNHSysWHId"
            _Qry &= vbCrLf & "  	   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
            _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "' AND C.FTReceiveNo='" & HI.UL.ULF.rpQuoted(_RcvNoNo) & "' AND W.FNHSysCmpId=" & _FNHSysCmpID & " "
            _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo"
            _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END"
            _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FNMerMatType = B.FNMerMatType"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "   SELECT M.FTOrderNo,FNMerMatType,SUM(FNTrwAmt-(FNTrwRetAmt+FNTrwRetAfIssAmt+FNTrwRetAfIssRtsAmt)) AS FNTrwAmt"
            _Qry &= vbCrLf & "   FROM ("
            _Qry &= vbCrLf & "   SELECT A.FTOrderNo,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType, (Convert(numeric(18,2),B.FNQuantity * BC.FNPrice)) AS FNTrwAmt"
            _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
            _Qry &= vbCrLf & "  	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS X  WITH(NOLOCK)"
            _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS XA WITH(NOLOCK) ON  XA.FTTransferWHNo = X.FTDocumentNo"
            _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON XA.FNHSysWHIdTo = XW.FNHSysWHId "
            _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
            _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
            _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
            _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
            _Qry &= vbCrLf & "        AND ISNULL(BCX.FTOrderNoRef,'')='' "
            _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAmt"
            _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
            _Qry &= vbCrLf & "  	FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & ""

            _Qry &= vbCrLf & "  SELECT TRWHD.FTBarcodeNo, TRWHD.FTDocumentNo, TRWHD.FNHSysWHId, TRWHD.FTOrderNo"
            _Qry &= vbCrLf & ", TRWHD.FNQuantity, TH.FTTransferWHNo AS FTDocumentRefNo, TRWHD.FNHSysCmpId, TRWHD.FNPriceTrans, TRWHD.FTStateReserve"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TRWH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS TRWHD  WITH(NOLOCK)  ON TRWH.FTTransferWHNo = TRWHD.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK)   ON A.FTDocumentNo = H.FTIssueNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TH WITH(NOLOCK)   ON A.FTDocumentRefNo = TH.FTTransferWHNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS RETD  WITH(NOLOCK)  ON A.FTBarcodeNo = RETD.FTBarcodeNo AND A.FTOrderNo = RETD.FTOrderNo AND A.FTDocumentNo = RETD.FTDocumentRefNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToStock AS PH WITH(NOLOCK)   ON RETD.FTDocumentNo = PH.FTReturnStockNo ON TRWHD.FTBarcodeNo = RETD.FTBarcodeNo AND TRWHD.FTOrderNo = RETD.FTOrderNo AND "
            _Qry &= vbCrLf & " TRWHD.FTDocumentRefNo = RETD.FTDocumentNo And TRWHD.FNHSysWHId = RETD.FNHSysWHId"

            ' [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS X  WITH(NOLOCK)"
            _Qry &= vbCrLf & " ) AS X"
            _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS XA WITH(NOLOCK) ON  XA.FTTransferWHNo = X.FTDocumentNo"
            _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON XA.FNHSysWHIdTo = XW.FNHSysWHId "
            _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
            _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
            _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
            _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
            _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAfIssAmt"

            _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
            _Qry &= vbCrLf & "  	FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & ""

            _Qry &= vbCrLf & "  SELECT TH.FTTransferWHNo, RETD.FTBarcodeNo, RETD.FTDocumentNo, RETD.FNHSysWHId"
            _Qry &= vbCrLf & "   , RETD.FTOrderNo, RETD.FNQuantity, RETD.FTStateReserve, TH.FTTransferWHNo AS FTDocumentRefNo, RETD.FNHSysCmpId,  RETD.FNPriceTrans"
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A  WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H  WITH(NOLOCK)  ON A.FTDocumentNo = H.FTIssueNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS RETD  WITH(NOLOCK)  ON A.FTBarcodeNo = RETD.FTBarcodeNo AND A.FTOrderNo = RETD.FTOrderNo AND A.FTDocumentNo = RETD.FTDocumentRefNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS PH  WITH(NOLOCK)  ON RETD.FTDocumentNo = PH.FTReturnSuplNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TH WITH(NOLOCK)  ON A.FTDocumentRefNo = TH.FTTransferWHNo"

            _Qry &= vbCrLf & " ) AS X"
            _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS XA WITH(NOLOCK) ON  XA.FTReturnSuplNo = X.FTDocumentNo"
            _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON X.FNHSysWHId = XW.FNHSysWHId "
            _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
            _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
            _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
            _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
            _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAfIssRtsAmt"

            _Qry &= vbCrLf & "   FROM    "
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS C WITH(NOLOCK)  ON W.FNHSysWHId = C.FNHSysWHIdTo INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK)  ON C.FTTransferWHNo = B.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BC WITH(NOLOCK)  ON B.FTBarcodeNo = BC.FTBarcodeNo  INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R  WITH(NOLOCK) ON BC.FTDocumentNo = R.FTReceiveNo"
            _Qry &= vbCrLf & "     INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON BC.FNHSysRawMatId = IM.FNHSysRawMatId "
            _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
            _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo  "
            _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_DummyNo) & "' AND BC.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_DummyNo) & "' AND R.FTReceiveNo='" & HI.UL.ULF.rpQuoted(_RcvNoNo) & "' AND W.FNHSysCmpId=" & _FNHSysCmpID & " AND  (C.FTStateApprove = '1')"
            _Qry &= vbCrLf & "   AND BC.FTOrderNoRef=N'' "
            _Qry &= vbCrLf & "   ) AS M"
            _Qry &= vbCrLf & "    GROUP BY M.FTOrderNo,FNMerMatType"
            _Qry &= vbCrLf & "   ) AS C ON A.FTOrderNo = C.FTOrderNo  AND A.FNMerMatType = C.FNMerMatType"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            For Each R As DataRow In _dt.Rows
                Select Case Integer.Parse(Val(R!FNMerMatType.ToString))
                    Case 0
                        _FNFabricDummyCost = _FNFabricDummyCost + Val(R!FNStockRcvAmt) + Val(R!FNTrwAmt)
                    Case Else
                        _FNAccessroryDummyCost = _FNAccessroryDummyCost + Val(R!FNStockRcvAmt) + Val(R!FNTrwAmt)
                End Select
            Next

        Next

        _Qry = "  SELECT A.FTOrderNo,A.FTReceiveNo,BBB.FTOrderNoRef  AS FTOrderMain "
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
        _Qry &= vbCrLf & "          INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BBB WITH(NOLOCK)"
        _Qry &= vbCrLf & "          ON A.FTReceiveNo=BBB.FTDocumentNo  AND  A.FTOrderNo = BBB.FTOrderNo AND A.FNHSysRawMatId = BBB.FNHSysRawMatId "
        _Qry &= vbCrLf & "    WHERE (O.FNOrderType = 4) AND  ISNULL(BBB.FTOrderNoRef,'') =N'" & HI.UL.ULF.rpQuoted(Key) & "' "
        _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo,A.FTReceiveNo,BBB.FTOrderNoRef "

        _dtdummy = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _DummyNo = ""
        _RcvNoNo = ""
        For Each Rx As DataRow In _dtdummy.Rows

            _DummyNo = Rx!FTOrderNo.ToString
            _RcvNoNo = Rx!FTReceiveNo.ToString

            _Qry = "    Select A.FTOrderNo"
            _Qry &= vbCrLf & "  	, A.FNMerMatType "
            _Qry &= vbCrLf & "    ,ISNULL(B.FNStockRcvAmt,0) AS FNStockRcvAmt"
            _Qry &= vbCrLf & "    ,ISNULL(C.FNTrwAmt,0) AS FNTrwAmt"
            _Qry &= vbCrLf & "   FROM  ( SELECT A.FTOrderNo ,0 AS FNMerMatType"
            _Qry &= vbCrLf & "  	FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  	WHERE  A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_DummyNo) & "'"
            _Qry &= vbCrLf & "     UNION"
            _Qry &= vbCrLf & "  	SELECT A.FTOrderNo ,1 AS FNMerMatType"
            _Qry &= vbCrLf & "  	FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  	WHERE  A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_DummyNo) & "'"
            _Qry &= vbCrLf & "  	 ) AS A"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   (SELECT B.FTOrderNo"
            _Qry &= vbCrLf & "  	,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType"
            _Qry &= vbCrLf & "  	, SUM(Convert(numeric(18,2),BBB.FNQuantity * BBB.FNPrice)) AS FNStockRcvAmt"
            _Qry &= vbCrLf & "   FROM    "
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK) INNER JOIN  "
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS C WITH(NOLOCK)  ON B.FTReceiveNo = C.FTReceiveNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)  ON C.FNHSysWHId = W.FNHSysWHId"
            _Qry &= vbCrLf & "  	   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
            _Qry &= vbCrLf & "          INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BBB WITH(NOLOCK)"
            _Qry &= vbCrLf & "          ON B.FTReceiveNo=BBB.FTDocumentNo  AND  B.FTOrderNo = BBB.FTOrderNo AND B.FNHSysRawMatId = BBB.FNHSysRawMatId "
            _Qry &= vbCrLf & "     INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) ON BBB.FTOrderNoRef  = A.FTOrderNo "
            _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(Key) & "' AND B.FTOrderNo='" & HI.UL.ULF.rpQuoted(_DummyNo) & "'  AND C.FTReceiveNo='" & HI.UL.ULF.rpQuoted(_RcvNoNo) & "' AND W.FNHSysCmpId=" & _FNHSysCmpID & " "
            _Qry &= vbCrLf & "   GROUP BY B.FTOrderNo"
            _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END"
            _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FNMerMatType = B.FNMerMatType"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "   SELECT M.FTOrderNo,FNMerMatType,SUM(FNTrwAmt-(FNTrwRetAmt+FNTrwRetAfIssAmt + FNTrwRetAfIssRtsAmt)) AS FNTrwAmt"
            _Qry &= vbCrLf & "   FROM ("
            _Qry &= vbCrLf & "   SELECT A.FTOrderNo,CASE WHEN ISNULL(MM.FNMerMatType,1) = 0 THEN 0 ELSE 1 END  AS FNMerMatType, (Convert(numeric(18,2),B.FNQuantity * BC.FNPrice)) AS FNTrwAmt"
            _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
            _Qry &= vbCrLf & "  	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS X  WITH(NOLOCK)"
            _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS XA WITH(NOLOCK) ON  XA.FTTransferWHNo = X.FTDocumentNo"
            _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON XA.FNHSysWHIdTo = XW.FNHSysWHId "
            _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
            _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
            _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
            _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
            _Qry &= vbCrLf & "        AND ISNULL(BCX.FTOrderNoRef,'')='" & HI.UL.ULF.rpQuoted(Rx!FTOrderMain.ToString()) & "' "
            _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAmt"

            _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
            _Qry &= vbCrLf & "  	FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & ""

            _Qry &= vbCrLf & "  SELECT TRWHD.FTBarcodeNo, TRWHD.FTDocumentNo, TRWHD.FNHSysWHId, TRWHD.FTOrderNo"
            _Qry &= vbCrLf & ", TRWHD.FNQuantity, TH.FTTransferWHNo AS FTDocumentRefNo, TRWHD.FNHSysCmpId, TRWHD.FNPriceTrans, TRWHD.FTStateReserve"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TRWH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS TRWHD  WITH(NOLOCK)  ON TRWH.FTTransferWHNo = TRWHD.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK)   ON A.FTDocumentNo = H.FTIssueNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TH WITH(NOLOCK)   ON A.FTDocumentRefNo = TH.FTTransferWHNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS RETD  WITH(NOLOCK)  ON A.FTBarcodeNo = RETD.FTBarcodeNo AND A.FTOrderNo = RETD.FTOrderNo AND A.FTDocumentNo = RETD.FTDocumentRefNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToStock AS PH WITH(NOLOCK)   ON RETD.FTDocumentNo = PH.FTReturnStockNo ON TRWHD.FTBarcodeNo = RETD.FTBarcodeNo AND TRWHD.FTOrderNo = RETD.FTOrderNo AND "
            _Qry &= vbCrLf & " TRWHD.FTDocumentRefNo = RETD.FTDocumentNo And TRWHD.FNHSysWHId = RETD.FNHSysWHId"

            ' [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS X  WITH(NOLOCK)"
            _Qry &= vbCrLf & " ) AS X"
            _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS XA WITH(NOLOCK) ON  XA.FTTransferWHNo = X.FTDocumentNo"
            _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON XA.FNHSysWHIdTo = XW.FNHSysWHId "
            _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
            _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
            _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
            _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
            _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAfIssAmt"

            _Qry &= vbCrLf & "  	,ISNULL((SELECT SUM ((Convert(numeric(18,2),X.FNQuantity * BCX.FNPrice)))"
            _Qry &= vbCrLf & "  	FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & ""

            _Qry &= vbCrLf & "  SELECT TH.FTTransferWHNo, RETD.FTBarcodeNo, RETD.FTDocumentNo, RETD.FNHSysWHId"
            _Qry &= vbCrLf & "   , RETD.FTOrderNo, RETD.FNQuantity, RETD.FTStateReserve, TH.FTTransferWHNo AS FTDocumentRefNo, RETD.FNHSysCmpId,  RETD.FNPriceTrans"
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A  WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H  WITH(NOLOCK)  ON A.FTDocumentNo = H.FTIssueNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS RETD  WITH(NOLOCK)  ON A.FTBarcodeNo = RETD.FTBarcodeNo AND A.FTOrderNo = RETD.FTOrderNo AND A.FTDocumentNo = RETD.FTDocumentRefNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS PH  WITH(NOLOCK)  ON RETD.FTDocumentNo = PH.FTReturnSuplNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS TH WITH(NOLOCK)  ON A.FTDocumentRefNo = TH.FTTransferWHNo"

            _Qry &= vbCrLf & " ) AS X"
            _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS XA WITH(NOLOCK) ON  XA.FTReturnSuplNo = X.FTDocumentNo"
            _Qry &= vbCrLf & "  	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS XW  WITH(NOLOCK) ON X.FNHSysWHId = XW.FNHSysWHId "
            _Qry &= vbCrLf & "      INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BCX WITH(NOLOCK)  ON X.FTBarcodeNo = BCX.FTBarcodeNo"
            _Qry &= vbCrLf & "   WHERE X.FTDocumentRefNo = C.FTTransferWHNo"
            _Qry &= vbCrLf & "     AND X.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & "  	  AND X.FTOrderNo = A.FTOrderNo "
            _Qry &= vbCrLf & "  	  AND XW.FNHSysCmpId <>A.FNHSysCmpId"
            _Qry &= vbCrLf & "  	 ),0) AS FNTrwRetAfIssRtsAmt"

            _Qry &= vbCrLf & "   FROM    "
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS C WITH(NOLOCK)  ON W.FNHSysWHId = C.FNHSysWHIdTo INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK)  ON C.FTTransferWHNo = B.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BC WITH(NOLOCK)  ON B.FTBarcodeNo = BC.FTBarcodeNo  INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R  WITH(NOLOCK) ON B.FTDocumentRefNo = R.FTReceiveNo"
            _Qry &= vbCrLf & "     INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON BC.FNHSysRawMatId = IM.FNHSysRawMatId "
            _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
            _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo  "
            _Qry &= vbCrLf & "   WHERE A.FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_DummyNo) & "' AND R.FTReceiveNo='" & HI.UL.ULF.rpQuoted(_RcvNoNo) & "' AND W.FNHSysCmpId=" & _FNHSysCmpID & " AND  (C.FTStateApprove = '1')"
            _Qry &= vbCrLf & "         AND BC.FTOrderNoRef=N'" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "   ) AS M"
            _Qry &= vbCrLf & "    GROUP BY M.FTOrderNo,FNMerMatType"
            _Qry &= vbCrLf & "   ) AS C ON A.FTOrderNo = C.FTOrderNo  AND A.FNMerMatType = C.FNMerMatType"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            For Each R As DataRow In _dt.Rows
                Select Case Integer.Parse(Val(R!FNMerMatType.ToString))
                    Case 0
                        _FNFabricDummyCost = _FNFabricDummyCost + Val(R!FNStockRcvAmt) + Val(R!FNTrwAmt)
                    Case Else
                        _FNAccessroryDummyCost = _FNAccessroryDummyCost + Val(R!FNStockRcvAmt) + Val(R!FNTrwAmt)
                End Select
            Next


        Next


        _Qry = "  SELECT CASE WHEN W.FNHSysCmpId  = " & _FNHSysCmpID & " THEN '0' ELSE '1' END  AS FTStateWH"
        _Qry &= vbCrLf & "   ,B.FTBarcodeNo ,SUM(Convert(numeric(18,2),D.FNQuantity * B.FNPrice)) "
        _Qry &= vbCrLf & "     - ISNULL((SELECT SUM(Convert(numeric(18,2),D.FNQuantity * B.FNPrice)) AS RtsAmt"
        _Qry &= vbCrLf & "   	  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS HD WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "   	   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS DD WITH(NOLOCK) ON HD.FTReturnSuplNo=DD.FTDocumentNo  INNER JOIN"
        _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BD WITH(NOLOCK) ON DD.FTBarcodeNo = BD.FTBarcodeNo"
        _Qry &= vbCrLf & "    WHERE(DD.FTOrderNo =N'" & HI.UL.ULF.rpQuoted(Key) & "') AND DD.FTBarcodeNo =B.FTBarcodeNo "
        _Qry &= vbCrLf & "   	),0"
        _Qry &= vbCrLf & "   ) AS FNAmt"
        _Qry &= vbCrLf & "  ,CASE WHEN  ISNULL(H.FTStateAutoFromCenter,'')='1' THEN '1' ELSE '0' END AS FTState"
        _Qry &= vbCrLf & "  	, MM.FNMerMatType "
        _Qry &= vbCrLf & "    FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS H WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS D WITH(NOLOCK) ON H.FTReserveNo=D.FTDocumentNo AND H.FNHSysWHId = D.FNHSysWHId INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON D.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK) ON H.FNHSysWHId = W.FNHSysWHId"
        _Qry &= vbCrLf & "            INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        _Qry &= vbCrLf & "    WHERE  (H.FTOrderNo =N'" & HI.UL.ULF.rpQuoted(Key) & "')"
        _Qry &= vbCrLf & "    GROUP BY B.FTBarcodeNo ,CASE WHEN W.FNHSysCmpId  = " & _FNHSysCmpID & " THEN '0' ELSE '1' END  "
        _Qry &= vbCrLf & "  ,CASE WHEN  ISNULL(H.FTStateAutoFromCenter,'')='1' THEN '1' ELSE '0' END "
        _Qry &= vbCrLf & "  	, MM.FNMerMatType "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        For Each R As DataRow In _dt.Select("FTState='0'")
            Select Case Integer.Parse(Val(R!FTStateWH.ToString))
                Case 0
                    _FNAccFabStockRcvCost = _FNAccFabStockRcvCost + Val(R!FNAmt)
                Case Else
                    _FNAccFabStockOtherRcvCost = _FNAccFabStockOtherRcvCost + Val(R!FNAmt)
            End Select
        Next

        For Each R As DataRow In _dt.Select("FTState='1'")
            Select Case Integer.Parse(Val(R!FNMerMatType.ToString))
                Case 0
                    _FNFabricRcvCost = _FNFabricRcvCost + Val(R!FNAmt)
                Case Else
                    _FNAccessroryRcvCost = _FNAccessroryRcvCost + Val(R!FNAmt)
            End Select
        Next

        _Qry = "  SELECT CASE WHEN W.FNHSysCmpId  = " & _FNHSysCmpID & " THEN '0' ELSE '1' END  AS FTStateWH"
        _Qry &= vbCrLf & "   ,B.FTBarcodeNo ,SUM(Convert(numeric(18,2),D.FNQuantity * B.FNPrice))  AS FNAmt"
        _Qry &= vbCrLf & "  	, MM.FNMerMatType "
        _Qry &= vbCrLf & "    FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS H WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS D WITH(NOLOCK) ON H.FTReserveNo=D.FTDocumentNo AND H.FNHSysWHId = D.FNHSysWHId INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON D.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK) ON H.FNHSysWHId = W.FNHSysWHId"
        _Qry &= vbCrLf & "            INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        _Qry &= vbCrLf & "    WHERE  (D.FTOrderNo =N'" & HI.UL.ULF.rpQuoted(Key) & "') AND ISNULL(H.FTStateAutoFromCenter,'')='1'"
        _Qry &= vbCrLf & "    GROUP BY B.FTBarcodeNo ,CASE WHEN W.FNHSysCmpId  = " & _FNHSysCmpID & " THEN '0' ELSE '1' END  "
        _Qry &= vbCrLf & "  	, MM.FNMerMatType "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        For Each R As DataRow In _dt.Rows
            Select Case Integer.Parse(Val(R!FNMerMatType.ToString))
                Case 0
                    _FNFabricRcvCost = _FNFabricRcvCost - Val(R!FNAmt)
                Case Else
                    _FNAccessroryRcvCost = _FNAccessroryRcvCost - Val(R!FNAmt)
            End Select
        Next

        _FNFabricBalCost = (_FNFabricRcvCost + _FNFabricDummyCost) - FNFabricCost.Value
        _FNAccessroryBalCost = (_FNAccessroryRcvCost + _FNAccessroryDummyCost) - FNAccessroryCost.Value

        FNFabricRcvCost.Value = _FNFabricRcvCost
        FNAccessroryRcvCost.Value = _FNAccessroryRcvCost
        FNFabricDummyCost.Value = _FNFabricDummyCost
        FNAccessroryDummyCost.Value = _FNAccessroryDummyCost
        FNFabricBalCost.Value = _FNFabricBalCost
        FNAccessroryBalCost.Value = _FNAccessroryBalCost

        FNFabricSum.Value = _FNFabricRcvCost + _FNFabricDummyCost
        FNAccessrorySum.Value = _FNAccessroryRcvCost + _FNAccessroryDummyCost


        _FNAccFabStockBalCost = _FNAccFabStockRcvCost - FNAccFabStockCost.Value
        _FNAccFabStockOtherBalCost = _FNAccFabStockOtherRcvCost - FNAccFabStockOtherCost.Value
        FNAccFabStockRcvCost.Value = _FNAccFabStockRcvCost
        FNAccFabStockOtherRcvCost.Value = _FNAccFabStockOtherRcvCost
        FNAccFabStockBalCost.Value = _FNAccFabStockBalCost
        FNAccFabStockOtherBalCost.Value = _FNAccFabStockOtherBalCost

    End Sub

    Private Sub LoadDetailOrderCostRawmatDetail(Key As String)
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = " SELECT     LTT.FNListIndex  AS FNMerMatType"
        _Qry &= vbCrLf & "  ,M.FNHSysRawMatId"
        _Qry &= vbCrLf & "  ,IM.FTRawMatCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " 	,LTT.FTNameTH  AS FTMerMatTypeName"
            _Qry &= vbCrLf & " 	,IM.FTRawMatNameTH  AS FTRawMatName"
        Else
            _Qry &= vbCrLf & " 	, LTT.FTNameEN AS FTMerMatTypeName "
            _Qry &= vbCrLf & " 	,IM.FTRawMatNameEN  AS FTRawMatName"
        End If

        _Qry &= vbCrLf & "  ,ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode "
        _Qry &= vbCrLf & "  ,ISNULL(IMS.FTRawMatSizeCode ,'') AS FTRawMatSizeCode"
        _Qry &= vbCrLf & " 	, M.FNUsedQuantity"
        _Qry &= vbCrLf & " 	, M.FNHSysUnitId"
        _Qry &= vbCrLf & " 	,ISNULL(IMU.FTUnitCode ,'') AS FTUnitCode"
        _Qry &= vbCrLf & " 	,ISNULL(IMU2.FTUnitCode ,'') AS FTUnitStockCode"
        _Qry &= vbCrLf & " 	, M.FNPrice"
        _Qry &= vbCrLf & " 	, M.FNQuantity"
        _Qry &= vbCrLf & " 	, M.FNQuantityRet"
        _Qry &= vbCrLf & " 	, M.FNQuantityRts"
        _Qry &= vbCrLf & " 	,(M.FNQuantity - (M.FNQuantityRet + M.FNQuantityRts)) AS FNActualUsed"
        _Qry &= vbCrLf & " 	, M.FNAmount"
        _Qry &= vbCrLf & " 	, M.FNINVChargeAmt"
        _Qry &= vbCrLf & " 	,(M.FNAmount + M.FNINVChargeAmt ) FNNetAmount"
        _Qry &= vbCrLf & "  FROM     (SELECT A4.FNHSysRawMatId, R.FNUsedQuantity, R.FNHSysUnitId, A4.FNPrice, SUM(A4.FNQuantity) AS FNQuantity, SUM(A4.FNQuantityRet) AS FNQuantityRet, SUM(A4.FNQuantityRts) AS FNQuantityRts, "
        _Qry &= vbCrLf & "    SUM(A4.FNAmount) AS FNAmount, SUM(A4.FNINVChargeAmt) AS FNINVChargeAmt"
        _Qry &= vbCrLf & "    FROM      (SELECT FNHSysRawMatId, SUM(FNUsedQuantity + FNUsedPlusQuantity) AS FNUsedQuantity, MAX(FNHSysUnitId) AS FNHSysUnitId"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource"
        _Qry &= vbCrLf & "   WHERE   (FTOrderNo =N'" & HI.UL.ULF.rpQuoted(Key) & "')"
        _Qry &= vbCrLf & "    GROUP BY FNHSysRawMatId) AS R RIGHT OUTER JOIN"
        _Qry &= vbCrLf & "        (SELECT FNHSysRawMatId, FNPrice, FNQuantity, FNQuantityRet, FNQuantityRts, FNAmount, FTStateRsv, FTInvoiceNo, FTReceiveNo, "
        _Qry &= vbCrLf & "         CASE WHEN FTInvoiceNo = '' THEN 0.00 ELSE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.FN_GET_Invoice_Charge_Amt(FTInvoiceNo, FTReceiveNo, FNHSysRawMatId, FNAmount) END AS FNINVChargeAmt"
        _Qry &= vbCrLf & "     FROM      (SELECT FNHSysRawMatId, FNPrice, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityRet) AS FNQuantityRet, SUM(FNQuantityRts) AS FNQuantityRts, SUM(FNAmount) AS FNAmount, FTStateRsv, "
        _Qry &= vbCrLf & " FTInvoiceNo, FTReceiveNo"
        _Qry &= vbCrLf & "      FROM      (SELECT FTIssueNo, FNHSysRawMatId, FNPrice, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityRet) AS FNQuantityRet, SUM(FNQuantityRts) AS FNQuantityRts, CONVERT(numeric(18, "
        _Qry &= vbCrLf & "       2), SUM(FNQuantity - (FNQuantityRet + FNQuantityRts)) * FNPrice) AS FNAmount, FTStateRsv, FTInvoiceNo, FTReceiveNo"
        _Qry &= vbCrLf & "     FROM      (SELECT H.FTIssueNo, BC.FNHSysRawMatId, H.FTOrderNo, B.FTBarcodeNo, BC.FNPrice, B.FTDocumentNo, B.FNHSysWHId, B.FNQuantity, ISNULL"
        _Qry &= vbCrLf & "    ((SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH (NOLOCK)"
        _Qry &= vbCrLf & "     WHERE   (FTDocumentRefNo = B.FTDocumentNo) AND (FTBarcodeNo = B.FTBarcodeNo)), 0) AS FNQuantityRet, ISNULL"
        _Qry &= vbCrLf & "    ((SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "   FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS BI WITH (NOLOCK)"
        _Qry &= vbCrLf & "   WHERE   (FTDocumentRefNo = B.FTDocumentNo) AND (FTBarcodeNo = B.FTBarcodeNo)), 0) AS FNQuantityRts, CASE WHEN ISNULL(RSV.FTReserveNo, '') "
        _Qry &= vbCrLf & "  = '' THEN '0' ELSE '1' END AS FTStateRsv, ISNULL(R.FTInvoiceNo, N'') AS FTInvoiceNo, ISNULL(R.FTReceiveNo, N'') AS FTReceiveNo"
        _Qry &= vbCrLf & "    FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH (NOLOCK) ON H.FTIssueNo = B.FTDocumentNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS RSV WITH (NOLOCK) ON B.FTDocumentRefNo = RSV.FTReserveNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BC WITH (NOLOCK) ON B.FTBarcodeNo = BC.FTBarcodeNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON B.FTDocumentNo = R.FTReceiveNo"
        _Qry &= vbCrLf & "  WHERE   (H.FTOrderNo =N'" & HI.UL.ULF.rpQuoted(Key) & "')) AS A"
        _Qry &= vbCrLf & "  GROUP BY FTIssueNo, FNHSysRawMatId, FNPrice, FTStateRsv, FTInvoiceNo, FTReceiveNo) AS A2"
        _Qry &= vbCrLf & "    GROUP BY FNHSysRawMatId, FNPrice, FTStateRsv, FTInvoiceNo, FTReceiveNo) AS A3) AS A4 ON R.FNHSysRawMatId = A4.FNHSysRawMatId"
        _Qry &= vbCrLf & "    GROUP BY A4.FNHSysRawMatId, R.FNUsedQuantity, R.FNHSysUnitId, A4.FNPrice) AS M INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH (NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   (SELECT FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & "     FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
        _Qry &= vbCrLf & "    WHERE   (FTListName = N'FNMerMatType')) AS LTT ON ISNULL(MM.FNMerMatType, 1) = LTT.FNListIndex"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH (NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH (NOLOCK) ON IM.FNHSysRawMatSizeId  = IMS.FNHSysRawMatSizeId "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS IMU WITH (NOLOCK) ON M.FNHSysUnitId  = IMU.FNHSysUnitId "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS IMU2 WITH (NOLOCK) ON IM.FNHSysUnitId  = IMU2.FNHSysUnitId "
        _Qry &= vbCrLf & "    Order By LTT.FNListIndex,IM.FTRawMatCode,ISNULL(IMC.FTRawMatColorCode,'') ,ISNULL(IMS.FNRawMatSizeSeq,0)"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        Me.ogcdetail.DataSource = _dt.Copy

        ogvdetail.ExpandAllGroups()
        ogvdetail.RefreshData()

    End Sub

    Private Sub CalculateConducted()
        Dim _TotalExportAmt As Double = 0
        _TotalExportAmt = FNExportAmt.Value

        If _TotalExportAmt > 0 Then

            If FNConductedCostPer.Value > 0 Then
                FNConductedCost.Value = Double.Parse(Format((_TotalExportAmt * FNConductedCostPer.Value) / 100.0, "0.00"))
            Else
                FNConductedCost.Value = 0
            End If

        Else
            FNConductedCost.Value = 0
        End If


    End Sub

    Private Sub CalculateCommission()

        Dim _TotalExportAmt As Double = 0
        _TotalExportAmt = FNExportAmt.Value

        If _TotalExportAmt > 0 Then

            If FNCommissionCostPer.Value > 0 Then
                FNCommissionCost.Value = Double.Parse(Format((_TotalExportAmt * FNCommissionCostPer.Value) / 100.0, "0.00"))
            Else
                FNCommissionCost.Value = 0
            End If

        Else
            FNCommissionCost.Value = 0
        End If

    End Sub

    Private Sub CalculatePer()
        Static _Proc As Boolean
        If Not (_Proc) Then
            _Proc = True




            Dim _TotalExportAmt As Double = 0
            _TotalExportAmt = FNExportAmt.Value
            Call CalculateConducted()
            Call CalculateCommission()

            FNWageCutSew_Per.Text = "-"
            FNWageCutTrim_Per.Text = "-"
            FNFabricCost_Per.Text = "-"
            FNAccessroryCost_Per.Text = "-"
            FNAccFabStockCost_Per.Text = "-"
            FNExportNetAmt_Per.Text = "-"
            FNEmbSubCost_Per.Text = "-"
            FNPrintSubCost_Per.Text = "-"
            FNEmbBranchCost_Per.Text = "-"
            FNPrintBranchCost_Per.Text = "-"
            FNEmbFacCost_Per.Text = "-"
            FNWageCost_Per.Text = "-"
            FNProdCost_Per.Text = "-"
            FNConductedCost_per.Text = "-"
            FNCommissionCost_Per.Text = "-"
            FNImportCost_Per.Text = "-"
            FNExportCost_Per.Text = "-"
            FNTransportCost_Per.Text = "-"
            FNTransportAirCost_Per.Text = "-"
            FNOtherCost_Per.Text = "-"
            FNNetCost_Per.Text = "-"
            FNNetProfit_Per.Text = "-"
            FNProfitPerPiece_Per.Text = "-"
            FNServiceCost_Per.Text = "-"
            FNAccFabStockOtherCost_Per.Text = "-"
            FNNetProfit.Value = 0
            FNProfitPerPiece.Value = 0
            FNNetProfitRcv.Value = 0
            FNProfitPerPieceRcv.Value = 0
            FNWagePull_Per.Text = "-"

            Dim _FNNetCost As Double = 0

            If FNExportAmt.Value > 0 Then
                FNExportNetAmt.Value = FNExportAmt.Value - (FNFabricCost.Value + FNAccessroryCost.Value + FNAccFabStockCost.Value + FNAccFabStockOtherCost.Value + FNServiceCost.Value + FNWagePull.Value)
                FNExportNetCostAmt.Value = FNExportAmt.Value - (FNFabricSum.Value + FNAccessrorySum.Value + FNAccFabStockRcvCost.Value + FNAccFabStockOtherRcvCost.Value + +FNServiceCost.Value + FNWagePull.Value)
            End If

            _FNNetCost = FNEmbSubCost.Value + FNPrintSubCost.Value + FNEmbBranchCost.Value + FNPrintBranchCost.Value + FNEmbFacCost.Value + FNWageCost.Value + FNProdCost.Value + FNConductedCost.Value + FNCommissionCost.Value + FNImportCost.Value + FNExportCost.Value + FNTransportCost.Value + FNTransportAirCost.Value + FNOtherCost.Value + FNWageCutSew.Value + FNWageCutTrim.Value
            FNNetCost.Value = _FNNetCost

            If _TotalExportAmt > 0 Then

                FNNetProfit.Value = (FNExportNetAmt.Value - FNNetCost.Value)
                FNProfitPerPiece.Value = Val(Format((FNNetProfit.Value / FNExportQty.Value), "0.00"))

                FNNetProfitRcv.Value = (FNExportNetCostAmt.Value - FNNetCost.Value)
                FNProfitPerPieceRcv.Value = Val(Format((FNNetProfitRcv.Value / FNExportQty.Value), "0.00"))

                If FNFabricCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNFabricCost_Per.Text = Format(((FNFabricCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNAccessroryCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNAccessroryCost_Per.Text = Format(((FNAccessroryCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNAccFabStockCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNAccFabStockCost_Per.Text = Format(((FNAccFabStockCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNAccFabStockOtherCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNAccFabStockOtherCost_Per.Text = Format(((FNAccFabStockOtherCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNServiceCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNServiceCost_Per.Text = Format(((FNServiceCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNWagePull.Value > 0 And _TotalExportAmt > 0 Then
                    FNWagePull_Per.Text = Format(((FNWagePull.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNExportNetAmt.Value > 0 And _TotalExportAmt > 0 Then
                    FNExportNetAmt_Per.Text = Format(((FNExportNetAmt.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNEmbSubCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNEmbSubCost_Per.Text = Format(((FNEmbSubCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNPrintSubCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNPrintSubCost_Per.Text = Format(((FNPrintSubCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNEmbBranchCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNEmbBranchCost_Per.Text = Format(((FNEmbBranchCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNPrintBranchCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNPrintBranchCost_Per.Text = Format(((FNPrintBranchCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNEmbFacCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNEmbFacCost_Per.Text = Format(((FNEmbFacCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNWageCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNWageCost_Per.Text = Format(((FNWageCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNProdCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNProdCost_Per.Text = Format(((FNProdCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNConductedCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNConductedCost_per.Text = Format(((FNConductedCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNCommissionCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNCommissionCost_Per.Text = Format(((FNCommissionCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNImportCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNImportCost_Per.Text = Format(((FNImportCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNExportCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNExportCost_Per.Text = Format(((FNExportCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNTransportCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNTransportCost_Per.Text = Format(((FNTransportCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNWageCutSew.Value > 0 And _TotalExportAmt > 0 Then
                    FNWageCutSew_Per.Text = Format(((FNWageCutSew.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNWageCutTrim.Value > 0 And _TotalExportAmt > 0 Then
                    FNWageCutTrim_Per.Text = Format(((FNWageCutTrim.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNTransportAirCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNTransportAirCost_Per.Text = Format(((FNTransportAirCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNOtherCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNOtherCost_Per.Text = Format(((FNOtherCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNNetCost.Value > 0 And _TotalExportAmt > 0 Then
                    FNNetCost_Per.Text = Format(((FNNetCost.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNNetProfit.Value > 0 And _TotalExportAmt > 0 Then
                    FNNetProfit_Per.Text = Format(((FNNetProfit.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNProfitPerPiece.Value > 0 And _TotalExportAmt > 0 Then
                    FNProfitPerPiece_Per.Text = Format(((FNProfitPerPiece.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNNetProfitRcv.Value > 0 And _TotalExportAmt > 0 Then
                    FNNetProfitRcv_Per.Text = Format(((FNNetProfitRcv.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

                If FNProfitPerPieceRcv.Value > 0 And _TotalExportAmt > 0 Then
                    FNProfitPerPieceRcv_Per.Text = Format(((FNProfitPerPieceRcv.Value / _TotalExportAmt) * 100.0), "0.00")
                End If

            End If
            _Proc = False
        End If
    End Sub

    Private Sub CalculateExport()
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()

            _dt = .Copy
        End With

        Dim _QtyExport As Integer = 0
        Dim _AmtExport As Double = 0

        For Each R As DataRow In _dt.Select("FTInvoiceNo<>'' AND FDInvoiceDate<>'' ")
            _QtyExport = _QtyExport + Val(R!FNExportQuantity.ToString)
            _AmtExport = _AmtExport + Val(R!FNExportAmtTHB.ToString)
        Next

        FNExportGAmt.Value = _AmtExport

        Try
            Me.FNExportAmt.Value = (Me.FNExportGAmt.Value + FNDebitAmt.Value)
        Catch ex As Exception
            FNExportAmt.Value = _AmtExport
        End Try

        FNExportQty.Value = _QtyExport

    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (_FormLoad) Then Exit Sub
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            If FTOrderNo.Text <> "" Then
                otbexport.SelectedTabPageIndex = 0
                If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  AND FTStateOrderApp='1'  AND  (FNOrderType =22)  AND FTOrderNoRef=''  AND FNJobState NOT IN(2,3) ", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                    Call LoadOrderCostDataInfo(FTOrderNo.Text)
                Else

                    'Me.ogv.Columns.ColumnByFieldName("FTOrderNoRef").Visible = False
                    'Me.ogv.Columns.ColumnByFieldName("FDShipDate").Visible = False
                    'Me.ogv.Columns.ColumnByFieldName("FTCustomerPO").Visible = False

                    HI.TL.HandlerControl.ClearControl(ogbinvoice)
                    HI.TL.HandlerControl.ClearControl(ogbdetail)
                    HI.TL.HandlerControl.ClearControl(ogbdetail2)
                    HI.TL.HandlerControl.ClearControl(ogbdetail3)

                End If
            Else

                'Me.ogv.Columns.ColumnByFieldName("FTOrderNoRef").Visible = False
                'Me.ogv.Columns.ColumnByFieldName("FDShipDate").Visible = False
                'Me.ogv.Columns.ColumnByFieldName("FTCustomerPO").Visible = False

                HI.TL.HandlerControl.ClearControl(ogbinvoice)
                HI.TL.HandlerControl.ClearControl(ogbdetail)
                HI.TL.HandlerControl.ClearControl(ogbdetail2)
                HI.TL.HandlerControl.ClearControl(ogbdetail3)

                otbexport.SelectedTabPageIndex = 0

            End If

        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        HI.TL.HandlerControl.ClearControl(ogbheader)
        HI.TL.HandlerControl.ClearControl(ogbinvoice)
        HI.TL.HandlerControl.ClearControl(ogbdetail)
        HI.TL.HandlerControl.ClearControl(ogbdetail2)
        HI.TL.HandlerControl.ClearControl(ogbdetail3)
        otbexport.SelectedTabPageIndex = 0
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Me.FTOrderNo.Text <> "" Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.FTOrderNo.Text) = True Then

                If Me.DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Me.FTOrderNo.Text = ""

                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTOrderNo_lbl.Text)
            Me.FTOrderNo.Focus()
        End If
    End Sub

    Private Sub FNExportAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNExportAmt.EditValueChanged, FNFabricCost.EditValueChanged, FNAccessroryCost.EditValueChanged, FNAccFabStockCost.EditValueChanged, FNExportNetAmt.EditValueChanged, FNEmbSubCost.EditValueChanged, FNPrintSubCost.EditValueChanged,
                  FNEmbBranchCost.EditValueChanged, FNPrintBranchCost.EditValueChanged, FNEmbFacCost.EditValueChanged, FNWageCost.EditValueChanged, FNProdCost.EditValueChanged, FNConductedCost.EditValueChanged, FNCommissionCost.EditValueChanged, FNImportCost.EditValueChanged, FNExportCost.EditValueChanged,
                  FNAccFabStockOtherCost.EditValueChanged, FNTransportCost.EditValueChanged, FNTransportAirCost.EditValueChanged, FNOtherCost.EditValueChanged, FNServiceCost.EditValueChanged, FNWageCutSew.EditValueChanged, FNWageCutTrim.EditValueChanged, FNWagePull.EditValueChanged

        Call CalculatePer()
    End Sub

    Private Sub ogv_HiddenEditor(sender As Object, e As EventArgs) Handles ogv.HiddenEditor
        Try
            If ogv.FocusedColumn.FieldName.ToString = "FTInvoiceNo" Then
                Try
                    Try
                        If Not (Me.ogc.DataSource Is Nothing) Then
                            Dim dtm As DataTable
                            Dim dtinv As DataTable

                            ' Me.ogv.SetFocusedRowCellValue("FTInvoiceNo", e.NewValue)

                            With CType(Me.ogc.DataSource, DataTable)
                                .AcceptChanges()
                                dtm = .Copy

                            End With

                            If Me.ogcinvoicecharge.DataSource Is Nothing Then
                                Call LoadDetailOrderInvoicecharge(Me.FTOrderNo.Text)
                            End If

                            With CType(Me.ogcinvoicecharge.DataSource, DataTable)
                                .AcceptChanges()
                                dtinv = .Copy

                            End With

                            dtinv.BeginInit()
                            For Each R As DataRow In dtinv.Select("FTInvoiceNo<>''")
                                If dtm.Select("FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'").Length <= 0 Then
                                    R.Delete()
                                End If
                            Next
                            dtinv.EndInit()

                            For Each R As DataRow In dtm.Select("FTInvoiceNo<>''")
                                If dtinv.Select("FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'").Length <= 0 Then
                                    dtinv.Rows.Add(R!FTInvoiceNo.ToString, 0, 0, 0, 0, 0)
                                End If
                            Next

                            Me.ogcinvoicecharge.DataSource = dtinv.Copy
                        End If
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogv.KeyDown
        Try

            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Down
                    With CType(Me.ogc.DataSource, DataTable)

                        .AcceptChanges()

                        If .Select("FTInvoiceNo='' OR FDInvoiceDate='' ").Length <= 0 Then

                            Call InitInvoice()

                            .AcceptChanges()

                            Me.ogv.ClearSelection()
                            Me.ogv.SelectRow(.Rows.Count - 1)
                            Me.ogv.FocusedRowHandle = .Rows.Count - 1
                            Me.ogv.FocusedColumn = Me.ogv.Columns.ColumnByFieldName("FTInvoiceNo")

                        End If

                    End With
                Case System.Windows.Forms.Keys.Delete
                    Me.ogv.DeleteRow(Me.ogv.FocusedRowHandle)
                    With CType(Me.ogc.DataSource, DataTable)
                        .AcceptChanges()
                    End With
                    CalculateExport()
                Case System.Windows.Forms.Keys.Enter

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepExportQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepExportQuantity.EditValueChanging, ReposQuantity.EditValueChanging, ReposPrice.EditValueChanging
        Try
            Dim _ExportQuantity As Integer = 0
            Dim _ExportPrice As Double = 0
            Dim _ExportEnchangeRate As Double = 0
            Dim _ExportAmt As Double = 0
            Dim _ExportAmtTHB As Double = 0

            If e.NewValue < 0 Then
                e.Cancel = True
                Exit Sub
            Else

                With Me.ogv

                    If .GetFocusedRowCellValue("FTInvoiceNo").ToString = "" Or .GetFocusedRowCellValue("FDInvoiceDate").ToString = "" Then
                        e.Cancel = True
                    Else
                        Select Case .FocusedColumn.FieldName.ToString.ToLower()
                            Case "FNExportQuantity".ToLower
                                If IsNumeric(e.NewValue) Then
                                    _ExportQuantity = e.NewValue
                                End If


                                .SetFocusedRowCellValue("FNExportQuantity", _ExportQuantity)
                            Case "FNPrice".ToLower
                                If IsNumeric(e.NewValue) Then
                                    _ExportPrice = e.NewValue
                                End If

                                .SetFocusedRowCellValue("FNPrice", _ExportPrice)

                            Case "FNExchangeRate".ToLower

                                If e.NewValue <= 0 Then
                                    e.Cancel = True
                                    Exit Sub
                                Else
                                    If IsNumeric(e.NewValue) Then
                                        _ExportEnchangeRate = e.NewValue

                                        .SetFocusedRowCellValue("FNExchangeRate", _ExportEnchangeRate)

                                    End If
                                End If
                            Case Else

                        End Select
                    End If

                    _ExportQuantity = Val(.GetFocusedRowCellValue("FNExportQuantity").ToString)
                    _ExportPrice = Val(.GetFocusedRowCellValue("FNPrice").ToString)
                    _ExportEnchangeRate = Val(.GetFocusedRowCellValue("FNExchangeRate").ToString)

                    _ExportAmt = CDbl(Format(_ExportQuantity * _ExportPrice, "0.00"))
                    _ExportAmtTHB = CDbl(Format(_ExportAmt * _ExportEnchangeRate, "0.00"))

                    .SetFocusedRowCellValue("FNExportAmt", _ExportAmt)
                    .SetFocusedRowCellValue("FNExportAmtTHB", _ExportAmtTHB)

                End With

                With CType(Me.ogc.DataSource, DataTable)
                    .AcceptChanges()
                End With

                Call CalculateExport()

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepExportQuantity_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepExportQuantity.Spin, ReposQuantity.Spin, ReposAmt.Spin, ReposFTInvoiceDate.Spin, ReposPrice.Spin
        e.Handled = True
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.FTOrderNo.Text <> "" Then
            _StateSaveJobType = 0
            Dim _Qry As String = ""
            Dim _FTOrderNoRef As String
            _Qry = " SELECT TOP 1 ISNULL(FTOrderNoRef,'') AS FTOrderNoRef "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
            _FTOrderNoRef = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

            If _FTOrderNoRef <> "" Then
                HI.MG.ShowMsg.mInfo("Job Order นี้ เป็น Job ลูก ของ Job Booking กรุณาทำการบันทึกที่ Job Booking !!!", 150609178795, Me.Text, _FTOrderNoRef, System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Not (ogc.DataSource Is Nothing) Then

                If Me.ogv.Columns.ColumnByFieldName("FTOrderNoRef").Visible Then
                    _StateSaveJobType = 1
                    With CType(ogc.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("(FTInvoiceNo<>'' AND FDInvoiceDate='') OR (FTInvoiceNo<>'' AND FTOrderNoRef='') ").Length > 0 Then
                            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่ Invoice Date ให้ครบ !!!", 1506020078, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                            Exit Sub
                        End If


                    End With

                Else
                    _StateSaveJobType = 0
                    With CType(ogc.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTInvoiceNo<>'' AND FDInvoiceDate=''").Length > 0 Then
                            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่ Invoice Date ให้ครบ !!!", 1506020078, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End With

                End If

            End If

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.FTOrderNo.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Saving....Data,Please Wait.....")

                If Me.SaveData() Then
                    _Spls.UpdateInformation("Calculeting...,Please wait...")
                    Call CalculateCostData(Me.FTOrderNo.Text)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTOrderNo_lbl.Text)
            Me.FTOrderNo.Focus()
        End If
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click


        'Dim _Qry As String = ""
        'Dim _dt As DataTable

        '_Qry = " SELECT FTOrderNo ,ISNULL(("
        '_Qry &= vbCrLf & "  	SELECT SUM(FNQuantity) AS FNGrandQuantity"
        '_Qry &= vbCrLf & " 	FROM   HITECH_MERCHAN.dbo.TMERTOrderSub_BreakDown AS Sub WITH(NOLOCK)"
        '_Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = A.FTOrderNo "
        '_Qry &= vbCrLf & "  	),0) AS FNGrandQuantity"
        '_Qry &= vbCrLf & " FROM       HITECH_ACCOUNT.dbo.TACCTJobCost AS A"
        '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        'Dim _Spls As New HI.TL.SplashScreen("Calculateing....")
        'For Each R As DataRow In _dt.Rows
        '    _Spls.UpdateInformation(R!FTOrderNo.ToString)
        '    FNOrderQuantity.Value = Val(R!FNGrandQuantity)
        '    Call CalculateCostData(R!FTOrderNo.ToString())
        'Next
        '_Spls.Close()

        'Exit Sub
        If Me.FTOrderNo.Text <> "" Then
            With New HI.RP.Report

                .FormTitle = Me.Text
                .ReportFolderName = "Account\"
                .ReportName = "JobCostSlip.rpt"
                .Formular = "{TACCTJobCost.FTOrderNo}='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                .Preview()

            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        End If
    End Sub

    Private Sub wJobCostTransaction_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            ogv.OptionsView.ShowAutoFilterRow = False
            ogvinvcharge.OptionsView.ShowAutoFilterRow = False

            _FormLoad = False
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogv_RowCountChanged(sender As Object, e As EventArgs) Handles ogv.RowCountChanged
        Try
            If Not (Me.ogc.DataSource Is Nothing) Then
                Dim dtm As DataTable
                Dim dtinv As DataTable

                With CType(Me.ogc.DataSource, DataTable)
                    .AcceptChanges()
                    dtm = .Copy

                End With


                If Me.ogcinvoicecharge.DataSource Is Nothing Then
                    Call LoadDetailOrderInvoicecharge(Me.FTOrderNo.Text)
                End If

                With CType(Me.ogcinvoicecharge.DataSource, DataTable)
                    .AcceptChanges()
                    dtinv = .Copy

                End With

                dtinv.BeginInit()
                For Each R As DataRow In dtinv.Select("FTInvoiceNo<>''")
                    If dtm.Select("FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'").Length <= 0 Then
                        R.Delete()
                    End If
                Next
                dtinv.EndInit()

                For Each R As DataRow In dtm.Select("FTInvoiceNo<>''")
                    If dtinv.Select("FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'").Length <= 0 Then
                        dtinv.Rows.Add(R!FTInvoiceNo.ToString, 0, 0, 0, 0, 0)
                    End If
                Next

                Me.ogcinvoicecharge.DataSource = dtinv.Copy

                Call SumInvoiceCharge()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SumInvoiceCharge()
        With CType(ogcinvoicecharge.DataSource, DataTable)
            .AcceptChanges()

            Dim _FNWageCost As Double = 0
            Dim _FNProdCost As Double = 0
            Dim _FNExportCost As Double = 0
            Dim _FNTransportCost As Double = 0
            Dim _FNTransportAirCost As Double = 0

            For Each R As DataRow In .Rows
                _FNWageCost = _FNWageCost + Val(R!FNWageCost)
                _FNProdCost = _FNProdCost + Val(R!FNProdCost)
                _FNExportCost = _FNExportCost + Val(R!FNExportCost)
                _FNTransportCost = _FNTransportCost + Val(R!FNTransportCost)
                _FNTransportAirCost = _FNTransportAirCost + Val(R!FNTransportAirCost)
            Next

            FNWageCost.Value = _FNWageCost
            FNProdCost.Value = _FNProdCost
            FNExportCost.Value = _FNExportCost
            FNTransportCost.Value = _FNTransportCost
            FNTransportAirCost.Value = _FNTransportAirCost

        End With
    End Sub

    Private Sub RepositoryCost_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryCost.EditValueChanging
        Try
            If e.NewValue < 0 Then
                e.Cancel = True
            Else

                With CType(ogcinvoicecharge.DataSource, DataTable)
                    .AcceptChanges()
                End With

                ogvinvcharge.SetFocusedRowCellValue(ogvinvcharge.FocusedColumn.FieldName.ToString, e.NewValue)

                Call SumInvoiceCharge()
                'With CType(ogcinvoicecharge.DataSource, DataTable)
                '    .AcceptChanges()

                '    Dim _FNWageCost As Double = 0
                '    Dim _FNProdCost As Double = 0
                '    Dim _FNExportCost As Double = 0
                '    Dim _FNTransportCost As Double = 0
                '    Dim _FNTransportAirCost As Double = 0

                '    For Each R As DataRow In .Rows
                '        _FNWageCost = _FNWageCost + Val(R!FNWageCost)
                '        _FNProdCost = _FNProdCost + Val(R!FNProdCost)
                '        _FNExportCost = _FNExportCost + Val(R!FNExportCost)
                '        _FNTransportCost = _FNTransportCost + Val(R!FNTransportCost)
                '        _FNTransportAirCost = _FNTransportAirCost + Val(R!FNTransportAirCost)
                '    Next

                '    FNWageCost.Value = _FNWageCost
                '    FNProdCost.Value = _FNProdCost
                '    FNExportCost.Value = _FNExportCost
                '    FNTransportCost.Value = _FNTransportCost
                '    FNTransportAirCost.Value = _FNTransportAirCost

                'End With

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNConductedCostPer_EditValueChanged(sender As Object, e As EventArgs) Handles FNConductedCostPer.EditValueChanged
        Call CalculateConducted()
    End Sub

    Private Sub FNCommissionCostPer_EditValueChanged(sender As Object, e As EventArgs) Handles FNCommissionCostPer.EditValueChanged
        Call CalculateCommission()
    End Sub


    Private Sub FNDebitAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNDebitAmt.EditValueChanged
        Try
            Me.FNExportAmt.Value = (Me.FNExportGAmt.Value + FNDebitAmt.Value)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNExportGAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNExportGAmt.EditValueChanged
        Try
            Me.FNExportAmt.Value = (Me.FNExportGAmt.Value + FNDebitAmt.Value)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTOrderNoRef_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTOrderNoRef.EditValueChanged
        Try



            Dim _FTOrderNoRef As String = ""
            Dim _FDShipDate As String = ""
            Dim _FTCustomerPO As String = ""

            With CType(sender, DevExpress.XtraEditors.LookUpEdit)
                _FTOrderNoRef = .GetColumnValue("FTOrderNoRef").ToString()
                _FDShipDate = .GetColumnValue("FDShipDate").ToString()
                _FTCustomerPO = .GetColumnValue("FTCustomerPO").ToString()
            End With


            With Me.ogv
                .SetFocusedRowCellValue("FDShipDate", _FDShipDate)
                .SetFocusedRowCellValue("FTCustomerPO", _FTCustomerPO)
            End With

        Catch ex As Exception
        End Try
    End Sub




End Class
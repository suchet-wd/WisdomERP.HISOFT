Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Data
Imports System.Drawing
Imports DevExpress.XtraGrid

Public Class wAccJobCustSummaryBranch
    Private StateOpen As Boolean = False
    Private _CmpCall As String = ""
    Private _Month As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        StateOpen = False
        Call InitGrid()
    End Sub
    Sub New(ByVal month As String, ByVal Cmd As String)

        ' This call is required by the designer.
        InitializeComponent()
        StateOpen = True
        ' Add any initialization after the InitializeComponent() call.
        If (StateOpen) Then

            Me.ocmsave.Visible = False
            Me.ocmload.Visible = False
            Me.ocmSendApprove.Visible = False
            Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
            Me.FNHSysCmpId.ReadOnly = True
            Me.SFTDateTrans.Properties.Buttons(0).Visible = False
            Me.SFTDateTrans.ReadOnly = True

        End If
        Call InitGrid()
        _CmpCall = Cmd
        _Month = month
    End Sub

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = ""


        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNExportQuantity|FNOrderQuantity|FNExportQuantityTo|FNExportQuantityOtherMonth|FNTotalExport|FNOrderQuantityBal"
        Dim sFieldGrpSumAmt As String = "" '"FNExportAmtTHB|FNFabricCost|FNAccessroryCost|FNAccFabStockCost|FNConductedCost|FNOtherCost|FNEmbFacCost|FNEmpPrintBranch|FNExportAmtBF|FNWageCost|FNEmpPrintSub|FNImportExportCost|FNProdCost|FNCommissionCost|FNTransportAirCost|FNNetProfit"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""
        Dim sFieldCustomSumAmt As String = "FNFabricCost|FNAccessroryCost|FNFabricAccMinCost|FNAccFabStockCost"
        sFieldCustomSumAmt &= "|FNFabricAccStockOtherCost|FNOtherServiceCost|FNIncomeAfterRawmaterial|FNImportCost|FNEtcCost|FNSewCost|FNEmbroideryCost|FNPrintCost"
        sFieldCustomSumAmt &= "|FNConductedCost|FNOtherCost|FNEmbFacCost|FNEmpPrintBranch|FNExportAmtBF|FNWageCost|FNEmpPrintSub|FNImportExportCost|FNProdCost|FNCommissionCost|FNTransportAirCost|FNNetProfit|FNNetProfitAct"
        sFieldCustomSumAmt &= "|FNExportQuantity|FNExportAmtTHB|FNOrderQuantity|FNExportQuantityTo|FNExportQuantityOtherMonth|FNTotalExport|FNOrderQuantityBal"
        Dim sFieldCustomGrpSumAmt As String = "" ' "FNExportAmtTHB|FNFabricCost|FNAccessroryCost|FNAccFabStockCost|FNConductedCost|FNOtherCost|FNEmbFacCost|FNEmpPrintBranch|FNExportAmtBF|FNWageCost|FNEmpPrintSub|FNImportExportCost|FNProdCost|FNCommissionCost|FNTransportAirCost|FNNetProfit"
        sFieldSumAmt = sFieldCustomSumAmt
        'With oAdvBandedGridView
        '    .ClearGrouping()
        '    .ClearDocument()

        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomSumAmt.Split("|")

        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
        '        End If

        '    Next

        '    For Each Str As String In sFieldSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldSumAmt.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpCount.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomSumAmt.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSumAmt.Split("|")

        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
        '        End If

        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        '    .ExpandAllGroups()
        '    .RefreshData()

        'End With



        'With oAdvBandedGridView
        '    .ClearGrouping()
        '    .ClearDocument()
        '    .Columns("FTStateRowGrp").Group()

        '    'For Each Str As String In sFieldCustomSumAmt.Split("|")

        '    '    If Str <> "" Then
        '    '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '    '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
        '    '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
        '    '    End If

        '    'Next


        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = False
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        '    .OptionsView.ShowGroupPanel = True
        '    .OptionsView.ShowAutoFilterRow = False

        '    ' Make the group footers always visible.
        '    '.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
        '    ' Create and setup the first summary item.
        '    'Dim item As GridGroupSummaryItem = New GridGroupSummaryItem()
        '    'item.FieldName = "FNNetAmt"
        '    'item.SummaryType = DevExpress.Data.SummaryItemType.Count
        '    '.GroupSummary.Add(item)
        '    ' Create and setup the second summary item.

        '    For Each Str As String In sFieldSumAmt.Split("|")
        '        If Str <> "" Then
        '            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
        '            item1.FieldName = Str
        '            item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '            'item1.DisplayFormat = "Summary by " & .Columns.ColumnByFieldName(Str).Caption & " {0:n2}"
        '            item1.DisplayFormat = "{0:n2}"
        '            item1.ShowInGroupColumnFooter = .Columns(Str)
        '            .GroupSummary.Add(item1)
        '        End If
        '    Next
        '    .ExpandAllGroups()
        '    .RefreshData()
        'End With

        '------End Add Summary Grid-------------

    End Sub

#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = "Select Top 1 Isnull(A.FTStateSendApp,'0') AS FTStateSendApp , Isnull(A.FTStateInspectorApp,'0') AS FTStateInspectorApp , Isnull(A.FTStateFactoryManagerApp,'0') AS FTStateFactoryManagerApp "
            _Qry &= vbCrLf & ", Isnull(A.FTStateApprovedApp,'0') AS FTStateApprovedApp , Isnull(A.FTStateDirectorApp,'0') AS FTStateDirectorApp "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTJobCost_Invoice AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH (NOLOCK) "
            _Qry &= vbCrLf & " ON CASE WHEN ISNULL(A.FTOrderNoRef, '') = '' THEN A.FTOrderNo ELSE ISNULL(A.FTOrderNoRef, '') END = O.FTOrderNo "
            _Qry &= vbCrLf & " Where  LEFT(A.FDInvoiceDate,7) = LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7)"
            _Qry &= vbCrLf & " AND O.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT).Rows
                Me.FTStateSendApp.Checked = R!FTStateSendApp.ToString = "1"
                Me.FTStateInspectorApp.Checked = R!FTStateInspectorApp.ToString = "1"
                Me.FTStateFactoryManagerApp.Checked = R!FTStateFactoryManagerApp.ToString = "1"
                Me.FTStateApprovedApp.Checked = R!FTStateApprovedApp.ToString = "1"
                Me.FTStateDirectorApp.Checked = R!FTStateDirectorApp.ToString = "1"

            Next

            _Qry = " Select * "
            _Qry &= vbCrLf & "  Into #TmpJobCost"
            _Qry &= vbCrLf & " From     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_JobCostSummary_Branch "

            _Qry &= vbCrLf & " Select 0 AS FNStateRow,FTInvoiceNo    "
            _Qry &= vbCrLf & " 	, FTOrderNo"
            _Qry &= vbCrLf & "	, FTPORef"
            _Qry &= vbCrLf & "	, FTStyleCode"
            _Qry &= vbCrLf & "	, FNExportQuantity"
            _Qry &= vbCrLf & "	, FNExportAmtTHB"
            _Qry &= vbCrLf & "	, FNExportAmt"
            _Qry &= vbCrLf & " , Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNWagePull / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNWagePullPer "
            _Qry &= vbCrLf & " , FNWagePull "
            _Qry &= vbCrLf & " , FNNetProfitRcv"
            _Qry &= vbCrLf & " , FNFabricAccMinCost "
            _Qry &= vbCrLf & " , Case WHEN FNFabricAccMinCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNFabricAccMinCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS FNFabricAccMinCostPer"
            _Qry &= vbCrLf & " , FNFabricAccStockOtherCost "
            _Qry &= vbCrLf & " , Case WHEN FNFabricAccStockOtherCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNFabricAccStockOtherCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNFabricAccStockOtherCostPER"
            _Qry &= vbCrLf & " , FNOtherServiceCost "
            _Qry &= vbCrLf & " , Case WHEN FNOtherServiceCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNOtherServiceCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS FNOtherServiceCostPer"
            _Qry &= vbCrLf & " , (FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNOtherServiceCost-FNFabricAccMinCost) AS FNIncomeAfterRawmaterial " 'FNAccFabStockCost-
            _Qry &= vbCrLf & " , Case WHEN (FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNAccFabStockCost-FNOtherServiceCost-FNFabricAccMinCost) > 0 Then "
            _Qry &= "  Convert(numeric(18,2), ((FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNAccFabStockCost-FNOtherServiceCost-FNFabricAccMinCost) / FNExportAmtTHBNet) * 100.00) Else 0 END AS  FNIncomeAfterRawmaterialPER  "
            _Qry &= vbCrLf & " ,FNImportCost "
            _Qry &= vbCrLf & " ,Case WHEN FNImportCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNImportCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNImportCostPer "
            _Qry &= vbCrLf & " ,FNEtcCost "
            _Qry &= vbCrLf & " ,Case WHEN FNEtcCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNEtcCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNEtcCostPer "
            _Qry &= vbCrLf & " ,FNSewCost"
            _Qry &= vbCrLf & " ,Case WHEN FNSewCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNSewCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNSewCostPer "
            _Qry &= vbCrLf & " , FNEmbroideryCost "
            _Qry &= vbCrLf & " ,Case WHEN FNEmbroideryCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNEmbroideryCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNEmbroideryCostPer "
            _Qry &= vbCrLf & " ,FNPrintCost "
            _Qry &= vbCrLf & " ,Case WHEN FNPrintCost > 0 AND FNExportAmtTHBNet>0  Then Convert(numeric(18,2),(FNPrintCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNPrintCostPer "

            _Qry &= vbCrLf & "	,Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNFabricCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNFabricCostPer"
            _Qry &= vbCrLf & " , FNFabricCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNAccessroryCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNAccessroryCostPer"
            _Qry &= vbCrLf & ", FNAccessroryCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNAccFabStockCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNAccFabStockCostPer"
            _Qry &= vbCrLf & ", FNAccFabStockCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNConductedCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNConductedCostPer"
            _Qry &= vbCrLf & ", FNConductedCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNOtherCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNOtherCostPer"
            _Qry &= vbCrLf & ", FNOtherCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNEmbFacCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNEmbFacCostPer"
            _Qry &= vbCrLf & ", FNEmbFacCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNEmpPrintBranch / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNEmpPrintBranchPer"
            _Qry &= vbCrLf & ", FNEmpPrintBranch"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),((FNExportAmtTHBNet - (FNFabricCost+FNAccessroryCost+FNAccFabStockCost+FNConductedCost+FNEmbFacCost+FNEmpPrintBranch + FNWagePull ))  / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNExportAmtBFPer"
            _Qry &= vbCrLf & ",  (FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNOtherServiceCost-FNFabricAccMinCost) - (FNConductedCost+FNEmbFacCost+FNEmpPrintBranch + FNWagePull) AS  FNExportAmtBF "
            '_Qry &= vbCrLf & ",(FNExportAmtTHBNet - (FNFabricCost+FNAccessroryCost+FNAccFabStockCost))  AS FNExportAmtBF" '+FNConductedCost+FNEmbFacCost+FNEmpPrintBranch
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNWageCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNWageCostPer"
            _Qry &= vbCrLf & ", FNWageCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNEmpPrintSub / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNEmpPrintSubPer"
            _Qry &= vbCrLf & ", FNEmpPrintSub"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNImportExportCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNImportExportCostPer"
            _Qry &= vbCrLf & ", FNImportExportCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNProdCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNProdCostPer"
            _Qry &= vbCrLf & ", FNProdCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNCommissionCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNCommissionCostPer"
            _Qry &= vbCrLf & ", FNCommissionCost"
            _Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNTransportAirCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNTransportAirCostPer"
            _Qry &= vbCrLf & ", FNTransportAirCost"
            _Qry &= vbCrLf & ", convert(nvarchar(30), convert(money,Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNNetProfit / FNExportAmtTHBNet ) *100.00) Else 0 END  )) AS FNNetProfitPer"
            _Qry &= vbCrLf & ", FNNetProfit,FNNetProfitAct"
            _Qry &= vbCrLf & ", FNOrderQuantity"
            _Qry &= vbCrLf & " ,FNExportQuantityNet AS FNExportQuantityTo"
            _Qry &= vbCrLf & "	, FNExportQuantityOtherMonth"
            _Qry &= vbCrLf & ", (FNExportQuantityNet + FNExportQuantityOtherMonth) AS FNTotalExport"
            _Qry &= vbCrLf & "	, (FNOrderQuantity - (FNExportQuantityNet + FNExportQuantityOtherMonth )) AS FNOrderQuantityBal"
            _Qry &= vbCrLf & ",Case WHEN FNOrderQuantity >0 Then Convert(numeric(18,2),((FNOrderQuantity - (FNExportQuantityNet + FNExportQuantityOtherMonth )) / FNOrderQuantity ) *100.00) Else 0 END AS FNLostPer"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & " (SELECT  A.FTInvoiceNo"
            _Qry &= vbCrLf & ", A.FTOrderNo  "
            _Qry &= vbCrLf & ", A.FTPORef"
            _Qry &= vbCrLf & ", A.FTStyleCode"
            _Qry &= vbCrLf & ", Sum(A.FNExportQuantity) AS FNExportQuantity"
            _Qry &= vbCrLf & ", SUM(A.FNExportAmtTHB) AS FNExportAmtTHB"
            _Qry &= vbCrLf & ", SUM(A.FNExportAmt) AS FNExportAmt"
            _Qry &= vbCrLf & ",Sum(Isnull(A.FNWagePull,0)) AS FNWagePull "
            _Qry &= vbCrLf & ",Sum(A.FNNetProfitRcv) AS FNNetProfitRcv"
            _Qry &= vbCrLf & ", A.FNFabricCost"
            _Qry &= vbCrLf & ", A.FNAccessroryCost"
            _Qry &= vbCrLf & ", A.FNAccFabStockCost"
            _Qry &= vbCrLf & ", A.FNConductedCost"
            _Qry &= vbCrLf & ", A.FNOtherCost"
            _Qry &= vbCrLf & ", A.FNEmbFacCost"
            '_Qry &= vbCrLf & ", A.FNEmbBranchCost + FNPrintBranchCost AS FNEmpPrintBranch"
            _Qry &= vbCrLf & ", 0 AS FNEmpPrintBranch"
            _Qry &= vbCrLf & ", A.FNWageCost"
            _Qry &= vbCrLf & ", A.FNEmbSubCost + FNPrintSubCost AS FNEmpPrintSub"
            _Qry &= vbCrLf & ", sum( isnull(FNExportCost,0) + isnull(FNTransportCost,0)) AS FNImportExportCost"
            '_Qry &= vbCrLf & ", A.FNImportCost + FNExportCost + FNTransportCost AS FNImportExportCost"
            _Qry &= vbCrLf & ", A.FNProdCost"
            _Qry &= vbCrLf & ", A.FNCommissionCost"
            _Qry &= vbCrLf & ", A.FNTransportAirCost"
            _Qry &= vbCrLf & ", A.FNNetProfit"
            _Qry &= vbCrLf & ", A.FNOrderQuantity"
            _Qry &= vbCrLf & ", A.FNNetProfitAct"
            _Qry &= vbCrLf & " ,Sum(A.FNFabricDummyCost + A.FNAccessroryDummyCost) AS FNFabricAccMinCost "
            _Qry &= vbCrLf & " ,Sum(A.FNAccFabStockOtherCost) AS FNFabricAccStockOtherCost "
            _Qry &= vbCrLf & " ,Sum(A.FNServiceCost) AS FNOtherServiceCost"
            _Qry &= vbCrLf & " ,Sum(A.FNImportCost) AS FNImportCost"
            _Qry &= vbCrLf & " ,Sum(A.FNOtherCost) AS FNEtcCost "
            _Qry &= vbCrLf & " ,Sum(A.FNEmbBranchCost) as FNEmbroideryCost   "
            _Qry &= vbCrLf & " ,Sum(A.FNPrintBranchCost) AS FNPrintCost "
            '_Qry &= vbCrLf & " ,(A.FNEmbBranchCost + FNPrintBranchCost) AS FNPrintCost " 'Edit 20160527 แก้ไขบันทึกหน้ากำไรขาดทุน
            _Qry &= vbCrLf & " ,Sum(A.FNWageCutSew) AS FNSewCost "

            _Qry &= vbCrLf & ", A.FNExportQuantityOtherMonth"
            _Qry &= vbCrLf & ", ISNULL(B.FNExportQuantityNet,0) AS FNExportQuantityNet"
            _Qry &= vbCrLf & ", ISNULL(B.FNExportAmtTHBNet,0) AS FNExportAmtTHBNet"
            _Qry &= vbCrLf & "  FROM     #TmpJobCost AS A"
            _Qry &= vbCrLf & " INNER JOIN (SELECT X.FTOrderNo"
            _Qry &= vbCrLf & ", Sum(X.FNExportQuantity) AS FNExportQuantityNet"
            _Qry &= vbCrLf & ", SUM(X.FNExportAmtTHB) AS FNExportAmtTHBNet"
            _Qry &= vbCrLf & "  FROM     #TmpJobCost AS X"
            _Qry &= vbCrLf & " WHERE X.FTInvoiceNo <> '' "
            _Qry &= vbCrLf & " AND X.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & " AND LEFT(X.FDInvoiceDate,7) =LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
            _Qry &= vbCrLf & " GROUP BY X.FTOrderNo ) AS B ON A.FTOrderNo=B.FTOrderNo"
            _Qry &= vbCrLf & " WHERE A.FTInvoiceNo <> '' "
            _Qry &= vbCrLf & " AND A.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & " AND LEFT(A.FDInvoiceDate,7) =LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
            _Qry &= vbCrLf & " GROUP BY A.FTInvoiceNo"
            _Qry &= vbCrLf & ", A.FTOrderNo"
            _Qry &= vbCrLf & ", A.FTPORef"
            _Qry &= vbCrLf & ", A.FTStyleCode "
            _Qry &= vbCrLf & ", A.FNFabricCost"
            _Qry &= vbCrLf & ", A.FNAccessroryCost"
            _Qry &= vbCrLf & ", A.FNAccFabStockCost"
            _Qry &= vbCrLf & ", A.FNConductedCost"
            _Qry &= vbCrLf & ", A.FNOtherCost"
            _Qry &= vbCrLf & ", A.FNEmbFacCost"
            _Qry &= vbCrLf & ", A.FNEmbBranchCost + A.FNPrintBranchCost "
            _Qry &= vbCrLf & ", A.FNWageCost"
            _Qry &= vbCrLf & ", A.FNEmbSubCost + A.FNPrintSubCost"
            _Qry &= vbCrLf & ", A.FNExportCost + A.FNTransportCost "
            _Qry &= vbCrLf & ", A.FNProdCost"
            _Qry &= vbCrLf & ", A.FNCommissionCost"
            _Qry &= vbCrLf & ", A.FNTransportAirCost"
            _Qry &= vbCrLf & ", A.FNNetProfit,A.FNNetProfitAct  "
            _Qry &= vbCrLf & ", A.FNOrderQuantity"
            _Qry &= vbCrLf & ", ISNULL(B.FNExportQuantityNet,0) "
            _Qry &= vbCrLf & ", ISNULL(B.FNExportAmtTHBNet,0)  "
            _Qry &= vbCrLf & ", A.FNExportQuantityOtherMonth  ) AS A ORDER BY FTOrderNo "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Me.ogdtime.DataSource = dt.Copy
            Call InitialGridMergCell()
            _Qry = "Select  FNHSysCmpId, FTOfMonth, FNIncomeAfter, FNWage, FNProdutionCost ,(Isnull(FNIncomeAfter,0) + Isnull(FNWage,0) - Isnull(FNProdutionCost,0)) AS FNTotal "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Additional WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysCmpId=" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString))
            _Qry &= vbCrLf & " AND FTOfMonth='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text), 7) & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            Dim _AmtAdj As Double = 0
            Dim _oDtAdd As DataTable = _oDt

            If dt.Rows.Count > 0 Then
                Dim row As System.Data.DataRow
                row = dt.NewRow

                For Each c As DataColumn In dt.Columns
                    Try
                        Select Case c.ColumnName.ToString
                            Case "FNStateRow"
                                row.Item(c.ColumnName.ToString) = 99
                            Case "FNIncomeAfterRawmaterial"
                                If _oDt.Rows.Count > 0 Then
                                    row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item("FNIncomeAfter")
                                Else
                                    row.Item(c.ColumnName.ToString) = 0
                                End If

                            Case "FNWageCost"
                                If _oDt.Rows.Count > 0 Then
                                    row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item("FNWage")
                                Else
                                    row.Item(c.ColumnName.ToString) = 0
                                End If

                            Case "FNEtcCost"
                                If _oDt.Rows.Count > 0 Then
                                    row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item("FNProdutionCost")
                                Else
                                    row.Item(c.ColumnName.ToString) = 0
                                End If
                            Case "FNNetProfit"
                                If _oDt.Rows.Count > 0 Then
                                    row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item("FNTotal")
                                    _AmtAdj = _oDt.Rows(0).Item("FNTotal")
                                Else
                                    row.Item(c.ColumnName.ToString) = 0
                                End If
                            Case Else
                                ' row.Item(c.FieldName.ToString) = System.DBNull
                        End Select
                    Catch ex As Exception
                        '  row.Item(c.FieldName.ToString) = 0
                    End Try
                Next

                dt.Rows.Add(row)
            End If
            dt.AcceptChanges()

            '_Qry = "  Select   sum(FNNetProfit) As FNNetProfit ,sum(FNWagePull) As  FNWagePull ,sum(FNEmbFacCost) As FNEmbFacCost ,sum(FNNetProfitRcv) As FNNetProfitRcv ,sum(FNFabricCost)  As FNFabricCost"
            '_Qry &= vbCrLf & "	,sum(FNAccessroryCost) as FNAccessroryCost ,sum(FNFabricAccMinCost) as FNFabricAccMinCost ,sum(FNAccFabStockCost) as FNAccFabStockCost"
            '_Qry &= vbCrLf & "	,sum(FNFabricAccStockOtherCost) as FNFabricAccStockOtherCost ,sum(FNOtherServiceCost) as FNOtherServiceCost , "
            '_Qry &= vbCrLf & " sum(FNIncomeAfterRawmaterial) As FNIncomeAfterRawmaterial,sum(FNImportCost) As FNImportCost ,sum(FNEtcCost)  As FNEtcCost "
            '_Qry &= vbCrLf & " ,sum(FNSewCost) As FNSewCost ,sum(FNEmbroideryCost) As FNEmbroideryCost ,sum(FNPrintCost)  As FNPrintCost"
            '_Qry &= vbCrLf & " ,sum(FNConductedCost) As FNConductedCost ,sum(FNOtherCost) As FNOtherCost ,sum(FNEmpPrintBranch) As FNEmpPrintBranch ,sum(FNExportAmtBF)  As FNExportAmtBF"
            '_Qry &= vbCrLf & " ,sum(FNWageCost) As FNWageCost ,sum(FNEmpPrintSub) As FNEmpPrintSub , "
            '_Qry &= vbCrLf & "  sum(FNImportExportCost) As FNImportExportCost,sum(FNProdCost) As FNProdCost,sum(FNCommissionCost) As FNCommissionCost,sum(FNTransportAirCost) As FNTransportAirCost"
            '_Qry &= vbCrLf & " ,sum(FNExportQuantity) As FNExportQuantity,sum(FNExportAmtTHB) As FNExportAmtTHB,sum(FNOrderQuantity) As FNOrderQuantity"
            '_Qry &= vbCrLf & " ,sum(FNExportQuantityTo) As FNExportQuantityTo,sum(FNExportQuantityOtherMonth) As FNExportQuantityOtherMonth,sum(FNTotalExport) As FNTotalExport, "
            '_Qry &= vbCrLf & "   sum(FNOrderQuantityBal) As NOrderQuantityBal"

            '_Qry &= vbCrLf & "FROM ( Select max(X.FNNetProfit) As FNNetProfit , max(X.FNWagePull) As FNWagePull ,max(X.FNEmbFacCost) As FNEmbFacCost , max(X.FNNetProfitRcv) As FNNetProfitRcv   "
            '_Qry &= vbCrLf & ",max(X.FNFabricCost) As FNFabricCost ,max(X.FNAccessroryCost) As FNAccessroryCost , max(FNFabricAccMinCost)  As FNFabricAccMinCost , max(X.FNAccFabStockCost) As FNAccFabStockCost"
            '_Qry &= vbCrLf & ",max(X.FNFabricAccStockOtherCost) As FNFabricAccStockOtherCost ,max(X.FNOtherServiceCost) As FNOtherServiceCost , max(FNIncomeAfterRawmaterial) As FNIncomeAfterRawmaterial , max(X.FNImportCost) As FNImportCost"
            '_Qry &= vbCrLf & ",max(X.FNOtherCost) As FNEtcCost ,max(X.FNSewCost) As FNSewCost , max(X.FNEmbroideryCost) As FNEmbroideryCost , max(X.FNPrintCost) As FNPrintCost"
            '_Qry &= vbCrLf & ",max(X.FNConductedCost) As FNConductedCost ,max(X.FNOtherCost) As FNOtherCost , 0 As FNEmpPrintBranch  "
            '_Qry &= vbCrLf & ",max(FNExportAmtBF)  As FNExportAmtBF " '0 as FNEmpPrintBranch
            '_Qry &= vbCrLf & ",max(X.FNWageCost) As FNWageCost , max(FNEmpPrintSub) As FNEmpPrintSub , Max(FNImportExportCost) As FNImportExportCost"
            '_Qry &= vbCrLf & ",max(X.FNProdCost) As FNProdCost ,max(X.FNCommissionCost) As FNCommissionCost , max(X.FNTransportAirCost) As FNTransportAirCost , sum(X.FNExportQuantity) As FNExportQuantity"
            '_Qry &= vbCrLf & ",max(X.FNExportAmtTHB) As FNExportAmtTHB ,max(X.FNOrderQuantity) As FNOrderQuantity , max(FNExportQuantityTo) As FNExportQuantityTo , max(X.FNExportQuantityOtherMonth) As FNExportQuantityOtherMonth"
            '_Qry &= vbCrLf & ",max(FNTotalExport) As FNTotalExport ,max(FNOrderQuantityBal)  As FNOrderQuantityBal "
            '_Qry &= vbCrLf & "  FROM      "
            '_Qry &= vbCrLf & " ( Select 0 AS FNStateRow,FTInvoiceNo    "
            '_Qry &= vbCrLf & " 	, FTOrderNo"
            '_Qry &= vbCrLf & "	, FTPORef"
            '_Qry &= vbCrLf & "	, FTStyleCode"
            '_Qry &= vbCrLf & "	, FNExportQuantity"
            '_Qry &= vbCrLf & "	, FNExportAmtTHB"
            '_Qry &= vbCrLf & "	, FNExportAmt"
            '_Qry &= vbCrLf & " , Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNWagePull / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNWagePullPer "
            '_Qry &= vbCrLf & " , FNWagePull "
            '_Qry &= vbCrLf & " , FNNetProfitRcv"
            '_Qry &= vbCrLf & " , FNFabricAccMinCost "
            '_Qry &= vbCrLf & " , Case WHEN FNFabricAccMinCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNFabricAccMinCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS FNFabricAccMinCostPer"
            '_Qry &= vbCrLf & " , FNFabricAccStockOtherCost "
            '_Qry &= vbCrLf & " , Case WHEN FNFabricAccStockOtherCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNFabricAccStockOtherCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNFabricAccStockOtherCostPER"
            '_Qry &= vbCrLf & " , FNOtherServiceCost "
            '_Qry &= vbCrLf & " , Case WHEN FNOtherServiceCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNOtherServiceCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS FNOtherServiceCostPer"
            '_Qry &= vbCrLf & " , (FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNOtherServiceCost-FNFabricAccMinCost) AS FNIncomeAfterRawmaterial " 'FNAccFabStockCost-
            '_Qry &= vbCrLf & " , Case WHEN (FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNAccFabStockCost-FNOtherServiceCost-FNFabricAccMinCost) > 0 Then "
            '_Qry &= "  Convert(numeric(18,2), ((FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNAccFabStockCost-FNOtherServiceCost-FNFabricAccMinCost) / FNExportAmtTHBNet) * 100.00) Else 0 END AS  FNIncomeAfterRawmaterialPER  "
            '_Qry &= vbCrLf & " ,FNImportCost "
            '_Qry &= vbCrLf & " ,Case WHEN FNImportCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNImportCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNImportCostPer "
            '_Qry &= vbCrLf & " ,FNEtcCost "
            '_Qry &= vbCrLf & " ,Case WHEN FNEtcCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNEtcCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNEtcCostPer "
            '_Qry &= vbCrLf & " ,FNSewCost"
            '_Qry &= vbCrLf & " ,Case WHEN FNSewCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNSewCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNSewCostPer "
            '_Qry &= vbCrLf & " , FNEmbroideryCost "
            '_Qry &= vbCrLf & " ,Case WHEN FNEmbroideryCost > 0 AND FNExportAmtTHBNet>0   Then Convert(numeric(18,2),(FNEmbroideryCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNEmbroideryCostPer "
            '_Qry &= vbCrLf & " ,FNPrintCost "
            '_Qry &= vbCrLf & " ,Case WHEN FNPrintCost > 0 AND FNExportAmtTHBNet>0  Then Convert(numeric(18,2),(FNPrintCost / FNExportAmtTHBNet ) *100.00) Else 0 END  AS  FNPrintCostPer "

            '_Qry &= vbCrLf & "	,Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNFabricCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNFabricCostPer"
            '_Qry &= vbCrLf & " , FNFabricCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNAccessroryCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNAccessroryCostPer"
            '_Qry &= vbCrLf & ", FNAccessroryCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNAccFabStockCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNAccFabStockCostPer"
            '_Qry &= vbCrLf & ", FNAccFabStockCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNConductedCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNConductedCostPer"
            '_Qry &= vbCrLf & ", FNConductedCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNOtherCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNOtherCostPer"
            '_Qry &= vbCrLf & ", FNOtherCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNEmbFacCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNEmbFacCostPer"
            '_Qry &= vbCrLf & ", FNEmbFacCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNEmpPrintBranch / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNEmpPrintBranchPer"
            '_Qry &= vbCrLf & ", FNEmpPrintBranch"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),((FNExportAmtTHBNet - (FNFabricCost+FNAccessroryCost+FNAccFabStockCost+FNConductedCost+FNEmbFacCost+FNEmpPrintBranch + FNWagePull ))  / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNExportAmtBFPer"
            '_Qry &= vbCrLf & ",  (FNExportAmtTHBNet-FNFabricCost-FNAccessroryCost-FNFabricAccStockOtherCost-FNOtherServiceCost-FNFabricAccMinCost) - (FNConductedCost+FNEmbFacCost+FNEmpPrintBranch + FNWagePull) AS  FNExportAmtBF "
            ''_Qry &= vbCrLf & ",(FNExportAmtTHBNet - (FNFabricCost+FNAccessroryCost+FNAccFabStockCost))  AS FNExportAmtBF" '+FNConductedCost+FNEmbFacCost+FNEmpPrintBranch
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNWageCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNWageCostPer"
            '_Qry &= vbCrLf & ", FNWageCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNEmpPrintSub / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNEmpPrintSubPer"
            '_Qry &= vbCrLf & ", FNEmpPrintSub"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNImportExportCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNImportExportCostPer"
            '_Qry &= vbCrLf & ", FNImportExportCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNProdCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNProdCostPer"
            '_Qry &= vbCrLf & ", FNProdCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNCommissionCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNCommissionCostPer"
            '_Qry &= vbCrLf & ", FNCommissionCost"
            '_Qry &= vbCrLf & ",Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNTransportAirCost / FNExportAmtTHBNet ) *100.00) Else 0 END AS FNTransportAirCostPer"
            '_Qry &= vbCrLf & ", FNTransportAirCost"
            '_Qry &= vbCrLf & ", convert(nvarchar(30), convert(money,Case WHEN FNExportAmtTHBNet>0 Then Convert(numeric(18,2),(FNNetProfit / FNExportAmtTHBNet ) *100.00) Else 0 END  )) AS FNNetProfitPer"
            '_Qry &= vbCrLf & ", FNNetProfit"
            '_Qry &= vbCrLf & ", FNOrderQuantity"
            '_Qry &= vbCrLf & " ,FNExportQuantityNet AS FNExportQuantityTo"
            '_Qry &= vbCrLf & "	, FNExportQuantityOtherMonth"
            '_Qry &= vbCrLf & ", (FNExportQuantityNet + FNExportQuantityOtherMonth) AS FNTotalExport"
            '_Qry &= vbCrLf & "	, (FNOrderQuantity - (FNExportQuantityNet + FNExportQuantityOtherMonth )) AS FNOrderQuantityBal"
            '_Qry &= vbCrLf & ",Case WHEN FNOrderQuantity >0 Then Convert(numeric(18,2),((FNOrderQuantity - (FNExportQuantityNet + FNExportQuantityOtherMonth )) / FNOrderQuantity ) *100.00) Else 0 END AS FNLostPer"
            '_Qry &= vbCrLf & " FROM"
            '_Qry &= vbCrLf & " (SELECT  A.FTInvoiceNo"
            '_Qry &= vbCrLf & ", A.FTOrderNo  "
            '_Qry &= vbCrLf & ", A.FTPORef"
            '_Qry &= vbCrLf & ", A.FTStyleCode"
            '_Qry &= vbCrLf & ", Sum(A.FNExportQuantity) AS FNExportQuantity"
            '_Qry &= vbCrLf & ", SUM(A.FNExportAmtTHB) AS FNExportAmtTHB"
            '_Qry &= vbCrLf & ", SUM(A.FNExportAmt) AS FNExportAmt"
            '_Qry &= vbCrLf & ",Sum(Isnull(A.FNWagePull,0)) AS FNWagePull "
            '_Qry &= vbCrLf & ",Sum(A.FNNetProfitRcv) AS FNNetProfitRcv"
            '_Qry &= vbCrLf & ", A.FNFabricCost"
            '_Qry &= vbCrLf & ", A.FNAccessroryCost"
            '_Qry &= vbCrLf & ", A.FNAccFabStockCost"
            '_Qry &= vbCrLf & ", A.FNConductedCost"
            '_Qry &= vbCrLf & ", A.FNOtherCost"
            '_Qry &= vbCrLf & ", A.FNEmbFacCost"
            '_Qry &= vbCrLf & ", 0 AS FNEmpPrintBranch"
            '_Qry &= vbCrLf & ", A.FNWageCost"
            '_Qry &= vbCrLf & ", A.FNEmbSubCost + FNPrintSubCost AS FNEmpPrintSub"
            '_Qry &= vbCrLf & ",  FNExportCost + FNTransportCost AS FNImportExportCost"
            '_Qry &= vbCrLf & ", A.FNProdCost"
            '_Qry &= vbCrLf & ", A.FNCommissionCost"
            '_Qry &= vbCrLf & ", A.FNTransportAirCost"
            '_Qry &= vbCrLf & ", A.FNNetProfit"
            '_Qry &= vbCrLf & ", A.FNOrderQuantity"

            '_Qry &= vbCrLf & " ,Sum(A.FNFabricDummyCost + A.FNAccessroryDummyCost) AS FNFabricAccMinCost "
            '_Qry &= vbCrLf & " ,Sum(A.FNAccFabStockOtherCost) AS FNFabricAccStockOtherCost "
            '_Qry &= vbCrLf & " ,Sum(A.FNServiceCost) AS FNOtherServiceCost"
            '_Qry &= vbCrLf & " ,Sum(A.FNImportCost) AS FNImportCost"
            '_Qry &= vbCrLf & " ,Sum(A.FNOtherCost) AS FNEtcCost "
            '_Qry &= vbCrLf & " ,Sum(A.FNEmbBranchCost) as FNEmbroideryCost   "
            '_Qry &= vbCrLf & " ,Sum(A.FNPrintBranchCost) AS FNPrintCost "
            '_Qry &= vbCrLf & " ,Sum(A.FNWageCutSew) AS FNSewCost "

            '_Qry &= vbCrLf & ", A.FNExportQuantityOtherMonth"
            '_Qry &= vbCrLf & ", ISNULL(B.FNExportQuantityNet,0) AS FNExportQuantityNet"
            '_Qry &= vbCrLf & ", ISNULL(B.FNExportAmtTHBNet,0) AS FNExportAmtTHBNet"
            '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_JobCostSummary_Branch AS A"
            '_Qry &= vbCrLf & " INNER JOIN (SELECT X.FTOrderNo"
            '_Qry &= vbCrLf & ", Sum(X.FNExportQuantity) AS FNExportQuantityNet"
            '_Qry &= vbCrLf & ", SUM(X.FNExportAmtTHB) AS FNExportAmtTHBNet"
            '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_JobCostSummary_Branch AS X"
            '_Qry &= vbCrLf & " WHERE X.FTInvoiceNo <> '' "
            '_Qry &= vbCrLf & " AND X.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            '_Qry &= vbCrLf & " AND LEFT(X.FDInvoiceDate,7) =LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
            '_Qry &= vbCrLf & " GROUP BY X.FTOrderNo ) AS B ON A.FTOrderNo=B.FTOrderNo"
            '_Qry &= vbCrLf & " WHERE A.FTInvoiceNo <> '' "
            '_Qry &= vbCrLf & " AND A.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            '_Qry &= vbCrLf & " AND LEFT(A.FDInvoiceDate,7) =LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
            '_Qry &= vbCrLf & " GROUP BY A.FTInvoiceNo"
            '_Qry &= vbCrLf & ", A.FTOrderNo"
            '_Qry &= vbCrLf & ", A.FTPORef"
            '_Qry &= vbCrLf & ", A.FTStyleCode "
            '_Qry &= vbCrLf & ", A.FNFabricCost"
            '_Qry &= vbCrLf & ", A.FNAccessroryCost"
            '_Qry &= vbCrLf & ", A.FNAccFabStockCost"
            '_Qry &= vbCrLf & ", A.FNConductedCost"
            '_Qry &= vbCrLf & ", A.FNOtherCost"
            '_Qry &= vbCrLf & ", A.FNEmbFacCost"
            '_Qry &= vbCrLf & ", A.FNEmbBranchCost + A.FNPrintBranchCost "
            '_Qry &= vbCrLf & ", A.FNWageCost"
            '_Qry &= vbCrLf & ", A.FNEmbSubCost + A.FNPrintSubCost"
            '_Qry &= vbCrLf & ", A.FNExportCost + A.FNTransportCost "
            '_Qry &= vbCrLf & ", A.FNProdCost"
            '_Qry &= vbCrLf & ", A.FNCommissionCost"
            '_Qry &= vbCrLf & ", A.FNTransportAirCost"
            '_Qry &= vbCrLf & ", A.FNNetProfit "
            '_Qry &= vbCrLf & ", A.FNOrderQuantity"
            '_Qry &= vbCrLf & ", ISNULL(B.FNExportQuantityNet,0) "
            '_Qry &= vbCrLf & ", ISNULL(B.FNExportAmtTHBNet,0)  "
            '_Qry &= vbCrLf & ", A.FNExportQuantityOtherMonth) AS A  ) AS X  "


            ''_Qry &= vbCrLf & " WHERE X.FTInvoiceNo <> '' "
            ''_Qry &= vbCrLf & " AND X.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            ''_Qry &= vbCrLf & " AND LEFT(X.FDInvoiceDate,7) =LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
            '_Qry &= vbCrLf & " group by X.FTOrderNo ,X.FTInvoiceNo )AS Z"

            _Qry = "Exec GetSumJobCostSummary_Branch " & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & ",'" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            _AmtAdj = 0
            If dt.Rows.Count > 0 Then
                Dim row As System.Data.DataRow
                row = dt.NewRow
                For Each c As DataColumn In dt.Columns
                    Try
                        Select Case c.ColumnName.ToString
                            Case "FNStateRow"
                                row.Item(c.ColumnName.ToString) = 101
                            Case "FNNetProfit"

                                If _oDt.Rows.Count > 0 Then
                                    If _oDtAdd.Rows.Count > 0 Then
                                        row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item("FNNetProfit") + _oDtAdd.Rows(0).Item("FNTotal")
                                        _AmtAdj += +_oDt.Rows(0).Item("FNNetProfit") + _oDtAdd.Rows(0).Item("FNTotal")
                                    Else
                                        row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item("FNNetProfit")
                                        _AmtAdj += +_oDt.Rows(0).Item("FNNetProfit")
                                    End If
                                Else
                                    row.Item(c.ColumnName.ToString) = _oDtAdd.Rows(0).Item("FNTotal")
                                    _AmtAdj += +_oDtAdd.Rows(0).Item("FNTotal")
                                End If
                            Case "FNIncomeAfterRawmaterial"
                                If _oDt.Rows.Count > 0 Then
                                    If _oDtAdd.Rows.Count > 0 Then
                                        row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString) + _oDtAdd.Rows(0).Item("FNIncomeAfter")
                                    Else
                                        row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString)
                                    End If

                                Else
                                    row.Item(c.ColumnName.ToString) = _oDtAdd.Rows(0).Item("FNIncomeAfter")
                                End If

                            Case "FNWageCost"
                                If _oDt.Rows.Count > 0 Then
                                    If _oDtAdd.Rows.Count > 0 Then
                                        If _oDtAdd.Rows.Count > 0 Then
                                            row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString) + _oDtAdd.Rows(0).Item("FNWage")
                                        Else
                                            row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString)
                                        End If
                                    Else
                                        row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString)
                                    End If
                                Else
                                    row.Item(c.ColumnName.ToString) = _oDtAdd.Rows(0).Item("FNWage")
                                End If

                            Case "FNEtcCost"
                                If _oDt.Rows.Count > 0 Then
                                    If _oDtAdd.Rows.Count > 0 Then
                                        row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString) + _oDtAdd.Rows(0).Item("FNProdutionCost")
                                    Else
                                        row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString)
                                    End If
                                Else
                                    row.Item(c.ColumnName.ToString) = _oDtAdd.Rows(0).Item("FNProdutionCost")
                                End If
                            Case Else
                                If _oDt.Rows.Count > 0 Then
                                    row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item(c.ColumnName.ToString)
                                Else
                                    row.Item(c.ColumnName.ToString) = 0
                                End If
                        End Select
                    Catch ex As Exception
                        '  row.Item(c.FieldName.ToString) = 0
                    End Try
                Next
                dt.Rows.Add(row)
            End If
            dt.AcceptChanges()

            _Qry = "Select sum(FNNetProfit) AS FNNetProfit , max(FNNetProfitPer) AS FNNetProfitPer"
            _Qry &= vbCrLf & " From (Select  (X.FNNetProfit) AS FNNetProfit ,'" & Me.ProfitBF_lbl.Text & "' AS FNNetProfitPer"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_JobCostSummary_Branch AS X"
            _Qry &= vbCrLf & " WHERE X.FTInvoiceNo <> '' "
            _Qry &= vbCrLf & " AND X.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & " AND LEFT(X.FDInvoiceDate,7) < LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
            _Qry &= vbCrLf & " AND LEFT(X.FDInvoiceDate,4) = LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',4) "
            _Qry &= vbCrLf & "   group by X.FTOrderNo ,FNNetProfit ) AS Z "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)


            _Qry = "   Select sum( (Isnull(FNIncomeAfter,0) + Isnull(FNWage,0) - Isnull(FNProdutionCost,0)) ) AS FNTotal "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Additional WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysCmpId=" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString))
            _Qry &= vbCrLf & " AND LEFT(FTOfMonth,7) = '" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text), 7) & "'"

            '  _Qry &= vbCrLf & " AND LEFT( FTOfMonth,4) = '" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text), 4) & "'"
            Dim _AmtBF As Double = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")
            Dim _AmtBFF As Double = 0

            _Qry = " Select top 1   FNIncomeAfter + FNWage + FNProdutionCost as TtotalBF From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Additional WITH(NOLOCK) "
            _Qry &= vbCrLf & "  where FTOfMonth < '" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text), 7) & "'"
            _Qry &= vbCrLf & "  and FNHSysCmpId=" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString))
            _Qry &= vbCrLf & "  order by FTOfMonth desc"

            Dim _AmtBFAdj As Double = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")

            If dt.Rows.Count > 0 Then
                Dim row As System.Data.DataRow
                row = dt.NewRow
                For Each c As DataColumn In dt.Columns
                    Try
                        Select Case c.ColumnName.ToString
                            Case "FNStateRow"
                                row.Item(c.ColumnName.ToString) = 102
                            Case "FNNetProfit"
                                If _oDt.Rows.Count > 0 Then
                                    row.Item(c.ColumnName.ToString) = _oDt.Rows(0).Item("FNNetProfit") + _AmtBFAdj
                                    _AmtBFF = _oDt.Rows(0).Item("FNNetProfit") + _AmtBFAdj
                                Else

                                    row.Item(c.ColumnName.ToString) = 0
                                End If
                            Case "FNNetProfitPer"
                                row.Item(c.ColumnName.ToString) = Me.ProfitBF_lbl.Text
                        End Select
                    Catch ex As Exception
                        '  row.Item(c.FieldName.ToString) = 0
                    End Try
                Next
                dt.Rows.Add(row)
            End If
            dt.AcceptChanges()

            _Qry = "Select sum( Z.FNNetProfit) AS FNNetProfit ,'กำไรขาดทุน สุทธิ' AS FNNetProfitPer"
            _Qry &= vbCrLf & "From (Select FNHSysCmpId , FTOrderNo ,  max(X.FNNetProfit) AS FNNetProfit ,'" & Me.ProfitNet_lbl.Text & "' AS FNNetProfitPer"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_JobCostSummary_Branch AS X"
            _Qry &= vbCrLf & " WHERE X.FTInvoiceNo <> '' "
            _Qry &= vbCrLf & " AND X.FNHSysCmpId =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & " AND LEFT(X.FDInvoiceDate,4) = LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',4) "
            _Qry &= vbCrLf & " AND LEFT(X.FDInvoiceDate,7) <= LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
            _Qry &= vbCrLf & " Group by FNHSysCmpId , FTOrderNo ) AS Z "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            If dt.Rows.Count > 0 Then
                Dim row As System.Data.DataRow
                row = dt.NewRow

                For Each c As DataColumn In dt.Columns
                    Try
                        Select Case c.ColumnName.ToString
                            Case "FNStateRow"
                                row.Item(c.ColumnName.ToString) = 103
                            Case "FNNetProfit"
                                If _oDt.Rows.Count > 0 Then
                                    'row.Item(c.ColumnName.ToString) = +_AmtAdj + _AmtBF + _AmtBFF
                                    row.Item(c.ColumnName.ToString) = +_AmtAdj + _AmtBFF
                                Else
                                    'row.Item(c.ColumnName.ToString) = _AmtAdj + _AmtBF + _AmtBFF
                                    row.Item(c.ColumnName.ToString) = _AmtAdj + _AmtBFF
                                End If
                            Case "FNNetProfitPer"
                                row.Item(c.ColumnName.ToString) = Me.ProfitNet_lbl.Text
                        End Select
                    Catch ex As Exception
                        '  row.Item(c.FieldName.ToString) = 0
                    End Try
                Next
                dt.Rows.Add(row)
            End If
            dt.AcceptChanges()


            Me.ogdtime.DataSource = dt.Copy
            'Me.oAdvBandedGridView.ExpandAllGroups()
            Me.ogdtime.Refresh()
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
        dt.Dispose()
    End Sub

    Private Sub InitialGridMergCell()
        oAdvBandedGridView.OptionsView.AllowCellMerge = True
        For Each c As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In oAdvBandedGridView.Columns
            Select Case c.FieldName.ToString
                Case "FTInvoiceNo", "FNExportQuantity", "FNExportAmtTHB", "FNExportAmt"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            End Select
        Next
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FNHSysCmpId.Text <> "" Then
            If Me.SFTDateTrans.Text <> "" Then
                Call LoadData()
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1501770001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysCmpId_lbl.Text)
            FNHSysCmpId.Focus()
        End If

    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (StateOpen) Then
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.ocmsave.Visible = True
            Me.ocmload.Visible = True
            Me.ocmSendApprove.Visible = True
            Me.FNHSysCmpId.Properties.Buttons(0).Visible = True
            Me.FNHSysCmpId.ReadOnly = False
            Me.SFTDateTrans.Properties.Buttons(0).Visible = True
            Me.SFTDateTrans.ReadOnly = False
        Else
            Me.FNHSysCmpId.Text = _CmpCall
            Me.SFTDateTrans.DateTime = _Month & "/01"
            Me.ocmsave.Enabled = False
            Me.ocmload.Enabled = False
            Me.ocmSendApprove.Enabled = False
            Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
            Me.FNHSysCmpId.ReadOnly = True
            Me.SFTDateTrans.Properties.Buttons(0).Visible = False
            Me.SFTDateTrans.ReadOnly = True
            LoadData()
        End If
        ' Me.oAdvBandedGridView.OptionsView.ShowAutoFilterRow = False
    End Sub

    Private Sub oAdvBandedGridView_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs)
        Try
            With Me.oAdvBandedGridView

                Select Case e.Column.FieldName
                    Case "FTInvoiceNo", "FNExportQuantity", "FNExportAmtTHB"
                        e.Merge = False
                        e.Handled = True
                    Case Else
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FNHSysCmpId.Text <> "" Then
            If Me.SFTDateTrans.Text <> "" Then
                With New HI.RP.Report
                    Dim FM As String = ""

                    FM = " {V_JobCostSummary_Branch.FNHSysCmpId} =" & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) & " "
                    FM &= vbCrLf & " AND MID({V_JobCostSummary_Branch.FDInvoiceDate},1,7) =MID('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',1,7) "

                    .FormTitle = Me.Text
                    .ReportFolderName = "Account\"
                    .ReportName = "JobCostSummaryByBranch.rpt"
                    .Formular = FM
                    .Preview()

                End With
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1501770001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysCmpId_lbl.Text)
            FNHSysCmpId.Focus()
        End If
    End Sub

    Private Sub oAdvBandedGridView_CellMerge1(sender As Object, e As CellMergeEventArgs) Handles oAdvBandedGridView.CellMerge
        Try
            With Me.oAdvBandedGridView
                If "" & .GetRowCellValue(e.RowHandle2, "FNStateRow").ToString = "99" Then
                    For Each c As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In oAdvBandedGridView.Columns
                        If e.Column.FieldName = c.FieldName.ToString Then
                            e.Merge = False
                            e.Handled = True
                        End If
                    Next
                ElseIf "" & .GetRowCellValue(e.RowHandle2, "FNStateRow").ToString = "101" Or
                    "" & .GetRowCellValue(e.RowHandle2, "FNStateRow").ToString = "102" Or
                    "" & .GetRowCellValue(e.RowHandle2, "FNStateRow").ToString = "103" Then
                    For Each c As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In oAdvBandedGridView.Columns
                        If e.Column.FieldName = c.FieldName.ToString Then
                            e.Merge = False
                            e.Handled = True
                        End If
                    Next

                Else
                    If "" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString Then
                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oAdvBandedGridView_CellValueChanging(sender As Object, e As CellValueChangedEventArgs) Handles oAdvBandedGridView.CellValueChanging
        Try
            With oAdvBandedGridView
                .BeginInit()
                If (.FocusedRowHandle >= 0) Then
                    If "" & .GetRowCellValue(.FocusedRowHandle, "FNStateRow").ToString = "99" Then

                        Select Case .FocusedColumn.FieldName.ToString
                            Case "FNIncomeAfterRawmaterial", "FNWageCost", "FNEtcCost"
                                If ocmsave.Enabled Then
                                    With .FocusedColumn.OptionsColumn
                                        .AllowEdit = True
                                        .ReadOnly = False
                                    End With

                                Else
                                    With .FocusedColumn.OptionsColumn
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End With
                                End If
                            Case Else
                                With .FocusedColumn.OptionsColumn
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With
                        End Select
                    Else
                        With .FocusedColumn.OptionsColumn
                            .AllowEdit = False
                            .ReadOnly = True
                        End With
                    End If
                Else
                    With .FocusedColumn.OptionsColumn
                        .AllowEdit = False
                        .ReadOnly = True
                    End With
                End If
                .EndInit()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private totalSum3 As Double = 0
    Private GrpSum3 As Integer = 0
    Private _RowHandleHold3 As Integer = 0
    Private totalSumProfit As Double = 0

    Private Sub InitTableColorSizeStartValue()
        totalSum3 = 0
        GrpSum3 = 0
        totalSumProfit = 0
        _RowHandleHold3 = 0
    End Sub
    Private Sub oAdvBandedGridView_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles oAdvBandedGridView.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitTableColorSizeStartValue()
            End If
            With oAdvBandedGridView
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNOtherCost"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold3 Or e.RowHandle = 0 Then
                                    If .GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString Then
                                        totalSum3 = totalSum3 + Double.Parse(Val(e.FieldValue.ToString))
                                    End If
                                End If
                                _RowHandleHold3 = e.RowHandle
                            End If
                            e.TotalValue = totalSum3
                        End If
                    Case "FNExportQuantity", "FNExportAmtTHB"

                        If e.IsTotalSummary Then
                            If (e.RowHandle <> _RowHandleHold3 Or e.RowHandle = 0) Then
                                If e.RowHandle = 0 Then
                                    totalSum3 = totalSum3 + Double.Parse(Val(e.FieldValue.ToString))
                                Else
                                    If .GetRowCellValue(e.RowHandle, "FTOrderNo").ToString & "|" & .GetRowCellValue(e.RowHandle, "FTInvoiceNo").ToString <>
                                                             .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString & "|" & .GetRowCellValue(_RowHandleHold3, "FTInvoiceNo").ToString Then
                                        totalSum3 = totalSum3 + Double.Parse(Val(e.FieldValue.ToString))
                                    End If
                                End If
                            End If
                            _RowHandleHold3 = e.RowHandle
                        End If
                        e.TotalValue = totalSum3
                    Case "FNFabricCost", "FNAccessroryCost", "FNFabricAccMinCost", "FNAccFabStockCost", "FNFabricAccStockOtherCost", "FNOtherServiceCost", "FNIncomeAfterRawmaterial", "FNConductedCost" _
                        , "FNWageCost", "FNEmpPrintBranch", "FNExportAmtBF", "FNEmbFacCost", "FNSewCost", "FNEmpPrintSub", "FNImportCost", "FNPrintCost", "FNEmbroideryCost", "FNImportExportCost", "FNProdCost",
                       "FNWagePull", "FNCommissionCost", "FNEtcCost", "FNTransportAirCost", "FNOrderQuantity", "FNExportQuantityTo", "FNExportQuantityOtherMonth", "FNTotalExport", "FNOrderQuantityBal"
                        If e.IsTotalSummary Then
                            If .GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString _
                                Or (e.RowHandle = 0 And _RowHandleHold3 = 0) Then
                                totalSum3 = totalSum3 + Double.Parse(Val(e.FieldValue.ToString))
                            End If
                            _RowHandleHold3 = e.RowHandle
                        End If
                        e.TotalValue = totalSum3
                    Case "FNNetProfit"
                        If e.IsTotalSummary Then
                            If (e.RowHandle <> _RowHandleHold3 Or e.RowHandle = 0) Then
                                If .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "101" Or .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "102" Then
                                    totalSumProfit += +Double.Parse("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold3 = e.RowHandle
                        End If
                        e.TotalValue = totalSumProfit
                        'Case "FNAccessroryCost"
                        '    If e.IsTotalSummary Then
                        '        If .GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString _
                        '            Or (e.RowHandle = 0 And _RowHandleHold3 = 0) Then
                        '            totalSum3 = totalSum3 + Double.Parse(Val(e.FieldValue.ToString))
                        '        End If
                        '        _RowHandleHold3 = e.RowHandle
                        '    End If
                        '    e.TotalValue = totalSum3
                        'Case "FNFabricAccMinCost"
                        '    If e.IsTotalSummary Then
                        '        If .GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString _
                        '            Or (e.RowHandle = 0 And _RowHandleHold3 = 0) Then
                        '            totalSum3 = totalSum3 + Double.Parse(Val(e.FieldValue.ToString))
                        '        End If
                        '        _RowHandleHold3 = e.RowHandle
                        '    End If
                        '    e.TotalValue = totalSum3

                        'Case "FNAccFabStockCost"
                        '    If e.IsTotalSummary Then
                        '        If .GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString _
                        '            Or (e.RowHandle = 0 And _RowHandleHold3 = 0) Then
                        '            totalSum3 = totalSum3 + Double.Parse(Val(e.FieldValue.ToString))
                        '        End If
                        '        _RowHandleHold3 = e.RowHandle
                        '    End If
                        '    e.TotalValue = totalSum3
                End Select
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub oAdvBandedGridView_FocusedColumnChanged(sender As Object, e As FocusedColumnChangedEventArgs) Handles oAdvBandedGridView.FocusedColumnChanged
        Try

            With oAdvBandedGridView
                .BeginInit()
                If (.FocusedRowHandle >= 0) Then
                    If "" & .GetRowCellValue(.FocusedRowHandle, "FNStateRow").ToString = "99" Then

                        Select Case e.FocusedColumn.FieldName.ToString
                            Case "FNIncomeAfterRawmaterial", "FNWageCost", "FNEtcCost"
                                If ocmsave.Enabled Then
                                    With e.FocusedColumn.OptionsColumn
                                        .AllowEdit = True
                                        .ReadOnly = False
                                    End With

                                Else
                                    With e.FocusedColumn.OptionsColumn
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End With
                                End If
                            Case Else
                                With e.FocusedColumn.OptionsColumn
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With
                        End Select
                    Else
                        With e.FocusedColumn.OptionsColumn
                            .AllowEdit = False
                            .ReadOnly = True
                        End With
                    End If
                Else
                    With .FocusedColumn.OptionsColumn
                        .AllowEdit = False
                        .ReadOnly = True
                    End With
                End If
                .EndInit()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub oAdvBandedGridView_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles oAdvBandedGridView.FocusedRowChanged
        Try


            With oAdvBandedGridView
                .BeginInit()
                If (.FocusedRowHandle >= 0) Then
                    If "" & .GetRowCellValue(.FocusedRowHandle, "FNStateRow").ToString = "99" Then

                        Select Case .FocusedColumn.FieldName.ToString
                            Case "FNIncomeAfterRawmaterial", "FNWageCost", "FNEtcCost"
                                If ocmsave.Enabled Then
                                    With .FocusedColumn.OptionsColumn
                                        .AllowEdit = True
                                        .ReadOnly = False
                                    End With

                                Else
                                    With .FocusedColumn.OptionsColumn
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End With
                                End If
                            Case Else
                                With .FocusedColumn.OptionsColumn
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With
                        End Select
                    Else
                        With .FocusedColumn.OptionsColumn
                            .AllowEdit = False
                            .ReadOnly = True
                        End With
                    End If
                Else
                    With .FocusedColumn.OptionsColumn
                        .AllowEdit = False
                        .ReadOnly = True
                    End With
                End If
                .EndInit()
            End With

        Catch ex As Exception
        End Try
    End Sub



    Private Sub oAdvBandedGridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles oAdvBandedGridView.RowCellStyle
        Try

            If (e.RowHandle >= 0) Then

                With oAdvBandedGridView
                    If "" & .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "99" Then

                        Select Case e.Column.FieldName.ToString
                            Case "FNIncomeAfterRawmaterial", "FNWageCost", "FNEtcCost"
                                If ocmsave.Enabled Then
                                    'With .Columns(.FocusedColumn.FieldName.ToString).OptionsColumn
                                    '    .AllowEdit = True
                                    '    .ReadOnly = False
                                    'End With

                                    e.Appearance.BackColor = Color.LightCyan
                                    e.Appearance.ForeColor = Color.Blue
                                    e.Appearance.Font = New Font("Tahoma", 10, FontStyle.Bold)
                                    'Else
                                    '    e.Appearance.BackColor = Color.Salmon
                                    '    e.Appearance.BackColor2 = Color.SeaShell
                                    '    e.Appearance.ForeColor = Color.Black
                                    '    e.Appearance.Font = New Font("Tahoma", 10, FontStyle.Bold)
                                End If
                                'Case Else
                                '    e.Appearance.BackColor = Color.Salmon
                                '    e.Appearance.BackColor2 = Color.SeaShell
                                '    e.Appearance.ForeColor = Color.Black
                                '    e.Appearance.Font = New Font("Tahoma", 10, FontStyle.Bold)
                        End Select
                    End If
                    If "" & .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "101" Or "" & .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "102" Then
                        e.Appearance.BackColor = Color.DarkGray
                    End If
                End With
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Function SaveDataMoreDetail() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            For Each R As DataRow In _oDt.Select("FNStateRow =99")
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Additional "
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNIncomeAfter=" & Double.Parse("0" & R!FNIncomeAfterRawmaterial.ToString)
                _Cmd &= vbCrLf & ",FNWage=" & Double.Parse("0" & R!FNWageCost.ToString)
                _Cmd &= vbCrLf & ",FNProdutionCost=" & Double.Parse("0" & R!FNEtcCost.ToString)
                _Cmd &= vbCrLf & "WHERE FNHSysCmpId=" & CInt("0" & Me.FNHSysCmpId.Properties.Tag)
                _Cmd &= vbCrLf & "And FTOfMonth='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text), 7) & "'"
                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Additional "
                    _Cmd &= " (FTInsUser, FDInsDate, FTInsTime,FNHSysCmpId, FTOfMonth, FNIncomeAfter, FNWage, FNProdutionCost)"
                    _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysCmpId.Properties.Tag)
                    _Cmd &= vbCrLf & ",'" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text), 7) & "'"
                    _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNIncomeAfterRawmaterial.ToString)
                    _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNWageCost.ToString)
                    _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNEtcCost.ToString)
                    If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            Next
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
        If Me.FNHSysCmpId.Text <> "" And Me.SFTDateTrans.Text <> "" Then
            If Me.FTStateSendApp.Checked Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถบันทึกได้ข้อมูลได้เนื่องจากมีการ ส่งอนุมัติแล้ว กรุณาตรวจสอบ !!!", 1701091341, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            If SaveDataMoreDetail() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub RepFNIncomeAfterRawmaterial_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNIncomeAfterRawmaterial.EditValueChanging
        Try
            Dim _Incomesum As Double = 0
            Dim _NetProfit As Double = 0
            With oAdvBandedGridView
                If .FocusedRowHandle >= 0 Then
                    If "" & .GetRowCellValue(.FocusedRowHandle, "FNStateRow").ToString = "99" Then

                        Select Case .FocusedColumn.FieldName.ToString
                            Case "FNIncomeAfterRawmaterial", "FNWageCost", "FNEtcCost"
                                If ocmsave.Enabled Then
                                    e.Cancel = False
                                    .SetFocusedRowCellValue("FNIncomeAfterRawmaterial", e.NewValue)

                                    Dim _ProdCost As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNEtcCost").ToString)
                                    Dim _Income As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNWageCost").ToString)
                                    .SetRowCellValue(.FocusedRowHandle, "FNNetProfit", (e.NewValue + _ProdCost + _Income))


                                    _Incomesum = 0
                                    _NetProfit = 0
                                    Dim _OrderHold As String = ""
                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='0'")
                                        If _OrderHold = "" Or _OrderHold <> r!FTOrderNo.ToString Then
                                            _Incomesum += +r!FNIncomeAfterRawmaterial.ToString
                                            _NetProfit += +r!FNNetProfit.ToString
                                        End If
                                        _OrderHold = r!FTOrderNo.ToString
                                    Next


                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='101'")
                                        ' r!FNIncomeAfterRawmaterial = (Double.Parse(r!FNIncomeAfterRawmaterial.ToString) + e.NewValue) + e.OldValue
                                        r!FNIncomeAfterRawmaterial = _Incomesum + e.NewValue

                                        r!FNNetProfit = _NetProfit + ((e.NewValue + _ProdCost + _Income))
                                    Next


                                    Dim _bf As Double = 0
                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='102'")
                                        Try
                                            _bf = Double.Parse(r!FNNetProfit.ToString)
                                        Catch ex As Exception
                                            _bf = 0
                                        End Try
                                    Next


                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='103'")
                                        r!FNNetProfit = (_bf + _NetProfit) + ((e.NewValue + _ProdCost + _Income))
                                        ' r!FNNetProfit = (_bf) + ((e.NewValue + _ProdCost + _Income))
                                    Next
                                    CType(ogdtime.DataSource, DataTable).AcceptChanges()
                                Else
                                    e.Cancel = True
                                End If
                            Case Else
                                e.Cancel = True
                        End Select
                    End If
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFNProdCost_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNProdCost.EditValueChanging
        Try
            Dim _EtcCostSum As Double = 0
            Dim _NetProfit As Double = 0
            With oAdvBandedGridView
                If .FocusedRowHandle >= 0 Then
                    If "" & .GetRowCellValue(.FocusedRowHandle, "FNStateRow").ToString = "99" Then

                        Select Case .FocusedColumn.FieldName.ToString
                            Case "FNIncomeAfterRawmaterial", "FNWageCost", "FNEtcCost"
                                If ocmsave.Enabled Then

                                    e.Cancel = False
                                    .SetFocusedRowCellValue("FNEtcCost", e.NewValue)

                                    Dim _ProdCost As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNWageCost").ToString)
                                    Dim _Income As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNIncomeAfterRawmaterial").ToString)

                                    .SetRowCellValue(.FocusedRowHandle, "FNNetProfit", ((_ProdCost + _Income) + e.NewValue))


                                    _EtcCostSum = 0
                                    _NetProfit = 0
                                    Dim _OrderHold As String = ""
                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='0'")
                                        If _OrderHold = "" Or _OrderHold <> r!FTOrderNo.ToString Then
                                            _EtcCostSum += +r!FNEtcCost
                                            _NetProfit += +r!FNNetProfit.ToString
                                        End If
                                        _OrderHold = r!FTOrderNo.ToString
                                    Next

                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='101'")

                                        '    r!FNEtcCost = (Double.Parse(r!FNEtcCost.ToString) + e.NewValue)
                                        r!FNEtcCost = _EtcCostSum + e.NewValue
                                        r!FNNetProfit = _NetProfit + ((e.NewValue + _ProdCost + _Income))

                                    Next


                                    Dim _bf As Double = 0
                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='102'")
                                        Try
                                            _bf = Double.Parse(r!FNNetProfit.ToString)
                                        Catch ex As Exception
                                            _bf = 0
                                        End Try
                                    Next


                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='103'")
                                        r!FNNetProfit = (_bf + _NetProfit) + ((e.NewValue + _ProdCost + _Income))
                                        '  r!FNNetProfit = (_bf) + ((e.NewValue + _ProdCost + _Income))
                                    Next

                                    'For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='103'")
                                    '    r!FNNetProfit = Double.Parse(r!FNNetProfit.ToString) + ((_ProdCost + _Income) + e.NewValue)
                                    'Next

                                    CType(ogdtime.DataSource, DataTable).AcceptChanges()

                                Else
                                    e.Cancel = True
                                End If
                            Case Else
                                e.Cancel = True
                        End Select
                    End If
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFNWageCost_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNWageCost.EditValueChanging
        Try
            Dim _WageCost As Double = 0
            Dim _NetProfit As Double = 0
            With oAdvBandedGridView
                If .FocusedRowHandle >= 0 Then
                    If "" & .GetRowCellValue(.FocusedRowHandle, "FNStateRow").ToString = "99" Then

                        Select Case .FocusedColumn.FieldName.ToString
                            Case "FNIncomeAfterRawmaterial", "FNWageCost", "FNEtcCost"
                                If ocmsave.Enabled Then
                                    e.Cancel = False
                                    .SetFocusedRowCellValue("FNWageCost", e.NewValue)
                                    Dim _ProdCost As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNEtcCost").ToString)
                                    Dim _Income As Double = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNIncomeAfterRawmaterial").ToString)
                                    .SetRowCellValue(.FocusedRowHandle, "FNNetProfit", (e.NewValue + _ProdCost + _Income))

                                    _WageCost = 0
                                    _NetProfit = 0
                                    Dim _OrderHold As String = ""
                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='0'")
                                        If _OrderHold = "" Or _OrderHold <> r!FTOrderNo.ToString Then
                                            _WageCost += +r!FNWageCost
                                            _NetProfit += +r!FNNetProfit.ToString
                                        End If
                                        _OrderHold = r!FTOrderNo.ToString
                                    Next


                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='101'")
                                        'r!FNWageCost = (Double.Parse(r!FNWageCost.ToString) + e.NewValue) + e.OldValue
                                        r!FNWageCost = _WageCost + e.NewValue
                                        r!FNNetProfit = _NetProfit + ((e.NewValue + _ProdCost + _Income))
                                    Next

                                    Dim _bf As Double = 0
                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='102'")
                                        Try
                                            _bf = Double.Parse(r!FNNetProfit.ToString)
                                        Catch ex As Exception
                                            _bf = 0
                                        End Try
                                    Next


                                    For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='103'")
                                        r!FNNetProfit = (_bf + _NetProfit) + ((e.NewValue + _ProdCost + _Income))
                                        '  r!FNNetProfit = (_bf) + ((e.NewValue + _ProdCost + _Income))
                                    Next

                                    'For Each r As DataRow In CType(ogdtime.DataSource, DataTable).Select("FNStateRow='103'")
                                    '    r!FNNetProfit = Double.Parse(r!FNNetProfit.ToString) + ((e.NewValue + _ProdCost + _Income))
                                    'Next
                                    CType(ogdtime.DataSource, DataTable).AcceptChanges()
                                Else
                                    e.Cancel = True
                                End If
                            Case Else
                                e.Cancel = True
                        End Select
                    End If
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub




    Private Sub oAdvBandedGridView_RowStyle(sender As Object, e As RowStyleEventArgs) Handles oAdvBandedGridView.RowStyle
        Try

            If (e.RowHandle >= 0) Then

                With oAdvBandedGridView
                    Select Case True
                        Case "" & .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "99"
                            e.Appearance.BackColor = Color.Salmon
                            e.Appearance.BackColor2 = Color.SeaShell
                            e.Appearance.ForeColor = Color.Black
                            e.Appearance.Font = New Font("Tahoma", 10, FontStyle.Bold)
                        Case "" & .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "101"
                            e.Appearance.BackColor = Color.LightGray
                            e.Appearance.ForeColor = Color.Black
                            e.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
                        Case "" & .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "102"
                            e.Appearance.BackColor = Color.LightSlateGray
                            e.Appearance.ForeColor = Color.Black
                            e.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
                        Case "" & .GetRowCellValue(e.RowHandle, "FNStateRow").ToString = "103"
                            e.Appearance.BackColor = Color.DarkOrange
                            e.Appearance.ForeColor = Color.Black
                            e.Appearance.Font = New Font("Tahoma", 9, FontStyle.Bold)
                    End Select


                End With
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub oAdvBandedGridView_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles oAdvBandedGridView.ShowingEditor
        Try
            With oAdvBandedGridView
                If .FocusedRowHandle >= 0 Then
                    If "" & .GetRowCellValue(.FocusedRowHandle, "FNStateRow").ToString = "99" Then
                        e.Cancel = False
                    Else
                        e.Cancel = True
                    End If
                Else
                    e.Cancel = True
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpId.EditValueChanged
        Try
            Me.ogdtime.DataSource = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SFTDateTrans_EditValueChanged(sender As Object, e As EventArgs) Handles SFTDateTrans.EditValueChanged
        Try
            Me.ogdtime.DataSource = Nothing
            Me.FTStateSendApp.Checked = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmSendApprove_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
        Try
            If Me.FTStateSendApp.Checked Then
                Exit Sub
            End If
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            With DirectCast(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _oDt.Rows
                If R!FTInvoiceNo.ToString = "" And R!FTOrderNo.ToString = "" Then Exit For

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & ", FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & ",FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' and  Isnull(FTStateSendApp,'') <>'1' "

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End If

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & ", FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & ",FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Qry &= vbCrLf & " And FTInvoiceNo='" & HI.UL.ULF.rpQuoted("" & R!FTInvoiceNo.ToString) & "'"
                '_Qry &= vbCrLf & " And FNPrice=" & Double.Parse("0" & R!FNPrice.ToString)

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & ", FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & ",FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " WHERE FTOrderNoRef='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Qry &= vbCrLf & " And FTInvoiceNo='" & HI.UL.ULF.rpQuoted("" & R!FTInvoiceNo.ToString) & "'"
                '_Qry &= vbCrLf & " And FNPrice=" & Double.Parse("0" & R!FNPrice.ToString)

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If
                Call Sendmail(R!FTOrderNo.ToString)
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Me.FTStateSendApp.Checked = True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        End Try
    End Sub

    Private Function Sendmail(OrderNo As String) As Boolean
        'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MAIL)
        'HI.Conn.SQLConn.SqlConnectionOpen()
        'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            Dim _Cmd As String = ""
            Dim _FTMailId As Long

            _Cmd = "Select Isnull(Isnull(Isnull(Isnull(P.FTUserNameChk,P.FTUserNameMngFac),P.FTUserNameMngFac),P.FTUserNameApp),P.FTUserNameDirector) AS FTUserName"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMPostAccount AS P WITH(NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON P.FNHSysCmpId = O.FNHSysCmpId "
            _Cmd &= vbCrLf & "Where O.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
            Dim _SendTo As String = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_MAIL, "0")

            _FTMailId = HI.TL.RunID.GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _SendTo & "'"
            _Cmd &= ",'Please Check and Approved  Order Costing ','Dear Sir , " & vbCrLf & " Please Check and Approved Order Cost Transaction  OrderNo " & OrderNo & "' ,0,1,0,0,0,0,"
            _Cmd &= "'" & HI.ST.SysInfo.DirectorName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.SysInfo.DirectorName & "',1)"

            HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function


End Class
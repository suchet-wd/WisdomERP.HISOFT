Public Class wJobCostAll
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
    Private _StateSaveJobType As Integer = 0
    Private FTOrderNo As String = ""

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _Finish As Boolean = False
            Dim _FNOrderQuantity, _FNExportQty As Integer
            Dim FNExportGAmt_, FNExportAmt_, FNDebitAmt_ As Double
            If Me.FNHSysCmpId.Text <> "" And SFTDateTrans.Text <> "" Then

                _Qry = "    Select  *  From TACCTJobCost_Monthly As M with(nolock)  "
                _Qry &= vbCrLf & "Where FTInvoiceMonth = LEFT('" & HI.UL.ULDate.ConvertEnDB("01/" & Me.SFTDateTrans.Text) & "',7) "
                _Qry &= vbCrLf & " And FTOrderNo In (Select FTOrderNo From [HITECH_MERCHAN].dbo.TMERTOrder With(nolock ) Where FNHSysCmpId = " & Integer.Parse(Me.FNHSysCmpId.Properties.Tag) & ")"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                For Each R As DataRow In _oDt.Rows
                    FTOrderNo = R!FTOrderNo.ToString
                    _Finish = HI.Conn.SQLConn.GetField(" select FTStateFinish From TACCTJobCost where FTOrderNo='" & FTOrderNo & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "") = "1"

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
                    _Qry &= vbCrLf & "   WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "'"
                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
                    For Each Rx As DataRow In _dt.Rows
                        _FNExportQty = Val(Rx!FNExportQty.ToString)
                        FNExportAmt_ = Double.Parse(Rx!FNExportAmt.ToString())
                        FNExportGAmt_ = Double.Parse(Rx!FNExportGAmt.ToString())
                        FNDebitAmt_ = Double.Parse(Rx!FNDebitAmt.ToString())
                    Next

                    _Qry = " 	SELECT SUM(FNQuantity) AS FNGrandQuantity"
                    _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS Sub WITH(NOLOCK)"
                    _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo ='" & FTOrderNo & "'"
                    _FNOrderQuantity = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "")

                    Dim _FTOrderNoRef As String
                    _Qry = " Select TOP 1 ISNULL(FTOrderNoRef,'') AS FTOrderNoRef "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "'"
                    _FTOrderNoRef = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")
                    If _FTOrderNoRef = "" Then
                        allCalculateCostData(FTOrderNo, _Finish, _FNOrderQuantity, _FNExportQty, FNExportAmt_, FNExportGAmt_, FNDebitAmt_)
                    End If

                Next

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub allCalculateCostData(OrderKey As String, FTStateFinish_ As Boolean, FNOrderQuantity_ As Integer, FNExportQty_ As Integer, FNExportAmt_ As Double, FNExportGAmt_ As Double, FNDebitAmt_ As Double)
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

                If Rind = 1 And FTStateFinish_ Then

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

                If Rind = 1 And FTStateFinish_ Then
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
                If Rind = 1 And FTStateFinish_ Then
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

                    _FNOrderQty = FNOrderQuantity_
                    FNTotalExportQty = FNExportQty_
                    FNTotalExportAmt = FNExportAmt_

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

                    FNTotalExportQty = FNOrderQuantity_  ' FNExportQty.Value
                    FNTotalExportAmt = FNExportGAmt_


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

                            If _FNFabricCost > 0 And (FNMonthSeq <> FNLastMonthSeq Or FTStateFinish_ = False) Then
                                _FNFabricCost = Double.Parse(Format(((_FNFabricCost * _FNExportQty) / (FNTotalExportQty - FNTotalExportBFQty)), "0.00"))
                            End If

                            If _FNWagePull > 0 And (FNMonthSeq <> FNLastMonthSeq Or FTStateFinish_ = False) Then
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
                            'TotalFNWagePull = TotalFNWagePull + _FNWagePull
                            TotalFNWagePull = _FNWagePull
                            'Modift by Num 20161004 Request By Pung Change  to Fix by Month

                            TotalFNFabricCost = TotalFNFabricCost + _FNFabricCost
                            TotalFNAccessroryCost = TotalFNAccessroryCost + _FNAccessroryCost
                            TotalFNAccFabStockCost = TotalFNAccFabStockCost + _FNAccFabStockCost
                            TotalFNServiceCost = TotalFNServiceCost + _FNServiceCost

                            'Modift by Num 20161004 Request By Pung Change to Calculate By Export

                            TotalFNOtherCost = TotalFNOtherCost + _FNOtherCost
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
                            'TotalFNConductedCost = TotalFNConductedCost + _FNConductedCost
                            'TotalFNCommissionCost = TotalFNCommissionCost + _FNCommissionCost
                            'TotalFNOtherCost = TotalFNOtherCost + _FNOtherCost

                            TotalFNOtherCost = _FNOtherCost
                            'TotalFNEmbSubCost = _FNEmbSubCost
                            'TotalFNPrintSubCost = _FNPrintSubCost
                            'TotalFNEmbBranchCost = _FNEmbBranchCost
                            'TotalFNPrintBranchCost = _FNPrintBranchCost
                            'TotalFNEmbFacCost = _FNEmbFacCost
                            TotalFNConductedCost = _FNConductedCost
                            TotalFNCommissionCost = _FNCommissionCost



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


                        If FNMonthSeq = FNLastMonthSeq And FTStateFinish_ = True Then

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
                            TotalFNOtherCost = TotalFNOtherCost + _GFNOtherCost
                            TotalFNAccFabStockOtherCost = TotalFNAccFabStockOtherCost + _GFNAccFabStockOtherCost
                            TotalFNFabricRcvCost = TotalFNFabricRcvCost + _GFNFabricRcvCost
                            TotalFNAccessroryRcvCost = TotalFNAccessroryRcvCost + _GFNAccessroryRcvCost
                            TotalFNFabricDummyCost = TotalFNFabricDummyCost + _GFNFabricDummyCost
                            TotalFNAccessroryDummyCost = TotalFNAccessroryDummyCost + _GFNAccessroryDummyCost
                            TotalFNAccFabStockRcvCost = TotalFNAccFabStockRcvCost + _GFNAccFabStockRcvCost
                            TotalFNAccFabStockOtherRcvCost = TotalFNAccFabStockOtherRcvCost + _GFNAccFabStockOtherRcvCost
                            TotalFNWageCutSew = TotalFNWageCutSew + _GFNWageCutSew
                            TotalFNWageCutTrim = TotalFNWageCutTrim + _GFNWageCutTrim

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

                        If FNMonthSeq = FNLastMonthSeq And FTStateFinish_ = True Then

                            _Str &= vbCrLf & ", FNDebitAmt=" & FNDebitAmt_ & ""

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




        Catch ex As Exception
        End Try

    End Sub





End Class
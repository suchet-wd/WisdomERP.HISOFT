Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Base

Public Class wOrderCMProduction

    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private oGridViewBreakdownDivertSrc As DevExpress.XtraGrid.Views.Grid.GridView
    Private _wDivertOrderDistination As wCMOrderDistination

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Sub New()
        InitializeComponent()
        _FormLoad = True
        Call PrepareDetailBreakDown()
    End Sub

    Public Sub LoadDataInfo(OrderKey As String, OrderSubKey As String)

        Me.FTOrderNo.Text = OrderKey
        Me.FTSubOrderNo.Text = OrderSubKey

        Try
            If Me.FTOrderNo.Text = "" Then
                HI.TL.HandlerControl.ClearControl(Me)
                Call FormRefresh()
            Else
                Call BrowseDataHeader()
                Call CheckStateOrder()
            End If
        Catch ex As Exception
        End Try

        Me.FTSubOrderNo.Text = OrderSubKey
        Try
            If Me.FTSubOrderNo.Text = "" Then
                Me.ogdDivertSrc.DataSource = Nothing
                Me.ogcDetailBreakDown.DataSource = Nothing
            Else
                Call BrowseSubOrderData()
                Call CheckStateSubOrder()
                Call LoadDetailSubDivertBreakDown()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub CheckStateOrder()

        FTStateMRP.Checked = False
        FTStateReserve.Checked = False
        FTStateSourcing.Checked = False
        FTStatePurchase.Checked = False
        FTStateReceive.Checked = False
        FTStateAdjust.Checked = False
        FTStateTransferIn.Checked = False
        FTStateTransferOut.Checked = False
        FTStateProduction.Checked = False
        Me.FTStateCutting.Checked = False
        Me.FTStateSewing.Checked = False
        Me.FTStatePacking.Checked = False
        Me.FTStateTransferWH.Checked = False

        If Me.FTOrderNo.Text.Trim = "" Then Exit Sub

        Dim _dt As DataTable
        Dim _Qry As String = ""

        Dim _OrderNo As String = Me.FTOrderNo.Text

        _Qry = "SELECT  ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateMRP"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateReserve"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateSourcing"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStatePurchase"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateReceive"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateAdjust"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) WHERE FTOrderNoTo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateTransferIn"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateTransferOut"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateProduction"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS A WITH(NOLOCK) INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK) ON A.FTTransferWHNo = B.FTDocumentNo  WHERE B.FTOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNo) & "')),'0') AS FTStateTransferWH"
        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'), '0') AS FTStateCutting"

        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C (NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo"
        _Qry &= vbCrLf & "	 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS D (NOLOCK) ON A.FNHSysUnitSectId = D.FNHSysUnitSectId"
        _Qry &= vbCrLf & "   WHERE D.FTStateActive = N'1'"
        _Qry &= vbCrLf & "   AND D.FTStateSew = N'1'"
        _Qry &= vbCrLf & "   AND C.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'), '0') AS FTStateSewing"
        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B (NOLOCK) ON A.FTPackNo = B.FTPackNo"
        _Qry &= vbCrLf & "   WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'), '0') AS FTStatePacking"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dt.Rows

            FTStateMRP.Checked = (R!FTStateMRP.ToString = "1")
            FTStateReserve.Checked = (R!FTStateReserve.ToString = "1")
            FTStateSourcing.Checked = (R!FTStateSourcing.ToString = "1")
            FTStatePurchase.Checked = (R!FTStatePurchase.ToString = "1")
            FTStateReceive.Checked = (R!FTStateReceive.ToString = "1")
            FTStateAdjust.Checked = (R!FTStateAdjust.ToString = "1")
            FTStateTransferIn.Checked = (R!FTStateTransferIn.ToString = "1")
            FTStateTransferOut.Checked = (R!FTStateTransferOut.ToString = "1")
            FTStateProduction.Checked = (R!FTStateProduction.ToString = "1")
            Me.FTStateCutting.Checked = (R!FTStateCutting.ToString = "1")
            Me.FTStateSewing.Checked = (R!FTStateSewing.ToString = "1")
            Me.FTStatePacking.Checked = (R!FTStatePacking.ToString = "1")
            FTStateTransferWH.Checked = (R!FTStateTransferWH.ToString = "1")
            Exit For

        Next

        If Not (FTStateProduction.Checked) Then

            _Qry = " SELECT Max(FTStateCut) AS FTStateCut, MAX(FTStateSew) AS FTStateSew, MAX(FTStatePack) AS FTStatePack "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each Rx As DataRow In _dt.Rows

                FTStateProduction.Checked = (Rx!FTStateCut.ToString = "1")
                Me.FTStateCutting.Checked = (Rx!FTStateCut.ToString = "1")
                Me.FTStateSewing.Checked = (Rx!FTStateSew.ToString = "1")
                Me.FTStatePacking.Checked = (Rx!FTStatePack.ToString = "1")

                Exit For

            Next

        End If

        _dt.Dispose()

    End Sub

    Private Sub CheckStateSubOrder()

        FTStateSubMRP.Checked = False
        FTStateSubProduction.Checked = False
        Me.FTStateSubCutting.Checked = False
        Me.FTStateSubSewing.Checked = False
        Me.FTStateSubPacking.Checked = False

        If Me.FTSubOrderNo.Text.Trim = "" Then Exit Sub

        Dim _dt As DataTable
        Dim _Qry As String = ""

        Dim _OrderNo As String = Me.FTOrderNo.Text
        Dim _OrderNoSub As String = Me.FTSubOrderNo.Text

        _Qry = "SELECT  ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_OrderNoSub) & "')),'0') AS FTStateSubMRP"
        _Qry &= vbCrLf & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A WITH(NOLOCK) WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNoSub) & "')),'0') AS FTStateSubProduction"
        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS A (NOLOCK) INNER JOIN  "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
        _Qry &= vbCrLf & "  AND A.FTOrderNo = B.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
        _Qry &= vbCrLf & "	AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNoSub) & "'"
        _Qry &= vbCrLf & "	), '0') AS FTStateSubCutting"

        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A (NOLOCK) "
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C (NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D (NOLOCK) ON C.FTOrderNo = D.FTOrderNo"
        _Qry &= vbCrLf & "AND C.FTOrderProdNo = D.FTOrderProdNo"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS E (NOLOCK) ON A.FNHSysUnitSectId = E.FNHSysUnitSectId"
        _Qry &= vbCrLf & "WHERE E.FTStateActive = N'1'"
        _Qry &= vbCrLf & "AND E.FTStateSew = N'1'"
        _Qry &= vbCrLf & "AND C.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
        _Qry &= vbCrLf & "AND D.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNoSub) & "'), '0') AS FTStateSubSewing"
        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B (NOLOCK) ON A.FTPackNo = B.FTPackNo"
        _Qry &= vbCrLf & "WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "'"
        _Qry &= vbCrLf & "AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text.Trim) & "'), '0') AS FTStateSubPacking"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


        For Each R As DataRow In _dt.Rows

            FTStateSubMRP.Checked = (R!FTStateSubMRP.ToString = "1")
            FTStateSubProduction.Checked = (R!FTStateSubProduction.ToString = "1")
            Me.FTStateSubCutting.Checked = (R!FTStateSubCutting.ToString = "1")
            Me.FTStateSubSewing.Checked = (R!FTStateSubSewing.ToString = "1")
            Me.FTStateSubPacking.Checked = (R!FTStateSubPacking.ToString = "1")

            Exit For

        Next

        If Not (FTStateSubProduction.Checked) Then

            _Qry = " SELECT Max(FTStateCut) AS FTStateCut, MAX(FTStateSew) AS FTStateSew, MAX(FTStatePack) AS FTStatePack "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTOrderNo=N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & "  AND FTSubOrderNo=N'" & HI.UL.ULF.rpQuoted(_OrderNoSub) & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            For Each Rx As DataRow In _dt.Rows
                FTStateSubProduction.Checked = (Rx!FTStateCut.ToString = "1")
                Me.FTStateSubCutting.Checked = (Rx!FTStateCut.ToString = "1")
                Me.FTStateSubSewing.Checked = (Rx!FTStateSew.ToString = "1")
                Me.FTStateSubPacking.Checked = (Rx!FTStatePack.ToString = "1")
                Exit For
            Next

        End If

        _dt.Dispose()

    End Sub
    Private Sub FormRefresh()

        HI.TL.HandlerControl.ClearControl(Me)

        Me.FTUpdUser.Text = ""
        Me.FTOrderBy.Text = ""

        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Text = ""
        Me.FNHSysCmpId_None.Text = ""

        Me.FNHSysCmpRunId.Properties.Tag = ""
        Me.FNHSysCmpRunId.Text = ""
        Me.FNHSysCmpRunId_None.Text = ""

        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Text = ""
        Me.FNHSysStyleId_None.Text = ""

        HI.TL.HandlerControl.ClearControl(Me.ogbSubOrder)

        Me.FTStateEmb.Checked = False : Me.FTStatePrint.Checked = False : Me.FTStateHeat.Checked = False : Me.FTStateLaser.Checked = False : Me.FTStateWindows.Checked = False

        Me.FTSubOrderNoDivertRef.Visible = False : Me.FTSubOrderNoDivertRef.Checked = False

        Me.FTStateApprovedSubOrderNo.Visible = False
        Me.FTStateApprovedSubOrderNo.Checked = False

        Me.FTStateApprovedSubOrderNoRevised.Visible = False
        Me.FTStateApprovedSubOrderNoRevised.Checked = False



        Call CheckStateOrder()
        Call CheckStateSubOrder()

    End Sub

    Private Sub BrowseDataHeader()
        Dim _Dt As DataTable
        Dim _Qry As String


        _Qry = " SELECT A.[FTUpdUser],A.[FTUpdTime],cmp.FTCmpCode,CmpRun.FTCmpRunCode,a.FTPORef,A.FTStateOrderApp,sty.FTStyleCode"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(A.FDUpdDate)= 1 Then Convert(varchar(10),Convert(Datetime,A.FDUpdDate),103) Else ''END AS FDUpdDate"
        _Qry &= vbCrLf & " FROM HITECH_MERCHAN..TMERTOrder AS A inner join"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS Cmp with(NOLOCK) on A.FNHSysCmpId=cmp.FNHSysCmpId LEfT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS sty with (NOLOCK) on a.FNHSysStyleId=sty.FNHSysStyleId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun AS CmpRun with(NOLOCK) on A.FNHSysCmpRunId=A.FNHSysCmpRunId"
        _Qry &= vbCrLf & "WHERE A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
        _Qry &= vbCrLf & "AND A.FNHSysCmpRunId=CmpRun.FNHSysCmpRunId"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


        For Each R As DataRow In _Dt.Rows
            FTUpdUser.Text = R!FTUpdUser.ToString
            FDUpdDate.Text = R!FDUpdDate.ToString
            FTUpdTime.Text = R!FTUpdTime.ToString
            FNHSysCmpId.Text = R!FTCmpCode.ToString
            FNHSysCmpRunId.Text = R!FTCmpRunCode.ToString
            FTPORef.Text = R!FTPORef.ToString
            FNHSysStyleId.Text = R!FTStyleCode.ToString
            FTStateOrderApp.Checked = R!FTStateOrderApp.ToString = "1"
        Next



    End Sub
    Private Sub BrowseSubOrderData()
        Dim _Dt As DataTable
        Dim _Qry As String

        _Qry = "SELECT TOP 1 CASE WHEN ISDATE(ors.FDSubOrderDate)=1 then convert(varchar(10),convert(Datetime,ors.FDSubOrderDate),103) Else'' END AS FDSubOrderDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(ors.FDProDate)=1 then convert(varchar(10),convert(Datetime,ors.FDProDate),103) Else'' END AS FDProDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(ors.FDShipDate) = 1 Then  Convert(varchar(10),Convert(Datetime,ors.FDShipDate) ,103)  Else ''END AS FDShipDate"
        _Qry &= vbCrLf & ",ors.FTUpdUser"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(ors.FDUpdDate)=1 Then Convert(varchar(10),Convert(Datetime,ors.FDUpdDate),103) Else ''END AS FDUpdDate"
        _Qry &= vbCrLf & ",ors.FTUpdTime"
        _Qry &= vbCrLf & ",coun.FTCountryCode"
        _Qry &= vbCrLf & ",conent.FTContinentCode"
        _Qry &= vbCrLf & ",prv.FTProvinceCode"
        _Qry &= vbCrLf & ",sh.FTShipModeCode"
        _Qry &= vbCrLf & ",shp.FTShipPortCode"
        _Qry &= vbCrLf & ",Cursy.FTCurCode"
        _Qry &= vbCrLf & ",Gender.FTGenderCode"
        _Qry &= vbCrLf & ",UNT.FTUnitCode"
        _Qry &= vbCrLf & ",(convert (int,(Vsub.FNSubOrderQty - (CONVERT(int, Vsub.FNGarmentQtyTest) + CONVERT(int, Vsub.FNAmntExtra))))) AS FNSubOrderQty"
        _Qry &= vbCrLf & ",convert(int,VSub.FNAmntExtra) as FNSubOrderExtraQty"
        _Qry &= vbCrLf & ",Vsub.FNGarmentQtyTest as FNSubOrderGarmentTestQty"
        _Qry &= vbCrLf & ",(convert (decimal(10,2),BD.FNAmntQtyTest)) as FNSubOrderGarmentTestAmnt"
        _Qry &= vbCrLf & ",(convert (decimal(10,2),Vsub.FNQuantityExtra)) as FNSubOrderExtraAmt"
        _Qry &= vbCrLf & ",(convert (decimal(10,2), Vsub.FNAmt)) AS FNSubOrderAmt"

        _Qry &= vbCrLf & ",Vsub.FTStateEmb"
        _Qry &= vbCrLf & ",Vsub.FTStatePrint"
        _Qry &= vbCrLf & ",Vsub.FTStateHeat"
        _Qry &= vbCrLf & ",Vsub.FTStateWindows"

        _Qry &= vbCrLf & ",ISNULL(bg.[FTBuyGrpCode],'') As FTBuyGrpCode"
        _Qry &= vbCrLf & ",ISNULL(pmt.FTPlantCode,'') AS FTPlantCode,ors.FNOrderSetType"


        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS ors LEFT OUTER join"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS conent with(NOLOCK) on ors.FNHSysContinentId=conent.FNHSysContinentId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCountry AS coun with(NOLOCK) on ors.FNHSysCountryId=coun.FNHSysCountryId AND ors.FNHSysContinentId=coun.FNHSysContinentId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince AS prv with(NOLOCK) on coun.FNHSysCountryId=prv.FNHSysCountryId AND ors.FNHSysProvinceId =prv.FNHSysProvinceId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipMode AS SH with(NOLOCK) on ors.FNHSysShipModeId=sh.FNHSysShipModeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipPort AS SHP with(NOLOCK) on ors.FNHSysShipPortId=shp.FNHSysShipPortId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS Cursy with(NOLOCK) on ors.FNHSysCurId=Cursy.FNHSysCurId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMGender AS Gender with(NOLOCK) on ors.FNHSysGenderId=Gender.FNHSysGenderId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnit AS UNT with(NOLOCK) on ors.FNHSysUnitId=UNT.FNHSysUnitId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_TMERTOrderSub AS Vsub with(NOLOCK) on ors.FTOrderNo=Vsub.FTOrderNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown  AS BD with(NOLOCK) on ors.FTSubOrderNo=BD.FTSubOrderNo "
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPlant AS pmt with(NOLOCK) on ors.FNHSysPlantId=pmt.FNHSysPlantId "
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS bg with(NOLOCK) on ors.FNHSysBuyGrpId=bg.FNHSysBuyGrpId "

        '  _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPR aS MPR with(NOLOCK) on ors.FTSubOrderNo=MPR.FTSubOrderNo"
        _Qry &= vbCrLf & "WHERE ors.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _Dt.Rows
            FDSubOrderDate.Text = R!FDSubOrderDate.ToString
            FDProDate.Text = R!FDProDate.ToString
            FDShipDate.Text = R!FDShipDate.ToString
            FNHSysContinentId.Text = R!FTContinentCode.ToString
            FNHSysCountryId.Text = R!FTCountryCode.ToString
            FNHSysProvinceId.Text = R!FTProvinceCode.ToString
            FNHSysShipModeId.Text = R!FTShipModeCode.ToString
            FNHSysShipPortId.Text = R!FTShipPortCode.ToString
            FNHSysCurId.Text = R!FTCurCode.ToString
            FNHSysGenderId.Text = R!FTGenderCode.ToString
            FNHSysUnitId.Text = R!FTUnitCode.ToString
            FTUpdUserSubOrder.Text = R!FTUpdUser.ToString
            FDUpdDate_OrderSub.Text = R!FDUpdDate.ToString
            FTUpdTime_OrderSub.Text = R!FTUpdTime.ToString
            FTStateEmb.Checked = R!FTStateEmb.ToString = "1"
            FTStatePrint.Checked = R!FTStatePrint.ToString = "1"
            FTStateHeat.Checked = R!FTStateHeat.ToString = "1"
            FTStateWindows.Checked = R!FTStateWindows.ToString = "1"

            FNHSysPlantId.Text = R!FTPlantCode.ToString
            FNHSysBuyGrpId.Text = R!FTBuyGrpCode.ToString

            FNOrderSetType.SelectedIndex = Val(R!FNOrderSetType.ToString)
        Next

    End Sub

#Region "Property"

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(value As String)
            _MainKeyID = value
        End Set
    End Property

    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
    End Property

    Private _RequireField As String = ""
    Public Property RequireField As String
        Get
            Return _RequireField
        End Get
        Set(value As String)
            _RequireField = value
        End Set
    End Property

    Public ReadOnly Property Query As String
        Get
            Return _FormHeader(0).Query
        End Get
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region


    Private Sub ocmExit_Click(sender As Object, e As EventArgs) Handles ocmExit.Click
        Me.Close()
    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.ogcDetailBreakDown.DataSource = Nothing
    End Sub

    Public Sub LoadDetailSubDivertBreakDown()
        Dim _dt As DataTable
        Dim _Qry As String = ""

        _Qry = "SELECT AA.FNCMSeq,AA.oFTSubOrderNo,AA.ooFDShipDate"
        _Qry &= vbCrLf & ",AA.oFTContinetName,AA.oFTCountryName"
        _Qry &= vbCrLf & ",AA.oFTProvinceName,AA.oFTShipModenName "
        _Qry &= vbCrLf & ",AA.TotalQuantity,'' FTRemark,AA.FTPORef,AA.FTPOTrading , aa.FTCmpCode  ,aa.FTCmpName , aa.FNCMOperCutting , aa.FNCMOperSew , aa.FNCMOperPack   "
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "(SELECT sd.FTSubOrderNo AS oFTSubOrderNo,SD.FTPORef,sdb.FNCMSeq  ,convert(varchar(10),convert(Datetime,sd.FDShipDate),103) AS ooFDShipDate, Max(SD.FTPORef) As FTPOTrading"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",conti.FTContinentNameTH AS oFTContinetName,cun.FTCountryNameTH AS oFTCountryName  , ct.FTCmpNameTH  as FTCmpName"
            _Qry &= vbCrLf & ",pro.FTProvinceNameTH AS oFTProvinceName,sh.FTShipModenNameTH AS oFTShipModenName --,shp.FTShipPortNameTH AS oFTShipPortName"
        Else
            _Qry &= vbCrLf & ",conti.FTContinentNameEN AS oFTContinetName,cun.FTCountryNameEN AS oFTCountryName  , ct.FTCmpNameEN as FTCmpName"
            _Qry &= vbCrLf & ",pro.FTProvinceNameEN AS oFTProvinceName,sh.FTShipModeNameEN AS oFTShipModenName -- ,shp.FTShipPortNameEN AS oFTShipPortName"
        End If

        _Qry &= vbCrLf & ",sum(sdb.FNQuantity) AS TotalQuantity"
        _Qry &= vbCrLf & ", ct.FTCmpCode , sdb.FNCMOperCutting , sdb.FNCMOperSew , sdb.FNCMOperPack"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_CM_BreakDown AS SDB with(NOLOCK)   inner join  "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_All_Divert AS SD with(NOLOCK) on sd.FTSubOrderNo=sdb.FTSubOrderNo AND sd.FTOrderNo=sdb.FTOrderNo  LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS CONTI with(NOLOCK) on sd.FNHSysContinentId=CONTI.FNHSysContinentId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCountry AS CUN with(NOLOCK) on sd.FNHSysCountryId=cun.FNHSysCountryId AND sd.FNHSysContinentId = cun.FNHSysContinentId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince as Pro with(NOLOCK) on sd.FNHSysProvinceId=pro.FNHSysProvinceId AND  Cun.FNHSysCountryId=Pro.FNHSysCountryId  LEFT OUtER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipMode AS Sh with(NOLOCK) on sd.FNHSysShipModeId=sh.FNHSysShipModeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp as ct on sdb.FNHSysCmpIdTo = ct.FNHSysCmpId "
        _Qry &= vbCrLf & "where sd.FTSubOrderNo='" & Me.FTSubOrderNo.Text & "'"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "GROUP BY sd.FTSubOrderNo,sdb.FNCMSeq  ,sd.FDShipDate "
            _Qry &= vbCrLf & ",conti.FTContinentNameTH,cun.FTCountryNameTH , ct.FTCmpCode , sdb.FNCMOperCutting , sdb.FNCMOperSew , sdb.FNCMOperPack "
            _Qry &= vbCrLf & ",pro.FTProvinceNameTH,sh.FTShipModenNameTH, SD.FTPORef , ct.FTCmpNameTH ) AS AA"
        Else
            _Qry &= vbCrLf & "GROUP BY sd.FTSubOrderNo,sdb.FNCMSeq  ,sd.FDShipDate "
            _Qry &= vbCrLf & ",conti.FTContinentNameEN,cun.FTCountryNameEN , ct.FTCmpCode , sdb.FNCMOperCutting , sdb.FNCMOperSew , sdb.FNCMOperPack "
            _Qry &= vbCrLf & ",pro.FTProvinceNameEN,sh.FTShipModeNameEN, SD.FTPORef , ct.FTCmpNameEN) AS AA"
        End If


        ' To do......
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogdDivertSrc.DataSource = _dt

    End Sub

    Private Function DeleteDivert(Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryChkSDB As String = ""
            Dim _QryChkSD As String = ""
            Dim MaxSeqSDB As String
            Dim MaxSeqSD As String

            With Me.ogvDivertSrc
                MaxSeqSD = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNCMSeq").ToString))
            End With


            Dim SeqSD As Integer
            Dim SeqSDB As Integer
            SeqSDB = MaxSeqSD 'Integer.Parse("0" & MaxSeqSD)
            SeqSD = MaxSeqSD 'Integer.Parse("0" & MaxSeqSD.tp)

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_CM_BreakDown"
            _Qry &= vbCrLf & "WHERE FNCMSeq=" & Val(SeqSDB) & ""
            _Qry &= vbCrLf & "AND FTSubOrderNo='" & Me.FTSubOrderNo.Text & "'"


            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If


            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False

        End Try

    End Function



    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        Try
            If Me.FTOrderNo.Text = "" Then
                HI.TL.HandlerControl.ClearControl(Me)
                Call FormRefresh()
            Else
                Call BrowseDataHeader()
                Call CheckStateOrder()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmDivert_Click(sender As Object, e As EventArgs) Handles ocmCmOrder.Click

        Call CheckStateOrder()
        Call CheckStateSubOrder()

        If Me.FTStateCutting.Checked = True And Me.FTStateSubSewing.Checked = True And Me.FTStatePacking.Checked = True Then

            HI.MG.ShowMsg.mInvalidData("sub order  นี้ ผลิตหมดแล้ว", 2301311138, Me.FTOrderNo.Text)
            Me.FTOrderNo.Focus()
            Exit Sub

        Else



        End If



        If Me.FTOrderNo.Text.Trim <> "" Then

            _wDivertOrderDistination = New wCMOrderDistination()

            With _wDivertOrderDistination

                .FTOrderNoSrc = Me.FTOrderNo.Text.Trim
                .SubOrderSrc = Me.FTSubOrderNo.Text.Trim
                .SubOrderDivertSeq = 0
                .FNOrderSetType.SelectedIndex = FNOrderSetType.SelectedIndex

                'If Me.FTStateCutting.Checked Then
                '    .FTCutting.Properties.ReadOnly = True
                'End If
                'If Me.FTStateSewing.Checked Then
                '    .FTSew.Properties.ReadOnly = True
                'End If

                'If Me.FTStatePacking.Checked Then
                '    .FTPack.Properties.ReadOnly = True
                'End If



                If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                    Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                    Call LoadDetailSubDivertBreakDown()
                    Call PrepareDetailBreakDown(1, Me.FTSubOrderNo.Text)
                End If

            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        End If
    End Sub


    Private Sub ocmDelete_Click(sender As Object, e As EventArgs) Handles ocmDelete.Click

        If FTOrderNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        Else
            If Me.FTSubOrderNo.Text <> "" Then

                Dim MaxSeqSD As Integer
                With Me.ogvDivertSrc
                    MaxSeqSD = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNDivertSeq").ToString))
                End With



                If HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการลบข้อมูลเบรกดาวน์ หรือไม่", 1507161034, "Seq No. " & MaxSeqSD.ToString) Then
                    Dim _Spls As New HI.TL.SplashScreen("Deleting.... Please Wait")
                    If Me.DeleteDivert(_Spls) Then

                        Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Call LoadDetailSubDivertBreakDown()
                        Call PrepareDetailBreakDown()
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                End If


            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาเลือก SubOrderNo", 1507170904, Me.Text)
                FTSubOrderNo.Focus()
            End If
        End If
    End Sub

    Private Sub ocmRefresh_Click(sender As Object, e As EventArgs) Handles ocmRefresh.Click
        Call BrowseDataHeader()
        Call CheckStateOrder()
        Call LoadDetailSubDivertBreakDown()
        Call PrepareDetailBreakDown(1, Me.FTSubOrderNo.Text)
    End Sub

    Private Sub PrepareDetailBreakDown(Optional FNseq As Integer = 0, Optional SubOrder As String = "")
        Dim _Qry As String
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _colcount As Integer = 0

        Try

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..SP_GET_CMBreakDown '" & HI.UL.ULF.rpQuoted(SubOrder.ToString) & "', " & FNseq & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            With Me.ogvDetailBreakDown

                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FNTotal".ToUpper, "FNHSysCmpId".ToUpper, "FTCmpCode".ToUpper, "FNCMSeq".ToUpper, "FTSubOrderNo".ToUpper,
                             "FNCMOperCutting".ToUpper, "FNCMOperSew".ToUpper, "FNCMOperPack".ToUpper,
                             "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper

                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowEdit = False
                            .Columns(I).OptionsColumn.AllowMove = DefaultBoolean.False
                            .Columns(I).OptionsColumn.AllowSort = DefaultBoolean.False

                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next

                If Not (_dt Is Nothing) Then
                    For Each col As DataColumn In _dt.Columns
                        Select Case col.ColumnName.ToString.ToUpper
                            Case "FNCMOperCutting".ToUpper, "FNCMOperSew".ToUpper, "FNCMOperPack".ToUpper, "FNTotal".ToUpper, "FNHSysCmpId".ToUpper, "FTCmpCode".ToUpper, "FNCMSeq".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper
                            Case Else

                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = col.ColumnName.ToString
                                    .Name = col.ColumnName.ToString
                                    .Caption = col.ColumnName.ToString

                                End With

                                .Columns.Add(ColG)



                                With .Columns(col.ColumnName.ToString)
                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                    With .OptionsColumn

                                        .AllowMove = False
                                        .AllowGroup = DefaultBoolean.False
                                        .AllowSort = DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True

                                    End With

                                End With

                                .Columns(col.ColumnName.ToString).Width = 50
                                .Columns(col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                        End Select

                    Next

                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        With GridCol
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With

                    Next

                End If

                .OptionsView.ShowAutoFilterRow = False

            End With

            Me.ogcDetailBreakDown.DataSource = _dt.Copy
            _dt.Dispose()

        Catch ex As Exception
        End Try
    End Sub

    Private Function RemoveGridViewColumn(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraGrid.Views.Grid.GridView
        Try

            With pGridView

                For nLoopColGridView As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(nLoopColGridView).FieldName.ToString.ToUpper
                        Case "FNDivertSeq".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
                            'If pGridView.Name = "ogvDetailBreakDown" Then
                            '    .Columns.Remove(.Columns.Item(nLoopColGridView))
                            'Else
                            'End If

                        Case Else
                            .Columns.Remove(.Columns.Item(nLoopColGridView))
                    End Select

                Next

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

        Return pGridView

    End Function

    Private Sub InitGrid()
        With ogvDetailBreakDown
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With
    End Sub

    Private Sub ogvDivertSrc_Click(sender As Object, e As EventArgs) Handles ogvDivertSrc.Click
        Dim FNseq As Integer
        Dim SubOrder As String
        Dim _Qry As String
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _colcount As Integer = 0
        With ogvDivertSrc
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            FNseq = HI.UL.ULF.rpQuoted("" & .GetRowCellValue(.FocusedRowHandle, "FNCMSeq").ToString)
            SubOrder = HI.UL.ULF.rpQuoted("" & .GetRowCellValue(.FocusedRowHandle, "oFTSubOrderNo").ToString)

            'Call PrepareDetailBreakDown(FNseq, SubOrder)

        End With




        Try
            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..SP_GET_CMBreakDown '" & HI.UL.ULF.rpQuoted(SubOrder.ToString) & "', " & FNseq & ""

            '_Qry = "Exec[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..SP_GET_DivertBreakDown_ChangeColorway '" & HI.UL.ULF.rpQuoted(SubOrder.ToString) & "', " & FNseq & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            Dim _columncount As Integer = 0
            With ogvDetailBreakDown
                .OptionsView.ShowAutoFilterRow = False
                _columncount = .Columns.Count - 1


                For i As Integer = _columncount To 0 Step -1
                    Select Case .Columns(i).FieldName.ToString.ToUpper
                        Case "FNCMSeq".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper
                            .Columns(i).AppearanceCell.BackColor = Color.White
                            .Columns(i).AppearanceCell.ForeColor = Color.Black
                            .Columns(i).OptionsColumn.AllowEdit = False
                            .Columns(i).OptionsColumn.AllowMove = False
                            .Columns(i).OptionsColumn.AllowSort = DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(i))
                    End Select
                Next


                If Not (_dt Is Nothing) Then
                    For Each col As DataColumn In _dt.Columns
                        Select Case col.ColumnName.ToString.ToUpper
                            Case "FNCMSeq".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper
                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = col.ColumnName.ToString
                                    .Name = "FTSubOrderNo" & col.ColumnName.ToString
                                    .Caption = col.ColumnName.ToString

                                End With
                                .Columns.Add(ColG)
                                With .Columns(col.ColumnName.ToString)
                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DefaultBoolean.False
                                        .AllowSort = DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True

                                    End With
                                End With
                                .Columns(col.ColumnName.ToString).Width = 50
                                .Columns(col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                        End Select
                    Next
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        With GridCol
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                    Next
                End If
                .OptionsView.ShowAutoFilterRow = False
            End With
            Me.ogcDetailBreakDown.DataSource = _dt.Copy
            _dt.Dispose()
            '_dtpack.Dispose()

        Catch ex As Exception
        End Try

    End Sub


    Private Sub FTSubOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSubOrderNo.EditValueChanged
        Try
            If Me.FTSubOrderNo.Text = "" Then
                Me.ogdDivertSrc.DataSource = Nothing
                Me.ogcDetailBreakDown.DataSource = Nothing
            Else
                Call BrowseSubOrderData()
                Call CheckStateSubOrder()
                Call LoadDetailSubDivertBreakDown()
                Call PrepareDetailBreakDown(1, Me.FTSubOrderNo.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub wOrderExtractDestination_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call InitGrid()
    End Sub

    Private Sub ogvDivertSrc_DoubleClick(sender As Object, e As EventArgs) Handles ogvDivertSrc.DoubleClick
        If FTOrderNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        Else
            If Me.FTSubOrderNo.Text <> "" Then

                Dim MaxSeqSD As Integer
                With Me.ogvDivertSrc
                    MaxSeqSD = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNDivertSeq").ToString))
                End With


                If MaxSeqSD <= 0 Then Exit Sub

                _wDivertOrderDistination = New wCMOrderDistination()

                With _wDivertOrderDistination

                    .FTOrderNoSrc = Me.FTOrderNo.Text.Trim
                    .SubOrderSrc = Me.FTSubOrderNo.Text.Trim
                    .SubOrderDivertSeq = MaxSeqSD

                    If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Call LoadDetailSubDivertBreakDown()
                    End If

                End With

            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาเลือก SubOrderNo", 1507170904, Me.Text)
                FTSubOrderNo.Focus()
            End If
        End If
    End Sub

    Private Sub ogdDivertSrc_Click(sender As Object, e As EventArgs) Handles ogdDivertSrc.Click

    End Sub
End Class

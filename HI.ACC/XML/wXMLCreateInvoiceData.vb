Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports System.Net
Imports Microsoft.Win32
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.Controls
Imports Newtonsoft.Json
Imports System.ComponentModel

Public Class wXMLCreateInvoiceData

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception
        End Try

    End Sub
    Private _DefailtPath As String
    Private Sub InitGrid()

        Try
            With Me.ogvbreakdown
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        Try
            With Me.ogvdetail
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        With ogvbreakdown
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
        End With

        With ogvdetail
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
        End With

    End Sub

    Private m_mergedCellEditorPOLineItem As DevExpress.XtraEditors.TextEdit
    Private m_mergedCellsEdited As GridCellInfoCollection

    Private Sub CreateMergeEditControl()
        m_mergedCellEditorPOLineItem = New DevExpress.XtraEditors.TextEdit

        With m_mergedCellEditorPOLineItem
            .Properties.MaxLength = 10
            .Properties.CharacterCasing = CharacterCasing.Upper
            .Properties.Appearance.BackColor = Color.LightCyan
        End With

    End Sub

    Private Sub ClearGrid(Optional Prod As Boolean = False)

        With Me.ogvbreakdown
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With

    End Sub

    Public Sub LoadOrderPriceInfo(ByVal Key As Object)

        If Me.ogvbreakdown.DataSource Is Nothing Then
            Call LoadOrderBreakDown(Me.FTCustomerPO.Text)
        End If

        Dim _dt As System.Data.DataTable
        Dim _dtPrice As System.Data.DataTable
        With CType(Me.ogcbreakdown.DataSource, System.Data.DataTable)
            .AcceptChanges()
            _dt = .Copy

        End With

        Dim _Qry As String = ""
        Dim _Filter As String = ""
        Dim _FirstPrice As Double = 0.0
        Dim _dtFirstPrice As System.Data.DataTable
        FTStateMerApp.Checked = False
        FNExchangeRate.Value = 0

        Dim _dtcminvoice As System.Data.DataTable
        Dim _dtdataxml As System.Data.DataTable

        '_Qry = "   SELECT Top 1 A.FTCustomerPO, A.FTInvoiceNo, A.FTInvoiceExportNo,A.FDInvoiceExportDate,A.FTStateMerApp,A.FTStateHanger,ISNULL(C.FNExchangeRate,ISNULL(B.FNSellingRate,0)) AS FNExchangeRate	 "
        '_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK)"
        '_Qry &= vbCrLf & "   LEFT OUTER JOIN  (SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRateMI AS B WITH(NOLOCK) WHERE FNHSysCurId=1310190001) AS B ON A.FDInvoiceDate = B.FDDate"
        '_Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS C WITH(NOLOCK) ON A.FTCustomerPO = C.FTCustomerPO AND A.FTInvoiceNo = C.FTInvoiceNo"
        '_Qry &= vbCrLf & "   WHERE  (A.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        '_Qry &= vbCrLf & "    AND (A.FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

        _Qry = "   SELECT Top 1 A.FTCustomerPO, A.FTInvoiceNo, A.FTInvoiceExportNo,A.FDInvoiceExportDate,A.FTStateMerApp,A.FTStateHanger,ISNULL(C.FNExchangeRate,0) AS FNExchangeRate	 "
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS C WITH(NOLOCK) ON A.FTCustomerPO = C.FTCustomerPO AND A.FTInvoiceNo = C.FTInvoiceNo"
        _Qry &= vbCrLf & "   WHERE  (A.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        _Qry &= vbCrLf & "    AND (A.FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

        _dtcminvoice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        'FTInvoiceExportNo.Text = ""
        'FDInvoiceExportDate.Text = ""

        For Each Rxi As DataRow In _dtcminvoice.Rows

            FTStateMerApp.Checked = (Rxi!FTStateMerApp.ToString = "1")
            FTStateHanger.Checked = (Rxi!FTStateHanger.ToString = "1")

            FNExchangeRate.Value = Val(Rxi!FNExchangeRate.ToString)

            'FTInvoiceExportNo.Text = Rxi!FTInvoiceExportNo.ToString
            'FDInvoiceExportDate.Text = HI.UL.ULDate.ConvertEN(Rxi!FDInvoiceExportDate.ToString)
            Exit For
        Next

        If FNExchangeRate.Value <= 0 Then

            _Qry = "  SELECT TOP 1 A.FTCustomerPO, A.FTInvoiceNo, A.FDInvoiceDate, A.FTInvoiceExportNo, A.FDInvoiceExportDate, Exc.FNBuyingRate, Exc.FNSellingRate"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice As A WITH(NOLOCK) INNER Join"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D As B WITH(NOLOCK) On A.FTCustomerPO = B.FTCustomerPO And A.FTInvoiceNo = B.FTInvoiceNo INNER Join"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As C On B.FTColorway = C.FTColorway And B.FTSizeBreakDown = C.FTSizeBreakDown And B.FTCustomerPO = C.FTPOref And "
            _Qry &= vbCrLf & "    B.FTPOLineItem = C.FTNikePOLineItem INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As D WITH(NOLOCK) On C.FTOrderNo = D.FTOrderNo INNER JOIN"
            _Qry &= vbCrLf & "    TACCTExchangeRateMI As Exc On D.FNHSysCurId = Exc.FNHSysCurId And A.FDInvoiceDate = Exc.FDDate"

            _dtcminvoice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            For Each Rxi As DataRow In _dtcminvoice.Rows
                FNExchangeRate.Value = Val(Rxi!FNSellingRate.ToString)
                Exit For
            Next

        End If

        _Qry = "   Select Top 1 FTPostUser,FDPostDate,FTPostTime	 "
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice As A With(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        _Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

        _dtdataxml = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each Rxi As DataRow In _dtdataxml.Rows

            FTPostUser.Text = Rxi!FTPostUser.ToString
            FDPostDate.Text = HI.UL.ULDate.ConvertEN(Rxi!FDPostDate.ToString) & "  " & Rxi!FTPostTime.ToString

            Exit For

        Next

        _Qry = "   SELECT TOP 1  FTCustomerPO"
        _Qry &= vbCrLf & "  , FNFabricAssetAct AS FNFabricAsset"
        _Qry &= vbCrLf & "  , FNAccessoryAssetAct AS FNAccessoryAsset"
        _Qry &= vbCrLf & "   , FNCMPAct AS FNCMP"
        _Qry &= vbCrLf & "  , FNCostTransportAct AS FNCostTransport"
        _Qry &= vbCrLf & "  , FNFirstSaleAct AS FNFirstSale"
        _Qry &= vbCrLf & "  , FNHangerAct AS FNHanger"

        If FTStateHanger.Checked Then
            '_Qry &= vbCrLf & "  , FNNetFirstSale"
            '_Qry &= vbCrLf & " CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN FNNetFirstSaleAct ELSE FNNetFirstSale  END AS FNNetFirstSale"
            _Qry &= vbCrLf & ", FNNetFirstSaleCostSheet AS FNNetFirstSale"
        Else
            '_Qry &= vbCrLf & "  , (FNNetFirstSale - ISNULL(FNHanger,0)) AS FNNetFirstSale"
            ' _Qry &= vbCrLf & " CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  (FNNetFirstSaleAct - ISNULL(FNHangerAct,0)) ELSE  (FNNetFirstSale - ISNULL(FNHanger,0))  END AS FNNetFirstSale "
            _Qry &= vbCrLf & " ,  (FNNetFirstSaleCostSheet - ISNULL(FNHangerCostSheet,0))  AS FNNetFirstSale "
        End If

        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        _dtFirstPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each R As DataRow In _dtFirstPrice.Rows
            _FirstPrice = Val(R!FNNetFirstSale.ToString)
        Next

        '_Qry = "   SELECT FTCustomerPO, FTInvoiceNo, FTColorway, FTSizeBreakDown, FNPrice"
        '_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS A WITH(NOLOCK)"
        '_Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        '_Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "')"
        '_dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)


        _Qry = "   SELECT FTCustomerPO, FTInvoiceNo, FTColorway , FTSizeBreakDown, FNQuantity"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "

        _Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') AND FNQuantity >0"
        _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns

                Select Case Col.ColumnName.ToString.ToUpper


                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                    Case Else
                        _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
                        If _dtPrice.Select(_Filter).Length > 0 Then

                            For Each Rx As DataRow In _dtPrice.Select(_Filter)

                                R.Item(Col.ColumnName.ToString) = _FirstPrice ' Val(Rx!FNPrice.ToString)
                                Exit For
                            Next

                        Else
                            R.Item(Col.ColumnName.ToString) = System.DBNull.Value
                        End If

                End Select

            Next

        Next

        Me.ogcbreakdown.DataSource = _dt.Copy
        Dim _dt2 As System.Data.DataTable

        _Qry = "  Select  *,CASE WHEN FNNetPrice > 0 THEN Convert(numeric(18,2),((FNNetPrice - FNFirstPrice) *100.00) / FNNetPrice) ELSE 0.00 END AS FNFirstPricePer  "
        _Qry &= vbCrLf & ",Convert(numeric(18,2),FNPriceFOB * " & FNExchangeRate.Value & ")  AS FNPriceFOBTHB"
        _Qry &= vbCrLf & ",Convert(numeric(18,2),FNNetPrice * " & FNExchangeRate.Value & ")  AS FNNetPriceTHB"
        _Qry &= vbCrLf & ",Convert(numeric(18,2),FNFirstPrice * " & FNExchangeRate.Value & ")  AS FNFirstPriceTHB"
        _Qry &= vbCrLf & "  FROM (  Select A.FTCustomerPO "
        _Qry &= vbCrLf & "  	, A.FTInvoiceNo"
        _Qry &= vbCrLf & "  	, A.FTColorway"
        _Qry &= vbCrLf & "  	, A.FTSizeBreakDown"
        _Qry &= vbCrLf & "  	, A.FNQuantity"
        _Qry &= vbCrLf & "  	, A.FTPOLineItem"
        '_Qry &= vbCrLf & " ,ISNULL(("

        '_Qry &= vbCrLf & "    Select TOP 1   XXA.FTPOLineItem"
        '_Qry &= vbCrLf & "    FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D As XXA With(NOLOCK) "
        '_Qry &= vbCrLf & "    WHERE XXA.FTCustomerPO = A.FTCustomerPO"
        '_Qry &= vbCrLf & "      And  XXA.FTInvoiceNo= N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
        '_Qry &= vbCrLf & "      AND  XXA.FTColorway=A.FTColorway "

        '_Qry &= vbCrLf & "   )"
        '_Qry &= vbCrLf & "  	,ISNULL(("
        '_Qry &= vbCrLf & "      		SELECT TOP 1  ISNULL(BA.FTNikePOLineItem,'') AS FTColorway"
        '_Qry &= vbCrLf & "       FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS AA WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "                    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS BA WITH(NOLOCK) ON AA.FTOrderNo = BA.FTOrderNo "
        '_Qry &= vbCrLf & "    WHERE AA.FTPORef = A.FTCustomerPO"
        '_Qry &= vbCrLf & "      AND  BA.FTColorway=A.FTColorway "
        '_Qry &= vbCrLf & "      ORDER BY  BA.FTNikePOLineItem DESC "
        '_Qry &= vbCrLf & "     	),'')) AS FTPOLineItem"

        _Qry &= vbCrLf & "  	,X.FNPrice AS FNPriceFOB ,X.FTCurCode,X.FTStateHold"
        _Qry &= vbCrLf & "  	,ISNULL(X.FNNetPrice,0) AS FNNetPrice"

        ''Change To New First Sale FOB 2017/06/07
        '_Qry &= vbCrLf & "  	,ISNULL(("

        '_Qry &= vbCrLf & " SELECT TOP 1  "


        'If FTStateHanger.Checked Then

        '    _Qry &= vbCrLf & " FNNetFirstSaleCostSheet AS FNNetFirstSale"
        'Else

        '    _Qry &= vbCrLf & " (FNNetFirstSaleCostSheet  - ISNULL(FNHangerCostSheet,0)) AS FNNetFirstSale "
        'End If

        '_Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet AS XCA WITH(NOLOCK) "
        '_Qry &= vbCrLf & " WHERE XCA.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
        '_Qry &= vbCrLf & " ),0) AS FNFirstPrice"

        '' _Qry &= vbCrLf & ",ISNULL(XZ.FNNetPrice ,0) AS FNFirstPrice"
        _Qry &= vbCrLf & "  	,ISNULL(X.FNNetFOBTVW,0) AS FNFirstPrice"

        '_Qry &= vbCrLf & ",ISNULL(XZ.FNNetPrice , "

        '_Qry &= vbCrLf & "  	ISNULL(("

        '_Qry &= vbCrLf & " SELECT TOP 1  "


        'If FTStateHanger.Checked Then

        '    _Qry &= vbCrLf & " FNNetFirstSaleCostSheet AS FNNetFirstSale"
        'Else

        '    _Qry &= vbCrLf & " (FNNetFirstSaleCostSheet  - ISNULL(FNHangerCostSheet,0)) AS FNNetFirstSale "
        'End If

        '_Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet AS XCA WITH(NOLOCK) "
        '_Qry &= vbCrLf & " WHERE XCA.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
        '_Qry &= vbCrLf & " ),0) "


        '_Qry &= vbCrLf & ") As FNFirstPrice"


        ''Change To New First Sale FOB 2017/06/07



        _Qry &= vbCrLf & " ,  MC.FNMatColorSeq, MS.FNMatSizeSeq"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D As A With(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor As MC On A.FTColorway = MC.FTMatColorCode  "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize  As MS On A.FTSizeBreakDown  = MS.FTMatSizeCode   "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN ("
        '_Qry &= vbCrLf & "      Select MAX(C.FTCurCode) As FTCurCode, A.FTPORef As FTCustomerPO, B.FTColorway, B.FTSizeBreakDown, MAX(B.FNPrice) As FNPrice, MAX(ISNULL(B.FNPriceOrg,B.FNPrice)) As FNPriceOrg, MAX(ISNULL(B.FNNetPrice,0)) As FNNetPrice"
        '_Qry &= vbCrLf & "       FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A With(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown As B With(NOLOCK) On A.FTOrderNo = B.FTOrderNo INNER JOIN"
        '_Qry &= vbCrLf & "  	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As S With(NOLOCK)  On B.FTOrderNo = S.FTOrderNo And B.FTSubOrderNo = S.FTSubOrderNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As C With(NOLOCK) On S.FNHSysCurId = C.FNHSysCurId"
        '_Qry &= vbCrLf & "   WHERE A.FTPORef = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
        '_Qry &= vbCrLf & "    GROUP BY A.FTPORef, B.FTColorway, B.FTSizeBreakDown"
        '_Qry &= vbCrLf & "    ) AS X ON  A.FTCustomerPO =  X.FTCustomerPO AND A.FTColorway = X.FTColorway AND A.FTSizeBreakDown =X.FTSizeBreakDown"

        _Qry &= vbCrLf & "    SELECT MAX(C.FTCurCode) AS FTCurCode"
        _Qry &= vbCrLf & " 	 , B.FTPORef AS FTCustomerPO"
        _Qry &= vbCrLf & " 	 , B.FTColorway"
        _Qry &= vbCrLf & " 	 , B.FTSizeBreakDown"
        _Qry &= vbCrLf & " 	 ,B.FTNikePOLineItem"
        _Qry &= vbCrLf & " 	 , MAX(B.FNPrice) AS FNPrice"
        _Qry &= vbCrLf & " 	 , MAX(B.FNPriceOrg) AS FNPriceOrg"
        _Qry &= vbCrLf & " 	 , MAX(B.FNNetNetPrice) AS FNNetFOBTVW"
        _Qry &= vbCrLf & " 	 , MAX(ISNULL(B.FNNetPrice,0)) AS FNNetPrice,MAX(ISNULL(B.FTStateHold,'0')) AS FTStateHold"
        _Qry &= vbCrLf & "   FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS B ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK)  ON B.FTOrderNo = S.FTOrderNo AND B.FTSubOrderNo = S.FTSubOrderNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) ON S.FNHSysCurId = C.FNHSysCurId"
        _Qry &= vbCrLf & "    WHERE B.FTPORef= N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
        _Qry &= vbCrLf & "   GROUP BY B.FTPORef, B.FTColorway, B.FTSizeBreakDown,B.FTNikePOLineItem"
        _Qry &= vbCrLf & "    ) AS X ON  A.FTCustomerPO =  X.FTCustomerPO AND A.FTColorway = X.FTColorway AND A.FTSizeBreakDown =X.FTSizeBreakDown AND A.FTPOLineItem=X.FTNikePOLineItem"

        '  _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet_Breakdown AS XZ WITH(NOLOCK) ON  A.FTCustomerPO =  XZ.FTCustomerPO AND A.FTColorway = XZ.FTColorway AND A.FTSizeBreakDown =XZ.FTSizeBreakDown AND A.FTPOLineItem=XZ.FTPOLineItem"

        _Qry &= vbCrLf & "   WHERE  (A.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        _Qry &= vbCrLf & "    AND (A.FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') AND A.FNQuantity >0"
        _Qry &= vbCrLf & "  ) AS M1"
        _Qry &= vbCrLf & "   ORDER BY FNMatColorSeq ,FNMatSizeSeq"

        _dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        Dim _Amt As Double = 0
        For Each R As DataRow In _dt2.Rows
            _Amt = _Amt + Double.Parse(Format(Val(R!FNQuantity.ToString) * Val(R!FNFirstPrice.ToString), "0.00"))
        Next

        Me.FNAmt.Value = _Amt
        Me.ogcdetail.DataSource = _dt2.Copy
    End Sub

    Public Sub LoadOrderDataInfo(ByVal Key As Object)

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        Call ClearGrid()

        Call LoadOrderBreakDown(Key)

        Dim _Qry As String = ""
        Dim _dtstyle As System.Data.DataTable
        _Qry = "  SELECT  TOP 1  A.FTPORef, ST.FNCM, ST.FNCMDisPer, ST.FNCMDisAmt, ST.FNNetCM, ST.FNCostTransport"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId"
        _Qry &= vbCrLf & "           INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON A.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"


        ' _Qry &= vbCrLf & " WHERE  (A.FTPORef = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "')"
        '_Qry &= vbCrLf & " WHERE A.FTOrderNo IN ("
        '_Qry &= vbCrLf & " SELECT O.FTOrderNo"
        '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        '_Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        '_Qry &= vbCrLf & " GROUP BY O.FTOrderNo"
        '_Qry &= vbCrLf & " )"
        _dtstyle = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        Dim _FNCMP As Double = 0

        For Each R As DataRow In _dtstyle.Rows
            _FNCMP = Val(R!FNNetCM.ToString)


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

            '_Qry = "  SELECT  TOP 1   ST.FNNetCM "
            '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId AND A.FNHSysSeasonId =ST.FNHSysSeasonId "
            '_Qry &= vbCrLf & "         INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON A.FTOrderNo = S.FTOrderNo"
            '_Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"

            ' _Qry &= vbCrLf & " WHERE  (A.FTPORef = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "')"
            '_Qry &= vbCrLf & " WHERE A.FTOrderNo IN ("
            '_Qry &= vbCrLf & " SELECT TOP 1 O.FTOrderNo"
            '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
            '_Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
            '_Qry &= vbCrLf & " GROUP BY O.FTOrderNo"
            '_Qry &= vbCrLf & " )"

            ' _FNCMP = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, _FNCMP.ToString))

        Next

        FNCMP.Value = _FNCMP
        _Spls.Close()
    End Sub

    Private Sub LoadOrderBreakDown(Key As Object)
        Dim _dt As System.Data.DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_BreakDown_CustomerPOXML '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.ogvbreakdown

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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
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

                                'If Not (Col.ColumnName.ToString = "Total") Then
                                '    .AppearanceCell.BackColor = Drawing.Color.LightCyan
                                '    .AppearanceCell.ForeColor = Color.Blue
                                'End If

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False ' Not (Col.ColumnName.ToString = "Total")

                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 70
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n4}"

                    End Select

                Next

            End If

        End With

        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                    Case Else
                        R.Item(Col.ColumnName.ToString) = System.DBNull.Value
                End Select
            Next

        Next
        Me.ogcbreakdown.DataSource = _dt.Copy

    End Sub

    Private Function SaveData() As Boolean
        Dim _Qry As String = ""
        Dim dtprice As System.Data.DataTable
        Dim dt As System.Data.DataTable
        Dim _dtFirstPrice As New System.Data.DataTable
        Dim _FTPOLineItem As String
        Dim _FNPriceFOB As Double
        Dim _FNNetPrice As Double
        Dim _FNFirstPrice As Double
        Dim _FNQuantity As Integer
        Dim _FNPriceFOBTHB As Double
        Dim _FNNetPriceTHB As Double
        Dim _FNFirstPriceTHB As Double
        Dim _FNExchangeRate As Double = FNExchangeRate.Value
        Dim _HFNFabricAsset, _HFNAccessoryAsset, _HFNCMP, _HFNCostTransport, _HFNFirstSale, _HFNHanger, _HFNFabImport, _HFNAccImport, _HFNFirstPrice As Double
        Dim _FNSurchargeCS As Double = 0

        _HFNFabricAsset = 0
        _HFNAccessoryAsset = 0
        _HFNCMP = 0
        _HFNCostTransport = 0
        _HFNFirstSale = 0
        _HFNHanger = 0
        _HFNFabImport = 0
        _HFNAccImport = 0
        _FNSurchargeCS = 0
        _HFNFirstPrice = 0

        '_Qry = "   SELECT TOP 1  FNFabricAsset, FNAccessoryAsset, FNCMP, FNCostTransport, FNFirstSale, FNHanger, FNFabImport, FNAccImport,FNNetFirstSale AS FNFirstPrice"
        '_Qry = "   SELECT TOP 1  CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  FNFabricAssetAct ELSE  FNFabricAsset  END AS FNFabricAsset"
        '_Qry &= vbCrLf & ",CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  FNAccessoryAssetAct ELSE  FNAccessoryAsset  END AS FNAccessoryAsset"
        '_Qry &= vbCrLf & ",CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  FNCMPAct ELSE  FNCMP  END AS  FNCMP"
        '_Qry &= vbCrLf & ",CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  FNCostTransportAct ELSE  FNCostTransport  END AS FNCostTransport"
        '_Qry &= vbCrLf & ", CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  FNFirstSaleAct ELSE  FNFirstSale  END AS FNFirstSale"
        '_Qry &= vbCrLf & ", CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  FNHangerAct ELSE  FNHanger  END AS FNHanger"
        '_Qry &= vbCrLf & ", FNFabImport"
        '_Qry &= vbCrLf & ", FNAccImport"
        '_Qry &= vbCrLf & ",CASE WHEN  FNFabricAssetAct>0 AND FNAccessoryAssetAct > 0 THEN  FNNetFirstSaleAct ELSE  FNNetFirstSale  END AS FNFirstPrice  "

        _Qry = "   SELECT TOP 1  FNFabricAssetCostSheet AS FNFabricAsset"
        _Qry &= vbCrLf & ", FNAccessoryAssetCostSheet AS FNAccessoryAsset"
        _Qry &= vbCrLf & ", FNCMPCostSheet AS  FNCMP"
        _Qry &= vbCrLf & ", FNCostTransportCostSheet AS FNCostTransport"
        _Qry &= vbCrLf & ", FNFirstSaleCostSheet AS FNFirstSale"
        _Qry &= vbCrLf & ", FNHangerCostSheet AS FNHanger"
        _Qry &= vbCrLf & ", FNFabImportCS AS  FNFabImport"
        _Qry &= vbCrLf & ", FNAccImportCS AS FNAccImport"
        _Qry &= vbCrLf & ", FNNetFirstSaleCostSheet AS FNFirstPrice,FNSurchargeCS  "
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "           WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        _dtFirstPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each R As DataRow In _dtFirstPrice.Rows

            _HFNFabricAsset = Val(R!FNFabricAsset.ToString)
            _HFNAccessoryAsset = Val(R!FNAccessoryAsset.ToString)
            _HFNCMP = Val(R!FNCMP.ToString)
            _HFNCostTransport = Val(R!FNCostTransport.ToString)
            _HFNFirstSale = Val(R!FNFirstSale.ToString)
            _HFNHanger = Val(R!FNHanger.ToString)
            _HFNFabImport = Val(R!FNFabImport.ToString)
            _HFNAccImport = Val(R!FNAccImport.ToString)
            _HFNFirstPrice = Val(R!FNFirstPrice.ToString)
            _FNSurchargeCS = Val(R!FNSurchargeCS.ToString)

        Next

        _dtFirstPrice.Dispose()

        With CType(Me.ogcbreakdown.DataSource, System.Data.DataTable)
            .AcceptChanges()
            dt = .Copy
        End With

        With CType(Me.ogcdetail.DataSource, System.Data.DataTable)
            .AcceptChanges()
            dtprice = .Copy
        End With

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice"
            _Qry &= vbCrLf & "  SET   FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & " , FDInvoiceDate='" & HI.UL.ULDate.ConvertEnDB(FDInvoiceDate.Text) & "'"
            _Qry &= vbCrLf & " , FNAmt=" & FNAmt.Value & ""
            _Qry &= vbCrLf & " , FTAmtTH='" & HI.UL.ULF.rpQuoted(FTAmtTH.Text) & "'"
            _Qry &= vbCrLf & " , FTAmtEN='" & HI.UL.ULF.rpQuoted(FTAmtEN.Text) & "'"
            _Qry &= vbCrLf & " , FTStateHanger='" & FTStateHanger.EditValue.ToString & "'"
            _Qry &= vbCrLf & " , FNExchangeRate='" & FNExchangeRate.Value & "'"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice"
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTCustomerPO, FTInvoiceNo, FDInvoiceDate,FNAmt,FTAmtTH,FTAmtEN,FTStateHanger"
                _Qry &= vbCrLf & ", FNFabricAsset, FNAccessoryAsset, FNCMP, FNCostTransport, FNFirstSale, FNHanger, FNFabImport, FNAccImport, FNFirstPrice,FNSurchargeCS,FNExchangeRate"

                _Qry &= vbCrLf & "  )"
                _Qry &= vbCrLf & " SELECT "
                _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(FDInvoiceDate.Text) & "'"
                _Qry &= vbCrLf & " ," & FNAmt.Value & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTAmtTH.Text) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTAmtEN.Text) & "'"
                _Qry &= vbCrLf & " ,'" & FTStateHanger.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ," & _HFNFabricAsset & ""
                _Qry &= vbCrLf & " ," & _HFNAccessoryAsset & ""
                _Qry &= vbCrLf & " ," & _HFNCMP & ""
                _Qry &= vbCrLf & " ," & _HFNCostTransport & ""
                _Qry &= vbCrLf & " ," & _HFNFirstSale & ""
                _Qry &= vbCrLf & " ," & _HFNHanger & ""
                _Qry &= vbCrLf & " ," & _HFNFabImport & ""
                _Qry &= vbCrLf & " ," & _HFNAccImport & ""
                _Qry &= vbCrLf & " ," & _HFNFirstPrice & ""
                _Qry &= vbCrLf & " ," & _FNSurchargeCS & ""
                _Qry &= vbCrLf & " ," & FNExchangeRate.Value & ""

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In dtprice.Rows

                _FTPOLineItem = R!FTPOLineItem.ToString
                _FNPriceFOB = Val(R!FNPriceFOB.ToString)
                _FNNetPrice = Val(R!FNNetPrice.ToString)
                _FNFirstPrice = Val(R!FNFirstPrice.ToString)
                _FNQuantity = Val(R!FNQuantity.ToString)

                _FNPriceFOBTHB = CDbl(Format(_FNPriceFOB * _FNExchangeRate, "0.00"))
                _FNNetPriceTHB = CDbl(Format(_FNNetPrice * _FNExchangeRate, "0.00"))
                _FNFirstPriceTHB = CDbl(Format(_FNFirstPrice * _FNExchangeRate, "0.00"))

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D"
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCustomerPO, FTInvoiceNo, FTColorway, FTSizeBreakDown, FNPrice "
                _Qry &= vbCrLf & ",FTPOLineItem,FNPriceFOB,FNNetPrice,FNFirstPrice,FNQuantity,FNPriceFOBTHB,FNNetPriceTHB,FNFirstPriceTHB"
                _Qry &= vbCrLf & "  ) "
                _Qry &= vbCrLf & " SELECT "
                _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString()) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Qry &= vbCrLf & " ," & _FNFirstPrice & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTPOLineItem) & "'"
                _Qry &= vbCrLf & " ," & Val(_FNPriceFOB) & ""
                _Qry &= vbCrLf & " ," & Val(_FNNetPrice) & ""
                _Qry &= vbCrLf & " ," & Val(_FNFirstPrice) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(_FNQuantity)) & ""
                _Qry &= vbCrLf & " ," & Val(_FNPriceFOBTHB) & ""
                _Qry &= vbCrLf & " ," & Val(_FNNetPriceTHB) & ""
                _Qry &= vbCrLf & " ," & Val(_FNFirstPriceTHB) & ""

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            'For Each R As DataRow In dt.Rows
            '    For Each Col As DataColumn In dt.Columns

            '        Select Case Col.ColumnName.ToString.ToUpper
            '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
            '            Case Else

            '                If Val((R.Item(Col.ColumnName.ToString)).ToString()) > 0 Then

            '                    _FTPOLineItem = ""
            '                    _FNPriceFOB = 0
            '                    _FNNetPrice = 0
            '                    _FNFirstPrice = 0
            '                    _FNQuantity = 0

            '                    For Each Rx As DataRow In dtprice.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString()) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'")

            '                        _FTPOLineItem = Rx!FTPOLineItem.ToString
            '                        _FNPriceFOB = Val(Rx!FNPriceFOB.ToString)
            '                        _FNNetPrice = Val(Rx!FNNetPrice.ToString)
            '                        _FNFirstPrice = Val(Rx!FNFirstPrice.ToString)
            '                        _FNQuantity = Val(Rx!FNQuantity.ToString)

            '                    Next

            '                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D"
            '                    _Qry &= vbCrLf & "  ("
            '                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCustomerPO, FTInvoiceNo, FTColorway, FTSizeBreakDown, FNPrice "
            '                    _Qry &= vbCrLf & ",FTPOLineItem,FNPriceFOB,FNNetPrice,FNFirstPrice,FNQuantity"
            '                    _Qry &= vbCrLf & "  ) "
            '                    _Qry &= vbCrLf & " SELECT "
            '                    _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '                    _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
            '                    _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
            '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"
            '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString()) & "'"
            '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
            '                    _Qry &= vbCrLf & " ," & Val(R.Item(Col.ColumnName.ToString)) & ""
            '                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTPOLineItem) & "'"
            '                    _Qry &= vbCrLf & " ," & Val(_FNPriceFOB) & ""
            '                    _Qry &= vbCrLf & " ," & Val(_FNNetPrice) & ""
            '                    _Qry &= vbCrLf & " ," & Val(_FNFirstPrice) & ""
            '                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_FNQuantity)) & ""

            '                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '                        HI.Conn.SQLConn.Tran.Rollback()
            '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '                        Return False
            '                    End If


            '                End If

            '        End Select

            '    Next
            'Next

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
        Dim _Qry As String = ""
        Dim dt As System.Data.DataTable
        With CType(Me.ogcbreakdown.DataSource, System.Data.DataTable)
            .AcceptChanges()
            dt = .Copy
        End With

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"
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

    Private Function ExportToXMLFile() As Boolean
        Try
            Dim _StatePostFile As Boolean = True
            Dim _Qry As String = ""
            Dim _dtinvoice As System.Data.DataTable
            Dim _dtinvoiced As System.Data.DataTable
            Dim _dtexportdata As System.Data.DataTable
            _Qry = "   SELECT X.FTCustomerPO, X.FTInvoiceNo, X.FDInvoiceDate,'" & HI.UL.ULF.rpQuoted(FTInvoiceExportNo.Text) & "' AS FTInvoiceExportNo,'" & HI.UL.ULDate.ConvertEnDB(FDInvoiceExportDate.Text) & "' AS FDInvoiceExportDate"
            _Qry &= vbCrLf & "	,ISNULL(("

            _Qry &= vbCrLf & "		SELECT TOP 1  PG.FTVenderPramCode"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "	    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PG WITH(NOLOCK) ON O.FNHSysVenderPramId = PG.FNHSysVenderPramId"
            _Qry &= vbCrLf & "	  WHERE O.FTPORef = X.FTCustomerPO"
            _Qry &= vbCrLf & "		),'') AS FTProgram"
            _Qry &= vbCrLf & "		,Y.FTColorway "
            _Qry &= vbCrLf & "	,ISNULL(("


            '_Qry &= vbCrLf & "	SELECT TOP 1  LEFT(ST.FTStyleCode,6)+'-'+ RMC.FTRawMatColorCode AS FTMaterial"
            '_Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "	   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS ST WITH(NOLOCK)   ON O.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN"
            '_Qry &= vbCrLf & "	   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat AS STM WITH(NOLOCK)   ON ST.FNHSysStyleId = STM.FNHSysStyleId INNER JOIN"
            '_Qry &= vbCrLf & "	   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_ColorWay AS STC WITH(NOLOCK)   ON STM.FNSeq = STC.FNSeq AND STM.FNHSysStyleId = STC.FNHSysStyleId AND STM.FNMerMatSeq = STC.FNMerMatSeq INNER JOIN"
            '_Qry &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS RMC WITH(NOLOCK)   ON STC.FNHSysRawMatColorId = RMC.FNHSysRawMatColorId INNER JOIN"
            '_Qry &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS MMC WITH(NOLOCK)   ON STC.FNHSysMatColorId = MMC.FNHSysMatColorId"
            '_Qry &= vbCrLf & "	  WHERE O.FTPORef = X.FTCustomerPO"
            '_Qry &= vbCrLf & "	 AND MMC.FTMatColorCode=Y.FTColorway "
            '_Qry &= vbCrLf & "	 AND STM.FTStateMainMaterial='1'"
            '_Qry &= vbCrLf & "	 ORDER BY STM.FNSeq"

            _Qry &= vbCrLf & "	SELECT TOP 1  LEFT(ST.FTStyleCode,6)+'-'+ Y.FTColorway AS FTMaterial"
            _Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "	   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS ST WITH(NOLOCK)   ON O.FNHSysStyleId = ST.FNHSysStyleId "
            _Qry &= vbCrLf & "	  WHERE O.FTPORef = X.FTCustomerPO"

            _Qry &= vbCrLf & "	),'') AS FTMaterial"


            '_Qry &= vbCrLf & "	,ISNULL(("
            '_Qry &= vbCrLf & "		SELECT TOP 1  ISNULL(B.FTNikePOLineItem,'') AS FTColorway"
            '_Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo "
            '_Qry &= vbCrLf & " WHERE A.FTPORef='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
            '_Qry &= vbCrLf & " AND  B.FTColorway=Y.FTColorway "
            '_Qry &= vbCrLf & " ORDER BY  B.FTNikePOLineItem DESC "
            '_Qry &= vbCrLf & "	),'') AS FTPOLineItem"
            _Qry &= vbCrLf & ",ISNULL(Y.FTPOLineItem,ISNULL(("
            _Qry &= vbCrLf & "		SELECT TOP 1  ISNULL(B.FTNikePOLineItem,'') AS FTColorway"
            _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo "
            _Qry &= vbCrLf & " WHERE A.FTPORef='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
            _Qry &= vbCrLf & " AND  B.FTColorway=Y.FTColorway "
            _Qry &= vbCrLf & " ORDER BY  B.FTNikePOLineItem DESC "
            _Qry &= vbCrLf & "	),'')) AS FTPOLineItem"

            _Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS X INNER JOIN ("
            _Qry &= vbCrLf & "	 SELECT FTCustomerPO, FTInvoiceNo, FTColorway,Max(ISNULL(FTPOLineItem,'')) AS FTPOLineItem"
            _Qry &= vbCrLf & "	 FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS D WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE D.FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
            _Qry &= vbCrLf & "  AND  (D.FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "')"
            _Qry &= vbCrLf & "	 GROUP BY FTCustomerPO, FTInvoiceNo, FTColorway"
            _Qry &= vbCrLf & "	 ) AS Y ON X.FTCustomerPO = Y.FTCustomerPO AND X.FTInvoiceNo = Y.FTInvoiceNo "
            _Qry &= vbCrLf & " WHERE X.FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
            _Qry &= vbCrLf & " AND X.FTInvoiceNo=N'" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "'"
            _Qry &= vbCrLf & " ORDER BY X.FTCustomerPO, X.FTInvoiceNo,Y.FTColorway"
            _dtinvoice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            If _dtinvoice.Rows.Count > 0 Then

                '_Qry = "   SELECT   H.FTCustomerPO, H.FTInvoiceNo, H.FTColorway, H.FTSizeBreakDown, H.FNPrice, D.FNMatSizeSeq,X.FNPrice AS FNPriceFOB ,X.FNPriceOrg AS FNPriceGross,X.FTCurCode,H.FNNetPrice"

                '_Qry = "   SELECT   H.FTCustomerPO, H.FTInvoiceNo, H.FTColorway, H.FTSizeBreakDown, H.FNFirstPrice, H.FNPrice, D.FNMatSizeSeq,H.FNPriceFOB AS FNPriceFOB ,X.FNPriceOrg AS FNPriceGross,X.FTCurCode,H.FNNetPrice"
                _Qry = "   SELECT   H.FTCustomerPO, H.FTInvoiceNo, H.FTColorway, H.FTSizeBreakDown "

                _Qry &= vbCrLf & " , H.FNPrice, D.FNMatSizeSeq"

                _Qry &= vbCrLf & " ,X.FNPriceOrg AS FNPriceGross"
                '_Qry &= vbCrLf & " ,CASE WHEN ISNULL(H.FNPriceFOBTHB,0)<=0 THEN X.FTCurCode ELSE 'THB' END AS  FTCurCode"
                '_Qry &= vbCrLf & " ,CASE WHEN ISNULL(H.FNPriceFOBTHB,0)<=0 THEN H.FNPriceFOB ELSE H.FNPriceFOBTHB END AS FNPriceFOB "
                '_Qry &= vbCrLf & " ,CASE WHEN ISNULL(H.FNPriceFOBTHB,0)<=0 THEN H.FNFirstPrice ELSE H.FNFirstPriceTHB END AS FNFirstPrice "
                '_Qry &= vbCrLf & " ,CASE WHEN ISNULL(H.FNPriceFOBTHB,0)<=0 THEN H.FNNetPrice ELSE H.FNNetPriceTHB END AS FNNetPrice"

                _Qry &= vbCrLf & " , X.FTCurCode AS  FTCurCode"
                _Qry &= vbCrLf & " , H.FNPriceFOB AS FNPriceFOB "
                _Qry &= vbCrLf & " , H.FNFirstPrice AS FNFirstPrice "
                _Qry &= vbCrLf & " , H.FNNetPrice AS FNNetPrice"

                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS H WITH(NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS D WITH(NOLOCK)  ON H.FTSizeBreakDown = D.FTMatSizeCode"

                _Qry &= vbCrLf & "   Left OUTER JOIN ("
                _Qry &= vbCrLf & " SELECT MAX(C.FTCurCode) AS FTCurCode, A.FTPORef AS FTCustomerPO, B.FTColorway, B.FTSizeBreakDown, MAX(B.FNPrice) AS FNPrice, MAX(ISNULL(B.FNPriceOrg,B.FNPrice)) AS FNPriceOrg, MAX(ISNULL(B.FNNetPrice,0)) AS FNNetPrice"
                _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK)  ON B.FTOrderNo = S.FTOrderNo And B.FTSubOrderNo = S.FTSubOrderNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) ON S.FNHSysCurId = C.FNHSysCurId"
                _Qry &= vbCrLf & " WHERE A.FTPORef='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
                _Qry &= vbCrLf & " GROUP BY A.FTPORef, B.FTColorway, B.FTSizeBreakDown"
                _Qry &= vbCrLf & " ) AS X ON  H.FTCustomerPO =  X.FTCustomerPO AND H.FTColorway = X.FTColorway AND H.FTSizeBreakDown =X.FTSizeBreakDown"

                _Qry &= vbCrLf & "  WHERE  (H.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "')"
                _Qry &= vbCrLf & "  AND  (H.FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "')"
                _dtinvoiced = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)


                '_Qry = "   SELECT    H.FTColorway,MAX(ISNULL(X.FTNikePOLineItem,'')) AS FTNikePOLineItem "
                _Qry = "   SELECT    H.FTColorway,MAX(ISNULL(H.FTPOLineItem,'')) AS FTNikePOLineItem "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS H WITH(NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS D WITH(NOLOCK)  ON H.FTSizeBreakDown = D.FTMatSizeCode"

                _Qry &= vbCrLf & "   Left OUTER JOIN ("
                _Qry &= vbCrLf & " SELECT MAX(C.FTCurCode) AS FTCurCode, A.FTPORef AS FTCustomerPO, B.FTColorway, B.FTSizeBreakDown, MAX(B.FNPrice) AS FNPrice, MAX(ISNULL(B.FNPriceOrg,B.FNPrice)) AS FNPriceOrg, MAX(ISNULL(B.FNNetPrice,0)) AS FNNetPrice,MAX( ISNULL(B.FTNikePOLineItem,'')) AS FTNikePOLineItem"
                _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK)  ON B.FTOrderNo = S.FTOrderNo AND B.FTSubOrderNo = S.FTSubOrderNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) ON S.FNHSysCurId = C.FNHSysCurId"
                _Qry &= vbCrLf & " WHERE A.FTPORef='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
                _Qry &= vbCrLf & " GROUP BY A.FTPORef, B.FTColorway, B.FTSizeBreakDown"
                _Qry &= vbCrLf & " ) AS X ON  H.FTCustomerPO =  X.FTCustomerPO AND H.FTColorway = X.FTColorway AND H.FTSizeBreakDown =X.FTSizeBreakDown"

                _Qry &= vbCrLf & "  WHERE  (H.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "')"
                _Qry &= vbCrLf & "  AND  (H.FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "')"
                _Qry &= vbCrLf & " GROUP BY H.FTColorway "

                _dtexportdata = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                Dim DS As New DataSet()
                Dim DS2 As New DataSet()
                DS.ReadXml(Application.StartupPath + "\FILENIKE\NikeFile.xml")
                DS2.ReadXml(Application.StartupPath + "\FILENIKE\NikeFileInvoice.xml")



                Dim _TableName As String = ""
                Dim Op As New System.Windows.Forms.FolderBrowserDialog 'System.Windows.Forms.SaveFileDialog
                Dim _IndSeq As Integer = 0
                With Op

                    If _DefailtPath <> "" Then
                        .SelectedPath = _DefailtPath
                    End If


                    If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        If _DefailtPath <> .SelectedPath Then
                            WriteRegistry(.SelectedPath)
                            _DefailtPath = .SelectedPath
                        End If

                        Dim _Spls As New HI.TL.SplashScreen("Exporting...To XML File ,Please Wait....")
                        Dim PathName As String = .SelectedPath
                        Dim _ColorWay As String = ""
                        Dim _POLineItem As String = ""

                        Try
                            For Each Rxind As DataRow In _dtexportdata.Rows
                                _ColorWay = Rxind!FTColorway.ToString
                                _POLineItem = Rxind!FTNikePOLineItem.ToString

                                DS.Tables("SizeDetails").Rows.Clear()
                                DS.Tables("ExtFirstSaleItem").Rows.Clear()
                                DS2.Tables("ExtFirstSaleInvoiceItem").Rows.Clear()

                                Try
                                    Dim _TempName As String = Me.FTCustomerPO.Text.Replace("/", "_").Replace("\", "_").Replace("%", "_") & "_" & _ColorWay.Replace("/", "_").Replace("\", "_").Replace("%", "_") & "_" & _POLineItem.Replace("/", "_").Replace("\", "_").Replace("%", "_")
                                    Dim _FileName As String = PathName & "\" & _TempName & ".xml"
                                    Dim _FileNameInvoice As String = PathName & "\" & _TempName & "_Invoice.xml"
                                    Dim _FileNametemp As String = PathName & "\" & _TempName & HI.ST.UserInfo.UserName & ".xml"
                                    Dim _FileNametempInvoice As String = PathName & "\" & _TempName & HI.ST.UserInfo.UserName & "_Invoice.xml"

                                    'DS.Tables("SizeDetails").Rows.Clear()
                                    'DS.Tables("ExtFirstSaleItem").Rows.Clear()
                                    'DS2.Tables("ExtFirstSaleInvoiceItem").Rows.Clear()

                                    For Each R As DataRow In _dtinvoice.Select("FTColorway='" & HI.UL.ULF.rpQuoted(_ColorWay) & "'")

                                        DS.Tables("ExtFirstSaleItem").Rows.Add(R!FTCustomerPO.ToString, R!FTPOLineItem.ToString, R!FTMaterial.ToString, R!FTProgram.ToString, _IndSeq, 0)
                                        DS2.Tables("ExtFirstSaleInvoiceItem").Rows.Add(R!FTCustomerPO.ToString, R!FTPOLineItem.ToString, R!FTProgram.ToString, R!FTInvoiceExportNo.ToString, R!FDInvoiceExportDate.ToString.Replace("/", "-"), 0)

                                        For Each Rv As DataRow In _dtinvoiced.Select("FTColorway='" & HI.UL.ULF.rpQuoted(_ColorWay) & "' AND FTCustomerPO='" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'", "FNMatSizeSeq")
                                            'DS.Tables("SizeDetails").Rows.Add(Rv!FTSizeBreakDown.ToString, (Format(Val(Rv!FNPriceFOB.ToString), "0.00")), Rv!FTCurCode.ToString, (Format(Val(Rv!FNNetPrice.ToString), "0.00")), Rv!FTCurCode.ToString, (Format(Val(Rv!FNPrice.ToString), "0.00")), Rv!FTCurCode.ToString, "N", _IndSeq)
                                            DS.Tables("SizeDetails").Rows.Add(Rv!FTSizeBreakDown.ToString, (Format(Val(Rv!FNPriceFOB.ToString), "0.00")), Rv!FTCurCode.ToString, (Format(Val(Rv!FNNetPrice.ToString), "0.00")), Rv!FTCurCode.ToString, (Format(Val(Rv!FNFirstPrice.ToString), "0.00")), Rv!FTCurCode.ToString, "N", _IndSeq)
                                        Next

                                        _IndSeq = _IndSeq + 1

                                    Next

                                    DS.WriteXml(_FileNametemp)
                                    DS2.WriteXml(_FileNametempInvoice)

                                    If File.Exists(_FileNametemp) = False Then
                                        _Spls.Close()
                                        Return False

                                    End If

                                    Dim dtsource As New System.Data.DataTable("ReadXML")
                                    dtsource.Columns.Add("FTData", GetType(String))
                                    Dim lines() As String
                                    lines = File.ReadAllLines(_FileNametemp)

                                    For linenum As Integer = 0 To lines.Length - 1
                                        dtsource.Rows.Add(lines(linenum))
                                    Next

                                    Dim dtsource2 As New System.Data.DataTable("ReadXML")
                                    dtsource2.Columns.Add("FTData", GetType(String))
                                    Dim lines2() As String
                                    lines2 = File.ReadAllLines(_FileNametempInvoice)

                                    For linenum As Integer = 0 To lines2.Length - 1
                                        dtsource2.Rows.Add(lines2(linenum))
                                    Next

                                    Dim _strBuilder As New StringBuilder()
                                    Dim I As Integer = 0
                                    For Each R As DataRow In dtsource.Rows
                                        If I > 0 Then
                                            _strBuilder.AppendLine()
                                        End If
                                        Select Case I
                                            Case 1
                                                _strBuilder.Append("<soapenv:Envelope xmlns:soapenv=""" & "http://schemas.xmlsoap.org/soap/envelope/" & """ xmlns:mes=""" & "http://customs.nike.com/extfirstsale/messages" & """ xmlns:v1=""" & "http://customs.nike.com/extendedfirstsale/xsd/v1" & """>")
                                            Case 2
                                                _strBuilder.Append(R!FTData.ToString().Replace(" ", ""))
                                            Case Else
                                                _strBuilder.Append((R!FTData.ToString().Replace(" xmlns:v1=""" & "http://customs.nike.com/extendedfirstsale/xsd/v1" & """", "")).Replace(" xmlns:mes=" & """http://customs.nike.com/extfirstsale/messages" & """", ""))
                                        End Select

                                        I = I + 1
                                    Next

                                    Dim _strBuilder2 As New StringBuilder()
                                    I = 0
                                    For Each R As DataRow In dtsource2.Rows
                                        If I > 0 Then
                                            _strBuilder2.AppendLine()
                                        End If

                                        Select Case I
                                            Case 1
                                                _strBuilder2.Append("<soapenv:Envelope xmlns:soapenv=""" & "http://schemas.xmlsoap.org/soap/envelope/" & """ xmlns:mes=""" & "http://customs.nike.com/extfirstsale/messages" & """ xmlns:v1=""" & "http://customs.nike.com/extendedfirstsale/xsd/v1" & """>")
                                            Case 2
                                                _strBuilder2.Append(R!FTData.ToString().Replace(" ", ""))
                                            Case Else
                                                _strBuilder2.Append((R!FTData.ToString().Replace(" xmlns:v1=""" & "http://customs.nike.com/extendedfirstsale/xsd/v1" & """", "")).Replace(" xmlns:mes=" & """http://customs.nike.com/extfirstsale/messages" & """", ""))
                                        End Select

                                        I = I + 1
                                    Next


                                    Try
                                        File.Delete(_FileNametemp)
                                    Catch ex As Exception
                                    End Try

                                    If Me.FTStatePostXML.Checked Then

                                        _Spls.UpdateInformation("Posting... XML File , Please wait....")

                                        If PostFileToWebServiceWithCerT(_strBuilder.ToString(), True) = False Then
                                            _StatePostFile = False
                                        End If

                                        If PostFileToWebServiceWithCerT(_strBuilder2.ToString()) = False Then
                                            _StatePostFile = False
                                        End If

                                    End If

                                    Try
                                        File.Delete(_FileNametempInvoice)
                                    Catch ex As Exception
                                    End Try

                                    Try
                                        File.Delete(_FileName)
                                    Catch ex As Exception
                                    End Try

                                    Try
                                        File.Delete(_FileNameInvoice)
                                    Catch ex As Exception
                                    End Try

                                    Dim myWriter As New IO.StreamWriter(_FileName, True, System.Text.Encoding.Default)
                                    myWriter.WriteLine(_strBuilder.ToString())
                                    myWriter.Close()


                                    Dim myWriter2 As New IO.StreamWriter(_FileNameInvoice, True, System.Text.Encoding.Default)
                                    myWriter2.WriteLine(_strBuilder2.ToString())
                                    myWriter2.Close()

                                Catch ex As Exception
                                End Try

                            Next
                        Catch ex As Exception
                            _Spls.Close()
                            Return False
                        End Try

                        _Spls.Close()
                        HI.MG.ShowMsg.mInfo("Export To XML Complete !!!", 1506240898, Me.Text, , MessageBoxIcon.Information)

                        If _StatePostFile = False Then
                            HI.MG.ShowMsg.mInfo("ระบบไม่วสามารถทำการ Post File ได้ กรุณาทำการตรวจสอบ Internet !!!", 1506249832, Me.Text, , MessageBoxIcon.Warning)
                            Return False
                        End If

                        Return True
                    Else
                        Return False
                    End If

                End With

            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลสำหรับทำการ Export To XML กรุณาทำการตรวจสอบ !!!", 1506240899, Me.Text, , MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            HI.MG.ShowMsg.mInfo("ไม่สามารถทำการ Export ได้กรุณาทำการติดต่อ Admin !!!", 1506240897, Me.Text, , MessageBoxIcon.Warning)
        End Try

        Return False

    End Function

    Public Shared Function ReadRegistry() As String
        Dim regKey As RegistryKey
        Dim valreturn As String = ""

        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)
        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExporXMLNike", "")
        regKey.Close()

        Return valreturn
    End Function

    Public Shared Sub WriteRegistry(ByVal value As Object)

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        regKey.SetValue("PathExporXMLNike", value.ToString)
        regKey.Close()

    End Sub

    Private Sub wXMLCreateInvoiceData_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call InitGrid()
        Call CreateMergeEditControl()
        ' RemoveHandler FTInvoiceNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
    End Sub

    Private Sub FTCustomerPO_EditValueChanged(sender As Object, e As EventArgs) Handles FTCustomerPO.EditValueChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTCustomerPO_EditValueChanged), New Object() {sender, e})
        Else
            FTPostUser.Text = ""
            FDPostDate.Text = ""
            FTOrderNo.Text = ""
            Me.FNHSysStyleId.Text = ""
            Me.FNHSysStyleId_None.Text = ""
            Me.FNHSysCustId.Text = ""
            Me.FNHSysCustId_None.Text = ""
            Me.FTStateMerApp.Checked = False
            Me.FTInvoiceNo.Text = ""
            Me.FTInvoiceExportNo.Text = ""
            Call LoadOrderDataInfo(FTCustomerPO.Text)
            Me.ogcdetail.DataSource = Nothing
            Me.FTStatePostXML.Checked = False
            Me.otbdetail.SelectedTabPageIndex = 0
            Call LoadListOrderInfo(FTCustomerPO.Text)
        End If

    End Sub

    Private Sub LoadListOrderInfo(ByVal _FTPORef As String)
        Dim _Str As String = ""

        _Str = "SELECT '0' AS FNSelect"
        _Str &= vbCrLf & "   , A.FTOrderNo"
        _Str &= vbCrLf & "  ,SEAS.FTSeasonCode,ISNULL(PMC.FTVenderPramCode,'') AS FTPGMCode  "
        _Str &= vbCrLf & "   , CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) "
        _Str &= vbCrLf & "         ELSE '' END AS FDOrderDate, ISNULL"
        _Str &= vbCrLf & "                  ((SELECT     CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate"
        _Str &= vbCrLf & "                      FROM         (SELECT     X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate"
        _Str &= vbCrLf & "                                             FROM          HITECH_MERCHAN.dbo.TMERTOrder AS X INNER JOIN"
        _Str &= vbCrLf & "                                                                    HITECH_MERCHAN.dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo"
        _Str &= vbCrLf & "                                             GROUP BY X.FTOrderNo) AS L1"
        _Str &= vbCrLf & "                      WHERE     (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, A.FNHSysCustId, C.FTCustCode, C.FTCustNameEN AS FTCustName"
        _Str &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId"
        _Str &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId  "
        _Str &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON "
        _Str &= vbCrLf & "  	A.FNHSysSeasonId = SEAS.FNHSysSeasonId"
        _Str &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PMC WITH(NOLOCK)  ON  A.FNHSysVenderPramId=PMC.FNHSysVenderPramId"
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

    End Sub

    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTInvoiceNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTInvoiceNo_EditValueChanged), New Object() {sender, e})
        Else
            FTPostUser.Text = ""
            FDPostDate.Text = ""

            Call LoadOrderPriceInfo(FTInvoiceNo.Text)
            Me.FTStatePostXML.Checked = False
            Me.otbdetail.SelectedTabPageIndex = 0
        End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function VerifyDataPgm() As Boolean

        Dim _dt As System.Data.DataTable
        With CType(Me.GridOrderList.DataSource, System.Data.DataTable)
            .AcceptChanges()

            _dt = .Copy
        End With

        If _dt.Rows.Count > 0 Then
            Dim grp As List(Of String) = (_dt.Select("FTOrderNo<>''", "FTPGMCode").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FTPGMCode")) _
                                                   .Distinct() _
                                                   .ToList()


            _dt.Dispose()

            If grp.Count > 1 Then
                HI.MG.ShowMsg.mInfo("ข้อมูล Program Code มีมากกว่า 1 Program กรุณาทำการตรวจสอบ !!!", 1510280546, Me.Text, , MessageBoxIcon.Warning)
                Return False
            End If
        Else
            'HI.MG.ShowMsg.mInfo("ข้อมูล Program Code มีมากกว่า 1 Program กรุณาทำการตรวจสอบ !!!", 1510280546, Me.Text, , MessageBoxIcon.Warning)
            'Return False
        End If


        Return True
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.FTCustomerPO.Text <> "" Then
            If Me.FTInvoiceNo.Text <> "" Then
                If Me.FDInvoiceDate.Text <> "" Then

                    If FNExchangeRate.Value <= 0 Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNExchangeRate_lbl.Text)
                        Exit Sub
                    End If

                    Dim _dt As System.Data.DataTable
                    With CType(Me.ogcdetail.DataSource, System.Data.DataTable)
                        .AcceptChanges()

                        _dt = .Copy
                    End With

                    If _dt.Select("FTPOLineItem=''").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Line Item กรุณาทำการตรวจสอบ !!!", 1507210855, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If _dt.Select("FTCurCode=''").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล สกุลเงิน กรุณาทำการตรวจสอบ !!!", 1507210856, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If _dt.Select("FNPriceFOB=0 OR FNNetPrice=0 OR FNFirstPrice=0").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบข้อมูลราคาบางรายการ เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210857, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    'If Me.FNCMP.Value <= 0 Then
                    '    HI.MG.ShowMsg.mInfo("พบข้อมูลราคา CMP เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210858, Me.Text, , MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If

                    If _dt.Select("FTStateHold='1'").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบข้อมูลราคาบางรายการ ถูก Hold ไว้ กรุณาทำการตรวจสอบ !!!", 1547219817, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If



                    If VerifyDataPgm() Then

                        If Me.SaveData() Then
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Else
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        End If

                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                    FDInvoiceDate.Focus()
                    FDInvoiceDate.SelectAll()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                FTInvoiceNo.Focus()
                FTInvoiceNo.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        FTStateMerApp.Checked = False
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Me.FTCustomerPO.Text <> "" Then
            If Me.FTInvoiceNo.Text <> "" Then
                If Me.FDInvoiceDate.Text <> "" Then

                    If Me.DeleteData() Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Call LoadOrderPriceInfo(FTInvoiceNo.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                    FDInvoiceDate.Focus()
                    FDInvoiceDate.SelectAll()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                FTInvoiceNo.Focus()
                FTInvoiceNo.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If

    End Sub

    Private Sub ocmexporttoxml_Click(sender As Object, e As EventArgs) Handles ocmexporttoxml.Click
        If Me.FTCustomerPO.Text <> "" Then
            If Me.FTInvoiceNo.Text <> "" Then
                If Me.FDInvoiceDate.Text <> "" Then
                    If FTInvoiceExportNo.Text <> "" Then
                        If Me.FDInvoiceExportDate.Text <> "" Then

                            If FNExchangeRate.Value <= 0 Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNExchangeRate_lbl.Text)
                                Exit Sub
                            End If

                            Dim _dt As System.Data.DataTable
                            With CType(Me.ogcdetail.DataSource, System.Data.DataTable)
                                .AcceptChanges()

                                _dt = .Copy
                            End With

                            If _dt.Select("FTPOLineItem=''").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Line Item กรุณาทำการตรวจสอบ !!!", 1507210855, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If _dt.Select("FTCurCode=''").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล สกุลเงิน กรุณาทำการตรวจสอบ !!!", 1507210856, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If _dt.Select("FNPriceFOB=0 OR FNNetPrice=0 OR FNFirstPrice=0").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("พบข้อมูลราคาบางรายการ เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210857, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            'If Me.FNCMP.Value <= 0 Then
                            '    HI.MG.ShowMsg.mInfo("พบข้อมูลราคา CMP เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210858, Me.Text, , MessageBoxIcon.Warning)
                            '    Exit Sub
                            'End If

                            If _dt.Select("FTStateHold='1'").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("พบข้อมูลราคาบางรายการ ถูก Hold ไว้ กรุณาทำการตรวจสอบ !!!", 1547219817, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If VerifyDataPgm() = False Then
                                Exit Sub
                            End If

                            If Me.SaveData() Then
                                If ExportToXMLFile() Then
                                    Dim _Qry As String = ""

                                    If (Me.FTStatePostXML.Checked) Then

                                        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice"
                                        _Qry &= vbCrLf & "  SET   FTPostUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & " , FDPostDate=" & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & " , FTPostTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
                                        _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                        _Qry = "   Select Top 1 FTPostUser,FDPostDate,FTPostTime	 "
                                        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice As A With(NOLOCK)"
                                        _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
                                        _Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

                                        Dim _dtdataxml As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                                        For Each Rxi As DataRow In _dtdataxml.Rows

                                            FTPostUser.Text = Rxi!FTPostUser.ToString
                                            FDPostDate.Text = HI.UL.ULDate.ConvertEN(Rxi!FDPostDate.ToString) & "  " & Rxi!FTPostTime.ToString

                                            Exit For

                                        Next
                                    End If

                                Else
                                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการ Export File XML ได้ กรุณาทำการตรวจสอบพื้นที่จัดเก็บ File !!!", 1721458874, Me.Text,, MessageBoxIcon.Warning)
                                End If
                            Else

                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                            FDInvoiceDate.Focus()
                            FDInvoiceDate.SelectAll()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                        FTInvoiceNo.Focus()
                        FTInvoiceNo.SelectAll()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                    FDInvoiceDate.Focus()
                    FDInvoiceDate.SelectAll()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                FTInvoiceNo.Focus()
                FTInvoiceNo.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If
    End Sub

    Private Sub ReposPrice_KeyDown(sender As Object, e As KeyEventArgs) Handles ReposPrice.KeyDown
        'Try
        '    With Me.ogvbreakdown
        '        If .FocusedRowHandle < 0 Then Exit Sub
        '        Select Case e.KeyCode
        '            Case Keys.F9
        '                Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value

        '                For I As Integer = 0 To .RowCount - 1
        '                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
        '                        Select Case GridCol.FieldName.ToString.ToUpper
        '                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
        '                            Case Else
        '                                .SetRowCellValue(I, GridCol.FieldName.ToString, _Value)
        '                        End Select

        '                    Next
        '                Next

        '                CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
        '                ogcbreakdown.RefreshDataSource()
        '            Case Keys.F10
        '                Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value


        '                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
        '                    Select Case GridCol.FieldName.ToString.ToUpper
        '                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
        '                        Case Else
        '                            .SetFocusedRowCellValue(GridCol.FieldName.ToString, _Value)
        '                    End Select

        '                Next

        '                CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
        '                ogcbreakdown.RefreshDataSource()
        '            Case Keys.F11
        '                Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value

        '                For I As Integer = 0 To .RowCount - 1
        '                    .SetRowCellValue(I, .FocusedColumn.FieldName.ToString, _Value)
        '                Next

        '                CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
        '                ogcbreakdown.RefreshDataSource()
        '        End Select
        '    End With


        'Catch ex As Exception

        'End Try
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
                    _Qry &= vbCrLf & "   , ST.FTStyleCode"
                    _Qry &= vbCrLf & "   , CT.FTCustCode"
                    _Qry &= vbCrLf & "   , O.FTPORef"
                    _Qry &= vbCrLf & "   , O.FDOrderDate"
                    _Qry &= vbCrLf & "   ,ISNULL(("
                    _Qry &= vbCrLf & " 	SELECT TOP 1 FDShipDate"
                    _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK)"
                    _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
                    _Qry &= vbCrLf & " 	ORDER BY    FDShipDate ASC"
                    _Qry &= vbCrLf & " 	),'') AS FDShipDate"
                    _Qry &= vbCrLf & " ,ISNULL(("
                    _Qry &= vbCrLf & " 	SELECT SUM(FNGrandQuantity) AS FNGrandQuantity"
                    _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS Sub WITH(NOLOCK)"
                    _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
                    _Qry &= vbCrLf & " 	),0) AS FNGrandQuantity "

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

                    For Each R As DataRow In _dt.Rows

                        Me.FDShipDate.Text = R!FDShipDate.ToString
                        Me.FDOrderDate.Text = R!FDOrderDate.ToString

                        Exit For

                    Next

                Else

                    Me.FDShipDate.Text = ""
                    Me.FDOrderDate.Text = ""
                    'FNHSysStyleId.Text = ""
                    'FNHSysCustId.Text = ""

                End If

            Else

                Me.FDShipDate.Text = ""
                Me.FDOrderDate.Text = ""
                'FNHSysStyleId.Text = ""
                'FNHSysCustId.Text = ""

            End If

        End If
    End Sub


    Private Function PostFileToWebService(StrXML As String, Optional StateRoot As Boolean = False) As Boolean
        Try
            Dim bline As Byte()
            Dim str As String
            Dim xml As String

            'CHANGE HERE use the real path of the pdf physical path here. 
            'bline = System.IO.File.ReadAllBytes("C:\Sample.pdf")
            'str = Convert.ToBase64String(bline)


            Dim data As String = StrXML

            'Modify by Num 20150912 Change Url
            'Dim url As String = "https://service-qa.nikeconnect.com/services/Customs_EFS.Customs_EFSHttpSoap11Endpoint"
            'Dim url As String = "https://service.nikeconnect.com/services/Customs_EFS?wsdl"
            'Modify by Num 20150912 Change Url
            'Dim url As String = "https://fin.fusionaws.nike.com/customs/efs?wsdl"
            'Modify by Num 20171010 Change Url

            'Modify by Num 20180129 Change Url
            ' Dim url As String = "https://fin.fusionaws.nike.com/customs/efs?wsdl"
            'Modify by Num 20171010 Change Url

            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            Dim url As String = "https://fin-qa.fusionaws.nike.com/customs/efs?wsdl"
            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            Dim responsestring As String = ""

            Dim myReq As HttpWebRequest = WebRequest.Create(url)
            Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
            Dim proxyaddress As String
            Dim myProxy As New WebProxy()
            Dim encoding As New ASCIIEncoding
            Dim buffer() As Byte = encoding.GetBytes(StrXML)

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            myReq.ContentType = "text/xml; charset=UTF-8"
            myReq.ContentLength = buffer.Length

            'Modify by Num 20150912 Change Url
            '  myReq.Headers.Add("SOAPAction", "https://service-qa.nikeconnect.com/services/Customs_EFS.Customs_EFSHttpSoap11Endpoint")
            'myReq.Headers.Add("SOAPAction", "https://service.nikeconnect.com/services/Customs_EFS?wsdl")
            'Modify by Num 20150912 Change Url
            'myReq.Headers.Add("SOAPAction", "https://fin.fusionaws.nike.com/customs/efs?wsdl")

            'Modify by Num 20180129 Change Url
            myReq.Headers.Add("SOAPAction", "https://fin.fusionaws.nike.com/customs/efs?wsdl")
            'Modify by Num 20180129 Change Url

            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            ' myReq.Headers.Add("SOAPAction", "https://fin-qa.fusionaws.nike.com/customs/efs?wsdl")
            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang

            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            Dim Cer As New System.Security.Cryptography.X509Certificates.X509Certificate()
            Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\securevdi.nike.com.cer")
            'If StateRoot = False Then
            '    Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\EntrustCertificationAuthority-L1K.CRT")
            'Else
            '    Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\EntrustRootCertificationAuthority-G2.CRT")
            'End If

            myReq.ClientCertificates.Add(Cer)
            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang

            'Modify by Num 20171010 Change Url
            ' myReq.Credentials = New NetworkCredential("abc", "123")
            myReq.PreAuthenticate = True
            proxyaddress = proxy.GetProxy(myReq.RequestUri).ToString

            Dim newUri As New Uri(proxyaddress)
            myProxy.Address = newUri
            ' myReq.Proxy = myProxy
            Dim post As Stream = myReq.GetRequestStream

            post.Write(buffer, 0, buffer.Length)
            post.Close()

            Dim response As HttpWebResponse = DirectCast(myReq.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)

            Dim responseFromServer As String = reader.ReadToEnd()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    Private Function PostFileToWebServiceWithCerT(StrXML As String, Optional StateRoot As Boolean = False) As Boolean
        Try


            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)


            Dim PageCount As Integer = 0
            Dim urlEndPoint As String = "https://fin.fusionaws.nike.com/customs/efs?wsdl"
            ' Refer to the documentation for more information on how to get the client id/secret '"https://fin-qa.fusionaws.nike.com/customs/efs?wsdl"


            Dim responsestring As String = ""
            ' -- Refresh the access token
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(urlEndPoint)
            request.UseDefaultCredentials = True
            request.PreAuthenticate = True
            request.Credentials = CredentialCache.DefaultCredentials

            request.Method = "POST"
            ' request.ContentType = "application/x-www-form-urlencoded"

            Dim Cer As New System.Security.Cryptography.X509Certificates.X509Certificate()
            'Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\securevdi.nike.com.cer")
            Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\fusion_service_cert.cer")
            request.ClientCertificates.Add(Cer)



            Dim json_data As String = StrXML

            Dim postBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(json_data)

            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Dim postStream As Stream = request.GetRequestStream()
            postStream.Write(postBytes, 0, postBytes.Length)
            postStream.Flush()
            postStream.Close()


            Try

                ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
                Using response As System.Net.HttpWebResponse = request.GetResponse()
                    'Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())

                    '    Dim jsonResponseText = streamReader.ReadToEnd()

                    'End Using

                    If response.StatusCode = HttpStatusCode.OK Then
                        Return True
                    Else
                        Return False
                    End If

                End Using

            Catch ex As Exception
                Return False
            End Try

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    Private Function PostFileToWebService_Backup(StrXML As String, Optional StateRoot As Boolean = False) As Boolean
        Try
            Dim bline As Byte()
            Dim str As String
            Dim xml As String

            'CHANGE HERE use the real path of the pdf physical path here. 
            'bline = System.IO.File.ReadAllBytes("C:\Sample.pdf")
            'str = Convert.ToBase64String(bline)


            Dim data As String = StrXML

            'Modify by Num 20150912 Change Url
            'Dim url As String = "https://service-qa.nikeconnect.com/services/Customs_EFS.Customs_EFSHttpSoap11Endpoint"
            'Dim url As String = "https://service.nikeconnect.com/services/Customs_EFS?wsdl"
            'Modify by Num 20150912 Change Url
            'Dim url As String = "https://fin.fusionaws.nike.com/customs/efs?wsdl"
            'Modify by Num 20171010 Change Url

            'Modify by Num 20180129 Change Url
            ' Dim url As String = "https://fin.fusionaws.nike.com/customs/efs?wsdl"
            'Modify by Num 20171010 Change Url

            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            Dim url As String = "https://fin-qa.fusionaws.nike.com/customs/efs?wsdl"
            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            Dim responsestring As String = ""

            Dim myReq As HttpWebRequest = WebRequest.Create(url)
            Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
            Dim proxyaddress As String
            Dim myProxy As New WebProxy()
            Dim encoding As New ASCIIEncoding
            Dim buffer() As Byte = encoding.GetBytes(StrXML)

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            myReq.ContentType = "text/xml; charset=UTF-8"
            myReq.ContentLength = buffer.Length

            'Modify by Num 20150912 Change Url
            '  myReq.Headers.Add("SOAPAction", "https://service-qa.nikeconnect.com/services/Customs_EFS.Customs_EFSHttpSoap11Endpoint")
            'myReq.Headers.Add("SOAPAction", "https://service.nikeconnect.com/services/Customs_EFS?wsdl")
            'Modify by Num 20150912 Change Url
            'myReq.Headers.Add("SOAPAction", "https://fin.fusionaws.nike.com/customs/efs?wsdl")

            'Modify by Num 20180129 Change Url
            ' myReq.Headers.Add("SOAPAction", "https://fin.fusionaws.nike.com/customs/efs?wsdl")
            'Modify by Num 20180129 Change Url

            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            myReq.Headers.Add("SOAPAction", "https://fin-qa.fusionaws.nike.com/customs/efs?wsdl")
            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang

            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang
            Dim Cer As New System.Security.Cryptography.X509Certificates.X509Certificate()

            If StateRoot = False Then
                Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\EntrustCertificationAuthority-L1K.CRT")
            Else
                Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\EntrustRootCertificationAuthority-G2.CRT")
            End If

            myReq.ClientCertificates.Add(Cer)
            'Modify by Num 20190624 Change Url Mail Kholchapol Chuensawang

            'Modify by Num 20171010 Change Url
            ' myReq.Credentials = New NetworkCredential("abc", "123")
            myReq.PreAuthenticate = True
            proxyaddress = proxy.GetProxy(myReq.RequestUri).ToString

            Dim newUri As New Uri(proxyaddress)
            myProxy.Address = newUri
            ' myReq.Proxy = myProxy
            Dim post As Stream = myReq.GetRequestStream

            post.Write(buffer, 0, buffer.Length)
            post.Close()

            Dim response As HttpWebResponse = DirectCast(myReq.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)

            Dim responseFromServer As String = reader.ReadToEnd()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    Private Function PostFileToWebService1st(StrXML As String, Optional StateRoot As Boolean = False) As Boolean
        Try
            Dim bline As Byte()
            Dim str As String
            Dim xml As String

            'CHANGE HERE use the real path of the pdf physical path here. 
            'bline = System.IO.File.ReadAllBytes("C:\Sample.pdf")
            'str = Convert.ToBase64String(bline)


            Dim data As String = StrXML

            'Modify by Num 20150912 Change Url
            'Dim url As String = "https://service-qa.nikeconnect.com/services/Customs_EFS.Customs_EFSHttpSoap11Endpoint"
            'Dim url As String = "https://service.nikeconnect.com/services/Customs_EFS?wsdl"
            'Modify by Num 20150912 Change Url
            Dim url As String = "https://fin.fusionaws.nike.com/customs/efs?wsdl"
            'Modify by Num 20171010 Change Url

            Dim responsestring As String = ""

            Dim myReq As HttpWebRequest = WebRequest.Create(url)
            Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
            Dim proxyaddress As String
            Dim myProxy As New WebProxy()
            Dim encoding As New ASCIIEncoding
            Dim buffer() As Byte = encoding.GetBytes(StrXML)

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            myReq.ContentType = "text/xml; charset=UTF-8"
            myReq.ContentLength = buffer.Length

            'Modify by Num 20150912 Change Url
            '  myReq.Headers.Add("SOAPAction", "https://service-qa.nikeconnect.com/services/Customs_EFS.Customs_EFSHttpSoap11Endpoint")
            'myReq.Headers.Add("SOAPAction", "https://service.nikeconnect.com/services/Customs_EFS?wsdl")
            'Modify by Num 20150912 Change Url
            'myReq.Headers.Add("SOAPAction", "https://fin.fusionaws.nike.com/customs/efs?wsdl")
            'Dim Cer As New System.Security.Cryptography.X509Certificates.X509Certificate()

            'If StateRoot = False Then
            '    Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\EntrustCertificationAuthority-L1K.CRT")
            'Else
            '    Cer = New System.Security.Cryptography.X509Certificates.X509Certificate(Application.StartupPath + "\FILENIKE\EntrustRootCertificationAuthority-G2.CRT")
            'End If

            'myReq.ClientCertificates.Add(Cer)
            ''Modify by Num 20171010 Change Url
            '' myReq.Credentials = New NetworkCredential("abc", "123")
            'myReq.PreAuthenticate = True
            'proxyaddress = proxy.GetProxy(myReq.RequestUri).ToString

            'Dim newUri As New Uri(proxyaddress)
            'myProxy.Address = newUri
            ' myReq.Proxy = myProxy
            Dim post As Stream = myReq.GetRequestStream

            post.Write(buffer, 0, buffer.Length)
            post.Close()

            Dim response As HttpWebResponse = DirectCast(myReq.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)

            Dim responseFromServer As String = reader.ReadToEnd()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function
    Private Sub ogvdetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvdetail.CellMerge
        Try

            With Me.ogvdetail
                Select Case e.Column.FieldName
                    Case "FTPOLineItem"

                        If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString And "" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_MouseDown(sender As Object, e As MouseEventArgs) Handles ogvdetail.MouseDown

        'Dim tmpview As DevExpress.XtraGrid.Views.Grid.GridView = sender
        'Dim hInfo As GridHitInfo = tmpview.CalcHitInfo(e.X, e.Y)

        'If (hInfo.InRowCell) Then

        '    If Not (m_mergedCellsEdited Is Nothing) Then

        '        If (ogcdetail.Contains(m_mergedCellEditorPOLineItem)) Then
        '            ogcdetail.Controls.Remove(m_mergedCellEditorPOLineItem)
        '            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
        '                With ogvdetail
        '                    .SetRowCellValue(cellInfo.RowHandle, "FTPOLineItem", m_mergedCellEditorPOLineItem.Text.Trim())

        '                End With
        '            Next

        '        End If

        '        m_mergedCellsEdited = Nothing
        '    End If

        '    Dim vInfo As GridViewInfo = tmpview.GetViewInfo()
        '    Dim cInfo As GridCellInfo = vInfo.GetGridCellInfo(hInfo)
        '    Select Case cInfo.Column.FieldName.ToString
        '        Case "FTPOLineItem"
        '            If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
        '                If (m_mergedCellsEdited IsNot Nothing) Then
        '                    ogcdetail.Controls.Remove(m_mergedCellEditorPOLineItem)
        '                End If

        '                m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells

        '                ogcdetail.Controls.Add(m_mergedCellEditorPOLineItem)
        '                m_mergedCellEditorPOLineItem.Bounds = cInfo.Bounds
        '                'm_mergedCellEditorPOLineItem.Text = cInfo.CellValue.ToString
        '                'm_mergedCellEditorPOLineItem.EditValue = cInfo.CellValue.ToString
        '                m_mergedCellEditorPOLineItem.Focus()
        '                m_mergedCellEditorPOLineItem.SelectAll()
        '            End If
        '    End Select
        'End If

    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With Me.ogvdetail
                Select Case e.Column.FieldName.ToString.ToLower
                    Case "FTPOLineItem".ToLower

                        If "" & .GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString) = "" Then
                            e.Appearance.BackColor = Color.Orange
                        End If

                    Case "FNPriceFOB".ToLower

                        If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString)) = 0 Then
                            e.Appearance.BackColor = Color.Orange
                        End If

                    Case "FNNetPrice".ToLower

                        If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString)) = 0 Then
                            e.Appearance.BackColor = Color.Orange
                        End If

                    Case "FNFirstPrice".ToLower

                        If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString)) = 0 Then
                            e.Appearance.BackColor = Color.Orange
                        Else
                            If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString)) > Val("" & .GetRowCellValue(e.RowHandle, "FNPriceFOB")) Then
                                ' e.Appearance.ForeColor = Color.Red
                                e.Appearance.BackColor = Color.Red
                            End If
                        End If

                    Case "FTCurCode".ToLower

                        If "" & .GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString) = "" Then
                            e.Appearance.BackColor = Color.Orange
                        End If

                    Case "FNFirstPricePer".ToLower

                        If Val("" & .GetRowCellValue(e.RowHandle, "FNFirstPrice")) > 0 And Val("" & .GetRowCellValue(e.RowHandle, "FNNetPrice")) > 0 Then

                            If Val("" & .GetRowCellValue(e.RowHandle, "FNFirstPricePer")) > 0 And Val("" & .GetRowCellValue(e.RowHandle, "FNFirstPricePer")) < 10 Then
                                e.Appearance.BackColor = Color.GreenYellow
                            Else
                                e.Appearance.BackColor = Color.Orange
                            End If

                        End If

                End Select

                If "" & .GetRowCellValue(e.RowHandle, "FTStateHold") = "1" Then
                    e.Appearance.ForeColor = Color.Red
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTCustomerPO.Text <> "" Then
            If Me.FTInvoiceNo.Text <> "" Then
                If Me.FDInvoiceDate.Text <> "" Then
                    If FTInvoiceExportNo.Text <> "" Then
                        If Me.FDInvoiceExportDate.Text <> "" Then

                            If FNExchangeRate.Value <= 0 Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNExchangeRate_lbl.Text)
                                Exit Sub
                            End If

                            Dim _dt As System.Data.DataTable
                            With CType(Me.ogcdetail.DataSource, System.Data.DataTable)
                                .AcceptChanges()

                                _dt = .Copy
                            End With

                            If _dt.Select("FTPOLineItem=''").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Line Item กรุณาทำการตรวจสอบ !!!", 1507210855, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If _dt.Select("FTCurCode=''").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล สกุลเงิน กรุณาทำการตรวจสอบ !!!", 1507210856, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If _dt.Select("FNPriceFOB=0 OR FNNetPrice=0 OR FNFirstPrice=0").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("พบข้อมูลราคาบางรายการ เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210857, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            'If Me.FNCMP.Value <= 0 Then
                            '    HI.MG.ShowMsg.mInfo("พบข้อมูลราคา CMP เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210858, Me.Text, , MessageBoxIcon.Warning)
                            '    Exit Sub
                            'End If

                            If Me.SaveData() Then

                                Dim _Fm As String = ""
                                _Fm = "{TACCTXMLCreateInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "' "
                                _Fm &= " And {TACCTXMLCreateInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "

                                With New HI.RP.Report
                                    .FormTitle = Me.Text
                                    .ReportFolderName = "Account\"
                                    .ReportName = "ReportTransactionValueWorkSheet.rpt"
                                    .Formular = _Fm
                                    .Preview()
                                End With

                            Else
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                            FDInvoiceDate.Focus()
                            FDInvoiceDate.SelectAll()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                        FTInvoiceNo.Focus()
                        FTInvoiceNo.SelectAll()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                    FDInvoiceDate.Focus()
                    FDInvoiceDate.SelectAll()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                FTInvoiceNo.Focus()
                FTInvoiceNo.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If


    End Sub

    Private Sub FNAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNAmt.EditValueChanged

        Me.FTAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNAmt.Value)
        Me.FTAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNAmt.Value)

    End Sub

    Private Sub ocmpreviewtvwthb_Click(sender As Object, e As EventArgs) Handles ocmpreviewtvwthb.Click
        If Me.FTCustomerPO.Text <> "" Then
            If Me.FTInvoiceNo.Text <> "" Then
                If Me.FDInvoiceDate.Text <> "" Then
                    If FTInvoiceExportNo.Text <> "" Then
                        If Me.FDInvoiceExportDate.Text <> "" Then

                            If FNExchangeRate.Value <= 0 Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNExchangeRate_lbl.Text)
                                Exit Sub
                            End If

                            Dim _dt As System.Data.DataTable
                            With CType(Me.ogcdetail.DataSource, System.Data.DataTable)
                                .AcceptChanges()

                                _dt = .Copy
                            End With

                            If _dt.Select("FTPOLineItem=''").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Line Item กรุณาทำการตรวจสอบ !!!", 1507210855, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If _dt.Select("FTCurCode=''").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล สกุลเงิน กรุณาทำการตรวจสอบ !!!", 1507210856, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If _dt.Select("FNPriceFOB=0 OR FNNetPrice=0 OR FNFirstPrice=0").Length > 0 Then
                                HI.MG.ShowMsg.mInfo("พบข้อมูลราคาบางรายการ เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210857, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If Me.FNCMP.Value <= 0 Then
                                HI.MG.ShowMsg.mInfo("พบข้อมูลราคา CMP เป็น 0 กรุณาทำการตรวจสอบ !!!", 1507210858, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                            If Me.SaveData() Then

                                Dim _Fm As String = ""
                                _Fm = "{TACCTXMLCreateInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "' "
                                _Fm &= " And {TACCTXMLCreateInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "

                                With New HI.RP.Report
                                    .FormTitle = Me.Text
                                    .ReportFolderName = "Account\"
                                    .ReportName = "ReportTransactionValueWorkSheet_THB.rpt"
                                    .Formular = _Fm
                                    .Preview()
                                End With

                            Else
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                            FDInvoiceDate.Focus()
                            FDInvoiceDate.SelectAll()
                        End If
                    Else

                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                        FTInvoiceNo.Focus()
                        FTInvoiceNo.SelectAll()

                    End If

                Else

                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                    FDInvoiceDate.Focus()
                    FDInvoiceDate.SelectAll()

                End If
            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                FTInvoiceNo.Focus()
                FTInvoiceNo.SelectAll()

            End If
        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()

        End If

    End Sub

    Private Sub FTInvoiceExportNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTInvoiceExportNo.EditValueChanged
    End Sub

    Private Function SendJSONXML(EFSData As EFSHeader) As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0

        Dim OktaurlEndPoint As String = "https://nike-qa.oktapreview.com/oauth2/ausa0mcornpZLi0C40h7/v1/token" 'test
        '  Dim OktaurlEndPoint As String = "https://nike.okta.com/oauth2/aus27z7p76as9Dz0H1t7/v1/token" 'Production

        Dim EFSurlEndPoint As String = "https://api.gflstnc.non.thecommons.nike.com/efs/v1/upload" 'test
        '  Dim EFSurlEndPoint As String = "https://api.gflstnc.prd.thecommons.nike.com/efs/v1/upload" 'Production


        ' Refer to the documentation for more information on how to get the client id/secret
        'Dim clientid As String = "niketrade.efs.hit"
        'Dim clientsecret As String = "WI4FxjQvBFXqdJfawye9Y28SlIeTd1JnWvtxzNKbjySMN21SFd5G5mQzmMipfU15"

        Dim clientid As String = "niketrade.efs.hit"
        Dim clientsecret As String = "WI4FxjQvBFXqdJfawye9Y28SlIeTd1JnWvtxzNKbjySMN21SFd5G5mQzmMipfU15"


        Dim granttype As String = "client_credentials"
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""


        ' -- Refresh the access token
        Dim request As System.Net.WebRequest = System.Net.HttpWebRequest.Create(OktaurlEndPoint)
        request.UseDefaultCredentials = True
        request.PreAuthenticate = True
        request.Credentials = CredentialCache.DefaultCredentials

        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"


        Dim json_data As String = String.Format("client_id={0}&client_secret={1}&scope=trade.efs.create&grant_type=client_credentials", System.Web.HttpUtility.UrlEncode(clientid), System.Web.HttpUtility.UrlEncode(clientsecret))

        Dim postBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(json_data)

        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        Dim postStream As Stream = request.GetRequestStream()
        postStream.Write(postBytes, 0, postBytes.Length)
        postStream.Flush()
        postStream.Close()


        Try
            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Using response As System.Net.WebResponse = request.GetResponse()
                Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                    ' Parse the JSON the way you prefer
                    Dim jsonResponseText As String = streamReader.ReadToEnd()
                    Dim jsonResult As RefreshTokenResultJSON = JsonConvert.DeserializeObject(Of RefreshTokenResultJSON)(jsonResponseText)
                    accessToken = jsonResult.access_token

                    If accessToken <> "" Then

                        Dim requestpost As System.Net.WebRequest = System.Net.HttpWebRequest.Create(EFSurlEndPoint)
                        Dim postStreamdata As Stream = request.GetRequestStream()

                        Dim EFSjson_data As String = JsonConvert.SerializeObject(EFSData)
                        Dim EFSpostBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(EFSjson_data)

                        postStreamdata.Write(EFSpostBytes, 0, EFSpostBytes.Length)
                        postStreamdata.Flush()
                        postStreamdata.Close()

                        Using responsepost As System.Net.WebResponse = requestpost.GetResponse()
                            Using streamReaderpost As System.IO.StreamReader = New System.IO.StreamReader(responsepost.GetResponseStream())

                                Dim postjsonResponseText As String = streamReader.ReadToEnd()

                            End Using
                        End Using

                    Else
                        Return False
                    End If

                End Using
            End Using
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Sub ogbmainprocbutton_Paint(sender As Object, e As PaintEventArgs) Handles ogbmainprocbutton.Paint

    End Sub
End Class
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wBIReceiveSummary

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        chartControl.CrosshairOptions.ShowArgumentLine = False

        Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
            If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                Continue For
            End If
            comboChartType.Properties.Items.Add(type)
        Next type
        comboChartType.SelectedItem = ViewType.Bar
        chartControl.DataSource = pivotGridControl

    End Sub

#Region "Chart"
    '<comboChartType>
    Private Sub comboBoxEdit2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboChartType.SelectedIndexChanged
        chartControl.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))
        If chartControl.SeriesTemplate.Label IsNot Nothing Then
            chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
            checkShowPointLabels.Enabled = True
        Else
            checkShowPointLabels.Enabled = False
        End If
        If (TryCast(chartControl.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
            chartControl.Legend.Visible = True
        End If
        If TypeOf chartControl.Diagram Is Diagram3D Then
            Dim diagram As Diagram3D = CType(chartControl.Diagram, Diagram3D)
            diagram.RuntimeRotation = True
            diagram.RuntimeZooming = True
            diagram.RuntimeScrolling = True
        End If
        For Each series As Series In chartControl.Series
            Dim supportTransparency As ISupportTransparency = TryCast(series.View, ISupportTransparency)
            If supportTransparency IsNot Nothing Then
                If (TypeOf series.View Is AreaSeriesView) OrElse (TypeOf series.View Is Area3DSeriesView) OrElse (TypeOf series.View Is RadarAreaSeriesView) OrElse (TypeOf series.View Is Bar3DSeriesView) Then
                    supportTransparency.Transparency = 135
                Else
                    supportTransparency.Transparency = 0
                End If
            End If
        Next series
    End Sub
    '</comboChartType>

    '<checkShowPointLabels>
    Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabels.CheckedChanged
        chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
    End Sub
    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVertical_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVertical.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnly.CheckedChanged
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelay_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelay.EditValueChanged
        pivotGridControl.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
    End Sub
    '</seUpdateDelay>


#End Region

#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try

            _Qry = "SELECT  LEFT(B.FDReceiveDate,4) AS FTYear"
            _Qry &= vbCrLf & "   ,RIGHT(LEFT(B.FDReceiveDate,7),2) AS FTMonth"
            _Qry &= vbCrLf & "   ,S.FTSuplCode"
            _Qry &= vbCrLf & "   ,ISNULL(IM.FTRawMatCode,'') AS  FTRawMatCode"
            _Qry &= vbCrLf & "   ,Cmp.FTCmpCode"
            _Qry &= vbCrLf & "   ,WM.FTWHCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "   ,S.FTSuplNameTH AS FTSuplName"
                _Qry &= vbCrLf & "   ,Cmp.FTCmpNameTH AS FTCmpName"
            Else
                _Qry &= vbCrLf & "   ,S.FTSuplNameEN AS FTSuplName"
                _Qry &= vbCrLf & "   ,Cmp.FTCmpNameEN AS FTCmpName"
            End If

            _Qry &= vbCrLf & " ,Sum(ISNULL(B.FNQuantity,0))  AS FNRcvQuantity"
            _Qry &= vbCrLf & "  ,Sum(ISNULL(B.FNNetAmt,0)) AS FNRcvAmt"
            _Qry &= vbCrLf & "  ,Sum(((ISNULL(C.FNQuantity,0) ))) AS FNRtsQuantity"
            _Qry &= vbCrLf & "  ,Sum(((ISNULL(C.FNNetAmt,0)  ))) AS FNRtsAmt  "
            _Qry &= vbCrLf & "    FROM"
            _Qry &= vbCrLf & " (SELECT        A.FTPurchaseNo"
            _Qry &= vbCrLf & "  , A.FDPurchaseDate"
            _Qry &= vbCrLf & "  , A.FNHSysSuplId"
            _Qry &= vbCrLf & "  , A.FNHSysCmpId"
            _Qry &= vbCrLf & "  , A.FNHSysCurId"
            _Qry &= vbCrLf & "  , A.FNExchangeRate,A.FNHSysRawMatId"
            _Qry &= vbCrLf & "  , SUM(Convert(numeric(18,2),A.FNNetPrice*FNQuantity)) AS FNNetAmt,Sum(FNQuantity ) AS FNQuantity"
            _Qry &= vbCrLf & " FROM ( SELECT        A.FTPurchaseNo"
            _Qry &= vbCrLf & "  , A.FDPurchaseDate"
            _Qry &= vbCrLf & "  , A.FNHSysSuplId"
            _Qry &= vbCrLf & "  , A.FNHSysCmpId"
            _Qry &= vbCrLf & "  , A.FNHSysCurId"
            _Qry &= vbCrLf & "  , A.FNExchangeRate,B.FNHSysRawMatId"
            _Qry &= vbCrLf & "  ,(B.FNPrice - B.FNDisAmt)  AS FNNetPrice,Sum(FNQuantity ) AS FNQuantity"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS B  WITH(NOLOCK)  ON A.FTPurchaseNo = B.FTPurchaseNo"          
            _Qry &= vbCrLf & " GROUP BY A.FTPurchaseNo, A.FDPurchaseDate, A.FNHSysSuplId, A.FNHSysCmpId, A.FNHSysCurId, A.FNExchangeRate,B.FNHSysRawMatId,B.FNPrice,B.FNDisAmt) AS A"
            _Qry &= vbCrLf & "  GROUP BY A.FTPurchaseNo, A.FDPurchaseDate, A.FNHSysSuplId, A.FNHSysCmpId, A.FNHSysCurId, A.FNExchangeRate,A.FNHSysRawMatId ) AS A"
            _Qry &= vbCrLf & "   INNER JOIN"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT    FTReceiveNo,FTPurchaseNo,FNHSysCmpId,FNHSysWHId,FDReceiveDate,FNHSysRawMatId,(Convert(numeric(18,2),FNPricePerStock* FNQuantity)) AS FNNetAmt,FNQuantity AS FNQuantity"
            _Qry &= vbCrLf & "  FROM ("
            _Qry &= vbCrLf & "  SELECT    A.FTReceiveNo,A.FTPurchaseNo,A.FNHSysCmpId,A.FNHSysWHId,A.FDReceiveDate,B.FNHSysRawMatId,B.FTOrderNo,B.FNPricePerStock,SUM(B.FNQuantityStock) AS FNQuantity"
            _Qry &= vbCrLf & " FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A  WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK)   ON A.FTReceiveNo = B.FTReceiveNo"
            _Qry &= vbCrLf & "  WHERE LEFT( A.FDReceiveDate,7)>='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.FTYear.Text), 7) & "'"
            _Qry &= vbCrLf & "  AND LEFT( A.FDReceiveDate,7)<='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.FTYearTo.Text), 7) & "'"
            _Qry &= vbCrLf & "  GROUP BY A.FTReceiveNo,A.FTPurchaseNo,A.FNHSysCmpId,A.FNHSysWHId,A.FDReceiveDate,B.FNHSysRawMatId,B.FTOrderNo,B.FNPricePerStock )B"
            _Qry &= vbCrLf & "  ) AS B  ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId "
            _Qry &= vbCrLf & "  Left Join"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & "SELECT   FTPurchaseNo,FNHSysRawMatId,SUM(FNNetAmt) AS FNNetAmt,Sum(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & " FROM (SELECT   A.FTPurchaseNo,C.FNHSysRawMatId, SUM(Convert(numeric(18,2), B.FNQuantity * C.FNPrice)) AS FNNetAmt,Sum(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B  WITH(NOLOCK)  ON A.FTReturnSuplNo = B.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK)   ON B.FTBarcodeNo = C.FTBarcodeNo"
            _Qry &= vbCrLf & " GROUP BY A.FTPurchaseNo,C.FNHSysRawMatId"
            _Qry &= vbCrLf & " UNION ALL "
            _Qry &= vbCrLf & " SELECT   A.FTPurchaseNo,C.FNHSysRawMatId, SUM(Convert(numeric(18,2), B.FNQuantity * C.FNPrice)) AS FNNetAmt,Sum(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS B  WITH(NOLOCK)  ON A.FTReturnSuplNo = B.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK)   ON B.FTBarcodeNo = C.FTBarcodeNo"
            _Qry &= vbCrLf & " GROUP BY A.FTPurchaseNo,C.FNHSysRawMatId ) AS BB"
            _Qry &= vbCrLf & " GROUP BY FTPurchaseNo,FNHSysRawMatId"
            _Qry &= vbCrLf & " ) AS C  ON A.FTPurchaseNo = C.FTPurchaseNo AND A.FNHSysRawMatId = C.FNHSysRawMatId"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON A.FNHSysSuplId =S.FNHSysSuplId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CU WITH(NOLOCK) ON A.FNHSysCurId =CU.FNHSysCurId"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON A.FNHSysRawMatId =IM.FNHSysRawMatId"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON B.FNHSysCmpId =Cmp.FNHSysCmpId"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WM WITH(NOLOCK) ON B.FNHSysWHId =WM.FNHSysWHId"
            _Qry &= vbCrLf & " GROUP BY  LEFT(B.FDReceiveDate,4) "
            _Qry &= vbCrLf & "  ,RIGHT(LEFT(B.FDReceiveDate,7),2) "
            _Qry &= vbCrLf & "  ,S.FTSuplCode"
            _Qry &= vbCrLf & "  ,ISNULL(IM.FTRawMatCode,'')"
            _Qry &= vbCrLf & "   ,Cmp.FTCmpCode"
            _Qry &= vbCrLf & "   ,WM.FTWHCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "   ,S.FTSuplNameTH "
                _Qry &= vbCrLf & "   ,Cmp.FTCmpNameTH"
            Else
                _Qry &= vbCrLf & "   ,S.FTSuplNameEN "
                _Qry &= vbCrLf & "   ,Cmp.FTCmpNameEN"
            End If

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.pivotGridControl.DataSource = dt.Copy

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FTYear.Text <> "" And Me.FTYearTo.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1496130001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

End Class
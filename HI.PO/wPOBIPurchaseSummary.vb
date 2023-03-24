Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wPOBIPurchaseSummary

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
            _Qry = " SELECT  LEFT(A.FDPurchaseDate,4) AS FTYear"
            _Qry &= vbCrLf & "   ,RIGHT(LEFT(A.FDPurchaseDate,7),2) AS FTYear"
            _Qry &= vbCrLf & "   ,A.FNMSysSuplId"
            _Qry &= vbCrLf & "  ,A.FNMSysCurId"
            _Qry &= vbCrLf & "  ,SUM(A.FNQuantity) AS FNPOQuantity"
            _Qry &= vbCrLf & "  ,Sum(Convert(numeric(18,2),A.FNNetAmt *  A.FNExchangeRate)) AS FNNetAmt"
            _Qry &= vbCrLf & "  ,Sum(ISNULL(B.FNQuantity,0) - (ISNULL(C.FNQuantity,0) + ISNULL(C.FNQuantity,0)))  AS FNRcvQuantity"
            _Qry &= vbCrLf & "   ,Sum(ISNULL(B.FNNetAmt,0) - (ISNULL(C.FNNetAmt,0) + ISNULL(D.FNNetAmt,0) )) AS FNRcvAmt"
            _Qry &= vbCrLf & "  ,Sum(((ISNULL(C.FNQuantity,0) + ISNULL(C.FNQuantity,0)))) AS FNRtsQuantity"
            _Qry &= vbCrLf & "   ,Sum(((ISNULL(C.FNNetAmt,0) + ISNULL(D.FNNetAmt,0) ))) AS FNRtsAmt  "
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & "  (SELECT        A.FTPurchaseNo"
            _Qry &= vbCrLf & "  , A.FDPurchaseDate"
            _Qry &= vbCrLf & "  , A.FNMSysSuplId"
            _Qry &= vbCrLf & "  , A.FNMSysCurId"
            _Qry &= vbCrLf & "  , A.FNExchangeRate"
            _Qry &= vbCrLf & "  , SUM(B.FNNetAmt) AS FNNetAmt,Sum(FNQuantity ) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dboTPURTPurchase AS A INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dboTPURTPurchase_OrderNo AS B ON A.FTPurchaseNo = B.FTPurchaseNo"
            _Qry &= vbCrLf & "  WHERE LEFT( A.FDPurchaseDate,7)='2014/07'"
            _Qry &= vbCrLf & "  GROUP BY A.FTPurchaseNo, A.FDPurchaseDate, A.FNMSysSuplId, A.FNMSysCurId, A.FNExchangeRate) AS A"

            _Qry &= vbCrLf & "  LEFT JOIN"
            _Qry &= vbCrLf & "  ("

            _Qry &= vbCrLf & "  SELECT    A.FTPurchaseNo,SUM(Convert(numeric(18,2), A.FNExchangeRate* B.FNNetAmt)) AS FNNetAmt,Sum(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dboTINVENReceive AS A INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dboTINVENReceive_Detail AS B ON A.FTReceiveNo = B.FTReceiveNo"
            _Qry &= vbCrLf & "  GROUP BY A.FTPurchaseNo"
            _Qry &= vbCrLf & "  ) AS B  ON A.FTPurchaseNo = B.FTPurchaseNo"

            _Qry &= vbCrLf & "   Left Join"
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "  SELECT   A.FTPurchaseNo, SUM(Convert(numeric(18,2), B.FNQuantity * C.FNPrice)) AS FNNetAmt,Sum(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dboTINVENReturnToSupplier AS A INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dboTINVENBarcode_OUT AS B ON A.FTReturnSuplNo = B.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dboTINVENBarcode AS C ON B.FTBarcodeNo = C.FTBarcodeNo"
            _Qry &= vbCrLf & "  GROUP BY A.FTPurchaseNo"
            _Qry &= vbCrLf & "  ) AS C  ON A.FTPurchaseNo = C.FTPurchaseNo"


            _Qry &= vbCrLf & "  GROUP BY  LEFT(A.FDPurchaseDate,4) "
            _Qry &= vbCrLf & "   ,RIGHT(LEFT(A.FDPurchaseDate,7),2) "
            _Qry &= vbCrLf & "  ,A.FNMSysSuplId"
            _Qry &= vbCrLf & "   ,A.FNMSysCurId"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

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
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1196130001, Me.Text, , Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

   
End Class
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wMERBIOrderSummary

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

        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String ="FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNOrderGrandQuantity"
        Dim sFieldSumAmt As String =  "FNOrderGrandAmt|FNOrderTestAmt|FNOrderExtraAmt|FNOrderAmt"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNOrderGrandQuantity"

        Dim sFieldGrpSumAmt As String = "FNOrderGrandAmt|FNOrderTestAmt|FNOrderExtraAmt|FNOrderAmt"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTCurCode").Group()

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

            _Qry = "Select FTOrderNo"
            _Qry &= vbCrLf & "   ,FTCmpCode"
            _Qry &= vbCrLf & "   ,FTCmpName"
            _Qry &= vbCrLf & "  ,FNPrice"
            _Qry &= vbCrLf & " ,FNQuantity"
            _Qry &= vbCrLf & "  ,Convert(numeric(18,2),FNQuantity * FNPrice) AS FNOrderAmt"
            _Qry &= vbCrLf & "  ,FNQuantityExtra"
            _Qry &= vbCrLf & " ,Convert(numeric(18,2),FNQuantityExtra * FNPrice) AS FNOrderExtraAmt"
            _Qry &= vbCrLf & " ,FNGarmentQtyTest"
            _Qry &= vbCrLf & "   ,Convert(numeric(18,2),FNGarmentQtyTest * FNPrice) AS FNOrderTestAmt"
            _Qry &= vbCrLf & "	,FNQuantity+FNQuantityExtra+FNGarmentQtyTest AS FNOrderGrandQuantity"
            _Qry &= vbCrLf & "	,Convert(numeric(18,2),(FNQuantity+FNQuantityExtra+FNGarmentQtyTest) * FNPrice) AS FNOrderGrandAmt"
            _Qry &= vbCrLf & "   ,FTCurCode"
            _Qry &= vbCrLf & "  ,FTCustCode"
            _Qry &= vbCrLf & ",FTCustName"
            _Qry &= vbCrLf & ",FTOrderBy AS FTInsUser"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDOrderDate) = 1 Then  Convert(nvarchar(10),  Convert(datetime,FDOrderDate) ,103) Else '' END AS FDOrderDate "
            _Qry &= vbCrLf & "	,FTStyleCode"
            _Qry &= vbCrLf & "	,FTStyleName"
            _Qry &= vbCrLf & ",FTPORef"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDShipDate) = 1 Then  Convert(nvarchar(10),  Convert(datetime,FDShipDate) ,103) Else '' END AS  FDShipDate"
            _Qry &= vbCrLf & ",FTCountryCode"
            _Qry &= vbCrLf & ",FTCountryName"
            _Qry &= vbCrLf & ",FDOrderYear"
            _Qry &= vbCrLf & "	,FDOrderMonth"
            _Qry &= vbCrLf & ",FDOrderShipYear"
            _Qry &= vbCrLf & ",FDOrderShiptMonth"
            _Qry &= vbCrLf & "	,FNYearTerm"
            _Qry &= vbCrLf & "	,FTPlantCode"
            _Qry &= vbCrLf & "	,FTPlantName,FTSeasonCode,FTOrderTypeName,FTNikePOLineItem"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & " (SELECT O.FTOrderNo,ISNULL(OSS.FTSeasonCode,'') As FTSeasonCode"
            _Qry &= vbCrLf & " , C.FTCmpCode"

            _Qry &= vbCrLf & " , ISNULL(OSubB.FTNikePOLineItem,'') AS FTNikePOLineItem"


            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , MAX(C.FTCmpNameTH) AS FTCmpName"
                _Qry &= vbCrLf & " , MAX(Cus.FTCustNameTH) AS FTCustName"
                _Qry &= vbCrLf & " , MAX(ST.FTStyleNameTH) AS FTStyleName"
                _Qry &= vbCrLf & " , MAX(Country.FTCountryNameTH) AS FTCountryName"
                _Qry &= vbCrLf & " , MAX(PLT.FTPlantNameTH) AS FTPlantName"

                _Qry &= vbCrLf & " , MAX(ISNULL(LST.FTNameTH,'')) AS FTOrderTypeName"

            Else
                _Qry &= vbCrLf & " , MAX(C.FTCmpNameEN) AS FTCmpName"
                _Qry &= vbCrLf & " , MAX(Cus.FTCustNameEN) AS   FTCustName"
                _Qry &= vbCrLf & " , MAX(ST.FTStyleNameEN) AS FTStyleName"
                _Qry &= vbCrLf & " , MAX(Country.FTCountryNameEN) AS FTCountryName"
                _Qry &= vbCrLf & " , MAX(PLT.FTPlantNameEN) AS FTPlantName"

                _Qry &= vbCrLf & " , MAX(ISNULL(LST.FTNameEN,'')) AS FTOrderTypeName"
            End If

            _Qry &= vbCrLf & " , OSubB.FNPrice"
            _Qry &= vbCrLf & " , Sum(ISNULL(OSubB.FNQuantity, 0)) AS FNQuantity"
            _Qry &= vbCrLf & " , SUM(ISNULL(OSubB.FNQuantityExtra, 0)) AS FNQuantityExtra"
            _Qry &= vbCrLf & " , SUM(ISNULL(OSubB.FNGarmentQtyTest, 0)) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & ", Cur.FTCurCode"
            _Qry &= vbCrLf & ", Cus.FTCustCode"
            _Qry &= vbCrLf & ", PLT.FTPlantCode"

            _Qry &= vbCrLf & ", O.FTOrderBy"
            _Qry &= vbCrLf & " , O.FDOrderDate"
            _Qry &= vbCrLf & " , ST.FTStyleCode"
            _Qry &= vbCrLf & " , O.FTPORef"
            _Qry &= vbCrLf & " , OSub.FDShipDate"
            _Qry &= vbCrLf & " , Country.FTCountryCode"
            _Qry &= vbCrLf & " , LEFT(O.FDOrderDate, 4) AS FDOrderYear"
            _Qry &= vbCrLf & " , RIGHT(LEFT(O.FDOrderDate, 7), 2) AS FDOrderMonth"
            _Qry &= vbCrLf & " , LEFT(OSub.FDShipDate, 4) AS FDOrderShipYear"
            _Qry &= vbCrLf & " , RIGHT(LEFT(OSub.FDShipDate, 7), 2) AS FDOrderShiptMonth"
            _Qry &= vbCrLf & " ,CASE WHEN Month(OSub.FDShipDate) >=0  AND Month(OSub.FDShipDate) <=4 Then 1 "
            _Qry &= vbCrLf & "  WHEN Month(OSub.FDShipDate) >=5  AND Month(OSub.FDShipDate) <=8 Then 2 "
            _Qry &= vbCrLf & "  WHEN Month(OSub.FDShipDate) >=9  AND Month(OSub.FDShipDate) <=12 Then 3"
            _Qry &= vbCrLf & " END AS FNYearTerm"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK) ON O.FNHSysCmpId = C.FNHSysCmpId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OSub WITH(NOLOCK) ON O.FTOrderNo = OSub.FTOrderNo INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS Cur WITH(NOLOCK) ON OSub.FNHSysCurId = Cur.FNHSysCurId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OSubB WITH(NOLOCK) ON OSub.FTOrderNo = OSubB.FTOrderNo AND OSub.FTSubOrderNo = OSubB.FTSubOrderNo INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS Cus ON O.FNHSysCustId = Cus.FNHSysCustId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST ON O.FNHSysStyleId = ST.FNHSysStyleId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS Country ON OSub.FNHSysCountryId = Country.FNHSysCountryId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS PLT ON O.FNHSysPlantId = PLT.FNHSysPlantId"


            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS OSS WITH(NOLOCK) ON O.FNHSysSeasonId = OSS.FNHSysSeasonId"

            _Qry &= vbCrLf & " LEFT OUTER JOIN  ( "

            _Qry &= vbCrLf & "    Select  FTListName, FNListIndex As FNOrderType, FTNameTH, FTNameEN "
            _Qry &= vbCrLf & "  From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
            _Qry &= vbCrLf & "  Where (FTListName = N'FNOrderType') "
            _Qry &= vbCrLf & "  ) As LST On O.FNOrderType = LST.FNOrderType"

            _Qry &= vbCrLf & "  WHERE  O.FTOrderNo <> ''  "

            If FNHSysCmpId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysCmpId =" & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ""
            End If

            If FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND OSub.FDShipDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "' "
            End If

            If FTEndShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND OSub.FDShipDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "' "
            End If

            If FTStartOrderDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDOrderDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "' "
            End If

            If FTEndOrderDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDOrderDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "' "
            End If

            If FNHSysCustId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysCustId =" & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & ""
            End If

            If FTStartCreateDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDInsDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartCreateDate.Text) & "' "
            End If

            If FTEndCreateDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDInsDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndCreateDate.Text) & "' "
            End If

            _Qry &= vbCrLf & " GROUP BY O.FTOrderNo,ISNULL(OSS.FTSeasonCode,'')"
            _Qry &= vbCrLf & ", C.FTCmpCode"
            _Qry &= vbCrLf & ", OSubB.FNPrice"
            _Qry &= vbCrLf & ", Cur.FTCurCode"
            _Qry &= vbCrLf & ", Cus.FTCustCode"
            _Qry &= vbCrLf & ", O.FTOrderBy"
            _Qry &= vbCrLf & ",O.FDOrderDate"
            _Qry &= vbCrLf & ", ST.FTStyleCode"
            _Qry &= vbCrLf & ",  O.FTPORef"
            _Qry &= vbCrLf & ", OSub.FDShipDate"
            _Qry &= vbCrLf & " , ISNULL(OSubB.FTNikePOLineItem,'') "
            _Qry &= vbCrLf & ", Country.FTCountryCode"
            _Qry &= vbCrLf & ", PLT.FTPlantCode"
            _Qry &= vbCrLf & ", LEFT(O.FDOrderDate, 4), RIGHT(LEFT(O.FDOrderDate, 7), 2), LEFT(OSub.FDShipDate, 4), RIGHT(LEFT(OSub.FDShipDate, 7), 2)) AS A"
            _Qry &= vbCrLf & " ORDER BY  FTCurCode"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.ogc.DataSource = dt.Copy
            Me.pivotGridControl.DataSource = dt.Copy
            Me.otb.SelectedTabPage = otpsummary
            Me.ogv.ExpandAllGroups()

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

        Call LoadData()

    End Sub

End Class
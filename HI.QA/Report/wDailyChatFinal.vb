
Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils
Imports System.Windows.Forms.VScrollProperties
Imports System.Windows.Forms.DragDropEffects
Imports System.Drawing
Imports System.Data.SqlClient

Public Class wDailyChatFinal

    Private _oDtReport As DataTable
    Private _oDtSubReport As DataTable
    Private _wQADailyDetail As wQADailyDetailPreFinal
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()


        hideContainerLeft.Visible = False
        ' Add any initialization after the InitializeComponent() call.
        Dim types() As String = System.Enum.GetNames(GetType(ViewType))

        Try
            For Each t As String In types
                ocViewType.Properties.Items.Add(t)
            Next
        Catch ex As Exception
        End Try

        'oDockPanelDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden

        'Dim restrictedTypes() As ViewType = {ViewType.Pie3D, ViewType.Pie, ViewType.Doughnut, ViewType.Doughnut3D}

        'For Each type As ViewType In restrictedTypes 'System.Enum.GetValues(GetType(ViewType))
        '    ocViewTypePie.Properties.Items.Add(type)
        'Next type

        _wQADailyDetail = New wQADailyDetailPreFinal
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wQADailyDetail.Name.ToString.Trim, _wQADailyDetail)
        Catch ex As Exception
        Finally
        End Try

        Try
            For Each t As String In types
                ocViewTypePie.Properties.Items.Add(t)
            Next
            'ocViewTypePie.SelectedItem = ViewType.Pie3D
        Catch ex As Exception
        End Try

        Call InitGrid()

    End Sub

    Dim oChart As New DevExpress.XtraCharts.ChartControl
    Private Sub LoadChart(ByVal _oDt As DataTable, ByVal _oDtd As DataTable)
        Try

            'oChart.ClearSelection()
            oChart = New DevExpress.XtraCharts.ChartControl

            ' Create the first side-by-side bar series and add points to it.
            Dim _Date As Date = Date.Now.ToString("d")
            Dim series1 As New Series(Me.FTSeriesName.Text & " " & Me.SFTDateTrans.Text & " - " & Me.EFTDateTrans.Text & "", CType(Me.ocViewType.SelectedIndex, ViewType))
            Dim series2 As New Series(Me.FTSeriesName.Text & " - " & _Date & "", CType(Me.ocViewType.SelectedIndex, ViewType))
            Dim _UnitSectCodeBefore As String = ""
            Dim _Where As String = ""
            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FTUnitSectCode.ToString, New Double() {CDbl("0" & R!FTRejectPer.ToString)}))
                _Where = ""

                If _UnitSectCodeBefore <> "" Then
                    _Where = " AND FTUnitSectCode > '" & HI.UL.ULF.rpQuoted(_UnitSectCodeBefore) & "'"
                End If

                'If _UnitSectCodeBefore = "" Then
                '    _UnitSectCodeBefore = HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString)
                'End If

                '_Where = " AND FTUnitSectCode > '" & HI.UL.ULF.rpQuoted(_UnitSectCodeBefore) & "'"

                'For Each Rx As DataRow In _oDtd.Select("(FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString) & "' " & _Where & ") OR FTUnitSectCode='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString) & "'")
                '    For Each Rs As DataRow In _oDt.Rows
                '        If Rx!FTUnitSectCode.ToString <> Rs!FTUnitSectCode.ToString Then
                '            series1.Points.Add(New SeriesPoint(Rx!FTUnitSectCode.ToString, New Double() {CDbl("0")}))
                '        End If
                '    Next

                '    series2.Points.Add(New SeriesPoint(Rx!FTUnitSectCode.ToString, New Double() {CDbl("0" & Rx!FTRejectPer.ToString)}))
                'Next

                'If _oDtd.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString) & "'").Length > 0 Then
                '    For Each Rx As DataRow In _oDtd.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString) & "'")
                '        For Each Rs As DataRow In _oDt.Rows
                '            If Rx!FTUnitSectCode.ToString <> Rs!FTUnitSectCode.ToString Then
                '                series1.Points.Add(New SeriesPoint(Rx!FTUnitSectCode.ToString, New Double() {CDbl("0")}))
                '            End If
                '        Next

                '        series2.Points.Add(New SeriesPoint(Rx!FTUnitSectCode.ToString, New Double() {CDbl("0" & Rx!FTRejectPer.ToString)}))
                '    Next

                'Else
                '    series2.Points.Add(New SeriesPoint(R!FTUnitSectCode.ToString, New Double() {CDbl("0")}))
                'End If

                If _oDtd.Select(" FTUnitSectCode='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString) & "'").Length > 0 Then
                    For Each Rx As DataRow In _oDtd.Select(" FTUnitSectCode='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString) & "'")
                        series2.Points.Add(New SeriesPoint(Rx!FTUnitSectCode.ToString, New Double() {CDbl("0" & Rx!FTRejectPer.ToString)}))
                    Next
                Else
                    series2.Points.Add(New SeriesPoint(R!FTUnitSectCode.ToString, New Double() {CDbl("0")}))
                End If


                _UnitSectCodeBefore = HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString)
            Next

            ' Add the series to the chart.
            oChart.Series.Add(series1)
            oChart.Series.Add(series2)
            ' Add a title to the chart (if necessary).
            'oChart.SeriesTemplate.LabelsVisibility = If(True, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            'oChart.CrosshairEnabled = If(True, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

            'CType(oChart.SeriesTemplate.Label, BarSeriesLabel).Position = BarSeriesLabelPosition.Top

            Try

                If TypeOf oChart.Diagram Is Diagram3D Then

                    Dim diagram As Diagram3D = CType(oChart.Diagram, Diagram3D)
                    diagram.RuntimeRotation = True
                    diagram.RuntimeZooming = True
                    diagram.RuntimeScrolling = True

                End If

            Catch ex As Exception
            End Try

            Dim chartTitle1 As New ChartTitle()

            chartTitle1.Text = Me.FTTitleChart.Text
            oChart.Titles.Add(chartTitle1)
            oChart.Dock = DockStyle.Fill


            Me.ogrpChart.Controls.Clear()
            Me.ogrpChart.Controls.Add(oChart)
        Catch ex As Exception

        End Try
    End Sub

    Private oChartPie As DevExpress.XtraCharts.ChartControl
    Private Sub LoadChartDoughnut(ByVal _oDt As DataTable)
        Try
            'oChart.ClearSelection()



            oChartPie = New DevExpress.XtraCharts.ChartControl
            ' Create the first side-by-side bar series and add points to it.
            'Dim series1 As New Series(Me.FTSeriesName.Text, CType(Me.ocViewTypePie.SelectedItem, ViewType))
            Dim series1 As New Series(Me.FTSeriesName.Text, CType(Me.ocViewTypePie.SelectedIndex, ViewType))

            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FTQADetailName.ToString, New Double() {CInt("0" & R!Qty.ToString)}))
            Next
            ' Add the series to the chart.


            oChartPie.Series.Add(series1)
            'oChartPie.AllowDrop = False
            'oChartPie.DoDragDrop(oChartPie, DragDropEffects.Move)

            series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            series1.Label.PointOptions.ValueNumericOptions.Precision = 2

            Try
                'CType(oChartPie.Diagram, SimpleDiagram3D).RotationType = RotationType.UseMouseAdvanced
                'CType(oChartPie.Diagram, SimpleDiagram3D).RuntimeRotation = True
                'CType(oChartPie.Diagram, SimpleDiagram3D).RuntimeZooming = True
                If TypeOf oChartPie.Diagram Is Diagram3D Then
                    Dim diagram As Diagram3D = CType(oChartPie.Diagram, Diagram3D)
                    diagram.RuntimeRotation = True
                    diagram.RuntimeZooming = True
                    diagram.RuntimeScrolling = True
                End If
            Catch ex As Exception
            End Try

            'CType(oChartPie.Diagram, SimpleDiagram3D).RotationAngleX = -35
            'CType(oChartPie.Diagram, SimpleDiagram3D).Dimension = 2
            ' Add a title to the chart (if necessary).
            Dim chartTitle1 As New ChartTitle()

            chartTitle1.Text = Me.FTTitleTopChart.Text
            oChartPie.Titles.Add(chartTitle1)
            oChartPie.Dock = DockStyle.Fill

            Me.oGrpTopChart.Controls.Clear()
            Me.oGrpTopChart.Controls.Add(oChartPie)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDetailSubChart(ByVal _oDt As DataTable)
        Try
            Dim ochartSubDefect As New DevExpress.XtraCharts.ChartControl
            ' Create the first side-by-side bar series and add points to it.
            Dim series1 As New Series(Me.FTSeriesDetialDefet.Text, ViewType.Bar)
            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FDQADate.ToString, New Double() {CInt("0" & R!FNTotalDefect.ToString)}))
            Next
            ' Add the series to the chart.

            ochartSubDefect.Series.Add(series1)
            series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            series1.Label.PointOptions.ValueNumericOptions.Precision = 0

            ' Add a title to the chart (if necessary).
            Dim chartTitle1 As New ChartTitle()
            chartTitle1.Text = Me.FTTitleDetailChart.Text
            ochartSubDefect.Titles.Add(chartTitle1)
            ochartSubDefect.Dock = DockStyle.Fill

            Me.ogrpSubChart.Controls.Clear()
            Me.ogrpSubChart.Controls.Add(ochartSubDefect)

            'ochartSubDefect.Titles.Add(chartTitle1)
            'ochartSubDefect.Dock = DockStyle.Fill
            'Me.ogrpSubChart.Controls.Clear()
            'Me.ogrpSubChart.Controls.Add(ochartSubDefect)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadSubChart(ByVal _oDt As DataTable)
        Try

            ' Qty, FDQADate, FNHSysQADetailId, FTQADetailCode  ,(Qty * 100) /18 AS DefectPer

            Dim ochartSubDefect As New DevExpress.XtraCharts.ChartControl
            ' Create the first side-by-side bar series and add points to it.
            Dim series1 As New Series(FTSubChartSeries.Text, ViewType.Bar)
            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FTQADetailName.ToString, New Double() {CInt("0" & R!DefectPer.ToString)}))
            Next
            ' Add the series to the chart.

            ochartSubDefect.Series.Add(series1)
            'series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            series1.Label.PointOptions.ValueNumericOptions.Precision = 0
            series1.View.Color = System.Drawing.Color.Pink
            series1.View.Color = System.Drawing.Color.Brown


            ' Add a title to the chart (if necessary).
            Dim chartTitle1 As New ChartTitle()
            chartTitle1.Text = Me.FTTitleDetailSubChart.Text
            ochartSubDefect.Titles.Add(chartTitle1)
            ochartSubDefect.Dock = DockStyle.Fill

            Me.ogrpSubChartDefect.Controls.Clear()
            Me.ogrpSubChartDefect.Controls.Add(ochartSubDefect)

            'ochartSubDefect.Titles.Add(chartTitle1)
            'ochartSubDefect.Dock = DockStyle.Fill
            'Me.ogrpSubChart.Controls.Clear()
            'Me.ogrpSubChart.Controls.Add(ochartSubDefect)

        Catch ex As Exception

        End Try
    End Sub


#Region "Procedure"
    Private Sub LoadData()
        Dim _Cmd As String = ""
        Dim dt As New DataTable
        Dim _TotalQAActual As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try

            _Cmd = "SELECT     A.FNHSysUnitSectId,    Sum(A.FNQAInQty) AS FNQAInQty , Sum(A.FNQAAqlQty) AS FNQAAqlQty "
            _Cmd &= vbCrLf & "	, Sum(A.FNQAActualQty) AS FNQAActualQty ,Sum(A.FNMajorQty) as FNMajorQty , Sum(A.FNMinorQty) as FNMinorQty, Sum(ISNULL(A.FNAndon, 0)) AS FNAndon, "
            _Cmd &= vbCrLf & "           B.FTUnitSectCode   "
            _Cmd &= vbCrLf & "   , SUM(ISNULL    (X.FTStateReject, 0)) AS FTTotalDefect "

            _Cmd &= vbCrLf & "   , convert(numeric(18,2) ,  SUM(ISNULL    (X.FTStateReject, 0)) / SUM(A.FNQAActualQty)) AS FTRejectPer "
            ' and FNHSysStyleId =  A.FNHSysStyleId  and  FTOrderNo =  A.FTOrderNo and  FNHourNo =  A.FNHourNo

            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTQAPreFinal_H AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH (NOLOCK) ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"

            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  ON A.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo "

            _Cmd &= vbCrLf & " OUTER APPLY(  "

            _Cmd &= vbCrLf & "  SELECT SUM(FTStateReject) AS FTStateReject FROM ( Select   FNHSysStyleId , FNHSysUnitSectId , FTOrderNo , FDQADate , FNHourNo , FNSeq ,1  FTStateReject  "
            _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS X With(NOLOCK)  "

            _Cmd &= vbCrLf & "   WHERE   X.FNHSysUnitSectId =  A.FNHSysUnitSectId   and  X.FDQADate =  A.FDQADate and X.FNHSysStyleId =  A.FNHSysStyleId  and  X.FTOrderNo =  A.FTOrderNo  AND X.FNHourNo=A.FNHourNo  "
            _Cmd &= vbCrLf & "   GROUP BY FNHSysStyleId , FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq ) AS X "
            _Cmd &= vbCrLf & " )  AS X "

            _Cmd &= vbCrLf & " WHERE O.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""



            _Cmd &= vbCrLf & " And     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "')"
            _Cmd &= vbCrLf & " AND A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"

            If Me.FNHSysUnitSectId.Text <> "" Then
                _Cmd &= vbCrLf & " AND B.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND B.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If

            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & " group by  A.FNHSysUnitSectId , B.FTUnitSectCode , A.FDQADate , A.FNHSysStyleId , A.FTOrderNo " ' , A.FNHSysStyleId , A.FTOrderNo , A.FNHourNo
            _Cmd &= vbCrLf & " ORDER BY  B.FTUnitSectCode ASC "


            dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcMain.DataSource = dt

            _Cmd = "SELECT      A.FNHSysUnitSectId,    sum(A.FNQAInQty) AS FNQAInQty , sum(A.FNQAAqlQty) AS FNQAAqlQty "
            _Cmd &= vbCrLf & "	, sum(A.FNQAActualQty) AS FNQAActualQty ,SUM(ISNULL(X2.FNMajorQty,0)) as FNMajorQty , sum(ISNULL(X2.FNMinorQty,0)) as FNMinorQty, sum(ISNULL(A.FNAndon, 0)) AS FNAndon, "
            _Cmd &= vbCrLf & "           B.FTUnitSectCode   "
            _Cmd &= vbCrLf & "   , SUM(ISNULL    (X.FTStateReject, 0)) AS FTTotalDefect "

            _Cmd &= vbCrLf & "   ,convert(numeric(18,2) ,  SUM(ISNULL    (X.FTStateReject, 0)) / SUM(A.FNQAActualQty ) ) AS FTRejectPer "


            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTQAPreFinal_H AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH (NOLOCK) ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"

            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  ON A.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & " INNER  JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo "

            _Cmd &= vbCrLf & " OUTER APPLY(  "

            _Cmd &= vbCrLf & "  SELECT SUM(FTStateReject) AS FTStateReject FROM ( Select   FNHSysStyleId , FNHSysUnitSectId , FTOrderNo , FDQADate , FNHourNo , FNSeq ,1  FTStateReject  "
            _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS X With(NOLOCK)  "

            _Cmd &= vbCrLf & "   WHERE   X.FNHSysUnitSectId =  A.FNHSysUnitSectId   And  X.FDQADate =  A.FDQADate And X.FNHSysStyleId =  A.FNHSysStyleId  And  X.FTOrderNo =  A.FTOrderNo  "
            _Cmd &= vbCrLf & "   GROUP BY FNHSysStyleId , FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq) As X "
            _Cmd &= vbCrLf & " )  As X "

            _Cmd &= vbCrLf & " OUTER APPLY(  "

            _Cmd &= vbCrLf & "  Select   SUM(CASE WHEN X3.FTStateCtitical='0' THEN 1 ELSE 0 END) AS FNMinorQty,SUM(CASE WHEN X3.FTStateCtitical='0' THEN 0 ELSE 1 END) AS FNMajorQty  "
            _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail As X4 With(NOLOCK)  "
            _Cmd &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail  AS X3 WITH(NOLOCK) ON X4.FNHSysQADetailId = X3.FNHSysQADetailId "
            _Cmd &= vbCrLf & "   WHERE   X4.FNHSysUnitSectId =  A.FNHSysUnitSectId   And  X4.FDQADate =  A.FDQADate And X4.FNHSysStyleId =  A.FNHSysStyleId  And  X4.FTOrderNo =  A.FTOrderNo  "

            _Cmd &= vbCrLf & " )  As X2 "

            _Cmd &= vbCrLf & " WHERE O.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""


            _Cmd &= vbCrLf & "  And     (A.FDQADate =" & HI.UL.ULDate.FormatDateDB & ")"
            '_Cmd &= vbCrLf & " And A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"

            If Me.FNHSysUnitSectId.Text <> "" Then
                _Cmd &= vbCrLf & " AND B.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND B.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If

            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & " group by  A.FNHSysUnitSectId , B.FTUnitSectCode,A.FDQADate, A.FNHSysStyleId , A.FTOrderNo , A.FNHourNo , A.FDQADate"
            _Cmd &= vbCrLf & "ORDER BY B.FTUnitSectCode ASC "
            Dim _odtd As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            _oDtReport = dt
            Call LoadChart(dt, _odtd)


            _Cmd = " select sum(Qty) AS Qty From ( SELECT   Top 5   sum(Qty) AS Qty "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",  FTQADetailNameTH AS FTQADetailName"
            Else
                _Cmd &= vbCrLf & ", FTQADetailNameEN AS FTQADetailName "
            End If

            _Cmd &= vbCrLf & "FROM         (SELECT     COUNT(*) AS Qty, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN"
            _Cmd &= vbCrLf & "                 FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                                      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  ON A.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & " INNER  JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & " LEFt OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH (NOLOCK) ON A.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " WHERE O.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

            _Cmd &= vbCrLf & " AND Isnull(U.FTUnitSectCode,'') <> ''"
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Cmd &= vbCrLf & " AND U.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND U.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If
            _Cmd &= vbCrLf & "   GROUP BY A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN) AS T"
            _Cmd &= vbCrLf & " WHERE     (FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "')"
            _Cmd &= vbCrLf & " AND FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            _Cmd &= vbCrLf & " Group by FNHSysQADetailId, FTQADetailCode, FTQADetailNameTH, FTQADetailNameEN,FTQADetailNameTH"
            _Cmd &= vbCrLf & "Order by sum(Qty) desc ) as t"

            _TotalQAActual = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")


            _Cmd = "SELECT   Top 5   sum(Qty) AS Qty,   FNHSysQADetailId, FTQADetailCode  , convert(numeric(18,2) ,(sum(Qty) * 100) /  convert(numeric(18,2)," & _TotalQAActual & ")) AS DefectPer"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",  FTQADetailNameTH AS FTQADetailName"
            Else
                _Cmd &= vbCrLf & ", FTQADetailNameEN AS FTQADetailName "
            End If

            _Cmd &= vbCrLf & "FROM         (SELECT     COUNT(*) AS Qty, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN"
            _Cmd &= vbCrLf & "                 FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                                      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  ON A.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH (NOLOCK) ON A.FNHSysUnitSectId = U.FNHSysUnitSectId"

            _Cmd &= vbCrLf & " WHERE O.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

            _Cmd &= vbCrLf & "  AND Isnull(U.FTUnitSectCode,'') <> ''"
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Cmd &= vbCrLf & " AND U.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND U.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If
            _Cmd &= vbCrLf & "   GROUP BY A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN) AS T"
            _Cmd &= vbCrLf & " WHERE     (FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "')"
            _Cmd &= vbCrLf & " AND FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            _Cmd &= vbCrLf & " Group by FNHSysQADetailId, FTQADetailCode, FTQADetailNameTH, FTQADetailNameEN,FTQADetailNameTH"
            _Cmd &= vbCrLf & "Order by sum(Qty) desc"

            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcTopDefect.DataSource = _oDt

            Call LoadChartDoughnut(_oDt)
            _oDtSubReport = _oDt
            _Spls.Close()
        Catch ex As Exception

            Dim msgerr As String = ex.Message

            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click


        If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1406130001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            ocViewTypePie.SelectedIndex = 51
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocViewType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ocViewType.SelectedIndexChanged
        Try
            If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocViewTypePie_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ocViewTypePie.SelectedIndexChanged
        Try
            If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ogvMain_DoubleClick(sender As Object, e As EventArgs)
        Try
            With ogvMain
                If .RowCount <= 0 Or .FocusedRowHandle <= -1 Then Exit Sub
                Dim _UnitSectId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitSectId").ToString)
                Dim _UnitSectCode As String = .GetRowCellValue(.FocusedRowHandle, "FTUnitSectCode").ToString
                'oPanalControlDetail.Dock = DockStyle.Fill
                'oDockPanelDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                'hideContainerLeft.Visible = True
                'oDockPanelDetail.BringToFront()
                'LoadDataDetailDefect(_UnitSectId)
                With New wQADailyDetail
                    .FTUnitCode_lbl.Text = _UnitSectCode
                    .SysUnitSectId = _UnitSectId
                    .EDate = Me.EFTDateTrans.Text
                    .SDate = Me.SFTDateTrans.Text
                    .ShowDialog()
                End With
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadDataDetailDefect(_FNHSysUnitSectId As Integer)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
            _Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
            _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty ,(SUM(A.FNMajorQty)+SUM(A.FNMinorQty)) AS FNTotalDefect"
            _Cmd &= vbCrLf & "		, B.FTStyleCode, O.FTPORef"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
            _Cmd &= vbCrLf & " WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "')"
            _Cmd &= vbCrLf & " AND A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)

            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "AND B.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND B.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If

            _Cmd &= vbCrLf & "group by   A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   B.FTStyleCode, O.FTPORef"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDefectDetail.DataSource = _oDt


            Me.LoadDetailSubChart(_oDt)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataDetailSubDefect(_date As String, _FNHSysUnitSectId As Integer)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            '_Cmd = "SELECT     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
            '_Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
            '_Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty ,(SUM(A.FNMajorQty)+SUM(A.FNMinorQty)) AS FNTotalDefect"
            '_Cmd &= vbCrLf & "		, B.FTStyleCode, O.FTPORef"
            '_Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
            '_Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & HI.UL.ULDate.ConvertEnDB(_date) & "')"
            '_Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)
            '_Cmd &= vbCrLf & "group by   A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   B.FTStyleCode, O.FTPORef"




            _Cmd = "   SELECT   Top 5   Qty, FDQADate, FNHSysQADetailId, FTQADetailCode  ,(Qty * 100) /18 AS DefectPer"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",  FTQADetailNameTH AS FTQADetailName"
            Else
                _Cmd &= vbCrLf & ",  FTQADetailNameEN AS FTQADetailName"
            End If

            _Cmd &= vbCrLf & "FROM         (SELECT     COUNT(*) AS Qty, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN ,A.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON A.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"

            _Cmd &= vbCrLf & "GROUP BY A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN,A.FNHSysUnitSectId) AS T"
            _Cmd &= vbCrLf & "WHERE     (FDQADate = '" & HI.UL.ULDate.ConvertEnDB(_date) & "')"
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If

            _Cmd &= vbCrLf & "Order by Qty desc"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            LoadSubChart(_oDt)



            _Cmd = "   SELECT    Qty, FDQADate, FNHSysQADetailId, FTQADetailCode  ,(Qty * 100) /18 AS DefectPer"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",  FTQADetailNameTH AS FTQADetailName"
            Else
                _Cmd &= vbCrLf & ",  FTQADetailNameEN AS FTQADetailName"
            End If

            _Cmd &= vbCrLf & "FROM         (SELECT     COUNT(*) AS Qty, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN ,A.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON A.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            If Me.FNHSysPOID.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If

            _Cmd &= vbCrLf & "GROUP BY A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN,A.FNHSysUnitSectId) AS T"
            _Cmd &= vbCrLf & "WHERE     (FDQADate = '" & HI.UL.ULDate.ConvertEnDB(_date) & "')"
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)

            _Cmd &= vbCrLf & "Order by Qty desc"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDetailSubDefect.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitGrid()
        Try
            With ogvDetailSubDefect
                .ClearGrouping()
                .ClearDocument()

                .Columns("Qty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty")
                .Columns("Qty").SummaryItem.DisplayFormat = "{0:n0}"
            End With



            Dim _FSumMain As String = "FTTotalDefect|FNAndon|FNQAAqlQty|FNQAInQty|FNQAActualQty"
            With ogvMain
                .ClearGrouping()
                .ClearDocument()
                For Each Str As String In _FSumMain.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next


            End With

            With ogvDefectDetail
                .ClearGrouping()
                .ClearDocument()

                .Columns("FNTotalDefect").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalDefect")
                .Columns("FNTotalDefect").SummaryItem.DisplayFormat = "{0:n0}"

                .Columns("FNQAInQty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQAInQty")
                .Columns("FNQAInQty").SummaryItem.DisplayFormat = "{0:n0}"
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvDefectDetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvDefectDetail.DoubleClick
        Try
            With ogvDefectDetail
                If .RowCount <= 0 Or .FocusedRowHandle <= -1 Then Exit Sub
                Dim _UnitSectId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitSectId").ToString)

                Dim _UnitSectCode As String = ""
                _UnitSectCode = HI.Conn.SQLConn.GetField("SELECT   Top 1  FTUnitSectCode  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect WITH(NOLOCK) WHERE  FNHSysUnitSectId=" & _UnitSectId, Conn.DB.DataBaseName.DB_MASTER, "")
                Dim _Date As String = .GetRowCellValue(.FocusedRowHandle, "FDQADate").ToString

                oPanelDefectSubChart.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                hideContainerLeft.Visible = True
                oPanelDefectSubChart.BringToFront()

                Me.FTCaptionSubDefect.Text = _UnitSectCode & "   " & _Date
                LoadDataDetailSubDefect(_Date, _UnitSectId)



            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Preview()
        Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")
        Try

            Dim _Cmd As String = ""
            If _oDtReport.Rows.Count > 0 Then
                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Daily Where FTUserLogIn='" & HI.ST.UserInfo.UserName & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                For Each R As DataRow In _oDtReport.Rows
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Daily (FTUserLogIn, FNHSysUnitSectId, FNTotalDefect, FNQAInQty, FNQAAqlQty, FNQAActualQty, FNMajorQty, FNMinorQty, FNAndon, FNRejectPer, FTUnitSectCode)"
                    _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FNHSysUnitSectId)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FTTotalDefect)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FNQAInQty)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FNQAAqlQty)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FNQAActualQty)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FNMajorQty)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FNMinorQty)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FNAndon)
                    _Cmd &= vbCrLf & "," & CInt("0" & R!FTRejectPer)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString) & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                Next
            End If

            If _oDtSubReport.Rows.Count > 0 Then
                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_SubDaily Where FTUserLogIn='" & HI.ST.UserInfo.UserName & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                For Each r As DataRow In _oDtSubReport.Rows

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_SubDaily (FTUserLogIn, FNHSysQADetailId, FNQty, FTQADetailCode, FNDefect, FTQADetailName) "
                    _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & "," & CInt("0" & r!FNHSysQADetailId)
                    _Cmd &= vbCrLf & "," & CInt("0" & r!Qty)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(r!FTQADetailCode.ToString) & "'"
                    _Cmd &= vbCrLf & "," & CInt("0" & r!DefectPer)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(r!FTQADetailName.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                Next

            End If

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Img Where  FTUserLogIn='" & HI.ST.UserInfo.UserName & "' and FTStateApp='0'"
            '_Cmd &= vbCrLf & "INSERT INTO TmpTPRODTQA_Report_Img (FTUserLogIn,FTStateApp)"
            '_Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
            '_Cmd &= vbCrLf & ",'0'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()

            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Img (FTUserLogIn,FTStateApp,FTImage,FTImageSub)"
            _Cmd &= vbCrLf & "Select @FTUserLogIn"
            _Cmd &= vbCrLf & ",@FTStateApp"
            _Cmd &= vbCrLf & ",@Image"
            _Cmd &= vbCrLf & ",@ImageSub"
            Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
            cmd.Parameters.Add("@FTUserLogIn", SqlDbType.NVarChar, 100)
            cmd.Parameters.Add("@FTStateApp", SqlDbType.NVarChar, 1)
            cmd.Parameters.Add("@Image", SqlDbType.Image)
            cmd.Parameters.Add("@ImageSub", SqlDbType.Image)


            cmd.Parameters("@FTUserLogIn").Value = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            cmd.Parameters("@FTStateApp").Value = "0"

            Dim data As Byte() = Nothing

            data = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(oChart), UL.ULImage.PicType.Employee)
            cmd.Parameters("@Image").Value = data

            Dim data2 As Byte() = Nothing

            data2 = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(oChartPie), UL.ULImage.PicType.Employee)
            cmd.Parameters("@ImageSub").Value = data2



            '_Cmd = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Img "
            '_Cmd &= vbCrLf & "Set FTImage = @Image"
            '_Cmd &= vbCrLf & ",FTImageSub = @ImageSub"
            '_Cmd &= vbCrLf & "WHERE FTUserLogIn=@FTUserLogIn"
            '_Cmd &= vbCrLf & " and FTStateApp=@FTStateApp"

            'Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
            'cmd.Parameters.AddWithValue("@FTUserLogIn", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
            'cmd.Parameters.AddWithValue("@FTStateApp", "0")

            'Dim data As Byte() = Nothing


            'For Each Obj As Object In Me.Controls.Find("ogrpChart", True)
            '    Select Case HI.ENM.Control.GeTypeControl(Obj)
            '        Case ENM.Control.ControlType.GroupControl
            '            data = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(Me.ogrpChart), UL.ULImage.PicType.Employee)
            '    End Select
            'Next

            'Dim p As New SqlParameter("@Image", SqlDbType.Image)
            'p.Value = data
            'cmd.Parameters.Add(p)


            'Dim data2 As Byte() = Nothing

            'For Each Obj As Object In Me.Controls.Find("oGrpTopChart", True)
            '    Select Case HI.ENM.Control.GeTypeControl(Obj)
            '        Case ENM.Control.ControlType.GroupControl
            '            data2 = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(Me.oGrpTopChart), UL.ULImage.PicType.Employee)
            '    End Select
            'Next


            'Dim p2 As New SqlParameter("@ImageSub", SqlDbType.Image)
            'p2.Value = data2
            'cmd.Parameters.Add(p2)



            cmd.ExecuteNonQuery()

            cmd.Parameters.Clear()
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)



            With New HI.RP.Report

                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                '.Formular = _Formular
                .AddParameter("FDSDate", Me.SFTDateTrans.Text)
                .AddParameter("FDEDate", Me.EFTDateTrans.Text)
                .ReportName = "QADailyChartReport.rpt"
                _spls.Close()
                .Preview()
            End With
        Catch ex As Exception
            _spls.Close()
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            Me.Preview()
        Catch ex As Exception
        End Try
    End Sub

    Private Function TakeScreenShot(ByVal Control As Control) As Bitmap
        Try
            Dim Screenshot As New Bitmap(Control.Width, Control.Height)
            Control.DrawToBitmap(Screenshot, New Rectangle(0, 0, Control.Width, Control.Height))
            Return Screenshot
        Catch ex As Exception
            MsgBox(ex.ToString & " Function TakeScreenShot")
        End Try
    End Function


    Private Sub ogvMain_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvMain.RowCellClick
        Try
            If e.Column.Name.ToString = "cFTTotalDefect" Then
                Dim _UnitSectId As Integer = CInt("0" & ogvMain.GetRowCellValue(e.RowHandle, "FNHSysUnitSectId").ToString)
                Dim _wPopupDefect As wPopupDefectFinal
                _wPopupDefect = New wPopupDefectFinal
                Dim oSysLang As New ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wPopupDefect.Name.ToString.Trim, _wPopupDefect)
                Catch ex As Exception
                Finally
                End Try

                With _wPopupDefect
                    .UnitSectId = _UnitSectId
                    .TDate = Me.SFTDateTrans.Text
                    .TDateTo = Me.EFTDateTrans.Text
                    .State = "1"
                    .ShowDialog()
                End With

            ElseIf e.Column.Name.ToString = "cFTUnitSectCode" Then
                With ogvMain
                    If .RowCount <= 0 Or .FocusedRowHandle <= -1 Then Exit Sub
                    Dim _UnitSectId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitSectId").ToString)
                    Dim _UnitSectCode As String = .GetRowCellValue(.FocusedRowHandle, "FTUnitSectCode").ToString




                    With _wQADailyDetail

                        .FTUnitCode_lbl.Text = _UnitSectCode
                        .SysUnitSectId = _UnitSectId
                        .EDate = Me.EFTDateTrans.Text
                        .SDate = Me.SFTDateTrans.Text
                        .ShowDialog()
                    End With
                End With
            End If


        Catch ex As Exception

        End Try
    End Sub


End Class
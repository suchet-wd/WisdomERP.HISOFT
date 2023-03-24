Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils
Imports System.Windows.Forms.VScrollProperties
Imports System.Windows.Forms.DragDropEffects

Public Class wDailyChat

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

        Dim restrictedTypes() As ViewType = {ViewType.Pie3D, ViewType.Pie, ViewType.Doughnut, ViewType.Doughnut3D}

        For Each type As ViewType In restrictedTypes 'System.Enum.GetValues(GetType(ViewType))
            ocViewTypePie.Properties.Items.Add(type)
        Next type

        Call InitGrid()

    End Sub


    Private Sub LoadChart(ByVal _oDt As DataTable)
        Try

            'oChart.ClearSelection()
            Dim oChart As New DevExpress.XtraCharts.ChartControl

            ' Create the first side-by-side bar series and add points to it.
            Dim series1 As New Series(Me.FTSeriesName.Text, CType(Me.ocViewType.SelectedIndex, ViewType))
            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FTUnitSectCode.ToString, New Double() {CInt("0" & R!FTTotalDefect.ToString)}))
            Next



            ' Add the series to the chart.
            oChart.Series.Add(series1)

            ' Add a title to the chart (if necessary).
            Dim chartTitle1 As New ChartTitle()


            chartTitle1.Text = Me.FTTitleChart.Text
            oChart.Titles.Add(chartTitle1)
            oChart.Dock = DockStyle.Fill


            Me.ogrpChart.Controls.Clear()
            Me.ogrpChart.Controls.Add(oChart)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadChartDoughnut(ByVal _oDt As DataTable)
        Try
            'oChart.ClearSelection()


            Dim oChartPie As New DevExpress.XtraCharts.ChartControl

            ' Create the first side-by-side bar series and add points to it.
            Dim series1 As New Series(Me.FTSeriesName.Text, CType(Me.ocViewTypePie.SelectedItem, ViewType))
            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FTQADetailName.ToString, New Double() {CInt("0" & R!Qty.ToString)}))
            Next
            ' Add the series to the chart.


            oChartPie.Series.Add(series1)
            'oChartPie.AllowDrop = False
            'oChartPie.DoDragDrop(oChartPie, DragDropEffects.Move)
            series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            series1.Label.PointOptions.ValueNumericOptions.Precision = 0


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

            _Cmd = "SELECT   (sum(A.FNMajorQty)+ sum(A.FNMinorQty)) AS FTTotalDefect,  A.FNHSysUnitSectId,    sum(A.FNQAInQty) AS FNQAInQty , sum(A.FNQAAqlQty) AS FNQAAqlQty "
            _Cmd &= vbCrLf & "	, sum(A.FNQAActualQty) AS FNQAActualQty ,SUM(A.FNMajorQty) as FNMajorQty , sum(A.FNMinorQty) as FNMinorQty, sum(ISNULL(A.FNAndon, 0)) AS FNAndon, "
            _Cmd &= vbCrLf & "           B.FTUnitSectCode , ((sum(A.FNMajorQty)+ sum(A.FNMinorQty)) *100) / SUM(A.FNQAActualQty) AS FTRejectPer "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH (NOLOCK) ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "')"
            _Cmd &= vbCrLf & " AND A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            _Cmd &= vbCrLf & " group by  A.FNHSysUnitSectId , B.FTUnitSectCode"


            dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)
            Me.ogcMain.DataSource = dt

            Call LoadChart(dt)

            _Cmd = "SELECT   sum(A.FNQAActualQty) AS FNQAActualQty  "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH (NOLOCK) ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "')"
            _Cmd &= vbCrLf & " AND A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            _TotalQAActual = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")


            _Cmd = "SELECT   Top 5   Qty, FDQADate, FNHSysQADetailId, FTQADetailCode  ,(Qty * 100) /" & _TotalQAActual & " AS DefectPer"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",  FTQADetailNameTH AS FTQADetailName"
            Else
                _Cmd &= vbCrLf & ", FTQADetailNameEN AS FTQADetailName "
            End If

            _Cmd &= vbCrLf & "FROM         (SELECT     COUNT(*) AS Qty, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN"
            _Cmd &= vbCrLf & "                 FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                                      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "                GROUP BY A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN) AS T"
            _Cmd &= vbCrLf & " WHERE     (FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "')"
            _Cmd &= vbCrLf & " AND FDQADate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            _Cmd &= vbCrLf & "Order by Qty desc"

            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcTopDefect.DataSource = _oDt

            Call LoadChartDoughnut(_oDt)

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


        If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1406130001, Me.Text, , Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
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


    Private Sub ogvMain_DoubleClick(sender As Object, e As EventArgs) Handles ogvMain.DoubleClick
        Try
            With ogvMain
                If .RowCount <= 0 Or .FocusedRowHandle <= -1 Then Exit Sub
                Dim _UnitSectId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitSectId").ToString)

                oPanalControlDetail.Dock = DockStyle.Fill
                oDockPanelDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                hideContainerLeft.Visible = True
                oDockPanelDetail.BringToFront()

                LoadDataDetailDefect(_UnitSectId)


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
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "GROUP BY A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN,A.FNHSysUnitSectId) AS T"
            _Cmd &= vbCrLf & "WHERE     (FDQADate = '" & HI.UL.ULDate.ConvertEnDB(_date) & "')"
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)
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
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
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
End Class
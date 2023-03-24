Imports DevExpress.XtraCharts
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Drawing.Imaging
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient

Public Class wPaitentStaticChart


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call IniGrid()
    End Sub

    Private Sub IniGrid()
        Try
            With ogcTop
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsMenu.EnableColumnMenu = False
                .OptionsMenu.ShowAutoFilterRowItem = False
                .OptionsFilter.AllowFilterEditor = False
                .OptionsFilter.AllowColumnMRUFilterList = False
                .OptionsFilter.AllowMRUFilterList = False
                .OptionsSelection.MultiSelect = False
                .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
                .OptionsCustomization.AllowFilter = False
                .OptionsCustomization.AllowSort = False
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Not (VerrifyData()) Then Exit Sub
            Call LoadDataBar()
        Catch ex As Exception

        End Try
    End Sub



    Dim oChart As New DevExpress.XtraCharts.ChartControl
    Private Sub LoadChart(ByVal _oDt As DataTable)
        Try

            'oChart.ClearSelection()
            oChart = New DevExpress.XtraCharts.ChartControl

            ' Create the first side-by-side bar series and add points to it.
            Dim _Date As Date = Date.Now.ToString("d")
            'Dim series1 As New Series(Me.FTSeriesName.Text & " " & Me.SFTDateTrans.Text & " - " & Me.EFTDateTrans.Text & "", CType(Me.ocViewType.SelectedIndex, ViewType))
            'Dim series2 As New Series(Me.FTSeriesName.Text & " - " & _Date & "", CType(Me.ocViewType.SelectedIndex, ViewType))


            Dim series1 As New Series()
            For Each R As DataRow In _oDt.Select("FTTypeofDiseaseName <> ''")
                series1.Points.Add(New SeriesPoint(R!FTTypeofDiseaseName.ToString, New Double() {CInt("0" & R!FNHSysEmpId.ToString)}))
            Next



            ' Add the series to the chart.
            oChart.Series.Add(series1)

            ' Add a title to the chart (if necessary).

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


            'chartTitle1.Text = Me.FTTitleChart.Text
            'oChart.Titles.Add(chartTitle1)
            oChart.Dock = DockStyle.Fill


            Me.ogrpchartBar.Controls.Clear()
            Me.ogrpchartBar.Controls.Add(oChart)
        Catch ex As Exception

        End Try
    End Sub

    Private oChartPie As DevExpress.XtraCharts.ChartControl
    Private Sub LoadChartDoughnut(ByVal _oDt As DataTable)
        Try
            'oChart.ClearSelection()

            oChartPie = New DevExpress.XtraCharts.ChartControl

            Dim series1 As New Series("", ViewType.Pie3D) ' CType(Me.ocViewTypePie.SelectedIndex, ViewType)


            For Each R As DataRow In _oDt.Select("FTTypeofDiseaseName <> ''")
                series1.Points.Add(New SeriesPoint(R!FTTypeofDiseaseName.ToString, New Double() {CInt("0" & R!FNHSysEmpId.ToString)}))
            Next

            oChartPie.Series.Add(series1)

            series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            series1.Label.PointOptions.ValueNumericOptions.Precision = 2

            Try
                If TypeOf oChartPie.Diagram Is Diagram3D Then
                    Dim diagram As Diagram3D = CType(oChartPie.Diagram, Diagram3D)
                    diagram.RuntimeRotation = True
                    diagram.RuntimeZooming = True
                    diagram.RuntimeScrolling = True
                End If
            Catch ex As Exception
            End Try

            Dim chartTitle1 As New ChartTitle()

            'chartTitle1.Text = Me.FTTitleTopChart.Text
            'oChartPie.Titles.Add(chartTitle1)
            oChartPie.Dock = DockStyle.Fill

            Me.oGrpChartPie.Controls.Clear()
            Me.oGrpChartPie.Controls.Add(oChartPie)
        Catch ex As Exception
        End Try
    End Sub



    Private Sub LoadDataBar()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT 1 AS seq   ,  T.FTTypeofDiseaseCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", T.FTTypeofDiseaseNameTH AS FTTypeofDiseaseName"
            Else
                _Cmd &= vbCrLf & ", T.FTTypeofDiseaseNameEN AS FTTypeofDiseaseName"
            End If
            _Cmd &= vbCrLf & " , COUNT(H.FNHSysEmpId) AS FNHSysEmpId"

            _Cmd &= vbCrLf & "FROM [HITECH_MEDICAL]..TMECTGeneral AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [HITECH_MASTER]..TMECMTypeofDisease AS T WITH (NOLOCK) ON H.FNHSysTypeofDiseaseId = T.FNHSysTypeofDiseaseId"
            _Cmd &= vbCrLf & "WHERE LEFT(H.FDMECDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"

            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTMECTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTMECTime >='17:25:01'   "
            End Select


            _Cmd &= vbCrLf & "group by  T.FTTypeofDiseaseCode, T.FTTypeofDiseaseNameTH, T.FTTypeofDiseaseNameEN   "

            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "SELECT     2 AS Seq ,'' "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", L.FTNameTH AS FTTypeofDiseaseName"
            Else
                _Cmd &= vbCrLf & ", L.FTNameEN AS FTTypeofDiseaseName"
            End If
            _Cmd &= vbCrLf & ", COUNT(H.FNHSysEmpId) AS FNHSysEmpId"
            _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTAccident AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(SELECT      FNListIndex, FTNameTH, FTNameEN "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE     (FTListName = N'FNAccidentType')) AS L   ON H.FNAccidentType = L.FNListIndex"
            _Cmd &= vbCrLf & "WHERE     LEFT(H.FDDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"
            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTTime >='08:00:00' And H.FTTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTTime >='17:25:01'   "
            End Select
            _Cmd &= vbCrLf & "GROUP BY  L.FTNameTH, L.FTNameEN "

            _Cmd &= vbCrLf & "   UNION ALL"
            _Cmd &= vbCrLf & "SELECT     4 AS Seq ,'' "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", 'การให้คำปรึกษา' AS FTTypeofDiseaseName"
            Else
                _Cmd &= vbCrLf & ",'Consulting' AS FTTypeofDiseaseName"
            End If
            _Cmd &= vbCrLf & ", COUNT(H.FNHSysEmpId) AS FNHSysEmpId "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTConsul AS H WITH (NOLOCK)  "

            _Cmd &= vbCrLf & "WHERE    LEFT(H.FDDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"
            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTTime >='08:00:00' And H.FTTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTTime >='17:25:01'   "
            End Select
            _Cmd &= vbCrLf & "GROUP BY   MONTH(CONVERT(datetime, H.FDDate))"


            _Cmd &= vbCrLf & "   UNION ALL" ' Union All

            _Cmd &= vbCrLf & "SELECT  4 AS seq , D.FTOpinionCode "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",D.FTOpinionNameTH AS FTTypeofDiseaseName"
            Else
                _Cmd &= vbCrLf & ",D.FTOpinionNameEN AS FTTypeofDiseaseName"
            End If
            _Cmd &= vbCrLf & ",  COUNT(H.FNHSysEmpId) AS FNHSysEmpId"

            _Cmd &= vbCrLf & "FROM         HITECH_MEDICAL.dbo.TMECTGeneral AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                    HITECH_MASTER.dbo.TMECMOpinion AS D WITH (NOLOCK) ON H.FNHSysOpinionId = D.FNHSysOpinionId"
            _Cmd &= vbCrLf & "WHERE LEFT(H.FDMECDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"
            _Cmd &= vbCrLf & "AND D.FTOpinionNameEN Not Like '%Work%'"

            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTMECTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTMECTime >='17:25:01'   "
            End Select


            _Cmd &= vbCrLf & "group by  D.FTOpinionCode, D.FTOpinionNameTH, D.FTOpinionNameEN  "
            _Cmd &= vbCrLf & "Order by Seq ASC "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)



            _Cmd = "Select Top 3   FTTypeofDiseaseCode , FNHSysEmpId,  "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & "   FTTypeofDiseaseNameTH AS FTTypeofDiseaseName"
            Else
                _Cmd &= vbCrLf & "   FTTypeofDiseaseNameEN AS FTTypeofDiseaseName"
            End If
            _Cmd &= vbCrLf & "From (SELECT 1 AS seq   ,  T.FTTypeofDiseaseCode , T.FTTypeofDiseaseNameTH , T.FTTypeofDiseaseNameEN  "

            _Cmd &= vbCrLf & " , COUNT(H.FNHSysEmpId) AS FNHSysEmpId"

            _Cmd &= vbCrLf & "FROM [HITECH_MEDICAL]..TMECTGeneral AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [HITECH_MASTER]..TMECMTypeofDisease AS T WITH (NOLOCK) ON H.FNHSysTypeofDiseaseId = T.FNHSysTypeofDiseaseId"
            _Cmd &= vbCrLf & "WHERE LEFT(H.FDMECDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"

            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTMECTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTMECTime >='17:25:01'   "
            End Select


            _Cmd &= vbCrLf & "group by  T.FTTypeofDiseaseCode, T.FTTypeofDiseaseNameTH, T.FTTypeofDiseaseNameEN   "

            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "SELECT     2 AS Seq,'',  L.FTNameTH, L.FTNameEN, COUNT(H.FNHSysEmpId) AS FNHSysEmpId "
            _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTAccident AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(SELECT      FNListIndex, FTNameTH, FTNameEN "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE     (FTListName = N'FNAccidentType')) AS L   ON H.FNAccidentType = L.FNListIndex"
            _Cmd &= vbCrLf & "WHERE     LEFT(H.FDDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"
            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTTime >='08:00:00' And H.FTTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTTime >='17:25:01'   "
            End Select
            _Cmd &= vbCrLf & "GROUP BY  L.FTNameTH, L.FTNameEN "

            _Cmd &= vbCrLf & "   UNION ALL"
            _Cmd &= vbCrLf & "SELECT     4 AS Seq,'',   'การให้คำปรึกษา' as FTNameTH,   'Consulting' as FTNameEN, COUNT(H.FNHSysEmpId) AS FNHSysEmpId "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTConsul AS H WITH (NOLOCK)  "

            _Cmd &= vbCrLf & "WHERE    LEFT(H.FDDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"
            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTTime >='08:00:00' And H.FTTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTTime >='17:25:01'   "
            End Select

            _Cmd &= vbCrLf & "GROUP BY   MONTH(CONVERT(datetime, H.FDDate))"

            _Cmd &= vbCrLf & "   UNION ALL" ' Union All
            _Cmd &= vbCrLf & "SELECT  3 AS seq , D.FTOpinionCode,  D.FTOpinionNameTH, D.FTOpinionNameEN "
            _Cmd &= vbCrLf & ",  COUNT(H.FNHSysEmpId) AS FNHSysEmpId"

            _Cmd &= vbCrLf & "FROM         HITECH_MEDICAL.dbo.TMECTGeneral AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                    HITECH_MASTER.dbo.TMECMOpinion AS D WITH (NOLOCK) ON H.FNHSysOpinionId = D.FNHSysOpinionId"
            _Cmd &= vbCrLf & "WHERE LEFT(H.FDMECDate,7)  = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "',7)"
            _Cmd &= vbCrLf & "AND D.FTOpinionNameEN Not Like '%Work%'"
            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTMECTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTMECTime >='17:25:01'   "
            End Select


            _Cmd &= vbCrLf & "group by  D.FTOpinionCode, D.FTOpinionNameTH, D.FTOpinionNameEN  ) AS Z "
            _Cmd &= vbCrLf & "Order by  FNHSysEmpId DESC "

            Dim _oDtTop As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)


            Call LoadChart(_oDt)
            Call LoadChartDoughnut(_oDt)
            Me.ogvTop.DataSource = _oDtTop
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If Me.FTYear.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTYear_lbl.Text)
                Me.FTYear.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub wPaitentStaticChart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogcTop.OptionsView.ShowAutoFilterRow = False
        Catch ex As Exception

        End Try
    End Sub

    Private Function TakeScreenShot(ByVal Control As Control) As Bitmap
        Dim Screenshot As New Bitmap(Control.Width, Control.Height)
        Control.DrawToBitmap(Screenshot, New Rectangle(0, 0, Control.Width, Control.Height))
        Return Screenshot
    End Function


    Private Sub PreviewReport()
        Try
            ' Dim Img As Image = Me.FTImage.Image
            'HI.UL.ULImage.
            Dim _Cmd As String = ""

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "].dbo.Tmp_MedRepot WHERE FTInsUser='" & HI.ST.UserInfo.UserName & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MEDC)
            HI.Conn.SQLConn.SqlConnectionOpen()

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MEDC) & "].dbo.Tmp_MedRepot (FTInsUser,   FTImage )"
            _Cmd &= vbCrLf & "Select @FTUserLogIn"
            _Cmd &= vbCrLf & ",@FTImage"


            Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
            cmd.Parameters.Add("@FTUserLogIn", SqlDbType.NVarChar, 100)
            cmd.Parameters.Add("@FTImage", SqlDbType.Image)
            cmd.Parameters("@FTUserLogIn").Value = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
      
            Dim data As Byte() = Nothing
            data = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(GroupControl2), UL.ULImage.PicType.Employee)
            cmd.Parameters("@FTImage").Value = data

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            Call PreviewReport()
            With New HI.RP.Report
                .Formular = "{Tmp_MedRepot.FTInsUser} = '" & HI.ST.UserInfo.UserName & "'"
                .FormTitle = Me.Text
                .AddParameter("FDDateTrans", Me.FTYear.Text)
                .ReportFolderName = "Human Report\"
                .AddParameter("FDAsOfDate", HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text))
                .ReportName = "ReportMEDStaticChartPerYear.rpt"
                .Preview()
            End With
        Catch ex As Exception
        End Try
    End Sub
End Class
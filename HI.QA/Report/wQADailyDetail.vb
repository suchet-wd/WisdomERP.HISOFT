Imports DevExpress.XtraCharts
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient

Public Class wQADailyDetail

    Private _dt1 As DataTable
    Private _dt2 As DataTable
#Region "Property"
    Private _UnitSectId As Integer
    Public Property SysUnitSectId As Integer
        Get
            Return _UnitSectId
        End Get
        Set(value As Integer)
            _UnitSectId = value
        End Set
    End Property

    Private _SDate As String
    Public Property SDate As String
        Get
            Return _SDate
        End Get
        Set(value As String)
            _SDate = value
        End Set
    End Property

    Private _EDate As String
    Public Property EDate As String
        Get
            Return _EDate
        End Get
        Set(value As String)
            _EDate = value
        End Set
    End Property
#End Region

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitGrid()
    End Sub


    Private Sub InitGrid()
        Try
            Dim _FSumMain As String = "FNQAInQty|FNQAAqlQty|FNQAActualQty|FNTotalDefect|FNAndon"
            With ogvDetail
                .ClearGrouping()
                .ClearDocument()
                For Each Str As String In _FSumMain.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
            End With
            _FSumMain = "Qty"
            With ogvTopDefect
                .ClearGrouping()
                .ClearDocument()
                .OptionsView.ShowFooter = True
                For Each Str As String In _FSumMain.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataDetailDefect(_FNHSysUnitSectId As Integer, SDate As String, EDate As String)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
            _Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
            _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty " ',(SUM(A.FNMajorQty)+SUM(A.FNMinorQty)) AS FNTotalDefect
            _Cmd &= vbCrLf & "		, B.FTStyleCode, O.FTPORef , sum(Isnull(A.FNAndon,0)) AS FNAndon"
            '_Cmd &= vbCrLf & "  ,((SUM(A.FNMajorQty)+SUM(A.FNMinorQty))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"

            _Cmd &= vbCrLf & " ,isnull( (Select count(*) as t  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "  WHERE        (FNHSysUnitSectId =A.FNHSysUnitSectId)   AND (FDQADate = A.FDQADate)  AND (FNHSysStyleId = A.FNHSysStyleId) AND (FTOrderNo =A.FTOrderNo) and FNHourNo = REPLACE(A.FNHourNo,':','') "
            _Cmd &= vbCrLf & "  and FTStateReject = '1') , 0 ) AS FNTotalDefect "

            _Cmd &= vbCrLf & " , ( (isnull( (Select count(*) as t  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "  WHERE        (FNHSysUnitSectId =A.FNHSysUnitSectId)   AND (FDQADate = A.FDQADate)  AND (FNHSysStyleId = A.FNHSysStyleId) AND (FTOrderNo =A.FTOrderNo) and FNHourNo = REPLACE(A.FNHourNo,':','') "
            _Cmd &= vbCrLf & "  and FTStateReject = '1') , 0 )  )* 100 ) /   SUM( A.FNQAActualQty)    AS FNDefectPer  "



            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
            _Cmd &= vbCrLf & " WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(SDate) & "')"
            _Cmd &= vbCrLf & " AND A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(EDate) & "'"
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)
            _Cmd &= vbCrLf & "group by   A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   B.FTStyleCode, O.FTPORef , A.FNHourNo"
            _Cmd &= vbCrLf & "Order by A.FDQADate"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDetail.DataSource = _oDt
            Me.LoadChart(_oDt)
            _dt1 = _oDt

            Dim _TotalQAActual As Integer
            _Cmd = "SELECT   sum(A.FNQAActualQty) AS FNQAActualQty  "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH (NOLOCK) ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  ON A.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & " WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(SDate) & "')"
            _Cmd &= vbCrLf & " AND A.FDQADate <='" & HI.UL.ULDate.ConvertEnDB(EDate) & "'"
            _Cmd &= vbCrLf & " AND A.FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)

            _TotalQAActual = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")


            _Cmd = "SELECT   Top 5   sum(Qty) AS Qty,   FNHSysQADetailId, FTQADetailCode  ,(sum(Qty) * 100) /" & _TotalQAActual & " AS DefectPer"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",  FTQADetailNameTH AS FTQADetailName"
            Else
                _Cmd &= vbCrLf & ", FTQADetailNameEN AS FTQADetailName "
            End If

            _Cmd &= vbCrLf & "FROM         (SELECT     COUNT(*) AS Qty, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN"
            _Cmd &= vbCrLf & "                 FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                                      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B WITH (NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  ON A.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH (NOLOCK) ON A.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "WHERE Isnull(U.FTUnitSectCode,'') <> ''"
            _Cmd &= vbCrLf & " AND A.FNHSysUnitSectId=" & Val("0" & _FNHSysUnitSectId)
            _Cmd &= vbCrLf & "                GROUP BY A.FDQADate, A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, B.FTQADetailNameEN) AS T"
            _Cmd &= vbCrLf & " WHERE     (FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(SDate) & "')"
            _Cmd &= vbCrLf & " AND FDQADate <='" & HI.UL.ULDate.ConvertEnDB(EDate) & "'"

            _Cmd &= vbCrLf & " Group by FNHSysQADetailId, FTQADetailCode, FTQADetailNameTH, FTQADetailNameEN,FTQADetailNameTH"
            _Cmd &= vbCrLf & "Order by sum(Qty) desc"
            Dim _oDt2 As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcTopDefect.DataSource = _oDt2
            Me.LoadChartDoughnut(_oDt2)
            _dt2 = _oDt2

        Catch ex As Exception
        End Try
    End Sub

    Private Sub close_Click(sender As Object, e As EventArgs) Handles oclose.Click
        Me.Close()
    End Sub

    Private Sub wQADailyDetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.LoadDataDetailDefect(_UnitSectId, _SDate, _EDate)
        Catch ex As Exception
        End Try
    End Sub

    Private oChart As DevExpress.XtraCharts.ChartControl

    Private Sub LoadChart(_oDt As DataTable)
        Try
            oChart = New DevExpress.XtraCharts.ChartControl

            ' Create the first side-by-side bar series and add points to it.
            Dim _Date As Date = Date.Now.ToString("d")
            Dim series1 As New Series(Me.FTSeriesDetailName.Text & " " & _SDate & " - " & _EDate & "", ViewType.Bar)

            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FDQADate.ToString, New Double() {CInt("0" & R!FNDefectPer.ToString)}))
            Next

            ' Add the series to the chart.
            oChart.Series.Add(series1)

            ' Add a title to the chart (if necessary).
            Dim chartTitle1 As New ChartTitle()


            chartTitle1.Text = FTTitleDetailChart.Text
            oChart.Titles.Add(chartTitle1)
            oChart.Dock = DockStyle.Fill

            'oChart.SeriesTemplate.LabelsVisibility = If(True, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            'oChart.CrosshairEnabled = If(True, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
            'CType(oChart.SeriesTemplate.Label, BarSeriesLabel).Position = BarSeriesLabelPosition.Top


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
            Dim series1 As New Series(Me.FTSeriesTopName.Text, ViewType.Doughnut3D)
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

            Me.ogrpTopDefectChart.Controls.Clear()
            Me.ogrpTopDefectChart.Controls.Add(oChartPie)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            Preview()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Preview()
        Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")
        Try
            Me.GetData(_dt1, _dt2)
            With New HI.RP.Report

                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                '.Formular = _Formular
                .AddParameter("FDSDate", SDate)
                .AddParameter("FDEDate", EDate)
                .ReportName = "QADailyChartDetailReport.rpt"
                _spls.Close()
                .Preview()
            End With
        Catch ex As Exception
            _spls.Close()
        End Try
    End Sub

    Private Sub GetData(_oDt As DataTable, _oDt2 As DataTable)
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Defect Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Defect "
                _Cmd &= "( FTUserLogIn, FNHSysUnitSectId, FNHSysStyleId, FTOrderNo, FDQADate, FTPORef, FNQAInQty, FNQAAqlQty, FNQAActualQty, FNTotalDefect, FNAndon, FNDefectPer)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & R!FNHSysUnitSectId)
                _Cmd &= vbCrLf & "," & CInt("0" & R!FNHSysStyleId)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDQADate.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & R!FNQAInQty)
                _Cmd &= vbCrLf & "," & CInt("0" & R!FNQAAqlQty)
                _Cmd &= vbCrLf & "," & CInt("0" & R!FNQAActualQty)
                _Cmd &= vbCrLf & "," & CInt("0" & R!FNTotalDefect)
                _Cmd &= vbCrLf & "," & CInt("0" & R!FNAndon)
                _Cmd &= vbCrLf & "," & CDbl("0" & R!FNDefectPer)
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Next

            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_DefectTop Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each r As DataRow In _oDt2.Rows
                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_DefectTop "
                _Cmd &= "(FTUserLogIn, FNHSysQADetailId, FTQADetailCode, FTQADetailName, DefectPer,FNQty)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & r!FNHSysQADetailId)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(r!FTQADetailCode.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(r!FTQADetailName.ToString) & "'"
                _Cmd &= vbCrLf & "," & CDbl("0" & r!DefectPer)
                _Cmd &= vbCrLf & "," & CDbl("0" & r!Qty)
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Next



            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Img Where  FTUserLogIn='" & HI.ST.UserInfo.UserName & "' and FTStateApp='1'"
            '_Cmd &= vbCrLf & "INSERT INTO TmpTPRODTQA_Report_Img (FTUserLogIn,FTStateApp)"
            '_Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
            '_Cmd &= vbCrLf & ",'1'"
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
            cmd.Parameters("@FTStateApp").Value = "1"

            Dim data As Byte() = Nothing

            data = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(oChart), UL.ULImage.PicType.Employee)
            cmd.Parameters("@Image").Value = data

            Dim data2 As Byte() = Nothing

            data2 = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(oChartPie), UL.ULImage.PicType.Employee)
            cmd.Parameters("@ImageSub").Value = data2


            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            'HI.Conn.SQLConn.SqlConnectionOpen()

            '_Cmd = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Img "
            '_Cmd &= vbCrLf & "Set FTImage = @Image"
            '_Cmd &= vbCrLf & ",FTImageSub = @ImageSub"
            '_Cmd &= vbCrLf & "WHERE FTUserLogIn=@FTUserLogIn"

            'Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
            'cmd.Parameters.AddWithValue("@FTUserLogIn", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))


            'Dim data As Byte() = Nothing

            'For Each Obj As Object In Me.Controls.Find("ogrpChart", True)
            '    Select Case HI.ENM.Control.GeTypeControl(Obj)
            '        Case ENM.Control.ControlType.GroupControl
            '            data = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(oChart), UL.ULImage.PicType.Employee)
            '    End Select
            'Next

            'Dim p As New SqlParameter("@Image", SqlDbType.Image)
            'p.Value = data

            'cmd.Parameters.Add(p)

            'Dim data2 As Byte() = Nothing

            'For Each Obj As Object In Me.Controls.Find("ogrpTopDefectChart", True)
            '    Select Case HI.ENM.Control.GeTypeControl(Obj)
            '        Case ENM.Control.ControlType.GroupControl
            '            data2 = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(oChartPie), UL.ULImage.PicType.Employee)
            '    End Select
            'Next

            'Dim p2 As New SqlParameter("@ImageSub", SqlDbType.Image)
            'p2.Value = data2

            'cmd.Parameters.Add(p2)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

        Catch ex As Exception

        End Try
    End Sub

    Private Function TakeScreenShot(ByVal Control As Control) As Bitmap
        Dim Screenshot As New Bitmap(Control.Width, Control.Height)
        Control.DrawToBitmap(Screenshot, New Rectangle(0, 0, Control.Width, Control.Height))
        Return Screenshot
    End Function

    Private Sub ogvDetail_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvDetail.RowCellClick
        Try
            If e.Column.Name.ToString = "cFNTotalDefect" Then
                Dim _UnitSectId As Integer = CInt("0" & ogvDetail.GetRowCellValue(e.RowHandle, "FNHSysUnitSectId").ToString)
                Dim _Date As String = ogvDetail.GetRowCellValue(e.RowHandle, "FDQADate").ToString
                Dim _StyleId As Integer = CInt("0" & ogvDetail.GetRowCellValue(e.RowHandle, "FNHSysStyleId").ToString)
                Dim _OrderNo As String = ogvDetail.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString

                With New wPopupDefect
                    .UnitSectId = _UnitSectId
                    .StyleId = _StyleId
                    .OrderNo = _OrderNo
                    .TDate = _Date
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
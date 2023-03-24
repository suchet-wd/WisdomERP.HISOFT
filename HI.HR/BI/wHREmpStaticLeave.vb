Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils

Public Class wHREmpStaticLeave


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        PivotGridControl1.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl1.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        PivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl2.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        PivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        PivotGridControl3.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl3.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        PivotGridControl3.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl3.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked


        chartControl.CrosshairOptions.ShowArgumentLine = False
        ChartControl1.CrosshairOptions.ShowArgumentLine = False
        ChartControl2.CrosshairOptions.ShowArgumentLine = False
        ChartControl3.CrosshairOptions.ShowArgumentLine = False


        'Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        'For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
        '    If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
        '        Continue For
        '    End If
        '    comboChartType.Properties.Items.Add(type)
        'Next type
        'comboChartType.SelectedItem = ViewType.Bar
        chartControl.DataSource = pivotGridControl
        ChartControl1.DataSource = PivotGridControl1
        ChartControl2.DataSource = PivotGridControl2
        ChartControl3.DataSource = PivotGridControl3

    End Sub

#Region "Chart"
    '<comboChartType>
    'Private Sub comboBoxEdit2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboChartType.SelectedIndexChanged
    '    chartControl.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))
    '    If chartControl.SeriesTemplate.Label IsNot Nothing Then
    '        chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
    '        chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
    '        checkShowPointLabels.Enabled = True
    '    Else
    '        checkShowPointLabels.Enabled = False
    '    End If
    '    If (TryCast(chartControl.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
    '        chartControl.Legend.Visible = True

    '    End If
    '    If TypeOf chartControl.Diagram Is Diagram3D Then
    '        Dim diagram As Diagram3D = CType(chartControl.Diagram, Diagram3D)
    '        diagram.RuntimeRotation = True
    '        diagram.RuntimeZooming = True
    '        diagram.RuntimeScrolling = True
    '    End If
    '    For Each series As Series In chartControl.Series
    '        Dim supportTransparency As ISupportTransparency = TryCast(series.View, ISupportTransparency)
    '        If supportTransparency IsNot Nothing Then
    '            If (TypeOf series.View Is AreaSeriesView) OrElse (TypeOf series.View Is Area3DSeriesView) OrElse (TypeOf series.View Is RadarAreaSeriesView) OrElse (TypeOf series.View Is Bar3DSeriesView) Then
    '                supportTransparency.Transparency = 135
    '            Else
    '                supportTransparency.Transparency = 0
    '            End If
    '        End If
    '    Next series
    'End Sub
    ''</comboChartType>

    '<checkShowPointLabels>
    Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabels.CheckedChanged
        chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        ChartControl1.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl1.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        ChartControl2.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl2.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        ChartControl3.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl3.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
    End Sub
    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVertical_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVertical.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl3.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnly.CheckedChanged
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled

        PivotGridControl1.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled

        PivotGridControl2.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled

        PivotGridControl3.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Enabled
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl3.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        PivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        PivotGridControl3.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelay_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelay.EditValueChanged
        pivotGridControl.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        PivotGridControl1.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        PivotGridControl2.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        PivotGridControl3.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
    End Sub
    '</seUpdateDelay>


#End Region



#Region "Procedure"
    Private Sub LoadDataLeave(_Spls As HI.TL.SplashScreen)
        Dim _Qry As String = ""
        Dim dt As New DataTable

        _Spls.UpdateInformation("Loading DataLeave Please Wait... ")
        Try
            '_Qry = " SELECT   FTCourseNameTH,COUNT(E.FNHSysEmpID)as Total, Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex"
            '_Qry &= vbCrLf & " FROM THRMEmployee AS E INNER JOIN"
            '_Qry &= vbCrLf & " THRTEmployeeEducation AS B ON E.FNHSysEmpID = B.FNHSysEmpID LEFT JOIN"
            '_Qry &= vbCrLf & " HITECH_MASTER.dbo.THRMCourse AS C ON B.FNHSysCourseId = C.FNHSysCourseId"

            _Qry = "SELECT * From ("
            _Qry &= vbCrLf & "SELECT    U.FNHSysUnitSectId, L.FTDateTrans,U.FTUnitSectNameTH, L.FTLeaveType, 1 AS Total, Case When E.FNEmpSex = 0 Then 'M' else 'F'end as FNEmpSex"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", FTUnitSectNameTH as FTUnitSectName ,FTNameTH as FTName ,FTNationalityNameTH AS FTNationality ,FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", FTUnitSectNameEN as FTUnitSectName ,FTNameEN as FTName ,FTNationalityNameEN AS FTNationality ,FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & " FROM THRTTransLeave AS L with (Nolock) INNER JOIN THRMEmployee AS E with (Nolock) ON L.FNHSysEmpID = E.FNHSysEmpID "
            _Qry &= vbCrLf & "              LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS I with (Nolock) ON E.FNHSysDivisonId = I.FNHSysDivisonId "
            _Qry &= vbCrLf & "                LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Qry &= vbCrLf & "                  LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Qry &= vbCrLf & "                    LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "                      LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "                        LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & "				            LEFT OUTER JOIN (SELECT FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "                            FROM HITECH_SYSTEM.dbo.HSysListData"
            _Qry &= vbCrLf & "						        WHERE (FTListName = N'FNLeaveType')) AS D ON L.FTLeaveType = D.FNListIndex"
            _Qry &= vbCrLf & " Where E.FNHSysEmpTypeId >0 And E.FNEmpStatus <> 2"

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTEmpTypeCode = '" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'"
            End If
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDeptCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDeptCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) & "'"
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDivisonCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) & "'"
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDivisonCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysSectId.Text) & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTUnitSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTUnitSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
            End If

            _Qry &= vbCrLf & " UNION "

            _Qry &= vbCrLf & "SELECT  U.FNHSysUnitSectId, L.FTDateTrans,U.FTUnitSectNameTH, '777' FTLeaveType, 1 AS Total, Case When E.FNEmpSex = 0 Then 'M' else 'F'end as FNEmpSex"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", FTUnitSectNameTH as FTUnitSectName ,'ขาดงาน' as FTName ,FTNationalityNameTH AS FTNationality ,FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", FTUnitSectNameEN as FTUnitSectName ,'Absent' as FTName ,FTNationalityNameEN AS FTNationality ,FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & " FROM THRTTrans AS L INNER JOIN THRMEmployee AS E with (Nolock) ON L.FNHSysEmpID = E.FNHSysEmpID "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS I with (Nolock) ON E.FNHSysDivisonId = I.FNHSysDivisonId "
            _Qry &= vbCrLf & "    LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Qry &= vbCrLf & "     LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "       LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "         LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " Where E.FNHSysEmpTypeId > 0 And E.FNEmpStatus <> 2 And L.FNAbsent >= 480 "
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTEmpTypeCode = '" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'"
            End If
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDeptCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDeptCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) & "'"
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDivisonCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) & "'"
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTDivisonCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysSectId.Text) & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTUnitSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND FTUnitSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
            End If
            _Qry &= vbCrLf & ") AS D"
            _Qry &= vbCrLf & "Where FNHSysUnitSectId <> 0"


            If Me.FTDateStart.Text <> "" Then
                _Qry &= vbCrLf & "AND FTDateTrans >='" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "' "
            End If
            If Me.FTDateEnd.Text <> "" Then
                _Qry &= vbCrLf & "AND FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(FTDateEnd.Text) & "' "
            End If
            '_Qry &= vbCrLf & " GROUP BY  FTCourseNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"
            '_Qry &= vbCrLf & " GROUP BY  U.FTUnitSectNameTH, D.FTNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.pivotGridControl.DataSource = dt.Copy


        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub
#End Region

#Region "Procedure1"
    Private Sub LoadDataLeavePer(_Spls As HI.TL.SplashScreen)
        Dim _Per As String = ""
        Dim dt As New DataTable

        _Spls.UpdateInformation("Loading DataLeave Please Wait... ")

        Try
            '_Qry = "  DECLARE @edu numeric(18, 0)"
            '_Qry &= vbCrLf & " SET @edu = (SELECT COUNT(FNHSysEmpID) FROM THRMEmployee)"
            '_Qry &= vbCrLf & " SELECT   CONVERT(numeric (18, 2),COUNT(E.FNHSysEmpID)*100.00 / @edu) as Total,FTCourseNameTH , Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex"
            '_Qry &= vbCrLf & " FROM     THRMEmployee AS E INNER JOIN"
            '_Qry &= vbCrLf & " THRTEmployeeEducation AS B ON E.FNHSysEmpID = B.FNHSysEmpID LEFT JOIN"
            '_Qry &= vbCrLf & " HITECH_MASTER.dbo.THRMCourse AS C ON B.FNHSysCourseId = C.FNHSysCourseId"
            _Per = " DECLARE @lev AS numeric(18, 0)"
            _Per &= vbCrLf & " Set @lev = (SELECT COUNT(FTLeaveType) FROM THRTTransLeave)"
            _Per &= vbCrLf & "SELECT * From ("
            _Per &= vbCrLf & " SELECT U.FNHSysUnitSectId, L.FTDateTrans, U.FTUnitSectNameTH, L.FTLeaveType, CASE WHEN @lev >0 THEN 1.00/@lev*100 ELSE 0.00 END as Total, CASE WHEN E.FNEmpSex = 0 THEN 'M' ELSE 'F' END AS FNEmpSex"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Per &= vbCrLf & ", FTUnitSectNameTH as FTUnitSectName ,FTNameTH as FTName ,FTNationalityNameTH AS FTNationality ,FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Per &= vbCrLf & ", FTUnitSectNameEN as FTUnitSectName ,FTNameEN as FTName ,FTNationalityNameEN AS FTNationality ,FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Per &= vbCrLf & " FROM     THRTTransLeave AS L with (Nolock) INNER JOIN THRMEmployee AS E with (Nolock) ON L.FNHSysEmpID = E.FNHSysEmpID "
            _Per &= vbCrLf & "                                LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS I with (Nolock) ON E.FNHSysDivisonId = I.FNHSysDivisonId "
            _Per &= vbCrLf & "                                  LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Per &= vbCrLf & "                                    LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Per &= vbCrLf & "                                      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Per &= vbCrLf & "                                        LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Per &= vbCrLf & "                                          LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Per &= vbCrLf & "			                                  LEFT OUTER JOIN (SELECT FNListIndex, FTNameTH, FTNameEN"
            _Per &= vbCrLf & "                                              FROM HITECH_SYSTEM.dbo.HSysListData"
            _Per &= vbCrLf & "									              WHERE (FTListName = N'FNLeaveType')) AS D ON L.FTLeaveType = D.FNListIndex"
            _Per &= vbCrLf & "Where E.FNHSysEmpTypeId >0 AND E.FNEmpStatus <> 2"
            If Me.FNHSysEmpTypeId.Text <> "" Then

                _Per &= vbCrLf & " AND FTEmpTypeCode = '" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'"
            End If
            If Me.FNHSysDeptId.Text <> "" Then
                _Per &= vbCrLf & " AND FTDeptCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTDeptCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) & "'"
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _Per &= vbCrLf & " AND FTDivisonCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) & "'"
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTDivisonCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Per &= vbCrLf & " AND FTSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysSectId.Text) & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Per &= vbCrLf & " AND FTUnitSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTUnitSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
            End If
            _Per &= vbCrLf & "UNION"
            _Per &= vbCrLf & " SELECT U.FNHSysUnitSectId, L.FTDateTrans, U.FTUnitSectNameTH, '777' FTLeaveType, CASE WHEN @lev >0 THEN 1.00/@lev*100 ELSE 0.00 END as Total, CASE WHEN E.FNEmpSex = 0 THEN 'M' ELSE 'F' END AS FNEmpSex"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Per &= vbCrLf & ", FTUnitSectNameTH as FTUnitSectName ,'ขาดงาน' as FTName ,FTNationalityNameTH AS FTNationality ,FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Per &= vbCrLf & ", FTUnitSectNameEN as FTUnitSectName ,'Absent' as FTName ,FTNationalityNameEN AS FTNationality ,FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Per &= vbCrLf & " FROM THRTTrans AS L INNER JOIN THRMEmployee AS E with (Nolock) ON L.FNHSysEmpID = E.FNHSysEmpID  "
            _Per &= vbCrLf & "                        LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS I with (Nolock) ON E.FNHSysDivisonId = I.FNHSysDivisonId "
            _Per &= vbCrLf & "                          LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Per &= vbCrLf & "                            LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Per &= vbCrLf & "                              LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Per &= vbCrLf & "                                LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Per &= vbCrLf & "                                  LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Per &= vbCrLf & "Where E.FNHSysEmpTypeId >0 AND E.FNEmpStatus <> 2 AND L.FNAbsent >= 480 "
            If Me.FNHSysEmpTypeId.Text <> "" Then

                _Per &= vbCrLf & " AND FTEmpTypeCode = '" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'"
            End If
            If Me.FNHSysDeptId.Text <> "" Then
                _Per &= vbCrLf & " AND FTDeptCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTDeptCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) & "'"
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _Per &= vbCrLf & " AND FTDivisonCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) & "'"
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTDivisonCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Per &= vbCrLf & " AND FTSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysSectId.Text) & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Per &= vbCrLf & " AND FTUnitSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND FTUnitSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
            End If
            _Per &= vbCrLf & ") AS D"
            _Per &= vbCrLf & "Where FNHSysUnitSectId <> 0"


            If Me.FTDateStart.Text <> "" Then
                _Per &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "'"
            End If
            If Me.FTDateEnd.Text <> "" Then
                _Per &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(FTDateEnd.Text) & "'"
            End If
            '_Qry &= vbCrLf & " GROUP BY  FTCourseNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"
            '_Per &= vbCrLf & " GROUP BY  D.FTNameTH,L.FTDateTrans,U.FTUnitSectNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"


            dt = HI.Conn.SQLConn.GetDataTable(_Per, Conn.DB.DataBaseName.DB_HR)

            Me.PivotGridControl1.DataSource = dt.Copy


        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub
#End Region

#Region "Procedure2"
    Private Sub LoadDataResign(_Spls As HI.TL.SplashScreen)
        Dim _Qry As String = ""
        Dim dt As New DataTable

        _Spls.UpdateInformation("Loading DataResign Please Wait... ")

        Try

            '_Qry = " SELECT  FTEmpTypeNameTH,Count(R.FNHSysEmpTypeId)as Total, Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex "
            '_Qry &= vbCrLf & " FROM THRMEmployee_Resign_Move AS R LEFT OUTER JOIN THRMEmployee AS E ON R.FNHSysEmpID = E.FNHSysEmpID "
            '_Qry &= vbCrLf & "	    LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS S ON E.FNHSysUnitSectId = S.FNHSysUnitSectId"
            '_Qry &= vbCrLf & "	        LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T ON R.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            '_Qry &= vbCrLf & "Where R.FNHSysEmpTypeId >0"
            _Qry = " SELECT  FDDateEnd, E.FNHSysEmpTypeId,1 as Total, Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", FTUnitSectNameTH as FTUnitSectName ,FTNationalityNameTH AS FTNationality, FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", FTUnitSectNameEN as FTUnitSectName ,FTNationalityNameEN AS FTNationality, FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName "
            End If
            _Qry &= vbCrLf & " FROM  THRMEmployee AS E with (Nolock) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "				            LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS D with (Nolock) ON E.FNHSysDivisonId = D.FNHSysDivisonId "
            _Qry &= vbCrLf & "					          LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Qry &= vbCrLf & "						        LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Qry &= vbCrLf & "							      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "                                  LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " Where E.FNEmpStatus = 2"

            If Me.FNHSysEmpTypeId.Text <> "" Then

                _Qry &= vbCrLf & " AND T.FTEmpTypeCode = '" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'"
            End If
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTDeptCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTDeptCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) & "'"
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND D.FTDivisonCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) & "'"
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND D.FTDivisonCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND S.FTSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysSectId.Text) & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND S.FTSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND U.FTUnitSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND U.FTUnitSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FTDateStart.Text <> "" Then
                _Qry &= vbCrLf & " AND FDDateEnd >= '" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "'"
            End If
            If Me.FTDateEnd.Text <> "" Then
                _Qry &= vbCrLf & " AND FDDateEnd <= '" & HI.UL.ULDate.ConvertEnDB(FTDateEnd.Text) & "'"
            End If

            '_Qry &= vbCrLf & " GROUP BY FTEmpTypeNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"
            '_Qry &= vbCrLf & " GROUP BY U.FTUnitSectNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.PivotGridControl2.DataSource = dt.Copy


        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub
#End Region

#Region "Procedure3"
    Private Sub LoadDataResignPer(_Spls As HI.TL.SplashScreen)
        Dim _Per As String = ""
        Dim dt As New DataTable

        _Spls.UpdateInformation("Loading DataResign Please Wait... ")

        Try
            '_Qry = "  DECLARE @edu numeric(18, 0)"
            '_Qry &= vbCrLf & " SET @edu = (SELECT COUNT(FNHSysEmpID) FROM THRMEmployee)"
            '_Qry &= vbCrLf & " SELECT   CONVERT(numeric (18, 2),COUNT(E.FNHSysEmpID)*100.00 / @edu) as Total,FTCourseNameTH , Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex"
            '_Qry &= vbCrLf & " FROM     THRMEmployee AS E INNER JOIN"
            '_Qry &= vbCrLf & " THRTEmployeeEducation AS B ON E.FNHSysEmpID = B.FNHSysEmpID LEFT JOIN"
            '_Qry &= vbCrLf & " HITECH_MASTER.dbo.THRMCourse AS C ON B.FNHSysCourseId = C.FNHSysCourseId"
            _Per = " DECLARE @resign numeric(18, 0)"
            _Per &= vbCrLf & " SET @resign = (SELECT COUNT(FNHSysEmpTypeId) FROM THRMEmployee)"
            _Per &= vbCrLf & " SELECT  FDDateEnd, U.FTUnitSectNameTH, CASE WHEN @resign >0 THEN 1.00/@resign*100 ELSE 0.00 END as Total,Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Per &= vbCrLf & ", FTUnitSectNameTH as FTUnitSectName ,FTNationalityNameTH AS FTNationality ,FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Per &= vbCrLf & ", FTUnitSectNameEN as FTUnitSectName ,FTNationalityNameEN AS FTNationality ,FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Per &= vbCrLf & " FROM         THRMEmployee AS E with (Nolock) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId "
            _Per &= vbCrLf & "					                LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS D with (Nolock) ON E.FNHSysDivisonId = D.FNHSysDivisonId"
            _Per &= vbCrLf & "						              LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId"
            _Per &= vbCrLf & "                                      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId"
            _Per &= vbCrLf & "                                        LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Per &= vbCrLf & "                                          LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Per &= vbCrLf & "Where E.FNEmpStatus = 2"

            If Me.FNHSysEmpTypeId.Text <> "" Then

                _Per &= vbCrLf & " AND T.FTEmpTypeCode = '" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'"
            End If
            If Me.FNHSysDeptId.Text <> "" Then
                _Per &= vbCrLf & " AND M.FTDeptCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND M.FTDeptCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) & "'"
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _Per &= vbCrLf & " AND D.FTDivisonCode >= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) & "'"
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND D.FTDivisonCode <= '" & HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) & "'"
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _Per &= vbCrLf & " AND S.FTSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysSectId.Text) & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND S.FTSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) & "'"
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Per &= vbCrLf & " AND U.FTUnitSectCode >= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Per &= vbCrLf & " AND U.FTUnitSectCode <= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
            End If
            If Me.FTDateStart.Text <> "" Then
                _Per &= vbCrLf & " AND FDDateEnd >= '" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "'"
            End If
            If Me.FTDateEnd.Text <> "" Then
                _Per &= vbCrLf & " AND FDDateEnd <= '" & HI.UL.ULDate.ConvertEnDB(FTDateEnd.Text) & "'"
            End If
            '_Qry &= vbCrLf & " GROUP BY  FTCourseNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"
            '_Per &= vbCrLf & " GROUP BY U.FTUnitSectNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"

            dt = HI.Conn.SQLConn.GetDataTable(_Per, Conn.DB.DataBaseName.DB_HR)

            Me.PivotGridControl3.DataSource = dt.Copy

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
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        'If Me.FNHSysEmpTypeId.Text <> "" Then
        Call LoadDataLeave(_Spls)
        Call LoadDataLeavePer(_Spls)
        Call LoadDataResign(_Spls)
        Call LoadDataResignPer(_Spls)
        _Spls.Close()
        'Else
        'HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysEmpTypeId_lbl.Text)
        'Me.FNHSysEmpTypeId.Focus()
        'Exit Sub
        'End If
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class
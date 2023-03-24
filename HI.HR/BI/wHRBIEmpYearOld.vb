Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils

Public Class wHRBIEmpYearOld

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        '------------------------------------------------------------------------------------------'
        pivotGridControl1.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl1.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        '------------------------------------------------------------------------------------------'
        pivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl2.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        '------------------------------------------------------------------------------------------'
        pivotGridControl3.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl3.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl3.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl3.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        '------------------------------------------------------------------------------------------'

        chartControl.CrosshairOptions.ShowArgumentLine = False
        chartControl1.CrosshairOptions.ShowArgumentLine = False
        ChartControl2.CrosshairOptions.ShowArgumentLine = False
        ChartControl2.CrosshairOptions.ShowArgumentLine = False

        'Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        'For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
        '    If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
        '        Continue For
        '    End If
        '    comboChartType.Properties.Items.Add(type)
        'Next type
        'comboChartType.SelectedItem = ViewType.Bar
        chartControl.DataSource = pivotGridControl
        ChartControl1.DataSource = pivotGridControl1
        ChartControl2.DataSource = pivotGridControl2
        ChartControl3.DataSource = pivotGridControl3


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
    '</comboChartType>

    '<checkShowPointLabels>
    Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabels.CheckedChanged
        chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
        '**********************************************************************************'
        ChartControl1.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl1.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
        '**********************************************************************************'
        ChartControl2.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl2.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
        '**********************************************************************************'
        ChartControl2.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl2.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
        '**********************************************************************************'
        '**********************************************************************************'
    End Sub
    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVertical_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVertical.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        '**********************************************************************************'
        pivotGridControl1.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        '**********************************************************************************'
        pivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        '**********************************************************************************'
        pivotGridControl3.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        '**********************************************************************************'
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnly.CheckedChanged
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
        '**********************************************************************************'
        pivotGridControl1.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
        '**********************************************************************************'
        pivotGridControl2.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
        '**********************************************************************************'
        pivotGridControl3.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
        '**********************************************************************************'
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        '**********************************************************************************'
        pivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        '**********************************************************************************'
        pivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        '**********************************************************************************'
        pivotGridControl3.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        '**********************************************************************************'
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        '**********************************************************************************'
        pivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        '**********************************************************************************'
        pivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        '**********************************************************************************'
        pivotGridControl3.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        '**********************************************************************************'
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelay_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelay.EditValueChanged
        pivotGridControl.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        '**********************************************************************************'
        pivotGridControl1.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        '**********************************************************************************'
        pivotGridControl2.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        '**********************************************************************************'
        pivotGridControl3.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        '**********************************************************************************'
    End Sub
    '</seUpdateDelay>


#End Region

#Region "Procedure"
    Private Sub LoadEmpYearOld()
        Dim _Qry As String = ""
        Dim dt As New DataTable
        '_Spls = New HI.TL.SplashScreen("Loading Data...  Age Length  Please Wait... ")

        Try

            _Qry &= vbCrLf & "  SELECT A.*,B.*,CASE WHEN b.FNType IS NULL then 0 else 1 end AS CountEmp"
            _Qry &= vbCrLf & "FROM ("
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "SELECT x.FTNameTH AS FTName,x.FNEmpSex,XA.*"
            Else
                _Qry &= vbCrLf & "SELECT x.FTNameEN AS FTName,x.FNEmpSex,XA.*"
            End If

            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "(SELECT        FNListIndex AS FNEmpSex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
            _Qry &= vbCrLf & "WHERE        (FTListName = N'FNEmpSex')) AS X"
            _Qry &= vbCrLf & "cross join"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'0 ต่ำกว่า 20 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'21-25 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'26-30 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'31-35 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'36-40 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'41-45 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'46-50 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'51-55 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 9 AS FNType,'55 ขึ้นไป'AS FNTypeDesc"
                _Qry &= vbCrLf & ") AS XA"

            Else
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'0 less than 20 year ' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'21-25 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'26-30 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'31-35 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'36-40 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'41-45 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'46-50 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'51-55 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 9 AS FNType,'55 more than'AS FNTypeDesc"
                _Qry &= vbCrLf & ") AS XA"

            End If

            _Qry &= vbCrLf & ") AS A LEFT JOIN"
            _Qry &= vbCrLf & "(SELECT     "
            _Qry &= vbCrLf & "CASE WHEN FNYear <= 20  then 1 "
            _Qry &= vbCrLf & "WHEN FNYear >= 21 AND FNYear <= 25  then 2"
            _Qry &= vbCrLf & "WHEN FNYear >= 26 AND FNYear <= 30  then 3"
            _Qry &= vbCrLf & "WHEN FNYear >= 31 AND FNYear <= 35  then 4"
            _Qry &= vbCrLf & "WHEN FNYear >= 36 AND FNYear <= 40  then 5"
            _Qry &= vbCrLf & "WHEN FNYear >= 41 AND FNYear <= 45  then 6"
            _Qry &= vbCrLf & "WHEN FNYear >= 46 AND FNYear <= 50  then 7"
            _Qry &= vbCrLf & "WHEN FNYear >= 51 AND FNYear <= 55  then 8"
            _Qry &= vbCrLf & "ELSE 9 END AS FNType"
            _Qry &= vbCrLf & "													, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, "
            _Qry &= vbCrLf & "           FNHSysSectId, FNHSysUnitSectId, FNEmpSex,FTNationalityName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "                         FROM            (SELECT        FNHSysEmpID, CONVERT(int, FNWorkage) AS FNYear, FNEmpSex, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId"
            _Qry &= vbCrLf & "                                                      , FNHSysUnitSectId,FTNationalityName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "                                                   FROM            (SELECT        E.FNHSysEmpID, FNEmpSex, E.FNHSysEmpTypeId, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"
            _Qry &= vbCrLf & "													, YEAR(GETDATE()) - YEAR(FDBirthDate) AS FNWorkage"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "

            _Qry &= vbCrLf & "  WHERE        (E.FTEmpCode <> '')"
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            Select Case FNEmpStatusReport.SelectedIndex
                Case 0
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select
            _Qry &= vbCrLf & ") AS A) AS A)  AS B ON A.FNType=B.FNType AND A.FNEmpSex=B.FNEmpSex"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.pivotGridControl.DataSource = dt.Copy



        Catch ex As Exception


        End Try

        dt.Dispose()

    End Sub

    Private Sub LoadEmpYearOldPerCen()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        '_Spls = New HI.TL.SplashScreen("Loading Data...  Age Length  Please Wait... ")
        Try
            _Qry = "            DECLARE @EmpTotal AS numeric (18,5)"
            _Qry &= vbCrLf & "SET @EmpTotal =0"
            _Qry &= vbCrLf & "SELECT @EmpTotal = COUNT(FNHSysEmpID) "
            _Qry &= vbCrLf & "FROM (SELECT E.FNHSysEmpID"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & "  WHERE        (E.FTEmpCode <> '')"
            Select Case FNEmpStatusReport.SelectedIndex
                Case 0
                Case Else
                    _Qry &= vbCrLf & " AND FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "

            End Select
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            _Qry &= vbCrLf & ") AS A"
            _Qry &= vbCrLf & "SELECT A.*,B.*,CASE WHEN B.FNType IS NULL then 0.00 ELSE 1.00/@EmpTotal*100 END AS CountEmp"
            _Qry &= vbCrLf & "FROM ("
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "SELECT x.FTNameTH AS FTName,x.FNEmpSex,XA.*"
            Else
                _Qry &= vbCrLf & "SELECT x.FTNameEN AS FTName,x.FNEmpSex,XA.*"
            End If

            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "(SELECT        FNListIndex AS FNEmpSex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
            _Qry &= vbCrLf & "WHERE        (FTListName = N'FNEmpSex')) AS X"
            _Qry &= vbCrLf & "cross join"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'0 ต่ำกว่า 20 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'21-25 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'26-30 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'31-35 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'36-40 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'41-45 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'46-50 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'51-55 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 9 AS FNType,'55 ขึ้นไป'AS FNTypeDesc"
                _Qry &= vbCrLf & ") AS XA"
            Else
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'0 less than 20 year ' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'21-25 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'26-30 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'31-35 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'36-40 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'41-45 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'46-50 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'51-55 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 9 AS FNType,'55 more than'AS FNTypeDesc"
                _Qry &= vbCrLf & ") AS XA"
            End If
            _Qry &= vbCrLf & ") AS A LEFT JOIN"
            _Qry &= vbCrLf & "(SELECT     "
            _Qry &= vbCrLf & "CASE WHEN FNYear <= 20  then 1 "
            _Qry &= vbCrLf & "WHEN FNYear >= 21 AND FNYear <= 25  then 2"
            _Qry &= vbCrLf & "WHEN FNYear >= 26 AND FNYear <= 30  then 3"
            _Qry &= vbCrLf & "WHEN FNYear >= 31 AND FNYear <= 35  then 4"
            _Qry &= vbCrLf & "WHEN FNYear >= 36 AND FNYear <= 40  then 5"
            _Qry &= vbCrLf & "WHEN FNYear >= 41 AND FNYear <= 45  then 6"
            _Qry &= vbCrLf & "WHEN FNYear >= 46 AND FNYear <= 50  then 7"
            _Qry &= vbCrLf & "WHEN FNYear >= 51 AND FNYear <= 55  then 8"
            _Qry &= vbCrLf & "ELSE 9 END AS FNType"
            _Qry &= vbCrLf & "													, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, "
            _Qry &= vbCrLf & "           FNHSysSectId, FNHSysUnitSectId, FNEmpSex,FTNationalityName,FTName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "                         FROM            (SELECT        FNHSysEmpID, CONVERT(int, FNWorkage) AS FNYear, FNEmpSex, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId"
            _Qry &= vbCrLf & "                                                      , FNHSysUnitSectId,FTNationalityName,FTName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "                                                   FROM            (SELECT        E.FNHSysEmpID, FNEmpSex, E.FNHSysEmpTypeId, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"
            _Qry &= vbCrLf & "													, YEAR(GETDATE()) - YEAR(FDBirthDate) AS FNWorkage"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName, TT.FTNameTH AS FTName ,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, TT.FTNameEN AS FTName ,ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "
            _Qry &= vbCrLf & "																			  /*WHERE HERE*/"
            _Qry &= vbCrLf & "  WHERE        (E.FTEmpCode <> '')"
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            Select Case FNEmpStatusReport.SelectedIndex
                Case 0
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select
            _Qry &= vbCrLf & ") AS A) AS A)  AS B ON A.FNType=B.FNType AND A.FNEmpSex=B.FNEmpSex"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.pivotGridControl1.DataSource = dt.Copy


        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadEmpWorking_Life()
        Dim _Qry As String = ""
        Dim dt As DataTable

        '_Spls = New HI.TL.SplashScreen("Loading Data...  Age Length  Please Wait... ")
        Try

            _Qry = "SELECT A.*,B.*,CASE WHEN B.FNType IS NULL then 0 else 1 end AS CountEmp"
            _Qry &= vbCrLf & "FROM"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "(SELECT X.FTNameTH AS FTName,X.FNEmpSex,XA.*"
            Else
                _Qry &= vbCrLf & "(SELECT X.FTNameEN AS FTName,X.FNEmpSex,XA.*"
            End If
            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "(SELECT        FNListIndex AS FNEmpSex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
            _Qry &= vbCrLf & "WHERE        (FTListName = N'FNEmpSex')) AS X"
            _Qry &= vbCrLf & "cross join"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'1) ไม่เกิน 3 เดือน' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'2) 4 เดือน ไม่เกิน 1 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'3) 1 ปี ไม่เกิน 3 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'4) 3 ปี ไม่เกิน 6 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'5) 6 ปี ไม่เกิน 10 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'6) 10 ปี ไม่เกิน 15 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'7) 15 ปี ไม่เกิน 20 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'8) 20 ปีขึ้นไป'AS FNTypeDesc"

            Else
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'1) Less than 3 month' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'2) 4 month Less than 1 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'3) 1 year Less than 3 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'4) 3 year Less than 6 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'5) 6 year Less than 10 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'6) 10 year Less than 15 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'7) 15 year Less than 20 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'8) 20 year More than'AS FNTypeDesc"
            End If
            _Qry &= vbCrLf & ") AS XA"
            _Qry &= vbCrLf & ") AS A LEFT JOIN"
            _Qry &= vbCrLf & "(SELECT     "
            _Qry &= vbCrLf & "CASE WHEN FNYear <= 3  then 1 "
            _Qry &= vbCrLf & "WHEN FNYear > 3 AND FNYear <= 12  then 2"
            _Qry &= vbCrLf & "WHEN FNYear > 12 AND  FNYear <= 36  then 3"
            _Qry &= vbCrLf & "WHEN FNYear > 36 AND FNYear <= 72  then 4"
            _Qry &= vbCrLf & "WHEN FNYear > 72 AND FNYear <= 120  then 5"
            _Qry &= vbCrLf & "WHEN FNYear > 120 AND FNYear <= 180 then 6"
            _Qry &= vbCrLf & "WHEN FNYear > 180 AND FNYear <= 240 then 7"
            _Qry &= vbCrLf & "ELSE 8 END AS FNType"

            _Qry &= vbCrLf & "					, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId,"
            _Qry &= vbCrLf & "           FNHSysSectId, FNHSysUnitSectId, FNEmpSex,FTNationalityName,FTName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "                         FROM            (SELECT        FNHSysEmpID, CONVERT(int, FNWorkage) AS FNYear, FNEmpSex, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId"
            _Qry &= vbCrLf & "                                                      , FNHSysUnitSectId,FTNationalityName,FTName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "                                                   FROM            (SELECT        E.FNHSysEmpID, FNEmpSex, E.FNHSysEmpTypeId, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"
            _Qry &= vbCrLf & "													, [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(FDDateStart, FDDateEnd)  AS FNWorkage"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName, TT.FTNameTH AS FTName ,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, TT.FTNameEN AS FTName ,ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "

            _Qry &= vbCrLf & "											/*WHERE HERE*/"
            _Qry &= vbCrLf & "  WHERE        (E.FTEmpCode <> '')"
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            Select Case FNEmpStatusReport.SelectedIndex
                Case 0
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select
            _Qry &= vbCrLf & ") AS A"
            _Qry &= vbCrLf & ") AS A"
            _Qry &= vbCrLf & ")  AS B ON A.FNType=B.FNType AND A.FNEmpSex=B.FNEmpSex"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.pivotGridControl2.DataSource = dt.Copy

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadEmpWork_Life_Percent()
        Dim _Qry As String = ""
        Dim dt As DataTable

        '_Spls = New HI.TL.SplashScreen("Loading Data...  Age Length  Please Wait... ")
        Try
            _Qry = " Declare @countTotalEmp numeric(18,5) "
            _Qry &= vbCrLf & "Set @countTotalEmp=0"
            _Qry &= vbCrLf & "SELECT @countTotalEmp = COUNT(FNHSysEmpID) "
            _Qry &= vbCrLf & "FROM (SELECT        E.FNHSysEmpID"
            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & "  WHERE        (E.FTEmpCode <> '')"
            Select Case FNEmpStatusReport.SelectedIndex
                Case 0
                Case Else
                    _Qry &= vbCrLf & " AND FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "

            End Select
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            _Qry &= vbCrLf & ""
            _Qry &= vbCrLf & ") AS A"
            _Qry &= vbCrLf & ""

            _Qry &= vbCrLf & "SELECT A.*,B.*,CASE WHEN B.FNType IS NULL then 0.00 ELSE 1.00/@countTotalEmp*100 END AS CountEmp"
            _Qry &= vbCrLf & "FROM"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "(SELECT X.FTNameTH AS FTName,X.FNEmpSex,XA.*"
            Else
                _Qry &= vbCrLf & "(SELECT X.FTNameEN AS FTName,X.FNEmpSex,XA.*"
            End If
            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "(SELECT        FNListIndex AS FNEmpSex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
            _Qry &= vbCrLf & "WHERE        (FTListName = N'FNEmpSex')) AS X"
            _Qry &= vbCrLf & "cross join"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'1) ไม่เกิน 3 เดือน' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'2) 4 เดือน ไม่เกิน 1 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'3) 1 ปี ไม่เกิน 3 ปี' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'4) 3 ปี ไม่เกิน 6 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'5) 6 ปี ไม่เกิน 10 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'6) 10 ปี ไม่เกิน 15 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'7) 15 ปี ไม่เกิน 20 ปี'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'8) 20 ปีขึ้นไป'AS FNTypeDesc"
            Else
                _Qry &= vbCrLf & "(SELECT 1 AS FNType,'1) Less than 3 month' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 2 AS FNType,'2) 4 month Less than 1 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 3 AS FNType,'3) 1 year Less than 3 year' AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 4 AS FNType,'4) 3 year Less than 6 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 5 AS FNType,'5) 6 year Less than 10 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 6 AS FNType,'6) 10 year Less than 15 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 7 AS FNType,'7) 15 year Less than 20 year'AS FNTypeDesc"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT 8 AS FNType,'8) 20 year More than'AS FNTypeDesc"
            End If
            _Qry &= vbCrLf & ") AS XA"
            _Qry &= vbCrLf & ") AS A LEFT JOIN"
            _Qry &= vbCrLf & "(SELECT     "
            _Qry &= vbCrLf & "CASE WHEN FNYear <= 3  then 1 "
            _Qry &= vbCrLf & "WHEN FNYear > 3 AND FNYear <= 12  then 2"
            _Qry &= vbCrLf & "WHEN FNYear > 12 AND  FNYear <= 36  then 3"
            _Qry &= vbCrLf & "WHEN FNYear > 36 AND FNYear <= 72  then 4"
            _Qry &= vbCrLf & "WHEN FNYear > 72 AND FNYear <= 120  then 5"
            _Qry &= vbCrLf & "WHEN FNYear > 120 AND FNYear <= 180 then 6"
            _Qry &= vbCrLf & "WHEN FNYear > 180 AND FNYear <= 240 then 7"
            _Qry &= vbCrLf & "ELSE 8 END AS FNType"

            _Qry &= vbCrLf & ", FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, "
            _Qry &= vbCrLf & "FNHSysSectId, FNHSysUnitSectId, FNEmpSex,FTNationalityName,FTName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "FROM (SELECT FNHSysEmpID, CONVERT(int, FNWorkage) AS FNYear, FNEmpSex, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId"
            _Qry &= vbCrLf & ", FNHSysUnitSectId,FTNationalityName,FTName,FTEmpTypeName,FTDivisionName,FTDeName,FTUnitSectName,FTSectName"
            _Qry &= vbCrLf & "FROM (SELECT E.FNHSysEmpID, FNEmpSex, E.FNHSysEmpTypeId, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"
            _Qry &= vbCrLf & ", [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(FDDateStart, FDDateEnd)  AS FNWorkage"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName, TT.FTNameTH AS FTName ,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, TT.FTNameEN AS FTName ,ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "

            _Qry &= vbCrLf & "											/*WHERE HERE*/"
            _Qry &= vbCrLf & "  WHERE        (E.FTEmpCode <> '')"
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  De.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   U.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            Select Case FNEmpStatusReport.SelectedIndex
                Case 0
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select
            _Qry &= vbCrLf & ") AS A"
            _Qry &= vbCrLf & ") AS A"
            _Qry &= vbCrLf & ")  AS B ON A.FNType=B.FNType AND A.FNEmpSex=B.FNEmpSex"


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.pivotGridControl3.DataSource = dt.Copy

        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        'If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then


        Dim _Spls As New HI.TL.SplashScreen("Loading Data Employee Age Please Wait... ")
        Try
            Call LoadEmpYearOld()
            Call LoadEmpYearOldPerCen()
            Call LoadEmpWorking_Life()
            Call LoadEmpWork_Life_Percent()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try



        'Else
        ' HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1406130001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        'End If
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub



End Class
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils
Imports System.Windows.Forms

Public Class wHRBIEmpSex

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        '*********************************************************************************'
        PivotGridControl1.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl1.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        '*********************************************************************************'
        PivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl2.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        PivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        '*********************************************************************************'
        PivotGridControl3.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl3.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        PivotGridControl3.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl3.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        ChartControl.CrosshairOptions.ShowArgumentLine = False
        chartControl1.CrosshairOptions.ShowArgumentLine = False
        chartControl2.CrosshairOptions.ShowArgumentLine = False
        ChartControl3.CrosshairOptions.ShowArgumentLine = False

        'Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        'For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
        '    If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
        '        Continue For
        '    End If
        '    comboChartType.Properties.Items.Add(type)
        'Next type
        'comboChartType.SelectedItem = ViewType.Bar
        ChartControl.DataSource = pivotGridControl
        chartControl1.DataSource = PivotGridControl1
        chartControl2.DataSource = PivotGridControl2
        ChartControl3.DataSource = PivotGridControl3


    End Sub

#Region "Chart"
    '<comboChartType>
    'Private Sub comboBoxEdit2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboChartType.SelectedIndexChanged
    '    chartControl.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))
    '    chartControl1.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))
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
    '        '************************************************************************************************************************'

    '    Next series

    'End Sub
    '</comboChartType>

    '<checkShowPointLabels>
    Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabels.CheckedChanged
        ChartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        '***************************************************************************************'
        chartControl1.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl1.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        '***************************************************************************************'
        chartControl2.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl2.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        '***************************************************************************************'
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

        '*********************************************'
        PivotGridControl1.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
        '*********************************************'
        PivotGridControl2.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
        '*********************************************'
        PivotGridControl3.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
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
    Private Sub LoadData(ByVal _Spls As HI.TL.SplashScreen)
        Dim _Qry As String = ""
        Dim dt As New DataTable

        _Spls = New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = " SELECT A.*,1 AS Emptotal"
            _Qry &= vbCrLf & "FROM(SELECT        N.FTNationalityCode, E.FNEmpSex,N.FTNationalityNameTH, N.FTNationalityNameEN"
            _Qry &= vbCrLf & ",E.FNHSysEmpTypeId, E.FNEmpStatus, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName, TT.FTNameTH AS FTName ,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, TT.FTNameEN AS FTName ,ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM            THRMEmployee AS E WITH(NOLOCK) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  HITECH_MASTER.dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM HITECH_SYSTEM.dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "
            'WHERE HERE
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
            _Qry &= vbCrLf & ")AS A"

            Clipboard.SetText(_Qry)

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.pivotGridControl.DataSource = dt.Copy

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub
    Private Sub LoadEmpSex(ByVal _Spls As HI.TL.SplashScreen)

        Dim _Qry As String = ""
        Dim dt As New DataTable
        _Spls = New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = " Declare @countEmp as numeric (18,2)"
            _Qry &= vbCrLf & "Set @countEmp=0"
            _Qry &= vbCrLf & "SELECT     @countEmp=  Count( FNHSysEmpID)"
            _Qry &= vbCrLf & "            FROM           (SELECT        E.FNHSysEmpID"
            _Qry &= vbCrLf & "										 FROM            THRMEmployee AS E WITH(NOLOCK) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  HITECH_MASTER.dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
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
            _Qry &= vbCrLf & "											 ) AS A"
            _Qry &= vbCrLf & "SELECT A.*,CASE WHEN @countEmp >0 THEN 1.00/@countEmp * 100.00 ELSE 0.00 END AS Emptotal"


            ' _Qry &= vbCrLf & "SELECT A.*,CASE WHEN @countEmp >0 THEN convert(decimal(16,2),(1.00/@countEmp*100.00)) ELSE 0.00 END AS Emptotal"


            _Qry &= vbCrLf & "FROM(SELECT        N.FTNationalityCode, E.FNEmpSex,N.FTNationalityNameTH, N.FTNationalityNameEN"
            _Qry &= vbCrLf & ",E.FNHSysEmpTypeId, E.FNEmpStatus, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName, TT.FTNameTH AS FTName ,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, TT.FTNameEN AS FTName ,ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM            THRMEmployee AS E WITH(NOLOCK) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  HITECH_MASTER.dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM HITECH_SYSTEM.dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "
            'Where Here
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
            _Qry &= vbCrLf & ")AS A"


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.PivotGridControl1.DataSource = dt.Copy

            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
        End Try

    End Sub

    Private Sub LoadEmpNational(ByVal _Spls As HI.TL.SplashScreen)
        Dim _Qry As String = ""
        Dim dt As New DataTable
        _Spls = New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = "            SELECT A.*,1 AS Emptotal"
            _Qry &= vbCrLf & "FROM(SELECT        N.FTNationalityCode, E.FNEmpSex,N.FTNationalityNameTH, N.FTNationalityNameEN"
            _Qry &= vbCrLf & ",E.FNHSysEmpTypeId, E.FNEmpStatus, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName, TT.FTNameTH AS FTName ,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, TT.FTNameEN AS FTName ,ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM            THRMEmployee AS E WITH(NOLOCK) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  HITECH_MASTER.dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM HITECH_SYSTEM.dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "
            'Where Here
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
            _Qry &= vbCrLf & ")AS A"


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.PivotGridControl2.DataSource = dt.Copy

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

    End Sub

    Private Sub LoadEmpNationalPercentage(ByVal _Spls As HI.TL.SplashScreen)

        Dim _Qry As String = ""
        Dim dt As New DataTable
        _Spls = New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = " Declare @countEmp as numeric (18,2)"
            _Qry &= vbCrLf & "Set @countEmp=0"
            _Qry &= vbCrLf & "SELECT     @countEmp=  Count( FNHSysEmpID)"
            _Qry &= vbCrLf & "            FROM           (SELECT        E.FNHSysEmpID"
            _Qry &= vbCrLf & "										 FROM            THRMEmployee AS E WITH(NOLOCK) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  HITECH_MASTER.dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
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
                _Qry &= vbCrLf & " AND  FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            _Qry &= vbCrLf & "											 ) AS A"
            _Qry &= vbCrLf & "SELECT A.*,CASE WHEN @countEmp >0 THEN 1.00/@countEmp*100.00 ELSE 0.00 END AS Emptotal"
            _Qry &= vbCrLf & "FROM(SELECT        N.FTNationalityCode, E.FNEmpSex,N.FTNationalityNameTH, N.FTNationalityNameEN"
            _Qry &= vbCrLf & ",E.FNHSysEmpTypeId, E.FNEmpStatus, E.FNHSysDeptId, E.FNHSysDivisonId, E.FNHSysSectId, E.FNHSysUnitSectId"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", N.FTNationalityNameTH AS FTNationalityName, TT.FTNameTH AS FTName ,ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameTH AS FTDivisionName,De.FTDeptDescTH AS FTDeName,U.FTUnitSectNameTH AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ", N.FTNationalityNameEN AS FTNationalityName, TT.FTNameEN AS FTName ,ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Di.FTDivisonNameEN AS FTDivisionName,De.FTDeptDescEN AS FTDeName,U.FTUnitSectNameEN AS FTUnitSectName"
                _Qry &= vbCrLf & ",S.FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & "FROM            THRMEmployee AS E WITH(NOLOCK) LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS ET ON ET.FNHSysEmpTypeId = E.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS Di WITH(NOLOCK) ON Di.FNHSysDivisonId = E.FNHSysDivisonId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS De WITH(NOLOCK) ON De.FNHSysDeptId = E.FNHSysDeptId "
            _Qry &= vbCrLf & "      LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S WITH(NOLOCK) ON S.FNHSysSectId = E.FNHSysSectId "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U WITH(NOLOCK) ON U.FNHSysUnitSectId = E.FNHSysUnitSectId  "
            _Qry &= vbCrLf & "		LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMPosition AS P WITH(NOLOCK) ON P.FNHSysPositId = E.FNHSysPositId INNER JOIN"
            _Qry &= vbCrLf & "                  HITECH_MASTER.dbo.THRMNationality AS N WITH(NOLOCK) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "        FROM HITECH_SYSTEM.dbo.HSysListData"
            _Qry &= vbCrLf & "WHERE (FTListName = N'FNEmpSex')) AS TT ON E.FNEmpSex = TT.FNListIndex "
            'Where Here
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
            _Qry &= vbCrLf & ")AS A"


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.PivotGridControl3.DataSource = dt.Copy
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        'If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try
            Call LoadData(_Spls)
            Call LoadEmpSex(_Spls)
            Call LoadEmpNational(_Spls)
            Call LoadEmpNationalPercentage(_Spls)
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()

        End Try


        'Else
        'HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1406130001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        'End If
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub



End Class
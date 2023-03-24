Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils

Public Class wHREmpBlood

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
        pivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        chartControl.CrosshairOptions.ShowArgumentLine = False
        ChartControl1.CrosshairOptions.ShowArgumentLine = False


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

    End Sub

#Region "Chart"
    ''<comboChartType>
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
    ' ''</comboChartType>

    '<checkShowPointLabels>
    Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabels.CheckedChanged
        chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
        '************************************************************************************************************************************************************'
        ChartControl1.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ChartControl1.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

    End Sub
    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVertical_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVertical.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnly.CheckedChanged
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
        '************************************************************************************************************************************************************'
        PivotGridControl1.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        PivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelay_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelay.EditValueChanged
        pivotGridControl.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        PivotGridControl1.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))

    End Sub
    '</seUpdateDelay>


#End Region



#Region "Procedure"
    Private Sub LoadData(_Spls As HI.TL.SplashScreen)
        Dim _Qry As String = ""
        Dim dt As New DataTable

        _Spls.UpdateInformation("Loading Data... Please Wait... ")

        Try
            _Qry = " SELECT Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex, E.FNHSysEmpID, 1 AS Total  "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",  FTUnitSectNameTH as FTUnitSectName ,FTNationalityNameTH AS FTNationality ,FTBldNameTH AS FTBldCode ,FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Qry &= vbCrLf & ",  FTUnitSectNameEN as FTUnitSectName ,FTNationalityNameEN AS FTNationality ,FTBldNameEN AS FTBldCode ,FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Qry &= vbCrLf & " FROM  THRMEmployee AS E with (Nolock) LEFT JOIN HITECH_MASTER.dbo.THRMBlood AS B with (Nolock) ON E.FNHSysBldId = B.FNHSysBldId"
            _Qry &= vbCrLf & "                          LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS D with (Nolock) ON E.FNHSysDivisonId = D.FNHSysDivisonId "
            _Qry &= vbCrLf & "                            LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Qry &= vbCrLf & "                              LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Qry &= vbCrLf & "                                LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "                                  LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "                                    LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " Where E.FNHSysEmpTypeId >0  And E.FNEmpStatus <> 2 "
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
                _Qry &= vbCrLf & " AND FDDateStart >= '" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "'"
            End If
            If Me.FTDateEnd.Text <> "" Then
                _Qry &= vbCrLf & " AND FDDateEnd <= '" & HI.UL.ULDate.ConvertEnDB(FTDateEnd.Text) & "'"
            End If
            '    _Qry &= vbCrLf & " AND E.FNHSysEmpTypeCode =" & Integer.Parse(Me.FNHSysEmpTypeId.Properties.Tag)
            'End If
            'If Me.FNHSysDeptId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND M.FTsDeptCode >=" & Integer.Parse(Me.FNHSysDeptId.Properties.Tag)
            'End If
            'If Me.FNHSysDeptIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND M.FTDeptCode <=" & Integer.Parse(Me.FNHSysDeptIdTo.Properties.Tag)
            'End If
            'If Me.FNHSysDivisonId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND D.FTDivisonCode >=" & Integer.Parse(Me.FNHSysDivisonId.Properties.Tag)
            'End If
            'If Me.FNHSysDivisonIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND D.FTDivisonCode <=" & Integer.Parse(Me.FNHSysDivisonIdTo.Properties.Tag)
            'End If
            'If Me.FNHSysSectId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND S.FTSectCode >=" & Integer.Parse(Me.FNHSysSectId.Properties.Tag)
            'End If
            'If Me.FNHSysSectIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND S.FTSectCode <=" & Integer.Parse(Me.FNHSysSectIdTo.Properties.Tag)
            'End If
            'If Me.FNHSysUnitSectId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND U.FTUnitSectCode >=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            'End If
            'If Me.FNHSysUnitSectIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND U.FTUnitSectCode <=" & Integer.Parse(Me.FNHSysUnitSectIdTo.Properties.Tag)
            'End If
            '_Qry &= vbCrLf & "Where E.FNHSysEmpTypeId =" & Integer.Parse(Me.FNHSysEmpTypeId.Properties.Tag)
            'If Me.FNHSysDivisonId.Text <> "" Then
            '    _Qry &= vbCrLf & "AND D.FTDivisonCode >='" & HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) & "'"
            'End If
            'If Me.FNHSysDivisonIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & "AND D.FTDivisonCode <='" & HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) & "'"
            'End If
            'If Me.FNHSysDeptId.Text <> "" Then
            '    _Qry &= vbCrLf & "AND M.FNHSysDepId >='" & HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) & "'"
            'End If
            'If Me.FNHSysDeptIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & "AND M.FNHSysDeptId <='" & HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) & "'"
            'End If
            'If Me.FNHSysSectId.Text <> "" Then
            '    _Qry &= vbCrLf & "AND S.FNHSysSectId >='" & HI.UL.ULF.rpQuoted(FNHSysSectId.Text) & "'"
            'End If
            'If Me.FNHSysSectIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & "AND S.FNHSysSectId <='" & HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) & "'"
            'End If
            'If Me.FNHSysUnitSectId.Text <> "" Then
            '    _Qry &= vbCrLf & "AND U.FNHSysUnitSectId >= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
            'End If
            'If Me.FNHSysUnitSectIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & "AND U.FNHSysUnitSectId <= '" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
            'End If
            '_Qry &= vbCrLf & " Group By B.FTBldCode, N.FTNationalityNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.pivotGridControl.DataSource = dt.Copy
 
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub
#End Region

#Region "Procedure1"
    Private Sub LoadDataP(_Spls As HI.TL.SplashScreen)
        Dim _Per As String = ""
        Dim dtp As New DataTable

        _Spls.UpdateInformation("Loading Data... Please Wait... ")

        Try


            _Per = "DECLARE @blo AS numeric(18, 0)"
            _Per &= vbCrLf & "SET @blo = (SELECT COUNT(FNHSysEmpID) FROM THRMEmployee WHERE  FNEmpStatus <> 2) "
            _Per &= vbCrLf & "SELECT Case When E.FNEmpSex = 0 Then 'M' else 'F' end as FNEmpSex, CASE WHEN @blo >0 THEN 1.00/@blo*100 ELSE 0.00 END AS BloPercent "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Per &= vbCrLf & ", FTUnitSectNameTH as FTUnitSectName ,FTNationalityNameTH as FTNationality ,FTBldNameTH as FTBldCode ,FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Per &= vbCrLf & ", FTUnitSectNameEN as FTUnitSectName ,FTNationalityNameEN as FTNationality ,FTBldNameEN as FTBldCode ,FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Per &= vbCrLf & "FROM         THRMEmployee AS E with (Nolock) LEFT JOIN HITECH_MASTER.dbo.THRMBlood AS B with (Nolock) ON E.FNHSysBldId = B.FNHSysBldId"
            _Per &= vbCrLf & "					LEFT OUTER JOIN HITECH_MASTER.dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Per &= vbCrLf & "					  LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDivision AS D with (Nolock) ON E.FNHSysDivisonId = D.FNHSysDivisonId "
            _Per &= vbCrLf & "						LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Per &= vbCrLf & "						  LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Per &= vbCrLf & "							LEFT OUTER JOIN HITECH_MASTER.dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Per &= vbCrLf & "                            LEFT OUTER JOIN HITECH_MASTER.dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Per &= vbCrLf & "Where E.FNHSysEmpTypeId >0  And E.FNEmpStatus <> 2"
           

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
                _Per &= vbCrLf & " AND FDDateStart >= '" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "'"
            End If
            If Me.FTDateEnd.Text <> "" Then
                _Per &= vbCrLf & " AND FDDateEnd <= '" & HI.UL.ULDate.ConvertEnDB(FTDateEnd.Text) & "'"
            End If
            '_Per &= vbCrLf & "GROUP BY B.FTBldCode, N.FTNationalityNameTH, Case When E.FNEmpSex = 0 Then 'M' else 'F' end"



            dtp = HI.Conn.SQLConn.GetDataTable(_Per, Conn.DB.DataBaseName.DB_HR)

            Me.PivotGridControl1.DataSource = dtp.Copy


        Catch ex As Exception
            _Spls.Close()
        End Try

        dtp.Dispose()

    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Call LoadData(_Spls)
        Call LoadDataP(_Spls)
        _Spls.Close()
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
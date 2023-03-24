Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils
Imports DevExpress.XtraPivotGrid

Public Class wHREmpEdu

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

        chartControl.CrosshairOptions.ShowArgumentLine = False
        ChartControl1.CrosshairOptions.ShowArgumentLine = False

        chartControl.DataSource = pivotGridControl
        ChartControl1.DataSource = PivotGridControl1

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

        _Spls.UpdateInformation("Loading DataEducation Please Wait... ")

        Try
            _Qry = " SELECT    A.*,B.*,CASE WHEN B.FTTypeCourse IS NULL then 0 else 1 end AS EmpCount"
            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "("
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "SELECT x.FTNameTH AS FTSexType,x.FNEmpSex,XA.*"
            Else
                _Qry &= vbCrLf & "SELECT x.FTNameEN AS FTSexType,x.FNEmpSex,XA.*"
            End If
            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "(SELECT        FNListIndex AS FNEmpSex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
            _Qry &= vbCrLf & "WHERE        (FTListName = N'FNEmpSex')) AS X"
            _Qry &= vbCrLf & "cross Join"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "(SELECT FNCourseSeq AS FTTypeCourse,Right('00' + convert(nvarchar(30),convert(int,FNCourseSeq)),2)+' ) '+FTCourseNameTH AS FTCourseName"
            Else
                _Qry &= vbCrLf & "(SELECT FNCourseSeq AS FTTypeCourse,Right('00' + convert(nvarchar(30),convert(int,FNCourseSeq)),2)+' ) '+FTCourseNameEN AS FTCourseName"
            End If
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..THRMCourse"
            _Qry &= vbCrLf & ") AS XA"
            _Qry &= vbCrLf & ") AS A LEFT JOIN"
            _Qry &= vbCrLf & "(SELECT FNCourseSeq as FTTypeCourse"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",  FTNationalityNameTH AS FTNationality, FTUnitSectNameTH as FTUnitSectName, FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName ,FNEmpSex"
            Else
                _Qry &= vbCrLf & ",  FTNationalityNameEN AS FTNationality, FTUnitSectNameEN as FTUnitSectName, FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName ,FNEmpSex"
            End If
            _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E with (Nolock) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeEducation AS B with (Nolock) ON E.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMCourse AS C with (Nolock) ON B.FNHSysCourseId = C.FNHSysCourseId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS D with (Nolock) ON E.FNHSysDivisonId = D.FNHSysDivisonId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Qry &= vbCrLf & " WHERE     (E.FNHSysEmpTypeId > 0)AND (E.FDDateEnd = '' OR E.FDDateEnd >= Convert (nvarchar (10),GetDate (),111)) "

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
           
            _Qry &= vbCrLf & ") AS B ON A.FTTypeCourse=B.FTTypeCourse AND A.FNEmpSex=B.FNEmpSex AND A.FTTypeCourse=b.FTTypeCourse ORDER BY A.FTTypeCourse asc"

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
        Dim dt As New DataTable

        _Spls.UpdateInformation("Loading DataEducation Please Wait... ")

        Try
            _Per = " DECLARE @edu AS numeric(18, 0)"
            _Per &= vbCrLf & " SET @edu = 0"
            _Per &= vbCrLf & "SELECT @edu=COUNT(FNHSysEmpID)"
            _Per &= vbCrLf & "FROM (SELECT        E.FNHSysEmpID"
            _Per &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E with (Nolock) "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeEducation AS B with (Nolock) ON E.FNHSysEmpID = B.FNHSysEmpID "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMCourse AS C with (Nolock) ON B.FNHSysCourseId = C.FNHSysCourseId"
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS D with (Nolock) ON E.FNHSysDivisonId = D.FNHSysDivisonId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Per &= vbCrLf & " WHERE  (E.FTEmpCode <> '') "
            Select Case FNEmpStatusReport.SelectedIndex
                Case 0
                Case Else
                    _Per &= vbCrLf & " AND FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "

            End Select
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
            _Per &= vbCrLf & ") AS AA"


            _Per &= vbCrLf & " SELECT A.*,B.*,CASE WHEN B.FTTypeCourse IS NULL then 0.00 else 1/@edu*100 end AS EmpCount"
            _Per &= vbCrLf & "FROM ("
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Per &= vbCrLf & "SELECT x.FTNameTH AS FTSexType,x.FNEmpSex,XA.*"
            Else
                _Per &= vbCrLf & "SELECT x.FTNameEN AS FTSexType,x.FNEmpSex,XA.*"
            End If
            _Per &= vbCrLf & "FROM"
            _Per &= vbCrLf & "(SELECT        FNListIndex AS FNEmpSex, FTNameTH, FTNameEN"
            _Per &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
            _Per &= vbCrLf & "WHERE        (FTListName = N'FNEmpSex')) AS X"
            _Per &= vbCrLf & "cross join"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Per &= vbCrLf & "(SELECT FNCourseSeq AS FTTypeCourse,Right('00' + convert(nvarchar(30),convert(int,FNCourseSeq)),2)+' ) '+FTCourseNameTH AS FTCourseName"
            Else
                _Per &= vbCrLf & "(SELECT FNCourseSeq AS FTTypeCourse,Right('00' + convert(nvarchar(30),convert(int,FNCourseSeq)),2)+' ) '+FTCourseNameEN AS FTCourseName"
            End If
            _Per &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..THRMCourse"
            _Per &= vbCrLf & ") AS XA"
            _Per &= vbCrLf & ") AS A LEFT JOIN"

            _Per &= vbCrLf & "(SELECT FNCourseSeq as FTTypeCourse,FNEmpSex"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Per &= vbCrLf & ",  FTCourseNameTH as FTCourseName ,FTNationalityNameTH AS FTNationality, FTUnitSectNameTH as FTUnitSectName, FTEmpTypeNameTH AS FTEmpTypeName ,FTDivisonNameTH AS FTDivisonName ,FTDeptDescTH AS FTDeptName ,FTSectNameTH AS FTSectName"
            Else
                _Per &= vbCrLf & ",  FTCourseNameEN as FTCourseName ,FTNationalityNameEN AS FTNationality, FTUnitSectNameEN as FTUnitSectName, FTEmpTypeNameEN AS FTEmpTypeName ,FTDivisonNameEN AS FTDivisonName ,FTDeptDescEN AS FTDeptName ,FTSectNameEN AS FTSectName"
            End If
            _Per &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E with (Nolock) "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeEducation AS B with (Nolock) ON E.FNHSysEmpID = B.FNHSysEmpID "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMCourse AS C with (Nolock) ON B.FNHSysCourseId = C.FNHSysCourseId"
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS T with (Nolock) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId"
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS D with (Nolock) ON E.FNHSysDivisonId = D.FNHSysDivisonId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS M with (Nolock) ON E.FNHSysDeptId = M.FNHSysDeptId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S with (Nolock) ON E.FNHSysSectId = S.FNHSysSectId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U with (Nolock) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Per &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS N with (Nolock) ON E.FNHSysNationalityId = N.FNHSysNationalityId "
            _Per &= vbCrLf & " WHERE     (E.FNHSysEmpTypeId > 0)AND (E.FDDateEnd = '' OR E.FDDateEnd >= Convert (nvarchar (10),GetDate (),111)) "
            
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
            _Per &= vbCrLf & ") AS B ON A.FTTypeCourse=B.FTTypeCourse AND A.FNEmpSex=B.FNEmpSex  ORDER BY A.FTTypeCourse asc"

            dt = HI.Conn.SQLConn.GetDataTable(_Per, Conn.DB.DataBaseName.DB_HR)

            Me.PivotGridControl1.DataSource = dt.Copy


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
        Call LoadDataP(_Spls)
        Call LoadData(_Spls)
        _Spls.Close()
       
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode


    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
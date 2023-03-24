Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wHRBIWageSummary

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
   
    End Sub

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
            _Qry = "EXEC SP_BI_WageSummary '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTYear.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTYearTo.Text) & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "  SELECT   A.FTPayYear, A.FNMonth, A.FNNetpay "
            '_Qry &= vbCrLf & "  '  A.UserLogin, A.FNHSysCmpId, A.FNHSysEmpTypeId, A.FNHSysDeptId, A.FNHSysDivisonId, A.FNHSysSectId, A.FNHSysUnitSectId"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

                _Qry &= vbCrLf & ", C.FTCmpNameTH AS FTCmpName"
                _Qry &= vbCrLf & ", ET.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Dept.FTDeptDescTH AS FTDeptName"
                _Qry &= vbCrLf & ", Div.FTDivisonNameTH AS FTDivisonName"
                _Qry &= vbCrLf & ", Sect.FTSectNameTH AS FTSectName"
                _Qry &= vbCrLf & ", US.FTUnitSectNameTH AS FTUnitSectName"

            Else
                _Qry &= vbCrLf & ", C.FTCmpNameEN AS FTCmpName"
                _Qry &= vbCrLf & ", ET.FTEmpTypeNameEN AS FTEmpTypeName"
                _Qry &= vbCrLf & ", Dept.FTDeptDescEN AS FTDeptName"
                _Qry &= vbCrLf & ", Div.FTDivisonNameEN AS FTDivisonName"
                _Qry &= vbCrLf & ", Sect.FTSectNameEN AS FTSectName"
                _Qry &= vbCrLf & ", US.FTUnitSectNameEN AS FTUnitSectName"

            End If

            '_Qry &= vbCrLf & "   , C.FTCmpCode, C.FTCmpNameTH, "
            '_Qry &= vbCrLf & "   C.FTCmpNameEN, ET.FTEmpTypeCode, ET.FTEmpTypeNameTH, ET.FTEmpTypeNameEN, Dept.FTDeptCode, Dept.FTDeptDescTH, Dept.FTDeptDescEN, Div.FTDivisonCode, Div.FTDivisonNameTH,"
            '_Qry &= vbCrLf & "   Div.FTDivisonNameEN, Sect.FTSectCode, Sect.FTSectNameTH, Sect.FTSectNameEN, US.FTUnitSectCode, US.FTUnitSectNameTH, US.FTUnitSectNameEN"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TBITWageSummary AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON A.FNHSysCmpId = C.FNHSysCmpId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON A.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS Sect WITH(NOLOCK)  ON A.FNHSysSectId = Sect.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Div WITH(NOLOCK) ON A.FNHSysDivisonId = Div.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept ON A.FNHSysDeptId = Dept.FNHSysDeptId"
            _Qry &= vbCrLf & "  WHERE  A.UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "  DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TBITWageSummary"
            _Qry &= vbCrLf & "  WHERE  UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.pivotGridControl.DataSource = dt.Copy

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
        If Me.FTYear.Text <> "" And Me.FTYearTo.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1406130001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub
End Class
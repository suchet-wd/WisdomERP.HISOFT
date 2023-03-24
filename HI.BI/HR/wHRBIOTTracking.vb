Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraPivotGrid

Public Class wHRBIOTTracking

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
            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_BI_OTTracking '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTYearTo.Text) & "'," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ",'' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "  SELECT   "
            _Qry &= vbCrLf & " Left(A.FTStartDate,4) AS FTYear, A.FTWeek"
            _Qry &= vbCrLf & " ,Convert(varchar(10), Convert(Datetime,A.FTStartDate),103) + ' - ' + Convert(varchar(10), Convert(Datetime,A.FTEndDate),103)  FTDateWeek"
            _Qry &= vbCrLf & ", A.FNEmpTotal"
            _Qry &= vbCrLf & ", A.FNOTOver3"
            _Qry &= vbCrLf & ",CASE WHEN A.FNEmpTotal >0 THEN Convert(numeric(18,2),(A.FNOTOver3 /  Convert(numeric(18,2),A.FNEmpTotal))*100.00) ELSE 0.00 END  AS FNOTOver3Per "
            _Qry &= vbCrLf & ", A.FNWorkTime60"
            _Qry &= vbCrLf & ",CASE WHEN A.FNEmpTotal >0 THEN Convert(numeric(18,2),(A.FNWorkTime60 /  Convert(numeric(18,2),A.FNEmpTotal))*100.00) ELSE 0.00 END  AS FNWorkTime60Per "
            _Qry &= vbCrLf & ", A.FNWorkTime6172"
            _Qry &= vbCrLf & ",CASE WHEN A.FNEmpTotal >0 THEN Convert(numeric(18,2),(A.FNWorkTime6172 /  Convert(numeric(18,2),A.FNEmpTotal))*100.00) ELSE 0.00 END  AS FNWorkTime6172Per "
            _Qry &= vbCrLf & ",A.FNWorkTimeOver72"
            _Qry &= vbCrLf & ",CASE WHEN A.FNEmpTotal >0 THEN Convert(numeric(18,2),(A.FNWorkTimeOver72 /  Convert(numeric(18,2),A.FNEmpTotal))*100.00) ELSE 0.00 END  AS FNWorkTimeOver72Per "
            _Qry &= vbCrLf & ", A.FNSunday"
            _Qry &= vbCrLf & ",CASE WHEN A.FNEmpTotal >0 THEN Convert(numeric(18,2),(A.FNSunday /  Convert(numeric(18,2),A.FNEmpTotal))*100.00) ELSE 0.00 END  AS FNSundayPer "
            _Qry &= vbCrLf & ", A.FNLacking7"
            _Qry &= vbCrLf & ", 0.00 AS FNLacking7Per"
            _Qry &= vbCrLf & ", A.FNLacking14"
            _Qry &= vbCrLf & ", 0.00 AS FNLacking14Per"
            _Qry &= vbCrLf & ", A.FNSwistch"
            _Qry &= vbCrLf & ", 0.00 AS FNSwistchPer"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", C.FTCmpNameTH AS FTCmpName"
                _Qry &= vbCrLf & ", US.FTUnitSectNameTH AS FTUnitSectName"

            Else
                _Qry &= vbCrLf & ", C.FTCmpNameEN AS FTCmpName"
                _Qry &= vbCrLf & ", US.FTUnitSectNameEN AS FTUnitSectName"
            End If

            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TBITOTTracking AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON A.FNHSysCmpId = C.FNHSysCmpId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & "  WHERE  A.UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "  DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TBITOTTracking"
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

    Private Sub pivotGridControl_CustomSummary(sender As Object, e As DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs) Handles pivotGridControl.CustomSummary
        Try
            Dim ds As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
            Dim Rx As PivotDrillDownDataRow
            If Not (ds Is Nothing) Then

            End If

            Select Case e.FieldName.ToString
                Case "FNOTOver3Per"
                    Dim Sum1 As Integer = 0
                    Dim Sum2 As Integer = 0
                    For I As Integer = 0 To ds.RowCount
                        Rx = ds(I)
                        If Not (Rx Is Nothing) Then
                            Sum1 = Sum1 + Integer.Parse(Val(Rx.Item("FNEmpTotal")))
                            Sum2 = Sum2 + Integer.Parse(Val(Rx.Item("FNOTOver3")))
                        End If
                    Next
                    If Sum1 > 0 Then
                        e.CustomValue = CDbl(Format((Sum2 / Sum1) * 100.0, "0.00"))
                    Else
                        e.CustomValue = 0.0
                    End If

                Case "FNWorkTime60Per"
                    Dim Sum1 As Integer = 0
                    Dim Sum2 As Integer = 0
                    For I As Integer = 0 To ds.RowCount
                        Rx = ds(I)
                        If Not (Rx Is Nothing) Then
                            Sum1 = Sum1 + Integer.Parse(Val(Rx.Item("FNEmpTotal")))
                            Sum2 = Sum2 + Integer.Parse(Val(Rx.Item("FNWorkTime60")))
                        End If
                    Next
                    If Sum1 > 0 Then
                        e.CustomValue = CDbl(Format((Sum2 / Sum1) * 100.0, "0.00"))
                    Else
                        e.CustomValue = 0.0
                    End If
                Case "FNWorkTime6172Per"
                    Dim Sum1 As Integer = 0
                    Dim Sum2 As Integer = 0
                    For I As Integer = 0 To ds.RowCount
                        Rx = ds(I)
                        If Not (Rx Is Nothing) Then
                            Sum1 = Sum1 + Integer.Parse(Val(Rx.Item("FNEmpTotal")))
                            Sum2 = Sum2 + Integer.Parse(Val(Rx.Item("FNWorkTime6172")))
                        End If
                    Next
                    If Sum1 > 0 Then
                        e.CustomValue = CDbl(Format((Sum2 / Sum1) * 100.0, "0.00"))
                    Else
                        e.CustomValue = 0.0
                    End If
                Case "FNWorkTimeOver72Per"
                    Dim Sum1 As Integer = 0
                    Dim Sum2 As Integer = 0
                    For I As Integer = 0 To ds.RowCount
                        Rx = ds(I)
                        If Not (Rx Is Nothing) Then
                            Sum1 = Sum1 + Integer.Parse(Val(Rx.Item("FNEmpTotal")))
                            Sum2 = Sum2 + Integer.Parse(Val(Rx.Item("FNWorkTimeOver72")))
                        End If
                    Next
                    If Sum1 > 0 Then
                        e.CustomValue = CDbl(Format((Sum2 / Sum1) * 100.0, "0.00"))
                    Else
                        e.CustomValue = 0.0
                    End If
                Case "FNSundayPer"
                    Dim Sum1 As Integer = 0
                    Dim Sum2 As Integer = 0
                    For I As Integer = 0 To ds.RowCount
                        Rx = ds(I)
                        If Not (Rx Is Nothing) Then
                            Sum1 = Sum1 + Integer.Parse(Val(Rx.Item("FNEmpTotal")))
                            Sum2 = Sum2 + Integer.Parse(Val(Rx.Item("FNSunday")))
                        End If
                    Next
                    If Sum1 > 0 Then
                        e.CustomValue = CDbl(Format((Sum2 / Sum1) * 100.0, "0.00"))
                    Else
                        e.CustomValue = 0.0
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
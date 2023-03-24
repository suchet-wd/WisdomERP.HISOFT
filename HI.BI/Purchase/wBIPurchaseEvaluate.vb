
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wBIPurchaseEvaluate

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

#Region "Procedure"


    Private Function Verifydata() As Boolean

        Dim _Pass As Boolean = False

        If FNHSysBuyId.Text <> "" Then
            _Pass = True
        End If

        If FNHSysStyleId.Text <> "" Then
            _Pass = True
        End If

        If FNHSysSeasonId.Text <> "" Then
            _Pass = True
        End If

        If FTSDate.Text <> "" Or FTEDate.Text <> "" Then
            If HI.UL.ULDate.ConvertEnDB(FTSDate.Text) <> "" And HI.UL.ULDate.ConvertEnDB(FTEDate.Text) <> "" Then
                _Pass = True
            End If
        End If
        Return _Pass

    End Function


    Private Sub LoadListData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable
        _Qry = "SELECT '1' AS FTSelect"
        _Qry &= vbCrLf & ", FNListIndex"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", FTNameTH AS FTName"
        Else
            _Qry &= vbCrLf & ", FTNameEN AS FTName "
        End If

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & "WHERE  (FTListName = N'FNMerMatType')"
        _Qry &= vbCrLf & " ORDER BY FNListIndex"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        Me.ogc.DataSource = _Dt.Copy
        _Dt.Dispose()
    End Sub

    Private Sub Loaddata()

        Dim _dtmattype As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกประเภทวัตถุดิบ !!!", 1613052478, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            _dtmattype = .Copy
        End With
        Dim _Qry As String = ""

        Dim _FNHSysBuyId As Integer = Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString()))
        Dim _FNHSysStyleId As Integer = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString()))
        Dim _FNHSysSeasonId As Integer = Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString()))
        Dim _FNAllMattype As String = ""
        Dim _FTOrderNo As String = ""
        Dim _Lang As String = "TH"

        For Each Rxm As DataRow In _dtmattype.Select("FTSelect='1'")

            If _FNAllMattype = "" Then
                _FNAllMattype = Rxm!FNListIndex.ToString
            Else
                _FNAllMattype = _FNAllMattype & "," & Rxm!FNListIndex.ToString
            End If

        Next

        If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
            _Lang = "EN"
        End If

        Dim Spls As New HI.TL.SplashScreen("Loading...,Please Wait.")

        Try

            Dim _dt As DataTable
            Dim username As String = ""
            'username = "mlpsirikanya"
            username = HI.ST.UserInfo.UserName


            Dim StrSDate As String = HI.UL.ULDate.ConvertEnDB(FTSDate.Text)
            Dim StrEDate As String = HI.UL.ULDate.ConvertEnDB(FTEDate.Text)

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_SULEVU " & _FNHSysBuyId & "," & _FNHSysStyleId & "," & _FNHSysSeasonId & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "','" & HI.UL.ULF.rpQuoted(username) & "','" & HI.UL.ULF.rpQuoted(_Lang) & "'," & Me.FNPOEvaluateOrderType.SelectedIndex & ",'" & _FNAllMattype & "','" & StrSDate & "','" & StrEDate & "'," & Val(FNHSysCustId.Properties.Tag.ToString) & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()

        Catch ex As Exception
        End Try

        Spls.Close()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click

        Me.Close()

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If Me.Verifydata() Then
            Me.Loaddata()
        End If

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        HI.TL.HandlerControl.ClearControl(Me)

        Call LoadListData()

    End Sub

    Private Sub wBIPurchaseEvaluate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadListData()
    End Sub

End Class
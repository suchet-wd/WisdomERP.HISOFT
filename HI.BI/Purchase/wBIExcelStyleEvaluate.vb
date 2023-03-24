Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO

Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wBIExcelStyleEvaluate




    Private _CustItemRef As String = ""
    Private Property CustItemRef As String
        Get
            Return _CustItemRef
        End Get
        Set(value As String)
            _CustItemRef = value
        End Set
    End Property

    Private mdt As System.Data.DataTable

    Public Sub New()

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

    Private Property DataExcel As System.Data.DataTable
        Get
            Return mdt
        End Get
        Set(value As System.Data.DataTable)
            mdt = value
        End Set
    End Property

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load

        LoadListData()

    End Sub

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
                    opFileDialog.ShowDialog()

                    Try
                        If opFileDialog.FileName <> "" Then

                            Dim _Pls As New HI.TL.SplashScreen("Reading...File Please Wait...")

                            Try

                                Dim _FileName As String = opFileDialog.FileName

                                FTFilePath.Text = _FileName

                                Select Case Path.GetExtension(_FileName)
                                    Case ".xls"
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

                                    Case ".xlsx"
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                                    Case ".xlsm"
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)

                                    Case Else
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)


                                End Select

                            Catch ex As Exception
                            End Try

                            GetSpreedSheet()

                            LoadListData()
                            otb.SelectedTabPageIndex = 0
                            _Pls.Close()

                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                End Try

            Case Else
                '...do nothing
        End Select
    End Sub
    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub

    Private Sub FNRceceiveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FTMapStyleCode.SelectedIndexChanged

    End Sub

    Private Sub opshet_ActiveSheetChanged(sender As Object, e As DevExpress.Spreadsheet.ActiveSheetChangedEventArgs) Handles opshet.ActiveSheetChanged

        GetSpreedSheet()
    End Sub

    Private Sub GetSpreedSheet(Optional SetList As Boolean = True)

        DataExcel = Nothing

        Try
            Dim dt As New System.Data.DataTable
            Dim ColName As String = ""

            If SetList Then
                FTMapStyleCode.Properties.Items.Clear()
                FTMapSeasonCode.Properties.Items.Clear()
            End If


            With opshet.ActiveWorksheet
                For C As Integer = 0 To .GetUsedRange().ColumnCount - 1
                    Try
                        ColName = .Columns(C).Heading()
                    Catch ex As Exception
                        ColName = C.ToString()
                    End Try

                    dt.Columns.Add(ColName, GetType(String))

                    If SetList Then
                        FTMapStyleCode.Properties.Items.Add(ColName)
                        FTMapSeasonCode.Properties.Items.Add(ColName)
                    End If

                Next

                For r As Integer = 1 To .GetUsedRange().RowCount - 1

                    Dim Rx As System.Data.DataRow = dt.NewRow()

                    For C As Integer = 0 To .GetUsedRange().ColumnCount - 1

                        ColName = .Columns(C).Heading()

                        If .Cells(r, C).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then
                            Rx.Item(ColName) = HI.UL.ULDate.ConvertEN(.Cells(r, C).Value.DateTimeValue)

                        Else
                            Rx.Item(ColName) = .Cells(r, C).DisplayText
                        End If

                    Next

                    dt.Rows.Add(Rx)

                Next
            End With

            DataExcel = dt.Copy

        Catch ex As Exception

        End Try
        If SetList Then
            FTMapStyleCode.SelectedIndex = -1
            FTMapSeasonCode.SelectedIndex = -1
        End If


    End Sub

    Private Sub ocmImportnetprice_Click(sender As Object, e As EventArgs) Handles ocmImportnetprice.Click
        Try

            If FTMapStyleCode.Text <> "" And FTMapSeasonCode.Text <> "" Then
                Dim dtstyle As New System.Data.DataTable
                dtstyle.Columns.Add("FNSeq", GetType(Integer))
                dtstyle.Columns.Add("FTStyleCode", GetType(String))
                dtstyle.Columns.Add("FTSeasonCode", GetType(String))


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


                GetSpreedSheet(False)


                If Not (DataExcel Is Nothing) Then
                    If DataExcel.Rows.Count > 0 Then

                        Dim RIdx As Integer = 0
                        Dim RowSeq As Integer = 0

                        Dim Spls As New HI.TL.SplashScreen("Import Excel Style Evaluate....")

                        Try
                            Dim cmd As String = " Delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TTEMPImportStyleEvaluate WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PUR)

                            For Each R As DataRow In DataExcel.Rows
                                ' If RIdx > 0 Then

                                If R.Item(FTMapStyleCode.Text).ToString().Trim <> "" And R.Item(FTMapSeasonCode.Text).ToString().Trim <> "" Then

                                    RowSeq = RowSeq + 1
                                    dtstyle.Rows.Add(RowSeq, R.Item(FTMapStyleCode.Text).ToString().Trim(), R.Item(FTMapSeasonCode.Text).ToString().Trim())


                                    cmd = "insert into   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TTEMPImportStyleEvaluate (FTUserLogin, FNRowSeq, FTStyleCode, FTSeasonCode)"
                                    cmd &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & RowSeq & ",'" & HI.UL.ULF.rpQuoted(R.Item(FTMapStyleCode.Text).ToString().Trim()) & "','" & HI.UL.ULF.rpQuoted(R.Item(FTMapSeasonCode.Text).ToString().Trim) & "'"
                                    HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PUR)


                                End If


                                ' End If
                                RIdx = RIdx + 1
                            Next
                        Catch ex As Exception

                        End Try

                        Try

                            Dim _dt As DataTable
                            Dim username As String = ""
                            'username = "mlpsirikanya"
                            username = HI.ST.UserInfo.UserName



                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAEXCEL_STYLE_EVALUATE '" & HI.UL.ULF.rpQuoted(username) & "','" & HI.UL.ULF.rpQuoted(_Lang) & "','" & _FNAllMattype & "'"
                            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

                            Me.pivotGridControl.DataSource = _dt.Copy

                            _dt.Dispose()

                        Catch ex As Exception
                        End Try
                        otb.SelectedTabPageIndex = 1
                        Spls.Close()

                    End If

                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
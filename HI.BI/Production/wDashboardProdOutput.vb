Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraCharts
Imports System.Windows.Forms
Imports System
Imports System.Drawing

Public Class wDashboardProdOutput

    Private dt1 As DataTable
    Private dt1show As DataTable
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        pivotGridControl.OptionsChartDataSource.SelectionOnly = False
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = True
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = False
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
        ChartControl.CrosshairOptions.ShowArgumentLine = False
        'ChartControl.DataSource = pivotGridControl
        InitDataChart()
        InitDataChartEff()
        ''cmp
        'PivotGridControl1.OptionsChartDataSource.ProvideDataByColumns = False
        'PivotGridControl1.OptionsChartDataSource.SelectionOnly = False
        'PivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = True
        'PivotGridControl1.OptionsChartDataSource.ProvideRowGrandTotals = False
        'PivotGridControl1.OptionsChartDataSource.ProvideColumnGrandTotals = False
        'chartControl2.CrosshairOptions.ShowArgumentLine = False
        'chartControl2.DataSource = PivotGridControl1
        'chartControl2.Visible = False

        ''Line
        'PivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = False
        'PivotGridControl2.OptionsChartDataSource.SelectionOnly = False
        'PivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = True
        'PivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = False
        'PivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = False
        'chartControl3.CrosshairOptions.ShowArgumentLine = False
        'chartControl3.DataSource = PivotGridControl2
        'chartControl3.Visible = False



        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub LoadCompany()
        Dim _Str As String

        _Str = " SELECT   '1' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameTH"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        Else

            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameEN"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
        _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub


#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub

#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()

        ogc1.DataSource = Nothing
        ogc2.DataSource = Nothing
        ogc3.DataSource = Nothing
        ogc4.DataSource = Nothing
        ogc5.DataSource = Nothing
        ogc6.DataSource = Nothing

        Dim dts As New DataSet
        Dim Ridx As Integer = 0

        'Dim dt1show As New DataTable
        Dim dt2show As New DataTable
        Dim dt3show As New DataTable
        Dim dt4show As New DataTable
        Dim dt5show As New DataTable
        Dim dt6show As New DataTable


        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dt4 As New DataTable
        Dim dt5 As New DataTable
        Dim dt6 As New DataTable

        Dim _StartDate As String = Microsoft.VisualBasic.Right(FDDate.Text, 4) + "/" + Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(FDDate.Text, 7), 2)
        Dim _EndDate As String = Microsoft.VisualBasic.Right(FDDateTo.Text, 4) + "/" + Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(FDDateTo.Text, 7), 2)
        Dim _CmpCode As String
        Dim _Qry As String = ""

        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            If _dtcmp.Select("FTSelect='1'").Length > 0 Then
                _dtcmp.Dispose()

                Dim _ServerName, _UID, _PWS, _DBName As String
                Dim _ConnectString As String = ""
                Dim _FNHSysCmpId As Integer = 0

                For Each R As DataRow In _dtcmp.Select("FTSelect='1'")

                    _CmpCode = R!FTCmpCode.ToString

                    If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then
                        Ridx = Ridx + 1

                        _ServerName = R!FTIPServer.ToString
                        _UID = HI.Conn.DB.UIDName
                        _PWS = HI.Conn.DB.PWDName
                        _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                        _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                        _Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")
                        Try


                            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT_DASH '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            dt1 = GetSewFGData(_Qry, _ConnectString)

                            If Ridx = 1 Then
                                dt1show = dt1.Clone
                            End If

                            dt1show.Merge(dt1)


                            If CheckEdit1.Checked Then
                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER_RATIO '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt2 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt2show = dt2.Clone
                                End If

                                dt2show.Merge(dt2)
                            End If




                            If CheckEdit2.Checked Then

                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt3 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt3show = dt3.Clone
                                End If

                                dt3show.Merge(dt3)
                            End If



                            If CheckEdit3.Checked Then

                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER_RESIGN '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt4 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt4show = dt4.Clone
                                End If

                                dt4show.Merge(dt4)
                            End If



                            If CheckEdit4.Checked Then
                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_HR_MONEY '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt5 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt5show = dt5.Clone
                                End If

                                dt5show.Merge(dt5)
                            End If



                            If CheckEdit5.Checked Then
                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT_DEFECT '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt6 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt6show = dt6.Clone
                                End If

                                dt6show.Merge(dt6)
                            End If



                        Catch ex22 As Exception
                            ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                        End Try

                    End If

                Next

            End If
            _dtcmp.Dispose()


            ogc1.DataSource = dt1show
            Me.ogv1.BestFitColumns()

            ogc2.DataSource = dt2show
            Me.ogv2.BestFitColumns()

            ogc3.DataSource = dt3show
            Me.ogv3.BestFitColumns()

            ogc4.DataSource = dt4show
            Me.ogv4.BestFitColumns()

            ogc5.DataSource = dt5show
            Me.ogv5.BestFitColumns()


            ogc6.DataSource = dt6show
            Me.ogv6.BestFitColumns()


            pivotGridControl.DataSource = dt1show
            'LoadChart(dt1show)

            dt1show = modifyData(dt1show)



            InitDataChart()
            InitDataChartEff()
        Catch ex As Exception
        End Try

        _Spls.Close()


    End Sub

    Private Function modifyData(dt As DataTable) As DataTable
        Try
            Dim dtx As DataTable = dt.Select("FDScanDate >='" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' and FDScanDate <='" & HI.UL.ULDate.ConvertEnDB(FDDateTo.Text) & "'").CopyToDataTable


            'Dim _Cmd As String = ""
            '_Cmd = "select  FNHSysUnitSectId , FTUnitSectCode"
            '_Cmd &= vbCrLf & " from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMUnitSect"
            '_Cmd &= vbCrLf & " where FTStateActive = '1'"
            '_Cmd &= vbCrLf & "  and FTStateSew = '1'"
            '_Cmd &= vbCrLf & " and isnull(FTStateRelease,'0') = '0' "
            '_Cmd &= vbCrLf & ""
            'Dim _UnitSectDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            'Dim _dxMerge As New DataTable
            'For Each R As DataRow In _UnitSectDt.Rows
            '    Try
            '        Dim _dtn As DataTable = dtx.Select("FTUnitSectCode='" & R!FTUnitSectCode.ToString & "'").CopyToDataTable
            '        _dxMerge.Merge(_dtn)
            '    Catch ex As Exception

            '    End Try
            'Next
            'dtx = _dxMerge


            dt = dtx


            Dim _Dtmain As DataTable = dt
            _Dtmain.Columns.Add("FNQuantityCmp", GetType(Integer))
            _Dtmain.Columns.Add("FNQuantityLine", GetType(Integer))
            _Dtmain.Columns.Add("FNQuantityCmpPer", GetType(Double))
            _Dtmain.Columns.Add("FNQuantityLinePer", GetType(Double))
            _Dtmain.Columns.Add("FNQuantityCmpEffPer", GetType(Double))
            _Dtmain.Columns.Add("FNQuantityCmpEff", GetType(Double))
            _Dtmain.Columns.Add("FNQuantityLineEffPer", GetType(Double))
            _Dtmain.Columns.Add("FNQuantityCmpEffPerPlan", GetType(Double))
            _Dtmain.Columns.Add("FNQuantityCmpEffPerOver", GetType(Double))
            Dim dtcmp As New DataTable
            dtcmp.Columns.Add("FTCmpCode", GetType(String))
            Dim _CmdOld As String = ""
            For Each Rx As DataRow In _Dtmain.Select("FTCmpCode <> '' ", "FTCmpCode asc")
                If _CmdOld = "" Or _CmdOld <> Rx!FTCmpCode.ToString Then
                    dtcmp.Rows.Add(Rx!FTCmpCode.ToString)
                    _CmdOld = Rx!FTCmpCode.ToString
                End If

            Next



            For Each R As DataRow In dtcmp.Rows
                Dim _Qty As Integer = dt.Compute("Sum(FNQuantity)", "FTCmpCode='" & R!FTCmpCode.ToString & "'")
                Dim _QtyEff As Integer = dt.Compute("Sum(FNAVGEff)", "FTCmpCode='" & R!FTCmpCode.ToString & "'")
                Dim _Linecount As Integer = dt.Compute("Count(FTUnitSectCode)", "FTCmpCode='" & R!FTCmpCode.ToString & "'")
                For Each rx As DataRow In _Dtmain.Select("FTCmpCode='" & R!FTCmpCode.ToString & "'")
                    rx!FNQuantityCmp = _Qty
                    rx!FNQuantityCmpEff = _QtyEff
                    rx!FNQuantityCmpEffPer = (_QtyEff / _Linecount) / 100
                    rx!FNQuantityCmpEffPerPlan = IIf(((_QtyEff / _Linecount) / 100) > 0.85, 0, 0.85 - ((_QtyEff / _Linecount) / 100))
                    rx!FNQuantityCmpEffPerOver = IIf(((_QtyEff / _Linecount) / 100) > 0.85, 1 - ((_QtyEff / _Linecount) / 100), 0.15)
                Next
            Next

            Dim _Total As Integer = 0

            For Each Rx As DataRow In _Dtmain.Select("FTCmpCode <> '' ", "FTCmpCode asc")
                If _CmdOld = "" Or _CmdOld <> Rx!FTCmpCode.ToString Then
                    _Total += Rx!FNQuantityCmp
                    _CmdOld = Rx!FTCmpCode.ToString
                End If
            Next
            For Each R As DataRow In dtcmp.Rows
                Dim _Qty As Integer = dt.Compute("Sum(FNQuantity)", "FTCmpCode='" & R!FTCmpCode.ToString & "'")
                For Each rx As DataRow In _Dtmain.Select("FTCmpCode='" & R!FTCmpCode.ToString & "'")
                    rx!FNQuantityCmpPer = ((Val(rx!FNQuantityCmp) * 100) / _Total) / 100
                Next
            Next




            Dim dtLine As New DataTable
            dtLine.Columns.Add("FTCmpCode", GetType(String))
            dtLine.Columns.Add("FTUnitSectCode", GetType(String))
            Dim _Line As String = ""
            _CmdOld = ""
            For Each Rx As DataRow In dt.Select("FTCmpCode <> '' ", "FTCmpCode asc, FTUnitSectCode asc  ")
                If (_Line = "" And _CmdOld = "") Or (_Line & "|" & _CmdOld <> Rx!FTUnitSectCode.ToString & "|" & Rx!FTCmpCode.ToString) Then
                    dtLine.Rows.Add(Rx!FTCmpCode.ToString, Rx!FTUnitSectCode.ToString)

                    _CmdOld = Rx!FTCmpCode.ToString
                    _Line = Rx!FTUnitSectCode.ToString
                End If
            Next



            For Each R As DataRow In dtLine.Rows
                Dim _Qty As Integer = dt.Compute("Sum(FNQuantity)", "FTUnitSectCode='" & R!FTUnitSectCode.ToString & "' and FTCmpCode='" & R!FTCmpCode.ToString & "' ")
                Dim _QtyEff As Integer = dt.Compute("Sum(FNAVGEff)", "FTUnitSectCode='" & R!FTUnitSectCode.ToString & "' and FTCmpCode='" & R!FTCmpCode.ToString & "' ")
                Dim _Linecount As Integer = dt.Compute("Count(FDScanDate)", "FTUnitSectCode='" & R!FTUnitSectCode.ToString & "' and FTCmpCode='" & R!FTCmpCode.ToString & "' ")
                For Each rx As DataRow In _Dtmain.Select("FTUnitSectCode='" & R!FTUnitSectCode.ToString & "' and FTCmpCode='" & R!FTCmpCode.ToString & "'")
                    rx!FNQuantityLine = _Qty
                    rx!FNQuantityLinePer = ((_Qty * 100) / Val(rx!FNQuantityCmp)) / 100
                    rx!FNQuantityLineEffPer = (_QtyEff / _Linecount) / 100
                Next
            Next
            'For Each R As DataRow In dtLine.Rows
            '    Dim _Qty As Integer = dt.Compute("Sum(FNQuantity)", "FTUnitSectCode='" & R!FTUnitSectCode.ToString & "' and FTCmpCode='" & R!FTCmpCode.ToString & "' ")
            '    For Each rx As DataRow In _Dtmain.Select("FTUnitSectCode='" & R!FTUnitSectCode.ToString & "' and FTCmpCode='" & R!FTCmpCode.ToString & "'")
            '        rx!FNQuantityLine = _Qty
            '    Next
            'Next
            Return _Dtmain
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FDDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FDDateTo.Text <> "" Then
            _Pass = True
        End If


        Dim _dtcmp As DataTable
        With CType(Me.ogccmp.DataSource, DataTable)
            .AcceptChanges()
            _dtcmp = .Copy
        End With

        If _dtcmp.Select("FTSelect='1'").Length > 0 Then
            _Pass = True
        End If
        CheckEdit0.Checked = True
        If CheckEdit0.Checked = False And CheckEdit1.Checked = False And CheckEdit2.Checked = False And CheckEdit5.Checked = False And CheckEdit4.Checked = False And CheckEdit3.Checked = False Then
            _Pass = False
        End If



        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Call LoadCompany()
            AddHandler ChartControl.ObjectSelected, AddressOf OnChartControlObjectSelected
            AddHandler ChartControl1.ObjectSelected, AddressOf OnChartControlObjectSelectedEff
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then

            Call LoadData()
            InitCmpChart()
            InitCmpChartEff()
            Me.otbdetail.SelectedTabPageIndex = 0

        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)

        Call LoadCompany()

    End Sub

    Private Function GetSewFGData(cmsstring As String, connstring As String) As DataTable
        Dim _Cnn = New System.Data.SqlClient.SqlConnection()
        Dim _Cmd = New System.Data.SqlClient.SqlCommand()
        Dim objDataSet As New DataTable
        Try

            If _Cnn.State = ConnectionState.Open Then
                _Cnn.Close()
            End If
            _Cnn.ConnectionString = connstring
            _Cnn.Open()
            _Cmd = _Cnn.CreateCommand

            Dim _Adepter As New System.Data.SqlClient.SqlDataAdapter(_Cmd)
            _Adepter.SelectCommand.CommandTimeout = 0
            _Adepter.SelectCommand.CommandType = CommandType.Text
            _Adepter.SelectCommand.CommandText = cmsstring
            _Adepter.Fill(objDataSet)
            _Adepter.Dispose()

            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        End Try
        Return objDataSet
    End Function
#End Region


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



    Private Sub chartControl_MouseClick(sender As Object, e As MouseEventArgs)
        Dim hi As ChartHitInfo = ChartControl.CalcHitInfo(e.X, e.Y)

        ' Check whether it was a series point, and if so -  
        ' obtain its argument, and pass it to the detail series. 
        Dim point As SeriesPoint = hi.SeriesPoint

        If point IsNot Nothing Then
            Dim argument As String = point.Argument.ToString()

            ' Flip the series. 
            If ChartControl.Series(0).Visible = True Then
                ChartControl.Series(0).Visible = False

                ChartControl.Series(1).Name = argument
                ChartControl.Series(1).Visible = True

                ' Since the new series determines another diagram's type, 
                ' you should re-define axes properties. 
                Dim diagram As XYDiagram = CType(ChartControl.Diagram, XYDiagram)
                diagram.AxisX.Label.Angle = -25
                diagram.AxisY.NumericOptions.Format = NumericFormat.Currency
                diagram.AxisY.NumericOptions.Precision = 0

                ChartControl.Series(1).DataFilters(0).Value = argument

                ChartControl.Titles(0).Visible = True
                ChartControl.Titles(1).Text = "By Cmp"
            End If
        End If

        ' Obtain the title under the test point. 
        Dim link As ChartTitle = hi.ChartTitle

        ' Check whether the link was clicked, and if so -  
        ' restore the main series. 
        If link IsNot Nothing AndAlso link.Text.StartsWith("Back") Then
            ChartControl.Series(0).Visible = True
            ChartControl.Series(1).Visible = False

            link.Visible = False
            'chartControl.Titles(1).Text = "Sales by Person"
        End If
    End Sub

    Private categories As List(Of String)
    Private linkFont As Font
    Private regularFont As Font



    Protected seriesSelected As Series = Nothing
    Protected pointSelected As SeriesPoint = Nothing
    Protected selectedAnotherObject As Object = Nothing

    Protected Overridable Function AllowSelectAnotherObject(ByVal obj As Object) As Boolean
        Return (Not IsCmpChart) AndAlso (TypeOf obj Is ChartTitle)
    End Function
    Protected Overridable Function AllowSelectAnotherObjectProd(ByVal obj As Object) As Boolean
        Return (Not IsLineChart) AndAlso (TypeOf obj Is ChartTitle)
    End Function
    Protected Overridable Sub OnChartControlObjectSelected(ByVal sender As Object, ByVal e As HotTrackEventArgs)
        If TypeOf e.Object Is Series Then

            If SeriesSelection Then
                Me.seriesSelected = CType(e.Object, Series)
                Me.pointSelected = TryCast(e.AdditionalObject, SeriesPoint)
                e.Cancel = Not SeriesSelection
            ElseIf SeriesLineSelection Then
                Me.seriesSelected = CType(e.Object, Series)
                Me.pointSelected = TryCast(e.AdditionalObject, SeriesPoint)
                e.Cancel = Not SeriesLineSelection
            End If
        Else
            If AllowSelectAnotherObject(e.Object) Then
                Me.selectedAnotherObject = e.Object
                e.Cancel = False
            ElseIf AllowSelectAnotherObjectProd(e.Object) Then
                Me.selectedAnotherObject = e.Object
                e.Cancel = False
            Else
                Me.selectedAnotherObject = Nothing
                e.Cancel = True
                ChartControl.ClearSelection(False)
            End If
            If SeriesSelection Then
                Me.seriesSelected = Nothing
                Me.pointSelected = Nothing

            End If
        End If
        UpdateControls()
    End Sub

    Private Sub InitDataChart()
        Try
            ChartControl.Series.Item(0).DataSource = dt1show
            ChartControl.Series.Item(0).ArgumentDataMember = "FTCmpCode"
            ChartControl.Series.Item(0).ValueDataMembersSerializable = "FNQuantityCmpPer"
            ChartControl.Series.Item(0).ToolTipHintDataMember = "FNQuantityCmp"

            ChartControl.Series.Item(0).Label.PointOptions.PointView = PointView.ArgumentAndValues
            ChartControl.Series.Item(0).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            ChartControl.Series.Item(0).Label.PointOptions.ValueNumericOptions.Precision = 2


            ChartControl.Series.Item(1).DataSource = dt1show
            ChartControl.Series.Item(1).ArgumentDataMember = "FTUnitSectCode"
            ChartControl.Series.Item(1).ValueDataMembersSerializable = "FNQuantityLinePer"

            ChartControl.Series.Item(1).Label.PointOptions.PointView = PointView.ArgumentAndValues
            ChartControl.Series.Item(1).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            ChartControl.Series.Item(1).Label.PointOptions.ValueNumericOptions.Precision = 2

            With ChartControl.Series.Item(1)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTCmpCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With


            ChartControl.Series.Item(2).DataSource = dt1show
            ChartControl.Series.Item(2).ArgumentDataMember = "FDScanDate"
            ChartControl.Series.Item(2).ValueDataMembersSerializable = "FNQuantity"
            With ChartControl.Series.Item(2)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTUnitSectCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With


        Catch ex As Exception

        End Try
    End Sub

    Public Sub UpdateControls()

        If IsCmpChart Then
            If seriesSelected IsNot Nothing AndAlso pointSelected IsNot Nothing Then
                InitLineChart(Me.pointSelected)
                seriesSelected = Nothing
            End If
        ElseIf IsLineChart Then
            If seriesSelected IsNot Nothing AndAlso pointSelected IsNot Nothing Then
                InitDateChart(Me.pointSelected)
                seriesSelected = Nothing
            Else
                If TypeOf selectedAnotherObject Is ChartTitle Then
                    InitCmpChart()
                    selectedAnotherObject = Nothing
                End If
            End If
        ElseIf IsDateChart Then
            If TypeOf selectedAnotherObject Is ChartTitle Then
                InitLineChart(Me.pointSelected)
                selectedAnotherObject = Nothing
            End If
        Else
            If TypeOf selectedAnotherObject Is ChartTitle Then
                InitCmpChart()
                selectedAnotherObject = Nothing
            End If
        End If
    End Sub
    Private ReadOnly Property CmpSeries() As Series
        Get
            Return ChartControl.GetSeriesByName("CmpSeries")
        End Get
    End Property
    Private ReadOnly Property LineSeries() As Series
        Get
            Return ChartControl.GetSeriesByName("LineSeries")
        End Get
    End Property

    Private ReadOnly Property DateSeries() As Series
        Get
            Return ChartControl.GetSeriesByName("DateSeries")
        End Get
    End Property

    Private ReadOnly Property IsCmpChart() As Boolean
        Get
            Return If(CmpSeries IsNot Nothing, CmpSeries.Visible, False)
        End Get
    End Property

    Private ReadOnly Property IsLineChart() As Boolean
        Get
            Return If(LineSeries IsNot Nothing, LineSeries.Visible, False)
        End Get
    End Property

    Private ReadOnly Property IsDateChart() As Boolean
        Get
            Return If(DateSeries IsNot Nothing, DateSeries.Visible, False)
        End Get
    End Property

    Protected ReadOnly Property SeriesSelection() As Boolean
        Get
            Return IsCmpChart
        End Get
    End Property

    Protected ReadOnly Property SeriesLineSelection() As Boolean
        Get
            Return IsLineChart
        End Get
    End Property

    Private Sub InitDateChart(ByVal point As SeriesPoint)
        If CmpSeries IsNot Nothing AndAlso LineSeries IsNot Nothing AndAlso DateSeries IsNot Nothing Then
            CmpSeries.Visible = False
            LineSeries.Visible = False
            DateSeries.DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTUnitSectCode")
            'With ChartControl.Series.Item(1)
            '    .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTCmpCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            'End With

            DateSeries.LegendText = point.Argument
            DateSeries.Visible = True
            Dim view As XYDiagramSeriesViewBase = TryCast(DateSeries.View, XYDiagramSeriesViewBase)
            If view IsNot Nothing Then
                Dim axisX As AxisXBase = view.AxisX
                axisX.Label.Angle = 30
                Dim axisY As AxisYBase = view.AxisY
                axisY.Title.Text = "Quantity"
                axisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                ChartControl.ToolTipOptions.ShowForPoints = True
                ChartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left
                ChartControl.Legend.AlignmentVertical = LegendAlignmentVertical.Top
                ChartControl.Legend.Margins.All = 10
                If ChartControl.Titles.Count > 1 Then
                    ChartControl.Titles(0).Visibility = DevExpress.Utils.DefaultBoolean.False
                    ChartControl.Titles(1).Visibility = DevExpress.Utils.DefaultBoolean.False
                    ChartControl.Titles(2).Visibility = DevExpress.Utils.DefaultBoolean.True
                End If
            End If
        End If
        ChartControl.Animate()
    End Sub

    Private Sub InitLineChart(ByVal point As SeriesPoint)
        If CmpSeries IsNot Nothing AndAlso LineSeries IsNot Nothing AndAlso DateSeries IsNot Nothing Then
            CmpSeries.Visible = False
            DateSeries.Visible = False
            LineSeries.DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTCmpCode")

            'With ChartControl.Series.Item(1)
            '    .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTCmpCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            'End With

            LineSeries.LegendText = point.Argument
            LineSeries.Visible = True

            Dim view As XYDiagramSeriesViewBase = TryCast(LineSeries.View, XYDiagramSeriesViewBase)
            If view IsNot Nothing Then
                Dim axisX As AxisXBase = view.AxisX
                axisX.Label.Angle = 30
                Dim axisY As AxisYBase = view.AxisY
                axisY.Title.Text = "Quantity"
                axisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                ChartControl.ToolTipOptions.ShowForPoints = True
                ChartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left
                ChartControl.Legend.AlignmentVertical = LegendAlignmentVertical.Top
                ChartControl.Legend.Margins.All = 10

                If ChartControl.Titles.Count > 1 Then
                    ChartControl.Titles(0).Visibility = DevExpress.Utils.DefaultBoolean.False
                    ChartControl.Titles(1).Visibility = DevExpress.Utils.DefaultBoolean.True
                    ChartControl.Titles(2).Visibility = DevExpress.Utils.DefaultBoolean.False
                End If
            End If
        End If
        ChartControl.Animate()
    End Sub
    Private Sub InitCmpChart()
        If CmpSeries IsNot Nothing Then
            CmpSeries.Visible = True
            ChartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside
            ChartControl.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            ChartControl.Legend.Margins.All = 5
            ChartControl.ToolTipOptions.ShowForPoints = False
            Dim view As XYDiagramSeriesViewBase = TryCast(CmpSeries.View, XYDiagramSeriesViewBase)
            Dim axisY As AxisYBase = view.AxisY
            axisY.Title.Text = "Quantity %"
            LineSeries.Visible = False
            DateSeries.Visible = False
            CmpSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True
            Dim label As SideBySideBarSeriesLabel = TryCast(CmpSeries.Label, SideBySideBarSeriesLabel)
            label.Position = BarSeriesLabelPosition.Top

            If ChartControl.Titles.Count > 1 Then
                ChartControl.Titles(0).Visibility = DevExpress.Utils.DefaultBoolean.True
                ChartControl.Titles(1).Visibility = DevExpress.Utils.DefaultBoolean.False
                ChartControl.Titles(2).Visibility = DevExpress.Utils.DefaultBoolean.False
            End If
        End If
        ChartControl.Animate()
    End Sub



#Region "Eff"

    Protected seriesSelectedEff As Series = Nothing
    Protected pointSelectedEff As SeriesPoint = Nothing
    Protected selectedAnotherObjectEff As Object = Nothing
    Protected Overridable Function AllowSelectAnotherObjectEff(ByVal obj As Object) As Boolean
        Return (Not IsCmpChartEff) AndAlso (TypeOf obj Is ChartTitle)
    End Function
    Protected Overridable Function AllowSelectAnotherObjectProdEff(ByVal obj As Object) As Boolean
        Return (Not IsLineChartEff) AndAlso (TypeOf obj Is ChartTitle)
    End Function
    Protected Overridable Sub OnChartControlObjectSelectedEff(ByVal sender As Object, ByVal e As HotTrackEventArgs)
        If TypeOf e.Object Is Series Then

            If SeriesSelectionEff Then
                Me.seriesSelectedEff = CType(e.Object, Series)
                Me.pointSelectedEff = TryCast(e.AdditionalObject, SeriesPoint)
                e.Cancel = Not SeriesSelectionEff
            ElseIf SeriesLineSelectionEff Then
                Me.seriesSelectedEff = CType(e.Object, Series)
                Me.pointSelectedEff = TryCast(e.AdditionalObject, SeriesPoint)
                e.Cancel = Not SeriesLineSelectionEff
            End If
        Else
            If AllowSelectAnotherObjectEff(e.Object) Then
                Me.selectedAnotherObjectEff = e.Object
                e.Cancel = False
            ElseIf AllowSelectAnotherObjectProdEff(e.Object) Then
                Me.selectedAnotherObjectEff = e.Object
                e.Cancel = False
            Else
                Me.selectedAnotherObjectEff = Nothing
                e.Cancel = True
                ChartControl1.ClearSelection(False)
            End If
            If SeriesSelectionEff Then
                Me.seriesSelectedEff = Nothing
                Me.pointSelectedEff = Nothing

            End If
        End If
        UpdateControlsEff()
    End Sub

    Private Sub InitDataChartEff()
        Try
            ChartControl1.Series.Item(0).DataSource = dt1show
            ChartControl1.Series.Item(0).ArgumentDataMember = "FTCmpCode"
            ChartControl1.Series.Item(0).ValueDataMembersSerializable = "FNQuantityCmpEffPer"
            ChartControl1.Series.Item(0).ToolTipHintDataMember = "FNQuantityCmp"

            ChartControl1.Series.Item(0).Label.PointOptions.PointView = PointView.ArgumentAndValues
            ChartControl1.Series.Item(0).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            ChartControl1.Series.Item(0).Label.PointOptions.ValueNumericOptions.Precision = 2


            ChartControl1.Series.Item(1).DataSource = dt1show
            ChartControl1.Series.Item(1).ArgumentDataMember = "FTUnitSectCode"
            ChartControl1.Series.Item(1).ValueDataMembersSerializable = "FNQuantityLineEffPer"

            ChartControl1.Series.Item(1).Label.PointOptions.PointView = PointView.ArgumentAndValues
            ChartControl1.Series.Item(1).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            ChartControl1.Series.Item(1).Label.PointOptions.ValueNumericOptions.Precision = 2

            With ChartControl1.Series.Item(1)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTCmpCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With


            ChartControl1.Series.Item(2).DataSource = dt1show
            ChartControl1.Series.Item(2).ArgumentDataMember = "FDScanDate"
            ChartControl1.Series.Item(2).ValueDataMembersSerializable = "FNAVGEff"
            ChartControl1.Series.Item(2).Label.PointOptions.PointView = PointView.ArgumentAndValues
            ChartControl1.Series.Item(2).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            ChartControl1.Series.Item(2).Label.PointOptions.ValueNumericOptions.Precision = 2

            With ChartControl1.Series.Item(2)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTUnitSectCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With

            ChartControl1.Series.Item(3).DataSource = dt1show
            ChartControl1.Series.Item(3).ArgumentDataMember = "FDScanDate"
            ChartControl1.Series.Item(3).ValueDataMembersSerializable = "FNQuantity"
            ChartControl1.Series.Item(3).Label.PointOptions.PointView = PointView.ArgumentAndValues
            ChartControl1.Series.Item(3).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            ChartControl1.Series.Item(3).Label.PointOptions.ValueNumericOptions.Precision = 2
            With ChartControl1.Series.Item(3)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTUnitSectCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With

            ChartControl1.Series.Item(4).DataSource = dt1show
            ChartControl1.Series.Item(4).ArgumentDataMember = "FDScanDate"
            ChartControl1.Series.Item(4).ValueDataMembersSerializable = "DefectRate"
            ChartControl1.Series.Item(4).Label.PointOptions.PointView = PointView.ArgumentAndValues
            ChartControl1.Series.Item(4).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            ChartControl1.Series.Item(4).Label.PointOptions.ValueNumericOptions.Precision = 2
            With ChartControl1.Series.Item(4)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTUnitSectCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With


            ChartControl1.Series.Item(5).DataSource = dt1show
            ChartControl1.Series.Item(5).ArgumentDataMember = "FDScanDate"
            ChartControl1.Series.Item(5).ValueDataMembersSerializable = "ManPower"
            With ChartControl1.Series.Item(5)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTUnitSectCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With


            ChartControl1.Series.Item(6).DataSource = dt1show
            ChartControl1.Series.Item(6).ArgumentDataMember = "FTCmpCode"
            ChartControl1.Series.Item(6).ValueDataMembersSerializable = "FNQuantityCmpEffPerPlan"
            ChartControl1.Series.Item(6).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            ChartControl1.Series.Item(6).Label.PointOptions.ValueNumericOptions.Precision = 2


            ChartControl1.Series.Item(7).DataSource = dt1show
            ChartControl1.Series.Item(7).ArgumentDataMember = "FTCmpCode"
            ChartControl1.Series.Item(7).ValueDataMembersSerializable = "FNQuantityCmpEffPerOver"
            ChartControl1.Series.Item(7).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            ChartControl1.Series.Item(7).Label.PointOptions.ValueNumericOptions.Precision = 2


            ChartControl1.Series.Item(8).DataSource = dt1show
            ChartControl1.Series.Item(8).ArgumentDataMember = "FDScanDate"
            ChartControl1.Series.Item(8).ValueDataMembersSerializable = "FNAVGEff"
            ChartControl1.Series.Item(8).Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            ChartControl1.Series.Item(8).Label.PointOptions.ValueNumericOptions.Precision = 2
            With ChartControl1.Series.Item(8)
                .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTUnitSectCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            End With

            ChartControl1.Titles(0).Text = " ALL Branch (EFF %  Date " & Me.FDDate.Text & " - " & Me.FDDateTo.Text & " )"


            'ChartControl1.Titles(1).Text = " (EFF %  Date " & Me.FDDate.Text & " - " & Me.FDDateTo.Text & " )"

            'ChartControl1.Titles(2).Text &= " (EFF %  Date " & Me.FDDate.Text & " - " & Me.FDDateTo.Text & " )"
        Catch ex As Exception

        End Try
    End Sub

    Public Sub UpdateControlsEff()

        If IsCmpChartEff Then
            If seriesSelectedEff IsNot Nothing AndAlso pointSelectedEff IsNot Nothing Then
                InitLineChartEff(Me.pointSelectedEff)
                seriesSelectedEff = Nothing
            End If
        ElseIf IsLineChartEff Then
            If seriesSelectedEff IsNot Nothing AndAlso pointSelectedEff IsNot Nothing Then
                InitDateChartEff(Me.pointSelectedEff)
                seriesSelectedEff = Nothing
            Else
                If TypeOf selectedAnotherObjectEff Is ChartTitle Then
                    InitCmpChartEff()
                    selectedAnotherObjectEff = Nothing
                End If
            End If
        ElseIf IsDateChartEff Then
            If TypeOf selectedAnotherObjectEff Is ChartTitle Then
                InitLineChartEff(Me.pointSelectedEff)
                selectedAnotherObjectEff = Nothing
            End If
        Else
            If TypeOf selectedAnotherObjectEff Is ChartTitle Then
                InitCmpChartEff()
                selectedAnotherObjectEff = Nothing
            End If
        End If
    End Sub
    Private ReadOnly Property CmpSeriesEff() As Series
        Get
            Return ChartControl1.GetSeriesByName("CmpSeries")
        End Get
    End Property
    Private ReadOnly Property LineSeriesEff() As Series
        Get
            Return ChartControl1.GetSeriesByName("LineSeries")
        End Get
    End Property

    Private ReadOnly Property DateSeriesEff() As Series
        Get
            Return ChartControl1.GetSeriesByName("DateSeries")
        End Get
    End Property

    Private ReadOnly Property DateSeriesEff3() As Series
        Get
            Return ChartControl1.GetSeriesByName("DateSeries1")
        End Get
    End Property

    Private ReadOnly Property DateSeriesEff4() As Series
        Get
            Return ChartControl1.GetSeriesByName("DateSeries2")
        End Get
    End Property

    Private ReadOnly Property DateSeriesEff5() As Series
        Get
            Return ChartControl1.GetSeriesByName("DateSeries3")
        End Get
    End Property


    Private ReadOnly Property IsCmpChartEff() As Boolean
        Get
            Return If(CmpSeriesEff IsNot Nothing, CmpSeriesEff.Visible, False)
        End Get
    End Property

    Private ReadOnly Property IsLineChartEff() As Boolean
        Get
            Return If(LineSeriesEff IsNot Nothing, LineSeriesEff.Visible, False)
        End Get
    End Property

    Private ReadOnly Property IsDateChartEff() As Boolean
        Get
            Return If(DateSeriesEff IsNot Nothing, DateSeriesEff.Visible, False)
        End Get
    End Property

    Protected ReadOnly Property SeriesSelectionEff() As Boolean
        Get
            Return IsCmpChartEff
        End Get
    End Property

    Protected ReadOnly Property SeriesLineSelectionEff() As Boolean
        Get
            Return IsLineChartEff
        End Get
    End Property

    Private Sub InitDateChartEff(ByVal point As SeriesPoint)
        If CmpSeriesEff IsNot Nothing AndAlso LineSeriesEff IsNot Nothing AndAlso DateSeriesEff IsNot Nothing Then
            CmpSeriesEff.Visible = False
            ChartControl1.Series.Item(6).Visible = False
            ChartControl1.Series.Item(7).Visible = False
            ChartControl1.Series.Item(8).Visible = True
            LineSeriesEff.Visible = False
            DateSeriesEff.DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTUnitSectCode")
            DateSeriesEff3.DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTUnitSectCode")
            DateSeriesEff4.DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTUnitSectCode")
            DateSeriesEff5.DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTUnitSectCode")
            ChartControl1.Series.Item(8).DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTUnitSectCode")

            'With ChartControl.Series.Item(1)
            '    .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTCmpCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            'End With

            DateSeriesEff.LegendText = point.Argument
            DateSeriesEff.Visible = True
            DateSeriesEff3.Visible = True
            DateSeriesEff4.Visible = True
            DateSeriesEff5.Visible = True

            Dim FullStackedBarSeriesView11 As New DevExpress.XtraCharts.SideBySideBarSeriesView
            FullStackedBarSeriesView11.Color = System.Drawing.Color.LightPink
            DateSeriesEff.View = FullStackedBarSeriesView11
            DateSeriesEff.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            DateSeriesEff.Label.PointOptions.ValueNumericOptions.Precision = 2

            Dim FullStackedBarSeriesView15 As New DevExpress.XtraCharts.SideBySideBarSeriesView
            FullStackedBarSeriesView15.Color = System.Drawing.Color.LimeGreen
            DateSeriesEff3.View = FullStackedBarSeriesView15
            DateSeriesEff3.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            DateSeriesEff3.Label.PointOptions.ValueNumericOptions.Precision = 2

            Dim FullStackedBarSeriesView12 As New DevExpress.XtraCharts.SideBySideBarSeriesView
            FullStackedBarSeriesView12.Color = System.Drawing.Color.OrangeRed
            DateSeriesEff4.View = FullStackedBarSeriesView12
            DateSeriesEff4.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            DateSeriesEff4.Label.PointOptions.ValueNumericOptions.Precision = 2


            Dim FullStackedBarSeriesView13 As New DevExpress.XtraCharts.SideBySideBarSeriesView
            FullStackedBarSeriesView13.Color = System.Drawing.Color.RoyalBlue
            DateSeriesEff5.View = FullStackedBarSeriesView13
            DateSeriesEff5.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number
            DateSeriesEff5.Label.PointOptions.ValueNumericOptions.Precision = 2
            With DirectCast(ChartControl1.Diagram, DevExpress.XtraCharts.XYDiagram)
                .AxisY.Label.TextPattern = "{V:F2}"
            End With



            Dim view As XYDiagramSeriesViewBase = TryCast(DateSeries.View, XYDiagramSeriesViewBase)
            If view IsNot Nothing Then
                Dim axisX As AxisXBase = view.AxisX
                axisX.Label.Angle = 30
                'Dim axisY As AxisYBase = view.AxisY
                'axisY.Title.Text = "Quantity"
                'axisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.False
                ChartControl1.ToolTipOptions.ShowForPoints = True
                ChartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left
                ChartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top
                ChartControl1.Legend.Margins.All = 10
                If ChartControl1.Titles.Count > 1 Then
                    ChartControl1.Titles(0).Visibility = DevExpress.Utils.DefaultBoolean.False
                    ChartControl1.Titles(1).Visibility = DevExpress.Utils.DefaultBoolean.False
                    ChartControl1.Titles(2).Visibility = DevExpress.Utils.DefaultBoolean.True
                    'ChartControl1.Titles(3).Visibility = DevExpress.Utils.DefaultBoolean.True
                    'ChartControl1.Titles(4).Visibility = DevExpress.Utils.DefaultBoolean.True
                    'ChartControl1.Titles(5).Visibility = DevExpress.Utils.DefaultBoolean.True
                End If
            End If
        End If
        ChartControl1.Animate()
    End Sub

    Private Sub InitLineChartEff(ByVal point As SeriesPoint)
        If CmpSeriesEff IsNot Nothing AndAlso LineSeriesEff IsNot Nothing AndAlso DateSeriesEff IsNot Nothing Then
            CmpSeriesEff.Visible = False
            ChartControl1.Series.Item(6).Visible = False
            ChartControl1.Series.Item(7).Visible = False
            ChartControl1.Series.Item(8).Visible = False
            DateSeriesEff.Visible = False
            DateSeriesEff3.Visible = False
            DateSeriesEff4.Visible = False
            DateSeriesEff5.Visible = False
            LineSeriesEff.DataFilters(0).Value = (CType(point.Tag, DataRowView))("FTCmpCode")

            'With ChartControl.Series.Item(1)
            '    .DataFilters.ClearAndAddRange(New DevExpress.XtraCharts.DataFilter() {New DevExpress.XtraCharts.DataFilter("FTCmpCode", "System.String", DevExpress.XtraCharts.DataFilterCondition.Equal, Nothing)})
            'End With
            Dim FullStackedBarSeriesView11 As New DevExpress.XtraCharts.SideBySideBarSeriesView
            LineSeriesEff.LegendText = point.Argument
            LineSeriesEff.Visible = True
            LineSeriesEff.View = FullStackedBarSeriesView11
            LineSeriesEff.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            LineSeriesEff.Label.PointOptions.ValueNumericOptions.Precision = 2
            With DirectCast(ChartControl1.Diagram, DevExpress.XtraCharts.XYDiagram)
                .AxisY.Label.TextPattern = "{V:P0}"
            End With

            Dim view As XYDiagramSeriesViewBase = TryCast(LineSeriesEff.View, XYDiagramSeriesViewBase)
            If view IsNot Nothing Then
                Dim axisX As AxisXBase = view.AxisX
                axisX.Label.Angle = 30
                Dim axisY As AxisYBase = view.AxisY

                'axisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
                ChartControl1.ToolTipOptions.ShowForPoints = True
                ChartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left
                ChartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top
                ChartControl1.Legend.Margins.All = 10

                If ChartControl1.Titles.Count > 1 Then
                    ChartControl1.Titles(0).Visibility = DevExpress.Utils.DefaultBoolean.False
                    ChartControl1.Titles(1).Visibility = DevExpress.Utils.DefaultBoolean.True
                    ChartControl1.Titles(2).Visibility = DevExpress.Utils.DefaultBoolean.False
                    'ChartControl1.Titles(3).Visibility = DevExpress.Utils.DefaultBoolean.False
                    'ChartControl1.Titles(4).Visibility = DevExpress.Utils.DefaultBoolean.False
                    'ChartControl1.Titles(5).Visibility = DevExpress.Utils.DefaultBoolean.False
                End If
            End If
        End If
        ChartControl1.Animate()
    End Sub
    Private Sub InitCmpChartEff()
        If CmpSeriesEff IsNot Nothing Then
            CmpSeriesEff.Visible = True
            ChartControl1.Series.Item(6).Visible = True
            ChartControl1.Series.Item(7).Visible = True
            ChartControl1.Series.Item(8).Visible = False
            DateSeriesEff3.Visible = False
            DateSeriesEff4.Visible = False
            DateSeriesEff5.Visible = False
            ChartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside
            ChartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Center
            ChartControl1.Legend.Margins.All = 5
            ChartControl1.ToolTipOptions.ShowForPoints = False

            LineSeriesEff.Visible = False
            DateSeriesEff.Visible = False
            CmpSeriesEff.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True


            With DirectCast(ChartControl1.Diagram, DevExpress.XtraCharts.XYDiagram)
                .AxisY.Label.TextPattern = "{V:P0}"
            End With


            'Dim label As SideBySideBarSeriesLabel = TryCast(CmpSeries.Label, SideBySideBarSeriesLabel)
            'label.Position = BarSeriesLabelPosition.Top

            If ChartControl1.Titles.Count > 1 Then
                ChartControl1.Titles(0).Visibility = DevExpress.Utils.DefaultBoolean.True
                ChartControl1.Titles(1).Visibility = DevExpress.Utils.DefaultBoolean.False
                ChartControl1.Titles(2).Visibility = DevExpress.Utils.DefaultBoolean.False
                'ChartControl1.Titles(3).Visibility = DevExpress.Utils.DefaultBoolean.False
                'ChartControl1.Titles(4).Visibility = DevExpress.Utils.DefaultBoolean.False
                'ChartControl1.Titles(5).Visibility = DevExpress.Utils.DefaultBoolean.False
            End If
        End If
        ChartControl1.Animate()
    End Sub



#End Region




End Class
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraCharts
Imports System.Windows.Forms
Imports System
Imports System.Drawing
Imports DevExpress.Utils
Imports System.Windows.Media


Public Class wDashboardProdOutputEff

    Private dt1 As DataTable
    Private dt1show As DataTable
    Private dteffallsumfac As DataTable
    Private dteffbyline As DataTable
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()



    End Sub


    Private Sub LoadCompany()
        Dim _Str As String

        _Str = " SELECT top 1  '1' AS FTSelect "
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

        Dim _StartDate As String = Microsoft.VisualBasic.Right(FDDate.Text, 4) + "/" + Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(FDDate.Text, 7), 2) + "/" + Microsoft.VisualBasic.Left(FDDate.Text, 2)
        Dim _EndDate As String = Microsoft.VisualBasic.Right(FDDateTo.Text, 4) + "/" + Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(FDDateTo.Text, 7), 2) + "/" + Microsoft.VisualBasic.Left(FDDateTo.Text, 2)
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

                For Each R As DataRow In _dtcmp.Select("FTSelect='1'", "FTIPServer asc")

                    _CmpCode = R!FTCmpCode.ToString

                    If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then
                        Ridx = Ridx + 1

                        If _ServerName = R!FTIPServer.ToString Then
                            Continue For
                        End If
                        _ServerName = R!FTIPServer.ToString
                        _UID = HI.Conn.DB.UIDName
                        _PWS = HI.Conn.DB.PWDName
                        _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                        _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                        _Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")
                        Try


                            '_Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT_DASH '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            'dt1 = GetSewFGData(_Qry, _ConnectString)
                            'If Ridx = 1 Then
                            '    dt1show = dt1.Clone
                            'End If

                            'dt1show.Merge(dt1)
                            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT_DASH_FAC '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            dt1 = GetSewFGData(_Qry, _ConnectString)
                            If Ridx = 1 Then
                                dteffallsumfac = dt1.Clone
                            End If
                            dteffallsumfac.Merge(dt1)

                            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT_DASH_BYLine '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            dt1 = GetSewFGData(_Qry, _ConnectString)
                            If Ridx = 1 Then
                                dteffbyline = dt1.Clone
                            End If
                            dteffbyline.Merge(dt1)


                            'If CheckEdit1.Checked Then
                            '    _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER_RATIO '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            '    dt2 = GetSewFGData(_Qry, _ConnectString)

                            '    If Ridx = 1 Then
                            '        dt2show = dt2.Clone
                            '    End If

                            '    dt2show.Merge(dt2)
                            'End If




                            'If CheckEdit2.Checked Then

                            '    _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            '    dt3 = GetSewFGData(_Qry, _ConnectString)

                            '    If Ridx = 1 Then
                            '        dt3show = dt3.Clone
                            '    End If

                            '    dt3show.Merge(dt3)
                            'End If



                            'If CheckEdit3.Checked Then

                            '    _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER_RESIGN '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            '    dt4 = GetSewFGData(_Qry, _ConnectString)

                            '    If Ridx = 1 Then
                            '        dt4show = dt4.Clone
                            '    End If

                            '    dt4show.Merge(dt4)
                            'End If



                            'If CheckEdit4.Checked Then
                            '    _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_HR_MONEY '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            '    dt5 = GetSewFGData(_Qry, _ConnectString)

                            '    If Ridx = 1 Then
                            '        dt5show = dt5.Clone
                            '    End If

                            '    dt5show.Merge(dt5)
                            'End If



                            'If CheckEdit5.Checked Then
                            '    _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT_DEFECT '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                            '    dt6 = GetSewFGData(_Qry, _ConnectString)

                            '    If Ridx = 1 Then
                            '        dt6show = dt6.Clone
                            '    End If

                            '    dt6show.Merge(dt6)
                            'End If



                        Catch ex22 As Exception
                            ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                        End Try

                    End If

                Next

            End If
            _dtcmp.Dispose()

            initGrid(dteffallsumfac)
            ogc1.DataSource = dteffallsumfac
            'Me.ogv1.BestFitColumns()



            'ogc2.DataSource = dt2show
            'Me.ogv2.BestFitColumns()

            'ogc3.DataSource = dt3show
            'Me.ogv3.BestFitColumns()

            'ogc4.DataSource = dt4show
            'Me.ogv4.BestFitColumns()

            'ogc5.DataSource = dt5show
            'Me.ogv5.BestFitColumns()


            'ogc6.DataSource = dt6show
            'Me.ogv6.BestFitColumns()


            'pivotGridControl.DataSource = dt1show
            ''LoadChart(dt1show)

            'dt1show = modifyData(dt1show)



            'InitDataChart()
            'InitDataChartEff()

        Catch ex As Exception
        End Try

        _Spls.Close()


    End Sub

    Private Sub initGrid(dt As DataTable)
        Try

            Dim AdvBandedGridView1 As New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView



            With AdvBandedGridView1
                .BeginInit()

                .GridControl = Me.ogc1
                .Name = "AdvBandedGridView1"
                .OptionsBehavior.ReadOnly = True
                .OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.[False]
                .OptionsView.ShowGroupPanel = False
                AddHandler .RowCellStyle, AddressOf AdvBandedGridView1_RowCellStyle


                For Each c As DataColumn In dt.Columns
                    Select Case c.ColumnName.ToUpper
                        Case "FTCmpCode".ToUpper


                            Dim ColumsBrand As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                            With ColumsBrand
                                .Caption = " "
                                .Name = "c" & c.ColumnName
                                .FieldName = c.ColumnName
                                .Visible = True
                                .OptionsColumn.AllowEdit = False

                                .Width = 100
                            End With
                            Dim bandh1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            With bandh1
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = " Eff "
                                .Columns.Add(ColumsBrand)
                                .Name = "bandh1" & c.ColumnName
                                '.VisibleIndex = 1
                                .Width = 100
                            End With

                            .Columns.Add(ColumsBrand)
                            .Bands.Add(bandh1)

                        Case Else
                            If IsDate(c.ColumnName) Then
                                Dim ColumsBrand As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                                With ColumsBrand
                                    .Caption = " "
                                    .Name = "c" & Replace(c.ColumnName, "/", "")
                                    .FieldName = c.ColumnName
                                    .Visible = True
                                    '.OptionsColumn.AllowEdit = False
                                    .DisplayFormat.FormatType = FormatType.Numeric
                                    .DisplayFormat.FormatString = "P2"
                                    .ColumnEdit = Me.RepoCals
                                    .Width = 89
                                End With
                                Dim bandhChild1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                With bandhChild1
                                    .Caption = HI.UL.ULDate.ConvertEN(c.ColumnName.ToString())
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Columns.Add(ColumsBrand)
                                    .Name = "bandhChild1" & Replace(c.ColumnName, "/", "")
                                    .VisibleIndex = 0
                                    .Width = 89
                                End With
                                Dim bandh1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                With bandh1
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = Format(CDate(c.ColumnName.ToString()), "ddd")
                                    .Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {bandhChild1})
                                    .Name = "bandh1" & Replace(c.ColumnName, "/", "")
                                    '.VisibleIndex = 1
                                    .Width = 89
                                End With


                                .Columns.Add(ColumsBrand)
                                .Bands.Add(bandh1)
                            End If

                    End Select


                Next
                .EndInit()
            End With
            Me.ogc1.MainView = AdvBandedGridView1
        Catch ex As Exception

        End Try
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
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function





#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Call LoadCompany()
            'AddHandler ChartControl.ObjectSelected, AddressOf OnChartControlObjectSelected
            'AddHandler ChartControl1.ObjectSelected, AddressOf OnChartControlObjectSelectedEff
            Me.FTCaption_lbl.Text = "ALL Branch"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then

            Call LoadData()
            'InitCmpChart()
            'InitCmpChartEff()
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

    Private Sub AdvBandedGridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs)
        Try
            Dim view As GridView = TryCast(sender, GridView)
            'Dim _mark As Boolean = CBool(view.GetRowCellValue(e.RowHandle, "Mark"))

            If e.Column.FieldName <> "FTCmpCode" Then
                Dim _length As Double = CDbl(e.CellValue)

                'e.Appearance.GradientMode = True

                Select Case True
                    Case _length > 0.0 And _length < 0.5
                        e.Appearance.BackColor = Color.FromArgb(222, 151, 175)
                        e.Appearance.BackColor2 = Color.FromArgb(245, 212, 95)
                    Case _length >= 0.5 And _length < 0.9
                        e.Appearance.BackColor = Color.FromArgb(245, 212, 95)
                        e.Appearance.BackColor2 = Color.FromArgb(245, 212, 95)
                    Case _length >= 0.9
                        e.Appearance.BackColor = Color.FromArgb(245, 212, 95)
                        e.Appearance.BackColor2 = Color.FromArgb(95, 245, 240)
                    Case Else
                        e.Appearance.BackColor = Color.LightGray
                        e.Appearance.BackColor2 = Color.LightGray
                End Select
            End If


        Catch ex As Exception
            e.Appearance.BackColor = Color.LightGray
            e.Appearance.BackColor2 = Color.LightGray
        End Try
    End Sub

    Private Sub RepoCals_DoubleClick(sender As Object, e As EventArgs) Handles RepoCals.DoubleClick
        Try
            Dim x As String = ""
            With DirectCast(DirectCast(DirectCast(sender, DevExpress.XtraEditors.CalcEdit).Parent, DevExpress.XtraGrid.GridControl).MainView, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                If .RowCount < 0 Then Exit Sub

                Dim cmpcode As String = .GetRowCellValue(.FocusedRowHandle, "FTCmpCode")

                Me.ogc1.DataSource = dteffbyline.Select("FTCmpCodeFill='" & cmpcode & "'").CopyToDataTable
                Me.FTCaption_lbl.Text = "" & cmpcode.ToString
                Me.SimpleButton1.Visible = True
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.ogc1.DataSource = dteffallsumfac
        Me.FTCaption_lbl.Text = "ALL Branch"
        Me.SimpleButton1.Visible = False
    End Sub

End Class
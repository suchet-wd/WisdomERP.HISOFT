Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports System.ComponentModel

Public Class UQCFabricSummay
    Private RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
    Sub New(DataDate As String, DataSupl As String, DataMatID As String, dtsummary As DataTable, Optional GridOrg As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.MatID = DataSupl & DataMatID
        InitGridView()

        Call CreateGridColumn()
        Call GenerateGridBand(DataDate, dtsummary, GridOrg)

        chartControl.Titles.Item(0).Text = "FABRIC INSPECTION REPORT OF ITEM " & DataMatID & "  (" & DataSupl & ")"
        Dim _Qry As String = ""

        'pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        'pivotGridControl.OptionsChartDataSource.SelectionOnly = False
        'pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
        'pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = False

        'chartControl.CrosshairOptions.ShowArgumentLine = False


        'Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        'For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
        '    If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
        '        Continue For
        '    End If
        '    comboChartType.Properties.Items.Add(type)
        'Next type
        'comboChartType.SelectedItem = ViewType.Bar
        'chartControl.DataSource = pivotGridControl

        HI.TL.HandlerControl.AddHandlerObj(Me)
        'loadObjLang()


        RepositoryItemFTSelect.AutoHeight = False
        RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        RepositoryItemFTSelect.ReadOnly = True
        RepositoryItemFTSelect.ValueChecked = "1"
        RepositoryItemFTSelect.ValueUnchecked = "0"
    End Sub

#Region "Procedure"



    Private _MatID As String = ""
    Private Property MatID As String
        Get
            Return _MatID
        End Get
        Set(value As String)
            _MatID = value
        End Set
    End Property



    Private Sub CreateGridColumn(Optional GridOrg As Boolean = False)

        Dim cmd As String = ""

        Dim dtgrpfabDefect As DataTable



        cmd = " Select   FNHSysQCFabricDetailId, FNQCFabricType, FTQCFabricDetailCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            cmd &= vbCrLf & "  , FTQCFabricDetailNameTH AS FTQCFabricDetailName "
        Else
            cmd &= vbCrLf & "  , FTQCFabricDetailNameEN  AS FTQCFabricDetailName"
        End If
        cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMQCFabric "
        cmd &= vbCrLf & "     Where (FTStateActive ='1') "

        dtgrpfabDefect = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SYSTEM)


        With Me.ogvdetail
            .Columns.Clear()


            For Each Str As String In "FTQCDate|FTStyleCode|FTPurchaseNo|FTItemRef|FTBatchNo|FTColorCode|FTRollNo|FNActQuantity|FTSupplier|FTQCBy|FNTotalPoint|FNCalPer|FTStateReject".Split("|")
                Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                With _BanCol
                    .Caption = Str
                    .FieldName = Str
                    .Name = MatID.ToString & "_" & Str
                    .OptionsColumn.AllowEdit = False
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                    .OptionsColumn.ReadOnly = True
                    .Visible = True

                    Select Case Str
                        Case "FNPointPerYard", "FNTotalPoint"
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "{0:n2}"
                        Case "FNActQuantity"
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "{0:n4}"
                            'Case "FTStateReject"
                            '    .ColumnEdit = RepositoryItemFTSelect
                    End Select

                    .Width = 80

                End With

                .Columns.Add(_BanCol)
            Next

            If GridOrg = False Then
                For Each R As DataRow In dtgrpfabDefect.Rows

                    Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                    With _BanCol
                        .Caption = R!FTQCFabricDetailName.ToString
                        .FieldName = R!FNHSysQCFabricDetailId.ToString
                        .Name = MatID.ToString & "_" & R!FNHSysQCFabricDetailId.ToString
                        .OptionsColumn.AllowEdit = False
                        .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                        .OptionsColumn.ReadOnly = True
                        .Visible = True
                        .Width = 45

                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .DisplayFormat.FormatString = "{0:n0}"

                    End With

                    .Columns.Add(_BanCol)

                Next
            End If


        End With


    End Sub

    Private _DtObject As DataTable
    Private Function GetObjLang(ByVal pFormName As String, ByVal pObjectName As String, ByVal _ObjTag As String) As String
        Dim tSql As String = Nothing
        Dim _Tag As String = "1"

        If Not String.IsNullOrEmpty(_ObjTag) Then
            _Tag = _ObjTag
        End If

        If _DtObject Is Nothing Then
            tSql = "SELECT '|'  + ISNULL(FTLangEN,'')  +'|'+ ISNULL(FTLangTH,'') + '|'+ ISNULL(FTLangVT,'') +'|'+ ISNULL(FTLangKM,'') +'|'+ ISNULL(FTLangBM,'') + '|'+ ISNULL(FTLangLAO,'') +'|'+ ISNULL(FTLangCH,'') AS LangT,FTObjectName "
            tSql += Constants.vbCrLf & " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & ".dbo.HSysLanguage WITH(Nolock) "
            tSql += Constants.vbCrLf & " WHERE  FTFormName='" + HI.UL.ULF.rpQuoted(pFormName) & "' "
            _DtObject = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_LANG)
        End If

        Try

            For Each R As DataRow In _DtObject.[Select](" FTObjectName='" & HI.UL.ULF.rpQuoted(pObjectName.Trim()) & "'")
                Return _Tag & (R("LangT")).ToString()
            Next

            Return ""
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return ""
    End Function


    Private Function loadObjLang() As String
        Dim tSql As String = Nothing
        Dim _Tag As String = "1"



        If _DtObject Is Nothing Then
            tSql = "SELECT '|'  + ISNULL(FTLangEN,'')  +'|'+ ISNULL(FTLangTH,'') + '|'+ ISNULL(FTLangVT,'') +'|'+ ISNULL(FTLangKM,'') +'|'+ ISNULL(FTLangBM,'') + '|'+ ISNULL(FTLangLAO,'') +'|'+ ISNULL(FTLangCH,'') AS LangT,FTObjectName "
            tSql += Constants.vbCrLf & " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & ".dbo.HSysLanguage WITH(Nolock) "
            tSql += Constants.vbCrLf & " WHERE  FTFormName='" + HI.UL.ULF.rpQuoted(Me.Name) & "' "
            _DtObject = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_LANG)
        End If
        Return ""
    End Function

    Private Sub GenerateGridBand(DataDate As String, dtsummary As DataTable, Optional GridOrg As Boolean = False)
        Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0

            Dim TotalDefectPer As Decimal = 0.00
            Dim TotalQtyCheck As Decimal = 0.00
            Dim TotalQtyYear As Decimal = 0.00
            Dim TotalPoint As Decimal = 0.00

            If Not (dtsummary Is Nothing) Then

                Try
                    For Each Rm As DataRow In dtsummary.Rows
                        TotalPoint = TotalPoint + Val(Rm!FNTotalPoint.ToString)
                        TotalQtyYear = TotalQtyYear + Val(Rm!FNActQuantity.ToString)

                        If Rm!FTState.ToString = "1" Then
                            TotalQtyCheck = TotalQtyCheck + Val(Rm!FNActQuantity.ToString)
                        End If
                    Next
                    If TotalQtyYear > 0 Then
                        TotalDefectPer = CDbl(Format((TotalPoint / TotalQtyYear) * 100.0, "0.00"))
                    End If


                Catch ex As Exception

                End Try

            End If


            Dim cmd As String = ""
            Dim dtgrpfab As DataTable
            Dim dtgrpfabDefect As DataTable
            Dim dtgrpfabChart As DataTable
            Dim dtgrpfabDefectDetail As DataTable

            cmd = " Select   FNListIndex AS  FNListIndex "

            cmd &= vbCrLf & "  , Convert(varchar(1),FNListIndex+1) + '. '  + FTNameEN + ' (' + FTNameTH + ')' AS  FTFabricGrpName"

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    cmd &= vbCrLf & "  , FTNameTH AS FTFabricGrpName "
            'Else
            '    cmd &= vbCrLf & "  , FTNameEN  AS FTFabricGrpName"
            'End If
            cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
            cmd &= vbCrLf & "     Where (FTListName = N'FNQCFabricType') "
            cmd &= vbCrLf & "  Order By FNListIndex "
            dtgrpfab = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SYSTEM)
            dtgrpfabChart = dtgrpfab.Clone
            dtgrpfabChart.Columns.Add("Type", GetType(String))
            dtgrpfabChart.Columns.Add("DataDefect", GetType(Double))
            dtgrpfabChart.Columns.Add("FNGrpType", GetType(Integer))



            dtgrpfabDefectDetail = New DataTable
            dtgrpfabDefectDetail.Columns.Add("Region", GetType(String))
            dtgrpfabDefectDetail.Columns.Add("Value1", GetType(Double))
            dtgrpfabDefectDetail.Columns.Add("Value2", GetType(Double))

            cmd = " Select   FNHSysQCFabricDetailId, FNQCFabricType, FTQCFabricDetailCode "
            ' cmd &= vbCrLf & "  , FTQCFabricDetailNameEN  + ' (' +FTQCFabricDetailNameTH + ')' AS FTQCFabricDetailName"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & "  ,FTQCFabricDetailNameTH   AS FTQCFabricDetailName "
            Else
                cmd &= vbCrLf & "  , FTQCFabricDetailNameEN  AS FTQCFabricDetailName"
            End If
            cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMQCFabric "
            cmd &= vbCrLf & "     Where (FTStateActive ='1') "

            dtgrpfabDefect = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SYSTEM)


            With Me.ogvdetail
                .BeginInit()

                Dim _Mailband00 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                With _Mailband00
                    .OptionsBand.ShowInCustomizationForm = False
                    .OptionsBand.AllowMove = False
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    .Caption = "  "
                    .Name = "MMmaingb" & MatID.ToString & "_00"
                    .RowCount = 2
                    .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                End With

                .Bands.Add(_Mailband00)

                Dim _Mailband01 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                With _Mailband01
                    .OptionsBand.ShowInCustomizationForm = False
                    .OptionsBand.AllowMove = False
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    .Caption = "Cumulative Defect  " & TotalDefectPer.ToString & "%"
                    .Name = "MMmaingb" & MatID.ToString & "_01"
                    .RowCount = 1
                    .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                End With

                _Mailband00.Children.Add(_Mailband01)

                Dim _Mailband001 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                With _Mailband001
                    .OptionsBand.ShowInCustomizationForm = False
                    .OptionsBand.AllowMove = False
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    .Caption = " " & DataDate.ToString
                    .Name = "MMmaingb" & MatID.ToString & "_001"
                    .RowCount = 2
                    .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                End With

                _Mailband01.Children.Add(_Mailband001)


                Dim _Mailband02 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                With _Mailband02
                    .OptionsBand.ShowInCustomizationForm = False
                    .OptionsBand.AllowMove = False
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    .Caption = " " & TotalQtyCheck.ToString
                    .Name = "MMmaingb" & MatID.ToString & "_02"
                    .RowCount = 1
                    .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                End With

                _Mailband001.Children.Add(_Mailband02)

                Dim _Mailband03 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                With _Mailband03
                    .OptionsBand.ShowInCustomizationForm = False
                    .OptionsBand.AllowMove = False
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    .Caption = "Total Quantity   " & TotalQtyYear.ToString
                    .Name = "MMmaingb" & MatID.ToString & "_02"
                    .RowCount = 3
                    .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                End With

                _Mailband02.Children.Add(_Mailband03)

                For Each Str As String In "FTQCDate|FTStyleCode|FTPurchaseNo|FTItemRef|FTBatchNo|FTColorCode|FTRollNo|FNActQuantity|FTStateReject".Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand

                        .OptionsBand.ShowInCustomizationForm = False
                        .OptionsBand.AllowMove = False
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        Dim atTag As String() = Nothing
                        atTag = GetObjLang(Me.Name, Str, Str).Split("|")
                        .Caption = atTag(HI.ST.Lang.Language)
                        .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Str))
                        .Name = "maingb" & MatID.ToString & "_" & Str
                        .RowCount = 8
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                    End With

                    _Mailband03.Children.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1

                Next

                Dim _GrbandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                With _GrbandType
                    .OptionsBand.ShowInCustomizationForm = False
                    .OptionsBand.AllowMove = False
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .Caption = "ตำหนิ (Defect)"
                    .Name = "gbt" & MatID.ToString & "_MainDefect"
                    .RowCount = 1
                End With

                .Bands.Add(_GrbandType)

                If GridOrg = False Then

                    For Each R As DataRow In dtgrpfab.Select("FNListIndex<>-1", "FNListIndex")

                        Dim PointTypeCheck As Decimal = 0
                        Dim PointTypeYear As Decimal = 0
                        Dim PcrTypeCheck As Decimal = 0
                        Dim PcrTypeYear As Decimal = 0

                        Dim _BandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        With _BandType
                            .OptionsBand.ShowInCustomizationForm = False
                            .OptionsBand.AllowMove = False
                            .AppearanceHeader.Options.UseTextOptions = True
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .Caption = R!FTFabricGrpName.ToString
                            .Name = "gbdt" & MatID.ToString & "_MainDefect" & R!FNListIndex.ToString
                            .RowCount = 1
                        End With

                        _GrbandType.Children.Add(_BandType)

                        For Each Rx As DataRow In dtgrpfabDefect.Select("FNQCFabricType=" & Val(R!FNListIndex.ToString) & "", "")


                            Dim PointCheck As Decimal = 0
                            Dim PointYear As Decimal = 0
                            Dim PcrCheck As Decimal = 0
                            Dim PcrYear As Decimal = 0

                            If Not (dtsummary Is Nothing) Then

                                Try
                                    For Each Rm As DataRow In dtsummary.Rows

                                        Try
                                            PointYear = PointYear + Val(Rm.Item(Rx!FNHSysQCFabricDetailId.ToString).ToString())
                                        Catch ex As Exception

                                        End Try


                                        If Rm!FTState.ToString = "1" Then
                                            Try
                                                PointCheck = PointCheck + Val(Rm.Item(Rx!FNHSysQCFabricDetailId.ToString).ToString())
                                            Catch ex As Exception

                                            End Try

                                        End If
                                    Next

                                    If TotalQtyCheck > 0 Then
                                        PcrCheck = CDbl(Format((PointCheck / TotalQtyCheck) * 100.0, "0.00"))
                                    End If

                                    If TotalQtyYear > 0 Then
                                        PcrYear = CDbl(Format((PointYear / TotalQtyYear) * 100.0, "0.00"))
                                    End If

                                Catch ex As Exception
                                End Try

                                PointTypeCheck = PointTypeCheck + PointCheck
                                PointTypeYear = PointTypeYear + PointYear
                            End If

                            Dim _GrbandCol01 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol02 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol03 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol04 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol05 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol06 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol07 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                            With _GrbandCol01
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.ForeColor = Color.Blue
                                .AppearanceHeader.BackColor = Color.LightCyan
                                .Caption = "" & PcrYear.ToString & "%"
                                .Name = "gxpcol01" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _BandType.Children.Add(_GrbandCol01)


                            With _GrbandCol02
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.ForeColor = Color.Blue
                                .AppearanceHeader.BackColor = Color.LightCyan
                                .Caption = "" & PcrCheck.ToString & "%"
                                .Name = "gxpcol02" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _GrbandCol01.Children.Add(_GrbandCol02)


                            With _GrbandCol03
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.ForeColor = Color.Blue
                                .AppearanceHeader.BackColor = Color.LightCyan
                                .Caption = " " & PointCheck.ToString()
                                .Name = "gxpcol03" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _GrbandCol02.Children.Add(_GrbandCol03)

                            With _GrbandCol04
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.ForeColor = Color.Blue
                                .AppearanceHeader.BackColor = Color.LightCyan
                                .Caption = "" & TotalQtyCheck.ToString()
                                .Name = "gxpcol04" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _GrbandCol03.Children.Add(_GrbandCol04)

                            With _GrbandCol05
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.ForeColor = Color.Blue
                                .AppearanceHeader.BackColor = Color.LightCyan
                                .Caption = "" & PcrYear.ToString & "%"
                                .Name = "gxpcol05" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _GrbandCol04.Children.Add(_GrbandCol05)

                            With _GrbandCol06
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.ForeColor = Color.Blue
                                .AppearanceHeader.BackColor = Color.LightCyan
                                .Caption = "" & PointYear.ToString
                                .Name = "gxpcol06" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _GrbandCol05.Children.Add(_GrbandCol06)

                            With _GrbandCol07
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.ForeColor = Color.Blue
                                .AppearanceHeader.BackColor = Color.LightCyan
                                .Caption = "" & TotalQtyYear.ToString()
                                .Name = "gxpcol07" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _GrbandCol06.Children.Add(_GrbandCol07)

                            Dim _GrbandCol1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                            With _GrbandCol1
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = Rx!FTQCFabricDetailCode.ToString
                                .Name = "gbcol1" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 1
                                .Width = 45
                            End With

                            _GrbandCol07.Children.Add(_GrbandCol1)

                            With _GrbandCol2
                                .OptionsBand.ShowInCustomizationForm = False
                                .OptionsBand.AllowMove = False
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceHeader.TextOptions.WordWrap = True
                                .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Rx!FNHSysQCFabricDetailId.ToString))
                                .Caption = Rx!FTQCFabricDetailName.ToString
                                .Name = "gbcol2" & MatID.ToString & "_" & Rx!FNHSysQCFabricDetailId.ToString
                                .RowCount = 6
                                .Width = 45

                            End With

                            _GrbandCol1.Children.Add(_GrbandCol2)

                        Next



                        If Not (dtsummary Is Nothing) Then

                            PcrTypeCheck = 0.0
                            PcrTypeYear = 0.0

                            If TotalQtyCheck > 0 Then
                                PcrTypeCheck = CDbl(Format((PointTypeCheck / TotalQtyCheck) * 100.0, "0.00"))
                            End If

                            If TotalQtyYear > 0 Then
                                PcrTypeYear = CDbl(Format((PointTypeYear / TotalQtyYear) * 100.0, "0.00"))
                            End If

                            dtgrpfabChart.Rows.Add(R!FNListIndex, R!FTFabricGrpName.ToString, "Cumulative Defect", PcrTypeYear, 2)
                            dtgrpfabChart.Rows.Add(R!FNListIndex, R!FTFabricGrpName.ToString, DataDate, PcrTypeCheck, 1)

                            Try
                                dtgrpfabDefectDetail.Rows.Add(R!FTFabricGrpName.ToString, PcrTypeCheck, PcrTypeYear)
                            Catch ex As Exception

                            End Try

                        End If



                    Next

                End If


                For Each Str As String In "FNTotalPoint|FNCalPer|FTQCBy|FTSupplier".Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                    With _gBand
                        .OptionsBand.ShowInCustomizationForm = False
                        .OptionsBand.AllowMove = False
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                        Dim atTag As String() = Nothing
                        atTag = GetObjLang(Me.Name, Str, Str).Split("|")
                        .Caption = atTag(HI.ST.Lang.Language)
                        .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Str))
                        .Name = "maingb" & MatID.ToString & "_" & Str
                        .RowCount = 8

                    End With

                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next

                .EndInit()
            End With

            pivotGridControl.DataSource = dtgrpfabChart
            pivotGridControl.RefreshData()
            '  chartControl.RefreshData()



            Try
                ' Dim ochartSubDefect As New DevExpress.XtraCharts.ChartControl
                ' Create the first side-by-side bar series and add points to it.
                Dim chart As ChartControl = New ChartControl
                chart.DataSource = dtgrpfabDefectDetail

                Dim series1 As New Series(DataDate, ViewType.Bar)
                series1.ArgumentDataMember = "Region"
                series1.ValueDataMembers.AddRange("Value1")


                Dim series2 As New Series("Cumulative Defect", ViewType.Bar)
                series2.ArgumentDataMember = "Region"
                series2.ValueDataMembers.AddRange("Value2")

                ' Add the series to the chart.

                chart.Series.Clear()
                chart.Series.AddRange(New Series() {series1, series2})
                series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
                series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
                series1.Label.PointOptions.ValueNumericOptions.Precision = 2


                ogbchart.Controls.Add(chart)
                chart.Dock = DockStyle.Fill

                ' Add a title to the chart (if necessary).
                'Dim chartTitle1 As New ChartTitle()
                'chartTitle1.Text = MatID.ToString
                'chartControl.Titles.Add(chartTitle1)


                'ochartSubDefect.Titles.Add(chartTitle1)
                'ochartSubDefect.Dock = DockStyle.Fill
                'Me.ogrpSubChart.Controls.Clear()
                'Me.ogrpSubChart.Controls.Add(ochartSubDefect)

            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try


    End Sub

    Private Sub InitGridView()

        ' AddHandler ogvdetail.CustomDrawColumnHeader, AddressOf ogvdetail_CustomDrawColumnHeader
    End Sub





    Private Sub ogvdetail_CustomDrawBandHeader(sender As Object, e As Views.BandedGrid.BandHeaderCustomDrawEventArgs) Handles ogvdetail.CustomDrawBandHeader
        Try
            Select Case Microsoft.VisualBasic.Left(e.Band.Name, 6)
                Case "maingb", "gbcol2"
                    DrawBandVertical(e)
                Case "gxpcol"
                    e.Band.AppearanceHeader.BackColor = Color.LightCyan
                    e.Band.AppearanceHeader.ForeColor = Color.Blue
                Case Else
                    Return
            End Select
        Catch ex As Exception

        End Try


    End Sub

    Private Shared stringFormat As New StringFormat() With {
            .Trimming = StringTrimming.EllipsisCharacter,
            .FormatFlags = StringFormatFlags.NoWrap
        }

    Private Sub ogvdetail_CustomDrawColumnHeader(sender As Object, e As ColumnHeaderCustomDrawEventArgs)
        If e.Column IsNot Nothing AndAlso Not e.Column.Visible Then
            Return
        End If
        DrawVertical(e)
    End Sub

    Private Shared Sub DrawVertical(ByVal e As DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs)
        e.Info.Caption = String.Empty
        e.Painter.DrawObject(e.Info)

        If e.Column IsNot Nothing Then
            e.Cache.DrawVString(e.Column.GetTextCaption(), e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Info.CaptionRect, stringFormat, -90)
        End If

        e.Handled = True
    End Sub

    Private Shared Sub DrawBandVertical(ByVal e As Views.BandedGrid.BandHeaderCustomDrawEventArgs)
        e.Info.Caption = String.Empty
        e.Painter.DrawObject(e.Info)

        ' If e.Column IsNot Nothing Then
        e.Cache.DrawVString(e.Band.GetTextCaption(), e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Info.CaptionRect, stringFormat, 90)

        ' End If
        e.Handled = True
    End Sub

    Private Sub comboBoxEdit2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboChartType.SelectedIndexChanged
        'chartControl.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))
        ''If chartControl.SeriesTemplate.Label IsNot Nothing Then
        ''    chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        ''    chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
        ''    checkShowPointLabels.Enabled = True
        ''Else
        ''    checkShowPointLabels.Enabled = False
        ''End If
        'If (TryCast(chartControl.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
        '    chartControl.Legend.Visible = True
        'End If
        'If TypeOf chartControl.Diagram Is Diagram3D Then
        '    Dim diagram As Diagram3D = CType(chartControl.Diagram, Diagram3D)
        '    diagram.RuntimeRotation = True
        '    diagram.RuntimeZooming = True
        '    diagram.RuntimeScrolling = True
        'End If
        'For Each series As Series In chartControl.Series
        '    Dim supportTransparency As ISupportTransparency = TryCast(series.View, ISupportTransparency)
        '    If supportTransparency IsNot Nothing Then
        '        If (TypeOf series.View Is AreaSeriesView) OrElse (TypeOf series.View Is Area3DSeriesView) OrElse (TypeOf series.View Is RadarAreaSeriesView) OrElse (TypeOf series.View Is Bar3DSeriesView) Then
        '            supportTransparency.Transparency = 135
        '        Else
        '            supportTransparency.Transparency = 0
        '        End If
        '    End If
        'Next series
    End Sub


#End Region



End Class

Public Class DataPoint
    Public Property Region As String
    Public Property Value1 As Double
    Public Property Value2 As Double

    Public Sub New(ByVal region As String, ByVal value1 As Double, ByVal value2 As Double)
        Me.Region = region
        Me.Value1 = value1
        Me.Value2 = value2
    End Sub

    Public Shared Function GetDataPoints() As BindingList(Of DataPoint)
        Dim data As BindingList(Of DataPoint) = New BindingList(Of DataPoint) From {
                New DataPoint("Asia", 4.7685, 5.289),
                New DataPoint("Australia", 1.9576, 2.2727),
                New DataPoint("Europe", 3.0884, 3.7257),
                New DataPoint("North America", 3.7477, 4.1825),
                New DataPoint("South America", 1.8945, 2.1172)
            }
        Return data
    End Function
End Class
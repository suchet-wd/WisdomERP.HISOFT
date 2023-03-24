Imports System.Windows.Forms
Public Class UITakeHomePayRawData

    Sub New(odt As DataTable, _UnitSectID As Integer, _UnitSectName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.UnitID = _UnitSectID
        Me.UnitName = _UnitSectName
        Me.EmpData = odt

        HI.TL.HandlerControl.AddHandlerObj(Me)

        Call CreateDatatable(odt)
        Call GenerateGridBand(_UnitSectID)
    End Sub

#Region "Procedure"

    Private _UnitID As Integer = 0
    Public Property UnitID As Integer
        Get
            Return _UnitID
        End Get
        Set(value As Integer)
            _UnitID = value
        End Set
    End Property


    Private _UnitName As String = ""
    Public Property UnitName As String
        Get
            Return _UnitName
        End Get
        Set(value As String)
            _UnitName = value
        End Set
    End Property

    Private _EmpData As DataTable = Nothing
    Public Property EmpData As DataTable
        Get
            Return _EmpData
        End Get
        Set(value As DataTable)
            _EmpData = value
        End Set
    End Property

    Private _GridData As DataTable = Nothing
    Private Property GridData As DataTable
        Get
            Return _GridData
        End Get
        Set(value As DataTable)
            _GridData = value
        End Set
    End Property

    Public Sub RefreshData()
        Call CreateDatatable(Me.EmpData)
        Call GenerateGridBand(Me.UnitID)
    End Sub

    Private Sub CreateDatatable(dt As DataTable)
       


        With Me.ogvdetail
            .BeginInit()
            .Columns.Clear()
            .Bands.Clear()

            For Each Col As DataColumn In dt.Columns

                Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                With _BanCol
                    .Caption = Col.ColumnName.ToString
                    .FieldName = Col.ColumnName.ToString
                    .Name = Col.ColumnName.ToString
                    .OptionsColumn.AllowEdit = False
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                    .OptionsColumn.ReadOnly = True
                    .Visible = True

                    Select Case Col.ColumnName.ToString

                        Case "FNRowSeq", "FNShowSeq", "FNRowSeq2"
                            .Width = 0
                            .Visible = False
                        Case Else

                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "C" Then
                                .Width = 50
                            Else

                                If Col.ColumnName.ToString = "FTDescription" Then
                                    .Width = 250
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                                Else
                                    .Width = 70
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                End If

                            End If

                    End Select
                End With

                .Columns.Add(_BanCol)

            Next
            .EndInit()
        End With

        Me.GridData = dt
    End Sub

    Private Sub GenerateGridBand(_UnitID As Integer)
        Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0
            With Me.ogvdetail
                .BeginInit()

                'For Each Str As String In "FTDescription|FNRowSeq|FNShowSeq|FNRowSeq2".Split("|")
                For Each Str As String In "FTDescription|FNRowSeq".Split("|")
                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Caption = "Description"
                        .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Str))
                        .Name = "gb" & _UnitID.ToString & Str
                        .RowCount = 4
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                    End With
                    .Bands.Add(_gBand)

                    If Str = "FNRowSeq" Then
                        .Bands("gb" & _UnitID.ToString & Str).Visible = False
                    End If

                    _GbandIndex = _GbandIndex + 1
                Next
                Dim _dtY As New DataTable
                _dtY.Columns.Add("FTYear", GetType(String))
                _dtY.Columns.Add("FNRowSeq", GetType(Integer))

                Dim _dtYM As New DataTable
                _dtYM.Columns.Add("FTYear", GetType(String))
                _dtYM.Columns.Add("FTMonth", GetType(String))
                _dtYM.Columns.Add("FNRowSeq", GetType(Integer))


                Dim _dtD As New DataTable
                _dtD.Columns.Add("FTYear", GetType(String))
                _dtD.Columns.Add("FTMonth", GetType(String))
                _dtD.Columns.Add("FNRowSeq", GetType(Integer))
                _dtD.Columns.Add("FTDate", GetType(String))

                Dim _StrDate As String = ""
                Dim _StrDateEN As String = ""

                For Each Col As DataColumn In EmpData.Columns

                    Select Case Col.ColumnName.ToString
                        Case "FTDescription", "FNRowSeq", "FNShowSeq", "FNRowSeq2"
                        Case Else
                            If _dtY.Select("FTYear='" & Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 4) & "'").Length <= 0 Then
                                _dtY.Rows.Add(Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 4), _dtY.Rows.Count + 1)
                            End If

                            If _dtYM.Select("FTMonth='" & Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 6) & "'").Length <= 0 Then
                                _dtYM.Rows.Add(Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 4), Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 6), _dtYM.Rows.Count + 1)
                            End If

                            _dtD.Rows.Add(Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 4), Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 6), _dtD.Rows.Count + 1, Col.ColumnName.ToString)
                    End Select

                Next

                For Each Ry As DataRow In _dtY.Rows
                    Dim _GrbandYear As New DevExpress.XtraGrid.Views.BandedGrid.GridBand


                    With _GrbandYear
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Caption = Ry!FTYear.ToString
                        .Name = "gbty" & _UnitID.ToString & Ry!FTYear.ToString
                        .RowCount = 1

                    End With

                    .Bands.Add(_GrbandYear)
                    For Each Rmx As DataRow In _dtYM.Select("FTYear='" & Ry!FTYear.ToString & "'", "FNRowSeq")

                        Dim _GrbandYearMonth As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                        With _GrbandYearMonth
                            .AppearanceHeader.Options.UseTextOptions = True
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .Caption = HI.UL.ULDate.GetMonthNameEN(Microsoft.VisualBasic.Left(Rmx!FTMonth.ToString, 4) & "/" & Microsoft.VisualBasic.Right(Rmx!FTMonth.ToString, 2) & "/01") ' Microsoft.VisualBasic.Right(Rmx!FTMonth.ToString, 2)
                            .Name = "gbtym" & _UnitID.ToString & Rmx!FTMonth.ToString
                            .RowCount = 1
                        End With

                        _GrbandYear.Children.Add(_GrbandYearMonth)

                        For Each Rmd As DataRow In _dtD.Select("FTMonth='" & Rmx!FTMonth.ToString & "'", "FNRowSeq")

                            Dim _StrDataDate As String = Microsoft.VisualBasic.Left(Rmd!FTDate.ToString, 4) & "/" & Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(Rmd!FTDate.ToString, 6), 2) & "/" & Microsoft.VisualBasic.Right(Rmd!FTDate.ToString, 2)

                            Dim _DatePart As Integer = 0
                            Dim _DateName As String = ""
                            Try
                                _DatePart = Microsoft.VisualBasic.DateAndTime.DatePart(DateInterval.Weekday, CDate(_StrDataDate), FirstDayOfWeek.Sunday)
                            Catch ex As Exception

                            End Try

                            Select Case _DatePart
                                Case 0
                                    _DateName = "Sunday"
                                Case 1
                                    _DateName = "Sunday"
                                Case 2
                                    _DateName = "Monday"
                                Case 3
                                    _DateName = "Tueseday"
                                Case 4
                                    _DateName = "Wednesday"
                                Case 5
                                    _DateName = "Thursday"
                                Case 6
                                    _DateName = "Friday"
                                Case 7
                                    _DateName = "Saturday"
                                Case Else
                                    _DateName = "Sunday"
                            End Select

                            Dim _GrbandDayName As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            With _GrbandDayName
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = _DateName
                                .Name = "gbdayName" & _UnitID.ToString & Rmd!FTDate.ToString

                                .RowCount = 1
                                .Width = 70

                            End With
                            _GrbandYearMonth.Children.Add(_GrbandDayName)

                            Dim _GrbandDay As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            With _GrbandDay
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Rmd!FTDate.ToString))
                                .Caption = Microsoft.VisualBasic.Right(Rmd!FTDate.ToString, 2)
                                .Name = "gbday" & _UnitID.ToString & Rmd!FTDate.ToString

                                .RowCount = 1
                                .Width = 70

                            End With

                            _GrbandDayName.Children.Add(_GrbandDay)

                        Next

                    Next

                Next


                'For Each Col As DataColumn In EmpData.Columns
                '    Select Case Col.ColumnName.ToString
                '        Case "FTDescription", "FNRowSeq", "FNShowSeq", "FNRowSeq2"

                '        Case Else

                '            _StrDate = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 2) & "/" & Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 6), 2) & "/" & Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 4)
                '            _StrDateEN = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 4) & "/" & Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 6), 2) & "/" & Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 2)

                '            Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                '            With _gBand
                '                .AppearanceHeader.Options.UseTextOptions = True
                '                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '                .Caption = _StrDate
                '                .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Col.ColumnName))
                '                .Name = "gb" & _UnitID.ToString & Col.ColumnName
                '                .RowCount = 5

                '            End With
                '            .Bands.Add(_gBand)

                '            _GbandIndex = _GbandIndex + 1
                '    End Select
                'Next

                'If Not (EmpData Is Nothing) Then
                '    Dim grp As List(Of String) = (EmpData.Select("FTQATypeCode<>''", "FTQATypeCode").CopyToDataTable).AsEnumerable() _
                '                                      .Select(Function(r) r.Field(Of String)("FTQATypeCode")) _
                '                                      .Distinct() _
                '                                      .ToList()


                '    Dim _StateCreateBand As Boolean = False
                '    Dim _UnitSectCode As String = EmpData.Rows(0)!FTUnitSectCode.ToString

                '    Dim _FTQAData As String = EmpData.Rows(0)!FTQADate.ToString
                '    Dim _SubOrderNo As String = EmpData.Rows(0)!FTSubOrderNo.ToString

                '    For Each Ind As String In grp

                '        _StateCreateBand = False
                '        Dim _GrbandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                '        Dim _GrbandType2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                '        For Each R As DataRow In EmpData.Select("FTQADate='" & HI.UL.ULF.rpQuoted(_FTQAData) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'  AND FTUnitSectCode='" & HI.UL.ULF.rpQuoted(_UnitSectCode) & "' AND FTQATypeCode='" & HI.UL.ULF.rpQuoted(Ind) & "'", "FTQADetailCode")

                '            If _StateCreateBand = False Then

                '                With _GrbandType
                '                    .AppearanceHeader.Options.UseTextOptions = True
                '                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '                    .Caption = R!FNStateQAByType.ToString
                '                    .Name = "gbt" & R!FNHSysQATypeId.ToString
                '                    .RowCount = 1

                '                End With

                '                .Bands.Add(_GrbandType)

                '                With _GrbandType2
                '                    .AppearanceHeader.Options.UseTextOptions = True
                '                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '                    .Caption = R!FTQATypeCode.ToString
                '                    .Name = "gbt2" & R!FNHSysQATypeId.ToString
                '                    .RowCount = 1
                '                End With

                '                _GrbandType.Children.Add(_GrbandType2)

                '                _StateCreateBand = True
                '            End If

                '            Dim _GrbandCol1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                '            Dim _GrbandCol2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                '            With _GrbandCol1
                '                .AppearanceHeader.Options.UseTextOptions = True
                '                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '                .Caption = R!FNStateQAByDetail.ToString
                '                .Name = "gbcol1" & R!FNHSysQADetailId.ToString
                '                .RowCount = 1

                '            End With

                '            _GrbandType2.Children.Add(_GrbandCol1)

                '            With _GrbandCol2
                '                .AppearanceHeader.Options.UseTextOptions = True
                '                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '                .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName("C" & R!FNHSysQADetailId.ToString))
                '                .Caption = R!FTQADetailCode.ToString
                '                .Name = "gbcol2" & R!FNHSysQADetailId.ToString
                '                .RowCount = 1
                '                .Width = 50

                '            End With

                '            _GrbandCol1.Children.Add(_GrbandCol2)

                '        Next

                '    Next
                'End If


                .EndInit()
            End With

            Me.ogcdetail.DataSource = Me.GridData


        Catch ex As Exception

        End Try


    End Sub

#End Region

    Private Sub ogvdetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            With Me.ogvdetail

                Dim _RowData As Double = Val("" & .GetRowCellValue(e.RowHandle, "FNRowSeq").ToString)

                Select Case True
                    Case (_RowData = 1)
                        e.Appearance.BackColor = Drawing.Color.LightYellow
                        e.Appearance.BackColor2 = Drawing.Color.Orange

                    Case (_RowData > 1 And _RowData < 14)
                        e.Appearance.BackColor = Drawing.Color.LightYellow
                        e.Appearance.BackColor2 = Drawing.Color.Orchid
                    Case (_RowData = 14)
                        e.Appearance.BackColor = Drawing.Color.LightYellow
                        e.Appearance.BackColor2 = Drawing.Color.Orange

                    Case (_RowData > 14 And _RowData < 29)
                        e.Appearance.BackColor = Drawing.Color.LightYellow
                        e.Appearance.BackColor2 = Drawing.Color.Lime
                    Case (_RowData = 29)
                        e.Appearance.BackColor = Drawing.Color.LightYellow
                        e.Appearance.BackColor2 = Drawing.Color.Orange

                    Case (_RowData > 29)
                        e.Appearance.BackColor = Drawing.Color.LightYellow
                        e.Appearance.BackColor2 = Drawing.Color.Pink
                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class

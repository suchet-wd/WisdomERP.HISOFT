Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.XtraEditors.Controls
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraTab

Public Class wProdMUSetRatio

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _wPopUpAddPartCode As wPopUpAddPartCode

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wPopUpAddPartCode.Name.ToString.Trim, _wPopUpAddPartCode)
        Catch ex As Exception
        Finally
        End Try

        _wPopUpAddPartCode = New wPopUpAddPartCode
        HI.TL.HandlerControl.AddHandlerObj(_wPopUpAddPartCode)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wPopUpAddPartCode.Name.ToString.Trim, _wPopUpAddPartCode)
        Catch ex As Exception
        Finally
        End Try

        ' Add any initialization after the InitializeComponent() call.




    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""


        '------End Add Summary Grid-------------
    End Sub


    Private Sub ClearGrid()



        With Me.ogvpackdetail
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With



        ogcpackdetail.DataSource = Nothing


    End Sub

#End Region

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


    Private Sub InitGridRatio(_dt As DataTable, _dt2 As DataTable)
        Try

            Dim _Qry As String = ""
            Dim _colcount As Integer = 0

            With Me.AdvBandedGridView2

                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                         "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNTableQty".ToUpper, "FNQuantity".ToUpper, "FNTotalYardPerLayer".ToUpper
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next


                Try
                    For I As Integer = .Bands.Count - 1 To 0 Step -1

                        Select Case .Bands(I).Name.ToUpper

                            Case "gBMark".ToUpper, "gBTotal".ToUpper
                            Case Else
                                .Bands.Remove(.Bands(I))



                        End Select

                    Next
                Catch ex As Exception

                End Try


                If Not (_dt Is Nothing) Then
                    'Dim view As New DataView(_dt)
                    'Dim distinctValues As DataTable = view.ToTable(True, "FNHSysStyleId")

                    'For Each r As DataRow In distinctValues.Rows



                    'Dim _Colsdt As DataTable = _dt.Select("FNHSysStyleId='" & r!FNHSysStyleId.ToString & "'").CopyToDataTable
                    'Dim colwith As Integer = 0
                    Dim _StyleCodeOld As String = ""
                    Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    Dim colwith As Integer = 0
                    For Each Col As DataColumn In _dt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper

                            Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                        "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                     "FNHSysStyleId_Hide".ToUpper


                            Case Else
                                Dim ColG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

                                Dim _StyleCode As String = ""
                                Dim _SizeBreakDown As String = ""
                                _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
                                _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))

                                If Not (_StyleCodeOld = _StyleCode) Then
                                    colwith = 0
                                    ColBand = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                    With ColBand
                                        .Visible = True

                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = _StyleCode

                                        .RowCount = 1

                                        .Name = "GridBand1" + _StyleCode
                                        .VisibleIndex = 1
                                        .Width = _dt2.Select("FTStyleCode='" & _StyleCode & "'").Length * 45

                                    End With

                                    .Bands.Add(ColBand)
                                End If


                                _colcount = _colcount + 1
                                colwith += +1
                                ColG = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString
                                    .Caption = _SizeBreakDown
                                    .Width = 45
                                End With
                                'ColBand.Columns.Add(ColG)
                                .Columns.Add(ColG)

                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    .ColumnEdit = Me.RepositoryItemCalcEdit1

                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = True
                                        .ReadOnly = False
                                    End With

                                End With

                                '.Columns(Col.ColumnName.ToString).Width = 45
                                '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                                _StyleCodeOld = _StyleCode
                        End Select
                    Next
                    ' Next
                End If
                Try
                    For I As Integer = .Bands.Count - 1 To 0 Step -1

                        Select Case .Bands(I).Name.ToUpper

                            Case "gBMark".ToUpper, "gBTotal".ToUpper
                            Case Else


                                For Each Col As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In .Columns
                                    Dim _name As String = Replace(.Bands(I).Name, "GridBand1", "")

                                    If Microsoft.VisualBasic.Left(Col.Name.ToString, Len(_name)) = _name Then
                                        Select Case Col.FieldName.ToUpper

                                            Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                            Case Else

                                                .Bands(I).Columns.Add(Col)

                                        End Select
                                    End If
                                Next
                        End Select
                    Next
                Catch ex As Exception
                End Try

            End With
            Me.ogcratio.DataSource = _dt.Copy


        Catch ex As Exception
            MsgBox("N" & ex.ToString)
        End Try
    End Sub

    Private Sub InitGridRatioDynamic(_dt As DataTable, _dt2 As DataTable, ogv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView, ogc As DevExpress.XtraGrid.GridControl)
        Try

            Dim _Qry As String = ""
            Dim _colcount As Integer = 0

            With ogv

                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                         "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next


                'Try
                '    For I As Integer = .Bands.Count - 1 To 0 Step -1

                '        Select Case .Bands(I).Name.ToUpper

                '            Case "gBMark".ToUpper, "gBTotal".ToUpper

                '            Case Else
                '                .Bands.Remove(.Bands(I))



                '        End Select

                '    Next
                'Catch ex As Exception

                'End Try


                If Not (_dt Is Nothing) Then
                    'Dim view As New DataView(_dt)
                    'Dim distinctValues As DataTable = view.ToTable(True, "FNHSysStyleId")

                    'For Each r As DataRow In distinctValues.Rows



                    'Dim _Colsdt As DataTable = _dt.Select("FNHSysStyleId='" & r!FNHSysStyleId.ToString & "'").CopyToDataTable
                    'Dim colwith As Integer = 0
                    Dim _StyleCodeOld As String = ""
                    Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    Dim colwith As Integer = 0
                    For Each Col As DataColumn In _dt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper

                            Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                        "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                     "FNHSysStyleId_Hide".ToUpper


                            Case Else
                                Dim ColG As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

                                Dim _StyleCode As String = ""
                                Dim _SizeBreakDown As String = ""
                                _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
                                _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))

                                If Not (_StyleCodeOld = _StyleCode) Then
                                    colwith = 0
                                    ColBand = New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                    With ColBand
                                        .Visible = True

                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = _StyleCode

                                        .RowCount = 1

                                        .Name = "GridBand1" + _StyleCode
                                        .VisibleIndex = 1
                                        .Width = _dt2.Select("FTStyleCode='" & _StyleCode & "'").Length * 45

                                    End With

                                    .Bands.Add(ColBand)
                                End If


                                _colcount = _colcount + 1
                                colwith += +1
                                ColG = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString
                                    .Caption = _SizeBreakDown
                                    .Width = 45
                                End With
                                'ColBand.Columns.Add(ColG)
                                .Columns.Add(ColG)

                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    .ColumnEdit = Me.RepositoryItemCalcEdit1

                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = True
                                        .ReadOnly = False
                                    End With

                                End With

                                '.Columns(Col.ColumnName.ToString).Width = 45
                                '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                                _StyleCodeOld = _StyleCode
                        End Select
                    Next
                    ' Next
                End If
                Try
                    For I As Integer = .Bands.Count - 1 To 0 Step -1

                        Select Case .Bands(I).Name.ToUpper

                            Case "gBMark".ToUpper, "gBTotal".ToUpper
                            Case Else


                                For Each Col As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In .Columns
                                    Dim _name As String = Replace(.Bands(I).Name, "GridBand1", "")

                                    If Microsoft.VisualBasic.Left(Col.Name.ToString, Len(_name)) = _name Then
                                        Select Case Col.FieldName.ToUpper

                                            Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                            Case Else

                                                .Bands(I).Columns.Add(Col)

                                        End Select
                                    End If
                                Next
                        End Select
                    Next
                Catch ex As Exception
                End Try

            End With
            AddHandler ogv.RowStyle, AddressOf AdvBandedGridView2_RowStyle




            ogc.DataSource = _dt.Copy
            ogv.BestFitColumns()

        Catch ex As Exception
            MsgBox("N" & ex.ToString)
        End Try
    End Sub


    Private Sub LoadOrderPackBreakDown()
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_MU_OrderBreakDown  @FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
        _Qry &= vbCrLf & " , @FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        With Me.ogvpackdetail

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTCustomerPO".ToUpper, "FTPOLine".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorWay".ToUpper, "FTCustomerPO".ToUpper, "FTPOLine".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                Next

            End If


        End With

        Me.ogcpackdetail.DataSource = _dt.Copy

    End Sub

    Public Sub loadpart()
        Dim _Qry As String = ""
        Dim _dt As DataTable
        If Me.FTDocumentNo.Text <> "" And Me.FTGroupNo.Text <> "" Then
            Dim _ds As New DataSet




            _Qry = "select  distinct '0' as FTSelect  , FTPartCode   from  TPRODMUGroupPlan S with(nolock)  "
            _Qry &= vbCrLf & "where FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'  and FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"






            _Qry &= vbCrLf & "Select    FNSeq  , FTPartCode  from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio with(nolock) "
            _Qry &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            _Qry &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            _Qry &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
            _Qry &= vbCrLf & " and   FNSeq =1 "
            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)


            '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcpart.DataSource = _ds.Tables(0).Copy
            With DirectCast(Me.ogcpart.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    For Each x As DataRow In _ds.Tables(1).Select("FTPartCode='" & R!FTPartCode.ToString & "'")
                        R!FTSelect = "1"
                    Next
                Next


            End With

            LoadData(Me.FTDocumentNo.Text, Me.FTGroupNo.Text)
        End If



    End Sub


    Public Sub LoadData(ByVal Key As String, ByVal Key2 As String)
        Me.FTDocumentNo.Text = Key
        Me.FTGroupNo.Text = Key2

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        Dim _ds As New DataSet
        Dim _dt2 As DataTable
        Dim _dtpart As DataTable

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        Try
            Me.FNConsAvg.Value = 0
            Me.FNMaxLayers.Value = 0
            Me.FNMaxYard.Value = 0
            Me.FNMinYard.Value = 0
            _Qry = "select top 1   FNMaxLayer, FNConsAvg, FNMinYard, FNMaxYard   from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio with(nolock)"
            _Qry &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            _Qry &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            _Qry &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows

                Me.FNConsAvg.Value = Val(R!FNConsAvg)
                Me.FNMaxLayers.Value = Val(R!FNMaxLayer)
                Me.FNMaxYard.Value = Val(R!FNMaxYard)
                Me.FNMinYard.Value = Val(R!FNMinYard)
            Next


            Dim _PartCode As String = ""
            _Qry = ""

            _Qry &= vbCrLf & "Select   distinct FNSeq  ,    "
            _Qry &= vbCrLf & "  STUFF ( (SELECT   distinct ',' +FTPartCode   "
            _Qry &= vbCrLf & " from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio b with(nolock) "
            _Qry &= vbCrLf & "where a.FTDocumentNo = b.FTDocumentNo"
            _Qry &= vbCrLf & "and a.FTGroupNo = b.FTGroupNo"
            _Qry &= vbCrLf & "and a.FNSeq = b.FNSeq"
            _Qry &= vbCrLf & " FOR XML PATH('')), 1, 1, ''"
            _Qry &= vbCrLf & ") FTPartCode"
            _Qry &= vbCrLf & ""
            _Qry &= vbCrLf & " from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio a with(nolock) "
            _Qry &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            _Qry &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            _Qry &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
            _Qry &= vbCrLf & " order by FNSeq asc "
            _dtpart = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Try
                Me.xtabpart.TabPages.Clear()
            Catch ex As Exception

            End Try

            Try
                Me.xtabpart.TabPages.Clear()
            Catch ex As Exception

            End Try


            Try
                Me.xtabpart.TabPages.Clear()
            Catch ex As Exception

            End Try


            Try
                Me.xtabpart.TabPages.Clear()
            Catch ex As Exception

            End Try


            Try
                Me.xtabpart.TabPages.Clear()
            Catch ex As Exception

            End Try



            If _dtpart.Rows.Count > 0 Then




                ' Me.xtabpart.TabPages.Add(Me.XtraTabPage1)

                Try
                    Dim i As Integer = 0
                    For Each x As DevExpress.XtraTab.XtraTabPage In Me.xtabpart.TabPages

                        If x.Name = "XtraTabPage1" Then

                            x.PageEnabled = False
                            x.PageVisible = False
                        Else
                            Try
                                Me.xtabpart.TabPages.Remove(x)
                                Me.xtabpart.TabPages.RemoveAt(i)
                            Catch ex As Exception

                            End Try

                        End If
                        i += +1
                    Next

                Catch ex As Exception

                End Try


                For Each R As DataRow In _dtpart.Rows
                    _PartCode = R!FTPartCode.ToString

                    _Qry = "Exec  dbo.SP_GET_MUGroupPlanForRatio_Save  @FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' , @FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'  , @PartCode='" & HI.UL.ULF.rpQuoted(_PartCode) & "'  , @Seq=" & Val(R!FNSeq)
                    Me.ogcratio.DataSource = Nothing
                    _ds = New DataSet


                    HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)
                    If _ds.Tables.Count <= 1 Then
                        Continue For
                    Else
                        _dt = _ds.Tables(0)
                        _dt2 = _ds.Tables(1)
                    End If

                    Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
                    Dim _Grid As New DevExpress.XtraGrid.GridControl
                    Dim _GridV As New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView

                    With _TabPage
                        .Name = "otbx" & _PartCode
                        .Text = _PartCode
                        .Tag = "2|"


                    End With

                    With _Grid
                        .Name = "ogcGSum" & _PartCode
                        .Tag = "2|"

                        .Dock = System.Windows.Forms.DockStyle.Fill
                        .EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
                        .Location = New System.Drawing.Point(0, 0)
                        .Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
                        .RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2, Me.RepositoryItemButtonEdit1, Me.RepositoryItemCalcEdit1})
                        .Size = New System.Drawing.Size(1116, 473)
                        .TabIndex = 2
                        .TabStop = False

                    End With


                    _GridV = _gridCtl(_PartCode, _Grid)




                    _Grid.BeginInit()
                    _Grid.MainView = _GridV
                    _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
                    _Grid.EndInit()


                    InitGridRatioDynamic(_dt, _dt2, _GridV, _Grid)

                    _TabPage.Controls.Add(_Grid)
                    _Grid.Dock = DockStyle.Fill



                    HI.TL.HandlerControl.AddHandlerObj(_TabPage)

                    Me.xtabpart.TabPages.Add(_TabPage)

                Next








            End If

            Call LoadOrderPackBreakDown()


        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub


    Public Sub genNewTabData(ByVal Key As String, ByVal Key2 As String, ByVal _PartCode As String)
        Me.FTDocumentNo.Text = Key
        Me.FTGroupNo.Text = Key2

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        Dim _ds As New DataSet
        Dim _dt2 As DataTable
        Dim _dtpart As DataTable

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        Try


            _Qry = "Exec  dbo.SP_GET_MUGroupPlanForRatio  @FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' , @FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"

            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)
            If _ds.Tables.Count <= 1 Then
                _ds = New DataSet
                _Qry = "Exec  dbo.SP_GET_MUGroupPlanForRatio  @FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' , @FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)

                _dt = _ds.Tables(0)
                _dt2 = _ds.Tables(1)
            Else
                _dt = _ds.Tables(0)
                _dt2 = _ds.Tables(1)
            End If




            Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
            Dim _Grid As New DevExpress.XtraGrid.GridControl
            Dim _GridV As New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView

            With _TabPage
                .Name = "otbx" & _PartCode
                .Text = _PartCode
                .Tag = "2|"
            End With

            With _Grid
                .Name = "ogcGSum" & _PartCode
                .Tag = "2|"
                .Dock = System.Windows.Forms.DockStyle.Fill
                .EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
                .Location = New System.Drawing.Point(0, 0)
                .Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
                .RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2, Me.RepositoryItemButtonEdit1, Me.RepositoryItemCalcEdit1})
                .Size = New System.Drawing.Size(1116, 473)
                .TabIndex = 2
                .TabStop = False

            End With





            '_GridV = Me.AdvBandedGridView2


            _GridV = _gridCtl(_PartCode, _Grid)




            _Grid.BeginInit()
            _Grid.MainView = _GridV
            _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
            _Grid.EndInit()
            _TabPage.Controls.Add(_Grid)
            _Grid.Dock = DockStyle.Fill






            InitGridRatioDynamic(_dt, _dt2, _GridV, _Grid)

            'Me.ogcratio.DataSource = _dt

            'InitGridRatio(_dt, _dt2)

            Dim _RawInd As Integer = 0
            With DirectCast(_Grid.DataSource, DataTable)
                .AcceptChanges()
                For Each r As DataRow In .Select("FNSeq=1", "MarkSeq asc ")
                    _RawInd += +1
                    If _PartCode = "" Then
                        r!FNHSysMarkId = ""
                    Else
                        'r!FNHSysMarkId = _PartCode & Microsoft.VisualBasic.Right("00" & _RawInd, 3)
                        r!FNHSysMarkId = Me.genMarkRun(_PartCode, _RawInd)
                    End If
                Next
                .AcceptChanges()
            End With



            HI.TL.HandlerControl.AddHandlerObj(_TabPage)


            Me.xtabpart.TabPages.Add(_TabPage)




            'Me.ogcratio.DataSource = _dt

            'Call LoadOrderPackBreakDown()

        Catch ex As Exception
            MsgBox("N" & ex.Message)
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Function genMarkRun(Part As String, Optional ByVal seq As Integer = 1) As String
        Try

            Dim _cmd As String = ""
            Dim _DocRun As String = ""
            _cmd = "select ISNULL( MAX(FTDocRunNo)  ,'')  as FTDocNo "
            _cmd &= vbCrLf & " FROM       " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.SequenceTABLE"
            _cmd &= vbCrLf & " where FTTableName = 'TPRODMURatio_D' and FNHSysCmpId =  " & HI.ST.SysInfo.CmpID
            _cmd &= vbCrLf & " and LEFT(FTDocRunNo,len('" & Part & "'))  = '" & Part & "'  "
            _cmd &= vbCrLf & " and LEFT(FTDocRunNo , len('" & Part & "') + 6 ) = '" & Part & "' +  RIGHT( CONVERT(varchar(10) ,  GETDATE() ,112)  , 6)   "
            _DocRun = HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_SYSTEM, "")
            If _DocRun = "" Then
                _cmd = " select  '" & Part & "' +  RIGHT( CONVERT(varchar(10) ,  GETDATE() ,112)  , 6) +  right( '000' + '" & seq.ToString & "' , 4) as FTDocNo   "
                _DocRun = HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_SYSTEM, "")
            Else
                Dim NewDoc As String = Microsoft.VisualBasic.Right("0000" + (Integer.Parse(Microsoft.VisualBasic.Right(_DocRun, 4)) + seq).ToString, 4)
                _DocRun = Microsoft.VisualBasic.Left(_DocRun, Len(_DocRun) - 4) & NewDoc
            End If

            _cmd = "insert into  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.SequenceTABLE ( FNHSysCmpId, FTDocRunNo, FTTableName) "
            _cmd &= vbCrLf & " select " & HI.ST.SysInfo.CmpID & ",'" & _DocRun & "' ,'TPRODMURatio_D'"
            HI.Conn.SQLConn.ExecuteOnly(_cmd, Conn.DB.DataBaseName.DB_PROD)

            Return _DocRun
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Function _gridCtl(_PartCode As String, _Grid As DevExpress.XtraGrid.GridControl) As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
        Try
            Dim _gBMark As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            Dim _gBTotal As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            Dim _GridV As New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
            Dim _BandedGridColumn17 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn18 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn19 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn20 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

            Dim _BandedGridColumn7 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn8 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn9 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn10 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn

            Dim _BandedGridColumn21 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn22 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn


            Dim _cFNHSysMarkId As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn


            _cFNHSysMarkId.Caption = "Mark"
            _cFNHSysMarkId.FieldName = "FNHSysMarkId"
            _cFNHSysMarkId.MinWidth = 25
            _cFNHSysMarkId.Name = "cFNHSysMarkId" & _PartCode
            _cFNHSysMarkId.OptionsColumn.AllowEdit = False
            _cFNHSysMarkId.Visible = True
            _cFNHSysMarkId.Width = 209

            _gBMark.Columns.Add(_cFNHSysMarkId)
            _gBMark.Name = "gBMark" & _PartCode
            _gBMark.Caption = "-"
            _gBMark.VisibleIndex = 0
            _gBMark.Width = 209

            _BandedGridColumn7.Caption = "FNHSysMarkId"
            _BandedGridColumn7.FieldName = "FNHSysMarkId_Hide"
            _BandedGridColumn7.MinWidth = 25
            _BandedGridColumn7.Name = "BandedGridColumn7" & _PartCode
            _BandedGridColumn7.Width = 94




            _BandedGridColumn20.Caption = "รวม"
            _BandedGridColumn20.DisplayFormat.FormatString = "N0"
            _BandedGridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn20.FieldName = "FNOrderQty"
            _BandedGridColumn20.MinWidth = 25
            _BandedGridColumn20.Name = "BandedGridColumn20" & _PartCode
            _BandedGridColumn20.OptionsColumn.AllowEdit = False
            _BandedGridColumn20.Visible = True
            _BandedGridColumn20.Width = 114


            _BandedGridColumn18.Caption = "ปู"
            _BandedGridColumn18.ColumnEdit = _RepositoryItemCalcEdit1
            _BandedGridColumn18.DisplayFormat.FormatString = "N0"
            _BandedGridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn18.FieldName = "FNLayerQty"
            _BandedGridColumn18.MinWidth = 25
            _BandedGridColumn18.Name = "BandedGridColumn18" & _PartCode
            _BandedGridColumn18.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNLayerQty", "{0:N0}")})
            _BandedGridColumn18.Visible = True
            _BandedGridColumn18.Width = 110



            _BandedGridColumn17.Caption = "จำนวนตัว"
            _BandedGridColumn17.DisplayFormat.FormatString = "N0"
            _BandedGridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn17.FieldName = "FNQuantity"
            _BandedGridColumn17.MinWidth = 25
            _BandedGridColumn17.Name = "BandedGridColumn17" & _PartCode
            _BandedGridColumn17.OptionsColumn.AllowEdit = False
            _BandedGridColumn17.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:N0}")})
            _BandedGridColumn17.Visible = True
            _BandedGridColumn17.Width = 114
            '
            'BandedGridColumn19
            '
            _BandedGridColumn19.Caption = "ยอด"
            _BandedGridColumn19.DisplayFormat.FormatString = "N0"
            _BandedGridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn19.FieldName = "FNTotalQty"
            _BandedGridColumn19.MinWidth = 25
            _BandedGridColumn19.Name = "BandedGridColumn19" & _PartCode
            _BandedGridColumn19.OptionsColumn.AllowEdit = False
            _BandedGridColumn19.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQty", "{0:N0}")})
            _BandedGridColumn19.Visible = True
            _BandedGridColumn19.Width = 110


            _BandedGridColumn21.Caption = "จำนวนโต๊ะ"
            _BandedGridColumn21.DisplayFormat.FormatString = "N0"
            _BandedGridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn21.FieldName = "FNTableQty"
            _BandedGridColumn21.MinWidth = 25
            _BandedGridColumn21.Name = "BandedGridColumn21" & _PartCode
            _BandedGridColumn21.OptionsColumn.AllowEdit = False
            _BandedGridColumn21.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTableQty", "{0:N0}")})
            _BandedGridColumn21.Visible = True
            _BandedGridColumn21.Width = 110


            _BandedGridColumn22.Caption = "Length"
            _BandedGridColumn22.DisplayFormat.FormatString = "N2"
            _BandedGridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn22.FieldName = "FNTotalYardPerLayer"
            _BandedGridColumn22.MinWidth = 25
            _BandedGridColumn22.Name = "BandedGridColumn22" & _PartCode
            _BandedGridColumn22.OptionsColumn.AllowEdit = False
            _BandedGridColumn22.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalYardPerLayer", "{0:N2}")})
            _BandedGridColumn22.Visible = True
            _BandedGridColumn21.Width = 110


            _BandedGridColumn10.Caption = "MarkSeq"
            _BandedGridColumn10.FieldName = "MarkSeq"
            _BandedGridColumn10.MinWidth = 25
            _BandedGridColumn10.Name = "BandedGridColumn10" & _PartCode
            _BandedGridColumn10.Width = 94
            '
            'BandedGridColumn9
            '
            _BandedGridColumn9.Caption = "BandedGridColumn9"
            _BandedGridColumn9.FieldName = "FNSeq"
            _BandedGridColumn9.MinWidth = 25
            _BandedGridColumn9.Name = "BandedGridColumn9" & _PartCode
            _BandedGridColumn9.Width = 94
            '
            'BandedGridColumn7
            '
            _BandedGridColumn7.Caption = "FNHSysMarkId"
            _BandedGridColumn7.FieldName = "FNHSysMarkId_Hide"
            _BandedGridColumn7.MinWidth = 25
            _BandedGridColumn7.Name = "BandedGridColumn7" & _PartCode
            _BandedGridColumn7.Width = 94
            '
            'BandedGridColumn8
            '
            _BandedGridColumn8.Caption = "BandedGridColumn8"
            _BandedGridColumn8.FieldName = "FNHSysStyleId"
            _BandedGridColumn8.MinWidth = 25
            _BandedGridColumn8.Name = "BandedGridColumn8" & _PartCode
            _BandedGridColumn8.Width = 94

            _gBTotal.Columns.Add(_BandedGridColumn20)
            _gBTotal.Columns.Add(_BandedGridColumn18)
            _gBTotal.Columns.Add(_BandedGridColumn17)
            _gBTotal.Columns.Add(_BandedGridColumn19)
            _gBTotal.Columns.Add(_BandedGridColumn21)
            _gBTotal.Columns.Add(_BandedGridColumn22)
            _gBTotal.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            _gBTotal.Name = "gBTotal" & _PartCode
            _gBTotal.Caption = "-"
            _gBTotal.VisibleIndex = 3
            _gBTotal.Width = 448


            With _GridV
                .GridControl = _Grid
                .Name = "ogvGSum" & _PartCode

                .Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {_gBMark, _gBTotal})
                .Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {_BandedGridColumn10, _BandedGridColumn9, _BandedGridColumn7, _cFNHSysMarkId, _BandedGridColumn20, _BandedGridColumn18, _BandedGridColumn17, _BandedGridColumn19, _BandedGridColumn8, _BandedGridColumn21, _BandedGridColumn22})
                .DetailHeight = 431
                '.GridControl = Me.ogcratio
                '.Name = "ogvGSum" & _PartCode
                .OptionsCustomization.AllowQuickHideColumns = False
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.ShowGroupPanel = False
                .Tag = "2|"

            End With

            Return _GridV
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTGroupNo.Text <> "" And FTGroupNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTDocumentNo.Text <> "" And FTDocumentNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If


        _Pass = True

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call InitGrid()
            HI.TL.HandlerControl.AddHandlerGridColumnEdit(Me.AdvBandedGridView2)
            Me.RepositoryItemCalcEdit1.Buttons(0).Visible = False
            Me.RepositoryItemCalcEdit1.Buttons(0).Enabled = False
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData(Me.FTDocumentNo.Text, Me.FTGroupNo.Text)
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region



    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmcalslayer_Click(sender As Object, e As EventArgs) Handles ocmcalslayer.Click
        Try
            'คีย์ ratio คำนวณ Layer

            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Calslayer(_Grid.MainView, _Grid)
                        Exit Select
                End Select
            Next


#Region "Old"
            'With Me.AdvBandedGridView2
            '    If .FocusedRowHandle < 0 Or .RowCount < 0 Then Exit Sub

            '    Dim ratiofocus As Integer = Val(.GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString())
            '    Dim breakdown As Integer = Val(.GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn).ToString())

            '    Dim MarkSeq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "MarkSeq").ToString())
            '    Dim Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString())


            '    Dim valuex As Double = breakdown \ ratiofocus


            '    Dim _dt As DataTable
            '    With DirectCast(Me.ogcratio.DataSource, DataTable)
            '        .AcceptChanges()

            '        _dt = .Copy

            '    End With


            '    If Not (_dt Is Nothing) Then
            '        Dim _StyleCodeOld As String = ""
            '        Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
            '        Dim colwith As Integer = 0
            '        Dim _OrderQty As Integer = 0

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 1 Then


            '                Dim _odtbd As DataTable

            '                If MarkSeq = 1 Then
            '                    _odtbd = _dt.Select("FNSeq=0").CopyToDataTable
            '                Else
            '                    _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1).CopyToDataTable
            '                End If
            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
            '                             "FNHSysStyleId_Hide".ToUpper
            '                        Case Else


            '                            R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex
            '                            _OrderQty += +Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex


            '                    End Select
            '                Next
            '            End If

            '        Next
            '        _dt.AcceptChanges()

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 1 Then

            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "Total".ToUpper, "FNHSysStyleId_Hide".ToUpper

            '                        Case "FNOrderQty".ToUpper
            '                            R.Item(Col.ColumnName) = 0
            '                        Case "FNLayerQty".ToUpper
            '                            R.Item(Col.ColumnName) = valuex
            '                        Case "FNQuantity".ToUpper
            '                            R.Item(Col.ColumnName) = _OrderQty
            '                        Case "FNTotalQty".ToUpper
            '                            R.Item(Col.ColumnName) = valuex * _OrderQty
            '                        Case Else


            '                            ' R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) / valuex

            '                    End Select
            '                Next
            '            End If

            '        Next

            '        Dim _OrderBal As Integer = 0
            '        _dt.AcceptChanges()

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 3 Then
            '                Dim _odtbd As DataTable
            '                If MarkSeq = 1 Then
            '                    _odtbd = _dt.Select("FNSeq<>3", "FNSeq asc").CopyToDataTable
            '                Else
            '                    _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1, "FNSeq asc").CopyToDataTable
            '                    _odtbd.Merge(_dt.Select("FNSeq<>3 and MarkSeq=" & MarkSeq, "FNSeq asc").CopyToDataTable)
            '                End If

            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
            '                             "FNHSysStyleId_Hide".ToUpper


            '                        Case Else

            '                            _OrderBal += +(Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex))
            '                            R.Item(Col.ColumnName) = (Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex)) 'Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

            '                    End Select
            '                Next
            '            End If

            '        Next

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 3 Then

            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "FNTotalQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
            '                             "FNHSysStyleId_Hide".ToUpper
            '                        Case "FNOrderQty".ToUpper
            '                            R.Item(Col.ColumnName) = _OrderBal

            '                        Case Else


            '                            'R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

            '                    End Select
            '                Next
            '            End If

            '        Next


            '        Me.ogcratio.DataSource = _dt


            '        ' Next


            '    End If


            'End With

#End Region



        Catch ex As Exception

        End Try
    End Sub


    Private Sub CalsAI(ogv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView, ogc As DevExpress.XtraGrid.GridControl)
        Try
            With ogv
                If .FocusedRowHandle < 0 Or .RowCount < 0 Then Exit Sub

                Dim _dt As DataTable
                With DirectCast(ogc.DataSource, DataTable)
                    .AcceptChanges()

                    _dt = .Copy

                End With

            End With
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Calslayer(ogv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView, ogc As DevExpress.XtraGrid.GridControl)
        Try

            With ogv
                If .FocusedRowHandle < 0 Or .RowCount < 0 Then Exit Sub

                Dim ratiofocus As Integer = Val(.GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString())
                Dim breakdown As Integer = Val(.GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn).ToString())

                Dim MarkSeq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "MarkSeq").ToString())
                Dim Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString())


                Dim valuex As Double = breakdown \ ratiofocus


                Dim _dt As DataTable
                With DirectCast(ogc.DataSource, DataTable)
                    .AcceptChanges()

                    _dt = .Copy

                End With


                If Not (_dt Is Nothing) Then
                    Dim _StyleCodeOld As String = ""
                    Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    Dim colwith As Integer = 0
                    Dim _OrderQty As Integer = 0

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 1 Then


                            Dim _odtbd As DataTable

                            If MarkSeq = 1 Then
                                _odtbd = _dt.Select("FNSeq=0").CopyToDataTable
                            Else
                                _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1).CopyToDataTable
                            End If
                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                    Case Else


                                        R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex
                                        _OrderQty += +Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex


                                End Select
                            Next
                        End If

                    Next
                    _dt.AcceptChanges()

                    Dim totalsolid As Integer = 0

                    If (Me.FNMaxLayers.Value < valuex) Then
                        totalsolid = Math.Floor(valuex / Me.FNMaxLayers.Value)
                        valuex = Me.FNMaxLayers.Value * totalsolid

                    End If

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 1 Then

                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "Total".ToUpper, "FNHSysStyleId_Hide".ToUpper

                                    Case "FNOrderQty".ToUpper
                                        R.Item(Col.ColumnName) = 0
                                    Case "FNLayerQty".ToUpper
                                        R.Item(Col.ColumnName) = valuex
                                    Case "FNQuantity".ToUpper
                                        R.Item(Col.ColumnName) = _OrderQty
                                    Case "FNTotalQty".ToUpper
                                        R.Item(Col.ColumnName) = valuex * _OrderQty
                                    Case "FNTableQty".ToUpper
                                        R.Item(Col.ColumnName) = totalsolid
                                    Case "FNTotalYardPerLayer".ToUpper
                                        R.Item(Col.ColumnName) = _OrderQty * Me.FNConsAvg.Value
                                    Case Else


                                        ' R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) / valuex

                                End Select
                            Next
                        End If

                    Next

                    Dim _OrderBal As Integer = 0
                    _dt.AcceptChanges()

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 3 Then
                            Dim _odtbd As DataTable
                            If MarkSeq = 1 Then
                                _odtbd = _dt.Select("FNSeq<>3", "FNSeq asc").CopyToDataTable
                            Else
                                _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1, "FNSeq asc").CopyToDataTable
                                _odtbd.Merge(_dt.Select("FNSeq<>3 and MarkSeq=" & MarkSeq, "FNSeq asc").CopyToDataTable)
                            End If

                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper


                                    Case Else

                                        _OrderBal += +(Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex))
                                        R.Item(Col.ColumnName) = (Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex)) 'Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                End Select
                            Next
                        End If

                    Next

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 3 Then

                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                    Case "FNOrderQty".ToUpper
                                        R.Item(Col.ColumnName) = _OrderBal

                                    Case Else


                                        'R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                End Select
                            Next
                        End If

                    Next


                    ogc.DataSource = _dt


                    ' Next


                End If


            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub AdvBandedGridView2_RowStyle(sender As Object, e As RowStyleEventArgs) Handles AdvBandedGridView2.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim rows As Integer = e.RowHandle Mod 2
                Select Case rows
                    Case 1
                        e.Appearance.BackColor = Color.LightBlue
                        e.Appearance.BackColor2 = Color.SkyBlue
                        e.HighPriority = True
                    Case 2
                        e.Appearance.BackColor = Color.Salmon
                        e.Appearance.BackColor2 = Color.SeaShell
                        e.HighPriority = True
                    Case Else

                        e.Appearance.BackColor = Color.Salmon
                        e.Appearance.BackColor2 = Color.SeaShell
                        e.HighPriority = True

                End Select


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmaddmark_Click(sender As Object, e As EventArgs) Handles ocmaddmark.Click
        addnewmark()
    End Sub

    Public Sub addnewmark()
        Try


            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                        _GridView = _Grid.MainView

                        With DirectCast(_Grid.MainView, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                            .BestFitColumns()
                        End With


                        With _GridView
                            If .RowCount < 0 Then Exit Sub
                            Dim _markSeq As Integer = 0
                            Dim _dr As DataRow
                            Dim _MarkCode As String = ""

                            With DirectCast(_Grid.DataSource, DataTable)
                                .AcceptChanges()
                                For Each R As DataRow In .Select("", "MarkSeq asc , FNSeq asc ")
                                    _markSeq = Val(R!MarkSeq.ToString)
                                Next
                                For Each R As DataRow In .Select("FNSeq=1", "MarkSeq asc  ")
                                    _MarkCode = (R!FNHSysMarkId.ToString)
                                Next
                                ' _MarkCode = Microsoft.VisualBasic.Left(_MarkCode, Len(_MarkCode) - 3) & Microsoft.VisualBasic.Right("00" & (Val(Microsoft.VisualBasic.Right(_MarkCode, 3) + 1)).ToString(), 3)
                                _MarkCode = Me.genMarkRun(Me.xtabpart.SelectedTabPage.Text)

                                _dr = .NewRow
                                _dr("MarkSeq") = _markSeq + 1
                                _dr("FNSeq") = 1
                                _dr("FNHSysMarkId") = _MarkCode
                                .Rows.Add(_dr)
                                _dr = .NewRow
                                _dr("MarkSeq") = _markSeq + 1
                                _dr("FNSeq") = 3
                                .Rows.Add(_dr)
                            End With

                        End With

                End Select
            Next



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try

            If SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Function SaveData() As Boolean
        Try


            Dim _Cmd As String = ""
            Dim _FNSeq As Integer = 0

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Me.FNSeq.Value = 0
            For Each x As DevExpress.XtraTab.XtraTabPage In Me.xtabpart.TabPages
                Dim _Tab As DevExpress.XtraTab.XtraTabPage
                _Tab = DirectCast(x, DevExpress.XtraTab.XtraTabPage)
                If x.Name = "XtraTabPage1" Then

                    x.PageEnabled = False
                    x.PageVisible = False
                Else
                    Me.FNSeq.Value += +1
                    For Each Obj As Object In _Tab.Controls
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.GridControl


                                Dim _Grid As DevExpress.XtraGrid.GridControl
                                _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                                Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                                _GridView = _Grid.MainView



                                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio "
                                _Cmd &= vbCrLf & " set FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Cmd &= vbCrLf & " , FDUpdDate =" & HI.UL.ULDate.FormatDateDB
                                _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                _Cmd &= vbCrLf & " ,FTRemark='" & Me.FTRemark.Text & "'"
                                _Cmd &= vbCrLf & " ,FNMaxLayer= " & Me.FNMaxLayers.Value
                                _Cmd &= vbCrLf & " ,FNConsAvg= " & Me.FNConsAvg.Value
                                _Cmd &= vbCrLf & " ,FNMinYard= " & Me.FNMinYard.Value
                                _Cmd &= vbCrLf & " ,FNMaxYard= " & Me.FNMaxYard.Value


                                _Cmd &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                                _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
                                _Cmd &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                                _Cmd &= vbCrLf & " and FNSeq=" & Me.FNSeq.Value & ""
                                _Cmd &= vbCrLf & " and FTPartCode='" & _Tab.Text & "'"
                                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    _Cmd = "insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio "
                                    _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,FNHSysCmpId, FTGroupNo, FTDocumentNo, FNSeq, FTRemark, FTPartCode,FNMaxLayer ,FNConsAvg,FNMinYard,FNMaxYard )"
                                    _Cmd &= vbCrLf & " select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB
                                    _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB
                                    _Cmd &= vbCrLf & " ," & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
                                    _Cmd &= vbCrLf & " ," & Me.FNSeq.Value & ""
                                    _Cmd &= vbCrLf & " ,'" & Me.FTRemark.Text & "'"
                                    _Cmd &= vbCrLf & " ,'" & _Tab.Text & "'"
                                    _Cmd &= vbCrLf & " ,  " & Me.FNMaxLayers.Value
                                    _Cmd &= vbCrLf & " , " & Me.FNConsAvg.Value
                                    _Cmd &= vbCrLf & " , " & Me.FNMinYard.Value
                                    _Cmd &= vbCrLf & " , " & Me.FNMaxYard.Value


                                    If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Return False
                                    End If
                                End If


                                _Cmd = "delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio_D "

                                _Cmd &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                                _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
                                _Cmd &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""


                                HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                                With DirectCast(_Grid.DataSource, DataTable)
                                    .AcceptChanges()
                                    For Each R As DataRow In .Rows
                                        For Each Col As DataColumn In .Columns
                                            Select Case Col.ColumnName.ToString.ToUpper

                                                Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                                Case "FNOrderQty".ToUpper

                                                Case Else
                                                    Dim _StyleCode As String = ""
                                                    Dim _SizeBreakDown As String = ""
                                                    _StyleCode = Microsoft.VisualBasic.Left(Col.ColumnName.ToString, Col.ColumnName.IndexOf("-"))
                                                    _SizeBreakDown = Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Len(Col.ColumnName.ToString) - (Col.ColumnName.IndexOf("-") + 1))


                                                    Dim _StyleId As Integer = HI.Conn.SQLConn.GetField("Select top 1 FNHSysStyleId from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle with(nolock) where FTStyleCode='" & _StyleCode & "'", Conn.DB.DataBaseName.DB_PROD)

                                                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio_D "
                                                    _Cmd &= vbCrLf & " set FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                                    _Cmd &= vbCrLf & " , FDUpdDate =" & HI.UL.ULDate.FormatDateDB
                                                    _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                                    _Cmd &= vbCrLf & " ,FTColorWay=''"

                                                    _Cmd &= vbCrLf & " ,FNMarkQty=" & Val(R.Item(Col.ColumnName.ToString)) & ""
                                                    _Cmd &= vbCrLf & " ,FNOrderQty=" & Val(R!FNOrderQty.ToString) & ""
                                                    _Cmd &= vbCrLf & " ,FNLayerQty=" & Val(R!FNLayerQty.ToString) & ""
                                                    _Cmd &= vbCrLf & " ,FNQuantity=" & Val(R!FNQuantity.ToString) & ""
                                                    _Cmd &= vbCrLf & " ,FNTotalQty=" & Val(R!FNTotalQty.ToString) & ""
                                                    _Cmd &= vbCrLf & " ,FNTableQty=" & Val(R!FNTableQty.ToString) & ""
                                                    _Cmd &= vbCrLf & " ,FNTotalYardPerLayer=" & Val(R!FNTotalYardPerLayer.ToString) & ""


                                                    _Cmd &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                                                    _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
                                                    _Cmd &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                                                    _Cmd &= vbCrLf & " and FNHSysStyleId=" & _StyleId & ""
                                                    _Cmd &= vbCrLf & " and FNHSysMarkId=" & Val(R!FNHSysMarkId_Hide.ToString) & ""
                                                    _Cmd &= vbCrLf & " and FTMarkCode='" & (R!FNHSysMarkId.ToString) & "'"
                                                    _Cmd &= vbCrLf & " and FNMarkSeq=" & Val(R!MarkSeq.ToString) & ""
                                                    _Cmd &= vbCrLf & " and FNRowNo=" & Val(R!FNSeq.ToString) & ""
                                                    _Cmd &= vbCrLf & " and FNSeq=" & Me.FNSeq.Value & ""
                                                    _Cmd &= vbCrLf & " and FTSizeBreakDown='" & _SizeBreakDown & "'"

                                                    If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                                        _Cmd = "insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio_D "
                                                        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FNHSysCmpId, FTGroupNo, FTDocumentNo,  FNHSysStyleId, FTColorWay, FTSizeBreakDown, FNHSysMarkId, FNMarkSeq, FNMarkQty, FNRowNo, "
                                                        _Cmd &= vbCrLf & "  FNOrderQty, FNLayerQty, FNQuantity, FNTotalQty , FNSeq ,FTMarkCode ,FNTableQty ,FNTotalYardPerLayer )"
                                                        _Cmd &= vbCrLf & " select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                                        _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB
                                                        _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB
                                                        _Cmd &= vbCrLf & " ," & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                                                        _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                                                        _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
                                                        _Cmd &= vbCrLf & " ," & _StyleId & ""
                                                        _Cmd &= vbCrLf & " ,''"
                                                        _Cmd &= vbCrLf & " ,'" & _SizeBreakDown & "'"
                                                        _Cmd &= vbCrLf & "," & Val(R!FNHSysMarkId_Hide.ToString) & ""
                                                        _Cmd &= vbCrLf & "," & Val(R!MarkSeq.ToString) & ""
                                                        _Cmd &= vbCrLf & " ," & Val(R.Item(Col.ColumnName.ToString)) & ""
                                                        _Cmd &= vbCrLf & "," & Val(R!FNSeq.ToString) & ""
                                                        _Cmd &= vbCrLf & " ," & Val(R!FNOrderQty.ToString) & ""
                                                        _Cmd &= vbCrLf & " ," & Val(R!FNLayerQty.ToString) & ""
                                                        _Cmd &= vbCrLf & " ," & Val(R!FNQuantity.ToString) & ""
                                                        _Cmd &= vbCrLf & " ," & Val(R!FNTotalQty.ToString) & ""
                                                        _Cmd &= vbCrLf & " ," & Me.FNSeq.Value & ""
                                                        _Cmd &= vbCrLf & " ,'" & (R!FNHSysMarkId.ToString) & "'"
                                                        _Cmd &= vbCrLf & " ," & Val(R!FNTableQty.ToString) & ""
                                                        _Cmd &= vbCrLf & " ," & Val(R!FNTotalYardPerLayer.ToString) & ""

                                                        If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                            HI.Conn.SQLConn.Tran.Rollback()
                                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                            Return False
                                                        End If
                                                    End If
                                            End Select
                                        Next
                                    Next

                                End With



                        End Select
                    Next
                End If

            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ocmcalsratio_Click(sender As Object, e As EventArgs) Handles ocmcalsratio.Click
        Try


            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Calsratio(_Grid.MainView, _Grid)
                        Exit Select
                End Select
            Next

#Region "Old"

            'คีย์ layer cals  Ratio
            'With Me.AdvBandedGridView2
            '    If .FocusedRowHandle < 0 Or .RowCount < 0 Then Exit Sub

            '    Dim ratiofocus As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNLayerQty").ToString())
            '    'Dim breakdown As Integer = Val(.GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn).ToString())
            '    Dim MarkSeq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "MarkSeq").ToString())
            '    Dim Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString())



            '    Dim valuex As Double = ratiofocus


            '    Dim _dt As DataTable
            '    With DirectCast(Me.ogcratio.DataSource, DataTable)
            '        .AcceptChanges()

            '        _dt = .Copy

            '    End With


            '    If Not (_dt Is Nothing) Then
            '        Dim _StyleCodeOld As String = ""
            '        Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
            '        Dim colwith As Integer = 0
            '        Dim _OrderQty As Integer = 0

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 1 Then


            '                Dim _odtbd As DataTable

            '                If MarkSeq = 1 Then
            '                    _odtbd = _dt.Select("FNSeq=0").CopyToDataTable
            '                Else
            '                    _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1).CopyToDataTable
            '                End If
            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
            '                             "FNHSysStyleId_Hide".ToUpper
            '                        Case Else


            '                            R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex
            '                            _OrderQty += +Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex


            '                    End Select
            '                Next
            '            End If

            '        Next
            '        _dt.AcceptChanges()

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 1 Then

            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "Total".ToUpper, "FNHSysStyleId_Hide".ToUpper

            '                        Case "FNOrderQty".ToUpper
            '                            R.Item(Col.ColumnName) = 0
            '                        Case "FNLayerQty".ToUpper
            '                            'R.Item(Col.ColumnName) = valuex
            '                        Case "FNQuantity".ToUpper
            '                            R.Item(Col.ColumnName) = _OrderQty
            '                        Case "FNTotalQty".ToUpper
            '                            R.Item(Col.ColumnName) = valuex * _OrderQty
            '                        Case Else


            '                            ' R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) / valuex

            '                    End Select
            '                Next
            '            End If

            '        Next

            '        Dim _OrderBal As Integer = 0
            '        _dt.AcceptChanges()

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 3 Then
            '                Dim _odtbd As DataTable
            '                If MarkSeq = 1 Then
            '                    _odtbd = _dt.Select("FNSeq<>3", "FNSeq asc").CopyToDataTable
            '                Else
            '                    _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1, "FNSeq asc").CopyToDataTable
            '                    _odtbd.Merge(_dt.Select("FNSeq<>3 and MarkSeq=" & MarkSeq, "FNSeq asc").CopyToDataTable)
            '                End If

            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
            '                             "FNHSysStyleId_Hide".ToUpper


            '                        Case Else

            '                            _OrderBal += +(Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex))
            '                            R.Item(Col.ColumnName) = (Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex)) 'Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

            '                    End Select
            '                Next
            '            End If

            '        Next

            '        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
            '            If R!FNSeq = 3 Then

            '                For Each Col As DataColumn In _dt.Columns

            '                    Select Case Col.ColumnName.ToString.ToUpper

            '                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
            '                                "FNTotalQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
            '                             "FNHSysStyleId_Hide".ToUpper
            '                        Case "FNOrderQty".ToUpper
            '                            R.Item(Col.ColumnName) = _OrderBal

            '                        Case Else


            '                            'R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

            '                    End Select
            '                Next
            '            End If

            '        Next


            '        Me.ogcratio.DataSource = _dt


            '        ' Next


            '    End If


            'End With

#End Region

        Catch ex As Exception
            MsgBox("n" & ex.Message)
        End Try
    End Sub


    Private Sub Calsratio(ogv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView, ogc As DevExpress.XtraGrid.GridControl)
        Try
            'คีย์ layer cals  Ratio
            With ogv
                If .FocusedRowHandle < 0 Or .RowCount < 0 Then Exit Sub

                Dim ratiofocus As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNLayerQty").ToString())
                If ratiofocus <= 0 Then Exit Sub
                'Dim breakdown As Integer = Val(.GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn).ToString())
                Dim MarkSeq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "MarkSeq").ToString())
                Dim Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString())



                Dim valuex As Double = ratiofocus


                Dim _dt As DataTable
                With DirectCast(ogc.DataSource, DataTable)
                    .AcceptChanges()

                    _dt = .Copy

                End With


                If Not (_dt Is Nothing) Then
                    Dim _StyleCodeOld As String = ""
                    Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    Dim colwith As Integer = 0
                    Dim _OrderQty As Integer = 0

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 1 Then


                            Dim _odtbd As DataTable

                            If MarkSeq = 1 Then
                                _odtbd = _dt.Select("FNSeq=0").CopyToDataTable
                            Else
                                _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1).CopyToDataTable
                            End If
                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                    Case Else


                                        R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex
                                        _OrderQty += +Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex


                                End Select
                            Next
                        End If

                    Next
                    _dt.AcceptChanges()

                    Dim totalsolid As Integer = 0

                    If (Me.FNMaxLayers.Value < valuex) Then
                        totalsolid = Math.Floor(valuex / Me.FNMaxLayers.Value)
                        valuex = Me.FNMaxLayers.Value * totalsolid

                    End If

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 1 Then

                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "Total".ToUpper, "FNHSysStyleId_Hide".ToUpper

                                    Case "FNOrderQty".ToUpper
                                        R.Item(Col.ColumnName) = 0
                                    Case "FNLayerQty".ToUpper
                                        'R.Item(Col.ColumnName) = valuex
                                    Case "FNQuantity".ToUpper
                                        R.Item(Col.ColumnName) = _OrderQty
                                    Case "FNTotalQty".ToUpper
                                        R.Item(Col.ColumnName) = valuex * _OrderQty
                                    Case "FNTableQty".ToUpper
                                        R.Item(Col.ColumnName) = totalsolid
                                    Case "FNTotalYardPerLayer".ToUpper

                                        R.Item(Col.ColumnName) = _OrderQty * Me.FNConsAvg.Value
                                    Case Else


                                        ' R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) / valuex

                                End Select
                            Next
                        End If

                    Next

                    Dim _OrderBal As Integer = 0
                    _dt.AcceptChanges()

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 3 Then
                            Dim _odtbd As DataTable
                            If MarkSeq = 1 Then
                                _odtbd = _dt.Select("FNSeq<>3", "FNSeq asc").CopyToDataTable
                            Else
                                _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1, "FNSeq asc").CopyToDataTable
                                _odtbd.Merge(_dt.Select("FNSeq<>3 and MarkSeq=" & MarkSeq, "FNSeq asc").CopyToDataTable)
                            End If

                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper


                                    Case Else

                                        _OrderBal += +(Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex))
                                        R.Item(Col.ColumnName) = (Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex)) 'Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                End Select
                            Next
                        End If

                    Next

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 3 Then

                            For Each Col As DataColumn In _dt.Columns

                                Select Case Col.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                    Case "FNOrderQty".ToUpper
                                        R.Item(Col.ColumnName) = _OrderBal

                                    Case Else


                                        'R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                End Select
                            Next
                        End If

                    Next


                    ogc.DataSource = _dt


                    ' Next


                End If


            End With

            ogv.BestFitColumns()

        Catch ex As Exception
            MsgBox("n" & ex.Message.ToString)
        End Try
    End Sub

    Private Sub ocmdelmark_Click(sender As Object, e As EventArgs) Handles ocmdelmark.Click
        Try



            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                        _GridView = _Grid.MainView


                        With _GridView
                            If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub
                            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, " ลบมาร์ค", Me.Text) = False Then Exit Sub
                            Dim _odt As DataTable
                            Dim _FNHSysMarkId As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysMarkId_Hide").ToString())
                            'Dim breakdown As Integer = Val(.GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn).ToString())
                            Dim MarkSeq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "MarkSeq").ToString())
                            Dim Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString())
                            With DirectCast(_Grid.DataSource, DataTable)
                                .AcceptChanges()
                                _odt = .Select("MarkSeq<>" & MarkSeq).CopyToDataTable
                                .AcceptChanges()
                            End With
                            _Grid.DataSource = _odt
                        End With
                End Select
            Next


            Dim _Cmd As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction





            _Cmd = "delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio_D "
            _Cmd &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Exit Sub
            End If




            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)




            Call SaveData()

        Catch ex As Exception
        End Try
    End Sub


    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMURatio WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            _Str &= vbCrLf & " and  FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMURatio_D WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            _Str &= vbCrLf & " and  FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.FTDocumentNo.Text = "" Or Me.FTGroupNo.Text = "" Then

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.LabelControl1.Text)
                Exit Sub
            End If
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = False Then
                Exit Sub
            End If
            If (CheckUse()) Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถลบข้อมูลได้ เนื่องจาก มีทำขั้นตอนถัดไปแล้ว !!", 2204291049, Me.Text)
                Exit Sub
            End If

            If DeleteData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckUse() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT top 1  FTGroupNo, FTDocumentNo "
            _Cmd &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMUActualPlan with(nolock) "
            _Cmd &= vbCrLf & " where FTGroupNo='" & Me.FTGroupNo.Text & "'"
            _Cmd &= vbCrLf & " and  FTDocumentNo='" & Me.FTDocumentNo.Text & "'"



            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckSelectPart() As Boolean
        Try
            Dim _Return As Boolean = False
            With DirectCast(Me.ogcpart.DataSource, DataTable)
                .AcceptChanges()
                _Return = .Select("FTSelect='1'").Length > 0

            End With

            Return _Return
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub RepositoryItemCalcEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcEdit1.EditValueChanging
        Try

            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                        _GridView = _Grid.MainView

                        With _GridView
                            If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub

                            'If Not CheckSelectPart() Then
                            '    HI.MG.ShowMsg.mInfo("กรุณาเลือก PartCode", 2202241517, Me.Text)
                            '    e.Cancel = True
                            '    Exit Sub
                            'End If
                            Dim _FileNameKey As String = .FocusedColumn.FieldName
                            Dim _Seq As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNSeq"))
                            Dim _markSeq As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "MarkSeq"))
                            Dim Total As Integer = 0
                            .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, Val(e.NewValue))


                            If _FileNameKey = "FNLayerQty" Then

                                'Dim _Qty As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity"))
                                'Dim _Tal As Integer = _Qty * Val(e.NewValue)

                                '.SetRowCellValue("0" & .FocusedRowHandle, "FNTotalQty", _Tal)

                                ''.SetRowCellValue(.FocusedRowHandle, "FNTotalQty", _Tal)

                                'Dim _focusRow As Integer = .FocusedRowHandle

                                'Call CalsAll(_markSeq, Val(e.NewValue))

                                '.FocusedRowHandle = _focusRow

                                ''Call CalsAll(_markSeq, Val(e.NewValue))




                            Else
                                For Each d As GridColumn In .Columns

                                    Select Case d.FieldName.ToUpper

                                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                    "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                 "FNHSysStyleId_Hide".ToUpper
                                        Case Else

                                            Total += +Val("0" & .GetRowCellValue(.FocusedRowHandle, d.FieldName.ToString))


                                    End Select
                                Next
                                Dim _Qty As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLayerQty"))
                                .SetRowCellValue(.FocusedRowHandle, "FNQuantity", Total)
                                .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", Total * _Qty)
                                Dim _BdQty As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn.FieldName.ToString))
                                .SetRowCellValue("0" & .FocusedRowHandle + 1, .FocusedColumn.FieldName.ToString, _BdQty - (_Qty * Val(e.NewValue)))

                            End If



                        End With



                        Exit Select
                End Select
            Next


        Catch ex As Exception

        End Try
    End Sub

    Private Sub AdvBandedGridView2_ShowingEditor(sender As Object, e As CancelEventArgs) Handles AdvBandedGridView2.ShowingEditor
        Try

            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                        _GridView = _Grid.MainView

                        With _GridView
                            If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub

                            Dim _Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq"))
                            If _Seq = 0 Or _Seq = 3 Then
                                e.Cancel = True
                            End If

                        End With
                End Select
            Next


        Catch ex As Exception

        End Try
    End Sub


    Private Sub CalsAll(MarkSeq As Integer, Layer As Integer)
        Try
            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                        _GridView = _Grid.MainView


                        With _GridView
                            If .FocusedRowHandle < 0 Or .RowCount < 0 Then Exit Sub

                            Dim ratiofocus As Integer = Val(.GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString())
                            Dim breakdown As Integer = Val(.GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn).ToString())

                            'Dim MarkSeq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "MarkSeq").ToString())
                            'Dim Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString())


                            Dim valuex As Double = Layer  ' breakdown \ ratiofocus


                            Dim _dt As DataTable
                            With DirectCast(_Grid.DataSource, DataTable)
                                .AcceptChanges()

                                _dt = .Copy

                            End With


                            If Not (_dt Is Nothing) Then
                                Dim _StyleCodeOld As String = ""
                                Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                Dim colwith As Integer = 0
                                Dim _OrderQty As Integer = 0

                                For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                                    If R!FNSeq = 1 Then


                                        Dim _odtbd As DataTable

                                        'If MarkSeq = 1 Then
                                        '    _odtbd = _dt.Select("FNSeq=0").CopyToDataTable
                                        'Else
                                        '    _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1).CopyToDataTable
                                        'End If
                                        For Each Col As DataColumn In _dt.Columns

                                            Select Case Col.ColumnName.ToString.ToUpper

                                                Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                                Case Else


                                                    ' R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex
                                                    _OrderQty += +Val("0" & R.Item(Col.ColumnName).ToString) '\ valuex


                                            End Select
                                        Next
                                    End If

                                Next


                                _dt.AcceptChanges()
                                Dim totalsolid As Integer = 0

                                If (Me.FNMaxLayers.Value < valuex) Then
                                    totalsolid = Math.Floor(valuex / Me.FNMaxLayers.Value)
                                    valuex = Me.FNMaxLayers.Value * totalsolid

                                End If
                                For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                                    If R!FNSeq = 1 Then

                                        For Each Col As DataColumn In _dt.Columns

                                            Select Case Col.ColumnName.ToString.ToUpper

                                                Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "Total".ToUpper, "FNHSysStyleId_Hide".ToUpper

                                                Case "FNOrderQty".ToUpper
                                                    R.Item(Col.ColumnName) = 0
                                                Case "FNLayerQty".ToUpper
                                                    R.Item(Col.ColumnName) = valuex
                                                Case "FNQuantity".ToUpper
                                                    R.Item(Col.ColumnName) = _OrderQty
                                                Case "FNTotalQty".ToUpper
                                                    R.Item(Col.ColumnName) = valuex * _OrderQty

                                                Case "FNTableQty".ToUpper
                                                    R.Item(Col.ColumnName) = totalsolid
                                                Case "FNTotalYardPerLayer".ToUpper
                                                    R.Item(Col.ColumnName) = _OrderQty * Me.FNConsAvg.Value
                                                Case Else

                                                    ' R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) / valuex

                                            End Select
                                        Next
                                    End If

                                Next

                                Dim _OrderBal As Integer = 0
                                _dt.AcceptChanges()

                                For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                                    If R!FNSeq = 3 Then
                                        Dim _odtbd As DataTable
                                        If MarkSeq = 1 Then
                                            _odtbd = _dt.Select("FNSeq<>3", "FNSeq asc").CopyToDataTable
                                        Else
                                            _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1, "FNSeq asc").CopyToDataTable
                                            _odtbd.Merge(_dt.Select("FNSeq<>3 and MarkSeq=" & MarkSeq, "FNSeq asc").CopyToDataTable)
                                        End If

                                        For Each Col As DataColumn In _dt.Columns

                                            Select Case Col.ColumnName.ToString.ToUpper

                                                Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTotalYardPerLayer".ToUpper, "FNTableQty".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper


                                                Case Else

                                                    _OrderBal += +(Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex))
                                                    R.Item(Col.ColumnName) = (Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex)) 'Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                            End Select
                                        Next
                                    End If

                                Next

                                For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                                    If R!FNSeq = 3 Then

                                        For Each Col As DataColumn In _dt.Columns

                                            Select Case Col.ColumnName.ToString.ToUpper

                                                Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                                Case "FNOrderQty".ToUpper
                                                    R.Item(Col.ColumnName) = _OrderBal

                                                Case Else


                                                    'R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                            End Select
                                        Next
                                    End If

                                Next


                                _Grid.DataSource = _dt


                                ' Next


                            End If


                        End With

                End Select
            Next


        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCalcEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCalcEdit1.EditValueChanged
        Try


            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                        _GridView = _Grid.MainView

                        With _GridView
                            If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub

                            Dim _FileNameKey As String = .FocusedColumn.FieldName
                            Dim _Seq As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNSeq"))
                            Dim _markSeq As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "MarkSeq"))
                            Dim Total As Integer = 0
                            '.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, Val(e.NewValue))


                            If _FileNameKey = "FNLayerQty" Then
                                Dim _value As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLayerQty"))
                                Dim _Qty As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity"))
                                Dim _Tal As Integer = _Qty * Val(_value)

                                .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", _Tal)

                                Dim _focusRow As Integer = .FocusedRowHandle

                                Call CalsAll(_markSeq, Val(_value))

                                .FocusedRowHandle = _focusRow



                            Else
                                For Each d As GridColumn In .Columns

                                    Select Case d.FieldName.ToUpper

                                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                    "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                 "FNHSysStyleId_Hide".ToUpper
                                        Case Else

                                            Total += +Val("0" & .GetRowCellValue(.FocusedRowHandle, d.FieldName.ToString))


                                    End Select
                                Next
                                Dim _Qty As Integer = Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLayerQty"))
                                .SetRowCellValue(.FocusedRowHandle, "FNQuantity", Total)
                                .SetRowCellValue(.FocusedRowHandle, "FNTotalQty", Total * _Qty)



                            End If



                        End With
                End Select
            Next



        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTGroupNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTDocumentNo.EditValueChanged, FTGroupNo.EditValueChanged
        Me.loadpart()
    End Sub


    'Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
    '    Try
    '        Dim _checkCode As String = e.NewValue
    '        Dim _PartCode As String = ""
    '        With Me.GridView1
    '            If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub

    '            .SetRowCellValue(.FocusedRowHandle, "FTSelect", e.NewValue)




    '            DirectCast(Me.ogcpart.DataSource, DataTable).AcceptChanges()
    '        End With

    '        With DirectCast(Me.ogcpart.DataSource, DataTable)
    '            .AcceptChanges()
    '            For Each r As DataRow In .Select("FTSelect='1'")
    '                _PartCode &= r!FTPartCode.ToString
    '            Next
    '        End With

    '        Dim _RawInd As Integer = 0
    '        With DirectCast(Me.ogcratio.DataSource, DataTable)
    '            .AcceptChanges()
    '            For Each r As DataRow In .Select("FNSeq=1", "MarkSeq asc ")
    '                _RawInd += +1
    '                If _PartCode = "" Then
    '                    r!FNHSysMarkId = ""
    '                Else
    '                    r!FNHSysMarkId = _PartCode & Microsoft.VisualBasic.Right("00" & _RawInd, 3)
    '                End If
    '            Next
    '            .AcceptChanges()
    '        End With







    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            Dim _dt As New DataTable
            Dim _Qry As String = ""
            If Me.FTDocumentNo.Text <> "" And Me.FTGroupNo.Text <> "" Then
                _Qry = "select  distinct '0' as FTSelect  , FTPartCode   , FTPartName  from  TPRODMUGroupPlan S with(nolock)  "
                _Qry &= vbCrLf & "where FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'  and FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                _Qry &= vbCrLf & "Order by  FTPartCode asc "

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                With _wPopUpAddPartCode
                    .ogcpart.DataSource = _dt
                    .ShowDialog()
                    If (.Poss) Then

                        Dim _PartCode As String = ""
                        With DirectCast(.ogcpart.DataSource, DataTable)
                            .AcceptChanges()
                            For Each r As DataRow In .Select("FTSelect='1'")
                                _PartCode &= r!FTPartCode.ToString
                            Next
                        End With



                        genNewTabData(Me.FTDocumentNo.Text, Me.FTGroupNo.Text, _PartCode)
                    End If

                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub ogvpackdetail_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvpackdetail.RowCellStyle
        Try
            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                If e.RowHandle <> .FocusedRowHandle OrElse e.Column.AbsoluteIndex = .FocusedColumn.AbsoluteIndex Then
                    If (e.RowHandle Mod 2 = 1) Then
                        e.Appearance.BackColor = System.Drawing.Color.LightSkyBlue
                    Else
                        e.Appearance.BackColor = System.Drawing.Color.White
                    End If
                End If
            End With



        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try

            If VerifyData() = False Then Exit Sub

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, , Me.Text) = False Then
                Exit Sub
            End If

            Dim _Cmd As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction




            _Cmd = "delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio "
            _Cmd &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Exit Sub
            End If


            _Cmd = "delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio_D "
            _Cmd &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Exit Sub
            End If




            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Me.xtabpart.TabPages.Remove(Me.xtabpart.SelectedTabPage)
            Call SaveData()



        Catch ex As Exception
            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            'Exit Sub
        End Try
    End Sub

    Private Sub xtabpart_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles xtabpart.SelectedPageChanged
        For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.GridControl
                    Dim _Grid As DevExpress.XtraGrid.GridControl
                    _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                    Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                    _GridView = _Grid.MainView

                    With DirectCast(_Grid.MainView, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                        .BestFitColumns()
                    End With
                Case Else
            End Select
        Next

    End Sub


    Private Function validatedata() As Boolean
        Try
            If Me.FNMaxLayers.Value <= 0 Then
                Me.FNMaxLayers.Focus()
                Return False
            End If

            If Me.FNConsAvg.Value <= 0.00 Then
                Me.FNConsAvg.Focus()
                Return False
            End If


            If Me.FNMinYard.Value <= 0.00 Then
                Me.FNMinYard.Focus()
                Return False
            End If
            If Me.FNMaxYard.Value <= 0.00 Then
                Me.FNMaxYard.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmcalsratioauto_Click(sender As Object, e As EventArgs) Handles ocmcalsratioauto.Click

        If Not validatedata() Then
            Exit Sub
        End If

        Dim _Spls As New HI.TL.SplashScreen("Calculating  data. Please wait a moment. ")
        Try

            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        Dim _Grid As DevExpress.XtraGrid.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                        With DirectCast(_Grid.DataSource, DataTable)

                            .AcceptChanges()
                            _Grid.DataSource = .Select("MarkSeq=1").CopyToDataTable
                        End With
                End Select
            Next




            Dim ref As Boolean = False
            Dim ref2 As Boolean = False
            Dim MarkSeq As Integer = 1
            Dim Seq As Integer = 1
            Dim lost As Integer = 0
            Dim div As Integer = 0

            Dim _maxloop As Integer = 100
5:
            ref = False
            If (lost >= 99) Then
                lost = 0
                div += +1
            End If

            For i As Integer = 1 To _maxloop Step 1
                For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.GridControl
                            Dim _Grid As DevExpress.XtraGrid.GridControl
                            _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                            Calslayeruto(_Grid.MainView, _Grid, i, MarkSeq, Seq, ref, ref2, lost, div)

                            If (ref2) Then
                                GoTo 9
                            End If

                            If (ref) Then
                                addnewmark()
                                MarkSeq += +1
                                GoTo 5


                            End If

                            If (lost >= 99) Then

                                GoTo 5
                            End If

                            Exit Select
                    End Select
                Next


            Next
9:

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub


    Private Sub Calslayeruto(ogv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView, ogc As DevExpress.XtraGrid.GridControl,
                             i As Integer, MarkSeq As Integer, Seq As Integer, ByRef ref As Boolean, ByRef ref2 As Boolean, ByRef lost As Integer, ByRef div As Integer)

        Dim _maxloop As Integer = 1000
        Dim _maxLayer As Integer = Me.FNMaxLayers.Value
        Dim _maxCons As Double = Me.FNConsAvg.Value
        Dim _maxyard As Double = Me.FNMaxYard.Value
        Dim _minyard As Double = Me.FNMinYard.Value

        Dim _stateUse As Boolean = False


        Try



            With ogv
                If .FocusedRowHandle < 0 Or .RowCount < 0 Then Exit Sub

                Dim ratiofocus As Integer = i ' Val(.GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString())
                Dim breakdown As Integer = 1 ' Val(.GetRowCellValue(.FocusedRowHandle - 1, .FocusedColumn).ToString())

                'Dim MarkSeq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "MarkSeq").ToString())
                ' Dim Seq As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString())


                Dim valuex As Double = breakdown \ ratiofocus


                Dim _dt As DataTable
                With DirectCast(ogc.DataSource, DataTable)
                    .AcceptChanges()

                    _dt = .Copy

                End With


                If Not (_dt Is Nothing) Then
                    Dim _StyleCodeOld As String = ""
                    Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    Dim colwith As Integer = 0
                    Dim _OrderQty As Integer = 0

                    For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                        If R!FNSeq = 1 Then


                            Dim _odtbd As DataTable

                            If MarkSeq = 1 Then
                                _odtbd = _dt.Select("FNSeq=0").CopyToDataTable
                            Else
                                _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1).CopyToDataTable
                            End If
                            Dim _stateassort As Boolean = False
                            Dim _ttassort As Integer = 0
                            For Each Colx As DataColumn In _dt.Columns

                                Select Case Colx.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                    Case Else

                                        _ttassort += +_odtbd.Rows(0).Item(Colx.ColumnName)
                                End Select

                            Next

                            If (_ttassort * _maxCons) <= 11 Then
                                _stateassort = True
                            End If

                            For Each Colx As DataColumn In _dt.Columns

                                Select Case Colx.ColumnName.ToString.ToUpper

                                    Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper
                                    Case Else

                                        Try
                                            breakdown = _odtbd.Rows(0).Item(Colx.ColumnName)
                                        Catch ex As Exception
                                            breakdown = 1
                                        End Try


                                        valuex = breakdown \ ratiofocus
                                        _OrderQty = 0
                                        If valuex = 0 Then
                                            valuex = 1
                                        End If

                                        For Each Col As DataColumn In _dt.Columns
                                            Select Case Col.ColumnName.ToString.ToUpper

                                                Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                                        "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                                     "FNHSysStyleId_Hide".ToUpper
                                                Case Else

                                                    If (_stateassort) Then
                                                        R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString)
                                                        _OrderQty += +Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString)
                                                    Else
                                                        R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex
                                                        _OrderQty += +Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) \ valuex
                                                    End If




                                            End Select

                                        Next
                                        If (_OrderQty * _maxCons) > (Me.FNMinYard.Value - div) And (_OrderQty * _maxCons) <= (Me.FNMaxYard.Value) Then
                                            _stateUse = True
                                            GoTo 9
                                        Else
                                            If (_stateassort) Then
                                                _stateUse = True
                                                GoTo 9
                                            End If

                                        End If

                                End Select



                            Next
                        End If

                    Next
                    _dt.AcceptChanges()

9:
                    Dim totalsolid As Integer = 0
                    If (_stateUse) Then
                        If (_maxLayer < valuex) Then
                            totalsolid = Math.Floor(valuex / _maxLayer)
                            valuex = _maxLayer * totalsolid
                        Else
                            totalsolid = 1
                        End If



                        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                            If R!FNSeq = 1 Then

                                For Each Col As DataColumn In _dt.Columns

                                    Select Case Col.ColumnName.ToString.ToUpper

                                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                                "Total".ToUpper, "FNHSysStyleId_Hide".ToUpper

                                        Case "FNOrderQty".ToUpper
                                            R.Item(Col.ColumnName) = 0
                                        Case "FNLayerQty".ToUpper
                                            R.Item(Col.ColumnName) = valuex
                                        Case "FNQuantity".ToUpper
                                            R.Item(Col.ColumnName) = _OrderQty
                                        Case "FNTotalQty".ToUpper
                                            R.Item(Col.ColumnName) = valuex * _OrderQty

                                        Case "FNTableQty".ToUpper
                                            R.Item(Col.ColumnName) = totalsolid
                                        Case "FNTotalYardPerLayer".ToUpper
                                            R.Item(Col.ColumnName) = _OrderQty * Me.FNConsAvg.Value
                                        Case Else


                                            ' R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) / valuex

                                    End Select
                                Next
                            End If

                        Next

                        Dim _OrderBal As Integer = 0
                        _dt.AcceptChanges()

                        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                            If R!FNSeq = 3 Then
                                Dim _odtbd As DataTable
                                If MarkSeq = 1 Then
                                    _odtbd = _dt.Select("FNSeq<>3", "FNSeq asc").CopyToDataTable
                                Else
                                    _odtbd = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq - 1, "FNSeq asc").CopyToDataTable
                                    _odtbd.Merge(_dt.Select("FNSeq<>3 and MarkSeq=" & MarkSeq, "FNSeq asc").CopyToDataTable)
                                End If

                                For Each Col As DataColumn In _dt.Columns

                                    Select Case Col.ColumnName.ToString.ToUpper

                                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                                "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                             "FNHSysStyleId_Hide".ToUpper


                                        Case Else

                                            _OrderBal += +(Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex))
                                            R.Item(Col.ColumnName) = (Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - (Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString) * valuex)) 'Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                    End Select
                                Next
                            End If

                        Next

                        For Each R As DataRow In _dt.Select("MarkSeq=" & MarkSeq)
                            If R!FNSeq = 3 Then

                                For Each Col As DataColumn In _dt.Columns

                                    Select Case Col.ColumnName.ToString.ToUpper

                                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                                "FNTotalQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                             "FNHSysStyleId_Hide".ToUpper
                                        Case "FNOrderQty".ToUpper
                                            R.Item(Col.ColumnName) = _OrderBal

                                        Case Else


                                            'R.Item(Col.ColumnName) = Val(_odtbd.Rows(0).Item(Col.ColumnName).ToString) - Val(_odtbd.Rows(1).Item(Col.ColumnName).ToString)

                                    End Select
                                Next
                            End If

                        Next


                        ogc.DataSource = _dt
                        ref = True

                        Dim _odtbdx As DataTable
                        If MarkSeq = 1 Then
                            _odtbdx = _dt.Select("FNSeq=3").CopyToDataTable
                        Else
                            _odtbdx = _dt.Select("FNSeq=3 and MarkSeq=" & MarkSeq).CopyToDataTable
                        End If
                        Dim _stateassort As Boolean = False
                        Dim _ttassort As Integer = 0
                        For Each Colx As DataColumn In _dt.Columns

                            Select Case Colx.ColumnName.ToString.ToUpper

                                Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                        "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper, "FNTableQty".ToUpper, "FNTotalYardPerLayer".ToUpper,
                                     "FNHSysStyleId_Hide".ToUpper
                                Case Else

                                    _ttassort += +_odtbdx.Rows(0).Item(Colx.ColumnName)
                            End Select

                        Next

                        If _ttassort <= 0 Then

                            ref2 = True
                        End If



                        ' Next
                    Else
                        lost += +1
                    End If

                End If


            End With

        Catch ex As Exception

        End Try
    End Sub


End Class
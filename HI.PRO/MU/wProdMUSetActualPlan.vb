Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Windows.Forms

Public Class wProdMUSetActualPlan

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _wAddGenProdJob As wAddGenProdJob

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _wAddGenProdJob = New wAddGenProdJob
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wAddGenProdJob.Name.ToString.Trim, _wAddGenProdJob)
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



        With Me.AdvBandedGridView2
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With



        ogcratio.DataSource = Nothing


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

    Private Sub LoadDataInfo(Key As Object)
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1  FTCfgData   from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig  where FTCfgName='TableCutMarkSpare'"

            Me.FNToralent.Value = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, 0)
            Dim _OptiplanYardsTotal As Double = 0
            Dim _OptiplanYardsOtherTotal As Double = 0
            _Cmd = "SELECT  sum(FNOptiplanYards ) as FNOptiplanYards   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan with(nolock)  FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
            _Cmd &= vbCrLf & " and FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
            _OptiplanYardsTotal = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, 0)

            _Cmd = "SELECT  sum(FNOptiplanYards ) as FNOptiplanYards   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan with(nolock)  FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
            _Cmd &= vbCrLf & " and FTDocumentNo <> '" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
            _OptiplanYardsOtherTotal = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, 0)

            Me.FNOptiplanYard.Value = _OptiplanYardsTotal - _OptiplanYardsOtherTotal

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadOrderPackBreakDown()
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_MU_OrderBreakDown  @FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
        _Qry &= vbCrLf & " , @FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        With Me.AdvBandedGridView2

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

        Me.ogcratio.DataSource = _dt.Copy

    End Sub



    'Public Sub loadpart()
    '    Dim _Qry As String = ""
    '    Dim _dt As DataTable
    '    If Me.FTDocumentNo.Text <> "" And Me.FTGroupNo.Text <> "" Then
    '        Dim _ds As New DataSet




    '        _Qry = "select  distinct '0' as FTSelect  , FTPartCode   from  TPRODMUGroupPlan S with(nolock)  "
    '        _Qry &= vbCrLf & "where FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'  and FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"






    '        _Qry &= vbCrLf & "Select    FNSeq  , FTPartCode  from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio with(nolock) "
    '        _Qry &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
    '        _Qry &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
    '        _Qry &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
    '        _Qry &= vbCrLf & " and   FNSeq =1 "
    '        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)


    '        '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '        Me.ogcpart.DataSource = _ds.Tables(0).Copy
    '        With DirectCast(Me.ogcpart.DataSource, DataTable)
    '            .AcceptChanges()

    '            For Each R As DataRow In .Rows
    '                For Each x As DataRow In _ds.Tables(1).Select("FTPartCode='" & R!FTPartCode.ToString & "'")
    '                    R!FTSelect = "1"
    '                Next
    '            Next


    '        End With

    '        LoadData(Me.FTDocumentNo.Text, Me.FTGroupNo.Text)
    '    End If



    'End Sub


    'Public Sub LoadData(ByVal Key As String, ByVal Key2 As String)
    '    Me.FTDocumentNo.Text = Key
    '    Me.FTGroupNo.Text = Key2

    '    Dim _StartDate As String = ""
    '    Dim _EndDate As String = ""
    '    Dim _Qry As String = ""
    '    Dim _dt As DataTable
    '    Dim _TotalRow As Integer = 0
    '    Dim _Rx As Integer = 0
    '    Dim _ds As New DataSet
    '    Dim _dt2 As DataTable
    '    Dim _dtpart As DataTable

    '    StateCal = False

    '    Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


    '    Try


    '        Dim _PartCode As String = ""


    '        _Qry &= vbCrLf & "Select   distinct FNSeq  ,    "
    '        _Qry &= vbCrLf & "  STUFF ( (SELECT   distinct ',' +FTPartCode   "
    '        _Qry &= vbCrLf & " from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio b with(nolock) "
    '        _Qry &= vbCrLf & "where a.FTDocumentNo = b.FTDocumentNo"
    '        _Qry &= vbCrLf & "and a.FTGroupNo = b.FTGroupNo"
    '        _Qry &= vbCrLf & "and a.FNSeq = b.FNSeq"
    '        _Qry &= vbCrLf & " FOR XML PATH('')), 1, 1, ''"
    '        _Qry &= vbCrLf & ") FTPartCode"
    '        _Qry &= vbCrLf & ""
    '        _Qry &= vbCrLf & " from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMURatio a with(nolock) "
    '        _Qry &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
    '        _Qry &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
    '        _Qry &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
    '        _Qry &= vbCrLf & " order by FNSeq asc "
    '        _dtpart = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '        If _dtpart.Rows.Count > 0 Then

    '            Me.xtabpart.TabPages.Clear()
    '            Me.xtabpart.TabPages.Add(Me.XtraTabPage1)

    '            For Each x As DevExpress.XtraTab.XtraTabPage In Me.xtabpart.TabPages

    '                If x.Name = "XtraTabPage1" Then

    '                    x.PageEnabled = False
    '                    x.PageVisible = False
    '                Else
    '                    Try
    '                        Me.xtabpart.TabPages.Remove(x)
    '                    Catch ex As Exception

    '                    End Try

    '                End If

    '            Next


    '            For Each R As DataRow In _dtpart.Rows
    '                _PartCode = R!FTPartCode.ToString

    '                _Qry = "Exec  dbo.SP_GET_MUGroupPlanForRatio_Save  @FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' , @FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'  , @PartCode='" & HI.UL.ULF.rpQuoted(_PartCode) & "'  , @Seq=" & Val(R!FNSeq)
    '                Me.ogcratio.DataSource = Nothing
    '                _ds = New DataSet


    '                HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)
    '                If _ds.Tables.Count <= 1 Then
    '                    Continue For
    '                Else
    '                    _dt = _ds.Tables(0)
    '                    _dt2 = _ds.Tables(1)
    '                End If

    '                Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
    '                Dim _Grid As New DevExpress.XtraGrid.GridControl
    '                Dim _GridV As New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView

    '                With _TabPage
    '                    .Name = "otbx" & _PartCode
    '                    .Text = _PartCode
    '                    .Tag = "2|"


    '                End With

    '                With _Grid
    '                    .Name = "ogcGSum" & _PartCode
    '                    .Tag = "2|"

    '                    .Dock = System.Windows.Forms.DockStyle.Fill
    '                    .EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
    '                    .Location = New System.Drawing.Point(0, 0)
    '                    .Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
    '                    .RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2, Me.RepositoryItemButtonEdit1, Me.RepositoryItemCalcEdit1})
    '                    .Size = New System.Drawing.Size(1116, 473)
    '                    .TabIndex = 2
    '                    .TabStop = False

    '                End With





    '                '_GridV = Me.AdvBandedGridView2


    '                _GridV = _gridCtl(_PartCode, _Grid)




    '                _Grid.BeginInit()
    '                _Grid.MainView = _GridV
    '                _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
    '                _Grid.EndInit()
    '                _TabPage.Controls.Add(_Grid)
    '                _Grid.Dock = DockStyle.Fill






    '                InitGridRatioDynamic(_dt, _dt2, _GridV, _Grid)
    '                'Me.ogcratio.DataSource = _dt

    '                'InitGridRatio(_dt, _dt2)



    '                HI.TL.HandlerControl.AddHandlerObj(_TabPage)

    '                Me.xtabpart.TabPages.Add(_TabPage)

    '            Next





    '        Else
    '            Me.xtabpart.TabPages.Clear()
    '            Me.xtabpart.TabPages.Add(Me.XtraTabPage1)

    '            For Each x As DevExpress.XtraTab.XtraTabPage In Me.xtabpart.TabPages

    '                If x.Name = "XtraTabPage1" Then

    '                    x.PageEnabled = False
    '                    x.PageVisible = False
    '                Else
    '                    Try
    '                        Me.xtabpart.TabPages.Remove(x)
    '                    Catch ex As Exception

    '                    End Try

    '                End If

    '            Next


    '        End If

    '        Call LoadOrderPackBreakDown()


    '    Catch ex As Exception
    '    End Try

    '    _Spls.Close()
    '    _RowDataChange = False

    'End Sub



    Private Sub LoadData()


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


            Dim _PartCode As String = ""


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

            Me.xtabpart.TabPages.Clear()
            Me.xtabpart.TabPages.Add(Me.XtraTabPage1)

            For Each R As DataRow In _dtpart.Rows
                _PartCode = R!FTPartCode.ToString

                _Qry = "Exec  dbo.[SP_GET_MUGroupPlanSetActual]  @FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' , @FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                _Qry &= vbCrLf & "  , @PartCode='" & HI.UL.ULF.rpQuoted(_PartCode) & "'  , @Seq=" & Val(R!FNSeq)
                '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                ' Me.ogcpackdetail.DataSource = _dt
                _ds = New DataSet


                HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PROD, _ds)
                _dt = _ds.Tables(0)
                _dt2 = _ds.Tables(1)



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





                ' InitGridRatio(_dt, _dt2)
                InitGridRatio(_dt, _dt2, _GridV, _Grid)
                'Me.ogcratio.DataSource = _dt

                'InitGridRatio(_dt, _dt2)


                HI.TL.HandlerControl.AddHandlerObj(_TabPage)
                HI.ST.Lang.SP_SETxLanguage(Me)

                Me.xtabpart.TabPages.Add(_TabPage)
                initgridBest()



                _Qry = "Select top 1  FTCfgData   from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig  where FTCfgName='TableCutMarkSpare'"

                Me.FNToralent.Value = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, 0)

                '_Qry = "SELECT  sum(FNOptiplanYards ) as FNOptiplanYards   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan with(nolock)  where FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
                '_Qry &= vbCrLf & " and FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
                'Me.FNOptiplanYard.Value = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, 0)

                Dim _OptiplanYardsTotal As Double = 0
                Dim _OptiplanYardsOtherTotal As Double = 0
                Dim _OrderNo As String = "" : Dim _SubOrderNo As String = "" : Dim _Colorway As String = "" : Dim _RawMatColor As String = ""
                Dim _Mark As String = ""
                _Qry = "SELECT  distinct  (FNOptiplanYards ) as FNOptiplanYards , FTOrderNo ,   FTColorWay , FTColorCode , FTRawMatCode ,  FTPartCode FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan with(nolock)  where FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
                _Qry &= vbCrLf & " and FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
                Dim _oxDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY, 0)

                For Each Rx As DataRow In _oxDt.Rows
                    _OptiplanYardsTotal += Val(Rx!FNOptiplanYards.ToString)
                    _OrderNo = Rx!FTOrderNo.ToString
                    '_SubOrderNo = Rx!FTSubOrderNo.ToString
                    _Colorway = Rx!FTColorWay.ToString


                Next
                '_OptiplanYardsTotal = 0

                _Qry = "SELECT  max(FNOptiplanYards ) as FNOptiplanYards   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan with(nolock)  where FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
                _Qry &= vbCrLf & " and FTDocumentNo <> '" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
                _OptiplanYardsOtherTotal = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, 0)

                Me.FNOptiplanYard.Value = _OptiplanYardsTotal '- _OptiplanYardsOtherTotal



                Me.FNOptiplanBal.Value = Me.FNOptiplanYard.Value - Val("0" & _dt.Compute("Sum(FNQuantityUse)", "").ToString())




            Next


            '_Qry = "select  distinct '0' as FTSelect  , FTPartCode   from  TPRODMUGroupPlan S with(nolock)  "
            '_Qry &= vbCrLf & "where FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'  and FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            'Me.ogcpart.DataSource = _dt


            '_Qry = "Exec  dbo.SP_GET_MUGroupPlan  @CmpCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpId.Text) & "' , @BuyGroupCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyId.Text) & "' , @FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            '_Qry &= vbCrLf & " ,@FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' , @FTOrderNoTo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' , @FTCustomerPO='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "' , @FTCustomerPOTo ='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPOTo.Text) & "'"
            '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            ' Call LoaddataDetailColorSize()

        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub


    Private Sub initgridBest()
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



                End Select
            Next



        Catch ex As Exception

        End Try
    End Sub


    Private Function _gridCtl(_PartCode As String, _Grid As DevExpress.XtraGrid.GridControl) As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
        Try



            Dim _gBMark As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            Dim _gBTotal As New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            Dim _gbyard As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
            Dim _GridV As New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
            Dim _BandedGridColumn17 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn18 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn19 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn20 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn7 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn8 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn9 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn10 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _cFNActuallong As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn11 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn12 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _BandedGridColumn13 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim cFNEfficency2 As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            Dim _gridBand3 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand


            'Dim _st As New HI.ST.SysLanguage
            '_st.LoadObjectLanguage()

            cFNEfficency2.Caption = Me.gridBand3.Caption
            cFNEfficency2.ColumnEdit = Me.RepositoryItemCalcFNEfficency
            cFNEfficency2.DisplayFormat.FormatString = "N2"
            cFNEfficency2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            cFNEfficency2.FieldName = "FNEfficency"
            cFNEfficency2.MinWidth = 25
            cFNEfficency2.Name = "cFNEfficency" & _PartCode
            cFNEfficency2.Visible = True
            cFNEfficency2.Width = 94

            _gridBand3.Columns.Add(cFNEfficency2)
            _gridBand3.Caption = "-"
            _gridBand3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            _gridBand3.Name = "gridBand3" & _PartCode
            _gridBand3.VisibleIndex = 5
            _gridBand3.Width = 94





            Dim _cFNHSysMarkId As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn


            _cFNHSysMarkId.Caption = Me.cFNHSysMarkId.Caption
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

            _BandedGridColumn7.Caption = Me.cFNHSysMarkId.Caption
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
            _BandedGridColumn20.Visible = False
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

            Try
                _BandedGridColumn19.Caption = GetObjLang("", Me.Name, "FNTotalQty", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn19.Caption = "FNTotalQty"
            End Try

            _BandedGridColumn19.DisplayFormat.FormatString = "N0"
            _BandedGridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn19.FieldName = "FNTotalQty"
            _BandedGridColumn19.MinWidth = 25
            _BandedGridColumn19.Name = "BandedGridColumn19" & _PartCode
            _BandedGridColumn19.OptionsColumn.AllowEdit = False
            _BandedGridColumn19.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQty", "{0:N0}")})
            _BandedGridColumn19.Visible = True
            _BandedGridColumn19.Width = 110




            Try
                _BandedGridColumn10.Caption = GetObjLang("", Me.Name, "MarkSeq", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn10.Caption = "MarkSeq"
            End Try

            _BandedGridColumn10.FieldName = "MarkSeq"
            _BandedGridColumn10.MinWidth = 25
            _BandedGridColumn10.Name = "BandedGridColumn10" & _PartCode
            _BandedGridColumn10.Width = 94
            '
            'BandedGridColumn9
            '

            Try
                _BandedGridColumn9.Caption = GetObjLang("", Me.Name, "BandedGridColumn9", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn9.Caption = "BandedGridColumn9"
            End Try
            _BandedGridColumn9.FieldName = "FNSeq"
            _BandedGridColumn9.MinWidth = 25
            _BandedGridColumn9.Name = "BandedGridColumn9" & _PartCode
            _BandedGridColumn9.Width = 94
            '
            'BandedGridColumn7
            '

            Try
                _BandedGridColumn7.Caption = GetObjLang("", Me.Name, "FNHSysMarkId", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn7.Caption = "FNHSysMarkId"
            End Try
            _BandedGridColumn7.FieldName = "FNHSysMarkId_Hide"
            _BandedGridColumn7.MinWidth = 25
            _BandedGridColumn7.Name = "BandedGridColumn7" & _PartCode
            _BandedGridColumn7.Width = 94
            '
            'BandedGridColumn8
            '

            Try
                _BandedGridColumn8.Caption = GetObjLang("", Me.Name, "BandedGridColumn8", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn8.Caption = "BandedGridColumn8"
            End Try

            _BandedGridColumn8.FieldName = "FNHSysStyleId"
            _BandedGridColumn8.MinWidth = 25
            _BandedGridColumn8.Name = "BandedGridColumn8" & _PartCode
            _BandedGridColumn8.Width = 94

            _gBTotal.Columns.Add(_BandedGridColumn20)
            _gBTotal.Columns.Add(_BandedGridColumn18)
            _gBTotal.Columns.Add(_BandedGridColumn17)
            _gBTotal.Columns.Add(_BandedGridColumn19)
            _gBTotal.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            _gBTotal.Name = "gBTotal" & _PartCode
            _gBTotal.Caption = "-"
            _gBTotal.VisibleIndex = 3
            _gBTotal.Width = 448




            Try
                _cFNActuallong.Caption = GetObjLang("", Me.Name, "FNActuallong", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _cFNActuallong.Caption = "FNActuallong"
            End Try

            _cFNActuallong.ColumnEdit = Me.RepositoryItemCalcEditFNActuallong
            _cFNActuallong.DisplayFormat.FormatString = "N2"
            _cFNActuallong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _cFNActuallong.FieldName = "FNActuallong"
            _cFNActuallong.MinWidth = 25
            _cFNActuallong.Name = "FNActuallong"
            _cFNActuallong.Visible = True
            _cFNActuallong.Width = 141
            _cFNActuallong.Tag = "2|"


            Try
                _BandedGridColumn11.Caption = GetObjLang("", Me.Name, "BandedGridColumn11", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn11.Caption = "BandedGridColumn11"
            End Try

            _BandedGridColumn11.ColumnEdit = Me.RepositoryItemCalcEdit1
            _BandedGridColumn11.DisplayFormat.FormatString = "N0"
            _BandedGridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn11.FieldName = "FNYard"
            _BandedGridColumn11.MinWidth = 25
            _BandedGridColumn11.Name = "BandedGridColumn11"
            _BandedGridColumn11.Visible = True
            _BandedGridColumn11.Width = 147
            _BandedGridColumn11.Tag = "2|"

            Try
                _BandedGridColumn12.Caption = GetObjLang("", Me.Name, "BandedGridColumn12", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn12.Caption = "BandedGridColumn12"
            End Try

            _BandedGridColumn12.ColumnEdit = Me.RepositoryItemCalcEdit2
            _BandedGridColumn12.DisplayFormat.FormatString = "N2"
            _BandedGridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn12.FieldName = "FNInc"
            _BandedGridColumn12.MinWidth = 25
            _BandedGridColumn12.Name = "BandedGridColumn12"
            _BandedGridColumn12.Visible = True
            _BandedGridColumn12.Width = 87


            Try
                _BandedGridColumn13.Caption = GetObjLang("", Me.Name, "BandedGridColumn13", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _BandedGridColumn13.Caption = "BandedGridColumn13"
            End Try
            _BandedGridColumn13.DisplayFormat.FormatString = "N6"
            _BandedGridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _BandedGridColumn13.FieldName = "FNQuantityUse"
            _BandedGridColumn13.MinWidth = 25
            _BandedGridColumn13.Name = "BandedGridColumn13"
            _BandedGridColumn13.OptionsColumn.AllowEdit = False
            _BandedGridColumn13.Visible = True
            _BandedGridColumn13.Width = 99






            _gbyard.AppearanceHeader.Options.UseTextOptions = True
            _gbyard.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            _gbyard.Columns.Add(_cFNActuallong)
            _gbyard.Columns.Add(_BandedGridColumn11)
            _gbyard.Columns.Add(_BandedGridColumn12)
            _gbyard.Columns.Add(_BandedGridColumn13)
            _gbyard.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            Try
                _gbyard.Caption = GetObjLang("", Me.Name, "gbyard", "").Split("|")(HI.ST.Lang.Language)
            Catch ex As Exception
                _gbyard.Caption = "ความยาว"
            End Try

            _gbyard.Name = "gbyard"
            _gbyard.VisibleIndex = 4
            _gbyard.Width = 474





            With _GridV
                .GridControl = _Grid
                .Name = "ogvGSum" & _PartCode


                .Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {_gBMark, _gBTotal, _gbyard, _gridBand3})
                .Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {_BandedGridColumn10, _BandedGridColumn9, _BandedGridColumn7, _cFNHSysMarkId, _BandedGridColumn20, _BandedGridColumn18, _BandedGridColumn17, _BandedGridColumn19, _BandedGridColumn8, _cFNActuallong, _BandedGridColumn11, _BandedGridColumn12, _BandedGridColumn13, cFNEfficency2})
                .DetailHeight = 431
                '.GridControl = Me.ogcratio
                '.Name = "ogvGSum" & _PartCode
                .OptionsCustomization.AllowQuickHideColumns = False
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.ShowGroupPanel = False
                .Tag = "2|"
            End With

            AddHandler Me.RepositoryItemCalcEdit1.EditValueChanging, AddressOf RepositoryItemCalcEdit1_EditValueChanging
            AddHandler Me.RepositoryItemCalcEdit2.EditValueChanging, AddressOf RepositoryItemCalcEdit2_EditValueChanging
            Return _GridV
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private _DtObject As DataTable

    Private Function GetObjLang(ByVal pModuleName As String, ByVal pFormName As String, ByVal pObjectName As String, ByVal _ObjTag As String) As String
        Dim tSql As String = Nothing
        Dim _Tag As String = "1"

        If Not String.IsNullOrEmpty(_ObjTag) Then
            _Tag = _ObjTag
        End If

        If _DtObject Is Nothing Then
            tSql = "SELECT '|'  + ISNULL(FTLangEN,'')  +'|'+ ISNULL(FTLangTH,'') + '|'+ ISNULL(FTLangVT,'') +'|'+ ISNULL(FTLangKM,'') +'|'+ ISNULL(FTLangBM,'') + '|'+ ISNULL(FTLangLAO,'') +'|'+ ISNULL(FTLangCH,'') AS LangT,FTObjectName "
            tSql += Constants.vbCrLf & " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage WITH(Nolock) "
            tSql += Constants.vbCrLf & " WHERE  FTFormName='" + HI.UL.ULF.rpQuoted(pFormName) & "' "
            _DtObject = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_LANG)
        End If

        Try

            For Each R As DataRow In _DtObject.[Select](" FTObjectName='" & HI.UL.ULF.rpQuoted(pObjectName.Trim()) & "'")
                Return _Tag & (R("LangT")).ToString()
            Next

            Return ""
        Catch ex As Exception
            Return ""
        End Try

        Return ""
    End Function





    Private Sub InitGridRatio(_dt As DataTable, _dt2 As DataTable, ogv As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView, ogc As DevExpress.XtraGrid.GridControl)
        Try

            Dim _Qry As String = ""
            Dim _colcount As Integer = 0

            With ogv

                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                         "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "FNYard".ToUpper, "FNInc".ToUpper, "FNQuantityUse".ToUpper, "FNActuallong".ToUpper,
                             "FNEfficency".ToUpper

                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next


                'Try
                '    For I As Integer = .Bands.Count - 1 To 0 Step -1

                '        Select Case .Bands(I).Name.ToUpper

                '            Case "gBMark".ToUpper, "gBTotal".ToUpper, "gbyard".ToUpper
                '            Case Else
                '                .Bands.Remove(.Bands(I))



                '        End Select

                '    Next
                'Catch ex As Exception

                'End Try


                If Not (_dt Is Nothing) Then
                    Dim _StyleCodeOld As String = ""
                    Dim ColBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    Dim colwith As Integer = 0
                    For Each Col As DataColumn In _dt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper

                            Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                        "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
                                     "FNHSysStyleId_Hide".ToUpper, "FNYard".ToUpper, "FNInc".ToUpper, "FNQuantityUse".ToUpper, "FNActuallong".ToUpper, "FNEfficency".ToUpper


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


                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End With

                                End With


                                _StyleCodeOld = _StyleCode
                        End Select
                    Next
                    ' Next
                End If
                Try
                    For I As Integer = .Bands.Count - 1 To 0 Step -1

                        Select Case .Bands(I).Name.ToUpper

                            Case "gBMark".ToUpper, "gBTotal".ToUpper, "gridBand3".ToUpper
                            Case Else


                                For Each Col As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In .Columns
                                    Dim _name As String = Replace(.Bands(I).Name, "GridBand1", "")

                                    If Microsoft.VisualBasic.Left(Col.Name.ToString, Len(_name)) = _name Then
                                        Select Case Col.FieldName.ToUpper

                                            Case "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNHSysStyleId".ToUpper, "FTColorWay".ToUpper, "FNHSysStyleId".ToUpper,
                                            "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "Total".ToUpper,
                                         "FNHSysStyleId_Hide".ToUpper, "FNYard".ToUpper, "FNInc".ToUpper, "FNQuantityUse".ToUpper, "FNEfficency".ToUpper, "FNActuallong".ToUpper
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


            AddHandler ogv.RowCellStyle, AddressOf AdvBandedGridView2_RowCellStyle
            ogc.DataSource = _dt.Copy
            ogv.BestFitColumns()

        Catch ex As Exception
            MsgBox("N" & ex.ToString)
        End Try
    End Sub



    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTGroupNo.Text <> "" And FTGroupNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If
        _Pass = True

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call InitGrid()
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.FNInc.Value = 36
            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub RepositoryItemCalcEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs)
        Try
            With CType(CType(CType(sender, DevExpress.XtraEditors.CalcEdit).Parent, DevExpress.XtraGrid.GridControl).MainView, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)

                If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub

                Dim _NYard As Double = e.NewValue
                Dim _Inc As Double = 0
                Dim _TotalUse As Double = 0
                Dim _NYard1 As Double = 0
                Dim _NLayer As Double = Val(.GetRowCellValue(.FocusedRowHandle, "FNLayerQty"))


                _Inc = Val(.GetRowCellValue(.FocusedRowHandle, "FNInc"))

                If Me.FNInc.Value <= 0 Then
                    Me.FNInc.Focus()
                    Exit Sub
                End If
                _NYard1 = (_Inc + Me.FNToralent.Value) / Me.FNInc.Value
                _TotalUse = (_NYard1 + _NYard) * _NLayer

                .SetRowCellValue(.FocusedRowHandle, "FNQuantityUse", _TotalUse)


                Call getBalOptiplan(CType(sender, DevExpress.XtraEditors.CalcEdit).Parent)


            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCalcEdit2_EditValueChanging(sender As Object, e As ChangingEventArgs)
        Try

            With CType(CType(CType(sender, DevExpress.XtraEditors.CalcEdit).Parent, DevExpress.XtraGrid.GridControl).MainView, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub

                Dim _NYard As Double = Val(.GetRowCellValue(.FocusedRowHandle, "FNYard"))
                Dim _Inc As Double = 0
                Dim _TotalUse As Double = 0
                Dim _NYard1 As Double = 0
                Dim _NLayer As Double = Val(.GetRowCellValue(.FocusedRowHandle, "FNLayerQty"))


                _Inc = e.NewValue  ' Val(.GetRowCellValue(.FocusedRowHandle, "FNInc"))

                _NYard1 = (_Inc + Me.FNToralent.Value) / Me.FNInc.Value
                _TotalUse = (_NYard1 + _NYard) * _NLayer

                .SetRowCellValue(.FocusedRowHandle, "FNQuantityUse", _TotalUse)

                Call getBalOptiplan(CType(sender, DevExpress.XtraEditors.CalcEdit).Parent)

            End With
        Catch ex As Exception

        End Try
    End Sub


    Private Sub getBalOptiplan(x As DevExpress.XtraGrid.GridControl)
        Try
            Dim _QtyUse As Double = 0

            With DirectCast(x.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    _QtyUse += +Val(R!FNQuantityUse.ToString)
                Next

            End With

            Me.FNOptiplanBal.Value = Me.FNOptiplanYard.Value - _QtyUse

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


            'Dim _PartId As Integer = HI.Conn.SQLConn.GetFieldOnBeginTrans("Select top 1 FNHSysPartId from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPart with(nolock) where FTPartCode='" & R!FTPartCode.ToString & "'", Conn.DB.DataBaseName.DB_PROD)


            For Each xt As DevExpress.XtraTab.XtraTabPage In Me.xtabpart.TabPages
                Dim _Tab As DevExpress.XtraTab.XtraTabPage
                _Tab = DirectCast(xt, DevExpress.XtraTab.XtraTabPage)

                If xt.Name = "XtraTabPage1" Then

                    xt.PageEnabled = False
                    xt.PageVisible = False
                Else

                    For Each Obj As Object In _Tab.Controls
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.GridControl


                                Dim _Grid As DevExpress.XtraGrid.GridControl
                                _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                                Dim _GridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                                _GridView = _Grid.MainView



                                With DirectCast(_Grid.DataSource, DataTable)
                                    .AcceptChanges()


                                    For Each X As DataRow In .Rows

                                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMUActualPlan "
                                        _Cmd &= vbCrLf & " set FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Cmd &= vbCrLf & " , FDUpdDate =" & HI.UL.ULDate.FormatDateDB
                                        _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                        _Cmd &= vbCrLf & " ,FTRemark='" & Me.FTRemark.Text & "'"
                                        _Cmd &= vbCrLf & " ,FNActuallong=" & Val(X!FNActuallong.ToString) & ""
                                        _Cmd &= vbCrLf & " ,FNYard=" & Val(X!FNYard.ToString) & ""
                                        _Cmd &= vbCrLf & " ,FNQuantityUse=" & Val(X!FNQuantityUse.ToString) & ""
                                        _Cmd &= vbCrLf & " ,FNLayerQty=" & Val(X!FNLayerQty.ToString) & ""
                                        _Cmd &= vbCrLf & " ,FNInc=" & Val(X!FNInc.ToString) & ""
                                        _Cmd &= vbCrLf & " ,FNEfficency=" & Val(X!FNEfficency.ToString) & ""

                                        _Cmd &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                                        _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
                                        _Cmd &= vbCrLf & " and FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                                        _Cmd &= vbCrLf & " and FNSeq=" & Val(X!MarkSeq.ToString) & ""
                                        _Cmd &= vbCrLf & " and FTPartCode='" & Microsoft.VisualBasic.Left(X!FNHSysMarkId.ToString, Len(X!FNHSysMarkId.ToString) - 10) & "'"
                                        _Cmd &= vbCrLf & " and FTMarkCode='" & (X!FNHSysMarkId.ToString) & "'"
                                        If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                            _Cmd = "insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMUActualPlan "
                                            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNHSysCmpId, FTGroupNo, FTDocumentNo, FNSeq, FTRemark, FTPartCode, FNActuallong, FNYard, FNInc, FNQuantityUse, FNLayerQty, FTMarkCode ,FNHSysMarkId ,FNEfficency)"
                                            _Cmd &= vbCrLf & " select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB
                                            _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB
                                            _Cmd &= vbCrLf & " ," & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                                            _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
                                            _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
                                            _Cmd &= vbCrLf & " ," & Val(X!MarkSeq.ToString) & ""
                                            _Cmd &= vbCrLf & " ,'" & Me.FTRemark.Text & "'"
                                            _Cmd &= vbCrLf & " ,'" & Microsoft.VisualBasic.Left(X!FNHSysMarkId.ToString, Len(X!FNHSysMarkId.ToString) - 10) & "'"

                                            _Cmd &= vbCrLf & " ," & Val(X!FNActuallong.ToString) & ""
                                            _Cmd &= vbCrLf & " ," & Val(X!FNYard.ToString) & ""
                                            _Cmd &= vbCrLf & " ," & Val(X!FNInc.ToString) & ""
                                            _Cmd &= vbCrLf & " ," & Val(X!FNQuantityUse.ToString) & ""
                                            _Cmd &= vbCrLf & " ," & Val(X!FNLayerQty.ToString) & ""

                                            _Cmd &= vbCrLf & " ,'" & (X!FNHSysMarkId.ToString) & "'"
                                            _Cmd &= vbCrLf & " ," & Val(0) & ""
                                            _Cmd &= vbCrLf & " ," & Val(X!FNEfficency.ToString) & ""


                                            If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                HI.Conn.SQLConn.Tran.Rollback()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                Return False
                                            End If
                                        End If


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

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            Try
                If SaveData() Then

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub


    Public Sub loadpart()
        Dim _Qry As String = ""
        Dim _dt As DataTable
        _Qry = "select  distinct '0' as FTSelect  , FTPartCode   from  TPRODMUGroupPlan S with(nolock)  "
        _Qry &= vbCrLf & "where FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'  and FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcpart.DataSource = _dt
    End Sub

    Private Sub FTGroupNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTDocumentNo.EditValueChanged, FTGroupNo.EditValueChanged
        Me.ogcratio.DataSource = Nothing
        Me.loadpart()
    End Sub

    Private Sub AdvBandedGridView2_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles AdvBandedGridView2.RowCellStyle
        With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If e.RowHandle <> .FocusedRowHandle OrElse e.Column.AbsoluteIndex = .FocusedColumn.AbsoluteIndex Then
                If (e.RowHandle Mod 2 = 1) Then
                    e.Appearance.BackColor = System.Drawing.Color.LightSkyBlue
                Else
                    e.Appearance.BackColor = System.Drawing.Color.White
                End If
            End If
        End With
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            Try
                If Me.FTDocumentNo.Text = "" Or Me.FTGroupNo.Text = "" Then

                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.LabelControl1.Text)
                    Exit Sub
                End If
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = False Then
                    Exit Sub
                End If

                If CheckUse() Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถลบข้อมูลได้ เนื่องจาก มีทำขั้นตอนถัดไปแล้ว !!", 2204291050, Me.Text)
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
        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckUse() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1 *  from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMUTOrderProd  where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return True
        End Try
    End Function



    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMUActualPlan WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            _Str &= vbCrLf & " and  FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            '_Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMURatio_D WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "'"
            '_Str &= vbCrLf & " and  FTGroupNo='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "'"
            'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


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

    Private Sub ocmgeneratejobprod_Click(sender As Object, e As EventArgs) Handles ocmgeneratejobprod.Click
        Try

            If Me.FTDocumentNo.Text = "" Then Exit Sub

            Dim _dt As DataTable
            Dim _dt2 As DataTable
            Dim _Qry As String = ""
            Dim _colcount As Integer = 0

            Dim _PartCode As String = Me.xtabpart.SelectedTabPage.Text.ToString

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_MUGroupPlanSetActual_ForProd  @FTGroupNo ='" & HI.UL.ULF.rpQuoted(Me.FTGroupNo.Text) & "' "
            _Qry &= vbCrLf & " , @FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTDocumentNo.Text) & "' "
            _Qry &= vbCrLf & "  , @PartCode='" & HI.UL.ULF.rpQuoted(_PartCode) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            'If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการสร้าง งานผลิต ใช่หรือไม่ !!!!", 2204121057,  , Me.Text) = False Then Exit Sub

            Dim _Grid As DevExpress.XtraGrid.GridControl
            For Each Obj As Object In Me.xtabpart.SelectedTabPage.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.GridControl
                        _Grid = DirectCast(Obj, DevExpress.XtraGrid.GridControl)
                End Select
            Next
            Dim _odtConfig As DataTable
            _Qry = "Select top 1  FNCutAuto, FNCutManual   From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSEConfig  "
            _Qry &= vbCrLf & " where FTCmpCode='" & HI.ST.SysInfo.CmpCode & "'"
            _odtConfig = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)


            '_Qry = "select T.* , h.FTRef from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMUTOrderProd_TableCut_Detail T with(nolock)   "
            '_Qry &= vbCrLf & " left join   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo. TPRODMUTOrderProd p with(nolock)  on t.FTOrderProdNo = p.FTOrderProdNo  "
            '_Qry &= vbCrLf & " left join   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.  TPRODMUTOrderProd_TableCut h with(nolock) on t.FTOrderProdNo = h.FTOrderProdNo"
            '_Qry &= vbCrLf & " and  t.FNTableNo  = h.FNTableNo"
            '_Qry &= vbCrLf & " where  p.FTOrderNo='" & Me.FTDocumentNo.Text & "'"

            '_dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            'For Each R As DataRow In _dt2.Rows

            '    For Each X As DataRow In _dt.Select("  FTMarkCode ='" & R!FTRef.ToString & "'")
            '        X!
            '    Next
            'Next

            HI.TL.HandlerControl.ClearControl(_wAddGenProdJob)
            With _wAddGenProdJob
                .ogcpart.DataSource = _dt
                .FTOrderProdNo = Me.FTDocumentNo.Text
                .ODTRatio = DirectCast(_Grid.DataSource, DataTable)
                .odtConfig = _odtConfig
                .Totalent = Me.FNToralent.Value
                .ockTableAuto.Checked = True

                .ShowDialog()

            End With


        Catch ex As Exception

        End Try

    End Sub


End Class
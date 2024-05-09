Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wProdIronTracking


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNTotalFullCartonQty|FNTotalScarpCartonQty|FNTotalScanQuantity|FNTotalCarton|FNTotalFullCarton|FNTotalScarpCarton"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNTotalFullCartonQty|FNTotalScarpCartonQty|FNTotalScanQuantity|FNTotalCarton|FNTotalFullCarton|FNTotalScarpCarton"


        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        Try
            With ogvtime
                .ClearGrouping()
                .ClearDocument()
                '.Columns("FTDateTrans").Group()

                For Each Str As String In sFieldCount.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldCustomSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldGrpCount.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldCustomGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()

            End With

        Catch ex As Exception
        End Try

        sFieldCount = ""
        sFieldSum = "FNScanQuantity"

        sFieldGrpCount = ""
        sFieldGrpSum = "FNScanQuantity"

        Try
            With ogvdetailcolorsize
                .ClearGrouping()
                .ClearDocument()
                '.Columns("FTDateTrans").Group()

                For Each Str As String In sFieldCount.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldCustomSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldGrpCount.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldCustomGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()

            End With
        Catch ex As Exception

        End Try

        '------End Add Summary Grid-------------
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

    Private Sub LoadData()

        ogdtime.DataSource = Nothing
        ogcdetailcolorsize.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            _Qry = "SELECT   MA.FDInsDate"
            _Qry &= vbCrLf & "  ,MA.FTCustomerPO"
            _Qry &= vbCrLf & "  ,MA.FTStyleCode"
            _Qry &= vbCrLf & " 	,MA.FTOrderNo	"
            _Qry &= vbCrLf & " ,SUM(1) AS FNTotalCarton"
            _Qry &= vbCrLf & " ,SUM(Case When (MA.FNPackPerCarton = MA.FNScanQuantity) Then 1 Else 0 End) As FNTotalFullCarton"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN (MA.FNPackPerCarton <> MA.FNScanQuantity) THEN 1 ELSE 0 END) AS FNTotalScarpCarton"
            _Qry &= vbCrLf & " ,SUM(Case When (MA.FNPackPerCarton = MA.FNScanQuantity) Then MA.FNScanQuantity Else 0 End) As FNTotalFullCartonQty"
		 _Qry &= vbCrLf & " ,SUM(Case When (MA.FNPackPerCarton <> MA.FNScanQuantity) Then MA.FNScanQuantity Else 0 End) As FNTotalScarpCartonQty"
            _Qry &= vbCrLf & " ,SUM(MA.FNScanQuantity) As FNTotalScanQuantity"

            _Qry &= vbCrLf & "  FROM ( "



            _Qry &= vbCrLf & "  Select Case When ISDATE(A.FDInsDate) = 1 Then   Convert(DateTime,A.FDInsDate)  Else NULL End As 	FDInsDate "
            _Qry &= vbCrLf & "   ,XPO.FTCustomerPO"
            _Qry &= vbCrLf & "  , ST.FTStyleCode"
            _Qry &= vbCrLf & "  , PCT.FTOrderNo"
            _Qry &= vbCrLf & "  , A.FTPackNo"
            _Qry &= vbCrLf & "  , A.FNCartonNo	"
            _Qry &= vbCrLf & " , PCT.FNPackPerCarton "
            _Qry &= vbCrLf & "  , SUM(ISNULL(X.FNScanQuantity,0)) As FNScanQuantity"
            _Qry &= vbCrLf & " FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton As A With(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack As B With(NOLOCK) On A.FTPackNo = B.FTPackNo INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As PCT With(NOLOCK) On A.FTPackNo = PCT.FTPackNo And A.FNCartonNo = PCT.FNCartonNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On B.FNHSysStyleId = ST.FNHSysStyleId"
            _Qry &= vbCrLf & "   OUTER APPLY (	Select SUM(FNScanQuantity) As FNScanQuantity"
            _Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As SC With(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE SC.FTPackNo =A.FTPackNo"
            _Qry &= vbCrLf & "  And SC.FNCartonNo  = PCT.FNCartonNo	 "
            _Qry &= vbCrLf & " And SC.FTOrderNo  = PCT.FTOrderNo"
            _Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            _Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            _Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "

            _Qry &= vbCrLf & ") As X  "


            _Qry &= vbCrLf & "   OUTER APPLY (	Select TOP 1 FTPOref AS FTCustomerPO,FTNikePOLineItem AS FTPOLine "
            _Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As SC With(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE  SC.FTOrderNo  = PCT.FTOrderNo"
            _Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            _Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            _Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "

            _Qry &= vbCrLf & "  And SC.FTNikePOLineItem  = ISNULL(PCT.FTPOLine,SC.FTNikePOLineItem) "

            _Qry &= vbCrLf & ") As XPO  "


            _Qry &= vbCrLf & " WHERE   (A.FTState = N'1') "
            _Qry &= vbCrLf & " AND   (B.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ") "

            If FDDate.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FDInsDate >='" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "'  "
            End If

            If FDDateTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FDInsDate <='" & HI.UL.ULDate.ConvertEnDB(FDDateTo.Text) & "'  "
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND ST.FTStyleCode >='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "'  "
            End If


            If FNHSysStyleIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND ST.FTStyleCode  <='" & HI.UL.ULF.rpQuoted(FNHSysStyleIdTo.Text) & "'  "
            End If


            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If FTSubOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTSubOrderNo >='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'  "
            End If

            If FTSubOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTSubOrderNo <='" & HI.UL.ULF.rpQuoted(FTSubOrderNoTo.Text) & "'  "
            End If

            If FNHSysPOID.Text <> "" Then
                _Qry &= vbCrLf & " AND B.FTCustomerPO>='" & HI.UL.ULF.rpQuoted(FNHSysPOID.Text) & "'  "
            End If

            If FNHSysPOIDTo.Text <> "" Then
                _Qry &= vbCrLf & " AND B.FTCustomerPO <='" & HI.UL.ULF.rpQuoted(FNHSysPOIDTo.Text) & "'  "
            End If

            _Qry &= vbCrLf & " GROUP BY   CASE WHEN ISDATE(A.FDInsDate) = 1 THEN   Convert(DateTime,A.FDInsDate)  ELSE NULL END "
            _Qry &= vbCrLf & " , A.FTPackNo"
            _Qry &= vbCrLf & " , A.FNCartonNo"
            _Qry &= vbCrLf & " , XPO.FTCustomerPO"
            _Qry &= vbCrLf & " , ST.FTStyleCode"
            _Qry &= vbCrLf & " ,PCT.FTOrderNo"
            _Qry &= vbCrLf & " ,PCT.FNPackPerCarton"
            _Qry &= vbCrLf & " ) AS MA"
            _Qry &= vbCrLf & " GROUP BY   MA.FDInsDate"
            _Qry &= vbCrLf & "  ,MA.FTCustomerPO"
            _Qry &= vbCrLf & "  ,MA.FTStyleCode"
            _Qry &= vbCrLf & " 	,MA.FTOrderNo	"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            Me.ogdtime.DataSource = _dt
            Me.ogvtime.BestFitColumns()
            Call LoaddataDetailColorSize()

        Catch ex As Exception
        End Try

        _Spls.Close()


    End Sub

    Private Sub LoaddataDetailColorSize()
        Dim _Qry As String
        Dim _dt As DataTable
        ogcdetailcolorsize.DataSource = Nothing
        Try
            _Qry = " Select   Case When ISDATE(A.FDInsDate) = 1 Then   Convert(DateTime,A.FDInsDate)  Else NULL End As FDInsDate "
            _Qry &= vbCrLf & "  , A.FTPackNo"
            _Qry &= vbCrLf & "  , A.FNCartonNo		 "
            _Qry &= vbCrLf & "  , B.FTCustomerPO"
            _Qry &= vbCrLf & "  , ST.FTStyleCode"
            _Qry &= vbCrLf & "  , PCT.FTOrderNo"
            _Qry &= vbCrLf & "  , PCT.FTSubOrderNo"
            _Qry &= vbCrLf & "  , PCT.FTColorway"
            _Qry &= vbCrLf & "  , PCT.FTSizeBreakDown"
            _Qry &= vbCrLf & "  , XPO.FTPOLine"
            _Qry &= vbCrLf & "  , SUM(ISNULL(X.FNScanQuantity,0)) As FNScanQuantity"
            _Qry &= vbCrLf & " FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton As A With(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack As B With(NOLOCK) On A.FTPackNo = B.FTPackNo INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As PCT With(NOLOCK) On A.FTPackNo = PCT.FTPackNo And A.FNCartonNo = PCT.FNCartonNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On B.FNHSysStyleId = ST.FNHSysStyleId"
            _Qry &= vbCrLf & "   OUTER APPLY (	Select SUM(FNScanQuantity) As FNScanQuantity"
            _Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As SC With(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE SC.FTPackNo =A.FTPackNo"
            _Qry &= vbCrLf & "  And SC.FNCartonNo  = PCT.FNCartonNo	 "
            _Qry &= vbCrLf & " And SC.FTOrderNo  = PCT.FTOrderNo"
            _Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            _Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            _Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "

            _Qry &= vbCrLf & ") As X  "


            _Qry &= vbCrLf & "   OUTER APPLY (	Select TOP 1 FTPOref AS FTCustomerPO,FTNikePOLineItem AS FTPOLine "
            _Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As SC With(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE  SC.FTOrderNo  = PCT.FTOrderNo"
            _Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            _Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            _Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "
            _Qry &= vbCrLf & "  And SC.FTNikePOLineItem  = ISNULL(PCT.FTPOLine,SC.FTNikePOLineItem) and sc.FTPOref = b.FTCustomerPO "
            _Qry &= vbCrLf & ") As XPO  "


            _Qry &= vbCrLf & " WHERE   (A.FTState = N'1') "
            _Qry &= vbCrLf & " AND   (B.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ") "
            If FDDate.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FDInsDate >='" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "'  "
            End If

            If FDDateTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FDInsDate <='" & HI.UL.ULDate.ConvertEnDB(FDDateTo.Text) & "'  "
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND ST.FTStyleCode >='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "'  "
            End If


            If FNHSysStyleIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND ST.FTStyleCode  <='" & HI.UL.ULF.rpQuoted(FNHSysStyleIdTo.Text) & "'  "
            End If


            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If FTSubOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTSubOrderNo >='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'  "
            End If

            If FTSubOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND PCT.FTSubOrderNo <='" & HI.UL.ULF.rpQuoted(FTSubOrderNoTo.Text) & "'  "
            End If


            If FNHSysPOID.Text <> "" Then
                _Qry &= vbCrLf & " AND B.FTCustomerPO>='" & HI.UL.ULF.rpQuoted(FNHSysPOID.Text) & "'  "
            End If

            If FNHSysPOIDTo.Text <> "" Then
                _Qry &= vbCrLf & " AND B.FTCustomerPO <='" & HI.UL.ULF.rpQuoted(FNHSysPOIDTo.Text) & "'  "
            End If

            _Qry &= vbCrLf & " GROUP BY   CASE WHEN ISDATE(A.FDInsDate) = 1 THEN   Convert(DateTime,A.FDInsDate)  ELSE NULL END "
            _Qry &= vbCrLf & " , A.FTPackNo"
            _Qry &= vbCrLf & "  , A.FNCartonNo"
            _Qry &= vbCrLf & "  , B.FTCustomerPO"
            _Qry &= vbCrLf & " , ST.FTStyleCode"
            _Qry &= vbCrLf & " , PCT.FTOrderNo"
            _Qry &= vbCrLf & " , PCT.FTSubOrderNo"
            _Qry &= vbCrLf & " , PCT.FTColorway"
            _Qry &= vbCrLf & " , PCT.FTSizeBreakDown"
            _Qry &= vbCrLf & " , XPO.FTPOLine "

            _Qry &= vbCrLf & "  ORDER BY CASE WHEN ISDATE(A.FDInsDate) = 1 THEN   Convert(DateTime,A.FDInsDate)  ELSE NULL END ,A.FTPackNo,A.FNCartonNo,B.FTCustomerPO "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Me.ogcdetailcolorsize.DataSource = _dt
            Me.ogvdetailcolorsize.BestFitColumns()
        Catch ex As Exception
        End Try

    End Sub



    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FDDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FDDateTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If


        If Me.FNHSysStyleIdTo.Text <> "" And FNHSysStyleIdTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If


        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If


        If Me.FTSubOrderNo.Text <> "" And FTSubOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTSubOrderNoTo.Text <> "" And FTSubOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If


        If Me.FNHSysPOID.Text <> "" And FNHSysPOID.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysPOIDTo.Text <> "" And FNHSysPOIDTo.Properties.Tag.ToString <> "" Then
            _Pass = True
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

            Call InitGrid()

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetailcolorsize)

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

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetailcolorsize)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


End Class
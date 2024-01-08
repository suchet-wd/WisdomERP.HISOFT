Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wPatternMasterPlan_New

    Private GridDataBefore As String = ""
    Private _FocusedColumn As DevExpress.XtraGrid.Columns.GridColumn
    Private _FocusedRowHendle As Integer = 0

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode;
        'TBuyCodeFrom.DataBindings = HI.Conn.SQLConn.GetField("SELECT DISTINCT FTBuyCode FROM TMERMBuy WITH(NOLOCK) Order BY FTBuyCode Desc", Conn.DB.DataBaseName.DB_MASTER, "")
        'TBuyCodeTo.Text = HI.Conn.SQLConn.GetField("SELECT DISTINCT FTBuyCode FROM TMERMBuy WITH(NOLOCK) Order BY FTBuyCode Desc", Conn.DB.DataBaseName.DB_MASTER, "")

        ' Add any initialization after the InitializeComponent() call.

        With ReposDate

            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave
            AddHandler .Click, AddressOf ItemDate_GotFocus

        End With

        With ReposPatternType

            AddHandler .Leave, AddressOf ItemString_Leave
            AddHandler .Leave, AddressOf ItemString_GotFocus

        End With


        'With rFTPatternGrpTypeCode
        '    AddHandler .Click, AddressOf ItemString_GotFocus
        '    AddHandler .Leave, AddressOf ItemGrpTypeCode_Leave
        'End With

        InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        'Me.FNYear.Text = New Date.Now.Year
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity" 'FNQuantity|FNPass|FNNotPass"
        '"FNQuantity|FNQuantityCut|FNQuantityEmb|FNQuantityRcvEmb|FNQuantityPrint|FNQuantityRcvPrint|FNQuantityHeat|FNQuantityRcvHeat|FNQuantityLasor|FNQuantityRcvLasor|FNQuantityPadPrint|FNQuantityRcvPadPrint|FNQuantitySew|FNQuantityFinishSew|FNQuantityQC|FNPass|FNNotPass"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldCustomSum As String = "" '"FNSMPSam"

        Dim sFieldCustomGrpSum As String = ""

        With ogvPattern
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")

                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n4}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n4})")
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


        '------End Add Summary Grid-------------
    End Sub
#End Region


#Region "Custom summaries"

    Private totalSum As Double = 0
    Private GrpSum As Double = 0
    Private _RowHandleHold As Integer = 0


    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

    Private Sub ogvsummary_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvPattern
                'Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                '    Case "FNSMPSam"
                '        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                '            If e.IsTotalSummary Then
                '                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                '                    If (.GetRowCellValue(e.RowHandle, "FTSMPOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTSMPOrderNo").ToString()) Then
                '                        totalSum = totalSum + (Val(e.FieldValue.ToString))

                '                    End If
                '                End If
                '                _RowHandleHold = e.RowHandle
                '            End If
                '            e.TotalValue = totalSum
                '        End If

                'End Select
            End With

        Catch ex As Exception

        End Try

    End Sub

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

#End Region

#Region "Procedure"

    'Public Sub LoadDataInfo(ByVal Key As String)

    '    Dim _Qry As String = ""
    '    _Qry = " SELECT     SOP.FNSeq"

    '    _Qry &= vbCrLf & "  , Case When ISDATE(ISNULL( SOP.FTDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime, SOP.FTDate),103) ELSE '' END AS  FTDate "
    '    _Qry &= vbCrLf & "  , ISNULL(SOP.FNQuantity,0) As FNQuantity"
    '    _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) As FNPass"
    '    _Qry &= vbCrLf & "  , ISNULL(SOP.FNNotPass,0) As FNNotPass"
    '    _Qry &= vbCrLf & " ,  ISNULL(SOP.FTRemark,'') AS FTRemark"
    '    _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) +  ISNULL(SOP.FNNotPass,0) As FNTotalQC"

    '    _Qry &= vbCrLf & "  , ISNULL(SOP.FTSizeBreakDown,'') As FTSizeBreakDown"
    '    '_Qry &= vbCrLf & "  , ISNULL(SOP.FTColorway,'') As FTColorway"

    '    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC AS SOP WITH (NOLOCK)"
    '    _Qry &= vbCrLf & "   WHERE SOP.FTTeam='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
    '    _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC"
    '    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '    Me.ogcPattern.DataSource = _dt

    'End Sub


#End Region

#Region "General"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function CheckProcess(key As String, Optional showmsg As Boolean = True) As Boolean
        Dim stateprocess As Boolean = False

        Dim cmd As String = ""

        cmd = "select top 1 FTStateFinish from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS x with(nolock) where FTTeam='" & HI.UL.ULF.rpQuoted(key) & "'"

        stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        If stateprocess Then

            If showmsg Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึก จบ กระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return stateprocess
    End Function



    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load

        With Me.ogvPattern
            .Columns.ColumnByFieldName("FTActPTNDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            .Columns.ColumnByFieldName("FTPTNDate").OptionsColumn.AllowEdit = False

            '.Columns.ColumnByFieldName("FTFabricDate").OptionsColumn.AllowEdit = False
            '.Columns.ColumnByFieldName("FTAccessoryDate").OptionsColumn.AllowEdit = False

            'For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
            '    GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            'Next
            .OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
            .OptionsSelection.MultiSelect = False
            .ClearColumnsFilter()
        End With
    End Sub

#End Region

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        Call LoadOrderProdDetail()

        _Spls.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs)

        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub LoadOrderProdDetail()
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogcPattern.DataSource = Nothing

        If chkSample.Checked Then
            cmd &= "SELECT '" & HI.ST.SysInfo.CmpID & "' AS 'FNHSysCmpID' "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & ", ptt.FTPatternTypeNameTH AS 'FTPatternType',ptt.FTPatternTypeNameTH AS 'FTPatternType_Hide' "
                cmd &= vbCrLf & ", ptg.FTPatternGrpTypeNameTH AS 'FTPtnGrpName',ptg.FTPatternGrpTypeNameTH AS 'FTPtnGrpName_Hide' "
                cmd &= vbCrLf & ", us.FTUnitSectNameTH AS 'PatternTeam' "
            Else
                cmd &= vbCrLf & ", ptt.FTPatternTypeNameEN AS 'FTPatternType',ptt.FTPatternTypeNameEN AS 'FTPatternType_Hide' "
                cmd &= vbCrLf & ", ptg.FTPatternGrpTypeNameEN AS 'FTPtnGrpName',ptg.FTPatternGrpTypeNameEN AS 'FTPtnGrpName_Hide' "
                cmd &= vbCrLf & ", us.FTUnitSectNameEN AS 'PatternTeam' "
            End If
            cmd &= vbCrLf & ", PT.FNHSysPTNTypeId AS 'FNHSysPatternTypeId', PT.FNHSysPTNTypeId AS 'FNHSysPatternTypeId_Hide' "
            cmd &= vbCrLf & ", PT.FNHSysPTNGrpTypeId AS 'FNHSysPatternGrpTypeId', PT.FNHSysPTNGrpTypeId AS 'FNHSysPatternGrpTypeId_Hide' "
            cmd &= vbCrLf & ", ptg.FTLeadTime AS 'FTLeadTime', ptg.FTLeadTime AS 'FTLeadTime_Hide' "
            cmd &= vbCrLf & ", 'SAMPLEROOM' AS 'Category' "
            cmd &= vbCrLf & ", PT.TPTNOrderBy AS 'TPTNOrderBy' "
            cmd &= vbCrLf & ", A.FTSMPOrderNo "
            cmd &= vbCrLf & ", CASE WHEN ISDATE(A.FDSMPOrderDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,A.FDSMPOrderDate),103),'') Else NULL END AS 'FDSMPOrderDate'"
            cmd &= vbCrLf & ", DatePart(week, Convert(DateTime, A.FDSMPOrderDate)+30) AS 'PlanFinishWeek'"
            cmd &= vbCrLf & ", DatePart(Year, Convert(DateTime, A.FDSMPOrderDate)+30) AS 'PlanFinishYear'"
            cmd &= vbCrLf & ", DatePart(week, Convert(DateTime, A.FDSMPOrderDate)+35) AS 'ActFinishWeek'"
            cmd &= vbCrLf & ", DatePart(Year, Convert(DateTime, A.FDSMPOrderDate)+35) AS 'ActFinishYear'"
            cmd &= vbCrLf & ", CONVERT(varchar(10),DatePart(week, Convert(DateTime, A.FDSMPOrderDate)+30)) + '-' + CONVERT(varchar(10),DatePart(Year, Convert(DateTime, A.FDSMPOrderDate)+30)) AS 'PlanFinishWY'"
            cmd &= vbCrLf & ", CONVERT(varchar(10),DatePart(week, Convert(DateTime, A.FDSMPOrderDate)+35)) + '-' + CONVERT(varchar(10),DatePart(Year, Convert(DateTime, A.FDSMPOrderDate)+35))  AS 'ActFinishWY'"
            cmd &= vbCrLf & ", A.FTStyleName"
            cmd &= vbCrLf & ", A.FTGenderCode "
            cmd &= vbCrLf & ", A.FTGenderName"
            cmd &= vbCrLf & ", A.FTSMPOrderBy "
            cmd &= vbCrLf & ", A.FTOrderRemark"
            cmd &= vbCrLf & ", A.FTBuyCode"
            cmd &= vbCrLf & ", A.FTPgmName"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(A.FTPatternDate) = 1 Then CONVERT(varchar(10),convert(Datetime,A.FTPatternDate),103) Else NULL END AS 'FTPTNDate'"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(ISNULL(PT.FTActPTNDate,SMPMP.FTActPatternDate)) = 1 Then CONVERT(varchar(10),convert(Datetime,ISNULL(PT.FTActPTNDate,SMPMP.FTActPatternDate)),103) Else NULL END AS 'FTActPTNDate'"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(SMPMP.FTCFMSendSampleDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,SMPMP.FTCFMSendSampleDate),103),'') Else NULL END AS  FTCFMSendSampleDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(a.FTGACDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,a.FTGACDate),103),'') Else NULL END  AS  FDGacDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE('') = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,''),103),'') Else NULL END  AS OGacDate"
            cmd &= vbCrLf & ", ISNULL(XSAM.FNSam,0) AS FNSMPSam"
            cmd &= vbCrLf & ", A.FTCustomerTeam  "
            cmd &= vbCrLf & ", A.FNSMPPrototypeNo"
            cmd &= vbCrLf & ", A.FTStyleCode"
            cmd &= vbCrLf & ", A.FTSeasonCode"
            cmd &= vbCrLf & ", A.FTCustCode"
            cmd &= vbCrLf & ", CASE WHEN A.FTCustCode = 'NI' THEN 'NIKE'  ELSE 'NON NIKE' END AS 'GroupCust' "
            cmd &= vbCrLf & ", CASE WHEN A.FTCustCode = 'NI' THEN 'NIKE' WHEN A.FTCustCode = 'ZS' OR  A.FTCustCode = 'FNT' THEN 'TRACK ON NIKE' ELSE 'NON NIKE' END AS 'SubGroupCust' "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & ", ISNULL(XT.FTNameTH,'') AS FNSMPOrderType"
                cmd &= vbCrLf & ", A.FTCustNameTH AS FTCustName"
                cmd &= vbCrLf & ", ISNULL(XTOT.FTNameTH,'') AS FNOrderSampleType"
                cmd &= vbCrLf & ", ISNULL(XTOTST.FTNameTH,'') AS OrderStatus"
            Else
                cmd &= vbCrLf & ", ISNULL(XT.FTNameEN,'') AS FNSMPOrderType"
                cmd &= vbCrLf & ", A.FTCustNameEN AS FTCustName"
                cmd &= vbCrLf & ", ISNULL(XTOT.FTNameEN,'') AS FNOrderSampleType"
                cmd &= vbCrLf & ", ISNULL(XTOTST.FTNameEN,'') AS OrderStatus"
            End If

            cmd &= vbCrLf & ", A.FTMerTeamCode"
            cmd &= vbCrLf & ", STUFF((Select DISTINCT ','+ B.FTSizeBreakDown From HITECH_SAMPLEROOM.dbo.TSMPOrder_Breakdown AS B "
            cmd &= vbCrLf & "Where B.FTSMPOrderNo = A.FTSMPOrderNo For Xml PATH('')),1,1,'') AS FTSize "
            cmd &= vbCrLf & ", SUM(A.FNQuantity) AS FNQuantity"
            cmd &= vbCrLf & ", A.FTRemark"
            cmd &= vbCrLf & ", A.OrderStatus_Hide AS 'OrderStatus_Hide', C.FTCmpCode"

            ' Start As A
            'OD.FNSeq,
            cmd &= vbCrLf & vbCrLf
            cmd &= vbCrLf & "FROM (Select A.FTSMPOrderNo, A.FDSMPOrderDate, A.FNSMPOrderType, A.FNSMPPrototypeNo, MST.FTStyleCode, MSS.FTSeasonCode "
            cmd &= vbCrLf & ", MCT.FTCustCode, MCT.FTCustNameTH, MCT.FTCustNameEN, MMT.FTMerTeamCode,  OD.FTSizeBreakDown "
            cmd &= vbCrLf & ", OD.FNQuantity, OD.FTRemark "
            cmd &= vbCrLf & ", MST.FTStyleNameEN AS 'FTStyleName', GD.FTGenderCode, GD.FTGenderNameEN  AS 'FTGenderName' "
            cmd &= vbCrLf & ", A.FTSMPOrderBy, B.FTBuyCode, A.FTPgmName "
            cmd &= vbCrLf & ", CASE WHEN ISNULL(A.FTRemark,'') <> '' THEN ISNULL(A.FTRemark,'') + char(30) ELSE  '' END AS FTOrderRemark "
            cmd &= vbCrLf & ", OD.FTPatternDate, OD.FTFabricDate, OD.FTAccessoryDate, A.FNOrderSampleType "
            cmd &= vbCrLf & ", A.FTCustomerTeam, ISNULL(A.FNSMPOrderStatus,0) AS 'OrderStatus_Hide', OD.FTGACDate , A.FNHSysCmpId"
            cmd &= vbCrLf
            cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As A With(NOLOCK) "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As MSS With(NOLOCK)  On A.FNHSysSeasonId = MSS.FNHSysSeasonId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As MCT With(NOLOCK)  On A.FNHSysCustId = MCT.FNHSysCustId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam As MMT With(NOLOCK)  On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown As OD With(NOLOCK)  On A.FTSMPOrderNo = OD.FTSMPOrderNo"
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B  With(NOLOCK)  On A.FNHSysBuyId=B.FNHSysBuyId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender As GD With(NOLOCK)  On A.FNHSysGenderId = GD.FNHSysGenderId "

            cmd &= vbCrLf & "WHERE A.FTSMPOrderNo <> '' "

            If FTPayYear.Text <> "" Then
                cmd &= vbCrLf & " And A.FDSMPOrderDate BETWEEN '" + FTPayYear.Text + "/01/01' AND '" + FTPayYear.Text + "/12/31' "
            End If

            If FNHSysCustId.Text <> "" Then
                cmd &= vbCrLf & " AND A.FNHSysCustId=" & Val(FNHSysCustId.Properties.Tag.ToString) & ""
            End If

            If FNHSysStyleId.Text <> "" Then
                cmd &= vbCrLf & " AND A.FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & ""
            End If

            If FNHSysSeasonId.Text <> "" Then
                cmd &= vbCrLf & " AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""
            End If

            If FNHSysMerTeamId.Text <> "" Then
                cmd &= vbCrLf & " AND A.FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & ""
            End If


            If FTStartOrderDate.Text <> "" And FTEndOrderDate.Text <> "" Then
                cmd &= vbCrLf & " AND A.FDSMPOrderDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) &
                    "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
            End If

            If FTStartReqDate.Text <> "" And FTEndReqDate.Text <> "" Then
                cmd &= vbCrLf & " AND (OD.FTPatternDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FTStartReqDate.Text) &
                    "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndReqDate.Text) & "')"
            End If

            If FNHSysBuyIdFrom.Text <> "" And FNHSysBuyIdTo.Text <> "" Then
                cmd &= vbCrLf & " AND B.FTBuyCode BETWEEN '" & FNHSysBuyIdFrom.Text & "' AND '" & FNHSysBuyIdTo.Text & "'"
            End If

            cmd &= vbCrLf & ") As A "
            'End As A
            cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 C.FTCmpCode FROM "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE A.FNHSysCmpId = C.FNHSysCmpId) AS C "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY ( SELECT SUM(FNSam) AS FNSam "
            cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam As SSAM With (NOLOCK) "
            cmd &= vbCrLf & "WHERE (SSAM.FTSMPOrderNo =A.FTSMPOrderNo) ) AS XSAM "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY( SELECT TOP 1 PT.TPTNOrderBy, PT.FTPTNDate, PT.FTActPTNDate, PT.FNHSysPTNTypeId, PT.FNHSysPTNGrpTypeId "
            cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder AS PT WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE A.FTSMPOrderNo = PT.FTOrderNo) AS PT"
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN ( SELECT FNListIndex,FTNameTH,FTNameEN FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "WHERE (FTListName = N'FNSMPOrderType') ) AS XT ON  A.FNSMPOrderType =XT.FNListIndex "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN ( SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "Where (FTListName = N'FNOrderSampleType') ) As XTOT On  A.FNOrderSampleType = XTOT.FNListIndex"
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN ( SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "Where (FTListName = N'FNSMPOrderStatus') ) As XTOTST On  A.OrderStatus_Hide = XTOTST.FNListIndex"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT ul.FNHSysEmpID, ul.FTUserDescriptionEN, ul.FTUserDescriptionTH "
            cmd &= vbCrLf & "From HITECH_SECURITY.dbo.TSEUserLogin AS ul WITH (NOLOCK) WHERE PT.TPTNOrderBy = ul.FTUserName) AS ul"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT e.FTEmpNameEN,e.FTEmpSurnameEN, e.FTEmpNameTH,e.FTEmpSurnameTH, e.FTEmpCode, e.FNHSysEmpTypeId, e.FNHSysDivisonId, e.FNHSysSectId, e.FNHSysUnitSectId, e.FNHSysPositId  "
            cmd &= vbCrLf & "FROM HITECH_HR.dbo.THRMEmployee AS e WITH (NOLOCK) WHERE e.FNHSysEmpID = ul.FNHSysEmpID) AS e"
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan As SMPMP With(NOLOCK) "
            cmd &= vbCrLf & "On A.FTSMPOrderNo = SMPMP.FTSMPOrderNo "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 us.FTUnitSectNameEN, us.FTUnitSectNameTH "
            cmd &= vbCrLf & "From HITECH_HR.dbo.HRDV_TCNMUnitSect AS us WITH (NOLOCK) WHERE e.FNHSysUnitSectId = us.FNHSysUnitSectId) AS us"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY(SELECT TOP 1 ptg.FTPatternGrpTypeCode, ptg.FTLeadTime, ptg.FTPatternGrpTypeNameEN, ptg.FTPatternGrpTypeNameTH FROM "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPTNMGroupType AS ptg WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE PT.FNHSysPTNGrpTypeId = ptg.FNHSysPatternGrpTypeId) AS ptg "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY(SELECT TOP 1 ptt.FTPatternTypeCode, ptt.FTPatternTypeNameEN, ptt.FTPatternTypeNameTH FROM "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPTNMType AS ptt WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE PT.FNHSysPTNTypeId = ptt.FNHSysPatternTypeId) AS ptt "
            cmd &= vbCrLf
            cmd &= vbCrLf & "GROUP BY A.FTSMPOrderNo, A.OrderStatus_Hide, A.FDSMPOrderDate,PT.FTActPTNDate "
            cmd &= vbCrLf & ", A.FTStyleName, A.FTGenderCode, A.FTGenderName, A.FTSMPOrderBy, A.FTOrderRemark, A.FTBuyCode, A.FTPgmName, A.FTPatternDate "
            cmd &= vbCrLf & ", SMPMP.FTActPatternDate, SMPMP.FTCFMSendSampleDate, A.FTGACDate, XSAM.FNSam, A.FTCustomerTeam "
            cmd &= vbCrLf & ", A.FNSMPPrototypeNo, A.FTStyleCode, A.FTSeasonCode, A.FTCustCode, ptg.FTPatternGrpTypeCode, ptg.FTLeadTime "
            cmd &= vbCrLf & ", A.FTMerTeamCode, A.FTRemark, C.FTCmpCode,PT.TPTNOrderBy, ptt.FTPatternTypeCode, PT.FNHSysPTNTypeId, PT.FNHSysPTNGrpTypeId"
            cmd &= vbCrLf & ", XT.FTNameTH, A.FTCustNameTH, XTOT.FTNameTH, XTOTST.FTNameTH "
            cmd &= vbCrLf & ", ptg.FTPatternGrpTypeNameTH, ptt.FTPatternTypeNameTH "
            cmd &= vbCrLf & ", XT.FTNameEN, A.FTCustNameEN, XTOT.FTNameEN, XTOTST.FTNameEN "
            cmd &= vbCrLf & ", ptg.FTPatternGrpTypeNameEN, ptt.FTPatternTypeNameEN "
            cmd &= vbCrLf & ", us.FTUnitSectNameEN,us.FTUnitSectNameTH"
        End If

        '-----------------------------------------------------------------------------------------------
        If chkSample.Checked And chkProd.Checked Then
            cmd &= vbCrLf
            cmd &= vbCrLf & "UNION ALL"
            cmd &= vbCrLf
        End If

        If chkProd.Checked Then
            cmd &= vbCrLf & "SELECT '" & HI.ST.SysInfo.CmpID & "' AS 'FNHSysCmpID' "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & ", ptt.FTPatternTypeNameTH AS 'FTPatternType',ptt.FTPatternTypeNameTH AS 'FTPatternType_Hide' "
                cmd &= vbCrLf & ", ptg.FTPatternGrpTypeNameTH AS 'FTPtnGrpName',ptg.FTPatternGrpTypeNameTH AS 'FTPtnGrpName_Hide' "
                cmd &= vbCrLf & ", us.FTUnitSectNameTH AS 'PatternTeam' "
            Else
                cmd &= vbCrLf & ", ptt.FTPatternTypeNameEN AS 'FTPatternType',ptt.FTPatternTypeNameEN AS 'FTPatternType_Hide' "
                cmd &= vbCrLf & ", ptg.FTPatternGrpTypeNameEN AS 'FTPtnGrpName',ptg.FTPatternGrpTypeNameEN AS 'FTPtnGrpName_Hide' "
                cmd &= vbCrLf & ", us.FTUnitSectNameEN AS 'PatternTeam' "
            End If

            cmd &= vbCrLf & ", PT.FNHSysPTNTypeId AS 'FNHSysPatternTypeId', PT.FNHSysPTNTypeId AS 'FNHSysPatternTypeId_Hide' "
            cmd &= vbCrLf & ", PT.FNHSysPTNGrpTypeId AS 'FNHSysPatternGrpTypeId', PT.FNHSysPTNGrpTypeId AS 'FNHSysPatternGrpTypeId_Hide' "
            cmd &= vbCrLf & ", ptg.FTLeadTime AS 'FTLeadTime', ptg.FTLeadTime AS 'FTLeadTime_Hide' "
            cmd &= vbCrLf & ", 'PRODUCTION' AS 'Category', PT.TPTNOrderBy AS 'TPTNOrderBy' "
            cmd &= vbCrLf & ", ISNULL(A.FTOrderNo,'') AS 'FTSMPOrderNo'"
            cmd &= vbCrLf & ", Case When ISDATE(A.FDInsDate) = 1 Then  ISNULL(CONVERT(varchar(10),convert(Datetime,A.FDInsDate),103),'') Else NULL END  AS 'FDSMPOrderDate' "
            cmd &= vbCrLf & ", DatePart(week, Convert(DateTime, A.FDInsDate)+30) AS 'PlanFinishWeek'"
            cmd &= vbCrLf & ", DatePart(Year, Convert(DateTime, A.FDInsDate)+30) AS 'PlanFinishYear'"
            cmd &= vbCrLf & ", DatePart(week, Convert(DateTime, A.FDInsDate)+35) AS 'ActFinishWeek'"
            cmd &= vbCrLf & ", DatePart(Year, Convert(DateTime, A.FDInsDate)+35) AS 'ActFinishYear'"
            cmd &= vbCrLf & ", CONVERT(varchar(10),DatePart(week, Convert(DateTime, A.FDInsDate)+30)) + '-' + CONVERT(varchar(10),DatePart(Year, Convert(DateTime, A.FDInsDate)+30)) AS 'PlanFinishWY'"
            cmd &= vbCrLf & ", CONVERT(varchar(10),DatePart(week, Convert(DateTime, A.FDInsDate)+35)) + '-' + CONVERT(varchar(10),DatePart(Year, Convert(DateTime, A.FDInsDate)+35))  AS 'ActFinishWY'"
            cmd &= vbCrLf & ", ISNULL(MST.FTStyleNameEN,'') AS 'FTStyleName', ISNULL(GD.FTGenderCode,'') AS 'FTGenderCode' "
            cmd &= vbCrLf & ", ISNULL(GD.FTGenderNameEN,'') AS 'FTGenderName', ISNULL(A.FTOrderBy,'') AS 'FTSMPOrderBy' "
            cmd &= vbCrLf & ", ISNULL(A.FTRemark,'') AS 'FTOrderRemark', B.FTBuyCode AS 'FTBuyCode' "
            cmd &= vbCrLf & ", ISNULL(A.FTSubPgm,'') AS 'FTPgmName'  "
            cmd &= vbCrLf & ", PT.FTPTNDate AS 'FTPTNDate' "
            cmd &= vbCrLf & ", PT.FTActPTNDate AS 'FTActPTNDate' "
            cmd &= vbCrLf & ", '' AS 'FTCFMSendSampleDate' "
            cmd &= vbCrLf & ", CASE WHEN ISDATE(S.FDShipDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,S.FDShipDate),103),'') Else NULL END AS 'FDGacDate' "
            cmd &= vbCrLf & ", CASE WHEN ISDATE(S.FDShipDateOrginal) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,S.FDShipDateOrginal),103),'') Else NULL END AS 'OGacDate' "
            cmd &= vbCrLf & ", 0 AS 'FNSMPSam', '' AS 'FTCustomerTeam' "
            cmd &= vbCrLf & ", ISNULL(A.FNHSysProdTypeId,'') AS 'FNSMPPrototypeNo', ISNULL(MST.FTStyleCode,'') AS 'FTStyleCode' "
            cmd &= vbCrLf & ", ISNULL(MSS.FTSeasonNameEN,'') AS 'FTSeasonCode' "
            cmd &= vbCrLf & ", ISNULL(MCT.FTCustCode,'') AS 'FTCustCode' "
            cmd &= vbCrLf & ", CASE WHEN MCT.FTCustCode = 'NI' THEN 'NIKE' ELSE 'NON NIKE' END AS 'GroupCust' "
            cmd &= vbCrLf & ", CASE WHEN MCT.FTCustCode = 'NI' THEN 'NIKE' WHEN (MCT.FTCustCode = 'ZS' OR  MCT.FTCustCode = 'FNT') "
            cmd &= vbCrLf & "THEN 'TRACK ON NIKE' ELSE 'NON NIKE' END AS 'SubGroupCust' "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & ", ISNULL(ODT.FTNameTH,'') AS FNSMPOrderType "
                cmd &= vbCrLf & ", MCT.FTCustNameTH AS FTCustName "
                cmd &= vbCrLf & ", '' AS FNOrderSampleType "
                cmd &= vbCrLf & ", ISNULL(XT.FTNameTH,'') AS FNSMPOrderStatus "
            Else
                cmd &= vbCrLf & ", ISNULL(ODT.FTNameEN,'') AS FNSMPOrderType "
                cmd &= vbCrLf & ", MCT.FTCustNameEN AS FTCustName "
                cmd &= vbCrLf & ", '' AS FNOrderSampleType "
                cmd &= vbCrLf & ", ISNULL(XT.FTNameEN,'') AS FNSMPOrderStatus "
            End If

            cmd &= vbCrLf & ", ISNULL(MMT.FTMerTeamCode,'') AS 'FTMerTeamCode' "

            cmd &= vbCrLf & ", STUFF((Select DISTINCT ','+ B.FTSizeBreakDown "
            cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub_BreakDown AS B "
            cmd &= vbCrLf & "where B.FTOrderNo = A.FTOrderNo  FOR XML PATH('')),1,1,'') AS FTSize " 'And SB.FTColorway = B.FTColorway

            cmd &= vbCrLf & ", ISNULL(SUM(SB.FNQuantity),0) AS 'FNQuantity' " ', ISNULL(SB.FTColorway,'') AS 'FTColorway'
            cmd &= vbCrLf & ", A.FTRemark AS 'FTRemark', A.FNJobState AS 'OrderStatus_Hide', C.FTCmpCode "

            cmd &= vbCrLf & vbCrLf
            cmd &= vbCrLf & "From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS A WITH (NOLOCK) "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY( SELECT TOP 1 PT.TPTNOrderBy, PT.FTPTNDate, PT.FTActPTNDate, PT.FNHSysPTNTypeId, PT.FNHSysPTNGrpTypeId  "
            cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TPTNOrder AS PT WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE PT.FTOrderNo = A.FTOrderNo) AS PT"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT C.FTCmpCode FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp AS C WITH (NOLOCK) WHERE A.FNHSysCmpId = C.FNHSysCmpId) AS C"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT * FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub_BreakDown AS SB WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE A.FTOrderNo = SB.FTOrderNo) AS SB "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT * FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub AS S WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE A.FTOrderNo = S.FTOrderNo) AS S "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason As MSS With(NOLOCK) On A.FNHSysSeasonId = MSS.FNHSysSeasonId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCustomer As MCT With(NOLOCK) On A.FNHSysCustId = MCT.FNHSysCustId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMerTeam As MMT With(NOLOCK) On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMGender As GD With(NOLOCK) On S.FNHSysGenderId = GD.FNHSysGenderId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMBuy AS B  With(NOLOCK) On A.FNHSysBuyId=B.FNHSysBuyId "
            cmd &= vbCrLf
            cmd &= vbCrLf & "LEFT OUTER JOIN ( Select FNListIndex,FTNameTH,FTNameEN  FROM  "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "WHERE  (FTListName = N'FNJobState')  ) AS XT ON  A.FNJobState = XT.FNListIndex "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY ( Select FNListIndex,FTNameTH,FTNameEN  FROM  "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "WHERE (FTListName = N'FNOrderSetType'  AND  A.FNOrderType = XT.FNListIndex )) AS ODT"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 ul.FNHSysEmpID, ul.FTUserDescriptionEN, ul.FTUserDescriptionTH "
            cmd &= vbCrLf & "From HITECH_SECURITY.dbo.TSEUserLogin AS ul WITH (NOLOCK) WHERE PT.TPTNOrderBy = ul.FTUserName) AS ul"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 e.FTEmpNameEN,e.FTEmpSurnameEN, e.FTEmpNameTH,e.FTEmpSurnameTH, e.FTEmpCode, e.FNHSysEmpTypeId, e.FNHSysDivisonId, e.FNHSysSectId, e.FNHSysUnitSectId, e.FNHSysPositId  "
            cmd &= vbCrLf & "FROM HITECH_HR.dbo.THRMEmployee AS e WITH (NOLOCK) WHERE e.FNHSysEmpID = ul.FNHSysEmpID) AS e"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 us.FTUnitSectNameEN, us.FTUnitSectNameTH "
            cmd &= vbCrLf & "From HITECH_HR.dbo.HRDV_TCNMUnitSect AS us WITH (NOLOCK) WHERE e.FNHSysUnitSectId = us.FNHSysUnitSectId) AS us"
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY(SELECT TOP 1 ptg.FTPatternGrpTypeCode, ptg.FTLeadTime, ptg.FTPatternGrpTypeNameEN, ptg.FTPatternGrpTypeNameTH FROM "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPTNMGroupType AS ptg WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE PT.FNHSysPTNGrpTypeId = ptg.FNHSysPatternGrpTypeId) AS ptg "
            cmd &= vbCrLf
            cmd &= vbCrLf & "OUTER APPLY(SELECT TOP 1 ptt.FTPatternTypeCode, ptt.FTPatternTypeNameEN, ptt.FTPatternTypeNameTH FROM "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPTNMType AS ptt WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE PT.FNHSysPTNTypeId = ptt.FNHSysPatternTypeId) AS ptt "
            cmd &= vbCrLf

            cmd &= vbCrLf & "WHERE A.FTOrderNo <> '' "

            If (FTPayYear.Text <> "") Then
                cmd &= vbCrLf & " And A.FDOrderDate BETWEEN '" + FTPayYear.Text + "/01/01' AND '" + FTPayYear.Text + "/12/31' "
            End If

            If (FNHSysCustId.Text <> "") Then
                cmd &= vbCrLf & " AND A.FNHSysCustId=" & Val(FNHSysCustId.Properties.Tag.ToString) & " "
            End If

            If (FNHSysStyleId.Text <> "") Then
                cmd &= vbCrLf & " AND A.FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
            End If

            If (FNHSysSeasonId.Text <> "") Then
                cmd &= vbCrLf & " AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & " "
            End If

            If (FNHSysMerTeamId.Text <> "") Then
                cmd &= vbCrLf & " AND A.FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & " "
            End If

            If (FTStartOrderDate.Text <> "" And FTEndOrderDate.Text <> "") Then
                cmd &= vbCrLf & " AND A.FDOrderDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text)
                cmd &= "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "' "
            End If

            If (FNHSysBuyIdFrom.Text <> "" And FNHSysBuyIdTo.Text <> "") Then
                cmd &= vbCrLf & "  AND B.FTBuyCode BETWEEN '" & FNHSysBuyIdFrom.Text & "' AND '" & FNHSysBuyIdTo.Text & "' "
            End If

            cmd &= vbCrLf & vbCrLf & " GROUP BY A.FTOrderNo, A.FTStateOrderApp, A.FDInsDate, GD.FTGenderCode, A.FTOrderBy "
            cmd &= vbCrLf & ", A.FTRemark, A.FTSubPgm, B.FTBuyCode, S.FTOther1Note, A.FNHSysProdTypeId, MST.FTStyleCode "
            cmd &= vbCrLf & ", MCT.FTCustCode, MMT.FTMerTeamCode, A.FNJobState, A.FTStateBy, C.FTCmpCode "
            cmd &= vbCrLf & ", PT.FTPTNDate, PT.FTActPTNDate, S.FDShipDate, S.FDShipDateOrginal, PT.TPTNOrderBy,A.FNJobState "
            cmd &= vbCrLf & ", ptg.FTPatternGrpTypeCode, ptg.FTLeadTime, ptt.FTPatternTypeCode, PT.FNHSysPTNTypeId, PT.FNHSysPTNGrpTypeId "
            cmd &= vbCrLf & ", ODT.FTNameTH, XT.FTNameTH, MCT.FTCustNameTH, MST.FTStyleNameTH, GD.FTGenderNameTH, MSS.FTSeasonNameTH "
            cmd &= vbCrLf & ", ptt.FTPatternTypeNameTH,us.FTUnitSectNameTH, ul.FTUserDescriptionTH, ptg.FTPatternGrpTypeNameTH  "
            cmd &= vbCrLf & ", ODT.FTNameEN, XT.FTNameEN, MCT.FTCustNameEN, MST.FTStyleNameEN, GD.FTGenderNameEN, MSS.FTSeasonNameEN "
            cmd &= vbCrLf & ", ptt.FTPatternTypeNameEN, us.FTUnitSectNameEN, ul.FTUserDescriptionEN, ptg.FTPatternGrpTypeNameEN "

        End If

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogcPattern.DataSource = _dtprod.Copy

        _dtprod.Dispose()
    End Sub

    Private Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < -1 Then Exit Sub

                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
                    Exit Sub
                End If

                Dim _TDate As String
                Me.ocmsave.Visible = True
                Try

                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)

                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                        .ClearColumnsFilter()
                    End If
                    If _TDate = "01/01/0001" Then
                        _TDate = ""
                        .ClearColumnsFilter()
                    End If

                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate

                Try
                    If _TDate <> "" Then
                        CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                    Else
                        CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = Nothing
                    End If

                Catch ex As Exception
                End Try

                If _TDate = "" Then
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, "")
                Else
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))
                End If

                Dim NewData As String = HI.UL.ULDate.ConvertEN(_TDate)
                If NewData <> GridDataBefore Then

                    'Dim Category As String = .GetRowCellValue(.FocusedRowHandle, "Category").ToString()
                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                    'Dim Color As String = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString()
                    Dim FTActPatternDate As String = .GetRowCellValue(.FocusedRowHandle, "FTActPTNDate").ToString()
                    'Dim Size As String = .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString()
                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    'If (HI.UL.ULF.rpQuoted(Category).Equals("SAMPLEROOM")) Then
                    '    cmdstring = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan Set "
                    '    cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    '    cmdstring &= vbCrLf & "," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '    cmdstring &= vbCrLf & "," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB & ""
                    '    cmdstring &= vbCrLf & "," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB & " "
                    '    cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                    '    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                    '    End If

                    'ElseIf (HI.UL.ULF.rpQuoted(Category).Equals("PRODUCTION")) Then

                    cmdstring = "BEGIN"
                    cmdstring &= vbCrLf & "If EXISTS(SELECT FTOrderNo FROM [HITECH_SAMPLEROOM].dbo.TPTNOrder WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "')  "
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "  BEGIN"
                    cmdstring &= vbCrLf & "    UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder Set "
                    cmdstring &= vbCrLf & "    " & FieldName & "='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & "    ," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    ," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    ," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & "    ,FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    ,FDUpdDate = " & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    ,FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & "    ,TPTNOrderBy = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "  END"
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "ELSE"
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "  BEGIN"
                    cmdstring &= vbCrLf & "    INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder  "
                    cmdstring &= vbCrLf & "    (FTOrderNo, FTActPTNDate, TPTNOrderBy,FTActPTNDateUser, FTActPTNDateDate, FTActPTNDateTime,FTInsUser,FDInsDate,FTInsTime) "
                    cmdstring &= vbCrLf & "    VALUES "
                    cmdstring &= vbCrLf & "    ('" & HI.UL.ULF.rpQuoted(OrderNo) & "','" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & "    ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    ," & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    ," & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & "    ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    ," & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    ," & HI.UL.ULDate.FormatTimeDB & ")"
                    cmdstring &= vbCrLf & "  END"
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "END"

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                    End If

                    'End If

                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ItemDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Exit Sub
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
                If _TDate = "" Then
                    Beep()
                End If
                If _TDate = "0001/01/01" Then
                    _TDate = ""
                    .ClearColumnsFilter()
                End If
                If _TDate = "01/01/0001" Then
                    _TDate = ""
                    .ClearColumnsFilter()
                End If
                Try
                    If _TDate = "" Then
                        CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
                        .ClearColumnsFilter()
                    Else
                        CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                    End If

                Catch ex As Exception
                    CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
                    .ClearColumnsFilter()
                End Try

                GridDataBefore = _TDate

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ItemString_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                GridDataBefore = (.GetFocusedRowCellValue(.FocusedColumn))
                'Val(FNHSysSeasonId.Properties.Tag.ToString)

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ItemString_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If .FocusedRowHandle < -1 Then Exit Sub

                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
                    Exit Sub
                End If

                Dim _PatternType As String
                Me.ocmsave.Visible = True
                _PatternType = CType(sender, DevExpress.XtraEditors.ButtonEdit).Text

                Dim NewData As String = _PatternType
                If NewData <> GridDataBefore Then

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                    Dim FNHSysPatternTypeId As String = .GetRowCellValue(.FocusedRowHandle, "FTPatternType_Hide").ToString()
                    Dim FNHSysPatternGrpTypeId As String = .GetRowCellValue(.FocusedRowHandle, "FTPtnGrpName_Hide").ToString()
                    Dim FTLeadTime As String = .GetRowCellValue(.FocusedRowHandle, "FTLeadTime").ToString()
                    Dim FieldName As String = .FocusedColumn.Name.ToString
                    'FieldName.ToString

                    Dim cmdstring As String = ""
                    cmdstring = "BEGIN"
                    cmdstring &= vbCrLf & " If EXISTS(SELECT FTOrderNo FROM [HITECH_SAMPLEROOM].dbo.TPTNOrder WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "')  "
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & " BEGIN"
                    cmdstring &= vbCrLf & "   UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder Set "
                    cmdstring &= vbCrLf & "   FNHSysPTNTypeId ='" & FNHSysPatternTypeId & "'"
                    cmdstring &= vbCrLf & "   , FNHSysPTNGrpTypeId = '" & FNHSysPatternGrpTypeId & "'"
                    cmdstring &= vbCrLf & "   , TPTNOrderBy = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "   , FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "   , FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "   , FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & "   WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    'cmdstring &= vbCrLf & " " & FieldName & "='" & GridDataBefore & "'"
                    cmdstring &= vbCrLf & " END"
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "ELSE"
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & " BEGIN"
                    cmdstring &= vbCrLf & "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder  "
                    cmdstring &= vbCrLf & "   (FTOrderNo, TPTNOrderBy, FNHSysPTNTypeId, FNHSysPTNGrpTypeId, FTInsUser, FDInsDate, FTInsTime) "
                    'cmdstring &= vbCrLf & "(FTOrderNo, " & FieldName & ",FTInsUser,FDInsDate,FTInsTime) "
                    cmdstring &= vbCrLf & "   VALUES "
                    cmdstring &= vbCrLf & "   ( '" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                    cmdstring &= vbCrLf & "   , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "   , '" & FNHSysPatternTypeId & "'"
                    cmdstring &= vbCrLf & "   , '" & FNHSysPatternGrpTypeId & "'"
                    cmdstring &= vbCrLf & "   , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "   , " & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "   , " & HI.UL.ULDate.FormatTimeDB & " )"
                    cmdstring &= vbCrLf & " END"
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "END"

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                    End If
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    'Private Sub ItemPosition_Leave(sender As Object, e As System.EventArgs)
    '    Try
    '        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

    '            If .FocusedRowHandle < -1 Then Exit Sub

    '            If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
    '                Exit Sub
    '            End If

    '            Dim _PositionCode As String
    '            Me.ocmsave.Visible = True
    '            _PositionCode = CType(sender, DevExpress.XtraEditors.ButtonEdit).EditValue
    '            'Dim _FNHSysEmpID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString()
    '            CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = _PositionCode


    '            Dim cmdstring As String = ""
    '            cmdstring = Val(sender.Properties.Tag.ToString)


    '        End With

    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub ItemTypeCode_Leave(sender As Object, e As System.EventArgs)
    '    Try
    '        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

    '            If .FocusedRowHandle < -1 Then Exit Sub

    '            If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
    '                Exit Sub
    '            End If

    '            Dim _TypeCode As String
    '            Me.ocmsave.Visible = True
    '            _TypeCode = CType(sender, DevExpress.XtraEditors.ButtonEdit).EditValue
    '            'Dim _FNHSysEmpID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString()
    '            CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = _TypeCode


    '            Dim cmdstring As String = ""
    '            cmdstring = Val(sender.Properties.Tag.ToString)


    '        End With

    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub ItemGrpTypeCode_Leave(sender As Object, e As System.EventArgs)
    '    Try
    '        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

    '            If .FocusedRowHandle < -1 Then Exit Sub

    '            If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
    '                Exit Sub
    '            End If

    '            Dim _GrpTypeCode As String
    '            Me.ocmsave.Visible = True
    '            _GrpTypeCode = CType(sender, DevExpress.XtraEditors.ButtonEdit).EditValue
    '            'Dim _FNHSysEmpID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString()
    '            CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = _GrpTypeCode


    '            Dim cmdstring As String = ""



    '        End With

    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub ogvoperation_CellMerge(sender As Object, e As CellMergeEventArgs)
        Try
            With Me.ogvPattern
                Select Case e.Column.FieldName
                    Case "FTSMPOrderBy", "FTMerTeamCode", "FTCustName", "FTCustCode", "FTGenderCode", "FTCustomerTeam", "FNOrderSampleType", "FNSMPPrototypeNo", "FNSMPSam", "FNSMPOrderType", "FTSeasonCode", "FTStyleName", "FTStyleCode", "FTStateReceiptDate", "FDSMPOrderDate"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTSMPOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSMPOrderNo").ToString) _
                             And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTSMPOrderNo"

                        If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                        'Case "FTOrderRemark"
                        '    e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                    Case Else
                        e.Merge = False
                        e.Handled = True

                End Select

            End With

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub ogvoperation_RowStyle(sender As Object, e As RowStyleEventArgs)
    '    Try
    '        With Me.ogvPattern
    '            If (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide")) = 2) Then

    '                e.Appearance.ForeColor = System.Drawing.Color.Red

    '            ElseIf (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide")) = 1) Then

    '                e.Appearance.ForeColor = System.Drawing.Color.Green

    '            End If
    '            'e.Appearance.ForeColor = If(Val(.GetRowCellValue(e.RowHandle, "OrderStatus")) = 2, System.Drawing.Color.Red, System.Drawing.Color.Black)
    '            'e.Appearance.ForeColor = If(Val(.GetRowCellValue(e.RowHandle, "OrderStatus")) = 1, System.Drawing.Color.Green, System.Drawing.Color.Black)

    '        End With

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs)
        Try
            If _FocusedRowHendle < -1 Then Exit Sub
            Try
                With CType(Me.ogvPattern, DevExpress.XtraGrid.Views.Grid.GridView)

                    Dim _TDate As String
                    .FocusedColumn = _FocusedColumn
                    .FocusedRowHandle = _FocusedRowHendle
                    Try

                        _TDate = .GetRowCellValue(_FocusedRowHendle, _FocusedColumn.FieldName)

                        If _TDate = "0001/01/01" Then
                            _TDate = ""
                            .ClearColumnsFilter()
                        End If

                    Catch ex As Exception
                        _TDate = ""
                        .ClearColumnsFilter()
                    End Try

                    CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate

                    Try
                        If _TDate <> "" Then
                            CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                        Else
                            CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = Nothing
                        End If

                    Catch ex As Exception
                    End Try

                    If _TDate = "" Then
                        .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, "")
                    Else
                        .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))
                    End If

                    Dim NewData As String = HI.UL.ULDate.ConvertEnDB(_TDate)
                    If NewData <> GridDataBefore Then
                        Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                        'Dim Color As String = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString()
                        Dim Size As String = .GetRowCellValue(.FocusedRowHandle, "FTSize").ToString()
                        Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    End If

                End With
                _FocusedRowHendle = -99
            Catch ex As Exception
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposDate_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposDate.EditValueChanging
        Try
            e.Cancel = If(e.NewValue.ToString = "0001/01/01", True, False)
            e.Cancel = If(e.NewValue.ToString = "01/01/0001", True, False)
            e.Cancel = If(e.NewValue.ToString = "", True, False)

        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub ogvPattern_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvPattern.RowStyle
        Try
            With Me.ogvPattern
                If (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide") = 2)) Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                ElseIf (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide") = 1)) Then
                    e.Appearance.ForeColor = System.Drawing.Color.Green
                ElseIf (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide") = 0)) Then
                    e.Appearance.ForeColor = System.Drawing.Color.Blue
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class
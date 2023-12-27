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


        With rFTEmpCode
            AddHandler .Leave, AddressOf ItemEmpCode_Leave
        End With

        With rFTPositCode
            AddHandler .Leave, AddressOf ItemPosition_Leave
        End With

        With rFTPatternTypeCode
            AddHandler .Leave, AddressOf ItemTypeCode_Leave
        End With


        With rFTPatternGrpTypeCode
            AddHandler .Leave, AddressOf ItemGrpTypeCode_Leave
        End With

        InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        Me.FNYear.Text = New Date.Now.Year
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

    Public Sub LoadDataInfo(ByVal Key As String)

        Dim _Qry As String = ""
        _Qry = " SELECT     SOP.FNSeq"

        _Qry &= vbCrLf & "  , Case When ISDATE(ISNULL( SOP.FTDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime, SOP.FTDate),103) ELSE '' END AS  FTDate "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNQuantity,0) As FNQuantity"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) As FNPass"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNNotPass,0) As FNNotPass"
        _Qry &= vbCrLf & " ,  ISNULL(SOP.FTRemark,'') AS FTRemark"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) +  ISNULL(SOP.FNNotPass,0) As FNTotalQC"

        _Qry &= vbCrLf & "  , ISNULL(SOP.FTSizeBreakDown,'') As FTSizeBreakDown"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FTColorway,'') As FTColorway"

        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC AS SOP WITH (NOLOCK)"
        _Qry &= vbCrLf & "   WHERE SOP.FTTeam='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC"
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcPattern.DataSource = _dt

    End Sub


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
            .Columns.ColumnByFieldName("FTActPatternDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            .Columns.ColumnByFieldName("FTPatternDate").OptionsColumn.AllowEdit = False

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
            cmd &= vbCrLf & "SELECT '" & HI.ST.SysInfo.CmpID & "' AS 'FNHSysCmpID', '" & HI.ST.SysInfo.CmpID & "' AS 'FNHSysCmpID_Hide', "
            cmd &= vbCrLf & "'' AS 'FTPTNTypeCode','' AS 'FTPTNTypeCode_Hide','' AS 'FTPTNGrpTypeCode','' AS 'FTPTNGrpTypeCode_Hide', "
            cmd &= vbCrLf & "'SAMPLEROOM' AS 'Category', "
            cmd &= vbCrLf & "'' AS 'FTPositCode', '' AS 'FTPositCode_Hide', '' AS 'FTEmpCode', '' AS 'FTEmpCode_Hide', "
            cmd &= vbCrLf & "A.FTSMPOrderNo, A.FNSMPOrderStatus As FNSMPOrderStatusState "
            cmd &= vbCrLf & ", CASE WHEN ISDATE(A.FDSMPOrderDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,A.FDSMPOrderDate),103),'') Else NULL END AS FDSMPOrderDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(A.FTDeliveryDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,A.FTDeliveryDate),103),'') Else NULL END AS FTDeliveryDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(A.FDSendToSMPDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,A.FDSendToSMPDate),103),'') Else NULL END AS FDSendToSMPDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(A.FTStateReceiptDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,A.FTStateReceiptDate),103),'') Else NULL END AS FTStateReceiptDate"
            cmd &= vbCrLf & ", A.FTStyleName"
            cmd &= vbCrLf & ", A.FTGenderCode "
            cmd &= vbCrLf & ", A.FTGenderName"
            cmd &= vbCrLf & ", A.FTSMPOrderBy "
            cmd &= vbCrLf & ", A.FTOrderRemark"
            cmd &= vbCrLf & ", A.FTBuyCode"
            cmd &= vbCrLf & ", A.FTPgmName"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(A.FTPatternDate) = 1 Then CONVERT(varchar(10),convert(Datetime,A.FTPatternDate),103) Else NULL END AS  FTPatternDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(SMPMP.FTActPatternDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,SMPMP.FTActPatternDate),103),'') Else NULL END AS  FTActPatternDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(SMPMP.FTCFMSendSampleDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,SMPMP.FTCFMSendSampleDate),103),'') Else NULL END AS  FTCFMSendSampleDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE(a.FTGACDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,a.FTGACDate),103),'') Else NULL END  AS  FDGacDate"
            cmd &= vbCrLf & ", CASE WHEN ISDATE('') = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,''),103),'') Else NULL END  AS OGacDate"
            cmd &= vbCrLf & ", SMPMP.FTStandBy"
            cmd &= vbCrLf & ", SMPMP.FTNote"
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
                cmd &= vbCrLf & ", ISNULL(XTOTST.FTNameTH,'') AS FNSMPOrderStatus"
            Else
                cmd &= vbCrLf & ", ISNULL(XT.FTNameEN,'') AS FNSMPOrderType"
                cmd &= vbCrLf & ", A.FTCustNameEN AS FTCustName"
                cmd &= vbCrLf & ", ISNULL(XTOT.FTNameEN,'') AS FNOrderSampleType"
                cmd &= vbCrLf & ", ISNULL(XTOTST.FTNameEN,'') AS FNSMPOrderStatus"
            End If

            cmd &= vbCrLf & ", A.FTMerTeamCode"
            ' FTSizeBreakDown
            cmd &= vbCrLf & ", STUFF((Select ','+ B.FTSizeBreakDown  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Breakdown AS B "
            cmd &= vbCrLf & "where B.FTSMPOrderNo = A.FTSMPOrderNo AND A.FTColorway = B.FTColorway "
            cmd &= vbCrLf & "ORDER BY B.FNSeq FOR XML PATH('')),1,1,'') AS FTSizeBreakDown "
            cmd &= vbCrLf & ", A.FTColorway"
            cmd &= vbCrLf & ", SUM(A.FNQuantity) AS FNQuantity"
            cmd &= vbCrLf & ", A.FTRemark"
            cmd &= vbCrLf & ", A.FNSMPOrderStatus, C.FTCmpCode"

            ' Start As A
            cmd &= vbCrLf & vbCrLf & " "
            cmd &= vbCrLf & "FROM (Select A.FTSMPOrderNo, A.FDSMPOrderDate, A.FNSMPOrderType, A.FNSMPPrototypeNo, MST.FTStyleCode, MSS.FTSeasonCode "
            cmd &= vbCrLf & ", MCT.FTCustCode, MCT.FTCustNameTH, MCT.FTCustNameEN, MMT.FTMerTeamCode, OD.FNSeq, OD.FTSizeBreakDown "
            cmd &= vbCrLf & ", OD.FTColorway, OD.FNQuantity, OD.FTDeliveryDate, OD.FTRemark, A.FTStateAppDate AS FDSendToSMPDate "
            cmd &= vbCrLf & ", A.FTStateReceiptDate, MST.FTStyleNameEN AS 'FTStyleName', GD.FTGenderCode, GD.FTGenderNameEN  AS 'FTGenderName' "
            cmd &= vbCrLf & ", A.FTSMPOrderBy, B.FTBuyCode, A.FTPgmName "
            cmd &= vbCrLf & ", CASE WHEN ISNULL(A.FTRemark,'') <> '' THEN ISNULL(A.FTRemark,'') + char(30) ELSE  '' END AS FTOrderRemark "
            cmd &= vbCrLf & ", OD.FTPatternDate, OD.FTFabricDate, OD.FTAccessoryDate, A.FNOrderSampleType "
            cmd &= vbCrLf & ", A.FTCustomerTeam, ISNULL(A.FNSMPOrderStatus,0) AS FNSMPOrderStatus, OD.FTGACDate "
            cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As A With(NOLOCK) "
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId "
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As MSS With(NOLOCK)  On A.FNHSysSeasonId = MSS.FNHSysSeasonId "
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As MCT With(NOLOCK)  On A.FNHSysCustId = MCT.FNHSysCustId "
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam As MMT With(NOLOCK)  On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId "
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown As OD With(NOLOCK)  On A.FTSMPOrderNo = OD.FTSMPOrderNo"
            cmd &= vbCrLf & "LEFT OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B  With(NOLOCK)  On A.FNHSysBuyId=B.FNHSysBuyId "
            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender As GD With(NOLOCK)  On A.FNHSysGenderId = GD.FNHSysGenderId "

            cmd &= vbCrLf & "OUTER APPLY( Select TOP 1 *  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TPTNOrder As pto With (NOLOCK) WHERE (pto.FTOrderNo = A.FTSMPOrderNo)) As pto "

            cmd &= vbCrLf & "WHERE A.FTSMPOrderNo <> '' "

            If FNYear.Text <> "" Then
                cmd &= vbCrLf & " And A.FDSMPOrderDate BETWEEN '" + FNYear.Text + "/01/01' AND '" + FNYear.Text + "/12/31' "
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
            cmd &= vbCrLf & "OUTER APPLY (SELECT C.FTCmpCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) WHERE FNHSysCmpId = C.FNHSysCmpId) AS C "

            cmd &= vbCrLf & "OUTER APPLY ( Select SUM(FNSam) AS FNSam FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam As SSAM With (NOLOCK) "
            cmd &= vbCrLf & "WHERE  (SSAM.FTSMPOrderNo =A.FTSMPOrderNo) ) AS XSAM "

            cmd &= vbCrLf & "LEFT OUTER JOIN ( Select FNListIndex,FTNameTH,FTNameEN FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "WHERE  (FTListName = N'FNSMPOrderType') ) AS XT ON  A.FNSMPOrderType =XT.FNListIndex "

            cmd &= vbCrLf & "LEFT OUTER JOIN ( Select FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "Where (FTListName = N'FNOrderSampleType') ) As XTOT On  A.FNOrderSampleType = XTOT.FNListIndex"

            cmd &= vbCrLf & "LEFT OUTER JOIN ( Select FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "Where (FTListName = N'FNSMPOrderStatus') ) As XTOTST On  A.FNSMPOrderStatus = XTOTST.FNListIndex"

            cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan As SMPMP With(NOLOCK) "
            cmd &= vbCrLf & "On A.FTSMPOrderNo =SMPMP.FTSMPOrderNo " 'And A.FTSizeBreakDown =SMPMP.FTSizeBreakDown And A.FTColorway=SMPMP.FTColorway "

            cmd &= vbCrLf & vbCrLf & "GROUP BY A.FTSMPOrderNo, A.FNSMPOrderStatus, A.FDSMPOrderDate, A.FTDeliveryDate, A.FDSendToSMPDate, A.FTStateReceiptDate, "
            cmd &= vbCrLf & "A.FTStyleName, A.FTGenderCode, A.FTGenderName, A.FTSMPOrderBy, A.FTOrderRemark, A.FTBuyCode, A.FTPgmName, A.FTPatternDate, "
            cmd &= vbCrLf & "SMPMP.FTActPatternDate, SMPMP.FTCFMSendSampleDate, A.FTGACDate, SMPMP.FTStandBy, SMPMP.FTNote, XSAM.FNSam, A.FTCustomerTeam, "
            cmd &= vbCrLf & "A.FNSMPPrototypeNo, A.FTStyleCode, A.FTSeasonCode, A.FTCustCode, "
            cmd &= vbCrLf & "A.FTMerTeamCode, A.FTColorway, A.FTRemark, C.FTCmpCode"
            cmd &= vbCrLf & " "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & ",XT.FTNameTH, A.FTCustNameTH, XTOT.FTNameTH, XTOTST.FTNameTH "
            Else
                cmd &= vbCrLf & ",XT.FTNameEN, A.FTCustNameEN, XTOT.FTNameEN, XTOTST.FTNameEN "
            End If
        End If

        '-----------------------------------------------------------------------------------------------
        If chkSample.Checked And chkProd.Checked Then
            cmd &= vbCrLf & " "
            cmd &= vbCrLf & " UNION ALL"
            cmd &= vbCrLf & " "
        End If

        If chkProd.Checked Then
            cmd &= vbCrLf & "SELECT '" & HI.ST.SysInfo.CmpID & "' AS 'FNHSysCmpID', '" & HI.ST.SysInfo.CmpID & "' AS 'FNHSysCmpID_Hide', "
            cmd &= vbCrLf & "'' AS 'FTPTNTypeCode','' AS 'FTPTNTypeCode_Hide','' AS 'FTPTNGrpTypeCode','' AS 'FTPTNGrpTypeCode_Hide', "
            cmd &= vbCrLf & "'PRODUCTION' AS 'Category','' AS 'FTPositCode', '' AS 'FTPositCode_Hide','' AS 'FTEmpCode', '' AS 'FTEmpCode_Hide', "
            cmd &= vbCrLf & "ISNULL(A.FTOrderNo,'') AS 'FTSMPOrderNo', ISNULL(A.FNJobState,'') AS 'FNSMPOrderStatusState' "
            cmd &= vbCrLf & ", Case When ISDATE(A.FDInsDate) = 1 Then  ISNULL(CONVERT(varchar(10),convert(Datetime,A.FDInsDate),103),'') Else NULL END  AS 'FDSMPOrderDate' "
            cmd &= vbCrLf & ", '' AS 'FTDeliveryDate' "
            cmd &= vbCrLf & ", '' AS 'FDSendToSMPDate' "
            cmd &= vbCrLf & ", '' AS 'FTStateReceiptDate' "
            cmd &= vbCrLf & ", ISNULL(MST.FTStyleNameEN,'') AS 'FTStyleName', ISNULL(GD.FTGenderCode,'') AS 'FTGenderCode' "
            cmd &= vbCrLf & ", ISNULL(GD.FTGenderNameEN,'') AS 'FTGenderName', ISNULL(A.FTOrderBy,'') AS 'FTSMPOrderBy' "
            cmd &= vbCrLf & ", ISNULL(A.FTRemark,'') AS 'FTOrderRemark', B.FTBuyCode AS 'FTBuyCode' "
            cmd &= vbCrLf & ", ISNULL(A.FTSubPgm,'') AS 'FTPgmName'  "
            cmd &= vbCrLf & ", PT.FTPatternDate AS 'FTPatternDate' "
            cmd &= vbCrLf & ", PT.FTActPatternDate AS 'FTActPatternDate' "
            cmd &= vbCrLf & ", '' AS 'FTCFMSendSampleDate' "
            cmd &= vbCrLf & ", CASE WHEN ISDATE(S.FDShipDate) = 1 Then ISNULL(CONVERT(varchar(10),convert(Datetime,S.FDShipDate),103),'') Else NULL END AS 'FDGacDate' "
            cmd &= vbCrLf & ", CASE WHEN ISDATE(S.FDShipDateOrginal) = 1 Then  ISNULL(CONVERT(varchar(10),convert(Datetime,S.FDShipDateOrginal),103),'') Else NULL END AS 'OGacDate' "

            cmd &= vbCrLf & ", '' AS 'FTStandBy',  ISNULL(S.FTOther1Note,'') AS 'FTNote' "
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

            cmd &= vbCrLf & ", STUFF((Select DISTINCT ','+ B.FTSizeBreakDown FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub_BreakDown AS B "
            cmd &= vbCrLf & "where B.FTOrderNo = A.FTOrderNo AND SB.FTColorway = B.FTColorway FOR XML PATH('')),1,1,'') AS FTSizeBreakDown "

            cmd &= vbCrLf & ", ISNULL(SB.FTColorway,'') AS 'FTColorway', ISNULL(SUM(SB.FNQuantity),0) AS 'FNQuantity' "
            cmd &= vbCrLf & ", A.FTRemark AS 'FTRemark', '' AS 'FNSMPOrderStatus', C.FTCmpCode "

            cmd &= vbCrLf & vbCrLf & " "
            cmd &= vbCrLf & "From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS A WITH (NOLOCK) "
            cmd &= vbCrLf & "OUTER APPLY( SELECT * FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TPTNOrder AS PT WITH (NOLOCK) WHERE PT.FTOrderNo = A.FTOrderNo) AS PT"
            cmd &= vbCrLf & "OUTER APPLY (SELECT C.FTCmpCode FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp AS C WITH (NOLOCK) WHERE A.FNHSysCmpId = C.FNHSysCmpId) AS C"
            cmd &= vbCrLf & "OUTER APPLY (SELECT * FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub_BreakDown AS SB WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE A.FTOrderNo = SB.FTOrderNo) AS SB "
            cmd &= vbCrLf & "OUTER APPLY (SELECT * FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub AS S WITH (NOLOCK) "
            cmd &= vbCrLf & "WHERE A.FTOrderNo = S.FTOrderNo) AS S "
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId "
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason As MSS With(NOLOCK) On A.FNHSysSeasonId = MSS.FNHSysSeasonId "
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCustomer As MCT With(NOLOCK) On A.FNHSysCustId = MCT.FNHSysCustId "
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMerTeam As MMT With(NOLOCK) On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId "
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMGender As GD With(NOLOCK) On S.FNHSysGenderId = GD.FNHSysGenderId "
            cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMBuy AS B  With(NOLOCK) On A.FNHSysBuyId=B.FNHSysBuyId "
            cmd &= vbCrLf & "LEFT OUTER JOIN ( Select FNListIndex,FTNameTH,FTNameEN  FROM  "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "WHERE  (FTListName = N'FNJobState')  ) AS XT ON  A.FNJobState = XT.FNListIndex "

            cmd &= vbCrLf & "OUTER APPLY ( Select FNListIndex,FTNameTH,FTNameEN  FROM  "
            cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
            cmd &= vbCrLf & "WHERE (FTListName = N'FNOrderSetType'  AND  A.FNOrderType = XT.FNListIndex )) AS ODT"

            cmd &= vbCrLf & "OUTER APPLY( Select TOP 1 *  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TPTNOrder As pto With (NOLOCK) WHERE (pto.FTOrderNo = A.FTOrderNo)) As pto "

            cmd &= vbCrLf & "WHERE A.FTOrderNo <> '' "

            If (FNYear.Text <> "") Then
                cmd &= vbCrLf & " And A.FDOrderDate BETWEEN '" + FNYear.Text + "/01/01' AND '" + FNYear.Text + "/12/31' "
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

            cmd &= vbCrLf & vbCrLf & " GROUP BY A.FTOrderNo, A.FTStateOrderApp, A.FDInsDate, GD.FTGenderCode, A.FTOrderBy, "
            cmd &= vbCrLf & " A.FTRemark, A.FTSubPgm, B.FTBuyCode, S.FTOther1Note, A.FNHSysProdTypeId, MST.FTStyleCode, "
            cmd &= vbCrLf & " MCT.FTCustCode, MMT.FTMerTeamCode, SB.FTColorway, A.FNJobState, A.FTStateBy, C.FTCmpCode,"
            cmd &= vbCrLf & " PT.FTPatternDate, PT.FTActPatternDate, S.FDShipDate, S.FDShipDateOrginal"
            cmd &= vbCrLf & " , MST.FTStyleNameEN, GD.FTGenderNameEN, MSS.FTSeasonNameEN "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & ", ODT.FTNameTH, XT.FTNameTH, MCT.FTCustNameTH "
            Else
                cmd &= vbCrLf & ", ODT.FTNameEN, XT.FTNameEN, MCT.FTCustNameEN "
            End If
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

                    Dim Category As String = .GetRowCellValue(.FocusedRowHandle, "Category").ToString()
                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                    Dim Color As String = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString()
                    Dim FTActPatternDate As String = .GetRowCellValue(.FocusedRowHandle, "FTActPatternDate").ToString()
                    'Dim Size As String = .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString()
                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    If (HI.UL.ULF.rpQuoted(Category).Equals("SAMPLEROOM")) Then
                        cmdstring = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan Set "
                        cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & "," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB & " "
                        cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                        End If

                    ElseIf (HI.UL.ULF.rpQuoted(Category).Equals("PRODUCTION")) Then

                        cmdstring = "BEGIN"
                        cmdstring &= vbCrLf & " If EXISTS(SELECT FTOrderNo FROM [HITECH_SAMPLEROOM].dbo.TPTNOrder WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "')  "
                        cmdstring &= vbCrLf & " "
                        cmdstring &= vbCrLf & "BEGIN"
                        cmdstring &= vbCrLf & "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder Set "
                        cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & "," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB & " "
                        cmdstring &= vbCrLf & ",FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & " "
                        cmdstring &= vbCrLf & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                        'AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "
                        cmdstring &= vbCrLf & "END"
                        cmdstring &= vbCrLf & " "
                        cmdstring &= vbCrLf & "ELSE"
                        cmdstring &= vbCrLf & " "
                        cmdstring &= vbCrLf & "BEGIN"
                        cmdstring &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder  "
                        cmdstring &= vbCrLf & "(FTOrderNo, FTActPatternDate, FTActPatternDateUser, FTActPatternDateDate, FTActPatternDateTime,FTInsUser,FDInsDate,FTInsTime) "
                        cmdstring &= vbCrLf & " VALUES "
                        cmdstring &= vbCrLf & "('" & HI.UL.ULF.rpQuoted(OrderNo) & "','" & HI.UL.ULF.rpQuoted(NewData) & "','"
                        cmdstring &= vbCrLf & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        cmdstring &= vbCrLf & HI.UL.ULDate.FormatDateDB & ","
                        cmdstring &= vbCrLf & HI.UL.ULDate.FormatTimeDB & ",'"
                        cmdstring &= vbCrLf & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        cmdstring &= vbCrLf & HI.UL.ULDate.FormatDateDB & ","
                        cmdstring &= vbCrLf & HI.UL.ULDate.FormatTimeDB & ")"
                        cmdstring &= vbCrLf & "END"
                        cmdstring &= vbCrLf & " "
                        cmdstring &= vbCrLf & "END"

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                        End If

                    End If

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

    Private Sub ItemEmpCode_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If .FocusedRowHandle < -1 Then Exit Sub

                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
                    Exit Sub
                End If

                Dim _EmpCode As String
                Me.ocmsave.Visible = True
                _EmpCode = CType(sender, DevExpress.XtraEditors.ButtonEdit).EditValue
                'Dim _FNHSysEmpID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString()
                CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = _EmpCode


                'If _TDate = "" Then
                '    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, "")
                'Else
                '    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))
                'End If

                'Dim NewData As String = HI.UL.ULDate.ConvertEN(_TDate)
                'If NewData <> GridDataBefore Then

                'Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                'Dim FieldName As String = .FocusedColumn.FieldName.ToString

                Dim cmdstring As String = ""


                'If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                'End If


            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ItemPosition_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If .FocusedRowHandle < -1 Then Exit Sub

                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
                    Exit Sub
                End If

                Dim _PositionCode As String
                Me.ocmsave.Visible = True
                _PositionCode = CType(sender, DevExpress.XtraEditors.ButtonEdit).EditValue
                'Dim _FNHSysEmpID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString()
                CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = _PositionCode


                Dim cmdstring As String = ""



            End With

        Catch ex As Exception
        End Try
    End Sub
    Private Sub ItemTypeCode_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If .FocusedRowHandle < -1 Then Exit Sub

                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
                    Exit Sub
                End If

                Dim _TypeCode As String
                Me.ocmsave.Visible = True
                _TypeCode = CType(sender, DevExpress.XtraEditors.ButtonEdit).EditValue
                'Dim _FNHSysEmpID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString()
                CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = _TypeCode


                Dim cmdstring As String = ""



            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ItemGrpTypeCode_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If .FocusedRowHandle < -1 Then Exit Sub

                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
                    Exit Sub
                End If

                Dim _GrpTypeCode As String
                Me.ocmsave.Visible = True
                _GrpTypeCode = CType(sender, DevExpress.XtraEditors.ButtonEdit).EditValue
                'Dim _FNHSysEmpID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString()
                CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = _GrpTypeCode


                Dim cmdstring As String = ""



            End With

        Catch ex As Exception
        End Try
    End Sub

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

    Private Sub ogvoperation_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            With Me.ogvPattern
                If (Val(.GetRowCellValue(e.RowHandle, "FNSMPOrderStatusState")) = 2) Then

                    e.Appearance.ForeColor = System.Drawing.Color.Red

                ElseIf (Val(.GetRowCellValue(e.RowHandle, "FNSMPOrderStatusState")) = 1) Then

                    e.Appearance.ForeColor = System.Drawing.Color.Green

                End If
                'e.Appearance.ForeColor = If(Val(.GetRowCellValue(e.RowHandle, "FNSMPOrderStatusState")) = 2, System.Drawing.Color.Red, System.Drawing.Color.Black)
                'e.Appearance.ForeColor = If(Val(.GetRowCellValue(e.RowHandle, "FNSMPOrderStatusState")) = 1, System.Drawing.Color.Green, System.Drawing.Color.Black)

            End With

        Catch ex As Exception

        End Try
    End Sub

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
                        Dim Color As String = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString()
                        Dim Size As String = .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString()
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
                If (Val(.GetRowCellValue(e.RowHandle, "FNSMPOrderStatusState")) = 2) Then

                    e.Appearance.ForeColor = System.Drawing.Color.Red

                ElseIf (Val(.GetRowCellValue(e.RowHandle, "FNSMPOrderStatusState")) = 1) Then

                    e.Appearance.ForeColor = System.Drawing.Color.Green

                End If
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class
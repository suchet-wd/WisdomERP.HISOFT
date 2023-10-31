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

        ' Add any initialization after the InitializeComponent() call.

        With ReposDate

            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave
            AddHandler .Click, AddressOf ItemDate_GotFocus

        End With

        InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity" 'FNQuantity|FNPass|FNNotPass"
        '"FNQuantity|FNQuantityCut|FNQuantityEmb|FNQuantityRcvEmb|FNQuantityPrint|FNQuantityRcvPrint|FNQuantityHeat|FNQuantityRcvHeat|FNQuantityLasor|FNQuantityRcvLasor|FNQuantityPadPrint|FNQuantityRcvPadPrint|FNQuantitySew|FNQuantityFinishSew|FNQuantityQC|FNPass|FNNotPass"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldCustomSum As String = "FNSMPSam"

        Dim sFieldCustomGrpSum As String = ""

        With ogvoperation
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

    Private Sub ogvsummary_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvoperation.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvoperation
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNSMPSam"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTSMPOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTSMPOrderNo").ToString()) Then
                                        totalSum = totalSum + (Val(e.FieldValue.ToString))

                                    End If
                                End If
                                _RowHandleHold = e.RowHandle
                            End If
                            e.TotalValue = totalSum
                        End If

                End Select
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
        Me.ogcoperation.DataSource = _dt

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


        With Me.ogvoperation
            .Columns.ColumnByFieldName("FTActPatternDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            '.Columns.ColumnByFieldName("FTActFabricDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            '.Columns.ColumnByFieldName("FTActAccessoryDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            '.Columns.ColumnByFieldName("FTPlanCutDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            '.Columns.ColumnByFieldName("FTEmpCut").OptionsColumn.AllowEdit = False '(Me.ocmsave.Enabled)
            '.Columns.ColumnByFieldName("FTPlanSewDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            '.Columns.ColumnByFieldName("FTStandBy").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            '.Columns.ColumnByFieldName("FTStandBy1").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            .Columns.ColumnByFieldName("FTNote1").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            .Columns.ColumnByFieldName("FTCFMSendSampleDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            .Columns.ColumnByFieldName("FDGacDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)

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
        Call LoadOrderProdDetail()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs)
        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub


    Private Sub LoadOrderProdDetail()
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogcoperation.DataSource = Nothing

        cmd = "   Select 'SAMPLEROOM' AS 'Category', A.FTSMPOrderNo,A.FNSMPOrderStatus AS FNSMPOrderStatusState "
        'cmd &= vbCrLf & "  , Case When ISDATE(A.FDSMPOrderDate) = 1 Then  Convert(varchar(10),convert(Datetime,A.FDSMPOrderDate) ,103) Else '' END AS  FDSMPOrderDate"
        'cmd &= vbCrLf & "  , Case When ISDATE(A.FTDeliveryDate) = 1 Then  Convert(varchar(10),convert(Datetime,A.FTDeliveryDate) ,103) Else '' END AS  FTDeliveryDate"
        'cmd &= vbCrLf & "  , Case When ISDATE(A.FDSendToSMPDate) = 1 Then  Convert(varchar(10),convert(Datetime,A.FDSendToSMPDate) ,103) Else '' END AS  FDSendToSMPDate"
        'cmd &= vbCrLf & "  , Case When ISDATE(A.FTStateReceiptDate) = 1 Then  Convert(varchar(10),convert(Datetime,A.FTStateReceiptDate) ,103) Else '' END AS  FTStateReceiptDate"

        cmd &= vbCrLf & "  , Case When ISDATE(A.FDSMPOrderDate) = 1 Then  convert(Datetime,A.FDSMPOrderDate) Else NULL END AS  FDSMPOrderDate"
        cmd &= vbCrLf & "  , Case When ISDATE(A.FTDeliveryDate) = 1 Then  convert(Datetime,A.FTDeliveryDate)  Else NULL END AS  FTDeliveryDate"
        cmd &= vbCrLf & "  , Case When ISDATE(A.FDSendToSMPDate) = 1 Then  convert(Datetime,A.FDSendToSMPDate) Else NULL END AS  FDSendToSMPDate"
        cmd &= vbCrLf & "  , Case When ISDATE(A.FTStateReceiptDate) = 1 Then  convert(Datetime,A.FTStateReceiptDate) Else NULL END AS  FTStateReceiptDate"

        cmd &= vbCrLf & " ,A.FTStyleName"
        cmd &= vbCrLf & " ,A.FTGenderCode "
        cmd &= vbCrLf & " ,A.FTGenderName"
        cmd &= vbCrLf & " ,A.FTSMPOrderBy "
        cmd &= vbCrLf & " ,A.FTOrderRemark"
        cmd &= vbCrLf & ",A.FTBuyCode AS FNHSysBuyId"
        cmd &= vbCrLf & ",A.FTPgmName"

        'cmd &= vbCrLf & " , Case When ISDATE(A.FTPatternDate) = 1 Then  Convert(varchar(10),convert(Datetime,A.FTPatternDate) ,103) Else '' END AS  FTPatternDate"
        'cmd &= vbCrLf & " , Case When ISDATE(A.FTFabricDate) = 1 Then  Convert(varchar(10),convert(Datetime,A.FTFabricDate) ,103) Else '' END AS  FTFabricDate"
        'cmd &= vbCrLf & " , Case When ISDATE(A.FTAccessoryDate) = 1 Then  Convert(varchar(10),convert(Datetime,A.FTAccessoryDate) ,103) Else '' END AS  FTAccessoryDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTActPatternDate) = 1 Then  Convert(varchar(10),convert(Datetime,SMPMP.FTActPatternDate) ,103) Else '' END AS  FTActPatternDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTActFabricDate) = 1 Then  Convert(varchar(10),convert(Datetime,SMPMP.FTActFabricDate) ,103) Else '' END AS  FTActFabricDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTActAccessoryDate) = 1 Then  Convert(varchar(10),convert(Datetime,SMPMP.FTActAccessoryDate) ,103) Else '' END AS  FTActAccessoryDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTPlanCutDate) = 1 Then  Convert(varchar(10),convert(Datetime,SMPMP.FTPlanCutDate) ,103) Else '' END AS  FTPlanCutDate"

        cmd &= vbCrLf & " , Case When ISDATE(A.FTPatternDate) = 1 Then  convert(Datetime,A.FTPatternDate)  Else NULL END AS  FTPatternDate"
        cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTActPatternDate) = 1 Then  convert(Datetime,SMPMP.FTActPatternDate) Else NULL END AS  FTActPatternDate"
        'cmd &= vbCrLf & " , Case When ISDATE(A.FTFabricDate) = 1 Then  convert(Datetime,A.FTFabricDate)  Else NULL END AS  FTFabricDate"
        'cmd &= vbCrLf & " , Case When ISDATE(A.FTAccessoryDate) = 1 Then  convert(Datetime,A.FTAccessoryDate)  Else NULL END AS  FTAccessoryDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTActFabricDate) = 1 Then  convert(Datetime,SMPMP.FTActFabricDate)  Else NULL END AS  FTActFabricDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTActAccessoryDate) = 1 Then  convert(Datetime,SMPMP.FTActAccessoryDate) Else NULL END AS  FTActAccessoryDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTPlanCutDate) = 1 Then  convert(Datetime,SMPMP.FTPlanCutDate) Else NULL END AS  FTPlanCutDate"

        ' cmd &= vbCrLf & " ,SMPMP.FTEmpCut  "
        'cmd &= vbCrLf & " ,FTEmpCutName AS FTEmpCut  "

        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTPlanCutDate) = 1 Then  Convert(varchar(10),convert(Datetime,SMPMP.FTPlanCutDate) ,103) Else '' END AS  FTPlanCutDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTPlanSewDate) = 1 Then  Convert(varchar(10),convert(Datetime,SMPMP.FTPlanSewDate) ,103) Else '' END AS  FTPlanSewDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTCFMSendSampleDate) = 1 Then  Convert(varchar(10),convert(Datetime,SMPMP.FTCFMSendSampleDate) ,103) Else '' END AS  FTCFMSendSampleDate"

        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTPlanCutDate) = 1 Then  convert(Datetime,SMPMP.FTPlanCutDate)  Else NULL END AS  FTPlanCutDate"
        'cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTPlanSewDate) = 1 Then  convert(Datetime,SMPMP.FTPlanSewDate)  Else NULL END AS  FTPlanSewDate"
        cmd &= vbCrLf & " , Case When ISDATE(SMPMP.FTCFMSendSampleDate) = 1 Then  convert(Datetime,SMPMP.FTCFMSendSampleDate) Else NULL END AS  FTCFMSendSampleDate"
        cmd &= vbCrLf & " , Case When ISDATE(a.FTGACDate) = 1 Then  convert(Datetime,a.FTGACDate) Else NULL END AS  FDGacDate"

        cmd &= vbCrLf & " ,SMPMP.FTStandBy"
        cmd &= vbCrLf & " ,SMPMP.FTNote"
        cmd &= vbCrLf & " ,ISNULL(XSAM.FNSam,0) AS FNSMPSam"
        cmd &= vbCrLf & " ,ISNULL(SMPFN.FNQTYFinish,0) AS FNQTYFinish"
        cmd &= vbCrLf & " ,A.FTCustomerTeam  "
        cmd &= vbCrLf & " , A.FNSMPPrototypeNo"
        cmd &= vbCrLf & " , A.FTStyleCode"
        cmd &= vbCrLf & " , A.FTSeasonCode"
        cmd &= vbCrLf & " , A.FTCustCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            cmd &= vbCrLf & "  ,ISNULL(XT.FTNameTH,'') AS FNSMPOrderType"
            cmd &= vbCrLf & "   , A.FTCustNameTH AS FTCustName"
            cmd &= vbCrLf & "  ,ISNULL(XTOT.FTNameTH,'') AS FNOrderSampleType"
            cmd &= vbCrLf & "  ,ISNULL(XTOTST.FTNameTH,'') AS FNSMPOrderStatus"

        Else

            cmd &= vbCrLf & "  ,ISNULL(XT.FTNameEN,'') AS FNSMPOrderType"
            cmd &= vbCrLf & "   , A.FTCustNameEN  AS FTCustName"
            cmd &= vbCrLf & "  ,ISNULL(XTOT.FTNameEN,'') AS FNOrderSampleType"
            cmd &= vbCrLf & "  ,ISNULL(XTOTST.FTNameEN,'') AS FNSMPOrderStatus"
        End If

        cmd &= vbCrLf & "   , A.FTMerTeamCode"
        cmd &= vbCrLf & "   , A.FNSeq"
        cmd &= vbCrLf & "   , A.FTSizeBreakDown"
        cmd &= vbCrLf & "   , A.FTColorway"
        cmd &= vbCrLf & "   , A.FNQuantity"
        cmd &= vbCrLf & "   , A.FTRemark"
        cmd &= vbCrLf & "   , A.FNSMPOrderStatus"
        cmd &= vbCrLf & "   ,ISNULL(TEmp.FTEmpName,'') AS FTEmpName"



        'cmd &= vbCrLf & "   ,Case When ISDATE(Cut.FTStartDate) = 1 Then  convert(Datetime,Cut.FTStartDate) Else NULL END AS   FTStartDateCut"
        'cmd &= vbCrLf & "  ,Case When ISDATE(Cut.FTLastDate) = 1 Then  convert(Datetime,Cut.FTLastDate) Else NULL END AS   FTLastDateCut"

        'cmd &= vbCrLf & "  ,Cut.FNQuantity As FNQuantityCut,Cut.FTEmpCutName"
        'cmd &= vbCrLf & "  ,Case When ISDATE(SendEmb.FTStartDate) = 1 Then  convert(Datetime,SendEmb.FTStartDate) Else NULL END AS  FTStartDateEmb"
        'cmd &= vbCrLf & "  ,Case When ISDATE(SendEmb.FTLastDate) = 1 Then  convert(Datetime,SendEmb.FTLastDate) Else NULL END AS  FTLastDateEmb"
        'cmd &= vbCrLf & "  ,SendEmb.FNQuantity As FNQuantityEmb"
        'cmd &= vbCrLf & "    ,Case When ISDATE(RcvEmb.FTStartDate) = 1 Then  convert(Datetime,RcvEmb.FTStartDate) Else NULL END AS  FTStartDateRcvEmb"
        'cmd &= vbCrLf & "   ,Case When ISDATE(RcvEmb.FTLastDate) = 1 Then  convert(Datetime,RcvEmb.FTLastDate) Else NULL END AS  FTLastDateRcvEmb"
        'cmd &= vbCrLf & "   ,RcvEmb.FNQuantity As FNQuantityRcvEmb"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendPrint.FTStartDate) = 1 Then  convert(Datetime,SendPrint.FTStartDate) Else NULL END AS  FTStartDatePrint"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendPrint.FTLastDate) = 1 Then  convert(Datetime,SendPrint.FTLastDate) Else NULL END AS  FTLastDatePrint"
        'cmd &= vbCrLf & "   ,SendPrint.FNQuantity As FNQuantityPrint"
        'cmd &= vbCrLf & "   ,Case When ISDATE(RcvPrint.FTStartDate) = 1 Then  convert(Datetime,RcvPrint.FTStartDate) Else NULL END AS  FTStartDateRcvPrint"
        'cmd &= vbCrLf & "   ,Case When ISDATE(RcvPrint.FTLastDate) = 1 Then  convert(Datetime,RcvPrint.FTLastDate) Else NULL END AS  FTLastDateRcvPrint"
        'cmd &= vbCrLf & "   ,RcvPrint.FNQuantity As FNQuantityRcvPrint"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendHeat.FTStartDate) = 1 Then  convert(Datetime,SendHeat.FTStartDate) Else NULL END AS  FTStartDateHeat"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendHeat.FTLastDate) = 1 Then  convert(Datetime,SendHeat.FTLastDate) Else NULL END AS  FTLastDateHeat"
        'cmd &= vbCrLf & "   ,SendHeat.FNQuantity As FNQuantityHeat"
        'cmd &= vbCrLf & "   ,Case When ISDATE(RcvHeat.FTStartDate) = 1 Then  convert(Datetime,RcvHeat.FTStartDate) Else NULL END AS  FTStartDateRcvHeat"
        'cmd &= vbCrLf & "  ,Case When ISDATE(RcvHeat.FTLastDate) = 1 Then  convert(Datetime,RcvHeat.FTLastDate) Else NULL END AS  FTLastDateRcvHeat"
        'cmd &= vbCrLf & "   ,RcvHeat.FNQuantity As FNQuantityRcvHeat"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendLasor.FTStartDate) = 1 Then  convert(Datetime,SendLasor.FTStartDate) Else NULL END AS  FTStartDateLasor"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendLasor.FTLastDate) = 1 Then  convert(Datetime,SendLasor.FTLastDate) Else NULL END AS  FTLastDateLasor"
        'cmd &= vbCrLf & "   ,SendLasor.FNQuantity As FNQuantityLasor"
        'cmd &= vbCrLf & "    ,Case When ISDATE(RcvLasor.FTStartDate) = 1 Then  convert(Datetime,RcvLasor.FTStartDate) Else NULL END AS  FTStartDateRcvLasor"
        'cmd &= vbCrLf & "   ,Case When ISDATE(RcvLasor.FTLastDate) = 1 Then  convert(Datetime,RcvLasor.FTLastDate) Else NULL END AS  FTLastDateRcvLasor"
        'cmd &= vbCrLf & "  ,RcvLasor.FNQuantity As FNQuantityRcvLasor"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendPadPrint.FTStartDate) = 1 Then  convert(Datetime,SendPadPrint.FTStartDate) Else NULL END AS  FTStartDatePadPrint"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendPadPrint.FTLastDate) = 1 Then  convert(Datetime,SendPadPrint.FTLastDate) Else NULL END AS  FTLastDatePadPrint"
        'cmd &= vbCrLf & "   ,SendPadPrint.FNQuantity As FNQuantityPadPrint"
        'cmd &= vbCrLf & "    ,Case When ISDATE(RcvPadPrint.FTStartDate) = 1 Then  convert(Datetime,RcvPadPrint.FTStartDate) Else NULL END AS  FTStartDateRcvPadPrint"
        'cmd &= vbCrLf & "   ,Case When ISDATE(RcvPadPrint.FTLastDate) = 1 Then  convert(Datetime,RcvPadPrint.FTLastDate) Else NULL END AS  FTLastDateRcvPadPrint"
        'cmd &= vbCrLf & "   ,RcvPadPrint.FNQuantity As FNQuantityRcvPadPrint"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendSew.FTStartDate) = 1 Then  convert(Datetime,SendSew.FTStartDate) Else NULL END AS  FTStartDateSew"
        'cmd &= vbCrLf & "   ,Case When ISDATE(SendSew.FTLastDate) = 1 Then  convert(Datetime,SendSew.FTLastDate) Else NULL END AS  FTLastDateSew"
        'cmd &= vbCrLf & "   ,SendSew.FNQuantity As FNQuantitySew"
        'cmd &= vbCrLf & "   ,Case When ISDATE(FinishSew.FTStartDate) = 1 Then  convert(Datetime,FinishSew.FTStartDate) Else NULL END AS  FTStartDateFinishSew"
        'cmd &= vbCrLf & "   ,Case When ISDATE(FinishSew.FTLastDate) = 1 Then  convert(Datetime,FinishSew.FTLastDate) Else NULL END AS  FTLastDateFinishSew"
        'cmd &= vbCrLf & "  ,FinishSew.FNQuantity As FNQuantityFinishSew"
        'cmd &= vbCrLf & "   ,Case When ISDATE(QC.FTStartDate) = 1 Then  convert(Datetime,QC.FTStartDate) Else NULL END AS  FTStartDateQC"
        'cmd &= vbCrLf & "  ,Case When ISDATE(QC.FTLastDate) = 1 Then  convert(Datetime,QC.FTLastDate) Else NULL END AS  FTLastDateQC"
        'cmd &= vbCrLf & "  ,QC.FNQuantity As FNQuantityQC"
        'cmd &= vbCrLf & "  ,QC.FNPass "
        'cmd &= vbCrLf & "   ,QC.FNNotPass "
        'cmd &= vbCrLf & " ,QC.FTRemark AS FTNote"
        'cmd &= vbCrLf & " ,QC.FTRemark AS CFTNote"
        'cmd &= vbCrLf & " ,QC.FTRemark AS FTNote1 "
        'cmd &= vbCrLf & " ,QC.FTRemark AS QCFTNote"

        cmd &= vbCrLf & "  , isnull(MerQC.FTMerAppState , '0') as FTMerAppState  ,  isnull(MerQC.FTRejectState ,'0') as FTRejectState   "
        cmd &= vbCrLf & " , MerQC.FTMerAppBy  ,Case When ISDATE(MerQC.FDMerAppDate) = 1 Then  convert(Datetime,MerQC.FDMerAppDate)   Else NULL END AS  FDMerAppDate  "
        cmd &= vbCrLf & "   , MerQC.FTMerAppTime , MerQC.FTMerRemark"

        cmd &= vbCrLf & "  FROM(Select A.FTSMPOrderNo, A.FDSMPOrderDate, A.FNSMPOrderType, A.FNSMPPrototypeNo, MST.FTStyleCode, MSS.FTSeasonCode, MCT.FTCustCode, MCT.FTCustNameTH, MCT.FTCustNameEN, MMT.FTMerTeamCode, OD.FNSeq,"
        cmd &= vbCrLf & "   OD.FTSizeBreakDown"
        cmd &= vbCrLf & " 	, OD.FTColorway"
        cmd &= vbCrLf & " 	, OD.FNQuantity"
        cmd &= vbCrLf & " 	,OD.FTDeliveryDate"
        cmd &= vbCrLf & " 	, OD.FTRemark"
        cmd &= vbCrLf & " 	,A.FTStateAppDate AS FDSendToSMPDate "
        cmd &= vbCrLf & " 	,A.FTStateReceiptDate"
        cmd &= vbCrLf & " 	,MST.FTStyleNameEN  AS FTStyleName"
        cmd &= vbCrLf & " 	,GD.FTGenderCode "
        cmd &= vbCrLf & " 	,GD.FTGenderNameEN  AS FTGenderName"
        cmd &= vbCrLf & " ,A.FTSMPOrderBy "
        cmd &= vbCrLf & " ,B.FTBuyCode"
        cmd &= vbCrLf & ",A.FTPgmName"
        cmd &= vbCrLf & " ,CASE WHEN ISNULL(A.FTRemark,'') <> ''  THEN ISNULL(A.FTRemark,'') + char(30) ELSE  '' END "
        cmd &= vbCrLf & " +   CASE WHEN ISNULL(FTStateEmb,'')='1' THEN 'Emb,' ELSE '' END"
        cmd &= vbCrLf & " 	+   CASE WHEN ISNULL(FTStatePrint,'')='1' THEN 'Print,' ELSE '' END"
        cmd &= vbCrLf & " +   CASE WHEN ISNULL(FTStateHeat,'')='1' THEN 'Heat,' ELSE '' END"
        cmd &= vbCrLf & " 	+   CASE WHEN ISNULL(FTStateLaser,'')='1' THEN 'Laser,' ELSE '' END"
        cmd &= vbCrLf & " +   CASE WHEN ISNULL(FTStateWindows,'')='1' THEN 'window' ELSE '' END AS FTOrderRemark"
        cmd &= vbCrLf & " 	,OD.FTPatternDate "
        cmd &= vbCrLf & " 	,OD.FTFabricDate "
        cmd &= vbCrLf & " 	,OD.FTAccessoryDate "
        cmd &= vbCrLf & " 	,A.FNOrderSampleType "
        cmd &= vbCrLf & " 	,A.FTCustomerTeam,ISNULL(A.FNSMPOrderStatus,0) AS FNSMPOrderStatus  , OD.FTGACDate  "
        cmd &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As A With(NOLOCK)  LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As MSS With(NOLOCK)  On A.FNHSysSeasonId = MSS.FNHSysSeasonId LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As MCT With(NOLOCK)  On A.FNHSysCustId = MCT.FNHSysCustId LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam As MMT With(NOLOCK)  On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown As OD With(NOLOCK)  On A.FTSMPOrderNo = OD.FTSMPOrderNo"
        cmd &= vbCrLf & "   LEFT OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B  With(NOLOCK)  On A.FNHSysBuyId=B.FNHSysBuyId "
        cmd &= vbCrLf & "    Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender As GD With(NOLOCK)  On A.FNHSysGenderId = GD.FNHSysGenderId "


        cmd &= vbCrLf & "    WHERE A.FTSMPOrderNo<>''"

        If FNHSysCustId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysCustId=" & Val(FNHSysCustId.Properties.Tag.ToString) & ""
        End If

        If FNHSysStyleId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & ""
        End If

        If FNHSysSeasonId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""
        End If

        If FNHSysMerTeamId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & ""
        End If


        If FTStartOrderDate.Text <> "" And FTEndOrderDate.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FDSMPOrderDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) &
                "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
        End If

        If FTStartReqDate.Text <> "" And FTEndReqDate.Text <> "" Then
            cmd &= vbCrLf & " AND (OD.FTPatternDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FTStartReqDate.Text) &
                "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndReqDate.Text) & "')"
        End If


        If FTStartCFMOrderDate.Text <> "" And FTEndCFMOrderDate.Text <> "" Then
            cmd &= vbCrLf & "  AND (A.FTStateAppDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartCFMOrderDate.Text) &
                "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndCFMOrderDate.Text) & "')"
        End If

        cmd &= vbCrLf & "  ) As A"

        cmd &= vbCrLf & "  OUTER APPLY (  "
        cmd &= vbCrLf & "  Select SUM(FNSam) AS FNSam "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam As SSAM With (NOLOCK) "
        cmd &= vbCrLf & "   WHERE  (SSAM.FTSMPOrderNo =A.FTSMPOrderNo)  "
        cmd &= vbCrLf & "   ) AS XSAM"

        cmd &= vbCrLf & "   LEFT OUTER JOIN (  "
        cmd &= vbCrLf & "  Select FNListIndex,FTNameTH,FTNameEN "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        cmd &= vbCrLf & "   WHERE  (FTListName = N'FNSMPOrderType')  "
        cmd &= vbCrLf & "   ) AS XT ON  A.FNSMPOrderType =XT.FNListIndex "

        cmd &= vbCrLf & "    Left OUTER JOIN (  "
        cmd &= vbCrLf & "  Select  FNListIndex,FTNameTH,FTNameEN "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        cmd &= vbCrLf & "   Where (FTListName = N'FNOrderSampleType')  "
        cmd &= vbCrLf & "   ) As XTOT On  A.FNOrderSampleType = XTOT.FNListIndex"


        cmd &= vbCrLf & "    Left OUTER JOIN (  "
        cmd &= vbCrLf & "  Select  FNListIndex,FTNameTH,FTNameEN "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        cmd &= vbCrLf & "   Where (FTListName = N'FNSMPOrderStatus')  "
        cmd &= vbCrLf & "   ) As XTOTST On  A.FNSMPOrderStatus = XTOTST.FNListIndex"


        cmd &= vbCrLf & "      Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan As SMPMP With(NOLOCK)"
        cmd &= vbCrLf & "  	 On A.FTSMPOrderNo =SMPMP.FTSMPOrderNo And A.FTSizeBreakDown =SMPMP.FTSizeBreakDown And A.FTColorway=SMPMP.FTColorway "
        'cmd &= vbCrLf & "   OUTER APPLY("




        'cmd &= vbCrLf & "  Select  STUFF((Select  ', ' + FTEmpName "
        'cmd &= vbCrLf & " 	From(Select  DISTINCT  X2.FTEmpNameTH + ' ' + FTEmpSurnameTH   AS FTEmpName"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp As X1 With(NOLOCK) INNER Join"
        'cmd &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As X2 With(NOLOCK) On X1.FNHSysEmpID = X2.FNHSysEmpID"
        'cmd &= vbCrLf & "        INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamBreakdown As X23 With(NOLOCK) On X1.FTSMPOrderNo = X23.FTSMPOrderNo  AND X1.FTTeam = X23.FTTeam "
        'cmd &= vbCrLf & "     Where X1.FTSMPOrderNo = A.FTSMPOrderNo"
        'cmd &= vbCrLf & "           AND X23.FTColorway = A.FTColorway "
        'cmd &= vbCrLf & "           AND X23.FTSizeBreakDown = A.FTSizeBreakDown "
        'cmd &= vbCrLf & "     ) As TEmp For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') AS FTEmpName  ) As TEmp"

        cmd &= vbCrLf & "   OUTER APPLY("
        cmd &= vbCrLf & "  Select  STUFF((Select  distinct ', ' + FTEmpName "
        cmd &= vbCrLf & " 	From(Select Convert(nvarchar(10),Row_number() Over(Order By  b.FNHSysEmpId)) + '.'  +  FTEmpNameTH + ' ' + FTEmpSurnameTH   AS FTEmpName"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Emp As b with(nolock)  "
        cmd &= vbCrLf & "      left join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee emp with(nolock) on b.FNHSysEmpId = emp.FNHSysEmpID   "
        cmd &= vbCrLf & " inner join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle bd on b.FTBarcodeNo = bd.FTBarcodeBundleNo  "

        cmd &= vbCrLf & "    where bd.FTOrderProdNo = a.FTSMPOrderNo    and      bd.FTSizeBreakDown  = a.FTSizeBreakDown  and bd.FTColorway = a.FTColorway  "
        cmd &= vbCrLf & "   "
        cmd &= vbCrLf & "     ) As TEmp For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') AS FTEmpName  ) As TEmp"



        'cmd &= vbCrLf & "   OUTER APPLY( "

        'cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"


        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then


        '    cmd &= vbCrLf & ",  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].[dbo].FN_GetEmpCutName(1,  MAX(FTEmp)) AS FTEmpCutName"
        'Else


        '    cmd &= vbCrLf & ",  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].[dbo].FN_GetEmpCutName(0,   MAX(FTEmp)) AS FTEmpCutName"

        'End If

        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=0"
        'cmd &= vbCrLf & "   ) As Cut"

        'cmd &= vbCrLf & "   		  OUTER APPLY( "

        'cmd &= vbCrLf & "   Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=1"
        'cmd &= vbCrLf & "   ) As SendEmb"
        'cmd &= vbCrLf & " 	   OUTER APPLY( "

        'cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=2"
        'cmd &= vbCrLf & "   ) As RcvEmb"

        'cmd &= vbCrLf & "    OUTER APPLY( "

        'cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "      From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & " 	 Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=3"
        'cmd &= vbCrLf & " 	  ) As SendPrint"
        'cmd &= vbCrLf & " 	   OUTER APPLY( "

        'cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=4"
        'cmd &= vbCrLf & "   ) As RcvPrint"
        'cmd &= vbCrLf & "   OUTER APPLY( "

        'cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "     From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=5"
        'cmd &= vbCrLf & " 	  ) As SendHeat"
        'cmd &= vbCrLf & " 	   OUTER APPLY( "

        'cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=6"
        'cmd &= vbCrLf & "   ) As RcvHeat"

        'cmd &= vbCrLf & "   OUTER APPLY( "

        'cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "     From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=7"
        'cmd &= vbCrLf & " 	  ) As SendLasor"
        'cmd &= vbCrLf & " 	   OUTER APPLY( "

        'cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=8"
        'cmd &= vbCrLf & "   ) As RcvLasor"

        'cmd &= vbCrLf & "   OUTER APPLY( "
        'cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "     From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=9"
        'cmd &= vbCrLf & " 	  ) As SendPadPrint"
        'cmd &= vbCrLf & " 	   OUTER APPLY( "

        'cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        ''cmd &= vbCrLf & " And X.FTTeam  = ''  "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=10"
        'cmd &= vbCrLf & "   ) As RcvPadPrint"

        'cmd &= vbCrLf & "    OUTER APPLY( "

        'cmd &= vbCrLf & "   Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        'cmd &= vbCrLf & " And ISNULL(X.FTTeam,'')  <>'' "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=11"
        'cmd &= vbCrLf & "   ) As SendSew"
        'cmd &= vbCrLf & "  OUTER APPLY( "

        'cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        'cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        'cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        'cmd &= vbCrLf & " And ISNULL(X.FTTeam,'')  <>'' "
        'cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        'cmd &= vbCrLf & " And FNSampleState=12"
        'cmd &= vbCrLf & "   ) As FinishSew"

        cmd &= vbCrLf & "   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity,SUM(FNPass) As FNPass,SUM(FNNotPass) As FNNotPass,FTRemark"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        'cmd &= vbCrLf & " And ISNULL(X.FTTeam,'')  <>'' "

        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " Group By FTRemark"
        cmd &= vbCrLf & "  ) As QC"

        cmd &= vbCrLf & "   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  top 1  FTStateApp as FTMerAppState , FTStateReject as FTRejectState ,  FTAppBy as FTMerAppBy , FDAppDate as FDMerAppDate , FTAppTime  as FTMerAppTime  ,FTRemark  as FTMerRemark  "
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQCMER As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = a.FTSMPOrderNo"
        'cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =a.FTSizeBreakDown And X.FTColorway=a.FTColorway"

        cmd &= vbCrLf & "  ) As MERQC"



        cmd &= vbCrLf & "   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  SUM(CASE WHEN (FNQuantity - ISNULL(QCX.FNPass,0)) <=0 THEN 0 ELSE (FNQuantity - ISNULL(QCX.FNPass,0)) END  ) AS  FNQTYFinish "
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown As XF  With(NOLOCK)"
        cmd &= vbCrLf & "   OUTER APPLY( "
        cmd &= vbCrLf & "    Select  SUM(FNPass) As FNPass "
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC As QCX  With(NOLOCK)"
        cmd &= vbCrLf & "  Where QCX.FTSMPOrderNo =XF.FTSMPOrderNo"
        cmd &= vbCrLf & " And QCX.FTSizeBreakDown =XF.FTSizeBreakDown And QCX.FTColorway=XF.FTColorway"
        cmd &= vbCrLf & "  ) As QCX"

        cmd &= vbCrLf & "  Where XF.FTSMPOrderNo = A.FTSMPOrderNo"

        cmd &= vbCrLf & "  ) As SMPFN"


        'cmd &= vbCrLf & " ORDER BY  A.FTSMPOrderNo ,A.FNSeq"
        cmd &= vbCrLf & " UNION ALL"

        cmd &= vbCrLf & " SELECT 'PRODUCTION' AS 'Category',ISNULL(A.FTOrderNo,'') AS 'FTSMPOrderNo', ISNULL(A.FTStateOrderApp,'') AS 'FNSMPOrderStatusState', "

        cmd &= vbCrLf & " Case When ISDATE(A.FDInsDate) = 1 Then  convert(Datetime,A.FDInsDate) Else NULL END AS 'FDSMPOrderDate', "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTDeliveryDate', "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FDSendToSMPDate', "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTStateReceiptDate', "
        cmd &= vbCrLf & "ISNULL(MST.FTStyleNameEN,'') AS 'FTStyleName',  ISNULL(GD.FTGenderCode,'') AS 'FTGenderCode', "
        cmd &= vbCrLf & "ISNULL(GD.FTGenderNameEN,'') AS 'FTGenderName', ISNULL(A.FTOrderBy,'') AS 'FTSMPOrderBy', "
        cmd &= vbCrLf & "ISNULL(A.FTRemark,'') AS 'FTOrderRemark', '' AS 'FNHSysBuyId', "
        cmd &= vbCrLf & "ISNULL(A.FTSubPgm,'') AS 'FTPgmName',  "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTPatternDate', "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTActPatternDate', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTFabricDate', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTAccessoryDate', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTActFabricDate', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTActAccessoryDate', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTPlanCutDate', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTPlanCutDate', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTPlanSewDate', "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTCFMSendSampleDate', "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FDGacDate', "
        cmd &= vbCrLf & "'' AS 'FTStandBy',  ISNULL(S.FTOther1Note,'') AS 'FTNote', "
        cmd &= vbCrLf & "0 AS 'FNSMPSam', 0 AS 'FNQTYFinish', '' AS 'FTCustomerTeam', "
        cmd &= vbCrLf & "ISNULL(A.FNHSysProdTypeId,'') AS 'FNSMPPrototypeNo', ISNULL(MST.FTStyleCode,'') AS 'FTStyleCode', "
        cmd &= vbCrLf & "ISNULL(MSS.FTSeasonNameEN,'') AS 'FTSeasonCode', "
        cmd &= vbCrLf & "ISNULL(MCT.FTCustCode,'') AS 'FTCustCode', "
        cmd &= vbCrLf & "ISNULL(XT.FTNameEN,'') AS 'FNSMPOrderType', "
        cmd &= vbCrLf & "ISNULL(MCT.FTCustNameEN,'') AS 'FTCustName', "
        cmd &= vbCrLf & "'' AS 'FNOrderSampleType', '' AS 'FNSMPOrderStatus', "
        cmd &= vbCrLf & "ISNULL(MMT.FTMerTeamCode,'') AS 'FTMerTeamCode', "
        cmd &= vbCrLf & "0 AS 'FNSeq', ISNULL(SB.FTSizeBreakDown,'') AS 'FTSizeBreakDown', "
        cmd &= vbCrLf & "ISNULL(SB.FTColorway,'') AS 'FTColorway', ISNULL(SB.FNQuantity,'') AS 'FNQuantity', "
        cmd &= vbCrLf & "'' AS 'FTRemark', '' AS 'FNSMPOrderStatus', '' AS 'FTEmpName', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTStartDateQC', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FTLastDateQC', "
        'cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FNQuantityQC', "
        'cmd &= vbCrLf & "0 AS 'FNPass', 0 AS 'FNNotPass', '' AS 'FTNote1', "
        cmd &= vbCrLf & "A.FNJobState AS 'FTMerAppState', 0 AS 'FTRejectState', "
        cmd &= vbCrLf & "A.FTStateBy AS 'FTMerAppBy', "
        cmd &= vbCrLf & "Case When ISDATE('') = 1 Then  convert(Datetime,'') Else NULL END AS 'FDMerAppDate', "
        cmd &= vbCrLf & "'' AS 'FTMerAppTime', '' AS 'FTMerRemark'"

        cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH (NOLOCK) "
        cmd &= vbCrLf & "OUTER APPLY (SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SB WITH (NOLOCK) "
        cmd &= vbCrLf & "WHERE A.FTOrderNo = SB.FTOrderNo) AS SB "
        cmd &= vbCrLf & "OUTER APPLY (SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH (NOLOCK) "
        cmd &= vbCrLf & "WHERE A.FTOrderNo = S.FTOrderNo) AS S "
        cmd &= vbCrLf & "Left OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId "
        cmd &= vbCrLf & "Left OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As MSS With(NOLOCK)  On A.FNHSysSeasonId = MSS.FNHSysSeasonId "
        cmd &= vbCrLf & "Left OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As MCT With(NOLOCK)  On A.FNHSysCustId = MCT.FNHSysCustId "
        cmd &= vbCrLf & "Left OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam As MMT With(NOLOCK)  On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId "
        cmd &= vbCrLf & "Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender As GD With(NOLOCK)  On S.FNHSysGenderId = GD.FNHSysGenderId "
        cmd &= vbCrLf & "Left OUTER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B  With(NOLOCK)  On A.FNHSysBuyId=B.FNHSysBuyId "
        cmd &= vbCrLf & "Left OUTER JOIN ( Select FNListIndex,FTNameTH,FTNameEN  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        cmd &= vbCrLf & "WHERE  (FTListName = N'FNSMPOrderType')  ) AS XT ON  A.FNOrderType =XT.FNListIndex "


        cmd &= vbCrLf & "    WHERE A.FTOrderNo <> '' "

        If FNHSysCustId.Text <> "" Then
            cmd &= vbCrLf & " AND A.FNHSysCustId=" & Val(FNHSysCustId.Properties.Tag.ToString) & ""
        End If

        If FNHSysStyleId.Text <> "" Then
            cmd &= vbCrLf & " AND A.FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & ""
        End If

        If FNHSysSeasonId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""
        End If

        If FNHSysMerTeamId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & ""
        End If


        If FTStartOrderDate.Text <> "" And FTEndOrderDate.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FDOrderDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) &
                "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
        End If

        'If FTStartReqDate.Text <> "" And FTEndReqDate.Text <> "" Then
        '    cmd &= vbCrLf & " AND (OD.FTPatternDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FTStartReqDate.Text) &
        '        "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndReqDate.Text) & "')"
        'End If


        'If FTStartCFMOrderDate.Text <> "" And FTEndCFMOrderDate.Text <> "" Then
        '    cmd &= vbCrLf & "  AND (A.FTStateAppDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartCFMOrderDate.Text) &
        '        "' AND '" & HI.UL.ULDate.ConvertEnDB(FTEndCFMOrderDate.Text) & "')"
        'End If


        cmd &= vbCrLf & " "



        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogcoperation.DataSource = _dtprod.Copy

        _dtprod.Dispose()
    End Sub

    Private Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                '_FocusedColumn = .FocusedColumn
                '_FocusedRowHendle = .FocusedRowHandle
                If .FocusedRowHandle < -1 Then Exit Sub

                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then
                    Exit Sub
                End If

                Dim _TDate As String
                Me.ocmsave.Visible = True
                'Exit Sub
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
                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                    Dim Color As String = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString()
                    Dim Size As String = .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString()
                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    If FieldName = "FDGacDate" Then

                        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown  Set "
                        cmdstring &= vbCrLf & " FTGACDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)


                        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown  Set "
                        cmdstring &= vbCrLf & " FTOGacDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' and isnull(FTOGacDate,'') = ''"
                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                    Else

                        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan  Set "
                        cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & "," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB & " "

                        cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                            cmdstring = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan ("
                            cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTSizeBreakDown, FTColorway ," & FieldName & "," & FieldName & "User," & FieldName & "Date," & FieldName & "Time"
                            cmdstring &= vbCrLf & " )"
                            cmdstring &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Size) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                        End If


                        Select Case FieldName
                            Case "FTPatternDate", "FTFabricDate", "FTAccessoryDate"
                                cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown  Set "
                                cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                                cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)
                        End Select

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


                'If _FocusedRowHendle = .FocusedRowHandle Then
                '    If .FocusedColumn.FieldName <> _FocusedColumn.FieldName Then
                '        If HI.MG.ShowMsg.mConfirmProcess("คุณยังไม่ได้บันทึกข้อมูลก่อนหน้า  ต้องการบันทึกข้อมูลก่อนหน้าหรือไม่ ?? ", 2303271140, Me.Text) = False Then
                '            _FocusedRowHendle = -99
                '        End If
                '    End If
                'ElseIf _FocusedRowHendle > -1 Then
                '    If .FocusedColumn.FieldName <> _FocusedColumn.FieldName Then
                '        If HI.MG.ShowMsg.mConfirmProcess("คุณยังไม่ได้บันทึกข้อมูลก่อนหน้า  ต้องการบันทึกข้อมูลก่อนหน้าหรือไม่ ?? ", 2303271140, Me.Text) = False Then
                '            _FocusedRowHendle = -99
                '        End If
                '    End If
                'End If


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


    Private Sub ogvoperation_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvoperation.CustomColumnDisplayText

        Select Case e.Column.FieldName
            Case "FTActPatternDate", "FTActFabricDate", "FTActAccessoryDate", "FTPlanCutDate", "FTPlanSewDate"

                If e.DisplayText = "01/01/0001" Then
                    e.DisplayText = ""
                    e.Column.ClearFilter()
                End If
                If e.DisplayText = "0001/01/01" Then
                    e.DisplayText = ""
                    e.Column.ClearFilter()
                End If
        End Select

    End Sub

    Private Sub ReposFTEmpCut_Click(sender As Object, e As EventArgs) Handles ReposFTEmpCut.Click
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < -1 Then Exit Sub

                GridDataBefore = (.GetFocusedRowCellValue(.FocusedColumn)).ToString()

            End With


        Catch ex As Exception
            GridDataBefore = ""
        End Try
    End Sub

    Private Sub ReposFTEmpCut_Leave(sender As Object, e As EventArgs) Handles ReposFTEmpCut.Leave
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < -1 Then Exit Sub
                If Not HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then

                    Exit Sub
                End If


                Dim NewData As String = sender.Text

                If NewData <> GridDataBefore Then

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                    Dim Color As String = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString()
                    Dim Size As String = .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString()
                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan  Set "
                    cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & "," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB & " "

                    cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                        cmdstring = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan ("
                        cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTSizeBreakDown, FTColorway ," & FieldName & "," & FieldName & "User," & FieldName & "Date," & FieldName & "Time"
                        cmdstring &= vbCrLf & " )"
                        cmdstring &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Size) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(NewData) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                    End If

                Else

                End If

                GridDataBefore = ""
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReposFTNote_Click(sender As Object, e As EventArgs) Handles ReposFTNote.Click
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                GridDataBefore = (.GetFocusedRowCellValue(.FocusedColumn)).ToString()

            End With


        Catch ex As Exception
            GridDataBefore = ""
        End Try
    End Sub

    Private Sub ReposFTNote_Leave(sender As Object, e As EventArgs) Handles ReposFTNote.Leave
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < -1 Then Exit Sub
                If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption) Then

                    Exit Sub
                End If

                Dim NewData As String = sender.Text

                If NewData <> GridDataBefore Then

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTSMPOrderNo").ToString()
                    Dim Color As String = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString()
                    Dim Size As String = .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString()
                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan  Set "
                    cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & "," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB & " "

                    cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                        cmdstring = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan ("
                        cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTSizeBreakDown, FTColorway ," & FieldName & "," & FieldName & "User," & FieldName & "Date," & FieldName & "Time"
                        cmdstring &= vbCrLf & " )"
                        cmdstring &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Size) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(NewData) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                    End If
                Else

                End If

                GridDataBefore = ""
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvoperation_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvoperation.CellMerge

        Try

            With Me.ogvoperation
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

                    Case Else
                        e.Merge = False
                        e.Handled = True

                End Select

            End With

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvoperation_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvoperation.RowStyle
        Try

            Try
                With Me.ogvoperation
                    Try
                        If Val(.GetRowCellValue(e.RowHandle, "FNQTYFinish")) = 0 Then

                            e.Appearance.BackColor = System.Drawing.Color.LightYellow
                            e.Appearance.BackColor2 = System.Drawing.Color.Orange
                            e.Appearance.ForeColor = System.Drawing.Color.Blue

                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If Val(.GetRowCellValue(e.RowHandle, "FNSMPOrderStatusState")) = 2 Then

                            e.Appearance.ForeColor = System.Drawing.Color.Red

                        End If
                    Catch ex As Exception

                    End Try
                End With
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) ' Handles ocmsave.Click
        Try
            If _FocusedRowHendle < -1 Then Exit Sub
            Try
                With CType(Me.ogvoperation, DevExpress.XtraGrid.Views.Grid.GridView)

                    Dim _TDate As String
                    .FocusedColumn = _FocusedColumn
                    .FocusedRowHandle = _FocusedRowHendle
                    Try

                        _TDate = .GetRowCellValue(_FocusedRowHendle, _FocusedColumn.FieldName) ' HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)

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

                        Dim cmdstring As String = ""

                        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan  Set "
                        cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & "," & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & "," & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB & " "

                        cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                            cmdstring = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrderMasterPlan ("
                            cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTSizeBreakDown, FTColorway ," & FieldName & "," & FieldName & "User," & FieldName & "Date," & FieldName & "Time"
                            cmdstring &= vbCrLf & " )"
                            cmdstring &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Size) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                        End If


                        Select Case FieldName
                            Case "FTPatternDate", "FTFabricDate", "FTAccessoryDate"
                                cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown  Set "
                                cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                                cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Size) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)
                        End Select

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
            If e.NewValue.ToString = "0001/01/01" Then
                e.Cancel = True
            End If
            If e.NewValue.ToString = "01/01/0001" Then
                e.Cancel = True
            End If
            If e.NewValue.ToString = "" Then
                e.Cancel = True
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

End Class
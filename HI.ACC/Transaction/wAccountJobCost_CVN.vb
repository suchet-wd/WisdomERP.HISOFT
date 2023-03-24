Imports System.Windows.Forms
Imports DevExpress.CodeParser

Public Class wAccountJobCost_CVN
    Private _StateFormLoad As Boolean = False


    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Sub New()
        InitializeComponent()
        _StateFormLoad = True

        Call InitGrid()
        Call InitGridDtl()
    End Sub


#Region "Initial Grid"
    Private Sub InitGrid()

        ''  _CfgIncentiveAmtDigit = HI.Conn.SQLConn.GetField("SELECT FTCfgData FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSESystemConfig] WHERE FTCfgName = 'CfgIncentiveAmtDigit' ", Conn.DB.DataBaseName.DB_SECURITY, "")


        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTEmpCode"
        'Dim sFieldSum As String = "FNNetProAmt|FNNetAmt|FNNetPayAmt|FNQAAmt|FNProOT|FNProNormal|FNAmtOT|FNAmtNormal|FNNetPay|FNNetIncen|FNAmt|FNNetPerDay"
        Dim sFieldGrpCount As String = "FTOrder"
        Dim sFieldGrpSum As String = "FTPayYear|FNAmt_OverHead"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            'For Each Str As String In sFieldCount.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n" + _CfgIncentiveAmtDigit + "}"
            '        .Columns(Str).DisplayFormat.FormatString = "{0:n" + _CfgIncentiveAmtDigit + "}"
            '    End If
            'Next

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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
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

    Private Sub InitGridDtl()

        ''  _CfgIncentiveAmtDigit = HI.Conn.SQLConn.GetField("SELECT FTCfgData FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSESystemConfig] WHERE FTCfgName = 'CfgIncentiveAmtDigit' ", Conn.DB.DataBaseName.DB_SECURITY, "")


        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTEmpCode"
        'Dim sFieldSum As String = "FNNetProAmt|FNNetAmt|FNNetPayAmt|FNQAAmt|FNProOT|FNProNormal|FNAmtOT|FNAmtNormal|FNNetPay|FNNetIncen|FNAmt|FNNetPerDay"
        Dim sFieldGrpCount As String = "FTOrder"
        Dim sFieldGrpSum As String = "FTPayYear|FNAmt_OverHead"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With odvDtl
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            'For Each Str As String In sFieldCount.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n" + _CfgIncentiveAmtDigit + "}"
            '        .Columns(Str).DisplayFormat.FormatString = "{0:n" + _CfgIncentiveAmtDigit + "}"
            '    End If
            'Next

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

            ''For Each Str As String In sFieldGrpSum.Split("|")
            ''    If Str <> "" Then
            ''        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
            ''    End If
            ''Next

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

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

#End Region

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load


        '' _FormLoad = False

        Dim _date As String = ""
        Dim _Qry As String = ""
        _Qry = "SELECT CONVERT(varchar(4), GETDATE(), 112)  as [date] "
        _date = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        FTDateStart.EditValue = New DateTime(Date.Now.Year, 1, 1)


        Dim thisDate As Date
        Dim thisMonth As Integer
        thisDate = New DateTime(Date.Now.Year, Date.Now.Month, 1)
        thisMonth = Month(thisDate)

        FNMonth.SelectedIndex = thisMonth - 1


        Call LoadData()
    End Sub

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

    'Public Property CfgIncentiveAmtDigit As String
    '    Get
    '        Return _CfgIncentiveAmtDigit
    '    End Get
    '    Set(value As String)
    '        _CfgIncentiveAmtDigit = value
    '    End Set
    'End Property
#End Region


    Private Sub LoadData()

        Dim _Qry As String = ""

        Dim _dt As DataTable
        Dim Date_S As String = ""
        Dim Date_E As String = ""
        Dim _Month As String = ""
        Dim MM As String = ""

        MM = "0" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)
        MM = MM.Substring(MM.Length - 2, 2)

        Date_S = FTDateStart.Text & "/" & MM & "/01"
        _Qry = " SELECT DATEADD(month, ((YEAR('" & Date_S & "') - 1900) * 12) + MONTH('" & Date_S & "'), -1) "
        _Qry = " SELECT Convert(varchar(10),Convert(Datetime,DATEADD(month, ((YEAR('" & Date_S & "') - 1900) * 12) + MONTH('" & Date_S & "'), -1) ),111)  "
        Date_E = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")



        ''''''''''''''Summary''''''''''''
        _Qry = "  SELECT  '" & FTDateStart.Text & "' AS FTPayYear, '" & MM & "'  AS FNMonth, FTOrder, SUM(FNMinute) [FNMinute], SUM(FNSewQty) [FNQty] "
        _Qry &= vbCrLf & " , SUM(FNAmt_OverHead) [FNAmt_OverHead], SUM(FNAmt_DL) [FNAmt_DL] "
        _Qry &= vbCrLf & " , SUM(FNAmt_IDL) [FNAmt_IDL], SUM(FNAmt_MNG) [FNAmt_MNG], SUM(FNAmt_None) [FNAmt_None] "
        _Qry &= vbCrLf & " , SUM(FNAmt_OverHead) + SUM(FNAmt_DL) + SUM(FNAmt_IDL) + SUM(FNAmt_MNG) + SUM(FNAmt_None) AS  [FNAmt_Summary] "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TFINJobCost WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE   FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "' "
        _Qry &= vbCrLf & " GROUP BY FTOrder "
        _Qry &= vbCrLf & " ORDER BY FTOrder "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogc.DataSource = _dt.Copy


        '''''''''''Detail'''''''''''''''''''
        _Qry = "  SELECT   FTCalDate, FTOrder, FNMinute, FNQty "
        _Qry &= vbCrLf & " , FNAmt_OverHead, FNAmt_DL "
        _Qry &= vbCrLf & " , FNAmt_IDL, FNAmt_MNG, FNAmt_None "
        _Qry &= vbCrLf & " , FNAmt_OverHead + FNAmt_DL + FNAmt_IDL + FNAmt_MNG +  FNAmt_None AS [FNAmt_Summary]"
        _Qry &= vbCrLf & " , FNCutMinute, FNCutQty, FNEmbellishmentMinute,FNEmbellishmentQty  "
        _Qry &= vbCrLf & " , FNSuperMarketMinute,FNSuperMarketQty , FNSewMinute, FNSewQty "
        _Qry &= vbCrLf & " , FNPackMinute , FNPackQty "
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TFINJobCost WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE   FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "' "

        _Qry &= vbCrLf & " ORDER BY FTCalDate, FTOrder "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcDtl.DataSource = _dt.Copy






        _Qry = "  SELECT  FTCalDate "
        _Qry &= vbCrLf & " , O.FNHSysUnitSectId, U.FTUnitSectCode, FNHSysStyleId, FTOrderNo, FTSubOrderNo, O.FNSeq "
        _Qry &= vbCrLf & " , FTStartTime, FTEndTime, FNQuantity, FNTotalMinute,FNAction "
        _Qry &= vbCrLf & " , CASE WHEN (ISNULL(FNAction,99))= 0 THEN 'Cut'  "
        _Qry &= vbCrLf & " WHEN (ISNULL(FNAction,99))= 3 THEN 'Sew' "
        _Qry &= vbCrLf & " WHEN (ISNULL(FNAction,99))= 4 THEN 'Pack' "
        _Qry &= vbCrLf & "  ELSE '' END AS 'FTAction' "
        _Qry &= vbCrLf & " , FNEmployeeInLine, FNTotalLateMin,FNEmployeeTotalWorkMin, FNEmployeeLineTotalWorkMin "
        _Qry &= vbCrLf & " , FNEmployeeTotalWageAmt, FNEmployeeTotalWageAmtAvgPerMin, FNJobCostDLEmptypeDaily "
        _Qry &= vbCrLf & " , FNEmployeeMonthlyDL, FNEmpTypeMonthlyDLAllMinPerDay, FNEmpTypeMonthlyDLAllCostPerDay "
        _Qry &= vbCrLf & " , FNAllJobMinutesActionPerDay, FNEmpTypeMonthlyDLAllCostAvgPerMin, FNEmpTypeMonthlyDLAllCostPerDayAction "
        _Qry &= vbCrLf & " , FNDLAddOtheTotalAmt, FNUniSectTotalWorkMin, FNDLAddOtherAmtAvgMin, FNDLAddOtherAmt, FNMonthlyDLAddOtheTotalAmt "
        _Qry &= vbCrLf & " ,  FNMonthlyUniSectTotalWorkMin, FNMonthlyDLAddOtherAmtAvgMin, FNMonthlyDLAddOtherAmt "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_OrderTime O "
        _Qry &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect U ON U.FNHSysUnitSectId = O.FNHSysUnitSectId "
        _Qry &= vbCrLf & "  WHERE   FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "' "

        _Qry &= vbCrLf & " ORDER BY FNAction, FNHSysUnitSectId, FTCalDate, FNSeq "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcDtlDaily.DataSource = _dt.Copy

    End Sub
    Private Sub ClearCreteria()

        FTDateStart.Text = ""
        FNMonth.SelectedIndex = -1


    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If ValidateData() Then
            ''   Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


            Call LoadData()

            'If LoadData() Then
            'End If

            '' _Spls.Close()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        'HI.TL.HandlerControl.ClearControl(Me)
        Call ClearCreteria()
        'ogcacc.DataSource = Nothing

    End Sub

    Private Function ValidateData() As Boolean

        If FTDateStart.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาเลือกปี !!!", 1511121406, Me.Text)
            Return False
        End If

        Return True
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


End Class
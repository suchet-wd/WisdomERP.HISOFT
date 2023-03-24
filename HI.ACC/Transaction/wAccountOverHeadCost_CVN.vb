Imports System.Windows.Forms
Imports DevExpress.CodeParser
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wAccountOverHeadCost_CVN
    Private _StateFormLoad As Boolean = False


    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Sub New()
        InitializeComponent()
        _StateFormLoad = True
        Call LoadDataAcc()

        Call InitGrid()

    End Sub


#Region "Initial Grid"
    Private Sub InitGrid()

        _CfgIncentiveAmtDigit = HI.Conn.SQLConn.GetField("SELECT FTCfgData FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSESystemConfig] WHERE FTCfgName = 'CfgIncentiveAmtDigit' ", Conn.DB.DataBaseName.DB_SECURITY, "")


        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTOverHeadCostCode"
        'Dim sFieldSum As String = "FNAmt"
        Dim sFieldGrpCount As String = "FTOverHeadCostCode"
        Dim sFieldGrpSum As String = "FNAmt"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvacc
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
        ' thisMonth now contains 2.

        ogvacc.OptionsView.ShowAutoFilterRow = False


        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvacc.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next

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
    Private _CfgIncentiveAmtDigit As String = ""
    Public Property CfgIncentiveAmtDigit As String
        Get
            Return _CfgIncentiveAmtDigit
        End Get
        Set(value As String)
            _CfgIncentiveAmtDigit = value
        End Set
    End Property
#End Region
    Private Sub LoadDataAcc()
        Dim cmdstring As String = ""
        Dim dt As DataTable


        cmdstring = "  SELECT  FNHSysAccountId, FTAccountCode , FTAccountCode AS [FTOverHeadCostCode]"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            cmdstring &= vbCrLf & " ,FTAccountNameTH AS FTAccountName"
        Else
            cmdstring &= vbCrLf & " ,FTAccountNameEN AS FTAccountName"
        End If

        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMAccount"
        cmdstring &= vbCrLf & "  WHERE FTStateActive = '1'AND FTOverHeadStateActive = '1' ORDER BY FTAccountCode  "
        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)
        repositoryFNHSysAccOverHeadCostID.DataSource = dt.Copy

    End Sub

    Private Sub LoadData()
        Dim _Qry As String = ""

        Dim _dt As DataTable

        _Qry = "  SELECT  Row_NUmber() over (Order By  OVH.FTYear, OVH.FNMonth, OVH.FNHSysAccOverHeadCostID) As FNSeq "
        _Qry &= vbCrLf & " ,   OVH.FNHSysAccOverHeadCostID, OVH.FTOverHeadCostCode, OVH.FTOverHeadCostDesc "
        _Qry &= vbCrLf & " , OVH.FTYear, OVH.FNMonth"
        _Qry &= vbCrLf & " , OVH.FNAmt "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "

        _Qry &= vbCrLf & " ,OVH.FTStateActive, OVH.FTApproveUser, OVH.FDApproveDate,  OVH.FTApproveTime ,  ISNULL(OVH.FTPIState,'0') as FTPIState  "

        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTOverHeadCost AS OVH WITH(NOLOCK) "
        '' _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMAccount  A ON  A.FNHSysAccountId =  FNHSysAccOverHeadCostID "
        _Qry &= vbCrLf & "  WHERE  OVH.FNHSysAccOverHeadCostID is not null "

        If FTDateStart.Text <> "" Then
            _Qry &= vbCrLf & "  	AND OVH.FTYear ='" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"
        End If


        If FNMonth.SelectedIndex >= 0 Then
            _Qry &= vbCrLf & "  	AND OVH.FNMonth ='" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & "' "
        End If


        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcacc.DataSource = _dt.Copy

        _dt.Dispose()
        Call InitGridDataAcc()
    End Sub
    Private Sub ClearCreteria()

        FTDateStart.Text = ""
        FNMonth.SelectedIndex = -1

        'FTStartShipDate.Text = ""
        'FTEndShipDate.Text = ""

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
        ogcacc.DataSource = Nothing

    End Sub

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.VerrifyData Then

            ''add VerriFySave -- check Already Approve

            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub


    Private Function SaveData() As Boolean
        Try

            Dim _Str As String = ""
            Dim _DSeq As Integer = 0
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = " DELETE FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.[TACCTOverHeadCost]  WHERE FTYear='" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "' AND  FNMonth ='" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & "' "
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            Dim dtacc As DataTable
            With CType(ogcacc.DataSource, DataTable)
                .AcceptChanges()
                dtacc = .Copy
            End With
            _DSeq = 0

            For Each R As DataRow In dtacc.Select("FTOverHeadCostCode<>'' ", "FNSeq")

                _DSeq = _DSeq + 1

                _Str = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTOverHeadCost ("
                _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FNHSysAccOverHeadCostID, FNSeq, FTOverHeadCostCode,FTOverHeadCostDesc, FTYear, FNMonth , FNAmt, FTPIState "
                _Str &= vbCrLf & " )"
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & "," & Val(R!FNHSysAccOverHeadCostID.ToString) & ""
                _Str &= vbCrLf & "," & _DSeq & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOverHeadCostCode.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOverHeadCostDesc.ToString) & "'"

                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)) & "'"

                _Str &= vbCrLf & "," & Val(R!FNAmt.ToString) & ""
                _Str &= vbCrLf & ",'" & R!FTPIState.ToString & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If



            Next


            dtacc.Dispose()
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


    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click

        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการยืนยันการต้นทุน ใช่หรือ ไม่ ?", 1812020457) Then
            Dim _Spls As New HI.TL.SplashScreen("Calculating...   Please Wait   ")
            If Calculate_JobCost(_Spls) Then

                _Spls.Close()


                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Call LoadData()
            Else

                _Spls.Close()
                HI.MG.ShowMsg.mInfo("ไม่สามารถคำนวนต้นทุนการผลิตได้ !!!!", 1604220576, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        End If

    End Sub


    Private Function Calculate_JobCost(_Spls As HI.TL.SplashScreen) As Boolean
        Try

            Dim Date_S As String = ""
            Dim Date_E As String = ""
            Dim _Month As String = ""

            Dim FNMonth_OverHead_Amt As Double = 0
            Dim FNMonth_DL_Amt As Double = 0
            Dim FNMonth_INDL_Amt As Double = 0
            Dim FNMonth_MNG_Amt As Double = 0
            Dim FNMonth_None_Amt As Double = 0

            Dim FNMinute_OverHead_Amt As Double = 0
            Dim FNMinute_DL_Amt As Double = 0
            Dim FNMinute_INDL_Amt As Double = 0
            Dim FNMinute_MNG_Amt As Double = 0
            Dim FNMinute_None_Amt As Double = 0


            Dim _Day_FNMinute_OverHead_Amt As Double = 0
            Dim _Day_FNMinute_DL_Amt As Double = 0
            Dim _Day_FNMinute_INDL_Amt As Double = 0
            Dim _Day_FNMinute_MNG_Amt As Double = 0
            Dim _Day_FNMinute_None_Amt As Double = 0


            Dim _Sum_FNMinute_OverHead_Amt As Double = 0
            Dim _Sum_FNMinute_DL_Amt As Double = 0
            Dim _Sum_FNMinute_INDL_Amt As Double = 0
            Dim _Sum_FNMinute_MNG_Amt As Double = 0
            Dim _Sum_FNMinute_None_Amt As Double = 0


            Dim FNMonth_TotalMinutes As Integer = 0
            Dim FNMonth_TotalQuantity As Integer = 0


            Dim _dt As DataTable
            Dim _dt_Order_Day As DataTable


            Dim _Qry As String = ""


            Dim MM As String = ""

            MM = "0" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)
            MM = MM.Substring(MM.Length - 2, 2)

            Date_S = FTDateStart.Text & "/" & MM & "/01"
            ' _Qry = " SELECT DATEADD(month, ((YEAR('" & Date_S & "') - 1900) * 12) + MONTH('" & Date_S & "'), -1) "

            _Qry = " SELECT Convert(varchar(10),Convert(Datetime,DATEADD(month, ((YEAR('" & Date_S & "') - 1900) * 12) + MONTH('" & Date_S & "'), -1) ),111)  "
            Date_E = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

            ''''''''''''Budget per month''''''''''''''''''''''

            _Qry = " SELECT  SUM(FNAmt) AS FNAmt_OverHead "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTOverHeadCost] "
            _Qry &= vbCrLf & "  WHERE  FTYear = '" & FTDateStart.Text & "'"
            _Qry &= vbCrLf & " AND FNMonth =  " & Val(HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex))
            _Qry &= vbCrLf & "   "
            _Qry &= vbCrLf & "   "
            FNMonth_OverHead_Amt = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, 0)
            '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            'For Each Rovh As DataRow In _dt.Rows
            '    FNMonth_OverHead_Amt = Val(Rovh!FNAmt_OverHead.ToString)
            '    Exit For
            ''Next



            _Qry = "  SELECT SUM(P.FNTotalIncome) AS FNCmpPay "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRoll P "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMCfgPayDT] PD ON PD.FTPayYear=P.FTPayYear and PD.FTPayTerm=P.FTPayTerm AND PD.FNHSysEmpTypeId=P.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployee] E ON E.FNHSysEmpID=P.FNHSysEmpID "
            _Qry &= vbCrLf & "   WHERE P.FTPayYear='" & FTDateStart.Text & "'"
            _Qry &= vbCrLf & "   AND PD.FNMonth = '" & MM & "' "
            _Qry &= vbCrLf & "    AND P.FNJobCost=0  "

            FNMonth_DL_Amt = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, 0)
            '' MsgBox("FNMonth_DL_Amt" & FNMonth_DL_Amt)

            _Qry = "  SELECT SUM(P.FNTotalIncome) AS FNCmpPay "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRoll P "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMCfgPayDT] PD ON PD.FTPayYear=P.FTPayYear and PD.FTPayTerm=P.FTPayTerm AND PD.FNHSysEmpTypeId=P.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployee] E ON E.FNHSysEmpID=P.FNHSysEmpID "
            _Qry &= vbCrLf & "   WHERE P.FTPayYear='" & FTDateStart.Text & "'"
            _Qry &= vbCrLf & "   AND PD.FNMonth = '" & MM & "' "
            _Qry &= vbCrLf & "    AND P.FNJobCost=1  "
            _Qry &= vbCrLf & "   "
            FNMonth_INDL_Amt = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, 0)

            '' MsgBox("FNMonth_INDL_Amt" & FNMonth_INDL_Amt)

            _Qry = "  SELECT SUM(P.FNTotalIncome) AS FNCmpPay "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRoll P "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMCfgPayDT] PD ON PD.FTPayYear=P.FTPayYear and PD.FTPayTerm=P.FTPayTerm AND PD.FNHSysEmpTypeId=P.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployee] E ON E.FNHSysEmpID=P.FNHSysEmpID "
            _Qry &= vbCrLf & "   WHERE P.FTPayYear='" & FTDateStart.Text & "'"
            _Qry &= vbCrLf & "   AND PD.FNMonth = '" & MM & "' "
            _Qry &= vbCrLf & "    AND P.FNJobCost=2  "
            _Qry &= vbCrLf & "   "
            FNMonth_MNG_Amt = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, 0)

            '' MsgBox("FNMonth_MNG_Amt" & FNMonth_MNG_Amt)

            _Qry = "  SELECT SUM(P.FNTotalIncome) AS FNCmpPay "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].THRTPayRoll P "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMCfgPayDT] PD ON PD.FTPayYear=P.FTPayYear and PD.FTPayTerm=P.FTPayTerm AND PD.FNHSysEmpTypeId=P.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMEmployee] E ON E.FNHSysEmpID=P.FNHSysEmpID "
            _Qry &= vbCrLf & "   WHERE P.FTPayYear='" & FTDateStart.Text & "'"
            _Qry &= vbCrLf & "   AND PD.FNMonth = '" & MM & "' "
            _Qry &= vbCrLf & "    AND P.FNJobCost=3  "
            _Qry &= vbCrLf & "   "
            FNMonth_None_Amt = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, 0)
            MsgBox("FNMonth_None_Amt" & FNMonth_None_Amt)


            ' 3 = action sew
            _Qry = "  SELECT  SUM (FNTotalMinute) [FNTotalMinute], SUM(FNQuantity) [FNQuantity] "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRTIncentive_OrderTime] "
            _Qry &= vbCrLf & "  WHERE ISNULL([FTOrderNo],'')<> '' "
            _Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'   "
            'MsgBox(_Qry)
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each Rtx As DataRow In _dt.Rows
                FNMonth_TotalMinutes = Val(Rtx!FNTotalMinute.ToString)
                FNMonth_TotalQuantity = Val(Rtx!FNQuantity.ToString)
                Exit For
            Next


            _Qry = "  SELECT  PV.FTCalDate, PV.FTOrderNo"
            _Qry &= vbCrLf & " , ISNULL(PV.[0],0) AS 'MinCut',ISNULL(PV.[1],0) AS 'MinEmb',ISNULL(PV.[2],0) AS 'MinSuperM',ISNULL(PV.[3],0) AS 'MinSew',ISNULL(PV.[4],0) AS 'MinPack' "
            _Qry &= vbCrLf & " , ISNULL(B.[0],0) AS 'QtyCut',ISNULL(B.[1],0) AS 'QtyEmb',ISNULL(B.[2],0) AS 'QtySuperM',ISNULL(B.[3],0) AS 'QtySew',ISNULL(B.[4],0) AS 'QtyPack' "
            _Qry &= vbCrLf & " , ISNULL(PV.[0],0)  + ISNULL(PV.[1],0) + ISNULL(PV.[2],0) + ISNULL(PV.[3],0)+ ISNULL(PV.[4],0) AS 'FNTotalMinute' "
            _Qry &= vbCrLf & " ,   ISNULL(B.[0],0) + ISNULL(B.[1],0) + ISNULL(B.[2],0)  + ISNULL(B.[3],0) + ISNULL(B.[4],0) AS 'FNQuantity' "
            _Qry &= vbCrLf & " ,   ISNULL(C.[0],0) + ISNULL(C.[1],0) + ISNULL(C.[2],0)  + ISNULL(C.[3],0) + ISNULL(C.[4],0) AS 'FNJobCostDLEmptypeDaily'  "
            _Qry &= vbCrLf & " ,   ISNULL(D.[0],0) + ISNULL(D.[1],0) + ISNULL(D.[2],0)  + ISNULL(D.[3],0) + ISNULL(D.[4],0) AS 'FNEmpTypeMonthlyDLAllCostPerDayAction'  "
            _Qry &= vbCrLf & " ,   ISNULL(E.[0],0) + ISNULL(E.[1],0) + ISNULL(E.[2],0)  + ISNULL(E.[3],0) + ISNULL(E.[4],0) AS 'FNDLAddOtherAmt'  "
            _Qry &= vbCrLf & " ,   ISNULL(F.[0],0) + ISNULL(F.[1],0) + ISNULL(F.[2],0)  + ISNULL(F.[3],0) + ISNULL(F.[4],0) AS 'FNMonthlyDLAddOtherAmt' "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & " ,   ISNULL(C.[0],0) + ISNULL(C.[1],0) + ISNULL(C.[2],0)  + ISNULL(C.[3],0) + ISNULL(C.[4],0)   "
            _Qry &= vbCrLf & " +   ISNULL(D.[0],0) + ISNULL(D.[1],0) + ISNULL(D.[2],0)  + ISNULL(D.[3],0) + ISNULL(D.[4],0)   "
            _Qry &= vbCrLf & " +   ISNULL(E.[0],0) + ISNULL(E.[1],0) + ISNULL(E.[2],0)  + ISNULL(E.[3],0) + ISNULL(E.[4],0)   "
            _Qry &= vbCrLf & " +   ISNULL(F.[0],0) + ISNULL(F.[1],0) + ISNULL(F.[2],0)  + ISNULL(F.[3],0) + ISNULL(F.[4],0) AS 'FNDLDailyAmt' "


            _Qry &= vbCrLf & " FROM  "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT  [FTCalDate],[FTOrderNo], [FNTotalMinute] ,FNAction "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRTIncentive_OrderTime] "
            _Qry &= vbCrLf & " WHERE ISNULL([FTOrderNo],'')<> '' "
            _Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'  "
            _Qry &= vbCrLf & " ) T "
            _Qry &= vbCrLf & "  PIVOT (SUM([FNTotalMinute]) FOR [FNAction] IN ([0],[1],[2],[3],[4])) PV "
            _Qry &= vbCrLf & " LEFT JOIN ( "
            _Qry &= vbCrLf & "  SELECT  [FTCalDate],[FTOrderNo],[0],[1],[2],[3],[4]"
            _Qry &= vbCrLf & "  FROM "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT  [FTCalDate],[FTOrderNo] "
            _Qry &= vbCrLf & " , FNQuantity ,FNAction "
            _Qry &= vbCrLf & "  From .[dbo].[THRTIncentive_OrderTime]  "
            _Qry &= vbCrLf & "  WHERE ISNULL([FTOrderNo],'')<> '' "
            _Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'  "
            _Qry &= vbCrLf & "  ) T "
            _Qry &= vbCrLf & "   PIVOT (SUM(FNQuantity) FOR [FNAction] IN ([0],[1],[2],[3],[4])) PV2 "
            _Qry &= vbCrLf & "   ) B ON PV.FTCalDate = B.FTCalDate AND PV.FTOrderNo= B.FTOrderNo "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & " LEFT JOIN ( "
            _Qry &= vbCrLf & "  SELECT  [FTCalDate],[FTOrderNo],[0],[1],[2],[3],[4]"
            _Qry &= vbCrLf & "  FROM "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT  [FTCalDate],[FTOrderNo] "
            _Qry &= vbCrLf & " , FNJobCostDLEmptypeDaily ,FNAction "
            _Qry &= vbCrLf & "  From .[dbo].[THRTIncentive_OrderTime]  "
            _Qry &= vbCrLf & "  WHERE ISNULL([FTOrderNo],'')<> '' "
            _Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'  "
            _Qry &= vbCrLf & "  ) T "
            _Qry &= vbCrLf & "   PIVOT (SUM(FNJobCostDLEmptypeDaily) FOR [FNAction] IN ([0],[1],[2],[3],[4])) PV3 "
            _Qry &= vbCrLf & "   ) C ON PV.FTCalDate = C.FTCalDate AND PV.FTOrderNo= C.FTOrderNo "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & " LEFT JOIN ( "
            _Qry &= vbCrLf & "  SELECT  [FTCalDate],[FTOrderNo],[0],[1],[2],[3],[4]"
            _Qry &= vbCrLf & "  FROM "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT  [FTCalDate],[FTOrderNo] "
            _Qry &= vbCrLf & " , FNEmpTypeMonthlyDLAllCostPerDayAction ,FNAction "
            _Qry &= vbCrLf & "  From .[dbo].[THRTIncentive_OrderTime]  "
            _Qry &= vbCrLf & "  WHERE ISNULL([FTOrderNo],'')<> '' "
            _Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'  "
            _Qry &= vbCrLf & "  ) T "
            _Qry &= vbCrLf & "   PIVOT (SUM(FNEmpTypeMonthlyDLAllCostPerDayAction) FOR [FNAction] IN ([0],[1],[2],[3],[4])) PV4 "
            _Qry &= vbCrLf & "   ) D ON PV.FTCalDate = D.FTCalDate AND PV.FTOrderNo= D.FTOrderNo "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & " LEFT JOIN ( "
            _Qry &= vbCrLf & "  SELECT  [FTCalDate],[FTOrderNo],[0],[1],[2],[3],[4]"
            _Qry &= vbCrLf & "  FROM "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT  [FTCalDate],[FTOrderNo] "
            _Qry &= vbCrLf & " , FNDLAddOtherAmt ,FNAction "
            _Qry &= vbCrLf & "  From .[dbo].[THRTIncentive_OrderTime]  "
            _Qry &= vbCrLf & "  WHERE ISNULL([FTOrderNo],'')<> '' "
            _Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'  "
            _Qry &= vbCrLf & "  ) T "
            _Qry &= vbCrLf & "   PIVOT (SUM(FNDLAddOtherAmt) FOR [FNAction] IN ([0],[1],[2],[3],[4])) PV5 "
            _Qry &= vbCrLf & "   ) E ON PV.FTCalDate = E.FTCalDate AND PV.FTOrderNo= E.FTOrderNo "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & " LEFT JOIN ( "
            _Qry &= vbCrLf & "  SELECT  [FTCalDate],[FTOrderNo],[0],[1],[2],[3],[4]"
            _Qry &= vbCrLf & "  FROM "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT  [FTCalDate],[FTOrderNo] "
            _Qry &= vbCrLf & " , FNMonthlyDLAddOtherAmt ,FNAction "
            _Qry &= vbCrLf & "  From .[dbo].[THRTIncentive_OrderTime]  "
            _Qry &= vbCrLf & "  WHERE ISNULL([FTOrderNo],'')<> '' "
            _Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'  "
            _Qry &= vbCrLf & "  ) T "
            _Qry &= vbCrLf & "   PIVOT (SUM(FNMonthlyDLAddOtherAmt) FOR [FNAction] IN ([0],[1],[2],[3],[4])) PV6 "
            _Qry &= vbCrLf & "   ) F ON PV.FTCalDate = F.FTCalDate AND PV.FTOrderNo= F.FTOrderNo "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & "     ORDER BY  PV.FTCalDate, PV.FTOrderNo "


            _dt_Order_Day = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            '' 0  = Cut
            '' 1 = Emb
            '' 2 = SuperMarket
            '' 3 = Sew
            '' 4 = pack to box


            '_Qry = "  SELECT  [FTCalDate],[FTOrderNo]"
            '_Qry &= vbCrLf & " "
            '_Qry &= vbCrLf & "  ,SUM (FNTotalMinute) [FNTotalMinute], SUM(FNQuantity) [FNQuantity] "
            '_Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRTIncentive_OrderTime] "
            '_Qry &= vbCrLf & "  WHERE ISNULL([FTOrderNo],'')<> '' "
            '_Qry &= vbCrLf & "  AND FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "'  AND FNAction =3 "
            '_Qry &= vbCrLf & "   GROUP BY [FTCalDate],[FTOrderNo] "
            '_Qry &= vbCrLf & "  ORDER BY [FTCalDate],[FTOrderNo] "
            '_dt_Order_Day = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)



            '''Average Budget per minutes''
            'MsgBox("aa")
            FNMinute_OverHead_Amt = Double.Parse(Format(FNMonth_OverHead_Amt / FNMonth_TotalMinutes, "0.0000"))
            FNMinute_DL_Amt = Double.Parse(Format(FNMonth_DL_Amt / FNMonth_TotalMinutes, "0.0000"))
            FNMinute_INDL_Amt = Double.Parse(Format(FNMonth_INDL_Amt / FNMonth_TotalMinutes, "0.0000"))
            FNMinute_MNG_Amt = Double.Parse(Format(FNMonth_MNG_Amt / FNMonth_TotalMinutes, "0.0000"))
            FNMinute_None_Amt = Double.Parse(Format(FNMonth_None_Amt / FNMonth_TotalMinutes, "0.0000"))



            Dim _FTCalDate As String = ""
            Dim _FTOrderNo As String = ""
            Dim _FNTotalMinute As Integer = 0
            Dim _FNQuantity As Integer = 0

            Dim _FNCutMinute As Integer = 0
            Dim _FNCutQty As Integer = 0
            Dim _FNEmbellishmentMinute As Integer = 0
            Dim _FNEmbellishmentQty As Integer = 0
            Dim _FNSuperMarketMinute As Integer = 0
            Dim _FNSuperMarketQty As Integer = 0
            Dim _FNSewMinute As Integer = 0
            Dim _FNSewQty As Integer = 0
            Dim _FNPackMinute As Integer = 0
            Dim _FNPackQty As Integer = 0


            Dim _last_Row As Integer = 0
            Dim _n As Integer = 0

            'MsgBox("x")
            ''สำหรับ row สุดท้ายเป็นปั่นยอดเฉลี่ยให้ตรง
            _last_Row = _dt_Order_Day.Rows.Count()
            'MsgBox("y")
            _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TFINJobCost]  "
            _Qry &= vbCrLf & " WHERE FTCalDate BETWEEN '" & Date_S & "' AND '" & Date_E & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)


            For Each R As DataRow In _dt_Order_Day.Rows
                _n = _n + 1
                _Spls.UpdateInformation("Calculate of Date " & R!FTCalDate.ToString & "  ( " & _n.ToString & " of  " & _last_Row.ToString & " )")

                _FTCalDate = ""
                _FTOrderNo = ""
                _FNTotalMinute = 0
                _FNQuantity = 0

                _Day_FNMinute_OverHead_Amt = 0
                _Day_FNMinute_DL_Amt = 0
                _Day_FNMinute_INDL_Amt = 0
                _Day_FNMinute_MNG_Amt = 0
                _Day_FNMinute_None_Amt = 0

                _FTCalDate = R!FTCalDate.ToString
                _FTOrderNo = R!FTOrderNo.ToString
                _FNTotalMinute = Val(R!FNTotalMinute.ToString)
                _FNQuantity = Val(R!FNQuantity.ToString)

                _FNCutMinute = Val(R!MinCut.ToString)
                _FNCutQty = Val(R!QtyCut.ToString)
                _FNEmbellishmentMinute = Val(R!MinEmb.ToString)
                _FNEmbellishmentQty = Val(R!QtyEmb.ToString)
                _FNSuperMarketMinute = Val(R!MinSuperM.ToString)
                _FNSuperMarketQty = Val(R!QtySuperM.ToString)
                _FNSewMinute = Val(R!MinSew.ToString)
                _FNSewQty = Val(R!QtySew.ToString)
                _FNPackMinute = Val(R!MinPack.ToString)
                _FNPackQty = Val(R!QtyPack.ToString)

                '' _Day_FNMinute_DL_Amt = Val(R!FNDLDailyAmt.ToString)


                If _n < _last_Row Then

                    If _FNTotalMinute > 0 Then
                        _Day_FNMinute_OverHead_Amt = Double.Parse(Format(FNMinute_OverHead_Amt * _FNTotalMinute, "0.00"))
                        _Day_FNMinute_DL_Amt = Double.Parse(Format(FNMinute_DL_Amt * _FNTotalMinute, "0.00"))
                        _Day_FNMinute_INDL_Amt = Double.Parse(Format(FNMinute_INDL_Amt * _FNTotalMinute, "0.00"))
                        _Day_FNMinute_MNG_Amt = Double.Parse(Format(FNMinute_MNG_Amt * _FNTotalMinute, "0.00"))
                        _Day_FNMinute_None_Amt = Double.Parse(Format(FNMinute_None_Amt * _FNTotalMinute, "0.00"))

                    Else
                        _Day_FNMinute_OverHead_Amt = 0
                        _Day_FNMinute_DL_Amt = 0
                        _Day_FNMinute_INDL_Amt = 0
                        _Day_FNMinute_MNG_Amt = 0
                        _Day_FNMinute_None_Amt = 0
                    End If

                Else

                    _Day_FNMinute_OverHead_Amt = FNMonth_OverHead_Amt - _Sum_FNMinute_OverHead_Amt

                    _Day_FNMinute_DL_Amt = FNMonth_DL_Amt - _Sum_FNMinute_DL_Amt
                    _Day_FNMinute_INDL_Amt = FNMonth_INDL_Amt - _Sum_FNMinute_INDL_Amt
                    _Day_FNMinute_MNG_Amt = FNMonth_MNG_Amt - _Sum_FNMinute_MNG_Amt
                    _Day_FNMinute_None_Amt = FNMonth_None_Amt - _Sum_FNMinute_None_Amt

                End If

                ''เก็บจำนวนเงินเป็น ทศนิยม2ตำแหน่ง
                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TFINJobCost]  "
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime "
                _Qry &= vbCrLf & "  , FTCalDate, FTOrder "
                _Qry &= vbCrLf & " , FNMinute, FNQty "
                _Qry &= vbCrLf & " , FNAmt_OverHead, FNAmt_DL "
                _Qry &= vbCrLf & " , FNAmt_IDL, FNAmt_MNG, FNAmt_None "
                _Qry &= vbCrLf & " , FNCutMinute, FNCutQty, FNEmbellishmentMinute,FNEmbellishmentQty ,FNSuperMarketMinute,FNSuperMarketQty , FNSewMinute, FNSewQty, FNPackMinute , FNPackQty )"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ,'" & _FTCalDate & "' "
                _Qry &= vbCrLf & " ,' " & _FTOrderNo & "' "
                _Qry &= vbCrLf & " ,' " & _FNTotalMinute & "' "
                _Qry &= vbCrLf & " ,' " & _FNQuantity & "' "
                _Qry &= vbCrLf & " , " & _Day_FNMinute_OverHead_Amt
                _Qry &= vbCrLf & " , " & _Day_FNMinute_DL_Amt
                _Qry &= vbCrLf & " , " & _Day_FNMinute_INDL_Amt
                _Qry &= vbCrLf & " , " & _Day_FNMinute_MNG_Amt
                _Qry &= vbCrLf & " ,  " & _Day_FNMinute_None_Amt

                _Qry &= vbCrLf & " ,  " & _FNCutMinute
                _Qry &= vbCrLf & " ,  " & _FNCutQty
                _Qry &= vbCrLf & " ,  " & _FNEmbellishmentMinute
                _Qry &= vbCrLf & " ,  " & _FNEmbellishmentQty
                _Qry &= vbCrLf & " ,  " & _FNSuperMarketMinute
                _Qry &= vbCrLf & " ,  " & _FNSuperMarketQty
                _Qry &= vbCrLf & " ,  " & _FNSewMinute
                _Qry &= vbCrLf & " ,  " & _FNSewQty
                _Qry &= vbCrLf & " ,  " & _FNPackMinute
                _Qry &= vbCrLf & " ,  " & _FNPackQty

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                _Sum_FNMinute_OverHead_Amt = _Sum_FNMinute_OverHead_Amt + _Day_FNMinute_OverHead_Amt
                _Sum_FNMinute_DL_Amt = _Sum_FNMinute_DL_Amt + _Day_FNMinute_DL_Amt
                _Sum_FNMinute_INDL_Amt = _Sum_FNMinute_INDL_Amt + _Day_FNMinute_INDL_Amt
                _Sum_FNMinute_MNG_Amt = _Sum_FNMinute_MNG_Amt + _Day_FNMinute_MNG_Amt
                _Sum_FNMinute_None_Amt = _Sum_FNMinute_None_Amt + _Day_FNMinute_None_Amt

            Next


            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTOverHeadCost] SET "
            _Qry &= vbCrLf & "  FTApproveUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " , FDApproveDate= " & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & " , FTApproveTime=  " & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & "  WHERE  FTYear = '" & FTDateStart.Text & "'"
            _Qry &= vbCrLf & "  AND FNMonth =  " & Val(HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex))
            _Qry &= vbCrLf & "   "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Return True
        Catch ex As Exception
            '' MsgBox(ex.Message.ToString())

            Return False
        End Try
    End Function


    Private Function VerrifyData() As Boolean

        If FTDateStart.Text = "" Then
            'HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityA_lbl.Text)
            'otbdetail.SelectedTabPageIndex = 1
            'oxtb.SelectedTabPageIndex = 0
            'FNMatQuantityA.Focus()
            'Return False
        End If



        If Not (ogcacc.DataSource Is Nothing) Then

            With CType(ogcacc.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTOverHeadCostCode<>''  AND FNAmt=0 ").Length > 0 Then

                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุจำนวนให้ครบ !!!", 112547814, Me.Text,, MessageBoxIcon.Warning)
                    ''     otbdetail.SelectedTabPageIndex = 2

                    Return False
                End If

            End With

        End If

        Dim _N As Integer = 0
        Dim _Qry As String

        _Qry = " SELECT COUNT(*) AS N "

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTOverHeadCost]  "

        _Qry &= vbCrLf & "  WHERE  FTYear = '" & FTDateStart.Text & "'"
        _Qry &= vbCrLf & " AND  FNMonth =  " & Val(HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex))
        _Qry &= vbCrLf & "   AND ISNULL(FTApproveUser,'') <> ''  "
        _Qry &= vbCrLf & "   "
        _N = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, 0)

        If _N > 0 Then
            HI.MG.ShowMsg.mInfo("มีการอนุมัติแล้ว กรุณายกเลิกอนุมัติเมื่อต้องการบันทึก !!!", 112547814, Me.Text,, MessageBoxIcon.Warning)

            Return False
        End If


        Return True

    End Function

    Private Function VerrifyDeleteData() As Boolean

        If FTDateStart.Text = "" Then

            Return False
        End If



        Return True

    End Function

    Private Function ValidateData() As Boolean

        'If FTStartShipDate.Text = "" And FTEndShipDate.Text = "" And FTOrderNo.Text = "" And FTOrderNo_To.Text = "" And FTCustomerPO.Text = "" And FTCustomerPO_To.Text = "" Then
        '    HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูลอย่างน้อยหนึ่งอย่าง !!!", 1511121406, Me.Text)
        '    Return False
        'End If

        If FTDateStart.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาเลือกปี !!!", 1511121406, Me.Text)
            Return False
        End If


        'If StateFTPOref.Checked = False And Me.StateFTStyleCode.Checked = False And StateFTSeasonCode.Checked = False And StateFTColorway.Checked = False _
        '    And StateFTSizeBreakDown.Checked = False And StateFTRawMatCode.Checked = False _
        '    And StateFTMatColorCode.Checked = False And StateFTMatSizeCode.Checked = False And StateFTSuplName.Checked = False _
        '    And StateFTCountryName.Checked = False And StateFDPurchaseDate.Checked = False And StateFTPurchaseNo.Checked = False Then

        '    HI.MG.ShowMsg.mInfo("กรูณาทำการเลือกข้อมูลที่ต้องการทำการแสดง !!!", 1511177546, Me.Text)
        '    Return False
        'End If

        Return True
    End Function

    Private Sub RepositoryFNHSysAccOverHeadCostID_EditValueChanged(sender As Object, e As EventArgs) Handles repositoryFNHSysAccOverHeadCostID.EditValueChanged
        Try
            With Me.ogvacc
                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                Dim FNHSysAccountId As Integer = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysAccountId").ToString())

                .SetFocusedRowCellValue("FNHSysAccOverHeadCostID", FNHSysAccountId)

                ' .SetFocusedRowCellValue("FTOverHeadCostCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTAccountCode").ToString())
                .SetFocusedRowCellValue("FTOverHeadCostDesc", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTAccountName").ToString())
            End With
        Catch ex As Exception

        End Try


    End Sub

    Private Sub ogcacc_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcacc.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                With Me.ogvacc
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogcacc.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNSeq>0", "FNSeq")
                        R!FNSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridDataAcc()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridDataAcc()

            Case Else

        End Select

        e.Handled = True
    End Sub

    Private Sub ogvacc_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvacc.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogcacc.DataSource, DataTable)

                    .AcceptChanges()
                    Call InitGridDataAcc()

                End With

            Case System.Windows.Forms.Keys.Delete

                With Me.ogvacc
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogcacc.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNSeq>0", "FNSeq")
                        R!FNSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridDataAcc()


            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub

    Private Sub InitGridDataAcc()

        Try
            If Not (Me.ogcacc.DataSource Is Nothing) Then



                With CType(Me.ogcacc.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTOverHeadCostCode=''").Length > 0 Then
                    Else

                        .Rows.Add(.Rows.Count + 1, 0, "", "", 0)
                    End If
                End With


            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub wPoDetailTrackingForExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        _StateFormLoad = False
        'Me.StateFTPurchaseNo.Checked = False
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click

        If Me.VerrifyDeleteData Then
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการยกเลิกการคำนวณ JobCost ใช่หรือไม่ ?", 1604228574, Me.Text) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Calculating...   Please Wait   ")

                If Me.DeleteData(_Spls) Then
                    Call LoadData()
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                    Me.ocmload_Click(ocmload, New System.EventArgs)

                Else

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                End If

            End If
        End If

    End Sub

    Private Function DeleteData(Spls As HI.TL.SplashScreen) As Boolean
        Dim _dt As DataTable
        Dim _CalDate As String
        Dim _Qry As String = ""

        Dim Date_S As String = ""
        Dim Date_E As String = ""

        Dim MM As String = ""

        MM = "0" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex)
        MM = MM.Substring(MM.Length - 2, 2)

        Date_S = FTDateStart.Text & "/" & MM & "/01"
        _Qry = " SELECT DATEADD(month, ((YEAR('" & Date_S & "') - 1900) * 12) + MONTH('" & Date_S & "'), -1) "
        Date_E = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


        Try

            ''update TACCTOverHeadCost ApproverUser 
            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTOverHeadCost] SET "
            _Qry &= vbCrLf & "  FTApproveUser= '' "
            _Qry &= vbCrLf & " , FDApproveDate=  ''"
            _Qry &= vbCrLf & " , FTApproveTime=  ''"
            _Qry &= vbCrLf & "  WHERE  FTYear = '" & FTDateStart.Text & "'"
            _Qry &= vbCrLf & " AND  FNMonth =  " & Val(HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex))
            _Qry &= vbCrLf & "   "
            _Qry &= vbCrLf & "   "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            ''Delete TFINJobCost
            _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TFINJobCost]  "
            _Qry &= vbCrLf & " WHERE FTCalDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(Date_S) & "' AND '" & HI.UL.ULDate.ConvertEnDB(Date_E) & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    '    AddHandler gridView.ShownEditor, Sub(s, e)
    '    Dim view As GridView = TryCast(s, GridView)
    '    view.ActiveEditor.Properties.ReadOnly =
    '        gridView.FocusedColumn.FieldName = "ID" AndAlso gridView.FocusedRowHandle Mod 2 = 0
    'End Sub

    'Private Sub ogvtime_Ro(ender As Object, e As EditFormPreparedEventArgs)



    Private Sub ReposFNAmt_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNAmt.EditValueChanging
        'Select Case ogvacc.FocusedColumn.FieldName.ToString

        '    Case "CFNAmt"

        Dim _FTPIState As String = ogvacc.GetRowCellValue(ogvacc.FocusedRowHandle, "FTPIState").ToString()
                If _FTPIState = "1" Then
                    e.Cancel = True
                End If


        'End Select
    End Sub

    'Private Sub ogvtime_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvacc.RowStyle



    '    'If dtTimeColor Is Nothing Then
    '    '    dtTimeColor = HI.HRCAL.Time.LoadTimeColor()

    '    'End If
    '    ogvacc.ActiveEditor.Properties.ReadOnly = ogvacc.FocusedColumn.FieldName = "ID"

    '    With ogvacc
    '        Try
    '            Select Case True
    '                Case ("" & .GetRowCellValue(e.RowHandle, "FTPIState").ToString = "1")
    '                    Try


    '                        ''.OptionsBehavior.ReadOnly = True
    '                        ''.Columns("").ColumnEdit = 

    '                        ''  .OptionsView
    '                        ''e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)


    '                    Catch ex As Exception
    '                    End Try


    '            End Select

    '        Catch ex As Exception

    '        End Try
    '    End With

    'End Sub
End Class
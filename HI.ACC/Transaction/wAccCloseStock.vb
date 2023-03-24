Imports DevExpress.Data
Imports System.IO

Public Class wAccCloseStock

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        ''------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
        '    "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity"

        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
        '     "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity"

        'Dim sFieldCustomSum As String = ""
        'Dim sFieldCustomGrpSum As String = ""

        'With ogvtime
        '    .ClearGrouping()
        '    .ClearDocument()
        '    '.Columns("FTDateTrans").Group()

        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpCount.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

        '    .ExpandAllGroups()
        '    .RefreshData()


        'End With
        ''------End Add Summary Grid-------------
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


    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNTime", "FNOT1", "FNOT1_5", "FNOT2", "FNOT3", "FNOT4"
                If e.SummaryProcess = CustomSummaryProcess.Calculate Then

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsGroupSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        GrpSum = GrpSum + Integer.Parse(Val(Str))
                                End Select
                                Seq = Seq + 1
                            Next
                        End If

                        If e.IsTotalSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        totalSum = totalSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        totalSum = totalSum + Integer.Parse(Val(Str))
                                End Select

                                Seq = Seq + 1
                            Next
                        End If

                    End If

                    If e.IsGroupSummary Then
                        Dim GrpDisplay As String = ""
                        GrpDisplay = Format(((GrpSum) \ 60), "00") & " : " & Format(((GrpSum) Mod 60), "00")
                        e.TotalValue = GrpSum
                    End If

                    If e.IsTotalSummary Then
                        Dim NetDisplay As String = ""

                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        Else
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        End If

                        e.TotalValue = NetDisplay ' totalSum 'NetDisplay

                    End If
                End If
        End Select
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

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        _Qry = " SELECT '0' AS FTSelect,FNHSysWHId,FTWHCode,FNHSysCmpId,FTWHName,FTYear,FTMonth "
        _Qry &= vbCrLf & "   , Left(FTCloseDate,4) AS FTYearClose "
        _Qry &= vbCrLf & "   , Right(Left(FTCloseDate,7),2) AS FTMonthClose"
        _Qry &= vbCrLf & "   FROM (SELECT A.FNHSysWHId"
        _Qry &= vbCrLf & "    , A.FTWHCode"
        _Qry &= vbCrLf & "   , A.FNHSysCmpId"
        _Qry &= vbCrLf & "   , A.FTWHNameTH AS FTWHName"
        _Qry &= vbCrLf & "   , B.FTYear"
        _Qry &= vbCrLf & "   , B.FTMonth "
        _Qry &= vbCrLf & "   , ISNULL(B.FTCloseDate,("
        _Qry &= vbCrLf & "  	SELECT TOP 1 FDDate"
        _Qry &= vbCrLf & "  	FROM ("
        _Qry &= vbCrLf & "  	SELECT TOP 1 FDAdjustStockDate AS FDDate"
        _Qry &= vbCrLf & "  	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock  WITH(NOLOCK)"
        _Qry &= vbCrLf & "    WHERE(FNHSysWHId = A.FNHSysWHId)"
        _Qry &= vbCrLf & "  	ORDER BY FDAdjustStockDate"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  	SELECT TOP 1 FDReceiveDate AS FDDate"
        _Qry &= vbCrLf & "  	FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE(FNHSysWHId = A.FNHSysWHId)"
        _Qry &= vbCrLf & "  	ORDER BY FDReceiveDate"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT TOP 1 FDTransferCenterDate AS FDDate"
        _Qry &= vbCrLf & "  	FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferCenter  WITH(NOLOCK) "
        _Qry &= vbCrLf & "    WHERE(FNHSysWHId = A.FNHSysWHId)"
        _Qry &= vbCrLf & "   ORDER BY FDTransferCenterDate"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT TOP 1 FDTransferWHDate AS FDDate"
        _Qry &= vbCrLf & "  	FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH  WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE(FNHSysWHId = A.FNHSysWHId)"
        _Qry &= vbCrLf & "   ORDER BY FDTransferWHDate"
        _Qry &= vbCrLf & "    ) AS BB"
        _Qry &= vbCrLf & "   ORDER BY FDDate ASC"
        _Qry &= vbCrLf & "   )) AS FTCloseDate"
        _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS A  WITH(NOLOCK)"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   ("
        _Qry &= vbCrLf & "   SELECT FNHSysWHId, Convert(varchar(10),Dateadd(Month,1,Convert(Datetime,FTYear+'/'+FTMonth + '/01')),111) AS FTCloseDate,FTYear,FTMonth"
        _Qry &= vbCrLf & "   FROM ("
        _Qry &= vbCrLf & "   SELECT FNHSysWHId, MAX(FTYear) AS FTYear, MAX(FTMonth) AS FTMonth"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENStockLastMonthly WITH(NOLOCK)"
        _Qry &= vbCrLf & "   GROUP BY FNHSysWHId"
        _Qry &= vbCrLf & "   ) AS AA"
        _Qry &= vbCrLf & "   ) AS B ON A.FNHSysWHId = B.FNHSysWHId"
        _Qry &= vbCrLf & "    WHERE(FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ")"
        _Qry &= vbCrLf & "   ) AS M"
        _Qry &= vbCrLf & "   ORDER BY FTWHCode"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Me.ogdtime.DataSource = _dt

        Me.ochkselectall.Checked = False

        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Call LoadData()
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
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

    Private Sub ocmclosestock_Click(sender As Object, e As EventArgs) Handles ocmclosestock.Click
        Try
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length > 0 Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการปิดสต๊อกประจำเดือนใช่หรือไม่ ?", 1501071120) = True Then

                        Dim _Spls As New HI.TL.SplashScreen("Closing...  Stock  Please Wait.")
                        Dim _Qry As String = ""

                        Try

                            For Each R As DataRow In .Select("FTSelect='1'")

                                If R!FTMonthClose.ToString <> "" And R!FTYearClose.ToString <> "" Then

                                    _Spls.UpdateInformation("Closing Warehouse " & R!FTWHCode.ToString & "   Month  " & R!FTMonthClose.ToString & "/" & R!FTYearClose.ToString)

                                    _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_CloseStock_Monthly '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(R!FNHSysWHId.ToString)) & ",'" & HI.UL.ULF.rpQuoted(R!FTYearClose.ToString & "/" & R!FTMonthClose.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTMonthClose.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTYearClose.ToString) & "' "

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                                End If

                            Next

                        Catch ex As Exception
                        End Try

                        _Spls.Close()
                        Call LoadData()

                        HI.MG.ShowMsg.mInfo(" ปิดสต๊อก เรียบร้อยแล้ว !!!", 1501071121, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)

                    End If

                Else
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลที่ต้องการทำการ ปิดสต๊อก !!!", 1501071125, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmreopenstock_Click(sender As Object, e As EventArgs) Handles ocmreopenstock.Click
        Try
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length > 0 Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการยกเลิกการปิดสต๊อกประจำเดือนใช่หรือไม่ ?", 1501071122) = True Then

                        Dim _Spls As New HI.TL.SplashScreen("Re Opening...  Stock  Please Wait.")
                        Dim _Qry As String = ""
                        Try

                            For Each R As DataRow In .Select("FTSelect='1'")

                                If R!FTMonth.ToString <> "" And R!FTYear.ToString <> "" Then

                                    _Spls.UpdateInformation("Opening Warehouse " & R!FTWHCode.ToString & "   Month  " & R!FTMonth.ToString & "/" & R!FTYear.ToString)

                                    _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_ReOpenStock_Monthly '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(R!FNHSysWHId.ToString)) & ",'" & HI.UL.ULF.rpQuoted(R!FTYear.ToString & "/" & R!FTMonth.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTMonth.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTYear.ToString) & "' "

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                                End If

                            Next
                        Catch ex As Exception
                        End Try

                        _Spls.Close()
                        Call LoadData()

                        HI.MG.ShowMsg.mInfo(" ยกเลิกการปิดสต๊อก เรียบร้อยแล้ว !!!", 1501071124, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                    End If
                Else
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลที่ต้องการทำการ ยกเลิกปิดสต๊อก !!!", 1501071129, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogdtime
                If Not (.DataSource Is Nothing) And ogvtime.RowCount > 0 Then

                    With ogvtime
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
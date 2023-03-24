Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wTruckSheetTrack

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Call InitGrid()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
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

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            '.OptionsSelection.MultiSelect = True
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .ExpandAllGroups()
            .RefreshData()


        End With




        '------End Add Summary Grid-------------
    End Sub
#End Region



#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try
            _Qry = " Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.SP_GET_TruckSheet_Track " & Val(Me.FNHSysCmpId.Properties.Tag) & ","
            _Qry &= "" & Val(Me.FNHSysCustId.Properties.Tag) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipment.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndShipment.Text) & "',"
            _Qry &= "'" & HI.UL.ULDate.ConvertEnDB(Me.FDStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            Me.ogc.DataSource = dt.Copy
            '  Call LoadDataHistory()
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
        dt.Dispose()
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysCmpId.Text <> "" And FNHSysCmpId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysCustId.Text <> "" And FNHSysCustId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTStartShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndShipment.Text <> "" Then
            _Pass = True
        End If


        If Me.FDStartDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FDEndDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTCustomerPO.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

    Private Function SaveGacDate() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm"
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc ,FTNikePOLineItem)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & ",ISNULL(("
                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                _Qry &= vbCrLf & "),0) + 1 "
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginal.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If


                '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " and  FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan"
                '_Qry &= vbCrLf & "SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
                _Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



                _Qry = "UPDATE A  set A.FDShipDate ='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan AS A"
                _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D AS B WITH(NOLOCK) ON A.FTPckPlanNo = B.FTPckPlanNo and A.FTPORef = B.FTPORef and A.FTPORefNo = A.FTPORefNo"
                _Qry &= vbCrLf & " where A.FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Qry &= vbCrLf & "and convert(nvarchar(5) ,convert(int,  B.FTPOLineNo)) ='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    'Return False
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return False
        End Try

        Return True
    End Function


    Private Function UpdateCfmDate() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                _Qry &= vbCrLf & " SET FDCfmShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",FDORShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",FTReasonDesc='" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
                _Qry &= vbCrLf & "where  FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & "and FNSeq = ISNULL(("
                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                _Qry &= vbCrLf & "),0)  "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & ",ISNULL(("
                    _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                    _Qry &= vbCrLf & "),0) + 1 "
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If


                End If

                '_Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
                '_Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
                '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '    HI.Conn.SQLConn.Tran.Rollback()
                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '    _Spls.Close()
                '    Return False
                'End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return False
        End Try

        Return True
    End Function


    Private Sub SendMailToProductin()
        Dim _Spls As New HI.TL.SplashScreen("Sending Mail....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""
        Dim dtcmp As New DataTable
        Dim dtmailto As DataTable
        dtcmp.Columns.Add("FNHSysCmpId", GetType(Integer))
        dtcmp.Columns.Add("FTCmpCode", GetType(String))
        Try
            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
                If dtcmp.Select("").Length <= 0 Then

                    dtcmp.Rows.Add(Integer.Parse(Val(R!FNHSysCmpId.ToString)), R!FTCmpCode.ToString)

                End If
            Next

            For Each Rcmp As DataRow In dtcmp.Rows

                _Qry = " SELECT DISTINCT MM.FTUserName "
                _Qry &= Environment.NewLine & "FROM (SELECT A.FTUserName, A.FNHSysPermissionID"
                _Qry &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLoginPermission AS A (NOLOCK)"
                _Qry &= Environment.NewLine & "      WHERE A.FNHSysPermissionID > 0"
                _Qry &= Environment.NewLine & "            AND EXISTS (SELECT 'T'"
                _Qry &= Environment.NewLine & "                        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEPermissionCmp AS LL (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS NN (NOLOCK) ON LL.FNHSysCmpId = NN.FNHSysCmpId"
                _Qry &= Environment.NewLine & "                        WHERE LL.FNHSysCmpId = " & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " "
                _Qry &= Environment.NewLine & "  			                 AND A.FNHSysPermissionID = LL.FNHSysPermissionID"
                _Qry &= Environment.NewLine & " 		               )"
                _Qry &= Environment.NewLine & "      ) AS MM INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLogin AS KK (NOLOCK) ON MM.FTUserName = KK.FTUserName"
                _Qry &= Environment.NewLine & "              LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS PP (NOLOCK) ON KK.FNHSysTeamGrpId = PP.FNHSysTeamGrpId"
                _Qry &= Environment.NewLine & "WHERE KK.FTStateActive = N'1'  AND (PP. FTStatePurchase='1' OR PP.FTStateProd='1' OR PP.FTStateQA='1' OR PP.FTStateQAFinal='1') "

                dtmailto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

                If dtmailto.Rows.Count > 0 Then
                    Dim tmpsubject As String = ""
                    Dim tmpmessage As String = ""
                    Dim _UserMailTo As String = ""

                    tmpsubject = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString
                    tmpmessage = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString

                    For Each R As DataRow In _dt.Select(" ( FTStateChange='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " ) OR (FTStateChangeO='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & "  )  ")

                        tmpmessage &= vbCrLf & "Factory Order No : " & R!FTOrderNo.ToString & " Sub Order No : " & R!FTSubOrderNo.ToString
                        If R!FTStateChange.ToString = "1" Then
                            tmpmessage &= vbCrLf & "       Change Shipment From " & HI.UL.ULDate.ConvertEN(R!FDShipDate.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateTo.ToString)
                        End If

                        If R!FTStateChangeO.ToString = "1" Then
                            tmpmessage &= vbCrLf & "       Change O GAC Date From " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginal.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginalTo.ToString)
                        End If

                    Next

                    For Each Rm As DataRow In dtmailto.Rows

                        _UserMailTo = Rm!FTUserName.ToString
                        HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, -1, "")

                    Next

                End If

            Next
        Catch ex As Exception

        End Try
        _Spls.Close()
    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If Me.VerifyData() Then
            Call LoadData()
        End If

    End Sub

    Private Sub ReposFDShipDateTo_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFDShipDateTo.EditValueChanging
        Try
            With Me.ogv
                Select Case .FocusedColumn.FieldName.ToString.ToLower
                    Case "FDShipDateTo".ToLower
                        If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) = "" & .GetFocusedRowCellValue("FDShipDateT").ToString Then
                            .SetFocusedRowCellValue("FTStateChange", "0")
                        Else
                            .SetFocusedRowCellValue("FTStateChange", "1")
                        End If
                    Case "FDShipDateOrginalTo".ToLower
                        If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) = "" & .GetFocusedRowCellValue("FDShipDateOrginalT").ToString Then
                            .SetFocusedRowCellValue("FTStateChangeO", "0")
                        Else
                            .SetFocusedRowCellValue("FTStateChangeO", "1")
                        End If
                    Case "FDCfmShipDate".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")


                    Case "FDORShipDate".ToLower
                        .SetFocusedRowCellValue("FTStateChangeO", "1")
                        .SetRowCellValue(.FocusedRowHandle, "FDCfmShipDate", e.NewValue)

                        'Dim selectedRowHandles As Int32() = .GetSelectedRows()
                        'Dim I As Integer
                        'For I = 0 To selectedRowHandles.Length - 1
                        '    Dim selectedRowHandle As Int32 = selectedRowHandles(I)
                        '    If (selectedRowHandle >= 0) Then
                        '        .SetRowCellValue(selectedRowHandle, "FDCfmShipDate", e.NewValue)
                        '    End If
                        'Next
                        For Ix As Integer = 0 To .RowCount Step 1
                            If .GetRowCellValue(Ix, "FTSelect").ToString = "1" Then
                                .SetRowCellValue(Ix, "FDORShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTStateChangeO", "1")
                                .SetRowCellValue(Ix, "FDCfmShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTSelect", "0")
                            End If
                        Next


                End Select

            End With

            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        With Me.ogv
            Try

                Select Case True
                    Case (("" & .GetRowCellValue(e.RowHandle, "FTStateChange").ToString = "1") Or ("" & .GetRowCellValue(e.RowHandle, "FTStateChangeO").ToString = "1"))
                        Try
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Catch ex As Exception
                        End Try
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTSelect").ToString = "1")
                        e.Appearance.BackColor = System.Drawing.Color.CornflowerBlue
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTSelect").ToString = "0")
                        e.Appearance.BackColor = System.Drawing.Color.Transparent
                End Select

            Catch ex As Exception
            End Try
        End With
    End Sub

    Private Sub ReposFDShipDateTo_Spin(sender As Object, e As SpinEventArgs) Handles ReposFDShipDateTo.Spin
        e.Handled = True
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Not (Me.ogc.DataSource Is Nothing) Then
            Dim _dt As DataTable
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With

            If _dt.Select("FTStateChange='1' OR FTStateChangeO='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then

                    If Me.SaveGacDate() Then
                        '  Call SendMailToProductin()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.LoadData()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If

                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub


    Private Sub ocmsavedocument_Click(sender As Object, e As EventArgs) Handles ocmsavedocument.Click
        If Not (Me.ogc.DataSource Is Nothing) Then
            Dim _dt As DataTable
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With

            If _dt.Select("FTStateChange='1' OR FTStateChangeO='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then
                    If Me.SaveGacDate() Then
                        '      Call SendMailToProductin()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.LoadData()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub



    Private Sub ogv_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ogv.SelectionChanged
        Try
            With Me.ogv
                If .RowCount > 0 And .FocusedRowHandle > -1 Then
                    For Each i As Integer In .GetSelectedRows()
                        If .GetRowCellValue(i, "FTSelect") = "0" Then
                            .SetRowCellValue(i, "FTSelect", "1")
                        Else
                            .SetRowCellValue(i, "FTSelect", "0")
                        End If
                    Next


                End If
            End With
        Catch ex As Exception

        End Try
    End Sub



    'Private Sub ogv_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles ogv.CellValueChanged
    '    OnCellValueChanged(e)
    'End Sub

#Region "Multiselect"




#End Region


End Class
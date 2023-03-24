Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraPrinting
Imports Microsoft.Office.Interop.Excel

Public Class wPartialShipmentTrack

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Call InitGrid()
        Call InitGridInfo()
    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "" '"FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity"
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvtrack
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
        Dim dt As New System.Data.DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = "Select  * "
            _Qry &= vbCrLf & "  From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.V_OrderInfo_PartialShipment_Track As T"
            _Qry &= vbCrLf & " WHERE FTPORef <>'' "

            If FNHSysCmpId.Text <> "" Then
                _Qry &= vbCrLf & "  AND T.FNHSysCmpId =" & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ""
            End If

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  T.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & ""
            End If


            If FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND SUBSTRING( FDShipDate  , 7,4) +'/'+ SUBSTRING( FDShipDate  , 4,2) +'/'+ SUBSTRING( FDShipDate  , 1,2) >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "' "
            End If

            If FTEndShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND SUBSTRING( FDShipDate  , 7,4) +'/'+ SUBSTRING( FDShipDate  , 4,2) +'/'+ SUBSTRING( FDShipDate  , 1,2) <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "' "
            End If

            If FNHSysCustId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  T.FNHSysCustId =" & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & ""
            End If

            If FTCustomerPO.Text <> "" Then
                _Qry &= vbCrLf & "  AND T.FTPORef ='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "' "
            End If
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogctrack.DataSource = dt.Copy

            '  Call LoadDataHistory()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True

        If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

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

        'If Me.FTStartOrderDate.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTEndOrderDate.Text <> "" Then
        '    _Pass = True
        'End If

        If Me.FTCustomerPO.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

    'Private Function SaveGacDate() As Boolean

    '    Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
    '    Dim _dt As DataTable
    '    With CType(Me.ogc.DataSource, DataTable)
    '        .AcceptChanges()
    '        _dt = .Copy()
    '    End With
    '    Dim _Qry As String = ""

    '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '    Try

    '        For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")

    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm"
    '            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc ,FTNikePOLineItem)"
    '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & ",ISNULL(("
    '            _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
    '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm"
    '            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
    '            _Qry &= vbCrLf & "),0) + 1 "
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginal.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                _Spls.Close()
    '                Return False
    '            End If


    '            '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

    '            '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
    '            '_Qry &= vbCrLf & " and  FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
    '            '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan"
    '            '_Qry &= vbCrLf & "SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
    '            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
    '            _Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
    '            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

    '            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



    '            _Qry = "UPDATE A  set A.FDShipDate ='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
    '            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan AS A"
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D AS B WITH(NOLOCK) ON A.FTPckPlanNo = B.FTPckPlanNo and A.FTPORef = B.FTPORef and A.FTPORefNo = A.FTPORefNo"
    '            _Qry &= vbCrLf & " where A.FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
    '            _Qry &= vbCrLf & "and convert(nvarchar(5) ,convert(int,  B.FTPOLineNo)) ='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                'HI.Conn.SQLConn.Tran.Rollback()
    '                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                _Spls.Close()
    '                'Return False
    '            End If

    '        Next

    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        _Spls.Close()
    '        Return True
    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        _Spls.Close()
    '        Return False
    '    End Try

    '    Return True
    'End Function


    'Private Function UpdateCfmDate() As Boolean

    '    Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
    '    Dim _dt As DataTable
    '    With CType(Me.ogc.DataSource, DataTable)
    '        .AcceptChanges()
    '        _dt = .Copy()
    '    End With
    '    Dim _Qry As String = ""

    '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '    Try

    '        For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
    '            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
    '            _Qry &= vbCrLf & " SET FDCfmShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
    '            _Qry &= vbCrLf & ",FDORShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
    '            _Qry &= vbCrLf & ",FTReasonDesc='" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
    '            _Qry &= vbCrLf & "where  FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & "and FNSeq = ISNULL(("
    '            _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
    '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
    '            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
    '            _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
    '            _Qry &= vbCrLf & "),0)  "

    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
    '                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc)"
    '                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
    '                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
    '                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
    '                _Qry &= vbCrLf & ",ISNULL(("
    '                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
    '                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
    '                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
    '                _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
    '                _Qry &= vbCrLf & "),0) + 1 "
    '                _Qry &= vbCrLf & ",''"
    '                _Qry &= vbCrLf & ",''"
    '                _Qry &= vbCrLf & ",''"
    '                _Qry &= vbCrLf & ",''"
    '                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
    '                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
    '                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"

    '                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    _Spls.Close()
    '                    Return False
    '                End If


    '            End If

    '            '_Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
    '            '_Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
    '            '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

    '            '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

    '            'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            '    HI.Conn.SQLConn.Tran.Rollback()
    '            '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            '    _Spls.Close()
    '            '    Return False
    '            'End If

    '        Next

    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        _Spls.Close()
    '        Return True
    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        _Spls.Close()
    '        Return False
    '    End Try

    '    Return True
    'End Function


    'Private Sub SendMailToProductin()
    '    Dim _Spls As New HI.TL.SplashScreen("Sending Mail....Please Wait.")
    '    Dim _dt As DataTable
    '    With CType(Me.ogc.DataSource, DataTable)
    '        .AcceptChanges()
    '        _dt = .Copy()
    '    End With
    '    Dim _Qry As String = ""
    '    Dim dtcmp As New DataTable
    '    Dim dtmailto As DataTable
    '    dtcmp.Columns.Add("FNHSysCmpId", GetType(Integer))
    '    dtcmp.Columns.Add("FTCmpCode", GetType(String))
    '    Try
    '        For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
    '            If dtcmp.Select("").Length <= 0 Then

    '                dtcmp.Rows.Add(Integer.Parse(Val(R!FNHSysCmpId.ToString)), R!FTCmpCode.ToString)

    '            End If
    '        Next

    '        For Each Rcmp As DataRow In dtcmp.Rows

    '            _Qry = " SELECT DISTINCT MM.FTUserName "
    '            _Qry &= Environment.NewLine & "FROM (SELECT A.FTUserName, A.FNHSysPermissionID"
    '            _Qry &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLoginPermission AS A (NOLOCK)"
    '            _Qry &= Environment.NewLine & "      WHERE A.FNHSysPermissionID > 0"
    '            _Qry &= Environment.NewLine & "            AND EXISTS (SELECT 'T'"
    '            _Qry &= Environment.NewLine & "                        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEPermissionCmp AS LL (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS NN (NOLOCK) ON LL.FNHSysCmpId = NN.FNHSysCmpId"
    '            _Qry &= Environment.NewLine & "                        WHERE LL.FNHSysCmpId = " & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " "
    '            _Qry &= Environment.NewLine & "  			                 AND A.FNHSysPermissionID = LL.FNHSysPermissionID"
    '            _Qry &= Environment.NewLine & " 		               )"
    '            _Qry &= Environment.NewLine & "      ) AS MM INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLogin AS KK (NOLOCK) ON MM.FTUserName = KK.FTUserName"
    '            _Qry &= Environment.NewLine & "              LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS PP (NOLOCK) ON KK.FNHSysTeamGrpId = PP.FNHSysTeamGrpId"
    '            _Qry &= Environment.NewLine & "WHERE KK.FTStateActive = N'1'  AND (PP. FTStatePurchase='1' OR PP.FTStateProd='1' OR PP.FTStateQA='1' OR PP.FTStateQAFinal='1') "

    '            dtmailto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

    '            If dtmailto.Rows.Count > 0 Then
    '                Dim tmpsubject As String = ""
    '                Dim tmpmessage As String = ""
    '                Dim _UserMailTo As String = ""

    '                tmpsubject = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString
    '                tmpmessage = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString

    '                For Each R As DataRow In _dt.Select(" ( FTStateChange='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " ) OR (FTStateChangeO='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & "  )  ")

    '                    tmpmessage &= vbCrLf & "Factory Order No : " & R!FTOrderNo.ToString & " Sub Order No : " & R!FTSubOrderNo.ToString
    '                    If R!FTStateChange.ToString = "1" Then
    '                        tmpmessage &= vbCrLf & "       Change Shipment From " & HI.UL.ULDate.ConvertEN(R!FDShipDate.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateTo.ToString)
    '                    End If

    '                    If R!FTStateChangeO.ToString = "1" Then
    '                        tmpmessage &= vbCrLf & "       Change O GAC Date From " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginal.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginalTo.ToString)
    '                    End If

    '                Next

    '                For Each Rm As DataRow In dtmailto.Rows

    '                    _UserMailTo = Rm!FTUserName.ToString
    '                    HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, -1, "")

    '                Next

    '            End If

    '        Next
    '    Catch ex As Exception

    '    End Try
    '    _Spls.Close()
    'End Sub
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
            With Me.ogvtrack
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

            With CType(Me.ogctrack.DataSource, DataTable)
                .AcceptChanges()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtrack.RowCellStyle
        With Me.ogvtrack
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
        'If Not (Me.ogc.DataSource Is Nothing) Then
        '    Dim _dt As DataTable
        '    With CType(Me.ogc.DataSource, DataTable)
        '        .AcceptChanges()
        '        _dt = .Copy()
        '    End With

        '    If _dt.Select("FTStateChange='1' OR FTStateChangeO='1'").Length > 0 Then
        '        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then

        '            If Me.SaveGacDate() Then
        '                '  Call SendMailToProductin()
        '                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '                Me.LoadData()
        '            Else
        '                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '            End If

        '        End If
        '    Else
        '        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    End If

        'End If
    End Sub


    Private Sub ocmsavedocument_Click(sender As Object, e As EventArgs) Handles ocmsavedocument.Click
        'If Not (Me.ogc.DataSource Is Nothing) Then
        '    Dim _dt As DataTable
        '    With CType(Me.ogc.DataSource, DataTable)
        '        .AcceptChanges()
        '        _dt = .Copy()
        '    End With

        '    If _dt.Select("FTStateChange='1' OR FTStateChangeO='1'").Length > 0 Then
        '        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then
        '            If Me.SaveGacDate() Then
        '                '      Call SendMailToProductin()
        '                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '                Me.LoadData()
        '            Else
        '                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '            End If
        '        End If
        '    Else
        '        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    End If

        'End If
    End Sub


    Private Sub ogv_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ogvtrack.SelectionChanged
        Try
            With Me.ogvtrack
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

    Private Sub Ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try

            Call generateExcelfile()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub generateExcelfile()
        Try
            Dim _Cmd As String = ""
            With DirectCast(Me.ogctrack.DataSource, System.Data.DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTSelect='1'")
                    Dim _StyleId As Integer = 0
                    _Cmd = "Select top 1  FNHSysStyleId From "
                    Call loadDataDeail(R!FTPORef.ToString, R!FTNikePOLineItem.ToString)

                    Dim _GControl As New DevExpress.XtraGrid.GridControl
                    Dim _Gcontrol2 As New DevExpress.XtraGrid.GridControl
                    Dim _GView As New DevExpress.XtraGrid.Views.Grid.GridView
                    ' _GView = DirectCast(DirectCast(xtab.Controls.Find("ogcPlandD" & xtab.Text, True)(0), DevExpress.XtraGrid.GridControl), DevExpress.XtraGrid.Views.Grid.GridView)
                    _GControl = Me.ogc
                    _Gcontrol2 = Me.ogdColorSizeBreakdown

                    'ExitExcel(R!FTPORef.ToString, R!FTNikePOLineItem.ToString)
                    Dim _odt As System.Data.DataTable
                    Dim _odtd As System.Data.DataTable
                    With DirectCast(Me.ogc.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        _odt = .Copy
                    End With
                    With DirectCast(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        _odtd = .Copy
                    End With
                    Dim _State As Boolean = False
                    _State = (R!FTStateShort.ToString = "1")
                    NewExcelNew_Form(_odt, _odtd, _State)

                Next
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub loadDataDeail(CustomerPO As String, POLineNo As String)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable


            _Cmd = "Select  * From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.V_OrderInfo_PartialShipment"
            _Cmd &= vbCrLf & " where FTPOref='" & HI.UL.ULF.rpQuoted(CustomerPO) & "'"
            _Cmd &= vbCrLf & " and  FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(POLineNo) & "'"
            Me.ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)

            _Cmd = " exec " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.SP_GET_OrderBreak_ForPartial  '" & HI.UL.ULF.rpQuoted(CustomerPO) & "','" & HI.UL.ULF.rpQuoted(POLineNo) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim _colcount As Integer = 0
            With Me.GridView1
                .OptionsView.ShowAutoFilterRow = False
                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNTotal".ToUpper, "FTDescription".ToUpper, "FNSeq".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper
                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next
                If Not (_oDt Is Nothing) Then
                    For Each Col As DataColumn In _oDt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNTotal".ToUpper, "FTDescription".ToUpper, "FNSeq".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper
                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = "c" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString

                                End With

                                .Columns.Add(ColG)
                                Dim Ctrl As New Object
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

                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit

                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit)
                                        .Name = "crep" & Col.ColumnName.ToString
                                        .Precision = 0
                                        .DisplayFormat.FormatType = FormatType.Numeric
                                        .DisplayFormat.FormatString = "N0"
                                        .Buttons(0).Visible = False
                                        ' AddHandler .EditValueChanging, AddressOf CalSet_EditValueChanging
                                    End With


                                    .ColumnEdit = Ctrl
                                End With

                                .Columns(Col.ColumnName.ToString).Width = 65
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


            Me.ogdColorSizeBreakdown.DataSource = _oDt.Copy

        Catch ex As Exception

        End Try
    End Sub


    Private Sub InitGridInfo()
        Try
            Dim _Cmd As String = "" : Dim _Qry As String = ""
            Dim _FieldName As String = ""
            Dim _dt As System.Data.DataTable
            Dim _ColCount As Integer = 0

            _Cmd = "Select   * From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysObjDynamic_D with(nolock) where  (FNObjID = 99999001)"
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SYSTEM)
            With Me.ogv
                .Columns.Clear()
                For Each Row As DataRow In _dt.Select("FNSeq>0", "FNSeq asc")


                    _FieldName = Row!FTFiledName.ToString


                    Dim GridCol As New DevExpress.XtraGrid.Columns.GridColumn
                    Dim Ctrl As New Object
                    With GridCol
                        .Name = _FieldName
                        .FieldName = _FieldName
                        .Width = Val(Row!FNColWidth.ToString)

                        .AppearanceCell.Options.UseTextOptions = True
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = VertAlignment.Center
                        .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                        .OptionsColumn.ShowInCustomizationForm = False
                        .OptionsColumn.AllowSize = True
                        .VisibleIndex = _ColCount

                        .Visible = (Row!FTStateShowInGrid.ToString = "Y")
                        .OptionsColumn.ReadOnly = True
                        .OptionsColumn.AllowEdit = False
                        .Caption = Row!FTFiledName.ToString


                        Select Case Row!FTFormControlType.ToString.ToUpper
                            Case "TextEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                                With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemTextEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .MaxLength = Val(Row!FNMaxLenght.ToString)

                                    If Row!FTStaGridTextUpper.ToString = "Y" Then
                                        .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                    End If
                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case "CalcEdit".ToUpper

                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit

                                With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .Precision = Val(Row!FNNumericScale.ToString)
                                    .DisplayFormat.FormatType = FormatType.Numeric
                                    .DisplayFormat.FormatString = "N" & Val(Row!FNNumericScale.ToString).ToString

                                End With

                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            Case "MemoEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                    .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)
                                End With

                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case "DateEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
                                With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .AllowNullInput = DefaultBoolean.False
                                    .DisplayFormat.FormatString = "d"
                                    .DisplayFormat.FormatType = FormatType.Custom
                                    .EditFormat.FormatString = "d"
                                    .EditFormat.FormatType = FormatType.Custom
                                    .Mask.EditMask = "d"

                                    .ShowClear = True

                                    '    AddHandler .Leave, AddressOf RepositoryItemDate_Leave
                                    '  AddHandler .Click, AddressOf RepositoryItemDate_GotFocus

                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            Case "CheckEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                    .ValueChecked = "1"
                                    .ValueUnchecked = "0"
                                End With

                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            Case "ButtonEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
                                With CType(Ctrl, DevExpress.XtraEditors.ButtonEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .Properties.Buttons.Item(0).Tag = Row!FNButtonEditBrwID.ToString
                                    .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)


                                    If Row!FTStaTextUpper.ToString = "Y" Then
                                        .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                    End If

                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case "ComboBoxEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case Else
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                    .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)

                                    If Row!FTStaGridTextUpper.ToString = "Y" Then
                                        .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                    End If

                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                        End Select

                        .ColumnEdit = Ctrl

                    End With

                    .Columns.Add(GridCol)
                    _ColCount = _ColCount + 1

                Next


            End With


        Catch ex As Exception

        End Try
    End Sub


    Private Sub ExitExcel(CustomerPO As String, POLine As String)
        Try

            Dim _GControl As New DevExpress.XtraGrid.GridControl
            Dim _Gcontrol2 As New DevExpress.XtraGrid.GridControl
            Dim _GView As New DevExpress.XtraGrid.Views.Grid.GridView
            ' _GView = DirectCast(DirectCast(xtab.Controls.Find("ogcPlandD" & xtab.Text, True)(0), DevExpress.XtraGrid.GridControl), DevExpress.XtraGrid.Views.Grid.GridView)
            _GControl = Me.ogc
            _Gcontrol2 = Me.ogdColorSizeBreakdown

            Dim CompositeLink As New DevExpress.XtraPrintingLinks.CompositeLink(New PrintingSystem())
            Dim link As New DevExpress.XtraPrinting.PrintableComponentLink()
            link.Component = _GControl
            CompositeLink.Links.Add(link)

            link = New DevExpress.XtraPrinting.PrintableComponentLink()
            link.Component = _Gcontrol2
            CompositeLink.Links.Add(link)

            Dim _GControl3 As New DevExpress.XtraGrid.GridControl
            Dim _Path As String = "C:\ExportDataForm_WISDOM\"


            If Not (Directory.Exists(_Path)) Then
                My.Computer.FileSystem.CreateDirectory(_Path)
            End If

            _Path = _Path & CustomerPO & "_" & POLine & ".xlsx"

            If _Path <> "" Then

                Try
                    If _Path <> "" Then

                        Dim options = New XlsxExportOptions()
                        options.ExportMode = XlsxExportMode.SingleFile

                        CompositeLink.CreateDocument()
                        CompositeLink.ExportToXlsx(_Path, options)

                        Try
                            Process.Start(_Path)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try

            End If



        Catch ex As Exception

        End Try
    End Sub


    Private _FileName As String = ""
    Private Sub NewExcelNew_Form(ByVal _oDt As System.Data.DataTable, _oDtDetail As System.Data.DataTable, StateShotship As Boolean)
        Try

            Dim _Qry As String = ""
            Dim _DateNow As String = HI.Conn.SQLConn.GetField(HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_PLANNING, "")
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0
            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
            Dim TmpFile As String = _Path & "\Reports\BDPartialShipmentTemp.xlsx"
            Dim BakFile As String = _Path & "\Reports\Blank.xlsx"
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook

            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection

            xlBookTmp = oExcel.Workbooks.Open(TmpFile)


            xlBookTmp.Worksheets(1).copy(After:=xlBookTmp.Worksheets(1))


            With xlBookTmp.Worksheets(1)

                Select Case StateShotship
                    Case True
                        .Cells(7, 3) = "X"
                    Case False
                        .Cells(7, 7) = "X"
                End Select

            End With

            ' header
            Dim rowInd As Integer = 10
            With xlBookTmp.Worksheets(1)
                If _oDt.Rows.Count > 0 Then
                    For Each R As DataRow In _oDt.Rows
                        .Cells(rowInd, 2) = R!FTCmpCode.ToString
                        .Cells(rowInd, 3) = R!FTPOref.ToString
                        .Cells(rowInd, 4) = R!FTNikePOLineItem.ToString
                        .Cells(rowInd, 5) = R!FTStyleCode.ToString
                        .Cells(rowInd, 6) = ""
                        .Cells(rowInd, 7) = R!FDShipDateOrginal.ToString
                        .Cells(rowInd, 8) = R!FDShipDate.ToString
                        .Cells(rowInd, 9) = ""
                        .Cells(rowInd, 10) = ""
                        .Cells(rowInd, 11) = R!FTPlantCode.ToString
                        .Cells(rowInd, 12) = R!FTShipModeCode.ToString
                        .Cells(rowInd, 13) = R!FTProdTypeNameEN.ToString
                        .Cells(rowInd, 14) = ""
                        .Cells(rowInd, 15) = ""
                        .Cells(rowInd, 16) = R!FTSeasonCode.ToString
                        .Cells(rowInd, 17) = ""
                        .Cells(rowInd, 18) = ""
                        .Cells(rowInd, 19) = "#"
                        .Cells(rowInd, 20) = R!FTCountryNameEN.ToString
                        .Cells(rowInd, 21) = R!FNGrandQuantity.ToString

                        Exit For
                    Next



                End If


            End With


            rowInd = 13
            Dim _RSeq As Integer = 0
            With xlBookTmp.Worksheets(1)
                If _oDt.Rows.Count > 0 Then
                    For Each R As DataRow In _oDtDetail.Select("FNSeq <=2", "FNSeq asc  , FNRowId asc ")
                        If Val(R!FNSeq.ToString) = _RSeq And Val(R!FNSeq.ToString) <> 0 Then
                            .Rows(rowInd).Insert()
                            .Cells(rowInd, 2) = "SHIPABLE QTY SHIP "
                            .Cells(rowInd, 16).Formula = "=SUM(D" & rowInd & ",E" & rowInd & ",F" & rowInd & ",G" & rowInd & ",H" & rowInd & ",I" & rowInd & ",J" & rowInd & ",K" & rowInd & ",L" & rowInd & ",M" & rowInd & ",N" & rowInd & ",O" & rowInd & ")"
                        End If
                        Dim xcolumn As Integer = 4

                        For Ix As Integer = 0 To _oDtDetail.Columns.Count - 1
                            Select Case _oDtDetail.Columns(Ix).ColumnName.ToString.ToUpper
                                Case "FTNewNikePOLineItem".ToUpper, "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper
                                Case Else

                                    If rowInd = 13 Then
                                        .Cells(rowInd - 1, xcolumn) = HI.UL.ULF.rpQuoted(_oDtDetail.Columns(Ix).ColumnName.ToString)
                                        .Cells(rowInd, xcolumn) = Integer.Parse(Val(R.Item(_oDtDetail.Columns(Ix).ColumnName.ToString).ToString))
                                        .Cells(rowInd, 17) = R.Item("FDShipDate").ToString
                                    Else
                                        .Cells(rowInd, xcolumn) = Integer.Parse(Val(R.Item(_oDtDetail.Columns(Ix).ColumnName.ToString).ToString))
                                        .Cells(rowInd, 17) = R.Item("FDShipDate").ToString
                                    End If

                                    xcolumn += +1

                            End Select
                        Next
                        _RSeq = Val(R!FNSeq.ToString)
                        rowInd += +1
                    Next



                End If


            End With


            Try
                If oExcel.Application.Sheets.Count > 1 Then
                    For xi As Integer = oExcel.Application.Sheets.Count To 2 Step -1
                        Try
                            CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Delete()
                            oExcel.Application.DisplayAlerts = False
                        Catch ex As Exception
                        End Try
                        Try
                            oExcel.Sheets(xi).delete()
                            oExcel.Application.DisplayAlerts = True
                        Catch ex As Exception
                        End Try
                    Next
                End If
            Catch ex As Exception
            End Try

            Try
                CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
            Catch ex As Exception
            End Try

            oExcel.DisplayAlerts = False
            '_FileName = "C:\Users\NOH-NB\Desktop\TestFile.xlsx"
            Dim _Paths As String = "C:\ExportDataForm_WISDOM\"

            If Not (Directory.Exists(_Paths)) Then
                My.Computer.FileSystem.CreateDirectory(_Paths)
            End If

            _FileName = _Paths & Me.FTCustomerPO.Text & "_" & Me.FTNikePOLineItem.Text & ".xlsx"

            xlBookTmp.SaveAs(_FileName)
            xlBookTmp.Close()

            Process.Start(_FileName)
        Catch ex As Exception

            HI.MG.ShowMsg.mProcessError(1505029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub
End Class
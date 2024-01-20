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

        Dim DataFrom As String = ""

        cmd = "EXEC [HITECH_SAMPLEROOM].[dbo].[SP_GET_DATAFORPATTERN] "

        If chkSample.Checked And chkProd.Checked Then
            cmd &= "@DataFrom = 'B'"
        Else
            If chkSample.Checked Then
                cmd &= "@DataFrom = 'S'"
            End If
            If chkProd.Checked Then
                cmd &= "@DataFrom = 'P'"
            End If
        End If

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            cmd &= ", @Lang = '" & ST.Lang.eLang.TH & "' "
        Else
            cmd &= ", @Lang = '" & ST.Lang.eLang.EN & "' "
        End If

        cmd &= ", @FNHSysCmpID = '" + (HI.ST.SysInfo.CmpID).ToString + "'"

        If (FTPayYear.Text <> "") Then
            cmd &= ", @YearStart = '" + FTPayYear.Text + "/01/01' , @YearEnd = '" + FTPayYear.Text + "/12/31' "
        End If

        If (FNHSysCustId.Text <> "") Then
            cmd &= ", @FNHSysCustId = '" & Val(FNHSysCustId.Properties.Tag.ToString) & "' "
        Else
            cmd &= ", @FNHSysCustId =  ''"
        End If

        If (FNHSysStyleId.Text <> "") Then
            cmd &= ", @FNHSysStyleId = '" & Val(FNHSysStyleId.Properties.Tag.ToString) & "' "
        Else
            cmd &= ", @FNHSysStyleId = ''"
        End If

        If (FNHSysSeasonId.Text <> "") Then
            cmd &= ", @FNHSysSeasonId = '" & Val(FNHSysSeasonId.Properties.Tag.ToString) & "' "
        Else
            cmd &= ", @FNHSysSeasonId = ''"
        End If

        If (FNHSysMerTeamId.Text <> "") Then
            cmd &= ", @FNHSysMerTeamId = '" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & "' "
        Else
            cmd &= ", @FNHSysMerTeamId = '' "
        End If

        If (FTStartOrderDate.Text <> "" And FTEndOrderDate.Text <> "") Then
            cmd &= ", @OrderDate >= '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text)
            cmd &= "', @OrderDateTo <= '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "' "
        Else
            cmd &= ", @OrderDate = '' , @OrderDateTo = '' "
        End If

        If (FNHSysBuyIdFrom.Text <> "" And FNHSysBuyIdTo.Text <> "") Then
            cmd &= ", @FNHSysBuyIdFrom = '" & FNHSysBuyIdFrom.Text & "', @FNHSysBuyIdTo = '" & FNHSysBuyIdTo.Text & "' "
        Else
            cmd &= ", @FNHSysBuyIdFrom = '' , @FNHSysBuyIdTo ='' "
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
                    cmdstring &= vbCrLf & "    , " & FieldName & "User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    , " & FieldName & "Date=" & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    , " & FieldName & "Time=" & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & "    , FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    , FDUpdDate = " & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    , FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & "    , TPTNOrderBy = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
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
                    cmdstring &= vbCrLf & "    , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    , " & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    , " & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & "    , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "    , " & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & "    , " & HI.UL.ULDate.FormatTimeDB & ")"
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
                    'ElseIf (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide") = 1)) Then
                    '    e.Appearance.ForeColor = System.Drawing.Color.Green
                    'ElseIf (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide") = 0)) Then
                    '    e.Appearance.ForeColor = System.Drawing.Color.Blue
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class
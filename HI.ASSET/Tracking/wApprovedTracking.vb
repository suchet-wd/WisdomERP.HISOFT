Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wApprovedTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _StateQtyBySPM As Boolean = False  ' Get Cut Qty by Supermarket 7 - 11

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()

    End Sub

#Region "Initial Grid"

    Private Sub InitGridClearSort()
        For Each c As GridColumn In ogvpr.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

        For Each c As GridColumn In ogvpo.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

        For Each c As GridColumn In ogvservice.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

    End Sub

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity|Total"

        'Dim sFieldGrpCount As String = "FNQuantity|Total"
        'Dim sFieldGrpSum As String = "FNQuantity|Total"

        'Dim sFieldCustomSum As String = "FFNQuantity|Total"
        'Dim sFieldCustomGrpSum As String = ""

        With ogvpr
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldCustomSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldCustomGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogvpo
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldCustomSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldCustomGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogvservice
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldCustomSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldCustomGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub

    Private Sub InitialGridSummaryMergCell()

        For Each c As GridColumn In ogvpr.Columns

            Select Case c.FieldName.ToString
                Case "FTPRPurchaseNo", "FDPRPurchaseDate", "FTPRPurchaseBy", "FNHSysSuplId", "FTSuplName", "FTProductCode", "FTAssetBrandName", "FTAssetModelName", "FTUnitCode", "FNPrice"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub

    Private Sub InitialGridColorSizeMergCell()

        For Each c As GridColumn In ogvpo.Columns

            Select Case c.FieldName.ToString
                Case "FDPurchaseDate", "FTPurchaseBy", "FNHSysSuplId", "FTSuplName", "FTProductCode", "FTAssetBrandName", "FTAssetModelName", "FTUnitCode", "FNPrice"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub

    Private Sub InitialGridTableColorSizeMergCell()

        For Each c As GridColumn In ogvservice.Columns

            Select Case c.FieldName.ToString
                Case "FDPurchaseDate", "FTPurchaseBy", "FNHSysSuplId", "FTSuplName", "FTProductCode", "FTAssetBrandName", "FTAssetModelName", "FTUnitCode", "FNPrice"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub

#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0
    Private _RowHandleHold As Integer = 0

    Private totalSum2 As Integer = 0
    Private GrpSum2 As Integer = 0
    Private _RowHandleHold2 As Integer = 0

    Private totalSum3 As Integer = 0
    Private GrpSum3 As Integer = 0
    Private _RowHandleHold3 As Integer = 0
    Private _RowHandleHoldChk As Integer = 0

    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
        _RowHandleHoldChk = -1
    End Sub

    Private Sub InitColorSizeStartValue()
        totalSum2 = 0
        GrpSum2 = 0
        _RowHandleHold2 = 0
    End Sub

    Private Sub InitTableColorSizeStartValue()
        totalSum3 = 0
        GrpSum3 = 0
        _RowHandleHold3 = 0
    End Sub

    Private Sub ogvpr_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvpr
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity", "Total"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTPRPurchaseNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTPRPurchaseNo").ToString) Or (e.RowHandle = _RowHandleHold And e.RowHandle <> _RowHandleHoldChk) Then
                                        totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))
                                    End If
                                    _RowHandleHold = e.RowHandle
                                    _RowHandleHoldChk = e.RowHandle
                                End If

                            End If
                            e.TotalValue = totalSum
                        End If
                End Select
            End With

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvpo_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitColorSizeStartValue()
            End If

            With ogvpo
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity", "Total"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold2 Or e.RowHandle = 0 Then
                                    ' If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold2, "FTOrderNo").ToString Or (e.RowHandle = _RowHandleHold And e.RowHandle <> _RowHandleHoldChk) Then
                                    '.GetRowCellValue(e.RowHandle, "FTPOLineItemNo").ToString <> .GetRowCellValue(_RowHandleHold2, "FTPOLineItemNo").ToString Or
                                    '.GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold2, "FTColorway").ToString Or
                                    ' .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold2, "FTSizeBreakDown").ToString) Or e.RowHandle = _RowHandleHold2 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTPRPurchaseNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTPRPurchaseNo").ToString) Or (e.RowHandle = _RowHandleHold And e.RowHandle <> _RowHandleHoldChk) Then
                                        totalSum2 = totalSum2 + Integer.Parse(Val(e.FieldValue.ToString))
                                    End If
                                    ' totalSum2 = totalSum2 + Integer.Parse(Val(e.FieldValue.ToString))
                                    'End If
                                End If
                                _RowHandleHold2 = e.RowHandle
                            End If
                            e.TotalValue = totalSum2
                        End If
                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvTableColorSize_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitTableColorSizeStartValue()
            End If

            With ogvservice
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity", "Total"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold3 Or e.RowHandle = 0 Then
                                    'If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString Or _
                                    '                           .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold3, "FTColorway").ToString Or _
                                    '                            .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold3, "FTSizeBreakDown").ToString) Or e.RowHandle = _RowHandleHold3 Then
                                    '    totalSum3 = totalSum3 + Integer.Parse(Val(e.FieldValue.ToString))
                                    'End If
                                    If (.GetRowCellValue(e.RowHandle, "FTPRPurchaseNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTPRPurchaseNo").ToString) Or (e.RowHandle = _RowHandleHold And e.RowHandle <> _RowHandleHoldChk) Then
                                        totalSum3 = totalSum3 + Integer.Parse(Val(e.FieldValue.ToString))
                                    End If
                                End If
                                _RowHandleHold3 = e.RowHandle
                            End If
                            e.TotalValue = totalSum3
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

        ogcpr.DataSource = Nothing
        ogcpo.DataSource = Nothing

       Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT PR.FTPRPurchaseNo,  Case When isdate(PR.FDPRPurchaseDate ) =1 Then convert(varchar(10),convert(date,PR.FDPRPurchaseDate ),103) Else '' END  as FDPRPurchaseDate ,PR.FTPRPurchaseBy "
        _Qry &= vbCrLf & ",S.FTSuplCode AS FNHSysSuplId,ISNULL(A.FTAssetCode,P.FTAssetPartCode) AS FTAssetCode,ISNULL(A.FTProductCode,P.FTProductCode)AS FTProductCode ,FTPurchaseRefNo "
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ISNULL(A.FTAssetNameTH,P.FTAssetPartNameTH) AS FTAssetName,isnull(B.FTAssetBrandNameTH,'-') as FTAssetBrandName ,ISNULL(M.FTAssetModelNameTH,'-') AS FTAssetModelName,S.FTSuplNameTH AS FTSuplName"
        Else
            _Qry &= vbCrLf & ",ISNULL(A.FTAssetNameEN,P.FTAssetPartNameEN) AS FTAssetName,isnull(B.FTAssetBrandNameEN,'-') as FTAssetBrandName ,ISNULL(M.FTAssetModelNameEN,'-') AS FTAssetModelName,S.FTSuplNameEN AS FTSuplName"
        End If
        _Qry &= vbCrLf & "   ,PRD.FNQuantity,U.FTUnitAssetCode AS FTUnitCode,PRD.FNPrice,(PRD.FNQuantity*PRD.FNPrice) AS Total ,CASE WHEN PR.FTStateSendApp ='1' THEN '1' ELSE '0' END AS FTStateSendApp ,PR.FTSendAppBy"
        _Qry &= vbCrLf & "  , Case When isdate(PR.FTSendAppDate ) =1 Then convert(varchar(10),convert(date,PR.FTSendAppDate ),103) Else '' END  as FTSendAppDate"
        _Qry &= vbCrLf & ",CASE WHEN PR.FTStateApp ='1' THEN '1' ELSE '0' END AS FTStateApp,PR.FTAppName, Case When isdate(PR.FTAppDate ) =1 Then convert(varchar(10),convert(date,PR.FTAppDate ),103) Else '' END  as FTAppDate"
        _Qry &= vbCrLf & ",CASE WHEN PR.FTStateManagerApp ='1' THEN '1' ELSE '0' END AS FTStateManagerApp,PR.FTManagerName, Case When isdate(PR.FTManagerAppDate ) =1 Then convert(varchar(10),convert(date,PR.FTManagerAppDate ),103) Else '' END  as FTManagerAppDate"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS PR WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail AS PRD WITH (NOLOCK) ON PR.FTPRPurchaseNo=PRD.FTPRPurchaseNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON PR.FNHSysSuplId=S.FNHSysSuplId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH (NOLOCK) ON PRD.FNHSysFixedAssetId=A.FNHSysFixedAssetId  AND PRD.FNFixedAssetType=A.FNFixedAssetType LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WItH(NOLOCK) ON  PRD.FNHSysFixedAssetId=P.FNHSysAssetPartId AND PRD.FNFixedAssetType=1  LEFT OUTER jOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WITH(NOLOCK) ON A.FNHSysAssetBrandId=B.FNHSysAssetBrandId OR P.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH (NOLOCK) ON A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON PRD.FNHSysUnitId=U.FNHSysUnitAssetId "
        _Qry &= vbCrLf & " WHERE PR.FTPRPurchaseNo<>''"
        If Me.FTPRPurchaseNo.Text <> "" Then
            _Qry &= vbCrLf & "    AND PR.FTPRPurchaseNo>='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNo.Text) & "' "
        End If
        If Me.FTPRPurchaseNoTo.Text <> "" Then
            _Qry &= vbCrLf & "    AND PR.FTPRPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPRPurchaseNoTo.Text) & "' "
        End If
        If Me.FDPRPurchaseDateStart.Text <> "" Then
            _Qry &= vbCrLf & "    AND PR.FDPRPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDPRPurchaseDateStart.Text) & "' "
        End If
        If Me.FDPRPurchaseDateEnd.Text <> "" Then
            _Qry &= vbCrLf & "    AND PR.FDPRPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDPRPurchaseDateEnd.Text) & "' "
        End If
        If Me.FNHSysSuplId.Text <> "" Then
            _Qry &= vbCrLf & "    AND S.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
        End If


        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FIXED)
        Me.ogcpr.DataSource = _dt


        _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Sub LoaddataDetailPO()
        ogcpr.DataSource = Nothing
        ogcpo.DataSource = Nothing

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT PO.FTPurchaseNo,Case When isdate(PO.FDPurchaseDate ) =1 Then convert(varchar(10),convert(date,PO.FDPurchaseDate ),103) Else '' END  as FDPurchaseDate ,PO.FTPurchaseBy"
        _Qry &= vbCrLf & ",S.FTSuplCode AS FNHSysSuplId,ISNULL(A.FTAssetCode,P.FTAssetPartCode) AS FTAssetCode,ISNULL(A.FTProductCode,P.FTProductCode)AS FTProductCode  "
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ISNULL(A.FTAssetNameTH,P.FTAssetPartNameTH) AS FTAssetName,isnull(B.FTAssetBrandNameTH,'-') as FTAssetBrandName ,ISNULL(M.FTAssetModelNameTH,'-') AS FTAssetModelName,S.FTSuplNameTH AS FTSuplName"
        Else
            _Qry &= vbCrLf & ",ISNULL(A.FTAssetNameEN,P.FTAssetPartNameEN) AS FTAssetName,isnull(B.FTAssetBrandNameEN,'-') as FTAssetBrandName ,ISNULL(M.FTAssetModelNameEN,'-') AS FTAssetModelName,S.FTSuplNameEN AS FTSuplName"
        End If
        _Qry &= vbCrLf & "   ,POD.FNQuantity,U.FTUnitAssetCode,POD.FNPrice,(POD.FNQuantity*POD.FNPrice) AS Total ,CASE WHEN PO.FTStateSendApp ='1' THEN '1' ELSE '0' END AS FTStateSendApp ,PO.FTSendAppBy"
        _Qry &= vbCrLf & "  , Case When isdate(PO.FTSendAppDate ) =1 Then convert(varchar(10),convert(date,PO.FTSendAppDate ),103) Else '' END  as FTSendAppDate"
        _Qry &= vbCrLf & ",CASE WHEN PO.FTStateManagerApp ='1' THEN '1' ELSE '0' END AS FTStateManagerApp,PO.FTManagerName, Case When isdate(PO.FTManagerAppDate ) =1 Then convert(varchar(10),convert(date,PO.FTManagerAppDate ),103) Else '' END  as FTManagerAppDate"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS PO WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS POD WITH (NOLOCK) ON PO.FTPurchaseNo=POD.FTPurchaseNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON PO.FNHSysSuplId=S.FNHSysSuplId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH (NOLOCK) ON POD.FNHSysFixedAssetId=A.FNHSysFixedAssetId  AND POD.FNFixedAssetType=A.FNFixedAssetType LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WItH(NOLOCK) ON  POD.FNHSysFixedAssetId=P.FNHSysAssetPartId AND POD.FNFixedAssetType='1' LEFT OUTER jOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WITH(NOLOCK) ON A.FNHSysAssetBrandId=B.FNHSysAssetBrandId AND P.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH (NOLOCK) ON A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON POD.FNHSysUnitId=U.FNHSysUnitAssetId "
        _Qry &= vbCrLf & " WHERE PO.FTPurchaseNo<>''"
        If Me.FTPurchaseNo.Text <> "" Then
            _Qry &= vbCrLf & "    AND PO.FTPurchaseNo>='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        End If
        If Me.FTPurchaseNoTo.Text <> "" Then
            _Qry &= vbCrLf & "    AND PO.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "' "
        End If
        If Me.FDPurchaseDateStart.Text <> "" Then
            _Qry &= vbCrLf & "    AND PO.FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDPurchaseDateStart.Text) & "' "
        End If
        If Me.FDPurchaseDateEnd.Text <> "" Then
            _Qry &= vbCrLf & "    AND PO.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPurchaseDateEnd.Text) & "' "
        End If
        If Me.FNHSysSuplId.Text <> "" Then
            _Qry &= vbCrLf & "    AND S.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
        End If
      


        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FIXED)
        Me.ogcpo.DataSource = _dt


        _Spls.Close()
        _RowDataChange = False
    End Sub

    Private Sub LoaddataDetailPS()
        ogcpr.DataSource = Nothing
        ogcpo.DataSource = Nothing

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT PS.FTPurchaseNo,Case When isdate(PS.FDPurchaseDate ) =1 Then convert(varchar(10),convert(date,PS.FDPurchaseDate ),103) Else '' END  as FDPurchaseDate ,PS.FTPurchaseBy"
        _Qry &= vbCrLf & ",S.FTSuplCode AS FNHSysSuplId,ISNULL(A.FTAssetCode,P.FTAssetPartCode) AS FTAssetCode,ISNULL(A.FTProductCode,P.FTProductCode)AS FTProductCode  "
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ISNULL(A.FTAssetNameTH,P.FTAssetPartNameTH) AS FTAssetName,isnull(B.FTAssetBrandNameTH,'-') as FTAssetBrandName ,ISNULL(M.FTAssetModelNameTH,'-') AS FTAssetModelName,S.FTSuplNameTH AS FTSuplName"
        Else
            _Qry &= vbCrLf & ",ISNULL(A.FTAssetNameEN,P.FTAssetPartNameEN) AS FTAssetName,isnull(B.FTAssetBrandNameEN,'-') as FTAssetBrandName ,ISNULL(M.FTAssetModelNameEN,'-') AS FTAssetModelName,S.FTSuplNameEN AS FTSuplName"
        End If
        _Qry &= vbCrLf & "   ,PSD.FNQuantity,U.FTUnitAssetCode,PSD.FNPrice,(PSD.FNQuantity*PSD.FNPrice) AS Total ,CASE WHEN PS.FTStateSendApp ='1' THEN '1' ELSE '0' END AS FTStateSendApp ,PS.FTSendAppBy"
        _Qry &= vbCrLf & "    , Case When isdate(PS.FTSendAppDate ) =1 Then convert(varchar(10),convert(date,PS.FTSendAppDate ),103) Else '' END  as FTSendAppDate"
        _Qry &= vbCrLf & ",CASE WHEN PS.FTStateSuperVisorApp ='1' THEN '1' ELSE '0' END AS FTStateSuperVisorApp,PS.FTSuperVisorName, Case When isdate(PS.FTSuperVisorAppDate ) =1 Then convert(varchar(10),convert(date,PS.FTSuperVisorAppDate ),103) Else '' END  as FTSuperVisorAppDate"
        _Qry &= vbCrLf & ",CASE WHEN PS.FTStateManagerApp ='1' THEN '1' ELSE '0' END AS FTStateManagerApp,PS.FTSuperManagerName, Case When isdate(PS.FTSuperManagerAppDate ) =1 Then convert(varchar(10),convert(date,PS.FTSuperManagerAppDate ),103) Else '' END  as FTManagerAppDate"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService AS PS WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail AS PSD WITH (NOLOCK) ON PS.FTPurchaseNo=PSD.FTPurchaseNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON PS.FNHSysSuplId=S.FNHSysSuplId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH (NOLOCK) ON PSD.FNHSysFixedAssetId=A.FNHSysFixedAssetId  AND PO.FNFixedAssetType=A.FNFixedAssetType LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WItH(NOLOCK) ON  PSD.FNHSysFixedAssetId=P.FNHSysAssetPartId  LEFT OUTER jOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WITH(NOLOCK) ON A.FNHSysAssetBrandId=B.FNHSysAssetBrandId OR P.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH (NOLOCK) ON A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON PSD.FNHSysUnitId=U.FNHSysUnitAssetId "
        _Qry &= vbCrLf & " WHERE PS.FTPurchaseNo<>''"
        If Me.FTPurchaseNoService.Text <> "" Then
            _Qry &= vbCrLf & "    AND PS.FTPurchaseNo>='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoService.Text) & "' "
        End If
        If Me.FTPurchaseNoServiceTo.Text <> "" Then
            _Qry &= vbCrLf & "    AND PS.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoServiceTo.Text) & "' "
        End If
        If Me.FDPurchaseDateServiceStart.Text <> "" Then
            _Qry &= vbCrLf & "    AND PS.FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDPurchaseDateServiceStart.Text) & "' "
        End If
        If Me.FDPurchaseDateServiceEnd.Text <> "" Then
            _Qry &= vbCrLf & "    AND PS.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPurchaseDateServiceEnd.Text) & "' "
        End If
        If Me.FNHSysSuplId.Text <> "" Then
            _Qry &= vbCrLf & "    AND S.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
        End If



        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FIXED)
        Me.ogcservice.DataSource = _dt


        _Spls.Close()
        _RowDataChange = False
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        'If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        If Me.FNHSysSuplId.Text <> "" And FNHSysSuplId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTPRPurchaseNo.Text <> "" And FTPRPurchaseNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTPRPurchaseNoTo.Text <> "" And FTPRPurchaseNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNo.Text <> "" And FTPurchaseNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNoTo.Text <> "" And FTPurchaseNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNoService.Text <> "" And FTPurchaseNoService.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNoServiceTo.Text <> "" And FTPurchaseNoServiceTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FDPRPurchaseDateStart.Text <> "" Then
            _Pass = True
        End If

        If Me.FDPRPurchaseDateEnd.Text <> "" Then
            _Pass = True
        End If

        If Me.FDPurchaseDateServiceStart.Text <> "" Then
            _Pass = True
        End If

        If Me.FDPurchaseDateServiceEnd.Text <> "" Then
            _Pass = True
        End If

        If Me.FDPurchaseDateStart.Text <> "" Then
            _Pass = True
        End If

        If Me.FDPurchaseDateEnd.Text <> "" Then
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

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvpr)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvpo)

            Call InitGridClearSort()
            _StateQtyBySPM = StateQtyBySPM()
            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Function StateQtyBySPM() As Boolean
        Try
            Dim _Cmd As String = " SELECT  Top 1  isnull(FTStateProdSMKToCutQty,0) AS FTStateProdSMKToCutQty  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEConfig WITH(NOLOCK)"
            Return Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "0")) = 1
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvpr)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvpo)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvsummary_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs)
        Try
            With Me.ogvpr
                Select Case e.Column.FieldName
                    Case "FTPRPurchaseNo", "FDPRPurchaseDate", "FTSuplName", "FNQuantity", "FTSendAppDate"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTPRPurchaseNo", "FDPRPurchaseDate", "FTSuplName", "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FTSizeBreakDown", "FDShipDate", "FTPORef"

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

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If VerifyData() Then


            'Dim _Qry As String = ""
            '_Qry = "SELECt TOP 1 FTStateProdSMKToCutQty "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig AS S WITH(NOLOCK)"
            '_Qry &= vbCrLf & " WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) & "'"

            '_FTStateProdSMKToCutQty = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")
            If Me.FTPRPurchaseNo.Text <> "" And Me.FTPRPurchaseNoTo.Text <> "" Or Me.FDPRPurchaseDateStart.Text <> "" And Me.FDPRPurchaseDateEnd.Text <> "" Or Me.FNHSysSuplId.Text <> "" Then
                Call LoadData()
            End If

            If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNoTo.Text <> "" Or Me.FDPurchaseDateStart.Text <> "" And Me.FDPurchaseDateEnd.Text <> "" Or Me.FNHSysSuplId.Text <> "" Then
                Call LoaddataDetailPO()
            End If

            If Me.FTPurchaseNoService.Text <> "" And Me.FTPurchaseNoServiceTo.Text <> "" Or Me.FDPurchaseDateServiceStart.Text <> "" And Me.FDPurchaseDateServiceEnd.Text <> "" Or Me.FNHSysSuplId.Text <> "" Then
                ogcpr.DataSource = Nothing
                ogcpo.DataSource = Nothing
                Call LoaddataDetailPS()
            End If




        End If
    End Sub

    Private Sub ogvdetailcolorsize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs)
        Try
            With Me.ogvpo
                Select Case e.Column.FieldName
                    Case "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FTPORef1"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FDShipDate", "FTPORef1", "FTSizeBreakDown"
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

    Private Sub ogvdetailtablecolorsize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs)
        Try
            With Me.ogvservice
                Select Case e.Column.FieldName
                    Case "G3FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FTPORef2"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FTSizeBreakDown", "FDShipDate", "FTUnitCode", "FNTableNo", "FTPORef2"

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

   
End Class
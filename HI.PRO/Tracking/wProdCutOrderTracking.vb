Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wProdCutOrderTracking

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
        For Each c As GridColumn In ogvsummary.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

        For Each c As GridColumn In ogvdetailcolorsize.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

        For Each c As GridColumn In ogvdetailtablecolorsize.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

    End Sub

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantityCut|FNQuantityBundle|FNQuantityBalCut"

        Dim sFieldGrpCount As String = "FNQuantityCut|FNQuantityBundle|FNQuantityBalCut|"
        Dim sFieldGrpSum As String = "FNQuantityBundle|FNQuantityBalCut|"

        Dim sFieldCustomSum As String = "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity"
        Dim sFieldCustomGrpSum As String = ""

        With ogvsummary
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

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

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogvdetailcolorsize
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

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

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With

        With ogvdetailtablecolorsize
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

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

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub

    Private Sub InitialGridSummaryMergCell()

        For Each c As GridColumn In ogvsummary.Columns

            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FTSizeBreakDown", "FDShipDate", "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FTPORef ", "FTPORef"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub

    Private Sub InitialGridColorSizeMergCell()

        For Each c As GridColumn In ogvdetailcolorsize.Columns

            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FTSizeBreakDown", "FDShipDate", "FTPORef1" ', "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub

    Private Sub InitialGridTableColorSizeMergCell()

        For Each c As GridColumn In ogvdetailtablecolorsize.Columns

            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FTSizeBreakDown", "FDShipDate", "FTUnitCode", "FNTableNo", "FTPORef2", "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FTPOLineItemNo"
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

    Private Sub ogvsummary_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvsummary.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvsummary
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString) Or (e.RowHandle = _RowHandleHold And e.RowHandle <> _RowHandleHoldChk) Then
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

    Private Sub ogvColorSize_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvdetailcolorsize.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitColorSizeStartValue()
            End If

            With ogvdetailcolorsize
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold2 Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold2, "FTOrderNo").ToString Or
                                                               .GetRowCellValue(e.RowHandle, "FTPOLineItemNo").ToString <> .GetRowCellValue(_RowHandleHold2, "FTPOLineItemNo").ToString Or
                                                               .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold2, "FTColorway").ToString Or
                                                                .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold2, "FTSizeBreakDown").ToString) Or e.RowHandle = _RowHandleHold2 Then

                                        totalSum2 = totalSum2 + Integer.Parse(Val(e.FieldValue.ToString))
                                    End If
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

    Private Sub ogvTableColorSize_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvdetailtablecolorsize.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitTableColorSizeStartValue()
            End If

            With ogvdetailtablecolorsize
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then

                            If e.IsTotalSummary Then

                                If e.RowHandle <> _RowHandleHold3 Or e.RowHandle = 0 Then

                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTOrderNo").ToString Or
                                                               .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold3, "FTColorway").ToString Or
                                                                .GetRowCellValue(e.RowHandle, "FTPOLineItemNo").ToString <> .GetRowCellValue(_RowHandleHold3, "FTPOLineItemNo").ToString Or
                                                                .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold3, "FTSizeBreakDown").ToString) Or e.RowHandle = _RowHandleHold3 Then

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

        ogcsummary.DataSource = Nothing
        ogcdetailcolorsize.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            _Qry = "  "
            If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then

                _Qry &= vbCrLf & " declare @TabOrder AS Table ( "
                _Qry &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL  UNIQUE  NONCLUSTERED  ([FTOrderNo])  "
                _Qry &= vbCrLf & "     ) "


                If (_FTStateProdSMKToCutQty) Then

                    _Qry &= vbCrLf & " insert into  @TabOrder (FTOrderNo) "
                    _Qry &= vbCrLf & "  SELECT  P.FTOrderNo "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC  WITH(NOLOCK)  "
                    'End If
                    _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTBarcodeNo = BU.FTBarcodeBundleNo INNER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON BU.FTOrderProdNo = P.FTOrderProdNo"
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON TC.FNHSysOperationId = OPR.FNHSysOperationId "
                    _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON TC.FTBarcodeNo=BD.FTBarcodeBundleNo"
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
                    _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS H WITH(NOLOCK) ON TC.FTDocScanNo=H.FTDocScanNo"
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"


                    _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "
                    ' If (_StateQtyBySPM) Then
                    _Qry &= vbCrLf & " and  TC.FNHSysOperationId = 1405310009 "
                    'End If

                    If Me.FTSDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND H.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                    End If
                    If Me.FTEDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND H.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                    End If

                    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo "
                Else
                    _Qry &= vbCrLf & " insert into  @TabOrder (FTOrderNo) "
                    _Qry &= vbCrLf & "  SELECT  P.FTOrderNo "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC  WITH(NOLOCK)  "
                    'End If
                    _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTOrderProdNo = BU.FTOrderProdNo INNER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON BU.FTOrderProdNo = P.FTOrderProdNo"
                    _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON  BU.FTBarcodeBundleNo=BD.FTBarcodeBundleNo "
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON  BD.FTLayCutNo = LC.FTLayCutNo   and TC.FNTableNo=LC.FNTableNo and TC.FNHSysMarkId=LC.FNHSysMarkId"
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"
                    _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "

                    If Me.FTSDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                    End If
                    If Me.FTEDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                    End If

                    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo "
                End If

            End If

            _Qry &= vbCrLf & "  Select  ST.FTStyleCode,A.FTOrderNo"
            '  If (_FTStateProdSMKToCutQty) Then
            '_Qry &= vbCrLf & " ,P.FDInsDate"
            'Else
            '_Qry &= vbCrLf & " ,P.FDGenBarcodeDate"
            'End If
            _Qry &= vbCrLf & " ,A.FTPORef"
            _Qry &= vbCrLf & ",Cmp.FTCmpCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
                _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkNameTH,'') AS FTMarkName "
            Else
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
                _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkNameEN,'') AS FTMarkName "
            End If

            _Qry &= vbCrLf & " ,ISNULL((SELECT  Convert(Datetime,MIN(FDShipDate)) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=A.FTOrderNo),null) AS FDShipDate"

            _Qry &= vbCrLf & " ,ISNULL(B.FNQuantity,0) AS FNQuantity"
            _Qry &= vbCrLf & " ,ISNULL(B.FNQuantityExtra,0) AS FNQuantityExtra"
            _Qry &= vbCrLf & " ,ISNULL(B.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & "  ,ISNULL(B.FNGrandQuantity,0) As FNGrandQuantity"
            _Qry &= vbCrLf & " ,P.FNHSysMarkId"
            _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkCode,'') AS FTMarkCode "
            _Qry &= vbCrLf & " ,ISNULL(P.FNQuantity,0) AS FNQuantityCut"
            _Qry &= vbCrLf & " ,ISNULL(P.FNQuantityBundle,0) AS FNQuantityBundle"
            _Qry &= vbCrLf & " ,(ISNULL(P.FNQuantity,0) -ISNULL(P.FNQuantityBundle,0))  AS FNQuantityBalCut"

            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId=ST.FNHSysStyleId INNER JOIN"
            _Qry &= vbCrLf & "    ("
            _Qry &= vbCrLf & " 	SELECT FTOrderNo, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK)"
            _Qry &= vbCrLf & " 	GROUP BY FTOrderNo"
            _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo  "

            If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then
                _Qry &= vbCrLf & " INNER JOIN  @TabOrder AS PMX ON A.FTOrderNo = PMX.FTOrderNo  "
            End If
            ' _Qry &= vbCrLf & "  LEFT OUTER JOIN"

            '    If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then
            _Qry &= vbCrLf & "  LEFT OUTER JOIN"
            'Else
            '_Qry &= vbCrLf & "  LEFT OUTER JOIN"
            'End If

            _Qry &= vbCrLf & "  (SELECT  FTOrderNo,FNHSysMarkId,SUM(FNQuantity) AS FNQuantity,Sum(FNQuantityBundle) AS FNQuantityBundle" ',FTPOLineItemNo,FTColorway"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (SELECT A.FTOrderNo,A.FTOrderProdNo,A.FNHSysMarkId,A.FNTableNo,A.FTColorway,A.FTSizeBreakDown,A.FNQuantity "
            _Qry &= vbCrLf & "    ,ISNULL(B.FNQuantity,0) AS FNQuantityBundle,B.FTPOLineItemNo"
            _Qry &= vbCrLf & "     FROM"
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "  SELECT P.FTOrderNo,P.FTOrderProdNo, PT.FNHSysMarkId,PT.FNTableNo,PT.FNHSysUnitSectId, PTD.FTColorway, PTD.FTSizeBreakDown, SUM(PTD.FNQuantity) AS FNQuantity,U.FTUnitSectCode"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS PT WITH(NOLOCK)  ON P.FTOrderProdNo = PT.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS PTD WITH(NOLOCK)  ON PT.FTOrderProdNo = PTD.FTOrderProdNo AND PT.FNHSysMarkId = PTD.FNHSysMarkId AND PT.FNTableNo = PTD.FNTableNo"
            _Qry &= vbCrLf & "    LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK)  ON PT.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,P.FTOrderProdNo, PT.FNHSysMarkId,PT.FNTableNo,PT.FNHSysUnitSectId, PTD.FTColorway, PTD.FTSizeBreakDown ,U.FTUnitSectCode"

            '  _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN"
            ' If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then
            _Qry &= vbCrLf & "  ) AS A INNER JOIN"
            'Else
            '_Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN"
            'End If

            _Qry &= vbCrLf & "  ("

            If (_FTStateProdSMKToCutQty) Then

                _Qry &= vbCrLf & "	SELECT LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity ,isnull(B.FTPOLineItemNo,'')As FTPOLineItemNo ,A.FNHSysUnitSectId"
                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
                _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON A.FNHSysOperationId = OPR.FNHSysOperationId "
                _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON A.FTBarcodeNo=BD.FTBarcodeBundleNo"
                _Qry &= vbCrLf & "	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
                _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS H WITH(NOLOCK) ON A.FTDocScanNo=H.FTDocScanNo"
                _Qry &= vbCrLf & "    WHERE (OPR.FTStateSPMK = '1')"

                If Me.FTSDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  H.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                End If

                If Me.FTEDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  H.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                End If

                _Qry &= vbCrLf & "   GROUP BY LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown ,B.FTPOLineItemNo,A.FNHSysUnitSectId"

            Else
                _Qry &= vbCrLf & "  SELECT  LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity"
                '   If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & " , TC.FNHSysUnitSectId,isnull(BU.FTPOLineItemNo,'')As FTPOLineItemNo"
                'End If

                '    If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC  WITH(NOLOCK)  "
                'End If
                _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTOrderProdNo = BU.FTOrderProdNo"
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON  BU.FTBarcodeBundleNo=BD.FTBarcodeBundleNo "
                _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON  BD.FTLayCutNo = LC.FTLayCutNo   and TC.FNTableNo=LC.FNTableNo and TC.FNHSysMarkId=LC.FNHSysMarkId"
                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"
                _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "
                If Me.FTSDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                End If
                If Me.FTEDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                End If


                _Qry &= vbCrLf & "  GROUP BY LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown ,BU.FTPOLineItemNo, TC.FNHSysUnitSectId"
            End If


            _Qry &= vbCrLf & "  ) B ON "
            If (_FTStateProdSMKToCutQty) Then
                _Qry &= vbCrLf & " A.FTOrderProdNo = B.FTOrderProdNo"
                _Qry &= vbCrLf & "    AND A.FNHSysMarkId = B.FNHSysMarkId  "
                _Qry &= vbCrLf & "      AND A.FNTableNo = B.FNTableNo  AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown "
            Else
                _Qry &= vbCrLf & " A.FTOrderProdNo = B.FTOrderProdNo"
                _Qry &= vbCrLf & "    AND A.FNHSysMarkId = B.FNHSysMarkId  "
                _Qry &= vbCrLf & "  and A.FNHSysUnitSectId=B.FNHSysUnitSectId"
                _Qry &= vbCrLf & "      AND A.FNTableNo = B.FNTableNo  AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown "
            End If
            _Qry &= vbCrLf & "  ) AS P"
            _Qry &= vbCrLf & "  GROUP BY FTOrderNo,FNHSysMarkId" ' ,FTColorway,FTPOLineItemNo "
            ' If (_FTStateProdSMKToCutQty) Then
            '_Qry &= vbCrLf & " ,P.FTDocScanNo"
            'Else
            '_Qry &= vbCrLf & " ,P.FDGenBarcodeDate"
            'End If
            _Qry &= vbCrLf & ") AS P ON A.FTOrderNo = P.FTOrderNo  "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS PMM WITH(NOLOCK) ON P.FNHSysMarkId = PMM.FNHSysMarkId"

            _Qry &= vbCrLf & " WHERE A.FTOrderNo <> ''"

            _Qry &= vbCrLf & "  AND  A.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If FTStartShipment.Text <> "" Or FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FTOrderNo In ( "
                _Qry &= vbCrLf & " SELECT DISTINCT  FTOrderNo "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTOrderNo <>'' "

                If FTStartShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                End If

                If FTEndShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                End If

                _Qry &= vbCrLf & ") "

            End If


            _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo,ISNULL(PMM.FTMarkCode,'')  "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcsummary.DataSource = _dt
            Call InitialGridSummaryMergCell()

            Call LoaddataDetailColorSize()
            Call LoaddataDetailColorSizeByTable()
        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Sub LoaddataDetailColorSize()
        Dim _Qry As String
        Dim _dt As DataTable
        Dim _PO As String = HI.Conn.SQLConn.GetField("select TOP 1 isnull(B.FTPOLineItemNo,'') as FTPOLineItemNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS P ON B.FTOrderProdNo=P.FTOrderProdNo and B.FTSizeBreakDown=P.FTSizeBreakDown and B.FTColorway=P.FTColorway where P.FTOrderNo='" & Me.FTOrderNo.Text & "' order by P.FTSizeBreakDown,B.FTPOLineItemNo", Conn.DB.DataBaseName.DB_SYSTEM, "")
        Dim _NPO As String = HI.Conn.SQLConn.GetField("select TOP 1 isnull(O.FTNikePOLineItem,'') as FTPOLineItemNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS O INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS P ON O.FTOrderNo=P.FTOrderNo and O.FTColorway=P.FTColorway INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B  ON  P.FTOrderProdNo=B.FTOrderProdNo and P.FTSizeBreakDown=B.FTSizeBreakDown and P.FTColorway=B.FTColorway and  O.FTNikePOLineItem=B.FTPOLineItemNo where O.FTOrderNo='" & Me.FTOrderNo.Text & "' order by O.FTSizeBreakDown,O.FTNikePOLineItem", Conn.DB.DataBaseName.DB_SYSTEM, "")
        Dim _CPO As String = HI.Conn.SQLConn.GetField("select TOP 1 isnull(O.FTNikePOLineItem,'') as FTPOLineItemNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS O where O.FTOrderNo='" & Me.FTOrderNo.Text & "' order by O.FTSizeBreakDown,O.FTNikePOLineItem", Conn.DB.DataBaseName.DB_SYSTEM, "")
        ogcdetailcolorsize.DataSource = Nothing
        Try


            _Qry = "  "
            If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then

                _Qry &= vbCrLf & " declare @TabOrder AS Table ( "
                _Qry &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL  UNIQUE  NONCLUSTERED  ([FTOrderNo])  "
                _Qry &= vbCrLf & "     ) "


                If (_FTStateProdSMKToCutQty) Then

                    _Qry &= vbCrLf & " insert into  @TabOrder (FTOrderNo) "
                    _Qry &= vbCrLf & "  SELECT  P.FTOrderNo "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC  WITH(NOLOCK)  "
                    'End If
                    _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTBarcodeNo = BU.FTBarcodeBundleNo INNER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON BU.FTOrderProdNo = P.FTOrderProdNo"
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON TC.FNHSysOperationId = OPR.FNHSysOperationId "
                    _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON TC.FTBarcodeNo=BD.FTBarcodeBundleNo"
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
                    _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS H WITH(NOLOCK) ON TC.FTDocScanNo=H.FTDocScanNo"
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"


                    _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "
                    _Qry &= vbCrLf & " and  TC.FNHSysOperationId = 1405310009 "
                    'End If

                    If Me.FTSDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND H.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                    End If
                    If Me.FTEDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND H.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                    End If
                    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo "
                Else
                    _Qry &= vbCrLf & " insert into  @TabOrder (FTOrderNo) "
                    _Qry &= vbCrLf & "  SELECT  P.FTOrderNo "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC  WITH(NOLOCK)  "
                    'End If
                    _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTOrderProdNo = BU.FTOrderProdNo INNER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON BU.FTOrderProdNo = P.FTOrderProdNo"
                    _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON  BU.FTBarcodeBundleNo=BD.FTBarcodeBundleNo "
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON  BD.FTLayCutNo = LC.FTLayCutNo   and TC.FNTableNo=LC.FNTableNo and TC.FNHSysMarkId=LC.FNHSysMarkId"
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"
                    _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "

                    If Me.FTSDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                    End If
                    If Me.FTEDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                    End If

                    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo "
                End If

            End If

            _Qry &= vbCrLf & " declare @TabOrder1 AS Table (  "
            _Qry &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL  , "
            _Qry &= vbCrLf & "   FTPORef  [nvarchar](30)   NOT NULL  , "
            _Qry &= vbCrLf & "   FNHSysStyleId  int   NOT NULL , "
            _Qry &= vbCrLf & "   FNHSysCmpId int  NOT NULL "
            _Qry &= vbCrLf & "  UNIQUE  NONCLUSTERED  ([FTOrderNo] ,FTPORef ,FNHSysStyleId  ,FNHSysCmpId ))  "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "insert @TabOrder1 "
            _Qry &= vbCrLf & "select  a.FTOrderNo , a.FTPORef , a.FNHSysStyleId , a.FNHSysCmpId "

            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "   @TabOrder AS PMX ON A.FTOrderNo = PMX.FTOrderNO"
            _Qry &= vbCrLf & " WHERE A.FTOrderNo <> ''"
            _Qry &= vbCrLf & "  AND  A.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""
            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            End If
            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If FTStartShipment.Text <> "" Or FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FTOrderNo In ( "
                _Qry &= vbCrLf & " SELECT DISTINCT  FTOrderNo "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTOrderNo <>'' "

                If FTStartShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                End If

                If FTEndShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                End If

                _Qry &= vbCrLf & ") "

            End If


            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "    "
            _Qry &= vbCrLf & " 	SELECT isnull(FTNikePOLineItem,'')As FTNikePOLineItem,sbd.FTOrderNo,FTColorway,FTSizeBreakDown, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "  into #TmpTMERTOrderSub_BreakDown  "
            _Qry &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK)"
            _Qry &= vbCrLf & " inner join @TabOrder1 o on sbd.FTOrderNo = o.FTOrderNo   "
            _Qry &= vbCrLf & " 	GROUP BY FTNikePOLineItem,sbd.FTOrderNo,FTColorway,FTSizeBreakDown" ',FTSubOrderNo"
            _Qry &= vbCrLf & "      "






            _Qry &= vbCrLf & "   SELECT FTOrderNo,FTColorway,FTSizeBreakDown,FNHSysMarkId,SUM(FNQuantity) AS FNQuantity,Sum(FNQuantityBundle) AS FNQuantityBundle ,FTPOLineItemNo"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " into #TmpTPRODBarcodeScan "
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (SELECT A.FTOrderNo,A.FTOrderProdNo,A.FNHSysMarkId,A.FNTableNo,A.FTColorway,A.FTSizeBreakDown,A.FNQuantity"
            _Qry &= vbCrLf & "    ,ISNULL(B.FNQuantity,0) AS FNQuantityBundle, B.FTPOLineItemNo"
            _Qry &= vbCrLf & "     FROM"
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "  SELECT P.FTOrderNo,P.FTOrderProdNo, PT.FNHSysMarkId,PT.FNTableNo,PT.FNHSysUnitSectId, PTD.FTColorway, PTD.FTSizeBreakDown, SUM(PTD.FNQuantity) AS FNQuantity,U.FTUnitSectCode"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS PT WITH(NOLOCK)  ON P.FTOrderProdNo = PT.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS PTD WITH(NOLOCK)  ON PT.FTOrderProdNo = PTD.FTOrderProdNo AND PT.FNHSysMarkId = PTD.FNHSysMarkId AND PT.FNTableNo = PTD.FNTableNo"
            _Qry &= vbCrLf & "    LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK)  ON PT.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,P.FTOrderProdNo, PT.FNHSysMarkId,PT.FNTableNo,PT.FNHSysUnitSectId, PTD.FTColorway, PTD.FTSizeBreakDown ,U.FTUnitSectCode"

            _Qry &= vbCrLf & "  ) AS A INNER JOIN"


            _Qry &= vbCrLf & "  ("
            If (_FTStateProdSMKToCutQty) Then
                _Qry &= vbCrLf & "	SELECT  LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown,isnull(B.FTPOLineItemNo,'')As FTPOLineItemNo, sum(BD.FNQuantity) AS FNQuantity ,A.FNHSysUnitSectId" ',isnull(B.FTPOLineItemNo,'')As FTPOLineItemNo " ',PD.FTSubOrderNo"
                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo "
                '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SB WITH(NOLOCK) on P.FTOrderNo  =SB.FTOrderNo "
                _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON A.FNHSysOperationId = OPR.FNHSysOperationId "
                _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON A.FTBarcodeNo=BD.FTBarcodeBundleNo"
                ' _Qry &= vbCrLf & "     INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS PD WITH(NOLOCK) ON P.FTOrderProdNo =PD.FTOrderProdNo and BD.FTSizeBreakDown=PD.FTSizeBreakDown and BD.FTColorway=PD.FTColorway"
                _Qry &= vbCrLf & "	  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
                _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS H WITH(NOLOCK) ON A.FTDocScanNo=H.FTDocScanNo"
                _Qry &= vbCrLf & "    WHERE (OPR.FTStateSPMK = '1')"


                If Me.FTSDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  H.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                End If

                If Me.FTEDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  H.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                End If

                _Qry &= vbCrLf & "   GROUP BY  LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown,B.FTPOLineItemNo,A.FNHSysUnitSectId" ',A.FTDocScanNo" 'PD.FTSubOrderNo "

            Else


                _Qry &= vbCrLf & "  SELECT LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown,sum (BD.FNQuantity) AS FNQuantity,isnull(BU.FTPOLineItemNo,'')As FTPOLineItemNo" ',PD.FTSubOrderNo"
                _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  INNER JOIN"
                _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
                _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU  WITH(NOLOCK)  ON BD.FTBarcodeBundleNo=BU.FTBarcodeBundleNo"
                ' _Qry &= vbCrLf & "     INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS PD WITH(NOLOCK) ON BU.FTOrderProdNo =PD.FTOrderProdNo and BD.FTSizeBreakDown=PD.FTSizeBreakDown and BD.FTColorway=PD.FTColorway"
                _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "

                If Me.FTSDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                End If

                If Me.FTEDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                End If

                _Qry &= vbCrLf & "  GROUP BY LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown,BU.FTPOLineItemNo" ',PD.FTSubOrderNo"

            End If

            _Qry &= vbCrLf & "  ) B ON"
            _Qry &= vbCrLf & " A.FTOrderProdNo = B.FTOrderProdNo"
            _Qry &= vbCrLf & "    AND A.FNHSysMarkId = B.FNHSysMarkId  "
            _Qry &= vbCrLf & "      AND A.FNTableNo = B.FNTableNo  AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown "
            _Qry &= vbCrLf & " inner join @TabOrder1 o on a.FTOrderNo = o.FTOrderNo   "
            _Qry &= vbCrLf & ") AS P"
            _Qry &= vbCrLf & "  GROUP BY FTOrderNo,FNHSysMarkId,FTColorway,FTSizeBreakDown , FTPOLineItemNo "





            _Qry &= vbCrLf & " Select  ST.FTStyleCode,A.FTOrderNo"
            _Qry &= vbCrLf & " ,A.FTPORef as FTPORef1"
            _Qry &= vbCrLf & ",Cmp.FTCmpCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
                _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkNameTH,'') AS FTMarkName "
            Else
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
                _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkNameEN,'') AS FTMarkName "
            End If

            _Qry &= vbCrLf & " ,ISNULL((SELECT  Convert(Datetime,MIN(FDShipDate)) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=A.FTOrderNo),null) AS FDShipDate"
            _Qry &= vbCrLf & " ,B.FTColorway,B.FTSizeBreakDown,B.FTNikePOLineItem as FTPOLineItemNo"
            _Qry &= vbCrLf & " ,ISNULL(B.FNQuantity,0) AS FNQuantity"
            _Qry &= vbCrLf & " ,ISNULL(B.FNQuantityExtra,0) AS FNQuantityExtra"
            _Qry &= vbCrLf & " ,ISNULL(B.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & "  ,ISNULL(B.FNGrandQuantity,0) As FNGrandQuantity"
            _Qry &= vbCrLf & " ,P.FNHSysMarkId"

            _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkCode,'') AS FTMarkCode "
            _Qry &= vbCrLf & " ,ISNULL(P.FNQuantity,0) AS FNQuantityCut"
            _Qry &= vbCrLf & " ,ISNULL(P.FNQuantityBundle,0) AS FNQuantityBundle"
            _Qry &= vbCrLf & " ,(ISNULL(P.FNQuantity,0) -ISNULL(P.FNQuantityBundle,0))  AS FNQuantityBalCut"

            _Qry &= vbCrLf & " FROM   @TabOrder1 AS A INNER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId=ST.FNHSysStyleId INNER JOIN"

            _Qry &= vbCrLf & "     #TmpTMERTOrderSub_BreakDown   B ON A.FTOrderNo = B.FTOrderNo  "

            If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then
                _Qry &= vbCrLf & " INNER JOIN  @TabOrder AS PMX ON A.FTOrderNo = PMX.FTOrderNo  "
            End If


            _Qry &= vbCrLf & "  LEFT OUTER JOIN    #TmpTPRODBarcodeScan   AS P ON A.FTOrderNo = P.FTOrderNo "
            _Qry &= vbCrLf & "  AND B.FTColorway = P.FTColorway AND B.FTSizeBreakDown=P.FTSizeBreakDown "
            If _CPO = "" Then
                If _PO = _NPO Then
                    _Qry &= vbCrLf & " AND B.FTNikePOLineItem = P.FTPOLineItemNo"

                End If
            Else
                If _PO = _NPO Then
                    _Qry &= vbCrLf & " AND B.FTNikePOLineItem = P.FTPOLineItemNo"

                End If
            End If
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS PMM WITH(NOLOCK) ON P.FNHSysMarkId = PMM.FNHSysMarkId"
            _Qry &= vbCrLf & "	   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS WITH(NOLOCK) ON B.FTSizeBreakDown = MS.FTMatSizeCode "




            _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo  "
            _Qry &= vbCrLf & " ,B.FTColorway"
            _Qry &= vbCrLf & " ,MS.FNMatSizeSeq,ISNULL(PMM.FTMarkCode,'')"

            _Qry &= vbCrLf & " drop table  #TmpTMERTOrderSub_BreakDown "
            _Qry &= vbCrLf & " drop table  #TmpTPRODBarcodeScan "


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcdetailcolorsize.DataSource = _dt
            Call InitialGridColorSizeMergCell()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoaddataDetailColorSizeByTable()
        Dim _Qry As String
        Dim _dt As DataTable
        ogcdetailtablecolorsize.DataSource = Nothing
        Try

            _Qry = "  "
            If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then

                _Qry &= vbCrLf & " declare @TabOrder AS Table ( "
                _Qry &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL  UNIQUE  NONCLUSTERED  ([FTOrderNo])  "
                _Qry &= vbCrLf & "     ) "


                If (_FTStateProdSMKToCutQty) Then

                    _Qry &= vbCrLf & " insert into  @TabOrder (FTOrderNo) "
                    _Qry &= vbCrLf & "  SELECT  P.FTOrderNo "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC  WITH(NOLOCK)  "
                    'End If
                    _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTBarcodeNo = BU.FTBarcodeBundleNo INNER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON BU.FTOrderProdNo = P.FTOrderProdNo"
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON TC.FNHSysOperationId = OPR.FNHSysOperationId "
                    _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON TC.FTBarcodeNo=BD.FTBarcodeBundleNo"
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
                    _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS H WITH(NOLOCK) ON TC.FTDocScanNo=H.FTDocScanNo"
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"


                    _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "
                    ' If (_StateQtyBySPM) Then
                    _Qry &= vbCrLf & " and  TC.FNHSysOperationId = 1405310009 "
                    'End If

                    If Me.FTSDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND H.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                    End If
                    If Me.FTEDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND H.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                    End If

                    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo "
                Else
                    _Qry &= vbCrLf & " insert into  @TabOrder (FTOrderNo) "
                    _Qry &= vbCrLf & "  SELECT  P.FTOrderNo "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC  WITH(NOLOCK)  "
                    'End If
                    _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTOrderProdNo = BU.FTOrderProdNo INNER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON BU.FTOrderProdNo = P.FTOrderProdNo"
                    _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON  BU.FTBarcodeBundleNo=BD.FTBarcodeBundleNo "
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON  BD.FTLayCutNo = LC.FTLayCutNo   and TC.FNTableNo=LC.FNTableNo and TC.FNHSysMarkId=LC.FNHSysMarkId"
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"
                    _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "

                    If Me.FTSDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                    End If
                    If Me.FTEDateCut.Text <> "" Then
                        _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                    End If

                    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo "
                End If

            End If




            _Qry &= vbCrLf & " declare @TabOrder1 AS Table (  "
            _Qry &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL  , "
            _Qry &= vbCrLf & "   FTPORef  [nvarchar](30)   NOT NULL  , "
            _Qry &= vbCrLf & "   FNHSysStyleId  int   NOT NULL , "
            _Qry &= vbCrLf & "   FNHSysCmpId int  NOT NULL "
            _Qry &= vbCrLf & "  UNIQUE  NONCLUSTERED  ([FTOrderNo] ,FTPORef ,FNHSysStyleId  ,FNHSysCmpId ))  "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "insert @TabOrder1 "
            _Qry &= vbCrLf & "select  a.FTOrderNo , a.FTPORef , a.FNHSysStyleId , a.FNHSysCmpId "

            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "   @TabOrder AS PMX ON A.FTOrderNo = PMX.FTOrderNO"
            _Qry &= vbCrLf & " WHERE A.FTOrderNo <> ''"
            _Qry &= vbCrLf & "  AND  A.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""
            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            End If
            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If FTStartShipment.Text <> "" Or FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FTOrderNo In ( "
                _Qry &= vbCrLf & " SELECT DISTINCT  FTOrderNo "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTOrderNo <>'' "

                If FTStartShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                End If

                If FTEndShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                End If

                _Qry &= vbCrLf & ") "

            End If



            _Qry &= vbCrLf & " 	SELECT MAX(isnull(FTNikePOLineItem,'')) As FTNikePOLineItem,sbd.FTOrderNo,FTColorway,FTSizeBreakDown, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "  into #TmpTMERTOrderSub_BreakDown  "
            _Qry &= vbCrLf & "     FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK)"
            _Qry &= vbCrLf & " inner join @TabOrder1 o on sbd.FTOrderNo = o.FTOrderNo   "
            _Qry &= vbCrLf & " 	GROUP BY sbd.FTOrderNo,FTColorway,FTSizeBreakDown,FTNikePOLineItem"





            _Qry &= vbCrLf & "   SELECT FTOrderNo,FTColorway,FTSizeBreakDown,FNHSysMarkId,FNTableNo,FNHSysUnitSectId,SUM(FNQuantity) AS FNQuantity,Sum(FNQuantityBundle) AS FNQuantityBundle ,FTPOLineItemNo"
            _Qry &= vbCrLf & " into #TmpTPRODBarcodeScan "

            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (SELECT A.FTOrderNo,A.FTOrderProdNo,A.FNHSysMarkId,A.FNTableNo,A.FTColorway,A.FTSizeBreakDown,A.FNQuantity"
            _Qry &= vbCrLf & "    ,ISNULL(B.FNQuantity,0) AS FNQuantityBundle , B.FTPOLineItemNo "

            If Not (_StateQtyBySPM) Then
                _Qry &= vbCrLf & " ,A.FNHSysUnitSectId"
            Else
                _Qry &= vbCrLf & " ,B.FNHSysUnitSectId"
            End If
            _Qry &= vbCrLf & "     FROM"
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "  SELECT P.FTOrderNo,P.FTOrderProdNo, PT.FNHSysMarkId,PT.FNTableNo,PT.FNHSysUnitSectId, PTD.FTColorway, PTD.FTSizeBreakDown, SUM(PTD.FNQuantity) AS FNQuantity,U.FTUnitSectCode"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS PT WITH(NOLOCK)  ON P.FTOrderProdNo = PT.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS PTD WITH(NOLOCK)  ON PT.FTOrderProdNo = PTD.FTOrderProdNo AND PT.FNHSysMarkId = PTD.FNHSysMarkId AND PT.FNTableNo = PTD.FNTableNo"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON PT.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo,P.FTOrderProdNo, PT.FNHSysMarkId,PT.FNTableNo,PT.FNHSysUnitSectId, PTD.FTColorway, PTD.FTSizeBreakDown ,U.FTUnitSectCode"

            _Qry &= vbCrLf & "  ) AS A INNER JOIN"


            _Qry &= vbCrLf & "  ("
            If (_FTStateProdSMKToCutQty) Then
                _Qry &= vbCrLf & "  SELECT LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity,U.FTUnitSectCode,isnull(BU.FTPOLineItemNo,'')As FTPOLineItemNo "
                '  If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & " , TC.FNHSysUnitSectId"
                'End If

                '   If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC  WITH(NOLOCK)  "
                'End If
                _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTBarcodeNo = BU.FTBarcodeBundleNo INNER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON BU.FTOrderProdNo = P.FTOrderProdNo"
                _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON TC.FNHSysOperationId = OPR.FNHSysOperationId "
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON TC.FTBarcodeNo=BD.FTBarcodeBundleNo"
                _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
                _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS H WITH(NOLOCK) ON TC.FTDocScanNo=H.FTDocScanNo"
                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"


                _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "
                ' If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & " and  TC.FNHSysOperationId = 1405310009 "
                'End If

                If Me.FTSDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND H.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                End If
                If Me.FTEDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND H.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                End If
                _Qry &= vbCrLf & "  GROUP BY LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown ,BU.FTPOLineItemNo,U.FTUnitSectCode, TC.FNHSysUnitSectId "
            Else
                _Qry &= vbCrLf & "  SELECT LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity,isnull(BU.FTPOLineItemNo,'')As FTPOLineItemNo"
                'If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & " , TC.FNHSysUnitSectId"

                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC  WITH(NOLOCK)  "
                'End If
                _Qry &= vbCrLf & "   INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BU WITH(NOLOCK) ON TC.FTOrderProdNo = BU.FTOrderProdNo"
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD  WITH(NOLOCK)  ON  BU.FTBarcodeBundleNo=BD.FTBarcodeBundleNo "
                _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON  BD.FTLayCutNo = LC.FTLayCutNo   and TC.FNTableNo=LC.FNTableNo and TC.FNHSysMarkId=LC.FNHSysMarkId"
                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON TC.FNHSysUnitSectId = U.FNHSysUnitSectId"
                _Qry &= vbCrLf & "    WHERE  BD.FTBarcodeBundleNo <> '' "
                If Me.FTSDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDateCut.Text) & "' "
                End If
                If Me.FTEDateCut.Text <> "" Then
                    _Qry &= vbCrLf & "    AND  BU.FDGenBarcodeDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDateCut.Text) & "' "
                End If
                _Qry &= vbCrLf & "  GROUP BY LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown ,BU.FTPOLineItemNo , TC.FNHSysUnitSectId"
            End If


            _Qry &= vbCrLf & "  ) B ON "
            If (_FTStateProdSMKToCutQty) Then
                _Qry &= vbCrLf & "A.FTOrderProdNo = B.FTOrderProdNo"
                _Qry &= vbCrLf & "    AND A.FNHSysMarkId = B.FNHSysMarkId"
                _Qry &= vbCrLf & "    AND A.FNTableNo = B.FNTableNo  AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown "
            Else
                _Qry &= vbCrLf & " A.FTOrderProdNo = B.FTOrderProdNo"
                _Qry &= vbCrLf & "    AND A.FNHSysMarkId = B.FNHSysMarkId "
                _Qry &= vbCrLf & "and A.FNHSysUnitSectId=B.FNHSysUnitSectId"
                _Qry &= vbCrLf & "    AND A.FNTableNo = B.FNTableNo  AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown "
            End If
            _Qry &= vbCrLf & ") AS P"
            _Qry &= vbCrLf & "  GROUP BY FTOrderNo,FNHSysMarkId,FNTableNo,FNHSysUnitSectId,FTColorway,FTSizeBreakDown,FTPOLineItemNo"




            _Qry &= vbCrLf & " Select  ST.FTStyleCode,A.FTOrderNo"

            _Qry &= vbCrLf & " ,A.FTPORef as FTPORef2"
            _Qry &= vbCrLf & ",Cmp.FTCmpCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
                _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkNameTH,'') AS FTMarkName "
            Else
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
                _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkNameEN,'') AS FTMarkName "
            End If

            _Qry &= vbCrLf & " ,ISNULL((SELECT  Convert(Datetime,MIN(FDShipDate)) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=A.FTOrderNo),null) AS FDShipDate"
            _Qry &= vbCrLf & " ,B.FTColorway,B.FTSizeBreakDown,B.FTNikePOLineItem as FTPOLineItemNo"
            _Qry &= vbCrLf & " ,ISNULL(B.FNQuantity,0) AS FNQuantity"
            _Qry &= vbCrLf & " ,ISNULL(B.FNQuantityExtra,0) AS FNQuantityExtra"
            _Qry &= vbCrLf & " ,ISNULL(B.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & "  ,ISNULL(B.FNGrandQuantity,0) As FNGrandQuantity"
            _Qry &= vbCrLf & " ,P.FNHSysMarkId"
            ' If (_FTStateProdSMKToCutQty) Then
            '_Qry &= vbCrLf & " ,P.FTDocScanNo"
            'Else
            '_Qry &= vbCrLf & " ,P.FDGenBarcodeDate"
            'End If
            _Qry &= vbCrLf & " ,ISNULL(PMM.FTMarkCode,'') AS FTMarkCode "
            _Qry &= vbCrLf & " ,P.FNTableNo"
            _Qry &= vbCrLf & " ,ISNULL(U.FTUnitSectCode,'') AS FTUnitCode"
            _Qry &= vbCrLf & " ,ISNULL(P.FNQuantity,0) AS FNQuantityCut"
            _Qry &= vbCrLf & " ,ISNULL(P.FNQuantityBundle,0) AS FNQuantityBundle"
            _Qry &= vbCrLf & " ,(ISNULL(P.FNQuantity,0) -ISNULL(P.FNQuantityBundle,0))  AS FNQuantityBalCut"
            _Qry &= vbCrLf & " FROM      @TabOrder1 AS A    INNER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId=ST.FNHSysStyleId INNER JOIN"
            _Qry &= vbCrLf & "     #TmpTMERTOrderSub_BreakDown    "

            _Qry &= vbCrLf & "     AS B ON A.FTOrderNo = B.FTOrderNo  "

            If Me.FTSDateCut.Text <> "" Or Me.FTEDateCut.Text <> "" Then
                _Qry &= vbCrLf & " INNER JOIN  @TabOrder AS PMX ON A.FTOrderNo = PMX.FTOrderNo  "
            End If

            _Qry &= vbCrLf & "  LEFT OUTER JOIN  #TmpTPRODBarcodeScan   AS P ON A.FTOrderNo = P.FTOrderNo  "
            _Qry &= vbCrLf & "  AND B.FTColorway = P.FTColorway AND B.FTSizeBreakDown=P.FTSizeBreakDown AND  B.FTNikePOLineItem=P.FTPOLineItemNo "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS PMM WITH(NOLOCK) ON P.FNHSysMarkId = PMM.FNHSysMarkId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON P.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Qry &= vbCrLf & "	   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS WITH(NOLOCK) ON B.FTSizeBreakDown = MS.FTMatSizeCode "


            _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo  "
            _Qry &= vbCrLf & " ,B.FTColorway,B.FTNikePOLineItem"
            _Qry &= vbCrLf & " ,MS.FNMatSizeSeq,ISNULL(PMM.FTMarkCode,''),P.FNTableNo"

            _Qry &= vbCrLf & " drop table  #TmpTMERTOrderSub_BreakDown "
            _Qry &= vbCrLf & " drop table  #TmpTPRODBarcodeScan "
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcdetailtablecolorsize.DataSource = _dt
            Call InitialGridTableColorSizeMergCell()
        Catch ex As Exception
        End Try

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTStartShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTSDateCut.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEDateCut.Text <> "" Then
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

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvsummary)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetailcolorsize)

            Call InitGridClearSort()
            _StateQtyBySPM = StateQtyBySPM()
            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Function StateQtyBySPM() As Boolean
          Try
            Dim _Cmd As String = " SELECT  Top 1  isnull(FTStateProdSMKToCutQty,0) AS FTStateProdSMKToCutQty  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEConfig WITH(NOLOCK) WHERE FTCmpCode ='" & HI.ST.SysInfo.CmpCode & "'"
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

        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvsummary)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetailcolorsize)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvsummary_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvsummary.CellMerge
        Try
            With Me.ogvsummary
                Select Case e.Column.FieldName
                    Case "FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FTPORef"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FTSizeBreakDown", "FDShipDate", "FTPORef"

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


            Dim _Qry As String = ""
            _Qry = "SELECt TOP 1 FTStateProdSMKToCutQty "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig AS S WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) & "'"

            _FTStateProdSMKToCutQty = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

            Call LoadData()

        End If
    End Sub

    Private Sub ogvdetailcolorsize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvdetailcolorsize.CellMerge
        Try
            With Me.ogvdetailcolorsize
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

    Private Sub ogvdetailtablecolorsize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvdetailtablecolorsize.CellMerge
        Try
            With Me.ogvdetailtablecolorsize
                Select Case e.Column.FieldName
                    Case "G3FNQuantity", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FTPORef2", "FNQuantity"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                            And ("" & .GetRowCellValue(e.RowHandle1, "FTPOLineItemNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPOLineItemNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTPOLineItemNo"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
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
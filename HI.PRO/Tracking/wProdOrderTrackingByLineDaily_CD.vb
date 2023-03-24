Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing

Public Class wProdOrderTrackingByLineDaily_CD

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _StateQtyBySPM As Boolean = False  ' get Qty by Super market

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

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldCustomSum As String = "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity|FNCutQuantity|FNSewQuantity|FNPackQuantity|FNSendSuplQuantity|FNRcvSuplQuantity|FNSPMKQuantity|FNSewOutQuantity|FNBalCutQuantity|FNBalSuplQuantity|FNBalSewQuantity|FNBalPackQuantity|FNCutBalQuantity"
        sFieldCustomSum &= "|FNQtyEmbroidery|FNRcvQtyEmbroidery|FNBalQtyEmbroidery|FNQtyPrint|FNRcvQtyPrint|FNBalQtyPrint|FNQtyHeat|FNRcvQtyHeat|FNBalQtyHeat|FNQtyLaser|FNRcvQtyLaser|FNBalQtyLaser|FNQtyPadPrint|FNRcvQtyPadPrint|FNBalQtyPadPrint|FNQtyWindow|FNRcvQtyWindow|FNBalQtyWindow|FNSPMKQuantityBal"
        'sFieldCustomSum &= "|FNExpQty|FNFGInQty|FNFGBalQty|FNExpBalQty"
        Dim sFieldCustomGrpSum As String = ""

        With ogvdetailcolorsizelineg
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
                'If Str <> "" Then
                '    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                'End If
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
    Private _RowHandleHold As Integer = 0


    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

    Private Sub ogvsummary_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvdetailcolorsizelineg.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvdetailcolorsizelineg
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNCutBalQuantity", "FNSPMKQuantityBal"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or
                                                        .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or
                                                             .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold, "FTColorway").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold, "FTSizeBreakDown").ToString) Or e.RowHandle = _RowHandleHold Then
                                        ' .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or _
                                        totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))

                                    End If
                                End If
                                _RowHandleHold = e.RowHandle
                            End If
                            e.TotalValue = totalSum
                        End If

                    Case "FNBalCutQuantity"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or
                                                        .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or
                                                             .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold, "FTColorway").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTUnitSectCodeCut").ToString <> .GetRowCellValue(_RowHandleHold, "FTUnitSectCodeCut").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold, "FTSizeBreakDown").ToString) Or e.RowHandle = _RowHandleHold Then
                                        ' .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or _
                                        totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))

                                    End If
                                End If
                                _RowHandleHold = e.RowHandle
                            End If
                            e.TotalValue = totalSum
                        End If


                    Case "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FNQuantity"

                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or
                                                             .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold, "FTColorway").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold, "FTSizeBreakDown").ToString) Or e.RowHandle = _RowHandleHold Then

                                        totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))

                                    End If
                                End If
                                _RowHandleHold = e.RowHandle
                            End If
                            e.TotalValue = totalSum
                        End If

                    Case "FNCutQuantity", "FNSendSuplQuantity", "FNRcvSuplQuantity", "FNSPMKQuantity", "FNBalSuplQuantity", "FNExpQty", "FNFGInQty", "FNFGBalQty", "FNExpBalQty"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then

                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or
                                                             .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold, "FTColorway").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold, "FTSizeBreakDown").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTUnitSectCodeCut").ToString <> .GetRowCellValue(_RowHandleHold, "FTUnitSectCodeCut").ToString) Or e.RowHandle = _RowHandleHold Then

                                        totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))

                                    End If
                                End If

                                _RowHandleHold = e.RowHandle

                            End If

                            e.TotalValue = totalSum

                        End If

                    Case "FNQtyEmbroidery", "FNRcvQtyEmbroidery", "FNBalQtyEmbroidery" _
                        , "FNQtyPrint", "FNRcvQtyPrint", "FNBalQtyPrint" _
                         , "FNQtyHeat", "FNRcvQtyHeat", "FNBalQtyHeat" _
                         , "FNQtyLaser", "FNRcvQtyLaser", "FNBalQtyLaser" _
                         , "FNQtyPadPrint", "FNRcvQtyPadPrint", "FNBalQtyPadPrint" _
                         , "FNQtyWindow", "FNRcvQtyWindow", "FNBalQtyWindow"

                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then

                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or
                                                             .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold, "FTColorway").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold, "FTSizeBreakDown").ToString Or
                                                             .GetRowCellValue(e.RowHandle, "FTUnitSectCodeCut").ToString <> .GetRowCellValue(_RowHandleHold, "FTUnitSectCodeCut").ToString) Or e.RowHandle = _RowHandleHold Then

                                        totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))

                                    End If
                                End If

                                _RowHandleHold = e.RowHandle

                            End If

                            e.TotalValue = totalSum

                        End If

                    Case "FNSewQuantity", "FNPackQuantity", "FNBalSewQuantity", "FNBalPackQuantity", "FNSewOutQuantity"

                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    'If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                    '                        .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or _
                                    '                         .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold, "FTColorway").ToString Or _
                                    '                         .GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold, "FTSizeBreakDown").ToString Or _
                                    '                         .GetRowCellValue(e.RowHandle, "FTUnitSectCodeSew").ToString <> .GetRowCellValue(_RowHandleHold, "FTUnitSectCodeSew").ToString) Or e.RowHandle = _RowHandleHold Then

                                    '    totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))

                                    'End If
                                    totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))
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

        Me.ogcdetailcolorsizelineg.DataSource = Nothing
        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        StateCal = False
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            Call LoaddataDetailColorSizeByLine()
            'Call LoaddataDetailColorSizeByLine_byline()
        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Sub LoaddataDetailColorSizeByLine()
        Dim _Qry As String
        Dim _dt As System.Data.DataTable

        Try
            Select Case Me.ogcdetailcolorsizeline.SelectedTabPageIndex
                Case 0
                    ogcdetailcolorsizelineg.DataSource = Nothing

                    _Qry = "exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..SP_GET_OrderWIPDaily_CD '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'"
                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    Me.ogcdetailcolorsizelineg.DataSource = _dt
                    Call InitialGridMergCell()


                Case Else

                    ogcmonthly.DataSource = Nothing
                    _Qry = "exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..SP_GET_OrderWIPDaily_Monthly_BF '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' , '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'," & Val(HI.ST.SysInfo.CmpID)
                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Qry = "exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..SP_GET_OrderWIPDaily_Monthly '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' , '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'," & Val(HI.ST.SysInfo.CmpID)
                    _dt.Merge(HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD))




                    Me.ogcmonthly.DataSource = _dt
            End Select





        Catch ex As Exception
        End Try

    End Sub


    Private Sub LoaddataDetailColorSizeByLine_byline()
        Dim _Qry As String
        Dim _dt As DataTable
        ogcdetailcolorsizelineg.DataSource = Nothing
        Try

            _Qry = "  CREATE TABLE #TabOrder([FTOrderNo] [nvarchar](30) NULL) CREATE INDEX [IDX_Tmp] ON #TabOrder([FTOrderNo]) "


            _Qry &= vbCrLf & " CREATE TABLE #TabDataBD([FTOrderNo] [nvarchar](30) NULL,		"
            _Qry &= vbCrLf & "  [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "  [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "  [FNScanQuantity] [Int] NULL,  "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "  [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "  ) CREATE INDEX [IDX_Tmp] ON #TabDataBD([FTOrderNo],[FTColorway],[FTSizeBreakDown])"

            _Qry &= vbCrLf & " CREATE TABLE #TabData([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNScanQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpD] ON #TabData([FTOrderNo],[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"

            _Qry &= vbCrLf & " CREATE TABLE #TabDataLC([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpLc] ON #TabDataLC([FTOrderNo],[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"


            _Qry &= vbCrLf & " CREATE TABLE #TabDataCut([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpCut] ON #TabDataCut([FTOrderNo],[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"


            _Qry &= vbCrLf & " CREATE TABLE #TabDataSendSupl([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNSendSuplType] [Int] NULL,  "
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpSendSupl] ON #TabDataSendSupl([FTOrderNo],[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"


            _Qry &= vbCrLf & " CREATE TABLE #TabDataRcvSupl([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNSendSuplType] [Int] NULL,  "
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpRcvSupl] ON #TabDataRcvSupl([FTOrderNo],[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"

            _Qry &= vbCrLf & " CREATE TABLE #TabDataSMK([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNQuantityOut] [Int] NULL, "
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpSMK] ON #TabDataSMK([FTOrderNo],[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"

            _Qry &= vbCrLf & " CREATE TABLE #TabDataSew([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "  [FTUnitSectCodeSew] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpSew] ON #TabDataSew([FTOrderNo],[FTUnitSectCode],[FTUnitSectCodeSew],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"

            _Qry &= vbCrLf & " CREATE TABLE #TabDataSewOut([FTOrderNo] [nvarchar](30) NULL,				 "
            _Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
            _Qry &= vbCrLf & "   [FNScanQuantity] [Int] NULL,               "
            _Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,"
            _Qry &= vbCrLf & "   ) CREATE INDEX [IDX_TmpSewOut] ON #TabDataSewOut([FTOrderNo],[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo])"

            _Qry &= vbCrLf & "   INSERT INTO #TabData (FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNScanQuantity, FTPOLineItemNo, FTBarcodeNo )"

            'Select Case True
            'Case (FTStartDateScanIn.Text <> "" Or FTEndDateScanIn.Text <> "")

            '    _Qry &= vbCrLf & "   	SELECT P.FTOrderNo,USC.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity , B.FTPOLineItemNo , B.FTBarcodeBundleNo"
            '    _Qry &= vbCrLf & "   	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            '    _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            '    _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo  INNER JOIN"
            '    _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '    _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo  INNER JOIN"
            '    _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '    _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"

            '    If Not (_StateQtyBySPM) Then
            '        _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            '    Else
            '        _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC WITH(NOLOCK) ON B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
            '        _Qry &= vbCrLf & " and A.FTBarcodeNo = TC.FTBarcodeNo	-- and A.FNHSysOperationId = TC.FNHSysOperationId and A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            '    End If


            '    _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As USC With(NOLOCK) On TC.FNHSysUnitSectId = USC.FNHSysUnitSectId "
            '    _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan As HA With(NOLOCK)  On A.FTDocScanNo=HA.FTDocScanNo"
            '    _Qry &= vbCrLf & "   WHERE  (US.FTStateSew = '1')   AND (A.FNHSysOperationId = 1405310010) "
            '    If (_StateQtyBySPM) Then
            '        _Qry &= vbCrLf & "  and  TC.FNHSysOperationId = 1405310009  "
            '    End If
            '    If FTStartDateScanIn.Text <> "" Or FTEndDateScanIn.Text <> "" Then

            '        If FTStartDateScanIn.Text <> "" Then
            '            _Qry &= vbCrLf & "   AND HA.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScanIn.Text) & "' "
            '        End If

            '        If FTEndDateScanIn.Text <> "" Then
            '            _Qry &= vbCrLf & "   AND HA.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScanIn.Text) & "' "
            '        End If

            '    End If

            '    _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,USC.FTUnitSectCode,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo, B.FTBarcodeBundleNo "

            'Case (FTStartDateScan.Text <> "" Or FTEndDateScan.Text <> "")
            '    _Qry &= vbCrLf & " Select  T.FTOrderNo,T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,T.FTPOLineItemNo ,T.FTBarcodeNo    "

            '    _Qry &= vbCrLf & " From("
            '    _Qry &= vbCrLf & "  Select  P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNQuantity) As FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeNo"
            '    _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
            '    _Qry &= vbCrLf & " Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With(NOLOCK) On SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            '    _Qry &= vbCrLf & " Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            '    _Qry &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) On B.FTOrderProdNo = P.FTOrderProdNo"
            '    _Qry &= vbCrLf & "  WHERE P.FTOrderNo <>'' "


            '    If FTStartDateScan.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SC.FDDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScan.Text) & "' "
            '    End If

            '    If FTEndDateScan.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SC.FDDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScan.Text) & "' "
            '    End If

            '    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo, US.FTUnitSectCode, B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItemNo, SC.FTBarcodeNo) As T "

            '    _Qry &= vbCrLf & " GROUP BY T.FTOrderNo, T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown, T.FTPOLineItemNo, T.FTBarcodeNo "
            'Case (FTSSendSuplDate.Text <> "" Or FTESendSuplDate.Text <> "")
            '    _Qry &= vbCrLf & " Select  T.FTOrderNo,T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,T.FTPOLineItemNo ,T.FTBarcodeNo    "

            '    _Qry &= vbCrLf & " From("
            '    _Qry &= vbCrLf & "  Select  P.FTOrderNo,'' As FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) As FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeBundleNo AS FTBarcodeNo"

            '    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS SC2 WITH(NOLOCK)  "
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS A WITH(NOLOCK) ON  SC2.FTSendSuplNo = A.FTSendSuplNo  "
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl  AS SC WITH(NOLOCK) ON SC2.FTBarcodeSendSuplNo = SC.FTBarcodeSendSuplNo    "


            '    _Qry &= vbCrLf & " Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON SC.FTBarcodeBundleNo = B.FTBarcodeBundleNo"
            '    _Qry &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) On B.FTOrderProdNo = P.FTOrderProdNo"
            '    _Qry &= vbCrLf & "  WHERE P.FTOrderNo <>'' "


            '    If FTSSendSuplDate.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND A.FDSendSuplDate >='" & HI.UL.ULDate.ConvertEnDB(FTSSendSuplDate.Text) & "' "
            '    End If

            '    If FTESendSuplDate.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND A.FDSendSuplDate <='" & HI.UL.ULDate.ConvertEnDB(FTESendSuplDate.Text) & "' "
            '    End If

            '    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo,  B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItemNo, SC.FTBarcodeBundleNo) As T "

            '    _Qry &= vbCrLf & " GROUP BY T.FTOrderNo, T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown, T.FTPOLineItemNo, T.FTBarcodeNo "
            'Case (FTSRcvSuplDate.Text <> "" Or FTERcvSuplDate.Text <> "")
            '    _Qry &= vbCrLf & " Select  T.FTOrderNo,T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,T.FTPOLineItemNo ,T.FTBarcodeNo    "

            '    _Qry &= vbCrLf & " From("
            '    _Qry &= vbCrLf & "  Select  P.FTOrderNo,'' AS FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) As FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeBundleNo AS FTBarcodeNo"

            '    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS SC2 WITH(NOLOCK)   "
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS A WITH(NOLOCK) ON  SC2.FTRcvSuplNo = A.FTRcvSuplNo  "
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl  AS SC WITH(NOLOCK) ON  SC2.FTBarcodeSendSuplNo =SC.FTBarcodeSendSuplNo  "


            '    _Qry &= vbCrLf & " Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) On SC.FTBarcodeBundleNo = B.FTBarcodeBundleNo"
            '    _Qry &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) On B.FTOrderProdNo = P.FTOrderProdNo"
            '    _Qry &= vbCrLf & "  WHERE P.FTOrderNo <>'' "


            '    If FTSRcvSuplDate.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND A.FDRcvSuplDate >='" & HI.UL.ULDate.ConvertEnDB(FTSRcvSuplDate.Text) & "' "
            '    End If

            '    If FTERcvSuplDate.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND A.FDRcvSuplDate <='" & HI.UL.ULDate.ConvertEnDB(FTERcvSuplDate.Text) & "' "
            '    End If

            '    _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItemNo, SC.FTBarcodeBundleNo) As T "

            '    _Qry &= vbCrLf & " GROUP BY T.FTOrderNo, T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown, T.FTPOLineItemNo, T.FTBarcodeNo "
            'Case (FTSSMKDate.Text <> "" Or FTESMKDate.Text <> "")
            _Qry &= vbCrLf & " Select  T.FTOrderNo,T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,T.FTPOLineItemNo ,T.FTBarcodeNo    "

            _Qry &= vbCrLf & " From("
            _Qry &= vbCrLf & "  Select  P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) As FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeNo"
            _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS SC WITH(NOLOCK)"
            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS SC2 WITH(NOLOCK) ON SC.FTDocScanNo = SC2.FTDocScanNo  "
            _Qry &= vbCrLf & " Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With(NOLOCK) On SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & " Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) On B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "  WHERE P.FTOrderNo <>'' "


            'If FTSSMKDate.Text <> "" Then
            '    _Qry &= vbCrLf & "   AND SC2.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(FTSSMKDate.Text) & "' "
            'End If

            'If FTESMKDate.Text <> "" Then
            '    _Qry &= vbCrLf & "   AND SC2.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(FTESMKDate.Text) & "' "
            'End If

            _Qry &= vbCrLf & "  GROUP BY P.FTOrderNo, US.FTUnitSectCode, B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItemNo, SC.FTBarcodeNo) As T "

            _Qry &= vbCrLf & " GROUP BY T.FTOrderNo, T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown, T.FTPOLineItemNo, T.FTBarcodeNo "

            '    Case Else

            '        _Qry &= vbCrLf & " Select  FTOrderNo,'' FTUnitSectCode, FTColorway, FTSizeBreakDown ,0 AS  FNScanQuantity ,FTNikePOLineItem ,'' FTBarcodeNo    "

            '        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination As Z  "
            '        _Qry &= vbCrLf & " WHERE Z.FTOrderNo <> '' "

            '        If FNHSysBuyId.Text <> "" Then
            '            _Qry &= vbCrLf & " And Z.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            '        End If

            '        If FNHSysStyleId.Text <> "" Then
            '            _Qry &= vbCrLf & " And Z.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            '        End If

            '        If FTOrderNo.Text <> "" Then
            '            _Qry &= vbCrLf & " And Z.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            '        End If

            '        If FTOrderNoTo.Text <> "" Then
            '            _Qry &= vbCrLf & " AND Z.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            '        End If


            '        If FTCustomerPO.Text <> "" Then
            '            _Qry &= vbCrLf & " AND Z.FTPOref >='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
            '        End If

            '        If FTCustomerPOTo.Text <> "" Then
            '            _Qry &= vbCrLf & " AND Z.FTPOref <='" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'  "
            '        End If

            '        If FTStartShipment.Text <> "" Then
            '            _Qry &= vbCrLf & " AND Z.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
            '        End If

            '        If FTEndShipment.Text <> "" Then

            '            _Qry &= vbCrLf & " AND Z.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "

            '        End If

            'End Select




            _Qry &= vbCrLf & " INSERT INTO #TabDataBD(FTOrderNo, FTColorway, FTSizeBreakDown, FTPOLineItemNo)"
            _Qry &= vbCrLf & " Select  T.FTOrderNo, T.FTColorway, T.FTSizeBreakDown  ,MAX(T.FTPOLineItemNo) AS FTPOLineItemNo"
            _Qry &= vbCrLf & " FROM #TabData   AS  T  INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination As Z On T.FTOrderNo = Z.FTOrderNo "
            _Qry &= vbCrLf & " And T.FTColorway = Z.FTColorway"
            _Qry &= vbCrLf & "And  T.FTSizeBreakDown = Z.FTSizeBreakDown"

            _Qry &= vbCrLf & " WHERE T.FTOrderNo <> '' "
            'If FNHSysBuyId.Text <> "" Then
            '    _Qry &= vbCrLf & " And Z.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            'End If

            'If FNHSysStyleId.Text <> "" Then
            '    _Qry &= vbCrLf & " And Z.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            'End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " And T.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND T.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If


            'If FTCustomerPO.Text <> "" Then
            '    _Qry &= vbCrLf & " AND Z.FTPOref >='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
            'End If

            'If FTCustomerPOTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND Z.FTPOref <='" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'  "
            'End If

            'If FTStartShipment.Text <> "" Then
            '    _Qry &= vbCrLf & " AND Z.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
            'End If

            'If FTEndShipment.Text <> "" Then

            '    _Qry &= vbCrLf & " AND Z.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "

            'End If
            _Qry &= vbCrLf & " GROUP BY   T.FTOrderNo, T.FTColorway, T.FTSizeBreakDown "

            _Qry &= vbCrLf & "  INSERT INTO #TabOrder ([FTOrderNo]) "
            _Qry &= vbCrLf & "  SELECT DISTINCT FTOrderNo  FROM #TabDataBD "


            '----------Start LC
            _Qry &= vbCrLf & "   INSERT INTO #TabDataLC (FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo )"
            _Qry &= vbCrLf & " SELECT A.FTOrderNo,'' AS FTUnitSectCode ,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity , B.FTPOLineItemNo,'' "
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"
            _Qry &= vbCrLf & "       INNER Join #TabDataBD As X ON X.FTOrderNo = A.FTOrderNo And X.FTColorway=B.FTColorway And X.FTSizeBreakDown=B.FTSizeBreakDown  "
            _Qry &= vbCrLf & " WHERE   A.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder) AND  (B.FTStateGenBarcode = '1')"
            _Qry &= vbCrLf & "  GROUP BY A.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo "
            '----------END LC

            '----------Start Cut

            _Qry &= vbCrLf & "   INSERT INTO #TabDataCut (FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo )"
            _Qry &= vbCrLf & "   SELECT A.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity , B.FTPOLineItemNo,'' "
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"

            If Not (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            Else
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC WITH(NOLOCK) ON B.FTBarcodeBundleNo = TC.FTBarcodeNo "
            End If

            _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON TC.FNHSysUnitSectId = US.FNHSysUnitSectId "

            _Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X ON X.FTOrderNo = A.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown  "

            _Qry &= vbCrLf & "   WHERE    A.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  AND  (B.FTStateGenBarcode = '1')"
            If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & " and  TC.FNHSysOperationId = 1405310009 "
            End If
            _Qry &= vbCrLf & "   GROUP BY A.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo "

            '----------End Cut


            '----------Start Send Supl
            _Qry &= vbCrLf & "   INSERT INTO #TabDataSendSupl (FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo,FNSendSuplType )"

            _Qry &= vbCrLf & "   SELECT P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown ,SUM(BD.FNQuantity) AS FNQuantity,  B.FTPOLineItemNo,'',SS.FNSendSuplType  "
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS SS WITH(NOLOCK)   ON A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B  WITH(NOLOCK)  ON SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK)   ON SS.FTOrderProdNo = P.FTOrderProdNo  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo "
            _Qry &= vbCrLf & "         INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"

            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS SPH WITH(NOLOCK) ON A.FTSendSuplNo = SPH.FTSendSuplNo  "


            '_Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            If Not (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            Else
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC WITH(NOLOCK) ON B.FTBarcodeBundleNo = TC.FTBarcodeNo  "

                ' _Qry &= vbCrLf & " and A.FTBarcodeNo = TC.FTBarcodeNo	and A.FNHSysOperationId = TC.FNHSysOperationId and A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            End If

            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON TC.FNHSysUnitSectId = US.FNHSysUnitSectId "

            _Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X ON X.FTOrderNo = P.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown     "

            _Qry &= vbCrLf & "   WHERE  P.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  "


            'If FTSSendSuplDate.Text <> "" Or FTESendSuplDate.Text <> "" Then

            '    If FTSSendSuplDate.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND SPH.FDSendSuplDate >='" & HI.UL.ULDate.ConvertEnDB(FTSSendSuplDate.Text) & "' "

            '    End If

            '    If FTESendSuplDate.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND SPH.FDSendSuplDate <='" & HI.UL.ULDate.ConvertEnDB(FTESendSuplDate.Text) & "' "

            '    End If

            'End If


            If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "  AND  TC.FNHSysOperationId = 1405310009  "
            End If

            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown ,US.FTUnitSectCode , B.FTPOLineItemNo,SS.FNSendSuplType "

            '----------End  Send Supl

            '----------Start Rcv Supl
            _Qry &= vbCrLf & "   INSERT INTO #TabDataRcvSupl (FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo ,FNSendSuplType)"

            _Qry &= vbCrLf & "   Select  P.FTOrderNo,US.FTUnitSectCode ,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) As FNQuantity, B.FTPOLineItemNo,'',SS.FNSendSuplType "
            _Qry &= vbCrLf & "  	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode As A With(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl As SS With(NOLOCK)   On A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode As B  With(NOLOCK)  On SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK)   On SS.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail As BD With(NOLOCK) On B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut As LC With(NOLOCK)  On BD.FTLayCutNo = LC.FTLayCutNo "
            _Qry &= vbCrLf & "            INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain As PM With(NOLOCK)  On LC.FTOrderProdNo = PM.FTOrderProdNo And LC.FNHSysMarkId = PM.FNHSysMarkId"

            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS SPH WITH(NOLOCK) ON A.FTRcvSuplNo = SPH.FTRcvSuplNo  "

            '_Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            If Not (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            Else
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail As TC With(NOLOCK) On B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
                '    _Qry &= vbCrLf & " And A.FTBarcodeNo = TC.FTBarcodeNo	And A.FNHSysOperationId = TC.FNHSysOperationId And A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            End If

            _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With(NOLOCK) On TC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X On X.FTOrderNo = P.FTOrderNo And X.FTColorway=B.FTColorway And X.FTSizeBreakDown=B.FTSizeBreakDown     "
            _Qry &= vbCrLf & "   WHERE  P.FTOrderNo In (Select DISTINCT FTOrderNo FROM #TabOrder)  "


            'If FTSRcvSuplDate.Text <> "" Or FTERcvSuplDate.Text <> "" Then

            '    If FTSRcvSuplDate.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND SPH.FDRcvSuplDate >='" & HI.UL.ULDate.ConvertEnDB(FTSRcvSuplDate.Text) & "' "

            '    End If

            '    If FTERcvSuplDate.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND SPH.FDRcvSuplDate <='" & HI.UL.ULDate.ConvertEnDB(FTERcvSuplDate.Text) & "' "

            '    End If

            'End If

            If (_StateQtyBySPM) Then

                _Qry &= vbCrLf & "  And  TC.FNHSysOperationId = 1405310009  "

            End If

            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown,US.FTUnitSectCode  , B.FTPOLineItemNo,SS.FNSendSuplType "

            '----------End  Rcv Supl


            '----------Start SMK
            _Qry &= vbCrLf & "   INSERT INTO #TabDataSMK (FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo ,FNQuantityOut)"
            _Qry &= vbCrLf & "  Select P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) As FNQuantity , B.FTPOLineItemNo,'',SUM(ISNULL(XXX.FNQuantity,0)) AS FNQuantityOut "
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail As A With(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode As B With(NOLOCK) On A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) On B.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail As BD With(NOLOCK)   On B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut As LC With(NOLOCK)  On BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain As PM With(NOLOCK)  On LC.FTOrderProdNo = PM.FTOrderProdNo And LC.FNHSysMarkId = PM.FNHSysMarkId"


            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS SPH WITH(NOLOCK) ON A.FTDocScanNo = SPH.FTDocScanNo  "


            If Not (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            Else
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail As TC With(NOLOCK) On B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
                _Qry &= vbCrLf & " And A.FTBarcodeNo = TC.FTBarcodeNo	And A.FNHSysOperationId = TC.FNHSysOperationId And A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            End If

            '_Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With(NOLOCK) On TC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation As OPR With(NOLOCK) On A.FNHSysOperationId = OPR.FNHSysOperationId "

            _Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X On X.FTOrderNo = P.FTOrderNo And X.FTColorway=B.FTColorway And X.FTSizeBreakDown=B.FTSizeBreakDown      "


            _Qry &= vbCrLf & "   OUTER APPLY(  "

            _Qry &= vbCrLf & "   SELECT  SUM(BDXXX.FNQuantity) As FNQuantity "
            _Qry &= vbCrLf & "   	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS AXXX WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS USXXX WITH(NOLOCK) ON AXXX.FNHSysUnitSectId = USXXX.FNHSysUnitSectId INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BXXX WITH(NOLOCK) ON AXXX.FTBarcodeNo = BXXX.FTBarcodeBundleNo  INNER JOIN "
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BDXXX WITH(NOLOCK)  ON BXXX.FTBarcodeBundleNo = BDXXX.FTBarcodeBundleNo"
            _Qry &= vbCrLf & "   WHERE   AXXX.FTBarcodeNo = A.FTBarcodeNo  AND  (USXXX.FTStateSew = '1')   AND (AXXX.FNHSysOperationId = 1405310010) "
            _Qry &= vbCrLf & "   ) As XXX "


            _Qry &= vbCrLf & "     WHERE P.FTOrderNo In (Select DISTINCT FTOrderNo FROM #TabOrder) And (OPR.FTStateSPMK = '1')"



            'If FTSSMKDate.Text <> "" Or FTESMKDate.Text <> "" Then
            '    If FTSSMKDate.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SPH.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(FTSSMKDate.Text) & "' "
            '    End If
            '    If FTESMKDate.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SPH.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(FTESMKDate.Text) & "' "
            '    End If
            'End If




            If (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "  and  TC.FNHSysOperationId = 1405310009  "
            End If

            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown,US.FTUnitSectCode , B.FTPOLineItemNo "

            '----------End  SMK


            '----------Start Sew
            _Qry &= vbCrLf & "   INSERT INTO #TabDataSew (FTOrderNo, FTUnitSectCode,FTUnitSectCodeSew, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo )"
            _Qry &= vbCrLf & "   	SELECT P.FTOrderNo,USC.FTUnitSectCode,US.FTUnitSectCode AS FTUnitSectCodeSew,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity , B.FTPOLineItemNo , B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & "   	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo  INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo  INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"

            If Not (_StateQtyBySPM) Then
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            Else
                _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC WITH(NOLOCK) ON B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
                _Qry &= vbCrLf & " and A.FTBarcodeNo = TC.FTBarcodeNo	-- and A.FNHSysOperationId = TC.FNHSysOperationId and A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            End If


            _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As USC With(NOLOCK) On TC.FNHSysUnitSectId = USC.FNHSysUnitSectId "
            _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan As HA With(NOLOCK)  On A.FTDocScanNo=HA.FTDocScanNo"

            _Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X ON X.FTOrderNo = P.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown       "


            _Qry &= vbCrLf & "   WHERE  P.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  AND  (US.FTStateSew = '1')   AND (A.FNHSysOperationId = 1405310010) "

            If (_StateQtyBySPM) Then

                _Qry &= vbCrLf & "  and  TC.FNHSysOperationId = 1405310009  "

            End If

            'If FTStartDateScanIn.Text <> "" Or FTEndDateScanIn.Text <> "" Then

            '    If FTStartDateScanIn.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND HA.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScanIn.Text) & "' "

            '    End If

            '    If FTEndDateScanIn.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND HA.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScanIn.Text) & "' "

            '    End If

            'End If

            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,USC.FTUnitSectCode,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo, B.FTBarcodeBundleNo "

            '----------End  Sew

            '----------Start Sew Out
            _Qry &= vbCrLf & "   INSERT INTO #TabDataSewOut (FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNScanQuantity, FTPOLineItemNo, FTBarcodeNo )"
            _Qry &= vbCrLf & "  Select   FTOrderNo,FTUnitSectCode, FTColorway, FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,FTPOLineItemNo ,FTBarcodeNo    From ( "

            _Qry &= vbCrLf & "   SELECT P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNQuantity) AS FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeNo"
            '_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS SC"
            '_Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON SC.FNHSysUnitSectId = US.FNHSysUnitSectId "

            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & " INNER JOIN #TabDataBD As X ON X.FTOrderNo = P.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown  "

            _Qry &= vbCrLf & " WHERE P.FTOrderNo <>'' AND   P.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  "

            'If FTStartDateScan.Text <> "" Or FTEndDateScan.Text <> "" Then

            '    If FTStartDateScan.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SC.FDDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScan.Text) & "' "
            '    End If

            '    If FTEndDateScan.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SC.FDDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScan.Text) & "' "
            '    End If

            'End If

            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo , SC.FTBarcodeNo) AS T "
            _Qry &= vbCrLf & "  GROUP BY FTOrderNo,FTUnitSectCode,FTColorway,FTSizeBreakDown , FTPOLineItemNo ,FTBarcodeNo "


            '----------End  Sew Out

            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & "  Select   ST.FTStyleCode,A.FTOrderNo , A.FTPORef"
            _Qry &= vbCrLf & ",Cmp.FTCmpCode  ,Isnull(B.FTNikePOLineItem,'') AS FTPOLineItemNo "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
            Else
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
            End If

            _Qry &= vbCrLf & " ,ISNULL((SELECT  Convert(Datetime,MIN(FDShipDate)) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=A.FTOrderNo),null) AS FDShipDate"
            _Qry &= vbCrLf & " ,B.FTColorway "
            _Qry &= vbCrLf & " ,B.FTSizeBreakDown "

            _Qry &= vbCrLf & ",ISNULL(B.FNQuantity,0) AS FNQuantity "
            _Qry &= vbCrLf & ",ISNULL(B.FNQuantityExtra,0) AS FNQuantityExtra"
            _Qry &= vbCrLf & ",ISNULL(B.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & ",ISNULL(B.FNGrandQuantity,0) As FNGrandQuantity"

            _Qry &= vbCrLf & ",ISNULL(BM.FTUnitSectCode,'') AS FTUnitSectCodeCut"
            _Qry &= vbCrLf & ",ISNULL(BM.FNQuantity,0) As FNCutQuantity"
            _Qry &= vbCrLf & ", ISNULL(C.FNQuantity, 0) AS FNTableCut"
            _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(C.FNQuantity,0) )  As FNCutBalQuantity"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQuantitySPMK,0) AS FNSPMKQuantity"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQuantity,0) -ISNULL(BM.FNQuantitySPMK,0) )  As FNBalCutQuantity"

            _Qry &= vbCrLf & ",CASE WHEN (ISNULL(BM.FNQuantitySPMK,0) - ISNULL(BM.FNQuantitySPMKOut,0) )<=0 THEN 0 ELSE (ISNULL(BM.FNQuantitySPMK,0) - ISNULL(BM.FNQuantitySPMKOut,0) ) END  As  FNSPMKQuantityBal"


            _Qry &= vbCrLf & ",ISNULL(BM.FNQuantitySendSupl,0) AS FNSendSuplQuantity"
            _Qry &= vbCrLf & ",ISNULL(BM.FNQuantityRcvSupl,0) AS FNRcvSuplQuantity"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQuantitySendSupl,0) - ISNULL(BM.FNQuantityRcvSupl,0)) AS FNBalSuplQuantity"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQtyEmbroidery,0) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & ",ISNULL(BM.FNRcvQtyEmbroidery,0) AS FNRcvQtyEmbroidery"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQtyEmbroidery,0) - ISNULL(BM.FNRcvQtyEmbroidery,0)) AS FNBalQtyEmbroidery"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQtyPrint,0) AS FNQtyPrint"
            _Qry &= vbCrLf & ",ISNULL(BM.FNRcvQtyPrint,0) AS FNRcvQtyPrint"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQtyPrint,0) - ISNULL(BM.FNRcvQtyPrint,0)) AS FNBalQtyPrint"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQtyHeat,0) AS FNQtyHeat"
            _Qry &= vbCrLf & ",ISNULL(BM.FNRcvQtyHeat,0) AS FNRcvQtyHeat"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQtyHeat,0) - ISNULL(BM.FNRcvQtyHeat,0)) AS FNBalQtyHeat"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQtyLaser,0) AS FNQtyLaser"
            _Qry &= vbCrLf & ",ISNULL(BM.FNRcvQtyLaser,0) AS FNRcvQtyLaser"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQtyLaser,0) - ISNULL(BM.FNRcvQtyLaser,0)) AS FNBalQtyLaser"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQtyPadPrint,0) AS FNQtyPadPrint"
            _Qry &= vbCrLf & ",ISNULL(BM.FNRcvQtyPadPrint,0) AS FNRcvQtyPadPrint"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQtyPadPrint,0) - ISNULL(BM.FNRcvQtyPadPrint,0)) AS FNBalQtyPadPrint"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQtyWindow,0) AS FNQtyWindow"
            _Qry &= vbCrLf & ",ISNULL(BM.FNRcvQtyWindow,0) AS FNRcvQtyWindow"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQtyWindow,0) - ISNULL(BM.FNRcvQtyWindow,0)) AS FNBalQtyWindow"


            _Qry &= vbCrLf & ",ISNULL(BM.FTUnitSectCodeSew,'') AS FTUnitSectCodeSew"
            _Qry &= vbCrLf & ",ISNULL(BM.FNQuantitySew,0) AS FNSewQuantity"
            _Qry &= vbCrLf & ",ISNULL(BM.FNQuantityScan,0) AS FNSewOutQuantity"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQuantitySew,0)-ISNULL(BM.FNQuantityScan,0)) AS FNBalSewQuantity"

            _Qry &= vbCrLf & ",ISNULL(BM.FNQuantityPack,0) AS FNPackQuantity"
            _Qry &= vbCrLf & ",(ISNULL(BM.FNQuantityScan,0)-ISNULL(BM.FNQuantityPack,0) ) AS FNBalPackQuantity"

            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId=ST.FNHSysStyleId INNER jOIN"
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "	SELECT FTNikePOLineItem,FTOrderNo,FTColorway,FTSizeBreakDown, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  SBD.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  "

            _Qry &= vbCrLf & "	GROUP BY FTNikePOLineItem,FTOrderNo,FTColorway,FTSizeBreakDown"
            _Qry &= vbCrLf & "  ) AS B ON A.FTOrderNo = B.FTOrderNo  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("

            '----------Start LC

            _Qry &= vbCrLf & " SELECT * FROM #TabDataLC "
            '_Qry &= vbCrLf & " SELECT A.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity , B.FTPOLineItemNo "
            '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"
            '_Qry &= vbCrLf & "       INNER Join #TabDataBD As X ON X.FTOrderNo = A.FTOrderNo And X.FTColorway=B.FTColorway And X.FTSizeBreakDown=B.FTSizeBreakDown  "
            '_Qry &= vbCrLf & " WHERE   A.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder) AND  (B.FTStateGenBarcode = '1')"
            '_Qry &= vbCrLf & "  GROUP BY A.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo "

            '----------END LC
            _Qry &= vbCrLf & "  ) AS C ON A.FTOrderNo = C.FTOrderNo  AND B.FTColorway = C.FTColorway AND B.FTSizeBreakDown = C.FTSizeBreakDown  and B.FTNikePOLineItem = C.FTPOLineItemNo"

            'If FTStartDateScan.Text = "" And FTEndDateScan.Text = "" Then
            '    _Qry &= vbCrLf & "   LEFT OUTER JOIN "
            'Else
            _Qry &= vbCrLf & "  INNER JOIN "
            'End If

            _Qry &= vbCrLf & " ("

            _Qry &= vbCrLf & "     Select Cut.FTOrderNo"
            _Qry &= vbCrLf & "   ,Cut.FTUnitSectCode"
            _Qry &= vbCrLf & "   ,Cut.FTColorway"
            _Qry &= vbCrLf & "   ,Cut.FTSizeBreakDown"
            _Qry &= vbCrLf & "   ,Cut.FNQuantity"
            _Qry &= vbCrLf & "   ,ISNULL(SPMK.FNQuantity ,0) AS FNQuantitySPMK ,ISNULL(SPMK.FNQuantityOut,0) AS FNQuantitySPMKOut"
            _Qry &= vbCrLf & "   ,ISNULL(SendSupl.FNQuantity ,0) AS FNQuantitySendSupl"
            _Qry &= vbCrLf & "   ,ISNULL(RcvSupl.FNQuantity ,0) AS FNQuantityRcvSupl"


            _Qry &= vbCrLf & "   ,ISNULL(SendSupl.FNQtyEmbroidery ,0) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & "   ,ISNULL(SendSupl.FNQtyPrint ,0) AS FNQtyPrint"
            _Qry &= vbCrLf & "   ,ISNULL(SendSupl.FNQtyHeat ,0) AS FNQtyHeat"
            _Qry &= vbCrLf & "   ,ISNULL(SendSupl.FNQtyLaser ,0) AS FNQtyLaser"
            _Qry &= vbCrLf & "   ,ISNULL(SendSupl.FNQtyPadPrint ,0) AS FNQtyPadPrint"
            _Qry &= vbCrLf & "   ,ISNULL(SendSupl.FNQtyWindow ,0) AS FNQtyWindow"

            _Qry &= vbCrLf & "   ,ISNULL(RcvSupl.FNQtyEmbroidery ,0) AS FNRcvQtyEmbroidery"
            _Qry &= vbCrLf & "   ,ISNULL(RcvSupl.FNQtyPrint ,0) AS FNRcvQtyPrint"
            _Qry &= vbCrLf & "   ,ISNULL(RcvSupl.FNQtyHeat ,0) AS FNRcvQtyHeat"
            _Qry &= vbCrLf & "   ,ISNULL(RcvSupl.FNQtyLaser ,0) AS FNRcvQtyLaser"
            _Qry &= vbCrLf & "   ,ISNULL(RcvSupl.FNQtyPadPrint ,0) AS FNRcvQtyPadPrint"
            _Qry &= vbCrLf & "   ,ISNULL(RcvSupl.FNQtyWindow ,0) AS FNRcvQtyWindow"


            _Qry &= vbCrLf & "   ,ISNULL(Sew.FTUnitSectCodeSew ,'') AS FTUnitSectCodeSew"
            _Qry &= vbCrLf & "   ,ISNULL(Sew.FNQuantity,0) AS FNQuantitySew "
            _Qry &= vbCrLf & "   ,ISNULL(SewOut.FNScanQuantity,0) AS FNQuantityScan "
            _Qry &= vbCrLf & "   ,ISNULL(PAC.FNQuantity,0) AS FNQuantityPack  ,Cut.FTPOLineItemNo "
            _Qry &= vbCrLf & "      FROM"
            _Qry &= vbCrLf & "   ("


            '----------Start Cut

            _Qry &= vbCrLf & " SELECT * FROM #TabDataCut "

            '_Qry &= vbCrLf & "   SELECT A.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity , B.FTPOLineItemNo "
            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"

            'If Not (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            'Else
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC WITH(NOLOCK) ON B.FTBarcodeBundleNo = TC.FTBarcodeNo "
            'End If

            '_Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON TC.FNHSysUnitSectId = US.FNHSysUnitSectId "

            '_Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X ON X.FTOrderNo = A.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown   "

            '_Qry &= vbCrLf & "   WHERE    A.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  AND  (B.FTStateGenBarcode = '1')"
            'If (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & " and  TC.FNHSysOperationId = 1405310009 "
            'End If
            '_Qry &= vbCrLf & "   GROUP BY A.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo "



            '----------END Cut

            _Qry &= vbCrLf & "   ) AS Cut"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("

            '----------Start Send Supl
            'FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo ,FNSendSuplType


            _Qry &= vbCrLf & " SELECT FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FTPOLineItemNo, FTBarcodeNo,SUM(FNQuantity) AS FNQuantity "

            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =0 THEN FNQuantity ELSE 0 END ) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =1 THEN FNQuantity ELSE 0 END ) AS FNQtyPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =2 THEN FNQuantity ELSE 0 END ) AS FNQtyHeat"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =3 THEN FNQuantity ELSE 0 END ) AS FNQtyLaser"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =4 THEN FNQuantity ELSE 0 END ) AS FNQtyPadPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =5 THEN FNQuantity ELSE 0 END ) AS FNQtyWindow"

            _Qry &= vbCrLf & "  FROM #TabDataSendSupl  "
            _Qry &= vbCrLf & " GROUP BY  FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FTPOLineItemNo, FTBarcodeNo "

            '_Qry &= vbCrLf & "   SELECT SUM(BD.FNQuantity) AS FNQuantity, P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown,US.FTUnitSectCode , B.FTPOLineItemNo  "
            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS A WITH(NOLOCK)   INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS SS WITH(NOLOCK)   ON A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B  WITH(NOLOCK)  ON SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK)   ON SS.FTOrderProdNo = P.FTOrderProdNo  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"

            ''_Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            'If Not (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            'Else
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC WITH(NOLOCK) ON B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
            '    ' _Qry &= vbCrLf & " and A.FTBarcodeNo = TC.FTBarcodeNo	and A.FNHSysOperationId = TC.FNHSysOperationId and A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            'End If

            '_Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON TC.FNHSysUnitSectId = US.FNHSysUnitSectId "

            '_Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X ON X.FTOrderNo = P.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown     "

            '_Qry &= vbCrLf & "   WHERE  P.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  "
            'If (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & "  AND  TC.FNHSysOperationId = 1405310009  "
            'End If

            '_Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown ,US.FTUnitSectCode , B.FTPOLineItemNo "

            '----------END Send Supl

            _Qry &= vbCrLf & "    ) AS SendSupl ON Cut.FTOrderNo = SendSupl.FTOrderNo AND Cut.FTUnitSectCode = SendSupl.FTUnitSectCode AND Cut.FTColorway = SendSupl.FTColorway AND Cut.FTSizeBreakDown = SendSupl.FTSizeBreakDown  and Cut.FTPOLineItemNo = SendSupl.FTPOLineItemNo "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("



            '----------Start Rcv Supl
            'FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNQuantity, FTPOLineItemNo, FTBarcodeNo ,FNSendSuplType
            _Qry &= vbCrLf & " SELECT FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FTPOLineItemNo, FTBarcodeNo,SUM(FNQuantity) AS FNQuantity "

            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =0 THEN FNQuantity ELSE 0 END ) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =1 THEN FNQuantity ELSE 0 END ) AS FNQtyPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =2 THEN FNQuantity ELSE 0 END ) AS FNQtyHeat"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =3 THEN FNQuantity ELSE 0 END ) AS FNQtyLaser"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =4 THEN FNQuantity ELSE 0 END ) AS FNQtyPadPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN FNSendSuplType =5 THEN FNQuantity ELSE 0 END ) AS FNQtyWindow"

            _Qry &= vbCrLf & "  FROM #TabDataRcvSupl "
            _Qry &= vbCrLf & " GROUP BY  FTOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FTPOLineItemNo, FTBarcodeNo "

            '_Qry &= vbCrLf & "   Select SUM(BD.FNQuantity) As FNQuantity, P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown,US.FTUnitSectCode , B.FTPOLineItemNo "
            '_Qry &= vbCrLf & "  	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode As A With(NOLOCK)   INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl As SS With(NOLOCK)   On A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B  With(NOLOCK)  On SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK)   On SS.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail As BD With(NOLOCK) On B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut As LC With(NOLOCK)  On BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain As PM With(NOLOCK)  On LC.FTOrderProdNo = PM.FTOrderProdNo And LC.FNHSysMarkId = PM.FNHSysMarkId"

            ''_Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            'If Not (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            'Else
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail As TC With(NOLOCK) On B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
            '    '    _Qry &= vbCrLf & " And A.FTBarcodeNo = TC.FTBarcodeNo	And A.FNHSysOperationId = TC.FNHSysOperationId And A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            'End If

            '_Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With(NOLOCK) On TC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            '_Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X On X.FTOrderNo = P.FTOrderNo And X.FTColorway=B.FTColorway And X.FTSizeBreakDown=B.FTSizeBreakDown     "
            '_Qry &= vbCrLf & "   WHERE  P.FTOrderNo In (Select DISTINCT FTOrderNo FROM #TabOrder)  "

            'If (_StateQtyBySPM) Then

            '    _Qry &= vbCrLf & "  And  TC.FNHSysOperationId = 1405310009  "

            'End If

            '_Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown,US.FTUnitSectCode  , B.FTPOLineItemNo "

            '----------END Rcv Supl
            _Qry &= vbCrLf & "   ) As RcvSupl On Cut.FTOrderNo = RcvSupl.FTOrderNo  And Cut.FTUnitSectCode = RcvSupl.FTUnitSectCode  And Cut.FTColorway = RcvSupl.FTColorway And Cut.FTSizeBreakDown = RcvSupl.FTSizeBreakDown And Cut.FTPOLineItemNo = RcvSupl.FTPOLineItemNo "

            _Qry &= vbCrLf & "    LEFT OUTER JOIN ("

            '----------Start SMK
            _Qry &= vbCrLf & " SELECT * FROM #TabDataSMK "
            '_Qry &= vbCrLf & "  Select P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) As FNQuantity,US.FTUnitSectCode , B.FTPOLineItemNo "
            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail As A With(NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) On A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) On B.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail As BD With(NOLOCK)   On B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut As LC With(NOLOCK)  On BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain As PM With(NOLOCK)  On LC.FTOrderProdNo = PM.FTOrderProdNo And LC.FNHSysMarkId = PM.FNHSysMarkId"

            'If Not (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            'Else
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail As TC With(NOLOCK) On B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
            '    _Qry &= vbCrLf & " And A.FTBarcodeNo = TC.FTBarcodeNo	And A.FNHSysOperationId = TC.FNHSysOperationId And A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            'End If

            ''_Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut As TC With(NOLOCK) On LC.FTOrderProdNo = TC.FTOrderProdNo And LC.FNHSysMarkId = TC.FNHSysMarkId And LC.FNTableNo = TC.FNTableNo "
            '_Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With(NOLOCK) On TC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            '_Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation As OPR With(NOLOCK) On A.FNHSysOperationId = OPR.FNHSysOperationId "

            '_Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X On X.FTOrderNo = P.FTOrderNo And X.FTColorway=B.FTColorway And X.FTSizeBreakDown=B.FTSizeBreakDown      "

            '_Qry &= vbCrLf & "     WHERE P.FTOrderNo In (Select DISTINCT FTOrderNo FROM #TabOrder) And (OPR.FTStateSPMK = '1')"

            'If (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & "  and  TC.FNHSysOperationId = 1405310009  "
            'End If

            '_Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown,US.FTUnitSectCode , B.FTPOLineItemNo "

            '----------END SMK

            _Qry &= vbCrLf & "    ) AS SPMK ON Cut.FTOrderNo = SPMK.FTOrderNo AND Cut .FTUnitSectCode = SPMK.FTUnitSectCode  AND Cut.FTColorway = SPMK.FTColorway AND Cut.FTSizeBreakDown = SPMK.FTSizeBreakDown and Cut.FTPOLineItemNo = SPMK.FTPOLineItemNo "

            'If FTStartDateScanIn.Text = "" And FTEndDateScanIn.Text = "" Then
            '    _Qry &= vbCrLf & "     LEFT OUTER JOIN ("
            'Else
            _Qry &= vbCrLf & "     INNER JOIN ("
            'End If



            '----------Start Sew
            _Qry &= vbCrLf & " SELECT * FROM #TabDataSew "
            '_Qry &= vbCrLf & "   	SELECT P.FTOrderNo,USC.FTUnitSectCode,US.FTUnitSectCode AS FTUnitSectCodeSew,B.FTColorway,B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity , B.FTPOLineItemNo , B.FTBarcodeBundleNo AS FTBarcodeNo"
            '_Qry &= vbCrLf & "   	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo  INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo  INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"

            'If Not (_StateQtyBySPM) Then
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH(NOLOCK) ON LC.FTOrderProdNo = TC.FTOrderProdNo AND LC.FNHSysMarkId = TC.FNHSysMarkId AND LC.FNTableNo = TC.FNTableNo "
            'Else
            '    _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS TC WITH(NOLOCK) ON B.FTBarcodeBundleNo = TC.FTBarcodeNo  "
            '    _Qry &= vbCrLf & " and A.FTBarcodeNo = TC.FTBarcodeNo	-- and A.FNHSysOperationId = TC.FNHSysOperationId and A.FNHSysUnitSectId = TC.FNHSysUnitSectId "
            'End If


            '_Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As USC With(NOLOCK) On TC.FNHSysUnitSectId = USC.FNHSysUnitSectId "
            '_Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan As HA With(NOLOCK)  On A.FTDocScanNo=HA.FTDocScanNo"

            '_Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X ON X.FTOrderNo = P.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown       "


            '_Qry &= vbCrLf & "   WHERE  P.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  AND  (US.FTStateSew = '1')   AND (A.FNHSysOperationId = 1405310010) "

            'If (_StateQtyBySPM) Then

            '    _Qry &= vbCrLf & "  and  TC.FNHSysOperationId = 1405310009  "

            'End If

            'If FTStartDateScanIn.Text <> "" Or FTEndDateScanIn.Text <> "" Then

            '    If FTStartDateScanIn.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND HA.FDDocScanDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScanIn.Text) & "' "

            '    End If

            '    If FTEndDateScanIn.Text <> "" Then

            '        _Qry &= vbCrLf & "   AND HA.FDDocScanDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScanIn.Text) & "' "

            '    End If

            'End If

            '_Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,USC.FTUnitSectCode,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo, B.FTBarcodeBundleNo "


            '----------End Sew

            _Qry &= vbCrLf & "    ) AS Sew ON Cut.FTOrderNo = Sew.FTOrderNo AND Cut .FTUnitSectCode = Sew.FTUnitSectCode AND Cut.FTColorway = Sew.FTColorway AND Cut.FTSizeBreakDown = Sew.FTSizeBreakDown 	and Cut.FTPOLineItemNo = Sew.FTPOLineItemNo"

            'If FTStartDateScan.Text = "" And FTEndDateScan.Text = "" Then

            '    _Qry &= vbCrLf & "    LEFT OUTER JOIN ( "

            'Else
            _Qry &= vbCrLf & "   INNER JOIN ( "

            'End If


            '----------Start Sew Out

            _Qry &= vbCrLf & " SELECT * FROM #TabDataSewOut "

            '_Qry &= vbCrLf & "  Select   FTOrderNo,FTUnitSectCode, FTColorway, FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,FTPOLineItemNo ,FTBarcodeNo    From ( "

            '_Qry &= vbCrLf & "   SELECT P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNQuantity) AS FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeNo"
            ''_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS SC"
            ''_Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON SC.FNHSysUnitSectId = US.FNHSysUnitSectId "

            '_Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
            '_Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            '_Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            '_Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            '_Qry &= vbCrLf & "      INNER JOIN #TabDataBD As X ON X.FTOrderNo = P.FTOrderNo AND X.FTColorway=B.FTColorway AND X.FTSizeBreakDown=B.FTSizeBreakDown        "

            '_Qry &= vbCrLf & "   WHERE P.FTOrderNo <>'' AND   P.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  "

            'If FTStartDateScan.Text <> "" Or FTEndDateScan.Text <> "" Then

            '    If FTStartDateScan.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SC.FDDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScan.Text) & "' "
            '    End If

            '    If FTEndDateScan.Text <> "" Then
            '        _Qry &= vbCrLf & "   AND SC.FDDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScan.Text) & "' "
            '    End If

            'End If

            '_Qry &= vbCrLf & "   GROUP BY P.FTOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo , SC.FTBarcodeNo) AS T "
            '_Qry &= vbCrLf & "  GROUP BY FTOrderNo,FTUnitSectCode,FTColorway,FTSizeBreakDown , FTPOLineItemNo ,FTBarcodeNo "

            '----------END Sew Out
            _Qry &= vbCrLf & "    ) AS SewOut ON Sew.FTOrderNo = SewOut.FTOrderNo  AND Sew.FTUnitSectCodeSew = SewOut.FTUnitSectCode  AND Sew.FTColorway = SewOut.FTColorway AND Sew.FTSizeBreakDown = SewOut.FTSizeBreakDown and Sew.FTPOLineItemNo = SewOut.FTPOLineItemNo and Sew.FTBarcodeNo = SewOut.FTBarcodeNo"

            _Qry &= vbCrLf & "    LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "  SELECT SC.FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown, SUM(SC.FNScanQuantity) AS FNQuantity , B.FTPOLineItemNo , SC.FTBarcodeNo"
            _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS SC WITH(NOLOCK) "
            _Qry &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS PC WITH(NOLOCK) ON SC.FTPackNo = PC.FTPackNo AND SC.FNCartonNo = PC.FNCartonNo "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo and SC.FTColorway = B.FTColorway and SC.FTSizeBreakDown = B.FTSizeBreakDown"

            _Qry &= vbCrLf & "     INNER JOIN #TabDataBD As X ON X.FTOrderNo = SC.FTOrderNo AND X.FTColorway=SC.FTColorway AND X.FTSizeBreakDown=SC.FTSizeBreakDown      "

            _Qry &= vbCrLf & "   WHERE   SC.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)  AND  PC.FTState='1' "
            _Qry &= vbCrLf & "   GROUP BY SC.FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown , B.FTPOLineItemNo , SC.FTBarcodeNo  "
            _Qry &= vbCrLf & "   ) AS PAC ON SewOut.FTOrderNo = PAC.FTOrderNo AND SewOut.FTPOLineItemNo = PAC.FTPOLineItemNo  AND SewOut.FTColorway = PAC.FTColorway AND SewOut.FTSizeBreakDown = PAC.FTSizeBreakDown "
            _Qry &= vbCrLf & "   and SewOut.FTBarcodeNo = PAC.FTBarcodeNo "
            _Qry &= vbCrLf & "  ) AS BM ON A.FTOrderNo = BM.FTOrderNo   AND B.FTColorway = BM.FTColorway AND B.FTSizeBreakDown = BM.FTSizeBreakDown and C.FTPOLineItemNo = BM.FTPOLineItemNo "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS SSB WITH(NOLOCK) ON B.FTSizeBreakDown = SSB.FTMatSizeCode "
            _Qry &= vbCrLf & " WHERE A.FTOrderNo <> ''"

            _Qry &= vbCrLf & " AND   A.FTOrderNo IN (SELECT DISTINCT FTOrderNo FROM #TabOrder)    "

            'If FNHSysBuyId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            'End If

            'If FNHSysStyleId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            'End If

            'If FTOrderNo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            'End If

            'If FTOrderNoTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            'End If

            'If FTCustomerPO.Text <> "" Then
            '    _Qry &= vbCrLf & " AND A.FTPORef >='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
            'End If

            'If FTCustomerPOTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND A.FTPORef <='" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'  "
            'End If

            'If FTStartShipment.Text <> "" Or FTStartShipment.Text <> "" Then
            '    _Qry &= vbCrLf & " AND  A.FTOrderNo In ( "
            '    _Qry &= vbCrLf & " SELECT DISTINCT  FTOrderNo "
            '    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
            '    _Qry &= vbCrLf & " WHERE FTOrderNo <>'' "
            '    If FTStartShipment.Text <> "" Then
            '        _Qry &= vbCrLf & " AND SS.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
            '    End If
            '    If FTEndShipment.Text <> "" Then
            '        _Qry &= vbCrLf & " AND SS.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
            '    End If
            '    _Qry &= vbCrLf & ") "
            'End If

            _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo,B.FTColorWay,Isnull(C.FTPOLineItemNo,''),SSB.FNMatSizeSeq,ISNULL(BM.FTUnitSectCode,''),ISNULL(BM.FTUnitSectCodeSew,'')  "
            _Qry &= vbCrLf & "  DROP TABLE #TabData"
            _Qry &= vbCrLf & "  DROP TABLE #TabDataBD "
            _Qry &= vbCrLf & "  DROP TABLE #TabOrder "
            _Qry &= vbCrLf & "  DROP TABLE #TabDataLC"
            _Qry &= vbCrLf & "  DROP TABLE #TabDataCut "
            _Qry &= vbCrLf & "  DROP TABLE #TabDataSendSupl"
            _Qry &= vbCrLf & "  DROP TABLE #TabDataRcvSupl"
            _Qry &= vbCrLf & "  DROP TABLE #TabDataSMK"
            _Qry &= vbCrLf & "  DROP TABLE #TabDataSew"
            _Qry &= vbCrLf & "  DROP TABLE  #TabDataSewOut"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcdetailcolorsizelineg.DataSource = _dt
            Call InitialGridMergCell()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub InitialGridNotSort()
        For Each c As GridColumn In ogvdetailcolorsizelineg.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next
    End Sub

    Private Sub InitialGridMergCell()
        For Each c As GridColumn In ogvdetailcolorsizelineg.Columns
            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTPORef", "FTOrderNo", "FTCmpCode", "FTCmpName", "FDShipDate", "FTColorway", "FTSizeBreakDown",
                       "FTUnitSectCodeCut", "FTUnitSectCodeSew",
                     "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FNQuantity", "FNCutQuantity",
                     "FNSendSuplQuantity", "FNRcvSuplQuantity", "FNSPMKQuantity", "FNSPMKQuantityBal", "FNBalSuplQuantity", "FNCutBalQuantity", "FNBalCutQuantity",
                      "FNQtyEmbroidery", "FNRcvQtyEmbroidery", "FNBalQtyEmbroidery",
                         "FNQtyPrint", "FNRcvQtyPrint", "FNBalQtyPrint",
                          "FNQtyHeat", "FNRcvQtyHeat", "FNBalQtyHeat",
                          "FNQtyLaser", "FNRcvQtyLaser", "FNBalQtyLaser",
                          "FNQtyPadPrint", "FNRcvQtyPadPrint", "FNBalQtyPadPrint",
                          "FNQtyWindow", "FNRcvQtyWindow", "FNBalQtyWindow"

                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        'If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTCustomerPO.Text <> "" And FTCustomerPO.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTCustomerPOTo.Text <> "" And FTCustomerPOTo.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTSSendSuplDate.Text <> "" And FTESendSuplDate.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTSRcvSuplDate.Text <> "" And FTERcvSuplDate.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTSSMKDate.Text <> "" And FTESMKDate.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTStartShipment.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTEndShipment.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTStartDateScanIn.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTEndDateScanIn.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTStartDateScan.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTEndDateScan.Text <> "" Then
        '    _Pass = True
        'End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call InitialGridNotSort()
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetailcolorsizelineg)
            StateCal = False
            _StateQtyBySPM = StateQtyBySPM()
            Me.FTSSMKDate.Text = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub

    Private Function StateQtyBySPM() As Boolean
        Try
            Dim _Cmd As String = " Select  Top 1  isnull(FTStateProdSMKToCutQty,0) As FTStateProdSMKToCutQty  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEConfig With(NOLOCK)"
            Return Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "0")) = 1
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        'If VerifyData() Then

        Dim _Qry As String = ""

        _Qry = "Select TOP 1 FTStateProdSMKToCutQty "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig As S With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) & "'"

        _FTStateProdSMKToCutQty = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

        Call LoadData()

        'End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetailcolorsizelineg)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ogvdetailcolorsizelineg_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvdetailcolorsizelineg.CellMerge
        Try
            With Me.ogvdetailcolorsizelineg
                Select Case e.Column.FieldName
                    Case "FTUnitSectCodeSew", "FTUnitSectCodeCut", "FDShipDate", "FTCmpCode", "FTCmpName", "FTOrderNo"

                        If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FNQuantity", "FNQuantityExtra", "FNGrandQuantity"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString) _
                           And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FNGarmentQtyTest", "FNCutBalQuantity", "FNBalCutQuantity", "FNSPMKQuantity", "FNSPMKQuantityBal", "FNExpQty", "FNFGInQty", "FNFGBalQty", "FNExpBalQty"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTPOLineItemNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPOLineItemNo").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString) _
                           And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FNQtyEmbroidery", "FNRcvQtyEmbroidery", "FNBalQtyEmbroidery" _
                        , "FNQtyPrint", "FNRcvQtyPrint", "FNBalQtyPrint" _
                         , "FNQtyHeat", "FNRcvQtyHeat", "FNBalQtyHeat" _
                         , "FNQtyLaser", "FNRcvQtyLaser", "FNBalQtyLaser" _
                         , "FNQtyPadPrint", "FNRcvQtyPadPrint", "FNBalQtyPadPrint" _
                         , "FNQtyWindow", "FNRcvQtyWindow", "FNBalQtyWindow", "FNSendSuplQuantity", "FNRcvSuplQuantity", "FNBalSuplQuantity"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTPOLineItemNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPOLineItemNo").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString) _
                           And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTColorway"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                         And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTSizeBreakDown"
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
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetailcolorsizelineg_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetailcolorsizelineg.RowCellStyle
        Try
            'With Me.ogvdetailcolorsize
            '    Select Case e.Column.FieldName
            '        Case "FNCutQuantity"
            '            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) Then
            '                e.Appearance.BackColor = Drawing.Color.OrangeRed
            '            Else
            '                e.Appearance.BackColor = Drawing.Color.GreenYellow
            '            End If
            '        Case "FNRcvSuplQuantity"
            '            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) > 0 Then
            '                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) Then
            '                    e.Appearance.BackColor = Drawing.Color.OrangeRed
            '                Else
            '                    e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                End If
            '            End If

            '        Case "FNSPMKQuantity"
            '            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
            '                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) Then
            '                    e.Appearance.BackColor = Drawing.Color.OrangeRed
            '                Else
            '                    e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                End If
            '            End If
            '        Case "FNSewQuantity"
            '            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) > 0 Then
            '                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) Then
            '                    e.Appearance.BackColor = Drawing.Color.OrangeRed
            '                Else
            '                    e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                End If
            '            End If

            '        Case "FNSewOutQuantity"
            '            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
            '                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) Then
            '                    e.Appearance.BackColor = Drawing.Color.OrangeRed
            '                Else
            '                    e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                End If
            '            End If

            '        Case "FNPackQuantity"
            '            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
            '                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNPackQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) Then
            '                    e.Appearance.BackColor = Drawing.Color.OrangeRed

            '                Else
            '                    e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                End If
            '            End If
            '        Case "FNBalCutQuantity", "FNBalSuplQuantity", "FNBalSewQuantity", "FNBalPackQuantity", "FNCutBalQuantity"
            '            If Double.Parse(.GetRowCellValue(e.RowHandle, e.Column.FieldName)) > 0 Then
            '                e.Appearance.BackColor = Drawing.Color.OrangeRed
            '            Else

            '                Select Case e.Column.FieldName
            '                    Case "FNBalCutQuantity"
            '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNTableCut")) > 0 Then
            '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                        End If
            '                    Case "FNBalSuplQuantity"
            '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) > 0 Then
            '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                        End If
            '                    Case "FNBalSewQuantity"
            '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
            '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                        End If
            '                    Case "FNBalPackQuantity"
            '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
            '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                        End If
            '                    Case "FNCutBalQuantity"
            '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) > 0 Then
            '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
            '                        End If
            '                End Select

            '            End If

            '    End Select
            'End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try

            Dim _FileName As String = ""
                Dim folderDlg As New SaveFileDialog
                With folderDlg
                    .Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx|Excel Worksheets(97-2003)|*.xls"
                    .FilterIndex = 1
                    Dim dr As DialogResult = .ShowDialog()
                    If (dr = System.Windows.Forms.DialogResult.OK) Then
                        Dim _Spls As New HI.TL.SplashScreen("Please Wait.....", "Export Data From File ")

                        Dim path As String = .FileNames(0).ToString
                        Dim _Strm As Stream = New MemoryStream()

                        Select Case Me.ogcdetailcolorsizeline.SelectedTabPageIndex
                            Case 0
                                ogcdetailcolorsizelineg.ExportToXlsx(path)

                            Case Else
                                ogcmonthly.ExportToXlsx(path)

                        End Select


                        Process.Start(path)
                        _Spls.Close()
                    End If
                End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub ogvmonthly_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvmonthly.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FTMonthly"))
                If category = "BF" Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
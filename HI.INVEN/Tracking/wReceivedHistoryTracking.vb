Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports System

Public Class wReceivedHistoryTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitialGridMergCell()


    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "BAL|RCVTotal" 'FNRCVQty|FNTWQty|FNISSQty|FNSNTQty|FNRTSQty|RTSISQty|TCQty|

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "BAL|RCVTotal" 'FNRCVQty|FNTWQty|FNISSQty|FNSNTQty|FNRTSQty|RTSISQty|TCQty|

 


        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
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
    Private GrpSum As Integer = 0
    Private _RowHandleHold As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitStartValue()
            End If
            Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString.ToUpper
                Case "FNQuantity".ToUpper
                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsTotalSummary Then
                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatCode").ToString Or _
                                                           ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatColorCode").ToString Or _
                                                            ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatSizeCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatSizeCode").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString) Or e.RowHandle = _RowHandleHold Then
                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum
                    End If

                    
                Case "FNRCVQty".ToUpper

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsTotalSummary Then

                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                'If ogvtime.GetRowCellValue(e.RowHandle, "FNRceceiveTypeState").ToString <> "99" Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                      ogvtime.GetRowCellValue(e.RowHandle, "FTReceiveNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReceiveNo").ToString Or _
                                                       ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                                        ogvtime.GetRowCellValue(e.RowHandle, "FNHSysRawMatId").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FNHSysRawMatId").ToString) Or e.RowHandle = _RowHandleHold Then

                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                                'End If

                            End If

                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum
                    End If

                Case "FNTWQty".ToUpper

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsTotalSummary Then
                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatCode").ToString Or _
                                                           ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatColorCode").ToString Or _
                                                            ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatSizeCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatSizeCode").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTReceiveNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReceiveNo").ToString Or _
                                                               ogvtime.GetRowCellValue(e.RowHandle, "FTTransferWHNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTTransferWHNo").ToString) Or e.RowHandle = _RowHandleHold Then

                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum
                    End If

                Case "FNISSQty".ToUpper

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then

                        If e.IsTotalSummary Then
                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatCode").ToString Or _
                                                           ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatColorCode").ToString Or _
                                                            ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatSizeCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatSizeCode").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTReceiveNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReceiveNo").ToString Or _
                                                               ogvtime.GetRowCellValue(e.RowHandle, "FTIssueNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTIssueNo").ToString Or _
                                                               ogvtime.GetRowCellValue(e.RowHandle, "FTBarcodeNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTBarcodeNo").ToString
                                                               ) Or e.RowHandle = _RowHandleHold Then

                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum

                    End If

                Case "FNSNTQty".ToUpper
                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsTotalSummary Then
                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatCode").ToString Or _
                                                           ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatColorCode").ToString Or _
                                                            ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatSizeCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatSizeCode").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTReceiveNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReceiveNo").ToString Or _
                                                               ogvtime.GetRowCellValue(e.RowHandle, "FTSaleAndTerminateNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTSaleAndTerminateNo").ToString Or _
                                                                ogvtime.GetRowCellValue(e.RowHandle, "FTBarcodeNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTBarcodeNo").ToString) Or e.RowHandle = _RowHandleHold Then

                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum
                    End If
                Case "TCQty".ToUpper
                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsTotalSummary Then
                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatCode").ToString Or _
                                                           ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatColorCode").ToString Or _
                                                            ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatSizeCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatSizeCode").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTReceiveNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReceiveNo").ToString Or _
                                                               ogvtime.GetRowCellValue(e.RowHandle, "FTTransferCenterNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTTransferCenterNo").ToString Or _
                                                                ogvtime.GetRowCellValue(e.RowHandle, "FTBarcodeNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTBarcodeNo").ToString) Or e.RowHandle = _RowHandleHold Then

                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum
                    End If
                Case "RTSISQty".ToUpper
                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsTotalSummary Then
                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatCode").ToString Or _
                                                           ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatColorCode").ToString Or _
                                                            ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatSizeCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatSizeCode").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTReceiveNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReceiveNo").ToString Or _
                                                               ogvtime.GetRowCellValue(e.RowHandle, "FTReturnSuplNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReturnSuplNo").ToString Or _
                                                                ogvtime.GetRowCellValue(e.RowHandle, "FTBarcodeNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTBarcodeNo").ToString) Or e.RowHandle = _RowHandleHold Then

                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum
                    End If
                Case "FNRTSQty".ToUpper
                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsTotalSummary Then
                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                If (ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatCode").ToString Or _
                                                           ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatColorCode").ToString Or _
                                                            ogvtime.GetRowCellValue(e.RowHandle, "FTRawMatSizeCode").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRawMatSizeCode").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTPurchaseNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString Or _
                                                             ogvtime.GetRowCellValue(e.RowHandle, "FTReceiveNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTReceiveNo").ToString Or _
                                                               ogvtime.GetRowCellValue(e.RowHandle, "FTRTSNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTRTSNo").ToString Or _
                                                                ogvtime.GetRowCellValue(e.RowHandle, "FTBarcodeNo").ToString <> ogvtime.GetRowCellValue(_RowHandleHold, "FTBarcodeNo").ToString) Or e.RowHandle = _RowHandleHold Then

                                    totalSum = totalSum + CDbl("0" & e.FieldValue.ToString)
                                End If
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                        e.TotalValue = totalSum
                    End If

            End Select
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

    Private Sub LoadData_Hold()
        Dim _Qry As String = ""
        Dim _dt As DataTable


        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = " SELECT  FTPurchaseNo"
        _Qry &= vbCrLf & ",FDPurchaseDate"
        _Qry &= vbCrLf & ",FTPurchaseBy"
        _Qry &= vbCrLf & ",FDDeliveryDate"
        _Qry &= vbCrLf & ",FTStateSendApp"
        _Qry &= vbCrLf & ",FTSendAppDate"
        _Qry &= vbCrLf & ",FTStateSuperVisorApp"
        _Qry &= vbCrLf & ",FTStateSuperVisorReject"
        _Qry &= vbCrLf & ",FTSuperVisorAppDate"
        _Qry &= vbCrLf & ",FTStateManagerApp"
        _Qry &= vbCrLf & ",FTStateManagerReject"
        _Qry &= vbCrLf & ",FTSuperManagerAppDate"
        _Qry &= vbCrLf & ",FNHSysSuplId"
        _Qry &= vbCrLf & ",FTSuplCode"
        _Qry &= vbCrLf & ",FNHSysRawMatId"
        _Qry &= vbCrLf & ",FNHSysUnitId"
        _Qry &= vbCrLf & ",FTRawMatCode"
        _Qry &= vbCrLf & ",FTRawMatColorCode"
        _Qry &= vbCrLf & ",FTRawMatSizeCode"
        _Qry &= vbCrLf & ",FTRawMatName"
        _Qry &= vbCrLf & ",FTSuplName"
        _Qry &= vbCrLf & ",FTUnitCode"
        _Qry &= vbCrLf & ",FNPOQuantity"
        _Qry &= vbCrLf & ",FNRcvQuantity"
        _Qry &= vbCrLf & ",FNRTsQuantity"
        _Qry &= vbCrLf & ",((FNPOQuantity - FNRcvQuantity) + FNRTsQuantity) AS FNPOBalQuantity"
        _Qry &= vbCrLf & "  FROM ( Select A.FTPurchaseNo,A.FTPurchaseBy"
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FDPurchaseDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FDPurchaseDate) ,103) ELSE '' END AS FDPurchaseDate "
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FDDeliveryDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FDDeliveryDate) ,103) ELSE '' END AS FDDeliveryDate "
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateSendApp ='1' THEN '1' ELSE '0' END AS FTStateSendApp"
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FTSendAppDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FTSendAppDate) ,103) ELSE '' END AS FTSendAppDate "
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateSuperVisorApp ='1' THEN '1' ELSE '0' END AS FTStateSuperVisorApp"
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateSuperVisorApp ='2' THEN '1' ELSE '0' END AS FTStateSuperVisorReject"
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FTSuperVisorAppDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FTSuperVisorAppDate) ,103) ELSE '' END AS FTSuperVisorAppDate "
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateManagerApp ='1' THEN '1' ELSE '0' END AS FTStateManagerApp"
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateManagerApp ='2' THEN '1' ELSE '0' END AS FTStateManagerReject"
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FTSuperManagerAppDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FTSuperManagerAppDate) ,103) ELSE '' END AS FTSuperManagerAppDate "
        _Qry &= vbCrLf & "  ,A.FNHSysSuplId,Sup.FTSuplCode"
        _Qry &= vbCrLf & "  ,A.FNHSysRawMatId"
        _Qry &= vbCrLf & "  ,A.FNHSysUnitId"
        _Qry &= vbCrLf & "  ,M.FTRawMatCode"
        _Qry &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & "  ,ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",M.FTRawMatNameTH AS FTRawMatName"
            _Qry &= vbCrLf & ",Sup.FTSuplNameTH AS FTSuplName"
        Else
            _Qry &= vbCrLf & ",M.FTRawMatNameEN AS FTRawMatName"
            _Qry &= vbCrLf & ",Sup.FTSuplNameEN AS FTSuplName"
        End If

        _Qry &= vbCrLf & "  ,ISNULL(U.FTUnitCode,'') AS FTUnitCode"
        _Qry &= vbCrLf & "  ,A.FNQuantity AS FNPOQuantity"
        _Qry &= vbCrLf & "  ,A.FNRcvQuantity AS FNRcvQuantity"
        _Qry &= vbCrLf & "  ,Convert(Numeric(18," & HI.ST.Config.QtyDigit & "),(A.FNRTsQuantity * ISNULL(UV.FNRateFrom,1)) / CASE WHEN ISNULL(UV.FNRateTo,1) =0 THEN 1 ELSE ISNULL(UV.FNRateTo,1) END)  AS FNRTsQuantity"
        _Qry &= vbCrLf & "  ,(SELECT TOP 1 FNRevisedSeq"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS PR  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo=A.FTPurchaseNo"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq DESC"
        _Qry &= vbCrLf & " ) AS FNRevisedSeq"
        _Qry &= vbCrLf & "  ,(SELECT TOP 1 CASE WHEN ISDATE(FTRevisedDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTRevisedDate) ,103) ELSE '' END FTRevisedDate"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS PR  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo=A.FTPurchaseNo"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq DESC"
        _Qry &= vbCrLf & " ) AS FTRevisedDate"
        _Qry &= vbCrLf & "  ,(SELECT TOP 1 FTPurchaseRevisedBy"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS PR  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo=A.FTPurchaseNo"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq DESC"
        _Qry &= vbCrLf & " ) AS FTPurchaseRevisedBy"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & "  (SELECT        A.FTPurchaseNo, A.FDPurchaseDate,A.FTPurchaseBy, A.FDDeliveryDate, A.FTStateSendApp, A.FTSendAppDate, A.FTStateSuperVisorApp, A.FTSuperVisorAppDate, A.FTStateManagerApp, "
        _Qry &= vbCrLf & "   A.FTSuperManagerAppDate, A.FNHSysSuplId, A.FNHSysRawMatId, A.FNQuantity, A.FNHSysUnitId"
        _Qry &= vbCrLf & " 	 , ISNULL(B.FNQuantity,0) AS FNRcvQuantity,(ISNULL(C.FNQuantity,0) + ISNULL(D.FNQuantity,0)) AS FNRTsQuantity"
        _Qry &= vbCrLf & "  FROM            (SELECT        H.FTPurchaseNo, H.FDPurchaseDate,H.FTPurchaseBy, H.FDDeliveryDate, H.FTStateSendApp, H.FTStateSuperVisorApp, H.FTStateManagerApp, D.FNHSysRawMatId, H.FNHSysSuplId, SUM(D.FNQuantity) "
        _Qry &= vbCrLf & "     AS FNQuantity, H.FTSendAppDate, H.FTSuperVisorAppDate, H.FTSuperManagerAppDate, D.FNHSysUnitId"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  WITH(NOLOCK)   INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS D  WITH(NOLOCK)   ON H.FTPurchaseNo = D.FTPurchaseNo"
        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Sup WITH(NOLOCK) ON H.FNHSysSuplId = Sup.FNHSysSuplId "

        _Qry &= vbCrLf & " WHERE  H.FTPurchaseNo<>'' "

        If Me.FTStartPurchaseDate.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartPurchaseDate.Text) & "' "
        End If

        If Me.FTEndPurchaseDate.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndPurchaseDate.Text) & "' "
        End If

        If Me.FTStartDelivery.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDDeliveryDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDelivery.Text) & "' "
        End If

        If Me.FTEndDelivery.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDDeliveryDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDelivery.Text) & "' "
        End If

        If Me.FTPurchaseNo.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        End If

        If Me.FTPurchaseNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "' "
        End If

        If Me.FNHSysSuplId.Text <> "" Then
            _Qry &= vbCrLf & " AND  Sup.FTSuplCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
        End If

        If Me.FNHSysSuplIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  Sup.FTSuplCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplIdTo.Text) & "' "
        End If

        _Qry &= vbCrLf & "    GROUP BY H.FTPurchaseNo, H.FDPurchaseDate,H.FTPurchaseBy, H.FDDeliveryDate, H.FTStateSendApp, H.FTStateSuperVisorApp, H.FTStateManagerApp, D.FNHSysRawMatId, H.FNHSysSuplId, H.FTSendAppDate, "
        _Qry &= vbCrLf & "     H.FTSuperVisorAppDate, H.FTSuperManagerAppDate, D.FNHSysUnitId) AS A LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    (SELECT        A.FTPurchaseNo, B.FNHSysRawMatId, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS B  WITH(NOLOCK)   ON A.FTReceiveNo = B.FTReceiveNo"
        _Qry &= vbCrLf & "    GROUP BY A.FTPurchaseNo, B.FNHSysRawMatId) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN ("
        _Qry &= vbCrLf & " 	SELECT        A.FTPurchaseNo, C.FNHSysRawMatId, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " 	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B  WITH(NOLOCK)  ON A.FTReturnSuplNo = B.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK)  ON B.FTBarcodeNo = C.FTBarcodeNo"
        _Qry &= vbCrLf & " 	GROUP BY A.FTPurchaseNo, C.FNHSysRawMatId"
        _Qry &= vbCrLf & "   ) AS C ON A.FTPurchaseNo = C.FTPurchaseNo AND A.FNHSysRawMatId = C.FNHSysRawMatId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN ("
        _Qry &= vbCrLf & " 	SELECT        A.FTPurchaseNo, C.FNHSysRawMatId, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " 	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & " 	     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS B WITH(NOLOCK)  ON A.FTReturnSuplNo = B.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK)  ON B.FTBarcodeNo = C.FTBarcodeNo"
        _Qry &= vbCrLf & " 	GROUP BY A.FTPurchaseNo, C.FNHSysRawMatId"
        _Qry &= vbCrLf & "    ) AS D ON A.FTPurchaseNo = D.FTPurchaseNo AND A.FNHSysRawMatId = D.FNHSysRawMatId"
        _Qry &= vbCrLf & "   ) AS A   "
        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH(NOLOCK) ON A.FNHSysRawMatId = M.FNHSysRawMatId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH(NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH(NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitId = U.FNHSysUnitId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UV WITH(NOLOCK) ON A.FNHSysUnitId = UV.FNHSysUnitId AND M.FNHSysUnitId =UV.FNHSysUnitIdTo "
        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Sup WITH(NOLOCK) ON A.FNHSysSuplId = Sup.FNHSysSuplId "
        _Qry &= vbCrLf & " ) AS A"


        _Qry &= vbCrLf & " ORDER BY  FTPurchaseNo,FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Me.ogdtime.DataSource = _dt.Copy
        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Sub LoadData()

        Dim _Qry As String = ""
        Dim _oDt As DataTable
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            StateCal = False
            _Qry = "SELECT   FTBarcodeNo, FDReceiveDate AS DateRCV, FTPurchaseNo,FTReceiveBy, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRawMatCode,  FTRawMatColorCode, FTRawMatSizeCode, "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

                _Qry &= vbCrLf & "FTRawMatNameTH as FTRawMatName ,FTRawMatColorNameTH as FTRawMatColorName ,FTUnitNameTH as FTUnitName  , FTSuplNameTH as FTSuplName,FTRawMatSizeNameTH as FTRawMatSizeName, FTUnitNameTHRCV as FTUnitNameRCV  "
                _Qry &= vbCrLf & ",FNRceceiveTypeTH AS FTReceiveType"
                _Qry &= vbCrLf & ",FNReturnSuplTypeNameTH AS FNReturnSuplTypeName"
                _Qry &= vbCrLf & ",FNReturnSuplTypeNameTH2 AS FNReturnSuplTypeName2"
            Else

                _Qry &= vbCrLf & " FTRawMatNameEN as FTRawMatName ,FTRawMatColorNameEN as FTRawMatColorName , FTUnitNameEN as FTUnitName, FTSuplNameEN as FTSuplName, FTRawMatSizeNameEN  as FTRawMatSizeName , FTUnitNameENRCV as FTUnitNameRCV"
                _Qry &= vbCrLf & ",FNRceceiveTypeEN AS FTReceiveType"
                _Qry &= vbCrLf & ",FNReturnSuplTypeNameEN AS FNReturnSuplTypeName"
                _Qry &= vbCrLf & ",FNReturnSuplTypeNameEN2 AS FNReturnSuplTypeName2"

            End If

            _Qry &= vbCrLf & "  ,FTUnitCode,  FTReceiveNo, FNRCVQty, CASE WHEN ISDATE(FDReceiveDate) = 1 THEN Convert(varchar(10),Convert(datetime,FDReceiveDate) ,103) ELSE '' END AS FDReceiveDate, FTInvoiceNo, TotalRCV, FTOrderNo, FTTransferWHNo,FTRTSNo,"
            _Qry &= vbCrLf & "   FNTWQty, FTIssueNo, FNISSQty, FTSaleAndTerminateNo, FNSNTQty, FTTransferCenterNo, FNRTSQty, FTReturnSuplNo, RTSISQty, BAL, TCQty, FDPurchaseDate, FDDeliveryDate,FTSuplCode, RCVTotal"
            _Qry &= vbCrLf & ", FTUnitCodeRCV"
            _Qry &= vbCrLf & ", FNRceceiveTypeState"
            _Qry &= vbCrLf & ", FNReturnSuplType"
            _Qry &= vbCrLf & ", FNReturnSuplType2"
            _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.V_Received_History "
            _Qry &= vbCrLf & " WHERE FTPurchaseNo <> ''" ' and FTReceiveNo <> ''
            If Me.FTStartPurchaseDate.Text <> "" Then
                _Qry &= vbCrLf & " AND  FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartPurchaseDate.Text) & "' "
            End If

            If Me.FTEndPurchaseDate.Text <> "" Then
                _Qry &= vbCrLf & " AND  FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndPurchaseDate.Text) & "' "
            End If

            If Me.FTStartDelivery.Text <> "" Then
                _Qry &= vbCrLf & " AND  FDDeliveryDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDelivery.Text) & "' "
            End If

            If Me.FTEndDelivery.Text <> "" Then
                _Qry &= vbCrLf & " AND  FDDeliveryDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDelivery.Text) & "' "
            End If

            If Me.SFTReceiveDate.Text <> "" Then
                _Qry &= vbCrLf & " AND  FDReceiveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTReceiveDate.Text) & "' "
            End If

            If Me.EFTReceiveDate.Text <> "" Then
                _Qry &= vbCrLf & " AND  FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTReceiveDate.Text) & "' "
            End If

            If Me.FTPurchaseNo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
            End If

            If Me.FTPurchaseNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "' "
            End If

            If Me.FNHSysSuplId.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTSuplCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
            End If

            If Me.FNHSysSuplIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  FTSuplCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplIdTo.Text) & "' "
            End If

            _Qry &= vbCrLf & "ORDER BY FTPurchaseNo ASC , FTOrderNo ASC , FTRawMatCode ASC ,FTRawMatColorCode ASC,FTRawMatSizeCode ASC , DateRCV DESC  "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            Me.ogdtime.DataSource = _oDt.Copy
            _oDt.Dispose()
            _Spls.Close()

            _RowDataChange = False

        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTStartPurchaseDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndPurchaseDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTStartDelivery.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndDelivery.Text <> "" Then
            _Pass = True
        End If

        If Me.SFTReceiveDate.Text <> "" Then
            _Pass = True
        End If

        If Me.EFTReceiveDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNoTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysSuplId.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysSuplIdTo.Text <> "" Then
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

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            StateCal = False
            Call InitGrid()
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

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs) Handles ogbheader.Click

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvtime
            'If .RowCount <= 0 Then Exit Sub
            'If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub


            Dim _Qry As String = ""

            _Qry = " {V_Received_History.FTPurchaseNo} <> '' AND  {V_Received_History.FTReceiveNo} <> '' "
            If Me.FTStartPurchaseDate.Text <> "" Then

                _Qry &= " AND {V_Received_History.FDPurchaseDate} >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartPurchaseDate.Text) & "' "
            End If

            If Me.FTEndPurchaseDate.Text <> "" Then

                _Qry &= " AND  {V_Received_History.FDPurchaseDate} <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndPurchaseDate.Text) & "' "
            End If

            If Me.FTStartDelivery.Text <> "" Then

                _Qry &= "  AND  {V_Received_History.FDDeliveryDate}  >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDelivery.Text) & "' "
            End If

            If Me.FTEndDelivery.Text <> "" Then

                _Qry &= " AND   {V_Received_History.FDDeliveryDate}  <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDelivery.Text) & "' "
            End If

            If Me.FTPurchaseNo.Text <> "" Then

                _Qry &= " AND   {V_Received_History.FTPurchaseNo} >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
            End If

            If Me.FTPurchaseNoTo.Text <> "" Then

                _Qry &= " AND  {V_Received_History.FTPurchaseNo} <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "' "
            End If

            If Me.FNHSysSuplId.Text <> "" Then

                _Qry &= " AND {V_Received_History.FTSuplCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
            End If

            If Me.FNHSysSuplIdTo.Text <> "" Then

                _Qry &= " AND {V_Received_History.FTSuplCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplIdTo.Text) & "' "
            End If



            With New HI.RP.Report
 
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "ReceiveHistory.rpt"
                .AddParameter("DateS", FTStartDelivery.Text)
                .AddParameter("DateE", FTEndDelivery.Text)


                .Formular = _Qry
                .Preview()

                ''HI.ST.Lang.Language = _tmplang
            End With

        End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub InitialGridMergCell()
        For Each c As GridColumn In ogvtime.Columns
            Select Case c.FieldName.ToString
                Case "FTPurchaseNo", "FTReceiveBy", "FTRawMatCode", "FTRawMatName", "FTRawMatColorCode", "FTRawMatSizeCode", "FTSuplCode", "FTSuplName", "FTUnitCode", "FTReceiveNo", "FTInvoiceNo", "FTUnitCodeRCV", _
                    "FTSaleAndTerminateNo", "FTOrderNo", "FTTransferWHNo", "FTIssueNo", "FDReceiveDate", "FNQuantity", "FTUnitNameRCV", "FTTransferCenterNo", "FTReturnSuplNo", "FTRTSNo"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case "FNTWQty", "FNISSQty", "FNRTSQty", "RTSISQty", "TCQty", "FNSNTQty", "FNRCVQty"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub


    Private Sub ogvtime_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvtime.CellMerge
        Try
            With Me.ogvtime
                If .GetRowCellValue(e.RowHandle1, "FTPurchaseNo,FTReceiveBy").ToString = .GetRowCellValue(e.RowHandle2, "FTPurchaseNo,FTReceiveBy").ToString Then
                    If e.Column.FieldName = "FTRawMatSizeCode" Then
                        If .GetRowCellValue(e.RowHandle1, "FTRawMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatCode").ToString And _
                            .GetRowCellValue(e.RowHandle1, "FTRawMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatColorCode").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True

                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "FNQuantity" Then
                        If .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTRawMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatCode").ToString And _
                             .GetRowCellValue(e.RowHandle1, "FTRawMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatColorCode").ToString And _
                                .GetRowCellValue(e.RowHandle1, "FTRawMatSizeCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatSizeCode").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "FNRCVQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FTReceiveNo").ToString = .GetRowCellValue(e.RowHandle2, "FTReceiveNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "FNTWQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FTTransferWHNo").ToString = .GetRowCellValue(e.RowHandle2, "FTTransferWHNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString And
                           .GetRowCellValue(e.RowHandle1, "FTBarcodeNo").ToString = .GetRowCellValue(e.RowHandle2, "FTBarcodeNo").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "FNISSQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FTIssueNo").ToString = .GetRowCellValue(e.RowHandle2, "FTIssueNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString And
                           .GetRowCellValue(e.RowHandle1, "FTBarcodeNo").ToString = .GetRowCellValue(e.RowHandle2, "FTBarcodeNo").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "FNRTSQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FTRTSNo").ToString = .GetRowCellValue(e.RowHandle2, "FTRTSNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString And
                           .GetRowCellValue(e.RowHandle1, "FTBarcodeNo").ToString = .GetRowCellValue(e.RowHandle2, "FTBarcodeNo").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "RTSISQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FTReturnSuplNo").ToString = .GetRowCellValue(e.RowHandle2, "FTReturnSuplNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString And
                           .GetRowCellValue(e.RowHandle1, "FTBarcodeNo").ToString = .GetRowCellValue(e.RowHandle2, "FTBarcodeNo").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "TCQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FTransferCenterNo").ToString = .GetRowCellValue(e.RowHandle2, "FTransferCenterNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString And
                           .GetRowCellValue(e.RowHandle1, "FTBarcodeNo").ToString = .GetRowCellValue(e.RowHandle2, "FTBarcodeNo").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                    If e.Column.FieldName = "FNSNTQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FTSaleAndTerminateNo").ToString = .GetRowCellValue(e.RowHandle2, "FTSaleAndTerminateNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString And
                           .GetRowCellValue(e.RowHandle1, "FTBarcodeNo").ToString = .GetRowCellValue(e.RowHandle2, "FTBarcodeNo").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    
    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        Try

            With ogvtime
                Dim _State As String = "" & .GetRowCellValue(e.RowHandle, "FNRceceiveTypeState").ToString()

                Select Case _State
                    Case "1"
                        e.Appearance.ForeColor = Drawing.Color.Blue
                    Case "2"
                        e.Appearance.ForeColor = Drawing.Color.Green
                End Select

                Dim _State2 As String = "" & .GetRowCellValue(e.RowHandle, "FNReturnSuplType").ToString()
                Dim _State3 As String = "" & .GetRowCellValue(e.RowHandle, "FNReturnSuplType2").ToString()

                If _State2 = "0" Or _State3 = "0" Then
                    e.Appearance.BackColor = Drawing.Color.LemonChiffon
                End If
              
            End With

        Catch ex As Exception

        End Try
    End Sub

End Class
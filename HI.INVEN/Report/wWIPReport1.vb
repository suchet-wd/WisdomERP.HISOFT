Imports DevExpress.Data
Imports System.IO

Public Class wWIPReport1

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
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNOrderQty|ReserveQty|QtyRCV|FNCutFabricQty|FNCutAccQty|FNWIPFabricQty|FNWIPAccQty|FNFGFabricQty|FNFGAccQty" & _
            "|FNExport1|FNExport2|FNExport3|FNFinish1|FNFinish2|FNFinish3|FNBalance1|FNBalance2|FNBalance3|FNFabricBalQty|FNAccBalQty|AmtFabric|AmtAcc"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNOrderQty|ReserveQty|QtyRCV|FNCutFabricQty|FNCutAccQty|FNWIPFabricQty|FNWIPAccQty|FNFGFabricQty|FNFGAccQty" & _
            "|FNExport1|FNExport2|FNExport3|FNFinish1|FNFinish2|FNFinish3|FNBalance1|FNBalance2|FNBalance3|FNFabricBalQty|FNAccBalQty|AmtFabric|AmtAcc"


        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""
 

        With ogvDetail
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


    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
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

    'Private Sub LoadDatax()

    '    Dim _StartDate As String = ""
    '    Dim _EndDate As String = ""
    '    Dim _Qry As String = ""
    '    Dim _dt As DataTable
    '    Dim _TotalRow As Integer = 0
    '    Dim _Rx As Integer = 0

    '    StateCal = False

    '    Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

    '    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempOrderTracking WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

    '    _Qry = " SELECT  A.FTOrderNo, A.FNHSysStyleId, A.FNHSysBuyerId, B.FDShipDate"
    '    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
    '    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_TMERTOrder_Cut_ShipDate AS B ON A.FTOrderNo = B.MFTOrderNo"
    '    _Qry &= vbCrLf & " WHERE A.FTOrderNo <>''  "

    '    If FNHSysBuyId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
    '    End If

    '    If FNHSysStyleId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
    '    End If

    '    If FTOrderNo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
    '    End If

    '    If FTOrderNoTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
    '    End If

    '    If FTStartShipment.Text <> "" Then
    '        _Qry &= vbCrLf & " AND B.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
    '    End If

    '    _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo "

    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

    '    _TotalRow = _dt.Rows.Count
    '    _Rx = 0

    '    For Each R As DataRow In _dt.Rows
    '        _Rx = _Rx + 1

    '        _Spls.UpdateInformation("Generating Data OrderNo" & R!FTOrderNo.ToString & "  Record  " & _Rx.ToString & " Of " & _TotalRow.ToString & "  (" & Format((_Rx * 100.0) / _TotalRow, "0.00") & " % ) ")
    '        _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_Order_Tracking '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

    '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

    '    Next

    '    _Qry = " SELECT   A.FTUserLogIn, A.FTOrderNo, A.FNHSysRawMatId, B.FTRawMatCode, C.FTRawMatColorCode, "
    '    _Qry &= vbCrLf & " S.FTRawMatSizeCode, A.FNUsedQuantity, A.FNUsedPlusQuantity, U.FTUnitCode,"
    '    _Qry &= vbCrLf & " A.FNRSVQuantity, A.FNPOQuantity, A.FNRCVQuantity, A.FNRTSQuantity, A.FNPOBalQuantity, "
    '    _Qry &= vbCrLf & " A.FNRCVStockQuantity, A.FNTROInQuantity, A.FNTROOutQuantity, A.FNISSQuantity, "
    '    _Qry &= vbCrLf & " A.FNRETQuantity, A.FNADJInQuantity, A.FNADJOutQuantity, A.FNTRWInQuantity, A.FNTRWOutQuantity, A.FNSaleQuantity, "
    '    _Qry &= vbCrLf & " A.FNTerminateQuantity, A.FNOnhandQuantity, A.FNTRCQuantity, A.FNRTSAfQuantity"

    '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '        _Qry &= vbCrLf & ", B.FTRawMatNameTH AS FTRawMatName"
    '        _Qry &= vbCrLf & ", B.FTRawMatColorNameTH AS FTRawMatColorName"
    '    Else
    '        _Qry &= vbCrLf & ", B.FTRawMatNameEN AS FTRawMatName"
    '        _Qry &= vbCrLf & ", B.FTRawMatColorNameEN AS FTRawMatColorName"
    '    End If

    '    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempOrderTracking AS A WITH(NOLOCK) INNER JOIN"
    '    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK)  ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH(NOLOCK)  ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S  WITH(NOLOCK) ON B.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
    '    _Qry &= vbCrLf & " WHERE  A.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '    _Qry &= vbCrLf & " ORDER BY  A.FTOrderNo,B.FTRawMatCode, C.FTRawMatColorCode,S.FTRawMatSizeCode  "

    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempOrderTracking WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
    '    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

    '    Me.ogdtime.DataSource = _dt
    '    _Spls.Close()

    '    _RowDataChange = False

    'End Sub

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            Dim _Qry As String = ""
            Dim _dt As DataTable
            '_Qry = " Select * From (     Select PO.FTPurchaseNo, HITECH_PURCHASE.dbo.fn_PoHeader_StyleNo(PO.FTPurchaseNo) AS StyleNo, PD.FTOrderNo, PD.FNQuantity AS FNOrderQty, PO.FDDeliveryDate"
            '_Qry &= vbCrLf & "   , MM.FNHSysRawMatId,MM.FTRawMatCode,MM.FTRawMatColorCode,MM.FTRawMatSizeCode , CASE WHEN LEFT(ISNULL(MM.FTRawMatCode, ''), 1) "
            '_Qry &= vbCrLf & "  = 'F' THEN 'Fabric' ELSE 'Accessory' END AS MATERIAL_TYPE , MM.FTUnitCode, MSp.FTSuplCode, MSp.FTSuplNameTH, MSp.FTSuplNameEN "
            '_Qry &= vbCrLf & "  , RS.FNQuantity AS FNReserveQty , RC.FTReceiveNo, RC.FTInvoiceNo, TW.FTTransferWHNo, RCD.FNQuantity, RCD.FNPrice, CASE WHEN LEFT(Isnull(MM.FTRawMatCode, ''), 1) "
            '_Qry &= vbCrLf & "  = 'F' THEN Isnull(RCD.FNQuantity, 0) * isnull(RCD.FNPrice, 0) ELSE 0 END AS FNQuantityFabric, CASE WHEN LEFT(Isnull(MM.FTRawMatCode, ''), 1) = 'A' THEN Isnull(RCD.FNQuantity, 0) "
            '_Qry &= vbCrLf & "   * isnull(RCD.FNPrice, 0) ELSE 0 END AS FNQuantityAcc     "

            ''*****
            '_Qry &= vbCrLf & ",0 as FNCutFabricQty, 0 as FNCutAccQty , 0 as FNWIPFabricQty , 0 as FNWIPAccQty , 0 as FNFGFabricQty , 0 as FNFGAccQty  "
            '_Qry &= vbCrLf & ",0  as FNExport1,0  as FNExport2 ,0  as FNExport3"
            '_Qry &= vbCrLf & ",0  as FNFinish1,0  as FNFinish2 ,0  as FNFinish3"
            '_Qry &= vbCrLf & ",0  as FNBalance1,0  as FNBalance2 ,0  as FNBalance3"
            ''*****

            '_Qry &= vbCrLf & " FROM         HITECH_PURCHASE.dbo.TPURTPurchase AS PO WITH (NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "      HITECH_PURCHASE.dbo.TPURTPurchase_OrderNo AS PD WITH (NOLOCK) ON PO.FTPurchaseNo = PD.FTPurchaseNo"
            '_Qry &= vbCrLf & "      LEFT OUTER JOIN [HITECH_INVENTORY].dbo.TINVENReceive AS RC WITH(NOLOCK) ON PO.FTPurchaseNo = RC.FTReceiveNo"
            '_Qry &= vbCrLf & "       LEFT OUTER JOIN [HITECH_INVENTORY].dbo.TINVENReceive_Detail AS RCD WITH(NOLOCK) ON RC.FTReceiveNo = RCD.FTReceiveNo"
            '_Qry &= vbCrLf & "     LEFT OUTER JOIN "
            '_Qry &= vbCrLf & "          (SELECT     RS.FTReserveNo, RS.FDReserveDate, RS.FTReserveBy, RS.FNHSysWHId AS FNHSysWHId_RSV, RS.FTOrderNo as FTOrderNo_RSV, RS.FTRemark, RS.FTStateMailToStock, RS.FTMailToStockBy, RS.FTMailToStockDate, "
            '_Qry &= vbCrLf & "  RS.FTMailToStockTime, BO.FTBarcodeNo, BO.FTDocumentNo, BO.FNHSysWHId, BO.FTOrderNo, BO.FNQuantity, BO.FTStateReserve, BO.FTDocumentRefNo,"
            '_Qry &= vbCrLf & "  BO.FNHSysCmpId"
            '_Qry &= vbCrLf & "	FROM         TINVENReserve AS RS WITH (NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "     TINVENBarcode_OUT AS BO WITH (NOLOCK) ON RS.FTReserveNo = BO.FTDocumentNo) AS RS ON  PD.FTOrderNo = RS.FTOrderNo_RSV"
            '_Qry &= vbCrLf & "    LEFT OUTER JOIN "
            '_Qry &= vbCrLf & "     (   SELECT     BO1.FTBarcodeNo, BO1.FTDocumentNo, BO1.FTOrderNo, BO1.FNQuantity, BO1.FTStateReserve, BO1.FTDocumentRefNo, BO1.FNHSysCmpId, TW.FTTransferWHNo, TW.FDTransferWHDate, "
            '_Qry &= vbCrLf & "    TW.FTTransferWHBy, TW.FNHSysWHIdTo, TW.FTRemark, TW.FTStateApprove, TW.FTApproveBy, TW.FDApproveDate, TW.FTApproveTime, TW.FNHSysCmpId AS FNHSysCmpIdTWNo, TW.FTCancelBy, "
            '_Qry &= vbCrLf & "    TW.FDCancelDate, TW.FTCancelTime, TW.FNHSysWHId"
            '_Qry &= vbCrLf & "	FROM         TINVENBarcode_OUT AS BO1 WITH (NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "   TINVENTransferWH AS TW WITH (NOLOCK) ON BO1.FTDocumentNo = TW.FTTransferWHNo) AS TW ON   PD.FTOrderNo = TW.FTOrderNo"
            '_Qry &= vbCrLf & "   LEFT OUTER JOIN [HITECH_INVENTORY].dbo.V_Material AS MM WITH(NOLOCK) ON PD.FNHSysRawMatId = MM.FNHSysRawMatId"
            '_Qry &= vbCrLf & "    LEFT OUTER JOIN    HITECH_MASTER.dbo.TCNMSupplier AS MSp WITH (NOLOCK) ON PO.FNHSysSuplId = MSp.FNHSysSuplId ) AS A  "
            '_Qry &= vbCrLf & " WHERE Isnull(A.FTPurchaseNo,'') <> ''"

            _Qry = "SELECT      FTPORef, StyleNo, FTOrderNo, FNOrderQty,  FTRawMatCode, FTRawMatColorCode, FTRawMatSizeCode, FTMatTypeCode, FTUnitCode, FTPurchaseNo, FTSuplCode, "
            _Qry &= vbCrLf & " ReserveQty, FTReceiveNo, FTInvoiceNo, FTTransferWHNo, QtyRCV, FNPrice, AmtFabric, AmtAcc, FNCutFabricQty, FNCutAccQty, FNWIPFabricQty, FNWIPAccQty, FNFGFabricQty, FNFGAccQty,"
            _Qry &= vbCrLf & " FNExport1, FNExport2, FNExport3, FNFinish1, FNFinish2, FNFinish3, FNBalance1, FNBalance2, FNBalance3, FNFabricBalQty, FNAccBalQty"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(FDDeliveryDate)=1 Then Convert(varchar(10),Convert(datetime,FDDeliveryDate) ,103)  Else '' END AS FDDeliveryDate"
            _Qry &= vbCrLf & ",DocDate,FTCmpCode"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(DocDate)=1 Then Convert(varchar(10),Convert(datetime,DocDate),103) Else '' END AS FDDocDateOut"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",FTCmpNameTH AS FTCmpName"
            Else
                _Qry &= vbCrLf & ",FTCmpNameEN AS FTCmpName"
            End If

            _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.V_RptWip WHERE Isnull(FTPORef,'') <> '' "


            If FNHSysCmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND FTCmpCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpId.Text) & "'  "
            End If

            Select Case oSelectDate.SelectedIndex
                Case 0
                    If Me.FTMonth.Text <> "" Then
                        _Qry &= vbCrLf & " AND left(DocDate,7)  ='" & Format(Me.FTMonth.EditValue, "yyyy/MM") & "'  "
                    End If
                Case 1
                    If Me.FTDateS.Text <> "" Then
                        _Qry &= vbCrLf & " AND DocDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateS.Text) & "'  "
                    End If
                    If Me.FTDateE.Text <> "" Then
                        _Qry &= vbCrLf & " AND DocDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateE.Text) & "'  "
                    End If
            End Select


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
            Me.ogcDetial.DataSource = _dt
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysCmpId.Text <> "" Then
            _Pass = True
        End If

        If Me.FTMonth.Text <> "" Then
            _Pass = True
        End If
       
        If Me.FTDateS.Text <> "" Then
            _Pass = True
        End If

        If Me.FTDateE.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvDetail)
            StateCal = False
            DateEdit1.Visible = False
            FTDateS.Enabled = False
            FTDateE.Enabled = False
          
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
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvDetail)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private _MonthState As Integer = 0
    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

        If VerifyData() Then
            Dim _FM As String = ""

            If FNHSysCmpId.Text <> "" Then
                _FM &= IIf(_FM.Trim <> "", " AND ", "")
                _FM &= " {V_RptWip.FTCmpCode}  ='" & HI.UL.ULF.rpQuoted(FNHSysCmpId.Text) & "'  "
            End If
 
            Select Case Me.oSelectDate.SelectedIndex
                Case 0
                    If Me.FTMonth.Text <> "" Then

                        _FM &= IIf(_FM.Trim <> "", " AND ", "")
                        _FM &= " {V_RptWip.DocDate} >='" & Format(FTMonth.EditValue, "yyyy/MM") & "/01" & "'  "
                        _FM &= " AND  {V_RptWip.DocDate} <='" & Format(FTMonth.EditValue, "yyyy/MM") & "/31" & "'  "

                    End If

                Case 1
                    If FTDateS.Text <> "" Then
                        _FM &= IIf(_FM.Trim <> "", " AND ", "")
                        _FM &= " {V_RptWip.DocDate} >='" & HI.UL.ULDate.ConvertEnDB(FTDateS.Text) & "'  "
                    End If
                    If FTDateE.Text <> "" Then
                        _FM &= IIf(_FM.Trim <> "", " AND ", "")
                        _FM &= " {V_RptWip.DocDate} <='" & HI.UL.ULDate.ConvertEnDB(FTDateE.Text) & "'  "
                    End If
            End Select






            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "StockWipReport.rpt"
                .Formular = _FM
                .Preview()
            End With

        End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
     
 
    Private Sub FTMonth_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTMonth.ButtonClick, DateEdit1.ButtonClick
        Try
            Me.FTMonth.Text = Date.Now.ToString
        Catch ex As Exception
        End Try
    End Sub

  
    
 
    Private Sub oSelectDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles oSelectDate.SelectedIndexChanged
        Try
            Select Case oSelectDate.SelectedIndex
                Case 0
                    DateEdit1.Visible = False
                    FTMonth.Visible = True
                    FTMonth.Enabled = True
                    FTDateS.Enabled = False
                    FTDateE.Enabled = False
                    FTDateE.Text = ""
                    FTDateS.Text = ""
                Case 1
                    DateEdit1.Visible = True
                    FTMonth.Visible = False
                    FTMonth.Enabled = False
                    FTDateS.Enabled = True
                    FTDateE.Enabled = True

            End Select

        Catch ex As Exception
        End Try
    End Sub

    
     
    
End Class
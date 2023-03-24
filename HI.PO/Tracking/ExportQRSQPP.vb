
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns


Public Class ExportQRSQPP
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitialGridSummaryMergCell()


    End Sub
#Region "Initial Grid"
    Private Sub InitGridClearSort()

        For Each c As GridColumn In ogvdetail.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

    End Sub

    Private Sub InitialGridSummaryMergCell()

        For Each c As GridColumn In ogvdetail.Columns

            Select Case c.FieldName.ToString
                Case "Factory", "FTSuplName", "FTPurchaseNo", "FDDeliveryDate", "FTRawMatCode", "FTRawMatColorCode", "FTRawMatColorName", "FTRawMatSizeCode", "FNQuantity", "FTUnitCode", "FTOrderNo", "FNOrderType", "FTBuyName", "Program", "FTSeasonCode"
                    ' Case "FTPurchaseNo", "FDDeliveryDate", "FTRawMatColorCode"
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


    Private Sub InitStartValue()
        totalSum2 = 0
        GrpSum2 = 0
        _RowHandleHold2 = 0
    End Sub



    Private Sub ogvdetail_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvdetail.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitStartValue()
            End If

            With ogvdetail
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold2 Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> .GetRowCellValue(_RowHandleHold2, "FTPurchaseNo").ToString Or
                                                               .GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold2, "FTOrderNo").ToString Or
                                                               .GetRowCellValue(e.RowHandle, "FTRawMatColorCode").ToString <> .GetRowCellValue(_RowHandleHold2, "FTRawMatColorCode").ToString Or
                                                                .GetRowCellValue(e.RowHandle, "FTRawMatCode").ToString <> .GetRowCellValue(_RowHandleHold2, "FTRawMatCode").ToString) Or e.RowHandle = _RowHandleHold2 Then

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

   
#End Region


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmexportrycexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click

        If Me.FNHSysBuyId.Text <> "" Then

            If Me.ogvdetail.RowCount <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export กรุณาทำการตรวจสอบ !!!", 1505140001, Me.Text)
            Else
                Try

                    Dim Op As New System.Windows.Forms.SaveFileDialog
                    Op.Filter = "Excel Files(.xlsx)|*.xlsx"
                    Op.ShowDialog()

                    Try

                        If Op.FileName <> "" Then

                            With ogcdetail
                                .ExportToXlsx(Op.FileName)

                                Try
                                    Process.Start(Op.FileName)
                                Catch ex As Exception
                                End Try

                            End With

                        End If

                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                End Try

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()

        End If

    End Sub

    Private Sub ExportQRSQPP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetail)


            Call InitGridClearSort()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadDataInfo(Optional ExportExcel As Boolean = False)
        Me.ogcdetail.DataSource = Nothing
        Dim dt As New DataTable
        Dim _Qry As String

        Dim _Spls As New HI.TL.SplashScreen("Loading...")


        Try

            _Qry = "SELECT DISTINCT C.FTCmpCode AS Factory,P.FTPurchaseNo,Case When isdate(P.FDDeliveryDate ) =1 Then convert(varchar(10),convert(date,P.FDDeliveryDate ),103) Else '' END AS FDDeliveryDate"
            _Qry &= vbCrLf & " ,M.FTRawMatCode,MC.FTRawMatColorCode,MS.FTRawMatSizeCode,T2.FNQuantity,U.FTUnitCode,T2.FTSMPOrderNo AS FTOrderNo,O.FNOrderType,'' AS PINumber ,''AS ConfirmDeliveryDate"
            _Qry &= vbCrLf & " ,OG.FTGACDate,OG.FTOGACDate,S.FTStyleCode,O.Program,SS.FTSeasonCode"
            _Qry &= vbCrLf & " ,Case When isdate(P.FDDeliveryDate ) =1 Then convert(varchar(10),convert(date,P.FDDeliveryDate ),103) Else '' END AS Bulkrequestdate"
            _Qry &= vbCrLf & ",Case When isdate(P.FDDeliveryDate ) =1 Then  CONVERT(varchar(20),(DATEADD(day,-15, P.FDDeliveryDate)),103) ELSE NULL END as Samplerequestdate"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",SUP.FTSuplNameTH AS FTSuplName,PO.FTRawMatColorNameTH AS FTRawMatColorName,B.FTBuyNameTH AS FTBuyName"
            Else
                _Qry &= vbCrLf & ",SUP.FTSuplNameEN AS FTSuplName,PO.FTRawMatColorNameEN AS FTRawMatColorName,B.FTBuyNameEN AS FTBuyName"
            End If
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) "
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK) ON P.FTPurchaseNo=PO.FTPurchaseNo"

            _Qry &= vbCrLf & " INNER JOIN  (SELECT O.FTSMPOrderNo AS FTOrderNo,O.FTPgmName AS Program,O.FNHSysBuyId,O.FNHSysSeasonId,O.FNHSysStyleId,L.FNListIndex"
            '  _Qry &= vbCrLf & " ,Case When isdate(OB.FTGACDate ) =1 Then convert(varchar(10),convert(date,OB.FTGACDate ),103) Else '' END AS FTGACDate,Case When isdate(OB.FTOGACDate ) =1 Then convert(varchar(10),convert(date,OB.FTOGACDate ),103) Else '' END AS FTOGACDate"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",L.FTNameTH AS FNOrderType"
            Else
                _Qry &= vbCrLf & ",L.FTNameEN AS FNOrderType"
            End If
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O   WITH(NOLOCK) "
            '_Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown AS OB ON O.FTSMPOrderNo=OB.FTSMPOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  WITH(NOLOCK) WHERE L.FTListName='FNSMPOrderType')AS L ON O.FNSMPOrderType=L.FNListIndex"


            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE (O.FNSMPOrderType = 5 OR O.FNSMPOrderType = 6 ) "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNSMPOrderType = 29  "
            End Select


            _Qry &= vbCrLf & "   UNION"
            _Qry &= vbCrLf & " SELECT O.FTOrderNo,O.FTSubPgm AS Program,O.FNHSysBuyId,O.FNHSysSeasonId,O.FNHSysStyleId,L.FNListIndex"
            '  _Qry &= vbCrLf & " ,Case When isdate(OB.FDShipDate ) =1 Then convert(varchar(10),convert(date,OB.FDShipDate ),103) Else '' END AS FTGACDate,Case When isdate(OB.FDShipDateOrginal ) =1 Then convert(varchar(10),convert(date,OB.FDShipDateOrginal ),103) Else '' END AS FTOGACDate"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",L.FTNameTH AS FNOrderType"
            Else
                _Qry &= vbCrLf & ",L.FTNameEN AS FNOrderType"
            End If
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH(NOLOCK) "
            ' _Qry &= vbCrLf & " LEFT OUTER JOIN (SELECT O.FTOrderNo,O.FDShipDate,O.FDShipDateOrginal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS O  WHERE O.FTSubOrderNo LIKE N'%A%')AS OB ON O.FTOrderNo=OB.FTOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  WITH(NOLOCK) WHERE L.FTListName='FNOrderType')AS L ON O.FNOrderType=L.FNListIndex "

            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 5 "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 26 "
            End Select


            _Qry &= vbCrLf & "   ) AS O ON PO.FTOrderNO=O.FTOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SUP  WITH(NOLOCK) ON P.FNHSysSuplId=SUP.FNHSysSuplId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C  WITH(NOLOCK) ON P.FNHSysCmpId=C.FNHSysCmpId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B  WITH(NOLOCK) ON O.FNHSysBuyId=B.FNHSysBuyId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M  WITH(NOLOCK) ON PO.FNHSysRawMatId=M.FNHSysRawMatId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC  WITH(NOLOCK) ON M.FNHSysRawMatColorId=MC.FNHSysRawMatColorId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS  WITH(NOLOCK) ON M.FNHSysRawMatSizeId=MS.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON PO.FNHSysUnitId=U.FNHSysUnitId"
            '_Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON O.FNHSysStyleId=S.FNHSysStyleId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN (SELECT DISTINCT P.FTPurchaseNo,O.FNHSysBuyId,P.FNHSysSuplId,  ISNULL ((SELECT TOP 1 STUFF "
            _Qry &= vbCrLf & "((SELECT  ', ' + t2.FTStyleCode"
            _Qry &= vbCrLf & "FROM      (Select DISTINCT S.FTStyleCode,S.FNHSysStyleId,PO.FTPurchaseNo,O.FNHSysBuyId"
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O   WITH(NOLOCK)ON S.FNHSysStyleId=O.FNHSysStyleId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK) ON O.FTOrderNo=PO.FTOrderNo"



            _Qry &= vbCrLf & ") AS T2"
            _Qry &= vbCrLf & "WHERE T2.FTPurchaseNo =P.FTPurchaseNo AND T2.FNHSysBuyId=O.FNHSysBuyId FOR XML PATH('')), 1, 2, '')  )"
            _Qry &= vbCrLf & ",'') AS FTStyleCode  "
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH(NOLOCK)"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK) ON O.FTOrderNo=PO.FTOrderNo"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo"
            _Qry &= vbCrLf & ")AS S ON P.FTPurchaseNo=S.FTPurchaseNo AND B.FNHSysBuyId=S.FNHSysBuyId AND SUP.FNHSysSuplId=S.FNHSysSuplId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS  WITH(NOLOCK)  ON O.FNHSysSeasonId=SS.FNHSysSeasonId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN(SELECT DISTINCT P.FTPurchaseNo,O.FNHSysBuyId,P.FNHSysSuplId,MC.FTRawMatColorCode,SUM(PO.FNQuantity)AS FNQuantity,O.FNOrderType,  ISNULL ((SELECT TOP 1 STUFF "
            _Qry &= vbCrLf & "((SELECT  ', ' + t2.FTOrderNo"
            _Qry &= vbCrLf & "FROM      (Select DISTINCT O.FTOrderNo AS FTOrderNo,O.FTPurchaseNo,O.FNHSysBuyId,O.FNHSysSuplId,O.FTRawMatColorNameTH,O.FNOrderType"
            _Qry &= vbCrLf & "FROM  (SELECT O.FTSMPOrderNo AS FTOrderNo,P.FTPurchaseNo,O.FNHSysBuyId,P.FNHSysSuplId,MC.FTRawMatColorNameTH,O.FNSMPOrderType AS FNOrderType"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O   WITH(NOLOCK) "
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK) ON O.FTSMPOrderNo=PO.FTOrderNo"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M  WITH(NOLOCK) ON PO.FNHSysRawMatId=M.FNHSysRawMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC  WITH(NOLOCK) ON M.FNHSysRawMatColorId=MC.FNHSysRawMatColorId"

            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE (O.FNSMPOrderType = 5 OR O.FNSMPOrderType = 6 ) "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNSMPOrderType = 29  "
            End Select


            _Qry &= vbCrLf & " UNION"
            _Qry &= vbCrLf & "SELECT O.FTOrderNo,P.FTPurchaseNo,O.FNHSysBuyId,P.FNHSysSuplId,MC.FTRawMatColorNameTH,O.FNOrderType"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK) ON O.FTOrderNo=PO.FTOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M  WITH(NOLOCK) ON PO.FNHSysRawMatId=M.FNHSysRawMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC  WITH(NOLOCK) ON M.FNHSysRawMatColorId=MC.FNHSysRawMatColorId"

            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 5 "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 26 "
            End Select


            _Qry &= vbCrLf & ") AS O "
            _Qry &= vbCrLf & ") t2"
            _Qry &= vbCrLf & "WHERE   t2.FTPurchaseNo=P.FTPurchaseNo AND t2.FNHSysBuyId=O.FNHSysBuyId AND t2.FNHSysSuplId=P.FNHSysSuplId AND t2.FTRawMatColorNameTH=MC.FTRawMatColorNameTH AND t2.FNOrderType=O.FNOrderType FOR XML PATH('')), 1, 2, '')  )"
            _Qry &= vbCrLf & ",'') AS FTSMPOrderNo "
            _Qry &= vbCrLf & "FROM (SELECT O.FTOrderNo,O.FNHSysBuyId,O.FNOrderType"
            _Qry &= vbCrLf & " FROM[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH(NOLOCK) "

            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 5 "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 26 "
            End Select


            _Qry &= vbCrLf & " UNION"
            _Qry &= vbCrLf & "SELECT O.FTSMPOrderNo AS FTOrderNo ,O.FNHSysBuyId,O.FNSMPOrderType AS FNOrderType"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O  WITH(NOLOCK)  "

            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE (O.FNSMPOrderType = 5 OR O.FNSMPOrderType = 6 ) "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNSMPOrderType = 29  "
            End Select


            _Qry &= vbCrLf & "  ) AS O"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK)  ON O.FTOrderNo=PO.FTOrderNo"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P   WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M  WITH(NOLOCK)  ON PO.FNHSysRawMatId=M.FNHSysRawMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC  WITH(NOLOCK)  ON M.FNHSysRawMatColorId=MC.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "GROUP BY P.FTPurchaseNo,O.FNHSysBuyId,P.FNHSysSuplId,MC.FTRawMatColorCode,MC.FTRawMatColorNameTH,O.FNOrderType)AS T2 ON P.FTPurchaseNo=T2.FTPurchaseNo AND B.FNHSysBuyId=T2.FNHSysBuyId AND SUP.FNHSysSuplId=T2.FNHSysSuplId AND MC.FTRawMatColorCode=T2.FTRawMatColorCode AND O.FNListIndex=T2.FNOrderType"

            _Qry &= vbCrLf & " LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "Select DISTINCT  O.FTPurchaseNo,O.FNHSysBuyId,max(O.FTGACDate)AS FTGACDate,max(O.FTOGACDate)AS FTOGACDate"
            _Qry &= vbCrLf & "FROM  (SELECT P.FTPurchaseNo,O.FNHSysBuyId"
            _Qry &= vbCrLf & ",Case When isdate(OB.FTGACDate ) =1 Then convert(varchar(10),convert(date,OB.FTGACDate ),103) Else '' END AS FTGACDate,Case When isdate(OB.FTOGACDate ) =1 Then convert(varchar(10),convert(date,OB.FTOGACDate ),103) Else '' END AS FTOGACDate"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O  WITH(NOLOCK) "
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK)  ON O.FTSMPOrderNo=PO.FTOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo"
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown AS OB  WITH(NOLOCK)  ON O.FTSMPOrderNo=OB.FTSMPOrderNo"

            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE (O.FNSMPOrderType = 5 OR O.FNSMPOrderType = 6 ) "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNSMPOrderType = 29  "
            End Select



            _Qry &= vbCrLf & "  UNION"
            _Qry &= vbCrLf & " SELECT P.FTPurchaseNo,O.FNHSysBuyId"
            _Qry &= vbCrLf & ",Case When isdate(OB.FDShipDate ) =1 Then convert(varchar(10),convert(date,OB.FDShipDate ),103) Else '' END AS FTGACDate,Case When isdate(OB.FDShipDateOrginal ) =1 Then convert(varchar(10),convert(date,OB.FDShipDateOrginal ),103) Else '' END AS FTOGACDate"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO  WITH(NOLOCK)  ON O.FTOrderNo=PO.FTOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P  WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN (SELECT O.FTOrderNo,O.FDShipDate,O.FDShipDateOrginal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS O  WITH(NOLOCK)  WHERE O.FTSubOrderNo LIKE N'%A%')AS OB ON O.FTOrderNo=OB.FTOrderNo"

            Select Case FNQppExportOrderType.SelectedIndex
                Case 1
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 5 "
                Case 2
                    _Qry &= vbCrLf & " WHERE O.FNOrderType = 26 "
            End Select


            _Qry &= vbCrLf & ") AS O "
            _Qry &= vbCrLf & " group by O.FTPurchaseNo,O.FNHSysBuyId"
            _Qry &= vbCrLf & " ) AS OG ON O.FNHSysBuyId=OG.FNHSysBuyId AND P.FTPurchaseNo=OG.FTPurchaseNo"



            _Qry &= vbCrLf & " WHERE B.FTBuyCode<>'' "

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & " AND B.FTBuyCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyId.Text) & "' "
            End If
            If FNHSysSuplId.Text <> "" Then
                _Qry &= vbCrLf & " AND SUP.FTSuplCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
            End If
            If FDPurchaseDate.Text <> "" Then
                _Qry &= vbCrLf & " AND P.FDPurchaseDate>='" & HI.UL.ULDate.ConvertEnDB(FDPurchaseDate.Text) & "'"
            End If
            If FDPurchaseDateEnd.Text <> "" Then
                _Qry &= vbCrLf & " AND P.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(FDPurchaseDateEnd.Text) & "'"
            End If

            _Qry &= vbCrLf & " ORDER BY C.FTCmpCode,P.FTPurchaseNo ASC"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.ogcdetail.DataSource = dt


            Call InitialGridSummaryMergCell()
            '  Me.ogcdetail.DataSource = dt.Copy
            '  Me.ogvdetail.BestFitColumns()

            'dt.Dispose()

        Catch ex As Exception
        End Try

        _Spls.Close()

    End Sub

    Private Function VerifyData() As Boolean
        Dim CheckState As Boolean = True
        If Me.FNHSysBuyId.Text.Trim = "" And Me.FNHSysSuplId.Text.Trim = "" And (Me.FDPurchaseDate.Text <> "" Or Me.FDPurchaseDateEnd.Text <> "") Then

            CheckState = False
        End If

        Return CheckState
    End Function

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If Me.VerifyData Then
            Call LoadDataInfo()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysSuplId_lbl.Text)
            FNHSysSuplId.Focus()
        End If

    End Sub

    Private Sub ogvdetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvdetail.CellMerge
        Try
            With Me.ogvdetail
                Select Case e.Column.FieldName
                    Case "Factory", "FTSuplName", "FTPurchaseNo", "FDDeliveryDate", "FTRawMatCode", "FTRawMatColorCode", "FTRawMatColorName", "FTRawMatSizeCode", "FNQuantity", "FTUnitCode", "FTOrderNo", "FNOrderType", "FTBuyName", "Program", "FTSeasonCode"
                        'Case "FTPurchaseNo", "FDDeliveryDate", "FNQuantity", "FTUnitCode", "FTOrderNo", "FNOrderType", "FTBuyName", "FTStyleCode", "Program", "FTSeasonCode"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTPurchaseNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPurchaseNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case "Factory", "FTSuplName", "FTPurchaseNo", "FDDeliveryDate", "FTRawMatCode", "FTRawMatColorCode", "FTRawMatColorName", "FTRawMatSizeCode", "FNQuantity", "FTUnitCode", "FTOrderNo", "FNOrderType", "FTBuyName", "Program", "FTSeasonCode"
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
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns


Public Class wPerformainvoiceTracking
    '  Private _StateFormLoad As Boolean = False

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Sub New()
        InitializeComponent()

        Call InitGrid()

        ' _StateFormLoad = True
    End Sub

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


#Region "Initial Grid"

    Private Property FNHSysCmpId As Object

    Private Sub InitGrid()
        Try
            With Me.ogvtime
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsMenu.EnableColumnMenu = False
                .OptionsMenu.ShowAutoFilterRowItem = False
                .OptionsFilter.AllowFilterEditor = False
                .OptionsFilter.AllowColumnMRUFilterList = False
                .OptionsFilter.AllowMRUFilterList = False
                .OptionsSelection.MultiSelect = False
            End With

            '------Start Add Summary Grid-------------
            With ogvtime
                .ClearGrouping()
                .ClearDocument()
                .Columns("FTSuplCode").Group()

                ' --------------------------------------------------

                .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
                .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n2}"
                ' .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Nothing, "( " & .Columns.ColumnByFieldName("FNQuantity").Caption & "={0:n2})")
                .Columns("FNAmount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount")
                .Columns("FNAmount").SummaryItem.DisplayFormat = "{0:n2}"
                '.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount", Nothing, "( " & .Columns.ColumnByFieldName("FNAmount").Caption & "={0:n2})")
                .Columns("FNAmtPO").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmtPO")
                .Columns("FNAmtPO").SummaryItem.DisplayFormat = "{0:n2}"
                ' .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmtPO", Nothing, "( " & .Columns.ColumnByFieldName("FNAmtPO").Caption & "={0:n2})")
                .Columns("FNAmtCharge").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmtCharge")
                .Columns("FNAmtCharge").SummaryItem.DisplayFormat = "{0:n2}"
                '.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmtCharge", Nothing, "( " & .Columns.ColumnByFieldName("FNAmtCharge").Caption & "={0:n2})")
                .Columns("FNDebitCredit").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNDebitCredit")
                .Columns("FNDebitCredit").SummaryItem.DisplayFormat = "{0:n2}"
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = False
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
                .OptionsView.ShowGroupPanel = True
                .OptionsView.ShowAutoFilterRow = False

                .GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
                item1.FieldName = "FNQuantity"
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
                item1.DisplayFormat = "{0:n2}"
                item1.ShowInGroupColumnFooter = .Columns("FNQuantity")
                .GroupSummary.Add(item1)
                Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem()
                item2.FieldName = "FNAmount"
                item2.SummaryType = DevExpress.Data.SummaryItemType.Sum
                item2.DisplayFormat = "{0:n2}"
                item2.ShowInGroupColumnFooter = .Columns("FNAmount")
                .GroupSummary.Add(item2)
                Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem()
                item3.FieldName = "FNAmtPO"
                item3.SummaryType = DevExpress.Data.SummaryItemType.Sum
                item3.DisplayFormat = "{0:n2}"
                item3.ShowInGroupColumnFooter = .Columns("FNAmtPO")
                .GroupSummary.Add(item3)
                Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem()
                item4.FieldName = "FNAmtCharge"
                item4.SummaryType = DevExpress.Data.SummaryItemType.Sum
                item4.DisplayFormat = "{0:n2}"
                item4.ShowInGroupColumnFooter = .Columns("FNAmtCharge")
                .GroupSummary.Add(item4)
                Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem()
                item5.FieldName = "FNDebitCredit"
                item5.SummaryType = DevExpress.Data.SummaryItemType.Sum
                item5.DisplayFormat = "{0:n2}"
                item5.ShowInGroupColumnFooter = .Columns("FNDebitCredit")
                .GroupSummary.Add(item5)
                '  ------------------------------------------------
            End With


        Catch ex As Exception

        End Try

    End Sub
  


    Private Sub InitialGridSummaryMergCell()

        For Each c As GridColumn In ogvtime.Columns

            Select Case c.FieldName.ToString
                Case "FTInvoiceBankNo", "FTSuplName", "FTPurchaseNo", "FTRawMatName", "FTCustName", "FTBnkAccNo", "FTCrTermCode", "FTCurCode", "FNAmount", "FNHSysUnitId", "Charge", "FNQuantity", "FTInvoiceBankDate", "FTPurchaseBy", "FDInvoiceDate", "FDDeliveryDate", "FNAmtCharge", "FDPayDate", "FTRemark", "FTInvoiceNo", "FTSuplCode", "FNDebitCredit"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub
    Private Sub ogvtime_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvtime.CellMerge
        Try
            With Me.ogvtime
                Select Case e.Column.FieldName
                    Case "FTSuplName", "FTCustName", "FTBnkAccNo", "FTCrTermCode", "FTCurCode", "FNHSysUnitId", "Charge", "FTInvoiceBankDate", "FTPurchaseBy", "FNAmount", "FTSuplCode"
                        If "" & .GetRowCellValue(e.RowHandle1, "FTSuplName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSuplName").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTInvoiceBankNo", "FTCurCode", "FTCrTermCode", "FTCustName", "FTPurchaseNo", "FTRawMatName", "FNQuantity", "FDInvoiceDate", "FDDeliveryDate", "FNAmtCharge", "FDPayDate", "FTRemark", "FTInvoiceNo", "FNDebitCredit"
                        If "" & .GetRowCellValue(e.RowHandle1, "FTInvoiceBankNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTInvoiceBankNo").ToString Then
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
#End Region



    Private Sub wPoDetailTrackingForExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' _StateFormLoad = False
        
        '  Call InitGrid()
    End Sub

    Private Sub ocmload_Click_1(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            '  If VerifyData() Then
            LoadData()
            ' End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData()

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Cmd As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        Dim _Raw As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNPIChargeType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge WITH(NOLOCK) WHERE FNPIChargeType<> 0 ", Conn.DB.DataBaseName.DB_MASTER, "")

        StateCal = False
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Cmd = "SELECT       S.FTSuplCode ,P.FTPurchaseNo,  C.FTInvoiceBankNo  ,isnuLL(isnull(IPO.FNAmount-IPO.FNNetAmt,C.FNChargeNetAmount),0) as FNAmtCharge,IPO.FTInvoiceNo, Case When isdate(DRD.FDInvoiceDate) =1 Then convert(varchar(10),convert(date,DRD.FDInvoiceDate),103) Else '' END  as FDInvoiceDate, isnull(DRD.FNQuantity,0)as FNQuantity" ',isnull(isnull(DRD.PO,C.FNNetAmount),0)AS FNAmtPO,(isnull(isnull(DRD.PO,C.FNNetAmount),0)+isnull(isnull(IPO.FNAmount-sum(PO.FNNetAmt),CCV.FNAmount),0))as FNAmount" ' , isnull(isnull(DRD.PO,0)+C.FNChargeNetAmount,0) AS FNAmount"
        _Cmd &= vbCrLf & "      ,isnull(isnull(IPO.FNNetAmt,CCV.FNAmount),PH.FNPONetAmt) AS FNAmtPO,isnuLL(isnull(IPO.FNAmount-IPO.FNNetAmt,C.FNChargeNetAmount),0) +isnull(isnull(IPO.FNNetAmt,CCV.FNAmount),PH.FNPONetAmt)+isnull(DA.Debit,0) AS FNAmount,isnull(DA.Debit,0)  as FNDebitCredit"
        _Cmd &= vbCrLf & "	  ,R.FTCurCode , T.FTCrTermCode  , U.FTCustCode,PH.FTPurchaseBy,S.FTBnkAccNo,UN.FTUnitCode as FNHSysUnitId,C.FTRemark" ',isnull( MD.FTMainMatNameTH,B.FTChageNameTH) as FTRawMatName
        _Cmd &= vbCrLf & ", Case When isdate(C.FDPrepareDate) =1 Then convert(varchar(10),convert(date,C.FDPrepareDate),103) Else '' END AS FTInvoiceBankDate "
        _Cmd &= vbCrLf & ", Case When isdate(Isnull(DRD.FDReceiveDate,PH.FDDeliveryDate)) =1 Then convert(varchar(10),convert(date,Isnull(DRD.FDReceiveDate,PH.FDDeliveryDate)),103) Else '' END AS FDDeliveryDate "
        _Cmd &= vbCrLf & ", Case When isdate(C.FDPayDate) =1 Then convert(varchar(10),convert(date,C.FDPayDate),103) Else '' END AS FDPayDate  "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & "	,isnull( U.FTCustNameTH,CT.FTCustNameTH) as FTCustName  , S.FTSuplNameTH as FTSuplName ,MD.FTMainMatNameTH as FTRawMatName" ', MT.FTMatTypeNameEN as FTRawMatName
        Else
            _Cmd &= vbCrLf & "	,isnull( U.FTCustNameEN,CT.FTCustNameEN) as FTCustName   , S.FTSuplNameEN as FTSuplName, MD.FTMainMatNameEN as FTRawMatName "
        End If
        _Cmd &= vbCrLf & ", (SELECT        TOP 1 STUFF"
        _Cmd &= vbCrLf & "       ((SELECT        ', ' + t2.FTChargeDesc "
        _Cmd &= vbCrLf & "       FROM            (SELECT      C.FNHSysPIChageId    ,C.FNPIType , C.FTChageNameTH as FTChargeDesc,CC.FTInvoiceBankNo"
        _Cmd &= vbCrLf & "   FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TEXPChage AS C WITH (NOLOCK)"
        _Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Charge AS CC ON C.FNHSysPIChageId=CC.FNHSysPIChageId) t2"
        _Cmd &= vbCrLf & "  WHERE        t2.FNPIType =  C.FNPIChargeType  and t2.FTInvoiceBankNo=C.FTInvoiceBankNo  FOR XML PATH('')), 1, 2, '') AS Charge) AS Charge"


        _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge AS C WITH (NOLOCK) LEFT OUTER JOIN"
        '_Cmd &= vbCrLf & "(select LS.FTNameTH,LS.FNListIndex from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData  AS LS"
        '_Cmd &= vbCrLf & "where FTListName='FNPIType')AS LS  ON C.FNPIChargeType=LS.FNListIndex LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail AS P  ON C.FTInvoiceBankNo = P.FTInvoiceBankNo LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON C.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS PH WITH(NOLOCK) ON P.FTPurchaseNo = PH.FTPurchaseNo LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO WITH (NOLOCK) ON P.FTPurchaseNo = PO.FTPurchaseNo LEFT OUTER JOIN"
        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON PO.FTOrderNo = O.FTOrderNo	 LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "	 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS U WITH(NOLOCK) ON O.FNHSysCustId = U.FNHSysCustId LEFT OUTER JOIN "
        _Cmd &= vbCrLf & "	 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS R WITH(NOLOCK) ON PH.FNHSysCurId = R.FNHSysCurId LEFT OUTER JOIN "
        _Cmd &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm AS T WITH(NOLOCK) ON PH.FNHSysCrTermId = T.FNHSysCrTermId LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnit AS UN WITH(NOLOCK) ON PO.FNHSysUnitId =UN.FNHSysUnitId	 LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON PO.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON M.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_PIDescription as MD ON MM.FNHSysMainMatId=MD.FNHSysMainMatId"
        '-----------------------------------
        _Cmd &= vbCrLf & " LEFT OUTER JOIN ("
        _Cmd &= vbCrLf & "select BCC.FTInvoiceBankNo,sum(BCC.FNNetAmount) as FNAmount"
        _Cmd &= vbCrLf & "from    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge AS BCC"
        _Cmd &= vbCrLf & " where BCC.FNPIChargeType<>0"
        _Cmd &= vbCrLf & " group by BCC.FTInvoiceBankNo "
        _Cmd &= vbCrLf & ")as  CCV ON C.FTInvoiceBankNo=CCV.FTInvoiceBankNo "
        '------------------------
        _Cmd &= vbCrLf & "LEFT OUTER JOIN ( 		 Select  C.FNHSysCurId ,PD.FTInvoiceBankNo ,C.FNHSysCustId,C.FTCustNameTH,C.FTCustNameEN"
        _Cmd &= vbCrLf & "   From     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS C WITH(NOLOCK) "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail AS PD ON C.FTCustCode = PD.FTPurchaseNo"
        _Cmd &= vbCrLf & "group by C.FNHSysCurId ,PD.FTInvoiceBankNo,C.FNHSysCustId,C.FTCustNameTH,C.FTCustNameEN"
        _Cmd &= vbCrLf & ") AS CT ON C.FTInvoiceBankNo=CT.FTInvoiceBankNo"
        '-------------------------------------------------------------------------------
        _Cmd &= vbCrLf & "	 LEFT OUTER JOIN("
        _Cmd &= vbCrLf & "select  C.FTInvoiceBankNo,RC.FDInvoiceDate,P.FTInvoiceNo,PO.FTPurchaseNo,sum(RD.FNQuantity)as FNQuantity,isnull(sum(RD.FNNetAmt)+((sum(RD.FNNetAmt)*PO.FNVatPer)/100),0) as PO,RC.FDReceiveDate,RD.FTReceiveNo"
        _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge AS C"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS P ON C.FTInvoiceBankNo=P.FTInvoiceBankNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Purchase_detail AS PB  ON C.FTInvoiceBankNo = PB.FTInvoiceBankNo "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON P.FTInvoiceNo = RC.FTInvoiceNo and PB.FTPurchaseNo=RC.FTPurchaseNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK) ON  RC.FTReceiveNo=RD.FTReceiveNo "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS PO ON PB.FTPurchaseNo=PO.FTPurchaseNo"
        _Cmd &= vbCrLf & "group by C.FTInvoiceBankNo, RC.FDInvoiceDate,PO.FTPurchaseNo,RC.FDReceiveDate,RD.FTReceiveNo,PO.FNVatPer,P.FTInvoiceNo"
        _Cmd &= vbCrLf & ")AS DRD ON  C.FTInvoiceBankNo=DRD.FTInvoiceBankNo and P.FTPurchaseNo=DRD.FTPurchaseNo"
        '----------------------------------------------------------------------------
        _Cmd &= vbCrLf & "	 LEFT OUTER JOIN("
        _Cmd &= vbCrLf & "   select isnull(A1.FTPurchaseNo,A2.FTPurchaseNo)as FTPurchaseNo ,isnull(A1.FTInvoiceBankNo,A2.FTInvoiceBankNo)as FTInvoiceBankNo,isnull(A1.FTInvoiceNo,A2.FTInvoiceNo)as FTInvoiceNo,isnull(A1.FNNetAmt,A2.FNNetAmt)as FNNetAmt ,isnull(A1.FNAmount,A2.FNAmount) as FNAmount,A1.FTUnitCode "
        _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge AS C "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS I ON C.FTInvoiceBankNo=I.FTInvoiceBankNo "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Purchase_detail AS PD ON C.FTInvoiceBankNo=PD.FTInvoiceBankNo"
        _Cmd &= vbCrLf & "	 LEFT OUTER JOIN("
        _Cmd &= vbCrLf & " select PD.FTPurchaseNo,UN.FTUnitCode,PD.FTInvoiceBankNo,RC.FTInvoiceNo,sum(RO.FNNetAmt)+isnull((sum(RO.FNNetAmt)*P.FNVatPer)/100,0) as FNNetAmt,(sum(RO.FNNetAmt)+isnull((sum(RO.FNNetAmt)*P.FNVatPer)/100,0))*AP.TT as FNAmount"
        _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Purchase_detail AS PD ON P.FTPurchaseNo=PD.FTPurchaseNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS I ON PD.FTInvoiceBankNo=I.FTInvoiceBankNo "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON PD.FTPurchaseNo = RC. FTPurchaseNo and I.FTInvoiceNo=RC.FTInvoiceNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK) ON  RC.FTReceiveNo=RD.FTReceiveNo "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail_Order AS RO WITH(NOLOCK) ON  RC.FTReceiveNo=RO.FTReceiveNo and RD.FNHSysRawMatId=RO.FNHSysRawMatId "

        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Order AS IO ON  RO.FTOrderNo=IO.FTOrderNo  and PD.FTInvoiceBankNo=IO.FTInvoiceBankNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS UN WITH(NOLOCK) ON RD.FNHSysUnitId =UN.FNHSysUnitId"
        _Cmd &= vbCrLf & "	 LEFT OUTER JOIN("
        _Cmd &= vbCrLf & "     select c.FTInvoiceBankNo ,C.FNGAmount /C.FNNetAmount as TT,count(I.FTInvoiceNo)as NumInv"
        _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge AS C "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS I ON C.FTInvoiceBankNo=I.FTInvoiceBankNo"
        _Cmd &= vbCrLf & " where  C.FNNetAmount>0 and FNChargeNetAmount>0"
        _Cmd &= vbCrLf & "group by c.FTInvoiceBankNo,C.FNNetAmount,C.FNChargeNetAmount,C.FNGAmount"
        _Cmd &= vbCrLf & ")as AP ON  PD.FTInvoiceBankNo=AP.FTInvoiceBankNo"
        ' _Cmd &= vbCrLf & " where AP.NumInv > 1"
        _Cmd &= vbCrLf & "  group by PD.FTPurchaseNo,UN.FTUnitCode,PD.FTInvoiceBankNo,P.FNVatPer,RC.FTInvoiceNo,AP.TT  "
        _Cmd &= vbCrLf & "  )as A1 ON  C.FTInvoiceBankNo=A1.FTInvoiceBankNo and I.FTInvoiceNo=A1.FTInvoiceNo and PD.FTPurchaseNo=A1.FTPurchaseNo"
        _Cmd &= vbCrLf & "	 LEFT OUTER JOIN("
        _Cmd &= vbCrLf & "  select  PD.FTPurchaseNo,PD.FTInvoiceBankNo ,I.FTInvoiceNo,(AP.FNNetAmount)as FNNetAmt,AP.FNGAmount as FNAmount"
        _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Purchase_detail AS PD"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS I ON PD.FTInvoiceBankNo=I.FTInvoiceBankNo"
        _Cmd &= vbCrLf & "	 LEFT OUTER JOIN("
        _Cmd &= vbCrLf & "   select c.FTInvoiceBankNo,C.FNNetAmount,C.FNGAmount,count(I.FTInvoiceNo)as NumInv"
        _Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge AS C "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS I ON C.FTInvoiceBankNo=I.FTInvoiceBankNo"
        _Cmd &= vbCrLf & " group by c.FTInvoiceBankNo,C.FNNetAmount,C.FNGAmount"
        _Cmd &= vbCrLf & ")as AP  ON  PD.FTInvoiceBankNo=AP.FTInvoiceBankNo"
        _Cmd &= vbCrLf & " where AP.NumInv = 0"
        _Cmd &= vbCrLf & "  )as A2 ON C.FTInvoiceBankNo=A2.FTInvoiceBankNo and I.FTInvoiceNo=A2.FTInvoiceNo and PD.FTPurchaseNo=A1.FTPurchaseNo"
        _Cmd &= vbCrLf & " )AS IPO  ON DRD.FTPurchaseNo=IPO.FTPurchaseNo and DRD.FTInvoiceBankNo=IPO.FTInvoiceBankNo   and DRD.FTInvoiceNo=IPO.FTInvoiceNo  and UN.FTUnitCode=IPO.FTUnitCode "
        '------------------------------
        _Cmd &= vbCrLf & " LEFT OUTER JOIN ("
        _Cmd &= vbCrLf & "select AM.FTInvoiceBankNo,AM.FTPurchaseNo,AM.FTInvoiceNo,AM.AM AS Debit,AM.PO,AM.AMT"
        _Cmd &= vbCrLf & "from (select TY.FTInvoiceBankNo,RC.FTPurchaseNo,RC.FTInvoiceNo,sum(RD.FNQuantity) as FNQuantity,sum(RD.FNQuantity)*TY.AM as AM,isnull(sum(RD.FNNetAmt)+((sum(RD.FNNetAmt)*PO.FNVatPer)/100),0) as PO,isnull(sum(RD.FNNetAmt)+((sum(RD.FNNetAmt)*PO.FNVatPer)/100),0) +sum(RD.FNQuantity)*TY.AM as AMT"
        _Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit AS D"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS DI ON D.FTDebitCreditNo=DI.FTDebitCreditNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase AS DP ON D.FTDebitCreditNo=DP.FTDebitCreditNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON  DI.FTInvoiceNo = RC.FTInvoiceNo and DP.FTPurchaseNo=RC.FTPurchaseNo "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK) ON  RC.FTReceiveNo=RD.FTReceiveNo "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS PO ON RC.FTPurchaseNo=PO.FTPurchaseNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN ("
        _Cmd &= vbCrLf & "select  P.FTInvoiceBankNo,D.FNDebitCreditGrandAmt /sum(RD.FNQuantity)as AM,D.FNDebitCreditGrandAmt,D.FNDocDebitCreditState,D.FTDebitCreditNo"
        _Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS P"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Purchase_detail AS PD ON P.FTInvoiceBankNo=PD.FTInvoiceBankNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON  P.FTInvoiceNo = RC.FTInvoiceNo and PD.FTPurchaseNo=RC.FTPurchaseNo "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK)  ON  RC.FTReceiveNo=RD.FTReceiveNo "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN ("
        _Cmd &= vbCrLf & "select D.FNDocDebitCreditState,isnull(DI.FTInvoiceNo,D.FTInvoiceNo)as FTInvoiceNo ,isnull(DP.FTPurchaseNo,C.FTCustCode)as FTPurchaseNo,isnull(D.FNHSysCmpIdTo ,D.FNHSysSuplId ) as CC,D.FTDebitCreditNo,D.FNDebitCreditGrandAmt"
        _Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit AS D"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS DI ON D.FTDebitCreditNo=DI.FTDebitCreditNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase AS DP ON D.FTDebitCreditNo=DP.FTDebitCreditNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C ON D.FNHSysCmpIdTo =C.FNHSysCustId"
        _Cmd &= vbCrLf & ")AS D ON  RC.FTInvoiceNo=D.FTInvoiceNo and PD.FTPurchaseNo=D.FTPurchaseNo "
        _Cmd &= vbCrLf & "group by P.FTInvoiceBankNo,D.FNDocDebitCreditState,D.FNDebitCreditGrandAmt,D.FTDebitCreditNo"
        _Cmd &= vbCrLf & ")As TY ON D.FTDebitCreditNo=TY.FTDebitCreditNo"
        _Cmd &= vbCrLf & "where TY.FNDocDebitCreditState='0' "
        _Cmd &= vbCrLf & "group by TY.FTInvoiceBankNo,RC.FTInvoiceNo,TY.AM ,RC.FTPurchaseNo,PO.FNVatPer"
        _Cmd &= vbCrLf & "UNION ALL "
        _Cmd &= vbCrLf & "select TY.FTInvoiceBankNo,RC.FTPurchaseNo,RC.FTInvoiceNo,sum(RD.FNQuantity) as FNQuantity,sum(RD.FNQuantity)*TY.AM as AM,isnull(sum(RD.FNNetAmt)+((sum(RD.FNNetAmt)*PO.FNVatPer)/100),0) as PO,isnull(sum(RD.FNNetAmt)+((sum(RD.FNNetAmt)*PO.FNVatPer)/100),0)-sum(RD.FNQuantity)*TY.AM as AMT"
        _Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit AS D"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS DI ON D.FTDebitCreditNo=DI.FTDebitCreditNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase AS DP ON D.FTDebitCreditNo=DP.FTDebitCreditNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON  DI.FTInvoiceNo = RC.FTInvoiceNo and DP.FTPurchaseNo=RC.FTPurchaseNo "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK) ON  RC.FTReceiveNo=RD.FTReceiveNo "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS PO ON RC.FTPurchaseNo=PO.FTPurchaseNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN ("
        _Cmd &= vbCrLf & "select  P.FTInvoiceBankNo,D.FNDebitCreditGrandAmt /sum(RD.FNQuantity)as AM,D.FNDebitCreditGrandAmt,D.FNDocDebitCreditState,D.FTDebitCreditNo"
        _Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS P"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Purchase_detail AS PD ON P.FTInvoiceBankNo=PD.FTInvoiceBankNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON  P.FTInvoiceNo = RC.FTInvoiceNo and PD.FTPurchaseNo=RC.FTPurchaseNo "
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK)  ON  RC.FTReceiveNo=RD.FTReceiveNo "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN ("
        _Cmd &= vbCrLf & "select D.FNDocDebitCreditState,isnull(DI.FTInvoiceNo,D.FTInvoiceNo)as FTInvoiceNo ,isnull(DP.FTPurchaseNo,C.FTCustCode)as FTPurchaseNo,isnull(D.FNHSysCmpIdTo ,D.FNHSysSuplId ) as CC,D.FTDebitCreditNo,D.FNDebitCreditGrandAmt"
        _Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit AS D"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS DI ON D.FTDebitCreditNo=DI.FTDebitCreditNo"
        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Purchase AS DP ON D.FTDebitCreditNo=DP.FTDebitCreditNo"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C ON D.FNHSysCmpIdTo =C.FNHSysCustId"
        _Cmd &= vbCrLf & ")AS D ON  RC.FTInvoiceNo=D.FTInvoiceNo and PD.FTPurchaseNo=D.FTPurchaseNo "
        _Cmd &= vbCrLf & "group by P.FTInvoiceBankNo,D.FNDocDebitCreditState,D.FNDebitCreditGrandAmt,D.FTDebitCreditNo"
        _Cmd &= vbCrLf & ")As TY ON D.FTDebitCreditNo=TY.FTDebitCreditNo"
        _Cmd &= vbCrLf & "where TY.FNDocDebitCreditState='1'"
        _Cmd &= vbCrLf & "group by TY.FTInvoiceBankNo,RC.FTInvoiceNo,TY.AM ,RC.FTPurchaseNo,PO.FNVatPer )as AM "
        _Cmd &= vbCrLf & ")AS DA ON DRD.FTInvoiceBankNo=DA.FTInvoiceBankNo and IPO.FTInvoiceNo=DA.FTInvoiceNo and DRD.FTPurchaseNo=DA.FTPurchaseNo"
        _Cmd &= vbCrLf & ""


        '_Cmd &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_Description"
        ''_Cmd &= vbCrLf & "where FTStatePI='1'"
        '_Cmd &= vbCrLf & "  )AS MD ON MM.FNHSysMainMatId=MD.FNHSysMainMatId"
        '_Cmd &= vbCrLf & "select B.FNPIChargeType,C.FTChageNameTH,B.FTInvoiceBankNo"
        '_Cmd &= vbCrLf & "from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TEXPChage AS C "
        '_Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Charge AS IC ON C.FNHSysPIChageId=IC.FNHSysPIChageId"
        '_Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge AS B ON IC.FTInvoiceBankNo=B.FTInvoiceBankNo and C.FNPIType=B.FNPIChargeType"
        '_Cmd &= vbCrLf & "where  B.FNPIChargeType<>'0'  "
        '_Cmd &= vbCrLf & ") as B  ON C.FTInvoiceBankNo=B.FTInvoiceBankNo "
        '_Cmd &= vbCrLf & " LEFT OUTER JOIN ("
        '_Cmd &= vbCrLf & "select  C.FTInvoiceBankNo,sum(C.FNAmount) AS FNNetAmt"
        '_Cmd &= vbCrLf & "from    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Charge AS C"
        '_Cmd &= vbCrLf & " group by C.FTInvoiceBankNo "
        '_Cmd &= vbCrLf & ")AS AC ON C.FTInvoiceBankNo=AC.FTInvoiceBankNo"
        ''-------------------
        '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMMatType AS MT WITH(NOLOCK) ON MM.FNHSysMatTypeId = MT.FNHSysMatTypeId"
        '_Cmd &= vbCrLf & " LEFT OUTER JOIN ("
        '_Cmd &= vbCrLf & "select FTCurCode,FNHSysCurId,FTInvoiceBankNo"
        '_Cmd &= vbCrLf & " from("
        '_Cmd &= vbCrLf & "   Select Max(FTCurCode) AS FTCurCode,FNHSysCurId,FTInvoiceBankNo"
        '_Cmd &= vbCrLf & " FROM (SELECT MAX(ISNULL(CU.FTCurCode,'')) AS FTCurCode,CU.FNHSysCurId,PD.FTInvoiceBankNo"
        '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK)  "
        '_Cmd &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail AS PD ON P.FTPurchaseNo=PD.FTPurchaseNo"
        '_Cmd &= vbCrLf & "      LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CU WITH(NOLOCK) ON P.FNHSysCurId = CU.FNHSysCurId"
        '_Cmd &= vbCrLf & " group by CU.FNHSysCurId ,PD.FTInvoiceBankNo) AS A"
        '_Cmd &= vbCrLf & " 	group by FNHSysCurId,FTInvoiceBankNo"
        '_Cmd &= vbCrLf & "  union all"
        '_Cmd &= vbCrLf & " Select  Max(C.FTCurCode) AS FTCurCode ,T.FNHSysCurId,T.FTInvoiceBankNo"
        '_Cmd &= vbCrLf & " FROM (  Select  B.FNHSysCurId ,PD.FTInvoiceBankNo "
        '_Cmd &= vbCrLf & " From    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S "
        '_Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS B WITH(NOLOCK) ON S.FTOrderNo = B.FTOrderNo and S.FTSubOrderNo = B.FTSubOrderNo "
        '_Cmd &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail AS PD ON S.FTPOref=PD.FTPurchaseNo"
        '_Cmd &= vbCrLf & "   group by  B.FNHSysCurId ,PD.FTInvoiceBankNo ) AS T "
        '_Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS C WITH(NOLOCK) ON T.FNHSysCurId = C.FNHSysCurId "
        '_Cmd &= vbCrLf & "   group by T.FNHSysCurId,T.FTInvoiceBankNo"
        '_Cmd &= vbCrLf & " union all"
        '_Cmd &= vbCrLf & " Select  Max(C.FTCurCode) AS FTCurCode ,T.FNHSysCurId,T.FTInvoiceBankNo"
        '_Cmd &= vbCrLf & " FROM (  Select  C.FNHSysCurId ,PD.FTInvoiceBankNo "
        '_Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS C WITH(NOLOCK)"
        '_Cmd &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail AS PD ON  C.FTCustCode = PD.FTPurchaseNo"
        '_Cmd &= vbCrLf & "   group by C.FNHSysCurId ,PD.FTInvoiceBankNo ) AS T "
        '_Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS C WITH(NOLOCK) ON T.FNHSysCurId = C.FNHSysCurId "
        '_Cmd &= vbCrLf & "   	group by T.FNHSysCurId,T.FTInvoiceBankNo )AS CUR"
        '_Cmd &= vbCrLf & "    )as CU on C.FTInvoiceBankNo=CU.FTInvoiceBankNo"
        '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON P.FTPurchaseNo = RC.FTPurchaseNo "
        '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK) ON  RC.FTReceiveNo=RD.FTReceiveNo "

        '-------------------------------------------------------------------------------



        '-------------------------------------------------------------------------



        '------------------------------------------------------------------------
        _Cmd &= vbCrLf & "Where C.FTInvoiceBankNo <> '' "
        If Me.FNHSysSuplId.Text <> "" Then
            _Cmd &= vbCrLf & " And  S.FTSuplCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "'"
        End If
        If Me.FNHSysSuplIdTo.Text <> "" Then
            _Cmd &= vbCrLf & " And  S.FTSuplCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplIdTo.Text) & "'"
        End If

        If Me.FTInvoiceBankNo.Text <> "" Then
            _Cmd &= vbCrLf & " And   C.FTInvoiceBankNo >='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "'"
        End If
        If Me.FTInvoiceBankNoTo.Text <> "" Then
            _Cmd &= vbCrLf & " And   C.FTInvoiceBankNo <='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNoTo.Text) & "'"
        End If

        If Me.FTPurchaseNo.Text <> "" Then
            _Cmd &= vbCrLf & " And   ( SELECT        TOP 1 STUFF"
            _Cmd &= vbCrLf & "      ((SELECT        ', ' + t2.FTPurchaseNo      FROM (SELECT       FTPurchaseNo ,FTInvoiceBankNo"
            _Cmd &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail  WITH (NOLOCK) "
            _Cmd &= vbCrLf & "   GROUP BY FTPurchaseNo ,FTInvoiceBankNo ) t2"
            _Cmd &= vbCrLf & "  WHERE     t2.FTInvoiceBankNo = C.FTInvoiceBankNo   FOR XML PATH('')), 1, 2, '') AS FTPurchaseNo) >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
        End If
        If Me.FTPurchaseNoTo.Text <> "" Then
            _Cmd &= vbCrLf & " And    ( SELECT        TOP 1 STUFF"
            _Cmd &= vbCrLf & "      ((SELECT        ', ' + t2.FTPurchaseNo      FROM (SELECT       FTPurchaseNo ,FTInvoiceBankNo"
            _Cmd &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail  WITH (NOLOCK) "
            _Cmd &= vbCrLf & "   GROUP BY FTPurchaseNo ,FTInvoiceBankNo ) t2"
            _Cmd &= vbCrLf & "  WHERE     t2.FTInvoiceBankNo = C.FTInvoiceBankNo   FOR XML PATH('')), 1, 2, '') AS FTPurchaseNo) <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "'"
        End If

        If Me.FTStartDate.Text <> "" Then
            _Cmd &= vbCrLf & " And C.FDPrepareDate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
        End If
        If Me.FTEndDate.Text <> "" Then
            _Cmd &= vbCrLf & " And C.FDPrepareDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
        End If

        _Cmd &= vbCrLf & "group by C.FTInvoiceBankNo  ,DRD.FNQuantity, P.FTPurchaseNo,DRD.FDInvoiceDate,DRD.FDReceiveDate,IPO.FNAmount,IPO.FNNetAmt,C.FNChargeNetAmount,IPO.FTInvoiceNo,DA.Debit,isnull( U.FTCustNameTH,CT.FTCustNameTH) , U.FTCustNameEN,CT.FTCustNameEN , S.FTSuplNameEN , S.FTSuplNameTH ,C.FNPIChargeType ,MD.FTMainMatNameTH , MD.FTMainMatNameEN"
        _Cmd &= vbCrLf & " ,PH.FNPONetAmt  , S.FTSuplCode,CCV.FNAmount,PH.FNPOGrandAmt,DA.AMT, U.FTCustCode,PH.FTPurchaseBy,S.FTBnkAccNo ,R.FTCurCode, T.FTCrTermCode,UN.FTUnitCode ,C.FTRemark,C.FDPrepareDate,C.FDPayDate,PH.FDDeliveryDate"
        _Cmd &= vbCrLf & "Order by   S.FTSuplCode asc , C.FTInvoiceBankNo asc" 'LS.FTNameTH asc "

        _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogdtime.DataSource = _dt
        Me.ogvtime.ExpandAllGroups()
        _Spls.Close()
        _RowDataChange = False
        Call InitialGridSummaryMergCell()
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

     

        If Me.FNHSysSuplId.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysSuplIdTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNoTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTInvoiceBankNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTInvoiceBankNoTo.Text <> "" Then
            _Pass = True
        End If
        If Me.FTStartDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndDate.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

 

    'Private totalSum As Double = 0
    'Private GrpSum As Integer = 0
    'Private _RowHandleHold As Integer = 0

    'Private Sub InitSummaryStartValue()
    '    totalSum = 0
    '    GrpSum = 0
    '    _RowHandleHold = 0
    'End Sub

    'Private Sub ogvtime_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
    '    Try
    '        If e.SummaryProcess = CustomSummaryProcess.Start Then
    '            InitSummaryStartValue()
    '        End If

    '        With Me.ogvtime
    '            Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
    '                Case "FNAmount"
    '                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
    '                        If e.IsTotalSummary Then
    '                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
    '                                If (.GetRowCellValue(e.RowHandle, "FTSuplName").ToString <> .GetRowCellValue(_RowHandleHold, "FTSuplName").ToString) Or e.RowHandle = _RowHandleHold Then
    '                                    totalSum = totalSum + Double.Parse(Val(e.FieldValue.ToString))

    '                                End If
    '                            End If
    '                            _RowHandleHold = e.RowHandle
    '                        End If
    '                        e.TotalValue = totalSum
    '                    End If
    '            End Select
    '        End With


    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

   

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        Try
            'Dim Str As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 B.FTInvoiceNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Invoice_detail as B WITH(NOLOCK) WHERE  B.FTInvoiceBankNo ='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
            'Dim _Cre As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 D.FNDocDebitCreditState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit as D WITH(NOLOCK) LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS DP ON D.FTDebitCreditNo=DP.FTDebitCreditNo WHERE  DP.FTInvoiceNo in (" & Str & ") ", Conn.DB.DataBaseName.DB_MASTER, "")
            With Me.ogvtime
                'If _Cre = "1" Then
                Select Case e.Column.FieldName
                    Case "FNDebitCredit"
                        If String.Format(.GetRowCellValue(e.RowHandle, "FTInvoiceBankNo")) <> "" Then
                            '   e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
               
                    Case "FDPayDate"
                        If String.Format(.GetRowCellValue(e.RowHandle, "FTInvoiceBankNo")) <> "" Then
                            '   e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        End If
                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub

End Class
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports System.Net

Public Class wFactoryInvoiceCM_ACC_CVN

    ''Private _InvoiceExport As wFactoryInvoiceCMInvoiceExport

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '_InvoiceExport = New wFactoryInvoiceCMInvoiceExport
        'HI.TL.HandlerControl.AddHandlerObj(_InvoiceExport)

        Dim oSysLang As New ST.SysLanguage
        Try
            ''  Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _InvoiceExport.Name.ToString.Trim, _InvoiceExport)
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub InitGrid()

        'Try
        '    With Me.ogvbreakdown
        '        For I As Integer = .Columns.Count - 1 To 0 Step -1
        '            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '        Next
        '    End With
        'Catch ex As Exception
        'End Try

        'With ogvbreakdown
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        '    .OptionsMenu.EnableColumnMenu = False
        '    .OptionsMenu.ShowAutoFilterRowItem = False
        '    .OptionsFilter.AllowFilterEditor = False
        '    .OptionsFilter.AllowColumnMRUFilterList = False
        '    .OptionsFilter.AllowMRUFilterList = False
        'End With

    End Sub

    Private _SysDBName As String = "HITECH_ACCOUNT"
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = "TACCTFactoryCMInvoice"
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(value As String)
            _SysDocType = value
        End Set
    End Property

    Private _SysDocClear As Boolean = False
    Public Property SysDocClear As Boolean
        Get
            Return _SysDocClear
        End Get
        Set(value As Boolean)
            _SysDocClear = value
        End Set
    End Property


    Public Sub DefaultsData()

        Me.FDInvoiceDate.DateTime = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
    End Sub

    Private _dtcuspobreakdown As System.Data.DataTable
    Private _dtcuspobreakdownspare As System.Data.DataTable
    Private Sub ClearGrid(Optional Prod As Boolean = False)

        'With Me.ogvbreakdown
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper
        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper,
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next
        'End With

    End Sub

    Private _LoadInvoice As Boolean = False

    Public Sub LoadOrderPriceInfo(ByVal Key As Object)
        'If _StateClear Then Exit Sub
        'Dim _Qry As String = ""
        'Dim _Filter As String = ""
        'Dim _oDt As System.Data.DataTable
        'Dim dtt As System.Data.DataTable

        '_Qry = "  SELECT TOP 1 A.FNHSysContinentId, A.FNHSysCountryId, A.FNHSysProvinceId"
        '_Qry &= vbCrLf & " , A.FNHSysShipModeId, A.FNHSysShipPortId, B.FTContinentCode, C.FTCountryCode"
        '_Qry &= vbCrLf & " , D.FTProvinceCode, E.FTShipModeCode,    F.FTShipPortCode"
        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A INNER JOIN"
        '_Qry &= vbCrLf & "           HITECH_MASTER.dbo.TCNMShipMode AS E ON A.FNHSysShipModeId = E.FNHSysShipModeId INNER JOIN"
        '_Qry &= vbCrLf & "           HITECH_MASTER.dbo.TCNMShipPort AS F ON A.FNHSysShipPortId = F.FNHSysShipPortId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "           HITECH_MASTER.dbo.TCMMProvince AS D ON A.FNHSysProvinceId = D.FNHSysProvinceId AND A.FNHSysCountryId = D.FNHSysCountryId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "           HITECH_MASTER.dbo.TCNMContinent AS B ON A.FNHSysContinentId = B.FNHSysContinentId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "           HITECH_MASTER.dbo.TCNMCountry AS C ON A.FNHSysCountryId = C.FNHSysCountryId AND A.FNHSysContinentId = C.FNHSysContinentId"
        '_Qry &= vbCrLf & "   WHERE  (A.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        '_Qry &= vbCrLf & "    AND (A.FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "')"

        'dtt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        'For Each R As DataRow In dtt.Rows

        '    FNHSysContinentId.Text = R!FTContinentCode.ToString
        '    FNHSysCountryId.Text = R!FTCountryCode.ToString
        '    FNHSysProvinceId.Text = R!FTProvinceCode.ToString
        '    FNHSysShipModeId.Text = R!FTShipModeCode.ToString
        '    FNHSysShipPortId.Text = R!FTShipPortCode.ToString

        '    Exit For

        'Next

        'Call LoadOrderBreakDown(FTCustomerPO.Text)

        'Dim _dt As System.Data.DataTable
        'Dim _dtPrice As System.Data.DataTable
        'With CType(Me.ogcbreakdown.DataSource, System.Data.DataTable)
        '    .AcceptChanges()
        '    _dt = .Copy
        'End With

        '_LoadInvoice = True


        'Dim _dtcminvoice As System.Data.DataTable

        '_Qry = "   SELECT Top 1 FTCustomerPO, FTInvoiceNo, FTInvoiceExportNo,FDInvoiceExportDate,FNTotalCarton,FTStateHanger"
        '_Qry &= vbCrLf & " , Isnull(FTStateSendApp,'0') AS FTStateSendApp , Isnull(FTStateApp,'0') AS FTStateApp , ISNULL(FTStateWHApp,'0') AS FTStateWHApp "

        '_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK)"
        '_Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        '_Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

        '_dtcminvoice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        'Me.FTStateSendApp.EditValue = "0"
        'Me.FTStateApp.EditValue = "0"
        'Me.FTWHStateApp.EditValue = "0"
        'FTInvoiceExportNo.Text = ""
        'FDInvoiceExportDate.Text = ""
        ''FNTotalCarton.Value = 0
        'FTStateHanger.Checked = False
        'For Each Rxi As DataRow In _dtcminvoice.Rows

        '    Me.FTStateSendApp.EditValue = Rxi!FTStateSendApp.ToString
        '    Me.FTStateApp.EditValue = Rxi!FTStateApp.ToString
        '    Me.FTWHStateApp.EditValue = Rxi!FTStateWHApp.ToString

        '    FTInvoiceExportNo.Text = Rxi!FTInvoiceExportNo.ToString
        '    FDInvoiceExportDate.Text = HI.UL.ULDate.ConvertEN(Rxi!FDInvoiceExportDate.ToString)
        '    FNTotalCarton.Value = Val(Rxi!FNTotalCarton.ToString)
        '    FTStateHanger.Checked = (Rxi!FTStateHanger.ToString = "1")

        'Next

        '_Qry = "   SELECT FTCustomerPO, FTInvoiceNo, FTColorway, FTSizeBreakDown, FNQuantity,FTPOLineItem  AS FTNikePOLineItem"
        '_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS A WITH(NOLOCK)"
        '_Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        '_Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "')"
        '_dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        'If _dtPrice.Rows.Count <= 0 Then
        '    ' Call FTCustomerPO_EditValueChanged(Me.FTCustomerPO, Nothing)





        '    _LoadInvoice = False
        '    Exit Sub
        'End If



        'For Each R As DataRow In _dt.Rows
        '    For Each Col As DataColumn In _dt.Columns
        '        Select Case Col.ColumnName.ToString.ToUpper
        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
        '            Case "FTNikePOLineItem".ToUpper

        '            Case Else

        '                _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
        '                ' _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "

        '                If _dtPrice.Select(_Filter).Length > 0 Then

        '                    For Each Rx As DataRow In _dtPrice.Select(_Filter)
        '                        R.Item(Col.ColumnName.ToString) = Val(Rx!FNQuantity.ToString)
        '                        Exit For
        '                    Next

        '                Else
        '                    R.Item(Col.ColumnName.ToString) = System.DBNull.Value
        '                End If

        '        End Select
        '    Next

        'Next

        'Me.ogcbreakdown.DataSource = _dt.Copy
        'SetCartonQty()

        'Call CalculateSumGrid()


        _LoadInvoice = False
    End Sub

    Public Sub LoadOrderDataInfo(ByVal Key As Object)

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        Call ClearGrid()
        ''  Call LoadListOrderInfo(Me.FTCustomerPO.Text.Trim())
        '' Call LoadOrderBreakDown(Key)


        Call LoadInvoice()
        Call LoadData()

        _Spls.Close()

    End Sub


    Private Sub LoadCustomerAddress()
        _LoadInvoice = True
        Dim _Qry As String = ""
        Dim dt As System.Data.DataTable

        _Qry = "  SELECT TOP 1 FTAddr1TH +' ' + FTPostCode as address "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer "
        _Qry &= vbCrLf & "  WHERE  (FTCustCode = N'" & HI.UL.ULF.rpQuoted(FNHSysCustId.Text) & "')"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In dt.Rows
            FTAddress.Text = R!address.ToString

            Exit For
        Next

        '' Call LoadOrderBreakDown(FTCustomerPO.Text)

        _LoadInvoice = False

    End Sub



    'Private Sub LoadListOrderInfo(ByVal _FTPORef As String)
    '    Dim _Str As String = ""

    '    Dim _FNHSysCmpId As Integer = 0

    '    _Str = "SELECT TOP 1 FNHSysCmpId"
    '    _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS X WITH(NOLOCK)"
    '    _Str &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

    '    _FNHSysCmpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

    '    If _FNHSysCmpId <= 0 Then
    '        _FNHSysCmpId = Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString))
    '    End If

    '    '_Str = "SELECT '0' AS FNSelect"
    '    '_Str &= vbCrLf & "   , A.FTOrderNo"
    '    '_Str &= vbCrLf & "  ,SEAS.FTSeasonCode,ISNULL(PMC.FTVenderPramCode,'') AS FTPGMCode  "
    '    '_Str &= vbCrLf & "   , CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) "
    '    '_Str &= vbCrLf & "         ELSE '' END AS FDOrderDate, ISNULL"
    '    '_Str &= vbCrLf & "                  ((SELECT     CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate"
    '    '_Str &= vbCrLf & "                      FROM         (SELECT     X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate"
    '    '_Str &= vbCrLf & "                                             FROM          HITECH_MERCHAN.dbo.TMERTOrder AS X INNER JOIN"
    '    '_Str &= vbCrLf & "                                                                    HITECH_MERCHAN.dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo"
    '    '_Str &= vbCrLf & "                                             GROUP BY X.FTOrderNo) AS L1"
    '    '_Str &= vbCrLf & "                      WHERE     (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, A.FNHSysCustId, C.FTCustCode, C.FTCustNameEN AS FTCustName"
    '    '_Str &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
    '    '_Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId"
    '    '_Str &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId  "
    '    '_Str &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON "
    '    '_Str &= vbCrLf & "  	A.FNHSysSeasonId = SEAS.FNHSysSeasonId"
    '    '_Str &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PMC WITH(NOLOCK)  ON  A.FNHSysVenderPramId=PMC.FNHSysVenderPramId"
    '    '_Str &= vbCrLf & " WHERE A.FTOrderNo IN ("
    '    '_Str &= vbCrLf & " SELECT O.FTOrderNo"
    '    '_Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
    '    '_Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
    '    '_Str &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
    '    '_Str &= vbCrLf & "         AND O.FNHSysCmpId=" & _FNHSysCmpId & ""
    '    '_Str &= vbCrLf & " GROUP BY O.FTOrderNo"
    '    '_Str &= vbCrLf & " ) AND A.FNJobState=1 "
    '    '_Str &= vbCrLf & "   ORDER BY  A.FTOrderNo"


    '    _Str = "    SELECT A.FTOrderNo, A.FTSubOrderNo"
    '    _Str &= vbCrLf & " ,CASE WHEN ISDATE(A.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME,  A.FDShipDate), 103)  ELSE '' END AS FDShipDate"
    '    _Str &= vbCrLf & " ,CASE WHEN ISDATE(B.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME,  B.FDOrderDate), 103)  ELSE '' END AS FDOrderDate"
    '    _Str &= vbCrLf & " , SEAS.FTSeasonCode"
    '    _Str &= vbCrLf & " , B.FNHSysCustId"
    '    _Str &= vbCrLf & " , C.FTCustCode"
    '    _Str &= vbCrLf & " , C.FTCustNameTH  AS FTCustName "
    '    _Str &= vbCrLf & " , PMC.FTVenderPramCode AS FTPGMCode"
    '    _Str &= vbCrLf & " , ISNULL(B.FTUpdUser,B.FTInsUser) AS FTInsUser"
    '    _Str &= vbCrLf & " , BG.FTBuyGrpCode, "
    '    _Str &= vbCrLf & "     BG.FNQtySpecialType"
    '    _Str &= vbCrLf & " , BG.FNQtySpecial"
    '    _Str &= vbCrLf & " , PVC.FTProvinceCode"
    '    _Str &= vbCrLf & " , PVC.FTProvinceNameEN AS FTProvinceName"
    '    _Str &= vbCrLf & " , Cmp.FTCmpCode"
    '    _Str &= vbCrLf & " ,CASE WHEN ISNULL(Cmp.FTStateShipOverQty,'') ='1' AND ISNULL(PVC.FTStateShipOverQty,'') ='1' THEN BG.FNQtySpecial ELSE 0.00 END AS FNShipOverQty"
    '    _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS A INNER JOIN"
    '    _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B ON A.FTOrderNo = B.FTOrderNo LEFT OUTER JOIN"
    '    _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp ON B.FNHSysCmpId = Cmp.FNHSysCmpId LEFT OUTER JOIN"
    '    _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS PVC ON A.FNHSysProvinceId = PVC.FNHSysProvinceId LEFT OUTER JOIN"
    '    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS BG ON B.FNHSysBuyGrpId = BG.FNHSysBuyGrpId LEFT OUTER JOIN"
    '    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram AS PMC ON B.FNHSysVenderPramId = PMC.FNHSysVenderPramId LEFT OUTER JOIN"
    '    _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C ON B.FNHSysCustId = C.FNHSysCustId LEFT OUTER JOIN"
    '    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SEAS ON B.FNHSysSeasonId = SEAS.FNHSysSeasonId"
    '    _Str &= vbCrLf & "  WHERE A.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
    '    _Str &= vbCrLf & "         AND B.FNHSysCmpId=" & _FNHSysCmpId & ""

    '    _Str &= vbCrLf & "   GROUP BY A.FTOrderNo, A.FTSubOrderNo, A.FDShipDate, B.FDOrderDate, SEAS.FTSeasonCode, C.FTCustCode, C.FTCustNameTH, PMC.FTVenderPramCode,  ISNULL(B.FTUpdUser,B.FTInsUser) , BG.FTBuyGrpCode, "
    '    _Str &= vbCrLf & "  BG.FNQtySpecialType, BG.FNQtySpecial, PVC.FTStateShipOverQty, PVC.FTProvinceCode, PVC.FTProvinceNameEN, Cmp.FTCmpCode, Cmp.FTStateShipOverQty, B.FNHSysCustId"



    '    Dim dt As System.Data.DataTable
    '    dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
    '    ogvOrderList.OptionsView.ShowAutoFilterRow = False

    '    Me.GridOrderList.DataSource = dt.Copy

    '    Me.GridOrderList.Refresh()

    'End Sub

    'Private Sub LoadOrderBreakDown(Key As Object)
    '    If _StateClear Then Exit Sub

    '    Dim _dt As System.Data.DataTable
    '    Dim _Qry As String = ""
    '    Dim _colcount As Integer = 0
    '    Dim _FNHSysContinentId As Integer = 0
    '    Dim _FNHSysCountryId As Integer = 0
    '    Dim _FNHSysProvinceId As Integer = 0
    '    Dim _FNHSysShipModeId As Integer = 0
    '    Dim _FNHSysShipPortId As Integer = 0

    '    _FNHSysContinentId = Integer.Parse(Val(FNHSysContinentId.Properties.Tag.ToString))
    '    _FNHSysCountryId = Integer.Parse(Val(FNHSysCountryId.Properties.Tag.ToString))
    '    _FNHSysProvinceId = Integer.Parse(Val(FNHSysProvinceId.Properties.Tag.ToString))
    '    _FNHSysShipModeId = Integer.Parse(Val(FNHSysShipModeId.Properties.Tag.ToString))
    '    _FNHSysShipPortId = Integer.Parse(Val(FNHSysShipPortId.Properties.Tag.ToString))


    '    _Qry = "SELECT TOP 1 FNHSysContinentId "
    '    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS X WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " WHERE  FTContinentCode='" & HI.UL.ULF.rpQuoted(FNHSysContinentId.Text) & "'"

    '    _FNHSysContinentId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

    '    _Qry = "SELECT TOP 1 FNHSysCountryId "
    '    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS X WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " WHERE  FTCountryCode='" & HI.UL.ULF.rpQuoted(FNHSysCountryId.Text) & "' AND FNHSysContinentId=" & _FNHSysContinentId & ""

    '    _FNHSysCountryId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

    '    _Qry = "SELECT TOP 1 FNHSysProvinceId "
    '    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS X WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " WHERE  FTProvinceCode='" & HI.UL.ULF.rpQuoted(FNHSysProvinceId.Text) & "' AND FNHSysCountryId=" & _FNHSysCountryId & ""

    '    _FNHSysProvinceId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

    '    _Qry = "SELECT TOP 1 FNHSysShipModeId "
    '    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS X WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " WHERE  FTShipModeCode='" & HI.UL.ULF.rpQuoted(FNHSysShipModeId.Text) & "'"

    '    _FNHSysShipModeId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))


    '    _Qry = "SELECT TOP 1 FNHSysShipPortId "
    '    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS X WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " WHERE  FTShipPortCode='" & HI.UL.ULF.rpQuoted(FNHSysShipPortId.Text) & "'"

    '    _FNHSysShipPortId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

    '    ' _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_BreakDown_FactoryCMInvoice '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "

    '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_BreakDown_FactoryCMInvoice_BYDestination '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & _FNHSysContinentId & "," & _FNHSysCountryId & "," & _FNHSysProvinceId & "," & _FNHSysShipModeId & "," & _FNHSysShipPortId & " "

    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '    _dtcuspobreakdown = _dt.Copy

    '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_BreakDown_FactoryCMInvoice_BYDestination_Spare '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & _FNHSysContinentId & "," & _FNHSysCountryId & "," & _FNHSysProvinceId & "," & _FNHSysShipModeId & "," & _FNHSysShipPortId & " "
    '    _dtcuspobreakdownspare = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    With Me.ogvbreakdown

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1

    '            Select Case .Columns(I).FieldName.ToString.ToUpper
    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select

    '        Next

    '        If Not (_dt Is Nothing) Then

    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1

    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn

    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString

    '                            If Not (Col.ColumnName.ToString = "Total") Then

    '                                .ColumnEdit = ReposPrice

    '                            End If

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            If Not (Col.ColumnName.ToString = "Total") Then

    '                                .AppearanceCell.BackColor = Drawing.Color.LightCyan
    '                                .AppearanceCell.ForeColor = Color.Blue

    '                            End If

    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = Not (Col.ColumnName.ToString = "Total")

    '                            End With

    '                        End With

    '                        .Columns(Col.ColumnName.ToString).Width = 70
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select

    '            Next

    '            Dim _dtPrice As System.Data.DataTable

    '            _Qry = "   SELECT A.FTCustomerPO,  A.FTColorway, A.FTSizeBreakDown, SUM(A.FNQuantity) AS FNQuantity,A.FTPOLineItem As FTNikePOLineItem "
    '            _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS A WITH(NOLOCK)"
    '            _Qry &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS B WITH(NOLOCK)"
    '            _Qry &= vbCrLf & " ON A.FTCustomerPO = B.FTCustomerPO "
    '            _Qry &= vbCrLf & " AND A.FTInvoiceNo = B.FTInvoiceNo "
    '            _Qry &= vbCrLf & "   WHERE  (A.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
    '            _Qry &= vbCrLf & "   AND A.FTInvoiceNo<> N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text.Trim()) & "'"
    '            _Qry &= vbCrLf & "   AND FNHSysContinentId=" & _FNHSysContinentId & ""
    '            _Qry &= vbCrLf & "   AND FNHSysCountryId=" & _FNHSysCountryId & ""
    '            _Qry &= vbCrLf & "   AND FNHSysProvinceId=" & _FNHSysProvinceId & ""
    '            _Qry &= vbCrLf & "   AND FNHSysShipModeId=" & _FNHSysShipModeId & ""
    '            _Qry &= vbCrLf & "   AND FNHSysShipPortId=" & _FNHSysShipPortId & ""
    '            _Qry &= vbCrLf & "  GROUP BY  A.FTCustomerPO,  A.FTColorway, A.FTSizeBreakDown,A.FTPOLineItem"
    '            _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
    '            If _dtPrice.Rows.Count > 0 Then

    '                Dim _Filter As String

    '                For Each R As DataRow In _dtcuspobreakdown.Rows
    '                    For Each Col As DataColumn In _dtcuspobreakdown.Columns
    '                        Select Case Col.ColumnName.ToString.ToUpper
    '                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
    '                            Case Else
    '                                _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
    '                                ' _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "

    '                                If _dtPrice.Select(_Filter).Length > 0 Then

    '                                    For Each Rx As DataRow In _dtPrice.Select(_Filter)

    '                                        If Val(R.Item(Col.ColumnName.ToString)) > Val(Rx!FNQuantity.ToString) Then
    '                                            R.Item(Col.ColumnName.ToString) = Val(R.Item(Col.ColumnName.ToString)) - Val(Rx!FNQuantity.ToString)
    '                                        Else
    '                                            R.Item(Col.ColumnName.ToString) = 0
    '                                        End If

    '                                        Exit For
    '                                    Next

    '                                End If

    '                        End Select
    '                    Next

    '                Next

    '                _Filter = ""

    '                For Each R As DataRow In _dtcuspobreakdownspare.Rows
    '                    For Each Col As DataColumn In _dtcuspobreakdownspare.Columns

    '                        Select Case Col.ColumnName.ToString.ToUpper
    '                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
    '                            Case Else
    '                                _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
    '                                ' _Filter = "FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "

    '                                If _dtPrice.Select(_Filter).Length > 0 Then

    '                                    For Each Rx As DataRow In _dtPrice.Select(_Filter)

    '                                        If Val(R.Item(Col.ColumnName.ToString)) > Val(Rx!FNQuantity.ToString) Then
    '                                            R.Item(Col.ColumnName.ToString) = Val(R.Item(Col.ColumnName.ToString)) - Val(Rx!FNQuantity.ToString)
    '                                        Else
    '                                            R.Item(Col.ColumnName.ToString) = 0
    '                                        End If

    '                                        Exit For
    '                                    Next

    '                                End If

    '                        End Select
    '                    Next

    '                Next


    '            End If

    '        End If

    '    End With

    '    With Me.ogvsubbeakdown

    '        For I As Integer = .Columns.Count - 1 To 0 Step -1
    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
    '                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next

    '        If Not (_dt Is Nothing) Then
    '            For Each Col As DataColumn In _dt.Columns

    '                Select Case Col.ColumnName.ToString.ToUpper
    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
    '                    Case Else
    '                        _colcount = _colcount + 1

    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "FTSubOrderNo" & Col.ColumnName.ToString
    '                            .Caption = Col.ColumnName.ToString

    '                            If Not (Col.ColumnName.ToString = "Total") Then
    '                                .ColumnEdit = ReposPrice
    '                            End If

    '                        End With

    '                        .Columns.Add(ColG)

    '                        With .Columns(Col.ColumnName.ToString)

    '                            .OptionsFilter.AllowAutoFilter = False
    '                            .OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

    '                            If Not (Col.ColumnName.ToString = "Total") Then
    '                                .AppearanceCell.BackColor = Drawing.Color.LightCyan
    '                                .AppearanceCell.ForeColor = Color.Blue
    '                            End If


    '                            With .OptionsColumn
    '                                .AllowMove = False
    '                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = Not (Col.ColumnName.ToString = "Total")

    '                            End With

    '                        End With

    '                        .Columns(Col.ColumnName.ToString).Width = 70
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

    '                End Select

    '            Next

    '        End If

    '    End With

    '    For Each R As DataRow In _dt.Rows

    '        For Each Col As DataColumn In _dt.Columns

    '            Select Case Col.ColumnName.ToString.ToUpper
    '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
    '                Case Else
    '                    R.Item(Col.ColumnName.ToString) = System.DBNull.Value
    '            End Select

    '        Next

    '    Next

    '    'Me.ogcbreakdown.DataSource = _dtcuspobreakdownspare.Copy ' _dt.Copy
    '    Me.ogcbreakdown.DataSource = _dtcuspobreakdown.Copy ' _dt.Copy
    '    ogcbreakdown.Refresh()
    '    Me.ogcsubbeakdown.DataSource = _dtcuspobreakdownspare.Copy

    '    ogcsubbeakdown.Refresh()

    '    Call SumGrid()

    'End Sub

    Private Sub LoadInvoice()


        ogcinvoice.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.SP_GET_TPACKCarton_Invoice " & Val(Me.FNHSysCmpId.Properties.Tag) & ",'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)


            Me.ogcinvoice.DataSource = _dt
            Me.ogvinvoice.BestFitColumns()

        Catch ex As Exception
        End Try

        _Spls.Close()


    End Sub


    Private Sub LoadData()

        ogcBalance.DataSource = Nothing
        ogcdetailcolorsize.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            '_Qry = "SELECT   MA.FDInsDate"
            '_Qry &= vbCrLf & "  ,MA.FTCustomerPO"
            '_Qry &= vbCrLf & "  ,MA.FTStyleCode"
            '_Qry &= vbCrLf & " 	,MA.FTOrderNo	"
            '_Qry &= vbCrLf & " ,SUM(1) AS FNTotalCarton"
            '_Qry &= vbCrLf & " ,SUM(Case When (MA.FNPackPerCarton = MA.FNScanQuantity) Then 1 Else 0 End) As FNTotalFullCarton"
            '_Qry &= vbCrLf & " ,SUM(CASE WHEN (MA.FNPackPerCarton <> MA.FNScanQuantity) THEN 1 ELSE 0 END) AS FNTotalScarpCarton"
            '_Qry &= vbCrLf & " ,SUM(Case When (MA.FNPackPerCarton = MA.FNScanQuantity) Then MA.FNScanQuantity Else 0 End) As FNTotalFullCartonQty"
            '_Qry &= vbCrLf & " ,SUM(Case When (MA.FNPackPerCarton <> MA.FNScanQuantity) Then MA.FNScanQuantity Else 0 End) As FNTotalScarpCartonQty"
            '_Qry &= vbCrLf & " ,SUM(MA.FNScanQuantity) As FNTotalScanQuantity"

            '_Qry &= vbCrLf & "  FROM ( "



            '_Qry &= vbCrLf & "  Select Case When ISDATE(A.FDInsDate) = 1 Then   Convert(DateTime,A.FDInsDate)  Else NULL End As 	FDInsDate "
            '_Qry &= vbCrLf & "   ,XPO.FTCustomerPO"
            '_Qry &= vbCrLf & "  , ST.FTStyleCode"
            '_Qry &= vbCrLf & "  , PCT.FTOrderNo"
            '_Qry &= vbCrLf & "  , A.FTPackNo"
            '_Qry &= vbCrLf & "  , A.FNCartonNo	"
            '_Qry &= vbCrLf & " , PCT.FNPackPerCarton "
            '_Qry &= vbCrLf & "  , SUM(ISNULL(X.FNScanQuantity,0)) As FNScanQuantity"
            '_Qry &= vbCrLf & " FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton As A With(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack As B With(NOLOCK) On A.FTPackNo = B.FTPackNo INNER JOIN"
            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As PCT With(NOLOCK) On A.FTPackNo = PCT.FTPackNo And A.FNCartonNo = PCT.FNCartonNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On B.FNHSysStyleId = ST.FNHSysStyleId"
            '_Qry &= vbCrLf & "   OUTER APPLY (	Select SUM(FNScanQuantity) As FNScanQuantity"
            '_Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As SC With(NOLOCK)"
            '_Qry &= vbCrLf & "	WHERE SC.FTPackNo =A.FTPackNo"
            '_Qry &= vbCrLf & "  And SC.FNCartonNo  = PCT.FNCartonNo	 "
            '_Qry &= vbCrLf & " And SC.FTOrderNo  = PCT.FTOrderNo"
            '_Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            '_Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            '_Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "

            '_Qry &= vbCrLf & ") As X  "


            '_Qry &= vbCrLf & "   OUTER APPLY (	Select TOP 1 FTPOref AS FTCustomerPO,FTNikePOLineItem AS FTPOLine "
            '_Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As SC With(NOLOCK)"
            '_Qry &= vbCrLf & "	WHERE  SC.FTOrderNo  = PCT.FTOrderNo"
            '_Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            '_Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            '_Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "
            '_Qry &= vbCrLf & ") As XPO  "


            '_Qry &= vbCrLf & " WHERE   (A.FTState = N'1') "
            '_Qry &= vbCrLf & " AND   (B.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ") "



            'If FTCustomerPO.Text <> "" Then
            '    _Qry &= vbCrLf & " AND XPO.FTCustomerPO ='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
            'End If



            '_Qry &= vbCrLf & " GROUP BY   CASE WHEN ISDATE(A.FDInsDate) = 1 THEN   Convert(DateTime,A.FDInsDate)  ELSE NULL END "
            '_Qry &= vbCrLf & " , A.FTPackNo"
            '_Qry &= vbCrLf & " , A.FNCartonNo"
            '_Qry &= vbCrLf & " , XPO.FTCustomerPO"
            '_Qry &= vbCrLf & " , ST.FTStyleCode"
            '_Qry &= vbCrLf & " ,PCT.FTOrderNo"
            '_Qry &= vbCrLf & " ,PCT.FNPackPerCarton"
            '_Qry &= vbCrLf & " ) AS MA"
            '_Qry &= vbCrLf & " GROUP BY   MA.FDInsDate"
            '_Qry &= vbCrLf & "  ,MA.FTCustomerPO"
            '_Qry &= vbCrLf & "  ,MA.FTStyleCode"
            '_Qry &= vbCrLf & " 	,MA.FTOrderNo	"

            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.SP_GET_TPACKCarton_BALANCE " & Val(Me.FNHSysCmpId.Properties.Tag) & ",'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            Me.ogcBalance.DataSource = _dt
            Me.ogvBalance.BestFitColumns()
            Call LoaddataDetailColorSize()

        Catch ex As Exception
        End Try

        _Spls.Close()


    End Sub

    Private Sub LoaddataDetailColorSize()
        Dim _Qry As String
        Dim _dt As DataTable
        ogcdetailcolorsize.DataSource = Nothing
        Try
            '_Qry = " Select   Case When ISDATE(A.FDInsDate) = 1 Then   Convert(DateTime,A.FDInsDate)  Else NULL End As FDInsDate "
            '_Qry &= vbCrLf & "  , A.FTPackNo"
            '_Qry &= vbCrLf & "  , A.FNCartonNo		 "
            '_Qry &= vbCrLf & "  , XPO.FTCustomerPO"
            '_Qry &= vbCrLf & "  , ST.FTStyleCode"
            '_Qry &= vbCrLf & "  , PCT.FTOrderNo"
            '_Qry &= vbCrLf & "  , PCT.FTSubOrderNo"
            '_Qry &= vbCrLf & "  , PCT.FTColorway"
            '_Qry &= vbCrLf & "  , PCT.FTSizeBreakDown"
            '_Qry &= vbCrLf & "  , XPO.FTPOLine"
            '_Qry &= vbCrLf & "  , SUM(ISNULL(X.FNScanQuantity,0)) As FNScanQuantity"
            '_Qry &= vbCrLf & " FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton As A With(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack As B With(NOLOCK) On A.FTPackNo = B.FTPackNo INNER JOIN"
            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As PCT With(NOLOCK) On A.FTPackNo = PCT.FTPackNo And A.FNCartonNo = PCT.FNCartonNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On B.FNHSysStyleId = ST.FNHSysStyleId"
            '_Qry &= vbCrLf & "   OUTER APPLY (	Select SUM(FNScanQuantity) As FNScanQuantity"
            '_Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As SC With(NOLOCK)"
            '_Qry &= vbCrLf & "	WHERE SC.FTPackNo =A.FTPackNo"
            '_Qry &= vbCrLf & "  And SC.FNCartonNo  = PCT.FNCartonNo	 "
            '_Qry &= vbCrLf & " And SC.FTOrderNo  = PCT.FTOrderNo"
            '_Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            '_Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            '_Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "

            '_Qry &= vbCrLf & ") As X  "


            '_Qry &= vbCrLf & "   OUTER APPLY (	Select TOP 1 FTPOref AS FTCustomerPO,FTNikePOLineItem AS FTPOLine "
            '_Qry &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As SC With(NOLOCK)"
            '_Qry &= vbCrLf & "	WHERE  SC.FTOrderNo  = PCT.FTOrderNo"
            '_Qry &= vbCrLf & "  And SC.FTSubOrderNo  = PCT.FTSubOrderNo"
            '_Qry &= vbCrLf & " And SC.FTColorway  = PCT.FTColorway"
            '_Qry &= vbCrLf & "  And SC.FTSizeBreakDown  = PCT.FTSizeBreakDown "
            '_Qry &= vbCrLf & ") As XPO  "


            '_Qry &= vbCrLf & " WHERE   (A.FTState = N'1') "
            '_Qry &= vbCrLf & " AND   (B.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ") "


            'If FTCustomerPO.Text <> "" Then
            '    _Qry &= vbCrLf & " AND XPO.FTCustomerPO ='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
            'End If


            '_Qry &= vbCrLf & " GROUP BY   CASE WHEN ISDATE(A.FDInsDate) = 1 THEN   Convert(DateTime,A.FDInsDate)  ELSE NULL END "
            '_Qry &= vbCrLf & " , A.FTPackNo"
            '_Qry &= vbCrLf & "  , A.FNCartonNo"
            '_Qry &= vbCrLf & "  , XPO.FTCustomerPO"
            '_Qry &= vbCrLf & " , ST.FTStyleCode"
            '_Qry &= vbCrLf & " , PCT.FTOrderNo"
            '_Qry &= vbCrLf & " , PCT.FTSubOrderNo"
            '_Qry &= vbCrLf & " , PCT.FTColorway"
            '_Qry &= vbCrLf & " , PCT.FTSizeBreakDown"
            '_Qry &= vbCrLf & " , XPO.FTPOLine "

            '_Qry &= vbCrLf & "  ORDER BY CASE WHEN ISDATE(A.FDInsDate) = 1 THEN   Convert(DateTime,A.FDInsDate)  ELSE NULL END ,A.FTPackNo,A.FNCartonNo,XPO.FTCustomerPO "

            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.SP_GET_TPACKCarton_DocRef " & Val(Me.FNHSysCmpId.Properties.Tag) & ",'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)





            Me.ogcdetailcolorsize.DataSource = _dt
            Me.ogvdetailcolorsize.BestFitColumns()
        Catch ex As Exception
        End Try

    End Sub




    Private Function SaveData() As Boolean
        Dim _Qry As String = ""
        Dim dt As System.Data.DataTable
        With CType(Me.ogcBalance.DataSource, System.Data.DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Dim dt_detail As System.Data.DataTable
        With CType(Me.ogcdetailcolorsize.DataSource, System.Data.DataTable)
            .AcceptChanges()
            dt_detail = .Copy
        End With


        Dim _dtColorway As New System.Data.DataTable
        _dtColorway.Columns.Add("FTColorway", GetType(String))

        Dim _CmpH As String = ""
        For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

            Select Case HI.ENM.Control.GeTypeControl(ctrl)
                Case ENM.Control.ControlType.ButtonEdit

                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                    End With

                    Exit For

                Case ENM.Control.ControlType.TextEdit

                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK) WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                    End With

                    Exit For
            End Select

        Next

        Dim _SysInvoiceKey As String = ""

        If FTInvoiceNo.Text = HI.TL.Document.GetDocumentNo(SysDBName, SysTableName, "", True, _CmpH).ToString() Then
            _SysInvoiceKey = HI.TL.Document.GetDocumentNo(SysDBName, SysTableName, "", False, _CmpH).ToString()
        Else
            _SysInvoiceKey = FTInvoiceNo.Text
        End If

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC"
            _Qry &= vbCrLf & "  SET   FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & " , FDInvoiceDate='" & HI.UL.ULDate.ConvertEnDB(FDInvoiceDate.Text) & "'"

            _Qry &= vbCrLf & " , FTAddress='" & HI.UL.ULF.rpQuoted(FTAddress.Text) & "'"
            _Qry &= vbCrLf & " , FTShipTo='" & HI.UL.ULF.rpQuoted(FTShipTo.Text) & "'"
            _Qry &= vbCrLf & " , FNHSysTermOfPMId=" & Val(FNHSysTermOfPMId.Properties.Tag) & ""
            _Qry &= vbCrLf & " , FDDuedate='" & HI.UL.ULDate.ConvertEnDB(FDDuedate.Text) & "'"
            _Qry &= vbCrLf & " , FTSalesName='" & HI.UL.ULF.rpQuoted(FTSalesName.Text) & "'"



            '_Qry &= vbCrLf & " , FTStateMerApp='0',FTStateWHApp='0'"
            '_Qry &= vbCrLf & " , FTStateMerAppBy=''"
            '_Qry &= vbCrLf & " , FTStateMerReject='0',FTStateSendApp='0',FTStateWHReject='0',FTStateApp='0'"
            '_Qry &= vbCrLf & " , FTStateMerRejectBy=''"
            ''_Qry &= vbCrLf & " , FNTotalCarton=" & FNTotalCarton.Value & ""
            '_Qry &= vbCrLf & " , FTStateHanger='" & FTStateHanger.EditValue.ToString & "'"
            _Qry &= vbCrLf & "   WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & "   AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_SysInvoiceKey) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC"
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTCustomerPO, FTInvoiceNo, FDInvoiceDate,FNHsysCmpID  "
                _Qry &= vbCrLf & "  ,FTAddress, FTShipTo ,FNHSysTermOfPMId , FDDuedate, FTSalesName  "
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & " SELECT "
                _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_SysInvoiceKey) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(FDInvoiceDate.Text) & "'"
                _Qry &= vbCrLf & " ," & Val(HI.ST.SysInfo.CmpID) & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTAddress.Text) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTShipTo.Text) & "'"
                _Qry &= vbCrLf & " ," & Val(FNHSysTermOfPMId.Properties.Tag) & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(FDDuedate.Text) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSalesName.Text) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False

                End If

            End If

            Dim _FTOrderNo As String
            Dim _FTSubOrderNo As String
            Dim _FTColorway As String
            Dim _FTSizeBreakDown As String

            Dim _FTPOLine As String

            Dim _FNBoxBL As Integer = 0


            Dim _d_FTOrderNo As String
            Dim _d_FTSubOrderNo As String
            Dim _d__FTColorway As String
            Dim _d__FTSizeBreakDown As String
            Dim _d__FTPackNo As String
            Dim _d_FTPOLine As String
            Dim _d_FNCartonNo As String


            Dim _FNBoxBL_to_pack As Integer = 0


            For Each R As DataRow In dt.Select("FTSelect='1'")
                _FTColorway = R!FTColorway.ToString()
                _FTSizeBreakDown = R!FTSizeBreakDown.ToString()


                _FNBoxBL = Val(R!FNBoxBL.ToString())
                _FNBoxBL_to_pack = 0

                For Each d As DataRow In dt_detail.Select("FTInvoiceNo='' and  FTColorway='" & _FTColorway & "' and FTSizeBreakDown='" & _FTSizeBreakDown & "'")


                    _FNBoxBL_to_pack = _FNBoxBL_to_pack + 1


                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC_Ref "
                    _Qry &= vbCrLf & "  ("
                    _Qry &= vbCrLf & "   FDInsDate, FTPackNo, FNCartonNo, FTCustomerPO, FTStyleCode , FTOrderNo , FTSubOrderNo, FTColorway, FTSizeBreakDown ,FTPOLine, FNScanQuantity, FTInvoiceNo"
                    _Qry &= vbCrLf & "  ) "

                    _Qry &= vbCrLf & " SELECT "
                    _Qry &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(d!FDInsDate.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTPackNo.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FNCartonNo.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTCustomerPO.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTStyleCode.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTOrderNo.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTSubOrderNo.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTColorway.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTSizeBreakDown.ToString()) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(d!FTPOLine.ToString()) & "'"
                    _Qry &= vbCrLf & " ," & Val(d!FNScanQuantity.ToString()) & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_SysInvoiceKey) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If


                    If _FNBoxBL = _FNBoxBL_to_pack Then
                        Exit For
                    End If

                Next



            Next




            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            FTInvoiceNo.Properties.Tag = _SysInvoiceKey
            FTInvoiceNo.Text = _SysInvoiceKey

            ''  Call CheckStateLockEdit()


            ''call load data 



            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub LoadCustomerPODefault()
        _LoadInvoice = True
        Dim _Qry As String = ""
        Dim dt As System.Data.DataTable

        '_Qry = "  SELECT TOP 1 A.FTCustomerPO, A.FNHSysContinentId, A.FNHSysCountryId, A.FNHSysProvinceId"
        '_Qry &= vbCrLf & " , A.FNHSysShipModeId, A.FNHSysShipPortId, B.FTContinentCode, C.FTCountryCode"
        '_Qry &= vbCrLf & " , D.FTProvinceCode, E.FTShipModeCode,    F.FTShipPortCode"
        '_Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_Customer_PO_ShipDestination AS A INNER JOIN"
        '_Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS E WITH(NOLOCK) ON A.FNHSysShipModeId = E.FNHSysShipModeId INNER JOIN"
        '_Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS F WITH(NOLOCK) ON A.FNHSysShipPortId = F.FNHSysShipPortId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS D WITH(NOLOCK) ON A.FNHSysProvinceId = D.FNHSysProvinceId AND A.FNHSysCountryId = D.FNHSysCountryId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS B WITH(NOLOCK) ON A.FNHSysContinentId = B.FNHSysContinentId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS C WITH(NOLOCK) ON A.FNHSysCountryId = C.FNHSysCountryId AND A.FNHSysContinentId = C.FNHSysContinentId"
        '_Qry &= vbCrLf & "  WHERE  (A.FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "')"

        'dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        'For Each R As DataRow In dt.Rows
        '    FNHSysContinentId.Text = R!FTContinentCode.ToString
        '    FNHSysCountryId.Text = R!FTCountryCode.ToString
        '    FNHSysProvinceId.Text = R!FTProvinceCode.ToString
        '    FNHSysShipModeId.Text = R!FTShipModeCode.ToString
        '    FNHSysShipPortId.Text = R!FTShipPortCode.ToString

        '    Exit For
        'Next

        '' Call LoadOrderBreakDown(FTCustomerPO.Text)

        _LoadInvoice = False

    End Sub

    Private Sub CheckStateLockEdit()

        Dim _Qry As String = ""
        Dim _State As Boolean
        _Qry = "SELECT TOP 1 FTInvoiceNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

        _State = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> "")

        'FNHSysContinentId.Properties.ReadOnly = _State
        'FNHSysContinentId.Properties.Buttons(0).Enabled = Not (_State)

        'FNHSysCountryId.Properties.ReadOnly = _State
        'FNHSysCountryId.Properties.Buttons(0).Enabled = Not (_State)

        'FNHSysProvinceId.Properties.ReadOnly = _State
        'FNHSysProvinceId.Properties.Buttons(0).Enabled = Not (_State)

        'FNHSysShipModeId.Properties.ReadOnly = _State
        'FNHSysShipModeId.Properties.Buttons(0).Enabled = Not (_State)

        'FNHSysShipPortId.Properties.ReadOnly = _State
        'FNHSysShipPortId.Properties.Buttons(0).Enabled = Not (_State)

    End Sub

    Private Function DeleteData() As Boolean
        Dim _Qry As String = ""
        'Dim dt As System.Data.DataTable
        'With CType(Me.ogcbreakdown.DataSource, System.Data.DataTable)
        '    .AcceptChanges()
        '    dt = .Copy
        'End With

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC_Ref"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice"
            '_Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            '_Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

            'HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub wXMLCreateInvoiceData_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call InitGrid()

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

    End Sub

    Private Sub FTCustomerPO_EditValueChanged(sender As Object, e As EventArgs) Handles FTCustomerPO.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTCustomerPO_EditValueChanged), New Object() {sender, e})
        Else

            Me.FTInvoiceNo.Text = ""
            Me.FDInvoiceDate.Text = ""
            'Me.FTInvoiceExportNo.Text = ""
            'Me.FDInvoiceExportDate.Text = ""
            'Me.FNHSysContinentId.Text = ""
            'Me.FNHSysCountryId.Text = ""
            'Me.FNHSysProvinceId.Text = ""
            'Me.FNHSysShipModeId.Text = ""
            'Me.FNHSysShipPortId.Text = ""
            FTStateHanger.Checked = False
            ''Call CheckStateLockEdit()

            ''  Call LoadCustomerAddress()

            Call LoadOrderDataInfo(FTCustomerPO.Text)
            Call LoadCustomerPODefault()
            Me.otbdetail.SelectedTabPageIndex = 0





        End If
    End Sub




    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTInvoiceNo.EditValueChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTInvoiceNo_EditValueChanged), New Object() {sender, e})
        Else

            '' Me.FTInvoiceExportNo.Text = ""
            FTStateHanger.Checked = False
            Call LoadCustomerPODefault()
            Call LoadOrderPriceInfo(FTInvoiceNo.Text)
            Call CheckStateLockEdit()

            '' Call LoadListOrderInfo(Me.FTCustomerPO.Text.Trim)
            Me.otbdetail.SelectedTabPageIndex = 0

        End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.FTCustomerPO.Text <> "" Then
            If Me.FTInvoiceNo.Text <> "" Then
                If Me.FDInvoiceDate.Text <> "" Then
                    '' If FNTotalCarton.Value > 0 Then
                    If Me.SaveData() Then

                        '' Me.FTInvoiceExportNo.Text = ""
                        '' Call LoadCustomerPODefault()
                        Call LoadOrderDataInfo(FTCustomerPO.Text)
                        '' Call CheckStateLockEdit()
                        ''
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                    'Else
                    '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNTotalCarton_lbl.Text)
                    '    FNTotalCarton.Focus()
                    '    FNTotalCarton.SelectAll()
                    'End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                    FDInvoiceDate.Focus()
                    FDInvoiceDate.SelectAll()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                FTInvoiceNo.Focus()
                FTInvoiceNo.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If
    End Sub

    Private _StateClear As Boolean = False
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        _StateClear = True
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTInvoiceNo.Text = ""
        Me.FDInvoiceDate.Text = ""
        'Me.FTInvoiceExportNo.Text = ""
        'Me.FDInvoiceExportDate.Text = ""
        'Me.FNHSysContinentId.Text = ""
        'Me.FNHSysCountryId.Text = ""
        'Me.FNHSysProvinceId.Text = ""
        'Me.FNHSysShipModeId.Text = ""
        'Me.FNHSysShipPortId.Text = ""
        Call CheckStateLockEdit()
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        _StateClear = False
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Me.FTCustomerPO.Text <> "" Then
            If Me.FTInvoiceNo.Text <> "" Then
                If Me.FDInvoiceDate.Text <> "" Then

                    If Me.DeleteData() Then

                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Me.FTInvoiceNo.Text = ""
                        Me.FDInvoiceDate.Text = ""
                        FTStateHanger.Checked = False

                        Call LoadOrderDataInfo(FTCustomerPO.Text)
                        ''  Call CheckStateLockEdit()


                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                    FDInvoiceDate.Focus()
                    FDInvoiceDate.SelectAll()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                FTInvoiceNo.Focus()
                FTInvoiceNo.SelectAll()
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            FTCustomerPO.SelectAll()
        End If
    End Sub

    Private Sub ReposPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try

            With CType(Me.ogcinvoice.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _dtcuspobreakdown = .Copy
            End With

            'With Me.ogvbreakdown
            '    Dim _GetColumnFocus As String = .FocusedColumn.FieldName.ToString
            '    If _dtcuspobreakdown.Select("FTColorway='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTColorway").ToString()) & "'  AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTNikePOLineItem").ToString()) & "'").Length > 0 Then

            '        Dim _TotalOrderQuantity As Double = 0
            '        For Each R As DataRow In _dtcuspobreakdown.Select("FTColorway='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTColorway").ToString()) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTNikePOLineItem").ToString()) & "'")
            '            Try
            '                _TotalOrderQuantity = Val(R.Item(.FocusedColumn.FieldName.ToString()))
            '            Catch ex As Exception
            '            End Try
            '        Next

            '        If _TotalOrderQuantity >= e.NewValue Then
            '            If e.NewValue < 0 Then
            '                e.Cancel = True
            '            Else
            '                e.Cancel = False
            '                .SetFocusedRowCellValue(.FocusedColumn.FieldName.ToString(), Val(e.NewValue))
            '                Call SumGrid()
            '            End If
            '        Else
            '            e.Cancel = True
            '            'If e.NewValue < 0 Then
            '            '    e.Cancel = True
            '            'Else
            '            '    e.Cancel = False
            '            'End If
            '        End If

            '    Else
            '        e.Cancel = True
            '    End If

            'End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReposPrice_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            'With Me.ogvbreakdown
            '    If .FocusedRowHandle < 0 Then Exit Sub
            '    Select Case e.KeyCode
            '        Case Keys.F9
            '            Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value

            '            For I As Integer = 0 To .RowCount - 1
            '                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
            '                    Select Case GridCol.FieldName.ToString.ToUpper
            '                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
            '                        Case Else
            '                            .SetRowCellValue(I, GridCol.FieldName.ToString, _Value)
            '                    End Select

            '                Next
            '            Next

            '            CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
            '            ogcbreakdown.RefreshDataSource()
            '        Case Keys.F10
            '            Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value


            '            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
            '                Select Case GridCol.FieldName.ToString.ToUpper
            '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
            '                    Case Else
            '                        .SetFocusedRowCellValue(GridCol.FieldName.ToString, _Value)
            '                End Select

            '            Next

            '            CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
            '            ogcbreakdown.RefreshDataSource()
            '        Case Keys.F11
            '            Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value

            '            For I As Integer = 0 To .RowCount - 1
            '                .SetRowCellValue(I, .FocusedColumn.FieldName.ToString, _Value)
            '            Next

            '            CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
            '            ogcbreakdown.RefreshDataSource()
            '    End Select
            'End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            If FTOrderNo.Text <> "" Then

                If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then

                    Dim _Qry As String = ""
                    Dim _dt As System.Data.DataTable
                    _Qry = "   Select A.FTOrderNo"
                    _Qry &= vbCrLf & "    ,A.FTStyleCode"
                    _Qry &= vbCrLf & "    ,A.FTCustCode"
                    _Qry &= vbCrLf & " 	 ,A.FTPORef"
                    _Qry &= vbCrLf & " 	 ,CASE WHEN ISDATE(FDShipDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDShipDate) ,103)  Else '' END AS FDShipDate"
                    _Qry &= vbCrLf & " 	 ,CASE WHEN ISDATE(FDOrderDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDOrderDate) ,103)  Else '' END AS FDOrderDate"
                    _Qry &= vbCrLf & " 	 ,FNGrandQuantity"
                    _Qry &= vbCrLf & " 	 ,FTCmpName"
                    _Qry &= vbCrLf & "  FROM (SELECT O.FTOrderNo"
                    _Qry &= vbCrLf & "    , ST.FTStyleCode"
                    _Qry &= vbCrLf & "    , CT.FTCustCode"
                    _Qry &= vbCrLf & "   , O.FTPORef"
                    _Qry &= vbCrLf & "   , O.FDOrderDate"
                    _Qry &= vbCrLf & "    ,ISNULL(("
                    _Qry &= vbCrLf & " 	SELECT TOP 1 FDShipDate"
                    _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK)"
                    _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
                    _Qry &= vbCrLf & " 	ORDER BY    FDShipDate ASC"
                    _Qry &= vbCrLf & " 	),'') AS FDShipDate"
                    _Qry &= vbCrLf & " ,ISNULL(("
                    _Qry &= vbCrLf & " 	SELECT SUM(FNGrandQuantity) AS FNGrandQuantity"
                    _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS Sub WITH(NOLOCK)"
                    _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
                    _Qry &= vbCrLf & " 	),0) AS FNGrandQuantity"

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & "   ,Cmp.FTCmpCode + ' : ' + Cmp.FTCmpNameTH AS FTCmpName"
                    Else
                        _Qry &= vbCrLf & "   ,Cmp.FTCmpCode + ' : ' +  Cmp.FTCmpNameEN AS FTCmpName"
                    End If

                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)  INNER JOIN"
                    _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN"
                    _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS CT WITH(NOLOCK)  ON O.FNHSysCustId = CT.FNHSysCustId"
                    _Qry &= vbCrLf & "       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK)  ON O.FNHSysCmpId = Cmp.FNHSysCmpId"
                    _Qry &= vbCrLf & "   WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                    _Qry &= vbCrLf & " 	 ) AS A"
                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                    Me.FDShipDate.Text = ""
                    Me.FDOrderDate.Text = ""

                    For Each R As DataRow In _dt.Rows
                        Me.FDShipDate.Text = R!FDShipDate.ToString
                        Me.FDOrderDate.Text = R!FDOrderDate.ToString
                        Exit For
                    Next
                Else
                    Me.FDShipDate.Text = ""
                    Me.FDOrderDate.Text = ""
                End If
            Else
                Me.FDShipDate.Text = ""
                Me.FDOrderDate.Text = ""
            End If

        End If
    End Sub

    Private Sub ogvbreakdown_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
        Try
            'With Me.ogvbreakdown
            '    Select Case e.Column.FieldName
            '        Case "FTColorway", "FTNikePOLineItem", "Total"


            '        Case Else



            '            If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString) > 0 Then

            '                Dim cmdstring As String = "SELECT TOP 1 FNNetPrice "
            '                cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination "
            '                cmdstring &= vbCrLf & " WHERE FTPOref='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            '                cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, "FTColorway").ToString) & "'"
            '                cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(e.Column.Caption) & "'"
            '                cmdstring &= vbCrLf & " AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, "FTNikePOLineItem").ToString) & "'"

            '                Dim netprice As Decimal = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "0"))

            '                If netprice <= 0 Then
            '                    e.Appearance.ForeColor = Drawing.Color.Red
            '                    e.Appearance.Font = New Drawing.Font("tahoma", 8, Drawing.FontStyle.Bold)
            '                End If

            '            Else

            '            End If
            '    End Select
            'End With
        Catch ex As Exception
        End Try
    End Sub
    Private Function VerrifyData() As Boolean
        Try
            If FTCustomerPO.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTCustomerPO_lbl.Text)
                Me.FTCustomerPO.Focus()
                Return False
            End If
            If FTInvoiceNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTInvoiceNo_lbl.Text)
                Me.FTInvoiceNo.Focus()
                Return False
            End If
            If FDInvoiceDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FDInvoiceDate_lbl.Text)
                Me.FDInvoiceDate.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Not VerrifyData() Then Exit Sub
        Dim _Fm As String = ""
        _Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "' "
        _Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
        With New HI.RP.Report
            .FormTitle = Me.Text
            .ReportFolderName = "Account\"
            .ReportName = "ReportInvoiceCm.rpt"
            .Formular = _Fm
            .Preview()
        End With
    End Sub

    'Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
    '    Try
    '        If Me.FTCustomerPO.Text <> "" And Me.FTInvoiceNo.Text <> "" And Me.FDInvoiceDate.Text <> "" Then
    '            Dim _Cmd As String = ""
    '            _Cmd = " SELECT Top 1  Isnull( FTStateSendApp,'0') AS FTStateSendApp  "
    '            _Cmd &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice WITH(NOLOCK) "
    '            _Cmd &= vbCrLf & "WHERE  FTCustomerPO='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
    '            _Cmd &= vbCrLf & "And FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"

    '            If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> "1" Then
    '                'If SendMailApp() Then
    '                _Cmd = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice    "
    '                _Cmd &= vbCrLf & "SET FTStateSendApp ='1'"
    '                _Cmd &= vbCrLf & ",FTStateSendBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                _Cmd &= vbCrLf & ",FDStateSendDate=" & HI.UL.ULDate.FormatDateDB
    '                _Cmd &= vbCrLf & ",FTStateSendTime=" & HI.UL.ULDate.FormatTimeDB
    '                _Cmd &= vbCrLf & "WHERE  FTCustomerPO='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
    '                _Cmd &= vbCrLf & "And FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
    '                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
    '                ''  Me.FTStateSendApp.Checked = True
    '                'End If

    '            End If

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function SendMailApp() As Boolean
        Dim _Qry As String = ""
        Dim _UserMailTo As String = ""

        _Qry = " SELECT TOP 1 Tm.FTUserName"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS Tm WITH(NOLOCK)  ON U.FNHSysTeamGrpId = Tm.FNHSysTeamGrpId"
        _Qry &= vbCrLf & " WHERE  (U.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"

        _UserMailTo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

        If _UserMailTo <> "" Then

            Dim tmpsubject As String = ""
            Dim tmpmessage As String = ""

            tmpsubject = "Send Approve FOB Price Adjusted  Customer Purchased No." & Me.FTCustomerPO.Text & "  Invoice No. " & Me.FTInvoiceNo.Text & ""
            tmpmessage = "Send Approve FOB Price Adjusted  Customer Purchased No." & Me.FTCustomerPO.Text & "  Invoice No. " & Me.FTInvoiceNo.Text & ""
            tmpmessage &= vbCrLf & "Date :" & HI.UL.ULDate.FormatDateDB
            tmpmessage &= vbCrLf & "By :" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)


            If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 5, Me.FTCustomerPO.Text) Then

            End If

        End If
        Return True
    End Function

    Private Function CreateFirstSale(_FTCustomerPO As String) As Boolean
        'Dim _Qry As String = ""
        '_Qry = " SELECT TOP 1 FTCustomerPO"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet WITH(NOLOCK)"
        '_Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(_FTCustomerPO) & "' "

        'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "" Then
        '    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการทำ First Sale Price กรุณาทำการตรวจสอบ !!!", 1510164875, Me.Text, , MessageBoxIcon.Warning)
        '    Return False
        'Else
        '    Return True
        'End If
        Return True
    End Function

    'Private Sub ocmsaveinvoice_Click(sender As Object, e As EventArgs) Handles ocmsaveinvoice.Click
    '    If Me.FTCustomerPO.Text <> "" Then
    '        If Me.FTInvoiceNo.Text <> "" And Me.FTInvoiceNo.Properties.Tag.ToString <> "" Then
    '            If Me.FDInvoiceDate.Text <> "" Then
    '                Dim _Qry As String = ""

    '                _Qry = "SELECT TOP 1 A.FTOrderNo"
    '                _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
    '                _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId"
    '                _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId  "
    '                _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON "
    '                _Qry &= vbCrLf & "  	A.FNHSysSeasonId = SEAS.FNHSysSeasonId"
    '                _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PMC WITH(NOLOCK)  ON  A.FNHSysVenderPramId=PMC.FNHSysVenderPramId"
    '                _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  AS CMP WITH(NOLOCK) ON A.FNHSysCmpId  = CMP.FNHSysCmpId  "
    '                _Qry &= vbCrLf & " WHERE A.FTOrderNo IN ("
    '                _Qry &= vbCrLf & " SELECT O.FTOrderNo"
    '                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
    '                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
    '                _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
    '                _Qry &= vbCrLf & " GROUP BY O.FTOrderNo"
    '                _Qry &= vbCrLf & " ) AND A.FNJobState=1 "
    '                _Qry &= vbCrLf & "  AND  CMP.FTStateXMLFile='1' "
    '                _Qry &= vbCrLf & "   ORDER BY  A.FTOrderNo"

    '                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
    '                    If CreateFirstSale(Me.FTCustomerPO.Text) = False Then
    '                        Exit Sub
    '                    End If
    '                End If

    '                '' Call HI.ST.Lang.SP_SETxLanguage(_InvoiceExport)

    '                Dim _FTRemark As String = ""

    '                _Qry = "   SELECT Top 1 FTInvoiceExportNote"
    '                _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK)"
    '                _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
    '                _Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

    '                _FTRemark = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "")

    '                'With _InvoiceExport
    '                '    .FTInvoiceExportNo.Text = Me.FTInvoiceExportNo.Text
    '                '    .FDInvoiceExportDate.Text = Me.FDInvoiceExportDate.Text
    '                '    .FTRemark.Text = _FTRemark
    '                '    .StateProc = False
    '                '    .ocmsave.Enabled = True
    '                '    .ocmcancel.Enabled = True
    '                '    .FTPORef.Text = Me.FTCustomerPO.Text
    '                '    .ShowDialog()

    '                '    If .StateProc Then

    '                '        _Qry = "  UPDATE A SET "
    '                '        _Qry &= vbCrLf & " FTInvoiceExportNo='" & HI.UL.ULF.rpQuoted(.FTInvoiceExportNo.Text) & "'"
    '                '        _Qry &= vbCrLf & " ,FDInvoiceExportDate='" & HI.UL.ULDate.ConvertEnDB(.FDInvoiceExportDate.Text) & "'"
    '                '        _Qry &= vbCrLf & " ,FTInvoiceExportNote='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "'"
    '                '        _Qry &= vbCrLf & " ,FTStateMerApp='0'"
    '                '        _Qry &= vbCrLf & " ,FTStateMerAppBy=''"
    '                '        _Qry &= vbCrLf & " ,FTStateMerReject='0'"
    '                '        _Qry &= vbCrLf & " ,FTStateMerRejectBy=''"
    '                '        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A "
    '                '        _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
    '                '        _Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

    '                '        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT) = True Then

    '                '            Me.FTInvoiceExportNo.Text = .FTInvoiceExportNo.Text
    '                '            Me.FDInvoiceExportDate.Text = .FDInvoiceExportDate.Text
    '                '            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

    '                '        Else

    '                '            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

    '                '        End If

    '                '    End If
    '                'End With

    '            Else
    '                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
    '                FDInvoiceDate.Focus()
    '                FDInvoiceDate.SelectAll()
    '            End If
    '        Else
    '            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
    '            FTInvoiceNo.Focus()
    '            FTInvoiceNo.SelectAll()
    '        End If
    '    Else
    '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
    '        FTCustomerPO.Focus()
    '        FTCustomerPO.SelectAll()
    '    End If
    'End Sub


    Private Sub FNHSysContinentId_EditValueChanged(sender As Object, e As EventArgs)




        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTInvoiceNo_EditValueChanged), New Object() {sender, e})
        Else
            If _LoadInvoice = False Then

                Select Case sender.name.ToString.ToUpper
                    Case "FNHSysContinentId".ToUpper

                        'Me.FNHSysCountryId.Text = ""
                        'Me.FNHSysProvinceId.Text = ""
                        'Me.FNHSysShipModeId.Text = ""
                        'Me.FNHSysShipPortId.Text = ""

                        'Case "FNHSysCountryId".ToUpper

                        '    Me.FNHSysProvinceId.Text = ""
                        '    Me.FNHSysShipModeId.Text = ""
                        '    Me.FNHSysShipPortId.Text = ""

                        'Case "FNHSysProvinceId".ToUpper

                        '    Me.FNHSysShipModeId.Text = ""
                        '    Me.FNHSysShipPortId.Text = ""

                        'Case "FNHSysShipModeId".ToUpper
                        '    Me.FNHSysShipPortId.Text = ""

                End Select

                ''Call LoadOrderBreakDown(Me.FTCustomerPO.Text)

            End If

        End If

    End Sub

    Private Sub SumGrid()

        Try

            Dim _Total As Integer = 0
            'With Me.ogvbreakdown
            '    For I As Integer = 0 To .RowCount - 1
            '        _Total = 0
            '        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

            '            Select Case GridCol.FieldName.ToString.ToUpper
            '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
            '                Case "FTNikePOLineItem".ToUpper

            '                Case Else

            '                    If IsNumeric(.GetRowCellValue(I, GridCol)) Then
            '                        _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
            '                    Else
            '                        _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
            '                    End If

            '            End Select

            '        Next

            '        .SetRowCellValue(I, "Total", _Total)

            '    Next

            'End With
            '_Total = 0

            'SetCartonQty()
            'CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub CalculateSumGrid()

        Try

            'Dim _Total As Integer = 0

            'Dim dt As System.Data.DataTable

            'With CType(ogcbreakdown.DataSource, System.Data.DataTable)
            '    .AcceptChanges()
            '    dt = .Copy()
            'End With

            'With Me.ogvbreakdown
            '    For I As Integer = 0 To .RowCount - 1
            '        _Total = 0
            '        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

            '            Select Case GridCol.FieldName.ToString.ToUpper
            '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
            '                Case Else

            '                    If IsNumeric(.GetRowCellValue(I, GridCol)) Then
            '                        _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
            '                    Else
            '                        _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
            '                    End If

            '            End Select

            '        Next

            '        .SetRowCellValue(I, "Total", _Total)

            '    Next

            'End With

            'dt.BeginInit()
            'For Each R As DataRow In dt.Rows
            '    _Total = 0
            '    For Each Col As DataColumn In dt.Columns
            '        Select Case Col.ColumnName.ToUpper
            '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
            '            Case Else

            '                If IsNumeric(R.Item(Col)) Then
            '                    _Total = _Total + CInt(R.Item(Col))
            '                Else
            '                    _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
            '                End If

            '        End Select
            '    Next

            '    R!Total = _Total

            'Next

            'dt.EndInit()
            '_Total = 0
            'ogcbreakdown.DataSource = dt
            'CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()

        Catch ex As Exception
        End Try
    End Sub

    'Private Sub ocmpreviewtvwthb_Click(sender As Object, e As EventArgs) Handles ocmpreviewtvwthb.Click
    '    If Not VerrifyData() Then Exit Sub
    '    Dim _Fm As String = ""
    '    _Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "' "
    '    _Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
    '    With New HI.RP.Report
    '        .FormTitle = Me.Text
    '        .ReportFolderName = "Account\"
    '        .ReportName = "ReportInvoiceCm_THB.rpt"
    '        .Formular = _Fm
    '        .Preview()
    '    End With
    'End Sub

    Private Sub ogvbreakdown_RowCountChanged(sender As Object, e As EventArgs)
        Try
            SetCartonQty()
        Catch ex As Exception

        End Try
    End Sub

    Private Function SetCartonQty() As Boolean
        Try
            'Dim _Cmd As String = ""
            'Dim _POLine As String = ""
            'Dim _dt As DataTable

            'With Me.ogvbreakdown
            '    For I As Integer = 0 To .RowCount - 1

            '        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

            '            Select Case GridCol.FieldName.ToString.ToUpper
            '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
            '                Case "FTNikePOLineItem".ToUpper
            '                    If _POLine <> "" Then _POLine &= "|"
            '                    _POLine &= HI.UL.ULF.rpQuoted(.GetRowCellValue(I, GridCol))
            '                Case Else
            '            End Select
            '        Next
            '    Next
            'End With
            '_POLine = "'" & _POLine & "|'"
            '_Cmd = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..sp_gettotalcarton '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'," & _POLine & ""
            'Me.FNTotalCarton.Value = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
            '_Cmd = "SELECT top 1  FNTotalCarton FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "..TACCTFactoryCMInvoice  WHERE  (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "')"
            'Dim _CartonQty As Integer = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
            'If _CartonQty > 0 Then
            '    Me.FNTotalCarton.Value = _CartonQty
            'End If
            'Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmadd_Click(sender As Object, e As EventArgs)
        Try
            ''check   check box




            ''validate data 




            ''check seq 




            ''  add header 



            '' add detail




            ''load   grid invoice



            ''load data 







        Catch ex As Exception

        End Try
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            'With ogcinvoice
            '    If Not (.DataSource Is Nothing) And ogvinvoice.RowCount > 0 Then

            '        With ogvinvoice
            '            For I As Integer = 0 To .RowCount - 1
            '                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
            '            Next
            '        End With

            '        CType(.DataSource, DataTable).AcceptChanges()
            '    End If

            'End With

            Dim Row As Integer = 0
            Dim FNBoxBL As Integer = 0
            'With DirectCast(Me.ogcinvoice.DataSource, DataTable)
            '    .AcceptChanges()
            '    For Each R As DataRow In .Rows

            '        FNBoxBL = ogvinvoice.GetRowCellValue(Row, "FNBoxBL").ToString()

            '        'If _State = "1" And FNBoxBL > 0 Then
            '        ogvinvoice.SetRowCellValue(Row, "FTSelect", _State)

            '        'Else

            '        '    ogvinvoice.SetRowCellValue(Row, "FTSelect", "0")
            '        'End If

            '        Row += 1
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcBalance
                If Not (.DataSource Is Nothing) And ogvBalance.RowCount > 0 Then


                    With ogvBalance
                        For I As Integer = 0 To .RowCount - 1

                            ogvBalance.SetRowCellValue(I, "FTSelect", _State)


                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try




    End Sub



    Private Function DeleteInvoice(ByVal FTInvoiceNO As String) As Boolean
        Dim _Qry As String = ""
        'Dim dt As System.Data.DataTable
        'With CType(Me.ogcbreakdown.DataSource, System.Data.DataTable)
        '    .AcceptChanges()
        '    dt = .Copy
        'End With

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNO) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC_Ref"
            _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            _Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNO) & "'"
            ''  HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice"
            '_Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim()) & "'"
            '_Qry &= vbCrLf & " AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

            'HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub ogvinvoice_DoubleClick(sender As Object, e As EventArgs) Handles ogvinvoice.DoubleClick
        Try

            Dim FTInvoiceNO As String

            With ogvinvoice

                If .FocusedRowHandle < 0 Then Exit Sub
                FTInvoiceNO = "" & .GetFocusedRowCellValue("FTInvoiceNo").ToString
            End With



            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, FTInvoiceNO, Me.Text) = True Then

                Call DeleteInvoice(FTInvoiceNO)


                With ogvinvoice
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)
                    With CType(Me.ogcinvoice.DataSource, DataTable)
                        .AcceptChanges()
                    End With
                End With

                Call LoadOrderDataInfo(FTCustomerPO.Text)

            End If


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvinvoice_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvinvoice.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete

                    Dim FTInvoiceNO As String

                    With ogvinvoice
                        If .FocusedRowHandle < 0 Then Exit Sub
                        FTInvoiceNO = "" & .GetFocusedRowCellValue("FTInvoiceNo").ToString
                    End With



                    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, FTInvoiceNO, Me.Text) = True Then

                        Call DeleteInvoice(FTInvoiceNO)


                        With ogvinvoice
                            If .FocusedRowHandle < 0 Then Exit Sub
                            .DeleteRow(.FocusedRowHandle)
                            With CType(Me.ogcinvoice.DataSource, DataTable)
                                .AcceptChanges()
                            End With
                        End With


                        Call LoadOrderDataInfo(FTCustomerPO.Text)

                    End If


                    'Try
                    '    With ogvBalance
                    '        If .FocusedRowHandle < 0 Then Exit Sub
                    '        .DeleteRow(.FocusedRowHandle)
                    '        With CType(Me.ogcBalance.DataSource, DataTable)
                    '            .AcceptChanges()
                    '        End With

                    '    End With
                    'Catch ex As Exception


                    'End Try
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
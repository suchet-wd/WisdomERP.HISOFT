Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports System.Net
Imports DevExpress.XtraEditors.Controls

Public Class wExpCMInvoiceBook
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
    Private _InvoiceExport As wFactoryInvoiceCMInvoiceExport
    Private _InvoicePost As wExpCMInvoicePost
    Private _PackingPlandPopup As wAddMainMat
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _ProcLoad As Boolean = False


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitFormControl()
        ' Add any initialization after the InitializeComponent() call.
        _InvoiceExport = New wFactoryInvoiceCMInvoiceExport
        HI.TL.HandlerControl.AddHandlerObj(_InvoiceExport)
        _PackingPlandPopup = New wAddMainMat
        HI.TL.HandlerControl.AddHandlerObj(_PackingPlandPopup)
        _InvoicePost = New wExpCMInvoicePost
        HI.TL.HandlerControl.AddHandlerObj(_InvoicePost)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _InvoiceExport.Name.ToString.Trim, _InvoiceExport)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PackingPlandPopup.Name.ToString.Trim, _PackingPlandPopup)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _InvoicePost.Name.ToString.Trim, _InvoicePost)
        Catch ex As Exception
        Finally
        End Try
    End Sub


    Private Sub InitFormControl()

        Dim _Str As String = ""
        Dim _objId As Integer
        Dim _dt As System.Data.DataTable
        Dim _StrQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _StartX As Double = 0
        Dim _StartY As Double = 0
        Dim _CtrLv As Double = -1
        Dim _CtrHeight As Double = 0
        Dim _dtgrpobj As New System.Data.DataTable


        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0) !FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0) !FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0) !FHSysTableName.ToString
            Me.TableName = _dt.Rows(0) !FTTableName.ToString

            _SortField = _dt.Rows(0) !FTSortField.ToString

            _Str = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
            _Str &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)


            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then

                For Each Row As DataRow In _dtgrpobj.Rows
                    Select Case Row!FNGenFormObj.ToString
                        Case "H"
                            Dim _DMF As New HI.TL.DynamicForm(_objId, Val(Row!FNFormObjID.ToString), _dt, Me)
                            _DMF.SysObjID = Val(Row!FNFormObjID.ToString)
                            _DMF.SysTableName = Row!FTTableName.ToString
                            _DMF.SysDBName = Row!FTBaseName.ToString
                            _FormHeader.Add(_DMF)

                    End Select
                Next

            End If

        End If

        _dt.Dispose()
        _dtgrpobj.Dispose()

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


    Public ReadOnly Property Query As String
        Get
            Return _FormHeader(0).Query
        End Get
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(value As String)
            _MainKeyID = value
        End Set
    End Property

    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
    End Property

    Private _SysDBName As String = "HITECH_ACCOUNT"
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = "TEXPTCMInvoice"
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


    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(value As String)
            _TableName = value
        End Set
    End Property


    Public Sub DefaultsData()

        Me.FDInvoiceDate.DateTime = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
        Me.FTInvoiceBy.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
        Me.FTStateApp.Checked = False
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

        '    FNExpSoldId.Text = R!FTContinentCode.ToString
        '    FNHSysCountryId.Text = R!FTCountryCode.ToString
        '    FNHSysProvinceId.Text = R!FTProvinceCode.ToString
        '    FNExpShipId.Text = R!FTShipModeCode.ToString
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
        'FNTotalCarton.Value = 0
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
        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
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

        'Call CalculateSumGrid()

        '_LoadInvoice = False
    End Sub

    Public Sub LoadOrderDataInfo(ByVal Key As Object)

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        'Call ClearGrid()
        'Call LoadListOrderInfo(Me.FTCustomerPO.Text.Trim())
        'Call LoadOrderBreakDown(Key)

        _Spls.Close()

    End Sub

    Private Sub LoadListOrderInfo(ByVal _FTPORef As String)
        'Dim _Str As String = ""

        'Dim _FNHSysCmpId As Integer = 0

        '_Str = "SELECT TOP 1 FNHSysCmpId"
        '_Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS X WITH(NOLOCK)"
        '_Str &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

        '_FNHSysCmpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

        'If _FNHSysCmpId <= 0 Then
        '    _FNHSysCmpId = Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString))
        'End If

        ''_Str = "SELECT '0' AS FNSelect"
        ''_Str &= vbCrLf & "   , A.FTOrderNo"
        ''_Str &= vbCrLf & "  ,SEAS.FTSeasonCode,ISNULL(PMC.FTVenderPramCode,'') AS FTPGMCode  "
        ''_Str &= vbCrLf & "   , CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) "
        ''_Str &= vbCrLf & "         ELSE '' END AS FDOrderDate, ISNULL"
        ''_Str &= vbCrLf & "                  ((SELECT     CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate"
        ''_Str &= vbCrLf & "                      FROM         (SELECT     X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate"
        ''_Str &= vbCrLf & "                                             FROM          HITECH_MERCHAN.dbo.TMERTOrder AS X INNER JOIN"
        ''_Str &= vbCrLf & "                                                                    HITECH_MERCHAN.dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo"
        ''_Str &= vbCrLf & "                                             GROUP BY X.FTOrderNo) AS L1"
        ''_Str &= vbCrLf & "                      WHERE     (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, A.FNHSysCustId, C.FTCustCode, C.FTCustNameEN AS FTCustName"
        ''_Str &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        ''_Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId"
        ''_Str &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId  "
        ''_Str &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON "
        ''_Str &= vbCrLf & "  	A.FNHSysSeasonId = SEAS.FNHSysSeasonId"
        ''_Str &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PMC WITH(NOLOCK)  ON  A.FNHSysVenderPramId=PMC.FNHSysVenderPramId"
        ''_Str &= vbCrLf & " WHERE A.FTOrderNo IN ("
        ''_Str &= vbCrLf & " SELECT O.FTOrderNo"
        ''_Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        ''_Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        ''_Str &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        ''_Str &= vbCrLf & "         AND O.FNHSysCmpId=" & _FNHSysCmpId & ""
        ''_Str &= vbCrLf & " GROUP BY O.FTOrderNo"
        ''_Str &= vbCrLf & " ) AND A.FNJobState=1 "
        ''_Str &= vbCrLf & "   ORDER BY  A.FTOrderNo"


        '_Str = "    SELECT A.FTOrderNo, A.FTSubOrderNo"
        '_Str &= vbCrLf & " ,CASE WHEN ISDATE(A.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME,  A.FDShipDate), 103)  ELSE '' END AS FDShipDate"
        '_Str &= vbCrLf & " ,CASE WHEN ISDATE(B.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME,  B.FDOrderDate), 103)  ELSE '' END AS FDOrderDate"
        '_Str &= vbCrLf & " , SEAS.FTSeasonCode"
        '_Str &= vbCrLf & " , B.FNHSysCustId"
        '_Str &= vbCrLf & " , C.FTCustCode"
        '_Str &= vbCrLf & " , C.FTCustNameTH  AS FTCustName "
        '_Str &= vbCrLf & " , PMC.FTVenderPramCode AS FTPGMCode"
        '_Str &= vbCrLf & " , ISNULL(B.FTUpdUser,B.FTInsUser) AS FTInsUser"
        '_Str &= vbCrLf & " , BG.FTBuyGrpCode, "
        '_Str &= vbCrLf & "     BG.FNQtySpecialType"
        '_Str &= vbCrLf & " , BG.FNQtySpecial"
        '_Str &= vbCrLf & " , PVC.FTProvinceCode"
        '_Str &= vbCrLf & " , PVC.FTProvinceNameEN AS FTProvinceName"
        '_Str &= vbCrLf & " , Cmp.FTCmpCode"
        '_Str &= vbCrLf & " ,CASE WHEN ISNULL(Cmp.FTStateShipOverQty,'') ='1' AND ISNULL(PVC.FTStateShipOverQty,'') ='1' THEN BG.FNQtySpecial ELSE 0.00 END AS FNShipOverQty"
        '_Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS A INNER JOIN"
        '_Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B ON A.FTOrderNo = B.FTOrderNo LEFT OUTER JOIN"
        '_Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp ON B.FNHSysCmpId = Cmp.FNHSysCmpId LEFT OUTER JOIN"
        '_Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS PVC ON A.FNHSysProvinceId = PVC.FNHSysProvinceId LEFT OUTER JOIN"
        '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS BG ON B.FNHSysBuyGrpId = BG.FNHSysBuyGrpId LEFT OUTER JOIN"
        '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram AS PMC ON B.FNHSysVenderPramId = PMC.FNHSysVenderPramId LEFT OUTER JOIN"
        '_Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C ON B.FNHSysCustId = C.FNHSysCustId LEFT OUTER JOIN"
        '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SEAS ON B.FNHSysSeasonId = SEAS.FNHSysSeasonId"
        '_Str &= vbCrLf & "  WHERE A.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        '_Str &= vbCrLf & "         AND B.FNHSysCmpId=" & _FNHSysCmpId & ""

        '_Str &= vbCrLf & "   GROUP BY A.FTOrderNo, A.FTSubOrderNo, A.FDShipDate, B.FDOrderDate, SEAS.FTSeasonCode, C.FTCustCode, C.FTCustNameTH, PMC.FTVenderPramCode,  ISNULL(B.FTUpdUser,B.FTInsUser) , BG.FTBuyGrpCode, "
        '_Str &= vbCrLf & "  BG.FNQtySpecialType, BG.FNQtySpecial, PVC.FTStateShipOverQty, PVC.FTProvinceCode, PVC.FTProvinceNameEN, Cmp.FTCmpCode, Cmp.FTStateShipOverQty, B.FNHSysCustId"



        'Dim dt As System.Data.DataTable
        'dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
        'ogvOrderList.OptionsView.ShowAutoFilterRow = False

        'Me.GridOrderList.DataSource = dt.Copy

        'Me.GridOrderList.Refresh()

    End Sub

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

    '    _FNHSysContinentId = Integer.Parse(Val(FNExpSoldId.Properties.Tag.ToString))
    '    _FNHSysCountryId = Integer.Parse(Val(FNHSysCountryId.Properties.Tag.ToString))
    '    _FNHSysProvinceId = Integer.Parse(Val(FNHSysProvinceId.Properties.Tag.ToString))
    '    _FNHSysShipModeId = Integer.Parse(Val(FNExpShipId.Properties.Tag.ToString))
    '    _FNHSysShipPortId = Integer.Parse(Val(FNHSysShipPortId.Properties.Tag.ToString))


    '    _Qry = "SELECT TOP 1 FNHSysContinentId "
    '    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS X WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " WHERE  FTContinentCode='" & HI.UL.ULF.rpQuoted(FNExpSoldId.Text) & "'"

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
    '    _Qry &= vbCrLf & " WHERE  FTShipModeCode='" & HI.UL.ULF.rpQuoted(FNExpShipId.Text) & "'"

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

    Private Function SaveData() As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False
        Dim _CmpH As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else


                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else

                                    _Key = .Text

                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
        End If

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean
            For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
                For I As Integer = 0 To _FormHeader(cind).BaseFiled.ToArray.Count - 1
                    _FieldName = _FormHeader(cind).BaseFiled(I).FiledName.ToString
                    _FoundControl = False
                    If (_StateNew) Then

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else
                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            If _Values <> "" Then _Values &= ","
                            If _Fields <> "" Then _Fields &= ","

                            _Fields &= _FieldName

                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate")
                                    _Values &= HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
                                    _Values &= "''"
                                Case UCase("FTInsTime")
                                    _Values &= HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FTInsUser")
                                    _Values &= "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= "'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If


                    Else

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else

                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case Else
                                    If _Values <> "" Then _Values &= ","
                            End Select

                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FTUpdTime")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case UCase("FTUpdUser")
                                    _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If




                    End If

                Next
                If (_StateNew) Then
                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
                Else
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next
            SaveDataDetail()


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function



    Private Sub LoadCustomerPODefault()
        '_LoadInvoice = True
        'Dim _Qry As String = ""
        'Dim dt As System.Data.DataTable

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
        '    FNExpSoldId.Text = R!FTContinentCode.ToString
        '    FNHSysCountryId.Text = R!FTCountryCode.ToString
        '    FNHSysProvinceId.Text = R!FTProvinceCode.ToString
        '    FNExpShipId.Text = R!FTShipModeCode.ToString
        '    FNHSysShipPortId.Text = R!FTShipPortCode.ToString

        '    Exit For
        'Next

        'Call LoadOrderBreakDown(FTCustomerPO.Text)

        '_LoadInvoice = False

    End Sub

    Private Sub CheckStateLockEdit()

        'Dim _Qry As String = ""
        'Dim _State As Boolean
        '_Qry = "SELECT TOP 1 FTInvoiceNo"
        '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK)"
        '_Qry &= vbCrLf & "  WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text.Trim()) & "'"

        '_State = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> "")

        'FNExpSoldId.Properties.ReadOnly = _State
        'FNExpSoldId.Properties.Buttons(0).Enabled = Not (_State)

        'FNHSysCountryId.Properties.ReadOnly = _State
        'FNHSysCountryId.Properties.Buttons(0).Enabled = Not (_State)

        'FNHSysProvinceId.Properties.ReadOnly = _State
        'FNHSysProvinceId.Properties.Buttons(0).Enabled = Not (_State)

        'FNExpShipId.Properties.ReadOnly = _State
        'FNExpShipId.Properties.Buttons(0).Enabled = Not (_State)

        'FNHSysShipPortId.Properties.ReadOnly = _State
        'FNHSysShipPortId.Properties.Buttons(0).Enabled = Not (_State)

    End Sub

    Private Sub wXMLCreateInvoiceData_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call InitGrid()

        HI.TL.HandlerControl.AddHandlerGridColumnEdit(Me.ogvdetail)
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

    End Sub

    Private Sub FTCustomerPO_EditValueChanged(sender As Object, e As EventArgs)
        'If (Me.InvokeRequired) Then
        '    Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTCustomerPO_EditValueChanged), New Object() {sender, e})
        'Else

        '    Me.FTInvoiceNo.Text = ""
        '    Me.FDInvoiceDate.Text = ""
        '    Me.FTInvoiceExportNo.Text = ""
        '    Me.FDInvoiceExportDate.Text = ""
        '    Me.FNExpSoldId.Text = ""
        '    Me.FNHSysCountryId.Text = ""
        '    Me.FNHSysProvinceId.Text = ""
        '    Me.FNExpShipId.Text = ""
        '    Me.FNHSysShipPortId.Text = ""
        '    FTStateHanger.Checked = False
        '    Call CheckStateLockEdit()
        '    Call LoadOrderDataInfo(FTCustomerPO.Text)
        '    Call LoadCustomerPODefault()
        '    Me.otbdetail.SelectedTabPageIndex = 0

        'End If
    End Sub

    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTInvoiceNo.EditValueChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTInvoiceNo_EditValueChanged), New Object() {sender, e})
        Else

            Call LoadDataInfo(Me.FTInvoiceNo.Text)

            Me.FTStateApp.Checked = CheckPostInv()
            'Me.FTInvoiceExportNo.Text = ""
            'FTStateHanger.Checked = False
            'Call LoadCustomerPODefault()
            'Call LoadOrderPriceInfo(FTInvoiceNo.Text)
            'Call CheckStateLockEdit()

            'Call LoadListOrderInfo(Me.FTCustomerPO.Text.Trim)
            'Me.otbdetail.SelectedTabPageIndex = 0

        End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerrifyData() Then
                Me.FTStateApp.Checked = CheckPostInv()
                If Me.FTStateApp.Checked Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากมีการทำ Post Invoice ไปแล้ว กรุณาตรวจสอบ !!!!   ", 1802140852, Me.Text, "", MessageBoxIcon.Information)
                    Exit Sub
                End If
                If Me.SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private _StateClear As Boolean = False
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        _StateClear = True
        HI.TL.HandlerControl.ClearControl(Me)
        'Me.FTInvoiceNo.Text = ""
        'Me.FDInvoiceDate.Text = ""
        'Me.FTInvoiceExportNo.Text = ""
        'Me.FDInvoiceExportDate.Text = ""
        'Me.FNExpSoldId.Text = ""
        'Me.FNHSysCountryId.Text = ""
        'Me.FNHSysProvinceId.Text = ""
        'Me.FNExpShipId.Text = ""
        'Me.FNHSysShipPortId.Text = ""
        'Call CheckStateLockEdit()
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        _StateClear = False
    End Sub


    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Not CheckOwner() Then Exit Sub
            If CheckApprove() Then Exit Sub
            If Me.FTStateApp.Checked Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากมีการทำ Post Invoice ไปแล้ว กรุณาตรวจสอบ !!!!   ", 1802140852, Me.Text, "", MessageBoxIcon.Information)
                Exit Sub
            End If
            If DeleteData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Function CheckApprove() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1 isnull(FTStateApprove,'0') as  FTStateApprove "
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & " where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0") = "1"

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckPostInv() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1  FTInvoiceNo  "
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Post  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & " where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> ""
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTInvoiceBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else

            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTInvoiceBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If


        End If
    End Function


    Private Sub ReposPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        'Try

        '    With CType(Me.ogcsubbeakdown.DataSource, System.Data.DataTable)
        '        .AcceptChanges()
        '        _dtcuspobreakdown = .Copy
        '    End With

        '    With Me.ogvbreakdown
        '        Dim _GetColumnFocus As String = .FocusedColumn.FieldName.ToString
        '        If _dtcuspobreakdown.Select("FTColorway='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTColorway").ToString()) & "'  AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTNikePOLineItem").ToString()) & "'").Length > 0 Then

        '            Dim _TotalOrderQuantity As Double = 0
        '            For Each R As DataRow In _dtcuspobreakdown.Select("FTColorway='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTColorway").ToString()) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTNikePOLineItem").ToString()) & "'")
        '                Try
        '                    _TotalOrderQuantity = Val(R.Item(.FocusedColumn.FieldName.ToString()))
        '                Catch ex As Exception
        '                End Try
        '            Next

        '            If _TotalOrderQuantity >= e.NewValue Then
        '                If e.NewValue < 0 Then
        '                    e.Cancel = True
        '                Else
        '                    e.Cancel = False
        '                    .SetFocusedRowCellValue(.FocusedColumn.FieldName.ToString(), e.NewValue)
        '                    Call SumGrid()
        '                End If
        '            Else
        '                e.Cancel = True
        '                'If e.NewValue < 0 Then
        '                '    e.Cancel = True
        '                'Else
        '                '    e.Cancel = False
        '                'End If
        '            End If

        '        Else
        '            e.Cancel = True
        '        End If

        '    End With
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub ReposPrice_KeyDown(sender As Object, e As KeyEventArgs)
        'Try
        '    With Me.ogvbreakdown
        '        If .FocusedRowHandle < 0 Then Exit Sub
        '        Select Case e.KeyCode
        '            Case Keys.F9
        '                Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value

        '                For I As Integer = 0 To .RowCount - 1
        '                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
        '                        Select Case GridCol.FieldName.ToString.ToUpper
        '                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
        '                            Case Else
        '                                .SetRowCellValue(I, GridCol.FieldName.ToString, _Value)
        '                        End Select

        '                    Next
        '                Next

        '                CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
        '                ogcbreakdown.RefreshDataSource()
        '            Case Keys.F10
        '                Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value


        '                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
        '                    Select Case GridCol.FieldName.ToString.ToUpper
        '                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
        '                        Case Else
        '                            .SetFocusedRowCellValue(GridCol.FieldName.ToString, _Value)
        '                    End Select

        '                Next

        '                CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
        '                ogcbreakdown.RefreshDataSource()
        '            Case Keys.F11
        '                Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value

        '                For I As Integer = 0 To .RowCount - 1
        '                    .SetRowCellValue(I, .FocusedColumn.FieldName.ToString, _Value)
        '                Next

        '                CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()
        '                ogcbreakdown.RefreshDataSource()
        '        End Select
        '    End With


        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs)
        'If (Me.InvokeRequired) Then
        '    Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        'Else
        '    If FTOrderNo.Text <> "" Then

        '        If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then

        '            Dim _Qry As String = ""
        '            Dim _dt As System.Data.DataTable
        '            _Qry = "   Select A.FTOrderNo"
        '            _Qry &= vbCrLf & "    ,A.FTStyleCode"
        '            _Qry &= vbCrLf & "    ,A.FTCustCode"
        '            _Qry &= vbCrLf & " 	 ,A.FTPORef"
        '            _Qry &= vbCrLf & " 	 ,CASE WHEN ISDATE(FDShipDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDShipDate) ,103)  Else '' END AS FDShipDate"
        '            _Qry &= vbCrLf & " 	 ,CASE WHEN ISDATE(FDOrderDate) = 1 Then  Convert(varchar(10),  Convert(Datetime,FDOrderDate) ,103)  Else '' END AS FDOrderDate"
        '            _Qry &= vbCrLf & " 	 ,FNGrandQuantity"
        '            _Qry &= vbCrLf & " 	 ,FTCmpName"
        '            _Qry &= vbCrLf & "  FROM (SELECT O.FTOrderNo"
        '            _Qry &= vbCrLf & "    , ST.FTStyleCode"
        '            _Qry &= vbCrLf & "    , CT.FTCustCode"
        '            _Qry &= vbCrLf & "   , O.FTPORef"
        '            _Qry &= vbCrLf & "   , O.FDOrderDate"
        '            _Qry &= vbCrLf & "    ,ISNULL(("
        '            _Qry &= vbCrLf & " 	SELECT TOP 1 FDShipDate"
        '            _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK)"
        '            _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
        '            _Qry &= vbCrLf & " 	ORDER BY    FDShipDate ASC"
        '            _Qry &= vbCrLf & " 	),'') AS FDShipDate"
        '            _Qry &= vbCrLf & " ,ISNULL(("
        '            _Qry &= vbCrLf & " 	SELECT SUM(FNGrandQuantity) AS FNGrandQuantity"
        '            _Qry &= vbCrLf & " 	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS Sub WITH(NOLOCK)"
        '            _Qry &= vbCrLf & " 	WHERE Sub.FTOrderNo = O.FTOrderNo "
        '            _Qry &= vbCrLf & " 	),0) AS FNGrandQuantity"

        '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '                _Qry &= vbCrLf & "   ,Cmp.FTCmpCode + ' : ' + Cmp.FTCmpNameTH AS FTCmpName"
        '            Else
        '                _Qry &= vbCrLf & "   ,Cmp.FTCmpCode + ' : ' +  Cmp.FTCmpNameEN AS FTCmpName"
        '            End If

        '            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)  INNER JOIN"
        '            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN"
        '            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS CT WITH(NOLOCK)  ON O.FNHSysCustId = CT.FNHSysCustId"
        '            _Qry &= vbCrLf & "       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK)  ON O.FNHSysCmpId = Cmp.FNHSysCmpId"
        '            _Qry &= vbCrLf & "   WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
        '            _Qry &= vbCrLf & " 	 ) AS A"
        '            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        '            Me.FDShipDate.Text = ""
        '            Me.FDOrderDate.Text = ""

        '            For Each R As DataRow In _dt.Rows
        '                Me.FDShipDate.Text = R!FDShipDate.ToString
        '                Me.FDOrderDate.Text = R!FDOrderDate.ToString
        '                Exit For
        '            Next
        '        Else
        '            Me.FDShipDate.Text = ""
        '            Me.FDOrderDate.Text = ""
        '        End If
        '    Else
        '        Me.FDShipDate.Text = ""
        '        Me.FDOrderDate.Text = ""
        '    End If

        'End If
    End Sub

    Private Sub ogvbreakdown_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
        Try
            'With Me.ogvbreakdown

            'End With
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean


        'If Me.FNHSysContinentId.Text = "" Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysContinentId_lbl.Text)
        '    Me.FNHSysContinentId.Focus()
        '    Return False
        'End If
        'If Me.FNHSysCountryId.Text = "" Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysCountryId_lbl.Text)
        '    Me.FNHSysCountryId.Focus()
        '    Return False
        'End If
        If Me.FNHSysProvinceId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysProvinceId_lbl.Text)
            Me.FNHSysProvinceId.Focus()
            Return False
        End If

        If Me.FNHSysShipModeId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysShipModeId_lbl.Text)
            Me.FNHSysShipModeId.Focus()
            Return False
        End If
        'If Me.FNHSysShipPortId.Text = "" Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysShipPortId_lbl.Text)
        '    Me.FNHSysShipPortId.Focus()
        '    Return False
        'End If
        'If Me.FNHSysCurId.Text = "" Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysCurId_lbl.Text)
        '    Me.FNHSysCurId.Focus()
        '    Return False
        'End If

        'If Me.FNHSysGenderId.Text = "" Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysGenderId_lbl.Text)
        '    Me.FNHSysGenderId.Focus()
        '    Return False
        'End If


        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
                _Caption = ""

                For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)
                    If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
                        _Caption = ObjCaption.Text
                        Exit For
                    End If
                Next


                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Properties.Buttons.Count <= 1 Then
                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                        Pass = False
                                    End If
                                End If
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                If Val(.Value.ToString) <= 0 Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If .SelectedIndex < 0 Then Pass = False
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            'With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            '    If Val(.Value.ToString) <= 0 Then
                            '        Pass = False
                            '    End If
                            'End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                If .Image Is Nothing Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            If Obj.Text = "" Then
                                Pass = False
                            End If
                        Case Else
                            Pass = False
                    End Select

                    If Pass = False Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
                        Obj.Focus()
                        Return False
                    End If
                Next
            Next
        Next

        '---------- Validate Document ---------------------
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else
                                Dim _CmpH As String = ""
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                    Dim _dt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If

                            End If
                        End With

                End Select
            Next
        Next

        'If FNExchangeRate.Value <= 0 Then
        '    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
        '    FNExchangeRate.Focus()
        '    Return False
        'End If

        Return True
    End Function


    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Not VerrifyData() Then Exit Sub
        Dim _Fm As String = ""

        _Fm = "  {V_ReportExportInv.FTInvoiceNo} = '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
        With New HI.RP.Report
            .FormTitle = Me.Text
            .ReportFolderName = "Account\"
            Select Case FNHSysCustId.Text
                Case "NI"
                    .ReportName = "RptExportInvoice_Nike.rpt"
                Case "AE"
                    .ReportName = "RptExportInvoice_AEON.rpt"
                Case Else
                    .ReportName = "RptExportInvoice_Nike.rpt"
            End Select
            .Formular = _Fm
            .Preview()
        End With
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        Try
            If Me.FTInvoiceNo.Text <> "" And FTStateSendApp.Checked = False Then

                If ApprovedData() Then
                    HI.MG.ShowMsg.mInfo("ส่งข้อมูลอนุมัติ เรียบร้อย !!!! ", 18002031200, Me.Text, "", MessageBoxIcon.Information)
                Else
                    HI.MG.ShowMsg.mInfo("ส่งข้อมูลอนุมัติ ผิดพลาดกรุณาตรวจสอบ !!!! ", 18002031201, Me.Text, "", MessageBoxIcon.Information)
                End If

                Me.FTStateSendApp.Checked = True
                'End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function ApprovedData() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice    "
            _Cmd &= vbCrLf & "SET FTStateSendApp ='1'"
            _Cmd &= vbCrLf & ",FTSandApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDSendAapproveDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & "WHERE   FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SendMailApp() As Boolean
        'Dim _Qry As String = ""
        'Dim _UserMailTo As String = ""

        '_Qry = " SELECT TOP 1 Tm.FTUserName"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS Tm WITH(NOLOCK)  ON U.FNHSysTeamGrpId = Tm.FNHSysTeamGrpId"
        '_Qry &= vbCrLf & " WHERE  (U.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"

        '_UserMailTo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

        'If _UserMailTo <> "" Then

        '    Dim tmpsubject As String = ""
        '    Dim tmpmessage As String = ""

        '    tmpsubject = "Send Approve FOB Price Adjusted  Customer Purchased No." & Me.FTCustomerPO.Text & "  Invoice No. " & Me.FTInvoiceNo.Text & ""
        '    tmpmessage = "Send Approve FOB Price Adjusted  Customer Purchased No." & Me.FTCustomerPO.Text & "  Invoice No. " & Me.FTInvoiceNo.Text & ""
        '    tmpmessage &= vbCrLf & "Date :" & HI.UL.ULDate.FormatDateDB
        '    tmpmessage &= vbCrLf & "By :" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)


        '    If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 5, Me.FTCustomerPO.Text) Then

        '    End If

        'End If
        'Return True
    End Function

    Private Function CreateFirstSale(_FTCustomerPO As String) As Boolean
        Dim _Qry As String = ""
        _Qry = " SELECT TOP 1 FTCustomerPO"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTTransactionValueWorksheet WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCustomerPO='" & HI.UL.ULF.rpQuoted(_FTCustomerPO) & "' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "" Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการทำ First Sale Price กรุณาทำการตรวจสอบ !!!", 1510164875, Me.Text, , MessageBoxIcon.Warning)
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub ocmsaveinvoice_Click(sender As Object, e As EventArgs)
        'If Me.FTCustomerPO.Text <> "" Then
        '    If Me.FTInvoiceNo.Text <> "" And Me.FTInvoiceNo.Properties.Tag.ToString <> "" Then
        '        If Me.FDInvoiceDate.Text <> "" Then
        '            Dim _Qry As String = ""

        '            _Qry = "SELECT TOP 1 A.FTOrderNo"
        '            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        '            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId"
        '            _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId  "
        '            _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON "
        '            _Qry &= vbCrLf & "  	A.FNHSysSeasonId = SEAS.FNHSysSeasonId"
        '            _Qry &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PMC WITH(NOLOCK)  ON  A.FNHSysVenderPramId=PMC.FNHSysVenderPramId"
        '            _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  AS CMP WITH(NOLOCK) ON A.FNHSysCmpId  = CMP.FNHSysCmpId  "
        '            _Qry &= vbCrLf & " WHERE A.FTOrderNo IN ("
        '            _Qry &= vbCrLf & " SELECT O.FTOrderNo"
        '            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        '            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        '            _Qry &= vbCrLf & "  WHERE S.FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text.Trim) & "'"
        '            _Qry &= vbCrLf & " GROUP BY O.FTOrderNo"
        '            _Qry &= vbCrLf & " ) AND A.FNJobState=1 "
        '            _Qry &= vbCrLf & "  AND  CMP.FTStateXMLFile='1' "
        '            _Qry &= vbCrLf & "   ORDER BY  A.FTOrderNo"

        '            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
        '                If CreateFirstSale(Me.FTCustomerPO.Text) = False Then
        '                    Exit Sub
        '                End If
        '            End If

        '            Call HI.ST.Lang.SP_SETxLanguage(_InvoiceExport)

        '            Dim _FTRemark As String = ""

        '            _Qry = "   SELECT Top 1 FTInvoiceExportNote"
        '            _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK)"
        '            _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        '            _Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

        '            _FTRemark = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "")

        '            With _InvoiceExport
        '                .FTInvoiceExportNo.Text = Me.FTInvoiceExportNo.Text
        '                .FDInvoiceExportDate.Text = Me.FDInvoiceExportDate.Text
        '                .FTRemark.Text = _FTRemark
        '                .StateProc = False
        '                .ocmsave.Enabled = True
        '                .ocmcancel.Enabled = True
        '                .ShowDialog()

        '                If .StateProc Then

        '                    _Qry = "  UPDATE A SET "
        '                    _Qry &= vbCrLf & " FTInvoiceExportNo='" & HI.UL.ULF.rpQuoted(.FTInvoiceExportNo.Text) & "'"
        '                    _Qry &= vbCrLf & " ,FDInvoiceExportDate='" & HI.UL.ULDate.ConvertEnDB(.FDInvoiceExportDate.Text) & "'"
        '                    _Qry &= vbCrLf & " ,FTInvoiceExportNote='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "'"
        '                    _Qry &= vbCrLf & " ,FTStateMerApp='0'"
        '                    _Qry &= vbCrLf & " ,FTStateMerAppBy=''"
        '                    _Qry &= vbCrLf & " ,FTStateMerReject='0'"
        '                    _Qry &= vbCrLf & " ,FTStateMerRejectBy=''"
        '                    _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS A "
        '                    _Qry &= vbCrLf & "   WHERE  (FTCustomerPO = N'" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "') "
        '                    _Qry &= vbCrLf & "    AND (FTInvoiceNo = N'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "') "

        '                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT) = True Then

        '                        Me.FTInvoiceExportNo.Text = .FTInvoiceExportNo.Text
        '                        Me.FDInvoiceExportDate.Text = .FDInvoiceExportDate.Text
        '                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

        '                    Else

        '                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

        '                    End If

        '                End If
        '            End With

        '        Else
        '            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceDate_lbl.Text)
        '            FDInvoiceDate.Focus()
        '            FDInvoiceDate.SelectAll()
        '        End If
        '    Else
        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceNo_lbl.Text)
        '        FTInvoiceNo.Focus()
        '        FTInvoiceNo.SelectAll()
        '    End If
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustomerPO_lbl.Text)
        '    FTCustomerPO.Focus()
        '    FTCustomerPO.SelectAll()
        'End If
    End Sub


    Private Sub FNHSysContinentId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCustId.EditValueChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTInvoiceNo_EditValueChanged), New Object() {sender, e})
        Else
            If _LoadInvoice = False Then

                'Select Case sender.name.ToString.ToUpper
                '    Case "FNHSysContinentId".ToUpper

                '        Me.FNHSysCountryId.Text = ""
                '        Me.FNHSysProvinceId.Text = ""
                '        Me.FNExpShipId.Text = ""
                '        Me.FNHSysShipPortId.Text = ""

                '    Case "FNHSysCountryId".ToUpper

                '        Me.FNHSysProvinceId.Text = ""
                '        Me.FNExpShipId.Text = ""
                '        Me.FNHSysShipPortId.Text = ""

                '    Case "FNHSysProvinceId".ToUpper

                '        Me.FNExpShipId.Text = ""
                '        Me.FNHSysShipPortId.Text = ""

                '    Case "FNHSysShipModeId".ToUpper
                '        Me.FNHSysShipPortId.Text = ""

                'End Select

                'Call LoadOrderBreakDown(Me.FTCustomerPO.Text)

            End If

        End If

    End Sub

    Private Sub SumGrid()

        'Try

        '    Dim _Total As Integer = 0

        '    With Me.ogvbreakdown
        '        For I As Integer = 0 To .RowCount - 1
        '            _Total = 0
        '            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

        '                Select Case GridCol.FieldName.ToString.ToUpper
        '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
        '                    Case Else

        '                        If IsNumeric(.GetRowCellValue(I, GridCol)) Then
        '                            _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
        '                        Else
        '                            _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
        '                        End If

        '                End Select

        '            Next

        '            .SetRowCellValue(I, "Total", _Total)

        '        Next

        '    End With
        '    _Total = 0

        '    CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()

        'Catch ex As Exception
        'End Try

    End Sub

    Private Sub CalculateSumGrid()

        'Try

        '    Dim _Total As Integer = 0

        '    Dim dt As System.Data.DataTable

        '    With CType(ogcbreakdown.DataSource, System.Data.DataTable)
        '        .AcceptChanges()
        '        dt = .Copy()
        '    End With

        '    With Me.ogvbreakdown
        '        For I As Integer = 0 To .RowCount - 1
        '            _Total = 0
        '            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

        '                Select Case GridCol.FieldName.ToString.ToUpper
        '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
        '                    Case Else

        '                        If IsNumeric(.GetRowCellValue(I, GridCol)) Then
        '                            _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
        '                        Else
        '                            _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
        '                        End If

        '                End Select

        '            Next

        '            .SetRowCellValue(I, "Total", _Total)

        '        Next

        '    End With

        '    dt.BeginInit()
        '    For Each R As DataRow In dt.Rows
        '        _Total = 0
        '        For Each Col As DataColumn In dt.Columns
        '            Select Case Col.ColumnName.ToUpper
        '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTNikePOLineItem".ToUpper
        '                Case Else

        '                    If IsNumeric(R.Item(Col)) Then
        '                        _Total = _Total + CInt(R.Item(Col))
        '                    Else
        '                        _Total = _Total + 0 ' CDbl(.GetFocusedRowCellValue(GridCol))
        '                    End If

        '            End Select
        '        Next

        '        R!Total = _Total

        '    Next

        '    dt.EndInit()
        '    _Total = 0
        '    'ogcbreakdown.DataSource = dt
        '    'CType(ogcbreakdown.DataSource, System.Data.DataTable).AcceptChanges()

        'Catch ex As Exception
        'End Try

    End Sub

    Private Sub ocmpreviewtvwthb_Click(sender As Object, e As EventArgs)
        'If Not VerrifyData() Then Exit Sub
        'Dim _Fm As String = ""
        '_Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "' "
        '_Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
        'With New HI.RP.Report
        '    .FormTitle = Me.Text
        '    .ReportFolderName = "Account\"
        '    .ReportName = "ReportInvoiceCm_THB.rpt"
        '    .Formular = _Fm
        '    .Preview()
        'End With
    End Sub
    Private Sub Addrow()
        Try
            Dim _Cmd As String = ""
            Dim _dt As System.Data.DataTable
            Dim _oDt As System.Data.DataTable


            RemoveHandler RepositoryItemFTPORef.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
            RemoveHandler RepositoryItemFTStyleCode.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
            RemoveHandler RepositoryItemFTNikePOLineItem.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
            RemoveHandler RepositoryItemFTSizeBreakDown.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
            RemoveHandler RepositoryItemFTColorway.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
            RemoveHandler RepositoryItemFTCartonCode.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave


            With ogvdetail
                .BeginInit()
                For Each col As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    col.OptionsColumn.AllowEdit = True
                Next
                .EndInit()
            End With

            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With
            _oDt = New System.Data.DataTable
            Dim _r As DataRow
            _r = _dt.NewRow()

            _r("FTInvoiceNo") = "" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & ""
            _r("FTPORef") = ""
            _r("FNHSysStyleId") = 0
            _r("FNCTNS") = 0
            _r("FNTNW") = 0
            _r("FNTGW") = 0
            _r("FNQuantity") = 0
            _r("FNUnitPrice") = 0
            _r("FNTotalAmount") = 0
            _r("FTStyleCode") = ""
            _r("FTColorway") = ""
            _r("FTSizeBreakDown") = ""
            _r("FTNikePOLineItem") = ""
            _r("FNHSysCartonId") = 0
            _r("FTCartonCode") = ""
            _dt.Rows.Add(_r)


            Me.ogcdetail.DataSource = _dt
            _dt.Dispose()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            If Not VerrifyData() Then Exit Sub
            If Me.SaveData() Then
            End If
            If FNHSysCustId.Text <> "NI" Then
                LoadData()
                Addrow()
                RepositoryItemCFNCTNS.ReadOnly = False
                RepositoryItemCFNQuantity.ReadOnly = False
                RepositoryItemCFNTGW.ReadOnly = False
                RepositoryItemCFNTNW.ReadOnly = False
                RepositoryItemCFNUnitPrice.ReadOnly = False
                RepositoryItemFTCartonCode.Buttons.Item(0).Visible = True
                RepositoryItemFTColorway.Buttons.Item(0).Visible = True
                RepositoryItemFTNikePOLineItem.Buttons.Item(0).Visible = True
                RepositoryItemFTPORef.Buttons.Item(0).Visible = True
                RepositoryItemFTSizeBreakDown.Buttons.Item(0).Visible = True
                RepositoryItemFTStyleCode.Buttons.Item(0).Visible = True
                Exit Sub
            Else
                RepositoryItemCFNCTNS.ReadOnly = True
                RepositoryItemCFNQuantity.ReadOnly = True
                RepositoryItemCFNTGW.ReadOnly = True
                RepositoryItemCFNTNW.ReadOnly = True
                RepositoryItemCFNUnitPrice.ReadOnly = True
                RepositoryItemFTCartonCode.Buttons.Item(0).Visible = False
                RepositoryItemFTColorway.Buttons.Item(0).Visible = False
                RepositoryItemFTNikePOLineItem.Buttons.Item(0).Visible = False
                RepositoryItemFTPORef.Buttons.Item(0).Visible = False
                RepositoryItemFTSizeBreakDown.Buttons.Item(0).Visible = False
                RepositoryItemFTStyleCode.Buttons.Item(0).Visible = False
            End If


            Dim _oDt As System.Data.DataTable
            Dim _Cmd As String = ""
            _Cmd = "Select '0' as FTSelect ,  FTPckPlanNo , P. FTPORef , O.FTProvinceCode  ,FNHSysStyleId  ,  FTDescription  "
            _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan AS P WITH(NOLOCK)   "
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN (SELECT distinct  case when isnull(S.FTPORef ,'') = '' then    O.FTPORef else S.FTPORef  end  as FTPORef , P.FTProvinceCode  , O.FNHSysCustId , O.FNHSysStyleId , M.FTMainMatSpecEN as FTDescription "
            _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) "
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince AS P WITH(NOLOCK) ON S.FNHSysProvinceId = P.FNHSysProvinceId  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainMatSpec AS M WITH(NOLOCK) ON O.FNHSysStyleId =M.FNHSysStyleId  and O.FNHSysSeasonId  = M .FNHSysSeasonId "
            _Cmd &= vbCrLf & "     ) as O ON P.FTPORef = O.FTPORef "
            _Cmd &= vbCrLf & " where isnull( P.FTApproveState,'0')  = '1'   and FNHSysCustId=" & Integer.Parse("0" & Me.FNHSysCustId.Properties.Tag.ToString)
            _Cmd &= vbCrLf & " and O.FTProvinceCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysProvinceId.Text) & "'"
            '   _Cmd &= vbCrLf & " and  FTPckPlanNo+'|'+P.FTPORef not in  ( select  FTPckPlanNo+'|'+FTPORef From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_D  WITH(NOLOCK)  )"


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            With _PackingPlandPopup
                .ogcref.DataSource = _oDt.Copy

                .ShowDialog()

                If (.StateProc) Then

                    With DirectCast(.ogcref.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        _oDt = .Copy
                    End With

                    Dim _dt As System.Data.DataTable
                    With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        _dt = .Copy
                    End With

                    For Each R As DataRow In _oDt.Select("FTSelect = '1'")

                        _Cmd = "SELECT FTPckPlanNo, FTPORef,   FTPOLineNo,right( FTShortDescription,3) as FTColorway, FTSizeBreakDown,sum(FNItemQty) as FNItemQty,  sum( FNPackCount) as FNPackCount, sum(FNTotalNetWeight) as   FNTotalNetWeight, sum(FNGrossNetWeight) as  FNGrossNetWeight   "
                        _Cmd &= vbCrLf & " ,sum( FNPackCount)  *  sum(FNTotalNetWeight)  as FNTotalNet ,   sum( FNPackCount) *  sum(FNGrossNetWeight)  as FNTotalGross   "
                        _Cmd &= vbCrLf & " , (select top 1 FNNetPrice FRom   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination  WITH(NOLOCK)  where FTPOref = D.FTPORef and FTColorway = right(D.FTShortDescription,3)  and FTSizeBreakDown = D.FTSizeBreakDown and FTPOLineNo = D.FTPOLineNo ) as FNNetPrice  "
                        _Cmd &= vbCrLf & " , C.FNHSysCartonId ,C.FTCartonCode "
                        _Cmd &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D  as D WITH (NOLOCK)   "
                        _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK)  ON     D.FTPackCode  = C.FTCartonCode "
                        _Cmd &= vbCrLf & " Where FTPckPlanNo='" & HI.UL.ULF.rpQuoted(R!FTPckPlanNo.ToString) & "'"
                        _Cmd &= vbCrLf & " and FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"

                        _Cmd &= vbCrLf & " group by FTPckPlanNo, FTPORef, FTPOLineNo,right( FTShortDescription,3), FTSizeBreakDown , C.FNHSysCartonId , C.FTCartonCode  "

                        For Each X As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows
                            Dim _r As DataRow
                            _r = _dt.NewRow()
                            _r("FTInvoiceNo") = "" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & ""
                            _r("FTPORef") = X!FTPORef.ToString
                            _r("FNHSysStyleId") = Integer.Parse("0" & R!FNHSysStyleId.ToString)
                            _r("FNCTNS") = Integer.Parse(X!FNPackcount.ToString)
                            _r("FNTNW") = Double.Parse("0" & X!FNTotalNet.ToString)
                            _r("FNTGW") = Double.Parse("0" & X!FNGrossNetWeight.ToString)
                            _r("FNQuantity") = Integer.Parse("0" & X!FNItemQty.ToString)
                            _r("FNUnitPrice") = Double.Parse("0" & X!FNNetPrice.ToString)
                            _r("FNTotalAmount") = Integer.Parse("0" & X!FNItemQty.ToString) * Double.Parse("0" & X!FNNetPrice.ToString)
                            _r("FTStyleCode") = HI.Conn.SQLConn.GetField("Select Top 1  FTStyleCode From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK)   where FNHSysStyleId=" & Integer.Parse("0" & R!FNHSysStyleId.ToString), Conn.DB.DataBaseName.DB_MASTER, "")
                            _r("FTColorway") = "" & HI.UL.ULF.rpQuoted(X!FTColorway.ToString) & ""
                            _r("FTSizeBreakDown") = "" & HI.UL.ULF.rpQuoted(X!FTSizeBreakDown.ToString) & ""
                            _r("FTNikePOLineItem") = "" & HI.UL.ULF.rpQuoted(X!FTPOLineNo.ToString) & ""
                            _r("FNHSysCartonId") = Integer.Parse(X!FNHSysCartonId.ToString)
                            _r("FTCartonCode") = HI.UL.ULF.rpQuoted(X!FTCartonCode.ToString)
                            _dt.Rows.Add(_r)
                        Next
                    Next
                    Me.ogcdetail.DataSource = _dt
                    Me.FNAmt.Value = Double.Parse(_dt.Compute("sum(FNTotalAmount)", "FTPORef <> ''"))
                    _dt.Dispose()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataInfo(Key As Object)
        Try
            _ProcLoad = True

            Dim _Dt As System.Data.DataTable
            Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

            _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

            Dim _FieldName As String = ""
            For Each R As DataRow In _Dt.Rows
                For Each Col As DataColumn In _Dt.Columns
                    _FieldName = Col.ColumnName.ToString

                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    .Text = R.Item(Col).ToString
                                End With

                            Case ENM.Control.ControlType.CalcEdit
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Value = Val(R.Item(Col).ToString)
                                End With
                            Case ENM.Control.ControlType.ComboBoxEdit
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    Try
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    Catch ex As Exception
                                        .SelectedIndex = -1
                                    End Try
                                End With
                            Case ENM.Control.ControlType.CheckEdit
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                                End With
                            Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                                Obj.Text = R.Item(Col).ToString
                            Case ENM.Control.ControlType.PictureEdit
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    Try
                                        .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                    Catch ex As Exception
                                        .Image = Nothing
                                    End Try
                                End With
                            Case ENM.Control.ControlType.DateEdit
                                Try

                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                        .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                    End With
                                Catch ex As Exception
                                End Try
                            Case Else
                                Obj.Text = R.Item(Col).ToString
                        End Select
                    Next
                Next

                Exit For
            Next
            Call LoadData()

            Me.FTStateApp.Checked = CheckPostInv()
            _ProcLoad = False

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData()
        Try

            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            _Cmd = " Select D.FTInvoiceNo, D.FTPORef, D.FNHSysStyleId, D.FNCTNS, D.FNTNW, D.FNTGW, D.FNQuantity, D.FNUnitPrice, D.FNTotalAmount, T.FTStyleCode , D.FTColorway, D.FTSizeBreakDown ,D.FTNikePOLineItem ,D.FNHSysCartonId , C.FTCartonCode   "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice_D As D INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As T On D.FNHSysStyleId = T.FNHSysStyleId"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK)  ON     D.FNHSysCartonId  = C.FNHSysCartonId  "
            _Cmd &= vbCrLf & " where  D.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogcdetail.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveDataDetail() As Boolean
        Try
            Dim _Cmd As String = "" : Dim _oDt As System.Data.DataTable
            Dim _StrFileH As String = "FTInvoiceNo|FTPORef|FNHSysStyleId|FNCTNS|FNTNW|FNTGW|FNQuantity|FNUnitPrice|FNTotalAmount|FTNikePOLineItem|FTColorway|FTSizeBreakDown|FNHSysCartonId"
            Dim _CmdIns As String = "" : Dim _CmdUpd As String = "" : Dim _Value As String = "" : Dim _Where As String = "" : Dim _ValueUpd As String = ""
            Dim _PKey As String = "FTInvoiceNo"
            Dim _FKey As String = "FTPORef"
            Dim _FKey2 As String = "FNHSysStyleId"
            Dim _FKey3 As String = "FNUnitPrice"
            Dim _FKey4 As String = "FTNikePOLineItem"
            Dim _FKey5 As String = "FTColorway"
            Dim _FKey6 As String = "FTSizeBreakDown"


            _CmdIns = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D  "
            _CmdIns &= vbCrLf & "  (FTInsUser, FDInsDate, FTInsTime,  FTInvoiceNo, FTPORef, FNHSysStyleId, FNCTNS, FNTNW, FNTGW, FNQuantity, FNUnitPrice, FNTotalAmount, FTNikePOLineItem, FTColorway, FTSizeBreakDown,FNHSysCartonId )"
            _CmdIns &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ","

            _CmdUpd = " UPDATE   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D  "
            _CmdUpd &= vbCrLf & " set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _CmdUpd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _CmdUpd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
                For Each R As DataRow In _oDt.Rows
                    If R!FNHSysStyleId = 0 Then
                        R!FNHSysStyleId = HI.Conn.SQLConn.GetField("Select Top 1  FNHSysStyleId   From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle where FTStyleCode ='" & R!FTStyleCode.ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
                    End If

                Next
            End With


            For Each R As DataRow In _oDt.Rows
                _Value = ""
                _ValueUpd = "" : _Where = ""
                For Each _Str As String In _StrFileH.Split("|")
                    If _Value <> "" Then _Value &= ","
                    If Microsoft.VisualBasic.Left(_Str, 2).ToString = "FT" Then
                        _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item(_Str.ToString)) & "'"
                    Else
                        _Value &= R.Item(_Str.ToString)
                    End If

                    If _PKey = _Str Then
                        _Where = "  WHERE " & _PKey & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey2 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey2 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey3 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey3 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey4 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey4 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey5 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey5 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey6 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey6 & " = '" & R.Item(_Str.ToString) & "'"
                    Else
                        If _ValueUpd <> "" Then _ValueUpd &= ","
                        _ValueUpd &= _Str & " ='" & R.Item(_Str.ToString) & "'"
                    End If

                Next
                _Cmd = _CmdUpd & " , " & _ValueUpd & " " & _Where
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = _CmdIns & " " & _Value
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If

            Next

            '_Cmd = " Select sum(FNTotalAmount) AS FNTotalAmount   "
            '_Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D  WITH(NOLOCK) "
            '_Cmd &= vbCrLf & " Where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"

            'Dim _TotalAmt As Double = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, 0)
            '_Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice "
            '_Cmd &= vbCrLf & "Set FNTotalAmount=" & _TotalAmt
            '_Cmd &= vbCrLf & " , FTTotalAmountTHB='" & HI.UL.ULF.Convert_Bath_TH(_TotalAmt) & "'"
            '_Cmd &= vbCrLf & " ,FTTotalAmountENB='" & HI.UL.ULF.Convert_Bath_EN(_TotalAmt) & "'"
            '_Cmd &= vbCrLf & "  Where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            'End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged
        Try
            Me.FNHSysContinentId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysContinentId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)

            Me.FNHSysCustId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysCustId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)

            Me.FNHSysCountryId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysCountryId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)

            Me.FNHSysShipPortId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysShipPortId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)

            Me.FNHSysShipModeId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysShipModeId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)

            Me.FNHSysCurId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysCurId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)

            Me.FNHSysProvinceId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysProvinceId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)


            Me.FNHSysGenderId.Properties.Buttons(0).Visible = (ogvdetail.RowCount = 0)
            Me.FNHSysGenderId.Properties.ReadOnly = Not (ogvdetail.RowCount = 0)


        Catch ex As Exception

        End Try
    End Sub


    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                  FNAmt.EditValueChanged,
                                                                                  FNDisCountAmt.EditValueChanged,
                                                                                  FNVatPer.EditValueChanged,
                                                                                  FNVatAmt.EditValueChanged,
                                                                                  FNSurcharge.EditValueChanged

        Static _Proc As Boolean

        If Not (_Proc) And Not (_ProcLoad) Then
            _Proc = True
            Dim _POAmt As Double = FNAmt.Value

            If _POAmt = 0 Then
                FNDisCountAmt.Value = 0
                FNVatAmt.Value = 0
            End If

            Dim _DisPer As Double = FNDisCountPer.Value
            Dim _DisAmt As Double = FNDisCountAmt.Value
            Dim _VatPer As Double = FNVatPer.Value
            Dim _VatAmt As Double = FNVatAmt.Value
            Dim _SurAmt As Double = FNSurcharge.Value

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper
                    _DisPer = FNDisCountPer.Value
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt
                Case "FNDisCountAmt".ToUpper
                    _DisAmt = FNDisCountAmt.Value

                    If _POAmt > 0 Then
                        _DisPer = Format((_DisAmt * 100) / _POAmt, HI.ST.Config.PercentFormat)
                    Else
                        _DisPer = 0
                    End If
                    FNDisCountPer.Value = _DisPer
                Case "FNVatPer".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
                Case "FNVatAmt".ToUpper
                    _VatAmt = FNVatAmt.Value

                    If (_POAmt - _DisAmt) > 0 Then
                        _VatPer = Format((_VatAmt * 100) / (_POAmt - _DisAmt), HI.ST.Config.PercentFormat)
                    Else
                        _VatPer = 0
                    End If
                    FNVatPer.Value = _VatPer
                Case Else
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt

                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            Me.FNNetAmt.Value = (_POAmt - _DisAmt)

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            FNTotalAmount.Value = Format(Me.FNNetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)

            _Proc = False
        End If
    End Sub

    Private Sub FNTotalAmount_EditValueChanged(sender As Object, e As EventArgs) Handles FNTotalAmount.EditValueChanged
        Try
            If Not (_ProcLoad) Then
                Me.FTTotalAmountENB.Text = HI.UL.ULF.Convert_Bath_EN(FNTotalAmount.Value)
                Me.FTTotalAmountTHB.Text = HI.UL.ULF.Convert_Bath_TH(FNTotalAmount.Value)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        With ogvdetail
            If CheckOwner() = False Then Exit Sub
            If CheckApprove() Then Exit Sub


            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub



            Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
            Dim _PORef As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPORef").ToString
            Dim _StyleId As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysStyleId").ToString)
            Dim _Unitprice As Double = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNUnitPrice").ToString)
            Dim _POLineItem As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTNikePOLineItem").ToString
            Dim _Colorway As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString
            Dim _Size As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString
            Dim _CartonId As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysCartonId").ToString)


            Dim _Str As String = ""

            Try
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D "
                _Str &= vbCrLf & " where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                _Str &= vbCrLf & " and FTPORef='" & HI.UL.ULF.rpQuoted(_PORef) & "'"
                _Str &= vbCrLf & " and FNHSysStyleId=" & _StyleId
                _Str &= vbCrLf & " and FNUnitPrice=" & _Unitprice
                _Str &= vbCrLf & " and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Str &= vbCrLf & " and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_Size) & "'"
                _Str &= vbCrLf & " and FNHSysCartonId=" & _CartonId

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub
                End If


                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


                ' Exit Sub
            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Exit Sub
            End Try

            Me.SaveData()
            LoadData()
        End With
    End Sub

    Private Sub RepositoryItemCFNQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCFNQuantity.EditValueChanging
        Try
            Dim _Cmd As String = ""
            With Me.ogvdetail
                If FNHSysCustId.Text <> "NI" Then
                    Dim _UnitPrice As Double = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNUnitPrice").ToString)
                    Dim _Amt As Double = e.NewValue * _UnitPrice
                    .SetRowCellValue(.FocusedRowHandle, "FNTotalAmount", _Amt)
                End If
            End With
            GetTotal()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemCFNUnitPrice_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCFNUnitPrice.EditValueChanging
        Try
            Dim _Cmd As String = ""
            With Me.ogvdetail
                If FNHSysCustId.Text <> "NI" Then
                    Dim _UnitPrice As Double = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                    Dim _Amt As Double = e.NewValue * _UnitPrice
                    .SetRowCellValue(.FocusedRowHandle, "FNTotalAmount", _Amt)
                End If

            End With
            GetTotal()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetTotal()
        Try
            Dim _dt As System.Data.DataTable
            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With
            Me.FNAmt.Value = Double.Parse(_dt.Compute("sum(FNTotalAmount)", "FTPORef <> ''"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            If VerrifyData() Then
                If SaveData() Then
                    If Me.FTStateSendApp.Checked Then
                        Dim _Str As String = ""
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TEXPTCMInvoice] "
                        _Str &= Environment.NewLine & "SET  [FTStateApprove] = '1"
                        _Str &= Environment.NewLine & ", [FTApproveBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDApproveDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTApproveTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " WHERE FTInvoiceNo = '" & FTInvoiceNo.Text & "'"
                        If HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_ACCOUNT) Then
                            HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลเรียบร้อยแล้ว..  !!!!  ", 1802091337, Me.Text, "", MessageBoxIcon.Information)
                        Else
                            HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลผิดพลาด..  !!!!  ", 1802091338, Me.Text, "", MessageBoxIcon.Information)
                        End If


                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpost_Click(sender As Object, e As EventArgs) Handles ocmpost.Click
        Try
            If VerrifyData() Then
                If SaveData() Then
                    If Me.FTStateSendApp.Checked Then
                        Dim _Str As String = ""
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TEXPTCMInvoice] "
                        _Str &= Environment.NewLine & "SET  [FTStateApprove] = '1'"
                        _Str &= Environment.NewLine & ", [FTApproveBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDApproveDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTApproveTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " WHERE FTInvoiceNo = '" & FTInvoiceNo.Text & "'"
                        If HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_ACCOUNT) Then
                            HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลเรียบร้อยแล้ว..  !!!!  ", 1802091337, Me.Text, "", MessageBoxIcon.Information)
                        Else
                            HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลผิดพลาด..  !!!!  ", 1802091338, Me.Text, "", MessageBoxIcon.Information)
                        End If


                    End If
                End If
            End If

            HI.TL.HandlerControl.ClearControl(_InvoicePost)
            If Me.FTInvoiceNo.Text <> "" Then
                Dim _Value As String = Nothing
                Dim myList As New ArrayList

                Dim T As System.Type = _InvoicePost.GetType()

                Dim _Cmd As String = ""
                _Cmd = "Select Top 1  FTInvoiceNo  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Post with(nolock) where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "" Then
                    _Cmd = "Exec   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_EXPORT_POSTINVOICE '" & Me.FTInvoiceNo.Text & "','" & HI.ST.UserInfo.UserName & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                End If

                myList.Add(Me.FTInvoiceNo.Text)

                '        '        End If
                '        '    Next
                '        'End If

                '        Try

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "MnuExportInvPost"
                Call CallByName(Me.Parent.Parent, "CallWindowForm", CallType.Method, {"MnuExportInvPost", "LoadInvoiceInfo", myList.ToArray(GetType(String))})
                _InvoicePost.LoadInvoiceInfo(Me.FTInvoiceNo.Text)
                HI.ST.SysInfo.MenuName = _TmpMenu

                '        Catch ex As Exception
                '        End Try
                '    End If


            End If





        Catch ex As Exception

        End Try
    End Sub


End Class
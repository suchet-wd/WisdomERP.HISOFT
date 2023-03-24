Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Imports System.Net
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid

Public Class wExpCMInvoice
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
    Private _InvoiceExport As wFactoryInvoiceCMInvoiceExport
    Private _InvoicePost As wExpCMInvoicePost
    Private _PackingPlandPopup As wPackingPlanPop
    Private _Mainmatadd As wAddMainMat
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _ProcLoad As Boolean = False
    Private _SuplCode As String = ""
    Private _StatePortProvince As Boolean = False

    Private _PopupTruckway As wPopupTruckWay


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitFormControl()
        ' Add any initialization after the InitializeComponent() call.
        _InvoiceExport = New wFactoryInvoiceCMInvoiceExport
        HI.TL.HandlerControl.AddHandlerObj(_InvoiceExport)
        _PackingPlandPopup = New wPackingPlanPop
        HI.TL.HandlerControl.AddHandlerObj(_PackingPlandPopup)
        _InvoicePost = New wExpCMInvoicePost
        HI.TL.HandlerControl.AddHandlerObj(_InvoicePost)
        _Mainmatadd = New wAddMainMat
        HI.TL.HandlerControl.AddHandlerObj(_Mainmatadd)

        _PopupTruckway = New wPopupTruckWay
        HI.TL.HandlerControl.AddHandlerObj(_PopupTruckway)




        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _InvoiceExport.Name.ToString.Trim, _InvoiceExport)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PackingPlandPopup.Name.ToString.Trim, _PackingPlandPopup)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _InvoicePost.Name.ToString.Trim, _InvoicePost)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Mainmatadd.Name.ToString.Trim, _Mainmatadd)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PopupTruckway.Name.ToString.Trim, _PopupTruckway)
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

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            _SortField = _dt.Rows(0)!FTSortField.ToString

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



            If Not (_StatePortProvince) Then

                _Str = "Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince  "
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Str &= vbCrLf & " Set FTProvinceNameTH='" & HI.UL.ULF.rpQuoted(Me.FNHSysProvinceId_None.Text) & "'"
                Else
                    _Str &= vbCrLf & " Set FTProvinceNameEN='" & HI.UL.ULF.rpQuoted(Me.FNHSysProvinceId_None.Text) & "'"
                End If
                _Str &= vbCrLf & " where  FNHSysProvinceId=" & Integer.Parse("0" & Me.FNHSysProvinceId.Properties.Tag.ToString)
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

            End If


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            'Dim _Province As Integer = 0
            'Dim _Country As Integer = 0

            '_Str = " Select Top 1  FNHSysProvinceId   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice  "
            '_Str &= vbCrLf & " where   FTInvoiceNo='" & _Key & "'"
            '_Province = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "0")

            '_Str = " Select Top 1  FNHSysCountryId   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_TCMMProvince  "
            '_Str &= vbCrLf & " where   FNHSysProvinceId='" & _Province & "'"
            '_Country = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "0")

            '_Str = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice"
            '_Str &= vbCrLf & " Set FNHSysContinentId=" & HI.Conn.SQLConn.GetField("SELECT TOP (1) FNHSysContinentId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_TCNMCountry  WHERE  (FNHSysCountryId = " & _Country & ")", Conn.DB.DataBaseName.DB_ACCOUNT, "0")
            '_Str &= vbCrLf & " , FNHSysCountryId=" & _Country
            '_Str &= vbCrLf & " where   FTInvoiceNo='" & _Key & "'"
            'HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)


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
        Me.FTInvoiceNo.Properties.CharacterCasing = CharacterCasing.Normal
        Me.ogvBookRef.OptionsView.ShowAutoFilterRow = False
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

            Me.FTStateApp.Checked = ChkPost()
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

    Private Function CheckUser() As Boolean
        Try
            If Not (HI.ST.SysInfo.Admin) Then
                If Me.FTInvoiceBy.Text.Trim = HI.ST.UserInfo.UserName.Trim Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerrifyData() Then
                Me.FTStateApp.Checked = ChkPost()
                If Me.FTStateApp.Checked Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากมีการทำ Post Invoice ไปแล้ว กรุณาตรวจสอบ !!!!   ", 1802140852, Me.Text, "", MessageBoxIcon.Information)
                    Exit Sub
                End If
                If Not (CheckUser()) Then
                    HI.MG.ShowMsg.mInfo("กรุณาตรวจสอบ ชื่อผู้สร้างเอกสาร !!!", 1907091419, Me.Text, , MessageBoxIcon.Information)
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
    Private Function GenTruckWay() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _TruckWayNo As String = ""
            Dim _oDt As DataTable

            _Cmd = "Select Top 1  FTTruckWayNo , FTPartyName, FTPortOFDistation, FTFreightPayName, FNORTruckBillsNo"
            _Cmd &= vbCrLf & " From  TFGTruckWay "
            _Cmd &= vbCrLf & " where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
            If _oDt.Rows.Count > 0 Then
                _TruckWayNo = _oDt.Rows(0).Item("FTTruckWayNo")
            End If
            If _TruckWayNo = "" Then
                _TruckWayNo = HI.TL.Document.GetDocumentNo("HITECH_FG", "TFGTruckWay", "", "", HI.ST.SysInfo.CmpRunID, "")
            End If

            HI.TL.HandlerControl.ClearControl(_PopupTruckway)
            With _PopupTruckway
                .FTTruckWayNo.Text = _TruckWayNo
                .FTInvoiceNo.Text = Me.FTInvoiceNo.Text
                For Each R As DataRow In _oDt.Rows
                    .FTPartyName.Text = R!FTPartyName.ToString
                    .FTPortOFDistation.Text = R!FTPortOFDistation.ToString
                    .FTFreightPayName.Text = R!FTFreightPayName.ToString
                    .FNORTruckBillsNo.Text = R!FNORTruckBillsNo.ToString
                Next

                .ShowDialog()
                If (.State) Then

                    _Cmd = "Update TFGTruckWay "
                    _Cmd &= vbCrLf & " set  FTPartyName='" & HI.UL.ULF.rpQuoted(.FTPartyName.Text) & "'"
                    _Cmd &= vbCrLf & " ,FTPortOFDistation='" & HI.UL.ULF.rpQuoted(.FTPortOFDistation.Text) & "'"
                    _Cmd &= vbCrLf & " ,FTFreightPayName=" & HI.UL.ULF.rpQuoted(.FTFreightPayName.Text) & "'"
                    _Cmd &= vbCrLf & " ,FNORTruckBillsNo='" & HI.UL.ULF.rpQuoted(.FNORTruckBillsNo.Text) & "'"
                    _Cmd &= vbCrLf & " where FTTruckWayNo='" & HI.UL.ULF.rpQuoted(_TruckWayNo) & "'"
                    If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_FG) = False Then
                        _Cmd = "Insert into  TFGTruckWay"
                        _Cmd &= vbCrLf & " (  FTInsUser, FDInsDate, FTInsTime,  FTTruckWayNo, FDTruckWayDate, FTTruckWayBy, FTPartyName, FTPortOFDistation, FTFreightPayName, FNORTruckBillsNo, FTInvoiceNo ) "
                        _Cmd &= vbCrLf & " Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_TruckWayNo) & "'"
                        _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.FTPartyName.Text) & "'"
                        _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.FTPortOFDistation.Text) & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFreightPayName.Text) & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FNORTruckBillsNo.Text) & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_FG)
                    End If
                End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

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



            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref  WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
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

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, , Me.Text) = False Then
                Exit Sub
            End If

            Me.FTStateApp.Checked = ChkPost()
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
        Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")

        Call SaveData()
        GenTruckWay()

        Dim _FOBDescription As String = "" : Dim _HTSData As String = "" : Dim _CatData As String = ""
        _FOBDescription = HI.Conn.SQLConn.GetField("Select Top 1 FTFOBDescripton  From V_TCNMSupplier_forFOBDesc where FNHSysSuplId=" & Val(FNHSysSuplId.Properties.Tag), Conn.DB.DataBaseName.DB_ACCOUNT, "")
        '_HTSData = HI.Conn.SQLConn.GetField("Select Top 1 FTFOBDescripton  From V_TCNMSupplier_forFOBDesc where FNHSysSuplId=" & Val(FNHSysSuplId.Properties.Tag), Conn.DB.DataBaseName.DB_ACCOUNT, "")
        '_CatData = HI.Conn.SQLConn.GetField("Select Top 1 FTFOBDescripton  From  [Fn_getCAtData]('3502237429')" & Val(FNHSysSuplId.Properties.Tag), Conn.DB.DataBaseName.DB_ACCOUNT, "")


        Dim _Cmd As String = ""
        _Cmd = "Delete  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TmpReportExportInv_GrpPrice "
        '_Cmd &= vbCrLf & " where Expr1='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
        _Cmd &= vbCrLf & " where  FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

        _Cmd &= vbCrLf & "                      SELECT V_ReportExportInv.FTNikePOLineItem, V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS, CONVERT(varchar(20), CONVERT(numeric(18, 2), "
        _Cmd &= vbCrLf & "                                         V_ReportExportInv.FNUnitPrice)) AS FNUnitPrice, V_ReportExportInv.FTPORef, V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo,   "
        _Cmd &= vbCrLf & "                                       V_ReportExportInv.FTRangeNo, V_ReportExportInv_Grp.FTInvoiceGrpNo AS Expr1 "
        _Cmd &= vbCrLf & "    INTO #Tmp "
        _Cmd &= vbCrLf & "                                FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..V_ReportExportInv_Assort AS V_ReportExportInv INNER  JOIN "
        _Cmd &= vbCrLf & "                                       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. V_ReportExportInv_Grp AS V_ReportExportInv_Grp ON V_ReportExportInv.FTInvoiceGrpNo = V_ReportExportInv_Grp.FTInvoiceBookNo "
        '_Cmd &= vbCrLf & "                                       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..V_TMERMMainMatSpec AS V_TMERMMainMatSpec ON V_ReportExportInv.FNHSysStyleId = V_TMERMMainMatSpec.FNHSysStyleId   "
        '_Cmd &= vbCrLf & "                                                               AND   V_ReportExportInv.FNHSysSeasonId = V_TMERMMainMatSpec.FNHSysSeasonId"
        _Cmd &= vbCrLf & " where V_ReportExportInv_Grp.FTInvoiceGrpNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
        _Cmd &= vbCrLf & "                 GROUP BY V_ReportExportInv.FTNikePOLineItem, V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS, V_ReportExportInv.FNUnitPrice, V_ReportExportInv.FTPORef, "
        _Cmd &= vbCrLf & "                                    V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo,   V_ReportExportInv.FTRangeNo, V_ReportExportInv_Grp.FTInvoiceGrpNo "
        _Cmd &= vbCrLf & "    "

        _Cmd &= vbCrLf & " insert into     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TmpReportExportInv_GrpPrice "
        _Cmd &= vbCrLf & " (FTUserLogin, FTNikePOLineItem, FNTNW, FNTGW, FNCTNS, FNUnitPrice, FTPORef, FTInvoiceGrpNo, FTInvoiceBookNo, FTMainMatSpecCode, FTRangeNo, Expr1, FNHSysStyleId, FNUnitPriceMuti)"
        _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', V_ReportExportInv.FTNikePOLineItem,  V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS,  isnull(V_ReportExportInv.FNUnitPrice,0) as FNUnitPrice,   "
        _Cmd &= vbCrLf & "       V_ReportExportInv.FTPORef, V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo, V_TMERMMainMatSpec.FTMainMatSpecCode, V_ReportExportInv.FTRangeNo, V_ReportExportInv_Grp.FTInvoiceGrpNo AS Expr1, V_ReportExportInv.FNHSysStyleId,"
        _Cmd &= vbCrLf & "    (SELECT TOP 1 STUFF "
        _Cmd &= vbCrLf & "     ((SELECT  distinct ' / ' + t2.FNUnitPrice   "
        _Cmd &= vbCrLf & "               FROM       #Tmp  as  t2 "
        _Cmd &= vbCrLf & "     "
        _Cmd &= vbCrLf & "     WHERE   t2.FTPORef = V_ReportExportInv.FTPORef AND t2.FTInvoiceGrpNo = V_ReportExportInv.FTInvoiceGrpNo AND t2.FTRangeNo = V_ReportExportInv.FTRangeNo  " 'AND   t2.FTNikePOLineItem = V_ReportExportInv.FTNikePOLineItem 
        _Cmd &= vbCrLf & "                     FOR XML PATH('')), 1, 2, '') AS FTSubOrderNo) AS FNUnitPriceMuti "
        _Cmd &= vbCrLf & "    FROM     V_ReportExportInv_Assort AS V_ReportExportInv INNER JOIN "
        _Cmd &= vbCrLf & "     V_ReportExportInv_Grp AS V_ReportExportInv_Grp ON V_ReportExportInv.FTInvoiceGrpNo = V_ReportExportInv_Grp.FTInvoiceBookNo INNER JOIN "
        _Cmd &= vbCrLf & "       V_TMERMMainMatSpec AS V_TMERMMainMatSpec ON V_ReportExportInv.FNHSysStyleId = V_TMERMMainMatSpec.FNHSysStyleId  "
        _Cmd &= vbCrLf & " where V_ReportExportInv_Grp.FTInvoiceGrpNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
        _Cmd &= vbCrLf & " GROUP BY V_ReportExportInv.FTNikePOLineItem, V_ReportExportInv_Grp.FTInvoiceBookNo, V_ReportExportInv.FNTNW, V_ReportExportInv.FNTGW, V_ReportExportInv.FNCTNS, V_ReportExportInv.FNUnitPrice, V_ReportExportInv.FTPORef,  "
        _Cmd &= vbCrLf & "   V_ReportExportInv.FTInvoiceGrpNo, V_ReportExportInv.FTInvoiceBookNo, V_TMERMMainMatSpec.FTMainMatSpecCode, V_ReportExportInv.FTRangeNo, V_ReportExportInv.FNHSysStyleId, V_ReportExportInv_Grp.FTInvoiceGrpNo"
        'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)



        _Cmd &= vbCrLf & "Delete  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.Tmp_V_ReportExportInv "
        _Cmd &= vbCrLf & " where FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        _Cmd &= vbCrLf & " insert into     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.Tmp_V_ReportExportInv "
        _Cmd &= vbCrLf & " ( FTUserLogIn  ,  FTCmpCode, FTCmpNameTH, FTCmpNameEN, FTAddressInvoiceTH, FTAddressInvoiceEN, FTExpSoldAddrEN, FTRemark, FTInvoiceNo, FTInvoiceBy, FDInvoiceDate, FTShipModeCode, FTShipModenNameTH,  "
        _Cmd &= vbCrLf & " FTShipModeNameEN, FTShipPortCode, FTShipPortNameTH, Expr1, FTProvinceCode, FTProvinceNameTH, FTProvinceNameEN, FTPORef, FTStyleCode, FNQuantity, FNUnitPrice, FNTotalAmount, FTTotalAmountTHB, FTTotalAmountENB, "
        _Cmd &= vbCrLf & " FTNikePOLineItem, FTColorway, FTSizeBreakDown, FTVesserName, FNHSysTermOfPMId, FNHSysCrTermId, FNExchangeRate, FNCreditDay, FNCTNS, FNTNW, FNTGW, FNHSysCartonId, FNHSysSeasonId, FNHSysCmpId, "
        _Cmd &= vbCrLf & " FTStateSendApp, FTSandApproveBy, FDSendAapproveDate, FTSendApproveTime, FTStateApprove, FTApproveBy, FDApproveDate, FTApproveTime, FTCustNameEN, FTCustNameTH, FTCustCode, FTExpShipNameEN, FTExpShipCode,  "
        _Cmd &= vbCrLf & "  FDESTTimeDept, FDESTTimeArrl, FTInvoiceBookNo, FDShipDate, FNHSysCurId, FTCurDescEN, FNHSysStyleId, FTRangeNo, FTLineNo, FTDiamondMarkCode, FTInvoiceGrp, FTPORefNo , FNHSysSuplId , FTFOBDescripton) "
        _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  , FTCmpCode, FTCmpNameTH, FTCmpNameEN, FTAddressInvoiceTH, FTAddressInvoiceEN, FTExpSoldAddrEN, FTRemark, FTInvoiceNo, FTInvoiceBy, FDInvoiceDate, FTShipModeCode, FTShipModenNameTH,   "
        _Cmd &= vbCrLf & "FTShipModeNameEN, FTShipPortCode, FTShipPortNameTH, Expr1, FTProvinceCode, FTProvinceNameTH, FTProvinceNameEN, FTPORef, FTStyleCode, FNQuantity, FNUnitPrice, FNTotalAmount, FTTotalAmountTHB, FTTotalAmountENB,  "
        _Cmd &= vbCrLf & " FTNikePOLineItem, FTColorwayCode, FTSizeBreakDown, FTVesserName, FNHSysTermOfPMId, FNHSysCrTermId, FNExchangeRate, FNCreditDay, FNCTNS, FNTNW, FNTGW, FNHSysCartonId, FNHSysSeasonId, FNHSysCmpId, "
        _Cmd &= vbCrLf & " FTStateSendApp, FTSandApproveBy, FDSendAapproveDate, FTSendApproveTime, FTStateApprove, FTApproveBy, FDApproveDate, FTApproveTime, FTCustNameEN, FTCustNameTH, FTCustCode, FTExpShipNameEN, FTExpShipCode, "
        _Cmd &= vbCrLf & " FDESTTimeDept, FDESTTimeArrl, FTInvoiceBookNo, FDShipDate, FNHSysCurId, FTCurDescEN, FNHSysStyleId, FTRangeNo, FTLineNo, FTDiamondMarkCode, FTInvoiceGrp, FTPORefNo ," & Val(Me.FNHSysSuplId.Properties.Tag) & " ,'" & _FOBDescription & "'"
        _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_ReportExportInv "
        _Cmd &= vbCrLf & " WHERE  (FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "')"
        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


        Dim _Fm As String = ""

        _Fm = "  {V_ReportExportInv.FTInvoiceNo} = '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'  and  {V_ReportExportInv.FTUserLogIn} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        With New HI.RP.Report
            .FormTitle = Me.Text
            .ReportFolderName = "Account\"
            Select Case FNHSysCustId.Text
                Case "NI"
                    If _SuplCode.Trim.ToString = "DAMCO" Then
                        .ReportName = "RptExportInvoice_Nike.rpt"
                    Else
                        .ReportName = "RptExportInvoice_Nike_APL.rpt"
                    End If
                Case "AE"
                    .ReportName = "RptExportInvoice_AEON.rpt"
                Case Else
                    .ReportName = "RptExportInvoice_Nike.rpt"
            End Select
            .Formular = _Fm
            .Preview()
        End With
        Try
            _spls.Close()
        Catch ex As Exception
            _spls.Close()
        End Try
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        Try
            If Me.FTInvoiceNo.Text <> "" Then

                If ApprovedData() Then
                    HI.MG.ShowMsg.mInfo("ส่งข้อมูลอนุมัติ เรียบร้อย !!!! ", 18002031200, Me.Text, "", MessageBoxIcon.Information)
                Else
                    HI.MG.ShowMsg.mInfo("ส่งข้อมูลอนุมัติ ผิดพลาดกรุณาตรวจสอบ !!!! ", 18002031201, Me.Text, "", MessageBoxIcon.Information)
                End If


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
                                    If _FieldName.ToUpper = "FNHSysCmpId".ToUpper Then
                                        If Me.FNHSysCmpId.Text = "" Then
                                            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
                                        End If
                                    End If
                                End With

                            Case ENM.Control.ControlType.CalcEdit
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Value = Val(R.Item(Col).ToString)
                                    If _FieldName.ToUpper = "FTStateApprove".ToUpper Then
                                        If Val(R.Item(Col).ToString) = "2" Then
                                            Me.FTStateCalcel.Checked = True
                                        Else
                                            Me.FTStateCalcel.Checked = False
                                        End If
                                    End If
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


            Me.FTStateApp.Checked = ChkPost()
            _ProcLoad = False
            If Me.FTInvoiceNo.Text <> "" Then
                Call LoadData()
            End If

            If (Me.FNHSysProvinceId.Text <> "" And Me.FNHSysShipModeId.Text <> "") Then
                _Str = "Select top 1  "
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Str &= vbCrLf & " FTPortProvinceNameTH "
                Else
                    _Str &= vbCrLf & " FTPortProvinceNameEN "
                End If
                _Str &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMPortProvince WITH(NOLOCK) "
                _Str &= vbCrLf & " where  FNHSysProvinceId =" & Integer.Parse("0" & Me.FNHSysProvinceId.Properties.Tag)
                _Str &= vbCrLf & " and FNHSysShipModeId=" & Integer.Parse("0" & Me.FNHSysShipModeId.Properties.Tag)
                Dim _provinceName As String = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

                If _provinceName <> "" Then
                    Me.FNHSysProvinceId_None.Text = _provinceName
                    _StatePortProvince = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub LoadData()
        Try

            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable



            '_Cmd = "    SELECT  distinct O.FNHSysStyleId, BD.FTColorway, BD.FTSizeBreakDown, convert(nvarchar(5), convert(int, BD.FTNikePOLineItem)) as FTNikePOLineItem , O.FDShipDate, O.FNHSysShipModeId "
            '_Cmd &= vbCrLf & " , O.FTPORef , max(  case when isnull(BD.FNNetPrice,0) = 0 then BD.FNPrice  else BD.FNNetPrice End)"
            '_Cmd &= vbCrLf & " Over (partition by O.FNHSysStyleId, BD.FTColorway, BD.FTSizeBreakDown ,convert(nvarchar(5), convert(int, BD.FTNikePOLineItem)) , O.FDShipDate, O.FNHSysShipModeId  , O.FTPORef )  as FNNetPrice  "
            '_Cmd &= vbCrLf & " into #TmpOrderInfo"
            '_Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_TMERTOrder_Info AS O  LEFT OUTER JOIN "
            '_Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS BD WITH (NOLOCK) ON O.FTOrderNo = BD.FTOrderNo  AND O.FTSubOrderNo = BD.FTSubOrderNo  "
            '_Cmd &= vbCrLf & "    INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D As D with(nolock) ON O.FTPORef = D.FTPORef "
            '_Cmd &= vbCrLf & " where  D.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            Dim _PriceDIstinct As Double = 0
            _PriceDIstinct = HI.Conn.SQLConn.GetField(" Select  FNPriceDis  from fn_getDistinctExport('" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "')", Conn.DB.DataBaseName.DB_ACCOUNT, "1")


            _Cmd &= vbCrLf & " Select distinct D.FTInvoiceNo, D.FTPORef, D.FNHSysStyleId, D.FNCTNS, D.FNTNW, D.FNTGW, D.FNQuantity, ROUND ((isnull(BD.FNNetPrice ,BD.FNPrice) * " & _PriceDIstinct & "),2) as FNUnitPrice "
            _Cmd &= vbCrLf & " ,ROUND(( isnull(BD.FNNetPrice ,BD.FNPrice) * " & _PriceDIstinct & "),2) * D.FNQuantity as FNTotalAmount, T.FTStyleCode , D.FTColorway, D.FTSizeBreakDown ,D.FTNikePOLineItem ,D.FNHSysCartonId , C.FTCartonCode ,D.FTRangeNo , D.FTLineNo  "
            _Cmd &= vbCrLf & " , D.FTPORefNo , isnull(BD.FNNetPrice,0) as FNNetPriceImport   ,PL.FTAddr1EN   AS FTAddrSold  ,PV.FTRemark   AS FTAddrShip , isnull(ZS.FTSuplCode ,  R.FTSuplCode ) as FTSuplCode "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D As D with(nolock) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice AS H WITH(NOLOCK) ON  D.FTInvoiceNo = H.FTInvoiceNo INNER JOIN  "
            _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As T with(nolock) On D.FNHSysStyleId = T.FNHSysStyleId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK)  ON     D.FNHSysCartonId  = C.FNHSysCartonId  "
            _Cmd &= vbCrLf & " 	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan_D AS PD WITH(NOLOCK)  ON D.FTPORef = PD.FTPORef And D.FTPORefNo = PD.FTPORefNo  "
            _Cmd &= vbCrLf & " And D.FTRangeNo = PD.FTRangeNo And D.FTSizeBreakDown = PD.FTSizeBreakDown And D.FTColorway = PD.FTColorWay "
            _Cmd &= vbCrLf & " 	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan AS PH WITH(NOLOCK) ON PD.FTPckPlanNo = PH.FTPckPlanNo and PD.FTPORef = PH.FTPORef and PD.FTPORefNo = PH.FTPORefNo "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo. V_Order_Export_ShipDestination AS BD   ON  convert(nvarchar(5), convert(int, D.FTNikePOLineItem)) = BD.FTNikePOLineItem"
            '_Cmd &= vbCrLf & "    LEFT OUTER JOIN      (SELECT   O.FNHSysStyleId, BD.FTColorway, BD.FTSizeBreakDown,  BD.FTNikePOLineItem , O.FDShipDate, O.FNHSysShipModeId "
            '_Cmd &= vbCrLf & " , O.FTPORef ,  case when isnull(BD.FNNetPrice,0) = 0 then BD.FNPrice  else BD.FNNetPrice end FNNetPrice "
            '_Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_TMERTOrder_Info AS O  LEFT OUTER JOIN "
            ''_Cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH (NOLOCK) ON O.FTOrderNo = S.FTOrderNo LEFT OUTER JOIN "
            '_Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS BD WITH (NOLOCK) ON O.FTOrderNo = BD.FTOrderNo  AND O.FTSubOrderNo = BD.FTSubOrderNo ) AS BD  ON  convert(nvarchar(5), convert(int, D.FTNikePOLineItem)) = BD.FTNikePOLineItem "
            _Cmd &= vbCrLf & "  And D.FTSizeBreakDown = BD.FTSizeBreakDown And D.FTPORef = BD.FTPORef And D.FNHSysStyleId = BD.FNHSysStyleId And RIGHT( PD.FTShortDescription , 3) = BD.FTColorway  " 'and H.FNHSysShipModeId = BD.FNHSysShipModeId and PH.FDShipDate = BD.FDShipDate
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS PL with(nolock) ON BD.FNHSysPlantId = PL.FNHSysPlantId "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS PT WITH(NOLOCK) ON BD.FNHSysContinentId = PT.FNHSysContinentId   "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS R WITH(NOLOCK) ON PT.FNHSysSuplId = R.FNHSysSuplId  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS PV with(nolock) on BD.FNHSysProvinceId = PV.FNHSysProvinceId "
            _Cmd &= vbCrLf & "  left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram as VN ON  BD.FNHSysVenderPramId = VN.FNHSysVenderPramId "
            _Cmd &= vbCrLf & " outer apply (Select  top  1  ZT.FNHSysSuplId , ZS.FTSuplCode  "
            _Cmd &= vbCrLf & "   from    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMCountryForwarder ZT  "
            _Cmd &= vbCrLf & "  left outer join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS ZS with(nolock) ON ZT.FNHSysSuplId = ZS.FNHSysSuplId"
            _Cmd &= vbCrLf & " where  BD.FNHSysCountryId  = ZT.FNHSysCountryId  "
            _Cmd &= vbCrLf & "  and BD.FNHSysVenderPramId  = ZT.FNHSysVenderPramId   ) ZS "
            _Cmd &= vbCrLf & " "
            _Cmd &= vbCrLf & " "

            _Cmd &= vbCrLf & " where  D.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _Cmd &= vbCrLf & " ORDER BY  D.FTNikePOLineItem  ASC , D.FTPORef ASC ,D.FTRangeNo ASC , D.FTLineNo  ASC "


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogcdetail.DataSource = _oDt


            _Cmd = "Select  Distinct FTPORef , FTNikePOLineItem"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D As D with(nolock) "
            _Cmd &= vbCrLf & " where  D.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            '_oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogcPOLine.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            Me.FTHarmonizedCode.Text = ""
            Me.FNHSysExpSoldId_None.Text = ""
            Me.FNHSysExpShipId_None.Text = ""
            _SuplCode = ""
            For Each R As DataRow In _oDt.Rows
                Me.FNHSysExpSoldId_None.Text = R!FTAddrSold.ToString
                Me.FNHSysExpShipId_None.Text = R!FTAddrShip.ToString
                Me.FTHarmonizedCode.Text = "" ' R!FTHTSData.ToString
                Me.FNHSysSuplId.Text = R!FTSuplCode.ToString
                _SuplCode = R!FTSuplCode.ToString
                Exit For
            Next



            '_Cmd = " SELECT  Top 1  '0' AS FTSelect ,   FTPckPlanNo,P.FTPORef, FNQuantity, FNPackcount, FNNet, FNTotalNet, FNGrossWeight, P.FNHSysUnitId, FNVol, FTVolUnit, isnull( FTApproveState ,'0') as FTApproveState , FTApproveBy,  FDApproveDate "
            '_Cmd &= vbCrLf & " ,  isnull(P.FDShipDate,  V.FDShipDate) as  FDShipDate  ,  isnull(FNHSysMainMatSpecId,0) as  FNHSysMainMatSpecId  , V.FNHSysProvinceId as  FNHSysProvinceId_Hide  "
            '_Cmd &= vbCrLf & " ,   V.FTMainMatSpecEN as FTMainMatSpec   "
            '_Cmd &= vbCrLf & " ,   (Select Top 1 isnull(FTProvinceCode,'') as FTProvinceCode  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince  WITH (NOLOCK)  Where FNHSysProvinceId = V.FNHSysProvinceId )  as FNHSysProvinceId  "
            '_Cmd &= vbCrLf & " ,     V.FTPlantCode , V.FTCmpCode , V.FNHSysCmpId , V.FTAddressInvoice ,  V.FNHSysCustId , V.FTCustCode , V.FTCustNameEN   , V.FNHSysShipModeId, V.FTShipModeCode  , V.FNHSysCountryId "
            '_Cmd &= vbCrLf & " ,R.FTInvoiceNo , R.FTInvoiceNoPre  , FTHTSData , FTCatData , CT.FTCustExpProductDesc , V.FTStyleCode  , V.FTProvinceNameEN as FTProvinceName, V.FTCountryNameEN as FTCountryName , V.FTCountry "
            ''_Cmd &= vbCrLf & "  , case when isnull(CC.FNWidth,0)  = 0 then '0' else   "
            ''_Cmd &= vbCrLf & "  convert(nvarchar(30),  convert(numeric(18,2),(isnull(CC.FNWidth,0) /10))) + ' x ' +  convert(nvarchar(30), convert(numeric(18,2) , ( CC.FNLength/10)) ) +' x '+  convert(nvarchar(30),  convert(numeric(18,2),(CC.FNWeight * 10))) end AS FNCNT  "


            '_Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan AS P WITH(NOLOCK)  "
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. V_ExportInvoice_H AS V ON P.FTPORef = V.FTPORef   "
            '_Cmd &= vbCrLf & " LEFT OUTER JOIN  (  SELECT Distinct  H.FTInvoiceNo, H.FTInvoiceRefNo, D.FTPORef , isnull(R.FTInvoiceNo,'') AS FTInvoiceNoPre   "
            '_Cmd &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) LEFT OUTER  JOIN  "
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice_D AS D WITH (NOLOCK) ON H.FTInvoiceNo = D.FTInvoiceNo "
            '_Cmd &= vbCrLf & "     LEFT OUTER JOIN (	  Select   FTInvoiceNo,FTInvoiceRefNo  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_Ref WITH(NOLOCK)   "
            '_Cmd &= vbCrLf & "  ) as R ON H.FTInvoiceNo  = R.FTInvoiceRefNo  ) AS R  ON   P.FTPORef  = R.FTPORef"
            ''_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..V_TCNMCarton AS CC ON R.FNHSysCartonId = CC.FNHSysCartonId"

            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMHTSData AS C WITH(NOLOCK) ON V.FNHSysProdTypeId = C.FNHSysProdTypeId and V.FNHSysGenderId = C.FNHSysGenderId "
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS CT WITH(NOLOCK) ON V.FNHSysCustId= CT.FNHSysCustId "

            '_Cmd &= vbCrLf & " where  R.FTInvoiceNoPre='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            ''_Cmd &= vbCrLf & " and P.FTPORef not in (Select distinct FTPORef  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D as D WITH(NOLOCK)   "
            ''_Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice AS H WITH(NOLOCK) ON D.FTInvoiceNo = H.FTInvoiceNo where isnull( H.FTStateApprove,'') = '1' ) "
            '_Cmd &= vbCrLf & " ORder by R.FTInvoiceNo  ASC  ,P.FTPORef ASC "
            'Dim _dt As New System.Data.DataTable
            '_dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            'Try
            '    Me.FTHarmonizedCode.Text = _dt.Rows(0).Item("FTHTSData").ToString
            'Catch ex As Exception
            '    Me.FTHarmonizedCode.Text = ""
            'End Try

            _Cmd = "SELECT   D.FTInvoiceNo, D.FTInvoiceRefNo ,FTBookingNo "
            _Cmd &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. TEXPTCMInvoice_Ref AS D WITH(NOLOCK)   "
            _Cmd &= vbCrLf & " where  D.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _Cmd &= vbCrLf & " ORDER BY D.FTInvoiceRefNo  ASC "
            Me.ogcBookRef.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            Call LoadChkMainMatDesc()

        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveDataDetail() As Boolean
        Try
            Dim _Cmd As String = "" : Dim _oDt As System.Data.DataTable
            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D  "
            _Cmd &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Cmd = ""
            Dim _StrFileH As String = "FTInvoiceNo|FTPORef|FNHSysStyleId|FNCTNS|FNTNW|FNTGW|FNQuantity|FNUnitPrice|FNTotalAmount|FTNikePOLineItem|FTColorway|FTSizeBreakDown|FNHSysCartonId|FTRangeNo|FTPORefNo|FTLineNo"
            Dim _CmdIns As String = "" : Dim _CmdUpd As String = "" : Dim _Value As String = "" : Dim _Where As String = "" : Dim _ValueUpd As String = ""
            Dim _PKey As String = "FTInvoiceNo"
            Dim _FKey As String = "FTPORef"
            Dim _FKey2 As String = "FNHSysStyleId"
            Dim _FKey3 As String = "FNUnitPrice"
            Dim _FKey4 As String = "FTNikePOLineItem"
            Dim _FKey5 As String = "FTColorway"
            Dim _FKey6 As String = "FTSizeBreakDown"
            Dim _FKey7 As String = "FNHSysCartonId"


            'Dim _FKey8 As String = "FTInvoiceRefNo"
            Dim _FKey9 As String = "FTRangeNo"
            Dim _FKey10 As String = "FTPORefNo"
            Dim _FKey11 As String = "FTLineNo"




            _CmdIns = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D  "
            _CmdIns &= vbCrLf & "  (FTInsUser, FDInsDate, FTInsTime,  FTInvoiceNo, FTPORef, FNHSysStyleId, FNCTNS, FNTNW, FNTGW, FNQuantity, FNUnitPrice, FNTotalAmount, FTNikePOLineItem, FTColorway, FTSizeBreakDown,FNHSysCartonId,FTRangeNo,FTPORefNo , FTLineNo  )"
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
                    ElseIf _FKey7 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey7 & " = '" & Integer.Parse("0" & R.Item(_Str.ToString)) & "'"
                        'ElseIf _FKey8 = _Str Then
                        '    _Where &= vbCrLf & "  AND " & _FKey8 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey9 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey9 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey10 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey10 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey11 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey11 & " = '" & R.Item(_Str.ToString) & "'"
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

            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count > 0 Then
                    Me.FNAmt.Value = Double.Parse("0" & .Compute("SUM(FNTotalAmount)", "FNTotalAmount >=0").ToString)
                End If
            End With

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
        Try
            If otbdetail.SelectedTabPage.Name = "XtraTabPage1" Then
                With ogvdetail
                    If CheckOwner() = False Then Exit Sub
                    If ChkPost() Then
                        HI.MG.ShowMsg.mInfo("เอกสารอนุมัติไปแล้ว ไม่สามารถแก้ไขรายการได้ !!!!!!", 1809191406, Me.Text)
                        Exit Sub
                    End If

                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub



                    '  Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
                    Dim _PORef As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPORef").ToString
                    Dim _StyleId As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysStyleId").ToString)
                    Dim _Unitprice As Double = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNUnitPrice").ToString)
                    Dim _POLineItem As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTNikePOLineItem").ToString
                    Dim _Colorway As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString
                    Dim _Size As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString
                    Dim _CartonId As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysCartonId").ToString)
                    Dim _RangeNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTRangeNo").ToString
                    Dim _PORefNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPORefNo").ToString
                    Dim _LineNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTLineNo").ToString
                    Dim _Qty As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                    Dim _Pck As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCTNS").ToString)
                    Dim _Str As String = ""
                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        '_Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice_D "
                        '_Str &= vbCrLf & " where FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        '_Str &= vbCrLf & " and FTPORef='" & HI.UL.ULF.rpQuoted(_PORef) & "'"
                        '_Str &= vbCrLf & " and FNHSysStyleId=" & _StyleId
                        ''_Str &= vbCrLf & " and FNUnitPrice=" & _Unitprice
                        '_Str &= vbCrLf & " and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                        '_Str &= vbCrLf & " and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_Size) & "'"
                        '_Str &= vbCrLf & " and FNHSysCartonId=" & _CartonId
                        '_Str &= vbCrLf & " and FTRangeNo='" & HI.UL.ULF.rpQuoted(_RangeNo) & "'"
                        '_Str &= vbCrLf & " and FTPORefNo='" & HI.UL.ULF.rpQuoted(_PORefNo) & "'"
                        '_Str &= vbCrLf & " and FTLineNo='" & HI.UL.ULF.rpQuoted(_LineNo) & "'"


                        _Str = "Delete D From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice_D AS D  "
                        _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref AS R WITH(NOLOCK) ON  D.FTInvoiceNo = R.FTInvoiceRefNo "
                        _Str &= vbCrLf & " where R.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        _Str &= vbCrLf & " and D.FTPORef='" & HI.UL.ULF.rpQuoted(_PORef) & "'"
                        _Str &= vbCrLf & " and D.FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(_POLineItem) & "'"
                        _Str &= vbCrLf & " and D.FTRangeNo ='" & HI.UL.ULF.rpQuoted(_RangeNo) & "'"
                        _Str &= vbCrLf & " and D.FNHSysStyleId=" & _StyleId

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Exit Sub
                        End If

                        _Str = "Update TOP (1)  D "
                        _Str &= vbCrLf & " Set  D.FNQuantity = D.FNQuantity-" & _Qty
                        _Str &= vbCrLf & " ,  D.FNPackcount = D.FNPackcount-" & _Pck
                        '_Str &= vbCrLf & " ,  D.FNNet = D.FNNet-" & _Pck
                        '_Str &= vbCrLf & " ,  D.FNTotalNet = D.FNTotalNet-" & _Pck
                        '_Str &= vbCrLf & " ,  D.FNGrossWeight = D.FNGrossWeight-" & _Pck

                        _Str &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice AS D  "
                        _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref AS R WITH(NOLOCK) ON  D.FTInvoiceNo = R.FTInvoiceRefNo "
                        _Str &= vbCrLf & " where R.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
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
                    'LoadData()

                    .DeleteRow(.FocusedRowHandle)

                End With

            ElseIf otbdetail.SelectedTabPage.Name = "XtraTabPage3" Then
                With ogvPOLine
                    If CheckOwner() = False Then Exit Sub
                    If ChkPost() Then
                        HI.MG.ShowMsg.mInfo("เอกสารอนุมัติไปแล้ว ไม่สามารถแก้ไขรายการได้ !!!!!!", 1809191406, Me.Text)
                        Exit Sub
                    End If

                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub



                    '  Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
                    Dim _PORef As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPORef").ToString
                    Dim _POLineItem As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTNikePOLineItem").ToString
                    Dim _Str As String = ""
                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction




                        _Str = "Delete D From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice_D AS D  "
                        _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref AS R WITH(NOLOCK) ON  D.FTInvoiceNo = R.FTInvoiceRefNo "
                        _Str &= vbCrLf & " where R.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        _Str &= vbCrLf & " and D.FTPORef='" & HI.UL.ULF.rpQuoted(_PORef) & "'"
                        _Str &= vbCrLf & " and D.FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(_POLineItem) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Exit Sub
                        End If

                        _Str = "Delete D From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D AS D  "
                        _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref AS R WITH(NOLOCK) ON  D.FTInvoiceNo = R.FTInvoiceRefNo "
                        _Str &= vbCrLf & " where R.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        _Str &= vbCrLf & " and D.FTPORef='" & HI.UL.ULF.rpQuoted(_PORef) & "'"
                        _Str &= vbCrLf & " and D.FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(_POLineItem) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Exit Sub
                        End If
                        Call LoadData()

                        'Dim _oDt As DataTable
                        'With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                        '    .AcceptChanges()
                        '    _oDt = .Copy
                        '    .AcceptChanges()
                        'End With
                        '_oDt.BeginInit()
                        'For Each R As DataRow In _oDt.Rows
                        '    If R!FTPORef.ToString = _PORef And R!FTNikePOLineItem.ToString = _POLineItem Then
                        '        _oDt.Rows.Remove(R)
                        '    End If
                        'Next
                        '_oDt.EndInit()
                        'Me.ogcdetail.DataSource = _oDt

                        '_Str = "Update TOP (1)  D "
                        '_Str &= vbCrLf & " Set  D.FNQuantity = D.FNQuantity-" & _Qty
                        '_Str &= vbCrLf & " ,  D.FNPackcount = D.FNPackcount-" & _Pck
                        ''_Str &= vbCrLf & " ,  D.FNNet = D.FNNet-" & _Pck
                        ''_Str &= vbCrLf & " ,  D.FNTotalNet = D.FNTotalNet-" & _Pck
                        ''_Str &= vbCrLf & " ,  D.FNGrossWeight = D.FNGrossWeight-" & _Pck

                        '_Str &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice AS D  "
                        '_Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref AS R WITH(NOLOCK) ON  D.FTInvoiceNo = R.FTInvoiceRefNo "
                        '_Str &= vbCrLf & " where R.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        'If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        '    HI.Conn.SQLConn.Tran.Rollback()
                        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        '    Exit Sub
                        'End If


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
                    'LoadData()

                    .DeleteRow(.FocusedRowHandle)

                End With

            Else
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบรายการ Invoice Booking ใช่หรือไม่ !!!!!!", 1809051531) = False Then
                    Exit Sub
                End If

                With ogvBookRef

                    If CheckOwner() = False Then Exit Sub
                    If ChkPost() Then
                        HI.MG.ShowMsg.mInfo("เอกสารอนุมัติไปแล้ว ไม่สามารถแก้ไขรายการได้ !!!!!!", 1809191406, Me.Text)
                        Exit Sub
                    End If

                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    Dim _invoiceBookRef As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTInvoiceRefNo").ToString
                    Dim _Spls As New HI.TL.SplashScreen("Delete Booking Ref., Please wait.")
                    Dim _Cmd As String = ""

                    _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D"
                    _Cmd &= vbCrLf & " where    FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                    _Cmd &= vbCrLf & " and  FTPORef+'|'+  FTNikePOLineItem +'|'+ FTRangeNo+'|'+ FTLineNo in  (SELECT    R.FTPORef +'|'+ R.FTNikePOLineItem +'|'+R.FTRangeNo+'|'+R.FTLineNo  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref AS D INNER JOIN "
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMBookigInvoice_D AS R ON D.FTInvoiceRefNo = R.FTInvoiceNo "
                    _Cmd &= vbCrLf & "   where   D.FTInvoiceNo ='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                    _Cmd &= vbCrLf & "      and  D.FTInvoiceRefNo ='" & HI.UL.ULF.rpQuoted(_invoiceBookRef) & "' )"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                    _Cmd = "Delete From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref "
                    _Cmd &= vbCrLf & " where   FTInvoiceRefNo='" & HI.UL.ULF.rpQuoted(_invoiceBookRef) & "'"
                    _Cmd &= vbCrLf & " and  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                    Dim _DocNew As String = GetDoc()
                    If _DocNew <> Me.FTInvoiceNo.Text Then
                        _Cmd = "Update   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice "
                        _Cmd &= vbCrLf & " Set FTInvoiceNo='" & _DocNew & "'"
                        _Cmd &= vbCrLf & " where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

                        _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D"
                        _Cmd &= vbCrLf & " Set FTInvoiceNo='" & _DocNew & "'"
                        _Cmd &= vbCrLf & " where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                        _Cmd = "Update    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Ref "
                        _Cmd &= vbCrLf & " Set FTInvoiceNo='" & _DocNew & "'"
                        _Cmd &= vbCrLf & " where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                        Dim _oDt As System.Data.DataTable
                        Dim _Amt As Double = 0
                        Dim FTTotalAmountENB As String = ""
                        Dim FTTotalAmountTHB As String = ""
                        Dim _VatAmt As Double = 0
                        Dim _GrandAmt As Double = 0

                        _Cmd = "   Select D.FTInvoiceNo, D.FTPORef, D.FNHSysStyleId, D.FNCTNS, D.FNTNW, D.FNTGW, D.FNQuantity, D.FNUnitPrice, D.FNTotalAmount   "
                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D As D  "
                        _Cmd &= vbCrLf & " where  D.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_DocNew) & "'"
                        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                        _Amt = Double.Parse(_oDt.Compute("sum(FNTotalAmount)", "FTPORef <> ''"))
                        _VatAmt = Format((_Amt * 0) / 100, HI.ST.Config.AmtFormat)
                        _GrandAmt = _Amt + _VatAmt
                        FTTotalAmountENB = HI.UL.ULF.Convert_Bath_EN(_GrandAmt)
                        FTTotalAmountTHB = HI.UL.ULF.Convert_Bath_TH(_GrandAmt)


                        _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice "
                        _Cmd &= vbCrLf & " set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Cmd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & " , FNAmt=" & _Amt
                        _Cmd &= vbCrLf & " , FNDisCountPer=0"
                        _Cmd &= vbCrLf & " , FNDisCountAmt=0"
                        _Cmd &= vbCrLf & " , FNNetAmt=" & _Amt
                        _Cmd &= vbCrLf & " , FNVatPer=0"
                        _Cmd &= vbCrLf & " , FNVatAmt=" & _VatAmt
                        _Cmd &= vbCrLf & " , FNSurcharge=0"
                        _Cmd &= vbCrLf & " , FNTotalAmount=" & _GrandAmt
                        _Cmd &= vbCrLf & " ,   FTTotalAmountTHB='" & HI.UL.ULF.rpQuoted(FTTotalAmountTHB) & "' "
                        _Cmd &= vbCrLf & " ,   FTTotalAmountENB='" & HI.UL.ULF.rpQuoted(FTTotalAmountENB) & "' "
                        _Cmd &= vbCrLf & " where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_DocNew) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                        Me.FTInvoiceNo.Text = _DocNew
                    End If

                    ' Call LoadData()
                    _Spls.Close()

                End With
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Function GetDoc() As String
        Try
            Dim _PreInvoice As String = "" : Dim _PreInvoiceGrp As String = ""
            Dim _Pass As Boolean = False
            Dim _Cmd As String = "" : Dim _DocInvoce As String = ""
            Dim _StateFormatDoc As Boolean = True
            Dim _DocRefDocGrpNo As String = "" : Dim _FTStateDocRefSort As Boolean = False
            Dim _ProcFormat As Boolean = False


            '_Cmd = " SELECT  '1' AS FTSelect ,    FTPckPlanNo,P.FTPORef, FNQuantity, FNPackcount, FNNet, FNTotalNet, FNGrossWeight, P.FNHSysUnitId, FNVol, FTVolUnit, isnull( FTApproveState ,'0') as FTApproveState , FTApproveBy,  FDApproveDate "
            '_Cmd &= vbCrLf & " , convert(nvarchar(10), convert(datetime ,P.FDShipDate),103)  as  FDShipDate  ,  isnull(FNHSysMainMatSpecId,0) as  FNHSysMainMatSpecId  , V.FNHSysProvinceId as  FNHSysProvinceId_Hide  "
            '_Cmd &= vbCrLf & " ,     V.FTMainMatSpecEN as FTMainMatSpec   "
            '_Cmd &= vbCrLf & " ,  (Select Top 1 isnull(FTProvinceCode,'') as FTProvinceCode  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince  WITH (NOLOCK)  Where FNHSysProvinceId = V.FNHSysProvinceId )  as FNHSysProvinceId  "
            '_Cmd &= vbCrLf & " ,  V.FTPlantCode , V.FTCmpCode , V.FNHSysCmpId , V.FTAddressInvoice ,  V.FNHSysCustId , V.FTCustCode , V.FTCustNameEN   , V.FNHSysShipModeId, V.FTShipModeCode  , V.FNHSysCountryId "
            '_Cmd &= vbCrLf & " ,R.FTInvoiceNo , R.FTInvoiceNoPre  , '' as  FTHTSData , '' as  FTCatData , CT.FTCustExpProductDesc , V.FTStyleCode  , V.FTProvinceNameEN as FTProvinceName, V.FTCountryNameEN as FTCountryName , V.FTCountry "
            ''_Cmd &= vbCrLf & "  , case when isnull(CC.FNWidth,0)  = 0 then '0' else   "
            ''_Cmd &= vbCrLf & "  convert(nvarchar(30),  convert(numeric(18,2),(isnull(CC.FNWidth,0) /10))) + ' x ' +  convert(nvarchar(30), convert(numeric(18,2) , ( CC.FNLength/10)) ) +' x '+  convert(nvarchar(30),  convert(numeric(18,2),(CC.FNWeight * 10))) end AS FNCNT  "
            '_Cmd &= vbCrLf & "  ,  isnull (  (Select Top 1   R.FTSuplCode   "
            '_Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.V_TMERTOrder_Info AS O     "
            '_Cmd &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK)  ON O.FTOrderNo = S.FTOrderNo  "
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS PP WITH(NOLOCK) ON S.FNHSysContinentId = PP.FNHSysContinentId    "
            '_Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS R WITH(NOLOCK) ON PP.FNHSysSuplId = R.FNHSysSuplId"
            '_Cmd &= vbCrLf & "    where  case when ISNULL(S.FTPORef,'') = '' then O.FTPORef else S.FTPORef end = P.FTPORef )   "
            '_Cmd &= vbCrLf & "    , '') AS FTSuplCode  "

            '_Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan As P With(NOLOCK)  "
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN  ( Select  distinct FTMainMatSpecEN , FNHSysCustId ,FTPORef , FNHSysProvinceId ,FTPlantCode , FTCmpCode , FNHSysCmpId ,FTAddressInvoice  , FTCustCode  ,FTCustNameEN ,FTCountryNameEN "
            '_Cmd &= vbCrLf & " ,FTProvinceNameEN , FTCountry ,FTShipModeCode ,FTStyleCode  ,FNHSysCountryId  ,FNHSysShipModeId  ,FNHSysGenderId From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. V_ExportInvoice_H  ) As  V On P.FTPORef = V.FTPORef   "
            '_Cmd &= vbCrLf & " LEFT OUTER JOIN  (  Select Distinct  H.FTInvoiceNo, H.FTInvoiceRefNo, D.FTPORef , isnull(R.FTInvoiceNo,'') AS FTInvoiceNoPre   "
            '_Cmd &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) LEFT OUTER  JOIN  "
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice_D AS D WITH (NOLOCK) ON H.FTInvoiceNo = D.FTInvoiceNo "
            '_Cmd &= vbCrLf & "     LEFT OUTER JOIN (	  Select   FTInvoiceNo,FTInvoiceRefNo  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_Ref WITH(NOLOCK)   "
            '_Cmd &= vbCrLf & "  ) as R ON H.FTInvoiceNo  = R.FTInvoiceRefNo  ) AS R  ON   P.FTPORef  = R.FTPORef"
            ''_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..V_TCNMCarton AS CC ON R.FNHSysCartonId = CC.FNHSysCartonId"

            ''_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMHTSData AS C WITH(NOLOCK) ON V.FNHSysProdTypeId = C.FNHSysProdTypeId and V.FNHSysGenderId = C.FNHSysGenderId "
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS CT WITH(NOLOCK) ON V.FNHSysCustId= CT.FNHSysCustId "

            '_Cmd &= vbCrLf & " where  isnull( P.FTApproveState ,'0')  = '1'  and R.FTInvoiceNoPre='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            '_Cmd &= vbCrLf & " and P.FTPORef not in (Select distinct FTPORef  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_D as D WITH(NOLOCK)   "
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice AS H WITH(NOLOCK) ON D.FTInvoiceNo = H.FTInvoiceNo where isnull( H.FTStateApprove,'') = '1' ) "
            '_Cmd &= vbCrLf & " ORder by R.FTInvoiceNo  ASC  ,P.FTPORef ASC "
            Dim _dt As New System.Data.DataTable
            '_dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "Select '1' AS FTSelect ,  FTInvoiceNo as FTInvoiceGrp,FTInvoiceRefNo as FTInvoiceNo  From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_Ref WITH(NOLOCK)   "
            _Cmd &= vbCrLf & " where FTInvoiceNo ='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            With _dt


                For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                    If Not (_DocRefDocGrpNo = Microsoft.VisualBasic.Left(R!FTInvoiceNo.ToString, Microsoft.VisualBasic.Len(R!FTInvoiceNo.ToString) - 1)) And Not (_DocRefDocGrpNo = "") Then
                        _StateFormatDoc = False
                    Else
                        If Not (_DocRefDocGrpNo = "") Then
                            _FTStateDocRefSort = True
                        End If
                    End If
                    _Cmd = "SELECT Top 1  FTInvoiceRefNo  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) where FTInvoiceNo = '" & R!FTInvoiceNo.ToString & "'"
                    _DocRefDocGrpNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")


                Next



                If _StateFormatDoc Then
                    For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                        _PreInvoice &= Microsoft.VisualBasic.Right(R!FTInvoiceNo.ToString, 1)
                        If Not _Pass Then
                            _Cmd = "SELECT Top 1  FTInvoiceRefNo  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) where FTInvoiceNo = '" & R!FTInvoiceNo.ToString & "'"
                            _DocInvoce = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")
                            _Pass = True
                        End If
                    Next
                    _PreInvoiceGrp = _DocInvoce & _PreInvoice
                Else
                    _DocRefDocGrpNo = ""
                    _PreInvoice = ""
                    Dim _RCount As Integer = 1 : _DocInvoce = ""
                    For Each R As DataRow In .Select("FTSelect ='1' and FTInvoiceNo <> ''", "FTInvoiceNo  ASC ")
                        _Cmd = "SELECT Top 1  FTInvoiceRefNo  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMBookigInvoice AS H WITH (NOLOCK) where FTInvoiceNo = '" & R!FTInvoiceNo.ToString & "'"
                        _DocInvoce = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "")
                        If (_FTStateDocRefSort) And (_DocRefDocGrpNo = "" Or _DocRefDocGrpNo = _DocInvoce) Then
                            _PreInvoice &= Microsoft.VisualBasic.Right(R!FTInvoiceNo.ToString, 1)
                            _ProcFormat = True
                        Else
                            _ProcFormat = False
                        End If

                        If _ProcFormat = False Then
                            If _RCount <= 2 Then
                                _PreInvoice = ""
                                _DocInvoce = R!FTInvoiceNo.ToString
                            End If
                            If _DocRefDocGrpNo <> "" Then
                                _DocInvoce = _DocRefDocGrpNo
                            End If
                            Exit For
                        End If

                        _DocRefDocGrpNo = _DocInvoce
                        _RCount += +1
                    Next
                    If _RCount = 1 Then
                        _PreInvoice = ""

                    End If

                    _PreInvoiceGrp = _DocInvoce & _PreInvoice

                End If
            End With
            Return _PreInvoiceGrp
        Catch ex As Exception
            Return Me.FTInvoiceNo.Text
        End Try
    End Function

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
                    'If Me.FTStateSendApp.Checked Then
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
                        Me.FTStateApp.Checked = True
                    Else
                        HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลผิดพลาด..  !!!!  ", 1802091338, Me.Text, "", MessageBoxIcon.Information)
                    End If

                    ' End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadChkMainMatDesc()
        Try
            Dim _Cmd As String = "" : Dim _oDt As System.Data.DataTable : Dim _FNHSysStyleId As Integer = 0
            Dim _oDtNew As New System.Data.DataTable : Dim _SeasonId As Integer = 0 : Dim _oDr As System.Data.DataRow
            Dim _FTStyleCode As String = "" : Dim _SeasonCode As String = "" : Dim _MainMatSpec As String = ""
            With _oDtNew
                .Columns.Add("FNHSysStyleId", GetType(Integer))
                .Columns.Add("FTStyleCode", GetType(String))
                .Columns.Add("FNHSysSeasonId", GetType(Integer))
                .Columns.Add("FTSeasonCode", GetType(String))
                .Columns.Add("FTMainMatSpecCode", GetType(String))
                .Columns.Add("FTMainMatSpecTH", GetType(String))
                .Columns.Add("FTMainMatSpecEN", GetType(String))
                .Columns.Add("FTNote", GetType(String))
            End With

            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            For Each R As DataRow In _oDt.Rows
                _FNHSysStyleId = Integer.Parse("0" & R!FNHSysStyleId.ToString)
                _FTStyleCode = HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString)
                '_Cmd = " Declare @FNHSysSeasonId int , @FTMainMatSpecCode nvarchar(30) , @FTSeasonCode nvarchar(30) "
                '_Cmd &= vbCrLf & "Select Top 1 @FNHSysSeasonId =FNHSysSeasonId"
                '_Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK) "
                '_Cmd &= vbCrLf & " Where FNHSysStyleId=" & Integer.Parse("0" & R!FNHSysStyleId.ToString)
                '_Cmd &= vbCrLf & " and FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Cmd = " Select Top 1  FTMainMatSpecCode  "
                _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMatSpec WITH(NOLOCK) "
                _Cmd &= vbCrLf & " where FNHSysStyleId=" & _FNHSysStyleId
                '_Cmd &= vbCrLf & " and FNHSysSeasonId=@FNHSysSeasonId " ' & _SeasonId
                '_Cmd &= vbCrLf & " Select Top 1  @FTSeasonCode = FTSeasonCode  "
                '_Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WITH(NOLOCK) "
                '_Cmd &= vbCrLf & " where  FNHSysSeasonId=@FNHSysSeasonId" ' & _SeasonId
                '_Cmd &= vbCrLf & " select @FNHSysSeasonId  as FNHSysSeasonId ,isnull(@FTMainMatSpecCode,'') as FTMainMatSpecCode   ,isnull( @FTSeasonCode,'') as FTSeasonCode  "

                Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                If _dt.Rows.Count > 0 Then
                    _SeasonId = 0 ' Integer.Parse("0" & _dt.Rows(0).Item("FNHSysSeasonId").ToString)
                    _SeasonCode = "" ' _dt.Rows(0).Item("FTSeasonCode").ToString
                    _MainMatSpec = _dt.Rows(0).Item("FTMainMatSpecCode").ToString
                End If

                If _MainMatSpec = "" Then
                    If _oDtNew.Select("FNHSysStyleId=" & _FNHSysStyleId).Length <= 0 Then
                        Dim _r As DataRow
                        _r = _oDtNew.NewRow()
                        _r("FNHSysStyleId") = _FNHSysStyleId
                        _r("FTStyleCode") = _FTStyleCode
                        _r("FNHSysSeasonId") = _SeasonId
                        _r("FTSeasonCode") = _SeasonCode
                        _r("FTMainMatSpecCode") = ""
                        _r("FTMainMatSpecTH") = ""
                        _r("FTMainMatSpecEN") = ""
                        _r("FTNote") = ""
                        _oDtNew.Rows.Add(_r)
                    End If
                End If

            Next
            Dim _oDn As System.Data.DataTable
            If _oDtNew.Rows.Count > 0 Then
                With _Mainmatadd
                    .ogcref.DataSource = _oDtNew
                    .ShowDialog()
                    If (.StateProc) Then
                        With DirectCast(.ogcref.DataSource, System.Data.DataTable)
                            .AcceptChanges()
                            _oDn = .Copy
                        End With

                        For Each R As DataRow In _oDn.Rows
                            If HI.UL.ULF.rpQuoted(R!FTMainMatSpecTH.ToString) <> "" And HI.UL.ULF.rpQuoted(R!FTMainMatSpecEN.ToString) <> "" Then
                                Dim _FNHSysMainMatSpecId As Integer = HI.SE.RunID.GetRunNoID("TMERMMainMatSpec", "FNHSysMainMatSpecId", Conn.DB.DataBaseName.DB_MASTER)
                                _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMatSpec "
                                _Cmd &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime ,FNHSysMainMatSpecId, FTMainMatSpecCode, FNHSysStyleId, FNHSysSeasonId, FTMainMatSpecTH, FTMainMatSpecEN, FTStateActive, FTNote)"
                                _Cmd &= vbCrLf & "Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                _Cmd &= vbCrLf & "," & _FNHSysMainMatSpecId
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatSpecCode.ToString) & "'"
                                _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNHSysStyleId.ToString)
                                _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNHSysSeasonId.ToString)
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatSpecTH.ToString) & "'"
                                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatSpecEN.ToString) & "'"
                                _Cmd &= vbCrLf & ",'1','" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
                            End If
                        Next
                    End If
                End With
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function ChkPost() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT   Top 1 isnull( P.FTInvoiceNo,'') as FTInvoiceNo"
            _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].TEXPTCMInvoice_Ref AS I WITH(NOLOCK)  LEFT OUTER JOIN  "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].TEXPTCMInvoice_Post AS P WITH(NOLOCK) ON I.FTInvoiceRefNo = P.FTInvoiceNo"
            _Cmd &= vbCrLf & "where I.FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> ""
        Catch ex As Exception

        End Try
    End Function

    Private Sub ocmpost_Click(sender As Object, e As EventArgs) Handles ocmpost.Click
        Try
            If VerrifyData() Then
                If ChkPost() Then
                    MG.ShowMsg.mInfo("กรุณาตรวจสอบเอกสารมีการ post invoice แล้ว !!!!!! ", 1807210848, Me.Text, "", MessageBoxIcon.Information)
                    Exit Sub
                End If
                Try
                    With DirectCast(Me.ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        If (.Select("FNNetPriceImport=0", "FNNetPriceImport asc").Length > 0) Then
                            MG.ShowMsg.mInfo("ไม่สามารถ  post invoice ได้ กรุณาตรวจสอบราคาที่ถูกต้อง.. !!!!!! ", 1907111130, Me.Text, "", MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End With
                Catch ex As Exception

                End Try
                If SaveData() Then
                    'If Me.FTStateSendApp.Checked Then
                    'Dim _Str As String = ""
                    '_Str = ""
                    '_Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TEXPTCMInvoice] "
                    '_Str &= Environment.NewLine & "SET  [FTStateApprove] = '1'"
                    '_Str &= Environment.NewLine & ", [FTApproveBy] = '" & HI.ST.UserInfo.UserName & "'"
                    '_Str &= Environment.NewLine & ", [FDApproveDate] = " & HI.UL.ULDate.FormatDateDB
                    '_Str &= Environment.NewLine & ", [FTApproveTime] = " & HI.UL.ULDate.FormatTimeDB
                    '_Str &= Environment.NewLine & " WHERE FTInvoiceNo = '" & FTInvoiceNo.Text & "'"
                    'If HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_ACCOUNT) Then
                    '    HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลเรียบร้อยแล้ว..  !!!!  ", 1802091337, Me.Text, "", MessageBoxIcon.Information)
                    '    Me.FTStateApp.Checked = True
                    'Else
                    '    HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลผิดพลาด..  !!!!  ", 1802091338, Me.Text, "", MessageBoxIcon.Information)
                    'End If

                    ' Else
                    '  Exit Sub
                    ' End If
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            HI.TL.HandlerControl.ClearControl(_InvoicePost)
            If Me.FTInvoiceNo.Text <> "" Then
                Dim _Value As String = Nothing
                Dim myList As New ArrayList
                Dim _InvoiceNoPost As String = ""
                Dim _odr As System.Data.DataTable
                With DirectCast(Me.ogcBookRef.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Rows
                        _InvoiceNoPost = R!FTInvoiceRefNo.ToString
                        If _InvoiceNoPost <> "" Then
                            Exit For
                        End If
                    Next
                End With

                Dim T As System.Type = _InvoicePost.GetType()

                Dim _Cmd As String = ""
                _Cmd = "Select Top 1  FTInvoiceNo  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTCMInvoice_Post with(nolock) where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "" Then
                    _Cmd = "Exec   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_EXPORT_POSTINVOICE '" & Me.FTInvoiceNo.Text & "','" & HI.ST.UserInfo.UserName & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                End If

                ' myList.Add(_InvoiceNoPost)
                'Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                'HI.ST.SysInfo.MenuName = "MnuExportInvPost"
                'Call CallByName(Me.Parent.Parent, "CallWindowForm", CallType.Method, {"MnuExportInvPost", "LoadInvoiceInfo", myList.ToArray(GetType(String))})
                '_InvoicePost.LoadInvoiceInfo(_InvoiceNoPost)
                'HI.ST.SysInfo.MenuName = _TmpMenu

                HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลเรียบร้อยแล้ว..  !!!!  ", 1802091337, Me.Text, "", MessageBoxIcon.Information)
                Me.FTStateApp.Checked = True

            End If





        Catch ex As Exception

        End Try
    End Sub


    Dim _pack As Double = 0
    Dim _netweigth As Double = 0
    Dim _grossweight As Double = 0
    Dim __nWeight As Double = 0
    Dim _rowCount As Integer = 0
    Dim _Instruction As String = ""
    Dim _PackCount As Integer = 0
    Dim _pQty As Integer = 0
    Dim _pCTN As Integer = 0
    Dim _pAmt As Double = 0
    Private Sub ogvdetail_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles ogvdetail.CustomSummaryCalculate
        Try

            Try
                Dim view As New GridView()
                view = DirectCast(sender, GridView)

                Dim _RangNo As String = view.GetRowCellValue(e.RowHandle, "FTRangeNo")
                Dim _RangNoOld As String = ""
                If e.RowHandle > 0 Then
                    _RangNoOld = view.GetRowCellValue(e.RowHandle - 1, "FTRangeNo")
                End If

                Dim _LineNo As String = view.GetRowCellValue(e.RowHandle, "FTLineNo")
                Dim _LineNoOld As String = ""
                If e.RowHandle > 0 Then
                    _LineNoOld = view.GetRowCellValue(e.RowHandle - 1, "FTLineNo")
                End If

                Dim _PORefNo As String = view.GetRowCellValue(e.RowHandle, "FTPORef")
                Dim _PORefNoOld As String = ""
                If e.RowHandle > 0 Then
                    _PORefNoOld = view.GetRowCellValue(e.RowHandle - 1, "FTPORef")
                End If

                Dim _POlineItem As String = view.GetRowCellValue(e.RowHandle, "FTNikePOLineItem")
                Dim _POlineItemOld As String = ""
                If e.RowHandle > 0 Then
                    _POlineItemOld = view.GetRowCellValue(e.RowHandle - 1, "FTNikePOLineItem")
                End If


                Dim _PORefPlanNo As String = view.GetRowCellValue(e.RowHandle, "FTPORefNo")
                Dim _PORefPlanNoOld As String = ""
                If e.RowHandle > 0 Then
                    _PORefPlanNoOld = view.GetRowCellValue(e.RowHandle - 1, "FTPORefNo")
                End If


                Dim _Qty As Integer = Integer.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNQuantity"))
                Dim _CTNS As Integer = Integer.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNCTNS"))
                Dim _Amt As Double = Double.Parse("0" & view.GetRowCellValue(e.RowHandle, "FNTotalAmount"))

                Select Case e.SummaryProcess
                    Case CustomSummaryProcess.Start
                        _pack = 0 : _netweigth = 0 : _grossweight = 0 : _rowCount = 0 : _PackCount = 0
                        _Instruction = ""
                        _pQty = 0 : _pCTN = 0 : _pAmt = 0
                    Case CustomSummaryProcess.Calculate
                        If _RangNo = "" And _LineNo = "" And _POlineItem = "" Then
                            _RangNo = _RangNoOld
                            _LineNo = _LineNoOld
                            _PORefNo = _PORefNoOld
                            _POlineItem = _POlineItemOld
                            If _RangNo <> "" And _LineNo <> "" And _POlineItem <> "" And _PORefPlanNo <> "" Then
                                _rowCount += +1
                            End If
                        Else
                            If _RangNo = _RangNoOld And _LineNo <> _LineNoOld And _POlineItem <> _POlineItemOld And _PORefPlanNo <> _PORefPlanNoOld Then
                                _rowCount += +1
                            Else
                                _rowCount = 1
                            End If
                        End If

                        Select Case DirectCast(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString.ToUpper
                            Case "FNQuantity".ToUpper
                                _pQty += +_Qty
                            Case "FNCTNS".ToUpper
                                If Not (_RangNo = _RangNoOld And _PORefNo = _PORefNoOld And _PORefPlanNo = _PORefPlanNoOld) Then 'And _POlineItem = _POlineItemOld
                                    _pCTN += +_CTNS
                                End If
                            Case "FNTotalAmount".ToUpper
                                _pAmt += +_Amt
                        End Select
                        _RangNo = _RangNoOld : _LineNo = _LineNoOld : _PORefNo = _PORefNoOld : _POlineItem = _POlineItemOld : _PORefPlanNo = _PORefPlanNoOld
                    Case CustomSummaryProcess.Finalize
                        Select Case DirectCast(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString.ToUpper
                            Case "FNQuantity".ToUpper
                                e.TotalValue = _pQty
                            Case "FNCTNS".ToUpper
                                e.TotalValue = _pCTN
                            Case "FNTotalAmount".ToUpper
                                e.TotalValue = _pAmt
                        End Select
                End Select
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNNetPriceImport"))
                If category = 0 Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            If Me.FTStateApp.Checked Then
                HI.MG.ShowMsg.mInfo("มีการอนุมัติเอกสาร ไปแล้ว กรุณาตรวจสอบ..  !!!!  ", 1907111142, Me.Text, "", MessageBoxIcon.Information)
                Exit Sub
            End If
            If Me.FTRemark.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTRemark_lbl.Text)
                Exit Sub
            End If
            Dim _Str As String = ""
            _Str = ""
            _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TEXPTCMInvoice] "
            _Str &= Environment.NewLine & "SET  [FTStateApprove] = '2'"
            _Str &= Environment.NewLine & ", [FTApproveBy] = '" & HI.ST.UserInfo.UserName & "'"
            _Str &= Environment.NewLine & ", [FDApproveDate] = " & HI.UL.ULDate.FormatDateDB
            _Str &= Environment.NewLine & ", [FTApproveTime] = " & HI.UL.ULDate.FormatTimeDB
            _Str &= Environment.NewLine & " WHERE FTInvoiceNo = '" & FTInvoiceNo.Text & "'"

            If HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_ACCOUNT) Then
                HI.MG.ShowMsg.mInfo("ยกเลิกอนุมัติข้อมูลเรียบร้อยแล้ว..  !!!!  ", 1907111140, Me.Text, "", MessageBoxIcon.Information)
                Me.FTStateCalcel.Checked = True
            Else
                HI.MG.ShowMsg.mInfo("ยกเลิกอนุมัติข้อมูลผิดพลาด..  !!!!  ", 1907111141, Me.Text, "", MessageBoxIcon.Information)
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class
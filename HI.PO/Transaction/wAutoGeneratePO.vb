Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base

Public Class wAutoGeneratePO

    Private _lstPo As wListAutoPurchaseOrderNo
    Private _ListPurchaseOrder As New List(Of DataTable)
    Private _ListPurchaseOrderSupl As New List(Of DataTable)
    Private _wChangeUnitAndCurrency As wAutoGeneratePOChangeUnit
    Private _PORunDocVat As Boolean = False
    Sub New(Optional ListJobOrderNo As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim _Spls As New HI.TL.SplashScreen("Prepare Data For Auto Purchase Order Please Wait...")

        HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(Me)
        _lstPo = New wListAutoPurchaseOrderNo

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _lstPo.Name.ToString.Trim, _lstPo)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_lstPo)
        HI.TL.HandlerControl.AddHandlerObj(_lstPo)

        _wChangeUnitAndCurrency = New wAutoGeneratePOChangeUnit

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wChangeUnitAndCurrency.Name.ToString.Trim, _wChangeUnitAndCurrency)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_wChangeUnitAndCurrency)
        HI.TL.HandlerControl.AddHandlerObj(_wChangeUnitAndCurrency)


        With RepFNCreditDay
            AddHandler .EditValueChanged, AddressOf CalcEdit_EditValueChanged
        End With

        With RepFNDisCountPer
            AddHandler .EditValueChanged, AddressOf CalcEdit_EditValueChanged
        End With

        With RepFNExchangeRate
            AddHandler .EditValueChanged, AddressOf CalcEditExc_EditValueChanged
        End With

        With RepFNSurcharge
            AddHandler .EditValueChanged, AddressOf CalcEdit_EditValueChanged
        End With

        With Me.RepFNPoState
            AddHandler .SelectedIndexChanged, AddressOf RepositoryPoState_SelectedIndexChanged
        End With

        With RepFNLeadtime
            AddHandler .EditValueChanged, AddressOf RepositoryItemLeadTime_EditValueChanged
        End With

        With RepFDDeliveryDate
            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            ' AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave
        End With

        '------Start Add Summary Grid-------------
        With ogvlistorder
            .Columns("FTOrderNo").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTOrderNo")
            .Columns("FTOrderNo").SummaryItem.DisplayFormat = "{0:n0}"
            .OptionsView.ShowFooter = True
        End With

        Call InitGridDetail()
        '------End Add Summary Grid-------------

        Call PrepareDataGenerate(ListJobOrderNo)

        If GENAUTOPO.Properties.Items.Count > 0 Then
            GENAUTOPO.SelectedIndex = 0
        End If

        Dim cmdstring As String = "select top 1 FTCfgData from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X With(Nolock) where FTCfgName ='CVNPORunVat'"
        _PORunDocVat = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "") = "Y")

        _Spls.Close()
    End Sub


    Private _ReplacePO As Boolean = False
    Public Property ReplacePO As Boolean
        Get
            Return _ReplacePO
        End Get
        Set(value As Boolean)
            _ReplacePO = value
        End Set
    End Property


#Region "Init Grid"
    Private Sub InitGridDetail()
        '------Start Add Summary Grid-------------
        With ogvdetail
            .Columns("FTRawMatCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTOrderNo")
            .Columns("FTRawMatCode").SummaryItem.DisplayFormat = "{0:n0}"

            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"

            .Columns("FNNetAmt").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNNetAmt")
            .Columns("FNNetAmt").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"

            .OptionsView.ShowFooter = True
        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region
#Region "Handler"
    Private Shared Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String
                Try
                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)
                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                    End If
                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate

                Try
                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                Catch ex As Exception

                End Try
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

                If _TDate <> "" Then
                    .SetRowCellValue(.FocusedRowHandle, "FNLeadtime", Val(HI.Conn.SQLConn.GetField("SELECT DATEDIFF(Day,GetDate(),'" & _TDate & "')", Conn.DB.DataBaseName.DB_PUR, "0")))
                Else
                    .SetRowCellValue(.FocusedRowHandle, "FNLeadtime", 0)
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub CalcEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If sender.value < 0 Then
                sender.value = 0
            End If

        End With
    End Sub

    Private Sub CalcEditExc_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If sender.value < 1 Then
                sender.value = 1
            End If

        End With
    End Sub

    Private Shared Sub RepositoryItemLeadTime_EditValueChanged(sender As System.Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                .SetRowCellValue(.FocusedRowHandle, "FDDeliveryDate", HI.Conn.SQLConn.GetField("SELECT Convert(varchar(10),DATEADD(DAY," & CType(sender, DevExpress.XtraEditors.CalcEdit).Value & ",GETDATE() ),103)", Conn.DB.DataBaseName.DB_PUR, ""))
            End With

        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub RepositoryPoState_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            .SetFocusedRowCellValue("FNPoState_Hide", CType(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex)
        End With
    End Sub

#End Region

#Region " VARIABLE "

    Private _DtSupl As DataTable
    Private _DtJob As DataTable
    Private _DtItem As DataTable
    Private _DtItemSub As DataTable

    Private _TmpPO As DataTable
    Private _RunDate As String

#End Region

#Region "Query"

    Private Function QueryJOb() As String
        Dim _Str As String = ""

        _Str = "  SELECT '0' AS FTStateSelect "
        _Str &= vbCrLf & " ,A.FTOrderNo"
        _Str &= vbCrLf & " ,C.FTStyleCode "
        _Str &= vbCrLf & " ,S.FTSeasonCode "
        _Str &= vbCrLf & " ,ISNULL(Cmp.FTCmpCode,'') AS FTCmpCode,B.FTOrderNoAutoPO "
        _Str &= vbCrLf & "    FROM "
        _Str &= vbCrLf & "  (SELECT        FTOrderNo"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH(NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  WHERE        (FTPurchaseNo = N'')   AND ISNULL(FNHSysUnitIdPurchase,0) <> 0  AND FNTotalPurchaseQuantity>0 AND  ISNULL(FNHSysUnitId,0) <> 0 "

        If Not (HI.ST.SysInfo.Admin) Then

            _Str &= vbCrLf & "   AND FTOrderNo IN ( SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP WITH(NOLOCK) WHERE  FTOrderBy  IN (SELECT FTUserName FROM  dbo.FT_GetOrderPermission_PurchseTeam('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') ) )"

        End If

        _Str &= vbCrLf & "  GROUP BY FTOrderNo"
        _Str &= vbCrLf & "  ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP AS B  WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo "
        _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS C WITH(NOLOCK)"
        _Str &= vbCrLf & "   ON  B.FNHSysStyleId =C.FNHSysStyleId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK) ON B.FNHSysSeasonId=S.FNHSysSeasonId "
        _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON B.FNHSysCmpId=Cmp.FNHSysCmpId "
        _Str &= vbCrLf & "  WHERE  ISNULL(B.FNHSysStyleIdPull,0) <=0 AND ISNULL(FNJobState,0) IN (0,1) "
        _Str &= vbCrLf & "  ORDER BY A.FTOrderNo  "

        Return _Str

    End Function

    Private Function QuerySupplier() As String
        Dim _Str As String = ""

        '----- Supplier
        _Str = " 	"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "  CREATE TABLE  #TAB (FTOrderNo nvarchar(50)) 	"
            _Str &= vbCrLf & "  INSERT INTO #TAB(FTOrderNo)	 SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP WITH(NOLOCK) WHERE   ISNULL(FNHSysStyleIdPull,0) <=0  AND  FTOrderBy  IN (SELECT FTUserName FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FT_GetOrderPermission_PurchseTeam('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') ) "
        End If

        _Str &= vbCrLf & " 	SELECT      '1' AS FTSelect"
        _Str &= vbCrLf & " 	, A.FNHSysSuplId"
        _Str &= vbCrLf & " , S.FTSuplCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , S.FTSuplNameTH AS FTSuplName"
            _Str &= vbCrLf & " , PS.FTNameTH AS FNPoState"

        Else

            _Str &= vbCrLf & " , S.FTSuplNameEN AS  FTSuplName "
            _Str &= vbCrLf & " , PS.FTNameEN AS FNPoState"

        End If

        _Str &= vbCrLf & " , S.FNPoState AS FNPoState_Hide"
        _Str &= vbCrLf & " ,'' AS FNHSysPurGrpId"
        _Str &= vbCrLf & " ,0 AS FNHSysPurGrpId_Hide"
        _Str &= vbCrLf & " ,0 AS FNLeadtime"
        _Str &= vbCrLf & " ,'' AS FDDeliveryDate"
        _Str &= vbCrLf & " , CRT.FTCrTermCode AS FNHSysCrTermId"
        _Str &= vbCrLf & " , S.FNHSysCrTermId AS FNHSysCrTermId_Hide"
        _Str &= vbCrLf & " 	,ISNULL(S.FNCreditDay,0) AS FNCreditDay"
        _Str &= vbCrLf & " , PM.FTTermOfPMCode AS FNHSysTermOfPMId"
        _Str &= vbCrLf & " , S.FNHSysTermOfPMId AS FNHSysTermOfPMId_Hide"
        _Str &= vbCrLf & " , CR.FTCurCode AS FNHSysCurId"
        _Str &= vbCrLf & " , S.FNHSysCurId AS FNHSysCurId_Hide"
        _Str &= vbCrLf & " , (CASE WHEN CR.FTStateLocal ='1' THEN 1 ELSE"

        _Str &= vbCrLf & " ISNULL((SELECT TOP 1 FNBuyingRate"
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
        _Str &= vbCrLf & "   WHERE  (FDDate = Convert(nvarchar(10),GetDate(),111) )"
        _Str &= vbCrLf & "   AND (FNHSysCurId = S.FNHSysCurId) ),0) END ) AS  FNExchangeRate "

        _Str &= vbCrLf & " , S.FNTax AS FNVatPer"
        _Str &= vbCrLf & " 	,0.00 AS FNSurcharge"
        _Str &= vbCrLf & " ,ISNULL(S.FNPODiscount,0.00) AS FNDisCountPer"

        _Str &= vbCrLf & " ,ISNULL(("
        _Str &= vbCrLf & " SELECT      Count(DISTINCT S.FNHSysCurId) AS FNHSysCurId"
        _Str &= vbCrLf & "    FROM            HITECH_PURCHASE.dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "    WHERE        (S.FTPurchaseNo = N'') AND ISNULL(FNHSysUnitId,0) <> 0  AND FNTotalPurchaseQuantity>0  AND S.FNHSysSuplId = A.FNHSysSuplId"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & " ),0) AS FNStateCurP"

        _Str &= vbCrLf & " ,ISNULL(("
        _Str &= vbCrLf & " 	 SELECT Max( FNHSysUnitIdPurchase) AS FNHSysUnitIdPurchase"
        _Str &= vbCrLf & "  FROM ("
        _Str &= vbCrLf & "   SELECT    S.FNHSysRawMatId,Count(DISTINCT FNHSysUnitIdPurchase) AS FNHSysUnitIdPurchase"
        _Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE  (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0 "
        _Str &= vbCrLf & " 	 AND S.FNHSysSuplId = A.FNHSysSuplId"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "   GROUP BY FNHSysRawMatId) AS Z"
        _Str &= vbCrLf & "  ),0) AS FNStateUnitP"
        _Str &= vbCrLf & " ,ISNULL(("
        _Str &= vbCrLf & "  SELECT Max( FNPricePurchase) AS FNPricePurchase"
        _Str &= vbCrLf & "  FROM ("
        _Str &= vbCrLf & "   SELECT    S.FNHSysRawMatId,Count(DISTINCT FNPricePurchase) AS FNPricePurchase"
        _Str &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE  (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0   "
        _Str &= vbCrLf & " 	 AND S.FNHSysSuplId = A.FNHSysSuplId"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "   GROUP BY FNHSysRawMatId) AS Z"
        _Str &= vbCrLf & " ),0) AS FNStatePriceP"
        _Str &= vbCrLf & " ,'0' AS FTStateComplete"

        _Str &= vbCrLf & "  FROM        (SELECT        S.FNHSysSuplId,Max(S.FNHSysCurId) AS FNHSysCurId"
        _Str &= vbCrLf & "    FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "    WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0  "

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "   GROUP BY S.FNHSysSuplId) AS A INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"
        _Str &= vbCrLf & "   LEFT JOIN (SELECT         FTListName, FNListIndex, FTNameTH, FTNameEN, FTReferCode"
        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData"
        _Str &= vbCrLf & " WHERE        (FTListName = N'FNPoState')) AS PS ON S.FNPoState =PS.FNListIndex"
        _Str &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CR WITH(NOLOCK) ON S.FNHSysCurId =CR.FNHSysCurId"
        _Str &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCreditTerm AS CRT WITH(NOLOCK) ON S.FNHSysCrTermId =CRT.FNHSysCrTermId"
        _Str &= vbCrLf & "  LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMPaymentTerm AS PM WITH(NOLOCK) ON S.FNHSysTermOfPMId =PM.FNHSysTermOfPMId"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "  DROP TABLE #TAB 	"
        End If

        Return _Str
    End Function

    Private Function QueryPOItem()

        Dim _Str As String = ""

        _Str = " 	"

        If Not (HI.ST.SysInfo.Admin) Then

            _Str &= vbCrLf & "  CREATE TABLE  #TAB (FTOrderNo nvarchar(50)) 	"
            _Str &= vbCrLf & " INSERT INTO #TAB(FTOrderNo)	  SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP WITH(NOLOCK) WHERE   ISNULL(FNHSysStyleIdPull,0) <=0  AND FTOrderBy  IN (SELECT FTUserName FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FT_GetOrderPermission_PurchseTeam('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') ) "

        End If

        _Str &= vbCrLf & "   SELECT  TOP 0   MPO.FNHSysSuplId"
        _Str &= vbCrLf & ", MPO.FNHSysRawMatId"
        _Str &= vbCrLf & ", MPO.FNHSysUnitId"
        _Str &= vbCrLf & ", MPO.FTFabricFrontSize"
        _Str &= vbCrLf & ", Convert(numeric(18,4),MPO.FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & ", FNPrice"
        _Str &= vbCrLf & ", Convert(numeric(18,2),(Convert(numeric(18,4),MPO.FNQuantity) * MPO.FNPrice)) AS FNNetAmt"

        _Str &= vbCrLf & ", IM.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
        Else
            _Str &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
        End If

        _Str &= vbCrLf & " , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & " , U.FTUnitCode ,'1' AS FTSelect,FNPrice AS FNPriceOld"
        _Str &= vbCrLf & " ,0 AS FNStateCurP"
        _Str &= vbCrLf & " ,0 AS FNStateUnitP"
        _Str &= vbCrLf & " ,0 AS FNStatePriceP,0 AS FNStateSplit"
        _Str &= vbCrLf & "  FROM            (SELECT        FNHSysSuplId, FNHSysRawMatId, Convert(numeric(18,4),FLOOR(FNQuantity)) + (CASE WHEN FNQuantity - Convert(numeric(18,4),FLOOR(FNQuantity)) > 0 THEN 1.0000 ELSE 0.0000 END) AS FNQuantity, FNHSysUnitId, FNPrice, FTFabricFrontSize"
        _Str &= vbCrLf & "     FROM            (SELECT        M.FNHSysSuplId, SC.FNHSysRawMatId, SUM(SC.FNTotalPurchaseQuantity) AS FNQuantity, SC.FNHSysUnitIdPurchase AS FNHSysUnitId, SC.FNPricePurchase AS FNPrice, "
        _Str &= vbCrLf & "        MAX(SC.FTFabricFrontSize) AS FTFabricFrontSize"
        _Str &= vbCrLf & "     FROM            (SELECT        FNHSysSuplId, ISNULL"
        _Str &= vbCrLf & "     ((SELECT        COUNT(DISTINCT S.FNHSysCurId) AS FNHSysCurId"
        _Str &= vbCrLf & "    FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "    WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0  AND (S.FNHSysSuplId = A.FNHSysSuplId)"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & " ), 0) AS FNStateCurP, ISNULL"
        _Str &= vbCrLf & "   ((SELECT        MAX( FNHSysUnitIdPurchase) AS FNHSysUnitIdPurchase"
        _Str &= vbCrLf & "   FROM            (SELECT        FNHSysRawMatId, COUNT(DISTINCT FNHSysUnitIdPurchase) AS FNHSysUnitIdPurchase"
        _Str &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "  WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0  AND (S.FNHSysSuplId = A.FNHSysSuplId)"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "    GROUP BY FNHSysRawMatId) AS Z), 0) AS FNStateUnitP, ISNULL"
        _Str &= vbCrLf & "   ((SELECT        Max( FNPricePurchase) AS FNPricePurchase"
        _Str &= vbCrLf & "   FROM            (SELECT        FNHSysRawMatId, COUNT(DISTINCT FNPricePurchase) AS FNPricePurchase"
        _Str &= vbCrLf & "    FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0  AND (S.FNHSysSuplId = A.FNHSysSuplId)"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "  GROUP BY FNHSysRawMatId) AS Z), 0) AS FNStatePriceP"
        _Str &= vbCrLf & "   FROM            (SELECT        S.FNHSysSuplId, MAX(S.FNHSysCurId) AS FNHSysCurId"
        _Str &= vbCrLf & "    FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE        (FTPurchaseNo = N'')   AND ISNULL(FNHSysUnitId,0) <> 0  AND FNTotalPurchaseQuantity>0 "

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "  GROUP BY S.FNHSysSuplId) AS A) AS M INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS SC WITH (NOLOCK) ON M.FNHSysSuplId = SC.FNHSysSuplId"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON SC.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON SC.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE     (SC.FTPurchaseNo = N'') AND SC.FNTotalPurchaseQuantity>0  "
        '  _Str &= vbCrLf & "   AND     (M.FNStateCurP = 1) AND (M.FNStateUnitP = 1) AND (M.FNStatePriceP = 1)  "
        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND SC.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "   GROUP BY M.FNHSysSuplId, SC.FNHSysRawMatId, SC.FNHSysUnitIdPurchase, SC.FNPricePurchase) AS MPO) AS MPO"
        _Str &= vbCrLf & "   INNER Join"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON MPO.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON MPO.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON MPO.FNHSysSuplId = SU.FNHSysSuplId"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "  DROP TABLE #TAB 	"
        End If

        Return _Str
    End Function

    Private Function QueryOrderItem()

        Dim _Str As String = ""

        _Str = " 	"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "  CREATE TABLE  #TAB (FTOrderNo nvarchar(50)) 	"
            _Str &= vbCrLf & " INSERT INTO #TAB(FTOrderNo)	  SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP WITH(NOLOCK) WHERE   ISNULL(FNHSysStyleIdPull,0) <=0 AND  FTOrderBy  IN (SELECT FTUserName FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FT_GetOrderPermission_PurchseTeam('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') ) "
        End If

        _Str &= vbCrLf & "  SELECT   MPO.FTOrderNo,MPO.FNHSysSuplId"
        _Str &= vbCrLf & ", MPO.FNHSysRawMatId"
        _Str &= vbCrLf & ", MPO.FNHSysUnitId"
        _Str &= vbCrLf & ", MPO.FTFabricFrontSize"
        _Str &= vbCrLf & ", Convert(numeric(18,4),MPO.FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & ", FNPrice"
        _Str &= vbCrLf & ", Convert(numeric(18,2),(Convert(numeric(18,4),MPO.FNQuantity) * MPO.FNPrice)) AS FNNetAmt"

        _Str &= vbCrLf & ", IM.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
        Else
            _Str &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
        End If

        _Str &= vbCrLf & " , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & " , U.FTUnitCode "
        _Str &= vbCrLf & ",isnull( ( SELECT MAX(FDShipDate) AS FDShipDate FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK) WHERE FTOrderNo=MPO.FTOrderNo),'') AS FTShipDate"
        _Str &= vbCrLf & ",FNHSysUnitIdPurchaseChk,FNPricePurchaseChk,FNHSysCurIdChk,CR.FTCurCode,Convert(varchar(10),ISNULL(ODTx.FNOrderType,'0')) AS FNOrderType,ISNULL(ODTx.FTOrderNoAutoPO,'') AS  FTOrderNoAutoPO"
        _Str &= vbCrLf & "  FROM            (SELECT        FTOrderNo,FNHSysSuplId, FNHSysRawMatId, FNQuantity, FNHSysUnitId, FNPrice, FTFabricFrontSize,FNHSysUnitIdPurchaseChk,FNPricePurchaseChk,FNHSysCurIdChk"
        _Str &= vbCrLf & "     FROM            (SELECT        SC.FTOrderNo,M.FNHSysSuplId, SC.FNHSysRawMatId, SUM(SC.FNTotalPurchaseQuantity) AS FNQuantity, SC.FNHSysUnitIdPurchase AS FNHSysUnitId, SC.FNPricePurchase AS FNPrice, "
        _Str &= vbCrLf & "        MAX(SC.FTFabricFrontSize) AS FTFabricFrontSize"
        _Str &= vbCrLf & " ,SC.FNHSysUnitIdPurchase AS FNHSysUnitIdPurchaseChk"
        _Str &= vbCrLf & " ,SC.FNPricePurchase AS FNPricePurchaseChk "
        _Str &= vbCrLf & " ,SC.FNHSysCurId AS FNHSysCurIdChk"
        _Str &= vbCrLf & "     FROM            (SELECT        FNHSysSuplId, ISNULL"
        _Str &= vbCrLf & "     ((SELECT        COUNT(DISTINCT S.FNHSysCurId) AS FNHSysCurId"
        _Str &= vbCrLf & "    FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "    WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0    AND FNTotalPurchaseQuantity>0 AND (S.FNHSysSuplId = A.FNHSysSuplId)"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "  ), 0) AS FNStateCurP, ISNULL"
        _Str &= vbCrLf & "   ((SELECT        MAX( FNHSysUnitIdPurchase) AS FNHSysUnitIdPurchase"
        _Str &= vbCrLf & "   FROM            (SELECT        FNHSysRawMatId, COUNT(DISTINCT FNHSysUnitIdPurchase) AS FNHSysUnitIdPurchase"
        _Str &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "  WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0  AND (S.FNHSysSuplId = A.FNHSysSuplId)"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "    GROUP BY FNHSysRawMatId) AS Z), 0) AS FNStateUnitP, ISNULL"
        _Str &= vbCrLf & "   ((SELECT        MAX( FNPricePurchase) AS FNPricePurchase"
        _Str &= vbCrLf & "   FROM            (SELECT        FNHSysRawMatId, COUNT(DISTINCT FNPricePurchase) AS FNPricePurchase"
        _Str &= vbCrLf & "    FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS S WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0  AND (S.FNHSysSuplId = A.FNHSysSuplId)"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "  GROUP BY FNHSysRawMatId) AS Z), 0) AS FNStatePriceP"

        _Str &= vbCrLf & "   FROM            (Select        S.FNHSysSuplId, MAX(S.FNHSysCurId) As FNHSysCurId"
        _Str &= vbCrLf & "    FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing As S With (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As SU With (NOLOCK) On S.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  As AAA With(NOLOCK) On S.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE        (FTPurchaseNo = N'')  AND ISNULL(FNHSysUnitId,0) <> 0   AND FNTotalPurchaseQuantity>0  "

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND S.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "  GROUP BY S.FNHSysSuplId) AS A) AS M INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS SC WITH (NOLOCK) ON M.FNHSysSuplId = SC.FNHSysSuplId"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON SC.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP  AS AAA WITH(NOLOCK) ON SC.FTOrderNo=AAA.FTOrderNo "
        _Str &= vbCrLf & "   WHERE    ISNULL(FNHSysUnitId,0) <> 0   AND (SC.FTPurchaseNo = N'')  AND SC.FNTotalPurchaseQuantity>0    "
        '  _Str &= vbCrLf & "   AND  (M.FNStateCurP = 1)  AND   (M.FNStatePriceP = 1)    AND (M.FNStateUnitP = 1) "

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND SC.FTOrderNo IN (SELECT  FTOrderNo FROM  #TAB  )"
        End If

        _Str &= vbCrLf & "   GROUP BY SC.FTOrderNo,M.FNHSysSuplId, SC.FNHSysRawMatId, SC.FNHSysUnitIdPurchase, SC.FNPricePurchase ,SC.FNHSysUnitIdPurchase ,SC.FNPricePurchase ,SC.FNHSysCurId ) AS MPO) AS MPO"
        _Str &= vbCrLf & "   INNER Join"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON MPO.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON MPO.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SU WITH (NOLOCK) ON MPO.FNHSysSuplId = SU.FNHSysSuplId"
        _Str &= vbCrLf & "   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CR WITH(NOLOCK) ON MPO.FNHSysCurIdChk =CR.FNHSysCurId"

        _Str &= vbCrLf & "   OUTER APPLY ( SELECT TOP 1  ISNULL(Xmx3.FNOrderType ,Xmx.FNOrderType ) AS FNOrderType,Xmx.FTOrderNoAutoPO "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP AS Xmx WITH(NOLOCK) "
        _Str &= vbCrLf & "  OUTER APPLY ( SELECT TOP 1  Xmx2.FNOrderType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderProdAndSMP AS Xmx2 WITH(NOLOCK) WHERE Xmx2.FTOrderNo=  Xmx.FTOrderNoRef AND ISNULL( Xmx2.FTOrderNo,'')<>'' ) AS  Xmx3"
        _Str &= vbCrLf & "  WHERE Xmx.FTOrderNo =MPO.FTOrderNo ) ODTx "

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "  DROP TABLE #TAB 	"
        End If

        Return _Str

    End Function

#End Region

#Region "Procedure"

    Public Sub PrepareDataGenerate(Optional ListJobOrderNo As String = "")

        _ListPurchaseOrder.Clear()
        _ListPurchaseOrderSupl.Clear()
        ogclistorder.DataSource = Nothing
        ogclistsupplier.DataSource = Nothing
        ogcdetail.DataSource = Nothing


        Try
            _DtJob = HI.Conn.SQLConn.GetDataTable(Me.QueryJOb, Conn.DB.DataBaseName.DB_MERCHAN)
            ogclistorder.DataSource = _DtJob
        Catch ex As Exception
        End Try

        Try
            _DtSupl = HI.Conn.SQLConn.GetDataTable(Me.QuerySupplier, Conn.DB.DataBaseName.DB_MERCHAN)

            _ListPurchaseOrderSupl.Add(_DtSupl.Copy)
            _DtSupl.Rows.Clear()
            ogclistsupplier.DataSource = _DtSupl
        Catch ex As Exception
        End Try
        '----- Supplier

        ''----- Item
        Try

            _DtItem = HI.Conn.SQLConn.GetDataTable(Me.QueryPOItem, Conn.DB.DataBaseName.DB_MERCHAN)
            ogcdetail.DataSource = _DtItem.DefaultView

        Catch ex As Exception
        End Try
        ''----- Item

        _DtItemSub = HI.Conn.SQLConn.GetDataTable(Me.QueryOrderItem, Conn.DB.DataBaseName.DB_MERCHAN)

        _ListPurchaseOrder.Add(_DtItemSub.Copy)
        _DtItemSub.Rows.Clear()

        _TmpPO = HI.Conn.SQLConn.GetDataTable("SELECT '' AS FTSelect, '' AS FTSupplier,'' AS FTSupplierName ,'' AS FTPurchaseNo,Convert(nvarchar(500),'') AS FTItemRef, '' AS FTDeliveryCode,'' AS FTDeliveryName", Conn.DB.DataBaseName.DB_PUR)

        Try
            Call CheckSupldata()
            ogclistsupplier.DataSource = _DtSupl
        Catch ex As Exception
        End Try
        Try
            ogvlistsupplier_FocusedRowChanged(ogvlistsupplier, New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0))
        Catch ex As Exception
        End Try


        If ListJobOrderNo <> "" Then

            With CType(ogclistorder.DataSource, DataTable)
                .AcceptChanges()

                For Each JobStr As String In ListJobOrderNo.Split("|")

                    For Each R As DataRow In .Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(JobStr) & "'")

                        R!FTStateSelect = "1"

                        Try
                            Call RemoveOrder(JobStr, True)
                        Catch ex As Exception
                        End Try

                    Next

                Next

                .AcceptChanges()

            End With

        End If
       

    End Sub

    Private Function ValidateData() As Boolean
        Dim _pass As Boolean = False
        Try
            If _DtSupl.Rows.Count > 0 Then
                'If _DtSupl.Select("FTSelect='1'").Length > 0 And _DtSupl.Select("FTStateComplete='1'").Length > 0 Then

                If _DtSupl.Select("FTSelect='1' AND FTStateComplete='1'  AND FNStateCurP=1 AND FNStatePriceP=1 AND FNStateUnitP=1 ").Length > 0 Then
                    If Me.FNHSysCmpRunId.Text <> "" And "" & Me.FNHSysCmpRunId.Properties.Tag.ToString <> "" Then
                        'If Me.FNHSysDeliveryId.Text <> "" And "" & Me.FNHSysDeliveryId.Properties.Tag.ToString <> "" Then
                        _pass = True
                        'Else
                        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysDeliveryId_lbl.Text)
                        '    If (Me.FNHSysDeliveryId.Enabled) Then Me.FNHSysDeliveryId.Focus()
                        'End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysCmpRunId_lbl.Text)
                        If (Me.FNHSysCmpRunId.Enabled) Then Me.FNHSysCmpRunId.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการระบุข้อมูลผู้ขายให้ครบถ้วนสมบูรณ์ !!!", 1401250003, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("ไม่พบข้อมูลผู้ขาย !!!", 1401250004, Me.Text)
            End If

        Catch ex As Exception
        End Try

        Return _pass

    End Function

    Private Sub GeneratePOAuto(_SplashScreen As HI.TL.SplashScreen)
        Dim _Complete As Boolean
        Dim _Supl As String

        '"" & .GetRowCellValue(e.RowHandle, "FNStateCurP").ToString <> "1" Or "" & .GetRowCellValue(e.RowHandle, "FNStatePriceP").ToString <> "1" Or "" & .GetRowCellValue(e.RowHandle, "FNStateUnitP").ToString <> "1"

        For Each Row As DataRow In _DtSupl.Select("FTSelect='1' AND FTStateComplete='1'  AND FNStateCurP=1 AND FNStatePriceP=1 AND FNStateUnitP=1 ")
            _Complete = False
            _Supl = Row!FNHSysSuplId.ToString

            _SplashScreen.UpdateInformation("Generating.... PO Of Supplier " & Row!FTSuplCode.ToString)

            Dim _PoDocNo As String = ""

            _Complete = GenerateDataPO(_Supl, _PoDocNo, Val(Row!FNPoState_Hide.ToString), Row!FNHSysPurGrpId.ToString.Trim(), Val(Row!FNHSysPurGrpId_Hide.ToString), _
                                        Row!FDDeliveryDate.ToString, Val(Row!FNLeadtime), _
                                       Val(Row!FNHSysCurId_Hide.ToString), Val(Row!FNExchangeRate), Val(Row!FNDisCountPer), _
                                       Val(Row!FNVatPer), Val(Row!FNSurcharge), Val(Row!FNHSysCrTermId_Hide), Val(Row!FNCreditDay), Val(Row!FNHSysTermOfPMId_Hide), FTContactPerson.Text, Row!FTSuplCode.ToString, Row!FTSuplName.ToString)

            If (_Complete) And _PoDocNo <> "" Then

                For Each _StrDocNo As String In _PoDocNo.Split("|")

                    If _StrDocNo <> "" Then
                        Me._TmpPO.Rows.Add("1", Row!FTSuplCode.ToString, Row!FTSuplName.ToString, _StrDocNo)
                    End If

                Next

                Call RemoveItem(_Supl)

                'Row.Delete()
            End If

            ' End If
        Next
        _DtSupl.AcceptChanges()

        _DtSupl.BeginInit()
        For Each Row As DataRow In _DtSupl.Select("FTSelect='1' AND FTStateComplete='1'  AND FNStateCurP=1 AND FNStatePriceP=1 AND FNStateUnitP=1 ")
            If _DtItem.Select(" FNHSysSuplId=" & Integer.Parse(Val(Row!FNHSysSuplId.ToString)) & " ").Length > 0 Then
                Row!FTSelect = "1"
            Else
                Row.Delete()
            End If
        Next
        _DtSupl.EndInit()



    End Sub

    Private Function GenerateDataPO(SuplKey As String, ByRef PoNoKey As String, POTypeKey As Integer, POGrpText As String, POGrpKey As Integer, DeliDateKey As String, _
                                                               LeadTimeKey As Integer, CurrCodeKey As Integer, Exchng As Double, DiscountPer As Double, _
                                                               Vat As Double, SurCharge As Double, Credit As Integer, CreditDay As Integer, PaymentTerm As Integer, ContactPerson As String, SuplCode As String, SuplName As String) As Boolean

        PoNoKey = ""
        Dim _PoDocNo As String
        Dim _MaxItem As Integer = 0
        Dim _Filter As String = ""
        Dim _Complete As Boolean
        Dim _Proc As Boolean = False
        Dim PODiscountPer As Decimal = 0.0
        Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
        Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
        Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
        Dim _CmpHCreate As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        Dim _CmpH As String = _CmpHCreate


        Dim cmprunpo As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPORun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun  WITH(NOLOCK)  WHERE FNHSysCmpRunId=" & Val(FNHSysCmpRunId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_MASTER, "")

        If cmprunpo = "" Then
            cmprunpo = Microsoft.VisualBasic.Left(FNHSysCmpRunId.Text, 1)
        End If

        PODiscountPer = Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNPODiscount FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier  WITH(NOLOCK)  WHERE FNHSysSuplId=" & Val(SuplKey) & " ", Conn.DB.DataBaseName.DB_MASTER, "0.00"))

        Select Case Me.GENAUTOPO.SelectedIndex
            Case 0 'Generate By Supplier All Item Code / PO

                _Filter = "FNHSysSuplId='" & HI.UL.ULF.rpQuoted(SuplKey) & "' "

                '-----Generate Purchase Order No


                If (_PORunDocVat) Then

                    If Vat > 0 Then
                        _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, "POC-").ToString
                    Else
                        _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, "POW-").ToString
                    End If

                Else
                    If HI.ST.SysInfo.CmpID = 1306010001 Then
                        _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "H" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                    Else
                        _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                    End If
                End If

                If _PoDocNo <> "" Then

                    _Complete = GeneratePO(SuplKey, _PoDocNo, POTypeKey, POGrpText, POGrpKey, DeliDateKey,
                                                                      LeadTimeKey, CurrCodeKey, Exchng, DiscountPer,
                                                                      Vat, SurCharge, Credit, CreditDay, PaymentTerm, ContactPerson, _MaxItem, _Filter, SuplCode, SuplName, _Year, _Month, cmprunpo)

                    If (_Complete) Then
                        PoNoKey = PoNoKey & "|" & _PoDocNo
                    End If

                End If

            Case 1 'Generate By Supplier 1 Item Code / PO
                Do
                    _Proc = False

                    For Each R As DataRow In _DtItem.Select("FNHSysSuplId=" & HI.UL.ULF.rpQuoted(SuplKey) & " AND FTSelect='1' ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")
                        Application.DoEvents()
                        _Filter = "FNHSysSuplId='" & HI.UL.ULF.rpQuoted(SuplKey) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "

                        '-----Generate Purchase Order No


                        If (_PORunDocVat) Then

                            If Vat > 0 Then
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, "POC-").ToString
                            Else
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, "POW-").ToString
                            End If

                        Else
                            If HI.ST.SysInfo.CmpID = 1306010001 Then
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "H" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                            Else
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                            End If
                        End If



                        If _PoDocNo <> "" Then
                            _Complete = GeneratePO(SuplKey, _PoDocNo, POTypeKey, POGrpText, POGrpKey, DeliDateKey,
                                                                   LeadTimeKey, CurrCodeKey, Exchng, DiscountPer,
                                                                   Vat, SurCharge, Credit, CreditDay, PaymentTerm, ContactPerson, _MaxItem, _Filter, SuplCode, SuplName, _Year, _Month, cmprunpo)

                            If (_Complete) Then
                                PoNoKey = PoNoKey & "|" & _PoDocNo
                            End If

                        End If

                        _Proc = True

                        Exit For

                    Next

                Loop Until Not (_Proc)

            Case 2 'Generate By Supplier 1 Item/ 1 Color/ PO
                Do
                    _Proc = False


                    For Each R As DataRow In _DtItem.Select("FNHSysSuplId='" & HI.UL.ULF.rpQuoted(SuplKey) & "' AND FTSelect='1' ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")
                        Application.DoEvents()
                        _Filter = "FNHSysSuplId='" & HI.UL.ULF.rpQuoted(SuplKey) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(R!FTRawMatColorCode.ToString) & "'  "

                        '-----Generate Purchase Order No

                        If (_PORunDocVat) Then

                            If Vat > 0 Then
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, "POC-").ToString
                            Else
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, "POW-").ToString
                            End If

                        Else
                            If HI.ST.SysInfo.CmpID = 1306010001 Then
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "H" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                            Else
                                _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                            End If
                        End If


                        If _PoDocNo <> "" Then
                            _Complete = GeneratePO(SuplKey, _PoDocNo, POTypeKey, POGrpText, POGrpKey, DeliDateKey,
                                                                                         LeadTimeKey, CurrCodeKey, Exchng, DiscountPer,
                                                                                         Vat, SurCharge, Credit, CreditDay, PaymentTerm, ContactPerson, _MaxItem, _Filter, SuplCode, SuplName, _Year, _Month, cmprunpo)

                            If (_Complete) Then
                                PoNoKey = PoNoKey & "|" & _PoDocNo
                            End If

                        End If

                        R.Delete()
                        _Proc = True

                        Exit For
                    Next

                Loop Until Not (_Proc)

        End Select

        Return True
    End Function

    Private Function GeneratePO(SuplKey As String, ByRef PoNoKey As String, POTypeKey As Integer, POGrpText As String, POGrpKey As Integer, DeliDateKey As String,
                                                               LeadTimeKey As Integer, CurrCodeKey As Integer, Exchng As Double, DiscountPer As Double,
                                                               Vat As Double, SurCharge As Double, Credit As Integer, CreditDay As Integer, PaymentTerm As Integer, ContactPerson As String,
                                                              _MaxItem As Integer, Filter As String, SuplCode As String, SuplName As String, _Year As String, _Month As String, cmprunpo As String) As Boolean

        Dim _Str As String
        Dim _Amt As Double = 0
        Dim _DisAmt As Double = 0
        Dim _VatAmt As Double = 0
        Dim _NetAmt As Double = 0
        Dim _AmtTH As String
        Dim _AmtEN As String
        Dim _Seq As Integer = 0
        Dim _OGACDate As String = ""
        Dim _OGACDateRemark As String = ""
        Dim _OGACDateOrg As String = ""
        Dim _DeleteOGACDate As Boolean = False

        Dim _Remark As String = Me.FTRemark.Text.Trim
        Dim _RemarkOrg As String = Me.FTRemark.Text.Trim
        Dim _DtSplitPO As DataTable = Nothing
        Dim _DtSplitPOItemSub As DataTable = Nothing
        Dim valuechk As String =
        _Seq = 0
        For Each Row As DataRow In _DtItem.Select(Filter & "  AND FTSelect='1'  ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")
            Application.DoEvents()
            _Seq = _Seq + 1

            _Str = " Select TOP 1 CASE WHEN  ISNULL(MM.FTStateSplitPO,'')='1' THEN '1' ELSE CASE WHEN  ISNULL(MM.FTStateUPCSplitPO,'')='1' THEN '2' ELSE '0' END END AS FTStateSplitPO"
            _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM  WITH(NOLOCK)  ON IM.FTRawMatCode = MM.FTMainMatCode"
            _Str &= vbCrLf & " WHERE (IM.FNHSysRawMatId =" & Val(Row!FNHSysRawMatId.ToString) & ")"
            valuechk = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")


            If valuechk = "1" Or valuechk = "2" Then

            Else
                Dim grpotype As List(Of String) = (_DtItemSub.Select("FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "", "FNOrderType").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FNOrderType")) _
                                                   .Distinct() _
                                                   .ToList()


                For Each StrFO As String In grpotype
                    If StrFO = "19" Then
                        valuechk = "99"
                        Exit For
                    End If
                Next
            End If


            If valuechk = "1" Or valuechk = "2" Or valuechk = "99" Then

            Else
                valuechk = "8888"
                'Dim grpfodatachk As List(Of String) = (_DtItemSub.Select("FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                '                                     .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                '                                     .Distinct() _
                '                                     .ToList()


                'Dim _StrAllFO As String = ""
                'For Each StrFO As String In grpfodatachk
                '    If _StrAllFO = "" Then
                '        _StrAllFO = StrFO
                '    Else
                '        _StrAllFO = _StrAllFO & "," & StrFO
                '    End If
                'Next
            End If

            If valuechk = "1" Or valuechk = "2" Or valuechk = "99" Or valuechk = "8888" Then
                Row!FNStateSplit = 1

                Dim _dt As New DataTable
                Dim grpfodata As List(Of String) = (_DtItemSub.Select("FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                     .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                     .Distinct() _
                                                     .ToList()

                Dim _StrAllFO As String = ""
                For Each StrFO As String In grpfodata
                    If _StrAllFO = "" Then
                        _StrAllFO = StrFO
                    Else
                        _StrAllFO = _StrAllFO & "," & StrFO
                    End If
                Next

                Select Case valuechk
                    Case "1"
                        _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_CHECK_PO_ITEM_SPLIT_AUTO " & Val(Row!FNHSysRawMatId.ToString) & "," & Val(SuplKey) & ",'" & HI.UL.ULF.rpQuoted(_StrAllFO) & "'"

                    Case "2"
                        _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_CHECK_PO_ITEM_SPLIT_UPC_AUTO " & Val(Row!FNHSysRawMatId.ToString) & "," & Val(SuplKey) & ",'" & HI.UL.ULF.rpQuoted(_StrAllFO) & "'"
                    Case "99"
                        _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_CHECK_PO_ITEM_SPLIT_MS_AUTO " & Val(Row!FNHSysRawMatId.ToString) & "," & Val(SuplKey) & ",'" & HI.UL.ULF.rpQuoted(_StrAllFO) & "'"

                    Case "8888"
                        _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_CHECK_PO_ITEM_SPLIT_SAF " & Val(Row!FNHSysRawMatId.ToString) & "," & Val(SuplKey) & ",'" & HI.UL.ULF.rpQuoted(_StrAllFO) & "'"
                End Select

                _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_PUR)

                If _dt.Rows.Count > 0 Then

                    If _DtSplitPO Is Nothing Then
                        _DtSplitPO = _dt.Copy
                    Else
                        _DtSplitPO.Merge(_dt.Copy)
                    End If

                End If

            Else
                Row!FNStateSplit = 0
            End If
        Next

        _OGACDate = ""
        _OGACDateRemark = ""
        If Not _DtSplitPO Is Nothing Then
            If _DtSplitPO.Rows.Count > 0 Then
                Dim grp As List(Of String) = (_DtSplitPO.Select("FNHSysRawMatId>0", "FTMonthShip").CopyToDataTable).AsEnumerable() _
                                                     .Select(Function(r) r.Field(Of String)("FTMonthShip")) _
                                                     .Distinct() _
                                                     .ToList()


                _DeleteOGACDate = True
                For Each StrFO As String In grp

                    _OGACDateOrg = StrFO

                    Dim StrFO2 As String = ""
                    _OGACDateRemark = ""
                    For Each Rxc As DataRow In _DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(_OGACDateOrg) & "' ")
                        _OGACDateRemark = Rxc!FTSAFCode.ToString()

                        If Rxc!FTSAFPlanInfo.ToString() <> "" Then

                            _OGACDateRemark = _OGACDateRemark & " (" & Rxc!FTSAFPlanInfo.ToString() & ")"

                        End If

                        StrFO2 = Rxc!FTGacDate.ToString()

                        Exit For
                    Next

                    If StrFO2 <> "" Then
                        If StrFO.Length >= 10 Then
                            _OGACDate = HI.UL.ULDate.ConvertEN(StrFO2)
                        Else
                            _OGACDate = Microsoft.VisualBasic.Right(StrFO2, 2) + "/" + Microsoft.VisualBasic.Left(StrFO2, 4)
                        End If

                        _OGACDateRemark = _OGACDateRemark & " O GAC Date : " & _OGACDate

                    End If

                    Exit For
                Next
            End If
        End If


        _Seq = 0
        '  _DtItem.BeginInit()
        For Each Row As DataRow In _DtItem.Select(Filter & "  AND FTSelect='1'  ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")
            Application.DoEvents()
            _Seq = _Seq + 1

            If Val(Row!FNStateSplit) = 1 Then

                If _DeleteOGACDate Then
                    Dim grp2 As List(Of String) = Nothing

                    Try

                        grp2 = (_DtSplitPO.Select("FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FTMonthShip='" & HI.UL.ULF.rpQuoted(_OGACDateOrg) & "'", "FTMonthShip").CopyToDataTable).AsEnumerable() _
                                                    .Select(Function(r) r.Field(Of String)("FTMonthShip")) _
                                                    .Distinct() _
                                                    .ToList()

                    Catch ex As Exception
                    End Try

                    If Not (grp2 Is Nothing) Then

                        If grp2.Count > 0 Then '

                            Dim grpfodata As List(Of String) = (_DtItemSub.Select("FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                    .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                    .Distinct() _
                                                    .ToList()

                            Dim _TotalQty As Double = 0

                            For Each StrcheckFt As String In grpfodata

                                If _DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(_OGACDateOrg) & "'  AND FTOrderNo='" & HI.UL.ULF.rpQuoted(StrcheckFt) & "' AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "").Length > 0 Then

                                    For Each Rxc As DataRow In _DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(_OGACDateOrg) & "' AND FTOrderNo='" & HI.UL.ULF.rpQuoted(StrcheckFt) & "' AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "")

                                        _TotalQty = _TotalQty + Val(Rxc!FNSCPQuantity.ToString)

                                        For Each Rms As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxc!FTOrderNo.ToString) & "'")

                                            Rms!FNQuantity = Val(Rxc!FNSCPQuantity.ToString)

                                        Next

                                    Next

                                Else

                                    For Each Rms As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(StrcheckFt) & "'")
                                        Rms!FNQuantity = 0
                                    Next

                                End If

                            Next

                            If _TotalQty Mod 1 > 0 Then
                                _TotalQty = (_TotalQty - (_TotalQty Mod 1)) + 1
                            End If

                            Row!FNQuantity = _TotalQty

                            _Amt = _Amt + CDbl(Format(Val(Row!FNPrice.ToString) * _TotalQty, HI.ST.Config.AmtFormat))

                        Else
                            Dim grpfodata As List(Of String) = (_DtItemSub.Select("FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                  .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                  .Distinct() _
                                                  .ToList()

                            Dim _TotalQty As Double = 0

                            For Each StrcheckFt As String In grpfodata
                                For Each Rms As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(StrcheckFt) & "'")
                                    Rms!FNQuantity = 0
                                Next
                            Next

                            If _TotalQty Mod 1 > 0 Then
                                _TotalQty = (_TotalQty - (_TotalQty Mod 1)) + 1
                            End If

                            Row!FNQuantity = _TotalQty

                            _Amt = _Amt + CDbl(Format(Val(Row!FNPrice.ToString) * _TotalQty, HI.ST.Config.AmtFormat))
                        End If
                    Else
                        Dim grpfodata As List(Of String) = (_DtItemSub.Select("FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                 .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                 .Distinct() _
                                                 .ToList()

                        Dim _TotalQty As Double = 0

                        For Each StrcheckFt As String In grpfodata
                            For Each Rms As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(StrcheckFt) & "'")
                                Rms!FNQuantity = 0
                            Next
                        Next

                        If _TotalQty Mod 1 > 0 Then
                            _TotalQty = (_TotalQty - (_TotalQty Mod 1)) + 1
                        End If

                        Row!FNQuantity = _TotalQty

                        _Amt = _Amt + CDbl(Format(Val(Row!FNPrice.ToString) * _TotalQty, HI.ST.Config.AmtFormat))
                    End If
                Else
                    _Amt = _Amt + CDbl(Format(Val(Row!FNPrice.ToString) * Val(Row!FNQuantity.ToString), HI.ST.Config.AmtFormat))
                End If


            Else
                _Amt = _Amt + CDbl(Format(Val(Row!FNPrice.ToString) * Val(Row!FNQuantity.ToString), HI.ST.Config.AmtFormat))
            End If

        Next

        If Not _DtSplitPO Is Nothing Then
            If _DtSplitPO.Rows.Count > 0 Then
                If _DeleteOGACDate Then
                    _DtSplitPO.BeginInit()
                    For Each R As DataRow In _DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(_OGACDateOrg) & "'")
                        R.Delete()
                    Next
                    _DtSplitPO.EndInit()

                End If
            End If
        End If

        '  _DtItem.EndInit()
        _Remark = _RemarkOrg
        If _OGACDateRemark <> "" Then
            '_Remark = "O GAC Date : " & _OGACDate & vbCrLf & _RemarkOrg

            _Remark = _OGACDateRemark & vbCrLf & _RemarkOrg


        End If

        If DiscountPer > 0 Then
            _DisAmt = CDbl(Format((_Amt * DiscountPer) / 100, HI.ST.Config.AmtFormat))
        End If

        If Vat > 0 Then
            _VatAmt = CDbl(Format((((_Amt - _DisAmt) + SurCharge) * Vat) / 100, HI.ST.Config.AmtFormat))
        End If

        _NetAmt = ((_Amt - _DisAmt) + SurCharge) + _VatAmt

        _AmtEN = HI.UL.ULF.Convert_Bath_EN(_NetAmt)
        _AmtTH = HI.UL.ULF.Convert_Bath_TH(_NetAmt)


        If Not _DtSplitPO Is Nothing Then

            If _DtSplitPO.Rows.Count > 0 Then

                _DtSplitPOItemSub = _DtItemSub.Copy

            End If

        End If

        Try

            Dim cmdstring As String = ""
            Dim DeliveryId As Integer = 0 ' Val(FNHSysDeliveryId.Properties.Tag.ToString)
            Dim dtdelivery As New DataTable
            Dim CmpCreatePOId As Integer = 0
            Dim CmpCreateOrderId As Integer = 0

            For Each Row As DataRow In _DtItem.Select(Filter & "  AND FTSelect='1'  ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")
                Dim _Filter As String = Filter

                If _Filter <> "" Then
                    _Filter = _Filter & "  AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FNQuantity > 0  "
                Else
                    _Filter = "  FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FNQuantity > 0  "
                End If

                For Each R As DataRow In _DtItemSub.Select(_Filter, "FNOrderType,FTShipDate") 'DRaw
                    ' R!FTOrderNo.ToString

                    cmdstring = " Select Top 1 A.FTOrderNo, B.FNHSysDeliveryIdDomestic, B.FNHSysDeliveryIdImport,D1.FNHSysCmpId AS FNHSysCmpIdDomes,D2.FNHSysCmpId AS FNHSysCmpIdImp,ISNULL(A.FNHSysCmpPOId,0) AS FNHSysCmpPOId,B.FNHSysCmpId AS FNHSysCmpIdOrder "
                    cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderProdAndSMP As A With(NOLOCK) INNER Join"
                    cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS B WITH(NOLOCK) ON A.FNHSysCmpId = B.FNHSysCmpId"
                    cmdstring &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery AS D1 WITH(NOLOCK) ON B.FNHSysDeliveryIdDomestic = D1.FNHSysDeliveryId"
                    cmdstring &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery AS D2 WITH(NOLOCK) ON B.FNHSysDeliveryIdImport = D2.FNHSysDeliveryId"
                    cmdstring &= vbCrLf & "    Where A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

                    dtdelivery = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each Rx As DataRow In dtdelivery.Rows
                        CmpCreateOrderId = Val(Rx!FNHSysCmpIdOrder.ToString)

                        If POTypeKey = 0 Then

                            If Val(Rx!FNHSysDeliveryIdDomestic.ToString) > 0 Then

                                DeliveryId = Val(Rx!FNHSysDeliveryIdDomestic.ToString)
                                CmpCreatePOId = Val(Rx!FNHSysCmpIdDomes.ToString)

                            End If

                        Else

                            If Val(Rx!FNHSysDeliveryIdImport.ToString) > 0 Then

                                DeliveryId = Val(Rx!FNHSysDeliveryIdImport.ToString)
                                CmpCreatePOId = Val(Rx!FNHSysCmpIdImp.ToString)

                            End If

                        End If

                        If Val(Rx!FNHSysCmpPOId.ToString) > 0 Then
                            CmpCreatePOId = Val(Rx!FNHSysCmpPOId.ToString)
                        End If

                        Exit For
                    Next

                    If DeliveryId > 0 Then
                        Exit For
                    End If

                Next

                If DeliveryId > 0 Then
                    Exit For
                End If

            Next

            If DeliveryId <= 0 Then


                For Each Row As DataRow In _DtItem.Select(Filter & "  AND FTSelect='1'  ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")
                    Dim _Filter As String = Filter

                    If _Filter <> "" Then
                        _Filter = _Filter & "  AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "   "
                    Else
                        _Filter = "  FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & "   "
                    End If

                    For Each R As DataRow In _DtItemSub.Select(_Filter, "FNOrderType,FTShipDate") 'DRaw
                        ' R!FTOrderNo.ToString

                        cmdstring = " Select Top 1 A.FTOrderNo, B.FNHSysDeliveryIdDomestic, B.FNHSysDeliveryIdImport,D1.FNHSysCmpId AS FNHSysCmpIdDomes,D2.FNHSysCmpId AS FNHSysCmpIdImp,ISNULL(A.FNHSysCmpPOId,0) AS FNHSysCmpPOId,B.FNHSysCmpId AS FNHSysCmpIdOrder "
                        cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderProdAndSMP As A With(NOLOCK) INNER Join"
                        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS B WITH(NOLOCK) ON A.FNHSysCmpId = B.FNHSysCmpId"
                        cmdstring &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery AS D1 WITH(NOLOCK) ON B.FNHSysDeliveryIdDomestic = D1.FNHSysDeliveryId"
                        cmdstring &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery AS D2 WITH(NOLOCK) ON B.FNHSysDeliveryIdImport = D2.FNHSysDeliveryId"
                        cmdstring &= vbCrLf & "    Where A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

                        dtdelivery = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        For Each Rx As DataRow In dtdelivery.Rows
                            CmpCreateOrderId = Val(Rx!FNHSysCmpIdOrder.ToString)
                            If POTypeKey = 0 Then

                                If Val(Rx!FNHSysDeliveryIdDomestic.ToString) > 0 Then

                                    DeliveryId = Val(Rx!FNHSysDeliveryIdDomestic.ToString)
                                    CmpCreatePOId = Val(Rx!FNHSysCmpIdDomes.ToString)

                                End If

                            Else

                                If Val(Rx!FNHSysDeliveryIdImport.ToString) > 0 Then

                                    DeliveryId = Val(Rx!FNHSysDeliveryIdImport.ToString)
                                    CmpCreatePOId = Val(Rx!FNHSysCmpIdImp.ToString)

                                End If

                            End If

                            If Val(Rx!FNHSysCmpPOId.ToString) > 0 Then
                                CmpCreatePOId = Val(Rx!FNHSysCmpPOId.ToString)
                            End If

                            Exit For
                        Next

                        If DeliveryId > 0 Then
                            Exit For
                        End If

                    Next

                    If DeliveryId > 0 Then
                        Exit For
                    End If

                Next

            End If

            dtdelivery.Dispose()

            Dim _CmpH As String = ""

            'If CmpCreatePOId > 0 Then
            '    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(CmpCreatePOId) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            'Else
            '    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            'End If


            If CmpCreateOrderId > 0 Then
                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(CmpCreateOrderId) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            Else
                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            End If



            If HI.ST.SysInfo.CmpID = 1306010001 Then
                PoNoKey = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "H" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
            Else
                PoNoKey = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString

            End If

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase(FTInsUser, FDInsDate, FTInsTime"
            _Str &= vbCrLf & " , FTPurchaseNo,FNHSysCmpId, FDPurchaseDate, FTPurchaseBy"
            _Str &= vbCrLf & " , FTPurchaseState, FTRefer, FNPoState"
            _Str &= vbCrLf & " , FNHSysPurGrpId, FNHSysCmpRunId, FNHSysSuplId"
            _Str &= vbCrLf & " , FDDeliveryDate,   FNHSysCrTermId, FNCreditDay"
            _Str &= vbCrLf & " , FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate"
            _Str &= vbCrLf & " , FNHSysDeliveryId, FTContactPerson, FDSampleAppDate"
            _Str &= vbCrLf & " , FDSignDate, FDBLDate, FDSuplCfmDliDate, FDCfmDate"
            _Str &= vbCrLf & " , FTRemark, FNPoAmt, FNDisCountPer"
            _Str &= vbCrLf & " , FNDisCountAmt, FNPONetAmt, FNVatPer"
            _Str &= vbCrLf & " , FNVatAmt, FNSurcharge, FNPOGrandAmt"
            _Str &= vbCrLf & " , FTPOGrandAmtTH, FTPOGrandAmtEN, FTStateSuperVisorApp"
            _Str &= vbCrLf & " , FTStateManagerApp,   FTStateSendMail, FTStatePrint"
            _Str &= vbCrLf & " , FTStateColorBox,FTStateSendApp,FNPoType,FNHSysDocCmpId)"

            _Str &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(PoNoKey) & "'," & HI.ST.SysInfo.CmpID & ""
            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & " AUTO ' + Convert(varchar(10),GetDate(),103)  +' '+ Convert(varchar(8),GetDate(),114),''," & POTypeKey & " "
            _Str &= vbCrLf & "," & Val(POGrpKey) & "," & Val(FNHSysCmpRunId.Properties.Tag.ToString) & " "
            _Str &= vbCrLf & "," & Val(SuplKey) & ",'" & HI.UL.ULDate.ConvertEnDB(DeliDateKey) & "'," & Credit & "," & CreditDay & ""
            _Str &= vbCrLf & "," & PaymentTerm & "," & CurrCodeKey & "," & Exchng & ""
            _Str &= vbCrLf & "," & DeliveryId & ",'" & HI.UL.ULF.rpQuoted(ContactPerson) & "'"
            _Str &= vbCrLf & ",'','','','','' "
            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Remark) & "'"
            _Str &= vbCrLf & "," & _Amt & "  "
            _Str &= vbCrLf & "," & DiscountPer & "," & _DisAmt & "," & (_Amt - _DisAmt) & "," & Vat & "," & _VatAmt & "," & SurCharge & "," & _NetAmt & ""
            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_AmtTH) & "','" & HI.UL.ULF.rpQuoted(_AmtEN) & "','0','0','0','0','" & FTStateColorBox.EditValue.ToString & "','0',0 "

            If CmpCreatePOId > 0 Then
                _Str &= vbCrLf & "," & Val(CmpCreatePOId) & "  "
            Else
                _Str &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & "  "
            End If

            Application.DoEvents()
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Seq = 0

            For Each Row As DataRow In _DtItem.Select(Filter & "  AND FTSelect='1'  ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")

                Dim _Filter As String = Filter

                If _Filter <> "" Then
                    _Filter = _Filter & "  AND FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FNQuantity > 0  "
                Else
                    _Filter = "  FNHSysRawMatId=" & Val(Row!FNHSysRawMatId.ToString) & " AND FNQuantity > 0  "
                End If

                Dim DRaw() As DataRow = _DtItemSub.Select(_Filter, "FTShipDate")
                Dim TotalRow As Integer = DRaw.Length
                Dim Count As Integer = 0
                Dim TotalPoQty As Double = Val(Row!FNQuantity.ToString)
                Dim TotalPoAmt As Double = Val(Row!FNNetAmt.ToString)
                Dim _PurQty As Double = 0
                Dim _PurAmount As Double = 0

                _DtItemSub.BeginInit()
                For Each R As DataRow In _DtItemSub.Select(_Filter, "FNOrderType,FTShipDate") 'DRaw
                    Application.DoEvents()
                    Count = Count + 1
                    _Seq = _Seq + 1

                    _PurQty = CDbl(Format((Val(R!FNQuantity.ToString)), HI.ST.Config.QtyFormat))
                    _PurAmount = CDbl(Format((Val(R!FNQuantity.ToString) * Val(R!FNPrice.ToString)), HI.ST.Config.AmtFormat))

                    If Count = TotalRow Then
                        _PurQty = TotalPoQty
                        _PurAmount = TotalPoAmt
                    End If

                    Dim _FTRawMatColorNameTH As String = ""
                    Dim _FTRawMatColorNameEN As String = ""
                    Dim _dtRawMatColor As New DataTable

                    _Str = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                    _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
                    _Str &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Str &= vbCrLf & " AND RM.FNHSysRawMatId =" & Val(Row!FNHSysRawMatId.ToString) & " "

                    Try
                        _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        For Each RxI As DataRow In _dtRawMatColor.Rows

                            _FTRawMatColorNameTH = RxI!FTRawMatColorNameTH.ToString
                            _FTRawMatColorNameEN = RxI!FTRawMatColorNameEN.ToString

                            Exit For

                        Next

                    Catch ex As Exception
                    End Try

                    _dtRawMatColor.Dispose()

                    _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                    _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FTOGacDate)"
                    _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(PoNoKey) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                    _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                    _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                    _Str &= vbCrLf & ",0 "
                    _Str &= vbCrLf & ",0 "
                    _Str &= vbCrLf & "," & _PurQty & " "
                    _Str &= vbCrLf & "," & _PurAmount & " "
                    _Str &= vbCrLf & ",'' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OGACDate) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing SET "
                    _Str &= vbCrLf & " FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNoKey) & "' "
                    _Str &= vbCrLf & " , FTStateAutoPO='1'"
                    _Str &= vbCrLf & ", FDStateAutoDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ", FTStateAutoTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & ", FTStateAutoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & "  WHERE     (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "') "
                    _Str &= vbCrLf & " AND (FNHSysRawMatId = " & Val(R!FNHSysRawMatId.ToString) & ") "
                    _Str &= vbCrLf & " AND FTPurchaseNo=''  AND FNTotalPurchaseQuantity>0 "

                    Application.DoEvents()
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    TotalPoQty = CDbl(Format((TotalPoQty - _PurQty), HI.ST.Config.QtyFormat))
                    TotalPoAmt = CDbl(Format((TotalPoAmt - _PurAmount), HI.ST.Config.AmtFormat))

                    R.Delete()

                Next

                _DtItemSub.EndInit()

            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Call RoudUpPOAmount(PoNoKey, DiscountPer, Vat, SurCharge)

            If Not _DtSplitPO Is Nothing Then
                If _DtSplitPO.Rows.Count > 0 Then

                    Try
                        _DtSplitPOItemSub.BeginInit()
                        _DtSplitPOItemSub.Columns.Add("FNStateAdd", GetType(Integer))
                        _DtSplitPOItemSub.EndInit()

                        Dim _RawMatPrice As Double = 0

                        _CmpH = ""

                        'If CmpCreatePOId > 0 Then
                        '    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(CmpCreatePOId) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        'Else
                        '    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        'End If

                        If CmpCreateOrderId > 0 Then
                            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(CmpCreateOrderId) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

                        Else
                            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

                        End If

                        Dim _PoDocNo As String = ""

                        Dim grp As List(Of String) = (_DtSplitPO.Select("FNHSysRawMatId>0", "FTMonthShip").CopyToDataTable).AsEnumerable() _
                                                        .Select(Function(r) r.Field(Of String)("FTMonthShip")) _
                                                        .Distinct() _
                                                        .ToList()

                        _OGACDate = ""
                        _Remark = Me.FTRemark.Text.Trim

                        For Each StrData As String In grp
                            _Amt = 0
                            _DisAmt = 0
                            _VatAmt = 0
                            _NetAmt = 0
                            _AmtTH = ""
                            _AmtEN = ""
                            _Seq = 0

                            _OGACDate = ""


                            _OGACDateRemark = ""
                            If StrData <> "" Then


                                For Each StrFO As String In grp

                                    _OGACDateOrg = StrFO

                                    Dim StrFO2 As String = ""
                                    _OGACDateRemark = ""
                                    For Each Rxc As DataRow In _DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(StrData) & "' ")
                                        _OGACDateRemark = Rxc!FTSAFCode.ToString()

                                        If Rxc!FTSAFPlanInfo.ToString() <> "" Then
                                            _OGACDateRemark = _OGACDateRemark & " (" & Rxc!FTSAFPlanInfo.ToString() & ")"
                                        End If

                                        StrFO2 = Rxc!FTGacDate.ToString()

                                        Exit For
                                    Next

                                    If StrFO2 <> "" Then
                                        If StrFO.Length >= 10 Then
                                            _OGACDate = HI.UL.ULDate.ConvertEN(StrFO2)
                                        Else
                                            _OGACDate = Microsoft.VisualBasic.Right(StrFO2, 2) + "/" + Microsoft.VisualBasic.Left(StrFO2, 4)
                                        End If

                                        _OGACDateRemark = _OGACDateRemark & " O GAC Date : " & _OGACDate

                                    End If

                                    Exit For
                                Next

                                'If StrData.Length >= 10 Then
                                '    _OGACDate = HI.UL.ULDate.ConvertEN(StrData)
                                'Else
                                '    _OGACDate = Microsoft.VisualBasic.Right(StrData, 2) + "/" + Microsoft.VisualBasic.Left(StrData, 4)
                                'End If

                            End If

                            _Remark = _RemarkOrg
                            If _OGACDateRemark <> "" Then
                                ' _Remark = "O GAC Date : " & _OGACDate & vbCrLf & _RemarkOrg

                                _Remark = _OGACDateRemark & vbCrLf & _RemarkOrg

                            End If

                            Dim grpitemmatcode As List(Of String) = (_DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(StrData) & "'", "FTMainMatCode").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTMainMatCode")) _
                                                      .Distinct() _
                                                      .ToList()

                            For Each DataMatCode As String In grpitemmatcode

                                Dim grpitem As List(Of Integer) = (_DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(StrData) & "' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(DataMatCode) & "'", "FNHSysRawMatId").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of Integer)("FNHSysRawMatId")) _
                                                      .Distinct() _
                                                      .ToList()


                                If HI.ST.SysInfo.CmpID = 1306010001 Then
                                    _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "H" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                                Else
                                    _PoDocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTPurchase", "", False, _CmpH & "" & cmprunpo & _Year & POGrpText & HI.TL.CboList.GetListRefer("FNPoState", POTypeKey) & _Month).ToString
                                End If


                                Dim _TotalQty As Double = 0
                                Dim _TotalRow As Integer = 0
                                _Amt = 0
                                _DisAmt = 0
                                _VatAmt = 0
                                _NetAmt = 0
                                _AmtTH = ""
                                _AmtEN = ""
                                _Seq = 0

                                For Each RaeMatID As Integer In grpitem

                                    For Each RmP As DataRow In _DtItem.Select("FNHSysSuplId=" & Val(SuplKey) & " AND  FNHSysRawMatId=" & RaeMatID & "")

                                        _RawMatPrice = RmP!FNPrice.ToString

                                        _TotalQty = 0

                                        For Each Rxc As DataRow In _DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(StrData) & "' AND FNHSysRawMatId=" & RaeMatID & "")
                                            _TotalRow = _TotalRow + 1

                                            _TotalQty = _TotalQty + Val(Rxc!FNSCPQuantity.ToString)

                                            For Each Rms As DataRow In _DtSplitPOItemSub.Select("FNHSysSuplId=" & Val(SuplKey) & " AND  FNHSysRawMatId=" & RaeMatID & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxc!FTOrderNo.ToString) & "'")
                                                Rms!FNQuantity = Val(Rxc!FNSCPQuantity.ToString)
                                                Rms!FNStateAdd = 1

                                            Next

                                        Next

                                        If _TotalQty Mod 1 > 0 Then
                                            _TotalQty = (_TotalQty - (_TotalQty Mod 1)) + 1
                                        End If

                                        RmP!FNQuantity = _TotalQty

                                        _Amt = _Amt + CDbl(Format(_RawMatPrice * _TotalQty, HI.ST.Config.AmtFormat))

                                        Exit For

                                    Next

                                Next


                                If DiscountPer > 0 Then
                                    _DisAmt = CDbl(Format((_Amt * DiscountPer) / 100, HI.ST.Config.AmtFormat))
                                End If

                                If Vat > 0 Then
                                    _VatAmt = CDbl(Format((((_Amt - _DisAmt) + SurCharge) * Vat) / 100, HI.ST.Config.AmtFormat))
                                End If

                                _NetAmt = ((_Amt - _DisAmt) + SurCharge) + _VatAmt

                                _AmtEN = HI.UL.ULF.Convert_Bath_EN(_NetAmt)
                                _AmtTH = HI.UL.ULF.Convert_Bath_TH(_NetAmt)

                                _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase(FTInsUser, FDInsDate, FTInsTime"
                                _Str &= vbCrLf & " , FTPurchaseNo,FNHSysCmpId, FDPurchaseDate, FTPurchaseBy"
                                _Str &= vbCrLf & " , FTPurchaseState, FTRefer, FNPoState"
                                _Str &= vbCrLf & " , FNHSysPurGrpId, FNHSysCmpRunId, FNHSysSuplId"
                                _Str &= vbCrLf & " , FDDeliveryDate,   FNHSysCrTermId, FNCreditDay"
                                _Str &= vbCrLf & " , FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate"
                                _Str &= vbCrLf & " , FNHSysDeliveryId, FTContactPerson, FDSampleAppDate"
                                _Str &= vbCrLf & " , FDSignDate, FDBLDate, FDSuplCfmDliDate, FDCfmDate"
                                _Str &= vbCrLf & " , FTRemark, FNPoAmt, FNDisCountPer"
                                _Str &= vbCrLf & " , FNDisCountAmt, FNPONetAmt, FNVatPer"
                                _Str &= vbCrLf & " , FNVatAmt, FNSurcharge, FNPOGrandAmt"
                                _Str &= vbCrLf & " , FTPOGrandAmtTH, FTPOGrandAmtEN, FTStateSuperVisorApp"
                                _Str &= vbCrLf & " , FTStateManagerApp,   FTStateSendMail, FTStatePrint"
                                _Str &= vbCrLf & " , FTStateColorBox,FTStateSendApp,FNPoType,FNHSysDocCmpId)"

                                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_PoDocNo) & "'," & HI.ST.SysInfo.CmpID & ""
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & " AUTO ' + Convert(varchar(10),GetDate(),103)  +' '+ Convert(varchar(8),GetDate(),114),''," & POTypeKey & " "
                                _Str &= vbCrLf & "," & Val(POGrpKey) & "," & Val(FNHSysCmpRunId.Properties.Tag.ToString) & " "
                                _Str &= vbCrLf & "," & Val(SuplKey) & ",'" & HI.UL.ULDate.ConvertEnDB(DeliDateKey) & "'," & Credit & "," & CreditDay & ""
                                _Str &= vbCrLf & "," & PaymentTerm & "," & CurrCodeKey & "," & Exchng & ""
                                _Str &= vbCrLf & "," & Val(DeliveryId) & ",'" & HI.UL.ULF.rpQuoted(ContactPerson) & "'"
                                _Str &= vbCrLf & ",'','','','','' "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Remark) & "'"
                                _Str &= vbCrLf & "," & _Amt & "  "
                                _Str &= vbCrLf & "," & DiscountPer & "," & _DisAmt & "," & (_Amt - _DisAmt) & "," & Vat & "," & _VatAmt & "," & SurCharge & "," & _NetAmt & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_AmtTH) & "','" & HI.UL.ULF.rpQuoted(_AmtEN) & "','0','0','0','0','" & FTStateColorBox.EditValue.ToString & "','0',0 "

                                If CmpCreatePOId > 0 Then
                                    _Str &= vbCrLf & "," & Val(CmpCreatePOId) & "  "
                                Else
                                    _Str &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & "  "
                                End If


                                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                                For Each RaeMatID As Integer In grpitem

                                    _TotalQty = 0
                                    _Amt = 0
                                    _TotalRow = 0

                                    For Each RmP As DataRow In _DtItem.Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & RaeMatID & "")

                                        _RawMatPrice = Val(RmP!FNPrice.ToString)
                                        _TotalQty = Val(RmP!FNQuantity)


                                        _Amt = _Amt + CDbl(Format(_RawMatPrice * _TotalQty, HI.ST.Config.AmtFormat))

                                        For Each Rxc As DataRow In _DtSplitPO.Select("FTMonthShip='" & HI.UL.ULF.rpQuoted(StrData) & "' AND FNHSysRawMatId=" & RaeMatID & "")
                                            _TotalRow = _TotalRow + 1
                                        Next

                                        Exit For

                                    Next

                                    Dim TotalRow As Integer = _TotalRow
                                    Dim Count As Integer = 0
                                    Dim TotalPoQty As Double = _TotalQty
                                    Dim TotalPoAmt As Double = _Amt
                                    Dim _PurQty As Double = 0
                                    Dim _PurAmount As Double = 0

                                    For Each R As DataRow In _DtSplitPOItemSub.Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & RaeMatID & " AND FNStateAdd=1") 'DRaw

                                        Application.DoEvents()
                                        Count = Count + 1
                                        _Seq = _Seq + 1

                                        _PurQty = CDbl(Format((Val(R!FNQuantity.ToString)), HI.ST.Config.QtyFormat))
                                        _PurAmount = CDbl(Format((Val(R!FNQuantity.ToString) * Val(R!FNPrice.ToString)), HI.ST.Config.AmtFormat))

                                        If Count = TotalRow Then

                                            _PurQty = TotalPoQty
                                            _PurAmount = TotalPoAmt

                                        End If

                                        Dim _FTRawMatColorNameTH As String = ""
                                        Dim _FTRawMatColorNameEN As String = ""
                                        Dim _dtRawMatColor As New DataTable

                                        _Str = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                                        _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
                                        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
                                        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
                                        _Str &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                        _Str &= vbCrLf & " AND RM.FNHSysRawMatId =" & RaeMatID & " "


                                        Try

                                            _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                                            For Each RxI As DataRow In _dtRawMatColor.Rows

                                                _FTRawMatColorNameTH = RxI!FTRawMatColorNameTH.ToString
                                                _FTRawMatColorNameEN = RxI!FTRawMatColorNameEN.ToString

                                                Exit For

                                            Next

                                        Catch ex As Exception
                                        End Try

                                        _dtRawMatColor.Dispose()

                                        _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                                        _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                                        _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FTOGacDate)"
                                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_PoDocNo) & "' "
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                        _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                                        _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                                        _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                                        _Str &= vbCrLf & ",0 "
                                        _Str &= vbCrLf & ",0 "
                                        _Str &= vbCrLf & "," & _PurQty & " "
                                        _Str &= vbCrLf & "," & _PurAmount & " "
                                        _Str &= vbCrLf & ",'' "
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OGACDate) & "'"

                                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                                        TotalPoQty = CDbl(Format((TotalPoQty - _PurQty), HI.ST.Config.QtyFormat))
                                        TotalPoAmt = CDbl(Format((TotalPoAmt - _PurAmount), HI.ST.Config.AmtFormat))

                                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing SET "
                                        _Str &= vbCrLf & " FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoDocNo) & "' "
                                        _Str &= vbCrLf & " , FTStateAutoPO='1'"
                                        _Str &= vbCrLf & ", FDStateAutoDate=" & HI.UL.ULDate.FormatDateDB & " "
                                        _Str &= vbCrLf & ", FTStateAutoTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                        _Str &= vbCrLf & ", FTStateAutoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Str &= vbCrLf & "  WHERE     (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "') "
                                        _Str &= vbCrLf & " AND (FNHSysRawMatId = " & RaeMatID & ") "
                                        _Str &= vbCrLf & " AND FTPurchaseNo=''  AND FNTotalPurchaseQuantity>0 "
                                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                                        R!FNStateAdd = 0

                                    Next

                                Next

                                Call RoudUpPOAmount(_PoDocNo, DiscountPer, Vat, SurCharge)

                                Me._TmpPO.Rows.Add("1", SuplCode, SuplName, _PoDocNo)
                            Next



                        Next
                    Catch ex As Exception

                    End Try



                    _DtSplitPOItemSub.Dispose()
                End If
                _DtSplitPO.Dispose()
            End If



            _DtItem.BeginInit()

            For Each Row As DataRow In _DtItem.Select(Filter & "  AND FTSelect='1'  ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode")
                Row.Delete()
            Next

            _DtItem.EndInit()

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub RemoveItem(SuplKey As String)

        _DtItemSub.BeginInit()

        For Each Rm As DataRow In _DtItem.Select(" FNHSysSuplId=" & Val(SuplKey) & " AND FTSelect='1' ")
            For Each Row As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & Integer.Parse(Rm!FNHSysRawMatId.ToString) & " ")
                Row.Delete()
            Next
        Next
      
        _DtItemSub.EndInit()

        _ListPurchaseOrder(0).BeginInit()

        For Each Rm As DataRow In _DtItem.Select(" FNHSysSuplId=" & Val(SuplKey) & " AND FTSelect='1' ")
            For Each Row As DataRow In _ListPurchaseOrder(0).Select("FNHSysSuplId=" & Val(SuplKey) & " AND FNHSysRawMatId=" & Integer.Parse(Rm!FNHSysRawMatId.ToString) & " ")
                Row.Delete()
            Next
        Next

        _ListPurchaseOrder(0).EndInit()

        _DtItemSub.AcceptChanges()

        _DtItem.BeginInit()
        For Each Row As DataRow In _DtItem.Select(" FNHSysSuplId=" & Val(SuplKey) & " AND FTSelect='1' ")
            Row.Delete()
        Next

        _DtItem.EndInit()
        _DtItem.AcceptChanges()

        '_DtSupl.BeginInit()

        'For Each Row As DataRow In _DtSupl.Select(" FNHSysSuplId=" & Val(SuplKey) & " ")
        '    Row.Delete()
        'Next

        '_DtSupl.EndInit()
        '_DtSupl.AcceptChanges()

        'For Each Row As DataRow In _DtSupl.Select("FNHSysSuplId=" & Val(SuplKey) & " ")
        '    Row.Delete()
        'Next
        '_DtSupl.AcceptChanges()

    End Sub

    Private Function ValidateSuplierData(Optional AlertMsg As Boolean = True) As Boolean
        Dim _StatePass As Boolean = True
        Dim _StateSupl As String = ""
        Dim _Str As String = ""

        Try
            With ogvlistsupplier
                CType(Me.ogclistsupplier.DataSource, DataTable).AcceptChanges()

                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    Return False

                End If

                Dim _Exc As Double = 0

                If AlertMsg Then
                    For Each R As DataRow In CType(Me.ogclistsupplier.DataSource, DataTable).Select("FTSelect='1'")

                        _Str = " SELECT  (CASE WHEN A.FTStateLocal ='1' THEN 1 ELSE"
                        _Str &= vbCrLf & " ISNULL((SELECT TOP 1 FNBuyingRate"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                        _Str &= vbCrLf & "   WHERE  (FDDate = Convert(nvarchar(10),GetDate(),111) )"
                        _Str &= vbCrLf & "   AND (FNHSysCurId = A.FNHSysCurId) ),0) END ) AS  FNExchangeRate "
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS A  WITH(NOLOCK) "
                        _Str &= vbCrLf & " WHERE A.FNHSysCurId=" & Integer.Parse(Val(R!FNHSysCurId_Hide.ToString)) & " "

                        _Exc = Double.Parse(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "0"))
                        R!FNExchangeRate = _Exc

                    Next

                End If

                For Each R As DataRow In CType(Me.ogclistsupplier.DataSource, DataTable).Select("FTSelect='1'")

                    Dim _Supl As String = R!FNHSysSuplId.ToString
                    Dim _PoType As String = R!FNPoState.ToString  '"" & .GetFocusedRowCellValue("FNPoState").ToString
                    Dim _PoGrp As String = R!FNHSysPurGrpId_Hide.ToString '"" & .GetFocusedRowCellValue("FNHSysPurGrpId_Hide").ToString
                    Dim _PoDeliveryDate As String = R!FDDeliveryDate.ToString '"" & .GetFocusedRowCellValue("FDDeliveryDate").ToString
                    Dim _LeadTime As Integer = Integer.Parse(Val(R!FNLeadtime.ToString)) ' Val("" & .GetFocusedRowCellValue("FNLeadtime").ToString)
                    Dim _Cur As String = R!FNHSysCurId_Hide.ToString '"" & .GetFocusedRowCellValue("FNHSysCurId_Hide").ToString
                    _Exc = Val(R!FNExchangeRate.ToString) 'Val("" & .GetFocusedRowCellValue("FNExchangeRate").ToString)
                    Dim _PT As Integer = Val(R!FNPoState_Hide.ToString) 'Val("" & .GetFocusedRowCellValue("FNPoState_Hide").ToString)
                    Dim _PoGrpText As String = R!FNHSysPurGrpId.ToString

                    If _Supl <> "" And _Supl.Length > 1 And _PoType <> "" And _PoGrp <> "" And _PoGrp.Length > 1 And _PoDeliveryDate <> "" And _Cur <> "" And _Cur.Length > 1 And _Exc > 0 And _PT >= 0 And _LeadTime >= 0 And _PoGrpText <> "" Then
                        R!FTStateComplete = "1"

                        For Each Row As DataRow In _DtSupl.Select("FNHSysSuplId=" & Val(_Supl) & "")
                            Row!FTStateComplete = "1"
                        Next

                    Else
                        R!FTStateComplete = "0"
                        _StatePass = False
                        If _StateSupl = "" Then
                            _StateSupl = R!FTSuplCode.ToString
                        Else
                            _StateSupl = _StateSupl & vbCrLf & R!FTSuplCode.ToString
                        End If

                        For Each Row As DataRow In _DtSupl.Select("FNHSysSuplId=" & Val(_Supl) & "")
                            Row!FTStateComplete = "0"
                        Next

                    End If
                Next

                CType(Me.ogclistsupplier.DataSource, DataTable).AcceptChanges()
                _DtSupl.AcceptChanges()

                If _StateSupl <> "" Then

                    If (AlertMsg) Then
                        HI.MG.ShowMsg.mInfo("กรุณาทำการตรวจสอบข้อมูล Supplier ให้ครบ", 1405260004, Me.Text, _StateSupl, MessageBoxIcon.Warning)
                    End If

                    _StatePass = False
                End If
                Return _StatePass
            End With
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

    Private Sub ogvlistsupplier_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvlistsupplier.FocusedRowChanged
        Try

            With ogvlistsupplier
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    Me.ogcdetail.DataSource = Nothing
                    Exit Sub
                End If

                Dim _Supl As String = "" & .GetFocusedRowCellValue("FNHSysSuplId").ToString
                Dim dt As DataTable = _DtItem.Clone
                dt = _DtItem.Select("FNHSysSuplId=" & Val(_Supl) & " ").CopyToDataTable()

                Me.ogcdetail.DataSource = dt.Copy

            End With

        Catch ex As Exception
        End Try
    End Sub


    Private Sub ogvlistsupplier_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvlistsupplier.RowCellStyle
        With Me.ogvlistsupplier

            If Me.ogclistsupplier.DataSource Is Nothing Then
                Exit Sub
            End If

            Try

                If "" & .GetRowCellValue(e.RowHandle, "FNStateCurP").ToString <> "1" Or "" & .GetRowCellValue(e.RowHandle, "FNStatePriceP").ToString <> "1" Or "" & .GetRowCellValue(e.RowHandle, "FNStateUnitP").ToString <> "1" Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                ElseIf "" & .GetRowCellValue(e.RowHandle, "FTStateComplete").ToString = "1" Then
                    e.Appearance.ForeColor = System.Drawing.Color.Green
                End If

            Catch ex As Exception
            End Try

        End With
    End Sub

    Private Sub ogvsupl_BeforeLeaveRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles ogvlistsupplier.BeforeLeaveRow
        Call ValidateSuplierData(False)
    End Sub

    Private Sub wAutoGeneratePO_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            HI.TL.HandlerControl.ClearControl(ogbheader)         
        Catch ex As Exception
        End Try

        Try
            _DtSupl.Dispose()
        Catch ex As Exception
        End Try

        Try
            _DtJob.Dispose()          
        Catch ex As Exception
        End Try

        Try
            _DtItem.Dispose()         
        Catch ex As Exception
        End Try

        Try
            _DtItemSub.Dispose()

        Catch ex As Exception
        End Try

        Try
            _TmpPO.Dispose()
        Catch ex As Exception
        End Try

        Try
            _ListPurchaseOrder = Nothing        
        Catch ex As Exception
        End Try

        Try
            _ListPurchaseOrderSupl = Nothing          
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmgenerate_Click(sender As System.Object, e As System.EventArgs) Handles ocmgenerate.Click
    
        If ValidateSuplierData() Then
            If (Me.ValidateData) Then
                If HI.MG.ShowMsg.mConfirmProcess("Auto Generate PO", 1000000035) = True Then
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

                    Dim Spls As New HI.TL.SplashScreen("Generate Auto Po..  Please Wait...")

                    Try
                        With Me.ogvdetail
                            .FocusedColumn = .Columns.ColumnByFieldName("FTRawMatCode")
                        End With
                    Catch ex As Exception
                    End Try

                    Try
                        CType(ogcdetail.DataSource, DataTable).AcceptChanges()
                    Catch ex As Exception
                    End Try

                    Try
                        _RunDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_PUR))

                        _TmpPO.Rows.Clear()

                        Call Me.GeneratePOAuto(Spls)

                        Spls.UpdateTitle("Generate Po Complete...")
                        Spls.UpdateInformation("")

                        'Me.PrepareDataGenerate()

                        If _TmpPO.Rows.Count > 0 Then
                            Dim _Qry As String = ""
                            _TmpPO.BeginInit()

                            For Each Rx As DataRow In _TmpPO.Rows

                                Dim dtdata As New DataTable

                                _Qry = "SELECT [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_ItemInfo('" & HI.UL.ULF.rpQuoted(Rx!FTPurchaseNo.ToString) & "') AS FTItemRef"
                                _Qry &= vbCrLf & ",ISNULL(D.FTDeliveryCode,'') AS FTDeliveryCode ,ISNULL(D.FTDeliveryDescEN,'') AS FTDeliveryName"
                                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS X WITH(NOLOCK) "
                                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery As D With(NOLOCK) On X.FNHSysDeliveryId =D.FNHSysDeliveryId "
                                _Qry &= vbCrLf & " WHERE X.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Rx!FTPurchaseNo.ToString) & "'"

                                dtdata = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                For Each Rxd As DataRow In dtdata.Rows

                                    Rx!FTItemRef = Rxd!FTItemRef.ToString
                                    Rx!FTDeliveryCode = Rxd!FTDeliveryCode.ToString
                                    Rx!FTDeliveryName = Rxd!FTDeliveryName.ToString

                                    Exit For

                                Next

                                Try
                                    dtdata.Dispose()
                                Catch ex As Exception
                                End Try

                            Next

                            _TmpPO.EndInit()
                        End If
                        Spls.Close()

                        Cursor.Current = Cursors.Default

                        If _TmpPO.Rows.Count > 0 Then
                            HI.MG.ShowMsg.mInfo("", 1000000036, Me.Text)

                            With _lstPo
                                .ogclist.DataSource = _TmpPO.Copy
                                .ShowDialog()
                            End With

                            ' Add any initialization after the InitializeComponent() call.
                            'Dim _Spls As New HI.TL.SplashScreen("Prepare Data For Auto Purchase Order Please Wait...")

                            Try
                                _DtJob.BeginInit()
                                For Each Row As DataRow In _DtJob.Select(" FTStateSelect='1' ")

                                    If _DtItemSub.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(Row!FTOrderNo.ToString) & "' ").Length <= 0 Then

                                        Row.Delete()

                                    End If

                                Next

                                _DtJob.EndInit()
                                _DtJob.AcceptChanges()
                            Catch ex As Exception
                            End Try

                            Try
                                ogvlistsupplier_FocusedRowChanged(ogvlistsupplier, New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, ogvlistsupplier.FocusedRowHandle))
                            Catch ex As Exception
                            End Try

                            ' _Spls.Close()
                        End If

                        _TmpPO.Rows.Clear()
                    Catch ex As Exception
                        Spls.Close()
                    End Try

                End If
            End If
        End If
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        Try
            With ogvdetail
                If Not (ogcdetail.DataSource Is Nothing) And ogvlistsupplier.RowCount > 0 Then

                    With ogvdetail

                        If .FocusedRowHandle < 0 Then Exit Sub
                        '  If .FocusedColumn.FieldName.ToString <> "FTUnitCode" Then Exit Sub

                        Dim _FTUnitCode As String = ("" & .GetRowCellValue(.FocusedRowHandle, "FTUnitCode").ToString)
                        Dim _FNHSysUnitId As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitId").ToString))

                        Dim _FNHSysCurCode As String = ("" & ogvlistsupplier.GetRowCellValue(ogvlistsupplier.FocusedRowHandle, "FNHSysCurId").ToString)
                        Dim _FNHSysCurId As Integer = Integer.Parse(Val("" & ogvlistsupplier.GetRowCellValue(ogvlistsupplier.FocusedRowHandle, "FNHSysCurId_Hide").ToString))

                        Dim _SysSuplID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
                        Dim _SysMatID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString))

                        Dim _Qry As String = ""


                        If _FTUnitCode <> "" And _FNHSysUnitId > 0 Then

                            For Each R As DataRow In _DtItemSub.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")
                                _FNHSysCurCode = R!FTCurCode.ToString
                                _FNHSysCurId = Val(R!FNHSysCurIdChk.ToString)
                            Next

                            With _wChangeUnitAndCurrency

                                .ProcOK = False

                                .FNHSysCurId.Text = _FNHSysCurCode
                                .FNHSysCurId.Properties.Tag = _FNHSysCurId.ToString

                                .FNHSysUnitIdPO.Text = _FTUnitCode
                                .FNHSysUnitIdPO.Properties.Tag = _FNHSysUnitId.ToString
                                .ocmchange.Enabled = True
                                .ocmcancel.Enabled = True
                                .ShowDialog()

                                If .ProcOK = True Then

                                    Dim _FTUnitCodeNew As String = .FNHSysUnitIdPO.Text
                                    Dim _FNHSysUnitIdNew As Integer = Integer.Parse(Val(.FNHSysUnitIdPO.Properties.Tag.ToString))

                                    Dim _FNHSysCurCodeNew As String = .FNHSysCurId.Text
                                    Dim _FNHSysCurIdNew As Integer = Integer.Parse(Val(.FNHSysCurId.Properties.Tag.ToString))

                                    'If _FNHSysCurId <> _FNHSysCurIdNew Then
                                    '    ogvlistsupplier.SetRowCellValue(ogvlistsupplier.FocusedRowHandle, "FNHSysCurId", _FNHSysCurCodeNew)
                                    '    ogvlistsupplier.SetRowCellValue(ogvlistsupplier.FocusedRowHandle, "FNHSysCurId_Hide", _FNHSysCurIdNew)
                                    'End If

                                    With ogvdetail
                                        .SetRowCellValue(.FocusedRowHandle, "FTUnitCode", _FTUnitCodeNew)
                                        .SetRowCellValue(.FocusedRowHandle, "FNHSysUnitId", _FNHSysUnitIdNew)

                                        _DtItem.BeginInit()
                                        For Each R As DataRow In _DtItem.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")
                                            R!FTUnitCode = _FTUnitCodeNew
                                            R!FNHSysUnitId = _FNHSysUnitIdNew
                                        Next
                                        _DtItem.EndInit()

                                        _DtItemSub.BeginInit()

                                        For Each R As DataRow In _DtItemSub.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")

                                            R!FTUnitCode = _FTUnitCodeNew
                                            R!FNHSysUnitId = _FNHSysUnitIdNew
                                            R!FNHSysUnitIdPurchaseChk = _FNHSysUnitIdNew
                                            R!FTCurCode = _FNHSysCurCodeNew
                                            R!FNHSysCurIdChk = _FNHSysCurIdNew

                                            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                            _Qry &= vbCrLf & " SET FNHSysUnitIdPurchase =" & _FNHSysUnitIdNew & " "
                                            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                            _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(_SysMatID)) & " "
                                            _Qry &= vbCrLf & " AND   FNHSysSuplId=" & Integer.Parse(Val(_SysSuplID)) & ""
                                            _Qry &= vbCrLf & " AND ISNULL(FTPurchaseNo,'')='' "
                                            _Qry &= vbCrLf & " AND   FNHSysUnitIdPurchase<>" & Integer.Parse(_FNHSysUnitIdNew) & ""
                                            _Qry &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                            _Qry &= vbCrLf & " SET FNHSysCurId =" & _FNHSysCurIdNew & " "
                                            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                            _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(_SysMatID)) & " "
                                            _Qry &= vbCrLf & " AND   FNHSysSuplId=" & Integer.Parse(Val(_SysSuplID)) & ""
                                            _Qry &= vbCrLf & " AND ISNULL(FTPurchaseNo,'')='' "
                                            _Qry &= vbCrLf & " AND   FNHSysCurId<>" & Integer.Parse(_FNHSysCurIdNew) & ""

                                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                        Next

                                        _DtItemSub.EndInit()
                                    End With


                                End If

                            End With

                        End If

                    End With

                    Dim _RowIndx As Integer = ogvlistsupplier.FocusedRowHandle

                    Call CheckSupldata()

                    Me.ogclistsupplier.DataSource = _DtSupl
                    Me.ogcdetail.DataSource = _DtItem.DefaultView

                    Try
                        ogvlistsupplier_FocusedRowChanged(ogvlistsupplier, New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, _RowIndx))
                    Catch ex As Exception
                    End Try

                    CType(.DataSource, DataTable).AcceptChanges()

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wAutoGeneratePO_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown, ogvlistorder.KeyDown, ogvlistsupplier.KeyDown, ogvdetail.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub wAutoGeneratePO_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FTStateColorBox.Checked = False
            ogvlistorder.Columns.ColumnByFieldName("FTStateSelect").Width = 50
            Me.ocmrefresh.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ockselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectall.Checked Then
                _State = "1"
            End If

            With ogclistsupplier
                If Not (.DataSource Is Nothing) And ogvlistsupplier.RowCount > 0 Then

                    With ogvlistsupplier
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTStateOrderSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTStateOrderSelect.EditValueChanging

        Try
            Call RemoveOrder(ogvlistorder.GetFocusedRowCellValue("FTOrderNo"), (e.NewValue.ToString = "1"))

        Catch ex As Exception
        End Try

    End Sub

    Private Sub CheckSupldata()

        _DtSupl.BeginInit()

        Try
            Dim _SuplKey As Integer = 0
            Dim FNStateCurP, FNStatePriceP, FNStateUnitP As Integer
            Dim FNStateCurPItem, FNStatePricePItem, FNStateUnitPItem As Integer

            Dim FNHSysUnitIdPurchaseChk As Integer = -1
            Dim FNPricePurchaseChk As Double = -1
            Dim FNHSysCurIdChk As Integer = -1
            Dim _SysItemKey As Integer = 0

            _DtSupl.BeginInit()
            For Each Row As DataRow In _DtSupl.Rows
                _SuplKey = Integer.Parse(Val(Row!FNHSysSuplId.ToString))

                FNStateCurP = 1
                FNStatePriceP = 1
                FNStateUnitP = 1
                'FNHSysCurIdChk = -1

                FNHSysCurIdChk = Integer.Parse(Val(Row!FNHSysCurId_Hide.ToString))

                For Each Rx As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(_SuplKey) & "  ", "FNHSysRawMatId")
                    If FNHSysCurIdChk = -1 Then
                        FNHSysCurIdChk = Integer.Parse(Val(Rx!FNHSysCurIdChk.ToString))
                    End If

                    If FNHSysCurIdChk <> Integer.Parse(Val(Rx!FNHSysCurIdChk.ToString)) Then
                        FNStateCurP = FNStateCurP + 1
                        FNHSysCurIdChk = Integer.Parse(Val(Rx!FNHSysCurIdChk.ToString))
                    End If
                Next

                Row!FNStateCurP = FNStateCurP
                _DtItem.BeginInit()
                For Each Rx3 As DataRow In _DtItem.Select("FNHSysSuplId=" & Val(_SuplKey) & "  ", "FNHSysRawMatId")
                    _SysItemKey = Integer.Parse(Val(Rx3!FNHSysRawMatId.ToString))
                    FNPricePurchaseChk = -1
                    FNHSysUnitIdPurchaseChk = -1
                    FNHSysCurIdChk = -1

                    FNStateCurPItem = 1
                    FNStatePricePItem = 1
                    FNStateUnitPItem = 1

                    For Each Rx As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(_SuplKey) & "  AND FNHSysRawMatId=" & Val(_SysItemKey) & " ", "FTOrderNo")

                        If FNHSysCurIdChk = -1 Then

                            FNHSysCurIdChk = Integer.Parse(Val(Rx!FNHSysCurIdChk.ToString))

                        End If

                        If FNHSysCurIdChk <> Integer.Parse(Val(Rx!FNHSysCurIdChk.ToString)) Then

                            FNStateCurPItem = FNStateCurPItem + 1
                            FNHSysCurIdChk = Integer.Parse(Val(Rx!FNHSysCurIdChk.ToString))

                        End If

                        If FNPricePurchaseChk = -1 Then

                            FNPricePurchaseChk = Double.Parse(Val(Rx!FNPricePurchaseChk.ToString))

                        End If

                        If FNPricePurchaseChk <> Double.Parse(Val(Rx!FNPricePurchaseChk.ToString)) Then

                            FNStatePriceP = FNStatePriceP + 1
                            FNStatePricePItem = FNStatePricePItem + 1
                            FNPricePurchaseChk = Double.Parse(Val(Rx!FNPricePurchaseChk.ToString))

                        End If

                        If FNHSysUnitIdPurchaseChk = -1 Then
                            FNHSysUnitIdPurchaseChk = Integer.Parse(Val(Rx!FNHSysUnitIdPurchaseChk.ToString))
                        End If

                        If FNHSysUnitIdPurchaseChk <> Integer.Parse(Val(Rx!FNHSysUnitIdPurchaseChk.ToString)) Then
                            FNStateUnitP = FNStateUnitP + 1
                            FNStateUnitPItem = FNStateUnitPItem + 1

                            FNHSysUnitIdPurchaseChk = Integer.Parse(Val(Rx!FNHSysUnitIdPurchaseChk.ToString))
                        End If

                    Next

                    Rx3!FNStateCurP = FNStateCurPItem
                    Rx3!FNStatePriceP = FNStatePricePItem
                    Rx3!FNStateUnitP = FNStateUnitPItem

                Next
                _DtItem.EndInit()

                Row!FNStatePriceP = FNStatePriceP
                Row!FNStateUnitP = FNStateUnitP

            Next
            _DtSupl.EndInit()

        Catch ex As Exception

        End Try

        _DtSupl.EndInit()
    End Sub

    Private Sub RemoveOrder(FTOrderNo As String, StateAdd As Boolean)
        Try
            Dim _SuplKey As Integer = 0
            For Each R As DataRow In _ListPurchaseOrder(0).Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' ")
                _SuplKey = Integer.Parse(Val(R!FNHSysSuplId.ToString))
                ' SC.FTOrderNo,M.FNHSysSuplId, SC.FNHSysRawMatId
                If StateAdd Then
                    _DtItemSub.BeginInit()

                    If _DtItemSub.Select("FNHSysSuplId=" & Val(_SuplKey) & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " ").Length <= 0 Then
                        _DtItemSub.ImportRow(R)
                    End If

                    _DtItemSub.EndInit()
                    _DtItemSub.AcceptChanges()
                    _DtItem.BeginInit()

                    If _DtItem.Select(" FNHSysSuplId=" & Val(_SuplKey) & "  AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & "  ").Length > 0 Then
                        For Each Row As DataRow In _DtItem.Select(" FNHSysSuplId=" & Val(_SuplKey) & "  AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & "  ")
                            Row!FNQuantity = Row!FNQuantity + R!FNQuantity
                            Row!FNNetAmt = Row!FNNetAmt + R!FNNetAmt
                        Next
                    Else
                        _DtItem.Rows.Add(R!FNHSysSuplId, R!FNHSysRawMatId, R!FNHSysUnitId, R!FTFabricFrontSize, R!FNQuantity, R!FNPrice, R!FNNetAmt, R!FTRawMatCode, R!FTMatDesc, R!FTRawMatColorCode, R!FTRawMatSizeCode, R!FTUnitCode, "1", R!FNPrice)
                    End If

                    _DtItem.EndInit()
                    _DtItem.AcceptChanges()

                    If _DtSupl.Select(" FNHSysSuplId=" & Val(_SuplKey) & "  ").Length <= 0 Then
                        _DtSupl.BeginInit()

                        For Each Rx As DataRow In _ListPurchaseOrderSupl(0).Select(" FNHSysSuplId=" & Val(_SuplKey) & "  ")
                            _DtSupl.ImportRow(Rx)
                        Next

                        _DtSupl.EndInit()
                        _DtSupl.AcceptChanges()

                    End If

                Else

                    _DtItemSub.BeginInit()
                    For Each Row As DataRow In _DtItemSub.Select("FNHSysSuplId=" & Val(_SuplKey) & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " ")
                        Row.Delete()
                    Next
                    _DtItemSub.EndInit()
                    _DtItemSub.AcceptChanges()

                    If _SuplKey = 140506070 Then
                        Beep()
                    End If

                    _DtItem.BeginInit()
                    For Each Row As DataRow In _DtItem.Select(" FNHSysSuplId=" & Val(_SuplKey) & "  AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & "  ")
                        Row!FNQuantity = Row!FNQuantity - R!FNQuantity
                        Row!FNNetAmt = Row!FNNetAmt - R!FNNetAmt
                    Next
                    _DtItem.EndInit()
                    _DtItem.AcceptChanges()

                End If
            Next

            If Not (StateAdd) Then

                For Each R As DataRow In _ListPurchaseOrder(0).Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' ")
                    _SuplKey = Integer.Parse(Val(R!FNHSysSuplId.ToString))

                    If _DtItemSub.Select("FNHSysSuplId=" & Val(_SuplKey) & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " ").Length <= 0 Then

                        _DtItem.BeginInit()
                        For Each Row As DataRow In _DtItem.Select("FNHSysSuplId=" & Val(_SuplKey) & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " ")
                            Row.Delete()
                        Next
                        _DtItem.EndInit()
                        _DtItem.AcceptChanges()
                    End If

                    If _DtItem.Select("FNHSysSuplId=" & Val(_SuplKey) & "  ").Length <= 0 Then
                        _DtSupl.BeginInit()
                        For Each Row As DataRow In _DtSupl.Select(" FNHSysSuplId=" & Val(_SuplKey) & " ")
                            Row.Delete()
                        Next
                        _DtSupl.EndInit()
                        _DtSupl.AcceptChanges()

                    End If

                Next
            End If

            Call CheckSupldata()

            _DtItem.Rows.Clear()

            For Each R As DataRow In _DtItemSub.Rows
                _SuplKey = Integer.Parse(Val(R!FNHSysSuplId.ToString))

                If _DtItem.Select(" FNHSysSuplId=" & Val(_SuplKey) & "  AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & "  ").Length > 0 Then
                    For Each Row As DataRow In _DtItem.Select(" FNHSysSuplId=" & Val(_SuplKey) & "  AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & "  ")
                        Row!FNQuantity = Row!FNQuantity + R!FNQuantity
                        Row!FNNetAmt = Row!FNNetAmt + R!FNNetAmt
                    Next
                Else
                    _DtItem.Rows.Add(R!FNHSysSuplId, R!FNHSysRawMatId, R!FNHSysUnitId, R!FTFabricFrontSize, R!FNQuantity, R!FNPrice, R!FNNetAmt, R!FTRawMatCode, R!FTMatDesc, R!FTRawMatColorCode, R!FTRawMatSizeCode, R!FTUnitCode, "1", R!FNPrice)
                End If

            Next

            Dim FNQuantity As Double = 0
            Dim FNPrice As Double = 0
            Dim FNNetAmt As Double = 0
            _DtItem.BeginInit()

            For Each R As DataRow In _DtItem.Rows

                FNQuantity = Double.Parse(Val(R!FNQuantity.ToString))

                If FNQuantity Mod 1 > 0 Then
                    FNQuantity = (FNQuantity - (FNQuantity Mod 1)) + 1
                End If

                FNPrice = Double.Parse(Val(R!FNPrice.ToString))
                FNNetAmt = Double.Parse(Format(FNQuantity * FNPrice, "0.00"))

                R!FNQuantity = FNQuantity
                R!FNNetAmt = FNNetAmt
            Next

            _DtItem.EndInit()

            Call CheckSupldata()

            Me.ogclistsupplier.DataSource = _DtSupl
            Me.ogcdetail.DataSource = _DtItem.DefaultView

            Try
                ogvlistsupplier_FocusedRowChanged(ogvlistsupplier, New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, ogvlistsupplier.FocusedRowHandle))
            Catch ex As Exception
            End Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ockselectallorder_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectallorder.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectallorder.Checked Then
                _State = "1"
            End If

            With ogclistorder
                If Not (.DataSource Is Nothing) And ogvlistorder.RowCount > 0 Then

                    With ogvlistorder
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSelect"), _State)

                            Try
                                Call RemoveOrder(ogvlistorder.GetRowCellValue(I, "FTOrderNo"), (_State = "1"))
                            Catch ex As Exception
                            End Try

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogclistsupplier_Click(sender As Object, e As EventArgs) Handles ogclistsupplier.Click

    End Sub

    Private Sub ReposGDFTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposGDFTSelect.EditValueChanging

        With Me.ogvdetail
            If .FocusedRowHandle < 0 Then Exit Sub

            Dim _SysSuplID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
            Dim _SysMatID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString))

            _DtItem.BeginInit()
            For Each R As DataRow In _DtItem.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")
                If e.NewValue.ToString = "1" Then
                    R!FTSelect = "1"
                Else
                    R!FTSelect = "0"
                End If
            Next
            _DtItem.EndInit()

        End With

    End Sub

    Private Sub ockselectdelatilall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectdelatilall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectdelatilall.Checked Then
                _State = "1"
            End If

            With ogvdetail
                If Not (.DataSource Is Nothing) And ogvlistsupplier.RowCount > 0 Then

                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)

                            Dim _SysSuplID As Integer = Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysSuplId").ToString))
                            Dim _SysMatID As Integer = Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysRawMatId").ToString))

                            _DtItem.BeginInit()
                            For Each R As DataRow In _DtItem.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")
                                R!FTSelect = _State
                            Next
                            _DtItem.EndInit()

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogclistorder_Click(sender As Object, e As EventArgs) Handles ogclistorder.Click

    End Sub

    Private Sub ReposFNPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNPrice.EditValueChanging
        Try
            With ogvdetail
                If Not (ogcdetail.DataSource Is Nothing) And ogvlistsupplier.RowCount > 0 Then

                    With ogvdetail
                        Dim _FNPrice As Double = 0
                        Dim _FNAmt As Double = 0
                        If IsNumeric(e.NewValue) Then
                            _FNPrice = CDbl(Format(Val(e.NewValue), HI.ST.Config.PriceFormat))
                        End If

                        Dim _FNFNQuantity As Double = Double.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString))
                        _FNAmt = CDbl(Format(_FNPrice * _FNFNQuantity, HI.ST.Config.AmtFormat))

                        .SetRowCellValue(.FocusedRowHandle, "FNNetAmt", _FNAmt)

                        Dim _SysSuplID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
                        Dim _SysMatID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString))

                        _DtItem.BeginInit()
                        For Each R As DataRow In _DtItem.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")
                            R!FNNetAmt = _FNAmt
                        Next
                        _DtItem.EndInit()

                    End With

                    CType(.DataSource, DataTable).AcceptChanges()

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReposFNPrice_Leave(sender As Object, e As EventArgs) Handles ReposFNPrice.Leave
        Try
            With ogvdetail
                If Not (.DataSource Is Nothing) And ogvlistsupplier.RowCount > 0 Then

                    With ogvdetail
                        Dim _FNPrice As Double = 0

                        If IsNumeric(sender.Value) Then
                            _FNPrice = CDbl(Format(sender.Value, HI.ST.Config.PriceFormat))
                        End If
                        Dim _FNPriceOld As Double = Double.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNPrice").ToString))
                        Dim _FNPriceNew As Double = Double.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNPriceOld").ToString))

                        Dim _SysSuplID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
                        Dim _SysMatID As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString))

                        Dim _Qry As String = ""

                        If _FNPrice <> _FNPriceNew Or _DtItemSub.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & " AND FNPricePurchaseChk<>" & _FNPriceNew & "").Length > 0 Then
                            .SetRowCellValue(.FocusedRowHandle, "FNPrice", _FNPrice)
                            .SetRowCellValue(.FocusedRowHandle, "FNPriceOld", _FNPrice)


                            _DtItem.BeginInit()
                            For Each R As DataRow In _DtItem.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")
                                R!FNPrice = _FNPrice
                                R!FNPriceOld = _FNPrice
                            Next
                            _DtItem.EndInit()

                            _DtItemSub.BeginInit()

                            For Each R As DataRow In _DtItemSub.Select("FNHSysSuplId=" & _SysSuplID & " AND FNHSysRawMatId=" & _SysMatID & "")
                                R!FNPrice = _FNPrice
                                R!FNPricePurchaseChk = _FNPrice

                                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                _Qry &= vbCrLf & " SET FNPricePurchase =" & _FNPrice & " "
                                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(_SysMatID)) & " "
                                _Qry &= vbCrLf & " AND   FNHSysSuplId=" & Integer.Parse(Val(_SysSuplID)) & ""
                                _Qry &= vbCrLf & " AND ISNULL(FTPurchaseNo,'')='' "
                                _Qry &= vbCrLf & " AND   FNPricePurchase<>" & Double.Parse(_FNPrice) & ""

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                            Next

                            _DtItemSub.EndInit()
                        End If

                    End With

                    Dim _RowIndx As Integer = ogvlistsupplier.FocusedRowHandle

                    Call CheckSupldata()

                    Me.ogclistsupplier.DataSource = _DtSupl
                    Me.ogcdetail.DataSource = _DtItem.DefaultView

                    Try
                        ogvlistsupplier_FocusedRowChanged(ogvlistsupplier, New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, _RowIndx))
                    Catch ex As Exception
                    End Try

                    CType(.DataSource, DataTable).AcceptChanges()

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private lastRowHandle As Integer = GridControl.InvalidRowHandle
    Private lastColumn As GridColumn = Nothing
    Private ttInfo As ToolTipControlInfo = Nothing

    Private Sub objToolTipController_GetActiveObjectInfo(sender As Object, e As ToolTipControllerGetActiveObjectInfoEventArgs) Handles objToolTipController.GetActiveObjectInfo
        Try
            If e.Info Is Nothing AndAlso e.SelectedControl Is ogcdetail Then
                Dim view As GridView = TryCast(ogcdetail.FocusedView, GridView)
                Dim info As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)

                If info.InRowCell AndAlso (info.RowHandle <> lastRowHandle OrElse info.Column IsNot lastColumn) Then
                    lastRowHandle = info.RowHandle
                    lastColumn = info.Column
                    Dim text As String = view.GetRowCellDisplayText(info.RowHandle, info.Column)
                    Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()
                    text = ""

                    Dim _FNHSysSuplId As Integer = Integer.Parse(Val("" & view.GetRowCellValue(info.RowHandle, "FNHSysSuplId")))
                    Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & view.GetRowCellValue(info.RowHandle, "FNHSysRawMatId")))

                    For Each R As DataRow In _DtItemSub.Select("FNHSysSuplId=" & _FNHSysSuplId & " AND FNHSysRawMatId =" & _FNHSysRawMatId & "")

                        If text = "" Then
                            text = " Order : " & R!FTOrderNo.ToString & "    Unit : " & R!FTUnitCode.ToString & "    Price : " & Format(Double.Parse(R!FNPrice), HI.ST.Config.PriceFormat) & "    Quantity : " & Format(Double.Parse(R!FNQuantity), HI.ST.Config.QtyFormat) & "    Currency : " & R!FTCurCode.ToString
                        Else
                            text &= vbCrLf & " Order : " & R!FTOrderNo.ToString & "    Unit : " & R!FTUnitCode.ToString & "    Price : " & Format(Double.Parse(R!FNPrice), HI.ST.Config.PriceFormat) & "    Quantity : " & Format(Double.Parse(R!FNQuantity), HI.ST.Config.QtyFormat) & "    Currency : " & R!FTCurCode.ToString
                        End If

                    Next

                    ttInfo = New DevExpress.Utils.ToolTipControlInfo(cellKey, text)

                End If

                If ttInfo IsNot Nothing Then
                    e.Info = ttInfo
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            With Me.ogvdetail

                If Me.ogcdetail.DataSource Is Nothing Then
                    Exit Sub
                End If

                Try

                    If "" & .GetRowCellValue(e.RowHandle, "FNStateCurP").ToString <> "1" Or "" & .GetRowCellValue(e.RowHandle, "FNStatePriceP").ToString <> "1" Or "" & .GetRowCellValue(e.RowHandle, "FNStateUnitP").ToString <> "1" Then
                        e.Appearance.ForeColor = System.Drawing.Color.Red
                    End If

                Catch ex As Exception
                End Try

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Dim _Spls As New HI.TL.SplashScreen("Prepare Data For Auto Purchase Order Please Wait...")

        FTStateColorBox.Checked = False

        Try
            HI.TL.HandlerControl.ClearControl(ogbheader)
        Catch ex As Exception
        End Try

        Call PrepareDataGenerate()
        _Spls.Close()
    End Sub


    Private Sub RoudUpPOAmount(PoKey As String, DiscountPer As Decimal, Vat As Decimal, SurCharge As Decimal)
        Dim _Str As String
        Dim _dtdiff As DataTable
        Dim _FNSurchangeAmt As Double
        Dim PoAmt As Decimal = 0.0
        Dim _DisAmt As Decimal = 0.0
        Dim _VatAmt As Decimal = 0.0
        Dim _NetAmt As Decimal = 0.0

        Dim PoAmtTH As String = ""
        Dim PoAmtEN As String = ""

        '_Str = " SELECt FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity,FNQuantity%1 AS FNQuantityDiff , 1-(FNQuantity%1) AS FNQuantityAdd,FNSurchangeAmt"
        '_Str &= vbCrLf & "    FROM"
        '_Str &= vbCrLf & "  (SELECT P.FTPurchaseNo, P.FNHSysRawMatId, MAX(P.FTOrderNo) AS FTOrderNo, SUM(P.FNQuantity) AS FNQuantity,Max(ISNULL(P.FNSurchangeAmt,0)) AS FNSurchangeAmt,CASE WHEN ISNULL(Sub.FTStatePromo,'') ='' THEN CASE WHEN ODX.FNOrderType =5 THEN 'QRS' WHEN ODX.FNOrderType =999 THEN 'QPP' ELSE '' END  ELSE ISNULL(Sub.FTStatePromo,'') END  AS FTStatePromo"
        '_Str &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P"
        '_Str &= vbCrLf & "  OUTER APPLY(SELECT TOP 1 'PROMOTIONAL' AS FTStatePromo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK) WHERE Sub.FTOrderNo = P.FTOrderNo AND  Sub.FNHSysBuyGrpId = 1405080002  )  AS Sub "

        '_Str &= vbCrLf & "    OUTER APPLY(SELECT TOP 1 ODX.FNOrderType  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.V_OrderProdAndSMPPurchase AS ODX  WHERE ODX.FTOrderNo = P.FTOrderNo  )  AS ODX "

        '_Str &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
        '_Str &= vbCrLf & "  GROUP BY P.FTPurchaseNo, P.FNHSysRawMatId,CASE WHEN ISNULL(Sub.FTStatePromo,'') ='' THEN CASE WHEN ODX.FNOrderType =5 THEN 'QRS' WHEN ODX.FNOrderType =999 THEN 'QPP' ELSE '' END  ELSE ISNULL(Sub.FTStatePromo,'') END ) AS A "
        '_Str &= vbCrLf & "  WHERE FNQuantity % 1 >0"


        _Str = " SELECt FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity,FNQuantity%1 AS FNQuantityDiff , 1-(FNQuantity%1) AS FNQuantityAdd,FNSurchangeAmt"
        _Str &= vbCrLf & "    FROM"
        _Str &= vbCrLf & "  (SELECT P.FTPurchaseNo, P.FNHSysRawMatId, MAX(P.FTOrderNo) AS FTOrderNo, SUM(P.FNQuantity) AS FNQuantity,Max(ISNULL(P.FNSurchangeAmt,0)) AS FNSurchangeAmt,P.FTStatePromo"
        _Str &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURPurchase_OrderNo AS P"
        _Str &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
        _Str &= vbCrLf & "  GROUP BY P.FTPurchaseNo, P.FNHSysRawMatId,P.FTStatePromo ) AS A "
        _Str &= vbCrLf & "  WHERE FNQuantity % 1 >0"



        _dtdiff = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_PUR)

        If _dtdiff.Rows.Count > 0 Then

            For Each R As DataRow In _dtdiff.Rows
                _Str = "   UPDATE   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                _Str &= vbCrLf & " SET FNQuantity=FNQuantity+" & Double.Parse(Val(R!FNQuantityAdd.ToString)) & ""
                _Str &= vbCrLf & ",FNNetAmt=Convert(numeric(18, 2), (FNQuantity+" & Double.Parse(Val(R!FNQuantityAdd.ToString)) & ") * (FNPrice - FNDisAmt))"
                _Str &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
                _Str &= vbCrLf & "  AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Str &= vbCrLf & "  AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "

                HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                _FNSurchangeAmt = Val(R!FNSurchangeAmt.ToString)
                If _FNSurchangeAmt > 0 Then

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                    _Str &= vbCrLf & " SET FNSurchangePerUnit = CASE WHEN " & _FNSurchangeAmt & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,5)," & _FNSurchangeAmt & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                    _Str &= vbCrLf & " ),1))  END "
                    _Str &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * ((FNPrice - FNDisAmt) +  "
                    _Str &= vbCrLf & " ( CASE WHEN " & _FNSurchangeAmt & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,4)," & _FNSurchangeAmt & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                    _Str &= vbCrLf & " ),1))  END ) "
                    _Str &= vbCrLf & " ))"
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                End If

            Next

            _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
            _Str &= vbCrLf & "    FROM"
            _Str &= vbCrLf & " ("
            _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
            _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
            _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
            _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

            PoAmt = Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_PUR, "0"))

            If DiscountPer > 0 Then
                _DisAmt = CDbl(Format((PoAmt * DiscountPer) / 100, HI.ST.Config.AmtFormat))
            End If

            If Vat > 0 Then
                _VatAmt = CDbl(Format((((PoAmt - _DisAmt) + SurCharge) * Vat) / 100, HI.ST.Config.AmtFormat))
            End If

            _NetAmt = ((PoAmt - _DisAmt) + SurCharge) + _VatAmt

            PoAmtEN = HI.UL.ULF.Convert_Bath_EN(_NetAmt)
            PoAmtTH = HI.UL.ULF.Convert_Bath_TH(_NetAmt)



            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET "
            _Str &= vbCrLf & "  FNPoAmt=" & PoAmt & "  "
            _Str &= vbCrLf & " , FNDisCountAmt =" & _DisAmt & "  "
            _Str &= vbCrLf & " , FNPONetAmt=" & (PoAmt - _DisAmt) & "  "
            _Str &= vbCrLf & " , FNVatAmt=" & _VatAmt & "  "
            _Str &= vbCrLf & " , FNPOGrandAmt=" & _NetAmt & "  "
            _Str &= vbCrLf & " , FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(PoAmtEN) & "'"
            _Str &= vbCrLf & " , FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(PoAmtEN) & "'"
            _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

        End If


        _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(PoKey) & "'  "
        HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

    End Sub
End Class
Imports DevExpress.XtraEditors.Controls

Public Class wSMPStatusActual


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



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

#Region "Procedure"



    Public Sub LoadDataInfo(ByVal Key As String)

        Dim _Qry As String = ""
        _Qry = " SELECT     SOP.FNSeq"

        _Qry &= vbCrLf & "  , Case When ISDATE(ISNULL( SOP.FTDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime, SOP.FTDate),103) ELSE '' END AS  FTDate "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNQuantity,0) As FNQuantity"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) As FNPass"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNNotPass,0) As FNNotPass"
        _Qry &= vbCrLf & " ,  ISNULL(SOP.FTRemark,'') AS FTRemark"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) +  ISNULL(SOP.FNNotPass,0) As FNTotalQC"

        _Qry &= vbCrLf & "  , ISNULL(SOP.FTSizeBreakDown,'') As FTSizeBreakDown"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FTColorway,'') As FTColorway"

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC AS SOP WITH (NOLOCK)"
        _Qry &= vbCrLf & "   WHERE SOP.FTTeam='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC"
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcoperation.DataSource = _dt

    End Sub


#End Region

#Region "General"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



    Private Function CheckProcess(key As String, Optional showmsg As Boolean = True) As Boolean
        Dim stateprocess As Boolean = False

        Dim cmd As String = ""

        cmd = "select top 1 FTStateFinish from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS x with(nolock) where FTTeam='" & HI.UL.ULF.rpQuoted(key) & "'"

        stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        If stateprocess Then

            If showmsg Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึก จบ กระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return stateprocess
    End Function



    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load




    End Sub

#End Region

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderProdDetail()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs)
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click
    End Sub

    Private Sub LoadOrderProdDetail()
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogcoperation.DataSource = Nothing

        cmd = "   Select  A.FTSMPOrderNo "
        cmd &= vbCrLf & "  , Case When ISDATE(A.FDSMPOrderDate) = 1 Then  convert(Datetime,A.FDSMPOrderDate)   Else NULL END AS  FDSMPOrderDate"

        cmd &= vbCrLf & "   , A.FNSMPPrototypeNo"
        cmd &= vbCrLf & "   , A.FTStyleCode"
        cmd &= vbCrLf & "   , A.FTSeasonCode"
        cmd &= vbCrLf & "   , A.FTCustCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            cmd &= vbCrLf & "  ,ISNULL(XT.FTNameTH,'') AS FNSMPOrderType"
            cmd &= vbCrLf & "   , A.FTCustNameTH AS FTCustName"
        Else
            cmd &= vbCrLf & "  ,ISNULL(XT.FTNameEN,'') AS FNSMPOrderType"
            cmd &= vbCrLf & "   , A.FTCustNameEN  AS FTCustName"
        End If

        cmd &= vbCrLf & "   , A.FTMerTeamCode"
        cmd &= vbCrLf & "   , A.FNSeq"
        cmd &= vbCrLf & "   , A.FTSizeBreakDown"
        cmd &= vbCrLf & "   , A.FTColorway"
        cmd &= vbCrLf & "   , A.FNQuantity"
        cmd &= vbCrLf & "   , A.FTDeliveryDate"
        cmd &= vbCrLf & "   , A.FTRemark"
        '  cmd &= vbCrLf & "   , B.FTTeam "
        cmd &= vbCrLf & "   ,ISNULL(TEmp.FTEmpName,'') AS FTEmpName"
        cmd &= vbCrLf & "   ,a.FNQuantity As TeqmQuantity"

        cmd &= vbCrLf & "   ,Case When ISDATE(Cut.FTStartDate) = 1 Then  convert(Datetime,Cut.FTStartDate)   Else NULL END AS   FTStartDateCut"
        cmd &= vbCrLf & "  ,Case When ISDATE(Cut.FTLastDate) = 1 Then  convert(Datetime,Cut.FTLastDate)   Else NULL END AS   FTLastDateCut"
        cmd &= vbCrLf & "   , case when  Cut.FNQuantity > A.FNQuantity then A.FNQuantity else   Cut.FNQuantity  END   As FNQuantityCut"

        cmd &= vbCrLf & "   ,Case When ISDATE(SendEmb.FTStartDate) = 1 Then  convert(Datetime,SendEmb.FTStartDate)   Else NULL END AS  FTStartDateEmb"
        cmd &= vbCrLf & "   ,Case When ISDATE(SendEmb.FTLastDate) = 1 Then  convert(Datetime,SendEmb.FTLastDate)   Else NULL END AS  FTLastDateEmb"
        cmd &= vbCrLf & "   ,SendEmb.FNQuantity As FNQuantityEmb"
        cmd &= vbCrLf & "    ,Case When ISDATE(RcvEmb.FTStartDate) = 1 Then  convert(Datetime,RcvEmb.FTStartDate)   Else NULL END AS  FTStartDateRcvEmb"
        cmd &= vbCrLf & "   ,Case When ISDATE(RcvEmb.FTLastDate) = 1 Then  convert(Datetime,RcvEmb.FTLastDate)   Else NULL END AS  FTLastDateRcvEmb"
        cmd &= vbCrLf & "   ,RcvEmb.FNQuantity As FNQuantityRcvEmb"

        cmd &= vbCrLf & "   ,Case When ISDATE(SendPrint.FTStartDate) = 1 Then  convert(Datetime,SendPrint.FTStartDate)   Else NULL END AS  FTStartDatePrint"
        cmd &= vbCrLf & "   ,Case When ISDATE(SendPrint.FTLastDate) = 1 Then  convert(Datetime,SendPrint.FTLastDate)   Else NULL END AS  FTLastDatePrint"
        cmd &= vbCrLf & "   ,SendPrint.FNQuantity As FNQuantityPrint"
        cmd &= vbCrLf & "    ,Case When ISDATE(RcvPrint.FTStartDate) = 1 Then  convert(Datetime,RcvPrint.FTStartDate)   Else NULL END AS  FTStartDateRcvPrint"
        cmd &= vbCrLf & "   ,Case When ISDATE(RcvPrint.FTLastDate) = 1 Then  convert(Datetime,RcvPrint.FTLastDate)   Else NULL END AS  FTLastDateRcvPrint"
        cmd &= vbCrLf & "   ,RcvPrint.FNQuantity As FNQuantityRcvPrint"

        cmd &= vbCrLf & "   ,Case When ISDATE(SendHeat.FTStartDate) = 1 Then  convert(Datetime,SendHeat.FTStartDate)   Else NULL END AS  FTStartDateHeat"
        cmd &= vbCrLf & "   ,Case When ISDATE(SendHeat.FTLastDate) = 1 Then  convert(Datetime,SendHeat.FTLastDate)   Else NULL END AS  FTLastDateHeat"
        cmd &= vbCrLf & "   ,SendHeat.FNQuantity As FNQuantityHeat"
        cmd &= vbCrLf & "    ,Case When ISDATE(RcvHeat.FTStartDate) = 1 Then  convert(Datetime,RcvHeat.FTStartDate)   Else NULL END AS  FTStartDateRcvHeat"
        cmd &= vbCrLf & "   ,Case When ISDATE(RcvHeat.FTLastDate) = 1 Then  convert(Datetime,RcvHeat.FTLastDate)   Else NULL END AS  FTLastDateRcvHeat"
        cmd &= vbCrLf & "   ,RcvHeat.FNQuantity As FNQuantityRcvHeat"

        cmd &= vbCrLf & "   ,Case When ISDATE(SendLasor.FTStartDate) = 1 Then  convert(Datetime,SendLasor.FTStartDate)   Else NULL END AS  FTStartDateLasor"
        cmd &= vbCrLf & "   ,Case When ISDATE(SendLasor.FTLastDate) = 1 Then  convert(Datetime,SendLasor.FTLastDate)   Else NULL END AS  FTLastDateLasor"
        cmd &= vbCrLf & "   ,SendLasor.FNQuantity As FNQuantityLasor"

        cmd &= vbCrLf & "    ,Case When ISDATE(RcvLasor.FTStartDate) = 1 Then  convert(Datetime,RcvLasor.FTStartDate)   Else NULL END AS  FTStartDateRcvLasor"
        cmd &= vbCrLf & "   ,Case When ISDATE(RcvLasor.FTLastDate) = 1 Then  convert(Datetime,RcvLasor.FTLastDate)   Else NULL END AS  FTLastDateRcvLasor"
        cmd &= vbCrLf & "   ,RcvLasor.FNQuantity As FNQuantityRcvLasor"


        cmd &= vbCrLf & "   ,Case When ISDATE(SendPadPrint.FTStartDate) = 1 Then  convert(Datetime,SendPadPrint.FTStartDate)   Else NULL END AS  FTStartDatePadPrint"
        cmd &= vbCrLf & "   ,Case When ISDATE(SendPadPrint.FTLastDate) = 1 Then  convert(Datetime,SendPadPrint.FTLastDate)   Else NULL END AS  FTLastDatePadPrint"
        cmd &= vbCrLf & "   ,SendPadPrint.FNQuantity As FNQuantityPadPrint"

        cmd &= vbCrLf & "    ,Case When ISDATE(RcvPadPrint.FTStartDate) = 1 Then  convert(Datetime,RcvPadPrint.FTStartDate)   Else NULL END AS  FTStartDateRcvPadPrint"
        cmd &= vbCrLf & "   ,Case When ISDATE(RcvPadPrint.FTLastDate) = 1 Then  convert(Datetime,RcvPadPrint.FTLastDate)   Else NULL END AS  FTLastDateRcvPadPrint"
        cmd &= vbCrLf & "   ,RcvPadPrint.FNQuantity As FNQuantityRcvPadPrint"

        cmd &= vbCrLf & "   ,Case When ISDATE(SendSew.FTStartDate) = 1 Then  convert(Datetime,SendSew.FTStartDate)   Else NULL END AS  FTStartDateSew"
        cmd &= vbCrLf & "   ,Case When ISDATE(SendSew.FTLastDate) = 1 Then  convert(Datetime,SendSew.FTLastDate)   Else NULL END AS  FTLastDateSew"
        cmd &= vbCrLf & "   ,SendSew.FNQuantity As FNQuantitySew"
        cmd &= vbCrLf & "   ,Case When ISDATE(FinishSew.FTStartDate) = 1 Then  convert(Datetime,FinishSew.FTStartDate)   Else NULL END AS  FTStartDateFinishSew"
        cmd &= vbCrLf & "   ,Case When ISDATE(FinishSew.FTLastDate) = 1 Then  convert(Datetime,FinishSew.FTLastDate)   Else NULL END AS  FTLastDateFinishSew"
        cmd &= vbCrLf & "   ,FinishSew.FNQuantity As FNQuantityFinishSew"

        cmd &= vbCrLf & "   ,Case When ISDATE(SMK.FTStartDate) = 1 Then  convert(Datetime,SMK.FTStartDate)   Else NULL END AS  FTStartDateSMK"
        cmd &= vbCrLf & "   ,Case When ISDATE(SMK.FTLastDate) = 1 Then  convert(Datetime,SMK.FTLastDate)   Else NULL END AS  FTLastDateSMK"
        cmd &= vbCrLf & "   ,SMK.FNQuantity As FNQuantitySMK"



        cmd &= vbCrLf & "    ,Case When ISDATE(QC.FTStartDate) = 1 Then  convert(Datetime,QC.FTStartDate)   Else NULL END AS  FTStartDateQC"
        cmd &= vbCrLf & "   ,Case When ISDATE(QC.FTLastDate) = 1 Then  convert(Datetime,QC.FTLastDate)   Else NULL END AS  FTLastDateQC"
        cmd &= vbCrLf & "   ,FX.FNQuantity As FNQuantityQC"
        cmd &= vbCrLf & "   ,FX.FNPass "
        cmd &= vbCrLf & "   ,FX.FNNotPass "

        cmd &= vbCrLf & "   ,ISNULL(FX.FTStateFinish,'0') AS FTStateQCFinish"
        cmd &= vbCrLf & "   ,Case When ISDATE(FX.FTStateFinishDate) = 1 Then  convert(Datetime,FX.FTStateFinishDate)   Else NULL END AS  FTStateQCFinishDate"
        cmd &= vbCrLf & "   ,ISNULL(FX.FTStateFinishBy,'') AS  FTStateFinishQCBy"

        cmd &= vbCrLf & "  FROM ( Select A.FTSMPOrderNo, A.FDSMPOrderDate, A.FNSMPOrderType, A.FNSMPPrototypeNo, MST.FTStyleCode, MSS.FTSeasonCode, MCT.FTCustCode, MCT.FTCustNameTH, MCT.FTCustNameEN, MMT.FTMerTeamCode, OD.FNSeq,"
        cmd &= vbCrLf & "     OD.FTSizeBreakDown"
        cmd &= vbCrLf & " 	, OD.FTColorway"
        cmd &= vbCrLf & " 	,   isnull(bd.FNQuantity ,  OD.FNQuantity) FNQuantity"
        cmd &= vbCrLf & " 	,Case When ISDATE(OD.FTDeliveryDate) = 1 Then  convert(Datetime,OD.FTDeliveryDate)  Else NULL END AS FTDeliveryDate"
        cmd &= vbCrLf & " 	, OD.FTRemark , bd.FTBarcodeBundleNo"
        cmd &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As A With(NOLOCK)  INNER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId INNER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As MSS With(NOLOCK)  On A.FNHSysSeasonId = MSS.FNHSysSeasonId INNER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As MCT With(NOLOCK)  On A.FNHSysCustId = MCT.FNHSysCustId INNER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam As MMT With(NOLOCK)  On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId INNER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown As OD With(NOLOCK)  On A.FTSMPOrderNo = OD.FTSMPOrderNo"
        cmd &= vbCrLf & "  LEFT JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle  BD with(nolock) on a.FTSMPOrderNo = bd.FTOrderProdNo and od.FTSizeBreakDown = bd.FTSizeBreakDown and od.FTColorway = bd.FTColorway"


        cmd &= vbCrLf & "    WHERE A.FTSMPOrderNo<>''"

        If FNHSysCustId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysCustId=" & Val(FNHSysCustId.Properties.Tag.ToString) & ""
        End If

        If FNHSysStyleId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & ""
        End If

        If FNHSysSeasonId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""
        End If

        If FNHSysMerTeamId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & ""
        End If


        If FTStartOrderDate.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FDSMPOrderDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "'"
        End If

        If FTEndOrderDate.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FDSMPOrderDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
        End If

        cmd &= vbCrLf & "  ) As A"
        'cmd &= vbCrLf & "   Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam As B With(NOLOCK)  On A.FTSMPOrderNo = B.FTSMPOrderNo "
        'cmd &= vbCrLf & "   Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamBreakdown As C With(NOLOCK)  On B.FTSMPOrderNo =C.FTSMPOrderNo And B.FTTeam =C.FTTeam And A.FTSizeBreakDown =C.FTSizeBreakDown And A.FTColorway=C.FTColorway"


        'cmd &= vbCrLf & " outer apply ( Select     sum(FNBarcodeSeq ) as FNQuantity   , sum(FNPass) FNPass , sum(FNNotPass)  FNNotPass   "
        'cmd &= vbCrLf & "   , min( isnull(Fxa.FTStateApp,'0') ) as FTStateFinish ,max( Fxa.FTAppBy) as FTStateFinishBy  , convert(varchar(10) , convert(date ,  max( Fxa.FDAppDate   ) ) , 111)  as FTStateFinishDate "
        'cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal Fxa with(nolock)  outer apply (  "
        'cmd &= vbCrLf & "  select count(FNBarcodeSeq) as FNBarcodeSeq ,   sum( FTPass) as FNPass ,  sum(FTNotPass) as FNNotPass   "
        'cmd &= vbCrLf & " from ( "
        'cmd &= vbCrLf & "  select b.FNBarcodeSeq ,case when  min( x.FTStateReject) = '0' then 1 else  0 end FTPass   ,case when  min( x.FTStateReject) = '1' then 1 else  0 end FTNotPass   "
        'cmd &= vbCrLf & "  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Detail x with(nolock)  "
        'cmd &= vbCrLf & "  LEFT JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Barcode b  with(nolock) on x.FTBarcodeRef = b.FTBarcodeRef  "
        'cmd &= vbCrLf & "  and x.FNHourNo = b.FNHourNo and x.FNSeq = b.FNSeq and x.FTOrderNo = b.FTOrderNo   "
        'cmd &= vbCrLf & " where x.FTOrderNo = Fxa.FTOrderNo   and x.FTBarcodeCartonNo = FXa.FTBarcodeCartonNo  group by  b.FNBarcodeSeq   ) as b ) as b  "
        'cmd &= vbCrLf & " left join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle bd with(nolocK) on fxa.FTBarcodeCartonNo = bd.FTBarcodeBundleNo "
        'cmd &= vbCrLf & "  where  Fxa.FTOrderNo =  A.FTSMPOrderNo  "
        'cmd &= vbCrLf & "  and bd.FTSizeBreakDown = A.FTSizeBreakDown "
        'cmd &= vbCrLf & "  and bd.FTColorway = A.FTColorway  "
        'cmd &= vbCrLf & " group by FTOrderNo    ) as FX"  ',  FNBarcodeSeq  , FNPass , FNNotPass 
        cmd &= vbCrLf & "   outer apply (  SELECT   FTTeam ,  FTSMPOrderNo ,   FTSizeBreakDown, FTColorway, sum(FNQuantity) FNQuantity, sum(FNPass)  FNPass, sum(FNNotPass)  FNNotPass "
        cmd &= vbCrLf & "      , FXd.FTStateFinish , FXd.FTStateFinishBy , fxd.FTStateFinishDate "
        cmd &= vbCrLf & "    FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC FXa  "
        cmd &= vbCrLf & "    outer apply (  select    min( isnull(FXd.FTStateFinish,'0') ) as FTStateFinish  "

        cmd &= vbCrLf & "   ,max( FXd.FTStateFinishBy) as FTStateFinishBy  "
        cmd &= vbCrLf & "  , convert(varchar(10) , convert(date ,  max( FXd.FTStateFinishDate   ) ) , 111)  as FTStateFinishDate "
        cmd &= vbCrLf & "   from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam FXd"
        cmd &= vbCrLf & "    where fxd.FTSMPOrderNo = fxa.FTSMPOrderNo  and  fxd.FTTeam = fxa.FTTeam  "
        cmd &= vbCrLf & "  ) as FXd"
        cmd &= vbCrLf & "  where FTSMPOrderNo =    a.FTSMPOrderNo    "
        cmd &= vbCrLf & "  and FXa.FTSizeBreakDown = A.FTSizeBreakDown "
        cmd &= vbCrLf & "  and FXa.FTColorway = A.FTColorway     and fxa.FTTeam = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & "   group by  FTTeam , FTSMPOrderNo ,  FTColorway , FTSizeBreakDown  , FXd.FTStateFinish , FXd.FTStateFinishBy , fxd.FTStateFinishDate  ) as FX "



        cmd &= vbCrLf & "   LEFT OUTER JOIN (  "
        cmd &= vbCrLf & "  Select FNListIndex,FTNameTH,FTNameEN "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        cmd &= vbCrLf & "   WHERE  (FTListName = N'FNSMPOrderType')  "
        cmd &= vbCrLf & "   ) AS XT ON  A.FNSMPOrderType =XT.FNListIndex "


        cmd &= vbCrLf & "   OUTER APPLY("
        cmd &= vbCrLf & "  Select  STUFF((Select  ', ' + FTEmpName "
        cmd &= vbCrLf & " 	From(Select Convert(nvarchar(10),Row_number() Over(Order By  b.FNHSysEmpId)) + '.'  +  FTEmpNameTH + ' ' + FTEmpSurnameTH   AS FTEmpName"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Emp As b with(nolock)  "
        cmd &= vbCrLf & "      left join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee emp with(nolock) on b.FNHSysEmpId = emp.FNHSysEmpID   "
        cmd &= vbCrLf & " inner join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle bd on b.FTBarcodeNo = bd.FTBarcodeBundleNo  "
        cmd &= vbCrLf & "    where bd.FTOrderProdNo = a.FTSMPOrderNo    and  bd.FTBarcodeBundleNo  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & "   "
        cmd &= vbCrLf & "     ) As TEmp For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') AS FTEmpName  ) As TEmp"

        cmd &= vbCrLf & "   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = a.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = ''  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =a.FTSizeBreakDown And X.FTColorway=a.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=0"
        cmd &= vbCrLf & "   ) As Cut"

        cmd &= vbCrLf & "   		  OUTER APPLY( "

        cmd &= vbCrLf & "   Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=1"
        cmd &= vbCrLf & "   ) As SendEmb"
        cmd &= vbCrLf & " 	   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=2"
        cmd &= vbCrLf & "   ) As RcvEmb"

        cmd &= vbCrLf & "    OUTER APPLY( "

        cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "      From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & " 	 Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=3"
        cmd &= vbCrLf & " 	  ) As SendPrint"
        cmd &= vbCrLf & " 	   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=4"
        cmd &= vbCrLf & "   ) As RcvPrint"
        cmd &= vbCrLf & "   OUTER APPLY( "

        cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "     From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  =  a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=5"
        cmd &= vbCrLf & " 	  ) As SendHeat"
        cmd &= vbCrLf & " 	   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=6"
        cmd &= vbCrLf & "   ) As RcvHeat"

        cmd &= vbCrLf & "   OUTER APPLY( "

        cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "     From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=7"
        cmd &= vbCrLf & " 	  ) As SendLasor"
        cmd &= vbCrLf & " 	   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=8"
        cmd &= vbCrLf & "   ) As RcvLasor"

        cmd &= vbCrLf & "   OUTER APPLY( "
        cmd &= vbCrLf & "     Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "     From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=9"
        cmd &= vbCrLf & " 	  ) As SendPadPrint"
        cmd &= vbCrLf & " 	   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=10"
        cmd &= vbCrLf & "   ) As RcvPadPrint"

        cmd &= vbCrLf & "    OUTER APPLY( "

        cmd &= vbCrLf & "   Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=11"
        cmd &= vbCrLf & "   ) As SendSew"


        cmd &= vbCrLf & "    OUTER APPLY( "

        cmd &= vbCrLf & "   Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo  "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=13"
        cmd &= vbCrLf & "   ) As SMK"



        cmd &= vbCrLf & "  OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = A.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =A.FTSizeBreakDown And X.FTColorway=A.FTColorway"
        cmd &= vbCrLf & " And FNSampleState=12"
        cmd &= vbCrLf & "   ) As FinishSew"

        cmd &= vbCrLf & "   OUTER APPLY( "

        cmd &= vbCrLf & "    Select  MIN(FTDate) As FTStartDate,MAX (FTDate) As FTLastDate,SUM(FNQuantity) As FNQuantity,SUM(FNPass) As FNPass,SUM(FNNotPass) As FNNotPass"
        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC As X  With(NOLOCK)"
        cmd &= vbCrLf & "  Where X.FTSMPOrderNo = a.FTSMPOrderNo"
        cmd &= vbCrLf & " And X.FTTeam  = a.FTBarcodeBundleNo   "
        cmd &= vbCrLf & " And X.FTSizeBreakDown =a.FTSizeBreakDown And X.FTColorway=a.FTColorway"

        cmd &= vbCrLf & "  ) As QC"


        If FTStartSendSew.Text <> "" Or FTEndSendSew.Text <> "" Then


            cmd &= vbCrLf & "   INNER JOIN ( "

            cmd &= vbCrLf & "   Select  FTSMPOrderNo,FTSizeBreakDown,FTColorway,FTTeam"
            cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X  With(NOLOCK)"
            cmd &= vbCrLf & "  WHERE FNSampleState=11 -- AND ISNULL(FTTeam,'') <> '' "

            If FTStartSendSew.Text <> "" Then
                cmd &= vbCrLf & " And X.FTDate >= '" & HI.UL.ULDate.ConvertEnDB(FTStartSendSew.Text) & "' "
            End If

            If FTEndSendSew.Text <> "" Then
                cmd &= vbCrLf & " And X.FTDate <= '" & HI.UL.ULDate.ConvertEnDB(FTEndSendSew.Text) & "' "
            End If

            cmd &= vbCrLf & "   GROUP BY  FTSMPOrderNo,FTSizeBreakDown,FTColorway ,FTTeam "
            cmd &= vbCrLf & "   ) As SendDataSew  "
            cmd &= vbCrLf & "  ON  A.FTSMPOrderNo = SendDataSew.FTSMPOrderNo "
            cmd &= vbCrLf & "  And a.FTBarcodeBundleNo    = SendDataSew.FTTeam   "
            cmd &= vbCrLf & "  And  A.FTColorway = SendDataSew.FTColorway  "
            cmd &= vbCrLf & "  And  A.FTSizeBreakDown = SendDataSew.FTSizeBreakDown "

        End If

        cmd &= vbCrLf & " ORDER BY  A.FTSMPOrderNo ,A.FNSeq"

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogcoperation.DataSource = _dtprod.Copy

        ogvoperation.BestFitColumns()

        _dtprod.Dispose()



    End Sub

    Private Sub ogvoperation_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvoperation.RowStyle

        Try
            With Me.ogvoperation

                'If "" & .GetRowCellValue(e.RowHandle, "FTStateQCFinish").ToString = "1" Then
                '    e.Appearance.BackColor = Drawing.Color.LightYellow
                '    e.Appearance.BackColor2 = Drawing.Color.Orange
                'End If

            End With

        Catch ex As Exception
        End Try

    End Sub
End Class
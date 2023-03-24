Imports System.Data
Imports DevExpress.XtraGrid.GridControl
Public Class wSubOrderCancelTracking
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'SetColMaster()

    End Sub

#Region "Property"
    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property
#End Region

#Region "SetGridControl"
    Private Sub SetColMaster()

    End Sub
#End Region

#Region "Procedure"

    Public Sub LoadDataInfo(OrderKey As String, OrderSubKey As String)
        Call LoadData(OrderKey, OrderSubKey)
    End Sub


    Private Sub LoadData(OrderKey As String, OrderSubKey As String)
        Dim Qry As String = ""
        Qry = "Select OC.FTOrderNo,OC.FTSubOrderNo,OC.FNCancelSeq,OCB.FTColorway,OCB.FTSizeBreakDown,OCB.FNQuantity,OB.FNQuantity As OriginalQuantity ,CASE  WHEN isnull(OS.FTPORef,'')='' THEN O.FTPORef else OS.FTPORef END  AS FTPORef,O.FDOrderDate,(OB.FNQuantity - OCB.FNQuantity) AS Balance FROM"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTORderSub_Cancel As OC With(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Cancel_BreakDown As OCB With(NOLOCK) On OC.FTOrderNo = OCB.FTOrderNo And OC.FTSubOrderNo = OCB.FTSubOrderNo And OC.FNCancelSeq = OCB.FNCancelSeq LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown As OB With(NOLOCK) On OCB.FTOrderNo = OB.FTOrderNo And OCB.FTSubOrderNo = OB.FTSubOrderNo And OCB.FTColorway = OB.FTColorway And OCB.FTSizeBreakDown = OB.FTSizeBreakDown LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS WITH(NOLOCK) ON OB.FTOrderNo = OS.FTOrderNo And OB.FTSubOrderNo = OS.FTSubOrderNo LEFT OUTER JOIN "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON OB.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize As S WITH(NOLOCK) ON  OB.FNHSysMatSizeId = s.FNHSysMatSizeId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor As C On OB.FNHSysMatColorId = C.FNHSysMatColorId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown As DIB WITH(NOLOCK) ON OB.FTOrderNo =  DIB.FTOrderNo And OB.FTSubOrderNo = DIB.FTSubOrderNo  And OB.FTColorway = DIB.FTColorway And OB.FTSizeBreakDown = DIB.FTSizeBreakDown LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS DI WITH(NOLOCK) ON DIB.FTOrderNo = DI.FTOrderNo And DIB.FTSubOrderNo = DI.FTSubOrderNo And DIB.FNDivertSeq = DI.FNDivertSeq"

        If FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "WHERE OC.FTOrderNo >= '" & FTOrderNo.Text & "'"
        End If
        If FTOrderNoTo.Text <> "" Then
            Qry &= vbCrLf & " AND OC.FTOrderNo <= '" & FTOrderNoTo.Text & "'"
        End If
        If FTSubOrderNo.Text <> "" Then
            Qry &= vbCrLf & " And OC.FTSubOrderNo >= '" & FTSubOrderNo.Text & "'"
        End If
        If FTSubOrderNoTo.Text <> "" Then
            Qry &= vbCrLf & "  AND OC.FTSubOrderNo <= '" & FTSubOrderNoTo.Text & "'"
        End If
        Qry &= vbCrLf & "ORDER BY OC.FTOrderNo asc, OC.FTSubOrderNo asc, S.FNMatSizeSeq asc"
        ogc.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Qry = "    Select O.FDOrderDate,CMP.FTCmpCode,CUS.FTCustCode,STY.FTStyleCode,OS.FDShipDate,CASE  WHEN isnull(OS.FTPORef,'')='' THEN O.FTPORef else OS.FTPORef END  AS FTPORef"
        Qry &= vbCrLf & " ,OC.FTOrderNo,OC.FTSubOrderNo,OC.FNCancelSeq"
        Qry &= vbCrLf & " ,OCB.FTNikePOLineItem,OCB.FTSizeBreakDown,OCB.FNQuantity,OB.FNQuantity AS OriginalQuantity,(OB.FNQuantity - OCB.FNQuantity) AS Balance"
        Qry &= vbCrLf & " ,Buy.FTBuyGrpNameTH AS FTBuyGrpName,P.FTPlantNameTH AS FTPlantName,OCB.FTColorway,CTN.FTContinentNameTH AS FTContinentName ,CTY.FTCountryNameTH AS FTCountryName,PV.FTProvinceNameTH AS FTProvinceName,SM.FTShipModenNameTH AS FTShipModeName,SP.FTShipPortNameTH AS FTShipPortName,OC.FTRemark"
        Qry &= vbCrLf & " FROM"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Cancel AS OC WITH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Cancel_BreakDown AS OCB WITH(NOLOCK) ON OC.FTOrderNo = OCB.FTOrderNo And OC.FTSubOrderNo = OCB.FTSubOrderNo And OC.FNCancelSeq = OCB.FNCancelSeq LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OB WITH(NOLOCK) ON OCB.FTOrderNo = OB.FTOrderNo And OCB.FTSubOrderNo = OB.FTSubOrderNo And OCB.FTColorway = OB.FTColorway And OCB.FTSizeBreakDown = OB.FTSizeBreakDown LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS WITH(NOLOCK) ON OB.FTOrderNo = OS.FTOrderNo And OB.FTSubOrderNo = OS.FTSubOrderNo LEFT OUTER JOIN "
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON OS.FTOrderNo = O.FTOrderNO LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CMP WITH(NOLOCK) ON O.FNHSysCmpId = CMP.FNHSysCmpId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS CUS WITH(NOLOCK) ON O.FNHSysCustId = CUS.FNHSysCustId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS STY WITH(NOLOCK) ON O.FNHSysStyleId = STY.FNHSysStyleId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS Buy WITH(NOLOCK) ON OS.FNHSysBuyGrpId = Buy.FNHSysBuyGrpId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS P WITH(NOLOCK) ON OS.FNHSysPlantId = P.FNHSysPlantId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS CTN WITH(NOLOCK) ON OS.FNHSysContinentId = CTN.FNHSysContinentId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS CTY WITH(NOLOCK) ON OS.FNHSysCountryId = CTY.FNHSysCountryId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS PV WITH(NOLOCK) ON OS.FNHSysProvinceId = PV.FNHSysProvinceId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS SM WITH(NOLOCK) ON OS.FNHSysShipModeId = SM.FNHSysShipModeId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS SP WITH(NOLOCK) ON OS.FNHSysShipPortId = SP.FNHSysShipPortId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize As S WITH(NOLOCK) ON OB.FNHSysMatSizeId = s.FNHSysMatSizeId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS C ON OB.FNHSysMatColorId = C.FNHSysMatColorId LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown As DIB WITH(NOLOCK) ON OB.FTOrderNo =  DIB.FTOrderNo And OB.FTSubOrderNo = DIB.FTSubOrderNo  And OB.FTColorway = DIB.FTColorway And OB.FTSizeBreakDown = DIB.FTSizeBreakDown LEFT OUTER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS DI WITH(NOLOCK) ON DIB.FTOrderNo = DI.FTOrderNo And DIB.FTSubOrderNo = DI.FTSubOrderNo And DIB.FNDivertSeq = DI.FNDivertSeq"

        If FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "WHERE OC.FTOrderNo >= '" & FTOrderNo.Text & "'"
        End If
        If FTOrderNoTo.Text <> "" Then
            Qry &= vbCrLf & " AND OC.FTOrderNo <= '" & FTOrderNoTo.Text & "'"
        End If
        If FTSubOrderNo.Text <> "" Then
            Qry &= vbCrLf & " And OC.FTSubOrderNo >= '" & FTSubOrderNo.Text & "'"
        End If
        If FTSubOrderNoTo.Text <> "" Then
            Qry &= vbCrLf & "  AND OC.FTSubOrderNo <= '" & FTSubOrderNoTo.Text & "'"
        End If
        Qry &= vbCrLf & "ORDER BY OC.FTOrderNo asc, OC.FTSubOrderNo asc, S.FNMatSizeSeq asc,OC.FNCancelSeq"
        ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)


    End Sub
#End Region

#Region "Event"
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Verify() Then
            Dim _Spls As New TL.SplashScreen("Please Wait.....Loading Data")
            LoadData("", "")
            _Spls.Close()
        Else
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTOrderNo_lbl.Text)
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTOrderNo_lbl.Text)
            End If

        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        TL.HandlerControl.ClearControl(Me)
    End Sub
#End Region

    Public Function Verify() As Boolean
        If Me.FTOrderNo.Text <> "" And FTOrderNoTo.Text <> "" Then


            Return True
        Else
            FTOrderNo.Focus()
            Return False
        End If
    End Function
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


End Class
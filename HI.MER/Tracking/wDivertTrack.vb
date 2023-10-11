Imports System.Data
Imports DevExpress.XtraGrid.GridControl

Public Class wDivertTrack
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
        'Dim Qry As String = ""
        'Dim dtMaster As DataTable
        'Dim _col As Integer = 1

        'Qry = "select FNMatSizeSeq,FTMatSizeNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize order by FNMatSizeSeq asc"
        'dtMaster = Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)

        'With ogvDetail
        '    .Columns.Add()
        '    With .Columns(0)
        '        .Name = "FTOrderNo"
        '        .FieldName = "FTOrderNo"
        '        .Caption = "FTOrderNo"
        '        .Visible = True
        '        With .AppearanceHeader
        '            .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '        End With
        '        With .OptionsColumn
        '            .AllowEdit = False
        '            .AllowMove = False
        '            .AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            .ReadOnly = True
        '        End With
        '        .Width = 80
        '    End With
        '    .Columns.Add()
        '    With .Columns(1)
        '        .Name = "FTOrderSubNo"
        '        .FieldName = "FTOrderSubNo"
        '        .Caption = "FTOrderSubNo"
        '        .Visible = True
        '        With .AppearanceHeader
        '            .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '        End With
        '        With .OptionsColumn
        '            .AllowEdit = False
        '            .AllowMove = False
        '            .AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            .ReadOnly = True
        '        End With
        '        .Width = 90
        '    End With
        '    For Each R As DataRow In dtMaster.Rows
        '        _col += 1
        '        .Columns.Add()
        '        With .Columns(_col)
        '            .Name = R!FTMatSizeNameEN.ToString
        '            .FieldName = R!FTMatSizeNameEN.ToString
        '            .Caption = R!FTMatSizeNameEN.ToString
        '            .Visible = True
        '            With .AppearanceHeader
        '                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '            End With
        '            With .AppearanceCell
        '                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '            End With
        '            With .OptionsColumn
        '                .AllowEdit = False
        '                .AllowSort = DevExpress.Utils.DefaultBoolean.False
        '                .AllowMove = False
        '                .ReadOnly = True
        '            End With
        '            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '            .DisplayFormat.FormatString = "{0:n0}"
        '        End With
        '    Next
        'End With

    End Sub
#End Region

#Region "Procedure"

    Public Sub LoadDataInfo(OrderKey As String, OrderSubKey As String)
        Call LoadData(OrderKey, OrderSubKey)
    End Sub


    Private Sub LoadData(OrderKey As String, OrderSubKey As String)
        Dim Qry As String = ""

        Qry = "Select * "
        Qry &= vbCrLf & " FROM ( Select * from"
        Qry &= vbCrLf & "(Select S.FTSubOrderNo,S.FTSizeBreakDown"
        Qry &= vbCrLf & ",sum(S.FNQuantity)-isnull(DD.Di,0) As FNQuantityOriginal ,0 As QtyDivert , 0 As Bl,SB.FTSubOrderNoDivertRef,Case When SB.FTPORef<>'' then '' else O.FTPORef end as FTPORef,O.FTOrderNo,S.FTColorway,'' AS FTColorwayNew"
        Qry &= vbCrLf & "from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS S WITH(NOLOCK) ON O.FTOrderNo=S.FTOrderNo INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SB WITH(NOLOCK) ON S.FTSubOrderNo=SB.FTSubOrderNo and S.FTOrderNo=SB.FTOrderNo LEFT OUTER JOIN"
        Qry &= vbCrLf & "(select S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway"
        Qry &= vbCrLf & ",sum(S.FNQuantity) AS Di"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS S WITH(NOLOCK)"
        Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway) AS DD ON S.FTSubOrderNo=DD.FTSubOrderNo and S.FTColorway=DD.FTColorway and S.FTSizeBreakDown=DD.FTSizeBreakDown"


        Qry &= vbCrLf & "where S.FTOrderNo<>''"
        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and S.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and O.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and S.FTOrderNo='" & OrderKey & "'"
        End If
        If OrderSubKey <> "" Then
            Qry &= vbCrLf & "and S.FTSubOrderNo='" & OrderSubKey & "'"
        End If
        Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown,SB.FTSubOrderNoDivertRef,O.FTPORef,O.FTOrderNo,S.FTColorway,SB.FTPORef,DD.Di) AS A"
        Qry &= vbCrLf & "where A.FTPORef<>''"
        Qry &= vbCrLf & "union"
        'Modify by joker 2016/10/08 13.37///***////Modify by joker 2016/10/10 09.06 //****////  modify by joker 2016/10/24 11.15
        Qry &= vbCrLf & "select * from"
        Qry &= vbCrLf & "(select S.FTSubOrderNo,SB.FTSizeBreakDown,sum(SB.FNQuantity) AS FNQuantityOriginal,0 AS QtyDivert , 0 AS Bl,S.FTSubOrderNoDivertRef,case when O.FTPORef='' then '' else S.FTPORef end as FTPORef"
        Qry &= vbCrLf & ",S.FTOrderNo,SB.FTColorway,'' As FTColorwayNew"
        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub aS S with(nolock) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SB with(NOLOCK) ON S.FTOrderNo=SB.FTOrderNo and S.FTSubOrderNo=SB.FTSubOrderNo"
        Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O with(NOLOCK) ON S.FTOrderNo=O.FTOrderNo"
        Qry &= vbCrLf & "where S.FTOrderNo<>''"

        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and S.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If

        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and S.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If

        If OrderKey <> "" Then
            Qry &= vbCrLf & "and S.FTOrderNo='" & OrderKey & "'"
        End If

        If OrderSubKey <> "" Then
            Qry &= vbCrLf & "and S.FTSubOrderNo='" & OrderSubKey & "'"
        End If

        Qry &= vbCrLf & "group by S.FTSubOrderNo,SB.FTSizeBreakDown,S.FTSubOrderNoDivertRef,S.FTPORef,S.FTOrderNo,SB.FTColorway,O.FTPORef )AS B"
        Qry &= vbCrLf & "where B.FTPORef<>''"
        'end modify
        ' modify by joker 2016/10/24 11.06
        Qry &= vbCrLf & "union"
        Qry &= vbCrLf & "select * from"
        Qry &= vbCrLf & "(select S.FTSubOrderNo+'-D' AS FTSubOrderNo,S.FTSizeBreakDown"
        Qry &= vbCrLf & ",0 AS FNQuantityOriginal  ,sum(S.FNQuantity) AS QtyDivert ,(A.Origin-B.Di) AS Bl,'' AS FTSubOrderNoDivertRef"
        Qry &= vbCrLf & ",case when isnull(D.FTPORef,'')='' then O.FTPORef else D.FTPORef end as FTPORef,S.FTOrderNo,S.FTColorway, ISNULL(S.FTColorwayNew,'') AS FTColorwayNew"
        Qry &= vbCrLf & "from "
        'end modify
        Qry &= vbCrLf & "(select  O.FTOrderNo FROM"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WItH(NOLOCK)"
        Qry &= vbCrLf & "where O.FTOrderNo<>''"
        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and O.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and O.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and O.FTOrderNo='" & OrderKey & "'"
        End If

        Qry &= vbCrLf & "union"
        Qry &= vbCrLf & "Select D.FTOrderNo"
        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS D with(nolock)"
        Qry &= vbCrLf & "where D.FTOrderNo<>''"
        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and D.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and D.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and D.FTOrderNo='" & OrderKey & "'"
        End If

        Qry &= vbCrLf & ")  AS AA LeFT OUtER JOIN"

        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS S WITH(NOLOCK)ON AA.FTOrderNo=S.FTOrderNo inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS D WITH(NOLOCK) ON S.FTOrderNo=D.FTOrderNo and S.FTSubOrderNo=D.FTSubOrderNo and S.FNDivertSeq=D.FNDivertSeq inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON S.FTOrderNo=O.FTOrderNo LeFT OUTER JOIN"
        'modify by joker 2016/10/11 10.25
        Qry &= vbCrLf & "(select S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway"
        Qry &= vbCrLf & ",sum(S.FNQuantity) AS Origin from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS S WITH(NOLOCK)"
        Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway) AS A ON S.FTSubOrderNo=A.FTSubOrderNo and S.FTSizeBreakDown=A.FTSizeBreakDown and S.FTColorway=A.FTColorway LeFT OUTER JOIN"
        Qry &= vbCrLf & "(select S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway"
        Qry &= vbCrLf & ",sum(S.FNQuantity) AS Di"
        Qry &= vbCrLf & "from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS S WITH(NOLOCK)"
        Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway) AS B ON S.FTSubOrderNo=B.FTSubOrderNo and S.FTSizeBreakDown=B.FTSizeBreakDown and S.FTColorway=B.FTColorway"
        'end modify

        Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown,a.Origin,b.Di,D.FTPORef,O.FTPORef,S.FTOrderNo,S.FTColorway, ISNULL(S.FTColorwayNew,'')"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS S WITH(NOLOCK) inner join"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS D WITH(NOLOCK) ON S.FTOrderNo=D.FTOrderNo and S.FTSubOrderNo=D.FTSubOrderNo and S.FNDivertSeq=D.FNDivertSeq inner join"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON S.FTOrderNo=O.FTOrderNo LeFT OUTER JOIN"
        'Qry &= vbCrLf & "(select S.FTSubOrderNo,S.FTSizeBreakDown"
        'Qry &= vbCrLf & ",sum(S.FNQuantity) AS Origin from"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS S WITH(NOLOCK)"
        'Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown) AS A ON S.FTSubOrderNo=A.FTSubOrderNo and S.FTSizeBreakDown=A.FTSizeBreakDown LeFT OUTER JOIN"
        'Qry &= vbCrLf & "(select S.FTSubOrderNo,S.FTSizeBreakDown"
        'Qry &= vbCrLf & ",sum(S.FNQuantity) AS Di"
        'Qry &= vbCrLf & "from"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS S WITH(NOLOCK)"
        'Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown) AS B ON S.FTSubOrderNo=B.FTSubOrderNo and S.FTSizeBreakDown=B.FTSizeBreakDown"
        'Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown,a.Origin,b.Di,S.FNDivertSeq,D.FTPORef,O.FTPORef,S.FTOrderNo"
        Qry &= vbCrLf & ") AS X "

        'Qry &= vbCrLf & " Outer apply (select top 1 X21.FTOrderTypeName   "
        'Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X2 "
        'Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 FTNameEN AS FTOrderTypeName  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X21 WITH(NOLOCK) WHERE        (X21.FTListName = N'FNOrderType') AND X21.FNListIndex = X2.FNOrderType  ) AS X21 "
        'Qry &= vbCrLf & "  WHERE X2.FTOrderNo = X.FTOrderNo "
        'Qry &= vbCrLf & "   ) AS Z21 "

        Qry &= vbCrLf & "where X.FTOrderNo<>''"
        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and X.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and X.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and X.FTOrderNo='" & OrderKey & "'"
        End If
        'If OrderSubKey <> "" Then
        '    Qry &= vbCrLf & "and X.FTSubOrderNo='" & OrderSubKey & "-D'"
        'End If
        Qry &= vbCrLf & " ) AS Z "
        Qry &= vbCrLf & " Outer apply (select top 1 X21.FTOrderTypeName   "
        Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X2 "
        Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 FTNameEN AS FTOrderTypeName  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X21 WITH(NOLOCK) WHERE        (X21.FTListName = N'FNOrderType') AND X21.FNListIndex = X2.FNOrderType  ) AS X21 "
        Qry &= vbCrLf & "  WHERE X2.FTOrderNo = Z.FTOrderNo "
        Qry &= vbCrLf & "   ) AS Z21 "


        Qry &= vbCrLf & " order by FTSubOrderNo"

        ogc.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)


        'Qry = "select C.FTCmpCode,Cust.FTCustCode,S.FTStyleCode,convert(varchar(10),convert(datetime,O.FDOrderDate),103) AS OrderDate"
        'If ST.Lang.Language = ST.Lang.eLang.TH Then
        '    Qry &= vbCrLf & ",Conti.FTContinentNameTH AS FTContinentName,Coun.FTCountryNameTH AS FTCountryName,Provin.FTProvinceNameTH AS FTProvinceName"
        '    Qry &= vbCrLf & ",Shipm.FTShipModenNameTH AS FTShipModenName,Shipp.FTShipPortNameTH AS FTShipPortName"
        'Else
        '    Qry &= vbCrLf & ",Conti.FTContinentNameEN AS FTContinentName,Coun.FTCountryNameEN AS FTCountryName,Provin.FTProvinceNameEN AS FTProvinceName"
        '    Qry &= vbCrLf & ",Shipm.FTShipModenNameEN AS FTShipModenName,Shipp.FTShipPortNameEN AS FTShipPortName"
        'End If
        'Qry &= vbCrLf & ""
        'Qry &= vbCrLf & ",T.*"
        'Qry &= vbCrLf & "from"

        'Qry &= vbCrLf & "(select convert(varchar(10),convert(datetime,OS.FDShipDate),103) AS Shipdate,CASE wHeN isnull(OS.FTPORef,'')='' Then O.FTPORef else isnull(OS.FTPORef,'')end AS FTPOref,OSB.FTOrderNo,OSB.FTSubOrderNo as FTSubOrderNoRef,OSB.FTColorway,OSB.FTSizeBreakDown,OSB.FTNikePOLineItem"
        'Qry &= vbCrLf & ",OSB.FNGrandQuantity,0 AS FNQuantity,OSB.FNQuantityExtra,OSB.FNGarmentQtyTest"
        'Qry &= vbCrLf & ",isnull((OSB.FNGrandQuantity-(OSB.FNQuantity+isnull(OSB.FNQuantityExtra,0)+isnull(OSB.FNGarmentQtyTest,0))),0) AS Balance"
        'Qry &= vbCrLf & "from"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITh(NOLOCK) INNER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OSB WItH(NOLOCK) ON OSB.FTOrderNo=O.FTOrderNo INNER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS WITH(NOLOCK) ON osb.FTOrderNo=os.FTOrderNo and osb.FTSubOrderNo=os.FTSubOrderNo"
        ''Qry &= vbCrLf & "and V.FTSizeBreakDown=OSB.FTSizeBreakDown "
        'Qry &= vbCrLf & "where O.FTOrderNo<>''"
        'If FTOrderNo.Text <> "" Then
        '    Qry &= vbCrLf & "and O.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        'End If
        'If FTCustomerPO.Text <> "" Then
        '    Qry &= vbCrLf & "and O.FTPORef='" & Me.FTCustomerPO.Text & "'"
        'End If

        'Qry &= vbCrLf & "union all"
        'Qry &= vbCrLf & "select convert(varchar(10),convert(datetime,V.FDShipDate),103) AS Shipdate,V.FTPOref,V.FTOrderNo,V.FTSubOrderNoRef,V.FTColorway,V.FTSizeBreakDown,V.FTNikePOLineItem"
        'Qry &= vbCrLf & ",0 AS FNGrandQuantity,OSDD.FNQuantity,OSD.FNQuantityExtra,'' AS FNGarmentQtyTest"
        'Qry &= vbCrLf & ",isnull((OSD.FNGrandQuantity-(OSDD.FNQuantity+isnull(OSD.FNQuantityExtra,0)+isnull(OSD.FNGarmentQtyTest,0))),0) AS Balance "
        'Qry &= vbCrLf & "from"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS V WITH(NOLOCK) INNER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown  AS OSD WItH(NOLOCK) ON V.FTOrderNo=OSD.FTOrderNo and V.FTSubOrderNo=OSD.FTSubOrderNo and V.FTColorway=OSD.FTColorway INnER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS OSDD WITH(NOLOCK) ON V.FTOrderNo=OSDD.FTOrderNo and v.FTSubOrderNo=OSDD.FTSubOrderNo and v.FTColorway=OSDD.FTColorway"
        'Qry &= vbCrLf & "and V.FTSizeBreakDown=OSDD.FTSizeBreakDown"
        'Qry &= vbCrLf & "and V.FTSizeBreakDown=OSD.FTSizeBreakDown "
        'Qry &= vbCrLf & "where V.FTSubOrderNo<>V.FTSubOrderNoRef"
        'If FTOrderNo.Text <> "" Then
        '    Qry &= vbCrLf & "and V.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        'End If
        'If FTCustomerPO.Text <> "" Then
        '    Qry &= vbCrLf & "and V.FTPORef='" & Me.FTCustomerPO.Text & "'"
        'End If

        'Qry &= vbCrLf & ") AS T "
        'Qry &= vbCrLf & "INNER Join"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON T.FTOrderNo=O.FTOrderNo"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON O.FNHSysCmpId=C.FNHSysCmpId"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS Cust with(nolock) ON O.FNHSysCustId=Cust.FNHSysCustId"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S with(nolock) ON O.FNHSysStyleId=S.FNHSysStyleId"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS VV WITH(NOLOCK)"
        'Qry &= vbCrLf & "ON T.FTOrderNo=VV.FTOrderNo and T.FTSubOrderNoRef=VV.FTSubOrderNoRef and T.FTColorway=VV.FTColorway and T.FTSizeBreakDown=VV.FTSizeBreakDown"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS Conti with(nolock) ON VV.FNHSysContinentId=Conti.FNHSysContinentId"
        'Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS Coun with(nolock) ON VV.FNHSysCountryId=Coun.FNHSysCountryId"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS Provin with(nolock) ON VV.FNHSysProvinceId=Provin.FNHSysProvinceId"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS ShipM with(nolock) ON VV.FNHSysShipModeId=ShipM.FNHSysShipModeId"
        'Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS ShipP with(nolock) ON VV.FNHSysShipPortId=ShipP.FNHSysShipPortId"
        'Qry &= vbCrLf & "order by FTSubOrderNoRef asc"

        Qry = "select C.FTCmpCode,Cust.FTCustCode,S.FTStyleCode,convert(varchar(10),convert(datetime,O.FDOrderDate),103) AS OrderDate,Bgrp.FTBuyGrpCode,Plnt.FTPlantCode"

        If ST.Lang.Language = ST.Lang.eLang.TH Then

            Qry &= vbCrLf & ",Conti.FTContinentNameTH AS FTContinentName,Coun.FTCountryNameTH AS FTCountryName,Provin.FTProvinceNameTH AS FTProvinceName"
            Qry &= vbCrLf & ",Shipm.FTShipModenNameTH AS FTShipModeName,Shipp.FTShipPortNameTH AS FTShipPortName"
            Qry &= vbCrLf & ",Bgrp.FTBuyGrpNameTH AS FTBuyGrpName"
            Qry &= vbCrLf & ",Plnt.FTPlantNameTH AS FTPlantName"

        Else

            Qry &= vbCrLf & ",Conti.FTContinentNameEN AS FTContinentName,Coun.FTCountryNameEN AS FTCountryName,Provin.FTProvinceNameEN AS FTProvinceName"
            Qry &= vbCrLf & ",ShipM.FTShipModeNameEN AS FTShipModeName,Shipp.FTShipPortNameEN AS FTShipPortName"
            Qry &= vbCrLf & ",Bgrp.FTBuyGrpNameEN AS FTShipModeName"
            Qry &= vbCrLf & ",Plnt.FTPlantNameEN AS FTPlantName"

        End If

        'Qry &= vbCrLf & ",Conti.FTContinentNameTH AS FTContinentName,Coun.FTCountryNameTH AS FTCountryName,Provin.FTProvinceNameTH AS FTProvinceName"
        'Qry &= vbCrLf & ",Shipm.FTShipModenNameTH AS FTShipModenName,Shipp.FTShipPortNameTH AS FTShipPortName"
        Qry &= vbCrLf & ",T.*,Z21.FTOrderTypeName"
        Qry &= vbCrLf & "from"
        Qry &= vbCrLf & "(select * from"
        Qry &= vbCrLf & "(select convert(varchar(10),convert(datetime,OS.FDShipDate),103) AS Shipdate,case when OS.FTPORef<>'' then '' else O.FTPORef end as FTPORef"
        Qry &= vbCrLf & ",OSB.FTOrderNo,OSB.FTSubOrderNo as FTSubOrderNoRef,OSB.FTColorway,OSB.FTSizeBreakDown,OSB.FTNikePOLineItem,'' AS FTColorwayNew"
        Qry &= vbCrLf & ",OSB.FNQuantity-isnull(DD.Di,0) AS FNGrandQuantity,0 AS FNQuantity,isnull(OSB.FNQuantityExtra,0) AS FNQuantityExtra,isnull(OSB.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
        Qry &= vbCrLf & ",OS.FNHSysContinentId,OS.FNHSysCountryId,OS.FNHSysProvinceId,OS.FNHSysShipModeId,OS.FNHSysShipPortId,OS.FNHSysBuyGrpId,OS.FNHSysPlantId"
        Qry &= vbCrLf & "from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITh(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OSB WItH(NOLOCK) ON OSB.FTOrderNo=O.FTOrderNo INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS WITH(NOLOCK) ON osb.FTOrderNo=os.FTOrderNo and osb.FTSubOrderNo=os.FTSubOrderNo LEFT OUTER JOIN"
        Qry &= vbCrLf & "(select S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway"
        Qry &= vbCrLf & ",sum(S.FNQuantity) AS Di"
        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS S WITH(NOLOCK)"
        Qry &= vbCrLf & "group by S.FTSubOrderNo,S.FTSizeBreakDown,S.FTColorway) AS DD ON OSB.FTSubOrderNo=DD.FTSubOrderNo and OSB.FTColorway=DD.FTColorway and OSB.FTSizeBreakDown=DD.FTSizeBreakDown"
        Qry &= vbCrLf & "where O.FTOrderNo<>''"
        If FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and O.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and O.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and OSB.FTOrderNo='" & OrderKey & "'"
        End If
        If OrderSubKey <> "" Then
            Qry &= vbCrLf & "and OSB.FTSubOrderNo='" & OrderSubKey & "'"
        End If
        Qry &= vbCrLf & ") AS A where A.FTPORef<>''"
        'Qry &= vbCrLf & "and O.FTOrderNo='NI1601772'"
        Qry &= vbCrLf & "union"
        ' modify by joker 2016/10/08 13.44

        Qry &= vbCrLf & "select * from"
        Qry &= vbCrLf & "(select convert(varchar(10),convert(datetime,S.FDShipDate),103) AS Shipdate,case when isnull(O.FTPORef,'')='' then '' else S.FTPORef end as FTPORef"
        Qry &= vbCrLf & ",SB.FTOrderNo,SB.FTSubOrderNo as FTSubOrderNoRef,SB.FTColorway,SB.FTSizeBreakDown,SB.FTNikePOLineItem,'' AS FTColorwayNew"
        Qry &= vbCrLf & ",SB.FNQuantity AS FNGrandQuantity,0 AS FNQuantity,isnull(SB.FNQuantityExtra,0) AS FNQuantityExtra,isnull(SB.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
        Qry &= vbCrLf & ",S.FNHSysContinentId,S.FNHSysCountryId,S.FNHSysProvinceId,S.FNHSysShipModeId,S.FNHSysShipPortId,S.FNHSysBuyGrpId,S.FNHSysPlantId"
        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub aS S with(nolock) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SB with(NOLOCK) ON S.FTOrderNo=SB.FTOrderNo and S.FTSubOrderNo=SB.FTSubOrderNo"
        Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON S.FTOrderNo=O.FTOrderNo"
        Qry &= vbCrLf & "where S.FTOrderNo<>''"
        If FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and s.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and s.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and SB.FTOrderNo='" & OrderKey & "'"
        End If
        If OrderSubKey <> "" Then
            Qry &= vbCrLf & "and SB.FTSubOrderNo='" & OrderSubKey & "'"
        End If
        Qry &= vbCrLf & ") AS B where B.FTPORef<>''"
        'end modify
        Qry &= vbCrLf & "union"
        Qry &= vbCrLf & "select * from"
        Qry &= vbCrLf & "(select convert(varchar(10),convert(datetime,SD.FDShipDate),103) AS Shipdate"
        Qry &= vbCrLf & ",case when isnull(OSDD.FTOrderNo,'')<>'' then case when isnull(sd.FTPORef,'')<>'' then SD.FTPORef else O.FTPORef end else O.FTPORef end as FTPORef"
        Qry &= vbCrLf & ",OSDD.FTOrderNo"
        Qry &= vbCrLf & ",OSDD.FTSubOrderNo+'-D'+convert(varchar(5),OSDD.FNDivertSeq) AS FTSubOrderNoRef"
        Qry &= vbCrLf & ",OSDD.FTColorway"
        Qry &= vbCrLf & ",OSDD.FTSizeBreakDown"
        Qry &= vbCrLf & ",OSDD.FTNikePOLineItem"
        Qry &= vbCrLf & ",ISNULL(OSDD.FTColorwayNew,'') AS FTColorwayNew"


        Qry &= vbCrLf & ",0 AS FNGrandQuantity,isnull(OSDD.FNQuantity,0) AS FNQuantity,0 as FNQuantityExtra,0 AS FNGarmentQtyTest"
        Qry &= vbCrLf & ",SD.FNHSysContinentId,SD.FNHSysCountryId,SD.FNHSysProvinceId,SD.FNHSysShipModeId,SD.FNHSysShipPortId,SD.FNHSysBuyGrpId,SD.FNHSysPlantId"
        Qry &= vbCrLf & "from"
        Qry &= vbCrLf & "(select  O.FTOrderNo from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WItH(NOLOCK)"
        Qry &= vbCrLf & "where O.FTOrderNo<>''"
        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and O.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and O.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and O.FTOrderNo='" & OrderKey & "'"
        End If

        Qry &= vbCrLf & "union"
        Qry &= vbCrLf & "Select D.FTOrderNo"
        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS D with(nolock)"
        Qry &= vbCrLf & "where D.FTOrderNo<>''"
        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and D.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and D.FTPORef='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and D.FTOrderNo='" & OrderKey & "'"
        End If

        Qry &= vbCrLf & ")  AS AA LeFT OUtER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCk) ON AA.FTOrderNo=O.FTOrderNo INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo=S.FTOrderNo INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown  AS OSD WItH(NOLOCK) ON S.FTOrderNo=OSD.FTOrderNo and S.FTSubOrderNo=OSD.FTSubOrderNo  LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS OSDD WITH(NOLOCK) ON OSD.FTOrderNo=OSDD.FTOrderNo and OSD.FTSubOrderNo=OSDD.FTSubOrderNo and OSD.FTColorway=OSDD.FTColorway"
        Qry &= vbCrLf & "and OSD.FTSizeBreakDown=OSDD.FTSizeBreakDown LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS SD WItH(NOLOCK) ON OSDD.FTOrderNo=Sd.FTOrderNo and OSDD.FTSubOrderNo=SD.FTSubOrderNo and OSDD.FNDivertSeq=SD.FNDivertSeq"

        Qry &= vbCrLf & ") AS A WHERE A.FTOrderNo<>''"
        If Me.FTOrderNo.Text <> "" Then
            Qry &= vbCrLf & "and A.FTOrderNo='" & Me.FTOrderNo.Text & "'"
        End If
        If Me.FTCustomerPO.Text <> "" Then
            Qry &= vbCrLf & "and A.FTPOref='" & Me.FTCustomerPO.Text & "'"
        End If
        If OrderKey <> "" Then
            Qry &= vbCrLf & "and A.FTOrderNo='" & OrderKey & "'"
        End If

        Qry &= vbCrLf & ") AS T "
        Qry &= vbCrLf & "INNER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON T.FTOrderNo=O.FTOrderNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON O.FNHSysCmpId=C.FNHSysCmpId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS Cust with(nolock) ON O.FNHSysCustId=Cust.FNHSysCustId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S with(nolock) ON O.FNHSysStyleId=S.FNHSysStyleId"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS VV WITH(NOLOCK)"
        'Qry &= vbCrLf & "ON T.FTOrderNo=VV.FTOrderNo and T.FTSubOrderNoRef=VV.FTSubOrderNoRef and T.FTColorway=VV.FTColorway and T.FTSizeBreakDown=VV.FTSizeBreakDown"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS Conti with(nolock) ON T.FNHSysContinentId=Conti.FNHSysContinentId"
        Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS Coun with(nolock) ON T.FNHSysCountryId=Coun.FNHSysCountryId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS Provin with(nolock) ON T.FNHSysProvinceId=Provin.FNHSysProvinceId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS ShipM with(nolock) ON T.FNHSysShipModeId=ShipM.FNHSysShipModeId"
        Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS ShipP with(nolock) ON T.FNHSysShipPortId=ShipP.FNHSysShipPortId"

        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS Bgrp with(nolock) ON T.FNHSysBuyGrpId=Bgrp.FNHSysBuyGrpId"
        Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS Plnt with(nolock) ON T.FNHSysPlantId=Plnt.FNHSysPlantId"

        Qry &= vbCrLf & " Outer apply (select top 1 X21.FTOrderTypeName   "
        Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X2 "
        Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 FTNameEN AS FTOrderTypeName  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X21 WITH(NOLOCK) WHERE        (X21.FTListName = N'FNOrderType') AND X21.FNListIndex = X2.FNOrderType  ) AS X21 "
        Qry &= vbCrLf & "  WHERE X2.FTOrderNo = O.FTOrderNo "
        Qry &= vbCrLf & "   ) AS Z21 "

        Qry &= vbCrLf & "order by FTSubOrderNoRef asc"


        ',OS.FNHSysBuyGrpId,OS.FNHSysPlantId
        ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)

    End Sub
#End Region

#Region "Event"
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Verify() Then
            Dim _Spls As New TL.SplashScreen("Please Wait.....Loading Data")
            LoadData("", "")
            _Spls.Close()
        Else
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTOrderNo_lbl.Text & " หรือ " & Me.FTCustomerPO_lbl.Text)
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTOrderNo_lbl.Text & " Or " & Me.FTCustomerPO_lbl.Text)
            End If

        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        TL.HandlerControl.ClearControl(Me)
    End Sub
#End Region

    Public Function Verify() As Boolean
        If Me.FTOrderNo.Text <> "" Or Me.FTCustomerPO.Text <> "" Then
            Return True
        Else
            Return False
        End If
    End Function



End Class
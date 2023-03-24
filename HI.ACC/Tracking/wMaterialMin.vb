Imports DevExpress.XtraGrid.Columns
Public Class wMaterialMin
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call SETGridMergCell()
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

    Private Sub LoadData()
        Dim Qry As String = ""
        Dim dt As DataTable
        Dim spls As New HI.TL.SplashScreen("Please wait Loding Data....")

        Qry = "select H.FTCmpCode, H.FTCustCode, H.FTOrderNo,convert(varchar(10),convert(date,FDShipDate),103) AS FDShipDate"
        Qry &= vbCrLf & ",H.FTMerTeamCode, H.FTPurchaseNo, H.FTRawMatCode, H.FTRawMatColorCode,H.cFDPurchaseDate"
        Qry &= vbCrLf & ",H.FTUnitCode, sum(H.FNQuantity) as FNQuantity, H.FNPrice,H.FTRawMatColorName,H.FTRawMatName,PoStateName,FTMatType"
        Qry &= vbCrLf & ",H.FTFabricFrontSize,H.FTRawMatSizeCode"
        'เพิ่ม ราคาตอนรับ ตอนซื้อ 20160721 10.16 ไม่เอาแล้ว 15.58
        'Qry &= vbCrLf & ",H.PurFNPrice,H.RecFNPrice"

        Qry &= vbCrLf & ",H.FNPrice*K.Allowcate as TotalMoney"
        Qry &= vbCrLf & ",K.Allowcate as FNAllowcate from"


        If (Me.FDPODateStart.Text <> "" Or Me.FDPODateEnd.Text <> "") And (Me.FTOrderNo.Text = "" And Me.FTOrderNoTo.Text = "") Then
            'อีกเคสสนึง ที่เอา FNOrderType =4 ขึ้นก่อน
            Qry &= vbCrLf & "("
            Qry &= vbCrLf & "SELECT b.FTPurchaseNo,b.FNHSysRawMatId,A.OrderMin,B.FTOrderNo AS ORDERNOMIN"
            Qry &= vbCrLf & ",b.FNQuantity AS QtyMIN"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM("
            Qry &= vbCrLf & "R_D.FNQuantity)"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
            'new
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            Qry &= vbCrLf & " where O.FNOrderType <> 4"
            Qry &= vbCrLf & "AND R.FNRceceiveType=0"
            Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
            Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
            Qry &= vbCrLf & "),0) AS QTYNOMIN"
            Qry &= vbCrLf & ",A.FDPurchaseDate"

            'Qry &= vbCrLf & ",case when b.FNQuantity=0 then b.FNQuantity else (b.FNQuantity*A.FNQuantity)/ISNULL((SELECT SUM("
            'Qry &= vbCrLf & "R_D.FNQuantity)"
            'Qry &= vbCrLf & "from"
            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
            ''new
            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            'Qry &= vbCrLf & " where O.FNOrderType <> 4 AND R.FNRceceiveType=0"
            'Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
            'Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
            'Qry &= vbCrLf & "),0) end AS Allowcate"

            'modify 2016/06/03 Start
            Qry &= vbCrLf & ",case when b.FNQuantity=0 then a.FNQuantity else "
            Qry &= vbCrLf & "(SELECT SUM(J.Allow) FROM"
            Qry &= vbCrLf & "(SELECT  FTBarcodeNo, FNHSysRawMatId, FTOrderNo, FNQuantity, FTPurchaseNo, FTOrderNoRef"
            Qry &= vbCrLf & ",FNQuantity - ISNULL((SELECT SUM("
            Qry &= vbCrLf & "B.FNQuantity ) As Qty"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReturnToSupplier AS A INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_OUT AS B ON A.FTReturnSuplNo = B.FTDocumentNo"
            Qry &= vbCrLf & "WHERE B.FTBarcodeNo = X.FTBarcodeNo"
            Qry &= vbCrLf & "),0) AS Allow"
            Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode AS X "
            Qry &= vbCrLf & "WHERE X.FTPurchaseNo = A.FTPurchaseNo AND X.FNHSysRawMatId=B.FNHSysRawMatId) AS J "
            Qry &= vbCrLf & "WHERE J.FTOrderNoRef=B.FTOrderNo)"
            Qry &= vbCrLf & "end AS Allowcate"
            'modify 2016/06/03 END


            Qry &= vbCrLf & "FROM"
            Qry &= vbCrLf & "(select "
            Qry &= vbCrLf & "PO.FTPurchaseNo, PO.FNHSysRawMatId, PO.FTOrderNo as OrderMin,"
            Qry &= vbCrLf & "R_D.FNQuantity, P.FDPurchaseDate"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            Qry &= vbCrLf & "where O.FNOrderType = 4  AND R.FNRceceiveType=0"
            'ตรงนี้ใส่ criteria
            If FDPODateStart.Text <> "" Then
                Qry &= vbCrLf & "and P.FDPurchaseDate>='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateStart.Text) & "'"
            End If
            If FDPODateEnd.Text <> "" Then
                Qry &= vbCrLf & "and P.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateEnd.Text) & "'"
            End If

            Qry &= vbCrLf & ") AS A INNER JOIN"
            Qry &= vbCrLf & "(select  PO.FNHSysRawMatId,PO.FTPurchaseNo,O.FTOrderNo,R_D.FNQuantity ,P.FDPurchaseDate"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            Qry &= vbCrLf & "where O.FNOrderType <> 4"
            Qry &= vbCrLf & " AND R.FNRceceiveType=0 ) AS B ON A.FNHSysRawMatId=B.FNHSysRawMatId and A.FTPurchaseNo=B.FTPurchaseNo and A.FDPurchaseDate=B.FDPurchaseDate  "
        Else
            Qry &= vbCrLf & "("
            Qry &= vbCrLf & "SELECT b.FTPurchaseNo,b.FNHSysRawMatId,B.OrderMin,A.FTOrderNo AS ORDERNOMIN"
            Qry &= vbCrLf & ",b.FNQuantity AS QtyMIN"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM("
            Qry &= vbCrLf & "R_D.FNQuantity)"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
            'new
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            Qry &= vbCrLf & " where O.FNOrderType <> 4  AND R.FNRceceiveType=0"
            Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
            Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
            Qry &= vbCrLf & "),0) AS QTYNOMIN"
            Qry &= vbCrLf & ",A.FDPurchaseDate"
            'Qry &= vbCrLf & ",case when a.FNQuantity=0 then a.FNQuantity else (b.FNQuantity*A.FNQuantity)/ISNULL((SELECT SUM("
            'Qry &= vbCrLf & "R_D.FNQuantity)"
            'Qry &= vbCrLf & "from"
            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
            ''new
            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            'Qry &= vbCrLf & " where O.FNOrderType <> 4  AND R.FNRceceiveType=0"
            'Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
            'Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
            'Qry &= vbCrLf & "),0) end AS Allowcate"

            'modify 2016/06/03 Start***********************************************************************************************************************************************************************
            Qry &= vbCrLf & ",case when a.FNQuantity=0 then B.FNQuantity else "
            Qry &= vbCrLf & "(SELECT sum(J.Allow) FROM"
            Qry &= vbCrLf & "(SELECT  FTBarcodeNo, FNHSysRawMatId, FTOrderNo, FNQuantity, FTPurchaseNo, FTOrderNoRef"
            Qry &= vbCrLf & ",FNQuantity - ISNULL((SELECT SUM("
            Qry &= vbCrLf & "B.FNQuantity ) As Qty"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReturnToSupplier AS A INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_OUT AS B ON A.FTReturnSuplNo = B.FTDocumentNo"
            Qry &= vbCrLf & "WHERE B.FTBarcodeNo = X.FTBarcodeNo"
            Qry &= vbCrLf & "),0) AS Allow"
            Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode AS X "
            Qry &= vbCrLf & "WHERE X.FTPurchaseNo = B.FTPurchaseNo AND X.FNHSysRawMatId=B.FNHSysRawMatId) AS J "
            Qry &= vbCrLf & "WHERE J.FTOrderNoRef=A.FTOrderNo)"
            Qry &= vbCrLf & "end AS Allowcate"
            'modify 2016/06/03 End

            Qry &= vbCrLf & "FROM"
            Qry &= vbCrLf & "(select "
            Qry &= vbCrLf & "PO.FTPurchaseNo, PO.FNHSysRawMatId, PO.FTOrderNo,"
            Qry &= vbCrLf & "R_D.FNQuantity, P.FDPurchaseDate"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
            'new
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            Qry &= vbCrLf & "where O.FNOrderType <> 4  AND R.FNRceceiveType=0"
            'ตรงนี้ใส่ criteria
            If FDPODateStart.Text <> "" Then
                Qry &= vbCrLf & "and P.FDPurchaseDate>='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateStart.Text) & "'"
            End If
            If FDPODateEnd.Text <> "" Then
                Qry &= vbCrLf & "and P.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateEnd.Text) & "'"
            End If
            If FTOrderNo.Text <> "" Then
                Qry &= vbCrLf & "and O.FTOrderNO>='" & Me.FTOrderNo.Text & "'"
            End If
            If FTOrderNoTo.Text <> "" Then
                Qry &= vbCrLf & "and O.FTOrderNo<='" & Me.FTOrderNoTo.Text & "'"
            End If

            Qry &= vbCrLf & ") AS A INNER JOIN"
            Qry &= vbCrLf & "(select  PO.FNHSysRawMatId,PO.FTPurchaseNo,O.FTOrderNo as OrderMin,R_D.FNQuantity ,P.FDPurchaseDate"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
            Qry &= vbCrLf & "where O.FNOrderType = 4  AND R.FNRceceiveType=0"
            Qry &= vbCrLf & ") AS B ON A.FNHSysRawMatId=B.FNHSysRawMatId and A.FTPurchaseNo=B.FTPurchaseNo and A.FDPurchaseDate=B.FDPurchaseDate  "
        End If

        Qry &= vbCrLf & ") AS K"
        Qry &= vbCrLf & "INNER Join"

        Qry &= vbCrLf & "(SELECT        com.FTCmpCode, Cus.FTCustCode, PO.FTOrderNo"
        Qry &= vbCrLf & ",(select MIN(FDShipDate) from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS with(nolock)"
        Qry &= vbCrLf & "where OS.FTOrderNo = PO.FTOrderNo ) AS FDShipDate"
        Qry &= vbCrLf & ",T.FTMerTeamCode, PO.FTPurchaseNo, M.FTRawMatCode,convert(varchar(10),convert(date,P.FDPurchaseDate),103) as cFDPurchaseDate "
        Qry &= vbCrLf & " ,U.FTUnitCode, R_D.FNQuantity,Bar.FNPrice AS FNPrice,PO.FNHSysRawMatId, P.FDPurchaseDate,MC.FTRawMatColorCode,M.FTFabricFrontSize,MS.FTRawMatSizeCode"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ", A.FTNameTH AS PoStateName"
            Qry &= vbCrLf & ",B.FTNameTH as FTMatType"
            Qry &= vbCrLf & ",M.FTRawMatNameTH AS FTRawMatName"
            Qry &= vbCrLf & ",M.FTRawMatColorNameTH AS FTRawMatColorName "
        Else
            Qry &= vbCrLf & ", A.FTNameEN AS PoStateName"
            Qry &= vbCrLf & ",B.FTNameEN as FTMatType"
            Qry &= vbCrLf & ",M.FTRawMatColorNameEN AS FTRawMatColorName "
            Qry &= vbCrLf & ",M.FTRawMatNameEN AS FTRawMatName"
        End If
        'เพิ่ม ราคาตอนรับ ตอนซื้อ 20160721 10.16  ไม่เอาแล้ว 15.58 ต้องไปเพิ่ม Group by ด้วยนะ แต่ลบออกแล้ว แก้ไปแก้มาแสด..
        'Qry &= vbCrLf & ",PO.FNPrice*P.FNExchangeRate AS PurFNPrice,R_D.FNPrice*R.FNExchangeRate AS RecFNPrice"

        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO WITH (NOLOCK) ON O.FTOrderNo = PO.FTOrderNo LEFT OUTER JOIN"

        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS Bar WITH(NOLOCK) ON PO.FTOrderNo=Bar.FTOrderNo AND PO.FTPurchaseNo=Bar.FTPurchaseNo AND PO.FNHSysRawMatId=Bar.FNHSysRawMatId LEFT OUtER jOIN"

        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH (NOLOCK) ON PO.FTPurchaseNo = P.FTPurchaseNo LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS T WITH (NOLOCK) ON O.FNHSysMerTeamId = T.FNHSysMerTeamId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON PO.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS WITH(NOLOCK)  ON M.FNHSysRawMatSizeId=MS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON M.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON Bar.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS Cus WITH (NOLOCK) ON O.FNHSysCustId = Cus.FNHSysCustId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS com WITH (NOLOCK) ON O.FNHSysCmpId = com.FNHSysCmpId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat as MM WITH (NOLOCK) ON M.FTRawMatCode=MM.FTMainMatCode LEFT OUTER JOIN"
        Qry &= vbCrLf & "(select FNListIndex,FTNameTH,FTNameEN from "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData  WITH(NOLOCK)"
        Qry &= vbCrLf & "Where FTListName='FNPoState') AS A ON P.FNPoState=A.FNListIndex LEFT OUTER JOIN"
        Qry &= vbCrLf & "(select FNListIndex,FTNameTH,FTNameEN from "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WITH(NOLOCK)"
        Qry &= vbCrLf & "WHERE FTListName='FNMerMatType'"
        Qry &= vbCrLf & ") AS B ON MM.FNMerMatType=B.FNListIndex"

        Qry &= vbCrLf & "group by com.FTCmpCode,Cus.FTCustCode,PO.FTOrderNo,T.FTMerTeamCode,PO.FTPurchaseNo"
        Qry &= vbCrLf & ",M.FTRawMatCode,PO.FNPrice,Bar.FNPrice"
        Qry &= vbCrLf & ",U.FTUnitCode,R_D.FNQuantity, R.FNExchangeRate,R_D.FNPrice, PO.FNHSysRawMatId"
        Qry &= vbCrLf & ",P.FDPurchaseDate,MC.FTRawMatColorCode,M.FTFabricFrontSize,MS.FTRawMatSizeCode"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ", A.FTNameTH"
            Qry &= vbCrLf & ",B.FTNameTH"
            Qry &= vbCrLf & ",M.FTRawMatColorNameTH"
            Qry &= vbCrLf & ",M.FTRawMatNameTH"
        Else
            Qry &= vbCrLf & ", A.FTNameEN"
            Qry &= vbCrLf & ",B.FTNameEN"
            Qry &= vbCrLf & ",M.FTRawMatColorNameEN "
            Qry &= vbCrLf & ",M.FTRawMatNameEN"
        End If
        Qry &= vbCrLf & ") AS H ON K.ORDERNOMIN=H.FTOrderNo and K.FTPurchaseNo=H.FTPurchaseNo and K.FNHSysRawMatId=H.FNHSysRawMatId and K.FDPurchaseDate=H.FDPurchaseDate"

        Qry &= vbCrLf & "group by H.FTCmpCode, H.FTCustCode, H.FTOrderNo,FDShipDate,H.FTMerTeamCode, H.FTPurchaseNo, H.FTRawMatCode, H.FTRawMatColorCode,H.cFDPurchaseDate"
        Qry &= vbCrLf & ",H.FTUnitCode,H.FNPrice,K.Allowcate,H.FTRawMatColorName,H.FTRawMatName,H.PoStateName,H.FTMatType,H.FTFabricFrontSize,H.FTRawMatSizeCode"

        Qry &= vbCrLf & "order by H.FTOrderNo"


        'REMEMBER EDIT 2016/06/02
        ''คำนวณจาก จำนวนรับ
        'Qry = "select H.FTCmpCode, H.FTCustCode, H.FTOrderNo,convert(varchar(10),convert(date,FDShipDate),103) AS FDShipDate"
        'Qry &= vbCrLf & ",H.FTMerTeamCode, H.FTPurchaseNo, H.FTRawMatCode, H.FTRawMatColorCode,H.cFDPurchaseDate"
        'Qry &= vbCrLf & ",H.FTUnitCode, sum(H.FNQuantity) as FNQuantity, H.FNPrice,H.FTRawMatColorName,H.FTRawMatName,PoStateName,FTMatType"
        'Qry &= vbCrLf & ",H.FTFabricFrontSize,H.FTRawMatSizeCode"
        'Qry &= vbCrLf & ",H.FNPrice*K.Allowcate as TotalMoney"
        'Qry &= vbCrLf & ",K.Allowcate as FNAllowcate from"

        'If (Me.FDPODateStart.Text <> "" Or Me.FDPODateEnd.Text <> "") And (Me.FTOrderNo.Text = "" And Me.FTOrderNoTo.Text = "") Then
        '    'อีกเคสสนึง ที่เอา FNOrderType =4 ขึ้นก่อน
        '    Qry &= vbCrLf & "("
        '    Qry &= vbCrLf & "SELECT b.FTPurchaseNo,b.FNHSysRawMatId,A.OrderMin,B.FTOrderNo AS ORDERNOMIN"
        '    Qry &= vbCrLf & ",b.FNQuantity AS QtyMIN"
        '    Qry &= vbCrLf & ",ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "R_D.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    'new
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4"
        '    Qry &= vbCrLf & "AND R.FNRceceiveType=0"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) AS QTYNOMIN"
        '    Qry &= vbCrLf & ",A.FDPurchaseDate"
        '    Qry &= vbCrLf & ",case when b.FNQuantity=0 then b.FNQuantity else (b.FNQuantity*(A.FNQuantity-AA.FNQuantityDummyyy))/ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "R_D.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    'new
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4 "
        '    Qry &= vbCrLf & "AND R.FNRceceiveType=0"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) end AS Allowcate"
        '    Qry &= vbCrLf & "FROM"


        '    Qry &= vbCrLf & "(select "
        '    Qry &= vbCrLf & "PO.FTPurchaseNo, PO.FNHSysRawMatId, PO.FTOrderNo as OrderMin,"
        '    Qry &= vbCrLf & "R_D.FNQuantity, P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType = 4  "
        '    Qry &= vbCrLf & "AND R.FNRceceiveType=0"

        '    'ตรงนี้ใส่ criteria
        '    If FDPODateStart.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate>='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateStart.Text) & "'"
        '    End If
        '    If FDPODateEnd.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateEnd.Text) & "'"
        '    End If
        '    Qry &= vbCrLf & ") AS A INNER JOIN"

        '    'ตรงนี้ที่เพิ่ม งานซ่อม
        '    Qry &= vbCrLf & "(SELECT    "
        '    Qry &= vbCrLf & "B.FNHSysRawMatId"
        '    Qry &= vbCrLf & ", B.FTPurchaseNo"
        '    Qry &= vbCrLf & ", sum(ISNULL(BO.FNQuantity,0)) as FNQuantityDummyyy"
        '    Qry &= vbCrLf & "FROM           ( SELECT BO.*  FROM    [ " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H WITH (NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTReturnSuplNo = BO.FTDocumentNo) AS BO INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        '    Qry &= vbCrLf & "WHERE B.FTPurchaseNo = B.FTPurchaseNo"
        '    Qry &= vbCrLf & "group by B.FNHSysRawMatId , B.FTPurchaseNo) AS AA ON A.FNHSysRawMatId=AA.FNHSysRawMatId AND A.FTPurchaseNo=AA.FTPurchaseNo INNER JOIN"
        '    'จบงานซ่อม


        '    Qry &= vbCrLf & "(select  PO.FNHSysRawMatId,PO.FTPurchaseNo,O.FTOrderNo,R_D.FNQuantity ,P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType <> 4"
        '    Qry &= vbCrLf & " AND R.FNRceceiveType=0 "
        '    Qry &= vbCrLf & ") AS B ON A.FNHSysRawMatId=B.FNHSysRawMatId and A.FTPurchaseNo=B.FTPurchaseNo and A.FDPurchaseDate=B.FDPurchaseDate  "

        'Else
        '    Qry &= vbCrLf & "("
        '    Qry &= vbCrLf & "SELECT b.FTPurchaseNo,b.FNHSysRawMatId,B.OrderMin,A.FTOrderNo AS ORDERNOMIN"
        '    Qry &= vbCrLf & ",b.FNQuantity AS QtyMIN"
        '    Qry &= vbCrLf & ",ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "R_D.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    'new
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4  "
        '    Qry &= vbCrLf & "AND R.FNRceceiveType=0"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) AS QTYNOMIN"
        '    Qry &= vbCrLf & ",A.FDPurchaseDate"
        '    Qry &= vbCrLf & ",case when a.FNQuantity=0 then a.FNQuantity else ((b.FNQuantity-AA.FNQuantityDummyyy)*A.FNQuantity)/ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "R_D.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    'new
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4  "
        '    Qry &= vbCrLf & "AND R.FNRceceiveType=0"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) end AS Allowcate"
        '    Qry &= vbCrLf & "FROM"


        '    Qry &= vbCrLf & "(select "
        '    Qry &= vbCrLf & "PO.FTPurchaseNo, PO.FNHSysRawMatId, PO.FTOrderNo,"
        '    Qry &= vbCrLf & "R_D.FNQuantity, P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    'new
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType <> 4  "
        '    Qry &= vbCrLf & "AND R.FNRceceiveType=0"

        '    'ตรงนี้ใส่ criteria
        '    If FDPODateStart.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate>='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateStart.Text) & "'"
        '    End If
        '    If FDPODateEnd.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateEnd.Text) & "'"
        '    End If
        '    If FTOrderNo.Text <> "" Then
        '        Qry &= vbCrLf & "and O.FTOrderNO>='" & Me.FTOrderNo.Text & "'"
        '    End If
        '    If FTOrderNoTo.Text <> "" Then
        '        Qry &= vbCrLf & "and O.FTOrderNo<='" & Me.FTOrderNoTo.Text & "'"
        '    End If
        '    Qry &= vbCrLf & ") AS A INNER JOIN"
        '    'ตรงนี้ที่เพิ่ม งานซ่อม
        '    Qry &= vbCrLf & "(SELECT    "
        '    Qry &= vbCrLf & "B.FNHSysRawMatId"
        '    Qry &= vbCrLf & ", B.FTPurchaseNo"
        '    Qry &= vbCrLf & ", sum(ISNULL(BO.FNQuantity,0)) as FNQuantityDummyyy"
        '    Qry &= vbCrLf & "FROM           ( SELECT BO.*  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H WITH (NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTReturnSuplNo = BO.FTDocumentNo) AS BO INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId "
        '    Qry &= vbCrLf & "WHERE B.FTPurchaseNo = B.FTPurchaseNo"
        '    Qry &= vbCrLf & "group by B.FNHSysRawMatId , B.FTPurchaseNo) AS AA ON A.FNHSysRawMatId=AA.FNHSysRawMatId AND A.FTPurchaseNo=AA.FTPurchaseNo INNER JOIN"
        '    'จบงานซ่อม

        '    Qry &= vbCrLf & "(select  PO.FNHSysRawMatId,PO.FTPurchaseNo,O.FTOrderNo as OrderMin,R_D.FNQuantity ,P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType = 4  "
        '    Qry &= vbCrLf & "AND R.FNRceceiveType=0"
        '    Qry &= vbCrLf & ") AS B ON A.FNHSysRawMatId=B.FNHSysRawMatId and A.FTPurchaseNo=B.FTPurchaseNo and A.FDPurchaseDate=B.FDPurchaseDate  "
        'End If

        'Qry &= vbCrLf & ") AS K"
        'Qry &= vbCrLf & "INNER Join"

        'Qry &= vbCrLf & "(SELECT        com.FTCmpCode, Cus.FTCustCode, PO.FTOrderNo"
        'Qry &= vbCrLf & ",(select MIN(FDShipDate) from"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS with(nolock)"
        'Qry &= vbCrLf & "where OS.FTOrderNo = PO.FTOrderNo ) AS FDShipDate"
        'Qry &= vbCrLf & ",T.FTMerTeamCode, PO.FTPurchaseNo, M.FTRawMatCode,convert(varchar(10),convert(date,P.FDPurchaseDate),103) as cFDPurchaseDate "
        'Qry &= vbCrLf & " ,U.FTUnitCode, R_D.FNQuantity, PO.FNPrice,PO.FNHSysRawMatId, P.FDPurchaseDate,MC.FTRawMatColorCode,M.FTFabricFrontSize,MS.FTRawMatSizeCode"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    Qry &= vbCrLf & ", A.FTNameTH AS PoStateName"
        '    Qry &= vbCrLf & ",B.FTNameTH as FTMatType"
        '    Qry &= vbCrLf & ",M.FTRawMatNameTH AS FTRawMatName"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameTH AS FTRawMatColorName "
        'Else
        '    Qry &= vbCrLf & ", A.FTNameEN AS PoStateName"
        '    Qry &= vbCrLf & ",B.FTNameEN as FTMatType"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameEN AS FTRawMatColorName "
        '    Qry &= vbCrLf & ",M.FTRawMatNameEN AS FTRawMatName"
        'End If
        'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO WITH (NOLOCK) ON O.FTOrderNo = PO.FTOrderNo LEFT OUTER JOIN"
        ''new
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON PO.FTPurchaseNo = R.FTPurchaseNo INNER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS R_D ON PO.FTOrderNo = R_D.FTOrderNo AND PO.FNHSysRawMatId = R_D.FNHSysRawMatId AND R.FTReceiveNo = R_D.FTReceiveNo LEFT OUTER JOIN"

        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH (NOLOCK) ON PO.FTPurchaseNo = P.FTPurchaseNo LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS T WITH (NOLOCK) ON O.FNHSysMerTeamId = T.FNHSysMerTeamId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON PO.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS WITH(NOLOCK)  ON M.FNHSysRawMatSizeId=MS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON M.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON PO.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS Cus WITH (NOLOCK) ON O.FNHSysCustId = Cus.FNHSysCustId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS com WITH (NOLOCK) ON O.FNHSysCmpId = com.FNHSysCmpId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat as MM WITH (NOLOCK) ON M.FTRawMatCode=MM.FTMainMatCode LEFT OUTER JOIN"
        'Qry &= vbCrLf & "(select FNListIndex,FTNameTH,FTNameEN from "
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData  WITH(NOLOCK)"
        'Qry &= vbCrLf & "Where FTListName='FNPoState') AS A ON P.FNPoState=A.FNListIndex LEFT OUTER JOIN"
        'Qry &= vbCrLf & "(select FNListIndex,FTNameTH,FTNameEN from "
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WITH(NOLOCK)"
        'Qry &= vbCrLf & "WHERE FTListName='FNMerMatType'"
        'Qry &= vbCrLf & ") AS B ON MM.FNMerMatType=B.FNListIndex"



        'Qry &= vbCrLf & "group by com.FTCmpCode,Cus.FTCustCode,PO.FTOrderNo,T.FTMerTeamCode,PO.FTPurchaseNo"
        'Qry &= vbCrLf & ",M.FTRawMatCode"
        'Qry &= vbCrLf & ",U.FTUnitCode,R_D.FNQuantity, PO.FNPrice, PO.FNHSysRawMatId"
        'Qry &= vbCrLf & ",P.FDPurchaseDate,MC.FTRawMatColorCode,M.FTFabricFrontSize,MS.FTRawMatSizeCode"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    Qry &= vbCrLf & ", A.FTNameTH"
        '    Qry &= vbCrLf & ",B.FTNameTH"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameTH"
        '    Qry &= vbCrLf & ",M.FTRawMatNameTH"
        'Else
        '    Qry &= vbCrLf & ", A.FTNameEN"
        '    Qry &= vbCrLf & ",B.FTNameEN"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameEN "
        '    Qry &= vbCrLf & ",M.FTRawMatNameEN"
        'End If
        'Qry &= vbCrLf & ") AS H ON K.ORDERNOMIN=H.FTOrderNo and K.FTPurchaseNo=H.FTPurchaseNo and K.FNHSysRawMatId=H.FNHSysRawMatId and K.FDPurchaseDate=H.FDPurchaseDate"

        'Qry &= vbCrLf & "group by H.FTCmpCode, H.FTCustCode, H.FTOrderNo,FDShipDate,H.FTMerTeamCode, H.FTPurchaseNo, H.FTRawMatCode, H.FTRawMatColorCode,H.cFDPurchaseDate"
        'Qry &= vbCrLf & ",H.FTUnitCode,H.FNPrice,K.Allowcate,H.FTRawMatColorName,H.FTRawMatName,H.PoStateName,H.FTMatType,H.FTFabricFrontSize,H.FTRawMatSizeCode"

        'Qry &= vbCrLf & "order by H.FTOrderNo"



        'คำนวณจาก จำนวนซื้อ

        'Qry = "select H.FTCmpCode, H.FTCustCode, H.FTOrderNo,convert(varchar(10),convert(date,FDShipDate),103) AS FDShipDate"
        'Qry &= vbCrLf & ",H.FTMerTeamCode, H.FTPurchaseNo, H.FTRawMatCode, H.FTRawMatColorCode,H.cFDPurchaseDate"
        'Qry &= vbCrLf & ",H.FTUnitCode, sum(H.FNQuantity) as FNQuantity, H.FNPrice,H.FTRawMatColorName,H.FTRawMatName,PoStateName,FTMatType"
        'Qry &= vbCrLf & ",H.FTFabricFrontSize,H.FTRawMatSizeCode"
        'Qry &= vbCrLf & ",H.FNPrice*K.Allowcate as TotalMoney"
        'Qry &= vbCrLf & ",K.Allowcate as FNAllowcate from"

        'If (Me.FDPODateStart.Text <> "" Or Me.FDPODateEnd.Text <> "") And (Me.FTOrderNo.Text = "" And Me.FTOrderNoTo.Text = "") Then
        '    'อีกเคสสนึง ที่เอา FNOrderType =4 ขึ้นก่อน
        '    Qry &= vbCrLf & "("
        '    Qry &= vbCrLf & "SELECT b.FTPurchaseNo,b.FNHSysRawMatId,A.OrderMin,B.FTOrderNo AS ORDERNOMIN"
        '    Qry &= vbCrLf & ",b.FNQuantity AS QtyMIN"
        '    Qry &= vbCrLf & ",ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "PO.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) AS QTYNOMIN"
        '    Qry &= vbCrLf & ",A.FDPurchaseDate"
        '    Qry &= vbCrLf & ",case when b.FNQuantity=0 then b.FNQuantity else (b.FNQuantity*A.FNQuantity)/ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "PO.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) end AS Allowcate"
        '    Qry &= vbCrLf & "FROM"


        '    Qry &= vbCrLf & "(select "
        '    Qry &= vbCrLf & "PO.FTPurchaseNo, PO.FNHSysRawMatId, PO.FTOrderNo as OrderMin,"
        '    Qry &= vbCrLf & "PO.FNQuantity, P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType = 4"
        '    'ตรงนี้ใส่ criteria
        '    If FDPODateStart.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate>='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateStart.Text) & "'"
        '    End If
        '    If FDPODateEnd.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateEnd.Text) & "'"
        '    End If

        '    Qry &= vbCrLf & ") AS A INNER JOIN"
        '    Qry &= vbCrLf & "(select  PO.FNHSysRawMatId,PO.FTPurchaseNo,O.FTOrderNo,PO.FNQuantity ,P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType <> 4"
        '    Qry &= vbCrLf & ") AS B ON A.FNHSysRawMatId=B.FNHSysRawMatId and A.FTPurchaseNo=B.FTPurchaseNo and A.FDPurchaseDate=B.FDPurchaseDate  "
        'Else
        '    Qry &= vbCrLf & "("
        '    Qry &= vbCrLf & "SELECT b.FTPurchaseNo,b.FNHSysRawMatId,B.OrderMin,A.FTOrderNo AS ORDERNOMIN"
        '    Qry &= vbCrLf & ",b.FNQuantity AS QtyMIN"
        '    Qry &= vbCrLf & ",ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "PO.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) AS QTYNOMIN"
        '    Qry &= vbCrLf & ",A.FDPurchaseDate"
        '    Qry &= vbCrLf & ",case when a.FNQuantity=0 then a.FNQuantity else (b.FNQuantity*A.FNQuantity)/ISNULL((SELECT SUM("
        '    Qry &= vbCrLf & "PO.FNQuantity)"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & " where O.FNOrderType <> 4"
        '    Qry &= vbCrLf & "AND PO.FTPurchaseNo =B.FTPurchaseNo"
        '    Qry &= vbCrLf & "AND PO.FNHSysRawMatId= B.FNHSysRawMatId"
        '    Qry &= vbCrLf & "),0) end AS Allowcate"
        '    Qry &= vbCrLf & "FROM"


        '    Qry &= vbCrLf & "(select "
        '    Qry &= vbCrLf & "PO.FTPurchaseNo, PO.FNHSysRawMatId, PO.FTOrderNo,"
        '    Qry &= vbCrLf & "PO.FNQuantity, P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType <> 4"
        '    'ตรงนี้ใส่ criteria
        '    If FDPODateStart.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate>='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateStart.Text) & "'"
        '    End If
        '    If FDPODateEnd.Text <> "" Then
        '        Qry &= vbCrLf & "and P.FDPurchaseDate<='" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateEnd.Text) & "'"
        '    End If
        '    If FTOrderNo.Text <> "" Then
        '        Qry &= vbCrLf & "and O.FTOrderNO>='" & Me.FTOrderNo.Text & "'"
        '    End If
        '    If FTOrderNoTo.Text <> "" Then
        '        Qry &= vbCrLf & "and O.FTOrderNo<='" & Me.FTOrderNoTo.Text & "'"
        '    End If

        '    Qry &= vbCrLf & ") AS A INNER JOIN"
        '    Qry &= vbCrLf & "(select  PO.FNHSysRawMatId,PO.FTPurchaseNo,O.FTOrderNo as OrderMin,PO.FNQuantity ,P.FDPurchaseDate"
        '    Qry &= vbCrLf & "from"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo as PO WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS P WITH(NOLOCK) ON PO.FTPurchaseNo=P.FTPurchaseNo LEFT OUtER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder as O WITH(NOLOCK) ON PO.FTOrderNo=O.FTOrderNo"
        '    Qry &= vbCrLf & "where O.FNOrderType = 4"
        '    Qry &= vbCrLf & ") AS B ON A.FNHSysRawMatId=B.FNHSysRawMatId and A.FTPurchaseNo=B.FTPurchaseNo and A.FDPurchaseDate=B.FDPurchaseDate  "
        'End If

        'Qry &= vbCrLf & ") AS K"
        'Qry &= vbCrLf & "INNER Join"

        'Qry &= vbCrLf & "(SELECT        com.FTCmpCode, Cus.FTCustCode, PO.FTOrderNo"
        'Qry &= vbCrLf & ",(select MIN(FDShipDate) from"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS with(nolock)"
        'Qry &= vbCrLf & "where OS.FTOrderNo = PO.FTOrderNo ) AS FDShipDate"
        'Qry &= vbCrLf & ",T.FTMerTeamCode, PO.FTPurchaseNo, M.FTRawMatCode,convert(varchar(10),convert(date,P.FDPurchaseDate),103) as cFDPurchaseDate "
        'Qry &= vbCrLf & " ,U.FTUnitCode, PO.FNQuantity, PO.FNPrice,PO.FNHSysRawMatId, P.FDPurchaseDate,MC.FTRawMatColorCode,M.FTFabricFrontSize,MS.FTRawMatSizeCode"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    Qry &= vbCrLf & ", A.FTNameTH AS PoStateName"
        '    Qry &= vbCrLf & ",B.FTNameTH as FTMatType"
        '    Qry &= vbCrLf & ",M.FTRawMatNameTH AS FTRawMatName"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameTH AS FTRawMatColorName "
        'Else
        '    Qry &= vbCrLf & ", A.FTNameEN AS PoStateName"
        '    Qry &= vbCrLf & ",B.FTNameEN as FTMatType"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameEN AS FTRawMatColorName "
        '    Qry &= vbCrLf & ",M.FTRawMatNameEN AS FTRawMatName"
        'End If
        'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
        ''Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS OS WITH (NOLOCK) ON O.FTOrderNo = OS.FTOrderNo INNER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO WITH (NOLOCK) ON O.FTOrderNo = PO.FTOrderNo LEFT OUTER JOIN"
        ''Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.vwPOHeader as vv with(NOLOCK) ON PO.FTPurchaseNo=vv.FTPurchaseNo LEFT OUtER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH (NOLOCK) ON PO.FTPurchaseNo = P.FTPurchaseNo LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS T WITH (NOLOCK) ON O.FNHSysMerTeamId = T.FNHSysMerTeamId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON PO.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS WITH(NOLOCK)  ON M.FNHSysRawMatSizeId=MS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON M.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON PO.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS Cus WITH (NOLOCK) ON O.FNHSysCustId = Cus.FNHSysCustId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS com WITH (NOLOCK) ON O.FNHSysCmpId = com.FNHSysCmpId LEFT OUTER JOIN"
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat as MM WITH (NOLOCK) ON M.FTRawMatCode=MM.FTMainMatCode LEFT OUTER JOIN"
        'Qry &= vbCrLf & "(select FNListIndex,FTNameTH,FTNameEN from "
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData  WITH(NOLOCK)"
        'Qry &= vbCrLf & "Where FTListName='FNPoState') AS A ON P.FNPoState=A.FNListIndex LEFT OUTER JOIN"
        'Qry &= vbCrLf & "(select FNListIndex,FTNameTH,FTNameEN from "
        'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WITH(NOLOCK)"
        'Qry &= vbCrLf & "WHERE FTListName='FNMerMatType'"
        'Qry &= vbCrLf & ") AS B ON MM.FNMerMatType=B.FNListIndex"



        'Qry &= vbCrLf & "group by com.FTCmpCode,Cus.FTCustCode,PO.FTOrderNo,T.FTMerTeamCode,PO.FTPurchaseNo"
        'Qry &= vbCrLf & ",M.FTRawMatCode"
        'Qry &= vbCrLf & ",U.FTUnitCode,PO.FNQuantity, PO.FNPrice, PO.FNHSysRawMatId"
        'Qry &= vbCrLf & ",P.FDPurchaseDate,MC.FTRawMatColorCode,M.FTFabricFrontSize,MS.FTRawMatSizeCode"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    Qry &= vbCrLf & ", A.FTNameTH"
        '    Qry &= vbCrLf & ",B.FTNameTH"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameTH"
        '    Qry &= vbCrLf & ",M.FTRawMatNameTH"
        'Else
        '    Qry &= vbCrLf & ", A.FTNameEN"
        '    Qry &= vbCrLf & ",B.FTNameEN"
        '    Qry &= vbCrLf & ",M.FTRawMatColorNameEN "
        '    Qry &= vbCrLf & ",M.FTRawMatNameEN"
        'End If
        'Qry &= vbCrLf & ") AS H ON K.ORDERNOMIN=H.FTOrderNo and K.FTPurchaseNo=H.FTPurchaseNo and K.FNHSysRawMatId=H.FNHSysRawMatId and K.FDPurchaseDate=H.FDPurchaseDate"

        'Qry &= vbCrLf & "group by H.FTCmpCode, H.FTCustCode, H.FTOrderNo,FDShipDate,H.FTMerTeamCode, H.FTPurchaseNo, H.FTRawMatCode, H.FTRawMatColorCode,H.cFDPurchaseDate"
        'Qry &= vbCrLf & ",H.FTUnitCode,H.FNPrice,K.Allowcate,H.FTRawMatColorName,H.FTRawMatName,H.PoStateName,H.FTMatType,H.FTFabricFrontSize,H.FTRawMatSizeCode"

        'Qry &= vbCrLf & "order by H.FTOrderNo"

        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcDetail.DataSource = dt.Copy

        spls.Close()
    End Sub

    Private Function checkCriteria() As Boolean
        If Me.FTOrderNo.Text = "" And Me.FTOrderNoTo.Text = "" And Me.FDPODateEnd.Text = "" And Me.FDPODateStart.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If checkCriteria() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        ogcDetail.DataSource = Nothing
    End Sub


    Private Sub SETGridMergCell()
        For Each c As GridColumn In ogvDetail.Columns
            Select Case c.FieldName.ToString
                Case "FNAllowcate", "TotalMoney", "FNQuantity"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            End Select

        Next
    End Sub
End Class
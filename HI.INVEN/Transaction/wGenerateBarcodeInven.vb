Public Class wGenerateBarcodeInven

    Enum BarCodeType As Integer
        Adjust = 0
        Receive = 1
        Scrap = 2
        Conversion = 3
    End Enum

    Private _ProcGen As Boolean
    Public Property ProcGen As Boolean
        Get
            Return _ProcGen
        End Get
        Set(value As Boolean)
            _ProcGen = value
        End Set
    End Property

    Private _BarType As BarCodeType = BarCodeType.Receive
    Public Property BarType As BarCodeType
        Get
            Return _BarType
        End Get
        Set(value As BarCodeType)
            _BarType = value
        End Set
    End Property

    Private _DocumentNo As String = ""
    Public Property DocumentNo As String
        Get
            Return _DocumentNo
        End Get
        Set(value As String)
            _DocumentNo = value
        End Set
    End Property

    Private _MainObject As Object = Nothing
    Public Property MainObject As Object
        Get
            Return _MainObject
        End Get
        Set(value As Object)
            _MainObject = value
        End Set
    End Property

    Private _DefaultGenBarcode As Integer = 0
    Public Property DefaultGenBarcode As Integer
        Get
            Return _DefaultGenBarcode
        End Get
        Set(value As Integer)
            _DefaultGenBarcode = value
        End Set
    End Property

    Private Function LoadDataTable_Back() As DataTable

        Dim _Str As String = ""
        Dim _FoundOrderType As Boolean = False
        Select Case BarType
            Case BarCodeType.Receive

                _Str = " Select TOP 1  B.FTOrderNo"
                _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS B WITH(NOLOCK)  ON A.FTPurchaseNo = B.FTPurchaseNo INNER JOIN"
                _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
                _Str &= vbCrLf & " WHERE  (C.FNOrderType <> 4) "
                _Str &= vbCrLf & " AND (A.FTReceiveNo =N'" & HI.UL.ULF.rpQuoted(DocumentNo) & "')"
                _FoundOrderType = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")

                _Str = "     DECLARE @PONo nvarchar(30)"
                _Str &= vbCrLf & "  SET @PONO =''"
                _Str &= vbCrLf & " SELECT TOP 1 @PONO=FTPurchaseNo"
                _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK)"
                _Str &= vbCrLf & "  WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"
                _Str &= vbCrLf & "  CREATE TABLE #TabDummy("
                _Str &= vbCrLf & "  FTDocumentNo nvarchar(30),"
                _Str &= vbCrLf & " FTPurchaseNo nvarchar(30),"
                _Str &= vbCrLf & " FNHSysRawMatId int,"
                _Str &= vbCrLf & "  FTOrderNo nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantity numeric(18,4),"
                _Str &= vbCrLf & "  FTOrderNoRef nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantityPer numeric(18,6),"
                _Str &= vbCrLf & "  FNQuantitySpare numeric(18,4),"
                _Str &= vbCrLf & "  FTFabricFrontSize nvarchar(50),"
                _Str &= vbCrLf & "  FNHSysWHId int,"
                _Str &= vbCrLf & "  FNHSysUnitIdStock int,"
                _Str &= vbCrLf & "  FNPricePerStock numeric(18, 5),"
                _Str &= vbCrLf & "  FNQuantityStock numeric(18, 5)"
                _Str &= vbCrLf & "  )"


                _Str &= vbCrLf & "    CREATE TABLE #TabDummySpare("
                _Str &= vbCrLf & "  FTPurchaseNo nvarchar(30),"
                _Str &= vbCrLf & " FNHSysRawMatId int,"
                _Str &= vbCrLf & "  FTOrderNo nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantity numeric(18,4),"
                _Str &= vbCrLf & "  FTOrderNoRef nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantityPer numeric(18,6),"
                _Str &= vbCrLf & "  FNQuantitySpare numeric(18,4),"
                _Str &= vbCrLf & "  FNQuantitySpareTotalOrderRef numeric(18,4),"
                _Str &= vbCrLf & "   FNTotalBFQuantityPer numeric(18,4),"
                _Str &= vbCrLf & "    FNTotalBFQuantityPerAdd numeric(18, 4)"
                _Str &= vbCrLf & "  )"


                _Str &= vbCrLf & "  INSERT INTO #TabDummySpare(FTPurchaseNo"
                _Str &= vbCrLf & "  ,FNHSysRawMatId"
                _Str &= vbCrLf & "  ,FTOrderNo"
                _Str &= vbCrLf & "  ,FNQuantity"
                _Str &= vbCrLf & "  ,FTOrderNoRef"
                _Str &= vbCrLf & "  ,FNQuantityPer"
                _Str &= vbCrLf & "  ,FNQuantitySpare"
                _Str &= vbCrLf & "  ,FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & "  ,FNTotalBFQuantityPer"
                _Str &= vbCrLf & "  ,FNTotalBFQuantityPerAdd)"
                _Str &= vbCrLf & "  Select A.FTPurchaseNo"
                _Str &= vbCrLf & " ,A.FNHSysRawMatId "
                _Str &= vbCrLf & "  ,A.FTOrderNo "
                _Str &= vbCrLf & "   ,A.FNQuantityStock   "
                _Str &= vbCrLf & "   ,B.FTOrderNoRef "
                _Str &= vbCrLf & "   ,0"
                _Str &= vbCrLf & "    ,Convert(numeric(18,4),(A.FNQuantityStock  * B.FNQuantityOrder )/ B.FNOrderNoRefQuantity ) AS FNQuantitySpare"

                _Str &= vbCrLf & "   ,ISNULL((SELECT SUM(FNQuantity ) AS AA"
                _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK)"
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = A.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = A.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = A.FNHSysRawMatId) "
                _Str &= vbCrLf & "  AND AA.FTOrderNoRef =B.FTOrderNoRef "
                _Str &= vbCrLf & " ),0)"
                _Str &= vbCrLf & " 	- ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & " 	FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                _Str &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = A.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = A.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = A.FNHSysRawMatId) "
                _Str &= vbCrLf & "  AND AA.FTOrderNoRef =B.FTOrderNoRef "
                _Str &= vbCrLf & "  ),0 )"
                _Str &= vbCrLf & "  AS FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & " ,0,0"
                _Str &= vbCrLf & "  FROM (SELECT B.FTOrderNo "
                _Str &= vbCrLf & " ,P.FTPurchaseNo"
                _Str &= vbCrLf & " ,B.FNHSysRawMatId"
                _Str &= vbCrLf & " ,SUM(B.FNQuantityStock )	"
                _Str &= vbCrLf & " - ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                _Str &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = P.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = B.FTOrderNo) "
                _Str &= vbCrLf & "   AND  (AA.FNHSysRawMatId = B.FNHSysRawMatId) "
                _Str &= vbCrLf & "  	),0 )		"
                _Str &= vbCrLf & "  AS FNQuantityStock"
                _Str &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK) ON A.FTReceiveNo = B.FTReceiveNo INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH(NOLOCK) ON A.FTPurchaseNo = P.FTPurchaseNo "
                _Str &= vbCrLf & "   AND B.FTOrderNo = P.FTOrderNo "
                _Str &= vbCrLf & "   AND B.FNHSysRawMatId = P.FNHSysRawMatId"
                _Str &= vbCrLf & "   INNER Join"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE P.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) "
                _Str &= vbCrLf & "  GROUP BY B.FTOrderNo "
                _Str &= vbCrLf & " ,P.FTPurchaseNo"
                _Str &= vbCrLf & " ,B.FNHSysRawMatId"
                _Str &= vbCrLf & "  ) AS A"

                _Str &= vbCrLf & "  INNER JOIN (SELECT A.*,B.FTOrderNo AS FTOrderNoRef"
                _Str &= vbCrLf & "  , B.FNQuantity  AS FNQuantityOrder"
                _Str &= vbCrLf & "  ,C.FNQuantity AS FNOrderNoRefQuantity"
                _Str &= vbCrLf & "     FROM"
                _Str &= vbCrLf & "  (SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                _Str &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE A.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) ) AS A  INNER JOIN ("
                _Str &= vbCrLf & "   SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                _Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                _Str &= vbCrLf & "  ) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                _Str &= vbCrLf & "  INNER JOIN ("
                _Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId ,SUM(A.FNQuantity ) AS FNQuantity"
                _Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                _Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                _Str &= vbCrLf & "  GROUP BY A.FTPurchaseNo,A.FNHSysRawMatId "
                _Str &= vbCrLf & "  ) AS C ON B.FTPurchaseNo = C.FTPurchaseNo "
                _Str &= vbCrLf & "  AND B.FNHSysRawMatId = C.FNHSysRawMatId) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FTOrderNo = B.FTOrderNo  AND A.FNHSysRawMatId =B.FNHSysRawMatId "

                '_Str &= vbCrLf & "  Select XA.FTPurchaseNo"
                '_Str &= vbCrLf & " ,XA.FNHSysRawMatId"
                '_Str &= vbCrLf & " ,XA.FTOrderNo"
                '_Str &= vbCrLf & "  ,XA.FNQuantity"
                '_Str &= vbCrLf & "  ,XA.FTOrderNoRef"
                '' _Str &= vbCrLf & "  ,(XA.FNQuantityPer - ((FNQuantitySpareTotalOrderRef /FNQuantitySpare)*100)) AS FNQuantityPer"
                '_Str &= vbCrLf & "  ,(XA.FNQuantityPer - (CASE WHEN FNQuantitySpareTotalOrderRef > 0 THEN    ((XA.FNQuantityPer * FNQuantitySpareTotalOrderRef /FNQuantitySpare)) ELSE 0 END)) AS FNQuantityPer "
                '_Str &= vbCrLf & "  ,XA.FNQuantitySpare"
                '_Str &= vbCrLf & "  ,XA.FNQuantitySpareTotalOrderRef"
                ''_Str &= vbCrLf & "  ,((FNQuantitySpareTotalOrderRef /FNQuantitySpare)*100)  AS FNTotalBFQuantityPer"
                '_Str &= vbCrLf & "  ,(CASE WHEN FNQuantitySpareTotalOrderRef > 0 THEN    ((XA.FNQuantityPer * FNQuantitySpareTotalOrderRef /FNQuantitySpare)) ELSE 0 END)  AS FNTotalBFQuantityPer"
                '_Str &= vbCrLf & "  ,0.00"
                '_Str &= vbCrLf & " FROM (SELECT  X.FTPurchaseNo"
                '_Str &= vbCrLf & "  ,X.FNHSysRawMatId"
                '_Str &= vbCrLf & " ,X.FTOrderNo"
                '_Str &= vbCrLf & "  ,X.FNQuantity"
                '_Str &= vbCrLf & " ,X.FTOrderNoRef"
                '_Str &= vbCrLf & "  ,X.FNQuantityPer"
                '_Str &= vbCrLf & "  ,Convert(numeric(18,4),((X.FNQuantity * X.FNQuantityPer) /100.00 )) AS FNQuantitySpare"

                '_Str &= vbCrLf & "  ,ISNULL((SELECT SUM(FNQuantity ) AS AA"
                '_Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK)"
                '_Str &= vbCrLf & "     WHERE(AA.FTPurchaseNo = X.FTPurchaseNo)"
                '_Str &= vbCrLf & "  AND  (AA.FTOrderNo = X.FTOrderNo) "
                '_Str &= vbCrLf & "   AND  (AA.FNHSysRawMatId = X.FNHSysRawMatId) "
                '_Str &= vbCrLf & "   AND AA.FTOrderNoRef =X.FTOrderNoRef "
                '_Str &= vbCrLf & "   AND (AA.FTDocumentNo <> '" & HI.UL.ULF.rpQuoted(DocumentNo) & "')"

                '_Str &= vbCrLf & " 	),0)"

                '_Str &= vbCrLf & " 	- ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                '_Str &= vbCrLf & " 	FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & " 	         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                '_Str &= vbCrLf & " 	 INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                '_Str &= vbCrLf & "   WHERE(AA.FTPurchaseNo = x.FTPurchaseNo)"
                '_Str &= vbCrLf & "  AND  (AA.FTOrderNo = X.FTOrderNo) "
                '_Str &= vbCrLf & "   AND  (AA.FNHSysRawMatId = X.FNHSysRawMatId) "
                '_Str &= vbCrLf & "   AND AA.FTOrderNoRef =X.FTOrderNoRef "
                '_Str &= vbCrLf & "   AND (AA.FTDocumentNo <> '" & HI.UL.ULF.rpQuoted(DocumentNo) & "')"
                '_Str &= vbCrLf & " 	),0 )"


                '_Str &= vbCrLf & "  AS FNQuantitySpareTotalOrderRef"

                '_Str &= vbCrLf & "  FROM (SELECT A.*,B.FTOrderNo AS FTOrderNoRef"
                '_Str &= vbCrLf & "  ,Convert(numeric(18,6),(((  B.FNQuantity *100.00) / C.FNQuantity))) AS FNQuantityPer"

                '_Str &= vbCrLf & "      FROM"
                '_Str &= vbCrLf & "  (SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE A.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) ) AS A  INNER JOIN ("
                '_Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & " ) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                '_Str &= vbCrLf & " INNER JOIN ("
                '_Str &= vbCrLf & " SELECT A.FTPurchaseNo,A.FNHSysRawMatId ,SUM(A.FNQuantity ) AS FNQuantity"
                '_Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & " GROUP BY A.FTPurchaseNo,A.FNHSysRawMatId "
                '_Str &= vbCrLf & " ) AS C ON B.FTPurchaseNo = C.FTPurchaseNo AND B.FNHSysRawMatId = C.FNHSysRawMatId"
                '_Str &= vbCrLf & " ) AS X "
                '_Str &= vbCrLf & " ) XA"

                '_Str &= vbCrLf & " DECLARE @CountOrderSpareAdd numeric(18,4)"
                '_Str &= vbCrLf & " DECLARE @CountOrder int"
                '_Str &= vbCrLf & " SET @CountOrder =0"
                '_Str &= vbCrLf & " SET @CountOrderSpareAdd =0"

                '_Str &= vbCrLf & " SELECT @CountOrder =COUNT(FTOrderNoRef)"
                '_Str &= vbCrLf & " FROM #TabDummySpare"
                '_Str &= vbCrLf & "  WHERE FNTotalBFQuantityPer = 0"

                '_Str &= vbCrLf & " IF @CountOrder > 0"
                '_Str &= vbCrLf & "BEGIN"
                '_Str &= vbCrLf & " SELECT @CountOrderSpareAdd = Convert(numeric(18,4),ISNULL((SELECT SUM(FNTotalBFQuantityPer)"
                '_Str &= vbCrLf & " FROM #TabDummySpare"
                '_Str &= vbCrLf & "    WHERE FNTotalBFQuantityPer <> 0"
                '_Str &= vbCrLf & "    ),0)  /@CountOrder )"
                '_Str &= vbCrLf & " End"

                '_Str &= vbCrLf & " UPDATE A SET FNTotalBFQuantityPerAdd=@CountOrderSpareAdd"
                '_Str &= vbCrLf & " FROM #TabDummySpare AS A "
                '_Str &= vbCrLf & "  WHERE FNTotalBFQuantityPer = 0"

                _Str &= vbCrLf & "  INSERT INTO #TabDummy(FTDocumentNo,FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity"
                _Str &= vbCrLf & "   ,FTOrderNoRef,FNQuantityPer,FNQuantitySpare,FTFabricFrontSize,FNHSysWHId,FNHSysUnitIdStock,FNPricePerStock,FNQuantityStock)"
                _Str &= vbCrLf & "  SELECT M.FTDocumentNo,M.FTPurchaseNo,M.FNHSysRawMatId,M.FTOrderNo,M.FNQuantityStock,MM.FTOrderNoRef ,MM.FNQuantityPer "
                '_Str &= vbCrLf & "   ,Convert(numeric(18,4),((M.FNQuantityStock * MM.FNQuantityPer) /100.00 )) AS FNQuantitySpare"
                _Str &= vbCrLf & "   ,(FNQuantitySpare-MM.FNQuantitySpareTotalOrderRef )  AS FNQuantitySpare"
                _Str &= vbCrLf & "   ,M.FTFabricFrontSize,M.FNHSysWHId, M.FNHSysUnitIdStock"
                _Str &= vbCrLf & " , M.FNPricePerStock"
                _Str &= vbCrLf & " 	,M.FNQuantityStock"
                _Str &= vbCrLf & "  FROM (  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "    ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & "  ,H.FNHSysWHId"
                _Str &= vbCrLf & "   , A.FNHSysUnitIdStock"
                _Str &= vbCrLf & " , A.FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantityStock"
                _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo   INNER JOIN"
                _Str &= vbCrLf & " 	       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'  AND (O.FNOrderType = 4)"
                _Str &= vbCrLf & "   ) AS M"
                '_Str &= vbCrLf & "  INNER JOIN (SELECT  X.FTPurchaseNo"
                '_Str &= vbCrLf & "  ,X.FNHSysRawMatId"
                '_Str &= vbCrLf & "  ,X.FTOrderNo"
                '_Str &= vbCrLf & "  ,X.FNQuantity"
                '_Str &= vbCrLf & "  ,X.FTOrderNoRef"
                '_Str &= vbCrLf & "  ,X.FNQuantityPer"
                '_Str &= vbCrLf & "  ,Convert(numeric(18,4),((X.FNQuantity * X.FNQuantityPer) /100.00 )) AS FNQuantitySpare"
                '_Str &= vbCrLf & "  FROM (SELECT A.*,B.FTOrderNo AS FTOrderNoRef"
                '_Str &= vbCrLf & "   ,Convert(numeric(18,6),(((  B.FNQuantity *100.00) / C.FNQuantity))) AS FNQuantityPer"
                '_Str &= vbCrLf & "   FROM"
                '_Str &= vbCrLf & "  (SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE A.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) ) AS A  INNER JOIN ("
                '_Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "   AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & "  ) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                '_Str &= vbCrLf & "  INNER JOIN ("
                '_Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId ,SUM(A.FNQuantity ) AS FNQuantity"
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "   AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & "  GROUP BY A.FTPurchaseNo,A.FNHSysRawMatId "
                '_Str &= vbCrLf & " ) AS C ON B.FTPurchaseNo = C.FTPurchaseNo AND B.FNHSysRawMatId = C.FNHSysRawMatId) AS X"

                _Str &= vbCrLf & "  INNER JOIN (SELECT  X.FTPurchaseNo"
                _Str &= vbCrLf & "  ,X.FNHSysRawMatId"
                _Str &= vbCrLf & "  ,X.FTOrderNo"
                _Str &= vbCrLf & "  ,X.FNQuantity"
                _Str &= vbCrLf & "  ,X.FTOrderNoRef"
                _Str &= vbCrLf & "  ,(X.FNQuantityPer + X.FNTotalBFQuantityPerAdd) AS  FNQuantityPer "
                _Str &= vbCrLf & "  ,X.FNQuantitySpare,X.FNQuantitySpareTotalOrderRef "
                _Str &= vbCrLf & "  FROM #TabDummySpare  AS X"

                _Str &= vbCrLf & "  ) AS MM ON M.FTPurchaseNo = MM.FTPurchaseNo ANd M.FTOrderNo = MM.FTOrderNo AND M.FNHSysRawMatId = MM.FNHSysRawMatId "

            Case Else
                _Str = ""
        End Select

        _Str &= vbCrLf & "  SELECT FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTPurchaseNo,FTOrderNo,FNHSysRawMatId,FNHSysUnitId"
        _Str &= vbCrLf & " 	,FNHSysWHId,FNQuantityStock,FNPricePerStock,FTUnitCode,FNBarCodeQty"
        '_Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNBarcodeBalance"

        _Str &= vbCrLf & "	, CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END AS FNBarcodeBalance"
        _Str &= vbCrLf & "	,0 AS FNQtyBarcode"
        '_Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNGenBarcodeQty"
        _Str &= vbCrLf & "	, CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END AS FNGenBarcodeQty"
        ' _Str &= vbCrLf & "	,0.0000  AS FNGenBarcodeQty"
        _Str &= vbCrLf & "	,'' AS FTBatchNo"
        _Str &= vbCrLf & "	,'' AS FTGrade"
        _Str &= vbCrLf & "	,'' AS FTRollNo,FTOrderNoRef,FNSeqRef"
        _Str &= vbCrLf & "  FROM   ("
        _Str &= vbCrLf & "  Select IM.FTRawMatCode"
        _Str &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & "  ,ISNULL( S.FTRawMatSizeCode ,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & "  , A.FTFabricFrontSize"
        _Str &= vbCrLf & "  , A.FTPurchaseNo"
        _Str &= vbCrLf & "  , A.FTOrderNo"
        _Str &= vbCrLf & "  , A.FNHSysRawMatId"
        _Str &= vbCrLf & " 	, A.FNHSysWHId"
        _Str &= vbCrLf & " 	, A.FNQuantityStock"
        _Str &= vbCrLf & " 	, A.FNHSysUnitIdStock AS FNHSysUnitId"
        _Str &= vbCrLf & "  , A.FNPricePerStock"
        _Str &= vbCrLf & "  , U.FTUnitCode"
        _Str &= vbCrLf & " 	, ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & " 		  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FTDocumentNo = A.FTDocumentNo "
        _Str &= vbCrLf & " 		AND  FNHSysRawMatId = A.FNHSysRawMatId"
        _Str &= vbCrLf & " 		AND  FTOrderNo = A.FTOrderNo"
        _Str &= vbCrLf & " 		AND  FTPurchaseNo = A.FTPurchaseNo"
        _Str &= vbCrLf & " 		AND  ISNULL(FTOrderNoRef,'') = A.FTOrderNoRef"
        _Str &= vbCrLf & " 		AND  ISNULL(FNSeqRef,0) = A.FNSeqRef"
        _Str &= vbCrLf & "  	 ),0) AS FNBarCodeQty"

        _Str &= vbCrLf & " , C.FNRawMatColorSeq, S.FNRawMatSizeSeq,A.FTOrderNoRef,A.FNSeqRef "
        _Str &= vbCrLf & "  FROM           ( "

        Select Case BarType
            Case BarCodeType.Receive

                _Str &= vbCrLf & "  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  , A.FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantityStock,'' AS FTOrderNoRef,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo  "
                _Str &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"

                _Str &= vbCrLf & " LEFT OUTER JOIN ("
                _Str &= vbCrLf & " SELECT FTOrderNo,FNHSysRawMatId "
                _Str &= vbCrLf & "  FROM #TabDummy "
                _Str &= vbCrLf & " GROUP BY FTOrderNo,FNHSysRawMatId"
                _Str &= vbCrLf & "  ) XSA ON A.FNHSysRawMatId = XSA.FNHSysRawMatId AND A.FTOrderNo = XSA.FTOrderNo  "

                _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "' "
                _Str &= vbCrLf & "  AND XSA.FTOrderNo IS NULL "

                'If (_FoundOrderType) Then
                '    _Str &= vbCrLf & " AND (ISNULL(O.FNOrderType,0) <> 4)"
                'End If

                _Str &= vbCrLf & "  UNION "
                _Str &= vbCrLf & "   SELECT  A.FTDocumentNo,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & ",  A.FTFabricFrontSize"
                _Str &= vbCrLf & "	,A.FNHSysRawMatId"
                _Str &= vbCrLf & "	,A.FNHSysWHId"
                _Str &= vbCrLf & "  ,A.FNHSysUnitIdStock"
                _Str &= vbCrLf & "  ,A.FNPricePerStock"
                '  _Str &= vbCrLf & "  ,(A.FNQuantitySpare + (A.FNQuantity - ISNULL(B.FNQuantityTotalSpare,0))) AS FNQuantitySpare"
                '_Str &= vbCrLf & "  ,(A.FNQuantitySpare +  CASE WHEN  ISNULL(B.FNQuantityTotalSpare,0) =0 THEN 0 ELSE  ((A.FNQuantity - ISNULL(B.FNQuantityTotalSpare,0))) END) AS FNQuantitySpare"
                _Str &= vbCrLf & "  ,A.FNQuantitySpare AS FNQuantitySpare"
                _Str &= vbCrLf & "  ,A.FTOrderNoRef ,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM #TabDummy AS A  LEFT OUTER JOIN ("
                _Str &= vbCrLf & "	SELECT FTDocumentNo,FNHSysRawMatId,FTOrderNo"
                _Str &= vbCrLf & "  ,MAX(FTOrderNoRef) AS FTOrderNoRef"
                _Str &= vbCrLf & "  , SUM(FNQuantitySpare) AS FNQuantityTotalSpare"
                _Str &= vbCrLf & "  FROM #TabDummy "
                _Str &= vbCrLf & "	GROUP BY FTDocumentNo,FNHSysRawMatId,FTOrderNo"
                _Str &= vbCrLf & "  ) AS B ON A.FTDocumentNo = B.FTDocumentNo"
                _Str &= vbCrLf & "   AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                _Str &= vbCrLf & "	AND A.FTOrderNo = B.FTOrderNo"
                _Str &= vbCrLf & "	AND A.FTOrderNoRef = B.FTOrderNoRef"

            Case BarCodeType.Adjust

                _Str &= vbCrLf & "  SELECT H.FTAdjustStockNo AS FTDocumentNo ,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  , A.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPrice AS FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantity AS FNQuantityStock,'' AS FTOrderNoRef ,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock  AS  H WITH(NOLOCK) ON A.FTAdjustStockNo = H.FTAdjustStockNo  "
                _Str &= vbCrLf & "  WHERE A.FTAdjustStockNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"

            Case BarCodeType.Scrap

                _Str &= vbCrLf & "   SELECT        FTScrapNo AS FTDocumentNo , FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & ",Convert(numeric(18," & Val(HI.ST.Config.PriceDigit) & "),Amount / FNQuantity) as FNPricePerStock"
                _Str &= vbCrLf & "	,  FNQuantity AS FNQuantityStock,'' AS FTOrderNoRef,0 AS FNSeqRef"
                _Str &= vbCrLf & " FROM            (SELECT        FTScrapNo, FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId, SUM(FNQuantity) AS FNQuantity, "
                _Str &= vbCrLf & "  SUM(Amount) AS Amount"
                _Str &= vbCrLf & "  FROM            (SELECT        H.FTScrapNo, B.FTPurchaseNo, BO.FTOrderNo, B.FTFabricFrontSize, B.FNHSysRawMatId, BO.FNHSysWHId, B.FNHSysUnitId, ISNULL(BO.FNPriceTrans,B.FNPrice) AS FNPrice, "
                _Str &= vbCrLf & "    BO.FNQuantity, CONVERT(NUMERIC(18, 2), ISNULL(BO.FNPriceTrans,B.FNPrice) * BO.FNQuantity) AS Amount"
                _Str &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENScrapBarcode AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTScrapNo = BO.FTDocumentNo INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
                _Str &= vbCrLf & "  WHERE FTScrapNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "' ) AS A"
                _Str &= vbCrLf & "  GROUP BY FTScrapNo, FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId) AS B"

            Case BarCodeType.Conversion

                _Str &= vbCrLf & "  SELECT H.FTConversionNo AS FTDocumentNo ,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  ,IM.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPrice AS FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantity AS  FNQuantityStock,'' AS FTOrderNoRef,A.FNSeq AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail_Finish AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion  AS  H WITH(NOLOCK) ON A.FTConversionNo = H.FTConversionNo  "
                _Str &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId "
                _Str &= vbCrLf & "  WHERE A.FTConversionNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"

        End Select

        _Str &= vbCrLf & "  ) AS A"
        _Str &= vbCrLf & "   INNER Join"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitIdStock = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & "  ) AS M"
        ' _Str &= vbCrLf & " WHERE (FNQuantityStock-FNBarCodeQty) >0"
        _Str &= vbCrLf & " WHERE ( CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END) >0 "
        '_Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTOrderNo"
        '_Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FNRawMatColorSeq,FNRawMatSizeSeq,FTFabricFrontSize,FTOrderNo"
        _Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FNRawMatSizeSeq,FTFabricFrontSize,FTOrderNo"

        Select Case BarType
            Case BarCodeType.Receive
                _Str &= vbCrLf & " DROP TABLE #TabDummy "
                _Str &= vbCrLf & " DROP TABLE #TabDummySpare "

            Case Else
        End Select

        Return HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Private Function LoadDataTable() As DataTable

        Me.DefaultGenBarcode = 1

        Dim _Str As String = ""
        Dim _FoundOrderType As Boolean = False
        Select Case BarType
            Case BarCodeType.Receive

                _Str = " Select TOP 1  B.FTOrderNo"
                _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS B WITH(NOLOCK)  ON A.FTPurchaseNo = B.FTPurchaseNo INNER JOIN"
                _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
                _Str &= vbCrLf & " WHERE  (C.FNOrderType <> 4) "
                _Str &= vbCrLf & " AND (A.FTReceiveNo =N'" & HI.UL.ULF.rpQuoted(DocumentNo) & "')"
                _FoundOrderType = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")

                _Str = "     DECLARE @PONo nvarchar(30)"
                _Str &= vbCrLf & "  SET @PONO =''"
                _Str &= vbCrLf & " SELECT TOP 1 @PONO=FTPurchaseNo"
                _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK)"
                _Str &= vbCrLf & "  WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"
                _Str &= vbCrLf & "  Declare @TabDummy AS TABLE("
                _Str &= vbCrLf & "  FTDocumentNo nvarchar(30),"
                _Str &= vbCrLf & " FTPurchaseNo nvarchar(30),"
                _Str &= vbCrLf & " FNHSysRawMatId int,"
                _Str &= vbCrLf & "  FTOrderNo nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantity numeric(18,4),"
                _Str &= vbCrLf & "  FTOrderNoRef nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantityPer numeric(18,6),"
                _Str &= vbCrLf & "  FNQuantitySpare numeric(18,4),"
                _Str &= vbCrLf & "  FTFabricFrontSize nvarchar(50),"
                _Str &= vbCrLf & "  FNHSysWHId int,"
                _Str &= vbCrLf & "  FNHSysUnitIdStock int,"
                _Str &= vbCrLf & "  FNPricePerStock numeric(18, 5),"
                _Str &= vbCrLf & "  FNQuantityStock numeric(18, 5)"
                _Str &= vbCrLf & "  )"


                _Str &= vbCrLf & "   Declare @TabDummySpareTmp AS Table("
                _Str &= vbCrLf & "  FTPurchaseNo nvarchar(30),FNSeq int,"
                _Str &= vbCrLf & " FNHSysRawMatId int,"
                _Str &= vbCrLf & "  FTOrderNo nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantity numeric(18,4),"
                _Str &= vbCrLf & "  FTOrderNoRef nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantityPer numeric(18,6),"
                _Str &= vbCrLf & "  FNQuantitySpare numeric(18,4),"
                _Str &= vbCrLf & "  FNQuantitySpareTotalOrderRef numeric(18,4),"
                _Str &= vbCrLf & "   FNTotalBFQuantityPer numeric(18,4),"
                _Str &= vbCrLf & "    FNTotalBFQuantityPerAdd numeric(18, 4)"
                _Str &= vbCrLf & " , FNQuantitySpareOrg numeric(18,4)"
                _Str &= vbCrLf & "  , FNQuantityStockBarcode numeric(18,4)"
                _Str &= vbCrLf & " , FNQuantityStockRts numeric(18,4)"
                _Str &= vbCrLf & " , FNQuantityStockBarcodeOther numeric(18,4)"
                _Str &= vbCrLf & " , FNQuantityStockRtsOther numeric(18,4)"
                _Str &= vbCrLf & "  )"

                _Str &= vbCrLf & "    Declare @TabDummySpare AS Table("
                _Str &= vbCrLf & "  FTPurchaseNo nvarchar(30),FNSeq int,"
                _Str &= vbCrLf & " FNHSysRawMatId int,"
                _Str &= vbCrLf & "  FTOrderNo nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantity numeric(18,4),"
                _Str &= vbCrLf & "  FTOrderNoRef nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantityPer numeric(18,6),"
                _Str &= vbCrLf & "  FNQuantitySpare numeric(18,4),"
                _Str &= vbCrLf & "  FNQuantitySpareTotalOrderRef numeric(18,4),"
                _Str &= vbCrLf & "   FNTotalBFQuantityPer numeric(18,4),"
                _Str &= vbCrLf & "    FNTotalBFQuantityPerAdd numeric(18, 4)"
                _Str &= vbCrLf & " , FNQuantitySpareOrg numeric(18,4)"
                _Str &= vbCrLf & "  , FNQuantityStockBarcode numeric(18,4)"
                _Str &= vbCrLf & " , FNQuantityStockRts numeric(18,4)"
                _Str &= vbCrLf & " , FNQuantityStockBarcodeOther numeric(18,4)"
                _Str &= vbCrLf & " , FNQuantityStockRtsOther numeric(18,4)"
                _Str &= vbCrLf & "  )"


                _Str &= vbCrLf & "  INSERT INTO @TabDummySpareTmp(FTPurchaseNo,FNSeq"
                _Str &= vbCrLf & "  ,FNHSysRawMatId"
                _Str &= vbCrLf & "  ,FTOrderNo"
                _Str &= vbCrLf & "  ,FNQuantity"
                _Str &= vbCrLf & "  ,FTOrderNoRef"
                _Str &= vbCrLf & "  ,FNQuantityPer"
                _Str &= vbCrLf & "  ,FNQuantitySpare"
                _Str &= vbCrLf & "  ,FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & "  ,FNTotalBFQuantityPer"
                _Str &= vbCrLf & "  ,FNTotalBFQuantityPerAdd"
                _Str &= vbCrLf & ",FNQuantitySpareOrg"
                _Str &= vbCrLf & ",FNQuantityStockBarcode"
                _Str &= vbCrLf & ",FNQuantityStockRts"
                _Str &= vbCrLf & ",FNQuantityStockBarcodeOther"
                _Str &= vbCrLf & ",FNQuantityStockRtsOther"
                _Str &= vbCrLf & ")"
                _Str &= vbCrLf & "  Select A.FTPurchaseNo ,Row_Number () Over (Order By B.FTOrderNoRef ) AS FNSeq"
                _Str &= vbCrLf & " ,A.FNHSysRawMatId "
                _Str &= vbCrLf & "  ,A.FTOrderNo "
                _Str &= vbCrLf & "   ,A.FNQuantityStock   "
                _Str &= vbCrLf & "   ,B.FTOrderNoRef "
                _Str &= vbCrLf & "   ,0"
                _Str &= vbCrLf & "    ,Convert(numeric(18,5),(((A.FNQuantityStock + A.FNQuantityStockOther)-(A.FNQuantityStockRts+A.FNQuantityStockRtsOther)  )   * B.FNQuantityOrder )/ B.FNOrderNoRefQuantity ) AS FNQuantitySpare"

                _Str &= vbCrLf & "   ,ISNULL((SELECT SUM(FNQuantity ) AS AA"
                _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK)"
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = A.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = A.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = A.FNHSysRawMatId) "
                _Str &= vbCrLf & "  AND AA.FTOrderNoRef =B.FTOrderNoRef "
                _Str &= vbCrLf & " ),0)"
                _Str &= vbCrLf & " 	- ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & " 	FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                _Str &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = A.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = A.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = A.FNHSysRawMatId) "
                _Str &= vbCrLf & "  AND AA.FTOrderNoRef =B.FTOrderNoRef "
                _Str &= vbCrLf & "  ),0 )"
                _Str &= vbCrLf & "  AS FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & " ,0,0,Convert(numeric(18,5),((A.FNQuantityStock- A.FNQuantityStockBarcode)  * B.FNQuantityOrder )/ B.FNOrderNoRefQuantity ) AS FNQuantitySpareOrg"
                _Str &= vbCrLf & "  ,FNQuantityStockBarcode"
                _Str &= vbCrLf & " ,FNQuantityStockRts"
                _Str &= vbCrLf & " ,FNQuantityStockBarcodeOther"
                _Str &= vbCrLf & "  ,FNQuantityStockRtsOther"
                _Str &= vbCrLf & "  FROM ("

                _Str &= vbCrLf & " SELECT   A1.FTPurchaseNo"
                _Str &= vbCrLf & ",A1.FNHSysRawMatId"
                _Str &= vbCrLf & ",A1.FTOrderNo"
                _Str &= vbCrLf & "	,A1.FNQuantityStock "
                _Str &= vbCrLf & ",ISNULL(A2.FNQuantityStock,0) as FNQuantityStockOther"
                _Str &= vbCrLf & "   ,A1.FNQuantityStockBarcode"
                _Str &= vbCrLf & ",A1.FNQuantityStockRts "
                _Str &= vbCrLf & ",ISNULL(A2.FNQuantityStockBarcode,0) AS FNQuantityStockBarcodeOther"
                _Str &= vbCrLf & ",ISNULL(A2.FNQuantityStockRts,0) AS  FNQuantityStockRtsOther"

                _Str &= vbCrLf & " FROM (SELECT B.FTOrderNo "
                _Str &= vbCrLf & ",P.FTPurchaseNo"
                _Str &= vbCrLf & ",B.FNHSysRawMatId"
                _Str &= vbCrLf & ",SUM(B.FNQuantityStock ) AS FNQuantityStock	"

                _Str &= vbCrLf & "  ,ISNULL((SELECT SUM(AA.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) "
                _Str &= vbCrLf & "  WHERE(AA.FTPurchaseNo = P.FTPurchaseNo)"
                _Str &= vbCrLf & "AND  (AA.FTOrderNo = B.FTOrderNo) "
                _Str &= vbCrLf & "AND  (AA.FNHSysRawMatId = B.FNHSysRawMatId) "
                _Str &= vbCrLf & "AND AA.FTDocumentNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"
                _Str &= vbCrLf & "),0 )		"
                _Str &= vbCrLf & "AS FNQuantityStockBarcode"

                _Str &= vbCrLf & ", ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & "FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                _Str &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                _Str &= vbCrLf & "  WHERE(AA.FTPurchaseNo = P.FTPurchaseNo)"
                _Str &= vbCrLf & " AND  (AA.FTOrderNo = B.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = B.FNHSysRawMatId) "
                _Str &= vbCrLf & " AND AA.FTDocumentNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"
                _Str &= vbCrLf & "	),0 )		"
                _Str &= vbCrLf & " AS FNQuantityStockRts"
                _Str &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK) ON A.FTReceiveNo = B.FTReceiveNo INNER JOIN"
                _Str &= vbCrLf & " [HITECH_PURCHASE].dbo.TPURTPurchase_OrderNo AS P WITH(NOLOCK) ON A.FTPurchaseNo = P.FTPurchaseNo "
                _Str &= vbCrLf & "AND B.FTOrderNo = P.FTOrderNo "
                _Str &= vbCrLf & " AND B.FNHSysRawMatId = P.FNHSysRawMatId"
                _Str &= vbCrLf & "  INNER Join"
                _Str &= vbCrLf & " [HITECH_MERCHAN].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & " WHERE P.FTPurchaseNo=@PONO AND A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "' AND (O.FNOrderType = 4) "
                _Str &= vbCrLf & "GROUP BY B.FTOrderNo "
                _Str &= vbCrLf & ",P.FTPurchaseNo"
                _Str &= vbCrLf & ",B.FNHSysRawMatId"
                _Str &= vbCrLf & ") AS A1 LEFT OUTER JOIN "


                _Str &= vbCrLf & " (SELECT B.FTOrderNo "
                _Str &= vbCrLf & " ,P.FTPurchaseNo"
                _Str &= vbCrLf & ",B.FNHSysRawMatId"
                _Str &= vbCrLf & ",SUM(B.FNQuantityStock ) AS FNQuantityStock	"

                _Str &= vbCrLf & "  ,ISNULL((SELECT SUM(AA.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) "
                _Str &= vbCrLf & "  WHERE(AA.FTPurchaseNo = P.FTPurchaseNo)"
                _Str &= vbCrLf & "AND  (AA.FTOrderNo = B.FTOrderNo) "
                _Str &= vbCrLf & "AND  (AA.FNHSysRawMatId = B.FNHSysRawMatId) "
                _Str &= vbCrLf & "AND AA.FTDocumentNo <>'" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"
                _Str &= vbCrLf & "),0 )		"
                _Str &= vbCrLf & "AS FNQuantityStockBarcode"
                _Str &= vbCrLf & ",ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                _Str &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                _Str &= vbCrLf & "   WHERE(AA.FTPurchaseNo = P.FTPurchaseNo)"
                _Str &= vbCrLf & "AND  (AA.FTOrderNo = B.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = B.FNHSysRawMatId) "
                _Str &= vbCrLf & " AND AA.FTDocumentNo <>'" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"
                _Str &= vbCrLf & "	),0 )		"
                _Str &= vbCrLf & "AS FNQuantityStockRts"
                _Str &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK) ON A.FTReceiveNo = B.FTReceiveNo INNER JOIN"
                _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH(NOLOCK) ON A.FTPurchaseNo = P.FTPurchaseNo "
                _Str &= vbCrLf & " AND B.FTOrderNo = P.FTOrderNo "
                _Str &= vbCrLf & " AND B.FNHSysRawMatId = P.FNHSysRawMatId"
                _Str &= vbCrLf & "  INNER Join"
                _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "WHERE P.FTPurchaseNo=@PONO AND A.FTReceiveNo <>'" & HI.UL.ULF.rpQuoted(DocumentNo) & "' AND (O.FNOrderType = 4) "
                _Str &= vbCrLf & " GROUP BY B.FTOrderNo "
                _Str &= vbCrLf & " ,P.FTPurchaseNo"
                _Str &= vbCrLf & " ,B.FNHSysRawMatId) AS A2 ON A1.FTPurchaseNo = A2.FTPurchaseNo AND A1.FTOrderNo = A2.FTOrderNo AND A1.FNHSysRawMatId =A2.FNHSysRawMatId "


                _Str &= vbCrLf & "  ) AS A"
                _Str &= vbCrLf & "  INNER JOIN (SELECT A.*,B.FTOrderNo AS FTOrderNoRef"
                _Str &= vbCrLf & "  , B.FNQuantity  AS FNQuantityOrder"
                _Str &= vbCrLf & "  ,C.FNQuantity AS FNOrderNoRefQuantity"
                _Str &= vbCrLf & "     FROM"
                _Str &= vbCrLf & "  (SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                _Str &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE A.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) ) AS A  INNER JOIN ("
                _Str &= vbCrLf & "   SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                _Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                _Str &= vbCrLf & "  ) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                _Str &= vbCrLf & "  INNER JOIN ("
                _Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId ,SUM(A.FNQuantity ) AS FNQuantity"
                _Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                _Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                _Str &= vbCrLf & "  GROUP BY A.FTPurchaseNo,A.FNHSysRawMatId "
                _Str &= vbCrLf & "  ) AS C ON B.FTPurchaseNo = C.FTPurchaseNo "
                _Str &= vbCrLf & "  AND B.FNHSysRawMatId = C.FNHSysRawMatId) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FTOrderNo = B.FTOrderNo  AND A.FNHSysRawMatId =B.FNHSysRawMatId "

                _Str &= vbCrLf & "  INSERT INTO @TabDummySpare(FTPurchaseNo"
                _Str &= vbCrLf & ",FNSeq"
                _Str &= vbCrLf & ",FNHSysRawMatId"
                _Str &= vbCrLf & ",FTOrderNo"
                _Str &= vbCrLf & ",FNQuantity"
                _Str &= vbCrLf & ",FTOrderNoRef"
                _Str &= vbCrLf & " ,FNQuantityPer"
                _Str &= vbCrLf & ",FNQuantitySpare"
                _Str &= vbCrLf & ",FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & ",FNTotalBFQuantityPer"
                _Str &= vbCrLf & ",FNTotalBFQuantityPerAdd"
                _Str &= vbCrLf & ",FNQuantitySpareOrg"
                _Str &= vbCrLf & ",FNQuantityStockBarcode"
                _Str &= vbCrLf & ",FNQuantityStockRts"
                _Str &= vbCrLf & ",FNQuantityStockBarcodeOther"
                _Str &= vbCrLf & ",FNQuantityStockRtsOther"
                _Str &= vbCrLf & ")"
                _Str &= vbCrLf & "   Select  A.FTPurchaseNo"
                _Str &= vbCrLf & ",A.FNSeq"
                _Str &= vbCrLf & ",A.FNHSysRawMatId"
                _Str &= vbCrLf & " ,A.FTOrderNo"
                _Str &= vbCrLf & ",A.FNQuantity"
                _Str &= vbCrLf & " ,A.FTOrderNoRef"
                _Str &= vbCrLf & " ,A.FNQuantityPer"
                _Str &= vbCrLf & " ,A.FNQuantitySpare + ISNULL(B.FNQuantitySpareDiff,0) AS FNQuantitySpare"
                _Str &= vbCrLf & " ,A.FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & " ,A.FNTotalBFQuantityPer"
                _Str &= vbCrLf & ",A.FNTotalBFQuantityPerAdd"
                _Str &= vbCrLf & ",A.FNQuantitySpareOrg + ISNULL(B.FNQuantitySpareDiff,0) AS FNQuantitySpareOrg"
                _Str &= vbCrLf & ",A.FNQuantityStockBarcode"
                _Str &= vbCrLf & ",A.FNQuantityStockRts"
                _Str &= vbCrLf & ",A.FNQuantityStockBarcodeOther"
                _Str &= vbCrLf & ",A.FNQuantityStockRtsOther"
                _Str &= vbCrLf & "FROM @TabDummySpareTmp AS A LEFT OUTER JOIN (SELECT FTPurchaseNo"
                _Str &= vbCrLf & ",MAX(FNSeq) AS FNSeq"
                _Str &= vbCrLf & ",FNHSysRawMatId "
                _Str &= vbCrLf & ",FTOrderNo"
                _Str &= vbCrLf & ",FNQuantity - SUM(FNQuantitySpareOrg +FNQuantityStockBarcode ) AS FNQuantitySpareDiff"
                _Str &= vbCrLf & "FROM @TabDummySpareTmp"
                _Str &= vbCrLf & "   WHERE FNQuantitySpare > 0"
                _Str &= vbCrLf & "  GROUP BY"
                _Str &= vbCrLf & "   FTPurchaseNo"
                _Str &= vbCrLf & ",FNHSysRawMatId "
                _Str &= vbCrLf & ",FTOrderNo"
                _Str &= vbCrLf & ",FNQuantity"
                _Str &= vbCrLf & " ) AS B  ON A.FTPurchaseNo= B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId AND A.FNSeq = B.FNSeq"

                _Str &= vbCrLf & "  INSERT INTO @TabDummy(FTDocumentNo,FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity"
                _Str &= vbCrLf & "   ,FTOrderNoRef,FNQuantityPer,FNQuantitySpare,FTFabricFrontSize,FNHSysWHId,FNHSysUnitIdStock,FNPricePerStock,FNQuantityStock)"
                _Str &= vbCrLf & "  SELECT M.FTDocumentNo,M.FTPurchaseNo,M.FNHSysRawMatId,M.FTOrderNo,M.FNQuantityStock,MM.FTOrderNoRef ,MM.FNQuantityPer "
                '_Str &= vbCrLf & "   ,Convert(numeric(18,4),((M.FNQuantityStock * MM.FNQuantityPer) /100.00 )) AS FNQuantitySpare"
                _Str &= vbCrLf & "   ,(CASE  WHEN FNQuantitySpareOrg < FNQuantitySpare THEN FNQuantitySpareOrg ELSE FNQuantitySpare-MM.FNQuantitySpareTotalOrderRef END)  AS FNQuantitySpare"
                _Str &= vbCrLf & "   ,M.FTFabricFrontSize,M.FNHSysWHId, M.FNHSysUnitIdStock"
                _Str &= vbCrLf & " , M.FNPricePerStock"
                _Str &= vbCrLf & " 	,M.FNQuantityStock"
                _Str &= vbCrLf & "  FROM (  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "    ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & "  ,H.FNHSysWHId"
                _Str &= vbCrLf & "   , A.FNHSysUnitIdStock"
                _Str &= vbCrLf & " , A.FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantityStock"
                _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo   INNER JOIN"
                _Str &= vbCrLf & " 	       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'  AND (O.FNOrderType = 4)"
                _Str &= vbCrLf & "   ) AS M"
                _Str &= vbCrLf & "  INNER JOIN (SELECT  X.FTPurchaseNo"
                _Str &= vbCrLf & "  ,X.FNHSysRawMatId"
                _Str &= vbCrLf & "  ,X.FTOrderNo"
                _Str &= vbCrLf & "  ,X.FNQuantity"
                _Str &= vbCrLf & "  ,X.FTOrderNoRef"
                _Str &= vbCrLf & "  ,(X.FNQuantityPer + X.FNTotalBFQuantityPerAdd) AS  FNQuantityPer "
                _Str &= vbCrLf & "  ,X.FNQuantitySpare,X.FNQuantitySpareTotalOrderRef,X.FNQuantitySpareOrg  "
                _Str &= vbCrLf & "  FROM @TabDummySpare  AS X"
                _Str &= vbCrLf & "  ) AS MM ON M.FTPurchaseNo = MM.FTPurchaseNo ANd M.FTOrderNo = MM.FTOrderNo AND M.FNHSysRawMatId = MM.FNHSysRawMatId "

            Case Else
                _Str = ""
        End Select

        _Str &= vbCrLf & "  SELECT FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTPurchaseNo,FTOrderNo,FNHSysRawMatId,FNHSysUnitId"
        _Str &= vbCrLf & " 	,FNHSysWHId,FNQuantityStock,FNPricePerStock,FTUnitCode,FNBarCodeQty"
        '_Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNBarcodeBalance"

        _Str &= vbCrLf & "	, CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END AS FNBarcodeBalance"
        _Str &= vbCrLf & "	," & (Val(Me.DefaultGenBarcode)).ToString() & " AS FNQtyBarcode"
        '_Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNGenBarcodeQty"
        _Str &= vbCrLf & "	, CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END AS FNGenBarcodeQty"
        ' _Str &= vbCrLf & "	,0.0000  AS FNGenBarcodeQty"
        _Str &= vbCrLf & "	,'' AS FTBatchNo"
        _Str &= vbCrLf & "	,'' AS FTGrade"
        _Str &= vbCrLf & "	,'' AS FTRollNo,FTOrderNoRef,FNSeqRef"
        _Str &= vbCrLf & "  FROM   ("
        _Str &= vbCrLf & "  Select IM.FTRawMatCode"
        _Str &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & "  ,ISNULL( S.FTRawMatSizeCode ,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & "  ,A.FTFabricFrontSize"
        _Str &= vbCrLf & "  ,A.FTPurchaseNo"
        _Str &= vbCrLf & "  , A.FTOrderNo"
        _Str &= vbCrLf & "  , A.FNHSysRawMatId"
        _Str &= vbCrLf & " 	, A.FNHSysWHId"
        _Str &= vbCrLf & " 	, A.FNQuantityStock"
        _Str &= vbCrLf & " 	, A.FNHSysUnitIdStock AS FNHSysUnitId"
        _Str &= vbCrLf & "  , A.FNPricePerStock"
        _Str &= vbCrLf & "  , U.FTUnitCode"
        _Str &= vbCrLf & " 	 ,ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & " 		  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FTDocumentNo = A.FTDocumentNo "
        _Str &= vbCrLf & " 		AND  FNHSysRawMatId = A.FNHSysRawMatId"
        _Str &= vbCrLf & " 		AND  FTOrderNo = A.FTOrderNo"
        _Str &= vbCrLf & " 		AND  FTPurchaseNo = A.FTPurchaseNo"
        _Str &= vbCrLf & " 		AND  ISNULL(FTOrderNoRef,'') = A.FTOrderNoRef"
        _Str &= vbCrLf & " 		AND  ISNULL(FNSeqRef,0) = A.FNSeqRef"
        _Str &= vbCrLf & " 		AND  ISNULL(FTFabricFrontSizeRcv,FTFabricFrontSize) = A.FTFabricFrontSize"
        _Str &= vbCrLf & "  	 ),0) AS FNBarCodeQty"

        _Str &= vbCrLf & " , C.FNRawMatColorSeq, S.FNRawMatSizeSeq,A.FTOrderNoRef,A.FNSeqRef "
        _Str &= vbCrLf & "  FROM           ( "

        Select Case BarType
            Case BarCodeType.Receive

                _Str &= vbCrLf & "  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  , A.FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantityStock,'' AS FTOrderNoRef,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo  "
                _Str &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & " LEFT OUTER JOIN ("
                _Str &= vbCrLf & " SELECT FTOrderNo,FNHSysRawMatId "
                _Str &= vbCrLf & "  FROM @TabDummy "
                _Str &= vbCrLf & " GROUP BY FTOrderNo,FNHSysRawMatId"
                _Str &= vbCrLf & "  ) XSA ON A.FNHSysRawMatId = XSA.FNHSysRawMatId AND A.FTOrderNo = XSA.FTOrderNo  "
                _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "' "
                _Str &= vbCrLf & "  AND XSA.FTOrderNo IS NULL "

                'If (_FoundOrderType) Then
                '    _Str &= vbCrLf & " AND (ISNULL(O.FNOrderType,0) <> 4)"
                'End If

                _Str &= vbCrLf & "  UNION "
                _Str &= vbCrLf & "   SELECT  A.FTDocumentNo,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & ",  A.FTFabricFrontSize"
                _Str &= vbCrLf & "	,A.FNHSysRawMatId"
                _Str &= vbCrLf & "	,A.FNHSysWHId"
                _Str &= vbCrLf & "  ,A.FNHSysUnitIdStock"
                _Str &= vbCrLf & "  ,A.FNPricePerStock"

                '_Str &= vbCrLf & "  ,(A.FNQuantitySpare + (A.FNQuantity - ISNULL(B.FNQuantityTotalSpare,0))) AS FNQuantitySpare"
                '_Str &= vbCrLf & "  ,(A.FNQuantitySpare +  CASE WHEN  ISNULL(B.FNQuantityTotalSpare,0) =0 THEN 0 ELSE  ((A.FNQuantity - ISNULL(B.FNQuantityTotalSpare,0))) END) AS FNQuantitySpare"

                _Str &= vbCrLf & "  ,A.FNQuantitySpare AS FNQuantitySpare"
                _Str &= vbCrLf & "  ,A.FTOrderNoRef ,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM @TabDummy AS A  LEFT OUTER JOIN ("
                _Str &= vbCrLf & "	SELECT FTDocumentNo,FNHSysRawMatId,FTOrderNo"
                _Str &= vbCrLf & "  ,MAX(FTOrderNoRef) AS FTOrderNoRef"
                _Str &= vbCrLf & "  , SUM(FNQuantitySpare) AS FNQuantityTotalSpare"
                _Str &= vbCrLf & "  FROM @TabDummy "
                _Str &= vbCrLf & "	GROUP BY FTDocumentNo,FNHSysRawMatId,FTOrderNo"
                _Str &= vbCrLf & "  ) AS B ON A.FTDocumentNo = B.FTDocumentNo"
                _Str &= vbCrLf & "   AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                _Str &= vbCrLf & "	AND A.FTOrderNo = B.FTOrderNo"
                _Str &= vbCrLf & "	AND A.FTOrderNoRef = B.FTOrderNoRef"

            Case BarCodeType.Adjust

                _Str &= vbCrLf & "  SELECT H.FTAdjustStockNo AS FTDocumentNo ,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  , A.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPrice AS FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantity AS FNQuantityStock,'' AS FTOrderNoRef ,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock  AS  H WITH(NOLOCK) ON A.FTAdjustStockNo = H.FTAdjustStockNo  "
                _Str &= vbCrLf & "  WHERE A.FTAdjustStockNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"

            Case BarCodeType.Scrap

                _Str &= vbCrLf & "   SELECT        FTScrapNo AS FTDocumentNo , FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & ",Convert(numeric(18,4),Amount / FNQuantity) as FNPricePerStock"
                _Str &= vbCrLf & "	,  FNQuantity AS FNQuantityStock,'' AS FTOrderNoRef,0 AS FNSeqRef"
                _Str &= vbCrLf & " FROM            (SELECT        FTScrapNo, FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId, SUM(FNQuantity) AS FNQuantity, "
                _Str &= vbCrLf & "  SUM(Amount) AS Amount"
                _Str &= vbCrLf & "  FROM            (SELECT        H.FTScrapNo, B.FTPurchaseNo, BO.FTOrderNo, B.FTFabricFrontSize, B.FNHSysRawMatId, BO.FNHSysWHId, B.FNHSysUnitId, ISNULL(BO.FNPriceTrans,B.FNPrice) AS FNPrice, "
                _Str &= vbCrLf & "    BO.FNQuantity, CONVERT(NUMERIC(18, 2),( CASE WHEN ISNULL(BO.FNPriceTrans,0) > 0 THEN ISNULL(BO.FNPriceTrans,0) ELSE B.FNPrice END ) * BO.FNQuantity) AS Amount"
                _Str &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENScrapBarcode AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTScrapNo = BO.FTDocumentNo INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
                _Str &= vbCrLf & "  WHERE FTScrapNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "' ) AS A"
                _Str &= vbCrLf & "  GROUP BY FTScrapNo, FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId) AS B"

            Case BarCodeType.Conversion

                _Str &= vbCrLf & "  SELECT H.FTConversionNo AS FTDocumentNo ,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  ,IM.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPrice AS FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantity AS  FNQuantityStock,'' AS FTOrderNoRef,A.FNSeq AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail_Finish AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion  AS  H WITH(NOLOCK) ON A.FTConversionNo = H.FTConversionNo  "
                _Str &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId "
                _Str &= vbCrLf & "  WHERE A.FTConversionNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"

        End Select

        _Str &= vbCrLf & "  ) AS A"
        _Str &= vbCrLf & "   INNER Join"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitIdStock = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & "  ) AS M"
        ' _Str &= vbCrLf & " WHERE (FNQuantityStock-FNBarCodeQty) >0"
        _Str &= vbCrLf & " WHERE ( CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END) >0 "
        '_Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTOrderNo"
        '_Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FNRawMatColorSeq,FNRawMatSizeSeq,FTFabricFrontSize,FTOrderNo"
        _Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FNRawMatSizeSeq,FTFabricFrontSize,FTOrderNo"

        Select Case BarType
            Case BarCodeType.Receive

                '_Str &= vbCrLf & " DROP TABLE #TabDummy "
                '_Str &= vbCrLf & " DROP TABLE #TabDummySpare "
                '_Str &= vbCrLf & " DROP TABLE #TabDummySpareTmp "

            Case Else
        End Select

        Return HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Private Function LoadDataTable_BakORG() As DataTable

        Dim _Str As String = ""
        Dim _FoundOrderType As Boolean = False
        Select Case BarType
            Case BarCodeType.Receive

                _Str = " Select TOP 1  B.FTOrderNo"
                _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS B WITH(NOLOCK)  ON A.FTPurchaseNo = B.FTPurchaseNo INNER JOIN"
                _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
                _Str &= vbCrLf & " WHERE  (C.FNOrderType <> 4) "
                _Str &= vbCrLf & " AND (A.FTReceiveNo =N'" & HI.UL.ULF.rpQuoted(DocumentNo) & "')"
                _FoundOrderType = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")

                _Str = "     DECLARE @PONo nvarchar(30)"
                _Str &= vbCrLf & "  SET @PONO =''"
                _Str &= vbCrLf & " SELECT TOP 1 @PONO=FTPurchaseNo"
                _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK)"
                _Str &= vbCrLf & "  WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"
                _Str &= vbCrLf & "  CREATE TABLE #TabDummy("
                _Str &= vbCrLf & "  FTDocumentNo nvarchar(30),"
                _Str &= vbCrLf & " FTPurchaseNo nvarchar(30),"
                _Str &= vbCrLf & " FNHSysRawMatId int,"
                _Str &= vbCrLf & "  FTOrderNo nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantity numeric(18,4),"
                _Str &= vbCrLf & "  FTOrderNoRef nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantityPer numeric(18,6),"
                _Str &= vbCrLf & "  FNQuantitySpare numeric(18,4),"
                _Str &= vbCrLf & "  FTFabricFrontSize nvarchar(50),"
                _Str &= vbCrLf & "  FNHSysWHId int,"
                _Str &= vbCrLf & "  FNHSysUnitIdStock int,"
                _Str &= vbCrLf & "  FNPricePerStock numeric(18, 5),"
                _Str &= vbCrLf & "  FNQuantityStock numeric(18, 5)"
                _Str &= vbCrLf & "  )"


                _Str &= vbCrLf & "    CREATE TABLE #TabDummySpare("
                _Str &= vbCrLf & "  FTPurchaseNo nvarchar(30),"
                _Str &= vbCrLf & " FNHSysRawMatId int,"
                _Str &= vbCrLf & "  FTOrderNo nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantity numeric(18,4),"
                _Str &= vbCrLf & "  FTOrderNoRef nvarchar(30),"
                _Str &= vbCrLf & "  FNQuantityPer numeric(18,6),"
                _Str &= vbCrLf & "  FNQuantitySpare numeric(18,4),"
                _Str &= vbCrLf & "  FNQuantitySpareTotalOrderRef numeric(18,4),"
                _Str &= vbCrLf & "   FNTotalBFQuantityPer numeric(18,4),"
                _Str &= vbCrLf & "    FNTotalBFQuantityPerAdd numeric(18, 4)"
                _Str &= vbCrLf & "  )"


                _Str &= vbCrLf & "  INSERT INTO #TabDummySpare(FTPurchaseNo"
                _Str &= vbCrLf & "  ,FNHSysRawMatId"
                _Str &= vbCrLf & "  ,FTOrderNo"
                _Str &= vbCrLf & "  ,FNQuantity"
                _Str &= vbCrLf & "  ,FTOrderNoRef"
                _Str &= vbCrLf & "  ,FNQuantityPer"
                _Str &= vbCrLf & "  ,FNQuantitySpare"
                _Str &= vbCrLf & "  ,FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & "  ,FNTotalBFQuantityPer"
                _Str &= vbCrLf & "  ,FNTotalBFQuantityPerAdd)"
                _Str &= vbCrLf & "  Select A.FTPurchaseNo"
                _Str &= vbCrLf & " ,A.FNHSysRawMatId "
                _Str &= vbCrLf & "  ,A.FTOrderNo "
                _Str &= vbCrLf & "   ,A.FNQuantityStock   "
                _Str &= vbCrLf & "   ,B.FTOrderNoRef "
                _Str &= vbCrLf & "   ,0"
                _Str &= vbCrLf & "    ,Convert(numeric(18,4),(A.FNQuantityStock  * B.FNQuantityOrder )/ B.FNOrderNoRefQuantity ) AS FNQuantitySpare"

                _Str &= vbCrLf & "   ,ISNULL((SELECT SUM(FNQuantity ) AS AA"
                _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK)"
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = A.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = A.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = A.FNHSysRawMatId) "
                _Str &= vbCrLf & "  AND AA.FTOrderNoRef =B.FTOrderNoRef "
                _Str &= vbCrLf & " ),0)"
                _Str &= vbCrLf & " 	- ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & " 	FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                _Str &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = A.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = A.FTOrderNo) "
                _Str &= vbCrLf & "  AND  (AA.FNHSysRawMatId = A.FNHSysRawMatId) "
                _Str &= vbCrLf & "  AND AA.FTOrderNoRef =B.FTOrderNoRef "
                _Str &= vbCrLf & "  ),0 )"
                _Str &= vbCrLf & "  AS FNQuantitySpareTotalOrderRef"
                _Str &= vbCrLf & " ,0,0"
                _Str &= vbCrLf & "  FROM (SELECT B.FTOrderNo "
                _Str &= vbCrLf & " ,P.FTPurchaseNo"
                _Str &= vbCrLf & " ,B.FNHSysRawMatId"
                _Str &= vbCrLf & " ,SUM(B.FNQuantityStock )	"
                _Str &= vbCrLf & " - ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                _Str &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                _Str &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                _Str &= vbCrLf & "    WHERE(AA.FTPurchaseNo = P.FTPurchaseNo)"
                _Str &= vbCrLf & "  AND  (AA.FTOrderNo = B.FTOrderNo) "
                _Str &= vbCrLf & "   AND  (AA.FNHSysRawMatId = B.FNHSysRawMatId) "
                _Str &= vbCrLf & "  	),0 )		"
                _Str &= vbCrLf & "  AS FNQuantityStock"
                _Str &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK) ON A.FTReceiveNo = B.FTReceiveNo INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH(NOLOCK) ON A.FTPurchaseNo = P.FTPurchaseNo "
                _Str &= vbCrLf & "   AND B.FTOrderNo = P.FTOrderNo "
                _Str &= vbCrLf & "   AND B.FNHSysRawMatId = P.FNHSysRawMatId"
                _Str &= vbCrLf & "   INNER Join"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE P.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) "
                _Str &= vbCrLf & "  GROUP BY B.FTOrderNo "
                _Str &= vbCrLf & " ,P.FTPurchaseNo"
                _Str &= vbCrLf & " ,B.FNHSysRawMatId"
                _Str &= vbCrLf & "  ) AS A"

                _Str &= vbCrLf & "  INNER JOIN (SELECT A.*,B.FTOrderNo AS FTOrderNoRef"
                _Str &= vbCrLf & "  , B.FNQuantity  AS FNQuantityOrder"
                _Str &= vbCrLf & "  ,C.FNQuantity AS FNOrderNoRefQuantity"
                _Str &= vbCrLf & "     FROM"
                _Str &= vbCrLf & "  (SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                _Str &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE A.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) ) AS A  INNER JOIN ("
                _Str &= vbCrLf & "   SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                _Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                _Str &= vbCrLf & "  ) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                _Str &= vbCrLf & "  INNER JOIN ("
                _Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId ,SUM(A.FNQuantity ) AS FNQuantity"
                _Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                _Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                _Str &= vbCrLf & "  GROUP BY A.FTPurchaseNo,A.FNHSysRawMatId "
                _Str &= vbCrLf & "  ) AS C ON B.FTPurchaseNo = C.FTPurchaseNo "
                _Str &= vbCrLf & "  AND B.FNHSysRawMatId = C.FNHSysRawMatId) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FTOrderNo = B.FTOrderNo  AND A.FNHSysRawMatId =B.FNHSysRawMatId "

                '_Str &= vbCrLf & "  Select XA.FTPurchaseNo"
                '_Str &= vbCrLf & " ,XA.FNHSysRawMatId"
                '_Str &= vbCrLf & " ,XA.FTOrderNo"
                '_Str &= vbCrLf & "  ,XA.FNQuantity"
                '_Str &= vbCrLf & "  ,XA.FTOrderNoRef"
                '' _Str &= vbCrLf & "  ,(XA.FNQuantityPer - ((FNQuantitySpareTotalOrderRef /FNQuantitySpare)*100)) AS FNQuantityPer"
                '_Str &= vbCrLf & "  ,(XA.FNQuantityPer - (CASE WHEN FNQuantitySpareTotalOrderRef > 0 THEN    ((XA.FNQuantityPer * FNQuantitySpareTotalOrderRef /FNQuantitySpare)) ELSE 0 END)) AS FNQuantityPer "
                '_Str &= vbCrLf & "  ,XA.FNQuantitySpare"
                '_Str &= vbCrLf & "  ,XA.FNQuantitySpareTotalOrderRef"
                ''_Str &= vbCrLf & "  ,((FNQuantitySpareTotalOrderRef /FNQuantitySpare)*100)  AS FNTotalBFQuantityPer"
                '_Str &= vbCrLf & "  ,(CASE WHEN FNQuantitySpareTotalOrderRef > 0 THEN    ((XA.FNQuantityPer * FNQuantitySpareTotalOrderRef /FNQuantitySpare)) ELSE 0 END)  AS FNTotalBFQuantityPer"
                '_Str &= vbCrLf & "  ,0.00"
                '_Str &= vbCrLf & " FROM (SELECT  X.FTPurchaseNo"
                '_Str &= vbCrLf & "  ,X.FNHSysRawMatId"
                '_Str &= vbCrLf & " ,X.FTOrderNo"
                '_Str &= vbCrLf & "  ,X.FNQuantity"
                '_Str &= vbCrLf & " ,X.FTOrderNoRef"
                '_Str &= vbCrLf & "  ,X.FNQuantityPer"
                '_Str &= vbCrLf & "  ,Convert(numeric(18,4),((X.FNQuantity * X.FNQuantityPer) /100.00 )) AS FNQuantitySpare"

                '_Str &= vbCrLf & "  ,ISNULL((SELECT SUM(FNQuantity ) AS AA"
                '_Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK)"
                '_Str &= vbCrLf & "     WHERE(AA.FTPurchaseNo = X.FTPurchaseNo)"
                '_Str &= vbCrLf & "  AND  (AA.FTOrderNo = X.FTOrderNo) "
                '_Str &= vbCrLf & "   AND  (AA.FNHSysRawMatId = X.FNHSysRawMatId) "
                '_Str &= vbCrLf & "   AND AA.FTOrderNoRef =X.FTOrderNoRef "
                '_Str &= vbCrLf & "   AND (AA.FTDocumentNo <> '" & HI.UL.ULF.rpQuoted(DocumentNo) & "')"

                '_Str &= vbCrLf & " 	),0)"

                '_Str &= vbCrLf & " 	- ISNULL((SELECT SUM(B2.FNQuantity) AS FNQuantity"
                '_Str &= vbCrLf & " 	FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A2 WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & " 	         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B2 WITH(NOLOCK) ON A2.FTReturnSuplNo = B2.FTDocumentNo"
                '_Str &= vbCrLf & " 	 INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS AA WITH(NOLOCK) ON B2.FTBarcodeNo = AA.FTBarcodeNo "
                '_Str &= vbCrLf & "   WHERE(AA.FTPurchaseNo = x.FTPurchaseNo)"
                '_Str &= vbCrLf & "  AND  (AA.FTOrderNo = X.FTOrderNo) "
                '_Str &= vbCrLf & "   AND  (AA.FNHSysRawMatId = X.FNHSysRawMatId) "
                '_Str &= vbCrLf & "   AND AA.FTOrderNoRef =X.FTOrderNoRef "
                '_Str &= vbCrLf & "   AND (AA.FTDocumentNo <> '" & HI.UL.ULF.rpQuoted(DocumentNo) & "')"
                '_Str &= vbCrLf & " 	),0 )"


                '_Str &= vbCrLf & "  AS FNQuantitySpareTotalOrderRef"

                '_Str &= vbCrLf & "  FROM (SELECT A.*,B.FTOrderNo AS FTOrderNoRef"
                '_Str &= vbCrLf & "  ,Convert(numeric(18,6),(((  B.FNQuantity *100.00) / C.FNQuantity))) AS FNQuantityPer"

                '_Str &= vbCrLf & "      FROM"
                '_Str &= vbCrLf & "  (SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE A.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) ) AS A  INNER JOIN ("
                '_Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & " ) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                '_Str &= vbCrLf & " INNER JOIN ("
                '_Str &= vbCrLf & " SELECT A.FTPurchaseNo,A.FNHSysRawMatId ,SUM(A.FNQuantity ) AS FNQuantity"
                '_Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "  AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & " GROUP BY A.FTPurchaseNo,A.FNHSysRawMatId "
                '_Str &= vbCrLf & " ) AS C ON B.FTPurchaseNo = C.FTPurchaseNo AND B.FNHSysRawMatId = C.FNHSysRawMatId"
                '_Str &= vbCrLf & " ) AS X "
                '_Str &= vbCrLf & " ) XA"

                '_Str &= vbCrLf & " DECLARE @CountOrderSpareAdd numeric(18,4)"
                '_Str &= vbCrLf & " DECLARE @CountOrder int"
                '_Str &= vbCrLf & " SET @CountOrder =0"
                '_Str &= vbCrLf & " SET @CountOrderSpareAdd =0"

                '_Str &= vbCrLf & " SELECT @CountOrder =COUNT(FTOrderNoRef)"
                '_Str &= vbCrLf & " FROM #TabDummySpare"
                '_Str &= vbCrLf & "  WHERE FNTotalBFQuantityPer = 0"

                '_Str &= vbCrLf & " IF @CountOrder > 0"
                '_Str &= vbCrLf & "BEGIN"
                '_Str &= vbCrLf & " SELECT @CountOrderSpareAdd = Convert(numeric(18,4),ISNULL((SELECT SUM(FNTotalBFQuantityPer)"
                '_Str &= vbCrLf & " FROM #TabDummySpare"
                '_Str &= vbCrLf & "    WHERE FNTotalBFQuantityPer <> 0"
                '_Str &= vbCrLf & "    ),0)  /@CountOrder )"
                '_Str &= vbCrLf & " End"

                '_Str &= vbCrLf & " UPDATE A SET FNTotalBFQuantityPerAdd=@CountOrderSpareAdd"
                '_Str &= vbCrLf & " FROM #TabDummySpare AS A "
                '_Str &= vbCrLf & "  WHERE FNTotalBFQuantityPer = 0"

                _Str &= vbCrLf & "  INSERT INTO #TabDummy(FTDocumentNo,FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity"
                _Str &= vbCrLf & "   ,FTOrderNoRef,FNQuantityPer,FNQuantitySpare,FTFabricFrontSize,FNHSysWHId,FNHSysUnitIdStock,FNPricePerStock,FNQuantityStock)"
                _Str &= vbCrLf & "  SELECT M.FTDocumentNo,M.FTPurchaseNo,M.FNHSysRawMatId,M.FTOrderNo,M.FNQuantityStock,MM.FTOrderNoRef ,MM.FNQuantityPer "
                '_Str &= vbCrLf & "   ,Convert(numeric(18,4),((M.FNQuantityStock * MM.FNQuantityPer) /100.00 )) AS FNQuantitySpare"
                _Str &= vbCrLf & "   ,(FNQuantitySpare-MM.FNQuantitySpareTotalOrderRef )  AS FNQuantitySpare"
                _Str &= vbCrLf & "   ,M.FTFabricFrontSize,M.FNHSysWHId, M.FNHSysUnitIdStock"
                _Str &= vbCrLf & " , M.FNPricePerStock"
                _Str &= vbCrLf & " 	,M.FNQuantityStock"
                _Str &= vbCrLf & "  FROM (  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "    ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & "  ,H.FNHSysWHId"
                _Str &= vbCrLf & "   , A.FNHSysUnitIdStock"
                _Str &= vbCrLf & " , A.FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantityStock"
                _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo   INNER JOIN"
                _Str &= vbCrLf & " 	       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
                _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'  AND (O.FNOrderType = 4)"
                _Str &= vbCrLf & "   ) AS M"
                '_Str &= vbCrLf & "  INNER JOIN (SELECT  X.FTPurchaseNo"
                '_Str &= vbCrLf & "  ,X.FNHSysRawMatId"
                '_Str &= vbCrLf & "  ,X.FTOrderNo"
                '_Str &= vbCrLf & "  ,X.FNQuantity"
                '_Str &= vbCrLf & "  ,X.FTOrderNoRef"
                '_Str &= vbCrLf & "  ,X.FNQuantityPer"
                '_Str &= vbCrLf & "  ,Convert(numeric(18,4),((X.FNQuantity * X.FNQuantityPer) /100.00 )) AS FNQuantitySpare"
                '_Str &= vbCrLf & "  FROM (SELECT A.*,B.FTOrderNo AS FTOrderNoRef"
                '_Str &= vbCrLf & "   ,Convert(numeric(18,6),(((  B.FNQuantity *100.00) / C.FNQuantity))) AS FNQuantityPer"
                '_Str &= vbCrLf & "   FROM"
                '_Str &= vbCrLf & "  (SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE A.FTPurchaseNo=@PONO AND (O.FNOrderType = 4) ) AS A  INNER JOIN ("
                '_Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId , O.FNOrderType, A.FTOrderNo,A.FNQuantity "
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "   AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & "  ) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                '_Str &= vbCrLf & "  INNER JOIN ("
                '_Str &= vbCrLf & "  SELECT A.FTPurchaseNo,A.FNHSysRawMatId ,SUM(A.FNQuantity ) AS FNQuantity"
                '_Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
                '_Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                '_Str &= vbCrLf & "  WHERE  A.FTPurchaseNo=@PONO  "
                '_Str &= vbCrLf & "   AND (O.FNOrderType <> 4)"
                '_Str &= vbCrLf & "  GROUP BY A.FTPurchaseNo,A.FNHSysRawMatId "
                '_Str &= vbCrLf & " ) AS C ON B.FTPurchaseNo = C.FTPurchaseNo AND B.FNHSysRawMatId = C.FNHSysRawMatId) AS X"

                _Str &= vbCrLf & "  INNER JOIN (SELECT  X.FTPurchaseNo"
                _Str &= vbCrLf & "  ,X.FNHSysRawMatId"
                _Str &= vbCrLf & "  ,X.FTOrderNo"
                _Str &= vbCrLf & "  ,X.FNQuantity"
                _Str &= vbCrLf & "  ,X.FTOrderNoRef"
                _Str &= vbCrLf & "  ,(X.FNQuantityPer + X.FNTotalBFQuantityPerAdd) AS  FNQuantityPer "
                _Str &= vbCrLf & "  ,X.FNQuantitySpare,X.FNQuantitySpareTotalOrderRef "
                _Str &= vbCrLf & "  FROM #TabDummySpare  AS X"

                _Str &= vbCrLf & "  ) AS MM ON M.FTPurchaseNo = MM.FTPurchaseNo ANd M.FTOrderNo = MM.FTOrderNo AND M.FNHSysRawMatId = MM.FNHSysRawMatId "

            Case Else
                _Str = ""
        End Select

        _Str &= vbCrLf & "  SELECT FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTPurchaseNo,FTOrderNo,FNHSysRawMatId,FNHSysUnitId"
        _Str &= vbCrLf & " 	,FNHSysWHId,FNQuantityStock,FNPricePerStock,FTUnitCode,FNBarCodeQty"
        '_Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNBarcodeBalance"

        _Str &= vbCrLf & "	, CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END AS FNBarcodeBalance"
        _Str &= vbCrLf & "	,1 AS FNQtyBarcode"
        '_Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNGenBarcodeQty"
        _Str &= vbCrLf & "	, CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END AS FNGenBarcodeQty"
        ' _Str &= vbCrLf & "	,0.0000  AS FNGenBarcodeQty"
        _Str &= vbCrLf & "	,'' AS FTBatchNo"
        _Str &= vbCrLf & "	,'' AS FTGrade"
        _Str &= vbCrLf & "	,'' AS FTRollNo,FTOrderNoRef,FNSeqRef"
        _Str &= vbCrLf & "  FROM   ("
        _Str &= vbCrLf & "  Select IM.FTRawMatCode"
        _Str &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & "  ,ISNULL( S.FTRawMatSizeCode ,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & "  ,A.FTFabricFrontSize"
        _Str &= vbCrLf & "  ,A.FTPurchaseNo"
        _Str &= vbCrLf & "  , A.FTOrderNo"
        _Str &= vbCrLf & "  , A.FNHSysRawMatId"
        _Str &= vbCrLf & " 	, A.FNHSysWHId"
        _Str &= vbCrLf & " 	, A.FNQuantityStock"
        _Str &= vbCrLf & " 	, A.FNHSysUnitIdStock AS FNHSysUnitId"
        _Str &= vbCrLf & "  , A.FNPricePerStock"
        _Str &= vbCrLf & "  , U.FTUnitCode"
        _Str &= vbCrLf & " 	 ,ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & " 		  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FTDocumentNo = A.FTDocumentNo "
        _Str &= vbCrLf & " 		AND  FNHSysRawMatId = A.FNHSysRawMatId"
        _Str &= vbCrLf & " 		AND  FTOrderNo = A.FTOrderNo"
        _Str &= vbCrLf & " 		AND  FTPurchaseNo = A.FTPurchaseNo"
        _Str &= vbCrLf & " 		AND  ISNULL(FTOrderNoRef,'') = A.FTOrderNoRef"
        _Str &= vbCrLf & " 		AND  ISNULL(FNSeqRef,0) = A.FNSeqRef"
        _Str &= vbCrLf & "  	 ),0) AS FNBarCodeQty"

        _Str &= vbCrLf & " , C.FNRawMatColorSeq, S.FNRawMatSizeSeq,A.FTOrderNoRef,A.FNSeqRef "
        _Str &= vbCrLf & "  FROM           ( "

        Select Case BarType
            Case BarCodeType.Receive

                _Str &= vbCrLf & "  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  , A.FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantityStock,'' AS FTOrderNoRef,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo  "
                _Str &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"

                _Str &= vbCrLf & " LEFT OUTER JOIN ("
                _Str &= vbCrLf & " SELECT FTOrderNo,FNHSysRawMatId "
                _Str &= vbCrLf & "  FROM #TabDummy "
                _Str &= vbCrLf & " GROUP BY FTOrderNo,FNHSysRawMatId"
                _Str &= vbCrLf & "  ) XSA ON A.FNHSysRawMatId = XSA.FNHSysRawMatId AND A.FTOrderNo = XSA.FTOrderNo  "

                _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "' "
                _Str &= vbCrLf & "  AND XSA.FTOrderNo IS NULL "

                'If (_FoundOrderType) Then
                '    _Str &= vbCrLf & " AND (ISNULL(O.FNOrderType,0) <> 4)"
                'End If

                _Str &= vbCrLf & "  UNION "
                _Str &= vbCrLf & "   SELECT  A.FTDocumentNo,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & ",  A.FTFabricFrontSize"
                _Str &= vbCrLf & "	,A.FNHSysRawMatId"
                _Str &= vbCrLf & "	,A.FNHSysWHId"
                _Str &= vbCrLf & "  ,A.FNHSysUnitIdStock"
                _Str &= vbCrLf & "  ,A.FNPricePerStock"
                '  _Str &= vbCrLf & "  ,(A.FNQuantitySpare + (A.FNQuantity - ISNULL(B.FNQuantityTotalSpare,0))) AS FNQuantitySpare"
                '_Str &= vbCrLf & "  ,(A.FNQuantitySpare +  CASE WHEN  ISNULL(B.FNQuantityTotalSpare,0) =0 THEN 0 ELSE  ((A.FNQuantity - ISNULL(B.FNQuantityTotalSpare,0))) END) AS FNQuantitySpare"
                _Str &= vbCrLf & "  ,A.FNQuantitySpare AS FNQuantitySpare"
                _Str &= vbCrLf & "  ,A.FTOrderNoRef ,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM #TabDummy AS A  LEFT OUTER JOIN ("
                _Str &= vbCrLf & "	SELECT FTDocumentNo,FNHSysRawMatId,FTOrderNo"
                _Str &= vbCrLf & "  ,MAX(FTOrderNoRef) AS FTOrderNoRef"
                _Str &= vbCrLf & "  , SUM(FNQuantitySpare) AS FNQuantityTotalSpare"
                _Str &= vbCrLf & "  FROM #TabDummy "
                _Str &= vbCrLf & "	GROUP BY FTDocumentNo,FNHSysRawMatId,FTOrderNo"
                _Str &= vbCrLf & "  ) AS B ON A.FTDocumentNo = B.FTDocumentNo"
                _Str &= vbCrLf & "   AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                _Str &= vbCrLf & "	AND A.FTOrderNo = B.FTOrderNo"
                _Str &= vbCrLf & "	AND A.FTOrderNoRef = B.FTOrderNoRef"

            Case BarCodeType.Adjust

                _Str &= vbCrLf & "  SELECT H.FTAdjustStockNo AS FTDocumentNo ,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  , A.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPrice AS FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantity AS FNQuantityStock,'' AS FTOrderNoRef ,0 AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock  AS  H WITH(NOLOCK) ON A.FTAdjustStockNo = H.FTAdjustStockNo  "
                _Str &= vbCrLf & "  WHERE A.FTAdjustStockNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"

            Case BarCodeType.Scrap

                _Str &= vbCrLf & "   SELECT        FTScrapNo AS FTDocumentNo , FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & ",Convert(numeric(18," & Val(HI.ST.Config.PriceDigit) & "),Amount / FNQuantity) as FNPricePerStock"
                _Str &= vbCrLf & "	,  FNQuantity AS FNQuantityStock,'' AS FTOrderNoRef,0 AS FNSeqRef"
                _Str &= vbCrLf & " FROM            (SELECT        FTScrapNo, FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId, SUM(FNQuantity) AS FNQuantity, "
                _Str &= vbCrLf & "  SUM(Amount) AS Amount"
                _Str &= vbCrLf & "  FROM            (SELECT        H.FTScrapNo, B.FTPurchaseNo, BO.FTOrderNo, B.FTFabricFrontSize, B.FNHSysRawMatId, BO.FNHSysWHId, B.FNHSysUnitId, ISNULL(BO.FNPriceTrans,B.FNPrice) AS FNPrice, "
                _Str &= vbCrLf & "    BO.FNQuantity, CONVERT(NUMERIC(18, 2), ISNULL(BO.FNPriceTrans,B.FNPrice) * BO.FNQuantity) AS Amount"
                _Str &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENScrapBarcode AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTScrapNo = BO.FTDocumentNo INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
                _Str &= vbCrLf & "  WHERE FTScrapNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "' ) AS A"
                _Str &= vbCrLf & "  GROUP BY FTScrapNo, FTPurchaseNo, FTOrderNo, FTFabricFrontSize, FNHSysRawMatId, FNHSysWHId, FNHSysUnitId) AS B"

            Case BarCodeType.Conversion

                _Str &= vbCrLf & "  SELECT H.FTConversionNo AS FTDocumentNo ,A.FTPurchaseNo,A.FTOrderNo"
                _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  ,IM.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPrice AS FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantity AS  FNQuantityStock,'' AS FTOrderNoRef,A.FNSeq AS FNSeqRef"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail_Finish AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion  AS  H WITH(NOLOCK) ON A.FTConversionNo = H.FTConversionNo  "
                _Str &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId "
                _Str &= vbCrLf & "  WHERE A.FTConversionNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"

        End Select

        _Str &= vbCrLf & "  ) AS A"
        _Str &= vbCrLf & "   INNER Join"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitIdStock = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & "  ) AS M"
        ' _Str &= vbCrLf & " WHERE (FNQuantityStock-FNBarCodeQty) >0"
        _Str &= vbCrLf & " WHERE ( CASE WHEN ISNULL(FTOrderNoRef,'')='' THEN FNQuantityStock - FNBarCodeQty ELSE FNQuantityStock END) >0 "
        '_Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTOrderNo"
        '_Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FNRawMatColorSeq,FNRawMatSizeSeq,FTFabricFrontSize,FTOrderNo"
        _Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FNRawMatSizeSeq,FTFabricFrontSize,FTOrderNo"

        Select Case BarType
            Case BarCodeType.Receive
                _Str &= vbCrLf & " DROP TABLE #TabDummy "
                _Str &= vbCrLf & " DROP TABLE #TabDummySpare "

            Case Else
        End Select

        Return HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Public Sub LoadGenbarcode()

        Dim _dt As DataTable = LoadDataTable()
        Me.ogcdetail.DataSource = _dt.Copy
        _dt.Dispose()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.ProcGen = False
        Me.Close()
    End Sub

    Private Sub ocmgenbarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmgenbarcode.Click
        CType(ogcdetail.DataSource, DataTable).AcceptChanges()

        If CType(ogcdetail.DataSource, DataTable).Select("FNQtyBarcode>0 AND FNGenBarcodeQty>0").Length > 0 Then

            With ogvdetail
                .FocusedRowHandle = 0
                .FocusedColumn = .Columns.ColumnByName("FTFabricFrontSize")
            End With

            Me.ProcGen = Me.GenBarcode

            If Me.ProcGen Then

                Dim _dt As DataTable = LoadDataTable()

                If _dt.Rows.Count <= 0 Then
                    Me.Close()
                Else
                    Me.ogcdetail.DataSource = _dt.Copy
                    Me.ogcdetail.RefreshDataSource()

                    Try
                        Call CallByName(Me.MainObject, "LoadBarcode", CallType.Method, {DocumentNo})
                    Catch ex As Exception
                    End Try

                End If

            End If

        Else
            HI.MG.ShowMsg.mInvalidData("กรูณาทำการระบุ จำนวนที่ต้องการสร้าง  !!!", 1311240004, Me.Text, "")
        End If

    End Sub

    Private Sub ReposFNQtyBarcode_EditValueChanged(sender As Object, e As System.EventArgs) Handles ReposFNQtyBarcode.EditValueChanged
        With ogvdetail

            Static _Proc As Boolean
            If Not (_Proc) Then
                _Proc = True
                Dim _Qty As Double = 0
                Dim _QtyBal As Double = 0
                With CType(sender, DevExpress.XtraEditors.CalcEdit)
                    _Qty = .Value
                End With

                Try
                    _QtyBal = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNBarcodeBalance").ToString)
                Catch ex As Exception
                    _QtyBal = 0
                End Try


                If _Qty = 0 Then
                    .SetRowCellValue(.FocusedRowHandle, "FNGenBarcodeQty", 0)
                Else
                    _QtyBal = CDbl(Format(_QtyBal / _Qty, HI.ST.Config.QtyFormat))
                    .SetRowCellValue(.FocusedRowHandle, "FNGenBarcodeQty", _QtyBal)
                End If


                _Proc = False
            End If


        End With
    End Sub

    Private Function GenBarcode() As Boolean
        Try
            For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Select("FNQtyBarcode>0 AND FNGenBarcodeQty>0")

                Dim MaxBar As Integer = Val(R!FNQtyBarcode.ToString)
                Dim QtyBal As Double = Val(R!FNBarcodeBalance.ToString)
                Dim QtyBar As Double = Val(R!FNGenBarcodeQty.ToString)
                Dim _Barcode As String = ""
                Dim _GenQtyBar As Double
                Dim _Str As String = ""

                For I As Integer = 1 To MaxBar
                    _GenQtyBar = QtyBar
                    _Barcode = HI.Conn.SQLConn.GetField(" EXEC SP_GEN_BARCODE_NO '" & HI.ST.SysInfo.CmpRunID & "' ", Conn.DB.DataBaseName.DB_INVEN, "")

                    If _Barcode <> "" And QtyBal > 0 Then
                        If (_GenQtyBar > QtyBal) Or (I = MaxBar And FTStaLastAll.Checked) Then
                            _GenQtyBar = QtyBal
                        End If

                        Try
                            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                            HI.Conn.SQLConn.SqlConnectionOpen()
                            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                            _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode( FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FNHSysWHId"
                            _Str &= vbCrLf & ", FNHSysRawMatId, FTOrderNo, FNHSysUnitId, "
                            _Str &= vbCrLf & "   FNPrice, FNQuantity, FTDocumentNo, FTFabricFrontSize, FTBatchNo, FTGrade, FTPurchaseNo,FNHSysCmpId,FTRollNo,FTOrderNoRef,FNSeqRef,FTFabricFrontSizeRcv,FTStateQCAccept,FTStateQCReject)"
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Barcode) & "' "
                            _Str &= vbCrLf & "," & Val(R!FNHSysWHId.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                            _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNPricePerStock.ToString) & " "
                            _Str &= vbCrLf & "," & _GenQtyBar & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBatchNo.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTGrade.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'" & HI.UL.ULF.rpQuoted(R!FTRollNo.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNoRef.ToString) & "'"
                            _Str &= vbCrLf & "," & Val(R!FNSeqRef.ToString) & ""
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                            _Str &= vbCrLf & ",'0','0' "

                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                Return True
                            End If

                            _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN( FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Barcode) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "
                            _Str &= vbCrLf & "," & Val(R!FNHSysWHId.ToString) & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                            _Str &= vbCrLf & "," & _GenQtyBar & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'," & Val(HI.ST.SysInfo.CmpID) & "  "

                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                Return True
                            End If

                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            ' Exit Sub
                        Catch ex As Exception

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return True
                        End Try

                        QtyBal = QtyBal - _GenQtyBar
                    End If
                Next

            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub wGenerateBarcodeInven_Load(sender As Object, e As EventArgs) Handles Me.Load
        FTStaLastAll.Checked = False
    End Sub
End Class
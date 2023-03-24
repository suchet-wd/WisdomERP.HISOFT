Public NotInheritable Class Barcode

    Public Enum DocType As Integer

        Reserve = 0
        Issue = 1
        ReturnToStock = 2
        ReturnToSupplier = 3
        SaleAndTerminate = 4
        ScrapBarcode = 5
        Adjust = 6
        TransferOrder = 7
        TransferCenter = 8
        TransferWH = 9
        ReturnToSupplierAfterIssue = 10
        Conversion = 11
        DCPrepareIssue = 12
        CountStock = 13
    End Enum

    Public Shared BarGrpRun As String = "BGR-"

    Public Shared Function CheckDucumentCreateBar(DocKey As String, Optional FNHSysRawMatId As Integer = 0, Optional SeqRefer As Integer = -1, Optional FOKey As String = "") As Boolean

        Dim Str As String = ""

        Str = "SELECT TOP 1 FTDocumentNo "
        Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode  AS A WITH (NOLOCK) "
        Str &= vbCrLf & " WHERE FTDocumentNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        If FNHSysRawMatId > 0 Then
            Str &= vbCrLf & " AND FNHSysRawMatId =" & FNHSysRawMatId & " "
        End If

        If SeqRefer > -1 Then
            Str &= vbCrLf & " AND FNSeqRef =" & SeqRefer & " "
        End If

        If FOKey <> "" Then
            Str &= vbCrLf & " AND FTOrderNo ='" & HI.UL.ULF.rpQuoted(FOKey) & "' "
        End If

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")

    End Function

    Public Shared Sub CheckUpdateUnitStock(FTDocNo As String, FNHSysRawMatId As Integer)
        Dim _Str As String = ""
        Dim _StateUsedUnit As Integer = 0

        _Str = "    SELECT      TOP 1  ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId "
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].TINVENReceive_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
        _Str &= vbCrLf & " WHERE ISNULL(B.FTRawMatCode,'') = ISNULL((SELECt TOP 1  ISNULL(Z.FTRawMatCode,'') "
        _Str &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS Z  WITH(NOLOCK) "
        _Str &= vbCrLf & "	 WHERE Z.FNHSysRawMatId =" & Integer.Parse(FNHSysRawMatId) & ""
        _Str &= vbCrLf & "	 ),'') AND A.FTReceiveNo<>'" & HI.UL.ULF.rpQuoted(FTDocNo) & "'"

        _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))

        If _StateUsedUnit = 0 Then

            _Str = "	SELECT      TOP 1  ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId "
            _Str &= vbCrLf & " 	FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
            _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].TINVENAdjustStock_AddIn_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
            _Str &= vbCrLf & " WHERE ISNULL(B.FTRawMatCode,'') = ISNULL((SELECt TOP 1  ISNULL(Z.FTRawMatCode,'') "
            _Str &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS Z  WITH(NOLOCK) "
            _Str &= vbCrLf & "	 WHERE Z.FNHSysRawMatId=" & Integer.Parse(FNHSysRawMatId) & ""
            _Str &= vbCrLf & "	 ),'') AND A.FTAdjustStockNo<>'" & HI.UL.ULF.rpQuoted(FTDocNo) & "'"

            _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))

        End If

        If _StateUsedUnit = 0 Then
            _Str = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_UPDATE_UNIT_RAWMAT " & FNHSysRawMatId & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
        End If

    End Sub

    Public Shared Function CheckTransactionOUT(BarKey As String, DocKey As String, WHKey As String, OrderKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
        Str &= vbCrLf & "  AND FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        If Val(WHKey) > 0 Then
            Str &= vbCrLf & "  AND FNHSysWHId =" & Val(WHKey) & " "
        End If

        If OrderKey <> "" Then
            Str &= vbCrLf & "  AND FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderKey) & "' "
        End If

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
    End Function

    Public Shared Function CheckTransactionIN(BarKey As String, DocKey As String, WHKey As String, OrderKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
        Str &= vbCrLf & "  AND FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        If Val(WHKey) > 0 Then
            Str &= vbCrLf & "  AND FNHSysWHId =" & Val(WHKey) & " "
        End If

        If OrderKey <> "" Then
            Str &= vbCrLf & "  AND FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderKey) & "' "
        End If

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
    End Function

    Public Shared Function CheckTransactionRTSAfterIssue(BarKey As String, DocKey As String, WHKey As String, OrderKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
        Str &= vbCrLf & "  AND FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        If Val(WHKey) > 0 Then
            Str &= vbCrLf & "  AND FNHSysWHId =" & Val(WHKey) & " "
        End If

        If OrderKey <> "" Then
            Str &= vbCrLf & "  AND FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderKey) & "' "
        End If

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
    End Function

    Public Shared Function CheckDocumentRefOut(DocKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
       
        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
    End Function

    Public Shared Function CheckDocumentRefIn(DocKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE  FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
    End Function

    Public Shared Function CheckDocumentRefRTSAfterIssue(DocKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE  FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
    End Function

    Public Shared Function BarCodeBalance(BarKey As String, WHKey As String, OrderKey As String, Optional DocOutKey As String = "", Optional StateMergeReserve As Boolean = False) As DataTable

        Dim Str As String

        If StateMergeReserve Then
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'1'  "
        Else
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'0'  "
        End If

        Return HI.Conn.SQLConn.GetDataTable(Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function


    Public Shared Function BarCodeBalanceCheckStock(BarKey As String, WHKey As String, OrderKey As String, Optional DocOutKey As String = "") As DataTable

        Dim Str As String


        Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_BALANCE_CHECKSTOCK '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'1'  "


        Return HI.Conn.SQLConn.GetDataTable(Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Public Shared Function BarCodeGrpBalance(BarKey As String, WHKey As String, OrderKey As String, Optional DocOutKey As String = "", Optional StateMergeReserve As Boolean = False) As DataTable

        Dim Str As String

        If StateMergeReserve Then
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_GROUP_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'1'  "
        Else
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_GROUP_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'0'  "
        End If

        Return HI.Conn.SQLConn.GetDataTable(Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function



    Public Shared Function BarCodePLBalance(BarKey As String, WHKey As String, OrderKey As String, Optional DocOutKey As String = "", Optional StateMergeReserve As Boolean = False) As DataTable

        Dim Str As String

        If StateMergeReserve Then
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_PACKINGLIST_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'1'  "
        Else
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_PACKINGLIST_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'0'  "
        End If

        Return HI.Conn.SQLConn.GetDataTable(Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Public Shared Function BarCodeOutForRet(BarKey As String, WHKey As String, OrderKey As String, Optional DocOutKey As String = "") As DataTable

        Dim Str As String
        Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GET_BARCODE_OUT_FOR_RET '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(OrderKey) & "' ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & "  "
        Return HI.Conn.SQLConn.GetDataTable(Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Public Shared Function SearchData(FTBarcodeNo As String, FNHSysWHId As Integer, FTOrderNo As String, FNHSysRawMatId As Integer, Optional MatCode As String = "", Optional MatColorId As Integer = 0, Optional MatSizeId As Integer = 0, Optional StateOnhand As Integer = 0) As DataTable

        Dim _Strsql As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_SEARCH_BARCODE '" & HI.UL.ULF.rpQuoted(FTBarcodeNo) & "'," & Val(FNHSysWHId) & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo) & "'," & Val(FNHSysRawMatId) & "," & Val(HI.ST.SysInfo.CmpID) & "," & Val(HI.ST.Lang.Language) & ",'" & HI.UL.ULF.rpQuoted(MatCode) & "'," & Val(MatColorId) & "," & Val(MatSizeId) & "," & Val(StateOnhand) & " "
        Return HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_INVEN)

    End Function


    Public Shared Function SearchDataForAcc(FTBarcodeNo As String, FNHSysWHId As Integer, FTOrderNo As String, FNHSysRawMatId As Integer, Optional MatCode As String = "", Optional MatColorId As Integer = 0, Optional MatSizeId As Integer = 0, Optional AsOfDate As String = "", Optional swh As String = "", Optional ewh As String = "") As DataTable

        Dim _Strsql As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_SEARCH_BARCODE_ACC '" & HI.UL.ULF.rpQuoted(FTBarcodeNo) & "'," & Val(FNHSysWHId) & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo) & "'," & Val(FNHSysRawMatId) & "," & Val(HI.ST.SysInfo.CmpID) & "," & Val(HI.ST.Lang.Language) & ",'" & HI.UL.ULF.rpQuoted(MatCode) & "'," & Val(MatColorId) & "," & Val(MatSizeId) & ",'" & AsOfDate & "','" & HI.UL.ULF.rpQuoted(swh) & "','" & HI.UL.ULF.rpQuoted(ewh) & "'"
        Return HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Public Shared Function GetBarcode(Key As String) As DataTable
        Dim _Str As String

        _Str = " Select  '0' AS FTSelect, Row_Number() Over(Order By A.FTBarcodeNo )   AS   FNSeq,  A.FTBarcodeNo"
        _Str &= vbCrLf & "	, IM.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & "	, IM.FTRawMatNameTH AS FTDescription "
        Else
            _Str &= vbCrLf & "	,  IM.FTRawMatNameEN AS FTDescription "
        End If

        _Str &= vbCrLf & "	, ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & "	, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & "	, A.FTFabricFrontSize"
        _Str &= vbCrLf & "	, U.FTUnitCode"
        _Str &= vbCrLf & "	, A.FTOrderNo"
        _Str &= vbCrLf & "	, A.FNQuantity"
        _Str &= vbCrLf & "	, A.FTPurchaseNo"
        _Str &= vbCrLf & ", A.FTBatchNo"
        _Str &= vbCrLf & ", A.FTBarcodeGrpNo"
        _Str &= vbCrLf & "	, A.FTGrade,A.FTRollNo"
        _Str &= vbCrLf & "	, A.FTShades,ISNULL(A.FTFabricFrontSizeRcv, A.FTFabricFrontSize) AS FTFabricFrontSizeRcv"
        _Str &= vbCrLf & ", ISNULL(A.FTStateQCAccept,'0') AS FTStateQCAccept"
        _Str &= vbCrLf & ", ISNULL(A.FTStateQCReject,'0') AS FTStateQCReject"
        _Str &= vbCrLf & ", ISNULL(WL.FTWHLocationCode,'') AS FTWHLocationCode"
        _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON A.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI  ON A.FTBarcodeNo = BI.FTBarcodeNo AND A.FTDocumentNo=BI.FTDocumentNo "
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocation AS WL ON BI.FNHSysWHLocationId = WL.FNHSysWHLocationId "
        _Str &= vbCrLf & " WHERE A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
        _Str &= vbCrLf & " ORDER BY  A.FTBarcodeNo"
        _Str &= vbCrLf & ", IM.FTRawMatCode"
        _Str &= vbCrLf & ", C.FTRawMatColorCode"
        _Str &= vbCrLf & ", S.FTRawMatSizeCode"

        Return HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

    End Function

    Public Shared Function LoadDocumentBarcode(DocKey As String, DocType As DocType, Optional BarcodeKey As String = "") As DataTable
        Dim _Str As String = ""
        Dim dt As New DataTable

        _Str = "  SELECT    ISNULL(W.FTWHCode,'') AS FTWHCode"
        _Str &= vbCrLf & ", BO.FTOrderNo"
        _Str &= vbCrLf & ", BO.FTBarcodeNo"
        _Str &= vbCrLf & ",B.FNHSysRawMatId,ISNULL(B.FTShades,'') AS FTShades"
        _Str &= vbCrLf & ", IM.FTRawMatCode"
        _Str &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & ", U.FTUnitCode"
        _Str &= vbCrLf & ", B.FTPurchaseNo"
        _Str &= vbCrLf & ", BO.FNQuantity"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameTH,'') AS  FTRawMatName"
        Else
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameEN,'') AS  FTRawMatName "
        End If

        _Str &= vbCrLf & ",ISNULL(B.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Str &= vbCrLf & ",BO.FNHSysWHId "
        _Str &= vbCrLf & ", PXTD.FTRawMatColorNameEN AS FTRawMatColorName"

        _Str &= vbCrLf & ", B.FTBatchNo"
        _Str &= vbCrLf & ", B.FTRollNo"

        _Str &= vbCrLf & ", ISNULL(BO.FTSateApp,'0') AS FTSateApp"
        _Str &= vbCrLf & ", BO.FTSateAppBy"
        _Str &= vbCrLf & ", BO.FTSateAppDate"
        _Str &= vbCrLf & ", BO.FTSateAppTime"

        _Str &= vbCrLf & ", ISNULL(WL.FTWHLocationCode,'') AS FTWHLocationCode"


        Select Case DocType
            Case DocType.DCPrepareIssue
                _Str &= vbCrLf & ", BO.FTIssueReferNo,BO.FTPLBarcodeNo "
            Case Else
                _Str &= vbCrLf & ",'' AS FTIssueReferNo,'' AS FTPLBarcodeNo "
        End Select


        Select Case DocType
            Case Barcode.DocType.Reserve

                _Str &= vbCrLf & " FROM   ( SELECT BO.*,9 AS FNStateSort  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTReserveNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTReserveNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.Issue


                _Str &= vbCrLf & " FROM   ( SELECT BO.*,9 AS FNStateSort  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTIssueNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTIssueNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
                _Str &= vbCrLf & "  ) As BO INNER JOIN"


            Case Barcode.DocType.Adjust

                _Str &= vbCrLf & " FROM            ( SELECT BO.*,9 AS FNStateSort  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTAdjustStockNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTAdjustStockNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.ReturnToStock

                _Str &= vbCrLf & " FROM            ( SELECT BO.*,9 AS FNStateSort  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToStock AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BO WITH (NOLOCK) ON H.FTReturnStockNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTReturnStockNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.ReturnToSupplier

                _Str &= vbCrLf & " FROM           ( SELECT BO.*,9 AS FNStateSort  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTReturnSuplNo = BO.FTDocumentNo) AS BO INNER JOIN"
            Case Barcode.DocType.ReturnToSupplierAfterIssue
                _Str &= vbCrLf & " FROM           ( SELECT BO.*,0 AS FNHSysWHLocationId ,'0' AS FTSateApp ,'' AS  FTSateAppBy,'' AS FTSateAppDate, '' AS FTSateAppTime,9 AS FNStateSort FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS BO WITH (NOLOCK) ON H.FTReturnSuplNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTReturnSuplNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
                _Str &= vbCrLf & "  ) As BO INNER JOIN"
            Case Barcode.DocType.SaleAndTerminate

                _Str &= vbCrLf & " FROM   ( SELECT BO.*,9 AS FNStateSort  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENSaleAndTerminate AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTSaleAndTerminateNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTSaleAndTerminateNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.ScrapBarcode

                _Str &= vbCrLf & " FROM           ( SELECT BO.*,9 AS FNStateSort  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENScrapBarcode AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTScrapNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTScrapNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.TransferCenter

                _Str &= vbCrLf & " FROM        ( SELECT BO.*,9 AS FNStateSort  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferCenter AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTTransferCenterNo = BO.FTDocumentNo "
                _Str &= vbCrLf & "  WHERE   H.FTTransferCenterNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"

                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.TransferOrder


                _Str &= vbCrLf & " FROM       ( SELECT BO.*,9 AS FNStateSort  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTTransferOrderNo = BO.FTDocumentNo"

                _Str &= vbCrLf & "  WHERE   H.FTTransferOrderNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"

                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.TransferWH

                _Str &= vbCrLf & " FROM     ( SELECT BO.*,9 AS FNStateSort  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTTransferWHNo = BO.FTDocumentNo "


                _Str &= vbCrLf & "  WHERE   H.FTTransferWHNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"

                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.Conversion

                _Str &= vbCrLf & " FROM   ( SELECT BO.*,9 AS FNStateSort  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTConversionNo = BO.FTDocumentNo"

                _Str &= vbCrLf & "  WHERE   H.FTConversionNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"

                _Str &= vbCrLf & "  ) As BO INNER JOIN"


            Case Barcode.DocType.DCPrepareIssue

                _Str &= vbCrLf & " FROM   ( SELECT BO.*,9 AS FNStateSort  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode AS BO WITH (NOLOCK) ON H.FTDCPrepareNo = BO.FTDocumentNo"
                _Str &= vbCrLf & "  WHERE   H.FTDCPrepareNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"

                _Str &= vbCrLf & "  ) As BO INNER JOIN"

            Case Barcode.DocType.CountStock

                _Str &= vbCrLf & " FROM     ( SELECT BO.*,CASE WHEN BO.FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "' THEN 1 ELSE 2 END AS FNStateSort  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENCountStock AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENCountStock_Barcode AS BO WITH (NOLOCK) ON H.FTCountStockNo = BO.FTDocumentNo "


                _Str &= vbCrLf & "  WHERE   H.FTCountStockNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"

                _Str &= vbCrLf & "  ) As BO INNER JOIN"

        End Select

        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) On BO.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With (NOLOCK) On B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With (NOLOCK) On B.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As S With (NOLOCK) On IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As C With (NOLOCK) On IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN "
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As W With (NOLOCK) On BO.FNHSysWHId = W.FNHSysWHId"
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PXTD On B.FNHSysRawMatId = PXTD.FNHSysRawMatId And B.FTOrderNo = PXTD.FTOrderNo And B.FTPurchaseNo = PXTD.FTPurchaseNo"
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocation AS WL ON BO.FNHSysWHLocationId = WL.FNHSysWHLocationId "
        _Str &= vbCrLf & "  WHERE BO.FTDocumentNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        _Str &= vbCrLf & "  ORDER BY BO.FNStateSort,BO.FTBarcodeNo"

        If _Str <> "" Then
            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        End If

        Return dt
    End Function

    Public Shared Function LoadDocumentPreapareBarcode(DocKey As String) As DataTable
        Dim _Str As String = ""
        Dim dt As New DataTable

        _Str = "  SELECT    ISNULL(W.FTWHCode,'') AS FTWHCode"
        _Str &= vbCrLf & ", BO.FTOrderNo"
        _Str &= vbCrLf & ", BO.FTBarcodeNo"
        _Str &= vbCrLf & ",B.FNHSysRawMatId"
        _Str &= vbCrLf & ", IM.FTRawMatCode"
        _Str &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & ", U.FTUnitCode"
        _Str &= vbCrLf & ", B.FTPurchaseNo"
        _Str &= vbCrLf & ", BO.FNQuantity"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameTH,'') AS  FTRawMatName"
        Else
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameEN,'') AS  FTRawMatName "
        End If

        _Str &= vbCrLf & ",ISNULL(B.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Str &= vbCrLf & ",BO.FNHSysWHId "
        _Str &= vbCrLf & ", PXTD.FTRawMatColorNameEN AS FTRawMatColorName"

        _Str &= vbCrLf & ", B.FTBatchNo"
        _Str &= vbCrLf & ", B.FTRollNo"

        _Str &= vbCrLf & ", ISNULL(BO.FTSateApp,'0') AS FTSateApp"
        _Str &= vbCrLf & ", BO.FTSateAppBy"
        _Str &= vbCrLf & ", BO.FTSateAppDate"
        _Str &= vbCrLf & ", BO.FTSateAppTime"
        _Str &= vbCrLf & ", ISNULL(WL.FTWHLocationCode,'') AS FTWHLocationCode"
        _Str &= vbCrLf & ", BO.FTIssueReferNo,BO.FTBarcodeGrpNo,BO.FTPLBarcodeNo"

        _Str &= vbCrLf & " FROM   ( SELECT BO.*  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare AS H WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode_ListPL AS BO WITH (NOLOCK) ON H.FTDCPrepareNo = BO.FTDocumentNo"
        _Str &= vbCrLf & "  WHERE   H.FTDCPrepareNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
        _Str &= vbCrLf & "  ) As BO INNER JOIN "

        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) On BO.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With (NOLOCK) On B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With (NOLOCK) On B.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As S With (NOLOCK) On IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As C With (NOLOCK) On IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN "
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As W With (NOLOCK) On BO.FNHSysWHId = W.FNHSysWHId"
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PXTD On B.FNHSysRawMatId = PXTD.FNHSysRawMatId And B.FTOrderNo = PXTD.FTOrderNo And B.FTPurchaseNo = PXTD.FTPurchaseNo"
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocation AS WL ON BO.FNHSysWHLocationId = WL.FNHSysWHLocationId "


        _Str &= vbCrLf & " WHERE BO.FTDocumentNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        _Str &= vbCrLf & " ORDER BY BO.FTBarcodeNo"

        If _Str <> "" Then
            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        End If

        Return dt
    End Function

    Public Shared Function LoadDocumentPreapareCFMBarcode(DocKey As String) As DataTable
        Dim _Str As String = ""
        Dim dt As New DataTable

        _Str = "  SELECT    ISNULL(W.FTWHCode,'') AS FTWHCode,BO.FTDCPrepareNo"
        _Str &= vbCrLf & ", BO.FTOrderNo"
        _Str &= vbCrLf & ", BO.FTBarcodeNo"
        _Str &= vbCrLf & ",B.FNHSysRawMatId"
        _Str &= vbCrLf & ", IM.FTRawMatCode"
        _Str &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & ", U.FTUnitCode"
        _Str &= vbCrLf & ", B.FTPurchaseNo"
        _Str &= vbCrLf & ", BO.FNQuantity"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameTH,'') AS  FTRawMatName"
        Else
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameEN,'') AS  FTRawMatName "
        End If

        _Str &= vbCrLf & ",ISNULL(B.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Str &= vbCrLf & ",BO.FNHSysWHId "
        _Str &= vbCrLf & ", PXTD.FTRawMatColorNameEN AS FTRawMatColorName"

        _Str &= vbCrLf & ", B.FTBatchNo"
        _Str &= vbCrLf & ", B.FTRollNo"

        _Str &= vbCrLf & ", ISNULL(BO.FTSateApp,'0') AS FTSateApp"
        _Str &= vbCrLf & ", BO.FTSateAppBy"
        _Str &= vbCrLf & ", BO.FTSateAppDate"
        _Str &= vbCrLf & ", BO.FTSateAppTime"
        _Str &= vbCrLf & ", ISNULL(WL.FTWHLocationCode,'') AS FTWHLocationCode"
        _Str &= vbCrLf & ", BO.FTTRWReferNo,BO.FTBarcodeGrpNo,BO.FTPLBarcodeNo"

        _Str &= vbCrLf & ", ISNULL(WoT.FTWHCode,'') AS FTWHCodeTo "

        _Str &= vbCrLf & " FROM   ( SELECT BO.*  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepareCFMToCar AS H WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepareCFMToCar_Barcode AS BO WITH (NOLOCK) ON H.FTDCPrepareCFMNo = BO.FTDCPrepareCFMNo"
        _Str &= vbCrLf & "  WHERE   H.FTDCPrepareCFMNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
        _Str &= vbCrLf & "  ) As BO INNER JOIN "

        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) On BO.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With (NOLOCK) On B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With (NOLOCK) On B.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As S With (NOLOCK) On IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As C With (NOLOCK) On IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN "
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As W With (NOLOCK) On BO.FNHSysWHId = W.FNHSysWHId"
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PXTD On B.FNHSysRawMatId = PXTD.FNHSysRawMatId And B.FTOrderNo = PXTD.FTOrderNo And B.FTPurchaseNo = PXTD.FTPurchaseNo"
        _Str &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocation AS WL ON BO.FNHSysWHLocationId = WL.FNHSysWHLocationId "

        _Str &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As WoT With (NOLOCK) On BO.FNHSysWHIdTo = WoT.FNHSysWHId"

        _Str &= vbCrLf & " WHERE BO.FTDCPrepareCFMNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        _Str &= vbCrLf & " ORDER BY BO.FTDCPrepareNo,BO.FTBarcodeNo"

        If _Str <> "" Then
            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        End If

        Return dt
    End Function


    Public Shared Function GetListBarcodeInForCreateTransaction(DocKey As String) As DataTable
        Dim _Qry As String = ""
        Dim dtauto As DataTable
        _Qry = " Select '1' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
        _Qry &= vbCrLf & " ,M.FTBarcodeNo "
        _Qry &= vbCrLf & " ,WS.FTWHCode"
        _Qry &= vbCrLf & ",M.FTOrderNo"
        _Qry &= vbCrLf & " ,M.FNQuantity"

        _Qry &= vbCrLf & "  , IM.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
        Else
            _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
        End If

        _Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Qry &= vbCrLf & ",M.FTFabricFrontSize"
        _Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade,M.FNPriceTrans"
        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
        _Qry &= vbCrLf & "   ,FNHSysWHId"
        _Qry &= vbCrLf & "   ,FTOrderNo"
        _Qry &= vbCrLf & "  ,FNQuantity"
        _Qry &= vbCrLf & "  ,ISNULL(("
        _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo"
        _Qry &= vbCrLf & "  AND FTDocumentRefNo = A.FTDocumentNo"
        _Qry &= vbCrLf & "  ),0) "

        _Qry &= vbCrLf & "  + ISNULL(("
        _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo"
        _Qry &= vbCrLf & "  AND FTDocumentRefNo = A.FTDocumentNo AND ISNULL(FTIssueReferNo,'') ='' "
        _Qry &= vbCrLf & "  ),0) "

        _Qry &= vbCrLf & "  + ISNULL(("
        _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo"
        _Qry &= vbCrLf & "  AND FTDocumentRefNo = A.FTDocumentNo AND FTStateIssue ='0' "
        _Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"

        _Qry &= vbCrLf & "   ,FTPurchaseNo"
        _Qry &= vbCrLf & "   ,FTDocumentNo"
        _Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade,FNPriceTrans"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,BI.FNHSysWHId,BI.FTOrderNo,SUM(BI.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  ,B.FTPurchaseNo,BI.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,ISNULL(BI.FNPriceTrans,-1) AS FNPriceTrans "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
        _Qry &= vbCrLf & "   ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE B.FTBarcodeNo = BI.FTBarcodeNo"
        _Qry &= vbCrLf & "  AND BI.FTDocumentNo='" & HI.UL.ULF.rpQuoted(DocKey) & "'"
        _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,BI.FNHSysWHId,BI.FTOrderNo,B.FTPurchaseNo,BI.FTDocumentNo "
        _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,ISNULL(BI.FNPriceTrans,-1) "
        _Qry &= vbCrLf & "  ) AS A ) AS M"
        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
        _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"
        _Qry &= vbCrLf & " ORDER BY  WS.FTWHCode,M.FTBarcodeNo"

        dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Return dtauto

    End Function

End Class

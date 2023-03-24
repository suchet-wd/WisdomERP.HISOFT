Public NotInheritable Class BarcodeAsset

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
    End Enum

    Public Shared Function CheckDucumentCreateBar(DocKey As String) As Boolean
        Dim Str As String = ""
        Str = "SELECT TOP 1 FTDocumentNo "
        Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode  AS A WITH (NOLOCK) "
        Str &= vbCrLf & " WHERE FTDocumentNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
    End Function

    Public Shared Function GetBarcode(Key As String) As DataTable
        Dim _Str As String

        _Str = "select B.FTBarcodeNo,isnull(A.FTAssetCode,AP.FTAssetPartCode) AS FTAssetCode,B.FTPurchaseNo,B.FNQuantity"

        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",isnull(A.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName,U.FTUnitAssetNameTH as FTUnitCode"
        Else
            _Str &= vbCrLf & ",isnull(A.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName,U.FTUnitAssetNameEN as FTUnitCode"
        End If
        _Str &= vbCrLf & ",B.FNHSysFixedAssetId,B.FNHSysWHAssetId"
        _Str &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH(NOLOCK) LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseAsset  AS W WITH(NOLOCK) ON B.FNHSysWHAssetId=W.FNHSysWHAssetId LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON B.FNHSysUnitId=U.FNHSysUnitAssetId LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) ON B.FNHSysFixedAssetId=A.FNHSysFixedAssetId LEFT OUTER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS C WITH (NOLOCK) ON A.FNHSysAssetModelId = C.FNHSysAssetModelId LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON B.FNHSysFixedAssetId=AP.FNHSysAssetPartId "
        _Str &= vbCrLf & "where B.FTDocumentNo='" & Key & "'"
        '91RAS-1612100003

        Return HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)

    End Function

    Public Shared Function CheckTransactionIN(_Bar As String, _Doc As String, _WHID As Integer) As Boolean
        Dim Qry As String = ""

        Qry = "select top 1 FTBarcodeNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN"
        Qry &= vbCrLf & "where FTBarcodeNo='" & _Bar & "' and FTDocumentNo='" & _Doc & "' and FNHSysWHAssetId=" & _WHID & ""

        Return (HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "") <> "")
    End Function

    Public Shared Function CheckTransactionOUT(_Bar As String, _Doc As String, _WHID As Integer) As Boolean
        Dim Qry As String = ""

        Qry = "select top 1 FTBarcodeNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT"
        Qry &= vbCrLf & "where FTBarcodeNo='" & _Bar & "' and FTDocumentNo='" & _Doc & "' and FNHSysWHAssetId=" & _WHID & ""

        Return (HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "") <> "")

    End Function


    Public Shared Function LoadDocumentBarcode(DocKey As String, DocType As DocType) As DataTable
        Dim _Str As String = ""
        Dim dt As New DataTable

        _Str = "select B.FTBarcodeNo,isnull(A.FTAssetCode,P.FTAssetPartCode)as FTAssetCode,W.FTWHAssetCode,U.FTUnitAssetCode as FTUnitCode,BO.FNHSysWHAssetId"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",isnull(A.FTAssetNameTH,P.FTAssetPartNameTH) AS FTAssetName"
        Else
            _Str &= vbCrLf & ",isnull(A.FTAssetNameEN,P.FTAssetPartNameEN) AS FTAssetName"
        End If

        _Str &= vbCrLf & ",B.FNHSysFixedAssetId"

        _Str &= vbCrLf & ", isnull(A.FTProductCode,P.FTProductCode) As FTProductNo"

        _Str &= vbCrLf & ",M.FTAssetModelCode as Model "
        Select Case DocType
            Case BarcodeAsset.DocType.Issue
                _Str &= vbCrLf & ",sum(BO.FNQuantity)as FNQuantity"
            Case BarcodeAsset.DocType.Adjust
                _Str &= vbCrLf & ",(BO.FNQuantity)as FNQuantity"
            Case BarcodeAsset.DocType.ReturnToStock
                _Str &= vbCrLf & ",sum(BO.FNQuantity)as FNQuantity"
            Case BarcodeAsset.DocType.ReturnToSupplier
                _Str &= vbCrLf & ",sum(BO.FNQuantity)as FNQuantity"
            Case BarcodeAsset.DocType.TransferCenter
                _Str &= vbCrLf & ",sum(BO.FNQuantity)as FNQuantity"
        End Select

        Select Case DocType
            'Case BarcodeAsset.DocType.Reserve

            '    _Str &= vbCrLf & " FROM   ( SELECT BO.*  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TINVENReserve AS H WITH (NOLOCK) INNER JOIN"
            '    _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON H.FTReserveNo = BO.FTDocumentNo) AS BO INNER JOIN"

            Case BarcodeAsset.DocType.Issue
                _Str &= vbCrLf & ",BO.FTAssetCode AS cFNHSysFixedAssetId,isnull(BO.HFNHSysFixedAssetId,0) AS HFNHSysFixedAssetId "
                _Str &= vbCrLf & "FROM ( SELECT BO.*,BOA.FTAssetCode,BOA.FNHSysFixedAssetId AS HFNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTIssue AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH (NOLOCK) ON H.FTIssueNo = BO.FTDocumentNo LEFT OUTER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS BOA WITH (NOLOCK) ON BO.FNHSysFixedAssetId = BOA.FNHSysFixedAssetId) AS BO INNER JOIN"

            Case BarcodeAsset.DocType.Adjust
                ' _Str &= vbCrLf & ",B.FTPurchaseNo"
                _Str &= vbCrLf & "FROM ( SELECT BO.*  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH (NOLOCK) ON H.FTAdjustStockNo = BO.FTDocumentNo) AS BO INNER JOIN"
            Case BarcodeAsset.DocType.ReturnToStock
                _Str &= vbCrLf & ",B.FTPurchaseNo"
                _Str &= vbCrLf & "FROM ( SELECT BO.*  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToStock AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BO WITH (NOLOCK) ON H.FTReturnStockNo = BO.FTDocumentNo) AS BO INNER JOIN"
            Case BarcodeAsset.DocType.ReturnToSupplier
                _Str &= vbCrLf & ",B.FTPurchaseNo"
                _Str &= vbCrLf & "FROM ( SELECT BO.*  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToSupplier AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH (NOLOCK) ON H.FTReturnSuplNo = BO.FTDocumentNo) AS BO INNER JOIN"

            Case BarcodeAsset.DocType.ScrapBarcode

            Case BarcodeAsset.DocType.TransferCenter
                _Str &= vbCrLf & ",B.FTPurchaseNo"
                _Str &= vbCrLf & "FROM ( SELECT BO.*  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTTransferWH AS H WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH (NOLOCK) ON H.FTTransferWHNo = BO.FTDocumentNo) AS BO INNER JOIN"

            Case BarcodeAsset.DocType.TransferWH

        End Select
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo   LEFT OUtER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON B.FNHSysUnitId=U.FNHSysUnitAssetId LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) ON B.FNHSysFixedAssetId=A.FNHSysFixedAssetId LEFT OUTER jOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) ON B.FNHSysFixedAssetId=P.FNHSysAssetPartId LEFT OUTER jOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH (NOLOCK) ON A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PXTD ON   B.FTPurchaseNo = PXTD.FTPurchaseNo AND B.FNHSysFixedAssetId=PXTD.FNHSysFixedAssetId LEFT OUTER JOIN "
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseAsset AS W WITH(NOLOCK) ON B.FNHSysWHAssetId=W.FNHSysWHAssetId"
        _Str &= vbCrLf & "WHERE BO.FTDocumentNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        _Str &= vbCrLf & "GROUP BY B.FTBarcodeNo,A.FTAssetCode,P.FTAssetPartCode,W.FTWHAssetCode,U.FTUnitAssetCode,BO.FNHSysWHAssetId"
        _Str &= vbCrLf & ",A.FTAssetNameTH,P.FTAssetPartNameTH,B.FNHSysFixedAssetId,A.FTProductCode,P.FTProductCode,M.FTAssetModelCode"
        Select Case DocType
            Case BarcodeAsset.DocType.Issue
                _Str &= vbCrLf & ",BO.FTAssetCode ,BO.HFNHSysFixedAssetId"
            Case BarcodeAsset.DocType.Adjust
                _Str &= vbCrLf & ",BO.FNQuantity"
            Case BarcodeAsset.DocType.ReturnToSupplier
                _Str &= vbCrLf & ",B.FTPurchaseNo"
            Case BarcodeAsset.DocType.TransferCenter
                _Str &= vbCrLf & ",B.FTPurchaseNo"
            Case BarcodeAsset.DocType.ReturnToStock
                _Str &= vbCrLf & ",B.FTPurchaseNo"
        End Select
        _Str &= vbCrLf & "ORDER BY B.FTBarcodeNo"

        If _Str <> "" Then

            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)
        End If

        Return dt
    End Function

    Public Shared Function CheckDocumentRefOut(DocKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_FIXED, "") <> "")
    End Function

    Public Shared Function CheckDocumentRefIn(DocKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE  FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_FIXED, "") <> "")
    End Function

    'Public Shared Function BarCodeBalance(BarKey As String, WHKey As String, Optional DocOutKey As String = "", Optional StateMergeReserve As Boolean = False, Optional FNAssetId As Integer = 0) As DataTable
    Public Shared Function BarCodeBalance(BarKey As String, WHKey As String, Optional DocOutKey As String = "", Optional StateMergeReserve As Boolean = False) As DataTable
        Dim Str As String

        If StateMergeReserve Then
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.SP_GET_BARCODE_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & " ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'1'  "
        Else
            Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.SP_GET_BARCODE_BALANCE '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & ",'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'0'  "
        End If

        Return HI.Conn.SQLConn.GetDataTable(Str, Conn.DB.DataBaseName.DB_FIXED)

    End Function
    Public Shared Function BarCodeOutForRet(BarKey As String, WHKey As String, Optional DocOutKey As String = "") As DataTable

        Dim Str As String
        Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.SP_GET_BARCODE_OUT_FOR_RET '" & HI.UL.ULF.rpQuoted(BarKey) & "'," & Val(WHKey) & " ,'" & HI.UL.ULF.rpQuoted(DocOutKey) & "'," & Val(HI.ST.SysInfo.CmpID) & "  "
        Return HI.Conn.SQLConn.GetDataTable(Str, Conn.DB.DataBaseName.DB_FIXED)

    End Function
End Class

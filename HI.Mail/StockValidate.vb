Public Class StockValidate

    Public Shared Function OrderUsedRawmat(FTOrderNo As String, FNHsysRawID As Integer) As Boolean
        Dim _Qry As String = ""
        Dim _FNOrderType As Integer
        Dim _State As Boolean = False
        _Qry = "SELECT TOP 1  FNOrderType "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' "

        _FNOrderType = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))


        Select Case _FNOrderType
            Case 0
                _Qry = "SELECT TOP 1  FTOrderNo "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A With(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' "
                _Qry &= vbCrLf & " AND FNHSysRawMatId=" & HI.UL.ULF.rpQuoted(FNHsysRawID) & " "

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการใช้วัตถุดิบนี้ใน Order", 1406160001, FTOrderNo, , System.Windows.Forms.MessageBoxIcon.Warning)
                Else
                    _State = True
                End If
            Case Else
                _State = True

        End Select


        Return _State

    End Function

    Public Shared Function EqualizeJob(FTReceiveNo As String, FTPurchaseNo As String, _cmd As System.Data.SqlClient.SqlCommand, _Trans As System.Data.SqlClient.SqlTransaction, _TsysMatId As Integer) As Boolean
        Try
            Dim _Str As String
            Dim _dtRcv As DataTable
            Dim _DtPoJob As DataTable
            Dim _TotalJob As Double
            Dim _Rind As Double
            Dim _TotalRcvQty As Double
            Dim _TotalRcvPOQty As Double
            Dim _TotalRcvStockQty As Double
            Dim _JobRcvQty As Double
            Dim _JobRcvPOQty As Double
            Dim _JobPOQty As Double
            Dim _SumRcvBFQty As Double
            Dim _SysUnitStock As Double
            Dim _FNConvRatio As Double = 1
            Dim _RtsQty As Double = 0
            Dim _Exc As Double = 0

            _Exc = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans("SELECt TOP 1 FNExchangeRate FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "0"))
            If _Exc <= 0 Then
                _Exc = 1
            End If
            _SysUnitStock = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans("SELECt TOP 1 FNHSysUnitId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial WHERE   FNHSysRawMatId =" & _TsysMatId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0"))

            _Str = "SELECT TOP 1 FTReceiveNo, FNHSysRawMatId, FNHSysUnitId,FTFabricFrontSize, FNPrice, FNDisPer, FNDisAmt, FNNetPrice,FNNetAmt, FNQuantity,FNSurchangePerUnit "
            _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail"
            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
            _Str &= vbCrLf & " AND    FNHSysRawMatId =" & _TsysMatId & " "
            _dtRcv = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Str)

            If _dtRcv.Rows.Count > 0 Then

                If Integer.Parse(_SysUnitStock) <> Integer.Parse(Val(_dtRcv.Rows(0)!FNHSysUnitId.ToString)) Then

                    _Str = " SELECT      TOP 1   Convert(numeric(18,5),FNRateFrom * FNRateTo)  As  FNConvRatio "
                    _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                    _Str &= vbCrLf & "  WHERE FNHSysUnitId =" & Integer.Parse(Val(_dtRcv.Rows(0)!FNHSysUnitId.ToString)) & " "
                    _Str &= vbCrLf & "  AND FNHSysUnitIdTo =" & Integer.Parse(_SysUnitStock) & " "

                    _FNConvRatio = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_INVEN, "1"))
                End If

                _Str = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order "
                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & _TsysMatId & " "

                HI.Conn.SQLConn.Execute_Tran(_Str, _cmd, _Trans)

                For Each R As DataRow In _dtRcv.Rows

                    _TotalRcvQty = Val(R!FNQuantity.ToString)
                    _TotalRcvPOQty = _TotalRcvQty
                    _TotalRcvQty = CDbl(Format(_TotalRcvQty * _FNConvRatio, HI.ST.Config.QtyFormat))
                    _TotalRcvStockQty = _TotalRcvQty

                    _Str = "   SELECT  D.FTPurchaseNo, D.FTOrderNo, D.FNHSysRawMatId, CEILING(D.FNQuantity) AS FNQuantity "
                    _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS D "
                    _Str &= vbCrLf & " LEFT OUTER JOIN ("
                    _Str &= vbCrLf & " SELECT        A.FTOrderNo"
                    _Str &= vbCrLf & " ,(CASE WHEN ISNULL(XX2.FNProcessSortDate,'') ='' THEN ( CASE WHEN A.FNOrderType =6 THEN '0000/00/00' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =7 THEN '0000/00/01' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =8 THEN '0000/00/02' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =9 THEN '0000/00/03' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =10 THEN '0000/00/04' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =11 THEN '0000/00/05' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =12 THEN '0000/00/06' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =14 THEN '0000/00/07' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =13 THEN '0000/00/08' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =1 THEN '0000/00/09' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =5 THEN '0000/00/10' "

                    _Str &= vbCrLf & "  WHEN A.FNOrderType =3 THEN '9999/00/00' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =4 THEN '9999/00/01' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =2 THEN '9999/00/02' "
                    _Str &= vbCrLf & "  WHEN A.FNOrderType =15 THEN '9999/00/03' "

                    _Str &= vbCrLf & " ELSE  ISNULL"
                    _Str &= vbCrLf & "    ((SELECT        MIN(FDShipDate) AS FDShipDate"
                    _Str &= vbCrLf & "   FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Su WITH (NOLOCK)"
                    _Str &= vbCrLf & "   WHERE        (FTOrderNo = A.FTOrderNo)), '') END) ELSE ISNULL(XX2.FNProcessSortDate,'') END) AS FDShipDate"
                    _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH (NOLOCK) "
                    _Str &= vbCrLf & " LEFT OUTER JOIN  ("
                    _Str &= vbCrLf & " SELECT  FTListName, FNListIndex AS FNOrderType , FTNameTH, FTNameEN"
                    _Str &= vbCrLf & " , FTReferCode, FTCallMnuName, FTCallMethodName"
                    _Str &= vbCrLf & " , FNProcessSortSeq, FNProcessSortDate"
                    _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS ZX WITH(NOLOCK)"
                    _Str &= vbCrLf & " WHERE  (FTListName = N'FNOrderType') "
                    _Str &= vbCrLf & " ) AS XX2 ON A.FNOrderType=XX2.FNOrderType"
                    _Str &= vbCrLf & " ) AS O ON D.FTOrderNo = O.FTOrderNo "
                    _Str &= vbCrLf & " WHERE        (D.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "') "
                    _Str &= vbCrLf & "  AND (D.FNHSysRawMatId =" & _TsysMatId & ") "
                    _Str &= vbCrLf & " ORDER BY  CASE WHEN ISNULL(O.FDShipDate,'' ) ='' THEN '9999/99/99' ELSE ISNULL(O.FDShipDate,'' ) END "

                    _DtPoJob = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Str)

                    If _DtPoJob.Rows.Count > 0 Then

                        _TotalJob = _DtPoJob.Rows.Count

                        _Rind = 0
                        For Each RPO As DataRow In _DtPoJob.Rows

                            _JobPOQty = Val(RPO!FNQuantity.ToString)
                            _JobPOQty = CDbl(Format(_JobPOQty * _FNConvRatio, HI.ST.Config.QtyFormat))

                            _Rind = _Rind + 1

                            If _TotalRcvQty > 0 Then

                                _SumRcvBFQty = 0
                                _JobRcvQty = 0
                                _JobRcvPOQty = 0
                                _RtsQty = 0

                                '-------------------ยอด Return To Supplier
                                _Str = " SELECT TOP 1 Convert(numeric(18," & HI.ST.Config.QtyDigit & " ),SUM(FNQuantity/FNConvRatio )) AS  RTSFNQuantity"
                                _Str &= vbCrLf & "     FROM"
                                _Str &= vbCrLf & " (SELECT        H.FTPurchaseNo, B.FNHSysRawMatId, SUM(BO.FNQuantity) AS FNQuantity, RC.FNConvRatio, B.FTOrderNo"
                                _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) INNER JOIN"
                                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON B.FTBarcodeNo = BO.FTBarcodeNo INNER JOIN"
                                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RC WITH(NOLOCK) ON B.FTDocumentNo = RC.FTReceiveNo AND B.FTOrderNo = RC.FTOrderNo AND B.FNHSysRawMatId = RC.FNHSysRawMatId INNER JOIN"
                                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H  WITH (NOLOCK) ON BO.FTDocumentNo = H.FTReturnSuplNo"
                                _Str &= vbCrLf & " WHERE    B.FNHSysRawMatId =" & _TsysMatId & " "
                                _Str &= vbCrLf & " AND    H.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "'"
                                _Str &= vbCrLf & " AND    B.FTOrderNo ='" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "'"
                                _Str &= vbCrLf & "  GROUP BY H.FTPurchaseNo, B.FNHSysRawMatId, RC.FNConvRatio, B.FTOrderNo) AS RTS"
                                _RtsQty = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")
                                '-------------------ยอด Return To Supplier

                                '-------------------ยอด รับก่อนหน้า
                                _Str = " SELECT SUM(FNQuantity) As FNQuantity FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order As D ,[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As H "
                                _Str &= vbCrLf & " WHERE H.FTReceiveNo=D.FTReceiveNo  "
                                _Str &= vbCrLf & " AND  H.FTReceiveNo <>'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                _Str &= vbCrLf & " AND    D.FNHSysRawMatId =" & _TsysMatId & " "
                                _Str &= vbCrLf & " AND    H.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "'"
                                _Str &= vbCrLf & " AND    D.FTOrderNo ='" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "'"
                                '-------------------ยอด รับก่อนหน้า

                                _SumRcvBFQty = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_INVEN, "0"))
                                _SumRcvBFQty = CDbl(Format(_SumRcvBFQty * _FNConvRatio, HI.ST.Config.QtyFormat))

                                _SumRcvBFQty = _SumRcvBFQty - _RtsQty

                                If _SumRcvBFQty <= 0 Then
                                    _SumRcvBFQty = 0
                                End If

                                If _Rind = _TotalJob Or (_JobPOQty - _SumRcvBFQty) > 0 Then

                                    If _Rind = _TotalJob Then
                                        _JobRcvQty = _TotalRcvQty
                                    Else
                                        If _JobPOQty - _SumRcvBFQty > 0 Then
                                            If _TotalRcvQty > (_JobPOQty - _SumRcvBFQty) Then
                                                _JobRcvQty = (_JobPOQty - _SumRcvBFQty)
                                            Else
                                                _JobRcvQty = _TotalRcvQty
                                            End If

                                        Else
                                            _JobRcvQty = _TotalRcvQty
                                        End If
                                    End If

                                    _JobRcvPOQty = CDbl(Format((_JobRcvQty * _TotalRcvPOQty) / _TotalRcvStockQty, HI.ST.Config.QtyFormat))

                                    _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order( FTInsUser, FDInsDate, FTInsTime,FTReceiveNo, FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, "
                                    _Str &= vbCrLf & " FNNetPrice, FNNetAmt, FNQuantity, FNQuantityStock, FNHSysUnitIdStock, FNPricePerStock, FNConvRatio, FNNetStockAmt,FTFabricFrontSize,FNSurchangePerUnit) "
                                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "' "
                                    _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNDisPer.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNDisAmt.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNNetPrice.ToString) & " "
                                    _Str &= vbCrLf & "," & CDbl(Format(Val(R!FNNetPrice.ToString) * Val(_JobRcvPOQty), HI.ST.Config.AmtFormat)) & " "
                                    _Str &= vbCrLf & "," & _JobRcvPOQty & " "
                                    _Str &= vbCrLf & "," & _JobRcvQty & " "
                                    _Str &= vbCrLf & "," & _SysUnitStock & " "
                                    _Str &= vbCrLf & "," & CDbl(Format((Val(R!FNNetPrice.ToString) * _Exc) / Val(_FNConvRatio), HI.ST.Config.PriceFormat)) & " "
                                    _Str &= vbCrLf & "," & _FNConvRatio & " "
                                    _Str &= vbCrLf & "," & CDbl(Format(CDbl(Format((Val(R!FNNetPrice.ToString) * _Exc) / Val(_FNConvRatio), HI.ST.Config.PriceFormat)) * Val(_JobRcvQty), HI.ST.Config.AmtFormat)) & " "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                    _Str &= vbCrLf & "," & Val(R!FNSurchangePerUnit.ToString) & " "

                                    If HI.Conn.SQLConn.ExecuteTran(_Str, _cmd, _Trans) = False Then
                                        Return False
                                    End If

                                End If

                                _TotalRcvQty = _TotalRcvQty - _JobRcvQty

                            End If
                        Next
                    End If
                Next

            Else

                _Str = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order "
                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & _TsysMatId & " "

                HI.Conn.SQLConn.ExecuteTran(_Str, _cmd, _Trans)

            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function CheckDocCreateInvoiceSale(DocNo As String) As Boolean
        Dim _Qry As String = ""

        _Qry = "SELECT TOP 1  FTDocRefNo "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDocRefNo='" & HI.UL.ULF.rpQuoted(DocNo) & "' "

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> "")

    End Function
End Class

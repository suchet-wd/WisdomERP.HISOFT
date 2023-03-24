Public Class StockValidate

    Public Shared Function OrderUsedRawmat(FTOrderNo As String, FNHsysRawID As Integer, Optional ShowMsg As Boolean = True) As Boolean
        Dim _Qry As String = ""
        Dim _FNOrderType As Integer
        Dim _State As Boolean = False
        _Qry = "SELECT TOP 1  FNOrderType "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' "

        _FNOrderType = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "-1")))


        Select Case _FNOrderType
            Case 0

                _Qry = "  SELECT  TOP 1  MM.FTStateNotCheckResuorce"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)   INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat  AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode"
                _Qry &= vbCrLf & "    WHERE (IM.FNHSysRawMatId =" & Val(FNHsysRawID) & ")"

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "1" Then

                    _State = True

                Else

                    _Qry = "SELECT TOP 1  FTOrderNo "
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A With(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo) & "' "
                    _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Val(FNHsysRawID) & " "

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then

                        If ShowMsg Then
                            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการใช้วัตถุดิบนี้ใน Order", 1406160001, FTOrderNo, , System.Windows.Forms.MessageBoxIcon.Warning)
                        End If

                    Else

                        _State = True
                    End If

                End If

            Case Else

                _State = True

        End Select

        Return _State

    End Function

    Public Shared Function OrderSateNotIssue(OrderNo As String) As Boolean
        Dim _Qry As String = ""
        Dim _FNOrderType As Integer = 0

        Dim _FNJobState As Integer = 0
        Dim _State As Boolean = False
        Dim dt As DataTable

        _Qry = "SELECT TOP 1  FNOrderType,FNJobState "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each Rx As DataRow In dt.Rows
            _FNOrderType = Val(Rx!FNOrderType.ToString)
            _FNJobState = Val(Rx!FNJobState.ToString)
        Next

        Select Case _FNOrderType
            Case 4
                _State = True
            Case Else
                _State = False
        End Select

        If _State = False Then

            Select Case _FNJobState
                Case 2, 3
                    _State = True
                Case Else
                    _State = False
            End Select

        End If

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
            Dim _MerMatType As Integer = 0

            _Exc = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans("SELECt TOP 1 FNExchangeRate FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "0"))

            If _Exc <= 0 Then
                _Exc = 1
            End If

            _SysUnitStock = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans("SELECt TOP 1 FNHSysUnitId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial WHERE   FNHSysRawMatId =" & _TsysMatId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0"))

            _Str = "SELECT TOP 1 R.FTReceiveNo, R.FNHSysRawMatId, R.FNHSysUnitId,R.FTFabricFrontSize, R.FNPrice, R.FNDisPer, R.FNDisAmt, R.FNNetPrice,R.FNNetAmt, R.FNQuantity,R.FNSurchangePerUnit,ISNULL(MM.FNMerMatType,0) AS FNMerMatType "
            _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS R"
            _Str &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM ON R.FNHSysRawMatId = IM.FNHSysRawMatId"
            _Str &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode"
            _Str &= vbCrLf & " WHERE   R.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
            _Str &= vbCrLf & "         AND R.FNHSysRawMatId =" & _TsysMatId & " "

            _dtRcv = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Str)

            If _dtRcv.Rows.Count > 0 Then

                If Integer.Parse(_SysUnitStock) <> Integer.Parse(Val(_dtRcv.Rows(0)!FNHSysUnitId.ToString)) Then

                    _Str = " SELECT      TOP 1   Convert(numeric(18,10),CASE WHEN FNRateFrom > 1 THEN FNRateTo/FNRateFrom  ELSE  FNRateFrom * FNRateTo END)  As  FNConvRatio "
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

                    _MerMatType = Val(R!FNMerMatType.ToString)
                    _TotalRcvQty = Val(R!FNQuantity.ToString)
                    _TotalRcvPOQty = _TotalRcvQty
                    _TotalRcvQty = CDbl(Format(_TotalRcvQty * _FNConvRatio, HI.ST.Config.QtyFormat))
                    _TotalRcvStockQty = _TotalRcvQty

                    _Str = "   SELECT  D.FTPurchaseNo, D.FTOrderNo, D.FNHSysRawMatId,CASE WHEN " & _MerMatType & " =0 THEN  CEILING(D.FNQuantity) ELSE D.FNQuantity END AS FNQuantity,ISNULL(RTS.RTSFNQuantity,0) AS RTSFNQuantity ,ISNULL(RCVBF.FNQuantityRCVBF,0) AS FNQuantityRCVBF"
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

                    _Str &= vbCrLf & "  OUTER APPLY ( "
                    _Str &= vbCrLf & "  SELECT TOP 1 Convert(numeric(18," & HI.ST.Config.QtyDigit & " ),SUM(FNQuantity/FNConvRatio )) AS  RTSFNQuantity"
                    _Str &= vbCrLf & "     FROM"
                    _Str &= vbCrLf & " (SELECT        H.FTPurchaseNo, B.FNHSysRawMatId, SUM(BO.FNQuantity) AS FNQuantity, RC.FNConvRatio, B.FTOrderNo"
                    _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) INNER JOIN"
                    _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON B.FTBarcodeNo = BO.FTBarcodeNo INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RC WITH(NOLOCK) ON B.FTDocumentNo = RC.FTReceiveNo AND B.FTOrderNo = RC.FTOrderNo AND B.FNHSysRawMatId = RC.FNHSysRawMatId INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H  WITH (NOLOCK) ON BO.FTDocumentNo = H.FTReturnSuplNo"
                    _Str &= vbCrLf & " WHERE    B.FNHSysRawMatId =D.FNHSysRawMatId "
                    _Str &= vbCrLf & " AND    H.FTPurchaseNo =D.FTPurchaseNo"
                    _Str &= vbCrLf & " AND    B.FTOrderNo =D.FTOrderNo "
                    _Str &= vbCrLf & "  GROUP BY H.FTPurchaseNo, B.FNHSysRawMatId, RC.FNConvRatio, B.FTOrderNo) AS RTS"
                    _Str &= vbCrLf & "  ) AS RTS "

                    _Str &= vbCrLf & "  OUTER APPLY ( "
                    _Str &= vbCrLf & "  SELECT SUM(DX.FNQuantity) As FNQuantityRCVBF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order As DX INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As HX ON DX.FTReceiveNo=HX.FTReceiveNo  "
                    _Str &= vbCrLf & " WHERE HX.FTPurchaseNo =D.FTPurchaseNo "
                    _Str &= vbCrLf & " AND  HX.FTReceiveNo <>'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                    _Str &= vbCrLf & " AND    DX.FNHSysRawMatId =D.FNHSysRawMatId "
                    _Str &= vbCrLf & " AND    DX.FTOrderNo =D.FTOrderNo"
                    _Str &= vbCrLf & "  ) AS RCVBF "

                    _Str &= vbCrLf & " WHERE        (D.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "') "
                    _Str &= vbCrLf & "  AND (D.FNHSysRawMatId =" & _TsysMatId & ") "
                    _Str &= vbCrLf & " ORDER BY  CASE WHEN ISNULL(O.FDShipDate,'0000/00/11' ) ='' THEN '9999/99/99' ELSE ISNULL(O.FDShipDate,'0000/00/11' ) END "
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


                                _RtsQty = Val(RPO!RTSFNQuantity.ToString)
                                _SumRcvBFQty = Val(RPO!FNQuantityRCVBF.ToString)

                                ''-------------------ยอด Return To Supplier
                                '_Str = " SELECT TOP 1 Convert(numeric(18," & HI.ST.Config.QtyDigit & " ),SUM(FNQuantity/FNConvRatio )) AS  RTSFNQuantity"
                                '_Str &= vbCrLf & "     FROM"
                                '_Str &= vbCrLf & " (SELECT        H.FTPurchaseNo, B.FNHSysRawMatId, SUM(BO.FNQuantity) AS FNQuantity, RC.FNConvRatio, B.FTOrderNo"
                                '_Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) INNER JOIN"
                                '_Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON B.FTBarcodeNo = BO.FTBarcodeNo INNER JOIN"
                                '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RC WITH(NOLOCK) ON B.FTDocumentNo = RC.FTReceiveNo AND B.FTOrderNo = RC.FTOrderNo AND B.FNHSysRawMatId = RC.FNHSysRawMatId INNER JOIN"
                                '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H  WITH (NOLOCK) ON BO.FTDocumentNo = H.FTReturnSuplNo"
                                '_Str &= vbCrLf & " WHERE    B.FNHSysRawMatId =" & _TsysMatId & " "
                                '_Str &= vbCrLf & " AND    H.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "'"
                                '_Str &= vbCrLf & " AND    B.FTOrderNo ='" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "'"
                                '_Str &= vbCrLf & "  GROUP BY H.FTPurchaseNo, B.FNHSysRawMatId, RC.FNConvRatio, B.FTOrderNo) AS RTS"
                                '_RtsQty = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")
                                ''-------------------ยอด Return To Supplier

                                ''-------------------ยอด รับก่อนหน้า
                                '_Str = " SELECT SUM(FNQuantity) As FNQuantity FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order As D INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As H ON D.FTReceiveNo=H.FTReceiveNo  "
                                '_Str &= vbCrLf & " WHERE H.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "' "
                                '_Str &= vbCrLf & " AND  H.FTReceiveNo <>'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                '_Str &= vbCrLf & " AND    D.FNHSysRawMatId =" & _TsysMatId & " "
                                '_Str &= vbCrLf & " AND    D.FTOrderNo ='" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "'"
                                ''-------------------ยอด รับก่อนหน้า

                                '_SumRcvBFQty = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_INVEN, "0"))
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

        _Qry = "SELECT TOP 1  A.FTDocRefNo "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS A With(NOLOCK) "
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS AH With(NOLOCK) ON A.FTInvoiceNo = AH.FTInvoiceNo  "
        _Qry &= vbCrLf & " WHERE A.FTDocRefNo='" & HI.UL.ULF.rpQuoted(DocNo) & "' AND ISNULL(AH.FTStateCancel,'') <> '1' "

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "") <> "")

    End Function

    Public Shared Function CheckCloseStock(FNHSysWHId As Integer, FTDateTrans As String) As Boolean
        Dim _State As Boolean = False
        Dim _Qry As String = ""

        If FNHSysWHId > 0 Then
            _Qry = " SELECT TOP 1  FTYear + '/' + FTMonth + '/31' AS FTCloseMonthYear"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENStockLastMonthly AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysWHId=" & FNHSysWHId & " "
            _Qry &= vbCrLf & "  AND  FTYear + '/' + FTMonth + '/31'>='" & HI.UL.ULDate.ConvertEnDB(FTDateTrans) & "' "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                HI.MG.ShowMsg.mInfo("ช่วงเวลานี้ ของ คลังนี้ได้มีการ ปิด สต๊อกไปแล้ว ไม่สามารถทำการแก้ไขรายการใดๆเพิ่มได้อีก !!!", 1496160701, "", , System.Windows.Forms.MessageBoxIcon.Warning)
                _State = True
            End If

        Else

            HI.MG.ShowMsg.mInfo("ข้องมูลคลังสินค้าไม่ถูกต้อง !!!", 1496160781, "", , System.Windows.Forms.MessageBoxIcon.Warning)
            _State = True

        End If

        Return _State
    End Function



    Public Shared Function AutoPurchase(TransferWHno As String, transferwhdate As String, SysCmpId As Integer, SysWHCmpId As Integer, ReceiveDate As String, PORef As String, Optional pofaccreate As String = "") As String()

        Dim cmdstring As String = ""
        Dim pofacno As String = ""

        Try
            Dim vatper As Double = 0

            Dim poamt As Double = 0
            Dim podisamt As Double = 0
            Dim ponetamt As Double = 0
            Dim povatamt As Double = 0
            Dim pograndamt As Double = 0
            Dim poamtth As String
            Dim poamten As String
            Dim Surcharge As Double = 0
            Dim FNHSysDeliveryId As Integer = 0
            Dim SysCurId As Integer = 1310200002
            Dim ExcRate As Double = 1
            Dim POFacExcRate As Double = 1
            Dim _POType As String = ""
            Dim FNPoState As Integer = 0

            If ((SysCmpId = 1311090006 Or SysCmpId = 1311090005 Or SysCmpId = 1410220001 Or SysCmpId = 1501190001) And (SysWHCmpId <> 1311090006 And SysWHCmpId <> 1311090005 And SysWHCmpId <> 1410220001 And SysWHCmpId <> 1501190001)) Or
                ((SysWHCmpId = 1311090006 Or SysWHCmpId = 1311090005 Or SysWHCmpId = 1410220001 Or SysWHCmpId = 1501190001) And (SysCmpId <> 1311090006 And SysCmpId <> 1311090005 And SysCmpId <> 1410220001 And SysCmpId <> 1501190001)) Then

                vatper = 0

                SysCurId = 1310190001

                Dim _Qry As String = ""

                _Qry = " Select TOP 1 FNBuyingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  With(NOLOCK)  "

                If ReceiveDate <> "" Then
                    _Qry &= vbCrLf & "   WHERE  (FDDate ='" & ReceiveDate & "')"
                Else
                    _Qry &= vbCrLf & "   WHERE  (FDDate ='" & HI.UL.ULDate.ConvertEnDB(transferwhdate) & "')"
                End If

                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTCurCode='USD'"
                _Qry &= vbCrLf & "  ))"

                ExcRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "1")))

                _POType = "I"
                FNPoState = 1

            Else

                vatper = 7
                SysCurId = 1310200002

                ExcRate = 1
                _POType = "D"

                FNPoState = 0

            End If

            Dim pokey As String = ""
            pokey = PORef

            Dim DeliveryId As Integer = 0
            cmdstring = "select  TOP 1 FNHSysDeliveryId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery AS X WITH(NOLOCK)  WHERE  FNHSysCmpId =" & Val(SysCmpId) & " AND ISNULL(FNHSysCmpIdTo,0) = 0"
            DeliveryId = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "0"))

            Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
            Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
            Dim _CmpHCreate As String = ""
            Dim _CmpRunText As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 13), 2)
            Dim _POGrp As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 9), 2)
            Dim UserDoucument As String = ""
            Dim dtuser As DataTable
            cmdstring = "Select TOP 1 FTDocRun,FTUserNameAutoPOFac FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp With(NOLOCK) WHERE FNHSysCmpId=" & Val(SysCmpId) & " "

            dtuser = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

            For Each Rxc As DataRow In dtuser.Rows

                _CmpHCreate = Rxc!FTDocRun.ToString()
                UserDoucument = Rxc!FTUserNameAutoPOFac.ToString()

                Exit For
            Next
            dtuser.Dispose()

            If UserDoucument = "" Then
                UserDoucument = HI.ST.UserInfo.UserName

            End If
            If pofaccreate = "" Then
                pofacno = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTFacPurchase", "", False, _CmpHCreate & "F" & _CmpRunText & _Year & _POGrp & _POType & _Month).ToString
            End If

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try
                If pofaccreate = "" Then



                    cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
                    cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
                    cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' FTInsUser"
                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " AS FDInsDate "
                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " AS  FDPurchaseDate"
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(UserDoucument) & "' AS FTPurchaseBy"
                    cmdstring &= vbCrLf & " ,'AUTO FROM TRANSFER WH NO ' +  '" & HI.UL.ULF.rpQuoted(TransferWHno) & "' AS  FTPurchaseState,'' FTRefer," & FNPoState & " AS  FNPoState"
                    cmdstring &= vbCrLf & ",FNHSysPurGrpId"
                    cmdstring &= vbCrLf & " ,FNHSysCmpRunId," & SysWHCmpId & " AS  FNHSysCmpId," & HI.UL.ULDate.FormatDateDB & " AS  FDDeliveryDate"
                    cmdstring &= vbCrLf & " , FNHSysCrTermId"
                    cmdstring &= vbCrLf & "  , FNCreditDay"
                    cmdstring &= vbCrLf & " , FNHSysTermOfPMId," & SysCurId & " AS FNHSysCurId," & ExcRate & " AS  FNExchangeRate, CASE WHEN " & DeliveryId & " >0 THEN " & DeliveryId & " ELSE  FNHSysDeliveryId END "
                    cmdstring &= vbCrLf & " ,'' AS  FTContactPerson"
                    cmdstring &= vbCrLf & " ,'' AS  FDSampleAppDate,'' AS FDSignDate "
                    cmdstring &= vbCrLf & " ,'' AS FDBLDate ,'' AS FDSuplCfmDliDate,'' AS FDCfmDate,'' AS FTRemark"
                    cmdstring &= vbCrLf & ",0 As FNPoAmt"
                    cmdstring &= vbCrLf & ",0 AS FNDisCountPer"
                    cmdstring &= vbCrLf & ",0 AS FNDisCountAmt"
                    cmdstring &= vbCrLf & ",0 AS FNPONetAmt," & vatper & " FNVatPer"
                    cmdstring &= vbCrLf & ",0 AS FNVatAmt"
                    cmdstring &= vbCrLf & ",0 AS FNSurcharge"
                    cmdstring &= vbCrLf & ",0 AS  FNPOGrandAmt"
                    cmdstring &= vbCrLf & ",'' AS FTPOGrandAmtTH"
                    cmdstring &= vbCrLf & ",'' AS  FTPOGrandAmtEN"
                    cmdstring &= vbCrLf & ",'1' AS   FTStateSendApp,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSendAppBy," & HI.UL.ULDate.FormatDateDB & "  AS FTSendAppDate," & HI.UL.ULDate.FormatTimeDB & " AS FTSendAppTime,  "
                    cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(TransferWHno) & "' AS FTPurchaseNoRef," & SysCmpId & " AS  FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return {""}
                    End If


                Else
                    pofacno = pofaccreate
                End If


                cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo ("
                cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo,  FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRemark, "
                cmdstring &= vbCrLf & "    FTFabricFrontSize, FNReservePOQuantity, FTRawMatColorNameTH, FTRawMatColorNameEN, FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate,FTPurchaseNo"
                cmdstring &= vbCrLf & ")"
                cmdstring &= vbCrLf & "  Select   "
                cmdstring &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "  , " & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & "  , " & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & "  ,   '" & HI.UL.ULF.rpQuoted(pofacno) & "' "
                cmdstring &= vbCrLf & "  ,  FTOrderNo "
                cmdstring &= vbCrLf & "  ,FNHSysRawMatId "
                cmdstring &= vbCrLf & "  ,FNHSysUnitId"
                cmdstring &= vbCrLf & " ,(FNPrice + Convert(numeric(18,4),((FNPrice * ISNULL(FNChargePer,0))/100.00))) AS FNPrice,0 AS FNDisPer,0 AS FNDisAmt"
                cmdstring &= vbCrLf & " ,FNQuantity"
                cmdstring &= vbCrLf & "  ,CONVERT(numeric(18,2),((FNPrice + Convert(numeric(18,4),((FNPrice * ISNULL(FNChargePer,0))/100.00))) ) * FNQuantity) AS FNNetAmt,'' AS FTRemark"
                cmdstring &= vbCrLf & " ,FTFabricFrontSize,0 AS FNReservePOQuantity"
                cmdstring &= vbCrLf & "  ,'' AS  FTRawMatColorNameTH,'' AS  FTRawMatColorNameEN,0 AS FNSurchangeAmt,0 AS  FNSurchangePerUnit,CONVERT(numeric(18,2),FNPrice * FNQuantity)  AS FNGrandNetAmt,'' AS FTOGacDat"
                cmdstring &= vbCrLf & " ,FTPurchaseNo"
                cmdstring &= vbCrLf & " FROM(Select B.FTOrderNo"
                cmdstring &= vbCrLf & "  , BB.FNHSysRawMatId"
                cmdstring &= vbCrLf & "  , MAX(BB.FNHSysUnitId) As FNHSysUnitId"
                cmdstring &= vbCrLf & "  , MAX(CASE WHEN ISNULL(BI.FNPriceTrans,0) <=0 THEN BB.FNPrice ELSE ISNULL(BI.FNPriceTrans,0) END) AS FNPrice		"
                cmdstring &= vbCrLf & " , SUM(B.FNQuantity) As FNQuantity"
                cmdstring &= vbCrLf & "  , MAX(BB.FTFabricFrontSize) AS FTFabricFrontSize"
                cmdstring &= vbCrLf & "  , MAX(BB.FTPurchaseNo) As FTPurchaseNo"
                cmdstring &= vbCrLf & "  , MAX(ISNULL(PCG.FNChargePer,0)) As FNChargePer"

                cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As BB WITH(NOLOCK) INNER Join"
                cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As B WITH(NOLOCK)  On BB.FTBarcodeNo = B.FTBarcodeNo INNER Join"
                cmdstring &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A WITH(NOLOCK)  On B.FTDocumentNo = A.FTTransferWHNo"
                cmdstring &= vbCrLf & " OUTER APPLY (SELECT TOP 1 FNPriceTrans FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As BIX WITH(NOLOCK) WHERE BIX.FTBarcodeNo=B.FTBarcodeNo AND BIX.FTOrderNo= B.FTOrderNo AND BIX.FTDocumentNo =B.FTDocumentRefNo ) AS BI "


                cmdstring &= vbCrLf & " OUTER APPLY ( "
                cmdstring &= vbCrLf & "  SELECT TOP 1  ISNULL(CH.FNChargePer, 0) AS FNChargePer "

                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS AX "
                cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS AXH  ON AX.FTReceiveNo=AXH.FTReceiveNo"
                cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  ON AXH.FTPurchaseNo=H.FTPurchaseNo"
                cmdstring &= vbCrLf & "  INNER Join "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On AX.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
                cmdstring &= vbCrLf & " (SELECT  *  "
                cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
                cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & SysCmpId & ""
                cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType AND H.FNPoState=CH.FNPoType"
                cmdstring &= vbCrLf & " WHERE AX.FTReceiveNo=B.FTDocumentRefNo AND AX.FNHSysRawMatId=BB.FNHSysRawMatId"

                cmdstring &= vbCrLf & "   ) As  PCG "


                cmdstring &= vbCrLf & " Where (A.FTTransferWHNo = N'" & HI.UL.ULF.rpQuoted(TransferWHno) & "')"
                cmdstring &= vbCrLf & " Group By B.FTOrderNo, BB.FNHSysRawMatId"
                cmdstring &= vbCrLf & " ) As R"


                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return {""}
                End If


                If ExcRate > 0 Then

                    cmdstring = " UPDATE B SET "
                    cmdstring &= vbCrLf & "B.FNPrice = Convert(numeric(18,4),(B.FNPrice )/" & ExcRate & " )"
                    cmdstring &= vbCrLf & ", B.FNDisAmt = Convert(numeric(18,4),(B.FNDisAmt)/" & ExcRate & " )"
                    cmdstring &= vbCrLf & " , B.FNNetAmt = Convert(numeric(18,4),(B.FNNetAmt )/" & ExcRate & " )"
                    cmdstring &= vbCrLf & ", B.FNSurchangeAmt = Convert(numeric(18,4),(B.FNSurchangeAmt )/" & ExcRate & " )"
                    cmdstring &= vbCrLf & ", B.FNSurchangePerUnit = Convert(numeric(18,4),(B.FNSurchangePerUnit )/" & ExcRate & " )"
                    cmdstring &= vbCrLf & ", B.FNGrandNetAmt = Convert(numeric(18,2),(B.FNGrandNetAmt )/" & ExcRate & " )"
                    cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A INNER Join"
                    cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As B On A.FTFacPurchaseNo = B.FTFacPurchaseNo "
                    cmdstring &= vbCrLf & " WHERE A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return {""}
                    End If

                End If


                podisamt = CDbl(Format(podisamt / ExcRate, "0.00")) ' Val(R!FNDisCountAmt.ToString)
                Surcharge = CDbl(Format(Surcharge / ExcRate, "0.00"))  'Val(R!FNSurcharge.ToString)

                cmdstring = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                cmdstring &= vbCrLf & "    FROM"
                cmdstring &= vbCrLf & " ("
                cmdstring &= vbCrLf & " SELECT        FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  WITH(NOLOCK)"
                cmdstring &= vbCrLf & " WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "' "
                cmdstring &= vbCrLf & " GROUP BY FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                poamt = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
                ponetamt = poamt - podisamt
                povatamt = CDbl(Format((ponetamt * vatper) / 100.0, "0.00"))
                pograndamt = ponetamt + povatamt + Surcharge

                poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
                poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

                cmdstring = "UPDATE A Set "
                cmdstring &= vbCrLf & "  FNPoAmt=" & poamt & ""
                cmdstring &= vbCrLf & ", FNPONetAmt=" & ponetamt & ""
                cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
                cmdstring &= vbCrLf & ", FNPOGrandAmt=" & pograndamt & ""
                cmdstring &= vbCrLf & ", FNSurcharge=" & Surcharge & ""
                cmdstring &= vbCrLf & ", FNDisCountAmt=" & podisamt & ""
                cmdstring &= vbCrLf & ", FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
                cmdstring &= vbCrLf & ", FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"
                cmdstring &= vbCrLf & ",FTStateSendApp='1'"
                cmdstring &= vbCrLf & ",FTSendAppBy='" & HI.UL.ULF.rpQuoted(UserDoucument) & "'"
                cmdstring &= vbCrLf & ",FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
                cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return {""}
                End If

            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return {""}
            End Try


            Dim InvoiceNo As String = AutoSaleInvoice(TransferWHno, transferwhdate, pofacno, SysCurId, ExcRate, vatper, SysCmpId, SysWHCmpId)

            If InvoiceNo = "" Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return {""}

            Else
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return {pofacno, InvoiceNo}
            End If

        Catch ex As Exception
            Return {""}
        End Try


        Return {""}

    End Function

    Private Shared Function AutoSaleInvoice(TransferWHno As String, transferwhdate As String, ByVal pofacno As String, SysCurId As Integer, Rateexchange As Decimal, Vat As Decimal, CmpIdTo As Integer, SysWHCmpId As Integer) As String


        Try
            Dim _Qry As String = ""
            Dim Invoice As String = ""
            Dim _CmpH As String = ""


            Dim UserDoucument As String = ""
            Dim dtuser As DataTable
            _Qry = "Select TOP 1 FTDocRun,FTUserNameAutoInvoice  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp With(NOLOCK) WHERE FNHSysCmpId=" & SysWHCmpId & " "

            dtuser = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            For Each Rxc As DataRow In dtuser.Rows

                _CmpH = Rxc!FTDocRun.ToString()
                UserDoucument = Rxc!FTUserNameAutoInvoice.ToString()

                Exit For
            Next
            dtuser.Dispose()

            If UserDoucument = "" Then
                UserDoucument = HI.ST.UserInfo.UserName
            End If


            Invoice = HI.TL.Document.GetDocumentNoOnBeginTrans("HITECH_ACCOUNT", "TACCTSaleInvoice", "2", False, _CmpH).ToString

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice"
            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTInvoiceNo, FDInvoiceDate, FTInvoiceBy, FTDocRefNo, FNInvoiceState"
            _Qry &= vbCrLf & ", FNHSysCmpIdTo, FTRemark, FNInvAmt, FNDisCountPer, FNDisCountAmt, "
            _Qry &= vbCrLf & "  FNInvNetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNInvGrandAmt"
            _Qry &= vbCrLf & " , FTInvGrandAmtTH, FTInvGrandAmtEN, FNHSysCmpId, FNHSysShipModeId, FTPaymentTerms, FTReferenceNo"
            _Qry &= vbCrLf & " ,  FTECNo, FNHSysCurId, FNExchangeRate,  "
            _Qry &= vbCrLf & "   FNChargeService, FNChargeClear, FNHSysTermOfPMId, FTStateAuto,FTDocAutoRefNo,,FDDateSailing  "
            _Qry &= vbCrLf & " )"
            _Qry &= vbCrLf & "  Select   "
            _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  , " & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "  , " & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & "  ,   '" & HI.UL.ULF.rpQuoted(Invoice) & "' "
            _Qry &= vbCrLf & "  ,   '" & HI.UL.ULDate.ConvertEnDB(transferwhdate) & "' "
            _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(UserDoucument) & "' "
            _Qry &= vbCrLf & " ,'' As  FTDocRefNo,2 AS  FNInvoiceState"
            _Qry &= vbCrLf & "," & CmpIdTo & " AS  FNHSysCmpIdTo,'' AS  FTRemark, 0 AS  FNInvAmt, 0 AS FNDisCountPer,0 AS FNDisCountAmt, "
            _Qry &= vbCrLf & "  0 AS FNInvNetAmt," & Vat & " FNVatPer, 0 AS FNVatAmt,0 AS FNSurcharge,0 AS FNInvGrandAmt"
            _Qry &= vbCrLf & " , '' AS FTInvGrandAmtTH,'' FTInvGrandAmtEN," & HI.ST.SysInfo.CmpID & " AS  FNHSysCmpId"
            _Qry &= vbCrLf & " ,1406250001 AS  FNHSysShipModeId,'' FTPaymentTerms,'' FTReferenceNo"
            _Qry &= vbCrLf & " ,'' FTECNo," & SysCurId & " AS  FNHSysCurId," & Rateexchange & " AS   FNExchangeRate,  "
            _Qry &= vbCrLf & "  0 AS  FNChargeService,0 AS FNChargeClear,1405060001 AS FNHSysTermOfPMId,'1' AS  FTStateAuto "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(TransferWHno) & "' "
            _Qry &= vbCrLf & "  ,   '" & HI.UL.ULDate.ConvertEnDB(transferwhdate) & "' "
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return ""
            End If

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef"
            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTInvoiceNo, FTDocRefNo)"
            _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Invoice) & "'"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(TransferWHno) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return ""
            End If


            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail"
            _Qry &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail"
            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTInvoiceNo, FNHSysRawMatId, FNPrice, FNQuantity"
            _Qry &= vbCrLf & ",FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice,FNHSysUnitId,FNCTN, FNNW, FNGW, FNQBM,FNGrpSeq,FNChargeServicePer,FNChargeClearPer)"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Invoice) & "'"
            _Qry &= vbCrLf & ", FNHSysRawMatId"
            _Qry &= vbCrLf & ",FNPrice"
            _Qry &= vbCrLf & ",FNQuantity"
            _Qry &= vbCrLf & ",FNPriceSale"
            _Qry &= vbCrLf & ",0 AS FNChargeService"
            _Qry &= vbCrLf & ",0 AS FNChargeService"
            _Qry &= vbCrLf & ",FNNetPrice"
            _Qry &= vbCrLf & ",FNHSysUnitId_Hide"
            _Qry &= vbCrLf & ",0 AS FNCTN"
            _Qry &= vbCrLf & ",0 AS FNNW"
            _Qry &= vbCrLf & ",0 AS FNGW "
            _Qry &= vbCrLf & ",0 AS FNQBM "
            _Qry &= vbCrLf & ",FNGrpSeq"
            _Qry &= vbCrLf & ",0 AS FNChargeServicePer"
            _Qry &= vbCrLf & ",0 AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM ( Select     TT.FTBarcodeNo, TT.FTOrderNo,SUM(Isnull(B_1.FNQuantity ,TT.FNQuantity )) As FNQuantity , TT.FNHSysCmpId, TT.FNHSysWHId, TT.FTDocumentNo, max(TT.FTFabricFrontSize) As FTFabricFrontSize, TT.FTPurchaseNo,SUM(Isnull(B_1.FNQuantity ,TT.FNQuantity )) * TT.FNPrice As FNAmount, TT.FNHSysRawMatId, "
            _Qry &= vbCrLf & "  TT.FTRawMatCode, TT.FTRawMatNameTH, TT.FTRawMatNameEN, TT.FTRawMatColorCode, TT.FTRawMatSizeCode, TT.FTRawMatSizeNameTH, TT.FTRawMatSizeNameEN, TT.FNHSysUnitId, "
            _Qry &= vbCrLf & "   TT.FTUnitNameTH, TT.FTUnitNameEN, TT.FNPrice, TT.FTRawMatName, max(TT.FNPriceSale) As FNPriceSale , TT.FNChargeService, TT.FNChargeClear, max(TT.FNNetPrice) As FNNetPrice, TT.FNChargeServicePer, TT.FNChargeClearPer, "
            _Qry &= vbCrLf & "  TT.FNMerMatType, TT.FNHSysUnitId_Hide, max(TT.FNPriceOrg) As FNPriceOrg , TT.FNHSysUnitIdSale, TT.FNConvRatio, TT.FNHSysMatTypeId, TT.FTRawMatColorNameEN, TT.FNCTN, TT.FNNW, TT.FNGW, TT.FNQBM, "
            _Qry &= vbCrLf & "    TT.FNGrpSeq, TT.FNChargeServiceState, TT.FNChargeClearState, TT.FNChargeServiceInv, TT.FNChargeClearInv"
            _Qry &= vbCrLf & " From ( Select     FTBarcodeNo, '' as FTOrderNo, SUM(FNQuantity) AS FNQuantity, FNHSysCmpId, FNHSysWHId, FTDocumentNo,max(FTFabricFrontSize) AS  FTFabricFrontSize, MAX(FTPurchaseNo) AS FTPurchaseNo, SUM(FNAmount) "
            _Qry &= vbCrLf & " AS FNAmount, FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FTRawMatColorCode, FTRawMatSizeCode, FTRawMatSizeNameTH, FTRawMatSizeNameEN, "
            _Qry &= vbCrLf & "FNHSysUnitId, FTUnitNameTH, FTUnitNameEN, FNPrice, FTRawMatName,max(FNPriceSale) AS  FNPriceSale, FNChargeService, FNChargeClear,max(FNNetPrice) AS  FNNetPrice, FNChargeServicePer, FNChargeClearPer, FNMerMatType, "
            _Qry &= vbCrLf & "FNHSysUnitId_Hide,max(FNPriceOrg) AS  FNPriceOrg, FNHSysUnitIdSale, FNConvRatio, FNHSysMatTypeId, FTRawMatColorNameEN, FNCTN, FNNW, FNGW, FNQBM, FNGrpSeq, FNChargeServiceState, "
            _Qry &= vbCrLf & " FNChargeClearState, FNChargeServiceInv, FNChargeClearInv"
            _Qry &= vbCrLf & " From ( SELECT      '' as FTBarcodeNo, BO.FTOrderNo,  BO.FNQuantity  as FNQuantity, BO.FNHSysCmpId, BO.FNHSysWHId,'' as  FTDocumentNo, BR.FTFabricFrontSize, BR.FTPurchaseNo, CONVERT(numeric(18, 4), "
            _Qry &= vbCrLf & "	          Isnull(B.FNQuantity, BO.FNQuantity) * BR.FNPrice) AS FNAmount, BR.FNHSysRawMatId, MM.FTRawMatCode, MM.FTRawMatNameTH,MM.FTRawMatNameEN , MC.FTRawMatColorCode, "
            _Qry &= vbCrLf & "	    MS.FTRawMatSizeCode, MS.FTRawMatSizeNameTH, MS.FTRawMatSizeNameEN, MU.FTUnitCode as FNHSysUnitId, MU.FTUnitNameTH, MU.FTUnitNameEN"

            _Qry &= vbCrLf & ", Isnull(B.FNPriceSale , Isnull(PO2.FNPrice ,0))  as FNPrice"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", Isnull(MD.FTMainMatNameTH, MM.FTRawMatNameTH) as FTRawMatName"
            Else
                _Qry &= vbCrLf & ", Isnull(MD.FTMainMatNameEN, MM.FTRawMatNameEN) as FTRawMatName"
            End If

            _Qry &= vbCrLf & ", ISNULL(PO2.FNPrice,0)  AS FNPriceSale,Isnull(B.FNChargeService,0) AS  FNChargeService,Isnull(B.FNChargeClear,0) AS  FNChargeClear, PO2.FNPrice  AS  FNNetPrice ,Isnull(B.FNChargeServicePer, 0) AS FNChargeServicePer ,Isnull(B.FNChargeClearPer,0) AS FNChargeClearPer "
            _Qry &= vbCrLf & ",ISNULL(MMM.FNMerMatType,0) AS FNMerMatType "
            _Qry &= vbCrLf & ",BR.FNHSysUnitId as FNHSysUnitId_Hide"
            _Qry &= vbCrLf & ",BR.FNPrice AS FNPriceOrg"
            _Qry &= vbCrLf & ",CASE WHEN MMM.FNMerMatType = 0 THEN  ISNULL((SELECT TOP 1 FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) WHERE FTUnitCode in ('YDS')),0)  ELSE BR.FNHSysUnitId END AS FNHSysUnitIdSale"
            _Qry &= vbCrLf & ", 1.00000 AS FNConvRatio"
            _Qry &= vbCrLf & ",ISNULL(MMM.FNHSysMatTypeId,0) AS FNHSysMatTypeId "
            _Qry &= vbCrLf & ",  MC.FTRawMatColorNameEN as FTRawMatColorNameEN"
            _Qry &= vbCrLf & ",Isnull(B.FNCTN,0) AS FNCTN"
            _Qry &= vbCrLf & ", Isnull(B.FNNW,0) AS FNNW"
            _Qry &= vbCrLf & ",Isnull(B.FNGW,0) AS FNGW"
            _Qry &= vbCrLf & ",0.0000 AS FNQBM,0 AS FNGrpSeq   ,Isnull(B.FNChargeService,-1) AS FNChargeServiceState, Isnull(B.FNChargeClear,-1) AS FNChargeClearState  "
            _Qry &= vbCrLf & " , Isnull(B.FNChargeService,0) AS FNChargeServiceInv  ,Isnull( B.FNChargeClear,0) AS FNChargeClearInv  ,PO2.FNPrice AS FNPOFacPrice"
            _Qry &= vbCrLf & "	FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BR WITH (NOLOCK) ON BO.FTBarcodeNo = BR.FTBarcodeNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS MM WITH (NOLOCK) ON BR.FNHSysRawMatId = MM.FNHSysRawMatId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON MM.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MS WITH (NOLOCK) ON MM.FNHSysRawMatSizeId = MS.FNHSysRawMatSizeId  "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MMM WITH(NOLOCK) ON MM.FTRawMatCode = MMM.FTMainMatCode "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial_Description AS MD WITH(NOLOCK) ON MMM.FNHSysMainMatId = MD.FNHSysMainMatId"
            _Qry &= vbCrLf & "LEFT OUTER JOIN (Select * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPriceMatType  WITH(NOLOCK) WHERE FNInvoiceState='2' AND FTStateActive = '1' ) AS MMP  ON MMM.FNHSysMainMatId = MMP.FNHSysMainMatId "
            _Qry &= vbCrLf & "LEFT OUTER JOIN "
            _Qry &= vbCrLf & " ( SELECT FNGrpSeq ,FNHSysRawMatId, FNPrice, FNQuantity, FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice, FNHSysUnitId,  FNCTN, FNNW, FNGW, FNQBM , Isnull(FNChargeServicePer,0) AS FNChargeServicePer , Isnull(FNChargeClearPer,0) AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "' "
            _Qry &= vbCrLf & " ) AS B ON BR.FNHSysRawMatId = B.FNHSysRawMatId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS MU WITH (NOLOCK) ON Isnull(B.FNHSysUnitId,BR.FNHSysUnitId) = MU.FNHSysUnitId"

            _Qry &= vbCrLf & "   OUTER APPLY ( SELECT TOP 1  PF.FNPrice  "
            _Qry &= vbCrLf & "   FROM   "
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS PF WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE  (PF.FTOrderNo =BO.FTOrderNo) "
            _Qry &= vbCrLf & "         And (PF.FNHSysRawMatId =  BR.FNHSysRawMatId) "
            _Qry &= vbCrLf & "         And (PF.FTFacPurchaseNo  ='" & HI.UL.ULF.rpQuoted(pofacno) & "') "
            _Qry &= vbCrLf & "  ) AS PO2  "

            _Qry &= vbCrLf & "WHERE BO.FTDocumentNo ='" & HI.UL.ULF.rpQuoted(TransferWHno) & "' ) AS T "
            _Qry &= vbCrLf & "GROUP BY FTBarcodeNo,   FNHSysCmpId, FNHSysWHId, FTDocumentNo,  FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FTRawMatColorCode, " 'FTFabricFrontSize,
            _Qry &= vbCrLf & " FTRawMatSizeCode, FTRawMatSizeNameTH, FTRawMatSizeNameEN, FNHSysUnitId, FTUnitNameTH, FTUnitNameEN, FNPrice, FTRawMatName,  FNChargeService, FNChargeClear, "
            _Qry &= vbCrLf & "  FNChargeServicePer, FNChargeClearPer, FNMerMatType, FNHSysUnitId_Hide,  FNHSysUnitIdSale, FNConvRatio, FNHSysMatTypeId, FTRawMatColorNameEN, FNCTN, "
            _Qry &= vbCrLf & " FNNW, FNGW, FNQBM, FNGrpSeq, FNChargeServiceState, FNChargeClearState, FNChargeServiceInv, FNChargeClearInv ) AS TT "
            _Qry &= vbCrLf & " LEFT OUTER JOIN "
            _Qry &= vbCrLf & "   (SELECT     FNGrpSeq, FNHSysRawMatId, FNPrice, FNQuantity, FNPriceSale, FNChargeService, FNChargeClear, FNNetPrice, FNHSysUnitId, FNCTN, FNNW, FNGW, FNQBM, "
            _Qry &= vbCrLf & "  ISNULL(FNChargeServicePer, 0) AS FNChargeServicePer, ISNULL(FNChargeClearPer, 0) AS FNChargeClearPer"
            _Qry &= vbCrLf & " FROM           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTSaleInvoice_Detail AS A WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE      (FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Invoice) & "')) AS B_1 ON TT.FNHSysRawMatId = B_1.FNHSysRawMatId"
            '20160107 Group Rawmat not validate OrderNO, PurchaseNo
            _Qry &= vbCrLf & "Group by      TT.FTBarcodeNo, TT.FTOrderNo,  TT.FNHSysCmpId, TT.FNHSysWHId, TT.FTDocumentNo, TT.FTPurchaseNo,   TT.FNHSysRawMatId, " ', TT.FTFabricFrontSize

            _Qry &= vbCrLf & "TT.FTRawMatCode, TT.FTRawMatNameTH, TT.FTRawMatNameEN, TT.FTRawMatColorCode, TT.FTRawMatSizeCode, TT.FTRawMatSizeNameTH, TT.FTRawMatSizeNameEN, TT.FNHSysUnitId,"
            _Qry &= vbCrLf & " TT.FTUnitNameTH, TT.FTUnitNameEN, TT.FNPrice, TT.FTRawMatName,  TT.FNChargeService, TT.FNChargeClear,  TT.FNChargeServicePer, TT.FNChargeClearPer,"
            _Qry &= vbCrLf & " TT.FNMerMatType, TT.FNHSysUnitId_Hide,  TT.FNHSysUnitIdSale, TT.FNConvRatio, TT.FNHSysMatTypeId, TT.FTRawMatColorNameEN, TT.FNCTN, TT.FNNW, TT.FNGW, TT.FNQBM,"
            _Qry &= vbCrLf & " TT.FNGrpSeq, TT.FNChargeServiceState, TT.FNChargeClearState, TT.FNChargeServiceInv, TT.FNChargeClearInv ) AS A "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return ""
            End If

            _Qry = " update O set O.FNPriceTrans =  D.FNPriceSale    "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_DocRef AS R WITH (NOLOCK) ON I.FTInvoiceNo = R.FTInvoiceNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENBarcode_OUT AS O WITH (NOLOCK) ON R.FTDocRefNo = O.FTDocumentNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeNo "
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS D WITH(NOLOCK) ON I.FTInvoiceNo = D.FTInvoiceNo and B.FNHSysRawMatId = D.FNHSysRawMatId"
            _Qry &= vbCrLf & "WHERE        (I.FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(Invoice) & "')  AND O.FNPriceTrans <>  D.FNPriceSale "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                'Return ""
            End If

            Dim cmdstring As String = ""
            Dim poamten As String = ""
            Dim poamtth As String = ""
            Dim poamt As Double = 0
            Dim ponetamt As Double = 0
            Dim podisamt As Double = 0
            Dim povatamt As Double = 0
            Dim Surcharge As Double = 0
            Dim pograndamt As Double = 0

            cmdstring = "   SELECT        SUM(Convert(numeric(18,2),FNQuantity * FNNetPrice)) AS FNAmount"
            cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice_Detail AS A  WITH(NOLOCK)"
            cmdstring &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "' "

            poamt = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT, "0"))
            ponetamt = poamt - podisamt
            povatamt = CDbl(Format((ponetamt * Vat) / 100.0, "0.00"))
            pograndamt = ponetamt + povatamt + Surcharge

            poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
            poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

            cmdstring = "UPDATE A Set "
            cmdstring &= vbCrLf & "  FNInvAmt=" & poamt & ""
            cmdstring &= vbCrLf & ", FNInvNetAmt=" & ponetamt & ""
            cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
            cmdstring &= vbCrLf & ", FNInvGrandAmt=" & pograndamt & ""
            cmdstring &= vbCrLf & ", FTInvGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
            cmdstring &= vbCrLf & ", FTInvGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"
            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSaleInvoice As A "
            cmdstring &= vbCrLf & " WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "' "

            If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return False
            End If

            cmdstring = "UPDATE A Set "
            cmdstring &= vbCrLf & "  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
            cmdstring &= vbCrLf & ", FTDocumentState='1'"
            cmdstring &= vbCrLf & " ,FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Invoice) & "'"
            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A "
            cmdstring &= vbCrLf & "WHERE  A.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(TransferWHno) & "'"

            If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                Return False
            End If

            Return Invoice

        Catch ex As Exception
            Return ""
        End Try

        Return ""
    End Function

End Class

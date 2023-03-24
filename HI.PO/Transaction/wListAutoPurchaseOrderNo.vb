Public Class wListAutoPurchaseOrderNo 

    Private _TmpDataPO As DataTable
    Public Property DataPO As DataTable
        Get
            Return _TmpDataPO
        End Get
        Set(value As DataTable)
            _TmpDataPO = value
        End Set
    End Property

    Public Sub RefreshDataPO()

        ogclist.DataSource = DataPO
        ogclist.RefreshDataSource()

    End Sub
 
    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsendapprove_Click(sender As System.Object, e As System.EventArgs) Handles ocmsendapprove.Click
        Try
            With ogvlist
                .FocusedRowHandle = 0
                .FocusedColumn = .Columns.ColumnByName("FTSelect")
            End With
            CType(Me.ogclist.DataSource, DataTable).AcceptChanges()
            If CType(Me.ogclist.DataSource, DataTable).Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mProcessError(1403040001, "กรุณาทำการเลือกข้อมูล !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim _Str As String = ""
            For Each R As DataRow In CType(Me.ogclist.DataSource, DataTable).Select("FTSelect='1'")

                _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                _Str &= vbCrLf & " SET FTStateSendApp='1'"
                _Str &= vbCrLf & ", FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & ", FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & ", FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' AND ISNULL(FTStateSendApp,'')<>'1' "

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                ' Call CreatePOFactory(R!FTPurchaseNo.ToString)
            Next

            HI.MG.ShowMsg.mInfo("Send Approve Complete ", 1403040002, Me.Text)
        Catch ex As Exception

        End Try



    End Sub

    Private Sub CreatePOFactory(pokey As String)
        Try
            Dim pofacno As String = ""
            Dim cmdstring = ""
            Dim FNHSysCmpIdTo As Integer = 0
            Dim FNHSysCmpId As Integer = 0
            Dim FNExchangeRate As Double = 1
            Dim dt As DataTable

            cmdstring = "SELECT TOP 1 FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt,FNHSysDeliveryId,FNExchangeRate "
            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A "
            cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

            Dim dtpo As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

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

            For Each R As DataRow In dtpo.Rows

                vatper = Val(R!FNVatPer.ToString)
                podisamt = CDbl(Format(Val(R!FNDisCountAmt.ToString) * Val(R!FNExchangeRate.ToString), "0.00")) ' Val(R!FNDisCountAmt.ToString)
                Surcharge = CDbl(Format(Val(R!FNSurcharge.ToString) * Val(R!FNExchangeRate.ToString), "0.00"))  'Val(R!FNSurcharge.ToString)
                FNHSysDeliveryId = Val(R!FNHSysDeliveryId.ToString)
                FNExchangeRate = Val(R!FNExchangeRate.ToString)
            Next

            cmdstring = "SELECT TOP 1 FNHSysCmpId,FNHSysCmpIdTo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery  WITH(NOLOCK)  WHERE FNHSysDeliveryId=" & Val(FNHSysDeliveryId) & " "
            dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            For Each R As DataRow In dt.Rows
                FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
                FNHSysCmpIdTo = Val(R!FNHSysCmpIdTo.ToString)
            Next

            cmdstring = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(pokey) & "'"
            pofacno = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")

            If FNHSysCmpId > 0 And FNHSysCmpIdTo > 0 Then

                If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then

                    SysCurId = 1310190001

                    Dim _Qry As String = ""

                    _Qry = " SELECT TOP 1 FNBuyingRate"
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                    _Qry &= vbCrLf & "   WHERE  (FDDate = Convert(varchar(10),Getdate(),111))"
                    _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                    _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE FTCurCode='USD'"
                    _Qry &= vbCrLf & "  ))"

                    ExcRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "1")))

                End If

                If pofacno = "" Then

                    pofacno = "F" & pokey

                    cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
                    cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
                    cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & " SELECT  FTInsUser, FDInsDate, FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
                    cmdstring &= vbCrLf & "     FNHSysCmpRunId," & FNHSysCmpId & " AS  FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId," & SysCurId & " AS FNHSysCurId," & ExcRate & " AS  FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,  "
                    cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(pokey) & "' AS FTPurchaseNoRef," & FNHSysCmpIdTo & " AS  FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                Else

                    cmdstring = "UPDATE A Set "
                    cmdstring &= vbCrLf & "  FTInsUser=B.FTInsUser"
                    cmdstring &= vbCrLf & ", FDInsDate=B.FDInsDate"
                    cmdstring &= vbCrLf & ", FTInsTime=B.FTInsTime"
                    cmdstring &= vbCrLf & ",  FTRefer=B.FTRefer"
                    cmdstring &= vbCrLf & ", FNPoState=B.FNPoState"
                    cmdstring &= vbCrLf & ", FNHSysPurGrpId=B.FNHSysPurGrpId"
                    cmdstring &= vbCrLf & ", FNHSysCmpRunId=B.FNHSysCmpRunId"
                    cmdstring &= vbCrLf & ", FNHSysCmpId=B.FNHSysCmpId"
                    cmdstring &= vbCrLf & ", FDDeliveryDate=B.FDDeliveryDate"
                    cmdstring &= vbCrLf & ", FNHSysCrTermId=B.FNHSysCrTermId"
                    cmdstring &= vbCrLf & ", FNCreditDay=B.FNCreditDay"
                    cmdstring &= vbCrLf & ", FNHSysTermOfPMId=B.FNHSysTermOfPMId"
                    cmdstring &= vbCrLf & ", FNHSysCurId=" & SysCurId & ""
                    cmdstring &= vbCrLf & ", FNExchangeRate=" & ExcRate & ""
                    cmdstring &= vbCrLf & ", FNHSysDeliveryId=B.FNHSysDeliveryId"
                    cmdstring &= vbCrLf & ", FTContactPerson=B.FTContactPerson"
                    cmdstring &= vbCrLf & ", FTRemark=B.FTRemark"
                    cmdstring &= vbCrLf & ", FNPoAmt=B.FNPoAmt"
                    cmdstring &= vbCrLf & ", FNDisCountPer=B.FNDisCountPer"
                    cmdstring &= vbCrLf & ", FNDisCountAmt=B.FNDisCountAmt"
                    cmdstring &= vbCrLf & ", FNPONetAmt=B.FNPONetAmt"
                    cmdstring &= vbCrLf & ", FNVatPer=B.FNVatPer"
                    cmdstring &= vbCrLf & ", FNVatAmt=B.FNVatAmt"
                    cmdstring &= vbCrLf & ", FNSurcharge=B.FNSurcharge"
                    cmdstring &= vbCrLf & ", FNPOGrandAmt=B.FNPOGrandAmt"
                    cmdstring &= vbCrLf & ", FTPOGrandAmtTH=B.FTPOGrandAmtTH"
                    cmdstring &= vbCrLf & ", FTPOGrandAmtEN=B.FTPOGrandAmtEN"
                    cmdstring &= vbCrLf & ", FTStateSendApp=B.FTStateSendApp"
                    cmdstring &= vbCrLf & ", FTSendAppBy=B.FTSendAppBy"
                    cmdstring &= vbCrLf & ", FTSendAppDate=B.FTSendAppDate"
                    cmdstring &= vbCrLf & ", FTSendAppTime=B.FTSendAppTime"
                    cmdstring &= vbCrLf & ", FNPoType=B.FNPoType"
                    cmdstring &= vbCrLf & ", FNHSysCmpIdCreate=" & FNHSysCmpIdTo & ""
                    cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As B"
                    cmdstring &= vbCrLf & "  WHERE A.FTPurchaseNoRef = B.FTPurchaseNo"
                    cmdstring &= vbCrLf & "  And A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                End If

                cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo ("
                cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo,  FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRemark, "
                cmdstring &= vbCrLf & "    FTFabricFrontSize, FNReservePOQuantity, FTRawMatColorNameTH, FTRawMatColorNameEN, FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate"
                cmdstring &= vbCrLf & ")"
                cmdstring &= vbCrLf & " Select  A.FTInsUser, A.FDInsDate, A.FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo"
                cmdstring &= vbCrLf & ",  A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysUnitId"
                cmdstring &= vbCrLf & ", (A.FNPrice + Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))) AS FNPrice , A.FNDisPer, A.FNDisAmt, A.FNQuantity, A.FNNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity)) AS FNNetAmt"
                cmdstring &= vbCrLf & ", A.FTRemark, "
                cmdstring &= vbCrLf & "  A.FTFabricFrontSize, A.FNReservePOQuantity, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN, A.FNSurchangeAmt, A.FNSurchangePerUnit, A.FNGrandNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,2),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity))  AS FNGrandNetAmt, A.FTOGacDate"
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A "

                'cmdstring &= vbCrLf & "  INNER Join "
                'cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
                'cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
                'cmdstring &= vbCrLf & " (SELECT  *  "
                'cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
                'cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & FNHSysCmpIdTo & ""
                'cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType "

                cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  ON A.FTPurchaseNo=H.FTPurchaseNo"
                cmdstring &= vbCrLf & "  INNER Join "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
                cmdstring &= vbCrLf & " (SELECT  *  "
                cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
                cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & FNHSysCmpIdTo & ""
                cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType AND H.FNPoState=CH.FNPoType"
                cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                cmdstring = " UPDATE B SET "
                cmdstring &= vbCrLf & "B.FNPrice = Convert(numeric(18,4),(B.FNPrice *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNDisAmt = Convert(numeric(18,4),(B.FNDisAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & " , B.FNNetAmt = Convert(numeric(18,4),(B.FNNetAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNSurchangeAmt = Convert(numeric(18,4),(B.FNSurchangeAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNSurchangePerUnit = Convert(numeric(18,4),(B.FNSurchangePerUnit *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNGrandNetAmt = Convert(numeric(18,2),(B.FNGrandNetAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A INNER Join"
                cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As B On A.FTFacPurchaseNo = B.FTFacPurchaseNo INNER Join"
                cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As C On A.FTPurchaseNoRef = C.FTPurchaseNo "
                cmdstring &= vbCrLf & " WHERE A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                podisamt = CDbl(Format(podisamt / ExcRate, "0.00")) ' Val(R!FNDisCountAmt.ToString)
                Surcharge = CDbl(Format(Surcharge / ExcRate, "0.00"))  'Val(R!FNSurcharge.ToString)

                cmdstring = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                cmdstring &= vbCrLf & "    FROM"
                cmdstring &= vbCrLf & " ("
                cmdstring &= vbCrLf & " SELECT        FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  WITH(NOLOCK)"
                cmdstring &= vbCrLf & " WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "' "
                cmdstring &= vbCrLf & " GROUP BY FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                poamt = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
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

                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
                cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            Else

                If pofacno <> "" Then

                    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""
                    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                End If

            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ogclist_Click(sender As System.Object, e As System.EventArgs) Handles ogclist.Click

    End Sub

    Private Sub ogvlist_DoubleClick(sender As Object, e As System.EventArgs) Handles ogvlist.DoubleClick

        Try
            With ogvlist
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _PurchaseNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                Dim _WformPo As New HI.PO.wPurchaseOrder

                With _WformPo
                    .ocmexit.Visible = False
                    .ocmclear.Visible = False
                    .FTPurchaseNo.Properties.ReadOnly = True
                    .FTPurchaseNo.Properties.Buttons(0).Enabled = False
                    .FTPurchaseNo.Properties.Buttons(1).Enabled = False
                End With

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "MnuManualPurchase"
                Dim _WShow As New HI.TLF.wShowData(_WformPo, _PurchaseNo)
                HI.ST.SysInfo.MenuName = _TmpMenu

                With _WShow
                    .WindowState = System.Windows.Forms.FormWindowState.Maximized
                    .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    .ShowDialog()
                End With

            End With
        Catch ex As Exception
            MsgBox(ex.Message())
        End Try


    End Sub

End Class
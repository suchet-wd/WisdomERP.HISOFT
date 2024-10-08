﻿Public Class wImportReceiveFormatNanyang
    Private _lstRcv As wImportReceiveNo
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim oSysLang As New ST.SysLanguage
        _lstRcv = New wImportReceiveNo

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _lstRcv.Name.ToString.Trim, _lstRcv)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_lstRcv)
        HI.TL.HandlerControl.AddHandlerObj(_lstRcv)
    End Sub

#Region "Property"
    Private _dtdataheader As DataTable = Nothing
    Private Property dataheader As DataTable
        Get
            Return _dtdataheader
        End Get
        Set(value As DataTable)
            _dtdataheader = value
        End Set
    End Property

    Private _dtdatadetail As DataTable = Nothing
    Private Property datadetail As DataTable
        Get
            Return _dtdatadetail
        End Get
        Set(value As DataTable)
            _dtdatadetail = value
        End Set
    End Property

#End Region
#Region "Procedure"

    Private Function ValidateData() As Boolean
        Dim _pass As Boolean = False
        If Not (Me.ogcdetail.DataSource Is Nothing) Then
            If CType(Me.ogcdetail.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                If Me.FNHSysWHId.Text <> "" And Me.FNHSysWHId.Properties.Tag.ToString <> "" Then
                    _pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysWHId_lbl.Text)
                    FNHSysWHId.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูล ที่ต้องการทำการ Import !!!", 1409160003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข่้อมูลกรุณาทำการเลือก File ที่ต้องการทำการ Import !!!", 1409160001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

        End If

        Return _pass
    End Function


    Private Function Import(ByRef dtreceive As DataTable) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Importing Receive Detail...   Please Wait   ")
        Dim dt As New DataTable
        Dim dtheader As New DataTable
        Dim dtdetail As New DataTable
        Dim dttmppo As New DataTable
        Try

            Dim _PurchaseNo As String
            Dim _InvoiceNo As String
            Dim _InvoiceDate As String
            Dim _Str As String
            Dim FTReceiveNo As String = ""
            Dim _CmpH As String = ""
            Dim _FTMaterialCode As String
            Dim _FTMaterialColorCode As String
            Dim _FTMaterialSizeCode As String
            Dim _FTUnitCode As String
            Dim _FQuantity As Double
            Dim _FNHSysRawMatId As Integer = 0
            Dim _FNHSysUnitId As Integer = 0
            Dim _FTFabricFrontSize As String = ""

            Dim _FNPrice As Double
            Dim _FNDisPer As Double
            Dim _FNDisAmt As Double
            Dim _FNNetPrice As Double

            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
                dtheader = .Copy

            End With
            dtdetail = Me.datadetail.Copy


            For Each R As DataRow In dt.Select("FTSelect='1'")
                _InvoiceNo = R!FTInvoiceNo.ToString
                _PurchaseNo = R!FTPurchaseNo.ToString.Trim
                _InvoiceDate = R!FTInvoiceDate.ToString.Trim




                If dtheader.Select("FTSelect='1' AND FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PurchaseNo) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "'").Length > 0 Then

                    For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                        Select Case HI.ENM.Control.GeTypeControl(ctrl)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                End With

                                Exit For
                            Case ENM.Control.ControlType.TextEdit
                                With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                End With

                                Exit For
                        End Select

                    Next

                    FTReceiveNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN), "TINVENReceive", "", False, _CmpH).ToString

                    _Str = "  INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive"
                    _Str &= vbCrLf & "   ( FTInsUser, FDInsDate, FTInsTime, FTReceiveNo, FDReceiveDate, FTReceiveBy, FTPurchaseNo"
                    _Str &= vbCrLf & " , FNHSysWHId, FNExchangeRate, FTInvoiceNo, FDInvoiceDate, FTRemark, FNRceceiveType, FNHSysCmpId, "
                    _Str &= vbCrLf & "    FTStateImport, FTImportBy, FTImportDate, FTImportTime)"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_PurchaseNo) & "' "
                    _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysWHId.Properties.Tag.ToString)) & " "
                    _Str &= vbCrLf & "," & Double.Parse(1) & " "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(_InvoiceDate) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTRemark.Text) & "' "
                    _Str &= vbCrLf & ",0 "
                    _Str &= vbCrLf & "," & Integer.Parse(HI.ST.SysInfo.CmpID) & " "
                    _Str &= vbCrLf & ",'1' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_INVEN) = True Then
                        dtreceive.Rows.Add(FTReceiveNo, _PurchaseNo, _InvoiceNo)

                        For Each Rx As DataRow In dtheader.Select("FTSelect='1' AND FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PurchaseNo) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "'")

                            _FTUnitCode = Rx!FTUnitCode.ToString.Trim
                            _FTMaterialCode = Rx!FTMaterialCode.ToString.Trim
                            _FTMaterialColorCode = Rx!FTMaterialColorCode.ToString.Trim
                            _FTMaterialSizeCode = ""
                            _FQuantity = Double.Parse(Rx!FQuantity)
                            _FNHSysUnitId = 0
                            _FNHSysRawMatId = 0
                            _FTFabricFrontSize = ""
                            _FNPrice = 0
                            _FNDisPer = 0
                            _FNDisAmt = 0
                            _FNNetPrice = 0

                            _Str = " SELECT TOP 1   P.FNHSysRawMatId,"
                            _Str &= vbCrLf & "  IM.FTRawMatCode ,"
                            _Str &= vbCrLf & "  C.FTRawMatColorCode,"
                            _Str &= vbCrLf & "  S.FTRawMatSizeCode,"
                            _Str &= vbCrLf & "  ISNULL(P.FTFabricFrontSize,'') AS FTFabricFrontSize ,"
                            _Str &= vbCrLf & "  P.FNHSysUnitId,"
                            _Str &= vbCrLf & "  U.FTUnitCode ,"
                            _Str &= vbCrLf & "  P.FNPrice,"
                            _Str &= vbCrLf & "  P.FNDisPer,"
                            _Str &= vbCrLf & "  P.FNDisAmt,"
                            _Str &= vbCrLf & "  (P.FNPrice- P.FNDisAmt ) AS FNNetPrice,"
                            _Str &= vbCrLf & "   P.FNQuantity"
                            _Str &= vbCrLf & "   FROM "
                            _Str &= vbCrLf & " (SELECT        FTPurchaseNo, FNHSysRawMatId, FNHSysUnitId,FTFabricFrontSize"
                            _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt"
                            _Str &= vbCrLf & " , SUM(FNQuantity) AS FNQuantity"
                            _Str &= vbCrLf & "  ,'' As FTStateRcvOver"
                            _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH (NOLOCK)"
                            _Str &= vbCrLf & " WHERE        (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(_PurchaseNo) & "')"
                            _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNHSysUnitId,FTFabricFrontSize, FNPrice, FNDisPer, FNDisAmt) AS P "
                            _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial as IM WITH(NOLOCK ) ON P.FNHSysRawMatId = IM.FNHSysRawMatId "
                            _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit as U WITH(NOLOCK) ON P.FNHSysUnitId = U.FNHSysUnitId "
                            _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
                            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
                            _Str &= vbCrLf & "  WHERE IM.FTRawMatCode='" & HI.UL.ULF.rpQuoted(_FTMaterialCode) & "' "
                            _Str &= vbCrLf & "  AND   ISNULL(C.FTRawMatColorCode,'')='" & HI.UL.ULF.rpQuoted(_FTMaterialColorCode) & "' "
                            _Str &= vbCrLf & "  AND   ISNULL(S.FTRawMatSizeCode,'')='" & HI.UL.ULF.rpQuoted(_FTMaterialSizeCode) & "' "
                            _Str &= vbCrLf & " ORDER BY   IM.FTRawMatCode ,  C.FTRawMatColorCode, S.FTRawMatSizeCode"

                            dttmppo = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)


                            For Each RxI As DataRow In dttmppo.Rows
                                _FNHSysRawMatId = Integer.Parse(RxI!FNHSysRawMatId)
                                _FNHSysUnitId = Integer.Parse(RxI!FNHSysUnitId)
                                _FTFabricFrontSize = RxI!FTFabricFrontSize.ToString
                                _FNPrice = Val(RxI!FNPrice.ToString)
                                _FNDisPer = Val(RxI!FNDisPer.ToString)
                                _FNDisAmt = Val(RxI!FNDisAmt.ToString)
                                _FNNetPrice = Val(RxI!FNNetPrice.ToString)


                            Next

                            If _FNHSysRawMatId > 0 Then

                                Try
                                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                                    HI.Conn.SQLConn.SqlConnectionOpen()
                                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                                    _Spls.UpdateInformation("Receive Detail....   Please Wait    ")

                                    _Str = "   SELECT   TOP 1    FTReceiveNo, FNHSysRawMatId, FNHSysUnitId"
                                    _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt, FNNetPrice,FNNetAmt, FNQuantity"
                                    _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail "
                                    _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                    _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(_FNHSysRawMatId) & " "

                                    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                                        _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail ( FTInsUser, FDInsDate, FTInsTime,FTReceiveNo, FNHSysRawMatId,FNHSysUnitId,FTFabricFrontSize,  FNPrice, FNDisPer, FNDisAmt,FNNetPrice,FNNetAmt, FNQuantity) "
                                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                        _Str &= vbCrLf & "," & Val(_FNHSysRawMatId) & " "
                                        _Str &= vbCrLf & "," & Val(_FNHSysUnitId) & ""
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTFabricFrontSize) & "' "
                                        _Str &= vbCrLf & "," & Val(_FNPrice) & " "
                                        _Str &= vbCrLf & "," & Val(_FNDisPer) & " "
                                        _Str &= vbCrLf & "," & Val(_FNDisAmt) & " "
                                        _Str &= vbCrLf & "," & Val(_FNNetPrice) & " "
                                        _Str &= vbCrLf & "," & CDbl(Format(Val(_FQuantity) * Val(_FNNetPrice), HI.ST.Config.AmtFormat)) & " "
                                        _Str &= vbCrLf & "," & Val(_FQuantity) & " "

                                    Else

                                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
                                        _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                        _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                        _Str &= vbCrLf & ",FNHSysUnitId=" & Val(_FNHSysUnitId) & " "
                                        _Str &= vbCrLf & ", FNPrice=" & Val(_FNPrice) & " "
                                        _Str &= vbCrLf & ", FNDisPer=" & Val(_FNDisPer) & " "
                                        _Str &= vbCrLf & ", FNDisAmt=" & Val(_FNDisAmt) & " "
                                        _Str &= vbCrLf & ", FNNetPrice=" & Val(_FNNetPrice) & " "
                                        _Str &= vbCrLf & ", FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(_FTFabricFrontSize) & "' "
                                        _Str &= vbCrLf & ",FNNetAmt=" & CDbl(Format(Val(_FQuantity) * Val(_FNNetPrice), HI.ST.Config.AmtFormat)) & " "
                                        _Str &= vbCrLf & ", FNQuantity=" & Val(_FQuantity) & " "
                                        _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                        _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(_FNHSysRawMatId) & " "

                                    End If

                                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                    End If

                                    _Spls.UpdateInformation("Equalize Job....   Please Wait    ")

                                    If StockValidate.EqualizeJob(FTReceiveNo, _PurchaseNo, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran, _FNHSysRawMatId) = False Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                    End If




                                    HI.Conn.SQLConn.Tran.Commit()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                Catch ex As Exception

                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                End Try

                                Dim _BatcodeQty As Double
                                Dim _FTBarcodeNo As String = ""
                                Dim _dtGenBar As DataTable
                                Dim _ConQty As Boolean = False
                                Dim _GenQtyBar As Double = 0
                                Dim _BarSeq As Integer = 0
                                Dim _Barcode As String = ""
                                Dim QtyBal As Double
                                Dim OrderQty As Double

                                For Each Rx3 As DataRow In dtdetail.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PurchaseNo) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "'  AND FTMaterialCode='" & HI.UL.ULF.rpQuoted(_FTMaterialCode) & "'  AND FTMaterialColorCode='" & HI.UL.ULF.rpQuoted(_FTMaterialColorCode) & "' ")
                                    _FTBarcodeNo = (Rx3!FTBarcodeNo.ToString)
                                    _BatcodeQty = Double.Parse(Rx3!FQuantity)
                                    _ConQty = False
                                    _BarSeq = 0
                                    _GenQtyBar = _BatcodeQty
                                    QtyBal = _BatcodeQty
                                    OrderQty = 0
                                    '_Str = " SELECT FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTPurchaseNo,FTOrderNo,FNHSysRawMatId,FNHSysUnitId"
                                    '_Str &= vbCrLf & " 	,FNHSysWHId,FNQuantityStock,FNPricePerStock,FTUnitCode,FNBarCodeQty"
                                    '_Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNBarcodeBalance"
                                    '_Str &= vbCrLf & "	,1 AS FNQtyBarcode"
                                    '_Str &= vbCrLf & "	,'' AS FTBatchNo"
                                    '_Str &= vbCrLf & "	,'' AS FTGrade,FNConvRatio"
                                    '_Str &= vbCrLf & "  FROM   ("
                                    '_Str &= vbCrLf & "  Select IM.FTRawMatCode"
                                    '_Str &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
                                    '_Str &= vbCrLf & "  ,ISNULL( S.FTRawMatSizeCode ,'') AS FTRawMatSizeCode"
                                    '_Str &= vbCrLf & "  ,A.FTFabricFrontSize"
                                    '_Str &= vbCrLf & "  ,A.FTPurchaseNo"
                                    '_Str &= vbCrLf & "  , A.FTOrderNo"
                                    '_Str &= vbCrLf & "  , A.FNHSysRawMatId"
                                    '_Str &= vbCrLf & " 	, A.FNHSysWHId"
                                    '_Str &= vbCrLf & " 	, A.FNQuantityStock"
                                    '_Str &= vbCrLf & " 	, A.FNHSysUnitIdStock AS FNHSysUnitId"
                                    '_Str &= vbCrLf & "  , A.FNPricePerStock"
                                    '_Str &= vbCrLf & "  , U.FTUnitCode,A.FNConvRatio"
                                    '_Str &= vbCrLf & " 	 ,ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
                                    '_Str &= vbCrLf & " 		  FROM TINVENBarcode AS B WITH(NOLOCK)"
                                    '_Str &= vbCrLf & " WHERE FTDocumentNo = A.FTDocumentNo "
                                    '_Str &= vbCrLf & " 		AND  FNHSysRawMatId = A.FNHSysRawMatId"
                                    '_Str &= vbCrLf & " 		AND  FTOrderNo = A.FTOrderNo"
                                    '_Str &= vbCrLf & " 		AND  FTPurchaseNo = A.FTPurchaseNo"
                                    '_Str &= vbCrLf & "  	 ),0) AS FNBarCodeQty"
                                    '_Str &= vbCrLf & "  FROM           ( "
                                    '_Str &= vbCrLf & "  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                                    '_Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                                    '_Str &= vbCrLf & "  , A.FNHSysRawMatId"
                                    '_Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                                    '_Str &= vbCrLf & " 	  , A.FNHSysUnitIdStock"
                                    '_Str &= vbCrLf & " 	, A.FNPricePerStock"
                                    '_Str &= vbCrLf & " 	,A.FNQuantityStock,A.FNConvRatio"
                                    '_Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
                                    '_Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo  "
                                    '_Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "'"
                                    '_Str &= vbCrLf & "  AND    A.FNHSysRawMatId=" & Integer.Parse(_FNHSysRawMatId) & " "
                                    '_Str &= vbCrLf & "  ) AS A"
                                    '_Str &= vbCrLf & "   INNER Join"
                                    '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
                                    '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitIdStock = U.FNHSysUnitId LEFT OUTER JOIN"
                                    '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
                                    '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
                                    '_Str &= vbCrLf & "  ) AS M"
                                    '_Str &= vbCrLf & " WHERE (FNQuantityStock-FNBarCodeQty) >0"
                                    '_Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTOrderNo"

                                    _Str = " SELECT FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTPurchaseNo,FTOrderNo,FNHSysRawMatId,FNHSysUnitId"
                                    _Str &= vbCrLf & " 	,FNHSysWHId,FNQuantityStock,FNPricePerStock,FTUnitCode,FNBarCodeQty"
                                    _Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNBarcodeBalance"
                                    _Str &= vbCrLf & "	,1 AS FNQtyBarcode"
                                    _Str &= vbCrLf & "	,'' AS FTBatchNo"
                                    _Str &= vbCrLf & "	,'' AS FTGrade,FNConvRatio"
                                    _Str &= vbCrLf & "  FROM   ("
                                    _Str &= vbCrLf & "  Select A.FNSortData ,IM.FTRawMatCode"
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
                                    _Str &= vbCrLf & "  , U.FTUnitCode,A.FNConvRatio"
                                    _Str &= vbCrLf & " 	 ,ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
                                    _Str &= vbCrLf & " 		  FROM TINVENBarcode AS B WITH(NOLOCK)"
                                    _Str &= vbCrLf & " WHERE FTDocumentNo = A.FTDocumentNo "
                                    _Str &= vbCrLf & " 		AND  FNHSysRawMatId = A.FNHSysRawMatId"
                                    _Str &= vbCrLf & " 		AND  FTOrderNo = A.FTOrderNo"
                                    _Str &= vbCrLf & " 		AND  FTPurchaseNo = A.FTPurchaseNo"
                                    _Str &= vbCrLf & "  	 ),0) AS FNBarCodeQty"
                                    _Str &= vbCrLf & "  FROM           ( "
                                    _Str &= vbCrLf & "  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
                                    _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
                                    _Str &= vbCrLf & "  , A.FNHSysRawMatId"
                                    _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                                    _Str &= vbCrLf & " 	  , A.FNHSysUnitIdStock"
                                    _Str &= vbCrLf & " 	, A.FNPricePerStock"
                                    _Str &= vbCrLf & " 	,A.FNQuantityStock,A.FNConvRatio"
                                    _Str &= vbCrLf & "   ,ROW_NUMBER() Over (  ORDER BY  Case When ISNULL(O.FDShipDate,'' ) ='' THEN '9999/99/99' ELSE ISNULL(O.FDShipDate,'' ) END ) AS FNSortData "


                                    _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order As A With (NOLOCK) INNER JOIN"
                                    _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  As  H With(NOLOCK) On A.FTReceiveNo = H.FTReceiveNo  "



                                    _Str &= vbCrLf & " LEFT OUTER JOIN ("
                                    _Str &= vbCrLf & " Select        A.FTOrderNo"
                                    _Str &= vbCrLf & " ,(Case When ISNULL(XX2.FNProcessSortDate,'') ='' THEN ( CASE WHEN A.FNOrderType =6 THEN '0000/00/00' "
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
                                    _Str &= vbCrLf & " ) AS O ON A.FTOrderNo = O.FTOrderNo "


                                    _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "'"
                                    _Str &= vbCrLf & "  AND    A.FNHSysRawMatId=" & Integer.Parse(_FNHSysRawMatId) & " "

                                    _Str &= vbCrLf & "  ) AS A"
                                    _Str &= vbCrLf & "   INNER Join"
                                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
                                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitIdStock = U.FNHSysUnitId LEFT OUTER JOIN"
                                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
                                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
                                    _Str &= vbCrLf & "  ) AS M"
                                    _Str &= vbCrLf & " WHERE (FNQuantityStock-FNBarCodeQty) >0"
                                    _Str &= vbCrLf & "  ORDER BY  FNSortData,FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTOrderNo"

                                    _dtGenBar = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

                                    For Each Rx4 As DataRow In _dtGenBar.Rows
                                        _BarSeq = _BarSeq + 1
                                        If _ConQty = False Then
                                            QtyBal = Double.Parse(Format(_GenQtyBar * Double.Parse(Val(Rx4!FNConvRatio)), HI.ST.Config.QtyFormat))

                                            _ConQty = True
                                        End If
                                        OrderQty = Double.Parse(Val(Rx4!FNBarcodeBalance))

                                        If _BarSeq = 1 Then

                                            _Str = " SELECT TOP 1 FTBarcodeNo "
                                            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode "
                                            _Str &= vbCrLf & " WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "' "

                                            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then
                                                _Barcode = _FTBarcodeNo
                                            Else
                                                _Barcode = HI.Conn.SQLConn.GetField(" EXEC SP_GEN_BARCODE_NO '" & HI.ST.SysInfo.CmpRunID & "' ", Conn.DB.DataBaseName.DB_INVEN, "")
                                            End If

                                        Else
                                            _Barcode = HI.Conn.SQLConn.GetField(" EXEC SP_GEN_BARCODE_NO '" & HI.ST.SysInfo.CmpRunID & "' ", Conn.DB.DataBaseName.DB_INVEN, "")
                                        End If

                                        If QtyBal >= OrderQty Then
                                            _GenQtyBar = OrderQty
                                        Else
                                            _GenQtyBar = QtyBal
                                        End If

                                        If _Barcode <> "" And QtyBal > 0 Then

                                            Try
                                                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                                                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode( FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FNHSysWHId"
                                                _Str &= vbCrLf & ", FNHSysRawMatId, FTOrderNo, FNHSysUnitId, "
                                                _Str &= vbCrLf & "   FNPrice, FNQuantity, FTDocumentNo, FTFabricFrontSize, FTBatchNo, FTGrade, FTPurchaseNo,FNHSysCmpId,FTBarcodeSuplRefNo)"
                                                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Barcode) & "' "
                                                _Str &= vbCrLf & "," & Val(Rx4!FNHSysWHId.ToString) & " "
                                                _Str &= vbCrLf & "," & Val(Rx4!FNHSysRawMatId.ToString) & " "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTOrderNo.ToString) & "' "
                                                _Str &= vbCrLf & "," & Val(Rx4!FNHSysUnitId.ToString) & " "
                                                _Str &= vbCrLf & "," & Val(Rx4!FNPricePerStock.ToString) & " "
                                                _Str &= vbCrLf & "," & _GenQtyBar & " "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTFabricFrontSize.ToString) & "' "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTBatchNo.ToString) & "' "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTGrade.ToString) & "' "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTPurchaseNo.ToString) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
                                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "'"

                                                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                                    Return True
                                                End If

                                                '_Str = " INSERT INTO TINVENBarcode_IN( FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                                                '_Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                '_Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                                '_Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Barcode) & "' "
                                                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "
                                                '_Str &= vbCrLf & "," & Val(R!FNHSysWHId.ToString) & " "
                                                '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                                '_Str &= vbCrLf & "," & _GenQtyBar & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'," & Val(HI.ST.SysInfo.CmpID) & "  "

                                                'If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                '    HI.Conn.SQLConn.Tran.Rollback()
                                                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                                '    Return True
                                                'End If

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

                                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
                                _Str &= vbCrLf & "FNNetAmt=0 "
                                _Str &= vbCrLf & ", FNQuantity=0 "
                                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(_FNHSysRawMatId) & " "
                                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order SET "
                                _Str &= vbCrLf & "FNNetAmt=0 "
                                _Str &= vbCrLf & ", FNQuantity=0 "
                                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(_FNHSysRawMatId) & " "
                                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                            End If


                        Next

                        dtheader.BeginInit()
                        For Each Rx As DataRow In dtheader.Select("FTSelect='1' AND FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PurchaseNo) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "'")
                            Rx.Delete()
                        Next
                        dtheader.EndInit()

                        dtdetail.BeginInit()
                        For Each Rx As DataRow In dtdetail.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PurchaseNo) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_InvoiceNo) & "'")
                            Rx.Delete()
                        Next
                        dtdetail.EndInit()

                    End If

                End If

            Next

        Catch ex As Exception
            _Spls.Close()
        End Try
        dttmppo.Dispose()
        dt.Dispose()
        dtheader.Dispose()
        dtdetail.Dispose()
    End Function

    Private Sub LoadFile(FilePath As String)
        Dim _FileError As Boolean = False
        Dim _Qry As String = ""
        Dim dttemp As DataTable
        Dim _Pls As New HI.TL.SplashScreen("Reading...File Please Wait...")

        Try

            Dim _dtHeader As New DataTable
            With _dtHeader
                .Columns.Add("FTSelect", GetType(String))
                .Columns.Add("FTPurchaseNo", GetType(String))
                .Columns.Add("FTInvoiceNo", GetType(String))
                .Columns.Add("FTInvoiceDate", GetType(String))
                .Columns.Add("FTMaterialCode", GetType(String))
                .Columns.Add("FTMaterialColorCode", GetType(String))
                .Columns.Add("FTUnitCode", GetType(String))
                .Columns.Add("FQuantity", GetType(Double))
                .Columns.Add("FTStateComplete", GetType(String))
            End With

            Dim _dtimport As New DataTable
            With _dtimport
                .Columns.Add("FTBarcodeNo", GetType(String))
                .Columns.Add("FTPurchaseNo", GetType(String))
                .Columns.Add("FTInvoiceNo", GetType(String))
                .Columns.Add("FTInvoiceDate", GetType(String))
                .Columns.Add("FTMaterialCode", GetType(String))
                .Columns.Add("FTMaterialColorCode", GetType(String))
                .Columns.Add("FTUnitCode", GetType(String))
                .Columns.Add("FQuantity", GetType(Double))
            End With


            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"


            Dim _dt As DataTable = HI.UL.ReadExcel.Read(FilePath, "", 0)

            Dim _FTBarcodeNo As String
            Dim _FTPurchaseNo As String
            Dim _FTInvoiceNo As String
            Dim _FTInvoiceDate As String
            Dim _FTMaterialCode As String
            Dim _FTMaterialColorCode As String
            Dim _FTUnitCode As String
            Dim _FQuantity As Double

            ' MsgBox(_dt.Rows.Count.ToString)
            ' MsgBox("1")

            If _dt.Rows.Count > 0 Then

                ' MsgBox("1.5")

                _dt.BeginInit()
                _dt.Rows.RemoveAt(0)
                _dt.EndInit()

            End If
            ' MsgBox("2")

            Try
                For Each R As DataRow In _dt.Rows
                    _FTBarcodeNo = R!F1.ToString.Trim
                    _FTPurchaseNo = R!F4.ToString.Trim
                    _FTInvoiceNo = R!F6.ToString.Trim
                    _FTInvoiceDate = R!F7.ToString.Trim
                    _FTUnitCode = R!F14.ToString.Trim
                    If IsNumeric(R!F17.ToString.Trim) Then

                        _FQuantity = Double.Parse(R!F17.ToString.Trim)
                    Else
                        _FQuantity = 0
                    End If

                    '_FTMaterialCode = R!F22.ToString.Trim
                    '_FTMaterialColorCode = R!F24.ToString.Trim

                    _FTMaterialCode = R!F8.ToString.Trim
                    _FTMaterialColorCode = R!F10.ToString.Trim

                    _Qry = " SELECT  TOP 1  IM.FTRawMatCode"
                    _Qry &= vbCrLf & ", ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode"
                    _Qry &= vbCrLf & ", ISNULL(IMS.FTRawMatSizeCode,'') AS FTRawMatSizeCode "
                    _Qry &= vbCrLf & ", M.FTSuplRawMatCodeRef, M.FTSuplColorCodeRef, M.FTSuplSizeCodeRef"
                    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMaterialMappingSuplRef AS M INNER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM  WITH(NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS  WITH(NOLOCK) ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC  WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
                    _Qry &= vbCrLf & "  WHERE  (M.FTSuplRawMatCodeRef = N'" & HI.UL.ULF.rpQuoted(_FTMaterialCode) & "' )"
                    _Qry &= vbCrLf & "   AND (M.FTSuplColorCodeRef = N'" & HI.UL.ULF.rpQuoted(_FTMaterialColorCode) & "' )"
                    _Qry &= vbCrLf & "  AND (M.FTSuplSizeCodeRef = N'')"
                    dttemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                    _FTMaterialCode = ""
                    _FTMaterialColorCode = ""

                    For Each Rx2 As DataRow In dttemp.Rows
                        _FTMaterialCode = Rx2!FTRawMatCode.ToString
                        _FTMaterialColorCode = Rx2!FTRawMatColorCode.ToString
                        Exit For
                    Next

                    If _dtHeader.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_FTInvoiceNo) & "'  AND FTMaterialCode='" & HI.UL.ULF.rpQuoted(_FTMaterialCode) & "'  AND FTMaterialColorCode='" & HI.UL.ULF.rpQuoted(_FTMaterialColorCode) & "'").Length <= 0 Then
                        _dtHeader.Rows.Add("0", _FTPurchaseNo, _FTInvoiceNo, _FTInvoiceDate, _FTMaterialCode, _FTMaterialColorCode, _FTUnitCode, _FQuantity, "0")
                    Else
                        For Each Rx As DataRow In _dtHeader.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "' AND FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_FTInvoiceNo) & "'  AND FTMaterialCode='" & HI.UL.ULF.rpQuoted(_FTMaterialCode) & "'  AND FTMaterialColorCode='" & HI.UL.ULF.rpQuoted(_FTMaterialColorCode) & "'")
                            Rx!FQuantity = Rx!FQuantity + _FQuantity
                        Next
                    End If
                    _dtimport.Rows.Add(_FTBarcodeNo, _FTPurchaseNo, _FTInvoiceNo, _FTInvoiceDate, _FTMaterialCode, _FTMaterialColorCode, _FTUnitCode, _FQuantity)
                Next
            Catch ex As Exception
                _FileError = True
            End Try
            ' MsgBox("3")

            If (_FileError = False) Then
                dataheader = _dtHeader.Copy
                datadetail = _dtimport.Copy
                'MsgBox("4")

                _dtHeader.Dispose()
                _dtimport.Dispose()

                For Each R As DataRow In dataheader.Rows
                    _FTInvoiceNo = R!FTInvoiceNo.ToString
                    _FTPurchaseNo = R!FTPurchaseNo.ToString.Trim
                    _FTUnitCode = R!FTUnitCode.ToString.Trim
                    _FTMaterialCode = R!FTMaterialCode.ToString.Trim
                    _FTMaterialColorCode = R!FTMaterialColorCode.ToString.Trim


                    _Qry = "  SELECT  TOP 1   P.FTPurchaseNo, IM.FTRawMatCode, IMC.FTRawMatColorCode, IMS.FTRawMatSizeCode"
                    _Qry &= vbCrLf & " , SUM(P.FNQuantity) AS FNQuantity, IMU.FTUnitCode"
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)  ON P.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS IMU WITH(NOLOCK)  ON P.FNHSysUnitId = IMU.FNHSysUnitId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK)  ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK)  ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId"
                    _Qry &= vbCrLf & " WHERE P.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "' "
                    _Qry &= vbCrLf & " AND IM.FTRawMatCode='" & HI.UL.ULF.rpQuoted(_FTMaterialCode) & "' "
                    _Qry &= vbCrLf & " AND ISNULL(IMC.FTRawMatColorCode,'')='" & HI.UL.ULF.rpQuoted(_FTMaterialColorCode) & "' "
                    _Qry &= vbCrLf & " GROUP BY P.FTPurchaseNo, IM.FTRawMatCode, IMC.FTRawMatColorCode, IMS.FTRawMatSizeCode, IMU.FTUnitCode"

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then

                        _Qry = "      SELECT   TOP 1     A.FTReceiveNo, A.FTPurchaseNo, A.FTInvoiceNo, IMC.FTRawMatColorCode"
                        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A INNER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS B ON A.FTReceiveNo = B.FTReceiveNo INNER JOIN"
                        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM ON B.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
                        _Qry &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "' "
                        _Qry &= vbCrLf & " AND A.FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_FTInvoiceNo) & "' "
                        _Qry &= vbCrLf & " AND IM.FTRawMatCode='" & HI.UL.ULF.rpQuoted(_FTMaterialCode) & "' "
                        _Qry &= vbCrLf & " AND ISNULL(IMC.FTRawMatColorCode,'')='" & HI.UL.ULF.rpQuoted(_FTMaterialColorCode) & "' "

                        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                            R!FTStateComplete = "2"
                        Else
                            R!FTStateComplete = "1"
                        End If

                    End If

                Next

                Me.ogcdetail.DataSource = dataheader

            Else

                Me.ogcdetail.DataSource = Nothing

            End If

            _Pls.Close()


            If (_FileError) Then
                HI.MG.ShowMsg.mInfo("รูปแบบ File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1409160002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            _Pls.Close()
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick

        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
                    opFileDialog.ShowDialog()

                    Try
                        If opFileDialog.FileName <> "" Then
                            FTFilePath.Text = opFileDialog.FileName
                            Call LoadFile(FTFilePath.Text)
                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                End Try

            Case Else
                '...do nothing
        End Select
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With ogvdetail
                Try
                    Select Case ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateComplete").ToString)
                        Case "0"
                            e.Appearance.ForeColor = Drawing.Color.Red
                        Case "2"
                            e.Appearance.ForeColor = Drawing.Color.Green
                    End Select

                Catch ex As Exception
                End Try
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReposFTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFTSelect.EditValueChanging
        With ogvdetail
            If .FocusedRowHandle < 0 Then Exit Sub
            Try
                Select Case ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateComplete").ToString)
                    Case "1"
                        e.Cancel = False
                    Case Else
                        e.Cancel = True
                End Select
             
            Catch ex As Exception
            End Try
        End With
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FTFilePath.Text = ""
        Me.ogcdetail.DataSource = Nothing
        Try
            Me.dataheader = Nothing
            Me.datadetail = Nothing
        Catch ex As Exception
        End Try
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub wImportReceiveFormatNanyang_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Sub FTStateSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateSelectAll.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTStateSelectAll.Checked Then
                _State = "1"
            End If

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then

                    With ogvdetail

                        For I As Integer = 0 To .RowCount - 1

                            If ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateComplete").ToString) = "1" Then
                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                            End If
                        Next

                    End With

                    CType(.DataSource, DataTable).AcceptChanges()

                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmimportoptiplan_Click(sender As Object, e As EventArgs) Handles ocmimportexcel.Click
        Try
            If (ValidateData()) Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Import ข้อมูลใช่หรือไม่ กรุณาทำการยืนยัน ", 1409170001) = True Then

                    Dim dtrcv As New DataTable
                    dtrcv.Columns.Add("FTReceiveNo", GetType(String))
                    dtrcv.Columns.Add("FTPurchaseNo", GetType(String))
                    dtrcv.Columns.Add("FTInvoiceNo", GetType(String))

                    If Me.Import(dtrcv) Then
                        HI.MG.ShowMsg.mInfo("Import Data Complete !!!", 1409170003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                        Call LoadFile(FTFilePath.Text)

                        With _lstRcv
                            .ogclist.DataSource = dtrcv.Copy
                            .ShowDialog()
                        End With
                    Else
                        HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Import ได้ กรุณาทำการตรวจสอบข้อมูล หรือติดต่อ Admin !!!", 1409170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    End If

                    dtrcv.Dispose()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub
End Class
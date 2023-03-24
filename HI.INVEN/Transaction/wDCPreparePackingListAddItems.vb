Imports DevExpress.XtraEditors.Controls

Public Class wDCPreparePackingListAddItems

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _ReceiveNo As String = ""
    Public Property ReceiveNo() As String
        Get
            Return _ReceiveNo
        End Get
        Set(value As String)
            _ReceiveNo = value
        End Set

    End Property


    Private _PONo As String = ""
    Public Property PONo() As String
        Get
            Return _PONo
        End Get
        Set(value As String)
            _PONo = value
        End Set
    End Property

    Private _POFactoryNo As String = ""
    Public Property POFactoryNo() As String
        Get
            Return _POFactoryNo
        End Get
        Set(value As String)
            _POFactoryNo = value
        End Set
    End Property

    Private _SysWHID As Integer = 0
    Public Property WHID() As Integer
        Get
            Return _SysWHID
        End Get
        Set(value As Integer)
            _SysWHID = value
        End Set
    End Property


    Private _StateOK As Boolean = False
    Public Property StateOK() As Boolean
        Get
            Return _StateOK
        End Get
        Set(value As Boolean)
            _StateOK = value
        End Set
    End Property


    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Function AutoTransfer(Optional StateSendApp As String = "0") As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Auto Create Rawmaterial Packing List.. Please Wait.... ")
        Try
            Dim cmdstring As String = ""
            Dim dtrmpl As DataTable
            Dim dtmat As DataTable
            Dim dtpld As DataTable
            Dim dtbarcode As DataTable
            Dim _tmpOrderPL As String = ""
            With CType(ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                dtrmpl = .Copy()
            End With

            For Each Rx As DataRow In dtrmpl.Select("FTStateSelect='1'")

                cmdstring = "UPDATE X SET X.FTStateSelect= '1' "
                cmdstring &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS X "
                cmdstring &= vbCrLf & "  WHERE        (X.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')  AND (X.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "') "
                cmdstring &= vbCrLf & "  AND X.FNHSysRawMatId = " & Val(Rx!FNHSysRawMatId.ToString) & ""

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            Next

            Dim dt As New DataTable
            cmdstring = "SELECT        FTSubOrderNo, FTColorWay "
            cmdstring &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS A "
            cmdstring &= vbCrLf & " WHERE        (A.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')  "
            cmdstring &= vbCrLf & " And (A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') "
            cmdstring &= vbCrLf & " And (A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "') "
            cmdstring &= vbCrLf & " And (A.FTStateSelect = N'1')"
            cmdstring &= vbCrLf & " GROUP BY FTSubOrderNo, FTColorWay"

            dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In dt.Rows

                cmdstring = "   SELECT  TOP 1 FTDCPLNo"
                cmdstring &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList"
                cmdstring &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'  AND LEN(FTDCPLNo) = LEN('" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text & "-PL001") & "') "
                cmdstring &= vbCrLf & "  ORDER BY FTDCPLNo DESC "

                _tmpOrderPL = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_INVEN, "")

                If _tmpOrderPL = "" Then
                    _tmpOrderPL = FTOrderNo.Text & "-PL001"
                Else
                    _tmpOrderPL = FTOrderNo.Text & "-PL" & Microsoft.VisualBasic.Right("0000" & Format(Val(Microsoft.VisualBasic.Right(_tmpOrderPL, 3)) + 1, "0"), 3)
                End If

                cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList (FTInsUser, FDInsDate, FTInsTime, FTDCPLNo, FTOrderNo, FTRemark)"
                cmdstring &= vbCrLf & "  select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                cmdstring &= vbCrLf & ",''"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

                cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_Breakdown (FTInsUser, FDInsDate, FTInsTime, FTDCPLNo, FTOrderNo, FTSubOrderNo, FTColorway,FTSizeBreakDown, FTPLLine,  FNQuantity, FNPackQuantity, FNBundleQty, FNBundleQtyDif)"
                cmdstring &= vbCrLf & "  select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'"
                cmdstring &= vbCrLf & ",A.FTOrderNo, A.FTSubOrderNo, A.FTColorWay, A.FTSizeBreakdown, MAX(A.FTPOLine) AS FTPOLine, SUM(( ISNULL(A.FNQuantity,0)+ ISNULL(A.FNQuantityExtra,0)+ ISNULL(A.FNQuantityTest,0))) AS FNQuantity,MAX(ISNULL(Sub.FNPackPerCarton,0)) AS FNPackQuantity,0 AS FNBundleQty,0 AS FNBundleQtyDif  "
                cmdstring &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS A  WITH(NOLOCK)"
                cmdstring &= vbCrLf & "   OUTER APPLY(SELECT TOP 1  FDShipDate, FNPackPerCarton FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As Sub With(NOLOCK) WHERE Sub.FTSubOrderNo = A.FTSubOrderNo  )AS Sub "
                cmdstring &= vbCrLf & "   WHERE        (A.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')  "
                cmdstring &= vbCrLf & "      And (A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') "
                cmdstring &= vbCrLf & "      And (A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "') "
                cmdstring &= vbCrLf & "      And (A.FTColorWay='" & HI.UL.ULF.rpQuoted(R!FTColorWay.ToString) & "') "
                cmdstring &= vbCrLf & "      And (FTStateSelect = N'1') "
                cmdstring &= vbCrLf & " GROUP BY A.FTOrderNo, A.FTSubOrderNo, A.FTColorWay, A.FTSizeBreakdown "
                cmdstring &= vbCrLf & "  UPDATE X SET X.FNBundleQty  =  FLOOR(X.FNQuantity / X.FNPackQuantity) , X.FNBundleQtyDif =  X.FNQuantity % X.FNPackQuantity  "
                cmdstring &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_Breakdown AS X "
                cmdstring &= vbCrLf & "  WHERE X.FTDCPLNo='" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'"
                cmdstring &= vbCrLf & "         AND X.FNPackQuantity>0 "

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

                cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_Rawmat (FTInsUser, FDInsDate, FTInsTime, FTDCPLNo, FNHSysRawmatId, FNQuantity, FTPONo, FNPOQuantity, FTRSVNo, FNRSVQuantity, FTRemark)"
                cmdstring &= vbCrLf & "  select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'"
                cmdstring &= vbCrLf & ",A.FNHSysRawmatId,SUM(A.FNPRQuantity) As FNPRQuantity, MAX(A.FTPONo) AS FTPONo, MAX(A.FNPOQuantity) AS FNPOQuantity, MAX(A.FTRSVNo) AS FTRSVNo, MAX(A.FNRSVQuantity) AS FNRSVQuantity ,''  "
                cmdstring &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS A  WITH(NOLOCK)"
                cmdstring &= vbCrLf & "   WHERE        (A.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')  "
                cmdstring &= vbCrLf & "      And (A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') "
                cmdstring &= vbCrLf & "      And (A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "') "
                cmdstring &= vbCrLf & "      And (A.FTColorWay='" & HI.UL.ULF.rpQuoted(R!FTColorWay.ToString) & "') "
                cmdstring &= vbCrLf & "      And (FTStateSelect = N'1') "
                cmdstring &= vbCrLf & " GROUP BY A.FNHSysRawmatId "

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_INVEN)


                cmdstring = "  SELECT  X.FTColorway, X.FTPLLine, X.FTSizeBreakDown, FNQuantity,X.FNBundleQty,X.FNBundleQtyDif ,X.FNPackQuantity,ISNULL(MMS.FNMatSizeSeq,99999999) AS FNMatSizeSeq  "
                cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_Breakdown AS X "
                cmdstring &= vbCrLf & " OUTER APPLY(SELECT TOP 1 FNMatSizeSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS SM (NOLOCK) WHERE  SM.FTMatSizeCode  = X.FTSizeBreakDown ) AS MMS "
                cmdstring &= vbCrLf & "  WHERE X.FTDCPLNo='" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'"
                cmdstring &= vbCrLf & " ORDER BY  X.FTColorway, ISNULL(MMS.FNMatSizeSeq,99999999)"
                dtpld = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_INVEN)


                Dim MaxBag As Integer = 0
                Dim DiffBag As Integer = 0
                Dim Consump As Decimal = 0.0
                Dim PackQuantity As Decimal = 0.0
                Dim BarcodePLLidx As Integer = 1
                Dim BarcodePLPreFix As String = "PL" & _tmpOrderPL.Replace("-PL", "")
                Dim BarcodePLNo As String = "PL" & _tmpOrderPL.Replace("-PL", "") & "0000000"

                Dim FNBarMatRefSeq As Integer = 1
                Dim FNBarPLSeq As Integer = 0

                For Each RxD As DataRow In dtpld.Rows

                    MaxBag = Val(RxD!FNBundleQty.ToString)
                    DiffBag = Val(RxD!FNBundleQtyDif.ToString)
                    PackQuantity = Val(RxD!FNPackQuantity.ToString)

                    If PackQuantity > 0 Then

                        cmdstring = "  SELECT       A.FNHSysRawMatId,A.FTColorWay,A.FTSizeBreakdown"
                        cmdstring &= vbCrLf & " ,MAX(A.FNConSmp) AS FNConSmp"
                        cmdstring &= vbCrLf & " ,MAX(ISNULL(OIH.FNBal,0)) AS FNBal,Sum(A.FNUsedQuantity) AS FNTotalUsed"
                        cmdstring &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS A WITH(NOLOCK)  "
                        cmdstring &= vbCrLf & "  OUTER APPLY ( "
                        cmdstring &= vbCrLf & "    Select  SUM( BI.FNQuantity - ISNULL(BO.FNOutQuantity,0)) As FNBal"
                        cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As BI WITH(NOLOCK) INNER Join"
                        cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B WITH(NOLOCK) On BI.FTBarcodeNo = B.FTBarcodeNo"
                        cmdstring &= vbCrLf & "  INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As W WITH(NOLOCK) ON BI.FNHSysWHId = W.FNHSysWHId "
                        cmdstring &= vbCrLf & "   OUTER APPLY(Select SUM( FNQuantity) As FNOutQuantity FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_Out As BO WITH(NOLOCK) "
                        cmdstring &= vbCrLf & "  WHERE BO.FTBarcodeNo = BI.FTBarcodeNo "
                        cmdstring &= vbCrLf & "        And  BO.FNHSysWHId = BI.FNHSysWHId"
                        cmdstring &= vbCrLf & "        And BO.FTOrderNo =BI.FTOrderNo "
                        cmdstring &= vbCrLf & "        And BO.FTDocumentRefNo = BI.FTDocumentNo   ) As BO"
                        cmdstring &= vbCrLf & "  WHERE BI.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                        cmdstring &= vbCrLf & "   AND B.FNHSysRawMatId = A.FNHSysRawMatId"
                        cmdstring &= vbCrLf & "   And CASE WHEN ISNULL(W.FNHSysCmpManageId, 0) =0 THEN W.FNHSysCmpId ELSE  ISNULL(W.FNHSysCmpManageId,0)  END = " & HI.ST.SysInfo.CmpID & " "
                        cmdstring &= vbCrLf & "  ) As OIH  "
                        cmdstring &= vbCrLf & "   WHERE        (A.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')  "
                        cmdstring &= vbCrLf & "                And (A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') "
                        cmdstring &= vbCrLf & "                And (A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "') "
                        cmdstring &= vbCrLf & "                And (A.FTColorWay='" & HI.UL.ULF.rpQuoted(R!FTColorWay.ToString) & "') "
                        cmdstring &= vbCrLf & "                And (A.FTSizeBreakdown='" & HI.UL.ULF.rpQuoted(RxD!FTSizeBreakDown.ToString) & "') "
                        cmdstring &= vbCrLf & "                And (FTStateSelect = N'1') "
                        cmdstring &= vbCrLf & "  GROUP BY A.FNHSysRawMatId,A.FTColorWay,A.FTSizeBreakdown"


                        dtmat = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)




                        cmdstring = " SELECT AA.FNHSysRawMatId,OIH.FTBarcodeNo,OIH.FNHSysWHLocationId, OIH.FTDocumentNo, OIH.FNHSysWHId, OIH.FTOrderNo, OIH.FNQuantity, OIH.FTDocumentRefNo, OIH.FNHSysCmpId, OIH.FNBal  ,Convert(numeric(18,4),0.0000) AS UsedQty ,OIH.FNBal AS BalQty "
                        cmdstring &= vbCrLf & " FROM ( SELECT       A.FNHSysRawMatId"
                        cmdstring &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS A WITH(NOLOCK)  "
                        cmdstring &= vbCrLf & "   WHERE        (A.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')  "
                        cmdstring &= vbCrLf & "                And (A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') "
                        cmdstring &= vbCrLf & "                And (A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "') "
                        cmdstring &= vbCrLf & "                And (A.FTColorWay='" & HI.UL.ULF.rpQuoted(R!FTColorWay.ToString) & "') "
                        cmdstring &= vbCrLf & "                And (A.FTSizeBreakdown='" & HI.UL.ULF.rpQuoted(RxD!FTSizeBreakDown.ToString) & "') "
                        cmdstring &= vbCrLf & "                And (FTStateSelect = N'1') "
                        cmdstring &= vbCrLf & "  GROUP BY A.FNHSysRawMatId ) As  AA"
                        cmdstring &= vbCrLf & "  OUTER APPLY ( "
                        cmdstring &= vbCrLf & "    Select   BI.FTBarcodeNo,BI.FNHSysWHLocationId, BI.FTDocumentNo, BI.FNHSysWHId, BI.FTOrderNo, BI.FNQuantity, BI.FTDocumentRefNo, BI.FNHSysCmpId, BI.FNQuantity - ( ISNULL(BO.FNOutQuantity,0) +  ISNULL(BO2.FNOutQuantity,0) +  ISNULL(BO3.FNOutQuantity,0) ) As FNBal"
                        cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As BI WITH(NOLOCK) INNER Join"
                        cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B WITH(NOLOCK) On BI.FTBarcodeNo = B.FTBarcodeNo"
                        cmdstring &= vbCrLf & "  INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As W WITH(NOLOCK) ON BI.FNHSysWHId = W.FNHSysWHId "
                        cmdstring &= vbCrLf & "   OUTER APPLY(Select SUM( FNQuantity) As FNOutQuantity FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_Out As BO WITH(NOLOCK) "
                        cmdstring &= vbCrLf & "  WHERE BO.FTBarcodeNo = BI.FTBarcodeNo "
                        cmdstring &= vbCrLf & " And  BO.FNHSysWHId = BI.FNHSysWHId"
                        cmdstring &= vbCrLf & "   And BO.FTOrderNo =BI.FTOrderNo "
                        cmdstring &= vbCrLf & " And BO.FTDocumentRefNo = BI.FTDocumentNo   ) As BO"


                        cmdstring &= vbCrLf & "   OUTER APPLY(Select SUM( FNQuantity) As FNOutQuantity FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM As BO2 WITH(NOLOCK) "
                        cmdstring &= vbCrLf & "  WHERE BO2.FTBarcodeNo = BI.FTBarcodeNo "
                        cmdstring &= vbCrLf & "   And  BO2.FNHSysWHId = BI.FNHSysWHId"
                        cmdstring &= vbCrLf & "   And BO2.FTOrderNo =BI.FTOrderNo "
                        cmdstring &= vbCrLf & " And BO2.FTDocumentRefNo = BI.FTDocumentNo AND  BO2.FTStateIssue='0'  ) As BO2"


                        cmdstring &= vbCrLf & "   OUTER APPLY(Select SUM( FNQuantity) As FNOutQuantity FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode As BO3 WITH(NOLOCK) "
                        cmdstring &= vbCrLf & "  WHERE BO3.FTBarcodeNo = BI.FTBarcodeNo "
                        cmdstring &= vbCrLf & "   And  BO3.FNHSysWHId = BI.FNHSysWHId"
                        cmdstring &= vbCrLf & "   And BO3.FTOrderNo =BI.FTOrderNo "
                        cmdstring &= vbCrLf & " And BO3.FTDocumentRefNo = BI.FTDocumentNo AND   ISNULL(BO3.FTIssueReferNo,'')=''  ) As BO3"

                        cmdstring &= vbCrLf & "  WHERE BI.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                        cmdstring &= vbCrLf & "   AND B.FNHSysRawMatId = AA.FNHSysRawMatId"
                        cmdstring &= vbCrLf & "   And CASE WHEN ISNULL(W.FNHSysCmpManageId, 0) =0 THEN W.FNHSysCmpId ELSE  ISNULL(W.FNHSysCmpManageId,0)  END = " & HI.ST.SysInfo.CmpID & " "
                        cmdstring &= vbCrLf & "  ) As OIH  "
                        cmdstring &= vbCrLf & "  WHERE ISNULL(OIH.FNBal,0) >0 "

                        dtbarcode = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)



                        For I As Integer = 1 To MaxBag
                            BarcodePLNo = BarcodePLPreFix & Microsoft.VisualBasic.Right("0000000" & Format(Val(Microsoft.VisualBasic.Right(BarcodePLNo, 7)) + 1, "0"), 7)

                            FNBarPLSeq = FNBarPLSeq + 1

                            cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENDCPackingList_PLBarcode (FTInsUser, FDInsDate, FTInsTime,FTDCPLNo, FNSeq, FTPLBarcodeNo, FTRemark, FTColorway, FTPLLine, FTSizeBreakDown,FNQuantity)"
                            cmdstring &= vbCrLf & "  Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'," & FNBarPLSeq & ""

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(BarcodePLNo) & "'"
                            cmdstring &= vbCrLf & ",''"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxD!FTColorWay.ToString) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxD!FTPLLine.ToString) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxD!FTSizeBreakDown.ToString) & "'"
                            cmdstring &= vbCrLf & "," & PackQuantity & ""

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

                            For Each Rx As DataRow In dtmat.Rows
                                Consump = Val(Rx!FNConSmp.ToString()) * PackQuantity

                                While Consump > 0 And dtbarcode.Select("FNHSysRawMatId=" & Val(Rx!FNHSysRawMatId.ToString) & " AND BalQty>0").Length > 0


                                    Dim BarQty As Decimal = 0.0
                                    With dtbarcode
                                        .BeginInit()
                                        For Each RxB As DataRow In .Select("FNHSysRawMatId=" & Val(Rx!FNHSysRawMatId.ToString) & "  AND BalQty>0 ")

                                            If Val(RxB!BalQty.ToString) >= Consump Then

                                                BarQty = Consump

                                                RxB!UsedQty = Val(RxB!UsedQty) + BarQty
                                                RxB!BalQty = Val(RxB!BalQty) - BarQty

                                                Consump = 0

                                            Else
                                                BarQty = Val(RxB!BalQty.ToString)


                                                RxB!UsedQty = Val(RxB!UsedQty) + BarQty


                                                Consump = Consump - Val(RxB!BalQty.ToString)
                                                RxB!BalQty = 0
                                            End If


                                            cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM (FTInsUser, FDInsDate, FTInsTime,FTDCPLNo,FTPLBarcodeNo, FNBarMatRefSeq, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FTDocumentRefNo, FNHSysCmpId, FNQuantity,FTStateIssue,FNHSysWHLocationId)"
                                            cmdstring &= vbCrLf & "  Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'"
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(BarcodePLNo) & "'"
                                            cmdstring &= vbCrLf & "," & FNBarMatRefSeq & ""
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTBarcodeNo.ToString) & "'"
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTDocumentNo.ToString) & "'"
                                            cmdstring &= vbCrLf & "," & Val(RxB!FNHSysWHId.ToString) & ""
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTOrderNo.ToString) & "'"
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTDocumentNo.ToString) & "'"
                                            cmdstring &= vbCrLf & "," & Val(RxB!FNHSysCmpId.ToString) & ""
                                            cmdstring &= vbCrLf & "," & BarQty & ""
                                            cmdstring &= vbCrLf & ",'0'"
                                            cmdstring &= vbCrLf & "," & Val(RxB!FNHSysWHLocationId.ToString) & ""

                                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

                                            FNBarMatRefSeq = FNBarMatRefSeq + 1


                                            If Consump <= 0 Then
                                                Exit For
                                            End If
                                        Next
                                        .EndInit()
                                    End With


                                End While

                            Next

                        Next

                        If DiffBag > 0 Then

                            BarcodePLNo = BarcodePLPreFix & Microsoft.VisualBasic.Right("0000000" & Format(Val(Microsoft.VisualBasic.Right(BarcodePLNo, 7)) + 1, "0"), 7)

                            FNBarPLSeq = FNBarPLSeq + 1

                            cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENDCPackingList_PLBarcode (FTInsUser, FDInsDate, FTInsTime,FTDCPLNo, FNSeq, FTPLBarcodeNo, FTRemark, FTColorway, FTPLLine, FTSizeBreakDown,FNQuantity)"
                            cmdstring &= vbCrLf & "  Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'," & FNBarPLSeq & ""
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(BarcodePLNo) & "'"
                            cmdstring &= vbCrLf & ",''"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxD!FTColorWay.ToString) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxD!FTPLLine.ToString) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxD!FTSizeBreakDown.ToString) & "'"
                            cmdstring &= vbCrLf & "," & DiffBag & ""

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

                            For Each Rx As DataRow In dtmat.Rows
                                Consump = Val(Rx!FNConSmp.ToString()) * DiffBag

                                While Consump > 0 And dtbarcode.Select("FNHSysRawMatId=" & Val(Rx!FNHSysRawMatId.ToString) & " AND BalQty>0").Length > 0


                                    Dim BarQty As Decimal = 0.0
                                    With dtbarcode
                                        .BeginInit()
                                        For Each RxB As DataRow In .Select("FNHSysRawMatId=" & Val(Rx!FNHSysRawMatId.ToString) & "  AND BalQty>0 ")

                                            If Val(RxB!BalQty.ToString) >= Consump Then

                                                BarQty = Consump

                                                RxB!UsedQty = Val(RxB!UsedQty) + BarQty
                                                RxB!BalQty = Val(RxB!BalQty) - BarQty

                                                Consump = 0

                                            Else

                                                BarQty = Val(RxB!BalQty.ToString)
                                                Consump = Consump - Val(RxB!BalQty.ToString)

                                                RxB!UsedQty = Val(RxB!UsedQty) + BarQty
                                                RxB!BalQty = 0

                                            End If


                                            cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM (FTInsUser, FDInsDate, FTInsTime,FTDCPLNo,FTPLBarcodeNo, FNBarMatRefSeq, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FTDocumentRefNo, FNHSysCmpId, FNQuantity,FTStateIssue)"
                                            cmdstring &= vbCrLf & "  Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderPL) & "'"
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(BarcodePLNo) & "'"
                                            cmdstring &= vbCrLf & "," & FNBarMatRefSeq & ""
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTBarcodeNo.ToString) & "'"
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTDocumentNo.ToString) & "'"
                                            cmdstring &= vbCrLf & "," & Val(RxB!FNHSysWHId.ToString) & ""
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTOrderNo.ToString) & "'"
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RxB!FTDocumentNo.ToString) & "'"
                                            cmdstring &= vbCrLf & "," & Val(RxB!FNHSysCmpId.ToString) & ""
                                            cmdstring &= vbCrLf & "," & BarQty & ""
                                            cmdstring &= vbCrLf & ",'0'"

                                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

                                            FNBarMatRefSeq = FNBarMatRefSeq + 1

                                            If Consump <= 0 Then
                                                Exit For
                                            End If

                                        Next
                                        .EndInit()
                                    End With


                                End While

                            Next


                        End If

                    End If

                Next

            Next

            _Spls.Close()

            HI.MG.ShowMsg.mInfo("Auto Create Rawmaterial Packing List Complete... ", 1335555775, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False

        End Try

        Return True
    End Function

    Private Sub ocmauto_Click(sender As Object, e As EventArgs) Handles ocmcreatepl.Click
        If Me.FTSubOrderNo.Text <> "" And FTSubOrderNo.Properties.Tag.ToString <> "" Then


            With CType(ogcbarcode.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTStateSelect='1' ").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Barcode ที่ต้องการทำการ Transfer !!!", 1414424240, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If

            End With

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ  Auto Create Ramaterial PL ใช่หรือไม่ ?", 1414424241) = True Then

                If Me.AutoTransfer() Then
                    Me.StateOK = True
                    Me.Close()
                Else
                    HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ  Auto Create Ramaterial PL ได้ เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1414424242, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTSubOrderNo_lbl.Text)
            FTSubOrderNo.Focus()
        End If
    End Sub

    Private Sub wReceiveAutoTransferToCenter_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Sub FTStaSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaSelectAll.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTStaSelectAll.Checked Then
                _State = "1"
            End If

            With ogcbarcode
                If Not (.DataSource Is Nothing) And ogvbarcode.RowCount > 0 Then

                    With ogvbarcode
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTSubOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSubOrderNo.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FTSubOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            If FTSubOrderNo.Text <> "" Then

                Dim _Qry As String = "SELECT TOP 1 FTSubOrderNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub WITH(NOLOCK) WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' "

                FTSubOrderNo.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

                Call LoadDataInfo(FTSubOrderNo.Text)
            Else

            End If
        End If
    End Sub

    Private Sub LoadDataInfo(key As String)
        Dim cmdstring As String = ""
        Dim dt As DataTable

        cmdstring = "UPDATE X SET X.FTStateSelect= '0' "
        cmdstring &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS X "
        cmdstring &= vbCrLf & "  WHERE        (X.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
        cmdstring &= vbCrLf & "  SELECT     '0' AS FTStateSelect,   A.FNHSysRawMatId, IM.FTRawMatCode, MAX(IM.FTRawMatNameEN) AS FTRawMatName"
        cmdstring &= vbCrLf & " , IMC.FTRawMatColorCode"
        cmdstring &= vbCrLf & " , IMS.FTRawMatSizeCode"
        cmdstring &= vbCrLf & " ,MAX(A.FNConSmp) AS FNConSmp"
        cmdstring &= vbCrLf & " ,MAX(ISNULL(OIH.FNBal,0)) AS FNBal,Sum(A.FNUsedQuantity) AS FNTotalUsed"
        cmdstring &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TTmpRawmatPL AS A WITH(NOLOCK) INNER JOIN "
        cmdstring &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)  ON A.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN "
        cmdstring &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK)  ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN "
        cmdstring &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK)  ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
        cmdstring &= vbCrLf & "  OUTER APPLY ( "
        cmdstring &= vbCrLf & "    Select  SUM( BI.FNQuantity - (ISNULL(BO.FNOutQuantity,0)+ ISNULL(BO2.FNOutQuantity,0) + ISNULL(BO3.FNOutQuantity,0) )) As FNBal"
        cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As BI WITH(NOLOCK) INNER Join"
        cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B WITH(NOLOCK) On BI.FTBarcodeNo = B.FTBarcodeNo"
        cmdstring &= vbCrLf & "  INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As W WITH(NOLOCK) ON BI.FNHSysWHId = W.FNHSysWHId "
        cmdstring &= vbCrLf & "   OUTER APPLY(Select SUM( FNQuantity) As FNOutQuantity FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_Out As BO WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE BO.FTBarcodeNo = BI.FTBarcodeNo "
        cmdstring &= vbCrLf & " And  BO.FNHSysWHId = BI.FNHSysWHId"
        cmdstring &= vbCrLf & "   And BO.FTOrderNo =BI.FTOrderNo "
        cmdstring &= vbCrLf & " And BO.FTDocumentRefNo = BI.FTDocumentNo    ) As BO"


        cmdstring &= vbCrLf & "   OUTER APPLY(Select SUM( FNQuantity) As FNOutQuantity FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM As BO2 WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE BO2.FTBarcodeNo = BI.FTBarcodeNo "
        cmdstring &= vbCrLf & "   And  BO2.FNHSysWHId = BI.FNHSysWHId"
        cmdstring &= vbCrLf & "   And BO2.FTOrderNo =BI.FTOrderNo "
        cmdstring &= vbCrLf & " And BO2.FTDocumentRefNo = BI.FTDocumentNo AND  BO2.FTStateIssue='0'  ) As BO2"


        cmdstring &= vbCrLf & "   OUTER APPLY(Select SUM( FNQuantity) As FNOutQuantity FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode As BO3 WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE BO3.FTBarcodeNo = BI.FTBarcodeNo "
        cmdstring &= vbCrLf & "   And  BO3.FNHSysWHId = BI.FNHSysWHId"
        cmdstring &= vbCrLf & "   And BO3.FTOrderNo =BI.FTOrderNo "
        cmdstring &= vbCrLf & " And BO3.FTDocumentRefNo = BI.FTDocumentNo AND   ISNULL(BO3.FTIssueReferNo,'') ='' ) As BO3"

        cmdstring &= vbCrLf & "  WHERE BI.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
        cmdstring &= vbCrLf & "   AND B.FNHSysRawMatId = A.FNHSysRawMatId"
        cmdstring &= vbCrLf & "   And CASE WHEN ISNULL(W.FNHSysCmpManageId, 0) =0 THEN W.FNHSysCmpId ELSE  ISNULL(W.FNHSysCmpManageId,0)  END = " & HI.ST.SysInfo.CmpID & " "
        cmdstring &= vbCrLf & "  ) As OIH  "
        cmdstring &= vbCrLf & "  WHERE        (A.FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND (A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(key) & "') "
        cmdstring &= vbCrLf & "  GROUP BY A.FNHSysRawMatId, IM.FTRawMatCode, IMC.FTRawMatColorCode, IMS.FTRawMatSizeCode"
        cmdstring &= vbCrLf & " ORDER BY  IM.FTRawMatCode, IMC.FTRawMatColorCode, IMS.FTRawMatSizeCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogcbarcode.DataSource = dt.Copy

    End Sub

    Private Sub RepFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTSelect.EditValueChanging

        Try

            With Me.ogvbarcode

                If .FocusedRowHandle < 0 Then Exit Sub
                If Val(.GetFocusedRowCellValue("FNBal").ToString) < Val(.GetFocusedRowCellValue("FNTotalUsed").ToString) Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If

            End With

        Catch ex As Exception
        End Try

    End Sub
End Class
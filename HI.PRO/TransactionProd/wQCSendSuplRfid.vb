Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraEditors

Public Class wQCSendSuplRfid
    Private _FNSendSuplType As Integer = 0
    Private _Type As String = ""
    Private _Static As Boolean = False
    Private _StateSave As Boolean = True
    Private _wAccept As wQCSendSupllist
    Private _HyperActive As Boolean = IsHyperActive()   ' State 1 = For HyperActive
    Private _TypeQCScan As String = IsTypeQCScan()      ' TypeQCScan = 1 for BoxBarcode / 2 For RFID

    Sub New()

        InitializeComponent()

        _wAccept = New wQCSendSupllist
        HI.TL.HandlerControl.AddHandlerObj(_wAccept)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wAccept.Name.ToString.Trim, _wAccept)
        Catch ex As Exception
        Finally
        End Try

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    'If Me.FTRfidNo.Text = "" Then
                    '    HI.MG.ShowMsg.mInfo("บาร์โค๊ด RFID ยังไม่มีการรับ..", 2010061452, Me.Text, , MessageBoxIcon.Error)
                    '    Me.FTRfidNo.Focus()
                    '    Exit Sub
                    'Else
                    If Me.FTBarcodeNo.Text = "" Then
                        HI.MG.ShowMsg.mInfo("บาร์โค๊ดยังไม่มีการรับ..", 2010061452, Me.Text, , MessageBoxIcon.Error)
                        Exit Sub
                    Else
                        If checkBarcodeSendSupl(FTBarcodeNo.Text) Then
                            If Not (checkBarcodeReceive(FTBarcodeNo.Text)) Then
                                HI.MG.ShowMsg.mInfo("บาร์โค๊ดยังไม่มีการรับ..", 2010061452, Me.Text, , MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End If
                        Call LoadBarcodeInfo(FTBarcodeNo.Text)
                    End If
                    'End If
                    If _HyperActive And _TypeQCScan <> "" And FTRfidNo.Text <> "" Then
                        If Not (checkColorWaySize(HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text), HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text))) Then
                            HI.MG.ShowMsg.mInfo("ColorWay and Size Not Match..", 2010061453, Me.Text, , MessageBoxIcon.Error)
                            ClearForm()
                            Exit Sub
                        End If
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTRfidNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTRfidNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    If (FTRfidNo.Text <> "") Then
                        If (isBarcodeSendSupl(FTRfidNo.Text)) Then
                            'HI.MG.ShowMsg.mInfo("Please check Barcode!!!", 1609161057, Me.Text, "", MessageBoxIcon.Information)
                            FTBarcodeNo.Text = FTRfidNo.Text
                            FTRfidNo.Text = ""
                        End If
                    End If
                    'If Me.FTRfidNo.Text = "" Then
                    '    HI.MG.ShowMsg.mInfo("บาร์โค๊ด RFID ยังไม่มีการรับ..", 2010061452, Me.Text, , MessageBoxIcon.Error)
                    '    Me.FTRfidNo.Focus()
                    '    Exit Sub
                    'End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTRfidNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTRfidNo.EditValueChanged
        If (FTRfidNo.Text <> "") Then
            If (isBarcodeSendSupl(FTRfidNo.Text)) Then
                'HI.MG.ShowMsg.mInfo("Please check Barcode!!!", 1609161057, Me.Text, "", MessageBoxIcon.Information)
                FTBarcodeNo.Text = FTRfidNo.Text
                FTRfidNo.Text = ""
            End If
        End If
    End Sub

    Private Function isBarcodeSendSupl(_BarcodeNo As String) As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT Count(ss.FDInsDate) "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSupl_Barcode AS ss WITH (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE ss.FTSendSuplNo = '" & _BarcodeNo & "' OR ss.FTBarcodeSendSuplNo = '" & _BarcodeNo & "'"
            Return If(Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HYPERACTIVE, "0")) <= 0, False, True)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckBarCode(Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT Top 1  FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSuplToBranch_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FTBarcodeSendSuplNo ='" & Key & "'"
            _Cmd &= vbCrLf & "UNION"
            _Cmd &= vbCrLf & "SELECT Top 1  FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSupl_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FTBarcodeSendSuplNo ='" & Key & "'"

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function checkBarcodeReceive(_BarcodeNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select * from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..fn_check_rcvsupl('" & _BarcodeNo & "') "
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function checkBarcodeSendSupl(_BarcodeNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select * from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..fn_checksendsupl('" & _BarcodeNo & "') "
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadBarcodeInfo(Key As String)

        Try
            If Not (CheckBarCode(Key)) Then
                FTStyleCode.Text = ""
                FTOrderNo.Text = ""
                FTOrderProdNo.Text = ""
                FTColorway.Text = ""
                FTSizeBreakDown.Text = ""
                FNHSysOperationId.Text = ""
                FNHSysMarkId.Text = ""
                FNHSysPartId.Text = ""
                FTBarcodeBundleNo.Text = ""
                FNHSysSuplId.Text = ""
                FNQuantity.Value = 0
                FNQCActualQty.Value = 0
                HI.MG.ShowMsg.mInfo("ข้อมูลบาร์โค้ดไม่ถูกต้อง....", 15061109471, Me.Text, "", MessageBoxIcon.Stop)
                Me.FTBarcodeNo.Focus()
                Me.FTBarcodeNo.SelectAll()
                Exit Sub
            End If

            Dim _Qry As String = ""
            Dim dt As DataTable
            Dim _StatePass As Boolean = False
            _Type = checkQCBefore(Key)

            _Qry = " SELECT  TOP 1 B.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNHSysPartId"
            _Qry &= vbCrLf & " 	, B.FNSendSuplType"
            _Qry &= vbCrLf & " 	, B.FNHSysSuplId"
            _Qry &= vbCrLf & " 	, B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, B.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, B.FTSendSuplRef"
            _Qry &= vbCrLf & " 	, B.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, S.FTSuplCode"
            _Qry &= vbCrLf & " 	, B.FTSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, B.FTColorway"
            _Qry &= vbCrLf & " 	, B.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, B.FNQuantity " '- dbo.get_defectqtybybundle(B.FTBarcodeSendSuplNo) as FNQuantity
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	,MP.FTPartCode "
            _Qry &= vbCrLf & " 	,ISNULL(B.FNHSysOperationId,0) AS FNHSysOperationId"
            _Qry &= vbCrLf & " 	,ISNULL(B.FNSeq,0) AS FNSeq"
            _Qry &= vbCrLf & " 	,ISNULL(B.FNHSysOperationIdTo,0) AS FNHSysOperationIdTo"
            _Qry &= vbCrLf & " 	,Mpp.FTOperationCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " ,ISNULL(("
            _Qry &= vbCrLf & "   SELECT        TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  CC.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & "  CC.FTMarkNameEN AS FTMarkName  "
            End If

            _Qry &= vbCrLf & "   FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTLayCut AS BB  WITH (NOLOCK) ON AA.FTLayCutNo = BB.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TPRODMMark AS Cc  WITH (NOLOCK)  ON BB.FNHSysMarkId = CC.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = B.FTBarcodeBundleNo)"

            _Qry &= vbCrLf & " ),'') AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSuplToBranch_Barcode AS B WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  B.FTOrderProdNo  LEFT OUTER JOIN"

            '_Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON A.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMOperationByStyle AS OPS WITH (NOLOCK)  ON O.FNHSysStyleId = OPS.FNHSysStyleId AND SD.FNHSysOperationId = OPS.FNHSysOperationId INNER JOIN"
            '_Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSupl_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSupplier AS S WITH (NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMPart  AS MP WITH (NOLOCK) ON B.FNHSysPartId = MP.FNHSysPartId"
            '_Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  B.FNHSysOperationId   = MPP.FNHSysOperationId"
            '_Qry &= vbCrLf & "    OUTER APPLY (SELECT TOP 1  FTBarcodeSendSuplNo FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTReceiveSupl_Barcode AS RB WITH(NOLOCK) WHERE   RB.FTBarcodeSendSuplNo  =B.FTBarcodeSendSuplNo ) AS XXX "
            _Qry &= vbCrLf & "   WHERE B.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' --AND ISNULL(BB.FTStateGenBarcode,'') ='1' "

            '_Qry &= vbCrLf & " and  OP.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " "
            '_Qry &= vbCrLf & "   AND (CASE WHEN S.FNSubContactType =0 THEN '1' ELSE ( CASE WHEN ISNULL(XXX.FTBarcodeSendSuplNo,'')<>'' THEN '1' ELSE '0' END ) END) ='1' "
            _Qry &= vbCrLf & "UNION "

            _Qry &= vbCrLf & " SELECT  TOP 1 B.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & " , B.FNHSysPartId"
            _Qry &= vbCrLf & " , B.FNSendSuplType"
            _Qry &= vbCrLf & " , B.FNHSysSuplId"
            _Qry &= vbCrLf & " , B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " , O.FTOrderNo "
            _Qry &= vbCrLf & " , B.FTOrderProdNo"
            _Qry &= vbCrLf & " , B.FTSendSuplRef"
            _Qry &= vbCrLf & " , B.FNHSysCmpId"
            _Qry &= vbCrLf & " , S.FTSuplCode"
            _Qry &= vbCrLf & " , B.FTSendSuplNo"
            _Qry &= vbCrLf & " , B.FNBunbleSeq"
            _Qry &= vbCrLf & " , B.FTColorway"
            _Qry &= vbCrLf & " , B.FTSizeBreakDown"
            _Qry &= vbCrLf & " , B.FNQuantity  " '- dbo.get_defectqtybybundle(B.FTBarcodeSendSuplNo) as FNQuantity  ///20201019
            _Qry &= vbCrLf & " , ST.FTStyleCode"
            _Qry &= vbCrLf & " , MP.FTPartCode "
            _Qry &= vbCrLf & " , ISNULL(OP.FNHSysOperationId,0) AS FNHSysOperationId"
            _Qry &= vbCrLf & " , ISNULL(OP.FNSeq,0) AS FNSeq"
            _Qry &= vbCrLf & " , ISNULL(OP.FNHSysOperationIdTo,0) AS FNHSysOperationIdTo"
            _Qry &= vbCrLf & " , Mpp.FTOperationCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " , MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " , MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " , MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " , ISNULL(("
            _Qry &= vbCrLf & " SELECT TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " CC.FTMarkNameTH AS FTMarkName "
            Else
                _Qry &= vbCrLf & " CC.FTMarkNameEN AS FTMarkName "
            End If

            _Qry &= vbCrLf & "   FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTLayCut AS BB  WITH (NOLOCK) ON AA.FTLayCutNo = BB.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TPRODMMark AS Cc  WITH (NOLOCK)  ON BB.FNHSysMarkId = CC.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = B.FTBarcodeBundleNo)"

            _Qry &= vbCrLf & " ),'') AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            '_Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSuplToBranch_Barcode AS B WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  B.FTOrderProdNo  LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	 ( Select  B.FTBarcodeSendSuplNo, FNHSysPartId , FNSendSuplType , SB.FNHSysSuplId , BD.FTBarcodeBundleNo , BD.FTOrderProdNo , FTSendSuplRef ,SB.FNHSysCmpId , SB.FTSendSuplNo "
            _Qry &= vbCrLf & " 	  , BD.FNBunbleSeq , BD.FTColorway, BD.FTSizeBreakDown , BD.FNQuantity  "
            _Qry &= vbCrLf & " 	   From    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSupl_Barcode AS B WITH (NOLOCK)   INNER JOIN "
            _Qry &= vbCrLf & " 	           " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTSendSupl AS SB WITH(NOLOCK) ON B.FTSendSuplNo = SB.FTSendSuplNo  LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	           " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODBarcode_SendSupl AS BS WITH(NOLOCK) ON B.FTBarcodeSendSuplNo = BS.FTBarcodeSendSuplNo  "
            _Qry &= vbCrLf & " 	  INNER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.V_TPRODTBundle_MainBarcode AS BD WITH(NOLOCK) ON BS.FTBarcodeBundleNo = BD.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	 ) AS B  ON  ODP.FTOrderProdNo =  B.FTOrderProdNo  INNER JOIN    "


            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSupplier AS S WITH (NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTOrderProd_SendSupl AS SD WITH(NOLOCK) ON B.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo  "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODMOperationByStyle AS OP with(nolock) ON O.FNHSysStyleId = OP.FNHSysStyleId AND SD.FNHSysOperationId = OP.FNHSysOperationId  "


            _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMPart  AS MP WITH (NOLOCK) ON B.FNHSysPartId = MP.FNHSysPartId"
            '_Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON OP.FNHSysOperationId   = MPP.FNHSysOperationId"
            '_Qry &= vbCrLf & "    OUTER APPLY (SELECT TOP 1  FTBarcodeSendSuplNo FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTReceiveSupl_Barcode AS RB WITH(NOLOCK) WHERE   RB.FTBarcodeSendSuplNo  =B.FTBarcodeSendSuplNo ) AS XXX "
            _Qry &= vbCrLf & "   WHERE B.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' --AND ISNULL(BB.FTStateGenBarcode,'') ='1' "


            '_Qry &= vbCrLf & " and  OP.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " " '/// 20201019


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            FTStyleCode.Text = ""
            FTOrderNo.Text = ""
            FTOrderProdNo.Text = ""
            FTColorway.Text = ""
            FTSizeBreakDown.Text = ""
            FNHSysOperationId.Text = ""
            FNHSysMarkId.Text = ""
            FNHSysPartId.Text = ""
            FTBarcodeBundleNo.Text = ""
            FNHSysSuplId.Text = ""
            FNQuantity.Value = 0
            FNQCActualQty.Value = 0
            If dt.Rows.Count > 0 Then

                For Each R As DataRow In dt.Rows

                    FTStyleCode.Text = R!FTStyleCode.ToString
                    FTOrderNo.Text = R!FTOrderNo.ToString
                    FTOrderProdNo.Text = R!FTOrderProdNo.ToString
                    FTColorway.Text = R!FTColorway.ToString
                    FTSizeBreakDown.Text = R!FTSizeBreakDown.ToString
                    FNHSysOperationId.Text = R!FTOperationName.ToString
                    FNHSysMarkId.Text = R!FNHSysMarkId.ToString
                    FNHSysPartId.Text = R!FTPartName.ToString
                    FTBarcodeBundleNo.Text = R!FNBunbleSeq.ToString
                    FNQuantity.Value = Double.Parse(R!FNQuantity.ToString)
                    _FNSendSuplType = Integer.Parse(R!FNSendSuplType.ToString)
                    Me.FNQCActualQty.Value = GetDefectQty()
                    Me.FNHSysSuplId.Text = R!FTSuplCode.ToString

                    If _Type <> "" Then _Type &= ","
                    _Type &= _FNSendSuplType
                    Call CreateGroupDefect(_Type)
                    Exit For

                Next
            Else

                If Me.FNQuantity.Value <= 0 Then

                    HI.MG.ShowMsg.mInfo("ยังไม่มีการรับงานเข้าระบบ.....กรุณาทำรับ", 15060900928, Me.Text, "", MessageBoxIcon.Stop)

                    Me.FTBarcodeNo.Focus()
                    Me.FTBarcodeNo.SelectAll()

                End If

            End If

            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private _TileGroup As DevExpress.XtraEditors.TileGroup

    Private Sub CreateGroupDefect(ByVal _FNSendSuplType As String)
        Try
            Dim _Cmd As String = ""
            Try
                TileControl.Groups.Remove(_TileGroup)
                While (TileControl.Groups.Count > 0)
                    TileControl.Groups.RemoveAt(0)
                End While

            Catch ex As Exception
            End Try
            TileControl.ShowGroupText = True

            _Cmd = "  SELECT    distinct CASE WHEN D.FNSubQCType = 0 Then  L.FTName Else  T.FTNameSub END AS FTName ,D.FNSubQCType   ,CASE WHEN  D.FNSubQCType  = 0 then D.FNSendSuplType  else 0 end FNSendSuplType "
            _Cmd &= vbCrLf & ",CASE WHEN D.FNSubQCType = 0 Then  L.FNSeq Else T.FNSeq END AS  FNSeq"
            _Cmd &= vbCrLf & "FROM   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "(SELECT   -1 AS FNSeq ,  FNListIndex"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTNameTH as FTName "
            Else
                _Cmd &= vbCrLf & ", FTNameEN as FTName "
            End If

            _Cmd &= vbCrLf & "FROM   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTListName ='FNSendSuplType' ) AS L ON D.FNSendSuplType = L.FNListIndex"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN ("
            _Cmd &= vbCrLf & "SELECT  FNListIndex AS FNSeq,  FNListIndex "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTNameTH as FTNameSub"
            Else
                _Cmd &= vbCrLf & ", FTNameEN as FTNameSub"
            End If

            _Cmd &= vbCrLf & "FROM      " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTListName ='FNSubQCType'"
            _Cmd &= vbCrLf & ") AS T ON D.FNSubQCType = T.FNListIndex"
            _Cmd &= vbCrLf & "WHERE  D.FNSendSuplType in (" & _FNSendSuplType & ")"
            _Cmd &= vbCrLf & "Order by FNSeq"

            Dim _oGDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            For Each G As DataRow In _oGDt.Rows
                _TileGroup = New DevExpress.XtraEditors.TileGroup
                _TileGroup.Text = "" & HI.UL.ULF.rpQuoted(G!FTName.ToString)
                TileControl.Groups.Add(_TileGroup)
                TileControl.ItemSize = 80

                _Cmd = "  SELECT     FNHSysQCSuplDetailId, FNSendSuplType, FNSubQCType, FTQCSupDetailCode "

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", FTQCSupDetailNameTH  AS FTQASupDetailName"
                Else
                    _Cmd &= vbCrLf & ", FTQCSupDetailNameEN  AS FTQASupDetailName"
                End If
                _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail WITH(NOLOCK) "
                If Integer.Parse(G!FNSubQCType.ToString) = 0 Then
                    _Cmd &= vbCrLf & "WHERE FNSendSuplType =" & G!FNSendSuplType.ToString
                    _Cmd &= vbCrLf & "And FNSubQCType=" & Integer.Parse(G!FNSubQCType.ToString)
                Else
                    _Cmd &= vbCrLf & "WHERE  FNSubQCType=" & Integer.Parse(G!FNSubQCType.ToString)
                End If

                Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

                For Each R As DataRow In _oDt.Rows

                    Dim _i As New DevExpress.XtraEditors.TileItem
                    _i.AllowAnimation = True
                    _i.BackgroundImageScaleMode = TileItemImageScaleMode.ZoomInside

                    _i.AppearanceItem.Normal.BackColor = Color.DodgerBlue
                    _i.AppearanceItem.Normal.BorderColor = Color.LightBlue
                    _i.AppearanceItem.Normal.ForeColor = Color.Black
                    _i.ItemSize = TileItemSize.Wide
                    _i.ContentAnimation = TileItemContentAnimationType.ScrollLeft
                    _i.Name = R!FTQCSupDetailCode.ToString
                    '_i.Text = R!FTQASupDetailName.ToString
                    _i.Text = R!FTQCSupDetailCode.ToString

                    _i.Id = CInt(R!FNHSysQCSuplDetailId)
                    Dim Elmt As DevExpress.XtraEditors.TileItemElement
                    Elmt = New DevExpress.XtraEditors.TileItemElement
                    'Elmt.Text = R!FTQCSupDetailCode.ToString
                    Elmt.Text = R!FTQASupDetailName.ToString
                    Elmt.TextAlignment = TileItemContentAlignment.BottomLeft
                    _i.Elements.Add(Elmt)
                    _TileGroup.Items.Add(_i)
                Next
            Next
            AddHandler TileControl.RightItemClick, AddressOf TileControl_RightItemClick
            AddHandler TileControl.ItemClick, AddressOf TileControl_ItemClick
        Catch ex As Exception

        End Try
    End Sub


    Private Sub TileControl_ItemClick(sender As Object, e As TileItemEventArgs) Handles TileControl.ItemClick
        Try
            If Me.FNQuantity.Value <= 0 Then
                Exit Sub
            End If
            If e.Item.Checked = False Then
                If (_StateSave) Then
                    Me.FNQCActualQty.Value += +1
                    _StateSave = False
                End If
                Call _SaveData()
                Call _AddDefect(e.Item.Id.ToString)
                e.Item.Checked = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TileControl_RightItemClick(sender As Object, e As TileItemEventArgs) Handles TileControl.RightItemClick
        Try
            If Me.FNQuantity.Value <= 0 Then
                Exit Sub
            End If
            If e.Item.Checked = True Then
                Call _RemoveDefect(e.Item.Id.ToString)
                e.Item.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetDefectQty() As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select FNDefectQty From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            Return Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function _SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect"
            _Cmd &= vbCrLf & "Set FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ", FDUpdDate =" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ", FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ", FNDefectQty=" & Double.Parse(Me.FNQCActualQty.Value)
            _Cmd &= vbCrLf & ", FTRemark='" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
            _Cmd &= vbCrLf & ", FTRFIDNo='" & HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text) & "'"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            '_Cmd &= vbCrLf & "AND FDDateTrans=" & HI.UL.ULDate.FormatDateDB
            If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect ( FTInsUser, FDInsDate, FTInsTime,   FTBarcodeSendSuplNo, FNDefectQty, FTRemark, FTRFIDNo )"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                _Cmd &= vbCrLf & "," & Double.Parse(Me.FNQCActualQty.Value)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text) & "'"
                '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                    Return False
                End If
            End If

            ' Set Data for HyperActive For API6 & API10
            If _HyperActive And _TypeQCScan <> "" Then
                If (checkColorWaySize(HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text), HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text))) Then
                    _Cmd = "EXEC " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".[dbo].[SP_CREATE_QADATA_FOR_HYPERACTIVE] "
                    _Cmd &= vbCrLf & "@FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                    _Cmd &= vbCrLf & ", @FNDefectQty = " & Double.Parse(Me.FNQCActualQty.Value)
                    _Cmd &= vbCrLf & ", @User = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    If _TypeQCScan = "1" Then ' Scan BoxNo get RFID Form API4
                        _Cmd &= vbCrLf & ", @FTRFIDNo = '" & GetRFIDNoByBoxNo(HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text)) & "'"
                        _Cmd &= vbCrLf & ", @BoxNo = '" & HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text) & "'"
                    ElseIf _TypeQCScan = "2" Then 'Scan RFID get BoxNo From API4
                        _Cmd &= vbCrLf & ", @FTRFIDNo = '" & HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text) & "'"
                        _Cmd &= vbCrLf & ", @BoxNo = '" & GetBoxNoByRFID(HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text)) & "'"
                    End If
                    If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                        Return False
                    End If
                End If
            End If
            ' End Set Data for HyperActive
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub _AddDefect(ByVal _DetailId As Integer)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNHSysQCSuplDetailId=" & _DetailId
            _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            If _oDt.Rows.Count > 0 Then Exit Sub

            _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail"
            _Cmd &= " ( FTInsUser, FDInsDate, FTInsTime, FTBarcodeSendSuplNo, FNHSysQCSuplDetailId,FNSeq)"
            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "," & _DetailId
            _Cmd &= vbCrLf & "," & Me.FNQCActualQty.Value
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _RemoveDefect(ByVal _DetailId As Integer)
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNHSysQCSuplDetailId=" & _DetailId
            _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            _Cmd = "Select Top 1 FNHSysQCSuplDetailId From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            If Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")) <= 0 Then
                _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect"
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                _StateSave = True
                Me.FNQCActualQty.Value = 0
            Else
                _Cmd = "Select Top 1 FNHSysQCSuplDetailId From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail WITH(NOLOCK)  "
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
                If Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")) <= 0 Then
                    _StateSave = True
                    Me.FNQCActualQty.Value = (Me.FNQCActualQty.Value - 1)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _DelDefect()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            _Cmd = "Select Top 1 FNHSysQCSuplDetailId From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            If Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")) <= 0 Then
                _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect"
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                '_Cmd &= vbCrLf & "And FDDateTrans=" & HI.UL.ULDate.FormatDateDB
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerifyData()
        Try
            If Me.FTBarcodeNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTBarcodeBundleNo_lbl.Text)
                Me.FTBarcodeNo.Focus()
                Return False
            End If
            If Me.FTOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTBarcodeBundleNo_lbl.Text)
                Me.FTBarcodeNo.Focus()
                Return False
            End If
            If Me.FTRfidNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTRfidNo_lbl.Text)
                Me.FTRfidNo.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function checkQCBefore(_BarCodeNo As String) As String
        Try
            Dim _Cmd As String = "" : Dim SType As String = ""
            _Cmd = "Select * from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..fn_get_QCBefore('" & _BarCodeNo & "')"
            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _dt.Rows
                If SType <> "" Then SType &= ","
                SType &= "" & R!FNSendSuplType
            Next
            Return SType
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' ---------- For HyperActive Check Same Colorway and Size ---------- 
    Private Function checkColorWaySize(_RfidNo As String, _BarCodeNo As String) As String
        Try
            Dim _Cmd As String = ""
            Dim IsMatch As Boolean = False
            _Cmd = "SELECT DISTINCT bd.FTColorway, bd.FTSizeBreakDown "
            _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect AS d WITH (NOLOCK)"

            _Cmd &= vbCrLf & "OUTER APPLY(SELECT b.FTBarcodeBundleNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODBarcode_SendSupl AS b WITH (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE b.FTBarcodeSendSuplNo = d.FTBarcodeSendSuplNo) AS b"

            _Cmd &= vbCrLf & "OUTER APPLY(SELECT bd.FTColorway, bd.FTSizeBreakDown FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBundle As bd With (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE b.FTBarcodeBundleNo = bd.FTBarcodeBundleNo) AS bd"

            _Cmd &= vbCrLf & "WHERE d.FTRFIDNo <> ''"
            _Cmd &= vbCrLf & "And d.FTRFIDNo = '" & _RfidNo & "'"
            _Cmd &= vbCrLf
            _Cmd &= vbCrLf & "UNION"
            _Cmd &= vbCrLf
            _Cmd &= vbCrLf & "Select DISTINCT bd.FTColorway, bd.FTSizeBreakDown "

            _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODBarcode_SendSupl AS b WITH (NOLOCK)"

            _Cmd &= vbCrLf & "OUTER APPLY(SELECT bd.FTColorway, bd.FTSizeBreakDown, bd.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBundle AS bd WITH (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE b.FTBarcodeBundleNo =  bd.FTBarcodeBundleNo) AS bd"

            _Cmd &= vbCrLf & "WHERE b.FTBarcodeSendSuplNo =  '" & _BarCodeNo & "'"
            '_Cmd &= vbCrLf & "WHERE b.FTBarcodeBundleNo =  '" & _BarCodeNo & "'"
            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            If (_dt.Rows.Count <= 1) Then
                IsMatch = True
            Else
                HI.MG.ShowMsg.mInfo("Color Way & Size Not Match..", 2010061453, Me.Text, , MessageBoxIcon.Error)
                ClearForm()
            End If
            Return IsMatch
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerifyData() Then
                Dim _State As Boolean
                Dim _msg As String = GetQtyMsg(_State)
                'If Not (_State) Then
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, _msg.ToString, Me.Text) = True Then
                    If _SaveData() Then
                        _StateSave = True
                    End If
                    Call CreateGroupDefect(_Type)
                Else
                End If
                'Else
                '    If _SaveData() Then
                '        _StateSave = True
                '    End If
                '    Call CreateGroupDefect(_FNSendSuplType)
                'End If
            End If
            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetQtyMsg(ByRef _State As Boolean) As String
        Try
            _State = False
            'ส่งปัก
            Dim _Cmd As String = ""
            Dim _EmQty, _Scr, _Fab, _Acc, _heat As Double
            _Cmd = "SELECT COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSendSuplType = 0"
            _Cmd &= vbCrLf & "and D.FNSubQCType = 0"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _EmQty = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            'ส่งพิมพ์
            _Cmd = "SELECT COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSendSuplType = 1"
            _Cmd &= vbCrLf & "and D.FNSubQCType = 0"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _Scr = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            'ส่งรีด
            _Cmd = "SELECT COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSendSuplType = 2"
            _Cmd &= vbCrLf & "and D.FNSubQCType = 0"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _heat = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            'Fabric
            _Cmd = "SELECT COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE     (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSubQCType = 1"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _Fab = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            'Acc
            _Cmd = "SELECT COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSubQCType = 2"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _Acc = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            If (_EmQty > 0 Or _Scr > 0 Or _heat > 0) And _Fab > 0 And _Acc > 0 Then _State = True

            Dim _Return As String = ""
            _Return = "" & Me.FTEmbroidDefectQty_lbl.Text & " " & _EmQty.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTScreenDefectQty_lbl.Text & " " & _Scr.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTHeatDefectQty_lbl.Text & " " & _heat.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTFabricDefectQty_lbl.Text & " " & _Fab.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTAccessoryDefectQty_lbl.Text & " " & _Acc.ToString & " " & Me.FTUnitDefect_lbl.Text

            Return _Return.ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub wQCSendSupl_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            Me.FTRfidNo.Focus()
            Me.FTBarcodeNo.SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wQCSendSupl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _StateSave = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            Try
                Call _DelDefect()
                If Me.FNQCActualQty.Value >= 1 Then
                    Me.FNQCActualQty.Value = Me.FNQCActualQty.Value - 1
                End If
                Call CreateGroupDefect(_Type)
                _StateSave = True
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            _StateSave = True
            ClearForm()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmAcceptqc_Click(sender As Object, e As EventArgs)
        Try
            With _wAccept
                .Data = LoadOnhandFG()
                .ShowDialog()
                If (.Process) Then
                    If .Data.Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    If _SaveDataAccept(.Data) Then
                        HI.MG.ShowMsg.mInfo("Accept QC SendSupl Success.", 1609161057, Me.Text, "", MessageBoxIcon.Information)
                    End If
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Function LoadOnhandFG() As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT '0' AS FTSelect ,  R.FTRcvSuplNo, R.FTBarcodeSendSuplNo, B.FTBarcodeBundleNo, LEFT( B.FTOrderProdNo , len( B.FTOrderProdNo) -4 )  as FTOrderNo ,B.FNQuantity "
            _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.V_TPRODTBundle_MainBarcode As B With (NOLOCK) "
            _Cmd &= vbCrLf & "RIGHT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODBarcode_SendSupl AS BS WITH (NOLOCK) ON B.FTOrderProdNo = BS.FTOrderProdNo AND B.FTBarcodeBundleNo = BS.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & "RIGHT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTReceiveSupl_Barcode As R With (NOLOCK) On BS.FTBarcodeSendSuplNo = R.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & "WHERE  (R.FTBarcodeSendSuplNo NOT IN"
            _Cmd &= vbCrLf & "  (SELECT FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "  FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect))  "
            _Cmd &= vbCrLf & " and Isnull(B.FTBarcodeBundleNo,'')  <> '' "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Function _SaveDataAccept(_odt As DataTable) As Boolean
        Try
            Dim _Cmd As String = ""

            For Each R As DataRow In _odt.Select("FTSelect = '1'")
                _Cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect"
                _Cmd &= vbCrLf & "Set FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNDefectQty=0"
                _Cmd &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Cmd &= vbCrLf & ",FTRfidNo='" & HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text) & "'"
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                '_Cmd &= vbCrLf & "AND FDDateTrans=" & HI.UL.ULDate.FormatDateDB
                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                    _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect ( FTInsUser, FDInsDate, FTInsTime,   FTBarcodeSendSuplNo, FNDefectQty, FTRemark ,FTRfidNo )"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString)) & "'"
                    _Cmd &= vbCrLf & ",0"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRfidNo.Text) & "'"
                    '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                        Return False
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function IsHyperActive() As Boolean
        Try
            Dim _Cmd As String = ""

            _Cmd = "SELECT Top 1  FTCfgData "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTCfgName ='HyperActive' AND FTCfgData = '1'"

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function IsTypeQCScan() As String
        Try
            Dim _Cmd As String = ""
            'Dim _TypeQCScan As String = ""
            _Cmd = "SELECT Top 1 FTCfgData "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTCfgName ='HyperActive_QCScan'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")

            'Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetRFIDNoByBoxNo(_BoxNo As String) As String
        Try
            Dim _Cmd As String = ""
            'Dim _TypeQCScan As String = ""
            _Cmd = "SELECT Top 1 a.FTBoxRfId "

            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HYPERACTIVE) & ".dbo.TSMGtoWisdom_Staging AS a  WITH (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE a.FTBoxBarCode = '" & _BoxNo & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HYPERACTIVE, "0")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetBoxNoByRFID(_RFIDNo As String) As String
        Try
            Dim _Cmd As String = ""
            'Dim _TypeQCScan As String = ""
            _Cmd = "SELECT Top 1 a.FTBoxBarCode "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HYPERACTIVE) & ".dbo.TSMGtoWisdom_Staging AS a  WITH (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE a.FTBoxRfId = '" & _RFIDNo & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HYPERACTIVE, "0")
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Function ClearForm() As String
        Try
            TileControl.Groups.Remove(_TileGroup)
            While (TileControl.Groups.Count > 0)
                TileControl.Groups.RemoveAt(0)
            End While
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
        Return ""
    End Function




End Class
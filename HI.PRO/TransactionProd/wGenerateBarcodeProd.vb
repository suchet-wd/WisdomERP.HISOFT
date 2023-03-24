Imports DevExpress.XtraGrid.Views.Grid

Public Class wGenerateBarcodeProd

    Private _StateSetSelectBySelect As Boolean = True
    Private _StateSetSelectAll As Boolean = True

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


#Region "Init Control"
    Private Sub InitGrid()
        With ogvlaycut
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvbarcode
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvbrcodesupl
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvbrcodesingle
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

    End Sub
#End Region

#Region "Procedure"

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        otbjobprod.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTOrderProdNo  "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        _Qry &= vbCrLf & "  Order By FTOrderProdNo  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FTOrderProdNo.ToString
                .Text = R!FTOrderProdNo.ToString
            End With

            otbjobprod.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbjobprod.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()

        _Spls.Close()
    End Sub

    Private Sub LoadOrderProdDetailInfo(ByVal Key As Object)
        Dim _Qry As String
        Dim dt As DataTable

        _Qry = "SELECT       '1' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq, A.FTColorway, A.FTSizeBreakDown, A.FNQuantity"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A  WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "' AND ISNULL(A.FTStateGenBarcode,'') <>'1'  "
        _Qry &= vbCrLf & " Order By  A.FNBunbleSeq "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogclaycut.DataSource = dt.Copy

        _Qry = "SELECT  '0' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq,  CASE WHEN ISNULL(A.FTColorwayNew,'') ='' THEN A.FTColorway ELSE ISNULL(A.FTColorwayNew,'') END AS FTColorway, A.FTSizeBreakDown, A.FNQuantity, CASE WHEN ISNULL(A.FTColorwayNew,'') <> '' OR ISNULL(A.FTChangeToLineItemNo,'') <>'' THEN '1' ELSE '0' END AS FTStateChange"
        _Qry &= vbCrLf & ",ISNULL(("
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "  SELECT TOP 1 "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "   C.FTMarkNameTH"
        Else
            _Qry &= vbCrLf & "   C.FTMarkNameEN "
        End If

        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B WITH(NOLOCK)  ON AA.FTLayCutNo = B.FTLayCutNo INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C WITH(NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
        _Qry &= vbCrLf & " WHERE  (AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "),'') AS FTMarkName"
        _Qry &= vbCrLf & " , CASE WHEN ISNULL(A.FTChangeToLineItemNo,'') ='' THEN A.FTPOLineItemNo ELSE ISNULL(A.FTChangeToLineItemNo,'') END AS  FTPOLineItemNo"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A  WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "' AND ISNULL(A.FTStateGenBarcode,'') ='1'  "
        _Qry &= vbCrLf & " Order By  A.FNBunbleSeq "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcbarcode.DataSource = dt.Copy

        _Qry = "  SELECT  '0' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq"
        _Qry &= vbCrLf & " , A.FTColorway, A.FTSizeBreakDown, A.FNQuantity"
        _Qry &= vbCrLf & " 	, B.FTBarcodeSendSuplNo, P.FTPartCode "
        _Qry &= vbCrLf & "   , S.FTSuplCode"
        _Qry &= vbCrLf & " ,B.FNSendSuplType  "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , P.FTPartNameTH AS FTPartName "
            _Qry &= vbCrLf & " , S.FTSuplNameTH AS FTSuplName "
            _Qry &= vbCrLf & " , LN.FTNameTH AS FTSendSuplName "
        Else
            _Qry &= vbCrLf & " , P.FTPartNameEN AS FTPartName "
            _Qry &= vbCrLf & " , S.FTSuplNameEN AS FTSuplName "
            _Qry &= vbCrLf & " , LN.FTNameEN AS FTSendSuplName "
        End If
        _Qry &= vbCrLf & " ,A.FTPOLineItemNo"

        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK) ON B.FNHSysPartId = P.FNHSysPartId INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & " 	ON  B.FNSendSuplType = LN.FNListIndex "


        _Qry &= vbCrLf & "  outer apply(select  AX.FTMainBarcodeBundleNo AS FTBarcodeBundleNo ,AX.FTPOLineItemNo,MIN(AX.FNBunbleSeq) As FNBunbleSeq, AX.FTColorway, AX.FTSizeBreakDown, SUM(AX.FNQuantity) AS FNQuantity "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS AX WITH(NOLOCK) WHERE AX.FTMainBarcodeBundleNo = B.FTBarcodeBundleNo "
        _Qry &= vbCrLf & "  Group BY AX.FTMainBarcodeBundleNo,AX.FTPOLineItemNo,AX.FTColorway, AX.FTSizeBreakDown  "
        _Qry &= vbCrLf & "  ) AS A"


        _Qry &= vbCrLf & " WHERE B.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "'"
        _Qry &= vbCrLf & "  ORDER BY  B.FTBarcodeSendSuplNo"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcbrcodesupl.DataSource = dt.Copy

        _Qry = "     SELECT   '0' AS FTSelect,  A.FTBarcodeBundleNo, A.FNBunbleSeq, A.FTColorway"
        _Qry &= vbCrLf & "  , A.FTSizeBreakDown, A.FNQuantity, B.FTBarcodeHeatNo"
        _Qry &= vbCrLf & "  , B.FNOperationSeq, C.FTOperationCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , C.FTOperationNameTH AS FTOperationName "
        Else
            _Qry &= vbCrLf & " , C.FTOperationNameEN AS FTOperationName "
        End If
        _Qry &= vbCrLf & " ,A.FTPOLineItemNo"

        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SingleCon AS B WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS C ON B.FNHSysOperationId = C.FNHSysOperationId"

        _Qry &= vbCrLf & "  outer apply(select   AX.FTMainBarcodeBundleNo AS FTBarcodeBundleNo ,AX.FTPOLineItemNo,MIN(AX.FNBunbleSeq) As FNBunbleSeq, AX.FTColorway, AX.FTSizeBreakDown, SUM(AX.FNQuantity) AS FNQuantity "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS AX WITH(NOLOCK) WHERE AX.FTMainBarcodeBundleNo = B.FTBarcodeBundleNo "
        _Qry &= vbCrLf & "  Group BY AX.FTMainBarcodeBundleNo,AX.FTPOLineItemNo,AX.FTColorway, AX.FTSizeBreakDown  "
        _Qry &= vbCrLf & "  ) AS A"

        _Qry &= vbCrLf & " WHERE B.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "'"
        _Qry &= vbCrLf & "  ORDER BY  B.FTBarcodeHeatNo"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcbrcodesingle.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Function GenerateBarcode() As Boolean

        Dim _Qry As String = ""
        Dim dt As DataTable
        Dim _BarcodeHeat As String = ""
        Dim _BarcodeSupl As String = ""
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In CType(ogclaycut.DataSource, DataTable).Select("FTSelect='1'")

                _Qry = "   SELECT        A.FTOrderProdNo, C.FNOperationState, C.FNHSysOperationId, C.FNSeq,C.FNSeq "
                _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK)  INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS C  WITH(NOLOCK)  ON A.FTOrderProdNo = C.FTOrderProdNo"
                _Qry &= vbCrLf & "  WHERE  (A.FTOrderProdNo =  N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "') "
                _Qry &= vbCrLf & "  AND   (C.FNOperationState = 1) "
                _Qry &= vbCrLf & "  ORDER BY C.FNSeq "

                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                If dt.Rows.Count <= 0 Then

                    _Qry = " SELECT        A.FTOrderProdNo, C.FNOperationState, C.FNHSysOperationId,C.FNSeq "
                    _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A WITH(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B  WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS C WITH(NOLOCK) ON B.FNHSysStyleId = C.FNHSysStyleId"
                    _Qry &= vbCrLf & "  WHERE  (A.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "') "
                    _Qry &= vbCrLf & "  AND   (C.FNOperationState = 1) "
                    _Qry &= vbCrLf & "  ORDER BY C.FNSeq "

                    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                End If

                For Each Rx As DataRow In dt.Rows
                    _BarcodeHeat = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SingleCon", "", False, HI.ST.SysInfo.CmpRunID & "H")

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SingleCon(FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ", FTBarcodeHeatNo, FNOperationSeq, FTBarcodeBundleNo, FNHSysCmpId, FNHSysOperationId,FTOrderProdNo)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarcodeHeat) & "'"
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNSeq.ToString)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNHSysOperationId.ToString)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next

                _Qry = "   SELECT        FTOrderProdNo, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo,FTSendSuplRef  "
                _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl_Detail AS A  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "  WHERE  (FTOrderProdNo =  N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "') "
                _Qry &= vbCrLf & "  AND   (FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "') "
                _Qry &= vbCrLf & "  ORDER BY FNSendSuplType "

                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                For Each Rx As DataRow In dt.Rows

                    Select Case Integer.Parse(Val(Rx!FNSendSuplType.ToString))
                        Case 0
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "E")
                        Case 1
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "P")
                        Case 2
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "H")
                        Case 3
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "L")
                        Case 4
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "D")
                        Case 5
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "W")
                        Case 6
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "N")
                        Case Else
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "O")
                    End Select

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl(FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ",FTBarcodeSendSuplNo, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo, FNHSysCmpId,FTOrderProdNo,FTSendSuplRef)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarcodeSupl) & "'"
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNHSysPartId.ToString)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNSendSuplType.ToString)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNHSysSuplId.ToString)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTSendSuplRef.ToString) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle "
                _Qry &= vbCrLf & " SET  FTStateGenBarcode='1'"
                _Qry &= vbCrLf & ",FTGenBarcodeBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & ",FDGenBarcodeDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTGenBarcodeTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'  "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Try

                _Qry = " UPDATE A SET	FTStatePrintBarcode='1' "
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                _Qry &= vbCrLf & "     INNER JOIN "
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & "   SELECT XA.FTOrderNo, XA.FTSubOrderNo"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS XB WITH(NOLOCK) INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS XA WITH(NOLOCK) ON XB.FTOrderProdNo = XA.FTOrderProdNo"
                _Qry &= vbCrLf & "   WHERE (XA.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "') AND XB.FTStateGenBarcode ='1' "
                _Qry &= vbCrLf & "   GROUP BY XA.FTOrderNo, XA.FTSubOrderNo"
                _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "    WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' AND ISNULL(FTStatePrintBarcode,'')<>'1'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function


    Private Function DeleteBarcode(BarKay As String) As Boolean

        Dim _Qry As String = ""
        Dim dt As DataTable
        Dim _BarcodeHeat As String = ""
        Dim _BarcodeSupl As String = ""
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SingleCon"
            _Qry &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarKay) & "' "

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl"
            _Qry &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarKay) & "' "

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle "
            _Qry &= vbCrLf & " SET  FTStateGenBarcode='0'"
            _Qry &= vbCrLf & ",FTDeleteBarcodeBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & ",FDDeleteBarcodeDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & ",FTDeleteBarcodeTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarKay) & "'  "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            _Qry = " DELETE BUNDLE NO '" & HI.UL.ULF.rpQuoted(BarKay) & "' "

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function
#End Region

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderProdDataInfo(FTOrderNo.Text)
        End If
    End Sub

    Private Sub wGenerateBarcode_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Call InitGrid()
    End Sub

    Private Sub otbjobprod_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbjobprod.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbjobprod_SelectedPageChanged), New Object() {sender, e})
            Else

                Me.ogclaycut.DataSource = Nothing
                Me.ogcbarcode.DataSource = Nothing
                Me.ogcbrcodesupl.DataSource = Nothing
                Me.ogcbrcodesingle.DataSource = Nothing

                If Not (otbjobprod.SelectedTabPage Is Nothing) Then
                    Call LoadOrderProdDetailInfo(otbjobprod.SelectedTabPage.Name.ToString)
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call FTOrderNo_EditValueChanged(FTOrderNo, New System.EventArgs)
    End Sub

    Private Sub ocmgeneratebarcodewip_Click(sender As Object, e As EventArgs) Handles ocmgeneratebarcodewip.Click
        CType(ogclaycut.DataSource, DataTable).AcceptChanges()

        If CType(ogclaycut.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Generate Barcode ใช่หรือไม่ ?", 1505300001) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Generating....Barcode , Please Wait...")
                If Me.GenerateBarcode() Then
                    Call LoadOrderProdDetailInfo(otbjobprod.SelectedTabPage.Name.ToString)
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("Generate Barcode Complete..", 1405300002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการ Generate Barcode ได้ กรุณาทำการติดต่อ Admin...", 1405300003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลมัดงานสำการ Generate Barcode", 1405300004, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub ocmpreviewbarcodewipall_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipall.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _Qry As String = ""

            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name

                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                Case otpbarcodesendsupl.Name

                    Try

                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    Catch ex As Exception
                    End Try

                    _ReportName = "BarcodeSendSupp.rpt"

                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"

                Case otpbarcodeheat.Name

                    Try

                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    Catch ex As Exception
                    End Try

                    _ReportName = "BarCodeSingleCon.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
            End Select

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()
            End With
            'Select Case otbdetail.SelectedTabPage.Name
            '    Case otpbarcodeheat.Name

            '        Dim _Qry As String = ""
            '        Try
            '            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempBarcode_SingleCon WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
            '        Catch ex As Exception

            '        End Try


            'End Select
        End If
    End Sub

    Private Sub ocmdeletebarcodeprod_Click(sender As Object, e As EventArgs) Handles ocmdeletebarcodeprod.Click
        Try
            With CType(ogcbarcode.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length > 0 Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ ลบ Barcode ใช่หรือไม่ ?", 1409150001) = True Then
                        Dim _CountDelete As Integer = 0
                        Dim _CountNotDelete As Integer = 0
                        Dim _Spls As New HI.TL.SplashScreen("Deleting....Barcode , Please Wait...")
                        Dim _Qry As String = ""


                        Dim _BarKey As String = ""
                        For Each R As DataRow In .Select("FTSelect='1'")
                            _BarKey = R!FTBarcodeBundleNo.ToString

                            _Qry = "SELECT TOP 1 FTOrderProdNo"
                            _Qry &= vbCrLf & "  FROM"
                            _Qry &= vbCrLf & " (SELECT  TOP 1   B.FTOrderProdNo"
                            _Qry &= vbCrLf & "  	FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS A WITH(NOLOCK) INNER JOIN"
                            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH(NOLOCK)  ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo INNER JOIN"
                            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS C WITH(NOLOCK)  ON B.FTSendSuplRef = C.FTSendSuplRef AND B.FTOrderProdNo = C.FTOrderProdNo AND B.FNHSysPartId = C.FNHSysPartId AND B.FNSendSuplType = C.FNSendSuplType AND "
                            _Qry &= vbCrLf & "    B.FNHSysSuplId = C.FNHSysSuplId INNER JOIN"
                            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BC WITH(NOLOCK) ON B.FTBarcodeBundleNo = BC.FTBarcodeBundleNo"
                            _Qry &= vbCrLf & " 	 WHERE   B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_BarKey) & "'"
                            _Qry &= vbCrLf & "  UNION"
                            _Qry &= vbCrLf & " 	SELECT    TOP 1     B.FTOrderProdNo"
                            _Qry &= vbCrLf & " 	FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Single_Detail AS A  WITH(NOLOCK)  INNER JOIN"
                            _Qry &= vbCrLf & " 	 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SingleCon AS B  WITH(NOLOCK)  ON A.FTBarcodeNo = B.FTBarcodeHeatNo INNER JOIN"
                            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BC   WITH(NOLOCK) ON B.FTBarcodeBundleNo = BC.FTBarcodeBundleNo"
                            _Qry &= vbCrLf & " 	 WHERE   B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_BarKey) & "'"
                            _Qry &= vbCrLf & "     UNION"
                            _Qry &= vbCrLf & " 		SELECT   TOP 1    B.FTOrderProdNo"
                            _Qry &= vbCrLf & " 	FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A  WITH(NOLOCK) INNER JOIN"
                            _Qry &= vbCrLf & " 	 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B  WITH(NOLOCK)  ON A.FTBarcodeNo = B.FTBarcodeBundleNo "
                            _Qry &= vbCrLf & " 	 WHERE   B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_BarKey) & "'"
                            _Qry &= vbCrLf & "     UNION"
                            _Qry &= vbCrLf & " 		SELECT   TOP 1    B.FTOrderProdNo"
                            _Qry &= vbCrLf & " 	FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTSendSupl_Barcode  AS A  WITH(NOLOCK) INNER JOIN"
                            _Qry &= vbCrLf & " 	 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B  WITH(NOLOCK)   ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo "
                            _Qry &= vbCrLf & " 	 WHERE   B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_BarKey) & "'"
                            _Qry &= vbCrLf & " 	) AS A"


                            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                                If Me.DeleteBarcode(_BarKey) Then
                                    _CountDelete = _CountDelete + 1
                                End If
                            Else
                                _CountNotDelete = _CountNotDelete + 1
                            End If

                        Next

                        Call LoadOrderProdDetailInfo(otbjobprod.SelectedTabPage.Name.ToString)

                        Try

                            _Qry = " UPDATE A SET	FTStatePrintBarcode='0' "
                            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                            _Qry &= vbCrLf & "     LEFT OUTER JOIN "
                            _Qry &= vbCrLf & "  ("
                            _Qry &= vbCrLf & "   SELECT XA.FTOrderNo, XA.FTSubOrderNo"
                            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS XB WITH(NOLOCK) INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS XA WITH(NOLOCK) ON XB.FTOrderProdNo = XA.FTOrderProdNo"
                            _Qry &= vbCrLf & "   WHERE (XA.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "') AND XB.FTStateGenBarcode ='1' "
                            _Qry &= vbCrLf & "   GROUP BY XA.FTOrderNo, XA.FTSubOrderNo"
                            _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                            _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                            _Qry &= vbCrLf & "    WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' AND B.FTOrderNo Is NULL"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                        Catch ex As Exception
                        End Try

                        _Spls.Close()

                        If _CountDelete > 0 Then
                            HI.MG.ShowMsg.mInfo("Delete Barcode Complete..", 1409150002, Me.Text, _CountDelete.ToString, System.Windows.Forms.MessageBoxIcon.Information)
                        End If

                        If _CountNotDelete > 0 Then
                            HI.MG.ShowMsg.mInfo("Can't Delete Barcode Because Barcode is Used !!!", 14091507722, Me.Text, _CountNotDelete.ToString, System.Windows.Forms.MessageBoxIcon.Information)
                        End If

                    End If
                Else
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูล Barcode มัดงาน ", 1409150004, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End With
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ockselectallbundle_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectallbundle.CheckedChanged
        Try


            Dim _State As String = "0"
            If Me.ockselectallbundle.Checked Then
                _State = "1"
            End If

            With ogclaycut
                If Not (.DataSource Is Nothing) And ogvlaycut.RowCount > 0 Then

                    With ogvlaycut
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ockselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectall.CheckedChanged
        Try

            If _StateSetSelectAll = False Then Exit Sub
            _StateSetSelectBySelect = False
            Me.ockselectallbyselection.Checked = False

            Dim _State As String = "0"
            If Me.ockselectall.Checked Then
                _State = "1"
            End If

            Select Case True
                Case otbdetail.SelectedTabPage.Name = otpbarcodebundle.Name
                    With ogcbarcode
                        If Not (.DataSource Is Nothing) And ogvbarcode.RowCount > 0 Then

                            With ogvbarcode
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                    End With

                Case otbdetail.SelectedTabPage.Name = otpbarcodesendsupl.Name
                    With ogcbrcodesupl
                        If Not (.DataSource Is Nothing) And ogvbrcodesupl.RowCount > 0 Then

                            With ogvbrcodesupl
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                    End With
                Case otbdetail.SelectedTabPage.Name = otpbarcodeheat.Name
                    With ogcbrcodesingle
                        If Not (.DataSource Is Nothing) And ogvbrcodesingle.RowCount > 0 Then

                            With ogvbrcodesingle
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                    End With
            End Select

        Catch ex As Exception

        End Try
        _StateSetSelectBySelect = True
    End Sub

    Private Sub ocmpreviewbarcodewipselect_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipselect.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _RowIndex As Integer = 0
            Dim _Qry As String = ""

            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name

                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"


                    _RowIndex = 0
                    With CType(Me.ogcbarcode.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarCodeBundle.FTBarcodeBundleNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodesendsupl.Name
                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarcodeSendSupp.rpt"

                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"

                    _RowIndex = 0

                    With CType(Me.ogcbrcodesupl.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarSendSup.FTBarcodeSendSuplNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodeheat.Name




                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try



                    _ReportName = "BarCodeSingleCon.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"

                    With CType(Me.ogcbrcodesingle.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_Barcode_SingleCon.FTBarcodeHeatNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

            End Select

            With New HI.RP.Report

                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()

            End With

            'Select Case otbdetail.SelectedTabPage.Name
            '    Case otpbarcodeheat.Name

            '        Dim _Qry As String = ""
            '        Try
            '            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempBarcode_SingleCon WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
            '        Catch ex As Exception

            '        End Try


            'End Select
        End If
    End Sub

    Private Sub ocmpreviewbarcodewipalla4_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipalla4.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _Qry As String = ""
            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name


                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode_Print.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"


                Case otpbarcodesendsupl.Name


                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarcodeSendSupp_Print.rpt"

                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"


                Case otpbarcodeheat.Name





                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarCodeSingleCon_Print.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
            End Select

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()
            End With


            _Qry = " UPDATE A SET FTStatePrintBarcode='1' "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A "
            _Qry &= vbCrLf & " INNER JOIN  (SELECT FTOrderNo, FTSubOrderNo"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"
            _Qry &= vbCrLf & " GROUP BY FTOrderNo, FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            'Select Case otbdetail.SelectedTabPage.Name
            '    Case otpbarcodeheat.Name

            '        Dim _Qry As String = ""
            '        Try
            '            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempBarcode_SingleCon WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
            '        Catch ex As Exception
            '        End Try

            'End Select
        End If
    End Sub

    Private Sub ocmpreviewbarcodewipselecta4_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipselecta4.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _RowIndex As Integer = 0
            Dim _Qry As String = ""

            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name
                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode_Print.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"

                    _RowIndex = 0
                    With CType(Me.ogcbarcode.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarCodeBundle.FTBarcodeBundleNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodesendsupl.Name


                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarcodeSendSupp_Print.rpt"

                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"

                    _RowIndex = 0

                    With CType(Me.ogcbrcodesupl.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarSendSup.FTBarcodeSendSuplNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodeheat.Name





                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarCodeSingleCon_Print.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"

                    With CType(Me.ogcbrcodesingle.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_Barcode_SingleCon.FTBarcodeHeatNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

            End Select

            With New HI.RP.Report

                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()

            End With


            _Qry = " UPDATE A SET FTStatePrintBarcode='1' "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A "
            _Qry &= vbCrLf & " INNER JOIN  (SELECT FTOrderNo, FTSubOrderNo"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"
            _Qry &= vbCrLf & " GROUP BY FTOrderNo, FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            'Select Case otbdetail.SelectedTabPage.Name
            '    Case otpbarcodeheat.Name

            '        Dim _Qry As String = ""
            '        Try
            '            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempBarcode_SingleCon WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
            '        Catch ex As Exception

            '        End Try


            'End Select
        End If
    End Sub

    Private Sub otbdetail_Click(sender As Object, e As EventArgs) Handles otbdetail.Click

    End Sub


    Private Sub TabChenge()
        ocmpreviewbarcodewipalla4.Visible = (otbdetail.SelectedTabPage.Name = otpbarcodebundle.Name)
        ocmpreviewbarcodewipselecta4.Visible = (otbdetail.SelectedTabPage.Name = otpbarcodebundle.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged
        Try
            Call TabChenge()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpreviewpacklist_Click(sender As Object, e As EventArgs) Handles ocmpreviewpacklist.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _RowIndex As Integer = 0

            _ReportName = "Packinglist.rpt"
            _FM = "{TPRODTOrderProd.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"

            _RowIndex = 0
            With CType(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length > 0 Then
                    _FM &= " AND {V_packinglistProduct.FTBarcodeBundleNo} IN ["
                    _RowIndex = 0
                    For Each R As DataRow In .Select("FTSelect='1'")

                        If _RowIndex = 0 Then
                            _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                        Else
                            _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                        End If

                        _RowIndex = _RowIndex + 1

                    Next

                    _FM &= "]"
                End If

            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()
            End With

            Dim _Qry As String = ""
            _Qry = " UPDATE A SET FTStatePrintBarcode='1' "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A "
            _Qry &= vbCrLf & " INNER JOIN  (SELECT FTOrderNo, FTSubOrderNo"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"
            _Qry &= vbCrLf & " GROUP BY FTOrderNo, FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

        End If
    End Sub

    Private Sub ockselectallbyselection_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectallbyselection.CheckedChanged
        Try

            If _StateSetSelectBySelect = False Then Exit Sub
            _StateSetSelectAll = False
            Me.ockselectall.Checked = False

            Dim _State As String = "0"
            If Me.ockselectallbyselection.Checked Then
                _State = "1"
            End If

            Select Case True
                Case otbdetail.SelectedTabPage.Name = otpbarcodebundle.Name
                    With ogcbarcode
                        If Not (.DataSource Is Nothing) And ogvbarcode.RowCount > 0 Then

                            With ogvbarcode
                                For Each i As Integer In .GetSelectedRows()
                                    .SetRowCellValue(i, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                    End With

                Case otbdetail.SelectedTabPage.Name = otpbarcodesendsupl.Name
                    With ogcbrcodesupl
                        If Not (.DataSource Is Nothing) And ogvbrcodesupl.RowCount > 0 Then

                            With ogvbrcodesupl
                                For Each i As Integer In .GetSelectedRows()
                                    .SetRowCellValue(i, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                    End With
                Case otbdetail.SelectedTabPage.Name = otpbarcodeheat.Name
                    With ogcbrcodesingle
                        If Not (.DataSource Is Nothing) And ogvbrcodesingle.RowCount > 0 Then

                            With ogvbrcodesingle
                                For Each i As Integer In .GetSelectedRows()
                                    .SetRowCellValue(i, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If
                    End With
            End Select

        Catch ex As Exception

        End Try
        _StateSetSelectAll = True
    End Sub

    Private Sub otbdetail_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otbdetail.SelectedPageChanging

        Me.ockselectall.Checked = False
        ockselectall.Checked = False
    End Sub

    Private Sub ocmbundlelaypreview_Click(sender As Object, e As EventArgs) Handles ocmbundlelaypreview.Click
        Try
            If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
                Dim _FM As String = ""
                Dim _ReportName As String = ""
                Dim _RowIndex As Integer = 0

                _ReportName = "ReportProdBundlelay.rpt"
                _FM = "{V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                _RowIndex = 0
                With CType(Me.ogcbarcode.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTSelect='1'").Length > 0 Then
                        _FM &= " AND {V_BarCodeBundle.FTBarcodeBundleNo} IN ["
                        _RowIndex = 0
                        For Each R As DataRow In .Select("FTSelect='1'")

                            If _RowIndex = 0 Then
                                _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                            Else
                                _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                            End If

                            _RowIndex = _RowIndex + 1

                        Next

                        _FM &= "]"
                    End If

                End With



                With New HI.RP.Report

                    .FormTitle = Me.Text
                    .ReportFolderName = "Production\"
                    .ReportName = "ReportProdBundlelay.rpt"
                    .Formular = _FM
                    .Preview()

                End With

                Dim _Qry As String = ""
                _Qry = " UPDATE A SET FTStatePrintBarcode='1' "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A "
                _Qry &= vbCrLf & " INNER JOIN  (SELECT FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS X WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"
                _Qry &= vbCrLf & " GROUP BY FTOrderNo, FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvbarcode_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvbarcode.RowStyle
        Try

            With ogvbarcode
                If .GetRowCellValue(e.RowHandle, "FTStateChange") = "1" Then
                    e.Appearance.ForeColor = System.Drawing.Color.HotPink
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class
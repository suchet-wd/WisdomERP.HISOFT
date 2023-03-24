Public Class wFGStockCard 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitGrid()
    End Sub

    Private Sub wFGStockCard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogvdetail.OptionsView.ShowAutoFilterRow = False
        Catch ex As Exception
        End Try
    End Sub

#Region "Command Button"
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Verrify() Then
                Call LoadData()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetail)
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Processing"
    Private Function Verrify() As Boolean
        Try
            Dim _State As Boolean = False
            If Me.FNHSysWHIdFG.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysWHIdFGTo.Text <> "" Then
                _State = True
            End If
            If Me.FTOrderNo.Text <> "" Then
                _State = True
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _State = True
            End If
            If Me.FTStartDate.Text <> "" Then
                _State = True
            End If
            If Me.FTEndDate.Text <> "" Then
                _State = True
            End If
            If Not (_State) Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                Me.FNHSysWHIdFG.Focus()
            End If
            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = " Select ROW_NUMBER() Over(Order by FTWHFGCode ,T.FTOrderNo, FTColorWay, FTSizeBreakDown) as RowSeq ,T.FNSeq ,FTDocumentNo"
            _Cmd &= vbCrLf & ",case when isdate(T.FDTransection) =1 Then convert(nvarchar(10),convert(datetime,T.FDTransection),103) Else '' END AS FDTransection "
            _Cmd &= vbCrLf & ", T.FNHSysWHFGId , T.FTOrderNo , T.FTColorWay , T.FTSizeBreakDown , T.FNQuantity , T.FNQuantityOut,T.FNQuantityIssue"
            _Cmd &= vbCrLf & " ,T.FNQuantity , FG.FTWHFGCode , S.FTStyleCode , O.FTPORef "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",FG.FTWHFGNameTH  as FTWHFGName"
            Else
                _Cmd &= vbCrLf & ",FG.FTWHFGNameEN  as FTWHFGName"
            End If
            _Cmd &= vbCrLf & "From ("
            _Cmd &= vbCrLf & "SELECT  'BF' as FTDocumentNo ,   0 AS FNSeq, FDInsDate AS FDTransection,  FNHSysWHFGId, FTOrderNo, FTColorWay, FTSizeBreakDown, FNQuantity , NULL AS FNQuantityOut , NULL AS FNQuantityIssue"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH (NOLOCK)"
            _Cmd &= vbCrLf & " UNION all"
            _Cmd &= vbCrLf & "SELECT   T.FTTransferFGNo as FTDocumentNo ,  0 AS FNSeq, T.FDDateTransferFG,  T.FNHSysWHIdFGTo , F.FTOrderNo , F.FTColorWay ,F.FTSizeBreakDown"
            _Cmd &= vbCrLf & ", F.FNQuantity , NULL AS FNQuantityOut , NULL AS FNQuantityIssue "
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH(NOLOCK) ON D.FTBarCodeCarton = F.FTBarCodeCarton"
            _Cmd &= vbCrLf & "WHERE isnull(T.FTStateApprove,'0') = '1' "

            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "SELECT      HJ.FTAdjustFGNo as FTDocumentNo ,   0 AS FNSeq,   HJ.FDAdjustFGDate, HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, VA.FTSizeBreakDown, AJ.FNQuantity, NULL AS FNQuantityOut , NULL AS FNQuantityIssue"
            _Cmd &= vbCrLf & "FROM        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG AS HJ WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTAdjustFGNo = AJ.FTAdjustFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FTCustBarcodeNo = VA.FTCustBarcodeNo and AJ.FTOrderNo = VA.FTOrderNo"

            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "Select    FTDocumentNo , 1 AS FNSeq ,min(ZZ.FDIssueFGDate) AS FDIssueFGDate ,ZZ.FNHSysWHFGId , ZZ.FTOrderNo ,ZZ.FTColorway , ZZ.FTSizeBreakDown ,NULL AS FNQuantity , NULL AS FNQuantityOut , sum(ZZ.FNQuantity) AS FNQuantityIssue "
            _Cmd &= vbCrLf & "FROM (Select FTDocumentNo , Z.FTIssueFGNo, Z.FDIssueFGDate, Z.FNHSysWHFGId, Z.FTCustBarcodeNo, Z.FTOrderNo, Z.FTColorway, Z.FTSizeBreakDown, sum(Z.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "FROM (SELECT IH.FTIssueFGNo as FTDocumentNo ,   IH.FDIssueFGDate ,  IH.FTIssueFGNo, IH.FNHSysWHFGId, IV.FTCustBarcodeNo, ID.FTOrderNo, IV.FTColorway, IV.FTSizeBreakDown, ID.FNQuantity  "
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG AS IH WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail AS ID WITH (NOLOCK) ON IH.FTIssueFGNo = ID.FTIssueFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS IV ON ID.FNHSysStyleId = IV.FNHSysStyleId AND ID.FTOrderNo = IV.FTOrderNo"
            _Cmd &= vbCrLf & "and  ID.FTColorway = IV.FTColorway and  ID.FTSizeBreakDown = IV.FTSizeBreakDown"
            _Cmd &= vbCrLf & "  UNION ALL"
            _Cmd &= vbCrLf & "SELECT D.FTDocumentRefNo as FTDocumentNo ,   H.FDInvoiceDate , D.FTDocumentRefNo,  D.FNHSysWHFGId,D.FTBarcodeCustNo, D.FTOrderNo,  D.FTColorway, D.FTSizeBreakDown, - sum(D.FNQuantity) AS FNQuantity "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS H WITH(NOLOCK) "
            _Cmd &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D  WITH(NOLOCK) ON H.FTInvoiceNo = D.FTInvoiceNo"
            _Cmd &= vbCrLf & "where H.FTInvoiceNo like '%INVO%'"
            _Cmd &= vbCrLf & " and D.FTDocumentRefNo is not null"
            _Cmd &= vbCrLf & " group by  H.FDInvoiceDate ,D.FTDocumentRefNo, D.FNHSysWHFGId,D.FTBarcodeCustNo,  D.FTColorway, D.FTSizeBreakDown, D.FTOrderNo, H.FTInvoiceNo"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT RD.FTDocumentRefNo as FTDocumentNo ,   RH.FDReturnFGDate, RD.FTDocumentRefNo  , RH.FNHSysWHFGId, IV.FTCustBarcodeNo, RD.FTOrderNo, IV.FTColorway, IV.FTSizeBreakDown , -RD.FNQuantity"
            _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG AS RH WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG_Detail AS RD WITH (NOLOCK) ON RH.FTReturnFGNo = RD.FTReturnFGNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS IV ON RD.FNHSysStyleId = IV.FNHSysStyleId AND RD.FTOrderNo = IV.FTOrderNo "
            _Cmd &= vbCrLf & "AND RD.FTColorway = IV.FTColorway AND RD.FTSizeBreakDown = IV.FTSizeBreakDown  )AS Z"
            _Cmd &= vbCrLf & " group by FTDocumentNo , Z.FTIssueFGNo,Z.FDIssueFGDate, Z.FNHSysWHFGId, Z.FTCustBarcodeNo, Z.FTOrderNo, Z.FTColorway, Z.FTSizeBreakDown ) AS ZZ "
            _Cmd &= vbCrLf & "Group by FTDocumentNo ,ZZ.FNHSysWHFGId , ZZ.FTOrderNo ,ZZ.FTColorway , ZZ.FTSizeBreakDown "

            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT  S.FTInvoiceNo as FTDocumentNo ,   2 AS FNSeq, S.FDInvoiceDate,  D.FNHSysWHFGId, D.FTOrderNo, D.FTColorway, D.FTSizeBreakDown,  NULL as FNQuantity , D.FNQuantity AS FNQuantityOut , NULL AS FNQuantityIssue"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D WITH (NOLOCK) ON S.FTInvoiceNo = D.FTInvoiceNo"
            '_Cmd &= vbCrLf & "where S.FTInvoiceNo like '%INVI%'"

            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & " SELECT   T.FTTransferFGNo as FTDocumentNo ,  2 AS FNSeq, T.FDDateTransferFG,  T.FNHSysWHIdFG , F.FTOrderNo , F.FTColorWay ,F.FTSizeBreakDown, NULL as FNQuantity , F.FNQuantity AS FNQuantityOut , NULL AS FNQuantityIssue "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH(NOLOCK) ON D.FTBarCodeCarton = F.FTBarCodeCarton"
            _Cmd &= vbCrLf & "WHERE isnull(T.FTStateApprove,'0') = '1' "
            _Cmd &= vbCrLf & " ) AS T  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG AS FG WITH(NOLOCK) ON T.FNHSysWHFGId = FG.FNHSysWHFGId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON T.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId "
            _Cmd &= vbCrLf & "Where T.FNSeq is not null "
            If Me.FNHSysWHIdFG.Text <> "" Then
                _Cmd &= vbCrLf & "and FG.FTWHFGCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdFG.Text) & "'"
            End If
            If Me.FNHSysWHIdFGTo.Text <> "" Then
                _Cmd &= vbCrLf & "and FG.FTWHFGCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdFGTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & "and T.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & "and T.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FTStartDate.Text <> "" Then
                _Cmd &= vbCrLf & "and T.FDTransection >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            End If
            If Me.FTEndDate.Text <> "" Then
                _Cmd &= vbCrLf & "and T.FDTransection <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            End If

            _Cmd &= vbCrLf & "Order by FTOrderNo, FTColorWay, FTSizeBreakDown ,T.FDTransection ,T.FNSeq "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)

            Me.ogcdetail.DataSource = CreateDatatable(_oDt)
            Try
                Me.ogvdetail.ExpandAllGroups()
            Catch ex As Exception
            End Try
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub InitGrid()
        Try
            With Me.ogvdetail
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsMenu.EnableColumnMenu = False
                .OptionsMenu.ShowAutoFilterRowItem = False
                .OptionsFilter.AllowFilterEditor = False
                .OptionsFilter.AllowColumnMRUFilterList = False
                .OptionsFilter.AllowMRUFilterList = False
                .OptionsSelection.MultiSelect = False
            End With

            Dim sFieldGrpSum As String = "FNQuantity|FNQuantityOut|FNQuantityIssue"
            Dim sFieldSum As String = "FNQuantity|FNQuantityOut|FNQuantityIssue"
            With ogvdetail
                .ClearGrouping()
                .ClearDocument()
                .Columns("FTWHFGCode").Group()
                .Columns("FTPORef").Group()
                .Columns("FTOrderNo").Group()
                .Columns("FTStyleCode").Group()
                .Columns("FTColorWay").Group()
                .Columns("FTSizeBreakDown").Group()

                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Function CreateDatatable(dt As DataTable) As DataTable
        Try
            Dim _dt As New DataTable
            With _dt
                .Columns.Add("FDTransection", GetType(String))
                .Columns.Add("FNHSysWHFGId", GetType(Integer))
                .Columns.Add("FTOrderNo", GetType(String))
                .Columns.Add("FTColorWay", GetType(String))
                .Columns.Add("FTSizeBreakDown", GetType(String))
                .Columns.Add("FNQuantity", GetType(Integer))
                .Columns.Add("FNQuantityOut", GetType(Integer))
                .Columns.Add("FNQuantityIssue", GetType(Integer))
                .Columns.Add("FTWHFGCode", GetType(String))
                .Columns.Add("FTWHFGName", GetType(String))
                .Columns.Add("FNQuantityBal", GetType(Integer))
                .Columns.Add("FTStyleCode", GetType(String))
                .Columns.Add("FTDocumentNo", GetType(String))
                .Columns.Add("FTPORef", GetType(String))

            End With

            Dim _StrFilter As String = ""
            Dim _FNQtyBal As Integer = 0 : Dim _FNQty As Integer = 0 : Dim _FNQtyOut As Integer = 0 : Dim _FNQtyIss As Integer = 0
            If Not (dt Is Nothing) Then
                For Each R As DataRow In dt.Select("FDTransection<>''", "FTWHFGCode,FTOrderNo,FTColorway,FTSizeBreakDown ,FDTransection")
                    _StrFilter = "FNHSysWHFGId=" & R!FNHSysWHFGId.ToString & " AND  FTOrderNo='" & R!FTOrderNo.ToString & "' "
                    _StrFilter &= " AND FTColorway='" & R!FTColorway.ToString & "' AND FTSizeBreakDown='" & R!FTSizeBreakDown.ToString & "'"
                    _FNQty = 0 : _FNQtyOut = 0 : _FNQtyIss = 0
                    If IsNumeric(R!FNQuantity.ToString) Then _FNQty = Integer.Parse(R!FNQuantity.ToString)
                    If IsNumeric(R!FNQuantityOut.ToString) Then _FNQtyOut = Integer.Parse(R!FNQuantityOut.ToString)
                    If IsNumeric(R!FNQuantityIssue.ToString) Then _FNQtyIss = Integer.Parse(R!FNQuantityIssue.ToString)

                    If _dt.Select(_StrFilter).Length <= 0 Then
                        _FNQtyBal = Integer.Parse(_FNQty) - (Integer.Parse(_FNQtyOut) + Integer.Parse(_FNQtyIss))
                    Else
                        _FNQtyBal += Integer.Parse(_FNQty) - (Integer.Parse(_FNQtyOut) + Integer.Parse(_FNQtyIss))
                    End If
                    If _FNQty = 0 And _FNQtyOut = 0 And _FNQtyIss = 0 Then
                    Else
                        _dt.Rows.Add(R!FDTransection.ToString, R!FNHSysWHFGId.ToString, R!FTOrderNo.ToString, R!FTColorWay.ToString, R!FTSizeBreakDown.ToString, IIf(_FNQty <> 0, _FNQty, Nothing), _
                                                            IIf(_FNQtyOut <> 0, _FNQtyOut, Nothing), IIf(_FNQtyIss <> 0, _FNQtyIss, Nothing), R!FTWHFGCode.ToString, R!FTWHFGName.ToString, _FNQtyBal, _
                                                            R!FTStyleCode.ToString, R!FTDocumentNo.ToString, R!FTPORef.ToString)
                    End If
                Next
            End If
            Return _dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
 
End Class
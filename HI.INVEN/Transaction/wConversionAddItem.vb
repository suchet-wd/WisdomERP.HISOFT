Public Class wConversionAddItem 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _DocumentNo As String = ""
    Public Property DocumentNo As String
        Get
            Return _DocumentNo
        End Get
        Set(value As String)
            _DocumentNo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _MainObject As Object = Nothing
    Public Property MainObject As Object
        Get
            Return _MainObject
        End Get
        Set(value As Object)
            _MainObject = value
        End Set
    End Property

    Public Function LoadData() As Boolean
        Dim _State As Boolean = False
        Dim _Qry As String = ""
        Dim _dt As DataTable

        FTCheckStateFinish.Checked = False

        _Qry = " SELECT '0' AS FTSelect"
        _Qry &= vbCrLf & "   ,A2.FNHSysRawMatId"
        _Qry &= vbCrLf & "  ,A2.FTPurchaseNo"
        _Qry &= vbCrLf & "  ,A2.FTFabricFrontSize"
        _Qry &= vbCrLf & "   ,A2.FNPrice"

        _Qry &= vbCrLf & "  ,IM.FTRawMatCode ,"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  IM.FTRawMatNameTH AS FTMatDesc,"
        Else
            _Qry &= vbCrLf & "  IM.FTRawMatNameEN AS FTMatDesc,"
        End If

        _Qry &= vbCrLf & "  C.FTRawMatColorCode,"
        _Qry &= vbCrLf & "  S.FTRawMatSizeCode,"
        _Qry &= vbCrLf & "  IM.FNHSysUnitId,"
        _Qry &= vbCrLf & "  U.FTUnitCode "
        _Qry &= vbCrLf & "  ,(A2.FNQuantity) AS FNQuantity"
        '_Qry &= vbCrLf & "  ,(A2.FNQuantity-ISNULL(B.FNQuantity,0)) AS FNQuantity"
        _Qry &= vbCrLf & "   ,(A2.FNQuantity) AS FNUsedQty"
        _Qry &= vbCrLf & "   ,Convert(numeric(18,2),(A2.FNPrice*A2.FNQuantity)) AS FNAmt"
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(B.FNTotalConvQuantity,0) = 0 THEN "
        _Qry &= vbCrLf & "   Convert(numeric(18,4), (A2.FNQuantity) * ISNULL(("
        _Qry &= vbCrLf & " SELECT TOP 1  (UV.FNHSysUnitIdTo / UV.FNRateFrom ) AS FNTotalConv"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UV WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS UT WITH(NOLOCK)  ON UV.FNHSysUnitIdTo = UT.FNHSysUnitId"
        _Qry &= vbCrLf & " WHERE UV.FNHSysConvId =U.FNHSysUnitId AND UT.FTUnitCode = 'PCS'		 "
        _Qry &= vbCrLf & " ),1"
        _Qry &= vbCrLf & "))"
        _Qry &= vbCrLf & " ELSE  ISNULL(B.FNTotalConvQuantity,0) END AS FNTotalConvQuantity"
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(B.FNTotalConvQuantity,0) = 0 THEN '0' ELSE  '1' END AS FTStateConverTo"
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(B.FNTotalConvQuantity,0) = 0 THEN A2.FNQuantity ELSE  ISNULL(B.FNTotalConvQuantity,0) - ISNULL(B.FNQuantityTo,0) END AS FNBalQuantity"
        _Qry &= vbCrLf & ",ISNULL(B.FTStateFinish,'') AS FTStateFinish,0.00 As FNTotalConvPrice"
        _Qry &= vbCrLf & " ,  ISNULL(B.FNQuantityTo,0) AS FNQuantityTo "
        _Qry &= vbCrLf & "  FROM"

        _Qry &= vbCrLf & "  (SELECT FNHSysRawMatId"
        _Qry &= vbCrLf & "   ,FTPurchaseNo"
        _Qry &= vbCrLf & "  ,Max(FTFabricFrontSize) AS FTFabricFrontSize"
        _Qry &= vbCrLf & "  ,FNPrice"
        _Qry &= vbCrLf & "  ,SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  FROM (SELECT A.FTConversionNo"
        _Qry &= vbCrLf & " , C.FNHSysRawMatId"
        _Qry &= vbCrLf & " , C.FTPurchaseNo"
        _Qry &= vbCrLf & " 	, C.FTFabricFrontSize"
        _Qry &= vbCrLf & " 	, B.FNQuantity"
        _Qry &= vbCrLf & " 	, CASE WHEN ISNULL(B.FNPriceTrans,0) <=0 THEN C.FNPrice ELSE ISNULL(B.FNPriceTrans,0)  END  AS FNPrice"
        _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK)  ON C.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion AS A WITH(NOLOCK) ON B.FTDocumentNo = A.FTConversionNo"
        _Qry &= vbCrLf & "  WHERE A.FTConversionNo='" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
        _Qry &= vbCrLf & "   ) AS A"
        _Qry &= vbCrLf & "  GROUP BY FNHSysRawMatId"
        _Qry &= vbCrLf & "    ,FTPurchaseNo"
        _Qry &= vbCrLf & "    ,FNPrice) AS A2 LEFT OUTER JOIN ("
        _Qry &= vbCrLf & " 		SELECT FTConversionNo, FNHSysRawMatId, FTPurchaseNo, FNPrice, SUM(FNQuantity) AS FNQuantity,MAX(ISNULL(FNTotalConvQuantity,0)) AS FNTotalConvQuantity"
        _Qry &= vbCrLf & " 	,MAX(ISNULL(FTStateFinish,'0')) AS FTStateFinish,SUM(FNQuantityTo) AS FNQuantityTo"
        _Qry &= vbCrLf & " 		FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail WITH(NOLOCK)"
        _Qry &= vbCrLf & " 	WHERE FTConversionNo ='" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
        _Qry &= vbCrLf & " 	GROUP BY FTConversionNo, FNHSysRawMatId, FTPurchaseNo, FNPrice				"
        _Qry &= vbCrLf & " 	) AS B ON A2.FNHSysRawMatId = B.FNHSysRawMatId"
        _Qry &= vbCrLf & " 	AND A2.FTPurchaseNo = B.FTPurchaseNo"
        _Qry &= vbCrLf & " 	AND A2.FNPrice = B.FNPrice "
        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial as IM WITH(NOLOCK ) ON A2.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "   LEFT OUTER  JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit as U WITH(NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        '  _Qry &= vbCrLf & "  WHERE  (A2.FNQuantity-ISNULL(B.FNQuantity,0)) > 0"
        _Qry &= vbCrLf & "  WHERE  CASE WHEN ISNULL(B.FNTotalConvQuantity,0) = 0 THEN A2.FNQuantity ELSE  ISNULL(B.FNTotalConvQuantity,0) - ISNULL(B.FNQuantityTo,0) END  > 0 AND ISNULL(B.FTStateFinish,'')  <>'1' "
        _Qry &= vbCrLf & "  ORDER BY IM.FTRawMatCode "
        _Qry &= vbCrLf & "  ,C.FTRawMatColorCode"
        _Qry &= vbCrLf & "  ,S.FTRawMatSizeCode"
        _Qry &= vbCrLf & "  ,A2.FTPurchaseNo"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
        _State = (_dt.Rows.Count > 0)

        ogcresource.DataSource = _dt.Copy
        ogcresource.Refresh()

        Return _State
    End Function

    Public Sub ClearDetail(Optional StateClearData As Boolean = True, Optional LoadRawmatDataDefault As Boolean = True)

        If StateClearData Then
            FNHSysRawMatId.Text = ""
            FTRawMatColorCode.Text = ""
            FNHSysRawMatId_None.Text = ""
            FTFabricFrontSize.Text = ""
            FNHSysUnitIdPO.Text = ""
            FNUsedAmount.Value = 0
            FNPOQuantity.Value = 0
            FNPrice.Value = 0
            FNCostPrice.Value = 0
            FNNetPrice.Value = 0
        End If

        FTOrderNo.Text = ""

        If LoadRawmatDataDefault Then
            Dim _Qry As String = ""
            Dim _dt As DataTable

            _Qry = "   SELECT IM.FTRawMatCode"
            _Qry &= vbCrLf & ", ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Qry &= vbCrLf & ", ISNULL(IMS.FTRawMatSizeCode,'') AS FTRawMatSizeCode "
            _Qry &= vbCrLf & ", ISNULL(MM.FTStateAssembly,'') AS FTStateAssembly "
            _Qry &= vbCrLf & ", ISNULL(MM.FTStateAssemblyDefault,'') AS FTStateAssemblyDefault "
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)  ON MM.FTMainMatCode = IM.FTRawMatCode LEFT OUTER JOIN"
            _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK)  ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "  WHERE  (MM.FTStateAssembly = '1') "


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            If _dt.Rows.Count > 0 Then

                If _dt.Select("FTStateAssemblyDefault='1'").Length > 0 Then

                    For Each R As DataRow In _dt.Select("FTStateAssemblyDefault='1'", "FTRawMatCode")

                        FTRawMatSizeCode.Text = R!FTRawMatSizeCode.ToString
                        FTRawMatColorCode.Text = R!FTRawMatColorCode.ToString
                        FNHSysRawMatId.Text = R!FTRawMatCode.ToString
                        Exit For

                    Next

                Else
                    For Each R As DataRow In _dt.Select("FTRawMatCode<>''", "FTRawMatCode")

                        FTRawMatSizeCode.Text = R!FTRawMatSizeCode.ToString
                        FTRawMatColorCode.Text = R!FTRawMatColorCode.ToString
                        FNHSysRawMatId.Text = R!FTRawMatCode.ToString
                        Exit For

                    Next
                End If

            End If
            _dt.Dispose()
        End If


    End Sub


    Private Function ValidateData() As Boolean
        With CType(Me.ogcresource.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1' AND FNUsedQty>0").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาเลือกวัตถุดิบที่ต้องการแปรสภาพ และระบุจำนวนใช้ !!!", 1509268054, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            End If

        End With

        If Me.FNHSysRawMatId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysRawMatId_lbl.Text)
            FNHSysRawMatId.Focus()
            Return False
        End If

        If Me.FNHSysUnitIdPO.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysUnitId_lbl.Text)
            FNHSysRawMatId.Focus()
            Return False
        End If

        If Me.FNPOQuantity.Value <= 0 Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNQuantity_lbl.Text)
            FNPOQuantity.Focus()
            Return False
        End If

        If Me.FTOrderNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub CalusedAmount()
        Try
            Dim _TotalRow As Integer = 0
            With CType(Me.ogcresource.DataSource, DataTable)
                .AcceptChanges()

                Dim _Amount As Double = 0
                Dim _PriceSum As Double = 0

                For Each R As DataRow In .Select("FTSelect='1'")

                    _TotalRow = _TotalRow + 1

                    _Amount = _Amount + Val(R!FNAmt.ToString)
                    _PriceSum = _PriceSum + Val(R!FNTotalConvPrice.ToString)

                Next

                FNUsedAmount.Value = _Amount
                FNPrice.Value = CDbl(Format(_PriceSum, "0.0000"))
                'FNPrice.Value = CDbl(Format(_PriceSum / _TotalRow, "0.0000"))

            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub CalusedAmountFinish(_Quantity As Double)
        Try
            Dim _TotalRow As Integer = 0
            Dim _TotalRow2 As Integer = 0
            Dim _PricePerUnit As Double

            With CType(Me.ogcresource.DataSource, DataTable)
                .AcceptChanges()

                Dim _Amount As Double = 0
                Dim _PriceSum As Double = 0

                For Each R As DataRow In .Select("FTSelect='1'")

                    _TotalRow = _TotalRow + 1

                    If Val(R!FNTotalConvQuantity.ToString) - (Val(R!FNQuantityTo.ToString) + _Quantity) Then
                        _TotalRow2 = _TotalRow2 + 1
                        _PricePerUnit = _PricePerUnit + CDbl(Format((Format(((Val(R!FNTotalConvQuantity.ToString) - (Val(R!FNQuantityTo.ToString) + _Quantity)) * Val(R!FNTotalConvPrice.ToString)), "0.00") / _Quantity), "0.0000"))
                    End If

                    _Amount = _Amount + Val(R!FNAmt.ToString)
                    _PriceSum = _PriceSum + Val(R!FNTotalConvPrice.ToString)

                Next

                FNUsedAmount.Value = _Amount
                FNPrice.Value = (CDbl(Format(_PriceSum / _TotalRow, "0.0000")) + CDbl(Format(_PricePerUnit / _TotalRow2, "0.0000")))

            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Function SaveData() As Boolean
        Dim _Qry As String = ""
        Dim _MaxSeq As Integer = 0
        Dim _dt As DataTable

        With CType(Me.ogcresource.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy

        End With

        _Qry = " SELECT MAX(FNSeq) AS FNSeq"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTConversionNo='" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "

        _MaxSeq = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "0"))) + 1

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_INVEN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            Dim _StateInsertFinish As Boolean = False

            For Each R As DataRow In _dt.Select("FTSelect='1'  AND FNUsedQty>0")

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTConversionNo"
                _Qry &= vbCrLf & ", FNHSysRawMatId, FTFabricFrontSize, FTPurchaseNo, FNPrice"
                _Qry &= vbCrLf & ", FNQuantity, FNHSysRawMatIdTo, FTOrderNo, FNSeq "
                _Qry &= vbCrLf & ", FNPriceCon,FNWageCost, FNWage, FNQuantityTo,FNTotalConvQuantity,FTStateFinish)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString()) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString()) & "' "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNPrice.ToString)) & " "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNUsedQty.ToString)) & " "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysRawMatId.Properties.Tag.ToString())) & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "' "
                _Qry &= vbCrLf & "," & _MaxSeq & " "
                _Qry &= vbCrLf & "," & Double.Parse(FNPrice.Value) & " "
                _Qry &= vbCrLf & "," & Double.Parse(FNWageCost.Value) & " "
                _Qry &= vbCrLf & "," & Double.Parse(FNCostPrice.Value) & " "
                _Qry &= vbCrLf & "," & Double.Parse(FNPOQuantity.Value) & " "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNTotalConvQuantity.ToString)) & " "
                _Qry &= vbCrLf & ",'" & FTCheckStateFinish.EditValue.ToString & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                If _StateInsertFinish = False Then

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail_Finish("
                    _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FTConversionNo, FNHSysRawMatId"
                    _Qry &= vbCrLf & " , FTOrderNo, FNSeq, FTFabricFrontSize, FTPurchaseNo, FNPrice, FNQuantity)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysRawMatId.Properties.Tag.ToString())) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "' "
                    _Qry &= vbCrLf & "," & _MaxSeq & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTFabricFrontSize.Text.Trim) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString()) & "' "
                    _Qry &= vbCrLf & "," & Double.Parse(FNNetPrice.Value) & " "
                    _Qry &= vbCrLf & "," & Double.Parse(FNPOQuantity.Value) & " "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _StateInsertFinish = True
                End If
            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

        Return True
    End Function


    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub ResFTStateSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ResFTStateSelect.EditValueChanging
        'Try
        '    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

        '        If e.NewValue.ToString = "1" Then
        '            Dim _balQty As Double = .GetFocusedRowCellValue("FNQuantity")
        '            Dim _Price As Double = .GetFocusedRowCellValue("FNPrice")

        '            .SetFocusedRowCellValue("FTSelect", "1")
        '            .SetFocusedRowCellValue("FNUsedQty", _balQty)
        '            .SetFocusedRowCellValue("FNAmt", Double.Parse(Format(_balQty * _Price, "0.00")))

        '        Else
        '            .SetFocusedRowCellValue("FTSelect", "0")
        '            .SetFocusedRowCellValue("FNUsedQty", 0)
        '            .SetFocusedRowCellValue("FNAmt", 0)

        '        End If
        '        Call CalusedAmount()
        '    End With
        'Catch ex As Exception
        'End Try

        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                FTCheckStateFinish.Checked = False
                If e.NewValue.ToString = "1" Then
                    Dim _balQty As Double = .GetFocusedRowCellValue("FNTotalConvQuantity")
                    Dim _Price As Double = .GetFocusedRowCellValue("FNAmt")

                    .SetFocusedRowCellValue("FTSelect", "1")

                    If _balQty > 0 Then
                        .SetFocusedRowCellValue("FNTotalConvPrice", Double.Parse(Format(_Price / _balQty, "0.0000")))
                    End If

                Else
                    .SetFocusedRowCellValue("FTSelect", "0")

                End If
                Call CalusedAmount()
            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ResFNRcvQty_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ResFNRcvQty.EditValueChanging
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .GetFocusedRowCellValue("FTSelect").ToString = "0" Then
                    e.Cancel = True
                Else
                    If Val(e.NewValue.ToString) < 0 Then
                        e.Cancel = True

                    Else
                        FTCheckStateFinish.Checked = False
                        Dim _balQty As Double = Val(e.NewValue.ToString)
                        Dim _Price As Double = .GetFocusedRowCellValue("FNPrice")

                        .SetFocusedRowCellValue("FNAmt", Double.Parse(Format(_balQty * _Price, "0.00")))

                        Call CalusedAmount()
                    End If
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    
    'Private Sub FNPrice_EditValueChanged(sender As Object, e As EventArgs) Handles FNPrice.EditValueChanged, FNCostPrice.EditValueChanged
    '    Static _Proc As Boolean
    '    If Not (_Proc) Then
    '        _Proc = True

    '        FNNetPrice.Value = FNPrice.Value + FNCostPrice.Value

    '        _Proc = False
    '    End If
    'End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If Me.ValidateData Then
            If Me.SaveData Then

                Try

                    Call CallByName(Me.MainObject, "LoadFinishRawmatDetail", CallType.Method, {DocumentNo})
                Catch ex As Exception
                End Try

                If Me.LoadData = False Then
                    Me.Close()
                Else
                    Me.ClearDetail(False, False)
                    Me.FNHSysRawMatId.Focus()
                End If
            End If
        End If
    End Sub

    'Private Sub FNUsedAmount_EditValueChanged(sender As Object, e As EventArgs) Handles FNUsedAmount.EditValueChanged, FNPOQuantity.EditValueChanged, FNWageCost.EditValueChanging
    '    Static _Proc As Boolean
    '    If Not (_Proc) Then
    '        _Proc = True
    '        Dim _FNUsedAmount As Double = 0
    '        Dim _FNPOQuantity As Double = 0
    '        Dim _NetPrice As Double = 0
    '        Dim _WagePrice As Double = 0
    '        Dim _FNWageAmount As Double = 0

    '        _FNUsedAmount = FNUsedAmount.Value
    '        _FNPOQuantity = FNPOQuantity.Value
    '        _FNWageAmount = FNWageCost.Value

    '        If _FNPOQuantity > 0 Then
    '            _NetPrice = Double.Parse(Format(_FNUsedAmount / _FNPOQuantity, HI.ST.Config.PriceFormat))
    '            _WagePrice = Double.Parse(Format(_FNWageAmount / _FNPOQuantity, HI.ST.Config.PriceFormat))
    '        End If

    '        FNPrice.Value = _NetPrice
    '        FNCostPrice.Value = _WagePrice

    '        FNNetPrice.Value = FNPrice.Value + FNCostPrice.Value
    '        _Proc = False
    '    End If
    'End Sub


    Private Sub FNPOQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNPOQuantity.EditValueChanging

        If sender.name.ToString.ToUpper = "FNPOQuantity".ToUpper Then

            If e.NewValue < 0 Then

                e.Cancel = True
                Return

            Else

                With CType(Me.ogcresource.DataSource, DataTable)

                    For Each R As DataRow In .Select("FTSelect='1'")
                        If Val(R!FNTotalConvQuantity.ToString) < (Val(R!FNQuantityTo.ToString) + e.NewValue) Then
                            e.Cancel = True
                            Return
                        End If
                    Next

                End With

            End If

        End If

        Static _Proc As Boolean
        If Not (_Proc) Then
            _Proc = True
            Dim _FNUsedAmount As Double = 0
            Dim _FNPOQuantity As Double = 0
            Dim _NetPrice As Double = 0
            Dim _WagePrice As Double = 0
            Dim _FNWageAmount As Double = 0

            Select Case sender.name.ToString.ToUpper
                Case "FNUsedAmount".ToUpper

                    _FNUsedAmount = e.NewValue
                    _FNPOQuantity = FNPOQuantity.Value
                    '_FNWageAmount = FNWageCost.Value
                    _FNWageAmount = FNCostPrice.Value
                Case "FNPOQuantity".ToUpper

                    _FNUsedAmount = FNUsedAmount.Value
                    _FNPOQuantity = e.NewValue
                    ' _FNWageAmount = FNWageCost.Value
                    _FNWageAmount = FNCostPrice.Value

                    If (Me.FTCheckStateFinish.Checked) Then
                        CalusedAmountFinish(_FNPOQuantity)
                    Else
                        CalusedAmount()
                    End If

                Case "FNWageCost".ToUpper
                    _FNUsedAmount = FNUsedAmount.Value
                    _FNPOQuantity = FNPOQuantity.Value
                    ' _FNWageAmount = e.NewValue
                    _FNWageAmount = FNCostPrice.Value
                Case Else

                    _FNUsedAmount = FNUsedAmount.Value
                    _FNPOQuantity = FNPOQuantity.Value
                    ' _FNWageAmount = FNWageCost.Value
                    _FNWageAmount = FNCostPrice.Value
            End Select

            If _FNPOQuantity > 0 Then

                _NetPrice = Double.Parse(Format(_FNUsedAmount / _FNPOQuantity, HI.ST.Config.PriceFormat))
                ' _WagePrice = Double.Parse(Format(_FNWageAmount / _FNPOQuantity, HI.ST.Config.PriceFormat))

            End If
            FNWageCost.Value = Double.Parse(Format(_FNWageAmount * _FNPOQuantity, HI.ST.Config.AmtFormat))
            'FNPrice.Value = _NetPrice
            'FNCostPrice.Value = _WagePrice
            FNNetPrice.Value = FNPrice.Value + FNCostPrice.Value

            _Proc = False
        End If
    End Sub

    Private Sub RepFNTotalConvQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFNTotalConvQuantity.EditValueChanging
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .GetFocusedRowCellValue("FTStateConverTo").ToString <> "1" Then
                    FTCheckStateFinish.Checked = False
                    If IsNumeric(e.NewValue) Then
                        If e.NewValue >= 0 Then

                            Dim _balQty As Double = e.NewValue
                            Dim _Price As Double = .GetFocusedRowCellValue("FNAmt")

                            .SetFocusedRowCellValue("FTSelect", "1")

                            If _balQty > 0 Then
                                .SetFocusedRowCellValue("FNTotalConvPrice", Double.Parse(Format(_Price / _balQty, "0.0000")))
                            End If

                        Else

                            e.Cancel = True

                        End If

                    End If

                    Call CalusedAmount()

                Else
                    e.Cancel = True
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTCheckStateFinish_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FTCheckStateFinish.EditValueChanging
        Try

            If e.NewValue.ToString = "1" Then
                CalusedAmountFinish(Me.FNPOQuantity.Value)
            Else
                CalusedAmount()
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub FNCostPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNCostPrice.EditValueChanging
        Try
            FNNetPrice.Value = FNPrice.Value + e.NewValue
            FNWageCost.Value = Double.Parse(Format(e.NewValue * FNPOQuantity.Value, HI.ST.Config.AmtFormat))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNWageCost_EditValueChanged(sender As Object, e As EventArgs) Handles FNWageCost.EditValueChanged

    End Sub

    Private Sub FNCostPrice_EditValueChanged(sender As Object, e As EventArgs) Handles FNCostPrice.EditValueChanged

    End Sub

    Private Sub FNPOQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles FNPOQuantity.EditValueChanged

    End Sub
End Class
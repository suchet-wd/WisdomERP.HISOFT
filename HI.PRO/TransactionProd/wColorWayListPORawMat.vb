Public Class wColorWayListPORawMat

#Region "Property"

    Private _ProcessSave As Boolean = False
    Private _FNOrderType As Integer = 0
    Public Property ProcessSave As Boolean
        Get
            Return _ProcessSave
        End Get
        Set(value As Boolean)
            _ProcessSave = value
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

    Private _OrderProdNo As String = ""
    Public Property OrderProdNo As String
        Get
            Return _OrderProdNo
        End Get
        Set(value As String)
            _OrderProdNo = value
        End Set
    End Property

    Private _Mark As String = ""
    Public Property MarkID As String
        Get
            Return _Mark
        End Get
        Set(value As String)
            _Mark = value
        End Set
    End Property

    Private _TableNo As String = ""
    Public Property TableNo As String
        Get
            Return _TableNo
        End Get
        Set(value As String)
            _TableNo = value
        End Set
    End Property

    Private _ColorWay As String = ""
    Public Property ColorWay As String
        Get
            Return _ColorWay
        End Get
        Set(value As String)
            _ColorWay = value
        End Set
    End Property

    Private _PurchaseNo As String = ""
    Public Property PurchaseNo As String
        Get
            Return _PurchaseNo
        End Get
        Set(value As String)
            _PurchaseNo = value
        End Set
    End Property

    Private _DataPurchase As DataTable = Nothing
    Public Property DataPurchase() As DataTable
        Get
            Return _DataPurchase
        End Get
        Set(value As DataTable)
            _DataPurchase = value
        End Set
    End Property
#End Region

    Private _LoadInfo As Boolean = False
#Region "Procedure"

    Private Sub LoadDataInFo()
        _LoadInfo = True
        'Try

        '    With FTPurchaseNo
        '        .Properties.DataSource = Me.DataPurchase
        '        .EditValue = ""
        '    End With

        'Catch ex As Exception
        'End Try

        Call LoadItem()
        Dim _Qry As String = ""
        Dim dt As DataTable

        _Qry = "  SELECT        A.FTOrderProdNo, A.FNHSysMarkId, A.FNTableNo, A.FTColorway, A.FTPurchaseNo, A.FNHSysRawMatId, B.FTRawMatCode, MC.FTRawMatColorCode,A.FTShades"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameTH,'') AS FTRawMatColorName"
        Else
            _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameEN,'') AS FTRawMatColorName "
        End If
        _Qry &= vbCrLf & " , ISNULL(MZ.FTRawMatSizeCode,'') AS FTRawMatSizeCode "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK)  ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON B.FNHSysRawMatColorId = MC.FNHSysRawMatColorId"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MZ WITH(NOLOCK) ON B.FNHSysRawMatSizeId = MZ.FNHSysRawMatSizeId "
        _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.OrderProdNo) & "' "
        _Qry &= vbCrLf & "  AND  A.FNHSysMarkId=" & Val(Me.MarkID) & " "
        _Qry &= vbCrLf & "  AND  A.FNTableNo=" & Val(Me.TableNo) & " "
        _Qry &= vbCrLf & "  AND  A.FTColorway='" & HI.UL.ULF.rpQuoted(Me.ColorWay) & "' "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        For Each R As DataRow In dt.Rows

            'FTPurchaseNo.EditValue = R!FTPurchaseNo.ToString
            'Call FTPurchaseNo_EditValueChanged(FTPurchaseNo, New System.EventArgs)
            'Try
            '    FNHSysRawMatId.Text = R!FTRawMatCode.ToString
            'Catch ex As Exception

            'End Try

            Try
                FNHSysRawMatId.EditValue = Integer.Parse(Val(R!FNHSysRawMatId.ToString)) ' FNHSysRawMatId.Properties.GetDataSourceValue("FNHSysRawMatId", Integer.Parse(Val(R!FNHSysRawMatId.ToString))) ' R!FNHSysRawMatId.ToString
                FNHSysRawMatId.RefreshEditValue()
            Catch ex As Exception
            End Try

            Try
                FTColorCode.Text = R!FTRawMatColorCode.ToString ' FNHSysRawMatId.GetColumnValue("FTRawMatColorCode").ToString
            Catch ex As Exception
                FTColorCode.Text = ""
            End Try

            Try
                FTColorName.Text = R!FTRawMatColorName.ToString ' FNHSysRawMatId.GetColumnValue("FTRawMatColorName").ToString
            Catch ex As Exception
                FTColorName.Text = ""
            End Try

            Try
                FTRawMatSizeCode.Text = R!FTRawMatSizeCode.ToString
            Catch ex As Exception
                FTRawMatSizeCode.Text = ""

            End Try

            Call FNHSysRawMatId_EditValueChanged(FNHSysRawMatId, New System.EventArgs)


            LoadPo(Integer.Parse(Val(R!FNHSysRawMatId.ToString)), FNHSysRawMatId.Text, FTColorCode.Text.Trim, FTRawMatSizeCode.Text.Trim)

            FTPurchaseNo.EditValue = R!FTPurchaseNo.ToString


            Try
                FTShades.Text = R!FTShades.ToString
            Catch ex As Exception
                FTShades.Text = ""
            End Try
            Exit For

        Next

        dt.Dispose()
        _LoadInfo = False
    End Sub

    Private Function SaveData() As Boolean
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


        Try

            _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat"
            _Qry &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  "
            _Qry &= vbCrLf & " ,FNHSysRawMatId=" & Val(FNHSysRawMatId.EditValue) & " "
            _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & " ,FTShades='" & HI.UL.ULF.rpQuoted(FTShades.Text) & "'"




            _Qry &= vbCrLf & "  WHERE  FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.OrderProdNo) & "' "
            _Qry &= vbCrLf & "  AND  FNHSysMarkId=" & Val(Me.MarkID) & " "
            _Qry &= vbCrLf & "  AND  FNTableNo=" & Val(Me.TableNo) & " "
            _Qry &= vbCrLf & "  AND  FTColorway='" & HI.UL.ULF.rpQuoted(Me.ColorWay) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTPurchaseNo, FNHSysRawMatId,FTShades)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.OrderProdNo) & "' "
                _Qry &= vbCrLf & "," & Val(Me.MarkID) & " "
                _Qry &= vbCrLf & "," & Val(Me.TableNo) & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.ColorWay) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  "
                _Qry &= vbCrLf & "," & Val(FNHSysRawMatId.EditValue) & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTShades.Text) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If
          
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
    End Function
#End Region

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click

        Me.ProcessSave = False
        Me.Close()

    End Sub

    Private Sub wColorWayListPORawMat_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call LoadDataInFo()

        Dim _Str As String = ""

        _Str = "SELECT TOP 1 FNOrderType "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "

        _FNOrderType = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "-1")))

    End Sub

    Private Sub FNHSysRawMatId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysRawMatId.EditValueChanged
        If FNHSysRawMatId.Text <> "" Then
            Try

                Me.FTColorCode.Text = FNHSysRawMatId.GetColumnValue("FTRawMatColorCode").ToString
                Me.FTColorName.Text = FNHSysRawMatId.GetColumnValue("FTRawMatColorName").ToString
                FTRawMatSizeCode.Text = FNHSysRawMatId.GetColumnValue("FTRawMatSizeCode").ToString
            Catch ex As Exception
            End Try

            If Not (_LoadInfo) Then
                Try
                    LoadPo(Val(FNHSysRawMatId.EditValue), FNHSysRawMatId.Text, Me.FTColorCode.Text, Me.FTColorName.Text)
                Catch ex As Exception
                    LoadPo(0, "", "", "")
                End Try
            End If

        Else
            If Not (_LoadInfo) Then
                Try
                    LoadPo(0, "", "", "")
                Catch ex As Exception

                End Try
            End If

        End If
    End Sub

    Private Sub LoadItem()
        Dim dt As DataTable
        Dim _Qry As String = ""

        _Qry = " SELECT FNHSysRawMatId,FTRawMatCode,FTRawMatColorCode,FTRawMatColorName,FTRawMatSizeCode"
        _Qry &= vbCrLf & " FROM ( SELECT 0 AS FNHSysRawMatId ,'' AS FTRawMatCode ,'' AS FTRawMatColorCode ,'' AS FTRawMatColorName,'' AS FTRawMatSizeCode ,0 FNSeq"
        _Qry &= vbCrLf & " UNION  "


        _Qry &= vbCrLf & "   Select A.FNHSysRawMatId, B.FTRawMatCode, ISNULL(MC.FTRawMatColorCode,'') AS FTRawMatColorCode"



        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , MAX(ISNULL(MACX.FTRawMatColorNameTH,ISNULL(MC.FTRawMatColorNameTH,''))) AS FTRawMatColorName"
        Else
            _Qry &= vbCrLf & " , MAX(ISNULL(MACX.FTRawMatColorNameEN,ISNULL(MC.FTRawMatColorNameTH,''))) AS FTRawMatColorName "
        End If


        _Qry &= vbCrLf & "  ,ISNULL(MZ.FTRawMatSizeCode,'') AS FTRawMatSizeCode,1 FNSeq"
        _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) "

        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS MACX WITH(NOLOCK)"

        _Qry &= vbCrLf & "  ON    A.FNHSysStyleId = MACX.FNHSysStyleId "
        _Qry &= vbCrLf & "    And  A.FTOrderNo= MACX.FTOrderNo "
        _Qry &= vbCrLf & "    And  A.FNHSysMerMatId =MACX.FNHSysMainMatId  And  A.FNHSysRawMatColorId =MACX.FNHSysRawMatColorId "

        _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON B.FNHSysRawMatColorId = MC.FNHSysRawMatColorId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MZ WITH(NOLOCK) ON B.FNHSysRawMatSizeId = MZ.FNHSysRawMatSizeId "
        _Qry &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS IMM WITH(NOLOCK)  ON A.FNHSysMerMatId = IMM.FNHSysMainMatId"

        _Qry &= vbCrLf & "   WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "') "
        _Qry &= vbCrLf & "          AND (IMM.FNMerMatType = 0) "
        _Qry &= vbCrLf & "          AND (A.FTMatColorCode = N'" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "')"
        _Qry &= vbCrLf & "   GROUP  BY A.FNHSysRawMatId, B.FTRawMatCode, ISNULL(MC.FTRawMatColorCode,''),ISNULL(MZ.FTRawMatSizeCode,'') "
        _Qry &= vbCrLf & " ) AS MMX "
        _Qry &= vbCrLf & " ORDER BY FNSeq,FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode "


        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With FNHSysRawMatId
            .Properties.ValueMember = "FNHSysRawMatId"
            .Properties.DisplayMember = "FTRawMatCode"
            .Properties.DataSource = dt.Copy
            '.Properties.PopulateColumns()
            .EditValue = 0
        End With

    End Sub

    Private Sub LoadPo(MatId As Integer, ItemCode As String, ColorCode As String, SizeCode As String)
        Dim dt As DataTable
        Dim _Qry As String = ""

        '_Qry = " SELECT  FTPurchaseNo  FROM "
        '_Qry &= vbCrLf & "  ( "
        '_Qry &= vbCrLf & "   Select A.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
        '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS C WITH(NOLOCK) ON B.FTRawMatCode = C.FTMainMatCode"
        '_Qry &= vbCrLf & "   WHERE      (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')  "
        '_Qry &= vbCrLf & "   AND     (C.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "      AND     (B.FNHSysRawMatId = " & MatId & ") "
        '_Qry &= vbCrLf & "   GROUP BY A.FTPurchaseNo "
        '_Qry &= vbCrLf & "   UNION "
        '_Qry &= vbCrLf & "   Select C.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS A WITH(NOLOCK) INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FTReserveNo = B.FTDocumentNo INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo  INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        '_Qry &= vbCrLf & "   WHERE      (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')  "
        '_Qry &= vbCrLf & "      AND     (MM.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "      AND     (IM.FNHSysRawMatId = " & MatId & ") "
        '_Qry &= vbCrLf & "   GROUP BY C.FTPurchaseNo "
        '_Qry &= vbCrLf & "   UNION "
        '_Qry &= vbCrLf & "   Select C.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock AS A WITH(NOLOCK) INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FTAdjustStockNo = B.FTDocumentNo INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo  INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        '_Qry &= vbCrLf & "   WHERE      (B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')  "
        '_Qry &= vbCrLf & "      AND     (MM.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "      AND     (IM.FNHSysRawMatId = " & MatId & ") "
        '_Qry &= vbCrLf & "   GROUP BY C.FTPurchaseNo "
        '_Qry &= vbCrLf & "   UNION "
        '_Qry &= vbCrLf & "   Select C.FTPurchaseNo"
        '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B WITH(NOLOCK) ON A.FTTransferOrderNo = B.FTDocumentNo INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo  INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON C.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
        '_Qry &= vbCrLf & "   WHERE      (B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')  "
        '_Qry &= vbCrLf & "      AND     (MM.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "      AND     (IM.FNHSysRawMatId = " & MatId & ") "
        '_Qry &= vbCrLf & "   GROUP BY C.FTPurchaseNo "
        '_Qry &= vbCrLf & "  ) AS A Order By FTPurchaseNo "


        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.USP_GETPONO_FORPROD '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "','" & HI.UL.ULF.rpQuoted(ItemCode) & "','" & HI.UL.ULF.rpQuoted(ColorCode) & "','" & HI.UL.ULF.rpQuoted(SizeCode) & "'," & Val(MatId) & " "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        If dt.Rows.Count <= 0 Then
            dt.Rows.Add("")
        End If

        Try

            With FTPurchaseNo
                .Properties.DataSource = dt.Copy
                .EditValue = ""
            End With

        Catch ex As Exception
        End Try

    End Sub
    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPurchaseNo.EditValueChanged
        'Dim dt As DataTable
        'Dim _Qry As String = ""

        '_Qry = "Select 0 As FNHSysRawMatId ,'' AS FTRawMatCode ,'' AS FTRawMatColorCode ,'' AS FTRawMatColorName,'' AS FTRawMatSizeCode "
        '_Qry &= vbCrLf & "   UNION "
        '_Qry &= vbCrLf & "   SELECT  MAX(FNHSysRawMatId) AS FNHSysRawMatId,FTRawMatCode,FTRawMatColorCode,FTRawMatColorName,FTRawMatSizeCode  FROM "
        '_Qry &= vbCrLf & "  ( Select  A.FTPurchaseNo, B.FNHSysRawMatId, B.FTRawMatCode, ISNULL(MC.FTRawMatColorCode,'') AS FTRawMatColorCode,ISNULL(MZ.FTRawMatSizeCode,'') AS FTRawMatSizeCode"

        ''If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        ''    _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameTH,'') AS FTRawMatColorName"
        ''Else
        ''    _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameEN,'') AS FTRawMatColorName "
        ''End If

        '_Qry &= vbCrLf & " ,ISNULL(("
        '_Qry &= vbCrLf & "   SELECT TOP 1  "

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & "  ISNULL(AA.FTRawMatColorNameTH,ISNULL(MC.FTRawMatColorNameTH,'')) AS FTRawMatColorName"
        'Else
        '    _Qry &= vbCrLf & "  ISNULL(AA.FTRawMatColorNameEN, ISNULL(MC.FTRawMatColorNameEN,'')) AS FTRawMatColorName "
        'End If

        '_Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS AA  WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "     HITECH_MASTER.dbo.TMERMMainMat AS BA ON AA.FNHSysMainMatId = BA.FNHSysMainMatId"
        '_Qry &= vbCrLf & "  WHERE  (AA.FNHSysRawMatColorId = B.FNHSysRawMatColorId) "
        '_Qry &= vbCrLf & "  AND (BA.FTMainMatCode = B.FTRawMatCode)"
        '_Qry &= vbCrLf & "  AND (AA.FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')"


        '_Qry &= vbCrLf & " ),"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & "  ISNULL(MC.FTRawMatColorNameTH,'') "
        'Else
        '    _Qry &= vbCrLf & "  ISNULL(MC.FTRawMatColorNameEN,'') "
        'End If

        '_Qry &= vbCrLf & ") AS  FTRawMatColorName"
        '_Qry &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS C WITH(NOLOCK) ON B.FTRawMatCode = C.FTMainMatCode"
        '_Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON B.FNHSysRawMatColorId = MC.FNHSysRawMatColorId "
        '_Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MZ WITH(NOLOCK) ON B.FNHSysRawMatSizeId = MZ.FNHSysRawMatSizeId "
        '_Qry &= vbCrLf & "   WHERE      (A.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "')  "
        '_Qry &= vbCrLf & "   AND     (C.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "   AND     ( B.FNHSysRawMatId IN ("
        '_Qry &= vbCrLf & "   Select A.FNHSysRawMatId"
        '_Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS B ON A.FNHSysMerMatId = B.FNHSysMainMatId"
        '_Qry &= vbCrLf & "   WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "') "
        '_Qry &= vbCrLf & "          AND (B.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "          AND (A.FTMatColorCode = N'" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "')"
        '_Qry &= vbCrLf & "   GROUP BY A.FNHSysRawMatId"
        '_Qry &= vbCrLf & " )) "
        '_Qry &= vbCrLf & " UNION "
        '_Qry &= vbCrLf & "   Select  A.FTPurchaseNo, B.FNHSysRawMatId, B.FTRawMatCode, ISNULL(MC.FTRawMatColorCode,'') AS FTRawMatColorCode,ISNULL(MZ.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        '_Qry &= vbCrLf & " ,ISNULL(("
        '_Qry &= vbCrLf & "   SELECT TOP 1  "

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & "  ISNULL(AA.FTRawMatColorNameTH,ISNULL(MC.FTRawMatColorNameTH,'')) AS FTRawMatColorName"
        'Else
        '    _Qry &= vbCrLf & "  ISNULL(AA.FTRawMatColorNameEN, ISNULL(MC.FTRawMatColorNameEN,'')) AS FTRawMatColorName "
        'End If

        '_Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS AA  WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "     HITECH_MASTER.dbo.TMERMMainMat AS BA ON AA.FNHSysMainMatId = BA.FNHSysMainMatId"
        '_Qry &= vbCrLf & "  WHERE  (AA.FNHSysRawMatColorId = B.FNHSysRawMatColorId) "
        '_Qry &= vbCrLf & "  AND (BA.FTMainMatCode = B.FTRawMatCode)"
        '_Qry &= vbCrLf & "  AND (AA.FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "')"

        '_Qry &= vbCrLf & " ),"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & "  ISNULL(MC.FTRawMatColorNameTH,'') "
        'Else
        '    _Qry &= vbCrLf & "  ISNULL(MC.FTRawMatColorNameEN,'') "
        'End If


        '_Qry &= vbCrLf & ") AS  FTRawMatColorName"
        '_Qry &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS C WITH(NOLOCK) ON B.FTRawMatCode = C.FTMainMatCode"
        '_Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON B.FNHSysRawMatColorId = MC.FNHSysRawMatColorId "
        '_Qry &= vbCrLf & "   LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MZ WITH(NOLOCK) ON B.FNHSysRawMatSizeId = MZ.FNHSysRawMatSizeId "
        '_Qry &= vbCrLf & "   WHERE      (A.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "')  "
        '_Qry &= vbCrLf & "   AND     (C.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "   AND    NOT (A.FTPurchaseNo IN (SELECT FTPurchaseNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK)  WHERE A.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' )) "
        '_Qry &= vbCrLf & "   AND     ( B.FNHSysRawMatId IN ("
        '_Qry &= vbCrLf & "   Select A.FNHSysRawMatId"
        '_Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS B ON A.FNHSysMerMatId = B.FNHSysMainMatId"
        '_Qry &= vbCrLf & "   WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "') "
        '_Qry &= vbCrLf & "          AND (B.FNMerMatType = 0) "
        '_Qry &= vbCrLf & "          AND (A.FTMatColorCode = N'" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "')"
        '_Qry &= vbCrLf & "   GROUP BY A.FNHSysRawMatId"
        '_Qry &= vbCrLf & " )) "
        '_Qry &= vbCrLf & ""
        '_Qry &= vbCrLf & "  ) AS A GROUP BY FTRawMatCode,FTRawMatColorCode,FTRawMatColorName,FTRawMatSizeCode  Order By FTRawMatCode,FTRawMatColorCode "

        'dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'With FNHSysRawMatId
        '    .Properties.ValueMember = "FNHSysRawMatId"
        '    .Properties.DisplayMember = "FTRawMatCode"
        '    .Properties.DataSource = dt.Copy
        '    '.Properties.PopulateColumns()
        '    .EditValue = 0
        'End With

        'If Not (_LoadInfo) Then
        '    Call FNHSysRawMatId_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        'End If
        '' dt.Dispose()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click

        If Me.FTOrderProdNo.Text <> "" Then
            If Me.FNHSysMarkId.Text <> "" Then
                If Me.FNTableNo.Text <> "" Then
                    If Me.FTColorway.Text <> "" Then
                        If Me.FNHSysRawMatId.Text <> "" Then
                            If _FNOrderType <> 13 Then

                                If Me.FTPurchaseNo.Text = "" Then

                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTPurchaseNo_lbl.Text)
                                    FTPurchaseNo.Focus()

                                    Exit Sub

                                End If

                            End If

                            If Me.SaveData Then
                                Me.ProcessSave = True
                                Me.Close()
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysRawMatId_lbl.Text)
                            FNHSysRawMatId.Focus()
                        End If


                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTColorway_lbl.Text)
                        FTColorway.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNTableNo_lbl.Text)
                    FNTableNo.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysMarkId_lbl.Text)
                FNHSysMarkId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderProdNo_lbl.Text)
            FTOrderProdNo.Focus()
        End If

    End Sub
End Class
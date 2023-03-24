Public Class wFormListBarcodeTransaction 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ogvdetail
            .Columns("FNQuantityIN").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantityIN")
            .Columns("FNQuantityIN").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("FNQuantityOUT").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantityOUT")
            .Columns("FNQuantityOUT").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With
    End Sub

    Private _BarcodeNo As String = ""
    Public Property BarcodeNo As String
        Get
            Return _BarcodeNo
        End Get
        Set(value As String)
            _BarcodeNo = value
        End Set
    End Property

    Public Sub LoadList()
        Dim Qry As String = ""


        Qry = "Select A.FTBarcodeNo,A.FNHSysFixedAssetId,ASS.FTAssetCode,U.FTUnitAssetCode,W.FTWHAssetCode,A.FNQuantityIN,A.FNQuantityOUT,A.FTDocumentNo "
        Qry &= vbCrLf & ",ASS.FTAssetNameTH AS FTAssetName,W.FTWHAssetNameTH AS FTWHAssetName"
        Qry &= vbCrLf & "FROM"
        Qry &= vbCrLf & "(SELECT B.FTBarcodeNo, B.FNHSysFixedAssetId, B.FNHSysUnitId, BIN.FNQuantity As FNQuantityIN, BIN.FNHSysWHAssetId, 0 As FNQuantityOUT, BIN.FTDocumentNo"
        Qry &= vbCrLf & "FROM"
        Qry &= vbCrLf & "(SELECT FTBarcodeNo, FNHSysFixedAssetId, FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode As B GROUP BY FTBarcodeNo, FNHSysFixedAssetId, FNHSysUnitId) As B LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BIN ON B.FTBarcodeNo = BIN.FTBarcodeNo"
        Qry &= vbCrLf & "UNION ALL "
        Qry &= vbCrLf & "Select B.FTBarcodeNo, B.FNHSysFixedAssetId,B.FNHSysUnitId,0 As FNQuantityIN,BOUT.FNHSysWHAssetId, BOUT.FNQuantity As FNQuantityOUT,BOUT.FTDocumentNo"
        Qry &= vbCrLf & "FROM"
        Qry &= vbCrLf & "(SELECT FTBarcodeNo, FNHSysFixedAssetId, FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode As B GROUP BY FTBarcodeNo, FNHSysFixedAssetId, FNHSysUnitId) As B LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BOUT ON B.FTBarcodeNo = BOUT.FTBarcodeNo"
        Qry &= vbCrLf & ") As A LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.v_TASMAsset AS ASS ON A.FNHSysFixedAssetId = ASS.FNHSysFixedAssetId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset As U On A.FNHSysUnitId = U.FNHSysUnitAssetId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseAsset AS W ON A.FNHSysWHAssetId = W.FNHSysWHAssetId"
        Qry &= vbCrLf & "WHERE A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(Me.BarcodeNo) & "'"



        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_INVEN)
    End Sub

    Private Sub wFormListBarcodeTransaction_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
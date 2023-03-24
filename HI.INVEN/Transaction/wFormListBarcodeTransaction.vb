Public Class wFormListBarcodeTransaction 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
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
        Dim _Str As String = ""


        _Str = " Select A.FTBarcodeNo"
        _Str &= vbCrLf & "    ,A.FTDocumentNo"
        _Str &= vbCrLf & "  ,A.FTOrderNo"
        _Str &= vbCrLf & "  , ISNULL(W.FTWHCode,'') AS FNHSysWHId"
        _Str &= vbCrLf & "  ,A.FNQuantity"
        _Str &= vbCrLf & "   FROM"
        _Str &= vbCrLf & "  (SELECT        FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity, FDInsDate, FTInsTime"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS A WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(Me.BarcodeNo) & "' "
        _Str &= vbCrLf & "   UNION "
        _Str &= vbCrLf & " SELECT        FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, -FNQuantity, FDInsDate, FTInsTime"
        _Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B WITH(NOLOCK)) AS A INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH (NOLOCK) ON A.FNHSysWHId = W.FNHSysWHId"
        _Str &= vbCrLf & "   WHERE FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(Me.BarcodeNo) & "' "
        _Str &= vbCrLf & "  ORDER BY A.FTBarcodeNo,A.FDInsDate,A.FTInsTime"

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
    End Sub

    Private Sub wFormListBarcodeTransaction_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
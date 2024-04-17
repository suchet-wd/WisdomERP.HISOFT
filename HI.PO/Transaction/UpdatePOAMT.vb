Public Class UpdatePOAMT

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub
#Region "Property"
    Private _PRNO As String = ""
    Public Property PRNO As String
        Get
            Return _PRNO
        End Get
        Set(value As String)
            _PRNO = value
        End Set
    End Property

    Private _FNSuplID As Integer = 0
    Public Property FNSuplID As Integer
        Get
            Return _FNSuplID
        End Get
        Set(value As Integer)
            _FNSuplID = value
        End Set
    End Property

    Private _AddMat As Boolean = False
    Public Property AddMat As Boolean
        Get
            Return _AddMat
        End Get
        Set(value As Boolean)
            _AddMat = value
        End Set
    End Property
#End Region

#Region "Function"


#End Region


    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click

        Me.Close()
    End Sub

    Private Sub wAddRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Dim cmdstring As String = "EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_CHKAM"
        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        Dim _FNSurchangeAmt As Double
        Dim PoAmt As Decimal = 0.0
        Dim _DisAmt As Decimal = 0.0
        Dim _VatAmt As Decimal = 0.0
        Dim _NetAmt As Decimal = 0.0
        Dim PoAmtTH As String = ""
        Dim PoAmtEN As String = ""
        Dim DiscountPer As Decimal = 0.0
        Dim Vat As Decimal = 0.0
        Dim SurCharge As Decimal = 0.0
        Dim _Str As String = ""
        Dim pPO As String = ""
        For Each R As DataRow In dt.Rows

            _DisAmt = 0
            _VatAmt = 0
            pPO = R!FTPurchaseNo.ToString
            PoAmt = Val(R!FNAmt.ToString)
            SurCharge = Val(R!FNSurcharge.ToString)
            DiscountPer = Val(R!FNDisCountPer.ToString)
            Vat = Val(R!FNVatPer.ToString)

            If DiscountPer > 0 Then
                _DisAmt = CDbl(Format((PoAmt * DiscountPer) / 100, HI.ST.Config.AmtFormat))
            End If

            If Vat > 0 Then
                _VatAmt = CDbl(Format((((PoAmt - _DisAmt) + SurCharge) * Vat) / 100, HI.ST.Config.AmtFormat))
            End If

            _NetAmt = ((PoAmt - _DisAmt) + SurCharge) + _VatAmt

            PoAmtEN = HI.UL.ULF.Convert_Bath_EN(_NetAmt)
            PoAmtTH = HI.UL.ULF.Convert_Bath_TH(_NetAmt)



            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET "
            _Str &= vbCrLf & "  FNPoAmt=" & PoAmt & "  "
            _Str &= vbCrLf & " , FNDisCountAmt =" & _DisAmt & "  "
            _Str &= vbCrLf & " , FNPONetAmt=" & (PoAmt - _DisAmt) & "  "
            _Str &= vbCrLf & " , FNVatAmt=" & _VatAmt & "  "
            _Str &= vbCrLf & " , FNPOGrandAmt=" & _NetAmt & "  "
            _Str &= vbCrLf & " , FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(PoAmtEN) & "'"
            _Str &= vbCrLf & " , FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(PoAmtEN) & "',FTStatePDF='0'"
            _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pPO) & "' "
            _Str &= vbCrLf & "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET FTStateExportAX='0' "
            _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pPO) & "' "

            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)


        Next
    End Sub
End Class
Imports System.Windows.Forms

Public Class ValidateExportXML

    Public Shared Function CheckExportFileXML(_OrderNo As String, SubOrderNo As String, Optional ShowMsg As Boolean = True) As Boolean
        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 A.FTOrderNo  "
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS A INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS B WITH(NOLOCK) ON A.FTPOref = B.FTCustomerPO AND A.FNHSysContinentId = B.FNHSysContinentId AND A.FNHSysCountryId = B.FNHSysCountryId AND "
        _Qry &= vbCrLf & "     A.FNHSysProvinceId = B.FNHSysProvinceId And A.FNHSysShipModeId = B.FNHSysShipModeId And A.FNHSysShipPortId = B.FNHSysShipPortId "
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS C ON B.FTCustomerPO = C.FTCustomerPO AND B.FTInvoiceNo = C.FTInvoiceNo AND A.FTNikePOLineItem = C.FTPOLineItem AND "
        _Qry &= vbCrLf & "   A.FTColorway = C.FTColorway And A.FTSizeBreakDown = C.FTSizeBreakDown"
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS X WITH(NOLOCK) ON B.FTCustomerPO = X.FTCustomerPO AND B.FTInvoiceNo = X.FTInvoiceNo"
        _Qry &= vbCrLf & "  WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
        '_Qry &= vbCrLf & "  AND A.FTSubOrderNoRef='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"
        _Qry &= vbCrLf & "  AND ISNULL(X.FTPostUser,'')<>''"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then
            Return False
        Else

            If (ShowMsg) Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการส่งออก XML File แล้ว ไม่สามารถเปลี่ยนแปลงหรือแก้ไขข้อมูลได้ !!!", 1601010574, "Check Export XML", , MessageBoxIcon.Warning)
            End If

            Return True
        End If

    End Function

    Public Shared Function CheckExportMI(_OrderNo As String, SubOrderNo As String, Optional ShowMsg As Boolean = True) As Boolean
        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 A.FTOrderNo  "
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS A INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS B WITH(NOLOCK) ON A.FTPOref = B.FTCustomerPO "
        _Qry &= vbCrLf & "     INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON A.FTOrderNo = C.FTOrderNo"
        _Qry &= vbCrLf & " INNER JOIN ("
        _Qry &= vbCrLf & "  SELECT AA.FNHSysStyleId, AA.FNHSysSeasonId "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS AA WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE AA.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
        _Qry &= vbCrLf & " ) AS MMX ON C.FNHSysStyleId=MMX.FNHSysStyleId AND  C.FNHSysSeasonId =  MMX.FNHSysSeasonId"



        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then
            Return False
        Else

            If (ShowMsg) Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการสร้างเอกสาร MI แล้ว ไม่สามารถเปลี่ยนแปลงหรือแก้ไขข้อมูลได้ !!!", 1677710574, "Check Create MI", , MessageBoxIcon.Warning)
            End If

            Return True
        End If

    End Function


    Public Shared Function CheckExportMILineItem(_OrderNo As String, SubOrderNo As String, FTLineItemNo As String, Optional ShowMsg As Boolean = True) As Boolean
        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 A.FTOrderNo  "
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS A INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS B WITH(NOLOCK) ON A.FTPOref = B.FTCustomerPO "
        _Qry &= vbCrLf & "     INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON A.FTOrderNo = C.FTOrderNo"
        _Qry &= vbCrLf & "    INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS BD WITH(NOLOCK) ON B.FTCustomerPO = BD.FTCustomerPO "
        _Qry &= vbCrLf & "    AND B.FTInvoiceNo = BD.FTInvoiceNo "
        _Qry &= vbCrLf & "    AND A.FTColorway = BD.FTColorway "
        _Qry &= vbCrLf & "    AND A.FTSizeBreakDown = BD.FTSizeBreakDown "
        _Qry &= vbCrLf & "    AND A.FTNikePOLineItem = BD.FTPOLineItem "
        _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
        ' _Qry &= vbCrLf & " AND A.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"
        _Qry &= vbCrLf & " AND A.FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(FTLineItemNo) & "'"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then
            Return False
        Else

            If (ShowMsg) Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการสร้างเอกสาร MI แล้ว ไม่สามารถเปลี่ยนแปลงหรือแก้ไขข้อมูลได้ !!!", 1677710574, "Check Create MI", , MessageBoxIcon.Warning)
            End If

            Return True
        End If

    End Function

    Public Shared Function CheckExportMIChangeCMP(_FNHSysStyleId As Integer, _FNHSysSeasonId As Integer, Optional ShowMsg As Boolean = True) As Boolean

        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 A.FTOrderNo  "
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS A INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS B WITH(NOLOCK) ON A.FTPOref = B.FTCustomerPO "
        _Qry &= vbCrLf & "     INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON A.FTOrderNo = C.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE C.FNHSysStyleId=" & _FNHSysStyleId & "  "
        _Qry &= vbCrLf & "  AND C.FNHSysSeasonId =  " & _FNHSysSeasonId & ""

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then

            Return False

        Else

            If (ShowMsg) Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการสร้างเอกสาร MI แล้ว ไม่สามารถเปลี่ยนแปลงหรือแก้ไขข้อมูลได้ !!!", 1677710579, "Check Create MI", , MessageBoxIcon.Warning)
            End If

            Return True

        End If

    End Function

End Class

Public Class SAMPLEROOM

    Private _MessageCheck As String = ""
    Public Property MessageCheck As String
        Get
            Return _MessageCheck
        End Get
        Set(value As String)
            _MessageCheck = value
        End Set
    End Property


    Public Function GetStyleCodeByOrderNo_SMP(FTOrderNo As String) As String
        Dim _Qry As String

        _Qry = "  SELECT   TOP 1  B.FTStyleCode "
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS A WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId"
        _Qry &= vbCrLf & "  WHERE    (A.FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo) & "')"

        Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SAMPLE, "")
    End Function
    Public Function CheckOperationAfter_SMP(FTStyleCode As String, FTOrderProdNo As String, FTBarcodeNo As String, FNHSysOperationId As Integer, Quantity As Double) As Boolean

        Dim _Qry As String = ""
        Dim _CheckOperBefore As Integer = 0
        Dim dt As DataTable
        _Qry = "SELECT TOP 1 FTOrderProdNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

            _Qry = "  SELECT    A.FNHSysOperationId"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPRODMOperationByStyle AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
            _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleId=ISNULL((SELECT TOP 1 FNHSysStyleId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStylecode='" & HI.UL.ULF.rpQuoted(FTStyleCode) & "'),0) "
            _Qry &= vbCrLf & "   AND A.FNHSysOperationIdTo=" & Integer.Parse(FNHSysOperationId) & " "

        Else

            _Qry = " SELECT  A.FNHSysOperationId"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
            _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'  "
            _Qry &= vbCrLf & "   AND A.FNHSysOperationIdTo=" & Integer.Parse(FNHSysOperationId) & " "

        End If
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In dt.Rows
            _CheckOperBefore = Integer.Parse(Val(R!FNHSysOperationId.ToString))

            If Integer.Parse(_CheckOperBefore) > 0 Then

                Dim dtcheck As DataTable
                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_CheckOperationBefore_SMP '" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'," & Integer.Parse(_CheckOperBefore) & ",'" & HI.UL.ULF.rpQuoted(FTBarcodeNo) & "'"
                dtcheck = (HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE))

                If dtcheck.Rows.Count > 0 Then
                    If Val(dtcheck.Rows(0)!FNQuantity.ToString) <= Val(Quantity) Then
                        Me.MessageCheck = HI.MG.ShowMsg.GetMessage("ไม่สามารถ ลบได้  เนื่องจากพบ การ Scan ขั้นตอนถัดไปแล้ว !!!", 1409251104)
                        Return False
                    End If
                End If

                dtcheck.Dispose()
            End If

        Next

        Return True
    End Function


    Public Function GetStyleCodeByOrderNoSMP(FTOrderNo As String) As String
        Dim _Qry As String

        _Qry = "  SELECT   TOP 1  B.FTStyleCode "
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS A WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId"
        _Qry &= vbCrLf & "  WHERE    (A.FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo) & "')"

        Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SAMPLE, "")
    End Function
    Public Function GetOpertionName_SMP(OperationID As Integer) As String
        Try
            Dim _Qry As String

            _Qry = "SELECT TOP 1 "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " FTOperationNameTH  "
            Else
                _Qry &= vbCrLf & " FTOperationNameEN "
            End If
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysOperationId =" & OperationID.ToString & ""

            Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

        Catch ex As Exception
            Return ""
        End Try

    End Function
End Class

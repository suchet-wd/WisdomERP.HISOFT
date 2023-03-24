Public Class wAutoGenerateBarcodeCust 

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _StateProc As Boolean = False
    Public Property StateProc As Boolean
        Get
            Return _StateProc
        End Get
        Set(value As Boolean)
            _StateProc = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.StateProc = False
        Me.Close()
    End Sub

    Private Sub ocmcreate_Click(sender As Object, e As EventArgs) Handles ocmcreate.Click
        If GenerateBarcode() Then
            Me.StateProc = True
            HI.MG.ShowMsg.mInfo("สร้าง Barcode เรียบร้อยแล้ว !!!", 1506778946, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
            Me.Close()
        Else
            HI.MG.ShowMsg.mInfo("ระะบบไม่สามารถทำการสร้าง Barcode ได้ กรุณาทำการติดต่อ Admin !!!", 1506778947, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function GenerateBarcode() As Boolean
        Dim _Qry As String = ""
        Dim _BarCodeNo As String = ""
        Dim _dt As DataTable
        Dim _CmpH As String
        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")


        Select Case FNGenerateBarcodeCustType.SelectedIndex
            Case 0
                _BarCodeNo = HI.TL.Document.GetDocumentNo("HITECH_PRODUCTION", "TPRODTOrder_CustBarcode", "", False, _CmpH).ToString

                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & "  SELECT "
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ", A.FTOrderNo, A.FTSubOrderNo,A.FTColorway, A.FTSizeBreakDown"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_BarCodeNo) & "'"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                Return HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Case 1
                _Qry = "   SELECT A.FTColorway, A.FTSizeBreakDown, C.FNMatColorSeq, S.FNMatSizeSeq"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS S  WITH(NOLOCK)  ON A.FTSizeBreakDown = S.FTMatSizeCode LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS C  WITH(NOLOCK)  ON A.FTColorway = C.FTMatColorCode"
                _Qry &= vbCrLf & "  WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNo) & "')"
                _Qry &= vbCrLf & "  GROUP BY A.FTColorway, A.FTSizeBreakDown, C.FNMatColorSeq, S.FNMatSizeSeq"
                _Qry &= vbCrLf & "  ORDER BY C.FNMatColorSeq, S.FNMatSizeSeq"


                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                For Each R As DataRow In _dt.Rows
                    _BarCodeNo = HI.TL.Document.GetDocumentNo("HITECH_PRODUCTION", "TPRODTOrder_CustBarcode", "", False, _CmpH).ToString

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", A.FTOrderNo, A.FTSubOrderNo,A.FTColorway, A.FTSizeBreakDown"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_BarCodeNo) & "'"
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    _Qry &= vbCrLf & " AND A.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                    _Qry &= vbCrLf & " AND A.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                Next
                Return True
            Case 2
                _Qry = "   SELECT A.FTColorway,C.FNMatColorSeq"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS S  WITH(NOLOCK)  ON A.FTSizeBreakDown = S.FTMatSizeCode LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS C  WITH(NOLOCK)  ON A.FTColorway = C.FTMatColorCode"
                _Qry &= vbCrLf & "  WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNo) & "')"
                _Qry &= vbCrLf & "  GROUP BY A.FTColorway,  C.FNMatColorSeq"
                _Qry &= vbCrLf & "  ORDER BY C.FNMatColorSeq"


                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                For Each R As DataRow In _dt.Rows

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", A.FTOrderNo, A.FTSubOrderNo,A.FTColorway, A.FTSizeBreakDown"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_BarCodeNo) & "'"
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    _Qry &= vbCrLf & " AND A.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                   
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                Next

                Return True
            Case 3

                _Qry = "   SELECT A.FTSizeBreakDown,  S.FNMatSizeSeq"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS S  WITH(NOLOCK)  ON A.FTSizeBreakDown = S.FTMatSizeCode LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS C  WITH(NOLOCK)  ON A.FTColorway = C.FTMatColorCode"
                _Qry &= vbCrLf & "  WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNo) & "')"
                _Qry &= vbCrLf & "  GROUP BY  A.FTSizeBreakDown,  S.FNMatSizeSeq"
                _Qry &= vbCrLf & "  ORDER BY  S.FNMatSizeSeq"

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                For Each R As DataRow In _dt.Rows

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", A.FTOrderNo, A.FTSubOrderNo,A.FTColorway, A.FTSizeBreakDown"
                    _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(_BarCodeNo) & "'"
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    _Qry &= vbCrLf & " AND A.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                Next
                Return True
        End Select

        Return False
    End Function
End Class
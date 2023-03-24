Imports System.Windows.Forms

Public Class wSearchBarcodeAsset
    Private _ListBarcode As wFormListBarcodeTransaction
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ListBarcode = New wFormListBarcodeTransaction

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListBarcode.Name.ToString.Trim, _ListBarcode)
        Catch ex As Exception
        Finally
        End Try
        HI.TL.HandlerControl.AddHandlerObj(_ListBarcode)
        otbmainsearch.SelectedTabPage = otpsearch
    End Sub

    Private Sub ocmsearch_Click(sender As System.Object, e As System.EventArgs) Handles ocmfind.Click
        If Me.FTDocumentNo.Text <> "" Then
            Me.ogcdetail.DataSource = SearchData("", FTDocumentNo.Text, Val(FNHSysWHAssetId.Properties.Tag))
        Else
            HI.MG.ShowMsg.mProcessError(1401250001, "กรุณาทำการระบุเงื่อนไขอย่างน้อย 1 อย่าง ", Me.Text, Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub FTBarcodeNo_InvalidValue(sender As Object, e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles FTBarcodeNo.InvalidValue

    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    Me.ogcdetail.DataSource = SearchData(FTBarcodeNo.Text, "", 0)

                End If
        End Select
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmexporttoexcel_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As System.EventArgs) Handles ogvdetail.DoubleClick
        With Me.ogvdetail
            If .FocusedRowHandle < 0 Then Exit Sub
            Dim _BarcodeNo As String = "" & .GetFocusedRowCellValue("FTBarcodeNo").ToString

            Call HI.ST.Lang.SP_SETxLanguage(_ListBarcode)
            With _ListBarcode
                .BarcodeNo = _BarcodeNo
                .LoadList()
                .ShowDialog()
            End With
        End With
    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

        Dim cmd As String
        Dim dtselectbar As DataTable

        With CType(ogcdetail.DataSource, DataTable)
            .AcceptChanges()

            dtselectbar = .Copy
        End With

        If dtselectbar.Select("FTSelect='1'").Length > 0 Then

            cmd = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_INVEN)

            For Each R As DataRow In dtselectbar.Select("FTSelect='1'")

                cmd = " Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp "
                cmd &= vbCrLf & " SET FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                cmd &= vbCrLf & " WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN) = False Then

                    cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp (FTUserLogIn,FTBarcodeNo) "
                    cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN)

                End If

            Next

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "Search_Barcode.rpt"
                .Formular = " {TINVENPrintBarcode_Temp.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                .Preview()
            End With

        Else

        End If

        dtselectbar.Dispose()


    End Sub
    Function SearchData(BarKey As String, FTDoc As String, WHID As Integer) As DataTable
        Dim Qry As String = ""
        Qry = "Select A.FTBarcodeNo,A.FNHSysFixedAssetId,ASS.FTAssetCode,U.FTUnitAssetCode,W.FTWHAssetCode,SUM(A.FNQuantityIN) AS FNQuantityIN,SUM(A.FNQuantityOUT) AS FNQuantityOUT"
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
        Qry &= vbCrLf & "WHERE A.FTBarcodeNo <> ''"
        If BarKey <> "" Then
            Qry &= vbCrLf & "AND A.FTBarcodeNo = '" & BarKey & "'"
        End If
        If FTDoc <> "" Then
            Qry &= vbCrLf & "AND A.FTDocumentNo = '" & FTDoc & "'"
        End If
        If WHID <> 0 Then
            Qry &= vbCrLf & "AND A.FNHSysWHAssetId = " & WHID & ""
        End If
        Qry &= vbCrLf & "GROUP BY A.FTBarcodeNo,A.FNHSysFixedAssetId,ASS.FTAssetCode,U.FTUnitAssetCode,W.FTWHAssetCode,ASS.FTAssetNameTH,W.FTWHAssetNameTH"
        Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
    End Function
End Class
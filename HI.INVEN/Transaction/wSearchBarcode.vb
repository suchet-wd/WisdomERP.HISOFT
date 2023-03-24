Imports System.Windows.Forms

Public Class wSearchBarcode
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

    End Sub

    Private Sub ocmsearch_Click(sender As System.Object, e As System.EventArgs) Handles ocmfind.Click

        If Me.FTOrderNo.Text <> "" Or FNHSysWHId.Text <> "" Or FNHSysMainMatId.Text <> "" Or FNHSysRawMatColorId.Text <> "" Or FNHSysRawMatSizeId.Text <> "" Then

            Dim _spls As New HI.TL.SplashScreen("Loading...")

            Try
                Me.ogcdetail.DataSource = HI.INVEN.Barcode.SearchData("", Val(FNHSysWHId.Properties.Tag), Me.FTOrderNo.Text, Val(0), FNHSysMainMatId.Text, Val(FNHSysRawMatColorId.Properties.Tag.ToString), Val(FNHSysRawMatSizeId.Properties.Tag.ToString), FNSearchBarcodeType.SelectedIndex)
            Catch ex As Exception

            End Try

            _spls.Close()
        Else

            HI.MG.ShowMsg.mProcessError(1401250001, "กรุณาทำการระบุเงื่อนไขอย่างน้อย 1 อย่าง ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)

        End If

    End Sub

    Private Sub FTBarcodeNo_InvalidValue(sender As Object, e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles FTBarcodeNo.InvalidValue
    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    Me.ogcdetail.DataSource = HI.INVEN.Barcode.SearchData(FTBarcodeNo.Text, 0, "", 0)

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

    Private Sub ocmpreviewbarcodewipselect_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipselect.Click

        Dim cmd As String
        Dim dtselectbar As DataTable

        With CType(ogcdetail.DataSource, DataTable)
            .AcceptChanges()

            dtselectbar = .Copy
        End With

        If dtselectbar.Select("FTSelect='1'").Length > 0 Then

            cmd = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcodeSearch_Temp WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_INVEN)
            Dim _odtbarcode As DataTable
            For Each R As DataRow In dtselectbar.Select("FTSelect='1'")
                _odtbarcode = HI.Conn.SQLConn.GetDataTable("select top 1   *   from dbo.GetOnhandBarcode_Search('" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "' ,'" & HI.UL.ULF.rpQuoted(R!FTWHNo.ToString) & "') as t ", Conn.DB.DataBaseName.DB_INVEN)
                cmd = " Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcodeSearch_Temp "
                cmd &= vbCrLf & " SET FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                ' cmd &= vbCrLf & " , FTWHNo ='" & HI.UL.ULF.rpQuoted(R!FTWHNo.ToString) & "'"
                cmd &= vbCrLf & " , FTOrderInfo ='" & HI.UL.ULF.rpQuoted(_odtbarcode.Rows(0).Item("FTOrderInfo").ToString) & "'"
                cmd &= vbCrLf & " , FNQuantityBalInfo =" & Val(_odtbarcode.Rows(0).Item("FNQuantityBalInfo"))


                cmd &= vbCrLf & " WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                cmd &= vbCrLf & " AND FTWHNo='" & HI.UL.ULF.rpQuoted(R!FTWHNo.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN) = False Then

                    cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcodeSearch_Temp (FTUserLogIn,FTBarcodeNo ,FTWHNo, FTOrderInfo , FNQuantityBalInfo) "
                    cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTWHNo.ToString) & "'"
                    cmd &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(_odtbarcode.Rows(0).Item("FTOrderInfo").ToString) & "'"
                    cmd &= vbCrLf & " , " & Val(_odtbarcode.Rows(0).Item("FNQuantityBalInfo"))
                    HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN)

                End If

            Next

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "Search_Barcode_Reprint.rpt"
                .Formular = " {TINVENPrintBarcode_Temp.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                .Preview()
            End With

        Else

        End If

        dtselectbar.Dispose()


    End Sub
End Class
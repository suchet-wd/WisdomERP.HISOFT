Imports System.Windows.Forms

Public Class wSearchBarcodeAcc
    ' Private _ListBarcode As wFormListBarcodeTransaction
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        '_ListBarcode = New wFormListBarcodeTransaction

        'Dim oSysLang As New ST.SysLanguage
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListBarcode.Name.ToString.Trim, _ListBarcode)
        'Catch ex As Exception
        'Finally
        'End Try
        'HI.TL.HandlerControl.AddHandlerObj(_ListBarcode)

    End Sub

    Private Sub ocmsearch_Click(sender As System.Object, e As System.EventArgs) Handles ocmfind.Click

        If Me.FTOrderNo.Text <> "" Or FNHSysWHId.Text <> "" Or FNHSysMainMatId.Text <> "" Or FNHSysRawMatColorId.Text <> "" Or FNHSysRawMatSizeId.Text <> "" Then

            Dim Spls As New HI.TL.SplashScreen("Data Loading ......")

            Try
                Me.ogcdetail.DataSource = HI.INVEN.Barcode.SearchDataForAcc("", Val(FNHSysWHId.Properties.Tag), Me.FTOrderNo.Text, Val(0), FNHSysMainMatId.Text, Val(FNHSysRawMatColorId.Properties.Tag.ToString), Val(FNHSysRawMatSizeId.Properties.Tag.ToString), HI.UL.ULDate.ConvertEnDB(FTEndDate.Text), FNHSysWHId.Text.Trim(), FNHSysWHIdTo.Text.Trim())
            Catch ex As Exception
            End Try

            Spls.Close()

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
                    Dim Spls As New HI.TL.SplashScreen("Data Loading ......")
                    Me.ogcdetail.DataSource = HI.INVEN.Barcode.SearchDataForAcc(FTBarcodeNo.Text, 0, "", 0, "", 0, 0, HI.UL.ULDate.ConvertEnDB(FTEndDate.Text))
                    Spls.Close()
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

    Private Sub ogvdetail_DoubleClick(sender As Object, e As System.EventArgs)

        'With Me.ogvdetail
        '    If .FocusedRowHandle < 0 Then Exit Sub
        '    Dim _BarcodeNo As String = "" & .GetFocusedRowCellValue("FTBarcodeNo").ToString
        '    Call HI.ST.Lang.SP_SETxLanguage(_ListBarcode)
        '    With _ListBarcode
        '        .BarcodeNo = _BarcodeNo
        '        .LoadList()
        '        .ShowDialog()
        '    End With
        'End With

    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs)

    End Sub
End Class
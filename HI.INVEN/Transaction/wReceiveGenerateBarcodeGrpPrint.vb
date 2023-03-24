Imports System.Windows.Forms

Public Class wReceiveGenerateBarcodeGrpPrint

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub ocmauto_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With New HI.RP.Report
            .FormTitle = Me.Text
            .ReportFolderName = "Inventrory\"
            .ReportName = "BarcodeGrpSlip.rpt"
            .Formular = "{TINVENBarcode.FTBarcodeGrpNo}='" & HI.UL.ULF.rpQuoted(FTBarcodeGrpNo.Text) & "' "
            .Preview()
        End With


    End Sub

    Private Sub wReceiveAutoTransferToCenter_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

End Class
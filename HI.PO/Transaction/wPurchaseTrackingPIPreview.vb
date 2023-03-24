Imports System.Windows.Forms
Imports System.Windows.Forms.Control

Public Class wPurchaseTrackingPIPreview



#Region "Event Handle"

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        If SFTDateTrans.Text <> "" And EFTDateTrans.Text <> "" Then

            Dim spls As New HI.TL.SplashScreen("Loading....")
            Dim cmdstring As String = ""
            cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CREATEDATAPIREPORT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULDate.ConvertEnDB(SFTDateTrans.Text) & "','" & HI.UL.ULDate.ConvertEnDB(EFTDateTrans.Text) & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            spls.Close()
            With New HI.RP.Report


                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PISummaryReport.rpt"

                .Formular = "{TTMPPIReport.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                .Preview()


            End With


        End If
    End Sub

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub wCopyOrderSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#End Region

End Class
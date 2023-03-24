Public Class MainForm
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Application.Exit()
    End Sub

    Private Sub ocmcomport_Click(sender As Object, e As EventArgs) Handles ocmcomport.Click
        With New ComportReceiver

            .WindowState = FormWindowState.Maximized
            .ShowDialog()

        End With
    End Sub

    Private Sub ocmtcpip_Click(sender As Object, e As EventArgs) Handles ocmtcpip.Click
        'With New TCPReceiver

        '    .WindowState = FormWindowState.Maximized
        '    .ShowDialog()

        'End With

        With New ServerTCP

            .WindowState = FormWindowState.Maximized
            .ShowDialog()

        End With
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        HI.Conn.DB.GetXmlConnectionString()
        ACKData.ReadACKData()


        If HI.Conn.SQLConn.GetField("Select Convert(varchar(10),GetDate(),111)", HI.Conn.DB.DataBaseName.DB_SYSTEM) = "" Then

            ACKData.StateConnectToServer = False

        Else

            ACKData.StateConnectToServer = True

        End If

        'Dim StartOfPacket As Char = Chr(CInt("0x02"))
        'Dim EndOfPacket As Char = Chr(CInt("0x03"))

        'Dim X As String = "999"

        'Dim X1 As String = Chr(2)
        'Dim X2 As String = Chr(3)
        ocmtcpipsocket.PerformClick()
    End Sub

    Private Sub ocmtcpipsocket_Click(sender As Object, e As EventArgs) Handles ocmtcpipsocket.Click
        With New ServerTCPSocketNew

            .WindowState = FormWindowState.Maximized
            .ShowDialog()

        End With
    End Sub
End Class
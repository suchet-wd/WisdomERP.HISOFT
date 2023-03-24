
Imports System.Net.Sockets

Public Class ControlData

    Public Shared Sub ReadDataACK()
    End Sub

    Public Sub SendACKDataFromSerial(com1 As System.IO.Ports.SerialPort)
        ' Send strings to a serial port.
        Using com1
            com1.WriteLine(ACKData.GetACKData())
        End Using
    End Sub

    Public Sub SendACKDataFromTCP(ipcontrol As String)
        Dim clientSocket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        clientSocket.Connect(New System.Net.IPEndPoint(System.Net.IPAddress.Parse(ipcontrol), 8080))

        Try
            clientSocket.Send(System.Text.Encoding.ASCII.GetBytes(ACKData.GetACKData()))
        Catch ex As Exception

        Finally
            clientSocket.Close()
        End Try

    End Sub

End Class

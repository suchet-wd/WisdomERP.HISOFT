Public Class ACKData
    Private Shared DataACK As String = ""
    Private Shared DataACKByte As Byte()

    Private Shared filebufferSend As Byte()
    Private Shared fileStreamSend As System.IO.Stream

    Public Shared StateConnectToServer As Boolean = False

    Public Shared Sub ReadACKData()
        DataACK = System.IO.File.ReadAllText(Application.StartupPath + "\Acknowledge_to_control_box.txt")
        DataACKByte = System.Text.Encoding.ASCII.GetBytes(DataACK)

        fileStreamSend = System.IO.File.OpenRead(Application.StartupPath + "\Acknowledge_to_control_box.txt")
        ' Alocate memory space for the file
        ReDim filebufferSend(fileStreamSend.Length)
        fileStreamSend.Read(filebufferSend, 0, fileStreamSend.Length)

    End Sub

    Public Shared Function GetACKData() As String
        Return DataACK
    End Function

    Public Shared Function GetACKByteArrayData() As Byte()
        Return DataACKByte
    End Function

    Public Shared Function GetACKFileBufferSend() As Byte()
        Return filebufferSend
    End Function

    Public Shared Function GetACKFileStreamSend() As System.IO.Stream
        Return fileStreamSend
    End Function


    Public Shared Function ConnectToServer() As Boolean
        Return StateConnectToServer
    End Function



End Class

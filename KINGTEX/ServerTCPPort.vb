Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text
Imports System.Net
Imports System.Diagnostics

Public Class ServerTCPPort
    Dim trlisten As Thread
    Public ContextMenu1 As New ContextMenu
    Dim DataAll As String
    Public PortNumber As Integer
    Dim Icount As Integer = 0
    Dim trSave As Thread
    Dim sql As String
    'Dim c As New Cls_Query
    Dim CheckDataIns As Boolean = True
    Public Sub CreateIconMenuStructure()
        '  NotifyIcon1.ContextMenu = ContextMenu1
    End Sub


    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            trlisten.Abort()
        Catch ex As Exception
        End Try
        'Me.NotifyIcon1.Visible = False
        'Me.NotifyIcon1.Dispose()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US", True)
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
        '  Me.NotifyIcon1.Icon = Me.Icon
        ' Me.NotifyIcon1.Text = Me.PortNumber
        Me.Text = "Receive Data From Port No  " & PortNumber.ToString
        My.Application.DoEvents()
        Process.GetCurrentProcess.PriorityBoostEnabled = True

        trlisten = New Thread(AddressOf ListenToServer)
        ' trlisten = New Thread(AddressOf ListemDataFromClient)
        trlisten.Start()

        'Me.Show()
        ' ListenToServer()
        'CreateIconMenuStructure()
    End Sub

    Sub ListenToServer()
        'Try
        Dim Ime As String = ""
        Dim Flag As String
        Dim sendfile As Boolean = False
        Dim LISTENING As Boolean

        ' Dim localhostAddress As IPAddress = ipAddress.Parse(ipAddress.ToString)

        Dim port As Integer = PortNumber

        '' PORT ADDRESS
        ''''''''''' making socket tcpList ''''''''''''''''
        'Dim tcpList As New TcpListener( localhostAddress, port)

        Try

            Dim tcpList As New TcpListener(port)

            tcpList.Start()
            LISTENING = True

            Do While LISTENING

                Do While tcpList.Pending = False And LISTENING = True
                    ' Yield the CPU for a while.
                    Thread.Sleep(10)
                Loop

                If Not LISTENING Then Exit Do

                Dim tcpCli As TcpClient = tcpList.AcceptTcpClient()
                Dim ns As NetworkStream = tcpCli.GetStream
                Dim reader As StreamReader = New StreamReader(ns)
                Dim writer As StreamWriter = New StreamWriter(ns)
                writer.AutoFlush = True

                Dim bytes(tcpCli.ReceiveBufferSize) As Byte

                'ns.Read(bytes, 0, CInt(tcpCli.ReceiveBufferSize))
                ' Return the data received from the client to the console.
                Dim receivedData As String = "" 'Encoding.UTF7.GetString(bytes)
                Dim recv As Integer = 0

                Icount = 0
                CheckDataIns = True


                While True

                    'System.Text.Encoding.ASCII.GetBytes


                    recv = ns.Read(bytes, 0, CInt(tcpCli.ReceiveBufferSize))

                    Dim data As String = System.Text.Encoding.ASCII.GetString(bytes, 0, recv)

                    receivedData = receivedData & data 'System.Text.Encoding.ASCII.GetString(bytes, 0, recv)

                    If receivedData <> "" Then
                        Try

                            If receivedData.Contains(ChrW(3)) And receivedData.Contains(ChrW(1)) Then

                                Dim returnedData As String = "�{F2INF=10,�"
                                Dim respondData As Byte() = Encoding.ASCII.GetBytes(ACKData.GetACKData)

                                If returnedData <> "" Then

                                    'Dim filebuffer As Byte()
                                    'Dim fileStream As Stream
                                    'fileStream = File.OpenRead(Application.StartupPath + "\Acknowledge_to_control_box.txt")
                                    '' Alocate memory space for the file
                                    'ReDim filebuffer(fileStream.Length)
                                    'fileStream.Read(filebuffer, 0, fileStream.Length)
                                    'ns.Write(filebuffer, 0, fileStream.Length)



                                    Dim filebufferSend As Byte()
                                    Dim fileStreamSend As Stream
                                    fileStreamSend = File.OpenRead(Application.StartupPath + "\Acknowledge_to_control_box.txt")
                                    ' Alocate memory space for the file
                                    ReDim filebufferSend(fileStreamSend.Length)
                                    fileStreamSend.Read(filebufferSend, 0, fileStreamSend.Length)
                                    ns.Write(filebufferSend, 0, fileStreamSend.Length)



                                    'Dim sw As New StreamWriter(ns)
                                    'ns.Write(respondData, 0, respondData.Length)
                                    'writer.WriteLine(returnedData)

                                    SetText(receivedData)
                                End If

                                'Dim respondData As Byte() = Encoding.ASCII.GetBytes(ACKData.GetACKData)
                                'Stream.Write(respondData, 0, respondData.Length)

                                'If Microsoft.VisualBasic.Left(receivedData, 1) = ChrW(1) And Microsoft.VisualBasic.Right(receivedData, 1) = ChrW(3) Then
                                '    receivedData = ""

                                'End If

                                receivedData = ""
                                Exit While


                            End If

                        Catch ex As Exception
                            Exit While
                        End Try

                    End If


                End While

                ns.Close()
                tcpCli.Close()

            Loop
            tcpList.Stop()
        Catch ex As Exception
            'error
            LISTENING = False
        End Try
    End Sub

    Private Delegate Sub AppendTextBoxDelegate(ByVal TB As RichTextBox, ByVal txt As String)

    Private Sub AppendTextBox(ByVal TB As RichTextBox, ByVal txt As String)
        If TB.InvokeRequired Then
            TB.Invoke(New AppendTextBoxDelegate(AddressOf AppendTextBox), New Object() {TB, txt})
        Else
            TB.AppendText(txt)
        End If
    End Sub


    Sub ListemDataFromClient()

        Dim port As Integer = PortNumber
        Try
            Dim tcpList As New TcpListener(port)

            tcpList.Start()

            Dim received As String = ""

            Try
                While True
                    Dim _TcpClient As TcpClient = tcpList.AcceptTcpClient()

                    Dim Stream As NetworkStream = _TcpClient.GetStream()
                    Dim reader As StreamReader = New StreamReader(Stream)
                    Dim writer As StreamWriter = New StreamWriter(Stream)
                    writer.NewLine = vbCr & vbLf
                    writer.AutoFlush = True
                    Dim bytes As Byte() = New Byte(_TcpClient.SendBufferSize - 1) {}
                    Dim recv As Integer = 0

                    While True

                        'System.Text.Encoding.ASCII.GetBytes

                        recv = Stream.Read(bytes, 0, _TcpClient.SendBufferSize)
                        Dim data As String = System.Text.Encoding.ASCII.GetString(bytes, 0, recv)
                        TReceive.Text = TReceive.Text & data

                        received = received & data

                        If (bytes.Length > 2) Then

                            Dim B1 As Byte = bytes(bytes.Length - 1)
                            Dim B2 As Byte = bytes(bytes.Length - 2)
                            Dim B3 As Byte = bytes(bytes.Length - 3)

                            Dim C1 As Char = Chr(bytes(0))
                            Dim C2 As Char = Chr(bytes(1))
                            Dim C3 As Char = Chr(bytes(2))
                            Dim C4 As Char = Chr(bytes(bytes.Length - 1))
                            Dim C5 As Char = Chr(bytes(bytes.Length - 2))

                            If received.EndsWith(vbLf & vbLf) Or received.EndsWith(Chr(3)) Or Chr(bytes(bytes.Length - 1)) = Chr(3) Then

                                'Byte[] respondData = Encoding.ASCII.GetBytes("respond");
                                'Array.Resize(ref respondData, 16); // Resizing To 16 Byte, because In this example all messages have 16 Byte To make it easier To understand.
                                'BinaryWriter.Write(respondData, 0, 16);

                                Dim respondData As Byte() = Encoding.ASCII.GetBytes("�{F2INF=10,�")
                                Stream.Write(respondData, 0, respondData.Length)


                                'Dim clientSocket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                                'clientSocket.Connect(New System.Net.IPEndPoint(System.Net.IPAddress.Parse("192.168.168.104"), 0))

                                'Try
                                '    clientSocket.Send(respondData)
                                'Catch ex As Exception

                                'Finally
                                '    clientSocket.Close()
                                'End Try

                                SetDataText(received)
                                received = ""
                                Exit While

                            End If
                        End If

                    End While


                End While

                tcpList.Stop()
            Catch ex As Exception
                tcpList.Stop()
            End Try
        Catch ex As Exception
            ' Me.Close()
        End Try


    End Sub

    Delegate Sub SetDataTextCallback(ByVal [text] As String)
    Private Sub SetDataText(ByVal [text] As String)

        If Me.InvokeRequired Then

            Dim d As New SetDataTextCallback(AddressOf SetDataText)
            Me.Invoke(d, New Object() {[text]})

        Else

            Dim data As String
            data = [text]

            Try

                TReceive.Text = TReceive.Text & data

            Catch ex As Exception
            End Try

        End If

    End Sub



    Delegate Sub SetTextCallback(ByVal [text] As String)

    Private Sub SetText(ByVal [text] As String)


        ' Me.TReceive.Text &= [text] & vbCrLf

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.

        If Me.InvokeRequired Then

            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text]})

        Else

            CheckDataIns = True

            Dim CtrlData As String = [text]
            Dim DataArr As String() = CtrlData.Split(",")

            Dim Str(2) As String
            Str(0) = DateTime.Now.ToString("HH:mm:ss")
            Str(1) = PortNumber.ToString
            Str(2) = CtrlData

            ServerTCP.GData.Rows.Add(CtrlData)

            Try
                'Dim MType As String = DataArr(0).Split("=")(1)
                'With New ControlMessageData

                '    Select Case Val(MType)
                '        Case 1
                '            .MessageDataType01(ControlData, DataArr)
                '        Case 2
                '            .MessageDataType02(ControlData, DataArr)
                '        Case 3
                '            .MessageDataType03(ControlData, DataArr)
                '        Case 4
                '            .MessageDataType04(ControlData, DataArr)
                '        Case 5
                '            .MessageDataType05(ControlData, DataArr)
                '        Case 6
                '            .MessageDataType06(ControlData, DataArr)
                '        Case 7
                '            .MessageDataType07(ControlData, DataArr)
                '        Case 8
                '            .MessageDataType08(ControlData, DataArr)
                '        Case 9
                '            .MessageDataType09(ControlData, DataArr)
                '        Case 10
                '            .MessageDataType10(ControlData, DataArr)
                '    End Select

                'End With
            Catch ex As Exception
            End Try


        End If


    End Sub
   

    
    Private currentWindowState As FormWindowState

    Public Sub New()

        ' This call is required by the designer.
        Dim x As String
        Try
            InitializeComponent()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        'If Me.WindowState = FormWindowState.Minimized Then
        Me.Hide()
        '   Me.NotifyIcon1.Visible = True
        'Else
        ' จำค่าปัจจุบันก่อนจะ Minimize ไว้
        'currentWindowState = Me.WindowState
        'End If

    End Sub

    'Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
    '    If Me.WindowState = FormWindowState.Minimized Then
    '        Me.NotifyIcon1.Visible = False
    '        Me.Show()
    '        Me.WindowState = Me.currentWindowState
    '    End If

    'End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        My.Application.DoEvents()
        'Icount += 1
        'Me.Text = Icount
        'If Icount > 120 Then
        '    Dim Str(0) As String
        '    Str(0) = Me.PortNumber
        '    AllportOpen.GPortClose.Rows.Add(Str(0))
        '    '  Me.NotifyIcon1.Visible = False
        '    'trlisten.Abort()
        '    'Thread.Sleep(10)
        '    '  Me.Show()
        '    'Me.WindowState = Me.currentWindowState
        '    Me.Close()
        'End If

    End Sub

End Class

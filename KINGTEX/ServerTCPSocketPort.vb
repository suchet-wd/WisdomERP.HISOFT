Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text
Imports System.Net
Imports System.Diagnostics

Public Class ServerTCPSocketPort
    Dim trlisten As Thread
    Public ContextMenu1 As New ContextMenu
    Dim DataAll As String
    Public PortNumber As Integer
    Dim Icount As Integer = 0
    Dim trSave As Thread
    Dim sql As String
    'Dim c As New Cls_Query
    Dim CheckDataIns As Boolean = True
    Private tcpLsn As New System.Net.Sockets.TcpListener(8080)

    Dim socketHolder As New Hashtable()
    Dim threadHolder As New Hashtable()
    Private connectId As Long = 0
    Private MaxConnected As Integer = 400
    Private HighLightDelay As Integer = 300

    Public Sub New(TCPPort As Integer)

        ' This call is required by the designer.
        Dim x As String
        Try
            InitializeComponent()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


        ' Add any initialization after the InitializeComponent() call.
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US", True)
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
        '  Me.NotifyIcon1.Icon = Me.Icon
        ' Me.NotifyIcon1.Text = Me.PortNumber
        Me.Text = "Receive Data From Port No  " & PortNumber.ToString
        My.Application.DoEvents()
        Process.GetCurrentProcess.PriorityBoostEnabled = True

        Dim port As Integer = TCPPort

        Try
            tcpLsn.Stop()
        Catch ex As Exception
        End Try

        tcpLsn = New System.Net.Sockets.TcpListener(port)
        tcpLsn.Start()
        ' tcpLsn.LocalEndpoint may have a bug, it only show 0.0.0.0:8002      
        'stpanel.Text = "Listen at: " + tcpLsn.LocalEndpoint.ToString();        
        trlisten = New Thread(AddressOf WaitingForClient)
        threadHolder.Add(connectId, trlisten)
        trlisten.Start()
    End Sub


    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            trlisten.Abort()
        Catch ex As Exception
        End Try

        Try
            CloseTheThreadAll()
        Catch ex As Exception
        End Try

        'Me.NotifyIcon1.Visible = False
        'Me.NotifyIcon1.Dispose()

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Sub WaitingForClient()
        While True
            ' Accept will block until someone connects
            Try
                Dim sckt As Socket = tcpLsn.AcceptSocket()
                If connectId < 10000 Then
                    Interlocked.Increment(connectId)
                Else
                    connectId = 1
                End If
                If socketHolder.Count < MaxConnected Then
                    While socketHolder.Contains(connectId)
                        Interlocked.Increment(connectId)
                    End While

                    Dim td As New Thread(AddressOf ReadSocket)
                    SyncLock Me
                        ' it is used to keep connected Sockets
                        socketHolder.Add(connectId, sckt)
                        ' it is used to keep the active thread
                        threadHolder.Add(connectId, td)


                    End SyncLock
                    td.Start()
                End If

            Catch ex As Exception
            End Try

        End While
    End Sub

    Sub ReadSocket()
        ' realId will be not changed for each thread, 
        ' but connectId is changed. it can't be used to delete object from Hashtable
        Dim realId As Long = connectId
        Dim ind As Integer = -1
        Dim s As Socket = DirectCast(socketHolder(realId), Socket)

        If Not (s Is Nothing) Then
            While True
                If s.Connected Then
                    Dim receive As [Byte]() = New [Byte](4096) {}
                    'Try
                    '    ' Receive will block until data coming
                    '    ' ret is 0 or Exception happen when Socket connection is broken
                    '    Dim ret As Integer = s.Receive(receive, receive.Length, 0)
                    '    If ret > 0 Then
                    '        Dim tmp As String = ""
                    '        tmp = System.Text.Encoding.ASCII.GetString(receive, 0, ret)
                    '        If tmp.Length > 0 Then

                    '            Dim strArry As String() = tmp.Split(",")

                    '            Dim filebufferSend As Byte()
                    '            Dim fileStreamSend As Stream
                    '            fileStreamSend = File.OpenRead(Application.StartupPath + "\Acknowledge_to_control_box.txt")
                    '            ' Alocate memory space for the file
                    '            ReDim filebufferSend(fileStreamSend.Length)
                    '            fileStreamSend.Read(filebufferSend, 0, fileStreamSend.Length)

                    '            s.Send(filebufferSend, filebufferSend.Length, 0)

                    '            SetText(tmp)

                    '        End If


                    '    End If
                    'Catch e As Exception
                    'End Try

                    Try
                        ' Receive will block until data coming
                        ' ret is 0 or Exception happen when Socket connection is broken
                        '''Dim ret As Integer = s.Receive(receive, receive.Length, 0)
                        '''If ret > 0 Then
                        '''    Dim tmp As String = ""
                        '''    tmp = System.Text.Encoding.ASCII.GetString(receive, 0, ret)
                        '''    If tmp.Length > 0 Then

                        '''        Dim strArry As String() = tmp.Split(",")

                        '''        Dim filebufferSend As Byte()
                        '''        Dim fileStreamSend As Stream
                        '''        fileStreamSend = File.OpenRead(Application.StartupPath + "\Acknowledge_to_control_box.txt")
                        '''        ' Alocate memory space for the file
                        '''        ReDim filebufferSend(fileStreamSend.Length)
                        '''        fileStreamSend.Read(filebufferSend, 0, fileStreamSend.Length)

                        '''        s.Send(filebufferSend, filebufferSend.Length, 0)

                        '''        SetText(tmp)

                        '''    End If


                        '''End If

                        Dim receivedData As String = ""

                        While True

                            'System.Text.Encoding.ASCII.GetBytes


                            ' Dim ret As Integer = s.Receive(receive, receive.Length, 0)

                            'Dim data As String = System.Text.Encoding.ASCII.GetString(receive, 0, ret)
                            Dim ret As Integer = s.Receive(receive, receive.Length, 0)
                            If ret > 0 Then
                                Dim data As String = System.Text.Encoding.ASCII.GetString(receive, 0, ret)

                                receivedData = receivedData & data 'System.Text.Encoding.ASCII.GetString(bytes, 0, recv)

                                If receivedData <> "" Then
                                    Try

                                        If receivedData.Contains(ChrW(3)) And receivedData.Contains(ChrW(1)) Then



                                            Dim filebufferSend As Byte()
                                            Dim fileStreamSend As Stream
                                            fileStreamSend = File.OpenRead(Application.StartupPath + "\Acknowledge_to_control_box.txt")
                                            ' Alocate memory space for the file
                                            ReDim filebufferSend(fileStreamSend.Length)
                                            fileStreamSend.Read(filebufferSend, 0, fileStreamSend.Length)

                                            s.Send(filebufferSend, filebufferSend.Length, 0)

                                            SetText(s.RemoteEndPoint.ToString, receivedData)



                                            receivedData = ""
                                            Exit While


                                        End If

                                    Catch ex As Exception
                                        Exit While
                                    End Try

                                End If
                            End If



                        End While
                    Catch e As Exception
                    End Try
                End If
            End While

        End If
        CloseTheThread(realId)

    End Sub

    Delegate Sub SetTextCallback(ByVal [ip] As String, ByVal [text] As String)

    Private Sub SetText(ByVal [ip] As String, ByVal [text] As String)
        Try
            ' Me.TReceive.Text &= [text] & vbCrLf

            ' InvokeRequired required compares the thread ID of the
            ' calling thread to the thread ID of the creating thread.
            ' If these threads are different, it returns true.

            If Me.InvokeRequired Then

                Dim d As New SetTextCallback(AddressOf SetText)
                Me.Invoke(d, New Object() {[ip], [text]})

            Else

                CheckDataIns = True

                Dim data, RCVDATE, RCVTIME As String
                data = ""
                RCVDATE = ""
                RCVTIME = ""
                RCVDATE = DateTime.Now.ToString("dd/MM/yyyy")
                RCVTIME = DateTime.Now.ToString("HH:mm:ss")
                data = [text]

                Dim Str(2) As String
                Str(0) = RCVTIME
                Str(1) = PortNumber.ToString + " Client IP " & [ip].ToString
                Str(2) = data

                ' ServerTCPSocket.GData.BeginEdit()
                ServerTCPSocket.GData.Rows.Add(Str)
                'ServerTCPSocket.GData.EndEdit()
                ' ServerTCPSocket.GData.Refresh()
                'ServerTCPSocket.GData.DataSource = Not
                'Dim c As Integer = ServerTCPSocket.GData.RowCount()

                'Dim datax As DataTable
                'With DirectCast(ServerTCPSocket.GData.DataSource, DataTable)
                '    .AcceptChanges()
                '    '    datax = .Copy()
                'End With
                'Dim _smg As String = ""
                'Me.TReceive.Text = Me.TReceive.Text & DataAll

            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try




    End Sub


    Private Sub CloseTheThread(realId As Long)
        Try
            Dim thd As Thread = DirectCast(threadHolder(realId), Thread)
            SyncLock Me
                socketHolder.Remove(realId)
                threadHolder.Remove(realId)
            End SyncLock
            thd.Abort()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CloseTheThreadAll()

        Try

            For Each xkey As Object In threadHolder.Keys
                Dim realId As Long = Val(xkey)

                Dim thd As Thread = DirectCast(threadHolder(realId), Thread)
                SyncLock Me
                    socketHolder.Remove(realId)
                    threadHolder.Remove(realId)
                End SyncLock
                thd.Abort()
            Next

        Catch ex As Exception
        End Try

    End Sub


End Class

Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text
Imports System.Net
Imports System.Diagnostics


Public Class ServerTCPSocketNew
    Dim TimeClose As Integer = 0
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

    Sub New()

        ' CheckProcess()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub BOpenPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BOpenPort.Click

        Me.GData.Rows.Clear()
        GPortClose.Rows.Clear()

        PortNumber = Val(TStart.Text)
        ' Add any initialization after the InitializeComponent() call.
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US", True)
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
        '  Me.NotifyIcon1.Icon = Me.Icon
        ' Me.NotifyIcon1.Text = Me.PortNumber

        Me.Text = "Receive Data From Port No  " & PortNumber.ToString & "( Connect to Server = " & IIf(ACKData.ConnectToServer, "Yes", "No") & ")"
        My.Application.DoEvents()
        Process.GetCurrentProcess.PriorityBoostEnabled = True

        Dim port As Integer = PortNumber

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

        Me.Timer1.Enabled = True

        TStart.Enabled = False
        BOpenPort.Enabled = False
        BStopPort.Enabled = True

        Dim Str(2) As String
        Str(0) = DateTime.Now.ToString("HH:mm:ss")
        Str(1) = PortNumber.ToString
        Str(2) = "Port is Open Ok "
        Me.GData.Rows.Add(Str)

    End Sub



    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        My.Application.DoEvents()


    End Sub

    Private Sub AllportOpen_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            trlisten.Abort()
        Catch ex As Exception
        End Try

        Try
            CloseTheThreadAll()
        Catch ex As Exception
        End Try


        Application.Exit()

    End Sub

    Private Sub AllportOpen_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub AllportOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TStart.Enabled = True
        BOpenPort.Enabled = True
        BStopPort.Enabled = False

        BOpenPort.PerformClick()

    End Sub

    Private Sub GData_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GData.CellClick
        'Me.TData.Text = Me.GData.CurrentRow.Cells(e.ColumnIndex).Value.ToString.Length
        Try

            Me.TData.Text = ""
            Dim str As String = CStr(Me.GData.CurrentRow.Cells(e.ColumnIndex).Value)
            'For i As Integer = 0 To Me.GData.CurrentRow.Cells(e.ColumnIndex).Value.ToString.Length - 1
            '    Me.TData.Text &= (CStr(Me.GData.CurrentRow.Cells(e.ColumnIndex).Value).Substring(i, 1))
            '    My.Application.DoEvents()
            'Next
            ''str = str.Replace(" ", "")
            Me.TData.Text = str ' StringToHex(Str)

        Catch ex As Exception
        End Try
    End Sub


    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        My.Application.DoEvents()

        TimeClose += 1
        ' Me.Text = "  NEW " & Date.Now.ToString("dd MMMM yyyy HH:mm:ss") & "  " & TimeClose
        Me.Refresh()

        If TimeClose > 1800 Then

        End If

    End Sub



    Private Sub GData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GData.CellContentClick

    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try


            If Me.GData.RowCount > 100 Then
                Me.GData.Rows.Clear()
            End If
        Catch ex As Exception

        End Try


        Try


            If Me.GPortClose.RowCount > 100 Then
                Me.GPortClose.Rows.Clear()
            End If
        Catch ex As Exception

        End Try
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

                    SetAddressText(s.RemoteEndPoint.ToString)

                    Dim receive As [Byte]() = New [Byte](4096) {}

                    Try

                        Dim receivedData As String = ""

                        While True

                            'System.Text.Encoding.ASCII.GetBytes

                            ' Dim ret As Integer = s.Receive(receive, receive.Length, 0)

                            'Dim data As String = System.Text.Encoding.ASCII.GetString(receive, 0, ret)
                            Dim ret As Integer = s.Receive(receive, receive.Length, 0)
                            If ret > 0 Then
                                Dim data As String = System.Text.Encoding.ASCII.GetString(receive, 0, ret)

                                receivedData = receivedData & data 'System.Text.Encoding.ASCII.GetString(bytes, 0, recv)
                                ' receivedData = data
                                If receivedData <> "" Then
                                    Try

                                        Dim StateCompleteData As Boolean = False

                                        If receivedData.Contains(ChrW(3)) And receivedData.Contains(ChrW(1)) Then
                                            StateCompleteData = True
                                        ElseIf receivedData.Contains(ChrW(1)) And receivedData.Length > 20 Then

                                            Dim Chidx1 As Integer = -1
                                            Dim Chidx2 As Integer = -1
                                            Dim IdxChar As Integer = 0
                                            For Each Ch As Char In receivedData.ToCharArray
                                                IdxChar = IdxChar + 1

                                                If Ch = ChrW(1) Then
                                                    If Chidx1 = -1 Then
                                                        Chidx1 = IdxChar
                                                    ElseIf Chidx2 = -1 Then
                                                        Chidx2 = IdxChar
                                                    End If

                                                End If

                                                If Chidx2 > Chidx1 Then
                                                    data = Microsoft.VisualBasic.Mid(receivedData, Chidx1, Chidx2 - 1)
                                                    receivedData = data
                                                    StateCompleteData = True
                                                End If

                                            Next

                                        End If

                                        If (StateCompleteData) Then

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

                Dim CharIdx As Integer = [text].IndexOf(ChrW(3))
                data = Microsoft.VisualBasic.Left([text], CharIdx + 1)

                Dim Str(2) As String
                Str(0) = RCVTIME
                Str(1) = PortNumber.ToString + " Client IP " & [ip].ToString
                Str(2) = data

                GData.Rows.Add(Str)

                Try

                    If ACKData.ConnectToServer() Then
                        Dim DataArr As String() = data.Split(",")

                        Dim MType As String = DataArr(0).Split("=")(1)
                        Dim ClientIP As String = [ip].ToString.Split(":")(0)

                        With New ControlMessageData

                            Select Case Val(MType)
                                Case 1
                                    .MessageDataType01(data, DataArr, ClientIP)
                                Case 2
                                    .MessageDataType02(data, DataArr, ClientIP)
                                Case 3
                                    .MessageDataType03(data, DataArr, ClientIP)
                                Case 4
                                    .MessageDataType04(data, DataArr, ClientIP)
                                Case 5
                                    .MessageDataType05(data, DataArr, ClientIP)
                                Case 6
                                    .MessageDataType06(data, DataArr, ClientIP)
                                Case 7
                                    .MessageDataType07(data, DataArr, ClientIP)
                                Case 8
                                    .MessageDataType08(data, DataArr, ClientIP)
                                Case 9
                                    .MessageDataType09(data, DataArr, ClientIP)
                                Case 10
                                    .MessageDataType10(data, DataArr, ClientIP)
                            End Select

                        End With
                    End If



                Catch ex As Exception
                End Try


            End If

        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
        End Try




    End Sub


    Delegate Sub SetAddressTextCallback(ByVal [ip] As String)

    Private Sub SetAddressText(ByVal [ip] As String)
        Try
            ' Me.TReceive.Text &= [text] & vbCrLf

            ' InvokeRequired required compares the thread ID of the
            ' calling thread to the thread ID of the creating thread.
            ' If these threads are different, it returns true.

            If Me.InvokeRequired Then

                Dim d As New SetAddressTextCallback(AddressOf SetAddressText)
                Me.Invoke(d, New Object() {[ip]})

            Else

                GPortClose.Rows.Add([ip])

            End If

        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
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

    Private Sub BStopPort_Click(sender As Object, e As EventArgs) Handles BStopPort.Click
        Try
            trlisten.Abort()
        Catch ex As Exception
        End Try

        Try
            CloseTheThreadAll()
        Catch ex As Exception
        End Try

        TStart.Enabled = True
        BOpenPort.Enabled = True
        BStopPort.Enabled = False

    End Sub
End Class
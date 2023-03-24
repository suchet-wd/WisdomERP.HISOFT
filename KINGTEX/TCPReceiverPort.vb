Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Text
Public Class TCPReceiverPort
    Dim trlisten As Thread
    Public ContextMenu1 As New ContextMenu
    Dim DataAll As String
    Public PortNumber As Integer
    Public FormCaption As String
    Dim Icount As Integer = 0
    Dim trSave As Thread
    Dim sql As String
    'Dim c As New Cls_Query
    Dim CheckDataIns As Boolean = True
    ' Dim tcpListener As TcpListener
    Public MemoData As DevExpress.XtraEditors.MemoEdit

    'Dim StartOfPacket As Char = Chr(CInt("0x02"))
    'Dim EndOfPacket As Char = Chr(CInt("0x03"))

    Public Sub CreateIconMenuStructure()
        '  NotifyIcon1.ContextMenu = ContextMenu1
    End Sub


    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            trlisten.Abort()
        Catch ex As Exception
        End Try

        'Try
        '    tcpListener.Stop()

        'Catch ex As Exception

        'End Try
        'Me.NotifyIcon1.Visible = False
        'Me.NotifyIcon1.Dispose()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US", True)
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
        '  Me.NotifyIcon1.Icon = Me.Icon
        ' Me.NotifyIcon1.Text = Me.PortNumber
        Me.Text = FormCaption
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

                Dim bytes(tcpCli.ReceiveBufferSize) As Byte
                ns.Read(bytes, 0, CInt(tcpCli.ReceiveBufferSize))
                ' Return the data received from the client to the console.
                Dim receivedData As String = Encoding.UTF7.GetString(bytes)

                ' receivedData = "$$B0356895032776810|AA$GPRMC,081805.000,A,1348.1347,N,10030.7724,E,0.00,,260210,,*15|02.4|01.5|01.8|000000000000|20100226081805|14291317|00000000|3AA20311|0000|0.0000|1992|D4F8"
                '  Dim sr As New StreamReader(ns)


                ''''''''' get data from client '''''''''''''''
                ' Dim receivedData As String = sr.ReadLine
1:

                '  If sr.Peek <> -1 Then
                'receivedData &= sr.ReadLine
                '  GoTo 1
                ' End If

                'Dim returnedData As String = "*8888*X#" '& " From Server"
                'Dim sw As New StreamWriter(ns)
                'sw.WriteLine(returnedData)
                'sw.Flush()

                Icount = 0
                CheckDataIns = True
                SetText(receivedData)

                If CheckDataIns = True Then
                    Ime = SplitData(receivedData, Flag)
                End If

                'If Flag = "CAT5"  Then
                Dim returnedData As String = ""
                'sql = "SELECT     Command  FROM         Control_Car  WHERE     (Gps_IMEI = '" & c.rpQuoted(Ime) & "') "
                'Dim DT As New DataTable
                'DT = c.GetDataTable(sql, Cls_Query.DataBaseName.GPS)

                'If DT.Rows.Count > 0 Then
                '    returnedData = DT.Rows(0)(0).ToString
                '    'sql = " DELETE FROM Control_Car  WHERE     (Gps_IMEI = '" & c.rpQuoted(Ime) & "') "
                '    'c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)
                'Else
                '    'If Ime = "353358018973557" Or Ime = "353358018962428" Or Ime = "356895030715877" Or Ime = "356895030639697" Then
                '    '    returnedData = "*8888*X#"
                '    'End If
                'End If

                If returnedData <> "" Then
                    Dim sw As New StreamWriter(ns)
                    sw.WriteLine(returnedData)
                    sw.Flush()
                End If

                ns.Close()
                tcpCli.Close()

            Loop
            tcpList.Stop()
        Catch ex As Exception
            'error
            LISTENING = False
        End Try
    End Sub

    Sub ListemDataFromClient()

        Dim port As Integer = PortNumber
        Try

            Dim tcpList As New TcpListener(port)
            'tcpList = New TcpListener(System.Net.IPAddress.Any, port)
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
                        received = received & System.Text.Encoding.ASCII.GetString(bytes, 0, recv)

                        If received <> "" Then
                            Try
                                Dim LastChar As String = Microsoft.VisualBasic.Right(received, 1)
                                Dim DataByte As Byte = Encoding.ASCII.GetBytes(LastChar)(0)
                                Dim CheckDataByte As Byte = Encoding.ASCII.GetBytes(Chr(3))(0)

                                If Encoding.ASCII.GetBytes(LastChar)(0) = Encoding.ASCII.GetBytes(Chr(3))(0) Then

                                    Dim respondData As Byte() = Encoding.ASCII.GetBytes(ACKData.GetACKData)
                                    Stream.Write(respondData, 0, respondData.Length)

                                    SetDataText(received)

                                    Exit While

                                End If

                            Catch ex As Exception
                                Exit While
                            End Try

                        End If

                        'If received.EndsWith(vbLf & vbLf) Or received.EndsWith(Chr(3)) Then
                        '    'Byte[] respondData = Encoding.ASCII.GetBytes("respond");
                        '    'Array.Resize(ref respondData, 16); // Resizing To 16 Byte, because In this example all messages have 16 Byte To make it easier To understand.
                        '    'BinaryWriter.Write(respondData, 0, 16);
                        '    Dim respondData As Byte() = Encoding.ASCII.GetBytes(ACKData.GetACKData)
                        '    Stream.Write(respondData, 0, respondData.Length)
                        '    SetDataText(received)
                        '    Exit While
                        'End If

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
                MemoData.Text = MemoData.Text & data
                'TReceive.Text = TReceive.Text & data
            Catch ex As Exception
            End Try

        End If

    End Sub

    Function AnotherConversion(ByVal s As String) As String

        Dim i As Integer
        Dim sb As New System.Text.StringBuilder(s.Length \ 2)

        For i = 0 To s.Length - 2 Step 2
            sb.Append(Chr(Convert.ToByte(s.Substring(i, 2), 16)))
        Next

        Return sb.ToString

    End Function

    Function SplitData(ByVal data As String, ByRef Flag As String) As String
        'Dim Flag As String
        Dim CarMode, Engine, Door, LAC, CID, IMEI, SIM, LN, LE, SP, GMC, Car_license, CompanyID, RCVDATE, RCVTIME, Fuel As String
        Fuel = ""
        Try
            If data.Substring(0, 1) = "$" Then

                If Split(data, ",").Length >= 12 Then
                    Flag = "AVL05"
                    Dim Li() As String = StringToHex(data).Split(",")
                    IMEI = Li(0)
                    'หาพิกัดที่ถูกต้อง
                    Dim sLat = Li(1)
                    Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                    dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                    LN = dLat
                    Dim sLon = Li(2)
                    Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                    dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                    LE = dLon
                    '/////
                    SP = Li(3)
                    CarMode = Li(4)
                    Fuel = Li(5)
                Else
                    Flag = "VT300"
                    Dim Li() As String = StringToHex(data).Split(",")
                    IMEI = Li(0)
                    'หาพิกัดที่ถูกต้อง
                    Dim sLat = Li(1)
                    Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                    dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                    LN = dLat
                    Dim sLon = Li(2)
                    Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                    dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                    LE = dLon
                    '/////
                    SP = Li(3)
                    CarMode = Li(4)
                End If
            ElseIf data.Substring(0, 1) = "I" Then
                Flag = "CAT5"
                Dim Li() As String = Cat5(data).Split(",")
                IMEI = Li(0)

                'หาพิกัดที่ถูกต้อง
                If Li(1) <> "" Then
                    Dim sLat = Li(1)

                    Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                    dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                    LN = dLat
                    Dim sLon = Li(2)
                    Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                    dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                    LE = dLon
                    '/////
                    SP = Li(3)
                End If
                If SP = "" Then SP = "0.00"
                If SP < 1 Then SP = "0.00"
                Door = Li(4).Split(":")(1)
                Engine = Li(5).Split(":")(1)
                CarMode = Li(7)

                If CarMode = "Real-time Report" Then
                    Flag = "CAT508"
                End If

            Else

                Flag = "CAT4"
                Dim Sysfld() As String
                Sysfld = Split(data, "#")
                Dim ListP() As String
                ListP = Split(Sysfld(2), "$")
                Dim ListData() As String
                ListData = Split(ListP(0), ";")
                If ListP.Length > 1 Then
                    GMC = ListP(1)
                Else
                    GMC = ""
                End If
                CarMode = ListData(0)
                Engine = ListData(1).Split(":")(1)
                Door = ListData(2).Split(":")(1)
                LAC = ListData(3).Split(":")(1)
                CID = ListData(4).Split(":")(1)

                IMEI = Sysfld(3).Split(";")(0)

                Try
                    SIM = Sysfld(3).Split(";")(1)
                Catch ex As Exception
                    SIM = ""
                End Try
                'หาพิกัดที่ถูกต้อง
                If GMC.Length > 10 Then
                    Dim sLat = ListP(1).Split(",")(3)
                    Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                    dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                    LN = dLat
                    Dim sLon = ListP(1).Split(",")(5)
                    Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                    dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                    LE = dLon
                    '/////
                    SP = ListP(1).Split(",")(7)
                End If

                'กล่องใหม่
            End If
            '  GMC = data
            RCVDATE = DateTime.Now.ToString("dd/MM/yyyy")
            RCVTIME = DateTime.Now.ToString("HH:mm:ss")

            'sql = "SELECT     Car_license_plate, CompanyID  FROM         Car   WHERE     (Gps_IMEI = '" & c.rpQuoted(IMEI) & "') "
            'Dim Dt As New DataTable
            'Dt = c.GetDataTable(sql, Cls_Query.DataBaseName.GPS)

            'If Dt.Rows.Count > 0 Then
            '    Car_license = Dt.Rows(0)("Car_license_plate").ToString
            '    CompanyID = Dt.Rows(0)("CompanyID").ToString
            'Else
            '    Car_license = ""
            '    CompanyID = ""
            'End If


        Catch ex As Exception
            'IMEI = ""
        End Try

        'If CarMode = "Real-time Report" Then
        '    If LN <> "" Then
        '        LE = Math.Round(CDbl(LE), 4)
        '        LN = Math.Round(CDbl(LN), 4)
        '        If Car_license <> "" Then
        '            sql = "  UPDATE    Gps_Data_Receive_MAX "
        '            sql &= " SET              N='" & LN & "', E='" & LE & "', Time = '" & c.rpQuoted(RCVTIME) & "', Date = '" & c.rpQuoted(RCVDATE) & "', Door = '" & c.rpQuoted(Door) & "', Engine = '" & c.rpQuoted(Engine) & "', Fuel = '" & c.rpQuoted(Fuel) & "',GPS_Status='" & c.rpQuoted("มีสัญญาณดาวเทียม") & "',Speed='" & SP & "' "
        '            sql &= " WHERE   (Gps_IMEI = '" & c.rpQuoted(IMEI) & "') "
        '            c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)
        '        End If
        '        GoTo EndReal
        '    End If
        'End If
        If IMEI = "" Then Exit Function

        If Car_license <> "" Then
            If LN = "" Or LE = "" Then
                If CarMode <> "" Then
                    'sql = "  UPDATE    Gps_Data_Receive_MAX "
                    'sql &= " SET              Time = '" & c.rpQuoted(RCVTIME) & "', Date = '" & c.rpQuoted(RCVDATE) & "', Door = '" & c.rpQuoted(Door) & "', CarMode = '" & c.rpQuoted(CarMode) & "', Engine = '" & c.rpQuoted(Engine) & "', Fuel = '" & c.rpQuoted(Fuel) & "',GPS_Status='" & c.rpQuoted("ไม่พบสัญญาณดาวเทียม") & "',Speed='0' "
                    'sql &= " WHERE   (Gps_IMEI = '" & c.rpQuoted(IMEI) & "') "
                    'c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)

                    'Try

                    '    sql = "  INSERT INTO Gps_Data_Receive "
                    '    sql &= "       (Gps_IMEI, Sim, N, E, Speed, Date, Time, LAC, CID, GMC, Door, CarMode, Engine, Fuel, IDRunning, Car_license_plate, CompanyID)"
                    '    sql &= " SELECT Gps_IMEI, Sim, N, E, Speed, Date, Time, LAC, CID, GMC, Door, CarMode, Engine, Fuel, IDRunning, Car_license_plate, CompanyID "
                    '    sql &= " FROM Gps_Data_Receive_MAX  WHERE (Gps_IMEI = '" & c.rpQuoted(IMEI) & "') AND Time = '" & c.rpQuoted(RCVTIME) & "' AND  Date = '" & c.rpQuoted(RCVDATE) & "'"
                    '    c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)

                    'Catch ex As Exception
                    'End Try
                End If
                ' clear()
                Exit Function
            End If

            LE = Math.Round(CDbl(LE), 4)
            LN = Math.Round(CDbl(LN), 4)

            If IMEI = "" Then Exit Function

            'sql = "  INSERT INTO Gps_Data_Receive "
            'sql &= "       (Gps_IMEI, Sim, N, E, Speed, Date, Time, LAC, CID, GMC, Door, CarMode, Engine, Fuel, IDRunning, Car_license_plate, CompanyID)"
            'sql &= " VALUES     ('" & c.rpQuoted(IMEI) & "', '" & c.rpQuoted(SIM) & "', '" & c.rpQuoted(LN) & "', '" & c.rpQuoted(LE) & "', '" & c.rpQuoted(SP) & "', '" & c.rpQuoted(RCVDATE) & "', '" & c.rpQuoted(RCVTIME) & "', '" & c.rpQuoted(LAC) & "', '" & c.rpQuoted(CID) & "', '" & c.rpQuoted(GMC) & "', '" & c.rpQuoted(Door) & "', '" & c.rpQuoted(CarMode) & "', '" & c.rpQuoted(Engine) & "', '" & c.rpQuoted(Fuel) & "','" & CStr(DateTime.Now.ToString("yyMMddHHmmss")) & "', '" & c.rpQuoted(Car_license) & "', '" & c.rpQuoted(CompanyID) & "') "
            'c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)
            'sql = " DELETE FROM Gps_Data_Receive_MAX   WHERE     (Gps_IMEI = '" & c.rpQuoted(IMEI) & "') "
            'c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)

            'Try
            '    sql = "  INSERT INTO  Gps_Data_Receive_MAX "
            '    sql &= "       (Gps_IMEI, Sim, N, E, Speed, Date, Time, LAC, CID, GMC, Door, CarMode, Engine, Fuel, IDRunning, Car_license_plate, CompanyID,GPS_Status)"
            '    sql &= " VALUES     ('" & c.rpQuoted(IMEI) & "', '" & c.rpQuoted(SIM) & "', '" & c.rpQuoted(LN) & "', '" & c.rpQuoted(LE) & "', '" & c.rpQuoted(SP) & "', '" & c.rpQuoted(RCVDATE) & "', '" & c.rpQuoted(RCVTIME) & "', '" & c.rpQuoted(LAC) & "', '" & c.rpQuoted(CID) & "', '" & c.rpQuoted(GMC) & "', '" & c.rpQuoted(Door) & "', '" & c.rpQuoted(CarMode) & "', '" & c.rpQuoted(Engine) & "', '" & c.rpQuoted(Fuel) & "','" & CStr(DateTime.Now.ToString("yyMMddHHmmss")) & "', '" & c.rpQuoted(Car_license) & "', '" & c.rpQuoted(CompanyID) & "','มีสัญญานดาวเทียม') "
            '    c.ExecuteNonqueryNontry(sql, Cls_Query.DataBaseName.GPS)
            'Catch ex As Exception
            '    sql = " DELETE FROM Gps_Data_Receive_MAX   WHERE     (Gps_IMEI = '" & c.rpQuoted(IMEI) & "') "
            '    c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)
            '    sql = "  INSERT INTO  Gps_Data_Receive_MAX "
            '    sql &= "       (Gps_IMEI, Sim, N, E, Speed, Date, Time, LAC, CID, GMC, Door, CarMode, Engine, Fuel, IDRunning, Car_license_plate, CompanyID,GPS_Status)"
            '    sql &= " VALUES     ('" & c.rpQuoted(IMEI) & "', '" & c.rpQuoted(SIM) & "', '" & c.rpQuoted(LN) & "', '" & c.rpQuoted(LE) & "', '" & c.rpQuoted(SP) & "', '" & c.rpQuoted(RCVDATE) & "', '" & c.rpQuoted(RCVTIME) & "', '" & c.rpQuoted(LAC) & "', '" & c.rpQuoted(CID) & "', '" & c.rpQuoted(GMC) & "', '" & c.rpQuoted(Door) & "', '" & c.rpQuoted(CarMode) & "', '" & c.rpQuoted(Engine) & "', '" & c.rpQuoted(Fuel) & "','" & CStr(DateTime.Now.ToString("yyMMddHHmmss")) & "', '" & c.rpQuoted(Car_license) & "', '" & c.rpQuoted(CompanyID) & "','มีสัญญานดาวเทียม') "
            '    c.ExecuteNonqueryNontry(sql, Cls_Query.DataBaseName.GPS)
            'End Try

            'If IMEI <> "" Then
            '    Try
            '        sql = "INSERT INTO IMEI_Port (Car_IMEI, Port)  VALUES     ('" & c.rpQuoted(IMEI) & "', '" & c.rpQuoted(PortNumber) & "') "
            '        c.ExecuteNonqueryNontry(sql, Cls_Query.DataBaseName.GPS)
            '    Catch ex As Exception
            '        sql = " UPDATE    IMEI_Port  SET              Port = '" & c.rpQuoted(PortNumber) & "'  WHERE     (Car_IMEI = '" & c.rpQuoted(IMEI) & "') "
            '        c.ExecuteNonquery(sql, Cls_Query.DataBaseName.GPS)
            '    End Try
            'End If

        End If

        ' SaveData()
EndReal:

        'trSave = New Thread(AddressOf SaveData)
        'trSave.Start()
        Return IMEI
    End Function

    Function StringToHex(ByVal text As String) As String
        Dim hex As String
        Dim x As Integer
        Dim IMEI, N, E, Speed, sos, Fuel As String
        Fuel = ""
        IMEI = ""
        N = ""
        E = ""
        Speed = ""
        sos = ""
        Try
            'For i As Integer = 0 To text.Length - 1
            '    x = i
            '    If i = 11 Then Exit For
            '    If i > 3 Then 'And i < 11 Then
            '        If Asc(text.Substring(i, 1)).ToString("x").ToUpper.Length = 1 Then
            '            hex &= "0"
            '        End If
            '        hex &= Asc(text.Substring(i, 1)).ToString("x").ToUpper
            '        ' Else
            '        'hex &= (text.Substring(i, 1))
            '    End If
            '    'IMEI = hex
            'Next


            For i As Integer = 0 To text.Length - 1
                x = i
                If i = 14 Then Exit For
                If i > 3 And i < 11 Then
                    If Asc(text.Substring(i, 1)).ToString("x").ToUpper.Length = 1 Then
                        hex &= "0"
                    End If
                    hex &= Asc(text.Substring(i, 1)).ToString("x").ToUpper
                ElseIf i > 10 Then
                    If Asc(text.Substring(i, 1)).ToString("x").ToUpper.Length = 1 Then
                        sos &= "0"
                    End If
                    sos &= Asc(text.Substring(i, 1)).ToString("x").ToUpper
                End If

                'IMEI = hex
            Next


            For Each a As String In hex
                If IsNumeric(a) = False Then
                    If IMEI.Length Mod 2 = 1 Then
                        IMEI = IMEI.Remove(IMEI.Length - 1, 1)
                    End If
                    Exit For
                End If
                IMEI &= a
            Next
            '////////////////
            For i As Integer = x To text.Length - 1
                hex &= (CStr(text).Substring(i, 1))
                '    My.Application.DoEvents()
            Next
            Dim str() As String = hex.Split(",")
            x = 0
            For Each St As String In str
                If St = "N" Then
                    N = str(x - 1)
                End If
                If St = "E" Then
                    E = str(x - 1)
                    Speed = str(x + 1)
                    Exit For
                End If
                x += 1
            Next
            If Speed = "" Then Speed = "0.00"
            If sos = "999901" Then sos = "SOS(Hijack Alert)"

            Dim StrFuel() As String = Split(text, ",")
            Dim StrF As String = ""
            If StrFuel.Length >= 12 Then
                StrF = StrFuel(11)
                StrFuel = Split(StrF, "|")
                If StrFuel.Length >= 7 Then
                    StrF = StrFuel(6)
                    Fuel = Mid(StrF, 5, 2) & "." & Mid(StrF, 7, 2)

                    If Val(Fuel) <> 0 Then
                        Fuel = Format((Val(Fuel) * 100) / 13.14, "0.00")
                    End If
                End If
            End If

        Catch ex As Exception
        End Try

        Return IMEI & "," & N & "," & E & "," & Speed & "," & sos & "," & Fuel

    End Function

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

            Dim data, CarMode, Engine, Door, LAC, CID, IMEI, SIM, LN, LE, SP, GMC, Car_license, CompanyID, RCVDATE, RCVTIME, Fuel As String
            RCVDATE = DateTime.Now.ToString("dd/MM/yyyy")
            RCVTIME = DateTime.Now.ToString("HH:mm:ss")
            data = [text]

            Try
                If data.Substring(0, 1) = "$" Then
                    If Split(data, ",").Length >= 12 Then
                        Dim Li() As String = StringToHex(data).Split(",")
                        IMEI = Li(0)
                        'หาพิกัดที่ถูกต้อง
                        Dim sLat = Li(1)
                        Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                        dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                        LN = dLat
                        Dim sLon = Li(2)
                        Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                        dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                        LE = dLon
                        '/////
                        SP = Li(3)
                        CarMode = Li(4)
                        Fuel = Li(5)
                    Else
                        Dim Li() As String = StringToHex(data).Split(",")
                        IMEI = Li(0)
                        'หาพิกัดที่ถูกต้อง
                        Dim sLat = Li(1)
                        Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                        dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                        LN = dLat
                        Dim sLon = Li(2)
                        Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                        dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                        LE = dLon
                        '/////
                        SP = Li(3)
                        CarMode = Li(4)
                    End If


                ElseIf data.Substring(0, 1) = "I" Then
                    Dim Li() As String = Cat5(data).Split(",")
                    IMEI = Li(0)
                    'หาพิกัดที่ถูกต้อง
                    If Li(1) <> "" Then


                        Dim sLat = Li(1)
                        Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                        dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                        LN = dLat
                        Dim sLon = Li(2)
                        Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                        dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                        LE = dLon
                        '/////
                        SP = Li(3)
                    End If

                    If SP = "" Then SP = "0.00"
                    If SP < 1 Then SP = "0.00"

                    Door = Li(4).Split(":")(1)
                    Engine = Li(5).Split(":")(1)

                    If Li.Length > 4 Then
                        CarMode = Li(7)
                    End If


                Else

                    Dim Sysfld() As String
                    Sysfld = Split(data, "#")
                    Dim ListP() As String
                    ListP = Split(Sysfld(2), "$")
                    Dim ListData() As String
                    ListData = Split(ListP(0), ";")
                    If ListP.Length > 1 Then
                        GMC = ListP(1)
                    Else
                        GMC = ""
                    End If

                    CarMode = ListData(0)
                    Engine = ListData(1).Split(":")(1)
                    Door = ListData(2).Split(":")(1)
                    LAC = ListData(3).Split(":")(1)
                    CID = ListData(4).Split(":")(1)
                    IMEI = Sysfld(3).Split(";")(0)

                    Try
                        SIM = Sysfld(3).Split(";")(1)
                    Catch ex As Exception
                        SIM = ""
                    End Try

                    If GMC.Length > 10 Then
                        'หาพิกัดที่ถูกต้อง
                        Dim sLat = ListP(1).Split(",")(3)
                        Dim dLat = CSng(Microsoft.VisualBasic.Left(sLat, 2))
                        dLat = dLat + CSng(Microsoft.VisualBasic.Right(sLat, Len(sLat) - 2)) / 60
                        LN = dLat
                        Dim sLon = ListP(1).Split(",")(5)
                        Dim dLon = CSng(Microsoft.VisualBasic.Left(sLon, 3))
                        dLon = dLon + CSng(Microsoft.VisualBasic.Right(sLon, Len(sLon) - 3)) / 60
                        LE = dLon
                        '/////
                        SP = ListP(1).Split(",")(7)
                    End If

                    'เช็คว่าเป็นกล่องใหม่ป่าว
                End If


                'sql = "SELECT     Car_license_plate, CompanyID  FROM         Car   WHERE     (Gps_IMEI = '" & c.rpQuoted(IMEI) & "') "
                'Dim Dt As New DataTable
                'Dt = c.GetDataTable(sql, Cls_Query.DataBaseName.GPS)

                'If Dt.Rows.Count > 0 Then
                '    Car_license = Dt.Rows(0)("Car_license_plate").ToString
                '    CompanyID = Dt.Rows(0)("CompanyID").ToString
                'Else
                '    Car_license = ""
                '    CompanyID = ""
                'End If

                If Trim(LE) <> "" Then
                    LE = Math.Round(CDbl(LE), 4)
                    LN = Math.Round(CDbl(LN), 4)
                End If

                Dim Str(7) As String
                Str(0) = RCVTIME
                Str(1) = PortNumber
                Str(2) = data
                Str(3) = Car_license
                Str(4) = IMEI
                Str(5) = SP & " Enginge:" & Engine & " Door:" & Door
                Str(6) = CarMode
                Str(7) = LN & "," & LE

                ' AllportOpen.GData.Rows.Add(Str)

                Me.LMode.Text = "Car Mode :" & CarMode
                Me.LEngine.Text = "Engint : " & Engine
                Me.LDoor.Text = "Door : " & Door
                Me.LLAC.Text = "LAC : " & LAC
                Me.LCID.Text = "CID :" & CID
                Me.LIMEI.Text = "IMEI : " & IMEI
                Me.LPosition.Text = " Position : (" & LN & "," & LE & ")"
                Me.LSIM.Text = "SIM : " & SIM
                Me.LSpeed.Text = " Speed : " & SP
                Me.LTime.Text = DateTime.Now.ToString("HH:mm:ss")
                Me.LDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
                Me.TReceive.Text = DataAll

            Catch ex As Exception

                '  NotifyIcon1.ShowBalloonTip(10, "", "ข้อมูลที่ส่งมาไม่สมบูรณ์", ToolTipIcon.Info)

                Dim Str(7) As String
                Str(0) = RCVTIME
                Str(1) = PortNumber
                Str(2) = data
                Str(3) = Car_license
                Str(4) = IMEI
                Str(5) = SP
                Str(6) = CarMode
                Str(7) = LN & "," & LE
                'AllportOpen.GData.Rows.Add(Str)

                CheckDataIns = False

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

        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            '   Me.NotifyIcon1.Visible = True
        Else
            ' จำค่าปัจจุบันก่อนจะ Minimize ไว้
            currentWindowState = Me.WindowState
        End If

    End Sub

    'Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
    '    If Me.WindowState = FormWindowState.Minimized Then
    '        Me.NotifyIcon1.Visible = False
    '        Me.Show()
    '        Me.WindowState = Me.currentWindowState
    '    End If

    'End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'My.Application.DoEvents()
        'Icount += 1
        'Me.Text = FormCaption & " ( " & Icount.ToString & ")"
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

    Function Cat5(ByVal text As String) As String
        Dim IMEI, N, E, Speed, door, engine, Power, GMC, CarMode As String
        Dim x As Integer

        Try
            GMC = text.Split("$")(1)
        Catch ex As Exception
        End Try

        Dim str() As String = text.Split(",")

        x = 0

        Select Case str(1)
            Case "00"
                CarMode = "UnArmed"
            Case "01"
                CarMode = "Armed"
            Case "02"
                CarMode = "Door-open alert"
            Case "03"
                CarMode = "Engine-on alert"
            Case "04"
                CarMode = "SOS(Hijack Alert)"
            Case "05"
                CarMode = "Power failure alert"
            Case "06"
                CarMode = "GEO alarm"
            Case "07"
                CarMode = "Move alert"
            Case "08"
                CarMode = "Real-time Report"
            Case "10"
                CarMode = "Over Speed Alert"
            Case "11"
                CarMode = "Route-points Report"
            Case "12"
                CarMode = "Fixed Distance Report"
            Case "09"
                CarMode = "Geographical"
        End Select

        For Each St As String In str
            If St.Contains("IMEI") = True Then
                IMEI = Trim(St.Replace("IMEI:", "").ToString.Replace("#", ""))
            End If
            If St.Contains("LAC") = True Then

                If str(x - 1).Substring(0, 1) = 1 Then
                    door = "Door:CLOSE"
                Else
                    door = "Door:OPEN"
                End If

                If str(x - 1).Substring(1, 1) = 1 Then
                    engine = "ENGINE:on"
                Else
                    engine = "ENGINE:off"
                End If
                If str(1) = "08" Then
                    'If IMEI = "353358018973557" Or IMEI = "353358018962428" Or IMEI = "356895030715877" Or IMEI = "356895030639697" Then
                    '    If str(x - 1).Substring(2, 1) = 1 Then
                    '        'Power = "POWER:ON"
                    '        ' CarMode = "Armed"
                    '    Else
                    '        'Power = "POWER:OFF"
                    '        '  CarMode = "Unarmed"
                    '        CarMode = "Power failure alert"
                    '    End If

                    'Else
                    If str(x - 1).Substring(2, 1) = 1 Then
                        'Power = "POWER:ON"
                        CarMode = "Armed"
                    Else
                        'Power = "POWER:OFF"
                        CarMode = "UnArmed"
                    End If

                    ' End If

                End If


            End If

            If St = "N" Then
                N = str(x - 1)
            End If
            If St = "E" Then
                E = str(x - 1)
                Speed = str(x + 1)
                Exit For
            End If
            x += 1
        Next
        If Speed = "" Then Speed = "0.00"
        If CDbl(Speed) < 3 Then Speed = "0.00"
        If engine = "ENGINE:off" Then Speed = "0.00"

        Return IMEI & "," & N & "," & E & "," & Speed & "," & door & "," & engine & "," & Power & "," & CarMode & "," & GMC
    End Function

End Class

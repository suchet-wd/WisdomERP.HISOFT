Imports System
Imports System.IO
Imports System.IO.Ports
Imports System.Text
Imports System.Threading

Public Class ComportReceiver

    'Private sensor As SerialPort

    Private _SerialPort As New List(Of SerialPort)()
    'Dim trdata As Thread

    Private Sub AutomationReceiver_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try

            For I As Integer = 0 To _SerialPort.Count - 1

                Dim sensor As SerialPort = _SerialPort.Item(I)

                If Not (sensor Is Nothing) Then
                    RemoveHandler sensor.DataReceived, AddressOf DataReceivedHandler
                    sensor.Close()
                    sensor = Nothing
                End If

            Next

            _SerialPort.Clear()
            ocmstart.Enabled = True
            ocmstop.Enabled = False
            ogcdataport.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private _ComName As String = ""
    Private Property ComName As String
        Get
            Return _ComName
        End Get
        Set(value As String)
            _ComName = value
        End Set
    End Property

    Private _LineNo As String = ""
    Private Property LineNo As String
        Get
            Return _LineNo
        End Get
        Set(value As String)
            _LineNo = value
        End Set
    End Property

    Private _DFPortName As String = ""
    Private Property DFPortName As String
        Get
            Return _DFPortName
        End Get
        Set(value As String)
            _DFPortName = value
        End Set
    End Property

    Private Sub GetSerialPortNames()

        Dim _dtPort As New DataTable
        _dtPort.Columns.Add("FTSelect", GetType(String))
        _dtPort.Columns.Add("FTComport", GetType(String))

        Dim _AllPort As String = ""

        For Each sp As String In My.Computer.Ports.SerialPortNames
            _dtPort.Rows.Add("0", sp)
        Next

        Me.ogcdataport.DataSource = _dtPort.Copy

    End Sub

    Private Sub AutomationReceiver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _SerialPort.Clear()

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        DevExpress.Skins.SkinManager.EnableFormSkins()
        Application.EnableVisualStyles()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin")

        Try
            'Dim _Theme As String = HI.UL.AppRegistry.ReadRegistry(HI.UL.AppRegistry.KeyName.Theme)

            'If _Theme <> "" Then
            '    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(_Theme)
            'End If

        Catch ex As Exception
        End Try

        '----------- Read Connecttion String From File XML
        ' HI.Conn.DB.GetXmlConnectionString()
        '----------- Read Connecttion String From File XML

        Call GetSerialPortNames()
        'Call ReadXmlDefualtPort()

        Dim _StateRun As Boolean = False
        If DFPortName <> "" Then

            With CType(Me.ogcdataport.DataSource, DataTable)
                .AcceptChanges()

                'For Each Str As String In DFPortName.Split("|")

                '    For Each R As DataRow In .Select("FTComport='" & HI.UL.ULF.rpQuoted(Str) & "'")
                '        R!FTSelect = "1"
                '        _StateRun = True
                '    Next

                'Next

                .AcceptChanges()

            End With

        End If

        Timer1.Enabled = True
        ocmstart.Enabled = True
        ocmstop.Enabled = False
        ogcdataport.Enabled = True

        If (_StateRun) Then
            Call ocmstart_Click(ocmstart, New System.EventArgs)
        End If

    End Sub

    Private Delegate Sub DataReceivedHandler_Delegate(sender As Object, e As SerialDataReceivedEventArgs)
    Dim data As String = ""
    Private Sub DataReceivedHandler(sender As Object, e As SerialDataReceivedEventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New DataReceivedHandler_Delegate(AddressOf DataReceivedHandler), New Object() {sender, e})
        Else

            Try

                Dim sp As SerialPort = CType(sender, SerialPort)
                Dim indata As String = sp.ReadExisting
                Dim countdata As Integer = 0
                Dim _cmd As String = ""

                data = data & indata

                omdata.Text = omdata.Text & indata
                Dim LastChar As String = Microsoft.VisualBasic.Right(data, 1)
                Dim DataByte As Byte = Encoding.ASCII.GetBytes(LastChar)(0)
                Dim CheckDataByte As Byte = Encoding.ASCII.GetBytes(Chr(3))(0)

                If data.Contains(ChrW(3)) Then



                    Dim returnedData As String = "�{F2INF=10,�"
                    Dim returnbyte As Byte() = Encoding.ASCII.GetBytes(returnedData)


                    If returnedData <> "" Then

                        Dim filebufferSend As Byte()
                        Dim fileStreamSend As Stream
                        fileStreamSend = File.OpenRead(Application.StartupPath + "\Acknowledge_to_control_box.txt")
                        ' Alocate memory space for the file
                        ReDim filebufferSend(fileStreamSend.Length)
                        fileStreamSend.Read(filebufferSend, 0, fileStreamSend.Length)

                        sp.Write(filebufferSend, 0, fileStreamSend.Length)
                        SetText(data)

                        data = ""
                    End If
                End If

                'Dim K As Byte() = Encoding.ASCII.GetBytes(data)

                'For Each DataByte In Encoding.ASCII.GetBytes(data)

                '    CheckDataByte = Encoding.ASCII.GetBytes(Chr(3))(0)

                '    If DataByte = CheckDataByte Then
                '        Beep()
                '    End If
                'Next
                'If Encoding.ASCII.GetBytes(LastChar)(0) = Encoding.ASCII.GetBytes(Chr(3))(0) Then

                '    Dim returnedData As String = "�{F2INF=10,�"

                '    If returnedData <> "" Then
                '        sp.WriteLine(returnedData)
                '    End If

                'End If


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
            Dim ControlData As String = [text]
            Dim DataArr As String() = ControlData.Split(",")

            'Try
            '    Dim MType As String = DataArr(0).Split("=")(1)
            '    With New ControlMessageData

            '        Select Case Val(MType)
            '            Case 1
            '                .MessageDataType01(ControlData, DataArr)
            '            Case 2
            '                .MessageDataType02(ControlData, DataArr)
            '            Case 3
            '                .MessageDataType03(ControlData, DataArr)
            '            Case 4
            '                .MessageDataType04(ControlData, DataArr)
            '            Case 5
            '                .MessageDataType05(ControlData, DataArr)
            '            Case 6
            '                .MessageDataType06(ControlData, DataArr)
            '            Case 7
            '                .MessageDataType07(ControlData, DataArr)
            '            Case 8
            '                .MessageDataType08(ControlData, DataArr)
            '            Case 9
            '                .MessageDataType09(ControlData, DataArr)
            '            Case 10
            '                .MessageDataType10(ControlData, DataArr)
            '        End Select
            '    End With
            'Catch ex As Exception
            'End Try

            'GData.Rows.Add(data)
        End If

    End Sub


    Private Sub ocmstart_Click(sender As Object, e As EventArgs) Handles ocmstart.Click
        Try
            'trdata.Abort()
        Catch ex As Exception
        End Try
        Me.GData.Rows.Clear()
        otb.SelectedTabPageIndex = 0
        _SerialPort.Clear()
        data = ""
        LineNo = ""
        omdata.Text = ""
        With CType(Me.ogcdataport.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FTSelect='1'").Length > 0 Then

                Try
                    For Each R As DataRow In .Select("FTSelect='1'")
                        Dim sensor As New SerialPort(R!FTComport.ToString)
                        '  sensor = New SerialPort(FNComport.Text)
                        If sensor.IsOpen Then
                            sensor.Close()
                        End If

                        With sensor
                            .BaudRate = 9600
                            .Parity = Parity.None
                            .StopBits = StopBits.One
                            .DataBits = 8
                            .Handshake = Handshake.None
                            .RtsEnable = True
                            '.Encoding = Encoding.ASCII
                            '.ReadBufferSize = 4096
                            '.NewLine = "\r\n"
                            '.ReceivedBytesThreshold = 100000
                            '.ReadTimeout = 500
                            AddHandler .DataReceived, AddressOf DataReceivedHandler
                            ' sensor.DataReceived += New SerialDataReceivedEventHandler(Sensor_DataReceived)
                        End With

                        sensor.Open()

                        _SerialPort.Add(sensor)

                    Next

                    ocmRefreshport.Enabled = False
                    ogcdataport.Enabled = False
                    ocmstart.Enabled = False
                    ocmstop.Enabled = True

                Catch ex As Exception
                    Call ocmstop_Click(ocmstop, New System.EventArgs)
                End Try

            End If


        End With

        'trdata = New Thread(AddressOf ChaeckDataInsert)
        'trdata.Start()


    End Sub

    Sub ChaeckDataInsert()
        While True
            ' Accept will block until someone connects
            Try
                While Me.GData.Rows.Count > 0

                    Dim ControlData As String = Me.GData.Rows(0).Cells(0).Value.ToString
                    Thread.Sleep(100)

                    Me.GData.Rows.RemoveAt(0)



                End While
            Catch ex As Exception

            End Try


        End While
    End Sub


    Delegate Sub InsertDataCallback(ByVal [text] As String)

    Private Sub InsertData(ByVal [text] As String)

        ' Me.TReceive.Text &= [text] & vbCrLf

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.

        If Me.InvokeRequired Then

            Dim d As New InsertDataCallback(AddressOf InsertData)
            Me.Invoke(d, New Object() {[text]})

        Else

            Dim StrData As String = [text]
            Dim strarr As String() = StrData.Split(",")

        End If


    End Sub

    Private Sub ocmstop_Click(sender As Object, e As EventArgs) Handles ocmstop.Click
        data = ""
        Try
            'trdata.Abort()
        Catch ex As Exception
        End Try
        Try

            For I As Integer = 0 To _SerialPort.Count - 1

                Dim sensor As SerialPort = _SerialPort.Item(I)

                If Not (sensor Is Nothing) Then

                    RemoveHandler sensor.DataReceived, AddressOf DataReceivedHandler
                    sensor.Close()
                    sensor = Nothing

                End If

            Next

            _SerialPort.Clear()

            ocmRefreshport.Enabled = True
            ocmstart.Enabled = True
            ocmstop.Enabled = False
            ogcdataport.Enabled = True

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmRefreshport_Click(sender As Object, e As EventArgs) Handles ocmRefreshport.Click

        Call GetSerialPortNames()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Timer1.Enabled = False

        If Me.LineNo <> "" Then

            'Dim _Cmd As String = ""
            'Dim _dt As DataTable

            '_Cmd = "SELECT FTLineNo, FTMachine, FTButton, FDDate, FTTime "
            '_Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMachineAndon AS X WITH(NOLOCK)"
            ''   _Cmd &= vbCrLf & " WHERE FTLineNo='" & HI.UL.ULF.rpQuoted(Me.LineNo) & "'"
            '_Cmd &= vbCrLf & " WHERE  FDDate=" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & " AND FTComName='" & HI.UL.ULF.rpQuoted(UserLogInComputer) & "'"
            '_Cmd &= vbCrLf & " ORDER BY FDDate ASC , FTTime DESC , FTRunNo DESC  "

            '_dt = HI.Conn.SQLConn.GetDataTable(_Cmd, HI.Conn.DB.DataBaseName.DB_PROD)

            'Me.ogc.DataSource = _dt

        End If

        Timer1.Enabled = True
    End Sub

    Private Function ReadXmlDefualtPort() As String
        Dim DS As New DataSet()
        Dim _DefaultPortName As String = ""

        Try
            DS.ReadXml(Application.StartupPath + "\Database.xml")
            _DefaultPortName = ""
            If (DS.Tables.IndexOf("DefualtPortName") > -1) Then
                _DefaultPortName = DS.Tables("DefualtPortName").Rows(0)("Name").ToString()
            End If

        Catch ex As Exception
        End Try

        Return _DefaultPortName

    End Function

    Private UserLogInComputer As String = ""
    Private UserLogInComputerIP As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        UserLogInComputer = System.Environment.MachineName
        UserLogInComputerIP = GetIP(UserLogInComputer)
        DFPortName = ReadXmlDefualtPort()

    End Sub

    Private Function GetIP(strHostName As String) As String
        Dim _GetIPv4Address As String = ""
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)
        For Each ipheal As System.Net.IPAddress In iphe.AddressList

            If (ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork) Then
                _GetIPv4Address = ipheal.ToString()
                Exit For
            End If

        Next

        Return _GetIPv4Address
    End Function

    Private Sub ocmack_Click(sender As Object, e As EventArgs) Handles ocmack.Click
        Try
            For I As Integer = 0 To _SerialPort.Count - 1

                Dim sensor As SerialPort = _SerialPort.Item(I)

                If Not (sensor Is Nothing) Then
                    '  Dim returnedData As String = "�{F2INF=10,�"
                    Dim returnbyte As Byte() = Encoding.ASCII.GetBytes(ACKData.GetACKData())
                    'sensor.Write(ACKData.GetACKData())
                    sensor.Write(returnbyte, 0, returnbyte.Length)
                End If

            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ComportReceiver_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            ' trdata.Abort()
        Catch ex As Exception
        End Try
    End Sub
End Class

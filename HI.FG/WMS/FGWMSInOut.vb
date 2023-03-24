Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.IO.Ports
Imports System.Text
Imports System.Windows.Forms

Public Class FGWMSInOut

    'Private sensor As SerialPort
    Private ListDataReceive As New List(Of DataSenser)()
    Private _SerialPort As New List(Of SerialPort)()
    Private ArrLenght As Integer = 3
    Private ArrWeight() As Decimal
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
        'HI.Conn.DB.GetXmlConnectionString()
        '----------- Read Connecttion String From File XML

        Call GetSerialPortNames()
        Call ReadXmlDefualtPort()

        Dim _StateRun As Boolean = False
        If DFPortName <> "" Then

            With CType(Me.ogcdataport.DataSource, DataTable)
                .AcceptChanges()

                For Each Str As String In DFPortName.Split("|")

                    For Each R As DataRow In .Select("FTComport='" & HI.UL.ULF.rpQuoted(Str) & "'")
                        R!FTSelect = "1"
                        _StateRun = True
                    Next

                Next

                .AcceptChanges()

            End With

        End If


        ocmstart.Enabled = True
        ocmstop.Enabled = False
        ogcdataport.Enabled = True

        If (_StateRun) Then
            Call ocmstart_Click(ocmstart, New System.EventArgs)
        End If



    End Sub

    Private Delegate Sub DataReceivedHandler_Delegate(sender As Object, e As SerialDataReceivedEventArgs)
    Dim indatakgread As String = ""
    Private Sub DataReceivedHandler(sender As Object, e As SerialDataReceivedEventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New DataReceivedHandler_Delegate(AddressOf DataReceivedHandler), New Object() {sender, e})
        Else
            Try
                Dim indata As String = ""
                Dim indatakg As String = ""
                With CType(sender, SerialPort)
                    indata = .ReadExisting()
                End With

                Dim oTrim() As Char = {vbCr, vbLf}
                indata = indata.TrimEnd(oTrim)

                If indata.Trim <> "" Then
                    ' SetText(indatakg)
                    SetText(indata)

                End If

            Catch ex As Exception
            End Try

        End If

    End Sub

    Delegate Sub SetTextCallback(ByVal [text] As String)
    Private Sub SetText(ByVal [text] As String)

        If Me.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text]})
        Else
            Dim ControlData As String = [text]
            Dim dataval3 As Double = 0.0
            Dim datavalweight As Double = 0.0

            ' MemoEdit1.Text = MemoEdit1.Text & vbCrLf & ControlData
            TextEdit1.Text = ControlData

            MemoEdit1.Text = MemoEdit1.Text & ControlData

            Dim objectid As String = "SenserDataNumber1"

            For I As Integer = 0 To ListDataReceive.Count - 1
                If ListDataReceive(I).ID = objectid Then

                    If ListDataReceive(I).WMSLocation.DataInOut = UWMSLocation.StateInOut.StateStop Then
                    Else
                        If ListDataReceive(I).SateRead = False Then
                            If Microsoft.VisualBasic.Strings.Right(ControlData, 1) = "1" Then
                                ListDataReceive(I).SateRead = True
                                ListDataReceive(I).Data1 = "1"
                            End If

                        Else

                            If ListDataReceive(I).Data1 <> Microsoft.VisualBasic.Strings.Right(ControlData, 1) AndAlso ListDataReceive(I).Data1 <> "" Then
                                Dim UComtrol As UWMSLocation = ListDataReceive(I).WMSLocation

                                If ListDataReceive(I).WMSLocation.DataInOut = UWMSLocation.StateInOut.StateIn Then
                                    For Idx As Integer = UComtrol.ListUloc.Count - 1 To 0 Step -1
                                        If UComtrol.ListUloc(Idx).DataFill = False Then
                                            UComtrol.ListUloc(Idx).DataFill = True
                                            UComtrol.ListUloc(Idx).DataInfo = "XXXXXXXX"
                                            Exit For
                                        End If
                                    Next
                                Else
                                    For Idx As Integer = 0 To UComtrol.ListUloc.Count - 1
                                        If UComtrol.ListUloc(Idx).DataFill = True Then
                                            UComtrol.ListUloc(Idx).DataFill = False
                                            UComtrol.ListUloc(Idx).DataInfo = ""
                                            Exit For
                                        End If
                                    Next
                                End If


                                ListDataReceive(I).SateRead = False
                                ListDataReceive(I).Data1 = ""
                            End If

                        End If
                    End If


                    Exit For
                End If

            Next
            'dataval3 = ArrWeight(ArrLenght)
            'Dim StateAdd As Boolean = False
            'If AddDataArray(Val(ControlData)) Then
            '    StateAdd = True

            '    datavalweight = ArrWeight(ArrLenght)
            'End If

        End If

    End Sub

    Private Sub ClearDataArray()
        Try
            For I As Integer = 0 To ArrLenght
                ArrWeight(I) = 0
            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Function AddDataArray(value As Double) As Boolean
        For I As Integer = ArrLenght To 1 Step -1

            If I <= 1 Then
                ArrWeight(I) = value
            Else
                ArrWeight(I) = ArrWeight(I - 1)
            End If

        Next

        Dim StateWeight As Boolean = True
        Dim StateCheckReceive As Boolean = True

        For I As Integer = 1 To ArrLenght

            If ArrWeight(I) <> 0 Then

                If StateCheckReceive Then
                    StateCheckReceive = False
                End If

            End If

            If I > 1 Then

                If ArrWeight(I) <> ArrWeight(I - 1) Then
                    StateWeight = False
                    Exit For
                End If

            End If

        Next



        If value <= 0 Then
            StateWeight = False
        End If
        Return StateWeight
    End Function


    Private Sub ocmstart_Click(sender As Object, e As EventArgs) Handles ocmstart.Click
        _SerialPort.Clear()
        indatakgread = ""
        LineNo = ""

        With CType(Me.ogcdataport.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FTSelect='1'").Length > 0 Then

                Try
                    For Each R As DataRow In .Select("FTSelect='1'")
                        Dim sensor As New SerialPort(R!FTComport.ToString)
                        '  sensor = New SerialPort(FNComport.Text)

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

    End Sub

    Private Sub ocmstop_Click(sender As Object, e As EventArgs) Handles ocmstop.Click
        indatakgread = ""

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



    Private Function ReadXmlDefualtPort() As String
        Dim DS As New DataSet()
        Dim _DefaultPortName As String = ""

        Try
            DS.ReadXml(Application.StartupPath + "\Database.xml")
            _DefaultPortName = ""
            If (DS.Tables.IndexOf("DefualtFGPortName") > -1) Then
                _DefaultPortName = DS.Tables("DefualtFGPortName").Rows(0)("Name").ToString()
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

        Me.Tag = "999"
        ListDataReceive = New List(Of DataSenser)
        InitDataLocation()


    End Sub

    Private Sub InitDataLocation()
        Dim StartLoc As Integer = 5
        Dim cmdstring As String = ""
        Dim ObjectNumber As Integer = 0
        Dim dt As DataTable
        cmdstring = "  Select  WH.FTWHFGCode,  LEFT(WHL.FTWHLocCode,LEN(WHL.FTWHLocCode)-3) As FTWHLocCode"
        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG As WHL WITH(NOLOCK) INNER Join"
        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG As WH WITH(NOLOCK) On WHL.FNHSysWHFGId = WH.FNHSysWHFGId"
        cmdstring &= vbCrLf & "  Where WH.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ""
        cmdstring &= vbCrLf & "  And  (WHL.FTStateActive = '1') AND (WH.FTStateActive = '1')"
        cmdstring &= vbCrLf & " And LEN(WHL.FTWHLocCode) > 5"
        cmdstring &= vbCrLf & "  GROUP BY WH.FTWHFGCode, Left(WHL.FTWHLocCode, Len(WHL.FTWHLocCode) - 3) "
        cmdstring &= vbCrLf & "  ORDER BY  WH.FTWHFGCode, Left(WHL.FTWHLocCode, Len(WHL.FTWHLocCode) - 3)  "


        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In dt.Rows

            ObjectNumber = ObjectNumber + 1


            Dim XCI As New UWMSLocation(R!FTWHFGCode.ToString, R!FTWHLocCode.ToString)
            XCI.Name = "SenserDataNumber" & ObjectNumber.ToString
            XCI.DataInOut = UWMSLocation.StateInOut.StateStop
            XCI.Location = New System.Drawing.Point(StartLoc, 5)
            XCI.olbheadloc.Text = R!FTWHLocCode.ToString() & " (" & R!FTWHFGCode.ToString & ")"
            XCI.opnheader.Text = R!FTWHLocCode.ToString() & " (" & R!FTWHFGCode.ToString & ")"
            XCI.Tag = R!FTWHLocCode.ToString()
            olm.Controls.Add(XCI)

            Dim DSenser As New DataSenser
            DSenser.SateRead = False
            DSenser.ID = "SenserDataNumber" & ObjectNumber.ToString
            DSenser.Location = R!FTWHLocCode.ToString
            DSenser.Data1 = "0"
            DSenser.Data2 = "0"
            DSenser.WMSLocation = XCI
            ListDataReceive.Add(DSenser)

            StartLoc = StartLoc + 175


        Next
        dt.Dispose()

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

    Private Sub FGWMSInOut_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub FGWMSInOut_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Try
        '    If SerialPortData.IsOpen Then

        '        Try
        '            With SerialPortData
        '                RemoveHandler .DataReceived, AddressOf DataReceivedHandler
        '            End With

        '            SerialPortData.Close()

        '        Catch ex As Exception

        '        End Try

        '    End If
        'Catch ex As Exception

        'End Try

        indatakgread = ""

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


        Catch ex As Exception
        End Try

    End Sub

    Private Sub FGWMSInOut_ControlRemoved(sender As Object, e As ControlEventArgs) Handles Me.ControlRemoved

    End Sub
End Class
Public Class DataSenser
    Property ID As String
    Property Location As String
    Property SateRead As Boolean
    Property Data1 As String
    Property Data2 As String
    Property WMSLocation As UWMSLocation

End Class

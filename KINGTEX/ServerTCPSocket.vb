Imports System.Text
Imports System.Threading

Public Class ServerTCPSocket
    Dim TimeClose As Integer = 0

    Dim trdata As Thread

    Sub New()
        ' CheckProcess()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub CheckProcess()
        Dim myProcesses As Process() = Process.GetProcesses  ' = Process.GetProcessesByName("FinishGood")
        Dim myProcess As Process
        Dim appname As String = Process.GetCurrentProcess.ProcessName
        Dim Processindex As Integer = Process.GetCurrentProcess.Id

        ' MsgBox(appname)
        Dim sameprocess As Integer = Process.GetProcessesByName(appname).Length
        If sameprocess > 1 Then

            '   MsgBox("โปรแกรมได้ถูกเปิดไว้อยู่แล้ว")
            For Each myProcess In myProcesses
                '    If myProcess.MainWindowTitle = "" Then myProcess.Kill()
                If myProcess.Id = Processindex Then myProcess.Kill()

                'MsgBox(myProcess.ProcessName)
            Next myProcess
            Me.Close()
            Exit Sub
        End If

        '  Threading.Thread.Sleep(1)



    End Sub
    Sub EndProcess()
        Dim myProcesses As Process() = Process.GetProcesses  ' = Process.GetProcessesByName("FinishGood")
        Dim myProcess As Process
        Dim sameprocess As Integer = Process.GetProcessesByName("NewGPSReceive").Length
        Threading.Thread.Sleep(1)
        For Each myProcess In myProcesses

            '    If myProcess.MainWindowTitle = "" Then myProcess.Kill()
            If myProcess.ProcessName = "NewGPSReceive" Then myProcess.Kill()
            'MsgBox(myProcess.ProcessName)
        Next myProcess





    End Sub
    Private Sub BOpenPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BOpenPort.Click
        Me.GData.Rows.Clear()

        Try
            trdata.Abort()
        Catch ex As Exception
        End Try

        For i As Integer = CInt(Me.TStart.Text) To CInt(Me.TStart.Text)
            For i2 As Integer = 0 To 1000
                For i3 As Integer = 0 To 500
                    My.Application.DoEvents()
                Next
                My.Application.DoEvents()
            Next
            My.Application.DoEvents()

            Try
                Dim f As New ServerTCPSocketPort(i)
                f.PortNumber = i
                f.Show()
                ' f.WindowState = FormWindowState.Minimized
                Dim Str(2) As String
                Str(0) = DateTime.Now.ToString("HH:mm:ss")
                Str(1) = i
                Str(2) = "Port is Open Ok "
                Me.GData.Rows.Add(Str)
            Catch ex As Exception
                Dim Str(1) As String
                Str(0) = i
                Str(1) = "Port มีการเปิดใช้งานก่อนหน้านี้แล้ว  "
                Me.GData.Rows.Add(Str)
            End Try

            My.Application.DoEvents()
        Next

        'trdata = New Thread(AddressOf ChaeckDataInsert)
        'trdata.Start()

        Me.Timer1.Enabled = True
    End Sub

    Sub ChaeckDataInsert()
        While True
            ' Accept will block until someone connects
            Try
                While Me.GData.Rows.Count > 0

                    Dim ControlData As String = Me.GData.Rows(0).Cells(2).Value.ToString
                    Me.GData.Rows.RemoveAt(0)

                    Thread.Sleep(10)

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

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        My.Application.DoEvents()
        'If Me.GData.Rows.Count > 25 Then Me.GData.Rows.Clear()
        If Me.GPortClose.Rows.Count > 0 Then
            Me.TPortClose.Text = Me.GPortClose.Rows(0).Cells(0).Value
            Me.GPortClose.Rows.RemoveAt(0)
        End If
        If Me.TPortClose.Text <> "" Then
            Dim f As New ServerTCPSocketPort(Val(Me.TPortClose.Text))
            f.PortNumber = Me.TPortClose.Text
            f.Show()
            ' f.WindowState = FormWindowState.Minimized
            Dim Str(2) As String
            Str(0) = DateTime.Now.ToString("HH:mm:ss")
            Str(1) = Me.TPortClose.Text
            Str(2) = "Port is Re Open  "
            Me.GData.Rows.Add(Str)
            Me.TPortClose.Text = ""

        End If


    End Sub

    Private Sub AllportOpen_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        EndProcess()

        Try
            trdata.Abort()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub AllportOpen_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        EndProcess()
    End Sub

    Private Sub AllportOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' EndProcess()
        'CheckProcess()

        Me.BOpenPort.PerformClick()

        CheckProcessRunning()
        '  Windows.Forms.Cursor.Hide()
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
    Public Function String2Hex(ByVal str As String) As String
        Dim bytes() As Byte
        Dim ret As String
        bytes = Encoding.UTF8.GetBytes(str)
        Dim i As Integer = 1
        For Each b As Byte In bytes
            ret &= b.ToString("x").ToUpper
        Next
        Return ret.Trim()
    End Function
    Function StringToHex(ByVal text As String) As String
        Dim hex As String
        Dim x As Integer
        Dim IMEI, N, E, Speed, sos, Fuel As String
        Fuel = ""

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
        'เช็คว่า IMEI ที่ส่งมาหมดหรือยัง
        'For i As Integer = 0 To hex.Length - 1 Step 2
        '    If IsNumeric(hex.Substring(i, 2)) = False Then Exit For
        '    IMEI &= hex.Substring(i, 2)

        'Next

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

        Dim StrFuel() As String = Split(text, ",")
        Dim StrF As String = ""
        If StrFuel.Length >= 12 Then
            StrF = StrFuel(11)
            StrFuel = Split(StrF, "|")
            If StrFuel.Length >= 7 Then
                StrF = StrFuel(6)
                Fuel = Mid(StrF, 5, 2) & "." & Mid(StrF, 7, 2)

                If Val(Fuel) <> 0 Then
                    Fuel = Format((Val(Fuel) * 100) / 20, "0.00")
                End If
            End If
        End If

        If sos = "999901" Then sos = "ON"
        Return IMEI & "," & N & "," & E & "," & Speed & "," & sos
    End Function

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        My.Application.DoEvents()

        TimeClose += 1
        Me.Text = "  NEW " & Date.Now.ToString("dd MMMM yyyy HH:mm:ss") & "  " & TimeClose
        Me.Refresh()

        If TimeClose > 1800 Then
            'Me.Close()
        End If
        CheckProcessRunning()
    End Sub

    Sub CheckProcessRunning()
        'Dim myProcesses As Process() = Process.GetProcesses  ' = Process.GetProcessesByName("FinishGood")
        'Dim myProcess As Process
        'Dim appname As String = "CheckProcessRunning" 'Process.GetCurrentProcess.ProcessName
        'Dim Processindex As Integer = Process.GetCurrentProcess.Id

        '' MsgBox(appname)
        'Dim sameprocess As Integer = Process.GetProcessesByName(appname).Length
        'If sameprocess > 0 Then
        '    Exit Sub
        'Else
        '    'Shell(CurDir() & "/CheckProcessRunning.exe", AppWinStyle.NormalFocus)
        'End If
        'CheckProcessRunning
        '  Threading.Thread.Sleep(1)



    End Sub

    Private Sub GData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GData.CellContentClick

    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class
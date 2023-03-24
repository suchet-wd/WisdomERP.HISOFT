Imports System.IO
Imports System.Timers
Imports System.Windows.Forms

Public Class Test_Tuk_Service
    'Dim Mythread As Threading.Thread
    'Dim RetVal
    Private t As System.Timers.Timer


    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        ' Log("OnStart")

        Try
           
            Call DB.GetXmlConnectionString()
            ' Call ReadPathAppName()

            Try
                If File.Exists(Application.StartupPath & "\Log_Service.txt") = True Then
                    File.Delete(Application.StartupPath & "\Log_Service.txt")
                End If
            Catch ex As Exception

            End Try

            t = New System.Timers.Timer(59999)
            AddHandler t.Elapsed, AddressOf TimerFired
            With t
                .AutoReset = True
                .Enabled = True
                .Start()
                Log("Service Starting") ' At : " + Date.Now.ToString("HH:mm"))

            End With
        Catch obug As Exception
            Log("Error = " & obug.Message)
            Throw obug
        End Try

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        ' Mythread.Abort()
        'Dim myProcess = Process.GetProcessById(RetVal)
        'myProcess.Kill()


        Try
            'log.WriteEntry("Service Stopping", EventLogEntryType.Information)
            ' Log("Service Stopping At : " + Date.Now.ToString("HH:mm"))
            t.Stop()
            t.Dispose()
        Catch obug As Exception
            Log("Error = " & obug.Message)
        End Try

        Log("Service Stopped successfully")
    End Sub

    Private Sub TimerFired(ByVal sender As Object, ByVal e As ElapsedEventArgs)

        Try

            ' Log("TimerFired  Work ")

            ' Dim ProcessName As Process() = Process.GetProcessesByName("HIsoftService.exe")
            Dim ProcessName As Process() = Process.GetProcessesByName(_AppServiceName)
            Dim ProcessAppName As Process() = Process.GetProcessesByName(_AppName)

            ' Log(ProcessName.Length)

            If ProcessName.Length >= 1 Or ProcessAppName.Length >= 1 Then
                ' Log("TimerFired  true ")

            Else

                ' ทำการ run Bat file  โดยผ่านโปรแกรม  HIsoftService.exe
                Call seProcess_Tuk()

                ' Log("TimerFired  false ")
            End If

        Catch ex As Exception
            Log("Error =" + Err.Number.ToString + vbCrLf + ex.ToString())
        End Try

    End Sub

    Public Shared Sub seProcess_Tuk()
        Try
            '  Log("seProcess_Tuk")

            ' ต้องเปลี่ยน Path ที่เก็บ HIsoftService.exe
            ' ServiceShell.Start("E:\HI SOFT PROJECT\HI SOFT\HIsoftService\bin\Debug\HIsoftService.exe", " Start", Nothing)  'ใช้งาน

            ' ทำงานที่ Programer100
            ' ServiceShell.Start(_PathAppName & "\" & _AppServiceName & ".exe", " Start", Nothing)
            ServiceShell.Start(Application.StartupPath & "\HICallExe.exe", " Start", Nothing)
            ' Log(_PathAppName & "\" & _AppServiceName & ".exe")
            '  ServiceShell.Start("E:\Project_Hi_Tuk\HI SOFT PROJECT\HIsoftService.exe", " Start", Nothing)

        Catch ex As Exception
            Log("Error =" + Err.Number.ToString + vbCrLf + ex.ToString())
        End Try
    End Sub

    Private Shared _PathAppName As String = ""
    Private Shared _AppName As String = ""
    Private Shared _AppServiceName As String = ""

    Public Shared Sub ReadPathAppName()

        While _PathAppName = ""
            Try
                For I As Integer = 1 To 900000000

                Next
                Dim _Qry As String = "Select Top 3  FTCfgName,FTCfgData  FROM [" & DB.GetDataBaseName(DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS A WITH(NOLOCK) WHERE FTCfgName in ('AppPath','AppName','AppSevice') "
                Dim dt As DataTable

                dt = SQLConn.GetDataTable(_Qry, DB.DataBaseName.DB_SECURITY)

                For Each R As DataRow In dt.Rows


                    Select Case R!FTCfgName.ToString.Trim.ToUpper
                        Case "AppPath".ToUpper
                            _PathAppName = R!FTCfgData.ToString
                        Case "AppName".ToUpper
                            _AppName = R!FTCfgData.ToString
                        Case "AppSevice".ToUpper
                            _AppServiceName = R!FTCfgData.ToString
                    End Select
                Next

            Catch ex As Exception
            End Try
        End While

    End Sub

   

    Public Shared Sub Log(str As String)
        Try
            Dim fileWritter As StreamWriter = File.AppendText(Application.StartupPath & "\Log_Service.txt")
            fileWritter.WriteLine(DateTime.Now.ToString() + " " + str)
            fileWritter.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class

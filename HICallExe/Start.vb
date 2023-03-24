Imports System.Management
Imports System.Windows.Forms
Imports System.Timers
Imports System.Security.Permissions

Friend Class Start
    Public Declare Function GetVolumeSerialNumber Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As Long, ByVal nVolumeNameSize As Long, ByVal lpVolumeSerialNumber As Long, ByVal lpMaximumComponentLength As Long, ByVal lpFileSystemFlags As Long, ByVal lpFileSystemNameBuffer As Long, ByVal nFileSystemNameSize As Long) As Long
    Private Shared t As System.Timers.Timer

    Private Sub New()
    End Sub

    <STAThread()> _
    Shared Sub Main()

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        '----------- Read Connecttion String From File XML
        HI.Conn.DB.GetXmlConnectionString()
        Call ReadPathAppName()

        If _PathAppName <> "" And _AppServiceName <> "" Then
            Try
                ' Log("TimerFired  Work ")

                ' Dim ProcessName As Process() = Process.GetProcessesByName("HIsoftService.exe")
                Dim ProcessName As Process() = Process.GetProcessesByName(_AppServiceName)
                Dim ProcessName2 As Process() = Process.GetProcessesByName(_AppServiceName & ".exe")

                Dim ProcessAppName As Process() = Process.GetProcessesByName(_AppName)
                Dim ProcessAppName2 As Process() = Process.GetProcessesByName(_AppName & ".exe")

                ' Log(ProcessName.Length)

                If ProcessName.Length >= 1 Or ProcessAppName.Length >= 1 Or ProcessName2.Length >= 1 Or ProcessAppName2.Length >= 1 Then
                    ' Log("TimerFired  true ")
                Else
                    ' ทำการ run Bat file  โดยผ่านโปรแกรม  HIsoftService.exe
                    Dim _CallPathFile As String = _PathAppName & "\" & _AppServiceName & ".Exe"

                    Process.Start(_CallPathFile)
                    '------------------------------------

                    'Log("TimerFired  false ")
                    'ServiceShell.Start(_PathAppName & "\" & _AppServiceName & ".exe", " Start", Nothing)
                End If

            Catch ex As Exception
            End Try
        End If

        End
    End Sub

    Private Shared _PathAppName As String = ""
    Private Shared _AppName As String = ""
    Private Shared _AppServiceName As String = ""

    Public Shared Sub ReadPathAppName()

        ' While _PathAppName = ""
        Try

            If HI.Conn.DB.AppService = "" Or HI.Conn.DB.AppServiceName = "" Or HI.Conn.DB.AppServicePath = "" Then

                For I As Integer = 1 To 900000000

                Next

                Dim _Qry As String = "Select Top 3  FTCfgName,FTCfgData  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS A WITH(NOLOCK) WHERE FTCfgName in ('AppPath','AppName','AppSevice') "
                Dim dt As DataTable

                dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_SECURITY)

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

            Else

                _AppName = HI.Conn.DB.AppService
                _PathAppName = HI.Conn.DB.AppServicePath
                _AppServiceName = HI.Conn.DB.AppServiceName

            End If

        Catch ex As Exception
        End Try
        ' End While

    End Sub

End Class

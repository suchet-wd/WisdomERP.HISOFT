Imports System.Management
Imports System.Windows.Forms
Imports System.Timers

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


        Application.EnableVisualStyles()

        Application.SetCompatibleTextRenderingDefault(False)

        '----------- Read Connecttion String From File XML
        HI.Conn.DB.GetXmlConnectionString()

        'Call ReadPathAppName()
        HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
        Try
            Dim ProcessAppName As Process() = Process.GetProcessesByName(_AppName)
            Dim ProcessAppName2 As Process() = Process.GetProcessesByName(_AppName & ".exe")

            If ProcessAppName.Length >= 1 Or ProcessAppName2.Length >= 1 Then
                End
            End If

        Catch ex As Exception
        End Try

        Try
            Application.Run(New ConvertPDF)
        Catch ex As Exception
        End Try

    End Sub

    Private Shared _PathAppName As String = ""
    Private Shared _AppName As String = ""


    Public Shared Sub ReadPathAppName()

        ' While _PathAppName = ""
        Try

            _AppName = My.Application.Info.AssemblyName


        Catch ex As Exception
        End Try
        ' End While


    End Sub

End Class

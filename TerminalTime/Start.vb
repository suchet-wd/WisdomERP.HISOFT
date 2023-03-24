Imports System.Management
Imports HI

Friend Class Start
    Public Declare Function GetVolumeSerialNumber Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As Long, ByVal nVolumeNameSize As Long, ByVal lpVolumeSerialNumber As Long, ByVal lpMaximumComponentLength As Long, ByVal lpFileSystemFlags As Long, ByVal lpFileSystemNameBuffer As Long, ByVal nFileSystemNameSize As Long) As Long
    Private Sub New()
    End Sub

    <STAThread()> _
    Shared Sub Main()

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        Application.EnableVisualStyles()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin")

        Try
            Dim _Theme As String = HI.UL.AppRegistry.ReadRegistry(HI.UL.AppRegistry.KeyName.Theme)

            If _Theme <> "" Then
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(_Theme)
            End If
        Catch ex As Exception
        End Try

        Application.SetCompatibleTextRenderingDefault(False)

        '----------- Read Connecttion String From File XML
        HI.Conn.DB.GetXmlConnectionString()
        '----------- Read Connecttion String From File XML



        HI.ST.Lang.Language = ST.Lang.eLang.TH
            HI.ST.UserInfo.UserName = ""
            HI.ST.UserInfo.UserLogInComputer = System.Environment.MachineName
            HI.ST.UserInfo.UserLogInComputerIP = "000.000.000.000"

            Dim _Date As DateTime = HI.UL.ULDate.GetOnServer(HI.Conn.DB.DataBaseName.DB_SYSTEM)
            HI.ST.UserInfo.LogINDate = Format(_Date, "dd/MM/yyyy")
            HI.ST.UserInfo.LogINTime = Format(_Date, "HH:mm:ss")

            HI.ST.SysInfo.AppName = HI.Conn.DB._AppName
            HI.ST.SysInfo.CmpID = 0
            HI.ST.SysInfo.CmpCode = ""
            HI.ST.SysInfo.CmpDefualtCode = ""


        Dim _LCDLanguage As Integer = 0

        Try
                HI.ST.Lang.Language = CType(_LCDLanguage, HI.ST.Lang.eLang)
            Catch ex As Exception
            End Try



            Dim _LCDDisplay As New HI.HR.wTerminalTime
            Dim oSysLang As New ST.SysLanguage
            Try
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplay.Name.ToString.Trim, _LCDDisplay)
            Catch ex As Exception
            Finally
            End Try


            Application.Run(_LCDDisplay)


    End Sub

End Class

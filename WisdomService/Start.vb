Imports System.Management

Friend Class Start

    Public Declare Function GetVolumeSerialNumber Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As Long, ByVal nVolumeNameSize As Long, ByVal lpVolumeSerialNumber As Long, ByVal lpMaximumComponentLength As Long, ByVal lpFileSystemFlags As Long, ByVal lpFileSystemNameBuffer As Long, ByVal nFileSystemNameSize As Long) As Long
    Private Sub New()
    End Sub

    <STAThread()>
    Shared Sub Main()

        Dim user() As String = System.Environment.GetCommandLineArgs
        Dim username As String = "admin"
        Dim userlang As String = "1"
        Dim cmpcode As String = "HT91"
        Try
            username = user(1)
        Catch ex As Exception
        End Try
        'username = "admin"
        Try
            userlang = user(2)
        Catch ex As Exception
        End Try

        Try
            cmpcode = user(3)
        Catch ex As Exception
        End Try

        If username = "" Then
            Application.Exit()
        End If

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

        HI.ST.UserInfo.UserName = username
        HI.ST.SysInfo.CmpCode = cmpcode
        HI.ST.UserInfo.ServiceLogin()
        Call HI.TL.CboList.PrepareList()

        If Val(userlang) = 0 Then
            HI.ST.Lang.Language = HI.ST.Lang.eLang.EN
        Else
            HI.ST.Lang.Language = Val(userlang)
        End If

        HI.ST.SysInfo.ModuleID = 0

        If HI.Conn.SQLConn.GetField("Select Convert(varchar(10),GetDate(),111)", HI.Conn.DB.DataBaseName.DB_SYSTEM) = "" Then
            MsgBox("Can't Connect Database Please Contact Admin...!!!")
            Application.Exit()
        Else
            Application.Run(New WisdomService)
        End If

    End Sub

End Class

Imports System.Management

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

        Try

            Dim _Font As String = HI.UL.AppRegistry.ReadRegistry(UL.AppRegistry.KeyName.Font)

            If _Font <> "" Then

                HI.ST.SysInfo.SystemFontName = _Font

            End If

            Dim FFont01 As System.Drawing.Font = DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont
            Dim FFontSize01 As Double = FFont01.Size
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize01)

            FFont01 = DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont
            FFontSize01 = FFont01.Size
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize01)

            FFont01 = DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont
            FFontSize01 = FFont01.Size
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize01)
            FFont01.Dispose()

        Catch ex As Exception
        End Try


        'HI.ST.SysInfo.Admin = True
        'HI.ST.UserInfo.UserName = "Admin"

        'Dim _W1 As New HI.Service.wLineLeaderApproved()
        'Dim _W2 As New HI.Service.wQAFinalLeaderApproved()

        'Try

        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _W1.Name.ToString.Trim, _W1)
        'Catch ex As Exception
        'Finally
        'End Try

        'Try

        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _W2.Name.ToString.Trim, _W2)
        'Catch ex As Exception
        'Finally
        'End Try

        'Dim DS As New DataSet()             
        'DS.ReadXml(Application.StartupPath + "\NikeFile.xml")

        'Dim Mpo As String = HI.UL.ULF.Convert_Bath_EN(1058.66)

        If HI.Conn.SQLConn.GetField("Select Convert(varchar(10),GetDate(),111)", Conn.DB.DataBaseName.DB_SYSTEM) = "" Then

            MsgBox("Can't Connect Database Please Contact Admin...!!!")
            Application.Exit()

        Else

            HI.ST.SysInfo.DevelopMode = True
            HI.APP.AppStart.Start()

        End If

    End Sub

End Class

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

        If HI.Conn.SQLConn.GetField("Select Convert(varchar(10),GetDate(),111)", HI.Conn.DB.DataBaseName.DB_SYSTEM) = "" Then
            MsgBox("Can't Connect Database Please Contact Admin...!!!")
            Application.Exit()
        Else

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

            Dim _Qry As String = ""
            Dim _FNLCDType As Integer = 0
            Dim _LCDLanguage As Integer = 0
            Dim dt As DataTable

            _Qry = "SELECT TOP 1  ISNULL(M.FNLCDType,0) AS FNLCDType,ISNULL(LCDLanguage,2) AS LCDLanguage"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigCom AS M WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE M.FTComputerName='" & HI.UL.ULF.rpQuoted(System.Environment.MachineName) & "'"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In dt.Rows

                _FNLCDType = Val(R!FNLCDType.ToString)
                _LCDLanguage = Val(R!LCDLanguage.ToString)

            Next

            dt.Dispose()

            Try
                HI.ST.Lang.Language = CType(_LCDLanguage, HI.ST.Lang.eLang)
            Catch ex As Exception
            End Try

            Select Case _FNLCDType
                Case 0

                    Dim _LCDDisplay As New HI.LCD.LCDDisplay
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplay.Name.ToString.Trim, _LCDDisplay)
                    Catch ex As Exception
                    Finally
                    End Try


                    Application.Run(_LCDDisplay)

                Case 1

                    Dim _LCDDisplayIncentive As New HI.LCD.LCDDisplayIncentive
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentive.Name.ToString.Trim, _LCDDisplayIncentive)
                    Catch ex As Exception
                    Finally
                    End Try

                    Application.Run(_LCDDisplayIncentive)

                Case 2

                    Dim _LCDDisplayIncentiveMD2 As New HI.LCD.LCDDisplayIncentiveMD2
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentiveMD2.Name.ToString.Trim, _LCDDisplayIncentiveMD2)
                    Catch ex As Exception
                    Finally
                    End Try

                    Application.Run(_LCDDisplayIncentiveMD2)

                    'Case 3
                    '    Dim _LCDDisplayIncentiveNew As New HI.LCD.LCDDisplayIncentive_New
                    '    Dim oSysLang As New ST.SysLanguage
                    '    Try
                    '        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentiveNew.Name.ToString.Trim, _LCDDisplayIncentiveNew)
                    '    Catch ex As Exception
                    '    Finally
                    '    End Try

                    '    Application.Run(_LCDDisplayIncentiveNew)
                    'Case 4
                    '    Dim _LCDDisplayIncentiveMD2New As New HI.LCD.LCDDisplayIncentiveMD2_New
                    '    Dim oSysLang As New ST.SysLanguage
                    '    Try
                    '        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentiveMD2New.Name.ToString.Trim, _LCDDisplayIncentiveMD2New)
                    '    Catch ex As Exception
                    '    Finally
                    '    End Try

                    '    Application.Run(_LCDDisplayIncentiveMD2New)
                Case 3

                    Dim _LCDDisplayIncentive As New HI.LCD.LCDDisplayIncentiveOneScreen
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentive.Name.ToString.Trim, _LCDDisplayIncentive)
                    Catch ex As Exception
                    Finally
                    End Try

                    Application.Run(_LCDDisplayIncentive)

                Case 4

                    Dim _LCDDisplayIncentive As New HI.LCD.LCDIncentiveV2
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentive.Name.ToString.Trim, _LCDDisplayIncentive)
                    Catch ex As Exception
                    Finally
                    End Try

                    Application.Run(_LCDDisplayIncentive)

                Case 5

                    Dim _LCDDisplayIncentive As New HI.LCD.LCDIncentiveCDV2
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentive.Name.ToString.Trim, _LCDDisplayIncentive)
                    Catch ex As Exception
                    Finally
                    End Try

                    Application.Run(_LCDDisplayIncentive)

                Case 6

                    Dim _LCDDisplayIncentive As New HI.LCD.LCDIncentiveV3
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentive.Name.ToString.Trim, _LCDDisplayIncentive)
                    Catch ex As Exception
                    Finally
                    End Try

                    Application.Run(_LCDDisplayIncentive)

                Case 7

                    Dim _LCDDisplayIncentive As New HI.LCD.LCDIncentiveV3OneScreen
                    Dim oSysLang As New ST.SysLanguage

                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplayIncentive.Name.ToString.Trim, _LCDDisplayIncentive)
                    Catch ex As Exception
                    Finally
                    End Try


                    Application.Run(_LCDDisplayIncentive)

                Case Else

                    Dim _LCDDisplay As New HI.LCD.LCDDisplay
                    Dim oSysLang As New ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _LCDDisplay.Name.ToString.Trim, _LCDDisplay)
                    Catch ex As Exception
                    Finally
                    End Try

                    Application.Run(_LCDDisplay)

            End Select
           
           
        End If

    End Sub

End Class

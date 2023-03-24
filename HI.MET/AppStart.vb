Imports System.Management
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class AppStart
    '<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    'Shared Function SetWindowText(ByVal hWnd As IntPtr, ByVal lpString As String) As Boolean
    'End Function

    '<DllImport("user32.dll", SetLastError:=True)>
    'Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    'End Function

    'Public Shared Sub Start()

    '    HI.ST.UserInfo.UserName = ""
    '    HI.ST.UserInfo.UserLogInComputer = System.Environment.MachineName
    '    HI.ST.UserInfo.UserLogInComputerIP = "000.000.000.000"

    '    Dim _Date As DateTime = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)
    '    HI.ST.UserInfo.LogINDate = Format(_Date, "dd/MM/yyyy")
    '    HI.ST.UserInfo.LogINTime = Format(_Date, "HH:mm:ss")

    '    HI.ST.SysInfo.AppName = HI.Conn.DB._AppName

    '    Call PrepareLanguage()
    '    Call HI.TL.CboList.PrepareList()
    '    Call LoadSystemConfig()
    '    Call HI.TL.HandlerControl.CreateManuStripGrid()

    '    Dim _lang As String = HI.UL.AppRegistry.ReadRegistry(UL.AppRegistry.KeyName.Language)
    '    Dim _DefaultCmp As String = HI.UL.AppRegistry.ReadRegistry(UL.AppRegistry.KeyName.Cmp)

    '    If _lang = "" Then
    '        HI.ST.Lang.Language = ST.Lang.eLang.TH
    '        HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Language, HI.ST.Lang.Language)
    '    Else
    '        HI.ST.Lang.Language = CType([Enum].Parse(GetType(HI.ST.Lang.eLang), _lang), HI.ST.Lang.eLang)
    '    End If

    '    If _DefaultCmp <> "" Then
    '        HI.ST.SysInfo.CmpDefualtCode = _DefaultCmp
    '    End If
    '    'Dim M As New HI.APP.ExportData()
    '    'HI.TL.HandlerControl.AddHandlerObj(M)
    '    'M.ShowDialog()

    '    ' Dim mCH As New HI.CH.CheckEnvironment
    '    Dim StateUserAD As Boolean = False

    '    Call UserLogIn(StateUserAD)

    'End Sub

    Public Shared Sub Start()

        HI.ST.UserInfo.UserName = ""
        HI.ST.UserInfo.UserLogInComputer = System.Environment.MachineName
        HI.ST.UserInfo.UserLogInComputerIP = "000.000.000.000"

        Dim _Date As DateTime = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)
        HI.ST.UserInfo.LogINDate = Format(_Date, "dd/MM/yyyy")
        HI.ST.UserInfo.LogINTime = Format(_Date, "HH:mm:ss")

        HI.ST.SysInfo.AppName = HI.Conn.DB._AppName

        Call PrepareLanguage()
        Call HI.TL.CboList.PrepareList()
        Call LoadSystemConfig()
        Call HI.TL.HandlerControl.CreateManuStripGrid()

        Dim _lang As String = HI.UL.AppRegistry.ReadRegistry(UL.AppRegistry.KeyName.Language)
        Dim _DefaultCmp As String = HI.UL.AppRegistry.ReadRegistry(UL.AppRegistry.KeyName.Cmp)

        If _lang = "" Then
            HI.ST.Lang.Language = ST.Lang.eLang.TH
            HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Language, HI.ST.Lang.Language)
        Else
            HI.ST.Lang.Language = CType([Enum].Parse(GetType(HI.ST.Lang.eLang), _lang), HI.ST.Lang.eLang)
        End If

        If _DefaultCmp <> "" Then
            HI.ST.SysInfo.CmpDefualtCode = _DefaultCmp
        End If
        'Dim M As New HI.APP.ExportData()
        'HI.TL.HandlerControl.AddHandlerObj(M)
        'M.ShowDialog()

        ' Dim mCH As New HI.CH.CheckEnvironment
        Dim StateUserAD As Boolean = False
        Dim StringUser As String = GetUserName()
        HI.ST.UserInfo.WindowUserName = StringUser
        If StringUser <> "" Then
            StateUserAD = HI.ST.UserInfo.VerifyLoginUserAD(StringUser)

            'If StateUserAD Then
            '    HI.ST.UserInfo.WindowUserName
            'End If

        End If

        'If StateUserAD Then
        '    MsgBox(StringUser)
        'End If
        Call UserLogIn(StateUserAD)

    End Sub




    Public Shared Function GetUserName() As String
        If TypeOf My.User.CurrentPrincipal Is
      Security.Principal.WindowsPrincipal Then
            ' The application is using Windows authentication.
            ' The name format is DOMAIN\USERNAME.
            'Dim parts() As String = Split(My.User.Name, "\")
            'Dim username As String = parts(1)


            Dim parts() As String = Split(Environment.UserName, "\")
            Dim username As String = parts(1)
            Return username
        Else
            ' The application is using custom authentication.
            Return Environment.UserName ' My.User.Name
        End If
    End Function

    Public Shared Sub PrepareLanguage()
        Dim _Dt As DataTable
        Dim _Strsql As String

        _Strsql = "SELECT TOP 11  FNHSysMessageID, FTMessageTH, FTMessageEN FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSystemMessasge WITH (NOLOCK) WHERE FNHSysMessageID>=1000000001 AND FNHSysMessageID<=1000000011   "
        _Dt = HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_SYSTEM)

        For Each Row In _Dt.Rows
            Select Case Long.Parse(Row!FNHSysMessageID.ToString)
                Case 1000000001
                    HI.MG.Msg.SaveData = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000002
                    HI.MG.Msg.DeleteData = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000003
                    HI.MG.Msg.SaveDataComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000004
                    HI.MG.Msg.DeleteDataComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000005
                    HI.MG.Msg.SaveDataNotComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000006
                    HI.MG.Msg.DeleteDataNotComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000007
                    HI.MG.Msg.MSelect = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000008
                    HI.MG.Msg.Input = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000009
                    HI.MG.Msg.PleaseWait = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000010
                    HI.MG.Msg.ExiHSystem = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000011
                    HI.MG.Msg.GenDocument = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
            End Select

        Next

        _Dt.Dispose()

    End Sub

    Public Shared Function VolumeSerial(ByVal DriveLetter) As String

        Dim disk As ManagementObject = _
            New ManagementObject(String.Format("Win32_Logicaldisk='{0}'", DriveLetter))
        Dim VolumeName As String = disk.Properties("Volumename").Value.ToString()
        Dim SerialNumber As String = disk.Properties("Volumeserialnumber").Value.ToString()
        Return (SerialNumber.Insert(4, "-"))

    End Function

    Public Shared Sub LoadSystemConfig()

        Dim _Str As String = "SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig WITH(NOLOCK)"
        Dim _dt As DataTable
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        For Each R As DataRow In _dt.Rows
            HI.ST.SysInfo.CmpDefualtCode = R!FTCmpCode.ToString
            HI.ST.Config.StateNotShowEmpLeave = (R!FTStateNotShowEmpLeave.ToString = "1")
            Exit For
        Next

    End Sub

    Public Shared Sub UserLogOff(FormMain As Object)
        FormMain.Close()
        Call UserLogIn()
    End Sub

    Public Shared Sub UserLogIn(Optional UserAD As Boolean = False)

        If UserInitialize(UserAD) = False Then

            Application.Exit()

        Else

            Try

                Dim ProcessAppName As Process() = Process.GetProcessesByName("WisdomService" & HI.ST.UserInfo.UserName)
                Dim ProcessAppName2 As Process() = Process.GetProcessesByName("WisdomService" & HI.ST.UserInfo.UserName & ".exe")
                Dim ProcessAppName3 As Process() = Process.GetProcessesByName("WisdomService")
                Dim ProcessAppName4 As Process() = Process.GetProcessesByName("WisdomService.exe")

                If ProcessAppName.Length >= 1 Or ProcessAppName2.Length >= 1 Or ProcessAppName3.Length >= 1 Or ProcessAppName4.Length >= 1 Then

                    For Each pkill As Process In ProcessAppName
                        pkill.Kill()
                    Next

                    For Each pkill As Process In ProcessAppName2
                        pkill.Kill()
                    Next

                    For Each pkill As Process In ProcessAppName3
                        pkill.Kill()
                    Next

                    For Each pkill As Process In ProcessAppName4
                        pkill.Kill()
                    Next

                End If

            Catch ex As Exception
                ' MsgBox(ex.Message())
            End Try

            Try

                Dim p = New System.Diagnostics.Process()

                p.StartInfo.FileName = Application.StartupPath & "\WisdomService.exe"
                p.StartInfo.Arguments = HI.ST.UserInfo.UserName & " " & Integer.Parse(Val(HI.ST.Lang.Language)).ToString & " " & HI.ST.SysInfo.CmpCode
                p.StartInfo.UseShellExecute = True
                p.Start()

            Catch ex As Exception
                ' MsgBox(ex.Message() & "222")
            End Try
            'Dim InPtrWindowHandel As IntPtr = FindWindow(0, "WisdomService")
            'SetWindowText(InPtrWindowHandel, "WisdomService" & HI.ST.UserInfo.UserName)


            Application.Run(New HI.APP.Main())

        End If

    End Sub

    Public Shared Function UserInitialize(Optional UserAD As Boolean = False) As Boolean
        Try
            Return HI.ST.UserInfo.Login(UserAD)
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message.GetHashCode(), ex.Message, "System", System.Windows.Forms.MessageBoxIcon.Error)
            Return False
        End Try
    End Function

End Class

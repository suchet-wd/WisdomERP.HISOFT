Imports System.Runtime.InteropServices
Imports System.Text
Imports System.IO

Public Class ServiceShell

    Private Sub New()
    End Sub
    ' egl1044
    ' Windows XP or later
    Private Const CreateUnicodeEnvironment As Integer = &H400

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Private Structure StartupInfo
        Dim cb As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Dim lpReserved As String
        <MarshalAs(UnmanagedType.LPTStr)> Dim lpDesktop As String
        <MarshalAs(UnmanagedType.LPTStr)> Dim lpTitle As String
        Dim dwX As Integer
        Dim dwY As Integer
        Dim dwXSize As Integer
        Dim dwXCountChars As Integer
        Dim dwYCountChars As Integer
        Dim dwFillAttribute As Integer
        Dim dwFlags As Integer
        Dim wShowWindow As Short
        Dim cbReserved2 As Short
        Dim lpReserved2 As IntPtr
        Dim hStdInput As IntPtr
        Dim hStdOutput As IntPtr
        Dim hStdError As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Private Structure ProcessInformation
        Dim hProcess As IntPtr
        Dim hThread As IntPtr
        Dim dwProcessID As Integer
        Dim dwThreadID As Integer
    End Structure


    '<DllImport("Advapi32.dll", ExactSpelling:=False, SetLastError:=True, CharSet:=CharSet.Unicode)> _
    'Private Shared Function CreateProcessAsUser(ByVal hToken As IntPtr, ByVal lpApplicationName As String, <[In](), Out(), [Optional]()> ByRef lpCommandLine As StringBuilder, ByVal lpProcessAttributes As IntPtr, ByVal lpThreadAttributes As IntPtr, <MarshalAs(UnmanagedType.Bool)> ByVal bInheritHandles As Boolean, ByVal dwCreationFlags As Integer, ByVal lpEnvironment As IntPtr, ByVal lpCurrentDirectory As String, <[In]()> ByRef lpStartupInfo As StartupInfo, <Out()> ByRef lpProcessInformation As ProcessInformation) As <MarshalAs(UnmanagedType.Bool)> Boolean
    'End Function
    <DllImport("Userenv.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function CreateEnvironmentBlock(ByRef lpEnvironment As IntPtr, ByVal hToken As IntPtr, <MarshalAs(UnmanagedType.Bool)> ByVal bInherit As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("Userenv.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function DestroyEnvironmentBlock(ByVal lpEnvironment As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("Wtsapi32.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function WTSQueryUserToken(ByVal SessionId As Integer, ByRef phToken As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("Kernel32.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function WTSGetActiveConsoleSessionId() As Integer
    End Function
    <DllImport("Kernel32.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("Advapi32.dll", ExactSpelling:=False, SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function CreateProcessAsUser(ByVal hToken As IntPtr, ByVal lpApplicationName As String, <[In](), Out(), [Optional]()> ByVal lpCommandLine As StringBuilder, ByVal lpProcessAttributes As IntPtr, ByVal lpThreadAttributes As IntPtr, <MarshalAs(UnmanagedType.Bool)> ByVal bInheritHandles As Boolean, ByVal dwCreationFlags As Integer, ByVal lpEnvironment As IntPtr, ByVal lpCurrentDirectory As String, <[In]()> ByRef lpStartupInfo As StartupInfo, <Out()> ByRef lpProcessInformation As ProcessInformation) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function




    Public Shared Sub Start(ByVal filename As String, ByVal commandLine As String, ByVal workingDirectory As String, Optional ByVal sessionId As Integer = -1)


        Try

            Dim hToken As IntPtr
            Dim pEnvBlock As IntPtr
            Dim dwSessionId As Integer
            Dim pi As ProcessInformation = Nothing
            Dim si As StartupInfo = Nothing

            '   If no session Id was specified defaults to using the session Id
            '   that is currently attached to the physical console.
            If sessionId = (-1) Then

                dwSessionId = WTSGetActiveConsoleSessionId()
                ' Log("ture" & dwSessionId)

            Else

                dwSessionId = sessionId
                ' Log("False" & dwSessionId)

            End If


            '   Obtain the primary access token of the logged-on user specified by the session
            '   * Note: This can only be called successfully under LocalSystem context.
            If WTSQueryUserToken(dwSessionId, hToken) Then

                '   Get the enviorment block pointer for the user session.
                If CreateEnvironmentBlock(pEnvBlock, hToken, False) Then

                    '  Log("CreateEnvironmentBlock")

                    ' Setup command line buffer for unicode version
                    ' Log(filename)
                    Dim cmdLine As New StringBuilder(commandLine, 32768)

                    If CreateProcessAsUser(hToken, filename, cmdLine, IntPtr.Zero, IntPtr.Zero, False, _
                                           CreateUnicodeEnvironment, pEnvBlock, workingDirectory, si, pi) Then
                        ' Log("CreateProcessAsUser")

                        '   cleanup handle information
                        CloseHandle(pi.hProcess)
                        CloseHandle(pi.hThread)
                    Else
                        ' Log("Fail")
                    End If

                    '   Destroy enviorment block pointer.
                    DestroyEnvironmentBlock(pEnvBlock)
                    '  Log("DestroyEnvironmentBlock")

                End If

                '   cleanup user session token handle.
                CloseHandle(hToken)

            End If

        Catch ex As Exception

            Log("Error =" + Err.Number.ToString + vbCrLf + ex.ToString())

        End Try
    End Sub

    Public Shared Sub Log(str As String)
        Try
            Dim fileWritter As StreamWriter = File.AppendText("C:\Log_Shell_Service.txt")
            fileWritter.WriteLine(DateTime.Now.ToString() + " " + str)
            fileWritter.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class



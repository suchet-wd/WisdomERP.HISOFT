Imports System.IO

Public Class wPOSPopUpSetServerName

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call GetComputerName()
    End Sub

    Private _Proc As Boolean = False
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private Sub wPopUpSetServerName_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FTComputerName.Text = "SVR_HISOFT"

        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetComputerName()
        Try

            Dim _netUtil As Process = New Process
            _netUtil.StartInfo.FileName = "net.exe"
            _netUtil.StartInfo.CreateNoWindow = True
            _netUtil.StartInfo.Arguments = "view"
            _netUtil.StartInfo.RedirectStandardOutput = True
            _netUtil.StartInfo.UseShellExecute = False
            _netUtil.StartInfo.RedirectStandardError = True
            _netUtil.Start()

            Dim streamReader As StreamReader = New StreamReader(_netUtil.StandardOutput.BaseStream, _netUtil.StandardOutput.CurrentEncoding)
            Dim line As String

            With Me.FTComputerName
                .Properties.Items.Clear()
                .Properties.Items.Add("")
                Do
                    line = streamReader.ReadLine()
                    If line.StartsWith("\\") Then
                        Dim _pcName As String = line.Substring(2).Substring(0, line.Substring(2).IndexOf(" ")).ToUpper()
                        Try
                            Dim _MyIp As String = Convert.ToString(System.Net.Dns.GetHostByName(line.Substring(2).Substring(0, line.Substring(2).IndexOf(" ")).ToUpper()).AddressList(0).ToString())
                            .Properties.Items.Add(_pcName.ToString)
                        Catch ex As Exception
                        End Try
                    End If
                Loop Until (streamReader.EndOfStream)
                .SelectedIndex = 0
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Try
            _Proc = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            _Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
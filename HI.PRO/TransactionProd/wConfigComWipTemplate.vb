Imports System.IO

Imports System.Net
Imports System.Net.NetworkInformation



Public Class wConfigComWipTemplate

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        GetComName()

    End Sub

    Private Sub GetComName()
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


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            LoadData()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT     L.FTComputerName"

            _Cmd &= vbCrLf & ",(Select Top 1 FTUnitSectCode From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect   WITH(NOLOCK) WHERE  FNHSysUnitSectId = L.FNHSysUnitSectId) AS FTUnitSectCode"

            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigWIPTemplateCom AS L WITH (NOLOCK)"


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDetail.DataSource = _oDt


        Catch ex As Exception

        End Try
    End Sub

    Private Sub wConfigCom_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.LoadData()
        Catch ex As Exception
        End Try

    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigWIPTemplateCom "
            _Cmd &= vbCrLf & "Set FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & ",FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & "WHERE FTComputerName='" & HI.UL.ULF.rpQuoted(Me.FTComputerName.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigWIPTemplateCom (FTInsUser, FDInsDate, FTInsTime, FTComputerName, FNHSysUnitSectId)"
                _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTComputerName.Text) & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)


                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function VerifyData() As Boolean
        Try
            If Me.FTComputerName.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FTComputerName_lbl.Text)
                Me.FTComputerName.Focus()
                Return False
            End If
            If Me.FNHSysUnitSectId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysUnitSectId_lbl.Text)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerifyData() Then
                If SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.LoadData()
                    HI.TL.HandlerControl.ClearControl(Me)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.LoadData()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If VerifyData() Then
                If DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    LoadData()
                    HI.TL.HandlerControl.ClearControl(Me)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigWIPTemplateCom  Where FTComputerName='" & HI.UL.ULF.rpQuoted(Me.FTComputerName.Text) & "'"
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ogvDetail_Click(sender As Object, e As EventArgs) Handles ogvDetail.Click
        Try
            With Me.ogvDetail
                Me.FTComputerName.Text = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTComputerName").ToString)
                Me.FNHSysUnitSectId.Text = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTUnitSectCode").ToString)

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTComputerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FTComputerName.SelectedIndexChanged

    End Sub
End Class
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Imports HI.SE.RunID

Public Class ClsSendMail

    ' Public Shared Function SendMail(ByVal TempFrom As String, ByVal TempTo As String, ByVal TempSubject As String, ByVal TempMessage As String, ByVal TempTypeInfo As Integer, ByVal TempInfoRef As String) As Boolean
    ' Friend Function
    Public Shared Function SendMail(ByVal TempFrom As String, ByVal TempTo As String, ByVal TempSubject As String, ByVal TempMessage As String, ByVal TempTypeInfo As Integer, ByVal TempInfoRef As String) As Boolean
        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MAIL)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0
            '_FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            _FTMailId = GetMailRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)

            _Str = ""
            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
            _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType],[FNMailTypeInfo],[FTMailInfoRef])"
            _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempTo & "'"
            _Str &= ",'" & TempSubject & "','" & TempMessage & "',0,1,0,0,0,0,"
            _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0," & TempTypeInfo & ",'" & TempInfoRef & "')"

            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            For Each MailTo As String In TempTo.Split("|")

                If MailTo <> "" Then

                    ' กรณีส่งหาตัวเอง
                    If MailTo = HI.ST.UserInfo.UserName Then
                        '_FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                        _FTMailId = GetMailRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                        _Str = ""
                        _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                        _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                        _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                        _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                        _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailTypeInfo],[FTMailInfoRef])"
                        _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & MailTo & "'"
                        _Str &= ",'" & TempSubject & "','" & TempMessage & "' ,0,0,1,0,0,0,"
                        _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "'," & TempTypeInfo & ",'" & TempInfoRef & "')"

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    Else
                        ' _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                        _FTMailId = GetMailRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                        _Str = ""
                        _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                        _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                        _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                        _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                        _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType],[FNMailTypeInfo],[FTMailInfoRef])"
                        _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & MailTo & "'"
                        _Str &= ",'" & TempSubject & "','" & TempMessage & "',0,1,0,0,0,0,"
                        _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1," & TempTypeInfo & ",'" & TempInfoRef & "')"

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If
                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

End Class

Public NotInheritable Class RunID

    Private Shared RunMailLenght As Integer = 13
    Private Shared RunLenght As Integer = 9
    Private Shared RunFmt As String = " Right(replace(Convert(varchar(10),Getdate(),111),'/',''),5)   "

    Public Shared Function GetRunNoID(ByVal TableName As String, ByVal FieldName As String, ByVal DbName As HI.Conn.DB.DataBaseName) As Integer

        Dim _Qry As String = ""
        Dim RunNo As String = ""
        Dim _RunFmt As String = ""
        Dim IndChar As Integer = 0
        Dim CmpFmt As String = ""

        If HI.ST.SysInfo.CmpCode <> "" Then
            For Each C As Char In Microsoft.VisualBasic.Right(HI.ST.SysInfo.CmpCode, 2).ToCharArray
                IndChar = IndChar + 1

                 CmpFmt = CmpFmt & (Asc(C)).ToString

                If IndChar >= 2 Then
                    Exit For
                End If
            Next

            If CmpFmt <> "" And CmpFmt.Length = 4 Then
                CmpFmt = Right(CmpFmt, 3)
                _RunFmt = " Left(Right(replace(Convert(varchar(10),Getdate(),111),'/',''),6),2) +  Right('0000'+ Convert(varchar(4),(Convert(int," & CmpFmt & ") +  Convert(int,Right(replace(Convert(varchar(10),Getdate(),111),'/',''),4)))),3)   "
            End If

        Else
            _RunFmt = RunFmt
        End If

        _Qry = " SELECT  ISNULL(("
        _Qry &= vbCrLf & " SELECT TOP 1  Convert(varchar(" & RunLenght & ")," & FieldName & " +1)  AS FNRunNo "
        _Qry &= vbCrLf & " FROM  " & TableName & "   "
        _Qry &= vbCrLf & " WHERE  LEN(" & FieldName & ") =" & RunLenght & ""
        _Qry &= vbCrLf & " AND LEFT(" & FieldName & ",5)= " & _RunFmt
        _Qry &= vbCrLf & "  ORDER BY " & FieldName & "  DESC) ," & _RunFmt & " + '0001') AS FNRunNo"

        Try

            RunNo = HI.Conn.SQLConn.GetField(_Qry, DbName, "")

            If IsNumeric(RunNo) Then
                Return Integer.Parse(RunNo)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function GetRunNoIDCmp(ByVal TableName As String, ByVal FieldName As String, ByVal DbName As HI.Conn.DB.DataBaseName) As Long

        Dim _Str As String = ""
        Dim RunNo As String = ""
        Dim IndChar As Integer = 0
        Dim CmpFmt As String = ""

        If HI.ST.SysInfo.CmpCode <> "" Then
            For Each C As Char In Microsoft.VisualBasic.Right(HI.ST.SysInfo.CmpCode, 2).ToCharArray
                IndChar = IndChar + 1

                CmpFmt = CmpFmt & (Asc(C)).ToString

                If IndChar >= 2 Then
                    Exit For
                End If
            Next

        End If

        _Str = " SELECT  ISNULL(("
        _Str &= vbCrLf & " SELECT TOP 1  Convert(varchar(" & (CmpFmt.Length + RunLenght) & ")," & FieldName & " +1)  AS FNRunNo "
        _Str &= vbCrLf & " FROM  " & TableName & "  "
        _Str &= vbCrLf & " WHERE  LEN(" & FieldName & ") =" & (CmpFmt.Length + RunLenght) & ""
        _Str &= vbCrLf & " AND LEFT(" & FieldName & ",5 + " & CmpFmt.Length & ")= '" & CmpFmt & "'+" & RunFmt
        _Str &= vbCrLf & "  ORDER BY " & FieldName & "  DESC) ,'" & CmpFmt & "'+" & RunFmt & " + '0001') AS FNRunNo"

        Try
            RunNo = HI.Conn.SQLConn.GetField(_Str, DbName, "")

            If IsNumeric(RunNo) Then
                Return Long.Parse(RunNo)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Public Shared Function GetMailRunNoID(ByVal TableName As String, ByVal FieldName As String, ByVal DbName As HI.Conn.DB.DataBaseName) As Integer

    '    Dim _Qry As String = ""
    '    Dim RunNo As String = ""
    '    Dim _RunFmt As String = ""
    '    Dim IndChar As Integer = 0
    '    Dim CmpFmt As String = ""

    '    If HI.ST.SysInfo.CmpCode <> "" Then
    '        For Each C As Char In Microsoft.VisualBasic.Right(HI.ST.SysInfo.CmpCode, 2).ToCharArray

    '            IndChar = IndChar + 1
    '            CmpFmt = CmpFmt & (Asc(C)).ToString

    '            If IndChar >= 2 Then
    '                Exit For
    '            End If
    '        Next

    '        If CmpFmt <> "" And CmpFmt.Length = 4 Then
    '            _RunFmt = " Left(Right(replace(Convert(varchar(10),Getdate(),111),'/',''),6),2) +  Right('0000'+ Convert(varchar(4),(Convert(int," & CmpFmt & ") +  Convert(int,Right(replace(Convert(varchar(10),Getdate(),111),'/',''),4)))),3)   "
    '        End If

    '    Else
    '        _RunFmt = RunFmt
    '    End If

    '    _Qry = " SELECT  ISNULL(("
    '    _Qry &= vbCrLf & " SELECT TOP 1  Convert(varchar(" & RunMailLenght & ")," & FieldName & " +1)  AS FNRunNo "
    '    _Qry &= vbCrLf & " FROM  " & TableName & " WITH(NOLOCK) "
    '    _Qry &= vbCrLf & " WHERE  LEN(" & FieldName & ") =" & RunMailLenght & ""
    '    _Qry &= vbCrLf & " AND LEFT(" & FieldName & ",5)= " & _RunFmt
    '    _Qry &= vbCrLf & "  ORDER BY " & FieldName & "  DESC) ," & _RunFmt & " + '00000001') AS FNRunNo"

    '    Try

    '        RunNo = HI.Conn.SQLConn.GetField(_Qry, DbName, "")

    '        If IsNumeric(RunNo) Then
    '            Return Integer.Parse(RunNo)
    '        Else
    '            Return Nothing
    '        End If
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

    'Public Shared Function GetMailRunNoIDCmp(ByVal TableName As String, ByVal FieldName As String, ByVal DbName As HI.Conn.DB.DataBaseName) As Long

    '    Dim _Str As String = ""
    '    Dim RunNo As String = ""
    '    Dim IndChar As Integer = 0
    '    Dim CmpFmt As String = ""

    '    If HI.ST.SysInfo.CmpCode <> "" Then
    '        For Each C As Char In Microsoft.VisualBasic.Right(HI.ST.SysInfo.CmpCode, 2).ToCharArray
    '            IndChar = IndChar + 1

    '            CmpFmt = CmpFmt & (Asc(C)).ToString

    '            If IndChar >= 2 Then
    '                Exit For
    '            End If

    '        Next

    '    End If

    '    _Str = " SELECT  ISNULL(("
    '    _Str &= vbCrLf & " SELECT TOP 1  Convert(varchar(" & (CmpFmt.Length + RunMailLenght) & ")," & FieldName & " +1)  AS FNRunNo "
    '    _Str &= vbCrLf & " FROM  " & TableName & " WITH(NOLOCK)   "
    '    _Str &= vbCrLf & " WHERE  LEN(" & FieldName & ") =" & (CmpFmt.Length + RunMailLenght) & ""
    '    _Str &= vbCrLf & " AND LEFT(" & FieldName & ",5 + " & CmpFmt.Length & ")= '" & CmpFmt & "'+" & RunFmt
    '    _Str &= vbCrLf & "  ORDER BY " & FieldName & "  DESC) ,'" & CmpFmt & "'+" & RunFmt & " + '00000001') AS FNRunNo"

    '    Try

    '        RunNo = HI.Conn.SQLConn.GetField(_Str, DbName, "")

    '        If IsNumeric(RunNo) Then
    '            Return Long.Parse(RunNo)
    '        Else
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function


    Public Shared Function GetMailRunNoID(ByVal TableName As String, ByVal FieldName As String, ByVal DbName As HI.Conn.DB.DataBaseName) As Integer

        Dim _Qry As String = ""
        Dim RunNo As String = ""
        Dim _RunFmt As String = ""
        Dim IndChar As Integer = 0
        Dim CmpFmt As String = ""

        If HI.ST.SysInfo.CmpCode <> "" Then
            For Each C As Char In Microsoft.VisualBasic.Right(HI.ST.SysInfo.CmpCode, 2).ToCharArray

                IndChar = IndChar + 1
                CmpFmt = CmpFmt & (Asc(C)).ToString

                If IndChar >= 2 Then
                    Exit For
                End If
            Next

            If CmpFmt <> "" And CmpFmt.Length = 4 Then
                _RunFmt = " Left(Right(replace(Convert(varchar(10),Getdate(),111),'/',''),6),2) +  Right('0000'+ Convert(varchar(4),(Convert(int," & CmpFmt & ") +  Convert(int,Right(replace(Convert(varchar(10),Getdate(),111),'/',''),4)))),3)   "
            End If

        Else
            _RunFmt = RunFmt
        End If

        _Qry = " SELECT  ISNULL(("
        _Qry &= vbCrLf & " SELECT TOP 1  Convert(varchar(" & (CmpFmt.Length + RunMailLenght) & ")," & FieldName & " +1)  AS FNRunNo "
        _Qry &= vbCrLf & " FROM  " & TableName & " WITH(NOLOCK)   "
        _Qry &= vbCrLf & " WHERE  LEN(" & FieldName & ") =" & (CmpFmt.Length + RunMailLenght) & ""
        _Qry &= vbCrLf & " AND  FTMailIdIndex= '" & CmpFmt & "'"
        _Qry &= vbCrLf & "  AND FDInsDate  = Convert(varchar(10),Getdate(),111)   "
        _Qry &= vbCrLf & "  ORDER BY " & FieldName & "  DESC) ,'" & CmpFmt & "'+" & RunFmt & " + '00000001') AS FNRunNo"


        Try

            RunNo = HI.Conn.SQLConn.GetField(_Qry, DbName, "")

            If IsNumeric(RunNo) Then
                Return Integer.Parse(RunNo)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function GetMailRunNoIDCmp(ByVal TableName As String, ByVal FieldName As String, ByVal DbName As HI.Conn.DB.DataBaseName) As Long

        Dim _Str As String = ""
        Dim RunNo As String = ""
        Dim IndChar As Integer = 0
        Dim CmpFmt As String = ""

        If HI.ST.SysInfo.CmpCode <> "" Then
            For Each C As Char In Microsoft.VisualBasic.Right(HI.ST.SysInfo.CmpCode, 2).ToCharArray
                IndChar = IndChar + 1

                CmpFmt = CmpFmt & (Asc(C)).ToString

                If IndChar >= 2 Then
                    Exit For
                End If

            Next

        End If

        _Str = " SELECT  ISNULL(("
        _Str &= vbCrLf & " SELECT TOP 1  Convert(varchar(" & (CmpFmt.Length + RunMailLenght) & ")," & FieldName & " +1)  AS FNRunNo "
        _Str &= vbCrLf & " FROM  " & TableName & " WITH(NOLOCK)   "
        _Str &= vbCrLf & " WHERE  LEN(" & FieldName & ") =" & (CmpFmt.Length + RunMailLenght) & ""
        _Str &= vbCrLf & " AND  FTMailIdIndex= '" & CmpFmt & "'"
        _Str &= vbCrLf & "  AND FDInsDate  = Convert(varchar(10),Getdate(),111)   "
        _Str &= vbCrLf & "  ORDER BY " & FieldName & "  DESC) ,'" & CmpFmt & "'+" & RunFmt & " + '00000001') AS FNRunNo"


        Try

            RunNo = HI.Conn.SQLConn.GetField(_Str, DbName, "")

            If IsNumeric(RunNo) Then
                Return Long.Parse(RunNo)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


End Class

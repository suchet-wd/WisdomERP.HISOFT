Public NotInheritable Class Document

    Public Shared Function GetDocumentNo(ByVal _DBName As String, ByVal _TblName As String, ByVal _DocType As String, Optional _GetFotmat As Boolean = False, Optional AddPrefix As String = "", Optional DocumentDate As String = "") As String

        Dim _Qrysql As String
        _Qrysql = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GEN_DOCUMENTNO '" & _DBName & "','" & _TblName & "','" & _DocType & "','" & IIf(_GetFotmat, "Y", "") & "','" & AddPrefix & "','" & HI.UL.ULDate.ConvertEnDB(DocumentDate) & "'"
        Return HI.Conn.SQLConn.GetField(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM, "")

    End Function

    Public Shared Function GetDocumentNoOnBeginTrans(ByVal _DBName As String, ByVal _TblName As String, ByVal _DocType As String, Optional _GetFotmat As Boolean = False, Optional AddPrefix As String = "", Optional DocumentDate As String = "") As String

        Dim _Qrysql As String
        _Qrysql = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GEN_DOCUMENTNO '" & _DBName & "','" & _TblName & "','" & _DocType & "','" & IIf(_GetFotmat, "Y", "") & "','" & AddPrefix & "','" & HI.UL.ULDate.ConvertEnDB(DocumentDate) & "'"
        Return HI.Conn.SQLConn.GetFieldByNameOnBeginTrans(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM, "")

    End Function

End Class

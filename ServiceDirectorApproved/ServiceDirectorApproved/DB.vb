Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType
Imports System.Windows.Forms

Public NotInheritable Class DB

    Public Shared _ConnectString As String
    Public Shared _AppName As String
    Private Shared _ServerName(15) As String
    Private Shared _DBName(15) As String
    Private Shared _UserName(15) As String
    Private Shared _PasswordName(15) As String
    Private Shared _SystemDBName() As String = {"DB_TEMPDB", "DB_SECURITY", "DB_HR", "DB_SYSTEM", "DB_MASTER", "DB_MER", "DB_PUR", "DB_INVEN", "DB_PROD", "DB_ACCOUNT", "DB_LANG", "DB_LOG", "DB_MAIL"}

    Public Enum DataBaseName As Integer

        DB_TEMPDB = 0
        DB_SECURITY = 1
        DB_HR = 2
        DB_SYSTEM = 3
        DB_MASTER = 4
        DB_MERCHAN = 5
        DB_PUR = 6
        DB_INVEN = 7
        DB_PROD = 8
        DB_ACCOUNT = 9
        DB_LANG = 10
        DB_LOG = 11
        DB_MAIL = 12
    End Enum

    Public Overloads Shared Function GetDataBaseName(ByVal DbName As DataBaseName) As String
        Try
            Return _DBName(DbName)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Overloads Shared Function GetDataBaseName(ByVal DbName As String) As String
        Try
            Dim I As Integer = 0
            For Each StrDBName As String In _DBName
                If UCase(StrDBName) Like UCase("*" & DbName & "*") Then

                    Return _DBName(I)

                End If
                I = I + 1
            Next
            Return False
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Overloads Shared Function UsedDB(ByVal DbName As DataBaseName) As Boolean
        Try
            SerVerName = _ServerName(DbName)
            UIDName = _UserName(DbName)
            PWDName = _PasswordName(DbName)
            BaseName = _DBName(DbName)
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    Public Overloads Shared Function UsedDB(ByVal DbName As String) As Boolean
        Try
            Dim I As Integer = 0
            For Each StrDBName As String In _DBName
                If UCase(StrDBName) Like UCase("*" & DbName & "*") Then
                    SerVerName = _ServerName(I)
                    UIDName = _UserName(I)
                    PWDName = _PasswordName(I)
                    BaseName = _DBName(I)
                    Return True
                    Exit For
                End If
                I = I + 1
            Next
            Return False
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Shared _SVName As String = ""
    Public Shared Property SerVerName() As String
        Get
            Return _SVName
        End Get
        Set(value As String)
            _SVName = value
        End Set
    End Property

    Private Shared _UIDName As String = ""
    Public Shared Property UIDName() As String
        Get
            Return _UIDName
        End Get
        Set(value As String)
            _UIDName = value
        End Set
    End Property

    Private Shared _PWDName As String = ""
    Public Shared Property PWDName() As String
        Get
            Return _PWDName
        End Get
        Set(value As String)
            _PWDName = value
        End Set
    End Property

    Private Shared _BaseName As String = ""
    Public Shared Property BaseName() As String
        Get
            Return _BaseName
        End Get
        Set(value As String)
            _BaseName = value
        End Set
    End Property


    Private Shared _PathFileDLL As String = ""
    Public Shared Property PathFileDLL As String
        Get
            Return _PathFileDLL
        End Get
        Set(value As String)
            _PathFileDLL = value
        End Set
    End Property

    Public Shared Function ConnectionString(ByVal DbName As DataBaseName) As String
        Try
            _ConnectString = "SERVER=" & _ServerName(DbName) & ";UID=" & _UserName(DbName) & ";PWD=" & _PasswordName(DbName) & ";Initial Catalog=" & _DBName(DbName)
            Return _ConnectString
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Sub GetXmlConnectionString()
        Try
            Dim DS As New DataSet
            Dim i As Integer

            Dim _Dt As DataTable
            DS.ReadXml(Application.StartupPath & "\Database.xml")
            Dim dbServerName, dbName, dbUserName, dbPassword As String

  
            _AppName = "Hi Soft System"
            If DS.Tables.IndexOf("APPLICATION") <> -1 Then
                _AppName = DS.Tables("APPLICATION").Rows(0).Item("Name").ToString
            End If

            i = 0
            _Dt = DS.Tables("SQLSERVER").Copy
            For Each StrDBName As String In _SystemDBName

                dbServerName = ""
                dbName = ""
                dbUserName = ""
                dbPassword = ""

                For Each Row As DataRow In _Dt.Select("name='" & StrDBName & "' ")
                    dbServerName = Row!server.ToString
                    dbName = Row!dbname.ToString
                    dbUserName = Row!username.ToString
                    dbPassword = FuncDecryptData(Row!password.ToString)
                Next

                _ServerName(i) = dbServerName
                _DBName(i) = dbName
                _UserName(i) = dbUserName
                _PasswordName(i) = dbPassword

                i = i + 1
            Next

            DS.Dispose()
            DS = Nothing

        Catch ex As Exception
        End Try
    End Sub


#Region " Function Decrypt FuncEncryptData"

    Public Shared Function FuncDecryptData(ByVal DecryTxt As String, Optional FirtsTime As Boolean = True) As String

        Dim txtDecry As String = ""
        Try
            Dim Buff1 As Char
            Dim Buff2 As Char
            Dim TxtBuff1 As String = ""
            Dim TxtBuff2 As String = ""
            Dim i As Integer
            Dim DecryCode As Integer

            If Trim(DecryTxt) <> "" Then

                DecryCode = Asc(Right(DecryTxt, 1)) - Asc(Mid(DecryTxt, 2, 1))

                For i = Len(DecryTxt) - 1 To 1 Step -1
                    TxtBuff1 &= Mid(DecryTxt, i, 1)
                Next i

                For i = 1 To Len(TxtBuff1)
                    Buff1 = Mid(TxtBuff1, i, 1)
                    Buff2 = Nothing
                    Buff2 = Chr(Asc(Buff1) - DecryCode)
                    TxtBuff2 &= Buff2
                Next i
                txtDecry = TxtBuff2
            End If

            If (FirtsTime) Then

                If txtDecry.Length > 1 Then
                    txtDecry = FuncDecryptData(Right(txtDecry, txtDecry.Length - 1), False)
                Else
                    txtDecry = FuncDecryptData(txtDecry, False)
                End If

            End If

        Catch ex As Exception
        End Try
        Return txtDecry
    End Function

    Public Shared Function FuncEncryptData(ByVal EncryTxt As String, Optional FirtsTime As Boolean = True) As String
        Dim txtEncry As String = ""
        Try
            Dim EncryCode As Integer
            Dim Buff1 As Char
            Dim Buff2 As Char
            Dim TxtBuff1 As String = ""
            Dim TxtBuff2 As String = ""
            Dim i As Integer
            Randomize()
            EncryCode = Conversion.Int((9 * Rnd()) + 1)

            For i = 1 To Len(EncryTxt)

                Buff1 = Mid(EncryTxt, i, 1)
                Buff2 = Nothing
                Buff2 = Chr(Asc(Buff1) + EncryCode)
                TxtBuff1 &= Buff2

            Next i

            For i = Len(TxtBuff1) To 1 Step -1
                TxtBuff2 &= Mid(TxtBuff1, i, 1)
            Next i

            EncryCode = Asc(Mid(TxtBuff2, 2, 1)) + EncryCode
            txtEncry = TxtBuff2 & Chr(EncryCode)

            If (FirtsTime) Then
                If txtEncry.Length > 1 Then
                    txtEncry = FuncEncryptData("H" & txtEncry, False)
                Else
                    txtEncry = FuncEncryptData(txtEncry, False)
                End If

            End If

        Catch ex As Exception
        End Try
        Return txtEncry
    End Function

#End Region

End Class



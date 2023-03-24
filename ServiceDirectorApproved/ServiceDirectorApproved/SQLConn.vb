Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType
Imports System.Windows.Forms

Public NotInheritable Class SQLConn

    Public Shared Cmd As System.Data.SqlClient.SqlCommand
    Public Shared Cnn As System.Data.SqlClient.SqlConnection
    Public Shared Tran As System.Data.SqlClient.SqlTransaction
    Public Shared Cmd2 As System.Data.SqlClient.SqlCommand
    Public Shared Cnn2 As System.Data.SqlClient.SqlConnection
    Public Shared Tran2 As System.Data.SqlClient.SqlTransaction
    Public Shared _ConnString As String

#Region " CONNECT "

    Public Shared Sub SqlConnectionOpen()
        If SQLConn.Cnn Is Nothing Then SQLConn.Cnn = New System.Data.SqlClient.SqlConnection
        With SQLConn.Cnn
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .ConnectionString = _ConnString
            .Open()
        End With
    End Sub

    Public Shared Sub SqlBeginTransaction()
        If SQLConn.Cnn Is Nothing Then SQLConn.Cnn = New System.Data.SqlClient.SqlConnection
        With SQLConn.Cnn
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .ConnectionString = _ConnString
            .Open()
            SQLConn.Tran = .BeginTransaction
        End With
    End Sub

    Public Shared Function SqlConnectionOpen(ByVal cnn As System.Data.SqlClient.SqlConnection) As System.Data.SqlClient.SqlConnection
        If cnn Is Nothing Then cnn = New System.Data.SqlClient.SqlConnection
        With cnn
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .ConnectionString = ""
            .Open()
        End With
        Return cnn
    End Function

    Public Shared Function SqlBeginTransaction(ByVal cnn As System.Data.SqlClient.SqlConnection) As System.Data.SqlClient.SqlConnection
        If cnn Is Nothing Then cnn = New System.Data.SqlClient.SqlConnection
        With cnn
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .ConnectionString = ""
            .Open()
            SQLConn.Tran = .BeginTransaction
        End With
        Return cnn
    End Function

    Public Shared Function SqlBeginTransaction(ByVal cnn As System.Data.SqlClient.SqlConnection, ByVal tran As System.Data.SqlClient.SqlTransaction) As System.Data.SqlClient.SqlConnection
        If cnn Is Nothing Then cnn = New System.Data.SqlClient.SqlConnection
        With cnn
            If .State = ConnectionState.Open Then
                .Close()
            End If
            .ConnectionString = ""
            .Open()
            tran = .BeginTransaction
        End With
        Return cnn
    End Function

    Public Shared Sub DisposeSqlConnection(ByVal cnn As System.Data.SqlClient.SqlConnection)
        If Not cnn Is Nothing Then
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            cnn.Dispose()
        End If
    End Sub

    Public Shared Sub DisposeSqlConnection(ByVal cmd As System.Data.SqlClient.SqlCommand)
        If Not cmd Is Nothing Then
            If Not cmd.Connection Is Nothing Then
                If cmd.Connection.State = ConnectionState.Open Then
                    cmd.Connection.Close()
                End If
                cmd.Connection.Dispose()
            End If
            cmd.Dispose()
        End If
    End Sub

    Public Shared Sub DisposeSqlConnection(ByVal adapter As System.Data.SqlClient.SqlDataAdapter)
        If Not adapter Is Nothing Then
            If Not adapter.SelectCommand Is Nothing Then
                If Not adapter.SelectCommand.Connection Is Nothing Then
                    If Not adapter.SelectCommand.Connection.State = ConnectionState.Open Then
                        adapter.SelectCommand.Connection.Close()
                    End If
                    adapter.SelectCommand.Connection.Dispose()
                End If
                adapter.SelectCommand.Dispose()
            End If
            adapter.Dispose()
        End If
    End Sub

    Public Shared Sub DisposeSqlTransaction(ByVal tran As System.Data.SqlClient.SqlTransaction)
        If Not tran Is Nothing Then
            If Not tran.Connection Is Nothing Then
                If tran.Connection.State = ConnectionState.Open Then
                    tran.Connection.Close()
                End If
                tran.Connection.Dispose()
            End If
            tran.Dispose()
        End If
    End Sub

    Public Shared Sub ClearParameterObject(ByVal cmd As System.Data.SqlClient.SqlCommand)
        If cmd.Parameters.Count > 0 Then
            cmd.Parameters.Clear()
        End If
    End Sub
#End Region

#Region "    TRANSACTION    "

    Public Overloads Shared Function Execute_Tran(ByVal QryStr() As String, ByVal DbName As DB.DataBaseName) As Boolean
        Try
            Dim Complete As Integer = 0

            _ConnString = DB.ConnectionString(DbName)

            SQLConn.SqlConnectionOpen()
            SQLConn.Cmd = SQLConn.Cnn.CreateCommand
            SQLConn.Tran = SQLConn.Cnn.BeginTransaction

            For Each Str As String In QryStr

                With SQLConn.Cmd
                    .CommandType = CommandType.Text
                    .CommandText = Str
                    .Transaction = SQLConn.Tran
                    Complete = .ExecuteNonQuery
                    .Parameters.Clear()
                End With

                If Complete <= 0 Then
                    SQLConn.Tran.Rollback()
                    SQLConn.DisposeSqlTransaction(SQLConn.Tran)
                    SQLConn.DisposeSqlConnection(SQLConn.Cmd)
                    Return False
                End If

            Next

            SQLConn.Tran.Commit()
            SQLConn.DisposeSqlTransaction(SQLConn.Tran)
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return True

        Catch ex As Exception
            SQLConn.Tran.Rollback()
            SQLConn.DisposeSqlTransaction(SQLConn.Tran)
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return False
        End Try
    End Function

    Public Overloads Shared Function Execute_Tran(ByVal sqlStr As String, ByVal sqlcmd As SqlClient.SqlCommand, ByVal Tr As SqlClient.SqlTransaction) As Integer
        Try
            Dim Complete As Integer = 0

            With sqlcmd
                .CommandType = CommandType.Text
                .CommandText = sqlStr
                .Transaction = Tr
                Complete = .ExecuteNonQuery
                .Parameters.Clear()
            End With

            Return Complete

        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function

    Public Overloads Shared Function ExecuteTran(ByVal sqlStr As String, ByVal sqlcmd As SqlClient.SqlCommand, ByVal Tr As SqlClient.SqlTransaction) As Integer
        Dim Complete As Integer = 0
        Try
            With sqlcmd
                .CommandType = CommandType.Text
                .CommandText = sqlStr
                .Transaction = Tr
                Complete = .ExecuteNonQuery
                .Parameters.Clear()
            End With

            Return Complete

        Catch ex As Exception
            Return Complete
        End Try
    End Function

#End Region

#Region "NonTransection"

    Public Overloads Shared Function ExecuteOnly(ByVal QryStr As String, ByVal DbName As DB.DataBaseName) As Boolean
        Try
            Dim Complete As Integer = 0

            _ConnString = DB.ConnectionString(DbName)
            SQLConn.SqlConnectionOpen()
            SQLConn.Cmd = SQLConn.Cnn.CreateCommand

            With SQLConn.Cmd
                .CommandType = CommandType.Text
                .CommandText = QryStr
                .ExecuteNonQuery()
                .Parameters.Clear()
            End With

            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return True

        Catch ex As Exception

            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            MsgBox(ex.Message())
            Return False
        End Try
    End Function

    Public Overloads Shared Function ExecuteOnly(ByRef objCmd As SqlCommand, ByVal DbName As DB.DataBaseName) As Boolean
        Try
            Dim Complete As Integer = 0

            _ConnString = DB.ConnectionString(DbName)
            SQLConn.SqlConnectionOpen()

            With objCmd
                .Connection = SQLConn.Cnn
                .CommandTimeout = 5000
                .ExecuteNonQuery()
                .Parameters.Clear()
            End With

            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return True

        Catch ex As Exception
            MsgBox(ex.Message())
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return False
        End Try
    End Function

    Public Overloads Shared Function ExecuteNonQuery(ByVal QryStr As String, ByVal DbName As DB.DataBaseName) As Boolean
        Try
            Dim Complete As Integer = 0

            _ConnString = DB.ConnectionString(DbName)
            SQLConn.SqlConnectionOpen()
            SQLConn.Cmd = SQLConn.Cnn.CreateCommand
            SQLConn.Tran = SQLConn.Cnn.BeginTransaction

            With SQLConn.Cmd
                .CommandType = CommandType.Text
                .CommandText = QryStr
                .Transaction = SQLConn.Tran
                Complete = .ExecuteNonQuery
                .Parameters.Clear()
            End With

            If Complete <= 0 Then
                SQLConn.Tran.Rollback()
                SQLConn.DisposeSqlTransaction(SQLConn.Tran)
                SQLConn.DisposeSqlConnection(SQLConn.Cmd)
                Return False
            End If

            SQLConn.Tran.Commit()
            SQLConn.DisposeSqlTransaction(SQLConn.Tran)
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return True

        Catch ex As Exception
            SQLConn.Tran.Rollback()
            SQLConn.DisposeSqlTransaction(SQLConn.Tran)
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return False
        End Try

    End Function

    Public Overloads Shared Function ExecuteNonQuery(ByRef objCmd As SqlCommand, ByVal DbName As DB.DataBaseName) As Boolean
        Try
            Dim Complete As Integer = 0

            _ConnString = DB.ConnectionString(DbName)
            SQLConn.SqlConnectionOpen()
            SQLConn.Tran = SQLConn.Cnn.BeginTransaction

            With objCmd
                .Connection = SQLConn.Cnn
                .CommandTimeout = 5000
                .Transaction = SQLConn.Tran
                Complete = .ExecuteNonQuery
                .Parameters.Clear()
            End With

            If Complete <= 0 Then
                SQLConn.Tran.Rollback()
                SQLConn.DisposeSqlTransaction(SQLConn.Tran)
                SQLConn.DisposeSqlConnection(SQLConn.Cmd)
                Return False
            End If

            SQLConn.Tran.Commit()
            SQLConn.DisposeSqlTransaction(SQLConn.Tran)
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            Return True

        Catch ex As Exception
            SQLConn.Tran.Rollback()
            SQLConn.DisposeSqlTransaction(SQLConn.Tran)
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
            MsgBox(ex.Message())
            Return False
        End Try
    End Function

    Public Shared Function ExecuteScalar(ByVal QryStr As String, ByVal DbName As DB.DataBaseName) As Object
        Try

            _ConnString = DB.ConnectionString(DbName)
            SQLConn.SqlConnectionOpen()
            SQLConn.Cmd = SQLConn.Cnn.CreateCommand

            With SQLConn.Cmd
                .CommandType = CommandType.Text
                .CommandText = QryStr
                Return .ExecuteScalar
            End With

        Catch ex As SqlException
            Return Nothing
        Finally
            SQLConn.DisposeSqlConnection(SQLConn.Cmd)
        End Try

    End Function

    Public Shared Function ExecuteStoredProcedure(ByVal tStoreName As String, ByVal DbName As DB.DataBaseName, Optional ByVal prms As SqlParameterCollection = Nothing) As Integer
        Try
            Dim _Complete As Integer = 0
            _ConnString = DB.ConnectionString(DbName)
            SQLConn.SqlConnectionOpen()

            Dim oCmd As New SqlClient.SqlCommand
            oCmd.Connection = SQLConn.Cnn
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.CommandText = tStoreName
            oCmd.Parameters.Clear()

            If Not prms Is Nothing Then
                For Each prm As SqlParameter In prms
                    oCmd.Parameters.AddWithValue(prm.ParameterName, prm.SqlDbType).Value = prm.Value
                Next
            End If

            _Complete = oCmd.ExecuteNonQuery

            SQLConn.DisposeSqlConnection(SQLConn.Cnn)

            Return _Complete
        Catch ex As Exception
            SQLConn.DisposeSqlConnection(SQLConn.Cnn)
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function

    Public Shared Function ExecuteScalar(ByRef objCmd As SqlCommand, ByVal DbName As DB.DataBaseName) As Object
        Try

            _ConnString = DB.ConnectionString(DbName)
            SQLConn.SqlConnectionOpen()

            With objCmd
                .Connection = SQLConn.Cnn
                Return .ExecuteScalar
            End With

        Catch ex As SqlException
            Return Nothing
        Finally
            SQLConn.DisposeSqlConnection(objCmd)
        End Try

    End Function
#End Region

#Region " GETDATA  "

    Public Shared Function GetDataTable(ByVal QryStr As String, ByVal DbName As DB.DataBaseName, Optional ByVal TableName As String = "DataTalble1") As DataTable
        Dim objDT As New DataTable(TableName)

        Dim _Cnn As New SqlClient.SqlConnection
        Dim _Cmd As New SqlClient.SqlCommand
        Try
            _ConnString = DB.ConnectionString(DbName)
            With _Cnn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = SQLConn._ConnString
                .Open()
                _Cmd = .CreateCommand
            End With

            With New SqlDataAdapter(_Cmd)
                .SelectCommand.CommandTimeout = 10000
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = QryStr
                .Fill(objDT)
                .Dispose()
            End With

            DisposeSqlConnection(_Cmd)
            DisposeSqlConnection(_Cnn)
        Catch ex As Exception
            DisposeSqlConnection(_Cmd)
            DisposeSqlConnection(_Cnn)
            ' MsgBox(ex.Message & vbCrLf & QryStr)
        End Try
        Return objDT
    End Function

    Public Overloads Shared Function GetDataTableOnbeginTrans(ByVal QryStr As String, Optional ByVal TableName As String = "DataTalble1") As DataTable

        Dim objDT As New DataTable(TableName)
        Dim _cmd As SqlCommand = Nothing

        Try

            If SQLConn.Tran IsNot Nothing Then
                _cmd = New SqlCommand(QryStr, SQLConn.Cnn, SQLConn.Tran)
            Else
                _cmd = New SqlCommand(QryStr, SQLConn.Cnn)
            End If

            With New SqlDataAdapter(_cmd)
                .SelectCommand.CommandTimeout = 0
                .Fill(objDT)
                .Dispose()
            End With

            _cmd.Dispose()

        Catch ex As Exception
            _cmd.Dispose()
        End Try

        Return objDT

    End Function

    Public Shared Sub GetDataSet(ByVal QryStr As String, ByVal DbName As DB.DataBaseName, ByRef objDataSet As DataSet, Optional ByVal TableName As String = Nothing)
        Try
            Dim objDA As New SqlDataAdapter(QryStr, DB.ConnectionString(DbName))
            If TableName Is Nothing Then
                objDA.Fill(objDataSet)
            Else
                objDA.Fill(objDataSet, TableName)
            End If

        Catch ex As Exception
        End Try
    End Sub


    Public Overloads Shared Function GetField(ByVal strSql As String, ByVal DbName As DB.DataBaseName, Optional ByVal defaultValue As Object = "") As String
        Dim dt As New DataTable
        Dim _Value As String = defaultValue.ToString

        Try

            dt = GetDataTable(strSql, DbName)

            If dt.Rows.Count <> 0 Then

                For Each R As DataRow In dt.Rows
                    If IsDBNull(R.Item(0)) Then
                        _Value = defaultValue.ToString
                    Else
                        _Value = R.Item(0).ToString
                    End If
                    Exit For
                Next

            Else
                _Value = defaultValue.ToString
            End If

        Catch ex As Exception
        End Try

        dt.Dispose()
        Return _Value
    End Function

    Public Overloads Shared Function GetFieldByName(ByVal strSql As String, ByVal DbName As DB.DataBaseName, FieldName As String, Optional ByVal defaultValue As Object = "") As String
        Dim dt As New DataTable
        Dim _Value As String = defaultValue.ToString

        Try

            dt = GetDataTable(strSql, DbName)

            If dt.Rows.Count <> 0 Then
                For Each R As DataRow In dt.Rows

                    If FieldName <> "" And dt.Columns.IndexOf(FieldName) >= 0 Then
                        If IsDBNull(R.Item(FieldName)) Then
                            _Value = defaultValue.ToString
                        Else

                            _Value = R.Item(FieldName).ToString
                        End If
                    Else
                        If IsDBNull(R.Item(0)) Then
                            _Value = defaultValue.ToString
                        Else
                            _Value = R.Item(0).ToString
                        End If
                    End If
                    Exit For
                Next

            Else
                _Value = defaultValue.ToString
            End If

        Catch ex As Exception
        End Try

        dt.Dispose()
        Return _Value
    End Function

    Public Overloads Shared Function GetFieldOnBeginTrans(ByVal strSql As String, ByVal DbName As DB.DataBaseName, Optional ByVal defaultValue As Object = "") As String
        Dim dt As New DataTable
        Dim _Value As String = defaultValue.ToString
        Try

            dt = GetDataTableOnbeginTrans(strSql, DbName)

            If dt.Rows.Count <> 0 Then
                For Each R As DataRow In dt.Rows

                    If IsDBNull(R.Item(0)) Then
                        _Value = defaultValue.ToString
                    Else
                        _Value = R.Item(0).ToString
                    End If
                    Exit For
                Next
            Else
                _Value = defaultValue.ToString
            End If

        Catch ex As Exception
        End Try

        dt.Dispose()
        Return _Value

    End Function

    Public Overloads Shared Function GetFieldByNameOnBeginTrans(ByVal strSql As String, ByVal DbName As DB.DataBaseName, FieldName As String, Optional ByVal defaultValue As Object = "") As String
        Dim dt As New DataTable
        Dim _Value As String = defaultValue.ToString
        Try

            dt = GetDataTableOnbeginTrans(strSql, DbName)

            If dt.Rows.Count <> 0 Then
                For Each R As DataRow In dt.Rows
                    If FieldName <> "" And dt.Columns.IndexOf(FieldName) >= 0 Then
                        If IsDBNull(R.Item(FieldName)) Then
                            _Value = defaultValue.ToString
                        Else
                            _Value = R.Item(FieldName).ToString
                        End If
                    Else
                        If IsDBNull(R.Item(0)) Then
                            _Value = defaultValue.ToString
                        Else
                            _Value = R.Item(0).ToString
                        End If
                    End If
                    Exit For
                Next

            Else
                _Value = defaultValue.ToString
            End If

        Catch ex As Exception
        End Try

        dt.Dispose()
        Return _Value

    End Function

#End Region

End Class

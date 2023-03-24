
Public NotInheritable Class HSysField

    Public Shared Function GetSysSubQuery(FilesName As String) As String
        Dim _Str As String = ""

        _Str = "SELECT TOP 1 'SELECT TOP 1  ' + FTColumnKeyCode  + ' FROM  [' +FTDBName+ '].' +FTPrefix +'.' + FTTableName + ' WITH ( NOLOCK ) WHERE ' +FTColumnName + '=M.' + '" & FilesName & "'"
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePK AS A WITH (NOLOCK)  "
        _Str &= vbCrLf & "  WHERE (FTColumnName IN (SELECT TOP 1 FTColumnName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePKRef AS B WITH(NOLOCK) WHERE  FTColumnNameRef= '" & HI.UL.ULF.rpQuoted(FilesName) & "'))"
        _Str &= vbCrLf & "    AND (FTColumnKeyCode <> '') AND ISNULL( (SELECt SUM(FNSeq) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePK AS C WITH (NOLOCK)  "
        _Str &= vbCrLf & "    WHERE (FTDBName = A.FTDBName)"
        _Str &= vbCrLf & "         AND FTPrefix = A.FTPrefix "
        _Str &= vbCrLf & "         AND FTTableName = A.FTTableName "
        _Str &= vbCrLf & "    ),0) =1"

        Return HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "")
    End Function

    Public Shared Function GetSysSubQueryDesc(FilesName As String) As String
        Dim _Str As String = ""

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Str = "SELECT TOP 1 'SELECT TOP 1  ' + FTColumnTH  + ' FROM  [' +FTDBName+ '].' +FTPrefix +'.' + FTTableName + ' WITH ( NOLOCK ) WHERE ' +FTColumnName + '=M.' + '" & FilesName & "'"

        Else
            _Str = "SELECT TOP 1 'SELECT TOP 1  ' + FTColumnEN  + ' FROM  [' +FTDBName+ '].' +FTPrefix +'.' + FTTableName + ' WITH ( NOLOCK ) WHERE ' +FTColumnName + '=M.' + '" & FilesName & "'"

        End If

        _Str &= vbCrLf & "  FROM  HSysTTablePK AS M WITH (NOLOCK)  "
        _Str &= vbCrLf & "  WHERE (FTColumnName IN (SELECT TOP 1 FTColumnName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePKRef WITH(NOLOCK) WHERE  FTColumnNameRef= '" & HI.UL.ULF.rpQuoted(FilesName) & "'))"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Str &= vbCrLf & "  AND (ISNULL(FTColumnTH,'') <> '') "
        Else
            _Str &= vbCrLf & "  AND (ISNULL(FTColumnEN,'') <> '') "
        End If

        _Str &= vbCrLf & "  AND ISNULL( (SELECt SUM(FNSeq) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePK AS A WITH (NOLOCK)  "
        _Str &= vbCrLf & "   WHERE (FTDBName = M.FTDBName)"
        _Str &= vbCrLf & "    AND FTPrefix = M.FTPrefix "
        _Str &= vbCrLf & "    AND FTTableName = M.FTTableName "
        _Str &= vbCrLf & "    ),0) =1"

        Return HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "")
    End Function

    Public Shared Function GetSysSubQueryCombolist(ByVal FilesName As String, ByVal ListName As String) As String
        Dim _Str As String = ""

        _Str = " SELECT TOP 1  " & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, "FTNameTH", "FTNameEN") & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData  AS L WITH (NOLOCK) WHERE  FTListName = N'" & HI.UL.ULF.rpQuoted(ListName) & "'  AND FNListIndex=M." & FilesName & " "
        Return _Str
    End Function

    
End Class

Public Class PKFiled
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property

    Private _FiledValue As String
    Public Property FiledValue() As String
        Get
            Return _FiledValue
        End Get
        Set(ByVal value As String)
            _FiledValue = value
        End Set
    End Property

End Class

Public Class LockEditField
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property

    Private _FiledValue As String
    Public Property FiledValue() As String
        Get
            Return _FiledValue
        End Get
        Set(ByVal value As String)
            _FiledValue = value
        End Set
    End Property

End Class

Public Class TableField
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property
End Class

Public Class ReadOnlyField
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property
End Class

Public Class CheckFiled
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property
End Class

Public Class DataBaseFiled
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property

    Private _ControlType As String
    Public Property ControlType() As String
        Get
            Return _ControlType
        End Get
        Set(ByVal value As String)
            _ControlType = value
        End Set
    End Property
End Class

Public Class DuplFiled
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property
End Class

Public Class CheckDelFiled
    Private _Query As String
    Public Property Query() As String
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
        End Set
    End Property
End Class

Public Class DefaultsData
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property

    Private _DataDefaults As String
    Public Property DataDefaults() As String
        Get
            Return _DataDefaults
        End Get
        Set(ByVal value As String)
            _DataDefaults = value
        End Set
    End Property

    Private _QueryDefaults As Boolean = False
    Public Property QueryDefaults() As Boolean
        Get
            Return _QueryDefaults
        End Get
        Set(ByVal value As Boolean)
            _QueryDefaults = value
        End Set
    End Property


End Class

Public Class CopyFromFiled
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property
End Class


Public Class GenAutoByFiled
    Private _FiledName As String
    Public Property FiledName() As String
        Get
            Return _FiledName
        End Get
        Set(ByVal value As String)
            _FiledName = value
        End Set
    End Property

    Private _GenByFiledName As String
    Public Property GenByFiledName() As String
        Get
            Return _GenByFiledName
        End Get
        Set(ByVal value As String)
            _GenByFiledName = value
        End Set
    End Property

End Class


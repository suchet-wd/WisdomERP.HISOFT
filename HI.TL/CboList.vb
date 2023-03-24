Public NotInheritable Class CboList

    Private Shared _ListComBo As New List(Of ComBoList)()

    Public Shared Function SetList(ListName As String) As String()
        Dim Str As String() = {}
        For I As Integer = 0 To _ListComBo.Count - 1
            If _ListComBo.Item(I).ListName = ListName Then

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    Str = _ListComBo.Item(I).ListTH
                Else
                    Str = _ListComBo.Item(I).ListEN
                End If

                Return Str

            End If
        Next
        Return Str
    End Function

    Public Shared Function GetListValue(ByVal ListName As String, ByVal Index As Integer) As String
        Dim Str As String = ""
        Try
            For I As Integer = 0 To _ListComBo.Count - 1
                If _ListComBo.Item(I).ListName = ListName Then
     
                    Str = _ListComBo.Item(I).ListValue(Index)


                    Return Str
                End If
            Next
        Catch ex As Exception
        End Try

        If Str = "" Then Str = Index.ToString
        Return Str

    End Function

    Public Shared Function GetListRefer(ByVal ListName As String, ByVal Index As Integer) As String
        Dim Str As String = ""
        Try
            For I As Integer = 0 To _ListComBo.Count - 1
                If _ListComBo.Item(I).ListName = ListName Then

                    Str = _ListComBo.Item(I).ListRefer(Index)

                    Return Str
                End If
            Next
        Catch ex As Exception
        End Try

        If Str = "" Then Str = Index.ToString
        Return Str

    End Function

    Public Shared Function GetIndexByValue(ByVal ListName As String, ByVal Value As String) As Integer
        Dim Str As Integer = 0
        For I As Integer = 0 To _ListComBo.Count - 1
            If _ListComBo.Item(I).ListName = ListName Then

                For k As Integer = 0 To _ListComBo.Item(I).ListValue.Count - 1
                    If Value = _ListComBo.Item(I).ListValue(k) Then
                        Return k
                    End If
                Next

                Return Str
            End If
        Next
        Return Str
    End Function

    Public Shared Function GetIndexByText(ByVal ListName As String, ByVal TextData As String) As Integer
        Dim Str As Integer = 0
        For I As Integer = 0 To _ListComBo.Count - 1
            If _ListComBo.Item(I).ListName = ListName Then

                For k As Integer = 0 To _ListComBo.Item(I).ListValue.Count - 1

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        If TextData = _ListComBo.Item(I).ListTH(k) Then
                            Return k
                        End If
                    Else
                        If TextData = _ListComBo.Item(I).ListEN(k) Then
                            Return k
                        End If
                    End If
                Next

                Return Str
            End If
        Next
        Return Str
    End Function

    Public Shared Sub PrepareList()
        _ListComBo.Clear()

        Dim _Qrysql As String = ""
        Dim _TmpStrTH As String = ""
        Dim _TmpStrEN As String = ""
        Dim _TmpStrValue As String = ""
        Dim _TmpStrRefer As String = ""
        Dim _Dt As New DataTable
        Dim _DtListName As New DataTable

        _Qrysql = " SELECT     FTListName "
        _Qrysql &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
        _Qrysql &= vbCrLf & " GROUP BY FTListName  "

        _DtListName = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

        _Qrysql = " SELECT     FTListName, FNListIndex, FTNameTH, FTNameEN,FTReferCode,CASE WHEN ISNULL(FNSortSeq,0) > 0 THEN ISNULL(FNSortSeq,0) ELSE  FNListIndex END AS FNSortSeq "
        _Qrysql &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
        _Qrysql &= vbCrLf & " ORDER BY FTListName,CASE WHEN ISNULL(FNSortSeq,0) > 0 THEN ISNULL(FNSortSeq,0) ELSE  FNListIndex END "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

        For Each R As DataRow In _DtListName.Rows

            _TmpStrTH = "" : _TmpStrEN = "" : _TmpStrValue = "" : _TmpStrRefer = ""

            For Each Row In _Dt.Select("FTListName='" & R!FTListName.ToString & "'", "FNSortSeq")

                If _TmpStrTH = "" Then

                    _TmpStrTH = Row!FTNameTH.ToString
                    _TmpStrEN = Row!FTNameEN.ToString
                    _TmpStrValue = Row!FNListIndex.ToString
                    _TmpStrRefer = Row!FTReferCode.ToString

                Else

                    _TmpStrTH = _TmpStrTH & "|" & Row!FTNameTH.ToString
                    _TmpStrEN = _TmpStrEN & "|" & Row!FTNameEN.ToString
                    _TmpStrValue = _TmpStrValue & "|" & Row!FNListIndex.ToString
                    _TmpStrRefer = _TmpStrRefer & "|" & Row!FTReferCode.ToString

                End If

            Next

            Dim M As New ComBoList
            M.ListName = R!FTListName.ToString
            M.ListEN = _TmpStrEN.Split("|")
            M.ListTH = _TmpStrTH.Split("|")
            M.ListValue = _TmpStrValue.Split("|")
            M.ListRefer = _TmpStrRefer.Split("|")

            _ListComBo.Add(M)

        Next

        _TmpStrTH = "" : _TmpStrEN = "" : _TmpStrValue = "" : _TmpStrRefer = ""

        Dim _Year As Integer = Integer.Parse(Left(HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)), 4))
        _Year = _Year + 1
        Dim _TempYear As Integer = _Year - 10

        Do
            If _TmpStrTH = "" Then
                _TmpStrTH = _Year.ToString
                _TmpStrEN = _Year.ToString
                _TmpStrValue = _Year.ToString
                _TmpStrRefer = _Year.ToString
            Else
                _TmpStrTH = _TmpStrTH & "|" & _Year.ToString
                _TmpStrEN = _TmpStrEN & "|" & _Year.ToString
                _TmpStrValue = _TmpStrValue & "|" & _Year.ToString
                _TmpStrRefer = _TmpStrRefer & "|" & _Year.ToString
            End If
            _Year = _Year - 1
        Loop Until _Year < _TempYear

        Dim M2 As New ComBoList
        M2.ListName = "FNYear"
        M2.ListEN = _TmpStrEN.Split("|")
        M2.ListTH = _TmpStrTH.Split("|")
        M2.ListValue = _TmpStrValue.Split("|")
        M2.ListRefer = _TmpStrRefer.Split("|")

        _ListComBo.Add(M2)



        _Qrysql = "  Select FTPayYear"
        _Qrysql &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH(NOLOCK)"
        _Qrysql &= vbCrLf & "  GROUP BY FTPayYear"
        _Qrysql &= vbCrLf & "  ORDER BY FTPayYear DESC"
        _Dt = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_HR)

        If _Dt.Rows.Count > 0 Then
            _TmpStrTH = "" : _TmpStrEN = "" : _TmpStrValue = "" : _TmpStrRefer = ""

            For Each Row In _Dt.Rows
                If _TmpStrTH = "" Then
                    _TmpStrTH = Row!FTPayYear.ToString
                    _TmpStrEN = Row!FTPayYear.ToString
                    _TmpStrValue = Row!FTPayYear.ToString
                    _TmpStrRefer = ""
                Else
                    _TmpStrTH = _TmpStrTH & "|" & Row!FTPayYear.ToString
                    _TmpStrEN = _TmpStrEN & "|" & Row!FTPayYear.ToString
                    _TmpStrValue = _TmpStrValue & "|" & Row!FTPayYear.ToString
                    _TmpStrRefer = _TmpStrRefer & "|"
                End If
            Next

            Dim M3 As New ComBoList
            M3.ListName = "FNPayYear"
            M3.ListEN = _TmpStrEN.Split("|")
            M3.ListTH = _TmpStrTH.Split("|")
            M3.ListValue = _TmpStrValue.Split("|")
            M3.ListRefer = _TmpStrRefer.Split("|")

            _ListComBo.Add(M3)

        End If

    End Sub

    Private Class ComBoList
        Private _ListName As String
        Public Property ListName() As String
            Get
                Return _ListName
            End Get
            Set(ByVal value As String)
                _ListName = value
            End Set
        End Property

        Private _ListEN As String()
        Public Property ListEN() As String()
            Get
                Return _ListEN
            End Get
            Set(ByVal value As String())
                _ListEN = value
            End Set
        End Property

        Private _ListTH As String()
        Public Property ListTH() As String()
            Get
                Return _ListTH
            End Get
            Set(ByVal value As String())
                _ListTH = value
            End Set
        End Property

        Private _ListValue As String()
        Public Property ListValue() As String()
            Get
                Return _ListValue
            End Get

            Set(ByVal value As String())
                _ListValue = value
            End Set
        End Property

        Private _ListRef As String()
        Public Property ListRefer() As String()
            Get
                Return _ListRef
            End Get

            Set(ByVal value As String())
                _ListRef = value
            End Set
        End Property
    End Class

End Class



Public Class ListReport

    Sub New(_FormName As String)
        Call PrepareList(_FormName)
    End Sub

    Private Sub PrepareList(_FormName As String)
        Dim _Str As String = ""
        Dim _dt As DataTable
        Dim _TmpFTNameTH, _TmpFTNameEN, _TmpFTReportName, _TmpFTStateGenPicture, _TmpFTReportFolderName, _TmpFTImageFolderName As String

        _Str = " SELECT   FTNameTH"
        _Str &= vbCrLf & ", FTNameEN"
        _Str &= vbCrLf & ", FTReportFolderName"
        _Str &= vbCrLf & ", FTReportName"
        _Str &= vbCrLf & ", FTImageFolderName"
        _Str &= vbCrLf & ", FTStateGenPicture"
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysReportList AS A WITH (NOLOCK) "
        _Str &= vbCrLf & " WHERE FTFormName='" & HI.UL.ULF.rpQuoted(_FormName) & "' "
        _Str &= vbCrLf & " ORDER BY  FNSeq "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then
            _TmpFTNameTH = "" : _TmpFTNameEN = "" : _TmpFTReportName = "" : _TmpFTStateGenPicture = ""
            _TmpFTReportFolderName = "" : _TmpFTImageFolderName = ""

            For Each Row In _dt.Rows
                If _TmpFTNameTH = "" Then
                    _TmpFTNameTH = Row!FTNameTH.ToString
                    _TmpFTNameEN = Row!FTNameEN.ToString
                    _TmpFTReportFolderName = Row!FTReportFolderName.ToString
                    _TmpFTReportName = Row!FTReportName.ToString
                    _TmpFTImageFolderName = Row!FTImageFolderName.ToString
                    _TmpFTStateGenPicture = Row!FTStateGenPicture.ToString
                Else
                    _TmpFTNameTH = _TmpFTNameTH & "|" & Row!FTNameTH.ToString
                    _TmpFTNameEN = _TmpFTNameEN & "|" & Row!FTNameEN.ToString
                    _TmpFTReportFolderName = _TmpFTReportFolderName & "|" & Row!FTReportFolderName.ToString
                    _TmpFTReportName = _TmpFTReportName & "|" & Row!FTReportName.ToString
                    _TmpFTImageFolderName = _TmpFTImageFolderName & "|" & Row!FTImageFolderName.ToString
                    _TmpFTStateGenPicture = _TmpFTStateGenPicture & "|" & Row!FTStateGenPicture.ToString
                End If
            Next

            Dim M As New ListOfReport
            M.ListTH = _TmpFTNameTH.Split("|")
            M.ListEN = _TmpFTNameEN.Split("|")
            M.ListFolderReportValue = _TmpFTReportFolderName.Split("|")
            M.ListValue = _TmpFTReportName.Split("|")
            M.ListFolderImageValue = _TmpFTImageFolderName.Split("|")
            M.ListValueGenPic = _TmpFTStateGenPicture.Split("|")
            _ListOfReport.Add(M)

        End If

    End Sub

    Public _ListOfReport As New List(Of ListOfReport)()
    Public Function GetList(Optional ByVal ListIndex As Integer = 0) As String()
        Dim Str As String() = {}
        Try
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                Str = _ListOfReport.Item(ListIndex).ListTH
            Else
                Str = _ListOfReport.Item(ListIndex).ListEN
            End If
        Catch ex As Exception
        End Try
        Return Str
    End Function

    Public Function GetFolderReportValue(ByVal Index As Integer, Optional ByVal ListIndex As Integer = 0) As String
        Dim Str As String = ""
        Try
            Str = _ListOfReport.Item(ListIndex).ListFolderReportValue(Index)
        Catch ex As Exception
        End Try
        Return Str
    End Function

    Public Function GetValue(ByVal Index As Integer, Optional ByVal ListIndex As Integer = 0) As String
        Dim Str As String = ""
        Try
            Str = _ListOfReport.Item(ListIndex).ListValue(Index)
        Catch ex As Exception
        End Try
        Return Str
    End Function

    Public Function GetFolderImageValue(ByVal Index As Integer, Optional ByVal ListIndex As Integer = 0) As String
        Dim Str As String = ""
        Try
            Str = _ListOfReport.Item(ListIndex).ListFolderImageValue(Index)
        Catch ex As Exception
        End Try
        Return Str
    End Function

    Public Function GetValueGenPic(ByVal Index As Integer, Optional ByVal ListIndex As Integer = 0) As String
        Dim Str As String = ""
        Try
            Str = _ListOfReport.Item(ListIndex).ListValueGenPic(Index)
        Catch ex As Exception
        End Try
        Return Str
    End Function

    Public Function GetIndexByValue(ByVal Value As String, Optional ByVal ListIndex As Integer = 0) As Integer
        Dim Str As Integer = 0
        Try
            For k As Integer = 0 To _ListOfReport.Item(ListIndex).ListValue.Count - 1
                If Value = _ListOfReport.Item(ListIndex).ListValue(k) Then
                    Return k
                End If
            Next
        Catch ex As Exception
        End Try
        Return Str
    End Function

End Class

Public Class ListOfReport
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

    Private _ListFolderReportValue As String()
    Public Property ListFolderReportValue() As String()
        Get
            Return _ListFolderReportValue
        End Get

        Set(ByVal value As String())
            _ListFolderReportValue = value
        End Set
    End Property

    Private _ListFolderImageValue As String()
    Public Property ListFolderImageValue() As String()
        Get
            Return _ListFolderImageValue
        End Get

        Set(ByVal value As String())
            _ListFolderImageValue = value
        End Set
    End Property

    Private _ListValueGenPic As String()
    Public Property ListValueGenPic() As String()
        Get
            Return _ListValueGenPic
        End Get

        Set(ByVal value As String())
            _ListValueGenPic = value
        End Set
    End Property

End Class



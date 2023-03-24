Public Class ButtonEditTag

    Public Property Value As String = ""
    Public Property BrowObjectID As Integer = 0
    Public Property BrwRetID As Integer = 0

    Public Property BrowseInfo As wDynamicBrowseInfo
    Public Property CmdSelectTH As String = ""
    Public Property CmdSelectEN As String = ""
    Public Property CmdWhere As String = ""
    Public Property CmdBrowseField As String = ""
    Public Property CmdFieldOptional As String = ""
    Public Property CmdStringFormatWhare As String = ""
    Public Property CmdGroupByTH As String = ""
    Public Property CmdGroupByEN As String = ""
    Public Property CmdSortBy As String = ""
    Public Property FTConField As String = ""

    Public Property MinLenght As Integer = 0
    Public Property MaxLenght As Integer = 0
    Public Property DataRet As DataTable = Nothing
    Public Property LoadData As Boolean = False


End Class

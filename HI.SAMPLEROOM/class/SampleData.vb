Imports System.Text
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports System.Collections.ObjectModel

' The data model defined by this file serves as a representative example of a strongly-typed
' model that supports notification when members are added, removed, or modified.  The property
' names chosen coincide with data bindings in the standard item WindowsUIViewApplications.
'
' Applications may use this model as a starting point and build on it, or discard it entirely and
' replace it with something appropriate to their needs.

''' <summary>
''' Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataGroup"/> that
''' defines properties common to both.
''' </summary>
Public Class SampleDataCommon
    Private subtitleCore, imagePathCore, descriptionCore, titleCore As String
    Public ReadOnly Property Title() As String
        Get
            Return titleCore
        End Get
    End Property
    Public ReadOnly Property Subtitle() As String
        Get
            Return subtitleCore
        End Get
    End Property
    Public ReadOnly Property ImagePath() As String
        Get
            Return imagePathCore
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Return descriptionCore
        End Get
    End Property
    Public Sub New(ByVal title As String, ByVal subtitle As String, ByVal imagePath As String, ByVal description As String)
        titleCore = title
        subtitleCore = subtitle
        imagePathCore = imagePath
        descriptionCore = description
    End Sub
    Public Sub New()
    End Sub
End Class
''' <summary>
''' Generic item data model.
''' </summary>
Public Class SampleDataItem
    Inherits SampleDataCommon
    Private contentCore, groupNameCore As String
    Private _id As Integer
    Public Sub New(ByVal title As String, ByVal subtitle As String, ByVal imagePath As String, ByVal description As String, ByVal content As String, ByVal groupName As String, ByVal _codeId As Integer)
        MyBase.New(title, subtitle, imagePath, description)
        contentCore = content
        groupNameCore = groupName
        _id = _codeId
    End Sub
    Public ReadOnly Property Id() As Integer
        Get
            Return _id
        End Get
    End Property
    Public ReadOnly Property Content() As String
        Get
            Return contentCore
        End Get
    End Property
    Public ReadOnly Property GroupName() As String
        Get
            Return groupNameCore
        End Get
    End Property
End Class

Public Class subSampleDataItem
    Inherits SampleDataCommon
    Private contentCore, groupNameCore As String
    Private _id As Integer
    Public Sub New(ByVal title As String, ByVal subtitle As String, ByVal imagePath As String, ByVal description As String, ByVal content As String, ByVal groupName As String, ByVal _codeId As Integer)
        MyBase.New(title, subtitle, imagePath, description)
        contentCore = content
        groupNameCore = groupName
        _id = _codeId
    End Sub
    Public ReadOnly Property Id() As Integer
        Get
            Return _id
        End Get
    End Property
    Public ReadOnly Property Content() As String
        Get
            Return contentCore
        End Get
    End Property
    Public ReadOnly Property GroupName() As String
        Get
            Return groupNameCore
        End Get
    End Property
End Class


''' <summary>
''' Generic group data model.
''' </summary>
Public Class SampleDataGroup
    Inherits SampleDataCommon
    Private nameCore As String
    Private itemsCore As Collection(Of SampleDataItem)
    Public Sub New(ByVal name As String)
        MyBase.New()
        Me.nameCore = name
        itemsCore = New Collection(Of SampleDataItem)()
    End Sub
    Public Sub New(ByVal name As String, ByVal title As String, ByVal subtitle As String, ByVal imagePath As String, ByVal description As String)
        MyBase.New(title, subtitle, imagePath, description)
        Me.nameCore = name
        itemsCore = New Collection(Of SampleDataItem)()
    End Sub
    Public ReadOnly Property Name() As String
        Get
            Return nameCore
        End Get
    End Property
    Public ReadOnly Property Items() As Collection(Of SampleDataItem)
        Get
            Return itemsCore
        End Get
    End Property
    Public Function AddItem(ByVal tile As SampleDataItem) As Boolean
        If Not itemsCore.Contains(tile) Then
            itemsCore.Add(tile)
            Return True
        End If
        Return False
    End Function
End Class
''' <summary>
''' Generic data model.
''' </summary>
Friend Class SampleDataModel
    Private groupsCore As Collection(Of SampleDataGroup)
    Public Sub New()
        groupsCore = New Collection(Of SampleDataGroup)()
    End Sub
    Public ReadOnly Property Groups() As Collection(Of SampleDataGroup)
        Get
            Return groupsCore
        End Get
    End Property
    Private Function GetGroup(ByVal name As String) As SampleDataGroup
        For Each group In groupsCore
            If group.Name = name Then
                Return group
            End If
        Next group
        Return Nothing
    End Function
    Public Function AddItem(ByVal tile As SampleDataItem) As Boolean
        If tile Is Nothing Then
            Return False
        End If
        Dim groupName As String = If(tile.GroupName Is Nothing, "", tile.GroupName)
        Dim thisGroup As SampleDataGroup = GetGroup(groupName)
        If thisGroup Is Nothing Then
            thisGroup = New SampleDataGroup(groupName)
            groupsCore.Add(thisGroup)
        End If
        Return thisGroup.AddItem(tile)
    End Function
    Private Function ContainsGroup(ByVal name As String) As Boolean
        Return GetGroup(name) IsNot Nothing
    End Function
    Public Sub CreateGroup(ByVal name As String, ByVal title As String, ByVal subtitle As String, ByVal imagePath As String, ByVal description As String)
        If ContainsGroup(name) Then
            Return
        End If
        Dim group As New SampleDataGroup(name, title, subtitle, imagePath, description)
        groupsCore.Add(group)
    End Sub
End Class
''' <summary>
''' Creates a collection of groups and items with hard-coded content.
''' 
''' SampleDataSource initializes with placeholder data rather than live production
''' data so that sample data is provided at both design-time and run-time.
''' </summary>
Friend Class SampleDataSource
    Private dataCore As SampleDataModel
    Public Sub New()
        dataCore = New SampleDataModel()
     

        Dim _oDt As DataTable
        Dim _Qry As String = ""
        _Qry = "SELECT     FNHSysQATypeId, FTQATypeCode, FTStateActive"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", FTQATypeNameTH as FTQATypeName "
        Else
            _Qry &= vbCrLf & ", FTQATypeNameEN as FTQATypeName"
        End If
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQAType WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTStateActive = '1'"
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        Dim _L As Integer = 1
        Dim _G As Integer = 1
        For Each R As DataRow In _oDt.Rows
            dataCore.AddItem(New SampleDataItem(R!FTQATypeCode.ToString, R!FTQATypeCode.ToString, "", R!FTQATypeName.ToString, R!FTQATypeName.ToString, "Group-" & _G, CInt(R!FNHSysQATypeId)))
         
        Next

    End Sub
    Public ReadOnly Property Data() As SampleDataModel
        Get
            Return dataCore
        End Get
    End Property
End Class


'Friend Class subSampleDataSource
'    Private dataCore As SampleDataModel
'    Public Sub New()
'        dataCore = New SampleDataModel()


'        Dim _oDt As DataTable
'        Dim _Qry As String = ""
'        _Qry = "SELECT     FNHSysQATypeId, FTQATypeCode, FTStateActive"
'        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
'            _Qry &= vbCrLf & ", FTQATypeNameTH as FTQATypeName "
'        Else
'            _Qry &= vbCrLf & ", FTQATypeNameEN as FTQATypeName"
'        End If
'        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQAType WITH(NOLOCK) "
'        _Qry &= vbCrLf & " WHERE FTStateActive = '1'"
'        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
'        For Each R As DataRow In _oDt.Rows
'            dataCore.AddItem(New subSampleDataItem(R!FTQATypeCode.ToString, R!FTQATypeCode.ToString, "", R!FTQATypeName.ToString, R!FTQATypeName.ToString, "Group-1", CInt(R!FNHSysQATypeId)))
'        Next



'          End Sub
'    Public ReadOnly Property Data() As SampleDataModel
'        Get
'            Return dataCore
'        End Get
'    End Property
'End Class

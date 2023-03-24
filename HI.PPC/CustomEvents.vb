Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel


Public Class CustomEvent
    Implements IEditableObject
    Private fStartTime As DateTime
    Private fEndTime As DateTime
    Private fSubject As String
    Private fStatus As Integer
    Private fDescription As String
    Private fLabel As Long
    Private fLocation As String
    Private fAllday As Boolean
    Private fEventType As Integer
    Private fRecurrenceInfo As String
    Private fReminderInfo As String
    Private fOwnerId As Object

    Private events As CustomEventList
    Private committed As Boolean = False

    Public Sub New(ByVal events As CustomEventList)
        Me.events = events
    End Sub

    Private Sub OnListChanged()
        Dim index As Integer = events.IndexOf(Me)
        events.OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, index))
    End Sub

    Public Property StartTime() As DateTime
        Get
            Return fStartTime
        End Get
        Set(ByVal value As DateTime)
            fStartTime = value
        End Set
    End Property
    Public Property EndTime() As DateTime
        Get
            Return fEndTime
        End Get
        Set(ByVal value As DateTime)
            fEndTime = value
        End Set
    End Property
    Public Property Subject() As String
        Get
            Return fSubject
        End Get
        Set(value As String)
            fSubject = Value
        End Set
    End Property
    Public Property Status() As Integer
        Get
            Return fStatus
        End Get
        Set(value As Integer)
            fStatus = Value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(value As String)
            fDescription = Value
        End Set
    End Property
    Public Property Label() As Long
        Get
            Return fLabel
        End Get
        Set(value As Long)
            fLabel = Value
        End Set
    End Property
    Public Property Location() As String
        Get
            Return fLocation
        End Get
        Set(value As String)
            fLocation = Value
        End Set
    End Property
    Public Property AllDay() As Boolean
        Get
            Return fAllday
        End Get
        Set(value As Boolean)
            fAllday = Value
        End Set
    End Property
    Public Property EventType() As Integer
        Get
            Return fEventType
        End Get
        Set(value As Integer)
            fEventType = Value
        End Set
    End Property
    Public Property RecurrenceInfo() As String
        Get
            Return fRecurrenceInfo
        End Get
        Set(value As String)
            fRecurrenceInfo = Value
        End Set
    End Property
    Public Property ReminderInfo() As String
        Get
            Return fReminderInfo
        End Get
        Set(value As String)
            fReminderInfo = Value
        End Set
    End Property
    Public Property OwnerId() As Object
        Get
            Return fOwnerId
        End Get
        Set(value As Object)
            fOwnerId = Value
        End Set
    End Property

    Public Sub BeginEdit() Implements IEditableObject.BeginEdit
    End Sub
    Public Sub CancelEdit() Implements IEditableObject.CancelEdit
        If (Not committed) Then
            CType(events, IList).Remove(Me)
        End If
    End Sub
    Public Sub EndEdit() Implements IEditableObject.EndEdit
        committed = True
    End Sub
End Class

Public Class CustomEventList
    Inherits CollectionBase
    Implements IBindingList
    Default Public ReadOnly Property Item(ByVal idx As Integer) As CustomEvent
        Get
            Return CType(MyBase.List(idx), CustomEvent)
        End Get
    End Property

    Public Shadows Sub Clear()
        MyBase.Clear()
        OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub
    Public Sub Add(ByVal appointment As CustomEvent)
        MyBase.List.Add(appointment)
    End Sub
    Public Function IndexOf(ByVal appointment As CustomEvent) As Integer
        Return List.IndexOf(appointment)
    End Function
    Public Function AddNew() As Object Implements IBindingList.AddNew
        Dim app As CustomEvent = New CustomEvent(Me)
        List.Add(app)
        Return app
    End Function
    Public ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit
        Get
            Return True
        End Get
    End Property
    Public ReadOnly Property AllowNew() As Boolean Implements IBindingList.AllowNew
        Get
            Return True
        End Get
    End Property
    Public ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
        Get
            Return True
        End Get
    End Property

    Private Event listChangedHandler As ListChangedEventHandler
    Public Custom Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged
        AddHandler(ByVal value As ListChangedEventHandler)
            AddHandler listChangedHandler, value
        End AddHandler
        RemoveHandler(ByVal value As ListChangedEventHandler)
            RemoveHandler listChangedHandler, value
        End RemoveHandler
        RaiseEvent(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
        End RaiseEvent
    End Event
    Friend Sub OnListChanged(ByVal args As ListChangedEventArgs)
        If Not listChangedHandlerEvent Is Nothing Then
            RaiseEvent listChangedHandler(Me, args)
        End If
    End Sub
    Protected Overrides Sub OnRemoveComplete(ByVal index As Integer, ByVal value As Object)
        OnListChanged(New ListChangedEventArgs(ListChangedType.ItemDeleted, index))
    End Sub
    Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
        OnListChanged(New ListChangedEventArgs(ListChangedType.ItemAdded, index))
    End Sub

    Public Sub AddIndex(ByVal pd As PropertyDescriptor) Implements IBindingList.AddIndex
        Throw New NotSupportedException()
    End Sub
    Public Sub ApplySort(ByVal pd As PropertyDescriptor, ByVal dir As ListSortDirection) Implements IBindingList.ApplySort
        Throw New NotSupportedException()
    End Sub
    Public Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
        Throw New NotSupportedException()
    End Function
    Public ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted
        Get
            Return False
        End Get
    End Property
    Public Sub RemoveIndex(ByVal pd As PropertyDescriptor) Implements IBindingList.RemoveIndex
        Throw New NotSupportedException()
    End Sub
    Public Sub RemoveSort() Implements IBindingList.RemoveSort
        Throw New NotSupportedException()
    End Sub
    Public ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection
        Get
            Throw New NotSupportedException()
        End Get
    End Property
    Public ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
        Get
            Throw New NotSupportedException()
        End Get
    End Property
    Public ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
        Get
            Return True
        End Get
    End Property
    Public ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
        Get
            Return False
        End Get
    End Property
    Public ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
        Get
            Return False
        End Get
    End Property
End Class

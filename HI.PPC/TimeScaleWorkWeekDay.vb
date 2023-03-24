Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraScheduler


Namespace TimelineTimeScales
    Public Class TimeScaleWorkWeekDay
        Inherits TimeScale

        Private Shared ReadOnly StartTime As TimeSpan = TimeSpan.FromHours(8)
        Private Shared ReadOnly EndTime As TimeSpan = TimeSpan.FromHours(17 - 1)

        Protected Overrides ReadOnly Property DefaultDisplayFormat() As String
            Get
                Return "dd ddd"
            End Get
        End Property
        Protected Overrides ReadOnly Property DefaultDisplayName() As String
            Get
                Return "Custom Day"
            End Get
        End Property
        Protected Overrides ReadOnly Property DefaultMenuCaption() As String
            Get
                Return "Custom Day"
            End Get
        End Property
        Protected Overrides ReadOnly Property SortingWeight() As TimeSpan
            Get
                Return TimeSpan.FromDays(1)
            End Get
        End Property

        Public Overrides Function Floor(ByVal [date] As Date) As Date
            Dim start As Date = [date].Date
            If start.DayOfWeek = DayOfWeek.Monday AndAlso [date].TimeOfDay < StartTime Then
                start = start.AddDays(-3)
                'ElseIf start.DayOfWeek = DayOfWeek.Saturday Then
                '    start = start.AddDays(-1)
                'ElseIf start.DayOfWeek = DayOfWeek.Sunday Then
                '    start = start.AddDays(-2)
            ElseIf [date].TimeOfDay < StartTime Then
                start = start.AddDays(-1)
            End If
            Return start.Add(StartTime)
        End Function
        Protected Overrides Function HasNextDate(ByVal [date] As Date) As Boolean
            Dim days As Integer = GetNextDayOffset([date].DayOfWeek)
            Return [date] <= Date.MaxValue.AddDays(-days)
        End Function
        Public Overrides Function GetNextDate(ByVal [date] As Date) As Date
            Dim days As Integer = GetNextDayOffset([date].DayOfWeek)
            Return [date].AddDays(days)
        End Function
        Protected Function GetNextDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
            'If dayOfWeek = System.DayOfWeek.Friday Then
            '    Return 3
            'End If
            'If dayOfWeek = System.DayOfWeek.Saturday Then
            '    Return 2
            'End If
            Return 1
        End Function
    End Class
    Public Class TimeScaleLessThanDay
        Inherits TimeScaleFixedInterval

        Private Shared ReadOnly StartTimeLimitation As TimeSpan = TimeSpan.FromHours(8)
        Private Shared ReadOnly EndTimeLimitation As TimeSpan = TimeSpan.FromHours(17)

        Public Sub New(ByVal scaleValue As TimeSpan)
            MyBase.New(scaleValue)
        End Sub

        Private ReadOnly Property StartTime() As TimeSpan
            Get
                Return StartTimeLimitation
            End Get
        End Property
        Private ReadOnly Property EndTime() As TimeSpan
            Get
                Return EndTimeLimitation - Value
            End Get
        End Property
        Protected Overrides ReadOnly Property DefaultDisplayFormat() As String
            Get
                Return "HH:mm"
            End Get
        End Property

        Protected Overrides ReadOnly Property SortingWeight() As TimeSpan
            Get
                Return Value
            End Get
        End Property
        Public Overrides Function Floor(ByVal [date] As Date) As Date
            If [date] = Date.MinValue OrElse [date] = Date.MaxValue Then
                Return [date]
            End If

            If [date].TimeOfDay < StartTime Then
                ' Round down to the end of the previous working day.
                Return RoundToHour([date].AddDays(GetPreviousDayOffset([date].DayOfWeek)), EndTime)
            End If

            If [date].TimeOfDay > EndTime Then
                ' Round down to the end of the current working day.
                Dim date1 As Date = [date].AddDays(GetPreviousDayOffset1([date].DayOfWeek))
                Return RoundToHour(date1, EndTime)
            End If
            Return MyBase.Floor([date])
        End Function
        Protected Overrides Function HasNextDate(ByVal [date] As Date) As Boolean
            Dim days As Integer = GetNextDayOffset([date].DayOfWeek)
            Return [date].AddDays(days) <= RoundToHour(Date.MaxValue, EndTime)
        End Function
        Protected Function GetNextDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
            'If dayOfWeek = System.DayOfWeek.Friday Then
            '    Return 3
            'End If
            'If dayOfWeek = System.DayOfWeek.Saturday Then
            '    Return 2
            'End If
            Return 1
        End Function
        Protected Function GetPreviousDayOffset1(ByVal dayOfWeek As DayOfWeek) As Integer
            If dayOfWeek = System.DayOfWeek.Monday Then
                Return -3
            End If
            If dayOfWeek = System.DayOfWeek.Sunday Then
                Return -2
            End If
            If dayOfWeek = System.DayOfWeek.Saturday Then
                Return -1
            End If

            Return 0
        End Function
        Protected Function GetPreviousDayOffset(ByVal dayOfWeek As DayOfWeek) As Integer
            If dayOfWeek = System.DayOfWeek.Monday Then
                Return -3
            End If
            If dayOfWeek = System.DayOfWeek.Sunday Then
                Return -2
            End If
            Return -1
        End Function
        Public Overrides Function GetNextDate(ByVal [date] As Date) As Date
            Return If([date].TimeOfDay >= EndTime, RoundToHour([date].AddDays(GetNextDayOffset([date].DayOfWeek)), StartTime), MyBase.GetNextDate([date]))
        End Function
        Protected Function RoundToHour(ByVal [date] As Date, ByVal timeOfDay As TimeSpan) As Date
            Return [date].Date + timeOfDay
        End Function
    End Class
End Namespace
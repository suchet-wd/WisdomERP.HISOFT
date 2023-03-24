Imports System.Runtime.InteropServices

Public Class SetDateTimeLocal

    'System time structure used to pass to P/Invoke...
    <StructLayoutAttribute(LayoutKind.Sequential)>
    Private Structure SYSTEMTIME
        Public year As Short
        Public month As Short
        Public dayOfWeek As Short
        Public day As Short
        Public hour As Short
        Public minute As Short
        Public second As Short
        Public milliseconds As Short
    End Structure

    'P/Invoke dec for setting the system time...
    <DllImport("Kernel32.dll")>
    Private Shared Function SetLocalTime(ByRef time As SYSTEMTIME) As Boolean
    End Function

    Private Sub ChangeDate()

        Dim st As SYSTEMTIME
        Dim NewDate As Date = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM) '"28-April-1978 22:30:00"
        st.year = NewDate.Year
        st.month = NewDate.Month
        st.dayOfWeek = NewDate.DayOfWeek
        st.day = NewDate.Day
        st.hour = NewDate.Hour
        st.minute = NewDate.Minute
        st.second = NewDate.Second
        st.milliseconds = NewDate.Millisecond

        'Set the new time...
        SetLocalTime(st)
    End Sub

End Class

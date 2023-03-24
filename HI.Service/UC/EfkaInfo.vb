
Imports System.Threading
Public Class EfkaInfo

    Public Sub New(MachineNo As Integer, EfkaSerialNo As String, ByVal EfkaIP As String)


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        olbtop.Text = "Machine No. " & MachineNo.ToString



        DataSerailNo = EfkaSerialNo
        DataIP = EfkaIP

        FTSerialNo.Text = EfkaSerialNo
        FTIP.Text = EfkaIP
        FTDate.Text = ""
        FTTime.Text = ""
        FTTimeSewing.Text = ""
        FTTotalStitch.Text = ""
        FTAvgSpeed.Text = ""
        FTRuntime.Text = ""
        FTStop.Text = ""
        FTTimeBet.Text = ""


        If MachineNo Mod 2 <> 0 Then
            Me.BackColor = Drawing.Color.White
        Else
            Me.BackColor = Drawing.Color.FromArgb(255, 255, 192)
        End If

        Dim _Theard As New System.Threading.Thread(AddressOf CheckDataEfkaInfo)
        _Theard.Start(DataIP)

    End Sub

    Private _DataSerailNo As String = ""
    Property DataSerailNo As String
        Get
            Return _DataSerailNo
        End Get
        Set(value As String)
            _DataSerailNo = value
        End Set
    End Property


    Private _DataIP As String = ""
    Property DataIP As String
        Get
            Return _DataIP
        End Get
        Set(value As String)
            _DataIP = value
        End Set
    End Property

    Private _StateTimchecker As Boolean = False
    Property StateTimchecker As Boolean
        Get
            Return _StateTimchecker
        End Get
        Set(value As Boolean)
            _StateTimchecker = value
        End Set
    End Property

    Private Sub Timchecker_Tick(sender As Object, e As EventArgs) Handles Timchecker.Tick

        If StateTimchecker Then
            StateTimchecker = False
            Timchecker.Enabled = False
            Dim _Theard As New System.Threading.Thread(AddressOf CheckDataEfkaInfo)
            _Theard.Start(DataIP)
        End If

    End Sub


    Private Delegate Sub DelegateCheckDataEfkaInfo(ByVal EfkaIP As String)
    Private Sub CheckDataEfkaInfo(ByVal EfkaIP As String)
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckDataEfkaInfo(AddressOf CheckDataEfkaInfo), New Object() {EfkaIP})
        Else


            If EfkaIP <> "" Then

                Dim _Str As String = ""
                Dim _Dt As DataTable


                _Str = "  Select  Top 1 FDInsDate, FTInsTime, FTID, FTControlData, FTClientIP, FNTimeSewing, FNTotalStitches, FNAVGSpeed, FNRunTime, FNStopTime, FNTimeBetweenStartEnd "
                _Str &= vbCrLf & ",CASE WHEN ISDATE(FDInsDate) = 1 THEN  Convert(nvarchar(10), Convert(Datetime,FDInsDate) ,103) ELSE '' END AS FTDataDate"
                _Str &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData05 As X With(NOLOCK)"
                _Str &= vbCrLf & " Where (FTClientIP ='" & HI.UL.ULF.rpQuoted(EfkaIP) & "')  "
                '_Str &= vbCrLf & " And (FDInsDate = Convert(nvarchar(10), GetDate(), 111)) "

                _Str &= vbCrLf & " And (FDInsDate ='2017/12/09') "

                _Str &= vbCrLf & " ORDER BY FTInsTime DESC "

                _Dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                For Each R As DataRow In _Dt.Rows

                    FTDate.Text = R!FTDataDate.ToString
                    FTTime.Text = R!FTInsTime.ToString
                    FTTimeSewing.Text = Format(Val(R!FNTimeSewing.ToString), "#,##")
                    FTTotalStitch.Text = Format(Val(R!FNTotalStitches.ToString), "#,##") ' R!FNTotalStitches.ToString
                    FTAvgSpeed.Text = Format(Val(R!FNAVGSpeed.ToString), "#,##") ' R!FNAVGSpeed.ToString
                    FTRuntime.Text = Format(Val(R!FNRunTime.ToString), "#,##") ' R!FNRunTime.ToString
                    FTStop.Text = Format(Val(R!FNStopTime.ToString), "#,##") 'R!FNStopTime.ToString
                    FTTimeBet.Text = Format(Val(R!FNTimeBetweenStartEnd.ToString), "#,##") 'R!FNTimeBetweenStartEnd.ToString

                Next

                _Dt.Dispose()


            End If

            StateTimchecker = True
            Timchecker.Enabled = True

        End If

    End Sub

    Private Sub FTDate_TextChanged(sender As Object, e As EventArgs) Handles FTDate.TextChanged
        If FTDate.Text <> "" Then
            olbtop.Appearance.BackColor = Drawing.Color.FromArgb(0, 192, 0)
        Else
            olbtop.Appearance.BackColor = Drawing.Color.DarkGray
        End If
    End Sub
End Class


Imports System.Threading
Public Class EfkaDataCycleTimeInfo

    Public Sub New(MachineNo As Integer, EfkaSerialNo As String, ByVal EfkaIP As String, Machine As String, LineId As Integer, MachineId As Integer)


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        olbtop.Text = "Machine No. " & MachineNo.ToString

        DataSerailNo = EfkaSerialNo
        DataIP = EfkaIP

        UnitSectId = LineId
        AssetId = MachineId

        FTSerialNo.Text = EfkaSerialNo
        FTIP.Text = EfkaIP
        FTEmpId.Text = ""
        FTMachine.Text = Machine
        FTStdtime.Text = ""

        FTOperation.Text = ""
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

    Private _UnitSectId As Integer = 0
    Property UnitSectId As Integer
        Get
            Return _UnitSectId
        End Get
        Set(value As Integer)
            _UnitSectId = value
        End Set
    End Property

    Private _AssetId As Integer = 0
    Property AssetId As Integer
        Get
            Return _AssetId
        End Get
        Set(value As Integer)
            _AssetId = value
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

    Public Sub LodDataCycleInfo()


        Dim _Str As String = ""
            Dim _Dt As DataTable

        _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_EFKA_CYCLETIME " & UnitSectId & "," & AssetId & ""
        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        If _Dt.Rows.Count > 0 Then

            For Each R As DataRow In _Dt.Rows

                Try

                    If Val(R!FNDataSeq.ToString) > 0 Then

                        FTStdtime.Text = Format(Val(R!FNSam.ToString), "#,##0.00") 'R!FNSam.ToString
                        FTOperation.Text = R!FTOperationName.ToString 'R!FNStopTime.ToString
                        FTTimeBet.Text = Format(Val(R!FNTotalTimeAVG.ToString), "#,##0.00") 'R!FNTimeBetweenStartEnd.ToString

                    End If

                Catch ex As Exception
                End Try

                Exit For

            Next

        Else
            FTStdtime.Text = ""
            FTOperation.Text = ""
            FTTimeBet.Text = ""

        End If

        _Dt.Dispose()

    End Sub

    Private Delegate Sub DelegateCheckDataEfkaInfo(ByVal EfkaIP As String)
    Private Sub CheckDataEfkaInfo(ByVal EfkaIP As String)
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckDataEfkaInfo(AddressOf CheckDataEfkaInfo), New Object() {EfkaIP})
        Else

            If EfkaIP <> "" Then
                LodDataCycleInfo()
            End If


            StateTimchecker = True
            Timchecker.Enabled = True

        End If

    End Sub


    Private Sub FTOperation_TextChanged(sender As Object, e As EventArgs) Handles FTOperation.TextChanged
        If FTOperation.Text <> "" Then
            olbtop.Appearance.BackColor = Drawing.Color.FromArgb(0, 192, 0)
            olbtop.Appearance.ForeColor = Drawing.Color.FromArgb(0, 0, 192)

            FTSerialNo.Appearance.ForeColor = Drawing.Color.Green
            FTIP.Appearance.ForeColor = Drawing.Color.Green

            FTOperation.Appearance.ForeColor = Drawing.Color.Green
            FTTimeBet.Appearance.ForeColor = Drawing.Color.Green
            FTEmpId.Appearance.ForeColor = Drawing.Color.Green
            FTMachine.Appearance.ForeColor = Drawing.Color.Green
            FTDowntime.Appearance.ForeColor = Drawing.Color.Green
            FTStdtime.Appearance.ForeColor = Drawing.Color.Green
        Else
            olbtop.Appearance.BackColor = Drawing.Color.DarkGray
            olbtop.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)


            FTSerialNo.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)
            FTIP.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)

            FTOperation.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)
            FTTimeBet.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)
            FTEmpId.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)
            FTMachine.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)
            FTDowntime.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)
            FTStdtime.Appearance.ForeColor = Drawing.Color.FromArgb(192, 0, 0)
        End If
    End Sub


    Private Sub FTTimeBet_TextChanged(sender As Object, e As EventArgs) Handles FTTimeBet.TextChanged
        If FTTimeBet.Text <> "" Then
            If IsNumeric(FTTimeBet.Text) And IsNumeric(FTStdtime.Text) Then
                If CDbl(FTStdtime.Text) < CDbl(FTTimeBet.Text) Then
                    FTTimeBet.Appearance.ForeColor = Drawing.Color.Red
                End If

            End If
        End If
    End Sub
End Class

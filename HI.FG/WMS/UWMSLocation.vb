Public Class UWMSLocation

    Public ListUloc As List(Of ULocation)
    Sub New(wh As String, grploc As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Size = New System.Drawing.Size(170, 600)
        ListUloc = New List(Of ULocation)
        InitLocaion(wh, grploc)

    End Sub

    Enum StateInOut As Integer
        StateStop = 0
        StateIn = 1
        StateOut = 2
    End Enum

    Private StateStartReader As Boolean = False
    Property StartReader As Boolean
        Get
            Return StateStartReader
        End Get
        Set(value As Boolean)
            StateStartReader = value
        End Set
    End Property

    Private StateDataInOut As StateInOut = StateInOut.StateIn
    Property DataInOut As StateInOut
        Get
            Return StateDataInOut
        End Get
        Set(value As StateInOut)
            StateDataInOut = value
        End Set
    End Property


    Private Sub InitLocaion(wh As String, grploc As String)

        Dim StartLoc As Integer = 45
        Dim cmdstring As String = ""
        Dim TotalLoc As Integer = 0
        Dim dt As DataTable
        cmdstring = "  Select WHL.FNHSysWHLocId, WHL.FTWHLocCode"
        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG As WHL WITH(NOLOCK) INNER Join"
        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG As WH WITH(NOLOCK) On WHL.FNHSysWHFGId = WH.FNHSysWHFGId"
        cmdstring &= vbCrLf & "  Where WH.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ""
        cmdstring &= vbCrLf & "  And  (WHL.FTStateActive = '1') AND (WH.FTStateActive = '1')"
        cmdstring &= vbCrLf & "  And LEN(WHL.FTWHLocCode) > 5"
        cmdstring &= vbCrLf & "  And WH.FTWHFGCode='" & HI.UL.ULF.rpQuoted(wh) & "'"
        cmdstring &= vbCrLf & "  And  Left(WHL.FTWHLocCode, Len(WHL.FTWHLocCode) - 3)='" & HI.UL.ULF.rpQuoted(grploc) & "'"
        cmdstring &= vbCrLf & "  ORDER BY   WHL.FTWHLocCode DESC   "

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In dt.Rows
            TotalLoc = TotalLoc + 1
            Dim Uloc As New ULocation()
            Uloc.Name = "Xloc" & R!FNHSysWHLocId.ToString
            Uloc.DataFill = False
            Uloc.LocationNo = R!FTWHLocCode.ToString
            Uloc.DataInfo = ""
            Uloc.LocationSystemId = Val(R!FNHSysWHLocId.ToString)
            Uloc.Size = New System.Drawing.Size(150, 40)
            Uloc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

            Uloc.Location = New System.Drawing.Point(9, StartLoc)


            StartLoc = StartLoc + 45

            Me.Controls.Add(Uloc)
            ListUloc.Add(Uloc)

            'AddHandler Uloc.Click, AddressOf Uloc_Ckick
            'AddHandler Uloc.olbdesc.Click, AddressOf UlocObject_Ckick
            'AddHandler Uloc.olbloc.Click, AddressOf UlocObject_Ckick

        Next

        If TotalLoc > 10 Then
            Me.Size = New System.Drawing.Size(170, 60 + (45 * TotalLoc))
        End If



        dt.Dispose()

    End Sub

    Private Sub Uloc_Ckick(sender As Object, e As System.EventArgs)

        Dim Uloc As ULocation = CType(sender, ULocation)
        If Uloc.DataFill Then
            Uloc.DataFill = False
            Uloc.DataInfo = ""
        Else
            Uloc.DataFill = True
            Uloc.DataInfo = "XXXXX"

        End If

    End Sub

    Private Sub UlocObject_Ckick(sender As Object, e As System.EventArgs)

        Dim Uloc As ULocation = CType(DirectCast(sender, System.Windows.Forms.Control).Parent.Parent, ULocation)
        If Uloc.DataFill Then
            Uloc.DataFill = False
            Uloc.DataInfo = ""
        Else
            Uloc.DataFill = True
            Uloc.DataInfo = "XXXXX"

        End If

    End Sub

    Private Sub olbheadloc_Click(sender As Object, e As EventArgs) Handles olbheadloc.Click
        Select Case DataInOut
            Case StateInOut.StateStop
                DataInOut = StateInOut.StateIn
                olbdatestate.BackColor = System.Drawing.Color.FromArgb(128, 255, 128)
                olbdatestate.ForeColor = System.Drawing.Color.Blue
                olbdatestate.Text = "In"
            Case StateInOut.StateIn
                DataInOut = StateInOut.StateOut
                olbdatestate.BackColor = System.Drawing.Color.FromArgb(255, 224, 192)
                olbdatestate.ForeColor = System.Drawing.Color.Blue
                olbdatestate.Text = "Out"


            Case StateInOut.StateOut
                DataInOut = StateInOut.StateStop
                olbdatestate.BackColor = System.Drawing.Color.Blue
                olbdatestate.ForeColor = System.Drawing.Color.Yellow
                olbdatestate.Text = "Press for Start"
        End Select
    End Sub
End Class

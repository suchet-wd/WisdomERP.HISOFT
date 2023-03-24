Imports System.Drawing
Imports System.Threading

Public Class LCDDisplay_bak

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Line1 = ""
        Me.Line2 = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT TOP 1  M.FTComputerName, M.FNHSysUnitSectId, M.FNHSysUnitSectIdTo"
        _Qry &= vbCrLf & " , ISNULL(S1.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & " , ISNULL(S2.FTUnitSectCode,'') AS FTUnitSectCodeTo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigCom AS M WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S2 WITH(NOLOCK)  ON M.FNHSysUnitSectIdTo = S2.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S1 WITH(NOLOCK)  ON M.FNHSysUnitSectId = S1.FNHSysUnitSectId "
        _Qry &= vbCrLf & " WHERE M.FTComputerName='" & HI.UL.ULF.rpQuoted(System.Environment.MachineName) & "'"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        For Each R As DataRow In _dt.Rows

            If R!FTUnitSectCode.ToString = "" Or R!FTUnitSectCodeTo.ToString = "" Or R!FTUnitSectCode.ToString = "-" Or R!FTUnitSectCodeTo.ToString = "-" Then

                Me.Line1 = ""

                If R!FTUnitSectCode.ToString = "" Or R!FTUnitSectCode.ToString = "-" Then
                    Me.Line2 = R!FTUnitSectCodeTo.ToString
                    Me.SysLine2 = Val(R!FNHSysUnitSectIdTo.ToString)
                Else
                    Me.Line2 = R!FTUnitSectCode.ToString
                    Me.SysLine2 = Val(R!FNHSysUnitSectId.ToString)
                End If

                Me.opnl1.Visible = False
                Me.opnl0.Visible = False

            Else
                Me.Line1 = R!FTUnitSectCode.ToString
                Me.SysLine1 = Val(R!FNHSysUnitSectId.ToString)
                Me.Line2 = R!FTUnitSectCodeTo.ToString
                Me.SysLine2 = Val(R!FNHSysUnitSectIdTo.ToString)
            End If

        Next

        _Qry = "SELECT " & HI.UL.ULDate.FormatDateDB & " "

        TransactionDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

    End Sub

#Region "Property"

#End Region
    Private _TransactionDate As String = ""
    Property TransactionDate As String
        Get
            Return _TransactionDate
        End Get
        Set(value As String)
            _TransactionDate = value
        End Set
    End Property

    Private _Line1 As String = ""
    Property Line1 As String
        Get
            Return _Line1
        End Get
        Set(value As String)
            _Line1 = value
        End Set
    End Property

    Private _SysLine1 As Integer = 0
    Property SysLine1 As Integer
        Get
            Return _SysLine1
        End Get
        Set(value As Integer)
            _SysLine1 = value
        End Set
    End Property

    Private _Line2 As String = ""
    Property Line2 As String
        Get
            Return _Line2
        End Get
        Set(value As String)
            _Line2 = value
        End Set
    End Property

    Private _SysLine2 As Integer = 0
    Property SysLine2 As Integer
        Get
            Return _SysLine2
        End Get
        Set(value As Integer)
            _SysLine2 = value
        End Set
    End Property

    Private Sub LCDDisplay_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.otmline1.Enabled = False
            Me.otmline2.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LCDDisplay_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub LCDDisplay_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized


        If Me.Line1 <> "" Then
            Me.otmline1.Enabled = True
            Dim _Theard1 As New Thread(AddressOf CheckStateLine1)
            _Theard1.Start()
        End If

        If Me.Line2 <> "" Then
            Me.otmline2.Enabled = True
            Dim _Theard2 As New Thread(AddressOf CheckStateLine2)
            _Theard2.Start()
        End If

        olbsline.Text = Me.Line1
        olbeline.Text = Me.Line2

    End Sub


    Private Sub LCDDisplay_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim _MeHeight As Integer = Me.Height
        Dim _MeWidth As Integer = Me.Width
        Dim _P1 As Integer = 80
        Dim _P2 As Integer = 85
        Dim _P3 As Integer = 167

        Dim _PM1 As Integer = 49

        Dim _LineWidth As Integer = 0
        Dim _CaptionWidth As Integer = 0

        _P1 = _MeHeight * 0.095
        _P2 = _MeHeight * 0.1001
        _P3 = _MeHeight * 0.1911

        _PM1 = (_P2 * 0.57647)

        If Me.opnl1.Visible And Me.opnl0.Visible Then
            Me.opnl1.Width = ((_MeWidth - 8) / 2)
        End If

        '-----------Start Set Line 1
        opnsline.Height = _P1

        opnstarget.Height = _P2
        opnsprod.Height = _P2
        opnsper.Height = _P2

        opnstargetqty.Height = _P3
        opnsprodqty.Height = _P3
        _LineWidth = opneline.Width
        _CaptionWidth = _LineWidth / 2

        opnstarget1.Width = _CaptionWidth
        opnstarget2.Width = (_CaptionWidth * 0.6474)
        opnsprod1.Width = _CaptionWidth
        opnstargetqty1.Width = _CaptionWidth
        opnsprodqty1.Width = _CaptionWidth
        opnsper1.Width = _CaptionWidth
        opnsperqty1.Width = _CaptionWidth

        opnstarget1_1.Height = _PM1
        opnstarget2_1.Height = _PM1
        opnsprod1_1.Height = _PM1
        opnsprod2_1.Height = _PM1
        opnsper1_1.Height = _PM1
        opnsper2_1.Height = _PM1

        '-----------End Set Line 1

        '-----------Start Set Line 2
        opneline.Height = _P1

        opnetarget.Height = _P2
        opneprod.Height = _P2
        opneper.Height = _P2

        opnetargetqty.Height = _P3
        opneprodqty.Height = _P3

        opnetarget1.Width = _CaptionWidth
        opnetarget2.Width = (_CaptionWidth * 0.6474)
        opneprod1.Width = _CaptionWidth
        opnetargetqty1.Width = _CaptionWidth
        opneprodqty1.Width = _CaptionWidth
        opneper1.Width = _CaptionWidth
        opneperqty1.Width = _CaptionWidth

        opnetarget1_1.Height = _PM1
        opnetarget2_1.Height = _PM1
        opneprod1_1.Height = _PM1
        opneprod2_1.Height = _PM1
        opneper1_1.Height = _PM1
        opneper2_1.Height = _PM1
        '-----------End Set Line 2

        'Start Set Font -
        Dim FontHeaderLineSize As Integer = (_P1 * 0.5625)
        Dim FontHeaderP2 As Integer = (opnetarget1_1.Height * 0.40816)
        Dim FontHeaderP3 As Integer = (_P3 * 0.527607)
        Dim FontHeaderP4 As Integer = (opneperqty.Height * 0.306748)

        Dim FontHeaderP5 As Integer = (opnetarget3_1.Height * 0.617284)
        Dim FontHeaderP6 As Integer = (opnetarget1_2.Height * 0.4375)

        Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)
        Dim FHeaderP2 As New Font("Tahoma", FontHeaderP2, FontStyle.Bold)
        Dim FHeaderP3 As New Font("Tahoma", FontHeaderP3, FontStyle.Bold)
        Dim FHeaderP4 As New Font("Tahoma", FontHeaderP4, FontStyle.Bold)
        Dim FHeaderP5 As New Font("Tahoma", FontHeaderP5, FontStyle.Bold)
        Dim FHeaderP6 As New Font("Tahoma", FontHeaderP6, FontStyle.Bold)

        olbsline.Font = FHeader
        olbeline.Font = FHeader

        opnstarget1_1.Font = FHeaderP2
        opnstarget1_2.Font = FHeaderP6
        opnstarget2_1.Font = FHeaderP2
        opnstarget3_1.Font = FHeaderP5

        opnetarget1_1.Font = FHeaderP2
        opnetarget1_2.Font = FHeaderP6
        opnetarget2_1.Font = FHeaderP2
        opnetarget3_1.Font = FHeaderP5

        opnsprod1_1.Font = FHeaderP2
        opnsprod1_2.Font = FHeaderP6
        opnsprod2_1.Font = FHeaderP2
        opnsprod1_2.Font = FHeaderP6

        opneprod1_1.Font = FHeaderP2
        opneprod1_2.Font = FHeaderP6
        opneprod2_1.Font = FHeaderP2
        opneprod2_2.Font = FHeaderP6

        opnsper1_1.Font = FHeaderP2
        opnsper1_2.Font = FHeaderP6
        opnsper2_1.Font = FHeaderP2
        opnsper2_2.Font = FHeaderP6

        opneper1_1.Font = FHeaderP2
        opneper1_2.Font = FHeaderP6
        opneper2_1.Font = FHeaderP2
        opneper2_2.Font = FHeaderP6

        opnstargetqty1_1.Font = FHeaderP3
        opnstargetqty2_1.Font = FHeaderP3

        opnetargetqty1_1.Font = FHeaderP3
        opnetargetqty2_1.Font = FHeaderP3

        opnsprodqty1_1.Font = FHeaderP3
        opnsprodqty2_1.Font = FHeaderP3

        opneprodqty1_1.Font = FHeaderP3
        opneprodqty2_1.Font = FHeaderP3

        opnsperqty1_1.Font = FHeaderP4
        opnsperqty2_1.Font = FHeaderP4

        opneperqty1_1.Font = FHeaderP4
        opneperqty2_1.Font = FHeaderP4
        'End Set Font

    End Sub

    Private Sub otmline1_Tick(sender As Object, e As EventArgs) Handles otmline1.Tick
        Dim _Theard As New Thread(AddressOf CheckStateLine1)
        _Theard.Start()
    End Sub

    Private Delegate Sub DelegateStateLine1()
    Private Sub CheckStateLine1()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateStateLine1(AddressOf CheckStateLine1), New Object() {})
        Else
            Try
                Dim _TotalTarget As Integer = 0
                Dim _dttime As DataTable
                Dim _TimeServer As String = ""
                Dim _Qry As String
                _Qry = "SELECT TOP 1 FNTarget "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & ""
                _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "

                _TotalTarget = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))

                If _TotalTarget > 0 Then
                    opnstargetqty1_1.Text = _TotalTarget.ToString


                    _Qry = " SELECT   "
                    _Qry &= vbCrLf & " 	ROW_NUMBER() Over (Order By A.FTStartTime ) AS FNHour"
                    _Qry &= vbCrLf & " 	,A.FTStartTime"
                    _Qry &= vbCrLf & " 	,A.FTEndTime"
                    _Qry &= vbCrLf & " 	,DateDiff(MINUTE,A.FTStartTime,A.FTEndTime) AS FNTotalMinute"
                    _Qry &= vbCrLf & " 	,CONVERT(varchar(5),Getdate(),114) AS FTTimeServer"
                    _Qry &= vbCrLf & " 	,A.FNHSysPeriodOfTimeId "
                    _Qry &= vbCrLf & " 	,ISNULL(B.FNConfigTime,0) AS FNConfigBreakTime"
                    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN ( SELECT FNHSysPeriodOfTimeId,FNConfigTime"
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTime AS C WITH(NOLOCK)"
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
                    _Qry &= vbCrLf & " 	 ) AS B ON A.FNHSysPeriodOfTimeId = B.FNHSysPeriodOfTimeId "
                    _Qry &= vbCrLf & "  WHERE FTStateActive ='1'"
                    _Qry &= vbCrLf & "  ORDER BY FTStartTime"

                    _dttime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    If _dttime.Rows.Count > 0 Then
                        _TimeServer = _dttime.Rows(0)!FTTimeServer.ToString

                        '------ Start Target
                        Dim _TotalTime As Integer = 0
                        Dim _TimeTotalHour As Integer = 0
                        Dim _TargetTotalHour As Integer = 0
                        Dim _TimeHour As Integer = 0
                        Dim _TargetHour As Integer = 0
                        Dim _TaktTime As Integer = 0
                        Dim _Hour As Integer = 0
                        Dim _StartTime As String = ""
                        Dim _EndTime As String = ""

                        For Each R As DataRow In _dttime.Rows
                            _TotalTime = _TotalTime + (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))
                        Next

                        _TaktTime = (_TotalTime * 60) / _TotalTarget

                        For Each R As DataRow In _dttime.Select("FTStartTime<='" & _TimeServer & "' AND  FTEndTime>='" & _TimeServer & "'")
                            _Hour = Val(R!FNHour)
                            _TimeHour = (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))
                            _StartTime = R!FTStartTime.ToString
                            _EndTime = R!FTEndTime.ToString
                            Exit For
                        Next
                        _TargetHour = ((_TimeHour * 60) / _TaktTime)

                        For Each R As DataRow In _dttime.Select("FNHour<=" & Val(_Hour) & " ")

                            _TimeTotalHour = _TimeTotalHour + (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))

                        Next

                        _dttime.Dispose()

                        _TargetTotalHour = ((_TimeTotalHour * 60) / _TaktTime)

                        opnstarget3_1.Text = _Hour.ToString
                        opnstargetqty2_1.Text = _TargetHour.ToString

                        '------ End Target

                        '------ Start Production------
                        Dim _dtprod As DataTable
                        Dim _TotalProd As Integer = 0
                        Dim _Prod As Integer = 0

                        '_Qry = "    SELECT    FDInsDate AS FTDateScan"
                        '_Qry &= vbCrLf & "   , LefT(FTInsTime,5)  AS FTTimeScan"
                        '_Qry &= vbCrLf & "   ,SUM(FNScanQuantity) AS FNScanQuantity"
                        '_Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A WITH(NOLOCK)"
                        '_Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
                        '_Qry &= vbCrLf & "   AND FDInsDate ='" & Me.TransactionDate & "'"
                        '_Qry &= vbCrLf & "   GROUP BY FDInsDate, LefT(FTInsTime,5)"


                        _Qry = "    SELECT    CASE WHEN ISNULL(FDUpdDate,'')='' THEN  FDInsDate ELSE  FDUpdDate  END  AS FTDateScan"
                        _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(FTUpdTime,'')='' THEN  LefT(FTInsTime,5) ELSE  LefT(FTUpdTime,5)  END  AS FTTimeScan"
                        _Qry &= vbCrLf & "   ,SUM(FNScanQuantity) AS FNScanQuantity"
                        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A WITH(NOLOCK)"
                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
                        _Qry &= vbCrLf & "   AND CASE  WHEN ISNULL(FDUpdDate,'')='' THEN  FDInsDate ELSE  FDUpdDate  END ='" & Me.TransactionDate & "'"
                        _Qry &= vbCrLf & "   GROUP BY CASE WHEN ISNULL(FDUpdDate,'')='' THEN  FDInsDate ELSE  FDUpdDate  END, CASE WHEN ISNULL(FTUpdTime,'')='' THEN  LefT(FTInsTime,5) ELSE  LefT(FTUpdTime,5) END"


                        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                        For Each R As DataRow In _dtprod.Rows
                            _TotalProd = _TotalProd + Val(R!FNScanQuantity)
                        Next

                        For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' AND  FTTimeScan<='" & _EndTime & "'")
                            _Prod = _Prod + Val(R!FNScanQuantity)
                        Next

                        _dtprod.Dispose()

                        opnsprodqty1_1.Text = _TotalProd.ToString
                        opnsprodqty2_1.Text = _Prod.ToString

                        If _TotalTarget > 0 Then
                            opnsperqty1_1.Text = Format((_TotalProd / _TotalTarget) * 100, "0.00")
                        Else
                            opnsperqty1_1.Text = "0.00"
                        End If

                        If _TargetHour > 0 Then
                            opnsperqty2_1.Text = Format((_Prod / _TargetHour) * 100, "0.00")
                        Else
                            opnsperqty2_1.Text = "0.00"
                        End If

                        '------ Start Production------

                    Else

                        opnstarget3_1.Text = "0"
                        opnstargetqty1_1.Text = "0"
                        opnstargetqty2_1.Text = "0"
                        opnsprodqty1_1.Text = "0"
                        opnsprodqty2_1.Text = "0"
                        opnsperqty1_1.Text = "0.00"
                        opnsperqty2_1.Text = "0.00"

                    End If

                Else

                    opnstarget3_1.Text = "0"
                    opnstargetqty1_1.Text = "0"
                    opnstargetqty2_1.Text = "0"
                    opnsprodqty1_1.Text = "0"
                    opnsprodqty2_1.Text = "0"
                    opnsperqty1_1.Text = "0.00"
                    opnsperqty2_1.Text = "0.00"

                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub otmline2_Tick(sender As Object, e As EventArgs) Handles otmline2.Tick
        Dim _Theard As New Thread(AddressOf CheckStateLine2)
        _Theard.Start()
    End Sub

    Private Delegate Sub DelegateStateLine2()
    Private Sub CheckStateLine2()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateStateLine2(AddressOf CheckStateLine2), New Object() {})
        Else
            Try

                Dim _TotalTarget As Integer = 0
                Dim _dttime As DataTable
                Dim _TimeServer As String = ""
                Dim _Qry As String

                _Qry = "SELECT TOP 1 FNTarget "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & ""
                _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "

                _TotalTarget = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))

                If _TotalTarget > 0 Then
                    opnetargetqty1_1.Text = _TotalTarget.ToString

                    _Qry = " SELECT   "
                    _Qry &= vbCrLf & " 	ROW_NUMBER() Over (Order By A.FTStartTime ) AS FNHour"
                    _Qry &= vbCrLf & " 	,A.FTStartTime"
                    _Qry &= vbCrLf & " 	,A.FTEndTime"
                    _Qry &= vbCrLf & " 	,DateDiff(MINUTE,A.FTStartTime,A.FTEndTime) AS FNTotalMinute"
                    _Qry &= vbCrLf & " 	,CONVERT(varchar(5),Getdate(),114) AS FTTimeServer"
                    _Qry &= vbCrLf & " 	,A.FNHSysPeriodOfTimeId "
                    _Qry &= vbCrLf & " 	,ISNULL(B.FNConfigTime,0) AS FNConfigBreakTime"
                    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN ( SELECT FNHSysPeriodOfTimeId,FNConfigTime"
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTime AS C WITH(NOLOCK)"
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
                    _Qry &= vbCrLf & " 	 ) AS B ON A.FNHSysPeriodOfTimeId = B.FNHSysPeriodOfTimeId "
                    _Qry &= vbCrLf & "  WHERE FTStateActive ='1'"
                    _Qry &= vbCrLf & "  ORDER BY FTStartTime"

                    _dttime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    If _dttime.Rows.Count > 0 Then
                        _TimeServer = _dttime.Rows(0)!FTTimeServer.ToString

                        '------ Start Target
                        Dim _TotalTime As Integer = 0
                        Dim _TimeTotalHour As Integer = 0
                        Dim _TargetTotalHour As Integer = 0
                        Dim _TimeHour As Integer = 0
                        Dim _TargetHour As Integer = 0
                        Dim _TaktTime As Integer = 0
                        Dim _Hour As Integer = 0
                        Dim _StartTime As String = ""
                        Dim _EndTime As String = ""

                        For Each R As DataRow In _dttime.Rows
                            _TotalTime = _TotalTime + (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))
                        Next

                        _TaktTime = (_TotalTime * 60) / _TotalTarget

                        For Each R As DataRow In _dttime.Select("FTStartTime<='" & _TimeServer & "' AND FTEndTime>='" & _TimeServer & "'")
                            _Hour = Val(R!FNHour)
                            _TimeHour = (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))
                            _StartTime = R!FTStartTime.ToString
                            _EndTime = R!FTEndTime.ToString
                            Exit For
                        Next
                        _TargetHour = ((_TimeHour * 60) / _TaktTime)

                        For Each R As DataRow In _dttime.Select("FNHour<=" & Val(_Hour) & " ")

                            _TimeTotalHour = _TimeTotalHour + (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))

                        Next

                        _TargetTotalHour = ((_TimeTotalHour * 60) / _TaktTime)

                        opnetarget3_1.Text = _Hour.ToString
                        opnetargetqty2_1.Text = _TargetHour.ToString

                        '------ End Target

                        '------ Start Production------
                        Dim _dtprod As DataTable
                        Dim _TotalProd As Integer = 0
                        Dim _Prod As Integer = 0

                        _Qry = "    SELECT    CASE WHEN ISNULL(FDUpdDate,'')='' THEN  FDInsDate ELSE  FDUpdDate  END  AS FTDateScan"
                        _Qry &= vbCrLf & "   ,CASE WHEN ISNULL(FTUpdTime,'')='' THEN  LefT(FTInsTime,5) ELSE  LefT(FTUpdTime,5)  END  AS FTTimeScan"
                        _Qry &= vbCrLf & "   ,SUM(FNScanQuantity) AS FNScanQuantity"
                        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A WITH(NOLOCK)"
                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
                        _Qry &= vbCrLf & "   AND CASE  WHEN ISNULL(FDUpdDate,'')='' THEN  FDInsDate ELSE  FDUpdDate  END ='" & Me.TransactionDate & "'"
                        _Qry &= vbCrLf & "   GROUP BY CASE WHEN ISNULL(FDUpdDate,'')='' THEN  FDInsDate ELSE  FDUpdDate  END, CASE WHEN ISNULL(FTUpdTime,'')='' THEN  LefT(FTInsTime,5) ELSE  LefT(FTUpdTime,5) END"

                        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                        For Each R As DataRow In _dtprod.Rows
                            _TotalProd = _TotalProd + Val(R!FNScanQuantity)
                        Next

                        For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' AND  FTTimeScan<='" & _EndTime & "'")
                            _Prod = _Prod + Val(R!FNScanQuantity)
                        Next

                        _dtprod.Dispose()

                        opneprodqty1_1.Text = _TotalProd.ToString
                        opneprodqty2_1.Text = _Prod.ToString

                        If _TotalTarget > 0 Then
                            opneperqty1_1.Text = Format((_TotalProd / _TotalTarget) * 100, "0.00")
                        Else
                            opneperqty1_1.Text = "0.00"
                        End If

                        If _TargetHour > 0 Then
                            opneperqty2_1.Text = Format((_Prod / _TargetHour) * 100, "0.00")
                        Else
                            opneperqty2_1.Text = "0.00"
                        End If

                        '------ Start Production------

                    Else

                        opnstarget3_1.Text = "0"
                        opnstargetqty1_1.Text = "0"
                        opnstargetqty2_1.Text = "0"
                        opnsprodqty1_1.Text = "0"
                        opnsprodqty2_1.Text = "0"
                        opnsperqty1_1.Text = "0.00"
                        opnsperqty2_1.Text = "0.00"

                    End If
                Else

                    opnetarget3_1.Text = "0"
                    opnetargetqty1_1.Text = "0"
                    opnetargetqty2_1.Text = "0"
                    opneprodqty1_1.Text = "0"
                    opneprodqty2_1.Text = "0"
                    opneperqty1_1.Text = "0.00"
                    opneperqty2_1.Text = "0.00"

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

End Class
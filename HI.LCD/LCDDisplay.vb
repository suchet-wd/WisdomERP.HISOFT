Imports System.Drawing
Imports System.Threading

Public Class LCDDisplay

    Private _SystemFilePath As Boolean = False
    Property StateWindowsUser As Boolean
        Get
            Return _SystemFilePath
        End Get
        Set(value As Boolean)
            _SystemFilePath = value
        End Set
    End Property

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Line1 = ""
        Me.Line2 = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable



        '_Qry = "SELECT TOP 1  M.FTComputerName, M.FNHSysUnitSectId, M.FNHSysUnitSectIdTo"
        '_Qry &= vbCrLf & " , ISNULL(S1.FTUnitSectCode,'') AS FTUnitSectCode"
        '_Qry &= vbCrLf & " , ISNULL(S2.FTUnitSectCode,'') AS FTUnitSectCodeTo"
        '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigCom AS M WITH(NOLOCK) LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S2 WITH(NOLOCK)  ON M.FNHSysUnitSectIdTo = S2.FNHSysUnitSectId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S1 WITH(NOLOCK)  ON M.FNHSysUnitSectId = S1.FNHSysUnitSectId "
        '_Qry &= vbCrLf & " WHERE M.FTComputerName='" & HI.UL.ULF.rpQuoted(System.Environment.MachineName) & "'"

        '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        Dim _cmd As String = ""
        _cmd = " SELECT TOP 1 [COLUMN_NAME]"
        _cmd &= vbCrLf & " FROM INFORMATION_SCHEMA.COLUMNS"
        _cmd &= vbCrLf & "   WHERE [TABLE_NAME] = 'TPRODLCDConfigCom'"
        _cmd &= vbCrLf & "   AND [COLUMN_NAME] = 'FTUserWindow'"

        Me.StateWindowsUser = (HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_PROD) <> "")

        _Qry = "SELECT TOP 1  M.FTComputerName, M.FNHSysUnitSectId, M.FNHSysUnitSectIdTo"
        _Qry &= vbCrLf & " , ISNULL(S1.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & " , ISNULL(S2.FTUnitSectCode,'') AS FTUnitSectCodeTo"
        _Qry &= vbCrLf & " , ISNULL(S1.FNHSysIncenFormulaId,0) AS FNHSysIncenFormulaId"
        _Qry &= vbCrLf & " , ISNULL(S2.FNHSysIncenFormulaId,0) AS FNHSysIncenFormulaIdTo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigCom AS M WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S2 WITH(NOLOCK)  ON M.FNHSysUnitSectIdTo = S2.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S1 WITH(NOLOCK)  ON M.FNHSysUnitSectId = S1.FNHSysUnitSectId "
        _Qry &= vbCrLf & " WHERE M.FTComputerName='" & HI.UL.ULF.rpQuoted(System.Environment.MachineName.ToUpper) & "'"

        If StateWindowsUser Then

            _Qry &= vbCrLf & " AND (M.FTUserWindow='" & HI.UL.ULF.rpQuoted(System.Environment.UserName.ToUpper) & "'  OR ISNULL(M.FTUserWindow,'')='') "
            _Qry &= vbCrLf & " ORDER BY  ISNULL(M.FTUserWindow,'') DESC "

        End If
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
            Me.ottime.Enabled = False
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

        Dim _Qry As String
        _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
        Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

        Me.ottime.Enabled = True
       
    End Sub

    Private Sub LCDDisplay_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim _MeHeight As Integer = Me.Height
        Dim _MeWidth As Integer = Me.Width
        Dim _P1 As Integer = 80
        Dim _P2 As Integer = 50
        Dim _P3 As Integer = 150
        Dim _P4 As Integer = 43

        Dim _PM1 As Integer = 49

        Dim _LineWidth As Integer = 0
        Dim _CaptionWidth As Integer = 0

        _P1 = _MeHeight * 0.095
        _P2 = _MeHeight * 0.073313
        _P3 = _MeHeight * 0.1711
        _P4 = _MeHeight * 0.06304

        _PM1 = (_P2 * 0.57647)

        If Me.opnl1.Visible And Me.opnl0.Visible Then
            Me.opnl1.Width = ((_MeWidth - 8) / 2)
            Me.opnhour.Location = New System.Drawing.Point((((_MeWidth) / 2)) - (Me.opnhour.Width / 2), 2)
        Else
            Me.opnhour.Location = New System.Drawing.Point(2, 2)
        End If

        Dim FontHourSize As Integer = (_P1 * 0.454545)
        Dim FHour As New Font("Tahoma", FontHourSize, FontStyle.Bold)
        Me.opnhour.Height = _P1
        Me.olbhour.Font = FHour

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

        opnsOrder.Height = _P4
        opnsStyle.Height = _P4
        opnsColor.Height = _P4
        opnsQty.Height = _P4

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


        opneOrder.Height = _P4
        opneStyle.Height = _P4
        opneColor.Height = _P4
        opneQty.Height = _P4
        '-----------End Set Line 2

        'Start Set Font -
        Dim FontHeaderLineSize As Integer = (_P1 * 0.5625)
        Dim FontHeaderP2 As Integer = (opnetarget1_1.Height * 0.40816)
        Dim FontHeaderP3 As Integer = (_P3 * 0.757607)
        Dim FontHeaderP4 As Integer = (opneperqty.Height * 0.456748)

        Dim FontHeaderP5 As Integer = (opnetarget3_1.Height * 0.887284)
        Dim FontHeaderP6 As Integer = (opnetarget1_2.Height * 0.4375)
        Dim FontHeaderP7 As Integer = ((_P4 / 2) * 0.55)

        Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)
        Dim FHeaderP2 As New Font("Tahoma", FontHeaderP2, FontStyle.Bold)
        Dim FHeaderP3 As New Font("Tahoma", FontHeaderP3, FontStyle.Bold)
        Dim FHeaderP4 As New Font("Tahoma", FontHeaderP4, FontStyle.Bold)
        Dim FHeaderP5 As New Font("Tahoma", FontHeaderP5, FontStyle.Bold)
        Dim FHeaderP6 As New Font("Tahoma", FontHeaderP6, FontStyle.Bold)
        Dim FHeaderP7 As New Font("Tahoma", FontHeaderP7, FontStyle.Bold)


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

        FTOrderNo_lbl.Font = FHeaderP7
        FTOrderNo.Font = FHeaderP7

        FDShipDate_lbl.Font = FHeaderP7
        FDShipDate.Font = FHeaderP7

        FTStyleNo.Font = FHeaderP7
        FTStyleNo_lbl.Font = FHeaderP7

        FTPORef.Font = FHeaderP7
        FTPORef_lbl.Font = FHeaderP7

        FTColorway.Font = FHeaderP7
        FTColorway_lbl.Font = FHeaderP7

        FTSizeCode.Font = FHeaderP7
        FTSizeCode_lbl.Font = FHeaderP7

        FNOrderQuantity.Font = FHeaderP7
        FNOrderQuantity_lbl.Font = FHeaderP7

        FNProdQty.Font = FHeaderP7
        FNProdQty_lbl.Font = FHeaderP7


        FTOrderNo2_lbl.Font = FHeaderP7
        FTOrderNo2.Font = FHeaderP7

        FDShipDate2_lbl.Font = FHeaderP7
        FDShipDate2.Font = FHeaderP7

        FTStyleNo2.Font = FHeaderP7
        FTStyleNo2_lbl.Font = FHeaderP7

        FTPORef2.Font = FHeaderP7
        FTPORef2_lbl.Font = FHeaderP7

        FTColorway2.Font = FHeaderP7
        FTColorway2_lbl.Font = FHeaderP7

        FTSizeCode2.Font = FHeaderP7
        FTSizeCode2_lbl.Font = FHeaderP7

        FNOrderQuantity2.Font = FHeaderP7
        FNOrderQuantity2_lbl.Font = FHeaderP7

        FNProdQty2.Font = FHeaderP7
        FNProdQty2_lbl.Font = FHeaderP7
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
                Dim _dttimeplan As DataTable
                Dim _TimeWorlPlanMinute As Integer = 0

                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & ""
                _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "
                _dttimeplan = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                For Each R As DataRow In _dttimeplan.Rows
                    _TotalTarget = Val(R!FNTarget.ToString)

                    If R!FTWorkTime.ToString <> "" Then

                        Try
                            _TimeWorlPlanMinute = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * 60) + (Val(R!FTWorkTime.ToString.Split(":")(1))))
                        Catch ex As Exception

                        End Try
                    End If
                    Exit For
                Next

                _dttimeplan.Dispose()
                ' _TotalTarget = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))

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

                        If _TimeWorlPlanMinute <= 0 Then
                            For Each R As DataRow In _dttime.Rows
                                _TotalTime = _TotalTime + (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))
                            Next
                        Else
                            _TotalTime = _TimeWorlPlanMinute
                        End If


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


                        _Qry = "    SELECT    FDScanDate  AS FTDateScan"
                        _Qry &= vbCrLf & "   ,FDScanTime  AS FTTimeScan"
                        _Qry &= vbCrLf & "   ,SUM(FNScanQuantity) AS FNScanQuantity"
                        _Qry &= vbCrLf & "    FROM  "  '[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS A WITH(NOLOCK)

                        _Qry &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        _Qry &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        _Qry &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        _Qry &= vbCrLf & "    UNION "
                        _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , '' ,'', B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo ) AS A  "


                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
                        _Qry &= vbCrLf & "   AND FDScanDate ='" & Me.TransactionDate & "'"
                        _Qry &= vbCrLf & "   GROUP BY FDScanDate, FDScanTime"


                        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                        For Each R As DataRow In _dtprod.Rows
                            _TotalProd = _TotalProd + Val(R!FNScanQuantity)
                        Next

                        'For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' AND  FTTimeScan<='" & _EndTime & "'")
                        '    _Prod = _Prod + Val(R!FNScanQuantity)
                        'Next

                        If _EndTime <> "" And _EndTime <= Microsoft.VisualBasic.Left(Me.olbhour.Text, 5) And Val(_Hour) >= 8 Then
                            For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' ")
                                _Prod = _Prod + Val(R!FNScanQuantity)
                            Next
                        Else
                            For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' AND  FTTimeScan<='" & _EndTime & "'")
                                _Prod = _Prod + Val(R!FNScanQuantity)
                            Next
                        End If

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


                        '-------New Info ------------
                        Dim _DateNow As String = _TransactionDate

                        Dim _Cmd As String = ""
                        Dim _oDt As DataTable
                        Dim _QtyOrder As Double = 0
                        Dim _QtyPRO As Double = 0
                        Dim _OrderNo As String = ""

                        _Cmd = "      SELECT  top 1  P.FDScanDate , P.FDScanTime, A.FTOrderNo, A.FNHSysStyleId, A.FTPORef , S.FTStyleCode, P.FTColorway, P.FTSizeBreakDown"
                        _Cmd &= vbCrLf & ", CASE WHEN Isdate(B.FDShipDate) = 1 Then Convert(varchar(10),Convert(datetime,B.FDShipDate) ,103) ELSE '' END AS FDShipDate"
                        _Cmd &= vbCrLf & "FROM   " '[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS P  WITH(NOLOCK)   RIGHT OUTER JOIN

                        _Cmd &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        _Cmd &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        _Cmd &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        _Cmd &= vbCrLf & "    UNION "
                        _Cmd &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , P.FTOrderNo ,P.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                        _Cmd &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                        _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                        _Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo  ) AS P  RIGHT OUTER JOIN"


                        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo ON P.FTOrderNo = A.FTOrderNo AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                        _Cmd &= vbCrLf & "        WHERE(FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                        _Cmd &= vbCrLf & "and P.FDScanDate ='" & _DateNow & "'     "
                        _Cmd &= vbCrLf & "GROUP BY P.FDScanDate , P.FDScanTime, A.FTOrderNo, A.FNHSysStyleId, A.FTPORef, B.FDShipDate, S.FTStyleCode, P.FTColorway, P.FTSizeBreakDown"
                        _Cmd &= vbCrLf & "ORDER BY P.FDScanDate , P.FDScanTime DESC"
                        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                        For Each R As DataRow In _oDt.Rows
                            _OrderNo = R!FTOrderNo.ToString
                            FTStyleNo.Text = R!FTStyleCode.ToString
                            FTColorway.Text = R!FTColorway.ToString
                            FTSizeCode.Text = R!FTSizeBreakDown.ToString
                            ' FDShipDate.Text = R!FDShipDate.ToString
                            FTPORef.Text = R!FTPORef.ToString

                        Next

                        '_Cmd = "SELECT     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
                        '_Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
                        '_Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty ,(SUM(A.FNMajorQty)+SUM(A.FNMinorQty)) AS FNTotalDefect"
                        '_Cmd &= vbCrLf & "		, B.FTStyleCode, O.FTPORef , sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        '_Cmd &= vbCrLf & "  ,((SUM(A.FNMajorQty)+SUM(A.FNMinorQty))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"
                        '_Cmd &= vbCrLf & " ,(Select Case when isdate(min(FDShipDate)) = 1 Then convert(varchar(10),convert(datetime,min(FDShipDate)),103) Else '' End "
                        '_Cmd &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub WITH(NOLOCK)"
                        '_Cmd &= vbCrLf & "  Where FTOrderNo = A.FTOrderNo ) AS FDShipDate "
                        '_Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
                        '_Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & _DateNow & "')"
                        '_Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1))
                        '_Cmd &= vbCrLf & "group by   A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   B.FTStyleCode, O.FTPORef"
                        '_Cmd &= vbCrLf & "Order by A.FDQADate"
                        '_oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        'For Each R As DataRow In _oDt.Rows
                        '    FTOrderNo.Text = R!FTOrderNo.ToString
                        '    ' FDShipDate.Text = R!FDShipDate.ToString
                        '    Exit For
                        'Next

                        _Cmd = "SELECT     sum(C.FNGrandQuantity) AS FNGrandQuantity		 "
                        _Cmd &= vbCrLf & "FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A LEFT outer JOIN"
                        _Cmd &= vbCrLf & "	 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)   ON A.FTOrderNo = B.FTOrderNo LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS C   WITH(NOLOCK)    ON     A.FTOrderNo = C.FTOrderNo"
                        _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                        _Cmd &= vbCrLf & "where A.FTOrderNo In (SELECT  Top 1  FTOrderNo "
                        _Cmd &= vbCrLf & "            FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan  WITH(NOLOCK)  "
                        _Cmd &= vbCrLf & "WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                        _Cmd &= vbCrLf & "and ISNULL(FDUpdDate,FDInsDate) ='" & _DateNow & "'"
                        _Cmd &= vbCrLf & "Order by ISNULL(FTUpdTime,FTInsTime) Desc)"
                        _Cmd &= vbCrLf & "group by A.FTOrderNo "
                        _QtyOrder = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
                        If _QtyOrder > 0 Then
                            FNOrderQuantity.Text = Format(Val(_QtyOrder.ToString), "#,#")
                        End If


                        _Cmd = "SELECT     sum(FNScanQuantity) AS FNScanQuantity"
                        _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan  WITH(NOLOCK) "
                        '_Cmd &= vbCrLf & "WHERE     (FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ") "
                        _Cmd &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                        _QtyPRO = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
                        If _QtyPRO > 0 Then
                            FNProdQty.Text = Format(Val(_QtyPRO.ToString), "#,#")
                        End If
                        '-------New Info -----------
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
                Dim _dttimeplan As DataTable
                Dim _TimeWorlPlanMinute As Integer = 0

                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') AS FTWorkTime "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & ""
                _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "
                _dttimeplan = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                For Each R As DataRow In _dttimeplan.Rows
                    _TotalTarget = Val(R!FNTarget.ToString)

                    If R!FTWorkTime.ToString <> "" Then

                        Try
                            _TimeWorlPlanMinute = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * 60) + (Val(R!FTWorkTime.ToString.Split(":")(1))))
                        Catch ex As Exception

                        End Try
                    End If
                    Exit For
                Next

                _dttimeplan.Dispose()
                ' _TotalTarget = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))

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

                        If _TimeWorlPlanMinute <= 0 Then
                            For Each R As DataRow In _dttime.Rows
                                _TotalTime = _TotalTime + (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))
                            Next
                        Else
                            _TotalTime = _TimeWorlPlanMinute
                        End If

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

                        _Qry = "    SELECT    FDScanDate  AS FTDateScan"
                        _Qry &= vbCrLf & "   ,FDScanTime  AS FTTimeScan"
                        _Qry &= vbCrLf & "   ,SUM(FNScanQuantity) AS FNScanQuantity"
                        _Qry &= vbCrLf & "    FROM  " '[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS A WITH(NOLOCK)
                        _Qry &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        _Qry &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        _Qry &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        _Qry &= vbCrLf & "    UNION "
                        _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , '' ,'', B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo ) AS A "
                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
                        _Qry &= vbCrLf & "   AND FDScanDate ='" & Me.TransactionDate & "'"
                        _Qry &= vbCrLf & "   GROUP BY FDScanDate,  FDScanTime"

                        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                        For Each R As DataRow In _dtprod.Rows
                            _TotalProd = _TotalProd + Val(R!FNScanQuantity)
                        Next

                        'For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' AND  FTTimeScan<='" & _EndTime & "'")
                        '    _Prod = _Prod + Val(R!FNScanQuantity)
                        'Next
                        If _EndTime <> "" And _EndTime <= Microsoft.VisualBasic.Left(Me.olbhour.Text, 5) And Val(_Hour) >= 8 Then
                            For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' ")
                                _Prod = _Prod + Val(R!FNScanQuantity)
                            Next
                        Else
                            For Each R As DataRow In _dtprod.Select("FTTimeScan>='" & _StartTime & "' AND  FTTimeScan<='" & _EndTime & "'")
                                _Prod = _Prod + Val(R!FNScanQuantity)
                            Next
                        End If

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

                        '--------New Info ---------------

                        Dim _DateNow As String = _TransactionDate

                        Dim _Cmd As String = ""
                        Dim _oDt As DataTable
                        Dim _QtyOrder As Double = 0
                        Dim _QtyPRO As Double = 0
                        Dim _OrderNo As String = ""

                        _Cmd = "      SELECT  top 1  P.FDScanDate , P.FDScanTime, A.FTOrderNo, A.FNHSysStyleId, A.FTPORef, S.FTStyleCode, P.FTColorway, P.FTSizeBreakDown"
                        _Cmd &= vbCrLf & " , CASE WHEN Isdate(B.FDShipDate) = 1 Then Convert(varchar(10),Convert(datetime,B.FDShipDate) ,103) ELSE '' END AS FDShipDate"

                        _Cmd &= vbCrLf & "FROM    " '[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS P WITH(NOLOCK)
                        _Cmd &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        _Cmd &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        _Cmd &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        _Cmd &= vbCrLf & "    UNION "
                        _Cmd &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , P.FTOrderNo ,P.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                        _Cmd &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                        _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                        _Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo  ) AS P  RIGHT OUTER JOIN"

                        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK)   LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)   ON A.FTOrderNo = B.FTOrderNo ON P.FTOrderNo = A.FTOrderNo AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                        _Cmd &= vbCrLf & "        WHERE(FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ")"
                        _Cmd &= vbCrLf & "and P.FDScanDate ='" & _DateNow & "'     "
                        _Cmd &= vbCrLf & "GROUP BY P.FDScanDate , P.FDScanTime, A.FTOrderNo, A.FNHSysStyleId, A.FTPORef, B.FDShipDate, S.FTStyleCode, P.FTColorway, P.FTSizeBreakDown"
                        _Cmd &= vbCrLf & "ORDER BY P.FDScanDate , P.FDScanTime DESC"



                        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                        For Each R As DataRow In _oDt.Rows
                            _OrderNo = R!FTOrderNo.ToString
                            FTStyleNo2.Text = R!FTStyleCode.ToString
                            FTColorway2.Text = R!FTColorway.ToString
                            FTSizeCode2.Text = R!FTSizeBreakDown.ToString
                            '  FDShipDate2.Text = R!FDShipDate.ToString
                            FTPORef2.Text = R!FTPORef.ToString

                        Next

                        '_Cmd = "SELECT     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
                        '_Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
                        '_Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty ,(SUM(A.FNMajorQty)+SUM(A.FNMinorQty)) AS FNTotalDefect"
                        '_Cmd &= vbCrLf & "		, B.FTStyleCode, O.FTPORef , sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        '_Cmd &= vbCrLf & "  ,((SUM(A.FNMajorQty)+SUM(A.FNMinorQty))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"
                        '_Cmd &= vbCrLf & ",(Select Case when isdate(min(FDShipDate)) = 1 Then convert(varchar(10),convert(datetime,min(FDShipDate)),103) Else '' End "
                        '_Cmd &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub WITH(NOLOCK)"
                        '_Cmd &= vbCrLf & "  Where FTOrderNo = A.FTOrderNo ) AS FDShipDate "
                        '_Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O ON A.FTOrderNo = O.FTOrderNo"
                        '_Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & _DateNow & "')"
                        '_Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2))
                        '_Cmd &= vbCrLf & "group by   A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   B.FTStyleCode, O.FTPORef"
                        '_Cmd &= vbCrLf & "Order by A.FDQADate"
                        '_oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        'For Each R As DataRow In _oDt.Rows
                        '    FTOrderNo2.Text = R!FTOrderNo.ToString
                        '    ' FDShipDate2.Text = R!FDShipDate.ToString
                        '    Exit For
                        'Next



                        _Cmd = "SELECT     sum(C.FNGrandQuantity) AS FNGrandQuantity		 "
                        _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK)  LEFT outer JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS C  WITH(NOLOCK)    ON     A.FTOrderNo = C.FTOrderNo"
                        _Cmd &= vbCrLf & "     LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)  ON A.FNHSysStyleId = S.FNHSysStyleId"
                        _Cmd &= vbCrLf & "where A.FTOrderNo In (SELECT  Top 1  FTOrderNo "
                        _Cmd &= vbCrLf & "            FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan  WITH(NOLOCK)  "
                        _Cmd &= vbCrLf & "WHERE(FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ")"
                        _Cmd &= vbCrLf & "and ISNULL(FDUpdDate,FDInsDate) ='" & _DateNow & "'"
                        _Cmd &= vbCrLf & "Order by ISNULL(FTUpdTime,FTInsTime) Desc)"
                        _Cmd &= vbCrLf & "group by A.FTOrderNo "
                        _QtyOrder = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
                        If _QtyOrder > 0 Then
                            FNOrderQuantity2.Text = Format(Val(_QtyOrder.ToString), "#,#")
                        End If

                        _Cmd = "SELECT     sum(FNScanQuantity) AS FNScanQuantity"
                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan  WITH(NOLOCK)  "
                        '_Cmd &= vbCrLf & "WHERE     (FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ") "
                        _Cmd &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                        _QtyPRO = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
                        If _QtyPRO > 0 Then
                            FNProdQty2.Text = Format(Val(_QtyPRO.ToString), "#,#")
                        End If

                        '--------New Info --------------
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

    Private Delegate Sub DelegateLoadTime()
    Private Sub CheckLoadTime()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateStateLine2(AddressOf CheckLoadTime), New Object() {})
        Else
            Try
                Dim _Qry As String
                _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
                Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub ottime_Tick(sender As Object, e As EventArgs) Handles ottime.Tick
        'Dim _Theard As New Thread(AddressOf CheckLoadTime)
        '_Theard.Start()

        Try

            'Dim _Qry As String
            '_Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
            'Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

            'Me.olbhour.Text = Format(DateAdd(DateInterval.Second, 1, CDate(_TransactionDate & " " & Me.olbhour.Text)), "HH:mm:ss")

            Me.olbhour.Text = Format(Date.Now(), "HH:mm:ss")

        Catch ex As Exception
        End Try
    End Sub

End Class
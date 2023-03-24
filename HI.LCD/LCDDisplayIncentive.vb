Imports System.Drawing
Imports System.Threading

Public Class LCDDisplayIncentive
    Private _StateFTStateDaily As Boolean = False
    Private _TimeSwitchtoSpeed As Integer = 0
    Private _TimeSwitchToHeader As Integer = 1


    Private _TotalEmpFromMasterLine1 As Integer = 0
    Private _TotalEmpHRmorningLine1 As Integer = 0
    Private _TotalEmpFromMasterLine2 As Integer = 0
    Private _TotalEmpHRmorningLine2 As Integer = 0

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
                    Me.IncenFormulaIdLine2 = Val(R!FNHSysIncenFormulaIdTo.ToString)

                Else

                    Me.Line2 = R!FTUnitSectCode.ToString
                    Me.SysLine2 = Val(R!FNHSysUnitSectId.ToString)
                    Me.IncenFormulaIdLine2 = Val(R!FNHSysIncenFormulaId.ToString)

                End If

                Me.opnl1.Visible = False
                Me.opnl0.Visible = False

            Else

                Me.Line1 = R!FTUnitSectCode.ToString
                Me.SysLine1 = Val(R!FNHSysUnitSectId.ToString)
                Me.IncenFormulaIdLine1 = Val(R!FNHSysIncenFormulaId.ToString)
                Me.Line2 = R!FTUnitSectCodeTo.ToString
                Me.SysLine2 = Val(R!FNHSysUnitSectIdTo.ToString)
                Me.IncenFormulaIdLine2 = Val(R!FNHSysIncenFormulaIdTo.ToString)

            End If

        Next

        _Qry = "SELECT " & HI.UL.ULDate.FormatDateDB & " "
        TransactionDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

        _Actualdate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Try

            _Qry = "SELECT TOP 1  '1' AS FTStateDaily"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE ISNULL(FTStateDaily,'') <> '2'"

            _StateFTStateDaily = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "1")

        Catch ex As Exception
        End Try

        Try
            _Qry = "SELECT TOP 1  FTCfgData "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS P WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCfgName = 'CfgLCDSwitchToSpeed'"
            _TimeSwitchtoSpeed = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "0"))

            _Qry = "SELECT TOP 1  FTCfgData "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS P WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCfgName = 'CfgLCDSwitchToHeader'"
            _TimeSwitchToHeader = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "1"))

        Catch ex As Exception
        End Try

    End Sub

#Region "Property"
    Private _Actualdate As String = ""
    ReadOnly Property Actualdate As String
        Get
            Return _Actualdate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

    Private _TimeInM As String = "08:00"
    Property TimeInM As String
        Get
            Return _TimeInM
        End Get
        Set(value As String)
            _TimeInM = value
        End Set
    End Property

    Private _TimeOutM As String = "12:00"
    Property TimeOutM As String
        Get
            Return _TimeOutM
        End Get
        Set(value As String)
            _TimeOutM = value
        End Set
    End Property

    Private _TimeInA As String = "13:00"
    Property TimeInA As String
        Get
            Return _TimeInA
        End Get
        Set(value As String)
            _TimeInA = value
        End Set
    End Property

    Private _TimeOutA As String = "17:00"
    Property TimeOutA As String
        Get
            Return _TimeOutA
        End Get
        Set(value As String)
            _TimeOutA = value
        End Set
    End Property

    Private _TimeInOT As String = "17:30"
    Property TimeInOT As String
        Get
            Return _TimeInOT
        End Get
        Set(value As String)
            _TimeInOT = value
        End Set
    End Property

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

    Private _IncenFormulaIdLine1 As Integer = 0
    Property IncenFormulaIdLine1 As Integer
        Get
            Return _IncenFormulaIdLine1
        End Get
        Set(value As Integer)
            _IncenFormulaIdLine1 = value
        End Set
    End Property



    Private _IncenFormulaIdLine2 As Integer = 0
    Property IncenFormulaIdLine2 As Integer
        Get
            Return _IncenFormulaIdLine2
        End Get
        Set(value As Integer)
            _IncenFormulaIdLine2 = value
        End Set
    End Property

    Private _Line1EmpCountTime As Integer = 0
    Property Line1EmpCountTime As Integer
        Get
            Return _Line1EmpCountTime
        End Get
        Set(value As Integer)
            _Line1EmpCountTime = value
        End Set
    End Property

    Private _Line1EmpCountMoney As Integer = 0
    Property Line1EmpCountMoney As Integer
        Get
            Return _Line1EmpCountMoney
        End Get
        Set(value As Integer)
            _Line1EmpCountMoney = value
        End Set
    End Property

    Private _Line1TotalEmpCountTime As Integer = 0
    Property Line1TotalEmpCountTime As Integer
        Get
            Return _Line1TotalEmpCountTime
        End Get
        Set(value As Integer)
            _Line1TotalEmpCountTime = value
        End Set
    End Property

    Private _Line2TotalEmpCountTime As Integer = 0
    Property Line2TotalEmpCountTime As Integer
        Get
            Return _Line2TotalEmpCountTime
        End Get
        Set(value As Integer)
            _Line2TotalEmpCountTime = value
        End Set
    End Property

    Private _Line2EmpCountTime As Integer = 0
    Property Line2EmpCountTime As Integer
        Get
            Return _Line2EmpCountTime
        End Get
        Set(value As Integer)
            _Line2EmpCountTime = value
        End Set
    End Property

    Private _Line2EmpCountMoney As Integer = 0
    Property Line2EmpCountMoney As Integer
        Get
            Return _Line2EmpCountMoney
        End Get
        Set(value As Integer)
            _Line2EmpCountMoney = value
        End Set
    End Property

    Private _Line1Data As DataTable = Nothing
    Property Line1Data As DataTable
        Get
            Return _Line1Data
        End Get
        Set(value As DataTable)
            _Line1Data = value
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

    Private _SysLine1Slary As Double = 0
    Property SysLine1Slary As Double
        Get
            Return _SysLine1Slary
        End Get
        Set(value As Double)
            _SysLine1Slary = value
        End Set
    End Property

    Private _SysLine1SlaryMax As Double = 0
    Property SysLine1SlaryMax As Double
        Get
            Return _SysLine1SlaryMax
        End Get
        Set(value As Double)
            _SysLine1SlaryMax = value
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

    Private _Line2Data As DataTable = Nothing
    Property Line2Data As DataTable
        Get
            Return _Line2Data
        End Get
        Set(value As DataTable)
            _Line2Data = value
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

    Private _SysLine2Slary As Double = 0
    Property SysLine2Slary As Double
        Get
            Return _SysLine2Slary
        End Get
        Set(value As Double)
            _SysLine2Slary = value
        End Set
    End Property

    Private _SysLine2SlaryMax As Double = 0
    Property SysLine2SlaryMax As Double
        Get
            Return _SysLine2SlaryMax
        End Get
        Set(value As Double)
            _SysLine2SlaryMax = value
        End Set
    End Property

    Private _Line1CheckTime As String = ""
    Property Line1CheckTime As String
        Get
            Return _Line1CheckTime
        End Get
        Set(value As String)
            _Line1CheckTime = value
        End Set
    End Property
    Private _Line2CheckTime As String = ""
    Property Line2CheckTime As String
        Get
            Return _Line2CheckTime
        End Get
        Set(value As String)
            _Line2CheckTime = value
        End Set
    End Property

    Private _Line1CheckTimeINM As String = ""
    Property Line1CheckTimeINM As String
        Get
            Return _Line1CheckTimeINM
        End Get
        Set(value As String)
            _Line1CheckTimeINM = value
        End Set
    End Property

    Private _Line1CheckTimeINA As String = ""
    Property Line1CheckTimeINA As String
        Get
            Return _Line1CheckTimeINA
        End Get
        Set(value As String)
            _Line1CheckTimeINA = value
        End Set
    End Property

    Private _Line2CheckTimeINM As String = ""
    Property Line2CheckTimeINM As String
        Get
            Return _Line2CheckTimeINM
        End Get
        Set(value As String)
            _Line2CheckTimeINM = value
        End Set
    End Property

    Private _Line2CheckTimeINA As String = ""
    Property Line2CheckTimeINA As String
        Get
            Return _Line2CheckTimeINA
        End Get
        Set(value As String)
            _Line2CheckTimeINA = value
        End Set
    End Property

#End Region


    Private Sub LCDDisplayIncentive_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.otmline1.Enabled = False
            Me.otmline2.Enabled = False
            Me.ottime.Enabled = False
            Me.otmcheckswitchtoheader.Enabled = False
            Me.otmcheckswitchtospeed.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LCDDisplayIncentive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    'Private Sub Line1GetEmployeeActualFromHR()

    '    Dim _Qry
    '    Dim _TotalCountEmp As Integer
    '    Dim _Slary As Double = 0
    '    Dim _Time As String = Me.olbhour.Text

    '    _TotalCountEmp = 0
    '    _Slary = 0

    '    _Qry = "  SELECT Sum(1) AS CountEmp "
    '    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
    '    _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

    '    If (_StateFTStateDaily) Then
    '        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '    End If

    '    _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
    '    If (_StateFTStateDaily) Then
    '        _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
    '    End If

    '    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) AND ISNULL(TT.FTIn1,'') <>''	"
    '    _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
    '    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
    '    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
    '    _TotalCountEmp = (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

    '    If _TotalCountEmp > 0 Then

    '        _Qry = "    SELECT Count(1) AS CountEmp "
    '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
    '        _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
    '        _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine1)) & " "
    '        _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine1)) & "	"
    '        _Qry &= vbCrLf & "	 AND  (FTStartTime<='" & _Time & "' AND FTEndTime>='" & _Time & "')	"

    '        _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

    '        _Qry = "    SELECT Count(1) AS CountEmp "
    '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
    '        _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
    '        _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine1)) & " "
    '        _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & "	"
    '        _Qry &= vbCrLf & "	 AND  (FTStartTime<='" & _Time & "' AND FTEndTime>='" & _Time & "')	"

    '        _TotalCountEmp = _TotalCountEmp - (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

    '    End If

    '    _Qry = "  SELECT Max(Emp.FNSalary) AS FNSalary"
    '    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
    '    _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '    _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
    '    _Qry &= vbCrLf & "	      AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
    '    _Qry &= vbCrLf & "	      AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
    '    _Qry &= vbCrLf & "	      AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

    '    Me.SysLine1Slary = 0
    '    _Slary = Double.Parse(Format((Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))), "0.00"))

    '    Me.SysLine1Slary = _Slary
    '    If _TotalCountEmp > 0 Then
    '        Me.olbemp1.Text = _TotalCountEmp.ToString
    '    End If

    '    'If Me.otmline1checkemp09.Enabled = True Then
    '    '    otmline1checkemp09.Enabled = False
    '    '    otmline1checkemp10.Enabled = True
    '    'End If

    '    'If Me.otmline1checkemp10.Enabled = True Then
    '    '    otmline1checkemp10.Enabled = False
    '    '    otmline1checkemp11.Enabled = True
    '    'End If

    '    'If Me.otmline1checkemp11.Enabled = True Then
    '    '    otmline1checkemp11.Enabled = False
    '    'End If

    'End Sub

    Private Sub Line1GetEmployeeActualFromHR()

        Dim _Qry
        Dim _TotalCountEmp As Integer = 0
        Dim _Slary As Double = 0
        Dim _Time As String = Microsoft.VisualBasic.Left(Me.olbhour.Text, 5)
        Dim dtemp As New DataTable
        Dim _CountEmpTime As Integer = 0
        Dim _CountEmpMoney As Integer = 0

        _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

        'If (_StateFTStateDaily) Then
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        ' End If

        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
        'If (_StateFTStateDaily) Then
        '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        'End If

        _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "

        If _Time < Me.Line1CheckTimeINA Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"
        End If

        If _Time > Me.Line1CheckTimeINA Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn2,'') <>''	"
        End If

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ") "
        _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

        _Qry &= vbCrLf & "	  ) "

        dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        If dtemp.Rows.Count <= 0 Then

            If _Time > Me.Line1CheckTimeINA Then

                _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
                _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

                'If (_StateFTStateDaily) Then
                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
                ' End If

                _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
                'If (_StateFTStateDaily) Then
                '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
                'End If

                _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "

                _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
                _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ") "
                _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

                _Qry &= vbCrLf & "	  ) "

                dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If

        End If

        ' _TotalCountEmp = (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

        If dtemp.Rows.Count > 0 Then

            _CountEmpTime = Integer.Parse(Val(dtemp.Rows(0)!CountEmpTime.ToString))
            _CountEmpMoney = Integer.Parse(Val(dtemp.Rows(0)!CountEmpMoney.ToString))
            _TotalCountEmp = _CountEmpTime

            _Qry = "    Select Sum(1) As CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType As X With(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	 And  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"

            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In dtemp.Rows
                _CountEmpTime = _CountEmpTime + Integer.Parse(Val(R!CountEmpTime.ToString))
                _CountEmpMoney = _CountEmpMoney + Integer.Parse(Val(R!CountEmpMoney.ToString))
            Next

            _Qry = "    Select Sum(1) As CountEmpTime "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType As X With(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	 AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

            _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	 AND  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & "  Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & "  AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In dtemp.Rows

                _CountEmpTime = _CountEmpTime - Integer.Parse(Val(R!CountEmpTime.ToString))
                _CountEmpMoney = _CountEmpMoney - Integer.Parse(Val(R!CountEmpMoney.ToString))

            Next

            _Qry = "    SELECT Sum(1) AS CountEmpTime"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            _TotalCountEmp = _TotalCountEmp - (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

        End If

        dtemp.Dispose()

        _Qry = "  SELECT Max(Emp.FNSalary) AS FNSalary"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        _Qry &= vbCrLf & "	  WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

        Me.SysLine1Slary = 0
        _Slary = Double.Parse(Format((Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))), "0.00"))

        Me.SysLine1Slary = _Slary

        If _CountEmpTime <= 0 Then
            If _Time < Me.Line1CheckTimeINA Then
                If _TotalEmpHRmorningLine1 <= 0 Then
                    If _CountEmpTime > 0 Then
                        _TotalEmpHRmorningLine1 = _CountEmpTime
                    Else
                        _CountEmpTime = _TotalEmpFromMasterLine1
                        _CountEmpMoney = _TotalEmpFromMasterLine1
                    End If
                End If
            End If

            If _Time > Me.Line1CheckTimeINA Then
                If _TotalEmpHRmorningLine1 > 0 Then
                    _CountEmpTime = _TotalEmpHRmorningLine1
                    _CountEmpMoney = _TotalEmpHRmorningLine1
                Else
                    _CountEmpTime = _TotalEmpFromMasterLine1
                    _CountEmpMoney = _TotalEmpFromMasterLine1
                End If


            End If

            _TotalCountEmp = _CountEmpTime

        End If

        Line1EmpCountTime = _CountEmpTime
        Line1EmpCountMoney = _CountEmpMoney

        If _CountEmpTime > 0 Then
            Me.olbemp1.Text = _CountEmpTime.ToString & "/" & _CountEmpMoney.ToString
        End If

        Line1TotalEmpCountTime = _CountEmpTime
        'If Me.otmline2checkemp09.Enabled = True Then
        '    otmline2checkemp09.Enabled = False
        '    otmline2checkemp10.Enabled = True
        'End If

        'If Me.otmline2checkemp10.Enabled = True Then
        '    otmline2checkemp10.Enabled = False
        '    otmline2checkemp11.Enabled = True
        'End If

        'If Me.otmline2checkemp11.Enabled = True Then
        '    otmline2checkemp11.Enabled = False
        'End If

    End Sub

    'Private Sub Line2GetEmployeeActualFromHR()

    '    Dim _Qry
    '    Dim _TotalCountEmp As Integer = 0
    '    Dim _Slary As Double = 0
    '    Dim _Time As String = Microsoft.VisualBasic.Left(Me.olbhour.Text, 5)
    '    Dim dtemp As New DataTable
    '    Dim _CountEmpTime As Integer = 0
    '    Dim _CountEmpMoney As Integer = 0

    '    _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
    '    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
    '    _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

    '    'If (_StateFTStateDaily) Then
    '    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '    ' End If

    '    _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
    '    'If (_StateFTStateDaily) Then
    '    '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
    '    'End If

    '    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) 	"

    '    If _Time < Me.Line2CheckTimeINA Then
    '        _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"
    '    End If

    '    If _Time > Me.Line2CheckTimeINA Then
    '        _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn2,'') <>''	"
    '    End If

    '    _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
    '    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
    '    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

    '    _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
    '    _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
    '    _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
    '    _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
    '    _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ") "
    '    _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
    '    _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

    '    _Qry &= vbCrLf & "	  ) "

    '    dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '    If dtemp.Rows.Count <= 0 Then

    '        If _Time > Me.Line2CheckTimeINA Then

    '            _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
    '            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
    '            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

    '            'If (_StateFTStateDaily) Then
    '            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '            ' End If

    '            _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
    '            'If (_StateFTStateDaily) Then
    '            '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
    '            'End If

    '            _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) 	"
    '            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"
    '            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
    '            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
    '            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

    '            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
    '            _Qry &= vbCrLf & "          Select  DISTINCT A.FNHSysEmpID "
    '            _Qry &= vbCrLf & "          From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
    '            _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
    '            _Qry &= vbCrLf & "          Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ") "
    '            _Qry &= vbCrLf & "          And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
    '            _Qry &= vbCrLf & "          AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

    '            _Qry &= vbCrLf & "	  ) "

    '            dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        End If


    '    End If

    '    ' _TotalCountEmp = (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

    '    If dtemp.Rows.Count > 0 Then

    '        _CountEmpTime = Integer.Parse(Val(dtemp.Rows(0)!CountEmpTime.ToString))
    '        _CountEmpMoney = Integer.Parse(Val(dtemp.Rows(0)!CountEmpMoney.ToString))
    '        _TotalCountEmp = _CountEmpTime

    '        _Qry = "    Select Sum(1) As CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
    '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType As X With(NOLOCK)"
    '        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
    '        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '        _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
    '        _Qry &= vbCrLf & "	 And FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine2)) & " "
    '        _Qry &= vbCrLf & "	 And  FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine2)) & "	"
    '        _Qry &= vbCrLf & "	 And  (FTStartTime<='" & _Time & "' AND FTEndTime>='" & _Time & "')	"

    '        _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
    '        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
    '        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
    '        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
    '        _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
    '        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
    '        dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        For Each R As DataRow In dtemp.Rows
    '            _CountEmpTime = _CountEmpTime + Integer.Parse(Val(R!CountEmpTime.ToString))
    '            _CountEmpMoney = _CountEmpMoney + Integer.Parse(Val(R!CountEmpMoney.ToString))
    '        Next

    '        _Qry = "    Select Sum(1) As CountEmpTime "
    '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType As X With(NOLOCK)"
    '        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
    '        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '        _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
    '        _Qry &= vbCrLf & "	 And FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine2)) & " "
    '        _Qry &= vbCrLf & "	 And  FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine2)) & "	"


    '        _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
    '        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
    '        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
    '        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
    '        _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
    '        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

    '        _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

    '        _Qry = "    SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
    '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
    '        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
    '        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '        _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
    '        _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine2)) & " "
    '        _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & "	"
    '        _Qry &= vbCrLf & "	 AND  (FTStartTime<='" & _Time & "' AND FTEndTime>='" & _Time & "')	"
    '        _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
    '        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
    '        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
    '        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
    '        _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
    '        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

    '        dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        For Each R As DataRow In dtemp.Rows
    '            _CountEmpTime = _CountEmpTime - Integer.Parse(Val(R!CountEmpTime.ToString))
    '            _CountEmpMoney = _CountEmpMoney - Integer.Parse(Val(R!CountEmpMoney.ToString))
    '        Next

    '        _Qry = "    SELECT Sum(1) AS CountEmpTime "
    '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
    '        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
    '        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '        _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
    '        _Qry &= vbCrLf & "	 AND FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine2)) & " "
    '        _Qry &= vbCrLf & "	 AND  FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & "	"
    '        _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
    '        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
    '        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
    '        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
    '        _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
    '        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

    '        _TotalCountEmp = _TotalCountEmp - (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))
    '    End If

    '    dtemp.Dispose()

    '    _Qry = "  SELECT Max(Emp.FNSalary) AS FNSalary"
    '    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
    '    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
    '    _Qry &= vbCrLf & "	  WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
    '    _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
    '    _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
    '    _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

    '    Me.SysLine2Slary = 0
    '    _Slary = Double.Parse(Format((Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))), "0.00"))

    '    Me.SysLine2Slary = _Slary


    '    If _CountEmpTime <= 0 Then
    '        If _Time < Me.Line1CheckTimeINA Then
    '            If _TotalEmpHRmorningLine2 <= 0 Then

    '                If _CountEmpTime > 0 Then
    '                    _TotalEmpHRmorningLine2 = _CountEmpTime
    '                Else
    '                    _CountEmpTime = _TotalEmpFromMasterLine2
    '                    _CountEmpMoney = _TotalEmpFromMasterLine2
    '                End If

    '            End If
    '        End If

    '        If _Time > Me.Line1CheckTimeINA Then
    '            If _TotalEmpHRmorningLine2 > 0 Then
    '                _CountEmpTime = _TotalEmpHRmorningLine2
    '                _CountEmpMoney = _TotalEmpHRmorningLine2
    '            Else
    '                _CountEmpTime = _TotalEmpFromMasterLine2
    '                _CountEmpMoney = _TotalEmpFromMasterLine2
    '            End If
    '        End If

    '        _TotalCountEmp = _CountEmpTime
    '    End If


    '    Line2EmpCountTime = _CountEmpTime
    '    Line2EmpCountMoney = _CountEmpMoney

    '    If _CountEmpTime > 0 Then
    '        Me.olbemp2.Text = _CountEmpTime.ToString & "/" & _CountEmpMoney.ToString
    '    End If
    '    Line2TotalEmpCountTime = _CountEmpTime
    '    'If Me.otmline2checkemp09.Enabled = True Then
    '    '    otmline2checkemp09.Enabled = False
    '    '    otmline2checkemp10.Enabled = True
    '    'End If

    '    'If Me.otmline2checkemp10.Enabled = True Then
    '    '    otmline2checkemp10.Enabled = False
    '    '    otmline2checkemp11.Enabled = True
    '    'End If

    '    'If Me.otmline2checkemp11.Enabled = True Then
    '    '    otmline2checkemp11.Enabled = False
    '    'End If

    'End Sub


    Private Sub Line2GetEmployeeActualFromHR()

        Dim _Qry
        Dim _TotalCountEmp As Integer = 0
        Dim _Slary As Double = 0
        Dim _Time As String = Microsoft.VisualBasic.Left(Me.olbhour.Text, 5)
        Dim dtemp As New DataTable
        Dim _CountEmpTime As Integer = 0
        Dim _CountEmpMoney As Integer = 0

        _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

        'If (_StateFTStateDaily) Then
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        ' End If

        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
        'If (_StateFTStateDaily) Then
        '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        'End If

        _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "

        If _Time < Me.Line2CheckTimeINA Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"
        End If

        If _Time > Me.Line2CheckTimeINA Then
            _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn2,'') <>''	"
        End If

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ") "
        _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
        _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

        _Qry &= vbCrLf & "	  ) "

        dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        If dtemp.Rows.Count <= 0 Then

            If _Time > Me.Line2CheckTimeINA Then

                _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
                _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH(NOLOCK) ON Emp.FNHSysEmpID=TT.FNHSysEmpID"

                'If (_StateFTStateDaily) Then
                _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
                ' End If

                _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
                'If (_StateFTStateDaily) Then
                '    _Qry &= vbCrLf & "  AND ISNULL(PP.FTStateDaily,'0') <>'1' "
                'End If

                _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "

                _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
                _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ") "
                _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

                _Qry &= vbCrLf & "	  ) "

                dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If

        End If

        ' _TotalCountEmp = (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

        If dtemp.Rows.Count > 0 Then

            _CountEmpTime = Integer.Parse(Val(dtemp.Rows(0)!CountEmpTime.ToString))
            _CountEmpMoney = Integer.Parse(Val(dtemp.Rows(0)!CountEmpMoney.ToString))
            _TotalCountEmp = _CountEmpTime

            _Qry = "    Select Sum(1) As CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType As X With(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine2)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine2)) & "	"
            _Qry &= vbCrLf & "	 And  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"

            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In dtemp.Rows
                _CountEmpTime = _CountEmpTime + Integer.Parse(Val(R!CountEmpTime.ToString))
                _CountEmpMoney = _CountEmpMoney + Integer.Parse(Val(R!CountEmpMoney.ToString))
            Next

            _Qry = "    Select Sum(1) As CountEmpTime "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType As X With(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine2)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine2)) & "	"
            _Qry &= vbCrLf & "	 AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

            _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine2)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & "	"
            _Qry &= vbCrLf & "	 AND  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & "  Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & "  AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In dtemp.Rows

                _CountEmpTime = _CountEmpTime - Integer.Parse(Val(R!CountEmpTime.ToString))
                _CountEmpMoney = _CountEmpMoney - Integer.Parse(Val(R!CountEmpMoney.ToString))

            Next

            _Qry = "    SELECT Sum(1) AS CountEmpTime"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
            _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine2)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & "	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "

            _TotalCountEmp = _TotalCountEmp - (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

        End If

        dtemp.Dispose()

        _Qry = "  SELECT Max(Emp.FNSalary) AS FNSalary"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        _Qry &= vbCrLf & "	  WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
        _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
        _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

        Me.SysLine2Slary = 0
        _Slary = Double.Parse(Format((Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))), "0.00"))

        Me.SysLine2Slary = _Slary

        If _CountEmpTime <= 0 Then
            If _Time < Me.Line2CheckTimeINA Then
                If _TotalEmpHRmorningLine2 <= 0 Then
                    If _CountEmpTime > 0 Then
                        _TotalEmpHRmorningLine2 = _CountEmpTime
                    Else
                        _CountEmpTime = _TotalEmpFromMasterLine2
                        _CountEmpMoney = _TotalEmpFromMasterLine2
                    End If
                End If
            End If

            If _Time > Me.Line2CheckTimeINA Then
                If _TotalEmpHRmorningLine2 > 0 Then
                    _CountEmpTime = _TotalEmpHRmorningLine2
                    _CountEmpMoney = _TotalEmpHRmorningLine2
                Else
                    _CountEmpTime = _TotalEmpFromMasterLine2
                    _CountEmpMoney = _TotalEmpFromMasterLine2
                End If


            End If

            _TotalCountEmp = _CountEmpTime

        End If

        Line2EmpCountTime = _CountEmpTime
        Line2EmpCountMoney = _CountEmpMoney

        If _CountEmpTime > 0 Then
            Me.olbemp2.Text = _CountEmpTime.ToString & "/" & _CountEmpMoney.ToString
        End If

        Line2TotalEmpCountTime = _CountEmpTime
        'If Me.otmline2checkemp09.Enabled = True Then
        '    otmline2checkemp09.Enabled = False
        '    otmline2checkemp10.Enabled = True
        'End If

        'If Me.otmline2checkemp10.Enabled = True Then
        '    otmline2checkemp10.Enabled = False
        '    otmline2checkemp11.Enabled = True
        'End If

        'If Me.otmline2checkemp11.Enabled = True Then
        '    otmline2checkemp11.Enabled = False
        'End If

    End Sub

    Private Sub LCDDisplayIncentive_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

        Dim _Qry As String
        Call ClearLabelData()
        olbsline.Text = ""
        If Me.Line1 <> "" Then

            Dim ST1 As String = ""
            For Each Str As String In Me.Line1.ToCharArray()

                If IsNumeric(Str) Then

                    Exit For
                Else
                    ST1 = Str
                End If
            Next

            If ST1 = "" Then
                ST1 = "L"
            End If

            olbsline.Text = ST1 & "." & Microsoft.VisualBasic.Right(Me.Line1, 2)

            Dim _TotalCountEmp As Integer
            _Qry = "  SELECT Sum(1) AS CountEmp"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"


            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ") "
            _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

            _Qry &= vbCrLf & "	  ) "

            _TotalCountEmp = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
            _TotalEmpFromMasterLine1 = _TotalCountEmp




            Me.olbemp1.Text = _TotalCountEmp.ToString() & "/" & _TotalCountEmp.ToString()
            Line1TotalEmpCountTime = _TotalCountEmp
            Line1EmpCountTime = _TotalCountEmp
            Line1EmpCountMoney = _TotalCountEmp
            Line1TotalEmpCountTime = _TotalCountEmp

            Line1CheckTimeINM = Me.TimeInM
            Line1CheckTimeINA = Me.TimeInA

            'Select Case e.FDDateEnd, ET.FTEmpTypeCode, TS.FTShiftCode, TS.FTIn1, TS.FTIn2, E.FNHSysUnitSectId
            'From THRMEmployee As E INNER Join
            '              THRMTimeShift As TS On e.FNHSysShiftID = TS.FNHSysShiftID INNER Join
            '              HITECH_MASTER.dbo.THRMEmpType AS ET ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId
            'Where (ISNULL(e.FDDateEnd, '') = '') AND (ET.FTEmpTypeCode = N'S')


            Call Line1GetEmployeeActualFromHR()
            Me.otmline1.Enabled = True
            Me.otmline1checkemp09.Enabled = True

            Dim _Theard1 As New Thread(AddressOf CheckStateLine1)
            _Theard1.Start()
        End If

        olbeline.Text = ""
        If Me.Line2 <> "" Then


            Dim ST1 As String = ""
            For Each Str As String In Me.Line2.ToCharArray()

                If IsNumeric(Str) Then
                    Exit For
                Else
                    ST1 = Str
                End If
            Next

            If ST1 = "" Then
                ST1 = "L"
            End If

            olbeline.Text = ST1 & "." & Microsoft.VisualBasic.Right(Me.Line2, 2)
            Dim _TotalCountEmp As Integer
            _Qry = "  SELECT Sum(1) AS CountEmp"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
            _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

            _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
            _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ") "
            _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "

            _Qry &= vbCrLf & "	  ) "

            _TotalCountEmp = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
            _TotalEmpFromMasterLine2 = _TotalCountEmp

            Me.olbemp2.Text = _TotalCountEmp.ToString() & "/" & _TotalCountEmp.ToString()
            Line2TotalEmpCountTime = _TotalCountEmp
            Line2EmpCountTime = _TotalCountEmp
            Line2EmpCountMoney = _TotalCountEmp
            Line2TotalEmpCountTime = _TotalCountEmp

            Line2CheckTimeINM = Me.TimeInM
            Line2CheckTimeINA = Me.TimeInA

            Call Line2GetEmployeeActualFromHR()
            Me.otmline2.Enabled = True
            Me.otmline2checkemp09.Enabled = True

            Dim _Theard2 As New Thread(AddressOf CheckStateLine2)
            _Theard2.Start()
        End If

        _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
        Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

        Me.ottime.Enabled = True

        Me.otmcheckswitchtoheader.Enabled = False
        Me.otmcheckswitchtospeed.Enabled = False

        If _TimeSwitchtoSpeed > 0 Then

            _TimeSwitchtoSpeed = _TimeSwitchtoSpeed * (60 * 1000)

            otmcheckswitchtospeed.Interval = _TimeSwitchtoSpeed

            If _TimeSwitchToHeader > 0 Then

                _TimeSwitchToHeader = _TimeSwitchToHeader * (60 * 1000)
                otmcheckswitchtospeed.Interval = _TimeSwitchToHeader

            End If

            Me.otmcheckswitchtospeed.Enabled = True

        End If

    End Sub

    Private Sub ClearLabelData()

        olbsline.Text = ""
        olbeline.Text = ""
        olbemp1.Text = ""
        olbemp2.Text = ""
        olbtime1.Text = ""
        olbtime2.Text = ""
        olbqa1.Text = ""
        olbqa2.Text = ""
        olbstarget1.Text = ""
        olbstarget2.Text = ""
        olbetarget1.Text = ""
        olbetarget2.Text = ""
        olbsscan1.Text = ""
        olbsscan2.Text = ""
        olbescan1.Text = ""
        olbescan2.Text = ""
        olbslv11.Text = ""

        olbslv13.Text = ""
        olbslv14.Text = ""
        olbslv21.Text = ""

        olbslv23.Text = ""
        olbslv24.Text = ""

        olbslvt1.Text = ""

        olbslvt3.Text = ""
        olbslvt4.Text = ""

        olbelv11.Text = ""

        olbelv13.Text = ""
        olbelv14.Text = ""
        olbelv21.Text = ""

        olbelv23.Text = ""
        olbelv24.Text = ""

        olbelvt1.Text = ""

        olbelvt3.Text = ""
        olbelvt4.Text = ""
    End Sub

    Private Sub ClearLabelLine1Data()
        olbslv11.Text = ""

        olbslv13.Text = ""
        olbslv14.Text = ""
        olbslv21.Text = ""

        olbslv23.Text = ""
        olbslv24.Text = ""

        olbslvt1.Text = ""

        olbslvt3.Text = ""
        olbslvt4.Text = ""
    End Sub

    Private Sub ClearLabelLine2Data()
        olbelv11.Text = ""

        olbelv13.Text = ""
        olbelv14.Text = ""
        olbelv21.Text = ""

        olbelv23.Text = ""
        olbelv24.Text = ""

        olbelvt1.Text = ""

        olbelvt3.Text = ""
        olbelvt4.Text = ""
    End Sub

    Private Sub LCDDisplay_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim _MeHeight As Integer = Me.Height
        Dim _MeWidth As Integer = Me.Width
        Dim _LineHeaderWidth As Integer = 632

        Dim _P11W As Integer = 321

        Dim _PH1W As Integer = 162
        Dim _PH2W As Integer = 153
        Dim _PH3W As Integer = 153
        Dim _PH4W As Integer = 158

        '----Form Width = 800
        Dim _P1 As Integer = 95
        Dim _P2 As Integer = 115
        Dim _P3 As Integer = 115
        Dim _PLVTITLE As Integer = 90
        Dim _PLV1 As Integer = 127
        Dim _PLV2 As Integer = 127
        Dim _PLV3 As Integer = 127
        Dim _PLVTop As Integer = 127

        Dim _PLineW As Integer = 162 'opnline1
        Dim _PineEmpW As Integer = 159 'opnemp1
        Dim _PlineTimeW As Integer = 153 'opntime1
        Dim _PLineQAW As Integer = 158 'opnqa1

        Dim _PM1 As Integer = 49

        Dim _LineWidth As Integer = 0
        Dim _CaptionWidth As Integer = 0

        _P1 = _MeHeight * 0.11875
        _P2 = _MeHeight * 0.14375
        _P3 = _MeHeight * 0.14375
        _PLVTITLE = _MeHeight * 0.1125
        _PLV1 = _MeHeight * 0.15875
        _PLV2 = _MeHeight * 0.15875
        _PLV3 = _MeHeight * 0.15875
        _PLVTop = _MeHeight * 0.15875

        _PM1 = (_P2 * 0.57647)

        If Me.opnl1.Visible And Me.opnl0.Visible Then
            Me.opnl1.Width = ((_MeWidth - 8) / 2)
        Else
            Me.opnl1.Width = _MeWidth
        End If

        _LineHeaderWidth = opnsline.Width

        _PLineW = _LineHeaderWidth * 0.25632911
        _PineEmpW = _LineHeaderWidth * 0.25158227848
        _PlineTimeW = _LineHeaderWidth * 0.242088607
        _PLineQAW = _LineHeaderWidth * 0.25
        _P11W = _LineHeaderWidth * 0.507911392
        _PH1W = _LineHeaderWidth * 0.2848101266 '180
        _PH2W = _LineHeaderWidth * 0.253164556962 ' 160
        _PH3W = _LineHeaderWidth * 0.253164556962 '160
        _PH4W = _LineHeaderWidth * 0.4620253165 '292

        Dim FontHourSize As Integer = (_P1 * 0.454545)
        Dim FHour As New Font("Tahoma", FontHourSize, FontStyle.Bold)
        'Me.opnhour.Height = _P1
        'Me.olbhour.Font = FHour

        '-----------Start Set Line 1
        opnsline.Height = _P1
        opnstargetqty.Height = _P2
        opnsprodqty.Height = _P3
        opnsheader.Height = _PLVTITLE
        opnslv1.Height = _PLV1
        opnslv2.Height = _PLV2

        opnslvtop.Height = _PLVTop

        opnline1.Width = _PLineW
        opnemp1.Width = _PineEmpW
        opntime1.Width = _PlineTimeW

        opnqa1.Width = _PLineQAW

        opnstargetqty1.Width = _P11W
        opnsprodqty1.Width = _P11W
        opnsprodspeed11.Width = _P11W

        olbsheader1.Width = _PH1W
        olbsheader2.Width = _PH3W
        olbsheader3.Width = _PH4W

        olbslv11.Width = _PH1W

        olbslv13.Width = _PH3W
        olbslv14.Width = _PH4W
        olbslv21.Width = _PH1W
        olbslv23.Width = _PH3W
        olbslv24.Width = _PH4W

        olbslvt1.Width = _PH1W
        olbslvt3.Width = _PH3W
        olbslvt4.Width = _PH4W

        _LineWidth = opneline.Width
        _CaptionWidth = _LineWidth / 2

        '-----------End Set Line 1

        '-----------Start Set Line 2

        opneline.Height = _P1
        opnetargetqty.Height = _P2
        opneprodqty.Height = _P3
        opneheader.Height = _PLVTITLE
        opnelv1.Height = _PLV1
        opnelv2.Height = _PLV2
        opnelv3.Height = _PLV3
        opnelvtop.Height = _PLVTop

        opnline2.Width = _PLineW
        opnemp2.Width = _PineEmpW
        opntime2.Width = _PlineTimeW
        opnqa2.Width = _PLineQAW

        opnetargetqty1.Width = _P11W
        opneprodqty1.Width = _P11W
        opneprodspeed11.Width = _P11W

        olbeheader1.Width = _PH1W
        olbeheader2.Width = _PH3W
        olbeheader3.Width = _PH4W

        olbelv11.Width = _PH1W

        olbelv13.Width = _PH3W
        olbelv14.Width = _PH4W

        olbelv21.Width = _PH1W

        olbelv23.Width = _PH3W
        olbelv24.Width = _PH4W

        olbelvt1.Width = _PH1W

        olbelvt3.Width = _PH3W
        olbelvt4.Width = _PH4W

        '-----------End Set Line 2
        Dim _ImageW As Integer = (_P11W * 0.42056075)

        Me.opcstargetqty1.Width = _ImageW
        Me.opcstargetqty2.Width = _ImageW

        Me.opcetargetqty1.Width = _ImageW
        Me.opcetargetqty2.Width = _ImageW

        Me.opcsscan1.Width = _ImageW
        Me.opcsscan2.Width = _ImageW
        Me.opcsprodspeed01.Width = _ImageW
        Me.opcsprodspeed02.Width = _ImageW

        Me.opcescan1.Width = _ImageW
        Me.opcescan2.Width = _ImageW
        Me.opceprodspeed01.Width = _ImageW
        Me.opceprodspeed02.Width = _ImageW

        'Start Set Font -
        '-----------Set Font Header------------
        Dim _FontLineH As Integer = _P1 * 0.45 ' 30 '90
        Dim _FontLineH2 As Integer = _P1 * 0.3 ' 30 '90
        Dim FFontLineH As New Font("Tahoma", _FontLineH, FontStyle.Bold)
        Dim FFontLineH2 As New Font("Tahoma", _FontLineH2, FontStyle.Bold)

        Me.olbsline.Font = FFontLineH
        Me.olbemp1.Font = FFontLineH2
        Me.olbtime1.Font = FFontLineH

        Me.olbeline.Font = FFontLineH
        Me.olbemp2.Font = FFontLineH2
        Me.olbtime2.Font = FFontLineH

        Dim _FontLineHQA As Integer = _P1 * 0.42 ' 30 '90
        Dim FFontLineHQA As New Font("Tahoma", _FontLineHQA, FontStyle.Bold)
        Me.olbqa1.Font = FFontLineHQA
        Me.olbqa2.Font = FFontLineHQA
        '----S Font LV
        Dim _FontLV As Integer = _PLV1 * 0.35 ' 30 '90
        Dim FFontLV As New Font("Tahoma", _FontLV, FontStyle.Bold)

        Dim _FontLVTile As Integer = _PLVTITLE * 0.5 ' 30 '90
        Dim FFontLVTile As New Font("Tahoma", _FontLVTile, FontStyle.Bold)

        'Dim _FontLV001 As Integer = _PLV1 * 0.300465 ' 30 '90
        'Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        'Dim _FontLV002 As Integer = _PLV1 * 0.4005952756 ' 30 '90
        'Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        'Dim _FontLV003 As Integer = _PLV1 * 0.4045952756 ' 30 '90
        'Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)

        Dim _FontLV001 As Integer = _PLV1 * 0.295465 ' 30 '90
        Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        Dim _FontLV002 As Integer = _PLV1 * 0.3595952756 ' 30 '90
        Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        Dim _FontLV003 As Integer = _PLV1 * 0.3635952756 ' 30 '90
        Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)

        olbsheader1.Font = FFontLVTile
        olbsheader2.Font = FFontLVTile
        olbsheader3.Font = FFontLVTile

        olbslv11.Font = FFontLV001
        olbslv13.Font = FFontLV002
        olbslv14.Font = FFontLV003

        olbslv21.Font = FFontLV001
        olbslv23.Font = FFontLV002
        olbslv24.Font = FFontLV003

        olbslvt1.Font = FFontLV001
        olbslvt3.Font = FFontLV002
        olbslvt4.Font = FFontLV003

        olbeheader1.Font = FFontLVTile
        olbeheader2.Font = FFontLVTile
        olbeheader3.Font = FFontLVTile

        olbelv11.Font = FFontLV001
        olbelv13.Font = FFontLV002
        olbelv14.Font = FFontLV003

        olbelv21.Font = FFontLV001
        olbelv23.Font = FFontLV002
        olbelv24.Font = FFontLV003

        olbelvt1.Font = FFontLV001
        olbelvt3.Font = FFontLV002
        olbelvt4.Font = FFontLV003

        '----E Font LV
        Dim FontHeaderLineSize As Integer = (_P2 * 0.1942991)
        Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)

        Dim FontHeaderLineSize0 As Integer = (_P2 * 0.45728972)
        Dim FHeader0 As New Font("Tahoma", FontHeaderLineSize0, FontStyle.Bold)

        Dim FontHeaderLineSize1 As Integer = (_PLVTITLE * 0.172)
        Dim FHeader1 As New Font("Tahoma", FontHeaderLineSize1, FontStyle.Bold)

        olbstarget1.Font = FHeader0
        olbstarget2.Font = FHeader0
        olbstarget2Eff.Font = FHeader0

        olbetarget1.Font = FHeader0
        olbetarget2.Font = FHeader0
        olbetarget2Eff.Font = FHeader0

        olbsscan1.Font = FHeader0
        olbsscan2.Font = FHeader0

        olbescan1.Font = FHeader0
        olbescan2.Font = FHeader0

        olbsprodspeed1.Font = FHeader0
        olbsprodspeed2.Font = FHeader0

        olbeprodspeed1.Font = FHeader0
        olbeprodspeed2.Font = FHeader0

        olbstarget01.Font = FHeader
        olbstarget02.Font = FHeader
        olbstarget02Eff.Font = FHeader

        olbetarget01.Font = FHeader
        olbetarget02.Font = FHeader
        olbetarget02Eff.Font = FHeader

        olbsscan01.Font = FHeader
        olbsscan02.Font = FHeader

        olbescan01.Font = FHeader
        olbescan02.Font = FHeader

        Me.olbsprodspeed01.Font = FHeader1
        Me.olbsprodspeed02.Font = FHeader1

        Me.olbeprodspeed01.Font = FHeader1
        Me.olbeprodspeed02.Font = FHeader1

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
                Dim _TotalHourTarget As Integer = 0
                Dim _TotalTargetPerHour As Integer = 0
                Dim _dttime As DataTable
                Dim _TotalCountEmp As Integer = 0
                Dim _TimeServer As String = ""
                Dim _FNSam As Double = 0
                Dim _Qry As String
                Dim _dttimeplan As DataTable
                Dim _TimeWorlPlanMinute As Integer = 0

                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime,FNTargetPerHour "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & ""
                _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "
                _dttimeplan = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                For Each R As DataRow In _dttimeplan.Rows
                    _TotalTarget = Integer.Parse(Val(R!FNTarget.ToString))
                    _TotalHourTarget = Integer.Parse(Val(R!FNTargetPerHour.ToString))

                    If R!FTWorkTime.ToString <> "" Then
                        Me.olbtime1.Text = R!FTWorkTime.ToString.Split(":")(0)
                        Try
                            _TimeWorlPlanMinute = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * 60) + (Val(R!FTWorkTime.ToString.Split(":")(1))))
                        Catch ex As Exception

                        End Try

                        If _TotalHourTarget > 0 Then
                            _TotalTarget = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * _TotalHourTarget)) + Integer.Parse(((Val(R!FTWorkTime.ToString.Split(":")(1)) * (_TotalHourTarget / 60.0))))
                        End If

                    Else
                        Me.olbtime1.Text = "8"
                        _TimeWorlPlanMinute = 480

                        If _TotalHourTarget > 0 Then
                            _TotalTarget = ((8 * _TotalHourTarget))
                        End If
                    End If



                    Exit For
                Next

                _dttimeplan.Dispose()

                If _TotalTarget > 0 Then
                    Dim _Salary As Double = 0
                    If Val(Me.olbtime1.Text) >= 8 Then

                        _Salary = Me.SysLine1Slary
                        _Salary = _Salary + Double.Parse(Format((Me.SysLine1Slary / 8) * (Val(Me.olbtime1.Text) - 8) * 1.5, "0.00"))

                    Else
                        _Salary = Double.Parse(Format((Me.SysLine1Slary / 8) * Val(Me.olbtime1.Text), "0.00"))

                    End If

                    Me.SysLine1SlaryMax = _Salary

                    If _TotalHourTarget > 0 Then
                        olbstarget1.Text = _TotalTarget.ToString
                        olbstarget2.Text = _TotalHourTarget.ToString
                    Else
                        olbstarget1.Text = _TotalTarget.ToString
                        _TotalTargetPerHour = (_TotalTarget / _TimeWorlPlanMinute) * 60.0
                        olbstarget2.Text = _TotalTargetPerHour.ToString
                    End If


                    Dim _DateNow As String = _TransactionDate

                    Dim _Cmd As String = ""
                    Dim _oDt As DataTable
                    Dim _QtyOrder As Double = 0
                    Dim _FNHSysStyleId As Integer = 0
                    Dim _PriceCost As Double = 0
                    Dim _QtyPRO As Double = 0
                    Dim _OrderNo As String = ""
                    Dim _TotalCountStyle As Integer = 0
                    Dim TotalCalEff As Decimal = 0
                    Dim OrderQty As Integer = 0

                    '_Cmd = " Create Table #Tmpbb ([FTSubOrderNo] varchar(30) NULL ,[FTBarcodeBundleNo] varchar(30) NULL )  Create Index [Idx_Tmpbb] ON #Tmpbb([FTSubOrderNo] ,[FTBarcodeBundleNo]) "
                    '_Cmd &= vbCrLf & "INSERT INTO #Tmpbb(FTSubOrderNo,FTBarcodeBundleNo)"
                    '_Cmd &= vbCrLf & "   Select MAX(A.FTSubOrderNo) As FTSubOrderNo, BCD.FTBarcodeBundleNo"

                    '_Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail As A With(NOLOCK) INNER JOIN"
                    '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As B On A.FTOrderNo = B.FTOrderNo And A.FTSubOrderNo = B.FTSubOrderNo And A.FTColorway = B.FTColorway And "
                    '_Cmd &= vbCrLf & "   A.FTSizeBreakDown = B.FTSizeBreakDown INNER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As BCD With(NOLOCK) On A.FTColorway = BCD.FTColorway And A.FTSizeBreakDown = BCD.FTSizeBreakDown And A.FTOrderProdNo = BCD.FTOrderProdNo And "
                    '_Cmd &= vbCrLf & "   B.FTNikePOLineItem = BCD.FTPOLineItemNo"
                    '_Cmd &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline As O With(NOLOCK) On BCD.FTBarcodeBundleNo=O.FTBarcodeNo "
                    '_Cmd &= vbCrLf & "        WHERE (O.FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                    '_Cmd &= vbCrLf & "              And O.FDDate ='" & _DateNow & "' "
                    '_Cmd &= vbCrLf & " GROUP BY BCD.FTBarcodeBundleNo"

                    '_Cmd &= vbCrLf & "  SELECT   P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId"
                    '_Cmd &= vbCrLf & "FROM   "
                    '_Cmd &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                    '_Cmd &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                    '_Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                    '_Cmd &= vbCrLf & "        WHERE (S.FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                    '_Cmd &= vbCrLf & "         and S.FDScanDate ='" & _DateNow & "'     "
                    '_Cmd &= vbCrLf & "   AND O.FTBarcodeNo Is NULL "
                    '_Cmd &= vbCrLf & "    UNION "
                    '_Cmd &= vbCrLf & " SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , PP.FTOrderNo , ISNULL(O.FTSubOrderNo,ISNULL(XXA.FTSubOrderNo,'')) AS FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                    '_Cmd &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                    '_Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                    '_Cmd &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP ON B.FTOrderProdNo = PP.FTOrderProdNo "

                    '_Cmd &= vbCrLf & "   Left OUTER JOIN  "
                    '_Cmd &= vbCrLf & "  #Tmpbb  "

                    '_Cmd &= vbCrLf & "     AS XXA ON O.FTBarcodeNo = XXA.FTBarcodeBundleNo"
                    '_Cmd &= vbCrLf & "        WHERE (O.FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                    '_Cmd &= vbCrLf & "              and O.FDDate ='" & _DateNow & "'     "
                    '_Cmd &= vbCrLf & "   ) AS P  RIGHT OUTER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                    '_Cmd &= vbCrLf & "        WHERE(FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                    '_Cmd &= vbCrLf & "and P.FDScanDate ='" & _DateNow & "'     "
                    '_Cmd &= vbCrLf & "GROUP BY  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId"
                    '_Cmd &= vbCrLf & "Drop table  #Tmpbb "

                    _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(Me.SysLine1)) & ",'" & _DateNow & "' "
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    _FNSam = 0
                    _PriceCost = 0
                    _TotalCountStyle = 0
                    TotalCalEff = 0

                    For Each R As DataRow In _oDt.Rows
                        _TotalCountStyle = _TotalCountStyle + 1

                        _OrderNo = R!FTOrderNo.ToString
                        _FNHSysStyleId = Integer.Parse(Val(R!FNHSysStyleId.ToString))
                        OrderQty = Val(R!FNQuantity.ToString)

                        Dim _dtCost As DataTable
                        _Cmd = "   SELECT FNSam, FNCostPerMin, FNPrice"
                        _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS P WITH(NOLOCK)"
                        _Cmd &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & ""
                        _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                        _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                        _dtCost = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)

                        For Each Rxc As DataRow In _dtCost.Rows
                            _PriceCost = _PriceCost + Val(Rxc!FNPrice.ToString)
                            _FNSam = _FNSam + Val(Rxc!FNSam.ToString)

                            TotalCalEff = TotalCalEff + CDbl(Format((OrderQty * Val(Rxc!FNSam.ToString)), "0.00"))

                            Exit For
                        Next

                        _dtCost.Dispose()

                    Next

                    If _TotalCountStyle > 1 Then

                        _PriceCost = Double.Parse(Format(_PriceCost / _TotalCountStyle, "0.0000"))
                        _FNSam = Double.Parse(Format(_FNSam / _TotalCountStyle, "0.0000"))

                    End If


                    If Line1TotalEmpCountTime <= 0 Then
                        Line1GetEmployeeActualFromHR()
                    End If

                    _TotalCountEmp = Line1TotalEmpCountTime 'Integer.Parse(Val(Me.olbemp1.Text))


                    TotalCalEff = Double.Parse(Format((TotalCalEff / (_TimeWorlPlanMinute * _TotalCountEmp)) * 100.0, "0.00"))
                    olbstarget2Eff.Text = Format(TotalCalEff, "0.00")

                    Dim _dtPrice As DataTable


                    If IncenFormulaIdLine1 <= 0 Then
                        _Qry = "  Select FNLVSeq"
                        _Qry &= vbCrLf & " , FNStartEff"
                        _Qry &= vbCrLf & "   , FNEndEff"
                        _Qry &= vbCrLf & "   , FNPriceMultiple"
                        _Qry &= vbCrLf & "  ," & _PriceCost & " AS FNPrice"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetQty"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetChkQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActBalQty"
                        _Qry &= vbCrLf & "  , 0.000 AS FNPriceMul"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmount"
                        _Qry &= vbCrLf & "  , '0' AS FTStateMax"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmountMax"
                        _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMEFFLevel AS P WITH(NOLOCK)"
                        _Qry &= vbCrLf & "  ORDER BY FNLVSeq"

                    Else

                        _Qry = "  Select FNLVSeq"
                        _Qry &= vbCrLf & " , FNStartEff"
                        _Qry &= vbCrLf & "   , FNEndEff"
                        _Qry &= vbCrLf & "   , FNPriceMultiple"
                        _Qry &= vbCrLf & "  ," & _PriceCost & " AS FNPrice"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetQty"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetChkQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActBalQty"
                        _Qry &= vbCrLf & "  , 0.000 AS FNPriceMul"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmount"
                        _Qry &= vbCrLf & "  , '0' AS FTStateMax"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmountMax"
                        _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMIncentiveFormulaLevel AS P WITH(NOLOCK)"
                        _Qry &= vbCrLf & "   WHERE FNHSysIncenFormulaId=" & IncenFormulaIdLine1 & ""
                        _Qry &= vbCrLf & "  ORDER BY FNLVSeq"

                    End If

                    _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    If _dtPrice.Rows.Count > 0 And _FNSam > 0 Then
                        Dim _TotalTGQty As Integer = ((_TimeWorlPlanMinute * _TotalCountEmp) / _FNSam)
                        Dim _TotalBFQty As Integer = 0
                        Dim _TotalQtyG As Integer = 0
                        Dim _TQty As Integer = 0
                        Dim _RowCount As Integer = _dtPrice.Rows.Count
                        Dim _RowIdx As Integer = 0
                        For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")

                            _RowIdx = _RowIdx + 1

                            If _RowIdx = _RowCount Then
                                _TQty = _TotalBFQty + 1
                            Else
                                _TQty = ((_TotalTGQty * Val(Rxp!FNEndEff.ToString) / 100))
                            End If

                            Rxp!FNTargetQty = _TQty
                            If _RowIdx = _RowCount Then
                                Rxp!FNTargetChkQty = _TQty
                                Rxp!FNActBalQty = _TQty
                                Rxp!FTStateMax = "1"
                            Else
                                Rxp!FNTargetChkQty = _TQty - _TotalBFQty
                                Rxp!FNActBalQty = _TQty - _TotalBFQty
                            End If

                            Rxp!FNPriceMul = CDbl(Format(Val(Rxp!FNPriceMultiple.ToString) * Val(Rxp!FNPrice.ToString), "0.00"))

                            _TotalBFQty = _TQty
                        Next
                        Dim _AmtMax As Double = 0

                        _RowIdx = 0
                        _TotalBFQty = 0
                        For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")
                            _RowIdx = _RowIdx + 1

                            If _RowIdx = _RowCount Then
                                _AmtMax = _AmtMax + CDbl(Format(Val(Rxp!FNPriceMul.ToString) * (Val(Rxp!FNActBalQty.ToString) - _TotalBFQty), "0.00"))

                            Else
                                _AmtMax = _AmtMax + CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActBalQty.ToString), "0.00"))

                            End If

                            If Line1EmpCountMoney > 0 Then
                                Rxp!FNAmountMax = CDbl(Format(_AmtMax / Line1EmpCountMoney, "0.00"))
                            Else
                                Rxp!FNAmountMax = CDbl(Format(_AmtMax / _TotalCountEmp, "0.00"))
                            End If

                            _TotalBFQty = Val(Rxp!FNTargetQty.ToString)
                        Next

                    End If


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

                        '------ End Target

                        '------ Start Production------
                        Dim _dtprod As DataTable
                        Dim _TotalProd As Integer = 0
                        Dim _Prod As Integer = 0

                        _Qry = "    SELECT    FDScanDate  AS FTDateScan"
                        _Qry &= vbCrLf & "   ,FDScanTime  AS FTTimeScan"
                        _Qry &= vbCrLf & "   ,SUM(FNScanQuantity) AS FNScanQuantity"
                        _Qry &= vbCrLf & "    FROM  "  '[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS A WITH(NOLOCK)

                        _Qry &= vbCrLf & " (SELECT  TOP 0  S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        _Qry &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        _Qry &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        _Qry &= vbCrLf & "    UNION "
                        _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , '' ,'', B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo   "


                        _Qry &= vbCrLf & "   WHERE O.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
                        _Qry &= vbCrLf & "   And  O.FDDate ='" & Me.TransactionDate & "' and ISNULL(O.FNStateSewPack,0)  IN (0,1)   "

                        _Qry &= vbCrLf & "   ) AS A  "


                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
                        _Qry &= vbCrLf & "   AND FDScanDate ='" & Me.TransactionDate & "'   "
                        _Qry &= vbCrLf & "   GROUP BY FDScanDate, FDScanTime"


                        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                        _TotalProd = 0

                        For Each R As DataRow In _dtprod.Rows
                            _TotalProd = _TotalProd + Val(R!FNScanQuantity)
                        Next

                        olbsscan1.Text = _TotalProd.ToString
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

                        '_TotalProd = 751
                        '_Prod = 78

                        If _TotalProd > 0 Then
                            Dim _TotalActualProd As Integer = _TotalProd
                            Dim _TotalActualQty As Integer = 0

                            Dim _RowCount As Integer = _dtPrice.Rows.Count
                            Dim _RowIdx As Integer = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")

                                _RowIdx = _RowIdx + 1

                                If _RowIdx = _RowCount Then
                                    Rxp!FNActQty = _TotalActualProd
                                    Rxp!FNActBalQty = 0
                                Else
                                    If Val(Rxp!FNActBalQty) > _TotalActualProd Then
                                        _TotalActualQty = _TotalActualProd
                                        Rxp!FNActQty = _TotalActualQty
                                        Rxp!FNActBalQty = (Val(Rxp!FNActBalQty) - _TotalActualQty)
                                    Else
                                        _TotalActualQty = Val(Rxp!FNActBalQty)
                                        Rxp!FNActQty = _TotalActualQty
                                        Rxp!FNActBalQty = 0
                                    End If
                                End If

                                _TotalActualProd = _TotalActualProd - _TotalActualQty


                                If _TotalActualProd <= 0 Then
                                    Exit For
                                End If
                            Next

                            For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1' AND FNActBalQty >0", "FNLVSeq")

                                If _TotalProd >= Val(Rxp!FNActBalQty) Then
                                    Rxp!FNActBalQty = 0
                                Else

                                    Rxp!FNActBalQty = (Val(Rxp!FNActBalQty) - _TotalProd)
                                End If
                            Next

                            Dim _Amount As Double = 0
                            Dim _MaxSeq As Integer = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNActQty >0", "FNLVSeq")

                                If _TotalCountEmp > 0 Then


                                    If Line1EmpCountMoney > 0 Then
                                        _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00")) / Line1EmpCountMoney), "0.00"))
                                    Else
                                        _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00")) / _TotalCountEmp), "0.00"))
                                    End If

                                Else
                                    _TotalCountEmp = 0
                                End If
                                _MaxSeq = Val(Rxp!FNLVSeq)
                                Rxp!FNAmount = _Amount

                            Next


                            If _Amount > 0 And _MaxSeq > 0 Then
                                If _dtPrice.Select("FNLVSeq =" & _MaxSeq & " AND FNActBalQty>0").Length <= 0 Then
                                    For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq =" & _MaxSeq + 1 & " AND FNTargetChkQty=FNActBalQty AND FTStateMax<>'1'", "FNLVSeq")
                                        Rxp!FNAmount = _Amount
                                        Exit For
                                    Next
                                End If

                            End If

                        End If

                        olbsscan1.Text = _TotalProd.ToString
                        olbsscan2.Text = _Prod.ToString

                        Call ClearLabelLine1Data()

                        If _dtPrice.Select("FNActBalQty >0", "FNLVSeq").Length > 0 Then

                            Dim _RowIdx As Integer = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNActBalQty >0", "FNLVSeq")
                                _RowIdx = _RowIdx + 1
                                If _RowIdx > 2 Then
                                    Exit For
                                End If

                                Select Case _RowIdx
                                    Case 1

                                        olbslv11.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                                        olbslv13.Text = Rxp!FNActBalQty.ToString
                                        olbslv14.Tag = "0"

                                        If Val(Rxp!FNAmount.ToString) = 0 Then

                                            olbslv14.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"

                                        Else

                                            olbslv14.ForeColor = Color.Blue
                                            olbslv14.Tag = "1"
                                            olbslv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString

                                        End If

                                    Case 2

                                        olbslv21.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                                        olbslv23.Text = Rxp!FNActBalQty.ToString
                                        olbslv24.Tag = "0"

                                        If Val(Rxp!FNAmount.ToString) = 0 Then
                                            olbslv24.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"
                                        Else
                                            olbslv24.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                        End If

                                        'Case 3
                                        '    olbslv31.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                                        '    olbslv32.Text = Rxp!FNTargetQty.ToString
                                        '    olbslv33.Text = Rxp!FNActBalQty.ToString
                                        '    olbslv34.Tag = "0"
                                        '    If Val(Rxp!FNAmount.ToString) = 0 Then
                                        '        olbslv34.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"
                                        '    Else
                                        '        olbslv34.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                        '    End If

                                End Select

                            Next
                        Else
                            olbslv14.Tag = "0"
                            For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")
                                olbslv11.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                                olbslv13.Text = Rxp!FNActBalQty.ToString
                                olbslv14.Tag = "1"
                                olbslv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                Exit For
                            Next

                        End If

                        For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")
                            olbslvt1.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                            olbslvt3.Text = Rxp!FNActBalQty.ToString
                            olbslvt4.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"

                            Exit For
                        Next

                        Me._Line1Data = _dtPrice.Copy
                        _dtPrice.Dispose()
                        '------ Start Production------

                        '-------New Info ------------

                        _Cmd = "SELECT   Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
                        _Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
                        _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty "
                        _Cmd &= vbCrLf & "	,(SUM(ISNULL(D.FNTotalDefect,0))) AS FNTotalDefect"
                        _Cmd &= vbCrLf & "		,  sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        _Cmd &= vbCrLf & "  ,((SUM(ISNULL(D.FNTotalDefect,0)))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"

                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B   WITH (NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH (NOLOCK) ON A.FTOrderNo = O.FTOrderNo"

                        _Cmd &= vbCrLf & "   OUTER APPLY(SELECT  SUM ( 1) AS FNTotalDefect "
                        _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail As D WITH( NOLOCK)"

                        _Cmd &= vbCrLf & " WHERE D.FNHSysStyleId = A.FNHSysStyleId"
                        _Cmd &= vbCrLf & "  And D.FNHSysUnitSectId =A.FNHSysUnitSectId"
                        _Cmd &= vbCrLf & "  And D.FTOrderNo =A.FTOrderNo"
                        _Cmd &= vbCrLf & "  And D.FDQADate =A.FDQADate"
                        _Cmd &= vbCrLf & "  And D.FNHourNo =A.FNHourNo"
                        _Cmd &= vbCrLf & "  And D.FTStateReject ='1'	"
                        _Cmd &= vbCrLf & " ) AS D"
                        _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & _DateNow & "')"
                        _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1))
                        _Cmd &= vbCrLf & "group by    A.FNHSysUnitSectId,  A.FDQADate "
                        _Cmd &= vbCrLf & "Order by A.FDQADate"
                        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                        Dim _QAPer As Double = 100

                        If _TotalProd <= 0 Then
                            _QAPer = 0
                        End If

                        For Each R As DataRow In _oDt.Rows
                            If Val(R!FNQAActualQty) > 0 Then
                                '_QAPer = CDbl(Format(((R!FNQAActualQty - R!FNTotalDefect) / R!FNQAActualQty) * 100, "0.00"))

                                _QAPer = CDbl(Format(100.0 - CDbl(Format(((R!FNTotalDefect) / R!FNQAActualQty) * 100, "0.00")), "0.00"))

                            End If

                            Exit For
                        Next

                        If _QAPer >= 100 Then
                            Me.olbqa1.Text = "100"
                        Else
                            If _TotalProd <= 0 Then
                                Me.olbqa1.Text = "-"
                            Else
                                Me.olbqa1.Text = Format(_QAPer, "0.0")
                            End If

                        End If

                    Else

                        olbtime1.Text = ""
                        olbqa1.Text = "-"
                        olbstarget1.Text = ""
                        olbstarget2.Text = ""
                        olbsscan1.Text = ""
                        olbsscan2.Text = ""
                        Call ClearLabelLine1Data()

                    End If

                Else

                    olbtime1.Text = ""
                    olbqa1.Text = "-"
                    olbstarget1.Text = ""
                    olbstarget2.Text = ""
                    olbsscan1.Text = ""
                    olbsscan2.Text = ""
                    Call ClearLabelLine1Data()
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
                Dim _TotalHourTarget As Integer = 0
                Dim _TotalTargetPerHour As Integer = 0
                Dim _dttime As DataTable
                Dim _TotalCountEmp As Integer = 0
                Dim _TimeServer As String = ""
                Dim _FNSam As Double = 0
                Dim _Qry As String
                Dim _dttimeplan As DataTable
                Dim _TimeWorlPlanMinute As Integer = 0

                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime,FNTargetPerHour "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2)) & ""
                _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "
                _dttimeplan = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                For Each R As DataRow In _dttimeplan.Rows
                    _TotalTarget = Integer.Parse(Val(R!FNTarget.ToString))
                    _TotalHourTarget = Integer.Parse(Val(R!FNTargetPerHour.ToString))

                    If R!FTWorkTime.ToString <> "" Then
                        Me.olbtime2.Text = R!FTWorkTime.ToString.Split(":")(0)
                        Try
                            _TimeWorlPlanMinute = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * 60) + (Val(R!FTWorkTime.ToString.Split(":")(1))))
                        Catch ex As Exception

                        End Try

                        If _TotalHourTarget > 0 Then
                            _TotalTarget = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * _TotalHourTarget)) + Integer.Parse(((Val(R!FTWorkTime.ToString.Split(":")(1)) * (_TotalHourTarget / 60.0))))
                        End If

                    Else
                        Me.olbtime2.Text = "8"
                        _TimeWorlPlanMinute = 480

                        If _TotalHourTarget > 0 Then
                            _TotalTarget = ((8 * _TotalHourTarget))
                        End If

                    End If

                    Exit For
                Next

                _dttimeplan.Dispose()

                If _TotalTarget > 0 Then

                    Dim _Salary As Double = 0
                    If Val(Me.olbtime2.Text) >= 8 Then
                        _Salary = Me.SysLine2Slary
                        _Salary = _Salary + Double.Parse(Format((Me.SysLine2Slary / 8) * (Val(Me.olbtime2.Text) - 8) * 1.5, "0.00"))
                    Else
                        _Salary = Double.Parse(Format((Me.SysLine2Slary / 8) * Val(Me.olbtime2.Text), "0.00"))
                    End If

                    Me.SysLine2SlaryMax = _Salary

                    If _TotalHourTarget > 0 Then
                        olbetarget1.Text = _TotalTarget.ToString
                        olbetarget2.Text = _TotalHourTarget.ToString
                    Else
                        olbetarget1.Text = _TotalTarget.ToString
                        _TotalTargetPerHour = (_TotalTarget / _TimeWorlPlanMinute) * 60.0
                        olbetarget2.Text = _TotalTargetPerHour.ToString
                    End If


                    Dim _DateNow As String = _TransactionDate

                    Dim _Cmd As String = ""
                    Dim _oDt As DataTable
                    Dim _QtyOrder As Double = 0
                    Dim _FNHSysStyleId As Integer = 0
                    Dim _PriceCost As Double = 0
                    Dim _QtyPRO As Double = 0
                    Dim _OrderNo As String = ""
                    Dim _TotalCountStyle As Integer = 0
                    Dim TotalCalEff As Decimal = 0
                    Dim OrderQty As Integer = 0

                    '_Cmd = " Create Table #Tmpbb2 ([FTSubOrderNo] varchar(30) NULL ,[FTBarcodeBundleNo] varchar(30) NULL )  Create Index [Idx_Tmpbb2] ON #Tmpbb2([FTSubOrderNo] ,[FTBarcodeBundleNo]) "
                    '_Cmd &= vbCrLf & "INSERT INTO #Tmpbb2 (FTSubOrderNo,FTBarcodeBundleNo)"
                    '_Cmd &= vbCrLf & "   Select MAX(A.FTSubOrderNo) As FTSubOrderNo, BCD.FTBarcodeBundleNo"

                    '_Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail As A With(NOLOCK) INNER JOIN"
                    '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As B On A.FTOrderNo = B.FTOrderNo And A.FTSubOrderNo = B.FTSubOrderNo And A.FTColorway = B.FTColorway And "
                    '_Cmd &= vbCrLf & "   A.FTSizeBreakDown = B.FTSizeBreakDown INNER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As BCD With(NOLOCK) On A.FTColorway = BCD.FTColorway And A.FTSizeBreakDown = BCD.FTSizeBreakDown And A.FTOrderProdNo = BCD.FTOrderProdNo And "
                    '_Cmd &= vbCrLf & "   B.FTNikePOLineItem = BCD.FTPOLineItemNo"
                    '_Cmd &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline As O With(NOLOCK) On BCD.FTBarcodeBundleNo=O.FTBarcodeNo "
                    '_Cmd &= vbCrLf & "        WHERE (O.FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")"
                    '_Cmd &= vbCrLf & "              And O.FDDate ='" & _DateNow & "' "
                    '_Cmd &= vbCrLf & " GROUP BY BCD.FTBarcodeBundleNo"

                    '_Cmd &= vbCrLf & "      SELECT   P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId"
                    '_Cmd &= vbCrLf & "FROM   "
                    '_Cmd &= vbCrLf & " (SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                    '_Cmd &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                    '_Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                    '_Cmd &= vbCrLf & "        WHERE (S.FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ")"
                    '_Cmd &= vbCrLf & "         and S.FDScanDate ='" & _DateNow & "'     "
                    '_Cmd &= vbCrLf & "   AND O.FTBarcodeNo Is NULL "
                    '_Cmd &= vbCrLf & "    UNION "
                    '_Cmd &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , PP.FTOrderNo , ISNULL(O.FTSubOrderNo,ISNULL(XXA.FTSubOrderNo,'')) AS FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                    '_Cmd &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                    '_Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                    '_Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP ON B.FTOrderProdNo = PP.FTOrderProdNo "

                    '_Cmd &= vbCrLf & "   Left OUTER JOIN  "
                    '_Cmd &= vbCrLf & " #Tmpbb2 "

                    ''_Cmd &= vbCrLf & "   SELECT MAX(A.FTSubOrderNo) AS FTSubOrderNo, BCD.FTBarcodeBundleNo"
                    ''_Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A WITH(NOLOCK) INNER JOIN"
                    ''_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTSubOrderNo = B.FTSubOrderNo AND A.FTColorway = B.FTColorway AND "
                    ''_Cmd &= vbCrLf & "   A.FTSizeBreakDown = B.FTSizeBreakDown INNER JOIN"
                    ''_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BCD WITH(NOLOCK) ON A.FTColorway = BCD.FTColorway AND A.FTSizeBreakDown = BCD.FTSizeBreakDown AND A.FTOrderProdNo = BCD.FTOrderProdNo AND "
                    ''_Cmd &= vbCrLf & "   B.FTNikePOLineItem = BCD.FTPOLineItemNo"
                    ''_Cmd &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK) ON BCD.FTBarcodeBundleNo=O.FTBarcodeNo "
                    ''_Cmd &= vbCrLf & "        WHERE (O.FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ")"
                    ''_Cmd &= vbCrLf & "              and O.FDDate ='" & _DateNow & "'     "
                    ''_Cmd &= vbCrLf & " GROUP BY BCD.FTBarcodeBundleNo"

                    '_Cmd &= vbCrLf & "    AS XXA ON O.FTBarcodeNo = XXA.FTBarcodeBundleNo"
                    '_Cmd &= vbCrLf & "        WHERE (O.FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ")"
                    '_Cmd &= vbCrLf & "              and O.FDDate ='" & _DateNow & "'     "
                    '_Cmd &= vbCrLf & "  ) AS P  RIGHT OUTER JOIN"
                    '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                    '_Cmd &= vbCrLf & "        WHERE(FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine2)) & ")"
                    '_Cmd &= vbCrLf & "and P.FDScanDate ='" & _DateNow & "'     "
                    '_Cmd &= vbCrLf & "GROUP BY  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId"
                    '_Cmd &= vbCrLf & "Drop Table #Tmpbb2"


                    _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(Me.SysLine2)) & ",'" & _DateNow & "' "
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    _FNSam = 0
                    _PriceCost = 0
                    _TotalCountStyle = 0
                    TotalCalEff = 0
                    For Each R As DataRow In _oDt.Rows
                        _TotalCountStyle = _TotalCountStyle + 1

                        _OrderNo = R!FTOrderNo.ToString
                        _FNHSysStyleId = Integer.Parse(Val(R!FNHSysStyleId.ToString))
                        OrderQty = Val(R!FNQuantity.ToString)

                        Dim _dtCost As DataTable
                        _Cmd = "   SELECT FNSam, FNCostPerMin, FNPrice"
                        _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS P WITH(NOLOCK)"
                        _Cmd &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & ""
                        _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                        _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                        _dtCost = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)

                        For Each Rxc As DataRow In _dtCost.Rows
                            _PriceCost = _PriceCost + Val(Rxc!FNPrice.ToString)
                            _FNSam = _FNSam + Val(Rxc!FNSam.ToString)

                            TotalCalEff = TotalCalEff + CDbl(Format((OrderQty * Val(Rxc!FNSam.ToString)), "0.00"))
                            Exit For
                        Next

                        _dtCost.Dispose()

                    Next

                    If _TotalCountStyle > 1 Then

                        _PriceCost = Double.Parse(Format(_PriceCost / _TotalCountStyle, "0.0000"))
                        _FNSam = Double.Parse(Format(_FNSam / _TotalCountStyle, "0.0000"))

                    End If

                    If Line2TotalEmpCountTime <= 0 Then
                        Line2GetEmployeeActualFromHR()
                    End If

                    _TotalCountEmp = Line2TotalEmpCountTime 'Integer.Parse(Val(Me.olbemp2.Text))

                    TotalCalEff = Double.Parse(Format((TotalCalEff / (_TimeWorlPlanMinute * _TotalCountEmp)) * 100.0, "0.00"))

                    olbetarget2Eff.Text = Format(TotalCalEff, "0.00")

                    Dim _dtPrice As DataTable

                    If IncenFormulaIdLine2 <= 0 Then
                        _Qry = "  Select FNLVSeq"
                        _Qry &= vbCrLf & " , FNStartEff"
                        _Qry &= vbCrLf & "   , FNEndEff"
                        _Qry &= vbCrLf & "   , FNPriceMultiple"
                        _Qry &= vbCrLf & "  ," & _PriceCost & " AS FNPrice"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetQty"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetChkQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActBalQty"
                        _Qry &= vbCrLf & "  , 0.000 AS FNPriceMul"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmount"
                        _Qry &= vbCrLf & "  , '0' AS FTStateMax"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmountMax"
                        _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMEFFLevel AS P WITH(NOLOCK)"
                        _Qry &= vbCrLf & "  ORDER BY FNLVSeq"

                    Else

                        _Qry = "  Select FNLVSeq"
                        _Qry &= vbCrLf & " , FNStartEff"
                        _Qry &= vbCrLf & "   , FNEndEff"
                        _Qry &= vbCrLf & "   , FNPriceMultiple"
                        _Qry &= vbCrLf & "  ," & _PriceCost & " AS FNPrice"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetQty"
                        _Qry &= vbCrLf & "  , 0 AS FNTargetChkQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActQty"
                        _Qry &= vbCrLf & "  , 0 AS FNActBalQty"
                        _Qry &= vbCrLf & "  , 0.000 AS FNPriceMul"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmount"
                        _Qry &= vbCrLf & "  , '0' AS FTStateMax"
                        _Qry &= vbCrLf & "  , 0.000 AS FNAmountMax"
                        _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMIncentiveFormulaLevel AS P WITH(NOLOCK)"
                        _Qry &= vbCrLf & "   WHERE FNHSysIncenFormulaId=" & IncenFormulaIdLine2 & ""
                        _Qry &= vbCrLf & "  ORDER BY FNLVSeq"

                    End If

                    _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    If _dtPrice.Rows.Count > 0 And _FNSam > 0 Then
                        Dim _TotalTGQty As Integer = ((_TimeWorlPlanMinute * _TotalCountEmp) / _FNSam)
                        Dim _TotalBFQty As Integer = 0
                        Dim _TotalQtyG As Integer = 0
                        Dim _TQty As Integer = 0
                        Dim _RowCount As Integer = _dtPrice.Rows.Count
                        Dim _RowIdx As Integer = 0
                        For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")

                            _RowIdx = _RowIdx + 1

                            If _RowIdx = _RowCount Then
                                _TQty = _TotalBFQty + 1
                            Else
                                _TQty = ((_TotalTGQty * Val(Rxp!FNEndEff.ToString) / 100))
                            End If

                            Rxp!FNTargetQty = _TQty
                            If _RowIdx = _RowCount Then
                                Rxp!FNTargetChkQty = _TQty
                                Rxp!FNActBalQty = _TQty
                                Rxp!FTStateMax = "1"
                            Else
                                Rxp!FNTargetChkQty = _TQty - _TotalBFQty
                                Rxp!FNActBalQty = _TQty - _TotalBFQty
                            End If

                            Rxp!FNPriceMul = CDbl(Format(Val(Rxp!FNPriceMultiple.ToString) * Val(Rxp!FNPrice.ToString), "0.00"))

                            _TotalBFQty = _TQty
                        Next

                        Dim _AmtMax As Double = 0

                        _RowIdx = 0

                        For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")
                            _RowIdx = _RowIdx + 1

                            If _RowIdx = _RowCount Then
                                _AmtMax = _AmtMax + CDbl(Format(Val(Rxp!FNPriceMul.ToString) * (Val(Rxp!FNActBalQty.ToString) - _TotalBFQty), "0.00"))

                            Else

                                _AmtMax = _AmtMax + CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActBalQty.ToString), "0.00"))

                            End If

                            ' Rxp!FNAmountMax = CDbl(Format(_AmtMax / _TotalCountEmp, "0.00"))

                            If Line2EmpCountMoney > 0 Then
                                Rxp!FNAmountMax = CDbl(Format(_AmtMax / Line2EmpCountMoney, "0.00"))
                            Else
                                Rxp!FNAmountMax = CDbl(Format(_AmtMax / _TotalCountEmp, "0.00"))
                            End If

                            _TotalBFQty = Val(Rxp!FNTargetQty.ToString)

                        Next

                    End If


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

                        '------ End Target

                        '------ Start Production------
                        Dim _dtprod As DataTable
                        Dim _TotalProd As Integer = 0
                        Dim _Prod As Integer = 0


                        _Qry = "    SELECT    FDScanDate  AS FTDateScan"
                        _Qry &= vbCrLf & "   ,FDScanTime  AS FTTimeScan"
                        _Qry &= vbCrLf & "   ,SUM(FNScanQuantity) AS FNScanQuantity"
                        _Qry &= vbCrLf & "    FROM  "  '[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS A WITH(NOLOCK)

                        _Qry &= vbCrLf & " (SELECT  TOP 0  S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        _Qry &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        _Qry &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        _Qry &= vbCrLf & "    UNION "
                        _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , '' ,'', B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo  "

                        _Qry &= vbCrLf & "   WHERE O.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
                        _Qry &= vbCrLf & "   And  O.FDDate ='" & Me.TransactionDate & "' and ISNULL(O.FNStateSewPack,0)  IN (0,1)   "

                        _Qry &= vbCrLf & "   ) AS A  "


                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine2)) & ""
                        _Qry &= vbCrLf & "   AND FDScanDate ='" & Me.TransactionDate & "'  "
                        _Qry &= vbCrLf & "   GROUP BY FDScanDate, FDScanTime"


                        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                        _TotalProd = 0

                        For Each R As DataRow In _dtprod.Rows
                            _TotalProd = _TotalProd + Val(R!FNScanQuantity)
                        Next

                        olbescan1.Text = _TotalProd.ToString
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

                        '_TotalProd = 589
                        '_Prod = 60

                        If _TotalProd > 0 Then
                            Dim _TotalActualProd As Integer = _TotalProd
                            Dim _TotalActualQty As Integer = 0

                            Dim _RowCount As Integer = _dtPrice.Rows.Count
                            Dim _RowIdx As Integer = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")

                                _RowIdx = _RowIdx + 1

                                If _RowIdx = _RowCount Then
                                    Rxp!FNActQty = _TotalActualProd
                                    Rxp!FNActBalQty = 0
                                Else
                                    If Val(Rxp!FNActBalQty) > _TotalActualProd Then
                                        _TotalActualQty = _TotalActualProd
                                        Rxp!FNActQty = _TotalActualQty
                                        Rxp!FNActBalQty = (Val(Rxp!FNActBalQty) - _TotalActualQty)
                                    Else
                                        _TotalActualQty = Val(Rxp!FNActBalQty)
                                        Rxp!FNActQty = _TotalActualQty
                                        Rxp!FNActBalQty = 0
                                    End If
                                End If

                                _TotalActualProd = _TotalActualProd - _TotalActualQty


                                If _TotalActualProd <= 0 Then
                                    Exit For
                                End If
                            Next

                            For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1' AND FNActBalQty >0", "FNLVSeq")

                                If _TotalProd >= Val(Rxp!FNActBalQty) Then
                                    Rxp!FNActBalQty = 0
                                Else

                                    Rxp!FNActBalQty = (Val(Rxp!FNActBalQty) - _TotalProd)
                                End If
                            Next

                            Dim _Amount As Double = 0
                            Dim _MaxSeq As Integer = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNActQty >0", "FNLVSeq")

                                If _TotalCountEmp > 0 Then


                                    If Line2EmpCountMoney > 0 Then
                                        _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00")) / Line2EmpCountMoney), "0.00"))
                                    Else
                                        _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00")) / _TotalCountEmp), "0.00"))
                                    End If


                                Else
                                    _TotalCountEmp = 0
                                End If
                                _MaxSeq = Val(Rxp!FNLVSeq)
                                Rxp!FNAmount = _Amount

                            Next


                            If _Amount > 0 And _MaxSeq > 0 Then
                                If _dtPrice.Select("FNLVSeq =" & _MaxSeq & " AND FNActBalQty>0").Length <= 0 Then
                                    For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq =" & _MaxSeq + 1 & " AND FNTargetChkQty=FNActBalQty AND FTStateMax<>'1'", "FNLVSeq")
                                        Rxp!FNAmount = _Amount
                                        Exit For
                                    Next
                                End If
                            End If

                        End If

                        olbescan1.Text = _TotalProd.ToString
                        olbescan2.Text = _Prod.ToString

                        Call ClearLabelLine2Data()

                        If _dtPrice.Select("FNActBalQty >0", "FNLVSeq").Length > 0 Then

                            Dim _RowIdx As Integer = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNActBalQty >0", "FNLVSeq")
                                _RowIdx = _RowIdx + 1
                                If _RowIdx > 2 Then
                                    Exit For
                                End If

                                Select Case _RowIdx
                                    Case 1
                                        olbelv11.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                                        olbelv13.Text = Rxp!FNActBalQty.ToString
                                        olbelv14.Tag = "0"
                                        If Val(Rxp!FNAmount.ToString) = 0 Then
                                            olbelv14.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"

                                        Else
                                            olbelv14.ForeColor = Color.Blue
                                            olbelv14.Tag = "1"
                                            olbelv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                        End If

                                    Case 2
                                        olbelv21.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                                        olbelv23.Text = Rxp!FNActBalQty.ToString
                                        olbelv24.Tag = "0"
                                        If Val(Rxp!FNAmount.ToString) = 0 Then
                                            olbelv24.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"
                                        Else
                                            olbelv24.Tag = "1"
                                            olbelv24.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                        End If

                                        'Case 3
                                        '    olbelv31.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")
                                        '    olbelv32.Text = Rxp!FNTargetQty.ToString
                                        '    olbelv33.Text = Rxp!FNActBalQty.ToString
                                        '    olbelv34.Tag = "0"
                                        '    If Val(Rxp!FNAmount.ToString) = 0 Then
                                        '        olbelv34.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"
                                        '    Else
                                        '        olbelv34.Tag = "1"
                                        '        olbelv34.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                        '    End If

                                End Select

                            Next

                        Else
                            olbelv14.Tag = "0"
                            For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")
                                olbelv11.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                                olbelv13.Text = Rxp!FNActBalQty.ToString
                                olbelv14.Tag = "1"
                                olbelv14.Text = Format(Val(Rxp!FNAmount.ToString), "0.00") ' Rxp!FNAmount.ToString
                                Exit For
                            Next

                        End If

                        For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")
                            olbelvt1.Text = Format(Val(Rxp!FNPriceMul.ToString), "0.00")

                            olbelvt3.Text = Rxp!FNActBalQty.ToString
                            olbelvt4.Text = Format(Val(Rxp!FNAmountMax.ToString), "0.00") '"-"
                            Exit For
                        Next

                        Me._Line2Data = _dtPrice.Copy
                        _dtPrice.Dispose()
                        '------ Start Production------

                        '-------New Info ------------

                        _Cmd = "SELECT   Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
                        _Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
                        _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty "
                        _Cmd &= vbCrLf & "	,(SUM(ISNULL(D.FNTotalDefect,0))) AS FNTotalDefect"
                        _Cmd &= vbCrLf & "		,  sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        _Cmd &= vbCrLf & "  ,((SUM(ISNULL(D.FNTotalDefect,0)))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"

                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B  WITH (NOLOCK)  ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH (NOLOCK) ON A.FTOrderNo = O.FTOrderNo"

                        _Cmd &= vbCrLf & "   OUTER APPLY(SELECT  SUM ( 1) AS FNTotalDefect "
                        _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail As D WITH( NOLOCK)"

                        _Cmd &= vbCrLf & " WHERE D.FNHSysStyleId = A.FNHSysStyleId"
                        _Cmd &= vbCrLf & "  And D.FNHSysUnitSectId =A.FNHSysUnitSectId"
                        _Cmd &= vbCrLf & "  And D.FTOrderNo =A.FTOrderNo"
                        _Cmd &= vbCrLf & "  And D.FDQADate =A.FDQADate"
                        _Cmd &= vbCrLf & "  And D.FNHourNo =A.FNHourNo"
                        _Cmd &= vbCrLf & "  And D.FTStateReject ='1'	"
                        _Cmd &= vbCrLf & " ) AS D"
                        _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & _DateNow & "')"
                        _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine2))
                        _Cmd &= vbCrLf & "group by    A.FNHSysUnitSectId,  A.FDQADate "
                        _Cmd &= vbCrLf & "Order by A.FDQADate"
                        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                        Dim _QAPer As Double = 100

                        If _TotalProd <= 0 Then
                            _QAPer = 0
                        End If

                        For Each R As DataRow In _oDt.Rows
                            If Val(R!FNQAActualQty) > 0 Then
                                '  _QAPer = CDbl(Format(((R!FNQAActualQty - R!FNTotalDefect) / R!FNQAActualQty) * 100, "0.00"))
                                _QAPer = CDbl(Format(100.0 - CDbl(Format(((R!FNTotalDefect) / R!FNQAActualQty) * 100, "0.00")), "0.00"))

                            End If

                            Exit For
                        Next
                        If _QAPer >= 100 Then
                            Me.olbqa2.Text = "100"
                        Else
                            If _TotalProd <= 0 Then
                                Me.olbqa2.Text = "-"
                            Else
                                Me.olbqa2.Text = Format(_QAPer, "0.0")
                            End If

                        End If


                    Else

                        olbtime2.Text = ""
                        olbqa2.Text = "-"
                        olbetarget1.Text = ""
                        olbetarget2.Text = ""
                        olbescan1.Text = ""
                        olbescan2.Text = ""
                        Call ClearLabelLine2Data()

                    End If

                Else

                    olbtime2.Text = ""
                    olbqa2.Text = "-"
                    olbetarget1.Text = ""
                    olbetarget2.Text = ""
                    olbescan1.Text = ""
                    olbescan2.Text = ""
                    'olbeprodspeed1.Text = ""
                    'olbeprodspeed2.Text = ""
                    Call ClearLabelLine2Data()
                End If

            Catch ex As Exception
            End Try


        End If
    End Sub

    Private Delegate Sub DelegateLoadTime()
    Private Sub CheckLoadTime()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateLoadTime(AddressOf CheckLoadTime), New Object() {})
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
        Try
            'Me.olbhour.Text = Format(DateAdd(DateInterval.Second, 1, CDate(_TransactionDate & " " & Me.olbhour.Text)), "HH:mm:ss")
            CheckLoadTime()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub olbqa1_TextChanged(sender As Object, e As EventArgs) Handles olbqa1.TextChanged
        Try
            Me.opnqa1.BackColor = Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(olbqa1.Text) >= 99.0)
                    Me.opnqa1.BackColor = Color.FromArgb(0, 192, 0)
                Case (Val(olbqa1.Text) < 99.0) And (Val(olbqa1.Text) >= 95.0)
                    Me.opnqa1.BackColor = Color.FromArgb(255, 128, 0)
                Case Else
                    Me.opnqa1.BackColor = Color.Red
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub olbqa2_TextChanged(sender As Object, e As EventArgs) Handles olbqa2.TextChanged
        Try
            Me.opnqa2.BackColor = Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(olbqa2.Text) >= 99.0)
                    Me.opnqa2.BackColor = Color.FromArgb(0, 192, 0)
                Case (Val(olbqa2.Text) < 99.0) And (Val(olbqa2.Text) >= 95.0)
                    Me.opnqa2.BackColor = Color.FromArgb(255, 128, 0)
                Case Else
                    Me.opnqa2.BackColor = Color.Red
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub otmline1checkemp09_Tick(sender As Object, e As EventArgs) Handles otmline1checkemp09.Tick
        'If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "09" Then
        '    Call Line1GetEmployeeActualFromHR()
        'End If
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) <> Line1CheckTime Or (Microsoft.VisualBasic.Left(Me.olbhour.Text, 5) = "08:30") Then

            Line1CheckTime = Microsoft.VisualBasic.Left(Me.olbhour.Text, 2)
            Call Line1GetEmployeeActualFromHR()

        End If

    End Sub

    Private Sub otmline1checkemp10_Tick(sender As Object, e As EventArgs) Handles otmline1checkemp10.Tick
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "10" Then

            Call Line1GetEmployeeActualFromHR()

        End If
    End Sub

    Private Sub otmline1checkemp11_Tick(sender As Object, e As EventArgs) Handles otmline1checkemp11.Tick
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "11" Then

            Call Line1GetEmployeeActualFromHR()

        End If
    End Sub

    Private Sub otmline2checkemp09_Tick(sender As Object, e As EventArgs) Handles otmline2checkemp09.Tick
        'If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "09" Then
        '    Call Line2GetEmployeeActualFromHR()
        'End If

        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) <> Line2CheckTime Or (Microsoft.VisualBasic.Left(Me.olbhour.Text, 5) = "08:30") Then

            Line2CheckTime = Microsoft.VisualBasic.Left(Me.olbhour.Text, 2)
            Call Line2GetEmployeeActualFromHR()

        End If

    End Sub

    Private Sub otmline2checkemp10_Tick(sender As Object, e As EventArgs) Handles otmline2checkemp10.Tick
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "10" Then
            Call Line2GetEmployeeActualFromHR()
        End If
    End Sub


    Private Sub otmline2checkemp11_Tick(sender As Object, e As EventArgs) Handles otmline2checkemp11.Tick
        If Microsoft.VisualBasic.Left(Me.olbhour.Text, 2) = "11" Then
            Call Line2GetEmployeeActualFromHR()
        End If
    End Sub

    Private Sub olbslv14_TextChanged(sender As Object, e As EventArgs) Handles olbslv14.TextChanged
        Try
            olbslv14.ForeColor = Color.Blue
            If IsNumeric(olbslv14.Text) Then
                If olbslv14.Tag.ToString = "1" AndAlso Me.SysLine1SlaryMax > 0 Then

                    If CDbl(olbslv14.Text) > Me.SysLine1SlaryMax Then
                        olbslv14.ForeColor = Color.Green
                    Else
                        olbslv14.ForeColor = Color.Red
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub olbelv14_TextChanged(sender As Object, e As EventArgs) Handles olbelv14.TextChanged
        Try
            olbelv14.ForeColor = Color.Blue
            If IsNumeric(olbelv14.Text) Then
                If olbelv14.Tag.ToString = "1" AndAlso Me.SysLine2SlaryMax > 0 Then

                    If CDbl(olbelv14.Text) > Me.SysLine2SlaryMax Then
                        olbelv14.ForeColor = Color.Green
                    Else
                        olbelv14.ForeColor = Color.Red
                    End If

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub olbsscan1_TextChanged(sender As Object, e As EventArgs) Handles olbsscan1.TextChanged
        Try
            If IsNumeric(olbsscan1.Text.Trim) Then
                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday

                Dim _T1 As Integer = 0
                Dim _T2 As Integer = 0
                Dim _T3 As Integer = 0
                Dim _TotalScan As Integer = Integer.Parse((olbsscan1.Text.Trim))
                Dim _Total As Integer = 0
                Dim _TotalH As Integer = Integer.Parse((olbtime1.Text))

                Dim _CurrentTime As String = HI.Conn.SQLConn.GetField(" SELECT " & HI.UL.ULDate.FormatTimeDB & "", Conn.DB.DataBaseName.DB_SYSTEM, "")

                If _CurrentTime <> "" And _CurrentTime >= "08:00" Then

                    If _CurrentTime >= TimeInM And _CurrentTime <= TimeOutM Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & _CurrentTime))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & TimeOutM))
                    End If

                    If _CurrentTime >= TimeInA Then
                        If _CurrentTime <= TimeOutA Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInA), CDate(Me.Actualdate & "  " & _CurrentTime))
                        End If
                    End If

                    If _TotalH > 8 Then
                        If _CurrentTime >= "17:30" Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOT), CDate(Me.Actualdate & "  " & _CurrentTime))
                        End If
                    End If

                    _Total = (_T1 + _T2 + _T3) * 60

                    If _Total <= 0 Then

                        olbsprodspeed1.Text = "" '_TotalScan.ToString()15960/757
                        olbsprodspeed2.Text = ""
                    Else

                        olbsprodspeed1.Text = Format((_Total / _TotalScan), "0")
                        olbsprodspeed2.Text = Format((3600 / Val(olbsprodspeed1.Text)), "0")
                    End If

                Else
                    olbsprodspeed1.Text = ""
                    olbsprodspeed2.Text = ""
                End If

            Else
                olbsprodspeed1.Text = ""
                olbsprodspeed2.Text = ""
            End If
        Catch ex As Exception
            olbsprodspeed1.Text = ""
            olbsprodspeed2.Text = ""
        End Try
    End Sub

    Private Sub olbescan1_TextChanged(sender As Object, e As EventArgs) Handles olbescan1.TextChanged
        Try
            If IsNumeric(olbescan1.Text.Trim) Then
                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday

                Dim _T1 As Integer = 0
                Dim _T2 As Integer = 0
                Dim _T3 As Integer = 0
                Dim _TotalScan As Integer = Integer.Parse((olbescan1.Text.Trim))
                Dim _Total As Integer = 0
                Dim _TotalH As Integer = Integer.Parse((olbtime2.Text))

                Dim _CurrentTime As String = HI.Conn.SQLConn.GetField(" SELECT " & HI.UL.ULDate.FormatTimeDB & "", Conn.DB.DataBaseName.DB_SYSTEM, "")

                If _CurrentTime <> "" And _CurrentTime >= "08:00" Then

                    If _CurrentTime >= TimeInM And _CurrentTime <= TimeOutM Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & _CurrentTime))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & TimeOutM))
                    End If

                    If _CurrentTime >= TimeInA Then
                        If _CurrentTime <= TimeOutA Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInA), CDate(Me.Actualdate & "  " & _CurrentTime))
                        End If
                    End If

                    If _TotalH > 8 Then
                        If _CurrentTime >= "17:30" Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOT), CDate(Me.Actualdate & "  " & _CurrentTime))
                        End If
                    End If

                    _Total = (_T1 + _T2 + _T3) * 60

                    If _Total <= 0 Then

                        olbeprodspeed1.Text = "" '_TotalScan.ToString()
                        olbeprodspeed2.Text = ""
                    Else

                        olbeprodspeed1.Text = Format((_Total / _TotalScan), "0")
                        olbeprodspeed2.Text = Format((3600 / Val(olbeprodspeed1.Text)), "0")
                    End If

                Else
                    olbeprodspeed1.Text = ""
                    olbeprodspeed2.Text = ""
                End If

            Else
                olbeprodspeed1.Text = ""
                olbeprodspeed2.Text = ""
            End If
        Catch ex As Exception
            olbeprodspeed1.Text = ""
            olbeprodspeed2.Text = ""
        End Try
    End Sub

    Private Sub otmcheckswitchtospeed_Tick(sender As Object, e As EventArgs) Handles otmcheckswitchtospeed.Tick

        Me.opnslv3.Dock = System.Windows.Forms.DockStyle.None
        Me.opnelv3.Dock = System.Windows.Forms.DockStyle.None

        Me.opnslv3.Visible = False
        Me.opnelv3.Visible = False

        Me.opnshowheaders.Visible = True
        Me.opnshowheadere.Visible = True

        Me.opnshowheaders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opnshowheadere.Dock = System.Windows.Forms.DockStyle.Fill



        olbstarget02.Dock = System.Windows.Forms.DockStyle.None
        olbstarget2.Dock = System.Windows.Forms.DockStyle.None
        olbstarget02.Visible = False
        olbstarget2.Visible = False

        olbstarget02Eff.Visible = True
        olbstarget2Eff.Visible = True
        olbstarget02Eff.Dock = System.Windows.Forms.DockStyle.Fill
        olbstarget2Eff.Dock = System.Windows.Forms.DockStyle.Fill


        olbetarget02.Dock = System.Windows.Forms.DockStyle.None
        olbetarget2.Dock = System.Windows.Forms.DockStyle.None
        olbetarget02.Visible = False
        olbetarget2.Visible = False

        olbetarget02Eff.Visible = True
        olbetarget2Eff.Visible = True
        olbetarget02Eff.Dock = System.Windows.Forms.DockStyle.Fill
        olbetarget2Eff.Dock = System.Windows.Forms.DockStyle.Fill


        Me.otmcheckswitchtospeed.Enabled = False
        Me.otmcheckswitchtoheader.Enabled = True

    End Sub

    Private Sub otmcheckswitchtoheader_Tick(sender As Object, e As EventArgs) Handles otmcheckswitchtoheader.Tick

        Me.opnshowheaders.Dock = System.Windows.Forms.DockStyle.None
        Me.opnshowheadere.Dock = System.Windows.Forms.DockStyle.None

        Me.opnshowheaders.Visible = False
        Me.opnshowheadere.Visible = False

        Me.opnslv3.Visible = True
        Me.opnelv3.Visible = True

        Me.opnslv3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opnelv3.Dock = System.Windows.Forms.DockStyle.Fill


        olbstarget02Eff.Dock = System.Windows.Forms.DockStyle.None
        olbstarget2Eff.Dock = System.Windows.Forms.DockStyle.None
        olbstarget02Eff.Visible = False
        olbstarget2Eff.Visible = False


        olbstarget02.Dock = System.Windows.Forms.DockStyle.Fill
        olbstarget2.Dock = System.Windows.Forms.DockStyle.Fill
        olbstarget02.Visible = True
        olbstarget2.Visible = True


        olbetarget02Eff.Dock = System.Windows.Forms.DockStyle.None
        olbetarget2Eff.Dock = System.Windows.Forms.DockStyle.None
        olbetarget02Eff.Visible = False
        olbetarget2Eff.Visible = False


        olbetarget02.Dock = System.Windows.Forms.DockStyle.Fill
        olbetarget2.Dock = System.Windows.Forms.DockStyle.Fill
        olbetarget02.Visible = True
        olbetarget2.Visible = True


        Me.otmcheckswitchtoheader.Enabled = False
        Me.otmcheckswitchtospeed.Enabled = True

    End Sub


End Class
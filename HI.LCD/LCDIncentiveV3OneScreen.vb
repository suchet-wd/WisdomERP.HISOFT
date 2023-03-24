Imports System.Drawing
Imports System.Threading

Public Class LCDIncentiveV3OneScreen
    Private _StateFTStateDaily As Boolean = False
    Private _TimeSwitchtoSpeed As Integer = 0
    Private _TimeSwitchToHeader As Integer = 1


    Private _TotalEmpFromMasterLine1 As Integer = 0
    Private _TotalEmpHRmorningLine1 As Integer = 0


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

            Me.Line1 = R!FTUnitSectCode.ToString
            Me.SysLine1 = Val(R!FNHSysUnitSectId.ToString)
            Me.IncenFormulaIdLine1 = Val(R!FNHSysIncenFormulaId.ToString)

        Next

        _Qry = "SELECT " & HI.UL.ULDate.FormatDateDB & " "
        TransactionDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

        _Actualdate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        TransactionDateBefore = ""
        StateActualDate = False

        If Me.SysLine1 > 0 Then

            _Qry = "   Select Top 1 FDDate "
            _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
            _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Me.SysLine1 & ") "
            _Qry &= vbCrLf & "  And (FDDate < N'" & TransactionDate & "') "
            _Qry &= vbCrLf & "  Order By FDDate DESC "
            TransactionDateBefore = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")


            _Qry = "   Select Top 1 FDDate "
            _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
            _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Me.SysLine1 & ") "
            _Qry &= vbCrLf & "  And (FDDate = N'" & TransactionDate & "') "
            _Qry &= vbCrLf & "  Order By FDDate DESC "
            StateActualDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> ""

        End If

        Try

            _Qry = "Select TOP 1  '1' AS FTStateDaily"
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

        'olbqc
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


    Private _TimeOutOT As String = "19:30"
    Property TimeOutOT As String
        Get
            Return _TimeOutOT
        End Get
        Set(value As String)
            _TimeOutOT = value
        End Set
    End Property

    Private _StateActualDate As Boolean = False
    Property StateActualDate As Boolean
        Get
            Return _StateActualDate
        End Get
        Set(value As Boolean)
            _StateActualDate = value
        End Set
    End Property

    Private _TransactionDateBefore As String = ""
    Property TransactionDateBefore As String
        Get
            Return _TransactionDateBefore
        End Get
        Set(value As String)
            _TransactionDateBefore = value
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

    Private _LabelDataTextQC As String = ""
    Property LabelDataTextQC As String
        Get
            Return _LabelDataTextQC
        End Get
        Set(value As String)
            _LabelDataTextQC = value
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


    Private _Line1CheckTime As String = ""
    Property Line1CheckTime As String
        Get
            Return _Line1CheckTime
        End Get
        Set(value As String)
            _Line1CheckTime = value
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



#End Region


    Private Sub LCDDisplayIncentive_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.otmline1.Enabled = False

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

        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & "	  AND TT.FTDateTrans ='" & TransactionDateBefore & "' "
        Else
            _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "
        End If


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


        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & TransactionDateBefore & "' "
            _Qry &= vbCrLf & " AND (A.FTStartDate <= '" & TransactionDateBefore & "' "
        Else
            _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
        End If


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

                If StateActualDate = False And TransactionDateBefore <> "" Then
                    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = '" & TransactionDateBefore & "' "
                Else
                    _Qry &= vbCrLf & "	  AND TT.FTDateTrans = Convert(varchar(10),Getdate(),111) "
                End If


                _Qry &= vbCrLf & "	  AND ISNULL(TT.FTIn1,'') <>''	"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"
                _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
                _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"

                _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
                _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
                _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ") "

                If StateActualDate = False And TransactionDateBefore <> "" Then
                    _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & TransactionDateBefore & "' "
                    _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & TransactionDateBefore & "' "
                Else
                    _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                    _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
                End If


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

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & TransactionDateBefore & "' "
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	 And  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"

            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >= '" & TransactionDateBefore & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & TransactionDateBefore & "')) "
            Else
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "
            End If


            dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In dtemp.Rows
                _CountEmpTime = _CountEmpTime + Integer.Parse(Val(R!CountEmpTime.ToString))
                _CountEmpMoney = _CountEmpMoney + Integer.Parse(Val(R!CountEmpMoney.ToString))
            Next

            _Qry = "    Select Sum(1) As CountEmpTime "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType As X With(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & TransactionDateBefore & "'"
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If


            _Qry &= vbCrLf & "	 And X.FNHSysUnitSectIdTo=" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 And  X.FNHSysUnitSectId<>" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	 AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & TransactionDateBefore & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & TransactionDateBefore & "')) "
            Else
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "
            End If


            _TotalCountEmp = _TotalCountEmp + (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

            _Qry = "  SELECT Sum(1) AS CountEmpTime,Sum(CASE WHEN ISNULL(PP.FTStateDaily,'0') ='1' THEN 0 ELSE 1 END) AS CountEmpMoney "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate = '" & TransactionDateBefore & "' "
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	 AND  (X.FTStartTime<='" & _Time & "' AND X.FTEndTime>='" & _Time & "')	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & "  Where  (A.FTEndDate >=  '" & TransactionDateBefore & "') "
                _Qry &= vbCrLf & "  AND (A.FTStartDate <=  '" & TransactionDateBefore & "')) "
            Else
                _Qry &= vbCrLf & "  Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & "  AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "
            End If


            dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In dtemp.Rows

                _CountEmpTime = _CountEmpTime - Integer.Parse(Val(R!CountEmpTime.ToString))
                _CountEmpMoney = _CountEmpMoney - Integer.Parse(Val(R!CountEmpMoney.ToString))

            Next

            _Qry = "    SELECT Sum(1) AS CountEmpTime"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK) ON X.FNHSysEmpID=Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & "	  WHERE X.FDDate ='" & TransactionDateBefore & "'  "
            Else
                _Qry &= vbCrLf & "	  WHERE X.FDDate = Convert(varchar(10),Getdate(),111) "
            End If

            _Qry &= vbCrLf & "	 AND X.FNHSysUnitSectIdTo<>" & Integer.Parse(Val(Me.SysLine1)) & " "
            _Qry &= vbCrLf & "	 AND  X.FNHSysUnitSectId=" & Integer.Parse(Val(Me.SysLine1)) & "	"
            _Qry &= vbCrLf & "	  AND X.FNHSysEmpID NOT IN ( "
            _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "

            If StateActualDate = False And TransactionDateBefore <> "" Then
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  '" & TransactionDateBefore & "') "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & TransactionDateBefore & "')) "
            Else
                _Qry &= vbCrLf & " Where  (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
                _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111))) "
            End If


            _TotalCountEmp = _TotalCountEmp - (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))))

        End If

        dtemp.Dispose()

        _Qry = "  SELECT Max(Emp.FNSalary) AS FNSalary"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH(NOLOCK)"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PP WITH(NOLOCK) ON Emp.FNHSysPositId = PP.FNHSysPositId"
        _Qry &= vbCrLf & "	  WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & " AND ISNULL(PP.FTStateDaily,'0') <>'1' "
        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpTypeId IN(SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) WHERE FNEmpTypeState=2  )"

        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & TransactionDateBefore & "' 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >'" & TransactionDateBefore & "' )"
        Else
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        End If


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
            'Me.olbemp1.Text = _CountEmpTime.ToString & "/" & _CountEmpMoney.ToString

            Me.olbemp1.Text = _CountEmpTime.ToString & "/" & _TotalEmpFromMasterLine1.ToString
        End If

        If _CountEmpMoney > 0 Then
            olbemp1incentive.Text = _CountEmpMoney.ToString
        Else
            olbemp1incentive.Text = ""
        End If

        Line1TotalEmpCountTime = _CountEmpTime


        Dim LeaveBeforeMin As Integer = 0
        Dim LeaveAcidentMin As Integer = 0

        Dim dtleave As DataTable

        _Qry = " SELECT       SUM(FNTotalMinute1) AS FNTotalMinute1 "
        _Qry &= vbCrLf & "  , SUM(FNTotalMinute2) AS FNTotalMinute2 "
        _Qry &= vbCrLf & "  , SUM(FNAbsent) AS FNAbsent "
        _Qry &= vbCrLf & "  , MAX(FTIn1) AS FTIn1 "
        _Qry &= vbCrLf & "  , MAX(FTOut1) AS FTOut1 "
        _Qry &= vbCrLf & "  , MAX(FTIn2) AS FTIn2 "
        _Qry &= vbCrLf & "  , MAX(FTOut2) AS FTOut2 "
        _Qry &= vbCrLf & "  , MAX(FTOtIn) AS FTOtIn "
        _Qry &= vbCrLf & "  , MAX(FTOtOut) AS FTOtOut "

        _Qry &= vbCrLf & "  FROM ( Select  Emp.FNHSysEmpID "
        _Qry &= vbCrLf & " ,ISNULL(XX1.FNTotalMinute1,0) As FNTotalMinute1 "
        _Qry &= vbCrLf & " ,ISNULL(XX2.FNTotalMinute2,0) As FNTotalMinute2  "
        _Qry &= vbCrLf & " ,ISNULL(XXT2.FNAbsent,0) As FNAbsent  "
        _Qry &= vbCrLf & " ,ISNULL(XXSHIFT.FTIn1,'') As FTIn1  "
        _Qry &= vbCrLf & " ,ISNULL(XXSHIFT.FTOut1,'') As FTOut1  "
        _Qry &= vbCrLf & " ,ISNULL(XXSHIFT.FTIn2,'') As FTIn2  "
        _Qry &= vbCrLf & " ,ISNULL(XXSHIFT.FTOut2,'') As FTOut2  "


        _Qry &= vbCrLf & " ,ISNULL(XXOT2.FTOtIn,'') As FTOtIn  "
        _Qry &= vbCrLf & " ,ISNULL(XXOT2.FTOtOut,'') As FTOtOut  "

        _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As Emp With(NOLOCK)"


        If StateActualDate = False And TransactionDateBefore <> "" Then

            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute1  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & TransactionDateBefore & "' And  X1.FTInsDate < '" & TransactionDateBefore & "'  ) As XX1 "
            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute2  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & TransactionDateBefore & "' And  X1.FTInsDate = '" & TransactionDateBefore & "'  ) As XX2 "

            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNAbsent) As FNAbsent  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = '" & TransactionDateBefore & "'  AND ISNULL(X1.FTIn1,'') ='' ) As XXT2 "

            _Qry &= vbCrLf & "    OUTER APPLY ( "
            _Qry &= vbCrLf & "                  Select TOP 1 FTOtIn, FTOtOut  "
            _Qry &= vbCrLf & "                  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest As X1 With(NOLOCK)  "

            _Qry &= vbCrLf & "                  WHERE     X1.FNHSysEmpID=Emp.FNHSysEmpID   AND  X1.FTDateRequest = '" & TransactionDateBefore & "'  ) As XXOT2 "

        Else

            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute1  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = Convert(varchar(10),Getdate(),111) And  X1.FTInsDate < Convert(varchar(10),Getdate(),111)  ) As XX1 "
            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNTotalMinute) As FNTotalMinute2  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = Convert(varchar(10),Getdate(),111) And  X1.FTInsDate = Convert(varchar(10),Getdate(),111)  ) As XX2 "

            _Qry &= vbCrLf & "    OUTER APPLY (Select SUM(FNAbsent) As FNAbsent  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans As X1 With(NOLOCK) WHERE X1.FNHSysEmpID=Emp.FNHSysEmpID  And X1.FTDateTrans = Convert(varchar(10),Getdate(),111)  AND ISNULL(X1.FTIn1,'') ='' ) As XXT2 "

            _Qry &= vbCrLf & "    OUTER APPLY ( "
            _Qry &= vbCrLf & "                  Select TOP 1 FTOtIn, FTOtOut  "
            _Qry &= vbCrLf & "                  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest As X1 With(NOLOCK)  "

            _Qry &= vbCrLf & "                  WHERE     X1.FNHSysEmpID=Emp.FNHSysEmpID   AND  X1.FTDateRequest = Convert(varchar(10),Getdate(),111)  ) As XXOT2 "

        End If

        _Qry &= vbCrLf & "    OUTER APPLY ( "
        _Qry &= vbCrLf & "                  Select TOP 1 FTIn1, FTOut1,FTIn2,FTOut2  "
        _Qry &= vbCrLf & "                  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift As X1 With(NOLOCK)  "
        _Qry &= vbCrLf & "                  WHERE     X1.FNHSysShiftID=Emp.FNHSysShiftID ) As XXSHIFT "


        _Qry &= vbCrLf & "	WHERE Emp.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ""
        _Qry &= vbCrLf & "	  And Emp.FNHSysEmpTypeId In(Select FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType As ET With (NOLOCK) WHERE FNEmpTypeState=2  )"

        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & TransactionDateBefore & "' 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >'" & TransactionDateBefore & "' )"
        Else
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        End If



        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ") "

        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & TransactionDateBefore & "') "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & TransactionDateBefore & "') "
        Else
            _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
        End If


        _Qry &= vbCrLf & "	  )   ) AS X1"

        dtleave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In dtleave.Rows

            LeaveBeforeMin = Val(R!FNTotalMinute1.ToString)
            LeaveAcidentMin = Val(R!FNTotalMinute2.ToString) ' + Val(R!FNAbsent.ToString)

            TimeInM = R!FTIn1.ToString
            TimeOutM = R!FTOut1.ToString

            TimeInA = R!FTIn2.ToString
            TimeOutA = R!FTOut2.ToString

            TimeInOT = R!FTOtIn.ToString
            TimeOutOT = R!FTOtOut.ToString

        Next

        Dim LeaveBPer As Double = (Val(LeaveBeforeMin) / (Val(_TotalEmpFromMasterLine1) * 480.0)) * 100.0
        Dim LeaveAPer As Double = (Val(LeaveAcidentMin) / (Val(_TotalEmpFromMasterLine1) * 480.0)) * 100.0

        olbleavebeforeper.Text = Format(LeaveBPer, "0.0")
        olbleaveactper.Text = Format(LeaveAPer, "0.0")

        lblgrade.ForeColor = Color.Blue
        lblgrade.Text = "-"

        _Qry = " Select  FTCalDate "
        _Qry &= vbCrLf & " , FNHSysUnitSectId "
        _Qry &= vbCrLf & ", FN5SPer "
        _Qry &= vbCrLf & ", FNReworkPer "
        _Qry &= vbCrLf & ", FNLeanPer "
        _Qry &= vbCrLf & ", FNGradeLevel "
        _Qry &= vbCrLf & ", Case When FTStateMetal ='1' THEN 'MD' ELSE '' END AS FTStateMetal "
        _Qry &= vbCrLf & ", X.FTGrade "
        _Qry &= vbCrLf & "  From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THREFFIncentive_Grade As A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  OUTER APPLY(   SELECT  X.FTNameEN AS FTGrade "
        _Qry &= vbCrLf & "	               FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData As X  WITH(NOLOCK) "
        _Qry &= vbCrLf & "	               Where (X.FTListName = N'FNGradeLevel') AND X.FNListIndex=A.FNGradeLevel "
        _Qry &= vbCrLf & ") As X "
        _Qry &= vbCrLf & "  WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(Me.SysLine1)) & ")  "

        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & "        And (FTCalDate = Convert(varchar(10),Datediff(Day,-1,Convert(Datetime,'" & TransactionDateBefore & "')),111) ) "
        Else
            _Qry &= vbCrLf & "        And (FTCalDate = Convert(varchar(10),Datediff(Day,-1,Getdate()),111) ) "
        End If


        dtleave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In dtleave.Rows

            Select Case True
                Case (R!FTStateMetal.ToString <> "")

                    lblgrade.ForeColor = Color.Red
                    lblgrade.Text = "MD"

                Case Else

                    If Val(R!FNReworkPer.ToString) > 2 Then

                        lblgrade.ForeColor = Color.Red
                        lblgrade.Text = R!FNReworkPer.ToString

                    Else

                        lblgrade.Text = R!FTGrade.ToString

                    End If
            End Select
        Next

        dtleave.Dispose()

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


        Me.LabelDataTextQC = olbqc.Text

        Dim _Qry As String
        Call ClearLabelData()
        olbsline.Text = ""
        If Me.Line1 <> "" Then

            SetData()
        End If

        '_Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "
        'Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

        Call CheckLoadTime()

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


        If StateActualDate = False And TransactionDateBefore <> "" And Me.Line1 <> "" Then
            olbqc.Text = Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEN(TransactionDateBefore), 5)
        End If
    End Sub

    Private Sub SetData(Optional StateSwipDate As Boolean = False)

        Dim _Qry As String

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

        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= '" & TransactionDateBefore & "' 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd > '" & TransactionDateBefore & "' )"
        Else
            _Qry &= vbCrLf & "	  AND Emp.FDDateStart <= Convert(varchar(10),Getdate(),111) 	"
            _Qry &= vbCrLf & "	  AND (Emp.FDDateEnd =N'' OR Emp.FDDateEnd >Convert(varchar(10),Getdate(),111) )"
        End If

        _Qry &= vbCrLf & "	  AND Emp.FNHSysEmpID NOT IN ( "
        _Qry &= vbCrLf & "  Select  DISTINCT A.FNHSysEmpID "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTMaternityPaidDaily As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        _Qry &= vbCrLf & " Where (B.FNHSysUnitSectId =" & Integer.Parse(Val(Me.SysLine1)) & ") "

        If StateActualDate = False And TransactionDateBefore <> "" Then
            _Qry &= vbCrLf & " And (A.FTEndDate >=  '" & TransactionDateBefore & "') "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  '" & TransactionDateBefore & "') "
        Else
            _Qry &= vbCrLf & " And (A.FTEndDate >=  Convert(varchar(10),Getdate(),111)) "
            _Qry &= vbCrLf & " AND (A.FTStartDate <=  Convert(varchar(10),Getdate(),111)) "
        End If


        _Qry &= vbCrLf & "	  ) "

        _TotalCountEmp = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
        _TotalEmpFromMasterLine1 = _TotalCountEmp

        If StateSwipDate Then
            Exit Sub
        End If

        Me.olbemp1.Text = _TotalCountEmp.ToString() & "/" & _TotalCountEmp.ToString()
        olbemp1incentive.Text = _TotalCountEmp.ToString()
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
    End Sub
    Private Sub ClearLabelData()

        olbsline.Text = ""

        olbemp1.Text = ""
        olbemp1incentive.Text = ""

        olbtime1.Text = ""

        olbqa1.Text = ""

        olbstarget1.Text = ""
        olbstarget2.Text = ""

        olbsscan1.Text = ""
        olbsscan2.Text = ""
        olbslvtarget11.Text = ""

        olbslv13.Text = ""
        olbslv14.Text = ""

        olbdefect1.Text = "0"
        olbdefect2.Text = "0"
        olbtaktime.Text = ""
        lblgrade.Text = ""
        olbleavebeforeper.Text = ""
        olbleaveactper.Text = ""
        olbstarget2Eff.Text = ""

    End Sub

    Private Sub ClearLabelLine1Data()

        olbslvtarget11.Text = ""

        olbslv13.Text = ""
        olbslv14.Text = ""

    End Sub

    Private Sub LCDDisplay_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Dim _MeHeight As Integer = Me.Height
        Dim _MeWidth As Integer = Me.Width
        Dim _LineHeaderWidth As Integer = Me.Width
        Dim designheight As Integer = 800
        Dim designwidth As Integer = 1277
        Dim _PanalTargetHeightPer As Double = 0.36
        Dim _PanalTargetHeight As Integer = 288
        Dim _opnstargetqty1Width As Integer = 455
        Dim _opnsprodqty1Width As Integer = 321
        Dim _opnalbonuswidth As Integer = 151
        Dim _opnalleavewidth As Integer = 342
        Dim _olbqa1width As Integer = 176
        Dim _opnsprodqty2width As Integer = 317
        Dim _olbsscan02width As Integer = 143

        _PanalTargetHeight = _MeHeight * _PanalTargetHeightPer
        opnstargetqty.Height = _PanalTargetHeight

        opnstargetqty1.Width = _MeWidth * 0.35630383711824593
        opnsprodqty1.Width = _MeWidth * 0.25137039937353173
        opnalbonus.Width = _MeWidth * 0.1182458888018794
        opnprodper.Width = opnalbonus.Width

        opnqa1.Width = _opnalleavewidth * 0.51461988304093575
        olbleavebefore.Width = opnqa1.Width
        olbleavebeforeper.Width = opnqa1.Width
        opnqrateper.Width = opnqa1.Width

        olbdefect1.Width = _opnalleavewidth * 0.23052631578947375
        olbdefect0.Width = _opnalleavewidth * 0.0935672514619883

        olbsscan02.Width = _opnsprodqty2width * 0.45110410094637221
        olbsprodspeed01.Width = olbsscan02.Width
        olbsprodspeed02.Width = olbsscan02.Width

        opcstargetqty1.Height = opnstargetqty.Height * 0.38028169014084512
        opcsscan1.Height = opnstargetqty.Height * 0.31338028169014082

        opnsprodqty2.Height = opnsprodqty1.Height * 0.33098591549295769
        opnspeed.Height = opnsprodqty2.Height


        olbstarget01.Width = opcstargetqty1.Width / 2
        olbstarget1.Width = olbstarget01.Width
        olbstarget2.Width = olbstarget01.Width

        opnwa1.Height = opnalleave.Height * 0.352112676056338
        opnwa2.Height = opnalleave.Height * 0.29577464788732388


        opnt1.Height = opnwa1.Height
        opnt2.Height = opnwa2.Height

        Dim _P11W As Integer = 321
        Dim _PH0W As Integer = 212
        Dim _PH1W As Integer = 243
        Dim _PH2W As Integer = 251
        Dim _PH3W As Integer = 245
        Dim _PH4W As Integer = 322

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
        _PLV1 = _MeHeight * 0.1375
        _PLV2 = _MeHeight * 0.1375
        _PLV3 = _MeHeight * 0.1375
        _PLVTop = _MeHeight * 0.1375

        _PM1 = (_P2 * 0.57647)

        Me.opnl1.Width = _MeWidth

        _LineHeaderWidth = _MeWidth 'opnsline.Width

        _PLineW = _LineHeaderWidth * 0.25632911
        _PineEmpW = _LineHeaderWidth * 0.25158227848
        _PlineTimeW = _LineHeaderWidth * 0.242088607
        _PLineQAW = _LineHeaderWidth * 0.25

        _P11W = _LineHeaderWidth * 0.507911392

        _PH0W = _LineHeaderWidth * 0.1660140955364135
        _PH1W = _LineHeaderWidth * 0.1902897415818324
        _PH2W = _LineHeaderWidth * 0.27052631578947373
        _PH3W = _LineHeaderWidth * 0.27530243519245884
        _PH4W = _LineHeaderWidth * 0.25215348472983562

        Dim FontHourSize As Integer = (_P1 * 0.454545)
        Dim FHour As New Font("Tahoma", FontHourSize, FontStyle.Bold)
        'Me.opnhour.Height = _P1
        'Me.olbhour.Font = FHour

        '-----------Start Set Line 1
        opnsline.Height = _P1

        opnsheader.Height = _PLVTITLE
        opnslv1.Height = _PLV1


        opnline1.Width = _MeWidth * 0.12685982772122159
        opnemp1.Width = _MeWidth * 0.18437431480031319
        opnincentive.Width = _MeWidth * 0.15348472983555209
        opntime1.Width = _MeWidth * 0.1425215348472984

        olbsheader02.Width = _PH2W

        olbsheader2.Width = _PH3W
        olbsheader3.Width = _PH4W


        olbslvtarget11.Width = _PH2W


        olbslvtarget11.Width = _PH1W

        olbslv13.Width = _PH3W
        olbslv14.Width = _PH4W


        _CaptionWidth = _LineWidth / 2
        olbsheader02.Width = _PH2W
        olbslvtarget11.Width = _PH2W

        '-----------End Set Line 1

        Dim _ImageW As Integer = (_P11W * 0.42056075)

        'Me.opcstargetqty1.Width = _ImageW


        Me.opcsscan1.Width = _ImageW

        'Start Set Font -
        '-----------Set Font Header------------
        Dim _FontLineH As Integer = _P1 * 0.45 ' 30 '90
        Dim _FontLineH2 As Integer = _P1 * 0.3 ' 30 '90
        Dim _FontLineH3 As Integer = _P1 * 0.25 ' 30 '90
        Dim FFontLineH As New Font("Tahoma", _FontLineH, FontStyle.Bold)
        Dim FFontLineH2 As New Font("Tahoma", _FontLineH2, FontStyle.Bold)
        Dim FFontLineH3 As New Font("Tahoma", _FontLineH3, FontStyle.Bold)

        Me.olbsline.Font = FFontLineH
        Me.olbemp1.Font = FFontLineH2
        Me.olbemp1incentive.Font = FFontLineH
        Me.olbtime1.Font = FFontLineH

        olbprodper.Font = FFontLineH3
        olbqrate.Font = FFontLineH3
        olbqc.Font = FFontLineH2

        Dim _FontLineHQA As Integer = _P1 * 0.46 ' 30 '90
        Dim FFontLineHQA As New Font("Tahoma", _FontLineHQA, FontStyle.Bold)
        Me.olbqa1.Font = FFontLineHQA
        olbstarget2Eff.Font = FFontLineHQA

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

        Dim _FontLV001 As Integer = _PLV1 * 0.3835952756 ' 30 '90
        Dim FFontLV001 As New Font("Tahoma", _FontLV001, FontStyle.Bold)

        Dim _FontLV002 As Integer = _PLV1 * 0.3835952756 ' 30 '90
        Dim FFontLV002 As New Font("Tahoma", _FontLV002, FontStyle.Bold)

        Dim _FontLV003 As Integer = _PLV1 * 0.3835952756 ' 30 '90
        Dim FFontLV003 As New Font("Tahoma", _FontLV003, FontStyle.Bold)


        olbsheader02.Font = FFontLVTile
        olbsheader2.Font = FFontLVTile
        olbsheader3.Font = FFontLVTile


        olbslvtarget11.Font = FFontLV001
        olbslv13.Font = FFontLV002
        olbslv14.Font = FFontLV003

        '----E Font LV
        Dim FontHeaderLineSize As Integer = (_P2 * 0.1942991)
        Dim FHeader As New Font("Tahoma", FontHeaderLineSize, FontStyle.Bold)

        Dim FontHeaderLineSize0 As Integer = (_P2 * 0.45728972)
        Dim FHeader0 As New Font("Tahoma", FontHeaderLineSize0, FontStyle.Bold)

        Dim FontHeaderLineSize1 As Integer = (_PLVTITLE * 0.172)
        Dim FHeader1 As New Font("Tahoma", FontHeaderLineSize1, FontStyle.Bold)


        Dim FontHeaderLineSize3 As Integer = (_P2 * 0.1742991)
        Dim FHeader3 As New Font("Tahoma", FontHeaderLineSize3, FontStyle.Bold)

        Dim FontHeaderLineSize4 As Integer = (_P2 * 0.1442991)
        Dim FHeader4 As New Font("Tahoma", FontHeaderLineSize4, FontStyle.Bold)


        olbstarget1.Font = FHeader0
        olbstarget2.Font = FHeader0


        olbsscan1.Font = FHeader0
        olbsscan2.Font = FHeader0

        olbsprodspeed1.Font = FHeader0
        olbsprodspeed2.Font = FHeader0
        lblgrade.Font = FHeader0
        olbleavebeforeper.Font = FHeader0
        olbleaveactper.Font = FHeader0
        olbtaktime.Font = FHeader0
        olbstarget01.Font = FHeader


        olbsscan01.Font = FHeader

        olbsscan02.Font = FHeader3
        Me.olbsprodspeed01.Font = FHeader3
        Me.olbsprodspeed02.Font = FHeader3

        olbincentive.Font = FHeader3
        olbleavebefore.Font = FHeader3
        olbleaveact.Font = FHeader3

        Dim FontHeaderLineSize6 As Integer = (_P2 * 0.26728972)
        Dim FHeader6 As New Font("Tahoma", FontHeaderLineSize6, FontStyle.Bold)

        olbdefect1.Font = FHeader6
        olbdefect0.Font = FHeader6
        olbdefect2.Font = FHeader6

        opcemp.Width = opnemp1.Width * 0.4285714285714286
        opcincentive.Width = opnincentive.Width * 0.52040816326530615
        opntime.Width = opntime1.Width * 0.56043956043956045

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
                L1Style.Text = ""
                If StateActualDate = False And TransactionDateBefore <> "" Then
                    _Qry = "   Select Top 1 FDDate "
                    _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  Where (FNHSysUnitSectId = " & Integer.Parse(Val(SysLine1)) & ") "
                    _Qry &= vbCrLf & "  And (FDDate = N'" & TransactionDate & "') "
                    _Qry &= vbCrLf & "  Order By FDDate DESC "
                    StateActualDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> ""

                    If (StateActualDate) Then

                        olbprodper.Text = LabelDataTextQC
                        SetData(True)
                        CheckLoadTime()
                        Line1GetEmployeeActualFromHR()

                    End If

                End If

                _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime,FNTargetPerHour "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(SysLine1)) & ""

                If StateActualDate = False And TransactionDateBefore <> "" Then
                    _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDateBefore & "' AND  FDEDate>='" & Me.TransactionDateBefore & "'  "
                Else
                    _Qry &= vbCrLf & "  AND FDSDate <='" & Me.TransactionDate & "' AND  FDEDate>='" & Me.TransactionDate & "'  "
                End If

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
                        _TotalHourTarget = _TotalTargetPerHour
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
                    Dim SawGrandAmt As Decimal = 0
                    Dim stylecode As String = ""

                    If StateActualDate = False And TransactionDateBefore <> "" Then
                        _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(SysLine1)) & ",'" & TransactionDateBefore & "' "
                    Else
                        _Cmd = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_DATA_LCD_DETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(SysLine1)) & ",'" & _DateNow & "' "
                    End If

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

                        If stylecode.Contains(R!FTStyleCode.ToString) = False Then
                            If stylecode = "" Then
                                stylecode = " STYLE NO. " & R!FTStyleCode.ToString
                            Else
                                stylecode = stylecode & "," & R!FTStyleCode.ToString
                            End If
                        End If

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

                            SawGrandAmt = SawGrandAmt + CDbl(Format((OrderQty * Val(Rxc!FNPrice.ToString)), "0.00"))

                            Exit For
                        Next

                        _dtCost.Dispose()

                    Next

                    L1Style.Text = stylecode

                    If _TotalCountStyle > 1 Then

                        _PriceCost = Double.Parse(Format(_PriceCost / _TotalCountStyle, "0.0000"))
                        _FNSam = Double.Parse(Format(_FNSam / _TotalCountStyle, "0.0000"))

                    End If

                    If Line1TotalEmpCountTime <= 0 Then

                        Line1GetEmployeeActualFromHR()

                    End If

                    Dim TotalTimeMinute As Integer = _TimeWorlPlanMinute
                    TotalTimeMinute = GetTimeMinuteData()

                    If TotalTimeMinute <= 0 Then
                        TotalTimeMinute = _TimeWorlPlanMinute
                    End If

                    _TotalCountEmp = Line1TotalEmpCountTime 'Integer.Parse(Val(Me.olbemp1.Text))

                    TotalCalEff = Double.Parse(Format((TotalCalEff / (TotalTimeMinute * _TotalCountEmp)) * 100.0, "0.00"))
                    olbstarget2Eff.Text = Format(TotalCalEff, "0.0")

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
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(SysLine1)) & ""
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
                        Dim CountHour As Integer = 0
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
                            CountHour = CountHour + 1

                            _TimeTotalHour = _TimeTotalHour + (Val(R!FNTotalMinute) - Val(R!FNConfigBreakTime))

                        Next


                        _dttime.Dispose()

                        If StateActualDate = False And TransactionDateBefore <> "" Then
                            CountHour = Val(olbtime1.Text)
                        End If

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

                        _Qry &= vbCrLf & " ("
                        '_Qry &= vbCrLf & " SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                        '_Qry &= vbCrLf & "  S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                        '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                        '_Qry &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
                        '_Qry &= vbCrLf & "    UNION "
                        _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 AS FNCartonNo , FTOrderNo ,FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & "	, O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate AS FDScanDate, O.FTTime AS FDScanTime, O.FNQuantity AS FNScanQuantity"
                        _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "


                        _Qry &= vbCrLf & "   WHERE O.FNHSysUnitSectId =" & Integer.Parse(Val(SysLine1)) & "   And ISNULL(FNStateSewPack, 0)  In (0, 1)  "

                        If StateActualDate = False And TransactionDateBefore <> "" Then
                            _Qry &= vbCrLf & "   AND O.FDDate ='" & Me.TransactionDateBefore & "'"
                        Else
                            _Qry &= vbCrLf & "   AND O.FDDate ='" & Me.TransactionDate & "'"
                        End If

                        _Qry &= vbCrLf & " ) AS A  "

                        '_Qry &= vbCrLf & "   WHERE FNHSysUnitSectId =" & Integer.Parse(Val(LineId)) & ""
                        '_Qry &= vbCrLf & "   AND FDScanDate ='" & Me.TransactionDate & "'"
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



                        olbsscan1.Text = _TotalProd.ToString
                        olbsscan2.Text = _Prod.ToString

                        Call ClearLabelLine1Data()

                        If Line1EmpCountMoney > 0 Then
                            SawGrandAmt = CDbl(Format((SawGrandAmt / Line1EmpCountMoney), "0.00"))
                        Else
                            SawGrandAmt = CDbl(Format((SawGrandAmt / _TotalCountEmp), "0.00"))
                        End If

                        olbslvtarget11.Text = Format((CountHour * _TotalHourTarget), "0")
                        olbslv13.Text = Format(_TotalProd, "#,#0")
                        olbslv14.Text = Format(SawGrandAmt, "#,#0.00")

                        '------ Start Production------

                        '-------New Info ------------

                        _Cmd = "SELECT   Convert(varchar(10),Convert(datetime,A.FDQADate) ,103) as FDQADate"
                        _Cmd &= vbCrLf & "	,   sum(A.FNQAInQty) AS  FNQAInQty , SUM(A.FNQAAqlQty) AS FNQAAqlQty"
                        _Cmd &= vbCrLf & "		, SUM( A.FNQAActualQty) AS FNQAActualQty "
                        _Cmd &= vbCrLf & "	,(SUM(ISNULL(D.FNTotalDefect,0))) AS FNTotalDefect"
                        _Cmd &= vbCrLf & "		,  sum(Isnull(A.FNAndon,0)) AS FNAndon"
                        _Cmd &= vbCrLf & "  ,((SUM(ISNULL(D.FNTotalDefect,0)))*100)/ SUM( A.FNQAActualQty) AS FNDefectPer"

                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B  WITH (NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH (NOLOCK)  ON A.FTOrderNo = O.FTOrderNo"

                        _Cmd &= vbCrLf & "   OUTER APPLY(SELECT  SUM ( 1) AS FNTotalDefect "
                        _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail As D WITH( NOLOCK)"

                        _Cmd &= vbCrLf & " WHERE D.FNHSysStyleId = A.FNHSysStyleId"
                        _Cmd &= vbCrLf & "  And D.FNHSysUnitSectId =A.FNHSysUnitSectId"
                        _Cmd &= vbCrLf & "  And D.FTOrderNo =A.FTOrderNo"
                        _Cmd &= vbCrLf & "  And D.FDQADate =A.FDQADate"
                        _Cmd &= vbCrLf & "  And D.FNHourNo =A.FNHourNo"
                        _Cmd &= vbCrLf & "  And D.FTStateReject ='1'	"
                        _Cmd &= vbCrLf & " ) AS D"


                        If StateActualDate = False And TransactionDateBefore <> "" Then
                            _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & TransactionDateBefore & "')"
                        Else
                            _Cmd &= vbCrLf & " WHERE     (A.FDQADate = '" & _DateNow & "')"
                        End If

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


    Private Function GetTimeMinuteData() As Integer
        Dim SumMinute As Integer = 0
        Dim _Minute As Integer = 0
        Dim _Time As String = Microsoft.VisualBasic.Left(Me.olbhour.Text, 5)

        If _Time >= TimeInM Then

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday


            If _Time >= TimeOutM Then
                SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & TimeOutM))

            Else
                SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & _Time))
            End If


            If _Time > TimeInA Then

                If _Time >= TimeOutA Then
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInA), CDate(Me.Actualdate & "  " & TimeOutA))

                Else
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInA), CDate(Me.Actualdate & "  " & _Time))
                End If

            End If

            If _Time > TimeInOT And TimeInOT <> "" And TimeOutOT <> "" Then
                If _Time >= TimeOutOT Then
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOT), CDate(Me.Actualdate & "  " & TimeOutOT))

                Else
                    SumMinute = SumMinute + DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOT), CDate(Me.Actualdate & "  " & _Time))
                End If
            End If


        End If

        Return SumMinute
    End Function

    Private Delegate Sub DelegateLoadTime()
    Private Sub CheckLoadTime()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateLoadTime(AddressOf CheckLoadTime), New Object() {})
        Else
            Try
                Dim _Qry As String
                _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB & " "

                If StateActualDate = False And TransactionDateBefore <> "" Then

                    If TimeOutOT <> "" Then
                        Me.olbhour.Text = TimeOutOT
                    Else
                        Me.olbhour.Text = TimeOutA
                    End If

                    opnline1.BackColor = Color.FromArgb(192, 255, 192)
                    olbsline.ForeColor = Color.Black

                Else

                    opnline1.BackColor = Color.FromArgb(255, 192, 192)
                    olbsline.ForeColor = Color.Black

                    Me.olbhour.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
                End If

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

                'Dim _CurrentTime As String = HI.Conn.SQLConn.GetField(" SELECT " & HI.UL.ULDate.FormatTimeDB & "", Conn.DB.DataBaseName.DB_SYSTEM, "")
                Dim _CurrentTime As String = Microsoft.VisualBasic.Left(Me.olbhour.Text, 5)
                If _CurrentTime <> "" And _CurrentTime >= "08:00" Then

                    If _CurrentTime >= TimeInM And _CurrentTime <= TimeOutM Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & _CurrentTime))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInM), CDate(Me.Actualdate & "  " & TimeOutM))
                    End If

                    If _CurrentTime >= TimeInA Then
                        If _CurrentTime <= TimeOutA Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInA), CDate(Me.Actualdate & "  " & _CurrentTime))
                        Else
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInA), CDate(Me.Actualdate & "  " & TimeOutA))
                        End If
                    End If

                    If _TotalH > 8 Then
                        If _CurrentTime >= "17:30" Then
                            _T3 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & TimeInOT), CDate(Me.Actualdate & "  " & _CurrentTime))
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


    Private Sub otmcheckswitchtospeed_Tick(sender As Object, e As EventArgs) Handles otmcheckswitchtospeed.Tick

        'Me.opnslv3.Dock = System.Windows.Forms.DockStyle.None


        'Me.opnslv3.Visible = False


        'Me.opnshowheaders.Visible = True


        'Me.opnshowheaders.Dock = System.Windows.Forms.DockStyle.Fill


        'olbstarget02.Dock = System.Windows.Forms.DockStyle.None
        'olbstarget2.Dock = System.Windows.Forms.DockStyle.None
        'olbstarget02.Visible = False
        'olbstarget2.Visible = False

        'olbstarget02Eff.Visible = True
        'olbstarget2Eff.Visible = True
        'olbstarget02Eff.Dock = System.Windows.Forms.DockStyle.Fill
        'olbstarget2Eff.Dock = System.Windows.Forms.DockStyle.Fill


        'Me.otmcheckswitchtospeed.Enabled = False
        'Me.otmcheckswitchtoheader.Enabled = True

    End Sub

    Private Sub otmcheckswitchtoheader_Tick(sender As Object, e As EventArgs) Handles otmcheckswitchtoheader.Tick

        'Me.opnshowheaders.Dock = System.Windows.Forms.DockStyle.None


        'Me.opnshowheaders.Visible = False

        'Me.opnslv3.Visible = True

        'Me.opnslv3.Dock = System.Windows.Forms.DockStyle.Fill


        'olbstarget02Eff.Dock = System.Windows.Forms.DockStyle.None
        'olbstarget2Eff.Dock = System.Windows.Forms.DockStyle.None
        'olbstarget02Eff.Visible = False
        'olbstarget2Eff.Visible = False


        'olbstarget02.Dock = System.Windows.Forms.DockStyle.Fill
        'olbstarget2.Dock = System.Windows.Forms.DockStyle.Fill
        'olbstarget02.Visible = True
        'olbstarget2.Visible = True

        'Me.otmcheckswitchtoheader.Enabled = False
        'Me.otmcheckswitchtospeed.Enabled = True

    End Sub

    Private Sub olbstarget2_Click(sender As Object, e As EventArgs) Handles olbstarget2.Click

    End Sub

    Private Sub olbstarget2_TextChanged(sender As Object, e As EventArgs) Handles olbstarget2.TextChanged
        If IsNumeric(olbstarget2.Text) Then
            olbtaktime.Text = Format((3600.0 / Double.Parse(olbstarget2.Text)), "0")
        Else
            olbtaktime.Text = ""
        End If
    End Sub

    Private Sub olbstarget2Eff_TextChanged(sender As Object, e As EventArgs) Handles olbstarget2Eff.TextChanged
        Try
            Me.opnt1.BackColor = Color.FromArgb(0, 192, 0)

            Select Case True
                Case (Val(olbstarget2Eff.Text) >= 90.0)
                    Me.opnt1.BackColor = Color.FromArgb(0, 192, 0)
                Case Else
                    Me.opnt1.BackColor = Color.Red
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub olbslv14_Click(sender As Object, e As EventArgs) Handles olbslv14.Click

    End Sub

    Private Sub olbsprodspeed1_Click(sender As Object, e As EventArgs) Handles olbsprodspeed1.Click

    End Sub

    Private Sub olbsscan1_Click(sender As Object, e As EventArgs) Handles olbsscan1.Click

    End Sub


End Class